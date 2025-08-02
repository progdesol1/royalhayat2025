using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Database;

namespace Web
{
    public partial class TBLLOCATION : System.Web.UI.Page
    {

        CallEntities DB = new CallEntities();
        static private string Baseurl = ConfigurationManager.AppSettings["Baseurl"];
        static private string bearerToken = ConfigurationManager.AppSettings["bearerToken"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CsTBLLOCATION objCsplancustinvoiceHD = new CsTBLLOCATION();

                List<CsTBLLOCATION> Listplan = GetUserList();
                Listview1.DataSource = Listplan;
                Listview1.DataBind();
            }
        }

        public static List<CsTBLLOCATION> GetCompanyList()
        {
            List<CsTBLLOCATION> CompanyList = new List<CsTBLLOCATION>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.GetAsync("api/Getlocation").Result;
                //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<CsTBLLOCATIONList>(EmpResponse);
                    CompanyList = rootobject.data.ToList();
                }
            }
            return CompanyList;
        }

        public static List<CsTBLLOCATION> GetUserList()
        {
            CsTBLLOCATION objUserMst = new CsTBLLOCATION();
            objUserMst.TenentID = 1;
          //  objUserMst.LOCATIONID = 1;
            List<CsTBLLOCATION> Userlist = new List<CsTBLLOCATION>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/ListLocation", objUserMst).Result;
                //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<CsTBLLOCATIONList>(EmpResponse);
                    Userlist = rootobject.data.ToList();
                }
            }
            return Userlist;
        }

        public void PostSingelLocation()
        {
            CsTBLLOCATION _objplanmealsetup = new CsTBLLOCATION();
            _objplanmealsetup.TenentID = 521;
            _objplanmealsetup.LOCATIONID = 1;
            _objplanmealsetup.PHYSICALLOCID = "HJI";
            _objplanmealsetup.LOCNAME1 = "Pune";
            _objplanmealsetup.LOCNAME2 = "Pune";
            _objplanmealsetup.LOCNAME3 = "Pune";
            _objplanmealsetup.ADDRESS = "Pune";
            _objplanmealsetup.DEPT = "IT";
            _objplanmealsetup.SECTIONCODE = "IT";
            _objplanmealsetup.ACCOUNT = "9888788888";
            _objplanmealsetup.MAXDISCOUNT = 7;
            _objplanmealsetup.USERID = "johar";
            _objplanmealsetup.ENTRYDATE = DateTime.Now;
            _objplanmealsetup.ENTRYTIME = DateTime.Now;
            _objplanmealsetup.UPDTTIME = DateTime.Now;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/Insertlocation", _objplanmealsetup).Result;

                //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<GetCsTBLLOCATION>(EmpResponse);
                    // _objicuom = rootobject.data;
                }
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PostSingelLocation();
        }

        public void Postupdatelocation()
        {
            CsTBLLOCATION _objplanmealsetup = new CsTBLLOCATION();
            _objplanmealsetup.TenentID = 521;
            _objplanmealsetup.LOCATIONID = 1;
            _objplanmealsetup.PHYSICALLOCID = "HJI";
            _objplanmealsetup.LOCNAME1 = "Banglore";
            _objplanmealsetup.LOCNAME2 = "Banglore";
            _objplanmealsetup.LOCNAME3 = "Banglore";
            _objplanmealsetup.ADDRESS = "Banglore";
            _objplanmealsetup.DEPT = "IT";
            _objplanmealsetup.SECTIONCODE = "IT";
            _objplanmealsetup.ACCOUNT = "9888788888";
            _objplanmealsetup.MAXDISCOUNT = 7;
            _objplanmealsetup.USERID = "johar";
            _objplanmealsetup.ENTRYDATE = DateTime.Now;
            _objplanmealsetup.ENTRYTIME = DateTime.Now;
            _objplanmealsetup.UPDTTIME = DateTime.Now;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/UpdateLocation", _objplanmealsetup).Result;

                //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<GetCsTBLLOCATION>(EmpResponse);
                    // _objicuom = rootobject.data;
                }
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Postupdatelocation();
        }

        public void PostSingellocationDelete()
        {
            CsTBLLOCATION _objplanmealsetup = new CsTBLLOCATION();
            _objplanmealsetup.TenentID = 521;
            _objplanmealsetup.LOCATIONID = 1;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/Deletelocation", _objplanmealsetup).Result;

                //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<GetCsTBLLOCATION>(EmpResponse);
                    // _objicuom = rootobject.data;
                }
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            PostSingellocationDelete();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            List<Database.TBLLOCATION> List = new List<Database.TBLLOCATION>();

            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                CheckBox chkchecklock = (CheckBox)Listview1.Items[i].FindControl("chkchecklock");
                Label lblID2 = (Label)Listview1.Items[i].FindControl("lblID2");
                int id = Convert.ToInt32(lblID2.Text);
                if (chkchecklock.Checked == true)
                {
                    Database.TBLLOCATION obj = DB.TBLLOCATIONs.Single(p => p.TenentID == 1 && p.LOCATIONID == id);
                    List.Add(obj);
                }
            }

            List<Database.TBLLOCATION> Userlist = List;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/GetLocationList", Userlist).Result;
                //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    //var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    //var rootobject = JsonConvert.DeserializeObject<CsTBLLOCATIONList>(EmpResponse);
                    //Userlist = rootobject.data.ToList();
                }
            }


            ListView2.DataSource = List;
            ListView2.DataBind();
        }
    }
}