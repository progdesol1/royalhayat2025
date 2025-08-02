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
    public partial class TBLPRODUCT : System.Web.UI.Page
    {

        CallEntities DB = new CallEntities();
        static private string Baseurl = ConfigurationManager.AppSettings["Baseurl"];
        static private string bearerToken = ConfigurationManager.AppSettings["bearerToken"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CstblPRODUCT objCsplancustinvoiceHD = new CstblPRODUCT();

                List<CstblPRODUCT> Listplan = GetUserList();
                
                Listview1.DataSource = Listplan;
                Listview1.DataBind();

            }

        }

        public static List<CstblPRODUCT> GetCompanyList()
        {
            List<CstblPRODUCT> CompanyList = new List<CstblPRODUCT>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.GetAsync("api/Getproduct").Result;
                //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<CstblPRODUCTList>(EmpResponse);
                    CompanyList = rootobject.data.ToList();
                }
            }
            return CompanyList;
        }

        public static List<CstblPRODUCT> GetUserList()
        {
            CstblPRODUCT objUserMst = new CstblPRODUCT();
            objUserMst.TenentID = 2;
           // objUserMst.LOCATION_ID = 1;
           
            
            List<CstblPRODUCT> Userlist = new List<CstblPRODUCT>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/Listproduct", objUserMst).Result;
                //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<CstblPRODUCTList>(EmpResponse);
                    Userlist = rootobject.data.ToList();
                }
            }
            return Userlist;
        }

        public void PostSingelproduct()
        {
            CstblPRODUCT _objplanmealsetup = new CstblPRODUCT();
            _objplanmealsetup.TenentID = 11;
            _objplanmealsetup.LOCATION_ID = 1;
            _objplanmealsetup.MYPRODID = 1;
            _objplanmealsetup.UserProdID = "112";
            _objplanmealsetup.BarCode ="12121212";
            _objplanmealsetup.AlternateCode1 = "kskskodo";
            _objplanmealsetup.AlternateCode2 ="sksoksoksk";
            _objplanmealsetup.UOM = 11;
            _objplanmealsetup.ProdName1 = "Laptop";
            _objplanmealsetup.ProdName2 = "Laptop";
            _objplanmealsetup.ProdName3 = "Laptop";
            _objplanmealsetup.Brand = "Apple";


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/InsertProduct", _objplanmealsetup).Result;

                //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<GetCstblPRODUCT>(EmpResponse);
                    // _objicuom = rootobject.data;
                }
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PostSingelproduct();
        }
        public void Postupdateproduct()
        {
            CstblPRODUCT _objplanmealsetup = new CstblPRODUCT();
            _objplanmealsetup.TenentID = 11;
            _objplanmealsetup.LOCATION_ID = 1;
            _objplanmealsetup.MYPRODID = 1;
            _objplanmealsetup.UserProdID = "112";
            _objplanmealsetup.BarCode = "12121212";
            _objplanmealsetup.AlternateCode1 = "kskskodo";
            _objplanmealsetup.AlternateCode2 = "sksoksoksk";
            _objplanmealsetup.UOM = 11;
            _objplanmealsetup.ProdName1 = "Mobile";
            _objplanmealsetup.ProdName2 = "Mobile";
            _objplanmealsetup.ProdName3 = "Mobile";
            _objplanmealsetup.Brand = "Vivo";


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/Updateproduct", _objplanmealsetup).Result;

                //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<GetCstblPRODUCT>(EmpResponse);
                    // _objicuom = rootobject.data;
                }
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Postupdateproduct();
        }

        public void PostSingelproductDelete()
        {
            CstblPRODUCT _objplanmealsetup = new CstblPRODUCT();
            _objplanmealsetup.TenentID = 11;
            _objplanmealsetup.LOCATION_ID = 1;
            _objplanmealsetup.MYPRODID = 1;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/Deleteproduct", _objplanmealsetup).Result;

                //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var rootobject = JsonConvert.DeserializeObject<GetCstblPRODUCT>(EmpResponse);
                    // _objicuom = rootobject.data;
                }
            }

        }

        protected void btndelte_Click(object sender, EventArgs e)
        {
            PostSingelproductDelete();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            List<Database.TBLPRODUCT> List = new List<Database.TBLPRODUCT>();
            
            for (int i = 0; i < Listview1.Items.Count; i++)
            {
                CheckBox chkProdcheck = (CheckBox)Listview1.Items[i].FindControl("chkProdcheck");
                Label lblID2 = (Label)Listview1.Items[i].FindControl("lblID2");
               int id = Convert.ToInt32(lblID2.Text);
                if (chkProdcheck.Checked == true)
                {
                    Database.TBLPRODUCT obj = DB.TBLPRODUCTs.Single(p => p.TenentID == 2 && p.MYPRODID == id);
                    List.Add(obj);
                }
            }

            List<Database.TBLPRODUCT> Userlist = List;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/GetProducytList", Userlist).Result;
                //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    //var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    //var rootobject = JsonConvert.DeserializeObject<CstblPRODUCTList>(EmpResponse);
                    //Userlist = rootobject.data.ToList();
                }
            }


            ListView2.DataSource = List;
            ListView2.DataBind();
        }

    }
}