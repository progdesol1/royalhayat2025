<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="SearchTitalPage.aspx.cs" Inherits="Web.CRM.SearchTitalPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <%--<ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">Search Data</a>
                    </li>
                </ul>--%>
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
                                        <asp:LinkButton ID="btnPagereload" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <div id="navigation" runat="server" class="btn-group btn-group-circle btn-group-solid">
                                            <asp:Button ID="btnFirst" class="btn red" runat="server" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" />
                                        </div>
                                        <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" Text="AddNew" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" Text="Cancel" />
                                        <asp:Button ID="btnEditLable" runat="server" class="btn green-haze btn-circle" Text="Update Label" />
                                        &nbsp;
                                        <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server">E&nbsp;<img src="/assets/global/img/flags/us.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server">A&nbsp;<img src="/assets/global/img/flags/ae.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server">F&nbsp;<img src="/assets/global/img/flags/fr.png" /></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="portlet-body form">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general1">
                                                    <div class="col-md-12" id="Divmainsub" runat="server">
                                                        <div class="form-body">

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
                                                                        <asp:LinkButton ID="btnlistreload" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>

                                                                        <a href="javascript:;" class="remove"></a>
                                                                    </div>
                                                                </div>
                                                                <div class="portlet-body form">
                                                                    <asp:Panel ID="pnlContact" runat="server" Visible="false">
                                                                        <div class="tab-content">
                                                                            <div id="tab_1_1" class="tab-pane active">
                                                                                <div class="tab-content no-space">
                                                                                    <div class="tab-pane active" id="tab_general2">
                                                                                        <div class="table-container" style="">
                                                                                            <div class="portlet-body" style="margin-left: 15px; margin-right: 15px;">
                                                                                                <div id="sample_1_wrapper" class="dataTables_wrapper no-footer">

                                                                                                    <div class="row">
                                                                                                        <div class="col-md-6 col-sm-12" style="padding-top: 18px;">
                                                                                                            <div class="dataTables_length" id="sample_1_length">
                                                                                                                <label>
                                                                                                                    Show
                                                                                       <asp:DropDownList class="form-control input-xsmall input-inline " ID="drpShowGrid" AutoPostBack="true" runat="server">
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
                                                                                                        <div class="col-md-6 col-sm-12" style="padding-top: 18px;">
                                                                                                            <div id="sample_1_filter" class="dataTables_filter">
                                                                                                                <label>Search:<input type="search" class="form-control input-small input-inline" placeholder="" aria-controls="sample_1" /></label>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                        <ContentTemplate>--%>
                                                                                                    <table class="table table-striped table-bordered table-hover" id="sample_11">
                                                                                                        <thead>
                                                                                                            <tr>
                                                                                                                <th>
                                                                                                                    <asp:Label ID="lblAction" runat="server" Text="Action" meta:resourcekey="lblActionResource1"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label ID="lblCuName" runat="server" Text="Customer Name" meta:resourcekey="lblCuNameResource1"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label ID="lbladdress3" runat="server" Text="Address" meta:resourcekey="lbladdress3Resource1"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label ID="lblemail3" runat="server" Text="EMAIL" meta:resourcekey="lblemail3Resource1"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label ID="lblmobileno3" runat="server" Text="MobileNo" meta:resourcekey="lblmobileno3Resource1"></asp:Label>
                                                                                                                </th>
                                                                                                                <th>
                                                                                                                    <asp:Label ID="lblState3" runat="server" Text="State" meta:resourcekey="lblState3Resource1"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label ID="lblzipcode3" runat="server" Text="ZipCode" meta:resourcekey="lblzipcode3Resource1"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label ID="lblcity3" runat="server" Text="City" meta:resourcekey="lblcity3Resource1"></asp:Label></th>
                                                                                                                <th>
                                                                                                                    <asp:Label ID="lblremark3" runat="server" Text="Remark" meta:resourcekey="lblremark3Resource1"></asp:Label></th>


                                                                                                            </tr>
                                                                                                        </thead>
                                                                                                        <tbody>
                                                                                                            <asp:ListView ID="listContactMaster" runat="server" OnItemCommand="listContactMaster_ItemCommand">
                                                                                                                <LayoutTemplate>
                                                                                                                    <tr id="ItemPlaceholder" runat="server">
                                                                                                                    </tr>
                                                                                                                </LayoutTemplate>
                                                                                                                <ItemTemplate>

                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="LinkButton3" CommandName="btnNavigate" runat="server">
                                                                                                                                <img id="callimg1" runat="server" src="images/phone.jpg" />
                                                                                                                            </asp:LinkButton>

                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:HiddenField ID="hidecontactid" runat="server" Value='<%# Eval("ContactMyID") %>' />
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
                                                                                                    <%--</ContentTemplate>
                                                                                                    </asp:UpdatePanel>--%>


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
                                                                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                                                    <ContentTemplate>
                                                                                                                        <ul class="pagination">
                                                                                                                            <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_fist">
                                                                                                                                <%--  <asp:LinkButton ID="Button1" runat="server"  BorderStyle="None" />First</asp:LinkButton> --%>
                                                                                                                                <asp:LinkButton ID="btnfirst1" runat="server"> First</asp:LinkButton>
                                                                                                                            </li>
                                                                                                                            <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_Next">
                                                                                                                                <%--  <asp:LinkButton ID="Button1" runat="server"  BorderStyle="None" />First</asp:LinkButton> --%>
                                                                                                                                <asp:LinkButton ID="btnNext1" runat="server"> Next</asp:LinkButton>
                                                                                                                            </li>
                                                                                                                            <asp:ListView ID="ListView2" runat="server">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <td>
                                                                                                                                        <li class="paginate_button " aria-controls="sample_1" tabindex="0">
                                                                                                                                            <asp:LinkButton ID="LinkPageavigation" runat="server" CommandName="LinkPageavigation" CommandArgument='<%# Eval("ID")%>'> <%# Eval("ID")%></asp:LinkButton></li>

                                                                                                                                    </td>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:ListView>
                                                                                                                            <li class="paginate_button next" aria-controls="sample_1" tabindex="0" id="sample_1_Previos">
                                                                                                                                <asp:LinkButton ID="btnPrevious1" runat="server"> Prev</asp:LinkButton>
                                                                                                                            </li>
                                                                                                                            <li class="paginate_button next" aria-controls="sample_1" tabindex="0" id="sample_1_last">
                                                                                                                                <asp:LinkButton ID="btnLast1" runat="server"> Last</asp:LinkButton>
                                                                                                                            </li>
                                                                                                                        </ul>
                                                                                                                    </ContentTemplate>
                                                                                                                </asp:UpdatePanel>
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
                                                                    <asp:Panel ID="pnlCompany" runat="server" Visible="false">
                                                                        <div class="row" style="margin-left: 9px; margin-right: 9px;">
                                                                            <div class="tabbable">
                                                                                <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Action</th>
                                                                                            <th>Company Name</th>
                                                                                            <th>Itmanager</th>
                                                                                            <th>EMAIL</th>
                                                                                            <th>Mobile No</th>
                                                                                            <th>State</th>
                                                                                            <th>City</th>
                                                                                            <th>Remark</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:ListView ID="listCustomerMasterCompany" runat="server" OnItemCommand="listCustomerMasterCompany_ItemCommand">
                                                                                            <LayoutTemplate>
                                                                                                <tr id="ItemPlaceholder" runat="server">
                                                                                                </tr>
                                                                                            </LayoutTemplate>
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:LinkButton ID="btnEdit1" CommandName="btnEdit" runat="server">
                                                                                                            <img id="callimg1" runat="server" src="images/phone.jpg" />
                                                                                                        </asp:LinkButton>

                                                                                                    </td>

                                                                                                    <td>
                                                                                                        <asp:HiddenField ID="hidecompanyctid" runat="server" Value='<%# Eval("COMPID") %>' />

                                                                                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("COMPNAME1")%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ITMANAGER")%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%# Eval("EMAIL1")%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblMOBPHONE" runat="server" Text='<%# Eval("MOBPHONE")%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblSTATE" runat="server" Text='<%# Eval("STATE")%>'></asp:Label></td>

                                                                                                    <td>
                                                                                                        <asp:Label ID="lblCITY" runat="server" Text='<%# Eval("CITY")%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS")%>'></asp:Label></td>


                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>

                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
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
            </div>
        </div>

    </div>

    <script src="assets/apps/scripts/todo-2.min.js"></script>
    <script>
        jQuery(document).ready(function () {

            TableManaged.init();
        });
    </script>

</asp:Content>
