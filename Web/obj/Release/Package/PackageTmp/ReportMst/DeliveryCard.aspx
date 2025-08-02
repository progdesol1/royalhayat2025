<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliveryCard.aspx.cs" Inherits="Web.ReportMst.DeliveryCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Delivery Card</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
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
    <script type="text/javascript">
        function showProgress() {
            $.blockUI({ message: '<h1>please wait...</h1>' });
        }

        function stopProgress() {
            $.unblockUI();
        }

        $("#ContentPlaceHolder1_btnAdd").click(function () {
            $.blockUI({ message: '<h1>please wait...</h1>' });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <%--<div class="page-container">
            <div class="page-content">--%>
        <div style="background-color: #ffffff">
            <table style="width: 100%; height: auto">
                <tr>
                    <td style="width: 10%;">
                        <%--<img src="assets/global/img/HealthBar.png" class="logo-default" style="margin-top: 10px; width: 250px;" />--%>
                        <asp:Image ID="healtybar1" ImageUrl="assets/global/img/HealthBar.png" runat="server" />
                    </td>
                    <td style="width: 90%;">
                        <h2 style="text-align: center;">&nbsp;<asp:Label ID="lblHead" Style="margin-left: -100px;" Font-Bold="true" runat="server" Text="Delivery Card"></asp:Label>
                        </h2>
                    </td>
                </tr>
            </table>
            <div class="row">
                <div class="col-md-12">

                    <%--<div class="portlet-title">
                        <div class="caption font-dark">
                            <i class="icon-settings font-dark"></i>
                            <span class="caption-subject bold uppercase">Delivery Card</span>
                        </div>
                        <div class="tools"></div>
                    </div>--%>
                    <asp:Panel ID="pnlWarningMsg" runat="server" Visible="false">
                        <div class="alert alert-warning alert-dismissable">
                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                            <asp:Label ID="lblWarningMsg" Font-Size="Large" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                        </div>
                    </asp:Panel>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group" style="color: ">
                                <asp:Label runat="server" ID="lblActive1s" CssClass="col-md-4 control-label" Text="Delivery Date" Style="margin-top: 15px;"></asp:Label>
                                <div class="col-md-8">
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
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-6">
                            <div class="form-group" style="color: ">

                                <div class="col-md-8">
                                    <strong>
                                        <asp:Label ID="lblTo" runat="server" Text="TO" Style="margin-left: 10px;"></asp:Label></strong>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtdateTO" Placeholder="dd/MMM/yyyy" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtendertxtdateTO" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateTO" Format="dd/MMM/yyyy"></cc1:CalendarExtender>
                                    <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtdateTO" ValidChars="/" FilterType="Custom, numbers" runat="server" />--%>
                                </div>

                            </div>
                        </div>

                    </div>
                    <asp:Panel ID="Panel1" runat="server" Style="margin-top: 10px;">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group" style="color: ">
                                    <asp:Label runat="server" ID="Label8" CssClass="col-md-4 control-label" Text="Subscriber"></asp:Label>
                                    <div class="col-md-3">
                                        <asp:TextBox CssClass="form-control" ID="txtFromSearchByID" AutoPostBack="true" OnTextChanged="txtFromSearchByID_TextChanged" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtFromSearchByID" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="DrpFromClient" CssClass="form-control select2me input-medium" runat="server" Style="margin-left: -20%;"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1">
                            </div>
                            <div class="col-md-6">
                                <div class="form-group" style="color: ">
                                    <div class="col-md-2">
                                        <asp:TextBox CssClass="form-control" ID="txttToSearchByID" AutoPostBack="true" OnTextChanged="txttToSearchByID_TextChanged" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txttToSearchByID" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="DrpToClient" CssClass="form-control select2me input-medium" runat="server" Style="margin-left: -12%;"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-1" style="margin-left: 42px;">
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-sm blue" Text="Search" OnClick="btnAdd_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="page-container">
                        <div class="page-content">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="portlet-body">
                                        <div class="table-scrollable">
                                            <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1" id="sample_1">
                                                <thead>
                                                    <%--class="repHeader"--%>
                                                    <tr>
                                                        <th></th>
                                                        <th>
                                                            <asp:CheckBox ID="CHKALLPrint" runat="server" AutoPostBack="true" OnCheckedChanged="CHKAction_CheckedChanged" />
                                                            <asp:LinkButton ID="lblAtion" CssClass="btn btn-sm green" runat="server" OnClick="lblAtion_Click">Print</asp:LinkButton>
                                                            <br />
                                                            <asp:Label runat="server" ID="lblSN" Text="SN#"></asp:Label>
                                                        </th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="lblCID" Text="Client ID - Name "></asp:Label></th>

                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="lblPhoneNomber" Text="Phone number"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="lblAddress" Text="Address"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="lblDriverID" Text="Driver"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="lblDeliveryID" Text="Delivery No."></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="Label2" Text="Expected Date"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="lvlRemDay" Text="Remarks"></asp:Label></th>
                                                    </tr>

                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview1" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:CheckBox ID="CHKAction" runat="server" />
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# GetCNAME(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label>
                                                                    <asp:Label ID="lblCustomerID" Visible="false" runat="server" Text='<%# Eval("CustomerID") %>'></asp:Label>
                                                                    <asp:Label ID="lblpaln" Visible="false" runat="server" Text='<%# Eval("planid") %>'></asp:Label>
                                                                    <asp:Label ID="lblMealType" Visible="false" runat="server" Text='<%# Eval("MealType") %>'></asp:Label>
                                                                    <asp:Label ID="lblmytransiid" Visible="false" runat="server" Text='<%# Eval("MYTRANSID") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# GetPHONE(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# GetAdd(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# GetDriver(Convert.ToInt32(Eval("DriverID"))) %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("DayNumber") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblexpdate" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpectedDelDate")).ToString("dd/MMM/yyyy") %>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbldeliveryID" runat="server" Text='<%# Eval("DeliveryID") %>'></asp:Label>
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
            </div>
            <%-- </div>
        </div>--%>
        </div>
    </form>
    <div class="quick-nav-overlay"></div>
    <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery.blockui.min.js"></script>

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
</body>
</html>
