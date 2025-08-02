<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="Opportunity_Mst.aspx.cs" Inherits="Web.CRM.Opportunity_Mst" %>

<%@ Register Src="~/CRM/UserControl/ContactTextBox.ascx" TagPrefix="uc1" TagName="ContactTextBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/CRM/UserControl/RightPanelUC.ascx" TagPrefix="uc1" TagName="RightPanelUC" %>
<%@ Register Src="~/CRM/UserControl/RightPNTaskTOAppoint.ascx" TagPrefix="uc1" TagName="RightPNTaskTOAppoint" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }
    </style>
    <script type="text/javascript">
        function ace_itemCustomer(sender, e) {

            var hidtext = $get('<%= hidtext.ClientID %>');

             hidtext.value = e.get_value();

         }
         function ace_itemSupplier(sender, e) {

             var HiddenField1 = $get('<%= HiddenField1.ClientID %>');

            HiddenField1.value = e.get_value();

         }
        function ace_itemContact(sender, e) {

            var HiddenField2 = $get('<%= HiddenField2.ClientID %>');

            HiddenField2.value = e.get_value();

        }
    </script>
    <link href="assets/apps/css/todo-2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <%--<ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="DashBoard.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">Opportunity</a>
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
                                        <asp:Button ID="btnExit" runat="server" class="btn grey-cascade btn-circle" Text="Exit" OnClick="btnExit_Click" />
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
                                                            <div class="alert alert-info">
                                                                <strong>Main Characteristic Data</strong>
                                                            </div>
                                                            <div class="row">


                                                                <div class="col-md-4">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblName1s" class="col-md-4 control-label">Name in Lang#1</asp:Label><asp:TextBox runat="server" ID="txtName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="txtName" ErrorMessage="Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtName2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label1" class="col-md-4 control-label">Name in Lang#2</asp:Label><asp:TextBox runat="server" ID="TextBox1" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtoppname2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtoppname2" ErrorMessage="Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="Label6" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox3" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label7" class="col-md-4 control-label">Name in Lang#3</asp:Label><asp:TextBox runat="server" ID="TextBox4" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtoppname3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtoppname3" ErrorMessage="Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="Label8" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox6" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblopportunity_type1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtopportunity_type1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <%--<asp:TextBox ID="txtopportunity_type" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatoropportunity_type" runat="server" ControlToValidate="ddlType" ErrorMessage="Opportunity Type Required." CssClass="Validation" InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblopportunity_type2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtopportunity_type2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label2" class="col-md-4 control-label">Opportunity Owner</asp:Label><%--<asp:TextBox runat="server" ID="TextBox1" class="col-md-4 control-label"></asp:TextBox>--%><div class="col-md-8">
                                                                            <asp:Label runat="server" ID="lblOpportunityOwner" Text=""></asp:Label>
                                                                        </div>
                                                                        <%--<asp:Label runat="server" ID="Label3" class="col-md-4 control-label"></asp:Label>--%><%--<asp:TextBox runat="server" ID="TextBox2" class="col-md-4 control-label" Visible="false">--%><%--</asp:TextBox>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblConact_id1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtConact_id1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">

                                                                            <%--<asp:DropDownList ID="drocontect" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                                             <asp:TextBox ID="TxtContact" runat="server" MaxLength="150" placeholder="Search Contact Min 5 Character" CssClass="form-control"></asp:TextBox>
                                                                             <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" TargetControlID="TxtContact" ServiceMethod="GetContact" CompletionInterval="1000" EnableCaching="FALSE" CompletionSetCount="20"
                                                                                    MinimumPrefixLength="2" OnClientItemSelected="ace_itemContact" DelimiterCharacters=";, :" FirstRowSelected="false"
                                                                                    runat="server" />

                                                                                <asp:HiddenField ID="HiddenField2" runat="server" />

                                                                        </div>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorCustomerName" CssClass="Validation" runat="server" ErrorMessage="Contact Name Is Required" ControlToValidate="TxtContact" InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        <asp:Label ID="lblCustomer1" runat="server" ForeColor="Red"></asp:Label>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblConact_id2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtConact_id2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblDateClosed1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateClosed1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:TextBox Placeholder="MM/dd/yyyy" ID="txtDateClosed" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxDateClosed_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDateClosed" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDateClosed" runat="server" ControlToValidate="txtDateClosed" ErrorMessage="Date Closed Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" TargetControlID="txtDateClosed" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblDateClosed2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateClosed2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row">


                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblCustomer_id1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCustomer_id1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <div class="input-group" style="width: 100%">
                                                                                <asp:TextBox ID="txtsercustomer" runat="server" MaxLength="150" placeholder="Search Customer Min 5 Character" CssClass="form-control"></asp:TextBox>
                                                                                <cc1:AutoCompleteExtender ID="ace" TargetControlID="txtsercustomer" ServiceMethod="GetCustomer" CompletionInterval="1000" EnableCaching="FALSE" CompletionSetCount="20"
                                                                                    MinimumPrefixLength="5" OnClientItemSelected="ace_itemCustomer" DelimiterCharacters=";, :" FirstRowSelected="false"
                                                                                    runat="server" />

                                                                                <asp:HiddenField ID="hidtext" runat="server" />
                                                                                <%-- <span class="input-group-btn"></span>
                                                                                    <asp:LinkButton ID="btnserchproduct" Style="margin-top: -27px;" CssClass="btn btn-icon-only yellow" runat="server" OnClick="btnserchproduct_Click">
                                                                                 <i class="fa fa-search" ></i>
                                                                                    </asp:LinkButton>--%>
                                                                            </div>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblCustomer_id2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCustomer_id2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblSupplier_id1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplier_id1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <div class="input-group" style="width: 100%">
                                                                                <asp:TextBox ID="txtsupplier" runat="server" MaxLength="150" placeholder="Search Supplier Min 5 Character" CssClass="form-control"></asp:TextBox>

                                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtsupplier" ServiceMethod="GetSupplier" CompletionInterval="1000" EnableCaching="FALSE" CompletionSetCount="20"
                                                                                    MinimumPrefixLength="5" OnClientItemSelected="ace_itemSupplier" DelimiterCharacters=";, :" FirstRowSelected="false"
                                                                                    runat="server" />
                                                                                <span role="status" aria-live="polite" class="ui-helper-hidden-accessible"></span>
                                                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                                <%--<span class="input-group-btn"></span>
                                                                                    <asp:LinkButton ID="btnserchsupplery" Style="margin-top: -27px;" CssClass="btn btn-icon-only yellow" runat="server" OnClick="btnserchsupplery_Click">
                                                                                 <i class="fa fa-search" ></i>
                                                                                    </asp:LinkButton>--%>
                                                                            </div>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblSupplier_id2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplier_id2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblProject_id1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProject_id1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpProject_id" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblProject_id2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProject_id2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label12" class="col-md-4 control-label">Question</asp:Label><asp:TextBox runat="server" ID="TextBox2" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:DropDownList ID="DrpQuestion" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="DrpQuestion" ErrorMessage="Select Question Group is Required." CssClass="Validation" InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="Label14" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox5" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label15" class="col-md-4 control-label">Search Title</asp:Label><asp:TextBox runat="server" ID="TextBox7" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-6">
                                                                            <%--<asp:TextBox ID="txtopportunity_name" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                            <asp:DropDownList ID="DrpSearchTitle" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="DrpSearchTitle" ErrorMessage="Select Search Title is Required." CssClass="Validation" InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator191" runat="server" ControlToValidate="DrpSearchTitle" ErrorMessage="Opportunity Name Required." CssClass="Validation"  ValidationGroup="s"  InitialValue="0"  ></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="Label16" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox8" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <asp:Panel ID="pnlSearchbutton" runat="server" Visible="false">
                                                                            <div class="col-md-2">
                                                                                <asp:Button ID="btnSearch" runat="server" class="btn default" ValidationGroup="startQu" Text="Start" OnClick="btnSearch_Click" Style="height: 33px; text-align: center" />
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="alert alert-info">
                                                                <strong>Opportunity Status</strong>


                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label4" class="col-md-4 control-label">Stage</asp:Label>
                                                                        <div class="col-md-8">
                                                                            <asp:DropDownList ID="DDLStage" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DDLStage" ErrorMessage="Stage Required." CssClass="Validation" ValidationGroup="s" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label3" class="col-md-4 control-label">Loss Reason</asp:Label>
                                                                        <div class="col-md-8">
                                                                            <asp:DropDownList ID="DDLLossReason" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DDLLossReason" ErrorMessage="Loss Reason Required." CssClass="Validation" ValidationGroup="s" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblCampaignName1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCampaignName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <%-- <asp:TextBox ID="txtCampaignName" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                            <asp:DropDownList ID="DDLCampaingName" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorCampaignName" runat="server" ControlToValidate="DDLCampaingName" ErrorMessage="Campaign Name Required." CssClass="Validation" ValidationGroup="s" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblCampaignName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCampaignName2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblAmount1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAmount1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ValidChars="." ID="FilteredTextBoxExtenderAmount" TargetControlID="txtAmount" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblAmount2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAmount2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblAmountBackup1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAmountBackup1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtAmountBackup" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" ValidChars="." TargetControlID="txtAmountBackup" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblAmountBackup2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAmountBackup2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblAmountUSdollar1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAmountUSdollar1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtAmountUSdollar" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" ValidChars="." TargetControlID="txtAmountUSdollar" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblAmountUSdollar2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAmountUSdollar2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblProbability1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProbability1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtProbability" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorProbability" runat="server" ControlToValidate="txtProbability" ErrorMessage="Probability Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblProbability2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProbability2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblAccountName1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAccountName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtAccountName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAccountName" runat="server" ControlToValidate="txtAccountName" ErrorMessage="Account Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblAccountName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAccountName2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:UpdatePanel ID="temname" runat="server">
                                                                <ContentTemplate>
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
                                                                                <asp:Label runat="server" ID="Label9" class="col-md-4 control-label">Team Leader</asp:Label>
                                                                                <div class="col-md-8">
                                                                                    <asp:DropDownList ID="drpassingTeamLeader" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                    <%--<asp:TextBox ID="txtTeamLeader" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpassingTeamLeader" ErrorMessage="Team Leader Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblAssignedName1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAssignedName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtAssignedName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAssignedName" runat="server" ControlToValidate="txtAssignedName" ErrorMessage="Assigned Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblAssignedName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAssignedName2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-4">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblSalesStage1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSalesStage1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtSalesStage" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSalesStage" runat="server" ControlToValidate="txtSalesStage" ErrorMessage="Sales Stage Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblSalesStage2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSalesStage2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblDateEntered1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateEntered1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:TextBox Placeholder="MM/dd/yyyy" ID="txtDateEntered" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxDateEntered_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDateEntered" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDateEntered" runat="server" ControlToValidate="txtDateEntered" ErrorMessage="Date Entered Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" TargetControlID="txtDateEntered" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblDateEntered2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateEntered2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblDateModified1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateModified1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:TextBox Placeholder="MM/dd/yyyy" ID="txtDateModified" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxDateModified_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDateModified" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDateModified" runat="server" ControlToValidate="txtDateModified" ErrorMessage="Date Modified Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" TargetControlID="txtDateModified" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblDateModified2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateModified2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="alert alert-info">
                                                                <strong>Additional Information</strong>


                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-4">
                                                                    <h3 style="font-weight: 600"></h3>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblNextStep1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNextStep1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtNextStep" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNextStep" runat="server" ControlToValidate="txtNextStep" ErrorMessage="Next Step Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblNextStep2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNextStep2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblDescription1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescription1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Description Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblDescription2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescription2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:Panel ID="pnl" runat="server" Visible="false">
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblActive1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                <asp:CheckBox ID="cbActive" runat="server" />
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblActive2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lbllead_source1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtlead_source1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtlead_source" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                                <%-- <asp:DropDownList ID="DDLLeadSource" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorlead_source" runat="server" ControlToValidate="txtlead_source" InitialValue="0" ErrorMessage="Lead Source Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lbllead_source2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtlead_source2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" id="DivMain" runat="server" style="display: none">
                                                        <uc1:RightPanelUC runat="server" ID="RightPanelUC" />
                                                    </div>
                                                    <div class="col-md-4" id="DivAppoint" runat="server" style="display:none;">
                                                        <uc1:RightPNTaskTOAppoint runat="server" ID="RightPNTaskTOAppoint" />
                                                    </div>
                                                    <asp:HiddenField ID="hidICHD" runat="server" />
                                                    <panel id="Panel1" style="padding: 1px; background-color: #fff; border: 1px solid #000; display: none" runat="server">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="close(this);disButton(this);">&times;</button><h4 class="modal-title"><i class="icon-paragraph-justify2"></i>
                                                            <asp:Label ID="lblheder" runat="server" ></asp:Label>
                                                          
                                                          
                                                            <a id="a1" data-dismiss="modal" aria-hidden="true" onclick="close(this)"></a>
                                                        </h4>
                                                    </div>
                                                    <br />
                                                    <div class="block-flat">
                                                        <div class="row">
                                                            <div class="col-md-2 form-group"></div> 
                                                            <div class="col-md-4 form-group" style="width: 420px;">
                                                                <asp:Label ID="lblcompnyname" runat="server" ></asp:Label><abbr class="req"> *</abbr>
                                                                <asp:DropDownList ID="drpcompniyname" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                             <div class="col-md-2 form-group" style="margin-bottom: 0px; padding-top: 20px; left: 39px;">
                                                                   <asp:Button ID="btnselect" class="btn red" runat="server" OnClick="btnselect_Click" Text="Select" />
                                                                 </div> 
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                     </panel>

                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
                                                        BackgroundCssClass="modalBackground" CancelControlID="LinkButton3" Enabled="True"
                                                        PopupControlID="Panel1" TargetControlID="hidICHD">
                                                    </cc1:ModalPopupExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="pnllist" runat="server">
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
                                                                                                            <asp:Label runat="server" ID="lblhopportunity_type" Text="Opportunity Type"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhlead_source" Text="Lead Source"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhCustomer_id" Text="Customer Id"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhSupplier_id" Text="Supplier Id"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhSalesStage" Text="Sales Stage"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhTeamName" Text="Team Name"></asp:Label></th>

                                                                                                    </tr>
                                                                                                </thead>
                                                                                                <tbody>
                                                                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="OpportID" DataKeyNames="OpportID">
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
                                                                                                                                <asp:LinkButton ID="linkbtnView" CommandName="btnView" class="btn btn-sm red filter-cancel" CommandArgument='<%# Eval("OpportID") %>' PostBackUrl='<%# "Opportunity_Mst.aspx?ID="+ Eval("OpportID") %>' runat="server">
                                                                                                                                            <i class="fa fa-eye"></i>
                                                                                                                                </asp:LinkButton>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" PostBackUrl='<%# "Opportunity_Mst.aspx?ID="+ Eval("OpportID")%>' CommandArgument='<%# Eval("OpportID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return confirm('Do you want to Delete Record ?') " CommandArgument='<%# Eval("OpportID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>

                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblopportunity_type" runat="server" Text='<%# GetOppertunityType(Convert.ToInt32(Eval("OpportID").ToString()))%>'></asp:Label></td>
                                                                                                                <td>

                                                                                                                    <asp:Label ID="lbllead_source" runat="server" Text='<%# Eval("lead_source")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <%--  <asp:Label ID="lblCustomer_id" runat="server" Text='<%# ((CRM.DMSMaster)Page.Master).GetSupplierName(Convert.ToInt32(Eval("Customer_id").ToString())) %>'></asp:Label>--%>
                                                                                                                    <asp:Label ID="lblCustomer_id" runat="server" Text='<%# Eval("Customer_Name")%>'></asp:Label></td>


                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblSupplier_id" runat="server" Text='<%#Eval("Supplier_Name")%>'></asp:Label></td>

                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblSalesStage" runat="server" Text='<%# Eval("SalesStage")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblTeamName" runat="server" Text='<%# Eval("TeamName")%>'></asp:Label></td>




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
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:LinkButton ID="btntest4" class="btn blue" runat="server" Visible="false"></asp:LinkButton>
        <panel id="ReceivedSign1" style="padding: 1px; background-color: #fff; border: 1px solid #000; display: none" runat="server">
        <div class="modal-header">
            <asp:LinkButton ID="LinkButton3" class="close" runat="server"  ><asp:Label ID="Label31" runat="server" Text="Cancel" ></asp:Label>
</asp:LinkButton>
           <%-- <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>--%>
            <h4 class="modal-title"> <b>  <asp:Label ID="Label13" runat="server" Text ="All Ready Exit"   ></asp:Label> </b></h4>
        </div>
        <div class="modal-body">
            <div class="row">     
                <div class="portlet-body">
                        <div class="tabbable">
                            <table class="table table-striped table-bordered table-hover" >
                                <thead>
                                    <tr>                                      
                                        <th><asp:Label ID="Label32" runat="server" Text="Contact Name" ></asp:Label></th>
                                        <th><asp:Label ID="Label33" runat="server" Text="Mobile Number" ></asp:Label></th>
                                        <th><asp:Label ID="Label34" runat="server" Text="Email" ></asp:Label></th>                                       
                                        <th><asp:Label ID="Label35" runat="server" Text="Fax Number" ></asp:Label></th>
                                        <th><asp:Label ID="Label36" runat="server" Text="Busphone" ></asp:Label></th>
                                        

                                    </tr>
                     </thead>
                                <tbody>
                                    
                                
                 <tr>                                      
                                        <td><asp:Label ID="labelCopop" runat="server" ></asp:Label></td>
                                        <td><asp:Label ID="lblmopop" runat="server"></asp:Label></td>
                                        <td><asp:Label ID="lblEmailpop" runat="server" ></asp:Label></td>                                       
                                        <td><asp:Label ID="lblFaxpop" runat="server"></asp:Label></td>
                                        <td><asp:Label ID="lblBuspop" runat="server"></asp:Label></td>
                    

                                    </tr>

              
                                       
                                    </tbody>
                  </table>
                            </div> 
                    </div> 
                
                </div>
            </div>
        <div class="modal-footer">
           <%-- <asp:Button ID="btnsend1" class="btn blue" runat="server" Text="Send" OnClick ="btnsend_Click" />
            <asp:LinkButton ID="btnEngineerSign" class="btn blue" runat="server" >Submit</asp:LinkButton>--%>
           <asp:Button ID="btnYes" runat="server" class="btn green-haze btn-circle" Text="Yes" OnClick="btnYes_Click" />
           <asp:Button ID="btnNo" runat="server" class="btn red-haze btn-circle" Text="No" OnClick="btnNo_Click" />
            
          
        </div>

    </panel>
        <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" DynamicServicePath=""
            BackgroundCssClass="modalBackground" CancelControlID="LinkButton3" Enabled="True"
            PopupControlID="ReceivedSign1" TargetControlID="btntest4">
        </cc1:ModalPopupExtender>



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
    <script src="assets/apps/scripts/todo-2.min.js"></script>

</asp:Content>
