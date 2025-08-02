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

namespace Web.Master
{
    public partial class planmealsetup : System.Web.UI.Page
    {
        //planmealsetup to be called using SP1=Ad SP=NO 
        //in this case of SP1=AD 
        //The system will allow to act as an Admin and will allow to delete a plan provided plan is not in use by customer invoice planmealcustinvoice table
        //Default=NO
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        #endregion
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            PanelError.Visible = false;
            lblErrorMsg.Text = "";

            if (!IsPostBack)
            {
                MyRead();
                ManageLang();
                //pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                Label6.Visible = false;
                //FirstData();
                if (Request.QueryString["AD"] != null)
                {
                    int ID = Convert.ToInt32(Request.QueryString["AD"]);
                }
                btnAdd.ValidationGroup = "ss";
            }
        }
        #region Step2
        public void BindData()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            //Listview1.DataSource = DB.planmealsetups.Where(p => p.TenentID == TID && p.ACTIVE == true);
            //Listview1.DataBind();

            //var List1 = DB.planmealsetups.Where(p => p.TenentID == TID && p.ACTIVE == true).Select(p=>p.planid).Distinct().ToList();
            //List<Database.planmealsetup> TempListPlanMeal = new List<Database.planmealsetup>();            

            //foreach (var item in List1)
            //{
            //    var obj = DB.planmealsetups.FirstOrDefault(p => p.MealType == item);
            //    TempListPlanMeal.Add(obj);               
            //}
            //Listview1.DataSource = TempListPlanMeal;
            //Listview1.DataBind();

            //List<Database.planmealsetup> TempListPlanMeal = new List<Database.planmealsetup>();
            //List<Database.planmealsetup> ListPlan = DB.planmealsetups.Where(p => p.TenentID == TID).ToList();
            //foreach (Database.planmealsetup item in ListPlan)
            //{
            //    List<Database.planmealsetup> list1 = DB.planmealsetups.Where(p => p.planid == item.planid).ToList();
            //    foreach (Database.planmealsetup item2 in list1)
            //    {
            //        List<Database.planmealsetup> obj = DB.planmealsetups.Where(p => p.planid == item2.planid && p.MealType == item2.MealType).ToList();
            //        if (obj.Count() > 0)
            //            TempListPlanMeal.Add(obj[0]);
            //    }
            //}
            //Listview1.DataSource = TempListPlanMeal;
            //Listview1.DataBind();           
            var query = (from t in DB.planmealsetups.Where(p => p.TenentID == TID && p.ACTIVE == true).ToList()
                         group t by new { t.MealType, t.planid }
                             into grp
                             select new
                             {
                                 grp.Key.planid,
                                 grp.Key.MealType,

                             }).ToList();
            Listview1.DataSource = query;
            Listview1.DataBind();
        }
        #endregion

        public void GetShow()
        {

            lblPlanID1s.Attributes["class"] = lblMealID1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblPlanID2h.Attributes["class"] = lblMealID2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblPlanID1s.Attributes["class"] = lblMealID1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblPlanID2h.Attributes["class"] = lblMealID2h.Attributes["class"] = "control-label col-md-4  getshow";
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
        public void Clear()
        {
            drpPlanID.SelectedIndex = 0;
            drpMealID.SelectedIndex = 0;
            drpProduct.SelectedIndex = 0;
            txtserial_no.Text = "";
            txtProtainCarb.Text = "";
            txtCalories.Text = "";
            txtCarbs.Text = "";
            txtFat.Text = "";
            txtWeight.Text = "";
            chkaction.Checked = true;
            //drpcrupID.SelectedIndex = 0;
            //txtActive.Text = "";

        }

        public void Clear1()
        {
            //drpPlanID.SelectedIndex = 0;
            //drpMealID.SelectedIndex = 0;
            drpProduct.SelectedIndex = 0;
            txtserial_no.Text = "";
            txtProtainCarb.Text = "";
            txtCalories.Text = "";
            txtCarbs.Text = "";
            txtFat.Text = "";
            txtWeight.Text = "";
            chkaction.Checked = true;
            //drpcrupID.SelectedIndex = 0;
            //txtActive.Text = "";

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PanelError.Visible = false;
            lblErrorMsg.Text = "";

            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    try
            //    {
            if (btnAdd.Text == "Add New")
            {
                MyWrite();
                Clear1();
                btnAdd.Text = "Add";
                lblnew.Text = "Add New";//for first Heading
                lblhLast.Text = "";//for first Heading
                lblPNew.Text = "Add New";//for second Heading for green                

                Write();
                pnlhide.Visible = true;
                btnAdd.ValidationGroup = "submit";
                drpPlanID.Enabled = true;
                drpMealID.Enabled = true;
            }
            else if (btnAdd.Text == "Add")
            {
                int planid = Convert.ToInt32(drpPlanID.SelectedValue);
                int MealType = Convert.ToInt32(drpMealID.SelectedValue);
                long MYPRODID = Convert.ToInt64(drpProduct.SelectedValue);

                if (DB.planmealsetups.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == planid && p.MealType == MealType && p.MYPRODID == MYPRODID).Count() < 1)
                {
                    Database.planmealsetup objplanmealsetup = new Database.planmealsetup();
                    //Server Content Send data Yogesh
                    objplanmealsetup.TenentID = TID;
                    objplanmealsetup.LOCATION_ID = LID;
                    objplanmealsetup.planid = Convert.ToInt32(drpPlanID.SelectedValue);
                    objplanmealsetup.MealType = Convert.ToInt32(drpMealID.SelectedValue);
                    objplanmealsetup.MYPRODID = Convert.ToInt64(drpProduct.SelectedValue);

                    if (txtserial_no.Text != "")
                    {
                        objplanmealsetup.serial_no = Convert.ToInt32(txtserial_no.Text);
                    }

                    objplanmealsetup.ItemWeight = txtWeight.Text != "" ? Convert.ToDecimal(txtWeight.Text) : 100;
                    objplanmealsetup.Protein = txtProtainCarb.Text != "" ? Convert.ToDecimal(txtProtainCarb.Text) : 14;
                    objplanmealsetup.Calories = txtCalories.Text != "" ? Convert.ToDecimal(txtCalories.Text) : 300;
                    objplanmealsetup.Carbs = txtCarbs.Text != "" ? Convert.ToDecimal(txtCarbs.Text) : 36;
                    objplanmealsetup.Fat = txtFat.Text != "" ? Convert.ToDecimal(txtFat.Text) : 9;

                    objplanmealsetup.MealRepeatInDay = txtMealRepeatInDay.Text != "" ? Convert.ToInt32(txtMealRepeatInDay.Text) : 2;
                    objplanmealsetup.MealRepeatInWeek = txtMealRepeatInWeek.Text != "" ? Convert.ToInt32(txtMealRepeatInWeek.Text) : 12;
                    objplanmealsetup.MealRepeatInMonth = txtMealRepeatInMonth.Text != "" ? Convert.ToInt32(txtMealRepeatInMonth.Text) : 48;
                    objplanmealsetup.MealRepeatInYear = txtMealRepeatInYear.Text != "" ? Convert.ToInt32(txtMealRepeatInYear.Text) : 576;

                    objplanmealsetup.ACTIVE = chkaction.Checked == true ? true : false;

                    DB.planmealsetups.AddObject(objplanmealsetup);
                    DB.SaveChanges();

                    UpdateRepratdays();

                    ProductList();
                    Clear1();
                    btnAdd.Text = "Add New";
                    btnAdd.ValidationGroup = "ss";
                    lblMsg.Text = "  Data Save Successfully";
                    pnlSuccessMsg.Visible = true;
                }
                else
                {
                    Database.planmealsetup objplanmealsetup = DB.planmealsetups.Single(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == planid && p.MealType == MealType && p.MYPRODID == MYPRODID);
                    //Server Content Send data Yogesh

                    if (txtserial_no.Text != "")
                    {
                        objplanmealsetup.serial_no = Convert.ToInt32(txtserial_no.Text);
                    }

                    objplanmealsetup.ItemWeight = txtWeight.Text != "" ? Convert.ToDecimal(txtWeight.Text) : 100;
                    objplanmealsetup.Protein = txtProtainCarb.Text != "" ? Convert.ToDecimal(txtProtainCarb.Text) : 14;
                    objplanmealsetup.Calories = txtCalories.Text != "" ? Convert.ToDecimal(txtCalories.Text) : 300;
                    objplanmealsetup.Carbs = txtCarbs.Text != "" ? Convert.ToDecimal(txtCarbs.Text) : 36;
                    objplanmealsetup.Fat = txtFat.Text != "" ? Convert.ToDecimal(txtFat.Text) : 9;

                    objplanmealsetup.MealRepeatInDay = txtMealRepeatInDay.Text != "" ? Convert.ToInt32(txtMealRepeatInDay.Text) : 2;
                    objplanmealsetup.MealRepeatInWeek = txtMealRepeatInWeek.Text != "" ? Convert.ToInt32(txtMealRepeatInWeek.Text) : 12;
                    objplanmealsetup.MealRepeatInMonth = txtMealRepeatInMonth.Text != "" ? Convert.ToInt32(txtMealRepeatInMonth.Text) : 48;
                    objplanmealsetup.MealRepeatInYear = txtMealRepeatInYear.Text != "" ? Convert.ToInt32(txtMealRepeatInYear.Text) : 576;

                    objplanmealsetup.ACTIVE = chkaction.Checked == true ? true : false;

                    DB.SaveChanges();

                    UpdateRepratdays();

                    ProductList();
                    Clear1();
                    btnAdd.Text = "Add New";
                    btnAdd.ValidationGroup = "ss";
                    lblMsg.Text = "  Data Save Successfully";
                    pnlSuccessMsg.Visible = true;
                    //PanelError.Visible = true;
                    //lblErrorMsg.Text = "Item Already Exist";
                    //return;
                }

                BindData();

                //navigation.Visible = true;
                MyRead();
                //FirstData();
            }
            else if (btnAdd.Text == "Update")
            {

                if (ViewState["PID"] != null && ViewState["Meal"] != null && ViewState["Prod"] != null)
                {
                    int pid = Convert.ToInt32(ViewState["PID"]);
                    int meal = Convert.ToInt32(ViewState["Meal"]);
                    long prod = Convert.ToInt64(ViewState["Prod"]);
                    Database.planmealsetup objplanmealsetup = DB.planmealsetups.Single(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == pid && p.MealType == meal && p.MYPRODID == prod);
                    //objplanmealsetup.planid = Convert.ToInt32(drpPlanID.SelectedValue);
                    //objplanmealsetup.MealType = Convert.ToInt32(drpMealID.SelectedValue);
                    //objplanmealsetup.MYPRODID = Convert.ToInt64(drpProduct.SelectedValue);

                    if (txtserial_no.Text != "")
                    {
                        objplanmealsetup.serial_no = Convert.ToInt32(txtserial_no.Text);
                    }

                    objplanmealsetup.ItemWeight = txtWeight.Text != "" ? Convert.ToDecimal(txtWeight.Text) : 100;
                    objplanmealsetup.Protein = txtProtainCarb.Text != "" ? Convert.ToDecimal(txtProtainCarb.Text) : 14;
                    objplanmealsetup.Calories = txtCalories.Text != "" ? Convert.ToDecimal(txtCalories.Text) : 300;
                    objplanmealsetup.Carbs = txtCarbs.Text != "" ? Convert.ToDecimal(txtCarbs.Text) : 36;
                    objplanmealsetup.Fat = txtFat.Text != "" ? Convert.ToDecimal(txtFat.Text) : 9;

                    objplanmealsetup.MealRepeatInDay = txtMealRepeatInDay.Text != "" ? Convert.ToInt32(txtMealRepeatInDay.Text) : 2;
                    objplanmealsetup.MealRepeatInWeek = txtMealRepeatInWeek.Text != "" ? Convert.ToInt32(txtMealRepeatInWeek.Text) : 12;
                    objplanmealsetup.MealRepeatInMonth = txtMealRepeatInMonth.Text != "" ? Convert.ToInt32(txtMealRepeatInMonth.Text) : 48;
                    objplanmealsetup.MealRepeatInYear = txtMealRepeatInYear.Text != "" ? Convert.ToInt32(txtMealRepeatInYear.Text) : 576;

                    objplanmealsetup.ACTIVE = chkaction.Checked == true ? true : false;

                    ViewState["PID"] = null;
                    ViewState["Meal"] = null;
                    ViewState["Prod"] = null;
                    btnAdd.Text = "Add New";
                    btnAdd.ValidationGroup = "ss";
                    DB.SaveChanges();

                    UpdateRepratdays();

                    ProductList();
                    Clear1();
                    lblMsg.Text = "  Data Edit Successfully";
                    pnlSuccessMsg.Visible = true;
                    BindData();

                    //navigation.Visible = true;
                    MyRead();
                    //FirstData();
                }
            }
            BindData();

            //        scope.Complete(); //  To commit.

            //    }
            //    catch (Exception ex)
            //    {
            //        throw;
            //    }
            //}
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Session["Previous"].ToString());
            MyRead();
            Readonly();
            ManageLang();
            pnlSuccessMsg.Visible = false;
            FillContractorID();
            btnAdd.Text = "Add New";
            BindData();
            pnlhide.Visible = false;
            lblnew.Text = "View";//for first Heading            
            lblPNew.Text = "View";//for second Heading for green
            Listview3.DataSource = null;
            Listview3.DataBind();
            drpPlanID.Enabled = true;
            drpMealID.Enabled = true;
        }

        public void UpdateRepratdays()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            int planid = Convert.ToInt32(drpPlanID.SelectedValue);
            int MealType = Convert.ToInt32(drpMealID.SelectedValue);

            List<Database.planmealsetup> ListPlanMeal = DB.planmealsetups.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == planid && p.MealType == MealType).ToList();
            foreach (Database.planmealsetup items in ListPlanMeal)
            {
                Database.planmealsetup objedit = items;
                objedit.MealRepeatInDay = txtMealRepeatInDay.Text != "" ? Convert.ToInt32(txtMealRepeatInDay.Text) : 1;
                objedit.MealRepeatInWeek = txtMealRepeatInWeek.Text != "" ? Convert.ToInt32(txtMealRepeatInWeek.Text) : 6;
                objedit.MealRepeatInMonth = txtMealRepeatInMonth.Text != "" ? Convert.ToInt32(txtMealRepeatInMonth.Text) : 24;
                objedit.MealRepeatInYear = txtMealRepeatInYear.Text != "" ? Convert.ToInt32(txtMealRepeatInYear.Text) : 288;

                DB.SaveChanges();
            }

        }
        public string PlaneName(int PID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            if (DB.tblProduct_Plan.Where(p => p.TenentID == TID && p.planid == PID).Count() > 0)
            {
                return DB.tblProduct_Plan.Single(p => p.TenentID == TID && p.planid == PID).planname1;
            }
            else
            {
                return "Not Found";
            }

        }
        public string MealN(int MID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (DB.REFTABLEs.Where(p => p.REFID == MID && p.TenentID == TID).Count() > 0)
            {
                return DB.REFTABLEs.Single(p => p.REFID == MID && p.TenentID == TID).REFNAME1;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetProd(long PRODID)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
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
        public void FillContractorID()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);

            drpPlanID.DataSource = DB.tblProduct_Plan.Where(p => p.TenentID == TID).OrderBy(p => p.planname1);
            drpPlanID.DataTextField = "planname1";
            drpPlanID.DataValueField = "planid";
            drpPlanID.DataBind();
            drpPlanID.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpMealID.DataSource = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "MealType").OrderBy(p => p.REFNAME1);
            drpMealID.DataTextField = "REFNAME1";
            drpMealID.DataValueField = "REFID";
            drpMealID.DataBind();
            drpMealID.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpProduct.DataSource = DB.TBLPRODUCTs.Where(p => p.TenentID == TID).OrderBy(p => p.ProdName1);
            drpProduct.DataTextField = "ProdName1";
            drpProduct.DataValueField = "MYPRODID";
            drpProduct.DataBind();
            drpProduct.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpFromPlan.DataSource = DB.tblProduct_Plan.Where(p => p.TenentID == TID).OrderBy(p => p.planname1);
            drpFromPlan.DataTextField = "planname1";
            drpFromPlan.DataValueField = "planid";
            drpFromPlan.DataBind();
            drpFromPlan.Items.Insert(0, new ListItem("-- Select --", "0"));

            drpTOPlan.DataSource = DB.tblProduct_Plan.Where(p => p.TenentID == TID).OrderBy(p => p.planname1);
            drpTOPlan.DataTextField = "planname1";
            drpTOPlan.DataValueField = "planid";
            drpTOPlan.DataBind();
            drpTOPlan.Items.Insert(0, new ListItem("-- Select --", "0"));

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
            int index = Convert.ToInt32(ViewState["Index"]);
            Listview1.SelectedIndex = 0;

            drpPlanID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            drpMealID.SelectedValue = Listview1.SelectedDataKey[3].ToString();

        }
        public void NextData()
        {

            if (Listview1.SelectedIndex != Listview1.Items.Count - 1)
            {
                Listview1.SelectedIndex = Listview1.SelectedIndex + 1;

                drpPlanID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                drpMealID.SelectedValue = Listview1.SelectedDataKey[3].ToString();

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

                drpPlanID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
                drpMealID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
            }
        }
        public void LastData()
        {
            Listview1.SelectedIndex = Listview1.Items.Count - 1;

            drpPlanID.SelectedValue = Listview1.SelectedDataKey[2].ToString();
            drpMealID.SelectedValue = Listview1.SelectedDataKey[3].ToString();
        }
        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblPlanID2h.Visible = lblMealID2h.Visible = false;
                    //2true
                    txtPlanID2h.Visible = txtMealID2h.Visible = true;

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
                    lblPlanID2h.Visible = lblMealID2h.Visible = true;
                    //2false
                    txtPlanID2h.Visible = txtMealID2h.Visible = false;

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
                    lblPlanID1s.Visible = lblMealID1s.Visible = false;
                    //1true
                    txtPlanID1s.Visible = txtMealID1s.Visible = true;
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
                    lblPlanID1s.Visible = lblMealID1s.Visible = true;
                    //1false
                    txtPlanID1s.Visible = txtMealID1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("planmealsetup").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblPlanID1s.ID == item.LabelID)
                    txtPlanID1s.Text = lblPlanID1s.Text = item.LabelName;
                else if (lblMealID1s.ID == item.LabelID)
                    txtMealID1s.Text = lblMealID1s.Text = item.LabelName;

                else if (lblPlanID2h.ID == item.LabelID)
                    txtPlanID2h.Text = lblPlanID2h.Text = item.LabelName;
                else if (lblMealID2h.ID == item.LabelID)
                    txtMealID2h.Text = lblMealID2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("planmealsetup").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\planmealsetup.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("planmealsetup").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                if (lblPlanID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlanID1s.Text;
                else if (lblMealID1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMealID1s.Text;

                else if (lblPlanID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtPlanID2h.Text;
                else if (lblMealID2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtMealID2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\planmealsetup.xml"));

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
            drpProduct.Enabled = true;
            txtProtainCarb.Enabled = true;
            txtserial_no.Enabled = true;
            txtCalories.Enabled = true;
            txtCarbs.Enabled = true;
            txtFat.Enabled = true;
            txtWeight.Enabled = true;

            txtMealRepeatInDay.Enabled = true;
            txtMealRepeatInWeek.Enabled = true;
            txtMealRepeatInMonth.Enabled = true;
            txtMealRepeatInYear.Enabled = true;

            chkaction.Enabled = true;
            for (int i = 0; i < Listview3.Items.Count; i++)
            {
                LinkButton btnEdit = (LinkButton)Listview3.Items[i].FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)Listview3.Items[i].FindControl("btnDelete");
                btnEdit.Visible = true;
                btnEdit.CssClass = "btn btn-sm yellow filter-submit margin-bottom";
                btnDelete.Visible = true;
                btnDelete.CssClass = "btn btn-sm red filter-cancel";
            }
        }
        public void Readonly()
        {

            //drpPlanID.Enabled = false;
            //drpMealID.Enabled = false;
            drpProduct.Enabled = false;
            txtProtainCarb.Enabled = false;
            txtserial_no.Enabled = false;
            txtCalories.Enabled = false;
            txtCarbs.Enabled = false;
            txtFat.Enabled = false;
            txtWeight.Enabled = false;

            txtMealRepeatInDay.Enabled = false;
            txtMealRepeatInWeek.Enabled = false;
            txtMealRepeatInMonth.Enabled = false;
            txtMealRepeatInYear.Enabled = false;

            chkaction.Enabled = false;
            for (int i = 0; i < Listview3.Items.Count; i++)
            {
                LinkButton btnEdit = (LinkButton)Listview3.Items[i].FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)Listview3.Items[i].FindControl("btnDelete");
                btnEdit.Visible = false;
                btnEdit.CssClass = "btn btn-sm yellow filter-submit margin-bottom";
                btnDelete.Visible = false;
                btnDelete.CssClass = "btn btn-sm red filter-cancel";
            }

        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.planmealsetups.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.planmealsetups.Where(p => p.TenentID == TID).OrderBy(m => m.planid).Take(take).Skip(Skip)).ToList());
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

        //    //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //}
        //protected void btnPrevious1_Click(object sender, EventArgs e)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.planmealsetups.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.planmealsetups.Where(p => p.TenentID == TID).OrderBy(m => m.planid).Take(take).Skip(Skip)).ToList());
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
        //        //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
        //    }
        //}
        //protected void btnfirst_Click(object sender, EventArgs e)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    if (ViewState["Take"] != null && ViewState["Skip"] != null)
        //    {
        //        int Totalrec = DB.planmealsetups.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.planmealsetups.Where(p => p.TenentID == TID).OrderBy(m => m.planid).Take(take).Skip(Skip)).ToList());
        //        ViewState["Take"] = take;
        //        ViewState["Skip"] = Skip;
        //        btnPrevious1.Enabled = false;
        //        ChoiceID = 0;
        //        //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
        //        if (take == Totalrec && Skip == (Totalrec - Showdata))
        //            btnNext1.Enabled = false;
        //        else
        //            btnNext1.Enabled = true;
        //        lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        //    }
        //}
        //protected void btnLast1_Click(object sender, EventArgs e)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.planmealsetups.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview1, (DB.planmealsetups.Where(p => p.TenentID == TID).OrderBy(m => m.planid).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    //((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2);
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        //}
        //protected void btnlistreload_Click(object sender, EventArgs e)
        //{
        //    BindData();
        //}
        protected void btnPagereload_Click(object sender, EventArgs e)
        {
            MyRead();
            ManageLang();
            pnlSuccessMsg.Visible = false;
            FillContractorID();
            int CurrentID = 1;
            if (ViewState["Es"] != null)
                CurrentID = Convert.ToInt32(ViewState["Es"]);
            BindData();

            //FirstData();
        }
        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (e.CommandName == "btnDelete")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int PID = Convert.ToInt32(ID[0]);
                int Meal = Convert.ToInt32(ID[1]);
                if (Request.QueryString["AD"] != null)
                {
                    int AID = Convert.ToInt32(Request.QueryString["AD"]);
                    int UID = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                    int Admin = Convert.ToInt32(DB.FUNCTION_MST.Single(p => p.TenentID == TID && p.MENU_ID == AID).SP1);
                    //Database.FUNCTION_MST ADMINobj = DB.FUNCTION_MST.Single(p => p.TenentID == TID && p.MENU_ID == AID);
                    if (Admin == UID)
                    {
                        if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.planid == PID && p.MealType == Meal && p.ExpectedDelDate != null).Count() > 0)
                        {
                            List<Database.planmealsetup> listDEL = DB.planmealsetups.Where(p => p.TenentID == TID && p.planid == PID && p.MealType == Meal && p.ACTIVE == true).ToList();
                            foreach (Database.planmealsetup item in listDEL)
                            {
                                Database.planmealsetup obj = DB.planmealsetups.Single(p => p.TenentID == TID && p.MYPRODID == item.MYPRODID && p.planid == PID && p.MealType == Meal && p.ACTIVE == true);
                                obj.ACTIVE = false;
                                DB.SaveChanges();
                            }
                        }
                        else
                        {
                            Label6.Visible = true;
                            Label6.Text = "This Group is AllReady Used in invoice...";
                        }
                    }
                    else
                    {
                        Label6.Visible = true;
                        Label6.Text = "You Have A No Permission To Delete A Group...";
                    }
                }
                BindData();
                Listview3.DataSource = DB.planmealsetups.Where(p => p.TenentID == TID && p.planid == PID && p.MealType == Meal && p.ACTIVE == true).OrderBy(p => p.serial_no);
                Listview3.DataBind();

            }

            if (e.CommandName == "btnEdit")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int PID = Convert.ToInt32(ID[0]);
                int Meal = Convert.ToInt32(ID[1]);
                //long Prod = Convert.ToInt64(ID[2]);

                Listview3.DataSource = DB.planmealsetups.Where(p => p.TenentID == TID && p.planid == PID && p.MealType == Meal && p.ACTIVE == true).OrderBy(p => p.serial_no);
                Listview3.DataBind();

                //Database.planmealsetup objplanmealsetup = DB.planmealsetups.Single(p => p.TenentID == TID && p.planid == PID && p.MealType == Meal);
                //drpPlanID.SelectedValue = objplanmealsetup.planid.ToString();
                //drpMealID.SelectedValue = objplanmealsetup.MealType.ToString();
                //drpProduct.SelectedValue = objplanmealsetup.MYPRODID.ToString();
                //txtProtainCarb.Text = Convert.ToDecimal(objplanmealsetup.Item_cost).ToString();
                //chkaction.Checked = objplanmealsetup.ACTIVE == true ? true : false;

                //btnAdd.Text = "Update";
                //ViewState["PID"] = PID;
                //ViewState["Meal"] = Meal;
                //ViewState["Prod"] = Prod;
                MyWrite();
                lblnew.Text = "Edit";// first Heading
                lblhLast.Text = "For Plan & Meal";// first Heading
                lblPNew.Text = "Edit";// Second Heading Green
                pnlhide.Visible = false;
                drpPlanID.SelectedValue = PID.ToString();
                drpMealID.SelectedValue = Meal.ToString();
            }
            if (e.CommandName == "btnview")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int PID = Convert.ToInt32(ID[0]);
                int Meal = Convert.ToInt32(ID[1]);

                drpPlanID.SelectedValue = PID.ToString();
                drpMealID.SelectedValue = Meal.ToString();
                //MyRead();
                Listview3.DataSource = DB.planmealsetups.Where(p => p.TenentID == TID && p.planid == PID && p.MealType == Meal && p.ACTIVE == true).OrderBy(p => p.serial_no);
                Listview3.DataBind();
                Readonly();
                lblnew.Text = "View";
                lblhLast.Text = "For Plan & Meal";
                pnlhide.Visible = false;
            }

        }
        protected void Listview3_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (e.CommandName == "btnDelete11")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int PID = Convert.ToInt32(ID[0]);
                int Meal = Convert.ToInt32(ID[1]);
                long Prod = Convert.ToInt64(ID[2]);

                Database.planmealsetup objplanmealsetup = DB.planmealsetups.Single(p => p.TenentID == TID && p.planid == PID && p.MealType == Meal && p.MYPRODID == Prod);
                objplanmealsetup.ACTIVE = false;
                DB.SaveChanges();
                lblPNew.Text = "Delete";

                Listview3.DataSource = DB.planmealsetups.Where(p => p.TenentID == TID && p.planid == PID && p.MealType == Meal && p.ACTIVE == true).OrderBy(p => p.serial_no);
                Listview3.DataBind();
            }
            if (e.CommandName == "btnEdit11")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int PID = Convert.ToInt32(ID[0]);
                int Meal = Convert.ToInt32(ID[1]);
                long Prod = Convert.ToInt64(ID[2]);

                Database.planmealsetup objplanmealsetup = DB.planmealsetups.Single(p => p.TenentID == TID && p.planid == PID && p.MealType == Meal && p.MYPRODID == Prod);
                drpPlanID.SelectedValue = objplanmealsetup.planid.ToString();
                drpMealID.SelectedValue = objplanmealsetup.MealType.ToString();
                drpProduct.SelectedValue = objplanmealsetup.MYPRODID.ToString();
                txtProtainCarb.Text = objplanmealsetup.Protein.ToString();
                txtserial_no.Text = objplanmealsetup.serial_no.ToString();
                txtCalories.Text = objplanmealsetup.Calories.ToString();
                txtCarbs.Text = objplanmealsetup.Carbs.ToString();
                txtFat.Text = objplanmealsetup.Fat.ToString();
                txtWeight.Text = objplanmealsetup.ItemWeight.ToString();

                txtMealRepeatInDay.Text = objplanmealsetup.MealRepeatInDay.ToString();
                txtMealRepeatInWeek.Text = objplanmealsetup.MealRepeatInWeek.ToString();
                txtMealRepeatInMonth.Text = objplanmealsetup.MealRepeatInMonth.ToString();
                txtMealRepeatInYear.Text = objplanmealsetup.MealRepeatInYear.ToString();

                chkaction.Checked = objplanmealsetup.ACTIVE == true ? true : false;

                pnlhide.Visible = true;
                btnAdd.Text = "Update";
                ViewState["PID"] = PID;
                ViewState["Meal"] = Meal;
                ViewState["Prod"] = Prod;
                MyWrite();
                drpPlanID.Enabled = false;
                drpMealID.Enabled = false;
                drpProduct.Enabled = false;
                lblPNew.Text = "Edit";
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            int pid = Convert.ToInt32(drpPlanID.SelectedValue);
            int MEAL = Convert.ToInt32(drpMealID.SelectedValue);

            //List<Database.planmealsetup> List = DB.planmealsetups.Where(p => p.TenentID == TID && p.LOCATION_ID==LID && p.planid == pid && p.MealType == MEAL && p.ACTIVE==false).ToList();
            //foreach (Database.planmealsetup item in List)
            //{
            //    Database.planmealsetup obj = item;
            //    obj.ACTIVE = true;
            //    DB.SaveChanges();
            //}


            Listview1.DataSource = DB.planmealsetups.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == pid && p.MealType == MEAL).GroupBy(p => p.MealType).Select(p => p.FirstOrDefault());
            Listview1.DataBind();

            ProductList();
            if (btnAdd.Text == "Add")
                Write();
            else
                Readonly();

        }
        public void ProductList()
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int pid = Convert.ToInt32(drpPlanID.SelectedValue);
            int MEAL = Convert.ToInt32(drpMealID.SelectedValue);
            Listview3.DataSource = DB.planmealsetups.Where(p => p.TenentID == TID && p.planid == pid && p.MealType == MEAL && p.ACTIVE == true).OrderBy(p => p.serial_no);
            Listview3.DataBind();
        }
        public void MyRead()
        {
            //drpPlanID.Enabled = false;
            //drpMealID.Enabled = false;
            drpProduct.Enabled = false;
            txtProtainCarb.Enabled = false;
            txtserial_no.Enabled = false;
            txtCalories.Enabled = false;
            txtCarbs.Enabled = false;
            txtFat.Enabled = false;
            txtWeight.Enabled = false;

            txtMealRepeatInDay.Enabled = false;
            txtMealRepeatInWeek.Enabled = false;
            txtMealRepeatInMonth.Enabled = false;
            txtMealRepeatInYear.Enabled = false;

            chkaction.Enabled = false;
        }
        public void MyWrite()
        {
            //drpPlanID.Enabled = true;
            //drpMealID.Enabled = true;
            drpProduct.Enabled = true;
            txtProtainCarb.Enabled = true;
            txtserial_no.Enabled = true;
            txtCalories.Enabled = true;
            txtCarbs.Enabled = true;
            txtFat.Enabled = true;
            txtWeight.Enabled = true;

            txtMealRepeatInDay.Enabled = true;
            txtMealRepeatInWeek.Enabled = true;
            txtMealRepeatInMonth.Enabled = true;
            txtMealRepeatInYear.Enabled = true;

            chkaction.Enabled = true;
        }
        protected void lblreloadd_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            PanelError.Visible = false;
            lblErrorMsg.Text = "";

            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);
            if (btnAdd.Text == "Add")
            {
                int planid = Convert.ToInt32(drpPlanID.SelectedValue);
                int MealType = Convert.ToInt32(drpMealID.SelectedValue);
                long MYPRODID = Convert.ToInt64(drpProduct.SelectedValue);

                if (DB.planmealsetups.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == planid && p.MealType == MealType && p.MYPRODID == MYPRODID).Count() < 1)
                {
                    Database.planmealsetup objplanmealsetup = new Database.planmealsetup();
                    //Server Content Send data Yogesh
                    objplanmealsetup.TenentID = TID;
                    objplanmealsetup.LOCATION_ID = 1;
                    objplanmealsetup.planid = Convert.ToInt32(drpPlanID.SelectedValue);
                    objplanmealsetup.MealType = Convert.ToInt32(drpMealID.SelectedValue);
                    objplanmealsetup.MYPRODID = Convert.ToInt64(drpProduct.SelectedValue);

                    if (txtserial_no.Text != "")
                    {
                        objplanmealsetup.serial_no = Convert.ToInt32(txtserial_no.Text);
                    }

                    objplanmealsetup.ItemWeight = txtWeight.Text != "" ? Convert.ToDecimal(txtWeight.Text) : 100;
                    objplanmealsetup.Protein = txtProtainCarb.Text != "" ? Convert.ToDecimal(txtProtainCarb.Text) : 14;
                    objplanmealsetup.Calories = txtCalories.Text != "" ? Convert.ToDecimal(txtCalories.Text) : 300;
                    objplanmealsetup.Carbs = txtCarbs.Text != "" ? Convert.ToDecimal(txtCarbs.Text) : 36;
                    objplanmealsetup.Fat = txtFat.Text != "" ? Convert.ToDecimal(txtFat.Text) : 9;

                    objplanmealsetup.MealRepeatInDay = txtMealRepeatInDay.Text != "" ? Convert.ToInt32(txtMealRepeatInDay.Text) : 2;
                    objplanmealsetup.MealRepeatInWeek = txtMealRepeatInWeek.Text != "" ? Convert.ToInt32(txtMealRepeatInWeek.Text) : 12;
                    objplanmealsetup.MealRepeatInMonth = txtMealRepeatInMonth.Text != "" ? Convert.ToInt32(txtMealRepeatInMonth.Text) : 48;
                    objplanmealsetup.MealRepeatInYear = txtMealRepeatInYear.Text != "" ? Convert.ToInt32(txtMealRepeatInYear.Text) : 576;

                    objplanmealsetup.ACTIVE = chkaction.Checked == true ? true : false;

                    DB.planmealsetups.AddObject(objplanmealsetup);
                    DB.SaveChanges();

                    UpdateRepratdays();

                    ProductList();
                    Clear1();
                    btnAdd.Text = "Add New";
                    btnAdd.ValidationGroup = "ss";
                    lblMsg.Text = "  Data Save Successfully";
                    pnlSuccessMsg.Visible = true;
                }
                else
                {

                    Database.planmealsetup objplanmealsetup = DB.planmealsetups.Single(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == planid && p.MealType == MealType && p.MYPRODID == MYPRODID);
                    if (txtserial_no.Text != "")
                    {
                        objplanmealsetup.serial_no = Convert.ToInt32(txtserial_no.Text);
                    }

                    objplanmealsetup.ItemWeight = txtWeight.Text != "" ? Convert.ToDecimal(txtWeight.Text) : 100;
                    objplanmealsetup.Protein = txtProtainCarb.Text != "" ? Convert.ToDecimal(txtProtainCarb.Text) : 14;
                    objplanmealsetup.Calories = txtCalories.Text != "" ? Convert.ToDecimal(txtCalories.Text) : 300;
                    objplanmealsetup.Carbs = txtCarbs.Text != "" ? Convert.ToDecimal(txtCarbs.Text) : 36;
                    objplanmealsetup.Fat = txtFat.Text != "" ? Convert.ToDecimal(txtFat.Text) : 9;

                    objplanmealsetup.MealRepeatInDay = txtMealRepeatInDay.Text != "" ? Convert.ToInt32(txtMealRepeatInDay.Text) : 2;
                    objplanmealsetup.MealRepeatInWeek = txtMealRepeatInWeek.Text != "" ? Convert.ToInt32(txtMealRepeatInWeek.Text) : 12;
                    objplanmealsetup.MealRepeatInMonth = txtMealRepeatInMonth.Text != "" ? Convert.ToInt32(txtMealRepeatInMonth.Text) : 48;
                    objplanmealsetup.MealRepeatInYear = txtMealRepeatInYear.Text != "" ? Convert.ToInt32(txtMealRepeatInYear.Text) : 576;

                    objplanmealsetup.ACTIVE = chkaction.Checked == true ? true : false;
                    objplanmealsetup.ACTIVE = true;
                    DB.SaveChanges();

                    UpdateRepratdays();

                    //PanelError.Visible = true;
                    //lblErrorMsg.Text = "Item Already Exist";
                    //return;

                    ProductList();
                    Clear1();
                    btnAdd.Text = "Add New";
                    btnAdd.ValidationGroup = "ss";
                    lblMsg.Text = "  Data Save Successfully";
                    pnlSuccessMsg.Visible = true;
                }
                BindData();

                //navigation.Visible = true;
                MyRead();
                //FirstData();
            }
            else if (btnAdd.Text == "Update")
            {
                if (ViewState["PID"] != null && ViewState["Meal"] != null && ViewState["Prod"] != null)
                {
                    int pid = Convert.ToInt32(ViewState["PID"]);
                    int meal = Convert.ToInt32(ViewState["Meal"]);
                    long prod = Convert.ToInt64(ViewState["Prod"]);
                    Database.planmealsetup objplanmealsetup = DB.planmealsetups.Single(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == pid && p.MealType == meal && p.MYPRODID == prod);
                    //objplanmealsetup.planid = Convert.ToInt32(drpPlanID.SelectedValue);
                    //objplanmealsetup.MealType = Convert.ToInt32(drpMealID.SelectedValue);
                    //objplanmealsetup.MYPRODID = Convert.ToInt64(drpProduct.SelectedValue);

                    if (txtserial_no.Text != "")
                    {
                        objplanmealsetup.serial_no = Convert.ToInt32(txtserial_no.Text);
                    }

                    objplanmealsetup.ItemWeight = txtWeight.Text != "" ? Convert.ToDecimal(txtWeight.Text) : 100;
                    objplanmealsetup.Protein = txtProtainCarb.Text != "" ? Convert.ToDecimal(txtProtainCarb.Text) : 14;
                    objplanmealsetup.Calories = txtCalories.Text != "" ? Convert.ToDecimal(txtCalories.Text) : 300;
                    objplanmealsetup.Carbs = txtCarbs.Text != "" ? Convert.ToDecimal(txtCarbs.Text) : 36;
                    objplanmealsetup.Fat = txtFat.Text != "" ? Convert.ToDecimal(txtFat.Text) : 9;

                    objplanmealsetup.MealRepeatInDay = txtMealRepeatInDay.Text != "" ? Convert.ToInt32(txtMealRepeatInDay.Text) : 2;
                    objplanmealsetup.MealRepeatInWeek = txtMealRepeatInWeek.Text != "" ? Convert.ToInt32(txtMealRepeatInWeek.Text) : 12;
                    objplanmealsetup.MealRepeatInMonth = txtMealRepeatInMonth.Text != "" ? Convert.ToInt32(txtMealRepeatInMonth.Text) : 48;
                    objplanmealsetup.MealRepeatInYear = txtMealRepeatInYear.Text != "" ? Convert.ToInt32(txtMealRepeatInYear.Text) : 576;

                    objplanmealsetup.ACTIVE = chkaction.Checked == true ? true : false;
                    DB.SaveChanges();

                    UpdateRepratdays();

                    ViewState["PID"] = null;
                    ViewState["Meal"] = null;
                    ViewState["Prod"] = null;
                    btnAdd.Text = "Add New";
                    btnAdd.ValidationGroup = "ss";

                    ProductList();
                    Clear1();
                    lblMsg.Text = "  Data Edit Successfully";
                    pnlSuccessMsg.Visible = true;
                    BindData();
                    MyRead();
                }
            }
            BindData();
        }

        public string getprotein(decimal Protein = 14, decimal Calories = 300, decimal Carbs = 36, decimal Fat = 9, decimal ItemWeight = 100)
        {
            string proteincarb = Protein.ToString() + ", " + Calories.ToString() + ", " + Carbs.ToString() + ", " + Fat.ToString() + ", " + ItemWeight.ToString();
            return proteincarb;
        }

        protected void lnkbtnCopyplan_Click(object sender, EventArgs e)
        {
            lblmessege.Visible = false;
            lblmessege.Text = "";

            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);

            int Fromplan = Convert.ToInt32(drpFromPlan.SelectedValue);
            int TOplan = Convert.ToInt32(drpTOPlan.SelectedValue);

            if (Fromplan != TOplan)
            {
                List<Database.planmealsetup> ListPlan = DB.planmealsetups.Where(p => p.TenentID == TID && p.planid == Fromplan).ToList();

                foreach (Database.planmealsetup items in ListPlan)
                {
                    if (DB.planmealsetups.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.planid == TOplan && p.MealType == items.MealType && p.MYPRODID == items.MYPRODID).Count() < 1)
                    {
                        Database.planmealsetup Objplan = new Database.planmealsetup();

                        Objplan.TenentID = TID;
                        Objplan.LOCATION_ID = LID;
                        Objplan.planid = TOplan;
                        Objplan.MealType = items.MealType;
                        Objplan.MYPRODID = items.MYPRODID;
                        Objplan.serial_no = items.serial_no;
                        Objplan.Calories = items.Calories;
                        Objplan.Protein = items.Protein;
                        Objplan.Carbs = items.Carbs;
                        Objplan.Fat = items.Fat;
                        Objplan.ItemWeight = items.ItemWeight;
                        Objplan.Item_cost = items.Item_cost;
                        Objplan.ShortRemark = items.ShortRemark;
                        Objplan.MealRepeatInDay = items.MealRepeatInDay;
                        Objplan.MealRepeatInWeek = items.MealRepeatInWeek;
                        Objplan.MealRepeatInMonth = items.MealRepeatInMonth;
                        Objplan.MealRepeatInYear = items.MealRepeatInYear;
                        Objplan.ACTIVE = items.ACTIVE;
                        Objplan.ChangesDate = items.ChangesDate;

                        DB.planmealsetups.AddObject(Objplan);
                        DB.SaveChanges();

                        UpdateRepratdays();
                    }
                }
            }
            else
            {
                lblmessege.Visible = false;
                lblmessege.Text = "";
                ModalPopupExtender7.Show();
                return;
            }

        }

        protected void txtMealRepeatInDay_TextChanged(object sender, EventArgs e)
        {
            if (txtMealRepeatInDay.Text != "")
            {
                int RepeatDay = Convert.ToInt32(txtMealRepeatInDay.Text);
                if (RepeatDay != 0)
                {
                    int repeatWeek = RepeatDay * 6;
                    txtMealRepeatInWeek.Text = repeatWeek.ToString();
                    int repeatMonth = repeatWeek * 4;
                    txtMealRepeatInMonth.Text = repeatMonth.ToString();
                    int repeatYear = repeatMonth * 12;
                    txtMealRepeatInYear.Text = repeatYear.ToString();
                }
            }
        }

        protected void txtMealRepeatInWeek_TextChanged(object sender, EventArgs e)
        {
            if (txtMealRepeatInWeek.Text != "")
            {
                int repeatWeek = Convert.ToInt32(txtMealRepeatInWeek.Text);
                if (repeatWeek != 0)
                {
                    int repeatMonth = repeatWeek * 4;
                    txtMealRepeatInMonth.Text = repeatMonth.ToString();
                    int repeatYear = repeatMonth * 12;
                    txtMealRepeatInYear.Text = repeatYear.ToString();
                }
            }
        }

        protected void txtMealRepeatInMonth_TextChanged(object sender, EventArgs e)
        {
            if (txtMealRepeatInMonth.Text != "")
            {
                int repeatMonth = Convert.ToInt32(txtMealRepeatInMonth.Text);
                if (repeatMonth != 0)
                {
                    int repeatYear = repeatMonth * 12;
                    txtMealRepeatInYear.Text = repeatYear.ToString();
                }
            }
        }

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{
        //    int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.planmealsetups.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview1, (DB.planmealsetups.Where(p => p.TenentID == TID).OrderBy(m => m.planid).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        //((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2);
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



    }
}