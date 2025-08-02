using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Web
{
    public partial class TrackProduct : System.Web.UI.Page
    {  CallEntities DB = new CallEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack )
            {
                int TID = Convert.ToInt32(Session["Tenent"]);
                ViewState["TempDisplayEcomTable"] = Classes.EcommUserClass.getDataTempDisplayEcomTable(TID);
                BindCategory();
                if (Session["EcommUSER"] == null)
                {
                    Response.Redirect("Login.aspx");

                }
            }
        }
        public void BindCategory()
        {



            // string qry = "SELECT [CATID],[PARENT_CATID],[SHORT_NAME],CAT_NAME1,[CAT_TYPE],(select count(*) from CAT_MST as B where B.[CAT_TYPE]='WEBSALE' And B.[PARENT_CATID]= A.[CATID]) as SubCatFound FROM CAT_MST as A where A.[CAT_TYPE]='WEBSALE' And A.[PARENT_CATID]=0 And (select count(*) from CAT_MST as B where B.[CAT_TYPE]='WEBSALE' And B.[PARENT_CATID]= A.[CATID]) >=5 ORDER BY CAT_NAME1 ";
            //string qry = " select [PARENT_CATID],count(*) from [NewSaasDB].[dbo].[TempDisplayEcomTable] group by [PARENT_CATID]";

            //string qry = "select distinct [CATID],CatName,PARENT_CATID,(select count(*) from TempDisplayEcomTable as B where A.[CATID]=B.[PARENT_CATID]) as total   from [TempDisplayEcomTable] as A where (select count(*) from TempDisplayEcomTable as B where A.[CATID]=B.[PARENT_CATID])>=5 ORDER BY CatName";
            //  command2 = new SqlCommand(qry, con);
            //  con.Open();

            // SqlDataReader reader2 = command2.ExecuteReader();
            int TID = Convert.ToInt32(Session["Tenent"]);
            List<TempDisplayEcomTable> list = new List<TempDisplayEcomTable>();
            List<TempDisplayEcomTable> list1 = (List<TempDisplayEcomTable>)ViewState["TempDisplayEcomTable"];
            list = list1.Where(a => a.TenentID == TID && a.Active == "1").GroupBy(p => p.PARENT_CATID).Select(grp => grp.FirstOrDefault()).ToList();
            ltsCategory.DataSource = list;
            ltsCategory.DataBind();
            list =list1.Where(p => p.Active == "1" && p.TenentID == TID && p.REFNAME2 == "BS").ToList();
            listBestSell.DataSource = list.Take(3);
            listBestSell.DataBind();

            listproduct.DataSource = list1.Where(p => p.TenentID == TID && p.Active == "1").OrderByDescending(p => p.MyID).Take(5);
            listproduct.DataBind();



        }
        protected void listproduct_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int TID = Convert.ToInt32(Session["Tenent"]);
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblProdID = (Label)e.Item.FindControl("lblProdID");
                Label labelNewpi = (Label)e.Item.FindControl("labelNewpi");
                //  Label lbloeldpries = (Label)e.Item.FindControl("lbloeldpries");
                Label labNewPties = (Label)e.Item.FindControl("labNewPties");
                Label lbloledpriesh = (Label)e.Item.FindControl("lbloledpriesh");
                Label lblSize = (Label)e.Item.FindControl("lblSize");
                Label lblcoler = (Label)e.Item.FindControl("lblcoler");
                DropDownList drpSize = (DropDownList)e.Item.FindControl("drpSize");
                DropDownList drpcoler = (DropDownList)e.Item.FindControl("drpcoler");
                ListView listImagBind = (ListView)e.Item.FindControl("listImagBind");
                ListView listMultiSizeNewArevel = (ListView)e.Item.FindControl("listMultiSizeNewArevel");
                ListView listMultiColerNewArevw = (ListView)e.Item.FindControl("listMultiColerNewArevw");
                int MID = Convert.ToInt32(lblProdID.Text);
                int PID = Convert.ToInt32(DB.TempDisplayEcomTables.Single(p => p.MyID == MID && p.TenentID == TID).MYPRODID);
                var obj = DB.TempDisplayEcomTables.Single(p => p.MYPRODID == PID && p.MyID == MID && p.TenentID == TID);
                List<ImageTable> list1 = Classes.EcommUserClass.getDataImageTable(TID);
                if (obj.Size == true)
                {
                    listMultiSizeNewArevel.DataSource = list1.Where(p => p.MYPRODID == PID && p.TenentID == TID).GroupBy(a => a.SIZECODE).Select(grp => grp.FirstOrDefault());
                    listMultiSizeNewArevel.DataBind();

                }
                if (obj.Color == true)
                {
                    listMultiColerNewArevw.DataSource = list1.Where(p => p.MYPRODID == PID && p.Active == "1" && p.TenentID == TID).GroupBy(a => a.COLORID).Select(grp => grp.FirstOrDefault());
                    listMultiColerNewArevw.DataBind();
                }
                Boolean MultiColor = Convert.ToBoolean(DB.TBLPRODUCTs.Single(p => p.MYPRODID == PID && p.TenentID == TID).MultiColor);
                if (list1.Where(p => p.MYPRODID == PID && p.Active == "1" && p.TenentID == TID).Count() > 0)
                {
                    if (MultiColor == Convert.ToBoolean(1))
                    {
                        if (DB.Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == PID && p.RecordType == "MultiColor" && p.RunSerial == 1 && p.TenentID == TID).Count() > 0)
                        {
                            int ColerId = DB.Tbl_Multi_Color_Size_Mst.Single(p => p.CompniyAndContactID == PID && p.RecordType == "MultiColor" && p.RunSerial == 1 && p.TenentID == TID).RecTypeID;
                            string imgmane = list1.Single(p => p.MYPRODID == PID && p.ImageID == 98801 && p.COLORID == ColerId).PICTURE;

                            listImagBind.DataSource = list1.Where(p => p.MYPRODID == PID && p.COLORID == ColerId && p.TenentID == TID);
                            listImagBind.DataBind();
                        }
                        else
                        {
                            listImagBind.DataSource = list1.Where(p => p.MYPRODID == PID && p.TenentID == TID);
                            listImagBind.DataBind();
                        }

                    }

                    else
                    {
                        listImagBind.DataSource = list1.Where(p => p.MYPRODID == PID && p.TenentID == TID);
                        listImagBind.DataBind();
                    }


                }
                Boolean Offer = Convert.ToBoolean(obj.OfferProd);
                double PriceshOeld = Convert.ToDouble(obj.Price);
                if (Offer == Convert.ToBoolean(1))
                {
                    double Pricesh = Convert.ToDouble(DB.tblOfferProds.Single(p => p.MYPRODID == PID && p.TenentID == TID).NewPrice);
                    labNewPties.Text = Pricesh.ToString("N2");
                    lbloledpriesh.Text = "K.D" + PriceshOeld.ToString("N2");
                    labelNewpi.Text = Pricesh.ToString("N2");
                    //   lbloeldpries.Text = "K.D" + obj.Price.ToString();
                }
                else
                {
                    labNewPties.Text = PriceshOeld.ToString("N2");
                    labelNewpi.Text = PriceshOeld.ToString("N2");
                }

            }
        }
        protected void ltsCategory_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int TID = Convert.ToInt32(Session["Tenent"]);
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Label lblCATID = (Label)e.Item.FindControl("lblCATID");
                Label lblSubCount = (Label)e.Item.FindControl("lblSubCount");
                Label lblCount = (Label)e.Item.FindControl("lblCount");

                int ID = Convert.ToInt32(lblCATID.Text);
                ListView ltsSubCategory = (ListView)e.Item.FindControl("ltsSubCategory");

                if (Session["EcommLanguage"] == null) { Session["EcommLanguage"] = "English"; }
                string lang = Session["EcommLanguage"].ToString();
                List<TempDisplayEcomTable> list1 = (List<TempDisplayEcomTable>)ViewState["TempDisplayEcomTable"];
                list1 = list1.Where(a => a.TenentID == TID && a.PARENT_CATID == ID && a.Active == "1").GroupBy(p => p.CATID).Select(grp => grp.FirstOrDefault()).ToList();
                lblSubCount.Text = " ( " + list1.Count().ToString() + " )";
                ltsSubCategory.DataSource = list1;
                ltsSubCategory.DataBind();

            }
        }
        protected void ltsBrand_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {// haresh
                //Label lblHide = (Label)e.Item.FindControl("lblHide");
                //int ID = Convert.ToInt32(lblHide.Text);
                //ListView ltsProduct = (ListView)e.Item.FindControl("ltsProduct");
                //var data = from b in DB.REFTABLEs
                //           join s in DB.TBLPRODUCTs on
                //               b.REFID equals s.Brand
                //           where b.REFID == ID
                //           select new { b.REFID, s.SUBPRODNAME };
                //ltsProduct.DataSource = data.ToList();

                //ltsProduct.DataBind();
            }

        }
        public string GETIMG3(int IID)
        {
            int TID = Convert.ToInt32(Session["Tenent"]);
            List<ImageTable> List = DB.ImageTables.Where(p => p.MYPRODID == IID && p.ImageID == 98804 && p.Active == "1" && p.TenentID == TID).ToList();
            if (List.Count() > 0)
            {
                Boolean MultiColor = Convert.ToBoolean(DB.TBLPRODUCTs.Single(p => p.MYPRODID == IID && p.TenentID == TID).MultiColor);
                if (MultiColor == Convert.ToBoolean(1))
                {
                    if (DB.Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == IID && p.RecordType == "MultiColor" && p.RunSerial == 1 && p.TenentID == TID).Count() > 0)
                    {
                        int ColerId = DB.Tbl_Multi_Color_Size_Mst.Single(p => p.CompniyAndContactID == IID && p.RecordType == "MultiColor" && p.RunSerial == 1 && p.TenentID == TID).RecTypeID;

                        return List.Single(p => p.COLORID == ColerId && p.TenentID == TID).PICTURE;
                    }
                    else
                    {
                        if (List.Count() == 1)
                            return List.Single(p=>p.TenentID == TID).PICTURE;
                        else
                            return "defolt.png";
                    }
                }
                else
                {
                    List<ImageTable> obj = List.GroupBy(p => p.ImageID).Select(grp => grp.FirstOrDefault()).ToList();
                    int ID = Convert.ToInt32(obj[0].ID);
                    if (List.Where(p => p.ID == ID && p.TenentID == TID).Count() == 1)
                        return List.Single(p => p.ID == ID && p.TenentID == TID).PICTURE;
                    else
                        return "defolt.png";
                }
            }
            else
            {
                return "";
            }
        }
        public string GETIMG(int IID)
        {
            int TID = Convert.ToInt32(Session["Tenent"]);
            List<ImageTable> List = DB.ImageTables.Where(p => p.MYPRODID == IID && p.ImageID == 98801 && p.Active == "1" && p.TenentID == TID).ToList();

            if (List.Count() > 0)
            {
                Boolean MultiColor = Convert.ToBoolean(DB.TBLPRODUCTs.Single(p => p.MYPRODID == IID && p.TenentID == TID).MultiColor);
                if (MultiColor == Convert.ToBoolean(1))
                {
                    if (DB.Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == IID && p.RecordType == "MultiColor" && p.RunSerial == 1 && p.TenentID == TID).Count() > 0)
                    {
                        int ColerId = DB.Tbl_Multi_Color_Size_Mst.Single(p => p.CompniyAndContactID == IID && p.RecordType == "MultiColor" && p.RunSerial == 1 && p.TenentID == TID).RecTypeID;
                        return List.Single(p => p.COLORID == ColerId && p.TenentID == TID).PICTURE;

                    }
                    else
                    {

                        if (List.Count() == 1)
                            return List.Single(p=>p.TenentID==TID).PICTURE;
                        else
                            return "defolt.png";
                    }

                }
                else
                {
                    List<ImageTable> obj = List.GroupBy(p => p.ImageID).Select(grp => grp.FirstOrDefault()).ToList();
                    int ID = Convert.ToInt32(obj[0].ID);
                    if (List.Where(p => p.ID == ID && p.TenentID == TID).Count() == 1)
                        return List.Single(p => p.ID == ID && p.TenentID == TID).PICTURE;
                    else
                        return "defolt.png";
                }
            }
            else
            {
                return "defolt.png";
            }

        }
        public string GETIMG1(int IID)
        {
            int TID = Convert.ToInt32(Session["Tenent"]);
            List<ImageTable> List = DB.ImageTables.Where(p => p.MYPRODID == IID && p.ImageID == 98802 && p.Active == "1" && p.TenentID == TID).ToList();
            if (List.Count() > 0)
            {
                Boolean MultiColor = Convert.ToBoolean(DB.TBLPRODUCTs.Single(p => p.MYPRODID == IID && p.TenentID == TID).MultiColor);
                if (MultiColor == Convert.ToBoolean(1))
                {
                    if (DB.Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == IID && p.RecordType == "MultiColor" && p.RunSerial == 1 && p.TenentID == TID).Count() > 0)
                    {
                        int ColerId = DB.Tbl_Multi_Color_Size_Mst.Single(p => p.CompniyAndContactID == IID && p.RecordType == "MultiColor" && p.RunSerial == 1 && p.TenentID == TID).RecTypeID;

                        return List.Single(p => p.COLORID == ColerId && p.TenentID == TID).PICTURE;
                    }
                    else
                    {
                        if (List.Count() == 1)
                            return List.Single(p=>p.TenentID ==TID).PICTURE;
                        else
                            return "defolt.png";
                    }
                }
                else
                {
                    List<ImageTable> obj = List.GroupBy(p => p.ImageID).Select(grp => grp.FirstOrDefault()).ToList();
                    int ID = Convert.ToInt32(obj[0].ID);
                    if (List.Where(p => p.ID == ID && p.TenentID == TID).Count() == 1)
                        return List.Single(p => p.ID == ID && p.TenentID == TID).PICTURE;
                    else
                        return "defolt.png";
                }

            }
            else
            {
                return "";
            }
        }
        public string GETIMG2(int IID)
        {
            int TID = Convert.ToInt32(Session["Tenent"]);
            List<ImageTable> List = DB.ImageTables.Where(p => p.MYPRODID == IID && p.ImageID == 98803 && p.Active == "1" && p.TenentID == TID).ToList();
            if (List.Count() > 0)
            {
                Boolean MultiColor = Convert.ToBoolean(DB.TBLPRODUCTs.Single(p => p.MYPRODID == IID && p.TenentID == TID).MultiColor);
                if (MultiColor == Convert.ToBoolean(1))
                {
                    if (DB.Tbl_Multi_Color_Size_Mst.Where(p => p.CompniyAndContactID == IID && p.RecordType == "MultiColor" && p.RunSerial == 1 && p.TenentID == TID).Count() > 0)
                    {
                        int ColerId = DB.Tbl_Multi_Color_Size_Mst.Single(p => p.CompniyAndContactID == IID && p.RecordType == "MultiColor" && p.RunSerial == 1 && p.TenentID == TID).RecTypeID;

                        return List.Single(p => p.COLORID == ColerId && p.TenentID == TID).PICTURE;
                    }
                    else
                    {
                        if (List.Count() == 1)
                            return List.Single(p=>p.TenentID == TID).PICTURE;
                        else
                            return "defolt.png";
                    }
                }
                else
                {
                    List<ImageTable> obj = List.GroupBy(p => p.ImageID).Select(grp => grp.FirstOrDefault()).ToList();
                    int ID = Convert.ToInt32(obj[0].ID);
                    if (List.Where(p => p.ID == ID && p.TenentID == TID).Count() == 1)
                        return List.Single(p => p.ID == ID && p.TenentID == TID).PICTURE;
                    else
                        return "defolt.png";
                }
            }
            else
            {
                return "";
            }
        }
        public string getprodname(int SID)
        {
            int TID = Convert.ToInt32(Session["Tenent"]);
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == SID && p.TenentID == TID).ProdName1;
        }
        public string getCost(int CID)
        {
            int TID = Convert.ToInt32(Session["Tenent"]);
            decimal id = Convert.ToDecimal(CID);
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == id && p.TenentID == TID).price.ToString();
        }
        public string getsale(int PID)
        {
            int TID = Convert.ToInt32(Session["Tenent"]);
            var obj = DB.TempDisplayEcomTables.Single(p => p.MYPRODID == PID && p.TenentID == TID);
            // if(DB .TempDisplayEcomTable .Where (p=>p.MYPRODID ==PID).Count ()>0)
            if (obj.Sale == true)
            {
                return "<div  class='sticker sticker-sale'></div>";
            }
            if (obj.New == true)
            {
                return "<div  class='sticker sticker-new'></div>";
            }
            return null;
        }
        public string getshomname(int ID)
        {
            int TID = Convert.ToInt32(Session["Tenent"]);
            return DB.TBLPRODUCTs.Single(p => p.MYPRODID == ID && p.TenentID == TID).ProdName1;
        }
        public string getAvailibility(int ID)
        {
            int TID = Convert.ToInt32(Session["Tenent"]);
            return DB.TempDisplayEcomTables.Single(p => p.MYPRODID == ID && p.TenentID == TID).IsStock == true ? "In Stock" : "Out of Stock";
        }
        public string getdiscription(int ID)
        {
            int TID = Convert.ToInt32(Session["Tenent"]);
            return DB.TBLPRODDTLs.Single(p => p.MYPRODID == ID && p.TenentID == TID).OVERVIEW;
        }
    }
}