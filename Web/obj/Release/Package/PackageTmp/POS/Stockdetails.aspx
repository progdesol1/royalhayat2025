<%@ Page Title="" Language="C#" MasterPageFile="~/POS/POSMaster.Master" AutoEventWireup="true" CodeBehind="Stockdetails.aspx.cs" Inherits="Web.POS.Stockdetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="shortcut icon" href="favicon.ico" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>Customer_Report</title> <style> @media all {.page-break { display: none; }} @media print { .page-break { display: block; page-break-before: always; }}</style>');
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
        <div class="page-content" style="padding-top: 0px;">
            <div class="tabbable-custom tabbable-noborder">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet light">
                                <div class="portlet-title">
                                    <div class="caption">

                                        <span class="caption-subject font-green-sharp bold uppercase">Stock Details</span>
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:Button ID="btnPrint" runat="server" class="btn green-haze btn-circle" OnClientClick="return PrintPanel();" Text="Print" ValidationGroup="submit" />
                                        <%--   <asp:LinkButton ID="btnCancel1" runat="server" CssClass="btn green-haze btn-circle" Text="Cancel" PostBackUrl="~/Admin/Index.aspx"></asp:LinkButton>--%>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlWarningMsg" runat="server" Visible="false">
                                    <div class="alert alert-warning alert-dismissable">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                        <asp:Label ID="lblWarningMsg" Font-Size="Large" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                  <%--  <div class="col-md-5">
                                        <div class="form-group" style="color: ">
                                            <asp:Label runat="server" ID="Label8" CssClass="col-md-4 control-label" Text="Start Date"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtstartdate" CssClass="form-control" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtstartdate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>--%>
                                  <%--  <div class="col-md-5">
                                        <div class="form-group" style="color: ">
                                            <asp:Label runat="server" ID="Label2" CssClass="col-md-4 control-label" Text="End Date"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtEnddate" CssClass="form-control" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtEnddate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-6">
                                        <asp:Label ID="lblsearch" runat="server" Text="Search by Item Code or Item Name"></asp:Label>
                                        <asp:TextBox ID="txtsearch" runat="server" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" />
                                    </div>
                                </div>

                                <asp:Panel ID="pnlContents" runat="server">
                                    <br />
                                    <table style="width: 100%" border="1" cellpadding="1" cellspacing="1">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Item Code</th>

                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Item Name</th>

                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Unit of Measure</th>

                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Quantity</th>

                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Buy Price</th>

                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Sales Price </th>

                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Category</th>

                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Supplier</th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="ListView1" runat="server">
                                                <ItemTemplate>

                                                    <tr>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Item Code") %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Item Name") %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Uint Of Masuare") %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("Buy Price") %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="lblcustomer" runat="server" Text='<%# Eval("Sales Price") %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="lblTamount" runat="server" Text='<%# Eval("Category") %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("Supplier") %>'></asp:Label></td>
                                                     </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </tbody>

                                    </table>
                                    <br />
                                    <br />
                                    <br />
                                </asp:Panel>
                            </div>
                        </div>


                    </div>

                </div>
            </div>
        </div>
    </div>


</asp:Content>
