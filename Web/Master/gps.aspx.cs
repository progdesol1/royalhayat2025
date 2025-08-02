using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web
{
    public partial class gps : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            if(!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    string str = Request.QueryString["date"];
                    string str1 = str.Trim(new Char[] { '{', '}' });
                    string str2 = str1.Replace(':', ',');
                    string[] Splitstr = str2.Split(new string[] { "\",\"" }, StringSplitOptions.None);
                    if (str != "")
                    {
                        Database.tblgprsdata objquery = new Database.tblgprsdata();
                        objquery.Date = str.ToString();
                        objquery.latitude = Splitstr[1];
                        objquery.logitude = Splitstr[3];
                        string[] str5 = Splitstr[5].Split(new string[] { "\"" }, StringSplitOptions.None);
                        string APPDevoceID = str5[0].ToString();
                        //string DeviceID = DB.tbl_Employee.Where(p => p.DeviceID == APPDevoceID).FirstOrDefault().DeviceID;
                        if (DB.tbl_Employee.Where(p => p.DeviceID == APPDevoceID && p.TenentID == TID && p.EmployeeType == "Driver").Count() > 0)
                        {
                            objquery.DeviceID = str5[0];
                            objquery.DateTime = DateTime.Now;
                            DB.tblgprsdatas.AddObject(objquery);
                            DB.SaveChanges();
                        }
                       
                    }
                }
            }
        }
    }
}