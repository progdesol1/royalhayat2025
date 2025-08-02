<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemoPOS.aspx.cs" Inherits="Web.ACM.DemoPOS" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html>
<html lang="en">
<!-- begin::Head -->
<head>
    <meta charset="utf-8" />
    <title>Royal Hayat Hospital 
    </title>
    <meta name="description" content="Latest updates and statistic charts">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <!--begin::Web font -->
    <%-- <script src="https://ajax.googleapis.com/ajax/libs/webfont/1.6.16/webfont.js"></script>--%>
    <%--<script>
        WebFont.load({
            google: { "families": ["Poppins:300,400,500,600,700", "Roboto:300,400,500,600,700"] },
            active: function () {
                sessionStorage.fonts = true;
            }
        });
    </script>--%>
    <!--end::Web font -->
    <!--begin::Base Styles -->
    <link href="../assetsP/vendors/base/vendors.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../assetsP/demo/demo2/base/style.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/components-rounded.css" rel="stylesheet" />
    <!--end::Base Styles -->
    <link rel="icon" type="image/png" href="/favicon.ico" />

    <script type="text/javascript">
        // <![CDATA[
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // this is the function I'm trying to replace:
        function loadIframe(iframeName, url) {
            if (window.frames[iframeName]) {
                window.frames[iframeName].location = url;
                return false;
            }
            return true;
        }


        // ]]>
    </script>
    <script type="text/javascript">
        function setIframeHeight(iframe) {
            if (iframe) {
                var iframeWin = iframe.contentWindow || iframe.contentDocument.parentWindow;
                if (iframeWin.document.body) {
                    iframe.height = 0 + iframeWin.document.documentElement.scrollHeight || iframeWin.document.body.scrollHeight;

                }
            }
        };
    </script>
    <style>
        .demo-preview__frame {
            border: none;
            -webkit-box-flex: 1;
            -ms-flex: 1;
            flex: 1;
            position: relative;
            z-index: 1;
            width: 100%;
            height: 100%;
        }
    </style>
    <script type="text/javascript">
        function changeClass() {
            if (document.getElementsByClassName("m-menu__item m-menu__item--submenu m-menu__item--rel").classList.contains('m-menu__item m-menu__item--submenu m-menu__item--rel')) {
                document.getElementsByClassName("m-menu__item m-menu__item--submenu m-menu__item--rel m-menu__item--open-dropdown m-menu__item--hover").classList.remove('m-menu__item m-menu__item--submenu m-menu__item--rel m-menu__item--open-dropdown m-menu__item--hover');
                document.getElementsByClassName("m-menu__item m-menu__item--submenu m-menu__item--rel").classList.add('m-menu__item m-menu__item--submenu m-menu__item--rel');
            }
        }
    </script>
</head>
<!-- end::Head -->
<!-- end::Body -->
<body class="m-page--wide m-header--fixed m-header--fixed-mobile m-footer--push m-aside--offcanvas-default">
    <form id="form1" class="m-grid m-grid--hor m-grid--root m-page" runat="server">

        <asp:ScriptManager ID="toolscriptmanagerID" runat="server">
        </asp:ScriptManager>
        <asp:LinkButton ID="lintenet" Style="display: none;" runat="server"><i class="icon-key"></i> Set TenetId</asp:LinkButton>
        <asp:Panel ID="Panel2" runat="server" Style="display: none;" DefaultButton="Button1">

            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box red-soft">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-globe"></i>
                                Warning
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="form-group">
                                <div class="col-md-12" style="padding-top: 10px; padding-bottom: 20px;">
                                    <asp:Label runat="server" ID="lbldiscription" Style="color: #a94442; font-size: 16px; font-family: 'Courier New';" class="col-md-12 control-label"></asp:Label>
                                </div>
                            </div>
                            <div class="form-actions noborder" style="padding-left: 70px;">
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-danger" Text="OK" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button1" Enabled="True" PopupControlID="Panel2" TargetControlID="lintenet"></cc1:ModalPopupExtender>
        <!-- begin:: Page -->

        <!-- begin::Header -->
        <header class="m-grid__item		m-header " data-minimize="minimize" data-minimize-offset="200" data-minimize-mobile-offset="200">
            <div class="m-header__top" style="background-color: white;">
                <div class="m-container m-container--responsive m-container--xxl m-container--full-height m-page__container">
                    <div class="m-stack m-stack--ver m-stack--desktop">
                        <!-- begin::Brand -->
                        <div class="m-stack__item m-brand" style="background-color: white;">
                            <div class="m-stack m-stack--ver m-stack--general m-stack--inline">
                                <div class="m-stack__item m-stack__item--middle m-brand__logo">
                                    <a href="index.html" class="m-brand__logo-wrapper">
                                        <%-- <img alt="" src="../assetsP/demo/demo2/media/img/logo/logo.png" />--%>
                                        <% if (Session["USERTID"].ToString() == "2")
                                           {%>
                                        <img src="../assets/RoyalHayatLogo.gif" />
                                        <%}
                                           else
                                           { %>
                                        <img src="../assets/RoyalHayatLogo.gif" />
                                        <%}%>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <!-- end::Brand -->
                        <!-- begin::Topbar -->
                        <div class="m-stack__item m-stack__item--fluid m-header-head" id="m_header_nav">
                            <div id="m_header_topbar" class="m-topbar  m-stack m-stack--ver m-stack--general">
                                <div class="m-stack__item m-topbar__nav-wrapper">
                                    <ul class="m-topbar__nav m-nav m-nav--inline">
                                        <li class="m-nav__item m-topbar__user-profile m-topbar__user-profile--img  m-dropdown m-dropdown--medium m-dropdown--arrow m-dropdown--header-bg-fill m-dropdown--align-right m-dropdown--mobile-full-width m-dropdown--skin-light" data-dropdown-toggle="click">
                                            <a href="#" class="m-nav__link m-dropdown__toggle">
                                                <span class="m-topbar__userpic m--hide">
                                                    <img src="../assetsP/app/media/img/users/user4.jpg" class="m--img-rounded m--marginless m--img-centered" alt="" />
                                                </span>
                                                <span class="m-topbar__welcome">Hello,&nbsp;
                                                </span>
                                                <asp:Label ID="lbluser" runat="server" class="m-topbar__username" Style="color: black;"></asp:Label>
                                                <%-- <span class="m-topbar__username">Nick
                                                </span>--%>
                                            </a>
                                            <div class="m-dropdown__wrapper">
                                                <span class="m-dropdown__arrow m-dropdown__arrow--right m-dropdown__arrow--adjust"></span>
                                                <div class="m-dropdown__inner">
                                                    <div class="m-dropdown__header m--align-center" style="background: url(../assetsP/app/media/img/misc/user_profile_bg.jpg); background-size: cover;">
                                                        <div class="m-card-user m-card-user--skin-dark">
                                                            <div class="m-card-user__pic">
                                                                <asp:Image ID="imgprofile"  runat="server" class="m--img-rounded m--marginless"/>
                                                              <%--  <img src="../assetsP/app/media/img/users/user4.jpg"  alt="" />--%>
                                                            </div>
                                                            <div class="m-card-user__details">
                                                                <%-- <span class="m-card-user__name m--font-weight-500">Mark Andre
                                                                </span>--%>
                                                                <asp:Label ID="Usernamee" runat="server" class="m-card-user__name m--font-weight-500"></asp:Label>
                                                                <a href="#" class="m-card-user__email m--font-weight-300 m-link">
                                                                    <asp:Label ID="useremail" runat="server"></asp:Label>
                                                                </a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="m-dropdown__body">
                                                        <div class="m-dropdown__content">
                                                            <ul class="m-nav m-nav--skin-light">
                                                                <li class="m-nav__section m--hide">
                                                                    <span class="m-nav__section-text">Section
                                                                    </span>
                                                                </li>
                                                                <li class="m-nav__item">
                                                                    <div class="m-nav__link">
                                                                        <i class="m-nav__link-icon flaticon-profile-1"></i>
                                                                        <span class="m-nav__link-title">
                                                                            <span class="m-nav__link-wrap">
                                                                                <span class="m-nav__link-text">

                                                                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>

                                                                                </span>
                                                                                <span class="m-nav__link-badge">
                                                                                    <span class="m-badge m-badge--success">2
                                                                                    </span>

                                                                                </span>
                                                                            </span>
                                                                        </span>
                                                                    </div>
                                                                </li>


                                                                <li class="m-nav__item">
                                                                    <a href="profile.html" class="m-nav__link">
                                                                        <i class="m-nav__link-icon flaticon-lifebuoy"></i>
                                                                        <span class="m-nav__link-text">Support
                                                                        </span>
                                                                    </a>
                                                                </li>
                                                                <li class="m-nav__separator m-nav__separator--fit"></li>
                                                                <li class="m-nav__item">
                                                                    <%-- <a href="#" class="btn m-btn--pill btn-secondary m-btn m-btn--custom m-btn--label-brand m-btn--bolder">
                                                                        Logout
                                                                    </a>--%>
                                                                    <asp:LinkButton ID="btnLogOut" class="btn m-btn--pill btn-secondary m-btn m-btn--custom m-btn--label-brand m-btn--bolder" runat="server" OnClick="btnLogOut_Click">Log Out</asp:LinkButton>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                        <li class="m-nav__item m-topbar__notifications m-topbar__notifications--img m-dropdown m-dropdown--large m-dropdown--header-bg-fill m-dropdown--arrow m-dropdown--align-center 	m-dropdown--mobile-full-width" data-dropdown-toggle="click" data-dropdown-persistent="true">
                                            <a href="#" class="m-nav__link m-dropdown__toggle" id="m_topbar_notification_icon">
                                                <span class="m-nav__link-badge m-badge m-badge--dot m-badge--dot-small m-badge--danger"></span>
                                                <span class="m-nav__link-icon">
                                                    <span class="m-nav__link-icon-wrapper">
                                                        <i class="flaticon-music-2"></i>
                                                    </span>
                                                </span>
                                            </a>
                                            <div class="m-dropdown__wrapper">
                                                <span class="m-dropdown__arrow m-dropdown__arrow--center"></span>
                                                <div class="m-dropdown__inner">
                                                    <div class="m-dropdown__header m--align-center" style="background: url(../assetsP/app/media/img/misc/notification_bg.jpg); background-size: cover;">
                                                        <span class="m-dropdown__header-title">9 New
                                                        </span>
                                                        <span class="m-dropdown__header-subtitle">User Notifications
                                                        </span>
                                                    </div>
                                                    <div class="m-dropdown__body">
                                                        <div class="m-dropdown__content">
                                                            <ul class="nav nav-tabs m-tabs m-tabs-line m-tabs-line--brand" role="tablist">
                                                                <li class="nav-item m-tabs__item">
                                                                    <a class="nav-link m-tabs__link active" data-toggle="tab" href="#topbar_notifications_notifications" role="tab">Alerts
                                                                    </a>
                                                                </li>

                                                                <li class="nav-item m-tabs__item">
                                                                    <a class="nav-link m-tabs__link" data-toggle="tab" href="#topbar_notifications_logs" role="tab">Logs
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                            <div class="tab-content">
                                                                <div class="tab-pane active" id="topbar_notifications_notifications" role="tabpanel">
                                                                    <div class="m-scrollable" data-scrollable="true" data-max-height="250" data-mobile-max-height="200">
                                                                        <div class="m-list-timeline m-list-timeline--skin-light">
                                                                            <div class="m-list-timeline__items">
                                                                                <div class="m-list-timeline__item">
                                                                                    <span class="m-list-timeline__badge -m-list-timeline__badge--state-success"></span>
                                                                                    <span class="m-list-timeline__text">12 new users registered
                                                                                    </span>
                                                                                    <span class="m-list-timeline__time">Just now
                                                                                    </span>
                                                                                </div>
                                                                                <div class="m-list-timeline__item">
                                                                                    <span class="m-list-timeline__badge"></span>
                                                                                    <span class="m-list-timeline__text">System shutdown
																							<span class="m-badge m-badge--success m-badge--wide">pending
                                                                                            </span>
                                                                                    </span>
                                                                                    <span class="m-list-timeline__time">14 mins
                                                                                    </span>
                                                                                </div>
                                                                                <div class="m-list-timeline__item">
                                                                                    <span class="m-list-timeline__badge"></span>
                                                                                    <span class="m-list-timeline__text">New invoice received
                                                                                    </span>
                                                                                    <span class="m-list-timeline__time">20 mins
                                                                                    </span>
                                                                                </div>
                                                                                <div class="m-list-timeline__item">
                                                                                    <span class="m-list-timeline__badge"></span>
                                                                                    <span class="m-list-timeline__text">DB overloaded 80%
																							<span class="m-badge m-badge--info m-badge--wide">settled
                                                                                            </span>
                                                                                    </span>
                                                                                    <span class="m-list-timeline__time">1 hr
                                                                                    </span>
                                                                                </div>
                                                                                <div class="m-list-timeline__item">
                                                                                    <span class="m-list-timeline__badge"></span>
                                                                                    <span class="m-list-timeline__text">System error -
																							<a href="#" class="m-link">Check
                                                                                            </a>
                                                                                    </span>
                                                                                    <span class="m-list-timeline__time">2 hrs
                                                                                    </span>
                                                                                </div>
                                                                                <div class="m-list-timeline__item">
                                                                                    <span class="m-list-timeline__badge"></span>
                                                                                    <span class="m-list-timeline__text">Production server down
                                                                                    </span>
                                                                                    <span class="m-list-timeline__time">3 hrs
                                                                                    </span>
                                                                                </div>
                                                                                <div class="m-list-timeline__item">
                                                                                    <span class="m-list-timeline__badge"></span>
                                                                                    <span class="m-list-timeline__text">Production server up
                                                                                    </span>
                                                                                    <span class="m-list-timeline__time">5 hrs
                                                                                    </span>
                                                                                </div>
                                                                                <div class="m-list-timeline__item">
                                                                                    <span class="m-list-timeline__badge"></span>
                                                                                    <span href="" class="m-list-timeline__text">New order received
																							<span class="m-badge m-badge--danger m-badge--wide">urgent
                                                                                            </span>
                                                                                    </span>
                                                                                    <span class="m-list-timeline__time">7 hrs
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="tab-pane" id="topbar_notifications_events" role="tabpanel">
                                                                    <div class="m-scrollable" m-scrollabledata-scrollable="true" data-max-height="250" data-mobile-max-height="200">
                                                                        <div class="m-list-timeline m-list-timeline--skin-light">
                                                                            <div class="m-list-timeline__items">
                                                                                <div class="m-list-timeline__item">
                                                                                    <span class="m-list-timeline__badge m-list-timeline__badge--state1-success"></span>
                                                                                    <a href="" class="m-list-timeline__text">New order received
                                                                                    </a>
                                                                                    <span class="m-list-timeline__time">Just now
                                                                                    </span>
                                                                                </div>
                                                                                <div class="m-list-timeline__item">
                                                                                    <span class="m-list-timeline__badge m-list-timeline__badge--state1-danger"></span>
                                                                                    <a href="" class="m-list-timeline__text">New invoice received
                                                                                    </a>
                                                                                    <span class="m-list-timeline__time">20 mins
                                                                                    </span>
                                                                                </div>
                                                                                <div class="m-list-timeline__item">
                                                                                    <span class="m-list-timeline__badge m-list-timeline__badge--state1-success"></span>
                                                                                    <a href="" class="m-list-timeline__text">Production server up
                                                                                    </a>
                                                                                    <span class="m-list-timeline__time">5 hrs
                                                                                    </span>
                                                                                </div>
                                                                                <div class="m-list-timeline__item">
                                                                                    <span class="m-list-timeline__badge m-list-timeline__badge--state1-info"></span>
                                                                                    <a href="" class="m-list-timeline__text">New order received
                                                                                    </a>
                                                                                    <span class="m-list-timeline__time">7 hrs
                                                                                    </span>
                                                                                </div>
                                                                                <div class="m-list-timeline__item">
                                                                                    <span class="m-list-timeline__badge m-list-timeline__badge--state1-info"></span>
                                                                                    <a href="" class="m-list-timeline__text">System shutdown
                                                                                    </a>
                                                                                    <span class="m-list-timeline__time">11 mins
                                                                                    </span>
                                                                                </div>
                                                                                <div class="m-list-timeline__item">
                                                                                    <span class="m-list-timeline__badge m-list-timeline__badge--state1-info"></span>
                                                                                    <a href="" class="m-list-timeline__text">Production server down
                                                                                    </a>
                                                                                    <span class="m-list-timeline__time">3 hrs
                                                                                    </span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="tab-pane" id="topbar_notifications_logs" role="tabpanel">
                                                                    <div class="m-stack m-stack--ver m-stack--general" style="min-height: 180px;">
                                                                        <div class="m-stack__item m-stack__item--center m-stack__item--middle">

                                                                            <div class="table-responsive">
                                                                                <table class="table table-striped table-hover table-bordered">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Log Name
                                                                                            </th>
                                                                                            <th>User
                                                                                            </th>
                                                                                            <th>Date Time
                                                                                            </th>

                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:ListView ID="ListOrderTop10" runat="server">
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("AuditType") %>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label3" runat="server" Text='<%#getname(Convert.ToInt32( Eval("CreatedUserName")))%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("CreatedDate") %>'></asp:Label></td>
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
                                            </div>
                                        </li>
                                        <li class="m-nav__item m-topbar__quick-actions m-topbar__quick-actions--img m-dropdown m-dropdown--large m-dropdown--header-bg-fill m-dropdown--arrow m-dropdown--align-right m-dropdown--align-push m-dropdown--mobile-full-width m-dropdown--skin-light" data-dropdown-toggle="click">
                                            <a href="#" class="m-nav__link m-dropdown__toggle">
                                                <span class="m-nav__link-badge m-badge m-badge--dot m-badge--info m--hide"></span>
                                                <span class="m-nav__link-icon">
                                                    <span class="m-nav__link-icon-wrapper">
                                                        <i class="flaticon-share"></i>
                                                    </span>
                                                </span>
                                            </a>
                                            <div class="m-dropdown__wrapper">
                                                <span class="m-dropdown__arrow m-dropdown__arrow--right m-dropdown__arrow--adjust"></span>
                                                <div class="m-dropdown__inner">
                                                    <div class="m-dropdown__header m--align-center" style="background: url(../assetsP/app/media/img/misc/quick_actions_bg.jpg); background-size: cover;">
                                                        <span class="m-dropdown__header-title">Quick Actions
                                                        </span>
                                                        <span class="m-dropdown__header-subtitle">Shortcuts
                                                        </span>
                                                    </div>
                                                    <div class="m-dropdown__body m-dropdown__body--paddingless">
                                                        <div class="m-dropdown__content">
                                                            <div class="m-scrollable" data-scrollable="false" data-max-height="380" data-mobile-max-height="200">
                                                                <div class="m-nav-grid m-nav-grid--skin-light">
                                                                    <div class="m-nav-grid__row">
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="m-nav-grid__item" OnClick="LinkButton1_Click1">
                                                                            <i class="m-nav-grid__icon flaticon-map"></i>
                                                                            <span class="m-nav-grid__text">Complain
                                                                            </span>
                                                                        </asp:LinkButton>
                                                                        <%-- <a href="#" class="m-nav-grid__item">
                                                                            <i class="m-nav-grid__icon flaticon-file"></i>
                                                                            <span class="m-nav-grid__text">Generate Report
                                                                            </span>
                                                                        </a>--%>
                                                                        <a href="../POS/ClientTiketR.aspx?status=pending" class="m-nav-grid__item">
                                                                            <i class="m-nav-grid__icon flaticon-time"></i>
                                                                            <span class="m-nav-grid__text">Complain Type
                                                                            </span>
                                                                        </a>
                                                                    </div>
                                                                    <div class="m-nav-grid__row">
                                                                        <a href="#../ReportMst/HelpDeskReport.aspx" class="m-nav-grid__item">
                                                                            <i class="m-nav-grid__icon flaticon-folder"></i>
                                                                            <span class="m-nav-grid__text">Complain Report
                                                                            </span></a>

                                                                        <a href="../ReportMst/HelpDeskExcRep.aspx" class="m-nav-grid__item">
                                                                            <i class="m-nav-grid__icon flaticon-clipboard"></i>
                                                                            <span class="m-nav-grid__text">% Report
                                                                            </span>
                                                                        </a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
                                        <li id="m_quick_sidebar_toggle" class="m-nav__item">
                                            <a href="#" class="m-nav__link m-dropdown__toggle">
                                                <span class="m-nav__link-icon m-nav__link-icon--aside-toggle">
                                                    <span class="m-nav__link-icon-wrapper">
                                                        <i class="flaticon-grid-menu"></i>
                                                    </span>
                                                </span>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <!-- end::Topbar -->
                    </div>
                </div>
            </div>
            <div class="m-header__bottom" style="background-color: maroon;">
                <div class="m-container m-container--responsive m-container--xxl m-container--full-height m-page__container">
                    <div class="m-stack m-stack--ver m-stack--desktop">
                        <!-- begin::Horizontal Menu -->
                        <div class="m-stack__item m-stack__item--middle m-stack__item--fluid">
                            <button class="m-aside-header-menu-mobile-close  m-aside-header-menu-mobile-close--skin-light " id="m_aside_header_menu_mobile_close_btn">
                                <i class="la la-close"></i>
                            </button>
                            <div id="m_header_menu" class="m-header-menu m-aside-header-menu-mobile m-aside-header-menu-mobile--offcanvas  m-header-menu--skin-dark m-header-menu--submenu-skin-light m-aside-header-menu-mobile--skin-light m-aside-header-menu-mobile--submenu-skin-light ">
                                <ul class="m-menu__nav  m-menu__nav--submenu-arrow ">
                                    <%-- <li class="m-menu__item  m-menu__item--active" aria-haspopup="true">
                                            <asp:Literal ID="lblDashboard" runat="server"></asp:Literal>
                                        </li>--%>
                                    <li class="m-menu__item  m-menu__item--active" aria-haspopup="true">

                                        <asp:Literal ID="lblDashboard" runat="server"></asp:Literal>

                                    </li>
                                    <asp:ListView ID="ltsMenu" runat="server">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMenuHide" runat="server" Visible="false" Text=' <%# Eval("MENUID")%>'></asp:Label>
                                            <li class="m-menu__item  m-menu__item--submenu m-menu__item--rel" data-menu-submenu-toggle="click" aria-haspopup="true">
                                                <a href="#" class="m-menu__link m-menu__toggle">
                                                    <span class="m-menu__item-here"></span>
                                                    <span class="m-menu__link-text"><%# GetLname(Convert.ToInt32(Eval("MENUID")))%>
                                                    </span>
                                                    <i class="m-menu__hor-arrow la la-angle-down"></i>
                                                    <i class="m-menu__ver-arrow la la-angle-right"></i>
                                                </a>
                                                <%# GetLink(Convert.ToInt32(Eval("MENUID")))%>

                                                <%# Displaysubmenu1(Convert.ToInt32(Eval("MENUID")))%>
                                            </li>
                                        </ItemTemplate>
                                    </asp:ListView>

                                    <asp:ListView ID="lstMaster" runat="server">
                                        <ItemTemplate>
                                            <li class="m-menu__item  m-menu__item--submenu m-menu__item--rel" data-menu-submenu-toggle="click" data-redirect="true" aria-haspopup="true">
                                                <a href="#" class="m-menu__link m-menu__toggle">
                                                    <span class="m-menu__item-here"></span>
                                                    <span class="m-menu__link-text">Actions
                                                    </span>
                                                    <i class="m-menu__hor-arrow la la-angle-down"></i>
                                                    <i class="m-menu__ver-arrow la la-angle-right"></i>
                                                </a>
                                                <%# GetLink(Convert.ToInt32(Eval("MENUID")))%>
                                            </li>
                                        </ItemTemplate>
                                    </asp:ListView>

                                    <asp:ListView ID="lstisGloble" runat="server">
                                        <ItemTemplate>
                                            <li class="m-menu__item  m-menu__item--submenu m-menu__item--rel" data-menu-submenu-toggle="click" aria-haspopup="true">
                                                <a href='<%# Eval("LINK")%>'>
                                                    <i class="icon-docs"></i><%# Eval("MENU_NAME1")%><span class="badge badge-success">&nbsp; </span>
                                                </a>
                                            </li>
                                        </ItemTemplate>

                                    </asp:ListView>

                                </ul>
                            </div>
                        </div>
                        <%--<div class="m-stack__item m-stack__item--middle m-dropdown m-dropdown--arrow m-dropdown--large m-dropdown--mobile-full-width m-dropdown--align-right m-dropdown--skin-light m-header-search m-header-search--expandable m-header-search--skin-" id="m_quicksearch" data-search-type="default">                           
                            <div class="m-header-search__form">
                                <div class="m-header-search__wrapper">
                                    <span class="m-header-search__icon-search" id="m_quicksearch_search">
                                        <i class="la la-search"></i>
                                    </span>
                                    <span class="m-header-search__input-wrapper">
                                        <input autocomplete="off" type="text" name="q" class="m-header-search__input" value="" placeholder="Search..." id="m_quicksearch_input">
                                    </span>
                                    <span class="m-header-search__icon-close" id="m_quicksearch_close">
                                        <i class="la la-remove"></i>
                                    </span>
                                    <span class="m-header-search__icon-cancel" id="m_quicksearch_cancel">
                                        <i class="la la-times"></i>
                                    </span>
                                </div>
                            </div>                            
                            <div class="m-dropdown__wrapper">
                                <div class="m-dropdown__arrow m-dropdown__arrow--center"></div>
                                <div class="m-dropdown__inner">
                                    <div class="m-dropdown__body">
                                        <div class="m-dropdown__scrollable m-scrollable" data-scrollable="true" data-max-height="300" data-mobile-max-height="200">
                                            <div class="m-dropdown__content m-list-search m-list-search--skin-light"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>                            
                        </div> --%>
                    </div>
                </div>
            </div>
        </header>
        <!-- end::Header -->
        <!-- begin::Body -->
        <div class="m-grid__item m-grid__item--fluid m-grid m-grid--hor-desktop m-grid--desktop m-body">
            <div class="m-grid__item m-grid__item--fluid  m-grid m-grid--ver	m-container m-container--responsive m-container--xxl m-page__container">
                <div class="m-grid__item m-grid__item--fluid m-wrapper">
                    <!-- BEGIN: Subheader -->
                    <div class="m-subheader">
                        <%-- <div class="d-flex align-items-center">--%>
                        <div class="mr-auto">
                            <%--<h3 class="m-subheader__title ">
										Dashboard
									</h3>--%>
                        </div>
                        <%--<div>
									<span class="m-subheader__daterange" id="m_dashboard_daterangepicker">
										<span class="m-subheader__daterange-label">
											<span class="m-subheader__daterange-title"></span>
											<span class="m-subheader__daterange-date m--font-brand"></span>
										</span>
										<a href="#" class="btn btn-sm btn-brand m-btn m-btn--icon m-btn--icon-only m-btn--custom m-btn--pill">
											<i class="la la-angle-down"></i>
										</a>
									</span>
								</div>
							</div>--%>
                        <%--</div>--%>
                        <!-- END: Subheader -->

                        <div class="m-content" style="height: 900px; padding-top: 15px;">
                            <%--<iframe runat="server" id="ifrm123" name="ifrm" frameborder="0" class="demo-preview__frame" clientidmode="Static" width="100%" height="100%" style="overflow: hidden"></iframe>--%>
                            <iframe class="demo-preview__frame" runat="server" id="ifrm" name="ifrm" style="overflow: hidden"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- end::Body -->
        <!-- begin::Footer -->
        <footer class="m-grid__item m-footer ">
            <div class="m-container m-container--responsive m-container--xxl m-container--full-height m-page__container">
                <div class="m-footer__wrapper">
                    <div class="m-stack m-stack--flex-tablet-and-mobile m-stack--ver m-stack--desktop">
                        <div class="m-stack__item m-stack__item--left m-stack__item--middle m-stack__item--last">
                            <span class="m-footer__copyright">2019 © Royal Hayat Hospital.
									<a href="#" class="m-link"></a>
                            </span>
                        </div>
                        <div class="m-stack__item m-stack__item--right m-stack__item--middle m-stack__item--first">
                            <ul class="m-footer__nav m-nav m-nav--inline m--pull-right">
                                <li class="m-nav__item">
                                    <a href="#" class="m-nav__link">
                                        <span class="m-nav__link-text">About
                                        </span>
                                    </a>
                                </li>
                                <li class="m-nav__item">
                                    <a href="#" class="m-nav__link">
                                        <span class="m-nav__link-text">Privacy
                                        </span>
                                    </a>
                                </li>
                                <li class="m-nav__item">
                                    <a href="#" class="m-nav__link">
                                        <span class="m-nav__link-text">T&C
                                        </span>
                                    </a>
                                </li>
                                <%-- <li class="m-nav__item">
                                    <a href="#" class="m-nav__link">
                                        <span class="m-nav__link-text">Purchase
                                        </span>
                                    </a>
                                </li>--%>
                                <li class="m-nav__item m-nav__item--last">
                                    <a href="#" class="m-nav__link" data-toggle="m-tooltip" title="Support Center" data-placement="left">
                                        <i class="m-nav__link-icon flaticon-info m--icon-font-size-lg3"></i>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </footer>
        <!-- end::Footer -->

        <!-- end:: Page -->
        <!-- begin::Quick Sidebar -->
        <div id="m_quick_sidebar" class="m-quick-sidebar m-quick-sidebar--tabbed m-quick-sidebar--skin-light">
            <div class="m-quick-sidebar__content m--hide">
                <span id="m_quick_sidebar_close" class="m-quick-sidebar__close">
                    <i class="la la-close"></i>
                </span>
                <ul id="m_quick_sidebar_tabs" class="nav nav-tabs m-tabs m-tabs-line m-tabs-line--brand" role="tablist">
                    <li class="nav-item m-tabs__item">
                        <a class="nav-link m-tabs__link active" data-toggle="tab" href="#m_quick_sidebar_tabs_messenger" role="tab">Messages
                        </a>
                    </li>

                    <li class="nav-item m-tabs__item">
                        <a class="nav-link m-tabs__link" data-toggle="tab" href="#m_quick_sidebar_tabs_logs" role="tab">Logs
                        </a>
                    </li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active m-scrollable" id="m_quick_sidebar_tabs_messenger" role="tabpanel">
                        <div class="m-messenger m-messenger--message-arrow m-messenger--skin-light">

                            <div class="m-messenger__messages">
                                <asp:Timer ID="Timer1" runat="server" Interval="18000" OnTick="Timer1_Tick">
                                </asp:Timer>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" class="item" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:ListView ID="listChet" runat="server">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Visible="false" Text='<%# Eval("TenentID") %>'></asp:Label>
                                                <div class='<%# getclassess(Convert.ToInt32(Eval("TenentID"))) %>' id="inout" runat="server">
                                                    <div class="m-messenger__message-body" style="padding-left: 1px;">
                                                        <div class="m-messenger__message-arrow"></div>
                                                        <div class="m-messenger__message-content">
                                                            <div class="m-messenger__message-username">
                                                                <%#Eval("Version")%> &nbsp; <%# Convert.ToDateTime(Eval("UPDTTIME")).ToString("dd-MMM-yyyy hh:mm tt") %>
                                                            </div>
                                                            <div class="m-messenger__message-text">
                                                                <%#Eval("ActivityPerform")%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--<div class="m-messenger__message m-messenger__message--out">m-messenger__message m-messenger__message--in
                                                    <div class="m-messenger__message-body">
                                                        <div class="m-messenger__message-arrow"></div>
                                                        <div class="m-messenger__message-content">
                                                            <div class="m-messenger__message-text">
                                                                Hi Megan. It's at 2.30PM
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>
                            <div class="m-messenger__seperator"></div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSubmit">
                                        <div class="m-messenger__form">
                                            <div class="m-messenger__form-controls">
                                                <%--<input type="text" name="" placeholder="Type here..." class="m-messenger__form-input">--%>
                                                <asp:TextBox ID="txtComent" runat="server" placeholder="Type here..." class="m-messenger__form-input"></asp:TextBox>
                                            </div>
                                            <div class="m-messenger__form-tools">
                                                <asp:LinkButton ID="btnSubmit" runat="server" class="m-messenger__form-attachment" OnClick="btnSubmit_Click"><i class="fa fa-send"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="tab-pane  m-scrollable" id="m_quick_sidebar_tabs_logs" role="tabpanel">
                        <div class="m-list-timeline">
                            <div class="table-responsive">
                                <table class="table table-striped table-hover table-bordered">
                                    <thead>
                                        <tr>

                                            <th>Log Name</th>
                                            <th>User</th>
                                            <th>Date Time</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="listuserlog" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("AuditType") %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" Text='<%#getname(Convert.ToInt32( Eval("CreatedUserName")))%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label17" runat="server" Text='<%# Eval("CreatedDate") %>'></asp:Label></td>

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
            <!-- end::Quick Sidebar -->
            <!-- begin::Scroll Top -->
            <div class="m-scroll-top m-scroll-top--skin-top" data-toggle="m-scroll-top" data-scroll-offset="500" data-scroll-speed="300">
                <i class="la la-arrow-up"></i>
            </div>
            <!-- end::Scroll Top -->
            <!-- begin::Quick Nav -->
            <ul class="m-nav-sticky" style="margin-top: 30px;">
                <!--
			<li class="m-nav-sticky__item" data-toggle="m-tooltip" title="Showcase" data-placement="left">
				<a href="">
					<i class="la la-eye"></i>
				</a>
			</li>
			<li class="m-nav-sticky__item" data-toggle="m-tooltip" title="Pre-sale Chat" data-placement="left">
				<a href="" >
					<i class="la la-comments-o"></i>
				</a>
			</li>
			-->
                <li class="m-nav-sticky__item" data-toggle="m-tooltip" title="Purchase" data-placement="left">
                    <a href="#" target="_blank">
                        <i class="la la-cart-arrow-down"></i>
                    </a>
                </li>
                <li class="m-nav-sticky__item" data-toggle="m-tooltip" title="Documentation" data-placement="left">
                    <a href="#" target="_blank">
                        <i class="flaticon-settings"></i>
                    </a>
                </li>
                <li class="m-nav-sticky__item" data-toggle="m-tooltip" title="Support" data-placement="left">
                    <a href="#" target="_blank">
                        <i class="la la-battery-3"></i>
                    </a>
                </li>
            </ul>
            <!-- begin::Quick Nav -->
    </form>

    <!--begin::Base Scripts -->
    <script src="../assetsP/vendors/base/vendors.bundle.js" type="text/javascript"></script>
    <script src="../assetsP/demo/demo2/base/scripts.bundle.js" type="text/javascript"></script>
    <!--end::Base Scripts -->
    <!--begin::Page Snippets -->
    <script src="../assetsP/app/js/dashboard.js" type="text/javascript"></script>
    <!--end::Page Snippets -->
</body>
<!-- end::Body -->
</html>

