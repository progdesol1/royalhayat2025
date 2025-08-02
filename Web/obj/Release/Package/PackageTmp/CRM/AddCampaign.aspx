<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="AddCampaign.aspx.cs" Inherits="Web.CRM.AddCampaign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="row">
        <div class="col-md-12">
            <!-- BEGIN EXAMPLE TABLE PORTLET-->
            <div class="portlet box blue">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-edit"></i>Campaign List
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                        <a href="javascript:;" class="remove"></a>
                    </div>
                    <div class="actions btn-set">
                        <asp:Button ID="btnadd" runat="server" Text="Add Campaign" CssClass="btn btn-sm green-jungle" OnClick="btnadd_Click" Style="left: 945px;" type="submit" />
                    </div>
                </div>
                <div class="portlet-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                <thead>
                                    <tr>


                                        <th>
                                            <asp:Label runat="server" ID="Label1" Text="CampaignName"></asp:Label></th>
                                        <th>
                                            <asp:Label runat="server" ID="Label2" Text="Redemptions"></asp:Label></th>
                                        <th>
                                            <asp:Label runat="server" ID="Label3" Text="Status"></asp:Label></th>

                                        <th style="width: 60px;">ACTION</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="Listview2" runat="server" OnItemCommand="Listview1_ItemCommand">
                                        <LayoutTemplate>
                                            <tr id="ItemPlaceholder" runat="server">
                                            </tr>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td>
                                                    <asp:Label ID="lblnation_code" runat="server" Text='<%# Eval("CampaignName")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblemp_mobile" runat="server" Text='<%# Eval("CIsMultiple")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblemp_work_telephone" runat="server" Text='<%# Eval("CActive")%>'></asp:Label></td>

                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("CampaignID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                            <td>
                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("CampaignID")%>' runat="server" Visible="false" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
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
            <!-- END EXAMPLE TABLE PORTLET-->
        </div>
    </div>
</asp:Content>
