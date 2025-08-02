<%@ Page Title="" Language="C#" MasterPageFile="~/POS/POSMaster.Master" AutoEventWireup="true" CodeBehind="ReportYearlySale.aspx.cs" Inherits="Web.Admin.ReportYearlySale" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="shortcut icon" href="favicon.ico" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>Yearly_Sale_Report</title> <style> @media all {.page-break { display: none; }} @media print { .page-break { display: block; page-break-before: always; }}</style>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrapper">

            <div class="tabbable-custom tabbable-noborder">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet light" style="border: 1px solid #abb2c9;">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="icon-basket font-green-sharp"></i>
                                        <span class="caption-subject font-green-sharp bold uppercase">Yearly Sale</span>
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:Button ID="btnPrint" runat="server" class="btn green-haze btn-circle" OnClientClick="return PrintPanel();" Text="Print" ValidationGroup="submit" />
                                        <asp:LinkButton ID="btnCancel1" runat="server" CssClass="btn green-haze btn-circle" Text="Cancel" PostBackUrl="~/Admin/Index.aspx"></asp:LinkButton>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlWarningMsg" runat="server" Visible="false">
                                    <div class="alert alert-warning alert-dismissable">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                        <asp:Label ID="lblWarningMsg" Font-Size="Large" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="form-group" style="color: ">
                                            <asp:Label runat="server" ID="Label8" CssClass="col-md-4 control-label" Text="Start Date"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtstartdate" CssClass="form-control" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtstartdate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group" style="color: ">
                                            <asp:Label runat="server" ID="Label2" CssClass="col-md-4 control-label" Text="End Date"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtEnddate" CssClass="form-control" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtEnddate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-sm blue" OnClientClick="showProgress()" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <asp:Panel ID="pnlContents" runat="server">
                                    <%-- <table style="width: 100%">
                                        <tr>
                                            <td style="">
                                                <asp:Image ID="HealtybarLogo" ImageUrl="assets/global/img/HealthBar.png" runat="server" />
                                            </td>
                                        </tr>
                                    </table>--%>

                                    <br />
                                    <table style="width: 100%" border="1" cellpadding="1" cellspacing="1">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;"><strong>Sales ID </strong></th>
                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;"><strong>Receipt No.</strong></th>
                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;"><strong>Date </strong></th>
                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;"><strong>Total</strong></th>
                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;"><strong>Sold By </strong></th>
                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;"><strong>Discount</strong></th>
                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;"><strong>TAX</strong></th>
                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;"><strong>Change Amount</strong></th>
                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;"><strong>Due </strong></th>
                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;"><strong>Comments </strong></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="ListView1" runat="server">
                                                <ItemTemplate>

                                                    <tr>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Sales ID") %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Recipt No") %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Date") %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Gettotal(Eval("Recipt No").ToString()) %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("Sold By") %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label11" runat="server" Text='<%# Getsum(Eval("Recipt No").ToString()) %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="lblcustomer" runat="server" Text='<%# Getvat(Eval("Recipt No").ToString()) %>'></asp:Label></td>

                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Getsumchange(Eval("Recipt No").ToString()) %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label9" runat="server" Text='<%# Getduechange(Eval("Recipt No").ToString()) %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label10" runat="server" Text='<%# Eval("Comments") %>'></asp:Label></td>
                                                        <td>
                                                    </tr>


                                                </ItemTemplate>

                                            </asp:ListView>


                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: right; padding-right: 5px;">
                                                    <asp:Label ID="Label12" runat="server" Text="Total:-" Style="font-weight: bold; font-size: large"></asp:Label></td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblFinalTotal" runat="server" Text=""></asp:Label></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: right; padding-right: 5px;">
                                                    <asp:Label ID="Label13" runat="server" Text="Total Discount:-" Style="font-weight: bold; font-size: large"></asp:Label></td>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lbldis" runat="server" Text=""></asp:Label></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: right; padding-right: 5px;">
                                                    <asp:Label ID="Label15" runat="server" Text="Total TAX:-" Style="font-weight: bold; font-size: large"></asp:Label></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblTAX" runat="server" Text=""></asp:Label></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: right; padding-right: 5px;">
                                                    <asp:Label ID="Label17" runat="server" Text="Total Sales+TAX:-" Style="font-weight: bold; font-size: large"></asp:Label></td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblsata" runat="server" Text=""></asp:Label></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>

                                                <td></td>
                                                <td></td>

                                                <td></td>
                                                <td></td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: right; padding-right: 5px;">
                                                    <asp:Label ID="Label19" runat="server" Text="Total Due:-" Style="font-weight: bold; font-size: large"></asp:Label></td>

                                                <td></td>
                                                <td></td>
                                                <td></td>

                                                <td></td>
                                                <td></td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lbldue" runat="server" Text=""></asp:Label></td>
                                                <td></td>
                                                <td></td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: right; padding-right: 5px;">
                                                    <asp:Label ID="Label21" runat="server" Text="Payment Report" Style="font-weight: bold; font-size: large"></asp:Label></td>

                                                <td></td>
                                                <td></td>
                                                <td></td>

                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; padding-right: 5px;">
                                                    <asp:Label ID="Label22" runat="server" Text="From Date :-" Style="font-weight: bold; font-size: large"></asp:Label></td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblfromdate" runat="server" Text=""></asp:Label></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; padding-right: 5px;">
                                                    <asp:Label ID="Label14" runat="server" Text="To Date :-" Style="font-weight: bold; font-size: large"></asp:Label></td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lbltodate" runat="server" Text=""></asp:Label></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>

                                        </tfoot>
                                    </table>

                                </asp:Panel>
                                <br />
                                <br />
                                <br />
                                <br />
                            </div>
                        </div>


                    </div>
                </div>
            </div>

    </div>
</asp:Content>
