<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="ContractPrintsInvoice1.aspx.cs" Inherits="Web.Master.ContractPrintsInvoice1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>Contract_print</title> <style> @media all {.page-break { display: none; }} @media print { .page-break { display: block; page-break-before: always; }}</style>');
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
    <script type="text/javascript">
        function showProgress() {
            $.blockUI({ message: '<img src="/../assets/image_1210200.gif" /><h2>  Processing...</h2>' });
        }

        function stopProgress() {
            $.unblockUI();
        }

        //$("#ContentPlaceHolder1_btnAdd").click(function () {
        //    $.blockUI({ message: '<h1>please wait...</h1>' });
        //});
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
                                        <span class="caption-subject font-green-sharp bold uppercase">Contract Print</span>
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:Button ID="btnPrint" runat="server" class="btn green-haze btn-circle" OnClientClick="return PrintPanel();" Text="Print" ValidationGroup="submit" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" Text="Cancel" />
                                    </div>
                                </div>
                                <asp:Panel ID="pnlWarningMsg" runat="server" Visible="false">
                                    <div class="alert alert-warning alert-dismissable">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                        <asp:Label ID="lblWarningMsg" Font-Size="Large" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="form-group" style="color: ">
                                            <asp:Label runat="server" ID="Label8" CssClass="col-md-4 control-label" Text="Customer"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="DrpCustomer" CssClass="form-control select2me input-medium" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrpCustomer_SelectedIndexChanged" onchange="showProgress()"></asp:DropDownList>                                                
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group" style="color: ">
                                            <asp:Label runat="server" ID="Label2" CssClass="col-md-4 control-label" Text="Plan"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="DrpPlan" CssClass="form-control select2me input-medium" runat="server"></asp:DropDownList>                                                
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-sm blue" OnClick="btnsearch_Click" OnClientClick="showProgress()" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-5">
                                        <asp:Label ID="Label1" runat="server" Visible="false" Text="Fill DropDown To Customer" ForeColor="#a94442"></asp:Label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:Label ID="Label3" runat="server" Visible="false" Text="Fill DropDown To Plan" ForeColor="#a94442"></asp:Label>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlContents" runat="server">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="">
                                                <%--<img src="../ReportMst/assets/global/img/HealthBar.png" alt="logo" class="logo-default" style="margin-top: 10px; width: 250px;" />--%>
                                                <asp:Image ID="HealtybarLogo" ImageUrl="assets/global/img/HealthBar.png" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="pnlhead" runat="server" Visible="false">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 20%; font-weight: bold">
                                                    <span class="muted">Customer Name </span>
                                                </td>
                                                <td style="width: 1%;">:-</td>
                                                <td>
                                                    <asp:Label ID="lblcustomer" runat="server" Text=""></asp:Label>
                                                </td>
                                                <%--</tr>
                                        <tr>--%>
                                                <td style="width: 20%; font-weight: bold">
                                                    <span class="muted">Contract No. </span>
                                                </td>
                                                <td style="width: 1%;">:-</td>
                                                <td>
                                                    <asp:Label ID="lblcontractno" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%; font-weight: bold">
                                                    <span class="muted">Plan Name </span>
                                                </td>
                                                <td style="width: 1%;">:-</td>
                                                <td>
                                                    <asp:Label ID="lblplan" runat="server" Text=""></asp:Label>
                                                </td>

                                                <%--</tr>
                                        <tr>--%>
                                                <td style="width: 20%; font-weight: bold">
                                                    <span class="muted">Delivery Time </span>
                                                </td>
                                                <td style="width: 1%;">:-</td>
                                                <td>
                                                    <asp:Label ID="lblDeliveryTime" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%; font-weight: bold">
                                                    <span class="muted">Contract Date </span>
                                                </td>
                                                <td style="width: 1%;">:-</td>
                                                <td>
                                                    <asp:Label ID="lblcontractDate" runat="server" Text=""></asp:Label>
                                                </td>
                                                <%--</tr>
                                        <tr>--%>
                                                <td style="width: 20%; font-weight: bold">
                                                    <span class="muted">Expectted Driver </span>
                                                </td>
                                                <td style="width: 1%;">:-</td>
                                                <td>
                                                    <asp:Label ID="lbldriver" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%; font-weight: bold">
                                                    <span class="muted">week of Day </span>
                                                </td>
                                                <td style="width: 1%;">:-</td>
                                                <td>
                                                    <asp:Label ID="lblweekofday" runat="server" Text=""></asp:Label>
                                                </td>
                                                <%--</tr>
                                         <tr>--%>
                                                <td style="width: 20%; font-weight: bold">
                                                    <span class="muted">Days Total </span>
                                                </td>
                                                <td style="width: 1%;">:-</td>
                                                <td>
                                                    <asp:Label ID="lbldaystotal" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%; font-weight: bold">
                                                    <span class="muted">Begin Date </span>
                                                </td>
                                                <td style="width: 1%;">:-</td>
                                                <td>
                                                    <asp:Label ID="lblBeginDate" runat="server" Text=""></asp:Label>
                                                </td>

                                                <td style="width: 20%; font-weight: bold">
                                                    <span class="muted">End Date </span>
                                                </td>
                                                <td style="width: 1%;">:-</td>
                                                <td>
                                                    <asp:Label ID="lblenddate" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%; font-weight: bold">
                                                    <span class="muted">Delivered </span>
                                                </td>
                                                <td style="width: 1%;">:-</td>
                                                <td>
                                                    <asp:Label ID="lblDelivered" runat="server" Text=""></asp:Label>
                                                </td>
                                                <%--</tr>
                                        <tr>--%>
                                                <td style="width: 20%; font-weight: bold">
                                                    <span class="muted">Remaining </span>
                                                </td>
                                                <td style="width: 1%;">:-</td>
                                                <td>
                                                    <asp:Label ID="lblRemaining" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <br />
                                    <table style="width: 100%" border="1" cellpadding="1" cellspacing="1">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center;"><strong>Plan Name</strong></th>
                                                <th style="text-align: center;"><strong>Meal Name</strong></th>
                                                <th style="text-align: center"><strong>Product Name</strong></th>
                                                <th style="text-align: center"><strong>Protein/carbs</strong></th>
                                                <th style="text-align: center"><strong>Delivery #</strong></th>
                                                <th style="text-align: center"><strong>Exp Del Date</strong></th>
                                                <th style="text-align: center"><strong>Week </strong></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="ListContract" runat="server">
                                                <ItemTemplate>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblPlan" runat="server" Text='<%# GetPlan(Convert.ToInt32( Eval("planid")))%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblMeal" runat="server" Text='<%# GetMeal1(Convert.ToInt32( Eval("MealType")))%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblProd2" runat="server" Text='<%# (Eval("ProdName1")) %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="lblproteinCarb1" runat="server" Text='<%# Eval("ItemWeight") %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label19" runat="server" Text='<%# Eval("DayNumber") %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label20" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpectedDelDate")).ToShortDateString() %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("NoOfWeek") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </tbody>
                                        <tfoot>
                                        </tfoot>

                                    </table>
                                </asp:Panel>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
