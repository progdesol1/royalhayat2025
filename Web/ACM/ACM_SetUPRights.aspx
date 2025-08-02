<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="ACM_SetUPRights.aspx.cs" Inherits="Web.ACM.ACM_SetUPRights" %>

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
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
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
                                                    User Name
                                                 <asp:DropDownList ID="drpUserMST" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpUserMST_SelectedIndexChanged" CssClass="table-group-action-input form-control input-small" Style="margin-bottom: 10px;"></asp:DropDownList>
                                                </label>
                                            </li>
                                            <li class="active" style="width: 210px" id="li2" runat="server">

                                                <label>
                                                    Privilage Name
                                                    <asp:DropDownList ID="drpPrevilegeMST" AutoPostBack="true" runat="server" CssClass="table-group-action-input form-control input-medium" OnSelectedIndexChanged="drpPrevilegeMST_SelectedIndexChanged"></asp:DropDownList>
                                                </label>
                                            </li>
                                            <li class="" style="width: 210px;" id="li3" runat="server">


                                                <a href="ACM_SetUP.aspx" style="border-radius: 0; left: 40px;" class="btn red ">Back To Manage</a>

                                            </li>
                                        </ul>
                                        <div class="tab-content no-space">

                                            <%--<div class="portlet box blue">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="fa fa-gift"></i>
                                                        <asp:Label ID="Label6" runat="server" Text="Role Wise Menu" meta:resourcekey="lblBCDetaResource1"></asp:Label>
                                                    </div>
                                                    <div class="tools">
                                                        <a href="javascript:;" class="collapse"></a>
                                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                        <a href="javascript:;" class="reload"></a>
                                                        <a href="javascript:;" class="remove"></a>
                                                    </div>
                                                    <div class="actions btn-set">
                                                        <asp:Button ID="btnSubmit" Visible="false" class="btn purple " runat="server" Text="Submit" OnClick="btnSubmit_Click1" />
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                    <div class="tabbable">
                                                        <div class="tab-content no-space">
                                                            <div class="form-body">
                                                                <div class="form-group">
                                                                    <div class="col-md-12">
                                                                        <div class="portlet-body">
                                                                            <table width="100%" class="table table-striped table-hover table-bordered">
                                                                                <thead>
                                                                                    <tr bgcolor="yellow">                                                                                       
                                                                                        <th width="12%">Privilage Name</th>
                                                                                        <th width="8%">Admin </th>
                                                                                        <th width="8%">Add </th>
                                                                                        <th width="8%">Edit</th>
                                                                                        <th width="8%">Delete</th>
                                                                                        <th width="8%">Print</th>
                                                                                        <th width="8%">Label</th>
                                                                                        <th width="8%">SP1 </th>
                                                                                        <th width="8%">SP2 </th>
                                                                                        <th width="8%">SP3</th>
                                                                                        <th width="8%">SP4</th>
                                                                                        <th width="8%">SP5</th>
                                                                                    </tr>
                                                                                </thead>                                                                           
                                                                                <asp:ListView ID="RoleListSeparate" runat="server" OnItemDataBound="RoleListSeparate_ItemDataBound" OnItemCommand="RoleListSeparate_ItemCommand">
                                                                                    <ItemTemplate>
                                                                                        <tbody>                                                                                           
                                                                                            <tr>                                                                                               
                                                                                                <td>
                                                                                                    <asp:Label ID="lblRIGHTS_ID" Visible="false" runat="server" Text='<%# Eval("RIGHTS_ID") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblTID" Visible="false" runat="server" Text='<%# Eval("TenentID") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblUID" Visible="false" runat="server" Text='<%# Eval("USER_ID") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblrightPRIVILEGE_ID" Visible="false" runat="server" Text='<%# Eval("PRIVILEGE_ID") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblGetPrivilagename" runat="server" Text='<%# GetPrivilagename(Convert.ToInt32(Eval("PRIVILEGE_ID"))) %>'></asp:Label></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightAdmin" Checked='<%# Eval("ALL_FLAG").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightadd" Checked='<%# Eval("ADD_FLAG").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightedit" Checked='<%# Eval("MODIFY_FLAG").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightdelete" Checked='<%# Eval("DELETE_FLAG").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightprint" Checked='<%# Eval("VIEW_FLAG").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightLabel" Checked='<%# Eval("VIEW_FLAG").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightSP1" Checked='<%# Eval("SP1").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightSP2" Checked='<%# Eval("SP2").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightSP3" Checked='<%# Eval("SP3").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightSP4" Checked='<%# Eval("SP4").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightSP5" Checked='<%# Eval("SP5").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>                                                                                              
                                                                                            </tr>                                                                                           
                                                                                        </tbody>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </table>
                                                                            <td><asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("RIGHTS_ID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="portlet box blue">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="fa fa-gift"></i>
                                                        <asp:Label ID="Label1" runat="server" Text="Module Map" meta:resourcekey="lblBCDetaResource1"></asp:Label>
                                                    </div>
                                                    <div class="tools">
                                                        <a href="javascript:;" class="collapse"></a>
                                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                        <a href="javascript:;" class="reload"></a>
                                                        <a href="javascript:;" class="remove"></a>
                                                    </div>
                                                    <div class="actions btn-set">
                                                        <asp:Button ID="Button1" class="btn purple " runat="server" Text="Submit" OnClick="btnSubmit_Click1" />
                                                        <asp:Button ID="Button2" class="btn red " runat="server" Text="Generate TempUser" OnClick="btnGenerateTempUser_Click" />

                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                    <div class="tabbable">
                                                        <div class="tab-content no-space">
                                                            <div class="form-body">
                                                                <div class="form-group">
                                                                    <div class="col-md-12">
                                                                        <div class="portlet-body">
                                                                            <table width="100%" class="table table-striped table-hover table-bordered">
                                                                                <thead>
                                                                                    <tr bgcolor="yellow">
                                                                                        
                                                                                        <th width="12%">Privilage Name</th>
                                                                                        <th width="8%">Admin </th>
                                                                                        <th width="8%">Add </th>
                                                                                        <th width="8%">Edit</th>
                                                                                        <th width="8%">Delete</th>
                                                                                        <th width="8%">Print</th>
                                                                                        <th width="8%">Label</th>
                                                                                        <th width="8%">SP1 </th>
                                                                                        <th width="8%">SP2 </th>
                                                                                        <th width="8%">SP3</th>
                                                                                        <th width="8%">SP4</th>
                                                                                        <th width="8%">SP5</th>
                                                                                    </tr>
                                                                                </thead>                                                                               
                                                                                <asp:ListView ID="ListView1" runat="server" OnItemDataBound="RoleListSeparate_ItemDataBound" OnItemCommand="RoleListSeparate_ItemCommand">
                                                                                    <ItemTemplate>
                                                                                        <tbody>                                                                                           
                                                                                            <tr>                                                                                              
                                                                                                <td>
                                                                                                    <asp:Label ID="lblRIGHTS_ID" Visible="false" runat="server" Text='<%# Eval("RIGHTS_ID") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblTID" Visible="false" runat="server" Text='<%# Eval("TenentID") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblUID" Visible="false" runat="server" Text='<%# Eval("USER_ID") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblrightPRIVILEGE_ID" Visible="false" runat="server" Text='<%# Eval("PRIVILEGE_ID") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblGetPrivilagename" runat="server" Text='<%# GetPrivilagename(Convert.ToInt32(Eval("PRIVILEGE_ID"))) %>'></asp:Label></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightAdmin" Checked='<%# Eval("ALL_FLAG").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightadd" Checked='<%# Eval("ADD_FLAG").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightedit" Checked='<%# Eval("MODIFY_FLAG").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightdelete" Checked='<%# Eval("DELETE_FLAG").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightprint" Checked='<%# Eval("VIEW_FLAG").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightLabel" Checked='<%# Eval("VIEW_FLAG").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightSP1" Checked='<%# Eval("SP1").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightSP2" Checked='<%# Eval("SP2").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightSP3" Checked='<%# Eval("SP3").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightSP4" Checked='<%# Eval("SP4").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                <td style="text-align: center">
                                                                                                    <asp:CheckBox ID="chrightSP5" Checked='<%# Eval("SP5").ToString()=="True"?true:false %>' runat="server"></asp:CheckBox></td>                                                                                               
                                                                                            </tr>                                                                                           
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

                                            </div>--%>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <div class="portlet box red-sunglo">
                                                        <div class="portlet-title">
                                                            <div class="caption">
                                                                <i class="fa fa-gift"></i>
                                                                <asp:Label ID="lblforUser" Text="Final Temp User" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="tools">
                                                                <a href="javascript:;" class="collapse"></a>
                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                <a href="javascript:;" class="reload"></a>
                                                                <a href="javascript:;" class="remove"></a>
                                                            </div>
                                                            <div class="actions btn-set">
                                                                <asp:Button ID="btnGenerateTempUser" class="btn green " runat="server" Text="Generate TempUser" OnClick="btnGenerateTempUser_Click" />
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

                                                                                <div class="portlet-body">
                                                                                    <table width="100%" class="table table-striped table-hover table-bordered">
                                                                                        <thead>
                                                                                            <tr style="background-color: lightskyblue">
                                                                                                <th width="12%">Menu and Sub Menu</th>
                                                                                                <th width="8%">
                                                                                                    <asp:CheckBox ID="chuserallAdmin" runat="server"></asp:CheckBox>
                                                                                                </th>
                                                                                                <th width="8%">
                                                                                                    <asp:CheckBox ID="chuserallAdd" runat="server"></asp:CheckBox>
                                                                                                </th>
                                                                                                <th width="8%">
                                                                                                    <asp:CheckBox ID="chuserallEdit" runat="server"></asp:CheckBox></th>
                                                                                                <th width="8%">
                                                                                                    <asp:CheckBox ID="chuserallDelete" runat="server"></asp:CheckBox></th>
                                                                                                <th width="8%">
                                                                                                    <asp:CheckBox ID="chuserallPrint" runat="server"></asp:CheckBox></th>
                                                                                                <%--<th width="8%">
                                                                                                <asp:CheckBox ID="chuserallLabel" runat="server"></asp:CheckBox></th>--%>
                                                                                                <th width="8%">
                                                                                                    <asp:CheckBox ID="chuserallSP1" runat="server"></asp:CheckBox>
                                                                                                </th>
                                                                                                <th width="8%">
                                                                                                    <asp:CheckBox ID="chuserallSP2" runat="server"></asp:CheckBox></th>
                                                                                                <th width="8%">
                                                                                                    <asp:CheckBox ID="chuserallSP3" runat="server"></asp:CheckBox></th>
                                                                                                <th width="8%">
                                                                                                    <asp:CheckBox ID="chuserallSP4" runat="server"></asp:CheckBox></th>
                                                                                                <th width="8%">
                                                                                                    <asp:CheckBox ID="chuserallSP5" runat="server"></asp:CheckBox></th>

                                                                                                <%--<th>Delete</th>--%>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <asp:ListView ID="userListSeparate" runat="server" OnItemDataBound="userListSeparate_ItemDataBound">
                                                                                            <ItemTemplate>


                                                                                                <thead>
                                                                                                    <tr style="background-color: lightgoldenrodyellow;">
                                                                                                        <th width="12%"><%# GetMenuname(Convert.ToInt32(Eval("MENUID"))) %><asp:Label ID="lbluserSeparateMENU_ID" Visible="false" runat="server" Text='<%# Eval("MENUID") %>'></asp:Label></th>
                                                                                                        <th width="8%">Admin </th>
                                                                                                        <th width="8%">Add </th>
                                                                                                        <th width="8%">Edit</th>
                                                                                                        <th width="8%">Delete</th>
                                                                                                        <th width="8%">Print</th>
                                                                                                        <%--<th width="8%">Label</th>--%>
                                                                                                        <th width="8%">SP1 </th>
                                                                                                        <th width="8%">SP2</th>
                                                                                                        <th width="8%">SP3</th>
                                                                                                        <th width="8%">SP4</th>
                                                                                                        <th width="8%">SP5</th>
                                                                                                        <%--<th>Delete</th>--%>
                                                                                                    </tr>
                                                                                                </thead>
                                                                                                <tbody>
                                                                                                    <asp:ListView ID="userListLeft" runat="server">
                                                                                                        <ItemTemplate>
                                                                                                            <tr>

                                                                                                                <td>
                                                                                                                    <asp:Label ID="lbluserListLeftMENU_ID" Visible="false" runat="server" Text='<%# Eval("MENUID") %>'></asp:Label>
                                                                                                                    <asp:Label ID="lbluserrleleftMENU_NAME1" runat="server" Text='<%# GetMenuname(Convert.ToInt32(Eval("MENUID"))) %>'></asp:Label></td>
                                                                                                                <td style="text-align: center">
                                                                                                                    <asp:CheckBox ID="chuserAdmin" Checked='<%# Eval("ALL_FLAG").ToString()=="Y"?true:false %>' Enabled="false" runat="server"></asp:CheckBox></td>
                                                                                                                <td style="text-align: center">
                                                                                                                    <asp:CheckBox ID="chuseradd" Enabled="false" Checked='<%# Eval("ADD_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                <td style="text-align: center">
                                                                                                                    <asp:CheckBox ID="chuseredit" Enabled="false" Checked='<%# Eval("MODIFY_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                <td style="text-align: center">
                                                                                                                    <asp:CheckBox ID="chuserdelete" Enabled="false" Checked='<%# Eval("DELETE_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                <td style="text-align: center">
                                                                                                                    <asp:CheckBox ID="chuserprint" Enabled="false" Checked='<%# Eval("VIEW_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                <%--<td style="text-align: center">
                                                                                                                <asp:CheckBox ID="chuserLabel"  Enabled="false" Checked='<%# Eval("LABEL_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>--%>
                                                                                                                <td style="text-align: center">
                                                                                                                    <asp:CheckBox ID="chusersp1" Enabled="false" Checked='<%# Eval("SP1").ToString()=="1"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                <td style="text-align: center">
                                                                                                                    <asp:CheckBox ID="chusersp2" Enabled="false" Checked='<%# Eval("SP2").ToString()=="1"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                <td style="text-align: center">
                                                                                                                    <asp:CheckBox ID="chusersp3" Enabled="false" Checked='<%# Eval("SP3").ToString()=="1"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                <td style="text-align: center">
                                                                                                                    <asp:CheckBox ID="chusersp4" Enabled="false" Checked='<%# Eval("SP4").ToString()=="1"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                <td style="text-align: center">
                                                                                                                    <asp:CheckBox ID="chusersp5" Enabled="false" Checked='<%# Eval("SP5").ToString()=="1"?true:false %>' runat="server"></asp:CheckBox></td>
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
                                            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
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
