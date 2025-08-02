using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data.Objects;

using System.Configuration;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using Classes;
using System.Web.Script.Serialization;

namespace Web.ReportMst
{
    public partial class DriverMap2 : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        static private string Baseurl = ConfigurationManager.AppSettings["Baseurl"];
        static private string bearerToken = ConfigurationManager.AppSettings["bearerToken"];
        int TID = 9000007;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EmpDestination();
            }
            string KM = GetDistHide.Value.ToString();
            string GADD = EmpDest.SelectedValue;
            txtDestination.Value = GADD;
            var DT = DateTime.Now.Date;
            if (DB.NewAttandances.Where(p => p.TenentID == TID && p.EmployeeID == 1 && p.UploadDate == DT && p.GoogleName == GADD).Count() > 0)
            {
                Database.NewAttandance obj = DB.NewAttandances.Where(p => p.TenentID == TID && p.EmployeeID == 1 && p.GoogleName == GADD && p.UploadDate == DT).OrderByDescending(p => p.CheckInID).FirstOrDefault();
                if (obj.InTime != null && obj.OutTime != null)
                {
                    CheckIn.Text = "Check In";
                }
                else if (obj.InTime != null && obj.OutTime == null)
                {
                    CheckIn.Text = "Check Out";
                }
            }
        }

        protected void CheckIn_Click(object sender, EventArgs e)
        {
            //CheckIn.Attributes["Style"] = "display:none;";
            var DT = DateTime.Now.Date;
            string GADD = EmpDest.SelectedValue;
            string LBN = CheckIn.Text;
            bool Flag = false;
            string[] Source = txtSource.Value.Split(',');
            string Lat = Source[0];
            string Long = Source[1];
            Database.NewAttandance OBJ = new Database.NewAttandance();
            if (LBN == "Check In")
            {
                OBJ.TenentID = TID;
                OBJ.LocationID = 1;
                OBJ.EmployeeID = 1;
                OBJ.CheckInID = DB.NewAttandances.Where(p => p.TenentID == TID && p.EmployeeID == 1).Count() > 0 ? Convert.ToInt32(DB.NewAttandances.Where(p => p.TenentID == TID && p.EmployeeID == 1).Max(p => p.CheckInID) + 1) : 1;
                OBJ.InTime = DateTime.Now;
                OBJ.OutTime = null;
            }
            else
            {
                OBJ = DB.NewAttandances.Where(p => p.TenentID == TID && p.EmployeeID == 1 && p.GoogleName == GADD && p.UploadDate == DT).OrderByDescending(p => p.CheckInID).FirstOrDefault();
                OBJ.OutTime = DateTime.Now;
                Flag = true;
            }
            OBJ.CheckInType = 1;
            OBJ.UserID = 0;
            OBJ.isAbsent = false;
            OBJ.GoogleName = EmpDest.SelectedValue;
            OBJ.Latitute = Lat;
            OBJ.Longitute = Long;
            OBJ.Active = true;
            OBJ.Deleted = true;
            OBJ.CRUP_ID = 0;
            OBJ.UploadDate = DateTime.Now;
            OBJ.Uploadby = "";
            if (Flag == false)
                DB.NewAttandances.AddObject(OBJ);
            DB.SaveChanges();

            if (LBN == "Check In")
                CheckIn.Text = "Check Out";
            else if (LBN == "Check Out")
                CheckIn.Text = "Check In";

            //if (LBN == "Check In")
            //{
            //    Baseurl = "localhost:1953/";
            //    Database.NewAttandance OBJAPI = new Database.NewAttandance();
            //    OBJAPI.TenentID = TID;
            //    OBJAPI.LocationID = 1;
            //    OBJAPI.EmployeeID = 1;
            //    OBJAPI.CheckInID = DB.NewAttandances.Where(p => p.TenentID == TID && p.EmployeeID == 1).Count() > 0 ? Convert.ToInt32(DB.NewAttandances.Where(p => p.TenentID == TID && p.EmployeeID == 1).Max(p => p.CheckInID) + 1) : 1;
            //    OBJAPI.InTime = DateTime.Now;
            //    OBJAPI.OutTime = null;
            //    OBJAPI.CheckInType = 1;
            //    OBJAPI.UserID = 0;
            //    OBJAPI.isAbsent = false;
            //    OBJAPI.GoogleName = EmpDest.SelectedValue;
            //    OBJAPI.Latitute = Lat;
            //    OBJAPI.Longitute = Long;
            //    OBJAPI.Active = true;
            //    OBJAPI.Deleted = true;
            //    OBJAPI.CRUP_ID = 0;
            //    OBJAPI.UploadDate = DateTime.Now;
            //    OBJAPI.Uploadby = "";

            //    using (var client = new HttpClient())
            //    {
            //        client.BaseAddress = new Uri(Baseurl);
            //        client.DefaultRequestHeaders.Clear();
            //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //        client.DefaultRequestHeaders.Add("Authentication", bearerToken);
            //        HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/InsertNewAttandance", OBJAPI).Result;
            //        //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
            //        if (responseMessage.IsSuccessStatusCode)
            //        {
            //            //var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
            //            //var rootobject = JsonConvert.DeserializeObject<Planmealcustinvo>(EmpResponse);
            //            //CompanyList = rootobject.data.ToList();
            //        }
            //    }
            //}
            //if (LBN == "Check Out")
            //{
            //    Baseurl = "localhost:1953/";
            //    Database.NewAttandance OBJAPI = DB.NewAttandances.Where(p => p.TenentID == TID && p.EmployeeID == 1 && p.GoogleName == GADD && p.UploadDate == DT).OrderByDescending(p => p.CheckInID).FirstOrDefault();
            //    OBJAPI.TenentID = TID;
            //    OBJAPI.LocationID = 1;
            //    OBJAPI.EmployeeID = 1;
            //    OBJAPI.CheckInID = OBJAPI.CheckInID;
            //    OBJAPI.OutTime = DateTime.Now;
            //    OBJAPI.CheckInType = 1;
            //    OBJAPI.UserID = 0;
            //    OBJAPI.isAbsent = false;
            //    OBJAPI.GoogleName = EmpDest.SelectedValue;
            //    OBJAPI.Latitute = Lat;
            //    OBJAPI.Longitute = Long;
            //    OBJAPI.Active = true;
            //    OBJAPI.Deleted = true;
            //    OBJAPI.CRUP_ID = 0;
            //    OBJAPI.UploadDate = DateTime.Now;
            //    OBJAPI.Uploadby = "";

            //    using (var client = new HttpClient())
            //    {
            //        client.BaseAddress = new Uri(Baseurl);
            //        client.DefaultRequestHeaders.Clear();
            //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //        client.DefaultRequestHeaders.Add("Authentication", bearerToken);
            //        HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/UpdateNewAttendance", OBJAPI).Result;
            //        //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
            //        if (responseMessage.IsSuccessStatusCode)
            //        {
            //            //var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
            //            //var rootobject = JsonConvert.DeserializeObject<Planmealcustinvo>(EmpResponse);
            //            //CompanyList = rootobject.data.ToList();
            //        }
            //    }
            //}
        }
        public void EmpDestination()
        {
            List<Database.EmployeeCheckIn> EmpList = DB.EmployeeCheckIns.Where(p => p.TenentID == TID && p.EmployeeID == 1).ToList();
            EmpDest.DataSource = EmpList;
            EmpDest.DataTextField = "GoogleName";
            EmpDest.DataValueField = "GoogleName";
            EmpDest.DataBind();
        }

        protected void EmpDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GN = EmpDest.SelectedValue;
            txtDestination.Value = GN;

            var DT = DateTime.Now.Date;
            if (DB.NewAttandances.Where(p => p.TenentID == TID && p.EmployeeID == 1 && (p.UploadDate) == DT && p.GoogleName == GN).Count() > 0)
            {
                Database.NewAttandance obj = DB.NewAttandances.Where(p => p.TenentID == TID && p.EmployeeID == 1 && p.GoogleName == GN && p.UploadDate == DT).OrderByDescending(p => p.CheckInID).FirstOrDefault();
                if (obj.InTime != null && obj.OutTime != null)
                {
                    CheckIn.Text = "Check In";
                }
                else if (obj.InTime != null && obj.OutTime == null)
                {
                    CheckIn.Text = "Check Out";
                }

            }
        }









    }
}