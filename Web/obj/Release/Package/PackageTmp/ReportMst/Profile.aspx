<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Web.ReportMst.Profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<!-- 
Template Name: Metronic - Responsive Admin Dashboard Template build with Twitter Bootstrap 3.3.7
Version: 4.7.5
Author: KeenThemes
Website: http://www.keenthemes.com/
Contact: support@keenthemes.com
Follow: www.twitter.com/keenthemes
Dribbble: www.dribbble.com/keenthemes
Like: www.facebook.com/keenthemes
Purchase: http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes
Renew Support: http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes
License: You must have a valid license purchased only from themeforest(the above link) in order to legally use the theme for your project.
-->
<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en">
<!--<![endif]-->
<!-- BEGIN HEAD -->


<!-- Mirrored from localhost:64878/theme/admin_4/ayoprofile.html by HTTrack Website Copier/3.x [XR&CO'2014], Wed, 20 Sep 2017 05:53:41 GMT -->
<head runat="server">
    <meta charset="utf-8" />
    <title>Customer Profile</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="Preview page of Metronic Admin Theme #4 for user profile sample" name="description" />
    <meta content="" name="author" />

    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&amp;subset=all" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link href="assets/pages/css/profile-2.min.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="assets/layouts/layout4/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/layouts/layout4/css/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="assets/layouts/layout4/css/custom.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/css/plugins.css" rel="stylesheet" />
    <link href="assets/global/plugins/jquery-tags-input/jquery.tagsinput.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.css" rel="stylesheet" />
    <link href="../assets/global/plugins/jquery-tags-input/jquery.tagsinput.css" rel="stylesheet" />
    <!-- END THEME LAYOUT STYLES -->
    <link rel="shortcut icon" href="favicon.html" />
    <%-- Dipak --%>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDd-0GRWdGVwGD-nc5Jojdnr6aQO46RV_4&sensor=false&libraries=places"></script>
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
    <style>
        .saletab {
            padding: 10px 0;
            overflow: hidden;
            border-top: none;
        }
    </style>


    <%-- Dipak --%>
</head>
<!-- END HEAD -->

<body class="page-container-bg-solid page-header-fixed page-sidebar-closed-hide-logo">
    <!-- BEGIN HEADER -->
    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>

            <!-- BEGIN PAGE BASE CONTENT -->
            <div class="profile">
                <div class="tabbable-line tabbable-full-width">
                    <ul class="nav nav-tabs">
                       <%-- <li class="active">
                            <a href="#tab_1_1" data-toggle="tab">Overview </a>
                        </li>--%>
                        <li class="active">
                            <a href="#tab_1_3" data-toggle="tab">Account </a>
                        </li>
                        <li>
                            <a href="#tab_1_6" data-toggle="tab">Help </a>
                        </li>
                        <li style="float: right;">
                            <a href="hbclogin.aspx">
                                <asp:Label ID="lbllogout" runat="server" Font-Size="Large" Font-Bold="true" CssClass="label label-danger label-sm" Text="LogOut"></asp:Label>
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane" id="tab_1_1">
                            <div class="row">
                                <div class="col-md-3">
                                    <ul class="list-unstyled profile-nav">
                                        <li>
                                            <asp:Image ID="IMGAvtar" class="img-responsive pic-bordered" ImageUrl="../CRM/images/No_image.png" runat="server" />
                                            <%--<img src="assets/pages/media/profile/people19.png" class="img-responsive pic-bordered" alt="" />--%>
                                            <a href="javascript:;" class="profile-edit">edit </a>
                                        </li>
                                        <li>
                                            <a href="javascript:;">Subscription </a>
                                        </li>
                                        <%-- <li>
                                            <a href="javascript:;">Projects </a>
                                        </li>--%>
                                        <li>
                                            <a href="javascript:;">Messages
                                                       
                                                        <span>3 </span>
                                            </a>
                                        </li>
                                        <%-- <li>
                                            <a href="javascript:;">Friends </a>
                                        </li>--%>
                                        <li>
                                            <a href="javascript:;">Settings </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-9">
                                    <div class="row">
                                        <div class="col-md-8 profile-info">
                                            <h1 class="font-green sbold uppercase"><%--John Doe--%>
                                                <asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
                                            </h1>
                                            <%--<p>
                                                    Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt laoreet dolore magna aliquam tincidunt erat volutpat laoreet dolore magna aliquam tincidunt erat volutpat.
                                                       
                                                </p>--%>
                                            <p>
                                                <a href="javascript:;"><%--www.mywebsite.com --%>
                                                    <asp:Label ID="lblmyWebside" runat="server" Text=""></asp:Label>
                                                </a>
                                            </p>
                                            <ul class="list-inline">
                                                <li>
                                                    <i class="fa fa-map-marker"></i><%--Spain --%>
                                                    <asp:Label ID="lbllocation" runat="server" Text=""></asp:Label>
                                                </li>
                                                <li>
                                                    <i class="fa fa-calendar"></i><%--18 Jan 1982 --%>
                                                    <asp:Label ID="lblBirthDate" runat="server" Text=""></asp:Label>
                                                </li>
                                                <li>
                                                    <i class="fa fa-times"></i>
                                                    <asp:Label ID="lblNdate" runat="server" Text=""></asp:Label>
                                                </li>
                                                <%-- <li>
                                                    <i class="fa fa-briefcase"></i>Design </li>
                                                <li>
                                                    <i class="fa fa-star"></i>Top Seller </li>
                                                <li>
                                                    <i class="fa fa-heart"></i>BASE Jumping </li>--%>
                                                <li>
                                                    <i class="fa fa-play"></i>
                                                    <asp:Label ID="lblplan" runat="server" Text=""></asp:Label>
                                                </li>
                                            </ul>
                                        </div>
                                        <!--end col-md-8-->
                                    </div>
                                    <!--end row-->
                                    <div class="row">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <div class="col-md-8">
                                                    <div class="portlet sale-summary">
                                                        <div class="portlet-title">
                                                            <div class="caption font-yellow-saffron sbold">Personal Record </div>

                                                        </div>
                                                        <div class="portlet-body">
                                                            <ul class="list-unstyled">
                                                                <li style="padding: 5px 0;">
                                                                    <%--<span>Height: </span>--%>
                                                                    <asp:Label ID="Label3" CssClass="col-md-3 control-label" runat="server" Text="Height"></asp:Label>
                                                                    <asp:TextBox ID="txtHeight" Style="width: 50%;" placeholder="Feet,CM" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </li>
                                                                <li style="border-top: solid 1px #fff; padding: 5px 0;">
                                                                    <%--<span>Agreed Cabs: </span>--%>
                                                                    <asp:Label ID="Label4" CssClass="col-md-3 control-label" runat="server" Text="Agreed Cabs"></asp:Label>
                                                                    <asp:TextBox ID="txtAgreedCabs" Style="width: 50%;" placeholder="7,2" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </li>
                                                                <li style="border-top: solid 1px #fff; padding: 5px 0;">
                                                                    <%--<span>Nationality: </span>--%>
                                                                    <asp:Label ID="Label18" CssClass="col-md-3 control-label" runat="server" Text="Nationality"></asp:Label>
                                                                    <asp:DropDownList ID="drpNationality" class="select2me form-control input-medium" Font-Size="Small" runat="server"></asp:DropDownList>
                                                                </li>
                                                                <li>
                                                                    <asp:Button ID="btnperrecord" CssClass="btn btn-sm yellow" Font-Bold="true" Font-Size="Medium" runat="server" Text="Update Personal Record" OnClick="btnperrecord_Click" />
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="col-md-4">
                                            <div class="portlet sale-summary">
                                                <div class="portlet-title">
                                                    <div class="caption font-red sbold">Plan Summary </div>
                                                    <div class="tools">
                                                        <a class="reload" href="javascript:;"></a>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                    <ul class="list-unstyled">
                                                        <li>
                                                            <span>Plan                                                                       
                                                                        <i class="fa fa-img-up"></i>
                                                            </span>
                                                            <asp:DropDownList ID="DrpPlan" class="select2me sale-num form-control input-small" Font-Size="Small" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrpPlan_SelectedIndexChanged"></asp:DropDownList>
                                                            <%--<span class="sale-num">23 </span>--%>
                                                        </li>
                                                        <li>
                                                            <span>Total Delivery <%--WEEKLY SALES class="sale-info"--%>
                                                                <i class="fa fa-img-down"></i>
                                                            </span>
                                                            <asp:Label ID="lblTotalDelivery" CssClass="sale-num" runat="server" Text=""></asp:Label>
                                                            <%--<span class="sale-num">87 </span>--%>
                                                        </li>
                                                        <li>
                                                            <span>Delivered </span>
                                                            <asp:Label ID="lblDelivered" CssClass="sale-num" runat="server" Text=""></asp:Label>
                                                            <%--<span class="sale-num">2377 </span>--%>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end col-md-4-->
                                    </div>
                                    <!--end row-->
                                    <div class="tabbable-line tabbable-custom-profile">
                                        <ul class="nav nav-tabs">
                                            <li>
                                                <a href="#tab_1_44" data-toggle="tab">Personal Food Choices </a>
                                            </li>
                                            <li class="active">
                                                <a href="#tab_1_33" data-toggle="tab">Delivery Address  </a>
                                            </li>
                                            <li>
                                                <a href="#tab_1_11" data-toggle="tab">Plan Delivery Status </a><%--Latest Customers--%>
                                            </li>
                                            <li>
                                                <a href="#tab_1_22" data-toggle="tab">Food Plan </a>
                                            </li>
                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane" id="tab_1_44">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <%-- Allergies --%>
                                                        <div class="portlet box blue">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="fa fa-gift"></i>
                                                                    Alergie
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"></a>
                                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                    <%--<asp:LinkButton ID="LinkButton5" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>--%>
                                                                    <a href="javascript:;" class="remove"></a>
                                                                </div>

                                                            </div>
                                                            <div class="portlet-body">
                                                                <asp:Panel ID="Panel4" runat="server" DefaultButton="btnAllergy">
                                                                    <div class="row" style="margin-top: 10px;">
                                                                        <div class="col-md-5">
                                                                            <div class="form-group">
                                                                                <asp:Label runat="server" ID="Label7" class="col-md-4 control-label" Text="Product"></asp:Label>
                                                                                <div class="col-md-8">
                                                                                    <asp:DropDownList ID="drpAllergy" class="select2me table-group-action-input form-control input-medium" ForeColor="Blue" runat="server"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="drpAllergy" ErrorMessage="Item of Product Required." CssClass="Validation" ValidationGroup="Allergy" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <%-- </div>
                                                        <div class="row">--%>

                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <asp:Label runat="server" ID="Label9" class="col-md-3 control-label" Text="Remarks "></asp:Label>

                                                                                <div class="col-md-6">
                                                                                    <asp:TextBox ID="txtremak" CssClass="form-control" ForeColor="Blue" runat="server"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtremak" ErrorMessage="Remark Required." CssClass="Validation" ValidationGroup="Allergy"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <asp:Button ID="btnAllergy" CssClass="btn bg-red-pink" runat="server" Text="Add Allergy" ValidationGroup="Allergy" OnClick="btnAllergy_Click" />
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </asp:Panel>

                                                                <div class="table-scrollable">

                                                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                                        <thead>
                                                                            <tr>
                                                                                <td style="width: 5%;">
                                                                                    <asp:Label ID="lbla" runat="server" Text="#"></asp:Label></td>
                                                                                <th style="width: 35%;">
                                                                                    <asp:Label runat="server" ID="lblIhtemCode" Text="ItemCode"></asp:Label></th>
                                                                                <th style="width: 40%;">
                                                                                    <asp:Label runat="server" ID="lbliRemark" Text="Remarks"></asp:Label></th>

                                                                                <th style="width: 20%;">ACTION</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:ListView ID="Allergy" runat="server" OnItemCommand="Allergy_ItemCommand">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <%#Container.DataItemIndex+1 %>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblIhtemCode" runat="server" Text='<%# nameAllergy(Convert.ToInt32(Eval("ItemCode"))) %>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lbliRemark" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label></td>

                                                                                        <td>
                                                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:LinkButton ID="btnEditAlergie" CommandName="btnEdit" CommandArgument='<%# Eval("customerID")+","+Eval("myID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>

                                                                                                    &nbsp;

                                                             <asp:LinkButton ID="btnDeleteAlergie" runat="server" class="btn btn-sm red filter-cancel" CommandArgument='<%#  Eval("customerID")+","+Eval("myID")%>' CommandName="btnDelete" OnClientClick="return ConfirmMsg();"><i class="fa fa-times"></i></asp:LinkButton>
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:AsyncPostBackTrigger ControlID="btnEditAlergie" EventName="Click" />
                                                                                                    <asp:AsyncPostBackTrigger ControlID="btnDeleteAlergie" EventName="Click" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>

                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </tbody>
                                                                    </table>


                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%-- Allergies End --%>
                                                        <%-- Complain --%>
                                                        <div class="portlet box blue">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="fa fa-gift"></i>
                                                                    Complain
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"></a>
                                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                    <%--<asp:LinkButton ID="LinkButton4" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>--%>
                                                                    <a href="javascript:;" class="remove"></a>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <asp:Panel ID="Panel6" runat="server" DefaultButton="btncomplain">

                                                                    <div class="row" style="margin-top: 10px;">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <asp:Label runat="server" ID="Label12" class="col-md-4 control-label" Text="Complain"></asp:Label>
                                                                                <div class="col-md-8">
                                                                                    <asp:DropDownList ID="drpcomplinid" class="select2me table-group-action-input form-control input-medium" ForeColor="Blue" runat="server"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="drpcomplinid" ErrorMessage="Item of Product Required." CssClass="Validation" ValidationGroup="Complain" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <asp:Label runat="server" ID="Label16" class="col-md-4 control-label" Text="Complain Date"></asp:Label>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtComplainDate" runat="server" CssClass="form-control" placeholder="Complain Date"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtComplainDate" ErrorMessage="Date Required." CssClass="Validation" ValidationGroup="Complain"></asp:RequiredFieldValidator>
                                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtComplainDate" Format="MM-dd-yyyy" Enabled="True">
                                                                                    </cc1:CalendarExtender>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row" style="margin-top: 10px;">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <asp:Label runat="server" ID="Label13" class="col-md-4 control-label" Text="Remarks "></asp:Label>

                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtrema" CssClass="form-control" placeholder="Remark" ForeColor="Blue" runat="server"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtrema" ErrorMessage="Remark Required." CssClass="Validation" ValidationGroup="Complain"></asp:RequiredFieldValidator>
                                                                                </div>

                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-6">
                                                                            <asp:Button ID="btncomplain" CssClass="btn blue-madison" runat="server" Text="Add Complain" ValidationGroup="Complain" OnClick="btncomplain_Click" />
                                                                        </div>


                                                                    </div>

                                                                </asp:Panel>
                                                                <div class="table-scrollable">

                                                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                                        <thead>
                                                                            <tr>
                                                                                <td style="width: 5%;">
                                                                                    <asp:Label ID="Label2" runat="server" Text="#"></asp:Label></td>
                                                                                <th style="width: 25%;">
                                                                                    <asp:Label runat="server" ID="lblcomplinid" Text="complinid"></asp:Label></th>
                                                                                <th style="width: 20%;">
                                                                                    <asp:Label runat="server" ID="Label14" Text="Complain Date"></asp:Label>
                                                                                </th>
                                                                                <th style="width: 30%;">
                                                                                    <asp:Label runat="server" ID="lblRemarks" Text="Remarks"></asp:Label></th>
                                                                                <th style="width: 20%;">ACTION</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:ListView ID="CustomerComplainID" runat="server" OnItemCommand="CustomerComplainID_ItemCommand">

                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <%#Container.DataItemIndex+1 %>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblcomplinid" runat="server" Text='<%# nameComplian(Convert.ToInt32(Eval("complinid"))) %>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label15" runat="server" Text='<%# Eval("DateAdded")%>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblRemak" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:LinkButton ID="btnEditComplain" CommandName="btnEdit" CommandArgument='<%# Eval("customerID")+","+Eval("myID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>

                                                                                                    &nbsp;

                                                             <asp:LinkButton ID="btnDeleteComplain" runat="server" class="btn btn-sm red filter-cancel" CommandArgument='<%#  Eval("customerID")+","+Eval("myID")%>' CommandName="btnDelete" OnClientClick="return ConfirmMsg();"><i class="fa fa-times"></i></asp:LinkButton>
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:AsyncPostBackTrigger ControlID="btnEditComplain" EventName="Click" />
                                                                                                    <asp:AsyncPostBackTrigger ControlID="btnDeleteComplain" EventName="Click" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </tbody>
                                                                    </table>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%-- Complain End --%>
                                                        <%-- Weight  --%>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-horizontal form-row-seperated">
                                                                    <div class="portlet box blue">
                                                                        <div class="portlet-title">
                                                                            <div class="caption">
                                                                                <i class="fa fa-gift"></i>

                                                                                Weight Measured
                                                                            </div>
                                                                            <div class="tools">
                                                                                <a href="javascript:;" class="collapse"></a>
                                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                <%--<asp:LinkButton ID="LinkButton1" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>--%>
                                                                                <a href="javascript:;" class="remove"></a>
                                                                            </div>
                                                                        </div>
                                                                        <div class="portlet-body">

                                                                            <div class="table-scrollable">
                                                                                <asp:Panel ID="Panel2" runat="server" DefaultButton="btnweight">
                                                                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">

                                                                                        <tr>
                                                                                            <td style="width: 5%;">
                                                                                                <asp:Label ID="Label20" runat="server" Text="#"></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 25%;">
                                                                                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control input-medium" placeholder="Insert Date"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="DateValidde" runat="server" ControlToValidate="txtdate" ErrorMessage="Date Required" CssClass="Validation" ValidationGroup="Weight"></asp:RequiredFieldValidator>
                                                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate" Format="MM-dd-yyyy" Enabled="True">
                                                                                                </cc1:CalendarExtender>
                                                                                            </td>
                                                                                            <td style="width: 25%;">
                                                                                                <asp:TextBox ID="txtwight" runat="server" CssClass="form-control input-medium" placeholder="Insert Weight"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="weightvalide" runat="server" ControlToValidate="txtwight" ErrorMessage="Weight Required" CssClass="Validation" ValidationGroup="Weight"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%;">
                                                                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control input-medium" placeholder="Insert Remark"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="RemarkValide" runat="server" ControlToValidate="txtRemarks" ErrorMessage="Remarks Required" CssClass="Validation" ValidationGroup="Weight"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td style="width: 20%;">
                                                                                                <div class="actions btn-set">
                                                                                                    <asp:Button ID="btnweight" CssClass="btn red" runat="server" Text="Add Weight" ValidationGroup="Weight" OnClick="btnweight_Click" />
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </asp:Panel>
                                                                                <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                                                    <thead>
                                                                                        <tr>

                                                                                            <td style="width: 5%;">
                                                                                                <asp:Label ID="lblh" runat="server" Text="#"></asp:Label></td>
                                                                                            <td style="width: 25%;">
                                                                                                <asp:Label ID="lblhDateMeasure" runat="server" Text="DateMeasure"></asp:Label></td>
                                                                                            <td style="width: 25%;">
                                                                                                <asp:Label ID="lblhWeightMeasuredinKG" runat="server" Text="Weight Measured in KG"></asp:Label></td>
                                                                                            <td style="width: 25%;">
                                                                                                <asp:Label ID="lblaRemarks" runat="server" Text="Remarks"></asp:Label></td>
                                                                                            <td style="width: 20%;">ACTION</td>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:ListView ID="WeightMeasured" runat="server" OnItemCommand="WeightMeasured_ItemCommand">

                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <%#Container.DataItemIndex+1 %>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblhDateMeasure" runat="server" Text='<%# Eval("DateAdded")%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblhWeightMeasuredinKG" runat="server" Text='<%# Eval("Weight")%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblaRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                                                            <ContentTemplate>
                                                                                                                <asp:LinkButton ID="btnEditWeight" CommandName="btnEdit" CommandArgument='<%# Eval("customerID")+","+Eval("myID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                                                                &nbsp;
                                                                                                                <asp:LinkButton ID="btnDeleteWeight" runat="server" class="btn btn-sm red filter-cancel" CommandArgument='<%#  Eval("customerID")+","+Eval("myID")%>' CommandName="btnDelete" OnClientClick="return ConfirmMsg();"><i class="fa fa-times"></i></asp:LinkButton>
                                                                                                            </ContentTemplate>
                                                                                                            <Triggers>
                                                                                                                <asp:AsyncPostBackTrigger ControlID="btnEditWeight" EventName="Click" />
                                                                                                                <asp:AsyncPostBackTrigger ControlID="btnDeleteWeight" EventName="Click" />
                                                                                                            </Triggers>
                                                                                                        </asp:UpdatePanel>

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
                                                        <%-- End Weight --%>
                                                        <%-- DisLike --%>
                                                        <div class="portlet box blue">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="fa fa-gift"></i>
                                                                    DisLike
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"></a>
                                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                    <%--<asp:LinkButton ID="LinkButton3" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>--%>
                                                                    <a href="javascript:;" class="remove"></a>
                                                                </div>

                                                            </div>
                                                            <div class="portlet-body">
                                                                <asp:Panel ID="Panel5" runat="server" DefaultButton="btnDislike">
                                                                    <div class="row" style="margin-top: 10px;">
                                                                        <div class="col-md-5">
                                                                            <div class="form-group">
                                                                                <asp:Label runat="server" ID="Label10" class="col-md-4 control-label" Text="Product"></asp:Label>
                                                                                <div class="col-md-8">
                                                                                    <asp:DropDownList ID="drpitems" class="select2me table-group-action-input form-control input-medium" ForeColor="Blue" runat="server"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="drpitems" ErrorMessage="Item of Product Required." CssClass="Validation" ValidationGroup="DisLike" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <asp:Label runat="server" ID="Label11" class="col-md-3 control-label" Text="Remarks "></asp:Label>

                                                                                <div class="col-md-6">
                                                                                    <asp:TextBox ID="txtrem" CssClass="form-control" ForeColor="Blue" runat="server"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtrem" ErrorMessage="Remark Required." CssClass="Validation" ValidationGroup="DisLike"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <asp:Button ID="btnDislike" CssClass="btn green" runat="server" Text="Add DisLike" ValidationGroup="DisLike" OnClick="btnDislike_Click" />
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </asp:Panel>

                                                                <div class="table-scrollable">

                                                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                                        <thead>
                                                                            <tr>
                                                                                <td style="width: 5%;">
                                                                                    <asp:Label ID="Label1" runat="server" Text="#"></asp:Label></td>
                                                                                <th style="width: 35%;">
                                                                                    <asp:Label runat="server" ID="lblItemCode" Text="ItemCode"></asp:Label></th>
                                                                                <th style="width: 40%;">
                                                                                    <asp:Label runat="server" ID="lblhRemark" Text="Remarks"></asp:Label></th>

                                                                                <th style="width: 20%;">ACTION</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:ListView ID="CustomerDisLike" runat="server" OnItemCommand="CustomerDisLike_ItemCommand">

                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <%#Container.DataItemIndex+1 %>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# nameItems(Convert.ToInt32(Eval("ItemCode")))%>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblRemak" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:LinkButton ID="btnEditDisLike" CommandName="btnEdit" CommandArgument='<%# Eval("customerID")+","+Eval("myID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>

                                                                                                    &nbsp;

                                                                                            <asp:LinkButton ID="btnDeleteDisLike" runat="server" class="btn btn-sm red filter-cancel" CommandArgument='<%#  Eval("customerID")+","+Eval("myID")%>' CommandName="btnDelete" OnClientClick="return ConfirmMsg();"><i class="fa fa-times"></i></asp:LinkButton>
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:AsyncPostBackTrigger ControlID="btnEditDisLike" EventName="Click" />
                                                                                                    <asp:AsyncPostBackTrigger ControlID="btnDeleteDisLike" EventName="Click" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%-- DisLike End --%>
                                                        <%-- Like --%>
                                                        <div class="portlet box blue">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="fa fa-gift"></i>
                                                                    Like
                                                                </div>
                                                                <div class="caption" style="margin-left: 45px;">
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"></a>
                                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                    <%--<asp:LinkButton ID="LinkButton2" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>--%>
                                                                    <a href="javascript:;" class="remove"></a>
                                                                </div>

                                                            </div>
                                                            <div class="portlet-body">

                                                                <div class="tabbable">
                                                                    <div class="tab-content no-space">
                                                                        <asp:Panel ID="Panel3" runat="server" DefaultButton="btnlike">
                                                                            <div class="row" style="margin-top: 10px;">
                                                                                <div class="col-md-5">
                                                                                    <div class="form-group">
                                                                                        <asp:Label runat="server" ID="Label6" class="col-md-4 control-label" Text="Product"></asp:Label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="drpItem" class="select2me table-group-action-input form-control input-medium" ForeColor="Blue" runat="server"></asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="likeProductvalide" runat="server" ControlToValidate="drpItem" ErrorMessage="Item of Product Item Required." CssClass="Validation" InitialValue="0" ValidationGroup="Like"></asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <%-- </div>
                                                        <div class="row">--%>

                                                                                <div class="col-md-6">
                                                                                    <div class="form-group">
                                                                                        <asp:Label runat="server" ID="Label8" class="col-md-3 control-label" Text="Remarks "></asp:Label>

                                                                                        <div class="col-md-6">
                                                                                            <asp:TextBox ID="txtremark" CssClass="form-control" ForeColor="Blue" runat="server"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtremark" ErrorMessage="Remark Required." CssClass="Validation" ValidationGroup="Like"></asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                        <div class="col-md-2">
                                                                                            <asp:Button ID="btnlike" CssClass="btn yellow" runat="server" Text="Add Like" ValidationGroup="Like" OnClick="btnlike_Click" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </div>


                                                                <div class="table-scrollable">
                                                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                                        <thead>
                                                                            <tr>
                                                                                <td style="width: 5%;">
                                                                                    <asp:Label ID="lbl" runat="server" Text="#"></asp:Label></td>
                                                                                <th style="width: 35%;">
                                                                                    <asp:Label runat="server" ID="lblhItemCode" Text="ItemCode"></asp:Label></th>
                                                                                <th style="width: 40%;">
                                                                                    <asp:Label runat="server" ID="lblhRemarks" Text="Remarks"></asp:Label></th>
                                                                                <th style="width: 20%;">ACTION</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:ListView ID="CustomerLike" runat="server" OnItemCommand="CustomerLike_ItemCommand">

                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td style="align-content: center">
                                                                                            <%#Container.DataItemIndex+1 %>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# nameItem(Convert.ToInt32(Eval("ItemCode"))) %>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblRemak" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" style="width: 100px;">
                                                                                                <ContentTemplate>
                                                                                                    <asp:LinkButton ID="btnEditLike" CommandName="btnEdit" CommandArgument='<%# Eval("customerID")+","+Eval("myID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>

                                                                                                    &nbsp;

                                                             <asp:LinkButton ID="btnDeleteLike" runat="server" class="btn btn-sm red filter-cancel" CommandArgument='<%#  Eval("customerID")+","+Eval("myID")%>' CommandName="btnDelete" OnClientClick="return ConfirmMsg();"><i class="fa fa-times"></i></asp:LinkButton>


                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:AsyncPostBackTrigger ControlID="btnEditLike" EventName="Click" />
                                                                                                    <asp:AsyncPostBackTrigger ControlID="btnDeleteLike" EventName="Click" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>

                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%-- Like end --%>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="tab-pane active" id="tab_1_33">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="portlet sale-summary">
                                                            <div class="portlet-title">
                                                                <div class="caption font-purple sbold">Delivery Address To Be Used</div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <ul class="list-unstyled">
                                                                    <li>
                                                                        <div class="col-md-4">
                                                                            <asp:DropDownList ID="DropDownList1" class="select2me form-control input-medium" runat="server">
                                                                                <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-md-8">
                                                                            <asp:Button ID="Button1" CssClass="btn purple dz-square" runat="server" Text="Edit" />
                                                                            <asp:Button ID="Button2" CssClass="btn yellow-saffron dz-square" runat="server" Text="Set Primary Address" />
                                                                            <asp:Button ID="Button3" CssClass="btn yellow-gold dz-square" runat="server" Text="Add New Address" />
                                                                        </div>

                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <%--<div id="dvMap" style="height: 300px">
                                                    </div>--%>
                                                    <div id="map123" style="height: 300px">
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="tab-pane" id="tab_1_11" runat="server">
                                                <%--<div class="tab-pane active" id="tab_1_1_2">--%>
                                                <div class="portlet-body">
                                                    <%--<table class="table table-striped table-bordered table-advance table-hover">--%>
                                                    <table class="table table-striped table-bordered table-hover table-advance" id="sample_1">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <i class="fa fa-briefcase"></i>Expected Delivery Date </th>
                                                                <th class="hidden-xs">
                                                                    <i class="fa fa-question"></i>Meal </th>
                                                                <th>
                                                                    <i class="fa fa-bookmark"></i>Delivery </th>
                                                                <th>Product</th>
                                                                <th>Status</th>
                                                                <th>Comments</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblexpDatee" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpectedDelDate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                                            <asp:Label ID="lblMytranssID" Visible="false" runat="server" Text='<%# Eval("MYTRANSID") %>'></asp:Label>
                                                                            <asp:Label ID="lbldeliveryyID" Visible="false" runat="server" Text='<%# Eval("DeliveryID") %>'></asp:Label>
                                                                        </td>
                                                                        <td class="hidden-xs">
                                                                            <asp:Label ID="lblDeliverymeall" runat="server" Text='<%# GetMeal(Convert.ToInt32(Eval("DeliveryMeal"))) %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbldeliverytimee" runat="server" Text='<%# Deliverytime(Convert.ToInt32(Eval("DeliveryTime"))) %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblprodd" runat="server" Text='<%# Eval("ProdName1") %>'></asp:Label></td>
                                                                        <td style="text-align: center;"><%--<span class="label label-warning label-sm">Pending </span>--%>
                                                                            <asp:Label ID="lblstatuss" Font-Size="Medium" CssClass="label label-primary label-sm" runat="server" Text="Pending"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: center;">
                                                                            <a class="btn btn-sm grey-salsa btn-outline" href="javascript:;">View </a>
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                            <%-- <tr>
                                                                <td>
                                                                    <a href="javascript:;">Pixel Ltd </a>
                                                                </td>
                                                                <td class="hidden-xs">Server hardware purchase </td>
                                                                <td>52560.10$
                                                                           
                                                                            <span class="label label-success label-sm">Paid </span>
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                    <a class="btn btn-sm grey-salsa btn-outline" href="javascript:;">View </a>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <a href="javascript:;">Smart House </a>
                                                                </td>
                                                                <td class="hidden-xs">Office furniture purchase </td>
                                                                <td>5760.00$
                                                                           
                                                                            <span class="label label-warning label-sm">Pending </span>
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                    <a class="btn btn-sm grey-salsa btn-outline" href="javascript:;">View </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <a href="javascript:;">FoodMaster Ltd </a>
                                                                </td>
                                                                <td class="hidden-xs">Company Anual Dinner Catering </td>
                                                                <td>12400.00$
                                                                           
                                                                            <span class="label label-success label-sm">Paid </span>
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                    <a class="btn btn-sm grey-salsa btn-outline" href="javascript:;">View </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <a href="javascript:;">WaterPure Ltd </a>
                                                                </td>
                                                                <td class="hidden-xs">Payment for Jan 2013 </td>
                                                                <td>610.50$
                                                                           
                                                                            <span class="label label-danger label-sm">Overdue </span>
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                    <a class="btn btn-sm grey-salsa btn-outline" href="javascript:;">View </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <a href="javascript:;">Pixel Ltd </a>
                                                                </td>
                                                                <td class="hidden-xs">Server hardware purchase </td>
                                                                <td>52560.10$
                                                                           
                                                                            <span class="label label-success label-sm">Paid </span>
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                    <a class="btn btn-sm grey-salsa btn-outline" href="javascript:;">View </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <a href="javascript:;">Smart House </a>
                                                                </td>
                                                                <td class="hidden-xs">Office furniture purchase </td>
                                                                <td>5760.00$
                                                                           
                                                                            <span class="label label-warning label-sm">Pending </span>
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                    <a class="btn btn-sm grey-salsa btn-outline" href="javascript:;">View </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <a href="javascript:;">FoodMaster Ltd </a>
                                                                </td>
                                                                <td class="hidden-xs">Company Anual Dinner Catering </td>
                                                                <td>12400.00$
                                                                           
                                                                            <span class="label label-success label-sm">Paid </span>
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                    <a class="btn btn-sm grey-salsa btn-outline" href="javascript:;">View </a>
                                                                </td>
                                                            </tr>--%>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <%--</div>--%>
                                            </div>
                                            <!--tab-pane-->
                                            <div class="tab-pane" id="tab_1_22">
                                                <%--<div class="tab-pane active" id="tab_1_1_1">--%>
                                                <div class="scroller" data-height="290px" data-always-visible="1" data-rail-visible1="1">
                                                    <ul class="feeds">
                                                        <li>
                                                            <div class="col1">
                                                                <div class="cont">
                                                                    <div class="cont-col1">
                                                                        <div class="label label-success">
                                                                            <i class="fa fa-bell-o"></i>
                                                                        </div>
                                                                    </div>
                                                                    <div class="cont-col2">
                                                                        <div class="desc">
                                                                            You have 4 pending tasks.
                                                                                       
                                                                                        <span class="label label-danger label-sm">Take action
                                                                                           
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
                                                                            <div class="label label-success">
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
                                                                        <div class="label label-danger">
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
                                                                        <div class="label label-info">
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
                                                                        <div class="label label-success">
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
                                                                        <div class="label label-warning">
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
                                                                        <div class="label label-success">
                                                                            <i class="fa fa-bell-o"></i>
                                                                        </div>
                                                                    </div>
                                                                    <div class="cont-col2">
                                                                        <div class="desc">
                                                                            Web server hardware needs to be upgraded.
                                                                                       
                                                                                        <span class="label label-inverse label-sm">Overdue </span>
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
                                                                        <div class="label label-default">
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
                                                                        <div class="label label-warning">
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
                                                                        <div class="label label-info">
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
                                                                        <div class="label label-default">
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
                                                                        <div class="label label-info">
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
                                                                        <div class="label label-default">
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
                                                                        <div class="label label-info">
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
                                                                        <div class="label label-default">
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
                                                                        <div class="label label-info">
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
                                                                        <div class="label label-default">
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
                                                                        <div class="label label-info">
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
                                                <%--</div>--%>
                                            </div>
                                            <!--tab-pane-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--tab_1_2-->
                        <div class="tab-pane active" id="tab_1_3">
                            <div class="row profile-account">
                                <div class="col-md-3">
                                    <ul class="ver-inline-menu tabbable margin-bottom-10">
                                        <li class="active">
                                            <a data-toggle="tab" href="#tab_1-1">
                                                <i class="fa fa-cog"></i>Personal info </a>
                                            <span class="after"></span>
                                        </li>
                                        <li>
                                            <a data-toggle="tab" href="#tab_2-2">
                                                <i class="fa fa-picture-o"></i>Change Avatar </a>
                                        </li>
                                        <li>
                                            <a data-toggle="tab" href="#tab_3-3">
                                                <i class="fa fa-lock"></i>Change Password </a>
                                        </li>

                                        <li>
                                            <a data-toggle="tab" href="#tab_4-4">
                                                <i class="fa fa-eye"></i>Privacity Settings </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-9">
                                    <div class="tab-content">
                                        <div id="tab_1-1" class="tab-pane active">
                                            <%--<form role="form" action="#">--%>
                                            <div class="form-group">
                                                <label class="control-label">First Name</label>
                                                <asp:TextBox ID="txtfirstname" runat="server" placeholder="John" class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUOMNAME1" runat="server" ControlToValidate="txtfirstname" ErrorMessage="First Name Required." ForeColor="Red" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <%--<input type="text" placeholder="John" class="form-control" />--%>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label">Last Name</label>
                                                <asp:TextBox ID="txtlastname" runat="server" placeholder="Doe" class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtlastname" ErrorMessage="Last Name Required." ForeColor="Red" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <%--<input type="text" placeholder="Doe" class="form-control" />--%>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label">Mobile Number</label>
                                                <asp:TextBox ID="txtmobile" runat="server" placeholder="+1 646 580 DEMO (6284)" CssClass="form-control tags"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtmobile" ErrorMessage="Mobile Number Required." ForeColor="Red" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtmobile" ValidChars="0123456789+," FilterType="Custom, numbers" runat="server" />
                                                <%-- <input type="text" placeholder="+1 646 580 DEMO (6284)" class="form-control" />--%>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label">Email</label>
                                                <asp:TextBox ID="txtemail" runat="server" placeholder="www.Demo@gmail.com" CssClass="form-control tags"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtemail" ErrorMessage="E_Mail Required." ForeColor="Red" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label">Interests</label>
                                                <asp:TextBox ID="txtinterest" runat="server" placeholder="Interest" class="form-control"></asp:TextBox>
                                                <%--<input type="text" placeholder="Design, Web etc." class="form-control" />--%>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label">Occupation</label>
                                                <asp:TextBox ID="txtocc" runat="server" placeholder="Occupation" class="form-control"></asp:TextBox>
                                                <%--<input type="text" placeholder="Web Developer" class="form-control" />--%>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label">About</label>
                                                <asp:TextBox ID="txtabove" class="form-control" Rows="3" placeholder="We Really Like your product please continue" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                <%--<textarea class="form-control" rows="3" placeholder="We are KeenThemes!!!"></textarea>--%>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label">Website Url</label>
                                                <asp:TextBox ID="txtweburl" runat="server" placeholder="http://www.mywebsite.com" class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtweburl" ErrorMessage="Website Url Required." ForeColor="Red" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <%--<input type="text" placeholder="http://www.mywebsite.com" class="form-control" />--%>
                                            </div>
                                            <div class="col-md-9">
                                                <div class="col-md-3" style="margin-left: -12px;">
                                                    <div class="form-group">

                                                        <asp:Label ID="Label17" runat="server" Text="Social Media:"></asp:Label>
                                                        <asp:DropDownList ID="drpSomib" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="drpSomib" ErrorMessage="Social Media Required." InitialValue="0" ValidationGroup="sosieyl"></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">

                                                        <asp:Label ID="Label19" runat="server" Text="Social Media ID"></asp:Label>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtSocial" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="txtSocial" ErrorMessage="Social Media Id  Required." ValidationGroup="sosieyl"></asp:RequiredFieldValidator>
                                                    </div>

                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label5" runat="server" Text=" "></asp:Label>
                                                        <asp:Button ID="btnsocial" CssClass="btn yellow" runat="server" Style="margin-top: 19px;" OnClick="btnsocial_Click" Text="Add" />
                                                    </div>
                                                </div>

                                                <div class="tabbable">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>Social Media</th>
                                                                <th>Social Media Id</th>
                                                                <th>Remark</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:ListView ID="listSocialMedia" runat="server">
                                                                <LayoutTemplate>
                                                                    <tr id="ItemPlaceholder" runat="server">
                                                                    </tr>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# getshocial(Convert .ToInt32 ( Eval("Recource")))%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("RecValue")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblEMAIL" runat="server" Text='<%# getremark(Convert .ToInt32 ( Eval("Recource")))%>'></asp:Label></td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </tbody>
                                                    </table>
                                                </div>

                                                <div class="margiv-top-10">
                                                    <%--<a href="javascript:;" class="btn green">Save Changes </a>--%>
                                                    <asp:Button ID="btnProfile" class="btn green" runat="server" Text="Save Changes" ValidationGroup="submit" OnClick="btnProfile_Click" />
                                                    <a href="javascript:;" class="btn default">Cancel </a>
                                                </div>
                                                <%--</form>--%>
                                            </div>
                                        </div>

                                        <div id="tab_2-2" class="tab-pane">
                                            <%--<p>
                                                Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod.
                                                       
                                            </p>--%>
                                            <%--<form action="#" role="form">--%>
                                            <div class="form-group">
                                                <div class="fileinput fileinput-new" data-provides="fileinput">
                                                    <div class="fileinput-new thumbnail" style="width: 200px; height: 150px;">
                                                        <img src="http://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image" alt="" />
                                                    </div>
                                                    <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 200px; max-height: 150px;"></div>
                                                    <div>
                                                        <%-- <span class="btn default btn-file">--%>
                                                        <%--<span class="fileinput-new">Select image </span>--%>
                                                        <asp:FileUpload ID="FileAvtar" class="btn btn-circle green-haze btn-sm" runat="server" />
                                                        <%--<span class="fileinput-exists">Change </span>
                                                                <input type="file" name="...">--%>
                                                        <%--</span>--%>
                                                        <a href="javascript:;" class="btn default fileinput-exists" data-dismiss="fileinput">Remove </a>
                                                    </div>
                                                </div>
                                                <div class="clearfix margin-top-10">
                                                    <span class="label label-danger">NOTE! </span>
                                                    <span>Attached image thumbnail is supported in Latest Firefox, Chrome, Opera, Safari and Internet Explorer 10 only </span>
                                                </div>
                                            </div>
                                            <div class="margin-top-10">
                                                <%--<a href="javascript:;" class="btn green">Submit </a>--%>
                                                <asp:Button ID="btnSubmit" runat="server" class="btn green" Text="Submit" OnClick="btnSubmit_Click" />
                                                <a href="javascript:;" class="btn default">Cancel </a>
                                                <%-- <asp:Button ID="btnCancel" runat="server" Text="Cancel" />--%>
                                            </div>
                                            <%-- </form>--%>
                                        </div>
                                        <div id="tab_3-3" class="tab-pane">
                                            <%--<form action="#">--%>
                                            <div class="form-group">
                                                <label class="control-label">Current Password</label>
                                                <asp:TextBox ID="txtCurrentPassword" CssClass="form-control" runat="server"></asp:TextBox>
                                                <%--<input type="password" class="form-control" />--%>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label">New Password</label>
                                                <asp:TextBox ID="txtNewPassword" CssClass="form-control" runat="server"></asp:TextBox>
                                                <%--<input type="password" class="form-control" />--%>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label">Re-type New Password</label>
                                                <asp:TextBox ID="txtRnewPassword" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password Not Match" Display="Dynamic" CssClass="Validation" ForeColor="Red" ControlToCompare="txtNewPassword" ControlToValidate="txtRnewPassword"></asp:CompareValidator>

                                                <%--<input type="password" class="form-control" />--%>
                                            </div>
                                            <div class="margin-top-10">
                                                <%--<a href="javascript:;" class="btn green">Change Password </a>--%>
                                                <asp:Button ID="btnChangePassword" CssClass="btn green" runat="server" Text="Change Password" OnClick="btnChangePassword_Click" />
                                                <a href="javascript:;" class="btn default">Cancel </a>
                                            </div>
                                            <%--</form>--%>
                                        </div>
                                        <div id="tab_4-4" class="tab-pane">
                                            <%--<form action="#">--%>
                                            <table class="table table-bordered table-striped">
                                                <tr>
                                                    <td>Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus.. </td>
                                                    <td>
                                                        <div class="mt-radio-inline">
                                                            <label class="mt-radio">
                                                                <input type="radio" name="optionsRadios1" value="option1" />
                                                                Yes
                                                                           
                                                                            <span></span>
                                                            </label>
                                                            <label class="mt-radio">
                                                                <input type="radio" name="optionsRadios1" value="option2" checked />
                                                                No
                                                                           
                                                                            <span></span>
                                                            </label>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Enim eiusmod high life accusamus terry richardson ad squid wolf moon </td>
                                                    <td>
                                                        <div class="mt-radio-inline">
                                                            <label class="mt-radio">
                                                                <input type="radio" name="optionsRadios21" value="option1" />
                                                                Yes
                                                                           
                                                                            <span></span>
                                                            </label>
                                                            <label class="mt-radio">
                                                                <input type="radio" name="optionsRadios21" value="option2" checked />
                                                                No
                                                                           
                                                                            <span></span>
                                                            </label>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Enim eiusmod high life accusamus terry richardson ad squid wolf moon </td>
                                                    <td>
                                                        <div class="mt-radio-inline">
                                                            <label class="mt-radio">
                                                                <input type="radio" name="optionsRadios31" value="option1" />
                                                                Yes
                                                                           
                                                                            <span></span>
                                                            </label>
                                                            <label class="mt-radio">
                                                                <input type="radio" name="optionsRadios31" value="option2" checked />
                                                                No
                                                                           
                                                                            <span></span>
                                                            </label>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Enim eiusmod high life accusamus terry richardson ad squid wolf moon </td>
                                                    <td>
                                                        <div class="mt-radio-inline">
                                                            <label class="mt-radio">
                                                                <input type="radio" name="optionsRadios41" value="option1" />
                                                                Yes
                                                                           
                                                                            <span></span>
                                                            </label>
                                                            <label class="mt-radio">
                                                                <input type="radio" name="optionsRadios41" value="option2" checked />
                                                                No
                                                                           
                                                                            <span></span>
                                                            </label>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!--end profile-settings-->
                                            <div class="margin-top-10">
                                                <a href="javascript:;" class="btn green">Save Changes </a>
                                                <a href="javascript:;" class="btn default">Cancel </a>
                                            </div>
                                            <%--</form>--%>
                                        </div>
                                    </div>
                                </div>
                                <!--end col-md-9-->
                            </div>
                        </div>
                        <!--end tab-pane-->
                        <div class="tab-pane" id="tab_1_6">
                            <div class="row">
                                <div class="col-md-2">
                                    <ul class="ver-inline-menu tabbable margin-bottom-10">
                                        <li class="active">
                                            <a data-toggle="tab" href="#tab_1">
                                                <i class="fa fa-briefcase"></i>General Questions </a>
                                            <span class="after"></span>
                                        </li>
                                        <li>
                                            <a data-toggle="tab" href="#tab_2">
                                                <i class="fa fa-group"></i>Membership </a>
                                        </li>
                                        <li>
                                            <a data-toggle="tab" href="#tab_3">
                                                <i class="fa fa-leaf"></i>Terms Of Service </a>
                                        </li>
                                        <li>
                                            <a data-toggle="tab" href="#tab_1">
                                                <i class="fa fa-info-circle"></i>License Terms </a>
                                        </li>
                                        <li>
                                            <a data-toggle="tab" href="#tab_2">
                                                <i class="fa fa-tint"></i>Payment Rules </a>
                                        </li>
                                        <li>
                                            <a data-toggle="tab" href="#tab_3">
                                                <i class="fa fa-plus"></i>Other Questions </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-10">
                                    <div class="tab-content">
                                        <div id="tab_1" class="tab-pane active">
                                            <div id="accordion1" class="panel-group">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#accordion1_1">1. Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion1_1" class="panel-collapse collapse in">
                                                        <div class="panel-body">
                                                            Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt
                                                                    laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes
                                                                    anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't
                                                                    heard of them accusamus labore sustainable VHS.
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#accordion1_2">2. Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion1_2" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Anim pariatur cliche reprehenderit,
                                                                    enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf
                                                                    moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente
                                                                    ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable
                                                                    VHS.
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-success">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#accordion1_3">3. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion1_3" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Anim pariatur cliche reprehenderit,
                                                                    enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf
                                                                    moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente
                                                                    ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable
                                                                    VHS.
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-warning">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#accordion1_4">4. Wolf moon officia aute, non cupidatat skateboard dolor brunch ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion1_4" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee
                                                                    nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat
                                                                    craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-danger">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#accordion1_5">5. Leggings occaecat craft beer farm-to-table, raw denim aesthetic ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion1_5" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee
                                                                    nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat
                                                                    craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS. Food truck quinoa nesciunt laborum eiusmod. Brunch 3
                                                                    wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#accordion1_6">6. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion1_6" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee
                                                                    nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat
                                                                    craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS. Food truck quinoa nesciunt laborum eiusmod. Brunch 3
                                                                    wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#accordion1_7">7. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion1_7" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee
                                                                    nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat
                                                                    craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS. Food truck quinoa nesciunt laborum eiusmod. Brunch 3
                                                                    wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab_2" class="tab-pane">
                                            <div id="accordion2" class="panel-group">
                                                <div class="panel panel-warning">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#accordion2_1">1. Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion2_1" class="panel-collapse collapse in">
                                                        <div class="panel-body">
                                                            <p>
                                                                Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt
                                                                        laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore
                                                                        wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably
                                                                        haven't heard of them accusamus labore sustainable VHS.
                                                            </p>
                                                            <p>
                                                                Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt
                                                                        laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore
                                                                        wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably
                                                                        haven't heard of them accusamus labore sustainable VHS.
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-danger">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#accordion2_2">2. Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion2_2" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Anim pariatur cliche reprehenderit,
                                                                    enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf
                                                                    moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente
                                                                    ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable
                                                                    VHS.
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-success">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#accordion2_3">3. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion2_3" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Anim pariatur cliche reprehenderit,
                                                                    enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf
                                                                    moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente
                                                                    ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable
                                                                    VHS.
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#accordion2_4">4. Wolf moon officia aute, non cupidatat skateboard dolor brunch ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion2_4" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee
                                                                    nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat
                                                                    craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#accordion2_5">5. Leggings occaecat craft beer farm-to-table, raw denim aesthetic ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion2_5" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee
                                                                    nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat
                                                                    craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS. Food truck quinoa nesciunt laborum eiusmod. Brunch 3
                                                                    wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#accordion2_6">6. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion2_6" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee
                                                                    nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat
                                                                    craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS. Food truck quinoa nesciunt laborum eiusmod. Brunch 3
                                                                    wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#accordion2_7">7. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion2_7" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee
                                                                    nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat
                                                                    craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS. Food truck quinoa nesciunt laborum eiusmod. Brunch 3
                                                                    wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab_3" class="tab-pane">
                                            <div id="accordion3" class="panel-group">
                                                <div class="panel panel-danger">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion3" href="#accordion3_1">1. Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion3_1" class="panel-collapse collapse in">
                                                        <div class="panel-body">
                                                            <p>
                                                                Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt
                                                                        laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et.
                                                            </p>
                                                            <p>
                                                                Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt
                                                                        laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et.
                                                            </p>
                                                            <p>
                                                                Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica,
                                                                        craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt
                                                                        you probably haven't heard of them accusamus labore sustainable VHS.
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-success">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion3" href="#accordion3_2">2. Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion3_2" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Anim pariatur cliche reprehenderit,
                                                                    enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf
                                                                    moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente
                                                                    ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable
                                                                    VHS.
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion3" href="#accordion3_3">3. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion3_3" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Anim pariatur cliche reprehenderit,
                                                                    enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf
                                                                    moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente
                                                                    ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable
                                                                    VHS.
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion3" href="#accordion3_4">4. Wolf moon officia aute, non cupidatat skateboard dolor brunch ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion3_4" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee
                                                                    nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat
                                                                    craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS.
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion3" href="#accordion3_5">5. Leggings occaecat craft beer farm-to-table, raw denim aesthetic ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion3_5" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee
                                                                    nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat
                                                                    craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS. Food truck quinoa nesciunt laborum eiusmod. Brunch 3
                                                                    wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion3" href="#accordion3_6">6. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion3_6" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee
                                                                    nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat
                                                                    craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS. Food truck quinoa nesciunt laborum eiusmod. Brunch 3
                                                                    wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion3" href="#accordion3_7">7. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft ? </a>
                                                        </h4>
                                                    </div>
                                                    <div id="accordion3_7" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee
                                                                    nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat
                                                                    craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS. Food truck quinoa nesciunt laborum eiusmod. Brunch 3
                                                                    wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--end tab-pane-->
                    </div>
                </div>
            </div>
            <!-- END PAGE BASE CONTENT -->
        </div>
        <!-- END CONTENT BODY -->

        <!-- END CONTAINER -->
    </form>
    <!--[if lt IE 9]>
<script src="assets/global/plugins/respond.min.js"></script>
<script src="assets/global/plugins/excanvas.min.js"></script> 
<script src="assets/global/plugins/ie8.fix.min.js"></script> 
<![endif]-->
    <!-- BEGIN CORE PLUGINS -->
    <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js" type="text/javascript"></script>
    <script src="http://maps.google.com/maps/api/js?sensor=false" type="text/javascript"></script>
    <script src="assets/global/plugins/gmaps/gmaps.min.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL SCRIPTS -->
    <script src="assets/global/scripts/app.min.js" type="text/javascript"></script>
    <!-- END THEME GLOBAL SCRIPTS -->
    <!-- BEGIN THEME LAYOUT SCRIPTS -->
    <script src="assets/layouts/layout4/scripts/layout.min.js" type="text/javascript"></script>
    <script src="assets/layouts/layout4/scripts/demo.min.js" type="text/javascript"></script>
    <script src="assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
    <script src="assets/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-tags-input/jquery.tagsinput.js"></script>
    <script src="../assets/global/plugins/jquery-tags-input/jquery.tagsinput.min.js"></script>
    <!-- END THEME LAYOUT SCRIPTS -->
    <script>
        $(document).ready(function () {
            $('#clickmewow').click(function () {
                $('#radio1003').attr('checked', 'checked');
            });
        })
    </script>
</body>


<!-- Mirrored from localhost:64878/theme/admin_4/ayoprofile.html by HTTrack Website Copier/3.x [XR&CO'2014], Wed, 20 Sep 2017 05:53:58 GMT -->
</html>
