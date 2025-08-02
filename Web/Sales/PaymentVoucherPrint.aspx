<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="PaymentVoucherPrint.aspx.cs" Inherits="Web.Sales.PaymentVoucherPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .border12 {
            position: relative;
            border-radius: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
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
    <div class="page-content">
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
                                    <div>
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="">
                                                    <img src="images/Logo.jpg" alt="logo" class="logo-default" style="margin-top: 10px; width: 250px;" />
                                                    <%-- <img src="../assets/admin/layout4/img/cmbc.jpg" alt="logo" class="logo-default" style="margin-top: 10px; width: 100px;" />--%></td>


                                            </tr>
                                        </table>
                                        <hr />
                                        <table style="width: 100%; height: auto">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 30%;">
                                                        <strong>
                                                            <span class="muted">&nbsp;&nbsp;K.D دينار  &nbsp; Fils فلس </span>
                                                        </strong>
                                                        <br />
                                                        <table style="width: 40%; height: auto">
                                                            <tr>
                                                                <td style=" border: double;">
                                                                    <asp:Label ID="lblKD" runat="server" Text="1000"></asp:Label>
                                                                </td>
                                                               
                                                                <td style=" border: double;">
                                                                    <asp:Label ID="lblFils" runat="server" Text="Fils"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>                                                     
                                                        <br />
                                                    </td>
                                                    <td style="width: 40%;">

                                                        <h3 style="text-align: center;">&nbsp;<asp:Label ID="lblcseandcredt" runat="server" Text="Payment"></asp:Label>&nbsp; Voucher
                                                        </h3>
                                                        <asp:Label ID="label3" Visible="false" runat="server" Text="test"></asp:Label>
                                                        <asp:Label ID="label6" Visible="false" runat="server" Text="test"></asp:Label>
                                                        <asp:Label ID="label7" Visible="false" runat="server" Text="test"></asp:Label>

                                                    </td>

                                                    <td style="text-align: right; width: 30%;">
                                                        <span class="muted">No :- </span>
                                                        <asp:Label ID="LblDate" runat="server" CssClass="text-left" Text="1" Width="109px"></asp:Label>                                                        
                                                    <br />
                                                        <span class="muted">Date :-</span>
                                                        <asp:Label ID="lblreferno" runat="server" Text="01/01/2017"></asp:Label>
                                                        المرجع
                                                    <br />
                                                    </td>
                                                </tr>
                                                <%--  <tr>
                                                    <td style="width: 30%">
                                                        <h3 style="margin-top: -35px;">Billing Client :
                                                            فواتير العملاء</h3>

                                                        <asp:Label ID="labelUSerNAme" runat="server"></asp:Label>
                                                        <asp:Label ID="LabelUserAddresh" runat="server"></asp:Label>
                                                        <asp:Label ID="lbltelemail" runat="server"></asp:Label>

                                                    </td>
                                                    <td style="width: 40%;"></td>
                                                    <td style="width: 30%; text-align: right">
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

                                        <table style="width: 100%" border="0" cellpadding="1" cellspacing="1">
                                            <thead>
                                                <tr style="height: 30px;">
                                                    <td style="text-align: left;"><strong>Paid To MR./MRS :-</strong>
                                                    <asp:Label ID="lblPaidToMRMRS" runat="server" Text="demo"></asp:Label>
                                                    </td>
                                                    <td></td>
                                                     <td></td>
                                                    <td></td>
                                                    <td style="text-align:right"><asp:Label ID="lblsumofKD2" runat="server" Text="دفعت إلى السيد السيدة" /></td>                                                   
                                                </tr>
                                                <tr style="height: 30px;">
                                                    <td style="text-align: left;"><strong>The Sum Of K.D :-</strong>
                                                        <asp:Label ID="lblsumofKD" runat="server" Text="demo"></asp:Label>                                                                                                             
                                                    </td>
                                                    <td></td>
                                                     <td></td>
                                                    <td></td>
                                                    <td style="text-align:right"><asp:Label ID="Label2" runat="server" Text="مجموع دينار" /></td>
                                                </tr>
                                                <tr style="height: 30px;">
                                                    <td style="text-align: left; width:30%;"><strong>Case Check NO. :-</strong>
                                                        <asp:Label ID="lblCaseCheckNO" runat="server" Text="demo"></asp:Label> 
                                                    </td>                                                   
                                                     <td style="text-align:right;"><asp:Label ID="Label4" runat="server" Text="لحالة تحقق أي" /></td>
                                                     <td>&nbsp;</td>
                                                    <td style="text-align: left;"><strong>On Bank :-</strong>
                                                        <asp:Label ID="lblonbank" runat="server" Text="demo"></asp:Label>
                                                    </td>
                                                     <td style="text-align:right;"><asp:Label ID="Label5" runat="server" Text="على البنك" /></td>
                                                </tr>
                                                <tr style="height: 30px;">
                                                    <td style="text-align: left;"><strong>Being :-</strong>
                                                        <asp:Label ID="lblbeing" runat="server" Text="demo"></asp:Label>
                                                    </td>                                                     
                                                    <td></td>
                                                     <td></td>
                                                    <td></td>
                                                    <td style="text-align:right"><asp:Label ID="Label8" runat="server" Text="يجرى" /></td>
                                                </tr>
                                            </thead>

                                            <tbody>
                                                <asp:ListView ID="listProductst" runat="server">

                                                    <ItemTemplate>
                                                        <tr>
                                                            <%--<td style="text-align: center"><%#Container.DataItemIndex+1%></td>
                                                            <td><%# getprodname(Convert.ToInt32 (Eval("MyProdID")))%> </td>
                                                            <td><%# Eval("DESCRIPTION")%> </td>
                                                            <td style="text-align: center"><%#Eval("QUANTITY")%></td>
                                                            <td style="text-align: center"><%#Eval("UNITPRICE")%> </td>
                                                            <td style="text-align: center"><%#Eval("AMOUNT")%> </td>--%>
                                                        </tr>

                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </tbody>
                                            <%--  <tfoot>
                                                <td style="text-align: left" colspan="5">

                                                    <strong>
                                                        <asp:Label ID="lblword" runat="server"></asp:Label></strong>

                                                </td>
                                                <td style="text-align: right">

                                                    <strong>Sub - Total amount:</strong>K.D
                                                    <asp:Label ID="lblSubtotal" runat="server"></asp:Label><br />

                                                    <strong>Discount:</strong><asp:Label ID="lblDiscount" runat="server"></asp:Label><br />
                                                    <strong>VAT:</strong>
                                                    <asp:Label ID="lblVat" runat="server" Text="0"></asp:Label>%<br />
                                                    <strong>Grand Total:</strong> K.D
                                                    <asp:Label ID="lblGalredTot" runat="server"></asp:Label>


                                                </td>
                                            </tfoot>--%>
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
                                        <hr />
                                        <center>
                                        <table style="width: 80%;" border="0" cellpadding="1" cellspacing="1">
                                            <thead>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;Receiver&nbsp;المتلقي
                                                         <br />
                                                        <br />
                                                        <br />
                                                    </td>
                                                    <td style="text-align: right">Accounts&nbsp; حسابات &nbsp;&nbsp;&nbsp;
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
                                                    </td>
                                                </tr>
                                            </thead>
                                        </table>
                                        </center>
                                        <hr />

                                        <asp:Label ID="lblteglin" runat="server"></asp:Label><asp:TextBox Style="width: 100%;" Visible="false" TextMode="MultiLine" ID="txtteglin" runat="server"></asp:TextBox>
                                        <hr />
                                        <center><h5> <asp:Label ID="lblbottumline" runat="server" ></asp:Label></h5></center>
                                    </div>
                                </asp:Panel>
                                <asp:Button ID="btnteglin" runat="server" Text="Set Footer" OnClick="btnteglin_Click" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
