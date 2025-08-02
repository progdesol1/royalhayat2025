<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="TBLCOLOR.aspx.cs" Inherits="Web.Master.TBLCOLOR" %>

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
                                        <div class="btn-group btn-group-circle btn-group-solid">
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
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTenantID1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenantID1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpTenantID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTenantID2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenantID2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCOLORID1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOLORID1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpCOLORID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCOLORID2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOLORID2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCOLORDESC11s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOLORDESC11s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCOLORDESC1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCOLORDESC1" runat="server" ControlToValidate="txtCOLORDESC1" ErrorMessage="Colordesc1 Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCOLORDESC12h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOLORDESC12h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCOLORDESC21s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOLORDESC21s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCOLORDESC2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCOLORDESC2" runat="server" ControlToValidate="txtCOLORDESC2" ErrorMessage="Colordesc2 Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCOLORDESC22h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOLORDESC22h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCOLORREMARKS1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOLORREMARKS1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox TextMode="MultiLine" ID="txtCOLORREMARKS" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCOLORREMARKS" runat="server" ControlToValidate="txtCOLORREMARKS" ErrorMessage="Colorremarks Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCOLORREMARKS2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOLORREMARKS2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblhex1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txthex1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txthex" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblhex2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txthex2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblRGB1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRGB1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtRGB" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblRGB2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRGB2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblcolor1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtcolor1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtcolor" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblcolor2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtcolor2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblActive1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:CheckBox ID="cbActive" runat="server" Checked="true" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblActive2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
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


                            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
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
                                                                                                        <asp:Label runat="server" ID="lblhCOLORDESC1" Text="Colordesc1"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhCOLORDESC2" Text="Colordesc2"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhCOLORREMARKS" Text="Colorremarks"></asp:Label></th>

                                                                                                    <th style="width: 60px;">ACTION</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:ListView ID="Listview" runat="server" OnItemCommand="Listview_ItemCommand1">
                                                                                                    <LayoutTemplate>
                                                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                                                        </tr>
                                                                                                    </LayoutTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>

                                                                                                            <td>
                                                                                                                <asp:Label ID="lblCOLORDESC1" runat="server" Text='<%# Eval("COLORDESC1")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblCOLORDESC2" runat="server" Text='<%# Eval("COLORDESC2")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblCOLORREMARKS" runat="server" Text='<%# Eval("COLORREMARKS")%>'></asp:Label></td>

                                                                                                            <td>
                                                                                                                <table>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("TenantID")+","+Eval("COLORID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("TenantID")+","+Eval("COLORID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
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
                                                            <asp:Label runat="server" ID="lblhCOLORDESC1" Text="Colordesc1"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhCOLORDESC2" Text="Colordesc2"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhCOLORREMARKS" Text="Colorremarks"></asp:Label></th>

                                                        <th style="width: 60px;">ACTION</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview" runat="server" OnItemCommand="Listview_ItemCommand1" DataKeyNames="TenentID, COLORID, COLORDESC1, COLORDESC2, COLORREMARKS, hex, RGB, color, CRUP_ID, Active">
                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>

                                                                <td>
                                                                    <asp:Label ID="lblCOLORDESC1" runat="server" Text='<%# Eval("COLORDESC1")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblCOLORDESC2" runat="server" Text='<%# Eval("COLORDESC2")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblCOLORREMARKS" runat="server" Text='<%# Eval("COLORREMARKS")%>'></asp:Label></td>

                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("TenentID")+","+Eval("COLORID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("TenentID")+","+Eval("COLORID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--  <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
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
