<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="Campaign_Mst.aspx.cs" Inherits="Web.CRM.Campaign_Mst" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/CRM/UserControl/RightPanelUC.ascx" TagPrefix="uc1" TagName="RightPanelUC" %>
<%@ Register Src="~/CRM/UserControl/AddReference.ascx" TagPrefix="uc1" TagName="AddReference" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }
    </style>

    <link href="assets/apps/css/todo-2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
               <%-- <ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="DashBoard.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">Campaign Mst</a>
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
                                        <asp:Button ID="btnAdd" runat="server" class="btn red btn-circle" Text="AddNew" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn green btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                        <asp:Button ID="btnEditLable" runat="server" class="btn purple btn-circle" OnClick="btnEditLable_Click" Text="Update Label" />
                                        <asp:Button ID="btnExit" runat="server" class="btn grey-cascade btn-circle" Text="Exit" OnClick="btnExit_Click"/>
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
                                                    <div class="col-md-12" id="Divmainsub" runat="server">
                                                        <div class="form-body">
                                                            <%--<div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTenantID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenantID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpTenantID" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTenantID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenantID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>

                                                            <div class="alert alert-info">
                                                                <strong>Main Campaign Data</strong>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-4">
                                                                    <h3 style="font-weight: 600"></h3>
                                                                </div>
                                                            </div>
                                                            
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblTenet1s" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenet1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-10">
                                                                            <asp:TextBox ID="txtTenet" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTenet" runat="server" ControlToValidate="txtTenet" ErrorMessage="Campaign Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblTenet2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenet2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 </div>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblName1s" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtName1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-10">
                                                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" OnTextChanged="txtName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="txtName" ErrorMessage="Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblName2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtName2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label1" class="col-md-2 control-label">Name in Lang#2</asp:Label><asp:TextBox runat="server" ID="TextBox1" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-10">
                                                                            <asp:TextBox ID="txtName2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName2" ErrorMessage="Name2 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="Label2" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox3" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                </div>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label3" class="col-md-2 control-label">Name in Lang#3</asp:Label><asp:TextBox runat="server" ID="TextBox4" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-10">
                                                                            <asp:TextBox ID="txtName3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName3" ErrorMessage="Name3 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="Label4" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox6" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblStartDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStartDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:TextBox Placeholder="MM/dd/yyyy" ID="txtStartDate" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtStartDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorStartDate" runat="server" ControlToValidate="txtStartDate" ErrorMessage="Start Date Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" TargetControlID="txtStartDate" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblStartDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStartDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label6" class="col-md-4 control-label">Actual Start Date</asp:Label><asp:TextBox runat="server" ID="TextBox7" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:TextBox Placeholder="MM/dd/yyyy" ID="txtActualStartDate" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtActualStartDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtActualStartDate" ErrorMessage="Start Date Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtActualStartDate" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="Label7" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox9" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblEndDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEndDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:TextBox Placeholder="MM/dd/yyyy" ID="txtEndDate" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxEndDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtEndDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorEndDate" runat="server" ControlToValidate="txtEndDate" ErrorMessage="End Date Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" TargetControlID="txtEndDate" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblEndDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEndDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label8" class="col-md-4 control-label">Actual End Date</asp:Label><asp:TextBox runat="server" ID="TextBox8" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:TextBox Placeholder="MM/dd/yyyy" ID="txtActualEndDate" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtActualEndDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtActualEndDate" ErrorMessage="End Date Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtActualEndDate" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="Label9" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox11" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblMyFavorite1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMyFavorite1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtMyFavorite" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorMyFavorite" runat="server" ControlToValidate="txtMyFavorite" ErrorMessage="My Favorite Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblMyFavorite2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMyFavorite2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblRevenue1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRevenue1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtRevenue" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorRevenue" runat="server" ControlToValidate="txtRevenue" ErrorMessage="Revenue Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblRevenue2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRevenue2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblCompaigntype1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCompaigntype1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <%--<asp:TextBox ID="txtCompaigntype" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                            <asp:DropDownList ID="DrpCampaignType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCompaigntype" runat="server" ControlToValidate="DrpCampaignType" ErrorMessage="Compaign Type Required." CssClass="Validation" ValidationGroup="s" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblCompaigntype2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCompaigntype2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblBudget1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBudget1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtBudget" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ValidChars="." ID="FilteredTextBoxExtenderBudget" TargetControlID="txtBudget" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblBudget2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBudget2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblExpectedCost1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtExpectedCost1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtExpectedCost" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ValidChars=".00" ID="FilteredTextBoxExtenderExpectedCost" TargetControlID="txtExpectedCost" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblExpectedCost2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtExpectedCost2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblActualCost1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActualCost1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtActualCost" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ValidChars="." ID="FilteredTextBoxExtenderActualCost" TargetControlID="txtActualCost" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblActualCost2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActualCost2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblImpressions1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtImpressions1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtImpressions" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorImpressions" runat="server" ControlToValidate="txtImpressions" ErrorMessage="Impressions Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblImpressions2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtImpressions2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblObjective1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtObjective1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtObjective" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorObjective" runat="server" ControlToValidate="txtObjective" ErrorMessage="Objective Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblObjective2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtObjective2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblStatus1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStatus1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:DropDownList ID="DrpStatus" runat="server" CssClass="form-control select2me" Width="233px"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorStatus" runat="server" ControlToValidate="DrpStatus" ErrorMessage="Status Required." CssClass="Validation" InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblStatus2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStatus2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label15" class="col-md-4 control-label">Question Group</asp:Label><asp:TextBox runat="server" ID="TextBox5" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:DropDownList ID="drpQutionGrup" runat="server" CssClass="form-control select2me" Style="width: 177px;"></asp:DropDownList>
                                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="drpQutionGrup" ErrorMessage="Select Question Gruop is Required." CssClass="Validation" InitialValue="0" ValidationGroup="startQu"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="Label16" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox12" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label17" class="col-md-4 control-label">Search Title</asp:Label><asp:TextBox runat="server" ID="TextBox14" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-7" style="width: 42.333%!important">
                                                                            <%--<asp:TextBox ID="txtopportunity_name" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                            <asp:DropDownList ID="DrpSearchTitle" runat="server" CssClass="form-control select2me" Style="width: 107px;"></asp:DropDownList>
                                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DrpSearchTitle" ErrorMessage="Select Search Title is Required." CssClass="Validation" InitialValue="0" ValidationGroup="startQu"></asp:RequiredFieldValidator>

                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="DrpOppertunityName" ErrorMessage="Opportunity Name Required." CssClass="Validation"  ValidationGroup="s"  InitialValue="0"  ></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <asp:Panel ID="pnlSearchbutton" runat="server" Visible="false">
                                                                            <div class="col-md-1">
                                                                                <asp:Button ID="btnSearch" runat="server" class="btn default" ValidationGroup="startQu" Text="Start" Style="height: 33px; text-align: center" OnClick="btnSearch_Click" />
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="portlet box blue tabbable">
                                                                <div class="portlet-title">
                                                                    <div class="caption">
                                                                        <i class="fa fa-gift"></i>Content
                                                                    </div>
                                                                </div>
                                                                <div class="portlet-body">
                                                                    <div class=" portlet-tabs">
                                                                        <ul class="nav nav-tabs">
                                                                            <li class="active">
                                                                                <a href="#portlet_tab1" data-toggle="tab" style="color: #5b9bd1">Contents in Language 1</a>
                                                                            </li>
                                                                            <li>
                                                                                <a href="#portlet_tab2" data-toggle="tab" style="color: #5b9bd1">Contents in Language 2</a>
                                                                            </li>
                                                                            <li>
                                                                                <a href="#portlet_tab3" data-toggle="tab" style="color: #5b9bd1">Contents in Language 3</a>
                                                                            </li>
                                                                        </ul>
                                                                        <div class="tab-content">
                                                                            <div class="tab-pane active" id="portlet_tab1">
                                                                                <div class="row">
                                                                                    <div class="form-group" style="color: ">
                                                                                       
                                                                                        <asp:Label runat="server" ID="lblContents1s" class="col-md-4 control-label" Visible="false"></asp:Label><asp:TextBox runat="server" ID="txtContents1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <div class="col-md-8" style="padding-left: 59px;">
                                                                                            <asp:TextBox ID="txtContents" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox>

                                                                                        </div>
                                                                                        <asp:Label runat="server" ID="lblContents2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtContents2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="tab-pane" id="portlet_tab2">
                                                                                <div class="row">
                                                                                    <div class="form-group" style="color: ">
                                                                                        <asp:Label runat="server" ID="Label10" class="col-md-4 control-label" Visible="false"></asp:Label><asp:TextBox runat="server" ID="TextBox2" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <div class="col-md-8" style="padding-left: 59px;">
                                                                                            <asp:TextBox ID="txtContentsCampaignLanguage2" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

                                                                                        </div>
                                                                                        <asp:Label runat="server" ID="Label11" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox10" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="tab-pane" id="portlet_tab3">
                                                                                <div class="row">
                                                                                    <div class="form-group" style="padding-left: 59px;">
                                                                                        <asp:Label runat="server" ID="Label12" class="col-md-4 control-label" Visible="false"></asp:Label><asp:TextBox runat="server" ID="TextBox13" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtContentsCampaignLanguage3" runat="server" CssClass="form-control"  TextMode="MultiLine"></asp:TextBox>

                                                                                        </div>
                                                                                        <asp:Label runat="server" ID="Label13" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox15" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblReferURL1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReferURL1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-6">
                                                                            <asp:TextBox ID="txtReferURL" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorReferURL" runat="server" ControlToValidate="txtReferURL" ErrorMessage="Refer Url Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblReferURL2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReferURL2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                            <%-- <div class="row">

                                                                <div class="form-group">
                                                                    <label class="control-label col-md-3">Customer/Supplier Social Media Name</label>
                                                                    <div class="col-md-6">
                                                                        <asp:TextBox ID="select2_sample51" runat="server" CssClass="form-control select2"></asp:TextBox>
                                                                       
                                                                    </div>
                                                                </div>

                                                            </div>--%>


                                                            <div class="alert alert-info">
                                                                <strong>Performing Team And Tasks</strong>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblTeamName1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTeamName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpTeamName" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpTeamName_SelectedIndexChanged"></asp:DropDownList>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblTeamName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTeamName2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label14" class="col-md-4 control-label">Assigned Team Leader</asp:Label><div class="col-md-8">
                                                                            <asp:DropDownList ID="DrpAssignedTeamLeader" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DrpAssignedTeamLeader" ErrorMessage="Assigned Team Leader Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <asp:Panel ID="pnl" runat="server" Visible="false">
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblMyItems1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMyItems1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtMyItems" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorMyItems" runat="server" ControlToValidate="txtMyItems" ErrorMessage="My Items Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblMyItems2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMyItems2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblTypeID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTypeID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                <asp:DropDownList ID="drpTypeID" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblTypeID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTypeID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>


                                                                </div>
                                                                <div class="row">


                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblTrackerText1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTrackerText1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtTrackerText" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTrackerText" runat="server" ControlToValidate="txtTrackerText" ErrorMessage="Tracker Text Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblTrackerText2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTrackerText2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblCurrency1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCurrency1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtCurrency" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCurrency" runat="server" ControlToValidate="txtCurrency" ErrorMessage="Currency Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblCurrency2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCurrency2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>


                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblAssignedUser1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAssignedUser1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtAssignedUser" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssignedUser" runat="server" ControlToValidate="txtAssignedUser" ErrorMessage="Assigned User Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblAssignedUser2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAssignedUser2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblCreatedDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCreatedDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                <asp:TextBox Placeholder="MM/dd/yyyy" ID="txtCreatedDate" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxCreatedDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtCreatedDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCreatedDate" runat="server" ControlToValidate="txtCreatedDate" ErrorMessage="Created Date Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" TargetControlID="txtCreatedDate" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblCreatedDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCreatedDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblCRUP_ID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRUP_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                <asp:DropDownList ID="drpCRUP_ID" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblCRUP_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRUP_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" id="DivMain" runat="server" style="display: none">
                                                        <uc1:RightPanelUC runat="server" ID="RightPanelUC" TestValue="2" />
                                                    </div>

                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <asp:Panel ID="pnllist" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
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

                                                                                                        Entries</label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-6 col-sm-12" style="padding-top: 18px;">
                                                                                                <div id="sample_1_filter" class="dataTables_filter">
                                                                                                    <label>Search:<input type="search" class="form-control input-small input-inline" placeholder="" aria-controls="sample_1"></label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div>
                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <table class="table table-striped table-bordered table-hover" id="sample_11">
                                                                                                        <thead>
                                                                                                            <tr>
                                                                                                                <th style="width: 60px;">ACTION</th>
                                                                                                                <th>
                                                                                                                    <asp:Label runat="server" ID="lblhTenet" Text="Tenet"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label runat="server" ID="lblhName" Text="Name"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label runat="server" ID="lblhMyItems" Text="My Items"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label runat="server" ID="lblhCompaigntype" Text="Compaign Type"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label runat="server" ID="lblhTypeID" Text="Type Name"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label runat="server" ID="lblhBudget" Text="BudGet"></asp:Label></th>
                                                                                                            </tr>
                                                                                                        </thead>
                                                                                                        <tbody>
                                                                                                            <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="TenentID,ID,Tenet,Name,MyFavorite,MyItems,Compaigntype,Status,TypeID,Budget,Revenue,TrackerText,ReferURL,Contents,Impressions,ActualCost,ExpectedCost,Objective,Currency,StartDate,EndDate,TeamName,AssignedUser,CreatedDate,Active,Deleted,CRUP_ID" DataKeyNames="TenentID,ID,Tenet,Name,MyFavorite,MyItems,Compaigntype,Status,TypeID,Budget,Revenue,TrackerText,ReferURL,Contents,Impressions,ActualCost,ExpectedCost,Objective,Currency,StartDate,EndDate,TeamName,AssignedUser,CreatedDate,Active,Deleted,CRUP_ID">
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
                                                                                                                                        <asp:LinkButton ID="linkbtnView" CommandName="btnView" class="btn btn-sm red filter-cancel" CommandArgument='<%# Eval("ID") %>' PostBackUrl='<%# "Campaign_Mst.aspx?ID="+ Eval("ID") %>' runat="server">
                                                                                                                                            <i class="fa fa-eye"></i>
                                                                                                                                        </asp:LinkButton>
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <asp:LinkButton ID="btnEdit" CommandName="btnEdit" PostBackUrl='<%# "Campaign_Mst.aspx?ID="+ Eval("ID")%>' CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                                                                                        <%-- <asp:LinkButton ID="LinkButton1" CommandName="btnEdit" PostBackUrl='<%# "Campaign_Mst.aspx?JobRef=" + Eval("ID")+"&JNme="+ Eval("NoteID") %>' CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>--%>
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return confirm('Do you want to Delete Record ?') " CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblTenet" runat="server" Text='<%# Eval("Tenet")%>'></asp:Label></td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label></td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblMyItems" runat="server" Text='<%# Eval("MyItems")%>'></asp:Label></td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblCompaigntype" runat="server" Text='<%#getCompintype(Convert.ToInt32( Eval("Compaigntype")))%>'></asp:Label></td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblTypeID" runat="server" Text='<%# Eval("TypeID")%>'></asp:Label></td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblBudget" runat="server" Text='<%# Eval("Budget")%>'></asp:Label></td>



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
                            </asp:Panel>
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

    <script src="assets/apps/scripts/todo-2.min.js"></script>
    <script>
        jQuery(document).ready(function () {

            TableManaged.init();
        });
    </script>
</asp:Content>
