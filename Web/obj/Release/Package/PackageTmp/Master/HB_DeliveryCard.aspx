<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="HB_DeliveryCard.aspx.cs" Inherits="Web.Master.HB_DeliveryCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>Delivery_Card</title> <style> @media all {.page-break { display: none; }} @media print { .page-break { display: block; page-break-before: always; }}</style>');
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
                                        <span class="caption-subject font-green-sharp bold uppercase"></span>
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:Button ID="btnPrint" runat="server" class="btn green-haze btn-circle" OnClientClick="return PrintPanel();" Text="Print" ValidationGroup="submit" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" Text="Cancel" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                                <asp:Panel ID="pnlContents" runat="server">
                                    <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
                                        <ItemTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="">
                                                        <%--<img src="../ReportMst/assets/global/img/HealthBar.png" alt="logo" class="logo-default" style="margin-top: 10px; width: 250px;" />--%>
                                                        <asp:Image ID="HealtybarLogo" ImageUrl="assets/global/img/HealthBar.png" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <table style="width: 100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="" colspan="2">

                                                            <h3 style="text-align: center;">&nbsp;Delivery Card
                                                            </h3>

                                                        </td>

                                                    </tr>

                                                    <tr>
                                                        <td style="" colspan="2">

                                                            <h3 style="text-align: center;">&nbsp;DATE:<asp:Label ID="lblDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpectedDelDate")).ToString("dd-MMM-yyyy") +"("+ Eval("NameOfDay")+")" %>'></asp:Label>
                                                            </h3>

                                                        </td>

                                                    </tr>
                                                    <%--<hr />--%>
                                                    <td style="" colspan="2">
                                                        <hr style="color: black;" />
                                                        <h3 style="text-align: center;">
                                                            <asp:Label ID="lblTimeDelivery" runat="server" Text='<%# GetDelivery(Convert.ToInt32(Eval("DeliveryTime")),Convert.ToInt32(Eval("DayNumber"))) %>'></asp:Label>
                                                        </h3>
                                                        <hr />
                                                    </td>
                                                    <%--<hr />--%>

                                                    <tr>
                                                        <td style="">
                                                            <strong>
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 20%;"><span class="muted">Client ID - Name </span></td>
                                                                        <%--<td><span class="muted">/ رقم الفاتورة ؛ :- &nbsp;</span></td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lblClientName" runat="server" Text='<%# GetCNAME(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label>
                                                                            <asp:Label ID="lblCustomerNo" Visible="false" runat="server" Text='<%# Eval("CustomerID") %>'></asp:Label>                                                                            
                                                                            <asp:Label ID="lblplan" runat="server" Visible="false" Text='<%# Eval("planid") %>'></asp:Label>
                                                                            <asp:Label ID="lblmeal" runat="server" Visible="false" Text='<%# Eval("MealType") %>'></asp:Label>
                                                                            <asp:Label ID="lblExpDate" runat="server" Visible="false" Text='<%# Eval("ExpectedDelDate") %>'></asp:Label>
                                                                            <asp:Label ID="lblDeliveryID" runat="server" Visible="false" Text='<%# Eval("DeliveryID") %>'></asp:Label>                                                                            
                                                                        </td>
                                                                    </tr>

                                                                    <%--<tr>
                                                                        <td style="width: 20%;"><span class="muted">Client Name:</span></td>                                                                        
                                                                        <td>
                                                                            <asp:Label ID="lblClientName" runat="server" Text='<%# GetCNAME(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label></td>
                                                                    </tr>--%>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblPlan1" runat="server" Text="Plan"></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblPlanName" runat="server" Text='<%# GetPlan(Convert.ToInt32(Eval("planid"))) %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 20%;"><span class="muted">Phone number:</span></td>
                                                                        <%--<td><span class="muted">/ فواتير العملاء :- &nbsp;</span></td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lblPhoneNo" runat="server" Text='<%# GetPHONE(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td style="width: 20%;"><span class="muted">Address:</span></td>
                                                                        <%--<td><span class="muted">/ فواتير العملاء :- &nbsp;</span></td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# GetAdd(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 20%;"><span class="muted">DriverID:</span></td>
                                                                        <td>
                                                                            <asp:Label ID="lblDriver" runat="server" Text='<%# GetDriver(Convert.ToInt32(Eval("DriverID"))) %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 20%;"><span class="muted">Remarks:</span></td>
                                                                        <%--<td><span class="muted">/ فواتير العملاء :- &nbsp;</span></td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lblRemark" runat="server" Text='<%# "This Delivery Number :" + Eval("DayNumber")+" Total:"+ GetTotal(Convert.ToInt32(Eval("CustomerID")),Convert.ToInt32(Eval("planid")))+" Delivered :"+ GetDeliverd(Convert.ToInt32(Eval("CustomerID")),Convert.ToInt32(Eval("planid"))) +" Your next Delivery on:"+ Convert.ToDateTime(Eval("NExtDeliveryDate")).ToString("dd-MMM-yyyy") %>'></asp:Label></td>
                                                                    </tr>

                                                                </table>

                                                            </strong>
                                                        </td>



                                                        <td style="text-align: right; width: 200px;"></td>
                                                    </tr>

                                                    <%--<hr />--%>
                                                </tbody>

                                            </table>
                                            <hr />
                                            <table style="width: 100%" border="1" cellpadding="1" cellspacing="1">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center; width: 3%"><strong>#</strong></th>
                                                        <th style="text-align: center; width: 10%"><strong>Category</strong></th>
                                                        <th style="text-align: center"><strong>Food Name </strong></th>
                                                        <%--<th style="text-align: center"><strong>Portion--%>
                                                        <th style="text-align: center"><strong>Qty</strong></th>
                                                        <th style="text-align: center"><strong>Cal</strong></th>
                                                        <th style="text-align: center"><strong>Prot</strong></th>
                                                        <th style="text-align: center"><strong>Cbs</strong></th>
                                                        <th style="text-align: center"><strong>Fat</strong></th>
                                                        <th style="text-align: center"><strong>Weight</strong></th>
                                                    <%--<br />
                                                            Qty + Cal + Prot + Cbs + Fat + Weight</strong></th>--%>
                                                        <th style="text-align: center"><strong>Inspection
                                                    <br />
                                                            check box</strong></th>

                                                    </tr>

                                                </thead>

                                                <tbody>
                                                    <asp:ListView ID="ListFoodDetail" runat="server">

                                                        <ItemTemplate>
                                                            <tr>
                                                                <th style="text-align: center"><%#Container.DataItemIndex+1%></th>
                                                                <th>
                                                                    <asp:Label ID="lblCustomerId" runat="server" Text='<%# getMealName(Convert.ToInt32(Eval("MealType")))%>'></asp:Label>                                                                  
                                                                </th>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# getmealprod(Convert.ToInt32(Eval("MealType")))%>'></asp:Label>                                                                    
                                                                </td>
                                                                
                                                                <td style="text-align: center">
                                                                    <%--<asp:Label ID="Label2" runat="server" Text='<%# getWeight(Convert.ToInt32(Eval("MealType"))) %>'></asp:Label>--%>
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                                </td>                                                               
                                                                <td style="text-align: center"><asp:Label ID="Label3" runat="server" Text='<%# Eval("Calories") %>'></asp:Label></td>
                                                                <td style="text-align: center"><asp:Label ID="Label4" runat="server" Text='<%# Eval("Protein") %>'></asp:Label></td>
                                                                <td style="text-align: center"><asp:Label ID="Label5" runat="server" Text='<%# Eval("Carbs") %>'></asp:Label></td>
                                                                <td style="text-align: center"><asp:Label ID="Label6" runat="server" Text='<%# Eval("Fat") %>'></asp:Label></td>
                                                                <td style="text-align: center"><asp:Label ID="Label7" runat="server" Text='<%# Eval("ItemWeight") %>'></asp:Label></td>
                                                                <td style="text-align: center"></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                                <tfoot>
                                                </tfoot>


                                            </table>
                                            <hr />
                                            <table style="width: 100%" border="0" cellpadding="1" cellspacing="1">
                                                <tbody>

                                                    <tr>
                                                        <td style="">
                                                            <strong>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 15%;"><span class="muted">Likes : </span></td>
                                                                        <%--<td><span class="muted">/ رقم الفاتورة ؛ :- &nbsp;</span></td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lblLIkes" runat="server"></asp:Label></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td style="width: 15%;"><span class="muted">Dislikes : </span></td>
                                                                        <%--<td><span class="muted">/ رقم الفاتورة ؛ :- &nbsp;</span></td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lblDisLIkes" runat="server"></asp:Label></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td style="width: 15%;"><span class="muted">Allergies : </span></td>
                                                                        <%--<td><span class="muted">/ رقم الفاتورة ؛ :- &nbsp;</span></td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lblAllergies" runat="server"></asp:Label></td>
                                                                    </tr>

                                                                </table>

                                                            </strong>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <hr />
                                            <center><h5> <asp:Label ID="lblbottumline" runat="server" Text="" ></asp:Label></h5></center>
                                            <%--HealthyBar Restaurant Salmiya, Block 2, Bin Masaud St. Building number 77. Restaurant Telephone number 22207320.--%>
                                            <div class="page-break"></div>
                                            
                                        </ItemTemplate>
                                    </asp:ListView>


                                </asp:Panel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
