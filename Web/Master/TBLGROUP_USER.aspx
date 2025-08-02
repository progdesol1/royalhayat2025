<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="TBLGROUP_USER.aspx.cs" Inherits="Web.Master.TBLGROUP_USER" %>

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
                <ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">TBLGROUP_USER </a>
                    </li>
                </ul>
                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <asp:Panel ID="PanelErrorMsg" runat="server" Visible="false">
                    <div class="alert alert-danger alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblErrormsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <div class="row">
                    <div class="col-md-6">
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
                                                                    <asp:Label runat="server" ID="lblGroupname1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtGroupname1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtGroupname" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorGroupname" runat="server" ControlToValidate="txtGroupname" ErrorMessage="Group name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblGroupname2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtGroupname2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>



                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblInfastructure1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtInfastructure1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:CheckBox ID="cbInfastructure" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblInfastructure2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtInfastructure2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>



                                                        <div class="col-md-12">
                                                            <div class="portlet box yellow">
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
                                                                                               
                                                                                                    <div class="table-scrollable">

                                                                                                        <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info" id="sample_2">
                                                                                                            <thead>
                                                                                                                <tr>

                                                                                                                    <th>
                                                                                                                        <asp:Label runat="server" ID="lblhGroupname" Text="Group name"></asp:Label></th>
                                                                                                                    <%--<th>
                                                                                                            <asp:Label runat="server" ID="lblhUSERCODE" Text="User code"></asp:Label></th>--%>
                                                                                                                    <th>
                                                                                                                        <asp:Label runat="server" ID="lblhInfastructure" Text="Infastructure"></asp:Label></th>
                                                                                                                    <%--<th>
                                                                                                            <asp:Label runat="server" ID="lblhGroupname1" Text="Group name 1"></asp:Label></th>--%>

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
                                                                                                                                <asp:Label ID="lblGroupname" runat="server" Text='<%# Eval("ITGROUPDESC1")%>'></asp:Label></td>
                                                                                                                            <%-- <td>
                                                                                                                    <asp:Label ID="lblUSERCODE" runat="server" Text='<%# Eval("USERCODE")%>'></asp:Label></td>--%>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="lblInfastructure" runat="server" Text='<%# Eval("Infastructure")%>'></asp:Label></td>
                                                                                                                            <%-- <td>
                                                                                                                    <asp:Label ID="lblGroupname1" runat="server" Text='<%# Eval("Groupname1")%>'></asp:Label></td>--%>

                                                                                                                            <td>
                                                                                                                                <table>
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("ITGROUPID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                                        <td>
                                                                                                                                            <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("ITGROUPID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                                                                        <%-- <td><asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("JobId")%>' CommandName="btnPrint" CommandArgument='<%# Eval("JobId")%>' runat="server" class="btn btn-sm green filter-submit margin-bottom"><i class="fa fa-print"></i></asp:LinkButton></td>--%>
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
                                                                                <asp:HiddenField ID="hideID" runat="server" Value="" />
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <asp:Label runat="server" ID="lblTenentId2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenentId2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>



                                    </div>
                                    <asp:Label runat="server" ID="lblLocationId2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLocationId2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                    <div class="col-md-6">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Add 
                                        <asp:Label runat="server" ID="Label1" Text="Group User"></asp:Label>
                                        <asp:TextBox Style="color: #333333" ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="LinkButton1" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">

                                        <asp:Button ID="btnAddUser" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="AddNew" OnClick="btnAddUser_Click" />
                                        <asp:Button ID="BtnCancelUser" runat="server" class="btn green-haze btn-circle" Text="Cancel" OnClick="BtnCancelUser_Click" />
                                       
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
                                                                    <asp:Label runat="server" ID="lblGroupname11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtGroupname11s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpGroupname1" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="drpGroupname1_SelectedIndexChanged" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorGroupname1" runat="server" ControlToValidate="drpGroupname1" ErrorMessage="Group name 1 Required." CssClass="Validation" ValidationGroup="s1"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblGroupname12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtGroupname12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUSERCODE1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUSERCODE1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpUSERCODE" Enabled="false" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUSERCODE" runat="server" ControlToValidate="drpUSERCODE" ErrorMessage="User code Required." CssClass="Validation" ValidationGroup="s1"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUSERCODE2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUSERCODE2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>



                                                        <div class="col-md-12">
                                                            <div class="portlet box yellow">
                                                                <div class="portlet-title">
                                                                    <div class="caption">
                                                                        <i class="fa fa-gift"></i>
                                                                        <asp:Label runat="server" ID="Label11" Text="Group User"></asp:Label>
                                                                        List
                                                                    </div>
                                                                    <div class="tools">
                                                                        <a href="javascript:;" class="collapse"></a>
                                                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                        <asp:LinkButton ID="LinkButton5" OnClick="btnlistreload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>

                                                                        <a href="javascript:;" class="remove"></a>
                                                                    </div>
                                                                </div>
                                                                <div class="portlet-body form">
                                                                    <asp:Panel runat="server" ID="Panel1">
                                                                        <div class="tab-content">
                                                                            <div id="tab_1_1" class="tab-pane active">

                                                                                <div class="tab-content no-space">
                                                                                    <div class="tab-pane active" id="tab_general2">
                                                                                        <div class="table-container" style="">




                                                                                            <div class="portlet-body">


                                                                                                <div class="table-scrollable">

                                                                                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_wrapper" id="sample_1">
                                                                                                        <thead>

                                                                                                            <tr>

                                                                                                                        <th>
                                                                                                                            <asp:Label runat="server" ID="Label12" Text="Group name"></asp:Label></th>
                                                                                                                        <%--<th>
                                                                                                            <asp:Label runat="server" ID="lblhUSERCODE" Text="User code"></asp:Label></th>--%>
                                                                                                                        <th>
                                                                                                                            <asp:Label runat="server" ID="Label13" Text="User Name"></asp:Label></th>
                                                                                                                        <%--<th>
                                                                                                            <asp:Label runat="server" ID="lblhGroupname1" Text="Group name 1"></asp:Label></th>--%>

                                                                                                                        <th style="width: 60px;">ACTION</th>
                                                                                                                    </tr>

                                                                                                        </thead>
                                                                                                        <tbody>                                                                                                                                                                                                                        
                                                                                                                <asp:ListView ID="Listview2" runat="server" OnItemCommand="Listview2_ItemCommand">
                                                                                                                <ItemTemplate>
                                                                                                                    <tr>

                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblGroupname" runat="server" Text='<%# getgroupName(Convert.ToInt32(Eval("ITGROUPID")))%>'></asp:Label></td>
                                                                                                                        <%-- <td>
                                                                                                                    <asp:Label ID="lblUSERCODE" runat="server" Text='<%# Eval("USERCODE")%>'></asp:Label></td>--%>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblInfastructure" runat="server" Text='<%# getUsername(Convert.ToInt32(Eval("USERCODE")))%>'></asp:Label></td>
                                                                                                                        <%-- <td>
                                                                                                                    <asp:Label ID="lblGroupname1" runat="server" Text='<%# Eval("Groupname1")%>'></asp:Label></td>--%>

                                                                                                                        <td>
                                                                                                                            <table>
                                                                                                                                <tr>
                                                                                                                                    <%--<td>
                                                                                                                                        <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("ITGROUPID")+","+Eval("USERCODE") %>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>--%>
                                                                                                                                    <td>
                                                                                                                                        <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("ITGROUPID")+","+Eval("USERCODE") %>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                                                                    <%-- <td><asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("JobId")%>' CommandName="btnPrint" CommandArgument='<%# Eval("JobId")%>' runat="server" class="btn btn-sm green filter-submit margin-bottom"><i class="fa fa-print"></i></asp:LinkButton></td>--%>
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
                                                                                <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <asp:Label runat="server" ID="Label14" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox11" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>



                                    </div>
                                    <asp:Label runat="server" ID="Label15" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox12" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>




                    </div>

                </div>
                </div>
            </div>
        </div>
    </div>









</asp:Content>
