<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DriverDemo.aspx.cs" Inherits="Web.ReportMst.DriverDemo" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/ReportMst/GoogleMapForASPNet.ascx" TagPrefix="uc1" TagName="GoogleMapForASPNet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="Preview page of Digital53 for buttons extension demos" name="description" />
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
    <link rel="shortcut icon" href="favicon.ico" />
    <script type="text/javascript">
        // disable back *******************************************
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", -1);
        window.onunload = function () { null };
        //*********************************************************


        //JS right click and ctrl+(n,a,j) disable
        function disableCtrlKeyCombination(e) {
            //list all CTRL + key combinations you want to disable
            var forbiddenKeys = new Array('a', 'n', 'j');

            var key;
            var isCtrl;

            if (window.event) {
                key = window.event.keyCode;     //IE
                if (window.event.ctrlKey)
                    isCtrl = true;
                else
                    isCtrl = false;
            }
            else {
                key = e.which;     //firefox

                if (e.ctrlKey)
                    isCtrl = true;
                else
                    isCtrl = false;
            }
            //Disabling F5 key
            if (key == 116) {
                alert('Key  F5 has been disabled.');
                return false;
            }
            //if ctrl is pressed check if other key is in forbidenKeys array
            if (isCtrl) {

                for (i = 0; i < forbiddenKeys.length; i++) {
                    //  alert(String.fromCharCode(key));
                    //case-insensitive comparation
                    if (forbiddenKeys[i].toLowerCase() == String.fromCharCode(key).toLowerCase()) {
                        alert('Key combination CTRL + '
                            + String.fromCharCode(key)
                            + ' has been disabled.');
                        return false;
                    }
                    if (key == 116) {
                        alert('Key combination CTRL + F5 has been disabled.');
                        return false;
                    }
                }
            }
            return true;
        }

        //Disable right mouse click Script

        var message = "Right click Disabled!";

        ///////////////////////////////////
        function clickIE4() {
            if (event.button == 2) {
                alert(message);
                return false;
            }
        }

        function clickNS4(e) {
            if (document.layers || document.getElementById && !document.all) {
                if (e.which == 2 || e.which == 3) {
                    alert(message);
                    return false;
                }
            }
        }

        if (document.layers) {
            document.captureEvents(Event.MOUSEDOWN);
            document.onmousedown = clickNS4;
        }

        else if (document.all && !document.getElementById) {
            document.onmousedown = clickIE4;
        }

        document.oncontextmenu = new Function("alert(message);return false");
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="background-color: #fff;">

            <table style="width: 100%; height: auto">
                <tr>
                    <td style="width: 10%;">
                        <%--<img src="assets/global/img/HealthBar.png" class="logo-default" style="margin-top: 10px; width: 250px;" />--%>
                        <asp:Image ID="healthybar1" Visible="false" ImageUrl="assets/global/img/HealthBar.png" runat="server" />
                    </td>
                    <td style="width: 90%;">
                        <h2 style="text-align: center;">&nbsp;<asp:Label ID="lblHead" Style="margin-left: -100px;" Font-Bold="true" runat="server" Text="Driver Checklist Report"></asp:Label>
                        </h2>
                    </td>
                </tr>
            </table>
            <div class="row">
                <div class="col-md-12">

                    <div class="form-horizontal form-row-seperated">
                        <%-- <div class="portlet box blue">
                            <div class="portlet-title">
                            </div>
                        </div>--%>
                        <asp:Panel ID="pnlWarningMsg" runat="server" Visible="false">
                            <div class="alert alert-warning alert-dismissable">
                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                <asp:Label ID="lblWarningMsg" Font-Size="Large" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                            </div>
                        </asp:Panel>
                        <div class="portlet-body" style="background-color: #ffffff">
                            <div class="portlet-body form">
                                <div class="tabbable">
                                    <div class="tab-content no-space">
                                        <div class="tab-pane active" id="tab_general1">
                                            <div class="form-body">

                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="lblActive1s" CssClass="col-md-4 control-label" Text="Delivery Date" Style="margin-top: 15px;"></asp:Label>
                                                            <div class="col-md-5">
                                                                <strong>
                                                                    <asp:Label ID="lblFrom" runat="server" Text="From" Style="margin-left: 10px;"></asp:Label></strong>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtdateFrom" Placeholder="dd/MMM/yyyy" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="TextBoxtxtdateFrom_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateFrom" Format="dd/MMM/yyyy"></cc1:CalendarExtender>
                                                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtdateFrom" ValidChars="/" FilterType="Custom, numbers" runat="server" />--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <%--<div class="col-md-6">
                                                        <div class="form-group" style="color: ">

                                                            <div class="col-md-5">
                                                                <strong>
                                                                    <asp:Label ID="lblTo" runat="server" Text="TO" Style="margin-left: 10px;"></asp:Label></strong>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtdateTO1" Placeholder="dd/MMM/yyyy" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtendertxtdateTO" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateTO1" Format="dd/MMM/yyyy"></cc1:CalendarExtender>                                                                
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label8" CssClass="col-md-4 control-label" Text="Driver"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="DrpFromDriver" CssClass="form-control select2me input-medium" runat="server">
                                                                    <asp:ListItem Value="99999" Text="-- Select --"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator13" runat="server" ForeColor="Red" ErrorMessage="Driver Required." ControlToValidate="DrpFromDriver" InitialValue="99999" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%-- <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="DrpToDriver" CssClass="form-control select2me input-medium" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label9" CssClass="col-md-4 control-label" Text="Delivery Time"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="DrpFromDelivery" CssClass="form-control select2me input-medium" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%-- <div class="col-md-3">
                                                        <div class="form-group" style="color: ">
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="DrpToDelivery" CssClass="form-control select2me input-medium" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <div class="col-md-3">
                                                        <asp:Button ID="btnAdd" runat="server"  ValidationGroup="submit" class="btn green-haze btn-circle" Text="Search" OnClick="btnAdd_Click" Style="width: 222px;" />
                                                    </div>
                                                </div>
                                                <%-- <div class="actions btn-set">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-3" style="margin-bottom: 10px;">
                                                                
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="portlet-body">
                                <div class="table-scrollable">
                                    <table class="table table-striped table-bordered table-hover" role="grid" aria-describedby="sample_2" id="sample_3">
                                        <thead class="repHeader">
                                            <tr>
                                                <th></th>
                                                <th colspan="2">
                                                    <asp:CheckBox ID="CHKcheck" runat="server" AutoPostBack="true" OnCheckedChanged="CHKcheck_CheckedChanged" />
                                                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-sm green" Text="Driver List" OnClick="btnPrint_Click" />
                                                    <asp:Button ID="btnSNNO" runat="server" CssClass="btn btn-sm blue" Text="SN_NO" OnClick="btnSNNO_Click" />
                                                    <asp:Label runat="server" ID="lblSN"><br />SN#</asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label runat="server" Font-Size="Large" ID="lblNAME" Text="Subscriber ID & Name"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label runat="server" Font-Size="Large" ID="Label14">Delivery<br />Status</asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" Font-Size="Large" ID="lblTel" Text="Telephone"></asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" Font-Size="Large" ID="lblAddress" Text="Address"></asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" Font-Size="Large" ID="Label11">Plan<br />Name</asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label runat="server" Font-Size="Large" ID="lblDay" Text="Day"></asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" Font-Size="Large" ID="Label10">Driver<br />Name</asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label runat="server" Font-Size="Large" ID="Label16">Contract<br />Start Date</asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" Font-Size="Large" ID="lblDate">Contract<br />End Date</asp:Label></th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" OnItemDataBound="Listview1_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td></td>
                                                        <td style="border-right-width: 0px;">
                                                            <asp:CheckBox ID="CHKPrint" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSerise" Font-Size="Medium" runat="server" CssClass="form-control input-xsmall"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label3" Font-Size="Medium" runat="server" Text='<%# GetCustomer(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label>
                                                            <asp:Label ID="Label2" runat="server" Visible="false" Text='<%# Eval("CustomerID") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <div class="btn-group">
                                                                <%--<a data-toggle="dropdown" href="#" class="btn btn-sm red dropdown-header" style="width: 100%;">--%>
                                                                <asp:Label ID="Label19" data-toggle="dropdown" runat="server" CssClass="btn btn-sm yellow dropdown-header">
                                                                    <asp:Label ID="Label15" runat="server" Text="Pending"></asp:Label>
                                                                    <i class="fa fa-angle-down"></i></asp:Label>
                                                                <%-- <i class="fa fa-angle-down"></i>
                                                                </a>     --%>
                                                                <ul class="dropdown-menu">
                                                                    <li>
                                                                        <asp:LinkButton ID="btnState" runat="server" Style="width: 100%;" Font-Size="Medium" CssClass="btn btn-sm blue" CommandName="btnState" CommandArgument='<%# Eval("CustomerID")+","+ Eval("MYTRANSID")+","+ Eval("DriverID") +","+ Eval("DeliveryID")+","+ Eval("ExpectedDelDate")+","+ Eval("planid") %>'>
                                                                            <asp:Label ID="Label73" runat="server" Text="Delivered" meta:resourcekey="Label73Resource1"></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton ID="lblReturnReason1" runat="server" Style="width: 100%;" Font-Size="Medium" CssClass="btn btn-sm blue">
                                                                            <asp:Label ID="Label1" runat="server" Text="Return(Accept Reason)" meta:resourcekey="Label73Resource1"></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                </ul>

                                                            </div>
                                                            <%-- t --%>
                                                            <panel id="pnlReason" style="padding: 1px; background-color: #fff; border: 2px solid pink; display: none; overflow: auto;" runat="server" cssclass="modalPopup">
                                                                            <div class="modal-dialog" style="position:fixed;left:30%;top:20px">
                                                                                <div class="col-md-12">
                                                                                    <div class="portlet box red-flamingo">
                                                                                        <div class="portlet-title">
                                                                                            <div class="caption">
                                                                                                <i class="fa fa-globe"></i>
                                                                                                Return(accept reason)
                                                                                            </div>
                                                                                        </div>                                                                                   
                                                                                    <div class="portlet-body">
                                                                                            <div class="tabbable">
                                                                                                <div class="tab-content no-space">
                                                                                                    <div class="form-body">
                                                                                                        <div class="form-group">
                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                        <label runat="server" id="Label58" class="col-md-4 control-label  ">
                                                                                                                            <asp:Label ID="Label59" runat="server" Text="Return Reason"></asp:Label>                                                                                                                            
                                                                                                                        </label>
                                                                                                                        <div class="col-md-8">
                                                                                                                            <asp:DropDownList ID="drpreson" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                    <div class="col-md-6">
                                                                                                                        <label runat="server" id="Label61" class="col-md-4 control-label  ">
                                                                                                                            <asp:Label ID="Label62" runat="server" Text="Return Date"></asp:Label>                                                                                                                           
                                                                                                                        </label>
                                                                                                                        <div class="col-md-8">
                                                                                                                             <asp:TextBox ID="txtresondate"  CssClass="form-control" Enabled="false" placeholder="MM/dd/yyyy" runat="server"></asp:TextBox>
                                                                                                                            <cc1:CalendarExtender ID="CalendarExtendertxtdateTO1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtresondate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" TargetControlID="txtresondate" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                        </div>
                                                                                                    </div>    
                                                                                                 </div>
                                                                                            </div>
                                                                                   
                                                                                    <%--<div class="modal-body">
											                                        <div class="scroller" style="height:200px;" data-always-visible="1" data-rail-visible1="1">
                                                                                        <div class="row">
                                                                                             <div class="col-md-12">
                                                                                                  <p> <h4>Return Reason<span class="required">* </span></h4>														                                                                                                                                                  
                                                                                                        <asp:DropDownList ID="drpreson" runat="server" CssClass="form-control select2me"></asp:DropDownList>
														                                            </p> 
                                                                                                  <p> <h4>Return Date<span class="required">* </span></h4>														                                                                                                                                                  
                                                                                                        <asp:TextBox ID="txtresondate"  CssClass="form-control" placeholder="MM/dd/yyyy" runat="server"></asp:TextBox>
                                                                                                        <cc1:CalendarExtender ID="CalendarExtendertxtdateTO1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtresondate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" TargetControlID="txtresondate" ValidChars="/" FilterType="Custom, numbers" runat="server" />
														                                            </p> 
                                                                                            </div>
                                                               
                                                                                        </div>
											                                       </div>
                                                                                   </div>--%>
                                                                 
                                                                                    <div class="modal-footer">
                                                                                         <asp:LinkButton ID="btnsGoReason" class="btn green" runat="server" Text="Return" CommandName="btnsGoReason" CommandArgument='<%# Eval("CustomerID")+","+ Eval("MYTRANSID")+","+ Eval("DriverID") +","+ Eval("DeliveryID")+","+ Eval("ExpectedDelDate")+","+ Eval("planid") %>'/>
                                                                                         <asp:LinkButton ID="btncancle1" runat="server"  Text="Cancel" class="btn default"/>
                                                                                    </div>
                                                                                     </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                             </panel>
                                                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
                                                                BackgroundCssClass="modalBackground" CancelControlID="btncancle1" Enabled="True"
                                                                PopupControlID="pnlReason" TargetControlID="lblReturnReason1">
                                                            </cc1:ModalPopupExtender>
                                                            <%-- t --%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label4" Font-Size="Medium" runat="server" Text='<%# GetPhone(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label5" Font-Size="Medium" runat="server" Text='<%# GetAdd(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label>
                                                            <asp:Label ID="lblCustomerID" runat="server" Visible="false" Text='<%# Eval("CustomerID") %>'></asp:Label>
                                                            <asp:Label ID="lblpaln" Visible="false" runat="server" Text='<%# Eval("planid") %>'></asp:Label>
                                                            <asp:Label ID="lblmytransiid" Visible="false" runat="server" Text='<%# Eval("MYTRANSID") %>'></asp:Label>
                                                            <asp:Label ID="lbldeliveryID" Visible="false" runat="server" Text='<%# Eval("DeliveryID") %>'></asp:Label>
                                                            <asp:Label ID="lblDriver" Visible="false" runat="server" Text='<%# Eval("DriverID") %>'></asp:Label>
                                                            <asp:Label ID="lblExpdate" Visible="false" runat="server" Text='<%# Eval("ExpectedDelDate") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%--<asp:Label ID="Label13" Font-Size="Medium" runat="server" Text='<%# getMealType1(Convert.ToInt32(Eval("DeliveryMeal"))) %>'></asp:Label>--%>
                                                            <asp:Label ID="Label13" Font-Size="Medium" runat="server" Text='<%# GetPlan(Convert.ToInt32(Eval("planid"))) %>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="Label7" Font-Size="Medium" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpectedDelDate")).ToString("dd/MMM/yyyy") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label12" Font-Size="Medium" runat="server" Text='<%# GetDriver(Convert.ToInt32(Eval("DriverID"))) %>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="Label17" Font-Size="Medium" runat="server" Text='<%# Convert.ToDateTime(Eval("StartDate")).ToString("dd/MMM/yyyy") %>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="Label6" Font-Size="Medium" runat="server" Text='<%# Convert.ToDateTime(Eval("EndDate")).ToString("dd/MMM/yyyy") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>


                                        </tbody>

                                    </table>
                                </div>
                            </div>
                            <%-- Map --%>
                            <div class="clearfix"></div>
                            <div class="page-container">
                                <div class="page-content">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="portlet light bordered">
                                                <div class="portlet-title">
                                                    <div class="caption font-dark">
                                                        <i class="icon-settings font-dark"></i>
                                                        <span class="caption-subject bold uppercase">Map</span>
                                                    </div>
                                                    <div class="tools"></div>
                                                </div>
                                                <div class="portlet-body">
                                                    <div class="container-960 innerT">


                                                        <table>
                                                            <tr>
                                                                <td align="right">Starting from : </td>
                                                                <td>                                                                  
                                                                    <asp:TextBox ID="txtStartingpoint" placeholder="Starting Point" runat="server" Text="29.340124,48.00830999999994" Width="395px"></asp:TextBox>
                                                                    <asp:Button ID="btnCurrentlocation" runat="server" Text="Get Current" OnClick="btnCurrentlocation_Click" /></td>
                                                            </tr>
                                                            <asp:ListView ID="ListView2" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td align="right"> Address <%#Container.DataItemIndex+1%> :</td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtFirstAddress" runat="server" Text='<%# GetMapAdd(Convert.ToInt32(Eval("CustomerID"))) %>' Width="395px"></asp:TextBox></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </table>
                                                        <table>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Button ID="btnDrawDirections" CssClass="btn btn-lg" runat="server" Text="Draw Directions" OnClick="btnDrawDirections_Click" />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>


                                                        <tr>
                                                            <td align="right">Show direction instructions?  :</td>
                                                            <td>
                                                                <asp:CheckBox ID="chkShowDirections" runat="server" Checked="true"></asp:CheckBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">Hide Markers? :</td>
                                                            <td>
                                                                <asp:CheckBox ID="chkHideMarkers" runat="server"></asp:CheckBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">Color of direction line :</td>
                                                            <td>
                                                                <asp:TextBox ID="txtDirColor" runat="server" Text="#FF2200" Width="120px"></asp:TextBox>(Hex value)</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">Direction line width :</td>
                                                            <td>
                                                                <asp:TextBox ID="txtDirWidth" runat="server" Text="3" Width="120px"></asp:TextBox>(1 to 6)</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">Direction line opacity :</td>
                                                            <td>
                                                                <asp:TextBox ID="txtDirOpacity" runat="server" Text="0.6" Width="120px"></asp:TextBox>(0.1 to 1.0)</td>
                                                        </tr>

                                                        </table>
        <br />
                                                        <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" />


                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- Map End --%>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
    <div class="quick-nav-overlay"></div>
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
    <%-- toast --%>
    <script src="../assets/toast/jquery.js"></script>
    <script src="../assets/toast/script.js"></script>
    <script src="../assets/toast/toastr.min.js"></script>
    <link href="../assets/toast/toastr.min.css" rel="stylesheet" />
</body>
</html>
