<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="CRMMainActivities.aspx.cs" Inherits="Web.CRM.CRMMainActivities" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
      <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <div>

        <ul class="page-breadcrumb breadcrumb">
            <li>
                <a href="index.aspx">
                    <asp:Label ID="Label2" runat="server" Text="CRM"></asp:Label></a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <a href="#">
                    <asp:Label ID="Label1" runat="server" Text="Crm Main Activities"></asp:Label></a>
            </li>
        </ul>
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="icon-basket font-green-sharp"></i>
                                <span class="caption-subject font-green-sharp bold uppercase">
                                    <asp:Label ID="Label3" runat="server" Text="CRM Main Activities"></asp:Label>
                                </span>
                            </div>
                            <div class="actions btn-set">
                                <asp:Button ID="btnSubmit" runat="server" class="btn green-haze btn-circle" OnClick="btnSave_Click" Text="Add" ValidationGroup="submit" />
                                <asp:Button ID="btnAdd" Visible="false" runat="server" class="btn green-haze btn-circle" Text="Add New" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="tabbable">
                                <div class="tab-content no-space">
                                    <div class="tab-pane active" id="tab_general1">
                                        <div class="form-body">--%>
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
               <%-- <ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">HOME </a>
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
                                       <asp:Label ID="Label3" runat="server" Text="CRM Main Activities"></asp:Label>
                                        <asp:TextBox Style="color: #333333" ID="txtHeader" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="btnPagereload" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <div id="navigation" runat="server" class="btn-group btn-group-circle btn-group-solid">
                                            <asp:Button ID="btnFirst" class="btn red" runat="server" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" />
                                        </div>
                                        <asp:Button ID="btnSubmit" runat="server" class="btn green-haze btn-circle" OnClick="btnSave_Click" Text="Add" ValidationGroup="submit" />
                                        <asp:Button ID="btnAdd" Visible="false" runat="server" class="btn green-haze btn-circle" Text="Add New" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                        &nbsp;
                                        <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server">E&nbsp;<img src="/assets/global/img/flags/us.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server">A&nbsp;<img src="/assets/global/img/flags/ae.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server">F&nbsp;<img src="/assets/global/img/flags/fr.png" /></asp:LinkButton>
                                    </div>
                                </div>

                                <div class="portlet-body form">
                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general1">
                                                <div class="col-md-12" id="Divmainsub" runat="server">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label4" runat="server" Text="Company"></asp:Label></label><div class="col-md-4">
                                                                    <asp:DropDownList ID="drpComid" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label5" runat="server" Text="Route"></asp:Label></label><div class="col-md-4">
                                                                    <asp:DropDownList ID="DrpRoute_Name" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                </div>
                                                            <%-- <label class="col-md-2 control-label"><asp:Label ID="Label5" runat="server" Text="Activity Perform By"></asp:Label></label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="drpActivitycode" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                </div>--%>
                                                        </div>
                                                        <%--<div class="form-group">
                                                <label class="col-md-2 control-label">
                                                    <asp:Label ID="Label6" runat="server" Text="Ref Type"></asp:Label></label><div class="col-md-4">
                                                        <asp:DropDownList ID="drpReftype" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                    </div>
                                                <label class="col-md-2 control-label">
                                                    <asp:Label ID="Label7" runat="server" Text="Ref Subtype"></asp:Label></label><div class="col-md-4">
                                                        <asp:DropDownList ID="drpRefsubtype" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                    </div>
                                            </div>--%>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label8" runat="server" Text="Type of Activity"></asp:Label></label><div class="col-md-4">
                                                                    <asp:DropDownList ID="drpActivitytype" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label9" runat="server" Text="Performing User"></asp:Label></label><div class="col-md-4">
                                                                    <asp:DropDownList ID="DrpUserCode" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>

                                                                    <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorUSERCODE" runat="server" ErrorMessage="USERCODE Required" ControlToValidate="DrpUserCode" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label10" runat="server" Text="Name in English"></asp:Label></label><div class="col-md-4">
                                                                    <asp:TextBox ID="txtACTIVITYE" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorACTIVITYE" runat="server" ErrorMessage="ACTIVITYE Required" ControlToValidate="txtACTIVITYE" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>

                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label11" runat="server" Text="Name in Language 2"></asp:Label></label><div class="col-md-4">
                                                                    <asp:TextBox ID="txtACTIVITYA" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorACTIVITYA" runat="server" ErrorMessage="ACTIVITYA Required" ControlToValidate="txtACTIVITYA" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label12" runat="server" Text="Name in Language 3"></asp:Label></label><div class="col-md-4">
                                                                    <asp:TextBox ID="txtACTIVITYA2" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorACTIVITYA2" runat="server" ErrorMessage="ACTIVITYA2 Required" ControlToValidate="txtACTIVITYA2" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>

                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label13" runat="server" Text="Reference"></asp:Label></label><div class="col-md-4">
                                                                    <asp:TextBox ID="txtReference" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorReference" runat="server" ErrorMessage="Reference Required" ControlToValidate="txtReference" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label14" runat="server" Text="Global Activity"></asp:Label></label><div class="col-md-4">
                                                                    <asp:CheckBox ID="ckbAmiglobal" runat="server" />
                                                                </div>

                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label15" runat="server" Text="Personal Activity"></asp:Label></label><div class="col-md-4">
                                                                    <asp:CheckBox ID="ckbmyPersonnel" runat="server" />
                                                                </div>
                                                        </div>
                                                        <div class="form-group">

                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label17" runat="server" Text="Repeat ForEver"></asp:Label></label><div class="col-md-4">
                                                                    <asp:CheckBox ID="ckbRepeatforever" runat="server" AutoPostBack="true" OnCheckedChanged="ckbRepeatforever_CheckedChanged" />
                                                                </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label18" runat="server" Text="Repeat Till"></asp:Label></label><div class="col-md-4">
                                                                    <asp:TextBox Placeholder="MM/DD/YYYY" ID="txtREPEATTILL" runat="server" CssClass="form-control"></asp:TextBox><cc1:CalendarExtender ID="TextBoxREPEATTILL_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtREPEATTILL"></cc1:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorREPEATTILL" runat="server" ControlToValidate="txtREPEATTILL" ErrorMessage="REPEATTILL Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                </div>


                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Panel ID="pnlintervaldays" runat="server">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label16" runat="server" Text="Interval Days"></asp:Label></label>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox Placeholder="Interval Days" ID="txtIntervalDays" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                </div>
                                                            </asp:Panel>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label20" runat="server" Text="Estimated Cost"></asp:Label></label><div class="col-md-4">
                                                                    <asp:TextBox ID="txtESTCOST" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorESTCOST" runat="server" ErrorMessage="ESTCOST Required" ControlToValidate="txtESTCOST" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>

                                                            <%--  <label class="col-md-2 control-label">
                                                    <asp:Label ID="Label24" runat="server" Text="UserName"></asp:Label></label><div class="col-md-4">
                                                        <asp:TextBox ID="txtUSERNAME" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorUSERNAME" runat="server" ErrorMessage="USERNAME Required" ControlToValidate="txtUSERNAME" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    </div>--%>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label23" runat="server" Text="Up Dt Time"></asp:Label></label><div class="col-md-4">
                                                                    <asp:TextBox Placeholder="MM/DD/YYYY" ID="txtUPDTTIME" runat="server" CssClass="form-control"></asp:TextBox><cc1:CalendarExtender ID="TextBoxUPDTTIME_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtUPDTTIME"></cc1:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUPDTTIME" runat="server" ControlToValidate="txtUPDTTIME" ErrorMessage="UPDTTIME Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label21" runat="server" Text="Group Code"></asp:Label></label><div class="col-md-4">
                                                                    <asp:DropDownList ID="DrpGroupCode" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    <%-- <asp:TextBox ID="txtGROUPCODE" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                    <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorGROUPCODE" runat="server" ErrorMessage="GROUPCODE Required" ControlToValidate="DrpGroupCode" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label22" runat="server" Text="UserCode Entered"></asp:Label></label><div class="col-md-4">
                                                                    <asp:TextBox ID="txtUSERCODEENTERED" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorUSERCODEENTERED" runat="server" ErrorMessage="USERCODEENTERED Required" ControlToValidate="txtUSERCODEENTERED" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label19" runat="server" Text="Reminder Note"></asp:Label></label><div class="col-md-10">
                                                                    <asp:TextBox ID="txtREMINDERNOTE" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorREMINDERNOTE" runat="server" ErrorMessage="REMINDERNOTE Required" ControlToValidate="txtREMINDERNOTE" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label25" runat="server" Text="Remarks"></asp:Label></label><div class="col-md-10">
                                                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorRemarks" runat="server" ErrorMessage="Remarks Required" ControlToValidate="txtRemarks" ValidationGroup="submit"></asp:RequiredFieldValidator>
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
                                            <h4 class="modal-title">
                                                <asp:Label ID="Label26" runat="server" Text="CRMActivities"></asp:Label></h4>
                                        </div>
                                        <div class="modal-body">
                                            <asp:Label ID="Label27" runat="server" Text="Widget settings form goes here"></asp:Label>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn blue">
                                                <asp:Label ID="Label28" runat="server" Text="Save changes"></asp:Label></button>
                                            <button type="button" class="btn default" data-dismiss="modal">
                                                <asp:Label ID="Label29" runat="server" Text="Close"></asp:Label></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12">
                                    <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                    <div class="portlet box blue">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-user"></i>
                                                <asp:Label ID="Label38" runat="server" Text="CRM Main Activities List"></asp:Label>
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <asp:LinkButton ID="btnlistreload" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>

                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <table class="table table-striped table-bordered table-hover" >
                                                <thead>
                                                    <tr>
                                                        <th style="width: 60px;">
                                                            <asp:Label ID="Label31" runat="server" Text="Action"></asp:Label></th>
                                                        
                                                        <th>
                                                            <asp:Label ID="Label6" runat="server" Text="Activity Name"></asp:Label></th>
                                                        <th>
                                                            <asp:Label ID="Label24" runat="server" Text="Remark"></asp:Label></th>
                                                        <th>
                                                            <asp:Label ID="Label36" runat="server" Text="Status"></asp:Label></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="ListProduct_ItemCommand">
                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="odd gradeX">
                                                                <td>
                                                                    <div class="btn-group">
                                                                        <a data-toggle="dropdown" href="#" class="btn btn-sm blue dropdown-toggle" style="width: 60px;">
                                                                            <asp:Label ID="Label2" runat="server" Text="Action"></asp:Label>
                                                                            <i class="fa fa-angle-down"></i>
                                                                        </a>
                                                                        <ul class="dropdown-menu">
                                                                            <li>
                                                                                <asp:LinkButton ID="LinkButton1" CommandName="btnEdit" CommandArgument='<%# Eval("COMPID")%>' runat="server">
                                                                                    <i class="fa fa-pencil"></i>
                                                                                    <asp:Label ID="Label33" runat="server" Text="Edit"></asp:Label>
                                                                                </asp:LinkButton>

                                                                            </li>
                                                                            <li>
                                                                                <asp:LinkButton ID="LinkButton2" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("COMPID")%>' runat="server">
                                                                                    <i class="fa fa-pencil"></i>
                                                                                    <asp:Label ID="Label34" runat="server" Text="Delete"></asp:Label>
                                                                                </asp:LinkButton>
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                </td>
                                                               <td>
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("ACTIVITYE")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label37" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label35" runat="server" Text='<%# Eval("MyStatus")%>'></asp:Label></td>


                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <!-- END EXAMPLE TABLE PORTLET-->
                                </div>

                            </div>
                            <!-- END PAGE CONTENT-->
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
