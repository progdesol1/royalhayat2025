<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="Sales_Quotations.aspx.cs" Inherits="Web.Sales.Sales_Quotations" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ECOMM/UserControl/items.ascx" TagPrefix="uc1" TagName="items" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function abc(obj) {

            debugger
            var q = $(obj).closest('tr').find('#txtq').val();
            var p = $(obj).closest('tr').find('#txtp').val();
            var t = $(obj).closest('tr').find('#txtt').val();
            var s = parseInt(q) - parseInt(p);
            $(obj).closest('tr').find('#txtt').val(s);
        }
    </script>
     <style>
       
      .aspNetDisabled btn btn-icon-only green{
           cursor: not-allowed !important;

       }
         .aspNetDisabled{
             cursor: not-allowed !important;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function getcurrency() {
            var ddlSupp = document.getElementById("ContentPlaceHolder1_ddlSupplier");
            var ddlSuppV = ddlSupp.options[ddlSupp.selectedIndex].value;
            var ddlLf = document.getElementById("ContentPlaceHolder1_ddlLocalForeign");
            var ddlLFT = ddlLf.options[ddlLf.selectedIndex].text;
            if (ddlLFT == "Foreign") {
                $(document).ready(function () {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Purchase_Order.aspx/getCurrency",
                        data: "{Param1: '" + ddlSuppV + "'}",
                        dataType: "json",
                        async: true,
                        cache: false,
                        success: OnSuccessCurr,
                        error: function (result) {
                        }
                    });
                });
            }
            else
                return false;
        }
        function OnSuccessCurr(result) {
            if (result.d != null || result.d != "") {
                var ddlCurr = document.getElementById("ContentPlaceHolder1_ddlCurrency");
                var ddlCurrCount = ddlCurr.options.length;
                for (var i = 0; i < ddlCurrCount; i++) {
                    if (ddlCurr.options[i].value == result.d) {
                        ddlCurr.selected = true;
                        //document.getElementById("ContentPlaceHolder1_ddlCurrency").selectedIndex = i;
                        ddlCurr.options[i].selected = true;
                        break;
                    }
                }
            }
        }
        function getLocalPrize() {
            var e = document.getElementById("ContentPlaceHolder1_ddlCurrency");
            var Param1 = e.options[e.selectedIndex].value;
            var Param2 = document.getElementById("ContentPlaceHolder1_txtUPriceForeign").value;
            var ddlSupp = document.getElementById("ContentPlaceHolder1_ddlSupplier");
            var Param3 = ddlSupp.options[ddlSupp.selectedIndex].value;
            if (Param3 == "0") {
                document.getElementById("ContentPlaceHolder1_txtUPriceForeign").value = "";
                alert("Please select the supplier.");
                return false;
            }
            else if (Param1 == "0") {
                document.getElementById("ContentPlaceHolder1_txtUPriceForeign").value = "";
                alert("Please select the currency.");
                return false;
            }
            else {
                $(document).ready(function () {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Purchase_Order.aspx/getLocalPriForeign",
                        data: "{Param1: '" + Param1 + "',Param2: '" + Param2 + "',Param3: '" + Param3 + "'}",
                        dataType: "json",
                        success: OnSuccess,
                        error: function (result) {
                        }
                    });
                });
            }
        }
        function OnSuccess(result) {

            document.getElementById("ContentPlaceHolder1_txtUPriceLocal").value = result.d;
            document.getElementById("ContentPlaceHolder1_hidUPriceLocal").value = result.d;
            var varres = result.d;
            var vardis = parseFloat(document.getElementById("ContentPlaceHolder1_txtDiscount").value);
            var vartax = parseFloat(document.getElementById("ContentPlaceHolder1_txtTax").value);
            var varqty = parseInt(document.getElementById("ContentPlaceHolder1_txtQuantity").value);
            var varUPF = parseFloat(document.getElementById("ContentPlaceHolder1_txtUPriceForeign").value);

            var varMulQUPF = varqty * varUPF;
            var varMulQUPL = varres * varqty;

            var varTotWithDisF = varMulQUPF - (varMulQUPF * (vardis / 100));
            var varTotWithDisL = varMulQUPL - (varMulQUPL * (vardis / 100));


            var varTotWithDisTaxF = varTotWithDisF + (varTotWithDisF * (vartax / 100));
            var varTotWithDisTaxL = varTotWithDisL + (varTotWithDisL * (vartax / 100));

            if (isNaN(vardis)) { vardis = 0; }
            if (isNaN(vartax)) { vartax = 0; }
            if (isNaN(varqty)) { varqty = 1; }
            if (isNaN(varTotWithDisTaxF)) { varTotWithDisTaxF = 0; }
            if (isNaN(varTotWithDisTaxL)) { varTotWithDisTaxL = 0; }


            document.getElementById('ContentPlaceHolder1_txtTotalCurrencyLocal').value = varTotWithDisTaxL.toFixed(2);
            document.getElementById('ContentPlaceHolder1_hidTotalCurrencyLocal').value = varTotWithDisTaxL.toFixed(2);
            document.getElementById('ContentPlaceHolder1_txtDiscount').value = vardis.toFixed(2);
            document.getElementById('ContentPlaceHolder1_txtQuantity').value = varqty;
            document.getElementById('ContentPlaceHolder1_txtTax').value = vartax.toFixed(2);
            document.getElementById('ContentPlaceHolder1_txtTotalCurrencyForeign').value = varTotWithDisTaxF.toFixed(2);
        }


        function getEmail() {
            var ddlSupp = document.getElementById("ContentPlaceHolder1_ddlSupplier");
            var ddlSuppV = ddlSupp.options[ddlSupp.selectedIndex].value;
            if (ddlSuppV != "0") {
                $(document).ready(function () {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Purchase_Order.aspx/getEmail",
                        data: "{Param2: '" + ddlSuppV + "'}",
                        dataType: "json",
                        async: true,
                        cache: false,
                        success: function (result) {
                            document.getElementById('ContentPlaceHolder1_txtRecipient').value = result.d;
                        },
                        error: function (result) {
                        }
                    });
                });
            }
            else {
                return false;
            }
        }
        function setSelectedIndex(s, i) {
            //Drop Down Selected Index Set Method
            s.options[i - 1].selected = true;
            return;
        }
        function OnSuccess1(result) {

            var e = document.getElementById("ContentPlaceHolder1_ddlLocalForeign");
            var Param1 = e.options[e.selectedIndex].text;
            var e2 = document.getElementById("ContentPlaceHolder1_ddlProduct");
            var Param2 = e2.options[e2.selectedIndex].value;
            var varDesc = e2.options[e2.selectedIndex].text;
            var result1 = new Array();
            result1 = result.d;

            if (Param1 == "Foreign") {
                //document.getElementById("ContentPlaceHolder1_txtUOM").value = result1[1];
                document.getElementById("ContentPlaceHolder1_txtUPriceForeign").disabled = false;
                document.getElementById("ContentPlaceHolder1_txtUPriceLocal").disabled = true;
                document.getElementById("ContentPlaceHolder1_txtDiscount").value = "0.00";
                document.getElementById("ContentPlaceHolder1_txtTax").value = "0.00";
                document.getElementById("ContentPlaceHolder1_txtTotalCurrencyLocal").value = "0.00";
                document.getElementById("ContentPlaceHolder1_hidTotalCurrencyLocal").value = "0.00";
                document.getElementById("ContentPlaceHolder1_txtTotalCurrencyForeign").value = "0.00";
                document.getElementById("ContentPlaceHolder1_txtDescription").value = varDesc;
                document.getElementById("ContentPlaceHolder1_txtQuantity").value = 1;
            }
            else if (Param1 == "Local") {
                //document.getElementById("ContentPlaceHolder1_txtUOM").value = result1[1];
                document.getElementById("ContentPlaceHolder1_txtUPriceLocal").value = result1[6];
                document.getElementById("ContentPlaceHolder1_hidUPriceLocal").value = result1[6];
                document.getElementById("ContentPlaceHolder1_txtTotalCurrencyLocal").value = result1[6];
                document.getElementById("ContentPlaceHolder1_hidTotalCurrencyLocal").value = result1[6];
                document.getElementById("ContentPlaceHolder1_txtDiscount").value = "0.00";
                document.getElementById("ContentPlaceHolder1_txtTax").value = "0.00";
                document.getElementById("ContentPlaceHolder1_txtQuantity").value = 1;
                document.getElementById("ContentPlaceHolder1_txtDescription").value = varDesc;
            }

            if (result1[3] == "True" || result1[4] == "True") {

                document.getElementById("ContentPlaceHolder1_divItem").style.display = "none";
                // document.getElementById("ContentPlaceHolder1_anchorModelPopUp").style.display = "block";
                document.getElementById("ContentPlaceHolder1_hidMColor").value = "True";
            }
            else {
                document.getElementById("ContentPlaceHolder1_divItem").style.display = "block";
                //  document.getElementById("ContentPlaceHolder1_anchorModelPopUp").style.display = "none";
                document.getElementById("ContentPlaceHolder1_hidMColor").value = "False";
            }
            var eUOM = document.getElementById("ContentPlaceHolder1_ddlUOM");
            for (var i = 0; i < eUOM.options.length; i++) {
                if (eUOM.options[i].value == result1[1]) {
                    eUOM.options[i].selected = true;
                    break;
                }
            }
            if (result1[5] == "True") {
                document.getElementById("ContentPlaceHolder1_ddlUOM").disabled = false;
            }
            else
                document.getElementById("ContentPlaceHolder1_ddlUOM").disabled = true;
            document.getElementById("ContentPlaceHolder1_hidUOMId").value = result1[1];
            document.getElementById("ContentPlaceHolder1_hidUOMText").value = result1[2];

        }

        function getProInfoMCS() {
            var e2 = document.getElementById("ContentPlaceHolder1_ddlProductMCS");
            var Param2 = e2.options[e2.selectedIndex].value;
            $(document).ready(function () {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Purchase_Order.aspx/getProInfoMCS",
                    data: "{Param2: '" + Param2 + "'}",
                    dataType: "json",
                    success: OnSuccessProInfoMCS,
                    error: function (result) {
                    }
                });
            });
        }
        function OnSuccessProInfoMCS(result) {
            var e = document.getElementById("ContentPlaceHolder1_ddlLocalForeign");
            var Param1 = e.options[e.selectedIndex].text;
            var e2 = document.getElementById("ContentPlaceHolder1_ddlProduct");
            var Param2 = e2.options[e2.selectedIndex].value;
            var varDesc = e2.options[e2.selectedIndex].text;
            var result1 = new Array();
            result1 = result.d;
            //if (result1[0] == "True") {
            //    document.getElementById("ContentPlaceHolder1_ddlItemColor").style.display = "block";
            //    document.getElementById("ContentPlaceHolder1_lblQuntiMC").innerHTML = result1[3];
            //}
            //else {
            //    document.getElementById("ContentPlaceHolder1_ddlItemColor").style.display = "none";
            //}
            //if (result1[1] == "True") {
            //    document.getElementById("ContentPlaceHolder1_ddlItemSize").style.display = "block";
            //    document.getElementById("ContentPlaceHolder1_lblQuntiMC").innerHTML = result1[3];
            //}
            //else {
            //    document.getElementById("ContentPlaceHolder1_ddlItemSize").style.display = "none";
            //}

        }
        function UniPriED() {
            document.getElementById("ContentPlaceHolder1_txtUPriceForeign").disabled = false;
            document.getElementById("ContentPlaceHolder1_txtUPriceLocal").disabled = true;
        }
    </script>
    <script src="PrductJs/CommonPurchase.js"></script>
    <div  id="b" runat="server">
        <div class="page-head">
            <ol class="breadcrumb">
                <li><a href="../ECOMM/AdminIndex.aspx">
                    <asp:Label ID="lblDisPro" runat="server" Text="Home"></asp:Label></a><i class="fa fa-circle"></i></li>
                <li><a href="#">
                    <asp:Label ID="lblDisProMst" runat="server" Text="Sales_Quotations"></asp:Label></a></li>

            </ol>

        </div>
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box blue-hoki">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Sales Quotations
                                    </div>
                                    <div class="tools">
                                        <a id="shlinkProductDetails" runat="server" href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                    </div>
                                </div>
                                <div id="shProductDetails" runat="server" class="portlet-body" style="padding-left: 25px;">
                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general">
                                               
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblLocalForeign" class="col-md-4 control-label" runat="server" Text="Local/Foreign"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlLocalForeign" CssClass="form-control select2me" runat="server" onchange="selectcurrency()"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblOrderDate" class="col-md-4 control-label" runat="server" Text="Date of Transaction"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtOrderDate" CssClass="form-control"
                                                                        runat="server" placeholder="Transaction Date" onchange="checkdate(this)"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtOrderDate" Format="dd/MM/yyyy" Enabled="True">
                                                                    </cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblSupplier" class="col-md-4 control-label" runat="server" Text="Supplier"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlSupplier" CssClass="form-control select2me" runat="server" onchange="getcurrency(this);"></asp:DropDownList>
                                                                    <asp:LinkButton ID="lnkbtnSupp" runat="server" Visible="false"></asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">

                                                                <asp:Label ID="lblCurrency" class="col-md-4 control-label" runat="server" Text="Currency"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlCurrency" CssClass="form-control select2me" Enabled="false" runat="server"></asp:DropDownList>
                                                                    <%--<asp:LinkButton ID="lbtnCR"  runat="server" Text="Currency Rate"></asp:LinkButton>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblBatchNo" class="col-md-4 control-label" runat="server" Text="Batch No."></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtBatchNo" CssClass="form-control" runat="server" placeholder="Batch Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblProjectNo" class="col-md-4 control-label" runat="server" Text="Project No."></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlProjectNo" runat="server" CssClass="form-control select2me">
                                                                    </asp:DropDownList>
                                                                    <%--<asp:TextBox AutoCompleteType="Disabled" ID="txtProjectNo" CssClass="form-control" runat="server" placeholder="Project Number"></asp:TextBox>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblRef" class="col-md-4 control-label" runat="server" Text="Reference"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtRef" CssClass="form-control" runat="server" placeholder="Supplier Reference"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label ID="Label4" class="col-md-4 control-label" runat="server" Text="Transaction No."></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtTraNoHD" ReadOnly="true" CssClass="form-control" runat="server" placeholder="Transaction Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="Label11" class="col-md-4 control-label" runat="server" Text="CRM Activity"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlCrmAct" runat="server" CssClass="form-control select2me">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">

                                                                <asp:Label ID="lblNotes" class="col-md-4 control-label" runat="server" Text="Notes"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtNoteHD" CssClass="form-control" runat="server" placeholder="Notes" TextMode="SingleLine" MaxLength="550"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="Label2" class="col-md-4 control-label" runat="server" Text="Overhead Cost"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtOHCostHD" ReadOnly="true" CssClass="form-control" runat="server" placeholder="Overhead Cost"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="tab-container">
                                                        <ul class="nav nav-tabs">
                                                            <li class="active" runat="server" id="liOHCost"><a href="#ContentPlaceHolder1_step1" data-toggle="tab">
                                                                <asp:Label ID="lblDOHC" runat="server" Text="1. Overhead Cost"></asp:Label></a></li>
                                                            <li id="liItems" runat="server"><a href="#ContentPlaceHolder1_step2" data-toggle="tab">
                                                                <asp:Label ID="lblITems" runat="server" Text="2. Items"></asp:Label></a></li>
                                                        </ul>
                                                        <div class="tab-content">
                                                            <div class="tab-pane active cont" id="step1" runat="server">
                                                                <asp:Panel ID="Panel1" runat="server">
                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <div class="col-md-12">
                                                                                <div class="block-flat">
                                                                                    <div class="content">
                                                                                        <div class="table-responsive">
                                                                                            <table class="table table-bordered" id="datatable">
                                                                                                <thead class="repHeader">
                                                                                                    <tr>
                                                                                                        <th>
                                                                                                            <asp:Label ID="Label5" runat="server" Text="#"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label ID="Label6" runat="server" Text="Overhead Cost"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label ID="Label7" runat="server" Text="Overhead Amount"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label ID="Label8" runat="server" Text="Notes"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label ID="Label9" runat="server" Text="Account ID"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label ID="Label10" runat="server" Text="Add"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label ID="Label12" runat="server" Text="Delete"></asp:Label></th>
                                                                                                    </tr>
                                                                                                </thead>
                                                                                                <tbody>
                                                                                                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                                                                                        <ItemTemplate>
                                                                                                            <tr class="gradeA">
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:DropDownList ID="ddlOHType" runat="server" CssClass="form-control">
                                                                                                                    </asp:DropDownList>
                                                                                                                    <asp:Label ID="lblOHType" Visible="false" Text='<%#Eval("OVERHEADCOSTID") %>' runat="server"></asp:Label>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtOHAmntLocal" runat="server" AutoCompleteType="Disabled" Text='<%#Eval("NEWCOST") %>' onchange="isNumericSourceOfIncomeallDecimal(this)" CssClass="form-control" placeholder="Overhead Amount"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtOHNote" runat="server" AutoCompleteType="Disabled" Text='<%#Eval("Note") %>' CssClass="form-control" placeholder="Notes" MaxLength="300"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtOHAMT" runat="server" AutoCompleteType="Disabled" CssClass="form-control" Enabled="false" placeholder="Account ID"></asp:TextBox>
                                                                                                                </td>
                                                                                                                <td class="center">
                                                                                                                    <asp:LinkButton ID="ButtonAdd" runat="server" OnClick="btnAdd_Click"><i class="fa fa-plus-circle"></i></asp:LinkButton></td>
                                                                                                                <td class="center">
                                                                                                                    <asp:LinkButton ID="lnkbtndelete1" runat="server" CommandName="Delete" OnClientClick="return confirm('Do you want to delete overhead cost?')">
                                                                                                                        <asp:Image ID="Image2" runat="server" ImageUrl="images/deleteRec.png" />
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
                                                                        </ContentTemplate>

                                                                    </asp:UpdatePanel>
                                                                </asp:Panel>
                                                                <asp:HiddenField ID="hiddOHCostTab" runat="server" Value="" />
                                                            </div>
                                                            <div class="tab-pane cont" id="step2" runat="server">

                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-4" style="display: none" runat="server" id="divItem">
                                                                            <asp:Button ID="btnAddDT" class="btn btn-primary" OnClientClick="return checkproduct();" runat="server" Text="Add" OnClick="btnAddDT_Click" />
                                                                            <input id="btnDiscard1" class="btn btn-default" type="button" value="Discard" onclick='return ClearItemTab(this);' />
                                                                        </div>
                                                                        <div class="col-md-4" runat="server">
                                                                        </div>

                                                                        <div class="col-md-2 pull-right">
                                                                            <a href="javascript:showItem();">
                                                                                <img src="images/list-view.png" title="View Record" class="showItem" /></a>&nbsp;
                                     <a href="javascript:showForm();">
                                         <img src="images/form-view.png" title="Add Record" class="showForm" /></a>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <asp:Panel ID="pnlItemAdd" runat="server">

                                                                    <asp:UpdatePanel ID="productDiscripton" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <div class="row">
                                                                                <asp:Panel ID="pnlSuccessMsg123" runat="server" Visible="false">
                                                                                    <div class="alert alert-danger alert-dismissable">
                                                                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                                        <asp:Label ID="lblMsg123" runat="server"></asp:Label>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                                <div class="col-md-12">
                                                                                    <div class="col-md-4">
                                                                                        <asp:Label ID="lblProduct" runat="server" Text="Product"></asp:Label><abbr class="req"> *</abbr>
                                                                                        <asp:DropDownList ID="ddlProduct" CssClass="form-control" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                                                                                        <asp:TextBox ID="txtDescription" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Description" Rows="2" Columns="5" MaxLength="250"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <asp:Label ID="lblUOM" runat="server" Text="Unit of Measure"></asp:Label>
                                                                                        <asp:DropDownList ID="ddlUOM" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                                        <asp:HiddenField ID="hidUOMId" runat="server" Value="" />
                                                                                        <asp:HiddenField ID="hidUOMText" runat="server" Value="" />

                                                                                        <%--<asp:TextBox ID="txtUOM" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Unit of Measure"></asp:TextBox>--%>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="col-md-4">
                                                                                        <asp:Label ID="lblQuantity" runat="server" Text="Quantity"></asp:Label><abbr class="req"> *</abbr>

                                                                                        <asp:TextBox ID="txtQuantity" runat="server" AutoCompleteType="Disabled" onblur="checkdate1(this)" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" MaxLength="7" placeholder="Quantity" CssClass="form-control quntity"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtQuantity" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                        <asp:HiddenField ID="hidMColor" runat="server" Value="" />
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <asp:Label ID="lblUnitPriceForeign" runat="server" Text="Unit Price (Foreign)"></asp:Label><abbr class="req"> *</abbr>
                                                                                        <asp:TextBox ID="txtUPriceForeign" Enabled="false" onblur="checkdate1(this)" onchange="getLocalPrize()" AutoCompleteType="Disabled" runat="server" placeholder="Unit Price" CssClass="form-control uprice"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtUPriceForeign" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <asp:Label ID="lblUnitPriceRS" runat="server" Text="Unit Price (local)"></asp:Label>
                                                                                        <asp:TextBox ID="txtUPriceLocal" runat="server" onblur="checkdate1(this)" AutoCompleteType="Disabled" placeholder="Unit Price" CssClass="form-control lprice"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtUPriceLocal" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                        <asp:HiddenField ID="hidUPriceLocal" runat="server" Value="" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="col-md-4">
                                                                                        <asp:Label ID="lblDiscount" runat="server" Text="Discount"></asp:Label>
                                                                                        <asp:TextBox ID="txtDiscount" runat="server" AutoCompleteType="Disabled" onblur="checkdate1(this)" onchange="isNumericWithPer(this)" placeholder="25 or 25%" CssClass="form-control dis"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtDiscount" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <asp:Label ID="Label59" runat="server" Text="Tax(%)"></asp:Label>
                                                                                        <asp:TextBox ID="txtTax" runat="server" AutoCompleteType="Disabled" onblur="checkdate1(this)" CssClass="form-control tax" onchange="isNumericSourceOfIncomeallDecimal(this);isPercentage(this)" placeholder="Tax(%)"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtTax" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <asp:Label ID="lblTotalCurrencyForeign" runat="server" Text="Total Currency (Foreign)"></asp:Label>
                                                                                        <asp:TextBox ID="txtTotalCurrencyForeign" AutoCompleteType="Disabled" runat="server" ReadOnly="true" CssClass="form-control" placeholder="Total Currency (Foreign)"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtTotalCurrencyForeign" ValidChars="." FilterType="Custom, numbers" runat="server" />

                                                                                        <asp:HiddenField ID="hidTotalCurrencyForeign" runat="server" Value="" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="col-md-4">
                                                                                        <asp:Label ID="lblTotalCurrencyLocal" runat="server" Text="Total Currency (Local)"></asp:Label>
                                                                                        <asp:TextBox ID="txtTotalCurrencyLocal" AutoCompleteType="Disabled" runat="server" ReadOnly="true" CssClass="form-control" placeholder="Total Currency (Local)"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtTotalCurrencyLocal" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                        <asp:HiddenField ID="hidTotalCurrencyLocal" runat="server" Value="" />
                                                                                    </div>
                                                                                    <div class="col-md-8" style="top: 10px;">
                                                                                        <asp:Button ID="Button7" class="btn btn-primary" Visible="false" runat="server" Text="MultiUOM" />
                                                                                        <asp:Panel ID="pnlMultiUom" runat="server" CssClass="modalPopup" Style="display: none; height: auto; overflow: auto">

                                                                                            <div class="row">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="portlet box purple">
                                                                                                        <div class="portlet-title">
                                                                                                            <div class="caption">
                                                                                                                <i class="fa fa-globe"></i>Multi UOM
                                                                                                            </div>

                                                                                                        </div>
                                                                                                        <div class="portlet-body">
                                                                                                            <table class="table table-striped table-bordered table-hover">

                                                                                                                <thead class="repHeader">
                                                                                                                    <tr>
                                                                                                                        <th>
                                                                                                                            <asp:Label ID="Label22" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                                                        <th>
                                                                                                                            <asp:Label ID="Label23" runat="server" Text=" UOM" meta:resourcekey="lblSNameResource2"></asp:Label></th>
                                                                                                                        <th>
                                                                                                                            <asp:Label ID="Label26" runat="server" Text="Uom New Qty" meta:resourcekey="lblProOnameResource1"></asp:Label></th>

                                                                                                                    </tr>
                                                                                                                </thead>
                                                                                                                <tbody>
                                                                                                                    <asp:ListView ID="lidtUom" runat="server">
                                                                                                                        <ItemTemplate>
                                                                                                                            <tr class="gradeA">
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="LblColoername" runat="server" Text='<%# Eval("RecValue") %>'></asp:Label>
                                                                                                                                    <asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("CompniyAndContactID") %>'></asp:Label>
                                                                                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("RecTypeID") %>'></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                    <asp:TextBox ID="txtuomQty" class="form-control" runat="server"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:ListView>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="modal-footer">
                                                                                                <asp:LinkButton ID="LinkButton1" class="btn green-haze modalBackgroundbtn-circle" OnClick="LinkButton1_Click" runat="server"> Save</asp:LinkButton>
                                                                                                <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                <asp:Button ID="Button11" runat="server" class="btn green-haze btn-circle" Text="Cancel" />

                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button4" Enabled="True" PopupControlID="pnlMultiUom" TargetControlID="Button7"></cc1:ModalPopupExtender>
                                                                                        <asp:Button ID="Button2" class="btn btn-primary" Visible="false" runat="server" Text="Multi Coloer" />
                                                                                        <asp:Panel ID="pnlMultiColoer" runat="server" CssClass="modalPopup" Style="display: none; height: auto; overflow: auto">

                                                                                            <div class="row">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="portlet box yellow">
                                                                                                        <div class="portlet-title">
                                                                                                            <div class="caption">
                                                                                                                <i class="fa fa-globe"></i>Multi Coloer
                                                                                                            </div>

                                                                                                        </div>
                                                                                                        <div class="portlet-body">
                                                                                                            <table class="table table-striped table-bordered table-hover">

                                                                                                                <thead class="repHeader">
                                                                                                                    <tr>
                                                                                                                        <th>
                                                                                                                            <asp:Label ID="Label30" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                                                        <th>
                                                                                                                            <asp:Label ID="Label31" runat="server" Text="Multi Coloer" meta:resourcekey="lblSNameResource2"></asp:Label></th>

                                                                                                                        <th>
                                                                                                                            <asp:Label ID="Label34" runat="server" Text="Coloer New Qty" meta:resourcekey="lblProOnameResource1"></asp:Label></th>

                                                                                                                    </tr>
                                                                                                                </thead>
                                                                                                                <tbody>
                                                                                                                    <asp:ListView ID="listmulticoler" runat="server">
                                                                                                                        <ItemTemplate>
                                                                                                                            <tr class="gradeA">
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="LblColoername" runat="server" Text='<%# Eval("RecValue") %>'></asp:Label>
                                                                                                                                    <asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("CompniyAndContactID") %>'></asp:Label>
                                                                                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("RecTypeID") %>'></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                    <asp:TextBox ID="txtcoloerqty" class="form-control" runat="server"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:ListView>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="modal-footer">
                                                                                                <asp:LinkButton ID="linkMulticoler" class="btn green-haze modalBackgroundbtn-circle" runat="server" OnClick="linkMulticoler_Click"> Save</asp:LinkButton>
                                                                                                <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                <asp:Button ID="Button5" runat="server" class="btn green-haze modalBackgroundbtn-circle" Text="Cancel" />

                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button5" Enabled="True" PopupControlID="pnlMultiColoer" TargetControlID="Button2"></cc1:ModalPopupExtender>
                                                                                        <asp:Button ID="Button3" class="btn btn-primary" Visible="false" runat="server" Text="Multi Size" />
                                                                                        <asp:Panel ID="pnlMultiSize" runat="server" CssClass="modalPopup" Style="display: none; height: auto; overflow: auto">

                                                                                            <div class="row">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="portlet box purple">
                                                                                                        <div class="portlet-title">
                                                                                                            <div class="caption">
                                                                                                                <i class="fa fa-globe"></i>Multi Size
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="portlet-body">
                                                                                                            <table class="table table-striped table-bordered table-hover">

                                                                                                                <thead class="repHeader">
                                                                                                                    <tr>
                                                                                                                        <th>
                                                                                                                            <asp:Label ID="Label24" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                                                        <th>
                                                                                                                            <asp:Label ID="Label25" runat="server" Text=" Size" meta:resourcekey="lblSNameResource2"></asp:Label></th>
                                                                                                                        <th>
                                                                                                                            <asp:Label ID="Label28" runat="server" Text="Size New Qty" meta:resourcekey="lblProOnameResource1"></asp:Label></th>

                                                                                                                    </tr>
                                                                                                                </thead>
                                                                                                                <tbody>
                                                                                                                    <asp:ListView ID="listSize" runat="server">
                                                                                                                        <ItemTemplate>
                                                                                                                            <tr class="gradeA">
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="LblColoername" runat="server" Text='<%# Eval("RecValue") %>'></asp:Label>
                                                                                                                                    <asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("CompniyAndContactID") %>'></asp:Label>
                                                                                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("RecTypeID") %>'></asp:Label>
                                                                                                                                </td>
                                                                                                                                <td>
                                                                                                                                    <asp:TextBox ID="txtmultisze" class="form-control" runat="server"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:ListView>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="modal-footer">
                                                                                                <asp:LinkButton ID="LinkButton7" class="btn green-haze modalBackgroundbtn-circle" OnClick="LinkButton7_Click" runat="server"> Save</asp:LinkButton>
                                                                                                <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                <asp:Button ID="Button9" runat="server" class="btn green-haze btn-circle" Text="Cancel" />

                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button4" Enabled="True" PopupControlID="pnlMultiSize" TargetControlID="Button3"></cc1:ModalPopupExtender>
                                                                                        <asp:Button ID="Button4" class="btn btn-primary" Visible="false" runat="server" Text="Perishable" />
                                                                                        <asp:Button ID="Button6" class="btn btn-primary" Visible="false" runat="server" Text="MultiBinStore" />
                                                                                        <asp:Button ID="Button8" class="btn btn-primary" Visible="false" runat="server" Text="Serialized" />
                                                                                        <asp:Panel ID="pneSerial" runat="server" CssClass="modalPopup" Style="display: none; height: 75%; overflow: auto">
                                                                                            <%-- <asp:HyperLink ID="lnkClose" runat="server">--%>
                                                                                            <%--</asp:HyperLink>--%>
                                                                                            <div class="row">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="portlet box grey-cascade">
                                                                                                        <div class="portlet-title">
                                                                                                            <div class="caption">
                                                                                                                <i class="fa fa-globe"></i>Serialization 
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="portlet-body">
                                                                                                            <table class="table table-striped table-bordered table-hover" id="sample_2">

                                                                                                                <thead class="repHeader">
                                                                                                                    <tr>
                                                                                                                        <th>
                                                                                                                            <asp:Label ID="Label1" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>

                                                                                                                        <th>
                                                                                                                            <asp:Label ID="Label21" runat="server" Text="Serialization No" meta:resourcekey="lblProOnameResource1"></asp:Label></th>

                                                                                                                    </tr>
                                                                                                                </thead>
                                                                                                                <tbody>
                                                                                                                    <asp:ListView ID="listSerial" runat="server">
                                                                                                                        <ItemTemplate>
                                                                                                                            <tr class="gradeA">
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                </td>

                                                                                                                                <td>
                                                                                                                                    <asp:TextBox ID="txtlistSerial" class="form-control" runat="server"></asp:TextBox>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:ListView>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                            <div class="modal-footer">
                                                                                                <asp:LinkButton ID="LinkButton3" class="btn green-haze modalBackgroundbtn-circle" OnClick="LinkButton3_Click" runat="server"> Save</asp:LinkButton>
                                                                                                <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                <asp:Button ID="Button10" runat="server" class="btn green-haze btn-circle" Text="Cancel" />

                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button1" Enabled="True" PopupControlID="pneSerial" TargetControlID="Button8"></cc1:ModalPopupExtender>
                                                                                    </div>
                                                                                </div>
                                                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                            </div>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="ddlProduct" EventName="SelectedIndexChanged" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlItemShow" runat="server" Style="display: none; padding-top: 10px;">
                                                                    <div class="col-md-12">
                                                                        <div class="block-flat">
                                                                            <div class="content">
                                                                                <div class="table-responsive">
                                                                                    <table class="table table-bordered" id="datatable">
                                                                                        <thead class="repHeader">
                                                                                            <tr>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label13" runat="server" Text="#"></asp:Label></th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label14" runat="server" Text="Product Name"></asp:Label></th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label15" runat="server" Text="Quantity"></asp:Label></th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label16" runat="server" Text="OH(Amt)"></asp:Label></th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label17" runat="server" Text="Tax(%)"></asp:Label></th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label18" runat="server" Text="TOTAL"></asp:Label></th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label19" runat="server" Text="Edit"></asp:Label></th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label20" runat="server" Text="Del"></asp:Label></th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="Repeater2_ItemCommand">
                                                                                                <ItemTemplate>
                                                                                                    <tr class="gradeA">
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblProductNameItem" runat="server" Text='<%#Eval("MyProdID") %>'></asp:Label>
                                                                                                            <asp:Label ID="lblDiscription" Visible="false" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label>
                                                                                                            <asp:Label ID="lblUOM" Visible="false" runat="server" Text='<%#Eval("UOM") %>'></asp:Label>
                                                                                                            <asp:Label ID="lblUNITPRICE" Visible="false" runat="server" Text='<%#Eval("UNITPRICE") %>'></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblDisQnty" runat="server" Style="text-align: right" Text='<%#Eval("QUANTITY") %>' />
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblOHAmnt" runat="server" Style="text-align: right" Text='<%#Eval("OVERHEADAMOUNT") %>' />
                                                                                                        </td>

                                                                                                        <td>
                                                                                                            <asp:Label ID="lblTaxDis" runat="server" Style="text-align: right" Width="50px" Text='<%#Eval("TAXPER") %>' />
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblTotalCurrency" Style="text-align: right" runat="server" Width="50px" Text='<%#Eval("AMOUNT") %>' />
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:LinkButton ID="lnkbtnItem" runat="server" CommandName="editDT" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") %>'>
                                                                                                                <asp:Image ID="Image1" runat="server" ImageUrl="images/editRec.png" />
                                                                                                            </asp:LinkButton></td>
                                                                                                        <td>
                                                                                                            <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDT" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") %>' OnClientClick="return confirm('Do you want to delete product?')">
                                                                                                                <asp:Image ID="Image2" runat="server" ImageUrl="images/deleteRec.png" />
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

                                                                </asp:Panel>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="pull-left" style="margin-left: 32%">
                                                        <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Draft Quotation" OnClientClick="return showWarningToast()" OnClick="btnSubmit_Click" />
                                                        <asp:Button ID="btnConfirmOrder" class="btn btn-default" Width="120px" runat="server" Text="Confirm Order" OnClick="btnConfirmOrder_Click" />
                                                        <asp:Button ID="btnDiscard" class="btn btn-primary" runat="server" Text="Discard" OnClick="btnDiscard_Click" OnClientClick="return confirm('Are you sure you want to discard data?');" />
                                                        <asp:Button ID="btnPrint" class="btn btn-default" OnClick="btnPrint_Click" runat="server" Text="Print" />
                                                        <asp:HyperLink data-toggle="modal" role="button" ID="hlPeri" onclick="getEmail(this.id)" Text="Send Mail" NavigateUrl="#form_Mail" runat="server">
                                                        </asp:HyperLink>
                                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                                    </div>

                                               

                                            </div>
                                        </div>

                                        <div id="form_Mail" class="modal fade" tabindex="-1" role="dialog" style="display: block; margin-top: -106.5px; width: 700px;">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="close(this);disButton(this);">&times;</button><h4 class="modal-title"><i class="icon-paragraph-justify2"></i>
                                                            <asp:Label ID="Label3" runat="server" Text="Send Mail"></asp:Label>
                                                            <asp:Button ID="btnSendMail" CssClass="btn btn-primary" runat="server" OnClick="btnSendMail_Click1" Text="Send Mail" />
                                                            <asp:Button ID="Button1" class="btn btn-default" data-dismiss="modal" aria-hidden="true" OnClientClick="close(this)" runat="server" Text="Close" />
                                                            <a id="a1" data-dismiss="modal" aria-hidden="true" onclick="close(this)"></a>
                                                        </h4>
                                                    </div>
                                                    <br />
                                                    <div class="block-flat">
                                                        <div class="row">
                                                            <div class="col-md-2 form-group">
                                                            </div>
                                                            <div class="col-md-4 form-group">
                                                                <asp:Label ID="lblRec" runat="server" Text="To Recipients"></asp:Label><abbr class="req"> *</abbr>
                                                                <asp:TextBox AutoCompleteType="Disabled" ID="txtRecipient" runat="server" CssClass="form-control" placeholder="Recipients email"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4 form-group">
                                                                <asp:Label ID="lblCC" runat="server" Text="CC"></asp:Label><abbr class="req"> *</abbr>
                                                                <asp:TextBox AutoCompleteType="Disabled" ID="txtRecCC" runat="server" CssClass="form-control" placeholder="Recipients email"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hidICHD" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="portlet box yellow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Sales Quotations List
                                    </div>
                                    <div class="tools">
                                        <a id="A2" runat="server" href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                    </div>
                                </div>
                                <div id="Div1" runat="server" class="portlet-body" style="padding-left: 25px;">
                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general">

                                                <div class="portlet-body">
                                                    <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                        <thead class="repHeader">
                                                            <tr>
                                                                <th>
                                                                    <asp:Label ID="lblSN" runat="server" Text="#"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lblPSName" runat="server" Text="Supplier Name"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lblPOD" runat="server" Text="Order Date"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lblPTA" runat="server" Text="Total Amount"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lblPQS" runat="server" Text="Status"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lblEdit" runat="server" Text="Edit"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lblDel" runat="server" Text="Delete"></asp:Label></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="grdPO" runat="server" OnItemCommand="grdPO_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr class="gradeA">
                                                                        <td>
                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblPSN" runat="server" Text='<%# Eval("CUSTVENDID") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblODate" runat="server" Text='<%# Eval("TRANSDATE", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("TOTAMT") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblspnoo" runat="server" Text='<%# Eval("MainTranType").ToString() =="SQ" ? "Sales Quotations" : "Draft PO"%>'></asp:Label></td>
                                                                        <td class="center">
                                                                            <asp:LinkButton ID="lnkbtnPurchase_Order" runat="server" CommandName="Edit" CommandArgument='<%# Eval("MYTRANSID") %>'>
                                                                                <asp:Image ID="Image1" runat="server" ImageUrl="images/editRec.png" />
                                                                            </asp:LinkButton></td>
                                                                        <td class="center">
                                                                            <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Do you want to delete purchase quotation?')" CommandArgument='<%# Eval("MYTRANSID") %>'>
                                                                                <asp:Image ID="Image2" runat="server" ImageUrl="images/deleteRec.png" />
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
