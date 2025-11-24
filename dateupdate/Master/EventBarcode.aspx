<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventBarcode.aspx.cs" Inherits="Web.Master.EventBarcode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Barcode</title>
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/bootstrap-datepicker/css/datepicker.css" />
    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" />
    <!-- END PAGE LEVEL STYLES -->
    <!-- BEGIN THEME STYLES -->
    <link href="../assets/global/css/components-rounded.css" id="style_components" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout4/css/layout.css" rel="stylesheet" type="text/css" />
    <link id="style_color" href="../assets/admin/layout4/css/themes/light.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout4/css/custom.css" rel="stylesheet" type="text/css" />
    <!-- END THEME STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />

</head>

<body style="margin: 0px; margin-top: 0px;">
    <form runat="server" id="form1">
        <div class="contentwrapper" id="contentwrapper">
            <%--style="width: 33%"--%>

            <%--<table style="width: 33%; margin-left: 12px; margin-top: 30px;">
                <asp:ListView ID="ListView1" GroupItemCount="1" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder runat="server" ID="groupPlaceholder" />
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                        </tr>
                    </GroupTemplate>
                    <GroupSeparatorTemplate>
                    </GroupSeparatorTemplate>
                    <ItemTemplate>
                        <td style="width: 99%; height: 99px;">
                            <table style="margin-top: -23px; width: 92%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbleventDetail" runat="server" Text='<%# Eval("Address") +" - "+ Eval("Notes")%>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblattendee" runat="server" Text='<%# Eval("Attendee") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblcompany" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRegAS" runat="server" Text='<%# Eval("PaymentReference") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <%# GetImage(Eval("BARCODE").ToString())%>
                                    
                                    </td>
                                </tr>

                            </table>

                        </td>
                    </ItemTemplate>
                    <ItemSeparatorTemplate>
                    </ItemSeparatorTemplate>
                </asp:ListView>
            </table>--%>




            <div class="tab-content">
                <div class="tab-pane active" id="tab_1">
                    <div class="row">
                        <div class="col-md-12">

                            <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
                                <ItemTemplate>

                                    <div id="Setupp" runat="server" class="col-md-6" style="margin-left: 10px;">
                                        <div class="portlet border box">

                                            <div class="portlet-body">
                                                <table>
                                                    <div class="row static-info">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbleventDetail" CssClass="col-md-12" runat="server" Text='<%# Eval("Address") +" - "+ Eval("Notes")%>' Style="font-size: large; font: bold 15px;"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </div>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblattendee" CssClass="col-md-12" runat="server" Text='<%# Eval("Attendee") %>' Style="font-size: large; font: bold 15px;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblcompany" CssClass="col-md-12" runat="server" Text='<%# Eval("CompanyName") %>' Style="font-size: large; font: bold 15px;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblRegAS" CssClass="col-md-12" runat="server" Text='<%# Eval("PaymentReference") %>' Style="font-size: large; font: bold 15px;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <div class="col-md-8">
                                                                <asp:Label ID="lblbarcodee" Visible="false" runat="server" Text='<%# Eval("BARCODE") %>'></asp:Label>
                                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# "Img/"+ (Eval("BARCODE").ToString()) + ".png" %>' Style="margin-top: 35px;" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:PlaceHolder ID="plBarCode" runat="server" />
                                                            </div>

                                                        </td>
                                                    </tr>

                                                </table>

                                            </div>
                                        </div>
                                        <div style="page-break-after: always;">
                                        </div>
                                    </div>

                                </ItemTemplate>
                            </asp:ListView>

                        </div>
                    </div>
                </div>
            </div>


        </div>
    </form>

    <script type="text/javascript">
        window.print();
        window.close();
    </script>


    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>

    <script src="../assets/global/scripts/metronic.js" type="text/javascript"></script>
    <script src="../assets/admin/layout4/scripts/layout.js" type="text/javascript"></script>
    <script src="../assets/admin/layout4/scripts/demo.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/datatable.js"></script>
    <script src="../assets/admin/pages/scripts/ecommerce-orders-view.js"></script>

    <script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Demo.init(); // init demo features
            EcommerceOrdersView.init();
        });
    </script>
</body>
</html>
