using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.Master
{
    public partial class AMPMDAYkitchenPrint : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            
            if (!IsPostBack)
            {
                if (Session["AMPMKichenPrint"] != null)
                {
                    if (Request.QueryString["Head"] != null)
                    {
                        List<Database.temp_AMPMMeal> KitchenList = ((List<Database.temp_AMPMMeal>)Session["AMPMKichenPrint"]).ToList();
                        string HeaddSeprate = Request.QueryString["Head"].ToString();
                        if(HeaddSeprate == "Kitchen Preparation Report Delivery Wise")
                        {
                            //List<Database.temp_AMPMMeal> KitchenList = ((List<Database.temp_AMPMMeal>)Session["AMPMKichenPrint"]).ToList();
                            KitchenList = KitchenList.OrderBy(x => x.ExpectedDelDate).ThenBy(x => x.DeliverTime).ToList(); //.ThenByDescending(x => x.ExpectedDelDate).ToList();
                            List<Database.temp_AMPMMeal> Tempkitchen = new List<Database.temp_AMPMMeal>();
                            bool checkdate = false;
                            DateTime? checkDT = null;
                            int delevarytime = 0;
                            //string prod = "";                    
                            foreach (Database.temp_AMPMMeal itemmss in KitchenList)
                            {
                                if (checkDT == itemmss.ExpectedDelDate && delevarytime == itemmss.DeliverTime)
                                {
                                    checkdate = true;
                                }
                                else
                                {
                                    checkdate = false;
                                }
                                if (checkdate == false)
                                {
                                    if (DB.temp_AMPMMeal.Where(p => p.MyID == itemmss.MyID).Count() > 0)
                                    {
                                        Database.temp_AMPMMeal obj = DB.temp_AMPMMeal.Single(p => p.MyID == itemmss.MyID);
                                        checkDT = obj.ExpectedDelDate;
                                        delevarytime = Convert.ToInt32(obj.DeliverTime);
                                        //prod = obj.ProductName.ToString();
                                        Tempkitchen.Add(obj);
                                    }
                                }
                            }
                            ListView1.DataSource = Tempkitchen;//KitchenList;
                            ListView1.DataBind();
                            
                        }
                        else if(HeaddSeprate == "Kitchen Preparation Report Day Wise")
                        {
                            KitchenList = KitchenList.OrderBy(x => x.ExpectedDelDate).ThenBy(x => x.DeliverTime).ToList(); //.ThenByDescending(x => x.ExpectedDelDate).ToList();
                            List<Database.temp_AMPMMeal> Tempkitchen = new List<Database.temp_AMPMMeal>();
                            bool checkdate = false;
                            DateTime? checkDT = null;                                           
                            foreach (Database.temp_AMPMMeal itemmss in KitchenList)
                            {
                                if (checkDT == itemmss.ExpectedDelDate)
                                {
                                    checkdate = true;
                                }
                                else
                                {
                                    checkdate = false;
                                }
                                if (checkdate == false)
                                {
                                    if (DB.temp_AMPMMeal.Where(p => p.MyID == itemmss.MyID).Count() > 0)
                                    {
                                        Database.temp_AMPMMeal obj = DB.temp_AMPMMeal.Single(p => p.MyID == itemmss.MyID);
                                        checkDT = obj.ExpectedDelDate;
                                        Tempkitchen.Add(obj);
                                    }
                                }
                            }

                            ListView1.DataSource = Tempkitchen;//KitchenList;
                            ListView1.DataBind();                            
                        }
                    }                    
                    
                }
            }
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListView ListView2 = (ListView)e.Item.FindControl("ListView2");
            Label lblMyid = (Label)e.Item.FindControl("lblMyid");
            Label lblExpDate = (Label)e.Item.FindControl("lblExpDate");
            Label lbldelevarytime = (Label)e.Item.FindControl("lbldelevarytime");
            Label lblprod = (Label)e.Item.FindControl("lblprod");
            Panel PanelTR = (Panel)e.Item.FindControl("PanelTR");
            Label lblkitchH3 = (Label)e.Item.FindControl("lblkitchH3");            
            System.Web.UI.WebControls.Image HealtybarLogo = (System.Web.UI.WebControls.Image)e.Item.FindControl("HealtybarLogo");
            string Loggo = Classes.EcommAdminClass.Logo(TID);
            HealtybarLogo.ImageUrl = "../assets/" + Loggo;
            
            int Myiid = Convert.ToInt32(lblMyid.Text);
            DateTime expdate = Convert.ToDateTime(lblExpDate.Text);
            int delevarytime = Convert.ToInt32(lbldelevarytime.Text);
            string prod = lblprod.Text.ToString();

            if (Session["AMPMKichenPrint"] != null)
            {
                if (Request.QueryString["Head"] != null)
                {
                    string pnlhead = Request.QueryString["Head"].ToString();
                    if (pnlhead == "Kitchen Preparation Report Delivery Wise")
                    {
                        List<Database.temp_AMPMMeal> KitchenList2 = ((List<Database.temp_AMPMMeal>)Session["AMPMKichenPrint"]).ToList();
                        KitchenList2 = KitchenList2.Where(p => p.ExpectedDelDate == expdate && p.DeliverTime == delevarytime).ToList();
                        ListView2.DataSource = KitchenList2;
                        ListView2.DataBind();
                        lblkitchH3.Text = "Kitchen Preparation Delivery Wise";
                    }
                    else if(pnlhead == "Kitchen Preparation Report Day Wise")
                    {
                        List<Database.temp_AMPMMeal> KitchenList2 = ((List<Database.temp_AMPMMeal>)Session["AMPMKichenPrint"]).ToList();
                        KitchenList2 = KitchenList2.Where(p => p.ExpectedDelDate == expdate).ToList();
                        ListView2.DataSource = KitchenList2;
                        ListView2.DataBind();

                        PanelTR.Visible = false;
                        lblkitchH3.Text = "Kitchen Preparation Day Wise";
                    }
                }
                
            }
           
            //ListView2.DataSource = DB.temp_AMPMMeal.Where(p => p.MyID == Myiid);
            //ListView2.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ReportMst/AMPMDAY.aspx");
        }



    }
}