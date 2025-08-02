<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="ICEXTRACOST.aspx.cs" Inherits="Web.CRM.ICEXTRACOST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="b" runat="server">
        <div class="col-md-12">
            <%-- <ul class="page-breadcrumb breadcrumb">
            <li>
                <a href="index.aspx">CRM</a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <a href="#">ICEXTRACOST</a>
            </li>

        </ul>--%>
            <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                <div class="alert alert-success alert-dismissable">
                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-horizontal form-row-seperated">
                       <%-- <div class="portlet light">--%>
                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <%-- <div class="caption">
                                    <i class="fa fa-gift"></i>
                                    <asp:Label ID="Label28" runat="server" Text="ICEXTRACOST"></asp:Label>
                                </div>--%>
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Add 
                                        <asp:Label runat="server" ID="lblHeader"></asp:Label>
                                        <asp:TextBox Style="color: #333333" ID="txtHeader" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <div class="btn-group btn-group-circle btn-group-solid">
                                            <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click1" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click1" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click1" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click1" />
                                        </div>

                                        <asp:LinkButton ID="btnSubmit" data-placement="top" data-toggle="tooltip" OnClientClick="return showWarningToast();" OnClick="btnSubmit_Click" ToolTip="Submit Data" runat="server" class="btn btn-success" Text="Save" />
                                        <asp:LinkButton ID="btnCancel" OnClientClick="ClearAllText()" data-placement="top" data-toggle="tooltip" ToolTip="Exit Data" runat="server" class="btn btn-danger" Text="Exit" /><%--trash-o--%>
                                        <asp:Button ID="btnEditLable" runat="server" class="btn green-haze btn-circle" OnClick="btnEditLable_Click1" Text="Update Label" />
                                        &nbsp;
                                        <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click1">E&nbsp;<img src="../assets/global/img/flags/us.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click1">A&nbsp;<img src="../assets/global/img/flags/ae.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click1">F&nbsp;<img src="../assets/global/img/flags/fr.png" /></asp:LinkButton>

                                    </div>
                                </div>
                                <div class="portlet-body">

                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general1">
                                                <div class="form-body">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="lblOHNAME11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOHNAME11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox ID="txtOHNAME1" runat="server" AutoCompleteType="Disabled" data-toggle="tooltip" ToolTip="Overhead Cost Name1" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorOHNAME1" runat="server" ControlToValidate="txtOHNAME1" ErrorMessage="Oh name 1 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblOHNAME12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOHNAME12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="lblOHNAME21s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOHNAME21s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox ID="txtOHNAME2" runat="server" AutoCompleteType="Disabled" data-toggle="tooltip" ToolTip="Overhead Cost Name2" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblOHNAME22h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOHNAME22h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="lblOHNAME31s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOHNAME31s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox ID="txtOHNAME3" runat="server" AutoCompleteType="Disabled" data-toggle="tooltip" ToolTip="Overhead Cost Name3" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblOHNAME32h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOHNAME32h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--<div class="form-group">--%>
                                                    <%-- <label class="col-md-2 control-label">
                                                        <asp:Label ID="lblOHNAME1" runat="server" Text="Overhead Cost Name1"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>--%>
                                                    <%--<div class="col-md-4">--%>
                                                    <%--<asp:TextBox ID="txtOHNAME1" runat="server" AutoCompleteType="Disabled" data-toggle="tooltip" ToolTip="Overhead Cost Name1" CssClass="form-control" MaxLength="50"></asp:TextBox>--%>
                                                    <%--  </div>--%>
                                                    <%--<label class="col-md-2 control-label">
                                                        <asp:Label ID="lblOHNAME2" runat="server" Text="Overhead Cost Name2"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>--%>
                                                    <%--<div class="col-md-4">--%>
                                                    <%--<asp:TextBox ID="txtOHNAME2" runat="server" AutoCompleteType="Disabled" data-toggle="tooltip" ToolTip="Overhead Cost Name2" CssClass="form-control" MaxLength="50"></asp:TextBox>--%>
                                                    <%-- </div>--%>
                                                    <%-- </div>--%>
                                                    <%-- <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lblOHNAME3" runat="server" Text="Overhead Cost Name3"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtOHNAME3" runat="server" AutoCompleteType="Disabled" data-toggle="tooltip" ToolTip="Overhead Cost Name3" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                    </div>

                                                </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                       <%-- </div>--%>
                        <div class="portlet box blue">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-gift"></i>
                                    <asp:Label runat="server" ID="Label5" Text="Extra Overhead Cost"></asp:Label>
                                    List
                                </div>
                            </div>
                            <div class="portlet-body">
                                <div class="tabbable">
                                    <table class="table table-striped table-bordered table-hover" id="sample_1">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="lblSN" runat="server" Text="#"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="lblGrEdit" runat="server" Text="Action"></asp:Label></th>

                                                <th>
                                                    <asp:Label ID="lblGRHOHNAME1" runat="server" Text="Overhead Cost Name1"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHOHNAME2" runat="server" Text="Overhead Cost Name2"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHOHNAME3" runat="server" Text="Overhead Cost Name3"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblHActive" runat="server" Text="Active"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="lblHAction" runat="server" Text="Action"></asp:Label></th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="grdmstr" runat="server" DataKey="TenentID,OVERHEADID,OHNAME1,OHNAME2,OHNAME3,ACCOUNTID,Active,CRUP_ID,OHNAME,OHNAMEO" DataKeyNames="TenentID,OVERHEADID,OHNAME1,OHNAME2,OHNAME3,ACCOUNTID,Active,CRUP_ID,OHNAME,OHNAMEO">
                                                <LayoutTemplate>
                                                    <tr id="ItemPlaceholder" runat="server">
                                                    </tr>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>' meta:resourcekey="lblSRNOResource2"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <div class="btn-group">
                                                                <a data-toggle="dropdown" href="#" class="btn btn-sm blue dropdown-toggle" style="width: 60px;">
                                                                    <asp:Label ID="Label19" runat="server" Text="Action" meta:resourcekey="Label19Resource1"></asp:Label>
                                                                    <i class="fa fa-angle-down"></i>
                                                                </a>
                                                                <ul class="dropdown-menu">
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkbtn" runat="server" CommandArgument='<%# Eval("TenentID") + "," +Eval("OVERHEADID") %>' OnClick="lnkbtn_Click">
                                                                            <i class="fa fa-pencil"></i>
                                                                            <asp:Label ID="Label73" runat="server" Text="Edit" meta:resourcekey="Label73Resource1"></asp:Label>
                                                                        </asp:LinkButton>

                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("TenentID") + "," +Eval("OVERHEADID") %>' OnClientClick="return confirm('Do you want to delete this Overhead Cost?')" OnClick="lnkbtndelete_Click">
                                                                            <i class="fa fa-pencil"></i>
                                                                            <asp:Label ID="Label74" runat="server" Text="Delete" meta:resourcekey="Label74Resource1"></asp:Label>
                                                                        </asp:LinkButton>

                                                                    </li>

                                                                </ul>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCOHNAME1" runat="server" Text='<%# Eval("OHNAME1") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCOHNAME2" runat="server" Text='<%# Eval("OHNAME2") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCOHNAME3" runat="server" Text='<%# Eval("OHNAME3") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCurrentStatus" runat="server" Text='<%# Eval("ACTIVE") %>' meta:resourcekey="lblCurrentStatusResource1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkAction" Style="color: #5b9bd1" runat="server" CommandArgument='<%# Eval("TenentID") + "," +Eval("Active")  + "," +Eval("OVERHEADID")  %>' OnClick="lnkAction_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </tbody>
                                    </table>
                                    <asp:HiddenField ID="hidId" runat="server" Value="" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
