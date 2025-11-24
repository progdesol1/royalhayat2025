<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="ICUOM.aspx.cs" Inherits="Web.Master.ICUOM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }
    </style>
    <style type="text/css">
        .col-md-1_5 {
            width: 11.33333333%;
            float: left;
            padding-left: 10px;
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
                                        <%--<div class="btn-group btn-group-circle btn-group-solid">
                                            <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />
                                        </div>--%>
                                        <%--<a target="_blank" href="ICUOMCONV.aspx" class="btn green-haze btn-circle">UOM Conversion</a>--%>

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
                                                                    <asp:Label runat="server" ID="lblTenantID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenantID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpTenantID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTenantID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenantID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUOM1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOM1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpUOM" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUOM2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOM2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUOMNAMESHORT1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAMESHORT1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtUOMNAMESHORT" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUOMNAMESHORT" ErrorMessage="Short Name UOM Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUOMNAMESHORT2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAMESHORT2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUOMNAME11s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME11s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtUOMNAME1" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtUOMNAME1_TextChanged"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUOMNAME1" runat="server" ControlToValidate="txtUOMNAME1" ErrorMessage="Uomname1 Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUOMNAME12h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME12h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUOMNAME21s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME21s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtUOMNAME2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUOMNAME2" runat="server" ControlToValidate="txtUOMNAME2" ErrorMessage="Uomname2 Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUOMNAME22h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME22h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUOMNAME31s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME31s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtUOMNAME3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUOMNAME3" runat="server" ControlToValidate="txtUOMNAME3" ErrorMessage="Uomname3 Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUOMNAME32h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME32h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label14" runat="server" Text="Allow Multi" CssClass="col-md-3 control-label"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:RadioButtonList ID="RdoAlloMulti" runat="server" CssClass="radio-inline" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                             <div class="col-md-5">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label15" runat="server" Text="Aspect Ratio" CssClass="col-md-3 control-label"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:RadioButtonList ID="RdoAllowRatio" runat="server" CssClass="radio-inline" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-11">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblREMARKS1s" CssClass="col-md-1_5 control-label"></asp:Label><asp:TextBox runat="server" ID="txtREMARKS1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-9">
                                                                        <asp:TextBox TextMode="MultiLine" ID="txtREMARKS" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtREMARKS" ErrorMessage="Remark Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblREMARKS2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtREMARKS2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <%--<div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCRUP_ID1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRUP_ID1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpCRUP_ID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCRUP_ID2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRUP_ID2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>--%>
                                                            <%--<div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblActive1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:CheckBox ID="cbActive" Checked="true" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblActive2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>--%>
                                                        </div>
                                                        <%-- <div class="row">                                                           
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUOMNAME1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtUOMNAME" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUOMNAME2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                             <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUOMNAMEO1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAMEO1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtUOMNAMEO" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUOMNAMEO2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAMEO2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
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
                            <asp:Panel ID="pnlConversion" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="portlet box purple">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <i class="fa fa-gift"></i>Add UOM Conversion
                                                </div>
                                                <div class="tools">
                                                    <a href="javascript:;" class="collapse"></a>
                                                    <a href="javascript:;" class="remove"></a>
                                                </div>
                                            </div>
                                            <div class="portlet-body">
                                                <div class="portlet-body form">
                                                    <div class="tabbable">
                                                        <div class="tab-content no-space">
                                                            <div class="tab-pane active">
                                                                <div class="form-body">

                                                                    <div class="row">
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <asp:Label ID="Label10" runat="server" CssClass="col-md-12 control-label" Text="From UOM" Style="padding-left: 20px; color: #4A8BC2; font-size: 16px; font-weight: bold;"></asp:Label>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList ID="DrpConvFromUOM" CssClass="form-control select2me" Enabled="false" Style="width: 80%;" runat="server"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <asp:Label ID="Label11" runat="server" CssClass="col-md-12 control-label" Text="To UOM" Style="padding-left: 20px; color: #4A8BC2; font-size: 16px; font-weight: bold;"></asp:Label>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList ID="DrpConvToUOM" CssClass="form-control select2me" Style="width: 80%;" runat="server"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <asp:Label ID="Label12" runat="server" CssClass="col-md-12 control-label" Text="Conversion" Style="padding-left: 20px; color: #4A8BC2; font-size: 16px; font-weight: bold;"></asp:Label>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="txtConv" CssClass="form-control" Style="width: 80%;" runat="server" AutoPostBack="true" OnTextChanged="txtConv_TextChanged"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" SetFocusOnError="true" runat="server" ControlToValidate="txtConv" ErrorMessage="Conversion Required" CssClass="Validation" ValidationGroup="CONV"></asp:RequiredFieldValidator>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtConv" ValidChars="0123456789." FilterType="Custom, numbers" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <div class="form-group">
                                                                                <asp:Label ID="Label13" runat="server" CssClass="col-md-12 control-label" Text="Remark" Style="padding-left: 20px; color: #4A8BC2; font-size: 16px; font-weight: bold;"></asp:Label>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="txtConvRemark" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" SetFocusOnError="true" runat="server" ControlToValidate="txtConvRemark" ErrorMessage="Remark Required" CssClass="Validation" ValidationGroup="CONV"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <div class="form-actions" style="padding: 10px 10px 10px 10px;">
                                                                        <asp:Button ID="btnConvSave" CssClass="btn green" runat="server" Text="Save" ValidationGroup="CONV" OnClick="btnConvSave_Click" />
                                                                        <asp:Button ID="btnConvCancel" CssClass="btn red" runat="server" Text="Cancel" OnClick="btnConvCancel_Click" />
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
                        </div>
                        <%--                           <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
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
                                                                                                        <asp:Label runat="server" ID="lblhUOMNAME1" Text="Uomname1"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhUOMNAME2" Text="Uomname2"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhUOMNAME3" Text="Uomname3"></asp:Label></th>

                                                                                                    <th style="width: 60px;">ACTION</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand">
                                                                                                    <LayoutTemplate>
                                                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                                                        </tr>
                                                                                                    </LayoutTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>

                                                                                                            <td>
                                                                                                                <asp:Label ID="lblUOMNAME1" runat="server" Text='<%# Eval("UOMNAME1")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblUOMNAME2" runat="server" Text='<%# Eval("UOMNAME2")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblUOMNAME3" runat="server" Text='<%# Eval("UOMNAME3")%>'></asp:Label></td>

                                                                                                            <td>
                                                                                                                <table>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("UOM")+","+ Eval("TenantID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("UOM") +","+ Eval("TenantID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>                                                                                                                     
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
                                            <i class="fa fa-globe"></i>
                                            <asp:Label runat="server" ID="Label5"></asp:Label>
                                            List
                                        </div>
                                        <div class="tools">
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <table class="table table-striped table-bordered table-hover" id="sample_1">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:Label runat="server" ID="lblhUOMNAME1" Text="Uomname1"></asp:Label></th>
                                                    <th>
                                                        <asp:Label runat="server" ID="lblhUOMNAME2" Text="Uomname2"></asp:Label></th>
                                                    <th>
                                                        <asp:Label runat="server" ID="lblhUOMNAME3" Text="Uomname3"></asp:Label></th>
                                                    <th style="width: 60px;">ACTION</th>
                                                </tr>
                                            </thead>
                                            <tbody>

                                                <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" OnItemDataBound="Listview1_ItemDataBound" DataKeyNames="TenentID,UOM,UOMNAMESHORT,UOMNAME1,UOMNAME2,UOMNAME3,REMARKS,CRUP_ID,Active,UOMNAME,UOMNAMEO">
                                                    <LayoutTemplate>
                                                        <tr id="ItemPlaceholder" runat="server">
                                                        </tr>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="View" CommandArgument='<%# Eval("UOM")+","+ Eval("TenentID")%>'>
                                                                    <asp:Label ID="lblUOMNAME1" runat="server" Text='<%# Eval("UOMNAME1")%>'></asp:Label>
                                                                    <asp:Label ID="MultiallowYN" runat="server" Visible="false" Text='<%# Eval("MultiUOMAllow") %>'></asp:Label>
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="View" CommandArgument='<%# Eval("UOM")+","+ Eval("TenentID")%>'>
                                                                    <asp:Label ID="lblUOMNAME2" runat="server" Text='<%# Eval("UOMNAME2")%>'></asp:Label>
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="View" CommandArgument='<%# Eval("UOM")+","+ Eval("TenentID")%>'>
                                                                    <asp:Label ID="lblUOMNAME3" runat="server" Text='<%# Eval("UOMNAME3")%>'></asp:Label>
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("UOM")+","+ Eval("TenentID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("UOM") +","+ Eval("TenentID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                         <td>
                                                                            <asp:LinkButton ID="LinkConversation" CommandName="Conversion" CommandArgument='<%# Eval("UOM")+","+ Eval("TenentID")%>' runat="server" Text="Add Conversion" CssClass="btn btn-sm yellow-lemon" Style="padding: 1px 10px 2px 10px; font-style: oblique; font-weight: bold;"></asp:LinkButton>
                                                                        </td>
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

                        <div class="row">
                            <div class="col-md-12 col-sm-12">
                                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                <div class="portlet box yellow">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-user"></i>UOM Conversion List
                                            <asp:Label ID="BaseUOM" runat="server" Text="Label" Style="font-family: 'Courier New'; font-size: 18px; font-weight: 600; padding-left: 2px;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <table class="table table-striped table-bordered table-hover" id="sample_2">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="Label1" runat="server" Text="From UOM"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label2" runat="server" Text="To UOM"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label3" runat="server" Text="Conversion"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="Label4" runat="server" Text="Remark"></asp:Label>
                                                    </th>
                                                    <th style="width: 60px;">ACTION</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:ListView ID="ListView2" runat="server" OnItemCommand="ListView2_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr class="odd gradeX">
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# GetUOMName(Convert.ToInt32(Eval("FUOM"))) %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# GetUOMName(Convert.ToInt32(Eval("TUOM"))) %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("CONVERSION") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkConvEdit" runat="server" CommandName="LinkConvEdit" CommandArgument='<%# Eval("FUOM") +","+ Eval("TUOM") %>' CssClass="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkConvDelete" runat="server" CommandName="LinkConvDelete" CommandArgument='<%# Eval("FUOM") +","+ Eval("TUOM") %>' CssClass="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton>
                                                                        </td>
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
                                <!-- END EXAMPLE TABLE PORTLET-->
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

        <%-- <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
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
