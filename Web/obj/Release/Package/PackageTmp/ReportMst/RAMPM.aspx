<%@ Page Title="" Language="C#" MasterPageFile="~/ReportMst/Report.Master" AutoEventWireup="true" CodeBehind="RAMPM.aspx.cs" Inherits="Web.ReportMst.RAMPM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table style="width: 100%; height: auto">
                <tr>
                    <td style="width: 10%;">
                        <img src="assets/global/img/HealthBar.png" class="logo-default" style="margin-top: 10px; width: 250px;" />
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
                        <div class="portlet box blue">
                            <div class="portlet-title">
                            </div>
                        </div>

                        <div class="portlet-body">
                            <div class="portlet-body form">
                                <div class="tabbable">
                                    <div class="tab-content no-space">
                                        <div class="tab-pane active" id="tab_general1">
                                            <div class="form-body">

                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="lblActive1s" CssClass="col-md-3 control-label" Text="Delivery Date" Style="margin-top: 15px;"></asp:Label>
                                                            <div class="col-md-8">
                                                                <strong>
                                                                    <asp:Label ID="lblFrom" runat="server" Text="From" Style="margin-left: 10px;"></asp:Label></strong>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtdateFrom" Placeholder="MM/DD/YYYY" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="TextBoxtxtdateFrom_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateFrom" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtdateFrom" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">

                                                            <div class="col-md-8">
                                                                <strong>
                                                                    <asp:Label ID="lblTo" runat="server" Text="TO" Style="margin-left: 10px;"></asp:Label></strong>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtdateTO" Placeholder="MM/DD/YYYY" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtendertxtdateTO" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateTO" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtdateTO" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label8" CssClass="col-md-3 control-label" Text="Driver" Style="margin-top: 15px;"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label9" CssClass="col-md-3 control-label" Text="Delivery Time" Style="margin-top: 15px;"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="actions btn-set">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-3" style="margin-bottom: 10px;">
                                                                <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" Text="Search" Style="width: 222px;" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="portlet-body">
                                <div class="table-scrollable">
                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1" id="sample_1">
                                        <thead class="repHeader">
                                            <tr>
                                                <th>
                                                    <asp:Label runat="server" ID="lblSN" Text="SN#"></asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" ID="lblCID" Text="ID"></asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" ID="lblNAME" Text="NAME"></asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" ID="lblTel" Text="Telephone"></asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" ID="lblAddress" Text="Address"></asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" ID="lblDate" Text="Date"></asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" ID="lblDay" Text="Day"></asp:Label></th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="Listview1" runat="server">

                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("CustomerID") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("CustomerName")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text='<%#Eval("MOBPHONE")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("CityEnglish")+","+ Eval("ADDR1") +","+ Eval("ADDR2")+","+ Eval("Building")+","+ Eval("Street")+","+ Eval("Lane") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("EndDate")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("Weight") %>'></asp:Label>
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
</asp:Content>
