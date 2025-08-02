using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data;

namespace Web.Sales
{
    public partial class SalesRegister : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID, LID, UID, EMPID, CID = 0;
        string LangID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                txtItemSearch.Focus();
                CategoryDDLDataBind();
                //  ItemsListDataBind(DDLCategory.Text);
                CustomerNameDDLDataBind();

                BindData(DDLCategory.Text);

                //Add item from item list 
                DataTable table = new DataTable();
                table.Columns.Add("Code", typeof(string));
                table.Columns.Add("Item Name", typeof(string));
                table.Columns.Add("Qty", typeof(string));
                table.Columns.Add("Price", typeof(string));
                table.Columns.Add("Disc%", typeof(string));
                table.Columns.Add("Total", typeof(string));
                Session["value"] = table;
            }
        }
        public void SessionLoad()
        {
            string Ref = ((Sales_Master)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
        }

        public void CategoryDDLDataBind()
        {
            try
            {
                List<Database.CAT_MST> ListCat = DB.CAT_MST.Where(p => p.TenentID == TID).ToList();

                DDLCategory.DataSource = ListCat.OrderBy(p => p.CAT_NAME1);
                DDLCategory.DataTextField = "CAT_NAME1";
                DDLCategory.DataValueField = "CATID";
                DDLCategory.DataBind();
            }
            catch
            {
                // lbtotalRow.Text = "No Records Found";
            }
        }

        public void CustomerNameDDLDataBind()
        {
            try
            {
                string CompanyType = DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.SHORTNAME == "Customer").Count() > 0 ? DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "COMP" && p.REFSUBTYPE == "COMPTYPE" && p.SHORTNAME == "Customer").REFID.ToString() : "0";
                List<Database.TBLCOMPANYSETUP> ListCustomer = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.CompanyType == CompanyType).ToList();
                DDLCustname.DataSource = ListCustomer.OrderBy(p => p.COMPNAME1);
                DDLCustname.DataTextField = "COMPNAME1";
                DDLCustname.DataValueField = "COMPID";
                DDLCustname.DataBind();

            }
            catch
            {
                // lbtotalRow.Text = "No Records Found";
            }
        }

        protected void BindData(string category)
        {
            try
            {

                List<Database.ICIT_BR> ListPRoduct = DB.ICIT_BR.Where(p => p.TenentID == TID && p.OnHand > 0).ToList();

                DataList1.DataSource = ListPRoduct;
                DataList1.DataBind();
            }
            catch
            {
            }
        }

        public string getprodname(int MYPRODID)
        {
            List<Database.TBLPRODUCT> ListProduct = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.MYPRODID == MYPRODID).ToList();
            if (ListProduct.Count() > 0)
            {
                return ListProduct.Single(p => p.TenentID == TID && p.LOCATION_ID == LID && p.MYPRODID == MYPRODID).ProdName1;
            }
            else
            {
                return "Not Found";
            }
        }

        public string getprice(int MYPRODID, int UOM)
        {            
            List<Database.TBLPRODUCT> ListProduct = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.LOCATION_ID == LID && p.MYPRODID == MYPRODID && p.UOM == UOM).ToList();
            if (ListProduct.Count() > 0)
            {
                return ListProduct.Single(p => p.TenentID == TID && p.LOCATION_ID == LID && p.MYPRODID == MYPRODID && p.UOM == UOM).price.ToString();
            }
            else
            {
                return "0.00";
            }
        }

        public string getimage(int MYPRODID)
        {
            List<Database.ImageTable> ListProduct = DB.ImageTables.Where(p => p.TenentID == TID && p.MYPRODID == MYPRODID).ToList();
            if (ListProduct.Count() > 0)
            {
                return "../ECOMM/Upload/" + ListProduct.Where(p => p.TenentID == TID && p.MYPRODID == MYPRODID).FirstOrDefault().PICTURE.ToString();
            }
            else
            {
                return "../ECOMM/Upload/defolt.png";
            }
        }


        double total = 0;
        double Disc = 0;
        double Qty = 0;
        protected void grdSelectedItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //pricetotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Price"));
                total += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
                Disc += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Disc%"));
                Qty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));

                e.Row.Cells[0].Width = 10;
                e.Row.Cells[2].Width = 210;
                e.Row.Cells[6].Font.Bold = true;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //  Label pricetotal = (Label)e.Row.FindControl("pricetotal");
                lblsubTotal.Text = total.ToString();
                lblTotalQty.Text = Qty.ToString();
                //lbldisc.Text = Disc.ToString();
            }
        }

        protected void btnsuspen_Click(object sender, EventArgs e)
        {

        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {

        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DataListItem item = (DataListItem)btn.NamingContainer;
            Label lblId = (Label)item.FindControl("LblID");
            Label LblCode = (Label)item.FindControl("LblCode");
            Label LblItemName = (Label)item.FindControl("LblItemName");
            Label LblQty = (Label)item.FindControl("LblQty");
            Label LblPrice = (Label)item.FindControl("LblPrice");
            Label LblDisc = (Label)item.FindControl("LblDisc");
            Label LblTotal = (Label)item.FindControl("LblTotal");
            TextBox txtqty = (TextBox)item.FindControl("txtqty");

            string ID = lblId.Text;
            string Code = LblCode.Text;
            string ItemName = LblItemName.Text;
            string Qty = txtqty.Text; // LblQty.Text;
            decimal QtyStock = Convert.ToDecimal(LblQty.Text);
            string Price = LblPrice.Text;
            string Disc = "0.00";// LblDisc.Text;
            // string Total = LblTotal.Text; 
            decimal Total = Math.Round((Convert.ToDecimal(Price) - (Convert.ToDecimal(Price) * Convert.ToDecimal(Disc) / 100)) * Convert.ToDecimal(Qty), 2);


            //Check Item Quantity less than 1 
            if (Convert.ToDecimal(Qty) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Purchase the Item from supplier')", true);
            }
            if (Convert.ToDecimal(Qty) > Convert.ToDecimal(QtyStock))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your given quantity is Greater than Stock Quantity')", true);
            }
            else
            {
                //Code	ItemsName	Available_Qty	Price	Disc%	Total
                DataTable dt = (DataTable)Session["value"];
                dt.Rows.Add(Code, ItemName, Qty, Price, Disc, Total);
                grdSelectedItem.DataSource = dt;
                grdSelectedItem.DataBind();

                // double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
                double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(0)) / 100);
                // lbldisc.Text =  pricetotal - 
                lblVat.Text = Math.Round(tex, 2).ToString();
                lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
            }
        }

        protected void DDLCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtPaid_TextChanged(object sender, EventArgs e)
        {

        }

        protected void DDLCustname_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void bntPay_Click(object sender, EventArgs e)
        {

        }

        protected void grdSelectedItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["value"];
            dt.Rows.RemoveAt(e.RowIndex);
            grdSelectedItem.DataSource = dt;
            grdSelectedItem.DataBind();

            ////////Show Popup dialogbox /////////////////////// 
            //LinkButton LbDelete;
            //foreach (GridViewRow rowItem in grdSelectedItem.Rows)
            //{
            //    LbDelete = (LinkButton)(rowItem.Cells[0].FindControl("LbDelete"));
            //    LbDelete.OnClientClick = "return confirm('are you sure to delete')";            
            //}

            if (grdSelectedItem.Rows.Count == 0)
            {
                lblsubTotal.Text = "0";
                lblVat.Text = "0";
                lbltotal.Text = "0";
                lblTotalQty.Text = "0";
            }
            else
            {
                // double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
                double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(0)) / 100);
                lblVat.Text = Math.Round(tex, 2).ToString();
                lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
            }
        }

        protected void txtItemSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}