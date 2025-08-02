using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization;
using System.Web.Services;
using System.Web.Configuration;
using AjaxControlToolkit;
using System.Transactions;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Drawing;
using System.Collections;
using System.Net;
using System.IO;
using System.Web.UI.WebControls;

namespace Web.POS
{
    public partial class POSWinSales : System.Web.UI.Page
    {
        int TID = 0;

        string SOID = "";
        int Win_UserID = 0;
        string Win_UserName, Win_usertype, Win_Shopid = "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        CallEntities DB = new CallEntities();

        //optional dropdown 
        //private ICollection DRPGroup()
        //{
        //    DataTable dt = new DataTable();
        //    DataRow dr;
        //    dt.Columns.Add(new DataColumn("id", typeof(String)));
        //    dt.Columns.Add(new DataColumn("letter", typeof(String)));
        //    dt.Columns.Add(new DataColumn("letters", typeof(String)));

        //    List<Database.tblCityStatesCounty> lista = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == 126).ToList();
        //    foreach (Database.tblCityStatesCounty items in lista)
        //    {
        //        dr = dt.NewRow();
        //        dr[0] = items.CityEnglish;
        //        dr[1] = items.CityEnglish;
        //        dr[2] = DB.tblStates.Where(p => p.COUNTRYID == 126 && p.StateID == items.StateID).FirstOrDefault().MYNAME1;
        //        dt.Rows.Add(dr);
        //    }
        //    DataView dv = new DataView(dt);
        //    return dv;
        //}
        static private string Baseurl = ConfigurationManager.AppSettings["Baseurl"];
        static private string bearerToken = ConfigurationManager.AppSettings["bearerToken"];
        protected void Page_Load(object sender, EventArgs e)
        {
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";
            if (Session["USER"] == null)
            {
                Response.Redirect("/ACM/Login.aspx");
            }
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            sessionData();
            if (DB.Win_usermgt.Where(p => p.TenentID == TID).Count() > 0)
            {
                SOID = DB.Win_usermgt.Where(p => p.TenentID == TID).First().Shopid;
                HD.Attributes["class"] = "getshow";
            }
            else
            {
                HD.Attributes["class"] = "gethide";
                panelMsg.Visible = true;
                lblErreorMsg.Text = "You Are Not Pos User, Please Contact Admin";
            }

            int invoice = DB.Win_sales_item.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Win_sales_item.Where(p => p.TenentID == TID).Max(p => p.sales_id) + 1) : 1;
            invoiceID.Text = invoice.ToString();
            InvoiceNO();
            if (!IsPostBack)
            {
                BindCat();
                Bindprod("All");
                DropBind();
                txtSalesDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }
        }

        public void sessionData()
        {
            int TenentID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string Userid = ((USER_MST)Session["USER"]).LOGIN_ID;
            string WebPassword = ((USER_MST)Session["USER"]).PASSWORD;
            string WinPass = Classes.EncryptionHelper.Decrypt(WebPassword);

            List<Win_usermgt> ListUser = DB.Win_usermgt.Where(p => p.TenentID == TenentID && p.Username == Userid && p.password == WinPass).ToList();
            if (ListUser.Count() > 0)
            {
                Database.Win_usermgt objuser = ListUser.FirstOrDefault();
                Win_UserID = Convert.ToInt32(objuser.id);
                Win_UserName = objuser.Username;
                Win_usertype = objuser.usertype;
                Win_Shopid = objuser.Shopid;
            }
        }
        public void InvoiceNO()
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");

            //string sqlNO = "select * from sales_item where sales_time like '%" + date + "%' group by sales_id";
            List<Database.Win_sales_item> SaleList = DB.Win_sales_item.Where(p => p.sales_time.Contains(date)).GroupBy(p => p.sales_id).Select(p => p.FirstOrDefault()).ToList();

            if (SaleList.Count() > 0)
            {
                int count = SaleList.Count() + 1;
                int year = DateTime.Now.Year;
                string terminal = SOID;
                int day = DateTime.Now.DayOfYear;
                invoiceNo.Text = year + "/" + terminal + "/" + day + "/" + count;
            }
            else
            {
                int count = 1;
                int year = DateTime.Now.Year;
                string terminal = SOID;
                int day = DateTime.Now.DayOfYear;
                invoiceNo.Text = year + "/" + terminal + "/" + day + "/" + count;
            }
        }
        public void BindCat()
        {
            ListCategory.DataSource = DB.CAT_MST.Where(p => p.TenentID == TID && p.CAT_TYPE == "WEBSALE");
            ListCategory.DataBind();
            ViewState["Category"] = "All";

        }
        public void DropBind()
        {
            var orderway = from item in DB.Win_tbl_orderWay_Maintenance.Where(p => p.TenentID == TID && p.OrderWayID == "Walk In")
                           select new
                           {
                               Name = item.OrderWayID + " - " + item.Name1,
                               ID = item.OrderWayID + " - " + item.Name1
                           };
            drpOrderWay.DataSource = orderway;
            drpOrderWay.DataTextField = "Name";
            drpOrderWay.DataValueField = "ID";
            drpOrderWay.DataBind();

            List<Database.REFTABLE> ListPay = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Payment" && p.REFSUBTYPE == "Method" && p.SHORTNAME == "POS" && p.ACTIVE == "Y").ToList();
            drpPayBy.DataSource = ListPay;
            drpPayBy.DataTextField = "REFNAME1";
            drpPayBy.DataValueField = "REFID";
            drpPayBy.DataBind();
            drpPayBy.Items.Insert(0, new ListItem("-- Select --", "0"));

            //optional dropdown
            //this.DRPCity.DataTextField = "letter";
            //this.DRPCity.DataValueField = "id";
            //this.DRPCity.OptionGroupField = "letters";

            //this.DRPCity.DataSource = this.DRPGroup();
            //this.DRPCity.DataBind();
            DRPCity1.DataSource = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == 126);
            DRPCity1.DataTextField = "CityEnglish";
            DRPCity1.DataValueField = "CityEnglish";
            DRPCity1.DataBind();
            DRPCity1.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        public void Bindprod(string cat)
        {
            int AllowMinusQty = 0;
            if (DB.Win_tblsetupsalesh.Where(p => p.TenentID == TID && p.locationID == 1).Count() > 0)
                AllowMinusQty = Convert.ToInt32(DB.Win_tblsetupsalesh.Single(p => p.TenentID == TID && p.locationID == 1).AllowMinusQty);
            try
            {
                string sql = "";
                if (cat != "")
                {
                    if (cat == "All")
                    {
                        if (AllowMinusQty == 1)
                        {
                            if (Chkoutput.Checked == true)
                            {
                                sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID " +
                                      " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                      " where  RecipeType = 'Output' and Win_purchase.TenentID = " + TID + " and msrp > 0  order by Win_purchase.product_id,Win_purchase.product_name;";
                            }
                            else if (chkService.Checked == true)
                            {
                                sql = "SELECT Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                      " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                      " where RecipeType = 'Service' and Win_purchase.TenentID = " + TID + " and msrp > 0  order by Win_purchase.product_id,Win_purchase.product_name;";
                            }
                            else
                            {
                                sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                      " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                      " where Win_purchase.TenentID = " + TID + " and msrp > 0 order by Win_purchase.product_id,Win_purchase.product_name;";
                            }
                        }
                        else
                        {
                            if (Chkoutput.Checked == true)
                            {
                                sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode  FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                      " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                      " where  OnHand >= 1 and RecipeType = 'Output' and Win_purchase.TenentID = " + TID + " and msrp > 0  order by Win_purchase.product_id,Win_purchase.product_name;";
                            }
                            else if (chkService.Checked == true)
                            {
                                sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode  FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                      " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                      " where  OnHand >= 1 and RecipeType = 'Service' and Win_purchase.TenentID = " + TID + " and msrp > 0  order by Win_purchase.product_id,Win_purchase.product_name;";
                            }
                            else
                            {
                                sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode  FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                      " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                      " where  OnHand >= 1 and Win_purchase.TenentID = " + TID + " and msrp > 0  order by Win_purchase.product_id,Win_purchase.product_name;";
                            }
                        }
                    }
                    else
                    {
                        if (AllowMinusQty == 1)
                        {
                            if (Chkoutput.Checked == true)
                            {
                                sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode  FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                      " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                      " where (( product_name like '%" + cat + "%') OR ( product_name_Arabic like '%" + cat + "%') OR ( product_id like '%" + cat + "%'  ) OR ( CustItemCode like '%" + cat + "%' ) OR ( BarCode like '%" + cat + "%' )  OR  (category like '%" + cat + "%')) and RecipeType = 'Output' and Win_purchase.TenentID = " + TID + " and msrp > 0 order by Win_purchase.product_id,Win_purchase.product_name;";
                            }
                            else if (chkService.Checked == true)
                            {
                                sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode  FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                      " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                      " where (( product_name like '%" + cat + "%') OR ( product_name_Arabic like '%" + cat + "%') OR ( product_id like '%" + cat + "%'  ) OR ( CustItemCode like '%" + cat + "%' ) OR ( BarCode like '%" + cat + "%' )  OR  (category like '%" + cat + "%')) and RecipeType = 'Service' and Win_purchase.TenentID = " + TID + " and msrp > 0 order by Win_purchase.product_id,Win_purchase.product_name;";
                            }
                            else
                            {
                                sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode  FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                      " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                      " where (( product_name like '%" + cat + "%') OR ( product_name_Arabic like '%" + cat + "%') OR ( product_id like '%" + cat + "%'  ) OR ( CustItemCode like '%" + cat + "%' ) OR ( BarCode like '%" + cat + "%' )  OR  (category like '%" + cat + "%')) and Win_purchase.TenentID = " + TID + "  and msrp > 0 order by Win_purchase.product_id,Win_purchase.product_name;";
                            }
                        }
                        else
                        {
                            if (Chkoutput.Checked == true)
                            {
                                sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode  FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                      " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                      " where (( product_name like '%" + cat + "%') OR ( product_name_Arabic like '%" + cat + "%') OR ( product_id like '%" + cat + "%'  )  OR ( CustItemCode like '%" + cat + "%' ) OR ( BarCode like '%" + cat + "%' )  OR   (category like '%" + cat + "%' and   OnHand >= 1)) and RecipeType = 'Output' and Win_purchase.TenentID = " + TID + "  and msrp > 0  order by Win_purchase.product_id,Win_purchase.product_name;";
                            }
                            else if (chkService.Checked == true)
                            {
                                sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode  FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                      " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                      " where (( product_name like '%" + cat + "%') OR ( product_name_Arabic like '%" + cat + "%') OR ( product_id like '%" + cat + "%'  )  OR ( CustItemCode like '%" + cat + "%' ) OR ( BarCode like '%" + cat + "%' )  OR   (category like '%" + cat + "%' and   OnHand >= 1)) and RecipeType = 'Service' and Win_purchase.TenentID = " + TID + "  and msrp > 0  order by Win_purchase.product_id,Win_purchase.product_name;";
                            }
                            else
                            {
                                sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode  FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                      " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                      " where (( product_name like '%" + cat + "%') OR ( product_name_Arabic like '%" + cat + "%') OR ( product_id like '%" + cat + "%'  )  OR ( CustItemCode like '%" + cat + "%' ) OR ( BarCode like '%" + cat + "%' )  OR   (category like '%" + cat + "%' and   OnHand >= 1)) and Win_purchase.TenentID = " + TID + "  and msrp > 0  order by Win_purchase.product_id,Win_purchase.product_name;";
                            }
                        }
                    }
                }
                else
                {
                    if (AllowMinusQty == 1)
                    {
                        if (Chkoutput.Checked == true)
                        {
                            sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode  FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                  " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                  " where  (( product_name like '%" + cat + "%') OR ( product_name_Arabic like '%" + cat + "%') OR ( product_id like '%" + cat + "%'  ) OR ( CustItemCode like '%" + cat + "%' ) OR ( BarCode like '%" + cat + "%' )  OR  (category like '%" + cat + "%')) and RecipeType = 'Output'  and Win_purchase.TenentID = " + TID + " and msrp > 0  order by Win_purchase.product_id,Win_purchase.product_name;";
                        }
                        else if (chkService.Checked == true)
                        {
                            sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode  FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                  " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                  " where  (( product_name like '%" + cat + "%') OR ( product_name_Arabic like '%" + cat + "%') OR ( product_id like '%" + cat + "%'  ) OR ( CustItemCode like '%" + cat + "%' ) OR ( BarCode like '%" + cat + "%' )  OR  (category like '%" + cat + "%')) and RecipeType = 'Service'  and Win_purchase.TenentID = " + TID + " and msrp > 0  order by Win_purchase.product_id,Win_purchase.product_name;";
                        }
                        else
                        {
                            sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode  FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                  " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                  " where  (( product_name like '%" + cat + "%') OR ( product_name_Arabic like '%" + cat + "%') OR ( product_id like '%" + cat + "%'  ) OR ( CustItemCode like '%" + cat + "%' ) OR ( BarCode like '%" + cat + "%' )  OR  (category like '%" + cat + "%')) and Win_purchase.TenentID = " + TID + " and msrp > 0  order by Win_purchase.product_id,Win_purchase.product_name;";
                        }
                    }
                    else
                    {
                        if (Chkoutput.Checked == true)
                        {
                            sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                  " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                  " where  (( product_name like '%" + cat + "%' and OnHand >= 1) OR ( product_name_Arabic like '%" + cat + "%') OR ( product_id like '%" + cat + "%'  and OnHand >= 1) OR ( CustItemCode like '%" + cat + "%' ) OR ( BarCode like '" + cat + "%' ) OR (category like '%" + cat + "%' and   OnHand >= 1)) and RecipeType = 'Output' and Win_purchase.TenentID = 9000016 and msrp > 0 order by Win_purchase.product_id,Win_purchase.product_name;";
                        }
                        else if (chkService.Checked == true)
                        {
                            sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                  " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                  " where  (( product_name like '%" + cat + "%' and OnHand >= 1) OR ( product_name_Arabic like '%" + cat + "%') OR ( product_id like '%" + cat + "%'  and OnHand >= 1) OR ( CustItemCode like '%" + cat + "%' ) OR ( BarCode like '" + cat + "%' ) OR (category like '%" + cat + "%' and   OnHand >= 1)) and RecipeType = 'Service' and Win_purchase.TenentID = 9000016 and msrp > 0 order by Win_purchase.product_id,Win_purchase.product_name;";
                        }
                        else
                        {
                            sql = "SELECT  Shopid,product_name,product_name_Arabic,product_name_print,category,supplier,status,taxapply,imagename,product_id,UOMID,ICUOM.UOMNAME1 as 'UOMNAME',msrp,price,Deleted,OpQty,OnHand,QtyOut,QtyConsumed,QtyReserved,QtyRecived,minQty,MaxQty,Discount,Image,product_id,CustItemCode,BarCode FROM  Win_purchase INNER JOIN Win_tbl_item_uom_price ON Win_purchase.product_id = Win_tbl_item_uom_price.itemID and Win_purchase.TenentID = Win_tbl_item_uom_price.TenentID" +
                                  " INNER JOIN ICUOM ON Win_tbl_item_uom_price.UOMID = ICUOM.UOM and Win_tbl_item_uom_price.TenentID = ICUOM.TenentID " +
                                  " where  (( product_name like '%" + cat + "%' and OnHand >= 1) OR ( product_name_Arabic like '%" + cat + "%') OR ( product_id like '%" + cat + "%'  and OnHand >= 1) OR ( CustItemCode like '%" + cat + "%' ) OR ( BarCode like '" + cat + "%' ) OR (category like '%" + cat + "%' and   OnHand >= 1)) and Win_purchase.TenentID = 9000016 and msrp > 0 order by Win_purchase.product_id,Win_purchase.product_name;";
                        }
                    }
                }
                SqlCommand CMD1 = new SqlCommand(sql, cn);
                SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
                DataSet ds = new DataSet();
                ADB.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (chkWithImage.Checked == true)
                {
                    Listview2.DataSource = dt;
                    Listview2.DataBind();
                }
                else
                {
                    Listview3.DataSource = dt;
                    Listview3.DataBind();
                }
                if (ViewState["Category"] != null)
                {
                    int ViewCat = Convert.ToInt32(ViewState["Category"]);
                    if (DB.CAT_MST.Where(p => p.TenentID == TID && p.CATID == ViewCat).Count() > 0)
                    {
                        string CatName = DB.CAT_MST.Single(p => p.TenentID == TID && p.CATID == ViewCat).CAT_NAME1;
                        lblCategory.Text = CatName + "(" + dt.Rows.Count + ")";
                    }
                }
                //lblCategory.Text = ViewState["Category"].ToString() + "(" + dt.Rows.Count + ")";

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCategoryall_Click(object sender, EventArgs e)
        {
            ViewState["Category"] = "All";
            Bindprod("All");

        }

        protected void ListCategory_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "DisplayCat")
            {
                string[] ID = e.CommandArgument.ToString().Split('-');
                int CatID = Convert.ToInt32(ID[0]);
                string Catname = ID[1].ToString();
                string Catidd = CatID.ToString();
                ViewState["Category"] = Catidd;
                Bindprod(Catidd);

            }

        }

        protected void btnProdsearch_Click(object sender, EventArgs e)
        {
            string Searchh = txtBarCode.Text.ToString().Trim();
            Bindprod(Searchh);

        }

        protected void Listview2_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            List<TempProduct> Tlist = new List<TempProduct>();
            if (e.CommandName == "LnkProdAdd")
            {
                string[] ID = e.CommandArgument.ToString().Split('~');
                string Prodname = ID[0].ToString();
                int Prodid = Convert.ToInt32(ID[1]);
                string UOMIDd = ID[2].ToString();

                Database.Win_purchase purobj = DB.Win_purchase.Single(p => p.TenentID == TID && p.product_id == Prodid);
                Database.Win_tbl_item_uom_price uomobj = DB.Win_tbl_item_uom_price.Single(p => p.TenentID == TID && p.itemID == Prodid && p.UOMID == UOMIDd);
                int UMID = Convert.ToInt32(uomobj.UOMID); // change by dipak
                //Win_purchase.product_id = Win_tbl_item_uom_price.itemID

                TempProduct obj = new TempProduct();
                if (ViewState["InvoiceProd"] == null)
                {
                    obj.Tenent = purobj.TenentID;
                    obj.product_id = purobj.product_id;
                    obj.UOMID = Convert.ToInt32(uomobj.UOMID); // change by dipak
                    obj.UOMNAME = DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == UMID).Count() > 0 ? DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == UMID).UOMNAME1 : "Not Name"; // change by dipak
                    obj.Shopid = purobj.Shopid;
                    obj.product_name = purobj.product_name;
                    obj.product_name_Arabic = purobj.product_name_Arabic;
                    obj.product_name_print = purobj.product_name_print;
                    obj.category = purobj.category;
                    obj.supplier = purobj.supplier;
                    obj.taxapply = Convert.ToInt32(purobj.taxapply);
                    obj.imagename = purobj.imagename;
                    obj.status = Convert.ToInt32(purobj.status);
                    obj.price = Convert.ToDecimal(uomobj.price);//price
                    obj.msrp = Convert.ToDecimal(uomobj.msrp);//total
                    obj.OpQty = Convert.ToInt32(1);//qty
                    obj.OnHand = Convert.ToInt32(uomobj.OnHand);
                    obj.QtyOut = Convert.ToInt32(uomobj.QtyOut);
                    obj.QtyConsumed = Convert.ToInt32(uomobj.QtyConsumed);
                    obj.QtyReserved = Convert.ToInt32(uomobj.QtyReserved);
                    obj.QtyRecived = Convert.ToInt32(uomobj.QtyRecived);
                    obj.minQty = Convert.ToInt32(uomobj.minQty);
                    obj.MaxQty = Convert.ToInt32(uomobj.MaxQty);
                    decimal qty = Convert.ToInt32(1);//qty
                    decimal Dis = Convert.ToDecimal(uomobj.Discount);
                    decimal msrp = (Convert.ToDecimal(uomobj.msrp) * qty);//total
                    decimal Fdis = ((msrp * Dis) / 100);
                    obj.Total = (msrp * qty);
                    obj.Discount = Math.Round(Fdis, 3);
                    obj.CustItemCode = purobj.CustItemCode;
                    obj.BarCode = purobj.BarCode;
                    obj.BatchNo = "";

                    Tlist.Add(obj);
                    ViewState["InvoiceProd"] = Tlist;

                    bool flag1 = IsPerishable(TID, Prodid);
                    if (flag1 == true)
                    {
                        lblPRodIDper.Text = Prodid.ToString();
                        lblProdNamePEr.Text = purobj.product_name;
                        lblUOMName.Text = uomobj.UOMID;

                        string NotDisplay = SplitBatch(obj.BatchNo);// selectBatch.Text;                        
                        int uom = getuomid(TID, uomobj.UOMID);
                        string query = "select * from ICIT_BR_Perishable where TenentID=" + TID + " and MyProdID =" + Prodid + "  and UOM=" + uom + " and OnHand>=1 and BatchNo not in ('" + NotDisplay + "') ";
                        DataTable dtquery = DataCon.GetDataTable(query);
                        if (dtquery != null)
                        {
                            if (dtquery.Rows.Count > 0)
                            {
                                ListView4.DataSource = dtquery;
                                ListView4.DataBind();
                                ModalPopupExtender8.Show();
                            }
                            else
                            {
                                ModalPopupExtender8.Hide();
                            }
                        }
                        else
                        {
                            ModalPopupExtender8.Hide();
                        }
                    }
                }
                else
                {
                    Tlist = ((List<TempProduct>)ViewState["InvoiceProd"]).ToList();
                    if (Tlist.Where(p => p.Tenent == TID && p.product_id == purobj.product_id && p.UOMID == UMID).Count() > 0) // change by dipak
                    {
                        //string BatchNo = Tlist.Single(p => p.Tenent == TID && p.product_id == purobj.product_id && p.UOMID == UMID && p.OnHand >= p.QtyOut).BatchNo; // change by dipak
                        string BatchNo = "";
                        CalcDirectProd(purobj.product_id, UMID, BatchNo);  // change by dipak
                    }
                    else
                    {
                        obj.Tenent = purobj.TenentID;
                        obj.product_id = purobj.product_id;
                        obj.UOMID = Convert.ToInt32(uomobj.UOMID); // change by dipak
                        obj.UOMNAME = DB.ICUOMs.Where(p => p.TenentID == TID && p.UOM == UMID).Count() > 0 ? DB.ICUOMs.Single(p => p.TenentID == TID && p.UOM == UMID).UOMNAME1 : "Not Name"; // change by dipak
                        obj.Shopid = purobj.Shopid;
                        obj.product_name = purobj.product_name;
                        obj.product_name_Arabic = purobj.product_name_Arabic;
                        obj.product_name_print = purobj.product_name_print;
                        obj.category = purobj.category;
                        obj.supplier = purobj.supplier;
                        obj.taxapply = Convert.ToInt32(purobj.taxapply);
                        obj.imagename = purobj.imagename;
                        obj.status = Convert.ToInt32(purobj.status);
                        obj.price = Convert.ToDecimal(uomobj.price);//price
                        obj.msrp = Convert.ToDecimal(uomobj.msrp);//total
                        obj.OpQty = Convert.ToInt32(1);//qty
                        obj.OnHand = Convert.ToInt32(uomobj.OnHand);
                        obj.QtyOut = Convert.ToInt32(uomobj.QtyOut);
                        obj.QtyConsumed = Convert.ToInt32(uomobj.QtyConsumed);
                        obj.QtyReserved = Convert.ToInt32(uomobj.QtyReserved);
                        obj.QtyRecived = Convert.ToInt32(uomobj.QtyRecived);
                        obj.minQty = Convert.ToInt32(uomobj.minQty);
                        obj.MaxQty = Convert.ToInt32(uomobj.MaxQty);
                        decimal qty = Convert.ToInt32(1);//qty
                        decimal Dis = Convert.ToDecimal(uomobj.Discount);
                        decimal msrp = (Convert.ToDecimal(uomobj.msrp) * qty);//total
                        decimal Fdis = ((msrp * Dis) / 100);
                        obj.Total = (msrp * qty);
                        obj.Discount = Math.Round(Fdis, 3);
                        obj.CustItemCode = purobj.CustItemCode;
                        obj.BarCode = purobj.BarCode;
                        obj.BatchNo = "";

                        Tlist.Add(obj);
                        ViewState["InvoiceProd"] = Tlist;

                        bool flag1 = IsPerishable(TID, Prodid);
                        if (flag1 == true)
                        {
                            lblPRodIDper.Text = Prodid.ToString();
                            lblProdNamePEr.Text = purobj.product_name;
                            lblUOMName.Text = uomobj.UOMID;

                            string NotDisplay = SplitBatch(obj.BatchNo);// selectBatch.Text;                        
                            int uom = getuomid(TID, uomobj.UOMID);
                            string query = "select * from ICIT_BR_Perishable where TenentID=" + TID + " and MyProdID =" + Prodid + "  and UOM=" + uom + " and OnHand>=1 and BatchNo not in ('" + NotDisplay + "') ";
                            DataTable dtquery = DataCon.GetDataTable(query);
                            if (dtquery != null)
                            {
                                if (dtquery.Rows.Count > 0)
                                {
                                    ListView4.DataSource = dtquery;
                                    ListView4.DataBind();
                                    ModalPopupExtender8.Show();
                                }
                                else
                                {
                                    ModalPopupExtender8.Hide();
                                }
                            }
                            else
                            {
                                ModalPopupExtender8.Hide();
                            }
                        }
                    }
                }


                Listview1.DataSource = Tlist;
                Listview1.DataBind();
                decimal total = Convert.ToInt32(Tlist.Sum(m => m.msrp));
                FinalCalc();

            }
        }
        protected void Listview1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblpdiscound = (Label)e.Item.FindControl("lblpdiscound");
            Label lblptaxapply = (Label)e.Item.FindControl("lblptaxapply");

            lblTax.Text = lblptaxapply.Text.ToString();

        }

        public static int getuomid(int TenentID, string UOMNAME1)
        {
            int UOM = 0;
            UOMNAME1 = UOMNAME1.Trim();
            string sql12 = " select * from ICUOM where TenentID = " + TenentID + " and UOMNAME1 = '" + UOMNAME1 + "' ";
            DataTable dt1 = DataCon.GetDataTable(sql12);
            if (dt1.Rows.Count > 0)
            {
                UOM = Convert.ToInt32(dt1.Rows[0]["UOM"]);
            }
            return UOM;
        }

        public static string getuomName(int TenentID, int UOM)
        {
            string UOMNAME1 = "";
            string sql12 = " select * from ICUOM where TenentID = " + TenentID + " and UOM = " + UOM + " ";
            DataTable dt1 = DataCon.GetDataTable(sql12);
            if (dt1.Rows.Count > 0)
            {
                UOMNAME1 = dt1.Rows[0]["UOMNAME1"].ToString();
            }
            return UOMNAME1;
        }
        public string SplitBatch(string selectBatch)
        {
            string Temp = "";
            if (selectBatch != null && selectBatch != "")
            {

                string[] strSplit = selectBatch.Split(',');

                int Lenth = strSplit.Length;

                for (int i = 0; i < Lenth; i++)
                {
                    string Temp1 = "'" + strSplit[i] + "'";
                    Temp = Temp + "," + Temp1;
                }

                Temp = Temp.Trim();
                Temp = Temp.TrimStart(',');
                Temp = Temp.TrimEnd(',');
            }
            return Temp;
        }
        public static bool IsPerishable(int TenentID, int ProdID)
        {
            string sql12 = " select * from Win_purchase where TenentID = " + TenentID + " and product_id = '" + ProdID + "' ";
            DataTable dt1 = DataCon.GetDataTable(sql12);
            if (dt1.Rows.Count > 0)
            {
                int Perishable = Convert.ToInt32(dt1.Rows[0]["IsPerishable"]);
                if (Perishable == 1)
                    return true;
                else
                    return false;

            }
            else
            {
                return false;
            }
        }
        public decimal GetDisRate(int itemID, string UOMID)
        {
            decimal Desrate = 0;
            List<Database.Win_tbl_item_uom_price> List = DB.Win_tbl_item_uom_price.Where(p => p.TenentID == TID && p.itemID == itemID && p.UOMID == UOMID).ToList();
            if (List.Count() > 0)
            {
                Desrate = Convert.ToDecimal(List.FirstOrDefault().Discount);
            }

            return Desrate;
        }

        public void CalcDirectProd(int pid, int uomid, string BatchNo) //Change by dipak
        {
            List<TempProduct> Tlist = new List<TempProduct>();
            Tlist = ((List<TempProduct>)ViewState["InvoiceProd"]).ToList();
            TempProduct Calc = Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo);

            decimal msrp = Convert.ToDecimal(Calc.msrp);
            int QTY = Convert.ToInt32(Calc.OpQty);
            QTY = QTY + 1;
            decimal total = Convert.ToDecimal(msrp * QTY);
            string SUMID = uomid.ToString();
            decimal DisRate = GetDisRate(pid, SUMID);

            decimal Fdis = ((msrp * DisRate) / 100);

            Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).msrp = msrp;
            Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Total = total;
            Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).OpQty = QTY;
            Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Discount = Fdis;

            ViewState["InvoiceProd"] = Tlist;
            Listview1.DataSource = Tlist;
            Listview1.DataBind();
            FinalCalc();
        }

        protected void Listview1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            List<TempProduct> Tlist = new List<TempProduct>();
            Tlist = ((List<TempProduct>)ViewState["InvoiceProd"]).ToList();
            if (e.CommandName == "ProdPlus")
            {
                string[] ID = e.CommandArgument.ToString().Split('-');
                int pid = Convert.ToInt32(ID[0]);
                int uomid = Convert.ToInt32(ID[1]);  // change by dipak
                string BatchNo = ID[2].ToString();
                TempProduct Calc = Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo);  // change by dipak

                int Onhand = Calc.OnHand;

                decimal msrp = Convert.ToDecimal(Calc.msrp);
                int QTY = Convert.ToInt32(Calc.OpQty);
                QTY = QTY + 1;
                decimal total = Convert.ToDecimal(msrp * QTY);
                string SUMID = uomid.ToString();  // change by dipak
                decimal DisRate = GetDisRate(pid, SUMID);  // change by dipak

                decimal Fdis = ((total * DisRate) / 100);

                if (Onhand != 0)
                {
                    if (QTY <= Onhand)
                    {
                        Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).msrp = msrp;  // change by dipak
                        Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).OpQty = QTY;  // change by dipak
                        Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Total = total;  // change by dipak
                        Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Discount = Fdis;  // change by dipak

                        ViewState["InvoiceProd"] = Tlist;
                        Listview1.DataSource = Tlist;
                        Listview1.DataBind();
                    }
                    else
                    {
                        bool flag1 = IsPerishable(TID, pid);
                        if (flag1 == true)
                        {
                            lblPRodIDper.Text = pid.ToString();
                            lblProdNamePEr.Text = Calc.product_name;
                            lblUOMName.Text = Calc.UOMNAME;
                            string Batch = "";
                            List<TempProduct> Tlist1 = Tlist.Where(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid).ToList();
                            foreach (TempProduct item in Tlist1)
                            {
                                Batch += item.BatchNo + ",";
                            }
                            Batch = Batch.Trim();
                            Batch = Batch.TrimStart(',');
                            Batch = Batch.TrimEnd(',');
                            string NotDisplay = SplitBatch(Batch);// selectBatch.Text;                        
                            int uom = Calc.UOMID;//getuomid(TID, Calc.UOM);  // change by dipak
                            string query = "select * from ICIT_BR_Perishable where TenentID=" + TID + " and MyProdID =" + pid + "  and UOM=" + uom + " and OnHand>=1 and BatchNo not in (" + NotDisplay + ") ";
                            DataTable dtquery = DataCon.GetDataTable(query);
                            if (dtquery != null)
                            {
                                if (dtquery.Rows.Count > 0)
                                {
                                    ListView4.DataSource = dtquery;
                                    ListView4.DataBind();
                                    ModalPopupExtender8.Show();
                                }
                                else
                                {
                                    ModalPopupExtender8.Hide();
                                }
                            }
                            else
                            {
                                ModalPopupExtender8.Hide();
                            }
                        }
                        else
                        {
                            Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).msrp = msrp;  // change by dipak
                            Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).OpQty = QTY;  // change by dipak
                            Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Total = total;  // change by dipak
                            Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Discount = Fdis;  // change by dipak

                            ViewState["InvoiceProd"] = Tlist;
                            Listview1.DataSource = Tlist;
                            Listview1.DataBind();
                        }
                    }
                }
                else
                {
                    Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).msrp = msrp;
                    Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).OpQty = QTY;
                    Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Total = total;
                    Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Discount = Fdis;

                    ViewState["InvoiceProd"] = Tlist;
                    Listview1.DataSource = Tlist;
                    Listview1.DataBind();
                }

                FinalCalc();
            }
            if (e.CommandName == "ProdMinus")
            {
                string[] ID = e.CommandArgument.ToString().Split('-');
                int pid = Convert.ToInt32(ID[0]);
                int uomid = Convert.ToInt32(ID[1]);  // change by dipak
                string BatchNo = ID[2].ToString();
                TempProduct Calc = Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo);

                decimal msrp = Convert.ToDecimal(Calc.msrp);
                int QTY = Convert.ToInt32(Calc.OpQty);
                if (QTY > 1)
                {
                    QTY = QTY - 1;
                    decimal total = Convert.ToDecimal(msrp * QTY);
                    string SUMID = uomid.ToString();  // change by dipak
                    decimal DisRate = GetDisRate(pid, SUMID);// change by dipak

                    decimal Fdis = ((total * DisRate) / 100);

                    Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).msrp = msrp;  // change by dipak
                    Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Total = total;  // change by dipak
                    Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).OpQty = QTY;  // change by dipak
                    Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Discount = Fdis;  // change by dipak

                    ViewState["InvoiceProd"] = Tlist;
                    Listview1.DataSource = Tlist;
                    Listview1.DataBind();

                    FinalCalc();
                }
            }
            if (e.CommandName == "ProdRemove")
            {
                string[] ID = e.CommandArgument.ToString().Split('-');
                int pid = Convert.ToInt32(ID[0]);
                int uomid = Convert.ToInt32(ID[1]);  // change by dipak
                string BatchNo = ID[2].ToString();
                TempProduct Calc = Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo);  // change by dipak

                Tlist.Remove(Calc);
                ViewState["InvoiceProd"] = Tlist;
                Listview1.DataSource = Tlist;
                Listview1.DataBind();

                FinalCalc();
                //final total work pending
            }
        }
        public void FinalCalc()
        {
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";

            List<TempProduct> Tlist = new List<TempProduct>();
            Tlist = ((List<TempProduct>)ViewState["InvoiceProd"]).ToList();

            decimal FTotal = Convert.ToDecimal(Tlist.Sum(p => p.Total));
            FTotall.Text = FTotal.ToString();
            FsubTotal.Text = FTotal.ToString();
            decimal DisInternal = Convert.ToDecimal(Tlist.Sum(p => p.Discount));
            DisInternal = Math.Round(DisInternal, 3);
            lblDiscount.Text = DisInternal.ToString();

            if (Tlist.Count() > 0)
            {
                try
                {
                    string str = "";
                    decimal DICUNT = 0;
                    decimal DICUNTOTAL = 0;
                    decimal InvoiceDes = 0;

                    if (txtDisPercent.Text.Contains("%"))
                    {
                        str = txtDisPercent.Text.Replace('%', ' ');

                        decimal Ftotal = Convert.ToDecimal(FTotal);
                        DICUNT = Convert.ToDecimal(str);
                        DICUNTOTAL = Ftotal - (Ftotal * (DICUNT / 100));
                        InvoiceDes = (Ftotal * (DICUNT / 100));
                        DICUNTOTAL = DICUNTOTAL - DisInternal;

                        string roundsubtotal = Math.Round(DICUNTOTAL, 3).ToString();
                        lblInvoiceDisPer.Text = DICUNT.ToString();
                        lblinvoiceDis.Text = Math.Round(InvoiceDes, 3).ToString();
                        FsubTotal.Text = roundsubtotal;
                        lblPayable.Text = roundsubtotal;
                        txtcashReceived.Text = roundsubtotal;
                        lblTotalpayableAmtPY.Text = roundsubtotal;
                        txtPaidAmount.Text = roundsubtotal;
                        lblChangeAMT.Text = "0.000";
                    }
                    else
                    {
                        str = txtDisPercent.Text;
                        decimal Ftotal = Convert.ToDecimal(FTotal);
                        DICUNT = Convert.ToDecimal(str);
                        decimal Disper = (Ftotal * DICUNT) / 100;
                        DICUNTOTAL = Ftotal - DICUNT;
                        InvoiceDes = DICUNT;
                        DICUNTOTAL = DICUNTOTAL - DisInternal;

                        string roundsubtotal = Math.Round(DICUNTOTAL, 3).ToString();
                        lblInvoiceDisPer.Text = Disper.ToString();
                        lblinvoiceDis.Text = Math.Round(InvoiceDes, 3).ToString();
                        FsubTotal.Text = roundsubtotal;
                        lblPayable.Text = roundsubtotal;
                        txtcashReceived.Text = roundsubtotal;
                        lblTotalpayableAmtPY.Text = roundsubtotal;
                        txtPaidAmount.Text = roundsubtotal;
                        lblChangeAMT.Text = "0.000";
                    }
                }
                catch
                {
                    panelMsg.Visible = true;
                    lblErreorMsg.Text = "insert Valid Discount";
                    return;
                }
            }
            else
            {
                Cancel();
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }
        public void Cancel()
        {
            List<TempProduct> Tlist = new List<TempProduct>();
            FTotall.Text = "0";
            FsubTotal.Text = "0";
            //txtSidPoint.Text = "0";
            txtDisPercent.Text = "0";
            lblPayable.Text = "0";
            txtcashReceived.Text = "0";
            lblChangeAMT.Text = "0";

            Listview1.DataSource = Tlist;
            Listview1.DataBind();
        }
        protected void txtDisPercent_TextChanged(object sender, EventArgs e)
        {
            //txtSidPoint.Text = "0";
            decimal total = Convert.ToDecimal(FTotall.Text);
            FinalCalc();
        }

        protected void txtSidPoint_TextChanged(object sender, EventArgs e)
        {
            txtDisPercent.Text = "0";
            decimal total = Convert.ToDecimal(FTotall.Text);
            FinalCalc();
        }

        protected void txtcashReceived_TextChanged(object sender, EventArgs e)
        {
            decimal CashReceiv = Convert.ToDecimal(txtcashReceived.Text);
            decimal Payable = Convert.ToDecimal(lblPayable.Text);
            if (CashReceiv >= Payable)
            {
                decimal amtchang = (CashReceiv - Payable);
                lblChangeAMT.Text = amtchang.ToString();
            }
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static string[] GetCustomer(string prefixText, int count)
        {
            int TID = Convert.ToInt32(((USER_MST)HttpContext.Current.Session["USER"]).TenentID);
            string conStr;
            conStr = WebConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString;
            string sqlQuery = "SELECT [Name]+' - '+ [Phone]+' - '+ [EmailAddress],[ID] FROM [Win_tbl_customer] WHERE TenentID='" + TID + "' and PeopleType='Customer' and (Name like '%' + @COMPNAME  + '%' or EmailAddress like '%' + @COMPNAME  + '%' or Phone like '%' + @COMPNAME  + '%')";
            //string sqlQuery = "SELECT [COMPNAME1]+' - '+MOBPHONE,[COMPID] FROM [TBLCOMPANYSETUP] WHERE TenentID='" + TID + "' and BUYER = 'true' and (COMPNAME1 like @COMPNAME  + '%' or COMPNAME2 like @COMPNAME  + '%' or COMPNAME3 like @COMPNAME  + '%' or MOBPHONE like @COMPNAME  + '%')";
            SqlConnection conn = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@COMPNAME", prefixText);
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void btnCash_Click(object sender, EventArgs e)
        {
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";

            if (HiddenField3.Value == null || HiddenField3.Value == "")
            {
                //panelMsg.Visible = true;
                //lblErreorMsg.Text = "Please Select Customer";
                //return;
                ModalPopupExtender1.Show();
                return;
            }

            List<TempProduct> Tlist = new List<TempProduct>();
            Tlist = ((List<TempProduct>)ViewState["InvoiceProd"]).ToList();

            List<SalesItemList> ListSend = new List<SalesItemList>();

            foreach (TempProduct item in Tlist)
            {
                SalesItemList Obj = new SalesItemList();
                Obj.Items_Name = item.product_name;
                Obj.UOMNAME = item.UOMNAME; // change by dipak to UOM
                Obj.UOMID = item.UOMID; // change by dipak to UOM
                decimal Price = Convert.ToInt32(item.msrp);
                Price = Math.Round(Price, 3);
                Obj.Price = Price;
                Obj.Qty = Convert.ToDecimal(item.OpQty);
                decimal Total = Convert.ToInt32(item.Total);
                Total = Math.Round(Total, 3);
                Obj.Total = Total;
                Obj.itemID = item.product_id.ToString();
                Obj.CustItemCode = item.CustItemCode;
                ListSend.Add(Obj);
            }

            CashSave sendObj1 = new CashSave();
            sendObj1.TenentID = TID;
            sendObj1.Shopid = Win_Shopid;
            sendObj1.Userid = Win_UserID;
            sendObj1.UserName = Win_UserName;
            sendObj1.TotalPayable = lblPayable.Text != "" ? Convert.ToDecimal(lblPayable.Text) : 0;
            sendObj1.TotalCashRecived = txtcashReceived.Text != "" ? Convert.ToDecimal(txtcashReceived.Text) : 0;
            sendObj1.OrderWay = drpOrderWay.SelectedItem.ToString() != "" ? drpOrderWay.SelectedItem.ToString() : "Walk In - Walk In";
            string Customer = txtCustomer1.Text != "" ? txtCustomer1.Text : "Cash";
            if (Customer.Contains('-'))
            {
                Customer = txtCustomer1.Text != "" ? txtCustomer1.Text.ToString().Split('-')[0].Trim() : "Cash";
            }
            sendObj1.Customer = Customer;
            int C_id = HiddenField3.Value != null && HiddenField3.Value != "" ? Convert.ToInt32(HiddenField3.Value) : 1;
            sendObj1.CustomerID = C_id != 0 ? C_id : 1;
            sendObj1.ShiftID = 1;
            decimal Predis = Convert.ToDecimal(lblDiscount.Text);
            decimal Invoidis = Convert.ToDecimal(lblinvoiceDis.Text);
            decimal Fdis = Predis + Invoidis;
            sendObj1.DiscountTotaloverall = Fdis.ToString();
            sendObj1.overalldisRate = lblInvoiceDisPer.Text != "" ? lblInvoiceDisPer.Text : "0";
            sendObj1.Delivery_Cahrge = lblDelivery.Text != "" ? lblDelivery.Text : "0";
            sendObj1.dgrvSalesItemList = ListSend;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/PerformCashTransaction", sendObj1).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<Response>(EmpResponse);
                    string MSg = rootobject.data.ToString();
                    int sales_id = GetSalesID(MSg);
                    Response.Redirect("POSPrintRpt.aspx?sales_id=" + sales_id);

                    Cancel();
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                else
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<Response>(EmpResponse);
                    string MSg = rootobject.data.ToString();
                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('" + MSg + "');", true);
                }
            }
        }

        public int GetSalesID(string InvoiceNO)
        {
            string sql3 = "select * from Win_sales_payment where TenentID=" + TID + " and InvoiceNO='" + InvoiceNO + "' ";
            DataTable dt3 = DataCon.GetDataTable(sql3);
            int Salesid = 0;
            if (dt3.Rows.Count > 0)
            {
                Salesid = Convert.ToInt32(dt3.Rows[0]["sales_id"]);
            }
            else
            {
                Salesid = 0;
            }
            return Salesid;
        }


        //public void oldsave()
        //{
        //    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //    {

        //        if (lblPayable.Text == "0" || lblPayable.Text == "0.0" || lblPayable.Text == "0.00" || lblPayable.Text == "0.000")
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Sorry ! You don't have enough product in Item cart \n  Please Add to cart');", true);
        //            return;
        //        }
        //        else
        //        {
        //            decimal TotalPayable = Convert.ToDecimal(lblPayable.Text);
        //            decimal Payamount = Convert.ToDecimal(txtcashReceived.Text);
        //            decimal ChangeAmount = 0;
        //            decimal dueAmount = 0;


        //            if (TotalPayable > Payamount)
        //            {
        //                decimal due = TotalPayable - Payamount;
        //                dueAmount = due;
        //            }
        //            else if (Payamount > TotalPayable)
        //            {
        //                decimal change = Payamount - TotalPayable;
        //                ChangeAmount = change;
        //            }
        //            else
        //            {
        //                dueAmount = 0;
        //                ChangeAmount = 0;

        //            }
        //            int cid = 0;
        //            string Customer = "";
        //            if (!String.IsNullOrEmpty(txtCustomer1.Text))
        //            {
        //                string[] Cname = txtCustomer1.Text.ToString().Split('-');
        //                string Nmae = Cname[0];
        //                if (DB.Win_tbl_customer.Where(p => p.TenentID == TID && p.Name == Nmae).Count() > 0)
        //                {
        //                    cid = Convert.ToInt32(DB.Win_tbl_customer.Single(p => p.TenentID == TID && p.Name == Nmae).ID);
        //                    Customer = Nmae;
        //                }
        //                else
        //                {
        //                    cid = Convert.ToInt32(DB.Win_tbl_customer.Single(p => p.TenentID == TID && p.Name == "Gest").ID);//Guest
        //                    Customer = Nmae;
        //                }
        //            }
        //            else
        //            {
        //                cid = Convert.ToInt32(DB.Win_tbl_customer.Single(p => p.TenentID == TID && p.Name == "Gest").ID);//Guest
        //                Customer = DB.Win_tbl_customer.Single(p => p.TenentID == TID && p.Name == "Gest").Name;
        //            }
        //            payment_item(Payamount, ChangeAmount, dueAmount, "Cash", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString(), cid.ToString(), Customer, "Success");

        //            sales_item(DateTime.Now.ToString("yyyy-MM-dd").ToString(), "Paid-send to Kitchen", 0, "PriPaid", Customer, cid);

        //        }
        //        scope.Complete();
        //    }
        //}
        //public void payment_item(decimal payamount, decimal changeamount, decimal dueamount, string salestype, string salesdate, string custid, string Comment, string PaymentStutas)
        //{
        //    string UploadDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
        //    DateTime sales_date = Convert.ToDateTime(DateTime.Now);
        //    salesdate = sales_date.ToString("yyyy-MM-dd HH:mm:ss");
        //    int ID = Convert.ToInt32(invoiceID.Text);
        //    Database.Win_sales_payment pobj = new Database.Win_sales_payment();
        //    pobj.TenentID = TID;
        //    pobj.ID = DB.Win_sales_payment.Where(p => p.TenentID == TID && p.sales_id == ID).Count() > 0 ? Convert.ToInt32(DB.Win_sales_payment.Where(p => p.TenentID == TID && p.sales_id == ID).Max(p => p.ID) + 1) : 1;
        //    pobj.sales_id = ID;
        //    pobj.return_id = 0;
        //    pobj.payment_type = "Cash";
        //    pobj.payment_amount = payamount;
        //    pobj.change_amount = changeamount;
        //    pobj.due_amount = dueamount;
        //    //pobj.dis = Convert.ToDecimal(txtSidPoint.Text);
        //    pobj.vat = Convert.ToDecimal(lblTax.Text);
        //    pobj.sales_time = salesdate;
        //    pobj.c_id = custid;
        //    pobj.emp_id = DB.Win_usermgt.Single(p => p.TenentID == TID).Username;
        //    pobj.comment = Comment;
        //    pobj.TrxType = "POS";
        //    pobj.Shopid = DB.Win_usermgt.Single(p => p.TenentID == TID).Shopid;
        //    pobj.ovdisrate = Convert.ToDecimal(txtDisPercent.Text);
        //    pobj.vaterate = Convert.ToDecimal(lblTaxPercent.Text);
        //    pobj.InvoiceNO = invoiceNo.Text;
        //    pobj.Customer = Comment;
        //    pobj.Uploadby = DB.Win_usermgt.Single(p => p.TenentID == TID).Username;
        //    pobj.UploadDate = DateTime.Now;
        //    pobj.SynID = null;
        //    pobj.Delivery_Cahrge = Convert.ToDecimal(lblDelivery.Text);
        //    pobj.PaymentStutas = PaymentStutas;
        //    DB.Win_sales_payment.AddObject(pobj);
        //    DB.SaveChanges();
        //}

        protected void btnCOD_Click(object sender, EventArgs e)
        {
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";

            if (HiddenField3.Value == null || HiddenField3.Value == "")
            {
                //panelMsg.Visible = true;
                //lblErreorMsg.Text = "Please Select Customer";
                //return;
                ModalPopupExtender1.Show();
                return;
            }

            List<TempProduct> Tlist = new List<TempProduct>();
            Tlist = ((List<TempProduct>)ViewState["InvoiceProd"]).ToList();

            List<SalesItemList> ListSend = new List<SalesItemList>();

            foreach (TempProduct item in Tlist)
            {
                SalesItemList Obj = new SalesItemList();
                Obj.Items_Name = item.product_name;
                Obj.UOMNAME = item.UOMNAME; // change by dipak to UOM
                Obj.UOMID = item.UOMID; // change by dipak to UOM
                decimal Price = Convert.ToInt32(item.msrp);
                Price = Math.Round(Price, 3);
                Obj.Price = Price;
                Obj.Qty = Convert.ToDecimal(item.OpQty);
                decimal Total = Convert.ToInt32(item.Total);
                Total = Math.Round(Total, 3);
                Obj.Total = Total;
                Obj.itemID = item.product_id.ToString();
                Obj.CustItemCode = item.CustItemCode;
                ListSend.Add(Obj);
            }

            CashSave sendObj1 = new CashSave();
            sendObj1.TenentID = TID;
            sendObj1.Shopid = Win_Shopid;
            sendObj1.Userid = Win_UserID;
            sendObj1.UserName = Win_UserName;
            sendObj1.TotalPayable = lblPayable.Text != "" ? Convert.ToDecimal(lblPayable.Text) : 0;
            sendObj1.TotalCashRecived = txtcashReceived.Text != "" ? Convert.ToDecimal(txtcashReceived.Text) : 0;
            sendObj1.OrderWay = drpOrderWay.SelectedItem.ToString() != "" ? drpOrderWay.SelectedItem.ToString() : "Walk In - Walk In";
            string Customer = txtCustomer1.Text != "" ? txtCustomer1.Text : "Cash";
            if (Customer.Contains('-'))
            {
                Customer = txtCustomer1.Text != "" ? txtCustomer1.Text.ToString().Split('-')[0].Trim() : "Cash";
            }
            sendObj1.Customer = Customer;
            int C_id = HiddenField3.Value != null && HiddenField3.Value != "" ? Convert.ToInt32(HiddenField3.Value) : 1;
            sendObj1.CustomerID = C_id != 0 ? C_id : 1;
            sendObj1.ShiftID = 1;
            decimal Predis = Convert.ToDecimal(lblDiscount.Text);
            decimal Invoidis = Convert.ToDecimal(lblinvoiceDis.Text);
            decimal Fdis = Predis + Invoidis;
            sendObj1.DiscountTotaloverall = Fdis.ToString();
            sendObj1.overalldisRate = lblInvoiceDisPer.Text != "" ? lblInvoiceDisPer.Text : "0";
            sendObj1.Delivery_Cahrge = lblDelivery.Text != "" ? lblDelivery.Text : "0";
            sendObj1.dgrvSalesItemList = ListSend;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/PerformCashONDeliveryTransaction", sendObj1).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    Cancel();
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                else
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<Response>(EmpResponse);
                    string MSg = "upsss Something Wrong...";//rootobject.data.ToString();
                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('" + MSg + "');", true);
                }
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";
            TextBox txtQty1 = (TextBox)sender;
            ListViewDataItem item = (ListViewDataItem)txtQty1.NamingContainer;

            Label lblproduct = (Label)item.FindControl("lblproduct");
            Label lblUOM = (Label)item.FindControl("lblUOM");
            TextBox txtQty = (TextBox)item.FindControl("txtQty");
            Label lblBatchNO = (Label)item.FindControl("lblBatchNO");

            List<TempProduct> Tlist = new List<TempProduct>();
            Tlist = ((List<TempProduct>)ViewState["InvoiceProd"]).ToList();

            try
            {
                int QTY = Convert.ToInt32(txtQty.Text);
                if (QTY >= 1)
                {
                    int pid = Convert.ToInt32(lblproduct.Text);
                    int uomid = Convert.ToInt32(lblUOM.Text); // change by dipak to UOM
                    string BatchNo = lblBatchNO.Text;

                    TempProduct Calc = Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo); // change by dipak to UOM

                    int Onhand = Calc.OnHand;
                    decimal msrp = Convert.ToDecimal(Calc.msrp);

                    decimal total = Convert.ToDecimal(msrp * QTY);
                    string SUMID = uomid.ToString();  // change by dipak
                    decimal DisRate = GetDisRate(pid, SUMID);  // change by dipak

                    decimal Fdis = ((msrp * DisRate) / 100);

                    if (Onhand != 0)
                    {
                        if (QTY <= Onhand)
                        {
                            Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).msrp = msrp; // change by dipak to UOM
                            Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).OpQty = QTY; // change by dipak to UOM
                            Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Total = total; // change by dipak to UOM
                            Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Discount = Fdis; // change by dipak to UOM

                            ViewState["InvoiceProd"] = Tlist;
                            Listview1.DataSource = Tlist;
                            Listview1.DataBind();
                        }
                        else
                        {
                            bool flag1 = IsPerishable(TID, pid);
                            if (flag1 == true)
                            {
                                Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).msrp = msrp;
                                Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).OpQty = Onhand;
                                Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Total = total;
                                Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Discount = Fdis;

                                lblPRodIDper.Text = pid.ToString();
                                lblProdNamePEr.Text = Calc.product_name;
                                lblUOMName.Text = Calc.UOMNAME; // change by dipak to UOM
                                string Batch = "";
                                List<TempProduct> Tlist1 = Tlist.Where(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid).ToList();
                                foreach (TempProduct item1 in Tlist1)
                                {
                                    Batch += item1.BatchNo + ",";
                                }
                                Batch = Batch.Trim();
                                Batch = Batch.TrimStart(',');
                                Batch = Batch.TrimEnd(',');
                                string NotDisplay = SplitBatch(Batch);// selectBatch.Text;                        
                                int uom = Calc.UOMID;//getuomid(TID, Calc.UOM); // change by dipak to UOM
                                string query = "select * from ICIT_BR_Perishable where TenentID=" + TID + " and MyProdID =" + pid + "  and UOM=" + uom + " and OnHand>=1 and BatchNo not in (" + NotDisplay + ") "; // change by dipak to UOM
                                DataTable dtquery = DataCon.GetDataTable(query);
                                if (dtquery != null)
                                {
                                    if (dtquery.Rows.Count > 0)
                                    {
                                        ListView4.DataSource = dtquery;
                                        ListView4.DataBind();
                                        ModalPopupExtender8.Show();
                                    }
                                    else
                                    {
                                        ModalPopupExtender8.Hide();
                                    }
                                }
                                else
                                {
                                    ModalPopupExtender8.Hide();
                                }
                            }
                            else
                            {
                                Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).msrp = msrp;// change by dipak to UOM
                                Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).OpQty = QTY;// change by dipak to UOM
                                Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Total = total;// change by dipak to UOM
                                Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Discount = Fdis;// change by dipak to UOM
                            }
                        }
                    }
                    else
                    {
                        Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).msrp = msrp;// change by dipak to UOM
                        Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).OpQty = QTY;// change by dipak to UOM
                        Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Total = total;// change by dipak to UOM
                        Tlist.Single(p => p.Tenent == TID && p.product_id == pid && p.UOMID == uomid && p.BatchNo == BatchNo).Discount = Fdis;// change by dipak to UOM
                    }
                }
                else
                {
                    panelMsg.Visible = true;
                    lblErreorMsg.Text = "please Enter Qty Greter than or Equal 1";
                }
            }
            catch
            {
                panelMsg.Visible = true;
                lblErreorMsg.Text = "please Enter Valid Qty";
            }




            ViewState["InvoiceProd"] = Tlist;
            Listview1.DataSource = Tlist;
            Listview1.DataBind();
            FinalCalc();
        }

        List<PaymentDatasale> ListPayment = new List<PaymentDatasale>();
        protected void drpPayBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpPayBy.SelectedValue != "0")
            {

                if (ViewState["GridPayment"] != null)
                {
                    ListPayment = ((List<PaymentDatasale>)ViewState["GridPayment"]).ToList();
                }

                panelMsg.Visible = false;
                lblErreorMsg.Text = "";

                if (txtPaidAmount.Text == "" || txtPaidAmount.Text == ".")
                {
                    return;
                }

                decimal Totalpay = Convert.ToDecimal(lblTotalpayableAmtPY.Text);
                decimal totalPaid = Convert.ToDecimal(lblPaid.Text);

                decimal rest = (Totalpay - totalPaid);
                decimal Enter = Convert.ToDecimal(txtPaidAmount.Text);

                if (Enter > rest)
                {
                    txtPaidAmount.Focus();
                    txtPaidAmount.Text = (rest).ToString();
                    panelMsg.Visible = true;
                    lblErreorMsg.Text = "Plase Enter Less Than " + rest;
                    return;
                }

                if (Totalpay > totalPaid)
                {
                    int sales_id = Convert.ToInt32(invoiceID.Text);
                    string payment_type = drpPayBy.SelectedItem.ToString().Trim();
                    decimal payment_amount = Convert.ToDecimal(txtPaidAmount.Text);
                    string Reffrance_NO = txtPayReffrance.Text;

                    if (payment_type != "Cash" && Reffrance_NO == "")
                    {
                        txtPayReffrance.Focus();
                        panelMsg.Visible = true;
                        lblErreorMsg.Text = "Reffrance_NO Can Not Be Empty in " + payment_type;
                        return;
                    }

                    if (ListPayment.Where(p => p.payment_type == payment_type).Count() < 1)  //If new item
                    {
                        PaymentDatasale Obj = new PaymentDatasale();
                        Obj.invoice = sales_id.ToString();
                        Obj.payment_type = payment_type;
                        Obj.Reffrance_NO = Reffrance_NO;
                        Obj.payment_amount = payment_amount;
                        ListPayment.Add(Obj);
                        ViewState["GridPayment"] = ListPayment;
                        GridPayment.DataSource = ListPayment;
                        GridPayment.DataBind();
                    }
                    else
                    {
                        decimal OldVal = Convert.ToInt32(ListPayment.Where(p => p.payment_type == payment_type).FirstOrDefault().payment_amount);

                        decimal New = OldVal + payment_amount;
                        ListPayment.Where(p => p.payment_type == payment_type).FirstOrDefault().payment_amount = New;

                        string Reffrance = ListPayment.Where(p => p.payment_type == payment_type).FirstOrDefault().Reffrance_NO.ToString();

                        Reffrance = Reffrance + ", " + Reffrance_NO;

                        ListPayment.Where(p => p.payment_type == payment_type).FirstOrDefault().Reffrance_NO = Reffrance;
                        ViewState["GridPayment"] = ListPayment;
                        GridPayment.DataSource = ListPayment;
                        GridPayment.DataBind();
                    }

                    decimal sum = ListPayment.Sum(p => p.payment_amount);

                    lblPaid.Text = sum.ToString();
                    txtPaidAmount.Text = (Totalpay - sum).ToString();
                    txtPayReffrance.Text = "";
                    drpPayBy.SelectedValue = "0";
                    ChangedueCalculation();
                }
                else
                {

                }
            }
        }

        protected void btnOnlySave_Click(object sender, EventArgs e)
        {
            OnlySave(false);
        }

        public void OnlySave(bool Print)
        {
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";

            if (HiddenpaymentCustomer.Value == null || HiddenpaymentCustomer.Value == "")
            {
                //panelMsg.Visible = true;
                //lblErreorMsg.Text = "Please Select Customer";
                //return;
                ModalPopupExtender1.Show();
                return;
            }

            List<TempProduct> Tlist = new List<TempProduct>();
            Tlist = ((List<TempProduct>)ViewState["InvoiceProd"]).ToList();

            List<PaymentDatasale> ListPayment = new List<PaymentDatasale>();
            ListPayment = ((List<PaymentDatasale>)ViewState["GridPayment"]).ToList();

            List<SalesItemList> ListSend = new List<SalesItemList>();

            foreach (TempProduct item in Tlist)
            {
                SalesItemList Obj = new SalesItemList();
                Obj.Items_Name = item.product_name;
                Obj.UOMNAME = item.UOMNAME; // change by dipak to UOM
                Obj.UOMID = item.UOMID; // change by dipak to UOM
                decimal Price = Convert.ToInt32(item.msrp);
                Price = Math.Round(Price, 3);
                Obj.Price = Price;
                Obj.Qty = Convert.ToDecimal(item.OpQty);
                decimal Total = Convert.ToInt32(item.Total);
                Total = Math.Round(Total, 3);
                Obj.Total = Total;
                Obj.itemID = item.product_id.ToString();
                Obj.CustItemCode = item.CustItemCode;
                ListSend.Add(Obj);
            }

            List<PaymentData> GridPayment = new List<PaymentData>();

            foreach (PaymentDatasale item in ListPayment)
            {
                PaymentData Obj = new PaymentData();
                Obj.payment_type = item.payment_type;
                Obj.Reffrance_NO = item.Reffrance_NO;
                Obj.payment_amount = item.payment_amount;
                GridPayment.Add(Obj);
            }

            OnlySave sendObj1 = new OnlySave();
            sendObj1.TenentID = TID;
            sendObj1.Shopid = Win_Shopid;
            sendObj1.Userid = Win_UserID;
            sendObj1.UserName = Win_UserName;
            sendObj1.TotalPayable = lblPayable.Text != "" ? Convert.ToDecimal(lblPayable.Text) : 0;
            sendObj1.TotalCashRecived = txtcashReceived.Text != "" ? Convert.ToDecimal(txtcashReceived.Text) : 0;
            sendObj1.OrderWay = drpOrderWay.SelectedItem.ToString() != "" ? drpOrderWay.SelectedItem.ToString() : "Walk In - Walk In";
            string Customer = txtPaymentCustomer.Text != "" ? txtPaymentCustomer.Text : "Cash";
            if (Customer.Contains('-'))
            {
                Customer = txtPaymentCustomer.Text != "" ? txtPaymentCustomer.Text.ToString().Split('-')[0].Trim() : "Cash";
            }
            sendObj1.Customer = Customer;
            int C_id = HiddenpaymentCustomer.Value != null && HiddenpaymentCustomer.Value != "" ? Convert.ToInt32(HiddenpaymentCustomer.Value) : 1;
            sendObj1.CustomerID = C_id != 0 ? C_id : 1;
            sendObj1.ShiftID = 1;
            decimal Predis = Convert.ToDecimal(lblDiscount.Text);
            decimal Invoidis = Convert.ToDecimal(lblinvoiceDis.Text);
            decimal Fdis = Predis + Invoidis;
            sendObj1.DiscountTotaloverall = Fdis.ToString();
            sendObj1.overalldisRate = lblInvoiceDisPer.Text != "" ? lblInvoiceDisPer.Text : "0";
            sendObj1.Delivery_Cahrge = lblDelivery.Text != "" ? lblDelivery.Text : "0";
            sendObj1.salesDate = txtSalesDate.Text != null && txtSalesDate.Text != "" ? txtSalesDate.Text : DateTime.Now.ToString();
            sendObj1.dgrvSalesItemList = ListSend;
            sendObj1.GridPayment = GridPayment;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/PerformOnlySaveTransaction", sendObj1).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    if (Print == true)
                    {
                        var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                        var rootobject = JsonConvert.DeserializeObject<Response>(EmpResponse);
                        string MSg = rootobject.data.ToString();
                        int sales_id = GetSalesID(MSg);
                        Response.Redirect("POSPrintRpt.aspx?sales_id=" + sales_id);
                    }
                    Cancel();
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                else
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<Response>(EmpResponse);
                    string MSg = rootobject.message.ToString();
                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('" + MSg + "');", true);
                }
            }
        }

        protected void btnSaveAndPrint_Click(object sender, EventArgs e)
        {
            OnlySave(true);
        }

        protected void GridPayment_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "GridRemove")
            {
                string payment_type = e.CommandArgument.ToString();

                List<PaymentDatasale> ListPayment = new List<PaymentDatasale>();
                ListPayment = ((List<PaymentDatasale>)ViewState["GridPayment"]).ToList();
                if (ListPayment.Where(p => p.payment_type == payment_type).Count() > 0)
                {
                    PaymentDatasale obj = ListPayment.Where(p => p.payment_type == payment_type).FirstOrDefault();
                    ListPayment.Remove(obj);
                    ViewState["GridPayment"] = ListPayment;
                    GridPayment.DataSource = ListPayment;
                    GridPayment.DataBind();
                    decimal Totalpay = Convert.ToDecimal(lblTotalpayableAmtPY.Text);

                    decimal sum = ListPayment.Sum(p => p.payment_amount);

                    lblPaid.Text = sum.ToString();
                    txtPaidAmount.Text = (Totalpay - sum).ToString();
                    ChangedueCalculation();

                }
            }
        }

        public void ChangedueCalculation()
        {
            if (Convert.ToDouble(lblPaid.Text) >= Convert.ToDouble(lblTotalpayableAmtPY.Text))
            {
                double changeAmt = Convert.ToDouble(lblPaid.Text) - Convert.ToDouble(lblTotalpayableAmtPY.Text);
                changeAmt = Math.Round(changeAmt, 3);
                txtChangeAmount.Text = changeAmt.ToString();
                txtDueAmount.Text = "0";
            }
            if (Convert.ToDouble(lblPaid.Text) <= Convert.ToDouble(lblTotalpayableAmtPY.Text))
            {
                double changeAmt = Convert.ToDouble(lblTotalpayableAmtPY.Text) - Convert.ToDouble(lblPaid.Text);
                changeAmt = Math.Round(changeAmt, 3);
                txtDueAmount.Text = changeAmt.ToString();
                txtChangeAmount.Text = "0";
            }
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            //tab_1.Attributes["class"] = "tab-pane";
            //tab_2.Attributes["class"] = "tab-pane active";


            //ProductTab.Attributes["class"] = "";
            //PaymentTab.Attributes["class"] = "active";

        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            //tab_1.Attributes["class"] = "tab-pane active";
            //tab_2.Attributes["class"] = "tab-pane";


            //ProductTab.Attributes["class"] = "active";
            //PaymentTab.Attributes["class"] = "";
        }

        public static Color GetCatagoryColor(int TenentID, string CatName)
        {//Parimal
            try
            {
                Color ColorNAme = SystemColors.Control;

                string ColorNam = "";
                string sqlName = "select * from CAT_MST where TenentID = " + TenentID + " and CAT_NAME1 ='" + CatName + "' ";
                DataTable dtName = DataCon.GetDataTable(sqlName);
                if (dtName.Rows.Count > 0)
                {
                    ColorNam = dtName.Rows[0]["COLOR_NAME"].ToString();
                }

                if (ColorNam != "")
                {
                    string coName = ColorNam.Trim();
                    coName = coName.Substring(2);
                    coName = "#" + coName;

                    Color c = System.Drawing.ColorTranslator.FromHtml(coName);
                    byte A = c.A;
                    byte R = c.R;
                    byte G = c.G;
                    byte B = c.B;
                    ColorNAme = Color.FromArgb(A, R, G, B);
                }
                return ColorNAme;
            }
            catch
            {
                return SystemColors.Control;
            }
        }

        protected void ListCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton btnCategory = (LinkButton)e.Item.FindControl("btnCategory");
            Color color = GetCatagoryColor(TID, btnCategory.Text);
            btnCategory.BackColor = color;
        }

        protected void Listview3_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label btnprodname = (Label)e.Item.FindControl("btnprodname");
            Label lblPID = (Label)e.Item.FindControl("lblPID");
            int PID = Convert.ToInt32(lblPID.Text);
            string catagory = FindCatagory(TID, PID);
            Color color = GetCatagoryColor(TID, catagory);
            btnprodname.BackColor = color;
        }

        public string FindCatagory(int TenentID, int product_id)
        {
            string Catagory = "";
            string Sql = "select * from Win_purchase where TenentID = " + TenentID + " and product_id = " + product_id + "";
            DataTable Dt = DataCon.GetDataTable(Sql);
            if (Dt != null)
            {
                if (Dt.Rows.Count > 0)
                {
                    Catagory = Dt.Rows[0]["category"] != null ? Dt.Rows[0]["category"].ToString() : "";
                }
            }
            return Catagory;
        }

        protected void chkWithImage_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWithImage.Checked == true)
            {
                Panel2.Visible = true;
                Panel3.Visible = false;
                string Category = ViewState["Category"].ToString();
                Bindprod(Category);
            }
            else
            {
                Panel2.Visible = false;
                Panel3.Visible = true;
                string Category = ViewState["Category"].ToString();
                Bindprod(Category);
            }
        }
        protected void Chkoutput_CheckedChanged(object sender, EventArgs e)
        {
            ChkInput.Checked = false;
            chkService.Checked = false;
            string Category = ViewState["Category"].ToString();
            Bindprod(Category);
        }
        protected void ChkInput_CheckedChanged(object sender, EventArgs e)
        {
            Chkoutput.Checked = false;
            chkService.Checked = false;
            string Category = ViewState["Category"].ToString();
            Bindprod(Category);
        }

        protected void chkService_CheckedChanged(object sender, EventArgs e)
        {
            Chkoutput.Checked = false;
            ChkInput.Checked = false;
            string Category = ViewState["Category"].ToString();
            Bindprod(Category);
        }

        protected void ListView4_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    string[] ID = e.CommandArgument.ToString().Split('-');

                    int MyProdID = Convert.ToInt32(ID[0]);
                    int uomid = Convert.ToInt32(ID[1]);
                    string UOMName = getuomName(TID, uomid);
                    string BatchNo = ID[2].ToString();
                    int OnHand = Convert.ToInt32(ID[3]);

                    Label lblExpiryDate = (Label)e.Item.FindControl("lblExpiryDate");

                    List<TempProduct> Tlist = new List<TempProduct>();
                    Tlist = ((List<TempProduct>)ViewState["InvoiceProd"]).ToList();
                    TempProduct Calc = Tlist.Single(p => p.Tenent == TID && p.product_id == MyProdID && p.UOMID == uomid);

                    int qty1 = Calc.OpQty;

                    if (BatchNo != Calc.BatchNo && Calc.BatchNo != null && Calc.BatchNo != "")
                    {
                        TempProduct obj = new TempProduct();
                        obj.Tenent = TID;
                        obj.product_id = Calc.product_id;
                        obj.UOMNAME = UOMName; // change by dipak to UOM
                        obj.UOMID = uomid; // change by dipak to UOM
                        obj.Shopid = Calc.Shopid;
                        obj.product_name = Calc.product_name;
                        obj.product_name_Arabic = Calc.product_name_Arabic;
                        obj.product_name_print = Calc.product_name_print;
                        obj.category = Calc.category;
                        obj.supplier = Calc.supplier;
                        obj.taxapply = Convert.ToInt32(Calc.taxapply);
                        obj.imagename = Calc.imagename;
                        obj.status = Convert.ToInt32(Calc.status);
                        obj.price = Convert.ToDecimal(Calc.price);//price
                        obj.msrp = Convert.ToDecimal(Calc.msrp);//total
                        obj.OpQty = Convert.ToInt32(1);//qty
                        obj.OnHand = Convert.ToInt32(Calc.OnHand);
                        obj.QtyOut = Convert.ToInt32(Calc.QtyOut);
                        obj.QtyConsumed = Convert.ToInt32(Calc.QtyConsumed);
                        obj.QtyReserved = Convert.ToInt32(Calc.QtyReserved);
                        obj.QtyRecived = Convert.ToInt32(Calc.QtyRecived);
                        obj.minQty = Convert.ToInt32(Calc.minQty);
                        obj.MaxQty = Convert.ToInt32(Calc.MaxQty);
                        decimal qty = Convert.ToInt32(1);//qty
                        decimal Dis = Convert.ToDecimal(Calc.Discount);
                        decimal msrp = (Convert.ToDecimal(Calc.msrp) * qty);//total
                        decimal Fdis = ((msrp * Dis) / 100);
                        obj.Total = (msrp * qty);
                        obj.Discount = Math.Round(Fdis, 3);
                        obj.CustItemCode = Calc.CustItemCode;
                        obj.BarCode = Calc.BarCode;
                        obj.BatchNo = BatchNo;

                        Tlist.Add(obj);
                        ViewState["InvoiceProd"] = Tlist;
                        Listview1.DataSource = Tlist;
                        Listview1.DataBind();
                        FinalCalc();
                    }
                    else
                    {
                        if (qty1 <= OnHand)
                        {
                            string ExpiryDate1 = lblExpiryDate.Text.ToString();

                            Tlist.Single(p => p.Tenent == TID && p.product_id == MyProdID && p.UOMID == uomid).BatchNo = BatchNo;
                            Tlist.Single(p => p.Tenent == TID && p.product_id == MyProdID && p.UOMID == uomid).OnHand = OnHand;
                            Tlist.Single(p => p.Tenent == TID && p.product_id == MyProdID && p.UOMID == uomid).ExpiryDate = ExpiryDate1;

                            ViewState["InvoiceProd"] = Tlist;
                            Listview1.DataSource = Tlist;
                            Listview1.DataBind();
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch
            {

            }
        }

        protected void ListView4_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {

        }

        //public void sales_item(string salesdate, string OrderStutas, int COD, string PaymentMode, string Customer, int cid)
        //{
        //    //string sqlprofit = "Select msrp, Discount,product_name_print,price  FROM  purchase INNER JOIN tbl_item_uom_price ON purchase.product_id = tbl_item_uom_price.itemID  where itemID = '" + itemid + "' and UOMID = '" + uom + "'";


        //    DateTime sales_date = DateTime.Now;
        //    salesdate = sales_date.ToString("yyyy-MM-dd");
        //    for (int i = 0; i < Listview1.Items.Count(); i++)
        //    {


        //        Label Label1 = (Label)Listview1.Items[i].FindControl("Label1");
        //        Label Label2 = (Label)Listview1.Items[i].FindControl("Label2");
        //        TextBox PRI = (TextBox)Listview1.Items[i].FindControl("TextBox2");
        //        Label Label4 = (Label)Listview1.Items[i].FindControl("Label4");
        //        //TextBox2
        //        Label lblpdiscound = (Label)Listview1.Items[i].FindControl("lblpdiscound");
        //        Label lblptaxapply = (Label)Listview1.Items[i].FindControl("lblptaxapply");
        //        string[] commID = Label1.Text.Split('-');
        //        int ProdID = Convert.ToInt32(commID[0]);
        //        string Prodname = commID[1].ToString();//itNam
        //        string UOM = commID[2].ToString();
        //        decimal Total = Convert.ToDecimal(Label4.Text);
        //        decimal price = Convert.ToDecimal(Label2.Text);
        //        decimal dis = Convert.ToDecimal(lblpdiscound.Text); //discount rate
        //        decimal taxapply = Convert.ToDecimal(lblptaxapply.Text);

        //        int qty = Convert.ToInt32(PRI.Text);
        //        int trno = Convert.ToInt32(invoiceID.Text);
        //        long item_id = DB.Win_sales_item.Where(p => p.TenentID == TID && p.sales_id == trno).Count() > 0 ? Convert.ToInt64(DB.Win_sales_item.Where(p => p.TenentID == TID && p.sales_id == trno).Max(p => p.item_id) + 1) : 1;


        //        var Profit = (from PUR in DB.Win_purchase.Where(p => p.TenentID == TID)
        //                      join UM in DB.Win_tbl_item_uom_price.Where(p => p.TenentID == TID) on PUR.product_id equals UM.itemID
        //                      where UM.itemID == ProdID && UM.UOMID == UOM
        //                      select new { PUR.product_name_print, UM.msrp, UM.Discount, UM.price, PUR.CustItemCode, PUR.status }).ToList();

        //        string product_name_print = Profit[0].product_name_print.ToString();
        //        decimal PRice = Convert.ToDecimal(Profit[0].price);
        //        decimal msrp = Convert.ToDecimal(Profit[0].msrp);
        //        decimal discount = Convert.ToDecimal(Profit[0].Discount);

        //        decimal Discount_amount = Convert.ToDecimal((PRice * discount) / 100);
        //        decimal priceAfterDiscount = (PRice - Discount_amount);
        //        decimal FProfit = Math.Round((msrp - priceAfterDiscount), 3);

        //        decimal OrderTotal = Convert.ToDecimal(lblPayable.Text);
        //        int kitchendisplay = Convert.ToInt32(Profit[0].status);
        //        string CustItemCode = Profit[0].CustItemCode.ToString();
        //        string[] OrderWaydrop = drpOrderWay.SelectedValue.Split('-');
        //        string OrderWay = OrderWaydrop[0];

        //        Database.Win_sales_item sobj = new Database.Win_sales_item();
        //        sobj.TenentID = TID;
        //        sobj.sales_id = trno;
        //        sobj.item_id = item_id;
        //        sobj.itemName = Prodname;
        //        sobj.product_name_print = product_name_print;
        //        sobj.Qty = qty;
        //        sobj.RetailsPrice = price;
        //        sobj.Total = Total;
        //        sobj.profit = FProfit;
        //        sobj.sales_time = salesdate;
        //        sobj.itemcode = ProdID.ToString();
        //        sobj.discount = dis;
        //        sobj.taxapply = taxapply.ToString();
        //        sobj.status = kitchendisplay;
        //        sobj.UOM = UOM;
        //        sobj.Customer = Customer;
        //        sobj.InvoiceNO = invoiceNo.Text.ToString();
        //        sobj.returnQty = 0;
        //        sobj.returnTotal = 0;
        //        sobj.OrderStutas = OrderStutas;
        //        sobj.SoldBy = DB.Win_usermgt.Single(p => p.TenentID == TID).Username;
        //        sobj.COD = COD;
        //        sobj.OrderTotal = OrderTotal;
        //        sobj.PaymentMode = PaymentMode;
        //        sobj.Shopid = DB.Win_usermgt.Single(p => p.TenentID == TID).Shopid;
        //        sobj.c_id = cid.ToString();
        //        sobj.BatchNo = "0";
        //        sobj.ExpiryDate = "";
        //        sobj.OrderWay = OrderWay;
        //        sobj.Uploadby = DB.Win_usermgt.Single(p => p.TenentID == TID).Username;
        //        sobj.UploadDate = DateTime.Now;
        //        sobj.SynID = 9;
        //        sobj.CustItemCode = CustItemCode;
        //        DB.Win_sales_item.AddObject(sobj);
        //        DB.SaveChanges();
        //        Cancel();
        //    }
        //}

        protected void btnDRAFT_Click(object sender, EventArgs e)
        {
            panelMsg.Visible = false;
            lblErreorMsg.Text = "";

            if (HiddenField3.Value == null || HiddenField3.Value == "")
            {
                //panelMsg.Visible = true;
                //lblErreorMsg.Text = "Please Select Customer";
                //return;
                ModalPopupExtender1.Show();
                return;
            }

            List<TempProduct> Tlist = new List<TempProduct>();
            Tlist = ((List<TempProduct>)ViewState["InvoiceProd"]).ToList();

            List<SalesItemList> ListSend = new List<SalesItemList>();

            foreach (TempProduct item in Tlist)
            {
                SalesItemList Obj = new SalesItemList();
                Obj.Items_Name = item.product_name;
                Obj.UOMNAME = item.UOMNAME; // change by dipak to UOM
                Obj.UOMID = item.UOMID; // change by dipak to UOM
                decimal Price = Convert.ToInt32(item.msrp);
                Price = Math.Round(Price, 3);
                Obj.Price = Price;
                Obj.Qty = Convert.ToDecimal(item.OpQty);
                decimal Total = Convert.ToInt32(item.Total);
                Total = Math.Round(Total, 3);
                Obj.Total = Total;
                Obj.itemID = item.product_id.ToString();
                Obj.CustItemCode = item.CustItemCode;
                ListSend.Add(Obj);
            }

            DraftSave sendObj1 = new DraftSave();
            sendObj1.TenentID = TID;
            sendObj1.Shopid = Win_Shopid;
            sendObj1.Userid = Win_UserID;
            sendObj1.UserName = Win_UserName;
            sendObj1.TotalPayable = lblPayable.Text != "" ? Convert.ToDecimal(lblPayable.Text) : 0;
            sendObj1.OrderWay = drpOrderWay.SelectedItem.ToString() != "" ? drpOrderWay.SelectedItem.ToString() : "Walk In - Walk In";
            string Customer = txtCustomer1.Text != "" ? txtCustomer1.Text : "Cash";
            if (Customer.Contains('-'))
            {
                Customer = txtCustomer1.Text != "" ? txtCustomer1.Text.ToString().Split('-')[0].Trim() : "Cash";
            }
            sendObj1.Customer = Customer;
            int C_id = HiddenField3.Value != null && HiddenField3.Value != "" ? Convert.ToInt32(HiddenField3.Value) : 1;
            sendObj1.CustomerID = C_id != 0 ? C_id : 1;
            sendObj1.ShiftID = 1;
            sendObj1.dgrvSalesItemList = ListSend;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/PerformDraftTransaction", sendObj1).Result;
                //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<Response>(EmpResponse);
                    string MSg = "Draft save Success with Transaction = " + rootobject.data.ToString();

                    Classes.Toastr.ShowToast(Page, Classes.Toastr.ToastType.Success, MSg, "Success!", Classes.Toastr.ToastPosition.TopCenter);
                    Cancel();
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                else
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<Response>(EmpResponse);
                    string MSg = rootobject.data.ToString();
                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('" + MSg + "');", true);
                }
            }
        }


        protected void Linkcust_Click(object sender, EventArgs e)
        {
            string NameENG = txtCustENG.Text;
            string NameARB = txtCustARB.Text;
            string Custphone = txtCustPhone.Text;
            string Custadd = txtCustAddress.Text;
            string CustCity = DRPCity1.SelectedValue;
            int CCID = DB.Win_tbl_customer.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Win_tbl_customer.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
            string Userid = ((USER_MST)Session["USER"]).LOGIN_ID;

            string sqlCmdWin = "insert into Win_tbl_customer (TenentID,ID, Name,NameArabic, Phone, address, City, PeopleType,Uploadby ,UploadDate ,SynID,EmailAddress )  " +
                                          " values (" + TID + ",'" + CCID + "', N'" + NameENG + "', N'" + NameARB + "','" + Custphone + "', N'" + Custadd + "', " +
                                          " N'" + CustCity + "', 'Customer','" + Userid + "' , '" + DateTime.Now + "', 1,'')";
            POS.DataCon.ExecuteSQL(sqlCmdWin);
            //Database.Win_tbl_customer obj = new Database.Win_tbl_customer();
            //obj.TenentID = TID;
            //obj.ID = CCID;
            //obj.Name = NameENG;
            //obj.NameArabic = NameARB;
            //obj.Phone = Custphone;
            //obj.Address = Custadd;
            //obj.City = CustCity;
            //obj.PeopleType = "Customer";
            //obj.Uploadby = Userid;
            //obj.UploadDate = DateTime.Now;
            //obj.SynID = 1;
            //DB.Win_tbl_customer.AddObject(obj);
            //DB.SaveChanges();
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
                return textvalue;
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

        protected void txtCustENG_TextChanged(object sender, EventArgs e)
        {
            string NameENG = txtCustENG.Text;
            txtCustARB.Text = Translate(NameENG, "ar");
        }

        protected void Listview2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label Prod = (Label)e.Item.FindControl("Prod");
            System.Web.UI.WebControls.Image lnkimg = (System.Web.UI.WebControls.Image)e.Item.FindControl("lnkimg");
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                long myprodid = Convert.ToInt64(Prod.Text);
                if(DB.ImageTables.Where(p => p.TenentID == TID && p.MYPRODID == myprodid).Count() > 0)
                {
                    Database.ImageTable ImgPath = DB.ImageTables.Single(p => p.TenentID == TID && p.MYPRODID == myprodid);
                    lnkimg.ImageUrl = "/ECOMM/Upload/" + ImgPath.PICTURE;
                }
            }
        }





    }

}