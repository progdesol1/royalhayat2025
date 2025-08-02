<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="herbs.aspx.cs" Inherits="Web.POS.herbs" %>


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
        <div>

            <div class="col-md-12" style="overflow: scroll; height: 650px; padding: 0px">
                <div class="tabbable tabbable-custom tabbable-noborder">

                    <div class="tab-content">
                        <div class="tab-pane active">
                            <!-- BEGIN FILTER -->
                            <div class="portlet box blue-madison">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-globe"></i>Medical Store
                                    </div>

                                    <div class="tools">
                                        <a href="javascript:;" class="reload" data-original-title="" title=""></a>
                                        <a href="javascript:;" class="remove" data-original-title="" title=""></a>
                                    </div>



                                </div>
                                <div class="portlet-body">
                                    <div class="col-md-12">
                                    </div>
                                    <div class="form-wizard">
                                        <div class="form-body">
                                            <ul class="nav nav-pills nav-justified steps">
                                                <li>
                                                    <a href="#tab1" data-toggle="tab" class="step">

                                                        <span class="desc" style="font-size: x-large; color: #f1353d">
                                                            <i class="fa fa-check"></i>Company </span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#tab2" data-toggle="tab" class="step">

                                                        <span class="desc" style="font-size: x-large; color: #000000">
                                                            <i class="fa fa-check"></i>Category </span>
                                                    </a>
                                                </li>

                                            </ul>
                                            <div id="bar" class="progress progress-striped" role="progressbar">
                                                <div class="progress-bar progress-bar-success">
                                                </div>
                                            </div>
                                            <div class="tab-content">
                                                <div class="alert alert-danger display-none">
                                                    <button class="close" data-dismiss="alert"></button>
                                                    You have some form errors. Please check below.
                                                </div>
                                                <div class="alert alert-success display-none">
                                                    <button class="close" data-dismiss="alert"></button>
                                                    Your form validation is successful!
                                                </div>
                                                <div class="tab-pane active" id="tab1">
                                                    <div class="row" >
                                                    <div class="form-group">
                                                        
                                                        <div class="col-md-3">
                                                            <img src="Img/h1.png" />
                                                        </div>
                                                    </div>
                                                        </div>
                                                    <div class="row" >
                                                <div class="form-group">
                                                        <div class="col-md-3">
                                                            <img src="Img/h2.png" />
                                                        </div>
                                                    </div>
                                                        </div>
                                                    <div class="row" style="margin-left: 112px;">
                                                    <div class="form-group">
                                                        <div class="col-md-3"></div>
                                                        <div class="col-md-3">
                                                            <img src="Img/h3.png" />
                                                        </div>
                                                    </div>
                                                <div class="form-group">
                                                        <div class="col-md-3">
                                                            <img src="Img/h4.png" />
                                                        </div>
                                                    <div class="col-md-3"></div>
                                                    </div>
                                                    </div>
                                                    <div class="row" style="margin-left: 112px;">
                                                    <div class="form-group">
                                                        <div class="col-md-3"></div>
                                                        <div class="col-md-3">
                                                            <img src="Img/h5.png" />
                                                        </div>
                                                    </div>
                                                <div class="form-group">
                                                        <div class="col-md-3">
                                                            <img src="Img/h6.png" />
                                                        </div>
                                                    <div class="col-md-3"></div>
                                                    </div>
                                                    </div>
                                                    <%--<div class="col-md-1"></div>--%>
                                                  
                                                </div>

                                                <div class="tab-pane" id="tab2">
                                                    <h3 class="block">Provide your profile details</h3>
                                                    <%--<div class="form-group">
													<label class="control-label col-md-3">Fullname <span class="required">
													* </span>
													</label>
													<div class="col-md-4">
														<input type="text" class="form-control" name="fullname"/>
														<span class="help-block">
														Provide your fullname </span>
													</div>
												</div>--%>
                                                    <%--<div class="form-group">
													<label class="control-label col-md-3">Phone Number <span class="required">
													* </span>
													</label>
													<div class="col-md-4">
														<input type="text" class="form-control" name="phone"/>
														<span class="help-block">
														Provide your phone number </span>
													</div>
												</div>--%>
                                                    <%--<div class="form-group">
													<label class="control-label col-md-3">Gender <span class="required">
													* </span>
													</label>
													<div class="col-md-4">
														<div class="radio-list">
															<label>
															<input type="radio" name="gender" value="M" data-title="Male"/>
															Male </label>
															<label>
															<input type="radio" name="gender" value="F" data-title="Female"/>
															Female </label>
														</div>
														<div id="form_gender_error">
														</div>
													</div>
												</div>--%>
                                                    <%--<div class="form-group">
													<label class="control-label col-md-3">Address <span class="required">
													* </span>
													</label>
													<div class="col-md-4">
														<input type="text" class="form-control" name="address"/>
														<span class="help-block">
														Provide your street address </span>
													</div>
												</div>--%>
                                                    <%--<div class="form-group">
													<label class="control-label col-md-3">City/Town <span class="required">
													* </span>
													</label>
													<div class="col-md-4">
														<input type="text" class="form-control" name="city"/>
														<span class="help-block">
														Provide your city or town </span>
													</div>
												</div>--%>

                                                    <%--<div class="form-group">
													<label class="control-label col-md-3">Remarks</label>
													<div class="col-md-4">
														<textarea class="form-control" rows="3" name="remarks"></textarea>
													</div>
												</div>--%>
                                                </div>


                                            </div>
                                        </div>

                                    </div>

                                    <br />
                                    <br />
                                    <%-- <div class="row">
                                        <div class="col-md-3">
                                            <img src="Img/shirts.png" />
                                        </div>
                                        <div class="col-md-3">
                                            <img src="Img/Tshirts.png" />
                                        </div>
                                        <div class="col-md-3">
                                            <img src="Img/pant.png" />
                                        </div>
                                        <div class="col-md-3">
                                            <img src="Img/gutra.png" />
                                        </div>
                                    </div>--%>
                                    <br />
                                    <br />
                                    <br />


                                </div>

                            </div>
                        </div>

                        <!-- END FILTER -->
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
