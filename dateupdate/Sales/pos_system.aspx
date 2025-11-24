<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pos_system.aspx.cs" Inherits="Web.POS.pos_system" %>

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
<html lang="en">
<!--<![endif]-->
<!-- BEGIN HEAD -->
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
    <link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css">
    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="../assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css">
    <link href="../assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link href="../assets/global/plugins/fancybox/source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/pages/css/portfolio.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL STYLES -->
    <!-- BEGIN THEME STYLES -->
    <link href="../assets/global/css/components-rounded.css" id="style_components" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout4/css/layout.css" rel="stylesheet" type="text/css" />
    <link id="style_color" href="../assets/admin/layout4/css/themes/light.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout4/css/custom.css" rel="stylesheet" type="text/css" />
    <!-- END THEME STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />


    <script type="text/javascript">
        function ace_itemCoutry(sender, e) {
            var HiddenField3 = $get('<%= HiddenField3.ClientID %>');
            HiddenField3.value = e.get_value();
        }
    </script>

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
  
    <script type="text/javascript">
        function TOPON() {            
            document.getElementById("ontop").style.marginTop = "40px";
        }
    </script>
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
<!-- END HEAD -->
<!-- BEGIN BODY -->
<!-- DOC: Apply "page-header-fixed-mobile" and "page-footer-fixed-mobile" class to body element to force fixed header or footer in mobile devices -->
<!-- DOC: Apply "page-sidebar-closed" class to the body and "page-sidebar-menu-closed" class to the sidebar menu element to hide the sidebar by default -->
<!-- DOC: Apply "page-sidebar-hide" class to the body to make the sidebar completely hidden on toggle -->
<!-- DOC: Apply "page-sidebar-closed-hide-logo" class to the body element to make the logo hidden on sidebar toggle -->
<!-- DOC: Apply "page-sidebar-hide" class to body element to completely hide the sidebar on sidebar toggle -->
<!-- DOC: Apply "page-sidebar-fixed" class to have fixed sidebar -->
<!-- DOC: Apply "page-footer-fixed" class to the body element to have fixed footer -->
<!-- DOC: Apply "page-sidebar-reversed" class to put the sidebar on the right side -->
<!-- DOC: Apply "page-full-width" class to the body element to have full width page without the sidebar menu -->
<body>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!-- BEGIN HEADER -->

        <!-- END HEADER -->

        <!-- BEGIN CONTAINER -->
        <div class="page-container">

            <!-- BEGIN CONTENT -->
            <asp:Panel ID="panelMsg" runat="server" Visible="false">
                <div class="alert alert-danger alert-dismissable">
                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                    <asp:Label ID="lblErreorMsg" runat="server"></asp:Label>
                </div>
            </asp:Panel>
            <div class="page-content">
                <!-- BEGIN SAMPLE PORTLET CONFIGURATION MODAL FORM-->
                <div class="modal fade" id="portlet-config" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                <h4 class="modal-title">Modal title</h4>
                            </div>
                            <div class="modal-body">
                                Widget settings form goes here
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn blue">Save changes</button>
                                <button type="button" class="btn default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>

                <!-- BEGIN PAGE CONTENT-->
                <div class="row">
                    <div class="col-md-12">

                        <div class="col-md-8">
                            <div class="tabbable tabbable-custom tabbable-noborder">

                                <ul class="nav nav-tabs">
                                    <li class="active">
                                        <a href="#tab_1" data-toggle="tab">Product </a>
                                    </li>
                                    <li>
                                        <a href="#tab_2" data-toggle="tab">Invoice </a>
                                    </li>

                                </ul>

                                <div class="tab-content">

                                    <div class="tab-pane active" id="tab_1">
                                        <!-- BEGIN FILTER -->
                                        <div class="row">
                                            <div class="col-md-12">

                                                <table border="0">

                                                    <%--<div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="lblPlan1s" Text="Category" class="col-md-4 control-label"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged" CssClass="form-control select2me input-medium"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorPlan" runat="server" ErrorMessage="Category Required." ControlToValidate="drpCategory" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label1" Text="Sub Category" class="col-md-4 control-label"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpsubCategory" runat="server" CssClass="form-control select2me input-medium"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Sub Category Required." ControlToValidate="drpsubCategory" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>--%>


                                                    <div class="scrollmenu">
                                                        <asp:LinkButton ID="btnCategoryall" runat="server" CssClass="btn red-haze" Text="All" OnClick="btnCategoryall_Click"></asp:LinkButton>
                                                        <asp:ListView ID="ListCategory" runat="server" OnItemCommand="ListCategory_ItemCommand">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnCategory" runat="server" CssClass="btn red-haze" CommandName="DisplayCat" CommandArgument='<%# Eval("CATID") %>' Text='<%# Eval("CAT_NAME1") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>

                                                </table>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">


                                                <div class="col-md-6">
                                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnBarcode">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label22" runat="server" class="col-md-4 control-label" Text="Bar Code"></asp:Label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtBarCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator13" runat="server" ErrorMessage="BarCode Required." ControlToValidate="txtBarCode" ValidationGroup="barcode"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:Button ID="btnBarcode" runat="server" OnClick="btnBarcode_Click" ValidationGroup="barcode" CssClass="btn yellow-gold dz-square" Text="SEARCH" />
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>

                                            </div>
                                        </div>


                                        <div style="height: 485px; overflow: scroll">

                                            <div class="margin-top-10">
                                                <div class="row mix-grid">
                                                    <asp:ListView ID="Listview2" runat="server" OnItemCommand="Listview2_ItemCommand" OnItemDataBound="Listview2_ItemDataBound">
                                                        <ItemTemplate>

                                                            <%-- <asp:LinkButton ID="LinkButton1" Font-Underline="false"  CommandName="btnaddKart" CommandArgument='<%# Eval("MYPRODID") %>' runat="server">--%>

                                                            <asp:LinkButton ID="LnkProdAdd" Font-Underline="false" runat="server" OnClick="LnkProdAdd_Click">
                                                                <div class="col-md-2 col-sm-4 mix category_1">
                                                                    <div class="mix-inner" style="height: 240px;">
                                                                        <asp:Image ID="lnkimg" class="img-responsive" Height="138" Width="138" ImageUrl='<%# getimage(Convert.ToInt32(Eval("MYPRODID")))%>' runat="server" />
                                                                        <%--<img class="img-responsive" src="../../assets/admin/pages/media/works/img1.jpg" alt="">--%>
                                                                        <asp:Label ID="lblprodname" runat="server" Font-Size="12px" Text='<%# getprodnamesub(Convert .ToInt32 ( Eval("MYPRODID"))) %>'></asp:Label>
                                                                        <asp:Label ID="lblProdid" Visible="false" runat="server" Text='<%# Eval("MYPRODID") %>'></asp:Label>
                                                                        <asp:Label ID="lblPROUOM" Visible="false" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>

                                                                    </div>
                                                                </div>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="LnkProdAdd1" Font-Underline="false" runat="server">
                                                            </asp:LinkButton>
                                                            <asp:Panel ID="PanelProd" runat="server" CssClass="modalPopup" Style="padding: 1px; background-color: #fff; border: 2px solid pink; display: none; overflow: auto;">

                                                                <div class="row" style="position: fixed; left: 10%; top: 4%; width: 50%">
                                                                    <div class="col-md-12">
                                                                        <div class="portlet box purple">
                                                                            <div class="portlet-title">
                                                                                <div class="caption">
                                                                                    <i class="fa fa-globe"></i>
                                                                                    Product Detail   (<asp:Label ID="Label18" runat="server" Font-Size="12px" Text='<%# getprodnamesub(Convert .ToInt32 ( Eval("MYPRODID"))) %>'></asp:Label>)
                                                                                </div>
                                                                            </div>
                                                                            <div class="portlet-body">
                                                                                <div class="tabbable">
                                                                                    <div class="tab-content no-space">
                                                                                        <div "modal-Body">
                                                                                        <div class="form-body">
                                                                                           
                                                                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    
                                                                                                    <div class="col-md-12"> 
                                                                                                        <div class="row">                                                                                                       
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-12" style="color: black; font-weight: normal;padding-bottom: 5px;">
                                                                                                                    <label runat="server" id="Label78" class="col-md-2 control-label">
                                                                                                                        <asp:Label ID="Label79" runat="server" Text="UOM" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                        <span class="required">* </span>
                                                                                                                    </label>
                                                                                                                    <div class="col-md-10">
                                                                                                                        <asp:Label ID="lbluprod" Visible="false" runat="server" Text='<%# Eval("MYPRODID") %>'></asp:Label>
                                                                                                                        <%--<asp:DropDownList ID="ddlUOM" OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorUOM" runat="server" InitialValue="0" ErrorMessage="Select Uom" ControlToValidate="ddlUOM" ValidationGroup="AddPRo"></asp:RequiredFieldValidator>--%>

                                                                                                                        <asp:ListView ID="ListUOM" runat="server" OnItemCommand="ListUOM_ItemCommand">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblUOM" Visible="false" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                                                                                                                <asp:LinkButton ID="btnUOM" CommandName="btnUOM" CommandArgument='<%# Eval("UOM") %>' runat="server" CssClass="btn red-haze">
                                                                                                                                    <asp:Label ID="UOMName" runat="server" Text='<%# getUom(Convert.ToInt32(Eval("UOM"))) %>'></asp:Label>
                                                                                                                                </asp:LinkButton>
                                                                                                                                <%--OnClick="btnUOM_Click"--%>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:ListView>
                                                                                                                        <asp:Label ID="lblUOMFinal" Visible="false" runat="server" Text="0"></asp:Label>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="row">
                                                                                                            </div>
                                                                                                    </div>
                                                                                                     
                                                                                                    <div class="col-md-12" style="padding-bottom: 5px;">
                                                                                                       <div class="row">
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <div class="col-md-12" style="color: black; font-weight: normal;padding-left: 0px;">
                                                                                                                        <label runat="server" id="Label13" class="col-md-4 control-label  ">
                                                                                                                            <asp:Label ID="Label14" runat="server" Text="Price" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                            <span class="required">* </span>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-8">
                                                                                                                            <asp:TextBox ID="txtprice" Style="padding-left: 1px; padding-right: 1px;" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Qty" ControlToValidate="txtprice" ValidationGroup="AddPRo"></asp:RequiredFieldValidator>
                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtprice" ValidChars="0123456789." FilterType="Custom, numbers" runat="server" />
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>

                                                                                                                <div class="col-md-6">
                                                                                                                    <div class="col-md-12" style="color: black; font-weight: normal">
                                                                                                                        <label runat="server" id="Label11" class="col-md-4 control-label  ">
                                                                                                                            <asp:Label ID="Label12" runat="server" Text="Qty" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                            <span class="required">* </span>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-8">
                                                                                                                            <asp:TextBox ID="txtproQty" Style="padding-left: 1px; padding-right: 1px;" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Qty" ControlToValidate="txtproQty" ValidationGroup="AddPRo"></asp:RequiredFieldValidator>
                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderqty" TargetControlID="txtproQty" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                        
                                                                                                                                                                                                       

                                                                                                </ContentTemplate>

                                                                                                <Triggers>
                                                                                                    <%--<asp:AsyncPostBackTrigger ControlID="ddlUOM" EventName="" />--%>

                                                                                                    <asp:AsyncPostBackTrigger ControlID="txtprice" EventName="" />
                                                                                                    <asp:AsyncPostBackTrigger ControlID="txtproQty" EventName="" />
                                                                                                </Triggers>

                                                                                            </asp:UpdatePanel>
                                                                                           
                                                                                            </div>
                                                                                            
                                                                                        </div>
                                                                                            <div class="modal-footer">
                                                                                                <asp:LinkButton ID="LnkAddPRoduct" class="btn green-haze modalBackgroundbtn-circle" runat="server" CommandName="Addproduct">
                                                                                                    Save
                                                                                                </asp:LinkButton>
                                                                                                <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                <asp:Button ID="btncanpro" runat="server" class="btn btn-default" Text="Cancel" />
                                                                                            </div>

                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </asp:Panel>
                                                            <cc1:ModalPopupExtender ID="ModalPopupExtender10" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="btncanpro" Enabled="True" PopupControlID="PanelProd" TargetControlID="LnkProdAdd1"></cc1:ModalPopupExtender>


                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <!-- END FILTER -->
                                    </div>


                                </div>
                            </div>
                        </div>







                        <div class="col-md-4" style="overflow: scroll; height: 650px; padding: 0px">
                            <div class="tabbable tabbable-custom tabbable-noborder" >

                                <div id="ontop" class="tab-content">
                                    <div class="tab-pane active">
                                        <!-- BEGIN FILTER -->
                                        <div class="portlet box blue-madison">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <i class="fa fa-globe"></i>Invoice
                                                </div>

                                                <div class="tools">
                                                    <a href="javascript:;" class="reload" data-original-title="" title=""></a>
                                                    <a href="javascript:;" class="remove" data-original-title="" title=""></a>
                                                </div>

                                                <div class="actions btn-set">
                                                    <asp:Button ID="btnnewAdd" class="btn bg-yellow-gold" runat="server" Text="Add New" OnClick="btnnewAdd_Click" />
                                                </div>

                                            </div>
                                            <div class="portlet-body">
                                                <div class="row">
                                                    <div class="col-md-8">
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <div class="form-group">
                                                                <asp:Label ID="Label10" runat="server" class="col-md-4 control-label" Text="Customer Name"></asp:Label>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtCustomerName" ServiceMethod="GetCounrty" CompletionInterval="500" EnableCaching="FALSE" CompletionSetCount="20"
                                                                        MinimumPrefixLength="1" OnClientItemSelected="ace_itemCoutry" DelimiterCharacters=";, :" FirstRowSelected="false"
                                                                        runat="server" />
                                                                    <asp:RequiredFieldValidator CssClass="Validation" ForeColor="Red" ID="RequiredFieldValidator11" runat="server" ErrorMessage="Customer Required." ControlToValidate="txtCustomerName" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <%-- <asp:Button ID="btncustomer" runat="server" CssClass="btn yellow-gold dz-square" Text="SEARCH" />--%>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label1" runat="server" class="col-md-6 control-label" Text="Invoice No"></asp:Label>
                                                        <asp:Label ID="InvoiceNO" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row">

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label2" runat="server" class="col-md-4 control-label" Text="Invoice Type :"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpInvoiceType" runat="server" CssClass="form-control select2me input-small"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ForeColor="Red" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Plan Required." ControlToValidate="drpInvoiceType" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label4" runat="server" class="col-md-3 control-label" Text="Source:"></asp:Label>
                                                            <div class="col-md-9">
                                                                <asp:DropDownList ID="drpInvoiceScurce" runat="server" CssClass="form-control select2me input-small"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ForeColor="Red" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Plan Required." ControlToValidate="drpInvoiceScurce" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="table-scrollable">
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <table class="table table-striped table-bordered table-hover dataTable no-footer" id="sample_1" role="grid" aria-describedby="sample_1_info">
                                                                <thead>
                                                                    <tr role="row">
                                                                        <th></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblCID1" Text="ID#"></asp:Label></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblContractID" Text="ITEM NAME"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label6" Text="QTY"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label7" Text="UOM"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label8" Text="Unit Price"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label30" Text="Price"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label26" Text="Disc"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label9" Text="AMOUNT"></asp:Label>
                                                                        </th>
                                                                        <%-- <th>
                                                                    <asp:Label runat="server" ID="Label11" Text="Action"></asp:Label>
                                                                </th>--%>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>

                                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" OnDataBinding="Listview1_DataBinding">
                                                                        <ItemTemplate>
                                                                            <tr role="row" class="odd">
                                                                                <td>
                                                                                    <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") + "," + Eval("MyProdID") %>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton>
                                                                                </td>

                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkButton2" CommandName="btnDisp" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") + "," + Eval("MyProdID") %>' runat="server">
                                                                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                                        <asp:Label ID="lblMYID" runat="server" Visible="false" Text='<%#Eval("MYID") %>'></asp:Label>
                                                                                        <asp:Label ID="lblMYTRANSID" runat="server" Visible="false" Text='<%#Eval("MYTRANSID") %>'></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkButton3" CommandName="btnDisp" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") + "," + Eval("MyProdID") %>' runat="server">
                                                                                        <asp:Label ID="lblPRodName" runat="server" Text='<%# getprodname(Convert .ToInt32 ( Eval("MyProdID"))) %>'></asp:Label>
                                                                                        <asp:Label ID="lblProdID" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkButton4" CommandName="btnDisp" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") + "," + Eval("MyProdID") %>' runat="server">
                                                                                        <asp:Label ID="lblQty" runat="server" Text='<%#Eval("QUANTITY") %>'></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkButton5" CommandName="btnDisp" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") + "," + Eval("MyProdID") %>' runat="server">
                                                                                        <asp:Label ID="lblUOMName" runat="server" Text='<%# getUom(Convert.ToInt32(Eval("UOM"))) %>'></asp:Label>
                                                                                        <asp:Label ID="lblUOM" Visible="false" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkButton6" CommandName="btnDisp" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") + "," + Eval("MyProdID") %>' runat="server">
                                                                                        <asp:Label ID="lblUNITPRICE" runat="server" Text='<%# Eval("price") %>'></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkButton9" CommandName="btnDisp" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") + "," + Eval("MyProdID") %>' runat="server">
                                                                                        <asp:Label ID="Label27" runat="server" Text='<%#Eval("TotalPrice") %>'></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkButton8" CommandName="btnDisp" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") + "," + Eval("MyProdID") %>' runat="server">
                                                                                        <asp:Label ID="lblDISPER" runat="server" Text='<%#Eval("Discount") %>'></asp:Label>
                                                                                        <asp:Label ID="lblDISAMT" Visible="false" runat="server" Text='<%# Eval("discAmount") %>'></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkButton7" CommandName="btnDisp" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") + "," + Eval("MyProdID") %>' runat="server">
                                                                                        <asp:Label ID="lblTotalAMOUNT" runat="server" Text='<%#Eval("AMOUNT") %>'></asp:Label>
                                                                                    </asp:LinkButton>
                                                                                </td>

                                                                                <%--<td>
                                                                            <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("MyProdID") %>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom" OnClientClick="showProgress()"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                        </td>--%>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>

                                                                </tbody>
                                                                <tfoot>
                                                                    <tr role="row">
                                                                        <th></th>
                                                                        <th></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label29" Text="Total"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblfQty"></asp:Label>
                                                                        </th>
                                                                        <th></th>
                                                                        <th></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblFPrice"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblFdisc"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblFamount"></asp:Label>
                                                                        </th>
                                                                        <%--<th></th>--%>
                                                                    </tr>
                                                                </tfoot>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row" style="padding-bottom: 5px;">

                                                            <div class="form-group">
                                                                <div class="col-md-4">
                                                                    <asp:Label runat="server" ID="Label17" Text="QTY" class="col-md-4 control-label"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <%--AutoPostBack="true" OnTextChanged="txtQty_TextChanged" --%>
                                                                        <%--<input name="txtQty" id="txtQty" type="number" onfocusout="TotalPriceui()" style="padding-left: 1px; padding-right: 1px;" class="form-control" runat="server" />--%>
                                                                        <asp:TextBox ID="txtQty" onfocusout="TotalPriceui()" Style="padding-left: 1px; padding-right: 1px;" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator12" runat="server" ErrorMessage="Enter Qty" ControlToValidate="txtQty" ValidationGroup="tempAdd"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" TargetControlID="txtQty" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <asp:Label runat="server" ID="Label3" Text="Disc %" class="col-md-4 control-label"></asp:Label>
                                                                    <%--Style="padding-left: 0px;"--%>
                                                                    <div class="col-md-8">
                                                                        <%--AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged"--%>
                                                                        <%--<input name="txtDiscount" id="txtDiscount" type="text" onfocusout="TotalPriceui()" style="padding-left: 1px; padding-right: 1px;" class="form-control" runat="server" />--%>
                                                                        <asp:TextBox ID="txtDiscount" onfocusout="TotalPriceui();" Style="padding-left: 1px; padding-right: 1px;" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <%--Style="margin-left: 15px;"--%>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Discount" ControlToValidate="txtDiscount" ValidationGroup="tempAdd"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtDiscount" ValidChars="0123456789.%" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <asp:Label runat="server" ID="Label5" Text="Disc Amt" class="col-md-4 control-label"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <%--AutoPostBack="true" OnTextChanged="txtAmt_TextChanged"--%>

                                                                        <%--<input name="txtAmt" id="txtAmt" type="text" onfocusout="TotalPriceui()" style="padding-left: 1px; padding-right: 1px;" class="form-control" runat="server" />--%>
                                                                        <asp:TextBox ID="txtAmt" onfocusout="TotalPriceui()" Enabled="false" Style="padding-left: 1px; padding-right: 1px;" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter Amt" ControlToValidate="txtAmt" ValidationGroup="tempAdd"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtAmt" ValidChars="0123456789.-" FilterType="Custom, numbers" runat="server" />
                                                                        <%--<asp:Label ID="lblPrice" runat="server" Visible="false" Text=""></asp:Label>--%>
                                                                        <input id="lblPrice" type="hidden" name="lblPrice" runat="server" />

                                                                    </div>
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <asp:Label runat="server" ID="Label16" Text="Amount" class="col-md-4 control-label"></asp:Label>
                                                                    <%-- Style="padding-left: 0px;"--%>
                                                                    <div class="col-md-8">

                                                                        <%--<input name="txtAmount" id="txtAmount" type="text" style="padding-left: 1px; padding-right: 1px;" class="form-control" runat="server" readonly />--%>
                                                                        <asp:TextBox ID="txtAmount" CssClass="form-control" Enabled="false" Style="padding-left: 1px; padding-right: 1px;" runat="server"></asp:TextBox>
                                                                        <%--Style="margin-left: 10px;"--%>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter Amount" ControlToValidate="txtAmount" ValidationGroup="tempAdd"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtAmount" ValidChars="0123456789.-" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:Button ID="Button9" runat="server" ValidationGroup="tempAdd" CssClass="btn green dz-square" OnClick="Button9_Click" Text="Save" />
                                                            </div>


                                                        </div>

                                                        <hr style="margin-top: 0px; margin-bottom: 5px;" />
                                                        <div class="row" style="padding-bottom: 5px;">

                                                            <div class="form-group">



                                                                <div class="col-md-5">
                                                                    <asp:Label runat="server" ID="Label24" Text="Invoice Amt" class="col-md-6 control-label"></asp:Label>
                                                                    <%--Style="padding-left: 0px;"--%>
                                                                    <div class="col-md-6">
                                                                        <asp:TextBox ID="txtInvoiceAmt" CssClass="form-control" Style="padding-left: 1px; padding-right: 1px;" runat="server"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator15" runat="server" ErrorMessage="Enter Invoice Amt" ControlToValidate="txtInvoiceAmt" ValidationGroup="txtqty"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" TargetControlID="txtInvoiceAmt" ValidChars="0123456789.-" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-3">
                                                                    <asp:Label runat="server" ID="Label21" Text="Extra" class="col-md-4 control-label"></asp:Label>
                                                                    <%--Style="padding-left: 0px;"--%>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtExtra" CssClass="form-control" Style="padding-left: 1px; padding-right: 1px;" runat="server"></asp:TextBox>
                                                                        <%--Style="margin-left: 10px;"--%>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator10" runat="server" ErrorMessage="Enter Extra" ControlToValidate="txtExtra" ValidationGroup="txtqty"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtExtra" ValidChars="0123456789.-" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-4">
                                                                    <asp:Label runat="server" ID="Label25" Text="Paid Amt" class="col-md-6 control-label"></asp:Label>
                                                                    <%--Style="padding-left: 0px;"--%>
                                                                    <div class="col-md-6">
                                                                        <asp:TextBox ID="txtPaidAmount" CssClass="form-control" Style="padding-left: 1px; padding-right: 1px;" runat="server"></asp:TextBox>
                                                                        <%--Style="margin-left: 10px;"--%>
                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator16" runat="server" ErrorMessage="Enter Paid Amount" ControlToValidate="txtPaidAmount" ValidationGroup="txtqty"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" TargetControlID="txtPaidAmount" ValidChars="0123456789.-" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                                <div class="row" style="padding-bottom: 5px;">
                                                    <div class="form-group">
                                                        <div class="col-md-4">
                                                            <asp:Button ID="btnPayPrint" runat="server" CssClass="btn yellow-gold dz-square" Text="PAY PRINT" OnClick="btnPayPrint_Click" ValidationGroup="submit" />
                                                        </div>

                                                        <div class="col-md-2">
                                                            <asp:Button ID="btnPay" runat="server" CssClass="btn yellow-gold dz-square" Text="PAY" OnClick="btnPay_Click" ValidationGroup="submit" />
                                                        </div>

                                                        <div class="col-md-6">
                                                            <asp:Label runat="server" ID="Label28" Text="Return Charge" class="col-md-7 control-label"></asp:Label>
                                                            <%--Style="padding-left: 0px;"--%>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="txtReturnCharge" CssClass="form-control" Style="padding-left: 1px; padding-right: 1px;" runat="server"></asp:TextBox>
                                                                <%--Style="margin-left: 10px;"--%>
                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator19" runat="server" ErrorMessage="Enter Return Charge" ControlToValidate="txtReturnCharge" ValidationGroup="txtqty"></asp:RequiredFieldValidator>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" TargetControlID="txtReturnCharge" ValidChars="0123456789.-" FilterType="Custom, numbers" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <hr style="margin-top: 0px; margin-bottom: 5px;" />

                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <div class="col-md-1">
                                                                <asp:LinkButton ID="btnaddamunt" runat="server" OnClick="btnaddamunt_Click">
                                                                    <asp:Image ID="Image3" runat="server" ImageUrl="../Sales/images/plus.png" />
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class="col-md-11">
                                                                <table class="table table-bordered" id="datatable11">
                                                                    <tbody>
                                                                        <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <tr class="gradeA" style="top: 0px;">
                                                                                    <td>
                                                                                        <asp:DropDownList ID="drppaymentMeted" runat="server" Style="width: 124px;" CssClass="form-control">
                                                                                        </asp:DropDownList>
                                                                                        <asp:Label ID="lblOHType" Visible="false" Text='<%#Eval("PaymentTermsId") %>' runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <b><font size="1px"> <asp:Label runat="server" slyle="font-size: 9px;" ID="Label191" Text="Card/Reference# , Bank Approval #" ></asp:Label></font></b>
                                                                                        <asp:TextBox ID="txtrefresh" runat="server" Text='<%#Eval("ReferenceNo")+","+Eval("ApprovalID") %>' AutoCompleteType="Disabled" placeholder="Card/Reference# , Bank Approval #" CssClass="form-control tags"></asp:TextBox>
                                                                                        <%--OnTextChanged="txtrefresh1_TextChanged"--%>

                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtammunt" runat="server" Text='<%#Eval("Amount") %>' AutoCompleteType="Disabled" placeholder="Amount" Style="width: 104px;" CssClass="form-control"></asp:TextBox>
                                                                                    </td>
                                                                                    <%--OnTextChanged="txtammunt1_TextChanged"--%>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                    </div>

                                    <!-- END FILTER -->
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="col-md-12">
                    <div class="col-md-12">
                        <div class="tabbable tabbable-custom tabbable-noborder">

                            <div class="tab-content">

                                <div class="tab-pane active" id="tab_1">
                                    <!-- BEGIN FILTER -->
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Button ID="Button1" runat="server" CssClass="btn yellow-gold dz-square" Text="CONDIMENT" />
                                            <asp:Button ID="Button2" runat="server" CssClass="btn yellow-gold dz-square" Text="MULTIUOM" />
                                            <asp:Button ID="Button3" runat="server" CssClass="btn yellow-gold dz-square" Text="ORDER NOTE" />
                                            <asp:Button ID="Button4" runat="server" CssClass="btn yellow-gold dz-square" Text="DISCOUNT" />
                                            <asp:Button ID="Button6" runat="server" CssClass="btn yellow-gold dz-square" Text="CANCEL INVOICE" />
                                            <asp:Button ID="Button5" runat="server" CssClass="btn yellow-gold dz-square" Text="GIFT VOUCHER" />


                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>


            </div>
            <!-- END PAGE CONTENT-->
        </div>

        <!-- END CONTENT -->
        
        <!-- END CONTAINER -->
        <!-- BEGIN FOOTER -->
    </form>
    <!-- END FOOTER -->
    <!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
    <!-- BEGIN CORE PLUGINS -->
    <!--[if lt IE 9]>
<script src="../../assets/global/plugins/respond.min.js"></script>
<script src="../../assets/global/plugins/excanvas.min.js"></script> 
<![endif]-->
    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
    <!-- IMPORTANT! Load jquery-ui.min.js before bootstrap.min.js to fix bootstrap tooltip conflict with jquery ui tooltip -->
    <script src="../assets/global/plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script type="text/javascript" src="../assets/global/plugins/jquery-mixitup/jquery.mixitup.min.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/fancybox/source/jquery.fancybox.pack.js"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <script src="../assets/global/scripts/metronic.js" type="text/javascript"></script>
    <script src="../assets/admin/layout4/scripts/layout.js" type="text/javascript"></script>
    <script src="../assets/admin/layout4/scripts/demo.js" type="text/javascript"></script>
    <script src="../assets/admin/pages/scripts/portfolio.js"></script>
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
<!-- END BODY -->
</html>
