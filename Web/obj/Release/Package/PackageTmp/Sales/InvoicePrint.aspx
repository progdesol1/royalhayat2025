<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="InvoicePrint.aspx.cs" Inherits="Web.Sales.InvoicePrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet light">
                                <!-- END PAGE BREADCRUMB -->
                                <!-- BEGIN PAGE BASE CONTENT -->
                                <div class="invoice">
                                    <div class="row invoice-logo">
                                        <div class="col-xs-6 invoice-logo-space">
                                            <img src="images/Logo.jpg" class="img-responsive" alt="" style="width: 300px;" />

                                        </div>
                                        <div class="col-xs-6">
                                            <p>
                                                <asp:Label ID="LblDate" runat="server"></asp:Label>

                                                <span class="muted">Consectetuer adipiscing elit </span>
                                            </p>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col-xs-4">
                                            <h3>Billing Client :</h3>
                                            <ul class="list-unstyled">
                                                <li>
                                                    <asp:Label ID="labelUSerNAme" runat="server"></asp:Label></li>
                                           
                                                <li>
                                                    <asp:Label ID="LabelUserAddresh" runat="server"></asp:Label>
                                                </li>
                                            
                                                <li>
                                                    <asp:Label ID="lbltelemail" runat="server"></asp:Label>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-xs-4">
                                            <h3>About:</h3>
                                            <ul class="list-unstyled">
                                                <li><asp:Label ID="lblhenderline" runat="server"></asp:Label></li>
                                            </ul>
                                        </div>
                                        <div class="col-xs-4 invoice-payment">
                                            <h3>Payment Details:</h3>
                                            <ul class="list-unstyled">
                                                <li>
                                                    <asp:Label ID="lblpayment" runat="server"></asp:Label>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                           <hr />
                                            <table class="table table-striped table-hover">
                                                <thead>
                                                    <tr> 
                                                        <th># </th>
                                                        <th>Item </th>
                                                        <th class="hidden-xs">Description </th>
                                                        <th class="hidden-xs">Quantity </th>
                                                        <th class="hidden-xs">Unit Cost K.D</th>
                                                        <th>Total K.D</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="listProductst" runat="server">

                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%#Container.DataItemIndex+1%></td>
                                                                <td><%# getprodname( Convert .ToInt32 (Eval("MyProdID")))%> </td>
                                                                <td class="hidden-xs"><%# Eval("DESCRIPTION")%> </td>
                                                                <td class="hidden-xs"><%#Eval("QUANTITY")%></td>
                                                                <td class="hidden-xs"><%#Eval("UNITPRICE")%> </td>
                                                                <td><%#Eval("AMOUNT")%> </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div> 
                                    <div class="row">
                                        <div class="col-xs-4">
                                            <div class="well">
                                                <address>
                                                    <strong>Shipping Address :-</strong>
                                                    <br />
                                                   <asp:Label ID="lblname" runat="server" ></asp:Label>
                                       
                                    <br />
                                                    <asp:Label ID="lbladdrsh" runat="server" ></asp:Label><br />
                                                    
                                                  <asp:Label ID="lblemailshlipen" runat="server" ></asp:Label><br />
                                                </address>
                                                
                                            </div>
                                        </div>
                                        <div class="col-xs-8 invoice-block">
                                            <ul class="list-unstyled amounts">
                                                <li>
                                                    <strong>Sub - Total amount:</strong>K.D
                                                    <asp:Label ID="lblSubtotal" runat="server"></asp:Label>
                                                </li>
                                                <li>
                                                    <strong>Discount:</strong><asp:Label ID="lblDiscount" runat="server"></asp:Label></li>
                                                <li>
                                                    <strong>VAT:</strong>
                                                    <asp:Label ID="lblVat" runat="server" Text="0"></asp:Label>%</li>
                                                <li>
                                                    <strong>Grand Total:</strong> K.D
                                                    <asp:Label ID="lblGalredTot" runat="server"></asp:Label>
                                                </li>
                                            </ul>
                                            <br />
                                            <a class="btn btn-lg blue hidden-print margin-bottom-5" onclick="javascript:window.print();">Print
                                   
                                <i class="fa fa-print"></i>
                                            </a>
                                            <a class="btn btn-lg green hidden-print margin-bottom-5">Submit Your Invoice
                                   
                                <i class="fa fa-check"></i>
                                            </a>
                                        </div>
                                        <hr />
                                        <center><h4><asp:Label ID="lblteglin" runat="server" ></asp:Label></h4></center>
                                        <hr />
                                        <center><h5> <asp:Label ID="lblbottumline" runat="server" ></asp:Label></h5></center>
                                    </div>
                                </div>
                                <!-- END PAGE BASE CONTENT -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
