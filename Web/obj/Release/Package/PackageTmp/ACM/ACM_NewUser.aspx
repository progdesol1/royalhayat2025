<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="ACM_NewUser.aspx.cs" Inherits="Web.ACM.ACM_NewUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ace_itemCoutry(sender, e) {
            var HiddenField3 = $get('<%= HiddenField3.ClientID %>');
            HiddenField3.value = e.get_value();
        }
    </script>

    <style>
        .LabelFormat {
            color: #428bca;
            font-size: 13px;
            font-weight: 700;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- class="page-content"--%>
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">

                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box blue">

                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Copy 
                                        <asp:Label runat="server" ID="Label15" Text="Fuction"></asp:Label>

                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="LinkButton1" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="portlet-body form">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active">
                                                    <div class="form-body">
                                                        <%-- For Tenent --%>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-horizontal form-row-seperated">
                                                                    <div class="portlet box yellow-crusta">

                                                                        <div class="portlet-title">
                                                                            <div class="caption">
                                                                                <i class="fa fa-clipboard"></i>Add 
                                                                                        <asp:Label runat="server" ID="Label16" Text="Tenent"></asp:Label>

                                                                            </div>
                                                                            <div class="tools">
                                                                                <a href="javascript:;" id="pnltenent" runat="server" class="collapse"></a>
                                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                <asp:LinkButton ID="LinkButton3" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                                                <a href="javascript:;" class="remove"></a>
                                                                            </div>
                                                                        </div>
                                                                        <div class="portlet-body" runat="server" id="pnlblocktenent" style="display: block;">
                                                                            <div class="portlet-body form">
                                                                                <div class="tabbable">
                                                                                    <div class="tab-content no-space">
                                                                                        <div class="tab-pane active">
                                                                                            <div class="form-body">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-5">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">From</legend>
                                                                                                        </fieldset>
                                                                                                    </div>
                                                                                                    <div class="col-md-5">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">To</legend>
                                                                                                        </fieldset>
                                                                                                    </div>
                                                                                                    <div class="col-md-2">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">Copy</legend>

                                                                                                        </fieldset>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-5">
                                                                                                        <div class="form-group" style="color: ">
                                                                                                            <asp:Label runat="server" ID="Label21" class="col-md-4 control-label" Text="Tenent"></asp:Label>
                                                                                                            <div class="col-md-8">
                                                                                                                <asp:DropDownList ID="drpCFfromTenent" CssClass="form-control select2me" runat="server"></asp:DropDownList>

                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-md-5">
                                                                                                        <div class="form-group" style="color: ">
                                                                                                            <asp:Label runat="server" ID="Label22" class="col-md-4 control-label" Text="Tenent"></asp:Label>
                                                                                                            <div class="col-md-8">
                                                                                                                <asp:DropDownList ID="drpCFToTenent" CssClass="form-control select2me" runat="server"></asp:DropDownList>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-md-2">
                                                                                                        <div class="form-group" style="color: ">
                                                                                                            <asp:Button ID="btnTcopy" CssClass="btn btn-sm red" runat="server" Text="Copy" />
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
                                                            </div>
                                                        </div>
                                                        <%-- End Tenent --%>
                                                        <%-- For user --%>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-horizontal form-row-seperated">
                                                                    <div class="portlet box purple">

                                                                        <div class="portlet-title">
                                                                            <div class="caption">
                                                                                <i class="fa fa-user"></i>Add 
                                                                                        <asp:Label runat="server" ID="Label17" Text="User"></asp:Label>

                                                                            </div>
                                                                            <div class="tools">
                                                                                <a href="javascript:;" id="pnluser" runat="server" class="collapse"></a>
                                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                <asp:LinkButton ID="LinkButton4" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                                                <a href="javascript:;" class="remove"></a>
                                                                            </div>
                                                                        </div>
                                                                        <div class="portlet-body" runat="server" id="pnlblockuser" style="display: block;">
                                                                            <div class="portlet-body form">
                                                                                <div class="tabbable">
                                                                                    <div class="tab-content no-space">
                                                                                        <div class="tab-pane active">
                                                                                            <div class="form-body">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-5">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">From</legend>
                                                                                                        </fieldset>
                                                                                                    </div>
                                                                                                    <div class="col-md-5">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">To</legend>
                                                                                                        </fieldset>
                                                                                                    </div>
                                                                                                    <div class="col-md-2">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">Copy</legend>

                                                                                                        </fieldset>
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
                                                        </div>
                                                        <%-- End user --%>
                                                        <%-- For module --%>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-horizontal form-row-seperated">
                                                                    <div class="portlet box green-meadow">

                                                                        <div class="portlet-title">
                                                                            <div class="caption">
                                                                                <i class="fa fa-line-chart"></i>Add 
                                                                                        <asp:Label runat="server" ID="Label18" Text="Module"></asp:Label>

                                                                            </div>
                                                                            <div class="tools">
                                                                                <a href="javascript:;" id="pnlmodule" runat="server" class="collapse"></a>
                                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                <asp:LinkButton ID="LinkButton5" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                                                <a href="javascript:;" class="remove"></a>
                                                                            </div>
                                                                        </div>
                                                                        <div class="portlet-body" runat="server" id="pnlblockmodule" style="display: block;">
                                                                            <div class="portlet-body form">
                                                                                <div class="tabbable">
                                                                                    <div class="tab-content no-space">
                                                                                        <div class="tab-pane active">
                                                                                            <div class="form-body">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-5">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">From</legend>
                                                                                                        </fieldset>
                                                                                                    </div>
                                                                                                    <div class="col-md-5">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">To</legend>
                                                                                                        </fieldset>
                                                                                                    </div>
                                                                                                    <div class="col-md-2">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">Copy</legend>

                                                                                                        </fieldset>
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
                                                        </div>
                                                        <%-- End module --%>
                                                        <%-- For Role --%>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-horizontal form-row-seperated">
                                                                    <div class="portlet box blue-madison">

                                                                        <div class="portlet-title">
                                                                            <div class="caption">
                                                                                <i class="fa fa-user-secret"></i>Add 
                                                                                        <asp:Label runat="server" ID="Label19" Text="Role"></asp:Label>

                                                                            </div>
                                                                            <div class="tools">
                                                                                <a href="javascript:;" runat="server" id="pnlrole" class="collapse"></a>
                                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                <asp:LinkButton ID="LinkButton6" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                                                <a href="javascript:;" class="remove"></a>
                                                                            </div>
                                                                        </div>
                                                                        <div class="portlet-body" runat="server" id="pnlblockrole" style="display: block;">
                                                                            <div class="portlet-body form">
                                                                                <div class="tabbable">
                                                                                    <div class="tab-content no-space">
                                                                                        <div class="tab-pane active">
                                                                                            <div class="form-body">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-5">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">From</legend>
                                                                                                        </fieldset>
                                                                                                    </div>
                                                                                                    <div class="col-md-5">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">To</legend>
                                                                                                        </fieldset>
                                                                                                    </div>
                                                                                                    <div class="col-md-2">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">Copy</legend>

                                                                                                        </fieldset>
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
                                                        </div>
                                                        <%-- End Role --%>
                                                        <%-- For Menu --%>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-horizontal form-row-seperated">
                                                                    <div class="portlet box red-flamingo">

                                                                        <div class="portlet-title">
                                                                            <div class="caption">
                                                                                <i class="fa fa-list"></i>Add 
                                                                                        <asp:Label runat="server" ID="Label20" Text="Menu"></asp:Label>

                                                                            </div>
                                                                            <div class="tools">
                                                                                <a href="javascript:;" id="pnlmmenu" runat="server"></a>
                                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                <asp:LinkButton ID="LinkButton7" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                                                <a href="javascript:;" class="remove"></a>
                                                                            </div>
                                                                        </div>
                                                                        <div class="portlet-body" id="pnlblockmenu" runat="server" style="display: block;">
                                                                            <div class="portlet-body form">
                                                                                <div class="tabbable">
                                                                                    <div class="tab-content no-space">
                                                                                        <div class="tab-pane active">
                                                                                            <div class="form-body">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-5">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">From</legend>
                                                                                                        </fieldset>
                                                                                                    </div>
                                                                                                    <div class="col-md-5">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">To</legend>
                                                                                                        </fieldset>
                                                                                                    </div>
                                                                                                    <div class="col-md-2">
                                                                                                        <fieldset style="margin-bottom: 10px;">
                                                                                                            <legend style="color: green">Copy</legend>

                                                                                                        </fieldset>
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
                                                        </div>
                                                        <%-- End menu --%>
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
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box blue">

                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Copy 
                                        <asp:Label runat="server" ID="lblHeader" Text="Function"></asp:Label>

                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="btnPagereload" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>

                                </div>

                                <div class="portlet-body">
                                    <div class="portlet-body form">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general1">
                                                    <div class="form-body">
                                                        <asp:Panel class="alert alert-danger " ID="pnlErrorMsg" Visible="false" runat="server">
                                                            <button data-close="alert" class="close"></button>
                                                            <asp:Label ID="lblerrmsg" runat="server" Text=""></asp:Label>
                                                        </asp:Panel>
                                                        <div class="row">

                                                            <div class="col-md-5">
                                                                <fieldset style="margin-bottom: 10px;">
                                                                    <legend style="color: green">From</legend>
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="user" class="col-md-4 control-label LabelFormat" Text="User"></asp:Label>
                                                                        <div class="col-md-4">
                                                                            <asp:TextBox ID="txtFromSearchUSer" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtFromSearchUSer" ServiceMethod="GetCustSearch" CompletionInterval="1000" EnableCaching="FALSE" CompletionSetCount="20"
                                                                                MinimumPrefixLength="1" OnClientItemSelected="ace_itemCoutry" DelimiterCharacters=";, :" FirstRowSelected="false"
                                                                                runat="server" />
                                                                            <asp:HiddenField ID="HiddenField3" runat="server" />
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:DropDownList ID="drpUserID" CssClass="form-control select2me" runat="server"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpUserID" ErrorMessage="From User Required" Display="Dynamic" InitialValue="0" ForeColor="#a94442" ValidationGroup="All"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </fieldset>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <fieldset style="margin-bottom: 10px;">
                                                                    <legend style="color: green">TO</legend>
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label3" class="col-md-4 control-label LabelFormat" Text="User"></asp:Label>
                                                                        <div class="col-md-8">
                                                                            <asp:DropDownList ID="drpToUserID" CssClass="form-control select2me" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpToUserID_SelectedIndexChanged"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpToUserID" ErrorMessage="To User Required" Display="Dynamic" InitialValue="0" ForeColor="#a94442" ValidationGroup="All"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                    </div>
                                                                </fieldset>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <fieldset style="margin-bottom: 10px;">
                                                                    <legend style="color: green">Copy</legend>
                                                                    <asp:Button ID="btnusergo" CssClass="btn btn-sm yellow-crusta" OnClick="btnusergo_Click" runat="server" Text="GO" />

                                                                </fieldset>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin-bottom: 10px;">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="Tenent" CssClass="col-md-4 control-label LabelFormat" Text="TenentID"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpTenentID" CssClass="form-control select2me" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpTenentID_SelectedIndexChanged1"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpTenentID" ErrorMessage="From Tenent Required" Display="Dynamic" InitialValue="00" ForeColor="#a94442" ValidationGroup="All"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="Label2" CssClass="col-md-4 control-label LabelFormat" Text="TenentID"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpToTenentID" CssClass="form-control select2me" runat="server"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="drpToTenentID" ErrorMessage="To Tenent Required" Display="Dynamic" InitialValue="00" ForeColor="#a94442" ValidationGroup="All"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:Button ID="btntenentGO" Enabled="false" CssClass="btn btn-sm yellow-crusta" runat="server" Text="GO" OnClick="btntenentGO_Click" ValidationGroup="All" />
                                                                <asp:Button ID="btntenentCppy" CssClass="btn btn-sm red" runat="server" Text="Copy" style="display:none;"/>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin-bottom: 10px;">

                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="Label6" class="col-md-4 control-label LabelFormat" Text="Module"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <%--<asp:DropDownList ID="drpFromModule" CssClass="form-control select2me" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpFromModule_SelectedIndexChanged"></asp:DropDownList>--%>

                                                                        <%-- <table>
                                                                                    <asp:ListView ID="ListView2" runat="server" OnItemCommand="ListView2_ItemCommand">
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:LinkButton ID="LinkFromTrance" runat="server" CommandName="LinkFromTrance" CommandArgument='<%# Eval("Module_Id") %>'>
                                                                                                        <asp:Label ID="Label14" runat="server" Text='<%# Eval("Module_Name") %>'></asp:Label>
                                                                                                    </asp:LinkButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:ListView>
                                                                                </table>--%>
                                                                        <asp:ListBox ID="ListModule" runat="server" CssClass="multi-select" Style="height: 200px; width: 100%" Font-Size="Large" SelectionMode="Multiple" ToolTip="Items"></asp:ListBox>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">

                                                                    <%--<asp:Label runat="server" ID="Label7" class="col-md-4 control-label" Text="Module"></asp:Label>--%>
                                                                    <center>
                                                                                <div class="col-md-4" style="margin-top:30px;">
                                                                                    <asp:LinkButton ID="Linkinput" runat="server" CssClass="btn yellow-gold btn-sm btn-circle" style="padding:1px 4px 2px 4px;" Text="Input" OnClick="Linkinput_Click"></asp:LinkButton>
                                                                                </div>
                                                                            </center>

                                                                    <div class="col-md-8">
                                                                        <%-- <asp:DropDownList ID="drpToModule" CssClass="form-control select2me" runat="server"></asp:DropDownList>--%>
                                                                        <div class="portlet-body" style="overflow: scroll; height: 200px; padding: 0px; border: 1px solid #808080;">
                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                <asp:ListView ID="ListView3" runat="server" OnItemCommand="ListView3_ItemCommand">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="Label14" Font-Size="Large" runat="server" Text='<%# Eval("Module_Name") %>'></asp:Label>
                                                                                                <asp:Label ID="ModuleIDD" Visible="false" runat="server" Text='<%# Eval("Module_Id") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: center; width: 50px;">
                                                                                                <asp:LinkButton ID="LinkDelete" runat="server" CommandName="LinkDelete" CommandArgument='<%# Eval("Module_Id") %>'><img src="../assets/images/recycling.png" /></asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </table>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <asp:Button ID="btnmoduleGO" Enabled="false" CssClass="btn btn-sm yellow-crusta" runat="server" Text="GO" OnClick="btnmoduleGO_Click" />
                                                                <asp:Button ID="btnmoduleCopy" CssClass="btn btn-sm red" runat="server" Text="Copy" style="display:none;" />
                                                            </div>
                                                        </div>

                                                        <%-- Menu --%>
                                                        <div class="row" style="margin-bottom: 10px;">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="Label1" class="col-md-4 control-label LabelFormat" Text="Menu"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <div class="portlet-body" style="overflow: scroll; height: 400px; padding: 0px; border: 1px solid #808080;">
                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>
                                                                                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged"/></th>
                                                                                        <th>Menu Name</th>
                                                                                        <th>Module</th>
                                                                                    </tr>
                                                                                </thead>

                                                                                <tbody>

                                                                                    <asp:ListView ID="fromListMenu" runat="server">
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td style="text-align:center">
                                                                                                    <asp:CheckBox ID="FromCheck" runat="server" />                                                                                                    
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("MENU_NAME1") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblFMenuid" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblFmodule" Visible="false" runat="server" Text='<%# Eval("MODULE_ID") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label7" runat="server" Text='<%# getmodule(Convert.ToInt32(Eval("MODULE_ID"))) %>'></asp:Label></td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:ListView>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <center>
                                                                                <div class="col-md-4" style="margin-top:30px;">
                                                                                    <asp:LinkButton ID="inputMenu" runat="server" CssClass="btn yellow-gold btn-sm btn-circle" style="padding:1px 4px 2px 4px;" Text="Input" OnClick="inputMenu_Click"></asp:LinkButton>
                                                                                </div>
                                                                            </center>
                                                                    <div class="col-md-8">

                                                                        <div class="portlet-body" style="overflow: scroll; height: 400px; padding: 0px; border: 1px solid #808080;">
                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                <thead>
                                                                                    <tr><u></u>
                                                                                        <th>Menu Name</th>
                                                                                        <th>Module</th>
                                                                                        <th>ACTION</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <asp:ListView ID="ToListMenu" runat="server" OnItemCommand="ToListMenu_ItemCommand">
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label8" runat="server" ToolTip="Removied" Text='<%# Eval("MENU_NAME1") %>'></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label9" runat="server" Text='<%# getmodule(Convert.ToInt32(Eval("MODULE_ID"))) %>'></asp:Label></td>
                                                                                                <td style="text-align: center; width: 50px;">
                                                                                                    <asp:LinkButton ID="DeleteMenu" runat="server" CommandName="DeleteMenu" CommandArgument='<%# Eval("MENU_ID")+"-"+ Eval("MODULE_ID") %>'><img src="../assets/images/recycling.png" /></asp:LinkButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:ListView>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:Button ID="FinalCopy" CssClass="btn btn-sm red" runat="server" Enabled="false" Text="Copy" OnClick="FinalCopy_Click" />
                                                            </div>
                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<asp:Panel ID="pnlMenu" runat="server" Visible="true">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="portlet box green-haze">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            <i class="fa fa-globe"></i>
                                                            <asp:Label runat="server" ID="Label8" Text="Menu"></asp:Label>
                                                            List
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                            <asp:LinkButton ID="LinkButton2" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                            <a href="javascript:;" class="remove"></a>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body">
                                                        <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                            <thead>
                                                                <tr>
                                                                    <th>
                                                                        <asp:Label runat="server" ID="lblhMODULE_ID" Text="Module  name"></asp:Label></th>
                                                                    <th>
                                                                        <asp:Label runat="server" ID="lblhMENU_NAME1" Text="Menu name1"></asp:Label></th>
                                                                    <th>
                                                                        <asp:Label runat="server" ID="lblhURLREWRITE" Text="Url Rewrite"></asp:Label></th>
                                                                    <th>
                                                                        <asp:Label runat="server" ID="lblhMENU_ORDER" Text="Menu order"></asp:Label></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:ListView ID="Listview1" runat="server">
                                                                    <LayoutTemplate>
                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                        </tr>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblMODULE_ID" runat="server" Text='<%# Module_Name(Convert.ToInt32(Eval("MODULE_ID")))%>'></asp:Label>
                                                                                <asp:Label ID="lblModule" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                                <asp:Label ID="lblMaster" Visible="false" runat="server" Text='<%# Eval("MASTER_ID") %>'></asp:Label>
                                                                                <asp:Label ID="lblMOD" Visible="false" runat="server" Text='<%# Eval("MODULE_ID") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblMENU_NAME1" runat="server" Text='<%# Eval("MENU_NAME1")%>'></asp:Label></td>

                                                                            <td>
                                                                                <asp:Label ID="lblURLREWRITE" runat="server" Text='<%# Eval("URLREWRITE")%>'></asp:Label></td>
                                                                            <td>
                                                                                <asp:Label ID="lblMENU_ORDER" runat="server" Text='<%# Eval("MENU_ORDER")%>'></asp:Label></td>

                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>

                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>

</asp:Content>

