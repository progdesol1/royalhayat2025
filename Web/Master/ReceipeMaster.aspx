<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="ReceipeMaster.aspx.cs" Inherits="Web.Master.ReceipeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .txtcenter {
            text-align: center;
        }
    </style>

    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 46) {
                var inputValue = $("#inputfield").val()
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function openModalsmall2() {
            $('#small2').modal('show');
            
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <!-- BEGIN PORTLET-->
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-gift"></i>Multiple Select
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                        <a href="javascript:;" class="reload"></a>
                        <a href="javascript:;" class="remove"></a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <!-- BEGIN FORM-->
                    <div class="form-horizontal form-row-seperated">
                        <div class="form-body">
                            <%-- <div class="form-group">--%>


                            <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                                <div class="alert alert-success alert-dismissable">
                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlErrorMsg" runat="server" Visible="false">
                                <div class="alert alert-danger alert-dismissable">
                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                    <asp:Label ID="lblerrorMsg" runat="server"></asp:Label>
                                </div>
                            </asp:Panel>




                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="control-label col-md-3" Text="Select Receipe"></asp:Label>
                                        <%--<label class="control-label col-md-3">Select Receipe</label>--%>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="drpReceipe" runat="server" CssClass="form-control select2me" AutoPostBack="true" OnSelectedIndexChanged="drpReceipe_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorReceipe_Arabic" runat="server" ControlToValidate="drpReceipe" InitialValue="0" ErrorMessage="Receipe Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" Text="Submit" ValidationGroup="submit" OnClick="btnAdd_Click" />

                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="col-md-5">


                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="portlet box green-haze">
                                                        <div class="portlet-title">
                                                            <div class="caption">
                                                                <i class="fa fa-globe"></i>
                                                                <asp:Label ID="Label3" runat="server" Text="Items List" Style="font-weight: bolder; font-size: large;"></asp:Label>
                                                            </div>
                                                            <div class="tools">
                                                            </div>
                                                        </div>
                                                        <div class="portlet-body">
                                                            <asp:ListBox ID="ListBox1" runat="server" CssClass="multi-select" Style="height: 500px; width: 100%" Font-Size="Large" SelectionMode="Multiple" ToolTip="Items">
                                                            </asp:ListBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>

                                        <center>

                                        <div class="col-md-1" style="height: 95PX;margin-top: 96px;">
                                            
                                            <asp:LinkButton ID="lnkInput" runat="server" CssClass="btn yellow-gold btn-circle" OnClick="lnkInput_Click" Text="Input "> </asp:LinkButton>

                                        </div>

                                            </center>

                                        <div class="col-md-6">

                                            <%--<asp:ListBox ID="ListBox2" runat="server" SelectionMode="Multiple" Style="height: 400px; width: 90%;"></asp:ListBox>--%>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="portlet box yellow-gold">
                                                        <div class="portlet-title">
                                                            <div class="caption">
                                                                <i class="fa fa-globe"></i>
                                                                <asp:Label ID="Label2" runat="server" Text="Input List" Style="font-weight: bolder; font-size: large;"></asp:Label>
                                                            </div>
                                                            <div class="tools">
                                                            </div>
                                                        </div>
                                                        <div class="portlet-body" style="overflow: scroll; height: 350px; padding: 0px">
                                                            <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="width: 10%;">
                                                                            <asp:Label runat="server" ID="Label6" Text="Product ID #"></asp:Label></th>

                                                                        <th>
                                                                            <asp:Label runat="server" ID="lblhReceipe_English" Text="Product Name"></asp:Label></th>

                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label7" Text="UOM"></asp:Label></th>
                                                                        <th style="width: 60px;">
                                                                            <asp:Label runat="server" ID="Label10" Text="Qty"></asp:Label></th>

                                                                        <th style="width: 70px;">ACTION</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" OnItemDataBound="Listview1_ItemDataBound">
                                                                        <LayoutTemplate>
                                                                            <tr id="ItemPlaceholder" runat="server">
                                                                            </tr>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblProdID" runat="server" Text='<%# Eval("MYPRODID")%>'></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblReceipe_English" runat="server" Text='<%# Eval("ProdName1")%>'></asp:Label></td>

                                                                                <td>
                                                                                    <asp:DropDownList ID="drpUOM" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="drpUOM_SelectedIndexChanged"></asp:DropDownList>
                                                                                    <asp:Label ID="lblUOM" Visible="false" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>

                                                                                </td>

                                                                                <td>
                                                                                    <asp:TextBox ID="txtQTY" runat="server" Width="100%" CssClass="txtcenter" Text="1" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                                    <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtQTY" FilterType="Custom, Numbers" ValidChars="." runat="server" />--%>
                                                                                </td>


                                                                                <td>
                                                                                    <asp:LinkButton ID="btnDelete" CommandName="btnDelete" CommandArgument='<%# Eval("MYPRODID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>

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


                                        </div>

                                        <center>

                                        <div class="col-md-1" style="height: 95PX;margin-top: 96px;">

                                            <asp:LinkButton ID="lnkOutput" runat="server" CssClass="btn blue btn-circle" OnClick="lnkOutput_Click" Text ="Output"> Output </asp:LinkButton>

                                        </div>

                                               </center>

                                        <div class="col-md-6">

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="portlet box blue">
                                                        <div class="portlet-title">
                                                            <div class="caption">
                                                                <i class="fa fa-globe"></i>
                                                                <asp:Label ID="Label4" runat="server" Text="OutPut List" Style="font-weight: bolder; font-size: large;"></asp:Label>
                                                            </div>
                                                            <div class="tools">
                                                            </div>
                                                        </div>
                                                        <div class="portlet-body">



                                                            <table class="table table-striped table-bordered table-hover" id="sample_3">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="width: 10%;">
                                                                            <asp:Label runat="server" ID="Label8" Text="Product ID #"></asp:Label></th>

                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label9" Text="Product Name"></asp:Label></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label5" Text="UOM"></asp:Label></th>
                                                                        <th style="width: 60px;">
                                                                            <asp:Label runat="server" ID="Label11" Text="Qty"></asp:Label></th>

                                                                        <th style="width: 70px;">ACTION</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:ListView ID="Listview2" runat="server" OnItemCommand="Listview2_ItemCommand" OnItemDataBound="Listview2_ItemDataBound">
                                                                        <LayoutTemplate>
                                                                            <tr id="ItemPlaceholder" runat="server">
                                                                            </tr>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblProdID" runat="server" Text='<%# Eval("MYPRODID")%>'></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblReceipe_English" runat="server" Text='<%# Eval("ProdName1")%>'></asp:Label></td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drpUOM" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="drpUOM_SelectedIndexChanged"></asp:DropDownList>
                                                                                    <asp:Label ID="lblUOM" Visible="false" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtQTY" runat="server" Width="100%" CssClass="txtcenter" Text="1" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                                    <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtQTY" FilterType="Custom, Numbers" ValidChars="." runat="server" />--%>
                                                                                </td>

                                                                                <td>
                                                                                    <asp:LinkButton ID="btnDelete" CommandName="btnDelete" CommandArgument='<%# Eval("MYPRODID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>

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

                                        </div>
                                    </div>
                                </div>

                            </div>
                            <%--</div>--%>
                        </div>

                    </div>
                    <!-- END FORM-->
                </div>
            </div>
            <!-- END PORTLET-->
        </div>
    </div>

    <div class="modal fade bs-modal-sm" id="small2" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm" style="margin-bottom: 0px; margin-top: 0px;">
            <div class="modal-content">
                <div class="portlet box green">
                    <div class="modal-header" style="padding-top: 10px; padding-bottom: 10px;">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title" style="color: white;"><i class="fa fa-save"></i>&nbsp;Success</h4>
                    </div>
                </div>
                <div class="modal-body">
                    <p id="lblmsgpop2" style="text-align: center; font-family: 'Courier New';">Receipe save sucessfully...</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn default" data-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
