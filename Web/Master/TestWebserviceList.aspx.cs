using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Configuration;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

namespace Web.Master
{
    public partial class TestWebserviceList : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();

        static private string Baseurl = ConfigurationManager.AppSettings["Baseurl"];
        static private string bearerToken = ConfigurationManager.AppSettings["bearerToken"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<Database.planmealcustinvoice> ListDt = DB.planmealcustinvoices.Where(p => p.TenentID == 9 && p.MYTRANSID == 2).ToList();

            List<Database.planmealcustinvoice> ListAdd = new List<Database.planmealcustinvoice>();

            foreach (Database.planmealcustinvoice item in ListDt)
            {
                Database.planmealcustinvoice Obj = new Database.planmealcustinvoice();
                Obj.TenentID = item.TenentID;
                Obj.MYTRANSID = 3;
                Obj.DeliveryID = item.DeliveryID;
                Obj.MYPRODID = item.MYPRODID;
                Obj.UOM = item.UOM;
                Obj.LOCATION_ID = item.LOCATION_ID;
                Obj.CustomerID = item.CustomerID;
                Obj.planid = item.planid;
                Obj.MealType = item.MealType;
                Obj.ProdName1 = item.ProdName1;
                Obj.OprationDay = item.OprationDay;
                Obj.DayNumber = item.DayNumber;
                Obj.TransID = item.TransID;
                Obj.ContractID = item.ContractID;
                Obj.WeekofDay = item.WeekofDay;
                Obj.NameOfDay = item.NameOfDay;
                Obj.TotalWeek = item.TotalWeek;
                Obj.NoOfWeek = item.NoOfWeek;
                Obj.DisplayWeek = item.DisplayWeek;
                Obj.TotalDeliveryDay = item.TotalDeliveryDay;
                Obj.ActualDeliveryDay = item.ActualDeliveryDay;
                Obj.ExpectedDeliveryDay = item.ExpectedDeliveryDay;
                Obj.DeliveryTime = item.DeliveryTime;
                Obj.DeliveryMeal = item.DeliveryMeal;
                Obj.DriverID = item.DriverID;
                Obj.StartDate = item.StartDate;
                Obj.EndDate = item.EndDate;
                Obj.ExpectedDelDate = item.ExpectedDelDate;
                Obj.ActualDelDate = item.ActualDelDate;
                Obj.NExtDeliveryDate = item.NExtDeliveryDate;
                Obj.ReturnReason = item.ReturnReason;
                Obj.ReasonDate = item.ReasonDate;
                Obj.ProductionDate = item.ProductionDate;
                Obj.chiefID = item.chiefID;
                Obj.SubscriptonDayNumber = item.SubscriptonDayNumber;
                Obj.Calories = item.Calories;
                Obj.Protein = item.Protein;
                Obj.Carbs = item.Carbs;
                Obj.Fat = item.Fat;
                Obj.ItemWeight = item.ItemWeight;
                Obj.Qty = item.Qty;
                Obj.Item_cost = item.Item_cost;
                Obj.Item_price = item.Item_price;
                Obj.Total_price = item.Total_price;
                Obj.ShortRemark = item.ShortRemark;
                Obj.ACTIVE = item.ACTIVE;
                Obj.CRUP_ID = item.CRUP_ID;
                Obj.ChangesDate = item.ChangesDate;
                Obj.DeliverySequence = item.DeliverySequence;
                Obj.Switch1 = item.Switch1;
                Obj.Switch2 = item.Switch2;
                Obj.Switch3 = item.Switch3;
                Obj.Switch4 = item.Switch4;
                Obj.Switch5 = item.Switch5;
                Obj.Status = item.Status;
                Obj.UploadDate = item.UploadDate;
                Obj.Uploadby = item.Uploadby;
                Obj.SyncDate = item.SyncDate;
                Obj.Syncby = item.Syncby;
                Obj.SynID = item.SynID;

                ListAdd.Add(Obj);
            }

            List<Database.planmealcustinvoice> ListSend = ListAdd;
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authentication", bearerToken);
                HttpResponseMessage responseMessage = client.PostAsJsonAsync("api/GetplanmealsetupcustinvoiceList", ListSend).Result;
                //HttpResponseMessage Res = client.GetAsync("api/GetCountCategories").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    //var EmpResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    //var rootobject = JsonConvert.DeserializeObject<Planmealcustinvo>(EmpResponse);
                    //CompanyList = rootobject.data.ToList();
                }
            }

        }

        
    }

}