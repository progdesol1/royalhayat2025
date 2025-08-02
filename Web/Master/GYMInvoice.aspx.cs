using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Web.Configuration;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Web.Services;

namespace Web.Master
{
    public partial class GYMInvoice : System.Web.UI.Page
    {
        int TID = 0;
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {
                Session["LANGUAGE"] = "en-US";
                ManageLang();
                Packagebind();
                PackClear();
            }
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static string[] GetCustSearch(string prefixText, int count)
        {
            int TID = Convert.ToInt32(((USER_MST)HttpContext.Current.Session["USER"]).TenentID);
            string conStr;
            conStr = WebConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString;
            string sqlQuery = "select PersName1,ContactMyID from TBLCONTACT where TenentID = " + TID + " and (PersName1 like @CustName  + '%' or PersName2 like @CustName  + '%' or PersName3 like @CustName  + '%' or CivilID like @CustName  + '%' or MOBPHONE like @CustName  + '%')";
            //string sqlQuery = "SELECT [COMPNAME1]+' - '+MOBPHONE,[COMPID] FROM [TBLCOMPANYSETUP] WHERE TenentID='" + TID + "' and BUYER = 'true' and (COMPNAME1 like @COMPNAME  + '%' or COMPNAME2 like @COMPNAME  + '%' or COMPNAME3 like @COMPNAME  + '%' or MOBPHONE like @COMPNAME  + '%')";
            SqlConnection conn = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@CustName", prefixText);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<string> custList = new List<string>();
            string custItem = string.Empty;
            while (dr.Read())
            {
                custItem = AutoCompleteExtender.CreateAutoCompleteItem(dr[0].ToString(), dr[1].ToString());
                custList.Add(custItem);
            }
            conn.Close();
            dr.Close();
            return custList.ToArray();
        }


        public void Packagebind()
        {
            List<Database.ICTR_HD> HDLIST = DB.ICTR_HD.Where(p => p.TenentID == TID).ToList();
            if (HDLIST.Count() > 0)
            {
                List<Database.ICTR_DT> ListDT = DB.ICTR_DT.Where(p => p.TenentID == TID).ToList();
                ListPackage.DataSource = ListDT;
                ListPackage.DataBind();
            }

        }


        public void PackClear()
        {

            txtAmount.Text = "";
            txtpackStartdate.Text = "";
            txtpackEnddate.Text = "";
        }
        public string GetPack(int RID)
        {
            if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == RID).Count() > 0)
            {
                string Packname = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == RID).ProdName1;
                return Packname;
            }
            else
            {
                return "Not Found";
            }
        }
        protected void btncnacel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Master/GYMInvoice.aspx");
        }

        protected void ListPackage_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnprient")
            {
                string[] ID = e.CommandArgument.ToString().Split(',');
                int Mytranceid = Convert.ToInt32(ID[0]);
                int Packid = Convert.ToInt32(ID[1]);
                //int Mytranceid = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("/Master/GYMPrint.aspx?Tranjestion=" + Mytranceid + "&Pack=" + Packid);
            }
            if (e.CommandName == "btnview")
            {
                int MyTranID = Convert.ToInt32(e.CommandArgument);
                FillData(MyTranID);
            }
        }
        public void FillData(int MID)
        {
            Database.ICTR_DT OBJ = DB.ICTR_DT.Single(p => p.TenentID == TID && p.MYTRANSID == MID);
            txtAmount.Text = OBJ.AMOUNT.ToString();
            txtpackStartdate.Text = Convert.ToDateTime(OBJ.PlanStartDate).ToString("MM/dd/yyyy");
            txtpackEnddate.Text = Convert.ToDateTime(OBJ.PlanEndDate).ToString("MM/dd/yyyy");
            lblmonth.Text = "Package in Months" + " " + OBJ.BIN_ID.ToString();
        }
        public string getContactName(int ID)
        {
            int ConID = Convert.ToInt32(ID);
            string Name = "";
            if (DB.TBLCONTACTs.Where(p => p.ContactMyID == ConID && p.TenentID == TID).Count() > 0)
            {
                Database.TBLCONTACT obj_con = DB.TBLCONTACTs.Single(p => p.ContactMyID == ConID && p.TenentID == TID);
                Name = obj_con.PersName1;
            }
            return ConID.ToString() + "-" + Name;
        }
        //For Label
        protected void LanguageEnglish_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "en-US";
            ManageLang();
        }

        protected void LanguageArabic_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "ar-KW";
            ManageLang();
        }

        protected void LanguageFrance_Click(object sender, EventArgs e)
        {
            Session["LANGUAGE"] = "fr-FR";
            ManageLang();
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
        public void GetShow()
        {

            lblActive1s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = lblCRUP_ID1s.Attributes["class"] = lblCOMPANYID1s.Attributes["class"] = lblMYCATSUBID1s.Attributes["class"] = lblPERSNAME1s.Attributes["class"] = lblPERSNAMEO1s.Attributes["class"] = lblPERSNAMEO21s.Attributes["class"] = "control-label col-md-4  getshow";
            lblActive2h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = lblCRUP_ID2h.Attributes["class"] = lblCOMPANYID2h.Attributes["class"] = lblMYCATSUBID2h.Attributes["class"] = lblPERSNAME2h.Attributes["class"] = lblPERSNAMEO2h.Attributes["class"] = lblPERSNAMEO22h.Attributes["class"] = "control-label col-md-4  gethide";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "ltr");

        }

        public void GetHide()
        {
            lblActive1s.Attributes["class"] = lblREMARKS1s.Attributes["class"] = lblCRUP_ID1s.Attributes["class"] = lblCOMPANYID1s.Attributes["class"] = lblMYCATSUBID1s.Attributes["class"] = lblPERSNAME1s.Attributes["class"] = lblPERSNAMEO1s.Attributes["class"] = lblPERSNAMEO21s.Attributes["class"] = "control-label col-md-4  gethide";
            lblActive2h.Attributes["class"] = lblREMARKS2h.Attributes["class"] = lblCRUP_ID2h.Attributes["class"] = lblCOMPANYID2h.Attributes["class"] = lblMYCATSUBID2h.Attributes["class"] = lblPERSNAME2h.Attributes["class"] = lblPERSNAMEO2h.Attributes["class"] = lblPERSNAMEO22h.Attributes["class"] = "control-label col-md-4  getshow";
            b.Attributes.Remove("dir");
            b.Attributes.Add("dir", "rtl");

        }
        public void RecieveLabel(string lang)
        {
            string str = "";
            string PID = ((AcmMaster)this.Master).getOwnPage();

            List<Database.TBLLabelDTL> List = ((AcmMaster)this.Master).Bindxml("TBLCONTACT").Where(p => p.LabelMstID == PID && p.LANGDISP == lang).ToList();
            foreach (Database.TBLLabelDTL item in List)
            {
                if (lblActive1s.ID == item.LabelID)
                    txtActive1s.Text = lblActive1s.Text = item.LabelName;
                else if (lblREMARKS1s.ID == item.LabelID)
                    txtREMARKS1s.Text = lblREMARKS1s.Text = item.LabelName;
                else if (lblCRUP_ID1s.ID == item.LabelID)
                    txtCRUP_ID1s.Text = lblCRUP_ID1s.Text = item.LabelName;
                else if (lblCOMPANYID1s.ID == item.LabelID)
                    txtCOMPANYID1s.Text = lblCOMPANYID1s.Text = item.LabelName;
                else if (lblMYCATSUBID1s.ID == item.LabelID)
                    txtMYCATSUBID1s.Text = lblMYCATSUBID1s.Text = item.LabelName;

                else if (lblPERSNAME1s.ID == item.LabelID)
                    txtPERSNAME1s.Text = lblPERSNAME1s.Text = item.LabelName;
                else if (lblPERSNAMEO1s.ID == item.LabelID)
                    txtPERSNAMEO1s.Text = lblPERSNAMEO1s.Text = item.LabelName;
                else if (lblPERSNAMEO21s.ID == item.LabelID)
                    txtPERSNAMEO21s.Text = lblPERSNAMEO21s.Text = item.LabelName;

                else if (lblActive2h.ID == item.LabelID)
                    txtActive2h.Text = lblActive2h.Text = item.LabelName;
                else if (lblREMARKS2h.ID == item.LabelID)
                    txtREMARKS2h.Text = lblREMARKS2h.Text = item.LabelName;
                else if (lblCRUP_ID2h.ID == item.LabelID)
                    txtCRUP_ID2h.Text = lblCRUP_ID2h.Text = item.LabelName;
                else if (lblCOMPANYID2h.ID == item.LabelID)
                    txtCOMPANYID2h.Text = lblCOMPANYID2h.Text = item.LabelName;
                else if (lblMYCATSUBID2h.ID == item.LabelID)
                    txtMYCATSUBID2h.Text = lblMYCATSUBID2h.Text = item.LabelName;

                else if (lblPERSNAME2h.ID == item.LabelID)
                    txtPERSNAME2h.Text = lblPERSNAME2h.Text = item.LabelName;
                else if (lblPERSNAMEO2h.ID == item.LabelID)
                    txtPERSNAMEO2h.Text = lblPERSNAMEO2h.Text = item.LabelName;
                else if (lblPERSNAMEO22h.ID == item.LabelID)
                    txtPERSNAMEO22h.Text = lblPERSNAMEO22h.Text = item.LabelName;

            }

        }



    }
}