<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="DeliverNotes.aspx.cs" Inherits="Web.Sales.DeliverNotes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type = "text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlform.ClientID %>");
             var printWindow = window.open('', '', 'height=400,width=800');
             printWindow.document.write('<html><head><title>DIV Contents</title>');
             printWindow.document.write('</head><body >');
             printWindow.document.write(panel.innerHTML);
             printWindow.document.write('</body></html>');
             printWindow.document.close();
             setTimeout(function () {
                 printWindow.print();
             }, 500);
             return false;
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
    <div  id="b" runat="server">
        <div class="page-head">
            <ol class="breadcrumb">
                <li><a href="../ECOMM/AdminIndex.aspx">
                    <asp:Label ID="lblDisPro" runat="server" Text="Home"></asp:Label></a><i class="fa fa-circle"></i></li>
                <li><a href="#">
                    <asp:Label ID="lblDisProMst" runat="server" Text="Delivery Request"></asp:Label></a></li>

            </ol>

        </div>
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Delivery Request
                                       
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="btnPagereload" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                       <%-- <asp:Button ID="btnprint" class="btn btn-info" runat="server" Style="height: 30px;" Text="Print Delivery " />--%>
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general">
                                                <asp:Panel ID="pnlform" runat="server">
                                                    <div class="form-body">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="Label1" Text="Main Transaction Type" class="col-md-4 control-label"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpMainTran" runat="server" Enabled="false" OnSelectedIndexChanged="drpMainTran_SelectedIndexChanged" AutoPostBack="true" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="Label2" Text="Sub Transaction Type" class="col-md-4 control-label"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpSubTra" runat="server" Enabled="false" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="Label3" Text="User Batch Name" class="col-md-4 control-label"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpBatch" runat="server" Enabled="false" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="Label4" Text="Project Name" class="col-md-4 control-label"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpProjectName" runat="server" Enabled="false" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTransDate" Text="Date of Transaction" class="col-md-4 control-label"></asp:Label><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtTRANSDATE" Enabled="false" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxTRANSDATE_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtTRANSDATE" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTRANSDATE" runat="server" ControlToValidate="txtTRANSDATE" ErrorMessage="Date of Transaction Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtTRANSDATE" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="txtrefresh" Text="Reference" class="col-md-4 control-label"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox  ID="txtrefrshno" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="tblnote" Text="Notes" class="col-md-4 control-label"></asp:Label><div class="col-md-8">
                                                                        <asp:TextBox TextMode="MultiLine" ID="txtNOTES" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                           
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="portlet box purple">
                                                                <div class="portlet-title">
                                                                    <div class="caption">
                                                                        <i class="fa fa-globe"></i>Request Delivery
                                                                    </div>
                                                                    <div class="actions btn-set">
                                                                        <asp:Button ID="btnsave" class="btn btn-info" runat="server" Style="height: 30px;" Text="Save" OnClick="btnsave_Click" />
                                                                         <a class="btn btn-lg blue hidden-print margin-bottom-5" onclick="javascript:window.print();">Print Delivery<i class="fa fa-print"></i></a>
                                                                    </div>
                                                                </div>
                                                                <div class="portlet-body">
                                                                    <asp:Panel ID="pnlSuccessMsg123" runat="server" Visible="false">
                                                                        <div class="alert alert-danger alert-dismissable">
                                                                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                            <asp:Label ID="lblMsg123" runat="server"></asp:Label>
                                                                        </div>
                                                                    </asp:Panel>
                                                                    <table class="table table-striped table-bordered table-hover">

                                                                        <thead class="repHeader">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:Label ID="Label22" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                <th>
                                                                                    <asp:Label ID="Label23" runat="server" Text="Location" meta:resourcekey="lblSNameResource2"></asp:Label></th>
                                                                                <th>
                                                                                    <asp:Label ID="Label26" runat="server" Text="Item Code" meta:resourcekey="lblProOnameResource1"></asp:Label></th>
                                                                                <th>
                                                                                    <asp:Label ID="Label38" runat="server" Text="Item Name" meta:resourcekey="lblProOnameResource1"></asp:Label></th>
                                                                                <th>
                                                                                    <asp:Label ID="Label39" runat="server" Text="Qty" meta:resourcekey="lblProOnameResource1"></asp:Label></th>
                                                                                <th>
                                                                                    <asp:Label ID="Label40" runat="server" Text="Date" meta:resourcekey="lblProOnameResource1"></asp:Label></th>
                                                                                <th>
                                                                                    <asp:Label ID="Label43" runat="server" Text="Total" meta:resourcekey="lblProOnameResource1"></asp:Label></th>
                                                                                <th>
                                                                                    <asp:Label ID="Label41" runat="server" Text="Received" meta:resourcekey="lblProOnameResource1"></asp:Label></th>
                                                                                <th>
                                                                                    <asp:Label ID="Label42" runat="server" Text="Rejected" meta:resourcekey="lblProOnameResource1"></asp:Label></th>


                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:ListView ID="listresivDelvry" runat="server">
                                                                                <ItemTemplate>
                                                                                    <tr class="gradeA">
                                                                                        <td>
                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label44" runat="server" Text='<%#getconpniname(Convert .ToInt32(  Eval("DelFlag"))) %>'></asp:Label>
                                                                                            <asp:Label ID="lbllocation" Visible="false" runat="server" Text='<%#Eval("DelFlag") %>'></asp:Label>
                                                                                            <asp:Label ID="lbltrction" Visible="false" runat="server" Text='<%#Eval("MYTRANSID") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label45" runat="server" Text='<%#getitemscode(Convert.ToInt32(Eval("MyProdID"))) %>'></asp:Label>
                                                                                            <asp:Label ID="lblproduct" Visible="false" runat="server" Text='<%#Eval("MyProdID") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label46" runat="server" Text='<%#getproductName (Convert.ToInt32(Eval("MyProdID"))) %>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtuomQty" class="form-control" Text='<%# Eval ("QUANTITY") %>' runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtDateDievery" class="form-control" runat="server"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateDievery" Format="dd/MM/yyyy" Enabled="True">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lbltotal" runat="server" Text='<%#  Eval("AMOUNT") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label5" runat="server"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label6" runat="server"></asp:Label>
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
                                                </asp:Panel>
                                                <asp:Panel ID="pnlGridView" runat="server" CssClassclass="portlet-body" Visible="false">

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
                                                                            <asp:Label ID="lblStatus" Visible="false" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblspnoo" runat="server" Text='<%# Eval("Status").ToString() == "DSO" ? "Darft" :"Final" %>'></asp:Label>
                                                                            <asp:LinkButton ID="btnDarf" Visible="false" runat="server" PostBackUrl='<%#"Invoice.aspx?TactionNo="+ Eval("MYTRANSID") %>'>
                                                                                    Darft 
                                                                            </asp:LinkButton>
                                                                        </td>

                                                                        <td class="center">
                                                                            <asp:LinkButton ID="lnkbtnPQ" CommandName="EditIncom" class="btn default btn-xs purple" runat="server" CommandArgument='<%# Eval("MYTRANSID")  %>'>
                                                                                     Request Delivery
                                                                            </asp:LinkButton></td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>

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
