using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Database;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Web.CRM
{
    public partial class CRMMailManagement : System.Web.UI.Page
    {
        Database.CallEntities DB = new Database.CallEntities();
        // Database.CallEntities DB1 = new Database.CallEntities();
        bool FirstFlag, ClickFlag = true;
        int TID, LID, stMYTRANSID, UID, MID, EMPID, Transid, Transsubid, CID = 0;
        string LangID, CURRENCY, USERID, Crypath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionLoad();
            if (!IsPostBack)
            {
                FistTimeLoad();
                BibdDropdwon();
                BindTemplet();
            }
            Panel1.Visible = false;
            pnlSuccessMsg.Visible = false;
            pnlTempletMailSend.Visible = false;
        }
        public void FistTimeLoad()
        {
            FirstFlag = false;
        }
        public void SessionLoad()
        {
            string Ref = ((CRMMaster)Page.Master).SessionLoad1(TID, LID, UID, EMPID, LangID);

            string[] id = Ref.Split(',');
            TID = Convert.ToInt32(id[0]);
            LID = Convert.ToInt32(id[1]);
            UID = Convert.ToInt32(id[2]);
            EMPID = Convert.ToInt32(id[3]);
            LangID = id[4].ToString();
        }
        public void BibdDropdwon()
        {
            var TitleData = (from TitleRef in DB.REFTABLEs
                             join Search in DB.ISSearchDetails on TitleRef.REFID equals Search.REFID
                             where Search.CreatedBy == UID && TitleRef.REFTYPE == "Search" && TitleRef.REFSUBTYPE == "Company" && TitleRef.TenentID == TID
                             select new
                             {
                                 ID = TitleRef.REFID,
                                 TitleRef.REFNAME1
                             }).ToList().Distinct();

            drpcompantSerch.DataSource = TitleData;
            drpcompantSerch.DataTextField = "REFNAME1";
            drpcompantSerch.DataValueField = "ID";
            drpcompantSerch.DataBind();
            drpcompantSerch.Items.Insert(0, new ListItem("-- Select --", "0"));


            var TitleDataContect = (from TitleRef in DB.REFTABLEs
                                    join Search in DB.ISSearchDetails on TitleRef.REFID equals Search.REFID
                                    where Search.CreatedBy == UID && TitleRef.REFTYPE == "Search" && TitleRef.REFSUBTYPE == "Contact" && TitleRef.TenentID == TID
                                    select new
                                    {
                                        ID = TitleRef.REFID,
                                        TitleRef.REFNAME1
                                    }).ToList().Distinct();

            drpcontactList.DataSource = TitleDataContect;
            drpcontactList.DataTextField = "REFNAME1";
            drpcontactList.DataValueField = "ID";
            drpcontactList.DataBind();
            drpcontactList.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int TitleID = 0;
            if (drpcontactList.SelectedValue != "0" || drpcompantSerch.SelectedValue != "0")
            {
                if (drpcontactList.SelectedValue != "0")
                {
                    TitleID = Convert.ToInt32(drpcontactList.SelectedValue);
                    List<ISSearchDetail> Search_List = DB.ISSearchDetails.Where(p => p.REFID == TitleID && p.CreatedBy == UID && p.TenentID == TID).ToList();
                    List<Database.TBLCONTACT> Con_List = new List<Database.TBLCONTACT>();

                    foreach (ISSearchDetail item in Search_List)
                    {
                        Database.TBLCONTACT obj_Contact = DB.TBLCONTACTs.Single(p => p.ContactMyID == item.ContactID && p.TenentID == TID);
                        Con_List.Add(obj_Contact);
                    }
                    ListView1.DataSource = Con_List;//DB.TBLCONTACT.Where(p => p.TenentID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
                    ListView1.DataBind();
                    pnlcontect.Visible = true;
                }

                else
                {
                    TitleID = Convert.ToInt32(drpcompantSerch.SelectedValue);
                    List<ISSearchDetail> Search_List = DB.ISSearchDetails.Where(p => p.REFID == TitleID && p.CreatedBy == UID && p.TenentID == TID).ToList();
                    List<Database.TBLCOMPANYSETUP> Con_List = new List<Database.TBLCOMPANYSETUP>();
                    foreach (ISSearchDetail item in Search_List)
                    {
                        Database.TBLCOMPANYSETUP obj_Contact = DB.TBLCOMPANYSETUPs.Single(p => p.TenentID == TID && p.COMPID == item.CompanyID);
                        Con_List.Add(obj_Contact);
                    }
                    grdmstr.DataSource = Con_List;//DB.TBLCONTACT.Where(p => p.TenentID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
                    grdmstr.DataBind();
                    pnlcompniy.Visible = true;
                }



            }
            else
            {
                Panel1.Visible = true;
            }

        }

        protected void cbkcontect_CheckedChanged(object sender, EventArgs e)
        {
            if (cbkcontect.Checked == true)
            {
                for (int I = 0; I < ListView1.Items.Count; I++)
                {
                    CheckBox cbkcontectInList = (CheckBox)ListView1.Items[I].FindControl("cbkcontectInList");
                    cbkcontectInList.Checked = true;
                }
            }
            else
            {
                for (int I = 0; I < ListView1.Items.Count; I++)
                {
                    CheckBox cbkcontectInList = (CheckBox)ListView1.Items[I].FindControl("cbkcontectInList");
                    cbkcontectInList.Checked = false;
                }
            }
        }

        protected void cbkchekbo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbkchekbo.Checked == true)
            {
                for (int I = 0; I < grdmstr.Items.Count; I++)
                {
                    CheckBox cbkcmpnylist = (CheckBox)grdmstr.Items[I].FindControl("cbkcmpnylist");
                    cbkcmpnylist.Checked = true;
                }
            }
            else
            {
                for (int I = 0; I < grdmstr.Items.Count; I++)
                {
                    CheckBox cbkcmpnylist = (CheckBox)grdmstr.Items[I].FindControl("cbkcmpnylist");
                    cbkcmpnylist.Checked = false;
                }
            }
        }

        protected void btnsendMail_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdmstr.Items.Count; i++)
            {
                CheckBox cbkcmpnylist = (CheckBox)grdmstr.Items[i].FindControl("cbkcmpnylist");
                if (cbkcmpnylist.Checked == true)
                {
                    Label lblEMAIL = (Label)grdmstr.Items[i].FindControl("lblEMAIL");
                    Label hidecompanyctid = (Label)grdmstr.Items[i].FindControl("hidecompanyctid");
                    string Email = lblEMAIL.Text;
                    if (Email != "")
                    {
                        string Getvalue = drptemplete.SelectedValue.ToString();
                        WebClient myWebClient = new WebClient();

                        // Download the markup from 
                        byte[] myDataBuffer = myWebClient.DownloadData(Getvalue);

                        // Convert the downloaded data into a string
                        string markup = Encoding.ASCII.GetString(myDataBuffer);
                        //  string Body = txtoverviewediter.Text;

                        ComposeMail Obj = new ComposeMail();
                        Obj.TenentID = TID;
                        Obj.LocationID = LID;
                        Obj.MyID = DB.ComposeMails.Where(p => p.TenentID == TID).Count() > 0 ? Convert.ToInt32(DB.ComposeMails.Where(p => p.TenentID == TID).Max(p => p.MyID) + 1) : 1;
                        Obj.CompanyAndContactID = Convert.ToInt32(hidecompanyctid.Text);
                        Obj.Reference = "Company";
                        Obj.HtmlTemplate = "";
                        Obj.HtmlLink = "";
                        Obj.IsSend = sendEmail(markup, Email);
                        Obj.UserId = UID;
                        Obj.DateTime = DateTime.Now;
                        Obj.TemplateId = 0;
                        DB.ComposeMails.AddObject(Obj);
                        DB.SaveChanges();
                    }

                }
            }
        }

        protected void btnContactsend_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListView1.Items.Count; i++)
            {
                CheckBox cbkcontectInList = (CheckBox)ListView1.Items[i].FindControl("cbkcontectInList");
                if (cbkcontectInList.Checked == true)
                {
                    Label lblEMAIL = (Label)grdmstr.Items[i].FindControl("lblEMAIL");
                    Label hidecontactid = (Label)grdmstr.Items[i].FindControl("hidecontactid");
                    string Email = lblEMAIL.Text;
                    if (Email != "")
                    {
                        string Getvalue = drptemplete.SelectedValue.ToString();

                        WebClient myWebClient = new WebClient();

                        // Download the markup from 
                        byte[] myDataBuffer = myWebClient.DownloadData(Getvalue);

                        // Convert the downloaded data into a string
                        string markup = Encoding.ASCII.GetString(myDataBuffer);
                        // string Body = txtoverviewediter.Text;

                        ComposeMail Obj = new ComposeMail();
                        Obj.TenentID = TID;
                        Obj.LocationID = LID;
                        Obj.MyID = 1;
                        Obj.CompanyAndContactID = Convert.ToInt32(hidecontactid.Text);
                        Obj.Reference = "Contact";
                        Obj.HtmlTemplate = "";
                        Obj.HtmlLink = "";
                        Obj.IsSend = sendEmail(markup, Email);
                        Obj.UserId = UID;
                        Obj.DateTime = DateTime.Now;
                        Obj.TemplateId = 0;
                        DB.ComposeMails.AddObject(Obj);
                        DB.SaveChanges();
                    }

                }
            }
        }


        public bool sendEmail(string body, string email)
        {
            try
            {


                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

                msg.Subject = "Thannk you for Demo Request..";

                msg.From = new System.Net.Mail.MailAddress("digital53marketing@gmail.com");//("supportteam@digital53.com ");

                msg.To.Add(new System.Net.Mail.MailAddress(email));
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.Body = body;
                msg.IsBodyHtml = true;
                msg.Priority = System.Net.Mail.MailPriority.High;

                System.Net.Mail.SmtpClient smpt = new System.Net.Mail.SmtpClient();
                smpt.UseDefaultCredentials = false;
                smpt.Host = "smtp.gmail.com";//for google required smtp.gmail.com and must be check Google Account setting https://myaccount.google.com/lesssecureapps?pli=1 ON//"mail.digital53.com";

                smpt.Port = 587;

                smpt.EnableSsl = true;

                //smpt.Credentials = new System.Net.NetworkCredential("supportteam@digital53.com ", "Support123$");
                smpt.Credentials = new System.Net.NetworkCredential("digital53marketing@gmail.com", "Digital123$");
                smpt.Send(msg);
                return true;

            }

            catch (Exception e)
            {
                return false;
            }



        }

        public string Gettems()
        {
            string Getvalue = drptemplete.SelectedValue.ToString();
            return Getvalue;

        }

        protected void drptemplete_SelectedIndexChanged(object sender, EventArgs e)
        {
            int templateID = Convert.ToInt32(drptemplete.SelectedValue);
            if (templateID == 0)
            {
                lblcheckmessage.Text = "select Templet";
                Image1.Visible = false;
            }
            else
            {
                string imgpath = "~/CRM/images/Mail/" + DB.Mail_Templet.Single(p => p.ID == templateID).Avtar;
                Image1.ImageUrl = imgpath;
                Image1.Visible = true;

            }
            pnlTempletMailSend.Visible = true;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            int templateID = Convert.ToInt32(drptemplete.SelectedValue);
            if (templateID == 0)
            {
                lblcheckmessage.Text = "select Templet";
                Image1.Visible = false;
            }
            else
            {
                string markup = DB.Mail_Templet.Single(p => p.ID == templateID).TempletHTML_E;
                string markupfinal = markup.Replace("CHANGEFROMPROJECT", txtname.Text).Replace("CURRENTMAIL", txtmailsend.Text).Replace("CURRENTDATE", DateTime.Now.ToShortDateString());

                sendEmail(markupfinal, txtmailsend.Text);
                txtname.Text = "";
                txtmailsend.Text = "";
                lblMsg.Text = "Mail Send successfull";
                pnlSuccessMsg.Visible = true;
            }





        }
        public void BindTemplet()
        {
            Classes.EcommAdminClass.getdropdown(drptemplete, TID, "", "", "", "Mail_Templet");
            //select * from Mail_Templet where TID = 1 

            //drptemplete.DataSource = DB.Mail_Templet.Where(p=>p.Actived==true);
            //drptemplete.DataTextField = "Title";
            //drptemplete.DataValueField = "ID";
            //drptemplete.DataBind();
            //drptemplete.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        protected void btntestmailsend_Click(object sender, EventArgs e)
        {

            pnlTempletMailSend.Visible = true;


        }



        protected void btnShowFormate_Click(object sender, EventArgs e)
        {
            int templateID = Convert.ToInt32(drptemplete.SelectedValue);
            if (templateID == 0)
            {
                lblcheckmessage.Text = "select Templet";
                Image1.Visible = false;
            }
            else
            {
                string imgpath = DB.Mail_Templet.Single(p => p.ID == templateID).Templet_Link;
                //Response.Redirect(imgpath);
                Page.ClientScript.RegisterStartupScript(
    this.GetType(), "OpenWindow", "window.open('" + imgpath + "','_newtab');", true);

            }

            //or

        }
    }
}