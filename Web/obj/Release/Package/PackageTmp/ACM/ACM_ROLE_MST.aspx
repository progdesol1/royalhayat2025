<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="ACM_ROLE_MST.aspx.cs" Inherits="Web.ACM.ACM_ROLE_MST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="FULLPAFE" runat="server">
        <ContentTemplate>
            <div>
                <div id="b" runat="server">
                    <div class="col-md-12">

                        <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                            <div class="alert alert-success alert-dismissable">
                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PNALGOL" runat="server" Visible="false">
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="form-horizontal form-row-seperated">
                                        <div class="portlet box blue">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <i class="fa fa-gift"></i>Glogle Configuration  
                                        <asp:Label runat="server" ID="Label1"></asp:Label>
                                                    <asp:TextBox Style="color: #333333" ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
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
                                                            <div class="tab-pane active" id="tab_general11">
                                                                <div class="form-body">
                                                                    <div class="row">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="Label2" class="col-md-4 control-label">Tenant ID</asp:Label><asp:TextBox runat="server" ID="TextBox2" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                    <asp:DropDownList ID="DrpTENANT_ID" runat="server" OnSelectedIndexChanged="DrpTENANT_ID_SelectedIndexChanged" AutoPostBack="true" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="Label3" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox3" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-5">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="Label4" class="col-md-4 control-label">Location ID</asp:Label><asp:TextBox runat="server" ID="TextBox4" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                    <asp:DropDownList ID="drplocation" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drplocation_SelectedIndexChanged"></asp:DropDownList>
                                                                                </div>
                                                                                <asp:Label runat="server" ID="Label6" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox5" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <asp:Button ID="btnGO" runat="server" CssClass="btn blue dz-square" Enabled="false" Text="Go" OnClick="btnGO_Click" />
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
                        </asp:Panel>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-horizontal form-row-seperated">
                                    <div class="portlet box blue">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>Add 
                                        <asp:Label runat="server" ID="lblHeader"></asp:Label>
                                                <asp:TextBox Style="color: #333333" ID="txtHeader" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <asp:LinkButton ID="btnPagereload" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                            <div class="actions btn-set">
                                                <div id="navigation" runat="server" visible="false" class="btn-group btn-group-circle btn-group-solid">
                                                    <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                                    <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                                    <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                                    <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />
                                                </div>
                                                <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="Add New" OnClick="btnAdd_Click" />
                                                <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                                <asp:Button ID="btnEditLable" runat="server" class="btn green-haze btn-circle" OnClick="btnEditLable_Click" Text="Update Label" />
                                                &nbsp;
                                        <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="../assets/global/img/flags/us.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="../assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="../assets/global/img/flags/fr.png" /></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="portlet-body form">
                                                <div class="tabbable">
                                                    <div class="tab-content no-space">
                                                        <div class="tab-pane active" id="tab_general1">
                                                            <div class="form-body">

                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblROLE_NAME1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtROLE_NAME1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtROLE_NAME" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorROLE_NAME" runat="server" ControlToValidate="txtROLE_NAME" ErrorMessage="Role Name Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblROLE_NAME2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtROLE_NAME2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblROLE_NAME11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtROLE_NAME11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtROLE_NAME1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorROLE_NAME1" runat="server" ControlToValidate="txtROLE_NAME1" ErrorMessage="Role Name1 Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblROLE_NAME12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtROLE_NAME12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblROLE_NAME21s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtROLE_NAME21s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtROLE_NAME2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorROLE_NAME2" runat="server" ControlToValidate="txtROLE_NAME2" ErrorMessage="Role Name2 Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblROLE_NAME22h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtROLE_NAME22h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblROLE_DESC1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtROLE_DESC1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                <asp:TextBox TextMode="MultiLine" ID="txtROLE_DESC" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorROLE_DESC" runat="server" ControlToValidate="txtROLE_DESC" ErrorMessage="Role Desc Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblROLE_DESC2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtROLE_DESC2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblACTIVE_FROM_DT1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtACTIVE_FROM_DT1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                <asp:TextBox Placeholder="MM/dd/yyyy" ID="txtACTIVE_FROM_DT" runat="server" CssClass="form-control"> </asp:TextBox>
                                                                                <cc1:CalendarExtender ID="TextBoxACTIVE_FROM_DT_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtACTIVE_FROM_DT" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorACTIVE_FROM_DT" runat="server" ControlToValidate="txtACTIVE_FROM_DT" ErrorMessage="Active From DT Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" TargetControlID="txtACTIVE_FROM_DT" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblACTIVE_FROM_DT2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtACTIVE_FROM_DT2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblACTIVE_TO_DT1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtACTIVE_TO_DT1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                <asp:TextBox Placeholder="MM/dd/yyyy" ID="txtACTIVE_TO_DT" runat="server" CssClass="form-control"> </asp:TextBox>
                                                                                <cc1:CalendarExtender ID="TextBoxACTIVE_TO_DT_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtACTIVE_TO_DT" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorACTIVE_TO_DT" runat="server" ControlToValidate="txtACTIVE_TO_DT" ErrorMessage="Active To DT Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" TargetControlID="txtACTIVE_TO_DT" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblACTIVE_TO_DT2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtACTIVE_TO_DT2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblACTIVE_FLAG1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtACTIVE_FLAG1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                <asp:CheckBox ID="cbACTIVE_FLAG" runat="server" />
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblACTIVE_FLAG2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtACTIVE_FLAG2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                            <div class="portlet box green-haze">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="fa fa-globe"></i>
                                                        <asp:Label runat="server" ID="Label5"></asp:Label>
                                                        List
                                                    </div>
                                                    <div class="tools">
                                                        <a href="javascript:;" class="collapse"></a>
                                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                        <asp:LinkButton ID="btnlistreload" OnClick="btnlistreload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                        <a href="javascript:;" class="remove"></a>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                    <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:Label runat="server" ID="lblhROLE_ID" Text="Role  ID"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label runat="server" ID="lblhROLE_NAME" Text="Role Name"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label runat="server" ID="lblhROLE_NAME2" Text="Role Name2"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label runat="server" ID="lblhACTIVE_FROM_DT" Text="Active From DT"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label runat="server" ID="lblhACTIVE_TO_DT" Text="Active To DT"></asp:Label></th>
                                                                <th style="width: 60px;">ACTION</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="TenentID,ROLE_ID,ROLE_NAME,ROLE_NAME1,ROLE_NAME2,ROLE_DESC,ACTIVE_FLAG,ACTIVE_FROM_DT,ACTIVE_TO_DT,ERP_TENANT_ID,CRUP_ID" DataKeyNames="TenentID,ROLE_ID,ROLE_NAME,ROLE_NAME1,ROLE_NAME2,ROLE_DESC,ACTIVE_FLAG,ACTIVE_FROM_DT,ACTIVE_TO_DT,ERP_TENANT_ID,CRUP_ID">
                                                                <LayoutTemplate>
                                                                    <tr id="ItemPlaceholder" runat="server">
                                                                    </tr>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblROLE_ID" runat="server" Text='<%# Eval("ROLE_ID")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblROLE_NAME" runat="server" Text='<%# Eval("ROLE_NAME")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblROLE_NAME2" runat="server" Text='<%# Eval("ROLE_NAME2")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblACTIVE_FROM_DT" runat="server" Text='<%# Eval("ACTIVE_FROM_DT")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblACTIVE_TO_DT" runat="server" Text='<%# Eval("ACTIVE_TO_DT")%>'></asp:Label></td>
                                                                        <td>
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("ROLE_ID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("ROLE_ID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                </tr>
                                                                            </table>

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
                                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <div class="portlet box blue">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="fa fa-gift"></i>
                                                        <asp:Label runat="server" ID="Label5"></asp:Label>
                                                        List
                                                    </div>
                                                    <div class="tools">
                                                        <a href="javascript:;" class="collapse"></a>
                                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                        <asp:LinkButton ID="btnlistreload" OnClick="btnlistreload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                        <a href="javascript:;" class="remove"></a>
                                                    </div>
                                                </div>
                                                <div class="portlet-body form">
                                                    <asp:Panel runat="server" ID="pnlGrid">
                                                        <div class="tab-content">
                                                            <div id="tab_1_1" class="tab-pane active">

                                                                <div class="tab-content no-space">
                                                                    <div class="tab-pane active" id="tab_general2">
                                                                        <div class="table-container" style="">
                                                                            <div class="portlet-body">
                                                                                <div id="sample_1_wrapper" class="dataTables_wrapper no-footer">

                                                                                    <div class="row">
                                                                                        <div class="col-md-6 col-sm-12">
                                                                                            <div class="dataTables_length" id="sample_1_length">
                                                                                                <label>
                                                                                                    Show
                                                                                       <asp:DropDownList class="form-control input-xsmall input-inline " ID="drpShowGrid" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpShowGrid_SelectedIndexChanged">
                                                                                           <asp:ListItem Value="5" Selected="True">5</asp:ListItem>
                                                                                           <asp:ListItem Value="15">15</asp:ListItem>
                                                                                           <asp:ListItem Value="20">20</asp:ListItem>
                                                                                           <asp:ListItem Value="30">30</asp:ListItem>
                                                                                           <asp:ListItem Value="40">40</asp:ListItem>
                                                                                           <asp:ListItem Value="50">50</asp:ListItem>
                                                                                           <asp:ListItem Value="100">100</asp:ListItem>
                                                                                       </asp:DropDownList>                                                                                                  
                                                                                                entries</label>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-6 col-sm-12">
                                                                                            <div id="sample_1_filter" class="dataTables_filter">
                                                                                                <label>Search:<input type="search" class="form-control input-small input-inline" placeholder="" aria-controls="sample_1"></label>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="table-scrollable">
                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                                                                    <thead>
                                                                                                        <tr>
                                                                                                            <th>
                                                                                                                <asp:Label runat="server" ID="lblhROLE_ID" Text="Role  ID"></asp:Label></th>
                                                                                                            <th>
                                                                                                                <asp:Label runat="server" ID="lblhROLE_NAME" Text="Role Name"></asp:Label></th>
                                                                                                            <th>
                                                                                                                <asp:Label runat="server" ID="lblhROLE_NAME2" Text="Role Name2"></asp:Label></th>
                                                                                                            <th>
                                                                                                                <asp:Label runat="server" ID="lblhACTIVE_FROM_DT" Text="Active From DT"></asp:Label></th>
                                                                                                            <th>
                                                                                                                <asp:Label runat="server" ID="lblhACTIVE_TO_DT" Text="Active To DT"></asp:Label></th>
                                                                                                            <th style="width: 60px;">ACTION</th>
                                                                                                        </tr>
                                                                                                    </thead>
                                                                                                    <tbody>
                                                                                                        <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="TenentID,ROLE_ID,ROLE_NAME,ROLE_NAME1,ROLE_NAME2,ROLE_DESC,ACTIVE_FLAG,ACTIVE_FROM_DT,ACTIVE_TO_DT,ERP_TENANT_ID,CRUP_ID" DataKeyNames="TenentID,ROLE_ID,ROLE_NAME,ROLE_NAME1,ROLE_NAME2,ROLE_DESC,ACTIVE_FLAG,ACTIVE_FROM_DT,ACTIVE_TO_DT,ERP_TENANT_ID,CRUP_ID">
                                                                                                            <LayoutTemplate>
                                                                                                                <tr id="ItemPlaceholder" runat="server">
                                                                                                                </tr>
                                                                                                            </LayoutTemplate>
                                                                                                            <ItemTemplate>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblROLE_ID" runat="server" Text='<%# Eval("ROLE_ID")%>'></asp:Label></td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblROLE_NAME" runat="server" Text='<%# Eval("ROLE_NAME")%>'></asp:Label></td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblROLE_NAME2" runat="server" Text='<%# Eval("ROLE_NAME2")%>'></asp:Label></td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblACTIVE_FROM_DT" runat="server" Text='<%# Eval("ACTIVE_FROM_DT")%>'></asp:Label></td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblACTIVE_TO_DT" runat="server" Text='<%# Eval("ACTIVE_TO_DT")%>'></asp:Label></td>
                                                                                                                    <td>
                                                                                                                        <table>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("ROLE_ID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                                <td>
                                                                                                                                    <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("ROLE_ID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>                                                                                                                                
                                                                                                                            </tr>
                                                                                                                        </table>

                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:ListView>

                                                                                                    </tbody>
                                                                                                </table>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                    <div class="row">
                                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <div class="col-md-5 col-sm-12">
                                                                                                    <div class="dataTables_info" id="sample_1_info" role="status" aria-live="polite">
                                                                                                        <asp:Label ID="lblShowinfEntry" runat="server"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                        <div class="col-md-7 col-sm-12">
                                                                                            <div class="dataTables_paginate paging_simple_numbers" id="sample_1_paginate">                                                                                                
                                                                                                <ul class="pagination">
                                                                                                    <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_fist">                                                                                                        
                                                                                                        <asp:LinkButton ID="btnfirst1" OnClick="btnfirst_Click" runat="server"> First</asp:LinkButton>
                                                                                                    </li>
                                                                                                    <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_Next">                                                                                                        
                                                                                                        <asp:LinkButton ID="btnNext1" OnClick="btnNext1_Click" runat="server"> Next</asp:LinkButton>
                                                                                                    </li>
                                                                                                    <asp:ListView ID="ListView2" runat="server" OnItemCommand="ListView2_ItemCommand" OnItemDataBound="AnswerList_ItemDataBound">
                                                                                                        <ItemTemplate>
                                                                                                            <td>
                                                                                                                <li class="paginate_button " aria-controls="sample_1" tabindex="0">
                                                                                                                    <asp:LinkButton ID="LinkPageavigation" runat="server" CommandName="LinkPageavigation" CommandArgument='<%# Eval("ID")%>'> <%# Eval("ID")%></asp:LinkButton></li>
                                                                                                            </td>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:ListView>
                                                                                                    <li class="paginate_button next" aria-controls="sample_1" tabindex="0" id="sample_1_Previos">
                                                                                                        <asp:LinkButton ID="btnPrevious1" OnClick="btnPrevious1_Click" runat="server"> Prev</asp:LinkButton>
                                                                                                    </li>
                                                                                                    <li class="paginate_button next" aria-controls="sample_1" tabindex="0" id="sample_1_last">
                                                                                                        <asp:LinkButton ID="btnLast1" OnClick="btnLast1_Click" runat="server"> Last</asp:LinkButton>
                                                                                                    </li>
                                                                                                </ul>                                                                                               
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <asp:HiddenField ID="hideID" runat="server" Value="" />
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnfirst1" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnNext1" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnPrevious1" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLast1" EventName="Click" />
                                        </Triggers>

                                    </asp:UpdatePanel>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text=" Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="assets/admin/layout4/img/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>
