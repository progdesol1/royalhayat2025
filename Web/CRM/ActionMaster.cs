using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Database;
namespace Web.CRM
{
    public static class ActionMaster
    {

        public enum ActionType
        {
            Note = 1,
            Task = 2,
            Memo = 3,
            Ticket = 4,
            File = 5,
            Email = 6,
            Appointment = 7
        }
        public enum Type
        {
            Opprtutnity = 1,
            Lead = 2,
            Campaign = 3
        }
        public enum ActionStatus
        {
            Opprtutnity = 1,
            Lead = 2,
            Campaign = 3
        }

      

    }
}