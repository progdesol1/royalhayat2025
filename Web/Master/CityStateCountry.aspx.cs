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
    public partial class CityStateCountry : System.Web.UI.Page
    {
        Database.CallEntities DB = new Database.CallEntities();
        int TID = 0;
        OleDbConnection Econ;
        SqlConnection con1;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                BindCountry();
                BindState();
            }
        }

        public void BindCountry()
        {
           
            drpCountry.DataSource = DB.tblCOUNTRies.Where(p => p.TenentID == TID);
            drpCountry.DataTextField = "COUNAME1";
            drpCountry.DataValueField = "COUNTRYID";
            drpCountry.DataBind();
            drpCountry.Items.Insert(0, new ListItem("-- Select Country --", "0"));
            
        }

        protected void DrpState_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void BindState()
        {
            if (drpCountry.SelectedValue != null && drpCountry.SelectedValue != "0")
            {

                int CID = Convert.ToInt32(drpCountry.SelectedValue);
                DrpState.DataSource = DB.tblStates.Where(p => p.COUNTRYID == CID && p.ACTIVE1 == "Y");
                DrpState.DataTextField = "MYNAME1";
                DrpState.DataValueField = "StateID";
                DrpState.DataBind();
                DrpState.Items.Insert(0, new ListItem("SELECT", "0"));
            }
            else
            {
                DrpState.DataSource = DB.tblStates.Where(p => p.ACTIVE1 == "Y");
                DrpState.DataTextField = "MYNAME1";
                DrpState.DataValueField = "StateID";
                DrpState.DataBind();
                DrpState.Items.Insert(0, new ListItem("SELECT", "0"));
            }
        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(drpCountry.SelectedValue);
            string str1 = drpCountry.SelectedValue;
            DrpState.DataSource = DB.tblStates.Where(p => p.COUNTRYID == CID);
            DrpState.DataTextField = "MYNAME1";
            DrpState.DataValueField = "StateID";
            DrpState.DataBind();
            DrpState.Items.Insert(0, new ListItem("SELECT", "0"));
        }

        protected void listSocialMedia_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            if (e.CommandName == "LinkEMPEDIT")
            {

                int CityID = Convert.ToInt32(e.CommandArgument);
              
                Label llname = (Label)e.Item.FindControl("llname");
                TextBox txtname = (TextBox)e.Item.FindControl("txtname");
                Label llname1 = (Label)e.Item.FindControl("llname1");
                TextBox txtname1 = (TextBox)e.Item.FindControl("txtname1");
                Label llname2 = (Label)e.Item.FindControl("llname2");
                TextBox txtname2 = (TextBox)e.Item.FindControl("txtname2");
                LinkButton LinkEMPEDIT = (LinkButton)e.Item.FindControl("LinkEMPEDIT");
                LinkButton LinkEditSave = (LinkButton)e.Item.FindControl("LinkEditSave");

                llname.Visible = false;
                txtname.Visible = true;
                llname1.Visible = false;
                txtname1.Visible = true;
                llname2.Visible = false;
                txtname2.Visible = true;
                LinkEMPEDIT.Visible = false;
                LinkEditSave.Visible = true;
            }
            if (e.CommandName == "LinkEditSave")
            {

                int CityID = Convert.ToInt32(e.CommandArgument);
                int CID = Convert.ToInt32(drpCountry.SelectedValue);
                int SID = Convert.ToInt32(DrpState.SelectedValue);
                Label llname = (Label)e.Item.FindControl("llname");
                TextBox txtname = (TextBox)e.Item.FindControl("txtname");
                Label llname1 = (Label)e.Item.FindControl("llname1");
                TextBox txtname1 = (TextBox)e.Item.FindControl("txtname1");
                Label llname2 = (Label)e.Item.FindControl("llname2");
                TextBox txtname2 = (TextBox)e.Item.FindControl("txtname2");

                string Str = "";
                Str += "update tblCityStatesCounty set CityEnglish='" + txtname.Text + "', CityArabic='" + txtname1.Text + "',CityOther='" + txtname2.Text + "' where COUNTRYID=" + CID + " and StateID=" + SID + ";";
                command2 = new SqlCommand(Str, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();
                llname.Visible = true;
                txtname.Visible = false;
                llname1.Visible = true;
                txtname1.Visible = false;
                llname2.Visible = true;
                txtname2.Visible = false;
                BindCity();
            }
            if (e.CommandName == "LinkEMPDELETE")
            {
                int CityID = Convert.ToInt32(e.CommandArgument);

                int CID = Convert.ToInt32(drpCountry.SelectedValue);
                int SID = Convert.ToInt32(DrpState.SelectedValue);

                Database.tblCityStatesCounty EditObj = DB.tblCityStatesCounties.Single(p => p.COUNTRYID == CID && p.StateID == SID && p.CityID == CityID);
                DB.tblCityStatesCounties.DeleteObject(EditObj);
                DB.SaveChanges();

                BindCity();

                string msg = "City save sucessfull...";
                string Function = "openModal('" + msg + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", Function, true);
            }
        }

        public void BindCity()
        {
            int CID = Convert.ToInt32(drpCountry.SelectedValue);
            int SID = Convert.ToInt32(DrpState.SelectedValue);
            listSocialMedia.DataSource = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == CID && p.StateID == SID);
            listSocialMedia.DataBind();
        }

        protected void btnsub_Click(object sender, EventArgs e)
        {
            BindCity();
        }


    }
}