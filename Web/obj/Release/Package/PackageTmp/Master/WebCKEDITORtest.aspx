<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="WebCKEDITORtest.aspx.cs" Inherits="Web.Master.WebCKEDITORtest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal form-row-seperated">
                <div class="portlet box blue">
                    <div class="portlet-title">
                        <div class="caption">
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="portlet-body form">
                            <div class="tabbable">
                                <div class="tab-content no-space">
                                    <div class="tab-pane active" id="tab_general1">
                                        <div class="form-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblDescription1s" class="col-md-2 control-label" Text="Description:"></asp:Label>
                                                        <div class="col-md-10">
                                                            <CKEditor:CKEditorControl runat="server" ID="CKEProDtlOverview" Width="" Height="200px"></CKEditor:CKEditorControl>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Button ID="btntestCK" runat="server" Text="ok" CssClass="btn btn-lg red" OnClick="btntestCK_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box green-haze">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-globe"></i>
                                        <asp:Label runat="server" ID="Label5" Text="demo"></asp:Label>
                                        List
                                    </div>
                                    <div class="tools">
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <table class="table table-striped table-bordered table-hover" id="sample_1">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:CheckBox ID="CheckBox1" CssClass="all" runat="server" onclick="PopClear1()" />
                                                </th>
                                                <th>
                                                    <asp:Label runat="server" ID="lblhUOMNAME1" Text="Description"></asp:Label></th>

                                                <th style="width: 60px;">ACTION</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand">


                                                <LayoutTemplate>
                                                    <tr id="ItemPlaceholder" runat="server">
                                                    </tr>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="CheckBox2" CssClass="one" runat="server" />
                                                        </th>
                                                        <td>
                                                            <asp:Label ID="lblUOMNAME1" runat="server" Text='<%# Eval("Address")%>'></asp:Label></td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("id")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                    <td>
                                                                        <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("id")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                </tr>
                                                            </table>
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

    <script type="text/javascript" src="../assets/global/plugins/ckeditor/ckeditor.js"></script>
</asp:Content>
