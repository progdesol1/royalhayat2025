<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="AMPMDAYkitchenPrint.aspx.cs" Inherits="Web.Master.AMPMDAYkitchenPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>kitchen</title> <style> @media all {.page-break { display: none; }} @media print { .page-break { display: block; page-break-before: always; }}</style>');
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

                                    <div class="actions btn-set">
                                        <asp:Button ID="btnPrint" runat="server" class="btn green-haze btn-circle" Text="Print" OnClientClick="return PrintPanel();" ValidationGroup="submit" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" Text="Cancel" OnClick="btnCancel_Click" />
                                    </div>
                                    <asp:Panel ID="pnlContents" runat="server">
                                        <%-- Listview1 --%>
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
                                                                <%--Chef Kitchen Preparation List--%>
                                                                <h3 style="text-align: center;">&nbsp;Chef&nbsp;<asp:Label ID="lblkitchH3" runat="server"></asp:Label>&nbsp;List
                                                                </h3>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>
                                                                    <table style="width: 100%">
                                                                        <asp:Panel ID="PanelTR" runat="server">
                                                                            <tr>
                                                                                <td style="width: 20%;">
                                                                                    <span class="muted">Delivery Time </span>
                                                                                </td>
                                                                                <td style="width: 1%;">:- </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl" runat="server" Text='<%# Eval("Delivery") %>'></asp:Label>
                                                                                    <asp:Label ID="lblMyid" Visible="false" runat="server" Text='<%# Eval("MyID") %>'></asp:Label>
                                                                                    <asp:Label ID="lblprod" Visible="false" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                                                    <asp:Label ID="lbldelevarytime" Visible="false" runat="server" Text='<%# Eval("DeliverTime") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </asp:Panel>
                                                                        <tr>
                                                                            <td style="width: 20%;">
                                                                                <span class="muted">Delivery Date </span>
                                                                            </td>
                                                                            <td style="width: 1%;">:- </td>
                                                                            <td>
                                                                                <asp:Label ID="lblExpDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpectedDelDate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
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
                                                                <asp:Label runat="server" ID="lblSN"><br />SN#</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" Font-Size="Large" ID="lblProduct" Text="Product"></asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" Font-Size="Large" ID="lblNormal" Text="Normal"></asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" Font-Size="Large" ID="lbl150grms" Text="100grms"></asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" Font-Size="Large" ID="lbl200grms" Text="150grms"></asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" Font-Size="Large" ID="lbl250grms" Text="200grms"></asp:Label></th>
                                                        </tr>
                                                        <tbody>
                                                            <asp:ListView ID="ListView2" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: center;">
                                                                            <asp:Label ID="lblNormalQty" runat="server" Font-Size="Medium" Text='<%# Eval("NormalQty")%>'></asp:Label></td>
                                                                        <td style="text-align: center;">
                                                                            <asp:Label ID="lblQty150" runat="server" Font-Size="Medium" Text='<%# Eval("Qty150") %>'></asp:Label></td>
                                                                        <td style="text-align: center;">
                                                                            <asp:Label ID="lblQty200" runat="server" Font-Size="Medium" Text='<%# Eval("Qty200") %>'></asp:Label></td>
                                                                        <td style="text-align: center;">
                                                                            <asp:Label ID="lblQty250" runat="server" Font-Size="Medium" Text='<%# Eval("Qty250") %>'></asp:Label></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </tbody>
                                                    </thead>
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
    </div>
</asp:Content>
