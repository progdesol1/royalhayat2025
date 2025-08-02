<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="ForeignInvoice.aspx.cs" Inherits="Web.Sales.ForeignInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <div class="">
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

                                                <td style=""><strong>
                                                    <span class="muted">Invoice # :- </span>
                                                    <asp:Label ID="tectionNo" runat="server"></asp:Label>
                                                    رقم الفاتورة ؛
                                                     <br />

                                                    <span class="muted">LPO # :- </span>
                                                    <asp:Label ID="lbllponumber" runat="server"></asp:Label>
                                                    طلب شراء :
                                                   <br />
                                                    <asp:Panel ID="pnltrrms" runat="server" Visible="false">
                                                        <span class="muted">Terms :- </span>
                                                        <asp:Label ID="lblterms" runat="server"></asp:Label>
                                                        شروط :
                                                   <br />
                                                    </asp:Panel>
                                                </strong>
                                                </td>

                                                <td style="">

                                                    <h3 style="text-align: center;">&nbsp;<asp:Label ID="lblcseandcredt" runat="server" Text="test"></asp:Label>&nbsp; INVOICE
                                                    </h3>
                                                    <asp:Label ID="label3" Visible="false" runat="server" Text="test"></asp:Label>
                                                    <asp:Label ID="label6" Visible="false" runat="server" Text="test"></asp:Label>
                                                    <asp:Label ID="label7" Visible="false" runat="server" Text="test"></asp:Label>

                                                </td>

                                                <td style="text-align: right">
                                                    <span class="muted">Date :- </span>
                                                    <asp:Label ID="LblDate" runat="server"></asp:Label>
                                                    تاريخ ؛
                                                    <br />
                                                    <span class="muted">Reference :-</span>
                                                    <asp:Label ID="lblreferno" runat="server"></asp:Label>
                                                    المرجع: 
                                                    <br />
                                                </td>
                                                <tr>
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

                                                </tr>


                                            </tbody>

                                        </table>
                                        <hr />

                                        <table style="width: 100%" border="1" cellpadding="1" cellspacing="1">
                                            <thead>
                                                <tr>
                                                    <td style="text-align: center; width: 3%"><strong>#</strong></td>
                                                    <td style="text-align: center"><strong>Item<br />
                                                        رمز المنتج</strong></td>
                                                    <td style="text-align: center"><strong>Description<br />
                                                        وصـــــــــف</strong></td>
                                                    <td style="text-align: center"><strong>Quantity<br />
                                                        كمية</strong></td>
                                                    <td style="text-align: center"><strong>Unit Cost K.D<br />
                                                        سعر الوحدة</strong></td>
                                                    <td style="text-align: center"><strong>Total K.D<br />
                                                        مجمـــــــــــوع</strong></td>
                                                </tr>

                                            </thead>

                                            <tbody>
                                                <asp:ListView ID="listProductst" runat="server">

                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align: center"><%#Container.DataItemIndex+1%></td>
                                                            <td><%# getprodname(Convert.ToInt32 (Eval("MyProdID")))%> </td>
                                                            <td><%# Eval("DESCRIPTION")%> </td>
                                                            <td style="text-align: center"><%#Eval("QUANTITY")%></td>
                                                            <td style="text-align: center"><%#Eval("UNITPRICE")%> </td>
                                                            <td style="text-align: center"><%#Eval("AMOUNT")%> </td>
                                                        </tr>

                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </tbody>
                                            <tfoot>
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
                                            </tfoot>
                                        </table>
                                        <hr />
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
                                        </table>
                                        <hr />
                                        <table style="width: 100%" border="0" cellpadding="1" cellspacing="1">
                                            <thead>
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;Salesman
                                                         <br />
                                                        <br />
                                                        <br />
                                                    </td>
                                                    <td style="text-align: right">Received By&nbsp;&nbsp;&nbsp;
                                                         <br />
                                                        <br />
                                                        <br />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>_____________________________<br />
                                                        ( Najmuddin Saiffudin Mandav )    
                                                       
                                                    </td>
                                                    <td style="text-align: right">_____________________________<br />
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </thead>
                                        </table>
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
