<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="campaignuser.aspx.cs" Inherits="Web.CRM.campaignuser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-12">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet box blue" id="form_wizard_1">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-gift"></i><span class="step-title"></span>
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
                                <div class="table-scrollable">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                <thead>
                                                    <tr>


                                                        <th>
                                                            <asp:Label runat="server" ID="Label1" Text="Username" CssClass="form-control"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label2" Text="Campaign Type" CssClass="form-control"></asp:Label></th>

                                                        <th style="width: 60px;">ACTION</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview2" runat="server" OnItemCommand="Listview2_ItemCommand" OnItemDataBound="Listview2_ItemDataBound">
                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>

                                                                <td>
                                                                    <asp:Label ID="lblnation_code" runat="server" Text='<%# Eval("FIRST_NAME")%>'></asp:Label></td>
                                                                <td>

                                                                    <asp:DropDownList ID="drptype" runat="server" AutoPostBack="true">
                                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="Spend Based Campaign"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="Product Based Campaign"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbluser" Visible="false" runat="server" Text='<%# Eval("USER_ID")%>'></asp:Label>
                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("USER_ID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom" Text="Apply"></asp:LinkButton></td>

                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("USER_ID")%>' runat="server" Visible="false" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                            <%-- <td><asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("JobId")%>' CommandName="btnPrint" CommandArgument='<%# Eval("JobId")%>' runat="server" class="btn btn-sm green filter-submit margin-bottom"><i class="fa fa-print"></i></asp:LinkButton></td>--%>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>


                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>





</asp:Content>
