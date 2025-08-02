using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Transactions;
using Database;
using System.Collections.Generic;
using Web.ACM;
using System.IO;
using System.Data.OleDb;
using Database;
using Classes;

namespace NewHRM
{
    public partial class ImportExportFunction : System.Web.UI.Page
    {
        Database.CallEntities DB = new Database.CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                bindDropdown();
            }
        }

        public void bindDropdown()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            Classes.EcommAdminClass.getdropdown(drpModulName, TID, "0", "", "", "MODULE_MST");
            //select * from MODULE_MST where Parent_Module_id =0 

            //drpModulName.DataSource = DB.MODULE_MST.Where(P => P.ACTIVE_FLAG == "Y" && P.Parent_Module_id != 0);
            //drpModulName.DataTextField = "Module_Name";
            //drpModulName.DataValueField = "Module_Id";
            //drpModulName.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //FileUpload1.SaveAs(Server.MapPath("Upload/") + FileUpload1.FileName);
            //string CurrentFilePath = Server.MapPath("Upload/") + FileUpload1.FileName;
            ////  Path.GetFullPath(FileUpload1.PostedFile.FileName);

            //InsertExcelRecords(CurrentFilePath);

            if (FileUpload1.HasFile)
            {
                FileUpload1.SaveAs(Server.MapPath("xml/") + FileUpload1.FileName);
                string strFileName = Server.HtmlEncode(FileUpload1.FileName);
                string strExtension = Path.GetExtension(strFileName);
                if (strExtension != ".xls" && strExtension != ".xlsx")
                {
                    Response.Write("<script>alert('Please select a Excel spreadsheet to import!');</script>");
                    return;
                }
                string strUploadFileName = "xml/" + strFileName;
                FileUpload1.SaveAs(Server.MapPath(strUploadFileName));
                string strExcelConn = "";
                strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath(strUploadFileName) + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                DataTable dtExcel = RetrieveData(strExcelConn);
                SqlBulkCopyImport1(dtExcel);
                File.Delete(Server.MapPath(strUploadFileName));

            }
        }
        protected DataTable RetrieveData(string strConn)
        {
            DataTable dtExcel = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                // Initialize an OleDbDataAdapter object.
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [Sheet1$]", conn);

                // Fill the DataTable with data from the Excel spreadsheet.
                da.Fill(dtExcel);
            }
            return dtExcel;
        }
        protected void SqlBulkCopyImport1(DataTable dtExcel)
        {
            //Set Default Value
            //using (TransactionScope scope = new TransactionScope())
            //{

                string duplicateCount = "";
                for (int i = 0; i < dtExcel.Rows.Count - 1; i++)
                {
                    decimal MENU_ORDER = 0;
                    int MASTER_ID = 0;
                    DateTime ACTIVETILLDATE = DateTime.Now.AddYears(1);
                    if (dtExcel.Rows[i]["MASTER_ID"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["MASTER_ID"].ToString().Trim() != "")
                        MASTER_ID = Convert.ToInt32(dtExcel.Rows[i]["MASTER_ID"]);
                    int MODULEID = 0;
                    if (dtExcel.Rows[i]["MODULE_ID"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["MODULE_ID"].ToString().Trim() != "")
                        MODULEID = Convert.ToInt32(dtExcel.Rows[i]["MODULE_ID"]);
                    string MENU_TYPE = "";
                    if (dtExcel.Rows[i]["MENU_TYPE"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["MENU_TYPE"].ToString().Trim() != "")
                        MENU_TYPE = dtExcel.Rows[i]["MENU_TYPE"].ToString();
                    string MENU_NAME1 = "";
                    if (dtExcel.Rows[i]["MENU_NAME1"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["MENU_NAME1"].ToString().Trim() != "")
                        MENU_NAME1 = dtExcel.Rows[i]["MENU_NAME1"].ToString();
                    string MENU_NAME2 = "";
                    if (dtExcel.Rows[i]["MENU_NAME2"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["MENU_NAME2"].ToString().Trim() != "")
                        MENU_NAME2 = dtExcel.Rows[i]["MENU_NAME2"].ToString();
                    string MENU_NAME3 = "";
                    if (dtExcel.Rows[i]["MENU_NAME3"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["MENU_NAME3"].ToString().Trim() != "")
                        MENU_NAME3 = dtExcel.Rows[i]["MENU_NAME3"].ToString();

                    string LINK = "";
                    if (dtExcel.Rows[i]["LINK"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["LINK"].ToString().Trim() != "")
                        LINK = dtExcel.Rows[i]["LINK"].ToString();

                    string URLREWRITE = "";
                    if (dtExcel.Rows[i]["URLREWRITE"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["URLREWRITE"].ToString().Trim() != "")
                        URLREWRITE = dtExcel.Rows[i]["URLREWRITE"].ToString();

                    string MENU_LOCATION = "";
                    if (dtExcel.Rows[i]["MENU_LOCATION"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["MENU_LOCATION"].ToString().Trim() != "")
                        MENU_LOCATION = dtExcel.Rows[i]["MENU_LOCATION"].ToString();

                    if (dtExcel.Rows[i]["MENU_ORDER"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["MENU_ORDER"].ToString().Trim() != "")
                        MENU_ORDER = Convert.ToDecimal(dtExcel.Rows[i]["MENU_ORDER"]);

                    string DOC_PARENT = "";
                    if (dtExcel.Rows[i]["DOC_PARENT"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["DOC_PARENT"].ToString().Trim() != "")
                        DOC_PARENT = dtExcel.Rows[i]["DOC_PARENT"].ToString();


                    int ADDFLAGE = dtExcel.Rows[i]["ADDFLAGE"].ToString() == "1" ? 1 : 0;
                    int EDITFLAGE = dtExcel.Rows[i]["EDITFLAGE"].ToString() == "1" ? 1 : 0;
                    int DELFLAGE = dtExcel.Rows[i]["DELFLAGE"].ToString() == "1" ? 1 : 0;
                    int PRINTFLAGE = dtExcel.Rows[i]["PRINTFLAGE"].ToString() == "1" ? 1 : 0;
                    int AMIGLOBALE = dtExcel.Rows[i]["AMIGLOBALE"].ToString() == "1" ? 1 : 0;
                    int MYPERSONAL = dtExcel.Rows[i]["MYPERSONAL"].ToString() == "1" ? 1 : 0;
                    int REFID = 0;
                    int TenentID = 0;
                    int MYBUSID = 0;
                    int CRUP_ID = 1;
                    int Role_ID = 0;
                    string SMALLTEXT = "";
                    if (dtExcel.Rows[i]["SMALLTEXT"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["SMALLTEXT"].ToString().Trim() != "")
                        SMALLTEXT = dtExcel.Rows[i]["SMALLTEXT"].ToString();
                    if (dtExcel.Rows[i]["ACTIVETILLDATE"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["ACTIVETILLDATE"].ToString().Trim() != "")
                    {
                        ACTIVETILLDATE = Convert.ToDateTime(dtExcel.Rows[i]["ACTIVETILLDATE"]);
                    }

                    string ICONPATH = "";
                    if (dtExcel.Rows[i]["ICONPATH"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["ICONPATH"].ToString().Trim() != "")
                        ICONPATH = dtExcel.Rows[i]["ICONPATH"].ToString();

                    string COMMANLINE = "";
                    if (dtExcel.Rows[i]["COMMANLINE"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["COMMANLINE"].ToString().Trim() != "")
                        COMMANLINE = dtExcel.Rows[i]["COMMANLINE"].ToString();

                    int ACTIVE_FLAG = 1;
                    string METATITLE = "";
                    if (dtExcel.Rows[i]["METATITLE"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["METATITLE"].ToString().Trim() != "")
                        METATITLE = dtExcel.Rows[i]["METATITLE"].ToString();
                    //objFUNCTION_MST.METATITLE = txtMETATITLE.Text;
                    string METAKEYWORD = "";
                    if (dtExcel.Rows[i]["METAKEYWORD"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["METAKEYWORD"].ToString().Trim() != "")
                        METAKEYWORD = dtExcel.Rows[i]["METAKEYWORD"].ToString();

                    string METADESCRIPTION = "";
                    if (dtExcel.Rows[i]["METADESCRIPTION"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["METADESCRIPTION"].ToString().Trim() != "")
                        METADESCRIPTION = dtExcel.Rows[i]["METADESCRIPTION"].ToString();

                    string HEADERVISIBLEDATA = "";
                    if (dtExcel.Rows[i]["HEADERVISIBLEDATA"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["HEADERVISIBLEDATA"].ToString().Trim() != "")
                        HEADERVISIBLEDATA = dtExcel.Rows[i]["HEADERVISIBLEDATA"].ToString();

                    string HEADERINVISIBLEDATA = "";
                    if (dtExcel.Rows[i]["HEADERINVISIBLEDATA"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["HEADERINVISIBLEDATA"].ToString().Trim() != "")
                        HEADERINVISIBLEDATA = dtExcel.Rows[i]["HEADERINVISIBLEDATA"].ToString();

                    string FOOTERVISIBLEDATA = "";
                    if (dtExcel.Rows[i]["FOOTERVISIBLEDATA"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["FOOTERVISIBLEDATA"].ToString().Trim() != "")
                        FOOTERVISIBLEDATA = dtExcel.Rows[i]["FOOTERVISIBLEDATA"].ToString();

                    string FOOTERINVISIBLEDATA = "";
                    if (dtExcel.Rows[i]["FOOTERINVISIBLEDATA"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["FOOTERINVISIBLEDATA"].ToString().Trim() != "")
                        FOOTERINVISIBLEDATA = dtExcel.Rows[i]["FOOTERINVISIBLEDATA"].ToString();


                    if (dtExcel.Rows[i]["REFID"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["REFID"].ToString().Trim() != "")
                    {
                        REFID = Convert.ToInt32(dtExcel.Rows[i]["REFID"]);
                    }

                    if (dtExcel.Rows[i]["MYBUSID"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["MYBUSID"].ToString().Trim() != "")
                    {
                        MYBUSID = Convert.ToInt32(dtExcel.Rows[i]["MYBUSID"]);
                    }

                    int LOCATION_ID = 0;
                    if (dtExcel.Rows[i]["LOCATION_ID"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["LOCATION_ID"].ToString().Trim() != "")
                    {
                        LOCATION_ID = Convert.ToInt32(dtExcel.Rows[i]["LOCATION_ID"]);
                    }


                    if (dtExcel.Rows[i]["TenentID"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["TenentID"].ToString().Trim() != "")
                    {
                        TenentID = Convert.ToInt32(dtExcel.Rows[i]["TenentID"]);
                    }
                    int MEMUID12 = 0;
                    if (dtExcel.Rows[i]["MENU_ID"].ToString().Trim() != "0" && dtExcel.Rows[i]["MENU_ID"].ToString().Trim() != "")
                    {
                        MEMUID12 = Convert.ToInt32(dtExcel.Rows[i]["MENU_ID"]);
                    }
                    if (DB.FUNCTION_MST.Where(p => p.MENU_NAME1 == MENU_NAME1 && p.TenentID == TenentID && p.MENU_ID == MEMUID12).Count() > 0)
                    {
                        duplicateCount += "," + MEMUID12;
                        lblMsg.Text = "Duplicate Found Menu ID " + duplicateCount;
                        pnlSuccessMsg.Visible = true;

                    }
                    else
                    {
                        if (DB.MODULE_MST.Where(p => p.TenentID == TenentID && p.Module_Id == MODULEID ).Count() > 0)
                        {
                            int MEMUID = Classes.ACMClass.InsertDataACMFunction(MEMUID12, MASTER_ID, MODULEID, MENU_TYPE, MENU_NAME1, MENU_NAME1,
                           MENU_NAME1, LINK, URLREWRITE, MENU_LOCATION, MENU_ORDER, DOC_PARENT, SMALLTEXT, ACTIVETILLDATE, ICONPATH,
                           COMMANLINE, METAKEYWORD, METADESCRIPTION, HEADERVISIBLEDATA, HEADERINVISIBLEDATA, FOOTERVISIBLEDATA,
                           FOOTERINVISIBLEDATA, REFID, TenentID, LOCATION_ID, MYBUSID, CRUP_ID, METATITLE,
                           ADDFLAGE, EDITFLAGE, DELFLAGE, PRINTFLAGE, AMIGLOBALE, MYPERSONAL, ACTIVE_FLAG);
                            var list = DB.PRIVILEGE_MST.Where(p => p.TenentID == TenentID && p.MODULE_ID == MODULEID).ToList();
                            //foreach (Database.PRIVILEGE_MST item in list)
                            //{

                            //    int LOCATION_ID = LOCATIONID;
                            int PRIVILEGE_ID = 0; //Convert.ToInt32(item.PRIVILEGE_ID);
                            int UserID = 0;
                            if (dtExcel.Rows[i]["UserID"].ToString().Trim() != "NULL" && dtExcel.Rows[i]["UserID"].ToString().Trim() != "")
                            {
                                UserID = Convert.ToInt32(dtExcel.Rows[i]["UserID"]);
                                Database.USER_MST obj_user = new Database.USER_MST();
                                if (DB.USER_MST.Where(p => p.USER_ID == UserID && p.TenentID == TenentID && p.LOCATION_ID == LOCATION_ID).Count() > 0)
                                {
                                    obj_user = DB.USER_MST.SingleOrDefault(p => p.USER_ID == UserID );
                                }
                                else
                                {
                                    pnlSuccessMsg.Visible = true;
                                    lblMsg.Text = UserID + " is not Exist of User Mst And " + i + " Records Successfully added.";
                                    return;
                                }

                                Role_ID = Convert.ToInt32(obj_user.USER_TYPE);
                            }
                            if (DB.PRIVILEGE_MST.Where(p => p.TenentID == TenentID && p.MODULE_ID == MODULEID  ).Count() == 1)
                            {
                                Database.PRIVILEGE_MST obj_Privil = DB.PRIVILEGE_MST.SingleOrDefault(p => p.TenentID == TenentID && p.MODULE_ID == MODULEID );
                                Classes.ACMClass.InsertDataACMPRIVILAGEMENU(TenentID, LOCATION_ID, obj_Privil.PRIVILEGE_ID, MEMUID);
                                if (DB.PRIVILEGE_MST.Where(p => p.TenentID == TenentID && p.MODULE_ID == MODULEID).Count() > 0)
                                {
                                    if (DB.MODULE_MAP.Where(p => p.PRIVILEGE_ID == obj_Privil.PRIVILEGE_ID && p.LOCATION_ID == LOCATION_ID && p.TenentID == TenentID && p.UserID == UserID).Count() !=1)
                                    {
                                        if (DB.MODULE_MAP.Where(p => p.PRIVILEGE_ID == obj_Privil.PRIVILEGE_ID && p.LOCATION_ID == LOCATION_ID && p.TenentID == TenentID && p.UserID == UserID).Count() == 0)
                                        {

                                            //insert data in ACMMODULEMAP table
                                            int MAPID = DB.MODULE_MAP.Count() > 0 ? Convert.ToInt32(DB.MODULE_MAP.Max(p => p.MODULE_MAP_ID) + 1) : 1;
                                            //Classes.ACMClass.InsertDataACMMODULEMAP(TenentID, LOCATION_ID, MAPID, obj_Privil.PRIVILEGE_ID, MODULEID, UserID);
                                            Classes.ACMClass.InsertDataACMMODULEMAP(TenentID, LOCATION_ID, obj_Privil.PRIVILEGE_ID, MODULEID, UserID, MAPID, "Y", 1, 999999999, "", "", "", "", "");
                                        }
                                        else
                                        {
                                            pnlSuccessMsg.Visible = true;
                                            lblMsg.Text = "Please Check Module Map Table";
                                            return;
                                        }
                                    }
                                    if (DB.ROLE_MST.Where(p => p.TenentID == TenentID && p.ROLE_ID == Role_ID ).Count() != 1)
                                    {
                                        if (DB.ROLE_MST.Where(p => p.TenentID == TenentID && p.ROLE_ID == Role_ID ).Count() > 0)
                                        {//insert data in ACMUSERROLE table
                                            if (DB.USER_ROLE.Where(p => p.PRIVILEGE_ID == obj_Privil.PRIVILEGE_ID && p.LOCATION_ID == LOCATION_ID && p.TenentID == TenentID && p.USER_ID == UserID).Count() == 0)
                                            {
                                                Classes.ACMClass.InsertDataACMUSERROLE(TenentID, LOCATION_ID, obj_Privil.PRIVILEGE_ID, UserID, Role_ID);
                                            }
                                            else
                                            {
                                                pnlSuccessMsg.Visible = true;
                                                lblMsg.Text = "Please Check User Role Table";
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            pnlSuccessMsg.Visible = true;
                                            lblMsg.Text = "Please Check Role Mst Table role Id is incurrent";
                                            return;
                                        }
                                    }
                                    if (DB.USER_RIGHTS.Where(p => p.PRIVILEGE_ID == obj_Privil.PRIVILEGE_ID && p.USER_ID == UserID && p.TenentID == TenentID && p.LOCATION_ID == LOCATION_ID).Count() != 1)
                                    {

                                        if (DB.USER_RIGHTS.Where(p => p.PRIVILEGE_ID == obj_Privil.PRIVILEGE_ID && p.USER_ID == UserID && p.TenentID == TenentID && p.LOCATION_ID == LOCATION_ID).Count() == 0)
                                        {
                                            //insert data in USER_RIGHT table
                                            Classes.ACMClass.InsertDataACMUSERRIGHTS(TenentID, LOCATION_ID, obj_Privil.PRIVILEGE_ID, UserID);
                                        }
                                        else
                                        {
                                            pnlSuccessMsg.Visible = true;
                                            lblMsg.Text = "Please Check User Rights Table";
                                            return;
                                        }
                                    }
                                }
                                   
                             
                                else
                                {
                                    pnlSuccessMsg.Visible = true;
                                    lblMsg.Text = "Please Check Privilege Table";
                                    return;
                                }

                            }
                            else
                            {
                                pnlSuccessMsg.Visible = true;
                                lblMsg.Text = "Please Enter the Privilege Id ";
                                return;
                            }
                        }
                        else
                        {
                            pnlSuccessMsg.Visible = true;
                            lblMsg.Text = "Please Enter the Module Id " + MODULEID;
                            return;
                        }


                        //if (DB.PRIVILAGE_MENU.Where(p => p.PRIVILEGE_ID == obj_Privil.PRIVILEGE_ID && p.LOCATION_ID == LOCATION_ID && p.TenentID == TenentID).Count() == 0)
                        //{

                        //}
                        //insert data in PRIVILAGE_MENU table

                    }
                }
                //  ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            //    scope.Complete(); //  To commit.

            //}
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            int MID = Convert.ToInt32(drpModulName.SelectedValue);
            if (DB.MODULE_MAP.Where(P => P.ACTIVE_FLAG == "Y" && P.MODULE_ID == MID).Count() > 0)
            {
                var  obj = DB.MODULE_MAP.Where(P => P.ACTIVE_FLAG == "Y" && P.MODULE_ID == MID).ToList();
                for (int i = 0; i < obj.Count();i++ )
                {
                    int TID=obj[i].TenentID;
                    int UID = obj[i].UserID;
                    int LID = obj[i].LOCATION_ID;
                    //GlobleClass.DeleteTempUser(TID, UID, LID, MID);
                    //GlobleClass.getMenuGloble(TID, UID, LID, MID);
                }
                   

                List<Database.tempUser> List = DB.tempUsers.Where(p => p.ACTIVEMENU == true&&p.MODULE_ID ==MID  ).ToList();
                ExportToExcel<Database.tempUser>(List, "tempUser");
                pnlSuccessMsg.Visible = true;
                lblMsg.Text = "Successfully Exported";
            }
            else
            {
                pnlSuccessMsg.Visible = true;
                lblMsg.Text = "Invalid Module , please assign Privilage to Module ";
                return;
            }

        }
        public void ExportToExcel<T>(List<T> List, string FileName)
        {
            if (List.Count > 0)
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", FileName + ".xls"));
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GridView gvdetails = new GridView();
                gvdetails.DataSource = List;
                gvdetails.AllowPaging = false;
                gvdetails.DataBind();
                gvdetails.HeaderRow.Style.Add("font-weight", "bold");
                gvdetails.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }

        }


    }
}