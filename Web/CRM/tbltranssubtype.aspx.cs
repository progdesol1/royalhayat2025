using System;
//using SaasDAL;
using System.Linq;
//using SaasBAL;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Transactions;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Linq.Expressions;
//using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Database;
namespace Web.CRM
{
    public partial class tbltranssubtype : System.Web.UI.Page
    {
        // DALtbltranssubtype objDALValue = new DALtbltranssubtype();
        //CommonSelectDelete objCommonSelDel = new CommonSelectDelete();
        //ERPDataLinqToSqlDataContext dbContext = new ERPDataLinqToSqlDataContext();
        Database.CallEntities DB = new Database.CallEntities();
        string[] lfillform;
        string popupScript = "";
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                
                fillGrid();
                bindTranType();
               // bindSystem();
            }
            Session["Pagename"] = "AdminForm";
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
       
        protected void lnkbtndelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton ln = (LinkButton)sender;
                lfillform = ln.CommandArgument.Split(',');
                int OID = Convert.ToInt32(lfillform[1]);
                Database.tbltranssubtype obj_tbltranstype = DB.tbltranssubtypes.SingleOrDefault(p => p.transid == OID && p.TenentID == TID);
                obj_tbltranstype.Active = "D";
                DB.SaveChanges();
               // objCommonSelDel.CommonDeleteWithTenant("prc_acm_common_update", lfillform[0].ToString(), "tbltranssubtype", "ACTIVE", "D", "transsubid", Convert.ToInt32(lfillform[1].ToString()), "Active");
              //  WebMsgBox.Show("Record deleted Successfully !!");
                fillGrid();
                clear();
                btnSubmit.Text = "Submit";
            }
            catch (Exception ex)
            {
              //  WebMsgBox.Show(ex.Message);
            }
        }

        protected void lnkbtn_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton ln = (LinkButton)sender;
                lfillform = ln.CommandArgument.Split(',');
                int ID = Convert.ToInt32(lfillform[1]);
                Database.tbltranssubtype obj_tbltranstype = DB.tbltranssubtypes.SingleOrDefault(p => p.transsubid == ID && p.TenentID == TID);
             //   System.Data.DataTable dt2 = objCommonSelDel.CommonFillConditionGridPQ("[dbo].[prc_acm_common_sel_con]", "c", "TenentID,transid,MYSYSNAME,transsubid,transsubtype1,transsubtype2,transsubtype3,OpQtyBeh,OnHandBeh,QtyOutBeh,QtyConsumedBeh,QtyReservedBeh,QtyAtDestination,QtyAtSource,serialno,years,Active,CRUP_ID", "tbltranssubtype", "transsubid", lfillform[1].ToString(), "");
                drpTranType.SelectedValue = obj_tbltranstype.transid.ToString(); //dt2.Rows[0]["transid"].ToString();
                //drpMysysname.SelectedValue = dt2.Rows[0]["MYSYSNAME"].ToString();
                txttranssubtype1.Text = obj_tbltranstype.transsubtype1.ToString(); //dt2.Rows[0]["transsubtype1"].ToString();
                txttranssubtype2.Text = obj_tbltranstype.transsubtype2.ToString(); //dt2.Rows[0]["transsubtype2"].ToString();
                txttranssubtype3.Text = obj_tbltranstype.transsubtype3.ToString();//dt2.Rows[0]["transsubtype3"].ToString();
                drpOnHandBeh.SelectedValue = obj_tbltranstype.OnHandBeh.ToString();// dt2.Rows[0]["OnHandBeh"].ToString();
                drpOpQtyBeh.SelectedValue = obj_tbltranstype.OpQtyBeh.ToString(); //dt2.Rows[0]["OpQtyBeh"].ToString();
                drpQtyConsumedBeh.SelectedValue = obj_tbltranstype.OpQtyBeh.ToString(); //dt2.Rows[0]["QtyConsumedBeh"].ToString();
                drpQtyOutBeh.SelectedValue = obj_tbltranstype.QtyOutBeh.ToString();//dt2.Rows[0]["QtyOutBeh"].ToString();
                drpQtyReservedBeh.SelectedValue = obj_tbltranstype.QtyAtDestination.ToString();//dt2.Rows[0]["QtyAtDestination"].ToString();
                txtQtyAtDestination.Text = obj_tbltranstype.QtyAtDestination.ToString();//dt2.Rows[0]["QtyReservedBeh"].ToString();
                txtQtyAtSource.Text = obj_tbltranstype.QtyAtSource.ToString();//dt2.Rows[0]["QtyAtSource"].ToString();
                txtserialno.Text = obj_tbltranstype.serialno.ToString();//dt2.Rows[0]["serialno"].ToString();
                txtyears.Text = obj_tbltranstype.years.ToString();//dt2.Rows[0]["years"].ToString();

                hidId.Value = lfillform[1].ToString();
                btnSubmit.Text = "Update";
            }
            catch (Exception ex)
            {
              //  WebMsgBox.Show(ex.Message);
            }

        }
        protected void lnkAction_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lfillform;
                LinkButton ln = (LinkButton)sender;
                lfillform = ln.CommandArgument.Split(',');
                int ID = Convert.ToInt32(lfillform[0]);
                string flag = lfillform[1];
                if (flag == "Y")
                {
                    flag = "N";
                }
                else
                {
                    flag = "Y";
                }
                int ID1 = Convert.ToInt32(lfillform[2]);
                Database.tbltranssubtype obj_tbltranstype = DB.tbltranssubtypes.SingleOrDefault(p => p.transsubid == ID1 && p.TenentID == TID);
                obj_tbltranstype.Active = flag;
                DB.SaveChanges();
              //  objCommonSelDel.CommonDeleteWithTenant("prc_acm_common_update", lfillform[0].ToString(), "tbltranssubtype", "ACTIVE", flag, "transsubid", Convert.ToInt32(lfillform[2].ToString()), "Active");
              //  WebMsgBox.Show("Status changed Successfully !!");
                fillGrid();
                clear();
                btnSubmit.Text = "Submit";
            }
            catch (Exception ex)
            {
               // WebMsgBox.Show(ex.Message);
            }
        }
      
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Database.tbltranssubtype objDALValue = new Database.tbltranssubtype();
               
                int i = (string.IsNullOrEmpty(hidId.Value) ? -1 : Convert.ToInt32(hidId.Value));
                if (i == -1)
                {
                    int intIdt = DB.tbltranssubtypes.Max(u => (int)u.transsubid) == null ? 1 : DB.tbltranssubtypes.Max(u => (int)u.transsubid) + 1;
                    objDALValue.transsubid = intIdt;
                    objDALValue.Active = "Y";
                }
                else
                {
                    objDALValue.transsubid = i;
                }

                objDALValue.transid =Convert.ToInt32(drpTranType.SelectedValue);
                int transid =Convert.ToInt32(drpTranType.SelectedValue);
                Database.tbltranssubtype obj_type = DB.tbltranssubtypes.SingleOrDefault(p => p.transid == transid && p.TenentID == TID);
                //System.Data.DataTable dt = objCommonSelDel.CommonFillConditionGridPQ("[dbo].[prc_acm_common_sel_con]", "c", "MYSYSNAME", "dbo.tbltranstype", "transid", drpTranType.SelectedValue, "");

                objDALValue.MYSYSNAME = obj_type.MYSYSNAME.ToString();
                objDALValue.transsubtype1 = txttranssubtype1.Text;
                objDALValue.transsubtype2 = txttranssubtype2.Text;
                objDALValue.transsubtype3 = txttranssubtype3.Text;
                objDALValue.OpQtyBeh = drpOpQtyBeh.SelectedItem.Text;
                objDALValue.OnHandBeh = drpOnHandBeh.SelectedItem.Text;
                objDALValue.QtyOutBeh = drpQtyOutBeh.SelectedItem.Text;
                objDALValue.QtyConsumedBeh = drpQtyConsumedBeh.SelectedItem.Text;
                objDALValue.QtyReservedBeh = drpQtyReservedBeh.SelectedItem.Text;
                objDALValue.QtyAtDestination = txtQtyAtDestination.Text;
                objDALValue.QtyAtSource = txtQtyAtSource.Text;
                objDALValue.serialno = txtserialno.Text;
                objDALValue.years = txtyears.Text;

                //objDALValue.TenentID = TID;
                //objDALValue.CreatedBy = UID;
                //objDALValue.UpdatedBy = UID;
                int j = 1; //objDALValue.insertTable();
                if (j == 1)
                {
                    popupScript = "<script language='javascript'>" + "showSuccessToast();" + "</script>";
                    clear();
                    hidId.Value = "";
                    fillGrid();
                    btnSubmit.Text = "Submit";
                }
                else if (j == 2)
                {
                    popupScript = "<script language='javascript'>" + "showSuccessToast1();" + "</script>";
                    //WebMsgBox.Show("Role updated successfully.");
                    clear();
                    hidId.Value = "";
                    fillGrid();
                    btnSubmit.Text = "Submit";
                }
                Page.ClientScript.RegisterStartupScript(typeof(Page), "popupScript", popupScript);
            }
            catch (Exception ex)
            {
              //  WebMsgBox.Show(ex.Message);
            }

        }
       
        public void clear()
        {
            try
            {
               txttranssubtype1.Text = txttranssubtype2.Text = txttranssubtype3.Text = txtQtyAtDestination.Text = txtQtyAtSource.Text = txtserialno.Text = txtyears.Text = "";
               drpOnHandBeh.SelectedIndex = drpOpQtyBeh.SelectedIndex = drpQtyConsumedBeh.SelectedIndex = drpQtyOutBeh.SelectedIndex = drpQtyReservedBeh.SelectedIndex = drpTranType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
               // WebMsgBox.Show(ex.Message);
            }
        }
        
        public void bindTranType()
        {
            try
            {
               
                //System.Data.DataTable dt = objCommonSelDel.CommonFillCondition("[dbo].[prc_acm_common_sel_con]", "checked_Active", "TenentID, transid, MYSYSNAME, inoutSwitch, transtype1, transtype2, transtype3, serialno, years, Active, CRUP_ID", "dbo.tbltranstype", "TenentID",TID.ToString());
                //if (dt.Rows.Count > 0)
                //{


                    //drpTranType.DataSource = DB.tbltranssubtypes.Where(p => p.TenentID == ID).Distinct();
                    //drpTranType.DataTextField = "transtype1";
                    //drpTranType.DataValueField = "transid";
                    //drpTranType.DataBind();
                    //drpTranType.Items.Insert(0, "-- Select --");

                Classes.EcommAdminClass.getdropdown(drpTranType, TID, "HLY", "", "", "tbltranssubtype");
                //select * from tbltranssubtype where TenentID = 0

                    drpTranType.SelectedIndex = -1;
                //}
                //else
                //{
                //    drpTranType.Items.Clear();
                //    drpTranType.Items.Insert(0, "-- Select --");
                //}
            }
            catch (Exception ex)
            {
              //  ERPNew.WebMsgBox.Show(ex.Message);
            }
        }
        //public void bindSystem()
        //{
        //    try
        //    {
        //        System.Data.DataTable dt = objCommonSelDel.CommonFillCondition("[dbo].[prc_acm_common_sel_con]", "DISTINCT", "MYSYSNAME", "dbo.TBLSYSTEMS", "", "");
        //        if (dt.Rows.Count > 0)
        //        {
        //            drpMysysname.DataSource = dt;
        //            drpMysysname.DataTextField = "MYSYSNAME";
        //            drpMysysname.DataValueField = "MYSYSNAME";
        //            drpMysysname.DataBind();
        //            drpMysysname.Items.Insert(0, "-- Select --");
        //            drpMysysname.SelectedIndex = -1;
        //        }
        //        else
        //        {
        //            drpMysysname.Items.Clear();
        //            drpMysysname.Items.Insert(0, "-- Select --");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ERPNew.WebMsgBox.Show(ex.Message);
        //    }
        //}
        public void fillGrid()
        {
            try
            {
                List<Database.tbltranssubtype> List_Transtype = DB.tbltranssubtypes.Where(p => p.TenentID == TID && p.Active == "Y").ToList();
               // System.Data.DataTable dt = objCommonSelDel.CommonMethodOderFnmTnmObTenant("[dbo].[prc_getAllTable_Act_OdrWise]", "t.TenentID,t.MYSYSNAME,t.transsubid,t.transsubtype1,t.transsubtype2,t.transsubtype3,t.OpQtyBeh,t.OnHandBeh,t.QtyOutBeh,t.QtyConsumedBeh,t.QtyReservedBeh,t.QtyAtDestination,t.QtyAtSource,t.serialno,t.years,t.Active,t.CRUP_ID,(select transtype1 from dbo.tbltranstype as r where r.transid = t.transid) as transtype", "tbltranssubtype as t", "transsubid",TID);
                if (List_Transtype.Count() > 0)
                {
                    grdmstr.DataSource = List_Transtype;
                    grdmstr.DataBind();
                    for (int i = 0; i < grdmstr.Items.Count; i++)
                    {
                        LinkButton ln = (LinkButton)grdmstr.Items[i].FindControl("lnkAction");
                        Label lbl = (Label)grdmstr.Items[i].FindControl("lblGRCActive");
                        if (lbl.Text.Trim().Equals("Y") || lbl.Text.Trim().Equals("1"))
                        {
                            ln.Text = "Inactive";
                        }
                        else
                        {
                            ln.Text = "Active";
                        }
                    }
                }
                else
                {
                    grdmstr.DataBind();
                }
            }
            catch (Exception ex)
            {
              //  WebMsgBox.Show(ex.Message);
            }

        }
       
    }
}