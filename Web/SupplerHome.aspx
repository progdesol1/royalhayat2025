<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.Master" AutoEventWireup="true" CodeBehind="SupplerHome.aspx.cs" Inherits="Web.SupplerHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <!-- BEGIN SAMPLE PORTLET CONFIGURATION MODAL FORM-->
        <div class="modal fade" id="portlet-config" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title">Modal title</h4>
                    </div>
                    <div class="modal-body">
                        Widget settings form goes here
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn blue">Save changes</button>
                        <button type="button" class="btn default" data-dismiss="modal">Close</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <!-- /.modal -->
        <!-- END SAMPLE PORTLET CONFIGURATION MODAL FORM-->
        <!-- BEGIN PAGE HEADER-->
        <!-- BEGIN PAGE HEAD -->
        <div class="page-head">
            <!-- BEGIN PAGE TITLE -->
            <div class="page-title">
                <h1>Dashboard <small>dashboard & statistics</small></h1>
            </div>
            <!-- END PAGE TITLE -->
            <!-- BEGIN PAGE TOOLBAR -->
            <div class="page-toolbar">
                <!-- BEGIN THEME PANEL -->
                <div class="btn-group btn-theme-panel">
                    <a href="javascript:;" class="btn dropdown-toggle" data-toggle="dropdown">
                        <i class="icon-settings"></i>
                    </a>
                    <div class="dropdown-menu theme-panel pull-right dropdown-custom hold-on-click">
                        <div class="row">
                            <div class="col-md-4 col-sm-4 col-xs-12">
                                <h3>THEME</h3>
                                <ul class="theme-colors">
                                    <li class="theme-color theme-color-default active" data-theme="default">
                                        <span class="theme-color-view"></span>
                                        <span class="theme-color-name">Dark Header</span>
                                    </li>
                                    <li class="theme-color theme-color-light" data-theme="light">
                                        <span class="theme-color-view"></span>
                                        <span class="theme-color-name">Light Header</span>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-md-8 col-sm-8 col-xs-12 seperator">
                                <h3>LAYOUT</h3>
                                <ul class="theme-settings">
                                    <li>Theme Style
											<select class="layout-style-option form-control input-small input-sm">
                                                <option value="square" selected="selected">Square corners</option>
                                                <option value="rounded">Rounded corners</option>
                                            </select>
                                    </li>
                                    <li>Layout
											<select class="layout-option form-control input-small input-sm">
                                                <option value="fluid" selected="selected">Fluid</option>
                                                <option value="boxed">Boxed</option>
                                            </select>
                                    </li>
                                    <li>Header
											<select class="page-header-option form-control input-small input-sm">
                                                <option value="fixed" selected="selected">Fixed</option>
                                                <option value="default">Default</option>
                                            </select>
                                    </li>
                                    <li>Top Dropdowns
											<select class="page-header-top-dropdown-style-option form-control input-small input-sm">
                                                <option value="light">Light</option>
                                                <option value="dark" selected="selected">Dark</option>
                                            </select>
                                    </li>
                                    <li>Sidebar Mode
											<select class="sidebar-option form-control input-small input-sm">
                                                <option value="fixed">Fixed</option>
                                                <option value="default" selected="selected">Default</option>
                                            </select>
                                    </li>
                                    <li>Sidebar Menu
											<select class="sidebar-menu-option form-control input-small input-sm">
                                                <option value="accordion" selected="selected">Accordion</option>
                                                <option value="hover">Hover</option>
                                            </select>
                                    </li>
                                    <li>Sidebar Position
											<select class="sidebar-pos-option form-control input-small input-sm">
                                                <option value="left" selected="selected">Left</option>
                                                <option value="right">Right</option>
                                            </select>
                                    </li>
                                    <li>Footer
											<select class="page-footer-option form-control input-small input-sm">
                                                <option value="fixed">Fixed</option>
                                                <option value="default" selected="selected">Default</option>
                                            </select>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- END THEME PANEL -->
            </div>
            <!-- END PAGE TOOLBAR -->
        </div>
        <!-- END PAGE HEAD -->
        <!-- BEGIN PAGE BREADCRUMB -->
        <ul class="page-breadcrumb breadcrumb">
            <li>
                <a href="index.html">Home</a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <a href="#">eCommerce</a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <a href="#">Dashboard</a>
            </li>
        </ul>
        <!-- END PAGE BREADCRUMB -->
        <!-- END PAGE HEADER-->
        <!-- BEGIN PAGE CONTENT-->
        <div class ="col-md-12">
            <div class="sidebar col-md-2 col-sm-2">
                <ul class="list-group margin-bottom-25 sidebar-menu">
                    <li class="list-group-item clearfix"><a href="SupplerHome.aspx"><i class="fa fa-angle-right"></i>Dashboard</a></li>
                    <li class="list-group-item clearfix"><a href="SaleshProduct.aspx"><i class="fa fa-angle-right"></i>Category Management</a></li>
                    <li class="list-group-item clearfix"><a href="SupplierProductMasterTest.aspx"><i class="fa fa-angle-right"></i>Add Product</a></li>
                    <li class="list-group-item clearfix"><a href="javascript:;"><i class="fa fa-angle-right"></i>Payment Status</a></li>
                    <li class="list-group-item clearfix"><a href="Profile.aspx"><i class="fa fa-angle-right"></i>Login/Register</a></li>
                    <li class="list-group-item clearfix"><a href="ChangPassword.aspx"><i class="fa fa-angle-right"></i>Restore Password</a></li>
                    <li class="list-group-item clearfix"><a href="javascript:;"><i class="fa fa-angle-right"></i>Address book</a></li>
                    <li class="list-group-item clearfix"><a href="javascript:;"><i class="fa fa-angle-right"></i>Returns</a></li>
                    <li class="list-group-item clearfix"><a href="javascript:;"><i class="fa fa-angle-right"></i>Newsletter</a></li>
                </ul>
            </div>
            <div class="col-md-10">
                <div class ="col-md-12">
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 margin-bottom-10">
                <div class="dashboard-stat2">
                    <div class="display">
                        <div class="number">
                            <h3 class="font-green-sharp">168,492<small class="font-green-sharp">$</small></h3>
                            <small>TOTAL PROFIT</small>
                        </div>
                        <div class="icon">
                            <i class="icon-pie-chart"></i>
                        </div>
                    </div>
                    <div class="progress-info">
                        <div class="progress">
                            <span style="width: 76%;" class="progress-bar progress-bar-success green-sharp">
                                <span class="sr-only">76% progress</span>
                            </span>
                        </div>
                        <div class="status">
                            <div class="status-title">
                                progress
                            </div>
                            <div class="status-number">
                                76%
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                <div class="dashboard-stat2">
                    <div class="display">
                        <div class="number">
                            <h3 class="font-red-haze">12127</h3>
                            <small>TOTAL ORDERS</small>
                        </div>
                        <div class="icon">
                            <i class="icon-like"></i>
                        </div>
                    </div>
                    <div class="progress-info">
                        <div class="progress">
                            <span style="width: 85%;" class="progress-bar progress-bar-success red-haze">
                                <span class="sr-only">85% change</span>
                            </span>
                        </div>
                        <div class="status">
                            <div class="status-title">
                                change
                            </div>
                            <div class="status-number">
                                85%
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                <div class="dashboard-stat2">
                    <div class="display">
                        <div class="number">
                            <h3 class="font-blue-sharp">670.54<small class="font-blue-sharp">$</small></h3>
                            <small>AVERAGE ORDER</small>
                        </div>
                        <div class="icon">
                            <i class="icon-basket"></i>
                        </div>
                    </div>
                    <div class="progress-info">
                        <div class="progress">
                            <span style="width: 45%;" class="progress-bar progress-bar-success blue-sharp">
                                <span class="sr-only">45% grow</span>
                            </span>
                        </div>
                        <div class="status">
                            <div class="status-title">
                                grow
                            </div>
                            <div class="status-number">
                                45%
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <!-- Begin: life time stats -->
                <div class="portlet light">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="icon-bar-chart font-green-sharp"></i>
                            <span class="caption-subject font-green-sharp bold uppercase">Overview</span>
                            <span class="caption-helper">weekly stats...</span>
                        </div>
                        <div class="tools">
                            <a href="javascript:;" class="collapse"></a>
                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                            <a href="javascript:;" class="reload"></a>
                            <a href="javascript:;" class="fullscreen"></a>
                            <a href="javascript:;" class="remove"></a>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="tabbable-line">
                            <ul class="nav nav-tabs">
                                <li class="active">
                                    <a href="#overview_1" data-toggle="tab">Top Selling </a>
                                </li>
                                <li>
                                    <a href="#overview_2" data-toggle="tab">Most Viewed </a>
                                </li>
                                <li>
                                    <a href="#overview_3" data-toggle="tab">Customers </a>
                                </li>
                                <li class="dropdown">
                                    <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown">Orders <i class="fa fa-angle-down"></i>
                                    </a>
                                    <ul class="dropdown-menu" role="menu">
                                        <li>
                                            <a href="#overview_4" tabindex="-1" data-toggle="tab">Latest 10 Orders </a>
                                        </li>
                                        <li>
                                            <a href="#overview_4" tabindex="-1" data-toggle="tab">Pending Orders </a>
                                        </li>
                                        <li>
                                            <a href="#overview_4" tabindex="-1" data-toggle="tab">Completed Orders </a>
                                        </li>
                                        <li>
                                            <a href="#overview_4" tabindex="-1" data-toggle="tab">Rejected Orders </a>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane active" id="overview_1">
                                    <div class="table-responsive">
                                        <table class="table table-striped table-hover table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Product Name
                                                    </th>
                                                    <th>Price
                                                    </th>
                                                    <th>Sold
                                                    </th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Apple iPhone 4s - 16GB - Black </a>
                                                    </td>
                                                    <td>$625.50
                                                    </td>
                                                    <td>809
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Samsung Galaxy S III SGH-I747 - 16GB </a>
                                                    </td>
                                                    <td>$915.50
                                                    </td>
                                                    <td>6709
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Motorola Droid 4 XT894 - 16GB - Black </a>
                                                    </td>
                                                    <td>$878.50
                                                    </td>
                                                    <td>784
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Regatta Luca 3 in 1 Jacket </a>
                                                    </td>
                                                    <td>$25.50
                                                    </td>
                                                    <td>1245
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Samsung Galaxy Note 3 </a>
                                                    </td>
                                                    <td>$925.50
                                                    </td>
                                                    <td>21245
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Inoval Digital Pen </a>
                                                    </td>
                                                    <td>$125.50
                                                    </td>
                                                    <td>1245
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Metronic - Responsive Admin + Frontend Theme </a>
                                                    </td>
                                                    <td>$20.00
                                                    </td>
                                                    <td>11190
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="tab-pane" id="overview_2">
                                    <div class="table-responsive">
                                        <table class="table table-striped table-hover table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Product Name
                                                    </th>
                                                    <th>Price
                                                    </th>
                                                    <th>Views
                                                    </th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Metronic - Responsive Admin + Frontend Theme </a>
                                                    </td>
                                                    <td>$20.00
                                                    </td>
                                                    <td>11190
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Regatta Luca 3 in 1 Jacket </a>
                                                    </td>
                                                    <td>$25.50
                                                    </td>
                                                    <td>1245
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Apple iPhone 4s - 16GB - Black </a>
                                                    </td>
                                                    <td>$625.50
                                                    </td>
                                                    <td>809
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Samsung Galaxy S III SGH-I747 - 16GB </a>
                                                    </td>
                                                    <td>$915.50
                                                    </td>
                                                    <td>6709
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Motorola Droid 4 XT894 - 16GB - Black </a>
                                                    </td>
                                                    <td>$878.50
                                                    </td>
                                                    <td>784
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Samsung Galaxy Note 3 </a>
                                                    </td>
                                                    <td>$925.50
                                                    </td>
                                                    <td>21245
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Inoval Digital Pen </a>
                                                    </td>
                                                    <td>$125.50
                                                    </td>
                                                    <td>1245
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="tab-pane" id="overview_3">
                                    <div class="table-responsive">
                                        <table class="table table-striped table-hover table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Customer Name
                                                    </th>
                                                    <th>Total Orders
                                                    </th>
                                                    <th>Total Amount
                                                    </th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:ListView ID="listCustomer" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <a href="javascript:;" style="color: #5b9bd1"><%# getName(Convert .ToInt32 ( Eval("UserID")))%> </a>
                                                            </td>
                                                            <td><%# Eval("Qty")%>
                                                            </td>
                                                            <td><%# Eval("TotalAmt")%>
                                                            </td>
                                                            <td>
                                                                <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="tab-pane" id="overview_4">
                                    <div class="table-responsive">
                                        <table class="table table-striped table-hover table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Customer Name
                                                    </th>
                                                    <th>Date
                                                    </th>
                                                    <th>Amount
                                                    </th>
                                                    <th>Status
                                                    </th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">David Wilson </a>
                                                    </td>
                                                    <td>3 Jan, 2013
                                                    </td>
                                                    <td>$625.50
                                                    </td>
                                                    <td>
                                                        <span class="label label-sm label-warning">Pending </span>
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Amanda Nilson </a>
                                                    </td>
                                                    <td>13 Feb, 2013
                                                    </td>
                                                    <td>$12625.50
                                                    </td>
                                                    <td>
                                                        <span class="label label-sm label-warning">Pending </span>
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Jhon Doe </a>
                                                    </td>
                                                    <td>20 Mar, 2013
                                                    </td>
                                                    <td>$125.00
                                                    </td>
                                                    <td>
                                                        <span class="label label-sm label-success">Success </span>
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Bill Chang </a>
                                                    </td>
                                                    <td>29 May, 2013
                                                    </td>
                                                    <td>$12,125.70
                                                    </td>
                                                    <td>
                                                        <span class="label label-sm label-info">In Process </span>
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Paul Strong </a>
                                                    </td>
                                                    <td>1 Jun, 2013
                                                    </td>
                                                    <td>$890.85
                                                    </td>
                                                    <td>
                                                        <span class="label label-sm label-success">Success </span>
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Jane Hilson </a>
                                                    </td>
                                                    <td>5 Aug, 2013
                                                    </td>
                                                    <td>$239.85
                                                    </td>
                                                    <td>
                                                        <span class="label label-sm label-danger">Canceled </span>
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <a href="javascript:;" style="color: #5b9bd1">Patrick Walker </a>
                                                    </td>
                                                    <td>6 Aug, 2013
                                                    </td>
                                                    <td>$1239.85
                                                    </td>
                                                    <td>
                                                        <span class="label label-sm label-success">Success </span>
                                                    </td>
                                                    <td>
                                                        <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End: life time stats -->
            </div>
            <div class="col-md-6">
                <!-- Begin: life time stats -->
                <div class="portlet light">
                    <div class="portlet-title tabbable-line">
                        <div class="caption">
                            <i class="icon-share font-red-sunglo"></i>
                            <span class="caption-subject font-red-sunglo bold uppercase">Revenue</span>
                            <span class="caption-helper">weekly stats...</span>
                        </div>
                        <ul class="nav nav-tabs">
                            <li>
                                <a href="#portlet_tab2" data-toggle="tab" id="statistics_amounts_tab">Amounts </a>
                            </li>
                            <li class="active">
                                <a href="#portlet_tab1" data-toggle="tab">Orders </a>
                            </li>
                        </ul>
                    </div>
                    <div class="portlet-body">
                        <div class="tab-content">
                            <div class="tab-pane active" id="portlet_tab1">
                                <div id="statistics_1" class="chart">
                                </div>
                            </div>
                            <div class="tab-pane" id="portlet_tab2">
                                <div id="statistics_2" class="chart">
                                </div>
                            </div>
                        </div>
                        <div class="well margin-top-10 no-margin no-border">
                            <div class="row">
                                <div class="col-md-3 col-sm-3 col-xs-6 text-stat">
                                    <span class="label label-success">Revenue: </span>
                                    <h3>$111K</h3>
                                </div>
                                <div class="col-md-3 col-sm-3 col-xs-6 text-stat">
                                    <span class="label label-info">Tax: </span>
                                    <h3>$14K</h3>
                                </div>
                                <div class="col-md-3 col-sm-3 col-xs-6 text-stat">
                                    <span class="label label-danger">Shipment: </span>
                                    <h3>$10K</h3>
                                </div>
                                <div class="col-md-3 col-sm-3 col-xs-6 text-stat">
                                    <span class="label label-warning">Orders: </span>
                                    <h3>2350</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End: life time stats -->
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 col-sm-6">
                <!-- BEGIN PORTLET-->
                <div class="portlet light bordered">
                    <div class="portlet-title tabbable-line">
                        <div class="caption">
                            <i class="icon-globe font-green-sharp"></i>
                            <span class="caption-subject font-green-sharp bold uppercase">Feeds</span>
                        </div>
                        <ul class="nav nav-tabs">
                            <li class="active">
                                <a href="#tab_1_1" class="active" data-toggle="tab">System </a>
                            </li>
                            <li>
                                <a href="#tab_1_2" data-toggle="tab">Activities </a>
                            </li>
                        </ul>
                    </div>
                    <div class="portlet-body">
                        <!--BEGIN TABS-->
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_1_1">
                                <div class="scroller" style="height: 339px;" data-always-visible="1" data-rail-visible="0">
                                    <ul class="feeds">
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-success">
                                                            <i class="fa fa-bell-o"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">
                                                            You have 4 pending tasks.
                                                                        <span class="label label-sm label-info">Take action
                                                                            <i class="fa fa-share"></i>
                                                                        </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">Just now </div>
                                            </div>
                                        </li>
                                        <li>
                                            <a href="javascript:;">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">New version v1.4 just lunched! </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">20 mins </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-danger">
                                                            <i class="fa fa-bolt"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">Database server #12 overloaded. Please fix the issue. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">24 mins </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-info">
                                                            <i class="fa fa-bullhorn"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">New order received. Please take care of it. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">30 mins </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-success">
                                                            <i class="fa fa-bullhorn"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">New order received. Please take care of it. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">40 mins </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-warning">
                                                            <i class="fa fa-plus"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">New user registered. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">1.5 hours </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-success">
                                                            <i class="fa fa-bell-o"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">
                                                            Web server hardware needs to be upgraded.
                                                                        <span class="label label-sm label-default ">Overdue </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">2 hours </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-default">
                                                            <i class="fa fa-bullhorn"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">New order received. Please take care of it. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">3 hours </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-warning">
                                                            <i class="fa fa-bullhorn"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">New order received. Please take care of it. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">5 hours </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-info">
                                                            <i class="fa fa-bullhorn"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">New order received. Please take care of it. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">18 hours </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-default">
                                                            <i class="fa fa-bullhorn"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">New order received. Please take care of it. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">21 hours </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-info">
                                                            <i class="fa fa-bullhorn"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">New order received. Please take care of it. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">22 hours </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-default">
                                                            <i class="fa fa-bullhorn"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">New order received. Please take care of it. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">21 hours </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-info">
                                                            <i class="fa fa-bullhorn"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">New order received. Please take care of it. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">22 hours </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-default">
                                                            <i class="fa fa-bullhorn"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">New order received. Please take care of it. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">21 hours </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-info">
                                                            <i class="fa fa-bullhorn"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">New order received. Please take care of it. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">22 hours </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-default">
                                                            <i class="fa fa-bullhorn"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">New order received. Please take care of it. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">21 hours </div>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-info">
                                                            <i class="fa fa-bullhorn"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">New order received. Please take care of it. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">22 hours </div>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="tab-pane" id="tab_1_2">
                                <div class="scroller" style="height: 290px;" data-always-visible="1" data-rail-visible1="1">
                                    <ul class="feeds">
                                        <li>
                                            <a href="javascript:;">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">New user registered </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">Just now </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="javascript:;">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">New order received </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">10 mins </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-danger">
                                                            <i class="fa fa-bolt"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">
                                                            Order #24DOP4 has been rejected.
                                                                        <span class="label label-sm label-danger ">Take action
                                                                            <i class="fa fa-share"></i>
                                                                        </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">24 mins </div>
                                            </div>
                                        </li>
                                        <li>
                                            <a href="javascript:;">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">New user registered </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">Just now </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="javascript:;">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">New user registered </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">Just now </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="javascript:;">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">New user registered </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">Just now </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="javascript:;">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">New user registered </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">Just now </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="javascript:;">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">New user registered </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">Just now </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="javascript:;">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">New user registered </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">Just now </div>
                                                </div>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="javascript:;">
                                                <div class="col1">
                                                    <div class="cont">
                                                        <div class="cont-col1">
                                                            <div class="label label-sm label-success">
                                                                <i class="fa fa-bell-o"></i>
                                                            </div>
                                                        </div>
                                                        <div class="cont-col2">
                                                            <div class="desc">New user registered </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col2">
                                                    <div class="date">Just now </div>
                                                </div>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <!--END TABS-->
                    </div>
                </div>
                <!-- END PORTLET-->
            </div>
            <div class="col-md-6 col-sm-6">
                <div class="portlet light bordered">
                    <div class="portlet-title">
                        <div class="caption caption-md">
                            <i class="icon-bar-chart font-green"></i>
                            <span class="caption-subject font-green bold uppercase">Customer Support</span>
                            <span class="caption-helper">45 Pending</span>
                        </div>
                        <div class="inputs">
                            <div class="portlet-input input-inline input-small ">
                                <div class="input-icon right">
                                    <i class="icon-magnifier"></i>
                                    <input type="text" class="form-control form-control-solid input-circle" placeholder="search...">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="scroller" style="height: 338px;" data-always-visible="1" data-rail-visible1="0" data-handle-color="#D7DCE2">
                            <div class="general-item-list">
                                <asp:ListView ID="listofTikit" runat="server">
                                    <ItemTemplate>
                                        <div class="item">
                                            <div class="item-head">
                                                <div class="item-details">

                                                    <a href="CreatingTikit.aspx?TkitNo=<%# Eval("ACTIVITYCODE")%>" class="item-name primary-link"><%#Eval("USERCODE")%> </a>
                                                    <span class="item-label"><%# DateTime .Parse ( Eval("UPDTTIME").ToString ())%></span>
                                                </div>
                                                <span class="item-status">
                                                    <span class="badge badge-empty badge-success"></span><%#Eval("MyStatus")%></span>
                                            </div>
                                            <div class="item-body"><%#Eval("Remarks")%> </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">

            <div class="col-md-6 col-sm-6">
                <div class="portlet light bordered">
                    <div class="portlet-title">
                        <div class="caption caption-md">
                            <i class="icon-bar-chart font-green"></i>
                            <span class="caption-subject font-green bold uppercase">New Category</span>
                            <span class="caption-helper">
                                <asp:Label ID="lblNewProduct" runat="server"></asp:Label></span>
                        </div>
                       
                    </div>
                    <div class="portlet-body">
                        <div class="scroller" style="height: 338px;" data-always-visible="1" data-rail-visible1="0" data-handle-color="#D7DCE2">
                            <div class="general-item-list">
                                <asp:ListView ID="ListNewProduct" runat="server" >
                                    <ItemTemplate>
                                        <div class="item">
                                            <div class="item-head">
                                                <div class="item-details">

                                                    <a href="#" class="item-name primary-link"><%#getName(Convert .ToInt32 ( Eval("UserID")))%> </a>
                                                    <span class="item-label"><%# DateTime .Parse ( Eval("Careatdate").ToString ())%></span>
                                                </div>
                                                <span class="item-status">
                                                    <span class="badge badge-empty badge-success"></span>
                                                        <asp:Label ID="lblStet" runat="server" Text='<%#Eval("Appover")%>'></asp:Label>
                                                </span>
                                            </div>
                                            <div class="item-body"><%#getCategory(Convert .ToInt32 ( Eval("ProductCategory")))%> </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-sm-6">
                <div class="portlet light bordered">
                    <div class="portlet-title">
                        <div class="caption caption-md">
                            <i class="icon-bar-chart font-green"></i>
                            <span class="caption-subject font-green bold uppercase">New Product</span>
                            <span class="caption-helper">
                                <asp:Label ID="lblProductCunt" runat="server"></asp:Label></span>
                        </div>
                       
                    </div>
                    <div class="portlet-body">
                        <div class="scroller" style="height: 338px;" data-always-visible="1" data-rail-visible1="0" data-handle-color="#D7DCE2">
                            <div class="general-item-list">
                                <asp:ListView ID="listProdct" runat="server" >
                                    <ItemTemplate>
                                        <div class="item">
                                            <div class="item-head">
                                                <div class="item-details">

                                                    <a href="#" class="item-name primary-link"><%#getName(Convert .ToInt32 ( Eval("COMPANYID")))%> </a>
                                                   <%-- <span class="item-label"><%# DateTime .Parse ( Eval("Careatdate").ToString ())%></span>--%>
                                                </div>
                                                <span class="item-status">
                                                    <span class="badge badge-empty badge-success"></span>
                                                        <asp:Label ID="lblStetProduct" runat="server" Text='<%#Eval("Appove")%>'></asp:Label>
                                                </span>
                                            </div>
                                            <div class="item-body"><%#Eval("ProdName1")%> </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div></div> </div>
        <!-- END PAGE CONTENT-->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Demo.init(); // init demo features
            EcommerceIndex.init();
        });
    </script>
</asp:Content>