using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Transactions;
using System.Net.Mail;
using System.Web.Services;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Configuration;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Configuration;

namespace Web.Sales
{
    public partial class POSIndex : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();

        CallEntities DB = new CallEntities();
        List<ICTR_HD> ListOFHd = new List<ICTR_HD>();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, CID, userID1, userTypeid = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        string MNAME = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            ShopbindFull();
            SessionLoad();
            if (DB.MYCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0)
            {
                MNAME = DB.MYCOMPANYSETUPs.Single(p => p.TenentID == TID).COMPNAME1;

                //  Label6.Text = MNAME;
                //   Label7.Text = MNAME;
                //  Label8.Text = MNAME;
            }
            if (!IsPostBack)
            {




                // TODAYDate.Text = DateTime.Now.ToString("dd/MMM/yy");
                var Date = DateTime.Now.ToString("yyyy-MM-dd");
                int ToDay = Convert.ToInt32(DateTime.Now.Day);
                int ToMonth = Convert.ToInt32(DateTime.Now.Month);
                int ToYear = Convert.ToInt32(DateTime.Now.Year);

                //ShopbindFull();
                QuickLink();
                QuickLinkDrop();
                HelpDesk();

            }
        }
        public void HelpDesk()
        {
        }
        public string GetUser(int UID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.USER_MST.Where(p => p.TenentID == TID && p.USER_ID == UID).Count() > 0)
            {
                string UName = DB.USER_MST.Single(p => p.TenentID == TID && p.USER_ID == UID).LOGIN_ID;
                return UName;
            }
            else
            {
                return "Not Found";
            }

        }
        public void SessionLoad()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            EMPID = Convert.ToInt32(((USER_MST)Session["USER"]).EmpID);
            LangID = Session["LANGUAGE"].ToString();
        }



        public int GEt_REFID(string REFTYPE, string REFSUBTYPE, string SHORTNAME)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == REFTYPE && p.REFSUBTYPE == REFSUBTYPE && p.SHORTNAME == SHORTNAME).Count() > 0)
            {
                int REFID = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == REFTYPE && p.REFSUBTYPE == REFSUBTYPE && p.SHORTNAME == SHORTNAME).FirstOrDefault().REFID;
                return REFID;
            }
            else
            {
                return 0;
            }
        }

        protected void ListViewLogdata_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }

        public string getproductcode(int PID)
        {
            if (DB.TBLPRODUCTs.Where(p => p.MYPRODID == PID && p.TenentID == TID).Count() > 0)
                return DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID).ProdName1;
            else
                return "";
        }

        public string getuom(int UID)
        {
            if (DB.Tbl_Multi_Color_Size_Mst.Where(p => p.RecTypeID == UID && p.TenentID == TID).Count() > 0)
                return DB.Tbl_Multi_Color_Size_Mst.Single(p => p.RecTypeID == UID && p.TenentID == TID).RecValue;
            else
                return DB.ICUOMs.Single(p => p.UOM == UID && p.TenentID == TID).UOMNAME1;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string id1 = txtsearch.Text.ToString();
            List<Database.CRMMainActivity> list1 = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.Patient_Name.ToUpper().Contains(id1.ToUpper())).ToList();
            listMinimumAndMax.DataSource = list1;
            listMinimumAndMax.DataBind();
        }

        public string getName(int NID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == NID).Count() > 0)
            {
                string CustVendID = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == NID).COMPNAME1;
                return CustVendID;
            }
            else
            {
                return "Not Found";
            }

        }


        protected void ListView3_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label Label12 = (Label)e.Item.FindControl("Label12");
            Label Label15 = (Label)e.Item.FindControl("Label15");
            Label Label11 = (Label)e.Item.FindControl("Label15");
            string Type = Label12.Text.ToString();
            if (Type == "Cash")
            {
                Label15.Text = " (IN)";
                Label11.Attributes["class"] = "label label-success label-sm";
            }
            else
            {
                Label15.Text = " (OUT)";
                Label11.Attributes["class"] = "label label-danger label-sm";
            }
        }



        protected void btnSearchCus_Click(object sender, EventArgs e)
        {
            string id1 = txtCustomer.Text.Trim().ToString();

            List<Database.CRMMainActivity> list1 = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.Contact.ToUpper().Contains(id1.ToUpper())).ToList();
            listCustomer.DataSource = list1;
            listCustomer.DataBind();
        }



        protected void btnshopid_Click(object sender, EventArgs e)
        {
            //Shopbind();
        }

        public void QuickLink()
        {
            if (DB.tbl_QuickLink.Where(p => p.TenentID == TID && p.UserID == UID).Count() < 8)
            {
                //check the Link for user
                int LID = DB.tbl_QuickLink.Where(p => p.TenentID == TID && p.UserID == UID).Count() > 0 ? Convert.ToInt32(DB.tbl_QuickLink.Where(p => p.TenentID == TID && p.UserID == UID).Max(p => p.ID)) : 0;
                int FID = 8 - LID;
                for (int i = 0; i < FID; i++)
                {
                    int id = DB.tbl_QuickLink.Where(p => p.TenentID == TID && p.UserID == UID).Count() > 0 ? Convert.ToInt32(DB.tbl_QuickLink.Where(p => p.TenentID == TID && p.UserID == UID).Max(p => p.ID) + 1) : 1;
                    Database.tbl_QuickLink obj = new Database.tbl_QuickLink();
                    obj.ID = id;
                    obj.TenentID = TID;
                    obj.UserID = UID;
                    obj.MenuID = 0;
                    obj.MasterID = 0;
                    obj.ModuleID = 39;
                    obj.Menu_Location = "Left Menu";
                    obj.MenuName = "No Link";
                    obj.LinkURL = "/Sales/POSIndex.aspx";
                    obj.Colour = getcolor(id);
                    DB.tbl_QuickLink.AddObject(obj);
                    DB.SaveChanges();
                }
            }
            ListQuickLink.DataSource = DB.tbl_QuickLink.Where(p => p.TenentID == TID && p.UserID == UID).Take(8);
            ListQuickLink.DataBind();
        }
        public string getcolor(int id)
        {
            if (id == 1)
                return "tile bg-red-sunglo";
            else if (id == 2)
                return "tile bg-blue";
            else if (id == 3)
                return "tile bg-blue-madison";
            else if (id == 4)
                return "tile bg-purple-studio";
            else if (id == 5)
                return "tile bg-green-meadow";
            else if (id == 6)
                return "tile bg-red-intense";
            else if (id == 7)
                return "tile bg-blue-hoki";
            else if (id == 8)
                return "tile bg-yellow-gold";
            else
                return "tile bg-red-sunglo";
        }
        public void QuickLinkDrop()
        {
            List<Database.tempUser1> Temp = DB.tempUser1.Where(p => p.TenentID == TID && p.UserID == UID).ToList();
            List<Database.FUNCTION_MST> Fun = new List<Database.FUNCTION_MST>();
            //List<Database.FUNCTION_MST> Fun1 = new List<Database.FUNCTION_MST>();
            foreach (Database.tempUser1 item in Temp)
            {
                if (DB.FUNCTION_MST.Where(p => p.MENU_ID == item.MENUID && p.MENU_LOCATION == "Left Menu" && p.URLREWRITE.Contains("UnderCuntruction")).Count() > 0)
                {

                }
                else
                {
                    if (DB.FUNCTION_MST.Where(p => p.MENU_ID == item.MENUID && p.MENU_LOCATION == "Left Menu").Count() > 0)
                    {
                        Database.FUNCTION_MST obj = DB.FUNCTION_MST.Single(p => p.MENU_ID == item.MENUID && p.MENU_LOCATION == "Left Menu");
                        Fun.Add(obj);
                    }
                }
            }
            //Fun1 = Fun;
            //foreach (Database.FUNCTION_MST Fitem in Fun1)
            //{
            //    if (Fun1.Where(p => p.MENU_ID == Fitem.MENU_ID && (p.URLREWRITE.Contains("UnderCuntruction"))).Count() > 0)
            //    {
            //        Database.FUNCTION_MST obj = Fun1.Single(p => p.MENU_ID == Fitem.MENU_ID && (p.URLREWRITE.Contains("UnderCuntruction")));
            //        Fun.Remove(obj);
            //    }
            //}
            drpLink.DataSource = Fun;
            drpLink.DataTextField = "MENU_NAME1";
            drpLink.DataValueField = "MENU_ID";
            drpLink.DataBind();

            DrpNolink.DataSource = DB.tbl_QuickLink.Where(p => p.TenentID == TID && p.UserID == UID).OrderBy(p => p.ID).Take(8);
            DrpNolink.DataTextField = "ID";
            DrpNolink.DataValueField = "ID";
            DrpNolink.DataBind();
        }

        protected void btnSaveCust_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(DrpNolink.SelectedValue);
            int menuid = Convert.ToInt32(drpLink.SelectedValue);

            List<Database.tbl_QuickLink> Link = DB.tbl_QuickLink.Where(p => p.TenentID == TID && p.UserID == UID).ToList();
            if (Link.Where(p => p.TenentID == TID && p.UserID == UID && p.ID == ID && p.MenuID == menuid).Count() == 0)
            {
                Database.FUNCTION_MST Fobj = DB.FUNCTION_MST.Single(p => p.MENU_ID == menuid);
                Database.tbl_QuickLink Linkobj = DB.tbl_QuickLink.Single(p => p.TenentID == TID && p.UserID == UID && p.ID == ID);

                Linkobj.MenuID = Fobj.MENU_ID;
                Linkobj.MasterID = Fobj.MASTER_ID;
                Linkobj.ModuleID = Fobj.MODULE_ID;
                Linkobj.MenuName = Fobj.MENU_NAME1;
                Linkobj.LinkURL = Fobj.URLREWRITE;

                DB.SaveChanges();
            }
            QuickLink();
        }






        public string GetLOCDEPT(int Dept, int LOCT)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string DETPLOCT = "";
            if (DB.DeptITSupers.Where(p => p.TenentID == TID && p.DeptID == Dept).Count() > 0)
            {
                DETPLOCT += DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == Dept).DeptName;
            }
            else
            {
                DETPLOCT += "";
            }
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == LOCT && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation").Count() > 0)
            {
                DETPLOCT += " / " + DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == LOCT && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation").REFNAME1;
            }
            else
            {
                DETPLOCT += " / ";
            }
            return DETPLOCT;
        }

        public string Performer(int per)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.tbl_Employee.Where(p => p.TenentID == TID && p.employeeID == per).Count() == 1)
            {
                return DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == per).firstname;
            }
            else
            {
                return "Not Found";
            }
        }


        protected void Listview4_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label StatusColor = (Label)e.Item.FindControl("lblapp5");
            string STColor = StatusColor.Text.ToString();

            if (STColor == "Pending")
                StatusColor.Attributes["style"] = "background-color:#1bbc9b;";
            else if (STColor == "Closed")
                StatusColor.Attributes["style"] = "background-color:#F3565D;";
            else if (STColor == "Completed")
                StatusColor.Attributes["style"] = "background-color:#9b59b6;";
            else if (STColor == "Progress")
                StatusColor.Attributes["style"] = "background-color:#F8CB00;";
            else if (STColor == "Delivered")
                StatusColor.Attributes["style"] = "background-color:#89C4F4;";
        }

        protected void btnsearch1_Click(object sender, EventArgs e)
        {
            string id1 = txtserchmrnno.Text.ToString();
            List<Database.CRMMainActivity> list1 = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.MRN.ToUpper().Contains(id1.ToUpper())).ToList();
            listMinimumAndMaxand.DataSource = list1;
            listMinimumAndMaxand.DataBind();
        }

        protected void btnsearch2_Click(object sender, EventArgs e)
        {
            string id1 = txtxcomplaint.Text.ToString();
            List<Database.CRMMainActivity> list1 = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ComplaintNumber.ToUpper().Contains(id1.ToUpper())).ToList();
            listmaxcomplaintno.DataSource = list1;
            listmaxcomplaintno.DataBind();
        }

        protected void btnseach3_Click(object sender, EventArgs e)
        {
            string id1 = txtcommentno.Text.ToString();
            List<Database.CRMMainActivity> list1 = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.Description.ToUpper().Contains(id1.ToUpper())).ToList();
            listmaximumcomment.DataSource = list1;
            listmaximumcomment.DataBind();
        }

        protected void listmaximumcomment_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "lnkpn")
            {

                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkmrn")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkcontack")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkcom")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkremark")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
        }

        protected void listMinimumAndMaxand_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "lnkpn")
            {

                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkmrn")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkcontack")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkcom")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkremark")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
        }

    /*public void ShopbindFull()
    {
        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        int ToDay = Convert.ToInt32(DateTime.Now.Day);
        int ToMonth = Convert.ToInt32(DateTime.Now.Month);
        int ToYear = Convert.ToInt32(DateTime.Now.Year);
        string YYYY = ToYear.ToString();
        string MMM = "";
        if (ToMonth > 9)
        {
            MMM = YYYY + "-" + ToMonth.ToString();
        }
        else
        {
            MMM = YYYY + "-0" + ToMonth.ToString();
        }
        var Date = DateTime.Now.ToString("yyyy-MM-dd");
        string sqlTodaysale = "select count(*) as TodayComp from CRMMainActivities where TenentID=" + TID + " and MyStatus not in('Completed', 'Closed') and UploadDate='" + Date + "'";
        SqlCommand CMD1 = new SqlCommand(sqlTodaysale, con);
        SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
        DataSet ds = new DataSet();
        ADB.Fill(ds);
        DataTable dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            lblbox1KD.Text = dt.Rows[0].ItemArray[0].ToString();
        }
        //List<Database.Win_sales_payment> itemList = DB.Win_sales_payment.Where(p => p.TenentID == TID && p.return_id == 0 && (p.sales_time.Contains(Date))).ToList();
        //decimal TOT1 = Convert.ToDecimal(itemList.Sum(p => p.payment_amount));
        //lblbox1KD.Text = TOT1.ToString();

        //Sale This Month
        DateTime startDate = DateTime.Now.AddMonths(-1);
        DateTime endDate = DateTime.Now;
        string START = startDate.ToString("yyyy-MM-dd");
        string End = endDate.ToString("yyyy-MM-dd");
        // select count(*) as  MonthComp from [CRMMainActivities] where [UploadDate] between month(getdate()) -1 and Month(getdate())

        string sqlMonthsale = "select count(*) as TodayComp from CRMMainActivities where TenentID=" + TID + " and MyStatus = 'Closed' and UploadDate='" + Date + "'";
        SqlCommand CMD2 = new SqlCommand(sqlMonthsale, con);
        SqlDataAdapter ADB2 = new SqlDataAdapter(CMD2);
        DataSet ds1 = new DataSet();
        ADB2.Fill(ds1);
        DataTable dt1 = ds1.Tables[0];
        if (dt1.Rows.Count > 0)
        {
            decimal MonthSe = 0;
            if (dt1.Rows[0].ItemArray[0].ToString() == "")
                MonthSe = 0;
            else
                MonthSe = Convert.ToDecimal(dt1.Rows[0].ItemArray[0].ToString());
            MonthSe = Math.Round(MonthSe, 3);
            lblbox2KD.Text = MonthSe.ToString();
        }

        //DateTime endDate123 = DateTime.Now;
        //string End123 = endDate123.ToString("yyyy-MM-dd");
        //DateTime startDate123 = endDate123;//.AddDays(-7);
        //string START123 = startDate123.ToString("yyyy-MM-dd");
        //var s = startDate123.DayOfWeek;



        // Complain new week
        DateTime WeekEndDate = DateTime.Now;
        string End1234 = WeekEndDate.ToString("yyyy-MM-dd");
        String swe1 = "select dateadd(d, 7-DATEPART(dw, GETDATE()), GETDATE()) as LastDateOfWeek";
        SqlCommand CMD4 = new SqlCommand(swe1, con);
        SqlDataAdapter ADB4 = new SqlDataAdapter(CMD4);
        DataSet ds3 = new DataSet();
        ADB4.Fill(ds3);
        DataTable dt3 = ds3.Tables[0];
        if (dt3.Rows.Count > 0)
        {
            if (dt3.Rows[0].ItemArray[0].ToString() == null || dt3.Rows[0].ItemArray[0].ToString() == "")
            { }
            else
            {
                WeekEndDate = Convert.ToDateTime(dt3.Rows[0].ItemArray[0].ToString());

            }
        }

        DateTime WeekStartDate = DateTime.Now;
        string End123 = WeekStartDate.ToString("yyyy-MM-dd");

        String swe2 = "select dateadd(d, - DATEPART(dw, GETDATE()), GETDATE()) as FirstDateOfWeek";
        SqlCommand CMD46 = new SqlCommand(swe2, con);
        SqlDataAdapter ADB46 = new SqlDataAdapter(CMD46);
        DataSet ds36 = new DataSet();
        ADB46.Fill(ds36);
        DataTable dt36 = ds36.Tables[0];
        if (dt36.Rows.Count > 0)
        {
            if (dt36.Rows[0].ItemArray[0].ToString() == null || dt36.Rows[0].ItemArray[0].ToString() == "")
            { }
            else
            {
                WeekStartDate = Convert.ToDateTime(dt36.Rows[0].ItemArray[0].ToString());

            }
        }

        string sqlYearsale = "select count(*) as  Currentweek from CRMMainActivities where TenentID=" + TID + " and MyStatus not in('Completed', 'Closed') and UploadDate BETWEEN '" + End123 + "' AND    '" + End1234 + "' ";
        SqlCommand CMD3 = new SqlCommand(sqlYearsale, con);
        SqlDataAdapter ADB3 = new SqlDataAdapter(CMD3);
        DataSet ds2 = new DataSet();
        ADB3.Fill(ds2);
        DataTable dt2 = ds2.Tables[0];
        if (dt2.Rows.Count > 0)
        {
            decimal Year = 0;
            if (dt2.Rows[0].ItemArray[0].ToString() == "")
                Year = 0;
            else
                Year = Convert.ToDecimal(dt2.Rows[0].ItemArray[0].ToString());
            Year = Math.Round(Year, 3);
            lblbox3KD.Text = Year.ToString();
        }

        //This week Close complain


        string sqlYearRetsale = "select count(*) as  Currentweek from CRMMainActivities where TenentID=" + TID + " and MyStatus = 'Closed' and UploadDate BETWEEN '" + End123 + "' AND    '" + End1234 + "'  ";
        SqlCommand CMD45 = new SqlCommand(sqlYearRetsale, con);
        SqlDataAdapter ADB45 = new SqlDataAdapter(CMD45);
        DataSet ds35 = new DataSet();
        ADB45.Fill(ds35);
        DataTable dt35 = ds35.Tables[0];
        if (dt35.Rows.Count > 0)
        {
            if (dt35.Rows[0].ItemArray[0].ToString() == null || dt35.Rows[0].ItemArray[0].ToString() == "")
            { }
            else
            {
                decimal Yearret = Convert.ToDecimal(dt35.Rows[0].ItemArray[0].ToString());
                Yearret = Math.Round(Yearret, 3);
                lblbox4KD.Text = Yearret.ToString();
            }
        }

        //This month new complain

        string TodayPur = " select count(*) as  CurrentMonth from CRMMainActivities where TenentID= " + TID + " and MyStatus not in('Completed', 'Closed') and Month(UploadDate)=Month(getdate()) ";
        SqlCommand CMD5 = new SqlCommand(TodayPur, con);
        SqlDataAdapter ADB5 = new SqlDataAdapter(CMD5);
        DataSet ds4 = new DataSet();
        ADB5.Fill(ds4);
        DataTable dt4 = ds4.Tables[0];
        if (dt4.Rows.Count > 0)
        {
            lblbox5KD.Text = (dt4.Rows[0].ItemArray[0].ToString());
        }

        string STARTp = startDate.ToString("yyyy-MM-dd");
        string Endp = endDate.ToString("yyyy-MM-dd");

        //This month close complain

        string MonthPur = " select count(*) as  CurrentMonth from CRMMainActivities where TenentID= " + TID + " and MyStatus = 'Closed' and Month(UploadDate)=Month(getdate())";
        SqlCommand CMD6 = new SqlCommand(MonthPur, con);
        SqlDataAdapter ADB6 = new SqlDataAdapter(CMD6);
        DataSet ds5 = new DataSet();
        ADB6.Fill(ds5);
        DataTable dt5 = ds5.Tables[0];
        if (dt5.Rows.Count > 0)
        {
            if (dt5.Rows[0].ItemArray[0].ToString() == null || dt5.Rows[0].ItemArray[0].ToString() == "")
            { }
            else
            {
                decimal Monthpur = Convert.ToDecimal(dt5.Rows[0].ItemArray[0].ToString());
                Monthpur = Math.Round(Monthpur, 3);
                lblbox6KD.Text = Monthpur.ToString();
            }
        }

        // This Year New complain

        DateTime EDT = DateTime.Now;
        string ENDJ = EDT.ToString("MM/dd/yyyy");
        DateTime SDT = new DateTime(EDT.Year, 1, 1);
        string STARTJ = SDT.ToString("MM/dd/yyyy");

        string yearPur = "select count(*) as CurrentYear from CRMMainActivities where TenentID=" + TID + " and MyStatus not in('Completed', 'Closed') and Year(UploadDate)=YEar(getdate())";
        SqlCommand CMD7 = new SqlCommand(yearPur, con);
        SqlDataAdapter ADB7 = new SqlDataAdapter(CMD7);
        DataSet ds6 = new DataSet();
        ADB7.Fill(ds6);
        DataTable dt7 = ds6.Tables[0];
        if (dt7.Rows.Count > 0)
        {
            if (dt7.Rows[0].ItemArray[0].ToString() == null || dt7.Rows[0].ItemArray[0].ToString() == "")
            { }
            else
            {
                decimal Yearp = Convert.ToDecimal(dt7.Rows[0].ItemArray[0].ToString());
                Yearp = Math.Round(Yearp, 3);
                lblbox7KD.Text = Yearp.ToString();
            }
        }

        //This year Close complain

        string yearPur2 = "select count(*) as CurrentYear from CRMMainActivities where TenentID=" + TID + " and MyStatus = 'Closed' and Year(UploadDate)=YEar(getdate())";
        SqlCommand CMD8 = new SqlCommand(yearPur2, con);
        SqlDataAdapter ADB8 = new SqlDataAdapter(CMD8);
        DataSet ds8 = new DataSet();
        ADB8.Fill(ds8);
        DataTable dt8 = ds8.Tables[0];
        if (dt8.Rows.Count > 0)
        {
            if (dt8.Rows[0].ItemArray[0].ToString() == null || dt8.Rows[0].ItemArray[0].ToString() == "")
            { }
            else
            {
                decimal Yearp = Convert.ToDecimal(dt8.Rows[0].ItemArray[0].ToString());
                Yearp = Math.Round(Yearp, 3);
                lblbox8KD.Text = Yearp.ToString();
            }
        }


        List<Database.Win_tbl_purchase_history> itemListYearPurchaseReturn = DB.Win_tbl_purchase_history.Where(p => p.TenentID == TID && (p.purchase_date.Contains(YYYY))).ToList();
        decimal TOT8 = Convert.ToDecimal(itemListYearPurchaseReturn.Sum(p => p.cost_price));
        // lblbox8KD.Text = TOT8.ToString();
        string Purreturnyear = DateTime.Now.ToString("yyyy");
        //  ThisYearPurchaseReturn.Text = Purreturnyear;

    }*/

      public void ShopbindFull()
      {
        int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

        string sql = @"
          DECLARE @TID INT = @TenantParam;
          DECLARE @Today DATE = CAST(GETDATE() AS DATE);
          DECLARE @WeekStart DATE = DATEADD(DAY, 1 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE));
          DECLARE @WeekEnd DATE = DATEADD(DAY, 7 - DATEPART(WEEKDAY, GETDATE()), CAST(GETDATE() AS DATE));

          SELECT 
              -- 1 Today's New
              (SELECT COUNT(*) FROM CRMActivities 
               WHERE TenentID = @TID AND MyStatus NOT IN ('Completed', 'Closed') AND UploadDate = @Today) AS TodayComp,

              -- 2 Today's Closed
              (SELECT COUNT(*) FROM CRMActivities 
               WHERE TenentID = @TID AND MyStatus = 'Closed' AND UploadDate = @Today) AS TodayClosed,

              -- 3 Current Week New
              (SELECT COUNT(*) FROM CRMActivities 
               WHERE TenentID = @TID AND MyStatus NOT IN ('Completed', 'Closed') 
               AND UploadDate BETWEEN @WeekStart AND @WeekEnd) AS CurrentWeekNew,

              -- 4 Current Week Closed
              (SELECT COUNT(*) FROM CRMActivities 
               WHERE TenentID = @TID AND MyStatus = 'Closed' 
               AND UploadDate BETWEEN @WeekStart AND @WeekEnd) AS CurrentWeekClosed,

              -- 5 Current Month New
              (SELECT COUNT(*) FROM CRMActivities 
               WHERE TenentID = @TID AND MyStatus NOT IN ('Completed', 'Closed') 
               AND MONTH(UploadDate) = MONTH(GETDATE()) AND YEAR(UploadDate) = YEAR(GETDATE())) AS CurrentMonthNew,

              -- 6 Current Month Closed
              (SELECT COUNT(*) FROM CRMActivities 
               WHERE TenentID = @TID AND MyStatus = 'Closed' 
               AND MONTH(UploadDate) = MONTH(GETDATE()) AND YEAR(UploadDate) = YEAR(GETDATE())) AS CurrentMonthClosed,

              -- 7 Current Year New
              (SELECT COUNT(*) FROM CRMActivities 
               WHERE TenentID = @TID AND MyStatus NOT IN ('Completed', 'Closed') 
               AND YEAR(UploadDate) = YEAR(GETDATE())) AS CurrentYearNew,

              -- 8 Current Year Closed
              (SELECT COUNT(*) FROM CRMActivities 
               WHERE TenentID = @TID AND MyStatus = 'Closed' 
               AND YEAR(UploadDate) = YEAR(GETDATE())) AS CurrentYearClosed;
      ";

        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
          cmd.Parameters.AddWithValue("@TenantParam", TID);
          SqlDataAdapter da = new SqlDataAdapter(cmd);
          DataTable dt = new DataTable();
          da.Fill(dt);

          if (dt.Rows.Count > 0)
          {
            lblbox1KD.Text = dt.Rows[0]["TodayComp"].ToString();
            lblbox2KD.Text = dt.Rows[0]["TodayClosed"].ToString();
            lblbox3KD.Text = dt.Rows[0]["CurrentWeekNew"].ToString();
            lblbox4KD.Text = dt.Rows[0]["CurrentWeekClosed"].ToString();
            lblbox5KD.Text = dt.Rows[0]["CurrentMonthNew"].ToString();
            lblbox6KD.Text = dt.Rows[0]["CurrentMonthClosed"].ToString();
            lblbox7KD.Text = dt.Rows[0]["CurrentYearNew"].ToString();
            lblbox8KD.Text = dt.Rows[0]["CurrentYearClosed"].ToString();
          }
        }
      }


    protected void listMinimumAndMax_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "lnkpn")
            {

                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkmrn")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkcontack")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkcom")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkremark")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
        }

        protected void listCustomer_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "lnkpn")
            {

                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkmrn")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkcontack")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkcom")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkremark")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
        }

        protected void listmaxcomplaintno_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "lnkpn")
            {

                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkmrn")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkcontack")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkcom")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkremark")
            {
                Label lblpt = (Label)e.Item.FindControl("lblpt");
                Label lblmrn = (Label)e.Item.FindControl("lblmrn");
                Label lblcontact = (Label)e.Item.FindControl("lblcontact");
                Label lblcn = (Label)e.Item.FindControl("lblcn");
                Label lblremark = (Label)e.Item.FindControl("lblremark");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
        }



















    }
    public class custamt
    {
        public string C_id { get; set; }
        public string CustomerName { get; set; }
        public decimal amt { get; set; }
    }
}
