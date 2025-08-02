using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.IO;

namespace Web.Master
{
    public partial class HelpDeskReport : System.Web.UI.Page
    {
        int TID = 0;
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if (!IsPostBack)
            {
                BindList();
            }
        }
        public void BindList()
        {
            List<Database.CRMMainActivity> Listmain = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk").ToList();
            List<Database.CRMMainActivity> DeptList = Listmain.GroupBy(p => p.TickDepartmentID).Select(g => g.FirstOrDefault()).ToList();
            ListView3.DataSource = DeptList;
            ListView3.DataBind();
            List<Database.CRMMainActivity> LOCList = Listmain.GroupBy(p => p.TickPhysicalLocation).Select(g => g.FirstOrDefault()).ToList();
            ListView1.DataSource = LOCList;
            ListView1.DataBind();
            List<Database.CRMMainActivity> catList = Listmain.GroupBy(p => p.TickCatID).Select(g => g.FirstOrDefault()).ToList();
            ListView2.DataSource = catList;
            ListView2.DataBind();
            List<Database.CRMMainActivity> subcatList = Listmain.GroupBy(p => p.TickSubCatID).Select(g => g.FirstOrDefault()).ToList();
            ListView4.DataSource = subcatList;
            ListView4.DataBind();
        }
        public string GetDept(int ID)
        {
            if(DB.DeptITSupers.Where(p=>p.TenentID == TID && p.DeptID == ID).Count() > 0)
            {
                return DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == ID).DeptName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetLOC(int ID)
        {
            if(DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.REFID == ID).Count() > 0)
            {
                return DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.REFID == ID).REFNAME1;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetCAT(int ID)
        {
            if(DB.ICCATEGORies.Where(p => p.TenentID == TID && p.CATTYPE == "HelpDesk" && p.CATID == ID).Count() > 0)
            {
                return DB.ICCATEGORies.Single(p => p.TenentID == TID && p.CATTYPE == "HelpDesk" && p.CATID == ID).CATNAME;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetSubCat(int ID)
        {
            if (DB.ICSUBCATEGORies.Where(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.SUBCATID == ID).Count() > 0)
            {
                return DB.ICSUBCATEGORies.Single(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.SUBCATID == ID).SUBCATNAME;
            }
            else
            {
                return "Not Found";
            }
        }

        protected void ListView3_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblDeptID = (Label)e.Item.FindControl("lblDeptID");            
            Label DeptCount = (Label)e.Item.FindControl("Label2");
            int Deptid = Convert.ToInt32(lblDeptID.Text);
            int count = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk" && p.TickDepartmentID == Deptid).Count();
            DeptCount.Text = count.ToString();
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            Label lbllocid = (Label)e.Item.FindControl("lbllocid");  
            Label LOCCount = (Label)e.Item.FindControl("Label4");
            int LOCid = Convert.ToInt32(lbllocid.Text);
            int count = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk" && p.TickPhysicalLocation == LOCid).Count();
            LOCCount.Text = count.ToString();
        }

        protected void ListView2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblCatid = (Label)e.Item.FindControl("lblCatid");
            Label CatCount = (Label)e.Item.FindControl("Label6");
            int Catid = Convert.ToInt32(lblCatid.Text);
            int count = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk" && p.TickCatID == Catid).Count();
            CatCount.Text = count.ToString();

        }

        protected void ListView4_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblSubCatid = (Label)e.Item.FindControl("lblSubCatid");
            Label SubCatCount = (Label)e.Item.FindControl("Label8");
            int subCatid = Convert.ToInt32(lblSubCatid.Text);
            int count = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "helpdesk" && p.TickSubCatID == subCatid).Count();
            SubCatCount.Text = count.ToString();
        }

        protected void lblexportDT_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        public void ExportToExcel()
        {
            List<HelpDeskExc> List1 = new List<HelpDeskExc>();
            HelpDeskExc DeptHead = new HelpDeskExc();
            DeptHead.Main = "Department";
            DeptHead.Count = "Count";
            List1.Add(DeptHead);
            HelpDeskExc DeptList = new HelpDeskExc();
            for (int i = 0; i < ListView3.Items.Count; i++)
            {
                Label Label1 = (Label)ListView3.Items[i].FindControl("Label1");
                Label Label2 = (Label)ListView3.Items[i].FindControl("Label2");
                DeptList.Main = Label1.Text.ToString();
                DeptList.Count = Label2.Text.ToString();
                List1.Add(DeptList);
            }
            if (List1.Count() > 0)
            {
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename", "Summarized.xls"));
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GridView gvdetails = new GridView();
                gvdetails.ShowHeader = false;
                gvdetails.DataSource = List1;
                gvdetails.AllowPaging = false;
                gvdetails.DataBind();
                gvdetails.HeaderRow.Style.Add("font-weight", "bold");
                gvdetails.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }

        }
    }
    public class HelpDeskExc
    {
        public string Main { get; set; }
        public string Count { get; set; }
      
    }
}