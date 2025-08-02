using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Transactions;
//using NewAdmin;
namespace Web.CRM.UserControl
{
    public partial class ContactTextBox : System.Web.UI.UserControl
    {

        Database.CallEntities DB = new Database.CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
            }
        }
        #region only for Contact Textbox
        protected void txtContactName_TextChanged(object sender, EventArgs e)
        {
            //lblCustomer1.Text = "";
            //if (!string.IsNullOrEmpty(txtContactName.Text))
            //{
            //    var exist = DB.TBLCONTACT.Where(c => c.PersName1 == txtContactName.Text && c.Active == "Y");

            //    var UserList = DB.TBLCONTACT.SingleOrDefault(p => p.PersName1 == txtContactName.Text && p.Active == "Y");
            //    if (exist.Count() <= 0)
            //    {

            //        //lblCustomer1.Text = "Customer Name  Available";
            //    }
            //    else
            //    {

            //        ViewState["compId"] = UserList.ContactMyID;
                    //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Customer Name Already Taken.');", true);
                    //lblCustomerName.Text = "Customer Name Already Taken"; 
            //        ModalPopupExtender4ContactTextBox.TargetControlID = h1.ID;
            //        ModalPopupExtender4ContactTextBox.Show();
            //        //BindData1();
            //        labelCopop.Text = UserList.PersName1;
            //        lblmopop.Text = UserList.MOBPHONE;
            //        lblEmailpop.Text = UserList.EmailId;
            //        lblFaxpop.Text = UserList.FaxID;
            //        lblBuspop.Text = UserList.BUSPHONE1;

            //    }
            //}
            //else
            //{
            //    //lblCustomer1.Text = "Insert The Customer Name Available";
            //}
        }
        protected void btnContact_Click(object sender, EventArgs e)
        {
           // lblCustomer1.Text = "";
           // var exist = DB.TBLCONTACT.Where(c => c.PersName1 == txtContactName.Text);
            //if (exist.Count() <= 0)
            //{

            //}
            //else
            //{
               // lblCustomer1.Text = "Contact Name 1 Is Duplicate";
           // }
           // ModalPopupExtender4ContactTextBox.TargetControlID = h1.ID;
           // ModalPopupExtender4ContactTextBox.Show();
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            int CID = Convert.ToInt32(ViewState["compId"]);
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            Database.TBLCONTACT objtbl_CONTACT = DB.TBLCONTACTs.Single(p => p.TenentID == TID && p.ContactMyID == CID);
           // btnAdd.Text = "Update";
          //  txtContactName.Text = objtbl_CONTACT.PersName1.ToString();

            //   txtEmail.Text = objtbl_CONTACT.EMAIL1.ToString();
            //txtMobileNo.Text = objtbl_CONTACT.MOBPHONE.ToString();
            //drpItManager.SelectedValue = objtbl_CONTACT.ITMANAGER.ToString();
            //txtAddress.Text = objtbl_CONTACT.ADDR1.ToString();
            //txtAddress2.Text = objtbl_CONTACT.ADDR2.ToString();
            //txtCity.Text = objtbl_CONTACT.CITY.ToString();
            int COID = Convert.ToInt32(objtbl_CONTACT.COUNTRYID);
           // drpCustomer_id.SelectedValue = DB.CRM_tblCOUNTRY.Single(p => p.COUNTRYID == COID).ISO3166_1_2LetterCode;
            //bindState();
            //drpState.SelectedValue = objtbl_CONTACT.STATE.ToString();
            //txtPostalCode.Text = objtbl_CONTACT.POSTALCODE.ToString();
            //txtZipCode.Text = objtbl_CONTACT.ZIPCODE.ToString();
            //txtPostalCode.Text = objtbl_CONTACT.POSTALCODE.ToString();
            //drpMyCounLocID.SelectedValue = objtbl_CONTACT.MYCONLOCID.ToString();
            //tags_2.Text = objtbl_CONTACT.EmailId.ToString();
            //tags_3.Text = objtbl_CONTACT.FaxID.ToString();
            //tags_4.Text = objtbl_CONTACT.BUSPHONE1.ToString();
            //txtRemark.Text = objtbl_CONTACT.REMARKS.ToString();
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}