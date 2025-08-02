<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RightPanelUC.ascx.cs" Inherits="Web.CRM.UserControl.RightPanelUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="row">
    <div class="portlet box blue">
        <div class="portlet-title">
            <div class="caption">
                <i class="fa fa-gift"></i>Tools
            </div>
            <div class="tools">
                <a href="javascript:;" class="collapse"></a>
                <a href="#portlet-config" data-toggle="modal" class="config"></a>
            </div>
        </div>
        <div class="portlet-body">

            <div id="accordion1" class="panel-group">
                 <div class="panel" Style="background: #dbf7d4 none; color:#24c28b;">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#accordion1_1">1. Appoinment
                                <asp:Label runat="server" ID="lblappoint" CssClass="label label-default"></asp:Label></a>&nbsp;<i class="fa fa-dropbox"></i>
                        </h4>
                    </div>
                    <div id="accordion1_1" class="panel-collapse collapse in">
                        <div class="row">
                            <div class="col-md-5" style="float: right">
                                <div class="form-group" style="color: ">
                                    <asp:Button ID="btnappoint" class="btn default" runat="server" Text="Add Appointment" CausesValidation="false" />

                                </div>
                            </div>
                        </div>
                        <div class="alert alert-info" style="background-color: #E5E5E5">
                            <table border="1" cellpadding="1" cellspacing="1" id="sample_1">
                                <asp:ListView ID="ListAppoint" runat="server">
                                    <ItemTemplate>


                                        <tr>
                                            <td><strong><%# Eval("Title") %></strong></td>
                                            <td><%# Eval("Color") %></td>
                                            <td><%# Eval("url") %></td>
                                            <td><%# Convert.ToDateTime(Eval("StartDt")).ToShortDateString() %></td>
                                            <td><%# Convert.ToDateTime(Eval("EndDt")).ToShortDateString() %></td>
                                            <td>
                                                <asp:LinkButton ID="LNKAppointTask" runat="server" CssClass="btn btn-sm btn-default" PostBackUrl="~/CRM/TaskAppointment.aspx">Task</asp:LinkButton>
                                            </td>
                                           
                                        </tr>


                                    </ItemTemplate>
                                </asp:ListView>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#accordion1_6">2.Notes 
                                <asp:Label runat="server" ID="lblnotecount" CssClass="label label-default"></asp:Label>&nbsp;<i class="fa fa-pagelines"></i>
                            </a>
                        </h4>
                        
                    </div>
                    <div id="accordion1_6" class="panel-collapse collapse">
                        <div class="row">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            <div class="col-md-5" style="float: right">
                                <div class="form-group" style="color: ">
                                    <asp:Button ID="btnAddNote" class="btn default" runat="server" Text="Add Note" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                        <asp:ListView ID="ListViewNote" runat="server">
                            <ItemTemplate>
                                <div class="alert alert-info" style="background-color: #E08283">
                                    <strong><%# Eval("Title") %></strong>
                                    <%# Eval("Description") %>
                                    <br />
                                    <strong style="float: right"><%# Convert.ToDateTime(Eval("CreatedDate")).ToShortDateString() %></strong>
                                    <br />
                                    <br />
                                    <%-- <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton>--%>

                                    <a href="AddTask.aspx">Add Task<span class="badge badge-success"></span></a>
                                    <%-- <a href='AddTask1.aspx?NoteID=<%# Eval("MyID")%>&SerialID=<%# Eval("MySerial")%>&AType=<%# Eval("Type")%>' style="color: white; padding-left: 5px; padding-right: 5px;">Add Task<span class="badge badge-success">
                                        <asp:Label runat="server" ID="lblTask" Text='<%# GetNoteTaskCount(Convert.ToInt32(Eval("MyID")), Convert.ToInt32(Eval("ActionType")),Convert.ToInt32(Eval("MySerial")),Convert.ToInt32(Eval("Type")))%>'></asp:Label>
                                    </span></a>--%>
                                    <%-- <a href=>
                                        <strong style="float: right">Add Task
                                             
                                            <span class="badge badge-success">
                                            </span></strong>

                                    </a>--%>
                                    <br />
                                </div>

                            </ItemTemplate>

                        </asp:ListView>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#accordion1_2">3.Tickets  
                                <asp:Label runat="server" ID="lblticketcount" CssClass="label label-default"></asp:Label></a>&nbsp;<i class="fa fa-ticket"></i>
                        </h4>
                    </div>
                    <div id="accordion1_2" class="panel-collapse collapse">
                        <div class="row">
                            <div class="col-md-5" style="float: right">
                                <div class="form-group" style="color: ">

                                    <asp:Button ID="btnaddticket" class="btn default" runat="server" Text="Add Ticket" CausesValidation="false" />
                                </div>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="up" runat="server">
                            <ContentTemplate>
                                <asp:ListView ID="ListViewTicket" runat="server" OnItemCommand="ltsRemainderNotes_ItemCommand">

                                    <ItemTemplate>
                                        <div class="todo-tasklist-item todo-tasklist-item-border-green">
                                            <div style="background-color: #36D7B7">
                                                <div class="todo-tasklist-item-title">
                                                    </span><%#Eval("USERCODE")%>
                                                    <asp:LinkButton ID="btnclick123" class="btn blue" Style="margin-left: 50px;" CommandArgument="btnclick123" CommandName="btnclick123" runat="server"> &nbsp; Detail &nbsp; </asp:LinkButton>
                                                    <span class="todo-tasklist-badge badge badge-roundless"><%#Eval("MasterCODE")%> </span>
                                                </div>
                                                <div class="todo-tasklist-item-text"><%#Eval("Remarks")%> </div>
                                                <asp:Label ID="tikitID" Visible="false" runat="server" Text='<%#Eval("MasterCODE")%>'></asp:Label>
                                                <div class="todo-tasklist-controls pull-left">

                                                    <span class="todo-tasklist-date">
                                                        <i class="fa fa-calendar"></i><%# DateTime .Parse ( Eval("UPDTTIME").ToString ())%></span>
                                                    <span class="todo-tasklist-badge badge badge-roundless"><%#Eval("MyStatus")%> </span>
                                                    <span class="todo-tasklist-badge badge badge-roundless"><%#Eval("ACTIVITYE")%></span>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Panel ID="panChat" runat="server" Visible="false">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" class="item">
                                <ContentTemplate>
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
                                                <div class="form-group">
                                                    <div class="col-md-12">

                                                        <asp:ListView ID="listChet" runat="server">

                                                            <ItemTemplate>
                                                                <ul class="media-list">
                                                                    <li class="media">
                                                                        <%--<a class="pull-left" href="javascript:;">
                                                                                                <img class="todo-userpic" src="<%# Eval("USERCODE")%>" width="27px" height="27px">
                                                                                            </a>--%>
                                                                        <div class="media-body todo-comment">
                                                                            <%-- <button type="button" class="todo-comment-btn btn btn-circle btn-default btn-sm">&nbsp; Reply &nbsp;</button>--%>
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


                                                    </div>
                                                </div>
                                                <!-- END TASK COMMENTS -->
                                                <!-- TASK COMMENT FORM -->
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <ul class="media-list">
                                                            <li class="media">

                                                                <div class="media-body">

                                                                    <asp:TextBox ID="txtComent" runat="server" placeholder="Type comment..." Rows="4" class="form-control todo-taskbody-taskdesc" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                            </li>
                                                        </ul>
                                                        <div class="form">
                                                            <div class="form-actions right todo-form-actions">
                                                                <asp:Button ID="btnSubmitchat" runat="server" class="pull-right btn btn-sm btn-circle green" Text="Reply" OnClick="btnSubmitchat_Click" />
                                                                <asp:Button ID="btnTikitClose" runat="server" class="btn btn-circle btn-sm btn-default" Text="Close" Style="margin-right: 10px;" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- END TASK COMMENT FORM -->
                                            </div>
                                            <div class="tab-pane" id="tab_2">
                                                <asp:ListView ID="ListHistoy" runat="server">
                                                    <ItemTemplate>
                                                        <div class="alert alert-info">

                                                            <%#Eval("Version")%>
                                                            <strong style="float: right"><%# DateTime .Parse ( Eval("UPDTTIME").ToString ())%></strong>
                                                        </div>

                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#accordion1_3">4. Files 
                                <asp:Label runat="server" ID="lblfiles" CssClass="label label-default"></asp:Label></a>&nbsp;<i class="fa fa-file"></i>
                        </h4>
                    </div>
                    <div id="accordion1_3" class="panel-collapse collapse">
                        <div class="row">
                            <div class="col-md-5" style="float: right">
                                <div class="form-group" style="color: ">
                                    <asp:Button ID="btnsavefile" runat="server" class="btn green-haze btn-circle" Text="Upload File" OnClick="btnsavefile_Click" CausesValidation="false" />

                                </div>

                            </div>
                        </div>

                        <asp:ListView ID="ListViewFile" runat="server">
                            <ItemTemplate>
                                <div class="alert alert-warning" style="background-color: #C49F47">
                                    <strong><%# Eval("AttachmentPath") %></strong>
                                    <br />
                                    <strong style="float: right"><%# Convert.ToDateTime(Eval("CreatedDate")).ToShortDateString() %></strong>
                                </div>
                            </ItemTemplate>

                        </asp:ListView>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnlfile" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Browse Document</label>
                                                <div class="col-md-7">
                                                    <asp:FileUpload ID="FUDoc" runat="server" CssClass="form-control" Style="padding-top: 0px;" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="Validation" ErrorMessage="File selected is Required" ControlToValidate="FUDoc" ValidationGroup="btnaddFilesubmit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Attachments Detail</label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="lblPath" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txtAttachmentsDetail" runat="server" TextMode="MultiLine" class="form-control" MaxLength="1000"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAttachmentsDetail" runat="server" CssClass="Validation" ErrorMessage="AttachmentsDetail Required" ControlToValidate="txtAttachmentsDetail" ValidationGroup="btnaddFilesubmit"></asp:RequiredFieldValidator>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Attachment Type</label>
                                                <div class="col-md-7">
                                                    <asp:DropDownList ID="drpAttachmentType" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="Validation" ErrorMessage="Attachments Type Required" ControlToValidate="drpAttachmentType" ValidationGroup="btnaddFilesubmit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Attachment Document</label>
                                                <div class="col-md-7">
                                                    <asp:Label ID="Label19" runat="server"></asp:Label>
                                                    <asp:DropDownList ID="drpAttachmentDocument" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="Validation" ErrorMessage="Attachments Document is Required" ControlToValidate="drpAttachmentDocument" ValidationGroup="btnaddFilesubmit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="Label25" class="col-md-2 control-label"></asp:Label>
                                                <div class="col-md-2" style="float: right">
                                                    <asp:Button ID="btnAddnewFile" runat="server" class="btn green-haze btn-circle" Text="Save" OnClick="btnAddnewFile_Click" ValidationGroup="btnaddFilesubmit" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnAddnewFile" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:ListView ID="ListViewFileCampaign" runat="server">
                            <ItemTemplate>
                                <div class="alert alert-success">
                                    <strong><%# Eval("Switch3") %></strong>
                                    <%# Eval("CreatedDate") %>
                                </div>

                            </ItemTemplate>

                        </asp:ListView>
                    </div>
                </div>
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#accordion1_4">5. Email 
                                <asp:Label runat="server" ID="lblEmail" CssClass="label label-default"></asp:Label></a>&nbsp;<i class="fa fa-mail-reply-all"></i>
                        </h4>
                    </div>
                    <div id="accordion1_4" class="panel-collapse collapse">
                        <div class="modal-body">
                            <div class="row">

                                <div class="table-scrollable" style="height: 50%; overflow: auto">
                                    <table class="table table-striped table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>To
                                                </th>
                                                <th>From
                                                </th>
                                                <th>Subject 
                                                </th>
                                                <th>Description
                                                </th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="ListViewEmail" runat="server">
                                                <LayoutTemplate>
                                                    <tr id="ItemPlaceholder" runat="server">
                                                    </tr>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTenet" runat="server" Text='<%# Eval("Switch1To")%>'></asp:Label></td>

                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Switch2From")%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Switch3")%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Description")%>'></asp:Label></td>


                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </tbody>
                                    </table>

                                </div>


                            </div>
                        </div>

                        <div class="tabbable">
                            <div class="tab-content no-space">
                                <div class="tab-pane active" id="tab_general12">
                                    <div class="col-md-12">
                                        <div class="form-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblinvalid_email1s" class="col-md-4 control-label">To</asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtTo" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <%-- <asp:RequiredFieldValidator ID="reqto" runat="server" ControlToValidate="txtTo" ErrorMessage="To Email is Required." CssClass="Validation" ValidationGroup="send"></asp:RequiredFieldValidator>--%>
                                                            <asp:RegularExpressionValidator ID="regularTovaliedate" runat="server" CssClass="Validation" ControlToValidate="txtTo" ErrorMessage="Email Right is Requered" ValidationGroup="send" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="Label22" class="col-md-4 control-label">From</asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" Text="" ReadOnly="true"></asp:TextBox>
                                                            <%-- <asp:RequiredFieldValidator ID="reqform" runat="server" ControlToValidate="txtFrom" ErrorMessage="From Email is Required." CssClass="Validation" ValidationGroup="send"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="Label23" class="col-md-4 control-label">Subject</asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtSubjectEmail1" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="reqsubemail" runat="server" ControlToValidate="txtSubjectEmail1" ErrorMessage="Subject is Required." CssClass="Validation" ValidationGroup="send"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="Label29" class="col-md-4 control-label">Body</asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">


                                                <div class="form-group" style="color: ">
                                                    <asp:TextBox ID="txtBody11" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="reqbody" runat="server" ControlToValidate="txtBody11" ErrorMessage="Email Body is Required." CssClass="Validation" ValidationGroup="send"></asp:RequiredFieldValidator>--%>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-md-5" style="float: right">
                                                    <div class="form-group" style="color: ">

                                                        <asp:Button ID="btnSend" class="btn default" runat="server" Text="Send" ValidationGroup="send" OnClick="btnSend_Click" />

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
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion1" href="#accordion1_5">6. Memo
                                <asp:Label runat="server" ID="lblMemo" CssClass="label label-default"></asp:Label></a>&nbsp;<i class="fa fa-save"></i>
                        </h4>
                    </div>
                    <div id="accordion1_5" class="panel-collapse collapse">
                        <div class="row">
                            <div class="col-md-5" style="float: right">
                                <div class="form-group" style="color: ">
                                    <%--<a class="btn default" data-toggle="modal" href="#form_modal13">Add Memo
                                                                                            </a>--%>
                                    <asp:Button ID="btnmemo" class="btn default" runat="server" Text="Add Memo" CausesValidation="false" />

                                </div>
                            </div>
                        </div>

                        <asp:ListView ID="ListViewMemo" runat="server">
                            <ItemTemplate>
                                <div class="alert alert-info" style="background-color: #E5E5E5">
                                    <strong><%# Eval("Title") %></strong>
                                    <%# Eval("Description") %>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
               

            </div>



        </div>
    </div>


    <asp:Panel ID="PnlUser123" runat="server" CssClass="modalPopup" Style="display: none">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">

                    <%-- <asp:Button ID="lnkClose" class="close" CausesValidation="false" runat="server" Text="Button" />--%>
                    <h4 class="modal-title">Add Note</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="row">
                            <asp:Panel ID="pnlother" runat="server" Visible="false">
                                <div class="alert alert-danger">
                                    <strong>Error!</strong>
                                    Note Title Allready Exist..
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label16" class="col-md-2 control-label">Title</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTitle" ErrorMessage="Title is Required." CssClass="Validation" ValidationGroup="btnsubmitnote"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label15" class="col-md-2 control-label">Note</asp:Label>
                                    <div class="col-md-10">
                                        <%-- <CKEditor:CKEditorControl runat="server" ID="txtNote123" ></CKEditor:CKEditorControl>--%>
                                        <asp:TextBox ID="txtNote123" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNote123" ErrorMessage="Note Description are Required." CssClass="Validation" ValidationGroup="btnsubmitnote"></asp:RequiredFieldValidator>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator81" runat="server" ControlToValidate="txtNote" ErrorMessage="Note is Required." CssClass="Validation" ValidationGroup="btnsubmitnote"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">

                    <asp:LinkButton ID="btnsavenoteclose" class="btn default" runat="server" CausesValidation="false">Close</asp:LinkButton>
                    <%--  <button class="btn default" data-dismiss="modal" aria-hidden="true">Close</button>--%>
                    <%-- <asp:Button ID="Button4" runat="server" class="btn default" data-dismiss="modal" aria-hidden="true" Text="Close" />--%>
                    <asp:LinkButton ID="btnsavenote" runat="server" class="btn green" ValidationGroup="btnsubmitnote" OnClick="btnsavenote_Click">Save</asp:LinkButton>

                </div>


            </div>
        </div>


        <%-- </ContentTemplate>

            </asp:UpdatePanel>--%>
        <%--</div>--%>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
        BackgroundCssClass="modalBackground" CancelControlID="btnsavenoteclose" Enabled="True"
        PopupControlID="PnlUser123" TargetControlID="btnAddNote">
    </cc1:ModalPopupExtender>

    <asp:Panel ID="pnlticket" runat="server" CssClass="modalPopup" Style="display: none">
        <%--<div id="form_modal11" class="modal fade" role="dialog" aria-labelledby="myModalLabel10" aria-hidden="true">--%>
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <%--<asp:Button ID="Button1" class="close" runat="server" Text="Button" />--%>
                    <h4 class="modal-title">Add Ticket</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal" role="form">
                        <div class="row">
                            <asp:Panel ID="Panel1" runat="server" Visible="false">
                                <div class="alert alert-danger">
                                    <strong>Error!</strong>
                                    Ticket Allready Exist..
                                </div>
                            </asp:Panel>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label17" class="col-md-2 control-label">Department</asp:Label>
                                    <div class="col-md-10" style="float: right">
                                        <asp:DropDownList ID="drpSDepartment" runat="server" class="form-control todo-taskbody-tags">
                                            <asp:ListItem Value="0">----Select----</asp:ListItem>
                                            <asp:ListItem Value="1">Buyer</asp:ListItem>
                                            <asp:ListItem Value="2">Seller</asp:ListItem>
                                            <asp:ListItem Value="3">Delivery</asp:ListItem>
                                            <asp:ListItem Value="4">Finance</asp:ListItem>
                                            <asp:ListItem Value="5">Account Manager</asp:ListItem>
                                            <asp:ListItem Value="6">Product Rating</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="drpSDepartment" ErrorMessage="Department is Required." CssClass="Validation" InitialValue="0" ValidationGroup="btnsubmitTicket"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label18" class="col-md-2 control-label">Priority</asp:Label>
                                    <div class="col-md-10" style="float: right">
                                        <asp:DropDownList ID="drppriority" runat="server" class="form-control todo-taskbody-tags">
                                            <asp:ListItem Value="0">----Select----</asp:ListItem>
                                            <asp:ListItem Value="Low">Low</asp:ListItem>
                                            <asp:ListItem Value="Medium">Medium</asp:ListItem>
                                            <asp:ListItem Value="High">High</asp:ListItem>
                                            <asp:ListItem Value="Urgent">Urgent</asp:ListItem>
                                            <asp:ListItem Value="Emergency">Emergency</asp:ListItem>
                                            <asp:ListItem Value="Critical">Critical</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="drppriority" ErrorMessage="Priority is Required." CssClass="Validation" InitialValue="0" ValidationGroup="btnsubmitTicket"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label20" class="col-md-2 control-label">Subject</asp:Label>
                                    <div class="col-md-10" style="float: right">
                                        <asp:TextBox ID="txtSubject" runat="server" placeholder="Wirte The Subject" class="form-control todo-taskbody-due">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtSubject" ErrorMessage="Subject is Required." CssClass="Validation" ValidationGroup="btnsubmitTicket"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label21" class="col-md-2 control-label">Message</asp:Label>
                                    <div class="col-md-10" style="float: right">
                                        <asp:TextBox ID="txtMessage" runat="server" placeholder="Wirte The Messaging" class="form-control" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtMessage" ErrorMessage="Message is Required." CssClass="Validation" ValidationGroup="btnsubmitTicket"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="LinkButton2" runat="server" class="btn default">Close</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton1" runat="server" class="btn green" ValidationGroup="btnsubmitTicket" OnClick="btnTicketSave_Click">Save</asp:LinkButton>
                </div>
            </div>
        </div>
        <%-- </div>--%>
    </asp:Panel>


    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" DynamicServicePath=""
        BackgroundCssClass="modalBackground" CancelControlID="LinkButton2" Enabled="True"
        PopupControlID="pnlticket" TargetControlID="btnaddticket">
    </cc1:ModalPopupExtender>


    <asp:Panel ID="pnlmemo" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <%-- <asp:Button ID="Button2" class="close" runat="server" Text="Button" />--%>
                    <h4 class="modal-title">Add Memo</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal" role="form">
                        <div class="row">
                            <asp:Panel ID="Panel2" runat="server" Visible="false">
                                <div class="alert alert-danger">
                                    <strong>Error!</strong>
                                    Memo Title Allready Exist..
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label26" class="col-md-2 control-label">Title</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txttitlememo" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqtitlememo" runat="server" ControlToValidate="txttitlememo" ErrorMessage="Title is Required." CssClass="Validation" ValidationGroup="btnsubmitMemo"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="Label27" class="col-md-2 control-label">Description</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtMemoDescrption1" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqmemodesc" runat="server" ControlToValidate="txtMemoDescrption1" ErrorMessage="Memo Description is Required." CssClass="Validation" ValidationGroup="btnsubmitMemo"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="LinkButton3" runat="server" class="btn default">Close</asp:LinkButton>

                    <asp:Button ID="btnsavememo" runat="server" class="btn green" Text="Save" ValidationGroup="btnsubmitMemo" OnClick="btnsavememo_Click" />

                </div>
            </div>
        </div>

    </asp:Panel>

    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" DynamicServicePath=""
        BackgroundCssClass="modalBackground" CancelControlID="LinkButton3" Enabled="True"
        PopupControlID="pnlmemo" TargetControlID="btnmemo">
    </cc1:ModalPopupExtender>

    <asp:Panel ID="pnltask" runat="server" CssClass="modalPopup" Style="display: none">
        <div id="form_modal1Task">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Button ID="Button3" class="close" runat="server" Text="Button" />
                        <h4 class="modal-title">Add Task</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-horizontal" role="form">
                            <div class="row">
                                <asp:Panel ID="Panel3" runat="server" Visible="false">
                                    <div class="alert alert-danger">
                                        <strong>Error!</strong>
                                        Task Allready Exist..
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lblTaskID1s" class="col-md-2 control-label">Task</asp:Label><asp:TextBox runat="server" ID="txtTaskID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                            <asp:DropDownList ID="drpTaskID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                        </div>
                                        <asp:Label runat="server" ID="lblTaskID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTaskID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lblCerticateNo1s" class="col-md-2 control-label">Certicate No</asp:Label><asp:TextBox runat="server" ID="txtCerticateNo1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                            <asp:DropDownList ID="drpCerticateNo" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                        </div>
                                        <asp:Label runat="server" ID="lblCerticateNo2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCerticateNo2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lblForEmp_ID1s" class="col-md-2 control-label">Employee</asp:Label><asp:TextBox runat="server" ID="txtForEmp_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                            <asp:DropDownList ID="drpForEmp_ID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                        </div>
                                        <asp:Label runat="server" ID="lblForEmp_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtForEmp_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lblPerformingEmp_ID1s" class="col-md-2 control-label">Performing Employee</asp:Label><asp:TextBox runat="server" ID="txtPerformingEmp_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                            <asp:DropDownList ID="drpPerformingEmp_ID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                        </div>
                                        <asp:Label runat="server" ID="lblPerformingEmp_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPerformingEmp_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lblactivity" Text="Activity" class="col-md-2 control-label">Activity</asp:Label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="drpactivity" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lblproject" Text="Project" class="col-md-2 control-label">Project</asp:Label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="drpproject" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lbltodate" Text="To Field" class="col-md-2 control-label">To</asp:Label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="drptofield" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lblowner" Text="Owner" class="col-md-2 control-label">Owner</asp:Label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtowner" runat="server" CssClass="form-control" Enabled="false"> </asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lblSubject1s" class="col-md-2 control-label">Subject</asp:Label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtlblSubject" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSubject" runat="server" ControlToValidate="txtSubject" ErrorMessage="Subject Required." CssClass="Validation" ValidationGroup="btnsubmittask"></asp:RequiredFieldValidator>
                                        </div>
                                        <asp:Label runat="server" ID="lblSubject2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSubject2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lblStartingDate1s" class="col-md-2 control-label">Starting Date</asp:Label><asp:TextBox runat="server" ID="txtStartingDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-4">
                                            <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtStartingDate" runat="server" CssClass="form-control" Style="width: 100%"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxStartingDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtStartingDate" Format="dd/MM/yyyy hh:mm:ss tt"></cc1:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorStartingDate" runat="server" ControlToValidate="txtStartingDate" ErrorMessage="Starting Date Required." CssClass="Validation" ValidationGroup="btnsubmittask"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender" TargetControlID="txtStartingDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox placeholder="Num Of Day" ID="txtnoofday" OnTextChanged="txtnoofday_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" Style="width: 118px;"> </asp:TextBox>
                                        </div>
                                        <asp:Label runat="server" ID="lblStartingDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStartingDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lblTaskStatus1s" class="col-md-3 control-label">Status</asp:Label><asp:TextBox runat="server" ID="txtTaskStatus1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="drpTaskStatus" runat="server" CssClass="form-control select2me" Style="width: 100%;">
                                                <asp:ListItem Value="Not Started">Not Started</asp:ListItem>
                                                <asp:ListItem Value="In Progress">In Progress</asp:ListItem>
                                                <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                                <asp:ListItem Value="Waiting on Somone else">Waiting on Somone else</asp:ListItem>
                                                <asp:ListItem Value="Deferred">Deferred</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <asp:Label runat="server" ID="lblTaskStatus2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTaskStatus2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lbltasktype" Text="Task Type" class="col-md-3 control-label">Task Type</asp:Label><asp:TextBox runat="server" ID="TextBox2" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="drptasktype" runat="server" CssClass="form-control select2me" Style="width: 100%;">
                                                <asp:ListItem Value="Private">Private</asp:ListItem>
                                                <asp:ListItem Value="High Importance">High Importance</asp:ListItem>
                                                <asp:ListItem Value="Low Importance">Low Importance</asp:ListItem>

                                            </asp:DropDownList>

                                        </div>
                                        <asp:Label runat="server" ID="Label28" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox5" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lblDueDate1s" class="col-md-2 control-label">DueD ate</asp:Label><asp:TextBox runat="server" ID="txtDueDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                            <asp:TextBox Placeholder="DD/MM/YYYY" OnTextChanged="txtDueDate_TextChanged" AutoPostBack="true" ID="txtDueDate" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxDueDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDueDate" Format="dd/MM/yyyy hh:mm:ss tt"></cc1:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDueDate" runat="server" ControlToValidate="txtDueDate" ErrorMessage="Due Date Required." CssClass="Validation" ValidationGroup="btnsubmittask"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtDueDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                        </div>
                                        <asp:Label runat="server" ID="lblDueDate2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDueDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lblPriority1s" class="col-md-3 control-label">Priority</asp:Label><asp:TextBox runat="server" ID="txtPriority1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="DrpPriorityTask" runat="server" CssClass="form-control select2me" Style="width: 100%;">
                                                <asp:ListItem Value="Low">Low</asp:ListItem>
                                                <asp:ListItem Value="Normal">Normal</asp:ListItem>
                                                <asp:ListItem Value="High">High</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <asp:Label runat="server" ID="lblPriority2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPriority2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="lblCompletedPerctange1s" class="col-md-3 control-label">Completed Perctange</asp:Label><asp:TextBox runat="server" ID="txtCompletedPerctange1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="drpCompletedPerctange" runat="server" CssClass="form-control select2me" Style="width: 100%;">
                                                <asp:ListItem Value="0.00000">0%</asp:ListItem>
                                                <asp:ListItem Value="25.00000">25%</asp:ListItem>
                                                <asp:ListItem Value="50.00000">50%</asp:ListItem>
                                                <asp:ListItem Value="75.00000">75%</asp:ListItem>
                                                <asp:ListItem Value="100.00000">100%</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <asp:Label runat="server" ID="lblCompletedPerctange2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCompletedPerctange2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="cheack" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group" style="color: ">
                                                <asp:Label runat="server" ID="lblReminderDate1s" class="col-md-2 control-label">Reminder Date</asp:Label><asp:TextBox runat="server" ID="txtReminderDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                <div class="col-md-1">
                                                    <asp:CheckBox ID="cbreminder" runat="server" OnCheckedChanged="cbreminder_CheckedChanged" AutoPostBack="true" />
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox Style="width: 100%;" Placeholder="DD/MM/YYYY" ID="txtReminderDate" Enabled="false" runat="server" CssClass="form-control"> </asp:TextBox>
                                                    <cc1:CalendarExtender ID="TextBoxReminderDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtReminderDate" Format="dd/MM/yyyy hh:mm:ss tt"></cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorReminderDate" runat="server" ControlToValidate="txtReminderDate" ErrorMessage="Reminder Date Required." CssClass="Validation" ValidationGroup="btnsubmittask"></asp:RequiredFieldValidator>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtReminderDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                                </div>
                                                <asp:Label runat="server" ID="lblReminderDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReminderDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="control-label col-md-2">Remark</label>
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtReminder" name="content" data-provide="markdown" class="form-control" Style="resize: none; height: 157px;" Text="" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button class="btn default" data-dismiss="modal" aria-hidden="true">Close</button>
                        <asp:Button ID="btnsavetask" runat="server" class="btn green" Text="AddNew" ValidationGroup="btnsubmittask" OnClick="btnsavetask_Click" />

                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>


    <asp:HiddenField ID="hiddenConfirm" runat="server" />
    <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="modal fade" id="basic" tabindex="-1" role="basic" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title">Request Is Received</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-horizontal" role="form">
                            <div class="row">
                                <asp:Panel ID="Panel5" runat="server" Visible="false">
                                    <div class="alert alert-danger">
                                        <strong>Error!</strong>
                                        Task Allready Exist..
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="Label4" class="col-md-2 control-label">Task</asp:Label>
                                        <div class="col-md-8">
                                            <asp:Label runat="server" ID="lblTicketNumber" class="col-md-2 control-label"></asp:Label>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="Label6" class="col-md-2 control-label">Name</asp:Label><div class="col-md-8">
                                            <asp:Label runat="server" ID="lblTicketUserName" class="col-md-2 control-label"></asp:Label>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group" style="color: ">
                                        <asp:Label runat="server" ID="Label5" class="col-md-2 control-label">Email</asp:Label><div class="col-md-8">
                                            <asp:Label runat="server" ID="lblConfirmTicketEmail" class="col-md-2 control-label"></asp:Label>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="cancelconfirm" class="btn default" data-dismiss="modal" runat="server">OK</asp:LinkButton>
                        <%--   <button type="button" class="btn default" data-dismiss="modal">OK</button>--%>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" DynamicServicePath=""
        BackgroundCssClass="modalBackground" CancelControlID="cancelconfirm" Enabled="True"
        PopupControlID="Panel4" TargetControlID="hiddenConfirm">
    </cc1:ModalPopupExtender>
    <%-- Appoinment by dipak --%>
    <asp:Panel ID="pnlappoint" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Add Apppinment</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal" role="form">
                        <div class="row">
                            <asp:Panel ID="Panel7" runat="server" Visible="false">
                                <div class="alert alert-danger">
                                    <strong>Error!</strong>
                                    Apppinment Allready Exist..
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblTitle" class="col-md-2 control-label">Title</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtTitleAP" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTitleAP" ErrorMessage="Title is Required." CssClass="Validation" ValidationGroup="appoint"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblcolor" class="col-md-2 control-label">Color</asp:Label>
                                    <div class="col-md-10">
                                        <asp:DropDownList ID="drpColor" runat="server" CssClass="table-group-action-input form-control input-medium">
                                            <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Red" Value="Red"></asp:ListItem>
                                            <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                                            <asp:ListItem Text="Blue" Value="Blue"></asp:ListItem>
                                            <asp:ListItem Text="Yellow" Value="Yellow"></asp:ListItem>
                                            <asp:ListItem Text="Purple" Value="Purple"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="drpColor" ErrorMessage="Color is Required." CssClass="Validation" ValidationGroup="appoint" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblurl" class="col-md-2 control-label">URL</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtURLAP" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtURLAP" ErrorMessage="URL is Required." CssClass="Validation" ValidationGroup="appoint"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblsdate" class="col-md-2 control-label">Start Date</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtSdateAP" runat="server" CssClass="form-control" placeholder="MM/dd/yyyy"></asp:TextBox>
                                        <cc1:CalendarExtender ID="TextBoxEndDt_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtSdateAP" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" TargetControlID="txtSdateAP" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtSdateAP" ErrorMessage="Start Date is Required." CssClass="Validation" ValidationGroup="appoint"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lbledate" class="col-md-2 control-label">End Date</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtEnddateAP" runat="server" CssClass="form-control" placeholder="MM/dd/yyyy"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtEnddateAP" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtEnddateAP" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtEnddateAP" ErrorMessage="End Date Required." CssClass="Validation" ValidationGroup="appoint"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="linkclose" runat="server" class="btn default">Close</asp:LinkButton>

                    <asp:Button ID="btnsaveAppoint" runat="server" class="btn green" Text="Save" ValidationGroup="appoint" OnClick="btnsaveAppoint_Click" />

                </div>
            </div>
        </div>

    </asp:Panel>

    <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" DynamicServicePath=""
        BackgroundCssClass="modalBackground" CancelControlID="linkclose" Enabled="True"
        PopupControlID="pnlappoint" TargetControlID="btnappoint">
    </cc1:ModalPopupExtender>
</div>
