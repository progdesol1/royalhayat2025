<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="ttt.aspx.cs" Inherits="Web.ACM.ttt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/bootstrap-toastr/toastr.min.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrapper">
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
					<h1>Toastr Notifications <small>gnome & growl type non-blocking notifications</small></h1>
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
										<li>
											 Theme Style
											<select class="layout-style-option form-control input-small input-sm">
												<option value="square" selected="selected">Square corners</option>
												<option value="rounded">Rounded corners</option>
											</select>
										</li>
										<li>
											 Layout
											<select class="layout-option form-control input-small input-sm">
												<option value="fluid" selected="selected">Fluid</option>
												<option value="boxed">Boxed</option>
											</select>
										</li>
										<li>
											 Header
											<select class="page-header-option form-control input-small input-sm">
												<option value="fixed" selected="selected">Fixed</option>
												<option value="default">Default</option>
											</select>
										</li>
										<li>
											 Top Dropdowns
											<select class="page-header-top-dropdown-style-option form-control input-small input-sm">
												<option value="light">Light</option>
												<option value="dark" selected="selected">Dark</option>
											</select>
										</li>
										<li>
											 Sidebar Mode
											<select class="sidebar-option form-control input-small input-sm">
												<option value="fixed">Fixed</option>
												<option value="default" selected="selected">Default</option>
											</select>
										</li>
										<li>
											 Sidebar Menu
											<select class="sidebar-menu-option form-control input-small input-sm">
												<option value="accordion" selected="selected">Accordion</option>
												<option value="hover">Hover</option>
											</select>
										</li>
										<li>
											 Sidebar Position
											<select class="sidebar-pos-option form-control input-small input-sm">
												<option value="left" selected="selected">Left</option>
												<option value="right">Right</option>
											</select>
										</li>
										<li>
											 Footer
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
					<a href="#">UI Features</a>
					<i class="fa fa-circle"></i>
				</li>
				<li>
					<a href="#">Toastr Notifications</a>
				</li>
			</ul>
			<!-- END PAGE BREADCRUMB -->
			<!-- END PAGE HEADER-->
			<!-- BEGIN PAGE CONTENT-->
			<div class="row">
				<div class="col-md-12">
					<div class="portlet yellow box">
						<div class="portlet-title">
							<div class="caption">
								<i class="fa fa-cogs"></i>Toastr Notification Demo
							</div>
							<div class="tools">
								<a href="javascript:;" class="collapse">
								</a>
								<a href="#portlet-config" data-toggle="modal" class="config">
								</a>
								<a href="javascript:;" class="reload">
								</a>
								<a href="javascript:;" class="remove">
								</a>
							</div>
						</div>
						<div class="portlet-body">
							<div class="row">
								<div class="col-md-3">
									<div class="form-group">
										<label class="control-label" for="title">Title</label>
										<input id="title" type="text" class="form-control" value="Toastr Notifications" placeholder="Enter a title ..."/>
									</div>
									<div class="form-group">
										<label class="control-label" for="message">Message</label>
										<textarea class="form-control" id="message" rows="3" placeholder="Enter a message ...">Gnome & Growl type non-blocking notifications</textarea>
									</div>
									<div class="form-group">
										<div class="checkbox-list">
											<label for="closeButton">
											<input id="closeButton" type="checkbox" value="checked" checked class="input-small"/>Close Button </label>
											<label for="addBehaviorOnToastClick">
											<input id="addBehaviorOnToastClick" type="checkbox" value="checked" class="input-small"/>Add behavior on toast click </label>
											<label for="debugInfo">
											<input id="debugInfo" type="checkbox" value="checked" class="input-small"/>Debug </label>
										</div>
									</div>
								</div>
								<div class="col-md-3">
									<div class="form-group" id="toastTypeGroup">
										<label>Toast Type</label>
										<div class="radio-list">
											<label>
											<input type="radio" name="toasts" value="success" checked/>Success </label>
											<label>
											<input type="radio" name="toasts" value="info"/>Info </label>
											<label>
											<input type="radio" name="toasts" value="warning"/>Warning </label>
											<label>
											<input type="radio" name="toasts" value="error"/>Error </label>
										</div>
									</div>
									<div class="form-group" id="positionGroup">
										<label>Position</label>
										<div class="radio-list">
											<label>
											<input type="radio" name="positions" value="toast-top-right" checked/>Top Right </label>
											<label>
											<input type="radio" name="positions" value="toast-bottom-right"/>Bottom Right </label>
											<label>
											<input type="radio" name="positions" value="toast-bottom-left"/>Bottom Left </label>
											<label>
											<input type="radio" name="positions" value="toast-top-left"/>Top Left </label>
											<label>
											<input type="radio" name="positions" value="toast-top-center"/>Top Center </label>
											<label>
											<input type="radio" name="positions" value="toast-bottom-center"/>Bottom Center </label>
											<label>
											<input type="radio" name="positions" value="toast-top-full-width"/>Top Full Width </label>
											<label>
											<input type="radio" name="positions" value="toast-bottom-full-width"/>Bottom Full Width </label>
										</div>
									</div>
								</div>
								<div class="col-md-3">
									<div class="form-group">
										<div class="controls">
											<label class="control-label" for="showEasing">Show Easing</label>
											<input id="showEasing" type="text" placeholder="swing, linear" class="form-control input-small" value="swing"/>
											<label class="control-label" for="hideEasing">Hide Easing</label>
											<input id="hideEasing" type="text" placeholder="swing, linear" class="form-control input-small" value="linear"/>
											<label class="control-label" for="showMethod">Show Method</label>
											<input id="showMethod" type="text" placeholder="show, fadeIn, slideDown" class="form-control input-small" value="fadeIn"/>
											<label class="control-label" for="hideMethod">Hide Method</label>
											<input id="hideMethod" type="text" placeholder="hide, fadeOut, slideUp" class="form-control input-small" value="fadeOut"/>
										</div>
									</div>
								</div>
								<div class="col-md-3">
									<div class="form-group">
										<div class="controls">
											<label class="control-label" for="showDuration">Show Duration</label>
											<input id="showDuration" type="text" placeholder="ms" class="form-control input-small" value="1000"/>
											<label class="control-label" for="hideDuration">Hide Duration</label>
											<input id="hideDuration" type="text" placeholder="ms" class="form-control input-small" value="1000"/>
											<label class="control-label" for="timeOut">Time out</label>
											<input id="timeOut" type="text" placeholder="ms" class="form-control input-small" value="5000"/>
											<label class="control-label" for="timeOut">Extended time out</label>
											<input id="extendedTimeOut" type="text" placeholder="ms" class="form-control input-small" value="1000"/>
										</div>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-12">
									<button type="button" class="btn green" id="showtoast">Show Toast</button>
									<button type="button" class="btn red" id="cleartoasts">Clear Toasts</button>
									<button type="button" class="btn red" id="clearlasttoast">Clear Last Toast</button>
								</div>
							</div>
							<div class="row margin-top-10">
								<div class="col-md-12">
									<pre id='toastrOptions'>
										 Settings...
									</pre>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			<!-- END PAGE CONTENT-->
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="../assets/global/plugins/bootstrap-toastr/toastr.min.js"></script>
     <script src="../assets/admin/pages/scripts/ui-toastr.js"></script>
</asp:Content>
