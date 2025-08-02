using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.CRM.Class.Class
{
    public static class OppertunityGlobal
    {
        public static int UpdateOppertunityTable(int ID, int Type, int Atype, int MyEditID)
        {
            int MainID = 0;
            try
            {
                Database.CallEntities DB = new Database.CallEntities();
                int CamID = Convert.ToInt32(MyEditID);
                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == CamID).Count() > 0)
                    {
                        Database.tbl_Opportunity_Mst obj_Opp = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == CamID);
                        if (Atype == Convert.ToInt32(ActionMaster.ActionType.Note))
                        {
                            if (DB.tbl_Opportunity_Mst.Where(p => p.NoteID == null && p.OpportID == CamID).Count() > 0)
                            {
                                obj_Opp.NoteID = ID;
                                MainID = ID;
                                DB.SaveChanges();
                            }
                            else
                            {
                                MainID = Convert.ToInt32(obj_Opp.NoteID);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Email))
                        {
                            if (DB.tbl_Opportunity_Mst.Where(p => p.EmailIDAction == null && p.OpportID == CamID).Count() > 0)
                            {
                                obj_Opp.EmailIDAction = ID;
                                DB.SaveChanges();
                                MainID = ID;
                            }
                            else
                            {
                                MainID = Convert.ToInt32(obj_Opp.EmailIDAction);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.File))
                        {
                            if (DB.tbl_Opportunity_Mst.Where(p => p.FileID == null && p.OpportID == CamID).Count() > 0)
                            {
                                obj_Opp.FileID = ID;
                                DB.SaveChanges();
                                MainID = ID;
                            }
                            else
                            {
                                MainID = Convert.ToInt32(obj_Opp.FileID);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Ticket))
                        {
                            if (DB.tbl_Opportunity_Mst.Where(p => p.TicketID == null && p.OpportID == CamID).Count() > 0)
                            {
                                obj_Opp.TicketID = ID;
                                DB.SaveChanges();
                                MainID = ID;
                            }
                            else
                            {
                                MainID = Convert.ToInt32(obj_Opp.TicketID);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Memo))
                        {
                            if (DB.tbl_Opportunity_Mst.Where(p => p.MemoID == null && p.OpportID == CamID).Count() > 0)
                            {
                                obj_Opp.MemoID = ID;
                                DB.SaveChanges();
                                MainID = ID;
                            }
                            else
                            {
                                MainID = Convert.ToInt32(obj_Opp.MemoID);
                            }
                        }
                        else if(Atype == Convert.ToInt32(ActionMaster.ActionType.Appointment))
                        {
                            if (DB.tbl_Opportunity_Mst.Where(p => p.AppointmentID == null && p.OpportID == CamID).Count() > 0)
                            {
                                obj_Opp.AppointmentID = ID;
                                DB.SaveChanges();
                                MainID = ID;
                            }
                            else
                            {
                                MainID = Convert.ToInt32(obj_Opp.AppointmentID);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return MainID;

        }



        public static int GetOppertunityMyID(int Type, int Atype, int EditPageID, int NoteID, int TicketID, int FileID, int EmailIDAction, int MemoID)
        {
            int NewMyID = 0;
            try
            {
                Database.CallEntities DB = new Database.CallEntities();
                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == EditPageID).Count() > 0)
                    {
                        Database.tbl_Opportunity_Mst obj_Oppobj = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == EditPageID);
                        if (Atype == Convert.ToInt32(ActionMaster.ActionType.Note))
                        {
                            if (DB.tbl_Opportunity_Mst.Where(p => p.NoteID == null && p.OpportID == obj_Oppobj.OpportID).Count() > 0)
                            {
                                NewMyID = GetNewID();
                            }
                            else
                            {
                                NewMyID = Convert.ToInt32(obj_Oppobj.NoteID);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Email))
                        {
                            if (DB.tbl_Opportunity_Mst.Where(p => p.EmailIDAction == null && p.OpportID == obj_Oppobj.OpportID).Count() > 0)
                            {
                                NewMyID = GetNewID();
                            }
                            else
                            {
                                NewMyID = Convert.ToInt32(obj_Oppobj.EmailIDAction);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Ticket))
                        {
                            if (DB.tbl_Opportunity_Mst.Where(p => p.TicketID == null && p.OpportID == obj_Oppobj.OpportID).Count() > 0)
                            {
                                NewMyID = GetNewID();
                            }
                            else
                            {
                                NewMyID = Convert.ToInt32(obj_Oppobj.TicketID);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.File))
                        {
                            if (DB.tbl_Opportunity_Mst.Where(p => p.FileID == null && p.OpportID == obj_Oppobj.OpportID).Count() > 0)
                            {
                                NewMyID = GetNewID();
                            }
                            else
                            {
                                NewMyID = Convert.ToInt32(obj_Oppobj.FileID);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Memo))
                        {
                            if (DB.tbl_Opportunity_Mst.Where(p => p.MemoID == null && p.OpportID == obj_Oppobj.OpportID).Count() > 0)
                            {
                                NewMyID = GetNewID();
                            }
                            else
                            {
                                NewMyID = Convert.ToInt32(obj_Oppobj.MemoID);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Appointment))
                        {
                            if (DB.tbl_Opportunity_Mst.Where(p => p.AppointmentID == null && p.OpportID == obj_Oppobj.OpportID).Count() > 0)
                            {
                                NewMyID = GetAppointID();
                            }
                            else
                            {
                                NewMyID = Convert.ToInt32(obj_Oppobj.AppointmentID);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return NewMyID;
        }

        public static List<ISActionMaster> DisplayOppertunityNoteData(int Type, int AType, int UID, int CID, int TID, int LID)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.ISActionMaster> obj_action = new List<ISActionMaster>();
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Opportunity_Mst obj_OppList = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == CID && p.TenentID == TID);
                        if (obj_OppList.NoteID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Note))
                        {
                            obj_action = DB.ISActionMasters.Where(p => p.MyID == obj_OppList.NoteID && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();

                        }
                        if (obj_OppList.EmailIDAction != null && AType == Convert.ToInt32(ActionMaster.ActionType.Email))
                        {
                            obj_action = DB.ISActionMasters.Where(p => p.MyID == obj_OppList.EmailIDAction && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();

                        }
                        if (obj_OppList.MemoID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Memo))
                        {
                            obj_action = DB.ISActionMasters.Where(p => p.MyID == obj_OppList.MemoID && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();

                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return obj_action;
        }

        public static List<ISActionMaster> DisplayOppertunityMemoData(int Type, int AType, int UID, int CID, int TID, int LID)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.ISActionMaster> obj_action = new List<ISActionMaster>();
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Opportunity_Mst obj_CamList = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == CID && p.TenentID == TID);
                        if (obj_CamList.MemoID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Memo))
                        {
                            obj_action = DB.ISActionMasters.Where(p => p.MyID == obj_CamList.MemoID && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                        }
                    }
                }


            }
            catch (Exception ex)
            {

            }

            return obj_action;
        }

        public static List<ISActionMaster> DisplayOpprtutnityEmailData(int Type, int AType, int UID, int CID, int TID, int LID)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.ISActionMaster> obj_action = new List<ISActionMaster>();
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    if (DB.tbl_Campaign_Mst.Where(p => p.ID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Campaign_Mst obj_CamList = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == CID && p.TenentID == TID);

                        if (obj_CamList.EmailIDAction != null && AType == Convert.ToInt32(ActionMaster.ActionType.Email))
                        {
                            obj_action = DB.ISActionMasters.Where(p => p.MyID == obj_CamList.EmailIDAction && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                        }
                    }
                }


            }
            catch (Exception ex)
            {

            }

            return obj_action;
        }

        public static List<CRMMainActivity> DisplayTicketData(int Type, int AType, int UID, int CID, int TID, int LID, string UName)
        {
            Database.CallEntities DB = new Database.CallEntities();
         //   Database.CallEntities DB1 = new Database.CallEntities();
            List<Database.CRMMainActivity> obj_activity = new List<CRMMainActivity>();
            try
            {

                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Opportunity_Mst obj_CamList = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == CID && p.TenentID == TID);
                        if (obj_CamList.TicketID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Ticket))
                        {
                            string Type1 = Enum.GetName(typeof(ActionMaster.Type), Type);
                            string Atype1 = Enum.GetName(typeof(ActionMaster.ActionType), AType);
                            obj_activity = DB.CRMMainActivities.Where(p => p.MyID == obj_CamList.TicketID && p.USERCODE == UID && p.TenentID == TID).ToList();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return obj_activity;

        }
        public static List<Database.Appointment> DisplayoppappointData(int Type, int AType, int UID, int CID, int TID, int LID, string UName)
        {
            Database.CallEntities DB = new Database.CallEntities();
            //   Database.CallEntities DB1 = new Database.CallEntities();
            List<Database.Appointment> obj_activity = new List<Database.Appointment>();
            try
            {

                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Opportunity_Mst obj_CamList = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == CID && p.TenentID == TID);
                        if (obj_CamList.AppointmentID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Appointment))
                        {
                            string Type1 = Enum.GetName(typeof(ActionMaster.Type), Type);
                            string Atype1 = Enum.GetName(typeof(ActionMaster.ActionType), AType);
                            obj_activity = DB.Appointments.Where(p => p.TenentID == TID && p.MyID == obj_CamList.AppointmentID && p.Type == Type && p.ActionType == AType).ToList();//DB.CRMMainActivities.Where(p => p.MyID == obj_CamList.AppointmentID && p.USERCODE == UID && p.TenentID == TID).ToList();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return obj_activity;



        }
        public static int CountOppertunityNoteData(int Type, int AType, int UID, int CID, int TID, int LID)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.ISActionMaster> obj_action = new List<ISActionMaster>();
            int a = 0;
            try
            {

                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Opportunity_Mst obj_OppList = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == CID && p.TenentID == TID);
                        if (obj_OppList.NoteID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Note))
                        {
                            obj_action = DB.ISActionMasters.Where(p => p.MyID == obj_OppList.NoteID && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                            a = obj_action.Count();
                        }
                        if (obj_OppList.EmailIDAction != null && AType == Convert.ToInt32(ActionMaster.ActionType.Email))
                        {
                            obj_action = DB.ISActionMasters.Where(p => p.MyID == obj_OppList.EmailIDAction && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                            a = obj_action.Count();
                        }
                        if (obj_OppList.MemoID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Memo))
                        {
                            obj_action = DB.ISActionMasters.Where(p => p.MyID == obj_OppList.MemoID && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                            a = obj_action.Count();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return a;

        }

        public static int CountOppertunityTicketData(int Type, int AType, int UID, int CID, int TID, int LID, string UName)
        {
            Database.CallEntities DB = new Database.CallEntities();
         //   Database.CallEntities DB1 = new Database.CallEntities();
            List<Database.CRMMainActivity> obj_activity = new List<CRMMainActivity>();
            int b = 0;
            try
            {

                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Opportunity_Mst obj_CamList = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == CID && p.TenentID == TID);
                        if (obj_CamList.TicketID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Ticket))
                        {
                            string Type1 = Enum.GetName(typeof(ActionMaster.Type), Type);
                            string Atype1 = Enum.GetName(typeof(ActionMaster.ActionType), AType);
                            obj_activity = DB.CRMMainActivities.Where(p => p.MyID == obj_CamList.TicketID && p.USERCODE == UID && p.TenentID == TID).ToList();
                            b = obj_activity.Count();
                        }
                    }
                }
            }
            catch (Exception ex)
            { }

            return b;

        }
        public static int CountOppertunityAppointData(int Type, int AType, int UID, int CID, int TID, int LID, string UName)
        {
            Database.CallEntities DB = new Database.CallEntities();
            //   Database.CallEntities DB1 = new Database.CallEntities();
            List<Database.Appointment> obj_activity = new List<Database.Appointment>();
            int b = 0;
            try
            {

                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Opportunity_Mst obj_CamList = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == CID && p.TenentID == TID);
                        if (obj_CamList.AppointmentID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Appointment))
                        {
                            string Type1 = Enum.GetName(typeof(ActionMaster.Type), Type);
                            string Atype1 = Enum.GetName(typeof(ActionMaster.ActionType), AType);
                            obj_activity = DB.Appointments.Where(p => p.TenentID == TID && p.MyID == obj_CamList.AppointmentID && p.Type == Type && p.ActionType == AType).ToList();//DB.CRMMainActivities.Where(p => p.MyID == obj_CamList.AppointmentID && p.USERCODE == UID && p.TenentID == TID).ToList();
                            b = obj_activity.Count();
                        }
                    }
                }
            }
            catch (Exception ex)
            { }

            return b;

        }
        public static int GetNewID()
        {
            Database.CallEntities DB = new Database.CallEntities();
            int ID = 0;
            try
            {
                ID = DB.ISActionMasters.Count() > 0 ? Convert.ToInt32(DB.ISActionMasters.Max(p => p.MyID) + 1) : 1;
            }
            catch (Exception ex)
            { }

            return ID;
        }
        public static int GetAppointID()
        {
            Database.CallEntities DB = new Database.CallEntities();
            int ID = 0;
            try
            {
                ID = DB.Appointments.Count() > 0 ? Convert.ToInt32(DB.Appointments.Max(p => p.MyID) + 1) : 1;
            }
            catch (Exception ex)
            { }

            return ID;
        }

        public static List<tbl_DMSAttachmentMst> DisplayOppertunityFileData(int Type, int AType, int UID, int CID, int TID, int LID, string UName)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.tbl_DMSAttachmentMst> obj_File = new List<tbl_DMSAttachmentMst>();
            try
            {

                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Opportunity_Mst obj_CamList = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == CID && p.TenentID == TID);
                        if (obj_CamList.FileID != null && AType == Convert.ToInt32(ActionMaster.ActionType.File))
                        {
                            string Type1 = Enum.GetName(typeof(ActionMaster.Type), Type);
                            string Atype1 = Enum.GetName(typeof(ActionMaster.ActionType), AType);
                            obj_File = DB.tbl_DMSAttachmentMst.Where(p => p.AttachID == obj_CamList.FileID && p.CreatedBy == UID && p.ReferenceNo == Type && p.TenentID == TID).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return obj_File;

        }



        public static int CountOppertunityMemoData(int Type, int AType, int UID, int CID, int TID, int LID)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.ISActionMaster> obj_action = new List<ISActionMaster>();
            int a = 0;
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Opportunity_Mst obj_CamList = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == CID && p.TenentID == TID);
                       
                        if (obj_CamList.MemoID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Memo))
                        {
                            obj_action = DB.ISActionMasters.Where(p => p.MyID == obj_CamList.MemoID && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                            a = obj_action.Count();
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {

            }

            return a;

        }
        public static int CountOppertunityFileData(int Type, int AType, int UID, int CID, int TID, int LID, string UName)
        {
            Database.CallEntities DB = new Database.CallEntities();
            int d = 0;
            List<Database.tbl_DMSAttachmentMst> obj_File = new List<Database.tbl_DMSAttachmentMst>();
            try
            {

                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Opportunity_Mst obj_CamList = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == CID && p.TenentID == TID);
                        if (obj_CamList.FileID != null && AType == Convert.ToInt32(ActionMaster.ActionType.File))
                        {
                            string Type1 = Enum.GetName(typeof(ActionMaster.Type), Type);
                            string Atype1 = Enum.GetName(typeof(ActionMaster.ActionType), AType);
                            obj_File = DB.tbl_DMSAttachmentMst.Where(p => p.AttachID == obj_CamList.FileID && p.CreatedBy == UID && p.ReferenceNo == Type && p.TenentID == TID).ToList();
                            d = obj_File.Count();
                        }
                    }
                }
            }
            catch (Exception ex)
            { }

            return d;

        }

        public static int CountOppertunityEmailData(int Type, int AType, int UID, int CID, int TID, int LID)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.ISActionMaster> obj_action = new List<ISActionMaster>();
            int a = 0;
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                {
                    if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Opportunity_Mst obj_CamList = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == CID && p.TenentID == TID);

                        if (obj_CamList.EmailIDAction != null && AType == Convert.ToInt32(ActionMaster.ActionType.Email))
                        {
                            obj_action = DB.ISActionMasters.Where(p => p.MyID == obj_CamList.EmailIDAction && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                            a = obj_action.Count();
                        }

                    }
                }

            }
            catch (Exception ex)
            {

            }

            return a;

        }
    }
}