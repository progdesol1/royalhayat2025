<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="GYMReport2Print.aspx.cs" Inherits="Web.Master.GYMReport2Print" %>
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
                                        <span class="caption-subject font-green-sharp bold uppercase">Print Reports</span>
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

                                                <td style="align-items: center">
                                                    <center>
                                                    <img src="../assets/demo.png" alt="logo" class="logo-default" style="margin-top: 10px; width: 120px; height: 70px;" />
                                                        </center>
                                                </td>

                                            </tr>

                                        </table>
                                        <hr />
                                        <table style="width: 100%; height: auto">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 25%;">

                                                        <h3 style="text-align: center;">&nbsp;<asp:Label ID="lblHeader" runat="server" Text="Daily Sales Transactions"></asp:Label><br />
                                                            <asp:Label ID="lblinvoice" runat="server" Text=""></asp:Label>
                                                        </h3>
                                                        <asp:Label ID="label3" Visible="false" runat="server" Text="test"></asp:Label>
                                                        <asp:Label ID="label6" Visible="false" runat="server" Text="test"></asp:Label>
                                                        <asp:Label ID="label7" Visible="false" runat="server" Text="test"></asp:Label>

                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <hr />
                                        <asp:Panel ID="PNLSaleHD" runat="server" Visible="false">
                                            <table style="width: 100%" border="1" cellpadding="1" cellspacing="1">
                                                <thead>
                                                    <tr>
                                                        <td style="text-align: center; width: 3%"><strong>#</strong></td>
                                                        <td style="text-align: center"><strong>Date<br />
                                                            تاريخ</strong></td>
                                                        <td style="text-align: center"><strong>Transaction<br />
                                                            عملية تجارية</strong></td>
                                                        <td style="text-align: center"><strong>User Doc No<br />
                                                            عبر دوك</strong></td>
                                                        <td style="text-align: center"><strong>Amount<br />
                                                            كمية</strong></td>
                                                        <td style="text-align: center"><strong>Paid BY<br />
                                                            دفع بواسطة</strong>
                                                        </td>
                                                        <td style="text-align: center"><strong>Reference<br />
                                                            مرجع</strong></td>
                                                        <td style="text-align: center"><strong>Customer<br />
                                                            المجموع الأجنبي</strong></td>

                                                    </tr>

                                                </thead>

                                                <tbody>
                                                    <asp:ListView ID="listProductst" runat="server">

                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="text-align: center"><%#Container.DataItemIndex+1%></td>
                                                                <td><%# Eval("ENTRYDATE", "{0:M/d/yyyy}")%> </td>
                                                                <td><%# CompAndUser(Convert.ToInt32(Eval("MYTRANSID")))%> </td>
                                                                <td style="text-align: center"><%# Eval("TransDocNo")%></td>
                                                                <td style="text-align: center"><%# Eval("TOTAMT")%> </td>
                                                                <td style="text-align: center"><%# PaidBy(Convert.ToInt32(Eval("MYTRANSID"))) %></td>
                                                                <td style="text-align: center"><%# Eval("REFERENCE")%> </td>
                                                                <td style="text-align: center"><%# CustVendID(Convert.ToInt32( Eval("CUSTVENDID")))%> </td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <td style="text-align: right" colspan="4">
                                                            <strong>Total:-&nbsp;</strong>
                                                        </td>
                                                        <td colspan="4">
                                                            <strong>
                                                                <asp:Label ID="lblTotAMT" runat="server" Text=""></asp:Label></strong></td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="PNLSaleDT" runat="server" Visible="false">
                                            <table style="width: 100%" border="1" cellpadding="1" cellspacing="1">
                                                <thead>
                                                    <tr>
                                                        <td style="text-align: center; width: 3%"><strong>#</strong></td>
                                                        <td style="text-align: center"><strong>User Doc No<br />
                                                            عبر دوك</strong>
                                                        </td>
                                                        <td style="text-align: center"><strong>TransDate<br />
                                                            تاريخ</strong>
                                                        </td>
                                                        <% if(Request.QueryString["ID"] == "9" || Request.QueryString["ID"] == "10" || Request.QueryString["ID"] == "11" || Request.QueryString["ID"] == "12"){ %>
                                                        <td style="text-align: center"><strong>Customer<br />
                                                            زبون</strong>
                                                        </td>
                                                        <%} else {%>
                                                        <td style="text-align: center"><strong>Supplier<br />
                                                            زبون</strong>
                                                        </td>
                                                        <% } %>
                                                        <td style="text-align: center"><strong>Reference<br />
                                                            عبر</strong>
                                                        </td>                                                        
                                                        <td style="text-align: center"><strong>ITEM CODE<br />
                                                            رمز الصنف</strong></td>
                                                        <td style="text-align: center"><strong>DESCRIPTION<br />
                                                            وصف</strong></td>
                                                        <td style="text-align: center"><strong>QTY<br />
                                                            الكمية</strong></td>
                                                        <td style="text-align: center"><strong>Unit Price<br />
                                                            سعر الوحدة</strong></td>
                                                        <td style="text-align: center"><strong>Amount<br />
                                                            كمية</strong></td>
                                                        <td style="text-align: center"><strong>Cost<br />
                                                            كلفة</strong>
                                                        </td>
                                                        <td style="text-align: center"><strong>GrossProfit<br />
                                                            اجمالي الربح</strong></td>                                                        
                                                    </tr>

                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="ListSaleDT" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="text-align: center"><%#Container.DataItemIndex+1%></td>
                                                                <td style="text-align: center"><%# TransDoc(Convert.ToInt32(Eval("MYTRANSID"))) %></td>
                                                                <td style="text-align: center"><%# TransDate(Convert.ToInt32(Eval("MYTRANSID"))) %></td>
                                                                <td style="text-align: center"><%# CusVendID(Convert.ToInt32(Eval("MYTRANSID"))) %></td>
                                                                <td style="text-align: center"><%# CompAndUser(Convert.ToInt32(Eval("MYTRANSID"))) +" / "+ Ref(Convert.ToInt32(Eval("MYTRANSID"))) %></td>                                                               
                                                                <td style="text-align: center"><%# Eval("MyProdID") %></td>
                                                                <td><%# Eval("DESCRIPTION") %> </td>
                                                                <td style="text-align: center"><%# Eval("QUANTITY") %></td>
                                                                <td style="text-align: center"><%# Eval("UNITPRICE") %></td>
                                                                <td style="text-align: center"><%# Eval("AMOUNT") %></td>
                                                                <td style="text-align: center"><%# Eval("CostAmount") %></td>
                                                                <td style="text-align: center"><%# Gross(Convert.ToInt32(Eval("MYTRANSID")),(Convert.ToInt32(Eval("MYID")))) %></td>                                                                
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                                <tfoot style="border-collapse: collapse;">
                                                    <tr>
                                                        <td colspan="7" style="text-align: right;"><strong>Total:-&nbsp;</strong></td>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="lblqty" runat="server"></asp:Label></strong></td>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="lblUnitPrice" runat="server"></asp:Label></strong></td>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="lblAmount" runat="server"></asp:Label></strong></td>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="lblCost" runat="server"></asp:Label></strong></td>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="lblGross" runat="server"></asp:Label></strong></td>
                                                        
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </asp:Panel>
                                        <asp:Label ID="lblteglin" runat="server"></asp:Label><asp:TextBox Style="width: 100%;" Visible="false" TextMode="MultiLine" ID="txtteglin" runat="server"></asp:TextBox>
                                        <hr />
                                        <center><h5> <asp:Label ID="lblbottumline" runat="server" ></asp:Label></h5></center>

                                    </div>
                                </asp:Panel>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
