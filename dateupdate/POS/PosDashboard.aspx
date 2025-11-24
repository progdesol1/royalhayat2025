<%@ Page Title="" Language="C#" MasterPageFile="~/POS/POSMaster.Master" AutoEventWireup="true" CodeBehind="PosDashboard.aspx.cs" Inherits="Web.POS.PosDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="shortcut icon" href="favicon.ico" />
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="page-content-wrapper">
        <div class="page-content">
            <!-- BEGIN PAGE HEAD -->
            <div class="page-head">
                <!-- BEGIN PAGE TITLE -->
                <div class="page-title">
                    <h1>Dashboard <small>statistics & reports</small></h1>
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
                                        <li class="theme-color theme-color-default" data-theme="default">
                                            <span class="theme-color-view"></span>
                                            <span class="theme-color-name">Dark Header</span>
                                        </li>
                                        <li class="theme-color theme-color-light active" data-theme="light">
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
                                                <option value="square">Square corners</option>
                                                <option value="rounded" selected="selected">Rounded corners</option>
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
            <ul class="page-breadcrumb breadcrumb hide">
                <li>
                    <a href="javascript:;">Home</a><i class="fa fa-circle"></i>
                </li>
                <li class="active">Dashboard
                </li>
            </ul>
            <!-- END PAGE BREADCRUMB -->
            <!-- BEGIN PAGE CONTENT INNER -->
            <div class="row margin-top-10" style="margin-bottom: -10px;">
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding-right: 0px;">
                    <a href="PaymentReport.aspx">
                        <div class="dashboard-stat2">
                            <div class="display">
                                <div class="number">
                                    <%--<h3 class="font-green-sharp">7800<small class="font-green-sharp">$</small></h3>--%>
                                    <h4 class="font-green-sharp">
                                        <asp:Label ID="Label1" runat="server" Text="Date to Date Payment Report"></asp:Label></h4>
                                  
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
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding-right: 0px;">
                    <a href="salesreport.aspx">
                        <div class="dashboard-stat2">
                            <div class="display">
                                <div class="number">
                                    <h4 class="font-red-haze">
                                        <asp:Label ID="Label2" runat="server" Text="Date to Date Sale Report"></asp:Label></h4>
                                  
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
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" style="padding-right: 0px;">
                    <a href="Salespresetreport.aspx">
                        <div class="dashboard-stat2">
                            <div class="display">
                                <div class="number">
                                    <h4 class="font-blue-sharp">
                                        <asp:Label ID="Label3" runat="server" Text="Sale By Preset Discount"></asp:Label></h4>
                                
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
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <a href="ReportYearlySale.aspx">
                        <div class="dashboard-stat2">
                            <div class="display">
                                <div class="number">
                                    <h4 class="font-purple-soft">
                                        <asp:Label ID="Label4" runat="server" Text="Due Lists"></asp:Label></h4>
                                   
                                </div>
                                </div>
                            <div class="progress-info">
                                <div class="progress">
                                    <span style="width: 57%;" class="progress-bar progress-bar-success purple-soft">
                                        <span class="sr-only">56% change</span>
                                    </span>
                                </div>
                                <div class="status">
                                    <div class="status-title">
                                        change
                                    </div>
                                    <div class="status-number">
                                        57%
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
        
          
          
        </div>
    </div>

</asp:Content>
