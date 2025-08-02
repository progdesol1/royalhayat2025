using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.CRM.Class
{
    public static class CampgainGlobal
    {
        public static int UpdateCamapainTable(int ID, int Type, int Atype, int MyEditID)
        {
            int MainID = 0;
            try
            {
                Database.CallEntities DB = new Database.CallEntities();
                int CamID = Convert.ToInt32(MyEditID);
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    if (DB.tbl_Campaign_Mst.Where(p => p.ID == CamID).Count() > 0)
                    {
                        Database.tbl_Campaign_Mst obj_Campaign = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == CamID);
                        if (Atype == Convert.ToInt32(ActionMaster.ActionType.Note))
                        {
                            if (DB.tbl_Campaign_Mst.Where(p => p.NoteID == null && p.ID == obj_Campaign.ID).Count() > 0)
                            {
                                obj_Campaign.NoteID = ID;
                                DB.SaveChanges();
                                MainID = ID;
                            }
                            else
                            {
                                MainID = Convert.ToInt32(obj_Campaign.NoteID);
                            }

                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Email))
                        {
                            if (DB.tbl_Campaign_Mst.Where(p => p.EmailIDAction == null && p.ID == obj_Campaign.ID).Count() > 0)
                            {
                                obj_Campaign.EmailIDAction = ID;
                                DB.SaveChanges();
                                MainID = ID;
                            }
                            else
                            {
                                MainID = Convert.ToInt32(obj_Campaign.EmailIDAction);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.File))
                        {
                            if (DB.tbl_Campaign_Mst.Where(p => p.FileID == null && p.ID == obj_Campaign.ID).Count() > 0)
                            {
                                obj_Campaign.FileID = ID;
                                DB.SaveChanges();
                                MainID = ID;
                            }
                            else
                            {
                                MainID = Convert.ToInt32(obj_Campaign.FileID);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Ticket))
                        {
                            if (DB.tbl_Campaign_Mst.Where(p => p.TicketID == null && p.ID == obj_Campaign.ID).Count() > 0)
                            {
                                int a = DB.CRMMainActivities.Count() > 0 ? Convert.ToInt32(DB.CRMMainActivities.Max(p => p.MyID) + 1) : 1;
                                obj_Campaign.TicketID = a;
                                DB.SaveChanges();
                                MainID = a;
                            }
                            else
                            {
                                MainID = Convert.ToInt32(obj_Campaign.TicketID);
                            }

                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Memo))
                        {
                            if (DB.tbl_Campaign_Mst.Where(p => p.MemoID == null && p.ID == obj_Campaign.ID).Count() > 0)
                            {
                                obj_Campaign.MemoID = ID;
                                DB.SaveChanges();
                                MainID = ID;
                            }
                            else
                            {
                                MainID = Convert.ToInt32(obj_Campaign.MemoID);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Appointment))
                        {
                            if (DB.tbl_Campaign_Mst.Where(p => p.AppointmentID == null && p.ID == obj_Campaign.ID).Count() > 0)
                            {
                                obj_Campaign.AppointmentID = ID;
                                DB.SaveChanges();
                                MainID = ID;
                            }
                            else
                            {
                                MainID = Convert.ToInt32(obj_Campaign.AppointmentID);
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


        public static int GetCampgainMyID(int Type, int Atype, int EditPageID, int NoteID, int TicketID, int FileID, int EmailIDAction, int MemoID)
        {
            int NewMyID = 0;
            try
            {
                Database.CallEntities DB = new Database.CallEntities();

                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    if (DB.tbl_Campaign_Mst.Where(p => p.ID == EditPageID).Count() > 0)
                    {
                        Database.tbl_Campaign_Mst obj_Camapignobj = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == EditPageID);
                        if (Atype == Convert.ToInt32(ActionMaster.ActionType.Note))
                        {
                            if (DB.tbl_Campaign_Mst.Where(p => p.NoteID == null && p.ID == obj_Camapignobj.ID).Count() > 0)
                            {
                                NewMyID = GetCampgainNewID();
                            }
                            else
                            {
                                NewMyID = Convert.ToInt32(obj_Camapignobj.NoteID);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Email))
                        {
                            if (DB.tbl_Campaign_Mst.Where(p => p.EmailIDAction == null && p.ID == obj_Camapignobj.ID).Count() > 0)
                            {
                                NewMyID = GetCampgainNewID();
                            }
                            else
                            {
                                NewMyID = Convert.ToInt32(obj_Camapignobj.EmailIDAction);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Ticket))
                        {
                            if (DB.tbl_Campaign_Mst.Where(p => p.TicketID == null && p.ID == obj_Camapignobj.ID).Count() > 0)
                            {
                                NewMyID = GetCampgainNewID();
                            }
                            else
                            {
                                NewMyID = Convert.ToInt32(obj_Camapignobj.TicketID);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.File))
                        {
                            if (DB.tbl_Campaign_Mst.Where(p => p.FileID == null && p.ID == obj_Camapignobj.ID).Count() > 0)
                            {
                                NewMyID = GetCampgainNewID();
                            }
                            else
                            {
                                NewMyID = Convert.ToInt32(obj_Camapignobj.FileID);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Memo))
                        {
                            if (DB.tbl_Campaign_Mst.Where(p => p.MemoID == null && p.ID == obj_Camapignobj.ID).Count() > 0)
                            {
                                NewMyID = GetCampgainNewID();
                            }
                            else
                            {
                                NewMyID = Convert.ToInt32(obj_Camapignobj.MemoID);
                            }
                        }
                        else if (Atype == Convert.ToInt32(ActionMaster.ActionType.Appointment))
                        {
                            if (DB.tbl_Campaign_Mst.Where(p => p.AppointmentID == null && p.ID == obj_Camapignobj.ID).Count() > 0)
                            {
                                NewMyID = GetAppointID();
                            }
                            else
                            {
                                NewMyID = Convert.ToInt32(obj_Camapignobj.AppointmentID);
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


        public static int GetCampgainNewID()
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

        public static List<ISActionMaster> DisplayCampgainNoteData(int Type, int AType, int UID, int CID, int TID, int LID)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.ISActionMaster> obj_action = new List<ISActionMaster>();
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    if (DB.tbl_Campaign_Mst.Where(p => p.ID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Campaign_Mst obj_CamList = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == CID && p.TenentID == TID);
                        if (obj_CamList.NoteID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Note))
                        {
                            obj_action = DB.ISActionMasters.Where(p => p.MyID == obj_CamList.NoteID && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                        }
                       
                    }
                }


            }
            catch (Exception ex)
            {

            }

            return obj_action;
        }

        public static List<ISActionMaster> DisplayCampgainMemoData(int Type, int AType, int UID, int CID, int TID, int LID)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.ISActionMaster> obj_action = new List<ISActionMaster>();
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    if (DB.tbl_Campaign_Mst.Where(p => p.ID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Campaign_Mst obj_CamList = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == CID && p.TenentID == TID);                       
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
        public static List<Database.Appointment> DisplayCampgainappointData(int Type, int AType, int UID, int CID, int TID, int LID)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.Appointment> obj_action = new List<Database.Appointment>();
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    if (DB.tbl_Campaign_Mst.Where(p => p.ID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Campaign_Mst obj_CamList = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == CID && p.TenentID == TID);
                        if (obj_CamList.AppointmentID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Appointment))
                        {
                            obj_action = DB.Appointments.Where(p => p.MyID == obj_CamList.AppointmentID && p.Type == Type && p.ActionType == AType && p.TenentID == TID).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return obj_action;
        }
        public static List<ISActionMaster> DisplayCampgainEmailData(int Type, int AType, int UID, int CID, int TID, int LID)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.ISActionMaster> obj_action = new List<ISActionMaster>();
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
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

        public static List<CRMMainActivity> DisplayCampgainTicketData(int Type, int AType, int UID, int CID, int TID, int LID, string UName)
        {
           // int UID1 = Convert.ToInt32(((ERP_WEB_USER_MST)Session["USER"]).USER_ID);
            Database.CallEntities DB = new Database.CallEntities();
         //   Database.CallEntities DB1 = new Database.CallEntities();
            List<Database.CRMMainActivity> obj_activity = new List<CRMMainActivity>();
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    if (DB.tbl_Campaign_Mst.Where(p => p.ID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Campaign_Mst obj_CamList = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == CID && p.TenentID == TID);
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

        public static int CountCampgainNoteData(int Type, int AType, int UID, int CID, int TID, int LID)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.ISActionMaster> obj_action = new List<ISActionMaster>();
            int a = 0;
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    if (DB.tbl_Campaign_Mst.Where(p => p.ID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Campaign_Mst obj_CamList = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == CID && p.TenentID == TID);
                        if (obj_CamList.NoteID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Note))
                        {
                            obj_action = DB.ISActionMasters.Where(p => p.MyID == obj_CamList.NoteID && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                            a = obj_action.Count();
                        }
                        //if (obj_CamList.EmailIDAction != null && AType == Convert.ToInt32(ActionMaster.ActionType.Email))
                        //{
                        //    obj_action = DB.ISActionMaster.Where(p => p.MyID == obj_CamList.EmailIDAction && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                        //    a = obj_action.Count();
                        //}
                        //if (obj_CamList.MemoID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Memo))
                        //{
                        //    obj_action = DB.ISActionMaster.Where(p => p.MyID == obj_CamList.MemoID && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                        //    a = obj_action.Count();
                        //}
                    }
                }
                //else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                //{
                //    if (DB.tbl_Lead_Mst.Where(p => p.ID == CID && p.TenentID == TID).Count() > 0)
                //    {
                //        Database.tbl_Lead_Mst obj_LeadList = DB.tbl_Lead_Mst.SingleOrDefault(p => p.ID == CID && p.TenentID == TID);
                //        if (obj_LeadList.NoteID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Note))
                //        {
                //            obj_action = DB.ISActionMaster.Where(p => p.MyID == obj_LeadList.NoteID && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                //            a = obj_action.Count();
                //        }
                //        if (obj_LeadList.EmailIDAction != null && AType == Convert.ToInt32(ActionMaster.ActionType.Email))
                //        {
                //            obj_action = DB.ISActionMaster.Where(p => p.MyID == obj_LeadList.EmailIDAction && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                //            a = obj_action.Count();
                //        }
                //        if (obj_LeadList.MemoID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Memo))
                //        {
                //            obj_action = DB.ISActionMaster.Where(p => p.MyID == obj_LeadList.MemoID && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                //            a = obj_action.Count();
                //        }
                //    }
                //}
                //else if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
                //{
                //    if (DB.tbl_Opportunity_Mst.Where(p => p.OpportID == CID && p.TenentID == TID).Count() > 0)
                //    {
                //        Database.tbl_Opportunity_Mst obj_OppList = DB.tbl_Opportunity_Mst.SingleOrDefault(p => p.OpportID == CID && p.TenentID == TID);
                //        if (obj_OppList.NoteID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Note))
                //        {
                //            obj_action = DB.ISActionMaster.Where(p => p.MyID == obj_OppList.NoteID && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                //            a = obj_action.Count();
                //        }
                //        if (obj_OppList.EmailIDAction != null && AType == Convert.ToInt32(ActionMaster.ActionType.Email))
                //        {
                //            obj_action = DB.ISActionMaster.Where(p => p.MyID == obj_OppList.EmailIDAction && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                //            a = obj_action.Count();
                //        }
                //        if (obj_OppList.MemoID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Memo))
                //        {
                //            obj_action = DB.ISActionMaster.Where(p => p.MyID == obj_OppList.MemoID && p.Enteredby == UID && p.Type == Type && p.ActionType == AType && p.TenantedID == TID).ToList();
                //            a = obj_action.Count();
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {

            }

            return a;

        }

        public static int CountCampgainMemoData(int Type, int AType, int UID, int CID, int TID, int LID)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.ISActionMaster> obj_action = new List<ISActionMaster>();
            int a = 0;
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    if (DB.tbl_Campaign_Mst.Where(p => p.ID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Campaign_Mst obj_CamList = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == CID && p.TenentID == TID);
                       
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
        public static int CountCampgainAppointData(int Type, int AType, int UID, int CID, int TID, int LID)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.Appointment> obj_action = new List<Database.Appointment>();
            int a = 0;
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    if (DB.tbl_Campaign_Mst.Where(p => p.ID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Campaign_Mst obj_CamList = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == CID && p.TenentID == TID);

                        if (obj_CamList.AppointmentID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Appointment))
                        {
                            obj_action = DB.Appointments.Where(p => p.MyID == obj_CamList.AppointmentID && p.Type == Type && p.ActionType == AType && p.TenentID == TID).ToList();
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
        public static int CountCampgainTicketData(int Type, int AType, int UID, int CID, int TID, int LID, string UName)
        {
            Database.CallEntities DB = new Database.CallEntities();
         //   Database.CallEntities DB1 = new Database.CallEntities();
            List<Database.CRMMainActivity> obj_activity = new List<CRMMainActivity>();
            int b = 0;
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    if (DB.tbl_Campaign_Mst.Where(p => p.ID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Campaign_Mst obj_CamList = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == CID && p.TenentID == TID);
                        if (obj_CamList.TicketID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Ticket))
                        {
                            string Type1 = Enum.GetName(typeof(ActionMaster.Type), Type);
                            string Atype1 = Enum.GetName(typeof(ActionMaster.ActionType), AType);
                            obj_activity = DB.CRMMainActivities.Where(p => p.MyID == obj_CamList.TicketID && p.USERCODE == UID && p.TenentID == TID).ToList();
                            b = obj_activity.Count();
                        }
                    }
                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Lead))
                {
                    if (DB.tbl_Lead_Mst.Where(p => p.ID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Lead_Mst obj_LeadList = DB.tbl_Lead_Mst.SingleOrDefault(p => p.ID == CID && p.TenentID == TID);
                        if (obj_LeadList.TicketID != null && AType == Convert.ToInt32(ActionMaster.ActionType.Ticket))
                        {
                            string Type1 = Enum.GetName(typeof(ActionMaster.Type), Type);
                            string Atype1 = Enum.GetName(typeof(ActionMaster.ActionType), AType);
                            obj_activity = DB.CRMMainActivities.Where(p => p.MyID == obj_LeadList.TicketID && p.TenentID == TID).ToList();
                            b = obj_activity.Count();
                        }
                    }
                }
                else if (Type == Convert.ToInt32(ActionMaster.Type.Opprtutnity))
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


        public static List<tbl_DMSAttachmentMst> DisplayCampgainFileData(int Type, int AType, int UID, int CID, int TID, int LID, string UName)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.tbl_DMSAttachmentMst> obj_File = new List<tbl_DMSAttachmentMst>();
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    if (DB.tbl_Campaign_Mst.Where(p => p.ID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Campaign_Mst obj_CamList = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == CID && p.TenentID == TID);
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


        public static int CountCampgainFileData(int Type, int AType, int UID, int CID, int TID, int LID, string UName)
        {
            Database.CallEntities DB = new Database.CallEntities();
            int d = 0;
            List<Database.tbl_DMSAttachmentMst> obj_File = new List<Database.tbl_DMSAttachmentMst>();
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    if (DB.tbl_Campaign_Mst.Where(p => p.ID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Campaign_Mst obj_CamList = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == CID && p.TenentID == TID);
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


        public static int CountCampgainEmailData(int Type, int AType, int UID, int CID, int TID, int LID)
        {
            Database.CallEntities DB = new Database.CallEntities();
            List<Database.ISActionMaster> obj_action = new List<ISActionMaster>();
            int a = 0;
            try
            {
                if (Type == Convert.ToInt32(ActionMaster.Type.Campaign))
                {
                    if (DB.tbl_Campaign_Mst.Where(p => p.ID == CID && p.TenentID == TID).Count() > 0)
                    {
                        Database.tbl_Campaign_Mst obj_CamList = DB.tbl_Campaign_Mst.SingleOrDefault(p => p.ID == CID && p.TenentID == TID);
                       
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