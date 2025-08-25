<%@ Page Title="" Language="C#" MasterPageFile="~/POS/POSMaster.Master" AutoEventWireup="true" CodeBehind="ViewofTickes.aspx.cs" Inherits="Web.POS.ViewofTickes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .card-header {
            background: #f8f9fa;
            border-bottom: 1px solid #eaeaea;
            padding: 15px 20px;
        }
        .ticket-container {
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
            margin-bottom: 20px;
            overflow: hidden;
        }
        .form-section {
            padding: 20px;
            border-bottom: 1px solid #eee;
        }
        .tab-content {
            border: 1px solid #dee2e6;
            border-top: none;
            padding: 15px;
            border-radius: 0 0 4px 4px;
        }
        .nav-tabs {
            background: #f8f9fa;
            padding: 0 15px;
        }
        .media-list li {
            border-bottom: 1px solid #f1f1f1;
            padding: 15px 0;
        }
        .btn-action {
            margin-right: 5px;
            min-width: 90px;
        }
        .status-badge {
            padding: 3px 8px;
            border-radius: 12px;
            font-size: 12px;
        }
        .badge-pending { background: #ffc107; color: #212529; }
        .badge-closed { background: #28a745; color: white; }
        .badge-inprogress { background: #17a2b8; color: white; }
        .priority-high { border-left: 4px solid #dc3545; }
        .priority-medium { border-left: 4px solid #ffc107; }
        .priority-low { border-left: 4px solid #28a745; }
        .attachment-badge {
            position: relative;
            top: -2px;
            margin-left: 5px;
        }
        .comment-avatar {
            width: 40px;
            height: 40px;
            border-radius: 50%;
            object-fit: cover;
        }
        .section-title {
            border-bottom: 1px solid #eee;
            padding-bottom: 10px;
            margin-bottom: 20px;
            color: #495057;
            font-weight: 600;
        }
    </style>
    <script type="text/javascript">
        function openModalsmall2() {
            $('#small2').modal('show');
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <!-- Page Header -->
        <div class="page-header">
            <div class="d-flex justify-content-between align-items-center">
                <h1 class="page-title">Complaint Ticket
                    <small>for <asp:Label ID="lblUser" runat="server" CssClass="text-primary"></asp:Label></small>
                </h1>
                <asp:DropDownList ID="drpyear1" runat="server" CssClass="form-control w-auto" AutoPostBack="true" Visible="false"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <!-- Left Column - Comments/History -->
            <div class="col-lg-5">
                <div class="ticket-container">
                    <div class="card-header">
                        <h5 class="mb-0"><i class="fas fa-comments mr-2"></i>Ticket Discussion</h5>
                    </div>
                    
                    <asp:Panel ID="panChat" runat="server" DefaultButton="btnSubmit">
                        <asp:Timer ID="Timer1" runat="server" Interval="18000" OnTick="Timer1_Tick"></asp:Timer>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="form-section">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-toggle="tab" href="#tab_1">Comments</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_2">History</a>
                                        </li>
                                    </ul>
                                    
                                    <div class="tab-content">
                                        <!-- Comments Tab -->
                                        <div id="tab_1" class="tab-pane active">
                                            <div class="mt-3">
                                                <asp:ListView ID="listChet" runat="server" OnItemCommand="listChet_ItemCommand">
                                                    <ItemTemplate>
                                                        <div class="media mb-4">
                                                            <img src="../CRM/images/No_image.png" class="comment-avatar mr-3" alt="User">
                                                            <div class="media-body">
                                                                <div class="d-flex justify-content-between mb-1">
                                                                    <h6 class="mb-0 font-weight-bold"><%#Eval("Version")%></h6>
                                                                    <small class="text-muted"><%# Convert.ToDateTime(Eval("UPDTTIME")).ToString("dd-MMM-yyyy hh:mm tt")%></small>
                                                                </div>
                                                                <p class="mb-2"><%#Eval("ActivityPerform")%></p>
                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("MasterCODE") %>' 
                                                                    runat="server" CssClass="btn btn-sm btn-light border">
                                                                    <i class="fas fa-edit mr-1"></i>Edit
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                        
                                        <!-- History Tab -->
                                        <div id="tab_2" class="tab-pane">
                                            <div class="mt-3">
                                                <asp:ListView ID="ListHistoy" runat="server">
                                                    <ItemTemplate>
                                                        <div class="d-flex justify-content-between border-bottom pb-2 mb-2">
                                                            <div><%#Eval("Version")%></div>
                                                            <div class="text-muted"><%# Convert.ToDateTime(Eval("UploadDate")).ToString("dd-MMM-yyyy hh:mm tt")%></div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                            </Triggers>
                        </asp:UpdatePanel>
                        
                        <!-- New Comment Form -->
                        <div class="form-section">
                            <h6 class="section-title">Add New Comment</h6>
                            
                            <div class="form-group">
                                <label>Action Type</label>
                                <asp:DropDownList ID="aspcomment" runat="server" AutoPostBack="true" 
                                    CssClass="form-control" onselectedindexchanged="aspcomment_SelectedIndexChanged">
                                    <asp:ListItem Value="0">---Select Action---</asp:ListItem>
                                    <asp:ListItem Value="1">Immediate action</asp:ListItem>
                                    <asp:ListItem Value="2">Follow-up</asp:ListItem>
                                    <asp:ListItem Value="3">Corrective action</asp:ListItem>
                                    <asp:ListItem Value="4">Investigation results</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            
                            <asp:Panel ID="pnlInvestigation" runat="server" Visible="false">
                                <div class="form-group">
                                    <label>Investigation Result</label>
                                    <asp:DropDownList ID="drpinvestigation" runat="server" AutoPostBack="true" 
                                        CssClass="form-control" onselectedindexchanged="drpinvestigation_SelectedIndexChanged1">
                                        <asp:ListItem Value="1">Relevant</asp:ListItem>
                                        <asp:ListItem Value="2">Irrelevant</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </asp:Panel>
                            
                            <div class="form-group">
                                <label>Your Comment</label>
                                <asp:TextBox ID="txtComent" runat="server" placeholder="Type your comment..." 
                                    Rows="3" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                            
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label>Status</label>
                                    <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                        <asp:ListItem Value="Closed">Closed</asp:ListItem>
                                        <asp:ListItem Value="InProgress">In Progress</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>Priority</label>
                                    <div class="d-flex align-items-center">
                                        <cc1:Rating ID="Rating1" runat="server" AutoPostBack="true" 
                                            StarCssClass="Star" WaitingStarCssClass="WaitingStar" 
                                            EmptyStarCssClass="Star" FilledStarCssClass="FilledStar"
                                            OnChanged="Rating1_Changed" />
                                        <asp:Label ID="lblRatingStatus" runat="server" CssClass="ml-2 font-weight-bold" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="d-flex justify-content-between mt-4">
                                <div>
                                    <asp:Button ID="lbllblattachments2" runat="server" Text="Attachments" 
                                        CssClass="btn btn-light border" onclick="lblattachments_Click" />
                                    <asp:Button ID="btnAttch" runat="server" CssClass="btn btn-light border attachment-badge" />
                                </div>
                                <div>
                                    <asp:Button ID="btnTikitClose" runat="server" Text="Back" 
                                        CssClass="btn btn-secondary btn-action" onclick="btnTikitClose_Click" />
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                        CssClass="btn btn-primary btn-action" onclick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>

            <!-- Right Column - Ticket Form -->
            <div class="col-lg-7">
                <div class="ticket-container">
                    <div class="card-header d-flex justify-content-between align-items-center">
                        <h5 class="mb-0"><i class="fas fa-ticket-alt mr-2"></i>Ticket Details</h5>
                        <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                            <div class="alert alert-success alert-dismissible py-1 px-3 mb-0">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                <button type="button" class="close" data-dismiss="alert">&times;</button>
                            </div>
                        </asp:Panel>
                    </div>
                    
                    <asp:Panel ID="pnlTicki" runat="server">
                        <div class="form-section">
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label>Date <span class="text-danger">*</span></label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtdates" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="input-group-append">
                                            <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                                        </div>
                                    </div>
                                    <cc1:CalendarExtender ID="CalendarExtendertxtdateTO" runat="server" 
                                        Enabled="True" PopupButtonID="calender" TargetControlID="txtdates" 
                                        Format="dd/MMM/yyyy"></cc1:CalendarExtender>
                                    <small class="text-danger">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                            ValidationGroup="Ticks" Display="Dynamic" ControlToValidate="txtdates" 
                                            ErrorMessage="Date required"></asp:RequiredFieldValidator>
                                    </small>
                                </div>
                                
                                <div class="form-group col-md-6">
                                    <label>Complaint Type <span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="drpComplainType" runat="server" 
                                        CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <small class="text-danger">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                            ValidationGroup="Ticks" Display="Dynamic" ControlToValidate="drpComplainType" 
                                            ErrorMessage="Complaint type required" InitialValue="0"></asp:RequiredFieldValidator>
                                    </small>
                                </div>
                            </div>
                            
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label>Department <span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="drpSDepartment" runat="server" 
                                        CssClass="form-control" AutoPostBack="true" 
                                        OnSelectedIndexChanged="drpSDepartment_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <small class="text-danger">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ValidationGroup="Ticks" Display="Dynamic" ControlToValidate="drpSDepartment" 
                                            ErrorMessage="Department required" InitialValue="0"></asp:RequiredFieldValidator>
                                    </small>
                                </div>
                                
                                <div class="form-group col-md-6">
                                    <label>Physical Location <span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="DrpPhysicalLocation" runat="server" 
                                        CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <small class="text-danger">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                            ValidationGroup="Ticks" Display="Dynamic" ControlToValidate="DrpPhysicalLocation" 
                                            ErrorMessage="Physical location required" InitialValue="0"></asp:RequiredFieldValidator>
                                    </small>
                                </div>
                            </div>
                            
                            <h6 class="section-title mt-4">Patient Information</h6>
                            
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label>Patient Name <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtpatientname" runat="server" 
                                        CssClass="form-control" placeholder="Enter full name"></asp:TextBox>
                                    <small class="text-danger">
                                        <asp:RequiredFieldValidator ID="required3" runat="server" 
                                            ValidationGroup="Ticks" Display="Dynamic" ControlToValidate="txtpatientname" 
                                            ErrorMessage="Patient name required"></asp:RequiredFieldValidator>
                                    </small>
                                </div>
                                
                                <div class="form-group col-md-6">
                                    <label>MRN <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="txtMRN" runat="server" 
                                        CssClass="form-control" placeholder="Medical record number"></asp:TextBox>
                                    <small class="text-danger">
                                        <asp:RequiredFieldValidator ID="required1" runat="server" 
                                            ValidationGroup="Ticks" Display="Dynamic" ControlToValidate="txtMRN" 
                                            ErrorMessage="MRN required"></asp:RequiredFieldValidator>
                                    </small>
                                </div>
                            </div>
                            
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label>Contact Number</label>
                                    <asp:TextBox ID="txtcontact" runat="server" 
                                        CssClass="form-control" placeholder="Phone number"></asp:TextBox>
                                    <small class="text-danger">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                            ControlToValidate="txtcontact" ValidationExpression="\d+"
                                            Display="Dynamic" ErrorMessage="Numbers only"
                                            runat="server" />
                                    </small>
                                </div>
                                
                                <div class="form-group col-md-6">
                                    <label>Reported By <span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="drpusermt" runat="server" 
                                        CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <small class="text-danger">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                            ValidationGroup="Ticks" Display="Dynamic" ControlToValidate="drpusermt" 
                                            ErrorMessage="Reporter required" InitialValue="0"></asp:RequiredFieldValidator>
                                    </small>
                                </div>
                            </div>
                            
                            <h6 class="section-title mt-4">Incident Details</h6>
                            
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label>Category <span class="text-danger">*</span></label>
                                    <asp:DropDownList ID="DrpTCatSubCate" runat="server" 
                                        CssClass="form-control" AutoPostBack="true" 
                                        OnSelectedIndexChanged="DrpTCatSubCate_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <small class="text-danger">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                            ValidationGroup="Ticks" Display="Dynamic" ControlToValidate="DrpTCatSubCate" 
                                            ErrorMessage="Category required" InitialValue="0"></asp:RequiredFieldValidator>
                                    </small>
                                </div>
                                
                                <div class="form-group col-md-6">
                                    <label>Sub-Category</label>
                                    <asp:DropDownList ID="DrpSubCat" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            
                            <div class="form-group">
                                <label>Subject <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtSubject" runat="server" 
                                    CssClass="form-control" placeholder="Brief description of issue"></asp:TextBox>
                                <small class="text-danger">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ValidationGroup="Ticks" Display="Dynamic" ControlToValidate="txtSubject" 
                                        ErrorMessage="Subject required"></asp:RequiredFieldValidator>
                                </small>
                            </div>
                            
                            <div class="form-group">
                                <label>Complaint Details <span class="text-danger">*</span></label>
                                <asp:TextBox ID="txtMessage" runat="server" Rows="4" TextMode="MultiLine" 
                                    CssClass="form-control" placeholder="Describe the issue in detail"></asp:TextBox>
                                <small class="text-danger">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                        ValidationGroup="Ticks" Display="Dynamic" ControlToValidate="txtMessage" 
                                        ErrorMessage="Details required"></asp:RequiredFieldValidator>
                                </small>
                            </div>
                            
                            <h6 class="section-title mt-4">Additional Information</h6>
                            
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox ID="chklist" runat="server" AutoPostBack="true" 
                                            CssClass="custom-control-input" 
                                            oncheckedchanged="chklist_CheckedChanged" />
                                        <label class="custom-control-label" for="<%= chklist.ClientID %>">Staff Involved</label>
                                    </div>
                                    <asp:Panel ID="pnlStaffName" runat="server" Visible="false" class="mt-2">
                                        <asp:TextBox ID="txtstaffname" runat="server" 
                                            CssClass="form-control" placeholder="Staff name"></asp:TextBox>
                                    </asp:Panel>
                                </div>
                                
                                <div class="form-group col-md-6">
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox ID="chkirno" runat="server" 
                                            CssClass="custom-control-input" />
                                        <label class="custom-control-label" for="<%= chkirno.ClientID %>">IR Done</label>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <div class="custom-control custom-checkbox">
                                        <asp:CheckBox ID="chkinci" runat="server" 
                                            CssClass="custom-control-input" />
                                        <label class="custom-control-label" for="<%= chkinci.ClientID %>">Incident Report Needed</label>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="d-flex justify-content-end mt-4">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                    CssClass="btn btn-light border btn-action" onclick="btnCancel_Click" />
                                <asp:Button ID="btnSave" runat="server" Text="Submit Ticket" 
                                    CssClass="btn btn-primary btn-action ml-2" onclick="btnSave_Click" 
                                    ValidationGroup="Ticks" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <!-- Success Modal -->
    <div class="modal fade" id="small2" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title"><i class="fas fa-check-circle mr-2"></i>Success</h5>
                    <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body text-center py-4">
                    <i class="fas fa-check-circle fa-3x text-success mb-3"></i>
                    <h5>Ticket Created Successfully!</h5>
                    <p class="mb-0">Your ticket has been submitted to the support team</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" data-dismiss="modal">Continue</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
