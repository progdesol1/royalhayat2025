<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="ACM_USER_ROLE.aspx.cs" Inherits="Web.ACM.ACM_USER_ROLE" %>

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
    <asp:UpdatePanel ID="UpdFullpage" runat ="server" ><ContentTemplate>
   
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
                                                                            <%--<asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Tenant Required." ControlToValidate="DrpTENANT_ID" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="Label3" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox3" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label4" class="col-md-4 control-label">Location ID</asp:Label><asp:TextBox runat="server" ID="TextBox4" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drplocation" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drplocation_SelectedIndexChanged"></asp:DropDownList>
                                                                            <%--<asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Tenant Required." ControlToValidate="DrpTENANT_ID" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="Label6" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox5" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                            <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="AddNew" OnClick="btnAdd_Click" />
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
                                                                        <asp:Label runat="server" ID="lblPRIVILEGE_ID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPRIVILEGE_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpPRIVILEGE_ID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblPRIVILEGE_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPRIVILEGE_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblUSER_ID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUSER_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpUSER_ID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblUSER_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUSER_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblROLE_ID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtROLE_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpROLE_ID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblROLE_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtROLE_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblACTIVE_FLAG1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtACTIVE_FLAG1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:CheckBox ID="cbACTIVE_FLAG" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblACTIVE_FLAG2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtACTIVE_FLAG2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblACTIVE_TO_DT1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtACTIVE_TO_DT1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtACTIVE_TO_DT" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxACTIVE_TO_DT_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtACTIVE_TO_DT" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorACTIVE_TO_DT" runat="server" ControlToValidate="txtACTIVE_TO_DT" ErrorMessage="Active To DT Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" TargetControlID="txtACTIVE_TO_DT" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblACTIVE_TO_DT2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtACTIVE_TO_DT2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblACTIVE_FROM_DT1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtACTIVE_FROM_DT1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtACTIVE_FROM_DT" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxACTIVE_FROM_DT_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtACTIVE_FROM_DT" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorACTIVE_FROM_DT" runat="server" ControlToValidate="txtACTIVE_FROM_DT" ErrorMessage="Active From DT Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" TargetControlID="txtACTIVE_FROM_DT" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblACTIVE_FROM_DT2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtACTIVE_FROM_DT2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                           
                                                            <div class="form-group" >
                                                            <asp:Panel ID ="menulistpnl" class="checkbox-list" runat ="server" Visible="false">
                                                                <label style ="color :blue">Select Menu</label>
                                                            <asp:ListView ID="listmenu" runat="server">
                                                                <ItemTemplate>
                                                                    <div  class="checkbox-inline">
                                                                    <tr class="gradeA">
                                                                        <td>
                                                                           <asp:CheckBox ID="cbkMenu" runat="server" Text='<%# Eval("MENU_NAME1") %>'  />
                                                                            <asp:Label ID="lblMenuId" Visible="false"  runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                        </td>
                                                                       
                                                                    </tr></div>
                                                                </ItemTemplate>
                                                            </asp:ListView></asp:Panel> </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
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
                                                                                                <%--<select name="sample_1_length" aria-controls="sample_1"  tabindex="-1" title="">
                                                                                            <option value="5">5</option>
                                                                                            <option value="15">15</option>
                                                                                            <option value="20">20</option>
                                                                                            <option value="-1">All</option>
                                                                                        </select>--%>
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
                                                                                                            <asp:Label runat="server" ID="lblhUSER_ROLE_ID" Text="User Role ID"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhUSER_ID" Text="User  ID"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhROLE_ID" Text="Role ID"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhACTIVE_FROM_DT" Text="Active From DT"></asp:Label></th>

                                                                                                        <th style="width: 60px;">ACTION</th>
                                                                                                    </tr>
                                                                                                </thead>
                                                                                                <tbody>
                                                                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="TENANT_ID,USER_ROLE_ID,PRIVILEGE_ID,USER_ID,ROLE_ID,ACTIVE_FLAG,ACTIVE_FROM_DT,ACTIVE_TO_DT,CRUP_ID" DataKeyNames="TENANT_ID,USER_ROLE_ID,PRIVILEGE_ID,USER_ID,ROLE_ID,ACTIVE_FLAG,ACTIVE_FROM_DT,ACTIVE_TO_DT,CRUP_ID">
                                                                                                        <LayoutTemplate>
                                                                                                            <tr id="ItemPlaceholder" runat="server">
                                                                                                            </tr>
                                                                                                        </LayoutTemplate>
                                                                                                        <ItemTemplate>
                                                                                                            <tr>

                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblUSER_ROLE_ID" runat="server" Text='<%# Eval("USER_ROLE_ID")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblUSER_ID" runat="server" Text='<%# Eval("USER_ID")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblROLE_ID" runat="server" Text='<%# Eval("ROLE_ID")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblACTIVE_FROM_DT" runat="server" Text='<%# Eval("ACTIVE_FROM_DT")%>'></asp:Label></td>

                                                                                                                <td>
                                                                                                                    <table>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("USER_ROLE_ID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                            <td>
                                                                                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("USER_ROLE_ID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                                                            <%-- <td><asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("JobId")%>' CommandName="btnPrint" CommandArgument='<%# Eval("JobId")%>' runat="server" class="btn btn-sm green filter-submit margin-bottom"><i class="fa fa-print"></i></asp:LinkButton></td>--%>
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
                                                                                            <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                                <ContentTemplate>--%>
                                                                                            <ul class="pagination">
                                                                                                <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_fist">
                                                                                                    <%--  <asp:LinkButton ID="Button1" runat="server"  BorderStyle="None" />First</asp:LinkButton> --%>
                                                                                                    <asp:LinkButton ID="btnfirst1" OnClick="btnfirst_Click" runat="server"> First</asp:LinkButton>
                                                                                                </li>
                                                                                                <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_Next">
                                                                                                    <%--  <asp:LinkButton ID="Button1" runat="server"  BorderStyle="None" />First</asp:LinkButton> --%>
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
                                                                                            <%--  </ContentTemplate>
                                                                                            </asp:UpdatePanel>--%>
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

                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate></asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text=" Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="assets/admin/layout4/img/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>


</asp:Content>
