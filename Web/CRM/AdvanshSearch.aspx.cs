using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
namespace Web.CRM
{
    public partial class AdvanshSearch : System.Web.UI.Page
    {
        Database.CallEntities DB = new Database.CallEntities();
        bool FirstFlag, ClickFlag = true;
        int TID, TTID, LID, stMYTRANSID, UID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (Session["ADMInPrevious"].ToString().Contains('?'))
            {
                string[] linkurl = Session["ADMInPrevious"].ToString().Split('?');
                Session["ADMInPrevious"] = linkurl[0].ToString();
            }
            if (!IsPostBack)
            {
                FistTimeLoad();
                if (Request.QueryString["Name"] != null)
                {
                    string Name = Request.QueryString["Name"].ToString();
                    if (Name == "customer")
                    {
                        pMasterGridview.DataSource = DB.TBLCOMPANYSETUPs.Where(p => p.Active == "Y" && p.BUYER == true && p.TenentID == TID);
                        pMasterGridview.DataBind();
                    }
                    if (Name == "supplier")
                    {
                        pMasterGridview.DataSource = DB.TBLCOMPANYSETUPs.Where(p => p.Active == "Y" && p.SALER == true && p.TenentID == TID);
                        pMasterGridview.DataBind();
                    }
                   
                }

            }
        }
        public void SessionLoad()
        {
            string Ref = ((CRMMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TTID = TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();

        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
        }
        public void dropdown()
        {
            //Classes.EcommAdminClass.getdropdown(drpBeand, TID, "BRAND", "OTH", "", "REFTABLE");

            //drpMinCategr.DataSource = DB.CAT_MST.Where(p => p.TenentID == TID && p.CAT_TYPE == "WEBSALE" && p.PARENT_CATID == 0);
            //drpMinCategr.DataTextField = "CAT_NAME1";
            //drpMinCategr.DataValueField = "CATID";
            //drpMinCategr.DataBind();
            //drpMinCategr.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Category--", "0"));
            //drpsubctegory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Sub Category--", "0"));


        }

        protected void lbApproveIss_Click(object sender, EventArgs e)
        {
            //string pro1 = txtproduct.Text.ToString();
            //string barcode = txtbarcode.Text.ToString();
            //string ac1 = txtaltcoed1.Text.ToString();
            //string ac2 = txtaltcoed2.Text.ToString();
            //string pro2 = txtproduc2.Text.ToString();
            //string pro3 = txtprodu3.Text.ToString();
            //string shor = txtshortname.Text.ToString();
            //string bard = drpBeand.SelectedValue.ToString();
            //string remark = txtremarck.Text.ToString();
            //string key = txtkeywored.Text.ToString();
            //decimal msrp = Convert.ToDecimal(txtmsrp.Text);
            //decimal pries = Convert.ToDecimal(txtprice.Text);
            //int MCAID = Convert.ToInt32(drpMinCategr.SelectedValue);
            //int SCID = Convert.ToInt32(drpsubctegory.SelectedValue);


            //List<Database.Eco_TBLPRODUCT> List = new List<Database.Eco_TBLPRODUCT>();
            //int Status1 = 0;
            //if (pro1 != null && pro1 != "")
            //{
            //    List = DB.Eco_TBLPRODUCT.Where(p => p.ProdName1.ToUpper().Contains(pro1.ToUpper())).ToList();
            //    Status1 = 1;
            //}
            //else
            //{

            //}
            //if (barcode != "" && barcode != null)
            //{
            //    if (Status1 == 0)
            //    {
            //        List = DB.Eco_TBLPRODUCT.Where(p => p.BarCode.ToUpper().Contains(barcode.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //    else
            //    {
            //        List = List.Where(p => p.BarCode.ToUpper().Contains(barcode.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //}
            //else
            //{

            //}
            //if (ac1 != "" && ac1 != null)
            //{
            //    if (Status1 == 0)
            //    {
            //        List = DB.Eco_TBLPRODUCT.Where(p => p.AlternateCode1.ToUpper().Contains(ac1.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //    else
            //    {
            //        List = List.Where(p => p.AlternateCode1.ToUpper().Contains(ac1.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //}
            //else
            //{

            //}
            //if (ac2 != "" && ac2 != null)
            //{
            //    if (Status1 == 0)
            //    {
            //        List = DB.Eco_TBLPRODUCT.Where(p => p.AlternateCode2.ToUpper().Contains(ac2.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //    else
            //    {
            //        List = List.Where(p => p.AlternateCode2.ToUpper().Contains(ac2.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //}
            //else
            //{

            //}
            //if (pro2 != "" && pro2 != null)
            //{
            //    if (Status1 == 0)
            //    {
            //        List = DB.Eco_TBLPRODUCT.Where(p => p.ProdName2.ToUpper().Contains(pro2.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //    else
            //    {
            //        List = List.Where(p => p.ProdName2.ToUpper().Contains(pro2.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //}
            //else
            //{

            //}
            //if (pro3 != "" && pro3 != null)
            //{
            //    if (Status1 == 0)
            //    {
            //        List = DB.Eco_TBLPRODUCT.Where(p => p.ProdName3.ToUpper().Contains(pro3.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //    else
            //    {
            //        List = List.Where(p => p.ProdName3.ToUpper().Contains(pro3.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //}
            //else
            //{

            //}
            //if (shor != "" && shor != null)
            //{
            //    if (Status1 == 0)
            //    {
            //        List = DB.Eco_TBLPRODUCT.Where(p => p.ShortName.ToUpper().Contains(shor.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //    else
            //    {
            //        List = List.Where(p => p.ShortName.ToUpper().Contains(shor.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //}
            //else
            //{

            //}
            //if (bard != "0" && bard != null)
            //{
            //    if (Status1 == 0)
            //    {
            //        List = DB.Eco_TBLPRODUCT.Where(p => p.Brand.ToUpper().Contains(bard.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //    else
            //    {
            //        List = List.Where(p => p.Brand.ToUpper().Contains(bard.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //}
            //else
            //{

            //}
            //if (remark != "" && remark != null)
            //{
            //    if (Status1 == 0)
            //    {
            //        List = DB.Eco_TBLPRODUCT.Where(p => p.REMARKS.ToUpper().Contains(remark.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //    else
            //    {
            //        List = List.Where(p => p.REMARKS.ToUpper().Contains(remark.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //}
            //else
            //{

            //}
            //if (key != "" && key != null)
            //{
            //    if (Status1 == 0)
            //    {
            //        List = DB.Eco_TBLPRODUCT.Where(p => p.keywords.ToUpper().Contains(key.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //    else
            //    {
            //        List = List.Where(p => p.keywords.ToUpper().Contains(key.ToUpper())).ToList();
            //        Status1 = 1;
            //    }
            //}
            //else
            //{

            //}
            //if (msrp != 0 && remark != null)
            //{
            //    if (Status1 == 0)
            //    {
            //        List = DB.Eco_TBLPRODUCT.Where(p => p.msrp == msrp).ToList();
            //        Status1 = 1;
            //    }
            //    else
            //    {
            //        List = List.Where(p => p.msrp == msrp).ToList();
            //        Status1 = 1;
            //    }
            //}
            //else
            //{

            //}

            //if (pries != 0 && pries != null)
            //{
            //    if (Status1 == 0)
            //    {
            //        List = DB.Eco_TBLPRODUCT.Where(p => p.price == pries).ToList();
            //        Status1 = 1;
            //    }
            //    else
            //    {
            //        List = List.Where(p => p.price == pries).ToList();
            //        Status1 = 1;
            //    }
            //}
            //else
            //{

            //}
            //if (MCAID != 0 && SCID != 0)
            //{
            //    if (Status1 == 0)
            //    {
            //        var result2 = (from Module in DB.Eco_TBLPRODUCT
            //                       join
            //                           pm in DB.Eco_Product_Cat_Mst on Module.MYPRODID equals pm.MYPRODID
            //                       where Module.ACTIVE == "1" && Module.TenentID == TID && pm.CATID == SCID && pm.Active == "1" && pm.PARENT_CATID == MCAID
            //                       select new { Module.MYPRODID, Module.BarCode, Module.ShortName, Module.ProdName1, Module.Brand, }).ToList();
            //        List<Eco_TBLPRODUCT> List1 = new List<Eco_TBLPRODUCT>();
            //        for (int i = 0; i < result2.Count(); i++)
            //        {
            //            Eco_TBLPRODUCT obj = new Eco_TBLPRODUCT();
            //            obj.MYPRODID = result2[i].MYPRODID;
            //            obj.BarCode = result2[i].BarCode;
            //            obj.ShortName = result2[i].ShortName;
            //            obj.ProdName1 = result2[i].ProdName1;
            //            obj.Brand = result2[i].Brand;
            //            List1.Add(obj);
            //        }
            //        for (int i = 0; i < List1.Count(); i++)
            //        {
            //            Eco_TBLPRODUCT obj = new Eco_TBLPRODUCT();
            //            obj.MYPRODID = List1[i].MYPRODID;
            //            obj.BarCode = List1[i].BarCode;
            //            obj.ShortName = List1[i].ShortName;
            //            obj.ProdName1 = List1[i].ProdName1;
            //            obj.Brand = List1[i].Brand;
            //            obj.ACTIVE = "1";
            //            List.Add(obj);
            //        }
            //        Status1 = 1;

            //    }
            //    else
            //    {
            //        List<Eco_TBLPRODUCT> List1 = new List<Eco_TBLPRODUCT>();
            //        for (int i = 0; i < List.Count(); i++)
            //        {
            //            long MPID = Convert.ToInt64(List[i].MYPRODID);
            //            var result2 = (from Module in DB.Eco_TBLPRODUCT
            //                           join
            //                               pm in DB.Eco_Product_Cat_Mst on Module.MYPRODID equals pm.MYPRODID
            //                           where Module.ACTIVE == "1" && Module.TenentID == TID && pm.CATID == SCID && pm.Active == "1" && pm.PARENT_CATID == MCAID && pm.MYPRODID == MPID
            //                           select new { Module.MYPRODID, Module.BarCode, Module.ShortName, Module.ProdName1, Module.Brand, }).ToList();

            //            for (int ii = 0; ii < result2.Count(); ii++)
            //            {
            //                long PID = Convert.ToInt64(result2[ii].MYPRODID);
            //                if (MPID != PID)
            //                {
            //                    Eco_TBLPRODUCT obj = new Eco_TBLPRODUCT();
            //                    obj.MYPRODID = result2[ii].MYPRODID;
            //                    obj.BarCode = result2[ii].BarCode;
            //                    obj.ShortName = result2[ii].ShortName;
            //                    obj.ProdName1 = result2[ii].ProdName1;
            //                    obj.Brand = result2[ii].Brand;
            //                    List1.Add(obj);
            //                }

            //            }

            //        }
            //        for (int ii = 0; ii < List1.Count(); ii++)
            //        {
            //            Eco_TBLPRODUCT obj = new Eco_TBLPRODUCT();
            //            obj.MYPRODID = List1[ii].MYPRODID;
            //            obj.BarCode = List1[ii].BarCode;
            //            obj.ShortName = List1[ii].ShortName;
            //            obj.ProdName1 = List1[ii].ProdName1;
            //            obj.Brand = List1[ii].Brand;
            //            obj.ACTIVE = "1";
            //            List.Add(obj);
            //        }

            //        Status1 = 0;
            //    }
            //}
            //else
            //{

            //}
            //pMasterGridview.DataSource = List.Where(p => p.ACTIVE == "1");
            //pMasterGridview.DataBind();
            //if (ViewState["Multi"] != null)
            //{
            //    for (int i = 0; i < pMasterGridview.Items.Count; i++)
            //    {
            //        LinkButton btnselect = (LinkButton)pMasterGridview.Items[i].FindControl("btnselect");
            //        CheckBox cbSelect = (CheckBox)pMasterGridview.Items[i].FindControl("cbSelect");
            //        btnselect.Visible = false;
            //        cbSelect.Visible = true;
            //        btnsubmit.Visible = true;
            //    }

            //}
            //Status1 = 0;


        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            //Searchlist.Visible = false;
            //panlForm.Visible = true;
            //txtaltcoed1.Text = txtaltcoed2.Text = txtbarcode.Text = txtkeywored.Text = txtprodu3.Text = txtproduc2.Text = txtproduct.Text = txtremarck.Text = txtshortname.Text = "";
            //drpBeand.SelectedIndex = 0;
            //txtmsrp.Text = "0";
            //txtprice.Text = "0";
        }


        protected void pMasterGridview_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "btnselect")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                //Database.Eco_TBLPRODUCT objproduct = DB.Eco_TBLPRODUCT.Single(p => p.MYPRODID == ID);


            }
        }

        protected void drpMinCategr_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int PCID = Convert.ToInt32(drpMinCategr.SelectedValue);
            //drpsubctegory.DataSource = DB.CAT_MST.Where(p => p.TenentID == TID && p.CAT_TYPE == "WEBSALE" && p.PARENT_CATID == PCID);
            //drpsubctegory.DataTextField = "CAT_NAME1";
            //drpsubctegory.DataValueField = "CATID";
            //drpsubctegory.DataBind();
            //drpsubctegory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Category--", "0"));
            //drpsubctegory.Enabled = true;

        }
        List<TBLCOMPANYSETUP> listsestionValue = new List<TBLCOMPANYSETUP>();
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pMasterGridview.Items.Count; i++)
            {
                CheckBox cbSelect = (CheckBox)pMasterGridview.Items[i].FindControl("cbSelect");
                Label lblmuprodid = (Label)pMasterGridview.Items[i].FindControl("lblmuprodid");
                Label lblGrICN = (Label)pMasterGridview.Items[i].FindControl("lblGrICN");
                Label lblGrICD = (Label)pMasterGridview.Items[i].FindControl("lblGrICD");
                Label lblGrIPC = (Label)pMasterGridview.Items[i].FindControl("lblGrIPC");
                Label lblGrICT = (Label)pMasterGridview.Items[i].FindControl("lblGrICT");
                if (cbSelect.Checked == true)
                {
                    TBLCOMPANYSETUP obj = new TBLCOMPANYSETUP();
                    obj.COMPNAME1  = lblmuprodid.Text;
                    obj.EMAIL1 = lblGrICN.Text;
                    obj.ADDR1 = lblGrICD.Text;
                    obj.CITY = lblGrIPC.Text;
                    obj.POSTALCODE = lblGrICT.Text;
                    listsestionValue.Add(obj);
                }
            }
            Session["SerchList"] = listsestionValue;
            string URL = Session["ADMInPrevious"].ToString();
            Response.Redirect(URL + "?Sesstion=SerchList");
        }
    }
}