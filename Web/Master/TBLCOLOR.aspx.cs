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
using System.Net;
using System.IO;

namespace Web.Master
{
    public partial class TBLCOLOR : System.Web.UI.Page
    {
        #region Step1
        int count = 0;
        int take = 0;
        int Skip = 0;
        public static int ChoiceID = 0;
        
        #endregion
        CallEntities DB = new CallEntities();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, CID, userID1, userTypeid = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                Session["LANGUAGE"] = "en-US";
                Readonly();
                ManageLang();
                //pnlSuccessMsg.Visible = false;
                FillContractorID();
                int CurrentID = 1;
                if (ViewState["Es"] != null)
                    CurrentID = Convert.ToInt32(ViewState["Es"]);
                BindData();
                //FirstData();
                btnAdd.ValidationGroup = "s";
            }
        }
        #region Step2
        public void BindData()
        {
            //List<Database.TBLCOLOR> List = DB.TBLCOLORs.Where(p => p.Active == "Y" && p.TenentID == TID).OrderBy(m => m.COLORID).ToList();
            Listview.DataSource = DB.TBLCOLORs.Where(p => p.Active == "Y" && p.TenentID == TID).OrderBy(m => m.COLORID).OrderByDescending(m=>m.COLORID);
            Listview.DataBind();
            //int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
            //int Totalrec = List.Count();
            //((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, lblShowinfEntry, btnPrevious1, btnNext1, Listview, ListView2, Totalrec, List);
        }
        #endregion
        public void FistTimeLoad()
        {
            FirstFlag = false;
        }
        public void SessionLoad()
        {
            string Ref = ((AcmMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
            userID1 = ((USER_MST)Session["USER"]).USER_ID;
            userTypeid = Convert.ToInt32(((USER_MST)Session["USER"]).USER_DETAIL_ID);

        }

        #region PAge Genarator
        public void GetShow()
        {

            lblCOLORDESC11s.Attributes["class"] = lblCOLORDESC21s.Attributes["class"] = lblCOLORREMARKS1s.Attributes["class"] = lblhex1s.Attributes["class"] = lblRGB1s.Attributes["class"] = lblcolor1s.Attributes["class"] = lblActive1s.Attributes["class"] = "control-label col-md-4  getshow";
            lblCOLORDESC12h.Attributes["class"] = lblCOLORDESC22h.Attributes["class"] = lblCOLORREMARKS2h.Attributes["class"] = lblhex2h.Attributes["class"] = lblRGB2h.Attributes["class"] = lblcolor2h.Attributes["class"] = lblActive2h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblCOLORDESC11s.Attributes["class"] = lblCOLORDESC21s.Attributes["class"] = lblCOLORREMARKS1s.Attributes["class"] = lblhex1s.Attributes["class"] = lblRGB1s.Attributes["class"] = lblcolor1s.Attributes["class"] = lblActive1s.Attributes["class"] = "control-label col-md-4  gethide";
            lblCOLORDESC12h.Attributes["class"] = lblCOLORDESC22h.Attributes["class"] = lblCOLORREMARKS2h.Attributes["class"] = lblhex2h.Attributes["class"] = lblRGB2h.Attributes["class"] = lblcolor2h.Attributes["class"] = lblActive2h.Attributes["class"] = "control-label col-md-4  getshow";
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
        #endregion

        public void Clear()
        {
            
            txtCOLORDESC1.Text = "";
            txtCOLORDESC2.Text = "";
            txtCOLORREMARKS.Text = "";
            txthex.Text = "";
            txtRGB.Text = "";
            txtcolor.Text = "";
            //drpCRUP_ID.SelectedIndex = 0;
            cbActive.Checked = true;

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                { 
                    if (btnAdd.Text == "Add New")
                    {

                        Write();
                        Clear();
                        btnAdd.Text = "Save";
                        btnAdd.ValidationGroup = "submit";
                    }
                    else if (btnAdd.Text == "Save")
                    {
                        if (DB.TBLCOLORs.Where(p => p.COLORDESC1.ToUpper() == txtCOLORDESC1.Text.ToUpper() && p.TenentID==TID).Count()<1)
                        {
                            Database.TBLCOLOR objTBLCOLOR = new Database.TBLCOLOR();
                            //Server Content Send data Yogesh
                            //objTBLCOLOR.COLORID = Convert.ToInt32(drpCOLORID.SelectedValue);
                            //int tenant = DB.TBLCOLORs.Max(p => p.TenentID);
                            objTBLCOLOR.TenentID = TID;
                            objTBLCOLOR.COLORID = DB.TBLCOLORs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.TBLCOLORs.Where(p => p.TenentID == TID).Max(p => p.COLORID) + 1) : 1;
                            objTBLCOLOR.COLORDESC1 = txtCOLORDESC1.Text;
                            objTBLCOLOR.COLORDESC2 = Translate(txtCOLORDESC2.Text, "ar");// txtCOLORDESC2.Text;
                            objTBLCOLOR.COLORREMARKS = txtCOLORREMARKS.Text;
                            objTBLCOLOR.hex = txthex.Text;
                            objTBLCOLOR.RGB = txtRGB.Text;
                            objTBLCOLOR.color = txtcolor.Text;
                            //objTBLCOLOR.CRUP_ID = Convert.ToInt64(drpCRUP_ID.SelectedValue);
                            objTBLCOLOR.Active = cbActive.Checked ? "Y" : "N";


                            DB.TBLCOLORs.AddObject(objTBLCOLOR);
                            DB.SaveChanges();
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                            BindData();
                        }
                        else
                        {
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Color Is All Ready Exist...", "Error!", Classes.Toastr.ToastPosition.TopCenter);
                        }
                        
                        Clear();
                        btnAdd.ValidationGroup = "s";
                         //lblMsg.Text = "  Data Save Successfully";
                        //pnlSuccessMsg.Visible = true;
                        //Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Save Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        //BindData();
                        //navigation.Visible = true;
                        btnAdd.Text = "Add New";
                        Readonly();
                        //FirstData();
                    }
                    else if (btnAdd.Text == "Update")
                    {

                        if (ViewState["TIDD"] != null && ViewState["TIDD"] != null)
                        {
                            //int ID = Convert.ToInt32(ViewState["Edit"]);
                            int tidd = Convert.ToInt32(ViewState["TIDD"]);
                            int cid = Convert.ToInt32(ViewState["CID"]);
                            Database.TBLCOLOR objTBLCOLOR = DB.TBLCOLORs.Single(p => p.COLORID == cid && p.TenentID == tidd);
                            //objTBLCOLOR.COLORID = Convert.ToInt32(drpCOLORID.SelectedValue);
                            objTBLCOLOR.COLORDESC1 = txtCOLORDESC1.Text;
                            objTBLCOLOR.COLORDESC2 = Translate(txtCOLORDESC2.Text, "ar"); //txtCOLORDESC2.Text;
                            objTBLCOLOR.COLORREMARKS = txtCOLORREMARKS.Text;
                            objTBLCOLOR.hex = txthex.Text;
                            objTBLCOLOR.RGB = txtRGB.Text;
                            objTBLCOLOR.color = txtcolor.Text;
                            //objTBLCOLOR.CRUP_ID = Convert.ToInt64(drpCRUP_ID.SelectedValue);
                            objTBLCOLOR.Active = cbActive.Checked ? "Y" : "N";

                            //ViewState["Edit"] = null;
                            ViewState["TIDD"] = null;
                            ViewState["CID"] = null;
                            btnAdd.Text = "Add New";
                            btnAdd.ValidationGroup = "s";
                            DB.SaveChanges();
                            Clear();
                            //lblMsg.Text = "  Data Edit Successfully";
                            //pnlSuccessMsg.Visible = true;
                            Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Edit Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                            BindData();
                            //navigation.Visible = true;
                            Readonly();
                            //FirstData();
                        }
                    }
                    BindData();

                    scope.Complete(); //  To commit.

                }
                catch (Exception ex)
                {
                     Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Error, "Error Occured During Saving Data !<br>" + ex.ToString(), "Add", Classes.Toastr.ToastPosition.TopCenter);
                    //throw;
                }
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
       
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect("TBLCOLOR.aspx?MID=y9s5iI3JheoJb0Gq8eAZew==");
            Response.Redirect(Session["Previous"].ToString());
        }
        public void FillContractorID()
        {
            //drpActive.Items.Insert(0, new ListItem("-- Select --", "0"));drpActive.DataSource = DB.0;drpActive.DataTextField = "0";drpActive.DataValueField = "0";drpActive.DataBind();
        }

        #region PAge Genarator navigation
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
            Listview.SelectedIndex = 0;
            //drpCOLORID.SelectedValue = Listview.SelectedDataKey[0].ToString();
            txtCOLORDESC1.Text = Listview.SelectedDataKey[2] != null ? Listview.SelectedDataKey[2].ToString(): "";
            txtCOLORDESC2.Text = Listview.SelectedDataKey[3]!=null? Listview.SelectedDataKey[3].ToString():"";
            txtCOLORREMARKS.Text = Listview.SelectedDataKey[4]!=null? Listview.SelectedDataKey[4].ToString():"";
            txthex.Text = Listview.SelectedDataKey[5]!=null? Listview.SelectedDataKey[5].ToString():"";
            txtRGB.Text = Listview.SelectedDataKey[6]!=null? Listview.SelectedDataKey[6].ToString():"";
            txtcolor.Text = Listview.SelectedDataKey[7]!=null? Listview.SelectedDataKey[7].ToString():"";
            //txtCRUP_ID.Text = Listview.SelectedDataKey[0].ToString();
            //txtActive.Text = Listview.SelectedDataKey[0].ToString();

        }
        public void NextData()
        {

            if (Listview.SelectedIndex != Listview.Items.Count - 1)
            {
                Listview.SelectedIndex = Listview.SelectedIndex + 1;
                //drpCOLORID.SelectedValue = Listview.SelectedDataKey[0].ToString();
                txtCOLORDESC1.Text = Listview.SelectedDataKey[2] != null ? Listview.SelectedDataKey[2].ToString() : "";
                txtCOLORDESC2.Text = Listview.SelectedDataKey[3] != null ? Listview.SelectedDataKey[3].ToString() : "";
                txtCOLORREMARKS.Text = Listview.SelectedDataKey[4] != null ? Listview.SelectedDataKey[4].ToString() : "";
                txthex.Text = Listview.SelectedDataKey[5] != null ? Listview.SelectedDataKey[5].ToString() : "";
                txtRGB.Text = Listview.SelectedDataKey[6] != null ? Listview.SelectedDataKey[6].ToString() : "";
                txtcolor.Text = Listview.SelectedDataKey[7] != null ? Listview.SelectedDataKey[7].ToString() : "";
                //txtCRUP_ID.Text = Listview.SelectedDataKey[0].ToString();
                //txtActive.Text = Listview.SelectedDataKey[0].ToString();

            }

        }
        public void PrevData()
        {
            if (Listview.SelectedIndex == 0)
            {
                //lblMsg.Text = "This is first record";
               // pnlSuccessMsg.Visible = true;

            }
            else
            {
                pnlSuccessMsg.Visible = false;
                Listview.SelectedIndex = Listview.SelectedIndex - 1;
                //drpCOLORID.SelectedValue = Listview.SelectedDataKey[0].ToString();
                txtCOLORDESC1.Text = Listview.SelectedDataKey[2] != null ? Listview.SelectedDataKey[2].ToString() : "";
                txtCOLORDESC2.Text = Listview.SelectedDataKey[3] != null ? Listview.SelectedDataKey[3].ToString() : "";
                txtCOLORREMARKS.Text = Listview.SelectedDataKey[4] != null ? Listview.SelectedDataKey[4].ToString() : "";
                txthex.Text = Listview.SelectedDataKey[5] != null ? Listview.SelectedDataKey[5].ToString() : "";
                txtRGB.Text = Listview.SelectedDataKey[6] != null ? Listview.SelectedDataKey[6].ToString() : "";
                txtcolor.Text = Listview.SelectedDataKey[7] != null ? Listview.SelectedDataKey[7].ToString() : "";
                //txtCRUP_ID.Text = Listview.SelectedDataKey[0].ToString();
                //txtActive.Text = Listview.SelectedDataKey[0].ToString();

            }
        }
        public void LastData()
        {
            Listview.SelectedIndex = Listview.Items.Count - 1;
            //drpCOLORID.SelectedValue = Listview.SelectedDataKey[0].ToString();
            txtCOLORDESC1.Text = Listview.SelectedDataKey[2] != null ? Listview.SelectedDataKey[2].ToString() : "";
            txtCOLORDESC2.Text = Listview.SelectedDataKey[3] != null ? Listview.SelectedDataKey[3].ToString() : "";
            txtCOLORREMARKS.Text = Listview.SelectedDataKey[4] != null ? Listview.SelectedDataKey[4].ToString() : "";
            txthex.Text = Listview.SelectedDataKey[5] != null ? Listview.SelectedDataKey[5].ToString() : "";
            txtRGB.Text = Listview.SelectedDataKey[6] != null ? Listview.SelectedDataKey[6].ToString() : "";
            txtcolor.Text = Listview.SelectedDataKey[7] != null ? Listview.SelectedDataKey[7].ToString() : "";
            //txtCRUP_ID.Text = Listview.SelectedDataKey[0].ToString();
            //txtActive.Text = Listview.SelectedDataKey[0].ToString();

        }

        #endregion

        #region PAge Genarator language


        protected void btnEditLable_Click(object sender, EventArgs e)
        {
            if (Session["LANGUAGE"].ToString() == "ar-KW")
            {
                if (btnEditLable.Text == "Update Label")
                {

                    //2false
                    lblCOLORDESC12h.Visible = lblCOLORDESC22h.Visible = lblCOLORREMARKS2h.Visible = lblhex2h.Visible = lblRGB2h.Visible = lblcolor2h.Visible = lblActive2h.Visible = false;
                    //2true
                    txtCOLORDESC12h.Visible = txtCOLORDESC22h.Visible = txtCOLORREMARKS2h.Visible = txthex2h.Visible = txtRGB2h.Visible = txtcolor2h.Visible = txtActive2h.Visible = true;

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
                    lblCOLORDESC12h.Visible = lblCOLORDESC22h.Visible = lblCOLORREMARKS2h.Visible = lblhex2h.Visible = lblRGB2h.Visible = lblcolor2h.Visible = lblActive2h.Visible = true;
                    //2false
                    txtCOLORDESC12h.Visible = txtCOLORDESC22h.Visible = txtCOLORREMARKS2h.Visible = txthex2h.Visible = txtRGB2h.Visible = txtcolor2h.Visible = txtActive2h.Visible = false;

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
                    lblCOLORDESC11s.Visible = lblCOLORDESC21s.Visible = lblCOLORREMARKS1s.Visible = lblhex1s.Visible = lblRGB1s.Visible = lblcolor1s.Visible = lblActive1s.Visible = false;
                    //1true
                    txtCOLORDESC11s.Visible = txtCOLORDESC21s.Visible = txtCOLORREMARKS1s.Visible = txthex1s.Visible = txtRGB1s.Visible = txtcolor1s.Visible = txtActive1s.Visible = true;
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
                    lblCOLORDESC11s.Visible = lblCOLORDESC21s.Visible = lblCOLORREMARKS1s.Visible = lblhex1s.Visible = lblRGB1s.Visible = lblcolor1s.Visible = lblActive1s.Visible = true;
                    //1false
                    txtCOLORDESC11s.Visible = txtCOLORDESC21s.Visible = txtCOLORREMARKS1s.Visible = txthex1s.Visible = txtRGB1s.Visible = txtcolor1s.Visible = txtActive1s.Visible = false;
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

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLCOLOR").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                //if (lblTenantID1s.ID == item.LabelID)
                //    txtTenantID1s.Text = lblTenantID1s.Text = item.LabelName;
                //else if (lblCOLORID1s.ID == item.LabelID)
                //    txtCOLORID1s.Text = lblCOLORID1s.Text = item.LabelName;
                if (lblCOLORDESC11s.ID == item.LabelID)
                    txtCOLORDESC11s.Text = lblCOLORDESC11s.Text = lblhCOLORDESC1.Text = item.LabelName;
                else if (lblCOLORDESC21s.ID == item.LabelID)
                    txtCOLORDESC21s.Text = lblCOLORDESC21s.Text = lblhCOLORDESC2.Text = item.LabelName;
                else if (lblCOLORREMARKS1s.ID == item.LabelID)
                    txtCOLORREMARKS1s.Text = lblCOLORREMARKS1s.Text = lblhCOLORREMARKS.Text = item.LabelName;
                else if (lblhex1s.ID == item.LabelID)
                    txthex1s.Text = lblhex1s.Text = item.LabelName;
                else if (lblRGB1s.ID == item.LabelID)
                    txtRGB1s.Text = lblRGB1s.Text = item.LabelName;
                else if (lblcolor1s.ID == item.LabelID)
                    txtcolor1s.Text = lblcolor1s.Text = item.LabelName;
                
                else if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = item.LabelName;

                //else if (lblTenantID2h.ID == item.LabelID)
                //    txtTenantID2h.Text = lblTenantID2h.Text = item.LabelName;
                //else if (lblCOLORID2h.ID == item.LabelID)
                //    txtCOLORID2h.Text = lblCOLORID2h.Text = item.LabelName;
                else if (lblCOLORDESC12h.ID == item.LabelID)
                    txtCOLORDESC12h.Text = lblCOLORDESC12h.Text = lblhCOLORDESC1.Text = item.LabelName;
                else if (lblCOLORDESC22h.ID == item.LabelID)
                    txtCOLORDESC22h.Text = lblCOLORDESC22h.Text = lblhCOLORDESC2.Text = item.LabelName;
                else if (lblCOLORREMARKS2h.ID == item.LabelID)
                    txtCOLORREMARKS2h.Text = lblCOLORREMARKS2h.Text = lblhCOLORREMARKS.Text = item.LabelName;
                else if (lblhex2h.ID == item.LabelID)
                    txthex2h.Text = lblhex2h.Text = item.LabelName;
                else if (lblRGB2h.ID == item.LabelID)
                    txtRGB2h.Text = lblRGB2h.Text = item.LabelName;
                else if (lblcolor2h.ID == item.LabelID)
                    txtcolor2h.Text = lblcolor2h.Text =  item.LabelName;
                
                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = item.LabelName;

                else
                    txtHeader.Text = lblHeader.Text = Label5.Text = item.LabelName;
            }

        }
        public void SaveLabel(string lang)
        {
            string PID = ((AcmMaster)this.Master).getOwnPage();
            //List<Database.TBLLabelDTL> List = DB.TBLLabelDTLs.Where(p => p.LabelMstID == PID  && p.LANGDISP == lang).ToList();
            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLCOLOR").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("\\Master\\xml\\TBLCOLOR.xml"));
            foreach (Database.TBLLabelDTL item in List)
            {

                var obj = ((AcmMaster)this.Master).Bindxml("TBLCOLOR").Single(p => p.LabelID == item.LabelID && p.LabelMstID == PID && p.LANGDISP == lang);
                int i = obj.ID - 1;

                //if (lblTenantID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenantID1s.Text;
                //else if (lblCOLORID1s.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCOLORID1s.Text;
                if (lblCOLORDESC11s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOLORDESC11s.Text;
                else if (lblCOLORDESC21s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOLORDESC21s.Text;
                else if (lblCOLORREMARKS1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOLORREMARKS1s.Text;
                else if (lblhex1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txthex1s.Text;
                else if (lblRGB1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRGB1s.Text;
                else if (lblcolor1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtcolor1s.Text;
                
                else if (lblActive1s.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive1s.Text;

                //else if (lblTenantID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtTenantID2h.Text;
                //else if (lblCOLORID2h.ID == item.LabelID)
                //    ds.Tables[0].Rows[i]["LabelName"] = txtCOLORID2h.Text;
                else if (lblCOLORDESC12h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOLORDESC12h.Text;
                else if (lblCOLORDESC22h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOLORDESC22h.Text;
                else if (lblCOLORREMARKS2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtCOLORREMARKS2h.Text;
                else if (lblhex2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txthex2h.Text;
                else if (lblRGB2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtRGB2h.Text;
                else if (lblcolor2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtcolor2h.Text;
                
                else if (lblActive2h.ID == item.LabelID)
                    ds.Tables[0].Rows[i]["LabelName"] = txtActive2h.Text;

                else
                    ds.Tables[0].Rows[i]["LabelName"] = txtHeader.Text;
            }
            ds.WriteXml(Server.MapPath("\\Master\\xml\\TBLCOLOR.xml"));

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

        #endregion
        public void Write()
        {
            //navigation.Visible = false;
            //drpCOLORID.Enabled = true;
            txtCOLORDESC1.Enabled = true;
            txtCOLORDESC2.Enabled = true;
            txtCOLORREMARKS.Enabled = true;
            txthex.Enabled = true;
            txtRGB.Enabled = true;
            txtcolor.Enabled = true;           
            cbActive.Enabled = true;
        }
        public void Readonly()
        {
            //navigation.Visible = true;
            //drpCOLORID.Enabled = false;
            txtCOLORDESC1.Enabled = false;
            txtCOLORDESC2.Enabled = false;
            txtCOLORREMARKS.Enabled = false;
            txthex.Enabled = false;
            txtRGB.Enabled = false;
            txtcolor.Enabled = false;           
            cbActive.Enabled = false;
        }

        #region Listview
        //protected void btnNext1_Click(object sender, EventArgs e)
        //{
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.TBLCOLORs.Count();
        //    if (ViewState["Take"] == null && ViewState["Skip"] == null)
        //    {
        //        ViewState["Take"] = Showdata;
        //        ViewState["Skip"] = 0;
        //    }
        //    take = Convert.ToInt32(ViewState["Take"]);
        //    take = take + Showdata;
        //    Skip = take - Showdata;

        //    ((AcmMaster)Page.Master).BindList(Listview, (DB.TBLCOLORs.Where(p => p.Active == "Y" && p.TenentID == TID).OrderBy(m => m.COLORID).Take(take).Skip(Skip)).ToList());
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
        //        int Totalrec = DB.TBLCOLORs.Count();
        //        Skip = Convert.ToInt32(ViewState["Skip"]);
        //        take = Skip;
        //        Skip = take - Showdata;
        //        ((AcmMaster)Page.Master).BindList(Listview, (DB.TBLCOLORs.Where(p => p.Active == "Y" && p.TenentID == TID).OrderBy(m => m.COLORID).Take(take).Skip(Skip)).ToList());
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
        //        int Totalrec = DB.TBLCOLORs.Count();
        //        take = Showdata;
        //        Skip = 0;
        //        ((AcmMaster)Page.Master).BindList(Listview, (DB.TBLCOLORs.Where(p => p.Active == "Y" && p.TenentID == TID).OrderBy(m => m.COLORID).Take(take).Skip(Skip)).ToList());
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
        //    int Totalrec = DB.TBLCOLORs.Count();
        //    take = Totalrec;
        //    Skip = Totalrec - Showdata;
        //    ((AcmMaster)Page.Master).BindList(Listview, (DB.TBLCOLORs.Where(p => p.Active == "Y" && p.TenentID == TID).OrderBy(m => m.COLORID).Take(take).Skip(Skip)).ToList());
        //    ViewState["Take"] = take;
        //    ViewState["Skip"] = Skip;
        //    btnNext1.Enabled = false;
        //    btnPrevious1.Enabled = true;
        //    ChoiceID = take / Showdata;
        //    ((AcmMaster)Page.Master).GetCurrentNavigationLast(ChoiceID, Showdata, ListView2, Totalrec);
        //    lblShowinfEntry.Text = "Showing " + ViewState["Skip"].ToString() + " to " + ViewState["Take"].ToString() + " of " + Totalrec + " entries";
        //}
        protected void btnlistreload_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void btnPagereload_Click(object sender, EventArgs e)
        {
            //Readonly();
            //ManageLang();
            //pnlSuccessMsg.Visible = false;
            //FillContractorID();
            //int CurrentID = 1;
            //if (ViewState["Es"] != null)
            //    CurrentID = Convert.ToInt32(ViewState["Es"]);
            BindData();
           // btnAdd.Text = "Add New";
            //btnAdd.ValidationGroup = "ss";
           // FirstData();
        }

        protected void Listview_ItemCommand1(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //try
                //{
                    if (e.CommandName == "btnDelete")
                    {

                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int TIDD = Convert.ToInt32(ID[0]);
                        int CID = Convert.ToInt32(ID[1]);
                        Database.TBLCOLOR objTBLCOLORr = DB.TBLCOLORs.Single(p => p.TenentID == TIDD && p.COLORID == CID);
                        objTBLCOLORr.Active = "N";
                        DB.SaveChanges();
                        Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, "Data Deleted Successfully", "Success!", Classes.Toastr.ToastPosition.TopCenter);
                        BindData();
                        
                    }

                    if (e.CommandName == "btnEdit")
                    {
                        string[] ID = e.CommandArgument.ToString().Split(',');
                        int TIDD = Convert.ToInt32(ID[0]);
                        int CID = Convert.ToInt32(ID[1]);
                        Database.TBLCOLOR objTBLCOLOR = DB.TBLCOLORs.Single(p => p.TenentID == TIDD && p.COLORID == CID);
                       
                        //drpCOLORID.SelectedValue = objTBLCOLOR.COLORID.ToString();
                        txtCOLORDESC1.Text = objTBLCOLOR.COLORDESC1.ToString();
                        txtCOLORDESC2.Text = objTBLCOLOR.COLORDESC2.ToString();
                        txtCOLORREMARKS.Text = objTBLCOLOR.COLORREMARKS.ToString();
                        if (objTBLCOLOR.hex == null)
                        {

                        }
                        else
                        {
                            txthex.Text = objTBLCOLOR.hex.ToString();
                        }
                        if (objTBLCOLOR.RGB == null)
                        {

                        }
                        else
                        {
                            txtRGB.Text = objTBLCOLOR.RGB.ToString();
                        }
                        if (objTBLCOLOR.color == null)
                        {

                        }
                        else
                        {
                            txtcolor.Text = objTBLCOLOR.color.ToString();
                        }
                       
                        //txtCRUP_ID.Text = objTBLCOLOR.CRUP_ID.ToString();
                        //txtActive.Text = objTBLCOLOR.Active.ToString();
                        if (objTBLCOLOR.Active != "Y")
                        {
                            cbActive.Checked = false;
                        }
                        else
                        {
                            cbActive.Checked = true;
                        }    
                        btnAdd.Text = "Update";
                        ViewState["Edit"] = ID;
                        ViewState["TIDD"] = TIDD;
                        ViewState["CID"] = CID;
                        Write();
                    }
                    scope.Complete(); //  To commit.
                //}
                //catch (Exception ex)
                //{
                //    ScriptManager.RegisterClientScriptBlock(sender as Control, this.GetType(), "alert", ex.Message, true);
                //    throw;
                //}
            }
        }
       

        //protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{
        //    int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);
        //    int Totalrec = DB.TBLCOLORs.Count();
        //    if (e.CommandName == "LinkPageavigation")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ViewState["Take"] = ID * Showdata;
        //        ViewState["Skip"] = (ID * Showdata) - Showdata;
        //        int Tvalue = Convert.ToInt32(ViewState["Take"]);
        //        int Svalue = Convert.ToInt32(ViewState["Skip"]);
        //        ((AcmMaster)Page.Master).BindList(Listview, (DB.TBLCOLORs.Where(p => p.Active == "Y" && p.TenentID == TID).OrderBy(m => m.COLORID).Take(Tvalue).Skip(Svalue)).ToList());
        //        ChoiceID = ID;
        //        ((AcmMaster)Page.Master).GetCurrentNavigation(ChoiceID, Showdata, ListView2,Totalrec);
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