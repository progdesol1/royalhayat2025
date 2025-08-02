using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Database;

namespace Web.ACM
{
    /// <summary>
    /// Summary description for WinRegistration
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WinRegistration : System.Web.Services.WebService
    {
        CallEntities DB = new CallEntities();
        [WebMethod]
        public bool WinReg(string Atntb, string Aunb, string Aupb, string AComb, string comp2, string comp3, string DefaultLA, string compAD, string compPH, string CompWeb, string ULast, string UAD, string UPH, string UBD, string UEM, string UT)
        {
            int Tenent = Convert.ToInt32(Classes.EncryptionClass.Decrypt(Atntb));
            string UName = Classes.EncryptionClass.Decrypt(Aunb);
            string UPass = Classes.EncryptionClass.Decrypt(Aupb);
            string Compname = Classes.EncryptionClass.Decrypt(AComb);
            string compname2 = Classes.EncryptionClass.Decrypt(comp2);
            string compname3 = Classes.EncryptionClass.Decrypt(comp3);
            string CommanDefauLANG = Classes.EncryptionClass.Decrypt(DefaultLA);
            string CompAdd = Classes.EncryptionClass.Decrypt(compAD);
            string CompPhone = Classes.EncryptionClass.Decrypt(compPH);
            string CompWebside = Classes.EncryptionClass.Decrypt(CompWeb);
            string userLastName = Classes.EncryptionClass.Decrypt(ULast);
            string useradd = Classes.EncryptionClass.Decrypt(UAD);
            string usermobile = Classes.EncryptionClass.Decrypt(UPH);
            string userbirthdate = Classes.EncryptionClass.Decrypt(UBD);
            string useremail = Classes.EncryptionClass.Decrypt(UEM);
            string userusertype = Classes.EncryptionClass.Decrypt(UT);
            try
            {
                Classes.WinPOS.WinPOSUSER(Tenent, UName, UPass, Compname, compname2, compname3, CommanDefauLANG, CompAdd, CompPhone, CompWebside, userLastName, useradd, usermobile, userbirthdate, useremail, userusertype);

                Classes.WinPOS.Win_registation(Tenent, UName, UPass, Compname, compname2, compname3, CommanDefauLANG, CompAdd, CompPhone, CompWebside, userLastName, useradd, usermobile, userbirthdate, useremail, userusertype,true);
               

                return true;
            }
            catch
            {
                Classes.WinPOS.Win_registation(Tenent, UName, UPass, Compname, compname2, compname3, CommanDefauLANG, CompAdd, CompPhone, CompWebside, userLastName, useradd, usermobile, userbirthdate, useremail, userusertype, false);
                return false;
            }

        }
    }
}
