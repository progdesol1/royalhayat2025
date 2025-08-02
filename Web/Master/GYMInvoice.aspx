<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="GYMInvoice.aspx.cs" Inherits="Web.Master.GYMInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="LangTextBox" Namespace="ServerControl1" TagPrefix="Lang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="b" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <div class="portlet-body form">
                            <div class="portlet-body">
                                <div class="form-wizard">
                                    <div class="tabbable">
                                        <asp:Panel ID="pnlwarning" runat="server" Visible="false">
                                            <div class="alert alert-warning alert-dismissable">
                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                <asp:Label ID="lblmsgw" Font-Size="Large" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <div class="row">
                                            <div class="col-md-12">

                                                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                                <div class="portlet box green">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            <i class="fa fa-pagelines"></i>
                                                            <asp:Label runat="server" ID="Label18" Text="Invoice"></asp:Label>
                                                            (View Only)
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                            <a href="javascript:;" class="reload"></a>
                                                            <a href="javascript:;" class="remove"></a>
                                                        </div>
                                                        <div class="actions btn-set">
                                                            <%--<asp:Button ID="btnpacksave" CssClass="btn btn-sm yellow-crusta" runat="server" Text="Save" OnClick="btnpacksave_Click" />--%>
                                                            <asp:Button ID="btncnacel" CssClass="btn btn-sm red" runat="server" Text="Cancel" OnClick="btncnacel_Click" />

                                                            <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="../assets/global/img/flags/us.png" /></asp:LinkButton>
                                                            <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="../assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                            <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="../assets/global/img/flags/fr.png" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body">

                                                        <div class="row" style="margin-left: -0px; margin-right: -0px;">

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="lblPERSNAME1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPERSNAME1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtAmount" Enabled="false" placeholder="Amount" MaxLength="12" runat="server" class="form-control" meta:resourcekey="txtAddress2Resource1"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Amount Is Required" ControlToValidate="txtAmount" InitialValue="0" ValidationGroup="ValidDT"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" TargetControlID="txtAmount" ValidChars="0123456789." FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPERSNAME2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPERSNAME2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin-left: -0px; margin-right: -0px;">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="lblPERSNAMEO1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPERSNAMEO1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtpackStartdate" Enabled="false" placeholder="MM/dd/yyyy" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtendertxtdateTO1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtpackStartdate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" TargetControlID="txtpackStartdate" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <asp:Label ID="lblmonth" runat="server" ForeColor="Red" CssClass="control-label"></asp:Label>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPERSNAMEO2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPERSNAMEO2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="lblPERSNAMEO21s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPERSNAMEO21s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtpackEnddate" Enabled="false" placeholder="MM/dd/yyyy" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtpackEnddate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtpackEnddate" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPERSNAMEO22h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPERSNAMEO22h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>


                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">

                                                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                                <div class="portlet box blue">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            <i class="fa fa-edit"></i>
                                                            <asp:Label runat="server" ID="Label41" Text="Package Details"></asp:Label>
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
                                                        <asp:Panel ID="PanelDtl" runat="server">

                                                            <table class="table table-striped table-hover table-bordered" id="sample_2">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="width: 1%;"></th>
                                                                        <th>
                                                                            <%--Text="Contact ID"--%>
                                                                            <asp:Label runat="server" ID="lblActive1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" CssClass="col-md-8 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:Label runat="server" ID="lblActive2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" CssClass="col-md-8 control-label" Visible="false"></asp:TextBox>
                                                                        </th>
                                                                        <th>
                                                                            <%--Text="Package"--%>
                                                                            <asp:Label runat="server" ID="lblREMARKS1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtREMARKS1s" CssClass="col-md-8 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:Label runat="server" ID="lblREMARKS2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtREMARKS2h" CssClass="col-md-8 control-label" Visible="false"></asp:TextBox>
                                                                        </th>
                                                                        <th>
                                                                            <%--Text="Start Date"--%>
                                                                            <asp:Label runat="server" ID="lblCRUP_ID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRUP_ID1s" CssClass="col-md-8 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:Label runat="server" ID="lblCRUP_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRUP_ID2h" CssClass="col-md-8 control-label" Visible="false"></asp:TextBox>
                                                                        </th>
                                                                        <th>
                                                                            <%--Text="End Date"--%>
                                                                            <asp:Label runat="server" ID="lblCOMPANYID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOMPANYID1s" CssClass="col-md-8 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:Label runat="server" ID="lblCOMPANYID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOMPANYID2h" CssClass="col-md-8 control-label" Visible="false"></asp:TextBox>
                                                                        </th>
                                                                        <th>
                                                                            <%--Text="Statuse"--%>
                                                                            <asp:Label runat="server" ID="lblMYCATSUBID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMYCATSUBID1s" CssClass="col-md-8 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:Label runat="server" ID="lblMYCATSUBID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMYCATSUBID2h" CssClass="col-md-8 control-label" Visible="false"></asp:TextBox>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label3" Text="Print"></asp:Label>
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:ListView ID="ListPackage" runat="server" OnItemCommand="ListPackage_ItemCommand">
                                                                        <LayoutTemplate>
                                                                            <tr id="ItemPlaceholder" runat="server">
                                                                            </tr>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td></td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkCustName" CommandName="btnview" CommandArgument='<%# Eval("MYTRANSID") %>' runat="server">
                                                                                        <asp:Label ID="Label11" runat="server" Text='<%# getContactName(Convert.ToInt32(Eval("JOID"))) %>'></asp:Label>
                                                                                        <asp:Label ID="lblmtid" Visible="false" runat="server" Text='<%# Eval("MYID") %>'></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkPackname" CommandName="btnview" CommandArgument='<%# Eval("MYTRANSID") %>' runat="server">
                                                                                        <asp:Label ID="Label50" runat="server" Text='<%# GetPack(Convert.ToInt32(Eval("QUANTITY"))) %>'></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkStartdate" CommandName="btnview" CommandArgument='<%# Eval("MYTRANSID") %>' runat="server">
                                                                                        <asp:Label ID="Label51" runat="server" Text='<%# Convert.ToDateTime(Eval("PlanStartDate")).ToShortDateString() %>'></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkEnddate" CommandName="btnview" CommandArgument='<%# Eval("MYTRANSID") %>' runat="server">
                                                                                        <asp:Label ID="Label52" runat="server" Text='<%# Convert.ToDateTime(Eval("PlanEndDate")).ToShortDateString() %>'></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkButton1" CommandName="btnview" CommandArgument='<%# Eval("MYTRANSID") %>' runat="server">
                                                                                        <asp:Label ID="Label53" runat="server" Text='<%# Eval("Stutas") %>'></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton class="btn btn-lg blue" CommandName="btnprient" CommandArgument='<%# Eval("MYTRANSID") +","+ Eval("QUANTITY") %>' ID="Print" Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;" runat="server">Print <i class="fa fa-print"></i></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>

                                                                </tbody>
                                                            </table>

                                                        </asp:Panel>

                                                    </div>



                                                </div>
                                                <!-- END EXAMPLE TABLE PORTLET-->
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
