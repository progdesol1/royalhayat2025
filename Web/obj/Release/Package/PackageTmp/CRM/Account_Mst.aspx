<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="Account_Mst.aspx.cs" Inherits="Web.CRM.Account_Mst" %>

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
               <%-- <ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">Account</a>
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
                                        <asp:LinkButton ID="btnPagereload" OnClick="btnPagereload_Click" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <div id="navigation" runat="server" class="btn-group btn-group-circle btn-group-solid">
                                            <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />
                                        </div>
                                        <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="AddNew" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                        <asp:Button ID="btnEditLable" runat="server" class="btn green-haze btn-circle" OnClick="btnEditLable_Click" Text="Update Label" />
                                        &nbsp;
                                        <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="/assets/global/img/flags/us.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="/assets/global/img/flags/ae.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="/assets/global/img/flags/fr.png" /></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="portlet-body form">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general1">
                                                    <div class="form-body">
                                                        <div class="alert alert-info">
                                                            <strong>Basic</strong>
                                                            Information
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                     <label runat="server" id="lbl1" class="control-label col-md-4 getshow"><span class="required">* </span>
                                                                        <asp:Label runat="server" ID="lblName1s"></asp:Label>
                                                                    </label>
                                                                    <asp:TextBox runat="server" ID="txtName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="txtName" ErrorMessage="Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtName2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblPhone_Office1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPhone_Office1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtPhone_Office" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPhone_Office" TargetControlID="txtPhone_Office" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPhone_Office2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPhone_Office2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblPhone1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPhone1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPhone" TargetControlID="txtPhone" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPhone2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPhone2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblPhone_Fax1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPhone_Fax1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtPhone_Fax" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPhone_Fax" TargetControlID="txtPhone_Fax" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPhone_Fax2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPhone_Fax2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblPhone_Alternate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPhone_Alternate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtPhone_Alternate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPhone_Alternate" TargetControlID="txtPhone_Alternate" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPhone_Alternate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPhone_Alternate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblWebsite1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtWebsite1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorWebsite" runat="server" ControlToValidate="txtWebsite" ErrorMessage="Website Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblWebsite2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtWebsite2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblEmail11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEmail11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtEmail1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail1" runat="server" ControlToValidate="txtEmail1" ErrorMessage="Email Required" ValidationGroup="Submit" CssClass="Validation"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ValidationGroup="Submit" CssClass="Validation" ErrorMessage="Email not Valid" ControlToValidate="txtEmail1" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblEmail12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEmail12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblEmail21s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEmail21s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtEmail2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail2" runat="server" ControlToValidate="txtEmail2" ErrorMessage="Email Required" ValidationGroup="Submit" CssClass="Validation"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ValidationGroup="Submit" CssClass="Validation" ErrorMessage="Email not Valid" ControlToValidate="txtEmail2" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblEmail22h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEmail22h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblAnnual_Revenue1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAnnual_Revenue1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtAnnual_Revenue" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAnnual_Revenue" runat="server" ControlToValidate="txtAnnual_Revenue" ErrorMessage="Annual Revenue Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblAnnual_Revenue2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAnnual_Revenue2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblEmployee1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEmployee1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmployee" runat="server" ControlToValidate="txtEmployee" ErrorMessage="Employee Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblEmployee2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEmployee2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblIndustryID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtIndustryID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpIndustryID" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblIndustryID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtIndustryID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblOwnership1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOwnership1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtOwnership" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorOwnership" runat="server" ControlToValidate="txtOwnership" ErrorMessage="Ownership Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblOwnership2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOwnership2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblAccountType1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAccountType1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpAccountType" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblAccountType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAccountType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTickerSymbol1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTickerSymbol1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtTickerSymbol" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTickerSymbol2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTickerSymbol2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="alert alert-info">
                                                            <strong>Address Information</strong>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblRating1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRating1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtRating" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderRating" TargetControlID="txtRating" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblRating2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRating2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblSicCode1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSicCode1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtSicCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderSicCode" TargetControlID="txtSicCode" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblSicCode2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSicCode2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblBilling_Address_Street1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBilling_Address_Street1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtBilling_Address_Street" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBilling_Address_Street" runat="server" ControlToValidate="txtBilling_Address_Street" ErrorMessage="Billing Address Street Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblBilling_Address_Street2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBilling_Address_Street2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblBilling_Address_City1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBilling_Address_City1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtBilling_Address_City" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBilling_Address_City" runat="server" ControlToValidate="txtBilling_Address_City" ErrorMessage="Billing Address City Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblBilling_Address_City2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBilling_Address_City2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblBilling_Address_Country1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBilling_Address_Country1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpBilling_Address_Country" OnSelectedIndexChanged="drpBilling_Address_Country_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblBilling_Address_Country2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBilling_Address_Country2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblBilling_Address_Postalcode1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBilling_Address_Postalcode1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtBilling_Address_Postalcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderBilling_Address_Postalcode" TargetControlID="txtBilling_Address_Postalcode" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblBilling_Address_Postalcode2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBilling_Address_Postalcode2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblBilling_Address_State1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBilling_Address_State1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpBilling_Address_State" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblBilling_Address_State2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBilling_Address_State2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>


                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblShipping_Address_Street1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtShipping_Address_Street1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtShipping_Address_Street" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorShipping_Address_Street" runat="server" ControlToValidate="txtShipping_Address_Street" ErrorMessage="Shipping Address Street Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblShipping_Address_Street2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtShipping_Address_Street2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblShipping_Address_City1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtShipping_Address_City1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtShipping_Address_City" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorShipping_Address_City" runat="server" ControlToValidate="txtShipping_Address_City" ErrorMessage="Shipping Address City Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblShipping_Address_City2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtShipping_Address_City2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblShipping_Address_Country1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtShipping_Address_Country1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpShipping_Address_Country" OnSelectedIndexChanged="drpShipping_Address_Country_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblShipping_Address_Country2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtShipping_Address_Country2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblShipping_Address_Postalcode1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtShipping_Address_Postalcode1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtShipping_Address_Postalcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderShipping_Address_Postalcode" TargetControlID="txtShipping_Address_Postalcode" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblShipping_Address_Postalcode2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtShipping_Address_Postalcode2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblShipping_Address_State1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtShipping_Address_State1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpShipping_Address_State" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblShipping_Address_State2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtShipping_Address_State2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblParentName1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtParentName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtParentName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorParentName" runat="server" ControlToValidate="txtParentName" ErrorMessage="Parent Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblParentName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtParentName2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDateEntered1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateEntered1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtDateEntered" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxDateEntered_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDateEntered" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDateEntered" runat="server" ControlToValidate="txtDateEntered" ErrorMessage="Date Entered Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" TargetControlID="txtDateEntered" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDateEntered2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateEntered2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDateModified1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateModified1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtDateModified" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxDateModified_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDateModified" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDateModified" runat="server" ControlToValidate="txtDateModified" ErrorMessage="Date Modified Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" TargetControlID="txtDateModified" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDateModified2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateModified2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblAssigned_to_Name1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAssigned_to_Name1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtAssigned_to_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssigned_to_Name" runat="server" ControlToValidate="txtAssigned_to_Name" ErrorMessage="Assigned To Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblAssigned_to_Name2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAssigned_to_Name2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTeamName1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTeamName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpTeamName" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTeamName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTeamName2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDescription1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescription1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox TextMode="MultiLine" ID="txtDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Description Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDescription2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescription2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <asp:Panel ID="pnl123" runat="server" Visible="false">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblCreatedBy1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCreatedBy1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderCreatedBy" TargetControlID="txtCreatedBy" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblCreatedBy2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCreatedBy2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblModifiedBy1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModifiedBy1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtModifiedBy" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderModifiedBy" TargetControlID="txtModifiedBy" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblModifiedBy2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModifiedBy2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblContactName1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtContactName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorContactName" runat="server" ControlToValidate="txtContactName" ErrorMessage="Contact Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblContactName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtContactName2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblDeleted1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeleted1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:CheckBox ID="cbDeleted" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblDeleted2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeleted2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblCRUP_ID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRUP_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpCRUP_ID" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblCRUP_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRUP_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                <asp:LinkButton ID="btnlistreload" OnClick="btnlistreload_Click" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>

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




                                                                    <div class="portlet-body" style="margin-left: 15px; margin-right: 15px;">
                                                                        <div id="sample_1_wrapper" class="dataTables_wrapper no-footer">

                                                                            <div class="row">
                                                                                <div class="col-md-6 col-sm-12" style="padding-top: 18px;">
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
                                                                                                Entries</label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-6 col-sm-12" style="padding-top: 18px;">
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
                                                                                                     <th style="width: 60px;">ACTION</th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhName" Text="Name"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhPhone_Office" Text="Phone Office"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhPhone" Text="Phone"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhWebsite" Text="Website"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhEmail1" Text="Email 1"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhAnnual_Revenue" Text="Annual Revenue"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhEmployee" Text="Employee"></asp:Label></th>


                                                                                                   
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="TenentID,ID,Name,Phone_Office,Phone,Phone_Fax,Phone_Alternate,Website,Email1,Email2,Annual_Revenue,Employee,IndustryID,Ownership,AccountType,TickerSymbol,Rating,SicCode,Billing_Address_Street,Billing_Address_City,Billing_Address_State,Billing_Address_Postalcode,Billing_Address_Country,Shipping_Address_Street,Shipping_Address_City,Shipping_Address_State,Shipping_Address_Postalcode,Shipping_Address_Country,ParentName,DateEntered,DateModified,Description,TeamName,Assigned_to_Name,CreatedBy,ModifiedBy,ContactName,Active,Deleted,CRUP_ID" DataKeyNames="TenentID,ID,Name,Phone_Office,Phone,Phone_Fax,Phone_Alternate,Website,Email1,Email2,Annual_Revenue,Employee,IndustryID,Ownership,AccountType,TickerSymbol,Rating,SicCode,Billing_Address_Street,Billing_Address_City,Billing_Address_State,Billing_Address_Postalcode,Billing_Address_Country,Shipping_Address_Street,Shipping_Address_City,Shipping_Address_State,Shipping_Address_Postalcode,Shipping_Address_Country,ParentName,DateEntered,DateModified,Description,TeamName,Assigned_to_Name,CreatedBy,ModifiedBy,ContactName,Active,Deleted,CRUP_ID">
                                                                                                    <LayoutTemplate>
                                                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                                                        </tr>
                                                                                                    </LayoutTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>
                                                                                                             <td>
                                                                                                                <table>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                                                        <%-- <td><asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("SUP_ID")%>' CommandName="btnPrint" CommandArgument='<%# Eval("SUP_ID")%>' runat="server" class="btn btn-sm green filter-submit margin-bottom"><i class="fa fa-print"></i></asp:LinkButton></td>--%>
                                                                                                                    </tr>
                                                                                                                </table>

                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblPhone_Office" runat="server" Text='<%# Eval("Phone_Office")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblWebsite" runat="server" Text='<%# Eval("Website")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblEmail1" runat="server" Text='<%# Eval("Email1")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblAnnual_Revenue" runat="server" Text='<%# Eval("Annual_Revenue")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblEmployee" runat="server" Text='<%# Eval("Employee")%>'></asp:Label></td>


                                                                                                           
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
