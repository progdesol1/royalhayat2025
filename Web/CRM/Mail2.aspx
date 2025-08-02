<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="Mail2.aspx.cs" Inherits="Web.CRM.Mail2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <ul class="page-breadcrumb breadcrumb">
            <li>
                <a href="DashBoard.aspx">CRM</a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <a href="#">CRM Mail Send</a>
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
                    <div class="portlet light">
                        <div class="portlet box blue-hoki">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-gift"></i>
                                    <asp:Label ID="Label16" runat="server" Text="CRM Testing Mail"></asp:Label>
                                </div>
                                <div class="tools">
                                    <a href="javascript:;" class="collapse"></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                    <a href="javascript:;" class="reload"></a>
                                    <a href="javascript:;" class="remove"></a>
                                </div>
                                <div class="actions btn-set">
                                     <asp:LinkButton ID="btnSend" data-placement="top" data-toggle="tooltip" ToolTip="Submit Data" runat="server" ValidationGroup="s" class="btn btn-success" OnClick="btnSend_Click" Text="Send" />
                                     <a href="http://corp.digital53.com/mail/mail.html" runat="server" class="btn btn-success" target="_blank" >Show Formate</a> 
                                        
                                    <%--<asp:LinkButton ID="btnshowformate" data-placement="top" data-toggle="tooltip" ToolTip="Submit Data" runat="server" class="btn btn-success" Text="ShowFormate" />--%>
                                     </div>
                            </div>

                            <div class="portlet-body">
                                <div class="tabbable">
                                    <div class="tab-content no-space">
                                        <div class="tab-pane active" id="tab_general1">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <label class="col-md-2 control-label">
                                                            <asp:Label ID="lblname" runat="server" Text="Contact Or Company Name"></asp:Label></label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtname" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="txtnamevalid" runat="server" CssClass="Validation" ControlToValidate="txtname" ErrorMessage="ContactOrCompany Name Required." ValidationGroup="s"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <label class="col-md-2 control-label">
                                                            <asp:Label ID="lblmailsend" runat="server" Text="Email ID"></asp:Label></label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtmailsend" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="txtmailvalid" runat="server" CssClass="Validation" ControlToValidate="txtmailsend" ErrorMessage="Email ID is Required." ValidationGroup="s"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <%--<asp:Label ID="lblsendingmail" CssClass="form-control" runat="server"></asp:Label>--%>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="portlet light">
                        <div class="portlet box blue-hoki">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-gift"></i>
                                    <asp:Label ID="Label28" runat="server" Text="CRM Mail Send"></asp:Label>
                                </div>
                                <div class="tools">
                                    <a href="javascript:;" class="collapse"></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                    <a href="javascript:;" class="reload"></a>
                                    <a href="javascript:;" class="remove"></a>
                                </div>
                                <div class="actions btn-set"> 
                                    <asp:LinkButton ID="btnSubmit" data-placement="top" data-toggle="tooltip" ToolTip="Submit Data" runat="server" OnClick="btnSubmit_Click" class="btn btn-success" Text="Show" />
                                </div>
                            </div>

                            <div class="portlet-body">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <div class="alert alert-success">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                        <strong></strong>
                                        Select Any One
                                    </div>
                                </asp:Panel>
                                <div class="tabbable">
                                    <div class="tab-content no-space">
                                        <div class="tab-pane active" id="tab_general1">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <div class="col-md-6">
                                                        <label class="col-md-4 control-label">
                                                            <asp:Label ID="lblyears" runat="server" Text="Company List"></asp:Label>
                                                        </label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="drpcompantSerch" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="col-md-4 control-label">
                                                            <asp:Label ID="Label1" runat="server" Text="Contact List"></asp:Label>

                                                        </label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="drpcontactList" runat="server" CssClass="form-control"></asp:DropDownList>
                                                           
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
   
    <asp:Panel ID="pnlcompniy" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <div class="portlet box blue">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-gift"></i>
                                    <asp:Label ID="Label13" runat="server" Text="Company List"></asp:Label>
                                </div>
                                <div class="tools">
                                    <a href="javascript:;" class="collapse"></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                    <a href="javascript:;" class="reload"></a>
                                    <a href="javascript:;" class="remove"></a>
                                </div>
                                <div class="actions btn-set">
                                    <asp:Button ID="btnsendMail" class="btn purple" runat="server" Text="Send Mail" OnClick="btnsendMail_Click" />
                                </div>
                            </div>

                            <div class="portlet-body">
                                <div class="tabbable">
                                    <table class="table table-striped table-bordered table-hover" id="sample_1">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="lblSN" runat="server" Text="#"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="lblGrEdit" runat="server" Text="Company Name"></asp:Label></th>

                                                <th>
                                                    <asp:Label ID="lblGRHMYSYSNAME" runat="server" Text="Itmanager"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHinoutSwitch" runat="server" Text="EMAIL"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHtranstype1" runat="server" Text="Mobile No"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHtranstype2" runat="server" Text="State"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="Label2" runat="server" Text="City"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="Label3" runat="server" Text="Remark"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:CheckBox ID="cbkchekbo" runat="server" AutoPostBack="true" OnCheckedChanged="cbkchekbo_CheckedChanged" Text="All" />
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="grdmstr" runat="server">
                                                <LayoutTemplate>
                                                    <tr id="ItemPlaceholder" runat="server">
                                                    </tr>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>' meta:resourcekey="lblSRNOResource2"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="hidecompanyctid" runat="server" Text='<%# Eval("COMPID") %>' Visible="false" />

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
                                                        <td>
                                                            <asp:CheckBox ID="cbkcmpnylist" runat="server" /></td>

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
    </asp:Panel>
    <asp:Panel ID="pnlcontect" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <div class="portlet box red">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-gift"></i>
                                    <asp:Label ID="Label14" runat="server" Text="Contact List"></asp:Label>
                                </div>
                                <div class="tools">
                                    <a href="javascript:;" class="collapse"></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                    <a href="javascript:;" class="reload"></a>
                                    <a href="javascript:;" class="remove"></a>
                                </div>
                                <div class="actions btn-set">
                                    <asp:Button ID="btnContactsend" class="btn blue" runat="server" Text="Send Mail" OnClick="btnContactsend_Click" />
                                </div>
                            </div>

                            <div class="portlet-body">
                                <div class="tabbable">
                                    <table class="table table-striped table-bordered table-hover" id="sample_2">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="Label4" runat="server" Text="#"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label5" runat="server" Text="Customer Name"></asp:Label></th>

                                                <th>
                                                    <asp:Label ID="Label6" runat="server" Text="Address"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="Label7" runat="server" Text="EMAIL"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="Label8" runat="server" Text="Mobile No"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="Label9" runat="server" Text="State"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="Label12" runat="server" Text="ZipCode"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="Label10" runat="server" Text="City"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="Label11" runat="server" Text="Remark"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:CheckBox ID="cbkcontect" runat="server" AutoPostBack="true" OnCheckedChanged="cbkcontect_CheckedChanged" Text="All" />
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="ListView1" runat="server">
                                                <LayoutTemplate>
                                                    <tr id="ItemPlaceholder" runat="server">
                                                    </tr>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>' meta:resourcekey="lblSRNOResource2"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label Visible="false" ID="hidecontactid" runat="server" Text='<%# Eval("ContactMyID") %>' />
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
                                                        <td>
                                                            <asp:CheckBox ID="cbkcontectInList" runat="server" /></td>
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
    </asp:Panel>
    </div>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
