<%@ Page Title="" Language="C#" MasterPageFile="~/POS/POSMaster.Master" AutoEventWireup="true" CodeBehind="ViewTick.aspx.cs" Inherits="Web.POS.ViewTick" %>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="background-color: #e9ecf3">  
    <div class="page-head">
            <!-- BEGIN PAGE TITLE -->
            <div class="page-title">
                <br />
                <br />
               <%-- Complaint 1234567 View Only Dated dd-mmm-yyyy MRN #####--%>
                <span class="todo-comment-username">&nbsp;&nbsp;<asp:Label ID="lblUser" runat="server"></asp:Label><br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton2" runat="server" class="btn btn-sm yellow filter-submit margin-bottom" Visible="false">Print</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3" runat="server" class="btn btn-sm blue filter-submit margin-bottom" Visible="false">Email</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton4" runat="server" class="btn btn-sm blue filter-submit margin-bottom" OnClick="lnkback_Click">Back</asp:LinkButton>
                    <small></small>
               </span>
                <br />
                <%--   <asp:Button ID="btn2015" runat="server" Text="2015" OnClick="btn2015_Click"/>
                 <asp:Button ID="btnsub2" runat="server" Text="2016" OnClick="btnsub2_Click"/>
                <asp:Button ID="btnsub3" runat="server" Text="2017" OnClick="btnsub3_Click"/>
                <asp:Button ID="btnsub4" runat="server" Text="2018" OnClick="btnsub4_Click"/>
                <asp:Button ID="btnsub5" runat="server" Text="2019" OnClick="btnsub5_Click"/>--%>
                <asp:DropDownList ID="drpyear1" runat="server" CssClass="form-control" AutoPostBack="true" Visible="false"></asp:DropDownList>
            </div>

        </div>
         </div>

   <div class="row">
            <div class="col-md-12">
                <!-- BEGIN TODO SIDEBAR -->
                <div class="todo-ui">
                  
                    <!-- END TODO SIDEBAR -->
                    <!-- BEGIN TODO CONTENT -->
                    <div class="todo-content">
                        <div class="portlet light bordered">

                            <div class="portlet-body">
                                <div class="row">
                                    <div class="col-md-5 col-sm-4">
                                       
                            <div class="portlet-body todo-project-list-content">
                                <div class="todo-project-list">
                                    <asp:Panel ID="panChat" runat="server" >
                                                   
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" class="item" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="tabbable-line" style="padding-left: 10px;">
                <ul class="nav nav-tabs ">
                    <li class="active">
                        <a href="#tab_1" data-toggle="tab">View</a>
                    </li>
                    <li>
                        <a href="#tab_2" data-toggle="tab">History </a>
                    </li>
                    <li>
                        <a href="#tab_3" data-toggle="tab">Audit Trail </a>
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
                                                <a class="pull-left" href="javascript:;">
                                                    <%-- <img class="todo-userpic" src="<%# Eval("USERCODE")%>" width="27px" height="27px">--%>
                                                    <img class="todo-userpic" src="../CRM/images/No_image.png" width="27px" height="27px">
                                                </a>
                                                &nbsp;&nbsp;&nbsp;&nbsp;<div class="media-body todo-comment">
                                                    <%-- <button type="button" class="todo-comment-btn btn btn-circle btn-default btn-sm">&nbsp; Reply &nbsp;</button>--%>
                                                    <p class="todo-comment-head">
                                                        <span class="todo-comment-username"><%#Eval("Patient_Name")%></span> &nbsp;
                                                                                            <span class="todo-comment-date"><%# Convert.ToDateTime( Eval("UploadDate")).ToString("dd-MMM-yyyy hh:mm tt")%></span>
                                                    </p>

                                                    <p class="todo-text-color">
                                                        <%#Eval("ActivityPerform")%>
                                                    </p>
                                                    <p>
                                                        <asp:Label ID="lblcompid" runat="server" Text='<%# Eval("COMPID")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblmastercode" runat="server" Text='<%# Eval("MasterCODE")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblLocationID" runat="server" Text='<%# Eval("LocationID")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblLinkMasterCODE" runat="server" Text='<%# Eval("LinkMasterCODE")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblPrefix" runat="server" Text='<%# Eval("Prefix")%>' Visible="false"></asp:Label>

                                                    </p>
                                                    <!-- Nested media object -->
                                                    <%--<div class="media">
                                                                                                    <a class="pull-left" href="javascript:;">
                                                                                                        <img class="todo-userpic" src="assets/pages/media/users/avatar4.jpg" width="27px" height="27px">
                                                                                                    </a>
                                                                                                    <div class="media-body">
                                                                                                        <p class="todo-comment-head">
                                                                                                            <span class="todo-comment-username"><%#Eval("Version")%></span> &nbsp;
                                                                                                    <span class="todo-comment-date"><%# DateTime .Parse ( Eval("UPDTTIME").ToString ())%></span>
                                                                                                        </p>
                                                                                                        <p class="todo-text-color"><%#Eval("ActivityPerform")%> </p>
                                                                                                    </div>
                                                                                                </div>--%>
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
                                        <div class="todo-task-history-desc"><%#Eval("USERNAME ")%> </div>
                                    </li>
                                </ul>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <div class="tab-pane" id="tab_3">
                        <asp:ListView ID="listcrupId" runat="server">
                            <ItemTemplate>
                                <ul class="todo-task-history">
                                    <li>
                                        <div class="todo-task-history-desc"><%#Eval("AuditType ")%> </div>
                                        <div class="todo-task-history-desc"><%#Eval("TableName ")%> </div>
                                        <div class="todo-task-history-desc"><%#Eval("NewValue ")%> </div>
                                    </li>
                                </ul>
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
                       
                                   </div>

                                    <div class="todo-tasklist-devider"></div>
                                    <div class="col-md-7 col-sm-8">
                                        <div class="scroller" style="max-height: 1000px;" data-always-visible="0" data-rail-visible="0" data-handle-color="#dae3e7">
                                            <div class="form-horizontal" style="padding-left: 10px;">
                                                <!-- TASK HEAD -->
                                                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                                                    <div class="alert alert-success alert-dismissable">
                                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlTicki" runat="server">
                                                    <div class="form">
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
                                                            <div class="col-md-2" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label7" runat="server" Text="Complain No.:"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-6" style="margin-bottom: 15px;">
                                                              <asp:TextBox ID="txtcomplaint" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>                                                           
                                                        </div>
                                                        <div class ="form-group">
                                                             <div class="col-md-2" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="lbldate" runat="server" Text="Date:"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-6" style="margin-bottom: 15px;">
                                                                <asp:TextBox ID="txtdates" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtendertxtdateTO" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdates" Format="dd/MMM/yyyy"></cc1:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtdates" ErrorMessage="Date required" InitialValue="0"></asp:RequiredFieldValidator>

                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-2" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label5" runat="server" Text="Complain Type :"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-6" style="margin-bottom: 15px;">
                                                               <asp:TextBox ID="txtcomplaintype" runat="server" CssClass="form-control"></asp:TextBox>
                                                                 </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-2" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label4" runat="server" Text="Department:"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-6" style="margin-bottom: 15px;">
                                                                <asp:TextBox ID="txtdepartment" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-2" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label8" runat="server" Text="Physical Location :"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-6" style="margin-bottom: 15px;">
                                                               <asp:TextBox ID="txtlocation" runat="server" CssClass="form-control"></asp:TextBox>
                                                                 </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-2" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label13" runat="server" Text="Patient Name :"></asp:Label></b>
                                                            </div>

                                                            <div class="col-md-6" style="margin-bottom: 15px;">
                                                                <asp:TextBox ID="txtpatientname" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-2" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label14" runat="server" Text="MRN :"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-6" style="margin-bottom: 15px;">
                                                                <asp:TextBox ID="txtMRN" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-2" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label18" runat="server" Text="Contact No :"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-6" style="margin-bottom: 15px;">
                                                                <asp:TextBox ID="txtcontact" runat="server" CssClass="form-control"></asp:TextBox>
                                                             
                                                            </div>
                                                        </div>
                                                   
                                                        <div class="form-group">
                                                            <div class="col-md-2" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label9" runat="server" Text="Category :"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-6" style="margin-bottom: 15px;">
                                                                <asp:TextBox ID="txtcate" runat="server" CssClass="form-control"></asp:TextBox>
                                                              </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-2" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label10" runat="server" Text="Sub-Category :"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-6" style="margin-bottom: 15px;">
                                                               <asp:TextBox ID="txtsubcat" runat="server" CssClass="form-control"></asp:TextBox>
                                                              </div>
                                                        </div>
                                                          <div class="form-group">
                                                            <div class="col-md-2" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label1" runat="server" Text="Description :"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-6" style="margin-bottom: 15px;">
                                                                 <asp:TextBox ID="txtmessage" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                             </div>
                                                        </div>
                                                     
                                                        <div class="form-group">
                                                            <div class="col-md-2" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label17" runat="server" Text="Reported by :"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-6" style="margin-bottom: 15px;">
                                                                 <asp:TextBox ID="txtreport" runat="server" CssClass="form-control"></asp:TextBox>
                                                             </div>
                                                        </div>

                                                        <div class="form-actions right todo-form-actions">
                                                           
                                                        </div>
                                                        </div>
                                                </asp:Panel>
                                                 <asp:LinkButton ID="btnEdit" runat="server" Visible="false" class="btn btn-sm yellow filter-submit margin-bottom">Print</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" class="btn btn-sm blue filter-submit margin-bottom">Email</asp:LinkButton>
                    <asp:LinkButton ID="lnkback" runat="server" class="btn btn-sm blue filter-submit margin-bottom" OnClick="lnkback_Click">Back</asp:LinkButton>
                                                
                                            
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
</asp:Content>
