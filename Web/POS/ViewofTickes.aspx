<%@ Page Title="" Language="C#" MasterPageFile="~/POS/POSMaster.Master" AutoEventWireup="true" CodeBehind="ViewofTickes.aspx.cs" Inherits="Web.POS.ViewofTickes" %>

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
        <%--class="page-content"--%>
        <!-- BEGIN PAGE HEAD-->
        <div class="page-head">
            <!-- BEGIN PAGE TITLE -->
            <div class="page-title">
                <h1>Feedback for <asp:Label ID="lblUser" runat="server"></asp:Label>
                    <small></small>
                </h1>
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
                  
                    <!-- END TODO SIDEBAR -->
                    <!-- BEGIN TODO CONTENT -->
                    <div class="todo-content">
                        <div class="portlet light bordered">

                            <div class="portlet-body">
                                <div class="row">
                                    <div class="col-md-5 col-sm-4">
                                       <div class="portlet light bordered">
                         

                            <div class="portlet-body todo-project-list-content">
                                <div class="todo-project-list">
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
                                                                                                        <span class="todo-comment-username"><%#Eval("Version")%></span> &nbsp;
                                                                                            <span class="todo-comment-date"><%# Convert.ToDateTime( Eval("UPDTTIME")).ToString("dd-MMM-yyyy hh:mm tt")%></span>
                                                                                                    </p>

                                                                                                    <p class="todo-text-color">
                                                                                                        <%#Eval("ActivityPerform")%>
                                                                                                    </p>
                                                                                                    <p>
                                                                                                        <asp:Label ID="lblcompid" runat="server" Text='<%# Eval("COMPID")%>' Visible="false"></asp:Label>
                                                                                                        <asp:Label ID="lblmastercode" runat="server" Text='<%# Eval("MasterCODE")%>' Visible="false"></asp:Label>
                                                                                                        <asp:Label ID="lblMyLineNo" runat="server" Text='<%# Eval("MyLineNo")%>' Visible="false"></asp:Label>
                                                                                                        <asp:Label ID="lblLocationID" runat="server" Text='<%# Eval("LocationID")%>' Visible="false"></asp:Label>
                                                                                                        <asp:Label ID="lblLinkMasterCODE" runat="server" Text='<%# Eval("LinkMasterCODE")%>' Visible="false"></asp:Label>
                                                                                                        <asp:Label ID="lblActivityID" runat="server" Text='<%# Eval("ActivityID")%>' Visible="false"></asp:Label>
                                                                                                        <asp:Label ID="lblPrefix" runat="server" Text='<%# Eval("Prefix")%>' Visible="false"></asp:Label>
                                                                                                        <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("MasterCODE") %>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>
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
                                                                        <asp:ListItem Value="1" Text="Immediate action"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="Follow-up"></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="Corrective action"></asp:ListItem>
                                                                        <asp:ListItem Value="4" Text="Investigation results"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <asp:DropDownList ID="drpinvestigation" runat="server" OnSelectedIndexChanged="drpinvestigation_SelectedIndexChanged1" Visible="false" AutoPostBack="true" CssClass="form-control">
                                                                        <asp:ListItem Value="1" Text="Releveant"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="Irrelevant"></asp:ListItem>
                                                                    </asp:DropDownList>

                                                                    <div class="media-body">
                                                                        <asp:TextBox ID="txtComent" runat="server" placeholder="Type comment..." Rows="4" class="form-control todo-taskbody-taskdesc" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group" style="padding-top: 20px; margin-right: 0px;">
                                                                        <div class="col-md-4" style="margin-bottom: 15px; padding-left: 20px;">
                                                                            <b>
                                                                                <asp:Label ID="Label6" runat="server" Text="Status:"></asp:Label></b>
                                                                        </div>

                                                                        <div class="col-md-8" style="margin-bottom: 15px;">

                                                                            <asp:DropDownList ID="drpStatus" runat="server" class="form-control todo-taskbody-tags">

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
                                                                    <asp:Button ID="btnSubmit" ValidationGroup="" runat="server" class="btn btn-sm btn-circle green" OnClick="btnSubmit_Click" Text="Submit" />
                                                                    <asp:Button ID="btnTikitClose" runat="server" class="pull-right btn btn-circle btn-sm btn-default" OnClick="btnTikitClose_Click" Text="Back" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
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
                                                           
                                                            <div class="col-md-4" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="lbldate" runat="server" Text="Date:"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-8" style="margin-bottom: 15px;">
                                                                <asp:TextBox ID="txtdates" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtendertxtdateTO" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdates" Format="dd/MMM/yyyy"></cc1:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtdates" ErrorMessage="Date required" InitialValue="0"></asp:RequiredFieldValidator>

                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-4" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label5" runat="server" Text="Feedback Type :"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-8" style="margin-bottom: 15px;">
                                                                <asp:DropDownList ID="drpFeedbackype" runat="server" class="form-control select2me" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpFeedbackype" ErrorMessage="Feedback Type required" InitialValue="0"></asp:RequiredFieldValidator>
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
                                                                <asp:RequiredFieldValidator ID="required1" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtMRN" ErrorMessage="Please add MRN"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
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
                                                                    <asp:Label ID="Label15" runat="server" Text="Staff Involved :"></asp:Label></b>
                                                            </div>

                                                            <div class="col-md-2" style="margin-bottom: 15px;">
                                                                <asp:CheckBox ID="chklist" runat="server" OnCheckedChanged="chklist_CheckedChanged" AutoPostBack="true" />

                                                            </div>
                                                            <div class="col-md-6" style="margin-bottom: 15px;">
                                                                <asp:TextBox ID="txtstaffname" runat="server" CssClass="form-control" Visible="false" placeholder="Staff Name"></asp:TextBox>
                                                                 </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-md-4" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label16" runat="server" Text="IR Done :"></asp:Label></b>
                                                            </div>

                                                            <div class="col-md-8" style="margin-bottom: 15px;">
                                                                <asp:CheckBox ID="chkirno" runat="server" />

                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-md-4" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label19" runat="server" Text="Incident Reported Needed "></asp:Label></b>
                                                            </div>

                                                            <div class="col-md-8" style="margin-bottom: 15px;">
                                                                <asp:CheckBox ID="chkinci" runat="server" />

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
                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="DrpSubCat" ErrorMessage="Sub-Category required" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-md-4" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label2" runat="server" Text="Subject :"></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-8" style="margin-bottom: 15px;">
                                                                <asp:TextBox ID="txtSubject" runat="server" placeholder="Wirte The Subject" class="form-control todo-taskbody-due">
                                                                </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtSubject" ErrorMessage="Subject required"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>

                                                        <!-- TASK DESC -->
                                                        <div class="form-group">
                                                            <div class="col-md-4" style="margin-bottom: 15px;">
                                                                <b>
                                                                    <asp:Label ID="Label3" runat="server" Text=" Feedback Details : "></asp:Label></b>
                                                            </div>
                                                            <div class="col-md-8" style="margin-bottom: 15px;">
                                                                <asp:TextBox ID="txtMessage" runat="server" placeholder="Wirte The Messaging" class="form-control todo-taskbody-taskdesc" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtMessage" ErrorMessage="Message required"></asp:RequiredFieldValidator>
                                                                <%--<textarea class="form-control todo-taskbody-taskdesc" rows="8" placeholder="Task Description..."></textarea>--%>
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

                                                        <div class="form-actions right todo-form-actions">
                                                            <asp:Button ID="btnSave" runat="server" class="btn btn-circle btn-sm green" OnClick="btnSave_Click" Text="Submit" ValidationGroup="Ticks" />
                                                            <asp:Button ID="btnCancel" runat="server" class="btn btn-circle btn-sm btn-default" OnClick="btnCancel_Click" Text="Cancel" />
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
