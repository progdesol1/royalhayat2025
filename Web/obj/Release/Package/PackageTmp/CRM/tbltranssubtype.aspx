<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="tbltranssubtype.aspx.cs" Inherits="Web.CRM.tbltranssubtype" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function showWarningToast() {
            var temp = document.getElementById("<%=drpTranType.ClientID %>").selectedIndex;
            if (temp == "0") {
                var message = 'Localize(TranType)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
            var temp = document.getElementById("<%=txttranssubtype1.ClientID %>").value;
            if (temp == "") {
                var message = 'Localize(transsubtype1)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
        }
        function showSuccessToast() {
            var message = 'Localize(TranSCreated)';
            $().toastmessage('showSuccessToast', message);
        }
        function showSuccessToast1() {
            var message = 'Localize(TranSUpdated)';
            $().toastmessage('showSuccessToast', message);
        }
        function ClearAllText() {
            var a = (confirm('Are you sure you want to clear all data?'))
            if (a == true) {
                document.getElementById('ContentPlaceHolder1_drpTranType').selectedIndex = "0";
                document.getElementById('ContentPlaceHolder1_txttranssubtype1').value = "";
                document.getElementById('ContentPlaceHolder1_txttranssubtype2').value = "";
                document.getElementById('ContentPlaceHolder1_txttranssubtype3').value = "";
                document.getElementById('ContentPlaceHolder1_drpOpQtyBeh').selectedIndex = "0";
                document.getElementById('ContentPlaceHolder1_drpOnHandBeh').selectedIndex = "0";
                document.getElementById('ContentPlaceHolder1_drpQtyOutBeh').selectedIndex = "0";
                document.getElementById('ContentPlaceHolder1_drpQtyConsumedBeh').selectedIndex = "0";
                document.getElementById('ContentPlaceHolder1_drpQtyReservedBeh').selectedIndex = "0";
                document.getElementById('ContentPlaceHolder1_txtQtyAtDestination').value = "";
                document.getElementById('ContentPlaceHolder1_txtQtyAtSource').value = "";
                document.getElementById('ContentPlaceHolder1_txtserialno').value = "";
                document.getElementById('ContentPlaceHolder1_txtyears').value = "";
                document.getElementById('ContentPlaceHolder1_btnSubmit').innerHTML = "Submit";
                return false;
            }
        }
    </script>
    <div>
       <%-- <ul class="page-breadcrumb breadcrumb">
            <li>
                <a href="index.aspx">CRM</a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <a href="#">Tbl Transsubtype</a>
            </li>

        </ul>--%>
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <div class="portlet box blue-hoki">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-gift"></i>
                                    <asp:Label ID="Label28" runat="server" Text="ICEXTRACOST"></asp:Label>
                                </div>
                                <div class="tools">
                                    <a href="javascript:;" class="collapse"></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                    <a href="javascript:;" class="reload"></a>
                                    <a href="javascript:;" class="remove"></a>
                                </div>
                                <div class="actions btn-set">
                                    <asp:LinkButton ID="btnSubmit" data-placement="top" data-toggle="tooltip" OnClientClick="return showWarningToast();" OnClick="btnSubmit_Click" ToolTip="Submit Data" runat="server" class="btn btn-success" Text="Submit" />
                                    <asp:LinkButton ID="btnCancel" OnClientClick="ClearAllText()" data-placement="top" data-toggle="tooltip" ToolTip="Exit Data" runat="server" class="btn btn-danger" Text="Exit" /><%--trash-o--%>
                                </div>
                            </div>
                            <div class="portlet-body">

                                <div class="tabbable">
                                    <div class="tab-content no-space">
                                        <div class="tab-pane active" id="tab_general1">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lbltransid" runat="server" Text="Transaction Type"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="drpTranType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lbltranssubtype1" runat="server" Text="Transaction Sub Type1"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txttranssubtype1" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lbltranssubtype2" runat="server" Text="Transaction Sub Type2"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txttranssubtype2" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lbltranssubtype3" runat="server" Text="Transaction Sub Type3"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txttranssubtype3" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lblOpQtyBeh" runat="server" Text="OpQtyBeh"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="drpOpQtyBeh" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="+">+</asp:ListItem>
                                                            <asp:ListItem Value="-">-</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lblOnHandBeh" runat="server" Text="OnHandBeh"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="drpOnHandBeh" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="+">+</asp:ListItem>
                                                            <asp:ListItem Value="-">-</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lblQtyOutBeh" runat="server" Text="QtyOutBeh"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="drpQtyOutBeh" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="+">+</asp:ListItem>
                                                            <asp:ListItem Value="-">-</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lblQtyConsumedBeh" runat="server" Text="QtyConsumedBeh"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="drpQtyConsumedBeh" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="+">+</asp:ListItem>
                                                            <asp:ListItem Value="-">-</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lblQtyReservedBeh" runat="server" Text="QtyReservedBeh"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="drpQtyReservedBeh" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="+">+</asp:ListItem>
                                                            <asp:ListItem Value="-">-</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lblQtyAtDestination" runat="server" Text="QtyAtDestination"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtQtyAtDestination" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="2"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lblQtyAtSource" runat="server" Text="QtyAtSource"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtQtyAtSource" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="2"></asp:TextBox>
                                                    </div>
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lblserialno" runat="server" Text="Serial no"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtserialno" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lblyears" runat="server" Text="Years"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtyears" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="portlet-body">
                                <div class="tabbable">
                                    <table class="table table-striped table-bordered table-hover" id="sample_1">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:Label ID="lblSN" runat="server" Text="#"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="lblGrEdit" runat="server" Text="Action"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="lblGRHtransid" runat="server" Text="Transaction Type"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHtranssubtype1" runat="server" Text="Subtype1"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHtranssubtype2" runat="server" Text="Subtype2"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHtranssubtype3" runat="server" Text="Subtype3"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHActive" runat="server" Text="Active"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblHAction" runat="server" Text="Action"></asp:Label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="grdmstr" runat="server">
                                                <LayoutTemplate>
                                                    <tr id="ItemPlaceholder" runat="server">
                                                    </tr>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>' meta:resourcekey="lblSRNOResource2"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <div class="btn-group">
                                                                <a data-toggle="dropdown" href="#" class="btn btn-sm blue dropdown-toggle" style="width: 60px;">
                                                                    <asp:Label ID="Label19" runat="server" Text="Action" meta:resourcekey="Label19Resource1"></asp:Label>
                                                                    <i class="fa fa-angle-down"></i>
                                                                </a>
                                                                <ul class="dropdown-menu">
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkbtn" runat="server" CommandArgument='<%# Eval("TenantId") + "," +Eval("transsubid") %>' OnClick="lnkbtn_Click">
                                                                            <i class="fa fa-pencil"></i>
                                                                            <asp:Label ID="Label73" runat="server" Text="Edit" meta:resourcekey="Label73Resource1"></asp:Label>
                                                                        </asp:LinkButton>

                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("TenantId") + "," +Eval("transsubid") %>' OnClientClick="return confirm('Do you want to delete this transaction sub type?')" OnClick="lnkbtndelete_Click">
                                                                            <i class="fa fa-pencil"></i>
                                                                            <asp:Label ID="Label74" runat="server" Text="Delete" meta:resourcekey="Label74Resource1"></asp:Label>
                                                                        </asp:LinkButton>

                                                                    </li>

                                                                </ul>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCtransid" runat="server" Text='<%# Eval("transtype") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCtranssubtype1" runat="server" Text='<%# Eval("transsubtype1") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCtranssubtype2" runat="server" Text='<%# Eval("transsubtype2") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCtranssubtype3" runat="server" Text='<%# Eval("transsubtype3") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCActive" runat="server" Text='<%# Eval("Active") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkAction" Style="color: #5b9bd1" runat="server" CommandArgument='<%# Eval("TenantId") + "," +Eval("Active")  + "," +Eval("transsubid")  %>' OnClick="lnkAction_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </tbody>
                                    </table>
                                    <asp:HiddenField ID="hidId" runat="server" Value="" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
