<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="tblSetupInventory.aspx.cs" Inherits="Web.Master.tblSetupInventory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">

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
                                        <asp:LinkButton ID="btnPagereload" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <div class="btn-group btn-group-circle btn-group-solid">
                                            <%-- <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />--%>
                                        </div>
                                        <asp:Button ID="btnAdd"  runat="server" class="btn green-haze btn-circle" ValidationGroup="Submit" Text="Update Setting" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                        <asp:Button ID="btnEditLable" runat="server" class="btn green-haze btn-circle" OnClick="btnEditLable_Click" Text="Update Label" />
                                        &nbsp;
                                        <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="../assets/global/img/flags/us.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="../assets/global/img/flags/ae.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="../assets/global/img/flags/fr.png" /></asp:LinkButton>
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
                                                                <%--<div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTenentID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenentID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtTenentID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTenentID" runat="server" ControlToValidate="txtTenentID" ErrorMessage="Tenent name Required." CssClass="Validation" ValidationGroup="s" InitialValue="0"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblTenentID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenentID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>

                                                                <%--<div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lbllocationID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtlocationID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drplocation" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true">
                                                                            <asp:ListItem Value="1" Text="Kuwait"></asp:ListItem>
                                                            <asp:ListItem Value="2 " Text="Lebanon"></asp:ListItem>
                                                                        </asp:DropDownList>--%>
                                                                <%-- <asp:TextBox ID="txtlocationID" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidatorlocationID" runat="server" ControlToValidate="drplocation" ErrorMessage="Location name Required." CssClass="Validation" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lbllocationID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtlocationID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                                </div>
                                                        </div>--%>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblTransferOutTransType1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTransferOutTransType1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                    <asp:TextBox ID="txttransferouttranstype" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <%--  <asp:DropDownList ID="drpTransferOutTransType" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>--%>
                                                                                    <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorTransferOutTransType" runat="server" ErrorMessage="Transferouttranstype Required." ControlToValidate="txttransferouttranstype" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblTransferOutTransType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTransferOutTransType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblTransferOutTransSubType1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTransferOutTransSubType1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                    <asp:TextBox ID="txttransferoutsubtype" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorTransferOutTransSubType" runat="server" ErrorMessage="Transferouttranssubtype Required." ControlToValidate="txttransferoutsubtype" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblTransferOutTransSubType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTransferOutTransSubType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblTransferInTransType1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTransferInTransType1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                    <asp:TextBox ID="txttransferintranstype" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <%-- <asp:DropDownList ID="drpTransferInTransType" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>--%>
                                                                                    <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorTransferInTransType" runat="server" ErrorMessage="Transferintranstype Required." ControlToValidate="txttransferintranstype" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblTransferInTransType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTransferInTransType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblTransferInTransSubType1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTransferInTransSubType1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                    <asp:TextBox ID="txttransferintranssubtype" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <%--   <asp:DropDownList ID="drpTransferInTransSubType" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>--%>
                                                                                    <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorTransferInTransSubType" runat="server" ErrorMessage="Transferintranssubtype Required." ControlToValidate="txttransferintranssubtype" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblTransferInTransSubType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTransferInTransSubType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblConsumeTransType1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtConsumeTransType1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                    <asp:TextBox ID="txtconsumetranstype" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <%--<asp:DropDownList ID="drpConsumeTransType" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>--%>
                                                                                    <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorConsumeTransType" runat="server" ErrorMessage="Consumetranstype Required." ControlToValidate="txtconsumetranstype" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblConsumeTransType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtConsumeTransType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblConsumeTransSubType1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtConsumeTransSubType1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                    <asp:TextBox ID="txtconsumetranssubtype" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <%-- <asp:DropDownList ID="drpConsumeTransSubType" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>--%>
                                                                                    <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorConsumeTransSubType" runat="server" ErrorMessage="Consumetranssubtype Required." ControlToValidate="txtconsumetranssubtype" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblConsumeTransSubType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtConsumeTransSubType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <%--     <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lbltransidOut1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttransidOut1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txttransidOut" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortransidOut" runat="server" ControlToValidate="txttransidOut" ErrorMessage="Transidout Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lbltransidOut2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttransidOut2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lbltranssubidOut1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttranssubidOut1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txttranssubidOut" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortranssubidOut" runat="server" ControlToValidate="txttranssubidOut" ErrorMessage="Transsubidout Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lbltranssubidOut2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttranssubidOut2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lbltransidin1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttransidin1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txttransidin" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortransidin" InitialValue="0" runat="server" ControlToValidate="txttransidin" ErrorMessage="Transidin Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txttransidin"
                                                                                    FilterType="Custom, numbers" runat="server" />
                                                                                <asp:Label runat="server" ID="lbltransidin2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttransidin2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lbltranssubidIn1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttranssubidIn1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txttranssubidIn" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortranssubidIn" InitialValue="0" runat="server" ControlToValidate="txttranssubidIn" ErrorMessage="Transsubidin Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderMENU_ORDER" TargetControlID="txttranssubidIn"
                                                                                    FilterType="Custom, numbers" runat="server" />
                                                                                <asp:Label runat="server" ID="lbltranssubidIn2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttranssubidIn2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lbltransidConsume1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttransidConsume1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txttransidConsume" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortransidConsume" InitialValue="0" runat="server" ControlToValidate="txttransidConsume" ErrorMessage="Transidconsume Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txttransidConsume"
                                                                                    FilterType="Custom, numbers" runat="server" />
                                                                                <asp:Label runat="server" ID="lbltransidConsume2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttransidConsume2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lbltranssubidConsume1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttranssubidConsume1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txttranssubidConsume" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortranssubidConsume" runat="server" InitialValue="0" ControlToValidate="txttranssubidConsume" ErrorMessage="Transsubidconsume Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txttranssubidConsume"
                                                                                    FilterType="Custom, numbers" runat="server" />
                                                                                <asp:Label runat="server" ID="lbltranssubidConsume2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttranssubidConsume2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblMYSYSNAMEOut1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMYSYSNAMEOut1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtMYSYSNAMEOut" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorMYSYSNAMEOut" runat="server" ControlToValidate="txtMYSYSNAMEOut" ErrorMessage="Mysysnameout Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblMYSYSNAMEOut2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMYSYSNAMEOut2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblMYSYSNAMEIn1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMYSYSNAMEIn1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtMYSYSNAMEIn" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorMYSYSNAMEIn" runat="server" ControlToValidate="txtMYSYSNAMEIn" ErrorMessage="Mysysnamein Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblMYSYSNAMEIn2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMYSYSNAMEIn2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblStockTeking1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTeking1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <div class="col-md-8">
                                                                                    <asp:CheckBox ID="chkteking" runat="server" />
                                                                                    <%-- <asp:TextBox ID="txtStockTeking" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidatorStockTeking" runat="server" ControlToValidate="chkteking" ErrorMessage="Stockteking Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblStockTeking2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTeking2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblStockTakingPeriodBegin1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingPeriodBegin1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <div class="col-md-8">
                                                                                    <%-- <asp:TextBox ID="txtStockTakingPeriodBegin" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                                    <asp:CheckBox ID="chkstock" runat="server" />
                                                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorStockTakingPeriodBegin" runat="server" ControlToValidate="chkstock" ErrorMessage="Stocktakingperiodbegin Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblStockTakingPeriodBegin2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingPeriodBegin2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblStockTakingPeriodEnd1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingPeriodEnd1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <div class="col-md-8">
                                                                                    <asp:CheckBox ID="chkend" runat="server" />
                                                                                    <%-- <asp:TextBox ID="txtStockTakingPeriodEnd" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorStockTakingPeriodEnd" runat="server" ControlToValidate="chkend" ErrorMessage="Stocktakingperiodend Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblStockTakingPeriodEnd2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingPeriodEnd2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblStockTakingTransTypeIn1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingTransTypeIn1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtStockTakingTransTypeIn" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorStockTakingTransTypeIn" InitialValue="0" runat="server" ControlToValidate="txtStockTakingTransTypeIn" ErrorMessage="Stocktakingtranstypein Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblStockTakingTransTypeIn2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingTransTypeIn2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblStockTakingTransTypeOut1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingTransTypeOut1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtStockTakingTransTypeOut" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorStockTakingTransTypeOut" runat="server" InitialValue="0" ControlToValidate="txtStockTakingTransTypeOut" ErrorMessage="Stocktakingtranstypeout Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblStockTakingTransTypeOut2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingTransTypeOut2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblStockTakingTransSubTypeIn1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingTransSubTypeIn1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                    <asp:TextBox ID="txtstocktranssubtypein" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <%--  <asp:DropDownList ID="drpStockTakingTransSubTypeIn" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>--%>
                                                                                    <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorStockTakingTransSubTypeIn" runat="server" ErrorMessage="Stocktakingtranssubtypein Required." ControlToValidate="txtstocktranssubtypein" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblStockTakingTransSubTypeIn2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingTransSubTypeIn2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblStockTakingTransSubTypeOut1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingTransSubTypeOut1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                    <asp:TextBox ID="txtstocktakingtranssubtypeout" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <%--   <asp:DropDownList ID="drpStockTakingTransSubTypeOut" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>--%>
                                                                                    <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorStockTakingTransSubTypeOut" runat="server" ErrorMessage="Stocktakingtranssubtypeout Required." ControlToValidate="txtstocktakingtranssubtypeout" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblStockTakingTransSubTypeOut2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingTransSubTypeOut2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblStockTakingtransidInLast1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingtransidInLast1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtStockTakingtransidInLast" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtStockTakingtransidInLast"
                                                                                        FilterType="Custom, numbers" runat="server" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorStockTakingtransidInLast" runat="server" InitialValue="0" ControlToValidate="txtStockTakingtransidInLast" ErrorMessage="Stocktakingtransidinlast Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblStockTakingtransidInLast2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingtransidInLast2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblStockTakingtransidOutLast1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingtransidOutLast1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtStockTakingtransidOutLast" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtStockTakingtransidOutLast"
                                                                                        FilterType="Custom, numbers" runat="server" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorStockTakingtransidOutLast" runat="server" ControlToValidate="txtStockTakingtransidOutLast" ErrorMessage="Stocktakingtransidoutlast Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblStockTakingtransidOutLast2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStockTakingtransidOutLast2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblActive1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                    <asp:CheckBox ID="cbActive" runat="server" />
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblActive2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <%-- <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDefaultCUSTVENDID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDefaultCUSTVENDID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtDefaultCUSTVENDID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDefaultCUSTVENDID" runat="server" ControlToValidate="txtDefaultCUSTVENDID" ErrorMessage="Defaultcustvend name Required." CssClass="Validation" ValidationGroup="s" InitialValue="0"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblDefaultCUSTVENDID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDefaultCUSTVENDID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>
                                                                </div>
                                                                <div class="row">
                                                                    <%--<div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCreated1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCreated1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCreated" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCreated" runat="server" ControlToValidate="txtCreated" ErrorMessage="Created Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblCreated2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCreated2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>
                                                                    <%-- <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDateTime1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateTime1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtDateTime" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxDateTime_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDateTime" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDateTime" runat="server" ControlToValidate="txtDateTime" ErrorMessage="Datetime Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtDateTime" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDateTime2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateTime2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                                    <div class="row">

                                                                        <%--  <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDeleted1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeleted1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:CheckBox ID="cbDeleted" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDeleted2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeleted2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <div class="portlet box blue">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            <i class="fa fa-gift"></i>
                                                            <asp:Label runat="server" ID="Label5"></asp:Label>
                                                            List
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                            <asp:LinkButton ID="btnlistreload" OnClick="btnlistreload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>

                                                            <a href="javascript:;" class="remove"></a>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body form">
                                                        <asp:Panel runat="server" ID="pnlGrid">
                                                            <div class="tab-content">
                                                                <div id="tab_1_1" class="tab-pane active">

                                                                    <div class="tab-content no-space">
                                                                        <div class="tab-pane active" id="tab_general2">
                                                                            <div class="table-container" style="">




                                                                                <div class="portlet-body">
                                                                                    <div id="sample_1_wrapper" class="dataTables_wrapper no-footer">

                                                                                        <div class="row">
                                                                                            <div class="col-md-6 col-sm-12">
                                                                                                <div class="dataTables_length" id="sample_1_length">
                                                                                                    <label>
                                                                                                        Show
                                                                                       <asp:DropDownList class="form-control input-xsmall input-inline " ID="drpShowGrid" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpShowGrid_SelectedIndexChanged">
                                                                                           <asp:ListItem Value="5" Selected="True">5</asp:ListItem>
                                                                                           <asp:ListItem Value="15">15</asp:ListItem>
                                                                                           <asp:ListItem Value="20">20</asp:ListItem>
                                                                                           <asp:ListItem Value="30">30</asp:ListItem>
                                                                                           <asp:ListItem Value="40">40</asp:ListItem>
                                                                                           <asp:ListItem Value="50">50</asp:ListItem>
                                                                                           <asp:ListItem Value="100">100</asp:ListItem>
                                                                                       </asp:DropDownList>
                                                                                                        <%--<select name="sample_1_length" aria-controls="sample_1"  tabindex="-1" title="">
                                                                                            <option value="5">5</option>
                                                                                            <option value="15">15</option>
                                                                                            <option value="20">20</option>
                                                                                            <option value="-1">All</option>
                                                                                        </select>--%>
                                                                                                entries</label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-6 col-sm-12">
                                                                                                <div id="sample_1_filter" class="dataTables_filter">
                                                                                                    <label>Search:<input type="search" class="form-control input-small input-inline" placeholder="" aria-controls="sample_1"></label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="table-scrollable">
                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                                                                        <thead>
                                                                                                            <tr>

                                                                                                                <th>
                                                                                                                    <asp:Label runat="server" ID="lblhTransferOutTransType" Text="Transferouttranstype"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label runat="server" ID="lblhTransferOutTransSubType" Text="Transferouttranssubtype"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label runat="server" ID="lblhMYSYSNAMEOut" Text="Mysysnameout"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label runat="server" ID="lblhMYSYSNAMEIn" Text="Mysysnamein"></asp:Label></th>

                                                                                                                <th style="width: 60px;">ACTION</th>
                                                                                                            </tr>
                                                                                                        </thead>
                                                                                                        <tbody>
                                                                                                            <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="transidOut" DataKeyNames="transidOut">
                                                                                                                <LayoutTemplate>
                                                                                                                    <tr id="ItemPlaceholder" runat="server">
                                                                                                                    </tr>
                                                                                                                </LayoutTemplate>
                                                                                                                <ItemTemplate>
                                                                                                                    <tr>

                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblTransferOutTransType" runat="server" Text='<%# Eval("TransferOutTransType")%>'></asp:Label></td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblTransferOutTransSubType" runat="server" Text='<%# Eval("TransferOutTransSubType")%>'></asp:Label></td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblMYSYSNAMEOut" runat="server" Text='<%# Eval("MYSYSNAMEOut")%>'></asp:Label></td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblMYSYSNAMEIn" runat="server" Text='<%# Eval("MYSYSNAMEIn")%>'></asp:Label></td>

                                                                                                                        <td>
                                                                                                                            <table>
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("transidOut")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                                    <td>
                                                                                                                                        <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("transidOut")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
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
                                                                                        <div class="row">
                                                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <div class="col-md-5 col-sm-12">
                                                                                                        <div class="dataTables_info" id="sample_1_info" role="status" aria-live="polite">
                                                                                                            <asp:Label ID="lblShowinfEntry" runat="server"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                            <div class="col-md-7 col-sm-12">
                                                                                                <div class="dataTables_paginate paging_simple_numbers" id="sample_1_paginate">
                                                                                                    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                                <ContentTemplate>--%>
                                                                                                    <ul class="pagination">
                                                                                                        <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_fist">
                                                                                                            <%--  <asp:LinkButton ID="Button1" runat="server"  BorderStyle="None" />First</asp:LinkButton> --%>
                                                                                                            <asp:LinkButton ID="btnfirst1" OnClick="btnfirst_Click" runat="server"> First</asp:LinkButton>
                                                                                                        </li>
                                                                                                        <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_Next">
                                                                                                            <%--  <asp:LinkButton ID="Button1" runat="server"  BorderStyle="None" />First</asp:LinkButton> --%>
                                                                                                            <asp:LinkButton ID="btnNext1" OnClick="btnNext1_Click" runat="server"> Next</asp:LinkButton>
                                                                                                        </li>
                                                                                                        <asp:ListView ID="ListView2" runat="server" OnItemCommand="ListView2_ItemCommand" OnItemDataBound="AnswerList_ItemDataBound">
                                                                                                            <ItemTemplate>
                                                                                                                <td>
                                                                                                                    <li class="paginate_button " aria-controls="sample_1" tabindex="0">
                                                                                                                        <asp:LinkButton ID="LinkPageavigation" runat="server" CommandName="LinkPageavigation" CommandArgument='<%# Eval("ID")%>'> <%# Eval("ID")%></asp:LinkButton></li>

                                                                                                                </td>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:ListView>
                                                                                                        <li class="paginate_button next" aria-controls="sample_1" tabindex="0" id="sample_1_Previos">
                                                                                                            <asp:LinkButton ID="btnPrevious1" OnClick="btnPrevious1_Click" runat="server"> Prev</asp:LinkButton>
                                                                                                        </li>
                                                                                                        <li class="paginate_button next" aria-controls="sample_1" tabindex="0" id="sample_1_last">
                                                                                                            <asp:LinkButton ID="btnLast1" OnClick="btnLast1_Click" runat="server"> Last</asp:LinkButton>
                                                                                                        </li>
                                                                                                    </ul>
                                                                                                    <%--  </ContentTemplate>
                                                                                            </asp:UpdatePanel>--%>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>

                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <asp:HiddenField ID="hideID" runat="server" Value="" />
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnfirst1" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnNext1" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnPrevious1" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnLast1" EventName="Click" />
                                            </Triggers>

                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
                runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="divWaiting">
                        <asp:Label ID="lblWait" runat="server"
                            Text=" Please wait... " />
                        <asp:Image ID="imgWait" runat="server"
                            ImageAlign="Middle" ImageUrl="assets/admin/layout4/img/loading.gif" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>

</asp:Content>
