using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.ReportMst
{
    public partial class AMPM : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 6;
        string FDELIVERY = "";
        string TDELIVERY = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                //AMPMRep();
                txtdateFrom.Text = txtdateTO.Text = DateTime.Now.ToShortDateString();
                DRPBIND();
                FDELIVERY = DrpFromDelivery.SelectedItem.ToString();
                TDELIVERY = DrpToDelivery.SelectedItem.ToString();
                Page.Header.Title = String.Format(FDELIVERY + "-" + TDELIVERY + " Driver checklist");
            }
        }
        public void AMPMRep()
        {
            Listview1.DataSource = DB.View_DriverCheckList;//DB.View_DriverCheckList.GroupBy(p=>p.CustomerID).Select(p=>p.FirstOrDefault());
            Listview1.DataBind();
        }
       
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            FDELIVERY = DrpFromDelivery.SelectedItem.ToString();
            TDELIVERY = DrpToDelivery.SelectedItem.ToString();
            Page.Header.Title = String.Format(FDELIVERY + "-" + TDELIVERY + " Driver checklist");
            DateTime? FDT = null;
            DateTime? TDT = null;
            if (txtdateFrom.Text != "")
                FDT = Convert.ToDateTime(txtdateFrom.Text);
            if (txtdateTO.Text != "")
                TDT = Convert.ToDateTime(txtdateTO.Text);
            int FDID = Convert.ToInt32(DrpFromDriver.SelectedValue);
            int TDID = Convert.ToInt32(DrpToDriver.SelectedValue);
            int Fdelivery = Convert.ToInt32(DrpFromDelivery.SelectedValue);
            int Tdelivery = Convert.ToInt32(DrpToDelivery.SelectedValue);
            List<Database.View_DriverCheckList> List = DB.View_DriverCheckList.ToList();
            if(txtdateFrom.Text != "" && txtdateTO.Text != "")
            {
                if (FDT.Value.Date >= DateTime.Now.Date)
                {
                    Listview1.DataSource = List.Where(p => (p.ExpDate.Value.Date >= FDT.Value.Date && p.ExpDate.Value.Date <= TDT.Value.Date) || (p.DriverID >= FDID && p.DriverID <= TDID) || (p.DeliverTime >= Fdelivery && p.DeliverTime <= Tdelivery));//DB.View_DriverCheckList.Where(p => p.ExpDate.Value.Date >= FDT.Value.Date && p.ExpDate.Value.Date <= TDT.Value.Date);
                    Listview1.DataBind();
                }
            }            
            else
            {
                Listview1.DataSource = List.Where(p => (p.DriverID >= FDID && p.DriverID <= TDID) || (p.DeliverTime >= Fdelivery && p.DeliverTime <= Tdelivery));//DB.View_DriverCheckList.Where(p => p.ExpDate.Value.Date >= FDT.Value.Date && p.ExpDate.Value.Date <= TDT.Value.Date);
                Listview1.DataBind();
            }
        }//DeliverTime
        public void DRPBIND()
        {
            DrpFromDriver.DataSource = DB.View_DriverCheckList.GroupBy(p => p.DriverName).Select(p => p.FirstOrDefault()).OrderBy(p => p.DriverID).Distinct();//DB.tbl_Employee.Where(p => p.TenentID == TID).OrderBy(p => p.employeeID);
            DrpFromDriver.DataTextField = "DriverName";
            DrpFromDriver.DataValueField = "DriverID";
            DrpFromDriver.DataBind();
            //DrpFromDriver.Items.Insert(0, new ListItem("-- Select --", "0"));

            DrpToDriver.DataSource = DB.View_DriverCheckList.GroupBy(p => p.DriverName).Select(p => p.FirstOrDefault()).OrderByDescending(p => p.DriverID);//DB.tbl_Employee.Where(p => p.TenentID == TID).OrderByDescending(p => p.employeeID);
            DrpToDriver.DataTextField = "DriverName";
            DrpToDriver.DataValueField = "DriverID";
            DrpToDriver.DataBind();
            //DrpToDriver.Items.Insert(0, new ListItem("-- Select --", "0"));

            DrpFromDelivery.DataSource = DB.View_DriverCheckList.GroupBy(p=>p.Delivery).Select(p=>p.FirstOrDefault()).OrderBy(p=>p.Delivery);//DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime").OrderBy(p => p.REFID);
            DrpFromDelivery.DataTextField = "Delivery";
            DrpFromDelivery.DataValueField = "DeliverTime";
            DrpFromDelivery.DataBind();
            //DrpFromDelivery.Items.Insert(0, new ListItem("-- Select --", "0"));

            DrpToDelivery.DataSource = DB.View_DriverCheckList.GroupBy(p => p.Delivery).Select(p => p.FirstOrDefault()).OrderByDescending(p=>p.Delivery);//DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Food" && p.REFSUBTYPE == "DeliveryTime").OrderByDescending(p => p.REFID);
            DrpToDelivery.DataTextField = "Delivery";
            DrpToDelivery.DataValueField = "DeliverTime";
            DrpToDelivery.DataBind();
            //DrpToDelivery.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        public string getMealType(int Type1)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == Type1 && p.REFTYPE == "Food" && p.REFSUBTYPE == "MealType").Count() > 0)
            {
                string TypeMeal = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == Type1 && p.REFTYPE == "Food" && p.REFSUBTYPE == "MealType").REFNAME1;
                return TypeMeal;
            }
            else
            {
                return "";
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }


    }
}
