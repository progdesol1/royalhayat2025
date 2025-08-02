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
    public partial class Mail2 : System.Web.UI.Page
    {
        Database.CallEMEntities DB = new Database.CallEMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BibdDropdwon();
                
            }
            Panel1.Visible = false;
            pnlSuccessMsg.Visible = false;
        }
        public void BibdDropdwon()
        {
            int TID = Convert.ToInt32(((ACM_USER_MST)Session["USER"]).TENANT_ID);
            int UID = Convert.ToInt32(((ACM_USER_MST)Session["USER"]).USER_ID);


            var TitleData = (from TitleRef in DB.CRM_REFTABLE
                             join Search in DB.CRM_ISSearchDetail on TitleRef.REFID equals Search.REFID
                             where Search.CreatedBy == UID && TitleRef.REFTYPE == "Search" && TitleRef.REFSUBTYPE == "Company"
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


            var TitleDataContect = (from TitleRef in DB.CRM_REFTABLE
                                    join Search in DB.CRM_ISSearchDetail on TitleRef.REFID equals Search.REFID
                                    where Search.CreatedBy == UID && TitleRef.REFTYPE == "Search" && TitleRef.REFSUBTYPE == "Contact"
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
            int UID = Convert.ToInt32(((ACM_USER_MST)Session["USER"]).USER_ID);
            int TID = Convert.ToInt32(((ACM_USER_MST)Session["USER"]).TENANT_ID);
            if (drpcontactList.SelectedValue != "0" || drpcompantSerch.SelectedValue != "0")
            {
                if (drpcontactList.SelectedValue != "0")
                {
                    TitleID = Convert.ToInt32(drpcontactList.SelectedValue);
                    List<CRM_ISSearchDetail> Search_List = DB.CRM_ISSearchDetail.Where(p => p.REFID == TitleID && p.CreatedBy == UID).ToList();
                    List<Database.CRM_TBLCONTACT> Con_List = new List<Database.CRM_TBLCONTACT>();

                    foreach (CRM_ISSearchDetail item in Search_List)
                    {
                        Database.CRM_TBLCONTACT obj_Contact = DB.CRM_TBLCONTACT.Single(p => p.ContactMyID == item.ContactCompanyID && p.TenantID == TID);
                        Con_List.Add(obj_Contact);
                    }
                    ListView1.DataSource = Con_List;//DB.CRM_TBLCONTACT.Where(p => p.TenantID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
                    ListView1.DataBind();
                    pnlcontect.Visible = true;
                }

                else
                {
                    TitleID = Convert.ToInt32(drpcompantSerch.SelectedValue);
                    List<CRM_ISSearchDetail> Search_List = DB.CRM_ISSearchDetail.Where(p => p.REFID == TitleID && p.CreatedBy == UID).ToList();
                    List<Database.CRM_TBLCOMPANYSETUP> Con_List = new List<Database.CRM_TBLCOMPANYSETUP>();
                    foreach (CRM_ISSearchDetail item in Search_List)
                    {
                        Database.CRM_TBLCOMPANYSETUP obj_Contact = DB.CRM_TBLCOMPANYSETUP.Single(p => p.TenantID == TID && p.COMPID == item.ContactCompanyID);
                        Con_List.Add(obj_Contact);
                    }
                    grdmstr.DataSource = Con_List;//DB.CRM_TBLCONTACT.Where(p => p.TenantID == TID && p.ContactMyID==TitleID && p.Active == "Y" && p.PHYSICALLOCID != "HLY");
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
            int UID = Convert.ToInt32(((ACM_USER_MST)Session["USER"]).USER_ID);
            int TID = Convert.ToInt32(((ACM_USER_MST)Session["USER"]).TENANT_ID);
            int LID = Convert.ToInt32(((ACM_USER_MST)Session["USER"]).LOCATION_ID);

            for (int i = 0; i < grdmstr.Items.Count; i++)
            {
                CheckBox cbkcmpnylist = (CheckBox)grdmstr.Items[i].FindControl("cbkcmpnylist");
                if (cbkcmpnylist.Checked == true)
                {
                    Label lblEMAIL = (Label)grdmstr.Items[i].FindControl("lblEMAIL");
                    Label lblCustomerName = (Label)grdmstr.Items[i].FindControl("lblCustomerName");
                    Label hidecompanyctid = (Label)grdmstr.Items[i].FindControl("hidecompanyctid");
                    string Email = lblEMAIL.Text;
                    string Name = lblCustomerName.Text;
                    DateTime Now = DateTime.Now;
                    string DateToday = Now.ToShortDateString();
                    if (Email != "")
                    {
                        // string Getvalue = drptemplete.SelectedValue.ToString();
                        // WebClient myWebClient = new WebClient();

                        // Download the markup from 
                        //  byte[] myDataBuffer = myWebClient.DownloadData(Getvalue);

                        // Convert the downloaded data into a string
                        string markup = "<html xmlns:v='urn:schemas-microsoft-com:vml' xmlns='http://www.w3.org/1999/xhtml'><head><meta name='viewport' content='text/html; charset=utf-8; width=device-width' http-equiv='Content-Type'><title>Digital Edge Solutions Computer Company</title><style type='text/css'>.outlook {background-color: #ffffff; display: none;display: none !important;}.ReadMsgBody {width: 100%; background-color: #f5f5f5;}.ExternalClass {width: 100%;background-color: #f5f5f5;}body {width: 100%;background-color: #f5f5f5;margin: 0 auto !important;}.hidetop {display: none;} @media screen and (max-width:639px) {*[class=mobemailfullwidth] {width: 100% !important;height: auto !important;}*[class=emailcolsplit] {float: left !important;width: 100% !important;}*[class=hidecellinmobile] {display: none !important;}*[class=spacer] {margin-top: 20px !important; }*[class=offer] {font-size: 30px !important;line-height: 36px !important;}.hideimage {display: none;}*[class=subhdr] {font-size: 18px !important;line-height: 21px !important; } *[class=phonenumber] {text-align: center !important;} *[class=disclaimer] {padding: 10px;}.ExternalClass * {line-height: 100%; }*[class=threshold] {text-align: left; font-size: 28px !important; line-height: 30px !important; }*[class=threshold2] { text-align: left; font-size: 18px !important;line-height: 22px !important;}}.ExternalClass * {line-height: 100%;}/* CLIENT-SPECIFIC STYLES - templates 1232 &amp; 3496 */img {-ms-interpolation-mode: bicubic;}/* Force IE to smoothly render resized images. */ #outlook a {padding: 0;} /* Force Outlook 2007 and up to provide a 'view in browser' message. */table {mso-table-lspace: 0pt; mso-table-rspace: 0pt;} /* Remove spacing between tables in Outlook 2007 and up. */.ReadMsgBody {width: 100%;}.ExternalClass {width: 100%;}/* Force Outlook.com to display emails at full width. */ p, a, li, td, blockquote {mso-line-height-rule: exactly;}/* Force Outlook to render line heights as they're originally set. */a[href^='tel'], a[href^='sms'] {color: inherit;cursor: default;text-decoration: none;}/* Force mobile devices to inherit declared link styles. */ p, a, li, td, body, table, blockquote {-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;}/* Prevent Windows- and Webkit-based mobile platforms from changing declared text sizes. */.ExternalClass, .ExternalClass p, .ExternalClass td, .ExternalClass div, .ExternalClass span, .ExternalClass font { line-height: 100%; }/* Force Outlook.com to display line heights normally. *//* General 'Download App' for mobile view code. Templates 485 &amp; 514 */.mobile, td[class='mobile'], table[class='mobile'], img[class='mobile'], div[class='mobile'], tr[class='mobile'] {display: inline-block !important;width: auto !important;overflow: visible !important;height: auto !important;max-height: inherit !important;font-size: 15px !important;line-height: 21px !important;visibility: visible !important;        }        #prehdr {display: none !important;visibility: hidden !important;        }        @font-face {font-family: 'Boing-Bold';src: url('http://img1.wsimg.com/ux/fonts/1.3/eot/Boing-Bold.eot?#iefix') format('embedded-opentype'), url('http://img1.wsimg.com/ux/fonts/1.3/woff2/Boing-Bold.woff2') format('woff2'), url('http://img1.wsimg.com/ux/fonts/1.3/woff/Boing-Bold.woff') format('woff'), url('http://img1.wsimg.com/ux/fonts/1.3/ttf/Boing-Bold.ttf') format('truetype');font-weight: 400;font-style: normal;        }    </style></head><body bgcolor='#ffffff' backcolo='backcolo' style='min-width:100%; margin:0; -webkit-text-size-adjust:none; -ms-text-size-adjust:100%'><img height='0' width='0' border='0' alt='' src='http://img.secureserver.net/bbimage.aspx?pl=1&amp;isc=gdbbu1901&amp;e=newayosoftech%40gmail.com&amp;tid=1901&amp;eid=309348279&amp;mid=3ed57945-0afc-49bf-96c3-9f024d7c4f51'><br><img height='0' width='0' border='0' alt='' src='https://godaddy.sp1.convertro.com/view/vt/v1/godaddy/1/cvo.gif?cvosrc=bounceback.1901.gdbbu1901'><img height='1' width='1' border='0' alt=' ' src='https://media.godaddy.com/image?spacedesc=34990388_34990386_1x1_34990387_34990388&amp;random='><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center'><tbody><tr> <td valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#FDC400'><tbody><tr><td valign='top' align='center'><table width='600' cellspacing='0' cellpadding='0' border='0' align='center' class='mobemailfullwidth'><tbody><tr><td><table width='100%' cellspacing='0' cellpadding='0' border='0' align='center'><tbody><tr><td valign='top' align='center' style='display:none !important; visibility:hidden !important;'><font face='Arial, sans-serif' color='#333333' style='font-size:10px; -webkit-text-size-adjust:none'>Open this email to read the details of your order and find additional savings inside. Keep this email for your records.</font></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#FDC400'><tbody><tr><td style='line-height:10px;'><div style='height:10px;'><img height='10' border='0' style='display:block' src='http://imagesak.secureserver.net/promos/std/spc_trans.gif'></div></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td width='120' valign='middle' align='center' class='emailcolsplit'><div align='center'><a target='_blank' href='http://www.digital53.com'><img border='0' style='display: block; width: 70px; padding-bottom: 5px; padding-top: 5px;' alt='Didital Edge Solution ' src='http://corp.digital53.com/mail/Logo.png'></a></div></td><td width='520' valign='middle' class='emailcolsplit'><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td align='right' class='phonenumber' style='padding-left:5px; padding-right:5px; padding-bottom:5px'><font face='Arial, sans-serif' color='#ffffff' class='phonenumber' style='font-size:18px; -webkit-text-size-adjust:none; line-height:18px'><font color='#FFFFFF'><a style='color: #DA251C; text-decoration: none;' target='_blank' href='http://www.digital53.com'>Digital Edge Solutions Computer Company</a></font></font></td></tr>   <tr><td align='right' class='phonenumber' style='padding-left:5px; padding-right:5px'><font face='Arial, sans-serif' color='#010101' class='phonenumber' style='font-size:14px; -webkit-text-size-adjust:none; line-height:14px'>E-Commerce : <a style='color: #37b8eb; text-decoration: none;' target='_blank' href='http://e.digital53.com'>http://e.digital53.com</a></font></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#333333'><tbody><tr><td style='line-height:10px;'><div style='height:10px;'><img height='10' border='0' style='display:block' src='http://imagesak.secureserver.net/promos/std/spc_trans.gif'></div></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#77c043'><tbody><tr><td valign='top' align='center'><table width='640' cellspacing='0' cellpadding='0' border='0' align='center' class='mobemailfullwidth'><tbody><tr><td style='padding-top:40px; padding-bottom:40px; padding-left:20px; padding-right:20px'> <table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td align='left' style='padding: 10px 10px 0; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>   Dear  <b>" + Name + "</b> ,</font></td>  <td align='left' style='padding: 10px 10px 0; text-align: justify;'>   <font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>Date :" + DateToday + "</font></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td valign='baseline' bgcolor='#ffffff' align='left' style='padding: 10px 10px 0px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'><br>Many thanks for the opportunity given to us to meet you at your premises " + Name + " we as Digital Edge Take pride to have esteem customer like you at our umbrella and we will be more than happy to server your organization. <br><br></font></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td align='left' style='padding: 10px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>We deal in the infrastructure product that can have your all the servers converted on our Suse Linux and you get a full Live DR Plan. We do care for your existing important servers as Machine and they can be saved using our MicroFocus product where you will receive back your entire server within an Hour back into operation.</font></td><td valign='middle' bgcolor='#ffffff' align='left' style='padding-left: 0px; padding-right: 10px; padding-top: 0px; text-align: justify;'><a href='http://corp.digital53.com/Netiq'><img src='http://corp.digital53.com/mail/Img/Step2.png' style='width: 192px; height: 78px;'></a></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td valign='middle' bgcolor='#ffffff' align='left' style='padding-left: 10px; padding-right: 0px; padding-top: 0px; text-align: justify;'><a href='http://corp.digital53.com/'><img style='' src='http://corp.digital53.com/mail/Img/CRMmail.png'></a></td><td align='left' style='padding: 10px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>We are specialized in <a href='http://e.digital53.com/acm/Index.aspx'>CRM</a>, Business Process Management used as core of the <a href='http://corp.digital53.com'>ERP</a> (GL, SL, Inventory, Purchase, Sales, Fixed Assets, <a href='http://hrm.digital53.com/'>HRM</a>, Ticket Management, Flow Management, Project Management and Schedule Management). Our unique products comes with the full online <a href='http://corp.digital53.com'>SAAS based solutions</a>.</font></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td align='left' style='padding: 10px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>The Help Desk Management solutions is a unique product that can cater all your IT Assets managed that includes Facilities Management (Buildings), Services, Telephony and much more.</font></td><td valign='middle' bgcolor='#ffffff' align='left' style='padding-left: 0px; padding-right: 10px; padding-top: 0px; text-align: justify;'><a href='http://corp.digital53.com/HouseOnTheHill'><img style='width: 105px; height: 104px;' src='http://corp.digital53.com/mail/Img/HotH-logo-square-150.jpg'></a></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td valign='middle' bgcolor='#ffffff' align='left' style='padding-left: 10px; padding-right: 0px; padding-top: 0px; text-align: justify;'><a href='http://corp.digital53.com/DRBD'><img src='http://corp.digital53.com/mail/Img/recovery%20Logo.png' style='width: 125px; height: 124px;'></a></td><td align='left' style='padding: 10px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size: 14px; -webkit-text-size-adjust: none; line-height: 20px;  '>With our unique expertise the Networking, CCTV, Blade Server Installation, Annual Maintenance Contracts, Novell Netware Expertise, Linux/Unix Expertise. Where which we do cover many domain that to handle the various way of the IT Department Consultancy including RFP, RFQ, Tending Process, SOP and much more .</font></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#fedc45'><tbody><tr><td valign='top' align='center'><table width='640' cellspacing='0' cellpadding='0' border='0' align='center' class='mobemailfullwidth'><tbody><tr><td style='padding-top:20px; padding-bottom:20px; padding-left:20px; padding-right:20px'><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td valign='middle' align='left' style='padding-left:0px; padding-right:0px; padding-top:0px; padding-bottom:5px'><a style='text-decoration:none; font-size:20px;' href='#'><font face='Arial, sans-serif' color='#333333' style='font-size:17px; -webkit-text-size-adjust:none; line-height:22px'><strong>We urge a call from your end to our expert on +965-99144172 / 22660781 .</strong></font></a></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td valign='middle' align='left' style='padding-left:0px; padding-right:0px; padding-top:5px; padding-bottom:0px'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>Email : <a href='mailto:sales@digital53.com'>sales@digital53.com</a></font><br><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>Website : <a target='_blank' href='http://www.digital53.com'>www.digital53.com</a>  </font></td><td valign='middle' align='left' style='padding: 5px 0px; width: 288px; height: 51px;'><a target='_blank' href='https://twitter.com/deonlineq8'><img style='width: 39px;' src='http://corp.digital53.com/mail/Img/tweeter.png'></a><a target='_blank' href='https://kw.linkedin.com/in/joharmandav'><img style='width: 39px;' src='http://corp.digital53.com/mail/Img/in.png'></a><a target='_blank' href='https://www.instagram.com/deonlineq8'><img style='width: 39px;' src='http://corp.digital53.com/mail/Img/image.png'></a><a target='_blank' href='https://www.facebook.com/digital53'><img style='width: 39px;' src='http://corp.digital53.com/mail/Img/fb.png'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#bebebe'><tbody><tr><td valign='top' align='center'><table width='640' cellspacing='0' cellpadding='0' border='0' align='center' class='mobemailfullwidth'><tbody><tr><td style='padding-top:20px; padding-bottom:20px; padding-left:20px; padding-right:10px'><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td valign='middle' align='left' style='padding-left:0px; padding-right:0px; padding-top:5px; padding-bottom:0px'><div class='social-c'> <a href='#'></a><a href='#' class='t-icon'></a><a href='#' class='g-icon'></a><a href='#' class='y-icon'></a> </div><div><center><a target='_blank' href='http://e.digital53.com/Subscribe.aspx'>SafeUnsubscribe&trade; " + Email + " </a><br> Forward email  | <a target='_blank' href='http://e.digital53.com/Profile.aspx'>  Update profile  </a>|  About our service provider<br>Sent by newsletter@channelsmedia.me<br><table>  </table><br></center></div><font face='Arial, sans-serif' color='#333333' style='font-size:12px; -webkit-text-size-adjust:none; line-height:20px'>Copyright &copy; 1999-2016 Digital Edge Solutions Company. Al-Mulhum Complex Ground Floor, Shop #2 and 3, HawallyP. O. Box No. 3552 Hawally, Kuwait (AG). All rights reserved.</font></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></body></html>";
                        //  string Body = txtoverviewediter.Text;

                        CRM_ComposeMail Obj = new CRM_ComposeMail();
                        Obj.TenantId = TID;
                        Obj.LocationID = LID;
                        Obj.MyID = DB.CRM_ComposeMail.Count() > 0 ? Convert.ToInt32(DB.CRM_ComposeMail.Max(p => p.MyID) + 1) : 1;
                        Obj.CompanyAndContactID = Convert.ToInt32(hidecompanyctid.Text);
                        Obj.Reference = "Company";
                        Obj.HtmlTemplate = "";
                        Obj.HtmlLink = "";
                        Obj.IsSend = sendEmail(markup, Email);
                        Obj.UserId = UID;
                        Obj.DateTime = DateTime.Now;
                        Obj.TemplateId = 0;
                        DB.CRM_ComposeMail.AddObject(Obj);
                        DB.SaveChanges();
                    }

                }
            }
        }

        protected void btnContactsend_Click(object sender, EventArgs e)
        {
            int UID = Convert.ToInt32(((ACM_USER_MST)Session["USER"]).USER_ID);
            int TID = Convert.ToInt32(((ACM_USER_MST)Session["USER"]).TENANT_ID);
            int LID = Convert.ToInt32(((ACM_USER_MST)Session["USER"]).LOCATION_ID);
            for (int i = 0; i < ListView1.Items.Count; i++)
            {
                CheckBox cbkcontectInList = (CheckBox)ListView1.Items[i].FindControl("cbkcontectInList");
                if (cbkcontectInList.Checked == true)
                {
                    Label lblEMAIL = (Label)ListView1.Items[i].FindControl("lblEMAIL");
                    Label lblCustomerName = (Label)ListView1.Items[i].FindControl("lblCustomerName");

                    Label hidecontactid = (Label)ListView1.Items[i].FindControl("hidecontactid");
                    string Email = lblEMAIL.Text;
                    string Name = lblCustomerName.Text;
                    DateTime Now = DateTime.Now;
                    string DateToday = Now.ToShortDateString();
                    if (Email != "")
                    {
                        //  string Getvalue = drptemplete.SelectedValue.ToString();

                        //   WebClient myWebClient = new WebClient();

                        // Download the markup from 
                        //  byte[] myDataBuffer = myWebClient.DownloadData(Getvalue);

                        // Convert the downloaded data into a string
                        string markup = "<html xmlns:v='urn:schemas-microsoft-com:vml' xmlns='http://www.w3.org/1999/xhtml'><head><meta name='viewport' content='text/html; charset=utf-8; width=device-width' http-equiv='Content-Type'><title>Digital Edge Solutions Computer Company</title><style type='text/css'>.outlook {background-color: #ffffff; display: none;display: none !important;}.ReadMsgBody {width: 100%; background-color: #f5f5f5;}.ExternalClass {width: 100%;background-color: #f5f5f5;}body {width: 100%;background-color: #f5f5f5;margin: 0 auto !important;}.hidetop {display: none;} @media screen and (max-width:639px) {*[class=mobemailfullwidth] {width: 100% !important;height: auto !important;}*[class=emailcolsplit] {float: left !important;width: 100% !important;}*[class=hidecellinmobile] {display: none !important;}*[class=spacer] {margin-top: 20px !important; }*[class=offer] {font-size: 30px !important;line-height: 36px !important;}.hideimage {display: none;}*[class=subhdr] {font-size: 18px !important;line-height: 21px !important; } *[class=phonenumber] {text-align: center !important;} *[class=disclaimer] {padding: 10px;}.ExternalClass * {line-height: 100%; }*[class=threshold] {text-align: left; font-size: 28px !important; line-height: 30px !important; }*[class=threshold2] { text-align: left; font-size: 18px !important;line-height: 22px !important;}}.ExternalClass * {line-height: 100%;}/* CLIENT-SPECIFIC STYLES - templates 1232 &amp; 3496 */img {-ms-interpolation-mode: bicubic;}/* Force IE to smoothly render resized images. */ #outlook a {padding: 0;} /* Force Outlook 2007 and up to provide a 'view in browser' message. */table {mso-table-lspace: 0pt; mso-table-rspace: 0pt;} /* Remove spacing between tables in Outlook 2007 and up. */.ReadMsgBody {width: 100%;}.ExternalClass {width: 100%;}/* Force Outlook.com to display emails at full width. */ p, a, li, td, blockquote {mso-line-height-rule: exactly;}/* Force Outlook to render line heights as they're originally set. */a[href^='tel'], a[href^='sms'] {color: inherit;cursor: default;text-decoration: none;}/* Force mobile devices to inherit declared link styles. */ p, a, li, td, body, table, blockquote {-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;}/* Prevent Windows- and Webkit-based mobile platforms from changing declared text sizes. */.ExternalClass, .ExternalClass p, .ExternalClass td, .ExternalClass div, .ExternalClass span, .ExternalClass font { line-height: 100%; }/* Force Outlook.com to display line heights normally. *//* General 'Download App' for mobile view code. Templates 485 &amp; 514 */.mobile, td[class='mobile'], table[class='mobile'], img[class='mobile'], div[class='mobile'], tr[class='mobile'] {display: inline-block !important;width: auto !important;overflow: visible !important;height: auto !important;max-height: inherit !important;font-size: 15px !important;line-height: 21px !important;visibility: visible !important;        }        #prehdr {display: none !important;visibility: hidden !important;        }        @font-face {font-family: 'Boing-Bold';src: url('http://img1.wsimg.com/ux/fonts/1.3/eot/Boing-Bold.eot?#iefix') format('embedded-opentype'), url('http://img1.wsimg.com/ux/fonts/1.3/woff2/Boing-Bold.woff2') format('woff2'), url('http://img1.wsimg.com/ux/fonts/1.3/woff/Boing-Bold.woff') format('woff'), url('http://img1.wsimg.com/ux/fonts/1.3/ttf/Boing-Bold.ttf') format('truetype');font-weight: 400;font-style: normal;        }    </style></head><body bgcolor='#ffffff' backcolo='backcolo' style='min-width:100%; margin:0; -webkit-text-size-adjust:none; -ms-text-size-adjust:100%'><img height='0' width='0' border='0' alt='' src='http://img.secureserver.net/bbimage.aspx?pl=1&amp;isc=gdbbu1901&amp;e=newayosoftech%40gmail.com&amp;tid=1901&amp;eid=309348279&amp;mid=3ed57945-0afc-49bf-96c3-9f024d7c4f51'><br><img height='0' width='0' border='0' alt='' src='https://godaddy.sp1.convertro.com/view/vt/v1/godaddy/1/cvo.gif?cvosrc=bounceback.1901.gdbbu1901'><img height='1' width='1' border='0' alt=' ' src='https://media.godaddy.com/image?spacedesc=34990388_34990386_1x1_34990387_34990388&amp;random='><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center'><tbody><tr> <td valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#FDC400'><tbody><tr><td valign='top' align='center'><table width='600' cellspacing='0' cellpadding='0' border='0' align='center' class='mobemailfullwidth'><tbody><tr><td><table width='100%' cellspacing='0' cellpadding='0' border='0' align='center'><tbody><tr><td valign='top' align='center' style='display:none !important; visibility:hidden !important;'><font face='Arial, sans-serif' color='#333333' style='font-size:10px; -webkit-text-size-adjust:none'>Open this email to read the details of your order and find additional savings inside. Keep this email for your records.</font></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#FDC400'><tbody><tr><td style='line-height:10px;'><div style='height:10px;'><img height='10' border='0' style='display:block' src='http://imagesak.secureserver.net/promos/std/spc_trans.gif'></div></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td width='120' valign='middle' align='center' class='emailcolsplit'><div align='center'><a target='_blank' href='http://www.digital53.com'><img border='0' style='display: block; width: 70px; padding-bottom: 5px; padding-top: 5px;' alt='Didital Edge Solution ' src='http://corp.digital53.com/mail/Logo.png'></a></div></td><td width='520' valign='middle' class='emailcolsplit'><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td align='right' class='phonenumber' style='padding-left:5px; padding-right:5px; padding-bottom:5px'><font face='Arial, sans-serif' color='#ffffff' class='phonenumber' style='font-size:18px; -webkit-text-size-adjust:none; line-height:18px'><font color='#FFFFFF'><a style='color: #DA251C; text-decoration: none;' target='_blank' href='http://www.digital53.com'>Digital Edge Solutions Computer Company</a></font></font></td></tr>   <tr><td align='right' class='phonenumber' style='padding-left:5px; padding-right:5px'><font face='Arial, sans-serif' color='#010101' class='phonenumber' style='font-size:14px; -webkit-text-size-adjust:none; line-height:14px'>E-Commerce : <a style='color: #37b8eb; text-decoration: none;' target='_blank' href='http://e.digital53.com'>http://e.digital53.com</a></font></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#333333'><tbody><tr><td style='line-height:10px;'><div style='height:10px;'><img height='10' border='0' style='display:block' src='http://imagesak.secureserver.net/promos/std/spc_trans.gif'></div></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#77c043'><tbody><tr><td valign='top' align='center'><table width='640' cellspacing='0' cellpadding='0' border='0' align='center' class='mobemailfullwidth'><tbody><tr><td style='padding-top:40px; padding-bottom:40px; padding-left:20px; padding-right:20px'> <table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td align='left' style='padding: 10px 10px 0; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>   Dear  <b>" + Name + "</b> ,</font></td>  <td align='left' style='padding: 10px 10px 0; text-align: justify;'>   <font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>Date :" + DateToday + "</font></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td valign='baseline' bgcolor='#ffffff' align='left' style='padding: 10px 10px 0px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'><br>Many thanks for the opportunity given to us to meet you at your premises " + Name + " we as Digital Edge Take pride to have esteem customer like you at our umbrella and we will be more than happy to server your organization. <br><br></font></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td align='left' style='padding: 10px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>We deal in the infrastructure product that can have your all the servers converted on our Suse Linux and you get a full Live DR Plan. We do care for your existing important servers as Machine and they can be saved using our MicroFocus product where you will receive back your entire server within an Hour back into operation.</font></td><td valign='middle' bgcolor='#ffffff' align='left' style='padding-left: 0px; padding-right: 10px; padding-top: 0px; text-align: justify;'><a href='http://corp.digital53.com/Netiq'><img src='http://corp.digital53.com/mail/Img/Step2.png' style='width: 192px; height: 78px;'></a></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td valign='middle' bgcolor='#ffffff' align='left' style='padding-left: 10px; padding-right: 0px; padding-top: 0px; text-align: justify;'><a href='http://corp.digital53.com/'><img style='' src='http://corp.digital53.com/mail/Img/CRMmail.png'></a></td><td align='left' style='padding: 10px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>We are specialized in <a href='http://e.digital53.com/acm/Index.aspx'>CRM</a>, Business Process Management used as core of the <a href='http://corp.digital53.com'>ERP</a> (GL, SL, Inventory, Purchase, Sales, Fixed Assets, <a href='http://hrm.digital53.com/'>HRM</a>, Ticket Management, Flow Management, Project Management and Schedule Management). Our unique products comes with the full online <a href='http://corp.digital53.com'>SAAS based solutions</a>.</font></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td align='left' style='padding: 10px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>The Help Desk Management solutions is a unique product that can cater all your IT Assets managed that includes Facilities Management (Buildings), Services, Telephony and much more.</font></td><td valign='middle' bgcolor='#ffffff' align='left' style='padding-left: 0px; padding-right: 10px; padding-top: 0px; text-align: justify;'><a href='http://corp.digital53.com/HouseOnTheHill'><img style='width: 105px; height: 104px;' src='http://corp.digital53.com/mail/Img/HotH-logo-square-150.jpg'></a></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td valign='middle' bgcolor='#ffffff' align='left' style='padding-left: 10px; padding-right: 0px; padding-top: 0px; text-align: justify;'><a href='http://corp.digital53.com/DRBD'><img src='http://corp.digital53.com/mail/Img/recovery%20Logo.png' style='width: 125px; height: 124px;'></a></td><td align='left' style='padding: 10px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size: 14px; -webkit-text-size-adjust: none; line-height: 20px;  '>With our unique expertise the Networking, CCTV, Blade Server Installation, Annual Maintenance Contracts, Novell Netware Expertise, Linux/Unix Expertise. Where which we do cover many domain that to handle the various way of the IT Department Consultancy including RFP, RFQ, Tending Process, SOP and much more .</font></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#fedc45'><tbody><tr><td valign='top' align='center'><table width='640' cellspacing='0' cellpadding='0' border='0' align='center' class='mobemailfullwidth'><tbody><tr><td style='padding-top:20px; padding-bottom:20px; padding-left:20px; padding-right:20px'><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td valign='middle' align='left' style='padding-left:0px; padding-right:0px; padding-top:0px; padding-bottom:5px'><a style='text-decoration:none; font-size:20px;' href='#'><font face='Arial, sans-serif' color='#333333' style='font-size:17px; -webkit-text-size-adjust:none; line-height:22px'><strong>We urge a call from your end to our expert on +965-99144172 / 22660781 .</strong></font></a></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td valign='middle' align='left' style='padding-left:0px; padding-right:0px; padding-top:5px; padding-bottom:0px'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>Email : <a href='mailto:sales@digital53.com'>sales@digital53.com</a></font><br><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>Website : <a target='_blank' href='http://www.digital53.com'>www.digital53.com</a>  </font></td><td valign='middle' align='left' style='padding: 5px 0px; width: 288px; height: 51px;'><a target='_blank' href='https://twitter.com/deonlineq8'><img style='width: 39px;' src='http://corp.digital53.com/mail/Img/tweeter.png'></a><a target='_blank' href='https://kw.linkedin.com/in/joharmandav'><img style='width: 39px;' src='http://corp.digital53.com/mail/Img/in.png'></a><a target='_blank' href='https://www.instagram.com/deonlineq8'><img style='width: 39px;' src='http://corp.digital53.com/mail/Img/image.png'></a><a target='_blank' href='https://www.facebook.com/digital53'><img style='width: 39px;' src='http://corp.digital53.com/mail/Img/fb.png'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#bebebe'><tbody><tr><td valign='top' align='center'><table width='640' cellspacing='0' cellpadding='0' border='0' align='center' class='mobemailfullwidth'><tbody><tr><td style='padding-top:20px; padding-bottom:20px; padding-left:20px; padding-right:10px'><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td valign='middle' align='left' style='padding-left:0px; padding-right:0px; padding-top:5px; padding-bottom:0px'><div class='social-c'> <a href='#'></a><a href='#' class='t-icon'></a><a href='#' class='g-icon'></a><a href='#' class='y-icon'></a> </div><div><center><a target='_blank' href='http://e.digital53.com/Subscribe.aspx'>SafeUnsubscribe&trade; " + Email + " </a><br> Forward email  | <a target='_blank' href='http://e.digital53.com/Profile.aspx'>  Update profile  </a>|  About our service provider<br>Sent by newsletter@channelsmedia.me<br><table>  </table><br></center></div><font face='Arial, sans-serif' color='#333333' style='font-size:12px; -webkit-text-size-adjust:none; line-height:20px'>Copyright &copy; 1999-2016 Digital Edge Solutions Company. Al-Mulhum Complex Ground Floor, Shop #2 and 3, HawallyP. O. Box No. 3552 Hawally, Kuwait (AG). All rights reserved.</font></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></body></html>";
                        CRM_ComposeMail Obj = new CRM_ComposeMail();
                        Obj.TenantId = TID;
                        Obj.LocationID = LID;
                        Obj.MyID = DB.CRM_ComposeMail.Count() > 0 ? Convert.ToInt32(DB.CRM_ComposeMail.Max(p => p.MyID) + 1) : 1;
                        Obj.CompanyAndContactID = Convert.ToInt32(hidecontactid.Text);
                        Obj.Reference = "Contact";
                        Obj.HtmlTemplate = "";
                        Obj.HtmlLink = "";
                        Obj.IsSend = sendEmail(markup, Email);
                        Obj.UserId = UID;
                        Obj.DateTime = DateTime.Now;
                        Obj.TemplateId = 0;
                        DB.CRM_ComposeMail.AddObject(Obj);
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

                msg.From = new System.Net.Mail.MailAddress("supportteam@digital53.com ");

                msg.To.Add(new System.Net.Mail.MailAddress(email));
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.Body = body;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.High;

                System.Net.Mail.SmtpClient smpt = new System.Net.Mail.SmtpClient();
                smpt.UseDefaultCredentials = false;
                smpt.Host = "mail.digital53.com";

                smpt.Port = 587;

                smpt.EnableSsl = false;

                smpt.Credentials = new System.Net.NetworkCredential("supportteam@digital53.com ", "Support123$");

                smpt.Send(msg);
                return true;

            }

            catch (Exception e)
            {
                return false;
            }



        }

        //public string Gettems()
        //{
        //    string Getvalue = drptemplete.SelectedValue.ToString();
        //    return Getvalue;

        //}

        protected void drptemplete_SelectedIndexChanged(object sender, EventArgs e)
        {

            // txtoverviewediter.Text = markup;



        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string markup = "<html xmlns:v='urn:schemas-microsoft-com:vml' xmlns='http://www.w3.org/1999/xhtml'><head><meta name='viewport' content='text/html; charset=utf-8; width=device-width' http-equiv='Content-Type'><title>Digital Edge Solutions Computer Company</title><style type='text/css'>.outlook {background-color: #ffffff; display: none;display: none !important;}.ReadMsgBody {width: 100%; background-color: #f5f5f5;}.ExternalClass {width: 100%;background-color: #f5f5f5;}body {width: 100%;background-color: #f5f5f5;margin: 0 auto !important;}.hidetop {display: none;} @media screen and (max-width:639px) {*[class=mobemailfullwidth] {width: 100% !important;height: auto !important;}*[class=emailcolsplit] {float: left !important;width: 100% !important;}*[class=hidecellinmobile] {display: none !important;}*[class=spacer] {margin-top: 20px !important; }*[class=offer] {font-size: 30px !important;line-height: 36px !important;}.hideimage {display: none;}*[class=subhdr] {font-size: 18px !important;line-height: 21px !important; } *[class=phonenumber] {text-align: center !important;} *[class=disclaimer] {padding: 10px;}.ExternalClass * {line-height: 100%; }*[class=threshold] {text-align: left; font-size: 28px !important; line-height: 30px !important; }*[class=threshold2] { text-align: left; font-size: 18px !important;line-height: 22px !important;}}.ExternalClass * {line-height: 100%;}/* CLIENT-SPECIFIC STYLES - templates 1232 &amp; 3496 */img {-ms-interpolation-mode: bicubic;}/* Force IE to smoothly render resized images. */ #outlook a {padding: 0;} /* Force Outlook 2007 and up to provide a 'view in browser' message. */table {mso-table-lspace: 0pt; mso-table-rspace: 0pt;} /* Remove spacing between tables in Outlook 2007 and up. */.ReadMsgBody {width: 100%;}.ExternalClass {width: 100%;}/* Force Outlook.com to display emails at full width. */ p, a, li, td, blockquote {mso-line-height-rule: exactly;}/* Force Outlook to render line heights as they're originally set. */a[href^='tel'], a[href^='sms'] {color: inherit;cursor: default;text-decoration: none;}/* Force mobile devices to inherit declared link styles. */ p, a, li, td, body, table, blockquote {-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;}/* Prevent Windows- and Webkit-based mobile platforms from changing declared text sizes. */.ExternalClass, .ExternalClass p, .ExternalClass td, .ExternalClass div, .ExternalClass span, .ExternalClass font { line-height: 100%; }/* Force Outlook.com to display line heights normally. *//* General 'Download App' for mobile view code. Templates 485 &amp; 514 */.mobile, td[class='mobile'], table[class='mobile'], img[class='mobile'], div[class='mobile'], tr[class='mobile'] {display: inline-block !important;width: auto !important;overflow: visible !important;height: auto !important;max-height: inherit !important;font-size: 15px !important;line-height: 21px !important;visibility: visible !important;        }        #prehdr {display: none !important;visibility: hidden !important;        }        @font-face {font-family: 'Boing-Bold';src: url('http://img1.wsimg.com/ux/fonts/1.3/eot/Boing-Bold.eot?#iefix') format('embedded-opentype'), url('http://img1.wsimg.com/ux/fonts/1.3/woff2/Boing-Bold.woff2') format('woff2'), url('http://img1.wsimg.com/ux/fonts/1.3/woff/Boing-Bold.woff') format('woff'), url('http://img1.wsimg.com/ux/fonts/1.3/ttf/Boing-Bold.ttf') format('truetype');font-weight: 400;font-style: normal;        }    </style></head><body bgcolor='#ffffff' backcolo='backcolo' style='min-width:100%; margin:0; -webkit-text-size-adjust:none; -ms-text-size-adjust:100%'><img height='0' width='0' border='0' alt='' src='http://img.secureserver.net/bbimage.aspx?pl=1&amp;isc=gdbbu1901&amp;e=newayosoftech%40gmail.com&amp;tid=1901&amp;eid=309348279&amp;mid=3ed57945-0afc-49bf-96c3-9f024d7c4f51'><br><img height='0' width='0' border='0' alt='' src='https://godaddy.sp1.convertro.com/view/vt/v1/godaddy/1/cvo.gif?cvosrc=bounceback.1901.gdbbu1901'><img height='1' width='1' border='0' alt=' ' src='https://media.godaddy.com/image?spacedesc=34990388_34990386_1x1_34990387_34990388&amp;random='><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center'><tbody><tr> <td valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#FDC400'><tbody><tr><td valign='top' align='center'><table width='600' cellspacing='0' cellpadding='0' border='0' align='center' class='mobemailfullwidth'><tbody><tr><td><table width='100%' cellspacing='0' cellpadding='0' border='0' align='center'><tbody><tr><td valign='top' align='center' style='display:none !important; visibility:hidden !important;'><font face='Arial, sans-serif' color='#333333' style='font-size:10px; -webkit-text-size-adjust:none'>Open this email to read the details of your order and find additional savings inside. Keep this email for your records.</font></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#FDC400'><tbody><tr><td style='line-height:10px;'><div style='height:10px;'><img height='10' border='0' style='display:block' src='http://imagesak.secureserver.net/promos/std/spc_trans.gif'></div></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td width='120' valign='middle' align='center' class='emailcolsplit'><div align='center'><a target='_blank' href='http://www.digital53.com'><img border='0' style='display: block; width: 70px; padding-bottom: 5px; padding-top: 5px;' alt='Didital Edge Solution ' src='http://corp.digital53.com/mail/Logo.png'></a></div></td><td width='520' valign='middle' class='emailcolsplit'><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td align='right' class='phonenumber' style='padding-left:5px; padding-right:5px; padding-bottom:5px'><font face='Arial, sans-serif' color='#ffffff' class='phonenumber' style='font-size:18px; -webkit-text-size-adjust:none; line-height:18px'><font color='#FFFFFF'><a style='color: #DA251C; text-decoration: none;' target='_blank' href='http://www.digital53.com'>Digital Edge Solutions Computer Company</a></font></font></td></tr>   <tr><td align='right' class='phonenumber' style='padding-left:5px; padding-right:5px'><font face='Arial, sans-serif' color='#010101' class='phonenumber' style='font-size:14px; -webkit-text-size-adjust:none; line-height:14px'>E-Commerce : <a style='color: #37b8eb; text-decoration: none;' target='_blank' href='http://e.digital53.com'>http://e.digital53.com</a></font></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#333333'><tbody><tr><td style='line-height:10px;'><div style='height:10px;'><img height='10' border='0' style='display:block' src='http://imagesak.secureserver.net/promos/std/spc_trans.gif'></div></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#77c043'><tbody><tr><td valign='top' align='center'><table width='640' cellspacing='0' cellpadding='0' border='0' align='center' class='mobemailfullwidth'><tbody><tr><td style='padding-top:40px; padding-bottom:40px; padding-left:20px; padding-right:20px'> <table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td align='left' style='padding: 10px 10px 0; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>   Dear  <b>" + txtname.Text + "</b> ,</font></td>  <td align='left' style='padding: 10px 10px 0; text-align: justify;'>   <font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>Date :" + DateTime.Now.ToShortDateString() + "</font></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td valign='baseline' bgcolor='#ffffff' align='left' style='padding: 10px 10px 0px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'><br>Many thanks for the opportunity given to us to meet you at your premises " + txtname.Text + " we as Digital Edge Take pride to have esteem customer like you at our umbrella and we will be more than happy to server your organization. <br><br></font></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td align='left' style='padding: 10px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>We deal in the infrastructure product that can have your all the servers converted on our Suse Linux and you get a full Live DR Plan. We do care for your existing important servers as Machine and they can be saved using our MicroFocus product where you will receive back your entire server within an Hour back into operation.</font></td><td valign='middle' bgcolor='#ffffff' align='left' style='padding-left: 0px; padding-right: 10px; padding-top: 0px; text-align: justify;'><a href='http://corp.digital53.com/Netiq'><img src='http://corp.digital53.com/mail/Img/Step2.png' style='width: 192px; height: 78px;'></a></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td valign='middle' bgcolor='#ffffff' align='left' style='padding-left: 10px; padding-right: 0px; padding-top: 0px; text-align: justify;'><a href='http://corp.digital53.com/'><img style='' src='http://corp.digital53.com/mail/Img/CRMmail.png'></a></td><td align='left' style='padding: 10px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>We are specialized in <a href='http://e.digital53.com/acm/Index.aspx'>CRM</a>, Business Process Management used as core of the <a href='http://corp.digital53.com'>ERP</a> (GL, SL, Inventory, Purchase, Sales, Fixed Assets, <a href='http://hrm.digital53.com/'>HRM</a>, Ticket Management, Flow Management, Project Management and Schedule Management). Our unique products comes with the full online <a href='http://corp.digital53.com'>SAAS based solutions</a>.</font></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td align='left' style='padding: 10px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>The Help Desk Management solutions is a unique product that can cater all your IT Assets managed that includes Facilities Management (Buildings), Services, Telephony and much more.</font></td><td valign='middle' bgcolor='#ffffff' align='left' style='padding-left: 0px; padding-right: 10px; padding-top: 0px; text-align: justify;'><a href='http://corp.digital53.com/HouseOnTheHill'><img style='width: 105px; height: 104px;' src='http://corp.digital53.com/mail/Img/HotH-logo-square-150.jpg'></a></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff' align='center' class='mobemailfullwidth'><tbody><tr><td valign='middle' bgcolor='#ffffff' align='left' style='padding-left: 10px; padding-right: 0px; padding-top: 0px; text-align: justify;'><a href='http://corp.digital53.com/DRBD'><img src='http://corp.digital53.com/mail/Img/recovery%20Logo.png' style='width: 125px; height: 124px;'></a></td><td align='left' style='padding: 10px; text-align: justify;'><font face='Arial, sans-serif' color='#333333' style='font-size: 14px; -webkit-text-size-adjust: none; line-height: 20px;  '>With our unique expertise the Networking, CCTV, Blade Server Installation, Annual Maintenance Contracts, Novell Netware Expertise, Linux/Unix Expertise. Where which we do cover many domain that to handle the various way of the IT Department Consultancy including RFP, RFQ, Tending Process, SOP and much more .</font></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#fedc45'><tbody><tr><td valign='top' align='center'><table width='640' cellspacing='0' cellpadding='0' border='0' align='center' class='mobemailfullwidth'><tbody><tr><td style='padding-top:20px; padding-bottom:20px; padding-left:20px; padding-right:20px'><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td valign='middle' align='left' style='padding-left:0px; padding-right:0px; padding-top:0px; padding-bottom:5px'><a style='text-decoration:none; font-size:20px;' href='#'><font face='Arial, sans-serif' color='#333333' style='font-size:17px; -webkit-text-size-adjust:none; line-height:22px'><strong>We urge a call from your end to our expert on +965-99144172 / 22660781 .</strong></font></a></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td valign='middle' align='left' style='padding-left:0px; padding-right:0px; padding-top:5px; padding-bottom:0px'><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>Email : <a href='mailto:sales@digital53.com'>sales@digital53.com</a></font><br><font face='Arial, sans-serif' color='#333333' style='font-size:14px; -webkit-text-size-adjust:none; line-height:20px'>Website : <a target='_blank' href='http://www.digital53.com'>www.digital53.com</a>  </font></td><td valign='middle' align='left' style='padding: 5px 0px; width: 288px; height: 51px;'><a target='_blank' href='https://twitter.com/deonlineq8'><img style='width: 39px;' src='http://corp.digital53.com/mail/Img/tweeter.png'></a><a target='_blank' href='https://kw.linkedin.com/in/joharmandav'><img style='width: 39px;' src='http://corp.digital53.com/mail/Img/in.png'></a><a target='_blank' href='https://www.instagram.com/deonlineq8'><img style='width: 39px;' src='http://corp.digital53.com/mail/Img/image.png'></a><a target='_blank' href='https://www.facebook.com/digital53'><img style='width: 39px;' src='http://corp.digital53.com/mail/Img/fb.png'></a></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table><table width='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#bebebe'><tbody><tr><td valign='top' align='center'><table width='640' cellspacing='0' cellpadding='0' border='0' align='center' class='mobemailfullwidth'><tbody><tr><td style='padding-top:20px; padding-bottom:20px; padding-left:20px; padding-right:10px'><table width='100%' cellspacing='0' cellpadding='0' border='0'><tbody><tr><td valign='middle' align='left' style='padding-left:0px; padding-right:0px; padding-top:5px; padding-bottom:0px'><div class='social-c'> <a href='#'></a><a href='#' class='t-icon'></a><a href='#' class='g-icon'></a><a href='#' class='y-icon'></a> </div><div><center><a target='_blank' href='http://e.digital53.com/Subscribe.aspx'>SafeUnsubscribe&trade; " + txtmailsend.Text + " </a><br> Forward email  | <a target='_blank' href='http://e.digital53.com/Profile.aspx'>  Update profile  </a>|  About our service provider<br>Sent by newsletter@channelsmedia.me<br><table>  </table><br></center></div><font face='Arial, sans-serif' color='#333333' style='font-size:12px; -webkit-text-size-adjust:none; line-height:20px'>Copyright &copy; 1999-2016 Digital Edge Solutions Company. Al-Mulhum Complex Ground Floor, Shop #2 and 3, HawallyP. O. Box No. 3552 Hawally, Kuwait (AG). All rights reserved.</font></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></body></html>";
            sendEmail(markup, txtmailsend.Text);
            txtname.Text = "";
            txtmailsend.Text = "";
            lblMsg.Text = "Mail Are Send Successfull..";
            pnlSuccessMsg.Visible = true;
        }
    }
}