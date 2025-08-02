using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Database;
namespace Web.CRM.Class
{
    public static class AddReference
    {
        public static void InsertDataRefTable(int TenentID,string ReferenceName,int Type)
        {
            Database.CallEntities DB = new Database.CallEntities();
            string RefSubType = Convert.ToString(Type);
            var exist = DB.REFTABLEs.Where(c => c.REFNAME1 == ReferenceName && c.REFSUBTYPE == RefSubType);
            if (exist.Count() <= 0)
            {
                Database.REFTABLE objtbl_REFTABLE = new Database.REFTABLE();
                objtbl_REFTABLE.TenentID = TenentID;
                objtbl_REFTABLE.REFID = DB.REFTABLEs.Count() > 0 ? Convert.ToInt32(DB.REFTABLEs.Max(p => p.REFID) + 1) : 1;
                objtbl_REFTABLE.REFSUBTYPE = RefSubType;
                objtbl_REFTABLE.REFTYPE = "CRM";
                objtbl_REFTABLE.SHORTNAME = ReferenceName;
                objtbl_REFTABLE.REFNAME1 = ReferenceName;
                objtbl_REFTABLE.REFNAME2 = ReferenceName;
                objtbl_REFTABLE.REFNAME3 = ReferenceName;
                objtbl_REFTABLE.Remarks = ReferenceName;
                objtbl_REFTABLE.ACTIVE = "Y";
                objtbl_REFTABLE.SWITCH3 = "99";
                objtbl_REFTABLE.SWITCH2 = "0";
                objtbl_REFTABLE.CRUP_ID = 1; //((Web.CRM.CRMMaster)Page.Master).WriteLog("From New Document for Reference,ID:" + objtbl_REFTABLE.REFID.ToString(), "From New Document for Reference,ID:" + objtbl_REFTABLE.REFID.ToString(), "REFTABLE", "");
                DB.REFTABLEs.AddObject(objtbl_REFTABLE);
                DB.SaveChanges();
                // ((DMSMaster)this.Page.Master).BindListFromPopup(((ContentPlaceHolder)this.Parent), Database.Common.PopupOP.reference.ToString(), "drpRefNo123");            

                // Response.Redirect("NewDocument.aspx");
            }
        }
    }
}