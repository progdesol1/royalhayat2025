<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="clothes.aspx.cs" Inherits="Web.POS.clothes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<!-- 
Template Name: Metronic - Responsive Admin Dashboard Template build with Twitter Bootstrap 3.3.2
Version: 3.7.0
Author: KeenThemes
Website: http://www.keenthemes.com/
Contact: support@keenthemes.com
Follow: www.twitter.com/keenthemes
Like: www.facebook.com/keenthemes
Purchase: http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes
License: You must have a valid license purchased only from themeforest(the above link) in order to legally use the theme for your project.
-->
<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Metronic | Pages - Portfolio</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="Content-type" content="text/html; charset=utf-8">
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css">
    <link href="../../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="../../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css">
    <link href="../../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="../../assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css">
    <link href="../../assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link href="../../assets/global/plugins/fancybox/source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/admin/pages/css/portfolio.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL STYLES -->
    <!-- BEGIN THEME STYLES -->
    <link href="../../assets/global/css/components-rounded.css" id="style_components" rel="stylesheet" type="text/css" />
    <link href="../../assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/admin/layout4/css/layout.css" rel="stylesheet" type="text/css" />
    <link id="style_color" href="../../assets/admin/layout4/css/themes/light.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/admin/layout4/css/custom.css" rel="stylesheet" type="text/css" />
    <!-- END THEME STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />


   <%-- <script type="text/javascript">
        function ace_itemCoutry(sender, e) {
            var HiddenField3 = $get('<%= HiddenField3.ClientID %>');
            HiddenField3.value = e.get_value();
        }
    </script>--%>

    <style>
        div.scrollmenu {
            background-color: #fff;
            overflow: auto;
            white-space: nowrap;
        }

            div.scrollmenu a {
                display: inline-block;
                color: white;
                text-align: center;
                padding: 5px;
                text-decoration: none;
            }

                div.scrollmenu a:hover {
                    background-color: #fff;
                }
    </style>

    <style>
        .Linkbuton {
            text-decoration: none;
        }
    </style>

    <script>


        function TotalPriceui() {
            var str = "";
            var DICUNTOTAL = 0;
            var Total = 0;
            var Priesh = 0;
            var DICUNT = 0;
            var qty = document.getElementById('txtQty').value != "" ? document.getElementById('txtQty').value : 0;
            Priesh = document.getElementById('lblPrice').value != "" ? document.getElementById('lblPrice').value : 0;

            var Discount = document.getElementById('txtDiscount').value != "" ? document.getElementById('txtDiscount').value : 0;
            Discount = Discount.trim();
            var flag = Discount.includes("%");
            if (flag == true) {
                str = Discount.replace("%", "");
                Total = qty * Priesh;
                DICUNT = str;
                var amt = (Total * (DICUNT / 100));
                document.getElementById('txtAmt').value = amt.toFixed(3); //(Total * (DICUNT / 100)).ToString("N3");
                DICUNTOTAL = Total - (Total * (DICUNT / 100));
                document.getElementById('txtAmount').value = DICUNTOTAL.toFixed(3);//.ToString("N3");
            }
            else {
                str = Discount;
                Total = qty * Priesh;
                DICUNT = str;
                document.getElementById('txtAmt').value = DICUNT;//.ToString("N3");
                DICUNTOTAL = Total - DICUNT;
                document.getElementById('txtAmount').value = DICUNTOTAL.toFixed(3);//.ToString("N3");
            }

        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
         <div class="col-md-12" style="overflow: scroll; height: 650px; padding: 0px">
                            <div class="tabbable tabbable-custom tabbable-noborder">

                                <div class="tab-content">
                                    <div class="tab-pane active">
                                        <!-- BEGIN FILTER -->
                                        <div class="portlet box blue-madison">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <i class="fa fa-globe"></i>Cloth's
                                                </div>

                                                <div class="tools">
                                                    <a href="javascript:;" class="reload" data-original-title="" title=""></a>
                                                    <a href="javascript:;" class="remove" data-original-title="" title=""></a>
                                                </div>

                                                

                                            </div>
                                            <div class="portlet-body">

                                                <div class="row">
                                                    <div class="col-md-12" style="left: 211px;">
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <div class="form-group">
                                                               
                                                                <div class="col-md-3" style="left: 241px;">
                                                                    <div class="actions btn-set">
                                                    <asp:Button ID="btnnewAdd" class="btn bg-white-gold" runat="server" Text="Store's"  />
                                                </div>
                                                                    </div>
                                                                <div class="col-md-3">
                                                                    <div class="actions btn-set">
                                                    <asp:Button ID="Button4" class="btn bg-yellow-gold" runat="server" Text="Clothes"  style="margin-left: 111px;"/>
                                                </div>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <%-- <asp:Button ID="btncustomer" runat="server" CssClass="btn yellow-gold dz-square" Text="SEARCH" />--%>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                   
                                                </div>
                                                <br />
                                                <br />
                                                <br />
                                                <div class="row">

                                                 <div class="col-md-3">
                                                                    <div class="actions btn-set">
                                                    <asp:Button ID="Button5" CssClass="btn btn-danger"  runat="server" Text="Men" style="margin-left: 465px;font-size: medium;" />
                                                </div>
                                                                </div>
                                                     <div class="col-md-3">
                                                                    <div class="actions btn-set">
                                                    <asp:Button ID="Button6"  runat="server" CssClass="btn btn-default" Text="Women"  style="margin-left: 220px; color: #800000;font-size: medium;" />
                                                </div>
                                                                </div>
                                                     <div class="col-md-3">
                                                                    <div class="actions btn-set">
                                                    <asp:Button ID="Button7" runat="server" CssClass="btn btn-default" Text="Kid's" style=" color: #800000;font-size: medium;" />
                                                </div>
                                                                </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3"></div>
                                                     <div class="col-md-3">
                                                                    <div class="actions btn-set">
                                                    <img src="Img/halfshirt.png" />
                                                </div>
                                                                </div>
                                                     <div class="col-md-3">
                                                                    <div class="actions btn-set">
                                                    <img src="Img/halfshirt1.png" />
                                                </div>
                                                                </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3"></div>
                                                    <div class="col-md-3">
                                                                    <div class="actions btn-set">
                                                    <img src="Img/halfshirt.png" />
                                                </div>
                                                                </div>
                                                     <div class="col-md-3">
                                                                    <div class="actions btn-set">
                                                    <img src="Img/halfshirt1.png" />
                                                </div>
                                                                </div>
                                                <div class="col-md-3"></div>
                                                </div>
                                                <div class="row">
                                                        <div class="actions btn-set">
                                                    <asp:Button ID="Button8" CssClass="btn btn-danger"  runat="server" Text="             Locate Store            " style="margin-left: 465px;font-size: medium;" />
                                                </div>
                                                </div>


                                                
                                              
                                            
                                            </div>

                                        </div>
                                    </div>

                                    <!-- END FILTER -->
                                </div>
                            </div>
                        </div>
     <div>
    <h3 align="center" style="color: #800000;">CLOTH'S</h3>
       
    </div>
        
        <asp:LinkButton ID="lnkstore" runat="server" style="margin-left: 544px;font-size:  x-large;color: #000000;">Store's</asp:LinkButton>
        <asp:LinkButton ID="lnkcloth" runat="server" style="margin-left: 117px;font-size:  x-large;color: #000000;">Clothes</asp:LinkButton>
       
        <br />
        <asp:Button ID="Button1" runat="server" Text="Men" CssClass="btn btn-danger" style="background-color: #800000;color: #ffffff;font-size: x-large; margin-left: 551px;"/>
        <asp:Button ID="Button2" runat="server" Text="Women" CssClass="btn btn-default" style="background-color: #ffffff;color: #800000;font-size: x-large;"/>
        <asp:Button ID="Button3" runat="server" Text="Kid's" CssClass="btn btn-default" style="background-color: #ffffff;color: #800000;font-size: x-large;"/>
        <br />
        <br />
        <br />
        <img src="Img/halfshirt.png"  style="margin-left: 544px;"/>
        <img src="Img/halfshirt1.png" />
        <br />
        <br />
        <img src="Img/halfshirt2.png" style="margin-left: 544px;"/>
        <img src="Img/halfshirt3.png" />
        <br />
        <br />
        <br />
        <asp:Button ID="btnloc" runat="server" Style="background-color: #800000;color: #ffffff;margin-left: 532px;font-size: x-large;"  Text="             Locate Store           "  />
        <br />
        <br />
        <br />

    </form>
    <!-- END FOOTER -->
    <!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
    <!-- BEGIN CORE PLUGINS -->
    <!--[if lt IE 9]>
<script src="../../assets/global/plugins/respond.min.js"></script>
<script src="../../assets/global/plugins/excanvas.min.js"></script> 
<![endif]-->
    <script src="../../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
    <!-- IMPORTANT! Load jquery-ui.min.js before bootstrap.min.js to fix bootstrap tooltip conflict with jquery ui tooltip -->
    <script src="../../assets/global/plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="../../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script type="text/javascript" src="../../assets/global/plugins/jquery-mixitup/jquery.mixitup.min.js"></script>
    <script type="text/javascript" src="../../assets/global/plugins/fancybox/source/jquery.fancybox.pack.js"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <script src="../../assets/global/scripts/metronic.js" type="text/javascript"></script>
    <script src="../../assets/admin/layout4/scripts/layout.js" type="text/javascript"></script>
    <script src="../../assets/admin/layout4/scripts/demo.js" type="text/javascript"></script>
    <script src="../../assets/admin/pages/scripts/portfolio.js"></script>
    <script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Demo.init(); // init demo features
            Portfolio.init();
        });
    </script>
    <!-- END JAVASCRIPTS -->
</body>
</html>
