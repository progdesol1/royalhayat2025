<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AMPM.aspx.cs" Inherits="Web.ReportMst.AMPM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
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
    <link rel="shortcut icon" href="favicon.ico" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>

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
                                                            <asp:Label runat="server" ID="lblActive1s" CssClass="col-md-4 control-label" Text="Delivery Date" Style="margin-top: 15px;"></asp:Label>
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
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label8" CssClass="col-md-4 control-label" Text="Driver"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="DrpFromDriver" CssClass="form-control select2me input-medium" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="DrpToDriver" CssClass="form-control select2me input-medium" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label9" CssClass="col-md-4 control-label" Text="Delivery Time"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="DrpFromDelivery" CssClass="form-control select2me input-medium" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="DrpToDelivery" CssClass="form-control select2me input-medium" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="actions btn-set">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-3" style="margin-bottom: 10px;">
                                                                <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" Text="Search" OnClick="btnAdd_Click" Style="width: 222px;" />
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
                                                    <asp:CheckBox ID="CHKcheck" runat="server" />
                                                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-sm green" Text="Print" OnClick="btnPrint_Click" />
                                                </th>
                                                <th>
                                                    <asp:Label runat="server" ID="lblSN" Text="SN#"></asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" ID="Label10" Text="Driver Name"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label runat="server" ID="Label11" Text="delivery Type"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label runat="server" ID="lblCID" Text="ID"></asp:Label></th>
                                                <th>
                                                    <asp:Label runat="server" ID="lblNAME" Text="Client NAME"></asp:Label></th>
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
                                                            <asp:CheckBox ID="CHKPrint" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" Text='<%# Eval("DriverName") %>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="Label13" runat="server" Text='<%# getMealType(Convert.ToInt32(Eval("DeliveryMeal"))) %>'></asp:Label></td>
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
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Convert.ToDateTime(Eval("EndDate")).ToShortDateString() %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpDate")).ToShortDateString() %>'></asp:Label>
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
</body>
</html>
