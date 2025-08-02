<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="ACM_Mapping.aspx.cs" Inherits="Web.ACM.ACM_Mapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div id="b" runat="server">
            <div class="col-md-12" style="height: 900px; padding-top: 15px;width: 1300px;margin-left: 23px;" >

                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Add 
                                        <asp:Label runat="server" ID="lblHeader"></asp:Label>
                                        <asp:TextBox Style="color: #333333" ID="txtHeader" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="btnPagereload" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                    </div>
                                </div>

                                <div class="portlet-body">
                                    <div class="portlet-body form">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general1">
                                                    <div class="form-body">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTenent" class="col-md-4 control-label" Text="TenentID"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="DrpTenent" runat="server" CssClass="form-control select2me" AutoPostBack="true" OnSelectedIndexChanged="DrpTenent_SelectedIndexChanged"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredTenent" runat="server" ControlToValidate="DrpTenent" ErrorMessage="TenentID Required." CssClass="Validation" ValidationGroup="Process" InitialValue="00"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblLocation" class="col-md-4 control-label" Text="LocationID"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="DrpLocation" runat="server" CssClass="form-control select2me" AutoPostBack="true" OnSelectedIndexChanged="DrpLocation_SelectedIndexChanged"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredLocation" runat="server" ControlToValidate="DrpLocation" ErrorMessage="Location Required." CssClass="Validation" ValidationGroup="Process" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lbluser" class="col-md-4 control-label" Text="UserID"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="DrpUSer" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredUSer3" runat="server" ControlToValidate="DrpUSer" ErrorMessage="User Required." CssClass="Validation" ValidationGroup="Process" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%--     <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblrole" class="col-md-4 control-label" Text="Role"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="DrpRole" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                        </div>
                                                        <div class="form-actions">
                                                            <div class="row">
                                                                <div class="col-md-9">
                                                                    <asp:Button ID="btnprocess" CssClass="btn btn-lg green-turquoise" runat="server" Text="Process" OnClick="btnprocess_Click" ValidationGroup="Process"/>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:Panel ID="pnlMapping" Visible="false" runat="server">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="portlet box green">
                                                                        <div class="portlet-title" style="margin-top: 5px;">
                                                                            <div class="caption">
                                                                                <i class="fa fa-gift"></i>Module Mapping
                                                                            </div>
                                                                            <div class="tools">
                                                                                <a href="javascript:;" class="collapse"></a>
                                                                                <a href="javascript:;" class="reload"></a>
                                                                            </div>
                                                                        </div>
                                                                        <div class="portlet-body form">
                                                                            <div class="form-body">
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">Module</label>
                                                                                    <div class="col-md-10">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <asp:ListView ID="ListModule" runat="server">
                                                                                                    <ItemTemplate>
                                                                                                        <td>
                                                                                                            
                                                                                                            <asp:Label ID="lblMID" Visible="false" runat="server" Text='<%# Eval("Module_Id") %>'></asp:Label>
                                                                                                            <asp:CheckBox ID="CHKmodule" runat="server" />
                                                                                                            <asp:Label ID="lblNameModule" runat="server" Text='<%# Eval("Module_Name") %>'></asp:Label>
                                                                                                        </td>
                                                                                                    </ItemTemplate>
                                                                                                </asp:ListView>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-12">
                                                                                        <div class="portlet box blue-hoki">
                                                                                            <div class="portlet-title" style="margin-top: 5px;">
                                                                                                <div class="caption">
                                                                                                    <i class="fa fa-gift"></i>List Module Mapping
                                                                                                </div>
                                                                                                <div class="tools">
                                                                                                    <a href="javascript:;" class="collapse"></a>
                                                                                                    <a href="javascript:;" class="reload"></a>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="portlet-body form">
                                                                                                <div class="form-body">
                                                                                                    <div class="form-group">
                                                                                                        <div class="col-md-12">
                                                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                                                <thead>
                                                                                                                    <tr>
                                                                                                                        <th>Privilage</th>
                                                                                                                        <th>Module</th>
                                                                                                                        <th>User</th>
                                                                                                                        <th>Active</th>
                                                                                                                    </tr>
                                                                                                                </thead>
                                                                                                                <tbody>
                                                                                                                    <asp:ListView ID="ListView1" runat="server">
                                                                                                                        <ItemTemplate>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Privilage(Convert.ToInt32(Eval("PRIVILEGE_ID"))) %>'></asp:Label>
                                                                                                                                    <asp:Label ID="Label5" Visible="false" runat="server" Text='<%# Eval("PRIVILEGE_ID") %>'></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Module(Convert.ToInt32(Eval("MODULE_ID"))) %>'></asp:Label>
                                                                                                                                    <asp:Label ID="Label6" Visible="false" runat="server" Text='<%# Eval("MODULE_ID") %>'></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label3" runat="server" Text='<%# USER(Convert.ToInt32(Eval("UserID"))) %>'></asp:Label>
                                                                                                                                    <asp:Label ID="Label7" Visible="false" runat="server" Text='<%# Eval("UserID") %>'></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("ACTIVE_FLAG") %>'></asp:Label></td>
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

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="portlet box purple">
                                                                        <div class="portlet-title" style="margin-top: 5px;">
                                                                            <div class="caption">
                                                                                <i class="fa fa-gift"></i>Role Mapping
                                                                            </div>
                                                                            <div class="tools">
                                                                                <a href="javascript:;" class="collapse"></a>
                                                                                <a href="javascript:;" class="reload"></a>
                                                                            </div>
                                                                        </div>
                                                                        <div class="portlet-body form">
                                                                            <div class="form-body">
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">Role</label>
                                                                                    <div class="col-md-10">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <asp:RadioButtonList ID="Rdorole" runat="server" ValidationGroup="a" CssClass="radio-list radio-inline" RepeatDirection="Horizontal">
                                                                                                </asp:RadioButtonList>
                                                                                                <asp:ListView ID="ListRole" runat="server">
                                                                                                    <ItemTemplate>
                                                                                                        <td>
                                                                                                            <%--<asp:Label ID="lblNameRole" runat="server" Text='<%# Eval("ROLE_NAME") %>'></asp:Label>--%>
                                                                                                            <asp:Label ID="lblRID" Visible="false" runat="server" Text='<%# Eval("ROLE_ID") %>'></asp:Label>
                                                                                                            <%--<asp:CheckBox ID="CHKrole" runat="server"/>    --%>                                                                                                        
                                                                                                        </td>
                                                                                                    </ItemTemplate>
                                                                                                </asp:ListView>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-12">
                                                                                        <div class="portlet box blue-hoki">
                                                                                            <div class="portlet-title" style="margin-top: 5px;">
                                                                                                <div class="caption">
                                                                                                    <i class="fa fa-gift"></i>List Role Mapping
                                                                                                </div>
                                                                                                <div class="tools">
                                                                                                    <a href="javascript:;" class="collapse"></a>
                                                                                                    <a href="javascript:;" class="reload"></a>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="portlet-body form">
                                                                                                <div class="form-body">
                                                                                                    <div class="form-group">
                                                                                                        <div class="col-md-12">
                                                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                                                <thead>
                                                                                                                    <tr>
                                                                                                                        <th>Privilage</th>
                                                                                                                        <th>User</th>
                                                                                                                        <th>Role</th>
                                                                                                                        <th>Active</th>
                                                                                                                    </tr>
                                                                                                                </thead>
                                                                                                                <tbody>
                                                                                                                    <asp:ListView ID="ListView2" runat="server">
                                                                                                                        <ItemTemplate>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Privilage(Convert.ToInt32(Eval("PRIVILEGE_ID"))) %>'></asp:Label></td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# USER(Convert.ToInt32(Eval("USER_ID"))) %>'></asp:Label></td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label3" runat="server" Text='<%# ROLE(Convert.ToInt32(Eval("ROLE_ID"))) %>'></asp:Label></td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("ACTIVE_FLAG") %>'></asp:Label></td>
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
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="portlet box blue-madison">
                                                                        <div class="portlet-title" style="margin-top: 5px;">
                                                                            <div class="caption">
                                                                                <i class="fa fa-gift"></i>Right Mapping
                                                                            </div>
                                                                            <div class="tools">
                                                                                <a href="javascript:;" class="collapse"></a>
                                                                                <a href="javascript:;" class="reload"></a>
                                                                            </div>
                                                                        </div>
                                                                        <div class="portlet-body form">
                                                                            <div class="form-body">
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">Right</label>
                                                                                    <div class="col-md-10">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <asp:ListView ID="ListRight" runat="server">
                                                                                                    <ItemTemplate>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblNameright" runat="server" Text='<%# "For " + Eval("LOGIN_ID") + " User" %>'></asp:Label>
                                                                                                            <asp:Label ID="lblRightID" Visible="false" runat="server" Text='<%# Eval("USER_ID") %>'></asp:Label>
                                                                                                            <asp:CheckBox ID="CHKRight" runat="server" />
                                                                                                        </td>
                                                                                                    </ItemTemplate>
                                                                                                </asp:ListView>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-12">
                                                                                        <div class="portlet box blue-hoki">
                                                                                            <div class="portlet-title" style="margin-top: 5px;">
                                                                                                <div class="caption">
                                                                                                    <i class="fa fa-gift"></i>List Right Mapping
                                                                                                </div>
                                                                                                <div class="tools">
                                                                                                    <a href="javascript:;" class="collapse"></a>
                                                                                                    <a href="javascript:;" class="reload"></a>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="portlet-body form">
                                                                                                <div class="form-body">
                                                                                                    <div class="form-group">
                                                                                                        <div class="col-md-12">
                                                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                                                <thead>
                                                                                                                    <tr>
                                                                                                                        <th>Privilage</th>
                                                                                                                        <th>User</th>

                                                                                                                    </tr>
                                                                                                                </thead>
                                                                                                                <tbody>
                                                                                                                    <asp:ListView ID="ListView3" runat="server">
                                                                                                                        <ItemTemplate>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Privilage(Convert.ToInt32(Eval("PRIVILEGE_ID"))) %>'></asp:Label></td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# USER(Convert.ToInt32(Eval("USER_ID"))) %>'></asp:Label></td>
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
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-actions">
                                                                <div class="row">
                                                                    <div class="col-md-9">
                                                                        <asp:Button ID="Button1" CssClass="btn btn-lg yellow-gold" runat="server" Text="Mapping" OnClick="Button1_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <%-- Listview --%>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>
</asp:Content>

