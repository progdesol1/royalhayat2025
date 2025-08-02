using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.Master
{
    public partial class Smiley : System.Web.UI.Page
    {
        Database.CallEntities DB = new Database.CallEntities();
        int TID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                lblmsg.Visible = false;
                lblmsg2.Visible = false;
                lblmsg3.Visible = false;
            }
        }
             
        
        protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
        {
            lblmsg.Visible = false;
            lblmsg2.Visible = false;
            lblmsg3.Visible = false;
            int MYTRANSID = Convert.ToInt32(Request.QueryString["TerminalID"]);
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            Database.ServeyTable objProject_Details = new Database.ServeyTable();
            int MAXID = DB.ServeyTables.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ServeyTables.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
            objProject_Details.TenentID = TID;
            objProject_Details.ID = MAXID;
            objProject_Details.ResultY = 1;
            objProject_Details.SurDateTime = DateTime.Now;
            objProject_Details.ResultColor = "Moderate";
            objProject_Details.Remark = "Yellow";
            objProject_Details.Active = "Y";
            objProject_Details.Deleted = "Y";
            objProject_Details.TerminalID = MYTRANSID.ToString();
            DB.ServeyTables.AddObject(objProject_Details);
            DB.SaveChanges();
            lblmsg2.Visible = true;
            lblmsg2.Text = "Thanks for your Feedback we are working hard to meet your expectations";

            lblwait2.Text = "wait for 5 seconds";
            System.Threading.Thread.Sleep(5000);
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {

            lblmsg.Visible = false;
            lblmsg2.Visible = false;
            lblmsg3.Visible = false;
            int MYTRANSID = Convert.ToInt32(Request.QueryString["TerminalID"]);
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            Database.ServeyTable objProject_Details = new Database.ServeyTable();
            int MAXID = DB.ServeyTables.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ServeyTables.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
            objProject_Details.TenentID = TID;
            objProject_Details.ID = MAXID;
            objProject_Details.ResultG = 1;
            objProject_Details.SurDateTime = DateTime.Now;
            objProject_Details.ResultColor = "Like";
            objProject_Details.Remark = "Green";
            objProject_Details.Active = "Y";
            objProject_Details.Deleted = "Y";
            objProject_Details.TerminalID = MYTRANSID.ToString();
            DB.ServeyTables.AddObject(objProject_Details);
            DB.SaveChanges();
            lblmsg.Visible = true;
            lblmsg.Text = "Thanks for your Feedback we are working hard to meet your expectations";
            System.Threading.Thread.Sleep(5000);
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            lblmsg.Visible = false;
            lblmsg2.Visible = false;
            lblmsg3.Visible = false;    
            int MYTRANSID = Convert.ToInt32(Request.QueryString["TerminalID"]);
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            Database.ServeyTable objProject_Details = new Database.ServeyTable();
            int MAXID = DB.ServeyTables.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ServeyTables.Where(p => p.TenentID == TID).Max(p => p.ID) + 1) : 1;
            objProject_Details.TenentID = TID;
            objProject_Details.ID = MAXID;
            objProject_Details.ResultR = 1;
            objProject_Details.SurDateTime = DateTime.Now;
            objProject_Details.ResultColor = "Don't Like";
            objProject_Details.Remark = "Red";
            objProject_Details.Active = "Y";
            objProject_Details.Deleted = "Y";
            objProject_Details.TerminalID = MYTRANSID.ToString();
            DB.ServeyTables.AddObject(objProject_Details);
            DB.SaveChanges();
            lblmsg3.Visible = true;
            lblmsg3.Text = "Thanks for your Feedback we are working hard to meet your expectations";
            System.Threading.Thread.Sleep(5000);
        }
    }
}