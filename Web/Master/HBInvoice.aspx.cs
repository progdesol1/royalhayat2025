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
using Classes;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Web.Hosting;
using System.Net;
using System.IO;

namespace Web.Master
{
    public partial class HBInvoice : System.Web.UI.Page
    {

        SqlConnection con1;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        CallEntities DB = new CallEntities();
        public static int ChoiceID = 0;
        int Trans = 0;
        int TID, LID, UID, EMPID, CID = 0;
        string LangID = "";
        #endregion

        int MenuID, crupID = 0;
        //bool editSwitch;
        string switch1;

        #region StepError
        public void GetDBError()
        {
            //How many Table Are Use
            /*
            planmealcustinvoice
            planmealcustinvoiceHD

            TBLCOMPANYSETUP
            tblcontact_addon1
            tblcontact_addon1_dtl
            TBLPRODUCT
            REFTABLE
            tblProduct_Plan
            tbl_Employee                  
         
            */

            //How many DropDownList
            //drpCustomer bind With TBLCOMPANYSETUP table
            /*also Relation with 
             tblcontact_addon1_dtl ,tblcontact_addon1, TBLCOMPANYSETUP, planmealcustinvoiceHD */


            //drpPlan bind With tblProduct_Plan table
            /*also Relation with 
             tblProduct_Plan ,planmealsetup, planmealcustinvoiceHD */

            //drpDeliveryTime Bind With REFTABLE Table
            /*also Relation with 
             REFTABLE ,tblcontact_addon1_dtl*/

            //drpExpecteddriver Bind With tbl_Employee

            //drpweekofday Bind With static Design Side Bind Sat to Thu

            //drpDeliveryMeal Bind With REFTABLE Table 
            /*also Relation with 
             REFTABLE ,tblcontact_addon1_dtl, planmealcustinvoice */

            //DrpWeek Bind With Static And planmealcustinvoice  (Add mode Static And Edit mode planmealcustinvoice) value with 1,2,3,etc

            //Listview1 Bind With join With planmealcustinvoices and planmealcustinvoiceHDs
            // Requird Feild CustomerID,MYTRANSID,planid,DeliveryMeal,DeliveryID

            //Listview3 Bind With planmealcustinvoice
            //Requird Feild planid,MealType,MYPRODID,Item_cost,ItemWeight,DayNumber,StartDate,NoOfWeek
            //DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == Plane && p.DeliveryMeal == Meal && p.DeliveryID == DID && p.ACTIVE == true);


            //ListView2 Bind With TBLPRODUCT
            //with Relation planmealsetup

            //Add mode
            //Requird Feild MYPRODID ,txtWeight.text= ItemWeight in planmealsetup ,txtsaleprice.text = msrp in TBLPRODUCT

            // Edit Mode
            //Feild MYPRODID ,txtWeight.text= ItemWeight in planmealcustinvoice ,txtsaleprice.text = Item_price in planmealcustinvoice

            /*  select CustomerID from planmealcustinvoiceHD where CustomerID is null

                select CustomerID from planmealcustinvoiceHD where CustomerID not in (select COMPID from TBLCOMPANYSETUP where ACTIVE='Y' and TenentID=planmealcustinvoiceHD.TenentID) 

                select CustomerID from planmealcustinvoiceHD where CustomerID not in (Select CustomerID from tblcontact_addon1_dtl where ACTIVE=1 and TenentID=planmealcustinvoiceHD.TenentID)

                select CustomerID from planmealcustinvoiceHD where CustomerID not in (Select CustomerID from tblcontact_addon1 where ACTIVE=1 and TenentID=planmealcustinvoiceHD.TenentID)
            */


            /*
             
             
             */

        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlSuccessMsg.Visible = false;
            lblMsg.Text = "";

            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";

            pnlErrorMsg1.Visible = false;
            lblerrorMsg1.Text = "";

            if (Request.QueryString["MID"] != null)
            {
                string Menuid = Request.QueryString["MID"].ToString();
                MenuID = ((AcmMaster)Page.Master).GetMenuID(Menuid);
            }

            SessionLoad();
            if (!IsPostBack)
            {
                delete_garbage();
                SwitchoONOFF("AddOff");
                Readonly();
                ManageLang();
                pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                //FirstData();
                btnAdd.ValidationGroup = "ss";
            }
        }
        #region Step2

        public void SessionLoad()
        {
            string Ref = ((AcmMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
        }

        public void delete_garbage()
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
            List<Database.planmealcustinvoiceHD> ListHD = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID).ToList();
            List<Database.planmealcustinvoiceHD> DeleteHD = new List<planmealcustinvoiceHD>();

            foreach (Database.planmealcustinvoiceHD items in ListHD)
            {
                List<Database.planmealcustinvoice> ListDT = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == items.MYTRANSID).ToList();

                if (ListDT.Count() < 1)
                {
                    DeleteHD.Add(items);
                }
            }

            foreach (Database.planmealcustinvoiceHD itemDelete in DeleteHD)
            {
                Database.planmealcustinvoiceHD objdelete = DB.planmealcustinvoiceHDs.Single(p => p.TenentID == TID && p.MYTRANSID == itemDelete.MYTRANSID);
                //int crupID = Convert.ToInt32(objdelete.CRUP_ID);
                DB.planmealcustinvoiceHDs.DeleteObject(objdelete);
                DB.SaveChanges();
                //string LogHD = " MYTRANSID=" + itemDelete.MYTRANSID;
                //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoiceHD,Delete:" + LogHD, crupID, "planmealcustinvoiceHD", UID.ToString());
            }

            //string Str = "delete from planmealcustinvoice where tenentid=" + TID + " and customerID not in (select customerID from planmealcustinvoicehd where tenentid=" + TID + ")";
            //command2 = new SqlCommand(Str, con);
            //con.Open();
            //command2.ExecuteReader();
            //con.Close();



            //    scope.Complete();
            //}
        }
        public void SwitchoONOFF(string switch1)
        {
            if (switch1 == "AddON")
            {
                btnCopyFullPlan.Enabled = false;
                btnChengeDriver.Enabled = false;
                //btntimeChange.Enabled = false;
                Button3.Enabled = false;
                btnAddmeal.Enabled = false;
                txtExpDate.Enabled = true;
                txtBeingDate.Enabled = true;
                btnDateSet.Enabled = true;
                txtDelTotalDay.Enabled = true;
                btnRecalculate.Enabled = false;
                btnOnHold.Enabled = false;
                btnOnHold.Visible = false;
                btnunhold.Enabled = false;
                btnunhold.Visible = false;

            }

            if (switch1 == "AddOff")
            {
                btnCopyFullPlan.Enabled = false;
                btnChengeDriver.Enabled = false;
                //btntimeChange.Enabled = false;
                Button3.Enabled = false;
                btnAddmeal.Enabled = false;
                txtExpDate.Enabled = false;
                txtBeingDate.Enabled = false;
                btnDateSet.Enabled = false;
                txtDelTotalDay.Enabled = false;
                btnRecalculate.Enabled = false;
                btnOnHold.Enabled = false;
                btnOnHold.Visible = false;
                btnunhold.Enabled = false;
                btnunhold.Visible = false;
            }

            if (switch1 == "editON")
            {
                btnCopyFullPlan.Enabled = false;
                btnChengeDriver.Enabled = false;
                //btntimeChange.Enabled = false;
                Button3.Enabled = false;
                btnRecalculate.Enabled = false;
                btnOnHold.Enabled = false;
                btnOnHold.Visible = false;
                btnunhold.Enabled = false;
                btnunhold.Visible = false;

                btnAddmeal.Enabled = false;
                //drpDeliveryTime.Enabled = false;
                drpDeliveryMeal.Enabled = false;
                txtExpDate.Enabled = false;
                txtBeingDate.Enabled = false;
                txtDelTotalDay.Enabled = false;
                btnDateSet.Enabled = false;
                txtEndDate.Enabled = false;
            }

            if (switch1 == "editOff")
            {
                //bool Flag = checkcopy();
                //if (Flag == true)
                //{
                //    btnCopyFullPlan.Enabled = true;
                //}
                //else
                //{
                //    btnCopyFullPlan.Enabled = false;
                //}
                btnCopyFullPlan.Enabled = true;
                btnChengeDriver.Enabled = true;
                //btntimeChange.Enabled = true;
                Button3.Enabled = true;
                btnRecalculate.Enabled = true;
                //btnAddmeal.Enabled = true;
                txtExpDate.Enabled = false;
                txtDelTotalDay.Enabled = true;
                btnDateSet.Enabled = true;
                txtEndDate.Enabled = true;

                if (ViewState["MYTRANSID"] != null)
                {
                    int MYTRANSID = Convert.ToInt32(ViewState["MYTRANSID"]);
                    List<Database.planmealcustinvoice> Listdt = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate != null).ToList();
                    if (Listdt.Count() > 0)
                    {
                        btnOnHold.Enabled = true;
                        btnOnHold.Visible = true;
                        txtBeingDate.Enabled = false;
                    }
                    else
                    {
                        btnOnHold.Enabled = false;
                        btnOnHold.Visible = false;
                        txtBeingDate.Enabled = true;
                    }
                }
                else
                {
                    btnOnHold.Enabled = false;
                    btnOnHold.Visible = false;
                    txtBeingDate.Enabled = true;
                }


                if (ViewState["MYTRANSID"] != null)
                {
                    int MYTRANSID = Convert.ToInt32(ViewState["MYTRANSID"]);
                    List<Database.planmealcustinvoiceHD> Listhd = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.SubscriptionOnHold == true).ToList();
                    if (Listhd.Count() > 0)
                    {
                        btnunhold.Enabled = true;
                        btnunhold.Visible = true;
                        btnOnHold.Enabled = false;
                        btnOnHold.Visible = false;
                    }
                    else
                    {
                        btnunhold.Enabled = false;
                        btnunhold.Visible = false;
                        btnOnHold.Enabled = true;
                        btnOnHold.Visible = true;
                    }
                }
                else
                {
                    btnunhold.Enabled = false;
                    btnunhold.Visible = false;
                    btnOnHold.Enabled = true;
                    btnOnHold.Visible = true;
                }
            }

        }

        public bool checkcopy()
        {
            bool returnflag = false;

            bool flagcheck1 = false;
            bool flagcheck2 = false;
            bool flagcheck3 = false;
            bool flagcheck4 = false;
            bool flagcheck5 = false;
            bool flagcheck6 = false;

            for (int i = 0; i < ListView2.Items.Count; i++)
            {
                CheckBox RDODay1 = (CheckBox)ListView2.Items[i].FindControl("RDODay1");
                CheckBox RDODay2 = (CheckBox)ListView2.Items[i].FindControl("RDODay2");
                CheckBox RDODay3 = (CheckBox)ListView2.Items[i].FindControl("RDODay3");
                CheckBox RDODay4 = (CheckBox)ListView2.Items[i].FindControl("RDODay4");
                CheckBox RDODay5 = (CheckBox)ListView2.Items[i].FindControl("RDODay5");
                CheckBox RDODay6 = (CheckBox)ListView2.Items[i].FindControl("RDODay6");

                string[] days = tags_1.Text.Split(',');
                foreach (string Val in days)
                {
                    if (Val == "Sat")
                    {
                        if (RDODay1.Checked == true)
                        {
                            flagcheck1 = true;
                        }
                    }
                    if (Val == "Sun")
                    {
                        if (RDODay2.Checked == true)
                        {
                            flagcheck2 = true;
                        }
                    }
                    if (Val == "Mon")
                    {
                        if (RDODay3.Checked == true)
                        {
                            flagcheck3 = true;
                        }
                    }
                    if (Val == "Tue")
                    {
                        if (RDODay4.Checked == true)
                        {
                            flagcheck4 = true;
                        }
                    }
                    if (Val == "Wed")
                    {
                        if (RDODay5.Checked == true)
                        {
                            flagcheck5 = true;
                        }
                    }
                    if (Val == "Thu")
                    {
                        if (RDODay6.Checked == true)
                        {
                            flagcheck6 = true;
                        }
                    }
                }
            }
            string MSG = "";
            string[] day = tags_1.Text.Split(',');
            foreach (string Val1 in day)
            {
                if (Val1 == "Sat")
                {
                    if (flagcheck1 == false)
                    {
                        MSG += " Check Atleast One Product in " + DayOfWeek.Saturday + " <br>";
                    }
                }
                if (Val1 == "Sun")
                {
                    if (flagcheck2 == false)
                    {
                        MSG += " Check Atleast One Product in " + DayOfWeek.Sunday + " <br>";
                    }
                }
                if (Val1 == "Mon")
                {
                    if (flagcheck3 == false)
                    {
                        MSG += " Check Atleast One Product in " + DayOfWeek.Monday + " <br>";
                    }
                }
                if (Val1 == "Tue")
                {
                    if (flagcheck4 == false)
                    {
                        MSG += " Check Atleast One Product in " + DayOfWeek.Tuesday + " <br>";
                    }
                }
                if (Val1 == "Wed")
                {
                    if (flagcheck5 == false)
                    {
                        MSG += " Check Atleast One Product in " + DayOfWeek.Wednesday + " <br>";
                    }
                }
                if (Val1 == "Thu")
                {
                    if (flagcheck6 == false)
                    {
                        MSG += " Check Atleast One Product in " + DayOfWeek.Thursday + " <br>";
                    }
                }
            }

            if (MSG != "")
            {
                returnflag = false;
            }
            else
            {
                returnflag = true;
            }

            if (returnflag == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool checkradio()
        {
            pnlErrorMsg1.Visible = false;
            lblerrorMsg1.Text = "";

            bool returnflag = false;

            bool flagcheck1 = false;
            bool flagcheck2 = false;
            bool flagcheck3 = false;
            bool flagcheck4 = false;
            bool flagcheck5 = false;
            bool flagcheck6 = false;

            for (int i = 0; i < ListView2.Items.Count; i++)
            {
                CheckBox RDODay1 = (CheckBox)ListView2.Items[i].FindControl("RDODay1");
                CheckBox RDODay2 = (CheckBox)ListView2.Items[i].FindControl("RDODay2");
                CheckBox RDODay3 = (CheckBox)ListView2.Items[i].FindControl("RDODay3");
                CheckBox RDODay4 = (CheckBox)ListView2.Items[i].FindControl("RDODay4");
                CheckBox RDODay5 = (CheckBox)ListView2.Items[i].FindControl("RDODay5");
                CheckBox RDODay6 = (CheckBox)ListView2.Items[i].FindControl("RDODay6");

                string[] days = tags_1.Text.Split(',');
                foreach (string Val in days)
                {
                    if (Val == "Sat")
                    {
                        if (RDODay1.Checked == true)
                        {
                            flagcheck1 = true;
                        }
                    }
                    if (Val == "Sun")
                    {
                        if (RDODay2.Checked == true)
                        {
                            flagcheck2 = true;
                        }
                    }
                    if (Val == "Mon")
                    {
                        if (RDODay3.Checked == true)
                        {
                            flagcheck3 = true;
                        }
                    }
                    if (Val == "Tue")
                    {
                        if (RDODay4.Checked == true)
                        {
                            flagcheck4 = true;
                        }
                    }
                    if (Val == "Wed")
                    {
                        if (RDODay5.Checked == true)
                        {
                            flagcheck5 = true;
                        }
                    }
                    if (Val == "Thu")
                    {
                        if (RDODay6.Checked == true)
                        {
                            flagcheck6 = true;
                        }
                    }
                }
            }

            string MSG = "";
            string[] day = tags_1.Text.Split(',');
            foreach (string Val1 in day)
            {
                if (Val1 == "Sat")
                {
                    if (flagcheck1 == false)
                    {
                        MSG += " Check Atleast One Product in " + DayOfWeek.Saturday + " <br>";
                    }
                }
                if (Val1 == "Sun")
                {
                    if (flagcheck2 == false)
                    {
                        MSG += " Check Atleast One Product in " + DayOfWeek.Sunday + " <br>";
                    }
                }
                if (Val1 == "Mon")
                {
                    if (flagcheck3 == false)
                    {
                        MSG += " Check Atleast One Product in " + DayOfWeek.Monday + " <br>";
                    }
                }
                if (Val1 == "Tue")
                {
                    if (flagcheck4 == false)
                    {
                        MSG += " Check Atleast One Product in " + DayOfWeek.Tuesday + " <br>";
                    }
                }
                if (Val1 == "Wed")
                {
                    if (flagcheck5 == false)
                    {
                        MSG += " Check Atleast One Product in " + DayOfWeek.Wednesday + " <br>";
                    }
                }
                if (Val1 == "Thu")
                {
                    if (flagcheck6 == false)
                    {
                        MSG += " Check Atleast One Product in " + DayOfWeek.Thursday + " <br>";
                    }
                }
            }

            if (MSG != "")
            {
                pnlErrorMsg1.Visible = true;
                lblerrorMsg1.Text = MSG;
                return false;
            }
            else
            {
                returnflag = true;
            }

            if (returnflag == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void BindData()
        {
            List<Database.planmealcustinvoice> List = new List<planmealcustinvoice>();

            var result1 = (from pm in DB.planmealcustinvoices.Where(p => p.TenentID == TID)
                           join
                             ur in DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.ACTIVE == true) on pm.MYTRANSID equals ur.MYTRANSID

                           select new { ur.MYTRANSID, ur.CustomerID, ur.planid, pm.DeliveryMeal, pm.DeliveryID, ur.ContractID, ur.StartDate, ur.EndDate }).ToList();
            var List1 = result1.GroupBy(p => p.MYTRANSID).Select(p => p.FirstOrDefault()).ToList();

            Listview1.DataSource = List1.OrderByDescending(p => p.MYTRANSID);
            Listview1.DataBind();

            Listview4.DataSource = DB.CRUP_MST.Where(p => p.TENANT_ID == TID && p.MENU_ID == MenuID).OrderByDescending(p => p.CRUP_ID);
            Listview4.DataBind();
        }
        #endregion

        public void GetShow()
        {
            lblCustomer1s.Attributes["class"] = lblInvoiceNumber1s.Attributes["class"] = lblPlan1s.Attributes["class"] = lblMeal1s.Attributes["class"] = lblBeingDate1s.Attributes["class"] = lblEndDate1s.Attributes["class"] = lblDay1s.Attributes["class"] = lblTotalPlanPrice1s.Attributes["class"] = "control-label col-md-4  getshow";//lblBlank1s.Attributes["class"] =
            lblCustomer2h.Attributes["class"] = lblInvoiceNumber2h.Attributes["class"] = lblPlan2h.Attributes["class"] = lblMeal2h.Attributes["class"] = lblBeingDate2h.Attributes["class"] = lblTotalDeliveryDay2h.Attributes["class"] = lblEndDate2h.Attributes["class"] = lblDay2h.Attributes["class"] = lblTotalPlanPrice2h.Attributes["class"] = "control-label col-md-4  gethide";//lblBlank2h.Attributes["class"] =
            lblTotalDeliveryDay1s.Attributes["class"] = "control-label col-md-6  getshow";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblCustomer1s.Attributes["class"] = lblInvoiceNumber1s.Attributes["class"] = lblPlan1s.Attributes["class"] = lblMeal1s.Attributes["class"] = lblBeingDate1s.Attributes["class"] = lblEndDate1s.Attributes["class"] = lblDay1s.Attributes["class"] = lblTotalPlanPrice1s.Attributes["class"] = "control-label col-md-4  gethide";//lblBlank1s.Attributes["class"] =
            lblCustomer2h.Attributes["class"] = lblInvoiceNumber2h.Attributes["class"] = lblPlan2h.Attributes["class"] = lblMeal2h.Attributes["class"] = lblBeingDate2h.Attributes["class"] = lblTotalDeliveryDay2h.Attributes["class"] = lblEndDate2h.Attributes["class"] = lblDay2h.Attributes["class"] = lblTotalPlanPrice2h.Attributes["class"] = "control-label col-md-4  getshow";//lblBlank2h.Attributes["class"] =
            lblTotalDeliveryDay1s.Attributes["class"] = "control-label col-md-6  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "rtl");

        }
        protected void btnHide_Click(object sender, EventArgs e)
        {
            GetHide();
        }
        protected void btnShow_Click(object sender, EventArgs e)
        {
            GetShow();
        }

        public void clearGrid()
        {
            ListView2.DataSource = null;
            ListView2.DataBind();

            Listview3.DataSource = null;
            Listview3.DataBind();
        }
        public void Clear()
        {
            txtInvoiceNumber.Text = "";
            drpPlan.SelectedValue = "0";
            drpMeal.SelectedValue = "0";
            txtBeingDate.Text = "";
            txtTotalDeliveryDay.Text = "";
            txtEndDate.Text = "";
            drpDay.SelectedIndex = 0;
            drpCustomer.SelectedIndex = 0;
            //drpProtein.SelectedIndex = 0;
            //txtWeight.Text = "";
            //txtsaleprice.Text = "";
            txtTotalWeek.Text = "";
            txtRemainingDay.Text = "";
            txtDelivered.Text = "";
            drpweekofday.SelectedIndex = 0;
            tags_1.Text = "";
            lblDeliveryTIME.Text = "0";
            //drpDeliveryTime.SelectedIndex = 0;
            drpDeliveryMeal.SelectedIndex = 0;
            drpHowManyDay.SelectedIndex = 0;
            txtTotalPlanPrice.Text = "";
            drpExpecteddriver.SelectedIndex = 0;
            //drpProductListDay.SelectedIndex = 0;

        }

        public void adddt(int Mytrans, Label lbldelday, Label lblFDate, Label lblDayName, Label lbldeldayH, Label lblProd, TextBox txtproduct, TextBox txtWeight, TextBox txtCalories, TextBox txtProtain, TextBox txtCarbs, TextBox txtFat, TextBox txtsaleprice, int DeliverySequence, TextBox txtqtyNO)
        {
            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";

            int Custo = Convert.ToInt32(drpCustomer.SelectedValue);
            long MYPRODID = Convert.ToInt64(lblProd.Text);
            int DayNumber = Convert.ToInt32(lbldelday.Text);
            int DeliveryMeal = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
            DateTime ExpectedDelDate = Convert.ToDateTime(lblFDate.Text);
            List<Database.planmealcustinvoice> ListExist = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == Mytrans && p.MYPRODID == MYPRODID && p.DeliveryMeal == DeliveryMeal && p.DayNumber == DayNumber).ToList();
            if (ListExist.Count() < 1)
            {
                Database.planmealcustinvoice objplanmealcustinvoice = new Database.planmealcustinvoice();

                objplanmealcustinvoice.TenentID = TID;
                objplanmealcustinvoice.MYTRANSID = Mytrans;
                objplanmealcustinvoice.DeliveryID = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == Mytrans).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == Mytrans).Max(p => p.DeliveryID) + 1) : 1;
                objplanmealcustinvoice.LOCATION_ID = LID;
                objplanmealcustinvoice.TransID = Convert.ToInt32(txtInvoiceNumber.Text);//Convert.ToInt32(txtInvoiceNumber1s.Text);
                objplanmealcustinvoice.ContractID = Mytrans.ToString();
                int plan = Convert.ToInt32(drpPlan.SelectedValue);
                objplanmealcustinvoice.planid = plan;
                objplanmealcustinvoice.CustomerID = Convert.ToInt32(drpCustomer.SelectedValue);
                objplanmealcustinvoice.Qty = Convert.ToInt32(txtqtyNO.Text);

                objplanmealcustinvoice.DayNumber = Convert.ToInt32(lbldelday.Text);
                objplanmealcustinvoice.ExpectedDelDate = Convert.ToDateTime(lblFDate.Text);
                objplanmealcustinvoice.Status = "Pending";
                DateTime nextday = Convert.ToDateTime(lblFDate.Text);
                if (nextday.DayOfWeek == DayOfWeek.Thursday)
                {
                    objplanmealcustinvoice.NExtDeliveryDate = nextday.AddDays(2);
                }
                else
                {
                    objplanmealcustinvoice.NExtDeliveryDate = nextday.AddDays(1);
                }
                objplanmealcustinvoice.NameOfDay = lblDayName.Text;
                objplanmealcustinvoice.OprationDay = Convert.ToInt32(lbldeldayH.Text);

                //objplanmealcustinvoice.DayNumber = Convert.ToInt32(drpDay.SelectedValue);
                objplanmealcustinvoice.Total_price = Convert.ToDecimal(txtTotalPlanPrice.Text);
                //Calculation Field
                if (txtBeingDate.Text != "")
                    objplanmealcustinvoice.StartDate = Convert.ToDateTime(txtBeingDate.Text);
                if (txtEndDate.Text != "")
                    objplanmealcustinvoice.EndDate = Convert.ToDateTime(txtEndDate.Text);

                objplanmealcustinvoice.TotalWeek = Convert.ToInt32(txtTotalWeek.Text);
                objplanmealcustinvoice.NoOfWeek = Convert.ToInt32(DrpWeek.SelectedValue);
                if (DrpWeek.SelectedValue == "1")
                {
                    objplanmealcustinvoice.DisplayWeek = 1;
                }
                objplanmealcustinvoice.TotalDeliveryDay = Convert.ToInt32(txtTotalDeliveryDay.Text);
                objplanmealcustinvoice.ActualDeliveryDay = Convert.ToInt32(txtDelivered.Text);
                objplanmealcustinvoice.ExpectedDeliveryDay = Convert.ToInt32(txtRemainingDay.Text);
                //objplanmealcustinvoice.DeliveryTime = Convert.ToInt32(drpDeliveryTime.SelectedValue);

                if (drpDeliveryMeal.SelectedValue != "0")
                {
                    int DeliveryMeal1 = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
                    objplanmealcustinvoice.DeliveryMeal = DeliveryMeal1;
                    objplanmealcustinvoice.MealType = DeliveryMeal1;// Convert.ToInt32(drpDeliveryMeal.SelectedValue);//Convert.ToInt32(drpMeal.SelectedValue);
                    List<Database.REFTABLE> ListRef = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == DeliveryMeal1).ToList();
                    objplanmealcustinvoice.Switch3 = ListRef.Where(p => p.SWITCH1 != null && p.SWITCH1 != "").Count() > 0 ? ListRef.Single(p => p.TenentID == TID && p.REFID == DeliveryMeal1 && p.SWITCH1 != null && p.SWITCH1 != "").SWITCH1.ToString() : "";
                    List<Database.tblcontact_addon1_dtl> ListTime = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.customerID == Custo && p.planid == plan && p.ItemCode == DeliveryMeal1).ToList();
                    objplanmealcustinvoice.DeliveryTime = ListTime.Count() > 0 ? Convert.ToInt32(ListTime.FirstOrDefault().DeliveryTime) : Convert.ToInt32(lblDeliveryTIMEID.Text);
                }
                else
                {
                    pnlErrorMsg.Visible = true;
                    lblerrorMsg.Text = "Select Delivery Meal";
                    return;
                }

                objplanmealcustinvoice.WeekofDay = tags_1.Text;
                //End
                //objplanmealcustinvoice.ExpectedDelDate = Convert.ToDateTime(txtExpDate.Text);

                decimal ItemWeight = 100;
                decimal Calories = 300;
                decimal Protein = 14;
                decimal Carbs = 36;
                decimal Fat = 9;

                List<Database.planmealsetup> ListPlanpro = DB.planmealsetups.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == plan && p.MealType == DeliveryMeal && p.MYPRODID == MYPRODID).ToList();

                if (ListPlanpro.Count() > 0)
                {
                    Database.planmealsetup objpan = ListPlanpro.Single(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == plan && p.MealType == DeliveryMeal && p.MYPRODID == MYPRODID);
                    ItemWeight = objpan.ItemWeight != null && objpan.ItemWeight != 0 ? Convert.ToDecimal(objpan.ItemWeight) : 100;
                    Calories = objpan.Calories != null && objpan.Calories != 0 ? Convert.ToDecimal(objpan.Calories) : 300;
                    Protein = objpan.Protein != null && objpan.Protein != 0 ? Convert.ToDecimal(objpan.Protein) : 14;
                    Carbs = objpan.Carbs != null && objpan.Carbs != 0 ? Convert.ToDecimal(objpan.Carbs) : 36;
                    Fat = objpan.Fat != null && objpan.Fat != 0 ? Convert.ToDecimal(objpan.Fat) : 9;
                }

                objplanmealcustinvoice.MYPRODID = Convert.ToInt64(lblProd.Text);
                objplanmealcustinvoice.ProdName1 = txtproduct.Text;
                objplanmealcustinvoice.ItemWeight = txtWeight.Text != "" ? Convert.ToDecimal(txtWeight.Text) : ItemWeight;
                objplanmealcustinvoice.Calories = txtCalories.Text != "" ? Convert.ToDecimal(txtCalories.Text) : Calories;
                objplanmealcustinvoice.Protein = txtProtain.Text != "" ? Convert.ToDecimal(txtProtain.Text) : Protein;
                objplanmealcustinvoice.Carbs = txtCarbs.Text != "" ? Convert.ToDecimal(txtCarbs.Text) : Carbs;
                objplanmealcustinvoice.Fat = txtFat.Text != "" ? Convert.ToDecimal(txtFat.Text) : Fat;
                decimal SalePrice = Convert.ToDecimal(DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == MYPRODID).msrp);
                objplanmealcustinvoice.Item_cost = txtsaleprice.Text != "" ? Convert.ToDecimal(txtsaleprice.Text) : SalePrice;
                objplanmealcustinvoice.DriverID = Convert.ToInt32(drpExpecteddriver.SelectedValue);
                objplanmealcustinvoice.DeliverySequence = DeliverySequence;
                objplanmealcustinvoice.ACTIVE = true;
                objplanmealcustinvoice.ShortRemark = "TRUE";


                //string Log = "MYTRANSID=" + Mytrans + ", CustomerID=" + CID + ", planid=" + plan + ", DeliveryMeal=" + DeliveryMeal + ", MYPRODID=" + MYPRODID;

                //int CRUP_ID = GlobleClass.EncryptionHelpers.WriteLog("planmealcustinvoice,INSERT: " + Log, "INSERT", "planmealcustinvoice", UID.ToString(), MenuID);
                //objplanmealcustinvoice.CRUP_ID = CRUP_ID;

                DB.planmealcustinvoices.AddObject(objplanmealcustinvoice);
                DB.SaveChanges();
            }


        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //btnAdd.Attributes["id"] = "ContentPlaceHolder1_blockui_sample_2_2";            
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "please wait... ", "showProgress()", true);
            pnlErrorMsg1.Visible = false;
            lblerrorMsg1.Text = "";

            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";



            int SelectCount1 = 0;
            int SelectCount2 = 0;
            int SelectCount3 = 0;
            int SelectCount4 = 0;
            int SelectCount5 = 0;
            int SelectCount6 = 0;

            int planid = Convert.ToInt32(drpPlan.SelectedValue);
            List<Database.tblProduct_Plan> ListPlan = DB.tblProduct_Plan.Where(p => p.TenentID == TID && p.locationid == LID && p.planid == planid).ToList();
            if (ListPlan.Count() > 0)
            {
                int DayProd = Convert.ToInt32(ListPlan.Single(p => p.TenentID == TID && p.locationid == LID && p.planid == planid).MealRepeatInDay);
                for (int i = 0; i < ListView2.Items.Count; i++)
                {
                    CheckBox RDODay1 = (CheckBox)ListView2.Items[i].FindControl("RDODay1");
                    CheckBox RDODay2 = (CheckBox)ListView2.Items[i].FindControl("RDODay2");
                    CheckBox RDODay3 = (CheckBox)ListView2.Items[i].FindControl("RDODay3");
                    CheckBox RDODay4 = (CheckBox)ListView2.Items[i].FindControl("RDODay4");
                    CheckBox RDODay5 = (CheckBox)ListView2.Items[i].FindControl("RDODay5");
                    CheckBox RDODay6 = (CheckBox)ListView2.Items[i].FindControl("RDODay6");

                    if (RDODay1.Checked == true)
                    {
                        SelectCount1++;
                    }

                    if (RDODay2.Checked == true)
                    {
                        SelectCount2++;
                    }

                    if (RDODay3.Checked == true)
                    {
                        SelectCount3++;
                    }

                    if (RDODay4.Checked == true)
                    {
                        SelectCount4++;
                    }

                    if (RDODay5.Checked == true)
                    {
                        SelectCount5++;
                    }

                    if (RDODay6.Checked == true)
                    {
                        SelectCount6++;
                    }
                }

                string MSG = "";
                string Planname = drpPlan.SelectedItem.ToString();
                string meal = drpDeliveryMeal.SelectedItem.ToString();

                if (DayProd < SelectCount1)
                {
                    MSG += "<B><U>" + Planname + "</U></B>  For  <b><U> " + meal + " </U></B>  are allowed to have only " + DayProd + "Item for " + lblDate1.Text + " . Kindly Remove more than Allowed ? <BR>";
                }

                if (DayProd < SelectCount2)
                {
                    MSG += "<B><U>" + Planname + "</U></B>  For  <b><U> " + meal + " </U></B>  are allowed to have only " + DayProd + "Item for " + lblDate2.Text + " . Kindly Remove more than Allowed ? <BR>";
                }

                if (DayProd < SelectCount3)
                {
                    MSG += "<B><U>" + Planname + "</U></B>  For  <b><U> " + meal + " </U></B>  are allowed to have only " + DayProd + "Item for " + lblDate3.Text + " . Kindly Remove more than Allowed ? <BR>";
                }

                if (DayProd < SelectCount4)
                {
                    MSG += "<B><U>" + Planname + "</U></B>  For  <b><U> " + meal + " </U></B>  are allowed to have only " + DayProd + "Item for " + lblDate4.Text + " . Kindly Remove more than Allowed ? <BR>";
                }

                if (DayProd < SelectCount5)
                {
                    MSG += "<B><U>" + Planname + "</U></B>  For  <b><U> " + meal + " </U></B>  are allowed to have only " + DayProd + "Item for " + lblDate5.Text + " . Kindly Remove more than Allowed ? <BR>";
                }
                if (DayProd < SelectCount6)
                {
                    MSG += "<B><U>" + Planname + "</U></B>  For  <b><U> " + meal + " </U></B>  are allowed to have only " + DayProd + "Item for " + lblDate6.Text + " . Kindly Remove more than Allowed ? <BR>";
                }

                if (MSG != "")
                {
                    pnlErrorMsg1.Visible = true;
                    lblerrorMsg1.Text = MSG;
                    return;
                }

            }

            int QTYCount1 = 0;
            int QTYCount2 = 0;
            int QTYCount3 = 0;
            int QTYCount4 = 0;
            int QTYCount5 = 0;
            int QTYCount6 = 0;

            if (ListPlan.Count() > 0)
            {
                int DayProd = Convert.ToInt32(ListPlan.Single(p => p.TenentID == TID && p.locationid == LID && p.planid == planid).MealRepeatInDay);
                for (int i = 0; i < ListView2.Items.Count; i++)
                {
                    CheckBox RDODay1 = (CheckBox)ListView2.Items[i].FindControl("RDODay1");
                    CheckBox RDODay2 = (CheckBox)ListView2.Items[i].FindControl("RDODay2");
                    CheckBox RDODay3 = (CheckBox)ListView2.Items[i].FindControl("RDODay3");
                    CheckBox RDODay4 = (CheckBox)ListView2.Items[i].FindControl("RDODay4");
                    CheckBox RDODay5 = (CheckBox)ListView2.Items[i].FindControl("RDODay5");
                    CheckBox RDODay6 = (CheckBox)ListView2.Items[i].FindControl("RDODay6");

                    TextBox txtqty1 = (TextBox)ListView2.Items[i].FindControl("txtqty1");
                    TextBox txtqty2 = (TextBox)ListView2.Items[i].FindControl("txtqty2");
                    TextBox txtqty3 = (TextBox)ListView2.Items[i].FindControl("txtqty3");
                    TextBox txtqty4 = (TextBox)ListView2.Items[i].FindControl("txtqty4");
                    TextBox txtqty5 = (TextBox)ListView2.Items[i].FindControl("txtqty5");
                    TextBox txtqty6 = (TextBox)ListView2.Items[i].FindControl("txtqty6");

                    txtqty1.Visible = false;
                    txtqty2.Visible = false;
                    txtqty3.Visible = false;
                    txtqty4.Visible = false;
                    txtqty5.Visible = false;
                    txtqty6.Visible = false;


                    TextBox txtproduct = (TextBox)ListView2.Items[i].FindControl("txtproduct");

                    if (RDODay1.Checked == true && (txtqty1.Text == "" || txtqty1.Text == "0"))
                    {
                        QTYCount1++;
                        txtqty1.Visible = true;
                    }

                    if (RDODay2.Checked == true && (txtqty2.Text == "" || txtqty2.Text == "0"))
                    {
                        QTYCount2++;
                        txtqty2.Visible = true;
                    }

                    if (RDODay3.Checked == true && (txtqty3.Text == "" || txtqty3.Text == "0"))
                    {
                        QTYCount3++;
                        txtqty3.Visible = true;
                    }

                    if (RDODay4.Checked == true && (txtqty4.Text == "" || txtqty4.Text == "0"))
                    {
                        QTYCount4++;
                        txtqty4.Visible = true;
                    }

                    if (RDODay5.Checked == true && (txtqty5.Text == "" || txtqty5.Text == "0"))
                    {
                        QTYCount5++;
                        txtqty5.Visible = true;
                    }

                    if (RDODay6.Checked == true && (txtqty6.Text == "" || txtqty6.Text == "0"))
                    {
                        QTYCount6++;
                        txtqty6.Visible = true;
                    }
                }

                string MSGqty = "";


                if (QTYCount1 >= 1)
                {
                    MSGqty = "Enter Qty Greater than Zero or in-Check Product";
                    //MSGqty += "";
                }

                if (QTYCount2 >= 1)
                {
                    MSGqty = "Enter Qty Greater than Zero or in-Check Product";
                    //MSGqty += "";
                }

                if (QTYCount3 >= 1)
                {
                    MSGqty = "Enter Qty Greater than Zero or in-Check Product";
                    //MSGqty += "";
                }

                if (QTYCount4 >= 1)
                {
                    MSGqty = "Enter Qty Greater than Zero or in-Check Product";
                    //MSGqty += "";
                }

                if (QTYCount5 >= 1)
                {
                    MSGqty = "Enter Qty Greater than Zero or in-Check Product";
                    //MSGqty += "";
                }
                if (QTYCount6 >= 1)
                {
                    MSGqty = "Enter Qty Greater than Zero or in-Check Product";
                    //MSGqty += "";
                }

                if (MSGqty != "")
                {
                    pnlErrorMsg1.Visible = true;
                    lblerrorMsg1.Text = MSGqty;
                    return;
                }
            }


            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {

            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";

            if (btnAdd.Text == "Add New")
            {
                Write();
                Clear();
                switch1 = "AddON";
                SwitchoONOFF(switch1);
                btnAdd.Text = "Save";
                btnAdd.ValidationGroup = "submit";
                Trans = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
                txtInvoiceNumber.Text = Trans.ToString();

                List<Database.TBLCOMPANYSETUP> ListCustomer = new List<Database.TBLCOMPANYSETUP>();
                List<Database.tblcontact_addon1> FinelCustomer = DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.active == true).ToList();

                foreach (Database.tblcontact_addon1 Citem in FinelCustomer)
                {
                    if (DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.CustomerId == Citem.CustomerId && p.active == true).Count() > 0)
                    {
                        List<Database.tblcontact_addon1_dtl> ListDtl = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.customerID == Citem.CustomerId && p.LikeType == "M" && p.planid != null && p.planid != 0).ToList();
                        if (ListDtl.Count() > 0)
                        {
                            Database.TBLCOMPANYSETUP Cobj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == Citem.CustomerId);
                            ListCustomer.Add(Cobj);
                        }
                    }
                }

                //List<Database.planmealcustinvoiceHD> ListHD = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.ACTIVE == true).ToList();

                //foreach (Database.planmealcustinvoiceHD itemhd in ListHD)
                //{
                //    List<Database.tblcontact_addon1_dtl> ListDtl = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.customerID == itemhd.CustomerID && p.LikeType == "M" && p.planid != null && p.planid != 0).GroupBy(p => p.planid).Select(p => p.FirstOrDefault()).ToList();
                //    List<Database.planmealcustinvoiceHD> Listplan = ListHD.Where(p => p.TenentID == TID && p.CustomerID == itemhd.CustomerID).ToList();
                //    if (ListDtl.Count() > 0)
                //    {
                //        int Count = ListDtl.Count();
                //        int CountHD = Listplan.Count();
                //        if (CountHD >= Count)
                //        {
                //            Database.TBLCOMPANYSETUP Cobj = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == itemhd.CustomerID);
                //            ListCustomer.Remove(Cobj);
                //        }
                //    }
                //}

                if (ListCustomer.Count > 0)
                {
                    drpCustomer.DataSource = ListCustomer.OrderBy(p => p.COMPNAME1);// DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID);
                    drpCustomer.DataTextField = "COMPNAME1";
                    drpCustomer.DataValueField = "COMPID";
                    drpCustomer.DataBind();
                    drpCustomer.Items.Insert(0, new ListItem("-- Select --", "0"));
                    drpCustomer.SelectedIndex = 0;
                }
                else
                {
                    drpCustomer.Items.Clear();
                    drpCustomer.Items.Insert(0, new ListItem("-- Select --", "0"));
                    drpCustomer.SelectedIndex = 0;
                }

                drpPlan.Items.Clear();

                //drpDeliveryTime.Items.Clear();
            }
            else if (btnAdd.Text == "Save")
            {
                for (int i = 0; i < ListView2.Items.Count; i++)
                {
                    TextBox txtproduct = (TextBox)ListView2.Items[i].FindControl("txtproduct");
                    string product = txtproduct.Text.Trim();
                    if (product == "" || product == null)
                    {

                        pnlErrorMsg1.Visible = true;
                        lblerrorMsg1.Text = "Product Name is Required";
                        return;
                    }
                }

                DateTime Contrctdate = Convert.ToDateTime(txtExpDate.Text);
                DateTime satrtdate = Convert.ToDateTime(txtBeingDate.Text);

                if (Contrctdate > satrtdate)
                {
                    pnlErrorMsg.Visible = true;
                    lblerrorMsg.Text = "Start Date greter then and equal Contract date";
                    return;
                }

                int Customer = Convert.ToInt32(drpCustomer.SelectedValue);
                int Plan = Convert.ToInt32(drpPlan.SelectedValue);
                int MealID = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
                int Mytrans = Convert.ToInt32(txtInvoiceNumber.Text);
                if (DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.MYTRANSID == Mytrans).Count() == 0)
                {
                    int MYTRANSID = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1) : 1;
                    Database.planmealcustinvoiceHD OBJHD = new Database.planmealcustinvoiceHD();
                    OBJHD.TenentID = TID;
                    OBJHD.MYTRANSID = MYTRANSID;
                    OBJHD.LOCATION_ID = LID;
                    OBJHD.CustomerID = Customer;
                    OBJHD.planid = Plan;
                    OBJHD.ContractID = MYTRANSID.ToString();
                    OBJHD.TransID = MYTRANSID;

                    OBJHD.DefaultDriverID = Convert.ToInt32(drpExpecteddriver.SelectedValue);
                    OBJHD.StartDate = Convert.ToDateTime(txtBeingDate.Text);
                    OBJHD.EndDate = Convert.ToDateTime(txtEndDate.Text);
                    OBJHD.TotalSubDays = Convert.ToInt32(txtRemainingDay.Text);
                    OBJHD.DeliveredDays = Convert.ToInt32(txtDelivered.Text);
                    OBJHD.ContractDate = Convert.ToDateTime(txtExpDate.Text);
                    OBJHD.WeekofDay = tags_1.Text.ToString();
                    DateTime BeingDate = Convert.ToDateTime(txtBeingDate.Text);
                    OBJHD.NExtDeliveryDate = BeingDate.AddDays(1);
                    OBJHD.NExtDeliveryNum = 1;
                    OBJHD.Total_price = Convert.ToDecimal(txtTotalPlanPrice.Text);
                    OBJHD.SubscriptionOnHold = false;
                    OBJHD.ACTIVE = true;
                    OBJHD.CStatus = "Started";

                    //string Log = " MYTRANSID=" + MYTRANSID;
                    //int CRUP_ID = GlobleClass.EncryptionHelpers.WriteLog("planmealcustinvoiceHD,INSERT:"+Log, "INSERT", "planmealcustinvoiceHD", UID.ToString(), MenuID);
                    //OBJHD.CRUP_ID = CRUP_ID;

                    DB.planmealcustinvoiceHDs.AddObject(OBJHD);
                    DB.SaveChanges();
                }

                int driverid = Convert.ToInt32(drpExpecteddriver.SelectedValue);
                //int DeliveryTime = Convert.ToInt32(drpDeliveryTime.SelectedValue);
                int DeliveryTime = Convert.ToInt32(lblDeliveryTIMEID.Text);
                int DeliverySequence = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.DeliveryMeal == MealID && p.DriverID == driverid && p.DeliveryTime == DeliveryTime).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.DeliveryMeal == MealID && p.DriverID == driverid && p.DeliveryTime == DeliveryTime).Max(p => p.DeliverySequence) + 1) : 1;

                for (int i = 0; i < ListView2.Items.Count; i++)
                {
                    Label lblProd = (Label)ListView2.Items[i].FindControl("lblProd");
                    TextBox txtWeight = (TextBox)ListView2.Items[i].FindControl("txtWeight");
                    TextBox txtCalories = (TextBox)ListView2.Items[i].FindControl("txtCalories");
                    TextBox txtProtain = (TextBox)ListView2.Items[i].FindControl("txtProtain");
                    TextBox txtCarbs = (TextBox)ListView2.Items[i].FindControl("txtCarbs");
                    TextBox txtFat = (TextBox)ListView2.Items[i].FindControl("txtFat");

                    TextBox txtproduct = (TextBox)ListView2.Items[i].FindControl("txtproduct");
                    TextBox txtsaleprice = (TextBox)ListView2.Items[i].FindControl("txtsaleprice");
                    CheckBox RDODay1 = (CheckBox)ListView2.Items[i].FindControl("RDODay1");
                    CheckBox RDODay2 = (CheckBox)ListView2.Items[i].FindControl("RDODay2");
                    CheckBox RDODay3 = (CheckBox)ListView2.Items[i].FindControl("RDODay3");
                    CheckBox RDODay4 = (CheckBox)ListView2.Items[i].FindControl("RDODay4");
                    CheckBox RDODay5 = (CheckBox)ListView2.Items[i].FindControl("RDODay5");
                    CheckBox RDODay6 = (CheckBox)ListView2.Items[i].FindControl("RDODay6");

                    TextBox txtqty1 = (TextBox)ListView2.Items[i].FindControl("txtqty1");
                    TextBox txtqty2 = (TextBox)ListView2.Items[i].FindControl("txtqty2");
                    TextBox txtqty3 = (TextBox)ListView2.Items[i].FindControl("txtqty3");
                    TextBox txtqty4 = (TextBox)ListView2.Items[i].FindControl("txtqty4");
                    TextBox txtqty5 = (TextBox)ListView2.Items[i].FindControl("txtqty5");
                    TextBox txtqty6 = (TextBox)ListView2.Items[i].FindControl("txtqty6");

                    if (RDODay1.Checked == true)
                    {
                        adddt(Mytrans, lbldelday1, lblFDate1, lblDayName1, lbldelday11, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty1);
                        DeliverySequence++;
                    }
                    if (RDODay2.Checked == true)
                    {
                        adddt(Mytrans, lbldelday2, lblFDate2, lblDayName2, lbldelday21, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty2);
                        DeliverySequence++;
                    }
                    if (RDODay3.Checked == true)
                    {
                        adddt(Mytrans, lbldelday3, lblFDate3, lblDayName3, lbldelday31, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty3);
                        DeliverySequence++;
                    }
                    if (RDODay4.Checked == true)
                    {
                        adddt(Mytrans, lbldelday4, lblFDate4, lblDayName4, lbldelday41, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty4);
                        DeliverySequence++;
                    }
                    if (RDODay5.Checked == true)
                    {
                        adddt(Mytrans, lbldelday5, lblFDate5, lblDayName5, lbldelday51, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty5);
                        DeliverySequence++;
                    }
                    if (RDODay6.Checked == true)
                    {
                        adddt(Mytrans, lbldelday6, lblFDate6, lblDayName6, lbldelday61, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty6);
                        DeliverySequence++;
                    }
                }
                lblMsg.Text = "  Data Save Successfully";
                btnAdd.Text = "Save";
                btnAdd.Enabled = false;
                //btnAdd.ValidationGroup = "ss";
                pnlSuccessMsg.Visible = true;
                Readonly();
                //Clear();
                btnAddmeal.Visible = true;
                btnAddmeal.Enabled = true;
                //clearGrid();
                GetProductList();
                ListView2.DataSource = null;
                ListView2.DataBind();
                //switch1 = "AddOff";
                //Response.Redirect("/Master/HBInvoice.aspx");
                //FillContractorID();
            }
            else if (btnAdd.Text == "Update")
            {
                for (int i = 0; i < ListView2.Items.Count; i++)
                {
                    TextBox txtproduct = (TextBox)ListView2.Items[i].FindControl("txtproduct");
                    string product = txtproduct.Text.Trim();
                    if (product == "" || product == null)
                    {
                        pnlErrorMsg1.Visible = true;
                        lblerrorMsg1.Text = "Product Name is Required";
                        return;
                    }
                }

                DateTime Contrctdate = Convert.ToDateTime(txtExpDate.Text);
                DateTime satrtdate = Convert.ToDateTime(txtBeingDate.Text);

                if (Contrctdate > satrtdate)
                {
                    pnlErrorMsg.Visible = true;
                    lblerrorMsg.Text = "Start Date greter then and equal Contract date";
                    return;
                }

                int rdCount1 = 0;
                int rdCount2 = 0;
                int rdCount3 = 0;
                int rdCount4 = 0;
                int rdCount5 = 0;
                int rdCount6 = 0;

                for (int i = 0; i < ListView2.Items.Count; i++)
                {
                    CheckBox RDODay1 = (CheckBox)ListView2.Items[i].FindControl("RDODay1");
                    CheckBox RDODay2 = (CheckBox)ListView2.Items[i].FindControl("RDODay2");
                    CheckBox RDODay3 = (CheckBox)ListView2.Items[i].FindControl("RDODay3");
                    CheckBox RDODay4 = (CheckBox)ListView2.Items[i].FindControl("RDODay4");
                    CheckBox RDODay5 = (CheckBox)ListView2.Items[i].FindControl("RDODay5");
                    CheckBox RDODay6 = (CheckBox)ListView2.Items[i].FindControl("RDODay6");

                    if (RDODay1.Checked == true)
                    {
                        rdCount1++;
                    }
                    if (RDODay2.Checked == true)
                    {
                        rdCount2++;
                    }
                    if (RDODay3.Checked == true)
                    {
                        rdCount3++;
                    }
                    if (RDODay4.Checked == true)
                    {
                        rdCount4++;
                    }
                    if (RDODay5.Checked == true)
                    {
                        rdCount5++;
                    }
                    if (RDODay6.Checked == true)
                    {
                        rdCount6++;
                    }

                }

                string[] days = tags_1.Text.Split(',');
                foreach (string Val in days)
                {
                    if (Val == "Sat")
                    {
                        if ((lblDate1.Text == "" || lblFDate1.Text == "" || lbldelday1.Text == "") && rdCount1 != 0)
                        {
                            pnlErrorMsg.Visible = true;
                            btnDateset1.Visible = true;
                            lblerrorMsg.Text = "Please set Date or Delivery Day";
                            return;
                        }
                    }

                    if (Val == "Sun")
                    {
                        if ((lblDate2.Text == "" || lblFDate2.Text == "" || lbldelday2.Text == "") && rdCount2 != 0)
                        {
                            pnlErrorMsg.Visible = true;
                            btnDateset2.Visible = true;
                            lblerrorMsg.Text = "Please set Date or Delivery Day";
                            return;
                        }
                    }

                    if (Val == "Mon")
                    {
                        if ((lblDate3.Text == "" || lblFDate3.Text == "" || lbldelday3.Text == "") && rdCount3 != 0)
                        {
                            pnlErrorMsg.Visible = true;
                            btnDateset3.Visible = true;
                            lblerrorMsg.Text = "Please set Date or Delivery Day";
                            return;
                        }
                    }

                    if (Val == "Tue")
                    {
                        if ((lblDate4.Text == "" || lblFDate4.Text == "" || lbldelday4.Text == "") && rdCount4 != 0)
                        {
                            pnlErrorMsg.Visible = true;
                            btnDateset4.Visible = true;
                            lblerrorMsg.Text = "Please set Date or Delivery Day";
                            return;
                        }
                    }

                    if (Val == "Wed")
                    {
                        if ((lblDate5.Text == "" || lblFDate5.Text == "" || lbldelday5.Text == "") && rdCount5 != 0)
                        {
                            pnlErrorMsg.Visible = true;
                            btnDateset5.Visible = true;
                            lblerrorMsg.Text = "Please set Date or Delivery Day";
                            return;
                        }
                    }

                    if (Val == "Thu")
                    {
                        if ((lblDate6.Text == "" || lblFDate6.Text == "" || lbldelday6.Text == "") && rdCount6 != 0)
                        {
                            pnlErrorMsg.Visible = true;
                            btnDateset6.Visible = true;
                            lblerrorMsg.Text = "Please set Date or Delivery Day";
                            return;
                        }
                    }
                }

                if (ViewState["ALL"] != null && ViewState["MYTRANSID"] != null)
                {
                    string[] ID = ViewState["ALL"].ToString().Split(',');
                    int CID = Convert.ToInt32(ID[0]);
                    int Plane = Convert.ToInt32(ID[1]);
                    int Meal = Convert.ToInt32(ID[2]);
                    //int DID = Convert.ToInt32(ID[3]);
                    //int Day = Convert.ToInt32(ID[4]);

                    int MYTRANSID = Convert.ToInt32(ViewState["MYTRANSID"]);

                    // efact where change of date
                    DateTime StartDate = Convert.ToDateTime(txtBeingDate.Text).Date;
                    DateTime EndDate = Convert.ToDateTime(txtEndDate.Text).Date;

                    List<Database.planmealcustinvoice> Listdtinvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                    List<Database.planmealcustinvoice> Listdt = Listdtinvoice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && (p.ExpectedDelDate < StartDate || p.ExpectedDelDate > EndDate)).ToList();

                    foreach (Database.planmealcustinvoice items in Listdt)
                    {
                        if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items.DeliveryID && p.MYPRODID == items.MYPRODID).Count() > 0)
                        {
                            Database.planmealcustinvoice objdt = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items.DeliveryID && p.MYPRODID == items.MYPRODID);
                            objdt.ACTIVE = false;
                            //DB.planmealcustinvoices.DeleteObject(objdt);
                            DB.SaveChanges();
                        }
                    }
                    //

                    if (DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ACTIVE == true).Count() > 0)
                    {
                        Database.planmealcustinvoiceHD OBJHD = DB.planmealcustinvoiceHDs.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ACTIVE == true);

                        OBJHD.DefaultDriverID = Convert.ToInt32(drpExpecteddriver.SelectedValue);
                        OBJHD.StartDate = Convert.ToDateTime(txtBeingDate.Text);
                        OBJHD.EndDate = Convert.ToDateTime(txtEndDate.Text);
                        OBJHD.TotalSubDays = Convert.ToInt32(txtRemainingDay.Text);
                        OBJHD.DeliveredDays = Convert.ToInt32(txtDelivered.Text);
                        OBJHD.WeekofDay = tags_1.Text.ToString();
                        //OBJHD.Total_price = Convert.ToDecimal(txtsaleprice.Text);
                        OBJHD.ACTIVE = true;

                        if (Listdtinvoice.Where(p => p.ActualDelDate != null).Count() > 0)
                        {
                            if (Listdtinvoice.Where(p => p.ActualDelDate == null).Count() > 0)
                            {
                                OBJHD.CStatus = "In Progress";
                            }
                            else
                            {
                                OBJHD.CStatus = "Completed";
                            }
                        }
                        else
                        {
                            OBJHD.CStatus = "Started";
                        }
                        //int crupID = Convert.ToInt32(OBJHD.CRUP_ID);
                        DB.SaveChanges();
                    }
                    int Week = Convert.ToInt32(DrpWeek.SelectedValue);
                    //string LogHD = " MYTRANSID=" + MYTRANSID;
                    //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoiceHD,Update:" +LogHD, crupID, "planmealcustinvoiceHD", UID.ToString());

                    int driverid = Convert.ToInt32(drpExpecteddriver.SelectedValue);
                    //int DeliveryTime = Convert.ToInt32(drpDeliveryTime.SelectedValue);
                    int DeliveryTime = Convert.ToInt32(lblDeliveryTIMEID.Text);
                    int DeleveryMeal = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
                    int DeliverySequence = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.DeliveryMeal == Meal && p.DriverID == driverid && p.DeliveryTime == DeliveryTime).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.DeliveryMeal == Meal && p.DriverID == driverid && p.DeliveryTime == DeliveryTime).Max(p => p.DeliverySequence) + 1) : 1;

                    for (int i = 0; i < ListView2.Items.Count; i++)
                    {
                        Label lblProd = (Label)ListView2.Items[i].FindControl("lblProd");
                        TextBox txtproduct = (TextBox)ListView2.Items[i].FindControl("txtproduct");
                        TextBox txtWeight = (TextBox)ListView2.Items[i].FindControl("txtWeight");
                        TextBox txtCalories = (TextBox)ListView2.Items[i].FindControl("txtCalories");
                        TextBox txtProtain = (TextBox)ListView2.Items[i].FindControl("txtProtain");
                        TextBox txtCarbs = (TextBox)ListView2.Items[i].FindControl("txtCarbs");
                        TextBox txtFat = (TextBox)ListView2.Items[i].FindControl("txtFat");

                        TextBox txtsaleprice = (TextBox)ListView2.Items[i].FindControl("txtsaleprice");
                        CheckBox RDODay1 = (CheckBox)ListView2.Items[i].FindControl("RDODay1");
                        CheckBox RDODay2 = (CheckBox)ListView2.Items[i].FindControl("RDODay2");
                        CheckBox RDODay3 = (CheckBox)ListView2.Items[i].FindControl("RDODay3");
                        CheckBox RDODay4 = (CheckBox)ListView2.Items[i].FindControl("RDODay4");
                        CheckBox RDODay5 = (CheckBox)ListView2.Items[i].FindControl("RDODay5");
                        CheckBox RDODay6 = (CheckBox)ListView2.Items[i].FindControl("RDODay6");

                        TextBox txtqty1 = (TextBox)ListView2.Items[i].FindControl("txtqty1");
                        TextBox txtqty2 = (TextBox)ListView2.Items[i].FindControl("txtqty2");
                        TextBox txtqty3 = (TextBox)ListView2.Items[i].FindControl("txtqty3");
                        TextBox txtqty4 = (TextBox)ListView2.Items[i].FindControl("txtqty4");
                        TextBox txtqty5 = (TextBox)ListView2.Items[i].FindControl("txtqty5");
                        TextBox txtqty6 = (TextBox)ListView2.Items[i].FindControl("txtqty6");

                        int PID = Convert.ToInt32(lblProd.Text);

                        List<Database.planmealcustinvoice> Listplanmealcustinvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID).ToList();

                        foreach (Database.planmealcustinvoice items in Listplanmealcustinvoice)
                        {
                            if (Listplanmealcustinvoice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items.DeliveryID && p.MYPRODID == items.MYPRODID && p.ActualDelDate == null).Count() > 0)
                            {
                                Database.planmealcustinvoice objeditprod = Listplanmealcustinvoice.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items.DeliveryID && p.MYPRODID == items.MYPRODID);
                                objeditprod.ProdName1 = txtproduct.Text;
                                //crupID = Convert.ToInt32(objeditprod.CRUP_ID);
                                DB.SaveChanges();
                                //string LogDT = " MYTRANSID=" + MYTRANSID;
                                //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoice,Update:" +LogDT, crupID, "planmealcustinvoice", UID.ToString());

                            }
                        }
                        int Custo = Convert.ToInt32(drpCustomer.SelectedValue);
                        int plan = Convert.ToInt32(drpPlan.SelectedValue);
                        List<Database.tblcontact_addon1_dtl> ListTime = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.customerID == Custo && p.planid == plan && p.ItemCode == DeleveryMeal).ToList();
                        List<Database.planmealcustinvoice> ListInvoiceEdit = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID && p.DeliveryMeal == DeleveryMeal && p.NoOfWeek == Week).ToList();


                        if (ListInvoiceEdit.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID).Count() > 0)
                        {
                            string[] days1 = tags_1.Text.Split(',');
                            foreach (string Val in days1)
                            {
                                if (RDODay1.Checked == true && Val == "Sat")
                                {
                                    DateTime Exp = Convert.ToDateTime(lblFDate1.Text);
                                    int Daynum = Convert.ToInt32(lbldelday11.Text);
                                    if (ListInvoiceEdit.Where(p => p.OprationDay == 1).Count() > 0)
                                    {
                                        if (ListInvoiceEdit.Where(p => p.OprationDay == 1 && p.DayNumber == Daynum && p.ExpectedDelDate == Exp && p.ActualDelDate == null).Count() > 0)
                                        {
                                            Database.planmealcustinvoice objinvoice1 = ListInvoiceEdit.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID && p.OprationDay == 1 && p.NoOfWeek == Week && p.DayNumber == Daynum && p.ExpectedDelDate == Exp);
                                            objinvoice1.DayNumber = Convert.ToInt32(lbldelday1.Text);
                                            objinvoice1.ExpectedDelDate = Convert.ToDateTime(lblFDate1.Text);
                                            DateTime nextday = Convert.ToDateTime(lblFDate1.Text);
                                            objinvoice1.NExtDeliveryDate = nextday.AddDays(1);
                                            objinvoice1.NameOfDay = lblDayName1.Text;
                                            objinvoice1.OprationDay = Convert.ToInt32(lbldelday11.Text);

                                            objinvoice1.Total_price = Convert.ToDecimal(txtTotalPlanPrice.Text);
                                            //Calculation Field
                                            if (txtBeingDate.Text != "")
                                                objinvoice1.StartDate = Convert.ToDateTime(txtBeingDate.Text);
                                            if (txtEndDate.Text != "")
                                                objinvoice1.EndDate = Convert.ToDateTime(txtEndDate.Text);
                                            objinvoice1.WeekofDay = tags_1.Text;
                                            objinvoice1.Qty = Convert.ToInt32(txtqty1.Text);
                                            objinvoice1.TotalWeek = Convert.ToInt32(txtTotalWeek.Text);
                                            objinvoice1.NoOfWeek = Convert.ToInt32(DrpWeek.SelectedValue);
                                            objinvoice1.TotalDeliveryDay = Convert.ToInt32(txtTotalDeliveryDay.Text);
                                            objinvoice1.ActualDeliveryDay = Convert.ToInt32(txtDelivered.Text);
                                            objinvoice1.ExpectedDeliveryDay = Convert.ToInt32(txtRemainingDay.Text);
                                            //objinvoice1.DeliveryTime = Convert.ToInt32(drpDeliveryTime.SelectedValue);                                          

                                            objinvoice1.DeliveryTime = ListTime.Count() > 0 ? Convert.ToInt32(ListTime.FirstOrDefault().DeliveryTime) : Convert.ToInt32(lblDeliveryTIMEID.Text);
                                            objinvoice1.DeliveryMeal = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
                                            objinvoice1.WeekofDay = tags_1.Text;
                                            //End
                                            //objplanmealcustinvoice.ExpectedDelDate = Convert.ToDateTime(txtExpDate.Text);
                                            objinvoice1.MYPRODID = Convert.ToInt64(lblProd.Text);

                                            objinvoice1.ItemWeight = Convert.ToDecimal(txtWeight.Text);
                                            objinvoice1.Calories = Convert.ToDecimal(txtCalories.Text);
                                            objinvoice1.Protein = Convert.ToDecimal(txtProtain.Text);
                                            objinvoice1.Carbs = Convert.ToDecimal(txtCarbs.Text);
                                            objinvoice1.Fat = Convert.ToDecimal(txtFat.Text);

                                            objinvoice1.Item_cost = Convert.ToDecimal(txtsaleprice.Text);
                                            objinvoice1.DriverID = Convert.ToInt32(drpExpecteddriver.SelectedValue);
                                            objinvoice1.ACTIVE = true;
                                            objinvoice1.ShortRemark = "TRUE";

                                            //crupID = Convert.ToInt32(objinvoice.CRUP_ID);
                                            DB.SaveChanges();
                                            //string Logdelete = " MYTRANSID=" + MYTRANSID + ", CustomerID=" + CID + ", planid=" + planid + ", DeliveryMeal=" + Meal + ", MYPRODID=" + PID;
                                            //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoice,Update:" + Logdelete, crupID, "planmealcustinvoice", UID.ToString());

                                        }
                                    }
                                    else
                                    {
                                        DateTime lbldate1 = Convert.ToDateTime(lblFDate1.Text);
                                        if (lbldate1 <= EndDate)
                                        {
                                            adddt(MYTRANSID, lbldelday1, lblFDate1, lblDayName1, lbldelday11, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty1);
                                            DeliverySequence++;
                                        }

                                    }
                                }
                                else
                                {
                                    if (Val == "Sat")
                                    {
                                        if (ListInvoiceEdit.Where(p => p.OprationDay == 1).Count() > 0)
                                        {
                                            Database.planmealcustinvoice objinvoicedel1 = ListInvoiceEdit.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID && p.OprationDay == 1 && p.NoOfWeek == Week);
                                            //crupID = Convert.ToInt32(objinvoice.CRUP_ID);
                                            //DB.planmealcustinvoices.DeleteObject(objinvoice);
                                            //string Logdelete = " MYTRANSID=" + MYTRANSID + ", CustomerID=" + CID + ", planid=" + planid + ", DeliveryMeal=" + Meal + ", MYPRODID=" + PID;
                                            objinvoicedel1.ACTIVE = false;
                                            DB.SaveChanges();
                                            // GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoice,Delete:" + Logdelete, crupID, "planmealcustinvoice", UID.ToString());
                                        }
                                    }
                                }

                                if (RDODay2.Checked == true && Val == "Sun")
                                {
                                    DateTime Exp = Convert.ToDateTime(lblFDate2.Text);
                                    int Daynum = Convert.ToInt32(lbldelday21.Text);
                                    if (ListInvoiceEdit.Where(p => p.OprationDay == 2).Count() > 0)
                                    {
                                        if (ListInvoiceEdit.Where(p => p.OprationDay == 2 && p.DayNumber == Daynum && p.ExpectedDelDate == Exp && p.ActualDelDate == null).Count() > 0)
                                        {
                                            Database.planmealcustinvoice objinvoice2 = ListInvoiceEdit.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID && p.OprationDay == 2 && p.NoOfWeek == Week && p.DayNumber == Daynum && p.ExpectedDelDate == Exp);
                                            objinvoice2.DayNumber = Convert.ToInt32(lbldelday2.Text);
                                            objinvoice2.ExpectedDelDate = Convert.ToDateTime(lblFDate2.Text);
                                            DateTime nextday = Convert.ToDateTime(lblFDate2.Text);
                                            objinvoice2.NExtDeliveryDate = nextday.AddDays(1);
                                            objinvoice2.NameOfDay = lblDayName2.Text;
                                            objinvoice2.OprationDay = Convert.ToInt32(lbldelday21.Text);

                                            objinvoice2.Total_price = Convert.ToDecimal(txtTotalPlanPrice.Text);
                                            //Calculation Field
                                            if (txtBeingDate.Text != "")
                                                objinvoice2.StartDate = Convert.ToDateTime(txtBeingDate.Text);
                                            if (txtEndDate.Text != "")
                                                objinvoice2.EndDate = Convert.ToDateTime(txtEndDate.Text);
                                            objinvoice2.WeekofDay = tags_1.Text;
                                            objinvoice2.Qty = Convert.ToInt32(txtqty2.Text); ;
                                            objinvoice2.TotalWeek = Convert.ToInt32(txtTotalWeek.Text);
                                            objinvoice2.NoOfWeek = Convert.ToInt32(DrpWeek.SelectedValue);
                                            objinvoice2.TotalDeliveryDay = Convert.ToInt32(txtTotalDeliveryDay.Text);
                                            objinvoice2.ActualDeliveryDay = Convert.ToInt32(txtDelivered.Text);
                                            objinvoice2.ExpectedDeliveryDay = Convert.ToInt32(txtRemainingDay.Text);
                                            //objinvoice2.DeliveryTime = Convert.ToInt32(drpDeliveryTime.SelectedValue);

                                            objinvoice2.DeliveryTime = ListTime.Count() > 0 ? Convert.ToInt32(ListTime.FirstOrDefault().DeliveryTime) : Convert.ToInt32(lblDeliveryTIMEID.Text);
                                            objinvoice2.DeliveryMeal = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
                                            objinvoice2.WeekofDay = tags_1.Text;
                                            //End
                                            //objplanmealcustinvoice.ExpectedDelDate = Convert.ToDateTime(txtExpDate.Text);
                                            objinvoice2.MYPRODID = Convert.ToInt64(lblProd.Text);

                                            objinvoice2.ItemWeight = Convert.ToDecimal(txtWeight.Text);
                                            objinvoice2.Calories = Convert.ToDecimal(txtCalories.Text);
                                            objinvoice2.Protein = Convert.ToDecimal(txtProtain.Text);
                                            objinvoice2.Carbs = Convert.ToDecimal(txtCarbs.Text);
                                            objinvoice2.Fat = Convert.ToDecimal(txtFat.Text);

                                            objinvoice2.Item_cost = Convert.ToDecimal(txtsaleprice.Text);
                                            objinvoice2.DriverID = Convert.ToInt32(drpExpecteddriver.SelectedValue);
                                            objinvoice2.ACTIVE = true;
                                            objinvoice2.ShortRemark = "TRUE";
                                            //crupID = Convert.ToInt32(objinvoice.CRUP_ID);
                                            DB.SaveChanges();
                                            //string Logdelete = " MYTRANSID=" + MYTRANSID + ", CustomerID=" + CID + ", planid=" + planid + ", DeliveryMeal=" + Meal + ", MYPRODID=" + PID;
                                            //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoice,Update:" + Logdelete, crupID, "planmealcustinvoice", UID.ToString());
                                        }
                                    }
                                    else
                                    {
                                        DateTime lbldate2 = Convert.ToDateTime(lblFDate2.Text);
                                        if (lbldate2 <= EndDate)
                                        {
                                            adddt(MYTRANSID, lbldelday2, lblFDate2, lblDayName2, lbldelday21, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty2);
                                            DeliverySequence++;
                                        }
                                    }
                                }
                                else
                                {
                                    if (Val == "Sun")
                                    {
                                        if (ListInvoiceEdit.Where(p => p.OprationDay == 2).Count() > 0)
                                        {
                                            Database.planmealcustinvoice objinvoicedel2 = ListInvoiceEdit.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID && p.OprationDay == 2 && p.NoOfWeek == Week);
                                            //crupID = Convert.ToInt32(objinvoice.CRUP_ID);
                                            //DB.planmealcustinvoices.DeleteObject(objinvoice);
                                            //string Logdelete = " MYTRANSID=" + MYTRANSID + ", CustomerID=" + CID + ", planid=" + planid + ", DeliveryMeal=" + Meal + ", MYPRODID=" + PID;
                                            objinvoicedel2.ACTIVE = false;
                                            DB.SaveChanges();
                                            //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoice,Delete:" + Logdelete, crupID, "planmealcustinvoice", UID.ToString());
                                        }
                                    }
                                }

                                if (RDODay3.Checked == true && Val == "Mon")
                                {
                                    DateTime Exp = Convert.ToDateTime(lblFDate3.Text);
                                    int Daynum = Convert.ToInt32(lbldelday31.Text);
                                    if (ListInvoiceEdit.Where(p => p.OprationDay == 3).Count() > 0)
                                    {
                                        if (ListInvoiceEdit.Where(p => p.OprationDay == 3 && p.DayNumber == Daynum && p.ExpectedDelDate == Exp && p.ActualDelDate == null).Count() > 0)
                                        {
                                            Database.planmealcustinvoice objinvoice3 = ListInvoiceEdit.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID && p.OprationDay == 3 && p.NoOfWeek == Week && p.DayNumber == Daynum && p.ExpectedDelDate == Exp);
                                            objinvoice3.DayNumber = Convert.ToInt32(lbldelday3.Text);
                                            objinvoice3.ExpectedDelDate = Convert.ToDateTime(lblFDate3.Text);
                                            DateTime nextday = Convert.ToDateTime(lblFDate3.Text);
                                            objinvoice3.NExtDeliveryDate = nextday.AddDays(1);
                                            objinvoice3.NameOfDay = lblDayName3.Text;
                                            objinvoice3.OprationDay = Convert.ToInt32(lbldelday31.Text);

                                            objinvoice3.Total_price = Convert.ToDecimal(txtTotalPlanPrice.Text);
                                            //Calculation Field
                                            if (txtBeingDate.Text != "")
                                                objinvoice3.StartDate = Convert.ToDateTime(txtBeingDate.Text);
                                            if (txtEndDate.Text != "")
                                                objinvoice3.EndDate = Convert.ToDateTime(txtEndDate.Text);
                                            objinvoice3.WeekofDay = tags_1.Text;
                                            objinvoice3.Qty = Convert.ToInt32(txtqty3.Text); ;
                                            objinvoice3.TotalWeek = Convert.ToInt32(txtTotalWeek.Text);
                                            objinvoice3.NoOfWeek = Convert.ToInt32(DrpWeek.SelectedValue);
                                            objinvoice3.TotalDeliveryDay = Convert.ToInt32(txtTotalDeliveryDay.Text);
                                            objinvoice3.ActualDeliveryDay = Convert.ToInt32(txtDelivered.Text);
                                            objinvoice3.ExpectedDeliveryDay = Convert.ToInt32(txtRemainingDay.Text);
                                            //objinvoice3.DeliveryTime = Convert.ToInt32(drpDeliveryTime.SelectedValue);
                                            objinvoice3.DeliveryTime = ListTime.Count() > 0 ? Convert.ToInt32(ListTime.FirstOrDefault().DeliveryTime) : Convert.ToInt32(lblDeliveryTIMEID.Text);
                                            objinvoice3.DeliveryMeal = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
                                            objinvoice3.WeekofDay = tags_1.Text;
                                            //End
                                            //objplanmealcustinvoice.ExpectedDelDate = Convert.ToDateTime(txtExpDate.Text);
                                            objinvoice3.MYPRODID = Convert.ToInt64(lblProd.Text);

                                            objinvoice3.ItemWeight = Convert.ToDecimal(txtWeight.Text);
                                            objinvoice3.Calories = Convert.ToDecimal(txtCalories.Text);
                                            objinvoice3.Protein = Convert.ToDecimal(txtProtain.Text);
                                            objinvoice3.Carbs = Convert.ToDecimal(txtCarbs.Text);
                                            objinvoice3.Fat = Convert.ToDecimal(txtFat.Text);

                                            objinvoice3.Item_cost = Convert.ToDecimal(txtsaleprice.Text);
                                            objinvoice3.DriverID = Convert.ToInt32(drpExpecteddriver.SelectedValue);
                                            objinvoice3.ACTIVE = true;
                                            objinvoice3.ShortRemark = "TRUE";
                                            //crupID = Convert.ToInt32(objinvoice.CRUP_ID);
                                            DB.SaveChanges();
                                            //string Logdelete = " MYTRANSID=" + MYTRANSID + ", CustomerID=" + CID + ", planid=" + planid + ", DeliveryMeal=" + Meal + ", MYPRODID=" + PID;
                                            //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoice,Update:" + Logdelete, crupID, "planmealcustinvoice", UID.ToString());
                                        }
                                    }
                                    else
                                    {
                                        DateTime lbldate3 = Convert.ToDateTime(lblFDate3.Text);
                                        if (lbldate3 <= EndDate)
                                        {
                                            adddt(MYTRANSID, lbldelday3, lblFDate3, lblDayName3, lbldelday31, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty3);
                                            DeliverySequence++;
                                        }
                                    }
                                }
                                else
                                {
                                    if (Val == "Mon")
                                    {
                                        if (ListInvoiceEdit.Where(p => p.OprationDay == 3).Count() > 0)
                                        {
                                            Database.planmealcustinvoice objinvoicedel3 = ListInvoiceEdit.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID && p.OprationDay == 3 && p.NoOfWeek == Week);
                                            //crupID = Convert.ToInt32(objinvoice.CRUP_ID);
                                            //DB.planmealcustinvoices.DeleteObject(objinvoice);
                                            //string Logdelete = " MYTRANSID=" + MYTRANSID + ", CustomerID=" + CID + ", planid=" + planid + ", DeliveryMeal=" + Meal + ", MYPRODID=" + PID;
                                            objinvoicedel3.ACTIVE = false;
                                            DB.SaveChanges();
                                            //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoice,Delete:" + Logdelete, crupID, "planmealcustinvoice", UID.ToString());
                                        }
                                    }

                                }

                                if (RDODay4.Checked == true && Val == "Tue")
                                {
                                    DateTime Exp = Convert.ToDateTime(lblFDate4.Text);
                                    int Daynum = Convert.ToInt32(lbldelday41.Text);
                                    if (ListInvoiceEdit.Where(p => p.OprationDay == 4).Count() > 0)
                                    {
                                        if (ListInvoiceEdit.Where(p => p.OprationDay == 4 && p.DayNumber == Daynum && p.ExpectedDelDate == Exp && p.ActualDelDate == null).Count() > 0)
                                        {
                                            Database.planmealcustinvoice objinvoice4 = ListInvoiceEdit.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID && p.OprationDay == 4 && p.NoOfWeek == Week && p.DayNumber == Daynum && p.ExpectedDelDate == Exp);
                                            objinvoice4.DayNumber = Convert.ToInt32(lbldelday4.Text);
                                            objinvoice4.ExpectedDelDate = Convert.ToDateTime(lblFDate4.Text);
                                            DateTime nextday = Convert.ToDateTime(lblFDate4.Text);
                                            objinvoice4.NExtDeliveryDate = nextday.AddDays(1);
                                            objinvoice4.NameOfDay = lblDayName4.Text;
                                            objinvoice4.OprationDay = Convert.ToInt32(lbldelday41.Text);

                                            objinvoice4.Total_price = Convert.ToDecimal(txtTotalPlanPrice.Text);
                                            //Calculation Field
                                            if (txtBeingDate.Text != "")
                                                objinvoice4.StartDate = Convert.ToDateTime(txtBeingDate.Text);
                                            if (txtEndDate.Text != "")
                                                objinvoice4.EndDate = Convert.ToDateTime(txtEndDate.Text);
                                            objinvoice4.WeekofDay = tags_1.Text;
                                            objinvoice4.Qty = Convert.ToInt32(txtqty4.Text); ;
                                            objinvoice4.TotalWeek = Convert.ToInt32(txtTotalWeek.Text);
                                            objinvoice4.NoOfWeek = Convert.ToInt32(DrpWeek.SelectedValue);
                                            objinvoice4.TotalDeliveryDay = Convert.ToInt32(txtTotalDeliveryDay.Text);
                                            objinvoice4.ActualDeliveryDay = Convert.ToInt32(txtDelivered.Text);
                                            objinvoice4.ExpectedDeliveryDay = Convert.ToInt32(txtRemainingDay.Text);
                                            //objinvoice4.DeliveryTime = Convert.ToInt32(drpDeliveryTime.SelectedValue);
                                            objinvoice4.DeliveryTime = ListTime.Count() > 0 ? Convert.ToInt32(ListTime.FirstOrDefault().DeliveryTime) : Convert.ToInt32(lblDeliveryTIMEID.Text);
                                            objinvoice4.DeliveryMeal = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
                                            objinvoice4.WeekofDay = tags_1.Text;
                                            //End
                                            //objplanmealcustinvoice.ExpectedDelDate = Convert.ToDateTime(txtExpDate.Text);
                                            objinvoice4.MYPRODID = Convert.ToInt64(lblProd.Text);

                                            objinvoice4.ItemWeight = Convert.ToDecimal(txtWeight.Text);
                                            objinvoice4.Calories = Convert.ToDecimal(txtCalories.Text);
                                            objinvoice4.Protein = Convert.ToDecimal(txtProtain.Text);
                                            objinvoice4.Carbs = Convert.ToDecimal(txtCarbs.Text);
                                            objinvoice4.Fat = Convert.ToDecimal(txtFat.Text);

                                            objinvoice4.Item_cost = Convert.ToDecimal(txtsaleprice.Text);
                                            objinvoice4.DriverID = Convert.ToInt32(drpExpecteddriver.SelectedValue);
                                            objinvoice4.ACTIVE = true;
                                            objinvoice4.ShortRemark = "TRUE";
                                            //crupID = Convert.ToInt32(objinvoice.CRUP_ID);
                                            DB.SaveChanges();
                                            //string Logdelete = " MYTRANSID=" + MYTRANSID + ", CustomerID=" + CID + ", planid=" + planid + ", DeliveryMeal=" + Meal + ", MYPRODID=" + PID;
                                            //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoice,Update:" + Logdelete, crupID, "planmealcustinvoice", UID.ToString());
                                        }
                                    }
                                    else
                                    {
                                        DateTime lbldate4 = Convert.ToDateTime(lblFDate4.Text);
                                        if (lbldate4 <= EndDate)
                                        {
                                            adddt(MYTRANSID, lbldelday4, lblFDate4, lblDayName4, lbldelday41, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty4);
                                            DeliverySequence++;
                                        }
                                    }
                                }
                                else
                                {
                                    if (Val == "Tue")
                                    {
                                        if (ListInvoiceEdit.Where(p => p.OprationDay == 4).Count() > 0)
                                        {
                                            Database.planmealcustinvoice objinvoicedel4 = ListInvoiceEdit.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID && p.OprationDay == 4 && p.NoOfWeek == Week);
                                            //crupID = Convert.ToInt32(objinvoice.CRUP_ID);
                                            //DB.planmealcustinvoices.DeleteObject(objinvoice);
                                            //string Logdelete = " MYTRANSID=" + MYTRANSID + ", CustomerID=" + CID + ", planid=" + planid + ", DeliveryMeal=" + Meal + ", MYPRODID=" + PID;
                                            objinvoicedel4.ACTIVE = false;
                                            DB.SaveChanges();
                                            //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoice,Delete:" + Logdelete, crupID, "planmealcustinvoice", UID.ToString());
                                        }
                                    }

                                }

                                if (RDODay5.Checked == true && Val == "Wed")
                                {
                                    DateTime Exp = Convert.ToDateTime(lblFDate5.Text);
                                    int Daynum = Convert.ToInt32(lbldelday51.Text);
                                    if (ListInvoiceEdit.Where(p => p.OprationDay == 5).Count() > 0)
                                    {
                                        if (ListInvoiceEdit.Where(p => p.OprationDay == 5 && p.DayNumber == Daynum && p.ExpectedDelDate == Exp && p.ActualDelDate == null).Count() > 0)
                                        {
                                            Database.planmealcustinvoice objinvoice5 = ListInvoiceEdit.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID && p.OprationDay == 5 && p.NoOfWeek == Week && p.DayNumber == Daynum && p.ExpectedDelDate == Exp);
                                            objinvoice5.DayNumber = Convert.ToInt32(lbldelday5.Text);
                                            objinvoice5.ExpectedDelDate = Convert.ToDateTime(lblFDate5.Text);
                                            DateTime nextday = Convert.ToDateTime(lblFDate5.Text);
                                            objinvoice5.NExtDeliveryDate = nextday.AddDays(1);
                                            objinvoice5.NameOfDay = lblDayName5.Text;
                                            objinvoice5.OprationDay = Convert.ToInt32(lbldelday51.Text);

                                            objinvoice5.Total_price = Convert.ToDecimal(txtTotalPlanPrice.Text);
                                            //Calculation Field
                                            if (txtBeingDate.Text != "")
                                                objinvoice5.StartDate = Convert.ToDateTime(txtBeingDate.Text);
                                            if (txtEndDate.Text != "")
                                                objinvoice5.EndDate = Convert.ToDateTime(txtEndDate.Text);
                                            objinvoice5.WeekofDay = tags_1.Text;
                                            objinvoice5.Qty = Convert.ToInt32(txtqty5.Text); ;
                                            objinvoice5.TotalWeek = Convert.ToInt32(txtTotalWeek.Text);
                                            objinvoice5.NoOfWeek = Convert.ToInt32(DrpWeek.SelectedValue);
                                            objinvoice5.TotalDeliveryDay = Convert.ToInt32(txtTotalDeliveryDay.Text);
                                            objinvoice5.ActualDeliveryDay = Convert.ToInt32(txtDelivered.Text);
                                            objinvoice5.ExpectedDeliveryDay = Convert.ToInt32(txtRemainingDay.Text);
                                            //objinvoice5.DeliveryTime = Convert.ToInt32(drpDeliveryTime.SelectedValue);
                                            objinvoice5.DeliveryTime = ListTime.Count() > 0 ? Convert.ToInt32(ListTime.FirstOrDefault().DeliveryTime) : Convert.ToInt32(lblDeliveryTIMEID.Text);
                                            objinvoice5.DeliveryMeal = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
                                            objinvoice5.WeekofDay = tags_1.Text;
                                            //End
                                            //objplanmealcustinvoice.ExpectedDelDate = Convert.ToDateTime(txtExpDate.Text);
                                            objinvoice5.MYPRODID = Convert.ToInt64(lblProd.Text);

                                            objinvoice5.ItemWeight = Convert.ToDecimal(txtWeight.Text);
                                            objinvoice5.Calories = Convert.ToDecimal(txtCalories.Text);
                                            objinvoice5.Protein = Convert.ToDecimal(txtProtain.Text);
                                            objinvoice5.Carbs = Convert.ToDecimal(txtCarbs.Text);
                                            objinvoice5.Fat = Convert.ToDecimal(txtFat.Text);

                                            objinvoice5.Item_cost = Convert.ToDecimal(txtsaleprice.Text);
                                            objinvoice5.DriverID = Convert.ToInt32(drpExpecteddriver.SelectedValue);
                                            objinvoice5.ACTIVE = true;
                                            objinvoice5.ShortRemark = "TRUE";
                                            //crupID = Convert.ToInt32(objinvoice.CRUP_ID);
                                            DB.SaveChanges();
                                            //string Logdelete = " MYTRANSID=" + MYTRANSID + ", CustomerID=" + CID + ", planid=" + planid + ", DeliveryMeal=" + Meal + ", MYPRODID=" + PID;
                                            //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoice,Update:" + Logdelete, crupID, "planmealcustinvoice", UID.ToString());
                                        }
                                    }
                                    else
                                    {
                                        DateTime lbldate5 = Convert.ToDateTime(lblFDate5.Text);
                                        if (lbldate5 <= EndDate)
                                        {
                                            adddt(MYTRANSID, lbldelday5, lblFDate5, lblDayName5, lbldelday51, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty5);
                                            DeliverySequence++;
                                        }
                                    }
                                }
                                else
                                {
                                    if (Val == "Wed")
                                    {
                                        if (ListInvoiceEdit.Where(p => p.OprationDay == 5).Count() > 0)
                                        {
                                            Database.planmealcustinvoice objinvoicedel5 = ListInvoiceEdit.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID && p.OprationDay == 5 && p.NoOfWeek == Week);
                                            //crupID = Convert.ToInt32(objinvoice.CRUP_ID);
                                            //DB.planmealcustinvoices.DeleteObject(objinvoice);
                                            //string Logdelete = " MYTRANSID=" + MYTRANSID + ", CustomerID=" + CID + ", planid=" + planid + ", DeliveryMeal=" + Meal + ", MYPRODID=" + PID;
                                            objinvoicedel5.ACTIVE = false;
                                            DB.SaveChanges();
                                            //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoice,Delete:" + Logdelete, crupID, "planmealcustinvoice", UID.ToString());
                                        }
                                    }

                                }

                                if (RDODay6.Checked == true && Val == "Thu")
                                {
                                    DateTime Exp = Convert.ToDateTime(lblFDate6.Text);
                                    int Daynum = Convert.ToInt32(lbldelday61.Text);
                                    if (ListInvoiceEdit.Where(p => p.OprationDay == 6).Count() > 0)
                                    {
                                        if (ListInvoiceEdit.Where(p => p.OprationDay == 6 && p.DayNumber == Daynum && p.ExpectedDelDate == Exp && p.ActualDelDate == null).Count() > 0)
                                        {
                                            Database.planmealcustinvoice objinvoice6 = ListInvoiceEdit.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID && p.OprationDay == 6 && p.NoOfWeek == Week && p.DayNumber == Daynum && p.ExpectedDelDate == Exp);
                                            objinvoice6.DayNumber = Convert.ToInt32(lbldelday6.Text);
                                            objinvoice6.ExpectedDelDate = Convert.ToDateTime(lblFDate6.Text);
                                            DateTime nextday = Convert.ToDateTime(lblFDate6.Text);
                                            objinvoice6.NExtDeliveryDate = nextday.AddDays(2);
                                            objinvoice6.NameOfDay = lblDayName6.Text;
                                            objinvoice6.OprationDay = Convert.ToInt32(lbldelday61.Text);

                                            objinvoice6.Total_price = Convert.ToDecimal(txtTotalPlanPrice.Text);
                                            //Calculation Field
                                            if (txtBeingDate.Text != "")
                                                objinvoice6.StartDate = Convert.ToDateTime(txtBeingDate.Text);
                                            if (txtEndDate.Text != "")
                                                objinvoice6.EndDate = Convert.ToDateTime(txtEndDate.Text);
                                            objinvoice6.WeekofDay = tags_1.Text;
                                            objinvoice6.Qty = Convert.ToInt32(txtqty6.Text); ;
                                            objinvoice6.TotalWeek = Convert.ToInt32(txtTotalWeek.Text);
                                            objinvoice6.NoOfWeek = Convert.ToInt32(DrpWeek.SelectedValue);
                                            objinvoice6.TotalDeliveryDay = Convert.ToInt32(txtTotalDeliveryDay.Text);
                                            objinvoice6.ActualDeliveryDay = Convert.ToInt32(txtDelivered.Text);
                                            objinvoice6.ExpectedDeliveryDay = Convert.ToInt32(txtRemainingDay.Text);
                                            //objinvoice6.DeliveryTime = Convert.ToInt32(drpDeliveryTime.SelectedValue);
                                            objinvoice6.DeliveryTime = ListTime.Count() > 0 ? Convert.ToInt32(ListTime.FirstOrDefault().DeliveryTime) : Convert.ToInt32(lblDeliveryTIMEID.Text);
                                            objinvoice6.DeliveryMeal = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
                                            objinvoice6.WeekofDay = tags_1.Text;
                                            //End
                                            //objplanmealcustinvoice.ExpectedDelDate = Convert.ToDateTime(txtExpDate.Text);
                                            objinvoice6.MYPRODID = Convert.ToInt64(lblProd.Text);

                                            objinvoice6.ItemWeight = Convert.ToDecimal(txtWeight.Text);
                                            objinvoice6.Calories = Convert.ToDecimal(txtCalories.Text);
                                            objinvoice6.Protein = Convert.ToDecimal(txtProtain.Text);
                                            objinvoice6.Carbs = Convert.ToDecimal(txtCarbs.Text);
                                            objinvoice6.Fat = Convert.ToDecimal(txtFat.Text);

                                            objinvoice6.Item_cost = Convert.ToDecimal(txtsaleprice.Text);
                                            objinvoice6.DriverID = Convert.ToInt32(drpExpecteddriver.SelectedValue);
                                            objinvoice6.ACTIVE = true;
                                            objinvoice6.ShortRemark = "TRUE";
                                            //crupID = Convert.ToInt32(objinvoice.CRUP_ID);
                                            DB.SaveChanges();
                                            //string Logdelete = " MYTRANSID=" + MYTRANSID + ", CustomerID=" + CID + ", planid=" + planid + ", DeliveryMeal=" + Meal + ", MYPRODID=" + PID;
                                            //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoice,Update:" + Logdelete, crupID, "planmealcustinvoice", UID.ToString());
                                        }
                                    }
                                    else
                                    {
                                        DateTime lbldate6 = Convert.ToDateTime(lblFDate6.Text);
                                        if (lbldate6 <= EndDate)
                                        {
                                            adddt(MYTRANSID, lbldelday6, lblFDate6, lblDayName6, lbldelday61, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty6);
                                            DeliverySequence++;
                                        }
                                    }
                                }
                                else
                                {
                                    if (Val == "Thu")
                                    {
                                        if (ListInvoiceEdit.Where(p => p.OprationDay == 6).Count() > 0)
                                        {
                                            Database.planmealcustinvoice objinvoicedel6 = ListInvoiceEdit.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID && p.OprationDay == 6 && p.NoOfWeek == Week);
                                            //crupID = Convert.ToInt32(objinvoice.CRUP_ID);
                                            //DB.planmealcustinvoices.DeleteObject(objinvoice);
                                            //string Logdelete = " MYTRANSID=" + MYTRANSID + ", CustomerID=" + CID + ", planid=" + planid + ", DeliveryMeal=" + Meal + ", MYPRODID=" + PID;
                                            objinvoicedel6.ACTIVE = false;
                                            DB.SaveChanges();
                                            //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoice,Delete:" + Logdelete, crupID, "planmealcustinvoice", UID.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            string[] days1 = tags_1.Text.Split(',');
                            foreach (string Val in days1)
                            {
                                if (RDODay1.Checked == true && Val == "Sat")
                                {
                                    DateTime lbldate1 = Convert.ToDateTime(lblFDate1.Text);
                                    if (lbldate1 <= EndDate)
                                    {
                                        adddt(MYTRANSID, lbldelday1, lblFDate1, lblDayName1, lbldelday11, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty1);
                                        DeliverySequence++;
                                    }
                                }
                                if (RDODay2.Checked == true && Val == "Sun")
                                {
                                    DateTime lbldate2 = Convert.ToDateTime(lblFDate2.Text);
                                    if (lbldate2 <= EndDate)
                                    {
                                        adddt(MYTRANSID, lbldelday2, lblFDate2, lblDayName2, lbldelday21, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty2);
                                        DeliverySequence++;
                                    }
                                }
                                if (RDODay3.Checked == true && Val == "Mon")
                                {
                                    DateTime lbldate3 = Convert.ToDateTime(lblFDate3.Text);
                                    if (lbldate3 <= EndDate)
                                    {
                                        adddt(MYTRANSID, lbldelday3, lblFDate3, lblDayName3, lbldelday31, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty3);
                                        DeliverySequence++;
                                    }
                                }
                                if (RDODay4.Checked == true && Val == "Tue")
                                {
                                    DateTime lbldate4 = Convert.ToDateTime(lblFDate4.Text);
                                    if (lbldate4 <= EndDate)
                                    {
                                        adddt(MYTRANSID, lbldelday4, lblFDate4, lblDayName4, lbldelday41, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty4);
                                        DeliverySequence++;
                                    }
                                }
                                if (RDODay5.Checked == true && Val == "Wed")
                                {
                                    DateTime lbldate5 = Convert.ToDateTime(lblFDate5.Text);
                                    if (lbldate5 <= EndDate)
                                    {
                                        adddt(MYTRANSID, lbldelday5, lblFDate5, lblDayName5, lbldelday51, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty5);
                                        DeliverySequence++;
                                    }
                                }
                                if (RDODay6.Checked == true && Val == "Thu")
                                {
                                    DateTime lbldate6 = Convert.ToDateTime(lblFDate6.Text);
                                    if (lbldate6 <= EndDate)
                                    {
                                        adddt(MYTRANSID, lbldelday6, lblFDate6, lblDayName6, lbldelday61, lblProd, txtproduct, txtWeight, txtCalories, txtProtain, txtCarbs, txtFat, txtsaleprice, DeliverySequence, txtqty6);
                                        DeliverySequence++;
                                    }
                                }
                            }
                        }
                    }

                    if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ACTIVE == false).Count() > 0)
                    {
                        string Str = "delete from planmealcustinvoice where TenentID=" + TID + " and MYTRANSID=" + MYTRANSID + " and ACTIVE='False'";
                        command2 = new SqlCommand(Str, con);
                        con.Open();
                        command2.ExecuteReader();
                        con.Close();
                    }

                    Editinvoice(CID, MYTRANSID, planid, Meal);
                }


            }
            BindData();
            //    scope.Complete(); //  To commit.

            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}

            // btnAdd.Attributes["id"] = "btnAdd";

            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "please wait... ", "stopProgress()", true);
        }

        public string getProdname(long PID)
        {
            int MYTRANSID = ViewState["MYTRANSID"] != null ? Convert.ToInt32(ViewState["MYTRANSID"]) : 0;
            if (PID != 0)
            {
                List<Database.planmealcustinvoice> Listprod = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID).ToList();
                if (Listprod.Count() > 0)
                {
                    Database.planmealcustinvoice objinv = Listprod.Where(p => p.MYTRANSID == MYTRANSID && p.MYPRODID == PID).FirstOrDefault();
                    if (objinv.ProdName1 != null && objinv.ProdName1 != "")
                    {
                        string prodname = objinv.ProdName1;
                        return prodname;
                    }
                    else
                    {
                        List<Database.TBLPRODUCT> Listtblprod = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).ToList();
                        if (Listtblprod.Count() > 0)
                        {
                            return Listtblprod.Single(p => p.TenentID == TID && p.MYPRODID == PID).ProdName1;
                        }
                        else
                        {
                            return "Record Not Found";
                        }
                    }
                }
                else
                {
                    List<Database.TBLPRODUCT> Listtblprod = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PID).ToList();
                    if (Listtblprod.Count() > 0)
                    {
                        return Listtblprod.Single(p => p.TenentID == TID && p.MYPRODID == PID).ProdName1;
                    }
                    else
                    {
                        return "Record Not Found";
                    }
                }
            }
            else
            {
                return "Record Not Found";
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Session["Previous"].ToString());
            switch1 = "AddOff";
            Response.Redirect("/Master/HBInvoice.aspx");
        }
        public void FillContractorID()
        {
            List<Database.TBLCOMPANYSETUP> ListCustomer = new List<Database.TBLCOMPANYSETUP>();
            List<Database.tblcontact_addon1> FinelCustomer = DB.tblcontact_addon1.Where(p => p.TenentID == TID && p.active == true).ToList();
            List<Database.tblcontact_addon1_dtl> ListDtl1 = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID).ToList();
            List<Database.TBLCOMPANYSETUP> ListComp = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).ToList();
            foreach (Database.tblcontact_addon1 Citem in FinelCustomer)
            {
                if (FinelCustomer.Where(p => p.TenentID == TID && p.CustomerId == Citem.CustomerId && p.active == true).Count() > 0)
                {
                    List<Database.tblcontact_addon1_dtl> ListDtl = ListDtl1.Where(p => p.TenentID == TID && p.customerID == Citem.CustomerId && p.LikeType == "M" && p.planid != null && p.planid != 0).ToList();
                    if (ListDtl.Count() > 0)
                    {
                        Database.TBLCOMPANYSETUP Cobj = ListComp.Single(p => p.TenentID == TID && p.COMPID == Citem.CustomerId);
                        ListCustomer.Add(Cobj);
                    }
                }
            }
            drpCustomer.DataSource = ListCustomer.OrderBy(p => p.COMPNAME1);// DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID);
            drpCustomer.DataTextField = "COMPNAME1";
            drpCustomer.DataValueField = "COMPID";
            drpCustomer.DataBind();
            drpCustomer.Items.Insert(0, new ListItem("-- Select --", "0"));

            List<Database.REFTABLE> ListREFTABLE = new List<Database.REFTABLE>();
            List<Database.TBLPRODUCT> ListTBLPRODUCT = new List<Database.TBLPRODUCT>();

            List<Database.tblProduct_Plan> ListtblProduct_Plan = new List<Database.tblProduct_Plan>();
            List<Database.planmealsetup> Listplanmealsetup = DB.planmealsetups.Where(p => p.ACTIVE == true && p.TenentID == TID).ToList();
            List<Database.tblProduct_Plan> ListtblProduct = DB.tblProduct_Plan.Where(p => p.TenentID == TID).ToList();
            foreach (Database.planmealsetup item in Listplanmealsetup.GroupBy(p => p.planid).Select(p => p.FirstOrDefault()))
            {
                if (ListtblProduct.Where(p => p.planid == item.planid && p.TenentID == TID).Count() > 0)
                {
                    Database.tblProduct_Plan obj = ListtblProduct.Single(p => p.planid == item.planid && p.TenentID == TID);
                    ListtblProduct_Plan.Add(obj);
                }
            }

            drpPlan.DataSource = ListtblProduct_Plan.OrderBy(p => p.planname1);
            drpPlan.DataTextField = "planname1";
            drpPlan.DataValueField = "planid";
            drpPlan.DataBind();
            //if (ListtblProduct_Plan.Count() == 0) drpPlan.Items.Insert(0, new ListItem("-- Select --", "0"));
            drpPlan.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpMeal.DataSource = ListREFTABLE;
            //drpMeal.DataTextField = "REFNAME1";
            //drpMeal.DataValueField = "REFID";
            //drpMeal.DataBind();
            if (ListREFTABLE.Count() == 0) drpMeal.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpProtein.DataSource = ListTBLPRODUCT;
            //drpProtein.DataTextField = "ProdName1";
            //drpProtein.DataValueField = "MYPRODID";
            //drpProtein.DataBind();
            //if (ListTBLPRODUCT.Count() == 0) drpProtein.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpExpecteddriver.DataSource = DB.tbl_Employee.Where(p => p.TenentID == TID && p.EmployeeType == "Driver").OrderBy(p => p.firstname);
            drpExpecteddriver.DataTextField = "firstname";
            drpExpecteddriver.DataValueField = "employeeID";
            drpExpecteddriver.DataBind();
            //if (DB.tbl_Employee.Where(p => p.TenentID == TID).Count() == 0) drpExpecteddriver.Items.Insert(0, new ListItem("-- Select --", "0"));
            drpExpecteddriver.Items.Insert(0, new ListItem("-- Select --", "0"));
            lblDeliveryTIMEID.Text = "0";
            //drpDeliveryTime.Items.Insert(0, new ListItem("-- Select --", "0"));
            drpDeliveryMeal.Items.Insert(0, new ListItem("-- Select --", "0"));
            drpHowManyDay.Items.Insert(0, new ListItem("0", "0"));
            //DrpWeek.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            FirstData();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            NextData();
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            PrevData();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            LastData();
        }
        public void FirstData()
        {
            Database.planmealcustinvoice objplanmealcustinvoice = DB.planmealcustinvoices.OrderByDescending(p => p.StartDate).First();
            drpCustomer.SelectedValue = objplanmealcustinvoice.CustomerID.ToString();
            txtInvoiceNumber.Text = objplanmealcustinvoice.TransID.ToString();
            drpPlan.SelectedValue = objplanmealcustinvoice.planid.ToString();
            //bindMeal();
            GetProductList();
            drpMeal.SelectedValue = objplanmealcustinvoice.MealType.ToString();
            txtBeingDate.Text = objplanmealcustinvoice.ActualDelDate.ToString();

            txtEndDate.Text = objplanmealcustinvoice.ExpectedDelDate.ToString();

            if (txtBeingDate.Text != "" && txtEndDate.Text != "")
            {
                DateTime dt1 = Convert.ToDateTime(txtBeingDate.Text);
                DateTime dt2 = Convert.ToDateTime(txtEndDate.Text);
                TimeSpan TotalDeliveryDay = dt2.Subtract(dt1);
                txtTotalDeliveryDay.Text = TotalDeliveryDay.TotalDays.ToString();
            }
            drpDay.SelectedValue = objplanmealcustinvoice.DayNumber.ToString();
            // drpTotalPlanPrice.SelectedValue = objHBInvoice.TotalPlanPrice.ToString();
            // txtBlank.Text = objHBInvoice.Blank.ToString();
            Listview3.DataSource = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == objplanmealcustinvoice.CustomerID && p.DeliveryMeal == objplanmealcustinvoice.DeliveryMeal && p.planid == objplanmealcustinvoice.planid && p.ACTIVE == true).OrderBy(p => p.DayNumber);
            Listview3.DataBind();
        }

        public void NextData()
        {
            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;
                txtCustomer1s.Text = Listview1.SelectedDataKey[0].ToString();
                txtInvoiceNumber.Text = Listview1.SelectedDataKey[0].ToString();
                drpPlan.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtMeal1s.Text = Listview1.SelectedDataKey[0].ToString();
                txtBeingDate.Text = Listview1.SelectedDataKey[0].ToString();
                txtTotalDeliveryDay.Text = Listview1.SelectedDataKey[0].ToString();
                txtEndDate.Text = Listview1.SelectedDataKey[0].ToString();
                txtDay1s.Text = Listview1.SelectedDataKey[0].ToString();
                // drpTotalPlanPrice.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtBlank.Text = Listview1.SelectedDataKey[0].ToString();
            }
        }
        public void PrevData()
        {
            if (Listview1.SelectedIndex == 0)
            {
                lblMsg.Text = "This is first record";
                pnlSuccessMsg.Visible = true;

            }
            else
            {
                pnlSuccessMsg.Visible = false;
                Listview1.SelectedIndex = Listview1.SelectedIndex - 1;
                txtCustomer1s.Text = Listview1.SelectedDataKey[0].ToString();
                txtInvoiceNumber.Text = Listview1.SelectedDataKey[0].ToString();
                drpPlan.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                txtMeal1s.Text = Listview1.SelectedDataKey[0].ToString();
                txtBeingDate.Text = Listview1.SelectedDataKey[0].ToString();
                txtTotalDeliveryDay.Text = Listview1.SelectedDataKey[0].ToString();
                txtEndDate.Text = Listview1.SelectedDataKey[0].ToString();
                txtDay1s.Text = Listview1.SelectedDataKey[0].ToString();
                // drpTotalPlanPrice.SelectedValue = Listview1.SelectedDataKey[0].ToString();
                //txtBlank.Text = Listview1.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;
            txtCustomer1s.Text = Listview1.SelectedDataKey[0].ToString();
            txtInvoiceNumber.Text = Listview1.SelectedDataKey[0].ToString();
            drpPlan.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            txtMeal1s.Text = Listview1.SelectedDataKey[0].ToString();
            txtBeingDate.Text = Listview1.SelectedDataKey[0].ToString();
            txtTotalDeliveryDay.Text = Listview1.SelectedDataKey[0].ToString();
            txtEndDate.Text = Listview1.SelectedDataKey[0].ToString();
            txtDay1s.Text = Listview1.SelectedDataKey[0].ToString();
            // drpTotalPlanPrice.SelectedValue = Listview1.SelectedDataKey[0].ToString();
            //txtBlank.Text = Listview1.SelectedDataKey[0].ToString();
        }


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {
                    //2false
                    lblCustomer2h.Visible = lblInvoiceNumber2h.Visible = lblPlan2h.Visible = lblMeal2h.Visible = lblBeingDate2h.Visible = lblTotalDeliveryDay2h.Visible = lblEndDate2h.Visible = lblDay2h.Visible = lblTotalPlanPrice2h.Visible = false;//lblBlank2h.Visible = 
                    //2true
                    txtCustomer2h.Visible = txtInvoiceNumber2h.Visible = txtPlan2h.Visible = txtMeal2h.Visible = txtBeingDate2h.Visible = txtTotalDeliveryDay2h.Visible = txtEndDate2h.Visible = txtDay2h.Visible = txtTotalPlanPrice2h.Visible = true;//txtBlank2h.Visible = 

                    //header
                    lblHeader.Visible = false;
                    txtHeader.Visible = true;
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());

                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //2true
                    lblCustomer2h.Visible = lblInvoiceNumber2h.Visible = lblPlan2h.Visible = lblMeal2h.Visible = lblBeingDate2h.Visible = lblTotalDeliveryDay2h.Visible = lblEndDate2h.Visible = lblDay2h.Visible = lblTotalPlanPrice2h.Visible = true;//lblBlank2h.Visible = 
                    //2false
                    txtCustomer2h.Visible = txtInvoiceNumber2h.Visible = txtPlan2h.Visible = txtMeal2h.Visible = txtBeingDate2h.Visible = txtTotalDeliveryDay2h.Visible = txtEndDate2h.Visible = txtDay2h.Visible = txtTotalPlanPrice2h.Visible = false;//txtBlank2h.Visible = 

                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
            else
            {
                if (btnEditLable.Text == "Update Label")
                {
                    //1false
                    lblCustomer1s.Visible = lblInvoiceNumber1s.Visible = lblPlan1s.Visible = lblMeal1s.Visible = lblBeingDate1s.Visible = lblTotalDeliveryDay1s.Visible = lblEndDate1s.Visible = lblDay1s.Visible = lblTotalPlanPrice1s.Visible = false;//lblBlank1s.Visible = 
                    //1true
                    txtCustomer1s.Visible = txtInvoiceNumber1s.Visible = txtPlan1s.Visible = txtMeal1s.Visible = txtBeingDate1s.Visible = txtTotalDeliveryDay1s.Visible = txtEndDate1s.Visible = txtDay1s.Visible = txtTotalPlanPrice1s.Visible = true;//txtBlank1s.Visible = 
                    //header
                    lblHeader.Visible = false;
                    txtHeader.Visible = true;
                    btnEditLable.Text = "Save Label";
                }
                else
                {
                    SaveLabel(Session["LANGUAGE"].ToString());
                    ManageLang();
                    btnEditLable.Text = "Update Label";
                    //1true
                    lblCustomer1s.Visible = lblInvoiceNumber1s.Visible = lblPlan1s.Visible = lblMeal1s.Visible = lblBeingDate1s.Visible = lblTotalDeliveryDay1s.Visible = lblEndDate1s.Visible = lblDay1s.Visible = lblTotalPlanPrice1s.Visible = true;//lblBlank1s.Visible =
                    //1false
                    txtCustomer1s.Visible = txtInvoiceNumber1s.Visible = txtPlan1s.Visible = txtMeal1s.Visible = txtBeingDate1s.Visible = txtTotalDeliveryDay1s.Visible = txtEndDate1s.Visible = txtDay1s.Visible = txtTotalPlanPrice1s.Visible = false;//txtBlank1s.Visible = 
                    //header
                    lblHeader.Visible = true;
                    txtHeader.Visible = false;
                }
            }
        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((AcmMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("HBInvoice").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblCustomer1s.ID == item.LabelID)
                    txtCustomer1s.Text = lblCustomer1s.Text = lblhCustomer.Text = item.LabelName;
                else if (lblInvoiceNumber1s.ID == item.LabelID)
                    txtInvoiceNumber1s.Text = lblInvoiceNumber1s.Text = item.LabelName;
                else if (lblPlan1s.ID == item.LabelID)
                    txtPlan1s.Text = lblPlan1s.Text = lblhPlan.Text = item.LabelName;
                else if (lblMeal1s.ID == item.LabelID)
                    txtMeal1s.Text = lblMeal1s.Text = lblhMeal.Text = item.LabelName;
                else if (lblBeingDate1s.ID == item.LabelID)
                    txtBeingDate1s.Text = lblBeingDate1s.Text = item.LabelName;
                else if (lblTotalDeliveryDay1s.ID == item.LabelID)
                    txtTotalDeliveryDay1s.Text = lblTotalDeliveryDay1s.Text = item.LabelName;
                else if (lblEndDate1s.ID == item.LabelID)
                    txtEndDate1s.Text = lblEndDate1s.Text = item.LabelName;
                else if (lblDay1s.ID == item.LabelID)
                    txtDay1s.Text = lblDay1s.Text = lblhDay.Text = item.LabelName;
                else if (lblTotalPlanPrice1s.ID == item.LabelID)
                    txtTotalPlanPrice1s.Text = lblTotalPlanPrice1s.Text = item.LabelName;
                else if (lblCustomer2h.ID == item.LabelID)
                    txtCustomer2h.Text = lblCustomer2h.Text = lblhCustomer.Text = item.LabelName;
                else if (lblInvoiceNumber2h.ID == item.LabelID)
                    txtInvoiceNumber2h.Text = lblInvoiceNumber2h.Text = item.LabelName;
                else if (lblPlan2h.ID == item.LabelID)
                    txtPlan2h.Text = lblPlan2h.Text = lblhPlan.Text = item.LabelName;
                else if (lblMeal2h.ID == item.LabelID)
                    txtMeal2h.Text = lblMeal2h.Text = lblhMeal.Text = item.LabelName;
                else if (lblBeingDate2h.ID == item.LabelID)
                    txtBeingDate2h.Text = lblBeingDate2h.Text = item.LabelName;
                else if (lblTotalDeliveryDay2h.ID == item.LabelID)
                    txtTotalDeliveryDay2h.Text = lblTotalDeliveryDay2h.Text = item.LabelName;
                else if (lblEndDate2h.ID == item.LabelID)
                    txtEndDate2h.Text = lblEndDate2h.Text = item.LabelName;
                else if (lblDay2h.ID == item.LabelID)
                    txtDay2h.Text = lblDay2h.Text = lblhDay.Text = item.LabelName;
                else if (lblTotalPlanPrice2h.ID == item.LabelID)
                    txtTotalPlanPrice2h.Text = lblTotalPlanPrice2h.Text = item.LabelName;
                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("HBInvoice").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\HBInvoice.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {
                var obj = ((AcmMaster)this.Master).Bindxml("HBInvoice").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblCustomer1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCustomer1s.Text;
                else if (lblInvoiceNumber1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtInvoiceNumber1s.Text;
                else if (lblPlan1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlan1s.Text;
                else if (lblMeal1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMeal1s.Text;
                else if (lblBeingDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBeingDate1s.Text;
                else if (lblTotalDeliveryDay1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotalDeliveryDay1s.Text;
                else if (lblEndDate1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEndDate1s.Text;
                else if (lblDay1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDay1s.Text;
                else if (lblTotalPlanPrice1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotalPlanPrice1s.Text;
                //else if (lblBlank1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtBlank1s.Text;
                else if (lblCustomer2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCustomer2h.Text;
                else if (lblInvoiceNumber2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtInvoiceNumber2h.Text;
                else if (lblPlan2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlan2h.Text;
                else if (lblMeal2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMeal2h.Text;
                else if (lblBeingDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtBeingDate2h.Text;
                else if (lblTotalDeliveryDay2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotalDeliveryDay2h.Text;
                else if (lblEndDate2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtEndDate2h.Text;
                else if (lblDay2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtDay2h.Text;
                else if (lblTotalPlanPrice2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtTotalPlanPrice2h.Text;
                //else if (lblBlank2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtBlank2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\HBInvoice.xml"));
        }
        public void ManageLang()
        {
            //for Language

            if (Session["LANGUAGE"] != null)
            {
                RecieveLabel(Session["LANGUAGE"].ToString());
                if (Session["LANGUAGE"].ToString() == "ar-KW")
                    GetHide();
                else
                    GetShow();
            }

        }
        protected void LanguageFrance_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "fr-FR";
            ManageLang();
        }
        protected void LanguageArabic_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "ar-KW";
            ManageLang();
        }
        protected void LanguageEnglish_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "en-US";
            ManageLang();
        }
        public void Write()
        {
            lblpagemode.Text = "[Mode:Write]";
            // navigation.Visible = false;
            drpCustomer.Enabled = true;
            //txtInvoiceNumber.Enabled = true;
            drpPlan.Enabled = true;
            drpMeal.Enabled = true;
            drpDay.Enabled = true;
            txtBeingDate.Enabled = true;
            //txtTotalDeliveryDay.Enabled = true;
            txtEndDate.Enabled = true;
            txtDay1s.Enabled = true;
            txtTotalPlanPrice.Enabled = true;
            drpExpecteddriver.Enabled = true;
            //txtDelivered.Enabled = true;
            //drpProtein.Enabled = true;
            //txtWeight.Enabled = true;
            //txtsaleprice.Enabled = true;
            //drpProductListDay.Enabled = true;
            //txtTotalWeek.Enabled = true;
            //drpDeliveryTime.Enabled = true;
            drpDeliveryMeal.Enabled = true;
            drpweekofday.Enabled = true;
            drpHowManyDay.Enabled = true;
            tags_1.Enabled = true;
            if (btnAdd.Text != "Update")
            {
                Listview3.DataSource = null;
                Listview3.DataBind();
            }
        }
        public void Readonly()
        {
            lblpagemode.Text = "[Mode:View]";
            drpCustomer.Enabled = false;
            txtInvoiceNumber.Enabled = false;
            drpPlan.Enabled = false;
            drpMeal.Enabled = false;
            drpDay.Enabled = false;
            txtBeingDate.Enabled = false;
            txtTotalDeliveryDay.Enabled = false;
            txtEndDate.Enabled = false;
            txtDay1s.Enabled = false;
            txtTotalPlanPrice.Enabled = false;
            drpExpecteddriver.Enabled = false;
            txtDelivered.Enabled = false;
            //drpProtein.Enabled = false;
            //txtWeight.Enabled = false;
            //txtsaleprice.Enabled = false;
            //drpProductListDay.Enabled = false;
            txtTotalWeek.Enabled = false;
            //drpDeliveryTime.Enabled = false;
            drpDeliveryMeal.Enabled = false;
            drpweekofday.Enabled = false;
            drpHowManyDay.Enabled = false;
            tags_1.Enabled = false;
        }

        public void ReadonlyFull()
        {
            lblpagemode.Text = "[Mode:View]";
            drpCustomer.Enabled = false;
            txtInvoiceNumber.Enabled = false;
            drpPlan.Enabled = false;
            drpMeal.Enabled = false;
            drpDay.Enabled = false;
            txtBeingDate.Enabled = false;
            txtTotalDeliveryDay.Enabled = false;
            txtEndDate.Enabled = false;
            txtDay1s.Enabled = false;
            txtTotalPlanPrice.Enabled = false;
            drpExpecteddriver.Enabled = false;
            txtDelivered.Enabled = false;
            //drpProtein.Enabled = false;
            //txtWeight.Enabled = false;
            //txtsaleprice.Enabled = false;
            //drpProductListDay.Enabled = false;
            txtTotalWeek.Enabled = false;
            //drpDeliveryTime.Enabled = false;
            drpDeliveryMeal.Enabled = false;
            drpweekofday.Enabled = false;
            drpHowManyDay.Enabled = false;
            tags_1.Enabled = false;
            txtDelTotalDay.Enabled = false;
            btnDateSet.Enabled = false;
            btnCopyFullPlan.Enabled = false;
            btnRecalculate.Enabled = false;
            btnAddmeal.Enabled = false;
            btnAdd.Enabled = false;
            DrpWeek.Enabled = false;
            btnChengeDriver.Enabled = false;
            //btntimeChange.Enabled = false;

            for (int i = 0; i < ListView2.Items.Count; i++)
            {
                CheckBox RDODay1 = (CheckBox)ListView2.Items[i].FindControl("RDODay1");
                CheckBox RDODay2 = (CheckBox)ListView2.Items[i].FindControl("RDODay2");
                CheckBox RDODay3 = (CheckBox)ListView2.Items[i].FindControl("RDODay3");
                CheckBox RDODay4 = (CheckBox)ListView2.Items[i].FindControl("RDODay4");
                CheckBox RDODay5 = (CheckBox)ListView2.Items[i].FindControl("RDODay5");
                CheckBox RDODay6 = (CheckBox)ListView2.Items[i].FindControl("RDODay6");
                TextBox txtproduct = (TextBox)ListView2.Items[i].FindControl("txtproduct");
                TextBox txtCalories = (TextBox)ListView2.Items[i].FindControl("txtCalories");
                TextBox txtProtain = (TextBox)ListView2.Items[i].FindControl("txtProtain");
                TextBox txtCarbs = (TextBox)ListView2.Items[i].FindControl("txtCarbs");
                TextBox txtFat = (TextBox)ListView2.Items[i].FindControl("txtFat");
                TextBox txtWeight = (TextBox)ListView2.Items[i].FindControl("txtWeight");
                TextBox txtsaleprice = (TextBox)ListView2.Items[i].FindControl("txtsaleprice");
                LinkButton lnkbtnQty = (LinkButton)ListView2.Items[i].FindControl("lnkbtnQty");

                RDODay1.Enabled = RDODay2.Enabled = RDODay3.Enabled = RDODay4.Enabled = RDODay5.Enabled = RDODay6.Enabled =
                txtproduct.Enabled = txtCalories.Enabled = txtProtain.Enabled = txtCarbs.Enabled = txtFat.Enabled = txtWeight.Enabled = txtsaleprice.Enabled =
                lnkbtnQty.Enabled = false;

            }

        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.planmealcustinvoices.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.planmealcustinvoices.Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    if (take == Totalrec && Skip == (Totalrec - Showdata))
        //        btnNext1.Enabled = false;
        //    else
        //        btnNext1.Enabled = true;
        //    if (take == Showdata && Skip == 0)
        //        btnPrevious1.Enabled = false;
        //    else
        //        btnPrevious1.Enabled = true;

        //    ChoiceID = take / Showdata;

        //    ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.planmealcustinvoices.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.planmealcustinvoices.Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        if (take == Showdata && Skip == 0)
        //            btnPrevious1.Enabled = false;
        //        else
        //            btnPrevious1.Enabled = true;

        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;

        //        ChoiceID = take / Showdata;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.planmealcustinvoices.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.planmealcustinvoices.Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //    }
        //}
        //protected void btnLast1_Click(object sender, EventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.planmealcustinvoices.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.planmealcustinvoices.Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        //}
        //protected void btnlistreload_Click(object sender, EventArgs e)
        //{
        //    BindData();
        //}
        protected void btnPagereload_Click(object sender, EventArgs e)
        {
            Readonly();
            ManageLang();
            pnlSuccessMsg.Visible = false;
            FillContractorID();
            int CurrentID = 1;
            if (ViewState["Es"] != null)
                CurrentID = Convert.ToInt32(ViewState["Es"]);
            BindData();
            //FirstData();
        }

        protected void ListViewContract_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnEditContract")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');

                int CID = Convert.ToInt32(ID[0]);
                int Plan = Convert.ToInt32(ID[1]);
                int Meal = Convert.ToInt32(ID[2]);
                int MYTRANSID = Convert.ToInt32(ID[3]);
                //int DID = Convert.ToInt32(ID[4]);
                Editinvoice(CID, MYTRANSID, Plan, Meal);
            }

            if (e.CommandName == "btnCopyContract")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');

                int CID = Convert.ToInt32(ID[0]);
                int Plan = Convert.ToInt32(ID[1]);
                int Meal = Convert.ToInt32(ID[2]);
                int MYTRANSID = Convert.ToInt32(ID[3]);

                List<Database.planmealcustinvoiceHD> ListCUstHD = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.CustomerID == CID && p.planid == Plan).ToList();
                if (ListCUstHD.Count() > 0)
                {
                    int NewMYTRANSID = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID).Count() > 0 ? DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID).Max(p => p.MYTRANSID) + 1 : 1;
                    foreach (Database.planmealcustinvoiceHD items in ListCUstHD)
                    {
                        Database.planmealcustinvoiceHD OBJHD = new planmealcustinvoiceHD();

                        OBJHD.TenentID = TID;
                        OBJHD.MYTRANSID = NewMYTRANSID;
                        OBJHD.LOCATION_ID = LID;
                        OBJHD.CustomerID = CID;
                        OBJHD.planid = Plan;
                        OBJHD.DayNumber = 0;
                        OBJHD.TransID = NewMYTRANSID;
                        OBJHD.ContractID = NewMYTRANSID.ToString();
                        OBJHD.DefaultDriverID = items.DefaultDriverID;
                        OBJHD.ContractDate = DateTime.Now;
                        OBJHD.WeekofDay = items.WeekofDay;
                        OBJHD.StartDate = DateTime.Now;
                        DateTime StartDate = DateTime.Now;
                        OBJHD.EndDate = StartDate.AddDays(27);
                        OBJHD.SubscriptionOnHold = false;
                        OBJHD.Total_price = items.Total_price;
                        OBJHD.CStatus = "Started";
                        OBJHD.ACTIVE = true;
                        DB.planmealcustinvoiceHDs.AddObject(OBJHD);
                        DB.SaveChanges();
                    }

                    List<Database.planmealcustinvoice> Listinvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DisplayWeek == 1).OrderBy(p => p.DayNumber).ToList();

                    int FriDay = 0;

                    foreach (Database.planmealcustinvoice items in Listinvoice)
                    {
                        List<Database.planmealcustinvoice> ListExist = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == NewMYTRANSID && p.DeliveryMeal == items.DeliveryMeal && p.MYPRODID == items.MYPRODID && p.DayNumber == items.DayNumber).ToList();
                        if (ListExist.Count() < 1)
                        {
                            Database.planmealcustinvoice objinvice = new planmealcustinvoice();
                            objinvice.TenentID = TID;
                            objinvice.LOCATION_ID = LID;
                            objinvice.MYTRANSID = NewMYTRANSID;
                            objinvice.DeliveryID = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == NewMYTRANSID).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == NewMYTRANSID).Max(p => p.DeliveryID) + 1) : 1;
                            objinvice.CustomerID = items.CustomerID;
                            objinvice.planid = items.planid;
                            objinvice.MealType = items.MealType;
                            objinvice.MYPRODID = items.MYPRODID;
                            objinvice.ProdName1 = items.ProdName1;
                            objinvice.DayNumber = items.DayNumber;
                            objinvice.OprationDay = items.OprationDay;
                            objinvice.TransID = items.TransID;
                            objinvice.ContractID = items.ContractID;
                            objinvice.WeekofDay = items.WeekofDay;
                            objinvice.TotalWeek = items.TotalWeek;
                            objinvice.NameOfDay = items.NameOfDay;
                            objinvice.NoOfWeek = items.NoOfWeek;
                            objinvice.DisplayWeek = 1;
                            objinvice.TotalDeliveryDay = items.TotalDeliveryDay;
                            objinvice.ActualDeliveryDay = items.ActualDeliveryDay;
                            objinvice.ExpectedDeliveryDay = items.ExpectedDeliveryDay;
                            objinvice.DeliveryTime = items.DeliveryTime;
                            objinvice.DeliveryMeal = items.DeliveryMeal;
                            objinvice.DriverID = items.DriverID;
                            objinvice.StartDate = DateTime.Now;
                            DateTime StartDate = DateTime.Now;
                            objinvice.EndDate = StartDate.AddDays(27);
                            int Day = Convert.ToInt32(items.DayNumber) - 1;
                            DateTime Fexp = StartDate.AddDays(Day);
                            DateTime fexp1 = StartDate.AddDays(Day);
                            if (Fexp.DayOfWeek == DayOfWeek.Friday)
                            {
                                Fexp = Fexp.AddDays(1);
                            }

                            if (FriDay == 1 && fexp1.DayOfWeek != DayOfWeek.Friday)
                            {
                                Fexp = Fexp.AddDays(1);
                                objinvice.ExpectedDelDate = Fexp;
                                objinvice.NExtDeliveryDate = Fexp.AddDays(1);
                            }
                            else
                            {
                                objinvice.ExpectedDelDate = Fexp;
                                objinvice.NExtDeliveryDate = Fexp.AddDays(1);
                            }

                            if (fexp1.DayOfWeek == DayOfWeek.Friday)
                            {
                                FriDay = 1;
                            }
                            objinvice.SubscriptonDayNumber = items.SubscriptonDayNumber;
                            objinvice.Status = "Copy";
                            objinvice.Calories = items.Calories;
                            objinvice.Carbs = items.Carbs;
                            objinvice.Protein = items.Protein;
                            objinvice.Fat = items.Fat;
                            objinvice.ItemWeight = items.ItemWeight;
                            objinvice.Qty = items.Qty;
                            objinvice.Item_cost = items.Item_cost;
                            objinvice.Item_price = items.Item_price;
                            objinvice.Total_price = items.Total_price;
                            objinvice.ShortRemark = items.ShortRemark;
                            objinvice.ACTIVE = items.ACTIVE;
                            objinvice.ChangesDate = items.ChangesDate;
                            objinvice.Switch3 = items.Switch3;
                            //int CRUP_ID = GlobleClass.EncryptionHelpers.WriteLog("planmealcustinvoice,INSERT: " + Log, "INSERT", "planmealcustinvoice", UID.ToString(), MenuID);
                            //objinvice.CRUP_ID = CRUP_ID;

                            DB.planmealcustinvoices.AddObject(objinvice);
                            DB.SaveChanges();
                        }

                    }
                    Editinvoice(CID, NewMYTRANSID, Plan, Meal);
                }

            }

        }
        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDelete")
            {
                if (switch1 == "editON")
                {
                    pnlErrorMsg.Visible = true;
                    lblerrorMsg.Text = "Please Update Or Cancel your Current Transection";
                    return;
                }

                string[] ID = e.CommandArgument.ToString().Split(',');
                int CID = Convert.ToInt32(ID[0]);
                int PID = Convert.ToInt32(ID[1]);
                int MID = Convert.ToInt32(ID[2]);
                int MYTRANSID = Convert.ToInt32(ID[3]);

                if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate != null).Count() < 1)
                {
                    Database.planmealcustinvoiceHD objSOJobDesc = DB.planmealcustinvoiceHDs.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
                    //objSOJobDesc.ACTIVE = false;
                    DB.planmealcustinvoiceHDs.DeleteObject(objSOJobDesc);
                    DB.SaveChanges();

                    List<Database.planmealcustinvoice> ListInvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                    foreach (Database.planmealcustinvoice intems in ListInvoice)
                    {
                        Database.planmealcustinvoice objDT = intems;
                        DB.planmealcustinvoices.DeleteObject(objDT);
                        //objDT.ACTIVE = false;
                        DB.SaveChanges();
                    }
                }
                else
                {
                    int count = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate != null).Max(p => p.DayNumber);
                    pnlErrorMsg.Visible = true;
                    lblerrorMsg.Text = "Delivered " + count + " that shows the transaction in Progress now. You can not delete this transaction !! !   !";
                    return;
                }

                //GlobleClass.EncryptionHelpers.UpdateLog("planmealcustinvoiceHD,Delete:Active=False", crupID, "planmealcustinvoiceHD", UID.ToString());
                BindData();
            }

            //if (e.CommandName == "btnprient")
            //{
            //    //int MYTID1 = Convert.ToInt32(e.CommandArgument);
            //    string[] ID = e.CommandArgument.ToString().Split(',');
            //    int CID = Convert.ToInt32(ID[0]);
            //    int PID = Convert.ToInt32(ID[1]);
            //    int MID = Convert.ToInt32(ID[2]);
            //    Response.Redirect("HB_DeliveryCard.aspx?CID=" + CID + "&planid=" + PID + "&MealType=" + MID);
            //}

            //if (e.CommandName == "btnview")
            //{
            //    string[] ID = e.CommandArgument.ToString().Split(',');

            //    int CID = Convert.ToInt32(ID[0]);
            //    int Plan = Convert.ToInt32(ID[1]);
            //    int Meal = Convert.ToInt32(ID[2]);
            //    int MYTRANSID = Convert.ToInt32(ID[3]);
            //    //int DID = Convert.ToInt32(ID[4]);
            //    Editinvoice(CID, MYTRANSID, Plan, Meal);
            //    ReadonlyFull();
            //}

            if (e.CommandName == "btnEdit")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');

                int CID = Convert.ToInt32(ID[0]);
                int Plan = Convert.ToInt32(ID[1]);
                int Meal = Convert.ToInt32(ID[2]);
                int MYTRANSID = Convert.ToInt32(ID[3]);
                //int DID = Convert.ToInt32(ID[4]);
                Editinvoice(CID, MYTRANSID, Plan, Meal);
            }
        }

        public void Editinvoice(int CID, int MYTRANSID, int Plan, int Meal)
        {

            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";

            if (MYTRANSID != 0)
            {
                if (switch1 == "editON")
                {
                    pnlErrorMsg.Visible = true;
                    lblerrorMsg.Text = "Please Update Or Cancel your Current Transection";
                    return;
                }

                FillContractorID();
                //btnAddmeal.Visible = true;
                btnCopyFullPlan.Enabled = true;
                //drpDeliveryTime.Enabled = true;

                List<Database.TBLCOMPANYSETUP> ListCustomer = new List<Database.TBLCOMPANYSETUP>();
                List<Database.tblcontact_addon1> FinelCustomer = DB.tblcontact_addon1.Where(p => p.TenentID == TID).ToList();
                List<Database.tblcontact_addon1_dtl> ListDtl1 = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID).ToList();
                List<Database.TBLCOMPANYSETUP> ListComp = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).ToList();
                foreach (Database.tblcontact_addon1 Citem in FinelCustomer)
                {
                    if (FinelCustomer.Where(p => p.TenentID == TID && p.CustomerId == Citem.CustomerId).Count() > 0)
                    {
                        List<Database.tblcontact_addon1_dtl> ListDtl = ListDtl1.Where(p => p.TenentID == TID && p.customerID == Citem.CustomerId && p.LikeType == "M" && p.planid != null && p.planid != 0).ToList();
                        if (ListDtl.Count() > 0)
                        {
                            Database.TBLCOMPANYSETUP Cobj = ListComp.Single(p => p.TenentID == TID && p.COMPID == Citem.CustomerId);
                            ListCustomer.Add(Cobj);
                        }
                    }
                }
                drpCustomer.DataSource = ListCustomer.OrderBy(p => p.COMPNAME1);// DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID);
                drpCustomer.DataTextField = "COMPNAME1";
                drpCustomer.DataValueField = "COMPID";
                drpCustomer.DataBind();
                drpCustomer.Items.Insert(0, new ListItem("-- Select --", "0"));

                Database.planmealcustinvoiceHD objplanHD = DB.planmealcustinvoiceHDs.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);

                drpCustomer.SelectedValue = objplanHD.CustomerID.ToString();
                drpExpecteddriver.SelectedValue = objplanHD.DefaultDriverID.ToString();
                txtInvoiceNumber.Text = objplanHD.TransID.ToString();
                drpPlan.SelectedValue = objplanHD.planid.ToString();
                txtBeingDate.Text = Convert.ToDateTime(objplanHD.StartDate).ToShortDateString();
                txtEndDate.Text = Convert.ToDateTime(objplanHD.EndDate).ToShortDateString();
                txtExpDate.Text = Convert.ToDateTime(objplanHD.ContractDate).ToShortDateString();
                List<Database.planmealcustinvoice> ListTotalweek = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                //txtTotalWeek.Text = ListTotalweek.Max(p => p.NoOfWeek).ToString();
                DeliveryTime();
                EditDeloveryMeal();
                NoWeek();

                List<Database.planmealcustinvoice> ListInvoice1 = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                List<Database.planmealcustinvoice> ListInvo = ListInvoice1.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.planid == Plan && p.DeliveryMeal == Meal).ToList();//&& p.CustomerID == CID

                if (ListInvo.Count() > 0)
                {
                    Database.planmealcustinvoice OBJPlanInvoice = ListInvo.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.planid == Plan && p.DeliveryMeal == Meal).FirstOrDefault();//&& p.CustomerID == CID

                    //bindMeal();               

                    //drpDay.SelectedValue = OBJPlanInvoice.DayNumber.ToString();
                    txtTotalPlanPrice.Text = OBJPlanInvoice.Total_price.ToString();
                    //Calculation Field                    
                    tags_1.Text = OBJPlanInvoice.WeekofDay.ToString();
                    txtTotalWeek.Text = OBJPlanInvoice.TotalWeek.ToString();
                    txtTotalDeliveryDay.Text = OBJPlanInvoice.TotalDeliveryDay.ToString();
                    txtRemainingDay.Text = OBJPlanInvoice.ExpectedDeliveryDay.ToString();
                    txtDelivered.Text = OBJPlanInvoice.ActualDeliveryDay.ToString();
                    lblDeliveryTIMEID.Text = OBJPlanInvoice.DeliveryTime.ToString();
                    //drpDeliveryTime.SelectedValue = OBJPlanInvoice.DeliveryTime.ToString();
                    //DeloveryMeal();

                    drpDeliveryMeal.SelectedValue = OBJPlanInvoice.DeliveryMeal.ToString();
                    int DeliveryTimes = Convert.ToInt32(OBJPlanInvoice.DeliveryTime);
                    lbldeltime.Text = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == DeliveryTimes).Count() > 0 ? DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == DeliveryTimes).REFNAME1.ToString() : "";
                    ViewState["DeliveryTimeInvoice"] = DeliveryTimes;
                    btnAddmeal.Visible = true;
                    //drpMeal.SelectedValue = OBJPlanInvoice.MealType.ToString();
                    //End
                    ViewState["ALL"] = CID + "," + Plan + "," + Meal;// +"," + DID;
                    ViewState["MYTRANSID"] = MYTRANSID;

                    DrpWeek.SelectedValue = OBJPlanInvoice.NoOfWeek.ToString();



                    if (ListInvoice1.Count() > 0)
                    {
                        int TOtal = Convert.ToInt32(txtRemainingDay.Text);
                        int Count = ListInvoice1.Count();
                        if (Count == TOtal)
                        {
                            //btnCopyFullPlan
                        }
                    }
                    int DelCount = ListInvoice1.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate != null).Count();//&& p.CustomerID == CID

                    GetProductList();
                }

                switch1 = "editOff";
                SwitchoONOFF(switch1);

                List<Database.planmealcustinvoice> List = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();

                List<Database.planmealcustinvoice> ListtotaldelDay = List.GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
                int totalDelDay = ListtotaldelDay.Count();

                List<Database.planmealcustinvoice> ListTotaldeldone = List.Where(p => p.ActualDelDate != null).GroupBy(p => p.DeliveryTime).Select(p => p.FirstOrDefault()).ToList();
                int DoneCount = ListTotaldeldone.Count();
                totalDelDay = totalDelDay != 0 ? totalDelDay : 1;
                int per = (DoneCount * 100) / totalDelDay;

                if (per >= 10)
                {
                    Button3.Enabled = true;
                }
                else
                {
                    Button3.Enabled = false;
                }

                btnAdd.Text = "Update";
                btnAdd.Enabled = true;

                Write();
                drpCustomer.Enabled = false;
                drpPlan.Enabled = false;
                //drpDeliveryTime.Enabled = false;
                if (List.Where(p => p.ActualDelDate != null).Count() > 0)
                {
                    txtBeingDate.Enabled = false;
                }
                else
                {
                    txtBeingDate.Enabled = true;
                }
                //txtBeingDate.Enabled = false;
                //txtEndDate.Enabled = false;
            }
            else
            {
                pnlErrorMsg.Visible = false;
                lblerrorMsg.Text = "Select Contract in List";
                return;
            }
        }

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{

        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.planmealcustinvoices.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.planmealcustinvoices.Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2, Totalrec);
        //        if (Tvalue == Showdata && Svalue == 0)
        //            btnPrevious1.Enabled = false;
        //        else
        //            btnPrevious1.Enabled = true;
        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;
        //    }
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";


        //}
        //protected void drpShowGrid_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindData();
        //}
        //protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
        //    ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
        //    control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        //}


        #endregion
        public string GetCustomer(int CID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.COMPID == CID && p.TenentID == TID).Count() > 0)
                return DB.TBLCOMPANYSETUPs.SingleOrDefault(p => p.COMPID == CID && p.TenentID == TID).COMPNAME1;
            else
                return "Not Found";

        }
        public string GetPlan(int PID)
        {
            if (DB.tblProduct_Plan.Where(p => p.planid == PID && p.TenentID == TID).Count() > 0)
                return DB.tblProduct_Plan.SingleOrDefault(p => p.planid == PID && p.TenentID == TID).planname1;
            else
                return "Not Found";
        }
        public string GetMeal1(int MID)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == MID).Count() > 0)
                return DB.REFTABLEs.SingleOrDefault(p => p.TenentID == TID && p.REFID == MID).REFNAME1;
            else
                return "Not Found";
        }

        //public string GetMeal(int Plan,int MYTRANSID)
        //{
        //    
        //    

        //    if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.planid == Plan).Count() > 0)
        //    {
        //        Database.planmealcustinvoice Objplaninvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.planid == Plan).SingleOrDefault();
        //        int MID = Objplaninvoice.MealType;
        //        if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == MID).Count() > 0)
        //            return DB.REFTABLEs.SingleOrDefault(p => p.TenentID == TID && p.REFID == MID).REFNAME1;
        //        else
        //            return "Not Found";
        //    }
        //    else
        //        return "Not Found";

        //}
        public string Getday(int DID)
        {
            if (DID == 1) return "1.Saturday";
            else if (DID == 2) return "2.Sunday";
            else if (DID == 3) return "3.Monday";
            else if (DID == 4) return "4.Tusday";
            else if (DID == 5) return "5.Wensday";
            else return "6.Thursday";

        }

        protected void btnCustomer_Click(object sender, EventArgs e)
        {
            Response.Redirect("/CRM/CompanyMaster.aspx?Type=82003&MID=gT9lZ6RqnrhwNtQwQSazyA==");
        }

        protected void btnAllergies_Click(object sender, EventArgs e)
        {
            Response.Redirect("tblcontact_addon1.aspx");
        }

        protected void btnAddresses_Click(object sender, EventArgs e)
        {
            Response.Redirect("TBLCONTACT_DEL_ADRES.aspx");
        }

        protected void drpPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bindMeal();
            DeliveryTime();
        }
        public void bindMeal()
        {
            int planid = Convert.ToInt32(drpPlan.SelectedValue);

            List<Database.REFTABLE> ListREFTABLE = new List<Database.REFTABLE>();
            List<Database.TBLPRODUCT> ListTBLPRODUCT = new List<Database.TBLPRODUCT>();

            List<Database.planmealsetup> Listplanmealsetup = DB.planmealsetups.Where(p => p.ACTIVE == true && p.TenentID == TID).ToList();

            if (DB.tblProduct_Plan.Where(p => p.planid == planid && p.TenentID == TID).Count() > 0)
            {
                foreach (Database.planmealsetup item2 in Listplanmealsetup.Where(a => a.planid == planid).GroupBy(p => p.MealType).Select(p => p.FirstOrDefault()))
                {
                    if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == item2.MealType).Count() > 0)
                    {
                        Database.REFTABLE obj2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == item2.MealType);
                        ListREFTABLE.Add(obj2);
                    }
                    foreach (Database.planmealsetup item3 in Listplanmealsetup.Where(a => a.planid == planid && a.MealType == item2.MealType).GroupBy(p => p.MYPRODID).Select(p => p.FirstOrDefault()))
                    {
                        if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == item3.MYPRODID).Count() > 0)
                        {
                            Database.TBLPRODUCT obj3 = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == item3.MYPRODID);
                            ListTBLPRODUCT.Add(obj3);
                        }
                    }
                }
            }
            //test
            drpMeal.DataSource = ListREFTABLE;
            drpMeal.DataTextField = "REFNAME1";
            drpMeal.DataValueField = "REFID";
            drpMeal.DataBind();
            drpMeal.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpProtein.DataSource = ListTBLPRODUCT;
            //drpProtein.DataTextField = "ProdName1";
            //drpProtein.DataValueField = "MYPRODID";
            //drpProtein.DataBind();
            //if (ListTBLPRODUCT.Count() == 0) drpProtein.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        protected void drpMeal_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetProductList();
        }
        protected void drpDeliveryMeal_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(drpCustomer.SelectedValue);
            int DeliveryTime = Convert.ToInt32(lblDeliveryTIMEID.Text);
            int meal = Convert.ToInt32(drpDeliveryMeal.SelectedValue);

            lbldeltime.Text = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == DeliveryTime).Count() > 0 ? DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == DeliveryTime).REFNAME1.ToString() : "";

            List<Database.planmealcustinvoice> List = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.DeliveryTime == DeliveryTime && p.DeliveryMeal == meal).ToList();

            if (List.Count() < 1)
            {
                btnAdd.Text = "Save";
            }

            if (switch1 == "editON")
            {
                btnAddmeal.Visible = false;
            }
            DateTime DT = Convert.ToDateTime(txtBeingDate.Text);
            DateTime dt1 = DT;
            setdate(dt1, 1);

            GetProductList();
        }
        public void GetProductList()
        {
            int daycount = 0;
            int DayCount1 = 0;
            string[] dayscount = tags_1.Text.Split(',');

            DayCount1 = dayscount.Length;

            foreach (string Val in dayscount)
            {
                if (Val == "Sat" && lbldelday1.Text != "")
                {
                    daycount++;
                }

                if (Val == "Sun" && lbldelday2.Text != "")
                {
                    daycount++;
                }

                if (Val == "Mon" && lbldelday3.Text != "")
                {
                    daycount++;
                }

                if (Val == "Tue" && lbldelday4.Text != "")
                {
                    daycount++;
                }

                if (Val == "Wed" && lbldelday5.Text != "")
                {
                    daycount++;
                }

                if (Val == "Thu" && lbldelday6.Text != "")
                {
                    daycount++;
                }
            }

            if (daycount == DayCount1)
            {
                btnsetday.Visible = false;
            }
            else
            {
                btnsetday.Visible = false;//true
            }

            List<Database.TBLPRODUCT> ListProd = new List<Database.TBLPRODUCT>();
            int planid = Convert.ToInt32(drpPlan.SelectedValue);

            int MID = drpDeliveryMeal.SelectedValue != "" ? Convert.ToInt32(drpDeliveryMeal.SelectedValue) : 0;//Convert.ToInt32(drpMeal.SelectedValue);
            int CID = Convert.ToInt32(drpCustomer.SelectedValue);
            List<Database.TBLPRODUCT> ListTBLPRODUCT = new List<Database.TBLPRODUCT>();
            List<Database.planmealsetup> Listpro = new List<Database.planmealsetup>();
            List<Database.planmealsetup> Listplanmealsetup = DB.planmealsetups.Where(p => p.ACTIVE == true && p.TenentID == TID).ToList();
            foreach (Database.planmealsetup item3 in Listplanmealsetup.Where(a => a.planid == planid && a.MealType == MID).GroupBy(p => p.MYPRODID).Select(p => p.FirstOrDefault()))
            {
                if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == item3.MYPRODID).Count() > 0)
                {
                    //Database.TBLPRODUCT obj3 = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == item3.MYPRODID);
                    //ListTBLPRODUCT.Add(obj3);
                    Listpro.Add(item3);
                }
            }

            ListView2.DataSource = Listpro.OrderBy(p => p.serial_no);// ListTBLPRODUCT;
            ListView2.DataBind();

            if (ViewState["MYTRANSID"] != null)
            {
                int MYTRANSID = Convert.ToInt32(ViewState["MYTRANSID"]);
                Listview3.DataSource = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ACTIVE == true).OrderBy(p => p.DayNumber);//&& p.MealType == MID
                Listview3.DataBind();

                getproductEdit(MYTRANSID);
            }
        }

        public void getproductEdit(int MYTRANSID)
        {
            int Week = Convert.ToInt32(DrpWeek.SelectedValue);
            int Planid = Convert.ToInt32(drpPlan.SelectedValue);
            int DTime = Convert.ToInt32(lblDeliveryTIMEID.Text);
            int MID = drpDeliveryMeal.SelectedValue != "" ? Convert.ToInt32(drpDeliveryMeal.SelectedValue) : 0;//Convert.ToInt32(drpMeal.SelectedValue);

            List<Database.planmealcustinvoiceHD> ListHD = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();

            if (ListHD.Count() > 0)
            {
                int DriverID = Convert.ToInt32(ListHD.Single(p => p.MYTRANSID == MYTRANSID).DriverID);

                drpChangeDriver.DataSource = DB.tbl_Employee.Where(p => p.TenentID == TID && p.employeeID != DriverID && p.EmployeeType == "Driver").OrderBy(p => p.firstname);
                drpChangeDriver.DataTextField = "firstname";
                drpChangeDriver.DataValueField = "employeeID";
                drpChangeDriver.DataBind();
                drpChangeDriver.Items.Insert(0, new ListItem("-- Select --", "0"));
            }

            List<Database.planmealcustinvoice> List = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();

            List<Database.planmealcustinvoice> ListtotaldelDay = List.GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
            int totalDelDay = ListtotaldelDay.Count();
            txtTotalDeliveryDay.Text = ListtotaldelDay.Count().ToString();

            //List<Database.planmealcustinvoice> ListTotaldel = List.Where(p => p.ActualDelDate == null).GroupBy(p => p.DeliveryTime).Select(p => p.FirstOrDefault()).ToList();
            //int TimeCount = ListTotaldel.Count();
            int Time = Convert.ToInt32(lblDeliveryTIME.Text);
            List<Database.planmealcustinvoice> ListdelDay = List.Where(p => p.ActualDelDate == null).GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
            int TimeCount = ListdelDay.Count();
            int remaining = TimeCount * Time;
            txtRemainingDay.Text = remaining.ToString();

            List<Database.planmealcustinvoice> ListTotaldeldone = List.Where(p => p.ActualDelDate != null).GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
            int DoneCount = ListTotaldeldone.Count();
            int Deliverd = DoneCount * Time;
            txtDelivered.Text = Deliverd.ToString();

            List<Database.planmealcustinvoice> ListInvoice = null;

            if (DrpWeek.SelectedValue == "1")
            {
                ListInvoice = List.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryMeal == MID && p.planid == Planid && p.DisplayWeek == Week && p.ACTIVE == true).ToList();// && p.CustomerID == CID && p.DeliveryTime == DTime
            }
            else
            {
                ListInvoice = List.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryMeal == MID && p.planid == Planid && p.NoOfWeek == Week && p.ACTIVE == true).ToList();// && p.CustomerID == CID && p.DeliveryTime == DTime
            }

            if (ListInvoice.Count() > 0)
            {
                Database.planmealcustinvoice OBJPlanInvoice = ListInvoice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.planid == Planid && p.DeliveryMeal == MID && p.ACTIVE == true).FirstOrDefault();//&& p.CustomerID == CID && p.NoOfWeek == Week 
                DateTime DT = Convert.ToDateTime(OBJPlanInvoice.ExpectedDelDate);
                int Day = Convert.ToInt32(OBJPlanInvoice.DayNumber);

                lbldelday1.Text = "";
                lbldelday2.Text = "";
                lbldelday3.Text = "";
                lbldelday4.Text = "";
                lbldelday5.Text = "";
                lbldelday6.Text = "";

                lblDate1.Text = "";
                lblDate2.Text = "";
                lblDate3.Text = "";
                lblDate4.Text = "";
                lblDate5.Text = "";
                lblDate6.Text = "";

                lblFDate1.Text = "";
                lblFDate2.Text = "";
                lblFDate3.Text = "";
                lblFDate4.Text = "";
                lblFDate5.Text = "";
                lblFDate6.Text = "";

                bool Flg1 = false;
                bool Flg2 = false;
                bool Flg3 = false;
                bool Flg4 = false;
                bool Flg5 = false;
                bool Flg6 = false;

                btnDateset1.Visible = false;
                btnDateset2.Visible = false;
                btnDateset3.Visible = false;
                btnDateset4.Visible = false;
                btnDateset5.Visible = false;
                btnDateset6.Visible = false;

                for (int i = 0; i < ListView2.Items.Count; i++)
                {
                    CheckBox RDODay1 = (CheckBox)ListView2.Items[i].FindControl("RDODay1");
                    CheckBox RDODay2 = (CheckBox)ListView2.Items[i].FindControl("RDODay2");
                    CheckBox RDODay3 = (CheckBox)ListView2.Items[i].FindControl("RDODay3");
                    CheckBox RDODay4 = (CheckBox)ListView2.Items[i].FindControl("RDODay4");
                    CheckBox RDODay5 = (CheckBox)ListView2.Items[i].FindControl("RDODay5");
                    CheckBox RDODay6 = (CheckBox)ListView2.Items[i].FindControl("RDODay6");

                    TextBox txtqty1 = (TextBox)ListView2.Items[i].FindControl("txtqty1");
                    TextBox txtqty2 = (TextBox)ListView2.Items[i].FindControl("txtqty2");
                    TextBox txtqty3 = (TextBox)ListView2.Items[i].FindControl("txtqty3");
                    TextBox txtqty4 = (TextBox)ListView2.Items[i].FindControl("txtqty4");
                    TextBox txtqty5 = (TextBox)ListView2.Items[i].FindControl("txtqty5");
                    TextBox txtqty6 = (TextBox)ListView2.Items[i].FindControl("txtqty6");

                    RDODay1.Checked = false;
                    RDODay2.Checked = false;
                    RDODay3.Checked = false;
                    RDODay4.Checked = false;
                    RDODay5.Checked = false;
                    RDODay6.Checked = false;

                    RDODay1.Enabled = false;
                    RDODay2.Enabled = false;
                    RDODay3.Enabled = false;
                    RDODay4.Enabled = false;
                    RDODay5.Enabled = false;
                    RDODay6.Enabled = false;

                    string[] days = tags_1.Text.Split(',');
                    foreach (string Val in days)
                    {
                        if (Val == "Sat")
                        {
                            RDODay1.Enabled = true;
                        }
                        if (Val == "Sun")
                        {
                            RDODay2.Enabled = true;
                        }
                        if (Val == "Mon")
                        {
                            RDODay3.Enabled = true;
                        }
                        if (Val == "Tue")
                        {
                            RDODay4.Enabled = true;
                        }
                        if (Val == "Wed")
                        {
                            RDODay5.Enabled = true;
                        }
                        if (Val == "Thu")
                        {
                            RDODay6.Enabled = true;
                        }
                    }

                    TextBox txtWeight = (TextBox)ListView2.Items[i].FindControl("txtWeight");
                    TextBox txtCalories = (TextBox)ListView2.Items[i].FindControl("txtCalories");
                    TextBox txtProtain = (TextBox)ListView2.Items[i].FindControl("txtProtain");
                    TextBox txtCarbs = (TextBox)ListView2.Items[i].FindControl("txtCarbs");
                    TextBox txtFat = (TextBox)ListView2.Items[i].FindControl("txtFat");

                    TextBox txtsaleprice = (TextBox)ListView2.Items[i].FindControl("txtsaleprice");
                    Label lblProd = (Label)ListView2.Items[i].FindControl("lblProd");
                    int PID = Convert.ToInt32(lblProd.Text);

                    if (ListInvoice.Where(p => p.MYPRODID == PID).Count() > 0)//&& p.CustomerID == CID && p.NoOfWeek == Week
                    {
                        Database.planmealcustinvoice objinvoice = ListInvoice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID).FirstOrDefault();//&& p.CustomerID == CID && p.NoOfWeek == Week

                        string Weight = "100";
                        string Calories = "300";
                        string Protain = "14";
                        string Carbs = "36";
                        string Fat = "9";
                        List<Database.planmealsetup> ListPlan = DB.planmealsetups.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == Planid && p.MealType == MID && p.MYPRODID == PID).ToList();
                        if (ListPlan.Count() > 0)
                        {
                            Database.planmealsetup objplan = ListPlan.Single(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == Planid && p.MealType == MID && p.MYPRODID == PID);

                            Weight = objplan.ItemWeight != null ? objplan.ItemWeight.ToString() : "100";
                            Calories = objplan.Calories != null ? objplan.Calories.ToString() : "300";
                            Protain = objplan.Protein != null ? objplan.Protein.ToString() : "14";
                            Carbs = objplan.Carbs != null ? objplan.Carbs.ToString() : "36";
                            Fat = objplan.Fat != null ? objplan.Fat.ToString() : "9";
                        }


                        txtWeight.Text = objinvoice.ItemWeight != null ? objinvoice.ItemWeight.ToString() : Weight;
                        txtCalories.Text = objinvoice.Calories != null ? objinvoice.Calories.ToString() : Calories;
                        txtProtain.Text = objinvoice.Protein != null ? objinvoice.Protein.ToString() : Protain;
                        txtCarbs.Text = objinvoice.Carbs != null ? objinvoice.Carbs.ToString() : Carbs;
                        txtFat.Text = objinvoice.Fat != null ? objinvoice.Fat.ToString() : Fat;

                        txtsaleprice.Text = objinvoice.Item_cost.ToString();

                        List<Database.planmealcustinvoice> ListInvoice123 = ListInvoice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID).ToList();//&& p.CustomerID == CID && p.NoOfWeek == Week

                        foreach (Database.planmealcustinvoice items in ListInvoice123)
                        {
                            Database.planmealcustinvoice objinvoice12 = ListInvoice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == PID && p.DayNumber == items.DayNumber && p.DeliveryMeal == MID).FirstOrDefault();//&& p.CustomerID == CID  && p.NoOfWeek == Week

                            if (objinvoice12.ActualDelDate != null)
                            {
                                DateTime ActDelDate = Convert.ToDateTime(objinvoice12.ActualDelDate);
                                if (ActDelDate.DayOfWeek == DayOfWeek.Saturday)
                                    RDODay1.Enabled = false;
                                if (ActDelDate.DayOfWeek == DayOfWeek.Sunday)
                                    RDODay2.Enabled = false;
                                if (ActDelDate.DayOfWeek == DayOfWeek.Monday)
                                    RDODay3.Enabled = false;
                                if (ActDelDate.DayOfWeek == DayOfWeek.Tuesday)
                                    RDODay4.Enabled = false;
                                if (ActDelDate.DayOfWeek == DayOfWeek.Wednesday)
                                    RDODay5.Enabled = false;
                                if (ActDelDate.DayOfWeek == DayOfWeek.Thursday)
                                    RDODay6.Enabled = false;

                            }

                            if (objinvoice12.OprationDay == 1)
                            {
                                RDODay1.Checked = true;
                                txtqty1.Text = objinvoice12.Qty.ToString();
                                lbldelday1.Text = objinvoice12.DayNumber.ToString();
                                txtdelday1.Text = objinvoice12.DayNumber.ToString();
                                DateTime EDT = Convert.ToDateTime(objinvoice12.ExpectedDelDate);
                                lblDate1.Text = setshortdate(EDT);
                                lblFDate1.Text = objinvoice12.ExpectedDelDate.Value.ToShortDateString();
                                Flg1 = true;
                            }

                            if (objinvoice12.OprationDay == 2)
                            {
                                RDODay2.Checked = true;
                                txtqty2.Text = objinvoice12.Qty.ToString();
                                lbldelday2.Text = objinvoice12.DayNumber.ToString();
                                txtdelday2.Text = objinvoice12.DayNumber.ToString();
                                DateTime EDT = Convert.ToDateTime(objinvoice12.ExpectedDelDate);
                                lblDate2.Text = setshortdate(EDT);
                                lblFDate2.Text = objinvoice12.ExpectedDelDate.Value.ToShortDateString();
                                Flg2 = true;
                            }

                            if (objinvoice12.OprationDay == 3)
                            {
                                RDODay3.Checked = true;
                                txtqty3.Text = objinvoice12.Qty.ToString();
                                lbldelday3.Text = objinvoice12.DayNumber.ToString();
                                txtdelday3.Text = objinvoice12.DayNumber.ToString();
                                DateTime EDT = Convert.ToDateTime(objinvoice12.ExpectedDelDate);
                                lblDate3.Text = setshortdate(EDT);
                                lblFDate3.Text = objinvoice12.ExpectedDelDate.Value.ToShortDateString();
                                Flg3 = true;
                            }

                            if (objinvoice12.OprationDay == 4)
                            {
                                RDODay4.Checked = true;
                                txtqty4.Text = objinvoice12.Qty.ToString();
                                lbldelday4.Text = objinvoice12.DayNumber.ToString();
                                txtdelday4.Text = objinvoice12.DayNumber.ToString();
                                DateTime EDT = Convert.ToDateTime(objinvoice12.ExpectedDelDate);
                                lblDate4.Text = setshortdate(EDT);
                                lblFDate4.Text = objinvoice12.ExpectedDelDate.Value.ToShortDateString();
                                Flg4 = true;
                            }

                            if (objinvoice12.OprationDay == 5)
                            {
                                RDODay5.Checked = true;
                                txtqty5.Text = objinvoice12.Qty.ToString();
                                lbldelday5.Text = objinvoice12.DayNumber.ToString();
                                txtdelday5.Text = objinvoice12.DayNumber.ToString();
                                DateTime EDT = Convert.ToDateTime(objinvoice12.ExpectedDelDate);
                                lblDate5.Text = setshortdate(EDT);
                                lblFDate5.Text = objinvoice12.ExpectedDelDate.Value.ToShortDateString();
                                Flg5 = true;
                            }

                            if (objinvoice12.OprationDay == 6)
                            {
                                RDODay6.Checked = true;
                                txtqty6.Text = objinvoice12.Qty.ToString();
                                lbldelday6.Text = objinvoice12.DayNumber.ToString();
                                txtdelday6.Text = objinvoice12.DayNumber.ToString();
                                DateTime EDT = Convert.ToDateTime(objinvoice12.ExpectedDelDate);
                                lblDate6.Text = setshortdate(EDT);
                                lblFDate6.Text = objinvoice12.ExpectedDelDate.Value.ToShortDateString();
                                Flg6 = true;
                            }

                        }
                    }
                }

                if (ListHD.Count() > 0)
                {
                    if (Week == 1)
                    {
                        DateTime dt1 = Convert.ToDateTime(ListHD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).StartDate);
                        setdate(dt1, 1);
                    }
                }

                for (int i = 0; i < ListView2.Items.Count; i++)
                {
                    CheckBox RDODay1 = (CheckBox)ListView2.Items[i].FindControl("RDODay1");
                    CheckBox RDODay2 = (CheckBox)ListView2.Items[i].FindControl("RDODay2");
                    CheckBox RDODay3 = (CheckBox)ListView2.Items[i].FindControl("RDODay3");
                    CheckBox RDODay4 = (CheckBox)ListView2.Items[i].FindControl("RDODay4");
                    CheckBox RDODay5 = (CheckBox)ListView2.Items[i].FindControl("RDODay5");
                    CheckBox RDODay6 = (CheckBox)ListView2.Items[i].FindControl("RDODay6");

                    DateTime Today = DateTime.Now;
                    DateTime updatedate = Today.AddDays(-1);
                    DateTime Enddate = Convert.ToDateTime(txtEndDate.Text);


                    List<Database.planmealcustinvoice> ListSetdate = List.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                    //int Weekset = Convert.ToInt32(ListSetdate.Max(p => p.NoOfWeek));
                    int Weekset = Convert.ToInt32(txtTotalWeek.Text);
                    int weekselect = Convert.ToInt32(DrpWeek.SelectedValue);
                    DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
                    string[] days1 = tags_1.Text.Split(',');
                    foreach (string Val in days1)
                    {
                        if (Val == "Sat")
                        {
                            if (Flg1 == false)
                            {
                                if (weekselect != Weekset && weekselect < Weekset)
                                {
                                    btnDateset1.Visible = true;
                                    RDODay1.Enabled = true;
                                }
                                else
                                {
                                    btnDateset1.Visible = false;
                                    RDODay1.Enabled = false;
                                }

                            }
                        }
                        if (Val == "Sun")
                        {
                            if (Flg2 == false)
                            {
                                if (weekselect != Weekset && weekselect < Weekset)
                                {
                                    btnDateset2.Visible = true;
                                    RDODay2.Enabled = true;
                                }
                                else
                                {
                                    btnDateset2.Visible = false;
                                    RDODay2.Enabled = false;
                                }

                            }
                        }
                        if (Val == "Mon")
                        {
                            if (Flg3 == false)
                            {
                                if (weekselect != Weekset && weekselect < Weekset)
                                {
                                    btnDateset3.Visible = true;
                                    RDODay3.Enabled = true;
                                }
                                else
                                {
                                    btnDateset3.Visible = false;
                                    RDODay3.Enabled = false;
                                }

                            }
                        }
                        if (Val == "Tue")
                        {
                            if (Flg4 == false)
                            {
                                if (weekselect != Weekset && weekselect < Weekset)
                                {
                                    btnDateset4.Visible = true;
                                    RDODay4.Enabled = true;
                                }
                                else
                                {
                                    btnDateset4.Visible = false;
                                    RDODay4.Enabled = false;
                                }

                            }
                        }
                        if (Val == "Wed")
                        {
                            if (Flg5 == false)
                            {
                                if (weekselect != Weekset && weekselect < Weekset)
                                {
                                    btnDateset5.Visible = true;
                                    RDODay5.Enabled = true;
                                }
                                else
                                {
                                    btnDateset5.Visible = false;
                                    RDODay5.Enabled = false;
                                }

                            }
                        }
                        if (Val == "Thu")
                        {
                            if (Flg6 == false)
                            {
                                if (weekselect != Weekset && weekselect < Weekset)
                                {
                                    btnDateset6.Visible = true;
                                    RDODay6.Enabled = true;
                                }
                                else
                                {
                                    btnDateset6.Visible = false;
                                    RDODay6.Enabled = false;
                                }

                            }
                        }

                    }
                    List<Database.planmealcustinvoice> ListMaxweek = List.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                    int Weekset1 = ListMaxweek.Count() > 0 ? Convert.ToInt32(ListMaxweek.Max(p => p.NoOfWeek)) : 1;

                    if (weekselect == Weekset1)
                    {
                        DateTime EndDate1 = Convert.ToDateTime(txtEndDate.Text);
                        string[] days2 = tags_1.Text.Split(',');
                        foreach (string Val in days2)
                        {
                            if (Val == "Sat")
                            {
                                btnDateset1.Visible = false;
                                RDODay1.Enabled = true;
                            }
                            if (Val == "Sun")
                            {
                                btnDateset2.Visible = false;
                                RDODay2.Enabled = true;
                            }
                            if (Val == "Mon")
                            {
                                btnDateset3.Visible = false;
                                RDODay3.Enabled = true;
                            }
                            if (Val == "Tue")
                            {
                                btnDateset4.Visible = false;
                                RDODay4.Enabled = true;
                            }
                            if (Val == "Wed")
                            {
                                btnDateset5.Visible = false;
                                RDODay5.Enabled = true;
                            }
                            if (Val == "Thu")
                            {
                                btnDateset6.Visible = false;
                                RDODay6.Enabled = true;
                            }
                        }
                    }

                    int DBDelCOUNT = List.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate != null).Count();
                    if (DBDelCOUNT > 0)
                    {
                        if (lblFDate1.Text != "")
                        {
                            DateTime checkdate = Convert.ToDateTime(lblFDate1.Text);
                            if (checkdate <= updatedate)
                            {
                                RDODay1.Enabled = false;
                            }
                        }

                        if (lblFDate2.Text != "")
                        {
                            DateTime checkdate = Convert.ToDateTime(lblFDate2.Text);
                            if (checkdate <= updatedate)
                            {
                                RDODay2.Enabled = false;
                            }
                        }

                        if (lblFDate3.Text != "")
                        {
                            DateTime checkdate = Convert.ToDateTime(lblFDate3.Text);
                            if (checkdate <= updatedate)
                            {
                                RDODay3.Enabled = false;
                            }
                        }

                        if (lblFDate4.Text != "")
                        {
                            DateTime checkdate = Convert.ToDateTime(lblFDate4.Text);
                            if (checkdate <= updatedate)
                            {
                                RDODay4.Enabled = false;
                            }
                        }

                        if (lblFDate5.Text != "")
                        {
                            DateTime checkdate = Convert.ToDateTime(lblFDate5.Text);
                            if (checkdate <= updatedate)
                            {
                                RDODay5.Enabled = false;
                            }
                        }

                        if (lblFDate6.Text != "")
                        {
                            DateTime checkdate = Convert.ToDateTime(lblFDate6.Text);
                            if (checkdate <= updatedate)
                            {
                                RDODay6.Enabled = false;
                            }
                        }
                    }

                }
                if (ListHD.Count() > 0)
                {
                    if (Week == 1)
                    {
                        DateTime dt1 = Convert.ToDateTime(ListHD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).StartDate);
                        setdate(dt1, 1);

                        if (lblDate1.Text != "")
                        {
                            btnDateset1.Visible = false;
                        }
                        if (lblDate2.Text != "")
                        {
                            btnDateset2.Visible = false;
                        }
                        if (lblDate3.Text != "")
                        {
                            btnDateset3.Visible = false;
                        }
                        if (lblDate4.Text != "")
                        {
                            btnDateset4.Visible = false;
                        }
                        if (lblDate5.Text != "")
                        {
                            btnDateset5.Visible = false;
                        }
                        if (lblDate6.Text != "")
                        {
                            btnDateset6.Visible = false;
                        }
                    }
                }
            }
        }

        public string setshortdate(DateTime DT)
        {
            string DT1 = Convert.ToInt32(DT.Day) + " - " + Convert.ToInt32(DT.Month);
            return DT1;
        }

        //protected void drpProtein_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    
        //    int planid = Convert.ToInt32(drpPlan.SelectedValue);
        //    int MID = Convert.ToInt32(drpDeliveryMeal.SelectedValue);//Convert.ToInt32(drpMeal.SelectedValue);
        //    int CID = Convert.ToInt32(drpCustomer.SelectedValue);
        //    int Prod = Convert.ToInt32(drpProtein.SelectedValue);

        //    if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == Prod).Count() > 0)
        //    {
        //        decimal SalePrice = Convert.ToDecimal(DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == Prod).msrp);
        //        txtsaleprice.Text = SalePrice.ToString();
        //    }
        //    else
        //    {
        //        txtsaleprice.Text = "0.00";
        //    }
        //    if (DB.planmealsetups.Where(p => p.TenentID == TID && p.planid == planid && p.MealType == MID && p.MYPRODID == Prod).Count() > 0)
        //    {
        //        int Carb = Convert.ToInt32(DB.planmealsetups.Single(p => p.TenentID == TID && p.planid == planid && p.MealType == MID && p.MYPRODID == Prod).ItemWeight);
        //        txtWeight.Text = Carb.ToString();
        //    }
        //    else
        //    {
        //        txtWeight.Text = "0";
        //    }
        //    //Database.planmealcustinvoice obj = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.CustomerID == CID && p.planid == planid && p.MealType == MID && p.MYPRODID == Prod);
        //    //txtWeight.Text = obj.ItemWeight.ToString();

        //    //txtsaleprice.Text = obj.Item_price.ToString();
        //    //drpProductListDay.SelectedValue = obj.DayNumber.ToString();
        //}
        public string GetProd(long PRODID)
        {
            if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == PRODID).Count() > 0)
            {
                string ProdName = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == PRODID).ProdName1;
                return ProdName;
            }
            else
            {
                return "Not Found";
            }
        }

        protected void txtBeingDate_TextChanged(object sender, EventArgs e)
        {
            lblMSGDate.Visible = false;

            DateTime CDT = Convert.ToDateTime(txtExpDate.Text);
            DateTime DT = Convert.ToDateTime(txtBeingDate.Text);

            if (DT.DayOfWeek == DayOfWeek.Friday)
            {
                lblMSGDate.Visible = true;
                lblMSGDate.Text = "Select Other than Friday";
                txtBeingDate.Focus();
                return;
            }

            if (DT.Date >= CDT.Date)
            { }
            else
            {
                lblMSGDate.Visible = true;
                lblMSGDate.Text = "Begin Date must be greater or Equal to the Contract Date";
                return;
            }
            DateTime dt1 = DT;
            setdate(dt1, 1);

            calculate();
            ViewState["ChangeBeingdate"] = true;
        }

        public void updateDtDatechange()
        {
            if (ViewState["MYTRANSID"] != null)
            {
                int MYTRANSID = Convert.ToInt32(ViewState["MYTRANSID"]);

                //using (TransactionScope scope = new TransactionScope())
                //{
                if (ViewState["ChangeBeingdate"] != null)
                {
                    List<Database.planmealcustinvoice> Listdt = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();

                    foreach (Database.planmealcustinvoice items in Listdt)
                    {
                        Database.planmealcustinvoice objdt = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items.DeliveryID && p.MYPRODID == MYTRANSID);
                        objdt.ACTIVE = false;
                        DB.SaveChanges();
                    }
                }

                //    scope.Complete();
                //}

            }
        }

        public void setdate(DateTime dt1, int day)
        {
            lbldelday1.Text = "";
            lbldelday2.Text = "";
            lbldelday3.Text = "";
            lbldelday4.Text = "";
            lbldelday5.Text = "";
            lbldelday6.Text = "";
            PositionNo = 1;
            string SDay = day.ToString();
            for (int i = 1; i <= 6; i++)
            {
                if (dt1.DayOfWeek != DayOfWeek.Friday)
                {
                    SDay = day.ToString();
                    setpositiondate(dt1, SDay);
                    dt1 = dt1.AddDays(1);
                    day = day + 1;
                }
                else
                {
                    dt1 = dt1.AddDays(1);
                    SDay = day.ToString();
                    setpositiondate(dt1, SDay);
                    dt1 = dt1.AddDays(1);
                    day = day + 1;
                }

            }

        }
        int PositionNo = 1;
        public void setpositiondate(DateTime dt1, string Sday)
        {
            string[] days1 = tags_1.Text.Split(',');
            foreach (string Val in days1)
            {
                if (Val == "Sat")
                {
                    if (dt1.DayOfWeek == DayOfWeek.Saturday)
                    {
                        string DT1 = Convert.ToInt32(dt1.Day) + " - " + Convert.ToInt32(dt1.Month);
                        lblDate1.Text = DT1;
                        string FDate1 = dt1.ToShortDateString();
                        lblFDate1.Text = FDate1;
                        lbldelday1.Text = PositionNo.ToString();
                        txtdelday1.Text = Sday;
                        PositionNo++;
                    }
                }
                else if (Val == "Sun")
                {
                    if (dt1.DayOfWeek == DayOfWeek.Sunday)
                    {
                        string DT1 = Convert.ToInt32(dt1.Day) + " - " + Convert.ToInt32(dt1.Month);
                        lblDate2.Text = DT1;
                        string FDate1 = dt1.ToShortDateString();
                        lblFDate2.Text = FDate1;
                        lbldelday2.Text = PositionNo.ToString();
                        txtdelday2.Text = Sday;
                        PositionNo++;
                    }
                }
                else if (Val == "Mon")
                {
                    if (dt1.DayOfWeek == DayOfWeek.Monday)
                    {
                        string DT1 = Convert.ToInt32(dt1.Day) + " - " + Convert.ToInt32(dt1.Month);
                        lblDate3.Text = DT1;
                        string FDate1 = dt1.ToShortDateString();
                        lblFDate3.Text = FDate1;
                        lbldelday3.Text = PositionNo.ToString();
                        txtdelday3.Text = Sday;
                        PositionNo++;
                    }
                }
                else if (Val == "Tue")
                {
                    if (dt1.DayOfWeek == DayOfWeek.Tuesday)
                    {
                        string DT1 = Convert.ToInt32(dt1.Day) + " - " + Convert.ToInt32(dt1.Month);
                        lblDate4.Text = DT1;
                        string FDate1 = dt1.ToShortDateString();
                        lblFDate4.Text = FDate1;
                        lbldelday4.Text = PositionNo.ToString();
                        txtdelday4.Text = Sday;
                        PositionNo++;
                    }
                }
                else if (Val == "Wed")
                {
                    if (dt1.DayOfWeek == DayOfWeek.Wednesday)
                    {
                        string DT1 = Convert.ToInt32(dt1.Day) + " - " + Convert.ToInt32(dt1.Month);
                        lblDate5.Text = DT1;
                        string FDate1 = dt1.ToShortDateString();
                        lblFDate5.Text = FDate1;
                        lbldelday5.Text = PositionNo.ToString();
                        txtdelday5.Text = Sday;
                        PositionNo++;
                    }
                }
                else if (Val == "Thu")
                {
                    if (dt1.DayOfWeek == DayOfWeek.Thursday)
                    {
                        string DT1 = Convert.ToInt32(dt1.Day) + " - " + Convert.ToInt32(dt1.Month);
                        lblDate6.Text = DT1;
                        string FDate1 = dt1.ToShortDateString();
                        lblFDate6.Text = FDate1;
                        lbldelday6.Text = PositionNo.ToString();
                        txtdelday6.Text = Sday;
                        PositionNo++;
                    }
                }

            }

        }

        protected void txtEndDate_TextChanged(object sender, EventArgs e)
        {
            lblMSGEdate.Visible = false;

            if (txtBeingDate.Text != "")
            {
                DateTime BDT = Convert.ToDateTime(txtBeingDate.Text);

                if (txtEndDate.Text != "")
                {
                    DateTime EDT = Convert.ToDateTime(txtEndDate.Text);
                    //DateTime WeekDT = BDT.AddDays(6);

                    //if (EDT.Date > WeekDT.Date)
                    //{ }
                    //else
                    //{
                    //    lblMSGEdate.Visible = true;
                    //    lblMSGEdate.Text = "Invalid End Date";
                    //    txtEndDate.Focus();
                    //    return;
                    //}
                    int week = Convert.ToInt32((EDT - BDT).TotalDays) / 7;
                    int HowManyDay = Convert.ToInt32(drpHowManyDay.SelectedValue);
                    int TotalDday = week * HowManyDay;
                    txtTotalDeliveryDay.Text = TotalDday.ToString();
                    txtDelivered.Text = TotalDday.ToString();
                    txtTotalWeek.Text = week.ToString();
                    //NoWeek();

                    calculate();

                    if (ViewState["MYTRANSID"] != null)
                    {
                        int MYTRANSID = Convert.ToInt32(ViewState["MYTRANSID"]);
                        if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DisplayWeek == 1).Count() > 0)
                        {
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Info, "please Recalculate Days if you complate First Week", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        }

                    }

                    //if (txtBeingDate.Text != "" && txtEndDate.Text != "")
                    //{
                    //    DateTime dt1 = Convert.ToDateTime(txtBeingDate.Text);
                    //    DateTime dt2 = Convert.ToDateTime(txtEndDate.Text);
                    //    TimeSpan TotalDeliveryDay = dt2.Subtract(dt1);
                    //    txtTotalDeliveryDay.Text = TotalDeliveryDay.TotalDays.ToString();
                    //}
                }
                else
                {
                    lblMSGEdate.Visible = true;
                    lblMSGEdate.Text = "Select End Date";
                    txtEndDate.Focus();
                    return;
                }
            }
            else
            {
                lblMSGEdate.Visible = true;
                lblMSGEdate.Text = "Invalid Beganing Date";
                txtBeingDate.Focus();
                return;
            }
        }
        public void NoWeek()
        {
            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";

            if (drpCustomer.SelectedValue != "0")
            {
                int CID = Convert.ToInt32(drpCustomer.SelectedValue);

                if (drpPlan.SelectedValue != "0")
                {
                    DrpWeek.Items.Clear();
                    DrpWeek.Items.Insert(0, new ListItem("-- Select --", "0"));
                    int Planid = Convert.ToInt32(drpPlan.SelectedValue);
                    if (switch1 == "AddOn")
                    {
                        if (DrpWeek.SelectedValue == "1")
                        { }
                        else
                        {
                            DrpWeek.Items.Insert(1, new ListItem("1", "1"));
                        }
                    }
                    else
                    {
                        List<Database.planmealcustinvoice> ListDT = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == Planid).GroupBy(p => p.NoOfWeek).Select(p => p.FirstOrDefault()).ToList();
                        if (ListDT.Count() > 0)
                        {
                            DrpWeek.DataSource = ListDT;
                            DrpWeek.DataTextField = "NoOfWeek";
                            DrpWeek.DataValueField = "NoOfWeek";
                            DrpWeek.DataBind();
                            //DrpWeek.Items.Insert(0, new ListItem("-- Select --", "0"));
                            //DrpWeek.SelectedIndex = 1;
                        }
                        else
                        {
                            if (DrpWeek.SelectedValue == "1")
                            { }
                            else
                            {
                                DrpWeek.Items.Insert(1, new ListItem("1", "1"));
                                DrpWeek.SelectedValue = "1";
                            }
                        }
                    }
                }
                else
                {
                    pnlErrorMsg.Visible = true;
                    lblerrorMsg.Text = "Select Plan";
                    return;
                }
            }
            else
            {
                pnlErrorMsg.Visible = true;
                lblerrorMsg.Text = "Select Customer";
                return;
            }

        }
        protected void drpDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int Day = Convert.ToInt32(drpDay.SelectedValue);
            //drpProductListDay.SelectedValue = Day.ToString();
            //DateTime DT = Convert.ToDateTime(txtBeingDate.Text);
            //if (Day == 1)
            //{
            //    DateTime Expdate = DT.AddDays(7 - 2);
            //    txtDelivered.Text = Expdate.ToShortDateString();
            //}
            //else
            //{
            //    DateTime Expdate = DT.AddDays(7 - 1);
            //    txtDelivered.Text = Expdate.ToShortDateString();
            //}
        }

        protected void drpweekofday_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";

            string Drpday = drpweekofday.SelectedItem.ToString();
            string[] dayCheck = tags_1.Text.Split(',');
            int DayCount = 0;
            foreach (string Val1 in dayCheck)
            {
                if (Val1 == Drpday)
                {
                    DayCount++;
                }
            }

            if (DayCount > 0)
            {
                return;
            }


            string tagdt = "";
            if (drpweekofday.SelectedValue != "0")
            {
                string drpval = drpweekofday.SelectedItem.ToString();
                string Vlaue = drpweekofday.SelectedValue;
                if (tags_1.Text != "")
                {
                    tagdt = tags_1.Text;
                    tagdt += "," + drpval;
                    tags_1.Text = tagdt;
                }
                else
                {
                    tagdt = drpval;
                    tags_1.Text = tagdt;
                }
                ViewState["SaveList1"] = tags_1.Text;

                string[] days = tags_1.Text.Split(',');
                int count = Convert.ToInt32(days.Count());
                ViewState["count"] = count;
                drpHowManyDay.Items.Clear();
                drpHowManyDay.Items.Add(new ListItem(count.ToString(), count.ToString()));

                List<tempweekofday> tempTable = new List<tempweekofday>();
                foreach (string Val in days)
                {
                    tempweekofday obj = new tempweekofday();
                    if (Val == "Sat")
                    {
                        obj.Name = "1.Saturday";
                        obj.ID = 1;
                        tempTable.Add(obj);
                    }
                    else if (Val == "Sun")
                    {
                        obj.Name = "2.Sunday";
                        obj.ID = 2;
                        tempTable.Add(obj);
                    }
                    else if (Val == "Mon")
                    {
                        obj.Name = "3.Monday";
                        obj.ID = 3;
                        tempTable.Add(obj);
                    }
                    else if (Val == "Tue")
                    {
                        obj.Name = "4.Tusday";
                        obj.ID = 4;
                        tempTable.Add(obj);
                    }
                    else if (Val == "Wed")
                    {
                        obj.Name = "5.Wednesday";
                        obj.ID = 5;
                        tempTable.Add(obj);
                    }
                    else if (Val == "Thu")
                    {
                        obj.Name = "6.Thursday";
                        obj.ID = 6;
                        tempTable.Add(obj);
                    }
                }
                drpDay.Items.Clear();
                drpDay.DataSource = tempTable;
                drpDay.DataTextField = "Name";
                drpDay.DataValueField = "ID";
                drpDay.DataBind();
                drpDay.Items.Insert(0, new ListItem("-- Select --", "0"));

                if (drpCustomer.SelectedValue != "0")
                {
                    if (drpPlan.SelectedValue != "0")
                    {
                        GetProductList();
                    }
                    else
                    {
                        pnlErrorMsg.Visible = true;
                        lblerrorMsg.Text = "Select Plan";
                        return;
                    }
                }
                else
                {
                    pnlErrorMsg.Visible = true;
                    lblerrorMsg.Text = "Select Customer";
                    return;
                }


                int daycount = 0;
                int DayCount1 = 0;
                string[] dayscount = tags_1.Text.Split(',');
                foreach (string Val in dayscount)
                {
                    DayCount1++;
                    if (Val == "Sat" && lbldelday1.Text != "")
                    {
                        daycount++;
                    }

                    if (Val == "Sun" && lbldelday2.Text != "")
                    {
                        daycount++;
                    }

                    if (Val == "Mon" && lbldelday3.Text != "")
                    {
                        daycount++;
                    }

                    if (Val == "Tue" && lbldelday4.Text != "")
                    {
                        daycount++;
                    }

                    if (Val == "Wed" && lbldelday5.Text != "")
                    {
                        daycount++;
                    }

                    if (Val == "Thu" && lbldelday6.Text != "")
                    {
                        daycount++;
                    }
                }

                if (daycount == DayCount1)
                {
                    btnsetday.Visible = false;
                }
                else
                {
                    btnsetday.Visible = false;//true
                }
            }
            if (txtBeingDate.Text != "" && txtEndDate.Text != "")
            {
                calculate();
                //int TimeCount = Convert.ToInt32(ViewState["TimeCount"]);
                //DateTime BDT = Convert.ToDateTime(txtBeingDate.Text);
                //DateTime EDT = Convert.ToDateTime(txtEndDate.Text);
                //int week = Convert.ToInt32((EDT - BDT).TotalDays) / 7;
                //int HowManyDay = Convert.ToInt32(drpHowManyDay.SelectedValue);
                //int TotalDday = week * HowManyDay;
                //txtTotalWeek.Text = week.ToString();
                //txtTotalDeliveryDay.Text = TotalDday.ToString();
                //txtRemainingDay.Text = (TotalDday * TimeCount).ToString();
                //txtDelivered.Text = (TotalDday * TimeCount).ToString();
            }

            drpweekofday.SelectedIndex = 0;

        }

        public void calculate()
        {
            if (txtBeingDate.Text != "" && txtEndDate.Text != "" && ViewState["TimeCount"] != null)
            {
                if (txtDelTotalDay.Text != "")
                {
                    int EnterDay = txtDelTotalDay.Text != "" ? Convert.ToInt32(txtDelTotalDay.Text) : 0;
                    int TimeCount = Convert.ToInt32(ViewState["TimeCount"]);
                    DateTime BDT = Convert.ToDateTime(txtBeingDate.Text);
                    DateTime EDT = Convert.ToDateTime(txtEndDate.Text);

                    int DayCount1 = 0;
                    string[] dayscount = tags_1.Text.Split(',');
                    foreach (string Val in dayscount)
                    {
                        DayCount1++;
                    }
                    int HowManyDay = DayCount1;
                    decimal Week1 = EnterDay / HowManyDay;

                    decimal Remainder = EnterDay % HowManyDay;

                    int week = 0;
                    if (Remainder > 0)
                    {
                        Week1 = Week1 + 1;
                        week = Convert.ToInt32(Math.Ceiling(Week1));
                    }
                    else
                    {
                        week = Convert.ToInt32(Math.Ceiling(Week1));
                    }

                    txtTotalWeek.Text = week.ToString();
                    txtTotalDeliveryDay.Text = EnterDay.ToString();
                    int Time = Convert.ToInt32(lblDeliveryTIME.Text);
                    int Remaining = Time * EnterDay;
                    txtRemainingDay.Text = Remaining.ToString(); //(TotalDday * TimeCount).ToString();
                }
                else
                {
                    if (txtEndDate.Text != "")
                    {
                        int TimeCount = Convert.ToInt32(ViewState["TimeCount"]);
                        DateTime BDT = Convert.ToDateTime(txtBeingDate.Text);
                        DateTime EDT = Convert.ToDateTime(txtEndDate.Text);
                        EDT = EDT.AddDays(1);
                        decimal Week1 = Convert.ToDecimal((EDT - BDT).TotalDays) / 7;
                        int week = Convert.ToInt32(Math.Ceiling(Week1));
                        //int week = Convert.ToInt32((EDT - BDT).TotalDays) / 7;
                        //int HowManyDay = Convert.ToInt32(drpHowManyDay.SelectedValue);
                        int DayCount1 = 0;
                        string[] dayscount = tags_1.Text.Split(',');
                        DayCount1 = dayscount.Length;
                        int HowManyDay = DayCount1;
                        int TotalDday = week * HowManyDay;
                        //int TotalDday = txtDelTotalDay.Text != "" ? Convert.ToInt32(txtDelTotalDay.Text) : week * HowManyDay;
                        txtTotalWeek.Text = week.ToString();
                        txtTotalDeliveryDay.Text = TotalDday.ToString();
                        int Time = Convert.ToInt32(lblDeliveryTIME.Text);

                        txtRemainingDay.Text = (TotalDday * TimeCount).ToString();
                    }

                }

                NoWeek();


                //int TimeCount = Convert.ToInt32(ViewState["TimeCount"]);
                //DateTime BDT = Convert.ToDateTime(txtBeingDate.Text);
                //DateTime EDT = Convert.ToDateTime(txtEndDate.Text);
                //decimal Week1 = Convert.ToDecimal((EDT - BDT).TotalDays) / 7;
                //int week = Convert.ToInt32(Math.Ceiling(Week1));
                ////int week = Convert.ToInt32((EDT - BDT).TotalDays) / 7;
                //int HowManyDay = Convert.ToInt32(drpHowManyDay.SelectedValue);
                //int TotalDday = week * HowManyDay;
                ////int TotalDday = txtDelTotalDay.Text != "" ? Convert.ToInt32(txtDelTotalDay.Text) : week * HowManyDay;
                //txtTotalWeek.Text = week.ToString();
                //txtTotalDeliveryDay.Text = EnterDay.ToString();
                //txtRemainingDay.Text = EnterDay.ToString(); //(TotalDday * TimeCount).ToString();
                txtDelivered.Text = "0"; //(TotalDday * TimeCount).ToString();
            }
        }

        protected void drpCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";

            int CID = Convert.ToInt32(drpCustomer.SelectedValue);

            if (CID != 0)
            {
                if (DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.CustomerID == CID).Count() > 0)
                {
                    var result1 = (from pm in DB.planmealcustinvoices.Where(p => p.TenentID == TID)
                                   join
                                     ur in DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.CustomerID == CID && p.ACTIVE == true) on pm.MYTRANSID equals ur.MYTRANSID

                                   select new { ur.MYTRANSID, ur.CustomerID, ur.planid, pm.DeliveryMeal, pm.DeliveryID, ur.ContractID, ur.StartDate, ur.EndDate }).ToList();
                    var List1 = result1.GroupBy(p => p.MYTRANSID).Select(p => p.FirstOrDefault()).ToList();
                    ListViewContract.DataSource = List1.OrderByDescending(p => p.MYTRANSID);
                    ListViewContract.DataBind();

                    ModalPopupExtenderContract.Show();

                }

                List<Database.tblProduct_Plan> ListtblProduct_Plan = new List<Database.tblProduct_Plan>();
                List<Database.planmealsetup> Listplanmealsetup = DB.planmealsetups.Where(p => p.ACTIVE == true && p.TenentID == TID).ToList();
                foreach (Database.planmealsetup item in Listplanmealsetup.GroupBy(p => p.planid).Select(p => p.FirstOrDefault()))
                {
                    if (DB.tblProduct_Plan.Where(p => p.planid == item.planid && p.TenentID == TID).Count() > 0)
                    {
                        if (DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.customerID == CID && p.planid == item.planid && p.LikeType == "M" && p.active == true).Count() > 0)
                        {
                            Database.tblProduct_Plan obj = DB.tblProduct_Plan.Single(p => p.planid == item.planid && p.TenentID == TID);
                            ListtblProduct_Plan.Add(obj);
                        }
                    }
                }

                List<Database.planmealcustinvoiceHD> ListHD = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.CustomerID == CID).ToList();

                foreach (Database.planmealcustinvoiceHD items in ListHD)
                {
                    Database.tblProduct_Plan obj = DB.tblProduct_Plan.Single(p => p.planid == items.planid && p.TenentID == TID);
                    ListtblProduct_Plan.Remove(obj);
                }

                drpPlan.DataSource = ListtblProduct_Plan.OrderBy(p => p.planname1);
                drpPlan.DataTextField = "planname1";
                drpPlan.DataValueField = "planid";
                drpPlan.DataBind();
                drpPlan.Items.Insert(0, new ListItem("-- Select --", "0"));

                if (ListtblProduct_Plan.Count() > 0)
                {
                    drpPlan.SelectedIndex = 1;
                }

                DeliveryTime();

            }
            else
            {
                pnlErrorMsg.Visible = true;
                lblerrorMsg.Text = "Select Customer";
                return;
            }
        }
        public void DeliveryTime()
        {
            int CID = Convert.ToInt32(drpCustomer.SelectedValue);
            int Planid = Convert.ToInt32(drpPlan.SelectedValue);
            List<Database.tblProduct_Plan> ListtblProduct_Plan = DB.tblProduct_Plan.Where(p => p.TenentID == TID && p.planid == Planid).ToList();
            if (ListtblProduct_Plan.Where(p => p.TenentID == TID && p.planid == Planid).Count() > 0)
            {
                int PlanPrice = ListtblProduct_Plan.Single(p => p.TenentID == TID && p.planid == Planid).Plan_price1;
                txtTotalPlanPrice.Text = PlanPrice.ToString();
            }
            else
            {
                txtTotalPlanPrice.Text = "0";
            }

            List<Database.REFTABLE> ListDelevaryTime = new List<Database.REFTABLE>();
            List<Database.tblcontact_addon1_dtl> OBJDelevaryTime = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.LikeType == "M" && p.customerID == CID && p.planid == Planid && p.active == true).GroupBy(p => p.DeliveryTime).Select(p => p.FirstOrDefault()).ToList();
            List<Database.REFTABLE> ListDelevary = DB.REFTABLEs.Where(p => p.TenentID == TID).ToList();
            foreach (Database.tblcontact_addon1_dtl item in OBJDelevaryTime)
            {
                int DeliveryTime = Convert.ToInt32(item.DeliveryTime);
                if (DeliveryTime != 0)
                {
                    Database.REFTABLE obj = ListDelevary.Single(p => p.TenentID == TID && p.REFID == DeliveryTime);
                    ListDelevaryTime.Add(obj);
                }

            }

            //drpDeliveryTime.DataSource = ListDelevaryTime.OrderBy(p => p.REFNAME1);//DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.customerID == CID && p.active == true).GroupBy(p=>p.DeliveryTime).Select(p=>p.FirstOrDefault());
            //drpDeliveryTime.DataTextField = "REFNAME1";
            //drpDeliveryTime.DataValueField = "REFID";
            //drpDeliveryTime.DataBind();
            //drpDeliveryTime.Items.Insert(0, new ListItem("-- Select --", "0"));
            int TimeCount = ListDelevaryTime.Count();
            ViewState["TimeCount"] = TimeCount;

            if (TimeCount > 0)
            {
                lblDeliveryTIME.Text = TimeCount.ToString();
                //drpDeliveryTime.SelectedIndex = 1;
                lblDeliveryTIMEID.Text = ListDelevaryTime.FirstOrDefault().REFID.ToString();
                DeloveryMeal();
            }

            int CustomerID = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).COMPID;
            lblCID.Text = CustomerID.ToString();
        }
        protected void drpDeliveryTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (switch1 == "editON")
            {
                pnlErrorMsg.Visible = true;
                lblerrorMsg.Text = "Please Update Or Cancel your Current Transection";
                return;
            }
            if (drpPlan.SelectedValue != "0")
            {
                DeloveryMeal();
                drpDeliveryMeal.Enabled = true;
            }

        }
        public void DeloveryMeal()
        {
            int CID = Convert.ToInt32(drpCustomer.SelectedValue);
            //string Delevery = drpDeliveryTime.SelectedValue;
            string Delevery = lblDeliveryTIMEID.Text;
            int plan = Convert.ToInt32(drpPlan.SelectedValue);
            List<Database.REFTABLE> ListREFTABLE = new List<Database.REFTABLE>();
            List<Database.tblcontact_addon1_dtl> Listplanmealsetup = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.customerID == CID && p.planid == plan && p.active == true).ToList();//&& p.DeliveryTime == Delevery
            foreach (Database.tblcontact_addon1_dtl item2 in Listplanmealsetup)
            {
                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == item2.ItemCode).Count() > 0)
                {
                    Database.REFTABLE obj2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == item2.ItemCode);
                    ListREFTABLE.Add(obj2);
                }
            }

            int Deltime = Convert.ToInt32(lblDeliveryTIMEID.Text);
            List<Database.planmealcustinvoice> ListInvo = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.DeliveryTime == Deltime).GroupBy(p => p.MealType).Select(p => p.FirstOrDefault()).ToList();

            foreach (Database.planmealcustinvoice items in ListInvo)
            {
                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == items.DeliveryMeal).Count() > 0)
                {
                    Database.REFTABLE obj2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == items.DeliveryMeal);
                    ListREFTABLE.Remove(obj2);
                }
            }

            drpDeliveryMeal.DataSource = ListREFTABLE.OrderBy(p => p.SWITCH1);
            drpDeliveryMeal.DataTextField = "REFNAME1";
            drpDeliveryMeal.DataValueField = "REFID";
            drpDeliveryMeal.DataBind();
            drpDeliveryMeal.Items.Insert(0, new ListItem("-- Select --", "0"));

            if (ListREFTABLE.Count() > 0)
            {
                btnAddmeal.Enabled = true;
            }
            else
            {
                btnAddmeal.Enabled = false;
            }

        }

        public void EditDeloveryMeal()
        {
            int CID = Convert.ToInt32(drpCustomer.SelectedValue);
            int Planid = Convert.ToInt32(drpPlan.SelectedValue);
            string Delevery = lblDeliveryTIMEID.Text;
            List<Database.REFTABLE> ListREF = DB.REFTABLEs.Where(p => p.TenentID == TID).ToList();
            List<Database.REFTABLE> ListREFTABLE = new List<Database.REFTABLE>();
            List<Database.planmealcustinvoice> Listplanmealsetup = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == Planid).GroupBy(p => p.DeliveryMeal).Select(p => p.FirstOrDefault()).ToList();// && p.ACTIVE == true
            foreach (Database.planmealcustinvoice item2 in Listplanmealsetup)
            {
                if (ListREF.Where(p => p.TenentID == TID && p.REFID == item2.DeliveryMeal).Count() > 0)
                {
                    Database.REFTABLE obj2 = ListREF.Single(p => p.TenentID == TID && p.REFID == item2.DeliveryMeal);
                    ListREFTABLE.Add(obj2);
                }
            }
            drpDeliveryMeal.DataSource = ListREFTABLE.OrderBy(p => p.SWITCH1);
            drpDeliveryMeal.DataTextField = "REFNAME1";
            drpDeliveryMeal.DataValueField = "REFID";
            drpDeliveryMeal.DataBind();

            List<Database.tblcontact_addon1_dtl> Listtblcontact_addon1_dtl = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.customerID == CID && p.planid == Planid && p.DeliveryTime == Delevery && p.active == true).ToList();
            int total = Listtblcontact_addon1_dtl.Count();
            int found = ListREFTABLE.Count();
            if (total > found)
            {
                btnAddmeal.Enabled = true;
            }
            else
            {
                btnAddmeal.Enabled = false;
            }

            //drpDeliveryMeal.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void btnAddmeal_Click(object sender, EventArgs e)
        {
            //drpDeliveryTime.Enabled = true;
            drpDeliveryMeal.Enabled = true;
            btnAddmeal.Visible = false;
            btnAdd.Enabled = true;
            btnAdd.Text = "Save";
            btnAdd.ValidationGroup = "submit";
            int CID = Convert.ToInt32(drpCustomer.SelectedValue);
            string Delevery = lblDeliveryTIMEID.Text;
            List<Database.REFTABLE> ListREFTABLE = new List<Database.REFTABLE>();
            List<Database.tblcontact_addon1_dtl> Listplanmealsetup = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.customerID == CID && p.active == true).ToList();//&& p.DeliveryTime == Delevery
            foreach (Database.tblcontact_addon1_dtl item2 in Listplanmealsetup)
            {
                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == item2.ItemCode).Count() > 0)
                {
                    Database.REFTABLE obj2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == item2.ItemCode);
                    ListREFTABLE.Add(obj2);
                }
            }

            int Deltime = Convert.ToInt32(lblDeliveryTIMEID.Text);
            List<Database.planmealcustinvoice> ListInvo = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.DeliveryTime == Deltime).GroupBy(p => p.MealType).Select(p => p.FirstOrDefault()).ToList();

            foreach (Database.planmealcustinvoice items in ListInvo)
            {
                if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == items.DeliveryMeal).Count() > 0)
                {
                    Database.REFTABLE obj2 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == items.DeliveryMeal);
                    ListREFTABLE.Remove(obj2);
                }
            }

            drpDeliveryMeal.DataSource = ListREFTABLE.OrderBy(p => p.SWITCH1);
            drpDeliveryMeal.DataTextField = "REFNAME1";
            drpDeliveryMeal.DataValueField = "REFID";
            drpDeliveryMeal.DataBind();
            drpDeliveryMeal.Items.Insert(0, new ListItem("-- Select --", "0"));


        }

        protected void ListView2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int planid = Convert.ToInt32(drpPlan.SelectedValue);
            int MID = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
            CheckBox RDODay1 = (CheckBox)e.Item.FindControl("RDODay1");
            CheckBox RDODay2 = (CheckBox)e.Item.FindControl("RDODay2");
            CheckBox RDODay3 = (CheckBox)e.Item.FindControl("RDODay3");
            CheckBox RDODay4 = (CheckBox)e.Item.FindControl("RDODay4");
            CheckBox RDODay5 = (CheckBox)e.Item.FindControl("RDODay5");
            CheckBox RDODay6 = (CheckBox)e.Item.FindControl("RDODay6");

            TextBox txtWeight = (TextBox)e.Item.FindControl("txtWeight");
            TextBox txtCalories = (TextBox)e.Item.FindControl("txtCalories");
            TextBox txtProtain = (TextBox)e.Item.FindControl("txtProtain");
            TextBox txtCarbs = (TextBox)e.Item.FindControl("txtCarbs");
            TextBox txtFat = (TextBox)e.Item.FindControl("txtFat");

            TextBox txtsaleprice = (TextBox)e.Item.FindControl("txtsaleprice");
            Label lblProd = (Label)e.Item.FindControl("lblProd");
            int Prod = Convert.ToInt32(lblProd.Text);

            int CID = Convert.ToInt32(drpCustomer.SelectedValue);
            int MYTRANSID = Convert.ToInt32(ViewState["MYTRANSID"]);

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                if (btnAdd.Text == "Save")
                {
                    if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == Prod).Count() > 0)
                    {
                        decimal SalePrice = Convert.ToDecimal(DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == Prod).msrp);
                        txtsaleprice.Text = SalePrice.ToString();
                    }
                    else
                    {
                        txtsaleprice.Text = "0.00";
                    }

                    string Weight = "100";
                    string Calories = "300";
                    string Protain = "14";
                    string Carbs = "36";
                    string Fat = "9";
                    List<Database.planmealsetup> ListPlan = DB.planmealsetups.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == planid && p.MealType == MID && p.MYPRODID == Prod).ToList();
                    if (ListPlan.Count() > 0)
                    {
                        Database.planmealsetup objplan = ListPlan.Single(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == planid && p.MealType == MID && p.MYPRODID == Prod);

                        Weight = objplan.ItemWeight != null ? objplan.ItemWeight.ToString() : "100";
                        Calories = objplan.Calories != null ? objplan.Calories.ToString() : "300";
                        Protain = objplan.Protein != null ? objplan.Protein.ToString() : "14";
                        Carbs = objplan.Carbs != null ? objplan.Carbs.ToString() : "36";
                        Fat = objplan.Fat != null ? objplan.Fat.ToString() : "9";
                    }

                    if (DB.planmealsetups.Where(p => p.TenentID == TID && p.planid == planid && p.MealType == MID && p.MYPRODID == Prod).Count() > 0)
                    {
                        Database.planmealsetup Objplanmealsetup = DB.planmealsetups.Single(p => p.TenentID == TID && p.planid == planid && p.MealType == MID && p.MYPRODID == Prod);

                        txtWeight.Text = Objplanmealsetup.ItemWeight != null ? Objplanmealsetup.ItemWeight.ToString() : Weight;
                        txtCalories.Text = Objplanmealsetup.Calories != null ? Objplanmealsetup.Calories.ToString() : Calories;
                        txtProtain.Text = Objplanmealsetup.Protein != null ? Objplanmealsetup.Protein.ToString() : Protain;
                        txtCarbs.Text = Objplanmealsetup.Carbs != null ? Objplanmealsetup.Carbs.ToString() : Carbs;
                        txtFat.Text = Objplanmealsetup.Fat != null ? Objplanmealsetup.Fat.ToString() : Fat;
                    }
                    else
                    {
                        txtWeight.Text = Weight;
                        txtCalories.Text = Calories;
                        txtProtain.Text = Protain;
                        txtCarbs.Text = Carbs;
                        txtFat.Text = Fat;
                    }
                }

            }
            string[] days = tags_1.Text.Split(',');
            foreach (string Val1 in days)
            {
                if (Val1 == "Sat")
                {
                    RDODay1.Enabled = true;
                }

                if (Val1 == "Sun")
                {
                    RDODay2.Enabled = true;
                }

                if (Val1 == "Mon")
                {
                    RDODay3.Enabled = true;
                }

                if (Val1 == "Tue")
                {
                    RDODay4.Enabled = true;
                }

                if (Val1 == "Wed")
                {
                    RDODay5.Enabled = true;
                }

                if (Val1 == "Thu")
                {
                    RDODay6.Enabled = true;
                }
            }
        }

        public int Get_MealRepeatInDay(int planid, int MealType)
        {
            int DayProd = 0;
            List<Database.planmealsetup> ListPlan = DB.planmealsetups.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == planid && p.MealType == MealType).ToList();
            if (ListPlan.Count() > 0)
            {
                DayProd = Convert.ToInt32(ListPlan.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == planid && p.MealType == MealType).FirstOrDefault().MealRepeatInDay);
            }

            return DayProd;
        }

        protected void RDODay1_CheckedChanged(object sender, EventArgs e)
        {

            Editswicth();

            int SelectCount = 0;
            int planid = Convert.ToInt32(drpPlan.SelectedValue);
            int MealType = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
            int DayProd = Get_MealRepeatInDay(planid, MealType);

            for (int i = 0; i < ListView2.Items.Count; i++)
            {
                CheckBox RDODay1 = (CheckBox)ListView2.Items[i].FindControl("RDODay1");
                if (RDODay1.Checked == true)
                {
                    SelectCount++;
                }
            }

            if (DayProd < SelectCount)
            {
                string Planname = drpPlan.SelectedItem.ToString();
                string meal = drpDeliveryMeal.SelectedItem.ToString();
                pnlErrorMsg1.Visible = true;
                lblerrorMsg1.Text = "<B><U>" + Planname + "</U></B>  For  <b><U> " + meal + " </U></B> are allowed to have only " + DayProd + " Item for " + lblDate1.Text + " . Kindly Remove more than Allowed ?";
                return;
            }

        }
        protected void RDODay2_CheckedChanged(object sender, EventArgs e)
        {
            Editswicth();

            int SelectCount = 0;
            int planid = Convert.ToInt32(drpPlan.SelectedValue);
            int MealType = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
            int DayProd = Get_MealRepeatInDay(planid, MealType);

            for (int i = 0; i < ListView2.Items.Count; i++)
            {
                CheckBox RDODay2 = (CheckBox)ListView2.Items[i].FindControl("RDODay2");
                if (RDODay2.Checked == true)
                {
                    SelectCount++;
                }
            }

            if (DayProd < SelectCount)
            {
                string Planname = drpPlan.SelectedItem.ToString();
                string meal = drpDeliveryMeal.SelectedItem.ToString();
                pnlErrorMsg1.Visible = true;
                lblerrorMsg1.Text = "<B><U>" + Planname + "</U></B>  For  <b><U> " + meal + " </U></B>  are allowed to have only " + DayProd + " Item for " + lblDate2.Text + " . Kindly Remove more than Allowed ?";
                return;
            }
        }
        protected void RDODay3_CheckedChanged(object sender, EventArgs e)
        {
            Editswicth();

            int SelectCount = 0;
            int planid = Convert.ToInt32(drpPlan.SelectedValue);
            int MealType = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
            int DayProd = Get_MealRepeatInDay(planid, MealType);

            for (int i = 0; i < ListView2.Items.Count; i++)
            {
                CheckBox RDODay3 = (CheckBox)ListView2.Items[i].FindControl("RDODay3");
                if (RDODay3.Checked == true)
                {
                    SelectCount++;
                }
            }

            if (DayProd < SelectCount)
            {
                string Planname = drpPlan.SelectedItem.ToString();
                string meal = drpDeliveryMeal.SelectedItem.ToString();
                pnlErrorMsg1.Visible = true;
                lblerrorMsg1.Text = "<B><U>" + Planname + "</U></B>  For  <b><U> " + meal + " </U></B> are allowed to have only " + DayProd + " Item for " + lblDate3.Text + " . Kindly Remove more than Allowed ?";
                return;
            }
        }
        protected void RDODay4_CheckedChanged(object sender, EventArgs e)
        {
            Editswicth();

            int SelectCount = 0;
            int planid = Convert.ToInt32(drpPlan.SelectedValue);
            int MealType = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
            int DayProd = Get_MealRepeatInDay(planid, MealType);

            for (int i = 0; i < ListView2.Items.Count; i++)
            {
                CheckBox RDODay4 = (CheckBox)ListView2.Items[i].FindControl("RDODay4");
                if (RDODay4.Checked == true)
                {
                    SelectCount++;
                }
            }

            if (DayProd < SelectCount)
            {
                string Planname = drpPlan.SelectedItem.ToString();
                string meal = drpDeliveryMeal.SelectedItem.ToString();
                pnlErrorMsg1.Visible = true;
                lblerrorMsg1.Text = "<B><U>" + Planname + "</U></B>  For  <b><U> " + meal + " </U></B>  are allowed to have only " + DayProd + " Item for " + lblDate4.Text + " . Kindly Remove more than Allowed ?";
                return;
            }
        }
        protected void RDODay5_CheckedChanged(object sender, EventArgs e)
        {
            Editswicth();

            int SelectCount = 0;
            int planid = Convert.ToInt32(drpPlan.SelectedValue);
            int MealType = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
            int DayProd = Get_MealRepeatInDay(planid, MealType);

            for (int i = 0; i < ListView2.Items.Count; i++)
            {
                CheckBox RDODay5 = (CheckBox)ListView2.Items[i].FindControl("RDODay5");
                if (RDODay5.Checked == true)
                {
                    SelectCount++;
                }
            }

            if (DayProd < SelectCount)
            {
                string Planname = drpPlan.SelectedItem.ToString();
                string meal = drpDeliveryMeal.SelectedItem.ToString();
                pnlErrorMsg1.Visible = true;
                lblerrorMsg1.Text = "<B><U>" + Planname + "</U></B>  For  <b><U> " + meal + " </U></B> are allowed to have only " + DayProd + " Item for " + lblDate5.Text + " . Kindly Remove more than Allowed ?";
                return;
            }
        }
        protected void RDODay6_CheckedChanged(object sender, EventArgs e)
        {
            Editswicth();

            int SelectCount = 0;
            int planid = Convert.ToInt32(drpPlan.SelectedValue);
            int MealType = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
            int DayProd = Get_MealRepeatInDay(planid, MealType);

            for (int i = 0; i < ListView2.Items.Count; i++)
            {
                CheckBox RDODay6 = (CheckBox)ListView2.Items[i].FindControl("RDODay6");
                if (RDODay6.Checked == true)
                {
                    SelectCount++;
                }
            }

            if (DayProd < SelectCount)
            {
                string Planname = drpPlan.SelectedItem.ToString();
                string meal = drpDeliveryMeal.SelectedItem.ToString();
                pnlErrorMsg1.Visible = true;
                lblerrorMsg1.Text = "<B><U>" + Planname + "</U></B>  For  <b><U> " + meal + " </U></B>  are allowed to have only " + DayProd + " Item for " + lblDate6.Text + " . Kindly Remove more than Allowed ?";
                return;
            }
        }
        public void Editswicth()
        {
            pnlErrorMsg1.Visible = false;
            btnAdd.Enabled = true;
            lblerrorMsg1.Text = "";
            //editSwitch = true;
            switch1 = "editON";
            SwitchoONOFF("editON");
        }
        protected void btnCopyFullPlan_Click(object sender, EventArgs e)
        {
            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";

            if (DrpWeek.SelectedValue != "1")
            {
                pnlErrorMsg.Visible = true;
                lblerrorMsg.Text = "Please Select Week 1";
                return;
            }

            bool Flag = checkradio();
            if (Flag == false)
            {
                return;
            }

            if (switch1 == "editON")
            {
                pnlErrorMsg.Visible = true;
                lblerrorMsg.Text = "Please Update Or Cancel your Current Transection";
                return;
            }



            if (ViewState["MYTRANSID"] != null)
            {
                int MYTRANSID = Convert.ToInt32(ViewState["MYTRANSID"]);
                int CID = Convert.ToInt32(drpCustomer.SelectedValue);
                int plan = Convert.ToInt32(drpPlan.SelectedValue);
                int Week = Convert.ToInt32(txtTotalWeek.Text);

                if (MYTRANSID != 0)
                {

                    // Changes 10 March 2018 to be done not yet started
                    // Copy full plan to be called with a StartCopyPlanWeek 
                    // Copy Full plan start date must be StartCopyPlanWeek +1

                    Copyfullplan(MYTRANSID, Week, plan);
                }
                else
                {
                    pnlErrorMsg.Visible = true;
                    lblerrorMsg.Text = "Please Select any contract From List";
                    return;
                }

                //GetProductList();
                DrpWeek.SelectedValue = "1";
                int Meal = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
                Editinvoice(CID, MYTRANSID, plan, Meal);

                if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DisplayWeek == 1).Count() > 0)
                {
                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Info, "please press Recalculate Button if Data not Currect", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                }


            }
            else
            {
                Response.Redirect("/Master/HBInvoice.aspx");
            }

        }

        public void copyplan(int MYTRANSID)
        {
            List<Database.planmealcustinvoice> ListMain = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
            List<Database.planmealcustinvoice> Listinvoice = ListMain.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DisplayWeek == 1).OrderBy(p => p.DayNumber).ToList();
            DateTime beginDate = Convert.ToDateTime(txtBeingDate.Text);
            int FriDay = 0;

            foreach (Database.planmealcustinvoice items in Listinvoice)
            {
                //List<Database.planmealcustinvoice> ListExist = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryMeal == items.DeliveryMeal && p.MYPRODID == items.MYPRODID && p.DayNumber == items.DayNumber).ToList();
                //if (ListExist.Count() < 1)
                //{
                Database.planmealcustinvoice objinvice = new planmealcustinvoice();
                objinvice.TenentID = TID;
                objinvice.LOCATION_ID = LID;
                objinvice.MYTRANSID = MYTRANSID;
                objinvice.DeliveryID = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Max(p => p.DeliveryID) + 1) : 1;
                objinvice.CustomerID = items.CustomerID;
                objinvice.planid = items.planid;
                objinvice.MealType = items.MealType;
                objinvice.MYPRODID = items.MYPRODID;
                objinvice.ProdName1 = items.ProdName1;
                objinvice.DayNumber = items.DayNumber;
                objinvice.OprationDay = items.OprationDay;
                objinvice.TransID = items.TransID;
                objinvice.ContractID = items.ContractID;
                objinvice.WeekofDay = items.WeekofDay;
                objinvice.TotalWeek = items.TotalWeek;
                objinvice.NameOfDay = items.NameOfDay;
                objinvice.NoOfWeek = items.NoOfWeek;
                objinvice.DisplayWeek = 1;
                objinvice.TotalDeliveryDay = items.TotalDeliveryDay;
                objinvice.ActualDeliveryDay = items.ActualDeliveryDay;
                objinvice.ExpectedDeliveryDay = items.ExpectedDeliveryDay;
                objinvice.DeliveryTime = items.DeliveryTime;
                objinvice.DeliveryMeal = items.DeliveryMeal;
                objinvice.DriverID = items.DriverID;
                objinvice.StartDate = beginDate;
                DateTime StartDate = beginDate;
                objinvice.EndDate = Convert.ToDateTime(txtEndDate.Text);
                int Day = Convert.ToInt32(items.DayNumber) - 1;
                DateTime Fexp = StartDate.AddDays(Day);
                DateTime fexp1 = StartDate.AddDays(Day);
                if (Fexp.DayOfWeek == DayOfWeek.Friday)
                {
                    Fexp = Fexp.AddDays(1);
                }

                if (FriDay == 1 && fexp1.DayOfWeek != DayOfWeek.Friday)
                {
                    Fexp = Fexp.AddDays(1);
                    objinvice.ExpectedDelDate = Fexp;
                    objinvice.NExtDeliveryDate = Fexp.AddDays(1);
                }
                else
                {
                    objinvice.ExpectedDelDate = Fexp;
                    objinvice.NExtDeliveryDate = Fexp.AddDays(1);
                }

                if (fexp1.DayOfWeek == DayOfWeek.Friday)
                {
                    FriDay = 1;
                }
                objinvice.SubscriptonDayNumber = items.SubscriptonDayNumber;
                objinvice.Status = "Pending";
                objinvice.Calories = items.Calories;
                objinvice.Carbs = items.Carbs;
                objinvice.Protein = items.Protein;
                objinvice.Fat = items.Fat;
                objinvice.ItemWeight = items.ItemWeight;
                objinvice.Qty = items.Qty;
                objinvice.Item_cost = items.Item_cost;
                objinvice.Item_price = items.Item_price;
                objinvice.Total_price = items.Total_price;
                objinvice.ShortRemark = items.ShortRemark;
                objinvice.ACTIVE = items.ACTIVE;
                objinvice.ChangesDate = items.ChangesDate;
                objinvice.Switch3 = items.Switch3;
                //int CRUP_ID = GlobleClass.EncryptionHelpers.WriteLog("planmealcustinvoice,INSERT: " + Log, "INSERT", "planmealcustinvoice", UID.ToString(), MenuID);
                //objinvice.CRUP_ID = CRUP_ID;

                DB.planmealcustinvoices.AddObject(objinvice);
                DB.SaveChanges();
                //}

            }
        }

        public void Copyfullplan(int MYTRANSID, int Week, int plan)
        {
            int daycount = 0;
            string[] days1 = tags_1.Text.Split(',');

            daycount = days1.Length;

            List<Database.planmealcustinvoiceHD> ListHD = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
            List<Database.planmealcustinvoice> Listinvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();//&& p.CustomerID == CID
            if (ListHD.Count() > 0)
            {
                Database.planmealcustinvoiceHD objhd = ListHD.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
                objhd.StartDate = Convert.ToDateTime(txtBeingDate.Text);
                objhd.EndDate = Convert.ToDateTime(txtEndDate.Text);
                objhd.WeekofDay = tags_1.Text.ToString();
                if (Listinvoice.Where(p => p.ActualDelDate != null).Count() > 0)
                {
                    if (Listinvoice.Where(p => p.ActualDelDate == null).Count() > 0)
                    {
                        objhd.CStatus = "In Progress";
                    }
                    else
                    {
                        objhd.CStatus = "Completed";
                    }
                }
                else
                {
                    objhd.CStatus = "Started";
                }
                DB.SaveChanges();
            }

            if (Listinvoice.Count() > 0)
            {
                foreach (Database.planmealcustinvoice items in Listinvoice)
                {
                    Database.planmealcustinvoice objupdatainvoice = Listinvoice.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items.DeliveryID && p.MYPRODID == items.MYPRODID);
                    objupdatainvoice.TotalWeek = Convert.ToInt32(txtTotalWeek.Text);
                    objupdatainvoice.StartDate = Convert.ToDateTime(txtBeingDate.Text);
                    objupdatainvoice.EndDate = Convert.ToDateTime(txtEndDate.Text);
                    DB.SaveChanges();
                }

                for (int i = 2; i <= Week; i++)
                {
                    int ADDweek = i - 1;

                    int dayst = daycount;
                    List<Database.planmealcustinvoice> Listweek = Listinvoice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.DisplayWeek == 1).ToList();//&& p.CustomerID == CID
                    if (Listweek.Count() > 0)
                    {
                        foreach (Database.planmealcustinvoice items in Listweek)
                        {
                            DateTime DTexp = Convert.ToDateTime(items.ExpectedDelDate);
                            int Tday = ADDweek * 7;
                            DateTime Fexp = DTexp.AddDays(Tday);
                            DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
                            EndDate = EndDate.AddDays(1);
                            if (EndDate >= Fexp)
                            {
                                int day = Convert.ToInt32(items.DayNumber);
                                int fday = dayst * ADDweek;
                                int Final = fday + day;
                                List<Database.planmealcustinvoice> ListExist = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.MYPRODID == items.MYPRODID && p.DeliveryMeal == items.DeliveryMeal && p.DayNumber == Final).ToList();
                                if (ListExist.Count() < 1)
                                {
                                    Database.planmealcustinvoice objinvice = new planmealcustinvoice();
                                    objinvice.TenentID = TID;
                                    objinvice.LOCATION_ID = LID;
                                    objinvice.MYTRANSID = MYTRANSID;
                                    objinvice.DeliveryID = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Max(p => p.DeliveryID) + 1) : 1;
                                    objinvice.CustomerID = items.CustomerID;
                                    objinvice.planid = plan;
                                    objinvice.MealType = items.MealType;
                                    objinvice.MYPRODID = items.MYPRODID;
                                    objinvice.ProdName1 = items.ProdName1;

                                    objinvice.DayNumber = Final;
                                    objinvice.OprationDay = items.OprationDay;
                                    objinvice.TransID = items.TransID;
                                    objinvice.ContractID = items.ContractID;
                                    objinvice.WeekofDay = items.WeekofDay;
                                    objinvice.TotalWeek = items.TotalWeek;
                                    objinvice.NameOfDay = items.NameOfDay;
                                    objinvice.NoOfWeek = i;
                                    objinvice.TotalDeliveryDay = items.TotalDeliveryDay;
                                    objinvice.ActualDeliveryDay = items.ActualDeliveryDay;
                                    objinvice.ExpectedDeliveryDay = items.ExpectedDeliveryDay;
                                    objinvice.DeliveryTime = items.DeliveryTime;
                                    objinvice.DeliveryMeal = items.DeliveryMeal;
                                    objinvice.DriverID = items.DriverID;
                                    objinvice.StartDate = items.StartDate;
                                    objinvice.EndDate = items.EndDate;

                                    objinvice.ExpectedDelDate = Fexp;
                                    objinvice.Status = "Pending";
                                    objinvice.NExtDeliveryDate = Fexp.AddDays(1);
                                    objinvice.SubscriptonDayNumber = items.SubscriptonDayNumber;
                                    objinvice.Calories = items.Calories;
                                    objinvice.Carbs = items.Carbs;
                                    objinvice.Protein = items.Protein;
                                    objinvice.Fat = items.Fat;
                                    objinvice.ItemWeight = items.ItemWeight;
                                    objinvice.Qty = items.Qty;
                                    objinvice.Item_cost = items.Item_cost;
                                    objinvice.Item_price = items.Item_price;
                                    objinvice.Total_price = items.Total_price;
                                    objinvice.ShortRemark = items.ShortRemark;
                                    objinvice.ACTIVE = items.ACTIVE;
                                    objinvice.ChangesDate = items.ChangesDate;
                                    objinvice.Switch3 = items.Switch3;
                                    //string Log = "MYTRANSID=" + MYTRANSID + ", CustomerID=" + items.CustomerID + ", planid=" + plan + ", DeliveryMeal=" + items.DeliveryMeal + ", MYPRODID=" + items.MYPRODID;
                                    //int CRUP_ID = GlobleClass.EncryptionHelpers.WriteLog("planmealcustinvoice,INSERT: " + Log, "INSERT", "planmealcustinvoice", UID.ToString(), MenuID);
                                    //objinvice.CRUP_ID = CRUP_ID;

                                    DB.planmealcustinvoices.AddObject(objinvice);
                                    DB.SaveChanges();
                                }
                            }
                        }

                    }
                }
            }
        }

        protected void DrpWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlErrorMsg1.Visible = false;
            lblerrorMsg1.Text = "";

            if (DrpWeek.SelectedValue != "0")
            {
                GetProductList();
                //Editswicth();
                drpDeliveryMeal.Enabled = true;
            }
            else
            {
                pnlErrorMsg1.Visible = true;
                lblerrorMsg1.Text = "Select Week";
            }

        }

        protected void txtproduct_TextChanged(object sender, EventArgs e)
        {
            Editswicth();
        }

        protected void txtWeight_TextChanged(object sender, EventArgs e)
        {
            Editswicth();
        }

        protected void txtFat_TextChanged(object sender, EventArgs e)
        {
            Editswicth();
        }

        protected void txtCarbs_TextChanged(object sender, EventArgs e)
        {
            Editswicth();
        }

        protected void txtProtain_TextChanged(object sender, EventArgs e)
        {
            Editswicth();
        }

        protected void txtCalories_TextChanged(object sender, EventArgs e)
        {
            Editswicth();
        }

        protected void txtsaleprice_TextChanged(object sender, EventArgs e)
        {
            Editswicth();
        }

        protected void txtdate1_TextChanged(object sender, EventArgs e)
        {
            lblmdate1.Visible = false;
            lblmdate1.Text = "";
            DateTime EDT = Convert.ToDateTime(txtdate1.Text);
            ModalPopupExtender1.Show();
            if (EDT.DayOfWeek != DayOfWeek.Saturday)
            {
                lblmdate1.Visible = true;
                lblmdate1.Text = "Please Select Saturday only";
                return;
            }
        }
        protected void setdate1_Click(object sender, EventArgs e)
        {
            lblmdate1.Visible = false;
            lblmdate1.Text = "";
            DateTime EDT = Convert.ToDateTime(txtdate1.Text);
            if (EDT.DayOfWeek == DayOfWeek.Saturday)
            {
                lbldelday1.Text = SetDay1.Text;
                txtdelday1.Text = SetDay1.Text;
                lblDate1.Text = setshortdate(EDT);
                lblFDate1.Text = txtdate1.Text;
                btnDateset1.Visible = false;
            }
            else
            {
                lblmdate1.Visible = true;
                lblmdate1.Text = "Please Select Saturday only";
                ModalPopupExtender1.Show();
                return;
            }
        }
        protected void txtdate2_TextChanged(object sender, EventArgs e)
        {
            lblmdate2.Visible = false;
            lblmdate2.Text = "";
            DateTime EDT = Convert.ToDateTime(txtdate2.Text);
            ModalPopupExtender2.Show();
            if (EDT.DayOfWeek != DayOfWeek.Sunday)
            {
                lblmdate2.Visible = true;
                lblmdate2.Text = "Please Select Sunday only";
                return;
            }
        }
        protected void setdate2_Click(object sender, EventArgs e)
        {
            lblmdate2.Visible = false;
            lblmdate2.Text = "";
            DateTime EDT = Convert.ToDateTime(txtdate2.Text);
            if (EDT.DayOfWeek == DayOfWeek.Sunday)
            {
                lbldelday2.Text = SetDay2.Text;
                txtdelday2.Text = SetDay2.Text;
                lblDate2.Text = setshortdate(EDT);
                lblFDate2.Text = txtdate2.Text;
                btnDateset2.Visible = false;
            }
            else
            {
                lblmdate2.Visible = true;
                lblmdate2.Text = "Please Select Sunday only";
                ModalPopupExtender2.Show();
                return;
            }
        }
        protected void txtdate3_TextChanged(object sender, EventArgs e)
        {
            lblmdate3.Visible = false;
            lblmdate3.Text = "";
            DateTime EDT = Convert.ToDateTime(txtdate3.Text);
            ModalPopupExtender3.Show();
            if (EDT.DayOfWeek != DayOfWeek.Monday)
            {
                lblmdate3.Visible = true;
                lblmdate3.Text = "Please Select Monday only";
                return;
            }
        }
        protected void setdate3_Click(object sender, EventArgs e)
        {
            lblmdate3.Visible = false;
            lblmdate3.Text = "";
            DateTime EDT = Convert.ToDateTime(txtdate3.Text);
            if (EDT.DayOfWeek == DayOfWeek.Monday)
            {
                lbldelday3.Text = SetDay3.Text;
                txtdelday3.Text = SetDay3.Text;
                lblDate3.Text = setshortdate(EDT);
                lblFDate3.Text = txtdate3.Text;
                btnDateset3.Visible = false;
            }
            else
            {
                lblmdate3.Visible = true;
                lblmdate3.Text = "Please Select Monday only";
                ModalPopupExtender3.Show();
                return;
            }
        }
        protected void txtdate4_TextChanged(object sender, EventArgs e)
        {
            lblmdate4.Visible = false;
            lblmdate4.Text = "";
            DateTime EDT = Convert.ToDateTime(txtdate4.Text);
            ModalPopupExtender4.Show();
            if (EDT.DayOfWeek != DayOfWeek.Tuesday)
            {
                lblmdate4.Visible = true;
                lblmdate4.Text = "Please Select Tuesday only";
                return;
            }
        }
        protected void setdate4_Click(object sender, EventArgs e)
        {
            lblmdate4.Visible = false;
            lblmdate4.Text = "";
            DateTime EDT = Convert.ToDateTime(txtdate4.Text);
            if (EDT.DayOfWeek == DayOfWeek.Tuesday)
            {
                lbldelday4.Text = SetDay4.Text;
                txtdelday4.Text = SetDay4.Text;
                lblDate4.Text = setshortdate(EDT);
                lblFDate4.Text = txtdate4.Text;
                btnDateset4.Visible = false;
            }
            else
            {
                lblmdate4.Visible = true;
                lblmdate4.Text = "Please Select Tuesday only";
                ModalPopupExtender4.Show();
                return;
            }
        }
        protected void txtdate5_TextChanged(object sender, EventArgs e)
        {
            lblmdate5.Visible = false;
            lblmdate5.Text = "";
            DateTime EDT = Convert.ToDateTime(txtdate5.Text);
            ModalPopupExtender5.Show();
            if (EDT.DayOfWeek != DayOfWeek.Wednesday)
            {
                lblmdate5.Visible = true;
                lblmdate5.Text = "Please Select Wednesday only";
                return;
            }
        }
        protected void setdate5_Click(object sender, EventArgs e)
        {
            lblmdate5.Visible = false;
            lblmdate5.Text = "";
            DateTime EDT = Convert.ToDateTime(txtdate5.Text);
            if (EDT.DayOfWeek == DayOfWeek.Wednesday)
            {
                lbldelday5.Text = SetDay5.Text;
                txtdelday5.Text = SetDay5.Text;
                lblDate5.Text = setshortdate(EDT);
                lblFDate5.Text = txtdate5.Text;
                btnDateset5.Visible = false;
            }
            else
            {
                lblmdate5.Visible = true;
                lblmdate5.Text = "Please Select Wednesday only";
                ModalPopupExtender5.Show();
                return;
            }
        }
        protected void txtdate6_TextChanged(object sender, EventArgs e)
        {
            lblmdate6.Visible = false;
            lblmdate6.Text = "";
            DateTime EDT = Convert.ToDateTime(txtdate6.Text);
            ModalPopupExtender6.Show();
            if (EDT.DayOfWeek != DayOfWeek.Thursday)
            {
                lblmdate6.Visible = true;
                lblmdate6.Text = "Please Select Thursday only";
                return;
            }
        }
        protected void setdate6_Click(object sender, EventArgs e)
        {
            lblmdate6.Visible = false;
            lblmdate6.Text = "";
            DateTime EDT = Convert.ToDateTime(txtdate6.Text);
            if (EDT.DayOfWeek == DayOfWeek.Thursday)
            {
                lbldelday6.Text = SetDay6.Text;
                txtdelday6.Text = SetDay6.Text;
                lblDate6.Text = setshortdate(EDT);
                lblFDate6.Text = txtdate6.Text;
                btnDateset6.Visible = false;
            }
            else
            {
                lblmdate6.Visible = true;
                lblmdate6.Text = "Please Select Thursday only";
                ModalPopupExtender6.Show();
                return;
            }
        }

        protected void SetDay1_TextChanged(object sender, EventArgs e)
        {
            //ModalPopupExtender1.Show();
        }

        protected void SetDay2_TextChanged(object sender, EventArgs e)
        {
            //ModalPopupExtender2.Show();
        }

        protected void SetDay3_TextChanged(object sender, EventArgs e)
        {
            //ModalPopupExtender3.Show();
        }

        protected void SetDay4_TextChanged(object sender, EventArgs e)
        {
            //ModalPopupExtender4.Show();
        }

        protected void SetDay5_TextChanged(object sender, EventArgs e)
        {
            //ModalPopupExtender5.Show();
        }

        protected void SetDay6_TextChanged(object sender, EventArgs e)
        {
            //ModalPopupExtender6.Show();
        }

        protected void txtExpDate_TextChanged(object sender, EventArgs e)
        {
            lblMSGDate.Visible = false;

            if (txtExpDate.Text != "")
            {
                DateTime CDT = Convert.ToDateTime(txtExpDate.Text);

                if (CDT.DayOfWeek == DayOfWeek.Friday)
                {
                    DateTime Date = CDT.AddDays(1);
                    txtBeingDate.Text = Date.ToString("MM/dd/yyyy");
                }
                else
                {
                    txtBeingDate.Text = CDT.ToString("MM/dd/yyyy");
                }

                DateTime DT = Convert.ToDateTime(txtBeingDate.Text);

                if (txtBeingDate.Text != "")
                {
                    DateTime BeingDate = Convert.ToDateTime(txtBeingDate.Text);
                    DateTime EndDate = BeingDate.AddDays(27);
                    txtEndDate.Text = EndDate.ToString("MM/dd/yyyy");
                    //DateTime EDT = Convert.ToDateTime(txtEndDate.Text);
                    //int week = Convert.ToInt32((EDT - BeingDate).TotalDays) / 7;
                    //int HowManyDay = Convert.ToInt32(drpHowManyDay.SelectedValue);
                    //int TotalDday = week * HowManyDay;
                    //txtTotalDeliveryDay.Text = TotalDday.ToString();
                    //txtDelivered.Text = TotalDday.ToString();
                    //txtTotalWeek.Text = week.ToString();
                    //NoWeek();

                    calculate();
                }

                if (DT.DayOfWeek == DayOfWeek.Friday)
                {
                    lblMSGDate.Visible = true;
                    lblMSGDate.Text = "Select Other than Friday";
                    txtBeingDate.Focus();
                    return;
                }

                if (DT.Date >= CDT.Date)
                { }
                else
                {
                    lblMSGDate.Visible = true;
                    lblMSGDate.Text = "Begin Date must be greater or Equal to the Contract Date";
                    return;
                }
                DateTime dt1 = DT;
                setdate(dt1, 1);
            }
        }

        protected void btnsetday_Click(object sender, EventArgs e)
        {
            if (btnsetday.Text == "Set")
            {
                txtdelday1.Visible = true;
                txtdelday2.Visible = true;
                txtdelday3.Visible = true;
                txtdelday4.Visible = true;
                txtdelday5.Visible = true;
                txtdelday6.Visible = true;

                lbldelday1.Visible = false;
                lbldelday2.Visible = false;
                lbldelday3.Visible = false;
                lbldelday4.Visible = false;
                lbldelday5.Visible = false;
                lbldelday6.Visible = false;

                btnsetday.Text = "Save";
                btnsetday.ValidationGroup = "delday";
            }
            else if (btnsetday.Text == "Save")
            {
                lbldelday1.Text = txtdelday1.Text;
                lbldelday2.Text = txtdelday2.Text;
                lbldelday3.Text = txtdelday3.Text;
                lbldelday4.Text = txtdelday4.Text;
                lbldelday5.Text = txtdelday5.Text;
                lbldelday6.Text = txtdelday6.Text;

                txtdelday1.Visible = false;
                txtdelday2.Visible = false;
                txtdelday3.Visible = false;
                txtdelday4.Visible = false;
                txtdelday5.Visible = false;
                txtdelday6.Visible = false;

                lbldelday1.Visible = true;
                lbldelday2.Visible = true;
                lbldelday3.Visible = true;
                lbldelday4.Visible = true;
                lbldelday5.Visible = true;
                lbldelday6.Visible = true;

                btnsetday.Text = "Set";
                btnsetday.ValidationGroup = "ss";
            }

        }

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            Editswicth();
            if (e.CommandName == "AddQty")
            {
                for (int i = 0; i < ListView2.Items.Count; i++)
                {
                    TextBox txtqty11 = (TextBox)ListView2.Items[i].FindControl("txtqty1");
                    TextBox txtqty21 = (TextBox)ListView2.Items[i].FindControl("txtqty2");
                    TextBox txtqty31 = (TextBox)ListView2.Items[i].FindControl("txtqty3");
                    TextBox txtqty41 = (TextBox)ListView2.Items[i].FindControl("txtqty4");
                    TextBox txtqty51 = (TextBox)ListView2.Items[i].FindControl("txtqty5");
                    TextBox txtqty61 = (TextBox)ListView2.Items[i].FindControl("txtqty6");

                    txtqty11.Visible = false;
                    txtqty21.Visible = false;
                    txtqty31.Visible = false;
                    txtqty41.Visible = false;
                    txtqty51.Visible = false;
                    txtqty61.Visible = false;

                }

                CheckBox RDODay1 = (CheckBox)e.Item.FindControl("RDODay1");
                CheckBox RDODay2 = (CheckBox)e.Item.FindControl("RDODay2");
                CheckBox RDODay3 = (CheckBox)e.Item.FindControl("RDODay3");
                CheckBox RDODay4 = (CheckBox)e.Item.FindControl("RDODay4");
                CheckBox RDODay5 = (CheckBox)e.Item.FindControl("RDODay5");
                CheckBox RDODay6 = (CheckBox)e.Item.FindControl("RDODay6");

                TextBox txtqty1 = (TextBox)e.Item.FindControl("txtqty1");
                TextBox txtqty2 = (TextBox)e.Item.FindControl("txtqty2");
                TextBox txtqty3 = (TextBox)e.Item.FindControl("txtqty3");
                TextBox txtqty4 = (TextBox)e.Item.FindControl("txtqty4");
                TextBox txtqty5 = (TextBox)e.Item.FindControl("txtqty5");
                TextBox txtqty6 = (TextBox)e.Item.FindControl("txtqty6");

                string[] days = tags_1.Text.Split(',');
                foreach (string Val1 in days)
                {
                    if (Val1 == "Sat")
                    {
                        if (RDODay1.Checked == true)
                            txtqty1.Visible = true;
                        else
                            txtqty1.Visible = false;
                    }

                    if (Val1 == "Sun")
                    {
                        if (RDODay2.Checked == true)
                            txtqty2.Visible = true;
                        else
                            txtqty2.Visible = false;
                    }

                    if (Val1 == "Mon")
                    {
                        if (RDODay3.Checked == true)
                            txtqty3.Visible = true;
                        else
                            txtqty3.Visible = false;

                    }

                    if (Val1 == "Tue")
                    {
                        if (RDODay4.Checked == true)
                            txtqty4.Visible = true;
                        else
                            txtqty4.Visible = false;
                    }

                    if (Val1 == "Wed")
                    {
                        if (RDODay5.Checked == true)
                            txtqty5.Visible = true;
                        else
                            txtqty5.Visible = false;
                    }

                    if (Val1 == "Thu")
                    {
                        if (RDODay6.Checked == true)
                            txtqty6.Visible = true;
                        else
                            txtqty6.Visible = false;
                    }
                }
            }
        }


        public void DatesetwithDay()
        {
            pnlErrorMsg.Visible = false;
            lblerrorMsg.Text = "";

            if (txtDelTotalDay.Text != "")
            {
                if (txtBeingDate.Text != "")
                {
                    int EnterDay = Convert.ToInt32(txtDelTotalDay.Text);
                    int LoopCount = 1;
                    DateTime Beingdate = Convert.ToDateTime(txtBeingDate.Text);
                    DateTime Enddate = Beingdate;
                    DateTime Enddate1 = Beingdate;

                    while (LoopCount <= EnterDay)
                    {
                        if (Enddate.DayOfWeek == DayOfWeek.Friday)
                        {
                            Enddate = Enddate.AddDays(1);
                        }

                        int Cloop = 0;
                        string[] days = tags_1.Text.Split(',');
                        foreach (string Val1 in days)
                        {
                            if (Cloop == 0)
                            {
                                if (Val1 == "Sat" && Enddate.DayOfWeek == DayOfWeek.Saturday)
                                {
                                    Cloop++;
                                }

                                if (Val1 == "Sun" && Enddate.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    Cloop++;
                                }

                                if (Val1 == "Mon" && Enddate.DayOfWeek == DayOfWeek.Monday)
                                {
                                    Cloop++;
                                }

                                if (Val1 == "Tue" && Enddate.DayOfWeek == DayOfWeek.Tuesday)
                                {
                                    Cloop++;
                                }

                                if (Val1 == "Wed" && Enddate.DayOfWeek == DayOfWeek.Wednesday)
                                {
                                    Cloop++;
                                }

                                if (Val1 == "Thu" && Enddate.DayOfWeek == DayOfWeek.Thursday)
                                {
                                    Cloop++;
                                }
                            }
                        }

                        if (Cloop > 0)
                        {
                            Enddate = Enddate.AddDays(1);
                            LoopCount++;
                            Enddate1 = Enddate;
                        }
                        else
                        {
                            if (Enddate.DayOfWeek != DayOfWeek.Friday)
                            {
                                Enddate = Enddate.AddDays(1);
                            }
                        }

                    }

                    Enddate1 = Enddate1.AddDays(-1);
                    //Enddate = Enddate.AddDays(FridayCount);
                    txtEndDate.Text = Enddate1.ToString("MM/dd/yyyy");
                    calculate();
                }
                else
                {
                    pnlErrorMsg.Visible = true;
                    lblerrorMsg.Text = "Please Select Begin Date";
                    return;
                }
            }
            else
            {
                pnlErrorMsg.Visible = true;
                lblerrorMsg.Text = "Please Enter Total Delevery Day";
                return;
            }
        }

        protected void btnDateSet_Click(object sender, EventArgs e)
        {
            DatesetwithDay();
        }

        protected void btnRecalculate_Click(object sender, EventArgs e)
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(0, 10, 0)))
            //{

            bool Flag = checkradio();
            if (Flag == false)
            {
                return;
            }

            recalculate();
            //scope.Complete();
            //}

            int Custo = Convert.ToInt32(drpCustomer.SelectedValue);
            int Plan = Convert.ToInt32(drpPlan.SelectedValue);
            int Meal = Convert.ToInt32(drpDeliveryMeal.SelectedValue);

            if (ViewState["MYTRANSID"] != null)
            {
                int MYTRANSID = Convert.ToInt32(ViewState["MYTRANSID"]);
                Editinvoice(Custo, MYTRANSID, Plan, Meal);
            }

        }

        public void recalculate()
        {
            if (ViewState["MYTRANSID"] != null)
            {
                DateTime Today = DateTime.Now;
                int MYTRANSID = Convert.ToInt32(ViewState["MYTRANSID"]);
                int plan = Convert.ToInt32(drpPlan.SelectedValue);

                DateTime StartDate = Convert.ToDateTime(txtBeingDate.Text).Date;
                DateTime EndDate = Convert.ToDateTime(txtEndDate.Text).Date;
                EndDate = EndDate.AddDays(1);
                List<Database.planmealcustinvoice> Listplan = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.StartDate >= StartDate && p.EndDate <= EndDate).ToList();

                if (Listplan.Count() < 1)
                {
                    copyplan(MYTRANSID);
                }


                if (txtTotalWeek.Text != "")
                {
                    DeleteDTFRomTo(MYTRANSID);

                    int Week = Convert.ToInt32(txtTotalWeek.Text);
                    if (Week > 0)
                    {
                        Copyfullplan(MYTRANSID, Week, plan);
                    }

                    DayAgoDelevery(MYTRANSID);

                    UpdatedaysHD(MYTRANSID);

                    Datewiceinsert(MYTRANSID);

                    Prementdeletedt(MYTRANSID);
                }

            }
        }

        public void DayAgoDelevery(int MYTRANSID)
        {
            DateTime Today = DateTime.Now;
            Today = Today.AddDays(-1);
            List<Database.planmealcustinvoice> ListInvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate == null && p.ExpectedDelDate < Today).ToList();
            if (ListInvoice.Count() > 0)
            {
                foreach (Database.planmealcustinvoice items in ListInvoice)
                {
                    Database.planmealcustinvoice Objinvoice = ListInvoice.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items.DeliveryID && p.MYPRODID == items.MYPRODID);
                    Objinvoice.ActualDelDate = items.ExpectedDelDate;
                    Objinvoice.ProductionDate = items.ExpectedDelDate;
                    Objinvoice.chiefID = UID;
                    Objinvoice.Status = "Delivered";
                    DB.SaveChanges();
                }
            }
        }

        public void UpdatedaysHD(int MYTRANSID)
        {
            List<Database.planmealcustinvoice> ListDTinvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();

            List<Database.planmealcustinvoice> ListtotaldelDay = ListDTinvoice.GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
            int totalDelDay = ListtotaldelDay.Count();

            List<Database.planmealcustinvoice> ListdelDay = ListDTinvoice.Where(p => p.ActualDelDate == null).GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
            int DeliveredDays = ListdelDay.Count();

            Database.planmealcustinvoiceHD ObjHD = DB.planmealcustinvoiceHDs.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
            ObjHD.TotalSubDays = totalDelDay;
            ObjHD.DeliveredDays = DeliveredDays;
            ObjHD.WeekofDay = tags_1.Text.ToString();
            ObjHD.StartDate = Convert.ToDateTime(txtBeingDate.Text);
            ObjHD.EndDate = Convert.ToDateTime(txtEndDate.Text);

            if (ListDTinvoice.Where(p => p.ActualDelDate != null).Count() > 0)
            {
                if (ListDTinvoice.Where(p => p.ActualDelDate == null).Count() > 0)
                {
                    ObjHD.CStatus = "In Progress";
                }
                else
                {
                    ObjHD.CStatus = "Completed";
                }
            }
            else
            {
                ObjHD.CStatus = "Started";
            }

            if (ListDTinvoice.Where(p => p.ActualDelDate != null).Count() > 0)
            {
                int nextdayno = ListDTinvoice.Where(p => p.ActualDelDate != null).Max(p => p.DayNumber) + 1;
                DateTime NextDate = ListDTinvoice.Where(p => p.DayNumber == nextdayno).Count() > 0 ? Convert.ToDateTime(ListDTinvoice.Where(p => p.DayNumber == nextdayno).FirstOrDefault().ExpectedDelDate) : Convert.ToDateTime(ListDTinvoice.Where(p => p.ActualDelDate != null).FirstOrDefault().ExpectedDelDate);
                ObjHD.NExtDeliveryNum = nextdayno;
                ObjHD.NExtDeliveryDate = NextDate;
            }

            DB.SaveChanges();
        }

        public void DeleteDTFRomTo(int MYTRANSID)
        {
            DateTime StartDate = Convert.ToDateTime(txtBeingDate.Text).Date;
            DateTime EndDate = Convert.ToDateTime(txtEndDate.Text).Date;

            List<Database.planmealcustinvoice> Listdt = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate == null && (p.ExpectedDelDate < StartDate || p.ExpectedDelDate >= EndDate)).ToList();

            foreach (Database.planmealcustinvoice items in Listdt)
            {
                Database.planmealcustinvoice objdt = Listdt.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items.DeliveryID && p.MYPRODID == items.MYPRODID);
                objdt.ACTIVE = false;
                //DB.planmealcustinvoices.DeleteObject(objdt);
                DB.SaveChanges();
            }
        }

        public void Datewiceinsert(int MYTRANSID)
        {
            DateTime Today = DateTime.Now;
            //List<Database.planmealcustinvoice> ListInvoiceAdd = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate == null && p.ExpectedDelDate >= Today).ToList();

            DateTime StartDate = Convert.ToDateTime(txtBeingDate.Text).Date;
            DateTime EndDate = Convert.ToDateTime(txtEndDate.Text).Date;

            DateTime ContractEnddate = Convert.ToDateTime(txtEndDate.Text);
            int Totaldafday1 = Convert.ToInt32((ContractEnddate - Today).TotalDays);

            int EnterDay = Totaldafday1;// Convert.ToInt32(txtDelTotalDay.Text);            
            DateTime Beingdate = Convert.ToDateTime(txtBeingDate.Text);
            DateTime Enddate = Today;
            DateTime Enddate1 = Today;
            //DateTime Enddate = Beingdate;
            //DateTime Enddate1 = Beingdate;
            int DayCount1 = 0;
            string[] dayscount = tags_1.Text.Split(',');

            int HowManyDay = dayscount.Length;

            List<Database.planmealcustinvoice> ListInvoiceadd = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate == null).ToList();

            ContractEnddate = ContractEnddate.AddDays(1);
            while (Enddate <= ContractEnddate)
            {
                if (Enddate.DayOfWeek == DayOfWeek.Friday)
                {
                    Enddate = Enddate.AddDays(1);
                }
                string TEmpDate = Enddate.ToShortDateString();
                DateTime Enddatecheck = Convert.ToDateTime(TEmpDate);

                var Day = Enddatecheck.Day;
                var Month = Enddatecheck.Month;
                var Year = Enddatecheck.Year;

                List<Database.planmealcustinvoice> ListInvoiceAdd1 = ListInvoiceadd.Where(p => (p.ExpectedDelDate.Value.Day == Day && p.ExpectedDelDate.Value.Month == Month && p.ExpectedDelDate.Value.Year == Year)).ToList();
                int ListInvoiceAdd1Count = ListInvoiceAdd1.Count();
                foreach (Database.planmealcustinvoice Itemsdt in ListInvoiceAdd1)
                {
                    int Cloop = 0;
                    int OprationDay = 0;
                    string NameOfDay = null;
                    string[] days = tags_1.Text.Split(',');
                    foreach (string Val1 in days)
                    {
                        if (Cloop == 0)
                        {
                            if (Val1 == "Sat" && Enddate.DayOfWeek == DayOfWeek.Saturday)
                            {
                                Cloop++;
                                OprationDay = 1;
                                NameOfDay = Val1;
                            }

                            if (Val1 == "Sun" && Enddate.DayOfWeek == DayOfWeek.Sunday)
                            {
                                Cloop++;
                                OprationDay = 2;
                                NameOfDay = Val1;
                            }

                            if (Val1 == "Mon" && Enddate.DayOfWeek == DayOfWeek.Monday)
                            {
                                Cloop++;
                                OprationDay = 3;
                                NameOfDay = Val1;
                            }

                            if (Val1 == "Tue" && Enddate.DayOfWeek == DayOfWeek.Tuesday)
                            {
                                Cloop++;
                                OprationDay = 4;
                                NameOfDay = Val1;
                            }

                            if (Val1 == "Wed" && Enddate.DayOfWeek == DayOfWeek.Wednesday)
                            {
                                Cloop++;
                                OprationDay = 5;
                                NameOfDay = Val1;
                            }

                            if (Val1 == "Thu" && Enddate.DayOfWeek == DayOfWeek.Thursday)
                            {
                                Cloop++;
                                OprationDay = 6;
                                NameOfDay = Val1;
                            }
                        }
                    }

                    if (Cloop > 0)
                    {
                        Enddate1 = Enddate;

                        int Totaldafday = Convert.ToInt32((Enddate1.Date - Beingdate.Date).TotalDays);

                        decimal Week1 = Totaldafday / 6;
                        decimal Remainder = Totaldafday % 6;

                        int week = 0;
                        if (Remainder > 0)
                        {
                            Week1 = Week1 + 1;
                            week = Convert.ToInt32(Math.Ceiling(Week1));
                        }
                        else
                        {
                            week = Convert.ToInt32(Math.Ceiling(Week1));
                        }

                        int No_OfWeek = week != 0 ? week : 1;

                        int planid = Itemsdt.planid;
                        int CustomerID = Itemsdt.CustomerID;
                        long MYPRODID = Itemsdt.MYPRODID;
                        string ProdName1 = Itemsdt.ProdName1;
                        string WeekofDay = tags_1.Text;
                        int TotalWeek = Convert.ToInt32(txtTotalWeek.Text);
                        int TotalDeliveryDay = Convert.ToInt32(txtTotalDeliveryDay.Text);
                        int ActualDeliveryDay = Convert.ToInt32(txtDelivered.Text);
                        int ExpectedDeliveryDay = Convert.ToInt32(txtRemainingDay.Text);
                        int DeliveryTime = Convert.ToInt32(Itemsdt.DeliveryTime);
                        int DeliveryMeal = Convert.ToInt32(Itemsdt.DeliveryMeal);
                        int DriverID = Convert.ToInt32(Itemsdt.DriverID);

                        DateTime ExpectedDelDate = Enddate1;
                        decimal Calories = Convert.ToDecimal(Itemsdt.Calories);
                        decimal Carbs = Convert.ToDecimal(Itemsdt.Carbs);
                        decimal Protein = Convert.ToDecimal(Itemsdt.Protein);
                        decimal Fat = Convert.ToDecimal(Itemsdt.Fat);
                        decimal ItemWeight = Convert.ToDecimal(Itemsdt.ItemWeight);
                        int Qty = Convert.ToInt32(Itemsdt.Qty);
                        decimal Item_cost = Convert.ToDecimal(Itemsdt.Item_cost);
                        decimal Item_price = Convert.ToDecimal(Itemsdt.Item_price);
                        decimal Total_price = Convert.ToDecimal(Itemsdt.Total_price);
                        string ShortRemark = Itemsdt.ShortRemark;


                        //DateTime Checkdate = Beingdate;
                        //int FridayCount = 0;                        
                        //for (int i = 1; i <= Totaldafday; i++)
                        //{
                        //    if (Checkdate.DayOfWeek == DayOfWeek.Friday)
                        //    {
                        //        FridayCount++;
                        //    }
                        //    Checkdate = Checkdate.AddDays(1);
                        //}

                        int FridayCount = CountDays(DayOfWeek.Friday, Beingdate, Enddate);

                        int temDaynumber = Totaldafday - FridayCount;
                        int DayNumber = temDaynumber != 0 ? temDaynumber : 1;// Convert.ToInt32(Itemsdt.DayNumber);

                        AddNewDT(MYTRANSID, planid, CustomerID, MYPRODID, ProdName1, DayNumber, OprationDay, WeekofDay, TotalWeek, NameOfDay, No_OfWeek, TotalDeliveryDay, ActualDeliveryDay, ExpectedDeliveryDay, DeliveryTime, DeliveryMeal, DriverID, StartDate, EndDate, ExpectedDelDate, Calories, Carbs, Protein, Fat, ItemWeight, Qty, Item_cost, Item_price, Total_price, ShortRemark);

                    }
                    else
                    {
                        if (Enddate.DayOfWeek != DayOfWeek.Friday)
                        {
                            Enddate = Enddate.AddDays(1);
                        }
                    }
                }

                Enddate = Enddate.AddDays(1);


            }
            updatedaynumber(MYTRANSID);
            updatenoofweek(MYTRANSID);
            UpdateDTrecord(MYTRANSID);
        }

        public void updatenoofweek(int MYTRANSID)
        {
            DateTime StartDate = Convert.ToDateTime(txtBeingDate.Text).Date;
            string start_date = StartDate.ToShortDateString();
            int Noofweek = 1;// DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate != null).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate != null).Max(p => p.NoOfWeek)) : 0;
            string dateflag = "";
            List<Database.planmealcustinvoice> ListInvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ACTIVE == true).OrderBy(p => p.DayNumber).ToList();//&& p.ActualDelDate == null

            foreach (Database.planmealcustinvoice items in ListInvoice)
            {

                DateTime Checkdate = Convert.ToDateTime(items.ExpectedDelDate);
                string Check_date = Checkdate.ToShortDateString();
                if (Checkdate.DayOfWeek == DayOfWeek.Saturday)
                {
                    if (Check_date != start_date && dateflag != Check_date)
                    {
                        dateflag = Check_date;
                        Noofweek = Noofweek + 1;
                    }
                }
                Database.planmealcustinvoice objdt = items;
                objdt.NoOfWeek = Noofweek;
                DB.SaveChanges();
            }
        }

        public void updatedaynumber(int MYTRANSID)
        {
            List<Database.planmealcustinvoice> ListDTMain = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();

            DateTime StartDate = Convert.ToDateTime(txtBeingDate.Text).Date;
            string start_date = StartDate.ToShortDateString();
            int dayNumber = ListDTMain.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate != null).Count() > 0 ? Convert.ToInt32(DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate != null).Max(p => p.DayNumber)) : 1;
            string dateflag = "";
            List<Database.planmealcustinvoice> ListInvoice = ListDTMain.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ACTIVE == true && p.ActualDelDate == null).ToList();

            foreach (Database.planmealcustinvoice items in ListInvoice)
            {
                DateTime Checkdate = Convert.ToDateTime(items.ExpectedDelDate);
                string Check_date = Checkdate.ToShortDateString();

                if (Check_date != start_date)
                {
                    if (Check_date != dateflag)
                    {
                        dateflag = Check_date;
                        dayNumber = dayNumber + 1;
                    }

                }

                Database.planmealcustinvoice objdt = items;
                objdt.DayNumber = dayNumber;
                DB.SaveChanges();
            }
        }

        public void updatedisplayweek(int MYTRANSID)
        {
            DateTime StartDate = Convert.ToDateTime(txtBeingDate.Text).Date;
            StartDate = StartDate.AddDays(6);

            List<Database.planmealcustinvoice> ListInvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ExpectedDelDate <= StartDate && p.ActualDelDate == null && p.ACTIVE == true).ToList();

            foreach (Database.planmealcustinvoice items in ListInvoice)
            {
                Database.planmealcustinvoice objdt = items;
                objdt.DisplayWeek = 1;
                DB.SaveChanges();
            }
        }

        public void UpdateDTOPrationDay(int MYTRANSID)
        {
            List<Database.planmealcustinvoice> ListInvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ACTIVE == true).OrderBy(p => p.DayNumber).ToList();

            foreach (Database.planmealcustinvoice items in ListInvoice)
            {
                DateTime expdate = Convert.ToDateTime(items.ExpectedDelDate);
                Database.planmealcustinvoice objdt = items;
                if (expdate.DayOfWeek == DayOfWeek.Saturday)
                {
                    objdt.OprationDay = 1;
                    objdt.NameOfDay = "Sat";
                }
                if (expdate.DayOfWeek == DayOfWeek.Sunday)
                {
                    objdt.OprationDay = 2;
                    objdt.NameOfDay = "Sun";
                }
                if (expdate.DayOfWeek == DayOfWeek.Monday)
                {
                    objdt.OprationDay = 3;
                    objdt.NameOfDay = "Mon";
                }
                if (expdate.DayOfWeek == DayOfWeek.Tuesday)
                {
                    objdt.OprationDay = 4;
                    objdt.NameOfDay = "Tue";
                }
                if (expdate.DayOfWeek == DayOfWeek.Wednesday)
                {
                    objdt.OprationDay = 5;
                    objdt.NameOfDay = "Wed";
                }
                if (expdate.DayOfWeek == DayOfWeek.Thursday)
                {
                    objdt.OprationDay = 6;
                    objdt.NameOfDay = "Thu";
                }
                DB.SaveChanges();
            }
        }

        public void UpdateDTrecord(int MYTRANSID)
        {
            updatedisplayweek(MYTRANSID);
            UpdateDTOPrationDay(MYTRANSID);
            List<Database.planmealcustinvoice> ListInvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate == null && p.ACTIVE == true).ToList();

            int Totalweek = Convert.ToInt32(ListInvoice.Max(p => p.NoOfWeek));

            foreach (Database.planmealcustinvoice items in ListInvoice)
            {
                Database.planmealcustinvoice objdt = items;
                if (Totalweek > 1)
                {
                    objdt.TotalWeek = Totalweek;
                }
                DB.SaveChanges();
            }

        }

        public void Prementdeletedt(int MYTRANSID)
        {
            if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ACTIVE == false).Count() > 0)
            {
                string Str = "delete from planmealcustinvoice where TenentID=" + TID + " and MYTRANSID=" + MYTRANSID + " and ACTIVE='false'";
                command2 = new SqlCommand(Str, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();
            }
        }

        public void AddNewDT(int MYTRANSID, int planid, int CustomerID, long MYPRODID, string ProdName1, int DayNumber, int OprationDay, string WeekofDay, int TotalWeek, string NameOfDay, int No_OfWeek, int TotalDeliveryDay, int ActualDeliveryDay, int ExpectedDeliveryDay, int DeliveryTime, int DeliveryMeal, int DriverID, DateTime StartDate, DateTime EndDate, DateTime ExpectedDelDate, decimal Calories, decimal Carbs, decimal Protein, decimal Fat, decimal ItemWeight, int Qty, decimal Item_cost, decimal Item_price, decimal Total_price, string ShortRemark)
        {
            var Day = ExpectedDelDate.Day;
            var Month = ExpectedDelDate.Month;
            var Year = ExpectedDelDate.Year;
            List<Database.planmealcustinvoice> ListRemoved = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryMeal == DeliveryMeal && p.MYPRODID == MYPRODID && (p.ExpectedDelDate.Value.Day == Day && p.ExpectedDelDate.Value.Month == Month && p.ExpectedDelDate.Value.Year == Year)).ToList();

            foreach (Database.planmealcustinvoice items in ListRemoved)
            {
                Database.planmealcustinvoice obj = items;
                obj.ACTIVE = false;
                DB.SaveChanges();
            }
            List<Database.planmealcustinvoice> ListInvice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
            if (ListInvice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryMeal == DeliveryMeal && p.MYPRODID == MYPRODID && (p.ExpectedDelDate.Value.Day == Day && p.ExpectedDelDate.Value.Month == Month && p.ExpectedDelDate.Value.Year == Year) && p.ACTIVE == true).Count() < 1)
            {
                Database.planmealcustinvoice objinvice = new planmealcustinvoice();
                objinvice.TenentID = TID;
                objinvice.MYTRANSID = MYTRANSID;
                objinvice.DeliveryID = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Count() > 0 ? Convert.ToInt32(ListInvice.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Max(p => p.DeliveryID) + 1) : 1;
                objinvice.LOCATION_ID = LID;
                objinvice.TransID = MYTRANSID;// Convert.ToInt32(txtInvoiceNumber.Text);//Convert.ToInt32(txtInvoiceNumber1s.Text);
                objinvice.ContractID = MYTRANSID.ToString();
                objinvice.planid = planid;//Convert.ToInt32(drpPlan.SelectedValue);
                objinvice.CustomerID = CustomerID;// Convert.ToInt32(drpCustomer.SelectedValue);
                objinvice.MYPRODID = MYPRODID;
                objinvice.ProdName1 = ProdName1;
                //int day = Convert.ToInt32(DayNumber);
                //int fday = dayst * ADDweek;
                //int Final = fday + day;
                objinvice.DayNumber = DayNumber;
                objinvice.OprationDay = OprationDay;
                objinvice.WeekofDay = WeekofDay;//tags_1.Text;
                objinvice.TotalWeek = TotalWeek;// Convert.ToInt32(txtTotalWeek.Text);
                objinvice.NameOfDay = NameOfDay;
                objinvice.NoOfWeek = No_OfWeek;
                objinvice.TotalDeliveryDay = TotalDeliveryDay;// Convert.ToInt32(txtTotalDeliveryDay.Text);
                objinvice.ActualDeliveryDay = ActualDeliveryDay;// Convert.ToInt32(txtDelivered.Text);
                objinvice.ExpectedDeliveryDay = ExpectedDeliveryDay;// Convert.ToInt32(txtRemainingDay.Text);
                objinvice.DeliveryTime = DeliveryTime;
                objinvice.DeliveryMeal = DeliveryMeal;
                objinvice.MealType = DeliveryMeal;
                objinvice.DriverID = DriverID;
                objinvice.StartDate = StartDate;// Convert.ToDateTime(txtBeingDate.Text);
                objinvice.EndDate = EndDate;// Convert.ToDateTime(txtEndDate.Text);

                objinvice.ExpectedDelDate = ExpectedDelDate; // Fexp;
                objinvice.Status = "Pending";
                objinvice.NExtDeliveryDate = ExpectedDelDate.AddDays(1);
                objinvice.Calories = Calories;
                objinvice.Carbs = Carbs;
                objinvice.Protein = Protein;
                objinvice.Fat = Fat;
                objinvice.ItemWeight = ItemWeight;
                objinvice.Qty = Qty;
                objinvice.Item_cost = Item_cost;
                objinvice.Item_price = Item_price;
                objinvice.Total_price = Total_price;
                objinvice.ShortRemark = ShortRemark;
                objinvice.ACTIVE = true;
                List<Database.REFTABLE> ListRef = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == DeliveryMeal).ToList();
                objinvice.Switch3 = ListRef.Where(p => p.SWITCH1 != null && p.SWITCH1 != "").Count() > 0 ? ListRef.Single(p => p.TenentID == TID && p.REFID == DeliveryMeal && p.SWITCH1 != null && p.SWITCH1 != "").SWITCH1.ToString() : "";
                //string Log = "MYTRANSID=" + MYTRANSID + ", CustomerID=" + CID + ", planid=" + plan + ", DeliveryMeal=" + items.DeliveryMeal + ", MYPRODID=" + items.MYPRODID;
                //int CRUP_ID = GlobleClass.EncryptionHelpers.WriteLog("planmealcustinvoice,INSERT: " + Log, "INSERT", "planmealcustinvoice", UID.ToString(), MenuID);
                //objinvice.CRUP_ID = CRUP_ID;

                DB.planmealcustinvoices.AddObject(objinvice);
                DB.SaveChanges();
            }

        }
        protected void lnkbtnhold_Click(object sender, EventArgs e)
        {
            lblHoadDate.Visible = false;
            lblHoadDate.Text = "";
            if (txtHoldDate.Text != "")
            {
                DateTime HoldDate = Convert.ToDateTime(txtHoldDate.Text);
                if (ViewState["MYTRANSID"] != null)
                {
                    int MYTRANSID = Convert.ToInt32(ViewState["MYTRANSID"]);
                    if (DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Count() > 0)
                    {
                        Database.planmealcustinvoiceHD Objhd = DB.planmealcustinvoiceHDs.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
                        Objhd.SubscriptionOnHold = true;
                        Objhd.HoldDate = HoldDate;
                        Objhd.HoldREmark = txtHoldRemark.Text;
                        Objhd.CStatus = "Hold";
                        Objhd.Holdbyuser = UID;
                        DB.SaveChanges();
                        Editswicth();
                    }
                }
            }
            else
            {
                lblHoadDate.Visible = false;
                lblHoadDate.Text = "Select Hold Date";
                return;
            }
        }

        protected void txtHoldDate_TextChanged(object sender, EventArgs e)
        {
            lblHoadDate.Visible = false;
            lblHoadDate.Text = "";
            if (txtHoldDate.Text != "")
            {
                DateTime HoldDate = Convert.ToDateTime(txtHoldDate.Text);
                ModalPopupExtender7.Show();
                DateTime Beingdate = Convert.ToDateTime(txtBeingDate.Text);
                DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
                DateTime Today = DateTime.Now;
                if (HoldDate >= Beingdate && HoldDate <= EndDate)
                {
                    if (HoldDate <= Today)
                    {
                        lblHoadDate.Visible = true;
                        lblHoadDate.Text = "Select Hold Date Grater Than Today";
                    }
                }
                else
                {
                    lblHoadDate.Visible = true;
                    lblHoadDate.Text = "Select Hold Between Start Date And End Date Of Plan ";
                }
            }
            else
            {
                lblHoadDate.Visible = false;
                lblHoadDate.Text = "Select Hold Date";
                return;
            }

        }

        protected void lnkbtnChangeDriver_Click(object sender, EventArgs e)
        {
            lblChangeDridate.Visible = false;
            lblChangeDridate.Text = "";
            if (txtDriverChangeDate.Text != "")
            {
                DateTime DriverChangeDate = Convert.ToDateTime(txtDriverChangeDate.Text);
                if (ViewState["MYTRANSID"] != null)
                {
                    int MYTRANSID = Convert.ToInt32(ViewState["MYTRANSID"]);

                    List<Database.planmealcustinvoiceHD> Listhd = DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                    if (Listhd.Count() > 0)
                    {
                        Database.planmealcustinvoiceHD objinv = Listhd.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
                        objinv.DriverID = Convert.ToInt32(drpChangeDriver.SelectedValue);
                        DB.SaveChanges();
                    }

                    if (chkAll.Checked == true)
                    {
                        List<Database.planmealcustinvoice> List = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                        if (List.Count() > 0)
                        {
                            List<Database.planmealcustinvoice> ListInvoice = List.Where(p => p.ExpectedDelDate >= DriverChangeDate && p.ActualDelDate == null).ToList();
                            foreach (Database.planmealcustinvoice items in ListInvoice)
                            {
                                Database.planmealcustinvoice objinv = ListInvoice.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items.DeliveryID);
                                objinv.DriverID = Convert.ToInt32(drpChangeDriver.SelectedValue);
                                DB.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        List<Database.planmealcustinvoice> List = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                        if (List.Count() > 0)
                        {
                            List<Database.planmealcustinvoice> ListInvoice = List.Where(p => p.ExpectedDelDate == DriverChangeDate && p.ActualDelDate == null).ToList();
                            foreach (Database.planmealcustinvoice items in ListInvoice)
                            {
                                Database.planmealcustinvoice objinv = ListInvoice.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items.DeliveryID);
                                objinv.DriverID = Convert.ToInt32(drpChangeDriver.SelectedValue);
                                DB.SaveChanges();
                            }
                        }
                    }
                }
            }
            else
            {
                lblChangeDridate.Visible = false;
                lblChangeDridate.Text = "Select Driver Date";
                return;
            }
        }

        protected void txtDriverChangeDate_TextChanged(object sender, EventArgs e)
        {
            lblChangeDridate.Visible = false;
            lblChangeDridate.Text = "";
            if (txtDriverChangeDate.Text != "")
            {
                DateTime DriverChangeDate = Convert.ToDateTime(txtDriverChangeDate.Text);
                ModalPopupExtender8.Show();
                DateTime Beingdate = Convert.ToDateTime(txtBeingDate.Text);
                DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
                DateTime Today = DateTime.Now;
                if (DriverChangeDate >= Beingdate && DriverChangeDate <= EndDate)
                {
                    if (DriverChangeDate <= Today)
                    {
                        lblChangeDridate.Visible = true;
                        lblChangeDridate.Text = "Select Driver Date Grater Than Today";
                    }
                }
                else
                {
                    lblChangeDridate.Visible = true;
                    lblChangeDridate.Text = "Select Driver Date Between Start Date And End Date Of Plan ";
                }
            }
            else
            {
                lblChangeDridate.Visible = false;
                lblChangeDridate.Text = "Select Driver Date";
                return;
            }
        }

        protected void lnkbtnUnHold_Click(object sender, EventArgs e)
        {
            lblUnholdMsg.Visible = false;
            lblUnholdMsg.Text = "";
            if (txtUnHoldDate.Text != "")
            {
                DateTime UnHoldDate = Convert.ToDateTime(txtUnHoldDate.Text);
                if (ViewState["MYTRANSID"] != null)
                {
                    int MYTRANSID = Convert.ToInt32(ViewState["MYTRANSID"]);
                    if (DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).Count() > 0)
                    {
                        Database.planmealcustinvoiceHD Objhd = DB.planmealcustinvoiceHDs.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);

                        DateTime HoldDate = Convert.ToDateTime(Objhd.HoldDate);
                        DateTime enddate = Convert.ToDateTime(Objhd.EndDate);

                        DateTime today = DateTime.Now;

                        List<Database.planmealcustinvoice> List = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();
                        List<Database.planmealcustinvoice> ListdelDay = List.Where(p => p.ActualDelDate == null).GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
                        int TimeCount = ListdelDay.Count();
                        DateTime Enddate = today.AddDays(TimeCount);
                        int days = CountDays(DayOfWeek.Friday, today, Enddate);

                        DateTime newenddate = Enddate.AddDays(days);

                        //int count = Convert.ToInt32((UnHoldDate - HoldDate).TotalDays);

                        //DateTime newenddate = enddate.AddDays(count);

                        Objhd.EndDate = newenddate;
                        Objhd.SubscriptionOnHold = false;
                        Objhd.UnHoldDate = UnHoldDate;
                        Objhd.HoldREmark = txtHoldRemark.Text;
                        Objhd.Holdbyuser = UID;
                        DB.SaveChanges();

                        txtEndDate.Text = newenddate.ToString("MM/dd/yyyy");
                        DateTime BDT = Convert.ToDateTime(txtBeingDate.Text);
                        int week = Convert.ToInt32((newenddate - BDT).TotalDays) / 7;
                        calculate();
                        int plan = Convert.ToInt32(drpPlan.SelectedValue);
                        Copyfullplan(MYTRANSID, week, plan);
                        recalculate();
                        int Custo = Convert.ToInt32(drpCustomer.SelectedValue);
                        int Meal = Convert.ToInt32(drpDeliveryMeal.SelectedValue);
                        Editinvoice(Custo, MYTRANSID, plan, Meal);

                        //List<Database.planmealcustinvoice> ListInvoiceDT = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate == null && p.ExpectedDelDate >= HoldDate).ToList();

                        //foreach (Database.planmealcustinvoice items in ListInvoiceDT)
                        //{
                        //    Database.planmealcustinvoice objremove = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == items.DeliveryID && p.MYPRODID==items.MYPRODID);
                        //    DB.planmealcustinvoices.DeleteObject(objremove);
                        //    DB.SaveChanges();
                        //}

                        //Editswicth();
                    }
                }
            }
            else
            {
                lblUnholdMsg.Visible = false;
                lblUnholdMsg.Text = "Select UnHold Date";
                return;
            }
        }

        static int CountDays(DayOfWeek day, DateTime start, DateTime end)
        {
            TimeSpan ts = end - start;
            int count = (int)Math.Floor(ts.TotalDays / 7);
            int remainder = (int)(ts.TotalDays % 7);
            int sinceLastDay = (int)(end.DayOfWeek - day);
            if (sinceLastDay < 0) sinceLastDay += 7;

            if (remainder >= sinceLastDay) count++;

            return count > 0 ? count : 0;
        }

        protected void txtUnHoldDate_TextChanged(object sender, EventArgs e)
        {
            lblUnholdMsg.Visible = false;
            lblUnholdMsg.Text = "";
            if (txtHoldDate.Text != "")
            {
                DateTime UnHoldDate = Convert.ToDateTime(txtUnHoldDate.Text);
                ModalPopupExtender9.Show();
                DateTime Beingdate = Convert.ToDateTime(txtBeingDate.Text);
                DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
                DateTime Today = DateTime.Now;
                if (UnHoldDate >= Beingdate && UnHoldDate <= EndDate)
                {
                    if (UnHoldDate <= Today)
                    {
                        lblUnholdMsg.Visible = true;
                        lblUnholdMsg.Text = "Select UnHold Date Grater Than Today";
                    }
                }
                else
                {
                    lblUnholdMsg.Visible = true;
                    lblUnholdMsg.Text = "Select UnHold Between Start Date And End Date Of Plan ";
                }
            }
            else
            {
                lblUnholdMsg.Visible = false;
                lblUnholdMsg.Text = "Select UnHold Date";
                return;
            }
        }

        public string getStartDate(DateTime StartDate)
        {
            if (StartDate != null)
            {
                return StartDate.ToString("dd-MMM-yyyy");
            }
            {
                return "Not Found";
            }

        }
        public string getEndDate(DateTime EndDate)
        {
            if (EndDate != null)
            {
                return EndDate.ToString("dd-MMM-yyyy");
            }
            {
                return "Not Found";
            }
        }

        public string getTotaldel(int MYTRANSID)
        {
            List<Database.planmealcustinvoice> List = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID).ToList();

            List<Database.planmealcustinvoice> ListtotaldelDay = List.GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
            int totalDelDay = ListtotaldelDay.Count();
            return ListtotaldelDay.Count().ToString();
        }

        public string getdeliverd(int MYTRANSID)
        {
            List<Database.planmealcustinvoice> ListdelDay = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate == null).GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
            int TimeCount = ListdelDay.Count();
            return TimeCount.ToString();
        }

        public string getTotaldeldone(int MYTRANSID)
        {
            List<Database.planmealcustinvoice> ListTotaldeldone = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.ActualDelDate != null).GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
            int DoneCount = ListTotaldeldone.Count();
            return DoneCount.ToString();
        }

        protected void lnkbtnreload_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void lnkbtndriveradd_Click(object sender, EventArgs e)
        {
            string DriverName = txtDriverEng.Text;

            lblDriverEnglish.Visible = false;
            lblDriverEnglish.Text = "";

            List<Database.tbl_Employee> ListEmp = DB.tbl_Employee.Where(p => p.TenentID == TID && p.firstname.ToUpper() == DriverName.ToUpper()).ToList();

            if (ListEmp.Count() > 0)
            {
                ModalPopupExtender10.Show();
                lblDriverEnglish.Visible = true;
                lblDriverEnglish.Text = "Driver Already Exist ";
                return;
            }
            else
            {
                int COMPID = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Count() > 0 ? DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID).Max(p => p.COMPID) + 1 : 1;
                Database.TBLCOMPANYSETUP ObjCompny = new TBLCOMPANYSETUP();
                ObjCompny.TenentID = TID;
                ObjCompny.COMPID = COMPID;
                ObjCompny.PHYSICALLOCID = "KWT";
                ObjCompny.COMPNAME1 = txtDriverEng.Text;
                ObjCompny.COMPNAME2 = txtDriverArabic.Text;
                ObjCompny.COMPNAME3 = txtDriverFranch.Text;
                ObjCompny.CITY = "79";
                ObjCompny.STATE = "1906";
                ObjCompny.POSTALCODE = "0";
                ObjCompny.ZIPCODE = "0";
                ObjCompny.MYCONLOCID = 0;
                ObjCompny.COUNTRYID = 126;
                ObjCompny.BUSPHONE1 = txtDriverMobile.Text;
                ObjCompny.BUSPHONE2 = "0";
                ObjCompny.BUSPHONE3 = "0";
                ObjCompny.BUSPHONE4 = "0";
                ObjCompny.MOBPHONE = txtDriverMobile.Text;
                ObjCompny.FAX = "0";
                ObjCompny.FAX1 = "0";
                ObjCompny.FAX2 = "0";
                ObjCompny.PRIMLANGUGE = "0";
                ObjCompny.WEBPAGE = "0";
                ObjCompny.ISMINISTRY = false;
                ObjCompny.ISSMB = false;
                ObjCompny.ISCORPORATE = false;
                ObjCompny.INHAWALLY = false;
                ObjCompny.SALER = false;
                ObjCompny.BUYER = false;
                ObjCompny.SALEDEPROD = false;
                ObjCompny.EMAISUB = false;
                ObjCompny.PRODUCTDEALIN = "0";
                ObjCompny.REMARKS = "0";
                ObjCompny.Keyword = "0";
                ObjCompny.COMPANYID = 0;
                ObjCompny.BUSID = 0;
                ObjCompny.MYCATSUBID = 0;
                ObjCompny.COMPNAME = "0";
                ObjCompny.COMPNAMEO = "0";
                ObjCompny.COMPNAMECH = "0";
                ObjCompny.Active = "Y";
                ObjCompny.CUSERID = txtdriverUserID.Text;
                ObjCompny.CPASSWRD = txtdriverpass.Text;
                //ObjCompny.USERID
                ObjCompny.ENTRYDATE = DateTime.Now;
                ObjCompny.ENTRYTIME = DateTime.Now;
                ObjCompny.UPDTTIME = DateTime.Now;
                ObjCompny.Approved = 1;
                ObjCompny.CompanyType = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.SHORTNAME == "Driver").Count() > 0 ? DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.SHORTNAME == "Driver").REFID.ToString() : "0";

                ObjCompny.Reload = false;

                DB.TBLCOMPANYSETUPs.AddObject(ObjCompny);
                DB.SaveChanges();

                int employeeID = DB.tbl_Employee.Where(p => p.TenentID == TID).Count() > 0 ? DB.tbl_Employee.Where(p => p.TenentID == TID).Max(p => p.employeeID) + 1 : 1;
                Database.tbl_Employee ObjEmp = new Database.tbl_Employee();
                ObjEmp.TenentID = TID;
                ObjEmp.LocationID = LID;
                ObjEmp.employeeID = employeeID;
                ObjEmp.contactID = COMPID;
                ObjEmp.ActiveDirectoryID = txtDriverEng.Text;
                ObjEmp.lastname = txtDriverEng.Text;
                ObjEmp.firstname = txtDriverEng.Text;
                ObjEmp.emp_nick_name = txtDriverEng.Text;
                ObjEmp.name2 = txtDriverArabic.Text;
                ObjEmp.name3 = txtDriverFranch.Text;
                ObjEmp.emp_mobile = txtDriverMobile.Text;
                ObjEmp.Studen_LoginID = employeeID.ToString();
                ObjEmp.PASSWORD = txtDriverEng.Text;
                ObjEmp.emp_smoker = 3;
                ObjEmp.EmployeeType = "Driver";
                ObjEmp.DeviceID = txtDeviceID.Text;
                ObjEmp.Active = true;
                ObjEmp.Deleted = false;

                DB.tbl_Employee.AddObject(ObjEmp);
                DB.SaveChanges();


                txtDriverEng.Text = "";
                txtDriverArabic.Text = "";
                txtDriverFranch.Text = "";
                txtDriverMobile.Text = "";
                txtdriverUserID.Text = "";
                txtdriverpass.Text = "";
                txtDeviceID.Text = "";
            }
        }

        protected void txtDriverEng_TextChanged(object sender, EventArgs e)
        {
            lblDriverEnglish.Visible = false;
            lblDriverEnglish.Text = "";
            string DriverName = txtDriverEng.Text;

            List<Database.tbl_Employee> ListEmp = DB.tbl_Employee.Where(p => p.TenentID == TID && p.firstname.ToUpper() == DriverName.ToUpper()).ToList();
            ModalPopupExtender10.Show();
            if (ListEmp.Count() < 1)
            {
                txtDriverArabic.Text = Translate(txtDriverEng.Text, "ar");
                txtDriverFranch.Text = Translate(txtDriverEng.Text, "fr");

            }
            else
            {
                lblDriverEnglish.Visible = true;
                lblDriverEnglish.Text = "Driver Already Exist ";
                return;

            }


        }

        public string Translate(string textvalue, string to)
        {
            string appId = "A70C584051881A30549986E65FF4B92B95B353A5";//go to http://msdn.microsoft.com/en-us/library/ff512386.aspx to obtain AppId.
            // string textvalue = "Translate this for me";
            string from = "en";

            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?appId=" + appId + "&text=" + textvalue + "&from=" + from + "&to=" + to;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    string translation = (string)dcs.ReadObject(stream);
                    return translation;
                }
            }
            catch (WebException e)
            {
                return "";
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }


        protected void ListViewContract_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton btnEditContract = (LinkButton)e.Item.FindControl("btnEditContract");
            Label lblMYTRANSID = (Label)e.Item.FindControl("lblMYTRANSID");
            int MYTRANSID = Convert.ToInt32(lblMYTRANSID.Text);

            if (DB.planmealcustinvoiceHDs.Where(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.CStatus != "Completed").Count() > 0)
            {
                btnEditContract.Visible = true;
            }
            else
            {
                btnEditContract.Visible = false;
            }
        }

        //protected void btnWeekofDay_Click(object sender, EventArgs e)
        //{
        //    string message = "";
        //    foreach (ListItem item in drpweekofday1.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            message += item.Text + ",";
        //        }
        //    }
        //    tags_1.Text = message;
        //}



        //protected void btntimeChange_Click(object sender, EventArgs e)
        //{
        //    if (ViewState["DeliveryTimeInvoice"] != null)
        //    {
        //        int Deltime = Convert.ToInt32(ViewState["DeliveryTimeInvoice"]);
        //        int DelivieryTimeinvo = Convert.ToInt32(drpDeliveryTime.SelectedValue);
        //        int Plan = Convert.ToInt32(drpPlan.SelectedValue);
        //        DateTime Today = DateTime.Now;
        //        Today = Today.AddDays(-1);
        //        if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.planid == Plan && p.DeliveryTime == Deltime && p.ExpectedDelDate > Today).Count() > 0)
        //        {
        //            string Str = "update planmealcustinvoice set DeliveryTime=" + DelivieryTimeinvo + " where TenentID=" + TID + " and DeliveryTime=" + Deltime + " and  ExpectedDelDate>'" + Today + "'";
        //            command2 = new SqlCommand(Str, con);
        //            con.Open();
        //            command2.ExecuteReader();
        //            con.Close();
        //        }
        //    }
        //}


        //public void UpdateActualDeliveryDate(int TID,int MYTRANSID, int DeliveryID, int MYPRODID, int NExtDeliveryNum, DateTime NExtDeliveryDate, DateTime ExpectedDelDate, int chiefID)
        //{
        //    Database.planmealcustinvoiceHD objhd = DB.planmealcustinvoiceHDs.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID);
        //    objhd.NExtDeliveryNum = NExtDeliveryNum;
        //    objhd.NExtDeliveryDate = NExtDeliveryDate;
        //    DB.SaveChanges();

        //    Database.planmealcustinvoice objdt = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.MYTRANSID == MYTRANSID && p.DeliveryID == DeliveryID && p.MYPRODID == MYPRODID);
        //    objdt.ActualDelDate = ExpectedDelDate;
        //    objdt.ProductionDate = ExpectedDelDate;
        //    objdt.chiefID = chiefID;
        //    DB.SaveChanges();
        //}

    }
}
public class tempweekofday
{
    public string Name { get; set; }
    public int ID { get; set; }
}
public class NoOfWeek
{
    public string name1 { get; set; }
    public int id1 { get; set; }
}