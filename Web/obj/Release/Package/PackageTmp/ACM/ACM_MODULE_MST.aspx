<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="ACM_MODULE_MST.aspx.cs" Inherits="Web.ACM.ACM_MODULE_MST" %>

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
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">

                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <%--<div class="row">

                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Glogle Configuration  
                                        <asp:Label runat="server" ID="Label3"></asp:Label>
                                        <asp:TextBox Style="color: #333333" ID="TextBox3" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="LinkButton1" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
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
                                                                    <asp:Label runat="server" ID="Label4" class="col-md-4 control-label">Tenant ID</asp:Label><asp:TextBox runat="server" ID="TextBox4" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="DrpTENANT_ID" runat="server" OnSelectedIndexChanged="DrpTENANT_ID_SelectedIndexChanged" AutoPostBack="true" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>                                                                        
                                                                    </div>
                                                                    <asp:Label runat="server" ID="Label6" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox5" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="Label7" class="col-md-4 control-label">Location ID</asp:Label><asp:TextBox runat="server" ID="TextBox6" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drplocation" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drplocation_SelectedIndexChanged"></asp:DropDownList>                                                                        
                                                                    </div>
                                                                    <asp:Label runat="server" ID="Label8" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox7" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                </div>--%>

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
                                        <asp:LinkButton ID="btnPagereload" OnClick="btnPagereload_Click" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
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
                                                        <%--<div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTENANT_ID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTENANT_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpTENANT_ID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTENANT_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTENANT_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                        <%--<div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTENANT_ID1s" class="col-md-4 control-label">Tenant ID</asp:Label><asp:TextBox runat="server" ID="txtTENANT_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpTENANT_ID" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpTENANT_ID_SelectedIndexChanged"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTENANT_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTENANT_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblLocation" class="col-md-4 control-label">Location</asp:Label><asp:TextBox runat="server" ID="TextBox3" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="DrpLocation" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="DrpLocation_SelectedIndexChanged"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="Label3" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox4" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblModule_Id1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModule_Id1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:CheckBox ID="chIsParents" runat="server" AutoPostBack="true" OnCheckedChanged="chIsParents_CheckedChanged" />
                                                                        <%-- <asp:DropDownList ID="drpModule_Id" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblModule_Id2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModule_Id2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblMYSYSNAME1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMYSYSNAME1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtMYSYSNAME" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMYSYSNAME" runat="server" ControlToValidate="txtMYSYSNAME" ErrorMessage="Mysys Name Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblMYSYSNAME2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMYSYSNAME2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblModule_Name1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModule_Name1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtModule_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorModule_Name" runat="server" ControlToValidate="txtModule_Name" ErrorMessage="Module Name Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblModule_Name2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModule_Name2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblModule_NameO1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModule_NameO1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtModule_NameO" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorModule_NameO" runat="server" ControlToValidate="txtModule_NameO" ErrorMessage="Module NameO Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblModule_NameO2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModule_NameO2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblModule_NameT1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModule_NameT1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtModule_NameT" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorModule_NameT" runat="server" ControlToValidate="txtModule_NameT" ErrorMessage="Module NameT Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblModule_NameT2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModule_NameT2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblModule_Desc1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModule_Desc1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox TextMode="MultiLine" ID="txtModule_Desc" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorModule_Desc" runat="server" ControlToValidate="txtModule_Desc" ErrorMessage="Module Desc Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblModule_Desc2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModule_Desc2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:Panel ID="pnlParent" Visible="true" runat="server">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblParent_Module_id1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtParent_Module_id1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpParent_Module_id" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblParent_Module_id2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtParent_Module_id2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblModule_Order1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModule_Order1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpModule_Order" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblModule_Order2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModule_Order2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblACTIVE_FLAG1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtACTIVE_FLAG1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:CheckBox ID="cbACTIVE_FLAG" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblACTIVE_FLAG2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtACTIVE_FLAG2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblModule_Location1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModule_Location1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtModule_Location" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblModule_Location2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtModule_Location2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="display: none">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="Label1" class="col-md-4 control-label">Tenant ID</asp:Label><asp:TextBox runat="server" ID="TextBox1" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox ID="txtTenant_ID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <%--<asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Tenant Required." ControlToValidate="txtTenant_ID" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                                        <cc1:FilteredTextBoxExtender TargetControlID="txtTenant_ID" ValidChars="/" FilterType="Numbers" ID="FilteredTextBoxExtender1" runat="server"></cc1:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="Label2" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox2" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                                                                Entries</label>
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
                                                                                                        <asp:Label runat="server" ID="lblhModule_Id" Text="Module ID"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhMYSYSNAME" Text="Mysys Name"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhModule_Name" Text="Module Name"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhModule_Desc" Text="Module Desc"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhModule_Order" Text="Module Order"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhModule_Location" Text="Module Location"></asp:Label></th>

                                                                                                    <th style="width: 60px;">ACTION</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="TenentID,Module_Id,MYSYSNAME,Module_Name,Module_NameO,Module_NameT,Module_Desc,Parent_Module_id,Module_Order,ACTIVE_FLAG,CRUP_ID,Module_Location" DataKeyNames="TenentID,Module_Id,MYSYSNAME,Module_Name,Module_NameO,Module_NameT,Module_Desc,Parent_Module_id,Module_Order,ACTIVE_FLAG,CRUP_ID,Module_Location">
                                                                                                    <LayoutTemplate>
                                                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                                                        </tr>
                                                                                                    </LayoutTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblModule_Id" runat="server" Text='<%# Eval("Module_Id")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblMYSYSNAME" runat="server" Text='<%# Eval("MYSYSNAME")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblModule_Name" runat="server" Text='<%# Eval("Module_Name")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblModule_Desc" runat="server" Text='<%# Eval("Module_Desc")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblModule_Order" runat="server" Text='<%# Eval("Module_Order")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblModule_Location" runat="server" Text='<%# Eval("Module_Location")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <table>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("Module_Id")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="LinkButton2" CommandName="btncopy" CommandArgument='<%# Eval("Module_Id")%>' runat="server" class="btn btn-sm blue filter-cancel" ToolTip="Copy"><i class="fa fa-copy"></i></asp:LinkButton></td>                                                                                                                       
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
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet box green-haze">
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
                                        <div class="portlet-body">

                                            <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 60px;">ACTION</th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label9" Text="Tenent"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhModule_Id" Text="Module ID"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhMYSYSNAME" Text="Mysys Name"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhModule_Name" Text="Module Name"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhModule_Desc" Text="Module Desc"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhModule_Order" Text="Module Order"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhModule_Location" Text="Module Location"></asp:Label></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="TenentID,Module_Id,MYSYSNAME,Module_Name,Module_NameO,Module_NameT,Module_Desc,Parent_Module_id,Module_Order,ACTIVE_FLAG,CRUP_ID,Module_Location" DataKeyNames="TenentID,Module_Id,MYSYSNAME,Module_Name,Module_NameO,Module_NameT,Module_Desc,Parent_Module_id,Module_Order,ACTIVE_FLAG,CRUP_ID,Module_Location">
                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("Module_Id")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton2" CommandName="btncopy" CommandArgument='<%# Eval("Module_Id")%>' runat="server" class="btn btn-sm blue filter-cancel" ToolTip="Copy"><i class="fa fa-copy"></i></asp:LinkButton></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label9" Text='<%# Eval("TenentID") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblModule_Id" runat="server" Text='<%# Eval("Module_Id")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblMYSYSNAME" runat="server" Text='<%# Eval("MYSYSNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblModule_Name" runat="server" Text='<%# Eval("Module_Name")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblModule_Desc" runat="server" Text='<%# Eval("Module_Desc")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblModule_Order" runat="server" Text='<%# Eval("Module_Order")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblModule_Location" runat="server" Text='<%# Eval("Module_Location")%>'></asp:Label></td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet box red-flamingo">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>
                                                <asp:Label runat="server" ID="Label3" Text="Privilage"></asp:Label>
                                                List
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <asp:LinkButton ID="LinkButton1" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                        </div>
                                        <div class="portlet-body">

                                            <table class="table table-striped table-bordered table-hover" id="sample_2">
                                                <thead>
                                                    <tr>                                                        
                                                        <th>
                                                            <asp:Label runat="server" ID="Label4" Text="Tenent"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label6" Text="PrivilageID"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label7" Text="Module ID"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label8" Text="Privilage Name"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label10" Text="Active"></asp:Label></th>
                                                       
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="ListPrvilage" runat="server">
                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                               
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label9" Text='<%# Eval("TenentID") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblModule_Id" runat="server" Text='<%# Eval("PRIVILEGE_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblMYSYSNAME" runat="server" Text='<%# Eval("MODULE_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblModule_Name" runat="server" Text='<%# Eval("PRIVILEGE_NAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblModule_Desc" runat="server" Text='<%# Eval("ACTIVE_FLAG").ToString() == "Y" ? "Yes" : "No"%>'></asp:Label></td>
                                                               
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                            </table>
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
