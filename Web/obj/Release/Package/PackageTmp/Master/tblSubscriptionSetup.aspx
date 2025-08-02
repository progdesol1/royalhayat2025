<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="tblSubscriptionSetup.aspx.cs" Inherits="Web.Master.tblSubscriptionSetup" %>

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
                                        <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="Update Setting" OnClick="btnAdd_Click" />
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
                                                                <%-- <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lbllocationID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtlocationID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drplocationID" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true">
                                                                                <asp:ListItem Value="1" Text="Kuwait"></asp:ListItem>
                                                                                <asp:ListItem Value="2 " Text="Lebanon"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorlocationID" runat="server" ErrorMessage="Location name Required." ControlToValidate="drplocationID" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lbllocationID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtlocationID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>--%>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lbldays_in_week1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtdays_in_week1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpdays_in_week" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true">
                                                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                                                <asp:ListItem Value="2 " Text="2"></asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatordays_in_week" runat="server" ErrorMessage="Days in week Required." ControlToValidate="drpdays_in_week" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lbldays_in_week2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtdays_in_week2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblWhitch_day_delivery1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtWhitch_day_delivery1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtWhitch_day_delivery" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorWhitch_day_delivery" runat="server" ControlToValidate="txtWhitch_day_delivery" ErrorMessage="Whitch day delivery Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblWhitch_day_delivery2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtWhitch_day_delivery2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblWeek_Start_With_Day1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtWeek_Start_With_Day1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpWeek_Start_With_Day" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="false">
                                                                                <asp:ListItem Value="Sun" Text="Sun"></asp:ListItem>
                                                                                <asp:ListItem Value="Mon" Text="Mon"></asp:ListItem>
                                                                                <asp:ListItem Value="Tues" Text="Tues"></asp:ListItem>
                                                                                <asp:ListItem Value="Wed" Text="Wed"></asp:ListItem>
                                                                                <asp:ListItem Value="Thur" Text="Thur"></asp:ListItem>
                                                                                <asp:ListItem Value="Fri" Text="Fri"></asp:ListItem>
                                                                                <asp:ListItem Value="Sat" Text="Sat"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorWeek_Start_With_Day" runat="server" ErrorMessage="Week start with day Required." ControlToValidate="drpWeek_Start_With_Day" ValidationGroup="" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblWeek_Start_With_Day2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtWeek_Start_With_Day2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblDelivery_in_day1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDelivery_in_day1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpDelivery_in_day" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true">
                                                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                                                <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                                                <%-- <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                                                  <asp:ListItem Value="7" Text="7"></asp:ListItem>--%>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorDelivery_in_day" runat="server" ErrorMessage="Delivery in day Required." ControlToValidate="drpDelivery_in_day" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblDelivery_in_day2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDelivery_in_day2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblDelivery_time_begin1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDelivery_time_begin1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtDelivery_time_begin" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDelivery_time_begin" runat="server" ControlToValidate="txtDelivery_time_begin" ErrorMessage="Delivery time begin Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblDelivery_time_begin2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDelivery_time_begin2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                
                                                                
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblBefore_how_many_Hours1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBefore_how_many_Hours1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtBefore_how_many_Hours" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorBefore_how_many_Hours" runat="server" ControlToValidate="txtBefore_how_many_Hours" ErrorMessage="Before how many hours Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblBefore_how_many_Hours2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBefore_how_many_Hours2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblAfter_Completion_of_how_many_Percentage_of_Delivery1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAfter_Completion_of_how_many_Percentage_of_Delivery1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtAfter_Completion_of_how_many_Percentage_of_Delivery" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAfter_Completion_of_how_many_Percentage_of_Delivery" runat="server" ControlToValidate="txtAfter_Completion_of_how_many_Percentage_of_Delivery" ErrorMessage="After completion of how many percentage of delivery Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblAfter_Completion_of_how_many_Percentage_of_Delivery2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAfter_Completion_of_how_many_Percentage_of_Delivery2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblKitchenRequestingStore1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtKitchenRequestingStore1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpKitchenRequestingStore" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorKitchenRequestingStore" runat="server" ErrorMessage="Kitchenrequestingstore Required." ControlToValidate="drpKitchenRequestingStore" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblKitchenRequestingStore2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtKitchenRequestingStore2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblMainStore1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMainStore1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpMainStore" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorMainStore" runat="server" ErrorMessage="Mainstore Required." ControlToValidate="drpMainStore" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblMainStore2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMainStore2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblChanges_Allowed1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtChanges_Allowed1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:CheckBox ID="cbChanges_Allowed" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblChanges_Allowed2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtChanges_Allowed2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblRefund_Allowed1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRefund_Allowed1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:CheckBox ID="cbRefund_Allowed" runat="server" />
                                                                            <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidatorRefund_Allowed" runat="server" ControlToValidate="cbRefund_Allowed" ErrorMessage="Refund allowed Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblRefund_Allowed2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRefund_Allowed2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblActive1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:CheckBox ID="cbActive" runat="server" />
                                                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidatorActive" runat="server" ControlToValidate="txtActive" ErrorMessage="Active Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblActive2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblIncomingKitchenAutoAccept1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtIncomingKitchenAutoAccept1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:CheckBox ID="cbIncomingKitchenAutoAccept" runat="server" />
                                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorIncomingKitchenAutoAccept" runat="server" ControlToValidate="txtIncomingKitchenAutoAccept" ErrorMessage="Incomingkitchenautoaccept Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblIncomingKitchenAutoAccept2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtIncomingKitchenAutoAccept2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                                                                        <asp:Label runat="server" ID="lblhemp_birthday" Text="Which_day_delivery"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhnation_code" Text="Week_Start_With_Day"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhemp_mobile" Text="Delivery_in_day"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhemp_work_telephone" Text="Main Store"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhemp_work_email" Text="Delivery_time_begin"></asp:Label></th>
                                                                                                    <th style="width: 60px;">ACTION</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="LocalID" DataKeyNames="LocalID">
                                                                                                    <LayoutTemplate>
                                                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                                                        </tr>
                                                                                                    </LayoutTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblemp_birthday" runat="server" Text='<%# Eval("Whitch_day_delivery")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblnation_code" runat="server" Text='<%# Eval("Week_Start_With_Day")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblemp_mobile" runat="server" Text='<%# Eval("Delivery_in_day")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblemp_work_telephone" runat="server" Text='<%# Eval("MainStore")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblemp_work_email" runat="server" Text='<%# Eval("Delivery_time_begin")%>'></asp:Label></td>

                                                                                                            <td>
                                                                                                                <table>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("LocalID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                        <td style="display:none;">
                                                                                                                            <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("LocalID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
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
