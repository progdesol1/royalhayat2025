using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.UI.DataVisualization.Charting;

namespace Web.ReportMst
{
    public partial class HelpDeskExcRep : System.Web.UI.Page
    {
        int TID = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);

            if (!IsPostBack)
            {
                complainmonthfrom();
                con.Open();
                SqlCommand command;
                SqlDataAdapter ADB = new SqlDataAdapter();
                TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
                string sql5 = "Delete from Tempdatatable where tenentID=" + TID;
                command = new SqlCommand(sql5, con);
                command.ExecuteNonQuery();
                command.Dispose();
                //string sql6 = "Delete From RHComplaintLists where tenentID=" + TID;
                //command = new SqlCommand(sql6, con);
                //command.ExecuteNonQuery();
                //command.Dispose();
            }
        }

        //public void BindList()
        //{
        //    List<Database.CRMMainActivity> TempList = new List<Database.CRMMainActivity>();
        //    List<Database.CRMMainActivity> Listmain = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "CALLPURPOSE").ToList();

        //    List<Database.CRMMainActivity> DeptList = Listmain.GroupBy(p => p.TickDepartmentID).Select(g => g.FirstOrDefault()).ToList();
        //    List<Database.CRMMainActivity> DeptTemp = new List<Database.CRMMainActivity>();
        //    foreach (Database.CRMMainActivity Deptitem in DeptList)
        //    {

        //        DeptTemp.Add(Deptitem);
        //        DeptTemp.Single(p => p.TenentID == Deptitem.TenentID && p.MasterCODE == Deptitem.MasterCODE && p.MyID == Deptitem.MyID).Description = "Department";
        //        Database.CRMMainActivity obj = DeptTemp.Single(p => p.TenentID == Deptitem.TenentID && p.MasterCODE == Deptitem.MasterCODE && p.MyID == Deptitem.MyID);
        //        TempList.Add(obj);
        //        //TempList.Add(Deptitem);
        //    }

        //    List<Database.CRMMainActivity> LOCList = Listmain.GroupBy(p => p.TickPhysicalLocation).Select(g => g.FirstOrDefault()).ToList();
        //    List<Database.CRMMainActivity> LOCTemp = new List<Database.CRMMainActivity>();
        //    foreach (Database.CRMMainActivity LOCitem in LOCList)
        //    {

        //        LOCTemp.Add(LOCitem);
        //        LOCTemp.Single(p => p.TenentID == LOCitem.TenentID && p.MasterCODE == LOCitem.MasterCODE && p.MyID == LOCitem.MyID).Description = "PhyLocation";
        //        Database.CRMMainActivity obj = LOCTemp.Single(p => p.TenentID == LOCitem.Tr34dcv xcdqwefcx enentID && p.MasterCODE == LOCitem.MasterCODE && p.MyID == LOCitem.MyID);
        //        TempList.Add(obj);
        //        //TempList.Add(LOCitem);
        //    }

        //    List<Database.CRMMainActivity> catList = Listmain.GroupBy(p => p.TickCatID).Select(g => g.FirstOrDefault()).ToList();
        //    List<Database.CRMMainActivity> CatTemp = new List<Database.CRMMainActivity>();
        //    foreach (Database.CRMMainActivity catitem in catList)
        //    {
        //        CatTemp.Add(catitem);
        //        CatTemp.Single(p => p.TenentID == catitem.TenentID && p.MasterCODE == catitem.MasterCODE && p.MyID == catitem.MyID).Description = "Category";
        //        Database.CRMMainActivity obj = CatTemp.Single(p => p.TenentID == catitem.TenentID && p.MasterCODE == catitem.MasterCODE && p.MyID == catitem.MyID);
        //        TempList.Add(obj);
        //        //TempList.Add(catitem);a
        //    }

        //    List<Database.CRMMainActivity> subcatList = Listmain.GroupBy(p => p.TickSubCatID).Select(g => g.FirstOrDefault()).ToList();
        //    List<Database.CRMMainActivity> SubCatTemp = new List<Database.CRMMainActivity>();
        //    foreach (Database.CRMMainActivity subcatitem in subcatList)
        //    {
        //        SubCatTemp.Add(subcatitem);
        //        SubCatTemp.Single(p => p.TenentID == subcatitem.TenentID && p.MasterCODE == subcatitem.MasterCODE && p.MyID == subcatitem.MyID).Description = "SubCategory";
        //        Database.CRMMainActivity obj = SubCatTemp.Single(p => p.TenentID == subcatitem.TenentID && p.MasterCODE == subcatitem.MasterCODE && p.MyID == subcatitem.MyID);
        //        TempList.Add(obj);
        //        //TempList.Add(subcatitem);
        //    }
        //    int PhyLocationCOU = 0;
        //    int CategoryCOU = 0;
        //    int SubCategoryCOU = 0;

        //    List<CrmACT> ACTList = new List<CrmACT>();
        //    foreach (Database.CRMMainActivity tempitem in TempList)
        //    {
        //        string typ = tempitem.Description;
        //        if (typ == "Department")
        //        {
        //            CrmACT obj2 = new CrmACT();
        //            obj2.COMMANID = tempitem.TickDepartmentID.ToString();
        //            obj2.typee = tempitem.Description;
        //            ACTList.Add(obj2);
        //        }
        //        else if (typ == "PhyLocation")
        //        {
        //            if (PhyLocationCOU == 0)
        //            {
        //                CrmACT obj1 = new CrmACT();
        //                obj1.Tenent = tempitem.TenentID;
        //                obj1.MasterCode = 0;
        //                obj1.myid = 0;
        //                obj1.datee = tempitem.UploadDate.Value.Date;
        //                obj1.COMMANID = tempitem.TickPhysicalLocation.ToString();
        //                obj1.typee = tempitem.Description;
        //                ACTList.Add(obj1);
        //            }
        //            PhyLocationCOU++;
        //        }
        //        else if (typ == "Category")
        //        {
        //            if (CategoryCOU == 0)
        //            {
        //                CrmACT obj2 = new CrmACT();
        //                obj2.Tenent = tempitem.TenentID;
        //                obj2.MasterCode = 0;
        //                obj2.myid = 0;
        //              //  obj2.datee = tempitem.UploadDate.Value.Date;
        //                obj2.COMMANID = tempitem.TickCatID.ToString();
        //                obj2.typee = tempitem.Description;
        //                ACTList.Add(obj2);
        //            }
        //            CategoryCOU++;
        //        }
        //        else if (typ == "SubCategory")
        //        {
        //            if (SubCategoryCOU == 0)
        //            {
        //                CrmACT obj3 = new CrmACT();
        //                obj3.Tenent = tempitem.TenentID;
        //                obj3.MasterCode = 0;
        //                obj3.myid = 0;
        //                obj3.datee = tempitem.UploadDate.Value.Date;
        //                obj3.COMMANID = tempitem.TickSubCatID.ToString();
        //                obj3.typee = tempitem.Description;
        //                ACTList.Add(obj3);
        //            }
        //            SubCategoryCOU++;
        //        }
        //        CrmACT obj = new CrmACT();
        //        obj.Tenent = tempitem.TenentID;
        //        obj.MasterCode = tempitem.MasterCODE;
        //        obj.myid = tempitem.MyID;
        //        //obj.datee = tempitem.UploadDate.Value.Date;
        //        if (typ == "Department")
        //            obj.COMMANID = tempitem.TickDepartmentID.ToString();
        //        if (typ == "PhyLocation")
        //            obj.COMMANID = tempitem.TickPhysicalLocation.ToString();
        //        if (typ == "Category")
        //            obj.COMMANID = tempitem.TickCatID.ToString();
        //        if (typ == "SubCategory")
        //            obj.COMMANID = tempitem.TickSubCatID.ToString();
        //        obj.typee = tempitem.Description;
        //        ACTList.Add(obj);
        //    }
        //    ListView3.DataSource = ACTList;
        //    ListView3.DataBind();


        //   // old work
        //    //List<Database.CRMMainActivity> DeptList = Listmain.GroupBy(p => p.TickDepartmentID).Select(g => g.FirstOrDefault()).ToList();
        //    //ListView3.DataSource = DeptList;
        //    //ListView3.DataBind();

        //    //List<Database.CRMMainActivity> LOCList = Listmain.GroupBy(p => p.TickPhysicalLocation).Select(g => g.FirstOrDefault()).ToList();            
        //    //ListView1.DataSource = LOCList;
        //    //ListView1.DataBind();

        //    //List<Database.CRMMainActivity> catList = Listmain.GroupBy(p => p.TickCatID).Select(g => g.FirstOrDefault()).ToList();
        //    //ListView2.DataSource = catList;
        //    //ListView2.DataBind();

        //    //List<Database.CRMMainActivity> subcatList = Listmain.GroupBy(p => p.TickSubCatID).Select(g => g.FirstOrDefault()).ToList();
        //    //ListView4.DataSource = subcatList;
        //    //ListView4.DataBind();


        //}
        public string GetDept(string ID, string type)
        {
            string Rettype = "";
            if (type == "Department")
            {
                int DID = Convert.ToInt32(ID);
                if (DB.DeptITSupers.Where(p => p.TenentID == TID && p.DeptID == DID).Count() > 0)
                {
                    Rettype = DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == DID).DeptName;
                }
                else
                {
                    Rettype = "Not Found";
                }
            }
            else if (type == "PhyLocation")
            {

                if (ID == "PhyLocation")
                {
                    Rettype = "Location";
                }
                else
                {
                    int LID = Convert.ToInt32(ID);
                    if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.REFID == LID).Count() > 0)
                    {
                        Rettype = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.REFID == LID).REFNAME1;
                    }
                    else
                    {
                        Rettype = "Not Found";
                    }
                }

            }
            else if (type == "Category")
            {
                if (ID == "Category")
                {
                    Rettype = "Category";
                }
                else
                {
                    int CID = Convert.ToInt32(ID);
                    if (DB.ICCATEGORies.Where(p => p.TenentID == TID && p.CATTYPE == "HelpDesk" && p.CATID == CID).Count() > 0)
                    {
                        Rettype = DB.ICCATEGORies.Single(p => p.TenentID == TID && p.CATTYPE == "HelpDesk" && p.CATID == CID).CATNAME;
                    }
                    else
                    {
                        Rettype = "Not Found";
                    }
                }
            }
            else if (type == "SubCategory")
            {
                if (ID == "SubCategory")
                {
                    Rettype = "SubCategory";
                }
                else
                {
                    int sID = Convert.ToInt32(ID);
                    if (DB.ICSUBCATEGORies.Where(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.SUBCATID == sID).Count() > 0)
                    {
                        Rettype = DB.ICSUBCATEGORies.Single(p => p.TenentID == TID && p.SUBCATTYPE == "HelpDesk" && p.SUBCATID == sID).SUBCATNAME;
                    }
                    else
                    {
                        Rettype = "Not Found";
                    }
                }
            }
            return Rettype;
        }
        public string GetLOC(int ID)
        {
            if (DB.REFTABLEs.Where(p => p.TenentID == TID && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation" && p.REFID == ID).Count() > 0)
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
            if (DB.ICCATEGORies.Where(p => p.TenentID == TID && p.CATTYPE == "HelpDesk" && p.CATID == ID).Count() > 0)
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

        //protected void ListView3_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{

        //    Label lblDeptID = (Label)e.Item.FindControl("lblDeptID");
        //    Label DeptCount = (Label)e.Item.FindControl("Label2");
        //    //if(lblDeptID.Text=="")
        //    //    lblDeptID.Text="99999";
        //    int Deptid = Convert.ToInt32(lblDeptID.Text);
        //    int count = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "CALLPURPOSE" && p.TickDepartmentID == Deptid).Count();
        //    DeptCount.Text = count.ToString();
        //}

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            Label lbllocid = (Label)e.Item.FindControl("lbllocid");
            Label LOCCount = (Label)e.Item.FindControl("Label4");
            int LOCid = Convert.ToInt32(lbllocid.Text);
            int count = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "CALLPURPOSE" && p.TickPhysicalLocation == LOCid).Count();
            LOCCount.Text = count.ToString();
        }

        protected void ListView2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblCatid = (Label)e.Item.FindControl("lblCatid");
            Label CatCount = (Label)e.Item.FindControl("Label6");
            int Catid = Convert.ToInt32(lblCatid.Text);
            int count = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "CALLPURPOSE" && p.TickCatID == Catid).Count();
            CatCount.Text = count.ToString();

        }

        protected void ListView4_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Label lblSubCatid = (Label)e.Item.FindControl("lblSubCatid");
            Label SubCatCount = (Label)e.Item.FindControl("Label8");
            int subCatid = Convert.ToInt32(lblSubCatid.Text);
            int count = DB.CRMMainActivities.Where(p => p.TenentID == TID && p.ACTIVITYE == "CALLPURPOSE" && p.TickSubCatID == subCatid).Count();
            SubCatCount.Text = count.ToString();
        }


        public void complainmonthfrom()
        {
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            string sql = "select distinct FORMAT(UploadDate, 'MMMM,yyyy') as monthyear, Month(UploadDate),Year(UploadDate) from CRMMainActivities where TenentID=" + TID + " and UploadDate is not NULL order by 2,1";
            DataTable dt = DataCon.GetDataTable(sql);
            drpmonthfrom.DataSource = dt;
            drpmonthfrom.DataTextField = "monthyear";
            drpmonthfrom.DataValueField = "monthyear";
            drpmonthfrom.DataBind();
            drpmonthfrom.Items.Insert(0, new ListItem("--All Month--", "0"));
        }


        //and Month(UploadDate) BETWEEN '" + drpmonthfrom.SelectedValue + "' AND ' " + drpmonthto.SelectedValue + " ' and Year(UploadDate) BETWEEN '" + drplistyear.SelectedValue + "' AND ' " + drplistyearto.SelectedValue + " '

        protected void Button1_Click(object sender, EventArgs e)
        {

            txtstartdate.Text = "";
            txtenddate.Text = "";
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            con.Open();
            SqlCommand command;
            SqlDataAdapter Ang = new SqlDataAdapter();
            int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            string sql5 = "Delete from Tempdatatable where tenentID=" + TID;
            command = new SqlCommand(sql5, con);
            command.ExecuteNonQuery();
            command.Dispose();

            DateTime monthyear = Convert.ToDateTime(drpmonthfrom.SelectedValue);
            string Month = monthyear.ToString("MMMM");
            string Year = monthyear.ToString("yyyy");

            string SQOCommad = " select TickDepartmentID , DeptITSuper.DeptName  , COUNT(*) as DeptCount  FROM  CRMMainActivities , DeptITSuper " +

                               "  where CRMMainActivities.TenentID = " + TID + "  and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "'  and DeptITSuper.TenentID = CRMMainActivities.TenentID   and DeptITSuper.DeptID = CRMMainActivities.TickDepartmentID and CRMMainActivities.TickDepartmentID is not null and CRMMainActivities.UploadDate is not null" +

                               " group by TickDepartmentID, DeptITSuper.DeptName";

            List<Tempdatatable> Listtemp = DB.Tempdatatables.Where(p => p.TenentID == TID).ToList();

            List<Tempdatatable> list1 = new List<Tempdatatable>();
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();




            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];

            decimal finalcount = 0;

            for (int a = 0; a <= dt.Rows.Count - 1; a++)
            {
                finalcount = finalcount + Convert.ToInt32(dt.Rows[a]["DeptCount"]);
            }


            if (dt.Rows.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        int TickDepartmentID = Convert.ToInt32(dt.Rows[i]["TickDepartmentID"]);
                        if (DB.RHComplaintLists.Where(p => p.TenentID == TID && p.ID == TickDepartmentID && p.Type == 1 && p.Month == Month && p.year == Year).Count() <= 0)
                        {
                            decimal DeptCount = Convert.ToInt32(dt.Rows[i]["DeptCount"]);
                            string deptName = DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == TickDepartmentID).DeptName;
                            RHComplaintList objrh = new RHComplaintList();
                            objrh.TenentID = TID;
                            objrh.Type = 1;
                            objrh.ID = TickDepartmentID;
                            objrh.Name = deptName;
                            objrh.Count =Convert.ToInt32(DeptCount);
                            decimal percent1 =(DeptCount / finalcount) ;
                            string s1 = percent1.ToString("N3");
                            decimal ds1 = Convert.ToDecimal(s1);
                            decimal fp = ds1 * 100;
                            objrh.TotalCount = Convert.ToInt32(finalcount);
                            objrh.Percentage = fp;
                            objrh.Month = Month;
                            objrh.year = Year;
                            DB.RHComplaintLists.AddObject(objrh);
                            DB.SaveChanges();
                        }
                    }
                }
            }


            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
            }

            decimal finalTotal = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                finalTotal = finalTotal + Convert.ToInt32(dt.Rows[i]["DeptCount"]);
            }
            lblFinalTotal.Text = finalTotal.ToString();

            //decimal finalTot = 0;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    finalTot = finalTot + Convert.ToDecimal(dt.Rows[i]["Tempdatatable.percentage"]);
            //}
            //lblperdept.Text = finalTot.ToString();



            //string com = " select TickDepartmentID , RHComplaintList.Name  , COUNT(*) as DeptCount , RHComplaintList.percentage  " +
            //             " FROM  CRMMainActivities , RHComplaintList   where CRMMainActivities.TenentID =  " + TID +
            //             " and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "' " +
            //             " and RHComplaintList.Type=1 and RHComplaintList.TenentID = CRMMainActivities.TenentID   and RHComplaintList.ID = CRMMainActivities.TickDepartmentID " +
            //             "  and CRMMainActivities.TickDepartmentID is not null and CRMMainActivities.UploadDate is not null group by TickDepartmentID, RHComplaintList.Name  , RHComplaintList.percentage ";


            string com = "select ID as TickDepartmentID, Name as Name  ,Count as DeptCount , percentage as percentage" +
                         " FROM    RHComplaintList   where TenentID =  " + TID + "  and Month='" + Month + "' and year='" + Year + "' " +
                         " and Type=1 and  RHComplaintList.ID is not null and Month is not null";

            SqlCommand CMDs = new SqlCommand(com, con);
            SqlDataAdapter ADBs = new SqlDataAdapter(CMDs);
            DataSet dss = new DataSet();
            ADBs.Fill(dss);
            DataTable dts = dss.Tables[0];
            ListView3.DataSource = dts;
            ListView3.DataBind();


            Chart1.Visible = true;
            string query = string.Format(" Select  Percentage,left(Name,15)  from RHComplaintList where TenentID= " + TID + " and type=1 and Month='" + Month + "' and year='" + Year + "' group by Percentage,Name");
            DataTable dtt = GetData(query);
            string[] x = new string[dtt.Rows.Count];
            int[] y = new int[dtt.Rows.Count];
            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                x[i] = dtt.Rows[i][1].ToString();
                y[i] = Convert.ToInt32(dtt.Rows[i][0]);

            }
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Series[0].ChartType = SeriesChartType.Pie;
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart1.Legends[0].Enabled = true;
            // Set pie labels to be outside the pie chart
            this.Chart1.Series[0]["PieLabelStyle"] = "Disabled";

            // Add a legend to the chart and dock it to the bottom-center
            this.Chart1.Legends.Add("Legend1");
            this.Chart1.Legends[0].Enabled = true;
            this.Chart1.Legends[0].Docking = Docking.Bottom;
            this.Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

            // Show labels in the legend in the format "Name (### %)"
            this.Chart1.Series[0].LegendText = "#VALX (#PERCENT)";

            // By sorting the data points, they show up in proper ascending order in the legend
            this.Chart1.DataManipulator.Sort(PointSortOrder.Descending, Chart1.Series[0]);



            string SQOCommad4 = " select TickCatID , ICCATEGORY.CATNAME  , COUNT(*) as CatCount  FROM  CRMMainActivities , ICCATEGORY " +
                                " where CRMMainActivities.TenentID =  " + TID + " and CATTYPE='HelpDesk' " +
                                " and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "'  and ICCATEGORY.TenentID = CRMMainActivities.TenentID " +
                                " and ICCATEGORY.CATID = CRMMainActivities.TickCatID and CRMMainActivities.TickCatID is not null  and CRMMainActivities.UploadDate is not null group by TickCatID, ICCATEGORY.CATNAME ";

            SqlCommand CMD5 = new SqlCommand(SQOCommad4, con);
            SqlDataAdapter ADB5 = new SqlDataAdapter(CMD5);
            DataSet ds5 = new DataSet();
            ADB5.Fill(ds5);
            DataTable dt5 = ds5.Tables[0];

            decimal fc = 0;

            for (int a = 0; a <= dt5.Rows.Count - 1; a++)
            {
                fc = fc + Convert.ToInt32(dt5.Rows[a]["CatCount"]);
            }


            if (dt5.Rows.Count > 0)
            {
                if (ds5.Tables[0] != null && ds5.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dt5.Rows.Count - 1; i++)
                    {
                        int TickCatID = Convert.ToInt32(dt5.Rows[i]["TickCatID"]);
                        if (DB.RHComplaintLists.Where(p => p.TenentID == TID && p.ID == TickCatID && p.Type == 2 && p.Month == Month && p.year == Year).Count() <= 0)
                        {
                            decimal CatCount = Convert.ToInt32(dt5.Rows[i]["CatCount"]);
                            string CATNAME = DB.ICCATEGORies.Single(p => p.TenentID == TID && p.CATID == TickCatID).CATNAME;


                            //Tempdatatable objprofile = new Tempdatatable();
                            //objprofile.TenentID = TID;
                            //objprofile.Type = 2;
                            //objprofile.ID = TickCatID;
                            //objprofile.Name = CATNAME;
                            //objprofile.UserId = UIN;
                            //objprofile.Count = CatCount;
                            //objprofile.TotalCount = Convert.ToInt32(fc);
                            //decimal percent = (CatCount / fc) * 100;
                            //objprofile.percentage = percent;
                            //DB.Tempdatatables.AddObject(objprofile);
                            //DB.SaveChanges();
                            RHComplaintList objrh = new RHComplaintList();
                            objrh.TenentID = TID;
                            objrh.Type = 2;
                            objrh.ID = TickCatID;
                            objrh.Name = CATNAME;
                            objrh.Count =Convert.ToInt32(CatCount);
                            decimal percent1 = (CatCount / finalcount);
                            string s1 = percent1.ToString("N3");
                            decimal ds1 = Convert.ToDecimal(s1);
                            decimal fp = ds1 * 100;
                            objrh.TotalCount = Convert.ToInt32(finalcount);
                            objrh.Percentage = fp;
                            objrh.Month = Month;
                            objrh.year = Year;
                            DB.RHComplaintLists.AddObject(objrh);
                            DB.SaveChanges();
                        }

                    }
                }

            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
            }
            decimal finalcategory = 0;
            for (int i = 0; i < dt5.Rows.Count; i++)
            {
                finalcategory = finalcategory + Convert.ToInt32(dt5.Rows[i]["CatCount"]);
            }
            lblcategory.Text = finalcategory.ToString();

            //decimal finalcat = 0;
            //for (int i = 0; i < dt5.Rows.Count; i++)
            //{
            //    finalcat = finalcat + Convert.ToDecimal(dt5.Rows[i]["Tempdatatable.percentage"]);
            //}
            //lblpercategory.Text = finalcat.ToString();

            //string fc1 = " select TickCatID , RHComplaintList.Name  , COUNT(*) as CatCount , RHComplaintList.percentage  " +
            //             " FROM  CRMMainActivities , RHComplaintList   where CRMMainActivities.TenentID =  " + TID +
            //             " and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "' " +
            //             " and RHComplaintList.Type=2 and RHComplaintList.TenentID = CRMMainActivities.TenentID   and RHComplaintList.ID = CRMMainActivities.TickCatID " +
            //             "  and CRMMainActivities.TickCatID is not null and CRMMainActivities.UploadDate is not null group by TickCatID, RHComplaintList.Name  , RHComplaintList.percentage ";


            string fc1 = "select ID as TickCatID, Name as Name ,Count as CatCount ,percentage as percentage" +
                         " FROM    RHComplaintList   where TenentID =  " + TID + " and Month='" + Month + "' and year='" + Year + "' " +
                         " and Type=2 and  RHComplaintList.ID is not null and Month is not null";
            SqlCommand CMDs1 = new SqlCommand(fc1, con);
            SqlDataAdapter ADBs1 = new SqlDataAdapter(CMDs1);
            DataSet dss1 = new DataSet();
            ADBs1.Fill(dss1);
            DataTable dts1 = dss1.Tables[0];
            ListView6.DataSource = dts1;
            ListView6.DataBind();


            string SQOCommad5 = " select TickSubCatID , ICSUBCATEGORY.SUBCATNAME  , COUNT(*) as subCatCount  FROM  CRMMainActivities , ICSUBCATEGORY  " +
                                 " where CRMMainActivities.TenentID = " + TID + " and ICSUBCATEGORY.SUBCATTYPE='HelpDesk' " +
                                 " and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "' and ICSUBCATEGORY.TenentID = CRMMainActivities.TenentID " +
                                 " and ICSUBCATEGORY.SUBCATID = CRMMainActivities.TickSubCatID and CRMMainActivities.TickSubCatID is not null and CRMMainActivities.UploadDate is not null group by TickSubCatID, ICSUBCATEGORY.SUBCATNAME ";

            SqlCommand CMD6 = new SqlCommand(SQOCommad5, con);
            SqlDataAdapter ADB6 = new SqlDataAdapter(CMD6);
            DataSet ds6 = new DataSet();
            ADB6.Fill(ds6);
            DataTable dt6 = ds6.Tables[0];

            decimal fs = 0;

            for (int a = 0; a <= dt6.Rows.Count - 1; a++)
            {
                fs = fs + Convert.ToInt32(dt6.Rows[a]["subCatCount"]);
            }


            if (dt6.Rows.Count > 0)
            {
                if (ds6.Tables[0] != null && ds6.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dt6.Rows.Count - 1; i++)
                    {
                        decimal TickSubCatID = Convert.ToInt32(dt6.Rows[i]["TickSubCatID"]);
                        if (DB.RHComplaintLists.Where(p => p.TenentID == TID && p.ID == TickSubCatID && p.Type == 3 && p.Month == Month && p.year == Year).Count() <= 0)
                        {
                            decimal subCatCount = Convert.ToInt32(dt6.Rows[i]["subCatCount"]);
                            string SUBCATNAME = DB.ICSUBCATEGORies.Single(p => p.TenentID == TID && p.SUBCATID == TickSubCatID).SUBCATNAME;
                            //Tempdatatable objprofile = new Tempdatatable();
                            //objprofile.TenentID = TID;
                            //objprofile.Type = 3;
                            //objprofile.ID = TickSubCatID;
                            //objprofile.Name = SUBCATNAME;
                            //objprofile.UserId = UIN;
                            //objprofile.Count = subCatCount;
                            //objprofile.TotalCount = Convert.ToInt32(fs);
                            //decimal percent = (subCatCount / fs) * 100;
                            //objprofile.percentage = percent;
                            //DB.Tempdatatables.AddObject(objprofile);
                            //DB.SaveChanges();
                            RHComplaintList objrh = new RHComplaintList();
                            objrh.TenentID = TID;
                            objrh.Type = 3;
                            objrh.ID =Convert.ToInt32(TickSubCatID);
                            objrh.Name = SUBCATNAME;
                            objrh.Count =Convert.ToInt32(subCatCount);
                            decimal percent1 = (subCatCount / finalcount) ;
                            string s1 = percent1.ToString("N3");
                            decimal ds1 = Convert.ToDecimal(s1);
                            decimal fp = ds1 * 100;
                            objrh.TotalCount = Convert.ToInt32(finalcount);
                            objrh.Percentage = fp;
                            objrh.Month = Month;
                            objrh.year = Year;
                            DB.RHComplaintLists.AddObject(objrh);
                            DB.SaveChanges();
                        }

                    }
                }

            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
            }
            decimal finalsubcategory = 0;
            for (int i = 0; i < dt6.Rows.Count; i++)
            {
                finalsubcategory = finalsubcategory + Convert.ToInt32(dt6.Rows[i]["subCatCount"]);
            }
            lblsubcategory.Text = finalsubcategory.ToString();


            //decimal finalpersubcategory = 0;
            //for (int i = 0; i < dt6.Rows.Count; i++)
            //{
            //    finalpersubcategory = finalpersubcategory + Convert.ToDecimal(dt6.Rows[i]["Tempdatatable.percentage"]);
            //}
            //lblpersubcategory.Text = finalpersubcategory.ToString();

            //string fs1 = " select TickSubCatID , RHComplaintList.Name  , COUNT(*) as subCatCount , RHComplaintList.percentage  " +
            //             " FROM  CRMMainActivities , RHComplaintList   where CRMMainActivities.TenentID =  " + TID +
            //             " and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "' " +
            //             " and RHComplaintList.Type=3 and RHComplaintList.TenentID = CRMMainActivities.TenentID   and RHComplaintList.ID = CRMMainActivities.TickSubCatID " +
            //             "  and CRMMainActivities.TickSubCatID is not null and CRMMainActivities.UploadDate is not null group by TickSubCatID, RHComplaintList.Name  , RHComplaintList.percentage ";

            string fs1 = "select ID as TickSubCatID, Name as Name  ,Count as subCatCount , percentage as percentage" +
                         " FROM    RHComplaintList   where TenentID =  " + TID + " and Month='" + Month + "' and year='" + Year + "' " +
                         " and Type=3 and  RHComplaintList.ID is not null and Month is not null";

            SqlCommand CMDs2 = new SqlCommand(fs1, con);
            SqlDataAdapter ADBs2 = new SqlDataAdapter(CMDs2);
            DataSet dss2 = new DataSet();
            ADBs2.Fill(dss2);
            DataTable dts2 = dss2.Tables[0];
            ListView7.DataSource = dss2;
            ListView7.DataBind();


            string SQOCommad6 = "select TickPhysicalLocation , REFTABLE.REFNAME1  , COUNT(*) as LocCount  FROM  CRMMainActivities , REFTABLE " +
                                " where CRMMainActivities.TenentID = " + TID + " and REFTABLE.REFTYPE='Ticket' and REFTABLE.REFSUBTYPE='PhysicalLocation' " +
                                " and FORMAT(CRMMainActivities.UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "' and REFTABLE.TenentID = CRMMainActivities.TenentID  " +
                                " and REFTABLE.REFID = CRMMainActivities.TickPhysicalLocation and CRMMainActivities.TickPhysicalLocation is not null and CRMMainActivities.UploadDate is not null group by TickPhysicalLocation, REFTABLE.REFNAME1 ";

            SqlCommand CMD7 = new SqlCommand(SQOCommad6, con);
            SqlDataAdapter ADB7 = new SqlDataAdapter(CMD7);
            DataSet ds7 = new DataSet();
            ADB7.Fill(ds7);
            DataTable dt7 = ds7.Tables[0];

            decimal fl = 0;

            for (int a = 0; a <= dt7.Rows.Count - 1; a++)
            {
                fl = fl + Convert.ToInt32(dt7.Rows[a]["LocCount"]);
            }


            if (dt7.Rows.Count > 0)
            {
                if (ds7.Tables[0] != null && ds7.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dt7.Rows.Count - 1; i++)
                    {
                        decimal TickPhysicalLocation = Convert.ToInt32(dt7.Rows[i]["TickPhysicalLocation"]);
                        if (DB.RHComplaintLists.Where(p => p.TenentID == TID && p.ID == TickPhysicalLocation && p.Type == 4 && p.Month == Month && p.year == Year).Count() <= 0)
                        {


                            decimal LocCount = Convert.ToInt32(dt7.Rows[i]["LocCount"]);
                            string REFNAME1 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == TickPhysicalLocation && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation").REFNAME1;


                            //Tempdatatable objprofile = new Tempdatatable();
                            //objprofile.TenentID = TID;
                            //objprofile.Type = 4;
                            //objprofile.ID = TickPhysicalLocation;
                            //objprofile.Name = REFNAME1;
                            //objprofile.UserId = UIN;
                            //objprofile.Count = LocCount;
                            //objprofile.TotalCount = Convert.ToInt32(fl);
                            //decimal percent = (LocCount / fs) * 100;
                            //objprofile.percentage = percent;
                            //DB.Tempdatatables.AddObject(objprofile);
                            //DB.SaveChanges();
                            RHComplaintList objrh = new RHComplaintList();
                            objrh.TenentID = TID;
                            objrh.Type = 4;
                            objrh.ID =Convert.ToInt32(TickPhysicalLocation);
                            objrh.Name = REFNAME1;
                            objrh.Count =Convert.ToInt32(LocCount);
                            decimal percent1 = (LocCount / finalcount);
                            string s1 = percent1.ToString("N3");
                            decimal ds1 = Convert.ToDecimal(s1);
                            decimal fp = ds1 * 100;
                            objrh.TotalCount = Convert.ToInt32(finalcount);
                            objrh.Percentage = fp;
                            objrh.Month = Month;
                            objrh.year = Year;
                            DB.RHComplaintLists.AddObject(objrh);
                            DB.SaveChanges();
                        }

                    }
                }

            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
            }

            decimal finallocation = 0;
            for (int i = 0; i < dt7.Rows.Count; i++)
            {
                finallocation = finallocation + Convert.ToInt32(dt7.Rows[i]["LocCount"]);
            }
            lbllocation.Text = finallocation.ToString();

            //decimal finalperlocation = 0;
            //for (int i = 0; i < dt7.Rows.Count; i++)
            //{
            //    finalperlocation = finalperlocation + Convert.ToDecimal(dt7.Rows[i]["Tempdatatable.percentage"]);
            //}
            //lblperloc.Text = finalperlocation.ToString();

            //string fl1 = " select TickPhysicalLocation , RHComplaintList.Name  , COUNT(*) as LocCount , RHComplaintList.percentage  " +
            //             " FROM  CRMMainActivities , RHComplaintList   where CRMMainActivities.TenentID =  " + TID +
            //             " and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "' " +
            //             " and RHComplaintList.Type=4 and RHComplaintList.TenentID = CRMMainActivities.TenentID   and RHComplaintList.ID = CRMMainActivities.TickPhysicalLocation " +
            //             "  and CRMMainActivities.TickPhysicalLocation is not null and CRMMainActivities.UploadDate is not null group by TickPhysicalLocation, RHComplaintList.Name  , RHComplaintList.percentage ";

            string fl1 = "select ID as TickPhysicalLocation , Name as Name  ,Count as LocCount , percentage as percentage" +
                          " FROM    RHComplaintList   where TenentID =  " + TID + " and Month='" + Month + "' and year='" + Year + "' " +
                          " and Type=4 and  RHComplaintList.ID is not null and Month is not null";

            SqlCommand CMDs3 = new SqlCommand(fl1, con);
            SqlDataAdapter ADBs3 = new SqlDataAdapter(CMDs3);
            DataSet dss3 = new DataSet();
            ADBs3.Fill(dss3);
            DataTable dts3 = dss3.Tables[0];
            ListView5.DataSource = dts3;
            ListView5.DataBind();
        }

        protected void btnsub_Click(object sender, EventArgs e)
        {
            drpmonthfrom.SelectedIndex = 0;
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            con.Open();
            SqlCommand command;
            SqlDataAdapter Ang = new SqlDataAdapter();
            int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            string sql5 = "Delete from Tempdatatable where tenentID=" + TID;
            command = new SqlCommand(sql5, con);
            command.ExecuteNonQuery();
            command.Dispose();



            DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime Enddate = Convert.ToDateTime(txtenddate.Text);

            string stdate = startdate.ToString("yyyy-MM-dd");
            string etdate = Enddate.ToString("yyyy-MM-dd");


            string SQOCommad = " select TickDepartmentID , DeptITSuper.DeptName  , COUNT(*) as DeptCount  FROM  CRMMainActivities , DeptITSuper " +

                               "  where CRMMainActivities.TenentID = " + TID + " and UploadDate BETWEEN '" + stdate + "' AND    '" + etdate + "' and DeptITSuper.TenentID = CRMMainActivities.TenentID   and DeptITSuper.DeptID = CRMMainActivities.TickDepartmentID and CRMMainActivities.TickDepartmentID is not null and CRMMainActivities.UploadDate is not null" +

                               " group by TickDepartmentID, DeptITSuper.DeptName";

            List<Tempdatatable> Listtemp = DB.Tempdatatables.Where(p => p.TenentID == TID).ToList();

            List<Tempdatatable> list1 = new List<Tempdatatable>();
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];

            //weeeeeeew

            decimal finalcount = 0;

            for (int a = 0; a <= dt.Rows.Count - 1; a++)
            {
                finalcount = finalcount + Convert.ToInt32(dt.Rows[a]["DeptCount"]);
            }


            if (dt.Rows.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {

                        decimal TickDepartmentID = Convert.ToInt32(dt.Rows[i]["TickDepartmentID"]);
                        int DeptCount = Convert.ToInt32(dt.Rows[i]["DeptCount"]);
                        string deptName = DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == TickDepartmentID).DeptName;


                        Tempdatatable objprofile = new Tempdatatable();
                        objprofile.TenentID = TID;
                        objprofile.Type = 1;
                        objprofile.ID =Convert.ToInt32(TickDepartmentID);
                        objprofile.Name = deptName;
                        objprofile.UserId = UIN;
                        objprofile.Count = DeptCount;
                        objprofile.TotalCount = Convert.ToInt32(finalcount);
                        decimal percent = (DeptCount / finalcount);
                        string s1 = percent.ToString("N3");
                        decimal ds1 = Convert.ToDecimal(s1);
                        decimal fp = ds1 * 100;

                        //string m1 = fp.ToString();
                        //var input = m1;
                        //var parts = input.Split('.');
                        //var part1 = parts[0];
                        //var part2 = parts[1];
                        //decimal p1 = Convert.ToDecimal(part1);
                        //decimal d1 = p1 + 1;
                        //objprofile.percentage = Convert.ToDecimal(d1);

                        objprofile.percentage = fp;
                        DB.Tempdatatables.AddObject(objprofile);
                        DB.SaveChanges();

                    }

                }

            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No Data available between this dates');window.location='HelpdeskExcRep.aspx';", true);
            }

            int finalTotal = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                finalTotal = finalTotal + Convert.ToInt32(dt.Rows[i]["DeptCount"]);
            }
            lblFinalTotal.Text = finalTotal.ToString();



            string com = " select TickDepartmentID , Tempdatatable.Name  , COUNT(*) as DeptCount , Tempdatatable.percentage  " +
                         " FROM  CRMMainActivities , Tempdatatable   where CRMMainActivities.TenentID =  " + TID +
                         " and UploadDate BETWEEN ' " + stdate + "' AND    '" + etdate + "' " +
                         " and Tempdatatable.Type=1 and Tempdatatable.TenentID = CRMMainActivities.TenentID   and Tempdatatable.ID = CRMMainActivities.TickDepartmentID " +
                         "  and CRMMainActivities.TickDepartmentID is not null and CRMMainActivities.UploadDate is not null group by TickDepartmentID, Tempdatatable.Name  , Tempdatatable.percentage ";

            SqlCommand CMDs = new SqlCommand(com, con);
            SqlDataAdapter ADBs = new SqlDataAdapter(CMDs);
            DataSet dss = new DataSet();
            ADBs.Fill(dss);
            DataTable dts = dss.Tables[0];
            ListView3.DataSource = dts;
            ListView3.DataBind();

            Chart1.Visible = true;
            string query = string.Format(" Select  Percentage,left(Name,15)  from Tempdatatable where TenentID= " + TID + " and type=1 group by Percentage,Name");
            DataTable dtt = GetData(query);
            string[] x = new string[dtt.Rows.Count];
            int[] y = new int[dtt.Rows.Count];
            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                x[i] = dtt.Rows[i][1].ToString();
                y[i] = Convert.ToInt32(dtt.Rows[i][0]);
            }
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Series[0].ChartType = SeriesChartType.Pie;
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart1.Legends[0].Enabled = true;
            // Set pie labels to be outside the pie chart
            this.Chart1.Series[0]["PieLabelStyle"] = "Disabled";

            // Add a legend to the chart and dock it to the bottom-center
            this.Chart1.Legends.Add("Legend1");
            this.Chart1.Legends[0].Enabled = true;
            this.Chart1.Legends[0].Docking = Docking.Bottom;
            this.Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

            // Show labels in the legend in the format "Name (### %)"
            this.Chart1.Series[0].LegendText = "#VALX (#PERCENT)";

            // By sorting the data points, they show up in proper ascending order in the legend
            this.Chart1.DataManipulator.Sort(PointSortOrder.Descending, Chart1.Series[0]);



            string SQOCommad4 = " select TickCatID , ICCATEGORY.CATNAME  , COUNT(*) as CatCount  FROM  CRMMainActivities , ICCATEGORY " +
                                " where CRMMainActivities.TenentID =  " + TID + " and ICCATEGORY.CATTYPE='HelpDesk' " +
                                " and CRMMainActivities.UploadDate BETWEEN ' " + stdate + "'  AND    ' " + etdate + "' and ICCATEGORY.TenentID = CRMMainActivities.TenentID " +
                                " and ICCATEGORY.CATID = CRMMainActivities.TickCatID and CRMMainActivities.TickCatID is not null and CRMMainActivities.UploadDate is not null group by TickCatID, ICCATEGORY.CATNAME ";

            SqlCommand CMD5 = new SqlCommand(SQOCommad4, con);
            SqlDataAdapter ADB5 = new SqlDataAdapter(CMD5);
            DataSet ds5 = new DataSet();
            ADB5.Fill(ds5);
            DataTable dt5 = ds5.Tables[0];

            decimal fc = 0;

            for (int a = 0; a <= dt5.Rows.Count - 1; a++)
            {
                fc = fc + Convert.ToInt32(dt5.Rows[a]["CatCount"]);
            }


            if (dt5.Rows.Count > 0)
            {
                if (ds5.Tables[0] != null && ds5.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dt5.Rows.Count - 1; i++)
                    {

                        decimal TickCatID = Convert.ToInt32(dt5.Rows[i]["TickCatID"]);
                        int CatCount = Convert.ToInt32(dt5.Rows[i]["CatCount"]);
                        string CATNAME = DB.ICCATEGORies.Single(p => p.TenentID == TID && p.CATID == TickCatID).CATNAME;


                        Tempdatatable objprofile = new Tempdatatable();
                        objprofile.TenentID = TID;
                        objprofile.Type = 2;
                        objprofile.ID =Convert.ToInt32(TickCatID);
                        objprofile.Name = CATNAME;
                        objprofile.UserId = UIN;
                        objprofile.Count = CatCount;
                        objprofile.TotalCount = Convert.ToInt32(fc);
                        decimal percent = (CatCount / fc) ;
                        string s1 = percent.ToString("N3");
                        decimal ds1 = Convert.ToDecimal(s1);
                        decimal fp = ds1 * 100;
                        string m1 = fp.ToString();
                        ////var input = m1;
                        ////var parts = input.Split('.');
                        ////var part1 = parts[0];
                        ////var part2 = parts[1];
                        ////decimal p1 = Convert.ToDecimal(part1);
                        ////decimal d1 = p1 + 1;
                        ////objprofile.percentage = Convert.ToDecimal(d1);
                        objprofile.percentage = fp;
                        DB.Tempdatatables.AddObject(objprofile);
                        DB.SaveChanges();

                    }
                }

            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No Data available between this dates');window.location='HelpdeskExcRep.aspx';", true);
            }

            decimal finalcategory = 0;
            for (int i = 0; i < dt5.Rows.Count; i++)
            {
                finalcategory = finalcategory + Convert.ToInt32(dt5.Rows[i]["CatCount"]);
            }
            lblcategory.Text = finalcategory.ToString();

            //decimal finalcat = 0;
            //for (int i = 0; i < dt5.Rows.Count; i++)
            //{
            //    finalcat = finalcat + Convert.ToDecimal(dt5.Rows[i]["Tempdatatable.percentage"]);
            //}
            //lblpercategory.Text = finalcat.ToString();

            string fc1 = " select TickCatID , Tempdatatable.Name  , COUNT(*) as CatCount , Tempdatatable.percentage  " +
                         " FROM  CRMMainActivities , Tempdatatable   where CRMMainActivities.TenentID =  " + TID +
                         " and UploadDate BETWEEN ' " + stdate + "' AND    '" + etdate + "' " +
                         " and Tempdatatable.Type=2 and Tempdatatable.TenentID = CRMMainActivities.TenentID   and Tempdatatable.ID = CRMMainActivities.TickCatID " +
                         "  and CRMMainActivities.TickCatID is not null and CRMMainActivities.UploadDate is not null group by TickCatID, Tempdatatable.Name  , Tempdatatable.percentage ";

            SqlCommand CMDs1 = new SqlCommand(fc1, con);
            SqlDataAdapter ADBs1 = new SqlDataAdapter(CMDs1);
            DataSet dss1 = new DataSet();
            ADBs1.Fill(dss1);
            DataTable dts1 = dss1.Tables[0];
            ListView6.DataSource = dts1;
            ListView6.DataBind();


            string SQOCommad5 = " select TickSubCatID , ICSUBCATEGORY.SUBCATNAME  , COUNT(*) as subCatCount  FROM  CRMMainActivities , ICSUBCATEGORY  " +
                                 " where CRMMainActivities.TenentID = " + TID + " and ICSUBCATEGORY.SUBCATTYPE='HelpDesk' " +
                                 " and CRMMainActivities.UploadDate BETWEEN ' " + stdate + "' AND    ' " + etdate + "'  and ICSUBCATEGORY.TenentID = CRMMainActivities.TenentID " +
                                 " and ICSUBCATEGORY.SUBCATID = CRMMainActivities.TickSubCatID and CRMMainActivities.TickSubCatID is not null and CRMMainActivities.UploadDate is not null group by TickSubCatID, ICSUBCATEGORY.SUBCATNAME ";

            SqlCommand CMD6 = new SqlCommand(SQOCommad5, con);
            SqlDataAdapter ADB6 = new SqlDataAdapter(CMD6);
            DataSet ds6 = new DataSet();
            ADB6.Fill(ds6);
            DataTable dt6 = ds6.Tables[0];

            decimal fs = 0;

            for (int a = 0; a <= dt6.Rows.Count - 1; a++)
            {
                fs = fs + Convert.ToInt32(dt6.Rows[a]["subCatCount"]);
            }


            if (dt6.Rows.Count > 0)
            {
                if (ds6.Tables[0] != null && ds6.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dt6.Rows.Count - 1; i++)
                    {

                        decimal TickSubCatID = Convert.ToInt32(dt6.Rows[i]["TickSubCatID"]);
                        int subCatCount = Convert.ToInt32(dt6.Rows[i]["subCatCount"]);
                        string SUBCATNAME = DB.ICSUBCATEGORies.Single(p => p.TenentID == TID && p.SUBCATID == TickSubCatID).SUBCATNAME;


                        Tempdatatable objprofile = new Tempdatatable();
                        objprofile.TenentID = TID;
                        objprofile.Type = 3;
                        objprofile.ID =Convert.ToInt32(TickSubCatID);
                        objprofile.Name = SUBCATNAME;
                        objprofile.UserId = UIN;
                        objprofile.Count = subCatCount;
                        objprofile.TotalCount = Convert.ToInt32(fs);
                        decimal percent = (subCatCount / fs) ;
                        string s1 = percent.ToString("N3");
                        decimal ds1 = Convert.ToDecimal(s1);
                        decimal fp = ds1 * 100;
                        string m1 = fp.ToString();
                        //var input = m1;
                        //var parts = input.Split('.');
                        //var part1 = parts[0];
                        //var part2 = parts[1];
                        //decimal p1 = Convert.ToDecimal(part1);
                        //decimal d1 = p1 + 1;
                        //objprofile.percentage = Convert.ToDecimal(d1);
                        objprofile.percentage = fp;
                        DB.Tempdatatables.AddObject(objprofile);
                        DB.SaveChanges();

                    }
                }

            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No Data available between this dates');window.location='HelpdeskExcRep.aspx';", true);
            }

            decimal finalsubcategory = 0;
            for (int i = 0; i < dt6.Rows.Count; i++)
            {
                finalsubcategory = finalsubcategory + Convert.ToInt32(dt6.Rows[i]["subCatCount"]);
            }
            lblsubcategory.Text = finalsubcategory.ToString();

            //decimal finalpersubcategory = 0;
            //for (int i = 0; i < dt6.Rows.Count; i++)
            //{
            //    finalpersubcategory = finalpersubcategory + Convert.ToDecimal(dt6.Rows[i]["Tempdatatable.percentage"]);
            //}
            //lblpersubcategory.Text = finalpersubcategory.ToString();

            string fs1 = " select TickSubCatID , Tempdatatable.Name  , COUNT(*) as subCatCount , Tempdatatable.percentage  " +
                         " FROM  CRMMainActivities , Tempdatatable   where CRMMainActivities.TenentID =  " + TID +
                         " and UploadDate BETWEEN ' " + stdate + "' AND    '" + etdate + "' " +
                         " and Tempdatatable.Type=3 and Tempdatatable.TenentID = CRMMainActivities.TenentID   and Tempdatatable.ID = CRMMainActivities.TickSubCatID " +
                         "  and CRMMainActivities.TickSubCatID is not null and CRMMainActivities.UploadDate is not null group by TickSubCatID, Tempdatatable.Name  , Tempdatatable.percentage ";

            SqlCommand CMDs2 = new SqlCommand(fs1, con);
            SqlDataAdapter ADBs2 = new SqlDataAdapter(CMDs2);
            DataSet dss2 = new DataSet();
            ADBs2.Fill(dss2);
            DataTable dts2 = dss2.Tables[0];
            ListView7.DataSource = dss2;
            ListView7.DataBind();


            string SQOCommad6 = "select TickPhysicalLocation , REFTABLE.REFNAME1  , COUNT(*) as LocCount  FROM  CRMMainActivities , REFTABLE " +
                                " where CRMMainActivities.TenentID = " + TID + " and REFTABLE.REFTYPE='Ticket' and REFTABLE.REFSUBTYPE='PhysicalLocation' " +
                                " and CRMMainActivities.UploadDate BETWEEN ' " + stdate + " ' AND    ' " + etdate + "' and REFTABLE.TenentID = CRMMainActivities.TenentID  " +
                                " and REFTABLE.REFID = CRMMainActivities.TickPhysicalLocation and CRMMainActivities.TickPhysicalLocation is not null and CRMMainActivities.UploadDate is not null group by TickPhysicalLocation, REFTABLE.REFNAME1 ";

            SqlCommand CMD7 = new SqlCommand(SQOCommad6, con);
            SqlDataAdapter ADB7 = new SqlDataAdapter(CMD7);
            DataSet ds7 = new DataSet();
            ADB7.Fill(ds7);
            DataTable dt7 = ds7.Tables[0];

            decimal fl = 0;

            for (int a = 0; a <= dt7.Rows.Count - 1; a++)
            {
                fl = fl + Convert.ToInt32(dt7.Rows[a]["LocCount"]);
            }


            if (dt7.Rows.Count > 0)
            {
                if (ds7.Tables[0] != null && ds7.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dt7.Rows.Count - 1; i++)
                    {

                        decimal TickPhysicalLocation = Convert.ToInt32(dt7.Rows[i]["TickPhysicalLocation"]);
                        int LocCount = Convert.ToInt32(dt7.Rows[i]["LocCount"]);
                        string REFNAME1 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == TickPhysicalLocation && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation").REFNAME1;


                        Tempdatatable objprofile = new Tempdatatable();
                        objprofile.TenentID = TID;
                        objprofile.Type = 4;
                        objprofile.ID =Convert.ToInt32(TickPhysicalLocation);
                        objprofile.Name = REFNAME1;
                        objprofile.UserId = UIN;
                        objprofile.Count = LocCount;
                        objprofile.TotalCount = Convert.ToInt32(fl);
                        decimal percent = (LocCount / fs) ;
                        string s1 = percent.ToString("N3");
                        decimal ds1 = Convert.ToDecimal(s1);
                        decimal fp = ds1 * 100;
                        string m1 = fp.ToString();
                        //var input = m1;
                        //var parts = input.Split('.');
                        //var part1 = parts[0];
                        //var part2 = parts[1];
                        //decimal p1 = Convert.ToDecimal(part1);
                        //decimal d1 = p1 + 1;
                        //objprofile.percentage = Convert.ToDecimal(d1);
                        objprofile.percentage = fp;
                        DB.Tempdatatables.AddObject(objprofile);
                        DB.SaveChanges();

                    }
                }

            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No Data available between this dates');window.location='HelpdeskExcRep.aspx';", true);
            }

            decimal finallocation = 0;
            for (int i = 0; i < dt7.Rows.Count; i++)
            {
                finallocation = finallocation + Convert.ToInt32(dt7.Rows[i]["LocCount"]);
            }
            lbllocation.Text = finallocation.ToString();

            //decimal finalperlocation = 0;
            //for (int i = 0; i < dt7.Rows.Count; i++)
            //{
            //    finalperlocation = finalperlocation + Convert.ToDecimal(dt7.Rows[i]["Tempdatatable.percentage"]);
            //}
            //lblperloc.Text = finalperlocation.ToString();

            string fl1 = " select TickPhysicalLocation , Tempdatatable.Name  , COUNT(*) as LocCount , Tempdatatable.percentage  " +
                         " FROM  CRMMainActivities , Tempdatatable   where CRMMainActivities.TenentID =  " + TID +
                         " and UploadDate BETWEEN ' " + stdate + "' AND    '" + etdate + "' " +
                         " and Tempdatatable.Type=4 and Tempdatatable.TenentID = CRMMainActivities.TenentID   and Tempdatatable.ID = CRMMainActivities.TickPhysicalLocation " +
                         "  and CRMMainActivities.TickPhysicalLocation is not null and CRMMainActivities.UploadDate is not null group by TickPhysicalLocation, Tempdatatable.Name  , Tempdatatable.percentage ";

            SqlCommand CMDs3 = new SqlCommand(fl1, con);
            SqlDataAdapter ADBs3 = new SqlDataAdapter(CMDs3);
            DataSet dss3 = new DataSet();
            ADBs3.Fill(dss3);
            DataTable dts3 = dss3.Tables[0];
            ListView5.DataSource = dts3;
            ListView5.DataBind();

        }

        protected void ListView3_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "lnkpn")
            {

                Label lblDeptID = (Label)e.Item.FindControl("lblDeptID");
                Label lblname = (Label)e.Item.FindControl("lblname");
                Label lbldeptcount = (Label)e.Item.FindControl("lbldeptcount");
                Label lblper = (Label)e.Item.FindControl("lblper");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.TickDepartmentID == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkpn1")
            {
                Label lblDeptID = (Label)e.Item.FindControl("lblDeptID");
                Label lblname = (Label)e.Item.FindControl("lblname");
                Label lbldeptcount = (Label)e.Item.FindControl("lbldeptcount");
                Label lblper = (Label)e.Item.FindControl("lblper");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkpn2")
            {
                Label lblDeptID = (Label)e.Item.FindControl("lblDeptID");
                Label lblname = (Label)e.Item.FindControl("lblname");
                Label lbldeptcount = (Label)e.Item.FindControl("lbldeptcount");
                Label lblper = (Label)e.Item.FindControl("lblper");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
            else if (e.CommandName == "lnkpn4")
            {
                Label lblDeptID = (Label)e.Item.FindControl("lblDeptID");
                Label lblname = (Label)e.Item.FindControl("lblname");
                Label lbldeptcount = (Label)e.Item.FindControl("lbldeptcount");
                Label lblper = (Label)e.Item.FindControl("lblper");
                int ID = Convert.ToInt32(e.CommandArgument);
                int TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
                Database.CRMMainActivity objREFTABLE = DB.CRMMainActivities.Single(p => p.TenentID == TID && p.MasterCODE == ID);
                ViewState["Edit"] = ID;
                Response.Redirect("../POS/ViewTicket.aspx?Mastercode=" + ID);
            }
        }

        protected void ListView3_ItemCommand1(object sender, ListViewCommandEventArgs e)
        {

        }



        private static DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(query);
            String constr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            return dt;
        }

        protected void btnrefresh_Click(object sender, EventArgs e)
        {
            txtstartdate.Text = "";
            txtenddate.Text = "";
            TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            con.Open();
            SqlCommand command;
            SqlDataAdapter Ang = new SqlDataAdapter();
            int UIN = Convert.ToInt32(((USER_MST)Session["USER"]).USER_ID);
            string sql5 = "Delete from Tempdatatable where tenentID=" + TID;
            command = new SqlCommand(sql5, con);
            command.ExecuteNonQuery();
            command.Dispose();

            DateTime monthyear = Convert.ToDateTime(drpmonthfrom.SelectedValue);
            string Month = monthyear.ToString("MMMM");
            string Year = monthyear.ToString("yyyy");

            string SQOCommad = " select TickDepartmentID , DeptITSuper.DeptName  , COUNT(*) as DeptCount  FROM  CRMMainActivities , DeptITSuper " +

                               "  where CRMMainActivities.TenentID = " + TID + "  and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "'  and DeptITSuper.TenentID = CRMMainActivities.TenentID   and DeptITSuper.DeptID = CRMMainActivities.TickDepartmentID and CRMMainActivities.TickDepartmentID is not null and CRMMainActivities.UploadDate is not null" +

                               " group by TickDepartmentID, DeptITSuper.DeptName";

            List<Tempdatatable> Listtemp = DB.Tempdatatables.Where(p => p.TenentID == TID).ToList();

            List<Tempdatatable> list1 = new List<Tempdatatable>();
            SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
            SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
            DataSet ds = new DataSet();
            ADB.Fill(ds);
            DataTable dt = ds.Tables[0];

            decimal finalcount = 0;

            for (int a = 0; a <= dt.Rows.Count - 1; a++)
            {
                finalcount = finalcount + Convert.ToInt32(dt.Rows[a]["DeptCount"]);
            }


            if (dt.Rows.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        int TickDepartmentID = Convert.ToInt32(dt.Rows[i]["TickDepartmentID"]);
                        if (DB.RHComplaintLists.Where(p => p.TenentID == TID && p.ID == TickDepartmentID && p.Type == 1 && p.Month == Month && p.year == Year).Count() <= 0)
                        {
                            decimal DeptCount = Convert.ToInt32(dt.Rows[i]["DeptCount"]);
                            string deptName = DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == TickDepartmentID).DeptName;
                            RHComplaintList objrh = new RHComplaintList();
                            objrh.TenentID = TID;
                            objrh.Type = 1;
                            objrh.ID = TickDepartmentID;
                            objrh.Name = deptName;
                            objrh.Count =Convert.ToInt32(DeptCount);
                            decimal percent1 = (DeptCount / finalcount);
                            objrh.TotalCount = Convert.ToInt32(finalcount);
                            string s1 = percent1.ToString("N3");
                            decimal ds1 = Convert.ToDecimal(s1);
                            decimal fp = ds1 * 100;
                            objrh.Percentage = fp;
                            objrh.Month = Month;
                            objrh.year = Year;
                            DB.RHComplaintLists.AddObject(objrh);
                            DB.SaveChanges();
                        }
                    }
                }
            }


            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
            }

            int finalTotal = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                finalTotal = finalTotal + Convert.ToInt32(dt.Rows[i]["DeptCount"]);
            }
            lblFinalTotal.Text = finalTotal.ToString();

            //decimal finalTot = 0;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    finalTot = finalTot + Convert.ToDecimal(dt.Rows[i]["Tempdatatable.percentage"]);
            //}
            //lblperdept.Text = finalTot.ToString();



            //string com = " select TickDepartmentID , RHComplaintList.Name  , COUNT(*) as DeptCount , RHComplaintList.percentage  " +
            //             " FROM  CRMMainActivities , RHComplaintList   where CRMMainActivities.TenentID =  " + TID +
            //             " and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "' " +
            //             " and RHComplaintList.Type=1 and RHComplaintList.TenentID = CRMMainActivities.TenentID   and RHComplaintList.ID = CRMMainActivities.TickDepartmentID " +
            //             "  and CRMMainActivities.TickDepartmentID is not null and CRMMainActivities.UploadDate is not null group by TickDepartmentID, RHComplaintList.Name  , RHComplaintList.percentage ";


            string com = "select ID as TickDepartmentID, Name as Name  ,Count as DeptCount , percentage as percentage" +
                         " FROM    RHComplaintList   where TenentID =  " + TID + "  and Month='" + Month + "' and year='" + Year + "' " +
                         " and Type=1 and  RHComplaintList.ID is not null and Month is not null";

            SqlCommand CMDs = new SqlCommand(com, con);
            SqlDataAdapter ADBs = new SqlDataAdapter(CMDs);
            DataSet dss = new DataSet();
            ADBs.Fill(dss);
            DataTable dts = dss.Tables[0];
            ListView3.DataSource = dts;
            ListView3.DataBind();


            Chart1.Visible = true;
            string query = string.Format("Select  Percentage,left(Name,15)  from RHComplaintList where TenentID= " + TID + " and type=1 and Month='" + Month + "' and year='" + Year + "' group by Percentage,Name");
            DataTable dtt = GetData(query);
            string[] x = new string[dtt.Rows.Count];
            int[] y = new int[dtt.Rows.Count];
            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                x[i] = dtt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dtt.Rows[i][0]);
            }
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Series[0].ChartType = SeriesChartType.Pie;
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart1.Legends[0].Enabled = true;
            // Set pie labels to be outside the pie chart
            this.Chart1.Series[0]["PieLabelStyle"] = "Disabled";

            // Add a legend to the chart and dock it to the bottom-center
            this.Chart1.Legends.Add("Legend1");
            this.Chart1.Legends[0].Enabled = true;
            this.Chart1.Legends[0].Docking = Docking.Bottom;
            this.Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

            // Show labels in the legend in the format "Name (### %)"
            this.Chart1.Series[0].LegendText = "#VALX (#PERCENT)";

            // By sorting the data points, they show up in proper ascending order in the legend
            this.Chart1.DataManipulator.Sort(PointSortOrder.Descending, Chart1.Series[0]);


            string SQOCommad4 = " select TickCatID , ICCATEGORY.CATNAME  , COUNT(*) as CatCount  FROM  CRMMainActivities , ICCATEGORY " +
                                " where CRMMainActivities.TenentID =  " + TID + " and CATTYPE='HelpDesk' " +
                                " and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "'  and ICCATEGORY.TenentID = CRMMainActivities.TenentID " +
                                " and ICCATEGORY.CATID = CRMMainActivities.TickCatID and CRMMainActivities.TickCatID is not null  and CRMMainActivities.UploadDate is not null group by TickCatID, ICCATEGORY.CATNAME ";

            SqlCommand CMD5 = new SqlCommand(SQOCommad4, con);
            SqlDataAdapter ADB5 = new SqlDataAdapter(CMD5);
            DataSet ds5 = new DataSet();
            ADB5.Fill(ds5);
            DataTable dt5 = ds5.Tables[0];

            decimal fc = 0;

            for (int a = 0; a <= dt5.Rows.Count - 1; a++)
            {
                fc = fc + Convert.ToInt32(dt5.Rows[a]["CatCount"]);
            }


            if (dt5.Rows.Count > 0)
            {
                if (ds5.Tables[0] != null && ds5.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dt5.Rows.Count - 1; i++)
                    {
                        decimal TickCatID = Convert.ToInt32(dt5.Rows[i]["TickCatID"]);
                        if (DB.RHComplaintLists.Where(p => p.TenentID == TID && p.ID == TickCatID && p.Type == 2 && p.Month == Month && p.year == Year).Count() <= 0)
                        {
                            int CatCount = Convert.ToInt32(dt5.Rows[i]["CatCount"]);
                            string CATNAME = DB.ICCATEGORies.Single(p => p.TenentID == TID && p.CATID == TickCatID).CATNAME;


                            //Tempdatatable objprofile = new Tempdatatable();
                            //objprofile.TenentID = TID;
                            //objprofile.Type = 2;
                            //objprofile.ID = TickCatID;
                            //objprofile.Name = CATNAME;
                            //objprofile.UserId = UIN;
                            //objprofile.Count = CatCount;
                            //objprofile.TotalCount = Convert.ToInt32(fc);
                            //decimal percent = (CatCount / fc) * 100;
                            //objprofile.percentage = percent;
                            //DB.Tempdatatables.AddObject(objprofile);
                            //DB.SaveChanges();
                            RHComplaintList objrh = new RHComplaintList();
                            objrh.TenentID = TID;
                            objrh.Type = 2;
                            objrh.ID =Convert.ToInt32(TickCatID);
                            objrh.Name = CATNAME;
                            objrh.Count = CatCount;
                            decimal percent1 = (CatCount / finalcount) ;
                            objrh.TotalCount = Convert.ToInt32(finalcount);
                            string s1 = percent1.ToString("N3");
                            decimal ds1 = Convert.ToDecimal(s1);
                            decimal fp = ds1 * 100;
                            objrh.Percentage = fp;
                            objrh.Month = Month;
                            objrh.year = Year;
                            DB.RHComplaintLists.AddObject(objrh);
                            DB.SaveChanges();
                        }

                    }
                }

            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
            }
            decimal finalcategory = 0;
            for (int i = 0; i < dt5.Rows.Count; i++)
            {
                finalcategory = finalcategory + Convert.ToInt32(dt5.Rows[i]["CatCount"]);
            }
            lblcategory.Text = finalcategory.ToString();

            //decimal finalcat = 0;
            //for (int i = 0; i < dt5.Rows.Count; i++)
            //{
            //    finalcat = finalcat + Convert.ToDecimal(dt5.Rows[i]["Tempdatatable.percentage"]);
            //}
            //lblpercategory.Text = finalcat.ToString();

            //string fc1 = " select TickCatID , RHComplaintList.Name  , COUNT(*) as CatCount , RHComplaintList.percentage  " +
            //             " FROM  CRMMainActivities , RHComplaintList   where CRMMainActivities.TenentID =  " + TID +
            //             " and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "' " +
            //             " and RHComplaintList.Type=2 and RHComplaintList.TenentID = CRMMainActivities.TenentID   and RHComplaintList.ID = CRMMainActivities.TickCatID " +
            //             "  and CRMMainActivities.TickCatID is not null and CRMMainActivities.UploadDate is not null group by TickCatID, RHComplaintList.Name  , RHComplaintList.percentage ";


            string fc1 = "select ID as TickCatID, Name as Name ,Count as CatCount ,percentage as percentage" +
                         " FROM    RHComplaintList   where TenentID =  " + TID + " and Month='" + Month + "' and year='" + Year + "' " +
                         " and Type=2 and  RHComplaintList.ID is not null and Month is not null";
            SqlCommand CMDs1 = new SqlCommand(fc1, con);
            SqlDataAdapter ADBs1 = new SqlDataAdapter(CMDs1);
            DataSet dss1 = new DataSet();
            ADBs1.Fill(dss1);
            DataTable dts1 = dss1.Tables[0];
            ListView6.DataSource = dts1;
            ListView6.DataBind();


            string SQOCommad5 = " select TickSubCatID , ICSUBCATEGORY.SUBCATNAME  , COUNT(*) as subCatCount  FROM  CRMMainActivities , ICSUBCATEGORY  " +
                                 " where CRMMainActivities.TenentID = " + TID + " and ICSUBCATEGORY.SUBCATTYPE='HelpDesk' " +
                                 " and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "' and ICSUBCATEGORY.TenentID = CRMMainActivities.TenentID " +
                                 " and ICSUBCATEGORY.SUBCATID = CRMMainActivities.TickSubCatID and CRMMainActivities.TickSubCatID is not null and CRMMainActivities.UploadDate is not null group by TickSubCatID, ICSUBCATEGORY.SUBCATNAME ";

            SqlCommand CMD6 = new SqlCommand(SQOCommad5, con);
            SqlDataAdapter ADB6 = new SqlDataAdapter(CMD6);
            DataSet ds6 = new DataSet();
            ADB6.Fill(ds6);
            DataTable dt6 = ds6.Tables[0];

            decimal fs = 0;

            for (int a = 0; a <= dt6.Rows.Count - 1; a++)
            {
                fs = fs + Convert.ToInt32(dt6.Rows[a]["subCatCount"]);
            }


            if (dt6.Rows.Count > 0)
            {
                if (ds6.Tables[0] != null && ds6.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dt6.Rows.Count - 1; i++)
                    {
                        decimal TickSubCatID = Convert.ToInt32(dt6.Rows[i]["TickSubCatID"]);
                        if (DB.RHComplaintLists.Where(p => p.TenentID == TID && p.ID == TickSubCatID && p.Type == 3 && p.Month == Month && p.year == Year).Count() <= 0)
                        {

                            int subCatCount = Convert.ToInt32(dt6.Rows[i]["subCatCount"]);
                            string SUBCATNAME = DB.ICSUBCATEGORies.Single(p => p.TenentID == TID && p.SUBCATID == TickSubCatID).SUBCATNAME;
                            //Tempdatatable objprofile = new Tempdatatable();
                            //objprofile.TenentID = TID;
                            //objprofile.Type = 3;
                            //objprofile.ID = TickSubCatID;
                            //objprofile.Name = SUBCATNAME;
                            //objprofile.UserId = UIN;
                            //objprofile.Count = subCatCount;
                            //objprofile.TotalCount = Convert.ToInt32(fs);
                            //decimal percent = (subCatCount / fs) * 100;
                            //objprofile.percentage = percent;
                            //DB.Tempdatatables.AddObject(objprofile);
                            //DB.SaveChanges();
                            RHComplaintList objrh = new RHComplaintList();
                            objrh.TenentID = TID;
                            objrh.Type = 3;
                            objrh.ID =Convert.ToInt32(TickSubCatID);
                            objrh.Name = SUBCATNAME;
                            objrh.Count = subCatCount;
                            decimal percent1 = (subCatCount / finalcount) ;
                            objrh.TotalCount = Convert.ToInt32(finalcount);
                            string s1 = percent1.ToString("N3");
                            decimal ds1 = Convert.ToDecimal(s1);
                            decimal fp = ds1 * 100;
                            objrh.Percentage = fp;
                            objrh.Month = Month;
                            objrh.year = Year;
                            DB.RHComplaintLists.AddObject(objrh);
                            DB.SaveChanges();
                        }

                    }
                }

            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
            }
            decimal finalsubcategory = 0;
            for (int i = 0; i < dt6.Rows.Count; i++)
            {
                finalsubcategory = finalsubcategory + Convert.ToInt32(dt6.Rows[i]["subCatCount"]);
            }
            lblsubcategory.Text = finalsubcategory.ToString();


            //decimal finalpersubcategory = 0;
            //for (int i = 0; i < dt6.Rows.Count; i++)
            //{
            //    finalpersubcategory = finalpersubcategory + Convert.ToDecimal(dt6.Rows[i]["Tempdatatable.percentage"]);
            //}
            //lblpersubcategory.Text = finalpersubcategory.ToString();

            //string fs1 = " select TickSubCatID , RHComplaintList.Name  , COUNT(*) as subCatCount , RHComplaintList.percentage  " +
            //             " FROM  CRMMainActivities , RHComplaintList   where CRMMainActivities.TenentID =  " + TID +
            //             " and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "' " +
            //             " and RHComplaintList.Type=3 and RHComplaintList.TenentID = CRMMainActivities.TenentID   and RHComplaintList.ID = CRMMainActivities.TickSubCatID " +
            //             "  and CRMMainActivities.TickSubCatID is not null and CRMMainActivities.UploadDate is not null group by TickSubCatID, RHComplaintList.Name  , RHComplaintList.percentage ";

            string fs1 = "select ID as TickSubCatID, Name as Name  ,Count as subCatCount , percentage as percentage" +
                         " FROM    RHComplaintList   where TenentID =  " + TID + " and Month='" + Month + "' and year='" + Year + "' " +
                         " and Type=3 and  RHComplaintList.ID is not null and Month is not null";

            SqlCommand CMDs2 = new SqlCommand(fs1, con);
            SqlDataAdapter ADBs2 = new SqlDataAdapter(CMDs2);
            DataSet dss2 = new DataSet();
            ADBs2.Fill(dss2);
            DataTable dts2 = dss2.Tables[0];
            ListView7.DataSource = dss2;
            ListView7.DataBind();


            string SQOCommad6 = "select TickPhysicalLocation , REFTABLE.REFNAME1  , COUNT(*) as LocCount  FROM  CRMMainActivities , REFTABLE " +
                                " where CRMMainActivities.TenentID = " + TID + " and REFTABLE.REFTYPE='Ticket' and REFTABLE.REFSUBTYPE='PhysicalLocation' " +
                                " and FORMAT(CRMMainActivities.UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "' and REFTABLE.TenentID = CRMMainActivities.TenentID  " +
                                " and REFTABLE.REFID = CRMMainActivities.TickPhysicalLocation and CRMMainActivities.TickPhysicalLocation is not null and CRMMainActivities.UploadDate is not null group by TickPhysicalLocation, REFTABLE.REFNAME1 ";

            SqlCommand CMD7 = new SqlCommand(SQOCommad6, con);
            SqlDataAdapter ADB7 = new SqlDataAdapter(CMD7);
            DataSet ds7 = new DataSet();
            ADB7.Fill(ds7);
            DataTable dt7 = ds7.Tables[0];

            decimal fl = 0;

            for (int a = 0; a <= dt7.Rows.Count - 1; a++)
            {
                fl = fl + Convert.ToInt32(dt7.Rows[a]["LocCount"]);
            }


            if (dt7.Rows.Count > 0)
            {
                if (ds7.Tables[0] != null && ds7.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dt7.Rows.Count - 1; i++)
                    {
                        int TickPhysicalLocation = Convert.ToInt32(dt7.Rows[i]["TickPhysicalLocation"]);
                        if (DB.RHComplaintLists.Where(p => p.TenentID == TID && p.ID == TickPhysicalLocation && p.Type == 4 && p.Month == Month && p.year == Year).Count() <= 0)
                        {


                            decimal LocCount = Convert.ToInt32(dt7.Rows[i]["LocCount"]);
                            string REFNAME1 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == TickPhysicalLocation && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation").REFNAME1;


                            //Tempdatatable objprofile = new Tempdatatable();
                            //objprofile.TenentID = TID;
                            //objprofile.Type = 4;
                            //objprofile.ID = TickPhysicalLocation;
                            //objprofile.Name = REFNAME1;
                            //objprofile.UserId = UIN;
                            //objprofile.Count = LocCount;
                            //objprofile.TotalCount = Convert.ToInt32(fl);
                            //decimal percent = (LocCount / fs) * 100;
                            //objprofile.percentage = percent;
                            //DB.Tempdatatables.AddObject(objprofile);
                            //DB.SaveChanges();
                            RHComplaintList objrh = new RHComplaintList();
                            objrh.TenentID = TID;
                            objrh.Type = 4;
                            objrh.ID = TickPhysicalLocation;
                            objrh.Name = REFNAME1;
                            objrh.Count =Convert.ToInt32(LocCount);
                            decimal percent1 = (LocCount / finalcount) ;
                            objrh.TotalCount = Convert.ToInt32(finalcount);
                            string s1 = percent1.ToString("N3");
                            decimal ds1 = Convert.ToDecimal(s1);
                            decimal fp = ds1 * 100;
                            objrh.Percentage = fp;
                            objrh.Month = Month;
                            objrh.year = Year;
                            DB.RHComplaintLists.AddObject(objrh);
                            DB.SaveChanges();
                        }

                    }
                }

            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
            }

            decimal finallocation = 0;
            for (int i = 0; i < dt7.Rows.Count; i++)
            {
                finallocation = finallocation + Convert.ToInt32(dt7.Rows[i]["LocCount"]);
            }
            lbllocation.Text = finallocation.ToString();

            //decimal finalperlocation = 0;
            //for (int i = 0; i < dt7.Rows.Count; i++)
            //{
            //    finalperlocation = finalperlocation + Convert.ToDecimal(dt7.Rows[i]["Tempdatatable.percentage"]);
            //}
            //lblperloc.Text = finalperlocation.ToString();

            //string fl1 = " select TickPhysicalLocation , RHComplaintList.Name  , COUNT(*) as LocCount , RHComplaintList.percentage  " +
            //             " FROM  CRMMainActivities , RHComplaintList   where CRMMainActivities.TenentID =  " + TID +
            //             " and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "' " +
            //             " and RHComplaintList.Type=4 and RHComplaintList.TenentID = CRMMainActivities.TenentID   and RHComplaintList.ID = CRMMainActivities.TickPhysicalLocation " +
            //             "  and CRMMainActivities.TickPhysicalLocation is not null and CRMMainActivities.UploadDate is not null group by TickPhysicalLocation, RHComplaintList.Name  , RHComplaintList.percentage ";

            string fl1 = "select ID as TickPhysicalLocation , Name as Name  ,Count as LocCount , percentage as percentage" +
                          " FROM    RHComplaintList   where TenentID =  " + TID + " and Month='" + Month + "' and year='" + Year + "' " +
                          " and Type=4 and  RHComplaintList.ID is not null and Month is not null";

            SqlCommand CMDs3 = new SqlCommand(fl1, con);
            SqlDataAdapter ADBs3 = new SqlDataAdapter(CMDs3);
            DataSet dss3 = new DataSet();
            ADBs3.Fill(dss3);
            DataTable dts3 = dss3.Tables[0];
            ListView5.DataSource = dts3;
            ListView5.DataBind();
        }

        protected void drpchart_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (drpmonthfrom.SelectedValue != null && drpmonthfrom.SelectedValue != "0")
            {
                DateTime monthyear = Convert.ToDateTime(drpmonthfrom.SelectedValue);
                string Month = monthyear.ToString("MMMM");
                string Year = monthyear.ToString("yyyy");

                if (drpchart.SelectedValue == "1")
                {
                    Chart1.Visible = true;
                    string query = string.Format("Select  Percentage,left(Name,15)  from RHComplaintList where TenentID= " + TID + " and type=1 and Month='" + Month + "' and year='" + Year + "' group by Percentage,Name");
                    DataTable dtt = GetData(query);
                    string[] x = new string[dtt.Rows.Count];
                    int[] y = new int[dtt.Rows.Count];
                    for (int i = 0; i < dtt.Rows.Count; i++)
                    {
                        x[i] = dtt.Rows[i][1].ToString();
                        y[i] = Convert.ToInt32(dtt.Rows[i][0]);
                    }
                    Chart1.Series[0].Points.DataBindXY(x, y);
                    Chart1.Series[0].ChartType = SeriesChartType.Pie;
                    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    Chart1.Legends[0].Enabled = true;
                }

                if (drpchart.SelectedValue == "2")
                {
                    Chart1.Visible = true;
                    string query = string.Format("Select  Percentage,left(Name,15)  from RHComplaintList where TenentID= " + TID + " and type=2 and Month='" + Month + "' and year='" + Year + "' group by Percentage,Name ");
                    DataTable dtt = GetData(query);
                    string[] x = new string[dtt.Rows.Count];
                    int[] y = new int[dtt.Rows.Count];
                    for (int i = 0; i < dtt.Rows.Count; i++)
                    {
                        x[i] = dtt.Rows[i][1].ToString();
                        y[i] = Convert.ToInt32(dtt.Rows[i][0]);
                    }
                    Chart1.Series[0].Points.DataBindXY(x, y);
                    Chart1.Series[0].ChartType = SeriesChartType.Pie;
                    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    Chart1.Legends[0].Enabled = true;
                    // Set pie labels to be outside the pie chart
                    this.Chart1.Series[0]["PieLabelStyle"] = "Disabled";

                    // Add a legend to the chart and dock it to the bottom-center
                    this.Chart1.Legends.Add("Legend1");
                    this.Chart1.Legends[0].Enabled = true;
                    this.Chart1.Legends[0].Docking = Docking.Bottom;
                    this.Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                    // Show labels in the legend in the format "Name (### %)"
                    this.Chart1.Series[0].LegendText = "#VALX (#PERCENT)";

                    // By sorting the data points, they show up in proper ascending order in the legend
                    this.Chart1.DataManipulator.Sort(PointSortOrder.Descending, Chart1.Series[0]);
                }
                if (drpchart.SelectedValue == "3")
                {
                    Chart1.Visible = true;
                    string query = string.Format("Select  Percentage,left(Name,15)  from RHComplaintList where TenentID= " + TID + " and type=3 and Month='" + Month + "' and year='" + Year + "' group by Percentage,Name");
                    DataTable dtt = GetData(query);
                    string[] x = new string[dtt.Rows.Count];
                    int[] y = new int[dtt.Rows.Count];
                    for (int i = 0; i < dtt.Rows.Count; i++)
                    {
                        x[i] = dtt.Rows[i][1].ToString();
                        y[i] = Convert.ToInt32(dtt.Rows[i][0]);
                    }
                    Chart1.Series[0].Points.DataBindXY(x, y);
                    Chart1.Series[0].ChartType = SeriesChartType.Pie;
                    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    Chart1.Legends[0].Enabled = true;
                    // Set pie labels to be outside the pie chart
                    this.Chart1.Series[0]["PieLabelStyle"] = "Disabled";

                    // Add a legend to the chart and dock it to the bottom-center
                    this.Chart1.Legends.Add("Legend1");
                    this.Chart1.Legends[0].Enabled = true;
                    this.Chart1.Legends[0].Docking = Docking.Bottom;
                    this.Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                    // Show labels in the legend in the format "Name (### %)"
                    this.Chart1.Series[0].LegendText = "#VALX (#PERCENT)";

                    // By sorting the data points, they show up in proper ascending order in the legend
                    this.Chart1.DataManipulator.Sort(PointSortOrder.Descending, Chart1.Series[0]);

                }
                if (drpchart.SelectedValue == "4")
                {
                    Chart1.Visible = true;
                    string query = string.Format("Select  Percentage,left(Name,15)  from RHComplaintList where TenentID= " + TID + " and type=4 and Month='" + Month + "' and year='" + Year + "' group by Percentage,Name");
                    DataTable dtt = GetData(query);
                    string[] x = new string[dtt.Rows.Count];
                    int[] y = new int[dtt.Rows.Count];
                    for (int i = 0; i < dtt.Rows.Count; i++)
                    {
                        x[i] = dtt.Rows[i][1].ToString();
                        y[i] = Convert.ToInt32(dtt.Rows[i][0]);
                    }
                    Chart1.Series[0].Points.DataBindXY(x, y);
                    Chart1.Series[0].ChartType = SeriesChartType.Pie;
                    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    Chart1.Legends[0].Enabled = true;
                    // Set pie labels to be outside the pie chart
                    this.Chart1.Series[0]["PieLabelStyle"] = "Disabled";

                    // Add a legend to the chart and dock it to the bottom-center
                    this.Chart1.Legends.Add("Legend1");
                    this.Chart1.Legends[0].Enabled = true;
                    this.Chart1.Legends[0].Docking = Docking.Bottom;
                    this.Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                    // Show labels in the legend in the format "Name (### %)"
                    this.Chart1.Series[0].LegendText = "#VALX (#PERCENT)";

                    // By sorting the data points, they show up in proper ascending order in the legend
                    this.Chart1.DataManipulator.Sort(PointSortOrder.Descending, Chart1.Series[0]);
                }
            }
            else
            {
                if (drpchart.SelectedValue == "1")
                {
                    Chart1.Visible = true;
                    string query = string.Format("Select  Percentage,left(Name,15)  from Tempdatatable where TenentID= " + TID + " and type=1 group by Percentage,Name");
                    DataTable dtt = GetData(query);
                    string[] x = new string[dtt.Rows.Count];
                    int[] y = new int[dtt.Rows.Count];
                    for (int i = 0; i < dtt.Rows.Count; i++)
                    {
                        x[i] = dtt.Rows[i][1].ToString();
                        y[i] = Convert.ToInt32(dtt.Rows[i][0]);
                    }
                    Chart1.Series[0].Points.DataBindXY(x, y);
                    Chart1.Series[0].ChartType = SeriesChartType.Pie;
                    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    Chart1.Legends[0].Enabled = true;
                    // Set pie labels to be outside the pie chart
                    this.Chart1.Series[0]["PieLabelStyle"] = "Disabled";

                    // Add a legend to the chart and dock it to the bottom-center
                    this.Chart1.Legends.Add("Legend1");
                    this.Chart1.Legends[0].Enabled = true;
                    this.Chart1.Legends[0].Docking = Docking.Bottom;
                    this.Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                    // Show labels in the legend in the format "Name (### %)"
                    this.Chart1.Series[0].LegendText = "#VALX (#PERCENT)";

                    // By sorting the data points, they show up in proper ascending order in the legend
                    this.Chart1.DataManipulator.Sort(PointSortOrder.Descending, Chart1.Series[0]);


                }

                if (drpchart.SelectedValue == "2")
                {
                    Chart1.Visible = true;
                    string query = string.Format("Select  Percentage,left(Name,15)  from Tempdatatable where TenentID= " + TID + " and type=2 group by Percentage,Name");
                    DataTable dtt = GetData(query);
                    string[] x = new string[dtt.Rows.Count];
                    int[] y = new int[dtt.Rows.Count];
                    for (int i = 0; i < dtt.Rows.Count; i++)
                    {
                        x[i] = dtt.Rows[i][1].ToString();
                        y[i] = Convert.ToInt32(dtt.Rows[i][0]);
                    }
                    Chart1.Series[0].Points.DataBindXY(x, y);
                    Chart1.Series[0].ChartType = SeriesChartType.Pie;
                    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    Chart1.Legends[0].Enabled = true;
                    // Set pie labels to be outside the pie chart
                    this.Chart1.Series[0]["PieLabelStyle"] = "Disabled";

                    // Add a legend to the chart and dock it to the bottom-center
                    this.Chart1.Legends.Add("Legend1");
                    this.Chart1.Legends[0].Enabled = true;
                    this.Chart1.Legends[0].Docking = Docking.Bottom;
                    this.Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                    // Show labels in the legend in the format "Name (### %)"
                    this.Chart1.Series[0].LegendText = "#VALX (#PERCENT)";

                    // By sorting the data points, they show up in proper ascending order in the legend
                    this.Chart1.DataManipulator.Sort(PointSortOrder.Descending, Chart1.Series[0]);
                }
                if (drpchart.SelectedValue == "3")
                {
                    Chart1.Visible = true;
                    string query = string.Format("Select  Percentage,left(Name,15)  from Tempdatatable where TenentID= " + TID + " and type=3 group by Percentage,Name");
                    DataTable dtt = GetData(query);
                    string[] x = new string[dtt.Rows.Count];
                    int[] y = new int[dtt.Rows.Count];
                    for (int i = 0; i < dtt.Rows.Count; i++)
                    {
                        x[i] = dtt.Rows[i][1].ToString();
                        y[i] = Convert.ToInt32(dtt.Rows[i][0]);
                    }
                    Chart1.Series[0].Points.DataBindXY(x, y);
                    Chart1.Series[0].ChartType = SeriesChartType.Pie;
                    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    Chart1.Legends[0].Enabled = true;
                    // Set pie labels to be outside the pie chart
                    this.Chart1.Series[0]["PieLabelStyle"] = "Disabled";

                    // Add a legend to the chart and dock it to the bottom-center
                    this.Chart1.Legends.Add("Legend1");
                    this.Chart1.Legends[0].Enabled = true;
                    this.Chart1.Legends[0].Docking = Docking.Bottom;
                    this.Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                    // Show labels in the legend in the format "Name (### %)"
                    this.Chart1.Series[0].LegendText = "#VALX (#PERCENT)";

                    // By sorting the data points, they show up in proper ascending order in the legend
                    this.Chart1.DataManipulator.Sort(PointSortOrder.Descending, Chart1.Series[0]);

                }
                if (drpchart.SelectedValue == "4")
                {
                    Chart1.Visible = true;
                    string query = string.Format("Select  Percentage,left(Name,15)  from Tempdatatable where TenentID= " + TID + " and type=4 group by Percentage,Name");
                    DataTable dtt = GetData(query);
                    string[] x = new string[dtt.Rows.Count];
                    int[] y = new int[dtt.Rows.Count];
                    for (int i = 0; i < dtt.Rows.Count; i++)
                    {
                        x[i] = dtt.Rows[i][1].ToString();
                        y[i] = Convert.ToInt32(dtt.Rows[i][0]);
                    }
                    Chart1.Series[0].Points.DataBindXY(x, y);
                    Chart1.Series[0].ChartType = SeriesChartType.Pie;
                    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                    Chart1.Legends[0].Enabled = true;
                    // Set pie labels to be outside the pie chart
                    this.Chart1.Series[0]["PieLabelStyle"] = "Disabled";

                    // Add a legend to the chart and dock it to the bottom-center
                    this.Chart1.Legends.Add("Legend1");
                    this.Chart1.Legends[0].Enabled = true;
                    this.Chart1.Legends[0].Docking = Docking.Bottom;
                    this.Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                    // Show labels in the legend in the format "Name (### %)"
                    this.Chart1.Series[0].LegendText = "#VALX (#PERCENT)";

                    // By sorting the data points, they show up in proper ascending order in the legend
                    this.Chart1.DataManipulator.Sort(PointSortOrder.Descending, Chart1.Series[0]);
                }
            }



        }

        protected void ListView3_ItemCommand2(object sender, ListViewCommandEventArgs e)
        {
            if (drpchart.SelectedValue == "1")
            {
                Chart1.Visible = true;
                string query = string.Format("Select  Percentage,left(Name,15)  from RHComplaintList where TenentID= " + TID + " and type=1 group by Percentage,Name");
                DataTable dtt = GetData(query);
                string[] x = new string[dtt.Rows.Count];
                int[] y = new int[dtt.Rows.Count];
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    x[i] = dtt.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(dtt.Rows[i][0]);
                }
                Chart1.Series[0].Points.DataBindXY(x, y);
                Chart1.Series[0].ChartType = SeriesChartType.Pie;
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart1.Legends[0].Enabled = true;
            }
        }

        protected void btnmail_Click(object sender, EventArgs e)
        {
            if (drpmonthfrom.SelectedValue != null && drpmonthfrom.SelectedValue != "0")
            {
                txtstartdate.Text = "";
                txtenddate.Text = "";
                TID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);


                DateTime monthyear = Convert.ToDateTime(drpmonthfrom.SelectedValue);
                string Month = monthyear.ToString("MMMM");
                string Year = monthyear.ToString("yyyy");

                string SQOCommad = " select TickDepartmentID , DeptITSuper.DeptName  , COUNT(*) as DeptCount  FROM  CRMMainActivities , DeptITSuper " +

                                   "  where CRMMainActivities.TenentID = " + TID + "  and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "'  and DeptITSuper.TenentID = CRMMainActivities.TenentID   and DeptITSuper.DeptID = CRMMainActivities.TickDepartmentID and CRMMainActivities.TickDepartmentID is not null and CRMMainActivities.UploadDate is not null" +

                                   " group by TickDepartmentID, DeptITSuper.DeptName";

                List<Tempdatatable> Listtemp = DB.Tempdatatables.Where(p => p.TenentID == TID).ToList();

                List<Tempdatatable> list1 = new List<Tempdatatable>();
                SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
                SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
                DataSet ds = new DataSet();
                ADB.Fill(ds);
                DataTable dt = ds.Tables[0];

                int finalcount = 0;

                for (int a = 0; a <= dt.Rows.Count - 1; a++)
                {
                    finalcount = finalcount + Convert.ToInt32(dt.Rows[a]["DeptCount"]);
                }


                if (dt.Rows.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            decimal TickDepartmentID = Convert.ToInt32(dt.Rows[i]["TickDepartmentID"]);
                            if (DB.RHComplaintLists.Where(p => p.TenentID == TID && p.ID == TickDepartmentID && p.Type == 1 && p.Month == Month && p.year == Year).Count() <= 0)
                            {
                                int DeptCount = Convert.ToInt32(dt.Rows[i]["DeptCount"]);
                                string deptName = DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == TickDepartmentID).DeptName;
                                RHComplaintList objrh = new RHComplaintList();
                                objrh.TenentID = TID;
                                objrh.Type = 1;
                                objrh.ID =Convert.ToInt32(TickDepartmentID);
                                objrh.Name = deptName;
                                objrh.Count = DeptCount;
                                decimal percent1 = (DeptCount / finalcount) * 100;
                                objrh.TotalCount = Convert.ToInt32(finalcount);
                                objrh.Percentage = percent1;
                                objrh.Month = Month;
                                objrh.year = Year;
                                DB.RHComplaintLists.AddObject(objrh);
                                DB.SaveChanges();
                            }
                        }
                    }
                }


                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
                }

                int finalTotal = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    finalTotal = finalTotal + Convert.ToInt32(dt.Rows[i]["DeptCount"]);
                }
                lblFinalTotal.Text = finalTotal.ToString();


                string com = "select ID as TickDepartmentID, Name as Name  ,Count as DeptCount , percentage as percentage" +
                             " FROM    RHComplaintList   where TenentID =  " + TID + "  and Month='" + Month + "' and year='" + Year + "' " +
                             " and Type=1 and  RHComplaintList.ID is not null and Month is not null";

                SqlCommand CMDs = new SqlCommand(com, con);
                SqlDataAdapter ADBs = new SqlDataAdapter(CMDs);
                DataSet dss = new DataSet();
                ADBs.Fill(dss);
                DataTable dts = dss.Tables[0];


                bool Flag = false;
                int dID = 0;


                string Tablecontant = "<h2>Department</h2><table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> Dept id </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Deptartment name </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Count </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> % </th></tr></thead><tbody>";
                string Tablecontant1 = "<h2>Location</h2><table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> LOC Id  </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Location Name  </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Count </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> % </th></tr></thead><tbody>";
                string Tablecontant2 = "<h2>Category</h2><table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> Cat ID </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Category Name </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'>  Count </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> % </th></tr></thead><tbody>";
                string Tablecontant3 = "<h2>Sub Category</h2><table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> SubCat ID  </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> SubCategory Name </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Count </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> % </th></tr></thead><tbody>";


                //string email = "";
                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    string complainno = dts.Rows[i]["Name"].ToString();
                    int Master = Convert.ToInt32(dts.Rows[i]["TickDepartmentID"]);
                    int department = Convert.ToInt32(dts.Rows[i]["DeptCount"]);
                    int action = Convert.ToInt32(dts.Rows[i]["percentage"]);


                    Flag = true;
                    Tablecontant += "<tr><td>" + Master + "</td><td>" + complainno + "</td> <td>" + department + " </td> <td>" + action + " </td> </tr>";

                }

                Chart1.Visible = true;
                string query = string.Format("Select  Percentage,left(Name,15)  from RHComplaintList where TenentID= " + TID + " and type=1 and Month='" + Month + "' and year='" + Year + "' group by Percentage,Name");
                DataTable dtt = GetData(query);
                string[] x = new string[dtt.Rows.Count];
                int[] y = new int[dtt.Rows.Count];
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    x[i] = dtt.Rows[i][1].ToString();
                    y[i] = Convert.ToInt32(dtt.Rows[i][0]);
                }
                Chart1.Series[0].Points.DataBindXY(x, y);
                Chart1.Series[0].ChartType = SeriesChartType.Pie;
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart1.Legends[0].Enabled = true;
                // Set pie labels to be outside the pie chart
                this.Chart1.Series[0]["PieLabelStyle"] = "Disabled";

                // Add a legend to the chart and dock it to the bottom-center
                this.Chart1.Legends.Add("Legend1");
                this.Chart1.Legends[0].Enabled = true;
                this.Chart1.Legends[0].Docking = Docking.Bottom;
                this.Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                // Show labels in the legend in the format "Name (### %)"
                this.Chart1.Series[0].LegendText = "#VALX (#PERCENT)";

                // By sorting the data points, they show up in proper ascending order in the legend
                this.Chart1.DataManipulator.Sort(PointSortOrder.Descending, Chart1.Series[0]);


                string SQOCommad4 = " select TickCatID , ICCATEGORY.CATNAME  , COUNT(*) as CatCount  FROM  CRMMainActivities , ICCATEGORY " +
                                    " where CRMMainActivities.TenentID =  " + TID + " and CATTYPE='HelpDesk' " +
                                    " and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "'  and ICCATEGORY.TenentID = CRMMainActivities.TenentID " +
                                    " and ICCATEGORY.CATID = CRMMainActivities.TickCatID and CRMMainActivities.TickCatID is not null  and CRMMainActivities.UploadDate is not null group by TickCatID, ICCATEGORY.CATNAME ";

                SqlCommand CMD5 = new SqlCommand(SQOCommad4, con);
                SqlDataAdapter ADB5 = new SqlDataAdapter(CMD5);
                DataSet ds5 = new DataSet();
                ADB5.Fill(ds5);
                DataTable dt5 = ds5.Tables[0];

                int fc = 0;

                for (int a = 0; a <= dt5.Rows.Count - 1; a++)
                {
                    fc = fc + Convert.ToInt32(dt5.Rows[a]["CatCount"]);
                }


                if (dt5.Rows.Count > 0)
                {
                    if (ds5.Tables[0] != null && ds5.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt5.Rows.Count - 1; i++)
                        {
                            int TickCatID = Convert.ToInt32(dt5.Rows[i]["TickCatID"]);
                            if (DB.RHComplaintLists.Where(p => p.TenentID == TID && p.ID == TickCatID && p.Type == 2 && p.Month == Month && p.year == Year).Count() <= 0)
                            {
                                decimal CatCount = Convert.ToInt32(dt5.Rows[i]["CatCount"]);
                                string CATNAME = DB.ICCATEGORies.Single(p => p.TenentID == TID && p.CATID == TickCatID).CATNAME;


                                RHComplaintList objrh = new RHComplaintList();
                                objrh.TenentID = TID;
                                objrh.Type = 2;
                                objrh.ID = TickCatID;
                                objrh.Name = CATNAME;
                                objrh.Count =Convert.ToInt32( CatCount);
                                decimal percent1 = (CatCount / finalcount) * 100;
                                objrh.TotalCount = Convert.ToInt32(finalcount);
                                objrh.Percentage = percent1;
                                objrh.Month = Month;
                                objrh.year = Year;
                                DB.RHComplaintLists.AddObject(objrh);
                                DB.SaveChanges();
                            }

                        }
                    }

                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
                }
                int finalcategory = 0;
                for (int i = 0; i < dt5.Rows.Count; i++)
                {
                    finalcategory = finalcategory + Convert.ToInt32(dt5.Rows[i]["CatCount"]);
                }
                lblcategory.Text = finalcategory.ToString();


                string fc1 = "select ID as TickCatID, Name as Name ,Count as CatCount ,percentage as percentage" +
                             " FROM    RHComplaintList   where TenentID =  " + TID + " and Month='" + Month + "' and year='" + Year + "' " +
                             " and Type=2 and  RHComplaintList.ID is not null and Month is not null";
                SqlCommand CMDs1 = new SqlCommand(fc1, con);
                SqlDataAdapter ADBs1 = new SqlDataAdapter(CMDs1);
                DataSet dss1 = new DataSet();
                ADBs1.Fill(dss1);
                DataTable dts1 = dss1.Tables[0];
                for (int i = 0; i < dts1.Rows.Count; i++)
                {
                    string complainno = dts1.Rows[i]["Name"].ToString();
                    int Master = Convert.ToInt32(dts1.Rows[i]["TickCatID"]);
                    int department = Convert.ToInt32(dts1.Rows[i]["CatCount"]);
                    int action = Convert.ToInt32(dts1.Rows[i]["percentage"]);



                    Flag = true;
                    Tablecontant2 += "<tr><td>" + Master + "</td><td>" + complainno + "</td> <td>" + department + " </td> <td>" + action + " </td> </tr>";
                }



                string SQOCommad5 = " select TickSubCatID , ICSUBCATEGORY.SUBCATNAME  , COUNT(*) as subCatCount  FROM  CRMMainActivities , ICSUBCATEGORY  " +
                                     " where CRMMainActivities.TenentID = " + TID + " and ICSUBCATEGORY.SUBCATTYPE='HelpDesk' " +
                                     " and FORMAT(UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "' and ICSUBCATEGORY.TenentID = CRMMainActivities.TenentID " +
                                     " and ICSUBCATEGORY.SUBCATID = CRMMainActivities.TickSubCatID and CRMMainActivities.TickSubCatID is not null and CRMMainActivities.UploadDate is not null group by TickSubCatID, ICSUBCATEGORY.SUBCATNAME ";

                SqlCommand CMD6 = new SqlCommand(SQOCommad5, con);
                SqlDataAdapter ADB6 = new SqlDataAdapter(CMD6);
                DataSet ds6 = new DataSet();
                ADB6.Fill(ds6);
                DataTable dt6 = ds6.Tables[0];

                decimal fs = 0;

                for (int a = 0; a <= dt6.Rows.Count - 1; a++)
                {
                    fs = fs + Convert.ToInt32(dt6.Rows[a]["subCatCount"]);
                }


                if (dt6.Rows.Count > 0)
                {
                    if (ds6.Tables[0] != null && ds6.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt6.Rows.Count - 1; i++)
                        {
                            int TickSubCatID = Convert.ToInt32(dt6.Rows[i]["TickSubCatID"]);
                            if (DB.RHComplaintLists.Where(p => p.TenentID == TID && p.ID == TickSubCatID && p.Type == 3 && p.Month == Month && p.year == Year).Count() <= 0)
                            {

                                decimal subCatCount = Convert.ToInt32(dt6.Rows[i]["subCatCount"]);
                                string SUBCATNAME = DB.ICSUBCATEGORies.Single(p => p.TenentID == TID && p.SUBCATID == TickSubCatID).SUBCATNAME;

                                RHComplaintList objrh = new RHComplaintList();
                                objrh.TenentID = TID;
                                objrh.Type = 3;
                                objrh.ID = TickSubCatID;
                                objrh.Name = SUBCATNAME;
                                objrh.Count =Convert.ToInt32( subCatCount);
                                decimal percent1 = (subCatCount / finalcount) * 100;
                                objrh.TotalCount = Convert.ToInt32(finalcount);
                                objrh.Percentage = percent1;
                                objrh.Month = Month;
                                objrh.year = Year;
                                DB.RHComplaintLists.AddObject(objrh);
                                DB.SaveChanges();
                            }

                        }
                    }

                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
                }
                int finalsubcategory = 0;
                for (int i = 0; i < dt6.Rows.Count; i++)
                {
                    finalsubcategory = finalsubcategory + Convert.ToInt32(dt6.Rows[i]["subCatCount"]);
                }
                lblsubcategory.Text = finalsubcategory.ToString();

                string fs1 = "select ID as TickSubCatID, Name as Name  ,Count as subCatCount , percentage as percentage" +
                             " FROM    RHComplaintList   where TenentID =  " + TID + " and Month='" + Month + "' and year='" + Year + "' " +
                             " and Type=3 and  RHComplaintList.ID is not null and Month is not null";

                SqlCommand CMDs2 = new SqlCommand(fs1, con);
                SqlDataAdapter ADBs2 = new SqlDataAdapter(CMDs2);
                DataSet dss2 = new DataSet();
                ADBs2.Fill(dss2);
                DataTable dts2 = dss2.Tables[0];
                for (int i = 0; i < dts2.Rows.Count; i++)
                {

                    string complainno = dts2.Rows[i]["Name"].ToString();
                    int Master = Convert.ToInt32(dts2.Rows[i]["TickSubCatID"]);
                    int department = Convert.ToInt32(dts2.Rows[i]["subCatCount"]);
                    int action = Convert.ToInt32(dts2.Rows[i]["percentage"]);



                    Flag = true;
                    Tablecontant3 += "<tr><td>" + Master + "</td><td>" + complainno + "</td> <td>" + department + " </td> <td>" + action + " </td> </tr>";

                }


                string SQOCommad6 = "select TickPhysicalLocation , REFTABLE.REFNAME1  , COUNT(*) as LocCount  FROM  CRMMainActivities , REFTABLE " +
                                    " where CRMMainActivities.TenentID = " + TID + " and REFTABLE.REFTYPE='Ticket' and REFTABLE.REFSUBTYPE='PhysicalLocation' " +
                                    " and FORMAT(CRMMainActivities.UploadDate, 'MMMM,yyyy') =  '" + drpmonthfrom.SelectedValue + "' and REFTABLE.TenentID = CRMMainActivities.TenentID  " +
                                    " and REFTABLE.REFID = CRMMainActivities.TickPhysicalLocation and CRMMainActivities.TickPhysicalLocation is not null and CRMMainActivities.UploadDate is not null group by TickPhysicalLocation, REFTABLE.REFNAME1 ";

                SqlCommand CMD7 = new SqlCommand(SQOCommad6, con);
                SqlDataAdapter ADB7 = new SqlDataAdapter(CMD7);
                DataSet ds7 = new DataSet();
                ADB7.Fill(ds7);
                DataTable dt7 = ds7.Tables[0];

                decimal fl = 0;

                for (int a = 0; a <= dt7.Rows.Count - 1; a++)
                {
                    fl = fl + Convert.ToInt32(dt7.Rows[a]["LocCount"]);
                }


                if (dt7.Rows.Count > 0)
                {
                    if (ds7.Tables[0] != null && ds7.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt7.Rows.Count - 1; i++)
                        {
                            int TickPhysicalLocation = Convert.ToInt32(dt7.Rows[i]["TickPhysicalLocation"]);
                            if (DB.RHComplaintLists.Where(p => p.TenentID == TID && p.ID == TickPhysicalLocation && p.Type == 4 && p.Month == Month && p.year == Year).Count() <= 0)
                            {


                                decimal LocCount = Convert.ToInt32(dt7.Rows[i]["LocCount"]);
                                string REFNAME1 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == TickPhysicalLocation && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation").REFNAME1;
                                RHComplaintList objrh = new RHComplaintList();
                                objrh.TenentID = TID;
                                objrh.Type = 4;
                                objrh.ID = TickPhysicalLocation;
                                objrh.Name = REFNAME1;
                                objrh.Count =Convert.ToInt32( LocCount);
                                decimal percent1 = (LocCount / finalcount) * 100;
                                objrh.TotalCount = Convert.ToInt32(finalcount);
                                objrh.Percentage = percent1;
                                objrh.Month = Month;
                                objrh.year = Year;
                                DB.RHComplaintLists.AddObject(objrh);
                                DB.SaveChanges();
                            }

                        }
                    }

                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
                }

                int finallocation = 0;
                for (int i = 0; i < dt7.Rows.Count; i++)
                {
                    finallocation = finallocation + Convert.ToInt32(dt7.Rows[i]["LocCount"]);
                }
                lbllocation.Text = finallocation.ToString();


                string fl1 = "select ID as TickPhysicalLocation , Name as Name  ,Count as LocCount , percentage as percentage" +
                              " FROM    RHComplaintList   where TenentID =  " + TID + " and Month='" + Month + "' and year='" + Year + "' " +
                              " and Type=4 and  RHComplaintList.ID is not null and Month is not null";

                SqlCommand CMDs3 = new SqlCommand(fl1, con);
                SqlDataAdapter ADBs3 = new SqlDataAdapter(CMDs3);
                DataSet dss3 = new DataSet();
                ADBs3.Fill(dss3);
                DataTable dts3 = dss3.Tables[0];
                for (int i = 0; i < dts3.Rows.Count; i++)
                {

                    string complainno = dts3.Rows[i]["Name"].ToString();
                    int Master = Convert.ToInt32(dts3.Rows[i]["TickPhysicalLocation"]);
                    int department = Convert.ToInt32(dts3.Rows[i]["LocCount"]);
                    int action = Convert.ToInt32(dts3.Rows[i]["percentage"]);



                    Flag = true;
                    Tablecontant1 += "<tr><td>" + Master + "</td><td>" + complainno + "</td> <td>" + department + " </td> <td>" + action + " </td> </tr>";

                }


                if (Flag)
                {
                    Tablecontant += " </tbody> </table>";
                    Tablecontant1 += " </tbody> </table>";
                    Tablecontant2 += " </tbody> </table>";
                    Tablecontant3 += " </tbody> </table>";
                    string Ourcontant = " <span style = \"font-family:Arial;font-size:10pt\">  Month='" + Month + "' and year='" + Year + "' <b></b>,<br /><br />This email is reference to the Complain recorded on '" + Month + "' and year='" + Year + "' ,  <br /><br />" + Tablecontant + "<br /><br />" + Tablecontant1 + "<br /><br />" + Tablecontant2 + "<br /><br />" + Tablecontant3 + "<br /><br /><br /></span>";
                    string Tocontant = " <span style = \"font-family:Arial;font-size:10pt\">  Month='" + Month + "' and year='" + Year + "' <b></b>,<br /><br />This email is reference to the Complain recorded on '" + Month + "' and year='" + Year + "' ,  <br /><br />" + Tablecontant + "<br /><br />" + Tablecontant1 + "<br /><br />" + Tablecontant2 + "<br /><br />" + Tablecontant3 + "<br /><br /><br /></span>";

                    sendEmail(Tocontant, txtpmail.Text);
                    sendEmail(Ourcontant, "dangijalpa@gmail.com");
                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Successfully mail Send.');", true);
                   
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('No data found');", true);
                    return;
                }
            }
            else
            {
                TID = 10;
                con.Open();
                SqlCommand command;
                SqlDataAdapter Ang = new SqlDataAdapter();
                string sql5 = "Delete from Tempdatatable where tenentID=" + TID;
                command = new SqlCommand(sql5, con);
                command.ExecuteNonQuery();
                command.Dispose();
                DateTime EDT = DateTime.Now;
                DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
                DateTime Enddate = Convert.ToDateTime(txtenddate.Text);

                string stdate = startdate.ToString("yyyy-MM-dd");
                string etdate = Enddate.ToString("yyyy-MM-dd");


                string SQOCommad = " select TickDepartmentID , DeptITSuper.DeptName  , COUNT(*) as DeptCount  FROM  CRMMainActivities , DeptITSuper " +

                                   "  where CRMMainActivities.TenentID = " + TID + " and UploadDate BETWEEN '" + stdate + "' AND    '" + etdate + "' and DeptITSuper.TenentID = CRMMainActivities.TenentID   and DeptITSuper.DeptID = CRMMainActivities.TickDepartmentID and CRMMainActivities.TickDepartmentID is not null and CRMMainActivities.UploadDate is not null" +

                                   " group by TickDepartmentID, DeptITSuper.DeptName";

                List<Tempdatatable> Listtemp = DB.Tempdatatables.Where(p => p.TenentID == TID).ToList();

                List<Tempdatatable> list1 = new List<Tempdatatable>();
                SqlCommand CMD1 = new SqlCommand(SQOCommad, con);
                SqlDataAdapter ADB = new SqlDataAdapter(CMD1);
                DataSet ds = new DataSet();
                ADB.Fill(ds);
                DataTable dt = ds.Tables[0];

                decimal finalcount = 0;

                for (int a = 0; a <= dt.Rows.Count - 1; a++)
                {
                    finalcount = finalcount + Convert.ToInt32(dt.Rows[a]["DeptCount"]);
                }


                if (dt.Rows.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {

                            int TickDepartmentID = Convert.ToInt32(dt.Rows[i]["TickDepartmentID"]);
                            int DeptCount = Convert.ToInt32(dt.Rows[i]["DeptCount"]);
                            string deptName = DB.DeptITSupers.Single(p => p.TenentID == TID && p.DeptID == TickDepartmentID).DeptName;
                            Tempdatatable objprofile = new Tempdatatable();
                            objprofile.TenentID = TID;
                            objprofile.Type = 1;
                            objprofile.ID = TickDepartmentID;
                            objprofile.Name = deptName;
                            objprofile.UserId = 11525;
                            objprofile.Count = Convert.ToInt32(DeptCount);
                            objprofile.TotalCount = Convert.ToInt32(finalcount);
                            decimal percent = (DeptCount / finalcount) * 100;
                            objprofile.percentage = percent;
                            DB.Tempdatatables.AddObject(objprofile);
                            DB.SaveChanges();
                        }
                    }
                }


                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='Summerised.aspx';", true);
                }




                string com = " select TickDepartmentID , Tempdatatable.Name  , COUNT(*) as DeptCount , Tempdatatable.percentage  " +
                             " FROM  CRMMainActivities , Tempdatatable   where CRMMainActivities.TenentID =  " + TID +
                            " and UploadDate BETWEEN '" + stdate + "' AND    '" + etdate + "' " +
                             " and Tempdatatable.Type=1 and Tempdatatable.TenentID = CRMMainActivities.TenentID   and Tempdatatable.ID = CRMMainActivities.TickDepartmentID " +
                             "  and CRMMainActivities.TickDepartmentID is not null and CRMMainActivities.UploadDate is not null group by TickDepartmentID, Tempdatatable.Name  , Tempdatatable.percentage ";

                SqlCommand CMDs = new SqlCommand(com, con);
                SqlDataAdapter ADBs = new SqlDataAdapter(CMDs);
                DataSet dss = new DataSet();
                ADBs.Fill(dss);
                DataTable dts = dss.Tables[0];



                bool Flag = false;
                int dID = 0;


                string Tablecontant = "<h2>Department</h2><table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> Dept id </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Deptartment name </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Count </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> % </th></tr></thead><tbody>";
                string Tablecontant1 = "<h2>Location</h2><table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> LOC Id  </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Location Name  </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Count </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> % </th></tr></thead><tbody>";
                string Tablecontant2 = "<h2>Category</h2><table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> Cat ID </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Category Name </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'>  Count </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> % </th></tr></thead><tbody>";
                string Tablecontant3 = "<h2>Sub Category</h2><table border='1' class='table table-bordered table-hover' style='with:100%'><thead><tr> <th style='background-color:skyblue;font-size:large;font-weight:bold;'> SubCat ID  </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> SubCategory Name </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> Count </th><th style='background-color:skyblue;font-size:large;font-weight:bold;'> % </th></tr></thead><tbody>";


                //string email = "";
                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    string complainno = dts.Rows[i]["Name"].ToString();
                    int Master = Convert.ToInt32(dts.Rows[i]["TickDepartmentID"]);
                    int department = Convert.ToInt32(dts.Rows[i]["DeptCount"]);
                    int action = Convert.ToInt32(dts.Rows[i]["percentage"]);


                    Flag = true;
                    Tablecontant += "<tr><td>" + Master + "</td><td>" + complainno + "</td> <td>" + department + " </td> <td>" + action + " </td> </tr>";

                }


                Chart1.Visible = true;
                string query = string.Format(" Select  Percentage,left(Name,15)  from Tempdatatable where TenentID= " + TID + " and type=1 group by Percentage,Name");
                DataTable dtt = GetData(query);
                string[] x = new string[dtt.Rows.Count];
                int[] y = new int[dtt.Rows.Count];
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    x[i] = dtt.Rows[i][1].ToString();
                    y[i] = Convert.ToInt32(dtt.Rows[i][0]);
                }
                Chart1.Series[0].Points.DataBindXY(x, y);
                Chart1.Series[0].ChartType = SeriesChartType.Pie;
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart1.Legends[0].Enabled = true;
                // Set pie labels to be outside the pie chart
                this.Chart1.Series[0]["PieLabelStyle"] = "Disabled";

                // Add a legend to the chart and dock it to the bottom-center
                this.Chart1.Legends.Add("Legend1");
                this.Chart1.Legends[0].Enabled = true;
                this.Chart1.Legends[0].Docking = Docking.Bottom;
                this.Chart1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                // Show labels in the legend in the format "Name (### %)"
                this.Chart1.Series[0].LegendText = "#VALX (#PERCENT)";

                // By sorting the data points, they show up in proper ascending order in the legend
                this.Chart1.DataManipulator.Sort(PointSortOrder.Descending, Chart1.Series[0]);


                string SQOCommad6 = "select TickPhysicalLocation , REFTABLE.REFNAME1  , COUNT(*) as LocCount  FROM  CRMMainActivities , REFTABLE " +
                                    " where CRMMainActivities.TenentID = " + TID + " and REFTABLE.REFTYPE='Ticket' and REFTABLE.REFSUBTYPE='PhysicalLocation' " +
                                    " and CRMMainActivities.UploadDate BETWEEN '" + stdate + "' AND    '" + etdate + "' and REFTABLE.TenentID = CRMMainActivities.TenentID  " +
                                    " and REFTABLE.REFID = CRMMainActivities.TickPhysicalLocation and CRMMainActivities.TickPhysicalLocation is not null and CRMMainActivities.UploadDate is not null group by TickPhysicalLocation, REFTABLE.REFNAME1 ";

                SqlCommand CMD7 = new SqlCommand(SQOCommad6, con);
                SqlDataAdapter ADB7 = new SqlDataAdapter(CMD7);
                DataSet ds7 = new DataSet();
                ADB7.Fill(ds7);
                DataTable dt7 = ds7.Tables[0];

                decimal fl = 0;

                for (int a = 0; a <= dt7.Rows.Count - 1; a++)
                {
                    fl = fl + Convert.ToInt32(dt7.Rows[a]["LocCount"]);
                }





                if (dt7.Rows.Count > 0)
                {
                    if (ds7.Tables[0] != null && ds7.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt7.Rows.Count - 1; i++)
                        {

                            int TickPhysicalLocation = Convert.ToInt32(dt7.Rows[i]["TickPhysicalLocation"]);
                            decimal LocCount = Convert.ToInt32(dt7.Rows[i]["LocCount"]);
                            string REFNAME1 = DB.REFTABLEs.Single(p => p.TenentID == TID && p.REFID == TickPhysicalLocation && p.REFTYPE == "Ticket" && p.REFSUBTYPE == "PhysicalLocation").REFNAME1;

                            Tempdatatable objprofile = new Tempdatatable();
                            objprofile.TenentID = TID;
                            objprofile.Type = 4;
                            objprofile.ID = TickPhysicalLocation;
                            objprofile.Name = REFNAME1;
                            objprofile.UserId = 11525;
                            objprofile.Count = LocCount;
                            objprofile.TotalCount = Convert.ToInt32(fl);
                            decimal percent = (LocCount / fl) * 100;
                            string m1 = percent.ToString();
                            var input = m1;
                            var parts = input.Split('.');
                            var part1 = parts[0];
                            var part2 = parts[1];
                            objprofile.percentage =Convert.ToDecimal(part1);
                            DB.Tempdatatables.AddObject(objprofile);
                            DB.SaveChanges();
                        }
                    }
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
                }

                //decimal finallocation = 0;
                //for (int i = 0; i < dt7.Rows.Count; i++)
                //{
                //    finallocation = finallocation + Convert.ToDecimal(dt7.Rows[i]["LocCount"]);
                //}
                //lbllocation.Text = finallocation.ToString();

                string fl1 = " select TickPhysicalLocation , Tempdatatable.Name  , COUNT(*) as LocCount , Tempdatatable.percentage  " +
                             " FROM  CRMMainActivities , Tempdatatable   where CRMMainActivities.TenentID =  " + TID +
                             " and UploadDate BETWEEN '" + stdate + "' AND    '" + etdate + "' " +
                             " and Tempdatatable.Type=4 and Tempdatatable.TenentID = CRMMainActivities.TenentID   and Tempdatatable.ID = CRMMainActivities.TickPhysicalLocation " +
                             "  and CRMMainActivities.TickPhysicalLocation is not null and CRMMainActivities.UploadDate is not null group by TickPhysicalLocation, Tempdatatable.Name  , Tempdatatable.percentage ";

                SqlCommand CMDs3 = new SqlCommand(fl1, con);
                SqlDataAdapter ADBs3 = new SqlDataAdapter(CMDs3);
                DataSet dss3 = new DataSet();
                ADBs3.Fill(dss3);
                DataTable dts3 = dss3.Tables[0];


                for (int i = 0; i < dts3.Rows.Count; i++)
                {

                    string complainno = dts3.Rows[i]["Name"].ToString();
                    int Master = Convert.ToInt32(dts3.Rows[i]["TickPhysicalLocation"]);
                    int department = Convert.ToInt32(dts3.Rows[i]["LocCount"]);
                    int action = Convert.ToInt32(dts3.Rows[i]["percentage"]);



                    Flag = true;
                    Tablecontant1 += "<tr><td>" + Master + "</td><td>" + complainno + "</td> <td>" + department + " </td> <td>" + action + " </td> </tr>";

                }

                string SQOCommad4 = " select TickCatID , ICCATEGORY.CATNAME  , COUNT(*) as CatCount  FROM  CRMMainActivities , ICCATEGORY " +
                                       " where CRMMainActivities.TenentID =  " + TID + " and ICCATEGORY.CATTYPE='HelpDesk' " +
                                       " and UploadDate BETWEEN '" + stdate + "' AND    '" + etdate + "'  and ICCATEGORY.TenentID = CRMMainActivities.TenentID " +
                                       " and ICCATEGORY.CATID = CRMMainActivities.TickCatID and CRMMainActivities.TickCatID is not null  and CRMMainActivities.UploadDate is not null group by TickCatID, ICCATEGORY.CATNAME ";

                SqlCommand CMD5 = new SqlCommand(SQOCommad4, con);
                SqlDataAdapter ADB5 = new SqlDataAdapter(CMD5);
                DataSet ds5 = new DataSet();
                ADB5.Fill(ds5);
                DataTable dt5 = ds5.Tables[0];

                decimal fc = 0;

                for (int a = 0; a <= dt5.Rows.Count - 1; a++)
                {
                    fc = fc + Convert.ToInt32(dt5.Rows[a]["CatCount"]);
                }


                if (dt5.Rows.Count > 0)
                {
                    if (ds5.Tables[0] != null && ds5.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt5.Rows.Count - 1; i++)
                        {

                            int TickCatID = Convert.ToInt32(dt5.Rows[i]["TickCatID"]);
                            decimal CatCount = Convert.ToInt32(dt5.Rows[i]["CatCount"]);
                            string CATNAME = DB.ICCATEGORies.Single(p => p.TenentID == TID && p.CATID == TickCatID).CATNAME;


                            Tempdatatable objprofile = new Tempdatatable();
                            objprofile.TenentID = TID;
                            objprofile.Type = 2;
                            objprofile.ID = TickCatID;
                            objprofile.Name = CATNAME;
                            objprofile.UserId = 11525;
                            objprofile.Count = CatCount;
                            objprofile.TotalCount = Convert.ToInt32(fc);
                            decimal percent = (CatCount / fc) * 100;
                            string m1 = percent.ToString();
                            var input = m1;
                            var parts = input.Split('.');
                            var part1 = parts[0];
                            var part2 = parts[1];
                            objprofile.percentage = Convert.ToDecimal(part1);
                           // objprofile.percentage = percent;
                            DB.Tempdatatables.AddObject(objprofile);
                            DB.SaveChanges();

                        }
                    }

                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
                }
                //decimal finalcategory = 0;
                //for (int i = 0; i < dt5.Rows.Count; i++)
                //{
                //    finalcategory = finalcategory + Convert.ToDecimal(dt5.Rows[i]["CatCount"]);
                //}
                //lblcategory.Text = finalcategory.ToString();

                //decimal finalcat = 0;
                //for (int i = 0; i < dt5.Rows.Count; i++)
                //{
                //    finalcat = finalcat + Convert.ToDecimal(dt5.Rows[i]["Tempdatatable.percentage"]);
                //}
                //lblpercategory.Text = finalcat.ToString();

                string fc1 = " select TickCatID , Tempdatatable.Name  , COUNT(*) as CatCount , Tempdatatable.percentage  " +
                             " FROM  CRMMainActivities , Tempdatatable   where CRMMainActivities.TenentID =  " + TID +
                             " and UploadDate BETWEEN '" + stdate + "' AND    '" + etdate + "'" +
                             " and Tempdatatable.Type=2 and Tempdatatable.TenentID = CRMMainActivities.TenentID   and Tempdatatable.ID = CRMMainActivities.TickCatID " +
                             "  and CRMMainActivities.TickCatID is not null and CRMMainActivities.UploadDate is not null group by TickCatID, Tempdatatable.Name  , Tempdatatable.percentage ";

                SqlCommand CMDs1 = new SqlCommand(fc1, con);
                SqlDataAdapter ADBs1 = new SqlDataAdapter(CMDs1);
                DataSet dss1 = new DataSet();
                ADBs1.Fill(dss1);
                DataTable dts1 = dss1.Tables[0];
                for (int i = 0; i < dts1.Rows.Count; i++)
                {
                    string complainno = dts1.Rows[i]["Name"].ToString();
                    int Master = Convert.ToInt32(dts1.Rows[i]["TickCatID"]);
                    int department = Convert.ToInt32(dts1.Rows[i]["CatCount"]);
                    int action = Convert.ToInt32(dts1.Rows[i]["percentage"]);



                    Flag = true;
                    Tablecontant2 += "<tr><td>" + Master + "</td><td>" + complainno + "</td> <td>" + department + " </td> <td>" + action + " </td> </tr>";
                }


                string SQOCommad5 = " select TickSubCatID , ICSUBCATEGORY.SUBCATNAME  , COUNT(*) as subCatCount  FROM  CRMMainActivities , ICSUBCATEGORY  " +
                                        " where CRMMainActivities.TenentID = " + TID + " and ICSUBCATEGORY.SUBCATTYPE='HelpDesk' " +
                                        " and UploadDate BETWEEN '" + stdate + "' AND    '" + etdate + "' and ICSUBCATEGORY.TenentID = CRMMainActivities.TenentID " +
                                        " and ICSUBCATEGORY.SUBCATID = CRMMainActivities.TickSubCatID and CRMMainActivities.TickSubCatID is not null and CRMMainActivities.UploadDate is not null group by TickSubCatID, ICSUBCATEGORY.SUBCATNAME ";

                SqlCommand CMD6 = new SqlCommand(SQOCommad5, con);
                SqlDataAdapter ADB6 = new SqlDataAdapter(CMD6);
                DataSet ds6 = new DataSet();
                ADB6.Fill(ds6);
                DataTable dt6 = ds6.Tables[0];

                decimal fs = 0;

                for (int a = 0; a <= dt6.Rows.Count - 1; a++)
                {
                    fs = fs + Convert.ToInt32(dt6.Rows[a]["subCatCount"]);
                }


                if (dt6.Rows.Count > 0)
                {
                    if (ds6.Tables[0] != null && ds6.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt6.Rows.Count - 1; i++)
                        {

                            int TickSubCatID = Convert.ToInt32(dt6.Rows[i]["TickSubCatID"]);
                            decimal subCatCount = Convert.ToInt32(dt6.Rows[i]["subCatCount"]);
                            string SUBCATNAME = DB.ICSUBCATEGORies.Single(p => p.TenentID == TID && p.SUBCATID == TickSubCatID).SUBCATNAME;
                            Tempdatatable objprofile = new Tempdatatable();
                            objprofile.TenentID = TID;
                            objprofile.Type = 3;
                            objprofile.ID = TickSubCatID;
                            objprofile.Name = SUBCATNAME;
                            objprofile.UserId = 11525;
                            objprofile.Count = subCatCount;
                            objprofile.TotalCount = Convert.ToInt32(fs);
                            decimal percent = (subCatCount / fs) * 100;
                            string m1 = percent.ToString();
                            var input = m1;
                            var parts = input.Split('.');
                            var part1 = parts[0];
                            var part2 = parts[1];
                            objprofile.percentage = Convert.ToDecimal(part1);
                          //  objprofile.percentage = percent;
                            DB.Tempdatatables.AddObject(objprofile);
                            DB.SaveChanges();

                        }
                    }

                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('No data available for this month');window.location='HelpdeskExcRep.aspx';", true);
                }
                //decimal finalsubcategory = 0;
                //for (int i = 0; i < dt6.Rows.Count; i++)
                //{
                //    finalsubcategory = finalsubcategory + Convert.ToDecimal(dt6.Rows[i]["subCatCount"]);
                //}
                //lblsubcategory.Text = finalsubcategory.ToString();


                //decimal finalpersubcategory = 0;
                //for (int i = 0; i < dt6.Rows.Count; i++)
                //{
                //    finalpersubcategory = finalpersubcategory + Convert.ToDecimal(dt6.Rows[i]["Tempdatatable.percentage"]);
                //}
                //lblpersubcategory.Text = finalpersubcategory.ToString();

                string fs1 = " select TickSubCatID , Tempdatatable.Name  , COUNT(*) as subCatCount , Tempdatatable.percentage  " +
                             " FROM  CRMMainActivities , Tempdatatable   where CRMMainActivities.TenentID =  " + TID +
                             " and UploadDate BETWEEN '" + stdate + "' AND    '" + etdate + "' " +
                             " and Tempdatatable.Type=3 and Tempdatatable.TenentID = CRMMainActivities.TenentID   and Tempdatatable.ID = CRMMainActivities.TickSubCatID " +
                             "  and CRMMainActivities.TickSubCatID is not null and CRMMainActivities.UploadDate is not null group by TickSubCatID, Tempdatatable.Name  , Tempdatatable.percentage ";

                SqlCommand CMDs2 = new SqlCommand(fs1, con);
                SqlDataAdapter ADBs2 = new SqlDataAdapter(CMDs2);
                DataSet dss2 = new DataSet();
                ADBs2.Fill(dss2);
                DataTable dts2 = dss2.Tables[0];


                for (int i = 0; i < dts2.Rows.Count; i++)
                {

                    string complainno = dts2.Rows[i]["Name"].ToString();
                    int Master = Convert.ToInt32(dts2.Rows[i]["TickSubCatID"]);
                    int department = Convert.ToInt32(dts2.Rows[i]["subCatCount"]);
                    int action = Convert.ToInt32(dts2.Rows[i]["percentage"]);



                    Flag = true;
                    Tablecontant3 += "<tr><td>" + Master + "</td><td>" + complainno + "</td> <td>" + department + " </td> <td>" + action + " </td> </tr>";

                }



                if (Flag)
                {
                    Tablecontant += " </tbody> </table>";
                    Tablecontant1 += " </tbody> </table>";
                    Tablecontant2 += " </tbody> </table>";
                    Tablecontant3 += " </tbody> </table>";
                    string Ourcontant = " <span style = \"font-family:Arial;font-size:10pt\"> Subject :'" + stdate + "' AND    '" + etdate + "'  <b></b>,<br /><br />This email is reference to the Complain recorded on " + stdate + "," + stdate + " ,  <br /><br />" + Tablecontant + "<br /><br />" + Tablecontant1 + "<br /><br />" + Tablecontant2 + "<br /><br />" + Tablecontant3 + "<br /><br /><br /></span>";
                    string Tocontant = " <span style = \"font-family:Arial;font-size:10pt\"> Subject :'" + stdate + "' AND    '" + etdate + "' <b></b>,<br /><br />This email is reference to the Complain recorded on " + stdate + "," + stdate + " ,  <br /><br />" + Tablecontant + "<br /><br />" + Tablecontant1 + "<br /><br />" + Tablecontant2 + "<br /><br />" + Tablecontant3 + "<br /><br /><br /></span>";

                    sendEmail(Tocontant, txtpmail.Text);
                    sendEmail(Ourcontant, "dangijalpa@gmail.com");
                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('Successfully mail Send.');", true);
                 }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "myscript", "alert('No data found');", true);
                    return;
                }
            }
        }
        public void sendEmail(string body, string email)
        {
             DateTime startdate = Convert.ToDateTime(txtstartdate.Text);
                DateTime Enddate = Convert.ToDateTime(txtenddate.Text);

                string stdate = startdate.ToString("yyyy-MM-dd");
                string etdate = Enddate.ToString("yyyy-MM-dd");
            if (String.IsNullOrEmpty(email))
                return;
            //try
            //{
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.Subject = "'" + stdate + "' AND    '" + etdate + "'";
            msg.From = new System.Net.Mail.MailAddress("complaints@royalehayat.com");//("supportteam@digital53.com ");
            msg.To.Add(new System.Net.Mail.MailAddress(email));
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.Body = body;
            msg.IsBodyHtml = true;
            msg.Priority = System.Net.Mail.MailPriority.High;
            System.Net.Mail.SmtpClient smpt = new System.Net.Mail.SmtpClient();
            smpt.UseDefaultCredentials = false;
            smpt.Host = "smtp.siteprotect.com";//for google required smtp.gmail.com and must be check Google Account setting https://myaccount.google.com/lesssecureapps?pli=1 ON//"mail.digital53.com";
            smpt.Port = 587;
            smpt.EnableSsl = true;
            //smpt.Credentials = new System.Net.NetworkCredential("supportteam@digital53.com ", "Support123$");
            smpt.Credentials = new System.Net.NetworkCredential("complaints@royalehayat.com", "Royal123$");
            smpt.Send(msg);
        }




    }
    //public class CrmACT
    //{
    //    public int Tenent { get; set; }
    //    public int MasterCode { get; set; }
    //    public int myid { get; set; }
    //    public DateTime datee { get; set; }
    //    public string COMMANID { get; set; }
    //    public string TickPhysicalLocation { get; set; }
    //    public string TickCatID { get; set; }
    //    public string TickSubCatID { get; set; }
    //    public string typee { get; set; }
    //}
}