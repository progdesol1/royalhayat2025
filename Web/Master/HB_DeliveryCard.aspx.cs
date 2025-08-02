using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.Master
{
    public partial class HB_DeliveryCard : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID, LID, CID, planid, MealType = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            LID = Convert.ToInt32(((USER_MST)Session["USER"]).LOCATION_ID);

            if (!IsPostBack)
            {
                if (Session["TempList"] != null)
                {
                    List<Database.planmealcustinvoice> ListItem = ((List<Database.planmealcustinvoice>)Session["TempList"]).ToList();
                    //ListItem = ListItem.GroupBy(p => p.CustomerID).Select(p => p.FirstOrDefault()).ToList();
                    ListView1.DataSource = ListItem;
                    ListView1.DataBind();
                }
            }
        }

        public string getMealName(int MealType)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == MealType).Count() > 0)
            {
                return DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == MealType).REFNAME1;
            }
            else
            {
                return "Not Found";
            }
        }

        public string getmealprod(int MealType)
        {
            CID = Convert.ToInt32(ViewState["CID"]);
            planid = Convert.ToInt32(ViewState["planid"]);
            DateTime ExpDate = Convert.ToDateTime(ViewState["EXPDate"]);

            List<Database.planmealcustinvoice> Listplanmeal = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == planid && p.MealType == MealType && p.ExpectedDelDate == ExpDate).ToList();
            if (Listplanmeal.Count() > 0)
            {
                string prodname = "";

                foreach (Database.planmealcustinvoice items in Listplanmeal)
                {
                    //if (DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.MYPRODID == items.MYPRODID).Count() > 0)
                    //{
                    //    Database.TBLPRODUCT objprod = DB.TBLPRODUCTs.Single(p => p.TenentID == TID && p.MYPRODID == items.MYPRODID);
                    //    prodname += objprod.ProdName1 + " ,<br>";
                    //}
                    string prid1 = items.ProdName1;
                    prodname += prid1 + " ,<br>";
                }
                prodname = prodname.Trim();
                prodname = prodname.TrimEnd(',');
                return prodname;

            }
            else
            {
                return "Not Found";
            }
        }
        public string getWeight(int MealType)
        {
            CID = Convert.ToInt32(ViewState["CID"]);
            planid = Convert.ToInt32(ViewState["planid"]);
            DateTime ExpDate = Convert.ToDateTime(ViewState["EXPDate"]);

            List<Database.planmealcustinvoice> Listplanmeal = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == planid && p.MealType == MealType && p.ExpectedDelDate == ExpDate).ToList();

            if (Listplanmeal.Count() > 0)
            {
                string weight = "";
                foreach (Database.planmealcustinvoice item in Listplanmeal)
                {
                    if (DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == planid && p.MealType == MealType && p.DeliveryID == item.DeliveryID && p.ExpectedDelDate == ExpDate).Count() > 0)
                    {
                        Database.planmealcustinvoice obj = DB.planmealcustinvoices.Single(p => p.TenentID == TID && p.CustomerID == CID && p.planid == planid && p.MealType == MealType && p.DeliveryID == item.DeliveryID && p.ExpectedDelDate == ExpDate);
                        weight += obj.Qty + "<br>";
                        //weight += "("+ obj.Qty + "),("+ obj.Calories+ "),(" + obj.Protein +"),("+ obj.Carbs +"),("+ obj.Fat + "),(" + obj.ItemWeight + ")" + "<br>";
                    }
                }
                weight = weight.Trim();
                return weight;
            }
            else
            {
                return "Not Found";
            }
        }
        //Session List Data
        public string GetCNAME(int CID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0)
            {
                string CustomerNAME = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).COMPNAME1;
                return CID + " - " + CustomerNAME;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetPHONE(int CID)
        {
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == TID && p.COMPID == CID).Count() > 0)
            {
                string phoneNO = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).MOBPHONE;
                string BusNO = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == CID).BUSPHONE1;
                return phoneNO + "," + BusNO;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetAdd(int CID)
        {
            //if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID).Count() > 0)
            //{
            //    Database.TBLCONTACT_DEL_ADRES obj = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID).FirstOrDefault();
            //    string Address1 = obj.ADDR1;
            //    string Address2 = obj.ADDR2;
            //    return Address1 + "," + Address2;
            //}
            //else
            //{
            //    return "Not Found";
            //}
            List<Database.TBLCONTACT_DEL_ADRES> ListItem = DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID).ToList();
            string Address1 = "";
            string City = "";
            string State = "";
            if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == CID && p.Defualt == true).Count() > 0)
            {
                foreach (Database.TBLCONTACT_DEL_ADRES itemon in ListItem)
                {
                    if (DB.TBLCONTACT_DEL_ADRES.Where(p => p.TenentID == TID && p.ContactMyID == itemon.ContactMyID && p.DeliveryAdressID == itemon.DeliveryAdressID && p.Defualt == true).Count() > 0)
                    {
                        Database.TBLCONTACT_DEL_ADRES obj = DB.TBLCONTACT_DEL_ADRES.Single(p => p.TenentID == TID && p.ContactMyID == itemon.ContactMyID && p.DeliveryAdressID == itemon.DeliveryAdressID && p.Defualt == true);
                        int countryID = Convert.ToInt32(obj.COUNTRYID);
                        int stateID = Convert.ToInt32(obj.STATE);
                        int cityID = Convert.ToInt32(obj.CITY);
                        string Building = obj.Building != null ? obj.Building.ToString() : "";
                        string Street = obj.Street != null ? obj.Street.ToString() : "";
                        string Lane = obj.Lane != null ? obj.Lane.ToString() : "";
                        State = DB.tblStates.Where(p => p.COUNTRYID == countryID && p.StateID == stateID).Count() > 0 ? DB.tblStates.Single(p => p.COUNTRYID == countryID && p.StateID == stateID).MYNAME1 : "'Not Found'";
                        City = DB.tblCityStatesCounties.Where(p => p.COUNTRYID == countryID && p.StateID == stateID && p.CityID == cityID).Count() > 0 ? DB.tblCityStatesCounties.Single(p => p.COUNTRYID == countryID && p.StateID == stateID && p.CityID == cityID).CityEnglish : "'Not Found'";
                        Address1 += obj.ADDR1 + "," + " Building=" + Building + " Street=" + Street + " Lane=" + Lane + " City=" + City + "," + " State=" + State + "</br>";
                    }
                }
                return Address1;
            }
            else
            {
                return "'Not Found'";
            }
        }
        public string GetDriver(int ID)
        {
            if (DB.tbl_Employee.Where(p => p.TenentID == TID && p.employeeID == ID).Count() > 0)
            {
                string DriverName = DB.tbl_Employee.Single(p => p.TenentID == TID && p.employeeID == ID).firstname;
                return DriverName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetDelivery(int ID, int DayNumber)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == ID).Count() > 0)
            {
                string Delivery = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == ID).REFNAME1;
                return Delivery + "(Delivery No - " + DayNumber.ToString() + ")";
            }
            else
            {
                return "Not Found";
            }
        }
        //Delivered Item(How many)
        public string GetDeliverd(int CID, int pid)
        {
            //int DateCount = 0;
            List<Database.planmealcustinvoice> ListDate = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == pid).ToList();
            //foreach (Database.planmealcustinvoice item in ListDate)
            //{
            //    Database.planmealcustinvoice DateObj = ListDate.Single(p => p.TenentID == TID && p.CustomerID == CID && p.planid == pid && p.DeliveryID == item.DeliveryID);
            //    string DT = DateObj.ActualDelDate.ToString();
            //    if (DT != "")
            //        DateCount++;
            //}
            //return DateCount.ToString();

            List<Database.planmealcustinvoice> ListdelDay = ListDate.Where(p => p.ActualDelDate != null).GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
            int DeliveredDays = ListdelDay.Count();
            return DeliveredDays.ToString();
        }
        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //for (int i = 0; i < ListView1.Items.Count; i++)
            //{
            //    Label lblCustomerNo = (Label)ListView1.Items[i].FindControl("lblCustomerNo");
            //}
            ListView ListFoodDetail = (ListView)e.Item.FindControl("ListFoodDetail");
            Label lblCustomerNo = (Label)e.Item.FindControl("lblCustomerNo");
            Label lblplan = (Label)e.Item.FindControl("lblplan");
            Label lblmeal = (Label)e.Item.FindControl("lblmeal");
            Label lblExpDate = (Label)e.Item.FindControl("lblExpDate");//Expected Date
            Label lblLIkes = (Label)e.Item.FindControl("lblLIkes");
            Label lblDisLIkes = (Label)e.Item.FindControl("lblDisLIkes");
            Label lblAllergies = (Label)e.Item.FindControl("lblAllergies");
            System.Web.UI.WebControls.Image HealtybarLogo = (System.Web.UI.WebControls.Image)e.Item.FindControl("HealtybarLogo");
            string Loggo = Classes.EcommAdminClass.Logo(TID);
            HealtybarLogo.ImageUrl = "../assets/" + Loggo;

            int CID = Convert.ToInt32(lblCustomerNo.Text);
            int planid = Convert.ToInt32(lblplan.Text);
            DateTime EXPDate = Convert.ToDateTime(lblExpDate.Text);
            ViewState["CID"] = CID;
            ViewState["planid"] = planid;
            ViewState["EXPDate"] = EXPDate;

            List<Database.planmealcustinvoice> TempDTList = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == planid && p.ExpectedDelDate == EXPDate).GroupBy(p => p.MealType).Select(p => p.FirstOrDefault()).ToList();
            foreach (Database.planmealcustinvoice SItem in TempDTList)
            {
                if (TempDTList.Count() > 0)
                {
                    if (TempDTList.Where(p => p.TenentID == TID && p.MYTRANSID == SItem.MYTRANSID && p.DeliveryID == SItem.DeliveryID && p.planid == planid).Count() > 0)
                    {
                        Database.planmealcustinvoice Mobj = TempDTList.Single(p => p.TenentID == TID && p.MYTRANSID == SItem.MYTRANSID && p.DeliveryID == SItem.DeliveryID && p.planid == planid);
                        int DeliveryMeall = Convert.ToInt32(Mobj.DeliveryMeal);
                        if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFID == DeliveryMeall && p.REFTYPE == "Food" && p.REFSUBTYPE == "MealType").Count() > 0)
                        {
                            string S3 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == DeliveryMeall && p.REFTYPE == "Food" && p.REFSUBTYPE == "MealType").SWITCH1.ToString();
                            Mobj.Switch3 = S3;
                            DB.SaveChanges();
                        }
                    }
                }
            }

            ListFoodDetail.DataSource = TempDTList.OrderBy(p => p.Switch3);// DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == planid && p.ExpectedDelDate == EXPDate).GroupBy(p => p.MealType).Select(p => p.FirstOrDefault());
            ListFoodDetail.DataBind();
            //Like ,DisLike erc.
            List<Database.tblcontact_addon1_dtl> Listaddondtl = DB.tblcontact_addon1_dtl.Where(p => p.TenentID == TID && p.customerID == CID).ToList();
            List<Database.TBLPRODUCT> ListPROduct = DB.TBLPRODUCTs.Where(p => p.TenentID == TID && p.LOCATION_ID == LID).ToList();

            string Likes = "";
            List<Database.tblcontact_addon1_dtl> ListLikes = Listaddondtl.Where(p => p.LikeType == "L").ToList();
            foreach (Database.tblcontact_addon1_dtl itemslike in ListLikes)
            {
                if (ListPROduct.Where(p => p.TenentID == TID && p.MYPRODID == itemslike.ItemCode && p.LOCATION_ID == LID).Count() > 0)
                {
                    Database.TBLPRODUCT objProd = ListPROduct.Single(p => p.TenentID == TID && p.MYPRODID == itemslike.ItemCode && p.LOCATION_ID == LID);
                    Likes += objProd.ProdName1 + " , ";
                }
            }
            Likes = Likes.Trim();
            Likes = Likes.TrimEnd(',');
            lblLIkes.Text = Likes;

            string Dislike = "";
            List<Database.tblcontact_addon1_dtl> ListDislike = Listaddondtl.Where(p => p.LikeType == "D").ToList();
            foreach (Database.tblcontact_addon1_dtl itemsDislike in ListDislike)
            {
                if (ListPROduct.Where(p => p.TenentID == TID && p.MYPRODID == itemsDislike.ItemCode && p.LOCATION_ID == LID).Count() > 0)
                {
                    Database.TBLPRODUCT objProd = ListPROduct.Single(p => p.TenentID == TID && p.MYPRODID == itemsDislike.ItemCode && p.LOCATION_ID == LID);
                    Dislike += objProd.ProdName1 + " , ";
                }
            }
            Dislike = Dislike.Trim();
            Dislike = Dislike.TrimEnd(',');
            lblDisLIkes.Text = Dislike;

            string Allergies = "";
            List<Database.tblcontact_addon1_dtl> ListAllergies = Listaddondtl.Where(p => p.LikeType == "A").ToList();
            foreach (Database.tblcontact_addon1_dtl itemsAllergies in ListAllergies)
            {
                if (ListPROduct.Where(p => p.TenentID == TID && p.MYPRODID == itemsAllergies.ItemCode && p.LOCATION_ID == LID).Count() > 0)
                {
                    Database.TBLPRODUCT objProd = ListPROduct.Single(p => p.TenentID == TID && p.MYPRODID == itemsAllergies.ItemCode && p.LOCATION_ID == LID);
                    Allergies += objProd.ProdName1 + ", ";
                }
            }
            Allergies = Allergies.Trim();
            Allergies = Allergies.TrimEnd(',');
            lblAllergies.Text = Allergies;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ReportMst/DeliveryCard.aspx");
        }
        public string GetPlan(int ID)
        {
            if (DB.tblProduct_Plan.Where(p => p.TenentID == TID && p.planid == ID).Count() > 0)
            {
                string PlanName = DB.tblProduct_Plan.Single(p => p.TenentID == TID && p.planid == ID).planname1;
                return PlanName;
            }
            else
            {
                return "Not Found";
            }
        }
        public string GetTotal(int CID, int PID)
        {
            List<Database.planmealcustinvoice> ListDTinvoice = DB.planmealcustinvoices.Where(p => p.TenentID == TID && p.CustomerID == CID && p.planid == PID).ToList();
            List<Database.planmealcustinvoice> ListtotaldelDay = ListDTinvoice.GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
            int totalDelDay = ListtotaldelDay.Count();
            return totalDelDay.ToString();
        }
        //Total
        //List<Database.planmealcustinvoice> ListtotaldelDay = ListDTinvoice.GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
        //int totalDelDay = ListtotaldelDay.Count();
        //Deliverd
        //List<Database.planmealcustinvoice> ListdelDay = ListDTinvoice.Where(p => p.ActualDelDate == null).GroupBy(p => p.DayNumber).Select(p => p.FirstOrDefault()).ToList();
        //int DeliveredDays = ListdelDay.Count();

    }
}