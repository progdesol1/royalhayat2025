using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Database;
namespace Web.CRM.Class.Class
{
    public static class ActionGlobal
    {

        public static void DatainsertActionMaster(int Type, int AType, string Title, string Description, int MyIDSelfID, int TID, int UID, int MyID, int LocationID, string SwitchTo, string SwitchFrom, string Switch3, int NoteID, int TicketID, int FileID, int EmailIDAction, int MemoID, DateTime? Date = null)
        {
            int MyID1 = 0, ID2 = 0;
            try
            {
                Database.CallEntities DB = new Database.CallEntities();
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    MyID1 = Convert.ToInt32(CampgainGlobal.GetCampgainMyID(Type, AType, MyID, NoteID, TicketID, FileID, EmailIDAction, MemoID));
                    ID2 = CampgainGlobal.UpdateCamapainTable(MyID1, Type, AType, MyID);
                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                {
                    MyID1 = Convert.ToInt32(LeadGlobal.GetLeadMyID(Type, AType, MyID, NoteID, TicketID, FileID, EmailIDAction, MemoID));
                    ID2 = LeadGlobal.UpdateLeadTable(MyID1, Type, AType, MyID);
                }
                else
                {
                    MyID1 = Convert.ToInt32(OppertunityGlobal.GetOppertunityMyID(Type, AType, MyID, NoteID, TicketID, FileID, EmailIDAction, MemoID));
                    ID2 = OppertunityGlobal.UpdateOppertunityTable(MyID1, Type, AType, MyID);
                }
                ISActionMaster obj_Action = new ISActionMaster();
                obj_Action.MyID = ID2;
                if (Date != null)
                    obj_Action.TransactionDate = Date;
                else
                    obj_Action.TransactionDate = DateTime.Now;
                obj_Action.TransactionStatus = "Y";
                obj_Action.LocationID = LocationID;
                obj_Action.Switch1To = SwitchTo;
                obj_Action.Switch2From = SwitchFrom;
                obj_Action.Switch3 = Switch3;
                obj_Action.MySerial = DB.ISActionMasters.Where(p => p.MyID == MyID1).Count() > 0 ? Convert.ToInt32(DB.ISActionMasters.Where(p => p.MyID == MyID1).Max(p => p.MySerial) + 1) : 1;
                obj_Action.ID = DB.ISActionMasters.Count() > 0 ? Convert.ToInt32(DB.ISActionMasters.Max(p => p.ID) + 1) : 1;
                obj_Action.TenantedID = TID;
                obj_Action.Type = Type;
                obj_Action.ActionType = AType;
                obj_Action.CreatedDate = DateTime.Now;
                obj_Action.Active = true;
                obj_Action.Deleted = true;
                obj_Action.Title = Title.ToString();
                obj_Action.Description = Description.ToString(); ;
                obj_Action.Enteredby = UID;
                if (AType == Convert.ToInt32(ActionMaster.ActionType.Task))
                {
                    obj_Action.MySelfSerialNumber = EmailIDAction;
                    obj_Action.MyID = MemoID;
                    obj_Action.MyIDSelf = MyIDSelfID;
                }
                DB.ISActionMasters.AddObject(obj_Action);
                DB.SaveChanges();
                int ID = Convert.ToInt32(obj_Action.ID);
                
            }
            catch (Exception ex)
            {

            }
        }
        public static int CRMDatainsertActionMaster(int Type, int AType, string Title, string Description, int MyIDSelfID, int TID, int UID, int MyID, int LocationID, string SwitchTo, string SwitchFrom, string Switch3, int NoteID, int TicketID, int FileID, int EmailIDAction, int MemoID, DateTime? Date = null)
        {
            int MyID1 = 0, ID2 = 0;
            try
            {
                Database.CallEntities DB = new Database.CallEntities();
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    MyID1 = Convert.ToInt32(CampgainGlobal.GetCampgainMyID(Type, AType, MyID, NoteID, TicketID, FileID, EmailIDAction, MemoID));
                    ID2 = CampgainGlobal.UpdateCamapainTable(MyID1, Type, AType, MyID);
                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                {
                    MyID1 = Convert.ToInt32(LeadGlobal.GetLeadMyID(Type, AType, MyID, NoteID, TicketID, FileID, EmailIDAction, MemoID));
                    ID2 = LeadGlobal.UpdateLeadTable(MyID1, Type, AType, MyID);
                }
                else
                {
                    MyID1 = Convert.ToInt32(OppertunityGlobal.GetOppertunityMyID(Type, AType, MyID, NoteID, TicketID, FileID, EmailIDAction, MemoID));
                    ID2 = OppertunityGlobal.UpdateOppertunityTable(MyID1, Type, AType, MyID);
                }
                ISActionMaster obj_Action = new ISActionMaster();
                obj_Action.MyID = ID2;
                if (Date != null)
                    obj_Action.TransactionDate = Date;
                else
                    obj_Action.TransactionDate = DateTime.Now;
                obj_Action.TransactionStatus = "Y";
                obj_Action.LocationID = LocationID;
                obj_Action.Switch1To = SwitchTo;
                obj_Action.Switch2From = SwitchFrom;
                obj_Action.Switch3 = Switch3;
                obj_Action.MySerial = DB.ISActionMasters.Where(p => p.MyID == MyID1).Count() > 0 ? Convert.ToInt32(DB.ISActionMasters.Where(p => p.MyID == MyID1).Max(p => p.MySerial) + 1) : 1;
                obj_Action.ID = DB.ISActionMasters.Count() > 0 ? Convert.ToInt32(DB.ISActionMasters.Max(p => p.ID) + 1) : 1;
                obj_Action.TenantedID = TID;
                obj_Action.Type = Type;
                obj_Action.ActionType = AType;
                obj_Action.CreatedDate = DateTime.Now;
                obj_Action.Active = true;
                obj_Action.Deleted = true;
                obj_Action.Title = Title.ToString();
                obj_Action.Description = Description.ToString(); ;
                obj_Action.Enteredby = UID;
                if (AType == Convert.ToInt32(ActionMaster.ActionType.Task))
                {
                    obj_Action.MySelfSerialNumber = EmailIDAction;
                    obj_Action.MyID = MemoID;
                    obj_Action.MyIDSelf = MyIDSelfID;
                }
                DB.ISActionMasters.AddObject(obj_Action);
                DB.SaveChanges();
                int ID = Convert.ToInt32(obj_Action.ID);
                return ID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}