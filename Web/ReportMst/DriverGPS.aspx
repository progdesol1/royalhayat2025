<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DriverGPS.aspx.cs" Inherits="Web.ReportMst.DriverGPS" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
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
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">

                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>
                                        <asp:Label runat="server" ID="lblHeader" Text="driver Location"></asp:Label>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="btnPagereload" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <div class="btn-group btn-group-circle btn-group-solid">
                                        </div>
                                        <asp:Button ID="btnDriverMap" runat="server" class="btn green-haze btn-circle" Text="Add New" />
                                                                                
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="portlet-body form">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general1">
                                                    <div class="form-body">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDriver" CssClass="col-md-4 control-label" Text="Driver"></asp:Label>
                                                                    <div class="col-md-6">
                                                                        <asp:DropDownList ID="DRPDriver" CssClass="form-control select2me input-medium" runat="server"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:LinkButton ID="Searcht" class="btn btn-lg blue" CommandName="btnsearch" Style="padding: 5px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 15px;" runat="server" OnClick="Searcht_Click">search <i class="fa fa-search"></i></asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDevice" CssClass="col-md-4 control-label" Text="Device ID"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="DRPDeviceID" CssClass="form-control select2me input-medium" runat="server"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet box green-haze">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-globe"></i>
                                                <asp:Label runat="server" ID="Label5" Text="Driver Location"></asp:Label>
                                                List
                                            </div>
                                            <div class="tools">
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <table class="table table-striped table-bordered table-hover dataTable no-footer" id="sample_1">
                                                <thead>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview1" runat="server">


                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbllati" runat="server" Text='<%# Eval("latitude")%>'></asp:Label>
                                                            <asp:Label ID="lbllandi" runat="server" Text='<%# Eval("logitude")%>'></asp:Label>
                                                            <asp:Label ID="lbldeviss" runat="server" Text='<%# Eval("DeviceID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
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
                                                            <%--<tr>
                                                                <td align="right">Starting from : </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtStartingpoint" placeholder="Starting Point" runat="server" Text="HealthyBar, 102, Salmiya" Width="395px"></asp:TextBox>
                                                                    <asp:Button ID="btnCurrentlocation" runat="server" Text="Get Current" OnClick="btnCurrentlocation_Click" /></td>
                                                            </tr>--%>

                                                            <tr>
                                                                <td align="right">Address 1</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFirstAddress" runat="server" Text="" Width="395px"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">Address 2</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtsecondAddress" runat="server" Text="" Width="395px"></asp:TextBox></td>
                                                            </tr>
                                                        </table>

                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Timer ID="Timer1" runat="server" Interval="50000" OnTick="Timer1_Tick">
                                                                </asp:Timer>
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:Button ID="btnDrawDirections" CssClass="btn btn-lg" runat="server" Text="Draw Directions" OnClick="btnDrawDirections_Click" />
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                                                    </Triggers>
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
