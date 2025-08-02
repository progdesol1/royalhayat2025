<%@ Page Title="" Language="C#" MasterPageFile="~/POS/POSMaster.Master" AutoEventWireup="true" CodeBehind="POSWinSales.aspx.cs" Inherits="Web.POS.POSWinSales" %>

<%@ Register TagPrefix="ddlb" Assembly="OptionDropDownList" Namespace="OptionDropDownList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        .auto-style1 {
            height: 24px;
        }

        .auto-style2 {
            width: 15%;
            height: 24px;
        }
    </style>

    <script type="text/javascript">
        function ace_itemCoutry(sender, e) {
            var HiddenField3 = $get('<%= HiddenField3.ClientID %>');
            HiddenField3.value = e.get_value();
            var HiddenpaymentCustomer = $get('<%= HiddenpaymentCustomer.ClientID %>');
            HiddenpaymentCustomer.value = e.get_value();
        }
        function paymentCustomer(sender, e) {
            var HiddenpaymentCustomer = $get('<%= HiddenpaymentCustomer.ClientID %>');
            HiddenpaymentCustomer.value = e.get_value();
            var HiddenField3 = $get('<%= HiddenField3.ClientID %>');
            HiddenField3.value = e.get_value();
        }

        function backItemlink() {
            document.getElementById('ProductTab').className = "active";
            document.getElementById('PaymentTab').className = "";
        }

        function PaymentItemlink() {
            document.getElementById('ProductTab').className = "";
            document.getElementById('PaymentTab').className = "active";
        }

        //$(document).ready(function() {
        //    $('#ContentPlaceHolder1_txtPaymentCustomer').keyup(function(e) {
        //        var txtVal = $(this).val();
        //        $('#ContentPlaceHolder1_txtCustomer1').val(txtVal);
        //    });

        //    $('#ContentPlaceHolder1_txtCustomer1').keyup(function(e) {
        //        var txtVal = $(this).val();
        //        $('#ContentPlaceHolder1_txtPaymentCustomer').val(txtVal);
        //    });
        //});​


    </script>

    <style type="text/css">
        .wrap {
            white-space: normal;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-container">

        <!-- BEGIN CONTENT -->
        <asp:Panel ID="panelMsg" runat="server" Visible="false">
            <div class="alert alert-danger alert-dismissable">
                <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                <asp:Label ID="lblErreorMsg" runat="server"></asp:Label>
            </div>
        </asp:Panel>
        <asp:LinkButton ID="LinkButton4" Style="display: none;" runat="server">LinkButton</asp:LinkButton>
        <div id="HD" runat="server">
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
                    <div class="col-md-7">
                        <div class="tabbable tabbable-custom tabbable-noborder">
                            <ul class="nav nav-tabs">
                                <li class="active" id="ProductTab">
                                    <a href="#tab_1" data-toggle="tab">Product </a>
                                </li>
                                <li id="PaymentTab">
                                    <a href="#tab_2" data-toggle="tab">Payment </a>
                                </li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab_1">
                                    <%--Product--%>
                                    <!-- BEGIN FILTER -->
                                    <div class="row">
                                        <div class="col-md-12">
                                            <table border="0" class="table-responsive">

                                                <div class="scrollmenu">
                                                    <asp:LinkButton ID="btnCategoryall" runat="server" CssClass="btn red-haze" Text="All" OnClick="btnCategoryall_Click"></asp:LinkButton>
                                                    <asp:ListView ID="ListCategory" runat="server" OnItemCommand="ListCategory_ItemCommand" OnItemDataBound="ListCategory_ItemDataBound">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnCategory" runat="server" CssClass="btn" BackColor="Red" Font-Names="Courier New" ForeColor="Black" CommandName="DisplayCat" CommandArgument='<%# Eval("CATID")+"-"+Eval("CAT_NAME1") %>' Text='<%# Eval("CAT_NAME1") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>

                                            </table>
                                        </div>
                                    </div>
                                    <hr style="margin-top: 5px; margin-bottom: 5px;" />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnProdsearch">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label22" runat="server" class="col-md-4 control-label" Text="Search" Style="font-size: 15px;"></asp:Label>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtBarCode" runat="server" CssClass="form-control" Style="height: 30px;"></asp:TextBox>
                                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator13" runat="server" ErrorMessage="BarCode Required." ControlToValidate="txtBarCode" ValidationGroup="barcode"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Button ID="btnProdsearch" runat="server" ValidationGroup="barcode" CssClass="btn btn-sm yellow-gold" Text="SEARCH" OnClick="btnProdsearch_Click" />
                                                        </div>

                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group pull-right" style="padding-top: 5px; padding-right: 5px;">
                                                    <asp:CheckBox ID="chkWithImage" runat="server" Text="With Image" Checked="true" AutoPostBack="true" OnCheckedChanged="chkWithImage_CheckedChanged" />
                                                    <asp:CheckBox ID="Chkoutput" runat="server" Text="OutPut" Checked="true" AutoPostBack="true" OnCheckedChanged="Chkoutput_CheckedChanged" />
                                                    <asp:CheckBox ID="chkService" runat="server" Text="Service" AutoPostBack="true" OnCheckedChanged="chkService_CheckedChanged" />
                                                    <asp:CheckBox ID="ChkInput" runat="server" Text="Input" AutoPostBack="true" OnCheckedChanged="ChkInput_CheckedChanged" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="margin-top-10" style="height: 485px; overflow: scroll; border: 1px solid #F68D90;">

                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:ListView ID="Listview2" runat="server" OnItemCommand="Listview2_ItemCommand" OnItemDataBound="Listview2_ItemDataBound" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkProdAdd" Font-Underline="false" runat="server" CommandName="LnkProdAdd" CommandArgument='<%# Eval("product_name") +"~"+ Eval("product_id")+"~"+ Eval("UOMID") %>'>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-md-2 col-sm-6 mix category_1">
                                                                    <div class="mix-inner" style="height: 180px;">
                                                                        <asp:Image ID="lnkimg" class="img-responsive" Style="height: 100px; width: 138px;" ImageUrl="~/ECOMM/Upload/defolt.png" runat="server" />
                                                                        <div style="text-align: center;">
                                                                            <asp:Label ID="lblprodname" runat="server" Style="font-family: 'Courier New'; font-size: 14px; font-weight: bold;" Text='<%# Eval("product_id")+"-"+Eval("product_name")+"-"+ Eval("UOMID") %>' ToolTip='<%# Eval("product_name") %>'></asp:Label>
                                                                            <asp:Label ID="Prod" runat="server" Visible="false" Text='<%# Eval("product_id") %>'></asp:Label>
                                                                        </div>
                                                                        <%--<asp:Label ID="lblProdid" Visible="false" runat="server" Text='<%# Eval("MYPRODID") %>'></asp:Label>
                                                                        <asp:Label ID="lblPROUOM" Visible="false" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>--%>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel3" runat="server" Visible="false">
                                            <asp:ListView ID="Listview3" runat="server" OnItemCommand="Listview2_ItemCommand" OnItemDataBound="Listview3_ItemDataBound">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkProdAdd1" Font-Underline="false" runat="server" CommandName="LnkProdAdd" CommandArgument='<%# Eval("product_name") +"~"+ Eval("product_id")+"~"+ Eval("UOMID") %>'>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-md-2 col-sm-6 mix category_1">
                                                                    <div class="mix-inner" style="height: 120px;">
                                                                        <div style="text-align: center;">
                                                                            <asp:Label ID="lblPID" Visible="false" runat="server" Text='<%# Eval("product_id") %>'></asp:Label>
                                                                            <asp:Label ID="btnprodname" Style="height: 100px; font-family: Arial;" ForeColor="Black" BorderStyle="Solid" BorderWidth="1" BorderColor="Black" runat="server" CssClass="wrap" Text='<%# Eval("product_id")+"-"+Eval("product_name")+"-"+ Eval("UOMID") %>' ToolTip='<%# Eval("product_name") %>'></asp:Label>
                                                                            <%--<asp:Label ID="lblprodname" runat="server" Font-Size="12px" Text='<%# Eval("product_id")+"-"+Eval("product_name")+"-"+ Eval("UOMID") %>' ToolTip='<%# Eval("product_name") %>'></asp:Label>--%>
                                                                        </div>
                                                                        <%--<asp:Label ID="lblProdid" Visible="false" runat="server" Text='<%# Eval("MYPRODID") %>'></asp:Label>
                                                                        <asp:Label ID="lblPROUOM" Visible="false" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>--%>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>

                                    </div>
                                    <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label>
                                    <!-- END FILTER -->
                                </div>

                                <div class="tab-pane" id="tab_2">
                                    <%-- Payment--%>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="margin-top-10" style="height: 580px; overflow: scroll; border: 1px solid #F68D90;">

                                                <div class="row" style="padding: 20px;">
                                                    <div class="col-md-12">
                                                        <center>
                                                <asp:Label ID="Label12" runat="server" Text="Total Payable:"></asp:Label>
                                                <asp:Label ID="lblTotalpayableAmtPY" runat="server" Text=""></asp:Label>
                                                    </center>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-6" style="padding-left: 20px;">Paid Amount :</label>
                                                                <asp:Label ID="lblPaid" runat="server" Text="0"></asp:Label>
                                                                <div class="col-md-10">
                                                                    <asp:TextBox ID="txtPaidAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldtxtSHORTNAME" runat="server" ControlToValidate="txtPaidAmount" ErrorMessage="Paid Amount Required" CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-12" style="padding-left: 20px;">Customer  :</label>
                                                                <div class="col-md-10">
                                                                    <div class="input-icon right">
                                                                        <i class="fa fa-user"></i>
                                                                        <asp:TextBox ID="txtPaymentCustomer" class="form-control" runat="server" placeholder="Customer"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" TargetControlID="txtPaymentCustomer" ServiceMethod="GetCustomer" CompletionInterval="1000" EnableCaching="FALSE" CompletionSetCount="20"
                                                                            MinimumPrefixLength="1" OnClientItemSelected="paymentCustomer" DelimiterCharacters=";, :" FirstRowSelected="false"
                                                                            runat="server" />
                                                                        <asp:HiddenField ID="HiddenpaymentCustomer" runat="server" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPaymentCustomer" ErrorMessage="Customer Required" CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12">

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-12" style="padding-left: 20px;">Reference #</label>
                                                                <div class="col-md-10">
                                                                    <asp:TextBox ID="txtPayReffrance" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPayReffrance" ErrorMessage="Reference Required" CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-12" style="padding-left: 20px;">Pay by :</label>
                                                                <div class="col-md-10">
                                                                    <asp:DropDownList ID="drpPayBy" AutoPostBack="true" OnSelectedIndexChanged="drpPayBy_SelectedIndexChanged" CssClass="form-control select2me" runat="server">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-12" style="padding-left: 20px;">Change Amount :</label>
                                                                <div class="col-md-10">
                                                                    <asp:TextBox ID="txtChangeAmount" runat="server" Text="0" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtChangeAmount" ErrorMessage="Change Amount Required" CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-12" style="padding-left: 20px;">Due :</label>
                                                                <div class="col-md-10">
                                                                    <asp:TextBox ID="txtDueAmount" Text="0" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDueAmount" ErrorMessage="Due Amount Required" CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-12" style="padding-left: 20px;">Sales Date</label>
                                                                <div class="col-md-10">
                                                                    <asp:TextBox ID="txtSalesDate" placeholder="MM/dd/yyyy" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtSalesDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSalesDate" ErrorMessage="Sales Date Required" CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label class="control-label col-md-12" style="padding-left: 20px;">Comment:</label>
                                                                <div class="col-md-12">
                                                                    <asp:TextBox ID="txtCommant" Text="Done.." TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <%-- <a href="#tab_1" Class="btn blue-soft " onclick="backItemlink()" data-toggle="tab">Back </a>--%>
                                                                <%--<asp:Button ID="btnback" runat="server" CssClass="btn blue-soft " Text="Back" OnClientClick="backItem()" />--%>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Button ID="btnOnlySave" runat="server" CssClass="btn yellow-gold " Text="Only Save" OnClick="btnOnlySave_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <asp:Button ID="btnSaveAndPrint" runat="server" CssClass="btn green-soft " Text=" Complate and Print " OnClick="btnSaveAndPrint_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="table-scrollable">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                                        <thead>
                                                                            <tr role="row">

                                                                                <th class="auto-style1">
                                                                                    <asp:Label runat="server" ID="Label14" Text="invoice"></asp:Label>
                                                                                </th>
                                                                                <th class="auto-style1">
                                                                                    <asp:Label runat="server" ID="Label15" Text="Pay By"></asp:Label>
                                                                                </th>
                                                                                <th class="auto-style2">
                                                                                    <asp:Label runat="server" ID="Label16" Text="Reffrance NO"></asp:Label>
                                                                                </th>
                                                                                <th class="auto-style1">
                                                                                    <asp:Label runat="server" ID="Label17" Text="Amount"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label runat="server" ID="Label18" Text="Action"></asp:Label>
                                                                                </th>

                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>

                                                                            <asp:ListView ID="GridPayment" runat="server" OnItemCommand="GridPayment_ItemCommand">
                                                                                <ItemTemplate>
                                                                                    <tr role="row" class="odd">
                                                                                        <td>
                                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("invoice") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("payment_type") %>'></asp:Label>

                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label19" runat="server" Text='<%# Eval("Reffrance_NO") %>'></asp:Label>

                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("payment_amount") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="GridRemove" CommandArgument='<%# Eval("payment_type") %>'><i class="fa fa-close fa-2x"></i></asp:LinkButton></td>

                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>

                                                                        </tbody>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-5" style="overflow: scroll; height: 650px; padding: 0px">
                        <div class="tabbable tabbable-custom tabbable-noborder">

                            <div class="tab-content" style="margin-top: 40px;">
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

                                            <%-- <div class="actions btn-set">
                                                    <asp:Button ID="btnnewAdd" class="btn bg-yellow-gold" runat="server" Text="Add New" OnClick="btnnewAdd_Click" />
                                                </div>--%>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="list-group-item" style="padding: 5px 5px 5px 10px;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <asp:Label ID="Label9" runat="server" class="control-label col-md-3" Text="Invoice No."></asp:Label>
                                                            <asp:Label ID="invoiceID" runat="server" class="control-label col-md-3" Style="background-color: #f3f1f1;" Text=""></asp:Label>
                                                            <asp:Label ID="invoiceNo" runat="server" class="control-label col-md-4" Style="background-color: #f3f1f1; margin-left: 5px;" Text=""></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label class="control-label col-md-12" style="padding-left: 20px;">Customer</label>
                                                        <div class="col-md-10">
                                                            <div class="input-icon right">
                                                                <i class="fa fa-user"></i>
                                                                <asp:TextBox ID="txtCustomer1" class="form-control" runat="server" placeholder="Customer"></asp:TextBox>
                                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtCustomer1" ServiceMethod="GetCustomer" CompletionInterval="1000" EnableCaching="FALSE" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" OnClientItemSelected="ace_itemCoutry" DelimiterCharacters=";, :" FirstRowSelected="false"
                                                                    runat="server" />
                                                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-1" style="padding-left: 0px; padding-right: 0px; padding-top: 4px;">
                                                            <%--<a data-toggle="modal" href="#responsive">
                                                                <img src="../Purchase/images/plus.png" />
                                                            </a>--%>
                                                            <asp:LinkButton ID="libtnNewClass" runat="server">   <img src="../Purchase/images/plus.png" /> </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label class="control-label col-md-12" style="padding-left: 20px;">Order Way</label>
                                                        <div class="col-md-10">
                                                            <asp:DropDownList ID="drpOrderWay" CssClass="form-control select2me" runat="server"></asp:DropDownList>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <%--  <div class="row">
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
                                                           
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="Label1" runat="server" class="col-md-6 control-label" Text="Invoice No"></asp:Label>
                                                    <asp:Label ID="InvoiceNO" runat="server"></asp:Label>
                                                </div>
                                            </div>--%>

                                            <%--<div class="row">
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
                                            </div>--%>


                                            <div class="table-scrollable">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                            <thead>
                                                                <tr role="row">

                                                                    <th class="auto-style1">
                                                                        <asp:Label runat="server" ID="lblContractID" Text="ITEM NAME"></asp:Label>
                                                                    </th>
                                                                    <th class="auto-style1">
                                                                        <asp:Label runat="server" ID="Label6" Text="Price"></asp:Label>
                                                                    </th>
                                                                    <th class="auto-style2">
                                                                        <asp:Label runat="server" ID="Label7" Text="Qty"></asp:Label>
                                                                    </th>
                                                                    <th class="auto-style1">
                                                                        <asp:Label runat="server" ID="Label8" Text="Total"></asp:Label>
                                                                    </th>
                                                                    <th class="auto-style1">
                                                                        <asp:Label runat="server" ID="Label30" Text="Batch"></asp:Label>
                                                                    </th>
                                                                    <th colspan="3" class="auto-style1">
                                                                        <asp:Label runat="server" ID="Label11" Text="Action"></asp:Label>
                                                                    </th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>

                                                                <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" OnItemDataBound="Listview1_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr role="row" class="odd">

                                                                            <td>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("product_id")+"-"+Eval("product_name")+"-"+Eval("UOMID") %>'></asp:Label>
                                                                                <asp:Label ID="lblproduct" Visible="false" runat="server" Text='<%# Eval("product_id") %>'></asp:Label>
                                                                                <asp:Label ID="lblUOM" Visible="false" runat="server" Text='<%# Eval("UOMID") %>'></asp:Label>

                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("msrp") %>'></asp:Label>
                                                                                <asp:Label ID="lblpdiscound" Visible="false" runat="server" Text='<%# Eval("Discount") %>'></asp:Label>
                                                                                <asp:Label ID="lblptaxapply" Visible="false" runat="server" Text='<%# Eval("taxapply") %>'></asp:Label>

                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" Text='<%# Eval("OpQty") %>' OnTextChanged="txtQty_TextChanged" Style="height: 25px; padding-left: 8px; padding-right: 8px; padding-top: 3px; padding-bottom: 3px;" AutoPostBack="True"></asp:TextBox>

                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblBatchNO" runat="server" Text='<%# Eval("BatchNo") %>'></asp:Label>
                                                                            </td>

                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="ProdPlus" CommandArgument='<%# Eval("product_id")+"-"+Eval("UOMID")+"-"+Eval("BatchNo") %>'><i class="fa fa-plus fa-2x"></i></asp:LinkButton></td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="ProdMinus" CommandArgument='<%# Eval("product_id")+"-"+Eval("UOMID")+"-"+Eval("BatchNo") %>'><i class="fa fa-minus fa-2x"></i></asp:LinkButton></td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="ProdRemove" CommandArgument='<%# Eval("product_id")+"-"+Eval("UOMID")+"-"+Eval("BatchNo") %>'><i class="fa fa-close fa-2x"></i></asp:LinkButton></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>

                                                            </tbody>
                                                            <%-- <tfoot>
                                                                <tr role="row">
                                                                 
                                                                    <th>
                                                                        <asp:Label runat="server" ID="Label29" Text="Final Total"></asp:Label>
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
                                                                   
                                                                </tr>
                                                            </tfoot>--%>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div>
                                                <table class="table table-striped table-bordered table-hover dataTable no-footer">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 25%;">Total:</td>
                                                            <td colspan="2" style="padding-left: 5px;">
                                                                <asp:Label ID="FTotall" runat="server" Text="0"></asp:Label>
                                                            </td>
                                                            <td style="width: 30%;">Discount in 0.5 or 0.5%
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25%;">Discount:</td>
                                                            <td style="padding-left: 5px; width: 20%;">
                                                                <asp:Label ID="lblDiscount" runat="server" Text="0"></asp:Label>
                                                            </td>
                                                            <td style="padding-left: 5px; width: 20%;">+
                                                                <asp:Label ID="lblinvoiceDis" runat="server" Text="0"></asp:Label>
                                                                <asp:Label ID="lblInvoiceDisPer" Visible="false" runat="server" Text="0"></asp:Label>
                                                            </td>
                                                            <%--<td style="width: 30%;">
                                                                <asp:TextBox ID="txtSidPoint" CssClass="form-control" runat="server" Text="0" AutoPostBack="true" OnTextChanged="txtSidPoint_TextChanged" Style="height: 25px; padding-left: 8px; padding-right: 8px; padding-top: 3px; padding-bottom: 3px;"></asp:TextBox>
                                                            </td>--%>
                                                            <td style="width: 30%;">
                                                                <div class="input-icon right">
                                                                    <%--<i style="margin: 0px 0px 0px 0px;">0.5 or 0.5%</i>--%>
                                                                    <asp:TextBox ID="txtDisPercent" CssClass="form-control" runat="server" placeholder=" 0.5 or 0.5%" Text="0" AutoPostBack="true" OnTextChanged="txtDisPercent_TextChanged" Style="height: 25px; padding-left: 8px; padding-right: 8px; padding-top: 3px; padding-bottom: 3px;"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25%;">Sub Total:</td>
                                                            <td colspan="2" style="padding-left: 5px;">
                                                                <asp:Label ID="FsubTotal" runat="server" Text="0"></asp:Label>
                                                            </td>
                                                            <td style="width: 30%;"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25%;">Tax Rate:</td>
                                                            <td colspan="2" style="padding-left: 5px;">
                                                                <asp:Label ID="lblTax" runat="server" Text="0"></asp:Label>
                                                            </td>
                                                            <td style="width: 30%; padding-left: 5px;">
                                                                <asp:Label ID="lblTaxPercent" runat="server" Text="0"></asp:Label>&nbsp;%
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25%;">Delivery</td>
                                                            <td colspan="2" style="padding-left: 5px;">
                                                                <asp:Label ID="lblDelivery" runat="server" Text="0"></asp:Label>
                                                            </td>
                                                            <td style="width: 30%;"></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div style="border: 1px solid #d8d1d1; padding: 5px 0px 5px 0px;">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group ">
                                                            <div class="col-md-4">
                                                                <div class="col-md-12" style="padding: 0px 0px 0px 0px; text-align: center;">
                                                                    <asp:Label ID="Label3" runat="server" Text="Total Payable" Style="color: #a94442; font-weight: bold; font-family: 'Courier New'; font-size: 14px;"></asp:Label>
                                                                </div>
                                                                <div class="col-md-12" style="padding: 0px 0px 0px 0px; text-align: center;">
                                                                    <asp:Label ID="lblPayable" runat="server" Text="0"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="col-md-12" style="padding: 0px 0px 0px 0px; text-align: center;">
                                                                    <asp:Label ID="Label10" runat="server" Text="Cash Received" Style="color: #a94442; font-family: 'Courier New'; font-size: 14px;"></asp:Label>
                                                                </div>
                                                                <div class="col-md-12" style="padding: 0px 0px 0px 0px; text-align: center;">
                                                                    <asp:TextBox ID="txtcashReceived" CssClass="form-control text-center" runat="server" Style="height: 25px;" Text="0" AutoPostBack="true" OnTextChanged="txtcashReceived_TextChanged"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="col-md-12" style="padding: 0px 0px 0px 0px; text-align: center;">
                                                                    <asp:Label ID="Label13" runat="server" Text="Change Amount" Style="color: #a94442; font-family: 'Courier New'; font-size: 14px;"></asp:Label>
                                                                </div>
                                                                <div class="col-md-12" style="padding: 0px 0px 0px 0px; text-align: center;">
                                                                    <asp:Label ID="lblChangeAMT" runat="server" Text="0"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="actions btn-set text-center">
                                                <div class=" btn-sm">
                                                    <a href="#tab_2" class="btn blue-soft btn-md" onclick="PaymentItemlink()" data-toggle="tab">Payment </a>
                                                    <%--<asp:Button ID="btnPayment" runat="server" CssClass="btn blue-soft btn-md" Text="Payment" OnClientClick="PaymentItem()" />--%>
                                                    <asp:Button ID="btnDRAFT" runat="server" CssClass="btn yellow btn-md" Text="Draft" OnClick="btnDRAFT_Click" />
                                                    <asp:Button ID="btnCOD" runat="server" CssClass="btn green-soft btn-md" Text="COD" OnClick="btnCOD_Click" />
                                                    <asp:Button ID="btnCash" runat="server" CssClass="btn purple-soft btn-md" Text="Cash" OnClick="btnCash_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn red btn-md" Text="Cancel" OnClick="btnCancel_Click" />
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

            <asp:LinkButton ID="btnpopPeri" runat="server" Text="."></asp:LinkButton>

            <asp:Panel ID="PanelConfirmmsg" runat="server" Style="display: none; height: 75%; overflow: auto">
                <div class="row">
                    <%--style="position: fixed; left: 5%; width: 90%; top: 1%;"--%>
                    <div class="col-md-12">

                        <div class="portlet-body">
                            <div class="portlet box yellow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>
                                        <asp:Label runat="server" ID="Label20" Style="width: 252px; padding-top: 0px;" ForeColor="White" class="col-md-4 control-label" Text="Perishable"></asp:Label>

                                    </div>
                                    <div class="actions btn-set">
                                        <asp:Button ID="Button6" runat="server" class="btn red" Text="Cancel" />
                                    </div>

                                </div>
                                <div id="Div2" runat="server" class="portlet-body">
                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general">

                                                <div class="portlet-body">

                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="Label33" Text="Product ID :-  "></asp:Label>
                                                                <asp:Label runat="server" ID="lblPRodIDper" Text="-"></asp:Label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="Label34" Text="ProductName :-  "></asp:Label>
                                                                <asp:Label runat="server" ID="lblProdNamePEr" Text="-"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="Label35" Text="UOM :-  "></asp:Label>
                                                                <asp:Label runat="server" ID="lblUOMName" Text="-"></asp:Label>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead class="repHeader">
                                                            <tr>
                                                                <th class="auto-style1">
                                                                    <asp:Label runat="server" ID="Label21" Text="Batch No"></asp:Label>
                                                                </th>
                                                                <th class="auto-style1">
                                                                    <asp:Label runat="server" ID="Label23" Text="On Hand"></asp:Label>
                                                                </th>
                                                                <th class="auto-style2">
                                                                    <asp:Label runat="server" ID="Label24" Text="Product Date"></asp:Label>
                                                                </th>
                                                                <th class="auto-style1">
                                                                    <asp:Label runat="server" ID="Label25" Text="Expiry Date"></asp:Label>
                                                                </th>
                                                                <th class="auto-style1">
                                                                    <asp:Label runat="server" ID="Label27" Text="Lead Days TO Destroy"></asp:Label>
                                                                </th>
                                                                <th>
                                                                    <asp:Label runat="server" ID="Label26" Text="Action"></asp:Label>
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:ListView ID="ListView4" runat="server" OnItemCommand="ListView4_ItemCommand" OnSelectedIndexChanging="ListView4_SelectedIndexChanging">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblBatch" runat="server" Text='<%# Eval("BatchNo") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblONHAND" runat="server" Text='<%# Eval("OnHand") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblProdDate" runat="server" Text='<%# Eval("ProdDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblExpiryDate" runat="server" Text='<%# Eval("ExpiryDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblLeadDays2Destroy" runat="server" Text='<%# Eval("LeadDays2Destroy") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-green" Text="Select" CommandName="Select" CommandArgument='<%# Eval("MyProdID")+"-"+ Eval("UOM")+"-"+ Eval("BatchNo")+"-"+ Eval("OnHand") %>'> Select</asp:LinkButton>
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="form-actions noborder">

                                                    <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
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
            <cc1:ModalPopupExtender ID="ModalPopupExtender8" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button6" Enabled="True" PopupControlID="PanelConfirmmsg" TargetControlID="btnpopPeri"></cc1:ModalPopupExtender>


        </div>

    </div>



    <asp:Panel ID="pnlcuste" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto;">

        <div class="modal-dialog" style="margin-top: 0px; margin-bottom: 0px;">
            <div class="modal-content">
                <div class="portlet box purple">
                    <div class="modal-header">
                        <h4 class="modal-title" style="color: white;">Add New Customer</h4>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="scroller" style="height: 350px" data-always-visible="1" data-rail-visible1="1">
                        <div class="row">

                            <div class="col-md-12">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <p>
                                            <strong style="font-size: larger; margin-left: 10px;">Name ENG<span>* </span></strong>
                                            <asp:TextBox ID="txtCustENG" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCustENG_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Cust" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtCustENG" ErrorMessage="Customer Name ENG required"></asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                            <strong style="font-size: larger; margin-left: 10px;">Name ARB<span>* </span></strong>
                                            <asp:TextBox ID="txtCustARB" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="Cust" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtCustARB" ErrorMessage="Customer Name ARB required"></asp:RequiredFieldValidator>
                                        </p>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <p>
                                    <strong style="font-size: larger; margin-left: 10px;">Phone<span>* </span></strong>
                                    <asp:TextBox ID="txtCustPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtCustPhone" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="Cust" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtCustPhone" ErrorMessage="Customer Phone required"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <strong style="font-size: larger; margin-left: 10px;">City<span>* </span></strong>
                                    <asp:DropDownList ID="DRPCity1" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                    <%--<ddlb:OptionGroupSelect ID="DRPCity" runat="server" EnableViewState="false" Style="display: block; width: 50% !important; height: 34px; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; color: #555; background-color: #fff; background-image: none; border: 1px solid #ccc;" />--%>
                                    <%--<asp:TextBox ID="txtCustCity" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="Cust" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtCustCity" ErrorMessage="Customer City required"></asp:RequiredFieldValidator>--%>
                                </p>
                                <p>
                                    <strong style="font-size: larger; margin-left: 10px;">Address<span>* </span></strong>
                                    <asp:TextBox ID="txtCustAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Cust" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtCustAddress" ErrorMessage="Address required"></asp:RequiredFieldValidator>
                                </p>

                            </div>

                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="background-color: lightgray;">
                    <asp:LinkButton ID="btncustCancel" runat="server" CssClass="btn btn-sm red" Text="Cancel"></asp:LinkButton>
                    <asp:LinkButton ID="Linkcust" class="btn btn-sm green" runat="server" ValidationGroup="Cust" OnClick="Linkcust_Click">save</asp:LinkButton>

                </div>
            </div>
        </div>

    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="btncustCancel" Enabled="True" PopupControlID="pnlcuste" TargetControlID="libtnNewClass"></cc1:ModalPopupExtender>

    <%--<div id="responsive" class="modal fade" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog" style="margin-top: 0px;margin-bottom: 0px;">
            <div class="modal-content">
                <div class="portlet box purple">
                    <div class="modal-header">

                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title" style="color: white;">Add New Customer</h4>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="scroller" style="height: 300px" data-always-visible="1" data-rail-visible1="1">
                        <div class="row">

                            <div class="col-md-12">
                                <p>
                                    <strong style="font-size: larger; margin-left: 10px;">Name ENG<span>* </span></strong>
                                    <asp:TextBox ID="txtCustENG" runat="server" CssClass="form-control" Text=""></asp:TextBox>                                   
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Cust" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtCustENG" ErrorMessage="Customer Name ENG required"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <strong style="font-size: larger; margin-left: 10px;">Name ARB<span>* </span></strong>
                                    <asp:TextBox ID="txtCustARB" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="Cust" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtCustARB" ErrorMessage="Customer Name ARB required"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <strong style="font-size: larger; margin-left: 10px;">Phone<span>* </span></strong>
                                    <asp:TextBox ID="txtCustPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="Cust" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtCustPhone" ErrorMessage="Customer Phone required"></asp:RequiredFieldValidator>
                                </p>
                                 <p>
                                    <strong style="font-size: larger; margin-left: 10px;">City<span>* </span></strong>
                                    <asp:TextBox ID="txtCustCity" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="Cust" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtCustCity" ErrorMessage="Customer City required"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <strong style="font-size: larger; margin-left: 10px;">Address<span>* </span></strong>
                                    <asp:TextBox ID="txtCustAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Cust" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtCustAddress" ErrorMessage="Address required"></asp:RequiredFieldValidator>
                                </p>

                            </div>
                        </div>


                    </div>
                </div>
                <div class="modal-footer" style="background-color: lightgray;">
                    <button type="button" data-dismiss="modal" class="btn btn-sm red">Close</button>
                    <asp:LinkButton ID="Linkcust" class="btn btn-sm green" runat="server" ValidationGroup="Cust" OnClick="Linkcust_Click">save</asp:LinkButton>
                    
                </div>
            </div>
        </div>
    </div>--%>

    <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto; top: 100px;">
        <div class="modal-dialog modal-sm" style="margin-bottom: 0px; margin-top: 0px;">
            <div class="modal-content">
                <div class="portlet box red">
                    <div class="modal-header" style="padding: 8px 8px 8px 8px;">
                        <h4 class="modal-title" style="color: white;"><i class="fa fa-warning"></i>&nbsp;Warning</h4>
                    </div>
                </div>
                <div class="modal-body" style="padding-bottom: 30px; padding-top: 30px;">
                    <p id="lblmsgpop" style="text-align: center; font-family: 'Courier New'; font-size: 15px; font-weight: bolder;">Please Select Customer</p>
                </div>
                <div class="modal-footer" style="padding-top: 10px; padding-bottom: 10px; background-color: #f2f2f2;">
                    <asp:LinkButton ID="LinkButton5" runat="server" CssClass="btn btn-sm red" Text="Cancel"></asp:LinkButton>
                </div>
            </div>
        </div>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="" CancelControlID="LinkButton5" Enabled="True" PopupControlID="Panel4" TargetControlID="LinkButton4"></cc1:ModalPopupExtender>


</asp:Content>
