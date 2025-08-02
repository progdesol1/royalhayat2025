<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.Master" AutoEventWireup="true" CodeBehind="TrackProduct.aspx.cs" Inherits="Web.TrackProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <div class="container">
            <ul class="breadcrumb">
                <li><a href="index.aspx">Home</a></li>
               
                <li class="active">Track Product</li>
            </ul>
            <!-- BEGIN SIDEBAR & CONTENT -->
            <div class="row margin-bottom-40">
                <!-- BEGIN SIDEBAR -->
                <div class="sidebar col-md-3 col-sm-5">
                    <div class="panel-group checkout-page accordion scrollable" id="checkout-page">

                        <!-- BEGIN CHECKOUT -->
                        <div id="checkout" class="panel panel-default">
                            <div class="panel-heading">
                                <h2 class="panel-title" style="background-color: #e45000">
                                    <a data-toggle="collapse" data-parent="#checkout-page" href="#checkout-content" class="accordion-toggle">Browse By Category
                                    </a>
                                </h2>
                            </div>
                            <div id="checkout-content" class="panel-collapse collapse in">
                                <ul class="list-group margin-bottom-25 sidebar-menu">
                                    <asp:ListView ID="ltsCategory" runat="server" OnItemDataBound="ltsCategory_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCATID" runat="server" Visible="false" Text=' <%# Eval("PARENT_CATID")%>'></asp:Label>
                                            <li class="list-group-item clearfix dropdown">
                                                <a href="#">
                                                    <i class="fa fa-angle-right"></i><%# Eval("ParentCatName")%><asp:Label runat="server" ID="lblSubCount"></asp:Label></a>
                                                <ul class="dropdown-menu">
                                                    <asp:ListView ID="ltsSubCategory" runat="server">
                                                        <ItemTemplate>
                                                            <li class="list-group-item dropdown clearfix">
                                                                <a href='<%# "ProductList.aspx?PID="+ Eval("CATID")%>'><i class="fa fa-angle-right"></i><%# Eval("CatName")%>
                                                                    <asp:Label runat="server" ID="lblCount" Text='<%# "( " +Eval("SubCatFound")+" )"%>'></asp:Label></a>

                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </ul>
                                            </li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </ul>
                            </div>
                        </div>
                        <div id="payment-address" class="panel panel-default">
                            <div class="panel-heading">
                                <h2 class="panel-title" style="background-color: #e45000">
                                    <a data-toggle="collapse" data-parent="#checkout-page" href="#payment-address-content" class="accordion-toggle">Browse By Brand
                                    </a>
                                </h2>
                            </div>
                            <div id="payment-address-content" class="panel-collapse collapse">
                                <ul class="list-group margin-bottom-25 sidebar-menu">
                                    <asp:ListView ID="ltsBrand" runat="server" OnItemDataBound="ltsBrand_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHide" runat="server" Visible="false" Text=' <%# Eval("Brand")%>'></asp:Label>
                                            <li class="list-group-item clearfix dropdown">
                                                <a href='<%# "ProductList.aspx?BID="+ Eval("Brand")%>'>
                                                    <i class="fa fa-angle-right"></i><%# Eval("BrandName")%><asp:Label runat="server" ID="lblCount1" Text='<%#"("+ Eval("NosInbrandFound")+")"%>'></asp:Label></a>

                                            </li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </ul>
                            </div>
                        </div>

                    </div>

                    <div class="sidebar-products clearfix">
                        <h2>Bestsellers</h2>
                        <asp:ListView ID="listBestSell" runat="server">
                            <ItemTemplate>
                                <div class="item">
                                    <a href="#">
                                        <img src="ECOMM/Upload/<%# GETIMG(Convert .ToInt32 ( Eval("MYPRODID")))%>" alt="Some Shoes in Animal with Cut Out"></a>
                                    <h3><a href="ProductDisscription.aspx?PN=<%# ((Web.UserMaster)this.Master).GetProductName(Convert.ToInt32(Eval("MyID"))) %>&PID=<%# Eval("MyID")%>">
                                        <asp:Label ID="Label3" runat="server" Text='<%# getprodname( Convert .ToInt32 (Eval("MYPRODID")))%>'></asp:Label></a></h3>
                                    <div class="price"><%# getCost (Convert .ToInt32  (Eval("MYPRODID")))%></div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                        <%--<div class="item">
                            <a href="shop-item.html">
                                <img src="../../assets/frontend/pages/img/products/k4.jpg" alt="Some Shoes in Animal with Cut Out"></a>
                            <h3><a href="shop-item.html">Some Shoes in Animal with Cut Out</a></h3>
                            <div class="price">$23.00</div>
                        </div>
                        <div class="item">
                            <a href="shop-item.html">
                                <img src="../../assets/frontend/pages/img/products/k3.jpg" alt="Some Shoes in Animal with Cut Out"></a>
                            <h3><a href="shop-item.html">Some Shoes in Animal with Cut Out</a></h3>
                            <div class="price">$86.00</div>
                        </div>--%>
                    </div>
                </div>
                <!-- END SIDEBAR -->

                <!-- BEGIN CONTENT -->
                <div class="col-md-9 col-sm-7">

                    <div class="product-page">
                        <div class="row">

                        </div>
                    </div>
                </div>
            </div>
            <div class="row margin-bottom-40">
                <div class="col-md-12 col-sm-12">
                    <h2>Most popular products</h2>
                    <div class="owl-carousel owl-carousel5">
                        <asp:ListView ID="listproduct" runat="server" OnItemDataBound="listproduct_ItemDataBound">

                            <ItemTemplate>
                                <div>
                                    <div class="product-item">
                                        <div class="pi-img-wrapper">
                                            <img src="ECOMM/Upload/<%# GETIMG(Convert .ToInt32 ( Eval("MYPRODID")))%>" class="img-responsive" style="width: 250px; height: 200px" alt="Berry Lace Dress">
                                            <div>
                                                <a href="ECOMM/Upload/<%# GETIMG(Convert .ToInt32 ( Eval("MYPRODID")))%>" class="btn btn-default fancybox-button">Zoom</a>
                                                <a href="#listproduct<%# Eval("MYPRODID")%>" class="btn btn-default fancybox-fast-view">View</a>
                                            </div>
                                        </div>
                                        <h3><a href="shop-item.html">
                                            <asp:Label ID="Label3" runat="server" Text='<%# getprodname( Convert .ToInt32 (Eval("MYPRODID")))%>'></asp:Label></a></h3>
                                        <div class="pi-price"> <strong><span>K.D</span><asp:Label ID="labelNewpi" runat="server"></asp:Label></strong></div>
                                        <asp:LinkButton ID="lbtAddcart" CommandArgument='<%# Eval("MYPRODID")  %>' CommandName="lbtAddcart" class="btn btn-default add2cart" runat="server">Add to cart</asp:LinkButton>
                                        <%# ((Web.UserMaster)this.Master).getsale(Convert .ToInt32 ( Eval("MyID")))%>
                                        <%--<div  class="sticker sticker-sale"></div>
                                         <div  class="sticker sticker-new"></div>--%>
                                    </div>
                                </div>
                              <div id="listproduct<%# Eval("MYPRODID")%>" style="display: none; width: 700px;">
                                    <div class="product-page product-pop-up">
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-3">
                                                <div class="product-main-image">
                                                    <img src="ECOMM/Upload/<%# ((Web.UserMaster)this.Master).MainImag(Convert .ToInt32 ( Eval("MYPRODID")))%>" alt="Cool green dress with red bell" class="img-responsive">
                                                </div>
                                                <div class="product-other-images">
                                                    <asp:ListView ID="listImagBind" runat="server">
                                                        <ItemTemplate>
                                                            <img alt='<%#Eval("PICTURE")%>' src="ECOMM/Upload/<%#Eval("PICTURE")%>">
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-9">
                                                <h2><%# ((Web.UserMaster)this.Master).GetProductName(Convert .ToInt32 ( Eval("MyID")))%></h2>
                                                <div class="price-availability-block clearfix">
                                                    <div class="price">
                                                        <strong><span>K.D</span><asp:Label ID="labNewPties" runat="server"></asp:Label></strong>
                                                        <em><span>
                                                            <asp:Label ID="lbloledpriesh" runat="server"></asp:Label>
                                                        </span></em>
                                                    </div>
                                                    <div class="availability">
                                                        Availability: <strong>
                                                            <asp:Label ID="lblstock" Text='<%# ((Web.UserMaster)this.Master).getAvailibility(Convert .ToInt32 ( Eval("MyID")))%>' runat="server"></asp:Label></strong>
                                                    </div>
                                                </div>
                                                <div class="description">
                                                    <p>
                                                        <asp:Label ID="Label2" Text='<%# ((Web.UserMaster)this.Master).getdiscription(Convert .ToInt32 ( Eval("MYPRODID")))%>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblProdID" Visible="false" Text='<%#  Eval("MyID")%>' runat="server"></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="product-page-options">
                                                    <div class="pull-left ">
                                                        <asp:Label ID="lblSize" runat="server" class="control-label" Text="Size:"></asp:Label>

                                                    </div>
                                                    <div class="pull-left ">
                                                        <asp:ListView ID="listMultiSizeNewArevel" runat="server">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblId" runat="server" Visible="false" Text=' <%# Eval("ID")%>'></asp:Label>
                                                                <asp:Label ID="lblMultisize" runat="server" class="control-label" Style="padding: 10px 25px; background-color: rgb(189, 189, 189); margin-right: 10px;" Text='<%# ((Web.UserMaster)this.Master).getsize (Convert .ToInt32 ( Eval("SIZECODE")))%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <asp:LinkButton ID="btnMultiSize" class="control-label" runat="server" Style="padding: 10px 25px; background-color: rgb(189, 189, 189); color: black; margin-right: 10px;">All Size</asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="product-page-options">
                                                    <div class="pull-left">
                                                        <asp:Label ID="lblcoler" runat="server" class="control-label" Text="Color:"></asp:Label>


                                                    </div>
                                                    <div class="pull-left">
                                                        <div class="product-other-images">

                                                            <asp:ListView ID="listMultiColerNewArevw" runat="server">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProduct" runat="server" Visible="false" Text=' <%# Eval("MYPRODID")%>'></asp:Label>
                                                                    <asp:Label ID="lblColerID" runat="server" Visible="false" Text=' <%# Eval("COLORID")%>'></asp:Label>
                                                                    <asp:Label ID="LblId" runat="server" Visible="false" Text=' <%# Eval("ID")%>'></asp:Label>
                                                                    <%-- <a href="assets/frontend/pages/img/products/model3.jpg" >--%>
                                                                    <%--<asp:Image ID="Image1" runat="server" ImageUrl="ECOMM/Upload/<%# GETIMG(Convert .ToInt32 ( Eval("MYPRODID")))%>" />--%>
                                                                    <div <%# ((Web.UserMaster)this.Master).GetStyle(Convert.ToInt32(Eval("COLORID"))) %>>
                                                                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;  
                                                                    </div>

                                                                    <%-- <img alt="Berry Lace Dress" src="assets/frontend/pages/img/products/model3.jpg">--%>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                            <asp:LinkButton ID="AllCorols" class="control-label" runat="server" Style="padding: 10px 25px; background-color: rgb(189, 189, 189); color: black; margin-right: 10px;">All Colors </asp:LinkButton>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="product-page-cart">
                                                    <div class="product-quantity">
                                                        <asp:TextBox ID="txtquntiy123" class="form-control input-sm" Text="1" runat="server"></asp:TextBox>
                                                    </div>
                                                    <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("MYPRODID")  %>' CommandName="lbtAddcart" class="btn btn-primary" runat="server">Add to cart</asp:LinkButton>
                                                    <%--  <button class="btn btn-primary" type="submit">Add to cart</button>--%>
                                                    <a href="ProductDisscription.aspx?PN=<%# ((Web.UserMaster)this.Master).GetProductName(Convert.ToInt32(Eval("MyID"))) %>&PID=<%# Eval("MyID")%>" class="btn btn-default">More details</a>
                                                </div>
                                                <div class="product-page-cart">
                                                    <asp:LinkButton ID="LinkButton4" CommandArgument='<%# Eval("MYPRODID")  %>' Style="padding-left: 10px;" CommandName="addtowishlist" class="pi-price" runat="server"><img src="ECOMM/Upload/wishlist.png" style ="padding-right: 5px;" />Add to My Wish list</asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton5" CommandArgument='<%# Eval("MYPRODID")  %>' Style="padding-left: 10px;" CommandName="addtomyfavorite" class="pi-price" runat="server"><img src="ECOMM/Upload/faverite.png" style ="padding-right: 5px;" /> Add to My Favorite</asp:LinkButton>
                                                </div>
                                            </div>
                                            <%# ((Web.UserMaster)this.Master).getsale(Convert .ToInt32 ( Eval("MyID")))%>
                                            <%--  <div class="sticker sticker-sale"></div>--%>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                        <%-- <div>
                            <div class="product-item">
                                <div class="pi-img-wrapper">
                                    <img src="../../assets/frontend/pages/img/products/k2.jpg" class="img-responsive" alt="Berry Lace Dress">
                                    <div>
                                        <a href="../../assets/frontend/pages/img/products/k2.jpg" class="btn btn-default fancybox-button">Zoom</a>
                                        <a href="#product-pop-up" class="btn btn-default fancybox-fast-view">View</a>
                                    </div>
                                </div>
                                <h3><a href="shop-item.html">Berry Lace Dress2</a></h3>
                                <div class="pi-price">$29.00</div>
                                <a href="javascript:;" class="btn btn-default add2cart">Add to cart</a>
                            </div>
                        </div>
                        <div>
                            <div class="product-item">
                                <div class="pi-img-wrapper">
                                    <img src="../../assets/frontend/pages/img/products/k3.jpg" class="img-responsive" alt="Berry Lace Dress">
                                    <div>
                                        <a href="../../assets/frontend/pages/img/products/k3.jpg" class="btn btn-default fancybox-button">Zoom</a>
                                        <a href="#product-pop-up" class="btn btn-default fancybox-fast-view">View</a>
                                    </div>
                                </div>
                                <h3><a href="shop-item.html">Berry Lace Dress3</a></h3>
                                <div class="pi-price">$29.00</div>
                                <a href="javascript:;" class="btn btn-default add2cart">Add to cart</a>
                            </div>
                        </div>
                        <div>
                            <div class="product-item">
                                <div class="pi-img-wrapper">
                                    <img src="../../assets/frontend/pages/img/products/k4.jpg" class="img-responsive" alt="Berry Lace Dress">
                                    <div>
                                        <a href="../../assets/frontend/pages/img/products/k4.jpg" class="btn btn-default fancybox-button">Zoom</a>
                                        <a href="#product-pop-up" class="btn btn-default fancybox-fast-view">View</a>
                                    </div>
                                </div>
                                <h3><a href="shop-item.html">Berry Lace Dress4</a></h3>
                                <div class="pi-price">$29.00</div>
                                <a href="javascript:;" class="btn btn-default add2cart">Add to cart</a>
                                <div class="sticker sticker-sale"></div>
                            </div>
                        </div>
                        <div>
                            <div class="product-item">
                                <div class="pi-img-wrapper">
                                    <img src="../../assets/frontend/pages/img/products/k1.jpg" class="img-responsive" alt="Berry Lace Dress">
                                    <div>
                                        <a href="../../assets/frontend/pages/img/products/k1.jpg" class="btn btn-default fancybox-button">Zoom</a>
                                        <a href="#product-pop-up" class="btn btn-default fancybox-fast-view">View</a>
                                    </div>
                                </div>
                                <h3><a href="shop-item.html">Berry Lace Dress5</a></h3>
                                <div class="pi-price">$29.00</div>
                                <a href="javascript:;" class="btn btn-default add2cart">Add to cart</a>
                            </div>
                        </div>
                        <div>
                            <div class="product-item">
                                <div class="pi-img-wrapper">
                                    <img src="../../assets/frontend/pages/img/products/k2.jpg" class="img-responsive" alt="Berry Lace Dress">
                                    <div>
                                        <a href="../../assets/frontend/pages/img/products/k2.jpg" class="btn btn-default fancybox-button">Zoom</a>
                                        <a href="#product-pop-up" class="btn btn-default fancybox-fast-view">View</a>
                                    </div>
                                </div>
                                <h3><a href="shop-item.html">Berry Lace Dress6</a></h3>
                                <div class="pi-price">$29.00</div>
                                <a href="javascript:;" class="btn btn-default add2cart">Add to cart</a>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
    <asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
     <script type="text/javascript">
         jQuery(document).ready(function () {
             Layout.init();
             Layout.initOWL();
             Layout.initTwitter();
             Layout.initImageZoom();
             Layout.initTouchspin();
             Layout.initUniform();
             Layout.initSliderRange();
         });
    </script>
</asp:Content>
