<%@ Page Title="" Language="C#" MasterPageFile="~/POS/POSMaster.Master" AutoEventWireup="true" CodeBehind="Win_tbl_customer.aspx.cs" Inherits="Web.POS.Win_tbl_customer" %>

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
    <div class="page-content" id="b" runat="server">
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
                                            <%-- <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />--%>
                                        </div>
                                        <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="Add Customer" OnClick="btnAdd_Click" />
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
                                                                    <asp:Label runat="server" ID="lblName1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Name Required" ControlToValidate="txtName" ValidationGroup="CRD"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtName2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblNameArabic1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNameArabic1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtNameArabic" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name ARABIC Required" ControlToValidate="txtNameArabic" ValidationGroup="CRD"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblNameArabic2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNameArabic2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblPhone1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPhone1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Phone NO Required" ControlToValidate="txtPhone" ValidationGroup="CRD"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderMobileNo" ValidChars="1234567890" TargetControlID="txtPhone" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPhone2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPhone2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                       <%-- </div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblEmailAddress1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEmailAddress1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Email Required" ControlToValidate="txtEmailAddress" ValidationGroup="CRD"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblEmailAddress2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEmailAddress2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblPeopleType1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPeopleType1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpPeopleType" runat="server" CssClass="table-group-action-input form-control input-medium">
                                                                            <asp:ListItem Text="Customer" Value="Customer"></asp:ListItem>
                                                                            <asp:ListItem Text="Supplier" Value="Supplier"></asp:ListItem>
                                                                            <asp:ListItem Text="Biller" Value="Biller"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPeopleType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPeopleType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                       <%-- </div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCity1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCity1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator5" runat="server" ErrorMessage="City Required" ControlToValidate="txtCity" ValidationGroup="CRD"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCity2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCity2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblAddress1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAddress1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator6" runat="server" ErrorMessage="City Required" ControlToValidate="txtAddress" ValidationGroup="CRD"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblAddress2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAddress2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                       <%-- </div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblFacebook1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtFacebook1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtFacebook" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblFacebook2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtFacebook2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTwitter1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTwitter1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtTwitter" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTwitter2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTwitter2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblInsta1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtInsta1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtInsta" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblInsta2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtInsta2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                <asp:LinkButton ID="btnlistreload" OnClick="btnlistreload_Click" runat="server"><img src="assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
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
                                                                                                        <asp:Label runat="server" ID="lblhName" Text="Name"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhEmailAddress" Text="Email Address"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhPhone" Text="Phone"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhAddress" Text="Address"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhCity" Text="City"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhPeopleType" Text="People Type"></asp:Label></th>

                                                                                                    <th style="width: 60px;">ACTION</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="ID" DataKeyNames="ID">
                                                                                                    <LayoutTemplate>
                                                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                                                        </tr>
                                                                                                    </LayoutTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Eval("EmailAddress")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblPeopleType" runat="server" Text='<%# Eval("PeopleType")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <table>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("JobId")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("JobId")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>                                                                                                                       
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
                                                            <asp:Label runat="server" ID="lblhName" Text="Name"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhEmailAddress" Text="Email Address"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhPhone" Text="Phone"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhAddress" Text="Address"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhCity" Text="City"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhPeopleType" Text="People Type"></asp:Label></th>

                                                        <th style="width: 60px;">ACTION</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="ID" DataKeyNames="ID">
                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Eval("EmailAddress")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblPeopleType" runat="server" Text='<%# Eval("PeopleType")%>'></asp:Label></td>
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
