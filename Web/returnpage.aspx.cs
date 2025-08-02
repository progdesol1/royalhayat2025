using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
namespace Web
{
    public partial class returnpage : System.Web.UI.Page
    {
        ERPEntities DB = new ERPEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {
                    FacebookLogin();
                }
            }

        }
        public bool FacebookLogin()
        {
            FacebookLoginHelper helper = new FacebookLoginHelper();

            Web.Common.FacebookVariable obj = new Web.Common.FacebookVariable();
            Dictionary<string, string> dicAccessToken = helper.GetAccessToken(Request["code"].ToString(), obj.Scope, obj.RedirectUrl);
            string a = dicAccessToken["access_token"];
            var fbClient = new Facebook.FacebookClient(dicAccessToken["access_token"]);
            dynamic me = fbClient.Get("me");
            string UEmail = me["email"];
            List<Eco_TBLCONTACT> Memberlist = (from item in DB.Eco_TBLCONTACT where item.EMAIL1 == UEmail select item).ToList();
            if (Memberlist.Count() < 1)
            {
                Eco_TBLCONTACT model = new Eco_TBLCONTACT();

              //  model.Image = "user-0.jpg";

                model.CUSERID = me["username"]; ;
                model.PersName1 = me["first_name"];
                model.EMAIL1 = me["email"];
                DB.Eco_TBLCONTACT.AddObject(model);
                DB.SaveChanges();
                Session["USER"] = model;

                //string FacebookMessage = "What can I say, fantasy football just comes easy to me. Especially now since I’m a boss at PigskinBoss.com. You can be a boss too - unless of course you’re in my league. I can’t let the competition get access to the boss network.";
                //string FacebookLink = "www.Pigskinboss.com";

                //Guid UserId = new Guid(Membership.GetUser().ProviderUserKey.ToString());
                //PigSkinDAL.Boss _Boss = db.Bosses.Single(b => b.UserID == UserId);

                //Common.ConnectToFacebook(FacebookMessage, FacebookLink, _User, (int)FacebookActionType.Register, true, Convert.ToInt32(_Boss.ID));


            }
            else
            {
                Session["USER"] = Memberlist[0];

            }


            return true;


        }

    }
}