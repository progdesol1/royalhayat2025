<%@ Page Title="" Language="C#" MasterPageFile="~/POS/POSMaster.Master" AutoEventWireup="true" CodeBehind="ClientTiketR.aspx.cs" Inherits="Web.POS.ClientTiketR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css" rel="stylesheet">
    <style type="text/css">
        .Star {
            background-image: url(../assets/images/Star.gif);
            height: 17px;
            width: 17px;
        }

        .WaitingStar {
            background-image: url(../assets/images/WaitingStar.gif);
            height: 17px;
            width: 17px;
        }

        .FilledStar {
            background-image: url(../assets/images/FilledStar.gif);
            height: 17px;
            width: 17px;
        }
        .complaint-header {
            background: #f8f9fa;
            border: 1px solid #dee2e6;
            transition: all 0.2s ease-in-out;
        }

        .complaint-header:hover {
            background: #f1f3f5; 
        }

        .complaint-header h1 {
            font-size: 1.5rem;
            color: #495057;
        }

        .complaint-header .form-select {
            min-width: 140px;
        }
        .todo-sidebar {
             font-size: 1.5rem;
        }

        .sidebar-card {
            border-radius: 10px;
            background-color: #f8f9fa;
            transition: all 0.2s ease-in-out;
        }

        .sidebar-card:hover {
            background-color: #f1f3f5;
        }

        .sidebar-card .card-header {
            border-bottom: 1px solid #e9ecef !important;
        }

        .list-group-item {
            font-size: 1.5rem;
        }
        .todo-sidebar {
            background-color: #f8f9fa;
            border-radius: 12px;
        }

        .todo-sidebar .card {
            border-radius: 12px;
        }

        .todo-sidebar .card-header h6 {
            font-size: 1.5rem;
            letter-spacing: 0.3px;
        }

        .todo-sidebar .list-group-item {
            font-size: 1.5rem;
        }

        .todo-sidebar .btn {
            transition: all 0.2s ease;
        }

        .todo-sidebar .btn:hover {
            transform: translateY(-1px);
            box-shadow: 0px 2px 6px rgba(0,0,0,0.08);
        }

        .todo-sidebar .form-control,
        .todo-sidebar .form-select {
            font-size: 1.5rem !important;
        }

        .ticket-sidebar {
            background: #f9fafb;
            border-radius: 10px;
            overflow-y: auto;
            max-height: 1500px;
        }

        .ticket-card {
            background: #fff;
            border-radius: 8px;
            padding: 12px;
            margin-bottom: 12px;
            box-shadow: 0 1px 4px rgba(0,0,0,0.06);
            transition: transform 0.1s ease, box-shadow 0.2s ease;
        }

        .ticket-card:hover {
            transform: translateY(-1px);
            box-shadow: 0 4px 12px rgba(0,0,0,0.08);
        }

        .ticket-header {
            display: flex;
            align-items: center;
            justify-content: space-between;
        }

        .ticket-avatar {
            width: 32px;
            height: 32px;
            border-radius: 50%;
            margin-right: 10px;
        }

        .ticket-title {
            flex-grow: 1;
            font-weight: 600;
            color: #333;
        }

        .ticket-actions .btn {
            padding: 2px 6px;
            font-size: 1.5rem;
        }

        .ticket-meta {
            font-size: 1.5rem;
            color: #777;
            margin: 5px 0;
        }

        .ticket-remarks {
            display: block;
            font-size: 1.5rem;
            color: #555;
            margin-top: 4px;
        }

        .ticket-footer {
            display: flex;
            align-items: center;
            justify-content: space-between;
            margin-top: 8px;
        }

        .ticket-date {
            font-size: 1.5rem;
            color: #666;
        }

        .ticket-status .badge {
            font-size: 1.5rem;
            padding: 4px 8px;
        }

        .ticket-pagination {
            display: flex;
            justify-content: center;
            padding: 8px;
        }

        .nav-tabs {
            border-bottom: none;
            background: #f8f9fa;
            border-radius: 8px;
            padding: 5px;
            display: inline-flex;
            gap: 8px;
        }
        .nav-tabs .nav-link {
            border: none;
            border-radius: 6px;
            padding: 8px 16px;
            font-weight: 500;
            color: #555;
            background-color: transparent;
            transition: all 0.3s ease;
        }
        .nav-tabs .nav-link:hover {
            background-color: #e9ecef;
            color: #000;
        }
        .nav-tabs .nav-link.active {
            background-color: #0d6efd;
            color: white;
            box-shadow: 0 2px 6px rgba(13, 110, 253, 0.3);
        }

        .complaint-section {
            background: #ffffff;
            border-radius: 10px;
            padding: 20px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
        }
        .complaint-section .row {
            border-bottom: 1px solid #f1f1f1;
            padding-bottom: 10px;
            margin-bottom: 10px;
        }
        .complaint-section .row:last-child {
            border-bottom: none;
        }
        .fw-bold {
            color: #495057;
        }
        .input-group-text {
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
       
        function openModalsmall2() {
            $('#small2').modal('show');

        }
        function validate() {
            if (document.getElementById("txtSubject").value.length != 50) {
                alert("text lenghth must be fifty");
                document.getElementById("txtSubject").select();
                document.getElementById("txtSubject").focus();
                return false;
            }
            else {
                return true;
            }
        }
        function validate1() {
            if (document.getElementById("txtComent").value.length != 1000) {
                alert("text lenghth must be thousand
                document.getElementById("txtComent").select();
                document.getElementById("txtComent").focus();
                return false;
            }
            else {
                return true;
            }

        }
        function validate2() {
            if (document.getElementById("txtMessage").value.length != 1000) {
                alert("text lenghth must be thousand");
                document.getElementById("txtMessage").select();
                document.getElementById("txtMessage").focus();
                return false;
            }
            else {
                return true;
            }
        }
    </script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            // Get actual ASP.NET rendered ID (ContentPlaceHolder1_btnSave)
            var btnSave = document.getElementById('<%= btnSave.ClientID %>');

            if (btnSave) {
                // Attach click event
                btnSave.addEventListener('click', function (e) {
                   //alert('btnSave clicked'); // 🟢 This will run ONLY when you click the button

                    // Run ASP.NET client-side validation for this ValidationGroup
                    if (typeof (Page_ClientValidate) === 'function') {
                        if (!Page_ClientValidate('Ticks')) {
                            e.preventDefault(); // Stop postback if validation fails
                            console.log('Validation failed');

                            // Find the first invalid field
                            var invalidField = document.querySelector('.tab-pane input:invalid');

                            if (invalidField) {
                                // Find parent tab of the invalid field
                                var parentTabId = invalidField.closest('.tab-pane').id;

                                // Activate the parent tab
                                var triggerEl = document.querySelector('[data-bs-target="#' + parentTabId + '"]');
                                if (triggerEl) {
                                    var tab = new bootstrap.Tab(triggerEl);
                                    tab.show();
                                }

                                invalidField.focus(); // Focus the invalid field
                            }
                            return false; // Prevent form submit
                        }
                    }
                });
            } else {
                console.error('btnSave not found. Is ClientID correct?');
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-color: #e9ecf3">
        <!-- Page Header -->
        <div class="complaint-header shadow-sm p-3 mb-4 rounded">
            <div class="d-flex justify-content-between align-items-center flex-wrap gap-2">

                <h1 class="h5 mb-0 fw-semibold text-dark">
                    Complaint for 
                    <asp:Label ID="lblUser" runat="server" CssClass="text-primary fw-bold"></asp:Label>
                </h1>
                <asp:DropDownList 
                    ID="drpyear1" 
                    runat="server" 
                    CssClass="form-select form-select-sm" 
                    AutoPostBack="true" 
                    Visible="false">
                </asp:DropDownList>

            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="todo-ui">
                    <div class="todo-sidebar bg-light p-3 rounded-3 shadow-sm">
    
                        <!-- Dashboard & New Ticket -->
                        <div class="card border-0 shadow-sm mb-4 rounded-4">
                            <div class="card-body p-2 d-grid gap-2">
                                <asp:Button ID="btndash" CssClass="btn btn-outline-primary fw-semibold rounded-pill" runat="server" Text="Go to Dashboard" OnClick="btndash_Click" />
                                <asp:Button ID="Button1" CssClass="btn btn-primary fw-semibold rounded-pill" runat="server" Text="New Ticket" OnClick="Button1_Click" />
                            </div>
                        </div>

                        <!-- Total Tickets -->
                        <div class="card border-0 shadow-sm mb-4 rounded-4">
                            <div class="card-header bg-white border-0 py-3 d-flex justify-content-between align-items-center">
                                <h6 class="fw-bold text-primary mb-0">
                                    <i class="fa fa-list me-2"></i>Total Tickets
                                </h6>
                                <asp:LinkButton ID="LinkRefreshTicket" runat="server" CssClass="btn btn-sm btn-light border rounded-circle" OnClick="LinkRefreshTicket_Click" ToolTip="Refresh">
                                    <i class="fa fa-refresh"></i>
                                </asp:LinkButton>
                            </div>
                            <ul class="list-group list-group-flush">
                                <li class="list-group-item bg-light px-3 py-2 border-0">
                                    <asp:LinkButton ID="linkAllNew" runat="server" CssClass="text-decoration-none text-primary fw-semibold" OnClick="linkAllNew_Click">
                                        Create a New Ticket
                                    </asp:LinkButton>
                                </li>
                                <li class="list-group-item px-3 py-2 border-0 bg-white rounded-3 shadow-sm mb-2 d-flex justify-content-between align-items-center">
                                    <asp:LinkButton ID="linkAllPending" runat="server" CssClass="text-dark text-decoration-none" OnClick="linkAllPending_Click">
                                        Pending
                                    </asp:LinkButton>
                                    <span class="badge bg-danger rounded-pill">
                                        <asp:Label ID="leblANew1" runat="server"></asp:Label>
                                    </span>
                                </li>
                                <li class="list-group-item px-3 py-2 border-0 bg-white rounded-3 shadow-sm mb-2 d-flex justify-content-between align-items-center">
                                    <asp:LinkButton ID="linkAllProgress" runat="server" CssClass="text-dark text-decoration-none" OnClick="linkAllProgress_Click">
                                        In Progress
                                    </asp:LinkButton>
                                    <span class="badge bg-success rounded-pill">
                                        <asp:Label ID="leblANew3" runat="server"></asp:Label>
                                    </span>
                                </li>
                                <li class="list-group-item px-3 py-2 border-0 bg-white rounded-3 shadow-sm d-flex justify-content-between align-items-center">
                                    <asp:LinkButton ID="linkAllClose" runat="server" CssClass="text-dark text-decoration-none" OnClick="linkAllClose_Click">
                                        Closed
                                    </asp:LinkButton>
                                    <span class="badge bg-warning text-dark rounded-pill">
                                        <asp:Label ID="leblANew4" runat="server"></asp:Label>
                                    </span>
                                </li>
                            </ul>
                        </div>

                        <!-- Search -->
                        <div class="card border-0 shadow-sm mb-4 rounded-4">
                            <div class="card-header bg-white border-0 py-3">
                                <h6 class="fw-bold text-primary mb-0">
                                    <i class="fa fa-search me-2"></i>Search
                                </h6>
                            </div>
                            <div class="card-body p-3">
                                <asp:DropDownList ID="drpyears" runat="server" AutoPostBack="true" CssClass="form-select mb-2 rounded-pill fs-6 py-2 px-3" OnSelectedIndexChanged="drpyears_SelectedIndexChanged"></asp:DropDownList>
                                <asp:DropDownList ID="drpmonths" runat="server" AutoPostBack="true" CssClass="form-select mb-2 rounded-pill fs-6 py-2 px-3"></asp:DropDownList>
                                <asp:DropDownList ID="drpusers" runat="server" AutoPostBack="true" CssClass="form-select mb-2 rounded-pill fs-6 py-2 px-3"></asp:DropDownList>
                                <asp:TextBox ID="txtpatientnames" runat="server" CssClass="form-control  mb-2 rounded-pill" placeholder="Patient Name"></asp:TextBox>
                                <asp:TextBox ID="txtcnose" runat="server" CssClass="form-select mb-2 rounded-pill fs-6 py-2 px-3" placeholder="Complaint No"></asp:TextBox>
                                <asp:Button ID="btnoksearch" runat="server" Text="Search" CssClass="btn btn-primary w-100 fw-semibold mb-2 rounded-pill" OnClick="btnoksearch_Click" />
                                <asp:Button ID="btncount" runat="server" CssClass="btn btn-secondary w-100 fw-semibold rounded-pill" Visible="false" />
                            </div>
                        </div>

                        <!-- Re-Open Ticket -->
                        <div class="card border-0 shadow-sm rounded-4">
                            <div class="card-header bg-white border-0 py-3">
                                <asp:LinkButton ID="linkreopen" runat="server" CssClass="fw-bold text-danger" Visible="false" OnClick="linkreopen_Click">
                                    <i class="fa fa-undo me-1"></i>Re-Open
                                </asp:LinkButton>
                            </div>
                            <div class="card-body p-3">
                                <asp:Label ID="lblticket" runat="server" Text="Master Code" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtticket" runat="server" Visible="false"></asp:TextBox>
                                <asp:Label ID="lblcomplain" runat="server" Text="Complaint Number" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtcomplain" runat="server" Visible="false"></asp:TextBox>
                                <asp:Button ID="btnsearch" runat="server" Text="Search" Visible="false" OnClick="btnsearch_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="todo-content">
                        <div class="portlet light bordered">
                            <div class="portlet-body">
                                <div class="row">
                                    <div class="col-md-5 col-sm-4">
                                        <div class="ticket-sidebar p-2">
                                            <asp:Panel ID="PnlBindTick" runat="server">
                                                <asp:UpdatePanel runat="server" ID="upnl">
                                                    <ContentTemplate>
                                                        <asp:ListView ID="ltsRemainderNotes" runat="server"
                                                            OnItemCommand="ltsRemainderNotes_ItemCommand"
                                                            OnItemDataBound="ltsRemainderNotes_ItemDataBound"
                                                            ItemPlaceholderID="itemPlaceHolder1"
                                                            GroupPlaceholderID="groupPlaceHolder1"
                                                            OnPagePropertiesChanging="ltsRemainderNotes_PagePropertiesChanging">

                                                            <LayoutTemplate>
                                                                <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                                                <div class="ticket-pagination">
                                                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ltsRemainderNotes" PageSize="5">
                                                                        <Fields>
                                                                            <asp:NextPreviousPagerField ButtonType="Link" ShowPreviousPageButton="true" ShowNextPageButton="false" />
                                                                            <asp:NumericPagerField ButtonType="Link" />
                                                                            <asp:NextPreviousPagerField ButtonType="Link" ShowPreviousPageButton="false" ShowNextPageButton="true" />
                                                                        </Fields>
                                                                    </asp:DataPager>
                                                                </div>
                                                            </LayoutTemplate>

                                                            <GroupTemplate>
                                                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                            </GroupTemplate>

                                                            <ItemTemplate>
                                                                <div class="ticket-card">
                                                                    <div class="ticket-header">
                                                                        <img src="../CRM/images/No_image.png" class="ticket-avatar" alt="User" />
                                                                        <div class="ticket-title">
                                                                            <asp:LinkButton ID="btnnames" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btnnames" runat="server" CssClass="ticket-user">
                                                                                <%# GetUName(Convert.ToInt32(Eval("ReportedBy"))) %>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                        <div class="ticket-actions">
                                                                            <asp:LinkButton ID="btnclick123" CssClass="btn btn-sm btn-outline-primary" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btnclick123" runat="server">Reply</asp:LinkButton>
                                                                            <asp:LinkButton ID="btneditticket" CssClass="btn btn-sm btn-outline-secondary" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btneditticket" runat="server">Edit</asp:LinkButton>
                                                                            <a href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("MasterCODE")%>' class="btn btn-sm btn-danger" target="_blank">View</a>
                                                                        </div>
                                                                    </div>

                                                                    <div class="ticket-meta">
                                                                        <small>Follow Up By: <%#GetEmplName(Convert.ToInt32(Eval("FoloEmp")))%></small>
                                                                    </div>

                                                                    <asp:LinkButton ID="btnremarks" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btnremarks" runat="server" CssClass="ticket-remarks">
                                                                        <%#Eval("Remarks")%>
                                                                    </asp:LinkButton>

                                                                    <div class="ticket-footer">
                                                                        <asp:LinkButton ID="btndates" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btndates" runat="server" CssClass="ticket-date">
                                                                            <i class="fa fa-calendar"></i> <%# Convert.ToDateTime(Eval("UploadDate")).ToString("dd-MMM-yyyy hh:mm tt")%>
                                                                        </asp:LinkButton>
                                                                        <div class="ticket-status">
                                                                            <asp:LinkButton ID="btnpendings" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btnpendings" runat="server" CssClass="badge bg-warning text-dark" Visible='<%# (string)Eval("MyStatus") == "Pending"%>'>Pending</asp:LinkButton>
                                                                            <asp:LinkButton ID="btninprogress" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btninprogress" runat="server" CssClass="badge bg-info text-dark" Visible='<%# (string)Eval("MyStatus") == "InProgress"%>'>In Progress</asp:LinkButton>
                                                                            <asp:LinkButton ID="btnclosed" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btnclosed" runat="server" CssClass="badge bg-success" Visible='<%# (string)Eval("MyStatus") == "Closed"%>'>Closed</asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>

                                                        </asp:ListView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="todo-tasklist-devider"></div>
                                    <div class="col-md-7 col-sm-8">
                                        <div class="scroller" style="max-height: 1500px;" data-always-visible="0" data-rail-visible="0" data-handle-color="#dae3e7">
                                            <div class="form-horizontal" style="padding-left: 10px;">
                                                <!-- TASK HEAD -->
                                                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                                                    <div class="alert alert-success alert-dismissable">
                                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlTicki" runat="server">
                                                  <ul class="nav nav-tabs flex-column flex-sm-row" id="profileTabs" role="tablist">
                                                      <li class="nav-item" role="presentation">
                                                          <button class="nav-link active" id="education-tab" data-bs-toggle="tab" data-bs-target="#education" type="button" role="tab">
                                                              COMPLAINT DATA
                                                          </button>
                                                      </li>
                                                      <li class="nav-item" role="presentation">
                                                          <button class="nav-link" id="experience-tab" data-bs-toggle="tab" data-bs-target="#experience" type="button" role="tab">
                                                              PERSONAL DATA
                                                          </button>
                                                      </li>
                                                      <li class="nav-item" role="presentation">
                                                          <button class="nav-link" id="skills-tab" data-bs-toggle="tab" data-bs-target="#skills" type="button" role="tab">
                                                              INVESTIGATION DATA
                                                          </button>
                                                      </li>
                                                  </ul>
                                                <div class="form">
                                                  <div class="tab-content p-3 border border-top-0" id="profileTabsContent">
                                                      <!-- complaint Tab -->
                                                      <div class="tab-pane show active" id="education" role="tabpanel" aria-labelledby="education-tab">
                                                                            <div class="complaint-section">
                                                                              <div class="row">
                                                                                <div class="col-4">
                                                                                  <!-- Complaint No & Log Only -->
                                                                                  <div class="mb-3">
                                                                                      <label class="form-label fw-bold">
                                                                                          <asp:Label ID="Label7" runat="server" Text="Complaint No." />
                                                                                      </label>
                                                                                      <asp:Label ID="lblcomplainno" runat="server" CssClass="form-control-plaintext" />
                                                                                  </div>
                                                                                </div>
                                                                                <div class="col-4">
                                                                                  <div class="mb-3">
                                                                                      <div >
                                                                                          <asp:CheckBox ID="chklog" runat="server" AutoPostBack="true" />
                                                                                          <label class="form-check-label fw-bold" for="chklog">
                                                                                              <asp:Label ID="Label24" runat="server" Text="Log Only" />
                                                                                          </label>
                                                                                      </div>
                                                                                  </div>
                                                                                </div>
                                                                                <div class="col-4">
                                                                                  <!-- Creation Date -->
                                                                                  <div class="mb-3">
                                                                                      <label class="form-label fw-bold">
                                                                                          <asp:Label ID="Label1" runat="server" Text="Creation Date" />
                                                                                      </label>
                                                                                      <asp:Label ID="lbldatesd" runat="server" CssClass="form-control-plaintext" />
                                                                                  </div>
                                                                                </div>
                                                                              </div>

                                                                                <!-- Complaint Date -->
                                                                                <div class="mb-3">
                                                                                    <label class="form-label fw-bold">
                                                                                        <asp:Label ID="lbldate" runat="server" Text="Complaint Date" />
                                                                                    </label>

                                                                                    <div class="input-group">
                                                                                      <div class="row">
                                                                                        <div class="col-1">
                                                                                          <!-- Calendar Button -->
                                                                                          <button class="btn btn-outline-secondary" type="button" id="calender">
                                                                                              <i class="bi bi-calendar"></i>
                                                                                          </button>
                                                                                        </div>
                                                                                        <div class="col-11">
                                                                                          <!-- Date Textbox -->
                                                                                          <asp:TextBox 
                                                                                              ID="txtdates" 
                                                                                              runat="server" 
                                                                                              CssClass="form-control" 
                                                                                              OnTextChanged="txtdates_TextChanged1">
                                                                                          </asp:TextBox>
                                                                                        </div>
                                                                                      </div>
                                                                                    </div>

                                                                                    <!-- Validation & Message -->
                                                                                    <asp:Label ID="lblmsgl" runat="server" Visible="false" CssClass="text-danger small mt-1 d-block"></asp:Label>                  

              <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtdates" ErrorMessage="Date required" InitialValue="0"></asp:RequiredFieldValidator>

                                                                                    <!-- Calendar Extender -->
                                                                                    <cc1:CalendarExtender 
                                                                                        ID="CalendarExtendertxtdateTO" 
                                                                                        runat="server" 
                                                                                        Enabled="True" 
                                                                                        PopupButtonID="calender" 
                                                                                        TargetControlID="txtdates" 
                                                                                        Format="dd/MMM/yyyy">
                                                                                    </cc1:CalendarExtender>
                                                                                </div>

                                                                               <!-- Complaint Type -->
                                                                                <div class="mb-3">
                                                                                    <label class="form-label fw-bold">
                                                                                        <asp:Label ID="Label5" runat="server" Text="Complaint Type" />
                                                                                    </label>
                                                                                    <asp:DropDownList ID="drpComplainType" runat="server" 
                                                                                        CssClass="form-select form-select-lg select2me w-100" AutoPostBack="true">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Ticks"
                                                                                        Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpComplainType"
                                                                                        ErrorMessage="Complain Type required" InitialValue="0">
                                                                                    </asp:RequiredFieldValidator>
                                                                                </div>

                                                                                <!-- Department -->
                                                                                <div class="mb-3">
                                                                                    <label class="form-label fw-bold">
                                                                                        <asp:Label ID="Label4" runat="server" Text="Department" />
                                                                                    </label>
                                                                                    <asp:DropDownList ID="drpSDepartment" runat="server" 
                                                                                        CssClass="form-select form-select-lg select2me w-100" AutoPostBack="true" 
                                                                                        OnSelectedIndexChanged="drpSDepartment_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Ticks"
                                                                                        Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpSDepartment"
                                                                                        ErrorMessage="Department required" InitialValue="0">
                                                                                    </asp:RequiredFieldValidator>
                                                                                </div>

                                                                                <!-- Physical Location -->
                                                                                <div class="mb-3">
                                                                                    <label class="form-label fw-bold">
                                                                                        <asp:Label ID="Label8" runat="server" Text="Physical Location" />
                                                                                    </label>
                                                                                    <asp:DropDownList ID="DrpPhysicalLocation" runat="server" 
                                                                                        CssClass="form-select form-select-lg select2me w-100" AutoPostBack="true">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Ticks"
                                                                                        Display="Dynamic" ForeColor="#a94442" ControlToValidate="DrpPhysicalLocation"
                                                                                        ErrorMessage="Physical Location required" InitialValue="0">
                                                                                    </asp:RequiredFieldValidator>
                                                                                </div>

                                                                 

                                                                              <div class="row">
                                                                                <div class="col-6">
                                                                                                                                                       <!-- Staff Involved -->
                                                                                   <div class="mb-3">
                                                                                       <label class="form-label fw-bold">
                                                                                           <asp:Label ID="Label15" runat="server" Text="Staff Involved" />
                                                                                       </label>
                                                                                       <div>
                                                                                           <asp:CheckBox ID="chklist" runat="server" OnCheckedChanged="chklist_CheckedChanged" AutoPostBack="true" />
                                                                                           <label class="form-check-label">Yes</label>
                                                                                       </div>
                                                                                       <asp:TextBox ID="txtstaffname" runat="server" CssClass="form-control mt-2" Visible="false" placeholder="Staff Name"></asp:TextBox>
                                                                                   </div>
                                                                                </div>
                                                                                <div class="col-3">
                                                                                   <div class="mb-3">
                                                                                       <label class="form-label fw-bold">
                                                                                           <asp:Label ID="Label19" runat="server" Text="Inc Rep" />
                                                                                       </label>
                                                                                       <div>
                                                                                           <asp:CheckBox ID="chkinci" runat="server"/>
                                                                                       </div>
                                                                                   </div>
                                                                                </div>
                                                                                <div class="col-3">
                                                                                  <div class="mb-3">
                                                                                      <label class="form-label fw-bold">
                                                                                          <asp:Label ID="Label16" runat="server" Text="IR Done" />
                                                                                      </label>
                                                                                      <div>
                                                                                          <asp:CheckBox ID="chkirno" runat="server"/>
                                                                                      </div>
                                                                                  </div>
                                                                                </div>
                                                                              </div>
                                                                            </div>
                                                                        </div>
                                                      <!-- Personal Tab -->
                                                      <div class="tab-pane" id="experience" role="tabpanel" aria-labelledby="experience-tab">

                                                          <!-- Patient Name -->
                                                          <div class="mb-3">
                                                              <label class="form-label fw-semibold">
                                                                  <asp:Label ID="Label13" runat="server" Text="Patient Name" />
                                                              </label>
                                                              <asp:TextBox ID="txtpatientname" runat="server" CssClass="form-control shadow-sm" placeholder="Enter patient name"></asp:TextBox>
                                                              <asp:RequiredFieldValidator ID="required3" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtpatientname" ErrorMessage="Please add Patient Name"></asp:RequiredFieldValidator>
                                                          </div>

                                                          <!-- MRN -->
                                                          <div class="mb-3">
                                                              <label class="form-label fw-semibold">
                                                                  <asp:Label ID="Label14" runat="server" Text="MRN" />
                                                              </label>
                                                              <asp:TextBox ID="txtMRN" runat="server" CssClass="form-control shadow-sm" placeholder="Enter MRN"></asp:TextBox>
                                                          </div>

                                                          <!-- Subject -->
                                                          <div class="mb-3">
                                                              <label class="form-label fw-semibold">
                                                                  <asp:Label ID="Label2" runat="server" Text="Subject" />
                                                              </label>
                                                              <asp:TextBox ID="txtSubject" runat="server" MaxLength="100" TextMode="MultiLine" Rows="2"
                                                                  CssClass="form-control shadow-sm" placeholder="Write the subject"></asp:TextBox>
                                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtSubject" ErrorMessage="Subject required"></asp:RequiredFieldValidator>
                                                          </div>

                                                          <!-- Complaint Details -->
                                                          <div class="mb-3">
                                                              <label class="form-label fw-semibold">
                                                                  <asp:Label ID="Label3" runat="server" Text="Complaint Details" />
                                                              </label>
                                                              <asp:TextBox ID="txtMessage" runat="server" MaxLength="1000" TextMode="MultiLine" Rows="15"
                                                                  CssClass="form-control shadow-sm" placeholder="Write the message"></asp:TextBox>
                                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtMessage" ErrorMessage="Message required"></asp:RequiredFieldValidator>
                                                          </div>

                                                      </div>
                                                      <!-- INVESTIGATION DATA Tab -->
                                                      <div class="tab-pane" id="skills" role="tabpanel" aria-labelledby="skills-tab">

    <!-- Contact No -->
    <div class="mb-3">
        <label class="form-label fw-semibold">
            <asp:Label ID="Label18" runat="server" Text="Contact No" />
        </label>
        <asp:TextBox ID="txtcontact" runat="server" CssClass="form-control shadow-sm" placeholder="Enter contact number" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
             ControlToValidate="txtcontact"
             ValidationExpression="\d+"
             Display="Static"
             EnableClientScript="true"
             ErrorMessage="Please enter numbers only"
             ForeColor="#a94442"
             ValidationGroup="Ticks"
             runat="server" />
    </div>

    <!-- Category -->
    <div class="mb-3">
        <label class="form-label fw-semibold">
            <asp:Label ID="Label9" runat="server" Text="Category" />
        </label>
        <asp:DropDownList 
            ID="DrpTCatSubCate" 
            runat="server" 
           CssClass="form-select form-select-lg select2me w-100" 
            OnSelectedIndexChanged="DrpTCatSubCate_SelectedIndexChanged" 
            AutoPostBack="true" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="DrpTCatSubCate" ErrorMessage="Category required" InitialValue="0"></asp:RequiredFieldValidator>
    </div>

    <!-- Sub-Category -->
    <div class="mb-3">
        <label class="form-label fw-semibold">
            <asp:Label ID="Label10" runat="server" Text="Sub-Category" />
        </label>
        <asp:DropDownList 
            ID="DrpSubCat" 
            runat="server" 
            CssClass="form-select form-select-lg select2me w-100" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="DrpSubCat" ErrorMessage="Sub-Category required" InitialValue="0"></asp:RequiredFieldValidator>
    </div>

    <!-- Investigation Result -->
    <div class="mb-3">
        <label class="form-label fw-semibold">
            <asp:Label ID="Label22" runat="server" Text="Investigation Result" />
        </label>
        <asp:DropDownList 
            ID="drpinvestresult" 
            runat="server" 
            AutoPostBack="true" 
            CssClass="form-select form-select-lg select2me w-100">
            <asp:ListItem Value="1" Text="Relevant" />
            <asp:ListItem Value="2" Text="Irrelevant" />
        </asp:DropDownList>
    </div>

    <!-- Reported by -->
    <div class="mb-3">
        <label class="form-label fw-semibold">
            <asp:Label ID="Label17" runat="server" Text="Reported by" />
        </label>
        <asp:DropDownList 
            ID="drpusermt" 
            runat="server" 
            CssClass="form-select form-select-lg select2me w-100" 
            AutoPostBack="true" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpusermt" ErrorMessage="reported by required" InitialValue="0"></asp:RequiredFieldValidator>
    </div>

    <!-- Follow Up By -->
    <div class="mb-3">
        <label class="form-label fw-semibold">
            <asp:Label ID="Label21" runat="server" Text="Follow Up By" />
        </label>
        <asp:DropDownList 
            ID="drpfoloemp" 
            runat="server" 
            CssClass="form-select form-select-lg select2me w-100" 
            AutoPostBack="true" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpfoloemp" ErrorMessage="Employee required" InitialValue="0"></asp:RequiredFieldValidator>
    </div>

</div>
                                                  </div>
                                                  <!-- Hidden Field to store tab -->
                                                  <asp:HiddenField ID="hfActiveTab" runat="server" />
                                                    <div class="d-flex flex-wrap gap-2 justify-content-end mt-3">
                                                        <asp:Button ID="btnattache" runat="server" CssClass="btn btn-outline-primary btn-sm" OnClick="btnattache_Click" Text="Attachments" />
    
                                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-sm" OnClick="btnSave_Click" Text="Submit" ValidationGroup="Ticks" />
    
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary btn-sm" OnClick="btnCancel_Click" Text="Cancel" />
    
                                                        <asp:Button ID="btncack" runat="server" CssClass="btn btn-outline-secondary btn-sm" OnClick="btncack_Click" Text="Back" Visible="false" />
    
                                                        <asp:Button ID="btndelete" runat="server" CssClass="btn btn-outline-danger btn-sm" OnClick="btndelete_Click" Text="Delete" Visible="false" />
    
                                                        <asp:Button ID="btncloseandupdate" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btncloseandupdate_Click" Text="Close Tkt & Save" />
                                                    </div>
                                                </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlinfo" runat="server">
                                                  <div class="d-flex flex-wrap gap-3 p-3">

                                                    <div class="d-flex align-items-center rounded-pill bg-primary text-white px-4 py-2 shadow-sm">
                                                      <i class="bi bi-building me-2"></i>
                                                      <span class="fw-semibold">Department:</span>
                                                      <asp:Label ID="lblinfoDepartment" runat="server" CssClass="ms-2 fw-normal"></asp:Label>
                                                    </div>

                                                    <div class="d-flex align-items-center rounded-pill bg-success text-white px-4 py-2 shadow-sm">
                                                      <i class="bi bi-geo-alt me-2"></i>
                                                      <span class="fw-semibold">Location:</span>
                                                      <asp:Label ID="lblinfoLocation" runat="server" CssClass="ms-2 fw-normal"></asp:Label>
                                                    </div>

                                                    <div class="d-flex align-items-center rounded-pill bg-warning text-dark px-4 py-2 shadow-sm">
                                                      <i class="bi bi-exclamation-circle me-2"></i>
                                                      <span class="fw-semibold">Complaint:</span>
                                                      <asp:Label ID="lblinfocomplaintype" runat="server" CssClass="ms-2 fw-normal"></asp:Label>
                                                    </div>

                                                  </div>
                                                </asp:Panel>
                                                <asp:Panel ID="panChat" runat="server" DefaultButton="btnSubmit">
                                                    <asp:Timer ID="Timer1" runat="server" Interval="18000" OnTick="Timer1_Tick">
                                                    </asp:Timer>
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" class="item" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="tabbable-line" style="padding-left: 10px;">
                                                                <ul class="nav nav-pills mb-3" id="complainTabs" role="tablist">
                                                                   <li class="nav-item" role="presentation">
                                                                          <a class="nav-link active" id="comments-tab" data-toggle="tab" href="#tab_1" role="tab" aria-controls="tab_1" aria-selected="true">
                                                                              <i class="bi bi-chat-text"></i> Comments
                                                                          </a>
                                                                      </li>
                                                                   <li class="nav-item" role="presentation">
                                                                          <a class="nav-link" id="history-tab" data-toggle="tab" href="#tab_2" role="tab" aria-controls="tab_2" aria-selected="false">
                                                                              <i class="bi bi-clock-history"></i> History
                                                                          </a>
                                                                      </li>
                                                                </ul>
                                                               <div class="tab-content p-3 border rounded bg-white shadow-sm">
                                                                  <div class="tab-pane fade show active" id="tab_1">
                                                                      <asp:ListView ID="listChet" runat="server" OnItemCommand="listChet_ItemCommand">
                                                                          <ItemTemplate>
                                                                              <div class="card mb-3 border-0 border-bottom pb-2">
                                                                                  <div class="d-flex align-items-start">
                                                                                      <img src="../CRM/images/No_image.png" class="rounded-circle me-3" style="width: 40px; height: 40px;">
                                                                                      <div class="flex-grow-1">
                                                                                          <div class="d-flex justify-content-between align-items-center">
                                                                                              <div>
                                                                                                  <strong><%# Eval("Version") %></strong> - 
                                                                                                  <span class="text-muted"><%# Eval("MasterCODE") %></span>
                                                                                              </div>
                                                                                              <small class="text-muted">
                                                                                                  <%# Convert.ToDateTime(Eval("InitialDate")).ToString("dd-MMM-yyyy hh:mm tt") %> 
                                                                                                  <span class="mx-1">•</span>
                                                                                                  <%# Convert.ToDateTime(Eval("UploadDate")).ToString("dd-MMM-yyyy hh:mm tt") %>
                                                                                              </small>
                                                                                          </div>
                                                                                          <p class="mt-2 mb-1"><%# Eval("MyStatus") %></p>

                                                                                          <!-- Hidden Fields -->
                                                                                          <asp:Label ID="lblcompid" runat="server" Text='<%# Eval("COMPID") %>' Visible="false"></asp:Label>
                                                                                          <asp:Label ID="lblmastercode" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>
                                                                                          <asp:Label ID="lblMyLineNo" runat="server" Text='<%# Eval("MyLineNo") %>' Visible="false"></asp:Label>
                                                                                          <asp:Label ID="lblLocationID" runat="server" Text='<%# Eval("LocationID") %>' Visible="false"></asp:Label>
                                                                                          <asp:Label ID="lblLinkMasterCODE" runat="server" Text='<%# Eval("LinkMasterCODE") %>' Visible="false"></asp:Label>
                                                                                          <asp:Label ID="lblActivityID" runat="server" Text='<%# Eval("ActivityID") %>' Visible="false"></asp:Label>
                                                                                          <asp:Label ID="lblPrefix" runat="server" Text='<%# Eval("Prefix") %>' Visible="false"></asp:Label>

                                                                                          <div class="mt-2">
                                                                                              <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("MasterCODE") %>' runat="server" CssClass="btn btn-sm btn-warning">
                                                                                                  <i class="fa fa-pencil"></i> Edit
                                                                                              </asp:LinkButton>
                                                                                          </div>
                                                                                      </div>
                                                                                  </div>
                                                                              </div>
                                                                          </ItemTemplate>
                                                                      </asp:ListView>
                                                                  </div>
                                                                  <div class="tab-pane fade" id="tab_2">
                                                                      <asp:ListView ID="ListHistoy" runat="server">
                                                                          <ItemTemplate>
                                                                              <div class="border-start border-3 border-primary ps-3 mb-3">
                                                                                  <small class="text-muted d-block">
                                                                                      <%# Convert.ToDateTime(Eval("UploadDate")).ToString("dd-MMM-yyyy hh:mm tt") %>
                                                                                  </small>
                                                                                  <span><%# Eval("Version") %></span>
                                                                              </div>
                                                                          </ItemTemplate>
                                                                      </asp:ListView>
                                                                  </div>
                                                              </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                   <div class="card shadow-sm border-0 mb-3">
                                                     <div class="card-body">

                                                            <!-- ACTION SELECTION -->
                                                            <div class="mb-3">
                                                                <asp:DropDownList ID="aspcomment" runat="server" OnSelectedIndexChanged="aspcomment_SelectedIndexChanged" AutoPostBack="true" CssClass="form-select">
                                                                    <asp:ListItem Value="0" Text="--- Select Action ---"></asp:ListItem>
                                                                    <asp:ListItem Value="2101401" Text="Immediate action"></asp:ListItem>
                                                                    <asp:ListItem Value="2101402" Text="Follow-up"></asp:ListItem>
                                                                    <asp:ListItem Value="2101403" Text="Corrective action"></asp:ListItem>
                                                                    <asp:ListItem Value="2101404" Text="Investigation results"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <!-- INVESTIGATION DROPDOWN -->
                                                            <div class="mb-3" id="investigationWrapper">
                                                                <asp:DropDownList ID="drpinvestigation" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="drpinvestigation_SelectedIndexChanged1" CssClass="form-select">
                                                                    <asp:ListItem Value="1" Text="Relevant"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Irrelevant"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <!-- COMMENT BOX -->
                                                            <div class="mb-3">
                                                                <asp:TextBox ID="txtComent" runat="server" placeholder="Type your comment..." MaxLength="1000" Rows="4" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            </div>

                                                            <!-- STATUS -->
                                                            <div class="row mb-3">
                                                                <div class="col-md-4">
                                                                    <label class="fw-bold">
                                                                        <asp:Label ID="Label6" runat="server" Text="Status:"></asp:Label>
                                                                    </label>
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-select">
                                                                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                                        <asp:ListItem Value="Closed">Closed</asp:ListItem>
                                                                        <asp:ListItem Value="InProgress">InProgress</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <!-- RATING -->
                                                            <div class="row mb-3">
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label20" runat="server" Visible="false"></asp:Label>
                                                                </div>
                                                                <div class="col-md-8 d-flex align-items-center">
                                                                    <cc1:Rating ID="Rating1" AutoPostBack="true" runat="server" Visible="false"
                                                                        StarCssClass="Star" WaitingStarCssClass="WaitingStar" OnChanged="Rating1_Changed"
                                                                        EmptyStarCssClass="Star" FilledStarCssClass="FilledStar">
                                                                    </cc1:Rating>
                                                                    <asp:Label ID="lblRatingStatus" runat="server" Text="5" CssClass="ms-2" Visible="false"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <!-- ACTION BUTTONS -->
                                                            <div class="d-flex justify-content-between mt-3">
                                                                <div>
                                                                    <asp:LinkButton ID="lbllblattachments2" CssClass="btn btn-success btn-sm" runat="server" Text="Attachments" OnClick="lblattachments_Click"></asp:LinkButton>
                                                                    <asp:Button ID="btnAttch" runat="server" Text="0" CssClass="btn btn-success btn-sm" OnClick="btnAttch_Click" />
                                                                </div>
                                                                <div>
                                                                    <asp:Button ID="btnSubmit" ValidationGroup="" Visible="false" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnSubmit_Click" Text="Submit" />
                                                                    <asp:Button ID="btnTikitClose" runat="server" CssClass="btn btn-secondary btn-sm" OnClick="btnTikitClose_Click" Text="Back" />
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
                    </div>
                </div>
            </div>
        </div>
    </div>
<div class="modal fade bs-modal-sm" id="small2" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm" style="margin-bottom: 0px; margin-top: 0px;">
            <div class="modal-content">
                <div class="portlet box green">
                    <div class="modal-header" style="padding-top: 10px; padding-bottom: 10px;">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title" style="color: white;"><i class="fa fa-save"></i>&nbsp;Success</h4>
                    </div>
                </div>
                <div class="modal-body">
                    <p id="lblmsgpop2" style="text-align: center; font-family: 'Courier New';">Your Ticket Generate Successfully...</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
