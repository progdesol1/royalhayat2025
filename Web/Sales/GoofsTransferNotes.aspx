<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="GoofsTransferNotes.aspx.cs" Inherits="Web.Sales.GoofsTransferNotes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function checkPosUOM(id) {
            var r3 = document.getElementById("ContentPlaceHolder1_hidMasterProID").value;
            var varMyId = "ContentPlaceHolder1_hidMyId";
            document.getElementById(varMyId).value = r3;
            var varFUOM = "ContentPlaceHolder1_gvPurQuat2_hidUOMId_" + r3;
            var varFUOMId = document.getElementById(varFUOM).value;
            var varTUOM = document.getElementById("ContentPlaceHolder1_ddlUOMPop");
            var varTUOMId = varTUOM.options[varTUOM.selectedIndex].value;
            $(document).ready(function () {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Incoming_Shipment.aspx/checkPosUOM",
                    data: "{Param1: '" + varFUOMId + "',Param2: '" + varTUOMId + "'}",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (result) {
                        if (result.d == "0") {
                            $().toastmessage('showWarningToast', "This conversation is not possible.");
                            varTUOM.options[0].selected = true;
                            return false;
                        }
                        else { return true; }
                    },
                    error: function (result) {
                    }
                });
            });
        }
        function getProInfoMCS() {
            var e2 = document.getElementById("ContentPlaceHolder1_ddlProductMCS");
            var Param2 = e2.options[e2.selectedIndex].value;
            $(document).ready(function () {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Incoming_Shipment.aspx/getProInfoMCS",
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

            if (result1[0] == "True") {
                document.getElementById("ContentPlaceHolder1_ddlItemColor").style.display = "block";
                document.getElementById("ContentPlaceHolder1_lblQuntiMC").innerHTML = result1[3];
            }
            else {
                document.getElementById("ContentPlaceHolder1_ddlItemColor").style.display = "none";
            }
            if (result1[1] == "True") {
                document.getElementById("ContentPlaceHolder1_ddlItemSize").style.display = "block";
                document.getElementById("ContentPlaceHolder1_lblQuntiMC").innerHTML = result1[3];
            }
            else {
                document.getElementById("ContentPlaceHolder1_ddlItemSize").style.display = "none";
            }
        }
        function UniPriED() {
            document.getElementById("ContentPlaceHolder1_txtUPriceForeign").disabled = false;
            document.getElementById("ContentPlaceHolder1_txtUPriceLocal").disabled = true;
        }
    </script>

    <script>
        function CheckValUOM() {

            var temp1 = document.getElementById('ContentPlaceHolder1_txtUONQua');
            if (temp1.value == "") {
                $().toastmessage('showWarningToast', "Please enter the quantity.");
                return false;
            }
            var temp2 = document.getElementById('ContentPlaceHolder1_ddlUOMPop');
            if (temp2.value == "0") {
                $().toastmessage('showWarningToast', "Please select the UOM.");
                return false;
            }
        }
        function showSuccessToast() {
            $().toastmessage('showSuccessToast', "Purchase quotation created successfully.");
        }
        function showSuccessToast1() {
            $().toastmessage('showSuccessToast', "Purchase quotation updated successfully.");
        }
        function showSuccessToast2() {
            $().toastmessage('showSuccessToast', "Purchase quotation successfully confirm to purchase order.");
        }
        function showSuccessSendMail() {
            $().toastmessage('showSuccessToast', "Purchase quotation send successfully.");
        }
        function checkproduct() {
            var temp3 = document.getElementById("ContentPlaceHolder1_gvPurQuat_ddlInsProductName");
            var temp5 = document.getElementById('ContentPlaceHolder1_gvPurQuat_txtInsDescription');
            var temp1 = document.getElementById('ContentPlaceHolder1_gvPurQuat_txtInsDis');
            var temp2 = document.getElementById('ContentPlaceHolder1_gvPurQuat_txtInsTax');
            var temp4 = document.getElementById('ContentPlaceHolder1_gvPurQuat_txtInsUpriceFooter');
            var temp6 = document.getElementById('ContentPlaceHolder1_gvPurQuat_txtInsQuantity');

            if (temp3.value == "0") {
                temp5.value = "";
                $().toastmessage('showWarningToast', "Please select the product.");
                return false;
            }
            else {
                var strUser = temp3.options[temp3.selectedIndex].text;
                if (temp3.value != "")
                    temp5.value = strUser;
            }
        }
    </script>
    <script type="text/javascript">
        function showhidepanel(va) {
            if (va == "lst") {
                document.getElementById('ContentPlaceHolder1_pnlCreateForm').style.display = "none";
                document.getElementById('ContentPlaceHolder1_pnlGridView').style.display = "block";
            }
            else if (va == "frm") {
                document.getElementById('ContentPlaceHolder1_pnlCreateForm').style.display = "block";
                document.getElementById('ContentPlaceHolder1_pnlGridView').style.display = "none";
            }
        }
    </script>
    <script type="text/javascript">

        var counter = 0;
        var varcounter = parseInt(counter);
        function GetDynamicTextBox1(value) {
            return '&nbsp&nbsp' + (++varcounter) + '&nbsp&nbsp<input name = "DynamicTextBox" placeholder="serial number" type="text" value = "' + value + '" />&nbsp' +
                                '<input type="button" value="Remove" onclick = "RemoveTextBox(this)" />'
        }
        function AddTextBox(id) {
            //var varQty = document.getElementById("ContentPlaceHolder1_gvPurQuat_hlSerialized_0").value;
            //var varQty1 = document.getElementById("ContentPlaceHolder1_hlSerialized1").value;
            var r = id;
            var r1 = r.split("_");
            var r2 = r1.length;
            var r3 = r1[r2 - 1];
            var varQut = "ContentPlaceHolder1_gvPurQuat2_txtQty_" + r3;
            var varQutVal = document.getElementById(varQut).value;
            var varQutValDiv = parseInt(varQutVal) / 5;
            var varQutValDiv1 = parseInt(varQutVal) % 5;

            for (var i = 0; i < parseInt(varQutVal) ; i++) {
                var div = document.createElement('DIV');
                div.innerHTML = GetDynamicTextBox1("");
                document.getElementById("TextBoxContainer").appendChild(div);
            }

        }
        function RemoveTextBox(div) {
            document.getElementById("TextBoxContainer").removeChild(div.parentNode);
        }

        function RecreateDynamicTextboxes() {
            <%--var values = eval('<%=Values%>');
            if (values != null) {
                var html = "";
                for (var i = 0; i < values.length; i++) {
                    html += "<div>" + GetDynamicTextBox(values[i]) + "</div>";
                }
                document.getElementById("TextBoxContainer").innerHTML = html;
            }--%>
        }
        window.onload = RecreateDynamicTextboxes;
        function MultiSizecolor(id) {
            var r = id;
            var r1 = r.split("_");
            var r2 = r1.length;
            var r3 = r1[r2 - 1];
            var varProId = "ContentPlaceHolder1_hidMasterProID";
            document.getElementById(varProId).value = r3;
        }
        function MultiUOM(id) {
            var r = id;
            var r1 = r.split("_");
            var r2 = r1.length;
            var r3 = r1[r2 - 1];
            var varProId = "ContentPlaceHolder1_hidMasterProID";
            document.getElementById(varProId).value = r3;

            var qty = "ContentPlaceHolder1_gvPurQuat2_txtQty_" + r3;
            var UOM = "ContentPlaceHolder1_gvPurQuat2_Label12_" + r3;
            var qtyV = document.getElementById(qty).value
            var UOMV = document.getElementById(UOM).innerHTML;
            document.getElementById("ContentPlaceHolder1_lblCQuaN").innerHTML = qtyV + " " + UOMV;
        }
        function visibleButton(id) {
            var r = id;
            var r1 = r.split("_");
            var r2 = r1.length;
            var r3 = r1[r2 - 1];
            var varProIdMC = "ContentPlaceHolder1_gvPurQuat2_hlMC_" + r3;
            var varProIdSe = "ContentPlaceHolder1_gvPurQuat2_hlSer_" + r3;
            var varProIdBin = "ContentPlaceHolder1_gvPurQuat2_hlMBin_" + r3;
            var varProIdPei = "ContentPlaceHolder1_gvPurQuat2_hlPeri_" + r3;
            document.getElementById(varProIdMC).Enable = true;
            document.getElementById(varProIdSe).disabled = false;
            document.getElementById(varProIdBin).disabled = false;
            document.getElementById(varProIdPei).disabled = false;
            return false;
        }
        //function RecreateDynamicTextboxes() {
        //    var values = eval('<Values%>');
        //    if (values != null) {
        //        var html = "";
        //        for (var i = 0; i < values.length; i++) {
        //            html += "<div>" + GetDynamicTextBox(values[i]) + "</div>";
        //        }
        //        document.getElementById("TextBoxContainer").innerHTML = html;
        //    }
        //}
        //window.onload = RecreateDynamicTextboxes;
    </script>
    <style type="text/css">
        .form-control .select2me {
            Color: #8e5fa2;
        }

        .select2-chosen {
            Color: #8e5fa2;
        }

        .control-label {
            Color: #428bca;
            font-weight: bold;
        }

        .form-control {
            Color: #8e5fa2;
        }

        .form-horizontal .control-label {
            margin-bottom: 0;
            padding-top: 7px;
            text-align: start;
        }

        .repHeader {
            Color: #428bca;
        }

        th {
            text-align: center;
        }
    </style>
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

    <div  id="b" runat="server">
        <div class="page-head">
            <ol class="breadcrumb">
                <li><a href="../ECOMM/AdminIndex.aspx">
                    <asp:Label ID="lblDisPro" runat="server" Text="Home"></asp:Label></a><i class="fa fa-circle"></i></li>
                <li><a href="#">
                    <asp:Label ID="lblDisProMst" runat="server" Text="Goods Received Notes"></asp:Label></a></li>
                <div class="pull-right">
                    <img id="Img4" src="images/list-view.png" runat="server" alt="ListviewDisplay" onclick="showhidepanel('lst');" />
                    <img id="Img2" src="images/form-view.png" runat="server" alt="formrolemstr" onclick="showhidepanel('frm');" />
                </div>
            </ol>

        </div>
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box red">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Goods Received Note
                                    </div>
                                    <div class="tools">
                                        <a id="shlinkProductDetails" runat="server" href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Receive" OnClientClick="return showWarningToast()" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnRequestde" class="btn btn-primary" runat="server" Text="Request Delivery" OnClick="btnRequestde_Click" />
                                        <asp:Button ID="Button3" class="btn btn-runat" runat="server" Text="Exit" ForeColor="Black" OnClick="btnExit_Click" />

                                    </div>
                                </div>
                                <div id="shProductDetails" runat="server" class="portlet-body" style="padding-left: 25px;">
                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general">
                                                <asp:Panel ID="pnlCreateForm" runat="server">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblSupplier" class="col-md-4 control-label" runat="server" Text="Supplier"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlSupplier" CssClass="form-control select2me" ReadOnly="true" Enabled="false" runat="server" onchange="getcurrency(this);"></asp:DropDownList>
                                                                    <asp:LinkButton ID="lnkbtnSupp" runat="server" Visible="false"></asp:LinkButton>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblOrderDate" class="col-md-4 control-label" runat="server" Text="Date of Transaction"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtOrderDate" ReadOnly="true" CssClass="form-control"
                                                                        runat="server" placeholder="Transaction Date" onchange="checkdate(this)"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtOrderDate" Format="dd/MM/yyyy" Enabled="True">
                                                                    </cc1:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblLocalForeign" class="col-md-4 control-label" runat="server" Text="Local/Foreign"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlLocalForeign" CssClass="form-control select2me" Enabled="false" ReadOnly="true" runat="server" onchange="selectcurrency()"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">

                                                                <asp:Label ID="lblCurrency" class="col-md-4 control-label" runat="server" Text="Currency"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlCurrency" CssClass="form-control select2me" Enabled="false" ReadOnly="true" runat="server"></asp:DropDownList>
                                                                    <%--<asp:LinkButton ID="lbtnCR"  runat="server" Text="Currency Rate"></asp:LinkButton>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblBatchNo" class="col-md-4 control-label" runat="server" Text="Batch No."></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtBatchNo" CssClass="form-control" ReadOnly="true" runat="server" placeholder="Batch Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblProjectNo" class="col-md-4 control-label" runat="server" Text="Project No."></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlProjectNo" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control select2me">
                                                                    </asp:DropDownList>
                                                                    <%--<asp:TextBox AutoCompleteType="Disabled" ID="txtProjectNo" CssClass="form-control" runat="server" placeholder="Project Number"></asp:TextBox>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                             <div class="col-md-6">
                                                                <asp:Label ID="lblRef" class="col-md-4 control-label" runat="server" Text="Reference"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtrefreshno" ReadOnly="true" CssClass="form-control" runat="server" placeholder="Supplier Reference"></asp:TextBox>
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
                                                                    <asp:DropDownList ID="ddlCrmAct" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control select2me">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">

                                                                <asp:Label ID="lblNotes" class="col-md-4 control-label" runat="server" Text="Notes"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtNoteHD" ReadOnly="true" CssClass="form-control" runat="server" placeholder="Notes" TextMode="SingleLine" MaxLength="550"></asp:TextBox>
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
                                                            <div class="col-md-6">
                                                                <asp:Label ID="Label1" class="col-md-4 control-label" runat="server" Text="Remark"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtRemark" CssClass="form-control" runat="server" placeholder="Remark" TextMode="MultiLine" MaxLength="550"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="tab-container">
                                                        <ul class="nav nav-tabs">
                                                            <li id="liItems" class="active" runat="server"><a href="#ContentPlaceHolder1_step1" data-toggle="tab">
                                                                <asp:Label ID="lblITems" runat="server" Text="Items"></asp:Label></a></li>
                                                        </ul>
                                                        <div class="tab-content">
                                                            <div class="tab-pane active cont" id="step1" runat="server">
                                                                <asp:Panel ID="pnlSuccessMsg123" runat="server" Visible="false">
                                                                    <div class="alert alert-danger alert-dismissable">
                                                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                        <asp:Label ID="lblMsg123" runat="server"></asp:Label>
                                                                    </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlItemShow" runat="server">
                                                                    <div class="table-responsive">
                                                                        <table class="table table-bordered" id="Table2">
                                                                            <thead class="repHeader">
                                                                                <tr>
                                                                                    <th class="col-md-1">
                                                                                        <asp:Label ID="Label13" runat="server" Text="#"></asp:Label></th>
                                                                                    <th class="col-md-4">
                                                                                        <asp:Label ID="Label3" runat="server" Text="Product Code - Product Name"></asp:Label></th>
                                                                                    <th class="col-md-1">
                                                                                        <asp:Label ID="Label15" runat="server" Text="Quantity"></asp:Label></th>
                                                                                    <th class="col-md-1">
                                                                                        <asp:Label ID="Label10" runat="server" Text="UOM"></asp:Label></th>
                                                                                    <th class="col-md-1">
                                                                                        <asp:Label ID="Label7" runat="server" Text="MultiUOM"></asp:Label></th>
                                                                                    <th class="col-md-1">
                                                                                        <asp:Label ID="Label16" runat="server" Text="MultiColor"></asp:Label></th>
                                                                                    <th class="col-md-1">
                                                                                        <asp:Label ID="Label25" runat="server" Text="MultiSize"></asp:Label></th>
                                                                                    <th class="col-md-1">
                                                                                        <asp:Label ID="Label17" runat="server" Text="Serialized"></asp:Label></th>
                                                                                    <th class="col-md-1">
                                                                                        <asp:Label ID="Label18" runat="server" Text="Multi Bin"></asp:Label></th>
                                                                                    <th class="col-md-1">
                                                                                        <asp:Label ID="Label5" runat="server" Text="Perishable"></asp:Label></th>

                                                                                    <th class="col-md-1">
                                                                                        <asp:Label ID="Label6" runat="server" Text="Delete"></asp:Label></th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <asp:Repeater ID="gvPurQuat2" runat="server" OnItemCommand="gvPurQuat2_ItemCommand" OnItemDataBound="gvPurQuat2_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <tr class="gradeA">
                                                                                            <td>
                                                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>
                                                                                                <asp:HiddenField ID="hidMyID" runat="server" Value='<%# Eval("MYID")%>' />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblPSN" runat="server" Text='<%# getproductName(Convert .ToInt32 ( Eval("MyProdID"))) %>'></asp:Label>
                                                                                                <asp:Label ID="lblproductid" runat="server" Visible="false" Text='<%#  Eval("MyProdID") %>'></asp:Label>
                                                                                                <asp:Label ID="labeltarjication" runat="server" Visible="false" Text='<%#  Eval("MYTRANSID") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%--<asp:TextBox ID="txtQty" CssClass="form-control" AutoCompleteType="Disabled" onchange="visibleButton(this.id);" runat="server" Text='<%# Eval("Quantity") %>'></asp:TextBox>--%>
                                                                                                <asp:TextBox ID="txtQty" CssClass="form-control" AutoCompleteType="Disabled" runat="server" Text='<%# Eval("Quantity") %>'></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label12" runat="server" Text='<%#getUOMTable(Convert.ToInt32( Eval("UOM"))) %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:HyperLink data-toggle="modal" role="button" ID="hlUOM" onclick="MultiUOM(this.id)" Visible='<%# Convert.ToBoolean(Eval("MultiUOM").ToString() == "True") %>' NavigateUrl='<%# string .Format ("{0}{1}", "#form_modalUOM", Eval("MyProdID") ) %>' runat="server">
                                                                                                    <asp:Label ID="Label14" runat="server" Text="Add"></asp:Label>
                                                                                                </asp:HyperLink>
                                                                                                <div id='<%# string .Format ("{0}{1}", "form_modalUOM", Eval("MyProdID") ) %>' class="modal fade" tabindex="-1" role="dialog">
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
                                                                                                                                    <asp:Label ID="Label33" runat="server" Text=" UOM" meta:resourcekey="lblSNameResource2"></asp:Label></th>
                                                                                                                                <th>
                                                                                                                                    <asp:Label ID="Label35" runat="server" Text="Uom New Qty" meta:resourcekey="lblProOnameResource1"></asp:Label></th>

                                                                                                                            </tr>
                                                                                                                        </thead>
                                                                                                                        <tbody>
                                                                                                                            <asp:ListView ID="listMUOMLIST" runat="server">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <tr class="gradeA">
                                                                                                                                        <td>
                                                                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <asp:Label ID="LblColoername" runat="server" Text='<%#GetUom(Convert .ToInt32 ( Eval("UOM"))) %>'></asp:Label>
                                                                                                                                            <asp:Label ID="LBLCOLERID" runat="server" Visible="false" Text='<%# Eval("UOM") %>'></asp:Label>
                                                                                                                                            <asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <asp:TextBox ID="Label38" class="form-control" Text='<%# Eval("OpQty") %>' runat="server"></asp:TextBox>
                                                                                                                                             <%--<asp:Label ID="Label38" Visible="false" runat="server" Text='<%# Eval("OpQty") %>'></asp:Label>--%>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:ListView>
                                                                                                                        </tbody>
                                                                                                                    </table>
                                                                                                                    <div class="actions btn-set">
                                                                                                                        <asp:LinkButton ID="btnmultiuom" class="btn green-haze modalBackgroundbtn-circle" runat="server" OnClick ="btnmultiuom_Click"> Save</asp:LinkButton>
                                                                                                                        <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                        <asp:Button ID="Button11" runat="server" OnClientClick="close(this)" class="btn btn-default" data-dismiss="modal" aria-hidden="true" Text="Cancel" />

                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                </div>
                                                                                                <%-- <asp:HiddenField ID="hidUOMId" runat="server" Value='<%#Eval("UOMId")%>' />--%>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:HyperLink data-toggle="modal" role="button" ID="hlMC" onclick="MultiSizecolor(this.id)" Visible='<%# Convert.ToBoolean(Eval("MultiColor").ToString() == "True") %>' NavigateUrl='<%# string .Format ("{0}{1}", "#form_modalMCSU", Eval("MyProdID") ) %>' runat="server">
                                                                                                    <asp:Label ID="lblAddMC" runat="server" Text="Add"></asp:Label>
                                                                                                    <%-- <asp:HiddenField ID="hidpro" runat="server" Value='<%# Eval("ITEMID") %>' />--%>
                                                                                                </asp:HyperLink>
                                                                                                <div id='<%# string .Format ("{0}{1}", "form_modalMCSU", Eval("MyProdID") ) %>' class="modal fade" tabindex="-1" role="dialog">
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
                                                                                                                                            <asp:Label ID="LblColoername" runat="server" Text='<%#getcolerName(Convert .ToInt32 ( Eval("COLORID"))) %>'></asp:Label>
                                                                                                                                            <asp:Label ID="LBLCOLERID" runat="server" Visible="false" Text='<%# Eval("COLORID") %>'></asp:Label>
                                                                                                                                            <asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>

                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <asp:TextBox ID="txtcoloerqty" class="form-control" Text='<%#Eval("OpQty") %>' runat="server"></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:ListView>
                                                                                                                        </tbody>
                                                                                                                    </table>
                                                                                                                    <div class="actions btn-set">
                                                                                                                        <asp:LinkButton ID="linkMulticoler" class="btn green-haze modalBackgroundbtn-circle" CommandName="btnMultiColoer" CommandArgument='<%# Eval("MYTRANSID") %>' runat="server"> Save</asp:LinkButton>
                                                                                                                        <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                        <asp:Button ID="Button5" runat="server" OnClientClick="close(this)" class="btn btn-default" data-dismiss="modal" aria-hidden="true" Text="Cancel" />

                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>


                                                                                                </div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:HyperLink data-toggle="modal" role="button" ID="hlMS" onclick="MultiSizecolor(this.id)" Visible='<%# Convert.ToBoolean(Eval("MultiSize").ToString() == "True") %>' NavigateUrl='<%# string .Format ("{0}{1}", "#form_modalMSU", Eval("MyProdID") ) %>' runat="server">
                                                                                                    <asp:Label ID="Label27" runat="server" Text="Add"></asp:Label>
                                                                                                    <%-- <asp:HiddenField ID="hidpro" runat="server" Value='<%# Eval("ITEMID") %>' />--%>
                                                                                                </asp:HyperLink>
                                                                                                <div id='<%# string .Format ("{0}{1}", "form_modalMSU", Eval("MyProdID") ) %>' class="modal fade" tabindex="-1" role="dialog">
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
                                                                                                                                    <asp:Label ID="Label28" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                                                                <th>
                                                                                                                                    <asp:Label ID="Label29" runat="server" Text=" Size" meta:resourcekey="lblSNameResource2"></asp:Label></th>
                                                                                                                                <th>
                                                                                                                                    <asp:Label ID="Label32" runat="server" Text="Size New Qty" meta:resourcekey="lblProOnameResource1"></asp:Label></th>

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
                                                                                                                                            <asp:Label ID="LblColoername" runat="server" Text='<%#getcolerName(Convert .ToInt32 ( Eval("SIZECODE"))) %>'></asp:Label>
                                                                                                                                            <asp:Label ID="LBLcOLERID" runat="server" Visible="false" Text='<%#Eval("SIZECODE") %>'></asp:Label>
                                                                                                                                            <asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <asp:TextBox ID="txtmultisze" class="form-control" Text='<%#Eval("OpQty") %>' runat="server"></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:ListView>
                                                                                                                        </tbody>
                                                                                                                    </table>
                                                                                                                    <div class="actions btn-set">
                                                                                                                        <asp:LinkButton ID="LinkButton7" class="btn green-haze modalBackgroundbtn-circle" CommandArgument='<%# Eval("MYPRODID") %>' CommandName="btmMultisize" runat="server"> Save</asp:LinkButton>
                                                                                                                        <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                        <asp:Button ID="Button9" runat="server" OnClientClick="close(this)" class="btn btn-default" data-dismiss="modal" aria-hidden="true" Text="Cancel" />

                                                                                                                    </div>
                                                                                                                </div>

                                                                                                            </div>

                                                                                                        </div>
                                                                                                    </div>

                                                                                                </div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:HyperLink data-toggle="modal" role="button" ID="hlSer" onclick="MultiSizecolor(this.id);AddTextBox(this.id);" Visible='<%# Convert.ToBoolean(Eval("Serialized").ToString() == "True") %>' NavigateUrl='<%# string .Format ("{0}{1}", "#form_modalSeial", Eval("MyProdID") ) %>' runat="server">
                                                                                                    <asp:Label ID="lblSer" runat="server" Text="Add"></asp:Label>
                                                                                                </asp:HyperLink>
                                                                                                <div id='<%# string .Format ("{0}{1}", "form_modalSeial", Eval("MyProdID") ) %>' class="modal fade" tabindex="-1" role="dialog">
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12">
                                                                                                            <div class="portlet box grey-cascade">
                                                                                                                <div class="portlet-title">
                                                                                                                    <div class="caption">
                                                                                                                        <i class="fa fa-globe"></i>Serialization 
                                                                                                                    </div>

                                                                                                                </div>
                                                                                                                <div class="portlet-body">
                                                                                                                    <table class="table table-striped table-bordered table-hover">

                                                                                                                        <thead class="repHeader">
                                                                                                                            <tr>
                                                                                                                                <th>
                                                                                                                                    <asp:Label ID="Label20" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>

                                                                                                                                <th>
                                                                                                                                    <asp:Label ID="Label26" runat="server" Text="Serialization No" meta:resourcekey="lblProOnameResource1"></asp:Label></th>

                                                                                                                            </tr>
                                                                                                                        </thead>
                                                                                                                        <tbody>
                                                                                                                            <asp:ListView ID="listSerial" runat="server">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <tr class="gradeA">
                                                                                                                                        <td>
                                                                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                            <asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                                                                        </td>

                                                                                                                                        <td>
                                                                                                                                            <asp:TextBox ID="txtlistSerial" Text='<%# Eval("Serial_Number") %>' class="form-control" runat="server"></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:ListView>
                                                                                                                        </tbody>
                                                                                                                    </table>
                                                                                                                    <div class="actions btn-set">
                                                                                                                        <asp:LinkButton ID="LinkButton3" class="btn green-haze modalBackgroundbtn-circle" CommandArgument='<%# Eval("MYPRODID") %>' CommandName="bltiSirroliez" runat="server"> Save</asp:LinkButton>
                                                                                                                        <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                        <asp:Button ID="Button10" runat="server" OnClientClick="close(this)" class="btn btn-default" data-dismiss="modal" aria-hidden="true" Text="Cancel" />

                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>


                                                                                                </div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:HyperLink data-toggle="modal" role="button" ID="hlMBin" onclick="MultiSizecolor(this.id)" Visible='<%# Convert.ToBoolean(Eval("MultiBinStore").ToString() == "True") %>' NavigateUrl='<%# string .Format ("{0}{1}", "#form_modalBin", Eval("MyProdID") ) %>' runat="server">
                                                                                                    <asp:Label ID="lblMBin" runat="server" Text="Add"></asp:Label>
                                                                                                </asp:HyperLink>
                                                                                                <div id='<%# string .Format ("{0}{1}", "form_modalBin", Eval("MyProdID") ) %>' class="modal fade" tabindex="-1" role="dialog">
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12">
                                                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                                <ContentTemplate>
                                                                                                                    <div class="form-horizontal form-row-seperated">
                                                                                                                        <div class="portlet box yellow">

                                                                                                                            <div class="portlet-title">

                                                                                                                                <div class="caption">
                                                                                                                                    <i class="fa fa-globe"></i>Multi Bin<asp:Label ID="tblProdid" Visible="false" runat="server"></asp:Label><asp:Label ID="lblQTY" Visible="false" runat="server"></asp:Label><asp:Label ID="lblUOM" Visible="false" runat="server"></asp:Label>
                                                                                                                                </div>

                                                                                                                            </div>
                                                                                                                            <div class="portlet-body">

                                                                                                                                <table class="table table-striped table-bordered table-hover">

                                                                                                                                    <thead class="repHeader">
                                                                                                                                        <tr>
                                                                                                                                            <th>
                                                                                                                                                <asp:Label ID="Label8" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                                                                            <th>
                                                                                                                                                <asp:Label ID="Label21" runat="server" Text="Bin" meta:resourcekey="lblSNameResource2"></asp:Label></th>

                                                                                                                                            <th>
                                                                                                                                                <asp:Label ID="Label36" runat="server" Text="Qty" meta:resourcekey="lblSNameResource2"></asp:Label></th>

                                                                                                                                        </tr>
                                                                                                                                    </thead>
                                                                                                                                    <tbody>
                                                                                                                                        <asp:ListView ID="listBin" runat="server">

                                                                                                                                            <ItemTemplate>
                                                                                                                                                <tr class="gradeA">
                                                                                                                                                    <td>
                                                                                                                                                        <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        <asp:Label ID="Label37" runat="server" Text='<%# getbinName(Convert .ToInt32 ( Eval("Bin_ID"))) %>'></asp:Label>
                                                                                                                                                        <asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                                                                                        <asp:Label ID="lblbinid" Visible="false" runat="server" Text='<%# Eval("Bin_ID") %>'></asp:Label>
                                                                                                                                                    </td>

                                                                                                                                                    <td>
                                                                                                                                                        <asp:TextBox ID="txtqty" Text='<%# Eval("OpQty") %>' class="form-control" runat="server"></asp:TextBox>
                                                                                                                                                    </td>

                                                                                                                                                </tr>
                                                                                                                                            </ItemTemplate>
                                                                                                                                        </asp:ListView>


                                                                                                                                    </tbody>
                                                                                                                                </table>
                                                                                                                                <div class="actions btn-set">
                                                                                                                                    <asp:LinkButton ID="lbApproveIss" class="btn blue" runat="server">Save</asp:LinkButton>
                                                                                                                                    <asp:Button ID="Button2" runat="server" OnClientClick="close(this)" class="btn btn-default" data-dismiss="modal" aria-hidden="true" Text="Cancel" />
                                                                                                                                </div>
                                                                                                                            </div>

                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:HyperLink data-toggle="modal" role="button" ID="hlPeri" onclick="MultiSizecolor(this.id)" Visible='<%# Convert.ToBoolean(Eval("Perishable").ToString() == "True") %>' NavigateUrl='<%# string .Format ("{0}{1}", "#form_modalPeri", Eval("MyProdID") ) %>' runat="server">
                                                                                                    <asp:Label ID="lblPerishble" runat="server" Text="Add"></asp:Label>
                                                                                                </asp:HyperLink>
                                                                                                <div id='<%# string .Format ("{0}{1}", "form_modalPeri", Eval("MyProdID") ) %>' class="modal fade" tabindex="-1" role="dialog">
                                                                                                    <div class="row">
                                                                                                        <div class="col-md-12">
                                                                                                            <div class="portlet box purple">
                                                                                                                <div class="portlet-title">
                                                                                                                    <div class="caption">
                                                                                                                        <i class="fa fa-globe"></i>Perishable
                                                                                                                    </div>

                                                                                                                </div>
                                                                                                                <div class="portlet-body">
                                                                                                                    <div class="row">
                                                                                                                        <div class="col-md-2 form-group">
                                                                                                                        </div>
                                                                                                                        <div class="col-md-4 form-group">
                                                                                                                            <asp:Label ID="Label9" runat="server" Text="Batch Number"></asp:Label>
                                                                                                                            <asp:TextBox AutoCompleteType="Disabled" ID="txtPerBatchNo" CssClass="form-control"
                                                                                                                                runat="server" placeholder="Batch Number"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                        <div class="col-md-4 form-group">
                                                                                                                            <asp:Label ID="Label19" runat="server" Text="Product Date"></asp:Label>
                                                                                                                            <asp:TextBox AutoCompleteType="Disabled" ID="txtProDate" CssClass="form-control"
                                                                                                                                runat="server" placeholder="Product Date" onchange="checkdate(this)"></asp:TextBox>
                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtProDate" Format="dd/MM/yyyy" Enabled="True">
                                                                                                                            </cc1:CalendarExtender>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                    <div class="row">
                                                                                                                        <div class="col-md-2 form-group">
                                                                                                                        </div>
                                                                                                                        <div class="col-md-4 form-group">
                                                                                                                            <asp:Label ID="Label23" runat="server" Text="Expiry Date"></asp:Label>
                                                                                                                            <asp:TextBox AutoCompleteType="Disabled" ID="txtExpDate" CssClass="form-control"
                                                                                                                                runat="server" placeholder="Expiry Date" onchange="checkdate(this)"></asp:TextBox>
                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtExpDate" Format="dd/MM/yyyy" Enabled="True">
                                                                                                                            </cc1:CalendarExtender>
                                                                                                                        </div>
                                                                                                                        <div class="col-md-4 form-group">
                                                                                                                            <asp:Label ID="Label24" runat="server" Text="Lead to Destroy"></asp:Label>
                                                                                                                            <asp:TextBox AutoCompleteType="Disabled" ID="txtLead2Dest" CssClass="form-control"
                                                                                                                                runat="server" placeholder="Lead to distroy" onchange="checkdate(this)"></asp:TextBox>
                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtLead2Dest" Format="dd/MM/yyyy" Enabled="True">
                                                                                                                            </cc1:CalendarExtender>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                    <div class="actions btn-set">
                                                                                                                        <asp:LinkButton ID="LinkButton2" class="btn green-haze modalBackgroundbtn-circle" CommandArgument='<%# Eval("MYPRODID") %>' CommandName="btnPerishable" runat="server"> Save</asp:LinkButton>
                                                                                                                        <%--<%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                        <asp:Button ID="Button1" runat="server" OnClientClick="close(this)" class="btn btn-default" data-dismiss="modal" aria-hidden="true" Text="Cancel" />

                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                </div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:LinkButton ID="lnkbtndelete1" runat="server" CommandName="Delete" CommandArgument='<%# Eval("MYID") %>' OnClientClick="return confirm('Do you want to delete item?')" OnClick="lnkbtndelete1_Click">
                                                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="images/deleteRec.png" />
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <asp:HiddenField ID="hidMasterProID" runat="server" Value="" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </asp:Panel>
                                                <asp:Panel ID="pnlGridView" runat="server" CssClass="panelDisplay">
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
                                                                        <asp:Label ID="lblEdit" runat="server" Text="Receive"></asp:Label></th>
                                                                    <th>
                                                                        <asp:Label ID="lblDel" runat="server" Text="Request Delivery"></asp:Label></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="grdPO" runat="server" OnItemCommand="grdPO_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr class="gradeA">
                                                                            <td>
                                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblPSN" runat="server" Text='<%# getSuplierName(Convert .ToInt32 ( Eval("CUSTVENDID"))) %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblODate" runat="server" Text='<%# Eval("TRANSDATE", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("TOTAMT") %>'></asp:Label>
                                                                                 <asp:Label ID="lblStatus" Visible ="false"  runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblspnoo" runat="server" Text='<%# Eval("Status").ToString() == "SO" ? "Final" : "Darft"%>'></asp:Label>
                                                                                <asp:LinkButton ID="btnDarf" Visible ="false"   runat="server"  PostBackUrl ='<%#"Invoice.aspx?TactionNo="+ Eval("MYTRANSID") %>'>
                                                                                    Darft 
                                                                                </asp:LinkButton>
                                                                            </td>

                                                                            <td class="center">
                                                                                <asp:LinkButton ID="lnkbtnPQ" CommandName="EditIncom" class="btn default btn-xs purple" runat="server" CommandArgument='<%# Eval("MYTRANSID")  %>'>
                                                                                    Receive
                                                                                </asp:LinkButton></td>
                                                                            <td class="center">
                                                                                <asp:LinkButton ID="lnkbtndelete" runat="server" class="btn default btn-xs blue"  OnClientClick="return confirm('Do you want to Delivery Request?') " PostBackUrl = '<%#"DeliverNotes.aspx?TactionNo="+ Eval("MYTRANSID") %>' CommandArgument='<%# Eval("MYTRANSID") %>'>
                                                                                    Request Delivery
                                                                                </asp:LinkButton></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </asp:Panel>

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
