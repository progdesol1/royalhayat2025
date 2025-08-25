<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DayReport.aspx.cs" Inherits="Web.ReportMst.DayReport" %>

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
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
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
                        <h2 style="text-align: center;">&nbsp;<asp:Label ID="lblHead" Style="margin-left: -100px;" Font-Bold="true" runat="server" Text="Daily Movement Report"></asp:Label>
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
                                                    <div class ="row">
                                                        <div class="col-md-6">
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label31" class="col-md-4 control-label" Text="Log Only:"></asp:Label>
                                                                        <div class="col-md-8">
                                                                            <asp:CheckBox ID="chklog" runat="server" AutoPostBack="true" />
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                     
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="lblActive1s" class="col-md-4 control-label" Text="Action Start Date"></asp:Label>

                                                                <div class="col-md-8">
                                                                    <asp:TextBox ID="txtdateFrom" Placeholder="dd/MMM/yyyy" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="TextBoxtxtdateFrom_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateFrom" Format="dd/MMM/yyyy"></cc1:CalendarExtender>
                                                                    <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtdateFrom" ValidChars="/" FilterType="Custom, numbers" runat="server" />--%>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label22" class="col-md-4 control-label" Text="Action To Date"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox ID="txtdateTO" Placeholder="dd/MMM/yyyy" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtendertxtdateTO" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateTO" Format="dd/MMM/yyyy"></cc1:CalendarExtender>
                                                                    <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtdateTO" ValidChars="/" FilterType="Custom, numbers" runat="server" />--%>
                                                                </div>
                                                            </div>
                                                        </div>


                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label7" class="col-md-4 control-label" Text="Action From"></asp:Label>

                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpactionfrom" runat="server" AutoPostBack="true" CssClass="form-control">
                                                                        <asp:ListItem Value="0" Text="--Select Action--"></asp:ListItem>
                                                                        <asp:ListItem Value="2101401" Text="2101401 - Immediate action"></asp:ListItem>
                                                                        <asp:ListItem Value="2101402" Text="2101402 - Follow-up"></asp:ListItem>
                                                                        <asp:ListItem Value="2101403" Text="2101403 - Corrective action"></asp:ListItem>
                                                                        <asp:ListItem Value="2101404" Text="2101404 - Investigation results"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpactionfrom" ErrorMessage="select Department"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">

                                                                <asp:Label runat="server" ID="lblactionto" class="col-md-4 control-label" Text="To Department"></asp:Label>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="drpactionTo" runat="server" AutoPostBack="true" CssClass="form-control">
                                                                        <asp:ListItem Value="0" Text="--Select Action--"></asp:ListItem>
                                                                        <asp:ListItem Value="2101404" Text="2101404 - Investigation results"></asp:ListItem>
                                                                        <asp:ListItem Value="2101403" Text="2101403 - Corrective action"></asp:ListItem>
                                                                        <asp:ListItem Value="2101402" Text="2101402 - Follow-up"></asp:ListItem>
                                                                        <asp:ListItem Value="2101401" Text="2101401 - Immediate action"></asp:ListItem>


                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpdepartmentto" ErrorMessage="select Department"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label1" class="col-md-4 control-label" Text="From Department"></asp:Label>

                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpdepartmentfrom" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpdepartmentfrom" ErrorMessage="select Department"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">

                                                                <asp:Label runat="server" ID="Label2" class="col-md-4 control-label" Text="To Department"></asp:Label>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="drpdepartmentto" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpdepartmentto" ErrorMessage="select Department"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label11" class="col-md-4 control-label" Text="From Category"></asp:Label>

                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpcategoryfrom" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpcategoryfrom" ErrorMessage="select Category "></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label14" class="col-md-4 control-label" Text="To Category"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpcategoryto" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpcategoryto" ErrorMessage="select Category"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label17" class="col-md-4 control-label" Text="From Sub Category"></asp:Label>

                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpscategoryfrom" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpscategoryfrom" ErrorMessage="select sub Category"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label16" class="col-md-4 control-label" Text="To Sub Category"></asp:Label>

                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpscategoryto" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpscategoryto" ErrorMessage="select sub Category"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label26" class="col-md-4 control-label" Text="From Location"></asp:Label>

                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drplocationfrom" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drplocationfrom" ErrorMessage="select Location"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label19" class="col-md-4 control-label" Text="To Location"></asp:Label>

                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drplocationto" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drplocationto" ErrorMessage="select Location"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label20" class="col-md-4 control-label" Text="From Feedback Type"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpFeedbackypefrom" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>


                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label21" class="col-md-4 control-label" Text="To Feedback Type"></asp:Label>

                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpFeedbackypeto" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label23" class="col-md-4 control-label" Text="From Reported By"></asp:Label>

                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpreportfrom" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpreportfrom" ErrorMessage="Select reportedby"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label18" class="col-md-4 control-label" Text="To Reported By"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpreportto" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpreportto" ErrorMessage="Select reportedby"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label3" class="col-md-4 control-label" Text="Feedback No"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label4" class="col-md-4 control-label" Text="MRN No"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox ID="txtMRN" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label29" class="col-md-4 control-label" Text="Text in Feedback"></asp:Label>

                                                                <div class="col-md-8">
                                                                    <asp:TextBox ID="txtFeedback" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2"></div>
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
                                                            <span class="caption-subject bold uppercase">Daily Movement Report</span>
                                                        </div>
                                                        <div class="tools"></div>
                                                    </div>
                                                    <div class="portlet-body">
                                                        <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                            <thead>
                                                                <tr>
                                                                 <th style="text-align: center"><strong>No</strong></th>
                                                                    <th style="text-align: center"><strong>Main Feedback Reference / Daily Activity</strong></th>
                                                                    <th style="text-align: center"><strong>Created By</strong></th>
                                                                    <th style="text-align: center"><strong>Action Status</strong></th>
                                                                    <th style="text-align: center"><strong>Patient</strong></th>
                                                                     <th style="text-align: center"><strong>MRN</strong></th>
                                                                    <th style="text-align: center"><strong>Investigation Result</strong></th>
                                                                     <th style="text-align: center"><strong>Total Time</strong></th>
                                                                     <th style="text-align: center"><strong>Attachment Link</strong></th>

                                                                    
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListView1_ItemCommand">
                                                                    <ItemTemplate>
                                                                         <tr>
                                                                            <td style="text-align: center;">
                                                                                <a href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("FeedbackNumber")%>' target="_blank">
                                                                                     <asp:Label ID="Label24" runat="server" Text='<%# Eval("FeedbackNumber")%>'></asp:Label>
                                                                                    <asp:Label ID="lblFeedback" runat="server" Visible="false" Text='<%# Eval("FeedbackNumber")%>'></asp:Label>
                                                                                     <asp:Label ID="lblsub" runat="server" Visible="false" Text='<%# Eval("Subject")%>'></asp:Label>
                                                                                       <asp:Label ID="lblre" runat="server" Visible="false" Text='<%# Eval("ActivityNote")%>'></asp:Label>
                                                                                    <asp:Label ID="lbluplo" runat="server" Visible="false" Text='<%# Eval("UploadDate")%>'></asp:Label>
                                                                                     <asp:Label ID="lblmylineno" runat="server" Visible="false" Text='<%# Eval("MyLineNo")%>'></asp:Label>
                                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("FeedbackNumber") %>'></asp:Label>
                                                                                </a>
                                                                            </td>
                                                                         
                                                                            <td style="text-align: left;">
                                                                                <a href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("FeedbackNumber")%>' target="_blank">
                                                                                      <asp:Label ID="Label28" runat="server" Text=' <%#Eval("FeedbackNumber")%>'></asp:Label><br /><br />
                                                                                     <asp:Label ID="Label30" runat="server" Text=' <%#Eval("UploadDate")%>'></asp:Label> - 
                                                                                      <asp:Label ID="lbltickdk" runat="server" Text=' <%#Eval("Reference")%>'></asp:Label>
                                                                                    <br /><br />
                                                                                      <asp:Label ID="Label31" runat="server" Text=' <%#Eval("UploadDate")%>'></asp:Label> - 
                                                                                      <asp:Label ID="Label32" runat="server" Text=' <%#Eval("ActivityNote")%>'></asp:Label>
                                                                                    <asp:Label ID="lblID2" Visible="false" runat="server" Text='<%# Eval("FeedbackNumber") %>'></asp:Label>
                                                                                </a>
                                                                            </td>
                                                                            <td style="text-align: center;">
                                                                                <a href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("FeedbackNumber")%>' target="_blank">
                                                                                    <asp:Label ID="lbltickcat" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                                                                                    <asp:Label ID="lblID3" Visible="false" runat="server" Text='<%# Eval("FeedbackNumber") %>'></asp:Label>
                                                                                </a>
                                                                            </td>
                                                                              <td style="text-align: center;">
                                                                               <a  href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("FeedbackNumber")%>'  target="_blank">
                                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                                                                    <asp:Label ID="Label8" Visible="false" runat="server" Text='<%# Eval("FeedbackNumber") %>'></asp:Label>
                                                                                </a>
                                                                              </td>
                                                                              <td style="text-align: center;">
                                                                               <a  href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("FeedbackNumber")%>'  target="_blank">
                                                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("Patient")%>'></asp:Label><br />
                                                                                    <asp:Label ID="Label27" runat="server" Text="Mob:-"></asp:Label><br />
                                                                                     <asp:Label ID="lblconta" runat="server" Text=' <%# Eval("Contact")%>'></asp:Label><br />
                                                                                    <asp:Label ID="Label10" Visible="false" runat="server" Text='<%# Eval("FeedbackNumber") %>'></asp:Label>
                                                                                </a>
                                                                              </td>
                                                                               <td style="text-align: center;">
                                                                                <a href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("FeedbackNumber")%>' target="_blank">
                                                                                    <asp:Label ID="lblmrn" runat="server" Text='<%# Eval("MRN")%>'></asp:Label>
                                                                                    <asp:Label ID="lblID1" Visible="false" runat="server" Text='<%# Eval("FeedbackNumber") %>'></asp:Label>
                                                                                </a>
                                                                               </td>
                                                                              <td style="text-align: center;">
                                                                               <a  href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("FeedbackNumber")%>'  target="_blank">
                                                                                    <asp:Label ID="Label12" runat="server" Text= '<%# getinvestigation(Convert.ToInt32(Eval("investigation"))) %>'></asp:Label>   
                                                                                    <asp:Label ID="lblinvest" runat="server" Visible="false" Text='<%# Eval("investigation")%>'></asp:Label>                                                                                   
                                                                                   </a>
                                                                              </td>    
                                                                              <td style="text-align: center;">
                                                                               <a  href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("FeedbackNumber")%>'  target="_blank">
                                                                                    <asp:Label ID="Label25" runat="server" Text='<%# getlink(Convert.ToInt32(Eval("FeedbackNumber"))) %>'></asp:Label>   
                                                                                    <asp:Label ID="Label33" runat="server" Text='<%# Eval("UseReciepeName")%>'></asp:Label>                                                                                   
                                                                                   </a>
                                                                              </td> 
                                                                             <td style="text-align: center;">
                                                                               <a  href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("FeedbackNumber")%>'  target="_blank">
                                                                                    <asp:Label ID="Label13" runat="server" Text='<%# getlink(Convert.ToInt32(Eval("FeedbackNumber"))) %>'></asp:Label>   
                                                                                    <asp:Label ID="Label15" runat="server" Visible="false" Text='<%# Eval("investigation")%>'></asp:Label>                                                                                   
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
