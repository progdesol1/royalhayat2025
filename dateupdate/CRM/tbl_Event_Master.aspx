<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="tbl_Event_Master.aspx.cs" Inherits="Web.CRM.tbl_Event_Master" %>

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
                        <asp:Label ID="lblMsg" Visible="false" runat="server"></asp:Label>
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
                                            <%--<asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />--%>
                                        </div>
                                        <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="s" Text="Add New" OnClick="btnAdd_Click" />
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
                                                                    <asp:Label runat="server" ID="lblTitle1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTitle1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" MaxLength="49"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Title Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTitle2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTitle2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%-- </div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDescreption1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescreption1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox TextMode="MultiLine" ID="txtDescreption" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDescreption2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescreption2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblSize1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSize1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtSize" runat="server" CssClass="form-control" MaxLength="49"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblSize2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSize2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblKeyWord1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtKeyWord1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtKeyWord" runat="server" CssClass="form-control" MaxLength="49"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblKeyWord2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtKeyWord2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblExetation1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtExetation1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtExetation" runat="server" CssClass="form-control" MaxLength="49"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblExetation2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtExetation2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblRate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtRate" runat="server" CssClass="form-control" MaxLength="49"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblRate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblVisit1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtVisit1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <%--<asp:DropDownList ID="drpVisit" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>--%>
                                                                        <asp:TextBox ID="txtVisit" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtVisit" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblVisit2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtVisit2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--  </div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCounterDounload1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCounterDounload1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <%--<asp:DropDownList ID="drpCounterDounload" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>--%>
                                                                        <asp:TextBox ID="txtCounterDounload" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtCounterDounload" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCounterDounload2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCounterDounload2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCategoryID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCategoryID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpCategoryID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorCategoryID" runat="server" ErrorMessage="Category Name Required." ControlToValidate="drpCategoryID" ValidationGroup="s" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCategoryID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCategoryID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--  </div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblAvtar1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAvtar1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtAvtar" runat="server" CssClass="form-control" MaxLength="49"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblAvtar2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAvtar2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblFileUpload1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtFileUpload1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtFileUpload" runat="server" CssClass="form-control" MaxLength="49"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblFileUpload2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtFileUpload2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                                                                        <asp:Label runat="server" ID="lblhTitle" Text="Title"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhDescreption" Text="Descreption"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhSize" Text="Size"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhKeyWord" Text="Keyword"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhExetation" Text="Exetation"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhRate" Text="Rate"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhVisit" Text="Visit"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhCounterDounload" Text="Counter DownLoad"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhCategoryID" Text="Category Name"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhAvtar" Text="Avtar"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhFileUpload" Text="File Upload"></asp:Label></th>

                                                                                                    <th style="width: 60px;">ACTION</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" >
                                                                                                    
                                                                                                    <LayoutTemplate>
                                                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                                                        </tr>
                                                                                                    </LayoutTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>

                                                                                                            <td>
                                                                                                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblDescreption" runat="server" Text='<%# Eval("Descreption")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblSize" runat="server" Text='<%# Eval("Size")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblKeyWord" runat="server" Text='<%# Eval("KeyWord")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblExetation" runat="server" Text='<%# Eval("Exetation")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblVisit" runat="server" Text='<%# Eval("Visit")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblCounterDounload" runat="server" Text='<%# Eval("CounterDounload")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblCategoryID" runat="server" Text='<%# Eval("CategoryID")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblAvtar" runat="server" Text='<%# Eval("Avtar")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblFileUpload" runat="server" Text='<%# Eval("FileUpload")%>'></asp:Label></td>

                                                                                                            <td>
                                                                                                                <table>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                                                    
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

                                    <div class="portlet box blue">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-edit"></i>
                                                <asp:Label ID="LableList" runat="server" Text="Label"></asp:Label>
                                                List
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <%--<div class="table-toolbar">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="btn-group">
                                                        <button id="sample_editable_1_new" class="btn green">
                                                            Add New <i class="fa fa-plus"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="btn-group pull-right">
                                                        <button class="btn dropdown-toggle" data-toggle="dropdown">
                                                            Tools <i class="fa fa-angle-down"></i>
                                                        </button>
                                                        <ul class="dropdown-menu pull-right">
                                                            <li>
                                                                <a href="javascript:;">Print </a>
                                                            </li>
                                                            <li>
                                                                <a href="javascript:;">Save as PDF </a>
                                                            </li>
                                                            <li>
                                                                <a href="javascript:;">Export to Excel </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>
                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhTitle" Text="Title"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhDescreption" Text="Descreption"></asp:Label></th>
                                                       <%-- <th>
                                                            <asp:Label runat="server" ID="lblhSize" Text="Size"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhKeyWord" Text="Keyword"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhExetation" Text="Exetation"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhRate" Text="Rate"></asp:Label></th>--%>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhVisit" Text="Visit"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhCounterDounload" Text="Counter DownLoad"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhCategoryID" Text="Category Name"></asp:Label></th>
                                                      <%--  <th>
                                                            <asp:Label runat="server" ID="lblhAvtar" Text="Avtar"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhFileUpload" Text="File Upload"></asp:Label></th>--%>

                                                        <th style="width: 60px;">ACTION</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" Datakey="ID,Title,Descreption,Size,KeyWord,Exetation,Rate,Visit,CounterDounload,CategoryID,Avtar,FileUpload,Deleted,Activated,CreatedBy,CreatedDate" DataKeyNames="ID,Title,Descreption,Size,KeyWord,Exetation,Rate,Visit,CounterDounload,CategoryID,Avtar,FileUpload,Deleted,Activated,CreatedBy,CreatedDate">

                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblDescreption" runat="server" Text='<%# Eval("Descreption")%>'></asp:Label></td>
                                                                <%--<td>
                                                                    <asp:Label ID="lblSize" runat="server" Text='<%# Eval("Size")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblKeyWord" runat="server" Text='<%# Eval("KeyWord")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblExetation" runat="server" Text='<%# Eval("Exetation")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate")%>'></asp:Label></td>--%>
                                                                <td>
                                                                    <asp:Label ID="lblVisit" runat="server" Text='<%# Eval("Visit")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblCounterDounload" runat="server" Text='<%# Eval("CounterDounload")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblCategoryID" runat="server" Text='<%#gatcategory(Convert.ToInt32(Eval("CategoryID"))) %>'></asp:Label></td>
                                                                <%--<td>
                                                                    <asp:Label ID="lblAvtar" runat="server" Text='<%# Eval("Avtar")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblFileUpload" runat="server" Text='<%# Eval("FileUpload")%>'></asp:Label></td>--%>

                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>

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
    <%-- </div>--%>

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
