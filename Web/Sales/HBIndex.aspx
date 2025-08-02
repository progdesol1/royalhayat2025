<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HBIndex.aspx.cs" Inherits="Web.Sales.HBIndex" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html lang="en">
<!--<![endif]-->
<!-- BEGIN HEAD -->

<head>
    <meta charset="utf-8" />
    <title>digital53  Admin Dashboard</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="Preview page of digital53 Admin Theme #4 for statistics, charts, recent events and reports" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->


    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />



    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/morris/morris.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/fullcalendar/fullcalendar.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/jqvmap/jqvmap/jqvmap.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="../assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="../assets/layouts/layout4/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout4/css/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout4/css/custom.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME LAYOUT STYLES -->
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
<!-- END HEAD -->

<body class="page-container-bg-solid page-header-fixed page-sidebar-closed-hide-logo">
    <form runat="server" id="form1">
        <asp:ScriptManager ID="toolscriptmanagerID" runat="server">
        </asp:ScriptManager>
        <!-- BEGIN HEADER -->

        <!-- END HEADER -->
        <!-- BEGIN HEADER & CONTENT DIVIDER -->
        <div class="clearfix"></div>
        <!-- END HEADER & CONTENT DIVIDER -->
        <!-- BEGIN CONTAINER -->
        <div class="page-container" style="margin-left: -20px; margin-top: 0px; padding-top: 0px;">
            <!-- BEGIN SIDEBAR -->

            <!-- END SIDEBAR -->
            <!-- BEGIN CONTENT -->
            <div class="page-content-wrapper">
                <!-- BEGIN CONTENT BODY -->
                <div class="page-content" style="margin-left: 0px;">
                    <!-- BEGIN PAGE HEAD-->

                    <!-- END PAGE HEAD-->
                    <!-- BEGIN PAGE BREADCRUMB -->
                    <ul class="page-breadcrumb breadcrumb">
                        <li>
                            <a href="index.html">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="active">Dashboard</span>
                        </li>
                    </ul>
                    <!-- END PAGE BREADCRUMB -->
                    <!-- BEGIN PAGE BASE CONTENT -->
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat2 bordered">
                                <div class="display">
                                    <div class="number">
                                        <h3 class="font-green-sharp">
                                            <span data-counter="counterup" data-value="7800">
                                                <asp:Label ID="lblbox1KD" runat="server" Text="0"></asp:Label>
                                            </span>
                                            <small class="font-green-sharp">KD</small>
                                        </h3>
                                        <small>Today
                                            <asp:Label ID="TODAYDate" runat="server" Text="Label"></asp:Label>
                                            Sale</small>
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
                                        <div class="status-title">progress </div>
                                        <div class="status-number">
                                            <asp:Label ID="lblbox1PER" runat="server" Text="76%"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat2 bordered">
                                <div class="display">
                                    <div class="number">

                                        <h3 class="font-red-haze">
                                            <span data-counter="counterup" data-value="1349">
                                                <asp:Label ID="lblBoxWeek" runat="server" Text="0"></asp:Label>
                                            </span>
                                            <small class="font-green-sharp">KD</small>

                                        </h3>
                                        <small>
                                            <asp:Label ID="lblWeek" runat="server" Text="Label"></asp:Label>
                                            Week Sale Amount</small>
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
                                        <div class="status-title">change </div>
                                        <div class="status-number">
                                            <asp:Label ID="Label8" runat="server" Text="85%"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat2 bordered">
                                <div class="display">
                                    <div class="number">

                                        <h3 class="font-red-haze">
                                            <span data-counter="counterup" data-value="1349">
                                                <asp:Label ID="lblbox2KD" runat="server" Text="0"></asp:Label>
                                            </span>
                                            <small class="font-green-sharp">KD</small>

                                        </h3>
                                        <small>This
                                            <asp:Label ID="Thismonth" runat="server" Text="Label"></asp:Label>
                                            Sale</small>
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
                                        <div class="status-title">change </div>
                                        <div class="status-number">
                                            <asp:Label ID="lblbox2PER" runat="server" Text="85%"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat2 bordered">
                                <div class="display">
                                    <div class="number">

                                        <h3 class="font-blue-sharp">
                                            <span data-counter="counterup" data-value="567">
                                                <asp:Label ID="lblbox3KD" runat="server" Text="0"></asp:Label>
                                            </span>
                                            <small class="font-green-sharp">KD</small>
                                        </h3>
                                        <small>Sale of the Year 
                                            <asp:Label ID="ThisYear" runat="server" Text="Label"></asp:Label>
                                            </small>
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
                                        <div class="status-title">grow </div>
                                        <div class="status-number">
                                            <asp:Label ID="lblbox3PER" runat="server" Text="45%"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat2 bordered">
                                <div class="display">
                                    <div class="number">

                                        <h3 class="font-blue-sharp">
                                            <span data-counter="counterup" data-value="567">
                                                <asp:Label ID="lblbox4KD" runat="server" Text="0"></asp:Label>
                                            </span>
                                            <small class="font-green-sharp">KD</small>
                                        </h3>
                                        <small>This
                                            <asp:Label ID="ThisYearReturn" runat="server" Text="Label"></asp:Label>
                                            Sale Return</small>
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
                                        <div class="status-title">grow </div>
                                        <div class="status-number">
                                            <asp:Label ID="lblbox4PER" runat="server" Text="45%"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <!-- Begin: life time stats -->
                            <div class="portlet light">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="icon-bar-chart font-green-sharp"></i>
                                        <span class="caption-subject font-green-sharp bold uppercase">Overview</span>
                                        <span class="caption-helper">weekly stats...</span>
                                    </div>

                                    <div class="tools">
                                        <asp:Button ID="btnFastCarReport" runat="server" CssClass="btn btn-circle red btn-outline" Text="ALL Report" OnClick="btnFastCarReport_Click" />
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
                                            
                                            <li class="dropdown">
                                                <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown">Orders <i class="fa fa-angle-down"></i>
                                                </a>
                                                <ul class="dropdown-menu" role="menu">
                                                    <li class="active">
                                                        <a href="#overview_4" data-toggle="tab">Latest 10 Orders </a>
                                                    </li>
                                                   <%-- <li>
                                                        <a href="#overview_5" data-toggle="tab">Pending Orders </a>
                                                    </li>--%>
                                                    <li>
                                                        <a href="#overview_6" data-toggle="tab">Completed Orders </a>
                                                    </li>
                                                   <%-- <li>
                                                        <a href="#overview_7" data-toggle="tab">Rejected Orders </a>
                                                    </li>--%>
                                                </ul>
                                            </li>
                                            <li>
                                                <a href="#overview_1" data-toggle="tab">Top Selling </a>
                                            </li>
                                            
                                           

                                        </ul>
                                        <div class="tab-content">
                                           
                                            <div class="tab-pane " id="overview_5">
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
                                                                <%--<th></th>--%>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:ListView ID="ListContractPending" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblCustomerpending" runat="server" Text='<%# getName(Convert.ToInt32( Eval("CustomerID")))%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("ContractDate")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbltotalamountpending" runat="server" Text='<%# gettotalAmaont(Convert.ToInt32( Eval("CustomerID")))%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <span class="label label-sm label-warning">Pending </span>
                                                                        </td>
                                                                        <%--<td>
                                                                    <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                                </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="tab-pane " id="overview_6">
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
                                                           <asp:ListView ID="ListoederCompleted" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblCustomerCompleted" runat="server" Text='<%# getName(Convert.ToInt32( Eval("CustomerID")))%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("ContractDate")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbltotalamountCompleted" runat="server" Text='<%# gettotalAmaont(Convert.ToInt32( Eval("CustomerID")))%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <span class="label label-sm label-warning">Completed </span>
                                                                        </td>
                                                                        <%--<td>
                                                                    <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                                </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="tab-pane " id="overview_7">
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
                                                                    <span class="label label-sm label-danger">Canceled </span>
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
                                                                    <span class="label label-sm label-danger">Canceled </span>
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
                                                                    <span class="label label-sm label-danger">Canceled </span>
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
                                                                    <span class="label label-sm label-danger">Canceled </span>
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
                                                                    <span class="label label-sm label-danger">Canceled </span>
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
                                                                    <span class="label label-sm label-danger">Canceled </span>
                                                                </td>
                                                                <td>
                                                                    <a href="javascript:;" class="btn default btn-xs green-stripe">View </a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="tab-pane " id="overview_1">
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
                                                                    <a href="javascript:;" style="color: #5b9bd1">digital53 - Responsive Admin + Frontend Theme </a>
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
                                          
                                          

                                            <div class="tab-pane active" id="overview_8">

                                                <div class="tools">

                                                    <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-solid input-circle" placeholder="Search" Style="width: 600px;"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" class="btn btn-circle green btn-outline btn-sm" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="lblcount" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Large" Text=""></asp:Label>
                                                </div>


                                                <div class="tools">
                                                </div>

                                                <div class="table-responsive">
                                                    <table class="table table-striped table-hover table-bordered">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Bar Code</th>
                                                                <th>Product Name</th>
                                                                <th>UOM</th>
                                                                <th>Alternate-2</th>
                                                                <th>Brand</th>
                                                                <th>QTY In Hand</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="listMinimumAndMax" runat="server">
                                                                <ItemTemplate>
                                                                    <tr class="gradeA">
                                                                        <td>
                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl='<%# "../ECOMM/PRODUCTMASTER.aspx?ID="+ Eval("MYPRODID") %>'>
                                                                                <asp:Label ID="Label5" Text='<%# Eval("BarCode") %>' runat="server"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <%--<a href="../ECOMM/PRODUCTMASTER.aspx?ID=" <%# Eval("MYPRODID") %>> </a>--%>
                                                                            <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl='<%# "../ECOMM/PRODUCTMASTER.aspx?ID="+ Eval("MYPRODID") %>'>
                                                                                <asp:Label ID="Label1" Text='<%# getproductcode(Convert.ToInt32( Eval("MyProdID"))) %>' runat="server"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl='<%# "../ECOMM/PRODUCTMASTER.aspx?ID="+ Eval("MYPRODID") %>'>
                                                                                <asp:Label ID="Label2" Text='<%# Eval("UOM") %>' runat="server"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl='<%# "../ECOMM/PRODUCTMASTER.aspx?ID="+ Eval("MYPRODID") %>'>
                                                                                <asp:Label ID="Label6" Text='<%# Eval("AlternateCode2") %>' runat="server"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl='<%# "../ECOMM/PRODUCTMASTER.aspx?ID="+ Eval("MYPRODID") %>'>
                                                                                <asp:Label ID="Label3" Text='<%# Eval("BrandName") %>' runat="server"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <%-- <asp:Label ID="Label4" Text='<%# Eval("msrp") %>' runat="server"></asp:Label>--%>

                                                                        </td>

                                                                    </tr>

                                                                </ItemTemplate>
                                                            </asp:Repeater>
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

                    </div>

                    <%-- <div class="row">
                        
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat2 bordered">
                                <div class="display">
                                    <div class="number">
                                        <h3 class="font-purple-soft">
                                            <span data-counter="counterup" data-value="276">
                                                <asp:Label ID="lblbox5KD" runat="server" Text="0"></asp:Label>
                                            </span>
                                            <small class="font-green-sharp">KD</small>
                                        </h3>
                                        <small>Today
                                            <asp:Label ID="TodayDatePurchase" runat="server" Text="Label"></asp:Label>
                                            Purchase</small>
                                    </div>
                                    <div class="icon">
                                        <i class="icon-user"></i>
                                    </div>
                                </div>
                                <div class="progress-info">
                                    <div class="progress">
                                        <span style="width: 57%;" class="progress-bar progress-bar-success purple-soft">
                                            <span class="sr-only">56% change</span>
                                        </span>
                                    </div>
                                    <div class="status">
                                        <div class="status-title">change </div>
                                        <div class="status-number">
                                            <asp:Label ID="lblbox5PER" runat="server" Text="50%"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat2 bordered">
                                <div class="display">
                                    <div class="number">
                                        <h3 class="font-purple-soft">
                                            <span data-counter="counterup" data-value="276">
                                                <asp:Label ID="lblbox6KD" runat="server" Text="0"></asp:Label>
                                            </span>
                                            <small class="font-green-sharp">KD</small>
                                        </h3>
                                        <small>This
                                            <asp:Label ID="ThisMonthPurchase" runat="server" Text="Label"></asp:Label>
                                            Purchase</small>
                                    </div>
                                    <div class="icon">
                                        <i class="icon-user"></i>
                                    </div>
                                </div>
                                <div class="progress-info">
                                    <div class="progress">
                                        <span style="width: 57%;" class="progress-bar progress-bar-success purple-soft">
                                            <span class="sr-only">56% change</span>
                                        </span>
                                    </div>
                                    <div class="status">
                                        <div class="status-title">change </div>
                                        <div class="status-number">
                                            <asp:Label ID="lblbox6PER" runat="server" Text="50%"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat2 bordered">
                                <div class="display">
                                    <div class="number">
                                        <h3 class="font-purple-soft">
                                            <span data-counter="counterup" data-value="276">
                                                <asp:Label ID="lblbox7KD" runat="server" Text="0"></asp:Label>
                                            </span>
                                            <small class="font-green-sharp">KD</small>
                                        </h3>
                                        <small>This
                                            <asp:Label ID="ThisYearPurchase" runat="server" Text="Label"></asp:Label>
                                            Purchase</small>
                                    </div>
                                    <div class="icon">
                                        <i class="icon-user"></i>
                                    </div>
                                </div>
                                <div class="progress-info">
                                    <div class="progress">
                                        <span style="width: 57%;" class="progress-bar progress-bar-success purple-soft">
                                            <span class="sr-only">56% change</span>
                                        </span>
                                    </div>
                                    <div class="status">
                                        <div class="status-title">change </div>
                                        <div class="status-number">
                                            <asp:Label ID="lblbox7PER" runat="server" Text="50%"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat2 bordered">
                                <div class="display">
                                    <div class="number">
                                        <h3 class="font-purple-soft">
                                            <span data-counter="counterup" data-value="276">
                                                <asp:Label ID="lblbox8KD" runat="server" Text="0"></asp:Label>
                                            </span>
                                            <small class="font-green-sharp">KD</small>
                                        </h3>
                                        <small>This
                                            <asp:Label ID="ThisYearPurchaseReturn" runat="server" Text="Label"></asp:Label>
                                            Purchase Return</small>
                                    </div>
                                    <div class="icon">
                                        <i class="icon-user"></i>
                                    </div>
                                </div>
                                <div class="progress-info">
                                    <div class="progress">
                                        <span style="width: 57%;" class="progress-bar progress-bar-success purple-soft">
                                            <span class="sr-only">56% change</span>
                                        </span>
                                    </div>
                                    <div class="status">
                                        <div class="status-title">change </div>
                                        <div class="status-number">
                                            <asp:Label ID="lblbox8PER" runat="server" Text="50%"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                       
                    </div>--%>

                    <div class="row">
                        <%-- Dipak --%>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat2 bordered">
                                <div class="display">
                                    <div class="number">
                                        <h3 class="font-purple-soft">
                                            <span data-counter="counterup" data-value="276">
                                                <asp:Label ID="lblbox5KD" runat="server" Text="0"></asp:Label>
                                            </span>
                                            <small class="font-green-sharp">KD</small>
                                        </h3>
                                        <small>Till today Total Sub Sale</small>
                                    </div>
                                    <div class="icon">
                                        <i class="icon-user"></i>
                                    </div>
                                </div>
                                <div class="progress-info">
                                    <div class="progress">
                                        <span style="width: 57%;" class="progress-bar progress-bar-success purple-soft">
                                            <span class="sr-only">56% change</span>
                                        </span>
                                    </div>
                                    <div class="status">
                                        <div class="status-title">change </div>
                                        <div class="status-number">
                                            <asp:Label ID="lblbox5PER" runat="server" Text="50%"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat2 bordered">
                                <div class="display">
                                    <div class="number">
                                        <h3 class="font-purple-soft">
                                            <span data-counter="counterup" data-value="276">
                                                <asp:Label ID="lblbox6KD" runat="server" Text="0"></asp:Label>
                                            </span>
                                            <%--<small class="font-green-sharp">KD</small>--%>
                                        </h3>
                                        <small>Total Delivery</small>
                                    </div>
                                    <div class="icon">
                                        <i class="icon-user"></i>
                                    </div>
                                </div>
                                <div class="progress-info">
                                    <div class="progress">
                                        <span style="width: 57%;" class="progress-bar progress-bar-success purple-soft">
                                            <span class="sr-only">56% change</span>
                                        </span>
                                    </div>
                                    <div class="status">
                                        <div class="status-title">change </div>
                                        <div class="status-number">
                                            <asp:Label ID="lblbox6PER" runat="server" Text="50%"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat2 bordered">
                                <div class="display">
                                    <div class="number">
                                        <h3 class="font-purple-soft">
                                            <span data-counter="counterup" data-value="276">
                                                <asp:Label ID="lblbox7KD" runat="server" Text="0"></asp:Label>
                                            </span>
                                            <%-- <small class="font-green-sharp">KD</small>--%>
                                        </h3>
                                        <small>Delivery Returned</small>
                                    </div>
                                    <div class="icon">
                                        <i class="icon-user"></i>
                                    </div>
                                </div>
                                <div class="progress-info">
                                    <div class="progress">
                                        <span style="width: 57%;" class="progress-bar progress-bar-success purple-soft">
                                            <span class="sr-only">56% change</span>
                                        </span>
                                    </div>
                                    <div class="status">
                                        <div class="status-title">change </div>
                                        <div class="status-number">
                                            <asp:Label ID="lblbox7PER" runat="server" Text="50%"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <div class="dashboard-stat2 bordered">
                                <div class="display">
                                    <div class="number">
                                        <h3 class="font-purple-soft">
                                            <span data-counter="counterup" data-value="276">
                                                <asp:Label ID="lblbox8KD" runat="server" Text="0"></asp:Label>
                                            </span>
                                            <%--<small class="font-green-sharp">KD</small>--%>
                                        </h3>
                                        <small>Total Production in Kitchen</small>
                                    </div>
                                    <div class="icon">
                                        <i class="icon-user"></i>
                                    </div>
                                </div>
                                <div class="progress-info">
                                    <div class="progress">
                                        <span style="width: 57%;" class="progress-bar progress-bar-success purple-soft">
                                            <span class="sr-only">56% change</span>
                                        </span>
                                    </div>
                                    <div class="status">
                                        <div class="status-title">change </div>
                                        <div class="status-number">
                                            <asp:Label ID="lblbox8PER" runat="server" Text="50%"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%-- Dipak --%>
                    </div>

                   
                 
                   
                    
                   
                  
                   
                    <!-- END PAGE BASE CONTENT -->
                </div>
                <!-- END CONTENT BODY -->
            </div>
            <!-- END CONTENT -->
            <!-- BEGIN QUICK SIDEBAR -->
            <a href="javascript:;" class="page-quick-sidebar-toggler">
                <i class="icon-login"></i>
            </a>
            <div class="page-quick-sidebar-wrapper" data-close-on-body-click="false">
                <div class="page-quick-sidebar">
                    <ul class="nav nav-tabs">
                        <li class="active">
                            <a href="javascript:;" data-target="#quick_sidebar_tab_1" data-toggle="tab">Users
                                <span class="badge badge-danger">2</span>
                            </a>
                        </li>
                        <li>
                            <a href="javascript:;" data-target="#quick_sidebar_tab_2" data-toggle="tab">Alerts
                                <span class="badge badge-success">7</span>
                            </a>
                        </li>
                        <li class="dropdown">
                            <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown">More
                                <i class="fa fa-angle-down"></i>
                            </a>
                            <ul class="dropdown-menu pull-right">
                                <li>
                                    <a href="javascript:;" data-target="#quick_sidebar_tab_3" data-toggle="tab">
                                        <i class="icon-bell"></i>Alerts </a>
                                </li>
                                <li>
                                    <a href="javascript:;" data-target="#quick_sidebar_tab_3" data-toggle="tab">
                                        <i class="icon-info"></i>Notifications </a>
                                </li>
                                <li>
                                    <a href="javascript:;" data-target="#quick_sidebar_tab_3" data-toggle="tab">
                                        <i class="icon-speech"></i>Activities </a>
                                </li>
                                <li class="divider"></li>
                                <li>
                                    <a href="javascript:;" data-target="#quick_sidebar_tab_3" data-toggle="tab">
                                        <i class="icon-settings"></i>Settings </a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active page-quick-sidebar-chat" id="quick_sidebar_tab_1">
                            <div class="page-quick-sidebar-chat-users" data-rail-color="#ddd" data-wrapper-class="page-quick-sidebar-list">
                                <h3 class="list-heading">Staff</h3>
                                <ul class="media-list list-items">
                                    <li class="media">
                                        <div class="media-status">
                                            <span class="badge badge-success">8</span>
                                        </div>
                                        <img class="media-object" src="../assets/layouts/layout/img/avatar3.jpg" alt="...">
                                        <div class="media-body">
                                            <h4 class="media-heading">Bob Nilson</h4>
                                            <div class="media-heading-sub">Project Manager </div>
                                        </div>
                                    </li>
                                    <li class="media">
                                        <img class="media-object" src="../assets/layouts/layout/img/avatar1.jpg" alt="...">
                                        <div class="media-body">
                                            <h4 class="media-heading">Nick Larson</h4>
                                            <div class="media-heading-sub">Art Director </div>
                                        </div>
                                    </li>
                                    <li class="media">
                                        <div class="media-status">
                                            <span class="badge badge-danger">3</span>
                                        </div>
                                        <img class="media-object" src="../assets/layouts/layout/img/avatar4.jpg" alt="...">
                                        <div class="media-body">
                                            <h4 class="media-heading">Deon Hubert</h4>
                                            <div class="media-heading-sub">CTO </div>
                                        </div>
                                    </li>
                                    <li class="media">
                                        <img class="media-object" src="../assets/layouts/layout/img/avatar2.jpg" alt="...">
                                        <div class="media-body">
                                            <h4 class="media-heading">Ella Wong</h4>
                                            <div class="media-heading-sub">CEO </div>
                                        </div>
                                    </li>
                                </ul>
                                <h3 class="list-heading">Customers</h3>
                                <ul class="media-list list-items">
                                    <li class="media">
                                        <div class="media-status">
                                            <span class="badge badge-warning">2</span>
                                        </div>
                                        <img class="media-object" src="../assets/layouts/layout/img/avatar6.jpg" alt="...">
                                        <div class="media-body">
                                            <h4 class="media-heading">Lara Kunis</h4>
                                            <div class="media-heading-sub">CEO, Loop Inc </div>
                                            <div class="media-heading-small">Last seen 03:10 AM </div>
                                        </div>
                                    </li>
                                    <li class="media">
                                        <div class="media-status">
                                            <span class="label label-sm label-success">new</span>
                                        </div>
                                        <img class="media-object" src="../assets/layouts/layout/img/avatar7.jpg" alt="...">
                                        <div class="media-body">
                                            <h4 class="media-heading">Ernie Kyllonen</h4>
                                            <div class="media-heading-sub">
                                                Project Manager,
                                                <br>
                                                SmartBizz PTL
                                            </div>
                                        </div>
                                    </li>
                                    <li class="media">
                                        <img class="media-object" src="../assets/layouts/layout/img/avatar8.jpg" alt="...">
                                        <div class="media-body">
                                            <h4 class="media-heading">Lisa Stone</h4>
                                            <div class="media-heading-sub">CTO, Keort Inc </div>
                                            <div class="media-heading-small">Last seen 13:10 PM </div>
                                        </div>
                                    </li>
                                    <li class="media">
                                        <div class="media-status">
                                            <span class="badge badge-success">7</span>
                                        </div>
                                        <img class="media-object" src="../assets/layouts/layout/img/avatar9.jpg" alt="...">
                                        <div class="media-body">
                                            <h4 class="media-heading">Deon Portalatin</h4>
                                            <div class="media-heading-sub">CFO, H&D LTD </div>
                                        </div>
                                    </li>
                                    <li class="media">
                                        <img class="media-object" src="../assets/layouts/layout/img/avatar10.jpg" alt="...">
                                        <div class="media-body">
                                            <h4 class="media-heading">Irina Savikova</h4>
                                            <div class="media-heading-sub">CEO, Tizda Motors Inc </div>
                                        </div>
                                    </li>
                                    <li class="media">
                                        <div class="media-status">
                                            <span class="badge badge-danger">4</span>
                                        </div>
                                        <img class="media-object" src="../assets/layouts/layout/img/avatar11.jpg" alt="...">
                                        <div class="media-body">
                                            <h4 class="media-heading">Maria Gomez</h4>
                                            <div class="media-heading-sub">Manager, Infomatic Inc </div>
                                            <div class="media-heading-small">Last seen 03:10 AM </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="page-quick-sidebar-item">
                                <div class="page-quick-sidebar-chat-user">
                                    <div class="page-quick-sidebar-nav">
                                        <a href="javascript:;" class="page-quick-sidebar-back-to-list">
                                            <i class="icon-arrow-left"></i>Back</a>
                                    </div>
                                    <div class="page-quick-sidebar-chat-user-messages">
                                        <div class="post out">
                                            <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar3.jpg" />
                                            <div class="message">
                                                <span class="arrow"></span>
                                                <a href="javascript:;" class="name">Bob Nilson</a>
                                                <span class="datetime">20:15</span>
                                                <span class="body">When could you send me the report ? </span>
                                            </div>
                                        </div>
                                        <div class="post in">
                                            <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar2.jpg" />
                                            <div class="message">
                                                <span class="arrow"></span>
                                                <a href="javascript:;" class="name">Ella Wong</a>
                                                <span class="datetime">20:15</span>
                                                <span class="body">Its almost done. I will be sending it shortly </span>
                                            </div>
                                        </div>
                                        <div class="post out">
                                            <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar3.jpg" />
                                            <div class="message">
                                                <span class="arrow"></span>
                                                <a href="javascript:;" class="name">Bob Nilson</a>
                                                <span class="datetime">20:15</span>
                                                <span class="body">Alright. Thanks! :) </span>
                                            </div>
                                        </div>
                                        <div class="post in">
                                            <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar2.jpg" />
                                            <div class="message">
                                                <span class="arrow"></span>
                                                <a href="javascript:;" class="name">Ella Wong</a>
                                                <span class="datetime">20:16</span>
                                                <span class="body">You are most welcome. Sorry for the delay. </span>
                                            </div>
                                        </div>
                                        <div class="post out">
                                            <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar3.jpg" />
                                            <div class="message">
                                                <span class="arrow"></span>
                                                <a href="javascript:;" class="name">Bob Nilson</a>
                                                <span class="datetime">20:17</span>
                                                <span class="body">No probs. Just take your time :) </span>
                                            </div>
                                        </div>
                                        <div class="post in">
                                            <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar2.jpg" />
                                            <div class="message">
                                                <span class="arrow"></span>
                                                <a href="javascript:;" class="name">Ella Wong</a>
                                                <span class="datetime">20:40</span>
                                                <span class="body">Alright. I just emailed it to you. </span>
                                            </div>
                                        </div>
                                        <div class="post out">
                                            <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar3.jpg" />
                                            <div class="message">
                                                <span class="arrow"></span>
                                                <a href="javascript:;" class="name">Bob Nilson</a>
                                                <span class="datetime">20:17</span>
                                                <span class="body">Great! Thanks. Will check it right away. </span>
                                            </div>
                                        </div>
                                        <div class="post in">
                                            <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar2.jpg" />
                                            <div class="message">
                                                <span class="arrow"></span>
                                                <a href="javascript:;" class="name">Ella Wong</a>
                                                <span class="datetime">20:40</span>
                                                <span class="body">Please let me know if you have any comment. </span>
                                            </div>
                                        </div>
                                        <div class="post out">
                                            <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar3.jpg" />
                                            <div class="message">
                                                <span class="arrow"></span>
                                                <a href="javascript:;" class="name">Bob Nilson</a>
                                                <span class="datetime">20:17</span>
                                                <span class="body">Sure. I will check and buzz you if anything needs to be corrected. </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="page-quick-sidebar-chat-user-form">
                                        <div class="input-group">
                                            <input type="text" class="form-control" placeholder="Type a message here...">
                                            <div class="input-group-btn">
                                                <button type="button" class="btn green">
                                                    <i class="icon-paper-clip"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane page-quick-sidebar-alerts" id="quick_sidebar_tab_2">
                            <div class="page-quick-sidebar-alerts-list">
                                <h3 class="list-heading">General</h3>
                                <ul class="feeds list-items">
                                    <li>
                                        <div class="col1">
                                            <div class="cont">
                                                <div class="cont-col1">
                                                    <div class="label label-sm label-info">
                                                        <i class="fa fa-check"></i>
                                                    </div>
                                                </div>
                                                <div class="cont-col2">
                                                    <div class="desc">
                                                        You have 4 pending tasks.
                                                        <span class="label label-sm label-warning ">Take action
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
                                                            <i class="fa fa-bar-chart-o"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">Finance Report for year 2013 has been released. </div>
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
                                                        <i class="fa fa-user"></i>
                                                    </div>
                                                </div>
                                                <div class="cont-col2">
                                                    <div class="desc">You have 5 pending membership that requires a quick review. </div>
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
                                                        <i class="fa fa-shopping-cart"></i>
                                                    </div>
                                                </div>
                                                <div class="cont-col2">
                                                    <div class="desc">
                                                        New order received with
                                                        <span class="label label-sm label-success">Reference Number: DR23923 </span>
                                                    </div>
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
                                                        <i class="fa fa-user"></i>
                                                    </div>
                                                </div>
                                                <div class="cont-col2">
                                                    <div class="desc">You have 5 pending membership that requires a quick review. </div>
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
                                                        <i class="fa fa-bell-o"></i>
                                                    </div>
                                                </div>
                                                <div class="cont-col2">
                                                    <div class="desc">
                                                        Web server hardware needs to be upgraded.
                                                        <span class="label label-sm label-warning">Overdue </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col2">
                                            <div class="date">2 hours </div>
                                        </div>
                                    </li>
                                    <li>
                                        <a href="javascript:;">
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-default">
                                                            <i class="fa fa-briefcase"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">IPO Report for year 2013 has been released. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">20 mins </div>
                                            </div>
                                        </a>
                                    </li>
                                </ul>
                                <h3 class="list-heading">System</h3>
                                <ul class="feeds list-items">
                                    <li>
                                        <div class="col1">
                                            <div class="cont">
                                                <div class="cont-col1">
                                                    <div class="label label-sm label-info">
                                                        <i class="fa fa-check"></i>
                                                    </div>
                                                </div>
                                                <div class="cont-col2">
                                                    <div class="desc">
                                                        You have 4 pending tasks.
                                                        <span class="label label-sm label-warning ">Take action
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
                                                        <div class="label label-sm label-danger">
                                                            <i class="fa fa-bar-chart-o"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">Finance Report for year 2013 has been released. </div>
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
                                                    <div class="label label-sm label-default">
                                                        <i class="fa fa-user"></i>
                                                    </div>
                                                </div>
                                                <div class="cont-col2">
                                                    <div class="desc">You have 5 pending membership that requires a quick review. </div>
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
                                                        <i class="fa fa-shopping-cart"></i>
                                                    </div>
                                                </div>
                                                <div class="cont-col2">
                                                    <div class="desc">
                                                        New order received with
                                                        <span class="label label-sm label-success">Reference Number: DR23923 </span>
                                                    </div>
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
                                                        <i class="fa fa-user"></i>
                                                    </div>
                                                </div>
                                                <div class="cont-col2">
                                                    <div class="desc">You have 5 pending membership that requires a quick review. </div>
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
                                                    <div class="label label-sm label-warning">
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
                                        <a href="javascript:;">
                                            <div class="col1">
                                                <div class="cont">
                                                    <div class="cont-col1">
                                                        <div class="label label-sm label-info">
                                                            <i class="fa fa-briefcase"></i>
                                                        </div>
                                                    </div>
                                                    <div class="cont-col2">
                                                        <div class="desc">IPO Report for year 2013 has been released. </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col2">
                                                <div class="date">20 mins </div>
                                            </div>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="tab-pane page-quick-sidebar-settings" id="quick_sidebar_tab_3">
                            <div class="page-quick-sidebar-settings-list">
                                <h3 class="list-heading">General Settings</h3>
                                <ul class="list-items borderless">
                                    <li>Enable Notifications
                                        <input type="checkbox" class="make-switch" checked data-size="small" data-on-color="success" data-on-text="ON" data-off-color="default" data-off-text="OFF">
                                    </li>
                                    <li>Allow Tracking
                                        <input type="checkbox" class="make-switch" data-size="small" data-on-color="info" data-on-text="ON" data-off-color="default" data-off-text="OFF">
                                    </li>
                                    <li>Log Errors
                                        <input type="checkbox" class="make-switch" checked data-size="small" data-on-color="danger" data-on-text="ON" data-off-color="default" data-off-text="OFF">
                                    </li>
                                    <li>Auto Sumbit Issues
                                        <input type="checkbox" class="make-switch" data-size="small" data-on-color="warning" data-on-text="ON" data-off-color="default" data-off-text="OFF">
                                    </li>
                                    <li>Enable SMS Alerts
                                        <input type="checkbox" class="make-switch" checked data-size="small" data-on-color="success" data-on-text="ON" data-off-color="default" data-off-text="OFF">
                                    </li>
                                </ul>
                                <h3 class="list-heading">System Settings</h3>
                                <ul class="list-items borderless">
                                    <li>Security Level
                                        <select class="form-control input-inline input-sm input-small">
                                            <option value="1">Normal</option>
                                            <option value="2" selected>Medium</option>
                                            <option value="e">High</option>
                                        </select>
                                    </li>
                                    <li>Failed Email Attempts
                                        <input class="form-control input-inline input-sm input-small" value="5" />
                                    </li>
                                    <li>Secondary SMTP Port
                                        <input class="form-control input-inline input-sm input-small" value="3560" />
                                    </li>
                                    <li>Notify On System Error
                                        <input type="checkbox" class="make-switch" checked data-size="small" data-on-color="danger" data-on-text="ON" data-off-color="default" data-off-text="OFF">
                                    </li>
                                    <li>Notify On SMTP Error
                                        <input type="checkbox" class="make-switch" checked data-size="small" data-on-color="warning" data-on-text="ON" data-off-color="default" data-off-text="OFF">
                                    </li>
                                </ul>
                                <div class="inner-content">
                                    <button class="btn btn-success">
                                        <i class="icon-settings"></i>Save Changes</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END QUICK SIDEBAR -->
        </div>
        <!-- END CONTAINER -->
        <!-- BEGIN FOOTER -->
       <%-- <div class="page-footer">
            <div class="page-footer-inner">
                2016 &copy; digital53 
            </div>
            <div class="scroll-to-top">
                <i class="icon-arrow-up"></i>
            </div>
        </div>--%>
        <!-- END FOOTER -->
        <!-- BEGIN QUICK NAV -->
        <nav class="quick-nav">
            <a class="quick-nav-trigger" href="#0">
                <span aria-hidden="true"></span>
            </a>
            <ul>
                <li>
                    <a href="https://themeforest.net/item/digital53-responsive-admin-dashboard-template/4021469?ref=keenthemes" target="_blank" class="active">
                        <span>Purchase digital53</span>
                        <i class="icon-basket"></i>
                    </a>
                </li>
                <li>
                    <a href="https://themeforest.net/item/digital53-responsive-admin-dashboard-template/reviews/4021469?ref=keenthemes" target="_blank">
                        <span>Customer Reviews</span>
                        <i class="icon-users"></i>
                    </a>
                </li>
                <li>
                    <a href="http://keenthemes.com/showcast/" target="_blank">
                        <span>Showcase</span>
                        <i class="icon-user"></i>
                    </a>
                </li>
                <li>
                    <a href="http://keenthemes.com/digital53-theme/changelog/" target="_blank">
                        <span>Changelog</span>
                        <i class="icon-graph"></i>
                    </a>
                </li>
            </ul>
            <span aria-hidden="true" class="quick-nav-bg"></span>
        </nav>
        <div class="quick-nav-overlay"></div>
        <!-- END QUICK NAV -->
        <!--[if lt IE 9]>
<script src="../assets/global/plugins/respond.min.js"></script>
<script src="../assets/global/plugins/excanvas.min.js"></script> 
<script src="../assets/global/plugins/ie8.fix.min.js"></script> 
<![endif]-->
    </form>
    <!-- BEGIN CORE PLUGINS -->
    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="../assets/global/plugins/moment.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/morris/morris.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/morris/raphael-min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/counterup/jquery.waypoints.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/counterup/jquery.counterup.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amcharts/amcharts.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amcharts/serial.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amcharts/pie.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amcharts/radar.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amcharts/themes/light.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amcharts/themes/patterns.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amcharts/themes/chalk.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/ammap/ammap.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/ammap/maps/js/worldLow.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amstockcharts/amstock.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/horizontal-timeline/horizontal-timeline.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.resize.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.categories.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-easypiechart/jquery.easypiechart.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.sparkline.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jqvmap/jqvmap/jquery.vmap.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.russia.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.world.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.europe.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.germany.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.usa.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jqvmap/jqvmap/data/jquery.vmap.sampledata.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL SCRIPTS -->
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <!-- END THEME GLOBAL SCRIPTS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="../assets/pages/scripts/dashboard.min.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- BEGIN THEME LAYOUT SCRIPTS -->
    <script src="../assets/layouts/layout4/scripts/layout.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/layout4/scripts/demo.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>
    <!-- END THEME LAYOUT SCRIPTS -->
    <script>
        $(document).ready(function () {
            $('#clickmewow').click(function () {
                $('#radio1003').attr('checked', 'checked');
            });
        })
    </script>
</body>

</html>
