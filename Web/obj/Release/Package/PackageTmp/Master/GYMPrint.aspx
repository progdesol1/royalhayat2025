<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="GYMPrint.aspx.cs" Inherits="Web.Master.GYMPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title></title> <style> @media all {.page-break { display: none; }} @media print { .page-break { display: block; page-break-before: always; }}</style>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);

            return false;
        }
    </script>
    <div>
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet light">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="icon-basket font-green-sharp"></i>
                                        <span class="caption-subject font-green-sharp bold uppercase">Print Invoice</span>
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:Button ID="btnPrint" runat="server" class="btn green-haze btn-circle" OnClientClick="return PrintPanel();" Text="Print" ValidationGroup="submit" />

                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" Text="Cancel" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                                <asp:Panel ID="pnlContents" runat="server">
                                    <div style="width: 219px; height: 793px;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <h3 style="text-align: center;">&nbsp;<asp:Label ID="Label1" runat="server" Text="GYM Management"></asp:Label>
                                                    </h3>

                                                </td>
                                            </tr>
                                        </table>

                                        <table style="width: 100%; height: auto">
                                            <tbody>
                                                <tr>
                                                    <td style="" colspan="2">
                                                        <h4 style="text-align: center;">&nbsp;<asp:Label ID="lblcseandcredt" runat="server" Text="test"></asp:Label>
                                                        </h4>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 70%;">

                                                        <table>
                                                            <tr>
                                                                <td><span class="muted"><strong>Invoice # </strong></span></td>
                                                                <%-- <td><span class="muted">/ رقم الفاتورة ؛ :- &nbsp;</span></td>--%>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="tectionNo" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td><span class="muted"><strong>Billing Client </strong></span></td>
                                                                <%--<td><span class="muted">/ فواتير العملاء :- &nbsp;</span></td>--%>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="labelUSerNAme" runat="server"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </td>

                                                    <td style="width: 30%;">
                                                        <table>
                                                            <tr>
                                                                <td style="text-align: right;"><span class="muted"><strong>Date</strong> </span></td>
                                                                <%-- <td><span class="muted">/ تاريخ ؛  :- &nbsp;</span></td>--%>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="LblDate" runat="server"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right;"><span class="muted"><strong>Reference </strong></span></td>
                                                                <%-- <td><span class="muted">/ المرجع :- &nbsp;</span></td>--%>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lblreferno" runat="server"></asp:Label></td>
                                                            </tr>
                                                        </table>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>

                                                    <td style="text-align: right"></td>

                                                </tr>
                                                <%-- <tr>
                                                    <td colspan="2" style="text-align: center;">
                                                        <h3>&nbsp;About:
                                                            من نحن </h3>
                                                        <asp:Label ID="lblhenderline" runat="server" Text="test"></asp:Label>
                                                        <asp:Label ID="label4" Visible="false" runat="server" Text="test"></asp:Label>
                                                        <asp:Label ID="label5" Visible="false" runat="server"></asp:Label>
                                                    </td>
                                                </tr>--%>
                                            </tbody>

                                        </table>
                                        <hr />

                                        <table style="width: 100%" border="1" cellpadding="1" cellspacing="1">
                                            <thead>
                                                <tr>
                                                    <td style="text-align: center; width: 3%"><strong>#</strong></td>
                                                    <%-- <td style="text-align: center"><strong>Item</strong></td>--%>
                                                    <td style="text-align: center"><strong>Package</strong></td>
                                                    <%--<td style="text-align: center"><strong>Month</strong></td>--%>
                                                    <%-- <td style="text-align: center"><strong>Unit Cost K.D<br />
                                                        سعر الوحدة</strong></td>--%>
                                                    <td style="text-align: center; width: 32%;"><strong>Total K.D</strong></td>
                                                </tr>

                                            </thead>

                                            <tbody>
                                                <asp:ListView ID="listProductst" runat="server">

                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align: center"><%#Container.DataItemIndex+1%></td>
                                                            <%-- <td><%# getprodname(Convert.ToInt32 (Eval("MyProdID")))%> </td>--%>
                                                            <td><%# GetPackage(Convert.ToInt32(Eval("QUANTITY")))%>
                                                                <br />
                                                                For <%# Eval("BIN_ID") %> Months
                                                                <br />
                                                                <%# Convert.ToDateTime(Eval("PlanStartDate")).ToString("dd-MMM-yyyy") %>                                                                
                                                                TO<br />
                                                                <%# Convert.ToDateTime(Eval("PlanEndDate")).ToString("dd-MMM-yyyy") %> </td>
                                                            <%--<td style="text-align: center"><%#Eval("BIN_ID")%></td>--%>
                                                            <%-- <td style="text-align: center"><%#Eval("AMOUNT")%> </td>--%>
                                                            <td style="text-align: center"><%#Eval("AMOUNT")%> </td>
                                                        </tr>

                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </tbody>
                                            <tfoot>
                                                <td style="text-align: left" colspan="2">

                                                    <strong>
                                                        <asp:Label ID="lblword" runat="server"></asp:Label></strong>

                                                </td>
                                                <td style="text-align: right">

                                                    <strong>Total:</strong>
                                                    <asp:Label ID="lblSubtotal" runat="server"></asp:Label><br />
                                                    <strong>Disc:</strong><asp:Label ID="lblDiscount" runat="server"></asp:Label><br />
                                                    <%--<strong>VAT:</strong>
                                                    <asp:Label ID="lblVat" runat="server" Text="0"></asp:Label>%<br />--%>
                                                    <strong>Final:</strong>
                                                    <asp:Label ID="lblGalredTot" runat="server"></asp:Label>
                                                </td>
                                            </tfoot>
                                        </table>
                                        <%--<hr />
                                        <table style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td class="well" style="width: 49%;">
                                                        <address>
                                                            <strong>Shipping Address :-
                                                                عنوان الشحن</strong>
                                                            <br />
                                                            <asp:Label ID="lblname" runat="server"></asp:Label>

                                                            <br />
                                                            <asp:Label ID="lbladdrsh" runat="server"></asp:Label><br />

                                                            <asp:Label ID="lblemailshlipen" runat="server"></asp:Label><br />
                                                        </address>
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td style="text-align: right; width: 49%;" class="well">
                                                        <address>
                                                            <strong>&nbsp;Payment Details :-
                                                            تفاصيل الدفع </strong>
                                                            <br />
                                                            <asp:Label ID="lblpayment" runat="server"></asp:Label>
                                                            <br />
                                                            &nbsp;
                                                             <br />
                                                            &nbsp;
                                                              <br />
                                                            &nbsp;
                                                        </address>
                                                    </td>


                                                </tr>
                                            </tbody>
                                        </table>--%>
                                        <%-- <hr />
                                        <table style="width: 100%" border="0" cellpadding="1" cellspacing="1">
                                            <thead>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;Authority Signature
                                                         <br />
                                                        <br />
                                                        <br />
                                                    </td>
                                                    <td style="text-align: right">Customer&nbsp;:-Name & Signature&nbsp;&nbsp;
                                                         <br />
                                                        <br />
                                                        <br />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>_____________________________<br />
                                                        <asp:Label ID="lblSalemen" runat="server"></asp:Label>

                                                    </td>
                                                    <td style="text-align: right">_____________________________<br />
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </thead>
                                        </table>--%>
                                        <hr />

                                        <asp:Label ID="lblteglin" runat="server"></asp:Label><asp:TextBox Style="width: 100%;" Visible="false" TextMode="MultiLine" ID="txtteglin" runat="server"></asp:TextBox>
                                        <hr />
                                        <center><h5> <asp:Label ID="lblbottumline" runat="server" ></asp:Label></h5></center>
                                    </div>
                                </asp:Panel>                                
                                <asp:Button ID="btnteglin" runat="server" Text="Set Footer" OnClick="btnteglin_Click" />
                                <div class="page-break"></div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
