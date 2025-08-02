<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.Master" AutoEventWireup="true" CodeBehind="SupplierProductMasterTest.aspx.cs" Inherits="Web.SupplierProductMasterTest" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%--<%@ Register Assembly="obout_ColorPicker" Namespace="OboutInc.ColorPicker" TagPrefix="obout" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }
    </style>
    <script type="text/javascript">
        function targetMeBlank1() {
            document.getElementById["ContentPlaceHolder1_CatEdit"].target = "_blank";
        }
    </script>
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="ECOMM/PrductJs/Productjs.js"></script>
    <div class="page-content" id="b" runat="server">
        <%-- <div class="page-head">
            <ol class="breadcrumb">
                <li><a href="#">
                    <asp:Label ID="lblDisPro" runat="server" Text="Product"></asp:Label></a><i class="fa fa-circle"></i></li>
                <li><a href="#">
                    <asp:Label ID="lblDisProMst" runat="server" Text="Product Master"></asp:Label></a></li>
                <%-- <div class="pull-right" style="margin-top: -9px;">
                    <img id="GridviewDisplay" src="ECOMM/images/grid-view.png" runat="server" alt="GridviewDisplay" class="lblDisPro2" onclick="x('grd');" />
                    <img id="ListviewDisplay" src="ECOMM/images/list-view.png" runat="server" alt="ListviewDisplay" class="lblDisPro2" onclick="showhidepanel('lst');" />
                    <img id="FormDisplay" src="ECOMM/images/form-view.png" runat="server" alt="FormDisplay" class="lblDisPro2" onclick="showhidepanel('frm');" />
                </div>
            </ol>

        </div>--%>
        <div class="col-md-12">
            <div class="sidebar col-md-3 col-sm-3">
                <ul class="list-group margin-bottom-25 sidebar-menu">
                    <li class="list-group-item clearfix"><a href="SupplerHome.aspx"><i class="fa fa-angle-right"></i>Dashboard</a></li>
                    <li class="list-group-item clearfix"><a href="SaleshProduct.aspx"><i class="fa fa-angle-right"></i>Category Management</a></li>
                    <li class="list-group-item clearfix"><a href="SupplierProductMasterTest.aspx"><i class="fa fa-angle-right"></i>Add Product</a></li>
                    <li class="list-group-item clearfix"><a href="javascript:;"><i class="fa fa-angle-right"></i>Payment Status</a></li>
                    <li class="list-group-item clearfix"><a href="Profile.aspx"><i class="fa fa-angle-right"></i>Login/Register</a></li>
                    <li class="list-group-item clearfix"><a href="ChangPassword.aspx"><i class="fa fa-angle-right"></i>Restore Password</a></li>
                    <li class="list-group-item clearfix"><a href="javascript:;"><i class="fa fa-angle-right"></i>Address book</a></li>
                    <li class="list-group-item clearfix"><a href="javascript:;"><i class="fa fa-angle-right"></i>Returns</a></li>
                    <li class="list-group-item clearfix"><a href="javascript:;"><i class="fa fa-angle-right"></i>Newsletter</a></li>
                </ul>
            </div>
            <div class="col-md-9">
                <div class="tabbable-custom tabbable-noborder">
                    <div class="row">
                        <div class="col-md-12">

                            <%-- <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                            <asp:Panel ID="FrmView" runat="server" Style="display: block;">
                                <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-horizontal form-row-seperated">
                                            <div class="portlet box yellow-crusta">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="fa fa-gift"></i>
                                                        <asp:Label runat="server" Visible="false" ID="lblHeader"></asp:Label>
                                                        <asp:TextBox Style="color: #333333" ID="txtHeader" runat="server" Visible="false"></asp:TextBox>
                                                        Product Details  &nbsp;<asp:Label ID="lblpronamemain" runat="server"></asp:Label>&nbsp;<asp:Label ID="lblProductmode" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="tools">
                                                        <a id="shlinkProductDetails" runat="server" href="javascript:;" class="collapse"></a>
                                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                        <a href="javascript:;" class="reload"></a>
                                                        <a href="javascript:;" class="remove"></a>
                                                    </div>
                                                    <div class="actions btn-set">
                                                        <asp:LinkButton ID="linkButtonWebProd" CssClass="btn btn-primary"  OnClick="linkButtonWebProd_Click" runat="server" Style="color: #fff; background-color: #36d7b7;"> Web Product</asp:LinkButton>
                                                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Style="padding-top: 7px; padding-bottom: 7px" Text="Add New" OnClick="Button1_Click" />
                                                        <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary" Style="padding-top: 7px; padding-bottom: 7px" ValidationGroup="submit" Text="Save" OnClick="BtnMstProSave_Click" />
                                                        <asp:Button ID="Button4" runat="server" CssClass="btn btn-primary" Style="padding-top: 7px; background-color: red; color: #fff; padding-bottom: 7px" meta:resourcekey="btnCloseResource1" Text="Discard" OnClick="Button4_Click" OnClientClick="return ConfirmMsg2();" />
                                                        <asp:Button ID="Button5" runat="server" CssClass="btn btn-primary" Style="padding-top: 7px; background-color: red; color: #fff; padding-bottom: 7px" meta:resourcekey="btnCloseResource1" Text="Cancel" OnClick="Button5_Click" OnClientClick="return ConfirmMsg3();" />
                                                        <asp:Button ID="btnEditLable" runat="server" class="btn green-haze" Style="padding-top: 7px; padding-bottom: 7px" OnClick="btnEditLable_Click" Text="Update Label" />
                                                        &nbsp;
                                        <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="/assets/global/img/flags/us.png" /></asp:LinkButton>
                                                        <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="/assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                        <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="/assets/global/img/flags/fr.png" /></asp:LinkButton>

                                                    </div>
                                                </div>

                                                <div id="shProductDetails" runat="server" class="portlet-body" style="padding-left: 25px;">
                                                    <div class="tabbable">
                                                        <div class="tab-content no-space">
                                                            <div class="tab-pane active" id="tab_general">
                                                                <div class="form-body">
                                                                    <asp:Panel ID="panelBarcod" runat="server" Visible="false">
                                                                        <div class="alert alert-danger alert-dismissable">
                                                                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                            <asp:Label ID="lblBarcodedublicat" runat="server"></asp:Label>
                                                                        </div>
                                                                    </asp:Panel>

                                                                    <%-- <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                            <ContentTemplate>--%>

                                                                    <div class="form-group">
                                                                        <div class="col-md-6">
                                                                            <asp:Label runat="server" ID="lblProductId1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductId1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="lblmstproid" runat="server" meta:resourcekey="lblmstproidResource1"></asp:Label>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblProductId2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductId2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label runat="server" ID="lblProductNameMainLanguage1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductNameMainLanguage1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="pm_txtProductName" runat="server" AutoCompleteType="Disabled" MaxLength="150" CssClass="form-control" placeholder="Product Name" data-popover="popover" data-placement="right" ToolTip="Type product name" data-trigger="hover" meta:resourcekey="pm_txtProductNameResource1"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorSHORTNAME" runat="server" ControlToValidate="pm_txtProductName" ErrorMessage="Product Name Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblProductNameMainLanguage2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductNameMainLanguage2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-6">
                                                                            <asp:Label runat="server" ID="lblBarcodeNo1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBarcodeNo1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtProBar" runat="server" AutoCompleteType="Disabled" MaxLength="50" CssClass="form-control" placeholder="Barcode Number" data-popover="popover" data-placement="right" ToolTip="Type barcode number" data-trigger="hover"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProBar" ErrorMessage="Barcode Number Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblBarcodeNo2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBarcodeNo2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label runat="server" ID="lblProductType1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductType1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="ddlProdType" Style="height: 30px; width: 275px;" runat="server" class="form-control select2me" ToolTip="Product Type" data-trigger="hover" meta:resourcekey="ddlProdTypeResource1">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblProductType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-6">
                                                                            <asp:Label runat="server" ID="lblAlternateCode11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAlternateCode11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtACode1" runat="server" AutoCompleteType="Disabled" MaxLength="50" CssClass="form-control" ToolTip="Alternate Code1" data-trigger="hover" placeholder="Alternate Code1" data-popover="popover" data-placement="right" meta:resourcekey="txtACode1Resource1"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtACode1" ErrorMessage="Alternate Code1 Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblAlternateCode12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAlternateCode12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label runat="server" ID="lblAlternateCode21s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAlternateCode21s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtACode2" runat="server" AutoCompleteType="Disabled" MaxLength="50" CssClass="form-control" placeholder="Alternate Code2" data-popover="popover" data-placement="right" ToolTip="Type alternate code" data-trigger="hover" meta:resourcekey="txtACode2Resource1"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtACode2" ErrorMessage="Alternate Code2 Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblAlternateCode22h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAlternateCode22h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <div class="col-md-6">
                                                                            <asp:Label runat="server" ID="lblProductNameLanguage21s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductNameLanguage21s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtItemRelSubProName2" runat="server" placeholder="Product Name Language 2" MaxLength="150" ToolTip="Product Name (O2)" data-trigger="hover" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtItemRelSubProName2" ErrorMessage="Product Name (O2) Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblProductNameLanguage22h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductNameLanguage22h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label runat="server" ID="lblProductNameLanguage31s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductNameLanguage31s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtItemRelSubProName3" runat="server" AutoCompleteType="Disabled" MaxLength="150" placeholder="Product Name Language 3" ToolTip="Product Name (O3)" data-trigger="hover" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtItemRelSubProName3" ErrorMessage="Product Name (O3) Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblProductNameLanguage32h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductNameLanguage32h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <div class="col-md-6">
                                                                            <asp:Label runat="server" ID="lblShortName1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtShortName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtShortName" runat="server" AutoCompleteType="Disabled" placeholder="Short Name" MaxLength="20" ToolTip="Short Name" data-trigger="hover" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtShortName" ErrorMessage="Short Name Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblShortName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtShortName2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Label runat="server" ID="lblDescriptiontoPrint1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescriptiontoPrint1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtDescToprint" Text="0" runat="server" placeholder="Description to Print" MaxLength="150" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpsnoResource1"></asp:TextBox>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblDescriptiontoPrint2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescriptiontoPrint2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>


                                                                </div>



                                                                <%-- </ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                                                                <div class="portlet-body form">
                                                                    <div class="portlet-body">
                                                                        <div class="form-wizard">
                                                                            <div class="tabbable">
                                                                                <ul class="nav nav-pills nav-justified steps" style="padding-top: 0px; padding-bottom: 0px; margin-bottom: 0px;">
                                                                                    <li class="active"><a style="color: #5b9bd1;" href="#pMaster" class="step" data-toggle="tab">
                                                                                        <span class="number" Style ="color :#333333">1 </span><span class="desc">
                                                                                            <asp:Label ID="lblBusinessCo" runat="server" Text="Product Master" Style ="color :#333333" meta:resourcekey="lblBusinessCoResource1"></asp:Label>
                                                                                        </span></a></li>
                                                                                    <%-- <li>
                                                                                        <a style="color: #5b9bd1;" href="#pDetails" class="step" data-toggle="tab"><span class="number">2 </span><span class="desc">
                                                                                            <asp:Label ID="lblBusinessDetai" runat="server" Text="Product Details" meta:resourcekey="lblBusinessDetaiResource1"></asp:Label></span></a></li>
                                                                                    <li>
                                                                                        <a style="color: #5b9bd1;" href="#pWebSoft" class="step" data-toggle="tab">
                                                                                            <span class="number">3 </span><span class="desc">
                                                                                                <asp:Label ID="lblWebExistance" runat="server" Text="Web Product" meta:resourcekey="lblWebExistanceResource1"></asp:Label></span></a>&nbsp;
                                                                                    </li>--%>
                                                                                    <li>
                                                                                        <a style="color: #5b9bd1;" href="#pPriceNote" class="step" data-toggle="tab">
                                                                                            <span class="number" Style ="color :#333333">2 </span><span class="desc">
                                                                                                <asp:Label ID="Label1" runat="server" Text="Display Product" Style ="color :#333333" meta:resourcekey="lblWebExistanceResource1"></asp:Label></span></a>&nbsp;
                                                                                    </li>
                                                                                    <li>
                                                                                        <asp:LinkButton ID="linktoprodMaster" Style="color: blue; padding: 0px; font-size: 40px" class="step" runat="server" OnClick="linktoprodMaster_Click">
                                                                                    <span class="number"><span class="glyphicon glyphicon-chevron-right" style ="font-size: 25px; padding-left: 0px; padding-right: 0px; right: 8px;"> </span></span> </asp:LinkButton>
                                                                                    </li>
                                                                                </ul>
                                                                                <div class="tab-content no-space">
                                                                                    <div id="pMaster" class="tab-pane active ">



                                                                                        <div class="tabbable">
                                                                                            <ul class="nav nav-tabs">
                                                                                                <li class="active" style ="padding: 10px 0px;">
                                                                                                    <%--  <asp:LinkButton ID="linkBusiness" runat="server" Style="color: #fff; background-color: #26a69a" onClick= "showhidepanelMoreItem('grd');" >BUSINESS PRODUCT</asp:LinkButton>--%>
                                                                                                    <a id="GridviewDisplay" style="color: #fff; background-color: #26a69a" runat="server" alt="GridviewDisplay" class="lblDisPro2" onclick="showhidepanelMoreItem('grd');">BUSINESS PRODUCT</a>
                                                                                                </li>
                                                                                                <li style ="padding: 10px 0px;">
                                                                                                    <%--<asp:LinkButton ID="linkMoreItem" onClick="showhidepanelMoreItem('lst');" runat="server" Style="color: #fff; background-color: #3598dc">MORE ITEM CHARACTERISTICS</asp:LinkButton>--%>
                                                                                                    <a id="ListviewDisplay" runat="server" alt="ListviewDisplay" class="lblDisPro2" onclick="showhidepanelMoreItem('lst');" style="color: #fff; background-color: #3598dc">MORE ITEM CHARACTERISTICS</a>
                                                                                                </li>
                                                                                                <li style ="padding: 10px 10px 10px 0px;">
                                                                                                    <a id="FormDisplay" runat="server" alt="FormDisplay" class="lblDisPro2" onclick="showhidepanelMoreItem('frm');" style="color: #fff; background-color: #8e5fa2">INTERNAL NOTES AND PRICES</a>
                                                                                                </li>

                                                                                                <%--<asp:LinkButton ID="linkInternal" onClick="showhidepanelMoreItem('frm');" runat="server" Style="color: #fff; background-color: #8e5fa2">INTERNAL NOTES AND PRICES</asp:LinkButton>--%>
                                                                                            </ul>



                                                                                            <div class="tab-content no-space">
                                                                                                <asp:HiddenField ID="bussproid" runat="server" />
                                                                                                <asp:HiddenField ID="ItemchHF" runat="server" />
                                                                                                <asp:HiddenField ID="WebProHF" runat="server" />
                                                                                                <asp:HiddenField ID="RelativeProHF" runat="server" />
                                                                                                <asp:HiddenField ID="SerSoftProHF" runat="server" />
                                                                                                <asp:HiddenField ID="OffreProHF" runat="server" />

                                                                                                <asp:Panel ID="pnelBissnesh" runat="server" Style="display: block">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <div id="ac3-1" class="tab-pane active ">
                                                                                                                <div class="portlet box green">
                                                                                                                    <div class="portlet-title">
                                                                                                                        <div class="caption">
                                                                                                                            <i class="fa fa-gift"></i>BUSINESS PRODUCT &nbsp;<asp:Label ID="lblBusinessMode" runat="server"></asp:Label>
                                                                                                                        </div>
                                                                                                                        <div class="actions btn-set">
                                                                                                                            &nbsp;<asp:LinkButton ID="linkbtnBusEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="/assets/global/img/flags/us.png" /></asp:LinkButton>
                                                                                                                            <asp:LinkButton ID="linkbtnBusArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="/assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                                                                                            <asp:LinkButton ID="linkbtnBusFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="/assets/global/img/flags/fr.png" /></asp:LinkButton>
                                                                                                                            <%-- <asp:Button ID="Button10" runat="server" CssClass="btn btn-primary" Style ="background-color :red;color :#fff;" meta:resourcekey="btnCloseResource1" Text="Discard" Height="34px" OnClientClick="return clearBus()" />--%>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                    <div class="portlet-body form">
                                                                                                                        <div class="form-horizontal">
                                                                                                                            <asp:Panel ID="pnlSuccessMsg123" runat="server" Visible="false">
                                                                                                                                <div class="alert alert-danger alert-dismissable">
                                                                                                                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                                                                                    <asp:Label ID="lblMsg123" runat="server"></asp:Label>
                                                                                                                                </div>
                                                                                                                            </asp:Panel>

                                                                                                                            <asp:Panel ID="pnlbussadd" runat="server" meta:resourcekey="pnlbussaddResource1">

                                                                                                                                <div class="form-body">
                                                                                                                                    <div class="form-group">
                                                                                                                                        <div class="col-md-6">
                                                                                                                                            <asp:Label runat="server" ID="lblBusinessProduct1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBusinessProduct1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            <div class="col-md-8">
                                                                                                                                                <asp:DropDownList ID="DDLBussPro" runat="server" Style="height: 30px; width: 275px;" Class="form-control form-control select2me">
                                                                                                                                                </asp:DropDownList>
                                                                                                                                            </div>
                                                                                                                                            <asp:Label runat="server" ID="lblBusinessProduct2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBusinessProduct2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        </div>
                                                                                                                                        <div class="col-md-6">
                                                                                                                                            <asp:Label runat="server" ID="lblActive1s" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            <div class="col-md-4" style="padding-top: 4px;">
                                                                                                                                                <div class="radio-list">
                                                                                                                                                    <asp:RadioButtonList ID="BussProact" runat="server" RepeatDirection="Horizontal">
                                                                                                                                                        <asp:ListItem Selected="True" Value="1" Text="Yes"></asp:ListItem>
                                                                                                                                                        <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                                                                                                                                    </asp:RadioButtonList>
                                                                                                                                                </div>
                                                                                                                                            </div>
                                                                                                                                            <asp:Label runat="server" ID="lblActive2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            <div class="col-md-4">
                                                                                                                                                <asp:Button ID="BtmBussAddRow" runat="server" CssClass="btn btn-primary" meta:resourcekey="btnSubmitSearchResource1" Text="Add Business" OnClick="BtmBussAddRow_Click" Height="34px" />
                                                                                                                                            </div>
                                                                                                                                            <div class="col-md-2">
                                                                                                                                                <asp:Image ID="Image4" runat="server" ImageUrl="ECOMM/images/plus.png" OnClick="javascript:showpanebussline();" Style="float: right; margin-top: -27px;" />
                                                                                                                                            </div>

                                                                                                                                        </div>



                                                                                                                                    </div>

                                                                                                                                    <asp:Panel ID="panebussline" runat="server" Style="display: none" meta:resourcekey="panebusslineResource1">
                                                                                                                                        <div class="row" style="margin-right: 0px;">
                                                                                                                                            <div class="form-group">
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="lblPlaceholderLine1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPlaceholderLine1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtbppl" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpplResource1"></asp:TextBox>
                                                                                                                                                    </div>

                                                                                                                                                    <asp:Label runat="server" ID="lblPlaceholderLine2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPlaceholderLine2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                </div>
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="lblPlaceholderAliRL1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPlaceholderAliRL1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtbprl" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbprlResource1"></asp:TextBox>
                                                                                                                                                    </div>
                                                                                                                                                    <asp:Label runat="server" ID="lblPlaceholderAliRL2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPlaceholderAliRL2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                </div>
                                                                                                                                            </div>


                                                                                                                                            <div class="form-group">
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="lblPlaceholderColumn1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPlaceholderColumn1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtpbcol" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtpbcolResource1"></asp:TextBox>
                                                                                                                                                    </div>
                                                                                                                                                    <asp:Label runat="server" ID="lblPlaceholderColumn2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPlaceholderColumn2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                </div>
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="lblLink2Direct1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLink2Direct1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtbpl2d" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpl2dResource1"></asp:TextBox>
                                                                                                                                                    </div>
                                                                                                                                                    <asp:Label runat="server" ID="lblLink2Direct2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLink2Direct2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                </div>
                                                                                                                                            </div>

                                                                                                                                            <div class="form-group">
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="lblSortNumber1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSortNumber1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtbpsno" Text="0" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpsnoResource1"></asp:TextBox>
                                                                                                                                                    </div>
                                                                                                                                                    <asp:Label runat="server" ID="lblSortNumber2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSortNumber2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                </div>
                                                                                                                                            </div>

                                                                                                                                        </div>

                                                                                                                                    </asp:Panel>
                                                                                                                                    <div class="row">
                                                                                                                                        <div class="col-md-12">
                                                                                                                                            <div class="form-horizontal form-row-seperated">
                                                                                                                                                <div class="portlet box green-turquoise">
                                                                                                                                                    <div class="portlet-title">
                                                                                                                                                        <div class="caption">
                                                                                                                                                            <i class="fa fa-gift"></i>BUSINESS PRODUCT LIST &nbsp;<asp:Label ID="lblBusinesslistmode" runat="server"></asp:Label>
                                                                                                                                                        </div>
                                                                                                                                                        <%--<div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>--%>
                                                                                                                                                    </div>
                                                                                                                                                    <div class="portlet-body" style="padding-left: 25px;">
                                                                                                                                                        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                                                                    <ContentTemplate>--%>

                                                                                                                                                        <table class="table table-striped table-bordered table-hover" id="sample_2">
                                                                                                                                                            <thead class="repHeader">
                                                                                                                                                                <tr>
                                                                                                                                                                    <th>
                                                                                                                                                                        <asp:Label ID="Label2" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                                                                                                    <th style="width: 150px;">
                                                                                                                                                                        <asp:Label ID="Label36" runat="server" Text="Allow Online Sale" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                                                                                                    <th>
                                                                                                                                                                        <asp:Label ID="Label6" runat="server" Text="Product in Business" meta:resourcekey="lblSNameResource2"></asp:Label></th>
                                                                                                                                                                    <th>
                                                                                                                                                                        <asp:Label ID="Label14" runat="server" Text="Business Type" meta:resourcekey="lblProNameResource1"></asp:Label></th>
                                                                                                                                                                    <th>
                                                                                                                                                                        <asp:Label ID="Label33" runat="server" Text="Delete" meta:resourcekey="lblDelResource1"></asp:Label></th>
                                                                                                                                                                </tr>
                                                                                                                                                            </thead>
                                                                                                                                                            <tbody>
                                                                                                                                                                <asp:Repeater ID="GridBussPro12" runat="server" OnItemCommand="GridBussPro12_ItemCommand" OnItemDataBound="GridBussPro12_ItemDataBound">
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <tr class="gradeA">
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString() %>' meta:resourcekey="lblSRNOResource1"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblBusID" Visible="false" runat="server" Text='<%# Eval("MYID") %>'></asp:Label>
                                                                                                                                                                                <asp:LinkButton ID="lnkAction" runat="server" Style="color: #337ab7" CommandArgument='<%#Eval("MYID") %>' CommandName="linkAction" Text='<%# Eval("ONLINESALEALLOW") %>'></asp:LinkButton>


                                                                                                                                                                                <%-- <table>
                                                                                                                                                                                        <tr>
                                                                                                                                                                                            <td>
                                                                                                                                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnAllowSel" CommandArgument='<%# Eval("MYID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                                                                                            <td>
                                                                                                                                                                                                <asp:LinkButton ID="btnDelete" Style =" margin-left: 50px;" CommandName="btnNotAllow" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("MYID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                                                                                                                            <%-- <td><asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("JobId")%>' CommandName="btnPrint" CommandArgument='<%# Eval("JobId")%>' runat="server" class="btn btn-sm green filter-submit margin-bottom"><i class="fa fa-print"></i></asp:LinkButton></td>
                                                                                                                                                                                        </tr>
                                                                                                                                                                                    </table>--%>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="Qname" runat="server" Text='<%# Eval("MYTYPE") %>' meta:resourcekey="QnameResource1"></asp:Label>

                                                                                                                                                                            </td>

                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblspn" runat="server" Text='<%# Eval("REFSUBTYPE") %>' meta:resourcekey="lblspnResource1"></asp:Label>
                                                                                                                                                                            </td>


                                                                                                                                                                            <td class="center">
                                                                                                                                                                                <asp:LinkButton ID="lnkbtndelete" runat="server" CommandArgument='<%# Eval("MYID") %>' CommandName="btnDelete" OnClientClick="return confirm('Do you want to delete Bisness ?')">
                                                                                                                                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="ECOMM/images/deleteRec.png" />
                                                                                                                                                                                </asp:LinkButton></td>
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
                                                                                                                            </asp:Panel>

                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </ContentTemplate>
                                                                                                        <Triggers>
                                                                                                            <asp:AsyncPostBackTrigger ControlID="GridBussPro12" EventName="ItemCommand" />
                                                                                                        </Triggers>
                                                                                                    </asp:UpdatePanel>
                                                                                                </asp:Panel>
                                                                                                <asp:Panel ID="panMoreItem" runat="server" Style="display: none">

                                                                                                    <div id="ac3-3" class="tab-pane">
                                                                                                        <div class="portlet box blue">
                                                                                                            <div class="portlet-title">
                                                                                                                <div class="caption">
                                                                                                                    <i class="fa fa-gift"></i>MORE ITEM CHARACTERISTICS &nbsp;<asp:Label ID="lblMoreItemMode" runat="server"></asp:Label>
                                                                                                                </div>
                                                                                                                <div class="tools">
                                                                                                                    <a href="javascript:;" class="collapse"></a>
                                                                                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                                                    <a href="javascript:;" class="reload"></a>
                                                                                                                    <a href="javascript:;" class="remove"></a>
                                                                                                                </div>
                                                                                                                <div class="actions btn-set">
                                                                                                                    <asp:LinkButton ID="LinkButton1" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="/assets/global/img/flags/us.png" /></asp:LinkButton>
                                                                                                                    <asp:LinkButton ID="LinkButton4" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="/assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                                                                                    <asp:LinkButton ID="LinkButton5" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="/assets/global/img/flags/fr.png" /></asp:LinkButton>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="portlet-body form">
                                                                                                                <div class="form-horizontal">
                                                                                                                    <div class="form-body">
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="form-group">
                                                                                                                                    <div class="col-md-6">
                                                                                                                                        <asp:Label runat="server" ID="lblColor1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtColor1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <%--<asp:DropDownList ID="ddlcolersh" runat="server" class=" form-control demo minicolors-input">
                                                                                                                            </asp:DropDownList>--%>
                                                                                                                                            <asp:DropDownList ID="dropColoerOne" runat="server" ons class=" form-control select2me">
                                                                                                                                            </asp:DropDownList>
                                                                                                                                            <%-- <asp:TextBox ID="txtxitemchcolore" runat="server" class="form-control"></asp:TextBox>

                                                                                                                                        <cc1:ColorPickerExtender ID="txtCardColor_ColorPickerExtender" TargetControlID="txtxitemchcolore" SampleControlID="txtxitemchcolore" Enabled="True" runat="server"></cc1:ColorPickerExtender>--%>
                                                                                                                                        </div>
                                                                                                                                        <asp:Label runat="server" ID="lblColor2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtColor2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                    </div>
                                                                                                                                    <div class="col-md-6">
                                                                                                                                        <asp:Label runat="server" ID="lblMultiColor1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiColor1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        <div class="col-md-3">
                                                                                                                                            <div class="radio-list">
                                                                                                                                                <asp:RadioButtonList ID="RadioBtnMoreItenMulticolor" OnSelectedIndexChanged="RadioBtnMoreItenMulticolor_SelectedIndexChanged" AutoPostBack="true" runat="server" RepeatDirection="Horizontal">
                                                                                                                                                    <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                                                                                                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                                                                                                                                </asp:RadioButtonList>
                                                                                                                                            </div>

                                                                                                                                        </div>
                                                                                                                                        <asp:Label runat="server" ID="lblMultiColor2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiColor2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        <div class="col-md-5">

                                                                                                                                            <%-- <asp:DropDownList ID="dropMulticooler" OnSelectedIndexChanged="dropMulticooler_SelectedIndexChanged" AutoPostBack="true" Visible="false" runat="server" class=" form-control ">
                                                                                                                            </asp:DropDownList>--%>
                                                                                                                                            <asp:DropDownList ID="dropMulticolor" runat="server" Visible="false" OnSelectedIndexChanged="dropMulticolor_SelectedIndexChanged1" AutoPostBack="true" class=" form-control select2me">
                                                                                                                                            </asp:DropDownList>

                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                                <asp:Panel ID="pnelmulticooler" runat="server" Visible="false">
                                                                                                                                    <fieldset style="margin-bottom: 10px;">
                                                                                                                                        <legend>Multi Colros</legend>
                                                                                                                                        <div class="form-group">
                                                                                                                                            <div class="col-md-12">
                                                                                                                                                <asp:Label runat="server" ID="lblSelectColors1s" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSelectColors1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                <div class="col-md-8">

                                                                                                                                                    <asp:TextBox ID="txtMulticolers" Style="width: 450px;" runat="server" CssClass="form-control tags"></asp:TextBox>
                                                                                                                                                </div>
                                                                                                                                                <asp:Label runat="server" ID="lblSelectColors2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSelectColors2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            </div>

                                                                                                                                        </div>

                                                                                                                                    </fieldset>

                                                                                                                                </asp:Panel>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <asp:AsyncPostBackTrigger ControlID="dropMulticolor" EventName="SelectedIndexChanged" />
                                                                                                                                <asp:AsyncPostBackTrigger ControlID="txtMulticolers" />
                                                                                                                            </Triggers>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="form-group">
                                                                                                                                    <div class="col-md-6">
                                                                                                                                        <asp:Label runat="server" ID="lblBrand1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBrand1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        <div class="col-md-6">
                                                                                                                                            <asp:DropDownList ID="ddlBrandname" runat="server" class=" form-control select2me">
                                                                                                                                            </asp:DropDownList>
                                                                                                                                        </div>
                                                                                                                                        <asp:Label runat="server" ID="lblBrand2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBrand2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        <div class="col-md-2" >
                                                                                                                                            <asp:Image ID="Image3" runat="server" ImageUrl="ECOMM/images/plus.png" OnClick="javascript:showpanepanewebline();" Style="float: right;margin-top: -27px;" />
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                    <div class="col-md-6">
                                                                                                                                        <asp:Label runat="server" ID="lblSKUProduction1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSKUProduction1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <div class="radio-list">
                                                                                                                                                <asp:RadioButtonList ID="rbtnSKUP" runat="server" RepeatDirection="Horizontal">
                                                                                                                                                    <asp:ListItem Selected="True" Value="1" Text="Yes"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                                                                                                                                </asp:RadioButtonList>
                                                                                                                                            </div>
                                                                                                                                        </div>

                                                                                                                                        <asp:Label runat="server" ID="lblSKUProduction2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSKUProduction2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                                <asp:Panel ID="panewebline" runat="server" Style="display: none" meta:resourcekey="panebusslineResource1">
                                                                                                                                    <fieldset style="margin-bottom: 10px;">
                                                                                                                                        <legend>Dispaly On Site By Brand</legend>
                                                                                                                                        <div class="row" style="margin-right: 0px;">
                                                                                                                                            <asp:Panel ID="panelBrandmsg" runat="server" Visible="false">
                                                                                                                                                <div class="alert alert-danger alert-dismissable">
                                                                                                                                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                                                                                                    <asp:Label ID="lblBrandmsg" runat="server"></asp:Label>
                                                                                                                                                </div>
                                                                                                                                            </asp:Panel>
                                                                                                                                            <div class="form-group">
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="Label5" class="col-md-4 control-label" Text="Placeholder Line"></asp:Label>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtbrandpl" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpplResource1"></asp:TextBox>
                                                                                                                                                    </div>
                                                                                                                                                </div>
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="Label8" class="col-md-4 control-label" Text="Placeholder Ali R L"></asp:Label>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtbrandparl" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbprlResource1"></asp:TextBox>
                                                                                                                                                    </div>

                                                                                                                                                </div>
                                                                                                                                            </div>


                                                                                                                                            <div class="form-group">
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="Label10" class="col-md-4 control-label" Text="Placeholder Column"></asp:Label>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtbrandpcl" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtpbcolResource1"></asp:TextBox>
                                                                                                                                                    </div>

                                                                                                                                                </div>
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="Label12" class="col-md-4 control-label" Text="Link 2Direct"></asp:Label>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtbrandl2d" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpl2dResource1"></asp:TextBox>
                                                                                                                                                    </div>

                                                                                                                                                </div>
                                                                                                                                            </div>

                                                                                                                                            <div class="form-group">
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="Label15" class="col-md-4 control-label" Text="Sort Number"></asp:Label>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtbrandsort" Text="0" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpsnoResource1"></asp:TextBox>
                                                                                                                                                    </div>

                                                                                                                                                </div>
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Button ID="btnbrand" runat="server" CssClass="btn btn-primary" OnClick="btnbrand_Click" Text="Save" Height="34px" />

                                                                                                                                                </div>
                                                                                                                                            </div>

                                                                                                                                        </div>
                                                                                                                                    </fieldset>
                                                                                                                                </asp:Panel>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <asp:AsyncPostBackTrigger ControlID="btnbrand" EventName="Click" />
                                                                                                                            </Triggers>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="form-group">
                                                                                                                                    <div class="col-md-6">
                                                                                                                                        <asp:Label runat="server" ID="lblSelectSize1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSelectSize1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <asp:DropDownList ID="ddlsize" runat="server" CssClass="form-control select2me" meta:resourcekey="ddlsizeResource1">
                                                                                                                                            </asp:DropDownList>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                    <asp:Label runat="server" ID="lblSelectSize2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSelectSize2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                                                                                    <div class="col-md-6">
                                                                                                                                        <asp:Label runat="server" ID="lblSize1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSize1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        <div class="col-md-3">
                                                                                                                                            <div class="radio-list">
                                                                                                                                                <asp:RadioButtonList ID="RadioBtnMoreItenMultisize" OnSelectedIndexChanged="RadioBtnMoreItenMultisize_SelectedIndexChanged" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" meta:resourcekey="RadioBtnMoreItenMultisizeResource1">
                                                                                                                                                    <asp:ListItem Value="1" Selected="True" Text="Yes" meta:resourcekey="ListItemResource13"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Value="0" Text="No" meta:resourcekey="ListItemResource14"></asp:ListItem>
                                                                                                                                                </asp:RadioButtonList>
                                                                                                                                            </div>
                                                                                                                                        </div>
                                                                                                                                        <asp:Label runat="server" ID="lblSize2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSize2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        <div class="col-md-5">
                                                                                                                                            <asp:DropDownList ID="dorpMultiSize" runat="server" Visible="false" OnSelectedIndexChanged="dorpMultiSize_SelectedIndexChanged" AutoPostBack="true" class=" form-control select2me">
                                                                                                                                            </asp:DropDownList>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                                <asp:Panel ID="panelMultisize" runat="server" Visible="false">
                                                                                                                                    <fieldset style="margin-bottom: 10px;">
                                                                                                                                        <legend>Multi Size</legend>
                                                                                                                                        <div class="form-group">
                                                                                                                                            <div class="col-md-12">
                                                                                                                                                <asp:Label runat="server" ID="lblMultiSizeItem1s" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiSizeItem1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                <div class="col-md-8">
                                                                                                                                                    <asp:TextBox ID="txtMultiSize" name="number" runat="server" CssClass="form-control tags"></asp:TextBox>
                                                                                                                                                </div>
                                                                                                                                                <asp:Label runat="server" ID="lblMultiSizeItem2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiSizeItem2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            </div>

                                                                                                                                        </div>

                                                                                                                                    </fieldset>
                                                                                                                                </asp:Panel>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <asp:AsyncPostBackTrigger ControlID="dorpMultiSize" EventName="SelectedIndexChanged" />
                                                                                                                            </Triggers>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="form-group">
                                                                                                                                    <div class="col-md-6 ">
                                                                                                                                        <asp:Label runat="server" ID="Label17" class="col-md-4 control-label" Text="UOM"></asp:Label>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <asp:DropDownList ID="drpUOM" runat="server" CssClass="form-control select2me">
                                                                                                                                            </asp:DropDownList>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                    <div class="col-md-6">
                                                                                                                                        <asp:Label runat="server" ID="lblMultiUOM1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiUOM1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        <div class="col-md-3">
                                                                                                                                            <div class="radio-list">
                                                                                                                                                <asp:RadioButtonList ID="rdbtnMUOM" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbtnMUOM_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                                                                                                                    <asp:ListItem Value="1" Selected="True" Text="Yes"></asp:ListItem>
                                                                                                                                                    <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                                                                                                                                </asp:RadioButtonList>
                                                                                                                                            </div>
                                                                                                                                        </div>
                                                                                                                                        <asp:Label runat="server" ID="lblMultiUOM2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiUOM2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        <%-- <div class="col-md-2">
                                                                                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control select2me" meta:resourcekey="ddlsizeResource1">
                                                                                                                </asp:DropDownList>
                                                                                                            </div>--%>
                                                                                                                                        <div class="col-md-5">
                                                                                                                                            <asp:DropDownList ID="ddluom" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddluom_SelectedIndexChanged" runat="server" CssClass="form-control select2me">
                                                                                                                                            </asp:DropDownList>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </div>

                                                                                                                                <asp:Panel ID="panelMultiUOM" runat="server" Visible="false">
                                                                                                                                    <div class="form-group">
                                                                                                                                        <div class="col-md-12">
                                                                                                                                            <asp:Label runat="server" ID="Label3" Text="Multi UOM" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox1" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            <div class="col-md-8">
                                                                                                                                                <asp:TextBox ID="txtmultiMOU" name="number" runat="server" CssClass="form-control tags"></asp:TextBox>
                                                                                                                                            </div>
                                                                                                                                            <asp:Label runat="server" ID="Label4" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox3" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        </div>

                                                                                                                                    </div>
                                                                                                                                </asp:Panel>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <asp:AsyncPostBackTrigger ControlID="ddluom" EventName="SelectedIndexChanged" />

                                                                                                                            </Triggers>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="form-group ">
                                                                                                                                    <div class="col-md-12">

                                                                                                                                        <asp:Label runat="server" ID="lblMainCategory1s" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMainCategory1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        <div class="col-md-6">
                                                                                                                                            <asp:DropDownList ID="ddlSuppCat" runat="server" CssClass="form-control select2me" meta:resourcekey="ddlSuppCatResource1">
                                                                                                                                            </asp:DropDownList>
                                                                                                                                        </div>
                                                                                                                                        <div class="col-md-1">
                                                                                                                                            <asp:Image ID="Image5" runat="server" ImageUrl="ECOMM/images/plus.png" OnClick="javascript:showpanepaneofferline();" Style="float: right" />
                                                                                                                                        </div>
                                                                                                                                        <div class="col-md-1">
                                                                                                                                            <asp:LinkButton ID="CatEdit" Visible="false" runat="server" OnClick="CatEdit_Click">
                                                                                                                                <%--OnClick="lnkbtnEdateMProduct_Click"--%><span class="glyphicon glyphicon-edit" style ="color :blue ;margin-top:5px;font-size:20px;line-height:20px"> </span><span class="bs-glyphicon-class"></span>
                                                                                                                               <%-- <asp:Image ID="Image1" runat="server" ImageUrl="ECOMM/images/editRec.png" />--%>
                                                                                                                                            </asp:LinkButton>
                                                                                                                                        </div>

                                                                                                                                        <asp:Label runat="server" ID="lblMainCategory2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMainCategory2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                                    </div>


                                                                                                                                </div>
                                                                                                                                <asp:Panel ID="PanelOFFERline" runat="server" Style="display: none" meta:resourcekey="panebusslineResource1">
                                                                                                                                    <fieldset style="margin-bottom: 10px;">
                                                                                                                                        <legend>Dispaly On Site By Category</legend>
                                                                                                                                        <div class="row" style="margin-right: 0px;">
                                                                                                                                            <asp:Panel ID="pnelCategorymsg" runat="server" Visible="false">
                                                                                                                                                <div class="alert alert-danger alert-dismissable">
                                                                                                                                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                                                                                                    <asp:Label ID="lblCategoryMsg" runat="server"></asp:Label>
                                                                                                                                                </div>
                                                                                                                                            </asp:Panel>
                                                                                                                                            <div class="form-group">
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="Label7" class="col-md-4 control-label" Text="Placeholder Line"></asp:Label>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtcatepl" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpplResource1"></asp:TextBox>
                                                                                                                                                    </div>
                                                                                                                                                </div>
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="Label9" class="col-md-4 control-label" Text="Placeholder Ali R L"></asp:Label>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtcateparl" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbprlResource1"></asp:TextBox>
                                                                                                                                                    </div>

                                                                                                                                                </div>
                                                                                                                                            </div>
                                                                                                                                            <div class="form-group">
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="Label11" class="col-md-4 control-label" Text="Placeholder Column"></asp:Label>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtcatepcol" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtpbcolResource1"></asp:TextBox>
                                                                                                                                                    </div>

                                                                                                                                                </div>
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="Label13" class="col-md-4 control-label" Text="Link 2Direct"></asp:Label>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtcatl2d" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpl2dResource1"></asp:TextBox>
                                                                                                                                                    </div>

                                                                                                                                                </div>
                                                                                                                                            </div>

                                                                                                                                            <div class="form-group">
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Label runat="server" ID="Label16" class="col-md-4 control-label" Text="Sort Number"></asp:Label>
                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                        <asp:TextBox ID="txtcatsornum" Text="0" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpsnoResource1"></asp:TextBox>
                                                                                                                                                    </div>

                                                                                                                                                </div>
                                                                                                                                                <div class="col-md-6">
                                                                                                                                                    <asp:Button ID="btncategory" runat="server" CssClass="btn btn-primary" OnClick="btncategory_Click" Text="Save" Height="34px" />

                                                                                                                                                </div>
                                                                                                                                            </div>

                                                                                                                                        </div>
                                                                                                                                    </fieldset>
                                                                                                                                </asp:Panel>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <asp:AsyncPostBackTrigger ControlID="btncategory" EventName="Click" />
                                                                                                                            </Triggers>
                                                                                                                        </asp:UpdatePanel>

                                                                                                                        <div class="form-group">
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblQuantityonHand1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantityonHand1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <asp:TextBox ID="txtQtnonHand" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtQtnonHandResource1"></asp:TextBox>

                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblQuantityonHand2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantityonHand2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblwarranty1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtwarranty1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <asp:TextBox ID="txtWarranty" runat="server" MaxLength="4" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtQtnonHandResource1"></asp:TextBox>
                                                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtWarranty" FilterType="Custom, numbers" runat="server" />

                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblwarranty2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtwarranty2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="form-group">
                                                                                                                            <div class="col-md-12">
                                                                                                                                <asp:Label runat="server" ID="lblKeywords1s" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtKeywords1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-10">
                                                                                                                                    <asp:TextBox ID="tags_4" name="number" runat="server" CssClass="form-control tags"></asp:TextBox>
                                                                                                                                    <%--  <asp:TextBox ID="" runat="server" AutoCompleteType="Disabled" CssClass="form-control"   meta:resourcekey="txtMoreItenKeywordResource1"></asp:TextBox>--%>
                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblKeywords2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtKeywords2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="form-group">
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblProductStrategicalGroup1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductStrategicalGroup1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <asp:DropDownList ID="ddlItGrp" runat="server" CssClass="form-control select2me" meta:resourcekey="ddlItGrpResource1">
                                                                                                                                    </asp:DropDownList>

                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblProductStrategicalGroup2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductStrategicalGroup2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblDepartmentofSale1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDepartmentofSale1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <asp:DropDownList ID="ddlDofSale" runat="server" CssClass="form-control select2me" meta:resourcekey="ddlDofSaleResource1">
                                                                                                                                    </asp:DropDownList>
                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblDepartmentofSale2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDepartmentofSale2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="form-group">
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblHotItem1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtHotItem1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <div class="radio-list">
                                                                                                                                        <asp:RadioButtonList ID="RadioBtnMoreItenHotItem" runat="server" RepeatDirection="Horizontal">
                                                                                                                                            <asp:ListItem Selected="True" Value="1" Text="Yes"></asp:ListItem>
                                                                                                                                            <asp:ListItem Value="0" Text="NO"></asp:ListItem>
                                                                                                                                        </asp:RadioButtonList>
                                                                                                                                    </div>

                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblHotItem2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtHotItem2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblSerializedItem1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSerializedItem1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">

                                                                                                                                    <asp:RadioButtonList ID="RadioBtnMoreItenSerItem" runat="server" RepeatDirection="Horizontal" meta:resourcekey="RadioBtnMoreItenSerItemResource1">
                                                                                                                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                                                                                                        <asp:ListItem Value="0" Selected="True" Text="NO"></asp:ListItem>
                                                                                                                                    </asp:RadioButtonList>
                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblSerializedItem2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSerializedItem2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="form-group">
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblPurchaseAllowed1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPurchaseAllowed1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <div class="radio-list">
                                                                                                                                        <asp:RadioButtonList ID="RadioBtnMoreItenPurchAllow" runat="server" RepeatDirection="Horizontal" meta:resourcekey="RadioBtnMoreItenPurchAllowResource1">
                                                                                                                                            <asp:ListItem Value="1" Selected="True" Text="Yes" meta:resourcekey="ListItemResource15"></asp:ListItem>
                                                                                                                                            <asp:ListItem Value="0" Text="NO" meta:resourcekey="ListItemResource16"></asp:ListItem>
                                                                                                                                        </asp:RadioButtonList>
                                                                                                                                    </div>
                                                                                                                                    <asp:Label runat="server" ID="lblPurchaseAllowed2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPurchaseAllowed2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblSaleAllowed1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSaleAllowed1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <div class="radio-list">
                                                                                                                                        <asp:RadioButtonList ID="RadioBtnMoreItenSellAllow" runat="server" RepeatDirection="Horizontal" meta:resourcekey="RadioBtnMoreItenSellAllowResource1">
                                                                                                                                            <asp:ListItem Value="1" Selected="True" Text="Yes" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                                                                                                                            <asp:ListItem Value="0" Text="NO" meta:resourcekey="ListItemResource18"></asp:ListItem>
                                                                                                                                        </asp:RadioButtonList>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblSaleAllowed2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSaleAllowed2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="form-group">
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblMultiBinStore1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiBinStore1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <div class="radio-list">
                                                                                                                                        <asp:RadioButtonList ID="rbtnMBSto" runat="server" RepeatDirection="Horizontal" meta:resourcekey="rbtnMBStoResource1">
                                                                                                                                            <asp:ListItem Value="1" Selected="True" Text="Yes" meta:resourcekey="ListItemResource19"></asp:ListItem>
                                                                                                                                            <asp:ListItem Value="0" Text="NO" meta:resourcekey="ListItemResource20"></asp:ListItem>
                                                                                                                                        </asp:RadioButtonList>
                                                                                                                                    </div>

                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblMultiBinStore2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiBinStore2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblPerishable1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPerishable1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <div class="radio-list">
                                                                                                                                        <asp:RadioButtonList ID="rbtPsle" runat="server" RepeatDirection="Horizontal" meta:resourcekey="rbtPsleResource1">
                                                                                                                                            <asp:ListItem Value="1" Selected="True" Text="Yes" meta:resourcekey="ListItemResource21"></asp:ListItem>
                                                                                                                                            <asp:ListItem Value="0" Text="NO" meta:resourcekey="ListItemResource22"></asp:ListItem>
                                                                                                                                        </asp:RadioButtonList>
                                                                                                                                    </div>
                                                                                                                                </div>

                                                                                                                                <asp:Label runat="server" ID="lblPerishable2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPerishable2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="form-group">
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblProductMethod1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductMethod1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <asp:DropDownList ID="ddlMoreItenProMethod" runat="server" CssClass="form-control select2me" meta:resourcekey="ddlMoreItenProMethodResource1">
                                                                                                                                    </asp:DropDownList>
                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblProductMethod2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductMethod2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>

                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblSupplyMethod1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplyMethod1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <asp:DropDownList ID="ddlMoreItenSupplyMethod" runat="server" CssClass="form-control select2me" meta:resourcekey="ddlMoreItenSupplyMethodResource1">
                                                                                                                                    </asp:DropDownList>
                                                                                                                                </div>

                                                                                                                                <asp:Label runat="server" ID="lblSupplyMethod2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplyMethod2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </div>

                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                </asp:Panel>
                                                                                                <asp:Panel ID="pnelInternal" runat="server" Style="display: none">
                                                                                                    <div id="ac3-4" class="tab-pane">
                                                                                                        <div class="portlet box purple">
                                                                                                            <div class="portlet-title">
                                                                                                                <div class="caption">
                                                                                                                    <i class="fa fa-gift"></i>INTERNAL NOTES AND PRICES &nbsp;<asp:Label ID="lblInterMode" runat="server"></asp:Label>
                                                                                                                </div>
                                                                                                                <div class="tools">
                                                                                                                    <a href="javascript:;" class="collapse"></a>
                                                                                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                                                    <a href="javascript:;" class="reload"></a>
                                                                                                                    <a href="javascript:;" class="remove"></a>
                                                                                                                </div>
                                                                                                                <div class="actions btn-set">
                                                                                                                    <asp:LinkButton ID="LinkButton6" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="/assets/global/img/flags/us.png" /></asp:LinkButton>
                                                                                                                    <asp:LinkButton ID="LinkButton7" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="/assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                                                                                    <asp:LinkButton ID="LinkButton8" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="/assets/global/img/flags/fr.png" /></asp:LinkButton>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="portlet-body form">
                                                                                                                <div class="form-horizontal">
                                                                                                                    <div class="form-body">
                                                                                                                        <div class="form-group">
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblInternalNotes1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtInternalNotes1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <asp:TextBox ID="txtnoteprinote" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblInternalNotes2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtInternalNotes2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblCostPrice1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCostPrice1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <asp:TextBox ID="txtnotepricostprice" runat="server" AutoCompleteType="Disabled" placeholder="Enter Cost Price" CssClass="form-control"></asp:TextBox>
                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblCostPrice2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCostPrice2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="form-group">
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblCurrency1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCurrency1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <asp:DropDownList ID="drpCurrency" CssClass="form-control select2me" runat="server" data-toggle="tooltip" ToolTip="Currency"></asp:DropDownList>
                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblCurrency2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCurrency2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblSalePrice11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSalePrice11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <asp:TextBox ID="txtnoteprisaleori1" runat="server" AutoCompleteType="Disabled" placeholder="20% of the cost price" CssClass="form-control" meta:resourcekey="txtnoteprisaleori1Resource1"></asp:TextBox>
                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblSalePrice12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSalePrice12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="form-group">
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblWebSalePrice1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtWebSalePrice1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <asp:TextBox ID="txtnotepriwebsalepri" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtnotepriwebsalepriResource1"></asp:TextBox>
                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblWebSalePrice2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtWebSalePrice2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>

                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblSalePrice21s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSalePrice21s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <asp:TextBox ID="txtnoteprisaleori2" runat="server" AutoCompleteType="Disabled" placeholder="30% of the cost price" CssClass="form-control" meta:resourcekey="txtnoteprisaleori2Resource1"></asp:TextBox>
                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblSalePrice22h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSalePrice22h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="form-group">
                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblmsrp1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtmsrp1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <asp:TextBox ID="txtnoteprimrp" runat="server" AutoCompleteType="Disabled" placeholder="60% of the cost price" CssClass="form-control" meta:resourcekey="txtnoteprimrpResource1"></asp:TextBox>
                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblmsrp2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtmsrp2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>

                                                                                                                            <div class="col-md-6">
                                                                                                                                <asp:Label runat="server" ID="lblSalePrice31s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSalePrice31s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                <div class="col-md-8">
                                                                                                                                    <asp:TextBox ID="txtnoteprisaleori3" runat="server" AutoCompleteType="Disabled" placeholder="40% of the cost price" CssClass="form-control" meta:resourcekey="txtnoteprisaleori3Resource1"></asp:TextBox>
                                                                                                                                </div>
                                                                                                                                <asp:Label runat="server" ID="lblSalePrice32h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSalePrice32h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </asp:Panel>
                                                                                            </div>
                                                                                        </div>

                                                                                    </div>
                                                                                    <div id="pPriceNote" class="tab-pane">
                                                                                        <div class="tabbable">
                                                                                            <ul class="nav nav-tabs">
                                                                                                <li class="active"><a data-toggle="tab" style="color: #fff; background-color: #1bbc9b" href="#ac3-41">
                                                                                                    <asp:Label ID="lblProSFF" runat="server" Text="PRODUCT SALE FORM FENCHISE" meta:resourcekey="lblProSFFResource1"></asp:Label>
                                                                                                </a></li>
                                                                                                <li><a href="#ac3-42" data-toggle="tab" style="color: #fff; background-color: #e26a6a">
                                                                                                    <asp:Label ID="Label24" runat="server" Text="INFORMATIVE DATA" meta:resourcekey="Label24Resource1"></asp:Label>
                                                                                                </a></li>

                                                                                            </ul>
                                                                                            <div class="tab-content no-space">
                                                                                                <div id="ac3-41" class="tab-pane active">
                                                                                                    <div class="panel-body">
                                                                                                        <div class="form-body">
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblAllowDirectSale1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAllowDirectSale1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">

                                                                                                                        <asp:RadioButtonList ID="RatioProSaleFen" runat="server" RepeatDirection="Horizontal" Width="123px" onclick="GetRadioButtonListSelectedValue(this);" meta:resourcekey="RatioProSaleFenResource1">
                                                                                                                            <asp:ListItem Value="0" Text="Yes" meta:resourcekey="ListItemResource59"></asp:ListItem>
                                                                                                                            <asp:ListItem Selected="True" Value="1" Text="No" meta:resourcekey="ListItemResource60"></asp:ListItem>
                                                                                                                        </asp:RadioButtonList>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblAllowDirectSale2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAllowDirectSale2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblProductIDD1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductIDD1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">

                                                                                                                        <asp:Label ID="ProSaleFenProid" runat="server" meta:resourcekey="ProSaleFenProidResource1"></asp:Label>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblProductIDD2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductIDD2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-12">
                                                                                                                    <asp:Label runat="server" ID="lblRemarks1s" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRemarks1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-10">
                                                                                                                        <asp:TextBox ID="txtProSaleFenRemark" AutoCompleteType="Disabled" CssClass="form-control" runat="server" meta:resourcekey="txtStreetResource1"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblRemarks2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRemarks2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>

                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-12">
                                                                                                                    <asp:Label runat="server" ID="lblLINK2DIRECTD1s" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLINK2DIRECTD1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-10">
                                                                                                                        <asp:TextBox ID="txtLINK2DIRECT" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtMoreItenwarrantyLink2dResource1"></asp:TextBox>

                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblLINK2DIRECTD2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLINK2DIRECTD2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblKeywordD1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtKeywordD1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="tags_3" name="number" runat="server" CssClass="form-control tags" meta:resourcekey="tags_3Resource1"></asp:TextBox>
                                                                                                                        <%--<asp:TextBox ID="txtkeywored" runat="server" AutoCompleteType="Disabled" CssClass="form-control"   meta:resourcekey="txtMoreItenwarrantyLink2dResource1"></asp:TextBox>--%>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblKeywordD2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtKeywordD2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblBoxshot1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBoxshot1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtboxshot" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtMoreItenwarrantySortnoResource1"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblBoxshot2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBoxshot2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblLargeBoxshot1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLargeBoxshot1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtlarge_boxshot" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtMoreItenwarrantyLink2dResource1"></asp:TextBox>

                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblLargeBoxshot2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLargeBoxshot2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblMediumBoxshot1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMediumBoxshot1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtmedium_boxshot" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtMoreItenwarrantySortnoResource1"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblMediumBoxshot2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMediumBoxshot2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblSmallBoxshot1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSmallBoxshot1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtsmall_boxshot" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtMoreItenwarrantyLink2dResource1"></asp:TextBox>

                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblSmallBoxshot2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSmallBoxshot2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblOsPlatform1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOsPlatform1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtos_platform" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtMoreItenwarrantySortnoResource1"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblOsPlatform2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOsPlatform2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblCorpLogo1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCorpLogo1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtcorp_logo" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtMoreItenwarrantyLink2dResource1"></asp:TextBox>

                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblCorpLogo2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCorpLogo2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblLadingPage1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLadingPage1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtlading_page" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtMoreItenwarrantySortnoResource1"></asp:TextBox>

                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblLadingPage2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLadingPage2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblLine1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLine1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtline" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtMoreItenwarrantyplclineResource1"></asp:TextBox>

                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblLine2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLine2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblTrialUrl1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTrialUrl1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txttrial_url" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtMoreItenwarrantyplcAliRLResource1"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblTrialUrl2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTrialUrl2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblCartLink1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCartLink1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtcart_link" runat="server" AutoCompleteType="Disabled" ToolTip="Short Name" data-trigger="hover" CssClass="form-control"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblCartLink2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCartLink2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>

                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblProductDetailLink1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductDetailLink1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtproduct_detail_link" runat="server" ToolTip="Loading Page Link" data-trigger="hover" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblProductDetailLink2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductDetailLink2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblLead1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLead1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtlead" runat="server" AutoCompleteType="Disabled" ToolTip="Loading Page Link Rewrite to show" data-trigger="hover" CssClass="form-control"></asp:TextBox>

                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblLead2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLead2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>

                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblOther1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOther1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtother" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtMoreItenwarrantyplcColResource1"></asp:TextBox>

                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblOther2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOther2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblPromotionType1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPromotionType1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtpromotion_type" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtMoreItenwarrantyLink2dResource1"></asp:TextBox>

                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblPromotionType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPromotionType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>

                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblPayout1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPayout1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtpayout" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtMoreItenwarrantySortnoResource1"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblPayout2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPayout2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <asp:Panel ID="PanelPRODUCTSALEFORM" runat="server" Style="display: none" meta:resourcekey="PanelPRODUCTSALEFORMResource1">
                                                                                                                <div class="form-body">
                                                                                                                    <div class="form-group">
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Lasbel6" runat="server" Text="Active" meta:resourcekey="Lasbel6Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:RadioButtonList ID="RadioProSaleFenAct" runat="server" RepeatDirection="Horizontal" Width="123px" meta:resourcekey="RadioProSaleFenActResource1">
                                                                                                                                <asp:ListItem Value="1" Text="Yes" meta:resourcekey="ListItemResource61"></asp:ListItem>
                                                                                                                                <asp:ListItem Selected="True" Value="2" Text="No" meta:resourcekey="ListItemResource62"></asp:ListItem>
                                                                                                                            </asp:RadioButtonList>
                                                                                                                        </div>
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Labesl23" runat="server" Text="Loading Page" meta:resourcekey="Labesl23Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">

                                                                                                                            <asp:TextBox ID="txtProSaleFenLaodpage" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtProSaleFenLaodpageResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                    </div>

                                                                                                                    <div class="form-group">
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Labgel9" runat="server" Text="Boxshot" meta:resourcekey="Labgel9Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:TextBox ID="txtProSaleFenBoxshot" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtProSaleFenBoxshotResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Labjel16" runat="server" Text="TrialUrl" meta:resourcekey="Labjel16Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">

                                                                                                                            <asp:TextBox ID="txtProSaleFenTiralURL" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtProSaleFenTiralURLResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                    </div>



                                                                                                                    <div class="form-group">
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Laberl10" runat="server" Text="Large boxshot" meta:resourcekey="Laberl10Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:TextBox ID="txtProSaleFenLBoxshot" AutoCompleteType="Disabled" CssClass="form-control" runat="server" meta:resourcekey="txtStreetResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Laberl19" runat="server" Text="Cart Link" meta:resourcekey="Laberl19Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">

                                                                                                                            <asp:TextBox ID="txtProSaleFenCartUrl" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtProSaleFenCartUrlResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                    </div>


                                                                                                                    <div class="form-group">
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Labelr11" runat="server" Text="Medium boxshot" meta:resourcekey="Labelr11Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:TextBox ID="txtProSaleFenMBoxshot" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtProSaleFenMBoxshotResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Larbel20" runat="server" Text="Product Details Link" meta:resourcekey="Larbel20Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">

                                                                                                                            <asp:TextBox ID="txtProSaleFenProDtlLink" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtProSaleFenProDtlLinkResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                    </div>



                                                                                                                    <div class="form-group">
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Labelt12" runat="server" Text="Small boxshot" meta:resourcekey="Labelt12Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:TextBox ID="txtProSaleFenSBoxshot" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtProSaleFenSBoxshotResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Label81" runat="server" Text="Lead" meta:resourcekey="Label81Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">

                                                                                                                            <asp:TextBox ID="txtProSaleFenLead" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtProSaleFenLeadResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                    </div>


                                                                                                                    <div class="form-group">
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Labtel15" runat="server" Text="Os Platfrom" meta:resourcekey="Labtel15Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:TextBox ID="txtProSaleFenOsPlatform" AutoCompleteType="Disabled" CssClass="form-control" runat="server" meta:resourcekey="txtStreetResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Label82" runat="server" Text="Other" meta:resourcekey="Label82Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">

                                                                                                                            <asp:TextBox ID="txtProSaleFenOhter" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtProSaleFenOhterResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                    </div>


                                                                                                                    <div class="form-group">
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Labtel17" runat="server" Text="Corp Logo" meta:resourcekey="Labtel17Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:TextBox ID="txtProSaleFenCorpLogo" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtProSaleFenCorpLogoResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Label2t1" runat="server" Text="Promotion Type" meta:resourcekey="Label2t1Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">

                                                                                                                            <asp:TextBox ID="txtProSaleFenProType" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtProSaleFenProTypeResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                    </div>


                                                                                                                    <div class="form-group">
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Labelr18" runat="server" Text="Link" meta:resourcekey="Labelr18Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">
                                                                                                                            <asp:TextBox ID="txtProSaleFenLink" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtProSaleFenLinkResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                        <label class="col-md-2 control-label">
                                                                                                                            <asp:Label ID="Labrel22" runat="server" Text="Payout" meta:resourcekey="Labrel22Resource1"></asp:Label>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-4">

                                                                                                                            <asp:TextBox ID="txtProSaleFenPayout" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtProSaleFenPayoutResource1"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                    </div>

                                                                                                                </div>
                                                                                                            </asp:Panel>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div id="ac3-42" class="panel-collapse collapse">
                                                                                                    <div class="panel-body">

                                                                                                        <div class="form-body">
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblLastDisplayDateonWeb1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLastDisplayDateonWeb1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtINFOlastdisonweb" Enabled="False" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtINFOlastdisonwebResource1"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblLastDisplayDateonWeb2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLastDisplayDateonWeb2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblSystemRemarks1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSystemRemarks1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtINFOsysremark" runat="server" Enabled="False" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtINFOsysremarkResource1"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblSystemRemarks2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSystemRemarks2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblLastPurchaseDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLastPurchaseDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtINFOlastpurchdt" Enabled="False" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtINFOlastpurchdtResource1"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblLastPurchaseDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLastPurchaseDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblLastSalesDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLastSalesDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtINFOlastsaledt" Enabled="False" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtINFOlastsaledtResource1"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblLastSalesDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLastSalesDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblQuantityRecived1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantityRecived1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtINFOQntyRes" runat="server" Enabled="False" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtINFOQntyResResource1"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblQuantityRecived2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantityRecived2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblQuantitySold1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantitySold1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtINFOQntySold" runat="server" Enabled="False" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtINFOQntySoldResource1"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblQuantitySold2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantitySold2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblQuantityOnHandDP1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantityOnHandDP1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:TextBox ID="txtINFOQntyonhand" Enabled="False" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtINFOQntyonhandResource1"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblQuantityOnHandDP2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantityOnHandDP2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>

                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblActiveDP1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActiveDP1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <a href="#">
                                                                                                                            <asp:RadioButtonList ID="RadioINFOAct" runat="server" RepeatDirection="Horizontal" meta:resourcekey="RadioINFOActResource1">
                                                                                                                                <asp:ListItem Value="1" meta:resourcekey="ListItemResource63">Yes</asp:ListItem>
                                                                                                                                <asp:ListItem Selected="True" Value="0" meta:resourcekey="ListItemResource64">NO</asp:ListItem>
                                                                                                                            </asp:RadioButtonList>
                                                                                                                        </a>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblActiveDP2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActiveDP2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <div class="col-md-6">
                                                                                                                    <asp:Label runat="server" ID="lblSupplierList1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplierList1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                    <div class="col-md-8">
                                                                                                                        <asp:Label ID="Label141" runat="server" Text="Customer List" meta:resourcekey="Label141Resource1"></asp:Label>
                                                                                                                    </div>
                                                                                                                    <asp:Label runat="server" ID="lblSupplierList2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplierList2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                            <div class="btnsetposition">
                                                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Style="padding-top: 7px; padding-bottom: 7px" Text="Add New" OnClick="Button1_Click" />
                                                                                <asp:Button ID="BtnMstProSave" runat="server" CssClass="btn btn-primary" Style="padding-top: 7px; padding-bottom: 7px" ValidationGroup="submit" meta:resourcekey="btnSubmitSearchResource1" Text="Save" OnClick="BtnMstProSave_Click" />
                                                                                <asp:Button ID="btnDiscard" runat="server" CssClass="btn btn-primary" Style="padding-top: 7px; background-color: red; color: #fff; padding-bottom: 7px" meta:resourcekey="btnCloseResource1" Text="Discard" OnClick="btnDiscard_Click" OnClientClick="return ConfirmMsg2();" />
                                                                                <asp:Button ID="btnupcuntinu" runat="server" CssClass="btn btn-primary" Style="padding-top: 7px; padding-bottom: 7px" ValidationGroup="submit" meta:resourcekey="btnSubmitSearchResource1" Visible="false" Text="Update & Continue" OnClick="btnupcuntinu_Click" />
                                                                                <asp:Button ID="Button6" runat="server" CssClass="btn btn-primary" Style="padding-top: 7px; background-color: red; color: #fff; padding-bottom: 7px" meta:resourcekey="btnCloseResource1" Text="Cancel" OnClick="Button5_Click" OnClientClick="return ConfirmMsg3();" />
                                                                                <asp:HiddenField ID="hidcheck" runat="server" />
                                                                                <asp:HiddenField ID="hidTenantId" runat="server" />
                                                                                <asp:HiddenField ID="hidMyProdid" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="Button2" />
                                        <asp:PostBackTrigger ControlID="Button1" />

                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <asp:Panel ID="LstView" runat="server" meta:resourcekey="LstViewResource1">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-horizontal form-row-seperated">
                                <div class="portlet box green-turquoise">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-gift"></i>Product List 
                                        </div>
                                        <div class="tools">
                                            <a id="ListPAnel" runat="server" href="javascript:;" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <a href="javascript:;" class="reload"></a>
                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                        
                                    </div>
                                    <div class="portlet-body" id="ListProdcuListPanel" runat="server" style="padding-left: 25px;">
                                        <%--  <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>--%>

                                        <table class="table table-striped table-bordered table-hover" id="sample_1">
                                            <thead class="repHeader">
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="lblSN" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label38" runat="server" Text="PrductID" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblSName" runat="server" Text="Product Name" meta:resourcekey="lblSNameResource2"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblProName" runat="server" Text="Qty in Hand" meta:resourcekey="lblProNameResource1"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblProOname" runat="server" Text="Brand" meta:resourcekey="lblProOnameResource1"></asp:Label></th>

                                                    <th>
                                                        <asp:Label ID="lblEdit" runat="server" Text="Edit" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblDel" runat="server" Text="Delete" meta:resourcekey="lblDelResource1"></asp:Label></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="pMasterGridview" runat="server" OnItemCommand="pMasterGridview_ItemCommand">
                                                    <ItemTemplate>
                                                        <tr class="gradeA">
                                                            <td>
                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString() %>' meta:resourcekey="lblSRNOResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label39" runat="server" Text='<%# Eval("UserProdID") %>' meta:resourcekey="lblSRNOResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Qname" runat="server" Text='<%# Eval("ProdName1") %>' meta:resourcekey="QnameResource1"></asp:Label>
                                                                <asp:Label ID="Label22" runat="server" Text='<%# Eval("MYPRODID") %>' Visible="False" meta:resourcekey="Label22Resource1"></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lblspn" runat="server" Text='<%# Eval("QTYINHAND") %>' meta:resourcekey="lblspnResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblspno" runat="server" Text='<%# Eval("Brand") %>' meta:resourcekey="lblspnoResource1"></asp:Label>
                                                            </td>

                                                            <td class="center">
                                                                <asp:LinkButton ID="lnkbtnEdateMProduct" runat="server" CommandArgument='<%# Eval("MYPRODID")  %>' CommandName="btnedit">
                                                                    <%--OnClick="lnkbtnEdateMProduct_Click"--%>
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="ECOMM/images/editRec.png" />
                                                                </asp:LinkButton></td>
                                                            <td class="center">
                                                                <asp:LinkButton ID="lnkbtnDeleteMProduct" runat="server" CommandArgument='<%# Eval("MYPRODID") %>'
                                                                    CommandName="btnDelet" OnClientClick="return confirm('Do you want to delete product?')">
                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="ECOMM/images/deleteRec.png" />
                                                                </asp:LinkButton></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>


                                        <%-- </ContentTemplate>
                                        <Triggers>
                                            <%--<asp:PostBackTrigger ControlID="" />
                                        </Triggers>
                                    </asp:UpdatePanel>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            // initiate layout and plugins
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Demo.init(); // init demo features
            FormValidation.init();
            TableAdvanced.init();
        });
    </script>
</asp:Content>