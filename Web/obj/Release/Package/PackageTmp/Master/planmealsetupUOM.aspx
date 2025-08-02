<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="planmealsetupUOM.aspx.cs" Inherits="Web.Master.planmealsetupUOM" %>

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
                <%--<ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">planmealsetup </a>
                    </li>
                </ul>--%>
                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <asp:Panel ID="PanelError" runat="server" Visible="false">
                    <div class="alert alert-danger alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>
                                        <asp:Label runat="server" ID="lblnew" Text="View"></asp:Label>
                                        <asp:Label runat="server" ID="lblHeader"></asp:Label>
                                        <asp:Label runat="server" ID="lblhLast" Text="For Plan & Meal"></asp:Label>
                                        <asp:TextBox Style="color: #333333" ID="txtHeader" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="btnPagereload" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <%-- <div class="btn-group btn-group-circle btn-group-solid">
                                            <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />
                                        </div>--%>

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
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblPlanID1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPlanID1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpPlanID" runat="server" CssClass="form-control select2me input-medium"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorPlan" runat="server" ErrorMessage="Plan Required." ControlToValidate="drpPlanID" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPlanID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPlanID2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--    </div>
                                                        <div class="row">--%>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblMealID1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMealID1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpMealID" runat="server" CssClass="form-control select2me input-medium"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Meal Type Required." ControlToValidate="drpMealID" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblMealID2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMealID2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:Button ID="btnsearch" CssClass="btn yellow-crusta dz-square" runat="server" Text="Search" OnClick="btnsearch_Click" />
                                                            </div>
                                                        </div>

                                                        <%-- List --%>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                                                <div class="portlet box green">
                                                                    <div class="portlet-title">
                                                                        <div class="caption">
                                                                            <i class="fa fa-edit"></i>
                                                                            <asp:Label ID="lblPNew" runat="server" Text="View"></asp:Label>
                                                                            &nbsp;Product&nbsp;
                                                                                <asp:Label ID="lblPhLast" runat="server" Text="For Plan & Meal"></asp:Label>
                                                                        </div>
                                                                        <div class="tools">
                                                                            <a href="javascript:;" class="collapse"></a>
                                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                            <a href="javascript:;" class="reload"></a>
                                                                            <a href="javascript:;" class="remove"></a>
                                                                        </div>
                                                                        <div class="actions btn-set">
                                                                            <asp:Button ID="btnCopyPlan" runat="server" CssClass="btn blue btn-circle" Text="Copy Plan" />
                                                                            <asp:Button ID="btnAdd" runat="server" class="btn yellow btn-circle" ValidationGroup="submit" Text="Add New" OnClick="btnAdd_Click" />
                                                                            <asp:Button ID="btnCancel" runat="server" class="btn red btn-circle" OnClick="btnCancel_Click" Text="Cancel" />


                                                                            <asp:Panel ID="PenelCopyplan" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto">

                                                                                <div class="row" style="position: fixed; left: 20%; top: 4%; width: auto">
                                                                                    <div class="col-md-12">
                                                                                        <div class="portlet box purple">
                                                                                            <div class="portlet-title">
                                                                                                <div class="caption">
                                                                                                    <i class="fa fa-globe"></i>
                                                                                                    Copy Plan
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="portlet-body">
                                                                                                <div class="tabbable">
                                                                                                    <div class="tab-content no-space">
                                                                                                        <div class="form-body">
                                                                                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                                                                <ContentTemplate>
                                                                                                                    <div class="form-group">
                                                                                                                        <div class="col-md-12">

                                                                                                                            <div class="col-md-6">
                                                                                                                                <div class="form-group" style="margin-top: 1px; margin-top: 4px; margin-left: 10px;">
                                                                                                                                    <asp:Label ID="Label9" runat="server" ForeColor="Black" class="col-md-3 control-label" Text="From Plan"></asp:Label>
                                                                                                                                    <div class="col-md-8">
                                                                                                                                        <asp:DropDownList ID="drpFromPlan" CssClass="form-control input-medium" runat="server"></asp:DropDownList>
                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpFromPlan" InitialValue="0" ErrorMessage="From Plan Required." CssClass="Validation" ValidationGroup="Copyplan"></asp:RequiredFieldValidator>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </div>

                                                                                                                            <div class="col-md-6">
                                                                                                                                <div class="form-group" style="margin-top: 1px; margin-top: 4px; margin-left: 10px;">
                                                                                                                                    <asp:Label ID="Label10" runat="server" ForeColor="Black" class="col-md-3 control-label" Text="To Plan"></asp:Label>
                                                                                                                                    <div class="col-md-6">
                                                                                                                                        <asp:DropDownList ID="drpTOPlan" CssClass="form-control input-medium" runat="server"></asp:DropDownList>
                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="drpTOPlan" InitialValue="0" ErrorMessage="To Plan Required." CssClass="Validation" ValidationGroup="Copyplan"></asp:RequiredFieldValidator>
                                                                                                                                        <asp:Label ID="lblmessege" Visible="false" ForeColor="Red" runat="server" Text=""></asp:Label>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </ContentTemplate>
                                                                                                                <Triggers>
                                                                                                                    <%--<asp:AsyncPostBackTrigger ControlID="txtHoldDate" EventName="TextChanged" />--%>
                                                                                                                    <%--<asp:AsyncPostBackTrigger ControlID="SetDay2" EventName="TextChanged" />--%>
                                                                                                                </Triggers>
                                                                                                            </asp:UpdatePanel>

                                                                                                            <div class="modal-footer">
                                                                                                                <asp:LinkButton ID="lnkbtnCopyplan" class="btn green-haze modalBackgroundbtn-circle" ValidationGroup="Copyplan" runat="server" OnClick="lnkbtnCopyplan_Click"> Copy</asp:LinkButton>
                                                                                                                <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                <asp:Button ID="btnCCancel" runat="server" class="btn btn-default" Text="Cancel" />

                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </asp:Panel>
                                                                            <cc1:ModalPopupExtender ID="ModalPopupExtender7" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="btnCCancel" Enabled="True" PopupControlID="PenelCopyplan" TargetControlID="btnCopyPlan"></cc1:ModalPopupExtender>

                                                                        </div>
                                                                    </div>
                                                                    <div class="portlet-body">
                                                                        <asp:Panel ID="pnlhide" Visible="false" runat="server">

                                                                            <table class="table table-striped table-hover table-bordered">

                                                                                <thead>
                                                                                    <tr>
                                                                                        <th style="width: 44%"></th>
                                                                                        <th style="width: 10%">Day
                                                                                        </th>
                                                                                        <th style="width: 10%">Wek
                                                                                        </th>
                                                                                        <th style="width: 10%">Mth
                                                                                        </th>
                                                                                        <th style="width: 10%">Yr
                                                                                        </th>
                                                                                        <th></th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <td style="text-align: right; font-size: x-large">Allowed number of the times :-->></td>
                                                                                    <td style="padding-left: 5px; padding-right: 5px;">
                                                                                        <asp:TextBox ID="txtMealRepeatInDay" Width="100%" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtMealRepeatInDay_TextChanged"  Text="2"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtMealRepeatInDay" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                    </td>
                                                                                    <td style="padding-left: 5px; padding-right: 5px;">
                                                                                        <asp:TextBox ID="txtMealRepeatInWeek" Width="100%" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtMealRepeatInWeek_TextChanged" Text="12"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtMealRepeatInWeek" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                    </td>
                                                                                    <td style="padding-left: 5px; padding-right: 5px;">
                                                                                        <asp:TextBox ID="txtMealRepeatInMonth" Width="100%" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtMealRepeatInMonth_TextChanged" Text="48"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" TargetControlID="txtMealRepeatInMonth" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                    </td>
                                                                                    <td style="padding-left: 5px; padding-right: 5px;">
                                                                                        <asp:TextBox ID="txtMealRepeatInYear" Width="100%" CssClass="form-control" runat="server" Text="576"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" TargetControlID="txtMealRepeatInYear" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                    </td>
                                                                                    <td></td>
                                                                                </tbody>
                                                                            </table>


                                                                            <table class="table table-striped table-hover table-bordered">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th style="width: 24%">Product
                                                                                        </th>
                                                                                        <th style="width: 10%">UOM
                                                                                        </th>
                                                                                        <th style="width: 10%">Serial No
                                                                                        </th>
                                                                                        <th style="width: 10%">Protein
                                                                                        </th>
                                                                                        <th style="width: 10%">Calories
                                                                                        </th>
                                                                                        <th style="width: 10%">Carbs
                                                                                        </th>
                                                                                        <th style="width: 10%">Fat
                                                                                        </th>
                                                                                        <th style="width: 10%">Weight
                                                                                        </th>
                                                                                        <th style="width: 85px;">Active
                                                                                        </th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="drpProduct" AutoPostBack="true" OnSelectedIndexChanged="drpProduct_SelectedIndexChanged" CssClass="form-control select2me input-xlarge" runat="server"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Product Required." ControlToValidate="drpProduct" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="drpUOM" CssClass="form-control select2me input-small" runat="server"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator5" runat="server" ErrorMessage="UOM Required." ControlToValidate="drpUOM" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td style="padding-left: 5px; padding-right: 5px;">
                                                                                        <asp:TextBox ID="txtserial_no" Width="100%" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceNumber" runat="server" ControlToValidate="txtProtainCarb" ErrorMessage="Protain Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" TargetControlID="txtserial_no" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                    </td>
                                                                                    <td style="padding-left: 5px; padding-right: 5px;">
                                                                                        <asp:TextBox ID="txtProtainCarb" Width="100%" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceNumber" runat="server" ControlToValidate="txtProtainCarb" ErrorMessage="Protain Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtProtainCarb" ValidChars="0123456789." FilterType="Custom, numbers" runat="server" />
                                                                                    </td>
                                                                                    <td style="padding-left: 5px; padding-right: 5px;">
                                                                                        <asp:TextBox ID="txtCalories" Width="100%" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCalories" ErrorMessage="Calories Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtCalories" ValidChars="0123456789." FilterType="Custom, numbers" runat="server" />

                                                                                    </td>
                                                                                    <td style="padding-left: 5px; padding-right: 5px;">
                                                                                        <asp:TextBox ID="txtCarbs" Width="100%" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCarbs" ErrorMessage="Carb Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtCarbs" ValidChars="0123456789." FilterType="Custom, numbers" runat="server" />
                                                                                    </td>
                                                                                    <td style="padding-left: 5px; padding-right: 5px;">
                                                                                        <asp:TextBox ID="txtFat" Width="100%" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFat" ErrorMessage="Fat Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtFat" ValidChars="0123456789." FilterType="Custom, numbers" runat="server" />
                                                                                    </td>
                                                                                    <td style="padding-left: 5px; padding-right: 5px;">
                                                                                        <asp:TextBox ID="txtWeight" Width="100%" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtWeight" ErrorMessage="Weight Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtWeight" ValidChars="0123456789." FilterType="Custom, numbers" runat="server" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:CheckBox ID="chkaction" Style="margin-left: 8px;" CssClass="input-inline list-inline" runat="server" />
                                                                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-xs yellow" ValidationGroup="submit" Text="Save" OnClick="btnSave_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                        <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>
                                                                                        <asp:Label runat="server" ID="Label8" Text="Serial No"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label runat="server" ID="Label1" Text="Plan name"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label runat="server" ID="Label2" Text="Meal name"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label runat="server" ID="Label3" Text="Prot/Cal/Cbs/Fat/Wght"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblProd1" runat="server" Text="Product Name"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="Label11" runat="server" Text="UOM"></asp:Label></th>                                                                                    </th>

                                                                                    <th style="width: 60px;">ACTION</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <asp:ListView ID="Listview3" runat="server" OnItemCommand="Listview3_ItemCommand">
                                                                                    <ItemTemplate>
                                                                                        <tr>

                                                                                            <td>
                                                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("serial_no")%>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblPlanID" runat="server" Text='<%# PlaneName(Convert.ToInt32(Eval("planid")))%>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblMealID" runat="server" Text='<%# MealN(Convert.ToInt32(Eval("MealType"))) %>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label4" runat="server" Text='<%# getprotein(Convert.ToDecimal(Eval("Protein")),Convert.ToDecimal(Eval("Calories")),Convert.ToDecimal(Eval("Carbs")),Convert.ToDecimal(Eval("Fat")),Convert.ToDecimal(Eval("ItemWeight"))) %>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblProd2" runat="server" Text='<%# GetProd(Convert.ToInt64(Eval("MYPRODID"))) %>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label12" runat="server" Text='<%# GetUOM(Convert.ToInt32(Eval("UOM"))) %>'></asp:Label></td>

                                                                                            <td>
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:LinkButton ID="btnEdit" CommandName="btnEdit11" CommandArgument='<%# Eval("PlanID")+","+ Eval("MealType")+","+ Eval("MYPRODID")+","+ Eval("UOM")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                        <td>
                                                                                                            <asp:LinkButton ID="btnDelete" CommandName="btnDelete11" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("PlanID")+","+ Eval("MealType")+","+ Eval("MYPRODID")+","+ Eval("UOM")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                                        <%-- <td><asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("JobId")%>' CommandName="btnPrint" CommandArgument='<%# Eval("JobId")%>' runat="server" class="btn btn-sm green filter-submit margin-bottom"><i class="fa fa-print"></i></asp:LinkButton></td>--%>
                                                                                                    </tr>
                                                                                                </table>

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

                                                        <%-- End List --%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                    <div class="portlet box blue">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-edit"></i>
                                                <asp:Label runat="server" ID="Label5"></asp:Label>
                                                List
                                                <asp:Label runat="server" ID="Label6" ForeColor="Yellow" Font-Bold="true" Text="MSG" Style="margin-left: 10px;"></asp:Label>
                                            </div>
                                            <div class="tools">
                                                <%--<a href="javascript:;" class="collapse"></a>--%>
                                                <asp:LinkButton ID="lblcollapse" CssClass="collapse" runat="server"></asp:LinkButton>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <%--<a href="javascript:;" class="reload"></a>--%>
                                                <asp:LinkButton ID="lblreloadd" OnClick="lblreloadd_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                        </div>
                                        <div class="portlet-body">

                                            <table class="table table-striped table-hover table-bordered" id="sample_5">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhPlanID" Text="Plan name"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhMealID" Text="Meal name"></asp:Label></th>
                                                        <th style="width: 60px;">ACTION</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand">

                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("PlanID")+","+ Eval("MealType") %>' CommandName="btnview">
                                                                        <asp:Label ID="lblPlanID" runat="server" Text='<%# PlaneName(Convert.ToInt32(Eval("planid")))%>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("PlanID")+","+ Eval("MealType") %>' CommandName="btnview">
                                                                        <asp:Label ID="lblMealID" runat="server" Text='<%# MealN(Convert.ToInt32(Eval("MealType"))) %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("PlanID")+","+ Eval("MealType")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("PlanID")+","+ Eval("MealType")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                        </tr>
                                                                    </table>
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
                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
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
                                                                                                        <asp:Label runat="server" ID="lblhPlanID" Text="Plan name"></asp:Label></th>                                                                                                  
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhMealID" Text="Meal name"></asp:Label></th>

                                                                                                    <th style="width: 60px;">ACTION</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="" DataKeyNames="">
                                                                                                    <LayoutTemplate>
                                                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                                                        </tr>
                                                                                                    </LayoutTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("PlanID")+","+ Eval("MealType") %>' CommandName="btnview">
                                                                                                                    <asp:Label ID="lblPlanID" runat="server" Text='<%# PlaneName(Convert.ToInt32(Eval("planid")))%>'></asp:Label>
                                                                                                                </asp:LinkButton>
                                                                                                            </td>                                                                                                           
                                                                                                            <td>
                                                                                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("PlanID")+","+ Eval("MealType") %>' CommandName="btnview">
                                                                                                                    <asp:Label ID="lblMealID" runat="server" Text='<%# MealN(Convert.ToInt32(Eval("MealType"))) %>'></asp:Label>
                                                                                                                </asp:LinkButton>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <table>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("PlanID")+","+ Eval("MealType")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("PlanID")+","+ Eval("MealType")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>                                                                                                                        
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
                                                                                        <ul class="pagination">
                                                                                            <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_fist">                                                                                               
                                                                                                <asp:LinkButton ID="btnfirst1" OnClick="btnfirst_Click" runat="server"> First</asp:LinkButton>
                                                                                            </li>
                                                                                            <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_Next">                                                                                               
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
                            </asp:UpdatePanel>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text=" Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="assets/admin/layout4/img/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
