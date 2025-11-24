<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="HelpDeskReport.aspx.cs" Inherits="Web.Master.HelpDeskReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="margin-left: 0px; margin-right: 10px;">
        <div class="col-md-6">
            <div class="portlet box blue">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-cogs"></i>Department
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                        <a href="javascript:;" class="reload"></a>
                        <a href="javascript:;" class="remove"></a>
                    </div>
                    
                </div>
                <div class="portlet-body">
                    <table class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <th>Department
                                </th>
                                <th style="width: 20%;">Count
                                </th>

                            </tr>
                        </thead>
                        <tbody>
                            <asp:ListView ID="ListView3" runat="server" OnItemDataBound="ListView3_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text='<%# GetDept(Convert.ToInt32(Eval("TickDepartmentID"))) %>'></asp:Label>
                                            <asp:Label ID="lblDeptID" Visible="false" runat="server" Text='<%# Eval("TickDepartmentID") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" CssClass="badge badge-primary" runat="server" Text="lll"></asp:Label>
                                        </td>

                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                        </tbody>
                    </table>

                </div>
            </div>
        </div>
        <%--    </div>
    <div class="row" style="margin-left: 0px;margin-right: 10px;">--%>
        <div class="col-md-6">
            <div class="portlet box yellow-crusta">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-cogs"></i>Location
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                        <a href="javascript:;" class="reload"></a>
                        <a href="javascript:;" class="remove"></a>
                    </div>
                </div>
                <div class="portlet-body">
                    <table class="table table-bordered table-striped">
                        <thead>
                            <tr>

                                <th>Location
                                </th>
                                <th style="width: 20%;">Count
                                </th>

                            </tr>
                        </thead>
                        <tbody>
                            <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
                                <ItemTemplate>
                                    <tr>

                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text='<%# GetLOC(Convert.ToInt32(Eval("TickPhysicalLocation"))) %>'></asp:Label>
                                            <asp:Label ID="lbllocid" Visible="false" runat="server" Text='<%# Eval("TickPhysicalLocation") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label4" CssClass="badge badge-warning" runat="server" Text="Label"></asp:Label>
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
    <div class="row" style="margin-left: 0px; margin-right: 10px;">
        <div class="col-md-6">
            <div class="portlet box green-jungle">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-cogs"></i>Category
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                        <a href="javascript:;" class="reload"></a>
                        <a href="javascript:;" class="remove"></a>
                    </div>
                </div>
                <div class="portlet-body">
                    <table class="table table-bordered table-striped">
                        <thead>
                            <tr>

                                <th>Category
                                </th>
                                <th style="width: 20%;">Count
                                </th>

                            </tr>
                        </thead>
                        <tbody>
                            <asp:ListView ID="ListView2" runat="server" OnItemDataBound="ListView2_ItemDataBound">
                                <ItemTemplate>
                                    <tr>

                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text='<%# GetCAT(Convert.ToInt32(Eval("TickCatID"))) %>'></asp:Label>
                                            <asp:Label ID="lblCatid" Visible="false" runat="server" Text='<%# Eval("TickCatID") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label6" CssClass="badge badge-success" runat="server" Text="Label"></asp:Label>
                                        </td>

                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                        </tbody>
                    </table>

                </div>
            </div>
        </div>
        <%--    </div>    
    <div class="row" style="margin-left: 0px;margin-right: 10px;">--%>
        <div class="col-md-6">
            <div class="portlet box purple-soft">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-cogs"></i>Sub Category
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                        <a href="javascript:;" class="reload"></a>
                        <a href="javascript:;" class="remove"></a>
                    </div>
                </div>
                <div class="portlet-body">
                    <table class="table table-bordered table-striped">
                        <thead>
                            <tr>

                                <th>Sub Category
                                </th>
                                <th style="width: 20%;">Count
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:ListView ID="ListView4" runat="server" OnItemDataBound="ListView4_ItemDataBound">
                                <ItemTemplate>
                                    <tr>

                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text='<%# GetSubCat(Convert.ToInt32(Eval("TickSubCatID"))) %>'></asp:Label>
                                            <asp:Label ID="lblSubCatid" runat="server" Text='<%# Eval("TickSubCatID") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label8" CssClass="badge badge-info" runat="server" Text="Label"></asp:Label>
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
    <%--<tr>
                                <td>Default
                                </td>
                                <td>
                                    <span class="label label-default">Default </span>
                                </td>
                                <td>
                                    <span class="badge badge-default">5 </span>
                                </td>
                                <td>
                                    <span class="badge badge-default badge-roundless">3 </span>
                                </td>
                            </tr>
                            <tr>
                                <td>Primary
                                </td>
                                <td>
                                    <span class="label label-primary">Primary </span>
                                </td>
                                <td>
                                    <span class="badge badge-primary">4 </span>
                                </td>
                                <td>
                                    <span class="badge badge-primary badge-roundless">Hot </span>
                                </td>
                            </tr>
                            <tr>
                                <td>Info
                                </td>
                                <td>
                                    <span class="label label-info">Info </span>
                                </td>
                                <td>
                                    <span class="badge badge-info">6 </span>
                                </td>
                                <td>
                                    <span class="badge badge-info badge-roundless">New </span>
                                </td>
                            </tr>
                            <tr>
                                <td>Success
                                </td>
                                <td>
                                    <span class="label label-success">Success </span>
                                </td>
                                <td>
                                    <span class="badge badge-success">1 </span>
                                </td>
                                <td>
                                    <span class="badge badge-success badge-roundless">2 </span>
                                </td>
                            </tr>
                            <tr>
                                <td>Danger
                                </td>
                                <td>
                                    <span class="label label-danger">Danger </span>
                                </td>
                                <td>
                                    <span class="badge badge-danger">3 </span>
                                </td>
                                <td>
                                    <span class="badge badge-danger badge-roundless">5 </span>
                                </td>
                            </tr>
                            <tr>
                                <td>Warning
                                </td>
                                <td>
                                    <span class="label label-warning">Warning </span>
                                </td>
                                <td>
                                    <span class="badge badge-warning">12 </span>
                                </td>
                                <td>
                                    <span class="badge badge-warning badge-roundless">3 </span>
                                </td>
                            </tr>--%>
</asp:Content>
