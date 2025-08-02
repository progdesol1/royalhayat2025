using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Threading;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Web.CRM
{
    public partial class Barcodetest : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand sqlcmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                //int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                var List = DB.TBLCONTACTs.Where(p => p.Active == "Y").ToList();
                foreach (Database.TBLCONTACT item in List)
                {

                    string str = Server.MapPath("~/CRM/images/" + item.BARCODE.Trim() + ".png");
                    if (!File.Exists(str))
                    {
                        //    if (File.Exists(Server.MapPath("Barcode.txt")))
                        //    {
                        //        File.Delete(Server.MapPath("BarCode.txt"));
                        //    }
                        //    //File.Create(Server.MapPath("BarCode.txt"));
                        //    File.WriteAllText(Server.MapPath("BarCode.txt"), item.BarCode.Trim());

                        //    Process.Start(Server.MapPath("BarCodeGenerate.exe"));
                        //    Thread.Sleep(500);
                        //}

                        string barCode = item.BARCODE.Trim();
                        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                        using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
                        {
                            using (Graphics graphics = Graphics.FromImage(bitMap))
                            {
                                Font oFont = new Font("IDAutomationHC39M", 16);
                                PointF point = new PointF(2f, 2f);
                                SolidBrush blackBrush = new SolidBrush(Color.Black);
                                SolidBrush whiteBrush = new SolidBrush(Color.White);
                                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                                graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                            }
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                byte[] byteImage = ms.ToArray();

                                Convert.ToBase64String(byteImage);
                                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                                try
                                {
                                    File.WriteAllBytes(Server.MapPath("images/" + barCode + ".png"), byteImage);
                                }
                                catch (System.Exception ex)
                                {
                                    Response.Write(ex.InnerException);

                                }
                            }
                            // plBarCode.Controls.Add(imgBarCode);
                        }
                    }
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Barcode genarate Successfully", "Barcode genarate 1", Classes.Toastr.ToastPosition.BottomLeft);
                }
            }
            catch (Exception ex)
            {
                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Error Occured During Barcode genarate 1 process !<br>" + ex.ToString(), "Barcode genarate 2", Classes.Toastr.ToastPosition.TopCenter);
            }
            // Response.Redirect("printbarcode.aspx?Partno=ACCALL0001&NoOf=1");
            //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "window.open('BarcodeList.aspx?PurchaseID=" + 1 + "','_blank','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbar=no,resizable=no,copyhistory=yes')", true);

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                var List = DB.TBLCONTACTs.Where(p => p.Active == "Y").ToList();
                foreach (Database.TBLCONTACT item in List)
                {

                    string str = Server.MapPath("~/CRM/images/" + item.BARCODE.Trim() + ".png");
                    if (!File.Exists(str))
                    {
                        if (File.Exists(Server.MapPath("Barcode.txt")))
                        {
                            File.Delete(Server.MapPath("BarCode.txt"));
                        }
                        //File.Create(Server.MapPath("BarCode.txt"));
                        File.WriteAllText(Server.MapPath("BarCode.txt"), item.BARCODE.Trim());

                        Process.Start(Server.MapPath("BarCodeGenerate.exe"));
                        Thread.Sleep(500);
                    }


                    // plBarCode.Controls.Add(imgBarCode);
                }
                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Barcode genarate Successfully", "Barcode genarate 2", Classes.Toastr.ToastPosition.BottomLeft);
            }
            catch (Exception ex)
            {
                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Error Occured During Barcode genarate 2 process !<br>" + ex.ToString(), "Barcode genarate 2", Classes.Toastr.ToastPosition.TopCenter);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection connection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);


            string backupDIR = "D:\\BackupDB";
            if (!System.IO.Directory.Exists(backupDIR))
            {
                System.IO.Directory.CreateDirectory(backupDIR);
            }

            try
            {
                con.Open();
                sqlcmd = new SqlCommand("backup database TestFastPO7 to disk='" + backupDIR + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".Bak'", con);
                sqlcmd.ExecuteNonQuery();
                con.Close();
                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Backup Database Successfully", "Backup Database", Classes.Toastr.ToastPosition.BottomLeft);
                //lblError1.Text = "Backup database successfully";
            }
            catch (Exception ex)
            {
                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Error Occured During DB backup process !<br>" + ex.ToString(), "Backup Database", Classes.Toastr.ToastPosition.TopCenter);
                //lblError1.Text = "Error Occured During DB backup process !<br>" + ex.ToString();
            }
        }

        protected void txtpass_Click(object sender, EventArgs e)
        {

            string pass = Classes.GlobleClass.EncryptionHelpers.Decrypt(TextBox1.Text);
            Label1.Text = pass;
        }

        protected void txtPassEnc_Click(object sender, EventArgs e)
        {
            string pass = Classes.GlobleClass.EncryptionHelpers.Encrypt(TextBox1.Text);
            Label1.Text = pass;
        }
    }
}