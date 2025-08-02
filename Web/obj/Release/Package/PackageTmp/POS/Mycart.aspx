<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mycart.aspx.cs" Inherits="Web.POS.Mycart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
   <%--<title></title>--%>
    <meta charset="utf-8">
    <title>E commerce | Home - Index</title>


    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />

    <meta content="Digital53 Shop UI description" name="description" />
    <meta content="Digital53 Shop UI keywords" name="keywords" />
    <meta content="keenthemes" name="author" />

    <meta property="og:site_name" content="-CUSTOMER VALUE-" />
    <meta property="og:title" content="-CUSTOMER VALUE-" />
    <meta property="og:description" content="-CUSTOMER VALUE-" />
    <meta property="og:type" content="website" />
    <meta property="og:image" content="-CUSTOMER VALUE-" />
    <!-- link to image for socio -->
    <meta property="og:url" content="-CUSTOMER VALUE-" />

    <link rel="shortcut icon" href="favicon.ico">

    <!-- Fonts START -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700|PT+Sans+Narrow|Source+Sans+Pro:200,300,400,600,700,900&amp;subset=all" rel="stylesheet" type="text/css" />
    <link href="http://fonts.googleapis.com/css?family=Source+Sans+Pro:200,300,400,600,700,900&amp;subset=all" rel="stylesheet" type="text/css" />
    <!--- fonts for slider on the index page -->
    <!-- Fonts END -->

    <!-- Global styles START -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css">
    <link href="assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css">
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css">
    <link href="assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- Global styles END -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/bootstrap-wysihtml5/bootstrap-wysihtml5.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/bootstrap-markdown/css/bootstrap-markdown.min.css">
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/bootstrap-datepicker/css/datepicker.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/extensions/Scroller/css/dataTables.scroller.min.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/extensions/ColReorder/css/dataTables.colReorder.min.css" />
    <link rel="stylesheet" type="text/css" href="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- Page level plugin styles START -->
    <link href="assets/global/plugins/fancybox/source/jquery.fancybox.css" rel="stylesheet" />
    <link href="assets/global/plugins/carousel-owl-carousel/owl-carousel/owl.carousel.css" rel="stylesheet" />
    <link href="assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/slider-layer-slider/css/layerslider.css" rel="stylesheet" />
    <link href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/rateit/src/rateit.css" rel="stylesheet" type="text/css" />
    <!-- Page level plugin styles END -->
    <!-- BEGIN THEME STYLES -->
    <link href="assets/global/css/components-rounded.css" id="style_components" rel="stylesheet" type="text/css" />
    <link href="assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="assets/admin/layout4/css/layout.css" rel="stylesheet" type="text/css" />
    <link id="style_color" href="assets/admin/layout4/css/themes/light.css" rel="stylesheet" type="text/css" />

    <link href="assets/admin/layout4/css/custom.css" rel="stylesheet" type="text/css" />
    <!-- END THEME STYLES -->
    <!-- Theme styles START -->
    <link href="assets/global/css/components.css" rel="stylesheet" />
    <link href="assets/frontend/layout/css/style.css" rel="stylesheet" />
    <link href="assets/frontend/pages/css/style-shop.css" rel="stylesheet" type="text/css" />
    <link href="assets/frontend/pages/css/style-layer-slider.css" rel="stylesheet" />
    <link href="assets/frontend/layout/css/style-responsive.css" rel="stylesheet" />
    <link href="assets/frontend/layout/css/themes/red.css" rel="stylesheet" id="stylecolor">
    <link href="assets/frontend/layout/css/custom.css" rel="stylesheet" />

    <link rel="shortcut icon" href="favicon.ico" />
    </head>
<body>
    <form id="form1" runat="server">
    <div>
      <div class="top-cart-block">

                            <div class="top-cart-info">
                                <a href="javascript:void(0);" class="top-cart-info-count">
                                    <asp:Label ID="lblitemcount" runat="server"></asp:Label>
                                    items</a>
                                <a href="javascript:void(0);" class="top-cart-info-value">K.D<asp:Label ID="lbltotalPriesh" runat="server"></asp:Label></a>
                            </div>
                            <i class="fa fa-shopping-cart"></i>

                            <div class="top-cart-content-wrapper">
                                <div class="top-cart-content">
                                    <ul class="scroller" style="height: 250px;">
                                         <li>
                                                        <a href="shop-item.html">
                                                            <img src="Img/halfshirt.png" alt="Rolex Classic Watch" width="37" height="34"></a>
                                            
                                                        <span class="cart-content-count">x 1</span>
                                                        <strong><a href="shop-item.html">Rolex Classic Watch</a></strong>
                                                        <em>$1230</em>
                                                        <a href="javascript:void(0);" class="del-goods">&nbsp;</a>
                                                    </li>                                                    
                                                    <li>
                                                        <a href="shop-item.html">
                                                            <img src="Img/halfshirt1.png" alt="Rolex Classic Watch" width="37" height="34"></a>
                                                        <span class="cart-content-count">x 1</span>
                                                        <strong><a href="shop-item.html">Rolex Classic Watch</a></strong>
                                                        <em>$1230</em>
                                                        <a href="javascript:void(0);" class="del-goods">&nbsp;</a> 
                                                    </li>
                                    </ul>
                                    <div class="text-right">
                                        <a href="ProductCart.aspx" class="btn btn-default">View Cart</a>
                                        <a href="CheckOut.aspx" class="btn btn-primary">Checkout</a>
                                    </div>
                                </div>
                            </div>

                        </div>
    </div>
    </form>
      <!-- END FOOTER -->

                <!-- BEGIN fast view of a product -->

                <!-- END fast view of a product -->

                <!-- Load javascripts at bottom, this will reduce page load time -->
                <!-- BEGIN CORE PLUGINS (REQUIRED FOR ALL PAGES) -->
                <!--[if lt IE 9]>
    <script src="assets/global/plugins/respond.min.js"></script>  
    <![endif]-->
                <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
                <script src="assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
                <script src="assets/global/plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
                <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
                <script src="assets/frontend/layout/scripts/back-to-top.js" type="text/javascript"></script>
                <script src="assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
                <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
                <script src="assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
                <script src="assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script>
                <script src="assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
                <!-- END CORE PLUGINS -->
                <!-- BEGIN PAGE LEVEL PLUGINS -->
                <script type="text/javascript" src="assets/global/plugins/jquery-validation/js/jquery.validate.min.js"></script>
                <script type="text/javascript" src="assets/global/plugins/jquery-validation/js/additional-methods.min.js"></script>
                <script type="text/javascript" src="assets/global/plugins/select2/select2.min.js"></script>
                <script type="text/javascript" src="assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
                <script type="text/javascript" src="assets/global/plugins/datatables/extensions/TableTools/js/dataTables.tableTools.min.js"></script>
                <script type="text/javascript" src="assets/global/plugins/datatables/extensions/ColReorder/js/dataTables.colReorder.min.js"></script>
                <script type="text/javascript" src="assets/global/plugins/datatables/extensions/Scroller/js/dataTables.scroller.min.js"></script>
                <script type="text/javascript" src="assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
                <script type="text/javascript" src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
                <script type="text/javascript" src="assets/global/plugins/bootstrap-wysihtml5/wysihtml5-0.3.0.js"></script>
                <script type="text/javascript" src="assets/global/plugins/bootstrap-wysihtml5/bootstrap-wysihtml5.js"></script>
                <script type="text/javascript" src="assets/global/plugins/ckeditor/ckeditor.js"></script>
                <script type="text/javascript" src="assets/global/plugins/bootstrap-markdown/js/bootstrap-markdown.js"></script>
                <script type="text/javascript" src="assets/global/plugins/bootstrap-markdown/lib/markdown.js"></script>
                <script src="assets//global/plugins/flot/jquery.flot.js" type="text/javascript"></script>
                <script src="assets//global/plugins/flot/jquery.flot.resize.js" type="text/javascript"></script>
                <script src="assets//global/plugins/flot/jquery.flot.categories.js" type="text/javascript"></script>
                <!-- END PAGE LEVEL PLUGINS -->
                <!-- BEGIN PAGE LEVEL JAVASCRIPTS (REQUIRED ONLY FOR CURRENT PAGE) -->
                <script src="assets/global/plugins/fancybox/source/jquery.fancybox.pack.js" type="text/javascript"></script>
                <!-- pop up -->
                <script src="assets/global/plugins/carousel-owl-carousel/owl-carousel/owl.carousel.min.js" type="text/javascript"></script>
                <!-- slider for products -->
                <script src='assets/global/plugins/zoom/jquery.zoom.min.js' type="text/javascript"></script>
                <!-- product zoom -->
                <script src="assets/global/plugins/bootstrap-touchspin/bootstrap.touchspin.js" type="text/javascript"></script>
                <script src="assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
                <script src="assets/global/plugins/rateit/src/jquery.rateit.js" type="text/javascript"></script>
                <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js" type="text/javascript"></script>
                <!-- for slider-range -->
                <!-- Quantity -->

                <!-- BEGIN LayerSlider -->
                <script src="assets/global/plugins/slider-layer-slider/js/greensock.js" type="text/javascript"></script>
                <!-- External libraries: GreenSock -->
                <script src="assets/global/plugins/slider-layer-slider/js/layerslider.transitions.js" type="text/javascript"></script>
                <!-- LayerSlider script files -->
                <script src="assets/global/plugins/slider-layer-slider/js/layerslider.kreaturamedia.jquery.js" type="text/javascript"></script>
                <!-- LayerSlider script files -->
                <script src="assets/frontend/pages/scripts/layerslider-init.js" type="text/javascript"></script>

                <!-- END LayerSlider -->
                <script src="assets/global/scripts/metronic.js" type="text/javascript"></script>
                <script src="assets/frontend/layout/scripts/layout.js" type="text/javascript"></script>
                <script src="assets/frontend/pages/scripts/checkout.js" type="text/javascript"></script>
                <script src="assets/admin/layout4/scripts/demo.js" type="text/javascript"></script>
                <script src="assets/admin/pages/scripts/form-validation.js"></script>
                <script src="assets/admin/pages/scripts/table-advanced.js"></script>
                <script src="assets//admin/pages/scripts/ecommerce-index.js"></script>
               
                <!-- END PAGE LEVEL JAVASCRIPTS -->
</body>
</html>
