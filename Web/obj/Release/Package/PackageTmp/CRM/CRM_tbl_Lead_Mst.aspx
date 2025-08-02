<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="CRM_tbl_Lead_Mst.aspx.cs" Inherits="Web.CRM.CRM_tbl_Lead_Mst" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/CRM/UserControl/ProjectUC.ascx" TagPrefix="uc1" TagName="ProjectUC" %>
<%@ Register Src="~/CRM/UserControl/SupplierUC.ascx" TagPrefix="uc1" TagName="SupplierUC" %>
<%@ Register Src="~/CRM/UserControl/RightPanelUC.ascx" TagPrefix="uc1" TagName="RightPanelUC" %>
<%@ Register Src="~/CRM/UserControl/RightPanelUC.ascx" TagPrefix="uc2" TagName="RightPanelUC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }

        .abc {
            width: 196px !important;
        }

        .form-horizontal .control-label {
            margin-bottom: 0;
            padding-top: 7px;
            text-align: end;
        }
    </style>
    <script type="text/javascript">
        function ace_itemSelected(sender, e) {

            var hidtext = $get('<%= hidtext.ClientID %>');

            hidtext.value = e.get_value();

        }
        function ace_itemCoutry(sender, e) {

            var HiddenField1 = $get('<%= HiddenField1.ClientID %>');

            HiddenField1.value = e.get_value();

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
                        <a href="#">Add Lead</a>
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
                                        <div class="btn-group btn-group-circle btn-group-solid">
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
                                                                <strong style="font-weight: 600">Relational Information Data</strong>

                                                            </div>

                                                            <%--<asp:Panel ID="paneloppref" runat="server">
                                                                <div class="form-group">
                                                                    <div class="col-md-6">

                                                                        <label class="col-md-4 control-label">Reference Name :</label>
                                                                        <div class="col-md-8">
                                                                            <div class="input-group">
                                                                                <asp:DropDownList ID="drpRefNo123" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                <span class="input-group-btn">
                                                                                    <asp:HyperLink ID="pnlrespop" class="btn default" data-toggle="tooltip" ToolTip="Add New Refersh" runat="server">
                                                                                        <i class="icon-plus" style="color: black; padding-left: 4px;"></i>
                                                                                    </asp:HyperLink>
                                                                                </span>
                                                                            </div>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                                ControlToValidate="drpRefNo123" ErrorMessage="Select Reference Name." CssClass="Validation"
                                                                                ValidationGroup="s" InitialValue="0"></asp:RequiredFieldValidator>
                                                                            <%-- <asp:LinkButton ID="responsivePop" class="btn default" runat="server"> Add Reference</asp:LinkButton>                                                      
                                                                            <%--<a class="btn default" data-toggle="modal"  id="pnlrespop">Add Reference </a>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">

                                                                        <asp:Label runat="server" ID="lblLeadOwner" class="col-md-4 control-label">Lead Owner</asp:Label><%--<asp:TextBox runat="server" ID="TextBox1" class="col-md-4 control-label"></asp:TextBox><div class="col-md-8">
                                                                            <asp:Label runat="server" ID="lblLeadOwner1" Text=""></asp:Label>
                                                                        </div>
                                                                        <%--<asp:Label runat="server" ID="Label3" class="col-md-4 control-label"></asp:Label>--%><%--<asp:TextBox runat="server" ID="TextBox2" class="col-md-4 control-label" Visible="false">--%><%--</asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </asp:Panel>--%>
                                                            <div class="form-group">
                                                                <div class="col-md-4">

                                                                    <asp:Label runat="server" ID="Label6" class="col-md-4 control-label">Name Lang#1 </asp:Label>
                                                                    <div class="col-md-8" style="float: right">
                                                                        <asp:TextBox ID="txtlead1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtlead1" ErrorMessage="Lead Name 1 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>
                                                                <div class="col-md-4">

                                                                    <asp:Label runat="server" ID="Label7" class="col-md-4 control-label"> Name Lang#2</asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtLeadName2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLeadName2" ErrorMessage="Lead Name 1 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>


                                                                </div>
                                                                <div class="col-md-4">

                                                                    <asp:Label runat="server" ID="Label8" class="col-md-4 control-label">Name Lang#3</asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtLeadName3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtLeadName3" ErrorMessage="Lead Name 1 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <asp:UpdatePanel ID="CaminName" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="form-group">
                                                                        <div class="col-md-6">

                                                                            <asp:Label runat="server" ID="lblcampaign_name1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtcampaign_name1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="Drpcampaign_name" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="Drpcampaign_name_SelectedIndexChanged1"></asp:DropDownList>

                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="Drpcampaign_name" ErrorMessage="Campaign Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                                <%--<asp:TextBox ID="txtcampaign_name" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtcampaign_name" ErrorMessage="Campaign Name Required." CssClass="Validation"  ValidationGroup="s"    ></asp:RequiredFieldValidator>--%>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblcampaign_name2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtcampaign_name2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>

                                                                        <div class="col-md-6">

                                                                            <asp:Label runat="server" ID="lblopportunity_name1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtopportunity_name1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <%--<asp:TextBox ID="txtopportunity_name" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                                <asp:DropDownList ID="DrpOppertunityName" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="DrpOppertunityName" ErrorMessage="Opportunity Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblopportunity_name2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtopportunity_name2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <div class="form-group">
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="Label12" class="col-md-4 control-label">Question</asp:Label><asp:TextBox runat="server" ID="TextBox2" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="DrpQuestion" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="DrpQuestion" ErrorMessage="Select Question Group is Required." CssClass="Validation" InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="Label14" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox3" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                </div>
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="Label15" class="col-md-4 control-label">Search Title</asp:Label><asp:TextBox runat="server" ID="TextBox5" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-6">
                                                                        <%--<asp:TextBox ID="txtopportunity_name" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                        <asp:DropDownList ID="DrpSearchTitle" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="DrpSearchTitle" ErrorMessage="Select Search Title is Required." CssClass="Validation" InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator191" runat="server" ControlToValidate="DrpSearchTitle" ErrorMessage="Opportunity Name Required." CssClass="Validation"  ValidationGroup="s"  InitialValue="0"  ></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="Label16" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox6" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <asp:Panel ID="pnlSearchbutton" runat="server" Visible="false">
                                                                        <div class="col-md-2">
                                                                            <asp:Button ID="btnSearch" runat="server" class="btn default" ValidationGroup="startQu" Text="Start" OnClick="btnSearch_Click" Style="height: 33px; text-align: center" />
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="col-md-6">
                                                                    <asp:Label runat="server" ID="Label21" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox7" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                       
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lblopportunity_amount1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtopportunity_amount1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtopportunity_amount" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtopportunity_amount" ErrorMessage="Opportunity Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                        <cc1:FilteredTextBoxExtender ValidChars="." ID="FilteredTextBoxExtenderBudget" TargetControlID="txtopportunity_amount" FilterType="Custom, numbers" runat="server" />

                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblopportunity_amount2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtopportunity_amount2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                </div>
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="Label3" class="col-md-4 control-label">No. of Employees in the Company</asp:Label><%--<asp:TextBox runat="server" ID="TextBox1" class="col-md-4 control-label" Visible="false"></asp:TextBox>--%>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtNoofEmployees" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtNoofEmployees" FilterType="Custom, numbers" runat="server" />
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtNoofEmployees" ErrorMessage="Stage Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <%--<asp:Label runat="server" ID="Label3" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox3" class="col-md-4 control-label" Visible="false"></asp:TextBox>--%>
                                                                </div>

                                                            </div>

                                                            <div class="form-group">
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lblinvalid_email1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtinvalid_email1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">

                                                                        <asp:TextBox ID="txtinvalid_email" CssClass="form-control" runat="server" ValidationGroup="s"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtinvalid_email"
                                                                            ErrorMessage="Email Required" ValidationGroup="Submit" CssClass="Validation"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ValidationGroup="Submit"
                                                                            CssClass="Validation" ErrorMessage="Email not Valid" ControlToValidate="txtinvalid_email"
                                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                        <%--<asp:TextBox ID="txtinvalid_email" runat="server" CssClass="form-control"></asp:TextBox>

                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="Validation" ValidationGroup="submit" ErrorMessage="Email not Valid" ControlToValidate="txtinvalid_email"
                                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtinvalid_email" ErrorMessage="Email is Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblinvalid_email2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtinvalid_email2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lbldate_entered1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtdate_entered1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox Placeholder="MM/dd/yyyy" ID="txtdate_entered" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxDateClosed_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdate_entered" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDateClosed" runat="server" ControlToValidate="txtdate_entered" ErrorMessage="Date Enter is Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" TargetControlID="txtdate_entered" ValidChars="/" FilterType="Custom, numbers" runat="server" />

                                                                        <%--<asp:TextBox ID="txtdate_entered" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtdate_entered" ErrorMessage="Date Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                            <cc1:CalendarExtender ID="TextBoxDateEntered_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdate_entered" Format="MM/dd/yyyy"></cc1:CalendarExtender>--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lbldate_entered2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtdate_entered2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>




                                                            <div class="alert alert-info">
                                                                <strong>Characteristic Data</strong>

                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lblSalutation1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSalutation1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <%--<asp:TextBox ID="txtSalutation" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                        <asp:DropDownList ID="DrpSalutation" runat="server" CssClass="table-group-action-input form-control input-medium abc">
                                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Mr</asp:ListItem>
                                                                            <asp:ListItem Value="2">Mrs</asp:ListItem>
                                                                            <asp:ListItem Value="3">MissMs</asp:ListItem>
                                                                            <asp:ListItem Value="4">Dr</asp:ListItem>
                                                                            <asp:ListItem Value="5">Prof</asp:ListItem>
                                                                            <asp:ListItem Value="6">Rev</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="DrpSalutation" ErrorMessage="Salutation Required." CssClass="Validation" InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblSalutation2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSalutation2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lblID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtID" ErrorMessage="Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-6">
                                                                    <asp:Label runat="server" ID="lblConact_id1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtConact_id1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtContactName" runat="server" name="name" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorCustomerName" runat="server" ErrorMessage="Contact Name Is Required" ControlToValidate="txtContactName" ValidationGroup="s"></asp:RequiredFieldValidator>

                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblConact_id2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtConact_id2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblTitle1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTitle1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>

                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblTitle2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTitle2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lblCustomer_id1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCustomer_id1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">

                                                                        <div class="input-group" style="width: 100%">
                                                                             <cc1:AutoCompleteExtender ID="ace" TargetControlID="txtsercustomer" ServiceMethod="GetCustomer" CompletionInterval="1000" EnableCaching="FALSE" CompletionSetCount="20"
                                                                            MinimumPrefixLength="5" OnClientItemSelected="ace_itemSelected" DelimiterCharacters=";, :" FirstRowSelected="false"
                                                                            runat="server" />
                                                                        <asp:TextBox ID="txtsercustomer" runat="server" CssClass="form-control" autocomplete="off" placeholder="Search Customer Max 5 Character"></asp:TextBox><span role="status" aria-live="polite" class="ui-helper-hidden-accessible"></span>
                                                                        <asp:HiddenField ID="hidtext" runat="server" />



                                                                            <%--<asp:TextBox ID="txtsercustomer" runat="server" MaxLength="150" CssClass="form-control"></asp:TextBox>
                                                                            <span class="input-group-btn"></span>
                                                                            <asp:LinkButton ID="btnserchproduct" Style="margin-top: -27px;" CssClass="btn btn-icon-only yellow" runat="server" OnClick="btnserchproduct_Click">
                                                                                 <i class="fa fa-search" ></i>
                                                                            </asp:LinkButton>--%>
                                                                        </div>


                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCustomer_id2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCustomer_id2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                </div>
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lblSupplier_id1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplier_id1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <div class="input-group" style="width: 100%">
                                                                            <asp:TextBox ID="txtsupplier" runat="server" MaxLength="150" placeholder="Search Supplier Max 5 Character" CssClass="form-control"></asp:TextBox>
                                                                         <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtsupplier" ServiceMethod="GetSupplier"  CompletionInterval="1000" EnableCaching="FALSE" CompletionSetCount="20"
                                        MinimumPrefixLength="5" OnClientItemSelected="ace_itemCoutry"  DelimiterCharacters=";, :" FirstRowSelected="false"
                                        runat="server" />
                                    <span role="status" aria-live="polite" class="ui-helper-hidden-accessible"></span>
                                     <asp:HiddenField ID="HiddenField1" runat="server" />

                                                                              <%-- <span class="input-group-btn"></span>
                                                                            <asp:LinkButton ID="btnserchsupplery" Style="margin-top: -27px;" CssClass="btn btn-icon-only yellow" runat="server" OnClick="btnserchsupplery_Click">
                                                                                 <i class="fa fa-search" ></i>
                                                                            </asp:LinkButton>--%>
                                                                        </div>

                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpSupplier_id" ErrorMessage="Supplier ID Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblSupplier_id2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplier_id2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                </div>


                                                            </div>


                                                            <div class="form-group">
                                                                <div class="col-md-12">

                                                                    <asp:Label runat="server" ID="Label10" class="col-md-2 control-label">Address</asp:Label>
                                                                    <div class="col-md-10">
                                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address is Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>


                                                                </div>
                                                            </div>

                                                            <div class="form-group">
                                                                <div class="col-md-4">

                                                                    <asp:Label runat="server" ID="lblDo_not_call1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDo_not_call1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:CheckBox ID="Chdonotcall" runat="server" />
                                                                        <%--<asp:TextBox ID="txtDo_not_call" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtDo_not_call" ErrorMessage="Do not call Required." CssClass="Validation"  ValidationGroup="s"    ></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDo_not_call2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDo_not_call2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>

                                                                <div class="col-md-4">

                                                                    <asp:Label runat="server" ID="lblemail_opt_out1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemail_opt_out1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:CheckBox ID="cbemail_opt_out" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblemail_opt_out2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemail_opt_out2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>

                                                                <div class="col-md-4">

                                                                    <asp:Label runat="server" ID="lblSMS_Opt_In1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSMS_Opt_In1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:CheckBox ID="cbSMS_Opt_In" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblSMS_Opt_In2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSMS_Opt_In2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>

                                                            </div>

                                                            <div class="alert alert-info">
                                                                <strong style="font-weight: 600">Lead Source</strong>

                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="Label11" class="col-md-4 control-label">Lead Source from</asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtLeadSourcefrom" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtLeadSourcefrom" ErrorMessage="Lead Source from Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lbllead_source1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtlead_source1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">

                                                                        <asp:DropDownList ID="DDLLeadSource" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="DDLLeadSource" ErrorMessage="Lead Source Required." CssClass="Validation" InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>

                                                                    </div>

                                                                    <asp:Label runat="server" ID="lbllead_source2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtlead_source2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lblRefered_by1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRefered_by1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtRefered_by" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtRefered_by" ErrorMessage="Refered by Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblRefered_by2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRefered_by2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>

                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lblWebsite1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtWebsite1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <%-- <asp:CheckBox ID="cbWebsite" runat="server" />--%>
                                                                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <%--<asp:RegularExpressionValidator ID="regUrl" runat="server" ControlToValidate="txtWebsite" ValidationExpression="^((http|https)://)?([\w-]+\.)+[\w]+(/[\w- ./?]*)?$" CssClass="Validation" Text="Enter a valid URL" />--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblWebsite2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtWebsite2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>

                                                            </div>



                                                            <div class="form-group">
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lblStatus1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStatus1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="drpStatus" ErrorMessage="Status Required." CssClass="Validation" InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblStatus2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStatus2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>

                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lblProject_id1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProject_id1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <div class="input-group" style="width: 90%">
                                                                            <asp:DropDownList ID="drpProject_id" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                            <span class="input-group-btn"></span>

                                                                            <uc1:ProjectUC runat="server" ID="ProjectUC" />
                                                                        </div>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="drpProject_id" ErrorMessage="Project Required." CssClass="Validation" InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                        <asp:Label runat="server" ID="lblProject_id2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProject_id2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="Label9" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox4" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>


                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lblDepartment1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDepartment1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="drpDepartment" ErrorMessage="Department Required." CssClass="Validation" InitialValue="0"  ValidationGroup="s"    ></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDepartment2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDepartment2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lblAssistant1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAssistant1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtAssistant" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtAssistant" ErrorMessage="Assistant Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblAssistant2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAssistant2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%-- <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lblteam_name1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtteam_name1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtteam_name" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtteam_name" ErrorMessage="Team Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblteam_name2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtteam_name2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>--%>

                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="form-group">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblTeamName1s" class="col-md-4 control-label" Text="TeamName"></asp:Label><asp:TextBox runat="server" ID="txtTeamName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                    <asp:DropDownList ID="drpTeamName" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpTeamName_SelectedIndexChanged"></asp:DropDownList>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblTeamName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTeamName2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="Label4" class="col-md-4 control-label">Assigned Team Leader</asp:Label><div class="col-md-8">
                                                                                    <asp:DropDownList ID="DrpAssignedTeamLeader" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DrpAssignedTeamLeader" ErrorMessage="Assigned Team Leader Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                                </div>

                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <div class="form-group">
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lblAssistant_phone1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAssistant_phone1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtAssistant_phone" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtAssistant_phone" ErrorMessage="Assistant phone Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblAssistant_phone2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAssistant_phone2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">

                                                                    <asp:Label runat="server" ID="lbllead_source_description1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtlead_source_description1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox TextMode="MultiLine" ID="txtlead_source_description" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lbllead_source_description2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtlead_source_description2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                            <asp:Panel ID="Vigiblti" runat="server" Visible="false">
                                                                <div class="row" style="visibility: hidden">

                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblaccount_name1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtaccount_name1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtaccount_name" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtaccount_name" ErrorMessage="Account Name Required." CssClass="Validation"   ValidationGroup="s"    ></asp:RequiredFieldValidator>--%>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblaccount_name2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtaccount_name2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row" style="visibility: hidden">

                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblCRUP_ID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRUP_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtCRUP_ID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtCRUP_ID" ErrorMessage="Crup ID Required." CssClass="Validation"  ValidationGroup="s"    ></asp:RequiredFieldValidator>--%>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblCRUP_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRUP_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="row" style="visibility: hidden">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="Label1" class="col-md-4 control-label">Company</asp:Label>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="DDLComapny" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DDLComapny" ErrorMessage="Company Required." CssClass="Validation" InitialValue="0"  ValidationGroup="s" ></asp:RequiredFieldValidator>--%>
                                                                            </div>

                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" id="DivMain" runat="server" style="display: none">

                                                        <uc1:RightPanelUC runat="server" ID="RightPanelUC" />
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

                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" DynamicServicePath=""
                                                        BackgroundCssClass="modalBackground" CancelControlID="LinkButton3" Enabled="True"
                                                        PopupControlID="Panel1" TargetControlID="hidICHD">
                                                    </cc1:ModalPopupExtender>
                                                    <%--<panel id="pnlresponsive" style="padding: 1px; background-color: #fff; border: 1px solid #000; display: none" runat="server">
        <div class="modal-header">
           
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
            <h4 class="modal-title"> <b>  <asp:Label ID="Label18" Text="ADD REFERENCE" runat="server" ></asp:Label> </b></h4>
        </div>
        <div class="modal-body">
            <div class="row">
           
                  <div class="col-md-6">
                                                             <h4><b>Add Reference Name</b></h4>
                                                            <p>
                                                                <asp:TextBox Style="width: 300px;" ID="txtAddReference3" runat="server" class="form-control" maxlength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Add Reference Required" ControlToValidate="txtAddReference3" ValidationGroup="s"></asp:RequiredFieldValidator>

                                                            </p>
                                                            <h4><b>Short Name</b></h4>
                                                            <p>
                                                               <asp:TextBox Style="width: 300px;" ID="txtShortName1" runat="server" class="form-control" maxlength="50"></asp:TextBox>

                                                            </p>
                                                            <h4><b>Remark</b></h4>
                                                            <p>
                                                                <asp:TextBox Style="width: 300px;" ID="txtREMARK2" runat="server" class="form-control" maxlength="500" TextMode="MultiLine"></asp:TextBox>

                                                            </p>

            </div>
        </div>
            </div>
        <div class="modal-footer">
          
            <asp:LinkButton ID="lbButton1" class="btn green-haze btn-circle" ValidationGroup="Submit"  runat="server" OnClick="btnAddNew_Click">AddNew</asp:LinkButton>
                  
                    <asp:Button ID="Button2" runat="server" data-dismiss="modal" class="btn green-haze btn-circle" OnClick="btnCancel2_Click" Text="Cancel" />

        </div>

    </panel>
                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
                                                        BackgroundCssClass="modalBackground" CancelControlID="Button2" Enabled="True"
                                                        PopupControlID="pnlresponsive" TargetControlID="pnlrespop">
                                                    </cc1:ModalPopupExtender>--%>
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
                                                                                                <%--  <select name="sample_1_length" aria-controls="sample_1"  tabindex="-1" title="">
                                                                                            <option value="5">5</option>
                                                                                            <option value="15">15</option>
                                                                                            <option value="20">20</option>
                                                                                            <option value="-1">All</option>
                                                                                        </select>
                                                                                                Entries</label>--%>
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
                                                                                            <table class="table table-striped table-bordered table-hover" style="border-collapse: separate!important">
                                                                                                <thead>
                                                                                                    <tr>
                                                                                                        <th style="width: 60px;">ACTION</th>
                                                                                                        <%--<th style="width: 60px;">Conact Name</th>--%>
                                                                                                        <th style="width: 60px;">Lead Name</th>
                                                                                                        <th style="width: 60px;">Customer Name</th>
                                                                                                        <th style="width: 60px;">Supplier Name</th>
                                                                                                        <th style="width: 60px;">Project Name</th>
                                                                                                        <%--<th style="width: 60px;">Salutation</th>--%>
                                                                                                        <th style="width: 60px;">Title</th>
                                                                                                        <th style="width: 60px;">Referred By</th>
                                                                                                        <th style="width: 60px;">lead Source</th>
                                                                                                        <th style="width: 60px;">Status</th>
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
                                                                                                                    <table>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <asp:LinkButton ID="linkbtnView" CommandName="btnView" class="btn btn-sm red filter-cancel" CommandArgument='<%# Eval("ID") %>' PostBackUrl='<%# "CRM_tbl_Lead_Mst.aspx?ID="+ Eval("ID") %>' runat="server">
                                                                                                                                            <i class="fa fa-eye"></i>
                                                                                                                                </asp:LinkButton>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" PostBackUrl='<%# "CRM_tbl_Lead_Mst.aspx?ID="+ Eval("ID")%>' CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom">  <i class="fa fa-pencil"></i></asp:LinkButton>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return confirm('Do you want to Delete Record ?') " CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblConact_id" runat="server" Text='<%# Eval("LeadName1")%>'></asp:Label></td>

                                                                                                                <td>

                                                                                                                    <asp:Label ID="lblCustomer_id" runat="server" Text='<%# Eval("Customer_Name")%>'></asp:Label>
                                                                                                                    <%-- <asp:Label ID="Label17" runat="server" Text='<%# GetCustomerName((String.IsNullOrEmpty(Convert.ToInt32(Eval("Customer_id").ToString())) ? "0" : Eval("Customer_id")))%>'></asp:Label>--%>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <%-- <asp:Label ID="lblSupplier_id" runat="server" Text='<%# Eval("Supplier_id")%>'></asp:Label>--%>
                                                                                                                    <asp:Label ID="Label17" runat="server" Text='<%# Eval("Supplier_Name") %>'></asp:Label>

                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label18" runat="server" Text='<%# ((Web.CRM.CRMMaster)Page.Master).GetProjectName(Convert.ToInt32(Eval("Project_id").ToString())) %>'></asp:Label>
                                                                                                                    <%--<asp:Label ID="lblProject_id" runat="server" Text='<%#Convert.ToInt32(Eval("Project_id"))==0? "" :Eval("Project_id")%>'></asp:Label>--%></td>
                                                                                                                <%--<td>
                                                                                                                    <asp:Label ID="lblSalutation" runat="server" Text='<%# Eval("Salutation")%>'></asp:Label></td>--%>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblRefered_by" runat="server" Text='<%# Eval("Refered_by")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="Label19" runat="server" Text='<%# ((Web.CRM.CRMMaster)Page.Master).GetLeadSource(Convert.ToInt32(Eval("lead_source").ToString())) %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>

                                                                                                                    <asp:Label ID="Label20" runat="server" Text='<%# ((Web.CRM.CRMMaster)Page.Master).GetLeadStatus(Convert.ToInt32(Eval("Status").ToString())) %>'></asp:Label>
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
                            </asp:Panel>

                            <asp:Panel ID="pnl" runat="server" Visible="false">

                                <div class="portlet box blue" style="visibility: hidden">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-gift"></i>Step:1 
                                        <asp:Label runat="server" ID="Label2"></asp:Label>
                                            <asp:TextBox Style="color: #333333" ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                        </div>
                                        <div class="tools">
                                            <a href="javascript:;" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <asp:LinkButton ID="LinkButton1" OnClick="btnPagereload_Click" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                            <a href="javascript:;" class="remove"></a>
                                        </div>

                                    </div>

                                    <div class="portlet-body" style="visibility: hidden">
                                        <div class="portlet-body form" style="visibility: hidden">
                                            <div class="tabbable" style="visibility: hidden">
                                                <div class="tab-content no-space">
                                                    <div class="tab-pane active" id="tab_general11">
                                                        <div class="form-body">
                                                            <div class="row">
                                                                <div class="col-md-4">
                                                                    <h3 style="font-weight: 600">Additional Information</h3>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-4">
                                                                    <h3 style="font-weight: 600"></h3>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblstatus_description1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtstatus_description1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtstatus_description" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblstatus_description2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtstatus_description2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lbldescription1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtdescription1s" class="col-md-4 control-label" Visible="false" TextMode="MultiLine"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtdescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

                                                                        </div>
                                                                        <asp:Label runat="server" ID="lbldescription2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtdescription2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblaccount_description1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtaccount_description1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:TextBox ID="txtaccount_description" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblaccount_description2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtaccount_description2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblTwitterScreenName1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTwitterScreenName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtTwitterScreenName" runat="server" CssClass="form-control"></asp:TextBox>

                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblTwitterScreenName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTwitterScreenName2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <asp:LinkButton ID="btntest4" class="btn blue" runat="server" Visible="false"></asp:LinkButton>
    <panel id="ReceivedSign1" style="padding: 1px; background-color: #fff; border: 1px solid #000; height: 50%; overflow: auto; display: none;" runat="server">
        <div class="modal-header">
            <asp:LinkButton ID="LinkButton3" class="close" runat="server"  ><asp:Label ID="Label31" runat="server" Text="Cancel" ></asp:Label>
</asp:LinkButton>
           <%-- <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>--%>
            <h4 class="modal-title"> <b>  <asp:Label ID="Label13" runat="server" Text ="All Ready Exit"></asp:Label> </b></h4>
        </div>
        <div class="modal-body" >
            <div class="row">     
                <div class="portlet-body">
                            <div class="table-scrollable" style="height:50%;overflow:auto">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <table class="table table-striped table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Contact Name
                                                    </th>
                                                    <th>Mobile No
                                                    </th>
                                                    <th>Email 
                                                    </th>
                                                    <th>Fax Number
                                                    </th>
                                                   <th>Bus Phone
                                                    </th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:ListView ID="ListViewConatctname" runat="server" OnItemCommand="ListViewConatctname_ItemCommand">
                                                    <LayoutTemplate>
                                                        <tr id="ItemPlaceholder" runat="server">
                                                        </tr>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTenet" runat="server" Text='<%# Eval("PersName1")%>'></asp:Label></td>

                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("MOBPHONE")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("EmailId")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("FaxID")%>'></asp:Label></td>

                                                             <td>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("BUSPHONE1")%>'></asp:Label></td>
                                                            <td>
                                                                 <asp:LinkButton ID="LinkPageavigation" class="btn btn-circle red-sunglo btn-sm" runat="server" CommandName="LinkPageavigation" CommandArgument='<%# Eval("ContactMyID")%>'>Yes</asp:LinkButton></li>
                                                            </td>
                                                         
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
    <script src="assets/apps/scripts/todo-2.min.js"></script>
</asp:Content>
