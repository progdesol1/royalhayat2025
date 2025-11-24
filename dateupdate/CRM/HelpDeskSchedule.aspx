<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpDeskSchedule.aspx.cs" Inherits="Web.CRM.HelpDeskSchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/Master/UserContol/Addappoiment.ascx" TagPrefix="uc1" TagName="Addappoiment" %>--%>


<!DOCTYPE html>

<html lang="en">
<!--<![endif]-->
<!-- BEGIN HEAD -->

<head runat="server">
    <meta charset="utf-8" />
    <title>Digital53 | Calendar</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="Preview page of Digital53 for calendar page" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/fullcalendar/fullcalendar.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout4/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout4/css/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout4/css/custom.min.css" rel="stylesheet" type="text/css" />
    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/moment.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL SCRIPTS -->

    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link href="../assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />



    <link href="../assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout4/css/layout.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout4/css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/components-rounded.css" rel="stylesheet" type="text/css" />

    <script src="assets/global/scripts/app.min.js" type="text/javascript"></script>
    <!-- END THEME LAYOUT STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />
    <style>
        .demo-preview__frame {
            border: none;
            -webkit-box-flex: 1;
            -ms-flex: 1;
            flex: 1;
            position: relative;
            z-index: 1;
            width: 100%;
            height: 400px;
        }
    </style>
    <style type="text/css">
        /* Dropdown Button */
        .dropbtn {
            background-color: #5b9bd1;
            color: white;
            padding: 16px;
            font-size: 16px;
            border: none;
            height: 35px;
        }

        /* The container <div> - needed to position the dropdown content */
        .dropdown {
            position: relative;
            display: inline-block;
        }

        /* Dropdown Content (Hidden by Default) */
        .dropdown-content {
            display: none;
            position: relative;
            background-color: #f1f1f1;
            min-width: 110px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            z-index: 1;
        }

            /* Links inside the dropdown */
            .dropdown-content a {
                color: black;
                padding: 12px 16px;
                text-decoration: none;
                display: block;
            }

                /* Change color of dropdown links on hover */
                .dropdown-content a:hover {
                    background-color: #ddd;
                }

        /* Show the dropdown menu on hover */
        .dropdown:hover .dropdown-content {
            display: block;
        }

        /* Change the background color of the dropdown button when the dropdown content is shown */
        .dropdown:hover .dropbtn {
            background-color: #3e8e41;
            height: 35px;
        }
    </style>
    <script type="text/javascript">
        function PopClear1() {
            document.getElementById("txtexpecteddate").value = "";
            document.getElementById("txtAppoitmentTitle").value = "";
            document.getElementById("txtRemark").value = "";
            document.getElementById("drpReceip").value = "0";
            document.getElementById("drpCustomer").value = "0";
            document.getElementById("drpEmployee").value = "0";
            document.getElementById("drpstares").value = "0";
            document.getElementById("drpReceip").disabled = false;
            document.getElementById("drpCustomer").disabled = false;
        }
    </script>
</head>
<!-- END HEAD -->

<body class="page-container-bg-solid page-header-fixed page-sidebar-closed-hide-logo">
    <form runat="server">
        <asp:ScriptManager ID="toolscriptmanagerID" runat="server">
        </asp:ScriptManager>
        <!-- BEGIN CONTENT BODY -->
        <div class="page-content">


            <!-- BEGIN PAGE BASE CONTENT -->

            <div class="row">
                <div class="col-md-12">
                    <div class="portlet light portlet-fit bordered calendar">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class=" icon-layers font-green"></i>
                                <span class="caption-subject font-green sbold uppercase">Help Desk Schedule</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnappoint" class="btn default" runat="server" style="display:none;" Text="Add Appointment" OnClientClick="PopClear1()" />
                                <%--<a class="btn default" data-toggle="modal" href="#full">Add Appointment
                                </a>--%>
                            </div>
                        </div>


                        <div class="portlet-body">
                            <div class="row">

                                <div class="col-md-6 col-sm-12">
                                    <div id="calendar" class="has-toolbar"></div>
                                </div>
                                <div class="col-md-6 col-sm-12">

                                    <asp:Button ID="Button1" runat="server" style="display:none;" CssClass="btn btn-default" Text="Default" OnClick="Button1_Click" />
                                </div>


                                <%--List Appointment--%>
                                <div class="col-md-6" style="margin-top: 41px;">
                                    <div class="portlet box green-haze">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-globe"></i>
                                                <asp:Label runat="server" ID="Label8" Text="Activity Calendar"></asp:Label>
                                            </div>
                                            <div class="tools">
                                            </div>
                                            <div class="actions btn-set">
                                                <asp:Button ID="btnShowAll" runat="server" CssClass="btn btn-sm blue" Text="Show All" OnClick="btnShowAll_Click" />
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div>
                                                <%--class="table-scrollable"--%>
                                                <table class="table table-striped table-bordered table-hover" id="sample_2">
                                                    <thead>
                                                        <tr>
                                                            <th></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblapp1">Act #</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblapp2" Text="Act Type"></asp:Label></th>
                                                            <th>Loc/Dept</th>
                                                            <th>Performer</th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblapp3" Text="Activity"></asp:Label></th>
                                                            
                                                            <th>
                                                                <asp:Label runat="server" ID="Label10" Text="Exp Start Dt"></asp:Label></th>
                                                            <th></th>
                                                            <%--<th style="width: 60px;">ACTION</th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" OnItemDataBound="Listview1_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <tr id="ItemPlaceholder" runat="server">
                                                                </tr>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%# Container.DataItemIndex + 1 %></td>
                                                                    <td>
                                                                        <asp:LinkButton ID="LinkjobbyID" CommandName="btnviewjob" CommandArgument='<%# Eval("MasterCODE") %>' runat="server">
                                                                            <asp:Label ID="lblapp4" runat="server" Text='<%# Eval("MasterCODE") %>'>'></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="LinkjobbyCustomer" CommandName="btnviewjob" CommandArgument='<%# Eval("MasterCODE") %>' runat="server">
                                                                            <asp:Label ID="lblapp5" CssClass="label label-sm" runat="server" Text='<%#Eval("MyStatus")%>'></asp:Label>
                                                                            <label style="color: black;">BY-</label>
                                                                            <asp:Label ID="Label27" runat="server" Style="color: black;" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label28" runat="server" Text='<%# GetLOCDEPT(Convert.ToInt32(Eval("TickDepartmentID")),Convert.ToInt32(Eval("TickPhysicalLocation"))) %>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="Label29" runat="server" Text='<%# Performer(Convert.ToInt32(Eval("Emp_ID"))) %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="LinkjobbyTitle" CommandName="btnviewjob" CommandArgument='<%# Eval("MasterCODE") %>' runat="server">
                                                                            <asp:Label ID="lblapp6" runat="server" Text='<%# Eval("Reference") %>'></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </td>
                                                                    
                                                                    <td>
                                                                        <asp:LinkButton ID="LinkjobbyEXPDT" CommandName="btnviewjob" CommandArgument='<%# Eval("MasterCODE") %>' runat="server">
                                                                            <asp:Label ID="Label9" runat="server" Text='<%# Convert.ToDateTime(Eval("UPDTTIME")).ToString("dd-MMM-yyyy hh:mm tt") %>'></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </td>
                                                                    <td></td>
                                                                    <%--<td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:LinkButton ID="btnAPPEdit" CommandName="btnAPPEdit" CommandArgument='<%# Eval("MasterCODE") %>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="btnAPPDelete" CommandName="btnAPPDelete" CommandArgument='<%# Eval("MasterCODE") %>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>--%>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--List Job--%>

                                <div class="col-md-6" style="float: right;">
                                    <div class="portlet box yellow-crusta">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-globe"></i>
                                                <asp:Label runat="server" ID="Label11" Text=" Detail Activity"></asp:Label>
                                            </div>
                                            <div class="tools">
                                            </div>
                                            <div class="actions btn-set">
                                                <asp:LinkButton ID="LinkAddjob" runat="server" Text="Add New Job" CssClass="btn btn-sm yellow-gold" Style="display: none;"></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <%--<div class="table-scrollable">--%>
                                                <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                <asp:Label ID="Label13" runat="server" Text="Act #"></asp:Label>
                                                            </th>
                                                            <th>
                                                                <asp:Label runat="server" ID="Label122" Text="Activity Detail"></asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="Label132" Text="Reference"></asp:Label></th>
                                                            <th style="width: 100px;">
                                                                <asp:Label runat="server" ID="Label142" Text="Loc/Dept"></asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="Label152" Text="Notes"></asp:Label></th>
                                                           
                                                            <th style="width: 60px;">ACTION</th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:ListView ID="ListView2" runat="server" OnItemCommand="ListView2_ItemCommand" OnItemDataBound="ListView2_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label14" runat="server" Text='<%# Eval("MasterCODE") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label15" runat="server" Text='<%# Eval("Version")+" / "+ Convert.ToDateTime(Eval("InitialDate")).ToString("dd-MMM-yyyy hh:mm tt") %>'></asp:Label><br />
                                                                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("ActivityPerform") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label16" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label17" runat="server" Text='<%# GetLOCDEPT(Convert.ToInt32(Eval("TickDepartmentID")),Convert.ToInt32(Eval("TickPhysicalLocation"))) %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label18" runat="server" Text=""></asp:Label>
                                                                    </td>

                                                                    <td>
                                                                        <div class="dropdown">
                                                                            <button class="dropbtn" style="padding-bottom: 0px; padding-top: 0px; height: auto">Action</button>
                                                                            <div class="dropdown-content">
                                                                                <asp:Literal ID="ltr" runat="server"></asp:Literal> 
                                                                                
                                                                            <%--<asp:LinkButton Style="padding-bottom: 0px; padding-top: 0px; height: auto" ID="LinkJobEdit" CommandName="LinkJobEdit" CommandArgument='<%# Eval("MasterCODE") %>' runat="server" Text="Reply">                                                                                                                                    
                                                                                </asp:LinkButton>

                                                                                <asp:LinkButton Style="padding-bottom: 0px; padding-top: 0px; height: auto" ID="LinkJobDelete" CommandName="LinkJobDelete" CommandArgument='<%# Eval("MasterCODE") %>' runat="server" Text="Close">                                                                                                                                    
                                                                                </asp:LinkButton>

                                                                                <asp:LinkButton Style="padding-bottom: 0px; padding-top: 0px; height: auto" ID="LinkJobEMPAssign" CommandName="LinkJobEMPAssign" CommandArgument='<%# Eval("MasterCODE") %>' runat="server" Text="Forward">                                                                                                                                    
                                                                                </asp:LinkButton>--%>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </tbody>
                                                </table>
                                            <%--</div>--%>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <%--List Appointment old--%>
                        <div class="row">
                        </div>

                        <asp:Panel ID="pnlappoint" runat="server" CssClass="modalPopup" Style="display: none">
                            <div class="modal-dialog">
                                <div class="modal-content" style="margin-top: -30%;">
                                    <div class="modal-header">
                                        <h4 class="modal-title">Add Apppinment</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="form-horizontal" role="form">

                                            <div class="row">
                                                <asp:Panel ID="Panel7" runat="server" Visible="false">
                                                    <div class="alert alert-danger">
                                                        <strong>Error!</strong>
                                                        Apppinment Allready Exist..
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label1" runat="server" class="col-md-12 control-label" Text="Use Service Template" Style="text-align: left; margin-left: 10px; font-size: 16px; font-weight: 600;"></asp:Label>
                                                            <div class="col-md-12">
                                                                <asp:DropDownList ID="drpReceip" runat="server" CssClass="table-group-action-input form-control" AutoPostBack="true" OnSelectedIndexChanged="drpReceip_SelectedIndexChanged"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpReceip" ErrorMessage="Receip Required" Display="Dynamic" ForeColor="#a94442" SetFocusOnError="true" InitialValue="0" ValidationGroup="appoint"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label2" runat="server" class="col-md-12 control-label" Text="Customer" Style="text-align: left; margin-left: 10px; font-size: 16px; font-weight: 600;"></asp:Label>
                                                            <div class="col-md-12">
                                                                <asp:DropDownList ID="drpCustomer" runat="server" CssClass="table-group-action-input form-control" AutoPostBack="true" OnSelectedIndexChanged="drpCustomer_SelectedIndexChanged"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpCustomer" ErrorMessage="Customer Required" Display="Dynamic" ForeColor="#a94442" SetFocusOnError="true" InitialValue="0" ValidationGroup="appoint"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label3" runat="server" class="col-md-12 control-label" Text="Expected Start Date" Style="text-align: left; margin-left: 10px; font-size: 16px; font-weight: 600;"></asp:Label>
                                                            <div class="col-md-12">
                                                                <asp:TextBox ID="txtexpecteddate" runat="server" CssClass="form-control" placeholder="MMM/dd/yyyy"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtexpecteddate" Format="MMM/dd/yyyy" Enabled="True"></cc1:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtexpecteddate" ErrorMessage="Date Required" Display="Dynamic" ForeColor="#a94442" SetFocusOnError="true" ValidationGroup="appoint"></asp:RequiredFieldValidator>
                                                            </div>

                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label26" runat="server" Text="" class="col-md-12 control-label" Style="padding-top: 30px;"></asp:Label>
                                                            <div class="col-md-6">
                                                                <%--<asp:TextBox ID="txtexpectedTime" runat="server" CssClass="form-control" placeholder="hh:mm:ss AM"></asp:TextBox>--%>
                                                                <asp:DropDownList ID="drpTimejon" CssClass="form-control input-small" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label4" runat="server" class="col-md-12 control-label" Text="Appoinment Title" Style="text-align: left; margin-left: 10px; font-size: 16px; font-weight: 600;"></asp:Label>
                                                            <div class="col-md-12">
                                                                <asp:TextBox ID="txtAppoitmentTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAppoitmentTitle" ErrorMessage="Tital Required" Display="Dynamic" ForeColor="#a94442" SetFocusOnError="true" ValidationGroup="appoint"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label5" runat="server" class="col-md-12 control-label" Text="Peferred Eployee" Style="text-align: left; margin-left: 10px; font-size: 16px; font-weight: 600;"></asp:Label>
                                                            <div class="col-md-12">
                                                                <asp:DropDownList ID="drpEmployee" runat="server" CssClass="table-group-action-input form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpEmployee" ErrorMessage="Employee Required" Display="Dynamic" ForeColor="#a94442" SetFocusOnError="true" InitialValue="0" ValidationGroup="appoint"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label7" runat="server" class="col-md-12 control-label" Text="Status" Style="text-align: left; margin-left: 10px; font-size: 16px; font-weight: 600;"></asp:Label>
                                                            <div class="col-md-12">
                                                                <asp:DropDownList ID="drpstares" runat="server" CssClass="table-group-action-input form-control">
                                                                    <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                                                    <%--<asp:ListItem Text="No Status" Value="No Status"></asp:ListItem>--%>
                                                                    <asp:ListItem Text="Not Confirmed" Value="Not Confirmed"></asp:ListItem>
                                                                    <asp:ListItem Text="Confirmed" Value="Confirmed"></asp:ListItem>
                                                                    <%-- <asp:ListItem Text="No Answer" Value="No Answer"></asp:ListItem>
                                                            <asp:ListItem Text="In Waiting" Value="In Waiting"></asp:ListItem>
                                                            <asp:ListItem Text="Visited" Value="Visited"></asp:ListItem>
                                                            <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                                                            <asp:ListItem Text="Canceled" Value="Canceled"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="drpstares" ErrorMessage="Status Required" Display="Dynamic" ForeColor="#a94442" SetFocusOnError="true" InitialValue="0" ValidationGroup="appoint"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label6" runat="server" class="col-md-12 control-label" Text="Remark" Style="text-align: left; margin-left: 10px; font-size: 16px; font-weight: 600;"></asp:Label>
                                                            <div class="col-md-12">
                                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:LinkButton ID="linkclose" runat="server" class="btn default">Close</asp:LinkButton>

                                        <asp:Button ID="btnsaveAppoint" runat="server" class="btn green" Text="Save" ValidationGroup="appoint" OnClick="btnsaveAppoint_Click" />

                                    </div>
                                </div>
                            </div>

                        </asp:Panel>

                        <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" DynamicServicePath=""
                            BackgroundCssClass="modalBackground" CancelControlID="linkclose" Enabled="True"
                            PopupControlID="pnlappoint" TargetControlID="btnappoint">
                        </cc1:ModalPopupExtender>
                        <%--List Job old--%>
                        <div class="row">
                        </div>
                        <asp:Panel ID="pnljob" runat="server">
                            <div class="modal-dialog">
                                <div class="modal-content" style="margin-top: -30%;">
                                    <div class="modal-header">
                                        <h4 class="modal-title">Add Job</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="form-horizontal" role="form">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Label ID="Label22" runat="server" class="col-md-12 control-label" Text="Appointment" Style="text-align: left; margin-left: 10px; font-size: 16px; font-weight: 600;"></asp:Label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtJobAppointment" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Label ID="Label23" runat="server" class="col-md-12 control-label" Text="User Service Template" Style="text-align: left; margin-left: 10px; font-size: 16px; font-weight: 600;"></asp:Label>
                                                    <div class="col-md-12">
                                                        <asp:DropDownList ID="drpjobServiceTem" runat="server" CssClass="table-group-action-input form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Label ID="Label24" runat="server" class="col-md-12 control-label" Text="Job Title" Style="text-align: left; margin-left: 10px; font-size: 16px; font-weight: 600;"></asp:Label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtjobTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Label ID="Label25" runat="server" class="col-md-12 control-label" Text="Remark" Style="text-align: left; margin-left: 10px; font-size: 16px; font-weight: 600;"></asp:Label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtjobremark" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:LinkButton ID="JobClose" runat="server" class="btn default">close</asp:LinkButton>
                                        <asp:LinkButton ID="jobedit" runat="server" class="btn green" Text="Save" OnClick="jobedit_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
                            BackgroundCssClass="modalBackground" CancelControlID="JobClose" Enabled="True"
                            PopupControlID="pnljob" TargetControlID="LinkAddjob">
                        </cc1:ModalPopupExtender>
                    </div>
                </div>
            </div>

            <!-- END PAGE BASE CONTENT -->
        </div>

        <!-- END CONTENT BODY -->
    </form>
    <!--[if lt IE 9]>
<script src="assets/global/plugins/respond.min.js"></style>
<script src="assets/global/plugins/excanvas.min.js"></script> 
<script src="assets/global/plugins/ie8.fix.min.js"></script> 
<![endif]-->
    <!-- BEGIN CORE PLUGINS -->

    <!-- END THEME GLOBAL SCRIPTS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <%--        <script src="assets/apps/scripts/calendar.min.js" type="text/javascript"></script>--%>
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- BEGIN THEME LAYOUT SCRIPTS -->
    <script src="../assets/layouts/layout4/scripts/layout.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/layout4/scripts/demo.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <script src="../assets/admin/pages/scripts/table-managed.js"></script>
    <!-- END THEME LAYOUT SCRIPTS -->

    <%--    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>--%>
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/metronic.js" type="text/javascript"></script>
    <script src="../.assets/admin/layout4/scripts/layout.js" type="text/javascript"></script>
    <script src="../assets/admin/layout4/scripts/demo.js" type="text/javascript"></script>

    <script src="../assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
<%--    <script src="../assets/admin/pages/scripts/table-editable.js"></script>--%>
    <script src="../assets/admin/pages/scripts/table-managed.js"></script>

    <script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Demo.init(); // init demo features
            //TableEditable.init();
            TableManaged.init();
        });
    </script>

</body>

</html>
