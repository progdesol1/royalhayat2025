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
using Database;
using System.Collections.Generic;
using System.Transactions;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using Web.CRM.Class.Class;
using System.Drawing;
using GenCode128;
using QRCoder;
using System.Web.Services;
using System.ComponentModel;
using System.Web.SessionState;
using Classes;

namespace Web.Master
{
    public partial class ICCATSUB : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 0;
        OleDbConnection Econ;
        SqlConnection con1;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {
                Readonly();
                bindsubcat();
                bindcat();
                Binddata();
                btnAdd.ValidationGroup = "ss";
            }
        }

        public void Readonly()
        {
            drpcat.Enabled = false;
            drpsubcat.Enabled = false;
        }


        public void Binddata()
        {
            List<Database.ICCATSUBCAT> List = DB.ICCATSUBCATs.Where(p => p.TenentID == TID).OrderBy(m => m.MYCATSUBID).ToList();
            listcategory.DataSource = List;
            listcategory.DataBind();
        }

        public void clear()
        {
            drpcat.SelectedIndex = 0;
            drpsubcat.SelectedIndex = 0;
        }

        public void Write()
        {
            drpcat.Enabled = true;
            drpsubcat.Enabled = true;
        }

   

        public void bindcat()
        {
            drpcat.DataSource = DB.ICCATEGORies.Where(p => p.TenentID == TID);
            drpcat.DataTextField = "CATNAME";
            drpcat.DataValueField = "CATID";
            drpcat.DataBind();
            drpcat.Items.Insert(0, new ListItem("-- Select Category --", "0"));
        }

        public void bindsubcat()
        {
            drpsubcat.DataSource = DB.ICSUBCATEGORies.Where(p => p.TenentID == TID);
            drpsubcat.DataTextField = "SUBCATNAME";
            drpsubcat.DataValueField = "SUBCATID";
            drpsubcat.DataBind();
            drpsubcat.Items.Insert(0, new ListItem("-- Select Sub Category --", "0"));
        }


        protected void listcategory_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "LinkEMPEDIT")
            {

                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int ID = Convert.ToInt32(e.CommandArgument);
                Database.ICCATSUBCAT objtblSubscriptionSetup = DB.ICCATSUBCATs.Single(p => p.TenentID == TID && p.MYCATSUBID == ID);
                drpcat.SelectedValue = objtblSubscriptionSetup.CATID.ToString();
                drpsubcat.SelectedValue = objtblSubscriptionSetup.SUBCATID.ToString();
                btnAdd.Text = "Update";
                ViewState["Edit"] = ID;
                Write();
                btnAdd.ValidationGroup = "submit";

                           }

            if (e.CommandName == "LinkEMPDELETE")
            {
               
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.ICCATSUBCAT EditObj = DB.ICCATSUBCATs.Single(p => p.TenentID == TID && p.MYCATSUBID == ID);
                DB.ICCATSUBCATs.DeleteObject(EditObj);
                DB.SaveChanges();

                String url = "delete ICCATSUBCAT with " + "TenentID = " + TID + "COMPANYID = 2" + "MYCATSUBID = " + ID;
                String evantname = "delete";
                String tablename = "ICCATSUBCAT";
                string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                lblMsg.Text = "  Data Delete Successfully";
                Binddata();
            }
        }

        public string getCatname(int CATID)
        {
            if (CATID == 0 || CATID == null)
            {
                return null;
            }
            else
            {
                return DB.ICCATEGORies.SingleOrDefault(p => p.CATID == CATID && p.TenentID == TID).CATNAME;
            }

        }
        public string getSubCatname(int SUBCATID)
        {
            if (SUBCATID == 0 || SUBCATID == null)
            {
                return null;
            }
            else
            {
                return DB.ICSUBCATEGORies.SingleOrDefault(p => p.SUBCATID == SUBCATID && p.TenentID == TID).SUBCATNAME;
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add New")
            {
                Write();
                clear();
                btnAdd.Text = "Save";
                btnAdd.ValidationGroup = "submit";
            }
            else if (btnAdd.Text == "Save")
            {
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Database.ICCATSUBCAT objProject_Details = new Database.ICCATSUBCAT();
                int MAXID = DB.ICCATSUBCATs.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ICCATSUBCATs.Where(p => p.TenentID == TID).Max(p => p.MYCATSUBID) + 1) : 1;
                objProject_Details.TenentID = TID;
                objProject_Details.COMPANYID = 2;
                objProject_Details.MYCATSUBID = MAXID;
                objProject_Details.CATID = Convert.ToInt32(drpcat.SelectedValue);
                objProject_Details.CATTYPE = "HelpDesk";
                objProject_Details.SUBCATID = Convert.ToInt32(drpsubcat.SelectedValue);
                objProject_Details.SUBCATTYPE = "HelpDesk";
                objProject_Details.REMARKS = "HelpDesk";
                objProject_Details.ITEMID = "HelpDesk";
                objProject_Details.status = 1;
                objProject_Details.CRUP_ID = 1;
                DB.ICCATSUBCATs.AddObject(objProject_Details);
                DB.SaveChanges();

                String url = "insert new record in ICCATSUBCAT with " + "TenentID = " + TID + "COMPANYID = 2" + "MYCATSUBID = " + MAXID;
                String evantname = "create";
                String tablename = "ICCATSUBCAT";
                string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                btnAdd.Text = "Add New";
                btnAdd.ValidationGroup = "ss";
                lblMsg.Text = "Data Save Successfully";
                pnlSuccessMsg.Visible = true;
                Binddata();
                Readonly();        
            }
            else if (btnAdd.Text == "Update")
            {

                if (ViewState["Edit"] != null)
                {
                    int ID = Convert.ToInt32(ViewState["Edit"]);
                    Database.ICCATSUBCAT objProject_Details = DB.ICCATSUBCATs.Single(p => p.MYCATSUBID == ID && p.TenentID == TID);
                    objProject_Details.TenentID = TID;
                    objProject_Details.COMPANYID = 2;
                    objProject_Details.MYCATSUBID = ID;
                    objProject_Details.CATID = Convert.ToInt32(drpcat.SelectedValue);
                    objProject_Details.CATTYPE = "Help Desk";
                    objProject_Details.SUBCATID = Convert.ToInt32(drpsubcat.SelectedValue);
                    objProject_Details.SUBCATTYPE = "Help Desk";
                    objProject_Details.REMARKS = "Help Desk";
                    objProject_Details.ITEMID = "Help Desk";
                    objProject_Details.status = 1;
                    objProject_Details.CRUP_ID = 1;
                    ViewState["Edit"] = null;
                    btnAdd.Text = "Add New";
                    DB.SaveChanges();

                    String url = "update ICCATSUBCAT with " + "TenentID = " + TID + "COMPANYID = 2" + "MYCATSUBID = " + ID;
                    String evantname = "update";
                    String tablename = "ICCATSUBCAT";
                    string loginUserId = (((USER_MST)HttpContext.Current.Session["USER"]).USER_ID).ToString();

                    Classes.GlobleClass.EncryptionHelpers.WriteLog(url, evantname, tablename, loginUserId.ToString(), 0,0);

                    clear();
                    lblMsg.Text = "  Data Edit Successfully";
                    pnlSuccessMsg.Visible = true;
                    Binddata();
                    Readonly();
                }
            }
        }

    }

}