<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="LocalInvoice.aspx.cs" Inherits="Web.Sales.LocalInvoice" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function PrintPanel() {
            var id = document.getElementById("<%=tectionNo.ClientID %>");
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title> ');
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
    <%--<style> @media screen {div.divFooter {display: none;}}@media print {div.divFooter {position: fixed;bottom: 0;}} </style>--%>
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
                                                    <br />
                                                    <br />

                                                    <%--<img src="images/Logo.jpg" alt="logo" class="logo-default" style="margin-top: 10px; width: 250px;" />--%>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%; height: auto">
                                            <tr>
                                                <td style="" colspan="2">
                                                    <h3 style="text-align: center; margin-bottom: 2px;">&nbsp;<asp:Label ID="lblcseandcredt" runat="server" Text="test"></asp:Label>&nbsp; INVOICE
                                                    </h3>
                                                    <h3 style="text-align: center; margin-top: 0px; margin-bottom: 0px;">&nbsp;<asp:Label ID="lblcseandcredtarabic" runat="server" Text=""></asp:Label>&nbsp; رقم الفاتورة
                                                    </h3>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%; height: auto;" border="1" cellpadding="1" cellspacing="1">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 17%;">
                                                        <div><span class="muted" style="padding-left: 5px; font-weight: bold;">Invoice #</span></div>
                                                        <div><span class="muted" style="padding-left: 5px; font-weight: bold;">رقم الفاتورة </span></div>
                                                    </td>
                                                    <td style="width: 45%; padding-left: 5px;">
                                                        <asp:Label ID="tectionNo" runat="server" Style="font-size: 16px;"></asp:Label></td>
                                                    <td style="text-align: left; width: 15%;">
                                                        <div><span class="muted" style="padding-left: 5px; font-weight: bold;">Date &nbsp;</span></div>
                                                        <div><span class="muted" style="padding-left: 5px; font-weight: bold;">تاريخ &nbsp;</span></div>
                                                    </td>
                                                    <td style="width: 23%; padding-left: 5px;">
                                                        <asp:Label ID="LblDate" runat="server" Style="font-size: 16px;"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div><span class="muted" style="padding-left: 5px; font-weight: bold;">Billing Client </span></div>
                                                        <div><span class="muted" style="padding-left: 5px; font-weight: bold;">فواتير العملاء </span></div>
                                                    </td>
                                                    <td style="padding-left: 5px;">
                                                        <asp:Label ID="labelUSerNAme" runat="server" Style="font-size: 16px;"></asp:Label></td>
                                                    <td style="text-align: left;">
                                                        <div><span class="muted" style="padding-left: 5px; font-weight: bold;">Reference </span></div>
                                                        <div><span class="muted" style="padding-left: 5px; font-weight: bold;">المرجع &nbsp;</span></div>
                                                    </td>
                                                    <td style="padding-left: 5px;">
                                                        <asp:Label ID="lblreferno" runat="server" Style="font-size: 16px;"></asp:Label></td>
                                                </tr>

                                            </tbody>
                                        </table>
                                        <hr />
                                       
                                            <table style="width: 100%;" border="1" cellpadding="1" cellspacing="0">
                                                <thead>
                                                    <tr>
                                                        <td style="text-align: center; width: 3%"><strong>#</strong></td>
                                                        <td style="text-align: center; width: 14%;"><strong>Item<br />
                                                            رمز المنتج</strong></td>
                                                        <td style="text-align: center; width: 40%;"><strong>Item Description<br />
                                                            بند وصـــــــــف</strong></td>
                                                        <td style="text-align: center; width: 10%;"><strong>QTY<br />
                                                            كمية</strong></td>
                                                        <td style="text-align: center; width: 10%;"><strong>Unit Cost<br />
                                                            سعر الوحدة</strong></td>
                                                        <td style="text-align: center; width: 20%;"><strong>Total<br />
                                                            مجمـــــــــــوع</strong></td>
                                                    </tr>

                                                </thead>

                                                <tbody>
                                                    <asp:ListView ID="listProductst" runat="server">

                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="text-align: center"><%#Container.DataItemIndex+1%></td>
                                                                <td><%# getprodname(Convert.ToInt32 (Eval("MyProdID")))%> </td>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# GetDesc(Eval("DESCRIPTION").ToString()) %>'></asp:Label></td>
                                                                <td style="text-align: center"><%#Eval("QUANTITY")%></td>
                                                                <td style="text-align: center"><%#Eval("UNITPRICE")%> </td>
                                                                <td style="text-align: center"><%#Eval("AMOUNT")%> </td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <td style="text-align: left" colspan="5">
                                                            <asp:Label ID="lblword" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <strong>Sub - Total</strong>
                                                            <asp:Label ID="lblSubtotal" runat="server"></asp:Label><br />

                                                            <strong>Discount:</strong><asp:Label ID="lblDiscount" runat="server"></asp:Label><br />
                                                            <%--<strong>VAT:</strong>
                                                    <asp:Label ID="lblVat" runat="server" Text="0"></asp:Label>%<br />--%>
                                                            <strong>Grand Total:</strong>
                                                            <asp:Label ID="lblGalredTot" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
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
                                            <hr />
                                       
                                        <div class="divFooter">
                                            <table style="width: 100%" border="0" cellpadding="1" cellspacing="1">
                                                <thead>
                                                    <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;Salesman
                                                         <br />
                                                            <br />
                                                            <br />
                                                        </td>
                                                        <td style="text-align: right">Received By&nbsp;:-Name & Signature&nbsp;&nbsp;
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
                                            </table>
                                            <hr />

                                            <asp:Label ID="lblteglin" runat="server"></asp:Label>
                                            <%--<asp:TextBox Style="width: 100%;" Visible="false" TextMode="MultiLine" ID="txtteglin" runat="server"></asp:TextBox>--%>
                                            <CKEditor:CKEditorControl runat="server" ID="CKEditTagline" Visible="false" Height="200px"></CKEditor:CKEditorControl>
                                            <hr />
                                            <center><h5> <asp:Label ID="lblbottumline" runat="server" ></asp:Label></h5></center>
                                        </div>
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

    <script type="text/javascript" src="../assets/global/plugins/ckeditor/ckeditor.js"></script>
</asp:Content>
