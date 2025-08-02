using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;

namespace Web.ReportMst
{
    public partial class DriverGPS : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        int TID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER"] != null)
            {
                TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            }
            else
            {

            }

            if (!IsPostBack)
            {
                FillContract();

            }
            GoogleMapForASPNet1.GoogleMapObject.Width = "99%"; //map
            GoogleMapForASPNet1.GoogleMapObject.Height = "400px";//map
            GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 14;//map
        }

        public void FillContract()
        {
            DRPDriver.DataSource = DB.tbl_Employee.Where(p => p.TenentID == TID && p.EmployeeType == "Driver").OrderBy(p => p.firstname);
            DRPDriver.DataTextField = "firstname";
            DRPDriver.DataValueField = "employeeID";
            DRPDriver.DataBind();
            DRPDriver.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void Searcht_Click(object sender, EventArgs e)
        {
            int DID = Convert.ToInt32(DRPDriver.SelectedValue);
            DRPDeviceID.DataSource = DB.tbl_Employee.Where(p => p.TenentID == TID && p.employeeID == DID && p.EmployeeType == "Driver").OrderBy(p => p.firstname);
            DRPDeviceID.DataTextField = "DeviceID";
            DRPDeviceID.DataValueField = "DeviceID";
            DRPDeviceID.DataBind();
            //DRPDeviceID.Items.Insert(0, new ListItem("-- Select --", "0"));
            ListBind();
        }
        public void ListBind()
        {
            string DeviceIDD = (DRPDeviceID.SelectedValue).ToString();
            DateTime DT = DateTime.Today.Date;
            int ToDay = Convert.ToInt32(DateTime.Now.Day);
            int ToMonth = Convert.ToInt32(DateTime.Now.Month);
            int ToYear = Convert.ToInt32(DateTime.Now.Year);

            List<Database.tblgprsdata> List = DB.tblgprsdatas.Where(p => p.DeviceID == DeviceIDD && p.DateTime.Value.Day == ToDay && p.DateTime.Value.Month == ToMonth && p.DateTime.Value.Year == ToYear).ToList();
            Listview1.DataSource = List;
            Listview1.DataBind();

            if (List.Count() > 0)
            {
                string LatitudeF1 = List.FirstOrDefault().latitude;
                string LatitudeF2 = List.LastOrDefault().latitude;
                string LongitudeF1 = List.FirstOrDefault().logitude;
                string LongitudeF2 = List.LastOrDefault().logitude;

                string LatiLongi1 = LatitudeF1 + "," + LongitudeF1;
                string LatiLongi2 = LatitudeF2 + "," + LongitudeF2;

                ViewState["LatiLongi1"] = LatiLongi1;
                ViewState["LatiLongi2"] = LatiLongi2;
                txtFirstAddress.Text = LatiLongi1;
                txtsecondAddress.Text = LatiLongi2;
            }

        }
        public string GetMapAdd(int CID)//map
        {
            string add = "";
            return add;
        }

        protected void btnCurrentlocation_Click(object sender, EventArgs e)//map
        {
            string sScript = "<script>\n";

            sScript += "if (navigator.geolocation) {\n";
            sScript += "navigator.geolocation.getCurrentPosition(function (p) {\n";
            sScript += " var n = p.coords.latitude.toString();\n";
            sScript += "var n1 = p.coords.longitude.toString();\n";
            sScript += "document.getElementById(\"txtStartingpoint\").value = p.coords.formatted_address;\n";
            sScript += "});\n";
            sScript += "} else {\n";
            sScript += "    alert('Geo Location feature is not supported in this browser.');}\n";
            sScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "onLoadCall", sScript);

            GoogleMapForASPNet1.GoogleMapObject.Directions.ShowDirectionInstructions = chkShowDirections.Checked;
            GoogleMapForASPNet1.GoogleMapObject.Directions.HideMarkers = chkHideMarkers.Checked;
            GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineColor = txtDirColor.Text;
            GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineWeight = int.Parse(txtDirWidth.Text);
            GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineOpacity = double.Parse(txtDirOpacity.Text);
            //Provide addresses or postal codes in following lines
            GoogleMapForASPNet1.GoogleMapObject.Directions.Addresses.Clear();


        }

        protected void btnDrawDirections_Click(object sender, EventArgs e)
        {
            Direction();
            ViewState["FlageMap"] = "MapFlage";
        }
        public void Direction()
        {


            GoogleMapForASPNet1.GoogleMapObject.Directions.ShowDirectionInstructions = chkShowDirections.Checked;
            GoogleMapForASPNet1.GoogleMapObject.Directions.HideMarkers = chkHideMarkers.Checked;
            GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineColor = txtDirColor.Text;
            GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineWeight = int.Parse(txtDirWidth.Text);
            GoogleMapForASPNet1.GoogleMapObject.Directions.PolylineOpacity = double.Parse(txtDirOpacity.Text);
            //Provide addresses or postal codes in following lines
            GoogleMapForASPNet1.GoogleMapObject.Directions.Addresses.Clear();
            if (txtFirstAddress.Text != "" && txtsecondAddress.Text != "")
            {
                GoogleMapForASPNet1.GoogleMapObject.Directions.Addresses.Add(txtFirstAddress.Text);
                GoogleMapForASPNet1.GoogleMapObject.Directions.Addresses.Add(txtsecondAddress.Text);
            }

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (ViewState["FlageMap"] != null)
            {
                string Name = ViewState["FlageMap"].ToString();
                if (Name == "MapFlage")
                {
                    Direction();
                    UpdatePanel1.Update();
                }
            }

        }

    }
}