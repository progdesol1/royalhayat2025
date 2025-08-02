<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Web.ReportMst.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
        <meta charset="utf-8" />
        <title>Metronic Admin Theme #4 | Buttons Datatable </title>
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta content="width=device-width, initial-scale=1" name="viewport" />
        <meta content="Preview page of Metronic Admin Theme #4 for buttons extension demos" name="description" />
        <meta content="" name="author" />
        <!-- BEGIN GLOBAL MANDATORY STYLES -->
        <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&amp;subset=all" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
        <!-- END GLOBAL MANDATORY STYLES -->
        <!-- BEGIN PAGE LEVEL PLUGINS -->
        <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
        <!-- END PAGE LEVEL PLUGINS -->
        <!-- BEGIN THEME GLOBAL STYLES -->
        <link href="assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
        <link href="assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
        <!-- END THEME GLOBAL STYLES -->
        <!-- BEGIN THEME LAYOUT STYLES -->
        <link href="assets/layouts/layout4/css/layout.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/layouts/layout4/css/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
        <link href="assets/layouts/layout4/css/custom.min.css" rel="stylesheet" type="text/css" />
        <!-- END THEME LAYOUT STYLES -->
        <link rel="shortcut icon" href="favicon.ico" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="portlet-body">
                                                            <%-- <div class="row">
                                                                <div class="col-md-6 col-sm-12">
                                                                    <div class="dataTables_length" id="sample_1_length">
                                                                        <label>
                                                                            Show
                                                                                       <asp:DropDownList class="form-control input-xsmall input-inline " ID="drpShowGrid" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpShowGrid_SelectedIndexChanged">
                                                                                           <asp:ListItem Value="5" Selected="True">5</asp:ListItem>
                                                                                           <asp:ListItem Value="15">15</asp:ListItem>
                                                                                           <asp:ListItem Value="20">20</asp:ListItem>
                                                                                           <asp:ListItem Value="30">30</asp:ListItem>
                                                                                           <asp:ListItem Value="40">40</asp:ListItem>
                                                                                           <asp:ListItem Value="50">50</asp:ListItem>
                                                                                           <asp:ListItem Value="100">100</asp:ListItem>
                                                                                       </asp:DropDownList>
                                                                            <%--<select name="sample_1_length" aria-controls="sample_1"  tabindex="-1" title="">
                                                                                            <option value="5">5</option>
                                                                                            <option value="15">15</option>
                                                                                            <option value="20">20</option>
                                                                                            <option value="-1">All</option>
                                                                                        </select>--%
                                                                                                entries</label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6 col-sm-12">
                                                                    <div id="sample_1_filter" class="dataTables_filter">
                                                                        <label>Search:<input type="search" class="form-control input-small input-inline" placeholder="" aria-controls="sample_1"></label>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                            <div class="table-scrollable">
                                                                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>--%>
                                                                <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1" id="sample_1">
                                                                    <thead class="repHeader">
                                                                        <tr>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblCustomerNo" Text="Customer No"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhCustomerName" Text="Customer Name"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhDeliveryCountry" Text="Deliverycountry"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhArea" Text="Area"></asp:Label></th>
                                                                            <%--<th>
                                                                                <asp:Label runat="server" ID="lblhDeliveryTime" Text="Deliverytime"></asp:Label></th>--%>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhAdvCollectionStatus" Text="Advcollectionstatus"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhPromotionOn" Text="Promotionon"></asp:Label></th>
                                                                            <%--<th>
                                                                                                            <asp:Label runat="server" ID="lblhlatituet" Text="Latituet"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhlongituet" Text="Longituet"></asp:Label></th>--%>

                                                                            <th style="width: 60px;">ACTION</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="CustomerId,DeliveryCountry,State,Area,DeliveryTime,AdvCollectionStatus,PromotionOn,Height" DataKeyNames="CustomerId,DeliveryCountry,State,Area,DeliveryTime,AdvCollectionStatus,PromotionOn,Height">

                                                                            <ItemTemplate>
                                                                                <tr>

                                                                                    <td>
                                                                                        <asp:LinkButton ID="LinkButton9" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerId")%>'>
                                                                                            <asp:Label ID="Label24" runat="server" Text='<%# Eval("CustomerId")%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="LinkButton6" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerId")%>'>
                                                                                            <asp:Label ID="lblCustomerId" runat="server" Text='<%#Eval("CustomerId")%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                    </td>

                                                                                    <td>
                                                                                        <asp:LinkButton ID="LinkButton7" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerId")%>'>
                                                                                            <asp:Label ID="lblDeliveryCountry" runat="server" Text='<%# Eval("DeliveryCountry")%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="LinkButton8" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerId")%>'>
                                                                                            <asp:Label ID="lblArea" runat="server" Text='<%# Eval("Area")%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                    </td>
                                                                                    <%-- <td>
                                                                                        <asp:LinkButton ID="LinkButton9" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerId")%>'>
                                                                                            <asp:Label ID="lblDeliveryTime" runat="server" Text='<%# Eval("DeliveryTime")%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                        <%--<asp:Label ID="lblDeliveryTime" runat="server" Text='<%# getsuppername11(Convert.ToInt32(Eval("DeliveryTime")))%>'></asp:Label>--%
                                                                                    </td>--%>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="LinkButton10" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerId")%>'>
                                                                                            <asp:Label ID="lblAdvCollectionStatus" runat="server" Text='<%#Eval("AdvCollectionStatus")%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="LinkButton11" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerId")%>'>
                                                                                            <asp:Label ID="lblPromotionOn" runat="server" Text='<%#Eval("PromotionOn")%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                    </td>
                                                                                    <%--<td>
                                                                                                                    <asp:Label ID="lbllatituet" runat="server" Text='<%# Eval("latituet")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lbllongituet" runat="server" Text='<%# Eval("longituet")%>'></asp:Label></td>--%>

                                                                                    <td>

                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("CustomerId")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                <td>
                                                                                                    <asp:LinkButton ID="btnDelete" runat="server" class="btn btn-sm red filter-cancel" CommandArgument='<%#  Eval("CustomerId")%>' CommandName="btnDelete" OnClientClick="return ConfirmMsg();"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                                <%-- <td><asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("JobId")%>'
                                                                                                                            CommandName="btnPrint" CommandArgument='<%# Eval("JobId")%>' runat="server" class="btn btn-sm green filter-submit margin-bottom"><i class="fa fa-print"></i></asp:LinkButton></td>
                                                                                                --%>
                                                                                            </tr>
                                                                                        </table>

                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>

                                                                    </tbody>
                                                                </table>
                                                                <%--</ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
                                                            </div>

                                                        </div>
    </div>
    </form>
    <div class="quick-nav-overlay"></div>
        <!-- END QUICK NAV -->
        <!--[if lt IE 9]>
<script src="assets/global/plugins/respond.min.js"></script>
<script src="assets/global/plugins/excanvas.min.js"></script> 
<script src="assets/global/plugins/ie8.fix.min.js"></script> 
<![endif]-->
        <!-- BEGIN CORE PLUGINS -->
        <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <!-- END CORE PLUGINS -->
        <!-- BEGIN PAGE LEVEL PLUGINS -->
        <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
        <script src="assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
        <!-- END PAGE LEVEL PLUGINS -->
        <!-- BEGIN THEME GLOBAL SCRIPTS -->
        <script src="assets/global/scripts/app.min.js" type="text/javascript"></script>
        <!-- END THEME GLOBAL SCRIPTS -->
        <!-- BEGIN PAGE LEVEL SCRIPTS -->
        <script src="assets/pages/scripts/table-datatables-buttons.min.js" type="text/javascript"></script>
        <!-- END PAGE LEVEL SCRIPTS -->
        <!-- BEGIN THEME LAYOUT SCRIPTS -->
        <script src="assets/layouts/layout4/scripts/layout.min.js" type="text/javascript"></script>
        <script src="assets/layouts/layout4/scripts/demo.min.js" type="text/javascript"></script>
        <script src="assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
        <script src="assets/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>
        <!-- END THEME LAYOUT SCRIPTS -->
</body>
</html>
