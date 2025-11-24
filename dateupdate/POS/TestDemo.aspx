<%@ Page Title="" Language="C#" MasterPageFile="~/POS/POSMaster.Master" AutoEventWireup="true" CodeBehind="TestDemo.aspx.cs" Inherits="Web.POS.TestDemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <title>Digital Edge Solutions | POS53
    </title>
    <meta name="description" content="Latest updates and statistic charts">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <!--begin::Web font -->
    <script src="https://ajax.googleapis.com/ajax/libs/webfont/1.6.16/webfont.js"></script>
    <script>
        WebFont.load({
            google: { "families": ["Poppins:300,400,500,600,700", "Roboto:300,400,500,600,700"] },
            active: function () {
                sessionStorage.fonts = true;
            }
        });
    </script>
    <link href="../assetsP/vendors/base/vendors.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../assetsP/demo/demo2/base/style.bundle.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="../assetsP/demo/demo2/media/img/logo/0.png" />

    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/morris/morris.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/fullcalendar/fullcalendar.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/jqvmap/jqvmap/jqvmap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout4/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout4/css/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout4/css/custom.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class='m-menu__submenu m-menu__submenu--classic m-menu__submenu--left'>
        <span class='m-menu__arrow m-menu__arrow--adjust'></span>
        <ul class='m-menu__subnav'>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'>
                <a href='/Sales/UnderCuntruction.aspx?MID=OngGL5LWTNimX1tyvh6RVA==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'>
                    <i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>System Settings</span>
                </a>
            </li>
            <div class='m-menu__submenu m-menu__submenu--classic m-menu__submenu--right'>
                <span class='m-menu__arrow '></span>
                <ul class='m-menu__subnav'>
                    <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Sales/UnderCuntruction.aspx?MID=OngGL5LWTNimX1tyvh6RVA==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>System-1</span></a></li>
                    <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Sales/UnderCuntruction.aspx?MID=OngGL5LWTNimX1tyvh6RVA==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>System-2</span></a></li>
                    <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Sales/UnderCuntruction.aspx?MID=OngGL5LWTNimX1tyvh6RVA==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>System-3</span></a></li>
                </ul>
            </div>

            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Setting/AddPrinterComman.aspx?MID=OngGL5LWTNgrG/FKV~/LLA==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Add Printer</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Sales/UnderCuntruction.aspx?MID=OngGL5LWTNjbL6/Qr81lMA==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Change Logo</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Master/tblCOUNTRY.aspx?MID=OngGL5LWTNj8ZyezBVCsyA==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Currencies</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Master/TBLGROUP.aspx?MID=OngGL5LWTNjaW/FdJOOjKw==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Customer Groups</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Master/TBLGROUP.aspx?MID=OngGL5LWTNivxhGW8Ex6hA==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Price Groups</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/ECOMM/Cat_Mst.aspx?MID=OngGL5LWTNjPIUN1tUocAw==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Categories</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Master/ICUOM.aspx?MID=OngGL5LWTNiijrmjX~B6AQ==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Units of Measure</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Master/REFTABLE.aspx?REFTYPE=BRAND&REFSUBTYPE=OTH&MID=OngGL5LWTNhTxOB1CElkrg==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Brands</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Sales/UnderCuntruction.aspx?MID=OngGL5LWTNhsqGdFOIrkzg==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Tax Rates</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Master/TBLLOCATION.aspx?MID=OngGL5LWTNg/vR6CZaHFmg==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Location</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Sales/UnderCuntruction.aspx?MID=OngGL5LWTNipYobKQ5i67g==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Email Templates</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Sales/UnderCuntruction.aspx?MID=OngGL5LWTNhJmIx9GqArqw==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Group Permissions</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Sales/UnderCuntruction.aspx?MID=OngGL5LWTNhIeWBZ4c5OBg==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Backups</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Sales/UnderCuntruction.aspx?MID=OngGL5LWTNiVEKxSyH/6ig==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Updates</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/CRM/ICEXTRACOST.aspx?MID=OngGL5LWTNgpNybsu27m3A==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Add Expenses</span></a></li>
            <li class='m-menu__item' data-redirect='true' aria-haspopup='true'><a href='/Sales/UnderCuntruction.aspx?MID=OngGL5LWTNgb~PJ5EFvewA==' onclick="return loadIframe('ifrm', this.href)" class='m-menu__link'><i class='m-menu__link-icon flaticon-users'></i><span class='m-menu__link-text'>Expense Categories</span></a></li>
        </ul>
    </div>

    <script src="../assetsP/vendors/base/vendors.bundle.js" type="text/javascript"></script>
    <script src="../assetsP/demo/demo2/base/scripts.bundle.js" type="text/javascript"></script>
    <!--end::Base Scripts -->
    <!--begin::Page Snippets -->
    <script src="../assetsP/app/js/dashboard.js" type="text/javascript"></script>

    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/moment.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/morris/morris.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/morris/raphael-min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/counterup/jquery.waypoints.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/counterup/jquery.counterup.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amcharts/amcharts.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amcharts/serial.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amcharts/pie.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amcharts/radar.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amcharts/themes/light.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amcharts/themes/patterns.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amcharts/themes/chalk.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/ammap/ammap.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/ammap/maps/js/worldLow.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/amcharts/amstockcharts/amstock.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/horizontal-timeline/horizontal-timeline.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.resize.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.categories.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-easypiechart/jquery.easypiechart.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.sparkline.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jqvmap/jqvmap/jquery.vmap.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.russia.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.world.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.europe.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.germany.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.usa.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jqvmap/jqvmap/data/jquery.vmap.sampledata.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/dashboard.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/layout4/scripts/layout.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/layout4/scripts/demo.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $('#clickmewow').click(function () {
                $('#radio1003').attr('checked', 'checked');
            });
        })
    </script>
</asp:Content>
