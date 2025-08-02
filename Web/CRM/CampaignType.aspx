<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="CampaignType.aspx.cs" Inherits="Web.CRM.CampaignType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-gift"></i>Campaign - Type
                    </div>
                    <div class="tools hidden-xs">
                        <a href="javascript:;" class="collapse"></a>
                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                        <a href="javascript:;" class="reload"></a>
                        <a href="javascript:;" class="remove"></a>
                    </div>
                </div>
                <div class="portlet-body form">

                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="row">
                                <h4 style="margin-left: 76px;">Select type of Campaign</h4>
                                <br />
                                <div class="col-md-6">
                                    <asp:Button ID="btntype1" runat="server" Text="Spend Based Campaign" CssClass="btn btn-default" Style="height: 100px; width: 400px; left: 66px;" OnClick="btntype1_Click" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="btntype2" runat="server" Text="Product Based Campaign" CssClass="btn btn-default" Style="height: 100px; width: 400px;" OnClick="btntype2_Click" />
                                </div>

                            </div>


                        </div>
                    </div>
                </div>
                <!-- END PAGE TOOLBAR -->
            </div>
            <!-- END PAGE HEAD -->
        </div>
    </div>
</asp:Content>

