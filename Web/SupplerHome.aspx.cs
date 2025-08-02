using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Transactions;
using System.Net.Mail;

namespace Web
{
    public partial class SupplerHome : System.Web.UI.Page
    {
        ERPEntities DB = new ERPEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USER"] == null)
                {
                    Response.Redirect("Login.aspx");

                }
                ListTikit();
                BindNewProduct();
            }
        }
        public void ListTikit()
        {
            List<Database.Eco_CRMMainActivitiesTikit> List = Classes.EcommAdminClass.getDataEco_CRMMainActivitiesTikit();
            listofTikit.DataSource = List;
            listofTikit.DataBind();
            List<Database.Eco_tbl_test_order> List2 = Classes.EcommAdminClass.getDataEco_tbl_test_order();
            listCustomer.DataSource = List2.Take(7);
            listCustomer.DataBind();

        }
        public void BindNewProduct()
        {
            //  int TID = Convert.ToInt32(((Eco_ERP_WEB_USER_MST)Session["UserAdmin"]).TENANT_ID);
              int Uid = Convert.ToInt32(((Eco_TBLCONTACT )Session["USER"]).ContactMyID );
              List<Database.Eco_User_Product_Add> List3 = DB.Eco_User_Product_Add.Where(p => p.Appover == false && p.Active == true && p.UserID == Uid).ToList();
            ListNewProduct.DataSource = List3;
            ListNewProduct.DataBind(); 
            for (int i = 0; i < ListNewProduct.Items.Count; i++)
            {
                //LinkButton ln = (LinkButton)ListNewProduct.Items[i].FindControl("lnkAction");
                Label lblStet = (Label)ListNewProduct.Items[i].FindControl("lblStet");
              
                if (lblStet.Text.Trim().Equals(true))
                {
                    lblStet.Text = "Appover";
                   
                }
                else
                {
                   
                    lblStet.Text = "In Appover";
                }
            }
            int Count = List3.Count;
            string Total = Count.ToString();
            lblNewProduct.Text = Total + " New".ToString();
            List<Eco_TBLPRODUCT> list = DB.Eco_TBLPRODUCT.Where(p => p.ACTIVE == "1" && p.Appove == false && p.COMPANYID == Uid).ToList();
            int productcount = list.Count;
            listProdct.DataSource = list;
            listProdct.DataBind();
            lblProductCunt.Text = productcount.ToString();
            for (int i = 0; i < listProdct.Items.Count; i++)
            {
                //LinkButton ln = (LinkButton)ListNewProduct.Items[i].FindControl("lnkAction");
                Label lblStetProduct = (Label)ListNewProduct.Items[i].FindControl("lblStetProduct");

                if (lblStetProduct.Text.Trim().Equals(true))
                {
                    lblStetProduct.Text = "Appover";

                }
                else
                {

                    lblStetProduct.Text = "In Appover";
                }
            }
        } 
        public string getName(int IID)
        {
            return DB.Eco_TBLCONTACT.Single(p => p.ContactMyID == IID).PersName1;
        }
        public string getCategory(int CID)
        {
            return DB.Eco_CAT_MST.Single(p => p.CATID == CID).CAT_NAME1;
        }

        protected void ListNewProduct_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (e.CommandName == "btnAppow")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);

                    Eco_User_Product_Add objEco_User_Product_Add = DB.Eco_User_Product_Add.Single(p => p.MyID == ID);
                    int UserID = objEco_User_Product_Add.UserID;
                    objEco_User_Product_Add.Appover = true;
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_User_Product_Add");
                    objEco_ERP_WEB_USER_MST.IsCache = true;
                    objEco_ERP_WEB_USER_MST.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                    Eco_TBLCONTACT objEco_TBLCONTACT = DB.Eco_TBLCONTACT.Single(p => p.ContactMyID == UserID);
                    Database.Eco_ERP_WEB_USER_DTL objERP_WEB_USER_DTL = new Database.Eco_ERP_WEB_USER_DTL();
                    objERP_WEB_USER_DTL.TENANT_ID = objEco_TBLCONTACT.TenantID;
                    objERP_WEB_USER_DTL.USER_DETAIL_ID = DB.Eco_ERP_WEB_USER_DTL.Count() > 0 ? Convert.ToInt32(DB.Eco_ERP_WEB_USER_DTL.Max(p => p.USER_DETAIL_ID) + 1) : 1;
                    if (objEco_TBLCONTACT.COUNTRYID != null)
                        objERP_WEB_USER_DTL.COUNTRY_CODE = objEco_TBLCONTACT.COUNTRYID.ToString();
                    // objERP_WEB_USER_DTL.TITLE = txtTITLE.Text;
                    // objERP_WEB_USER_DTL.HOUSE_NO = txtHOUSE_NO.Text;
                    // objERP_WEB_USER_DTL.STREET = txtSTREET.Text;
                    if (objEco_TBLCONTACT.ADDR1 != null)
                        objERP_WEB_USER_DTL.ADDRESS1 = objEco_TBLCONTACT.ADDR1;
                    if (objEco_TBLCONTACT.ADDR2 != null)
                        objERP_WEB_USER_DTL.ADDRESS2 = objEco_TBLCONTACT.ADDR2;
                    if (objEco_TBLCONTACT.CITY != null)
                        objERP_WEB_USER_DTL.CITY = objEco_TBLCONTACT.CITY;
                    if (objEco_TBLCONTACT.COUNTRYID != null)
                        objERP_WEB_USER_DTL.COUNTRY = objEco_TBLCONTACT.COUNTRYID;
                    if (objEco_TBLCONTACT.STATE != null)
                        objERP_WEB_USER_DTL.STATE = objEco_TBLCONTACT.STATE;
                    if (objEco_TBLCONTACT.ZIPCODE != null)
                        objERP_WEB_USER_DTL.ZIP = objEco_TBLCONTACT.ZIPCODE;
                    if (objEco_TBLCONTACT.MOBPHONE != null)
                        objERP_WEB_USER_DTL.PH_NO = objEco_TBLCONTACT.MOBPHONE;
                    if (objEco_TBLCONTACT.CITY != null)
                        objERP_WEB_USER_DTL.VILLAGE_TOWN_CITY = objEco_TBLCONTACT.CITY;
                    if (objEco_TBLCONTACT.BUSPHONE1 != null)
                        objERP_WEB_USER_DTL.PINCODE_NO = objEco_TBLCONTACT.BUSPHONE1;
                    if (objEco_TBLCONTACT.POSTALCODE != null)
                        objERP_WEB_USER_DTL.POST_OFFICE = objEco_TBLCONTACT.POSTALCODE;
                    if (objEco_TBLCONTACT.MOBPHONE == null)
                        objERP_WEB_USER_DTL.MOBILE_NUM = 0;
                    else
                        objERP_WEB_USER_DTL.MOBILE_NUM = Convert.ToDecimal(objEco_TBLCONTACT.MOBPHONE);
                    DB.Eco_ERP_WEB_USER_DTL.AddObject(objERP_WEB_USER_DTL);
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST1 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ERP_WEB_USER_DTL");
                    objEco_ERP_WEB_USER_MST1.IsCache = true;
                    objEco_ERP_WEB_USER_MST1.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                    Database.Eco_ERP_WEB_USER_MST objERP_WEB_USER_MST = new Database.Eco_ERP_WEB_USER_MST();
                    objERP_WEB_USER_MST.TENANT_ID = objEco_TBLCONTACT.TenantID;
                    objERP_WEB_USER_MST.USER_ID = DB.Eco_ERP_WEB_USER_MST.Count() > 0 ? Convert.ToInt32(DB.Eco_ERP_WEB_USER_MST.Max(p => p.USER_ID) + 1) : 1;
                    objERP_WEB_USER_MST.LocationId = 1;
                    if (objEco_TBLCONTACT.PersName1 != null)
                        objERP_WEB_USER_MST.FIRST_NAME = objEco_TBLCONTACT.PersName1;
                    if (objEco_TBLCONTACT.PersName2 != null)
                        objERP_WEB_USER_MST.LAST_NAME = objEco_TBLCONTACT.PersName2;
                    if (objEco_TBLCONTACT.PersName1 != null)
                        objERP_WEB_USER_MST.FIRST_NAME1 = objEco_TBLCONTACT.PersName1;
                    if (objEco_TBLCONTACT.PersName2 != null)
                        objERP_WEB_USER_MST.LAST_NAME1 = objEco_TBLCONTACT.PersName2;
                    if (objEco_TBLCONTACT.PersName1 != null)
                        objERP_WEB_USER_MST.FIRST_NAME2 = objEco_TBLCONTACT.PersName1;
                    if (objEco_TBLCONTACT.PersName2 != null)
                        objERP_WEB_USER_MST.LAST_NAME2 = objEco_TBLCONTACT.PersName2;
                    if (objEco_TBLCONTACT.CUSERID != null)
                        objERP_WEB_USER_MST.LOGIN_ID = objEco_TBLCONTACT.CUSERID;
                    if (objEco_TBLCONTACT.CPASSWRD != null)
                        objERP_WEB_USER_MST.PASSWORD = objEco_TBLCONTACT.CPASSWRD;

                    objERP_WEB_USER_MST.USER_TYPE = 116;
                    if (objEco_TBLCONTACT.REMARKS != null)
                        objERP_WEB_USER_MST.REMARKS = objEco_TBLCONTACT.REMARKS;
                    objERP_WEB_USER_MST.ACTIVE_FLAG = "Y";
                    objERP_WEB_USER_MST.LAST_LOGIN_DT = DateTime.Now;

                    objERP_WEB_USER_MST.ACC_LOCK = "Y";
                    objERP_WEB_USER_MST.FIRST_TIME = "Y";
                    if (objEco_TBLCONTACT.EMAIL1 != null)
                        objERP_WEB_USER_MST.EmailAddress = objEco_TBLCONTACT.EMAIL1;
                    //if (txtAvtar.Text == "")
                    //    objERP_WEB_USER_MST.Avtar = " ";
                    //else
                    //    objERP_WEB_USER_MST.Avtar = txtAvtar.Text;
                    objERP_WEB_USER_MST.CompId = Convert.ToInt32(objEco_TBLCONTACT.ContactMyID);
                    objERP_WEB_USER_MST.USER_DETAIL_ID = objERP_WEB_USER_DTL.USER_DETAIL_ID;
                    DB.Eco_ERP_WEB_USER_MST.AddObject(objERP_WEB_USER_MST);
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST2 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_ERP_WEB_USER_MST");
                    objEco_ERP_WEB_USER_MST2.IsCache = true;
                    objEco_ERP_WEB_USER_MST2.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                }
                if (e.CommandName == "btnNotAppow")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    Eco_User_Product_Add objEco_User_Product_Add = DB.Eco_User_Product_Add.Single(p => p.MyID == ID);
                    objEco_User_Product_Add.Active = false;
                    objEco_User_Product_Add.Deleted = false;
                    DB.SaveChanges();
                    Eco_Cache_Mst objEco_ERP_WEB_USER_MST2 = DB.Eco_Cache_Mst.Single(p => p.TableName == "Eco_User_Product_Add");
                    objEco_ERP_WEB_USER_MST2.IsCache = true;
                    objEco_ERP_WEB_USER_MST2.DateAndTime = DateTime.Now;
                    DB.SaveChanges();
                } 
                scope.Complete(); //  To commit.

            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
           
            String Url = ("NewProduct.aspx");
            String URL = ("<script language='javascript'>window.open('" + Url + "','_blank','');");
            Response.Write(URL);
            Response.Write("</scr" + "ipt>");
        }
    }
}