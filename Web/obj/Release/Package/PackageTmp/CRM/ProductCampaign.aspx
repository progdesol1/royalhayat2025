<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="ProductCampaign.aspx.cs" Inherits="Web.CRM.ProductCampaign" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <div class="portlet box green-jungle" style="background-color: snow;">
                    <div class="portlet-title">
                        <div class="caption">
                            Product Based Campaign  <span class="step-title"></span>
                        </div>
                        <div class="tools">
                            <a href="javascript:;" id="A1" runat="server" class="collapse"></a>
                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                            <a href="javascript:;" class="reload"></a>
                            <a href="javascript:;" class="remove"></a>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 7px;">
                        <%--Row1--%>
                        <div class="col-md-12" style="padding-left: 15px;">
                            <div class="form-horizontal form-row-seperated">
                                <div class="portlet box blue" id="form_wizard_1">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <asp:Label ID="lblgo" runat="server"></asp:Label>
                                            Goal  <span class="step-title"></span>
                                        </div>
                                        <div class="tools hidden-xs">
                                            <a href="javascript:;" id="pack1" runat="server" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <a href="javascript:;" class="reload"></a>
                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                    </div>
                                    <div class="portlet-body form" id="body1" runat="server" style="display: block;">
                                        <div class="form-wizard">
                                            <div class="form-body">
                                                <h4 class="block" style="font-family: 'Courier New'; font-weight: bold; margin-top: 0px; margin-bottom: 0px;">Number of items a customer needs to purchase to get rewarded</h4>
                                                <hr style="margin-top: 0px; margin-bottom: 10px;" />

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-4">
                                                                Campaign Start Date  <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtSTDate" runat="server" CssClass="form-control" Placeholder="Campaign Start date" AutoPostBack="true" OnTextChanged="txtSTDate_TextChanged"></asp:TextBox>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select End Date." ControlToValidate="txtSTDate" ValidationGroup="Desire" InitialValue="0"></asp:RequiredFieldValidator>
                                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtSTDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="margin-left:0px;margin-right:0px;">
                                                            <asp:Label ID="lblmsg1" runat="server" Visible="false" ForeColor="#a94442" Font-Bold="true" Font-Size="Large" Text=""></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-12">
                                                                Reedem Reward Until ?   Campaign Expiration Date <span class="required">*</span>
                                                            </label>
                                                            <div class="col-md-12">
                                                                <div class="col-md-8 pull-right" style="padding: 0px 0px 0px 7px">
                                                                    <asp:TextBox ID="txtEDDate" runat="server" CssClass="form-control" Placeholder="Campaign End Date or expiry Date" AutoPostBack="true" OnTextChanged="txtEDDate_TextChanged"></asp:TextBox>

                                                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtEDDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                    <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Expiration Date Required" ControlToValidate="txtEDDate" ValidationGroup="Desire" InitialValue="0"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-4">
                                                                Goal
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpproductgoal" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpproductgoal_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0" Text="Select Quantity"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                                    <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                                    <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                                    <asp:ListItem Value="11" Text="Custom Quantity"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<span class="help-block">Social Media Required.</span>--%>
                                                                <asp:RequiredFieldValidator CssClass="Validation" Style="color: #FF0000;" ID="RequiredFieldValidator8" runat="server" ErrorMessage="Select Goal." ControlToValidate="drpproductgoal" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3" style="padding-left: 20px;">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtcustom" runat="server" CssClass="form-control input-small" Visible="false"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtcustom" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-4">
                                                                Campaign Name
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator CssClass="Validation" Style="color: #FF0000;" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campaign Name required" ControlToValidate="txtname" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-4">
                                                                Purchase Type <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:RadioButtonList ID="RDO1" CssClass="radio-list" runat="server" OnSelectedIndexChanged="RDO1_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="1" Text="Specific Product" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Multiple Product"></asp:ListItem>
                                                                    <asp:ListItem Value="3" Text="Category"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:RequiredFieldValidator CssClass="Validation" Style="color: #FF0000;" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Product type required" ControlToValidate="RDO1" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr style="margin-top: 0px; margin-bottom: 10px;" />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <div class="col-md-4">
                                                                <%-- <asp:TextBox ID="txtcat" runat="server" CssClass="form-control" placeholder="Search Category" Visible="false"></asp:TextBox>--%>
                                                                <asp:DropDownList ID="DRPCatSearch" CssClass="form-control" runat="server" Visible="false"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:LinkButton ID="lnksearches" CssClass="btn btn-info" Visible="false" OnClick="lnksearches_Click" runat="server">Search</asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="drpcat" runat="server" CssClass="form-control" Visible="false">
                                                                </asp:DropDownList>
                                                                <%--  <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select Category." ControlToValidate="drpcat" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                                <%--<asp:Button ID="btnsub" runat="server" Text="Submit" OnClick="btnsub_Click" CssClass="btn btn-info" Visible="false" />--%>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:LinkButton ID="lblsubcat" runat="server" OnClick="lblsubcat_Click" CssClass="btn btn-info" Visible="false">Add</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label2" Text="Product Name"></asp:Label></th>
                                                                        <th style="width: 60px;">ACTION</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:ListView ID="Listview3" runat="server" OnItemCommand="Listview3_ItemCommand">
                                                                        <LayoutTemplate>
                                                                            <tr id="ItemPlaceholder" runat="server">
                                                                            </tr>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblProdName" runat="server" Text='<%# Eval("ProdName1")%>'></asp:Label></td>
                                                                                <asp:Label ID="lblPID" Visible="false" runat="server" Text='<%# Eval("MYPRODID") %>'></asp:Label>
                                                                                <asp:Label ID="lbltype" Visible="false" runat="server" Text='<%# Eval("MainCategoryID") %>'></asp:Label>

                                                                                <td>
                                                                                    <table>
                                                                                        <tr>

                                                                                            <td>
                                                                                                <asp:LinkButton ID="btnselect" CommandName="btnRemove" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("MYPRODID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                            <%--  <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("MYPRODID")%>' runat="server"  class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>--%>
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
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control" placeholder="Search Product by name" Visible="false"></asp:TextBox>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select Status." ControlToValidate="txtsearch" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtsku" runat="server" CssClass="form-control" placeholder="Search by SKU" type="text" Visible="false"></asp:TextBox>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select Status." ControlToValidate="txtsku" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:LinkButton ID="lnkserxh" runat="server" Visible="false" OnClick="lnkserxh_Click" CssClass="btn btn-info">Search</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="false">
                                                            <ContentTemplate>
                                                                <table class="table table-striped table-bordered table-hover dataTable no-footer" style="border: 1px solid #F68D90;" role="grid" aria-describedby="sample_1_info">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="Label1" Text="Product Name"></asp:Label></th>
                                                                            <th style="width: 60px;">ACTION</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:ListView ID="Listview2" runat="server" OnItemCommand="Listview2_ItemCommand" Visible="false">
                                                                            <LayoutTemplate>
                                                                                <tr id="ItemPlaceholder" runat="server">
                                                                                </tr>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblProdName" runat="server" Text='<%# Eval("ProdName1")%>'></asp:Label>
                                                                                        <asp:Label ID="lblPID" Visible="false" runat="server" Text='<%# Eval("MYPRODID") %>'></asp:Label>
                                                                                        <asp:Label ID="Label3" Visible="false" runat="server" Text='<%# Eval("MainCategoryID") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <table>
                                                                                            <tr>

                                                                                                <td>
                                                                                                    <asp:LinkButton ID="btnselect" CommandName="btnselect" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("MYPRODID")%>' runat="server" class="btn btn-sm red filter-cancel" Text="Select"></asp:LinkButton></td>
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
                                                    <%--  <asp:Button ID="btnsbs" runat="server" Text="Submit" CssClass="btn btn-info" Visible="false" OnClick="btnsbs_Click" />--%>
                                                </div>
                                                <asp:Panel ID="panelMsg" runat="server" Visible="false">
                                                    <div class="alert alert-danger alert-dismissable">
                                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                        <asp:Label ID="lblErreorMsg" runat="server"></asp:Label>
                                                    </div>
                                                </asp:Panel>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtskud" runat="server" Visible="false" CssClass="form-control" placeholder="Search"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:LinkButton ID="lnksub" runat="server" OnClick="lnksub_Click" CssClass="btn btn-info" Visible="false">Search</asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:DropDownList ID="drpcategory" runat="server" CssClass="form-control" Visible="false">
                                                                </asp:DropDownList>

                                                                <%-- <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Category." ControlToValidate="drpcategory" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:LinkButton ID="lnkdAdd" runat="server" OnClick="lnkdAdd_Click" CssClass="btn btn-info" Visible="false">Add</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="false">
                                                            <ContentTemplate>
                                                                <table class="table table-striped table-bordered table-hover dataTable no-footer" style="border: 1px solid #F68D90;" role="grid" aria-describedby="sample_1_info">
                                                                    <thead>
                                                                        <tr>

                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhemp_birthday" Text="Product Name"></asp:Label></th>


                                                                            <th style="width: 60px;">ACTION</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand1">
                                                                            <LayoutTemplate>
                                                                                <tr id="ItemPlaceholder" runat="server">
                                                                                </tr>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblProdName" runat="server" Text='<%# Eval("ProdName1")%>'></asp:Label>
                                                                                        <asp:Label ID="lblPID" Visible="false" runat="server" Text='<%# Eval("MYPRODID") %>'></asp:Label>
                                                                                        <asp:Label ID="lbltype" Visible="false" runat="server" Text='<%# Eval("MainCategoryID") %>'></asp:Label>
                                                                                    </td>

                                                                                    <td>
                                                                                        <table>
                                                                                            <tr>

                                                                                                <td>
                                                                                                    <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("MYPRODID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
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
                                                    <%--    <asp:Button ID="btnsubmit" runat="server" Text="Submit" Visible="false" CssClass="btn btn-info" OnClick="btnsubmit_Click" />--%>
                                                </div>

                                                <div class="col-md-offset-3 col-md-9">
                                                    <a href="javascript:;" class="btn blue button-previous">
                                                        <i class="m-icon-swapleft"></i>Back </a>
                                                    <asp:LinkButton ID="btnsave" runat="server" CssClass="btn btn-info" ValidationGroup="submit" OnClick="btnsave_Click1">Save as Draft</asp:LinkButton>
                                                    <asp:LinkButton ID="lnkjnext" runat="server" ValidationGroup="submit" CssClass="btn btn-info" OnClick="lnkjnext_Click">Next</asp:LinkButton>
                                                </div>
                                                <div id="bar4" class="progress progress-striped" role="progressbar">
                                                    <div class="progress-bar progress-bar-success">
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
                        <%--Row2--%>
                        <div class="col-md-12" style="padding-left: 15px;">
                            <div class="form-horizontal form-row-seperated">
                                <div class="portlet box red" id="form_wizard_2">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <asp:Label ID="lblgoals" runat="server"></asp:Label>
                                            <asp:Label ID="lblrewa" runat="server"></asp:Label>
                                            Reward  <span class="step-title"></span>
                                        </div>
                                        <div class="tools hidden-xs">
                                            <a href="javascript:;" id="pack2" runat="server" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <a href="javascript:;" class="reload"></a>
                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                    </div>
                                    <div class="portlet-body form" id="body2" runat="server" style="display: block;">
                                        <div class="form-wizard">
                                            <div class="form-body">
                                                <h4 class="block" style="font-family: 'Courier New'; font-weight: bold; margin-top: 0px; margin-bottom: 0px;">Purchase Type</h4>
                                                <hr style="margin-top: 0px; margin-bottom: 10px;" />
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-4">Reward Type <span class="required">* </span></label>

                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drprewards" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drprewards_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0" Text="Select Reward"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="Item for Free"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Discount"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Select Reward type." ControlToValidate="drprewards" ValidationGroup="Breathe" InitialValue="0"></asp:RequiredFieldValidator>
                                                                <asp:Label ID="lblreward" runat="server" Visible="false" Style="color: #FF0000;"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="drppercentage" Visible="false" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drppercentage_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0" Text="Select Percentage"></asp:ListItem>
                                                                    <asp:ListItem Value="5" Text="5%"></asp:ListItem>
                                                                    <asp:ListItem Value="10" Text="10%"></asp:ListItem>
                                                                    <asp:ListItem Value="15" Text="15%"></asp:ListItem>
                                                                    <asp:ListItem Value="20" Text="20%"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="Custom Percentage %"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator9" runat="server" ErrorMessage="Select Percentage." ControlToValidate="drppercentage" ValidationGroup="Breathe" InitialValue="0"></asp:RequiredFieldValidator>
                                                                <asp:Label ID="lblper" runat="server" Visible="false" Style="color: #FF0000;"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtCustomPercentage" CssClass="form-control" Visible="false" runat="server"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtCustomPercentage" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-6">
                                                                Can a customer participate in this campaign more than once?  <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:RadioButtonList ID="rdo2" runat="server">
                                                                    <asp:ListItem Value="Y" Text=" Yes , Multiple Times"></asp:ListItem>
                                                                    <asp:ListItem Value="N" Text="No , Just once per customer"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator10" runat="server" ErrorMessage="Select Redemption." ControlToValidate="rdo2" ValidationGroup="Case" InitialValue="0"></asp:RequiredFieldValidator>
                                                                <asp:Label ID="lblmultiple" runat="server" Visible="false" Style="color: #FF0000;"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr style="margin-top: 0px; margin-bottom: 10px;" />
                                                <div class="row">
                                                    <div class="col-md-offset-3 col-md-9">
                                                        <a href="javascript:;" class="btn blue button-previous">
                                                            <i class="m-icon-swapleft"></i>Back </a>
                                                        <asp:LinkButton ID="lnkreawrd" runat="server" ValidationGroup="Breathe" CssClass="btn btn-info" OnClick="lnkreawrd_Click">save as draft</asp:LinkButton>
                                                        <asp:LinkButton ID="lblnext1" runat="server" ValidationGroup="Breathe" CssClass="btn btn-info" OnClick="lblnext1_Click">Next</asp:LinkButton>
                                                    </div>
                                                </div>

                                                <div id="bars" class="progress progress-striped" role="progressbar">
                                                    <div class="progress-bar progress-bar-success">
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
                        <%--Row3--%>
                        <div class="col-md-12" style="padding-left: 15px;">
                            <div class="form-horizontal form-row-seperated">
                                <div class="portlet box green" id="form_wizard_4">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <asp:Label ID="lblending" runat="server"></asp:Label>
                                            Setting <span class="step-title"></span>
                                        </div>
                                        <div class="tools hidden-xs">
                                            <a href="javascript:;" id="pack3" runat="server" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <a href="javascript:;" class="reload"></a>
                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                    </div>
                                    <div class="portlet-body form" id="body3" runat="server" style="display: block;">
                                        <div class="form-wizard">
                                            <div class="form-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-4">
                                                                Campaign Start Date  <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control" Placeholder="Campaign end date" Enabled="false"></asp:TextBox>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator11" runat="server" ErrorMessage="Select End Date." ControlToValidate="txtenddate" ValidationGroup="Desire" InitialValue="0"></asp:RequiredFieldValidator>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtenddate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-12">
                                                                Reedem Reward Until ?   Campaign Expiration Date <span class="required">*</span>
                                                            </label>
                                                            <div class="col-md-12">
                                                                <div class="col-md-8 pull-right" style="padding: 0px 0px 0px 7px">
                                                                    <asp:TextBox ID="txtexpirydate" runat="server" CssClass="form-control" Placeholder="Campaign End Date or expiry Date" Enabled="false"></asp:TextBox>

                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtexpirydate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                    <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator12" runat="server" ErrorMessage="Expiration Date Required" ControlToValidate="txtexpirydate" ValidationGroup="Desire" InitialValue="0"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--<div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-4">
                                                                Reedem Reward Until ? <span class="required">* </span>
                                                            </label>
                                                            <br />
                                                            <div class="col-md-8">
                                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                                                    <asp:ListItem Value="1" Text="Campaign Expiration Date(31 / 7 / 2018)"></asp:ListItem>
                                                                    <asp:ListItem></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:TextBox ID="txtexpirydate" runat="server" CssClass="form-control" Text="31 / 7 / 2018" Style="margin-left: 16px; margin-top: -25px;"></asp:TextBox>

                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtexpirydate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator12" runat="server" ErrorMessage="Expiration Date Required" ControlToValidate="txtexpirydate" ValidationGroup="Desire" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-4">Campaign Status <span class="required">* </span></label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpstatus" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Pause"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator13" runat="server" ErrorMessage="Select Status." ControlToValidate="drpstatus" ValidationGroup="Desire" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-offset-3 col-md-9">
                                                        <a href="javascript:;" class="btn blue button-previous">
                                                            <i class="m-icon-swapleft"></i>Back </a>
                                                        <asp:LinkButton ID="lnksavecampaign" ValidationGroup="Desire" CssClass="btn btn-info" runat="server" OnClick="lnksavecampaign_Click">Save Campaign</asp:LinkButton>
                                                    </div>
                                                </div>

                                                <div id="bar" class="progress progress-striped" role="progressbar">
                                                    <div class="progress-bar progress-bar-success">
                                                    </div>
                                                </div>

                                                <%-- <div class="tab-content">
                                                    <div class="alert alert-danger display-none">
                                                        <button class="close" data-dismiss="alert"></button>
                                                        You have some form errors. Please check below.
                                                    </div>--%>
                                                <%-- <div class="alert alert-success display-none">
                                                        <button class="close" data-dismiss="alert"></button>
                                                        Your form validation is successful!
                                                    </div>--%>
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

</asp:Content>
