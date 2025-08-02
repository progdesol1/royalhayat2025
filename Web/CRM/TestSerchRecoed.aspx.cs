using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.CRM
{
    public partial class TestSerchRecoed : System.Web.UI.Page
    {
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        Database.CallEntities DB = new Database.CallEntities();
        List<Database.TBLCOMPANYSETUP> List = new List<Database.TBLCOMPANYSETUP>();
        int Status1 = 0;
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["ADMInPrevious"].ToString().Contains('?'))
            //{
            //    string[] linkurl = Session["ADMInPrevious"].ToString().Split('?');
            //    Session["ADMInPrevious"] = linkurl[0].ToString();
            //}
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                FillContractorID();
                if (Request.QueryString["ID"] != null)
                {
                    int ID = Convert.ToInt32(Request.QueryString["ID"]);
                    if (ID == 1)
                        List = DB.TBLCOMPANYSETUPs.Where(p => p.BUYER == true).ToList();
                    else if (ID == 2)
                        List = DB.TBLCOMPANYSETUPs.Where(p => p.SALER == true).ToList();
                    else if (ID == 3)
                        List = DB.TBLCOMPANYSETUPs.Where(p => p.SALER == true && p.BUYER == true).ToList();
                    ViewState["List"] = List;
                }
                BindTitleData();
            }
        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
        }
        public void SessionLoad()
        {
            string Ref = ((CRMMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
        }

        protected void btnfind_Click(object sender, EventArgs e)
        {
            Panel2.Visible = true;
            Panel2.Focus();
            //Panel1.Visible = true;
            //Panel1.Focus();
            string id1 = txtCustomerName.Text.Trim().ToString();
            string id2 = txtCustomer.Text.Trim().ToString();
            string id3 = txtCustomer2.Text.Trim().ToString();
            string id4 = txtPostalCode.Text.Trim().ToString();
            string id5 = txtZipCode.Text.Trim().ToString();
            string id6 = txtCity.Text.Trim().ToString();
            string id7 = txtAddress.Text.Trim().ToString();
            string id8 = txtAddress2.Text.Trim().ToString();
            string id9 = tags_2.Text.Trim().ToString();
            string id10 = tags_3.Text.Trim().ToString();
            string id11 = tags_4.Text.Trim().ToString();
            //  string id18 = tags_1.Text.ToString();
            string id19 = txtWebsite.Text.Trim().ToString();
            string id20 = txtcUserName.Text.Trim().ToString();
            string id12 = txtMobileNo.Text.Trim().ToString();
            string id13 = txtRemark.Text.Trim().ToString();
            //  int id15 = Convert.ToInt32(drpType.SelectedValue);
            //  string id14 = drpCountry.SelectedValue;
            string CID1 = drpCountry12.SelectedValue;
            int cid = Convert.ToInt32(CID1);
          //  int id16 = Convert.ToInt32(drpState.SelectedValue);
            string SID = drpSates.SelectedValue.ToString();
            int id17 = Convert.ToInt32(drpMyProductId.SelectedValue);

            int PLID = Convert.ToInt32(drpPrimaryLang.SelectedValue);
            string id21 = PLID.ToString();
            bool id22 = chbIsMinistry.Checked ? true : false;
            bool id23 = chbBuyer.Checked ? true : false;
            bool id24 = chbEmailSub.Checked ? true : false;
            bool id25 = chbInHawally.Checked ? true : false;
            bool id26 = chbIsCorporate.Checked ? true : false;
            bool id28 = chbIssMb.Checked ? true : false;
            bool id29 = chbSaleDeProd.Checked ? true : false;
            bool id30 = chbSaler.Checked ? true : false;
            //if (ViewState["List"] != null)
            //{
            //    List = (List<Database.TBLCOMPANYSETUP>)ViewState["List"];
            //    Status1 = 1;
            //}
            List = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.STATE!=null).ToList();
            Status1 = 1;
            if (id1 != null && id1 != "")
            {
                if (Status1 ==0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.COMPNAME1.ToUpper().Contains(id1.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.COMPNAME1.ToUpper().Contains(id1.ToUpper())).ToList();
                    Status1 = 1;

                }
            }
            else
            {
                
            }
            if (id2 != "" && id2 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.COMPNAME2.ToUpper().Contains(id2.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.COMPNAME2.ToUpper().Contains(id2.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id3 != "" && id3 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.COMPNAME3.ToUpper().Contains(id3.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.COMPNAME3.ToUpper().Contains(id3.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id4 != "" && id4 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.POSTALCODE.ToUpper().Contains(id4.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.POSTALCODE.ToUpper().Contains(id4.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id5 != "" && id5 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.ZIPCODE.ToUpper().Contains(id5.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.ZIPCODE.ToUpper().Contains(id5.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id6 != "" && id6 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.CITY.ToUpper().Contains(id6.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.CITY.ToUpper().Contains(id6.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id7 != "" && id7 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.ADDR1.ToUpper().Contains(id7.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.ADDR1.ToUpper().Contains(id7.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id8 != "" && id8 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.ADDR2.ToUpper().Contains(id8.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.ADDR2.ToUpper().Contains(id8.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id9 != "" && id9 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.EMAIL1.ToUpper().Contains(id9.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.EMAIL1.ToUpper().Contains(id9.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id10 != "" && id10 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.FAX.ToUpper().Contains(id10.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.FAX.ToUpper().Contains(id10.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id11 != "" && id11 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.BUSPHONE1.ToUpper().Contains(id11.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.BUSPHONE1.ToUpper().Contains(id11.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id12 != "" && id12 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.MOBPHONE.ToUpper().Contains(id12.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.MOBPHONE.ToUpper().Contains(id12.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id13 != "" && id13 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.REMARKS.ToUpper().Contains(id13.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.REMARKS.ToUpper().Contains(id13.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (cid != 0 && cid != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.COUNTRYID == cid ).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.COUNTRYID == cid).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            //if (id15 != 0 && id15 != null)
            //{
            //    if (Status1 == 0)
            //    {
            //        List = DB.TBLCOMPANYSETUP.Where(p => p.Companytype==id15).ToList();
            //        Status1 = 1;
            //    }
            //    else
            //    {
            //        List = List.Where(p => p.Companytype == id15).ToList();
            //        Status1 = 1;
            //    }
            //}
            //else
            //{

            //}
            if (SID != "" && SID != null && SID != "0")
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.STATE.ToUpper().Contains(SID.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {

                    List = List.Where(p => p.STATE.ToUpper().Contains(SID.Trim().ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id17 != 0 && id17 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.MYPRODID == id17).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.MYPRODID == id17).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id21 != "" && id21 != null && id21 != "0")
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.PRIMLANGUGE.ToUpper().Contains(id21.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.PRIMLANGUGE.ToUpper().Contains(id21.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id19 != "" && id19 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.WEBPAGE.ToUpper().Contains(id19.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.WEBPAGE.ToUpper().Contains(id19.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id20 != "" && id20 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.USERID.ToUpper().Contains(id20.ToUpper())).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.USERID.ToUpper().Contains(id20.ToUpper())).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id22 != false && id22 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.ISMINISTRY == id22).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.ISMINISTRY == id22).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id23 != false && id23 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.BUYER == id23).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.BUYER == id23).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id24 != false && id24 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.EMAISUB == id24).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.EMAISUB == id24).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id25 != false && id25 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.INHAWALLY == id25).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.INHAWALLY == id25).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id26 != false && id26 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.ISCORPORATE == id26).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.ISCORPORATE == id26).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id28 != false && id28 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.ISSMB == id28).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.ISSMB == id28).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id29 != false && id29 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.SALEDEPROD == id29).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.SALEDEPROD == id29).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            if (id30 != false && id30 != null)
            {
                if (Status1 == 0)
                {
                    List = DB.TBLCOMPANYSETUPs.Where(p => p.SALER == id30).ToList();
                    Status1 = 1;
                }
                else
                {
                    List = List.Where(p => p.SALER == id30).ToList();
                    Status1 = 1;
                }
            }
            else
            {

            }
            //string id15 = drpItManager.SelectedValue;
            //string id16 = drpMyCounLocID.SelectedValue;
            //string id17 = drpCountry.SelectedValue;
            //List<Database.TBLCOMPANYSETUP> list = DB.TBLCOMPANYSETUP.Where(l =>
            //     (l.COMPNAME == txtCustomerName.Text) || (l.COMPNAMEO == txtCustomer.Text) || (l.COMPNAMECH == txtCustomer2.Text) || (l.POSTALCODE == id4) ||
            //     (l.ZIPCODE == id5) || (l.CITY == txtCity.Text) || (l.ADDR1 == id7) || (l.ADDR2 == id8) || (l.EMAIL == id9) || (l.Fax1 == id10) || (l.BUSPHONE1 == id11) ||
            //    (l.MOBPHONE == id12) || (l.remarks == id13) || (l.MYCONLOCID == cid) || (l.STATE == SID) || (l.MYPRODID == id17)
            //    || (l.WEBPAGE == id12)  || (l.ISMINISTRY == id22) || (l.BUYER  == id23) ||
            //    (l.EMAISUB  == id24) || (l.INHAWALLY == id25) || (l.ISCORPORATE == id26) || (l.ISSMB == id28) || (l.SALEDEPROD == id29) || (l.SALER == id30) || (l.PRIMLANGUGE == id21)).ToList();

            GridList.DataSource = List;
            GridList.DataBind();
            lblcount.Text = List.Count().ToString();
            ViewState["SaveList"] = List;
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView3, Totalrec, List);
                 
            //Listview1.DataSource = List;
            //Listview1.DataBind();
          //  UpdatePanel1.Update();
           
        }
        protected void GridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridList.PageIndex = e.NewPageIndex;
            var Listserch = ((List<TBLCOMPANYSETUP>)ViewState["SaveList"]).ToList();

            GridList.DataSource = Listserch;
            GridList.DataBind();
        }

        public void clen()
        {
            txtAddress.Text = txtAddress2.Text = txtCity.Text = txtcPassword.Text = txtcUserName.Text = txtCustomer.Text = txtCustomer2.Text = txtCustomerName.Text = txtMobileNo.Text = txtPostalCode.Text = txtRemark.Text = txtWebsite.Text = txtZipCode.Text = tags_2.Text = tags_3.Text = tags_4.Text = "";
            //drpCategory.SelectedIndex = 0;

            drpCountry12.SelectedIndex = 0;


            drpMyProductId.SelectedIndex = 0;
            drpPrimaryLang.SelectedIndex = 0;
          //  drpState.SelectedIndex = 0;

        }

        
        public void FillContractorID()
        {
            Classes.EcommAdminClass.getdropdown(drpCountry12, TID, "", "", "", "tblCOUNTRY");
            //select * from tblCOUNTRY where Active= 'Y' 
           
            //drpCountry12.DataSource = DB.tblCOUNTRies.Where(p => p.TenentID == TID && p.Active == "Y");
            //drpCountry12.DataTextField = "COUNAME1";
            //drpCountry12.DataValueField = "COUNTRYID";
            //drpCountry12.DataBind();
            //drpCountry12.Items.Insert(0, new ListItem("-- Select --", "0"));

           // drpState.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpPrimaryLang, TID, "", "", "", "tblLanguage");
            //select * from tblLanguage where ACTIVE= 'Y'

            //drpPrimaryLang.DataSource = DB.tblLanguages.Where(P => P.TenentID == TID && P.ACTIVE == "Y");
            //drpPrimaryLang.DataTextField = "LangName1";
            //drpPrimaryLang.DataValueField = "MYCONLOCID";
            //drpPrimaryLang.DataBind();
            //drpPrimaryLang.Items.Insert(0, new ListItem("-- Select --", "0"));

            Classes.EcommAdminClass.getdropdown(drpMyProductId, TID, "", "", "", "TBLPRODUCT");
            //select * from TBLPRODUCT where ACTIVE= 1 and TenentID = 1

            //drpMyProductId.DataSource = DB.TBLPRODUCTs.Where(P => P.TenentID == TID && P.ACTIVE == "Y");
            //drpMyProductId.DataTextField = "ProdName1";
            //drpMyProductId.DataValueField = "MYPRODID";
            //drpMyProductId.DataBind();
            //drpMyProductId.Items.Insert(0, new ListItem("-- Select --", "0"));
            drpSort.Items.Clear();
            drpSort.Items.Insert(0, new ListItem("---Sorting By---", "0"));
            drpSort.Items.Insert(1, new ListItem(Label1.Text, "1"));
            drpSort.Items.Insert(2, new ListItem(Label15.Text, "2"));
            drpSort.Items.Insert(2, new ListItem(Label16.Text, "3"));
            //drpSort.Items.Insert(3, new ListItem(Label19.Text, "4"));
            //drpSort.Items.Insert(4, new ListItem(Label20.Text, "5"));
           

        }
        List<TBLCOMPANYSETUP> listsestionValue = new List<TBLCOMPANYSETUP>();
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
           
        }

        public void BindTitleData()
        {
            //string REFtype = Convert.ToString(SearchManagement.SearchType.Search);
            //string REFSubtype = Convert.ToString(SearchManagement.SearchType.Company);
            //DrpTitle.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == REFtype && P.REFSUBTYPE == REFSubtype && P.TenentID == TID && P.ACTIVE == "Y");
            //DrpTitle.DataTextField = "REFNAME1";
            //DrpTitle.DataValueField = "REFID";
            //DrpTitle.DataBind();
            //DrpTitle.Items.Insert(0, new ListItem("-- Select --", "0"));


            var TitleData = (from TitleRef in DB.REFTABLEs
                             join Search in DB.ISSearchDetails on TitleRef.REFID equals Search.REFID
                             where Search.CreatedBy == UID && TitleRef.REFTYPE == "Search" && TitleRef.REFSUBTYPE == "Company" && TitleRef.TenentID == TID
                             select new
                             {
                                 ID = TitleRef.REFID,
                                 TitleRef.REFNAME1
                             }).ToList().Distinct();

            DrpTitle.DataSource = TitleData;
            DrpTitle.DataTextField = "REFNAME1";
            DrpTitle.DataValueField = "ID";
            DrpTitle.DataBind();
            DrpTitle.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(DrpTitle.SelectedValue) != 0)
            {
                int TitleID = Convert.ToInt32(DrpTitle.SelectedValue);
                List<ISSearchDetail> Search_List = DB.ISSearchDetails.Where(p => p.REFID == TitleID && p.CreatedBy == UID && p.TenentID == TID).ToList();
                List<Database.TBLCOMPANYSETUP> Con_List = new List<Database.TBLCOMPANYSETUP>();
                //TBLCOMPANYSETUP.Where(p => p.TenentID == TID && p.Approved == 0);

                foreach (ISSearchDetail item in Search_List)
                {
                    Database.TBLCOMPANYSETUP obj_Contact = DB.TBLCOMPANYSETUPs.Single(p => p.COMPID == item.CompanyID&&p.TenentID==TID);
                    Con_List.Add(obj_Contact);
                }
                var List = Con_List.ToList();
                ViewState["SaveList"] = List;
                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = List.Count();
                ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView3, Totalrec, List);
                 
                //Listview1.DataSource = List;
                //Listview1.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Show Found...');", true);
            }
        }
        protected void btnSearchSave_Click(object sender, EventArgs e)
        {
            List<TBLCOMPANYSETUP> List_MainOption1 = new List<TBLCOMPANYSETUP>();
            string RefType = Convert.ToString(SearchManagement.SearchType.Search);
            string RefSubType = Convert.ToString(SearchManagement.SearchType.Company.ToString());

            if (Convert.ToInt32(DrpTitle.SelectedValue) == 0)
            {
                if (Listview1.Items.Count() > 0)
                {
                    if (txtTitle.Text != "")
                    {
                        List<Database.REFTABLE> List_Ref = DB.REFTABLEs.ToList();
                        if (List_Ref.Where(p => p.REFNAME1 == txtTitle.Text && p.REFTYPE == RefType && p.REFSUBTYPE == RefSubType && p.TenentID == TID).Count() < 1)
                        {
                            int RID = List_Ref.Count() > 0 ? Convert.ToInt32(List_Ref.Max(p => p.REFID) + 1) : 1;
                            int Con = 0;
                            bool flag = false;
                            if (ViewState["CurrentTable"] != null)
                                List_MainOption1 = ((List<TBLCOMPANYSETUP>)ViewState["CurrentTable"]).ToList();
                            else
                                List_MainOption1 = ((List<TBLCOMPANYSETUP>)ViewState["SaveList"]).ToList();
                            for (int i = 0; i < List_MainOption1.Count(); i++)
                            {
                                int HID = Convert.ToInt32(List_MainOption1[i].COMPID);
                                //Label HID = (Label)Listview1.Items[i].FindControl("lblcompniid");
                               // CheckBox cbSelect = (CheckBox)Listview1.Items[i].FindControl("cbSelect");
                                Con = Convert.ToInt32(HID);
                                //if(cbSelect.Checked==true)
                                //{
                                    Database.ISSearchDetail obj_Detail = new ISSearchDetail();
                                    obj_Detail.ID = DB.ISSearchDetails.Count() > 0 ? Convert.ToInt32(DB.ISSearchDetails.Max(p => p.ID) + 1) : 1;
                                    obj_Detail.TenentID = TID;
                                    obj_Detail.LocationID = 1;
                                    obj_Detail.REFID = RID;
                                    obj_Detail.CompanyID = Con;
                                    obj_Detail.CreatedBy = UID;
                                    obj_Detail.Active = true;
                                    obj_Detail.Deleted = true;
                                    if (DB.ISSearchDetails.Where(p => p.CompanyID == Con && p.CreatedBy == UID && p.REFID == RID).Count() < 1)
                                    {
                                        DB.ISSearchDetails.AddObject(obj_Detail);
                                        DB.SaveChanges();
                                        flag = true;
                                    }
                                   
                               // }
                                
                            }
                           
                            if (flag == true)
                            {
                                Database.REFTABLE obj_Ref = new Database.REFTABLE();
                                obj_Ref.TenentID = TID;
                                obj_Ref.REFID = RID;
                                obj_Ref.REFTYPE = RefType;
                                obj_Ref.REFSUBTYPE = RefSubType;
                                obj_Ref.SHORTNAME = RefType;
                                obj_Ref.REFNAME1 = txtTitle.Text;
                                obj_Ref.REFNAME2 = txtTitle.Text;
                                obj_Ref.REFNAME3 = txtTitle.Text;
                                obj_Ref.ACTIVE = "Y";
                                DB.REFTABLEs.AddObject(obj_Ref);
                                DB.SaveChanges();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Data Save Sucessfully...');", true);
                            }

                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Title Name Already Exist...');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Must Enter Title...');", true);

                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('No Search Found...');", true);
                }
            }
            BindTitleData();
            txtTitle.Text = "";
            UpdatePanel1.Update();
        }
        protected void ckball_CheckedChanged(object sender, EventArgs e)
        {
            List<TBLCOMPANYSETUP> List = ((List<TBLCOMPANYSETUP>)ViewState["SaveList"]).ToList();
            if(ckball.Checked==true)
            {
                ViewState["AllChecked"] = true;
                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = List.Count();
                ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView3, Totalrec, List);
               
                //for (int i = 0; i < List.Count; i++)
                //{
                //    CheckBox cbSelect = (CheckBox)Listview1.Items[i].FindControl("cbSelect");
                //    string Active = List[i].Active;
                //    cbSelect.Checked = true;
                //}
            }
            else
            {
                ViewState["AllChecked"] = false;
                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = List.Count();
                ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView3, Totalrec, List);
               
                //for (int i = 0; i < List.Count; i++)
                //{
                //    CheckBox cbSelect = (CheckBox)Listview1.Items[i].FindControl("cbSelect");
                //    cbSelect.Checked = false;
                //}
            }
        }

        protected void btnAppend_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(DrpTitle.SelectedValue);
            int Con = 0;
            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                Label lblcompniid = (Label)Listview1.Items[i].FindControl("lblcompniid");
                Con = Convert.ToInt32(lblcompniid.Text);
                Database.ISSearchDetail obj_Detail = new ISSearchDetail();
                obj_Detail.ID = DB.ISSearchDetails.Count() > 0 ? Convert.ToInt32(DB.ISSearchDetails.Max(p => p.ID) + 1) : 1;
                obj_Detail.TenentID = TID;
                obj_Detail.LocationID = 1;
                obj_Detail.REFID = ID;
                obj_Detail.CompanyID = Con;
                obj_Detail.CreatedBy = UID;
                obj_Detail.Active = true;
                obj_Detail.Deleted = true;
                if (DB.ISSearchDetails.Where(p => p.CompanyID == Con && p.CreatedBy == UID && p.REFID == ID).Count() < 1)
                {
                    DB.ISSearchDetails.AddObject(obj_Detail);
                    DB.SaveChanges();
                  //  flag = true;
                }

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Append Successfully...');", true);
        }
       
        protected void Button2_Click(object sender, EventArgs e)
        {
            Listview1.DataSource = null;
            Listview1.DataBind();
            Panel2.Visible = true;
        }

        protected void btnselectexit_Click(object sender, EventArgs e)
        {
            Session["SerchList"] = null;
            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                CheckBox cbSelect = (CheckBox)Listview1.Items[i].FindControl("cbSelect");
                Label lblCustomerName = (Label)Listview1.Items[i].FindControl("lblCustomerName");
                Label lblEMAIL = (Label)Listview1.Items[i].FindControl("lblEMAIL");
                Label lblAddress = (Label)Listview1.Items[i].FindControl("lblAddress");
                Label lblCITY = (Label)Listview1.Items[i].FindControl("lblCITY");
                Label lblMOBPHONE = (Label)Listview1.Items[i].FindControl("lblMOBPHONE");
                Label lblZIPCODE = (Label)Listview1.Items[i].FindControl("lblZIPCODE");
                Label lblREMARKS = (Label)Listview1.Items[i].FindControl("lblREMARKS");
                Label lblSTATE = (Label)Listview1.Items[i].FindControl("lblSTATE");
                Label lblcompniid = (Label)Listview1.Items[i].FindControl("lblcompniid");
                if (cbSelect.Checked == true)
                {
                    TBLCOMPANYSETUP obj = new TBLCOMPANYSETUP();
                    obj.COMPID = Convert.ToInt32(lblcompniid.Text);
                    obj.COMPNAME1 = lblCustomerName.Text;
                    obj.EMAIL1 = lblEMAIL.Text;
                    obj.ADDR1 = lblAddress.Text;
                    obj.CITY = lblCITY.Text;
                    obj.ZIPCODE = lblZIPCODE.Text;
                    obj.MOBPHONE = lblMOBPHONE.Text;
                    obj.REMARKS = lblREMARKS.Text;
                    obj.STATE = lblSTATE.Text;
                    listsestionValue.Add(obj);
                }
            }
            Session["SerchList"] = listsestionValue;
            string URL = Session["ADMInPrevious"].ToString();
            Response.Redirect(URL + "?Sesstion=SerchList");
        }

        protected void drpCountry12_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CID = drpCountry12.SelectedValue;
            bindSates(CID);
        }
        public void bindSates(string CID)
        {
            Classes.EcommAdminClass.getdropdown(drpSates, TID, CID, "", "", "tblStates");
            //select * from tblStates where Active= 'Y'

            //drpSates.DataSource = DB.tblStates.Where(P => P.COUNTRYID == CID);
            //drpSates.DataTextField = "MYNAME1";
            //drpSates.DataValueField = "StateID";
            //drpSates.DataBind();
            //drpSates.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        protected void drpShowGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  BindData();
        }
        protected void drpSort_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<TBLCOMPANYSETUP> List = ((List<TBLCOMPANYSETUP>)ViewState["SaveList"]).ToList();
            if (drpSort.SelectedValue == "1")
                List = List.OrderBy(m => m.COMPNAME1).Where(p => p.Active == "Y").ToList();
            if (drpSort.SelectedValue == "2")
                List = List.OrderBy(m => m.ADDR1).Where(p => p.Active == "Y").ToList();
            if (drpSort.SelectedValue == "3")
                List = List.OrderBy(m => m.EMAIL).Where(p => p.Active == "Y").ToList();
            if (drpSort.SelectedValue == "4")
                List = List.OrderBy(m => m.MOBPHONE).Where(p => p.Active == "Y").ToList();
            if (drpSort.SelectedValue == "5")
                List = List.OrderBy(m => m.STATE).Where(p => p.Active == "Y").ToList();
            if (drpSort.SelectedValue == "6")
                List = List.OrderBy(m => m.ZIPCODE).Where(p => p.Active == "Y").ToList();
            if (drpSort.SelectedValue == "7")
                List = List.OrderBy(m => m.CITY).Where(p => p.Active == "Y").ToList();
            if (drpSort.SelectedValue == "8")
                List = List.OrderBy(m => m.REMARKS).Where(p => p.Active == "Y").ToList();
            int COMPID = List[0].COMPID;
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView3, Totalrec, List);
            //BindEditcompniy(COMPID);
            //redonlyfalse();
        }
        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            string id1 = txtSearch.Text;
            var Listserch =((List<TBLCOMPANYSETUP>) ViewState["SaveList"]).ToList();
            List<TBLCOMPANYSETUP> List = Listserch.Where(p => (p.COMPNAME1.ToUpper().Contains(id1.ToUpper()) || p.COMPNAME2.ToUpper().Contains(id1.ToUpper()) || p.POSTALCODE.ToUpper().Contains(id1.ToUpper()) || p.ZIPCODE.ToUpper().Contains(id1.ToUpper()) || p.CITY.ToUpper().Contains(id1.ToUpper()) || p.ADDR1.ToUpper().Contains(id1.ToUpper()) || p.ADDR2.ToUpper().Contains(id1.ToUpper()) || p.MOBPHONE.ToUpper().Contains(id1.ToUpper()) || p.BUSPHONE1.ToUpper().Contains(id1.ToUpper()) || p.REMARKS.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper())) && p.Active == "Y").OrderBy(p => p.COMPID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView3, Totalrec, List);
        }
        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

           

        }
        protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
            ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
            control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        }
        protected void btnfirst1_Click(object sender, EventArgs e)
        {
          
        }
        protected void btnLast1_Click(object sender, EventArgs e)
        {
            var Listserch = ((List<TBLCOMPANYSETUP>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = Listserch.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((CRMMaster)Page.Master).BindList(Listview1, (Listserch.OrderBy(m => m.COMPID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((CRMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView3, Totalrec);
            lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        }
        protected void btnNext1_Click(object sender, EventArgs e)
        {

            

        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {
            var Listserch = ((List<TBLCOMPANYSETUP>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = Listserch.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((CRMMaster)Page.Master).BindList(Listview1, (Listserch.OrderBy(m => m.COMPID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                if (take == Showdata && Skip == 0)
                    btnPrevious1.Enabled = false;
                else
                    btnPrevious1.Enabled = true;

                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;

                ChoiceID = take / Showdata;
                ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";
            }
        }

        protected void ListView3_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var Listserch = ((List<TBLCOMPANYSETUP>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = Listserch.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((CRMMaster)Page.Master).BindList(Listview1, (Listserch.OrderBy(m => m.COMPID).Take(Tvalue).Skip(Svalue)).ToList());
                ChoiceID = ID;
                ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
                if (Tvalue == Showdata && Svalue == 0)
                    btnPrevious1.Enabled = false;
                else
                    btnPrevious1.Enabled = true;
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
            }
            lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";

        }

        protected void btnfirst1_Click1(object sender, EventArgs e)
        {
            var Listserch = ((List<TBLCOMPANYSETUP>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = Listserch.Count();
                take = Showdata;
                Skip = 0;
                ((CRMMaster)Page.Master).BindList(Listview1, (Listserch.OrderBy(m => m.COMPID).Take(take).Skip(Skip)).ToList());
                ViewState["Take"] = take;
                ViewState["Skip"] = Skip;
                btnPrevious1.Enabled = false;
                ChoiceID = 0;
                ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
                if (take == Totalrec && Skip == (Totalrec - Showdata))
                    btnNext1.Enabled = false;
                else
                    btnNext1.Enabled = true;
                lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

            }
        }

        protected void btnNext1_Click1(object sender, EventArgs e)
        {
            var Listserch = ((List<TBLCOMPANYSETUP>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = Listserch.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((CRMMaster)Page.Master).BindList(Listview1, (Listserch.OrderBy(m => m.COMPID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            if (take == Totalrec && Skip == (Totalrec - Showdata))
                btnNext1.Enabled = false;
            else
                btnNext1.Enabled = true;
            if (take == Showdata && Skip == 0)
                btnPrevious1.Enabled = false;
            else
                btnPrevious1.Enabled = true;

            ChoiceID = take / Showdata;

            ((CRMMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView3, Totalrec);
            lblShowinfEntry.Text = "Showing " + Skip + " to " + take + " of " + Totalrec + " entries";

        }

        protected void cbSelect_CheckedChanged(object sender, EventArgs e)
        {
            for(int i=0;i<Listview1.Items.Count();i++)
            {
                CheckBox cbSelect = (CheckBox)Listview1.Items[i].FindControl("cbSelect");
                if(cbSelect.Checked==false)
                {
                    Label lblcompniid = (Label)Listview1.Items[i].FindControl("lblcompniid");
                    int CID = Convert.ToInt32(lblcompniid.Text);
                    List<TBLCOMPANYSETUP> List_MainOption1 = ((List<TBLCOMPANYSETUP>)ViewState["SaveList"]).ToList();
                    Database.TBLCOMPANYSETUP Obj_Option = List_MainOption1.Single(p => p.COMPID == CID);
                    List_MainOption1.Remove(Obj_Option);
                    Session["SaveList"] = List_MainOption1;
                }
            }
        }

        protected void Listview1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if(ViewState["AllChecked"]!=null)
            {
                CheckBox cbSelect = (CheckBox)e.Item.FindControl("cbSelect");
                bool Active = Convert.ToBoolean(ViewState["AllChecked"]);
                if (Active == true)
                     
                     cbSelect.Checked = true;
                else
                    cbSelect.Checked = false;
            }
           
        }
    }
}