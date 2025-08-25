<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelpDeskExcRep.aspx.cs" Inherits="Web.ReportMst.HelpDeskExcRep" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <title>Summirized</title>
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

        <div class="row" style="margin-left: 0px; margin-right: 10px;">

            <div class="row">
                <div class="col-md-5">
                    <div class="form-group">
                        <asp:Label ID="Label9" runat="server" Text="From" CssClass="col-md-3"></asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control" placeholder="dd/MM/yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="TextBoxemp1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtstartdate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                        </div>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="form-group">
                        <asp:Label ID="Label10" runat="server" Text="To" CssClass="col-md-1"></asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control" placeholder="dd/MM/yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtenddate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                        </div>
                    </div>
                    <asp:Button ID="btnsub" runat="server" CssClass="btn btn-sm btn-circle red" Text="GO" OnClick="btnsub_Click" />
                    <div class="col-md-1">
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5">
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Month to Year" CssClass="col-md-3" Visible="false"></asp:Label>
                        <div class="col-md-6">
                            <asp:DropDownList ID="drpmonthfrom" runat="server" AutoPostBack="true" CssClass="form-control"  Visible="false"></asp:DropDownList>
                        </div>
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-sm btn-circle red" Text="GO" OnClick="Button1_Click"  Visible="false"/>
                        <asp:Button ID="btnrefresh" runat="server" CssClass="btn btn-sm btn-circle red" Text="Refresh" OnClick="btnrefresh_Click"  Visible="false"/>
                    </div>
                </div>
                <div class="col-md-1">
                </div>
                <div class="col-md-5"></div>
            </div>
            <br />
            <div class="col-md-6">
                <div class="portlet box blue">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Department
                        </div>

                    </div>
                    <div class="portlet-body">

                        <table class="table table-bordered table-striped" id="sample_1">
                            <thead>
                                <tr style="background-color: #337ab7;">

                                    <th>Dept id
                                    </th>
                                    <th>Deptartment name
                                    </th>
                                    <th>Count
                                    </th>
                                    <th>%
                                    </th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:ListView ID="ListView3" runat="server" OnItemCommand="ListView3_ItemCommand2">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDeptID" runat="server" Text='<%# Eval("TickDepartmentID") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                            </td>
                                            <td>
                                             <center> <asp:Label ID="lbldeptcount" runat="server" Text='<%# Eval("DeptCount") %>'></asp:Label></center>  
                                            </td>
                                            <td>
                                              <center>  <asp:Label ID="lblper" runat="server" Text='<%# Eval("percentage") %>'></asp:Label></center>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td></td>

                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label12" runat="server" Text="Total:-" Style="font-weight: bold; font-size: large"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblFinalTotal" runat="server" Text=""></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblperdept" runat="server" Text=""></asp:Label></td>
                                    <td></td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="portlet box yellow-crusta">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Location
                        </div>

                    </div>
                    <div class="portlet-body">

                        <table class="table table-bordered table-striped" id="sample_2">
                            <thead>
                                <tr style="background-color: #F1C40F;">

                                    <th>LOC Id 
                                    </th>
                                    <th>Location Name 
                                    </th>
                                    <th>Count
                                    </th>
                                    <th>%
                                    </th>

                                </tr>

                            </thead>
                            <tbody>
                                <asp:ListView ID="ListView5" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDeptID" runat="server" Text='<%# Eval("TickPhysicalLocation") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                            </td>
                                            <td>
                                              <center>  <asp:Label ID="Label3" runat="server" Text='<%# Eval("LocCount") %>'></asp:Label></center>
                                            </td>
                                            <td>
                                               <center> <asp:Label ID="Label7" runat="server" Text='<%# Eval("percentage") %>'></asp:Label></center>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td></td>

                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label4" runat="server" Text="Total:-" Style="font-weight: bold; font-size: large"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbllocation" runat="server" Text=""></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblperloc" runat="server" Text=""></asp:Label></td>

                                </tr>
                            </tfoot>
                        </table>

                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="margin-left: 0px; margin-right: 10px;">
            <div class="col-md-6">
                <div class="portlet box green-jungle">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Category
                        </div>
                    </div>
                    <div class="portlet-body">
                        <table class="table table-bordered table-striped" id="sample_3">
                            <thead>
                                <tr style="background-color: #36c6d3;">

                                    <th>Cat ID
                                    </th>
                                    <th>Category Name
                                    </th>
                                    <th>Count
                                    </th>
                                    <th>%
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:ListView ID="ListView6" runat="server">
                                    <ItemTemplate>
                                        <tr>

                                            <td>
                                                <asp:Label ID="label2" runat="server" Text='<%# Eval("TickCatID") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                            </td>
                                            <td>
                                              <center>    <asp:Label ID="Label3" runat="server" Text='<%# Eval("CatCount") %>'></asp:Label></center>
                                            </td>
                                            <td>
                                             <asp:Label ID="Label5" runat="server" Text='<%# Eval("percentage") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td></td>
                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label11" runat="server" Text="Total:-" Style="font-weight: bold; font-size: large"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblcategory" runat="server" Text=""></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblpercategory" runat="server" Text=""></asp:Label></td>

                                </tr>
                            </tfoot>
                        </table>

                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="portlet box purple-soft">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Sub Category
                        </div>
                    </div>
                    <div class="portlet-body">
                        <table class="table table-bordered table-striped" id="sample_7">
                            <thead>
                                <tr style="background-color: #659be0;">

                                    <th>SubCat ID 
                                    </th>
                                    <th>SubCategory Name
                                    </th>
                                    <th>Count
                                    </th>
                                    <th>%
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:ListView ID="ListView7" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDeptID" runat="server" Text='<%# Eval("TickSubCatID") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                            </td>
                                            <td>
                                               <center>   <asp:Label ID="Label3" runat="server" Text='<%# Eval("subCatCount") %>'></asp:Label></center>
                                            </td>
                                            <td>
                                              <center>  <asp:Label ID="Label6" runat="server" Text='<%# Eval("percentage") %>'></asp:Label></center>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td></td>

                                    <td style="text-align: right; padding-right: 5px;">
                                        <asp:Label ID="Label14" runat="server" Text="Total:-" Style="font-weight: bold; font-size: large"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblsubcategory" runat="server" Text=""></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblpersubcategory" runat="server" Text=""></asp:Label></td>

                                </tr>
                            </tfoot>
                        </table>
                    </div>
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
                <div class="col-md-3">
                    <asp:Button ID="btnmail" runat="server" Text="Send by mail" CssClass="btn btn-info" OnClick="btnmail_Click" ValidationGroup="tmail" />
                </div>
            </div>
             <div class="row">

                <div class="col-md-6">
                    <div class="form-group" style="color: ">
                        <asp:Label runat="server" ID="Label8" class="col-md-4 control-label" ></asp:Label>
           
                          <div class="col-md-8"> <asp:DropDownList ID="drpchart" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpchart_SelectedIndexChanged">
                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                <asp:ListItem Value="1" Text="Department"></asp:ListItem>
                <asp:ListItem Value="2" Text="Category"></asp:ListItem>
                <asp:ListItem Value="3" Text="Sub Category"></asp:ListItem>
                <asp:ListItem Value="4" Text="Location"></asp:ListItem>
            </asp:DropDownList>
                              </div>
                        </div>
                    </div>
                 </div>
            <asp:Chart ID="Chart1" runat="server" Height="500px" Width="1000px" Visible="false">
                <Titles>
                    <asp:Title ShadowOffset="3" Name="Items" />
                </Titles>
                <Legends>
                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                </Legends>
                <Series>
                    <asp:Series Name="Default" />
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                </ChartAreas>
            </asp:Chart>
    </form>
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
    <%--<script src="../assets/pages/scripts/table-datatables-buttons.js"></script>
    <script src="../assets/pages/scripts/table-datatables-buttons.min.js"></script>
    <script src="../assets/pages/scripts/table-datatables-colreorder.js"></script>
    <script src="../assets/pages/scripts/table-datatables-colreorder.min.js"></script>
    <script src="../assets/pages/scripts/table-datatables-fixedheader.js"></script>
    <script src="../assets/pages/scripts/table-datatables-fixedheader.min.js"></script>
    <script src="../assets/pages/scripts/table-datatables-managed.js"></script>
    <script src="../assets/pages/scripts/table-datatables-managed.min.js"></script>
    <script src="../assets/pages/scripts/table-datatables-responsive.js"></script>
    <script src="../assets/pages/scripts/table-datatables-responsive.min.js"></script>
    <script src="../assets/pages/scripts/table-datatables-rowreorder.js"></script>
    <script src="../assets/pages/scripts/table-datatables-rowreorder.min.js"></script>
    <script src="../assets/pages/scripts/table-datatables-scroller.js"></script>
    <script src="../assets/pages/scripts/table-datatables-scroller.min.js"></script>
    <script src="../assets/admin/pages/scripts/table-advanced.js"></script>
    <script src="../assets/admin/pages/scripts/table-managed.js"></script>
    <script src="assets/pages/scripts/table-datatables-buttons.min.js"></script>--%>
</body>

</html>
