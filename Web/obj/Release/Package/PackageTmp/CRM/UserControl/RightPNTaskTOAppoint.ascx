<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RightPNTaskTOAppoint.ascx.cs" Inherits="Web.CRM.UserControl.RightPNTaskTOAppoint" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="row">
    <div class="portlet box blue">
        <div class="portlet-title">
            <div class="caption">
                <i class="fa fa-gift"></i> Appointment Tools
            </div>
            <div class="tools">
                <a href="javascript:;" class="collapse"></a>
                <a href="#portlet-config" data-toggle="modal" class="config"></a>
            </div>
        </div>
        <div class="portlet-body">

            <div id="Appoint1" class="panel-group">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#Appoint1" href="#Fax1_1">1.Fax 
                                <asp:Label runat="server" ID="lblnotecount" CssClass="label label-default" Text="0"></asp:Label>&nbsp;<i class="fa fa-fax"></i>
                            </a>
                        </h4>

                    </div>
                    <div id="Fax1_1" class="panel-collapse collapse in">
                        <div class="row">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <div class="col-md-12">
                                <div class="form-group" style="margin-right: -0px; margin-left: 10px; margin-top: 10px;">
                                    <asp:TextBox ID="tagsFax" runat="server" CssClass="form-control tabs"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-5" style="float: right">
                                <div class="form-group" style="color: ">
                                    <asp:Button ID="btnAddNote" class="btn default" runat="server" Text="Add Fax" CausesValidation="false" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#Appoint1" href="#Mail1_2">2.Mail  
                                <asp:Label runat="server" ID="lblticketcount" Text="0" CssClass="label label-default"></asp:Label></a>&nbsp;<i class="fa fa-mail-reply-all"></i>
                        </h4>
                    </div>
                    <div id="Mail1_2" class="panel-collapse collapse">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group" style="margin-right: -0px; margin-left: 10px; margin-top: 10px;">
                                    <asp:TextBox ID="txtMail" runat="server" CssClass="form-control tabs"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-5" style="float: right">
                                <div class="form-group" style="color: ">
                                    <asp:Button ID="btnaddticket" class="btn default" runat="server" Text="Add Mail" CausesValidation="false" />
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#Appoint1" href="#WhatUP1_3">3. WhatsUp 
                                <asp:Label runat="server" ID="lblfiles" Text="0" CssClass="label label-default"></asp:Label></a>&nbsp;<i class="fa fa-whatsapp"></i>
                        </h4>
                    </div>
                    <div id="WhatUP1_3" class="panel-collapse collapse">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group" style="margin-right: -0px; margin-left: 10px; margin-top: 10px;">
                                    <asp:TextBox ID="txtWhatsup" runat="server" CssClass="form-control tabs"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-5" style="float: right">
                                <div class="form-group" style="color: ">
                                    <asp:Button ID="btnsavefile" runat="server" class="btn green-haze btn-circle" Text="WhatsUp No." CausesValidation="false" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#Appoint1" href="#SocialMedia1_4">4. Social Media 
                                <asp:Label runat="server" ID="lblEmail" Text="0" CssClass="label label-default"></asp:Label></a>&nbsp;<i class="fa fa-mobile"></i>
                        </h4>
                    </div>
                    <div id="SocialMedia1_4" class="panel-collapse collapse">
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group" style="margin-right: -0px; margin-left: 10px; margin-top: 10px;">
                                        <asp:TextBox ID="txtSocialMedia" runat="server" CssClass="form-control tabs"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-5" style="float: right">
                                    <div class="form-group" style="color: ">
                                        <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle" Text="Social Media" CausesValidation="false" />
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
