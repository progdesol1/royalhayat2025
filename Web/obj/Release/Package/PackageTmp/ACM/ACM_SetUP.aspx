<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="ACM_SetUP.aspx.cs" Inherits="Web.ACM.ACM_SetUP" %>

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
        function GetCHK() {

            var rowCount = '<%=sysListSeparate.Items.Count%>';

            if (document.getElementById('<%=chsysallAdd.ClientID%>').checked == true) {
                var listview = document.getElementById("ContentPlaceHolder1_sysListSeparate");
                var nestedListView = listview.querySelector('#sysListLeft');
                
                for (var i = 0; i < rowCount; i++) {
                    document.getElementById('<%=sysListSeparate.ClientID%>' + '_checkbox1_' + i).checked = true;
                }
            }
            else {
                for (var i = 0; i < rowCount; i++) {
                    document.getElementById('<%=sysListSeparate.ClientID%>' + '_checkbox1_' + i).checked = false;
             }
         }
     }
    </script>

    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
    <script type="text/javascript">
        function showProgressSYS() {
            var updateProgress = $get("<%= UpdateProgressSYS.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
    <script type="text/javascript">
        function showProgressUser() {
            var updateProgress = $get("<%= UpdateProgressUser.ClientID %>");
            updateProgress.style.display = "block";

        }
    </script>
    <script type="text/javascript">
        function fnSelectAll() {
            var rowCount = '<%=ViewState["rowscount"]%>';
            if (document.getElementById('<%=chsysallAdd.ClientID%>').checked == true) {


            }

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
            <div class="col-md-12" style="height: 900px; padding-top: 15px;width: 1300px;margin-left: 23px;">
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
                                            <li class="" style="width: 210px" id="licomditel" runat="server">

                                                <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_Role" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">1 </span><span class="desc">
                                                        <asp:Label ID="lblBusinessRole" runat="server" Text="Role" meta:resourcekey="lblBusinessCoResource1"></asp:Label>
                                                    </span></a>
                                            </li>
                                            <li class="active" style="width: 210px" id="libisnde" runat="server">
                                                <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_System" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">2 </span><span class="desc">
                                                        <asp:Label ID="lblBusinessSystem" runat="server" Text="System"></asp:Label></span></a></li>
                                            <li style="width: 210px">
                                                <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_User" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">3 </span><span class="desc">
                                                        <asp:Label ID="lblBusinessUser" runat="server" Text="User" meta:resourcekey="lblWebExistanceResource1"></asp:Label></span></a>&nbsp;
                                            </li>
                                            <%-- <li style="width: 210px">
                                                <a style="color: #5b9bd1; padding: 0px; width: 150px" href="#tab_reviews" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">4 </span><span class="desc">
                                                        <asp:Label ID="lblWorkingEmploy" runat="server" Text="User Rightes" meta:resourcekey="lblWorkingEmployResource1"></asp:Label></span> </a>
                                            </li>--%>
                                        </ul>

                                        <div class="tab-content no-space">

                                            <div class="tab-pane " id="tab_Role">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <div class="portlet box blue">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="fa fa-gift"></i>
                                                                    <asp:Label ID="lblRolewise" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"></a>
                                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                    <a href="javascript:;" class="reload"></a>
                                                                    <a href="javascript:;" class="remove"></a>
                                                                </div>
                                                                <div class="actions btn-set">
                                                                    <asp:Button ID="btnSaveRole" class="btn purple " runat="server" Text="Submit Menu Role wise" OnClientClick="showProgress()" OnClick="btnSaveRole_Click1" />
                                                                    <a href="#tab_System" class="step" data-toggle="tab" aria-expanded="true">
                                                                        <asp:Label ID="Label7" runat="server" Text="Next " class="btn red btn-circle"></asp:Label>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="tabbable">
                                                                    <div class="tab-content no-space">
                                                                        <div class="form-body">
                                                                            <asp:Panel class="alert alert-danger " ID="pnlErrorMsg" Visible="false" runat="server">
                                                                                <button data-close="alert" class="close"></button>
                                                                                <asp:Label ID="lblerrmsg" runat="server" Text=""></asp:Label>
                                                                            </asp:Panel>
                                                                            <div class="form-group">
                                                                                <div class="col-md-12">


                                                                                    <div class="row">
                                                                                        <div class="col-md-3">
                                                                                            <div class="btn-group">
                                                                                                <asp:DropDownList ID="drproleMST" AutoPostBack="true" runat="server" CssClass="table-group-action-input form-control input-medium" OnSelectedIndexChanged="drproleMST_SelectedIndexChanged"></asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-5">
                                                                                            <asp:Label ID="lblActiveRole" runat="server" Text="Active Or Deactive Role"></asp:Label>
                                                                                            <asp:CheckBox ID="CHKRoleActive" runat="server" />
                                                                                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="(Role is active or deactive to all user For This Tenent)"></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="portlet-body">
                                                                                        <table width="100%" class="table table-striped table-hover table-bordered">
                                                                                            <thead>
                                                                                                <tr style="background-color: lightskyblue">
                                                                                                    <th width="12%">Menu and Sub Menu</th>
                                                                                                    <th width="8%">
                                                                                                        <asp:CheckBox ID="chroleallAdmin" runat="server"></asp:CheckBox>
                                                                                                    </th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chroleallAdd" runat="server" class="group-checkable" data-set="#sample_2 .checkboxes"></asp:CheckBox>
                                                                                                    </th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chroleallEdit" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chroleallDelete" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chroleallPrint" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chroleallLabel" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox6" runat="server"></asp:CheckBox>
                                                                                                    </th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox7" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox8" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox9" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox10" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="10%">
                                                                                                        <asp:CheckBox ID="CheckBox21" runat="server"></asp:CheckBox></th>
                                                                                                    <%--<th>Delete</th>--%>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <asp:ListView ID="RoleListSeparate" runat="server" OnItemDataBound="RoleListSeparate_ItemDataBound">
                                                                                                <ItemTemplate>


                                                                                                    <thead>
                                                                                                        <tr style="background-color: lightgoldenrodyellow;">
                                                                                                            <th width="12%"><%# GetMenuname(Convert.ToInt32(Eval("MENU_ID"))) %><asp:Label ID="lblroleSeparateMENU_ID" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label></th>
                                                                                                            <th width="8%">Admin </th>
                                                                                                            <th width="7%">Add </th>
                                                                                                            <th width="7%">Edit</th>
                                                                                                            <th width="7%">Delete</th>
                                                                                                            <th width="7%">Print</th>
                                                                                                            <th width="7%">Label</th>
                                                                                                            <th width="7%">SP1</th>
                                                                                                            <th width="7%">SP2</th>
                                                                                                            <th width="7%">SP3</th>
                                                                                                            <th width="7%">SP4</th>
                                                                                                            <th width="7%">SP5</th>
                                                                                                            <th width="10%">Active<br />
                                                                                                                Menu</th>
                                                                                                            <%--<th>Delete</th>--%>
                                                                                                        </tr>
                                                                                                    </thead>
                                                                                                    <tbody>

                                                                                                        <asp:ListView ID="RoleListLeft" runat="server">
                                                                                                            <ItemTemplate>
                                                                                                                <tr>

                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblRoleListLeftMENU_ID" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                                                                        <asp:Label ID="lblrolerleleftMENU_NAME1" runat="server" Text='<%# GetMenuname(Convert.ToInt32(Eval("MENU_ID"))) %>'></asp:Label></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chroleAdmin" Checked='<%# Eval("ALL_FLAG").ToString()=="Y"?true:false %>' Enabled="false" runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chroleadd" CssClass="checkboxes" Checked='<%# Eval("ADD_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chroleedit" Checked='<%# Eval("MODIFY_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chroledelete" Checked='<%# Eval("DELETE_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chroleprint" Checked='<%# Eval("VIEW_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chroleLabel" Checked='<%# Eval("LABEL_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chrolesp1" Checked='<%# Eval("SP1").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chrolesp2" Checked='<%# Eval("SP2").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chrolesp3" Checked='<%# Eval("SP3").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chrolesp4" Checked='<%# Eval("SP4").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chrolesp5" Checked='<%# Eval("SP5").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="CHroleMenu" Checked='<%# Eval("ActiveMenu").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>

                                                                                                                </tr>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:ListView>
                                                                                                    </tbody>
                                                                                                </ItemTemplate>
                                                                                            </asp:ListView>

                                                                                        </table>
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
                                                <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                    <ProgressTemplate>
                                                        <div class="overlay">
                                                            <div style="z-index: 1000; margin-left: 45%; margin-top: 30%; opacity: 1; -moz-opacity: 1;">
                                                                <%--<img src="../assets/admin/layout4/img/loading-spinner-blue.gif" />--%>
                                                                <img src="../assets/admin/layout4/img/loading.gif" />
                                                                &nbsp;<asp:Label ID="Labels3" runat="server" Text="Loading..." Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>

                                            <div class="tab-pane active" id="tab_System">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <div class="portlet box purple">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="fa fa-gift"></i>
                                                                    <asp:Label ID="lblforSystem1" Text="Module Wise Menu" runat="server"></asp:Label>

                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"></a>
                                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                    <a href="javascript:;" class="reload"></a>
                                                                    <a href="javascript:;" class="remove"></a>
                                                                </div>
                                                                <div class="actions btn-set">
                                                                    <asp:Button ID="btnSavesys" class="btn red " runat="server" Text="Submit Menu Module wise" OnClientClick="showProgressSYS()" OnClick="btnSavesys_Click1" />
                                                                    <a href="#tab_User" class="step" data-toggle="tab" aria-expanded="true">
                                                                        <asp:Label ID="Label14" runat="server" Text="Next " class="btn red btn-circle"></asp:Label>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="tabbable">
                                                                    <div class="tab-content no-space">
                                                                        <div class="form-body">
                                                                            <asp:Panel class="alert alert-danger " ID="PanelSYS" Visible="false" runat="server">
                                                                                <button data-close="alert" class="close"></button>
                                                                                <asp:Label ID="MsgSYS" runat="server" Text=""></asp:Label>
                                                                            </asp:Panel>
                                                                            <div class="form-group">
                                                                                <div class="col-md-12">

                                                                                    <div class="row">
                                                                                        <div class="col-md-8">
                                                                                            <asp:Label ID="lblActiveModule" runat="server" Text="Tenent Wise Active Or Deactive Module"></asp:Label>
                                                                                            <asp:CheckBox ID="CHKModule" runat="server" />
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="portlet-body">
                                                                                        <table width="100%" class="table table-striped table-hover table-bordered">
                                                                                            <thead>
                                                                                                <tr style="background-color: lightskyblue">
                                                                                                    <th width="12%">Menu and Sub Menu</th>
                                                                                                    <th width="8%">
                                                                                                        <asp:CheckBox ID="chsysallAdmin" runat="server"></asp:CheckBox>
                                                                                                    </th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chsysallAdd" runat="server" onchange="GetCHK()" onclick="GetCHK()"></asp:CheckBox>
                                                                                                    </th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chsysallEdit" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chsysallDelete" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chsysallPrint" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chsysallLabel" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox>
                                                                                                    </th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox2" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox3" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox4" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox5" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="10%">
                                                                                                        <asp:CheckBox ID="CheckBox18" runat="server"></asp:CheckBox></th>
                                                                                                    <%--<th>Delete</th>--%>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <asp:ListView ID="sysListSeparate" runat="server" OnItemDataBound="sysListSeparate_ItemDataBound">
                                                                                                <ItemTemplate>


                                                                                                    <thead>
                                                                                                        <tr style="background-color: lightgoldenrodyellow;">
                                                                                                            <th width="12%"><%# GetMenuname(Convert.ToInt32(Eval("MENU_ID"))) %><asp:Label ID="lblsysSeparateMENU_ID" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label></th>
                                                                                                            <th width="8%">Admin </th>
                                                                                                            <th width="7%">Add </th>
                                                                                                            <th width="7%">Edit</th>
                                                                                                            <th width="7%">Delete</th>
                                                                                                            <th width="7%">Print</th>
                                                                                                            <th width="7%">Label</th>
                                                                                                            <th width="7%">SP1</th>
                                                                                                            <th width="7%">SP2</th>
                                                                                                            <th width="7%">SP3</th>
                                                                                                            <th width="7%">SP4</th>
                                                                                                            <th width="7%">SP5</th>
                                                                                                            <th width="10%">Active<br />
                                                                                                                Menu</th>
                                                                                                            <%--<th>Delete</th>--%>
                                                                                                        </tr>
                                                                                                    </thead>
                                                                                                    <tbody>
                                                                                                        <asp:ListView ID="sysListLeft" runat="server">
                                                                                                            <ItemTemplate>
                                                                                                                <tr>

                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblsysListLeftMENU_ID" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                                                                        <asp:Label ID="lblsysrleleftMENU_NAME1" runat="server" Text='<%# GetMenuname(Convert.ToInt32(Eval("MENU_ID"))) %>'></asp:Label></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chsysAdmin" Checked='<%# Eval("ALL_FLAG").ToString()=="Y"  ?true:false %>' Enabled="false" runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chsysadd" Checked='<%# Eval("ADD_FLAG").ToString()=="Y" ?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chsysedit" Checked='<%# Eval("MODIFY_FLAG").ToString()=="Y"  ?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chsysdelete" Checked='<%# Eval("DELETE_FLAG").ToString()=="Y"  ?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chsysprint" Checked='<%# Eval("VIEW_FLAG").ToString()=="Y"   ?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chsysLabel" Checked='<%# Eval("LABEL_FLAG").ToString()=="Y"   ?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chsyssp1" Checked='<%# Eval("SP1").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chsyssp2" Checked='<%# Eval("SP2").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chsyssp3" Checked='<%# Eval("SP3").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chsyssp4" Checked='<%# Eval("SP4").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chsyssp5" Checked='<%# Eval("SP5").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="CHsysMenu" Checked='<%# Eval("ActiveMenu").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                </tr>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:ListView>
                                                                                                    </tbody>
                                                                                                </ItemTemplate>
                                                                                            </asp:ListView>

                                                                                        </table>
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
                                                <asp:UpdateProgress ID="UpdateProgressSYS" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                                    <ProgressTemplate>
                                                        <div class="overlay">
                                                            <div style="z-index: 1000; margin-left: 45%; margin-top: 30%; opacity: 1; -moz-opacity: 1;">
                                                                <%--<img src="../assets/admin/layout4/img/loading-spinner-blue.gif" />--%>
                                                                <img src="../assets/admin/layout4/img/loading.gif" />
                                                                &nbsp;<asp:Label ID="Labelr3" runat="server" Text="Loading..." Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>
                                            <div class="tab-pane" id="tab_User">
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
                                                                    <asp:Button ID="btnSaveuser" class="btn purple " runat="server" Text="Submit Menu User wise" OnClientClick="showProgressUser()" OnClick="btnSaveuser_Click1" />
                                                                    <a href="ACM_SetUPRights.aspx" class="step">
                                                                        <asp:Label ID="Label62" runat="server" Text="Generate UserTemp " class="btn red btn-circle"></asp:Label>
                                                                    </a>
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
                                                                                        <div class="col-md-3">
                                                                                            <div class="btn-group">
                                                                                                <asp:DropDownList ID="drpUserMST" AutoPostBack="true" runat="server" CssClass="table-group-action-input form-control input-medium" OnSelectedIndexChanged="drpUserMST_SelectedIndexChanged"></asp:DropDownList>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-2">
                                                                                            <asp:Label ID="lblActiveuser" runat="server" Text="Active Or Deactive User"></asp:Label>
                                                                                            <asp:CheckBox ID="CHKUserActive" runat="server" />
                                                                                        </div>
                                                                                        <div class="col-md-3">
                                                                                            <asp:Label ID="Label1" runat="server" Text="User Wise Active Or Deactive Module"></asp:Label>
                                                                                            <asp:CheckBox ID="cheusewiseModule" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="portlet-body">
                                                                                        <table width="100%" class="table table-striped table-hover table-bordered">
                                                                                            <thead>
                                                                                                <tr style="background-color: lightskyblue">
                                                                                                    <th width="12%">Menu and Sub Menu</th>
                                                                                                    <th width="8%">
                                                                                                        <asp:CheckBox ID="chuserallAdmin" runat="server"></asp:CheckBox>
                                                                                                    </th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chuserallAdd" runat="server"></asp:CheckBox>
                                                                                                    </th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chuserallEdit" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chuserallDelete" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chuserallPrint" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="chuserallLabel" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox11" runat="server"></asp:CheckBox>
                                                                                                    </th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox12" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox13" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox14" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="7%">
                                                                                                        <asp:CheckBox ID="CheckBox15" runat="server"></asp:CheckBox></th>
                                                                                                    <th width="10%">
                                                                                                        <asp:CheckBox ID="CheckBox16" runat="server"></asp:CheckBox></th>
                                                                                                    <%--<th>Delete</th>--%>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <asp:ListView ID="userListSeparate" runat="server" OnItemDataBound="userListSeparate_ItemDataBound">
                                                                                                <ItemTemplate>
                                                                                                    <thead>
                                                                                                        <tr style="background-color: lightgoldenrodyellow;">
                                                                                                            <th width="12%"><%# GetMenuname(Convert.ToInt32(Eval("MENU_ID"))) %><asp:Label ID="lbluserSeparateMENU_ID" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label></th>
                                                                                                            <th width="8%">Admin </th>
                                                                                                            <th width="7%">Add </th>
                                                                                                            <th width="7%">Edit</th>
                                                                                                            <th width="7%">Delete</th>
                                                                                                            <th width="7%">Print</th>
                                                                                                            <th width="7%">Label</th>
                                                                                                            <th width="7%">SP1</th>
                                                                                                            <th width="7%">SP2</th>
                                                                                                            <th width="7%">SP3</th>
                                                                                                            <th width="7%">SP4</th>
                                                                                                            <th width="7%">Assign Menu</th>
                                                                                                            <th width="10%">Active<br />
                                                                                                                Menu</th>
                                                                                                            <%--<th>Delete</th>--%>
                                                                                                        </tr>
                                                                                                    </thead>
                                                                                                    <tbody>
                                                                                                        <asp:ListView ID="userListLeft" runat="server">
                                                                                                            <ItemTemplate>
                                                                                                                <tr>

                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lbluserListLeftMENU_ID" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                                                                        <asp:Label ID="lbluserrleleftMENU_NAME1" runat="server" Text='<%# GetMenuname(Convert.ToInt32(Eval("MENU_ID"))) %>'></asp:Label></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chuserAdmin" Checked='<%# Eval("ALL_FLAG").ToString()=="Y"?true:false %>' Enabled="false" runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chuseradd" Checked='<%# Eval("ADD_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chuseredit" Checked='<%# Eval("MODIFY_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chuserdelete" Checked='<%# Eval("DELETE_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chuserprint" Checked='<%# Eval("VIEW_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chuserLabel" Checked='<%# Eval("LABEL_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chusersp1" Checked='<%# Eval("SP1").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chusersp2" Checked='<%# Eval("SP2").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chusersp3" Checked='<%# Eval("SP3").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chusersp4" Checked='<%# Eval("SP4").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="chusersp5" Checked='<%# Eval("SP5").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:CheckBox ID="CHuserMenu" Checked='<%# Eval("ActiveMenu").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                </tr>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:ListView>
                                                                                                    </tbody>
                                                                                                </ItemTemplate>
                                                                                            </asp:ListView>

                                                                                        </table>
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
