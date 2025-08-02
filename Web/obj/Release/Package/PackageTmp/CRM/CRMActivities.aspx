<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="CRMActivities.aspx.cs" Inherits="Web.CRM.CRMActivities" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <ul class="page-breadcrumb breadcrumb">
            <li>
                <a href="index.aspx"><asp:Label ID="Label2" runat="server" Text="CRM"></asp:Label></a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <a href="#"><asp:Label ID="Label3" runat="server" Text="CRM Activities"></asp:Label> </a>
            </li>
        </ul>
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="icon-basket font-green-sharp"></i>
                                <span class="caption-subject font-green-sharp bold uppercase"><asp:Label ID="Label4" runat="server" Text="CRM Activities"></asp:Label> </span>
                            </div>
                            <span class="caption-helper"></span>
                            <div class="actions btn-set">
                                <asp:Button ID="btnSubmit"  runat="server" class="btn green-haze btn-circle" OnClick="btnSave_Click" Text="Add" ValidationGroup="submit" />
                                <asp:Button ID="btnAdd"  runat="server" class="btn green-haze btn-circle" Text="Add New" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="portlet-body">
                                <div class="tabbable">
                                    <div class="tab-content no-space">

                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><asp:Label ID="Label5" runat="server" Text="Company"></asp:Label></label><div class="col-md-4">
                                                    <asp:DropDownList ID="drpComid"  runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                </div>

                                             <%--   <label class="col-md-2 control-label"><asp:Label ID="Label6" runat="server" Text="Activity Perform By"></asp:Label></label><div class="col-md-4">
                                                    <asp:DropDownList ID="drpActivitycode"  runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                </div>--%>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><asp:Label ID="Label7" runat="server" Text="Activity Type"></asp:Label></label><div class="col-md-4">
                                                    <asp:DropDownList ID="drpActivitytype"  runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                </div>

                                                <label class="col-md-2 control-label"><asp:Label ID="Label8" runat="server" Text="Ref Type"></asp:Label></label><div class="col-md-4">
                                                    <asp:DropDownList ID="drpReftype"  runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><asp:Label ID="Label9" runat="server" Text="Ref Subtype"></asp:Label></label><div class="col-md-4">
                                                    <asp:DropDownList ID="drpRefsubtype" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                </div>

                                                <label class="col-md-2 control-label"><asp:Label ID="Label10" runat="server" Text="MyLineNo"></asp:Label></label>
                                                <div class="col-md-9" style="width: 335px">
                                                    <div id="spinner1">
                                                        <div class="input-group input-small">
                                                            <asp:TextBox ID="txtMylineno" runat="server" class="spinner-input form-control" MaxLength="3"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Mylineno Required" ControlToValidate="txtMylineno" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            <div class="spinner-buttons input-group-btn btn-group-vertical">
                                                                <button class="btn spinner-up btn-xs blue" type="button">
                                                                    <i class="fa fa-angle-up"></i>
                                                                </button>
                                                                <button class="btn spinner-down btn-xs blue" type="button">
                                                                    <i class="fa fa-angle-down"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><asp:Label ID="Label11" runat="server" Text="User Code"></asp:Label></label><div class="col-md-4">
                                                    <asp:TextBox ID="txtUSERCODE" runat="server"  class="form-control" MaxLength="10"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorUSERCODE" runat="server" ErrorMessage="USERCODE Required" ControlToValidate="txtUSERCODE" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>

                                                <label class="col-md-2 control-label"><asp:Label ID="Label12" runat="server" Text="Reference No"></asp:Label></label><div class="col-md-4">
                                                    <asp:TextBox ID="txtReferenceNo"  runat="server" class="form-control" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorReferenceNo" runat="server" ErrorMessage="ReferenceNo Required" ControlToValidate="txtReferenceNo" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><asp:Label ID="Label13" runat="server" Text="Earlier RefNo"></asp:Label></label><div class="col-md-4">
                                                    <asp:TextBox ID="txtEarlierRefNo"  runat="server" class="form-control" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorEarlierRefNo" runat="server" ErrorMessage="EarlierRefNo Required" ControlToValidate="txtEarlierRefNo" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>

                                                <label class="col-md-2 control-label"><asp:Label ID="Label14" runat="server" Text="NextUser"></asp:Label></label><div class="col-md-4">
                                                    <asp:TextBox ID="txtNextUser" runat="server"  class="form-control" MaxLength="10"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorNextUser" runat="server" ErrorMessage="NextUser Required" ControlToValidate="txtNextUser" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><asp:Label ID="Label15" runat="server" Text="Documnet Name"></asp:Label></label><div class="col-md-4">
                                                    <asp:TextBox ID="txtNextRefNo" runat="server"  class="form-control" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorNextRefNo" runat="server" ErrorMessage="NextRefNo Required" ControlToValidate="txtNextRefNo" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>

                                                <label class="col-md-2 control-label"><asp:Label ID="Label16" runat="server" Text="AmiGlobal"></asp:Label></label><div class="col-md-4">
                                                    <asp:CheckBox ID="ckbAmiglobal" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><asp:Label ID="Label17" runat="server" Text="MyPersonnel"></asp:Label></label><div class="col-md-4">
                                                    <asp:CheckBox ID="ckbMypersonnel" runat="server" />

                                                </div>

                                                <label class="col-md-2 control-label"><asp:Label ID="Label18" runat="server" Text="Activity Perform"></asp:Label></label><div class="col-md-4">
                                                    <asp:TextBox ID="txtActivityPerform" runat="server" class="form-control" MaxLength="1000"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorActivityPerform" runat="server" ErrorMessage="ActivityPerform Required" ControlToValidate="txtActivityPerform" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><asp:Label ID="Label19" runat="server" Text="ReminderNote"></asp:Label></label><div class="col-md-4">
                                                    <asp:TextBox ID="txtREMINDERNOTE" runat="server" class="form-control" MaxLength="1000"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorREMINDERNOTE" runat="server" ErrorMessage="REMINDERNOTE Required" ControlToValidate="txtREMINDERNOTE" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>

                                                <label class="col-md-2 control-label"><asp:Label ID="Label20" runat="server" Text="EstCost"></asp:Label></label><div class="col-md-4">
                                                    <asp:TextBox ID="txtESTCOST" runat="server" class="form-control" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorESTCOST" runat="server" ErrorMessage="ESTCOST Required" ControlToValidate="txtESTCOST" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtESTCOST" FilterMode="ValidChars" runat="server" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><asp:Label ID="Label21" runat="server" Text="GroupCode"></asp:Label></label><div class="col-md-4">
                                                    <asp:TextBox ID="txtGROUPCODE" runat="server" class="form-control" MaxLength="30"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorGROUPCODE" runat="server" ErrorMessage="GROUPCODE Required" ControlToValidate="txtGROUPCODE" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>

                                                <label class="col-md-2 control-label"><asp:Label ID="Label22" runat="server" Text="UserCodeEntered"></asp:Label></label><div class="col-md-4">
                                                    <asp:TextBox ID="txtUSERCODEENTERED" runat="server" class="form-control" MaxLength="10"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorUSERCODEENTERED" runat="server" ErrorMessage="USERCODEENTERED Required" ControlToValidate="txtUSERCODEENTERED" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><asp:Label ID="Label23" runat="server" Text="Up Dt Time"></asp:Label></label><div class="col-md-4">
                                                    <asp:TextBox Placeholder="MM/DD/YYYY" ID="txtUPDTTIME" runat="server" class="form-control" MaxLength="10"></asp:TextBox><cc1:CalendarExtender ID="TextBoxUPDTTIME_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtUPDTTIME"></cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUPDTTIME" runat="server" ControlToValidate="txtUPDTTIME" ErrorMessage="UPDTTIME Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                </div>

                                                <label class="col-md-2 control-label"><asp:Label ID="Label24" runat="server" Text="UserName"></asp:Label></label><div class="col-md-4">
                                                    <asp:TextBox ID="txtUSERNAME" runat="server" class="form-control" MaxLength="15"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorUSERNAME" runat="server" ErrorMessage="USERNAME Required" ControlToValidate="txtUSERNAME" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label"><asp:Label ID="Label25" runat="server" Text="Initial Date"></asp:Label></label><div class="col-md-4">
                                                    <asp:TextBox Placeholder="MM/DD/YYYY" ID="txtInitialDate" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtInitialDate"></cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorInitialDate" runat="server" ControlToValidate="txtInitialDate" ErrorMessage="UPDTTIME Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                </div>
                                                <label class="col-md-2 control-label"><asp:Label ID="Label26" runat="server" Text="DeadLine Date"></asp:Label></label><div class="col-md-4">
                                                    <asp:TextBox Placeholder="MM/DD/YYYY" ID="txtDeadLineDate" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDeadLineDate"></cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtDeadLineDate" runat="server" ControlToValidate="txtDeadLineDate" ErrorMessage="UPDTTIME Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
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
            <div class="modal fade" id="portlet-config" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title"><asp:Label ID="Label27" runat="server" Text="CRM Activities"></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                           <asp:Label ID="Label28" runat="server" Text="Widget settings form goes here"></asp:Label> 
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn blue"><asp:Label ID="Label29" runat="server" Text="Save changes"></asp:Label></button>
                            <button type="button" class="btn default" data-dismiss="modal"><asp:Label ID="Label30" runat="server" Text="Close"></asp:Label></button>
                        </div>
                    </div>
                </div>
            </div>



            <div class="form-horizontal form-row-seperated">
                <div class="portlet light">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="icon-basket font-green-sharp"></i>
                            <span class="caption-subject font-green-sharp bold uppercase"><asp:Label ID="Label31" runat="server" Text="CRM Activities"></asp:Label> 
                            </span>
                        </div>
                        <%--<div class="actions btn-set">
                                <asp:Button ID="addNew" runat="server" Text="Add New" OnClick="addNew_Click" />
                                <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle" Text="Cancel" />
                            </div>--%>
                    </div>
                    <div class="portlet-body">
                        <div class="tabbable">
                            <table class="table table-striped table-bordered table-hover" id="sample_1">
                                <thead>
                                    <tr>
                                        <th style="width: 60px;"><asp:Label ID="Label32" runat="server" Text="Action"></asp:Label></th>
                                        <th><asp:Label ID="Label33" runat="server" Text="Company Name"></asp:Label></th>
                                        <th><asp:Label ID="Label34" runat="server" Text="Document Name"></asp:Label></th>
                                        <th><asp:Label ID="Label35" runat="server" Text="Activity performed"></asp:Label></th>
                                        <th><asp:Label ID="Label36" runat="server" Text="Line #"></asp:Label></th>
                                        <th><asp:Label ID="Label37" runat="server" Text="Activity due Date"></asp:Label></th>
                                        <th><asp:Label ID="Label38" runat="server" Text="Activity Performed By"></asp:Label></th>

                                        <%--<th>Edit</th>
                                            <th>Delete</th>--%>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:ListView ID="liscrmactivities" runat="server" OnItemCommand="Listcrmactivities_ItemCommand">
                                        <LayoutTemplate>
                                            <tr id="ItemPlaceholder" runat="server">
                                            </tr>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <div class="btn-group">
                                                        <a data-toggle="dropdown" href="#" class="btn btn-sm blue dropdown-toggle" style="width: 60px;"><asp:Label ID="Label2" runat="server" Text="Action"></asp:Label> <i class="fa fa-angle-down"></i>
                                                        </a>
                                                        <ul class="dropdown-menu">
                                                            <li>
                                                                <asp:LinkButton ID="LinkButton1" CommandName="btnEdit" CommandArgument='<%# Eval("COMPID")%>' runat="server">  <i class="fa fa-pencil"></i><asp:Label ID="Label39" runat="server" Text="Edit"></asp:Label></asp:LinkButton>

                                                            </li>
                                                            <li>
                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("COMPID")%>' runat="server"> <i class="fa fa-pencil"></i><asp:Label ID="Label40" runat="server" Text="Delete"></asp:Label></asp:LinkButton>

                                                            </li>
                                                            <%--<li>
                                                                    <asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("ID")%>' CommandName="btnPrint" CommandArgument='<%# Eval("ID")%>' runat="server"><i class="fa fa-pencil"></i>Print</asp:LinkButton>
                                                                </li>--%>
                                                        </ul>
                                                    </div>
                                                    <%-- <td>
                                                            <asp:Label ID="Label1123" runat="server" Text=' <%# Container.DataItemIndex + 1  %>'></asp:Label>
                                                        </td>--%>
                                                    <td>
                                                        <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("COMPID") %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblDocumentName" runat="server" Text='<%# Eval("NextRefNo")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblActivityPerform" runat="server" Text='<%# Eval("ActivityPerform")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblLine" runat="server" Text='<%# Eval("MyLineNo")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblUPDTTIME" runat="server" Text='<%# Eval("UPDTTIME")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("COMPID") %>'></asp:Label></td>
                                                    <%--<td>
                                                        <div class="actions btn-set">
                                                            <asp:LinkButton ID="btnEdit" class="btn green-haze btn-circle" CommandName="btnEdit" CommandArgument='<%# Eval("COMPID")%>'  PostBackUrl='<%# "CRMActivities.aspx?ID="+ Eval("COMPID")%>' runat="server">Edit</asp:LinkButton>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="actions btn-set">
                                                            <asp:LinkButton ID="btnDelete" class="btn red-haze btn-circle"  CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("COMPID")%>' runat="server">Delete</asp:LinkButton>
                                                        </div>
                                                    </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="scroll-to-top">
                <i class="icon-arrow-up"></i>
            </div>
        </div>
    </div>

</asp:Content>
