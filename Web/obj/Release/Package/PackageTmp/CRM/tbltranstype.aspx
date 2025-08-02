<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="tbltranstype.aspx.cs" Inherits="Web.CRM.tbltranstype" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function showWarningToast() {
            var temp = document.getElementById("<%=drpMysysname.ClientID %>").selectedIndex;
            if (temp == "0") {
                var message = 'Localize(Mysysname)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
            var temp = document.getElementById("<%=txtinoutSwitch.ClientID %>").value;
            if (temp == "") {
                var message = 'Localize(inoutswitch)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
            var temp = document.getElementById("<%=txttranstype1.ClientID %>").value;
            if (temp == "") {
                var message = 'Localize(trantype1)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
        }
        function showSuccessToast() {
            var message = 'Localize(TranCreated)';
            $().toastmessage('showSuccessToast', message);
        }
        function showSuccessToast1() {
            var message = 'Localize(TranUpdated)';
            $().toastmessage('showSuccessToast', message);
        }
        function ClearAllText() {
            var a = (confirm('Are you sure you want to clear all data?'))
            if (a == true) {
                document.getElementById('ContentPlaceHolder1_drpMysysname').selectedIndex = "0";
                document.getElementById('ContentPlaceHolder1_txtinoutSwitch').value = "";
                document.getElementById('ContentPlaceHolder1_txttranstype1').value = "";
                document.getElementById('ContentPlaceHolder1_txttranstype2').value = "";
                document.getElementById('ContentPlaceHolder1_txttranstype3').value = "";
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
                <a href="#">Tbl Transtype</a>
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
                                    <asp:Label ID="Label28" runat="server" Text="Tbl Transtype"></asp:Label>
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
                                                        <asp:Label ID="lbltranstype1" runat="server" Text="Transaction type1"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txttranstype1" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lbltranstype2" runat="server" Text="Transaction type2"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txttranstype2" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lbltranstype3" runat="server" Text="Transaction type3"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txttranstype3" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lblMYSYSNAME" runat="server" Text="System"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="drpMysysname" runat="server" CssClass="select2"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">
                                                        <asp:Label ID="lblinoutSwitch" runat="server" Text="in out Switch"></asp:Label>
                                                        <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtinoutSwitch" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="1"></asp:TextBox>
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
                                                    <asp:Label ID="lblGRHMYSYSNAME" runat="server" Text="System"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHinoutSwitch" runat="server" Text="in out Switch"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHtranstype1" runat="server" Text="Transaction type1"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHtranstype2" runat="server" Text="Transaction type2"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHtranstype3" runat="server" Text="Transaction type3"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHserialno" runat="server" Text="Serial no"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblGRHyears" runat="server" Text="Years"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblHActive" runat="server" Text="Active"></asp:Label></th>
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
                                                                        <asp:LinkButton ID="lnkbtn" runat="server" CommandArgument='<%# Eval("TenantId") + "," +Eval("transid") %>' OnClick="lnkbtn_Click">
                                                                            <i class="fa fa-pencil"></i>
                                                                            <asp:Label ID="Label73" runat="server" Text="Edit" meta:resourcekey="Label73Resource1"></asp:Label>
                                                                        </asp:LinkButton>

                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("TenantId") + "," +Eval("transid") %>' OnClientClick="return confirm('Do you want to delete this role?')" OnClick="lnkbtndelete_Click">
                                                                            <i class="fa fa-pencil"></i>
                                                                            <asp:Label ID="Label74" runat="server" Text="Delete" meta:resourcekey="Label74Resource1"></asp:Label>
                                                                        </asp:LinkButton>

                                                                    </li>

                                                                </ul>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCMYSYSNAME" runat="server" Text='<%# Eval("MYSYSNAME") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCinoutSwitch" runat="server" Text='<%# Eval("inoutSwitch") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCtranstype1" runat="server" Text='<%# Eval("transtype1") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCtranstype2" runat="server" Text='<%# Eval("transtype2") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCtranstype3" runat="server" Text='<%# Eval("transtype3") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCserialno" runat="server" Text='<%# Eval("serialno") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCyears" runat="server" Text='<%# Eval("years") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGRCActive" runat="server" Text='<%# Eval("Active") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkAction" Style="color: #5b9bd1" runat="server" CommandArgument='<%# Eval("TenantId") + "," +Eval("Active")  + "," +Eval("transid")  %>' OnClick="lnkAction_Click"></asp:LinkButton>
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
