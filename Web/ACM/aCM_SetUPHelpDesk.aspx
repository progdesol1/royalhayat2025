<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="ACM_SetUPHelpDesk.aspx.cs" Inherits="Web.ACM.ACM_SetUPHelpDesk" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Assembly="EditableDropDownList" Namespace="EditableControls" TagPrefix="editable" %>--%>
<%@ Register Assembly="LangTextBox" Namespace="ServerControl1" TagPrefix="Lang" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <style>
        .btn {
            border-bottom-right-radius: 0px;
            border-top-right-radius: 0px;
        }

        .input-group {
            width: 100%;
            text-align: left;
        }

        .form-control {
            width: 100%;
        }

        .aspNetDisabled {
            width: 100%;
        }

        .tagsinput {
            width: 60%;
        }

        .gethide {
            display: none;
        }

        .getshow {
            display: block;
        }
    </style>
    <style type="text/css" media="screen">
        /* commented backslash hack for ie5mac \*/
        html, body {
            height: 100%;
        }
        /* end hack */
        .overlay {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: #EDEDF3;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .CenterPB {
            position: fixed;
            z-index: 999;
            left: 50%;
            top: 40%;
            margin-top: -30px; /* make this half your image/element height */
            margin-left: -30px; /* make this half your image/element width */
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
    </style>

    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css">
    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="../assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css">
    <link href="../assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link href="../assets/global/css/components-rounded.css" id="style_components" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout4/css/layout.css" rel="stylesheet" type="text/css" />
    <link id="style_color" href="../assets/admin/layout4/css/themes/light.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout4/css/custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        //select all checkboxes
        $("#ContentPlaceHolder1_challchallAdd").click(function () {
            $(".checkBoxClass").prop('checked', $(this).prop('checked'));
        });
    </script>

    <script type="text/javascript">
        $(function () {
            // Select deselect all
            $("#ContentPlaceHolder1_chsysallAdd").click(function () {
                if ($(this).is(':checked')) {
                    $('.chsysadd').attr('checked', true);
                } else {
                    $('.chsysadd').prop('checked', false);
                }
            });
        });
    </script>



    <script type="text/javascript">
        function showProgressUser() {
            var updateProgress = $get("<%= UpdateProgressUser.ClientID %>");
            updateProgress.style.display = "block";

        }
    </script>

    <%--document.getElementById('<%=RoleListSeparate.ClientID%>' + '_RoleListLeft_0_chroleadd_' + i).checked = true;
    document.getElementById('<%=RoleListSeparate.ClientID%>' + '_RoleListLeft_0_chroleadd_' + i).checked = false;--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="b" runat="server">
        <%--<ul class="page-breadcrumb breadcrumb">
            <li>
                <a href="index.aspx">
                    <asp:Label ID="lblCRm" runat="server" Text="CRM" meta:resourcekey="lblCRmResource1"></asp:Label></a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <a href="#">
                    <asp:Label ID="lblCompCRm" runat="server" Text="Company Master" meta:resourcekey="lblCompCRmResource1"></asp:Label>
                </a>
            </li>
        </ul>--%>

        <!-- BEGIN BODY -->
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <div class="portlet-body form">
                            <div class="portlet-body">


                                <div class="form-wizard">
                                    <div class="tabbable">
                                        <ul class="nav nav-pills nav-justified steps" style="margin-bottom: 0px; padding-bottom: 0px; padding-top: 0px;">
                                            <li class="active" style="width: 210px" id="licomditel1" runat="server">
                                                <label>
                                                    Tenent_ID
                                                <asp:DropDownList ID="drpTenet" runat="server" OnSelectedIndexChanged="drpTenet_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-small" Style="margin-bottom: 10px;"></asp:DropDownList>
                                                </label>
                                            </li>
                                            <li class="active" style="width: 210px" id="li1" runat="server">

                                                <label>
                                                    Previlage_Name
                                                 <asp:DropDownList ID="drpPrevilegeMST" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpPrevilegeMST_SelectedIndexChanged" CssClass="table-group-action-input form-control input-small" Style="margin-bottom: 10px;"></asp:DropDownList>
                                                </label>
                                            </li>
                                        </ul>
                                        <ul class="nav nav-pills nav-justified steps" style="margin-bottom: 0px; padding-bottom: 0px; padding-top: 0px;">
                                            <%-- <li class="" style="width: 210px" id="licomditel" runat="server">

                                                <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_Role" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">1 </span><span class="desc">
                                                        <asp:Label ID="lblBusinessRole" runat="server" Text="Role" meta:resourcekey="lblBusinessCoResource1"></asp:Label>
                                                    </span></a>
                                            </li>--%>
                                            <%--<li class="active" style="width: 210px" id="libisnde" runat="server">
                                                <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_System" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">2 </span><span class="desc">
                                                        <asp:Label ID="lblBusinessSystem" runat="server" Text="System"></asp:Label></span></a>
                                            </li>--%>
                                            <li class="active" style="width: 210px">
                                                <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_User" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;"></span><span class="desc">
                                                        <asp:Label ID="lblBusinessUser" runat="server" Text="User" meta:resourcekey="lblWebExistanceResource1"></asp:Label></span></a>&nbsp;
                                            </li>
                                            <%-- <li style="width: 210px">
                                                <a style="color: #5b9bd1; padding: 0px; width: 150px" href="#tab_reviews" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">4 </span><span class="desc">
                                                        <asp:Label ID="lblWorkingEmploy" runat="server" Text="User Rightes" meta:resourcekey="lblWorkingEmployResource1"></asp:Label></span> </a>
                                            </li>--%>
                                        </ul>

                                        <div class="tab-content no-space">


                                              <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                                                    <div class="alert alert-success alert-dismissable">
                                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                        <asp:Label ID="lblmsh" runat="server"></asp:Label>
                                                    </div>
                                                </asp:Panel>

                                            <div class="tab-pane active" id="tab_User">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <div class="portlet box red-sunglo">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="fa fa-gift"></i>
                                                                    <asp:Label ID="lblforUser" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"></a>
                                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                    <a href="javascript:;" class="reload"></a>
                                                                    <a href="javascript:;" class="remove"></a>
                                                                </div>
                                                                <div class="actions btn-set">
                                                                    <asp:Button ID="btnSaveuser" class="btn purple " runat="server" Text="Submit" OnClientClick="showProgressUser()" OnClick="btnSaveuser_Click1" />
                                                                  
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="tabbable">
                                                                    <div class="tab-content no-space">
                                                                        <div class="form-body">
                                                                            <asp:Panel class="alert alert-danger " ID="PanelUser" Visible="false" runat="server">
                                                                                <button data-close="alert" class="close"></button>
                                                                                <asp:Label ID="MsgUser" runat="server" Text=""></asp:Label>
                                                                            </asp:Panel>
                                                                            <div class="form-group">
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                       <div class="col-md-1"></div>
                                                                                        <div class="col-md-3">
                                                                                            <div class="btn-group">
                                                                                                <asp:DropDownList ID="drpUserMST" AutoPostBack="true" runat="server" CssClass="table-group-action-input form-control input-medium" OnSelectedIndexChanged="drpUserMST_SelectedIndexChanged"></asp:DropDownList>
                                                                                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                                                                                 </div>
                                                                                        </div>
                                                                                       
                                                                                    </div>
                                                                                    <div class="portlet-body">
                                                                                        <table width="100%" class="table table-striped table-hover table-bordered">
                                                                                            <thead>
                                                                                                <tr style="background-color: lightskyblue">
                                                                                                    <th width="12%">Functions And Events</th>
                                                                                                    <th width="8%">
                                                                                                        <asp:CheckBox ID="chuserallAdmin" runat="server"></asp:CheckBox>
                                                                                                    </th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chuserallAdd" runat="server" Visible="false"></asp:CheckBox>
                                                                                                    </th>
                                                                                                    <th width="10%">
                                                                                                        <asp:CheckBox ID="CheckBox16" runat="server" Visible="false"></asp:CheckBox></th>

                                                                                                    <%--<th>Delete</th>--%>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            
                                                                                        </table>
                                                                                        <div class="row">
                                                                                             <div class="col-md-2"></div>
                                                                                            <div class="col-md-3">
                                                                                               <h4 ><strong><asp:Label ID="lblreopen" Visible="false" runat="server" Text="Reopen"></asp:Label></strong></h4>  
                                                                                            </div>
                                                                                             <div class="col-md-4">
                                                                                                 <asp:CheckBox ID="chkreopen" runat="server" Visible="false"/>
                                                                                             </div>
                                                                                        </div>
                                                                                       <div class="row">
                                                                                             <div class="col-md-2"></div>
                                                                                            <div class="col-md-3">
                                                                                               <h4 ><strong><asp:Label ID="lblnewtkt" Visible="false" runat="server" Text="Add New Ticket"></asp:Label></strong></h4>  
                                                                                            </div>
                                                                                             <div class="col-md-4">
                                                                                                 <asp:CheckBox ID="chknewtickt" runat="server" Visible="false"/>
                                                                                             </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                             <div class="col-md-2"></div>
                                                                                            <div class="col-md-3">
                                                                                               <h4 ><strong><asp:Label ID="lbldlttkt" Visible="false" runat="server" Text="Delete Ticket"></asp:Label></strong></h4>  
                                                                                            </div>
                                                                                             <div class="col-md-4">
                                                                                                 <asp:CheckBox ID="chkdlttkt" runat="server" Visible="false"/>
                                                                                             </div>
                                                                                        </div>
                                                                                         <div class="row">
                                                                                             <div class="col-md-2"></div>
                                                                                            <div class="col-md-3">
                                                                                               <h4 ><strong><asp:Label ID="lblptinr" Visible="false" runat="server" Text="Print"></asp:Label></strong></h4>  
                                                                                            </div>
                                                                                             <div class="col-md-4">
                                                                                                 <asp:CheckBox ID="chkprint" runat="server" Visible="false"/>
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
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:UpdateProgress ID="UpdateProgressUser" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                                    <ProgressTemplate>
                                                        <div class="overlay">
                                                            <div style="z-index: 1000; margin-left: 45%; margin-top: 30%; opacity: 1; -moz-opacity: 1;">
                                                                <%--<img src="../assets/admin/layout4/img/loading-spinner-blue.gif" />--%>
                                                                <img src="../assets/admin/layout4/img/loading.gif" />
                                                                &nbsp;<asp:Label ID="Labelu3" runat="server" Text="Loading..." Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>

                                            <asp:Panel ID="Panel1" Visible="false" runat="server">
                                                <div class="page-content-wrapper">
                                                    <div class="form-horizontal form-row-seperated">
                                                        <div class="portlet light">

                                                            <div class="portlet-body">
                                                                <div class="tabbable">
                                                                    <div class="tab-content no-space">
                                                                        <div class="form-body">
                                                                            <div class="form-group">
                                                                                <div class="col-md-12">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
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



        <div class="scroll-to-top">
            <i class="icon-arrow-up"></i>
        </div>
    </div>

    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
    <!-- IMPORTANT! Load jquery-ui.min.js before bootstrap.min.js to fix bootstrap tooltip conflict with jquery ui tooltip -->
    <script src="../assets/global/plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script type="text/javascript" src="../assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="../assets/global/scripts/metronic.js" type="text/javascript"></script>
    <script src="../assets/admin/layout4/scripts/layout.js" type="text/javascript"></script>
    <script src="../assets/admin/layout4/scripts/demo.js" type="text/javascript"></script>
    <script src="../assets/admin/pages/scripts/table-editable.js"></script>
    <script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Demo.init(); // init demo features
            TableEditable.init();
        });
    </script>
</asp:Content>
