using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.CRM
{
    public partial class ContactSearch : System.Web.UI.Page
    {
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        Database.CallEntities DB = new Database.CallEntities();
        int Status1 = 0;
        List<Database.TBLCONTACT> List = new List<Database.TBLCONTACT>();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (Session["ADMInPrevious"] != null)
            {
                if (Session["ADMInPrevious"].ToString().Contains('?'))
                {
                    string[] linkurl = Session["ADMInPrevious"].ToString().Split('?');
                    Session["ADMInPrevious"] = linkurl[0].ToString();
                }
            }
            if (!IsPostBack)
            {
                FistTimeLoad();
                FillContractorID();
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
        public void FillContractorID()
        {
            Classes.EcommAdminClass.getdropdown(drpCountry12, TID, "", "", "", "tblCOUNTRY");
            drpSort.Items.Clear();
            drpSort.Items.Insert(0, new ListItem("---Sorting By---", "0"));
            drpSort.Items.Insert(1, new ListItem(Label78.Text, "1"));
            drpSort.Items.Insert(2, new ListItem(Label79.Text, "2"));
            drpSort.Items.Insert(2, new ListItem(Label80.Text, "3"));
            drpSort.Items.Insert(3, new ListItem(Label81.Text, "4"));
            drpSort.Items.Insert(4, new ListItem(Label82.Text, "5"));
            drpSort.Items.Insert(2, new ListItem(Label83.Text, "6"));
            drpSort.Items.Insert(3, new ListItem(Label84.Text, "7"));
            drpSort.Items.Insert(4, new ListItem(Label85.Text, "8"));
            //select * from tblCOUNTRY where Active= 'Y' 

            //drpCountry12.DataSource = DB.tblCOUNTRies.Where(p => p.Active == "Y");
            //drpCountry12.DataTextField = "COUNAME1";
            //drpCountry12.DataValueField = "COUNTRYID";
            //drpCountry12.DataBind();
            //drpCountry12.Items.Insert(0, new ListItem("-- Select --", "0"));

            //drpMyCounLocID.DataSource = DB.TBLCOMPANY_LOCATION.Where(P => P.TenentID == TID && P.ACTIVE == "Y");
            //drpMyCounLocID.DataTextField = "LOCATION_NAME1";
            //drpMyCounLocID.DataValueField = "LOCATIONID";
            //drpMyCounLocID.DataBind();
            //drpMyCounLocID.Items.Insert(0, new ListItem("-- Select --", "0"));

            //string CNAME = drpCountry.SelectedItem.ToString();
            //int CID = DB.CRM_tblCOUNTRY.Single(p => p.ISO3166_1_2LetterCode == drpCountry.SelectedValue && p.COUNAME1 == CNAME).COUNTRYID;

            //drpState.DataSource = DB.CRM_tblStates.Where(p => p.COUNTRYID == CID && p.ACTIVE == "Y");
            //drpState.DataTextField = "MYNAME1";
            //drpState.DataValueField = "StateID";
            //drpState.DataBind();

        }

        public List<Database.TBLCONTACT> getcontactList(List<Database.TBLCONTACT> List1, List<Database.TBLCONTACT> List2)
        {
            List<Database.TBLCONTACT> Listcon = List2;
            foreach (Database.TBLCONTACT listtemp in List1)
            {
                Listcon.Add(listtemp);

            }

           return Listcon;
        }
        protected void btnfind_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            string id1 = txtContactName.Text;
            string id2 = txtContact2.Text;
            string id3 = txtContact3.Text;
            string id4 = txtPostalCode.Text;
            string id5 = txtZipCode.Text;
            string id6 = txtCity.Text;
            string id7 = txtAddress.Text;
            string id8 = txtAddress2.Text;
            string id9 = tags_2.Text;
            string id10 = tags_3.Text;
            string id11 = tags_4.Text;
            string id12 = txtMobileNo.Text;
            string id13 = txtRemark.Text;
            string id14 = drpCountry12.SelectedValue;
            //   string CID1 = DB.CRM_tblCOUNTRY.Single(p => p.ISO3166_1_2LetterCode == id14).COUNTRYID.ToString();
            int cid = Convert.ToInt32(id14);
            // int id15 = Convert.ToInt32(txtstest.SelectedValue);
            string SID = txtstest.Text.ToString();
            //int LNID = Convert.ToInt32(drpMyCounLocID.SelectedValue);

            //string id15 = drpItManager.SelectedValue;
            //string id16 = drpMyCounLocID.SelectedValue;
            //string id17 = drpCountry.SelectedValue;
            List<Database.TBLCONTACT> List1 = new List<TBLCONTACT>();
            List<Database.TBLCONTACT> List2 = new List<TBLCONTACT>();
          

            List = DB.TBLCONTACTs.Where(p => p.TenentID == TID).ToList();
            Status1 = 1;
            if (id1 != null && id1 != "")
            {
                if (Status1 == 0)
                {
                    List1 = DB.TBLCONTACTs.Where(p => p.PersName1.ToUpper().Contains(id1.ToUpper()) && p.TenentID == TID).ToList();                    
                    List2 = getcontactList(List1, List2);                    
                    Status1 = 1;
                }
                else
                {

                    List1 = List.Where(p => p.PersName1.ToUpper().Contains(id1.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2); 
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
                    List1 = DB.TBLCONTACTs.Where(p => p.PersName2.ToUpper().Contains(id2.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2); 
                    Status1 = 1;
                }
                else
                {
                    List1 = List.Where(p => p.PersName2.ToUpper().Contains(id2.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2); 
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
                    List1 = DB.TBLCONTACTs.Where(p => p.PersName3.ToUpper().Contains(id3.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2); 
                    Status1 = 1;
                }
                else
                {
                    List1 = List.Where(p => p.PersName3.ToUpper().Contains(id3.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2); 
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
                    List1 = DB.TBLCONTACTs.Where(p => p.POSTALCODE.ToUpper().Contains(id4.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
                    Status1 = 1;
                }
                else
                {
                    List1 = List.Where(p => p.POSTALCODE.ToUpper().Contains(id4.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
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
                    List1 = DB.TBLCONTACTs.Where(p => p.ZIPCODE.ToUpper().Contains(id5.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
                    Status1 = 1;
                }
                else
                {
                    List1 = List.Where(p => p.ZIPCODE.ToUpper().Contains(id5.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
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
                    List1 = DB.TBLCONTACTs.Where(p => p.CITY.ToUpper().Contains(id6.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
                    Status1 = 1;
                }
                else
                {
                    List1 = List.Where(p => p.CITY.ToUpper().Contains(id6.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
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
                    List1 = DB.TBLCONTACTs.Where(p => p.ADDR1.ToUpper().Contains(id7.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
                    Status1 = 1;
                }
                else
                {
                    List1 = List.Where(p => p.ADDR1.ToUpper().Contains(id7.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
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
                    List1 = DB.TBLCONTACTs.Where(p => p.ADDR2.ToUpper().Contains(id8.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
                    Status1 = 1;
                }
                else
                {
                    List1 = List.Where(p => p.ADDR2.ToUpper().Contains(id8.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
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
                    List1 = DB.TBLCONTACTs.Where(p => p.EMAIL1.ToUpper().Contains(id9.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
                    Status1 = 1;
                }
                else
                {
                    List1 = List.Where(p => p.EMAIL1.ToUpper().Contains(id9.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
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
                    List1 = DB.TBLCONTACTs.Where(p => p.FaxID.ToUpper().Contains(id10.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
                    Status1 = 1;
                }
                else
                {
                    List1 = List.Where(p => p.FaxID.ToUpper().Contains(id10.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
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
                    List1 = DB.TBLCONTACTs.Where(p => p.BUSPHONE1.ToUpper().Contains(id11.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
                    Status1 = 1;
                }
                else
                {
                    List1 = List.Where(p => p.BUSPHONE1.ToUpper().Contains(id11.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
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
                    List1 = DB.TBLCONTACTs.Where(p => p.MOBPHONE.ToUpper().Contains(id12.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
                    Status1 = 1;
                }
                else
                {
                    List1 = List.Where(p => p.MOBPHONE.ToUpper().Contains(id12.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
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
                    List1 = DB.TBLCONTACTs.Where(p => p.REMARKS.ToUpper().Contains(id13.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
                    Status1 = 1;
                }
                else
                {
                    List1 = List.Where(p => p.REMARKS.ToUpper().Contains(id13.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2);
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
                    List1 = DB.TBLCONTACTs.Where(p => p.COUNTRYID == cid && p.STATE != null && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2); 
                    Status1 = 1;
                }
                else
                {
                    List1 = List.Where(p => p.COUNTRYID == cid && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2); 
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
            //        List = DB.TBLCONTACT.Where(p => p.Companytype==id15).ToList();
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
                    List1 = DB.TBLCONTACTs.Where(p => p.STATE.ToUpper().Contains(SID.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2); 
                    Status1 = 1;
                }
                else
                {
                    List1 = List.Where(p => p.STATE.ToUpper().Contains(SID.ToUpper()) && p.TenentID == TID).ToList();
                    List2 = getcontactList(List1, List2); 
                    Status1 = 1;
                }
            }
            else
            {

            }

            List2 = List2.GroupBy(p => p.ContactMyID).Select(p => p.FirstOrDefault()).ToList();


            ViewState["SaveList"] = List2;
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List2.Count();
            ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView3, Totalrec, List2);

            //Listview1.DataSource = ViewState["SaveList"];
            //Listview1.DataBind();






            //List<Database.TBLCONTACT> list = DB.TBLCONTACT.Where(l =>

            //   (l.PersName1 == id1) ||
            //   (l.PersName2 == id2) ||
            //   (l.PersName3 == id3) ||
            //   (l.POSTALCODE == id4) ||
            //   (l.ZIPCODE == id5) ||
            //   (l.CITY == id6) ||
            //   (l.ADDR1 == id7) ||
            //   (l.ADDR2 == id8) ||
            //   (l.COUNTRYID == cid) ||
            //   (l.MOBPHONE == id12) ||
            //   (l.STATE == SID) ||

            //   (l.REMARKS == id13)).ToList();

            //ViewState["SaveList"] = list;
            //Listview1.DataSource = ViewState["SaveList"];
            //Listview1.DataBind();
        }

        protected void chball_CheckedChanged(object sender, EventArgs e)
        {
            List<TBLCONTACT> List = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
            if (chball.Checked == true)
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
            //for (int i = 0; i < Listview1.Items.Count(); i++)
            //{
            //    if (chball.Checked == true)
            //    {
            //        CheckBox checkesone = (CheckBox)Listview1.Items[i].FindControl("checkesone");
            //        checkesone.Checked = true;
            //    }
            //    else
            //    {
            //        CheckBox checkesone = (CheckBox)Listview1.Items[i].FindControl("checkesone");
            //        checkesone.Checked = false;
            //    }
            //}
        }
        //protected void drpCountry_SelectedIndexChanged1(object sender, EventArgs e)
        //{
        //    bindState();
        //}
        //public void bindState()
        //{
        //    string CNAME = drpCountry.SelectedItem.ToString();
        //    int CID = DB.CRM_tblCOUNTRY.Single(p => p.ISO3166_1_2LetterCode == drpCountry.SelectedValue && p.COUNAME1 == CNAME).COUNTRYID;

        //    drpState.DataSource = DB.CRM_tblStates.Where(p => p.COUNTRYID == CID && p.ACTIVE == "Y");
        //    drpState.DataTextField = "MYNAME1";
        //    drpState.DataValueField = "StateID";
        //    drpState.DataBind();

        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(DrpTitle.SelectedValue) != 0)
            {
                int TitleID = Convert.ToInt32(DrpTitle.SelectedValue);
                List<ISSearchDetail> Search_List = DB.ISSearchDetails.Where(p => p.REFID == TitleID  && p.TenentID == TID).ToList();
                List<Database.TBLCONTACT> Con_List = new List<Database.TBLCONTACT>();

                foreach (ISSearchDetail item in Search_List)
                {
                    Database.TBLCONTACT obj_Contact = DB.TBLCONTACTs.Single(p => p.ContactMyID == item.ContactID && p.TenentID == TID);
                    Con_List.Add(obj_Contact);
                }
                //Listview2.DataSource = Con_List;//DB.TBLCONTACT.Where(p => p.TenentID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
                //Listview2.DataBind();
                var List = Con_List;
                int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
                int Totalrec = List.Count();
                ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView3, Totalrec, List);

                //Listview1.DataSource = Con_List;
                //Listview1.DataBind();

            }
        }
        protected void btnSearchSave_Click(object sender, EventArgs e)
        {
            List<TBLCONTACT> List_MainOption1 = new List<TBLCONTACT>();
            string RefType = Convert.ToString(SearchManagement.SearchType.Search);
            string RefSubType = Convert.ToString(SearchManagement.SearchType.Contact.ToString());

            if (Convert.ToInt32(DrpTitle.SelectedValue) == 0)
            {
                if (Listview1.Items.Count() > 0)
                {
                    if (txtTitle.Text != "")
                    {
                        List<Database.REFTABLE> List_Ref = DB.REFTABLEs.ToList();
                        if (List_Ref.Where(p => p.REFNAME1 == txtTitle.Text && p.REFTYPE == RefType && p.REFSUBTYPE == RefSubType && p.TenentID == TID).Count() < 1)
                        {
                            int RID = List_Ref.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(List_Ref.Where(p => p.TenentID == TID).Max(p => p.REFID) + 1) : 1;
                            int Con = 0;
                            bool flag = false;
                            if (ViewState["SaveList"] != null)
                                List_MainOption1 = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
                            // List<TBLCONTACT> Listview1 = ((List<TBLCONTACT>)ViewState["SerchListTabel"]).ToList();
                            for (int i = 0; i < List_MainOption1.Count(); i++)
                            {
                                int HID = Convert.ToInt32(List_MainOption1[i].ContactMyID);
                                //Label HID = (Label)Listview1.Items[i].FindControl("lblcompniid");
                                // CheckBox cbSelect = (CheckBox)Listview1.Items[i].FindControl("cbSelect");
                                Con = Convert.ToInt32(HID);
                                //if (checkesone.Checked == true)
                                //{
                                ISSearchDetail obj_Detail = new ISSearchDetail();
                                obj_Detail.ID = DB.ISSearchDetails.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ISSearchDetails.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                                obj_Detail.TenentID = TID;
                                obj_Detail.LocationID = 1;
                                obj_Detail.REFID = RID;
                                obj_Detail.ContactID = Con;
                                obj_Detail.CreatedBy = UID;
                                obj_Detail.Active = true;
                                obj_Detail.Deleted = true;
                                if (DB.ISSearchDetails.Where(p => p.ContactID == Con && p.CreatedBy == UID && p.REFID == RID && p.TenentID == TID).Count() < 1)
                                {
                                    DB.ISSearchDetails.AddObject(obj_Detail);
                                    DB.SaveChanges();
                                    flag = true;
                                }

                            }
                            //}

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
        }
        public void BindTitleData()
        {
            string REFtype = Convert.ToString(SearchManagement.SearchType.Search);
            string REFSubtype = Convert.ToString(SearchManagement.SearchType.Contact);
            DrpTitle.DataSource = DB.REFTABLEs.Where(P => P.REFTYPE == REFtype && P.REFSUBTYPE == REFSubtype && P.TenentID == TID && P.ACTIVE == "Y");
            DrpTitle.DataTextField = "REFNAME1";
            DrpTitle.DataValueField = "REFID";
            DrpTitle.DataBind();
            DrpTitle.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Listview1.DataSource = null;
            Listview1.DataBind();
        }
        List<TBLCONTACT> listsestionValue = new List<TBLCONTACT>();
        protected void btnselectexit_Click(object sender, EventArgs e)
        {
            Session["SerchListContact"] = null;
            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                CheckBox checkesone = (CheckBox)Listview1.Items[i].FindControl("checkesone");
                Label lblCustomerName = (Label)Listview1.Items[i].FindControl("lblCustomerName");
                Label lblEMAIL = (Label)Listview1.Items[i].FindControl("lblEMAIL");
                Label lblAddress = (Label)Listview1.Items[i].FindControl("lblAddress");
                Label lblCITY = (Label)Listview1.Items[i].FindControl("lblCITY");
                Label lblMOBPHONE = (Label)Listview1.Items[i].FindControl("lblMOBPHONE");
                Label lblZIPCODE = (Label)Listview1.Items[i].FindControl("lblZIPCODE");
                Label lblREMARKS = (Label)Listview1.Items[i].FindControl("lblREMARKS");
                Label lblSTATE = (Label)Listview1.Items[i].FindControl("lblSTATE");
                Label Label2 = (Label)Listview1.Items[i].FindControl("Label2");
                if (checkesone.Checked == true)
                {
                    TBLCONTACT obj = new TBLCONTACT();
                    obj.ContactMyID = Convert.ToInt32(Label2.Text);
                    obj.PersName1 = lblCustomerName.Text;
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
            Session["SerchListContact"] = listsestionValue;
            string URL = Session["ADMInPrevious"].ToString();
            Response.Redirect(URL + "?Sesstion=SerchList");
        }
        protected void btnAppend_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(DrpTitle.SelectedValue);
            int Con = 0;
            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                Label Label2 = (Label)Listview1.Items[i].FindControl("Label2");
                Con = Convert.ToInt32(Label2.Text);
                Database.ISSearchDetail obj_Detail = new ISSearchDetail();
                obj_Detail.ID = DB.ISSearchDetails.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ISSearchDetails.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
                obj_Detail.TenentID = TID;
                obj_Detail.LocationID = 1;
                obj_Detail.REFID = ID;
                obj_Detail.ContactID = Con;
                obj_Detail.CreatedBy = UID;
                obj_Detail.Active = true;
                obj_Detail.Deleted = true;
                if (DB.ISSearchDetails.Where(p => p.ContactID == Con && p.CreatedBy == UID && p.REFID == ID && p.TenentID == TID).Count() < 1)
                {
                    DB.ISSearchDetails.AddObject(obj_Detail);
                    DB.SaveChanges();
                }

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Append Successfully...');", true);
        }
        protected void drpShowGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  BindData();
        }
        protected void drpSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<TBLCONTACT> List = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
            if (drpSort.SelectedValue == "1")
                List = List.OrderBy(m => m.PersName1).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            if (drpSort.SelectedValue == "2")
                List = List.OrderBy(m => m.ADDR1).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            if (drpSort.SelectedValue == "3")
                List = List.OrderBy(m => m.EMAIL1).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            if (drpSort.SelectedValue == "4")
                List = List.OrderBy(m => m.MOBPHONE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            if (drpSort.SelectedValue == "5")
                List = List.OrderBy(m => m.STATE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            if (drpSort.SelectedValue == "6")
                List = List.OrderBy(m => m.ZIPCODE).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            if (drpSort.SelectedValue == "7")
                List = List.OrderBy(m => m.CITY).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            if (drpSort.SelectedValue == "8")
                List = List.OrderBy(m => m.REMARKS).Where(p => p.Active == "Y" && p.TenentID == TID).ToList();
            decimal COMPID = List[0].ContactMyID;
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView3, Totalrec, List);
            //BindEditcompniy(COMPID);
            //redonlyfalse();
        }
        protected void LinkButton10_Click(object sender, EventArgs e)
        { 
            string id1 = txtSearch.Text;
            var List1 = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
            List<TBLCONTACT> List = List1.Where(p => (p.PersName1.ToUpper().Contains(id1.ToUpper()) || p.PersName2.ToUpper().Contains(id1.ToUpper()) || p.POSTALCODE.ToUpper().Contains(id1.ToUpper()) || p.ZIPCODE.ToUpper().Contains(id1.ToUpper()) || p.CITY.ToUpper().Contains(id1.ToUpper()) || p.ADDR1.ToUpper().Contains(id1.ToUpper()) || p.ADDR2.ToUpper().Contains(id1.ToUpper()) || p.MOBPHONE.ToUpper().Contains(id1.ToUpper()) || p.BUSPHONE1.ToUpper().Contains(id1.ToUpper()) || p.REMARKS.ToUpper().Contains(id1.ToUpper()) || p.EMAIL1.ToUpper().Contains(id1.ToUpper())) && p.TenentID == TID && p.Active == "Y").OrderBy(p => p.ContactMyID).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            ((CRMMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview1, ListView3, Totalrec, List);
        }
        protected void btnfirst1_Click1(object sender, EventArgs e)
        {
            var List = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = List.Count();
                take = Showdata;
                Skip = 0;
                ((CRMMaster)Page.Master).BindList(Listview1, (List.OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
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
            var List = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            if (ViewState["Take"] == null && ViewState["Skip"] == null)
            {
                ViewState["Take"] = Showdata;
                ViewState["Skip"] = 0;
            }
            take = Convert.ToInt32(ViewState["Take"]);
            take = take + Showdata;
            Skip = take - Showdata;

            ((CRMMaster)Page.Master).BindList(Listview1, (List.OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
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
        protected void ListView3_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            var List = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            if (e.CommandName == "LinkPageavigation")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ViewState["Take"] = ID * Showdata;
                ViewState["Skip"] = (ID * Showdata) - Showdata;
                int Tvalue = Convert.ToInt32(ViewState["Take"]);
                int Svalue = Convert.ToInt32(ViewState["Skip"]);
                ((CRMMaster)Page.Master).BindList(Listview1, (List.OrderBy(m => m.ContactMyID).Take(Tvalue).Skip(Svalue)).ToList());
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
        protected void AnswerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lb = e.Item.FindControl("LinkPageavigation") as LinkButton;
            ScriptManager control = this.Master.FindControl("toolscriptmanagerID") as ScriptManager;
            control.RegisterAsyncPostBackControl(lb);  // ToolkitScriptManager
        }
        protected void btnPrevious1_Click(object sender, EventArgs e)
        {
            var List = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            if (ViewState["Take"] != null && ViewState["Skip"] != null)
            {
                int Totalrec = List.Count();
                Skip = Convert.ToInt32(ViewState["Skip"]);
                take = Skip;
                Skip = take - Showdata;
                ((CRMMaster)Page.Master).BindList(Listview1, (List.OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
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
        protected void btnLast1_Click(object sender, EventArgs e)
        {
            var List = ((List<TBLCONTACT>)ViewState["SaveList"]).ToList();
            int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            int Totalrec = List.Count();
            take = Totalrec;
            Skip = Totalrec - Showdata;
            ((CRMMaster)Page.Master).BindList(Listview1, (List.OrderBy(m => m.ContactMyID).Take(take).Skip(Skip)).ToList());
            ViewState["Take"] = take;
            ViewState["Skip"] = Skip;
            btnNext1.Enabled = false;
            btnPrevious1.Enabled = true;
            ChoiceID = take / Showdata;
            ((CRMMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView3, Totalrec);
            lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        }

        protected void Listview1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (ViewState["AllChecked"] != null)
            {
                CheckBox checkesone = (CheckBox)e.Item.FindControl("checkesone");
                bool Active = Convert.ToBoolean(ViewState["AllChecked"]);
                if (Active == true)

                    checkesone.Checked = true;
                else
                    checkesone.Checked = false;
            }
        }
    }
}

