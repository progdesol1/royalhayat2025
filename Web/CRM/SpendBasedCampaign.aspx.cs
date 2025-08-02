using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.CRM
{
    public partial class SpendBasedCampaign : System.Web.UI.Page
    {
        int TID = 1;
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {
                
                //BindData();
                Clear();
                petsetpanel("block", "collapse");
                PackSetpanel("none", "expand");
                panel1.Visible = true;
                panelreset("none", "expand");

                //setpanelmain("none", "expand");
                //SetPanelMainList("none", "expand");
                //Setpanel("none", "Expand");
                lnknext.ValidationGroup = "ss";
                linkpnl.ValidationGroup = "sm";
                
                lnkcreate1.ValidationGroup = "sg";


                if (Request.QueryString["CampaignID"] != null)
                {
                    int MAXID = Convert.ToInt32(Request.QueryString["CampaignID"]);
                    Database.Campaign_Master objtblsetupsalesh = DB.Campaign_Master.Single(p => p.TenentID == TID && p.CampaignID == MAXID);

                    if (objtblsetupsalesh.CampaignName != null)
                    {
                        txtCampaignName.Text = objtblsetupsalesh.CampaignName.ToString();
                    }
                    if (objtblsetupsalesh.SpendGoal != null)
                    {
                        drpSpendGoal.SelectedValue = objtblsetupsalesh.SpendGoal.ToString();
                    }
                    if (objtblsetupsalesh.CRewardCreditOffer != null)
                    {
                        drpCRewardCreditOffer.SelectedValue = objtblsetupsalesh.CRewardCreditOffer.ToString();
                    }
                    if (objtblsetupsalesh.CEndDate != null)
                    {
                        txtEnddate.Text = objtblsetupsalesh.CEndDate.ToString();
                    }
                    if (objtblsetupsalesh.CExpirationDate != null)
                    {
                        txtexpirydate.Text = objtblsetupsalesh.CExpirationDate.ToString();
                    }

                    if (objtblsetupsalesh.CActive != null)
                    {
                        DropDownList1.SelectedValue = objtblsetupsalesh.CActive.ToString();
                    }

                }
                else
                {

                }
            }
        }
        public void BindList<T>(ListView Listview1, List<T> List)
        {
            //  List<Database.Campaign_Master> List = DB.Campaign_Master.Where(m => m.TenentID == TID).OrderBy(m => m.CampaignID).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();
            // int Totalrec = List.Count();
        }
        public void BindData()
        {
            List<Database.Campaign_Master> List = DB.Campaign_Master.Where(m => m.TenentID == TID && m.CampaignType == 1).OrderBy(m => m.CampaignID).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();


            // int Showdata = Convert.ToInt32(drpShowGrid.SelectedValue);

            // ((AcmMaster)Page.Master).Loadlist(Showdata, take, Skip, ChoiceID, Listview1, Totalrec, List);
        }
        public void Clear()
        {
            
            txtCampaignName.Text = "";
            drpSpendGoal.SelectedIndex = 0;
            drpCRewardCreditOffer.SelectedIndex = 0;
            txtEnddate.Text = "";
            txtexpirydate.Text = "";
            DropDownList1.Text = "";
            lblreward.Text = "";
            lblset.Text = "";
            lblend.Text = "";
        }

        protected void btngoal_Click(object sender, EventArgs e)
        {
            int ID = DB.Campaign_Master.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Campaign_Master.Where(p => p.TenentID == TID).Max(p => p.CampaignID) + 1) : 1;
            Database.Campaign_Master objCampaign_Master = new Database.Campaign_Master();
            objCampaign_Master.TenentID = TID;
            objCampaign_Master.LocationID = 1;
            objCampaign_Master.CampaignID = ID;
            objCampaign_Master.SpendGoal = Convert.ToDecimal(drpSpendGoal.SelectedValue);

        }

        protected void btnreward_Click(object sender, EventArgs e)
        {
            int ID = DB.Campaign_Master.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Campaign_Master.Where(p => p.TenentID == TID).Max(p => p.CampaignID) + 1) : 1;
            Database.Campaign_Master objCampaign_Master = new Database.Campaign_Master();
            objCampaign_Master.TenentID = TID;
            objCampaign_Master.LocationID = 1;
            objCampaign_Master.CampaignID = ID;
            objCampaign_Master.CRewardCreditOffer = Convert.ToInt32(drpCRewardCreditOffer.SelectedValue);
            if (rdo1.SelectedValue == "Yes,Multiple Times")
            {
                objCampaign_Master.CIsMultiple = true;
            }
            else
            {
                objCampaign_Master.CIsMultiple = false;
            }
            if (rdo1.SelectedValue == "No,Just once per customer")
            {
                objCampaign_Master.CIsSingle = true;
            }
            else
            {
                objCampaign_Master.CIsSingle = false;
            }
            if (DropDownList1.SelectedValue == "1")
            {
                objCampaign_Master.CActive = true;
            }
            else
            {
                objCampaign_Master.CActive = false;
            }
            lblreward.Visible = true;
            lblreward.Text = "Reward : Get " + drpCRewardCreditOffer.SelectedItem + "Credit" + "(" + rdo1.SelectedItem + "Redemtions" + ")";
            DB.Campaign_Master.AddObject(objCampaign_Master);
            DB.SaveChanges();
            // BindData();

        }

        protected void btncreate_Click(object sender, EventArgs e)
        {
            int ID = DB.Campaign_Master.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Campaign_Master.Where(p => p.TenentID == TID).Max(p => p.CampaignID) + 1) : 1;
            Database.Campaign_Master objCampaign_Master = new Database.Campaign_Master();
            objCampaign_Master.TenentID = TID;
            objCampaign_Master.LocationID = 1;
            objCampaign_Master.CampaignID = ID;
            objCampaign_Master.CampaignType = 1;
            objCampaign_Master.CampaignName = txtCampaignName.Text;
            if (objCampaign_Master.CampaignType == 1)
            {
                objCampaign_Master.SpendID = 1;
            }
            else
            {
                objCampaign_Master.ProductID = 2;
            }
            objCampaign_Master.SpendGoal = Convert.ToDecimal(drpSpendGoal.SelectedValue);
            objCampaign_Master.CRewardCreditOffer = Convert.ToInt32(drpCRewardCreditOffer.SelectedValue);
            objCampaign_Master.CEndDate = Convert.ToDateTime(txtEnddate.Text);
            objCampaign_Master.CExpirationDate = Convert.ToDateTime(txtexpirydate.Text);
            if (DropDownList1.SelectedValue == "1")
            {
                objCampaign_Master.CActive = true;
            }
            else
            {
                objCampaign_Master.CActive = false;
            }

            if (rdo1.SelectedValue == "Y")
            {
                objCampaign_Master.CIsMultiple = true;
            }
            else
            {
                objCampaign_Master.CIsMultiple = false;
            }
            if (rdo1.SelectedValue == "N")
            {
                objCampaign_Master.CIsSingle = true;
            }
            else
            {
                objCampaign_Master.CIsSingle = false;
            }
            lblset.Visible = true;
            lblset.Text = "Setting : Campaign End Date -" + txtEnddate.Text + "Reward Reedem Until :" + txtexpirydate.Text;
            DB.Campaign_Master.AddObject(objCampaign_Master);
            DB.SaveChanges();
            BindData();
            Clear();

        }


        protected void Listview1_ItemCommand1(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnDelete")
            {

                int MAXID = Convert.ToInt32(e.CommandArgument);
                // int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

                Database.Campaign_Master objSOJobDesc = DB.Campaign_Master.Single(p => p.CampaignID == MAXID && p.TenentID == TID);
                DB.Campaign_Master.DeleteObject(objSOJobDesc);
                DB.SaveChanges();
                BindData();

            }

            if (e.CommandName == "btnEdit")
            {
                int MAXID = Convert.ToInt32(e.CommandArgument);



                Database.Campaign_Master objtblsetupsalesh = DB.Campaign_Master.Single(p => p.CampaignID == MAXID && p.TenentID == TID);
                // drpCampaignType.SelectedValue = objtblsetupsalesh.CampaignType.ToString();
                txtCampaignName.Text = objtblsetupsalesh.CampaignName.ToString();
                drpSpendGoal.SelectedValue = objtblsetupsalesh.SpendGoal.ToString();
                drpCRewardCreditOffer.SelectedValue = objtblsetupsalesh.CRewardCreditOffer.ToString();
                txtEnddate.Text = objtblsetupsalesh.CEndDate.ToString();
                txtexpirydate.Text = objtblsetupsalesh.CExpirationDate.ToString();
                DropDownList1.SelectedValue = objtblsetupsalesh.CActive.ToString();
                //btncreate.Text = "Update";
                ViewState["Edit"] = MAXID;

            }

        }

      
        public void petsetpanel(string body5, string package)
        {
            body1.Style.Add("dispay", body5);
            pack1.Attributes["class"] = package;
        }
        public void PackSetpanel(string link, string panal)
        {
            body2.Style.Add("display", link);
            pack2.Attributes["class"] = panal;

        }
        public void panelreset(string body1, string pack1)
        {
            body4.Style.Add("display", body1);
            pack4.Attributes["class"] = pack1;
        }
        protected void lnknext_Click(object sender, EventArgs e)
        {
            lnknext.ValidationGroup = "submit";
            panel2.Visible = true;
            petsetpanel("none", "expand");
            PackSetpanel("block", "collapse");
            //if (lnknext.ValidationGroup == "submit")
            //{
            //    petsetpanel("none", "expand");
            //    PackSetpanel("block", "collapse");
            //}
            //else if(lnknext.ValidationGroup == "submit")
            //{
            //    petsetpanel("none", "expand");
            //    PackSetpanel("block", "collapse");
            //}

        }

        protected void lnkconti_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = true;
            linkpnl.ValidationGroup = "Submition";
            petsetpanel("none", "expand");
            PackSetpanel("none", "expand");
            panelreset("block", "collapse");
        }

        protected void linkpnl_Click(object sender, EventArgs e)
        {
            petsetpanel("none", "expand");
            PackSetpanel("none", "expand");
            lblmsg.Text = "Goal" + ":" + drpSpendGoal.SelectedItem.ToString();
        }

        protected void lnkcreate1_Click(object sender, EventArgs e)
        {
            int ID = DB.Campaign_Master.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.Campaign_Master.Where(p => p.TenentID == TID).Max(p => p.CampaignID) + 1) : 1;
            Database.Campaign_Master objCampaign_Master = new Database.Campaign_Master();
            objCampaign_Master.TenentID = TID;
            objCampaign_Master.LocationID = 1;
            objCampaign_Master.CampaignID = ID;
            objCampaign_Master.CampaignType = 1;
            objCampaign_Master.CampaignName = txtCampaignName.Text;
            if (objCampaign_Master.CampaignType == 1)
            {
                objCampaign_Master.SpendID = 1;
            }
            else
            {
                objCampaign_Master.ProductID = 2;
            }
            if (drpSpendGoal.SelectedValue != null)
            {
                objCampaign_Master.SpendGoal = Convert.ToDecimal(drpSpendGoal.SelectedValue);
            }
            if (txtcustom.Text != null && txtcustom.Text != "")
            {
                objCampaign_Master.SpendGoal = Convert.ToInt32(txtcustom.Text);
            }
            if (drpCRewardCreditOffer.SelectedValue!=null)
            {
                objCampaign_Master.CRewardCreditOffer = Convert.ToInt32(drpCRewardCreditOffer.SelectedValue);
            }
            if (txtEnddate.Text != null && txtEnddate.Text!="")
            {
                objCampaign_Master.CEndDate = Convert.ToDateTime(txtEnddate.Text);
            }
            if (txtexpirydate.Text != null && txtexpirydate.Text!="")
            {
                objCampaign_Master.CExpirationDate = Convert.ToDateTime(txtexpirydate.Text);
            }
            
            if (DropDownList1.SelectedValue == "1")
            {
                objCampaign_Master.CActive = true;
            }
            else
            {
                objCampaign_Master.CActive = false;
            }

            if (rdo1.SelectedValue == "Y")
            {
                objCampaign_Master.CIsMultiple = true;
            }
            else
            {
                objCampaign_Master.CIsMultiple = false;
            }
            if (rdo1.SelectedValue == "N")
            {
                objCampaign_Master.CIsSingle = true;
            }
            else
            {
                objCampaign_Master.CIsSingle = false;
            }
            lblset.Visible = true;
            lblset.Text = "Setting : Campaign End Date -" + txtEnddate.Text + "Reward Reedem Until :" + txtexpirydate.Text;
            DB.Campaign_Master.AddObject(objCampaign_Master);
            DB.SaveChanges();
            lnkcreate1.ValidationGroup = "submeet";
            lblend.Text = "Campaign End Date : " + txtEnddate.Text + "Reward Reedemable Until : " + txtexpirydate.Text;
            BindData();
            Clear();

        }

      
        protected void drpSpendGoal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSpendGoal.SelectedValue == "7.000")
            {
                txtcustom.Visible = true;
            }
            else
            {
                txtcustom.Visible = false;
            }
        }

    }
}