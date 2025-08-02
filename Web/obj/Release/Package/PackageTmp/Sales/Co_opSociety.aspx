<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="Co_opSociety.aspx.cs" Inherits="Web.Sales.Co_opSociety" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }

        .linkpadding {
            padding: 0px 5px 0px 5px;
        }
    </style>
    <script src="popup/jquery-1.htm"></script>
    <script src="popup/tie-scripts.htm"></script>
    <script src="popup/WebResource.js" type="text/javascript"></script>

    <script src="popup/ScriptResource.js" type="text/javascript"></script>
    <script type="text/javascript">
        //<![CDATA[
        if (typeof (Sys) === 'undefined') throw new Error('ASP.NET Ajax client-side framework failed to load.');
        //]]>
    </script>



    <script src="popup/ScriptResource_002.js" type="text/javascript"></script>
    <script src="popup/ThailandDrow.js" type="text/javascript"></script>

    <link rel="stylesheet" id="easy-image-gallery-css" href="popup/easy-image-galleryc64e.css" type="text/css" media="screen">
    <link rel="stylesheet" id="pretty-photo-css" href="popup/prettyPhotoc64e.htm" type="text/css" media="screen">
    <script type="text/javascript" src="popup/jquery_005.js"></script>
    <script type="text/javascript" src="popup/tie-scripts3a05.js"></script>

    <script src="popup/common.js" charset="UTF-8" type="text/javascript"></script>
    <script src="popup/map.js" charset="UTF-8" type="text/javascript"></script>
    <script src="popup/util.js" charset="UTF-8" type="text/javascript"></script>
    <script src="popup/marker.js" charset="UTF-8" type="text/javascript"></script>
    <script src="popup/onion.js" charset="UTF-8" type="text/javascript"></script>
    <script src="popup/infowindow.js" charset="UTF-8" type="text/javascript"></script>
    <script src="popup/stats.js" charset="UTF-8" type="text/javascript"></script>
    <script src="popup/controls.js" charset="UTF-8" type="text/javascript"></script>
    <style class="firebugResetStyles" type="text/css" charset="utf-8">
        /* See license.txt for terms of usage */
    </style>
    <script src="popup/QuotaService.htm" charset="UTF-8" type="text/javascript"></script>

    <script type="text/javascript" src="popup/jquery_003.js"></script>
    <script type="text/javascript" src="popup/jquery.js"></script>
    <script type="text/javascript" src="popup/jquery_006.js"></script>
    <script type="text/javascript" src="popup/jquery_007.js"></script>

    <script type="text/javascript" src="popup/ajax.js"></script>


    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />

    <link href="../assets/global/css/components-rounded.css" id="style_components" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout3/css/layout.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout3/css/themes/default.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/admin/layout3/css/custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function showProgress() {
            $.blockUI({ message: '<h1>please wait...</h1>' });
        }

        function stopProgress() {
            $.unblockUI();
        }

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">

                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panelMsg" runat="server" Visible="false">
                    <div class="alert alert-danger alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblErreorMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">

                            <div class="row">
                                <div class="col-md-12">

                                    <div class="portlet box green">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-cogs font-grey"></i>
                                                <span class="caption-subject font-grey bold uppercase">Sales Co-Operative Society
                                                </span>
                                            </div>

                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                            <div class="actions btn-set">
                                            </div>
                                        </div>
                                        <div class="portlet-body">

                                            <table class="table table-bordered " style="width: 100%; font: normal;">
                                                <%--id="sample_editable_1"--%>
                                                <%--class="table table-striped table-hover table-bordered"--%>
                                                <thead>

                                                    <asp:Label ID="Label7" runat="server" Text="Date :"></asp:Label>
                                                    <asp:Label ID="lblDateSales_Co_op" runat="server" Text=""></asp:Label>

                                                    <tr>
                                                        <th></th>
                                                        <th>Customer </th>
                                                        <th>Product #   </th>
                                                        <th>Qty </th>
                                                        <th>Amount </th>
                                                        <%--<th>Confirmed Qty </th>
                                                        <th>Delivered Qty- Date </th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="List_CO_Operative" runat="server" OnItemDataBound="List_CO_Operative_ItemDataBound" OnItemCommand="List_CO_Operative_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>

                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkbtnItem" CssClass="btn blue" Style="padding: 0px 5px 0px 5px;" runat="server" OnClick="lnkbtnItem_Click" OnClientClick="showProgress()"><i class="fa fa-save"></i></asp:LinkButton></td>
                                                                <asp:Panel ID="pnlMultiColoer" CssClass="modalPopup" runat="server" Style="display: none; height: auto; overflow: auto;">

                                                                    <%-- <section class="cat-box recent-box">

                                                                        <div class="cat-box-content" style="width: 250px;">--%>

                                                                    <div class="row" style="position: fixed; left: 10%; top: 3%;">


                                                                        <div style="text-align: left; background: #e26a70; align-items: center;">
                                                                            <div style="padding: 25px">
                                                                                <br />

                                                                                <div>
                                                                                    <asp:Label ID="Label9" runat="server" Text="Reference :"></asp:Label>
                                                                                    <asp:Label ID="lblReference" runat="server" Text='<%# ReffranceNO(Convert.ToInt32(Eval("COMPID"))) %>'></asp:Label>
                                                                                </div>
                                                                                <div>
                                                                                    <asp:Label ID="Label5" runat="server" Text="Customer"></asp:Label><br />
                                                                                    <asp:Label ID="lblCOMPID" Visible="false" runat="server" Text='<%# Eval("COMPID") %>'></asp:Label>
                                                                                    <asp:TextBox ID="txtCutomer" runat="server" Enabled="false" Placeholder="Customer" Text='<%# Eval("COMPNAME1")%>' TabIndex="1" Width="200px" Style="text-transform: uppercase;"></asp:TextBox>

                                                                                </div>

                                                                                <div>
                                                                                    <asp:Label ID="Label6" runat="server" Text="Product #"></asp:Label><br />
                                                                                    <asp:Label ID="lblMYPRODID" Visible="false" runat="server" Text='<%# Eval("MYPRODID")%>'></asp:Label>
                                                                                    <asp:Label ID="lblUOM" Visible="false" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                                                    <asp:TextBox ID="txtproduct" runat="server" Enabled="false" Placeholder="Product #" Text='<%# Eval("ProdName1")%>' TabIndex="1" Width="200px" Style="text-transform: uppercase;"></asp:TextBox>

                                                                                </div>

                                                                                <div>
                                                                                    <asp:Label ID="Label3" runat="server" Text="QTY"></asp:Label><br />
                                                                                    <asp:TextBox ID="txtpopQty" runat="server" AutoPostBack="true" OnTextChanged="txtpopQty_TextChanged" Text='<%# getQty( Convert.ToInt32(Eval("MYPRODID")),Eval("UOM").ToString(),Convert.ToInt32(Eval("COMPID"))) %>' Placeholder="QTY" TabIndex="1" Width="200px" Style="text-transform: uppercase;"></asp:TextBox>

                                                                                </div>

                                                                                <div>
                                                                                    <asp:Label ID="Label8" runat="server" Text="Unit Price:-"></asp:Label>
                                                                                    <br />
                                                                                    <asp:TextBox ID="txtpopamt" runat="server" Enabled="false" Text='<%# getMSRP(Convert.ToInt32(Eval("MYPRODID")),Convert.ToInt32(Eval("UOM")),Convert.ToInt32(Eval("COMPID"))) %>' Placeholder="Amount" TabIndex="1" Width="200px" Style="text-transform: uppercase;"></asp:TextBox>

                                                                                </div>

                                                                                <div>
                                                                                    <asp:Label ID="Label11" runat="server" Text="Total Amount:-"></asp:Label>
                                                                                    <br />
                                                                                    <asp:TextBox ID="txttotalamt" runat="server" Enabled="false" Text="" Placeholder="Amount" TabIndex="1" Width="200px" Style="text-transform: uppercase;"></asp:TextBox>

                                                                                </div>


                                                                                <div>

                                                                                    <asp:Label ID="Label10" runat="server" Text="Confirmed Qty"></asp:Label><br />
                                                                                    <asp:TextBox ID="txtcnfqty" runat="server" Enabled="false" Text="" Placeholder="Confirmed Qty" TabIndex="1" Width="200px" Style="text-transform: uppercase;"></asp:TextBox>

                                                                                </div>


                                                                                <div>
                                                                                    <asp:Label ID="Label12" runat="server" Text="Delivered Qty- Date"></asp:Label><br />
                                                                                    <asp:TextBox ID="txtdelQty" runat="server" Enabled="false" Text="" Placeholder="Delivered Qty" TabIndex="1" Width="200px" Style="text-transform: uppercase;"></asp:TextBox>

                                                                                </div>

                                                                                <br />
                                                                                <div>
                                                                                    <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" OnClientClick="showProgress()" Text="save" />
                                                                                    <%--<asp:Button ID="btnCustomerLogin" Visible="false" runat="server" CommandName="Save" CommandArgument='<%# Eval("COMPID") + "," + Eval("MYPRODID")+","+Eval("UOM") %>' Text="Save"></asp:Button>--%>
                                                                                    <asp:Button ID="btnCancelClogin" runat="server" Text="Close" />
                                                                                </div>

                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <%--</div>
                                                                    </section>--%>
                                                                </asp:Panel>

                                                                <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" DynamicServicePath="" BackgroundCssClass="modalBackground" CancelControlID="btnCancelClogin" Enabled="True" PopupControlID="pnlMultiColoer" TargetControlID="LinkButton1"></cc1:ModalPopupExtender>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" OnClientClick="showProgress()">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME1")%>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" OnClientClick="showProgress()">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ProdName1") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtQty" runat="server" AutoPostBack="true" OnTextChanged="txtQty_TextChanged" Text='<%# getQty( Convert.ToInt32(Eval("MYPRODID")),Eval("UOM").ToString(),Convert.ToInt32(Eval("COMPID"))) %>' Style="width: 30px;"></asp:TextBox>
                                                                </td>
                                                                <%--Text='<%# Eval("Qty") %>' --%>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# getMSRP(Convert.ToInt32(Eval("MYPRODID")),Convert.ToInt32(Eval("UOM")),Convert.ToInt32(Eval("COMPID"))) %>'></asp:Label>
                                                                </td>
                                                                <%-- <td>  <asp:Label ID="Label5" runat="server"></asp:Label> </td>                                                               
                                                                <td>  <asp:Label ID="Label6" runat="server"></asp:Label> </td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%-- <div class="row">
                                <div class="col-md-12">

                                    <div class="portlet box yellow">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-cogs font-grey"></i>
                                                <span class="caption-subject font-grey bold uppercase">Sales Chapra
                                                    </span>
                                            </div>

                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                            <div class="actions btn-set">
                                               
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <asp:Label ID="Label8" runat="server" Text="Date :"></asp:Label>
                                            <asp:Label ID="lblDate_Sales_Chapra" runat="server" Text=""></asp:Label>

                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th></th>
                                                        <th># </th>
                                                        <th>Customer Name </th>
                                                        <th>Product #   </th>
                                                        <th>Qty Order </th>
                                                        <th>Amount </th>
                                                        <th>Confirmed Qty </th>
                                                        <th>Delivered Qty- Date </th>
                                                        <th>Action  </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="List_Chapra" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td> </td>
                                                                <td> <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label> </td>
                                                                <td> <asp:Label ID="Label1" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>  </td>
                                                                <td> <asp:Label ID="Label2" runat="server" Text='<%# Eval("Product") %>'></asp:Label>  </td>
                                                                <td> <asp:TextBox ID="txtQuty" runat="server" Text='<%# Eval("Qty") %>'></asp:TextBox> </td>
                                                                <td> <asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:TextBox> </td>
                                                                <td> <asp:Label ID="Label5" runat="server" Text='<%# Eval("ConfirmedQty") %>'></asp:Label>  </td>
                                                                <td> <asp:Label ID="Label6" runat="server" Text='<%# Eval("Delivered_Qty_Date ") %>'></asp:Label>  </td>
                                                                <td> <asp:LinkButton ID="lnkbtnItem" runat="server" CommandName="editDT" CommandArgument='<%# Eval("MYTRANSID") %>'>Save</asp:LinkButton></td>
                                                                
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-12">

                                    <div class="portlet box blue">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-cogs font-grey"></i>
                                                <span class="caption-subject font-grey bold uppercase">Sales Bakala
                                                    </span>
                                            </div>

                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                            <div class="actions btn-set">
                                               
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <asp:Label ID="Label10" runat="server" Text="Date :"></asp:Label>
                                            <asp:Label ID="lblDate_Sales_Bakala" runat="server" Text=""></asp:Label>

                                            <table class="table table-striped table-hover table-bordered" id="sample_2">
                                                <thead>
                                                    <tr>
                                                        <th></th>
                                                        <th># </th>
                                                        <th>Customer Name </th>
                                                        <th>Product #   </th>
                                                        <th>Qty Order </th>
                                                        <th>Amount </th>
                                                        <th>Confirmed Qty </th>
                                                        <th>Delivered Qty- Date </th>
                                                        <th>Action  </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="List_bakala" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td> </td>
                                                                <td> <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label> </td>
                                                                <td> <asp:Label ID="Label1" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>  </td>
                                                                <td> <asp:Label ID="Label2" runat="server" Text='<%# Eval("Product") %>'></asp:Label>  </td>
                                                                <td> <asp:TextBox ID="txtQrderQTY" runat="server" Text='<%# Eval("Qty") %>'></asp:TextBox></td>
                                                                <td> <asp:Label ID="Label4" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>  </td>
                                                                <td> <asp:Label ID="Label5" runat="server" Text='<%# Eval("ConfirmedQty") %>'></asp:Label>  </td>
                                                                <td> <asp:Label ID="Label6" runat="server" Text='<%# Eval("Delivered_Qty_Date ") %>'></asp:Label>  </td>
                                                                <td> <asp:LinkButton ID="lnkbtnItem" runat="server" CommandName="editDT" CommandArgument='<%# Eval("MYTRANSID") %>'>Save</asp:LinkButton></td>
                                                                
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="popup/browserLink" async="async"></script>
    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>

    <script src="../assets/global/scripts/metronic.js" type="text/javascript"></script>
    <script src="../assets/admin/layout3/scripts/layout.js" type="text/javascript"></script>
    <script src="../assets/admin/layout3/scripts/demo.js" type="text/javascript"></script>
    <script src="../assets/admin/pages/scripts/table-editable.js"></script>
    <script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Demo.init(); // init demo features
            TableEditable.init();
        });
    </script>
</asp:Content>
