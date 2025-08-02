<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewTickit.ascx.cs" Inherits="Web.UserControl.NewTickit" %>
<div class="page-head">
    <!-- BEGIN PAGE TITLE -->
    <div class="page-title">
        <h1>Ticket Creat
                                <small></small>
        </h1>
    </div>
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

    <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
        <div class="alert alert-success alert-dismissable">
            <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
    </asp:Panel>
    <!-- END PAGE TOOLBAR -->
</div>

<!-- END PAGE HEAD-->
<!-- BEGIN PAGE BREADCRUMB -->
<ul class="page-breadcrumb breadcrumb">
    <li>
        <a href="index.html">Home</a>
        <i class="fa fa-circle"></i>
    </li>
    <li>
        <span class="active">Ticket Creat</span>
    </li>
</ul>
<!-- END PAGE BREADCRUMB -->
<!-- BEGIN PAGE BASE CONTENT -->
<div class="row">
    <div class="col-md-12">
        <!-- BEGIN TODO SIDEBAR -->
        <div class="todo-ui">
            <div class="todo-sidebar">
                <%-- <asp:UpdatePanel runat="server" ID="UpdatePanel2" class="item">
                            <ContentTemplate>--%>
                <asp:Panel ID="pnlTags" runat="server">
                    <div class="portlet light bordered">
                        <div class="portlet-title">
                            <div class="caption" data-toggle="collapse" data-target=".todo-project-list-content-tags">
                                <span class="caption-subject font-red bold uppercase">
                                    <asp:Label ID="lblTagtitle" runat="server"></asp:Label>
                                    TAGS </span>
                                <asp:Label ID="lbltenetid" Visible="false" runat="server"></asp:Label>
                                <span class="caption-helper visible-sm-inline-block visible-xs-inline-block">click to view</span>
                            </div>
                            <%--<div class="actions">
                                                <div class="actions">
                                                    <a class="btn btn-circle grey-salsa btn-outline btn-sm" href="javascript:;">
                                                        <i class="fa fa-plus"></i>Add </a>
                                                </div>
                                            </div>--%>
                        </div>


                        <div class="portlet-body todo-project-list-content todo-project-list-content-tags">
                            <div class="todo-project-list">

                                <ul class="nav nav-pills nav-stacked">
                                    <li>

                                        <asp:LinkButton ID="linkNew" runat="server" OnClick="linkNew_Click" Style="color: #65a0d0">
                                            <span class="badge badge-danger">
                                                <asp:Label ID="lblNew" runat="server"></asp:Label>
                                            </span>New
                                        </asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="linkPending_Click" Style="color: #65a0d0">
                                            <span class="badge badge-danger">
                                                <asp:Label ID="lblPending" runat="server"></asp:Label>
                                            </span>Pending
                                        </asp:LinkButton>

                                    </li>
                                    <li>
                                        <asp:LinkButton ID="linkCOmplet" runat="server" OnClick="linkCOmplet_Click" Style="color: #65a0d0">
                                            <span class="badge badge-info">
                                                <asp:Label ID="lblCompleted" runat="server"></asp:Label>
                                            </span>Completed
                                        </asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="linkProgress" runat="server" OnClick="linkProgress_Click" Style="color: #65a0d0">
                                            <span class="badge badge-success">
                                                <asp:Label ID="lblProgress" runat="server"></asp:Label>
                                            </span>In Progress
                                        </asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="linkClosed" runat="server" OnClick="linkClosed_Click" Style="color: #65a0d0">
                                            <span class="badge badge-warning">
                                                <asp:Label ID="lblClosed" runat="server"></asp:Label></span>Closed
                                        </asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="linkDeliverd" runat="server" OnClick="linkDeliverd_Click" Style="color: #65a0d0">
                                            <span class="badge badge-info">
                                                <asp:Label ID="lblDelivred" runat="server"></asp:Label>
                                            </span>Delivered
                                        </asp:LinkButton>
                                    </li>
                                </ul>

                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <div class="portlet light bordered">
                    <div class="portlet-title">
                        <div class="caption" data-toggle="collapse" data-target=".todo-project-list-content">
                            <span class="caption-subject font-green-sharp bold uppercase">Total Ticket </span>
                            <span class="caption-helper visible-sm-inline-block visible-xs-inline-block">click to view New Ticket  list</span>
                        </div>
                        <div class="actions">
                            <div class="btn-group">
                                <a class="btn green btn-circle btn-outline btn-sm todo-projects-config" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                                    <i class="icon-settings"></i>&nbsp;
                                                        <i class="fa fa-angle-down"></i>
                                </a>
                                <ul class="dropdown-menu pull-right">
                                    <li>
                                        <a href="javascript:;">New Tikit </a>
                                    </li>
                                    <li class="divider"></li>
                                    <li>
                                        <a href="javascript:;">Pending
                                                                <span class="badge badge-danger">4 </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">Completed
                                                                <span class="badge badge-success">12 </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">Overdue
                                                                <span class="badge badge-warning">9 </span>
                                        </a>
                                    </li>
                                    <li class="divider"></li>
                                    <li>
                                        <a href="javascript:;">Archived Projects </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <div class="portlet-body todo-project-list-content">
                        <div class="todo-project-list">
                            <ul class="nav nav-stacked">
                                <li>
                                    <asp:LinkButton ID="linkAllNew" runat="server" OnClick="linkAllNew_Click" Style="color: #65a0d0">
                                        <span class="badge badge-danger">
                                            <asp:Label ID="leblANew" runat="server"></asp:Label>
                                        </span>New
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="linkAllPending" runat="server" OnClick="linkAllPending_Click" Style="color: #65a0d0">
                                        <span class="badge badge-danger">
                                            <asp:Label ID="leblANew1" runat="server"></asp:Label>
                                        </span>Pending
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="linkallComplet" runat="server" OnClick="linkallComplet_Click" Style="color: #65a0d0">
                                        <span class="badge badge-info">
                                            <asp:Label ID="leblANew2" runat="server"></asp:Label>
                                        </span>Completed
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="linkAllProgress" runat="server" OnClick="linkAllProgress_Click" Style="color: #65a0d0">
                                        <span class="badge badge-success">
                                            <asp:Label ID="leblANew3" runat="server"></asp:Label>
                                        </span>In Progress 
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="linkAllClose" runat="server" OnClick="linkAllClose_Click" Style="color: #65a0d0">
                                        <span class="badge badge-warning">
                                            <asp:Label ID="leblANew4" runat="server"></asp:Label></span>Closed
                                    </asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="linkAllDeliverd" runat="server" OnClick="linkAllDeliverd_Click" Style="color: #65a0d0">
                                        <span class="badge badge-info">
                                            <asp:Label ID="leblANew5" runat="server"></asp:Label>
                                        </span>Delivered 
                                    </asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="portlet light bordered">

                    <div class="portlet-title">
                        <div class="caption" data-toggle="collapse" data-target=".todo-project-list-content">
                            <span class="caption-subject font-green-sharp bold uppercase">Module Name </span>
                            <span class="caption-helper visible-sm-inline-block visible-xs-inline-block">click to view project list</span>
                        </div>
                        <div class="actions">
                            <div class="btn-group">
                                <a class="btn green btn-circle btn-outline btn-sm todo-projects-config" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                                    <i class="icon-settings"></i>&nbsp;
                                                        <i class="fa fa-angle-down"></i>
                                </a>
                                <ul class="dropdown-menu pull-right">
                                    <li>
                                        <a href="javascript:;">New Project </a>
                                    </li>
                                    <li class="divider"></li>
                                    <li>
                                        <a href="javascript:;">Pending
                                                                <span class="badge badge-danger">4 </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">Completed
                                                                <span class="badge badge-success">12 </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">Overdue
                                                                <span class="badge badge-warning">9 </span>
                                        </a>
                                    </li>
                                    <li class="divider"></li>
                                    <li>
                                        <a href="javascript:;">Archived Projects </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="portlet-body todo-project-list-content">
                        <div class="todo-project-list">

                            <asp:ListView ID="ltsProduct" runat="server" OnItemCommand="ltsProduct_ItemCommand">

                                <ItemTemplate>
                                    <ul class="nav nav-stacked">
                                        <li>
                                            <asp:LinkButton ID="linModul123" runat="server" CommandArgument=' <%# Eval("Module_Id")%>' CommandName="linModul123" Style="color: #65a0d0">
                                                            
                                                                <span class="badge badge-info"></span>
                                                                <%# Eval("Module_Name")%></asp:LinkButton>
                                            <asp:Label ID="lblHide" runat="server" Visible="false" Text=' <%# Eval("Module_Name")%>'></asp:Label>
                                        </li>

                                    </ul>
                                </ItemTemplate>
                            </asp:ListView>

                        </div>
                    </div>
                </div>


                <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
            </div>
            <!-- END TODO SIDEBAR -->
            <!-- BEGIN TODO CONTENT -->
            <div class="todo-content">
                <div class="portlet light bordered">
                    <!-- PROJECT HEAD -->
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="icon-bar-chart font-green-sharp hide"></i>
                            <asp:Label class="caption-subject font-red bold uppercase" ID="lblCompniName" runat="server"></asp:Label>
                            &nbsp;
                                               
                                    <asp:LinkButton ID="btnNewTickit1" runat="server" class="btn btn-circle btn-sm green " OnClick="btnNewTickit_Click"> <i class="fa fa-plus"></i> &nbsp;Open New Ticket</asp:LinkButton>&nbsp;&nbsp;
                             <span class="badge badge-success">

                                 <asp:Label ID="lblAttachment" runat="server"></asp:Label></span>
                        </div>
                        <div class="actions">
                            <div class="btn-group">


                                <asp:LinkButton ID="LinkButton1" class="btn green btn-circle btn-sm" runat="server" data-toggle="dropdown" data-hover="dropdown" data-close-others="true"> MANAGE<i class="fa fa-angle-down"></i></asp:LinkButton>

                                <ul class="dropdown-menu pull-right">
                                    <li>
                                        <a href="javascript:;">New Task </a>
                                    </li>
                                    <li class="divider"></li>
                                    <li>
                                        <a href="javascript:;">Pending
                                                                <span class="badge badge-danger">4 </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">Completed
                                                                <span class="badge badge-success">12 </span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">Overdue
                                                                <span class="badge badge-warning">9 </span>
                                        </a>
                                    </li>
                                    <li class="divider"></li>
                                    <li>
                                        <a href="javascript:;">Delete Project </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <!-- end PROJECT HEAD -->
                    <div class="portlet-body">
                        <div class="row">
                            <div class="col-md-5 col-sm-4">
                                <div class="todo-tasklist">
                                    <asp:UpdatePanel runat="server" ID="upnl" class="item">
                                        <ContentTemplate>
                                            <asp:ListView ID="ltsRemainderNotes" runat="server" OnItemCommand="ltsRemainderNotes_ItemCommand">

                                                <ItemTemplate>
                                                    <div class="todo-tasklist-item todo-tasklist-item-border-green">
                                                        <%--<img class="todo-userpic pull-left" src="<%#Eval("USERCODE")%>" width="27px" height="27px">--%>
                                                        <img class="todo-userpic" src="../CRM/images/No_image.png" width="27px" height="27px">
                                                        <div class="todo-tasklist-item-title">
                                                            <%#Eval("USERNAME")%>
                                                            <asp:LinkButton ID="btnclick123" class="btn blue" Style="margin-left: 50px; padding: 0px; border-right-width: 0px; margin-right: 30px;" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btnclick123" runat="server"> &nbsp; Reply &nbsp; </asp:LinkButton>
                                                            <span class="todo-tasklist-badge badge badge-roundless">Ti.No <%#Eval("MasterCODE")%> </span>
                                                        </div>
                                                        <div class="todo-tasklist-item-text"><%#Eval("Remarks")%> </div>
                                                        <asp:Label ID="tikitID" Visible="false" runat="server" Text='<%#Eval("MasterCODE")%>'></asp:Label>
                                                        <div class="todo-tasklist-controls pull-left">
                                                            <span class="todo-tasklist-date">
                                                                <i class="fa fa-calendar"></i><%# DateTime .Parse ( Eval("UPDTTIME").ToString ())%></span>
                                                            <span class="todo-tasklist-badge badge badge-roundless"><%#Eval("MyStatus")%> </span>
                                                            <span class="todo-tasklist-badge badge badge-roundless"><%#Eval("ACTIVITYE")%></span>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                            <div class="todo-tasklist-devider"></div>
                            <div class="col-md-7 col-sm-8">
                                <div class="form-horizontal">
                                    <!-- TASK HEAD -->
                                    <asp:Panel ID="pnlTicki" runat="server">
                                        <div class="form">
                                            <div class="form-group">
                                                <div class="col-md-8 col-sm-8" style="margin-bottom: 15px;">
                                                    <div class="todo-taskbody-user">
                                                        <asp:Image ID="imgUser" class="todo-userpic pull-left" Width="50px" Height="50px" runat="server" />
                                                        <%--<img  src="assets/pages/media/users/avatar6.jpg" >--%>
                                                        <span class="todo-username pull-left">
                                                            <asp:Label ID="lblUserName" runat="server"></asp:Label></span>
                                                        <%--<button type="button" class="todo-username-btn btn btn-circle btn-default btn-sm">&nbsp;edit&nbsp;</button>--%>
                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-sm-4" style="margin-bottom: 15px;">
                                                    <%--<div class="todo-taskbody-date pull-right">
                                                                <button type="button" class="todo-username-btn btn btn-circle btn-default btn-sm">&nbsp; Complete &nbsp;</button>
                                                            </div>--%>
                                                </div>
                                            </div>
                                            <!-- END TASK HEAD -->
                                            <!-- TASK TITLE -->
                                            <div class="form-group">
                                                <div class="col-md-4" style="margin-bottom: 15px;">
                                                    <b>
                                                        <asp:Label ID="Label4" runat="server" Text="Select Department:"></asp:Label></b>
                                                </div>
                                                <div class="col-md-8" style="margin-bottom: 15px;">

                                                    <asp:DropDownList ID="drpSDepartment" runat="server" class="form-control todo-taskbody-tags">
                                                        <asp:ListItem Value="0">----Select----</asp:ListItem>
                                                        <asp:ListItem Value="1">User</asp:ListItem>
                                                        <asp:ListItem Value="2">Support Level-1</asp:ListItem>
                                                        <asp:ListItem Value="3">Support Level-2</asp:ListItem>
                                                        <asp:ListItem Value="4">Support Level-3</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-4" style="margin-bottom: 15px;">
                                                    <b>
                                                        <asp:Label ID="Label5" runat="server" Text="Priority :"></asp:Label></b>
                                                </div>
                                                <div class="col-md-8" style="margin-bottom: 15px;">

                                                    <asp:DropDownList ID="drppriority" runat="server" class="form-control todo-taskbody-tags">
                                                        <asp:ListItem Value="0">----Select----</asp:ListItem>
                                                        <asp:ListItem Value="Low">Low</asp:ListItem>
                                                        <asp:ListItem Value="Medium">Medium</asp:ListItem>
                                                        <asp:ListItem Value="High">High</asp:ListItem>
                                                        <asp:ListItem Value="Urgen">Urgen</asp:ListItem>
                                                        <asp:ListItem Value="Emergency">Emergency</asp:ListItem>
                                                        <asp:ListItem Value="Critical">Critical</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-4" style="margin-bottom: 15px;">
                                                    <b>
                                                        <asp:Label ID="Label1" runat="server" Text="Activity Name  :"></asp:Label></b>
                                                </div>

                                                <div class="col-md-8" style="margin-bottom: 15px;">

                                                    <asp:DropDownList ID="drpActivityName" runat="server" class="form-control todo-taskbody-tags">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-4" style="margin-bottom: 15px;">
                                                    <b>
                                                        <asp:Label ID="Label2" runat="server" Text="Subject :"></asp:Label></b>
                                                </div>
                                                <div class="col-md-8" style="margin-bottom: 15px;">

                                                    <asp:TextBox ID="txtSubject" runat="server" placeholder="Wirte The Messaging" class="form-control todo-taskbody-due">
                                                    </asp:TextBox>
                                                </div>
                                            </div>

                                            <!-- TASK DESC -->
                                            <div class="form-group">
                                                <div class="col-md-4" style="margin-bottom: 15px;">
                                                    <b>
                                                        <asp:Label ID="Label3" runat="server" Text=" Message : "></asp:Label></b>
                                                </div>
                                                <div class="col-md-8" style="margin-bottom: 15px;">

                                                    <asp:TextBox ID="txtMessage" runat="server" placeholder="Wirte The Messaging" class="form-control todo-taskbody-taskdesc" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                                    <%--<textarea class="form-control todo-taskbody-taskdesc" rows="8" placeholder="Task Description..."></textarea>--%>
                                                </div>
                                            </div>


                                            <div class="form-actions right todo-form-actions">
                                                <asp:Button ID="btnSave" runat="server" class="btn btn-circle btn-sm green" OnClick="btnSave_Click" Text="Save Changes" />
                                                <asp:Button ID="btnCancel" runat="server" class="btn btn-circle btn-sm btn-default" OnClick="btnCancel_Click" Text="Cancel" />
                                                <%-- <button class="btn btn-circle btn-sm green">Save Changes</button>
                                                    <button class="btn btn-circle btn-sm btn-default">Cancel</button>--%>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="panChat" runat="server" DefaultButton="btnSubmit">
                                        <asp:Timer ID="Timer1" runat="server" Interval="18000" OnTick="Timer1_Tick">
                                        </asp:Timer>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" class="item" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="tabbable-line">
                                                    <ul class="nav nav-tabs ">
                                                        <li class="active">
                                                            <a href="#tab_1" data-toggle="tab">Comments </a>
                                                        </li>
                                                        <li>
                                                            <a href="#tab_2" data-toggle="tab">History </a>
                                                        </li>
                                                    </ul>
                                                    <div class="tab-content">
                                                        <div class="tab-pane active" id="tab_1">
                                                            <!-- TASK COMMENTS -->
                                                            <div class="form-group">
                                                                <div class="col-md-12">

                                                                    <asp:ListView ID="listChet" runat="server">

                                                                        <ItemTemplate>
                                                                            <ul class="media-list">
                                                                                <li class="media">
                                                                                    <a class="pull-left" href="javascript:;">
                                                                                        <%--<img class="todo-userpic" src="<%# Eval("USERCODEENTERED")%>" width="27px" height="27px">--%>
                                                                                        <img class="todo-userpic" src="../CRM/images/No_image.png" width="27px" height="27px">
                                                                                    </a>
                                                                                    <div class="media-body todo-comment">
                                                                                        <%-- <button type="button" class="todo-comment-btn btn btn-circle btn-default btn-sm">&nbsp; Reply &nbsp;</button>--%>
                                                                                        <p class="todo-comment-head">
                                                                                            <span class="todo-comment-username"><%#Eval("Version")%></span> &nbsp;
                                                                                            <span class="todo-comment-date"><%# DateTime .Parse ( Eval("UPDTTIME").ToString ())%></span>
                                                                                        </p>
                                                                                        <p class="todo-text-color">
                                                                                            <%#Eval("ActivityPerform")%>
                                                                                        </p>
                                                                                        <!-- Nested media object -->
                                                                                        <%--<div class="media">
                                                                                                    <a class="pull-left" href="javascript:;">
                                                                                                        <img class="todo-userpic" src="assets/pages/media/users/avatar4.jpg" width="27px" height="27px">
                                                                                                    </a>
                                                                                                    <div class="media-body">
                                                                                                        <p class="todo-comment-head">
                                                                                                            <span class="todo-comment-username"><%#Eval("Version")%></span> &nbsp;
                                                                                                    <span class="todo-comment-date"><%# DateTime .Parse ( Eval("UPDTTIME").ToString ())%></span>
                                                                                                        </p>
                                                                                                        <p class="todo-text-color"><%#Eval("ActivityPerform")%> </p>
                                                                                                    </div>
                                                                                                </div>--%>
                                                                                    </div>
                                                                                </li>
                                                                            </ul>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>


                                                                </div>
                                                            </div>
                                                            <!-- END TASK COMMENTS -->
                                                            <!-- TASK COMMENT FORM -->
                                                            <div class="form-group">
                                                                <div class="col-md-12">
                                                                    <ul class="media-list">
                                                                        <li class="media">
                                                                            <%--<a class="pull-left" href="javascript:;">
                                                                                        <img class="todo-userpic" src="assets/pages/media/users/avatar4.jpg" width="27px" height="27px">
                                                                                    </a>--%>
                                                                            <div class="media-body">

                                                                                <asp:TextBox ID="txtComent" runat="server" placeholder="Type comment..." Rows="4" class="form-control todo-taskbody-taskdesc" TextMode="MultiLine"></asp:TextBox>
                                                                            </div>
                                                                            <div class="form-group" style="padding-top: 20px; margin-right: 0px;">
                                                                                <div class="col-md-4" style="margin-bottom: 15px; padding-left: 20px;">
                                                                                    <b>
                                                                                        <asp:Label ID="Label6" runat="server" Text="Status:"></asp:Label></b>
                                                                                </div>

                                                                                <div class="col-md-8" style="margin-bottom: 15px;">

                                                                                    <asp:DropDownList ID="drpStatus" runat="server" class="form-control todo-taskbody-tags">
                                                                                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                                                        <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                                                                        <asp:ListItem Value="Progress">Progress</asp:ListItem>
                                                                                        <asp:ListItem Value="Closed">Closed</asp:ListItem>
                                                                                        <asp:ListItem Value="Delivered">Delivered</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </li>
                                                                    </ul>
                                                                    <div class="form">
                                                                        <div class="form-actions right todo-form-actions">
                                                                            <asp:LinkButton ID="lbllblattachments2" class="btn btn-sm btn-circle green" runat="server" Text="Attachments" OnClick="lbllblattachments2_Click"></asp:LinkButton>
                                                                            <%--<span class="badge badge-success">
                                                                                <asp:Label ID="lblAttach2" runat="server" Text="0"></asp:Label></span>--%>
                                                                            <asp:Button ID="btnAttch" runat="server" Text="0" CssClass="btn btn-sm btn-circle green" OnClick="btnAttch_Click" />
                                                                            <asp:Button ID="btnSubmit" runat="server" class="btn btn-sm btn-circle green" OnClick="btnSubmit_Click" Text="Submit" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!-- END TASK COMMENT FORM -->
                                                        </div>
                                                        <div class="tab-pane" id="tab_2">
                                                            <asp:ListView ID="ListHistoy" runat="server">
                                                                <ItemTemplate>
                                                                    <ul class="todo-task-history">
                                                                        <li>
                                                                            <div class="todo-task-history-date"><%# DateTime .Parse ( Eval("UPDTTIME").ToString ())%> </div>
                                                                            <div class="todo-task-history-desc"><%#Eval("Version")%> </div>
                                                                        </li>
                                                                    </ul>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END TODO CONTENT -->
        </div>
    </div>
</div>
<!-- END PAGE CONTENT-->
