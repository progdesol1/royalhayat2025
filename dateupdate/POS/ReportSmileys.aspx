<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportSmileys.aspx.cs" Inherits="Web.ReportMst.ReportSmileys" %>

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
        <div style="background-color: #ffffff">
            <table style="width: 100%; height: auto">
                <tr>
                    <td style="width: 10%;">
                        <%--<img src="assets/global/img/HealthBar.png" class="logo-default" style="margin-top: 10px; width: 250px;" />--%>
                        <asp:Image ID="HealtybarLogo" ImageUrl="assets/global/img/HealthBar.png" runat="server" />
                    </td>
                    <td style="width: 90%;">
                        <h2 style="text-align: center;">&nbsp;<asp:Label ID="lblHead" Style="margin-left: -100px;" Font-Bold="true" runat="server" Text="Customer OnHold List"></asp:Label>
                        </h2>
                    </td>
                </tr>
            </table>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-horizontal form-row-seperated">
                        <div class="portlet-body" style="background-color: #ffffff">
                            <%-- List --%>
                            <div class="page-container">
                                <div class="page-content">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="portlet light bordered">
                                                <div class="portlet-title">
                                                    <div class="caption font-dark">
                                                        <i class="icon-settings font-dark"></i>
                                                        <span class="caption-subject bold uppercase">Customer On Hold</span>
                                                    </div>
                                                    <div class="tools"></div>
                                                </div>
                                                <div class="portlet-body">
                                                    <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                        <thead>
                                                            <tr>
                                                                <th></th>
                                                                <th><asp:Label runat="server" ID="lblSN"><br />SN#</asp:Label></th>
                                                                <th>Client Name</th>
                                                                <th>Plan Name</th>
                                                                <th>Start<br />
                                                                    Date</th>
                                                                <th>End<br />
                                                                    Date</th>
                                                                <th>Hold Date</th>
                                                                <th>Hold By</th>
                                                                <th>Hold Remark</th>
                                                                <th>Status</th>
                                                               
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:ListView ID="ListView1" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td><%# Container.DataItemIndex+1 %></td>
                                                                        <td>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# GetCustomer(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# GetPlan(Convert.ToInt32(Eval("planid"))) %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Convert.ToDateTime(Eval("StartDate")).ToString("dd/MMM/yyyy") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Convert.ToDateTime(Eval("EndDate")).ToString("dd/MMM/yyyy") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("HoldDate") != null ? Convert.ToDateTime(Eval("HoldDate")).ToString("dd/MMM/yyyy") : "Not Found" %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="Label6" runat="server" Text='<%# GetUser(Convert.ToInt32(Eval("Holdbyuser"))) %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("HoldREmark") != null && Eval("HoldREmark") != "" ? Eval("HoldREmark") : "Not Found" %>'></asp:Label></td>
                                                                        <td style="text-align:center;">
                                                                            <asp:Label ID="Label8" Font-Bold="true" CssClass="label label-danger label-sm" Font-Size="Medium" runat="server" Text='<%# GetStatus(Convert.ToBoolean(Eval("SubscriptionOnHold"))) %>'></asp:Label></td>
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
