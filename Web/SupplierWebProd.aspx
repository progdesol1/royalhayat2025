<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="SupplierWebProd.aspx.cs" Inherits="Web.ECOMM.SupplierWebProd" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%--<%@ Register Assembly="obout_ColorPicker" Namespace="OboutInc.ColorPicker" TagPrefix="obout" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function targetMeBlank() {
            document.getElementById["ContentPlaceHolder1_btnpreview123"].target = "_blank";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="ECOMM/PrductJs/Productjs.js"></script>
    <div class="page-content">
        <div class="page-head">
            <ol class="breadcrumb">
                <li><a href="#">
                    <asp:Label ID="lblDisPro" runat="server" Text="Product"></asp:Label></a><i class="fa fa-circle"></i></li>
                <li><a href="#">
                    <asp:Label ID="lblDisProMst" runat="server" Text="Product Master"></asp:Label></a></li>
                <%-- <div class="pull-right" style="margin-top: -9px;">
                    <img id="GridviewDisplay" src="ECOMM/images/grid-view.png" runat="server" alt="GridviewDisplay" class="lblDisPro2" onclick="x('grd');" />
                    <img id="ListviewDisplay" src="ECOMM/images/list-view.png" runat="server" alt="ListviewDisplay" class="lblDisPro2" onclick="showhidepanel('lst');" />
                    <img id="FormDisplay" src="ECOMM/images/form-view.png" runat="server" alt="FormDisplay" class="lblDisPro2" onclick="showhidepanel('frm');" />
                </div>--%>
            </ol>

        </div>

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
                <asp:Panel ID="LstView" runat="server" meta:resourcekey="LstViewResource1">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-horizontal form-row-seperated">
                                <div class="portlet box green-turquoise">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-gift"></i>Product Web List
                                        </div>
                                        <div class="tools">
                                            <a id="shlinkProductDetails" runat="server" href="javascript:;" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <a href="javascript:;" class="reload"></a>
                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                        <div class="actions btn-set">
                                            <asp:LinkButton ID="lbApproveIss" class="btn blue" runat="server" OnClick="lbApproveIss_Click">Advanced Search</asp:LinkButton>
                                            <asp:LinkButton ID="btnWebReload" class="btn blue" runat="server" OnClientClick="return ConfirmMsgPriveAll();" OnClick="btnWebReload_Click">Web Reload</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="portlet-body" id="shProductDetails" runat="server" style="padding-left: 25px;">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="pMasterGridview" EventName="ItemCommand" />
                                            </Triggers>
                                            <ContentTemplate>

                                                <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                    <thead class="repHeader">
                                                        <tr>
                                                            <th>
                                                                <asp:Label ID="lblSN" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="lblSName" runat="server" Text="BusProName" meta:resourcekey="lblSNameResource2"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="Label1" runat="server" Text="Product ID" meta:resourcekey="lblSNameResource2"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="lblProName" runat="server" Text="PRODUCT NAME" meta:resourcekey="lblProNameResource1"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="lblProOname" runat="server" Text="Short Name" meta:resourcekey="lblProOnameResource1"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="lblProO2name" runat="server" Text="Bar Code" meta:resourcekey="lblProO2nameResource1"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="Label48" runat="server" Text="Qty"></asp:Label></th>
                                                            <th>
                                                                <asp:Label ID="lblcheactiv" runat="server" Text="Active"></asp:Label></th>
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
                                                                        <asp:Label ID="lblMytype" runat="server" Text='<%# Eval("MYTYPE") %>' meta:resourcekey="lblspnResource1"></asp:Label>
                                                                        <asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("MYID") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Qname" runat="server" Text='<%# Eval("MYPRODID") %>' meta:resourcekey="QnameResource1"></asp:Label>

                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lblspn" runat="server" Text='<%# getProdname(Convert .ToInt32 ( Eval("MYPRODID"))) %>' meta:resourcekey="lblspnResource1"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblspno" runat="server" Text='<%# getShortname(Convert .ToInt32 ( Eval("MYPRODID"))) %>' meta:resourcekey="lblspnoResource1"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblspnoo" runat="server" Text='<%# getbarcode (Convert.ToInt32 ( Eval("MYPRODID"))) %>' meta:resourcekey="lblspnooResource1"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="Label49" runat="server" Text='<%# getqty (Convert.ToInt32 ( Eval("MYPRODID"))) %>' meta:resourcekey="lblspnooResource1"></asp:Label></td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chbcheckActiv" runat="server" OnCheckedChanged="chbcheckActiv_CheckedChanged" AutoPostBack="true" />

                                                                    </td>
                                                                    <td class="center">
                                                                        <asp:LinkButton ID="lnkbtnEdateMProduct" runat="server" CommandArgument='<%# Eval("MYID")  %>' CommandName="btnedit">
                                                                            <asp:Image ID="Image1" runat="server" ImageUrl="ECOMM/images/editRec.png" />
                                                                        </asp:LinkButton></td>
                                                                    <td class="center">
                                                                        <asp:LinkButton ID="lnkbtnDeleteMProduct" runat="server" CommandArgument='<%# Eval("MYID")  %>' CommandName="btndelet"
                                                                            OnClientClick="return confirm('Do you want to delete product?')">
                                                                            <asp:Image ID="Image2" runat="server" ImageUrl="ECOMM/images/deleteRec.png" />
                                                                        </asp:LinkButton></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>


                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>


                <div class="row">
                    <div class="col-md-12">
                        <%--<asp:UpdatePanel ID="pnlprod" runat="server">
                            <ContentTemplate>--%>


                        <asp:Panel ID="FrmView" Visible="false" runat="server">
                            <div class="form-horizontal form-row-seperated">
                                <div class="portlet box yellow-crusta">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-gift"></i>Product Web Details &nbsp;<asp:Label ID="lblprodname" runat="server"></asp:Label>
                                        </div>
                                        <div class="tools">
                                            <a href="javascript:;" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <a href="javascript:;" class="reload"></a>
                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                        <div class="actions btn-set">
                                            <asp:LinkButton ID="btnProductMaster" class="btn blue" Style="background-color: #36d7b7; color: #fff" runat="server" OnClick="btnProductMaster_Click">Product Master</asp:LinkButton>
                                            <asp:LinkButton ID="btnPushOnWeb" class="btn blue" runat="server" OnClick="btnPushOnWeb_Click" OnClientClick="return ConfirmMsgPrive();">Push On Web</asp:LinkButton>

                                        </div>
                                    </div>

                                    <div class="portlet-body" style="padding-left: 25px;">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general">
                                                    <div class="form-body">
                                                        <asp:Panel runat="server" Visible="false" ID="prodid">
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">

                                                                    <asp:Label ID="lblFVPID" runat="server" Text="Product Id" meta:resourcekey="lblFVPIDResource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblmstproid" runat="server" meta:resourcekey="lblmstproidResource1"></asp:Label>
                                                                </div>

                                                            </div>

                                                        </asp:Panel>
                                                        <div class="portlet-body form">
                                                            <div class="portlet-body">
                                                                <div class="form-wizard">
                                                                    <div class="tabbable">
                                                                        <ul class="nav nav-pills nav-justified steps" style="padding-top: 0px; height: 70px; padding-bottom: 0px; margin-bottom: 0px; width: 100%">
                                                                            <%--<li class="active"><a style="color: #5b9bd1;" href="#pMaster" class="step" data-toggle="tab">
                                                                                        <span class="number">1 </span><span class="desc">
                                                                                            <asp:Label ID="lblBusinessCo" runat="server" Text="Product Master" meta:resourcekey="lblBusinessCoResource1"></asp:Label>
                                                                                        </span></a></li>--%>
                                                                            <li style="width: 20%">
                                                                                <asp:LinkButton ID="linktoprodMaster" Style="color: blue; padding: 0px; font-size: 40px; width: 89px;" class="step" runat="server" OnClick="linktoprodMaster_Click">
                                                                                    <span class="number"><span class="glyphicon glyphicon-chevron-left" style ="font-size: 25px; padding-left: 0px; padding-right: 0px; right: 8px;"> </span></span> </asp:LinkButton>
                                                                            </li>
                                                                            <li style="width: 345px;">
                                                                                <a id="A4" runat="server" alt="FormDisplay" style="color: #5b9bd1; padding: 0px;" onclick="showhidepanelWebProd('3Product');" class="step">
                                                                                    <%-- <asp:LinkButton ID="link3Product"  runat="server">--%>
                                                                                    <span class="number">3 </span><span class="desc">
                                                                                        <asp:Label ID="lblBusinessDetai" runat="server" Text="Product Details" meta:resourcekey="lblBusinessDetaiResource1"></asp:Label></span>
                                                                                    <%-- </asp:LinkButton>--%></a>
                                                                            </li>
                                                                            <li style="width: 40%">
                                                                                <a id="A5" runat="server" alt="FormDisplay" style="color: #5b9bd1; padding: 0px;" onclick="showhidepanelWebProd('4Webprod');" class="step">
                                                                                    <%-- <asp:LinkButton ID="link4Webprod"  runat="server">--%>
                                                                                    <span class="number">4</span><span class="desc">
                                                                                        <asp:Label ID="lblWebExistance" runat="server" Text="Web Product" meta:resourcekey="lblWebExistanceResource1"></asp:Label></span>
                                                                                    <%-- </asp:LinkButton>--%></a>
                                                                            </li>
                                                                            <%-- <li>
                                                                                        <a style="color: #5b9bd1;" href="#pPriceNote" class="step" data-toggle="tab">
                                                                                            <span class="number">4 </span><span class="desc">
                                                                                                <asp:Label ID="Label1" runat="server" Text="Display Product" meta:resourcekey="lblWebExistanceResource1"></asp:Label></span></a>&nbsp;
                                                                                    </li>--%>
                                                                        </ul>



                                                                        <div class="tab-content no-space">
                                                                            <asp:Panel ID="panelpdetails" runat="server" Style="display: block">
                                                                                <div id="pDetails" class="tab-pane active  ">
                                                                                    <div class="tabbable">
                                                                                        <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                    <ContentTemplate>--%>
                                                                                        <ul class="nav nav-tabs">

                                                                                            <li class="active">
                                                                                                <%-- <asp:LinkButton ID="linkProduct"   runat="server"></asp:LinkButton>--%>
                                                                                                <a id="A3" runat="server" alt="FormDisplay" class="lblDisPro2" style="color: #fff; background-color: #f3c200" onclick="showhidepanelWebProd('Product');">PRODUCT DETAILS</a>
                                                                                            </li>
                                                                                            <li>
                                                                                                <%-- <asp:LinkButton ID="linkWebproduct"  runat="server"></asp:LinkButton>--%>
                                                                                                <a id="A2" runat="server" alt="FormDisplay" class="lblDisPro2" style="color: #fff; background-color: #26a69a" onclick="showhidepanelWebProd('Webproduct');">WEB PRODUCT</a>
                                                                                            </li>

                                                                                        </ul>
                                                                                        <div class="tab-content no-space">
                                                                                            <asp:Panel ID="panelProduct" runat="server" Style="display: block">

                                                                                                <div id="ac3-21" class="tab-pane active">

                                                                                                    <div class="panel-body">

                                                                                                        <div class="tab-container">
                                                                                                            <ul class="nav nav-tabs">
                                                                                                                <li class="active"><a href="#pOverview" data-toggle="tab" style="color: black !important;">
                                                                                                                    <asp:Label ID="lblPDOV" runat="server" Text="Overview" meta:resourcekey="lblPDOVResource1"></asp:Label></a></li>
                                                                                                                <li><a href="#pFeatures" data-toggle="tab" style="color: black !important;">
                                                                                                                    <asp:Label ID="lblPDFet" runat="server" Text="Features" meta:resourcekey="lblPDFetResource1"></asp:Label></a></li>
                                                                                                                <li><a href="#pSpecification" data-toggle="tab" style="color: black !important;">
                                                                                                                    <asp:Label ID="lblPDS" runat="server" Text="Specification" meta:resourcekey="lblPDSResource1"></asp:Label></a></li>
                                                                                                                <li><a href="#pFAQDownload" data-toggle="tab" style="color: black !important;">
                                                                                                                    <asp:Label ID="lblPDFD" runat="server" Text="FAQ Download" meta:resourcekey="lblPDFDResource1"></asp:Label></a></li>
                                                                                                            </ul>
                                                                                                            <div class="tab-content">
                                                                                                                <div class="tab-pane active cont" aria-atomic="True" aria-expanded="true" id="pOverview" style="width: auto;">

                                                                                                                    <div class="portlet box yellow-crusta">
                                                                                                                        <div class="portlet-title">
                                                                                                                            <div class="caption">
                                                                                                                                <i class="fa fa-gift"></i>Overview
                                                                                                                            </div>
                                                                                                                            <div class="tools">
                                                                                                                                <a href="javascript:;" class="collapse"></a>
                                                                                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                                                                <a href="javascript:;" class="reload"></a>
                                                                                                                                <a href="javascript:;" class="remove"></a>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="portlet-body form" style="padding-left: 10px;">
                                                                                                                            <div class="tabbable">
                                                                                                                                <div class="tab-content no-space">
                                                                                                                                    <div class="form-horizontal" id="form_sample_3">
                                                                                                                                        <div class="form-body">
                                                                                                                                            <div class="form-group last">
                                                                                                                                                <div class="col-md-12">
                                                                                                                                                    <asp:TextBox ID="txtoverviewediter" style="width: 840px;" runat="server" TextMode="MultiLine" class="ckeditor form-control" name="editor2" Rows="6" data-error-container="#editor2_error"></asp:TextBox>

                                                                                                                                                    <div id="editor2_error">
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
                                                                                                                <div class="tab-pane" aria-atomic="True" aria-expanded="true" id="pFeatures" style="width: auto;">
                                                                                                                    <%-- <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                                                                                        <ContentTemplate>--%>
                                                                                                                    <%--<h4 class="CKEditorHeader">
                                                                                                    <asp:Label ID="lblPDFea" runat="server" Text="Features" meta:resourcekey="lblPDFeaResource1"></asp:Label></h4>--%>
                                                                                                                    <div class="portlet box yellow-crusta">
                                                                                                                        <div class="portlet-title">
                                                                                                                            <div class="caption">
                                                                                                                                <i class="fa fa-gift"></i>Features
                                                                                                                            </div>
                                                                                                                            <div class="tools">
                                                                                                                                <a href="javascript:;" class="collapse"></a>
                                                                                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                                                                <a href="javascript:;" class="reload"></a>
                                                                                                                                <a href="javascript:;" class="remove"></a>
                                                                                                                            </div>
                                                                                                                            <div class="actions btn-set">

                                                                                                                                <asp:Button ID="btnfeadeskart" runat="server" CssClass="btn btn-default" Text="Discard" OnClick="btnfeadeskart_Click" />
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="portlet-body form" style="padding-left: 10px;">
                                                                                                                            <div class="tabbable">
                                                                                                                                <div class="tab-content no-space">
                                                                                                                                    <div class="form-horizontal" id="form_sample_3">
                                                                                                                                        <div class="form-body">
                                                                                                                                            <div class="form-group last">
                                                                                                                                                <div class="col-md-12">
                                                                                                                                                    <asp:TextBox ID="txtfutereditre" runat="server"  style="width: 840px;" TextMode="MultiLine" class="ckeditor form-control" name="editor2" Rows="6" data-error-container="#editor2_error"></asp:TextBox>

                                                                                                                                                    <div id="editor2_error">
                                                                                                                                                    </div>
                                                                                                                                                </div>
                                                                                                                                            </div>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                    <div class="row">
                                                                                                                                        <div class="col-md-12">
                                                                                                                                            <div class="form-horizontal form-row-seperated">


                                                                                                                                                <div class="portlet-body" style="padding-left: 25px;">
                                                                                                                                                    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                                                                                            <ContentTemplate>--%>

                                                                                                                                                    <table class="table table-striped table-bordered table-hover" id="sample_7">
                                                                                                                                                        <thead class="repHeader">
                                                                                                                                                            <tr>
                                                                                                                                                                <th>
                                                                                                                                                                    <asp:Label ID="Label4" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>

                                                                                                                                                                <th>
                                                                                                                                                                    <asp:Label ID="Label6" runat="server" Text="Feature Name" meta:resourcekey="lblProNameResource1"></asp:Label></th>

                                                                                                                                                                <th>
                                                                                                                                                                    <asp:Label ID="Label7" runat="server" Text="Feature Image" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                <th>
                                                                                                                                                                    <asp:Label ID="Label15" runat="server" Text="Feature Type" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                <th>
                                                                                                                                                                    <asp:Label ID="Label20" runat="server" Text="Save" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                            </tr>
                                                                                                                                                        </thead>
                                                                                                                                                        <tbody>
                                                                                                                                                            <%--<asp:Repeater ID="Repeater2" runat="server" OnItemCommand ="Repeater2_ItemCommand">
                                                                                                                                                                    <ItemTemplate>--%>
                                                                                                                                                            <tr class="gradeA">
                                                                                                                                                                <td>
                                                                                                                                                                    <asp:Label ID="lblSRNO" runat="server" Text="1" meta:resourcekey="lblSRNOResource1"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                                <td>
                                                                                                                                                                    <asp:DropDownList ID="drpfuchtertype" AutoPostBack="true" OnSelectedIndexChanged="drpfuchtertype_SelectedIndexChanged" runat="server" CssClass="form-control select2me">
                                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                                </td>
                                                                                                                                                                <td>
                                                                                                                                                                    <asp:Image ID="fucherimg1" runat="server" Style="width: 30px;" />
                                                                                                                                                                </td>
                                                                                                                                                                <td>
                                                                                                                                                                    <asp:Label ID="lblfutertype1" runat="server"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                                <td>
                                                                                                                                                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Style="padding-top: 7px; padding-bottom: 7px" meta:resourcekey="btnSubmitSearchResource1" Text="Save" OnClick="btnfeatures_Click" /></td>
                                                                                                                                                            </tr>
                                                                                                                                                            <%-- <tr class="gradeA">
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:Label ID="Label2" runat="server" Text="2" meta:resourcekey="lblSRNOResource1"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:DropDownList ID="drpfuchtertype2" AutoPostBack ="true" OnSelectedIndexChanged ="drpfuchtertype2_SelectedIndexChanged"  runat="server" CssClass="form-control select2me">
                                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:Image ID="fucherimg2" runat="server" Style="width: 30px;" />
                                                                                                                                                                    </td>
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:Label ID="lblfutertype2" runat="server" ></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>
                                                                                                                                                                <tr class="gradeA">
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:Label ID="Label3" runat="server"  Text="3" meta:resourcekey="lblSRNOResource1"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:DropDownList ID="drpfuchtertype3" AutoPostBack ="true" OnSelectedIndexChanged ="drpfuchtertype3_SelectedIndexChanged"  runat="server" CssClass="form-control select2me">
                                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:Image ID="fucherimg3" runat="server"  Style="width: 30px;"/>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:Label ID="lblfutertype3" runat="server" ></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                </tr>--%>
                                                                                                                                                            <%--</ItemTemplate>
                                                                                                                                                                </asp:Repeater>--%>
                                                                                                                                                        </tbody>
                                                                                                                                                    </table>
                                                                                                                                                    <table class="table table-striped table-bordered table-hover" id="sample_7">
                                                                                                                                                        <thead class="repHeader">
                                                                                                                                                            <tr>
                                                                                                                                                                <th>
                                                                                                                                                                    <asp:Label ID="Label2" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>

                                                                                                                                                                <th>
                                                                                                                                                                    <asp:Label ID="Label3" runat="server" Text="Feature Name" meta:resourcekey="lblProNameResource1"></asp:Label></th>

                                                                                                                                                                <th>
                                                                                                                                                                    <asp:Label ID="Label16" runat="server" Text="Feature Image" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                <th>
                                                                                                                                                                    <asp:Label ID="Label17" runat="server" Text="Feature Type" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                <th>
                                                                                                                                                                    <asp:Label ID="Label18" runat="server" Text="Edit" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                <th>
                                                                                                                                                                    <asp:Label ID="Label19" runat="server" Text="Deleted" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                            </tr>
                                                                                                                                                        </thead>
                                                                                                                                                        <tbody>
                                                                                                                                                            <asp:Repeater ID="listprodfucter" runat="server" OnItemCommand="listprodfucter_ItemCommand">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <tr class="gradeA">
                                                                                                                                                                        <td>
                                                                                                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString() %>' meta:resourcekey="lblSRNOResource1"></asp:Label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td>
                                                                                                                                                                            <asp:Label ID="lblFucter" runat="server" Text='<%# getfuctername(Convert .ToInt32 ( Eval("FeatureName"))) %>'></asp:Label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td>
                                                                                                                                                                            <img src="<%#  Eval("FeatureImage")%>" style="width: 30px; height: 30px;">
                                                                                                                                                                            <%-- // <asp:Label ID="lblImg" runat="server" Text='<%# Eval("FeatureImage") %>'></asp:Label>--%>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td>
                                                                                                                                                                            <asp:Label ID="lblfutertype1" runat="server" Text='<%# Eval("FeatureType") %>'></asp:Label>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="center">
                                                                                                                                                                            <asp:LinkButton ID="btnedit" runat="server" CommandName="btnedit" CommandArgument='<%# Eval("MyID") %>' CausesValidation="false">
                                                                                                                                                                                <asp:Image ID="Image1" runat="server" ImageUrl="ECOMM/images/editRec.png" />
                                                                                                                                                                            </asp:LinkButton>
                                                                                                                                                                        </td>
                                                                                                                                                                        <td class="center">
                                                                                                                                                                            <asp:LinkButton ID="btndelet" runat="server" CommandName="btndelet" CommandArgument='<%#Eval("MyID")  %>' OnClientClick="return confirm('Do you want to delete Fucter ?')">
                                                                                                                                                                                <asp:Image ID="Image2" runat="server" ImageUrl="ECOMM/images/deleteRec.png" />
                                                                                                                                                                            </asp:LinkButton>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:Repeater>
                                                                                                                                                        </tbody>
                                                                                                                                                    </table>
                                                                                                                                                    <%-- </ContentTemplate>
                                                                                                                                                                        </asp:UpdatePanel>--%>
                                                                                                                                                </div>
                                                                                                                                            </div>

                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>

                                                                                                                <div class="tab-pane" aria-atomic="True" aria-expanded="true" id="pSpecification" style="width: auto;">
                                                                                                                    <div class="portlet box yellow-crusta">
                                                                                                                        <div class="portlet-title">
                                                                                                                            <div class="caption">
                                                                                                                                <i class="fa fa-gift"></i>Specification
                                                                                                                            </div>
                                                                                                                            <div class="tools">
                                                                                                                                <a href="javascript:;" class="collapse"></a>
                                                                                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                                                                <a href="javascript:;" class="reload"></a>
                                                                                                                                <a href="javascript:;" class="remove"></a>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="portlet-body form" style="padding-left: 10px;">
                                                                                                                            <div class="tabbable">
                                                                                                                                <div class="tab-content no-space">
                                                                                                                                    <div class="form-horizontal" id="form_sample_3">
                                                                                                                                        <div class="form-body">
                                                                                                                                            <div class="form-group last">
                                                                                                                                                <div class="col-md-12">
                                                                                                                                                    <asp:TextBox ID="txtspecificaediter"  style="width: 840px;" runat="server" TextMode="MultiLine" class="ckeditor form-control" name="editor2" Rows="6" data-error-container="#editor2_error"></asp:TextBox>

                                                                                                                                                    <div id="editor2_error">
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
                                                                                                                <div class="tab-pane" aria-atomic="True" aria-expanded="true" id="pFAQDownload" style="width: auto;">
                                                                                                                    <div class="portlet box yellow-crusta">
                                                                                                                        <div class="portlet-title">
                                                                                                                            <div class="caption">
                                                                                                                                <i class="fa fa-gift"></i>FAQ Download
                                                                                                                            </div>
                                                                                                                            <div class="tools">
                                                                                                                                <a href="javascript:;" class="collapse"></a>
                                                                                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                                                                <a href="javascript:;" class="reload"></a>
                                                                                                                                <a href="javascript:;" class="remove"></a>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="portlet-body form" style="padding-left: 10px;">
                                                                                                                            <div class="tabbable">
                                                                                                                                <div class="tab-content no-space">
                                                                                                                                    <div class="form-horizontal" id="form_sample_3">
                                                                                                                                        <div class="form-body">
                                                                                                                                            <div class="form-group last">
                                                                                                                                                <div class="col-md-12">
                                                                                                                                                    <asp:TextBox ID="txtfaqediter"  style="width: 840px;" runat="server" TextMode="MultiLine" class="ckeditor form-control" name="editor2" Rows="6" data-error-container="#editor2_error"></asp:TextBox>

                                                                                                                                                    <div id="editor2_error">
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
                                                                                                            </div>
                                                                                                        </div>

                                                                                                    </div>
                                                                                                </div>

                                                                                            </asp:Panel>
                                                                                            <asp:Panel ID="panelwebprod123" runat="server" Style="display: none">
                                                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <div>
                                                                                                            <div class="portlet box green">
                                                                                                                <div class="portlet-title">
                                                                                                                    <div class="caption">
                                                                                                                        <i class="fa fa-gift"></i>WEB PRODUCT
                                                                                                                    </div>
                                                                                                                    <%-- <div class="tools">
                                                                                                                        <a href="javascript:;" class="collapse"></a>
                                                                                                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                                                        <a href="javascript:;" class="reload"></a>
                                                                                                                        <a href="javascript:;" class="remove"></a>
                                                                                                                    </div>--%>
                                                                                                                    <div class="actions btn-set">
                                                                                                                        <asp:Button ID="btnWebProduct" runat="server" OnClick="btnWebProduct_Click" CssClass="btn yellow-crusta" Text="Save Web" Height="34px" />
                                                                                                                        <asp:Button ID="btnAddwebProd" runat="server" OnClick="btnAddwebProd_Click" CssClass="btn btn-primary" Style="padding-top: 7px; background-color: red; color: #fff; padding-bottom: 7px" Text="Add New" Height="34px" />

                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <div class="portlet-body" style="padding-left: 25px;">
                                                                                                                    <div class="tabbable">
                                                                                                                        <div class="tab-content no-space">
                                                                                                                            <div class="tab-pane active" id="tab_general1">
                                                                                                                                <div class="form-body">

                                                                                                                                    <div class="row">
                                                                                                                                        <div class="form-group">
                                                                                                                                            <div class="col-md-4">
                                                                                                                                                <asp:Label ID="lblWebBusPro" runat="server" Text="Business Product"></asp:Label>
                                                                                                                                                <asp:DropDownList ID="ddlWebBusPro" runat="server" Enabled="false" CssClass="form-control select2me" Height="30px">
                                                                                                                                                </asp:DropDownList>
                                                                                                                                            </div>
                                                                                                                                            <div class="col-md-4">
                                                                                                                                                <asp:Label ID="lblinterval" runat="server" Text="Interval in"></asp:Label>
                                                                                                                                                <asp:DropDownList ID="ddlwebinterval" runat="server" CssClass="form-control select2me" Height="30px">
                                                                                                                                                </asp:DropDownList>
                                                                                                                                            </div>
                                                                                                                                            <div class="col-md-4">
                                                                                                                                                <asp:Label ID="lblfromdate" runat="server" Text="From Date"></asp:Label>
                                                                                                                                                <asp:TextBox ID="txtwebfromdt" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="45" meta:resourcekey="txtwebfromdtResource1"></asp:TextBox>
                                                                                                                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtwebfromdt" Format="yyyy/MM/dd" Enabled="True">
                                                                                                                                                </cc1:CalendarExtender>
                                                                                                                                            </div>
                                                                                                                                        </div>
                                                                                                                                        <div class="form-group">
                                                                                                                                            <h5></h5>
                                                                                                                                            <div class="col-md-4">
                                                                                                                                                <h5></h5>
                                                                                                                                                <asp:Label ID="lbltodate" runat="server" Text="To Date"></asp:Label>
                                                                                                                                                <asp:TextBox ID="txtwebtomdt" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="45"></asp:TextBox>
                                                                                                                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtwebtomdt" Format="yyyy/MM/dd" Enabled="True">
                                                                                                                                                </cc1:CalendarExtender>
                                                                                                                                            </div>
                                                                                                                                            <div class="col-md-4">
                                                                                                                                                <h5></h5>
                                                                                                                                                <asp:Label ID="lbltilldate" runat="server" Text="Till The Date"></asp:Label>
                                                                                                                                                <asp:TextBox ID="txtwebtillmdt" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="45" meta:resourcekey="txtwebtillmdtResource1"></asp:TextBox>
                                                                                                                                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtwebtillmdt" Format="yyyy/MM/dd" Enabled="True">
                                                                                                                                                </cc1:CalendarExtender>
                                                                                                                                            </div>
                                                                                                                                            <div class="col-md-3">
                                                                                                                                                <h5></h5>
                                                                                                                                                <asp:Label ID="lbldisplay" runat="server" Text="Displayed on Web"></asp:Label>
                                                                                                                                                <asp:TextBox ID="txtwebDisponWeb" runat="server" Enabled="False" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="45" meta:resourcekey="txtwebDisponWebResource1"></asp:TextBox>
                                                                                                                                            </div>
                                                                                                                                            <div class="col-md-1">
                                                                                                                                                <h5></h5>
                                                                                                                                                <asp:Label ID="Label35" runat="server" Visible="false" Text="Displayed on Web"></asp:Label>
                                                                                                                                                <asp:Image ID="Image3" runat="server" ImageUrl="ECOMM/images/plus.png" OnClick="javascript:showpanepanewebline();" Style="float: right" />
                                                                                                                                            </div>
                                                                                                                                        </div>
                                                                                                                                        <asp:Panel ID="panewebline" runat="server" Style="display: none" meta:resourcekey="panebusslineResource1">
                                                                                                                                            <fieldset style="margin-bottom: 10px;">
                                                                                                                                                <legend>Dispaly On Site By WebProd</legend>
                                                                                                                                                <div class="row" style="margin-right: 0px;">
                                                                                                                                                    <div class="form-group">
                                                                                                                                                        <div class="col-md-6">
                                                                                                                                                            <asp:Label runat="server" ID="Label8" class="col-md-4 control-label" Text="Placeholder Line"></asp:Label>
                                                                                                                                                            <div class="col-md-8">
                                                                                                                                                                <asp:TextBox ID="txtwebPlal" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpplResource1"></asp:TextBox>
                                                                                                                                                            </div>
                                                                                                                                                        </div>
                                                                                                                                                        <div class="col-md-6">
                                                                                                                                                            <asp:Label runat="server" ID="Label31" class="col-md-4 control-label" Text="Placeholder Ali R L"></asp:Label>
                                                                                                                                                            <div class="col-md-8">
                                                                                                                                                                <asp:TextBox ID="txtwebparl" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbprlResource1"></asp:TextBox>
                                                                                                                                                            </div>

                                                                                                                                                        </div>
                                                                                                                                                    </div>


                                                                                                                                                    <div class="form-group">
                                                                                                                                                        <div class="col-md-6">
                                                                                                                                                            <asp:Label runat="server" ID="Label32" class="col-md-4 control-label" Text="Placeholder Column"></asp:Label>
                                                                                                                                                            <div class="col-md-8">
                                                                                                                                                                <asp:TextBox ID="txtwebplco" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtpbcolResource1"></asp:TextBox>
                                                                                                                                                            </div>

                                                                                                                                                        </div>
                                                                                                                                                        <div class="col-md-6">
                                                                                                                                                            <asp:Label runat="server" ID="Label33" class="col-md-4 control-label" Text="Link 2Direct"></asp:Label>
                                                                                                                                                            <div class="col-md-8">
                                                                                                                                                                <asp:TextBox ID="txtweblink2d" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpl2dResource1"></asp:TextBox>
                                                                                                                                                            </div>

                                                                                                                                                        </div>
                                                                                                                                                    </div>

                                                                                                                                                    <div class="form-group">
                                                                                                                                                        <div class="col-md-6">
                                                                                                                                                            <asp:Label runat="server" ID="Label34" class="col-md-4 control-label" Text="Sort Number"></asp:Label>
                                                                                                                                                            <div class="col-md-8">
                                                                                                                                                                <asp:TextBox ID="txtwebsortn" Text="0" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpsnoResource1"></asp:TextBox>
                                                                                                                                                            </div>

                                                                                                                                                        </div>

                                                                                                                                                    </div>

                                                                                                                                                </div>
                                                                                                                                            </fieldset>
                                                                                                                                        </asp:Panel>
                                                                                                                                        <div class="row">
                                                                                                                                            <div class="col-md-12">
                                                                                                                                                <div class="form-horizontal form-row-seperated">
                                                                                                                                                    <div class="portlet box green-turquoise">
                                                                                                                                                        <div class="portlet-title">
                                                                                                                                                            <div class="caption">
                                                                                                                                                                <i class="fa fa-gift"></i>OFFER PRODUCT LIST
                                                                                                                                                            </div>
                                                                                                                                                            <%--<div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>--%>
                                                                                                                                                        </div>
                                                                                                                                                        <div class="portlet-body" style="padding-left: 25px;">
                                                                                                                                                            <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                                                                                                                    <ContentTemplate>--%>

                                                                                                                                                            <table class="table table-striped table-bordered table-hover" id="sample_3">
                                                                                                                                                                <thead class="repHeader">
                                                                                                                                                                    <tr>
                                                                                                                                                                        <th>
                                                                                                                                                                            <asp:Label ID="Label55" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                                                                                                        <th>
                                                                                                                                                                            <asp:Label ID="Label56" runat="server" Text="Web Product" meta:resourcekey="lblSNameResource2"></asp:Label></th>
                                                                                                                                                                        <th>
                                                                                                                                                                            <asp:Label ID="Label57" runat="server" Text="Interval" meta:resourcekey="lblProNameResource1"></asp:Label></th>

                                                                                                                                                                        <th>
                                                                                                                                                                            <asp:Label ID="Label58" runat="server" Text="From Date" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                        <th>
                                                                                                                                                                            <asp:Label ID="Label59" runat="server" Text="Till Date" meta:resourcekey="lblDelResource1"></asp:Label></th>
                                                                                                                                                                        <th>
                                                                                                                                                                            <asp:Label ID="Label60" runat="server" Text="Edit" meta:resourcekey="lblDelResource1"></asp:Label></th>
                                                                                                                                                                        <th>
                                                                                                                                                                            <asp:Label ID="Label61" runat="server" Text="Delete" meta:resourcekey="lblDelResource1"></asp:Label></th>
                                                                                                                                                                    </tr>
                                                                                                                                                                </thead>
                                                                                                                                                                <tbody>
                                                                                                                                                                    <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="Repeater2_ItemCommand">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <tr class="gradeA">
                                                                                                                                                                                <td>
                                                                                                                                                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString() %>' meta:resourcekey="lblSRNOResource1"></asp:Label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td>
                                                                                                                                                                                    <asp:Label ID="Qname" runat="server" Text='<%# getWebProduct(Convert .ToInt32 ( Eval("MYPRODID"))) %>' meta:resourcekey="QnameResource1"></asp:Label>

                                                                                                                                                                                </td>

                                                                                                                                                                                <td>
                                                                                                                                                                                    <%-- <asp:Label ID="lblAns" runat="server" Visible="False" Text='<%# Eval("OfferInterIn") %>' meta:resourcekey="lblAnsResource4"></asp:Label>--%>
                                                                                                                                                                                    <asp:Label ID="lblspn" runat="server" Text='<%# Eval("INTERVAL") %>' meta:resourcekey="lblspnResource1"></asp:Label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td>
                                                                                                                                                                                    <asp:Label ID="Label40" runat="server" Text='<%# Eval("FROMDATE") %>' meta:resourcekey="lblspnResource1"></asp:Label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td>
                                                                                                                                                                                    <asp:Label ID="Label42" runat="server" Text='<%# Eval("TILLDATE") %>' meta:resourcekey="lblspnResource1"></asp:Label>
                                                                                                                                                                                </td>
                                                                                                                                                                                <td class="center">
                                                                                                                                                                                    <asp:LinkButton ID="lnkbtnOff" runat="server" CommandArgument='<%# Eval("WEBID") %>' CommandName="btneditWeb">
                                                                                                                                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="ECOMM/images/editRec.png" />
                                                                                                                                                                                    </asp:LinkButton></td>
                                                                                                                                                                                <td class="center">
                                                                                                                                                                                    <asp:LinkButton ID="lnkbtndelete" runat="server" CommandArgument='<%# Eval("WEBID") %>' CommandName="DeleteWeb" OnClientClick="return confirm('Do you want to delete offer product?')">
                                                                                                                                                                                        <asp:Image ID="Image2" runat="server" ImageUrl="ECOMM/images/deleteRec.png" />
                                                                                                                                                                                    </asp:LinkButton></td>
                                                                                                                                                                            </tr>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:Repeater>
                                                                                                                                                                </tbody>
                                                                                                                                                            </table>


                                                                                                                                                            <%--  </ContentTemplate>
                                                                                                                                                                                </asp:UpdatePanel>--%>
                                                                                                                                                        </div>
                                                                                                                                                    </div>
                                                                                                                                                </div>
                                                                                                                                            </div>
                                                                                                                                        </div>
                                                                                                                                        <%--<div class="col-md-12">
                                                                                                                                <div class="col-md-4">
                                                                                                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Admin/ECOMM/images/plus.png" Style="float: right" OnClick="javascript:showpanepanewebline();" ToolTip="Add More Item" meta:resourcekey="Image3Resource1" />
                                                                                                                                </div>
                                                                                                                            </div>--%>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>

                                                                                                        </div>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </asp:Panel>
                                                                                        </div>
                                                                                        <%-- </ContentTemplate>
                                                                                                </asp:UpdatePanel>--%>
                                                                                    </div>
                                                                                </div>
                                                                            </asp:Panel>
                                                                            <asp:Panel ID="panelpWebsoft" runat="server" Style="display: none">
                                                                                <div id="pWebSoft" class="tab-pane">
                                                                                    <div class="tabbable">
                                                                                        <ul class="nav nav-tabs">
                                                                                            <li class="active">
                                                                                                <%--<asp:LinkButton ID="linkImagges"    runat="server"></asp:LinkButton>--%>
                                                                                                <a id="A1" runat="server" alt="FormDisplay" class="lblDisPro2" onclick="showhidepanelWebProd('Imagges');" style="color: #fff; background-color: #26a69a">IMAGES</a>

                                                                                            </li>
                                                                                            <li>
                                                                                                <%-- <asp:LinkButton ID="linkOffer"  onclick="showhidepanelWebProd('Offer');" runat="server">OFFER PRODUCT</asp:LinkButton>--%>
                                                                                                <a id="FormDisplay" runat="server" alt="FormDisplay" class="lblDisPro2" onclick="showhidepanelWebProd('Offer');" style="color: #fff; background-color: #8e5fa2">OFFER PRODUCT</a>
                                                                                            </li>
                                                                                        </ul>
                                                                                        <div class="tab-content no-space">

                                                                                            <asp:Panel ID="panelimag" runat="server" Style="display: none">
                                                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
                                                                                                    <ContentTemplate>
                                                                                                        <div id="pnlMainImag" class="tab-pane">
                                                                                                            <div class="tabbable">
                                                                                                                <ul class="nav nav-tabs">
                                                                                                                    <li class="active">
                                                                                                                        <%--<asp:LinkButton ID="linkImagges"    runat="server"></asp:LinkButton>--%>
                                                                                                                        <a id="A10" runat="server" alt="FormDisplay" class="lblDisPro2" onclick="ShowImag('ImaMain');" style="color: #fff; background-color: #26a69a">Main IMAGES</a>

                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <%-- <asp:LinkButton ID="linkOffer"  onclick="showhidepanelWebProd('Offer');" runat="server">OFFER PRODUCT</asp:LinkButton>--%>
                                                                                                                        <a id="A11" runat="server" alt="FormDisplay" class="lblDisPro2" onclick="ShowImag('ImgAll');" style="color: #fff; background-color: #8e5fa2">All IMAGES</a>
                                                                                                                    </li>
                                                                                                                </ul>
                                                                                                                <div class="tab-content no-space">
                                                                                                                    <asp:Panel ID="pnelMainImag" runat="server" Style="display: none">
                                                                                                                        <div id="ac3-140" class="tab-pane active ">
                                                                                                                            <div class="row">
                                                                                                                                <div class="col-md-12">
                                                                                                                                    <div class="form-horizontal form-row-seperated">
                                                                                                                                        <div class="portlet box green">

                                                                                                                                            <div class="portlet-title">
                                                                                                                                                <div class="caption">
                                                                                                                                                    <i class="fa fa-gift"></i>Main IMAGES
                                                                                                                                                </div>
                                                                                                                                            </div>
                                                                                                                                            <div class="portlet-body" style="padding-left: 10px;">
                                                                                                                                                <div class="tabbable">
                                                                                                                                                    <div class="tab-content no-space">
                                                                                                                                                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                                                                                                                            <div class="alert alert-danger alert-dismissable">
                                                                                                                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                                                                                                                <asp:Label ID="Label62" runat="server"></asp:Label>
                                                                                                                                                            </div>
                                                                                                                                                        </asp:Panel>
                                                                                                                                                        <table class="table table-striped table-bordered table-hover">
                                                                                                                                                            <thead class="repHeader">
                                                                                                                                                                <tr>
                                                                                                                                                                   
                                                                                                                                                                    <th style="width: 20%;">
                                                                                                                                                                        <asp:Label ID="Label67" runat="server" Text="File" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                    <th style="width: 10%;">
                                                                                                                                                                        <asp:Label ID="Label68" runat="server" Text="Add" meta:resourcekey="lblEditResource1"></asp:Label></th>

                                                                                                                                                                </tr>
                                                                                                                                                            </thead>
                                                                                                                                                            <tbody>
                                                                                                                                                                <tr class="gradeA">
                                                                                                                                                                   
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:FileUpload ID="FileUpload2"  runat="server"/>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:Button ID="btnAddMainImg"  runat="server" OnClick ="btnAddMainImg_Click"  CssClass="btn green-haze"  Style="padding-top: 7px; background-color: #3598dc; color: #fff; padding-bottom: 7px" Text="Add" />

                                                                                                                                                                    </td>
                                                                                                                                                                </tr>

                                                                                                                                                            </tbody>
                                                                                                                                                        </table>

                                                                                                                                                        <table class="table table-striped table-bordered table-hover" id="sample_7">
                                                                                                                                                            <thead class="repHeader">
                                                                                                                                                                <tr>
                                                                                                                                                                    <th style="width: 5%;">
                                                                                                                                                                        <asp:Label ID="Label70" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>

                                                                                                                                                                    <th style="width: 15%;">
                                                                                                                                                                        <asp:Label ID="Label71" runat="server" Text="Type of the Image " meta:resourcekey="lblProNameResource1"></asp:Label></th>

                                                                                                                                                                    <th style="width: 20%;">
                                                                                                                                                                        <asp:Label ID="Label72" runat="server" Text="Icon" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                    <th style="width: 20%;">
                                                                                                                                                                        <asp:Label ID="Label73" runat="server" Text="File" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                   
                                                                                                                                                                    <th style="width: 10%;">
                                                                                                                                                                        <asp:Label ID="Label77" runat="server" Text="Deleted" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                </tr>
                                                                                                                                                            </thead>
                                                                                                                                                            <tbody>
                                                                                                                                                                <asp:Repeater ID="Repeater4" runat="server" OnItemCommand ="Repeater4_ItemCommand" >
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <tr class="gradeA">
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString() %>' meta:resourcekey="lblSRNOResource1"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblImagID" runat="server" Text="Main Imagge"></asp:Label>
                                                                                                                                                                                <asp:Label ID="lblProduct" runat="server" Visible="false" Text='<%# Eval("MYPRODID") %>'></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <img src="Upload/<%#Eval("PICTURE")%>" style="width: 30px; height: 30px;">
                                                                                                                                                                                <%-- <asp:Label ID="lblImg" runat="server" Text='<%# Eval("PICTURE") %>'></asp:Label>--%>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblImg" runat="server" Text='<%# Eval("PICTURE") %>'></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="center">
                                                                                                                                                                                <asp:LinkButton ID="btndelet" runat="server" CommandName="btnMainDetet" CommandArgument='<%#Eval("ID")  %>' OnClientClick="return confirm('Do you want to delete Image ?')">
                                                                                                                                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="ECOMM/images/deleteRec.png" />
                                                                                                                                                                                </asp:LinkButton>
                                                                                                                                                                            </td>
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

                                                                                                                        </div>
                                                                                                                    </asp:Panel>
                                                                                                                    <asp:Panel ID="panelAllImg" runat="server" Style="display: none">
                                                                                                                        <div id="ac3-31" class="tab-pane active ">
                                                                                                                            <div class="row">
                                                                                                                                <div class="col-md-12">
                                                                                                                                    <div class="form-horizontal form-row-seperated">
                                                                                                                                        <div class="portlet box purple">

                                                                                                                                            <div class="portlet-title">
                                                                                                                                                <div class="caption">
                                                                                                                                                    <i class="fa fa-gift"></i>All IMAGES
                                                                                                                                                </div>
                                                                                                                                                <div class="actions btn-set">
                                                                                                                                                    <asp:Button ID="btnImagUpload" runat="server" class="btn yellow-crusta" Style="width: 88px;" OnClick="btnImagUpload_Click" Text="Save" />
                                                                                                                                                </div>
                                                                                                                                            </div>



                                                                                                                                            <div class="portlet-body" style="padding-left: 10px;">
                                                                                                                                                <div class="tabbable">
                                                                                                                                                    <div class="tab-content no-space">
                                                                                                                                                        <asp:Panel ID="pnlSuccessMsg123" runat="server" Visible="false">
                                                                                                                                                            <div class="alert alert-danger alert-dismissable">
                                                                                                                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                                                                                                                <asp:Label ID="lblMsg123" runat="server"></asp:Label>
                                                                                                                                                            </div>
                                                                                                                                                        </asp:Panel>
                                                                                                                                                        <table class="table table-striped table-bordered table-hover">
                                                                                                                                                            <thead class="repHeader">
                                                                                                                                                                <tr>
                                                                                                                                                                    <th style="width: 5%;">
                                                                                                                                                                        <asp:Label ID="Label5" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>

                                                                                                                                                                    <th style="width: 20%;">
                                                                                                                                                                        <asp:Label ID="Label9" runat="server" Text="Type of the Image " meta:resourcekey="lblProNameResource1"></asp:Label></th>
                                                                                                                                                                    <th style="width: 22%;">
                                                                                                                                                                        <asp:Label ID="Label22" runat="server" Text="Color" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                    <th style="width: 23%;">
                                                                                                                                                                        <asp:Label ID="Label23" runat="server" Text="Size" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                    <th style="width: 20%;">
                                                                                                                                                                        <asp:Label ID="Label10" runat="server" Text="File" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                    <th style="width: 10%;">
                                                                                                                                                                        <asp:Label ID="Label12" runat="server" Text="Add" meta:resourcekey="lblEditResource1"></asp:Label></th>

                                                                                                                                                                </tr>
                                                                                                                                                            </thead>
                                                                                                                                                            <tbody>
                                                                                                                                                                <tr class="gradeA">
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:Label ID="Label11" runat="server" Text="1" meta:resourcekey="lblSRNOResource1"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:DropDownList ID="ddlImages" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlImages_SelectedIndexChanged" CssClass="form-control select2me" meta:resourcekey="ddlImagesResource1">
                                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorSHORTNAME" runat="server" InitialValue="0" ControlToValidate="ddlImages" ErrorMessage="Required Type of the Image..." CssClass="Validation" ValidationGroup="forfileupload" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                                                                                                    </td>

                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:DropDownList ID="dropMultiColor" runat="server" CssClass="form-control select2me">
                                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:DropDownList ID="dropMultiSize" runat="server" CssClass="form-control select2me">
                                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:FileUpload ID="FileUpload1" AllowMultiple="true" runat="server" meta:resourcekey="FileUpload1Resource1" />
                                                                                                                                                                    </td>
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:Button ID="Button2" ValidationGroup="forfileupload" runat="server" CssClass="btn green-haze" OnClick="Button2_Click" Style="padding-top: 7px; background-color: #3598dc; color: #fff; padding-bottom: 7px" Text="Add" />

                                                                                                                                                                    </td>
                                                                                                                                                                </tr>

                                                                                                                                                            </tbody>
                                                                                                                                                        </table>

                                                                                                                                                        <table class="table table-striped table-bordered table-hover" id="sample_7">
                                                                                                                                                            <thead class="repHeader">
                                                                                                                                                                <tr>
                                                                                                                                                                    <th style="width: 5%;">
                                                                                                                                                                        <asp:Label ID="Label13" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>

                                                                                                                                                                    <th style="width: 15%;">
                                                                                                                                                                        <asp:Label ID="Label14" runat="server" Text="Type of the Image " meta:resourcekey="lblProNameResource1"></asp:Label></th>

                                                                                                                                                                    <th style="width: 20%;">
                                                                                                                                                                        <asp:Label ID="Label21" runat="server" Text="Icon" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                    <th style="width: 20%;">
                                                                                                                                                                        <asp:Label ID="Label50" runat="server" Text="File" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                    <th style="width: 20%;">
                                                                                                                                                                        <asp:Label ID="Label24" runat="server" Text="Color" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                    <th style="width: 20%;">
                                                                                                                                                                        <asp:Label ID="Label26" runat="server" Text="Size" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                    <th style="width: 20%;">
                                                                                                                                                                        <asp:Label ID="Label29" runat="server" Text="Sort Number" meta:resourcekey="lblEditResource1"></asp:Label></th>

                                                                                                                                                                    <th style="width: 10%;">
                                                                                                                                                                        <asp:Label ID="Label28" runat="server" Text="Deleted" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                </tr>
                                                                                                                                                            </thead>
                                                                                                                                                            <tbody>
                                                                                                                                                                <asp:Repeater ID="listImag" runat="server" OnItemCommand="listImag_ItemCommand" OnItemDataBound="listImag_ItemDataBound">
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <tr class="gradeA">
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString() %>' meta:resourcekey="lblSRNOResource1"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblImagID" runat="server" Text='<%#getimagType(Convert .ToInt32 (Eval("ImageID"))) %>'></asp:Label>
                                                                                                                                                                                <asp:Label ID="lblIDIMG" runat="server" Visible="false" Text='<%#Eval("ImageID") %>'></asp:Label>
                                                                                                                                                                                <asp:Label ID="lblProduct" runat="server" Visible="false" Text='<%# Eval("MYPRODID") %>'></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <img src="Upload/<%#Eval("PICTURE")%>" style="width: 30px; height: 30px;">
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblImg" runat="server" Text='<%# Eval("PICTURE") %>'></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:DropDownList ID="ListdropMultiColor" runat="server" CssClass="form-control select2me">
                                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                                                <%-- <asp:Label ID="lblfutertype1" runat="server" Text='<%#getcolerName(Convert .ToInt32 ( Eval("COLORID"))) %>'></asp:Label>--%>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:DropDownList ID="ListdropMultiSize" runat="server" CssClass="form-control select2me">
                                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                                                <%--<asp:Label ID="Label30" runat="server" Text='<%#getSize(Convert .ToInt32 ( Eval("SIZECODE"))) %>'></asp:Label>--%>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:TextBox ID="txtShortBY" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString() %>'></asp:TextBox>
                                                                                                                                                                            </td>

                                                                                                                                                                            <td class="center">
                                                                                                                                                                                <asp:LinkButton ID="btndelet" runat="server" CommandName="btndelet" CommandArgument='<%#Eval("ID")  %>' OnClientClick="return confirm('Do you want to delete Image ?')">
                                                                                                                                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="ECOMM/images/deleteRec.png" />
                                                                                                                                                                                </asp:LinkButton>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                </asp:Repeater>
                                                                                                                                                                <br />
                                                                                                                                                                <asp:Repeater ID="BindImagProduct" runat="server" OnItemCommand="BindImagProduct_ItemCommand">
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <tr class="gradeA">
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString() %>' meta:resourcekey="lblSRNOResource1"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblImagID" runat="server" Text='<%#getimagType(Convert .ToInt32 (Eval("ImageID"))) %>'></asp:Label>
                                                                                                                                                                                <asp:Label ID="lblProduct" runat="server" Visible="false" Text='<%# Eval("MYPRODID") %>'></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <img src="Upload/<%#Eval("PICTURE")%>" style="width: 30px; height: 30px;">
                                                                                                                                                                                <%-- <asp:Label ID="lblImg" runat="server" Text='<%# Eval("PICTURE") %>'></asp:Label>--%>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblImg" runat="server" Text='<%# Eval("PICTURE") %>'></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>

                                                                                                                                                                                <asp:Label ID="lblfutertype1" runat="server" Text='<%#getcolerName(Convert .ToInt32 ( Eval("COLORID"))) %>'></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>

                                                                                                                                                                                <asp:Label ID="Label30" runat="server" Text='<%#getSize(Convert .ToInt32 ( Eval("SIZECODE"))) %>'></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:TextBox ID="txtShotName" runat="server" Text='<%# Eval("sortnumber") %>'></asp:TextBox>
                                                                                                                                                                                <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="center">
                                                                                                                                                                                <asp:LinkButton ID="btndelet" runat="server" CommandName="btndelet" CommandArgument='<%#Eval("ID")  %>' OnClientClick="return confirm('Do you want to delete Image ?')">
                                                                                                                                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="ECOMM/images/deleteRec.png" />
                                                                                                                                                                                </asp:LinkButton>
                                                                                                                                                                            </td>
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

                                                                                                                        </div>
                                                                                                                    </asp:Panel>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </ContentTemplate>
                                                                                                    <Triggers>
                                                                                                        <asp:PostBackTrigger ControlID="Button2" />
                                                                                                          <asp:PostBackTrigger ControlID="btnAddMainImg" />
                                                                                                        
                                                                                                    </Triggers>
                                                                                                </asp:UpdatePanel>
                                                                                            </asp:Panel>

                                                                                            <%--  <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                                                                <ContentTemplate>--%>
                                                                                            <asp:Panel ID="panelOff" runat="server" Style="display: none">
                                                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <div id="ac3-32" class="tab-pane">
                                                                                                            <%-- <asp:UpdatePanel runat="server" ID="UpdatePanel14">
                                                                                                                                <ContentTemplate>--%>
                                                                                                            <div class="portlet box purple">
                                                                                                                <div class="portlet-title">
                                                                                                                    <div class="caption">
                                                                                                                        <i class="fa fa-gift"></i>OFFER PRODUCT
                                                                                                                    </div>
                                                                                                                    <div class="tools">
                                                                                                                        <a href="javascript:;" class="collapse"></a>
                                                                                                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                                                        <a href="javascript:;" class="reload"></a>
                                                                                                                        <a href="javascript:;" class="remove"></a>
                                                                                                                    </div>
                                                                                                                    <div class="actions btn-set">
                                                                                                                        <asp:Button ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" CssClass="btn btn-primary" Style="padding-top: 7px; background-color: red; color: #fff; padding-bottom: 7px" Text="Add New" Height="34px" />
                                                                                                                        <asp:Button ID="btnOfferProduct" runat="server" OnClick="btnOfferProduct_Click" CssClass="btn yellow-crusta" Text="Save Offer" Height="34px" />
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <div class="portlet-body" style="padding-left: 25px;">
                                                                                                                    <div class="tabbable">
                                                                                                                        <div class="tab-content no-space">
                                                                                                                            <div class="tab-pane active" id="tab_general2">
                                                                                                                                <div class="form-body">


                                                                                                                                    <asp:Panel ID="PanelOFFERproadd" runat="server" meta:resourcekey="PanelOFFERproaddResource1">
                                                                                                                                        <div class="row">
                                                                                                                                            <div class="col-md-12">
                                                                                                                                                <div class="form-group">
                                                                                                                                                    <label class="col-md-2 control-label">
                                                                                                                                                        <asp:Label ID="lblOffWebPro" runat="server" Text="Web Product" meta:resourcekey="lblOffWebProResource1"></asp:Label>
                                                                                                                                                    </label>
                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:DropDownList ID="ddlOffWebProduct" runat="server" Enabled="false" CssClass="form-control select2me">
                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                    </div>
                                                                                                                                                    <label class="col-md-2 control-label">
                                                                                                                                                        <asp:Label ID="lblinterin" runat="server" Text="Interval in"></asp:Label>
                                                                                                                                                    </label>
                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:DropDownList ID="ddlOfferinterval" runat="server" CssClass="form-control select2me">
                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="ddlOfferinterval" InitialValue="0" runat="server" ErrorMessage="Please Enter To Date" ValidationGroup="submitOffremst" SetFocusOnError="True" meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator>
                                                                                                                                                    </div>
                                                                                                                                                </div>
                                                                                                                                            </div>


                                                                                                                                            <div class="col-md-12">
                                                                                                                                                <div class="form-group">
                                                                                                                                                    <label class="col-md-2 control-label">
                                                                                                                                                        <asp:Label ID="lblform" runat="server" Text="From Date"></asp:Label>
                                                                                                                                                    </label>
                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:TextBox ID="txtOffreFromDt" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="45"></asp:TextBox>
                                                                                                                                                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtOffreFromDt" Format="yyyy-MM-dd" Enabled="True">
                                                                                                                                                        </cc1:CalendarExtender>
                                                                                                                                                    </div>
                                                                                                                                                    <label class="col-md-2 control-label">
                                                                                                                                                        <asp:Label ID="lbltd" runat="server" Text="To Date"></asp:Label>
                                                                                                                                                    </label>
                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:TextBox ID="txtOffreToDt" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="45"></asp:TextBox>
                                                                                                                                                        <cc1:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtOffreToDt" Format="yyyy-MM-dd" Enabled="True">
                                                                                                                                                        </cc1:CalendarExtender>
                                                                                                                                                    </div>
                                                                                                                                                </div>
                                                                                                                                            </div>

                                                                                                                                            <div class="col-md-12">
                                                                                                                                                <div class="form-group">
                                                                                                                                                    <label class="col-md-2 control-label">
                                                                                                                                                        <asp:Label ID="lblttd" runat="server" Text="Till The Date"></asp:Label>
                                                                                                                                                    </label>
                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:TextBox ID="txtOffreTillDt" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="45"></asp:TextBox>
                                                                                                                                                        <cc1:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtOffreTillDt" Format="yyyy-MM-dd" Enabled="True">
                                                                                                                                                        </cc1:CalendarExtender>
                                                                                                                                                    </div>
                                                                                                                                                    <label class="col-md-2 control-label">
                                                                                                                                                        <asp:Label ID="Label51" runat="server" Text="Price"></asp:Label>
                                                                                                                                                    </label>
                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:TextBox ID="txtoledPrice" Enabled="false" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="45"></asp:TextBox>

                                                                                                                                                    </div>

                                                                                                                                                </div>
                                                                                                                                            </div>
                                                                                                                                            <div class="col-md-12">
                                                                                                                                                <div class="form-group">
                                                                                                                                                    <label class="col-md-2 control-label">
                                                                                                                                                        <asp:Label ID="Label52" runat="server" Text="Discount:-%"></asp:Label>
                                                                                                                                                    </label>
                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:TextBox ID="txtDiscount" runat="server" OnTextChanged="txtDiscount_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="45"></asp:TextBox>

                                                                                                                                                    </div>
                                                                                                                                                    <label class="col-md-2 control-label">
                                                                                                                                                        <asp:Label ID="Label53" runat="server" Text="New Price"></asp:Label>
                                                                                                                                                    </label>
                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:TextBox ID="txtnewprice" Enabled="false" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="45"></asp:TextBox>

                                                                                                                                                    </div>

                                                                                                                                                </div>
                                                                                                                                            </div>
                                                                                                                                            <div class="col-md-12">
                                                                                                                                                <div class="form-group">
                                                                                                                                                    <label class="col-md-2 control-label">
                                                                                                                                                        <asp:Label ID="Label54" runat="server" Text="Offers"></asp:Label>
                                                                                                                                                    </label>
                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:DropDownList ID="drpOffers" runat="server" CssClass="form-control select2me">
                                                                                                                                                        </asp:DropDownList>
                                                                                                                                                    </div>
                                                                                                                                                    <label class="col-md-2 control-label">
                                                                                                                                                        <asp:Label ID="lbldow" runat="server" Text="Displayed on Web"></asp:Label>
                                                                                                                                                    </label>
                                                                                                                                                    <div class="col-md-2">
                                                                                                                                                        <asp:TextBox ID="txtOffreDispOnWeb" runat="server" Enabled="False" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="45" meta:resourcekey="txtOffreDispOnWebResource1"></asp:TextBox>
                                                                                                                                                    </div>
                                                                                                                                                    <div class="col-md-2">

                                                                                                                                                        <asp:Image ID="Image4" runat="server" ImageUrl="ECOMM/images/plus.png" OnClick="javascript:showpanepaneofferline();" Style="float: right" />
                                                                                                                                                    </div>
                                                                                                                                                    <%--<div class="col-md-2">
                                                                                                                                                <asp:Image ID="Image6" runat="server" ImageUrl="~/Admin/ECOMM/images/plus.png" OnClick="javascript:showpanepaneofferline();" ToolTip="Add More Item" meta:resourcekey="Image6Resource1" />
                                                                                                                                            </div>--%>
                                                                                                                                                </div>
                                                                                                                                                <asp:Panel ID="PanelOFFERline" runat="server" Style="display: none" meta:resourcekey="panebusslineResource1">
                                                                                                                                                    <fieldset style="margin-bottom: 10px;">
                                                                                                                                                        <legend>Dispaly On Site By OfferProduct</legend>
                                                                                                                                                        <div class="row" style="margin-right: 0px;">
                                                                                                                                                            <div class="form-group">
                                                                                                                                                                <div class="col-md-6">
                                                                                                                                                                    <asp:Label runat="server" ID="Label43" class="col-md-4 control-label" Text="Placeholder Line"></asp:Label>
                                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                                        <asp:TextBox ID="txtOffPlac" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpplResource1"></asp:TextBox>
                                                                                                                                                                    </div>
                                                                                                                                                                </div>
                                                                                                                                                                <div class="col-md-6">
                                                                                                                                                                    <asp:Label runat="server" ID="Label44" class="col-md-4 control-label" Text="Placeholder Ali R L"></asp:Label>
                                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                                        <asp:TextBox ID="txtoffPArl" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbprlResource1"></asp:TextBox>
                                                                                                                                                                    </div>

                                                                                                                                                                </div>
                                                                                                                                                            </div>


                                                                                                                                                            <div class="form-group">
                                                                                                                                                                <div class="col-md-6">
                                                                                                                                                                    <asp:Label runat="server" ID="Label45" class="col-md-4 control-label" Text="Placeholder Column"></asp:Label>
                                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                                        <asp:TextBox ID="txtoffPCol" runat="server" Text="0" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtpbcolResource1"></asp:TextBox>
                                                                                                                                                                    </div>

                                                                                                                                                                </div>
                                                                                                                                                                <div class="col-md-6">
                                                                                                                                                                    <asp:Label runat="server" ID="Label46" class="col-md-4 control-label" Text="Link 2Direct"></asp:Label>
                                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                                        <asp:TextBox ID="txtLink2D" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpl2dResource1"></asp:TextBox>
                                                                                                                                                                    </div>

                                                                                                                                                                </div>
                                                                                                                                                            </div>

                                                                                                                                                            <div class="form-group">
                                                                                                                                                                <div class="col-md-6">
                                                                                                                                                                    <asp:Label runat="server" ID="Label47" class="col-md-4 control-label" Text="Sort Number"></asp:Label>
                                                                                                                                                                    <div class="col-md-8">
                                                                                                                                                                        <asp:TextBox ID="txtoffsortN" Text="0" runat="server" AutoCompleteType="Disabled" CssClass="form-control" meta:resourcekey="txtbpsnoResource1"></asp:TextBox>
                                                                                                                                                                    </div>

                                                                                                                                                                </div>

                                                                                                                                                            </div>

                                                                                                                                                        </div>
                                                                                                                                                    </fieldset>
                                                                                                                                                </asp:Panel>
                                                                                                                                            </div>

                                                                                                                                        </div>
                                                                                                                                    </asp:Panel>
                                                                                                                                    <div class="row">
                                                                                                                                        <div class="col-md-12">
                                                                                                                                            <div class="form-horizontal form-row-seperated">
                                                                                                                                                <div class="portlet box green-turquoise">
                                                                                                                                                    <div class="portlet-title">
                                                                                                                                                        <div class="caption">
                                                                                                                                                            <i class="fa fa-gift"></i>OFFER PRODUCT LIST
                                                                                                                                                        </div>
                                                                                                                                                        <%--<div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>--%>
                                                                                                                                                    </div>
                                                                                                                                                    <div class="portlet-body" style="padding-left: 25px;">
                                                                                                                                                        <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                                                                                                                    <ContentTemplate>--%>

                                                                                                                                                        <table class="table table-striped table-bordered table-hover" id="sample_3">
                                                                                                                                                            <thead class="repHeader">
                                                                                                                                                                <tr>
                                                                                                                                                                    <th>
                                                                                                                                                                        <asp:Label ID="Label25" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                                                                                                    <th>
                                                                                                                                                                        <asp:Label ID="Label27" runat="server" Text="Web Product" meta:resourcekey="lblSNameResource2"></asp:Label></th>
                                                                                                                                                                    <th>
                                                                                                                                                                        <asp:Label ID="Label36" runat="server" Text="Interval" meta:resourcekey="lblProNameResource1"></asp:Label></th>

                                                                                                                                                                    <th>
                                                                                                                                                                        <asp:Label ID="Label37" runat="server" Text="From Date" meta:resourcekey="lblEditResource1"></asp:Label></th>
                                                                                                                                                                    <th>
                                                                                                                                                                        <asp:Label ID="Label38" runat="server" Text="Till Date" meta:resourcekey="lblDelResource1"></asp:Label></th>
                                                                                                                                                                    <th>
                                                                                                                                                                        <asp:Label ID="Label39" runat="server" Text="Edit" meta:resourcekey="lblDelResource1"></asp:Label></th>
                                                                                                                                                                    <th>
                                                                                                                                                                        <asp:Label ID="Label41" runat="server" Text="Delete" meta:resourcekey="lblDelResource1"></asp:Label></th>
                                                                                                                                                                </tr>
                                                                                                                                                            </thead>
                                                                                                                                                            <tbody>
                                                                                                                                                                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <tr class="gradeA">
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString() %>' meta:resourcekey="lblSRNOResource1"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="Qname" runat="server" Text='<%# getWebProduct(Convert .ToInt32 ( Eval("MYPRODID"))) %>' meta:resourcekey="QnameResource1"></asp:Label>

                                                                                                                                                                            </td>

                                                                                                                                                                            <td>
                                                                                                                                                                                <%-- <asp:Label ID="lblAns" runat="server" Visible="False" Text='<%# Eval("OfferInterIn") %>' meta:resourcekey="lblAnsResource4"></asp:Label>--%>
                                                                                                                                                                                <asp:Label ID="lblspn" runat="server" Text='<%# Eval("INTERVAL") %>' meta:resourcekey="lblspnResource1"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="Label40" runat="server" Text='<%# Eval("FROMDATE") %>' meta:resourcekey="lblspnResource1"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td>
                                                                                                                                                                                <asp:Label ID="Label42" runat="server" Text='<%# Eval("TILLDATE") %>' meta:resourcekey="lblspnResource1"></asp:Label>
                                                                                                                                                                            </td>
                                                                                                                                                                            <td class="center">
                                                                                                                                                                                <asp:LinkButton ID="lnkbtnOff" runat="server" CommandArgument='<%# Eval("MYID") %>' CommandName="btnedit">
                                                                                                                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="ECOMM/images/editRec.png" />
                                                                                                                                                                                </asp:LinkButton></td>
                                                                                                                                                                            <td class="center">
                                                                                                                                                                                <asp:LinkButton ID="lnkbtndelete" runat="server" CommandArgument='<%# Eval("MYID") %>' CommandName="Delete" OnClientClick="return confirm('Do you want to delete offer product?')">
                                                                                                                                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="ECOMM/images/deleteRec.png" />
                                                                                                                                                                                </asp:LinkButton></td>
                                                                                                                                                                        </tr>
                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                </asp:Repeater>
                                                                                                                                                            </tbody>
                                                                                                                                                        </table>


                                                                                                                                                        <%--  </ContentTemplate>
                                                                                                                                                                                </asp:UpdatePanel>--%>
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
                                                                                                            </div>
                                                                                                            <%--</ContentTemplate>
                                                                                                                            </asp:UpdatePanel>--%>
                                                                                                        </div>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </asp:Panel>
                                                                                            <%--</ContentTemplate>
                                                                                                            </asp:UpdatePanel>--%>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>
                                                                            </asp:Panel>
                                                                        </div>
                                                                        <div class="btnsetposition">

                                                                            <asp:Button ID="BtnMstProSave" runat="server" CssClass="btn btn-primary" Style="padding-top: 7px; padding-bottom: 7px" meta:resourcekey="btnSubmitSearchResource1" Text="Update" OnClick="BtnMstProSave_Click" />
                                                                            <asp:Button ID="btnDiscard" runat="server" CssClass="btn btn-default" meta:resourcekey="btnCloseResource1" Text="Discard" OnClientClick="return clearallText();" OnClick="btnDiscard_Click" />
                                                                            <%--<asp:Button ID="btnpreview" runat="server" Visible ="false"  CssClass="btn btn-primary" Style="padding-top: 7px; padding-bottom: 7px"  Text="Preview" OnClick ="btnpreview_Click" />--%>
                                                                            <asp:LinkButton ID="btnpreview123" Visible="false" CssClass="btn btn-primary" Style="padding-top: 7px; padding-bottom: 7px" OnClick="btnpreview_Click" runat="server">Preview</asp:LinkButton>
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
                                </div>
                            </div>
                        </asp:Panel>
                        <%--</ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="BtnMstProSave" />
                                <asp:AsyncPostBackTrigger ControlID="btnDiscard" />
                                <asp:PostBackTrigger ControlID="pMasterGridview" />
                                

                            </Triggers>


                        </asp:UpdatePanel>--%>
                    </div>
                </div>

            </div>

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