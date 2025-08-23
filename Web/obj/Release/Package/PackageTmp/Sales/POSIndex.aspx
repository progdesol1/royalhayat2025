<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POSIndex.aspx.cs" Inherits="Web.Sales.POSIndex" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>digital53 Admin Theme #4 | Admin Dashboard 2</title>
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
    <script src="../assets/global/plugins/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
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
    <%--<link rel="shortcut icon" href="favicon.ico" />--%>
    <link rel="icon" type="image/png" href="/favicon.ico" />
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

        //var message = "Right click Disabled!";

        /////////////////////////////////
        //function clickIE4() {
        //    if (event.button == 2) {
        //        alert(message);
        //        return false;
        //    }
        //}

        //function clickNS4(e) {
        //    if (document.layers || document.getElementById && !document.all) {
        //        if (e.which == 2 || e.which == 3) {
        //            alert(message);
        //            return false;
        //        }
        //    }
        //}

        //if (document.layers) {
        //    document.captureEvents(Event.MOUSEDOWN);
        //    document.onmousedown = clickNS4;events
        //}

        //else if (document.all && !document.getElementById) {
        //    document.onmousedown = clickIE4;
        //}

        //document.oncontextmenu = new Function("alert(message);return false");
    </script>
    <style>
        .fa-comments:before {
            content: "\f000";
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="toolscriptmanagerID" runat="server">
        </asp:ScriptManager>
        <div class="clearfix"></div>
        <div class="page-container" style="margin-left: -20px; margin-top: 0px; padding-top: 0px;">
            <div class="page-content-wrapper">
                <div class="page-content" style="margin-left: 0px; padding-top: 0px;">
                    <ul class="page-breadcrumb breadcrumb" style="padding-bottom: 0px;">
                        <li>
                            <a href="index.html">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="active">Dashboard</span>
                        </li>
                    </ul>


                    <div class="row">
                        <div class="col-md-12">

                            <div class="portlet-body">
                                <div class="tab-content">
                                    <div class="tiles">
                                        <asp:ListView ID="ListQuickLink" runat="server" Visible="false">
                                            <ItemTemplate>
                                                <a href='<%# Eval("LinkURL") %>'>
                                                    <div class='<%# Eval("Colour") %>'>
                                                        <div class="tile-body">
                                                            <img src="../POS/img/link.png" />
                                                        </div>
                                                        <div class="tile-object">
                                                            <div class="name">
                                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("MenuName") %>'></asp:Label>
                                                            </div>
                                                            <div class="number">
                                                                <i class="m-icon-swapright m-icon-white"></i>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </a>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                            <%-- </div>--%>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">

                            <a href="#">
                                <div class="dashboard-stat blue-madison">
                                    <div class="visual">
                                        <%-- <i class="fa fa-comments"></i>--%>
                                        <img src="../POS/img/Today.png" style="width: 50px;" />
                                    </div>
                                    <div class="details">
                                        <div class="number">
                                            <asp:Label ID="lblbox1KD" runat="server" Text="0"></asp:Label>
                                        </div>

                                        <div class="desc">
                                            <%--<asp:Label ID="TODAYDate" runat="server" Text=""></asp:Label>--%>&nbsp;Feedbackt
                                        </div>
                                    </div>
                                    <div class="more">
                                        Today's Feedbacks <i class="m-icon-swapright m-icon-white"></i>
                                    </div>
                                </div>
                            </a>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">

                            <a href="#">
                                <div class="dashboard-stat red-intense">
                                    <div class="visual">
                                        <%--<i class="fa fa-bar-chart-o" style="content: box;"></i>--%>
                                        <img src="../POS/img/Month.png" style="width: 50px;" />
                                    </div>
                                    <div class="details">
                                        <div class="number">
                                            <asp:Label ID="lblbox2KD" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="desc">
                                            <asp:Label ID="Thismonth" runat="server" Text=""></asp:Label>&nbsp;Feedbackt
                                        </div>
                                    </div>
                                    <div class="more">
                                        Today Closed Feedbacks  <i class="m-icon-swapright m-icon-white"></i>
                                    </div>
                                </div>
                            </a>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">

                            <div class="dashboard-stat purple-plum">
                                <div class="visual">
                                    <%--<i class="fa fa-bar-chart-o"></i>--%>
                                    <img src="../POS/img/YED.png" style="width: 45px;" />
                                </div>
                                <div class="details">
                                    <div class="number">
                                        <asp:Label ID="lblbox3KD" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="desc">
                                        <asp:Label ID="ThisYear" runat="server" Text=""></asp:Label>&nbsp;Feedbackt
                                    </div>
                                </div>
                                <div class="more">
                                    This Week's Open Feedbacks <i class="m-icon-swapright m-icon-white"></i>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">

                            <div class="dashboard-stat green-haze">
                                <div class="visual">
                                    <%--<i class="fa fa-bar-chart-o"></i>--%>
                                    <img src="../POS/img/week.png" />
                                </div>
                                <div class="details">
                                    <div class="number">
                                        <asp:Label ID="lblbox4KD" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="desc">
                                        <asp:Label ID="ThisYearReturn" runat="server"></asp:Label>
                                        &nbsp;Feedbackt 
                                    </div>
                                </div>
                                <div class="more">
                                    This Week Closed Feedbacks <i class="m-icon-swapright m-icon-white"></i>
                                </div>
                            </div>
                        </div>


                    </div>

                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">


                            <div class="dashboard-stat blue-dark">
                                <div class="visual">
                                    <i class="fa fa-bar-chart-o"></i>
                                </div>
                                <div class="details">
                                    <div class="number">
                                        <asp:Label ID="lblbox5KD" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="desc">
                                        <asp:Label ID="TodayDatePurchase" runat="server" Text=""></asp:Label>&nbsp;Feedbackt
                                    </div>
                                </div>
                                <div class="more">
                                    This Month's Open Feedbacks <i class="m-icon-swapright m-icon-white"></i>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">

                            <div class="dashboard-stat red-intense">
                                <div class="visual">
                                    <i class="fa fa-bar-chart-o"></i>
                                </div>
                                <div class="details">
                                    <div class="number">
                                        <asp:Label ID="lblbox6KD" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="desc">
                                        <asp:Label ID="ThisMonthPurchase" runat="server" Text=""></asp:Label>&nbsp;Feedbackt
                                    </div>
                                </div>
                                <div class="more">
                                    This Month Closed Feedbacks  <i class="m-icon-swapright m-icon-white"></i>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">

                            <div class="dashboard-stat purple-plum">
                                <div class="visual">
                                    <i class="fa fa-bar-chart-o"></i>
                                </div>
                                <div class="details">
                                    <div class="number">
                                        <asp:Label ID="lblbox7KD" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="desc">
                                        <asp:Label ID="ThisYearPurchase" runat="server" Text=""></asp:Label>&nbsp;Feedbackt
                                    </div>
                                </div>
                                <div class="more">
                                    This Year's Open Feedbacks  <i class="m-icon-swapright m-icon-white"></i>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">

                            <div class="dashboard-stat green-jungle">
                                <div class="visual">
                                    <i class="fa fa-bar-chart-o"></i>
                                </div>
                                <div class="details">
                                    <div class="number">
                                        <asp:Label ID="lblbox8KD" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="desc">
                                        <%--<asp:Label ID="ThisYearPurchaseReturn" runat="server" Text=""></asp:Label>--%>&nbsp;Feedbackt
                                    </div>
                                </div>
                                <div class="more">
                                    This Year Closed Feedbacks <i class="m-icon-swapright m-icon-white"></i>
                                </div>
                            </div>
                        </div>
                    </div>





                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box blue">

                                <asp:ListView ID="ListViewLogdata" runat="server" Visible="false">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ActivityName") %>'></asp:Label></td>

                                            <td style="text-align: center;">
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Logdatetime") %>'></asp:Label></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>




                            <br />
                            <br />

                            <a href="../POS/ClientTiketR.aspx">
                                <img src="../Smilies/Smilies/co1.png" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <a href="../POS/ClientTiketR.aspx?status=pending">
                            <img src="../Smilies/Smilies/co22.png" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       <a href="../ReportMst/HelpDeskReport.aspx">
                           <img src="../Smilies/Smilies/co3.png" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       <a href="../ReportMst/HelpDeskExcRep.aspx">
                           <img src="../Smilies/Smilies/com44.png" /></a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <a href="../ReportMst/DayReport.aspx">
                             <img src="../Smilies/Smilies/DR.png" style="height: 95px; width: 99px;" /></a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <br />
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet box yellow-gold">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="icon-bar-chart font-green-sharp"></i>
                                                <span class="caption-subject font-white bold uppercase">SEARCH ENGINE</span>
                                                <span class="caption-helper font-grey">Various way to search in the Data...</span>
                                            </div>

                                            <div class="tools">
                                                <asp:Button ID="btnshopAct" runat="server" CssClass="btn btn-circle red btn-outline" Text="Action" Style="display: none;" />
                                                <asp:Button ID="btnFastCarReport" runat="server" Style="display: none;" CssClass="btn btn-circle green btn-outline" Text="ALL Report" />
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
                                                    <li style="background-color: #E9F6BC;">
                                                        <a href="#overview_8" data-toggle="tab">Patient Name</a>
                                                    </li>
                                                    <li class="dropdown" style="background-color: #e6ffe6;">
                                                        <a href="#overview_9" data-toggle="tab">MRN No 
                                                        </a>
                                                    </li>
                                                    <li style="background-color: #FEF2BA;">
                                                        <a href="#overview_1" data-toggle="tab">Feedbackt No </a>
                                                    </li>
                                                    <%-- <li style="background-color: #F2DEDE;">
                                                <a href="#overview_2" data-toggle="tab">Most Viewed </a>
                                            </li>--%>
                                                    <li style="background-color: #edf2f8;">
                                                        <a href="#overview_3" data-toggle="tab">Contact No </a>
                                                    </li>

                                                    <li style="background-color: #E9F6BC;">
                                                        <a href="#overview_10" data-toggle="tab">Comments Text </a>
                                                    </li>

                                                </ul>

                                                <div class="tab-content">
                                                    <div class="tab-pane" id="overview_4">
                                                        <div class="table-responsive">
                                                            <table class="table table-striped table-hover table-bordered">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Client Name
                                                                        </th>
                                                                        <th>MRN No
                                                                        </th>
                                                                        <th>Contact
                                                                        </th>
                                                                        <th>Feedbackt No
                                                                        </th>
                                                                        <th>Total Time
                                                                        </th>
                                                                        <th>Feedback Details
                                                                        </th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:ListView ID="ListOrderTop10" runat="server">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <a href="../POS/ClientTiketR.aspx">
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Patient_Name") %>'></asp:Label></a></td>
                                                                                <td>
                                                                                    <a href="../POS/ClientTiketR.aspx">
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("MRN") %>'></asp:Label></a></td>
                                                                                </td>
                                                                        <td>
                                                                            <a href="../POS/ClientTiketR.aspx">
                                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("Contact") %>'></asp:Label></a></td>
                                                                                </td>
                                                                        <td>
                                                                            <a href="../POS/ClientTiketR.aspx">
                                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("TickFeedbackType") %>'></asp:Label></a></td>
                                                                                </td>
                                                                                 <td>
                                                                                     <a href="../POS/ClientTiketR.aspx">
                                                                                         <asp:Label ID="Label4" runat="server" Text='<%# Eval("TickFeedbackType") %>'></asp:Label></a></td>
                                                                                </td>
                                                                        <td>
                                                                            <a href="../POS/ClientTiketR.aspx">
                                                                                <asp:Label ID="Label16" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label></a></td>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>

                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>

                                                    <div class="tab-pane " id="overview_1">
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>

                                                                <div class="tools" style="padding-bottom: 10px;">

                                                                    <asp:TextBox ID="txtxFeedbackt" runat="server" CssClass="input-circle" Style="width: 60%; height: 34px; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc; box-shadow: none; transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s; -webkit-border-radius: 4px; -moz-border-radius: 4px; -ms-border-radius: 4px; -o-border-radius: 4px; border-radius: 4px;" placeholder="Feedbackt Number"></asp:TextBox>


                                                                    <asp:Button ID="btnsearch2" class="btn btn-circle green btn-outline btn-sm" runat="server" Text="Search" OnClick="btnsearch2_Click" />
                                                                    <%--<asp:Label ID="lblcount" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Large" Text=""></asp:Label>--%>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <table class="table table-striped table-hover table-bordered">
                                                                        <thead>
                                                                            <tr>

                                                                                <th>Patient Name</th>
                                                                                <th>MRN No</th>
                                                                                <th>Contact</th>
                                                                                <th>Feedback No</th>
                                                                                <th>Total Time</th>
                                                                                <th>Feedback Details</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="listmaxFeedbacktno" runat="server" OnItemCommand="listmaxFeedbacktno_ItemCommand">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="lnkpn" runat="server" CommandName="lnkpn" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="lblpt" runat="server" Text='<%# Eval("Patient_Name") %>'></asp:Label>
                                                                                                <asp:Label ID="lblm1" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="lnkmrn" runat="server" CommandName="lnkmrn" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="lblmrn" runat="server" Text='<%# Eval("MRN") %>'></asp:Label>
                                                                                                <asp:Label ID="lblm2" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="lnkcontack" runat="server" CommandName="lnkcontack" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="lblcontact" runat="server" Text='<%# Eval("Contact") %>'></asp:Label>
                                                                                                <asp:Label ID="lblm3" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="lnkcom" runat="server" CommandName="lnkcom" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="lblcn" runat="server" Text='<%# Eval("FeedbacktNumber") %>'></asp:Label><asp:Label ID="lblm4" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="lnkcom" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("UseReciepeName") %>'></asp:Label><asp:Label ID="Label8" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="lnkremark" runat="server" CommandName="lnkremark" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="lblremark" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label><asp:Label ID="lblm5" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>

                                                                                    </tr>

                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>
                                                                    </table>
                                                                </div>

                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnsearch2" EventName="Click" />

                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <div class="tab-pane" id="overview_3">
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>

                                                                <div class="tools" style="padding-bottom: 10px;">

                                                                    <asp:TextBox ID="txtCustomer" runat="server" CssClass="input-circle" placeholder="Contact Number" Style="width: 60%; height: 34px; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc; box-shadow: none; transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s; -webkit-border-radius: 4px; -moz-border-radius: 4px; -ms-border-radius: 4px; -o-border-radius: 4px; border-radius: 4px;"></asp:TextBox>
                                                                    <asp:Button ID="btnSearchCus" class="btn btn-circle green btn-outline btn-sm" runat="server" Text="Search" OnClick="btnSearchCus_Click" />
                                                                </div>

                                                                <div class="table-responsive">
                                                                    <table class="table table-striped table-hover table-bordered">
                                                                        <thead>
                                                                            <tr>

                                                                                <th>Patient Name</th>
                                                                                <th>MRN No</th>
                                                                                <th>Contact</th>
                                                                                <th>Feedback No</th>
                                                                                <th>Total Time</th>
                                                                                <th>Feedback Details</th>

                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:ListView ID="listCustomer" runat="server" OnItemCommand="listCustomer_ItemCommand">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="lnkpn" runat="server" CommandName="lnkpn" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="lblpt" runat="server" Text='<%# Eval("Patient_Name") %>'></asp:Label><asp:Label ID="lblm1" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="lnkmrn" runat="server" CommandName="lnkmrn" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="lblmrn" runat="server" Text='<%# Eval("MRN") %>'></asp:Label><asp:Label ID="lblm2" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>
                                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkcontack" runat="server" CommandName="lnkcontack" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                <asp:Label ID="lblcontact" runat="server" Text='<%# Eval("Contact") %>'></asp:Label><asp:Label ID="lblm3" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkcom" runat="server" CommandName="lnkcom" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                <asp:Label ID="lblcn" runat="server" Text='<%# Eval("FeedbacktNumber") %>'></asp:Label><asp:Label ID="lblm4" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="lnkcom" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("UseReciepeName") %>'></asp:Label><asp:Label ID="Label11" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>
                                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkremark" runat="server" CommandName="lnkremark" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                <asp:Label ID="lblremark" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label><asp:Label ID="lblm5" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>

                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnSearchCus" EventName="Click" />

                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <div class="tab-pane active" id="overview_8">

                                                        <div class="tools" style="padding-bottom: 10px;">

                                                            <asp:TextBox ID="txtsearch" runat="server" CssClass="input-circle" Style="width: 60%; height: 34px; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc; box-shadow: none; transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s; -webkit-border-radius: 4px; -moz-border-radius: 4px; -ms-border-radius: 4px; -o-border-radius: 4px; border-radius: 4px;" placeholder="Patient Name"></asp:TextBox>


                                                            <asp:Button ID="btnSearch" class="btn btn-circle green btn-outline btn-sm" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                                            <%--<asp:Label ID="lblcount" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Large" Text=""></asp:Label>--%>
                                                        </div>

                                                        <div class="table-responsive">
                                                            <table class="table table-striped table-hover table-bordered">
                                                                <thead>
                                                                    <tr>

                                                                        <th>Patient Name</th>
                                                                        <th>MRN No</th>
                                                                        <th>Contact</th>
                                                                        <th>Feedback No</th>
                                                                        <th>Total Time</th>
                                                                        <th>Feedback Details</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="listMinimumAndMax" runat="server" OnItemCommand="listMinimumAndMax_ItemCommand">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:LinkButton ID="lnkpn" runat="server" CommandName="lnkpn" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                        <asp:Label ID="lblpt" runat="server" Text='<%# Eval("Patient_Name") %>'></asp:Label><asp:Label ID="lblm1" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="lnkmrn" runat="server" CommandName="lnkmrn" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                        <asp:Label ID="lblmrn" runat="server" Text='<%# Eval("MRN") %>'></asp:Label><asp:Label ID="lblm2" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkcontack" runat="server" CommandName="lnkcontack" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                <asp:Label ID="lblcontact" runat="server" Text='<%# Eval("Contact") %>'></asp:Label><asp:Label ID="lblm3" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                                </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkcom" runat="server" CommandName="lnkcom" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                <asp:Label ID="lblcn" runat="server" Text='<%# Eval("FeedbacktNumber") %>'></asp:Label><asp:Label ID="lblm4" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>

                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="lnkcom" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("UseReciepeName") %>'></asp:Label><asp:Label ID="Label13" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkremark" runat="server" CommandName="lnkremark" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                <asp:Label ID="lblremark" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label><asp:Label ID="lblm5" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                                </td>
                                                                            </tr>

                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>
                                                        </div>

                                                    </div>



                                                    <div class="tab-pane" id="overview_9">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="tools" style="padding-bottom: 10px;">

                                                                    <asp:TextBox ID="txtserchmrnno" runat="server" CssClass="input-circle" Style="width: 60%; height: 34px; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc; box-shadow: none; transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s; -webkit-border-radius: 4px; -moz-border-radius: 4px; -ms-border-radius: 4px; -o-border-radius: 4px; border-radius: 4px;" placeholder="MRN No"></asp:TextBox>


                                                                    <asp:Button ID="btnsearch1" class="btn btn-circle green btn-outline btn-sm" runat="server" Text="Search" OnClick="btnsearch1_Click" />
                                                                </div>

                                                                <div class="table-responsive">
                                                                    <table class="table table-striped table-hover table-bordered">
                                                                        <thead>
                                                                            <tr>

                                                                                <th>Patient Name</th>
                                                                                <th>MRN No</th>
                                                                                <th>Contact</th>
                                                                                <th>Feedback No</th>
                                                                                <th>Total Time</th>
                                                                                <th>Feedback Details</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="listMinimumAndMaxand" runat="server" OnItemCommand="listMinimumAndMaxand_ItemCommand">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="lnkpn" runat="server" CommandName="lnkpn" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="lblpt" runat="server" Text='<%# Eval("Patient_Name") %>'></asp:Label><asp:Label ID="lblm1" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="lnkmrn" runat="server" CommandName="lnkmrn" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="lblmrn" runat="server" Text='<%# Eval("MRN") %>'></asp:Label><asp:Label ID="lblm2" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>
                                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkcontack" runat="server" CommandName="lnkcontack" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                <asp:Label ID="lblcontact" runat="server" Text='<%# Eval("Contact") %>'></asp:Label><asp:Label ID="lblm3" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkcom" runat="server" CommandName="lnkcom" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                <asp:Label ID="lblcn" runat="server" Text='<%# Eval("FeedbacktNumber") %>'></asp:Label><asp:Label ID="lblm4" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="lnkcom" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="Label14" runat="server" Text='<%# Eval("UseReciepeName") %>'></asp:Label><asp:Label ID="Label15" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>
                                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkremark" runat="server" CommandName="lnkremark" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                <asp:Label ID="lblremark" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label><asp:Label ID="lblm5" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                                        </td>
                                                                                    </tr>

                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnsearch1" EventName="Click" />

                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <div class="tab-pane" id="overview_10">
                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="tools" style="padding-bottom: 10px;">

                                                                    <asp:TextBox ID="txtcommentno" runat="server" CssClass="input-circle" Style="width: 60%; height: 34px; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc; box-shadow: none; transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s; -webkit-border-radius: 4px; -moz-border-radius: 4px; -ms-border-radius: 4px; -o-border-radius: 4px; border-radius: 4px;" placeholder="Text From Description"></asp:TextBox>


                                                                    <asp:Button ID="btnseach3" class="btn btn-circle green btn-outline btn-sm" runat="server" Text="Search" OnClick="btnseach3_Click" />
                                                                </div>

                                                                <div class="table-responsive">
                                                                    <table class="table table-striped table-hover table-bordered">
                                                                        <thead>
                                                                            <tr>

                                                                                <th>Patient Name</th>
                                                                                <th>MRN No</th>
                                                                                <th>Contact</th>
                                                                                <th>Feedback No</th>
                                                                                <th>Total Time</th>
                                                                                <th>Feedback Details</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="listmaximumcomment" runat="server" OnItemCommand="listmaximumcomment_ItemCommand">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="lnkpn" runat="server" CommandName="lnkpn" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="lblpt" runat="server" Text='<%# Eval("Patient_Name") %>'></asp:Label><asp:Label ID="lblm1" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="lnkmrn" runat="server" CommandName="lnkmrn" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="lblmrn" runat="server" Text='<%# Eval("MRN") %>'></asp:Label><asp:Label ID="lblm2" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>
                                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkcontack" runat="server" CommandName="lnkcontack" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                <asp:Label ID="lblcontact" runat="server" Text='<%# Eval("Contact") %>'></asp:Label><asp:Label ID="lblm3" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkcom" runat="server" CommandName="lnkcom" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                <asp:Label ID="lblcn" runat="server" Text='<%# Eval("FeedbacktNumber") %>'></asp:Label><asp:Label ID="lblm4" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="LinkButton5" runat="server" CommandName="lnkcom" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                                <asp:Label ID="Label17" runat="server" Text='<%# Eval("UseReciepeName") %>'></asp:Label><asp:Label ID="Label18" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                            </asp:LinkButton>
                                                                                        </td>
                                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkremark" runat="server" CommandName="lnkremark" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                                                <asp:Label ID="lblremark" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label><asp:Label ID="lblm5" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                                        </td>
                                                                                    </tr>

                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnseach3" EventName="Click" />

                                                            </Triggers>
                                                        </asp:UpdatePanel>
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
                                        <input type="checkbox" class="make-switch" checked="checked" data-size="small" data-on-color="success" data-on-text="ON" data-off-color="default" data-off-text="OFF">
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
            </div>

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

            <div id="responsive" class="modal fade" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog" style="margin-top: 0px; margin-bottom: 0px;">
                    <div class="modal-content">
                        <div class="portlet box purple" style="margin-bottom: 0px;">
                            <div class="modal-header">

                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                <h4 class="modal-title" style="color: white;">Add New Customer</h4>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div class="scroller" style="height: 100px" data-always-visible="1" data-rail-visible1="1">
                                <div class="row">

                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <p>
                                                <strong style="font-size: larger; margin-left: 10px;">No. of Link<span>* </span></strong>
                                                <asp:DropDownList ID="DrpNolink" runat="server" CssClass="form-control input-medium">
                                                </asp:DropDownList>

                                            </p>
                                        </div>
                                        <div class="col-md-6">
                                            <p>
                                                <strong style="font-size: larger; margin-left: 10px;">Quick Link<span>* </span></strong>
                                                <asp:DropDownList ID="drpLink" runat="server" CssClass="form-control input-medium">
                                                </asp:DropDownList>
                                            </p>
                                        </div>
                                        <%--<div class="form-group">
                                    <div class="col-md-6">
                                        <h4 style="margin-left: 10px;">Some Input</h4>
                                        <asp:TextBox ID="TextBox7" runat="server" class="col-md-12 form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <h4 style="margin-left: 10px;">Some Input</h4>
                                        <asp:TextBox ID="TextBox8" runat="server" class="col-md-12 form-control"></asp:TextBox>
                                    </div>
                                </div>--%>
                                    </div>
                                </div>


                            </div>
                        </div>
                        <div class="modal-footer" style="background-color: lightgray;">
                            <button type="button" data-dismiss="modal" class="btn btn-sm red">Close</button>
                            <asp:Button ID="btnSaveCust" runat="server" class="btn btn-sm green" Text="Save" OnClick="btnSaveCust_Click" />
                        </div>
                    </div>
                </div>
            </div>
    </form>
    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
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
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/dashboard.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/layout4/scripts/layout.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/layout4/scripts/demo.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $('#clickmewow').click(function () {
                $('#radio1003').attr('checked', 'checked');
            });
        })
    </script>
</body>
</html>
