<%@ Page Title="" Language="C#" MasterPageFile="~/POS/POSMaster.Master" AutoEventWireup="true" CodeBehind="ClientTiketR.aspx.cs" Inherits="Web.POS.ClientTiketR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
               alert('btnSave clicked'); // 🟢 This will run ONLY when you click the button

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
        <%--class="page-content"--%>
        <!-- BEGIN PAGE HEAD-->
        <div class="page-head">
            <!-- BEGIN PAGE TITLE -->
            <div class="page-title">
                <h1>Complaint for
                    <asp:Label ID="lblUser" runat="server"></asp:Label>
                    <small></small>
                </h1>
                <br />
                <%--<asp:Button ID="btn2015" runat="server" Text="2015" OnClick="btn2015_Click"/>
                 <asp:Button ID="btnsub2" runat="server" Text="2016" OnClick="btnsub2_Click"/>
                <asp:Button ID="btnsub3" runat="server" Text="2017" OnClick="btnsub3_Click"/>
                <asp:Button ID="btnsub4" runat="server" Text="2018" OnClick="btnsub4_Click"/>
                <asp:Button ID="btnsub5" runat="server" Text="2019" OnClick="btnsub5_Click"/>--%>

                <asp:DropDownList ID="drpyear1" runat="server" CssClass="form-control" AutoPostBack="true" Visible="false"></asp:DropDownList>
            </div>

        </div>

        <%-- <ul class="page-breadcrumb breadcrumb">
            <li>
                <a href="index.aspx ">Home</a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <span class="active">Ticket Create</span>
            </li>
        </ul>--%>

        <div class="row">
            <div class="col-md-12">
                <!-- BEGIN TODO SIDEBAR -->
                <div class="todo-ui">
                    <div class="todo-sidebar">
                        <%-- <asp:UpdatePanel runat="server" ID="UpdatePanel2" class="item">
                            <ContentTemplate>--%>
                        <div class="portlet light bordered">
                            <div class="portlet-title">
                                <asp:Button ID="btndash" CssClass="btn btn-info" runat="server" Text="Go to Dashboard" OnClick="btndash_Click" /><br />
                                <br />
                                <asp:Button ID="Button1" CssClass="btn btn-info" runat="server" Text="New Ticket" OnClick="Button1_Click" Style="margin-top: -18px;" />
                            </div>
                        </div>
                        <div class="portlet light bordered">
                            <div class="portlet-title">
                                <div class="caption" data-toggle="collapse" data-target=".todo-project-list-content">
                                    <span class="caption-subject font-green-sharp bold uppercase">Total Ticket </span>
                                    <asp:LinkButton ID="LinkRefreshTicket" runat="server" Style="padding-left: 10px;" OnClick="LinkRefreshTicket_Click"><i class="fa fa-refresh"></i></asp:LinkButton><br />
                                    <span class="caption-helper visible-sm-inline-block visible-xs-inline-block">click to view New Ticket  list</span>
                                </div>
                            </div>
                            <div class="portlet-body todo-project-list-content">
                                <div class="todo-project-list">
                                    <ul class="nav nav-stacked">
                                        <li>
                                            <asp:LinkButton ID="linkAllNew" runat="server" OnClick="linkAllNew_Click" Style="color: #65a0d0">
                                                        Create a New Ticket
                                            </asp:LinkButton>
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="linkAllPending" runat="server" OnClick="linkAllPending_Click" Style="color: #65a0d0">
                                                <span class="badge badge-danger">
                                                    <asp:Label ID="leblANew1" runat="server"></asp:Label>
                                                </span>Pending
                                            </asp:LinkButton>
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="linkAllProgress" runat="server" OnClick="linkAllProgress_Click" Style="color: #65a0d0">
                                                <span class="badge badge-success">
                                                    <asp:Label ID="leblANew3" runat="server"></asp:Label>
                                                </span>InProgress 
                                            </asp:LinkButton>
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="linkAllClose" runat="server" OnClick="linkAllClose_Click" Style="color: #65a0d0">
                                                <span class="badge badge-warning">
                                                    <asp:Label ID="leblANew4" runat="server"></asp:Label></span>Closed
                                            </asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="portlet light bordered">
                            <div class="portlet-title">
                                <asp:LinkButton ID="LinkButton1" runat="server" class="caption-subject font-green-sharp bold uppercase">Search</asp:LinkButton>
                            </div>
                            <div class="portlet-body todo-project-list-content">
                                <div class="todo-project-list">
                                    <asp:DropDownList ID="drpyears" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="drpyears_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <br />
                                <div class="todo-project-list">
                                    <asp:DropDownList ID="drpmonths" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <br />
                                <div class="todo-project-list">
                                    <asp:DropDownList ID="drpusers" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <br />
                                <div class="todo-project-list">
                                    <asp:TextBox ID="txtpatientnames" runat="server" CssClass="form-control" placeholder="Patient Name"></asp:TextBox>
                                </div>
                                <br />
                                <div class="todo-project-list">
                                    <asp:TextBox ID="txtcnose" runat="server" CssClass="form-control" placeholder="Complaint No"></asp:TextBox>
                                </div>
                                <br />
                                <asp:Button ID="btnoksearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnoksearch_Click" />
                                <asp:Button ID="btncount" runat="server" CssClass="btn btn-info" Visible="false" />
                            </div>

                        </div>


                        <div class="portlet light bordered">
                            <div class="portlet-title">
                                <asp:LinkButton ID="linkreopen" runat="server" class="caption-subject font-green-sharp bold uppercase" Visible="false" OnClick="linkreopen_Click">Re Open</asp:LinkButton>
                            </div>
                            <div class="portlet-body todo-project-list-content">
                                <div class="todo-project-list">
                                    <asp:Label ID="lblticket" runat="server" Text="Master Code" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtticket" runat="server" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lblcomplain" runat="server" Text="Complaint Number" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtcomplain" runat="server" Visible="false"></asp:TextBox>
                                    <asp:Button ID="btnsearch" runat="server" Text="Search" Visible="false" OnClick="btnsearch_Click" />
                                </div>
                            </div>
                        </div>

                        <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                    <!-- END TODO SIDEBAR -->
                    <!-- BEGIN TODO CONTENT -->
                    <div class="todo-content">
                        <div class="portlet light bordered">

                            <div class="portlet-body">
                                <div class="row">
                                    <div class="col-md-5 col-sm-4">
                                        <div class="scroller" style="max-height: 1500px;" data-always-visible="0" data-rail-visible="0" data-handle-color="#dae3e7">
                                            <asp:Panel ID="PnlBindTick" runat="server">
                                                <div class="todo-tasklist">
                                                    <asp:UpdatePanel runat="server" ID="upnl" class="item">
                                                        <ContentTemplate>
                                                            <asp:ListView ID="ltsRemainderNotes" runat="server" OnItemCommand="ltsRemainderNotes_ItemCommand" OnItemDataBound="ltsRemainderNotes_ItemDataBound" ItemPlaceholderID="itemPlaceHolder1" GroupPlaceholderID="groupPlaceHolder1" OnPagePropertiesChanging="ltsRemainderNotes_PagePropertiesChanging">
                                                                <LayoutTemplate>

                                                                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ltsRemainderNotes" PageSize="5">
                                                                        <Fields>
                                                                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                                                ShowNextPageButton="false" />
                                                                            <asp:NumericPagerField ButtonType="Link" />
                                                                            <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                                        </Fields>
                                                                    </asp:DataPager>
                                                                </LayoutTemplate>
                                                                <GroupTemplate>
                                                                    <tr>
                                                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                                    </tr>
                                                                </GroupTemplate>
                                                                <ItemTemplate>
                                                                    <div class="todo-tasklist-item todo-tasklist-item-border-green" style="border-left: #3faba4 3px solid;">
                                                                        <%--<img class="todo-userpic pull-left" src="<%#Eval("USERCODE")%>" width="27px" height="27px">--%>
                                                                        <img class="todo-userpic pull-left" src="../CRM/images/No_image.png" width="27px" height="27px">
                                                                        <div class="todo-tasklist-item-title">
                                                                            <asp:LinkButton ID="btnnames" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btnnames" runat="server">
                                                                                <%# GetUName(Convert.ToInt32(Eval("ReportedBy"))) %></asp:LinkButton>
                                                                            <asp:LinkButton ID="btnclick123" class="btn btn-sm blue" Style="padding: 0px 2px 0px 2px; font-weight: bold; height: 20px; width: 52px;" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btnclick123" runat="server"> &nbsp; Reply &nbsp; </asp:LinkButton>
                                                                            <asp:LinkButton ID="btneditticket" class="btn btn-sm blue" Style="padding: 0px 2px 0px 2px; font-weight: bold; height: 20px; width: 34px;" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btneditticket" runat="server"> Edit </asp:LinkButton>
                                                                            <asp:LinkButton ID="btnticketes" class="todo-tasklist-badge badge badge-roundless" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btnticketes" runat="server" Style="height: 20px; width: 70px;"><%#Eval("MasterCODE")%></asp:LinkButton>
                                                                            <a href='<%# "/POS/ViewTicket.aspx?Mastercode="+ Eval("MasterCODE")%>' class="btn btn-sm red" style="height: 20px; width: 43px; margin-left: 0px; margin-top: 1px; border-top-width: -15px; padding-top: 0px;" target="_blank">View</a>

                                                                            <asp:LinkButton ID="LinkButton2" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btnpendings" runat="server"> 
                                                                                <span runat="server" class="btn btn-sm blue" style="height: 20px;padding-top: 0px;" Visible='<%# (int)Eval("UseReciepeID") == 1%>'>                                                                                 
                                                                                Log
                                                                            </span></asp:LinkButton>

                                                                            <%--<span class="todo-tasklist-badge badge badge-roundless"><%#Eval("MasterCODE")%> </span>--%>
                                                                        </div>
                                                                        <div class="todo-tasklist-item-text">Follow Up By:- <%#GetEmplName(Convert.ToInt32(Eval("FoloEmp")))%></div>
                                                                        <asp:LinkButton ID="btnremarks" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btnremarks" runat="server">
                                                                        <div class="todo-tasklist-item-text"><%#Eval("Remarks")%> </div></asp:LinkButton>
                                                                        <asp:Label ID="tikitID" Visible="false" runat="server" Text='<%#Eval("MasterCODE")%>'></asp:Label>
                                                                        <asp:Label ID="Label11" Visible="false" runat="server" Text='<%#Eval("MyID")%>'></asp:Label>
                                                                        <asp:Label ID="Label12" Visible="false" runat="server" Text='<%#Eval("TenentID")%>'></asp:Label>
                                                                        <asp:Label ID="lblremidernotes" Visible="false" runat="server" Text='<%#Eval("REMINDERNOTE ")%>'></asp:Label>
                                                                        <asp:Label ID="lblstatus" Visible="false" runat="server" Text='<%#Eval("MyStatus")%>'></asp:Label>
                                                                        <div class="todo-tasklist-controls pull-left">
                                                                            <asp:LinkButton ID="btndates" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btndates" runat="server"> <span class="todo-tasklist-date">
                                                                                <i class="fa fa-calendar"></i><%# Convert.ToDateTime(Eval("UploadDate")).ToString("dd-MMM-yyyy hh:mm tt")%></span></asp:LinkButton>
                                                                            <asp:LinkButton ID="btnpendings" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btnpendings" runat="server"> 
                                                                                <span runat="server" class="btn btn-sm green" style="height: 20px;padding-top: 0px;" Visible='<%# (string)Eval("MyStatus") == "Pending"%>'>                                                                                 
                                                                                Pending
                                                                            </span></asp:LinkButton>
                                                                            <asp:LinkButton ID="btninprogress" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btninprogress" runat="server"> 
                                                                                  <span runat="server" class="btn btn-sm green" style="height: 20px;padding-top: 0px;" Visible='<%# (string)Eval("MyStatus") == "InProgress"%>'>                                                                                 
                                                                                InProgress
                                                                            </span></asp:LinkButton>
                                                                            <asp:LinkButton ID="btnclosed" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btnclosed" runat="server">   <span runat="server" class="btn btn-sm red" style="height: 20px;padding-top: 0px;" Visible='<%# (string)Eval("MyStatus") == "Closed"%>'>                                                                              
                                                                                Closed
                                                                            </span></asp:LinkButton>
                                                                            <span class="todo-tasklist-badge badge badge-roundless">Ticket</span> <%--<%#Eval("ACTIVITYE")%>--%>
                                                                            <asp:Label ID="Label23" runat="server" Text='<%#Eval("UseReciepeName")%>'></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
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
                                               <ul class="nav nav-tabs" id="profileTabs" role="tablist">
                                                <li class="nav-item" role="presentation">
                                                    <button class="nav-link active" id="education-tab" data-bs-toggle="tab" data-bs-target="#education" type="button" role="tab">COMPLAINT DATA</button>
                                                </li>
                                                <li class="nav-item" role="presentation">
                                                    <button class="nav-link" id="experience-tab" data-bs-toggle="tab" data-bs-target="#experience" type="button" role="tab">PERSONAL DATA</button>
                                                </li>
                                              
                                                    <li class="nav-item" role="presentation">
                                                      <button class="nav-link" id="skills-tab" data-bs-toggle="tab" data-bs-target="#skills" type="button" role="tab">INVESTIGATION DATA</button>
                                                      </li>
                                                 </ul>
                        

                                                    <div class="form">
  <div class="tab-content p-3 border border-top-0" id="profileTabsContent">
    <!-- complaint Tab -->
    <div class="tab-pane  show active" id="education" role="tabpanel" aria-labelledby="education-tab">
       
         <div class="form-group">
      <div class="col-md-8 col-sm-8" style="margin-bottom: 15px;">
          <div class="todo-taskbody-user">
          </div>
      </div>
      <div class="col-md-4 col-sm-4" style="margin-bottom: 15px;">
      </div>
  </div>
  <!-- END TASK HEAD -->
  <!-- TASK TITLE -->
  <div class="form-group">
      <div class="col-md-4" style="margin-bottom: 15px;">
          <b>
              <asp:Label ID="Label7" runat="server" Text="Complaint No.:"></asp:Label></b>
      </div>
      <div class="col-md-4" style="margin-bottom: 15px;">
          <asp:Label ID="lblcomplainno" runat="server"></asp:Label>
      </div>
      <div class="col-md-2" style="margin-bottom: 15px;">
          <b>
              <asp:Label ID="Label24" runat="server" Text="Log Only:"></asp:Label></b>
      </div>
      <div class="col-md-1" style="margin-bottom: 15px;">
          <asp:CheckBox ID="chklog" runat="server" AutoPostBack="true" />
      </div>
  </div>
  <div class="form-group">
      <div class="col-md-4" style="margin-bottom: 15px;">
          <asp:Label ID="Label1" runat="server" Text="Creation Date:"></asp:Label>
      </div>
      <div class="col-md-8" style="margin-bottom: 15px;">
          <asp:Label ID="lbldatesd" runat="server"></asp:Label>
      </div>
  </div>
  <div class="form-group">
      <div class="col-md-4" style="margin-bottom: 15px;">
          <b>
              <asp:Label ID="lbldate" runat="server" Text="Complaint Date:"></asp:Label></b>
      </div>
      <div class="col-md-8" style="margin-bottom: 15px;">
          <asp:TextBox ID="txtdates" runat="server" CssClass="form-control" OnTextChanged="txtdates_TextChanged1"></asp:TextBox>
          <asp:Label ID="lblmsgl" runat="server" Visible="false" Style="color: red;"></asp:Label>
          <cc1:CalendarExtender ID="CalendarExtendertxtdateTO" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdates" Format="dd/MMM/yyyy"></cc1:CalendarExtender>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtdates" ErrorMessage="Date required" InitialValue="0"></asp:RequiredFieldValidator>

      </div>
  </div>
  <div class="form-group">
      <div class="col-md-4" style="margin-bottom: 15px;">
          <b>
              <asp:Label ID="Label5" runat="server" Text="Complaint Type :"></asp:Label></b>
      </div>
      <div class="col-md-8" style="margin-bottom: 15px;">
          <asp:DropDownList ID="drpComplainType" runat="server" class="form-control select2me" AutoPostBack="true">
          </asp:DropDownList>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpComplainType" ErrorMessage="Complain Type required" InitialValue="0"></asp:RequiredFieldValidator>
      </div>
  </div>

  <div class="form-group">
      <div class="col-md-4" style="margin-bottom: 15px;">
          <b>
              <asp:Label ID="Label4" runat="server" Text="Department:"></asp:Label></b>
      </div>
      <div class="col-md-8" style="margin-bottom: 15px;">
          <asp:DropDownList ID="drpSDepartment" runat="server" class="form-control select2me" OnSelectedIndexChanged="drpSDepartment_SelectedIndexChanged" AutoPostBack="true">
          </asp:DropDownList>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpSDepartment" ErrorMessage="Department required" InitialValue="0"></asp:RequiredFieldValidator>
      </div>
  </div>
  <div class="form-group">
      <div class="col-md-4" style="margin-bottom: 15px;">
          <b>
              <asp:Label ID="Label8" runat="server" Text="Physical Location :"></asp:Label></b>
      </div>
      <div class="col-md-8" style="margin-bottom: 15px;">
          <asp:DropDownList ID="DrpPhysicalLocation" runat="server" class="form-control select2me" AutoPostBack="true">
          </asp:DropDownList>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="DrpPhysicalLocation" ErrorMessage="Physical Location required" InitialValue="0"></asp:RequiredFieldValidator>
      </div>
  </div>
  <div class="form-group">
      <div class="col-md-3" style="margin-bottom: 15px;">
          <b>
              <asp:Label ID="Label15" runat="server" Text="Staff Involved :"></asp:Label></b>
      </div>

      <div class="col-md-1" style="margin-bottom: 15px;">
          <asp:CheckBox ID="chklist" runat="server" OnCheckedChanged="chklist_CheckedChanged" AutoPostBack="true" />

      </div>

      <div class="col-md-3" style="margin-bottom: 15px;">
          <b>
              <asp:Label ID="Label19" runat="server" Text="Inc Rep"></asp:Label></b>
      </div>

      <div class="col-md-1" style="margin-bottom: 15px;">
          <asp:CheckBox ID="chkinci" runat="server" />

      </div>
      <div class="col-md-3" style="margin-bottom: 15px;">
          <b>
              <asp:Label ID="Label16" runat="server" Text="IR Done :"></asp:Label></b>
      </div>

      <div class="col-md-1" style="margin-bottom: 15px;">
          <asp:CheckBox ID="chkirno" runat="server" />

      </div>
      <div class="col-md-6" style="margin-bottom: 15px;">
          <asp:TextBox ID="txtstaffname" runat="server" CssClass="form-control" Visible="false" placeholder="Staff Name"></asp:TextBox>
      </div>
  </div>
    </div>

    <!-- Personal Tab -->
    <div class="tab-pane" id="experience" role="tabpanel" aria-labelledby="experience-tab">
       
                                                       <div class="form-group">
                                                           <div class="col-md-4" style="margin-bottom: 15px;">
                                                               <b>
                                                                   <asp:Label ID="Label13" runat="server" Text="Patient Name :"></asp:Label></b>
                                                           </div>

                                                           <div class="col-md-8" style="margin-bottom: 15px;">
                                                               <asp:TextBox ID="txtpatientname" runat="server" CssClass="form-control"></asp:TextBox>
                                                               <asp:RequiredFieldValidator ID="required3" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtpatientname" ErrorMessage="Please add Patient Name"></asp:RequiredFieldValidator>
                                                           </div>
                                                       </div>
                                                       <div class="form-group">
                                                           <div class="col-md-4" style="margin-bottom: 15px;">
                                                               <b>
                                                                   <asp:Label ID="Label14" runat="server" Text="MRN :"></asp:Label></b>
                                                           </div>
                                                           <div class="col-md-8" style="margin-bottom: 15px;">
                                                               <asp:TextBox ID="txtMRN" runat="server" CssClass="form-control"></asp:TextBox>
                                                               <%--<asp:RequiredFieldValidator ID="required1" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtMRN" ErrorMessage="Please add MRN"></asp:RequiredFieldValidator>--%>
                                                           </div>
                                                       </div>
                                                       <div class="form-group">
    <div class="col-md-4" style="margin-bottom: 15px;">
        <b>
            <asp:Label ID="Label2" runat="server" Text="Subject :"></asp:Label></b>
    </div>
    <div class="col-md-8" style="margin-bottom: 15px;">
        <asp:TextBox ID="txtSubject" runat="server" MaxLength="50" placeholder="Wirte The Subject" class="form-control todo-taskbody-due">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtSubject" ErrorMessage="Subject required"></asp:RequiredFieldValidator>
    </div>
</div>
                                                       <div class="form-group">
     <div class="col-md-4" style="margin-bottom: 15px;">
         <b>
             <asp:Label ID="Label3" runat="server" Text=" Complaint Details : "></asp:Label></b>
     </div>
     <div class="col-md-8" style="margin-bottom: 15px;">
         <asp:TextBox ID="txtMessage" runat="server" placeholder="Wirte The Messaging" MaxLength="1000"  TextMode="MultiLine" class="form-control" Rows="4"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtMessage" ErrorMessage="Message required"></asp:RequiredFieldValidator>
         <%--<textarea class="form-control todo-taskbody-taskdesc" rows="8" placeholder="Task Description..."></textarea>--%>
     </div>
 </div>
    </div>

 <!-- INVESTIGATION DATA Tab -->
    <div class="tab-pane" id="skills" role="tabpanel" aria-labelledby="skills-tab">
     
 <div class="form-group">
     <div class="col-md-4" style="margin-bottom: 15px;">
         <b>
             <asp:Label ID="Label18" runat="server" Text="Contact No :"></asp:Label></b>
     </div>
     <div class="col-md-8" style="margin-bottom: 15px;">
         <asp:TextBox ID="txtcontact" runat="server" CssClass="form-control"></asp:TextBox>
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
 </div>
 <div class="form-group">
     <div class="col-md-4" style="margin-bottom: 15px;">
         <b>
             <asp:Label ID="Label9" runat="server" Text="Category :"></asp:Label></b>
     </div>
     <div class="col-md-8" style="margin-bottom: 15px;">
         <asp:DropDownList ID="DrpTCatSubCate" runat="server" CssClass="form-control select2me" OnSelectedIndexChanged="DrpTCatSubCate_SelectedIndexChanged" AutoPostBack="true">
         </asp:DropDownList>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="DrpTCatSubCate" ErrorMessage="Category required" InitialValue="0"></asp:RequiredFieldValidator>
     </div>
 </div>
 <div class="form-group">
     <div class="col-md-4" style="margin-bottom: 15px;">
         <b>
             <asp:Label ID="Label10" runat="server" Text="Sub-Category :"></asp:Label></b>
     </div>
     <div class="col-md-8" style="margin-bottom: 15px;">
         <asp:DropDownList ID="DrpSubCat" runat="server" CssClass="form-control select2me">
         </asp:DropDownList>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="DrpSubCat" ErrorMessage="Sub-Category required" InitialValue="0"></asp:RequiredFieldValidator>
     </div>
 </div>

                                 
 <div class="form-group">
     <div class="col-md-4" style="margin-bottom: 15px;">
         <b>
             <asp:Label ID="Label22" runat="server" Text="Investigation Result :"></asp:Label></b>
     </div>
     <div class="col-md-8" style="margin-bottom: 15px;">
         <asp:DropDownList ID="drpinvestresult" runat="server" AutoPostBack="true" CssClass="form-control">
             <asp:ListItem Value="1" Text="Releveant"></asp:ListItem>
             <asp:ListItem Value="2" Text="Irrelevant"></asp:ListItem>
         </asp:DropDownList>
     </div>
 </div>

 <div class="form-group">
     <div class="col-md-4" style="margin-bottom: 15px;">
         <b>
             <asp:Label ID="Label17" runat="server" Text="Reported by :"></asp:Label></b>
     </div>
     <div class="col-md-8" style="margin-bottom: 15px;">
         <asp:DropDownList ID="drpusermt" runat="server" class="form-control select2me" AutoPostBack="true">
         </asp:DropDownList>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpusermt" ErrorMessage="reported by required" InitialValue="0"></asp:RequiredFieldValidator>
     </div>
 </div>
 <div class="form-group">
     <div class="col-md-4" style="margin-bottom: 15px;">
         <b>
             <asp:Label ID="Label21" runat="server" Text="Follow Up By:"></asp:Label></b>
     </div>
     <div class="col-md-8" style="margin-bottom: 15px;">
         <asp:DropDownList ID="drpfoloemp" runat="server" class="form-control select2me" AutoPostBack="true">
         </asp:DropDownList>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpfoloemp" ErrorMessage="Employee required" InitialValue="0"></asp:RequiredFieldValidator>
     </div>
 </div>                           
    </div>
</div>
                                                      
                          <!-- Hidden Field to store tab -->
<asp:HiddenField ID="hfActiveTab" runat="server" />                           

                                                       
                                                        <div class="form-actions right todo-form-actions">
                                                            <asp:Button ID="btnattache" runat="server" class="btn btn-circle btn-sm green" OnClick="btnattache_Click" Text="Attachments" />
                                                            <asp:Button ID="btnSave" runat="server" class="btn btn-circle btn-sm green" OnClick="btnSave_Click" Text="Submit" ValidationGroup="Ticks" />
                                                            <asp:Button ID="btnCancel" runat="server" class="btn btn-circle btn-sm btn-default" OnClick="btnCancel_Click" Text="Cancel" />
                                                            <asp:Button ID="btncack" runat="server" class="btn btn-circle btn-sm btn-default" OnClick="btncack_Click" Text="Back" Visible="false" />
                                                            <asp:Button ID="btndelete" runat="server" class="btn btn-circle btn-sm btn-default" OnClick="btndelete_Click" Text="delete" Visible="false" />
                                                            <asp:Button ID="btncloseandupdate" runat="server" class="btn btn-circle btn-sm btn-default" Style="background-color: red; color: white;" OnClick="btncloseandupdate_Click" Text="Close Tkt & Save" />
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlinfo" runat="server">
                                                    <div class="todo-tasklist-controls pull-left">
                                                        <span class="todo-tasklist-badge badge" style="margin-bottom: 3px;">Department-->
                                                            <asp:Label ID="lblinfoDepartment" runat="server" Text=""></asp:Label></span>
                                                        <span class="todo-tasklist-badge badge" style="margin-bottom: 3px;">Physical Location-->
                                                            <asp:Label ID="lblinfoLocation" runat="server" Text=""></asp:Label></span>
                                                        <span class="todo-tasklist-badge badge" style="margin-bottom: 3px;">Complain-->
                                                            <asp:Label ID="lblinfocomplaintype" runat="server" Text=""></asp:Label></span>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="panChat" runat="server" DefaultButton="btnSubmit">
                                                    <asp:Timer ID="Timer1" runat="server" Interval="18000" OnTick="Timer1_Tick">
                                                    </asp:Timer>
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" class="item" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="tabbable-line" style="padding-left: 10px;">
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

                                                                                <asp:ListView ID="listChet" runat="server" OnItemCommand="listChet_ItemCommand">

                                                                                    <ItemTemplate>
                                                                                        <ul class="media-list">
                                                                                            <li class="media">
                                                                                                <a class="pull-left" href="javascript:;">
                                                                                                    <%-- <img class="todo-userpic" src="<%# Eval("USERCODE")%>" width="27px" height="27px">--%>
                                                                                                    <img class="todo-userpic" src="../CRM/images/No_image.png" width="27px" height="27px">
                                                                                                </a>
                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;<div class="media-body todo-comment">
                                                                                                    <%-- <button type="button" class="todo-comment-btn btn btn-circle btn-default btn-sm">&nbsp; Reply &nbsp;</button>--%>
                                                                                                    <p class="todo-comment-head">
                                                                                                        <span class="todo-comment-username"><%#Eval("Version")%></span> &nbsp; - 
                                                                                                        <span class="todo-comment-username"><%#Eval("MasterCODE ")%></span> &nbsp;
                                                                                            <span class="todo-comment-date"><%# Convert.ToDateTime(Eval("InitialDate")).ToString("dd-MMM-yyyy hh:mm tt")%></span> On
                                                                                                        <span class="todo-comment-date"><%# Convert.ToDateTime(Eval("UploadDate")).ToString("dd-MMM-yyyy hh:mm tt")%></span>
                                                                                                    </p>

                                                                                                    <p class="todo-text-color">
                                                                                                        <%#Eval("MyStatus")%>
                                                                                                    </p>
                                                                                                    <p>
                                                                                                        <asp:Label ID="lblcompid" runat="server" Text='<%# Eval("COMPID")%>' Visible="false"></asp:Label>
                                                                                                        <asp:Label ID="lblmastercode" runat="server" Text='<%# Eval("MasterCODE")%>' Visible="false"></asp:Label>
                                                                                                        <asp:Label ID="lblMyLineNo" runat="server" Text='<%# Eval("MyLineNo")%>' Visible="false"></asp:Label>
                                                                                                        <asp:Label ID="lblLocationID" runat="server" Text='<%# Eval("LocationID")%>' Visible="false"></asp:Label>
                                                                                                        <asp:Label ID="lblLinkMasterCODE" runat="server" Text='<%# Eval("LinkMasterCODE")%>' Visible="false"></asp:Label>
                                                                                                        <asp:Label ID="lblActivityID" runat="server" Text='<%# Eval("ActivityID")%>' Visible="false"></asp:Label>
                                                                                                        <asp:Label ID="lblPrefix" runat="server" Text='<%# Eval("Prefix")%>' Visible="false"></asp:Label>
                                                                                                        <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("MasterCODE")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>
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

                                                                    </div>
                                                                    <div class="tab-pane" id="tab_2">
                                                                        <asp:ListView ID="ListHistoy" runat="server">
                                                                            <ItemTemplate>
                                                                                <ul class="todo-task-history">
                                                                                    <li>
                                                                                        <div class="todo-task-history-date"><%# Convert.ToDateTime(Eval("UploadDate")).ToString("dd-MMM-yyyy hh:mm tt")%> </div>
                                                                                        <div class="todo-task-history-desc"><%#Eval("Version")%> </div>
                                                                                    </li>
                                                                                </ul>
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
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <ul class="media-list">
                                                                <li class="media">
                                                                    <%--<a class="pull-left" href="javascript:;">
                                                                                        <img class="todo-userpic" src="assets/pages/media/users/avatar4.jpg" width="27px" height="27px">
                                                                                    </a>--%>
                                                                    <asp:DropDownList ID="aspcomment" runat="server" OnSelectedIndexChanged="aspcomment_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                                                        <asp:ListItem Value="0" Text="---Select Action---"></asp:ListItem>
                                                                        <asp:ListItem Value="2101401" Text="Immediate action"></asp:ListItem>
                                                                        <asp:ListItem Value="2101402" Text="Follow-up"></asp:ListItem>
                                                                        <asp:ListItem Value="2101403" Text="Corrective action"></asp:ListItem>
                                                                        <asp:ListItem Value="2101404" Text="Investigation results"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <asp:DropDownList ID="drpinvestigation" runat="server" OnSelectedIndexChanged="drpinvestigation_SelectedIndexChanged1" Visible="false" AutoPostBack="true" CssClass="form-control">
                                                                        <asp:ListItem Value="1" Text="Releveant"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="Irrelevant"></asp:ListItem>
                                                                    </asp:DropDownList>

                                                                    <div class="media-body">
                                                                        <asp:TextBox ID="txtComent" runat="server" placeholder="Type comment..." MaxLength="1000" Rows="4" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group" style="padding-top: 20px; margin-right: 0px;">
                                                                        <div class="col-md-4" style="margin-bottom: 15px; padding-left: 20px;">
                                                                            <b>
                                                                                <asp:Label ID="Label6" runat="server" Text="Status:"></asp:Label></b>
                                                                        </div>

                                                                        <div class="col-md-8" style="margin-bottom: 15px;">

                                                                            <asp:DropDownList ID="drpStatus" runat="server" CssClass="form-control">

                                                                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                                                <asp:ListItem Value="Closed">Closed</asp:ListItem>
                                                                                <asp:ListItem Value="InProgress">InProgress</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </li>
                                                            </ul>
                                                            <%-- <div class="form-group">
                                                                <div class="col-md-4" style="margin-bottom: 15px; padding-left: 20px;">
                                                                    <b>
                                                                        <asp:Label ID="lblrate" runat="server" Text="Ratting:"></asp:Label></b>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <cc1:Rating ID="Rating1" AutoPostBack="true" runat="server"
                                                                        StarCssClass="Star" WaitingStarCssClass="WaitingStar" OnChanged="Rating1_Changed" EmptyStarCssClass="Star"
                                                                        FilledStarCssClass="FilledStar">
                                                                    </cc1:Rating>
                                                                     <asp:Label ID="lblRatingStatus" runat="server" Text="5" Style="margin-left: 10px;" Visible="false"></asp:Label>
                                                                </div>
                                                                


                                                            </div>--%>
                                                            <div class="form-group">
                                                                <div class="col-md-4" style="margin-bottom: 15px; padding-left: 20px;">
                                                                    <b>
                                                                        <asp:Label ID="Label20" runat="server" Visible="false"></asp:Label></b>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <cc1:Rating ID="Rating1" AutoPostBack="true" runat="server" Visible="false"
                                                                        StarCssClass="Star" WaitingStarCssClass="WaitingStar" OnChanged="Rating1_Changed" EmptyStarCssClass="Star"
                                                                        FilledStarCssClass="FilledStar">
                                                                    </cc1:Rating>
                                                                    <asp:Label ID="lblRatingStatus" runat="server" Text="5" Style="margin-left: 10px;" Visible="false"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="form">
                                                                <div class="form-actions right todo-form-actions">
                                                                    <asp:LinkButton ID="lbllblattachments2" class="btn btn-circle btn-sm green" runat="server" Text="Attachments" OnClick="lblattachments_Click"></asp:LinkButton>
                                                                    <%--<span class="badge badge-success">
                                                                                        <asp:Label ID="lblAttach2" runat="server" Text="0"></asp:Label></span>--%>
                                                                    <asp:Button ID="btnAttch" runat="server" Text="0" CssClass="btn btn-sm btn-circle green" OnClick="btnAttch_Click" />
                                                                    <asp:Button ID="btnSubmit" ValidationGroup="" Visible="false" runat="server" class="btn btn-sm btn-circle green" OnClick="btnSubmit_Click" Text="Submit" />
                                                                    <asp:Button ID="btnTikitClose" runat="server" class="pull-right btn btn-circle btn-sm btn-default" OnClick="btnTikitClose_Click" Text="Back" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <%-- Chat Group --%>
                                                <%--<asp:Panel ID="pnlGroupChat" runat="server">
                                                <div class="form-group">
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="padding-top: 20px; margin-right: 0px;">
                                                            <div class="col-md-4" style="margin-bottom: 15px; padding-left: 20px;">
                                                                <b>
                                                                    <asp:Label ID="Label8" runat="server" Text="Person:"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-8" style="margin-bottom: 15px;">
                                                                <asp:DropDownList ID="drpSingleChat" runat="server" class="form-control todo-taskbody-tags">                                                                   
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="padding-top: 20px; margin-right: 0px;">
                                                            <div class="col-md-4" style="margin-bottom: 15px; padding-left: 20px;">
                                                                <b>
                                                                    <asp:Label ID="Label9" runat="server" Text="Group:"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-8" style="margin-bottom: 15px;">
                                                                <asp:DropDownList ID="drpGroupchat" runat="server" class="form-control todo-taskbody-tags">                                                                   
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="portlet light bordered">
                                                        <div class="portlet-title">
                                                            <div class="caption">
                                                                <i class="icon-bubble font-hide hide"></i>
                                                                <span class="caption-subject font-hide bold uppercase">Chats</span>
                                                            </div>
                                                           
                                                        </div>
                                                        <div class="portlet-body" id="chats">
                                                            <div class="scroller" style="height: 525px;" data-always-visible="1" data-rail-visible1="1">
                                                                <ul class="chats">
                                                                    <li class="out">
                                                                        <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar2.jpg" />
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Lisa Wong </a>
                                                                            <span class="datetime">at 20:11 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="out">
                                                                        <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar2.jpg" />
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Lisa Wong </a>
                                                                            <span class="datetime">at 20:11 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="in">
                                                                        <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar1.jpg" />
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Bob Nilson </a>
                                                                            <span class="datetime">at 20:30 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="in">
                                                                        <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar1.jpg" />
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Bob Nilson </a>
                                                                            <span class="datetime">at 20:30 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="out">
                                                                        <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar3.jpg" />
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Richard Doe </a>
                                                                            <span class="datetime">at 20:33 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="in">
                                                                        <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar3.jpg" />
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Richard Doe </a>
                                                                            <span class="datetime">at 20:35 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="out">
                                                                        <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar1.jpg" />
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Bob Nilson </a>
                                                                            <span class="datetime">at 20:40 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="in">
                                                                        <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar3.jpg" />
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Richard Doe </a>
                                                                            <span class="datetime">at 20:40 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="out">
                                                                        <img class="avatar" alt="" src="../assets/layouts/layout/img/avatar1.jpg" />
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Bob Nilson </a>
                                                                            <span class="datetime">at 20:54 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. sed diam nonummy nibh euismod tincidunt ut laoreet. </span>
                                                                        </div>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="chat-form">
                                                                <div class="input-cont">
                                                                    <input class="form-control" type="text" placeholder="Type a message here..." />
                                                                </div>
                                                                <div class="btn-cont">
                                                                    <span class="arrow"></span>
                                                                    <a href="" class="btn blue icn-only">
                                                                        <i class="fa fa-check icon-white"></i>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </asp:Panel>--%>
                                                <%--<div class="form-group">
                                                    <div class="portlet light bordered">
                                                        <div class="portlet-title">
                                                            <div class="caption">
                                                                <i class="icon-bubble font-hide hide"></i>
                                                                <span class="caption-subject font-hide bold uppercase">Chats</span>
                                                            </div>
                                                           
                                                        </div>
                                                        <div class="portlet-body" id="chats">
                                                            <div class="scroller" style="height: 525px;" data-always-visible="1" data-rail-visible1="1">
                                                                <ul class="chats">
                                                                    <li class="out">
                                                                        
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Lisa Wong </a>
                                                                            <span class="datetime">at 20:11 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="out">
                                                                        
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Lisa Wong </a>
                                                                            <span class="datetime">at 20:11 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="in">
                                                                        
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Bob Nilson </a>
                                                                            <span class="datetime">at 20:30 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="in">
                                                                        
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Bob Nilson </a>
                                                                            <span class="datetime">at 20:30 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="out">
                                                                        
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Richard Doe </a>
                                                                            <span class="datetime">at 20:33 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="in">
                                                                        
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Richard Doe </a>
                                                                            <span class="datetime">at 20:35 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="out">
                                                                        
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Bob Nilson </a>
                                                                            <span class="datetime">at 20:40 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="in">

                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Richard Doe </a>
                                                                            <span class="datetime">at 20:40 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. </span>
                                                                        </div>
                                                                    </li>
                                                                    <li class="out">
                                                                        
                                                                        <div class="message">
                                                                            <span class="arrow"></span>
                                                                            <a href="javascript:;" class="name">Bob Nilson </a>
                                                                            <span class="datetime">at 20:54 </span>
                                                                            <span class="body">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. sed diam nonummy nibh euismod tincidunt ut laoreet. </span>
                                                                        </div>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="chat-form">
                                                                <div class="input-cont">
                                                                    <input class="form-control" type="text" placeholder="Type a message here..." />
                                                                </div>
                                                                <div class="btn-cont">
                                                                    <span class="arrow"></span>
                                                                    <a href="" class="btn blue icn-only">
                                                                        <i class="fa fa-check icon-white"></i>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <!-- END TODO CONTENT -->
                    <%-- <div class="todo-content">
                        <div class="portlet light bordered">
                           
                            <div class="portlet-body">
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="todo-tasklist">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" class="item">
                                                <ContentTemplate>

                                                    <asp:ListView ID="ListGroupChat" runat="server">
                                                        <ItemTemplate>
                                                            <div class="todo-tasklist-item todo-tasklist-item-border-green">
                                                                <img class="todo-userpic pull-left" src="../CRM/images/No_image.png" width="27px" height="27px">
                                                                <div class="todo-tasklist-item-title">
                                                                    <%# GetUName(Convert.ToInt32(Eval("USERCODE"))) %>
                                                                    <asp:LinkButton ID="btnclick123" class="btn blue" Style="margin-left: 50px;" CommandArgument='<%#Eval("MasterCODE")%>' CommandName="btnclick123" runat="server"> &nbsp; Reply &nbsp; </asp:LinkButton>
                                                                    <span class="todo-tasklist-badge badge badge-roundless">Ti.No <%#Eval("MasterCODE")%> </span>
                                                                </div>
                                                                <div class="todo-tasklist-item-text"><%#Eval("Remarks")%> </div>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="todo-tasklist-devider"></div>
                                    <div class="col-md-7">
                                        <div class="form-horizontal">
                                          
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>

        </div>

    </div>
    <%--    <a href="#small2" data-toggle="modal">Link</a>--%>

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
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
