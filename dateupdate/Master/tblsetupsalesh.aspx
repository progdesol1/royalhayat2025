<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="tblsetupsalesh.aspx.cs" Inherits="Web.Master.tblsetupsalesh" %>

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
                                        <asp:Button ID="btnAdd"  runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="Update Setting" OnClick="btnAdd_Click" />
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
                                                    </div>
                                                    <asp:Label runat="server" ID="lblTenentID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenentID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <%--  <div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lbllocationID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtlocationID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList  ID="drplocation" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drplocation_SelectedIndexChanged">
                                                            <asp:ListItem Value="1" Text="Kuwait"></asp:ListItem>
                                                            <asp:ListItem Value="2 " Text="Lebanon"></asp:ListItem>
                                                        </asp:DropDownList>--%>
                                                <%--<asp:TextBox ID="txtlocationID" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlocationID" runat="server" ControlToValidate="drplocation" ErrorMessage="Location name Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator></div>
                                                    <asp:Label runat="server" ID="lbllocationID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtlocationID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>--%>
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblmodule1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtmodule1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="drpmodule" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpmodule_SelectedIndexChanged"></asp:DropDownList>
                                                            <%-- <asp:TextBox ID="txtmodule" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatormodule" runat="server" ControlToValidate="drpmodule" ErrorMessage="Module Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblmodule2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtmodule2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblDeliveryLocation1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeliveryLocation1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="drpdeliverylocation" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpdeliverylocation_SelectedIndexChanged">                                                                
                                                            </asp:DropDownList>
                                                            <%--<asp:TextBox ID="txtDeliveryLocation" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDeliveryLocation" runat="server" ControlToValidate="drpdeliverylocation" ErrorMessage="Deliverylocation Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblDeliveryLocation2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeliveryLocation2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <%--<div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lbltransid1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttransid1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txttransid" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortransid" runat="server" ControlToValidate="txttransid" ErrorMessage="Transid Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                    <asp:Label runat="server" ID="lbltransid2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttransid2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>--%>
                                            </div>
                                        </div>
                                        <%--  <div class="row">
                                             <div class="col-md-12">--%>
                                        <%--  <div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lbltranssubid1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttranssubid1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txttranssubid" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortranssubid" runat="server" ControlToValidate="txttranssubid" ErrorMessage="Transsubid Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                    <asp:Label runat="server" ID="lbltranssubid2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttranssubid2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>--%>

                                        <%--</div>
                                        </div>--%>
                                        <div class="row">
                                            <div class="col-md-12">

                                                <%-- <div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lblCompniID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCompniID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtCompniID" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCompniID" runat="server" ControlToValidate="txtCompniID" ErrorMessage="Compni name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                    <asp:Label runat="server" ID="lblCompniID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCompniID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>--%>
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblPaymentDays1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPaymentDays1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtPaymentDays" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderMENU_ORDER" TargetControlID="txtPaymentDays"
                                                                FilterType="Custom, numbers" runat="server" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPaymentDays" runat="server" ControlToValidate="txtPaymentDays" ErrorMessage="Paymentdays Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblPaymentDays2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPaymentDays2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblReminderDays1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReminderDays1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtReminderDays" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorReminderDays" runat="server" ControlToValidate="txtReminderDays" ErrorMessage="Reminderdays Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblReminderDays2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReminderDays2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- <div class="row">
                                             <div class="col-md-12">--%>
                                        <%--<div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lblLastClosePeriod1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLastClosePeriod1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtLastClosePeriod" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorLastClosePeriod" runat="server" ControlToValidate="txtLastClosePeriod" ErrorMessage="Lastcloseperiod Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                    <asp:Label runat="server" ID="lblLastClosePeriod2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLastClosePeriod2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>--%>
                                        <%--  <div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lblCurrentPeriod1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCurrentPeriod1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    <div class="col-md-8">--%>
                                        <%-- <asp:DropDownList ID="drpcurrentperiod" runat="server" Visible="false" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpcurrentperiod_SelectedIndexChanged"></asp:DropDownList>--%>
                                        <%-- <asp:TextBox ID="txtCurrentPeriod" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorCurrentPeriod" runat="server" ControlToValidate="txtCurrentPeriod" ErrorMessage="Currentperiod Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>--%>
                                        <%--  <asp:Label runat="server" ID="lblCurrentPeriod2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCurrentPeriod2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>
                                                 </div>--%>
                                        <%-- </div>--%>
                                        <div class="row">
                                            <div class="col-md-12">


                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblDescWithWarantee1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescWithWarantee1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                            <asp:CheckBox ID="cbDescWithWarantee" runat="server" />
                                                        </div>
                                                        <asp:Label runat="server" ID="lblDescWithWarantee2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescWithWarantee2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblDescWithSerial1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescWithSerial1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                            <asp:CheckBox ID="cbDescWithSerial" runat="server" />
                                                        </div>
                                                        <asp:Label runat="server" ID="lblDescWithSerial2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescWithSerial2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <%-- <div class="row">
                                             <div class="col-md-12">--%>
                                        <%--   <div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lblAcceptWarantee1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAcceptWarantee1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtAcceptWarantee" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAcceptWarantee" runat="server" ControlToValidate="txtAcceptWarantee" ErrorMessage="Acceptwarantee Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                    <asp:Label runat="server" ID="lblAcceptWarantee2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAcceptWarantee2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>--%>

                                        <%-- </div>
                                        </div>--%>
                                        <div class="row">
                                            <div class="col-md-12">

                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblDescWithColor1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescWithColor1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                            <asp:CheckBox ID="cbDescWithColor" runat="server" />
                                                        </div>
                                                        <asp:Label runat="server" ID="lblDescWithColor2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescWithColor2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblAllowMinusQty1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAllowMinusQty1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                            <asp:CheckBox ID="cbAllowMinusQty" runat="server" />
                                                        </div>
                                                        <asp:Label runat="server" ID="lblAllowMinusQty2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAllowMinusQty2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblCOUNTRYID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOUNTRYID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="drpcountry" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true"></asp:DropDownList>
                                                            <%-- <asp:TextBox ID="txtCOUNTRYID" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCOUNTRYID" runat="server" ControlToValidate="drpcountry" ErrorMessage="Country name Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblCOUNTRYID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOUNTRYID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblSalesAdminID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSalesAdminID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="drpsaleadmin" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true"></asp:DropDownList>
                                                            <%-- <asp:TextBox ID="txtSalesAdminID" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSalesAdminID" runat="server" ControlToValidate="drpsaleadmin" ErrorMessage="Salesadmin name Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblSalesAdminID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSalesAdminID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <%--  <div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lblTCQuotation1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTCQuotation1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtTCQuotation" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTCQuotation" runat="server" ControlToValidate="txtTCQuotation" ErrorMessage="Tcquotation Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                    <asp:Label runat="server" ID="lblTCQuotation2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTCQuotation2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>--%>
                                            </div>
                                        </div>
                                        <%-- <div class="row">
                                             <div class="col-md-12">--%>
                                        <%-- <div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lblIntroLetter1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtIntroLetter1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtIntroLetter" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorIntroLetter" runat="server" ControlToValidate="txtIntroLetter" ErrorMessage="Introletter Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                    <asp:Label runat="server" ID="lblIntroLetter2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtIntroLetter2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>--%>

                                        <%--   </div>
                                        </div>--%>
                                        <div class="row">
                                            <div class="col-md-12">

                                                <%-- <div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lblCRUP_ID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRUP_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtCRUP_ID" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCRUP_ID" runat="server" ControlToValidate="txtCRUP_ID" ErrorMessage="Crup  name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                    <asp:Label runat="server" ID="lblCRUP_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRUP_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>--%>
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblDeliveryPrintURL1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeliveryPrintURL1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtDeliveryPrintURL" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDeliveryPrintURL" runat="server" ControlToValidate="txtDeliveryPrintURL" ErrorMessage="Deliveryprinturl Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblDeliveryPrintURL2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeliveryPrintURL2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblCounterName1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCounterName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtCounterName" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCounterName" runat="server" ControlToValidate="txtCounterName" ErrorMessage="Countername Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblCounterName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCounterName2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--  <div class="row">
                                             <div class="col-md-12">--%>
                                        <%--<div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lblInvoicePrintURL1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtInvoicePrintURL1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtInvoicePrintURL" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoicePrintURL" runat="server" ControlToValidate="txtInvoicePrintURL" ErrorMessage="Invoiceprinturl Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                    <asp:Label runat="server" ID="lblInvoicePrintURL2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtInvoicePrintURL2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>--%>

                                        <%--</div>
                                        </div>--%>
                                        <div class="row">
                                            <div class="col-md-12">

                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblHeaderLine1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtHeaderLine1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-8">
                                                            
                                                            <asp:TextBox ID="txtHeaderLine" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorHeaderLine" runat="server" ControlToValidate="txtHeaderLine" ErrorMessage="Headerline Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblHeaderLine2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtHeaderLine2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblTagLine1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTagLine1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtTagLine" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTagLine" runat="server" ControlToValidate="txtTagLine" ErrorMessage="Tagline Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblTagLine2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTagLine2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">

                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblBottomTagLine1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBottomTagLine1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-8">

                                                            <asp:TextBox ID="txtBottomTagLine" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorBottomTagLine" runat="server" ControlToValidate="txtBottomTagLine" ErrorMessage="Bottomtagline Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblBottomTagLine2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBottomTagLine2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblPaymentDetails1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPaymentDetails1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtPaymentDetails" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorPaymentDetails" runat="server" ControlToValidate="txtPaymentDetails" ErrorMessage="Paymentdetails Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblPaymentDetails2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPaymentDetails2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">

                                                <%--  <div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lblEmployeeId1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEmployeeId1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtEmployeeId" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmployeeId" runat="server" ControlToValidate="txtEmployeeId" ErrorMessage="Employeeid Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                    <asp:Label runat="server" ID="lblEmployeeId2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEmployeeId2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>--%>
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblActive1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                            <asp:CheckBox ID="cbActive" runat="server" />
                                                        </div>
                                                        <asp:Label runat="server" ID="lblActive2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- <div class="row">
                                             <div class="col-md-12">--%>
                                        <%--<div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lblDeftCoustomer1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeftCoustomer1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="txtDeftCoustomer" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDeftCoustomer" runat="server" ControlToValidate="txtDeftCoustomer" ErrorMessage="Deftcoustomer Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                    <asp:Label runat="server" ID="lblDeftCoustomer2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeftCoustomer2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>--%>
                                        <%--<div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lblCreated1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCreated1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                        <asp:CheckBox ID="cbCreated" runat="server" Visible="false"/>
                                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorCreated" runat="server" ControlToValidate="cbCreated" ErrorMessage="Created Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>--%>
                                        <%-- <asp:Label runat="server" ID="lblCreated2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCreated2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>
                                        </div>--%>
                                        <%-- </div>
                                            </div>--%>
                                        <%--<div class="row">
                                             <div class="col-md-12">--%>
                                        <%--  <div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lblDateTime1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateTime1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                        <asp:TextBox Placeholder="DD/MM/YYYY" Visible="false" ID="txtDateTime" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxDateTime_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDateTime" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDateTime" runat="server" ControlToValidate="txtDateTime" ErrorMessage="Datetime Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtDateTime" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                    </div>
                                                    <asp:Label runat="server" ID="lblDateTime2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateTime2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>--%>

                                        <%-- </div>
                                        </div>--%>
                                        <%--  <div class="row">
                                             <div class="col-md-12">
                                            <div class="col-md-6">
                                                <div class="form-group" style="color: ">
                                                    <asp:Label runat="server" ID="lblDeleted1s" Visible="false" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeleted1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                        <asp:CheckBox ID="cbDeleted" runat="server" />
                                                    </div>
                                                    <asp:Label runat="server" ID="lblDeleted2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeleted2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                            </div>
                                                 </div>
                                        </div>--%>
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

                                                                                        <%--<th>
                                                                                            <asp:Label runat="server" ID="lblhCompniID" Text="Compni name"></asp:Label></th>--%>
                                                                                        <th>
                                                                                            <asp:Label runat="server" ID="lblhReminderDays" Text="Reminderdays"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label runat="server" ID="lblhCOUNTRYID" Text="Country name"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label runat="server" ID="lblhSalesAdminID" Text="Salesadmin name"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label runat="server" ID="lblhCounterName" Text="Countername"></asp:Label></th>

                                                                                        <th style="width: 60px;">ACTION</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="transid" DataKeyNames="transid">
                                                                                        <LayoutTemplate>
                                                                                            <tr id="ItemPlaceholder" runat="server">
                                                                                            </tr>
                                                                                        </LayoutTemplate>
                                                                                        <ItemTemplate>
                                                                                            <tr>

                                                                                                <%--<td>
                                                                                                    <asp:Label ID="lblCompniID" runat="server" Text='<%# Eval("CompniID")%>'></asp:Label></td>--%>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblReminderDays" runat="server" Text='<%# Eval("ReminderDays")%>'></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblCOUNTRYID" runat="server" Text='<%#Catname(Convert.ToInt32 (Eval("COUNTRYID"))) %>'></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblSalesAdminID" runat="server" Text='<%#catadmin(Convert.ToInt32 (Eval("SalesAdminID"))) %>'></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblCounterName" runat="server" Text='<%# Eval("CounterName")%>'></asp:Label></td>

                                                                                                <td>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("locationID") +"-"+ Eval("transid") +"-"+ Eval("transsubid") %>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                            <td style="display:none;">
                                                                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("transid")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
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


</asp:Content>
