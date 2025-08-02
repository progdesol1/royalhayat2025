using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Data.SqlClient;
using System.Configuration;

namespace Web.ACM
{
    public partial class testg : System.Web.UI.Page
    {
        CallEntities DB = new CallEntities();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRMNewEntitiesNew"].ConnectionString);
        SqlCommand command2 = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Server.TransferRequest("/CRM/CompanyMaster.aspx?COMPID="+7);
            //string INFo = GetMACAddress();
            //string HostName = Dns.GetHostName();
            //Console.WriteLine("Name of machine =" + HostName);  
            //List<Database.TBLPRODUCT> d123 = getDataTBLPRODUCT(1);
            //int FromTID = 1;
            //int TTID = Convert.ToInt32(((USER_MST)Session["USER"]).TenentID);
            int FromTID = Convert.ToInt32(1);
            int Comp = 0;
            string Str = "";
            int Tenent = Convert.ToInt32(txtTenent.Text);
            var TDT = DateTime.Now.Date;
            if (DB.TBLLOCATIONs.Where(p => p.TenentID == Tenent).Count() == 0)
                Str += "INSERT INTO [dbo].[TBLLOCATION]([TenentID],[LOCATIONID],[PHYSICALLOCID],[LOCNAME1],[LOCNAME2],[LOCNAME3],[ADDRESS],[DEPT],[SECTIONCODE],[ACCOUNT],[MAXDISCOUNT],[USERID],[ENTRYDATE],[ENTRYTIME],[UPDTTIME],[Active],[LOCNAME],[LOCNAMEO],[LOCNAMECH],[CRUP_ID]) SELECT " + Tenent + ",[LOCATIONID],[PHYSICALLOCID],[LOCNAME1],[LOCNAME2],[LOCNAME3],[ADDRESS],[DEPT],[SECTIONCODE],[ACCOUNT],[MAXDISCOUNT],[USERID],[ENTRYDATE],[ENTRYTIME],[UPDTTIME],[Active],[LOCNAME],[LOCNAMEO],[LOCNAMECH],[CRUP_ID] from [TBLLOCATION] where [TenentID]=" + FromTID + ";";
            //if (DB.REFTABLEs.Where(p => p.TenentID == Tenent).Count() == 0)
            //    Str += "INSERT INTO [dbo].[REFTABLE]([TenentID],[REFID],[REFTYPE],[REFSUBTYPE],[SHORTNAME],[REFNAME1],[REFNAME2],[REFNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[SWITCH4],[Remarks],[ACTIVE],[CRUP_ID],[Infrastructure],[SyncDate]) Select " + Tenent + ",[REFID],[REFTYPE],[REFSUBTYPE],[SHORTNAME],[REFNAME1],[REFNAME2],[REFNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[SWITCH4],[Remarks],[ACTIVE],[CRUP_ID],[Infrastructure],[SyncDate] from [REFTABLE] where [TenentID]=" + FromTID + ";";
            Str += "INSERT INTO [dbo].[REFTABLE]([TenentID],[REFID],[REFTYPE],[REFSUBTYPE],[SHORTNAME],[REFNAME1],[REFNAME2],[REFNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[SWITCH4],[Remarks],[ACTIVE],[CRUP_ID],[Infrastructure],[SyncDate]) Select " + Tenent + ",[REFID],[REFTYPE],[REFSUBTYPE],[SHORTNAME],[REFNAME1],[REFNAME2],[REFNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[SWITCH4],[Remarks],[ACTIVE],[CRUP_ID],[Infrastructure],[SyncDate] from [REFTABLE] where [TenentID]=" + FromTID + " and REFID not in(select REFID from REFTABLE where TenentID = " + Tenent + " and REFID = REFTABLE.REFID);";
            if (DB.tblCOUNTRies.Where(p => p.TenentID == Tenent).Count() == 0)
                Str += "INSERT INTO [dbo].[tblCOUNTRY]([TenentID],[COUNTRYID],[REGION1],[COUNAME1],[COUNAME2],[COUNAME3],[CAPITAL],[NATIONALITY1],[NATIONALITY2],[NATIONALITY3],[CURRENCYNAME1],[CURRENCYNAME2],[CURRENCYNAME3],[CURRENTCONVRATE],[CURRENCYSHORTNAME1],[CURRENCYSHORTNAME2],[CURRENCYSHORTNAME3],[CountryType],[CountryTSubType],[Sovereignty],[ISO4217CurCode],[ISO4217CurName],[ITUTTelephoneCode],[FaxLength],[TelLength],[ISO3166_1_2LetterCode],[ISO3166_1_3LetterCode],[ISO3166_1Number],[IANACountryCodeTLD],[Active],[CRUP_ID]) select " + Tenent + ",[COUNTRYID],[REGION1],[COUNAME1],[COUNAME2],[COUNAME3],[CAPITAL],[NATIONALITY1],[NATIONALITY2],[NATIONALITY3],[CURRENCYNAME1],[CURRENCYNAME2],[CURRENCYNAME3],[CURRENTCONVRATE],[CURRENCYSHORTNAME1],[CURRENCYSHORTNAME2],[CURRENCYSHORTNAME3],[CountryType],[CountryTSubType],[Sovereignty],[ISO4217CurCode],[ISO4217CurName],[ITUTTelephoneCode],[FaxLength],[TelLength],[ISO3166_1_2LetterCode],[ISO3166_1_3LetterCode],[ISO3166_1Number],[IANACountryCodeTLD],[Active],[CRUP_ID] from [tblCOUNTRY] where [TenentID]=" + FromTID + ";";
            if (DB.TBLCOLORs.Where(p => p.TenentID == Tenent && p.COLORID == 999999999).Count() == 0)
                Str += "INSERT INTO [dbo].[TBLCOLOR]([TenentID],[COLORID],[COLORDESC1],[COLORDESC2],[COLORREMARKS],[hex],[RGB],[color],[CRUP_ID],[Active]) VALUES (" + Tenent + ",999999999,'NOT USED','NOT USED','NOT USED','NOT USED','NOT USED','NOT USED',0,'Y');";
            //Str += "INSERT INTO [dbo].[TBLCOLOR]([TenentID],[COLORID],[COLORDESC1],[COLORDESC2],[COLORREMARKS],[hex],[RGB],[color],[CRUP_ID],[Active]) select " + Tenent + ",[COLORID],[COLORDESC1],[COLORDESC2],[COLORREMARKS],[hex],[RGB],[color],[CRUP_ID],[Active] from [TBLCOLOR] where TenentID=" + FromTID + ";";
            if (DB.TBLSIZEs.Where(p => p.TenentID == Tenent && p.SIZECODE == 999999999).Count() == 0)
                Str += "INSERT INTO [dbo].[TBLSIZE]([TenentID],[SIZECODE],[SIZETYPE],[SIZEDESC1],[SIZEDESC2],[SIZEDESC3],[SIZEREMARKS],[CRUP_ID],[ACTIVE])VALUES(" + Tenent + ",999999999,'NOT Used','NOT Used','NOT Used','NOT Used','NOT Used',0,'Y');";
            //Str += "INSERT INTO [dbo].[TBLSIZE]([TenentID],[SIZECODE],[SIZETYPE],[SIZEDESC1],[SIZEDESC2],[SIZEDESC3],[SIZEREMARKS],[CRUP_ID],[ACTIVE])select " + Tenent + ",[SIZECODE],[SIZETYPE],[SIZEDESC1],[SIZEDESC2],[SIZEDESC3],[SIZEREMARKS],[CRUP_ID],[ACTIVE] from [TBLSIZE] where TenentID=" + FromTID + ";";
            if (DB.ICUOMs.Where(p => p.TenentID == Tenent && p.UOM == 99999).Count() == 0)
                Str += "INSERT INTO [dbo].[ICUOM]([TenentID],[UOM],[UOMNAMESHORT],[UOMNAME1],[UOMNAME2],[UOMNAME3],[REMARKS],[CRUP_ID],[Active],[UOMNAME],[UOMNAMEO],[UploadDate],[SynID])VALUES(" + Tenent + ",99999,'Not Used','Not Used','Not Used','Not Used','Not Used',0,'Y','Not Used','Not Used','" + TDT + "',1);";
            if (DB.CAT_MST.Where(p => p.TenentID == Tenent && p.CATID == 99999).Count() == 0)
                Str += "INSERT INTO [dbo].[CAT_MST]([TenentID],[CATID],[PARENT_CATID],[DefaultPic],[SHORT_NAME],[CAT_NAME1],[CAT_NAME2],[CAT_NAME3],[CAT_DESCRIPTION],[CAT_TYPE],[WARRANTY],[CRUP_ID],[Active],[SupplierPercent],[UploadDate],[SynID]) VALUES (" + Tenent + ",99999,0,'cc','Not Used','Not Used','Not Used','Not Used','Not Used','WEBSALE','0 Months',0,'1',0.0,'" + TDT + "',1);";
            if (DB.TBLGROUPs.Where(p => p.TenentID == Tenent && p.ITGROUPID == 999999999).Count() == 0)
                Str += "INSERT INTO [dbo].[TBLGROUP]([TenentID],[LocationId],[ITGROUPID],[GroupType],[ITGROUPDESC1],[ITGROUPDESC2],[ITGROUPREMARKS],[ACTIVE_Flag],[USERCODE],[GROUPID],[remarks],[ACTIVE],[CRUP_ID],[Infastructure]) VALUES (" + Tenent + ",1,999999999,'','Not Used','Not Used','Not Used','True','','','','1',0,'False');";
            if (DB.DEPTOFSales.Where(p => p.TenentID == Tenent && p.SalDeptID == 999999999).Count() == 0)
                Str += "INSERT INTO [dbo].[DEPTOFSale]([TenentID],[SalDeptID],[DeptDesc1],[DeptDesc2],[DeptDesc3],[DeptRemarks],[SalesAccountID],[Margin],[ExpenseAccountID],[PurchaseAccountID],[Active_Flag],[CRUP_ID],[DeptManagerID]) VALUES (" + Tenent + ",999999999,'Not Exist Yet','Not Exist Yet','Not Exist Yet','Not Exist Yet','0',0.0,'','','True',0,0);";
            if (DB.MYBUS.Where(p => p.TenentID == Tenent).Count() == 0)
                Str += "INSERT INTO [dbo].[MYBUS]([TenentID],[BUSID],[BUSTYPE],[SHORTNAME],[BUSNAME1],[BUSNAME2],[BUSNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[Remarks],[COMPANID],[BUSNAME],[BUSNAMEO],[BUSNAMCHIN],[BUSYPE],[CRUP_ID]) Select " + Tenent + ",[BUSID],[BUSTYPE],[SHORTNAME],[BUSNAME1],[BUSNAME2],[BUSNAME3],[SWITCH1],[SWITCH2],[SWITCH3],[Remarks],[COMPANID],[BUSNAME],[BUSNAMEO],[BUSNAMCHIN],[BUSYPE],[CRUP_ID] from [MYBUS] Where TenentID=" + FromTID + ";";
            if (DB.RefLabelMSTs.Where(p => p.TenentID == Tenent).Count() == 0)
                Str += "INSERT INTO [dbo].[RefLabelMST]([TenentID],[RefLabelID],[RefType],[RefSubType],[LE1],[LE2],[LE3],[LE4],[LE5],[LE6],[LE7],[LE8],[LE9],[LE10],[LF1],[LF2],[LF3],[LF4],[LF5],[LF6],[LF7],[LF8],[LF9],[LF10],[LA1],[LA2],[LA3],[LA4],[LA5],[LA6],[LA7],[LA8],[LA9],[LA10]) Select " + Tenent + ",[RefLabelID],[RefType],[RefSubType],[LE1],[LE2],[LE3],[LE4],[LE5],[LE6],[LE7],[LE8],[LE9],[LE10],[LF1],[LF2],[LF3],[LF4],[LF5],[LF6],[LF7],[LF8],[LF9],[LF10],[LA1],[LA2],[LA3],[LA4],[LA5],[LA6],[LA7],[LA8],[LA9],[LA10] from [RefLabelMST] where TenentID=" + FromTID + ";";
            //if (DB.tbltranstypes.Where(p => p.TenentID == Tenent).Count() == 0)
            //Str += "select * into TempCopy_tbltranstype from tbltranstype where TenentID = " + FromTID + ";update TempCopy_tbltranstype set TenentID = " + Tenent + " where TenentID = " + FromTID + ";INSERT INTO tbltranstype select * from TempCopy_tbltranstype where TenentID = " + Tenent + ";drop table TempCopy_tbltranstype;";                
            //if (DB.tbltranssubtypes.Where(p => p.TenentID == Tenent).Count() == 0)
            //Str += "select * into TempCopy_tbltranssubtype from tbltranssubtype where TenentID = " + FromTID + ";update TempCopy_tbltranssubtype set TenentID = " + Tenent + " where TenentID = " + FromTID + ";INSERT INTO tbltranssubtype select * from TempCopy_tbltranssubtype where TenentID = " + Tenent + ";drop table TempCopy_tbltranssubtype;";
            Str += "INSERT INTO [dbo].[tbltranstype]([TenentID],[transid],[MYSYSNAME],[inoutSwitch],[transtype1],[transtype2],[transtype3],[serialno],[years],[Active],[CRUP_ID],[transtype],[switch1]) select " + Tenent + ",[transid],[MYSYSNAME],[inoutSwitch],[transtype1],[transtype2],[transtype3],[serialno],[years],[Active],[CRUP_ID],[transtype],[switch1] from tbltranstype where TenentID=" + FromTID + " and transid not in(select transid from tbltranstype where TenentID=" + Tenent + " and transid=tbltranstype.transid);";
            Str += "INSERT INTO [dbo].[tbltranssubtype]([TenentID],[transid],[MYSYSNAME],[transsubid],[transsubtype1],[transsubtype2],[transsubtype3],[OpQtyBeh],[OnHandBeh],[QtyOutBeh],[QtyConsumedBeh],[QtyReservedBeh],[QtyAtDestination],[QtyAtSource],[serialno],[years],[Active],[CRUP_ID],[transsubtype],[CashBeh],[QtyReceivedBeh]) select " + Tenent + ",[transid],[MYSYSNAME],[transsubid],[transsubtype1],[transsubtype2],[transsubtype3],[OpQtyBeh],[OnHandBeh],[QtyOutBeh],[QtyConsumedBeh],[QtyReservedBeh],[QtyAtDestination],[QtyAtSource],[serialno],[years],[Active],[CRUP_ID],[transsubtype],[CashBeh],[QtyReceivedBeh] from tbltranssubtype where TenentID = " + FromTID + " and transsubid not in(select transsubid from tbltranssubtype where TenentID=" + Tenent + " and transid = tbltranssubtype.transid and transsubid = tbltranssubtype.transsubid);";
            if (DB.tblLanguages.Where(p => p.TenentID == Tenent).Count() == 0)//
                Str += "INSERT INTO [dbo].[tblLanguage]([TenentID],[MYCONLOCID],[COUNTRYID],[LangName1],[LangName2],[LangName3],[CULTUREOCDE],[ACTIVE],[REMARKS],[CRUP_ID],[Deleted],[UploadDate],[Uploadby],[SyncDate],[Syncby],[SynID])select " + Tenent + ",[MYCONLOCID],[COUNTRYID],[LangName1],[LangName2],[LangName3],[CULTUREOCDE],[ACTIVE],[REMARKS],[CRUP_ID],[Deleted],[UploadDate],[Uploadby],[SyncDate],[Syncby],[SynID] from tblLanguage where TenentID=" + 0 + ";";
            if (DB.tblsetupPurchases.Where(p => p.TenentID == Tenent).Count() == 0)
                Str += "INSERT INTO [dbo].[tblsetupPurchase]([TenentID],[locationID],[module],[DeliveryLocation],[BottomTagLine],[CompniID],[LastClosePeriod],[CurrentPeriod],[PaymentDays],[ReminderDays],[AcceptWarantee],[AutoGeneratePO],[AutoGenerateGRN],[transid2],[transsubid2],[transid1],[transsubid1],[Created],[DateTime],[Active],[Deleted]) select " + Tenent + ",[locationID],[module],[DeliveryLocation],[BottomTagLine],[CompniID],[LastClosePeriod],[CurrentPeriod],[PaymentDays],[ReminderDays],[AcceptWarantee],[AutoGeneratePO],[AutoGenerateGRN],[transid2],[transsubid2],[transid1],[transsubid1],[Created],[DateTime],[Active],[Deleted] from tblsetupPurchase where TenentID = " + FromTID + ";";
            if (DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == Tenent && p.COMPNAME1.Contains("Cash")).Count() == 0)
            {
                Comp = DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == Tenent).Count() > 0 ? Convert.ToInt32(DB.TBLCOMPANYSETUPs.Where(p => p.TenentID == Tenent).Max(p => p.COMPID) + 1) : 1;
                Str += "INSERT INTO [dbo].[TBLCOMPANYSETUP]([TenentID],[COMPID],[OldCOMPid],[PHYSICALLOCID],[COMPNAME1],[COMPNAME2],[COMPNAME3],[BirthDate],[CivilID],[EMAIL],[EMAIL1],[EMAIL2],[ITMANAGER],[ADDR1],[ADDR2],[CITY],[STATE],[POSTALCODE],[ZIPCODE],[MYCONLOCID],[MYPRODID],[COUNTRYID],[BUSPHONE1],[BUSPHONE2],[BUSPHONE3],[BUSPHONE4],[MOBPHONE],[FAX],[FAX1],[FAX2],[PRIMLANGUGE],[WEBPAGE],[ISMINISTRY],[ISSMB],[ISCORPORATE],[INHAWALLY],[SALER],[BUYER],[SALEDEPROD],[EMAISUB],[EMAILSUBDATE],[PRODUCTDEALIN],[REMARKS],[Keyword],[COMPANYID],[BUSID],[MYCATSUBID],[COMPNAME],[COMPNAMEO],[COMPNAMECH],[Active],[CRUP_ID],[CUSERID],[CPASSWRD],[USERID],[ENTRYDATE],[ENTRYTIME],[UPDTTIME],[Approved],[CompanyType],[Images],[BARCODE],[Avtar],[Reload],[datasource],[PACI],[Marketting],[UploadDate],[Uploadby],[SyncDate],[Syncby],[SynID]) VALUES (" + Tenent + "," + Comp + ",0 ,'KWT','Cash','Cash' ,'Cash',NULL,'' ,'','','' ,'0','','' ,'1','1902','' ,'',0,0 ,126,'','' ,'','','' ,'','','' ,'1','',0 ,0,0,0 ,0,0,0 ,0,NULL,'' ,'','',0 ,0,0,'' ,'','','Y' ,0,'','' ,'',NULL,NULL ,NULL,0,'82005' ,NULL,'','' ,0,0,'' ,'',NULL,'' ,NULL,'',0);";
            }
            else
            {
                Comp = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == Tenent && p.COMPNAME1.Contains("Cash")).COMPID;
            }
            Str += "INSERT INTO [dbo].[tblsetupsalesh]([TenentID],[locationID],[transid],[transsubid],[module],[DeliveryLocation],[CompniID],[LastClosePeriod],[CurrentPeriod],[PaymentDays],[ReminderDays],[AcceptWarantee],[DescWithWarantee],[DescWithSerial],[DescWithColor],[AllowMinusQty],[HeaderLine],[TagLine],[BottomTagLine],[PaymentDetails],[TCQuotation],[IntroLetter],[COUNTRYID],[SalesAdminID],[CRUP_ID],[InvoicePrintURL],[DeliveryPrintURL],[CounterName],[EmployeeId],[DeftCoustomer],[Created],[DateTime],[Active],[Deleted]) select " + Tenent + ",[locationID],[transid],[transsubid],[module],[DeliveryLocation],[CompniID],[LastClosePeriod],[CurrentPeriod],[PaymentDays],[ReminderDays],[AcceptWarantee],[DescWithWarantee],[DescWithSerial],[DescWithColor],[AllowMinusQty],[HeaderLine],[TagLine],[BottomTagLine],[PaymentDetails],[TCQuotation],[IntroLetter],[COUNTRYID],[SalesAdminID],[CRUP_ID],[InvoicePrintURL],[DeliveryPrintURL],[CounterName],[EmployeeId]," + Comp + ",[Created],[DateTime],[Active],[Deleted] from [tblsetupsalesh] where TenentID=" + FromTID + " and transsubid not in ( SELECT transsubid from tblsetupsalesh  WHERE TenentID=" + Tenent + " and locationID=1 and transid = tblsetupsalesh.transid and transsubid = tblsetupsalesh.transsubid);";
            if (DB.tblSetupInventories.Where(p => p.TenentID == Tenent).Count() == 0)
            {
                Str += "INSERT INTO [dbo].[tblSetupInventory]([TenentID],[locationID],[TransferOutTransType],[TransferOutTransSubType],[TransferInTransType],[TransferInTransSubType],[ConsumeTransType],[ConsumeTransSubType],[transidOut],[transsubidOut],[transidin],[transsubidIn],[transidConsume],[transsubidConsume],[MYSYSNAMEOut],[MYSYSNAMEIn],[StockTeking],[StockTakingPeriodBegin],[StockTakingPeriodEnd],[StockTakingTransTypeIn],[StockTakingTransTypeOut],[StockTakingTransSubTypeIn],[StockTakingTransSubTypeOut],[StockTakingtransidInLast],[StockTakingtransidOutLast],[DefaultCUSTVENDID],[Created],[DateTime],[Active],[Deleted]) VALUES (" + Tenent + ",1,'Transfer Notes - Out','Transfer Notes - Out','Transfer Notes - In','Transfer Notes - In','Transfer Notes - Consume','Transfer Notes - Consume',21,221,11,111,31,331,'IC','IC',NULL,NULL,NULL,'In StockTaking','Out StockTaking','In StockTaking','Out StockTaking',301,401," + Comp + ",NULL,NULL,NULL,NULL);";
            }
            Str += "INSERT INTO [dbo].[TBLSYSTEMS]([TenentID],[SystemID],[MYSYSNAME],[SYSDESC1],[SYSDESC2],[SYSDESC3],[SHORTNAME],[REMARKS],[STARTDATE],[CRUP_ID],[ACTIVE],[SYSDESC],[SYSDESCO],[SYSDESCCH]) SELECT " + Tenent + ",[SystemID],[MYSYSNAME],[SYSDESC1],[SYSDESC2],[SYSDESC3],[SHORTNAME],[REMARKS],[STARTDATE],[CRUP_ID],[ACTIVE],[SYSDESC],[SYSDESCO],[SYSDESCCH] from TBLSYSTEMS where TenentID=" + FromTID + " and SystemID not in(select SystemID from TBLSYSTEMS where TenentID=" + Tenent + " and SystemID=TBLSYSTEMS.SystemID);";
            Str += "INSERT INTO [dbo].[ICEXTRACOST]([TenentID],[OVERHEADID],[OHNAME1],[OHNAME2],[OHNAME3],[ACCOUNTID],[Active],[CRUP_ID],[OHNAME],[OHNAMEO]) Select " + Tenent + ",[OVERHEADID],[OHNAME1],[OHNAME2],[OHNAME3],[ACCOUNTID],[Active],[CRUP_ID],[OHNAME],[OHNAMEO] from ICEXTRACOST where TenentID=" + FromTID + " and OVERHEADID not in(select OVERHEADID from ICEXTRACOST where TenentID=" + Tenent + " and OVERHEADID=ICEXTRACOST.OVERHEADID);";
            Str += "INSERT INTO [dbo].[ICUOM]([TenentID],[UOM],[UOMNAMESHORT],[UOMNAME1],[UOMNAME2],[UOMNAME3],[REMARKS],[CRUP_ID],[Active],[UOMNAME],[UOMNAMEO],[UOM_TYPE],[UploadDate],[Uploadby],[SyncDate],[Syncby],[SynID]) Select " + Tenent + ",[UOM],[UOMNAMESHORT],[UOMNAME1],[UOMNAME2],[UOMNAME3],[REMARKS],[CRUP_ID],[Active],[UOMNAME],[UOMNAMEO],[UOM_TYPE],[UploadDate],[Uploadby],[SyncDate],[Syncby],[SynID] From ICUOM Where TenentID=0 and UOM_TYPE='POS' and UOM not in(select UOM from ICUOM where TenentID=" + Tenent + " and UOM=ICUOM.UOM);";
            if (DB.Win_tbl_customer.Where(p => p.TenentID == Tenent && p.ID == 1).Count() == 0)
                Str += "INSERT INTO [dbo].[Win_tbl_customer]([TenentID],[ID],[Name],[EmailAddress],[Phone],[Address],[City],[PeopleType],[UploadDate],[Uploadby],[SynID],[NameArabic]) VALUES (" + Tenent + ",1,'Gest','Gest@gmail.com','12345678','Kuwait','Kuwait','Customer','10/12/2018  00:00:00 AM','Parimal',9,'غيست');";
            if (DB.Win_tbl_orderWay_Maintenance.Where(p => p.TenentID == Tenent).Count() == 0)
            {
                Str += "INSERT INTO [dbo].[Win_tbl_orderWay_Maintenance]([TenentID],[ID],[OrderWayID],[Name1],[Name2],[Commission_per],[Commission_Amount],[DeliveryCharges],[Paid_Commission],[Pending_Commission]) VALUES (" + Tenent + ",1,'Walk In','Walk In','Walk In',0,0,0,0,0);";
                Str += "INSERT INTO [dbo].[Win_tbl_orderWay_Maintenance]([TenentID],[ID],[OrderWayID],[Name1],[Name2],[Commission_per],[Commission_Amount],[DeliveryCharges],[Paid_Commission],[Pending_Commission]) VALUES (" + Tenent + ",2,'Talabat','Talabat','Talabat',0,0,0,0,0);";
                Str += "INSERT INTO [dbo].[Win_tbl_orderWay_Maintenance]([TenentID],[ID],[OrderWayID],[Name1],[Name2],[Commission_per],[Commission_Amount],[DeliveryCharges],[Paid_Commission],[Pending_Commission]) VALUES (" + Tenent + ",3,'Carriage','Carriage','Carriage',0,0,0,0,0);";
                Str += "INSERT INTO [dbo].[Win_tbl_orderWay_Maintenance]([TenentID],[ID],[OrderWayID],[Name1],[Name2],[Commission_per],[Commission_Amount],[DeliveryCharges],[Paid_Commission],[Pending_Commission]) VALUES (" + Tenent + ",4,'Home Delivery 1','Home Delivery 1','Home Delivery 1',0,0,0,0,0);";
                Str += "INSERT INTO [dbo].[Win_tbl_orderWay_Maintenance]([TenentID],[ID],[OrderWayID],[Name1],[Name2],[Commission_per],[Commission_Amount],[DeliveryCharges],[Paid_Commission],[Pending_Commission]) VALUES (" + Tenent + ",5,'Home Delivery 2','Home Delivery 2','Home Delivery 2',0,0,0,0,0);";
            }
            if (Str != "")
            {
                command2 = new SqlCommand(Str, con);
                con.Open();
                command2.ExecuteReader();
                con.Close();
            }
            //Server.Transfer("http://liveme.erp53.com/ACM/Login.aspx?RDNAD=900@MFtest@test@1");
            //HttpContext.Current.RewritePath("http://liveme.erp53.com/ACM/Login.aspx?RDNAD=900@MFtest@test@1");
        }
        public static string GetMACAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {

                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet) //&& nic.OperationalStatus == OperationalStatus.Up
                {
                    return nic.GetPhysicalAddress().ToString();

                    
                    
                }
            }
            return null;
        }

        public List<Database.TBLPRODUCT> getDataTBLPRODUCT(int TID)//haresh
        {
            CallEntities DB1 = new CallEntities();
            List<Database.TBLPRODUCT> List = new List<Database.TBLPRODUCT>();
            var objChache = DB.Cache_Mst.Single(p => p.TableName == "TBLPRODUCT");
            if (objChache.IsCache == false)
            {
                if (System.Web.HttpContext.Current.Cache["TBLPRODUCT"] != null)
                    List = ((List<Database.TBLPRODUCT>)System.Web.HttpContext.Current.Cache["TBLPRODUCT"]).ToList();
                else
                {
                    List = DB.TBLPRODUCTs.Where(p => p.ACTIVE == "1").OrderBy(m => m.ProdName1).ToList();
                    System.Web.HttpContext.Current.Cache["TBLPRODUCT"] = List;
                    objChache.IsCache = false;
                    DB.SaveChanges();
                }
            }
            else
            {
            List = DB1.TBLPRODUCTs.Where(p => p.ACTIVE == "1" && p.TenentID == TID).OrderBy(m => m.ProdName1).ToList();
            System.Web.HttpContext.Current.Cache["TBLPRODUCT"] = List;
            objChache.IsCache = false;
            DB.SaveChanges();
            }
            return List;
        }

    }
}