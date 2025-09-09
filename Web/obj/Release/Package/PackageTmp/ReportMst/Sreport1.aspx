<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sreport1.aspx.cs" Inherits="Web.ReportMst.Sreport1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="Preview page of Metronic Admin Theme #4 for buttons extension demos" name="description" />
    <meta content="" name="author" />
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&amp;subset=all" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/data
        
        tables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/layouts/layout4/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/layouts/layout4/css/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="assets/layouts/layout4/css/custom.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="background-color: #ffffff">
            <table style="width: 100%; height: auto">
                <tr>
                    <td style="width: 10%;">
                        <%--<img src="assets/global/img/HealthBar.png" class="logo-default" style="margin-top: 10px; width: 250px;" />--%>
                        <%-- <asp:Image ID="HealtybarLogo" ImageUrl="assets/global/img/HealthBar.png" runat="server" />--%>
                    </td>
                    <td style="width: 90%;">
                        <h2 style="text-align: center;">&nbsp;<asp:Label ID="lblHead" Style="margin-left: -100px;" Font-Bold="true" runat="server" Text="Summary Management Report"></asp:Label>
                        </h2>
                    </td>
                </tr>
            </table>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-horizontal form-row-seperated">
                        <div class="portlet-body" style="background-color: #ffffff">
                            <%-- List --%>
                            <div class="portlet-body">
                                <div class="portlet-body form">
                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <div class="tab-pane active">
                                                <div class="form-body">

                                                    <div class="row">                                                       
                                                        <div class="col-md-3">
                                                            <asp:Button ID="btnsubmit" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnsubmit_Click1" ValidationGroup="Ticks" />
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label6" class="col-md-4 control-label" Text="Email"></asp:Label>

                                                                <div class="col-md-8">
                                                                    <asp:TextBox ID="txtpmail" runat="server" ValidationGroup="tmail" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-1"></div>
                                                        <div class="col-md-3">
                                                            <asp:Button ID="btnmail" runat="server" Text="Send by mail" CssClass="btn btn-info" OnClick="btnmail_Click" ValidationGroup="tmail" />
                                                        </div>
                                                    </div>

                                                    <div class="portlet-title">
                                                        <div class="caption font-dark">
                                                            <i class="icon-settings font-dark"></i>
                                                            <span class="caption-subject bold uppercase">Summary Management Report</span>
                                                        </div>
                                                        <div class="tools"></div>
                                                    </div>
                                                    <div class="portlet-body">
                                                        <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                            <thead>
                                                                <tr>
                                                                    <th style="text-align: center"><strong>Complaint No</strong></th>
                                                                    <th style="text-align: center"><strong>Reference</strong></th>
                                                                    <th style="text-align: center"><strong>Created By</strong></th>
                                                                    <th style="text-align: center"><strong>Action</strong></th>
                                                                    <th style="text-align: center"><strong>Status</strong></th>
                                                                    <th style="text-align: center"><strong>Patient</strong></th>
                                                                     <th style="text-align: center"><strong>MRN</strong></th>
                                                                    <%-- <th style="text-align: center"><strong>Reported By </strong></th>--%>
                                                                    <th style="text-align: center"><strong>Reported By </strong></th>
                                                                     <th style="text-align: center"><strong>Dept</strong></th>
                                                                    <th style="text-align: center"><strong>ActivityNotes</strong></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListView1_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td style="text-align: center;">
                                                                                <a href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("ComplaintNumber")%>' target="_blank">
                                                                                    <asp:Label ID="lblcomplain" runat="server" Text='<%# Eval("ComplaintNumber")%>'></asp:Label>
                                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ComplaintNumber") %>'></asp:Label>
                                                                                </a>
                                                                            </td>
                                                                         
                                                                            <td style="text-align: center;">
                                                                                <a href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("ComplaintNumber")%>' target="_blank">
                                                                                    <asp:Label ID="lbltickdk" runat="server" Text='<%#Eval("Reference")%>'></asp:Label>
                                                                                    <asp:Label ID="lblID2" Visible="false" runat="server" Text='<%# Eval("ComplaintNumber") %>'></asp:Label>
                                                                                </a>
                                                                            </td>
                                                                            <td style="text-align: center;">
                                                                                <a href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("ComplaintNumber")%>' target="_blank">
                                                                                    <asp:Label ID="lbltickcat" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                                                                                    <asp:Label ID="lblID3" Visible="false" runat="server" Text='<%# Eval("ComplaintNumber") %>'></asp:Label>
                                                                                </a>
                                                                            </td>
                                                                         
                                                                            <td style="text-align: center;">
                                                                               <a  href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("ComplaintNumber")%>'  target="_blank">
                                                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Action")%>'></asp:Label>
                                                                                    <asp:Label ID="lblID5" Visible="false" runat="server" Text='<%# Eval("ComplaintNumber") %>'></asp:Label>
                                                                                </a>
                                                                            </td>
                                                                              <td style="text-align: center;">
                                                                               <a  href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("ComplaintNumber")%>'  target="_blank">
                                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                                                                    <asp:Label ID="Label8" Visible="false" runat="server" Text='<%# Eval("ComplaintNumber") %>'></asp:Label>
                                                                                </a>
                                                                              </td>
                                                                              <td style="text-align: center;">
                                                                               <a  href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("ComplaintNumber")%>'  target="_blank">
                                                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("Patient")%>'></asp:Label>
                                                                                    <asp:Label ID="Label10" Visible="false" runat="server" Text='<%# Eval("ComplaintNumber") %>'></asp:Label>
                                                                                </a>
                                                                              </td>
                                                                               <td style="text-align: center;">
                                                                                <a href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("ComplaintNumber")%>' target="_blank">
                                                                                    <asp:Label ID="lblmrn" runat="server" Text='<%# Eval("MRN")%>'></asp:Label>
                                                                                    <asp:Label ID="lblID1" Visible="false" runat="server" Text='<%# Eval("ComplaintNumber") %>'></asp:Label>
                                                                                </a>
                                                                               </td>
                                                                              <td style="text-align: center;">
                                                                               <a  href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("ComplaintNumber")%>'  target="_blank">
                                                                                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("ReportedBy")%>'></asp:Label>                                                                               
                                                                                    <asp:Label ID="Label13" Visible="false" runat="server" Text='<%# Eval("ComplaintNumber") %>'></asp:Label>
                                                                                </a>
                                                                              </td>
                                                                             <td style="text-align: center;">
                                                                               <a  href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("ComplaintNumber")%>'  target="_blank">
                                                                                    <asp:Label ID="Label15" runat="server" Text='<%# Eval("Dept")%>'></asp:Label>
                                                                                    <asp:Label ID="Label24" Visible="false" runat="server" Text='<%# Eval("ComplaintNumber") %>'></asp:Label>
                                                                                </a>
                                                                            </td>
                                                                             <td style="text-align: center;">
                                                                               <a  href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("ComplaintNumber")%>'  target="_blank">
                                                                                    <asp:Label ID="Label25" runat="server" Text='<%# Eval("ActivityNote")%>'></asp:Label>
                                                                                    <asp:Label ID="Label27" Visible="false" runat="server" Text='<%# Eval("ComplaintNumber") %>'></asp:Label>
                                                                                </a>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </tbody>
                                                        </table>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="quick-nav-overlay"></div>
                            <%-- End List --%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
        <script src="assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
        <script src="assets/global/scripts/app.min.js" type="text/javascript"></script>
        <script src="assets/pages/scripts/table-datatables-buttons.min.js" type="text/javascript"></script>
        <script src="assets/layouts/layout4/scripts/layout.min.js" type="text/javascript"></script>
        <script src="assets/layouts/layout4/scripts/demo.min.js" type="text/javascript"></script>
        <script src="assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
        <script src="assets/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
