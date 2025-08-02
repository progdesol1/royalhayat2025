<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="CRM_tbl_Account_Mst.aspx.cs" Inherits="Web.CRM.CRM_tbl_Account_Mst" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <%--<ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">CRM_tbl_Account_Mst </a>
                    </li>
                </ul>
                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>--%>

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet light">
                                <div class="portlet box blue-hoki">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-gift"></i>
                                            <asp:Label ID="Label28" runat="server" Text="Account_Mst"></asp:Label>
                                        </div>
                                        <div class="tools">
                                            <a href="javascript:;" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <a href="javascript:;" class="reload"></a>
                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                        <div class="actions btn-set">
                                            <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="Submit" Text="AddNew" OnClick="btnAdd_Click" />

                                            <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
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
                                                                    <asp:Label runat="server" ID="lblName1s" class="col-md-2 control-label" Text="NAME"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidatorCustomerName" runat="server" ErrorMessage=" Name Is Required" ControlToValidate="txtName" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblName2h" class="col-md-4 control-label" Text="NAME"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <%-- <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblPhone_Office1s" class="col-md-2 control-label" Text="PHONE OFFICE"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtPhone_Office" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtPhone_Office" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPhone_Office2h" class="col-md-4 control-label" Text="PHONE OFFICE"></asp:Label></div>
                                                            </div>--%>
                                                        </div>
                                                        <%--<div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblPhone1s" class="col-md-2 control-label" Text="PHONE"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtPhone" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPhone2h" class="col-md-4 control-label" Text="PHONE"></asp:Label></div>
                                                            </div>
                                                       
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblPhone_Fax1s" class="col-md-2 control-label" Text="PHONE FAX"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtPhone_Fax" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtPhone_Fax" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPhone_Fax2h" class="col-md-4 control-label" Text="PHONE FAX"></asp:Label></div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblPhone_Alternate1s" class="col-md-2 control-label" Text="PHONE ALTERNATE"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtPhone_Alternate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtPhone_Alternate" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPhone_Alternate2h" class="col-md-4 control-label" Text="PHONE ALTERNATE"></asp:Label></div>
                                                            </div>
                                                       
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblWebsite1s" class="col-md-2 control-label" Text="WEBSITE"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblWebsite2h" class="col-md-4 control-label" Text="WEBSITE"></asp:Label></div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblEmail11s" class="col-md-2 control-label" Text="EMAIL1"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtEmail1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblEmail12h" class="col-md-4 control-label" Text="EMAIL1"></asp:Label></div>
                                                            </div>
                                                       
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblEmail21s" class="col-md-2 control-label" Text="EMAIL2"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtEmail2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblEmail22h" class="col-md-4 control-label" Text="EMAIL2"></asp:Label></div>
                                                            </div>
                                                        </div>--%>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblAnnual_Revenue1s" class="col-md-2 control-label" Text="ANNUAL REVENUE"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtAnnual_Revenue" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtAnnual_Revenue" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblAnnual_Revenue2h" class="col-md-4 control-label" Text="ANNUAL REVENUE"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblEmployee1s" class="col-md-2 control-label" Text="EMPLOYEE"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblEmployee2h" class="col-md-4 control-label" Text="EMPLOYEE"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblIndustryID1s" class="col-md-2 control-label" Text="INDUSTRY NAME"></asp:Label><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpIndustryID" runat="server" class="form-control select2me"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblIndustryID2h" class="col-md-4 control-label" Text="INDUSTRY NAME"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblOwnership1s" class="col-md-2 control-label" Text="OWNERSHIP"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtOwnership" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblOwnership2h" class="col-md-4 control-label" Text="OWNERSHIP"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblAccountType1s" class="col-md-2 control-label" Text="ACCOUNTTYPE"></asp:Label><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpAccountType" runat="server" class="form-control select2me"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblAccountType2h" class="col-md-4 control-label" Text="ACCOUNTTYPE"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTickerSymbol1s" class="col-md-2 control-label" Text="TICKERSYMBOL"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtTickerSymbol" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTickerSymbol2h" class="col-md-4 control-label" Text="TICKERSYMBOL"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblRating1s" class="col-md-2 control-label" Text="RATING"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtRating" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblRating2h" class="col-md-4 control-label" Text="RATING"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblSicCode1s" class="col-md-2 control-label" Text="SICCODE"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtSicCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblSicCode2h" class="col-md-4 control-label" Text="SICCODE"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblBilling_Address_Street1s" class="col-md-2 control-label" Text="BILLING ADDRESS STREET"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtBilling_Address_Street" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblBilling_Address_Street2h" class="col-md-4 control-label" Text="BILLING ADDRESS STREET"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblBilling_Address_City1s" class="col-md-2 control-label" Text="BILLING ADDRESS CITY"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtBilling_Address_City" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblBilling_Address_City2h" class="col-md-4 control-label" Text="BILLING ADDRESS CITY"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblBilling_Address_State1s" class="col-md-2 control-label" Text="BILLING ADDRESS STATE"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtBilling_Address_State" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblBilling_Address_State2h" class="col-md-4 control-label" Text="BILLING ADDRESS STATE"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblBilling_Address_Postalcode1s" class="col-md-2 control-label" Text="BILLING ADDRESS POSTALCODE"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtBilling_Address_Postalcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtBilling_Address_Postalcode" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblBilling_Address_Postalcode2h" class="col-md-4 control-label" Text="BILLING ADDRESS POSTALCODE"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblBilling_Address_Country1s" class="col-md-2 control-label" Text="BILLING ADDRESS COUNTRY"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtBilling_Address_Country" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblBilling_Address_Country2h" class="col-md-4 control-label" Text="BILLING ADDRESS COUNTRY"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblShipping_Address_Street1s" class="col-md-2 control-label" Text="SHIPPING ADDRESS STREET"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtShipping_Address_Street" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblShipping_Address_Street2h" class="col-md-4 control-label" Text="SHIPPING ADDRESS STREET"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblShipping_Address_City1s" class="col-md-2 control-label" Text="SHIPPING ADDRESS CITY"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtShipping_Address_City" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblShipping_Address_City2h" class="col-md-4 control-label" Text="SHIPPING ADDRESS CITY"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblShipping_Address_State1s" class="col-md-2 control-label" Text="SHIPPING ADDRESS STATE"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtShipping_Address_State" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblShipping_Address_State2h" class="col-md-4 control-label" Text="SHIPPING ADDRESS STATE"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblShipping_Address_Postalcode1s" class="col-md-2 control-label" Text="SHIPPING ADDRESS POSTALCODE"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtShipping_Address_Postalcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtShipping_Address_Postalcode" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblShipping_Address_Postalcode2h" class="col-md-4 control-label" Text="SHIPPING ADDRESS POSTALCODE"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblShipping_Address_Country1s" class="col-md-2 control-label" Text="SHIPPING ADDRESS COUNTRY"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtShipping_Address_Country" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblShipping_Address_Country2h" class="col-md-4 control-label" Text="SHIPPING ADDRESS COUNTRY"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblParentName1s" class="col-md-2 control-label" Text="PARENTNAME"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtParentName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblParentName2h" class="col-md-4 control-label" Text="PARENTNAME"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDateEntered1s" class="col-md-2 control-label" Text="DATEENTERED"></asp:Label><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtDateEntered" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxDateEntered_CalendarExtender" Format="dd/MM/yyyy" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDateEntered"></cc1:CalendarExtender>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDateEntered2h" class="col-md-4 control-label" Text="DATEENTERED"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDateModified1s" class="col-md-2 control-label" Text="DATEMODIFIED"></asp:Label><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtDateModified" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxDateModified_CalendarExtender" Format="dd/MM/yyyy" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDateModified"></cc1:CalendarExtender>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDateModified2h" class="col-md-4 control-label" Text="DATEMODIFIED"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDescription1s" class="col-md-2 control-label" Text="DESCRIPTION"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDescription2h" class="col-md-4 control-label" Text="DESCRIPTION"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTeamName1s" class="col-md-2 control-label" Text="TEAM NAME"></asp:Label><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpTeamName" runat="server" class="form-control select2me"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTeamName2h" class="col-md-4 control-label" Text="TEAM NAME"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblAssigned_to_Name1s" class="col-md-2 control-label" Text="ASSIGNED TO NAME"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtAssigned_to_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblAssigned_to_Name2h" class="col-md-4 control-label" Text="ASSIGNED TO NAME"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblContactName1s" class="col-md-2 control-label" Text="CONTACT NAME"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblContactName2h" class="col-md-4 control-label" Text="CONTACT NAME"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                       
                                                                    <div class="row">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label runat="server" id="lbl5214" class="col-md-4 control-label getshow ">
                                                                                    <asp:Label ID="Label15" runat="server" Text="Employ Name:"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:Label ID="Label16" runat="server" meta:resourcekey="Label3Resource1"></asp:Label>
                                                                                    <asp:DropDownList ID="drpCompnay" Style="width: 258px;" runat="server" class="form-control"  AutoPostBack="true" meta:resourcekey="drpCategoryResource1">
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                                <label runat="server" id="Label74" class="col-md-4 control-label gethide ">
                                                                                    <asp:Label ID="Label75" runat="server" Text="Employ Name:"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label runat="server" id="lbl632" class="col-md-4 control-label getshow">
                                                                                    <asp:Label ID="lblItmanager1" runat="server" Text="It Manager:" meta:resourcekey="lblItmanager1Resource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:Label ID="lblitManager" runat="server" meta:resourcekey="lblitManagerResource1"></asp:Label>
                                                                                    <div class="input-group">
                                                                                        <asp:DropDownList ID="drpItManager" runat="server" class="form-control" meta:resourcekey="drpItManagerResource1"></asp:DropDownList>
                                                                                        <span class="input-group-btn">
                                                                                            <asp:LinkButton ID="LinkButton8" runat="server" >
                                                                                    <i class="icon-plus " style="color:black;padding-left: 4px;"></i>
                                                                                            </asp:LinkButton>
                                                                                            <%-- <asp:Button ID="btnAddCobus" class="btn blue " runat="server" Text="Add"  Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                        </span>
                                                                                    </div>
                                                                                </div>
                                                                                <label runat="server" id="Label72" class="col-md-4 control-label gethide">
                                                                                    <asp:Label ID="Label73" runat="server" Text="It Manager:" meta:resourcekey="lblItmanager1Resource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="tabbable">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <thead>
                                                                                <tr>

                                                                                    <th>Company Name</th>
                                                                                    <th>Itmanager</th>
                                                                                    <th>EMAIL</th>
                                                                                    <th>Mobile No</th>
                                                                                    <th>State</th>

                                                                                    <th>City</th>
                                                                                    <th>Remark</th>


                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <asp:ListView ID="lstCompniy" runat="server">
                                                                                    <LayoutTemplate>
                                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                                        </tr>
                                                                                    </LayoutTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lblCustomerName" runat="server" Text='<%#getcompniy(Convert .ToInt32 ( Eval("CompID")))%>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("JobTitle")%>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblEMAIL" runat="server" Text='<%# Eval("email2")%>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblMOBPHONE" runat="server" Text='<%# Eval("BusPhone1")%>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblSTATE" runat="server" Text='<%#getStste (Convert .ToInt32 ( Eval("CompID")))%>'></asp:Label></td>

                                                                                            <td>
                                                                                                <asp:Label ID="lblCITY" runat="server" Text='<%# getcity(Convert .ToInt32 ( Eval("CompID")))%>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("remarks")%>'></asp:Label></td>


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


                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>CRM_tbl_Account_Mst List
                                    </div>
                                    <%-- <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>--%>
                                </div>
                                <div class="portlet-body form">
                                    <asp:Panel runat="server" ID="pnlGrid">
                                        <div class="tab-content">
                                            <div id="tab_1_1" class="tab-pane active">

                                                <div class="tab-content no-space">
                                                    <div class="tab-pane active" id="tab_general2">
                                                        <div class="table-container" style="">
                                                            <div class="portlet box blue-hoki">

                                                                <div class="portlet-body">
                                                                    <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                                        <thead>
                                                                            <tr>
                                                                                <th style="width: 60px;">ACTION</th>
                                                                                <th style="width: 60px;">Name</th>
                                                                                <th style="width: 60px;">Phone_Office</th>
                                                                                <th style="width: 60px;">Phone</th>
                                                                                <th style="width: 60px;">Phone_Fax</th>
                                                                                <th style="width: 60px;">Phone_Fax</th>
                                                                                <th style="width: 60px;">Website</th>
                                                                                <th style="width: 60px;">Email1</th>
                                                                                <th style="width: 60px;">Email2</th>
                                                                                <th style="width: 60px;">Annual_Revenue</th>
                                                                                <th style="width: 60px;">Employee</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="ID" DataKeyNames="ID">
                                                                                <LayoutTemplate>
                                                                                    <tr id="ItemPlaceholder" runat="server">
                                                                                    </tr>
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <div class="btn-group">
                                                                                                <a data-toggle="dropdown" href="#" class="btn btn-sm blue dropdown-toggle" style="width: 60px;">Action <i class="fa fa-angle-down"></i>
                                                                                                </a>
                                                                                                <ul class="dropdown-menu">
                                                                                                    <li>
                                                                                                        <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("ID")%>' runat="server">  <i class="fa fa-pencil"></i>Edit</asp:LinkButton>

                                                                                                    </li>
                                                                                                    <li>
                                                                                                        <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("ID")%>' runat="server"> <i class="fa fa-pencil"></i>Delete</asp:LinkButton>

                                                                                                    </li>
                                                                                                    <li>
                                                                                                        <asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("ID")%>' CommandName="btnPrint" CommandArgument='<%# Eval("ID")%>' runat="server"><i class="fa fa-pencil"></i>Print</asp:LinkButton>
                                                                                                    </li>
                                                                                                </ul>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblPhone_Office" runat="server" Text='<%# Eval("Phone_Office")%>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone")%>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblPhone_Fax" runat="server" Text='<%# Eval("Phone_Fax")%>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblPhone_Alternate" runat="server" Text='<%# Eval("Phone_Fax")%>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblWebsite" runat="server" Text='<%# Eval("Website")%>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblEmail1" runat="server" Text='<%# Eval("Email1")%>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblEmail2" runat="server" Text='<%# Eval("Email2")%>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblAnnual_Revenue" runat="server" Text='<%# Eval("Annual_Revenue")%>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblEmployee" runat="server" Text='<%# Eval("Employee")%>'></asp:Label></td>


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
                                                <asp:HiddenField ID="hideID" runat="server" Value="" />
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

    </div>
</asp:Content>







