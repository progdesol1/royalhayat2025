<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="ContactSearch.aspx.cs" Inherits="Web.CRM.ContactSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="LangTextBox" Namespace="ServerControl1" TagPrefix="Lang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css" media="screen">
        /* commented backslash hack for ie5mac \*/
        html, body {
            height: 100%;
        }
        /* end hack */
        .overlay {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: #EDEDF3;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .CenterPB {
            position: fixed;
            z-index: 999;
            left: 50%;
            top: 40%;
            margin-top: -30px; /* make this half your image/element height */
            margin-left: -30px; /* make this half your image/element width */
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
    </style>
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="b" runat="server">

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet-body form">
                                <div class="portlet-body">
                                    <div class="form-wizard">
                                        <div class="portlet box blue">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <i class="fa fa-gift"></i>
                                                    <asp:Label ID="Label20" runat="server" Text="Search Contact Master" meta:resourcekey="Label20Resource1"></asp:Label>
                                                    <asp:TextBox Style="color: #333333" ID="txtHeader" runat="server" Visible="false"></asp:TextBox>
                                                </div>
                                                <div class="tools">
                                                    <a href="javascript:;" class="collapse"></a>
                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                    <asp:LinkButton ID="btnPagereload" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                    <a href="javascript:;" class="remove"></a>
                                                </div>
                                                <div class="actions btn-set">

                                                    <asp:Button ID="btnfind" class="btn yellow  btn-circle" runat="server" Text="Find" OnClick="btnfind_Click" OnClientClick="showProgress()" />
                                                    <asp:Button ID="Button2" class="btn green-haze btn-circle" runat="server" Text="Clear Search" OnClick="Button2_Click" />
                                                </div>
                                            </div>
                                            <div class="portlet-body ">
                                                <div class="tabbable">
                                                    <div class="tab-content no-space">
                                                        <div class="form-body">
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label29" runat="server" Text="Contact Name:" meta:resourcekey="Label29Resource1"></asp:Label>


                                                                </label>
                                                                <div class="col-md-4">

                                                                    <asp:TextBox ID="txtContactName" runat="server" name="name" placeholder="Contact Name" data-toggle="tooltip" ToolTip="Company Name" class="form-control"></asp:TextBox>


                                                                </div>
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label30" runat="server" Text="Contact Lan 2:" meta:resourcekey="Label30Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">

                                                                    <%--<asp:TextBox ID="txtCustomer" placeholder="اسم الشخص" runat="server" AutoCompleteType="Disabled" class="arabic form-control" TextLanguage="Arabic"></asp:TextBox>--%>
                                                                    <Lang:LangTextBox ID="txtContact2" runat="server" AutoCompleteType="Disabled" CssClass="arabic form-control" placeholder="اسم الشخص" TextLanguage="Arabic" meta:resourcekey="txtContact2Resource1"></Lang:LangTextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label37" runat="server" Text="Contact Lan 3:" meta:resourcekey="Label37Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">

                                                                    <asp:TextBox ID="txtContact3" placeholder="Contact Name  Language 3" runat="server" class="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label38" runat="server" Text="Country:" meta:resourcekey="Label38Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label6" runat="server" meta:resourcekey="Label6Resource1"></asp:Label>
                                                                    <asp:DropDownList ID="drpCountry12" runat="server" class="form-control ">
                                                                    </asp:DropDownList>
                                                                    <%-- <asp:TextBox ID="TextBox4" runat="server" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Address Is Required" ControlToValidate="txtAddress" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">

                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label39" runat="server" Text="Postal Code :" meta:resourcekey="Label39Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblPostalCode" runat="server" meta:resourcekey="lblPostalCodeResource1"></asp:Label>
                                                                    <asp:TextBox ID="txtPostalCode" placeholder="Postal Code" runat="server" class="form-control" meta:resourcekey="txtPostalCodeResource1"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtPostalCode" FilterType="Custom, Numbers" runat="server" Enabled="True" />

                                                                </div>
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label40" runat="server" Text="State:" meta:resourcekey="Label40Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label8" runat="server" meta:resourcekey="Label8Resource1"></asp:Label>
                                                                    <asp:TextBox ID="txtstest" placeholder="State" runat="server" class="form-control"></asp:TextBox>
                                                                </div>

                                                            </div>

                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label41" runat="server" Text="Zip Code" meta:resourcekey="Label41Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblZipCode" runat="server" meta:resourcekey="lblZipCodeResource1"></asp:Label>
                                                                    <asp:TextBox ID="txtZipCode" placeholder="ZipCode" runat="server" class="form-control" meta:resourcekey="txtZipCodeResource1"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtZipCode" FilterType="Custom, Numbers" runat="server" Enabled="True" />

                                                                </div>
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label42" runat="server" Text="City:" meta:resourcekey="Label42Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblCity" runat="server" meta:resourcekey="lblCityResource1"></asp:Label>
                                                                    <asp:TextBox ID="txtCity" placeholder="City" runat="server" class="form-control" meta:resourcekey="txtCityResource1"></asp:TextBox>

                                                                </div>

                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label43" runat="server" Text="Address1:" meta:resourcekey="Label43Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblAddress" runat="server" meta:resourcekey="lblAddressResource1"></asp:Label>
                                                                    <asp:TextBox ID="txtAddress" placeholder="Address1" runat="server" class="form-control" meta:resourcekey="txtAddressResource1"></asp:TextBox>

                                                                </div>
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label44" runat="server" Text="Address2:" meta:resourcekey="Label44Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblAddress2" runat="server" meta:resourcekey="lblAddress2Resource1"></asp:Label>
                                                                    <asp:TextBox ID="txtAddress2" placeholder="Address2" runat="server" class="form-control" meta:resourcekey="txtAddress2Resource1"></asp:TextBox>

                                                                </div>

                                                            </div>

                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblEmail" runat="server" Text="EMAIL:" meta:resourcekey="lblEmailResource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-10">

                                                                    <asp:TextBox ID="tags_2" runat="server" name="email" CssClass="form-control tags" meta:resourcekey="tags_2Resource1"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblFax" runat="server" Text="Fax:" meta:resourcekey="lblFaxResource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-10">

                                                                    <asp:TextBox ID="tags_3" name="number" runat="server" CssClass="form-control tags" meta:resourcekey="tags_3Resource1"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblBusPhone" runat="server" Text=" Bus Phone:" meta:resourcekey="lblBusPhoneResource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-10">

                                                                    <asp:TextBox ID="tags_4" name="number" runat="server" CssClass="form-control tags" meta:resourcekey="tags_4Resource1"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Bus Phone Is Required" ControlToValidate="tags_2" ValidationGroup="Submit" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">

                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label45" runat="server" Text="Mobile No:" meta:resourcekey="Label45Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">

                                                                    <asp:TextBox ID="txtMobileNo" placeholder="Mobile No" runat="server" class="form-control"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtMobileNo" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label48" runat="server" Text="Remark:" meta:resourcekey="Label48Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-10">
                                                                    <asp:Label ID="lblRemark" runat="server" meta:resourcekey="lblRemarkResource1"></asp:Label>
                                                                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" placeholder="Remark" runat="server" class="form-control" meta:resourcekey="txtRemarkResource1"></asp:TextBox>
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



                        <asp:Panel ID="Panel1" Visible="false" runat="server">
                            <div class="page-content-wrapper">
                                <div class="form-horizontal form-row-seperated">
                                    <div class="portlet box red">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-cogs"></i>Search Contact List
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                            <div class="actions btn-set">
                                                <asp:LinkButton ID="btnselectexit" class="btn yellow  btn-circle " runat="server" OnClick="btnselectexit_Click">Select & Exit </asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" class="btn green-haze btn-circle" runat="server" OnClick="Button2_Click">Clear Search </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label runat="server" id="Label106" class="control-label col-md-2 getshow">
                                                                <asp:Label runat="server" ID="Label107">Saved Search</asp:Label>
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="DrpTitle" runat="server" class="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Title" ControlToValidate="DrpTitle" ValidationGroup="SaveSearch21" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:Button ID="Button3" class="btn btn-circle purple" runat="server" Text="Show" ValidationGroup="SaveSearch21" OnClick="btnSearch_Click" />
                                                                <asp:Button ID="btnAppend" class="btn btn-circle red" runat="server" Text="Append" ValidationGroup="SaveSearch21" OnClick="btnAppend_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label runat="server" id="Label104" class="control-label col-md-2 getshow">
                                                                <asp:Label runat="server" ID="Label105">Title</asp:Label>
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTitle" ErrorMessage="Title Required." CssClass="Validation" ValidationGroup="SaveSearch"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:Button ID="Button1" class="btn btn-circle blue" runat="server" Text="Save" ValidationGroup="SaveSearch" OnClick="btnSearchSave_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--<asp:Button ID="btnSearch" class="btn green" runat="server" Text="Search" ValidationGroup="SaveSearch21" OnClick="" />--%>
                                                    <%-- <asp:Button ID="btnSearchSave" class="btn green" runat="server" Text="Save" ValidationGroup="SaveSearch" OnClick="" />--%>
                                                </div>
                                            </div>
                                            <asp:Panel runat="server" ID="pnlGrid">
                                                <div class="tab-content">
                                                    <div id="tab_1_1" class="tab-pane active">

                                                        <div class="tab-content no-space">
                                                            <div class="tab-pane active" id="tab_general2">
                                                                <div class="table-container" style="">




                                                                    <div class="portlet-body" style="margin-left: 10px; margin-right: 10px; margin-top: 0px; padding-top: 10px; padding-bottom: 10px;">
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
                         Entries&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList Style="width: 150px;" class="form-control input-inline " ID="drpSort" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpSort_SelectedIndexChanged">
                         </asp:DropDownList></label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-6 col-sm-12">
                                                                                    <div id="sample_1_filter" class="dataTables_filter">
                                                                                        <label>
                                                                                            <asp:TextBox ID="txtSearch" Placeholder="Search" class="form-control input-small input-inline" runat="server"></asp:TextBox>
                                                                                            <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton10_Click" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-search"></i></asp:LinkButton></label>

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
                                                                                                        <asp:CheckBox ID="chball" OnCheckedChanged="chball_CheckedChanged" AutoPostBack="true" runat="server" /><asp:Label ID="Label1" runat="server" Text="All"></asp:Label></th>

                                                                                                    <th>
                                                                                                        <asp:Label ID="Label78" runat="server" Text="Contact Name"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label79" runat="server" Text="Address"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label80" runat="server" Text="EMAIL"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label81" runat="server" Text="Mobile No"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label82" runat="server" Text="State"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label83" runat="server" Text="ZipCode"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label84" runat="server" Text="City"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label85" runat="server" Text="Remark"></asp:Label></th>


                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:ListView ID="Listview1" runat="server" OnItemDataBound="Listview1_ItemDataBound">
                                                                                                    <LayoutTemplate>
                                                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                                                        </tr>
                                                                                                    </LayoutTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <tr class="gradeA">
                                                                                                            <td>
                                                                                                                <asp:CheckBox ID="checkesone" runat="server" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="Label2" Visible="false" runat="server" Text='<%# Eval("ContactMyID") %>'></asp:Label>
                                                                                                                <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("PersName1") %>' meta:resourcekey="lblCustomerNameResource3"></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDR1") %>' meta:resourcekey="lblAddressResource4"></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblEMAIL" runat="server" Text='<%# Eval("EMAIL1") %>' meta:resourcekey="lblEMAILResource4"></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblMOBPHONE" runat="server" Text='<%# Eval("MOBPHONE") %>' meta:resourcekey="lblMOBPHONEResource2"></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblSTATE" runat="server" Text='<%# Eval("STATE") %>' meta:resourcekey="lblSTATEResource2"></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblZIPCODE" runat="server" Text='<%# Eval("ZIPCODE") %>' meta:resourcekey="lblZIPCODEResource2"></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblCITY" runat="server" Text='<%# Eval("CITY") %>' meta:resourcekey="lblCITYResource3"></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>' meta:resourcekey="lblREMARKSResource2"></asp:Label></td>
                                                                                                        </tr>
                                                                                                    </ItemTemplate>
                                                                                                </asp:ListView>

                                                                                            </tbody>
                                                                                        </table>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                            <div class="row">
                                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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

                                                                                                <asp:LinkButton ID="btnfirst1" OnClick="btnfirst1_Click1" runat="server"> First</asp:LinkButton>
                                                                                            </li>
                                                                                            <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_Next">

                                                                                                <asp:LinkButton ID="btnNext1" Style="width: 53px;" OnClick="btnNext1_Click1" runat="server"> Next</asp:LinkButton>
                                                                                            </li>
                                                                                            <asp:ListView ID="ListView3" runat="server" OnItemCommand="ListView3_ItemCommand" OnItemDataBound="AnswerList_ItemDataBound">
                                                                                                <ItemTemplate>
                                                                                                    <td>
                                                                                                        <li class="paginate_button " aria-controls="sample_1" tabindex="0">
                                                                                                            <asp:LinkButton ID="LinkPageavigation" runat="server" CommandName="LinkPageavigation" CommandArgument='<%# Eval("ID")%>'> <%# Eval("ID")%></asp:LinkButton></li>

                                                                                                    </td>
                                                                                                </ItemTemplate>
                                                                                            </asp:ListView>
                                                                                            <li class="paginate_button next" aria-controls="sample_1" tabindex="0" id="sample_1_Previos">
                                                                                                <asp:LinkButton ID="btnPrevious1" OnClick="btnPrevious1_Click" Style="width: 58px;" runat="server"> Prev</asp:LinkButton>
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
                                            </asp:Panel>
                                            <div class="scroll-to-top">
                                                <i class="icon-arrow-up"></i>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="chball" />
            <asp:PostBackTrigger ControlID="btnfind" />
            <asp:AsyncPostBackTrigger ControlID="btnfirst1" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnNext1" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnPrevious1" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnLast1" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="overlay">
                <div style="z-index: 1000; margin-left: 350px; margin-top: 200px; opacity: 1; -moz-opacity: 1;">
                    <img src="../assets/admin/layout4/img/loading-spinner-blue.gif" />
                    &nbsp;please wait...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
