using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using Database;
using System.Net;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Transactions;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Web.Services;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.UI.WebControls;
using System.Web.Configuration;



namespace Web.CRM
{
    public partial class Event_Management_MST : System.Web.UI.Page
    {
        Database.CallEntities DB = new Database.CallEntities();
        int TID, LID, UID, EMPID, userID1, userTypeid = 0;
        string LangID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                var Date = DateTime.Now.Date;
                if (DB.View_EventMainDetail.Where(p => p.TenantID == TID && p.ToDate >= Date).Count() > 0)
                {
                    if (Request.QueryString["event"] != null)
                    {
                        int EID = Convert.ToInt32(Request.QueryString["event"]);
                        if (DB.View_EventMainDetail.Where(p => p.TenantID == TID && p.ToDate >= Date && p.EventID == EID).Count() > 0)
                        {
                            Database.View_EventMainDetail obj = DB.View_EventMainDetail.Where(p => p.TenantID == TID && p.ToDate >= Date && p.EventID == EID).OrderBy(p => p.ToDate).First();
                            lblhead.Text = obj.Title2Print + " - " + obj.Description1;
                            uc1.Attributes["class"] = "input-group-btn";
                            uc2.Attributes["class"] = "page-head";
                        }
                        else
                        {
                            lblhead.Text = "Event Not Found" + " - " + "Sub Event Not Found";
                            uc1.Attributes["class"] = "input-group-btn hide";
                            uc2.Attributes["class"] = "page-head hide";
                        }
                    }
                    else
                    {
                        lblhead.Text = "Event Not Found" + " - " + "Sub Event Not Found";
                        uc1.Attributes["class"] = "input-group-btn hide";
                        uc2.Attributes["class"] = "page-head hide";
                    }
                }
                else
                {
                    lblhead.Text = "Event Not Found" + " - " + "Sub Event Not Found";
                    uc1.Attributes["class"] = "input-group-btn hide";
                    uc2.Attributes["class"] = "page-head hide";
                }
                //lblhead.Text = DB.Tbl_Event_Main.Where(p => p.TenantID == TID && p.ToDate >= DateTime.Now).Count() > 0 ? DB.Tbl_Event_Main.Where(p => p.TenantID == TID && p.ToDate >= DateTime.Now).OrderBy(p=>p.ToDate).First().Title2Print : "No Event Found";
            }
        }
        public void SessionLoad()
        {
            if (Session["USER"] != null)
            {
                TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
                UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                EMPID = Convert.ToInt32(((USER_MST)Session["USER"]).EmpID);
                LangID = Session["LANGUAGE"].ToString();
                userID1 = ((USER_MST)Session["USER"]).USER_ID;
                userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);
            }
            else
            {
                TID = Convert.ToInt32(2);
                LID = Convert.ToInt32(1);
                UID = Convert.ToInt32(11405);
                EMPID = Convert.ToInt32(0);
                Session["LANGUAGE"] = "en-US";
                LangID = Session["LANGUAGE"].ToString();
                userID1 = 0;
                userTypeid = 0;
            }
        }
        //[WebMethod]
        //[System.Web.Script.Services.ScriptMethod]
        //public static string[] GetCounrty(string prefixText, int count)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)HttpContext.Current.Session["USER"]).TenentID);
        //    string conStr;
        //    conStr = WebConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString;
        //    string sqlQuery = "SELECT [COMPNAME1],[COMPID] FROM [TBLCOMPANYSETUP] WHERE TenantID='" + TID + "' and Active='Y' and BUYER = 'true' and COUNTRYID ='126' and (COMPNAME1 like @COMPNAME  + '%' or COMPNAME2 like @COMPNAME  + '%' or COMPNAME3 like @COMPNAME  + '%' or MOBPHONE like @COMPNAME  + '%')";
        //    //string sqlQuery = "SELECT [COMPNAME1]+' - '+MOBPHONE,[COMPID] FROM [TBLCOMPANYSETUP] WHERE TenantID='" + TID + "' and BUYER = 'true' and (COMPNAME1 like @COMPNAME  + '%' or COMPNAME2 like @COMPNAME  + '%' or COMPNAME3 like @COMPNAME  + '%' or MOBPHONE like @COMPNAME  + '%')";
        //    SqlConnection conn = new SqlConnection(conStr);
        //    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
        //    cmd.Parameters.AddWithValue("@COMPNAME", prefixText);
        //    conn.Open();
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    List<string> custList = new List<string>();
        //    string custItem = string.Empty;
        //    while (dr.Read())
        //    {
        //        custItem = AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString());
        //        custList.Add(custItem);
        //    }
        //    conn.Close();
        //    dr.Close();
        //    return custList.ToArray();
        //}
        protected void btnSerrch_Click(object sender, EventArgs e)
        {
            string SearchID = txtSearch.Text.Trim().ToString();
            if (SearchID == "")
            {
                Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Please Search In SearchBox", "warning!", Classes.Toastr.ToastPosition.TopCenter);
            }
            else
            {
                var ListAttendee = DB.Tbl_Event_Register.Where(p => (p.Attendee.ToUpper().Contains(SearchID.ToUpper()) || p.CompanyName.ToUpper().Contains(SearchID.ToUpper()) || p.PositionName.ToUpper().Contains(SearchID.ToUpper()) || p.Email.ToUpper().Contains(SearchID.ToUpper()) || p.Address.ToUpper().Contains(SearchID.ToUpper()) || p.MobileNo.Contains(SearchID)) && p.TenantID == TID).ToList();
                //var ListContact = DB.TBLCONTACTs.Where(p => (p.PersName1.ToUpper().Contains(SearchID.ToUpper()) || p.PersName2.ToUpper().Contains(SearchID.ToUpper()) || p.PersName3.ToUpper().Contains(SearchID.ToUpper()) || p.EMAIL1.ToUpper().Contains(SearchID.ToUpper()) || p.MOBPHONE.Contains(SearchID) || p.BUSPHONE1.Contains(SearchID) || p.BARCODE.Contains(SearchID) || p.Instuctor_Username.ToUpper().Contains(SearchID.ToUpper())) && p.TenentID == TID && p.Active == "Y").ToList();
                ListView1.DataSource = ListAttendee;//ListContact;
                ListView1.DataBind();
                if (ListAttendee.Count == 0)
                {
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Your Name Not Found Press a Registration & Joining...", "warning!", Classes.Toastr.ToastPosition.TopCenter);
                }
                //}
                //else
                //{
                //    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Please Search Perticuler One Company...", "warning!", Classes.Toastr.ToastPosition.TopCenter);
                //}
            }
            //for Company
            //string SearchID = txtSearch.Text.Trim().ToString();
            //if (SearchID == "")
            //{
            //    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Please Search", "warning!", Classes.Toastr.ToastPosition.TopCenter);
            //}
            //else
            //{
            //    var List = DB.TBLCOMPANYSETUPs.Where(p => (p.COMPNAME1.ToUpper().Contains(SearchID.ToUpper()) || p.MOBPHONE.Contains(SearchID) || p.BUSPHONE1.Contains(SearchID) || p.ADDR1.ToUpper().Contains(SearchID.ToUpper()) || p.EMAIL.ToUpper().Contains(SearchID.ToUpper()) || p.BARCODE.Contains(SearchID)) && p.TenantID == TID && p.BUYER == true && p.Active == "Y").ToList();
            //    ListView1.DataSource = List;
            //    ListView1.DataBind();
            //    if (List.Count == 0)
            //    {
            //        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Your Name Not Found Press a Registration & Joining...", "warning!", Classes.Toastr.ToastPosition.TopCenter);
            //    }  
            //}  
        }
        public string CompanyID(int Comid)
        {
            if (Comid == 0 || Comid == null)
            {
                return "Not Found";
            }
            else
            {
                if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == Comid).Count() > 0)
                {
                    string commid = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == Comid).COMPNAME1.ToString();
                    return commid;
                }
                else
                {
                    return "Not Found";
                }
            }

        }
        public string Image(int eventid, int myid, int CID)
        {
            if (DB.Tbl_Event_Register.Where(p => p.TenantID == TID && p.EventID == eventid && p.MyID == myid && p.ContactMyID == CID).Count() > 0)
            {
                string Img = DB.Tbl_Event_Register.Single(p => p.TenantID == TID && p.EventID == eventid && p.MyID == myid && p.ContactMyID == CID).IMG;
                //string Img = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.COMPANYID == IMG && p.ContactMyID == ContactMYID).IMG;
                if (Img == "" || Img == null)
                {
                    return "NO_image.png";
                }
                else
                {
                    return Img;
                }
            }
            else
            {
                return "NO_image.png";
            }
        }
        protected void ListView1_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {
            //for(int i=0; i < ListView1.Items.Count ;i++)
            //{
            //Label lblUOM = (Label)Repeater2.Items[i].FindControl("lblUOM");
            //LinkButton print = (LinkButton)ListView1.Items[i].FindControl("btnPrint");
            ImageButton Card = (ImageButton)e.Item.FindControl("btnCard");
            Label lblCusName = (Label)e.Item.FindControl("lblCusName");
            Label lblCusomer = (Label)e.Item.FindControl("lblCusomer");
            Label lblFamilyName = (Label)e.Item.FindControl("lblFamilyName");
            Label lblBar = (Label)e.Item.FindControl("lblBar");

            System.Web.UI.WebControls.Image Image1 = (System.Web.UI.WebControls.Image)e.Item.FindControl("Image1");
            System.Web.UI.WebControls.Image IMGCusImg = (System.Web.UI.WebControls.Image)e.Item.FindControl("IMGCusImg");
            System.Web.UI.WebControls.Image Imgbar = (System.Web.UI.WebControls.Image)e.Item.FindControl("Imgbar");
            System.Web.UI.WebControls.Image IMGBarCode = (System.Web.UI.WebControls.Image)e.Item.FindControl("IMGBarCode");
            string date = DateTime.Now.ToString("dd/MMM/yyyy");
            Label DateBar = (Label)e.Item.FindControl("lblbardate");

            string Customer = lblCusomer.Text;
            string BarCode = lblBar.Text.ToString();

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                lblFamilyName.Text = Customer.ToString();
                lblCusName.Text = Customer.ToString();
                IMGCusImg.ImageUrl = Image1.ImageUrl;
                IMGBarCode.ImageUrl = Imgbar.ImageUrl + ".png";
                DateBar.Text = date.ToString();

            }
            //}

        }




    }
}