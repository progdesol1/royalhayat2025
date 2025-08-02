<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="AMPMnewPrint.aspx.cs" Inherits="Web.Master.AMPMnewPrint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <script type="text/javascript">
       function PrintPanel() {
           var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>Driver_CheckList</title> <style> @media all {.page-break { display: none; }} @media print { .page-break { display: block; page-break-before: always; }}</style>');
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
                                    <%-- <div class="caption">
                                        <i class="icon-basket font-green-sharp"></i>
                                        <span class="caption-subject font-green-sharp bold uppercase">Healthy Bar</span>
                                    </div>--%>
                                    <div class="actions btn-set">
                                        <asp:Button ID="btnPrint" runat="server" class="btn green-haze btn-circle" Text="Print" OnClientClick="return PrintPanel();" ValidationGroup="submit" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" Text="Cancel" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                                <asp:Panel ID="pnlContents" runat="server">
                                    <%-- Listview1 --%>
                                    <asp:ListView ID="ListViewDriverhead" runat="server" OnItemDataBound="ListViewDriverhead_ItemDataBound">
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
                                                            <h3 style="text-align: center;">&nbsp;Driver Check List(<asp:Label ID="Label7" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpectedDelDate")).ToString("dd-MMM-yyyy") %>'></asp:Label>)
                                                            </h3>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <strong>
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 20%;">
                                                                            <span class="muted">Delivery Date</span>
                                                                        </td>
                                                                        <td style="width: 1%;">:-</td>
                                                                        <td>
                                                                            <asp:Label ID="lblExpDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpectedDelDate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 20%;">
                                                                            <span class="muted">Driver Name</span>
                                                                        </td>
                                                                        <td style="width: 1%;">:-</td>
                                                                        <td>
                                                                            <asp:Label ID="Label9" runat="server" Text='<%# GetDriver(Convert.ToInt32(Eval("DriverID"))) %>'></asp:Label>
                                                                            <asp:Label ID="lbldriverID" runat="server" Visible="false" Text='<%# Eval("DriverID") %>'></asp:Label>
                                                                            <asp:Label ID="lblcustomerID" runat="server" Visible="false" Text='<%# Eval("CustomerID") %>'></asp:Label>
                                                                            <asp:Label ID="lblplan" runat="server" Visible="false" Text='<%# Eval("planid") %>'></asp:Label>
                                                                             <asp:Label ID="lblExpDelDate" runat="server" Visible="false" Text='<%# Eval("ExpectedDelDate") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 20%;">
                                                                            <span class="muted">Delivery</span>
                                                                        </td>
                                                                        <td style="width: 1%;">:-</td>
                                                                        <td>
                                                                            <asp:Label ID="Label8" runat="server" Text='<%# GetTime(Convert.ToInt32(Eval("DeliveryTime"))) %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                   <%-- <tr>
                                                                        <td style="width: 20%;">
                                                                            <span class="muted">Plan</span>
                                                                        </td>
                                                                        <td style="width: 1%;">:-</td>
                                                                        <td>
                                                                            <asp:Label ID="Label14" runat="server" Text='<%# GetPlan(Convert.ToInt32(Eval("planid"))) %>'></asp:Label>
                                                                        </td>
                                                                    </tr>--%>
                                                                </table>
                                                            </strong>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <br />
                                            <%-- Listview2 --%>
                                            <table style="width: 100%" border="1" cellpadding="1" cellspacing="1">
                                                <thead class="repHeader">
                                                    <tr>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblSN" Text="SN#"></asp:Label></th>
                                                       <%-- <th>
                                                            <asp:Label runat="server" ID="lblDay" Text="Day"></asp:Label>
                                                        </th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label10" Text="Driver Name"></asp:Label>
                                                        </th>--%>
                                                        
                                                        <th>
                                                            <asp:Label runat="server" ID="lblNAME" Text="Subscriber ID & Name"></asp:Label>
                                                        </th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblTel" Text="Telephone"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblAddress" Text="Address"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label11" Text="Plan"></asp:Label>
                                                        </th>
                                                        <th style="width:100px;">
                                                            <asp:Label runat="server" ID="Label14">Contract<br />Start Date</asp:Label></th>
                                                        <th style="width:100px;">
                                                            <asp:Label runat="server" ID="lblDate">Contract<br />End Date</asp:Label></th>
                                                        <th style="width:80px;">
                                                            <asp:Label runat="server" ID="Label10">Delivery<br />Status</asp:Label></th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview1" runat="server">

                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <%--<td>
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpectedDelDate")).ToShortDateString() %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" Text='<%# GetDriver(Convert.ToInt32(Eval("DriverID"))) %>'></asp:Label>
                                                                    </td>--%>                                                                
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# GetCustomer(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label>
                                                                    <asp:Label ID="Label2" runat="server" Visible="false" Text='<%# Eval("CustomerID") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# GetPhone(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# GetAdd(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label>
                                                                    <asp:Label ID="lblCustomerID" Visible="false" runat="server" Text='<%# Eval("CustomerID") %>'></asp:Label>
                                                                    <asp:Label ID="lblpaln" Visible="false" runat="server" Text='<%# Eval("planid") %>'></asp:Label>
                                                                    <asp:Label ID="lblmytransiid" Visible="false" runat="server" Text='<%# Eval("MYTRANSID") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label13" runat="server" Text='<%# GetPlan(Convert.ToInt32(Eval("planid"))) %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label15" runat="server" Text='<%# Convert.ToDateTime(Eval("StartDate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Convert.ToDateTime(Eval("EndDate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                            </table>
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
