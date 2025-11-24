<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="tblProduct_SubPlan.aspx.cs" Inherits="Web.Master.tblProduct_SubPlan" %>

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
    <div  id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">Product_SubPlan </a>
                    </li>
                </ul>
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
                                                                    <asp:Label runat="server" ID="lblplanid1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtplanid1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpplanid" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblplanid2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtplanid2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%-- </div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label ID="lblName" runat="server" Text="Name" class="col-md-4 control-label"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="Name Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblSubPlanId1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSubPlanId1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtSubPlanId" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblSubPlanId2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSubPlanId2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            <%--</div>
                                                            <div class="row">--%>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblSub_planname11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_planname11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtSub_planname1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblSub_planname12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_planname12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                  </div>
                                                        <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblSub_planname21s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_planname21s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtSub_planname2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSub_planname2" runat="server" ControlToValidate="txtSub_planname2" ErrorMessage="Sub planname2 Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblSub_planname22h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_planname22h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            <%--</div>
                                                            <div class="row">--%>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblSub_planname31s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_planname31s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtSub_planname3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSub_planname3" runat="server" ControlToValidate="txtSub_planname3" ErrorMessage="Sub planname3 Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblSub_planname32h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_planname32h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 </div>
                                                        <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblSub_Plan_cost1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_Plan_cost1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtSub_Plan_cost" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtccount" FilterMode="ValidChars" runat="server" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblSub_Plan_cost2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_Plan_cost2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                           <%-- </div>
                                                            <div class="row">--%>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblSub_Plan_price11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_Plan_price11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtSub_Plan_price1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtSub_Plan_price1" FilterMode="ValidChars" runat="server" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblSub_Plan_price12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_Plan_price12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 </div>
                                                        <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblSub_Plan_price21s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_Plan_price21s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtSub_Plan_price2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtSub_Plan_price2" FilterMode="ValidChars" runat="server" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblSub_Plan_price22h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_Plan_price22h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                           <%-- </div>
                                                            <div class="row">--%>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblSub_Plan_price31s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_Plan_price31s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtSub_Plan_price3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtSub_Plan_price3" FilterMode="ValidChars" runat="server" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblSub_Plan_price32h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_Plan_price32h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 </div>
                                                        <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblccount1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtccount1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtccount" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtccount" FilterMode="ValidChars" runat="server" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblccount2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtccount2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                           <%-- </div>
                                                            <div class="row">--%>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblSub_Plan_sale1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_Plan_sale1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtSub_Plan_sale" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblSub_Plan_sale2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSub_Plan_sale2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                 </div>
                                                        <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblaccount1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtaccount1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtaccount" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtaccount" FilterMode="ValidChars" runat="server" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblaccount2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtaccount2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                                                            <%--<label>Search:<input type="search" class="form-control input-small input-inline" placeholder="" aria-controls="sample_1"></label>--%>
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
                                                                                                            <asp:Label runat="server" ID="lblhplanid" Text="Planid"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhSubPlanId" Text="Subplanid"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhSub_planname1" Text="Sub planname1"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhSub_planname2" Text="Sub planname2"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhSub_planname3" Text="Sub planname3"></asp:Label></th>
                                                                                                       <%-- <th>
                                                                                                            <asp:Label runat="server" ID="lblhSub_Plan_cost" Text="Sub plan cost"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhSub_Plan_price1" Text="Sub plan price1"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhSub_Plan_price2" Text="Sub plan price2"></asp:Label></th>--%>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhSub_Plan_price3" Text="Sub plan price3"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhccount" Text="Ccount"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhSub_Plan_sale" Text="Sub plan sale"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhaccount" Text="Account"></asp:Label></th>

                                                                                                        <th style="width: 60px;">ACTION</th>
                                                                                                    </tr>
                                                                                                </thead>
                                                                                                <tbody>
                                                                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="" DataKeyNames="">
                                                                                                        <LayoutTemplate>
                                                                                                            <tr id="ItemPlaceholder" runat="server">
                                                                                                            </tr>
                                                                                                        </LayoutTemplate>
                                                                                                        <ItemTemplate>
                                                                                                            <tr>

                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblplanid" runat="server" Text='<%# Eval("planid")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblSubPlanId" runat="server" Text='<%# Eval("SubPlanId")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblSub_planname1" runat="server" Text='<%# Eval("Sub_planname1")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblSub_planname2" runat="server" Text='<%# Eval("Sub_planname2")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblSub_planname3" runat="server" Text='<%# Eval("Sub_planname3")%>'></asp:Label></td>
                                                                                                               <%-- <td>
                                                                                                                    <asp:Label ID="lblSub_Plan_cost" runat="server" Text='<%# Eval("Sub_Plan_cost")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblSub_Plan_price1" runat="server" Text='<%# Eval("Sub_Plan_price1")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblSub_Plan_price2" runat="server" Text='<%# Eval("Sub_Plan_price2")%>'></asp:Label></td>--%>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblSub_Plan_price3" runat="server" Text='<%# Eval("Sub_Plan_price3")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblccount" runat="server" Text='<%# Eval("ccount")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblSub_Plan_sale" runat="server" Text='<%# Eval("Sub_Plan_sale")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblaccount" runat="server" Text='<%# Eval("account")%>'></asp:Label></td>

                                                                                                                <td>
                                                                                                                    <table>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("SubPlanId")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                            <td>
                                                                                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("SubPlanId")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
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
    </div>
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
