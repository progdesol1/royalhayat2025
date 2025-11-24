<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="SaleReturnHPSPrints.aspx.cs" Inherits="Web.Sales.SaleReturnHPSPrints" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">function copyDiv() {var firstDivContent = document.getElementById('mydiv1'); var secondDivContent = document.getElementById('mydiv2'); secondDivContent.innerHTML = firstDivContent.innerHTML; } </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <script type="text/javascript">
        function PrintPanel() {

            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=100%');
            printWindow.document.write('<html><head><title></title> ');
            printWindow.document.write('</head><body onload="copyDiv();">');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
           
            return false;
        }
       
       
    </script>
    <div id="copy" class="">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet light">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="icon-basket font-green-sharp"></i>
                                        <span class="caption-subject font-green-sharp bold uppercase">Print Sale Return</span>
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:Button ID="btnPrint" runat="server" class="btn green-haze btn-circle" OnClientClick="return PrintPanel();" Text="Print" ValidationGroup="submit" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" Text="Cancel" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                                <asp:Panel ID="pnlContents" runat="server">
                                    <div>
                                       <table style="width: 100%; height: 190px">
                                            <tr>
                                                <td style="width: 40%;font-size: 13px;">
                                                    
                                                      Name :  <asp:Label runat="server" ID="lblCustomerName" Text=""></asp:Label><br />
                                                 Mobile :  <asp:Label runat="server" ID="lblCustomerMobile" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 20%;">
                                                    <center style="margin-top: 10px;">
                                                        Sales Return &nbsp;<asp:Label runat="server" ID="lblInvoiceNo" Text="Label"></asp:Label><br />
                                                        Using Sales Invoice &nbsp;<asp:Label runat="server" ID="lblAgains" Text="Label"></asp:Label>
                                                   </center>
                                                </td>
                                                <td style="width: 40%;text-align: right;">
                                                    Date :  <asp:Label runat="server" ID="lblInvoiceDate" Text=""></asp:Label><br />
                                                </td>
                                            </tr>
                                        </table>
                                         <table style="width: 100% ;font-size: 12px;" border="0" cellpadding="1" cellspacing="1">
                                            <tbody>
                                                <asp:ListView ID="listProductst" runat="server">

                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align: center;height:31px; width: 10%;"><%# getProdID(Eval("MyProdID").ToString()) %></td>
                                                            <td style="text-align: center;height:31px; width: 13%;"><%# getprodname(Eval("MyProdID").ToString())%> </td>
                                                            <td style="text-align: Left;height:31px; font-size: 12px;width: 39%;"><%# Eval("DESCRIPTION")%> </td>
                                                            <td style="text-align: right;height:31px; width: 8%;"><%#Eval("QUANTITY")%></td>
                                                            <td style="text-align: right;height:31px; width: 11%;"><%#Eval("UNITPRICE")%> </td>
                                                            <td style="text-align: right;height:31px; width: 10%;"><%#Eval("AMOUNT")%> </td>
                                                        </tr>

                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </tbody>
                                            <tfoot>
                                                <td style="text-align: left" colspan="5">

                                                  <table style="width: 100% ;font-size: 12px;">
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                               <strong>
                                                        <asp:Label ID="lblword" runat="server"></asp:Label></strong></td>
                                                        </tr>
                                                    </table>

                                                </td>
                                                 <td style="text-align: right">
                                                     <table style="width: 100% ;font-size: 12px;">
                                                        <tr>
                                                            <td>Sub Total:</td>
                                                            <td>
                                                                <asp:Label ID="lblSubtotal" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Discount:</td>
                                                            <td>
                                                                <asp:Label ID="lblDiscount" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <asp:Label ID="lblGalredTot" runat="server"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tfoot>
                                        </table>
 					<asp:Label ID="lblVat" runat="server" Visible="false" Text="0"></asp:Label>
                                        <asp:Label ID="lblteglin" runat="server"></asp:Label><asp:TextBox Style="width: 100%;" Visible="false" TextMode="MultiLine" ID="txtteglin" runat="server"></asp:TextBox>
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
    <div id="copied"></div>
   
</asp:Content>
