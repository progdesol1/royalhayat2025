using Database;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Master
{
    public partial class EventBarcode : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
            
        }
        public string GetImage(string partno)
        {
            if (partno != "")
            {
                return "<img src='Img/" + partno + ".png' style='margin-bottom: -10px; width: 188px;height:47px; margin-top: -3px;' />";
                //showBarcode(partno);
            }
            else
            {
                return "";
            }
        }
        public void BindGrid()
        {
            if (Session["Barcode"] != null)
            {
               
                List<Database.Tbl_Event_Register> List = ((List<Database.Tbl_Event_Register>)Session["Barcode"]).ToList();
                ListView1.DataSource = List;
                ListView1.DataBind();
            }
        }
        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            PlaceHolder plBarCode = (PlaceHolder)e.Item.FindControl("plBarCode");
            Label lblbarcodee = (Label)e.Item.FindControl("lblbarcodee");
            string Barcode = lblbarcodee.Text.ToString();
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(Barcode, QRCodeGenerator.ECCLevel.Q);
                System.Web.UI.WebControls.Image imgBarCode1 = new System.Web.UI.WebControls.Image();
                imgBarCode1.Height = 150;
                imgBarCode1.Width = 150;
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        imgBarCode1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    plBarCode.Controls.Add(imgBarCode1);
                }
            }
        }

       










    }
}