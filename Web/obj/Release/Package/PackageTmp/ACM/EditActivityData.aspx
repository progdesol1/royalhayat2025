<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="EditActivityData.aspx.cs" Inherits="Web.ACM.EditActivityData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div  id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
               <%-- <ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">ACM</a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">ACTIVITY</a>
                    </li>
                </ul>--%>
                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <asp:UpdatePanel ID="updatedesk" runat="server">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-horizontal form-row-seperated">
                                    <div class="portlet box blue">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>
                                                <asp:Label ID="Label31" runat="server" Text="CRM Activity"></asp:Label>
                                                <asp:TextBox Style="color: #333333" ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <asp:LinkButton ID="LinkButton1" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                            <div class="actions btn-set">
                                                <div id="Div1" runat="server" class="btn-group btn-group-circle btn-group-solid">
                                                    <asp:Button ID="Button1" class="btn red" runat="server" Text="First" />
                                                    <asp:Button ID="Button2" class="btn green" runat="server" Text="Next" />
                                                    <asp:Button ID="Button3" class="btn purple" runat="server" Text="Prev" />
                                                    <asp:Button ID="Button4" class="btn grey-cascade" runat="server" Text="Last" />

                                                </div>
                                                <asp:Button ID="Button5" class="btn green-haze btn-circle" runat="server" Text="Find" ValidationGroup="Submit"  />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                &nbsp;
                                        <asp:LinkButton ID="LinkButton3" Style="color: #fff; width: 60px; padding: 0px;" runat="server">E&nbsp;<img src="../assets/global/img/flags/us.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton4" Style="color: #fff; width: 40px; padding: 0px;" runat="server">A&nbsp;<img src="../assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton5" Style="color: #fff; width: 50px; padding: 0px;" runat="server">F&nbsp;<img src="../assets/global/img/flags/fr.png" /></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="portlet-body form">

                                            <div class="form-wizard">
                                                <div class="tabbable">
                                                    <div class="tab-content no-space">

                                                        <div class="form-body">
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label5" runat="server" Text="Company"></asp:Label></label><div class="col-md-4">
                                                                        <asp:DropDownList ID="drpComid" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                    </div>

                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label6" runat="server" Text="Activity Perform By"></asp:Label></label><div class="col-md-4">
                                                                        <asp:DropDownList ID="drpActivitycode" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                                                    </div>
                                                            </div>
                                                            <%--    <div class="form-group">


                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label8" runat="server" Text="Ref Type"></asp:Label></label>
                                                                <div class="col-md-4">
                                                                    <asp:DropDownList ID="drpReftype" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="drpReftype_SelectedIndexChanged"></asp:DropDownList>
                                                                </div>
                                                            </div>--%>
                                                            <%-- <div class="form-group">
                                                               
                                                            </div>--%>
                                                            <asp:Panel ID="ticketpanel" runat="server">
                                                            </asp:Panel>
                                                            <div class="form-group">
                                                                <asp:Panel ID="panellineno" runat="server">
                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblmylineno" runat="server" Text="MyLineNo"></asp:Label></label>
                                                                    <div class="col-md-4">
                                                                        <div id="spinner1">
                                                                            <div class="input-group input-xlarge">
                                                                                <asp:TextBox ID="txtMylineno" runat="server" class="spinner-input form-control" MaxLength="3"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Mylineno Required" ControlToValidate="txtMylineno" ValidationGroup="submit"></asp:RequiredFieldValidator>
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
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlUserCode" runat="server">

                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblusercode" runat="server" Text="User Code"></asp:Label></label><div class="col-md-4">
                                                                            <asp:TextBox ID="txtUSERCODE" runat="server" class="form-control" MaxLength="10"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorUSERCODE" runat="server" ErrorMessage="USERCODE Required" ControlToValidate="txtUSERCODE" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlReferenceNo" runat="server">
                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblrefno" runat="server" Text="Reference No"></asp:Label></label><div class="col-md-4">
                                                                            <asp:TextBox ID="txtReferenceNo" runat="server" class="form-control" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorReferenceNo" runat="server" ErrorMessage="ReferenceNo Required" ControlToValidate="txtReferenceNo" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlEarlierRefNo" runat="server">

                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblearlierrefno" runat="server" Text="Earlier RefNo"></asp:Label></label><div class="col-md-4">
                                                                            <asp:TextBox ID="txtEarlierRefNo" runat="server" class="form-control" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorEarlierRefNo" runat="server" ErrorMessage="EarlierRefNo Required" ControlToValidate="txtEarlierRefNo" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlNextUser" runat="server">
                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblnesxtuser" runat="server" Text="NextUser"></asp:Label></label><div class="col-md-4">
                                                                            <asp:TextBox ID="txtNextUser" runat="server" class="form-control" MaxLength="10"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorNextUser" runat="server" ErrorMessage="NextUser Required" ControlToValidate="txtNextUser" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlDocumnetName" runat="server">
                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lbldocname" runat="server" Text="Documnet Name"></asp:Label></label><div class="col-md-4">
                                                                            <asp:TextBox ID="txtNextRefNo" runat="server" class="form-control" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorNextRefNo" runat="server" ErrorMessage="NextRefNo Required" ControlToValidate="txtNextRefNo" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                </asp:Panel>

                                                                <asp:Panel ID="pnlActivityPerform" runat="server">
                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblActivityPerform" runat="server" Text="Activity Perform"></asp:Label></label><div class="col-md-4">
                                                                            <asp:TextBox ID="txtActivityPerform" runat="server" class="form-control" MaxLength="1000"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorActivityPerform" runat="server" ErrorMessage="ActivityPerform Required" ControlToValidate="txtActivityPerform" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlReminderNote" runat="server">

                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblReminderNote" runat="server" Text="ReminderNote"></asp:Label></label><div class="col-md-4">
                                                                            <asp:TextBox ID="txtREMINDERNOTE" runat="server" class="form-control" MaxLength="1000"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorREMINDERNOTE" runat="server" ErrorMessage="REMINDERNOTE Required" ControlToValidate="txtREMINDERNOTE" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlEstCost" runat="server">
                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblEstCost" runat="server" Text="EstCost"></asp:Label></label>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtESTCOST" runat="server" class="form-control" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorESTCOST" runat="server" ErrorMessage="ESTCOST Required" ControlToValidate="txtESTCOST" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlGroupCode" runat="server">

                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblGroupCode" runat="server" Text="GroupCode"></asp:Label></label><div class="col-md-4">
                                                                            <asp:TextBox ID="txtGROUPCODE" runat="server" class="form-control" MaxLength="30"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorGROUPCODE" runat="server" ErrorMessage="GROUPCODE Required" ControlToValidate="txtGROUPCODE" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlUserCodeEntered" runat="server">
                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblUserCodeEntered" runat="server" Text="UserCodeEntered"></asp:Label></label><div class="col-md-4">
                                                                            <asp:TextBox ID="txtUSERCODEENTERED" runat="server" class="form-control" MaxLength="10"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorUSERCODEENTERED" runat="server" ErrorMessage="USERCODEENTERED Required" ControlToValidate="txtUSERCODEENTERED" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlUpDtTime" runat="server">

                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblUpDtTime" runat="server" Text="Up Dt Time"></asp:Label></label><div class="col-md-4">
                                                                            <asp:TextBox Placeholder="MM/DD/YYYY" ID="txtUPDTTIME" runat="server" class="form-control" MaxLength="10"></asp:TextBox><cc1:calendarextender id="TextBoxUPDTTIME_CalendarExtender" runat="server" enabled="True" popupbuttonid="calender" targetcontrolid="txtUPDTTIME"></cc1:calendarextender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorUPDTTIME" runat="server" ControlToValidate="txtUPDTTIME" ErrorMessage="UPDTTIME Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlUserName" runat="server">
                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblUserName" runat="server" Text="UserName"></asp:Label></label><div class="col-md-4">
                                                                            <asp:TextBox ID="txtUSERNAME" runat="server" class="form-control" MaxLength="15"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorUSERNAME" runat="server" ErrorMessage="USERNAME Required" ControlToValidate="txtUSERNAME" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlInitialDate" runat="server">

                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblInitialDate" runat="server" Text="Initial Date"></asp:Label></label><div class="col-md-4">
                                                                            <asp:TextBox Placeholder="MM/DD/YYYY" ID="txtInitialDate" runat="server" class="form-control" MaxLength="10"></asp:TextBox><cc1:calendarextender id="CalendarExtender1" runat="server" enabled="True" popupbuttonid="calender" targetcontrolid="txtInitialDate"></cc1:calendarextender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInitialDate" runat="server" ControlToValidate="txtInitialDate" ErrorMessage="UPDTTIME Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlDeadLineDate" runat="server">
                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblDeadLineDate" runat="server" Text="DeadLine Date"></asp:Label></label><div class="col-md-4">
                                                                            <asp:TextBox Placeholder="MM/DD/YYYY" ID="txtDeadLineDate" runat="server" class="form-control" MaxLength="10"></asp:TextBox><cc1:calendarextender id="CalendarExtender2" runat="server" enabled="True" popupbuttonid="calender" targetcontrolid="txtDeadLineDate"></cc1:calendarextender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtDeadLineDate" runat="server" ControlToValidate="txtDeadLineDate" ErrorMessage="UPDTTIME Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlAmiGlobal" runat="server">
                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblamiglobal" runat="server" Text="AmiGlobal"></asp:Label></label>
                                                                    <div class="col-md-4">
                                                                        <asp:CheckBox ID="ckbAmiglobal" runat="server" />
                                                                    </div>

                                                                </asp:Panel>
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:Panel ID="pnlMyPersonnel" runat="server">
                                                                    <label class="col-md-2 control-label">
                                                                        <asp:Label ID="lblmypersonnel" runat="server" Text="MyPersonnel"></asp:Label></label>
                                                                    <div class="col-md-4">
                                                                        <asp:CheckBox ID="ckbMypersonnel" runat="server" />
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                        <div class="form-actions">
                                                            <asp:Button ID="btnSaveACTIVITY" runat="server" class="btn blue"  Text="Save" />
                                                            <asp:Button ID="btnCancelACTIVITY" runat="server" class="btn blue"  Text="Cancel" />

                                                        </div>

                                                        <asp:Panel ID="ShowTicket" runat="server" Visible="false">
                                                            <div class="col-md-5 col-sm-4">
                                                                <div class="todo-tasklist">
                                                                    <asp:UpdatePanel runat="server" ID="upnl" class="item">
                                                                        <ContentTemplate>
                                                                            <asp:ListView ID="ltsRemainderNotes" runat="server" >

                                                                                <ItemTemplate>
                                                                                    <div class="todo-tasklist-item todo-tasklist-item-border-green">

                                                                                        <div class="todo-tasklist-item-title">
                                                                                            </span><%#Eval("USERCODE")%>
                                                                                            <asp:LinkButton ID="btnclick123" class="btn blue" Style="margin-left: 50px;" CommandArgument="btnclick123" CommandName="btnclick123" runat="server"> &nbsp; Reply &nbsp; </asp:LinkButton>
                                                                                            <span class="todo-tasklist-badge badge badge-roundless"># <%#Eval("ACTIVITYCODE")%> </span>
                                                                                        </div>
                                                                                        <div class="todo-tasklist-item-text"><%#Eval("Remarks")%> </div>
                                                                                        <asp:Label ID="tikitID" Visible="false" runat="server" Text='<%#Eval("ESTCOST")%>'></asp:Label>
                                                                                        <div class="todo-tasklist-controls pull-left">

                                                                                            <span class="todo-tasklist-date">
                                                                                                <i class="fa fa-calendar"></i><%# DateTime .Parse ( Eval("UPDTTIME").ToString ())%></span>
                                                                                            <span class="todo-tasklist-badge badge badge-roundless"><%#Eval("MyStatus")%> </span>
                                                                                            <span class="todo-tasklist-badge badge badge-roundless"><%#Eval("REFTYPE")%></span>
                                                                                        </div>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </div>
                                                            </div>
                                                            <div class="todo-tasklist-devider"></div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="panChat" runat="server" Visible="false">
                                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" class="item">
                                                                <ContentTemplate>

                                                                    <div class="col-md-7 col-sm-8">
                                                                        <div class="tabbable-line">
                                                                            <ul class="nav nav-tabs ">
                                                                                <li class="active">
                                                                                    <a href="#tab_1" data-toggle="tab">Comments </a>
                                                                                </li>
                                                                                <li>
                                                                                    <a href="#tab_2" data-toggle="tab">History </a>
                                                                                </li>
                                                                            </ul>
                                                                            <div class="tab-content">
                                                                                <div class="tab-pane active" id="tab_1">
                                                                                    <!-- TASK COMMENTS -->
                                                                                    <div class="form-body">
                                                                                        <div class="form-group">
                                                                                            <div class="col-md-12">
                                                                                                <asp:ListView ID="listChet" runat="server">

                                                                                                    <ItemTemplate>
                                                                                                        <ul class="media-list">
                                                                                                            <li class="media">
                                                                                                                <div class="media-body todo-comment">
                                                                                                                    <p class="todo-comment-head">
                                                                                                                        <span class="todo-comment-username"><%#Eval("Version")%></span> &nbsp;
                                                                                            <span class="todo-comment-date"><%# DateTime .Parse ( Eval("UPDTTIME").ToString ())%></span>
                                                                                                                    </p>
                                                                                                                    <p class="todo-text-color">
                                                                                                                        <%#Eval("ActivityPerform")%>
                                                                                                                    </p>

                                                                                                                </div>
                                                                                                            </li>
                                                                                                        </ul>
                                                                                                    </ItemTemplate>
                                                                                                </asp:ListView>



                                                                                                <!-- END TASK COMMENTS -->
                                                                                                <!-- TASK COMMENT FORM -->

                                                                                                <div class="col-md4">
                                                                                                    <ul class="media-list">
                                                                                                        <li class="media">
                                                                                                            <div class="media-body">
                                                                                                                <asp:TextBox ID="txtComent" runat="server" placeholder="Type comment..." Rows="4" class="form-control todo-taskbody-taskdesc" TextMode="MultiLine"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </li>
                                                                                                    </ul>
                                                                                                    <div class="form">

                                                                                                        <asp:Button ID="btnSubmit" runat="server" class="pull-right btn btn-sm btn-circle green"  Text="Submit" />
                                                                                                        <%--   <asp:Button ID="btnTikitClose" runat="server" class="btn btn-circle btn-sm btn-default" OnClick="btnTikitClose_Click" Text="Close" Style="margin-right: 10px;" />--%>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <%--<div class="form-group">--%>
                                                                                                <div class="col-md-8"></div>

                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <!-- END TASK COMMENT FORM -->
                                                                                </div>

                                                                                <div class="tab-pane" id="tab_2">
                                                                                    <asp:ListView ID="ListHistoy" runat="server">
                                                                                        <ItemTemplate>
                                                                                            <ul class="todo-task-history">
                                                                                                <li>
                                                                                                    <div class="todo-task-history-date"><%# DateTime .Parse ( Eval("UPDTTIME").ToString ())%> </div>
                                                                                                    <div class="todo-task-history-desc"><%#Eval("Version")%> </div>
                                                                                                </li>
                                                                                            </ul>
                                                                                        </ItemTemplate>
                                                                                    </asp:ListView>

                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="scroll-to-top">
                <i class="icon-arrow-up"></i>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
