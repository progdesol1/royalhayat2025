<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="ProductItemRequest.aspx.cs" Inherits="Web.Master.ProductItemRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding-left: 10px; padding-right: 10px;">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet box green-haze">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-globe"></i>
                            <asp:Label runat="server" ID="Label5" Text="List Kitchen"></asp:Label>
                            <asp:DropDownList ID="Dateformate" CssClass="form-control select2me input-small" runat="server" DataTextFormatString="{0:dd/MMM/yyyy}" AutoPostBack="true" OnSelectedIndexChanged="Dateformate_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="tools">
                            <a href="javascript:;" class="collapse"></a>
                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                            <a href="javascript:;" class="reload"></a>
                            <a href="javascript:;" class="remove"></a>
                        </div>
                    </div>

                    <div class="portlet-body">

                        <table class="table table-striped table-bordered table-hover" id="sample_1">
                            <thead>
                                <tr>

                                    <th>
                                        <asp:Label runat="server" Font-Size="Large" ID="Label20" Text="Delivery Date"></asp:Label></th>
                                    <th>
                                        <asp:Label runat="server" Font-Size="Large" ID="Label22" Text="Product"></asp:Label></th>
                                    <th>
                                        <asp:Label ID="Label13" runat="server" Text="UOM"></asp:Label>
                                    </th>
                                    <th style="display:none;">
                                        <asp:Label runat="server" Font-Size="Large" ID="Label23" Text="Normal"></asp:Label></th>
                                    <th style="display:none;">
                                        <asp:Label runat="server" Font-Size="Large" ID="Label24" Text="100grms"></asp:Label></th>
                                    <th style="display:none;">
                                        <asp:Label runat="server" Font-Size="Large" ID="Label25" Text="150grms"></asp:Label></th>
                                    <th style="display:none;">
                                        <asp:Label runat="server" Font-Size="Large" ID="Label26" Text="200grms"></asp:Label></th>
                                    <th>
                                        <asp:Label ID="Label12" Font-Size="Large" runat="server" Text="QTY"></asp:Label>
                                    </th>

                                </tr>
                            </thead>
                            <tbody>
                                <asp:ListView ID="Listview1" runat="server" OnItemDataBound="Listview1_ItemDataBound">


                                    <LayoutTemplate>
                                        <tr id="ItemPlaceholder" runat="server">
                                        </tr>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label113" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpectedDelDate")).ToString("dd/MMM/yyyy") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label23" runat="server" Text='<%# Eval("Product")+" - "+Eval("ProductName") %>'></asp:Label>
                                                <asp:Label ID="kitchProdid" Visible="false" runat="server" Text='<%# Eval("Product") %>'></asp:Label>
                                                <asp:Label ID="KUOM" runat="server" Visible="false" Text='<%# Eval("UOM") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label14" runat="server" Text='<%# GetUOM(Convert.ToInt32(Eval("UOM"))) %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center;display:none;">
                                                <asp:Label ID="lblNormal" runat="server" Font-Size="Medium" Text='<%# Eval("NormalQty") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center;display:none;">
                                                <asp:Label ID="lblQty100" runat="server" Font-Size="Medium" Text='<%# Eval("Qty150") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center;display:none;">
                                                <asp:Label ID="lblQty150" runat="server" Font-Size="Medium" Text='<%# Eval("Qty200") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center;display:none;">
                                                <asp:Label ID="lblQty200" runat="server" Font-Size="Medium" Text='<%# Eval("Qty250") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="KitchenQTY" runat="server" Text="0"></asp:Label>
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

        <div class="row">
            <div class="col-md-12">
                <div class="portlet box blue-dark">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-globe"></i>
                            <asp:Label runat="server" ID="Label1"></asp:Label>
                            List Recipe
                        </div>
                        <div class="tools">
                            <a href="javascript:;" class="collapse"></a>
                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                            <a href="javascript:;" class="reload"></a>
                            <a href="javascript:;" class="remove"></a>
                        </div>
                        <div class="actions btn-set">
                            <asp:LinkButton ID="btnRequest" runat="server" CssClass="btn btn-sm blue" Text="Request for Material" OnClick="btnRequest_Click"></asp:LinkButton>
                            <asp:LinkButton ID="btnAcceptAll" runat="server" Visible="false" CssClass="btn btn-sm btn-success" Text="Accept All" OnClick="btnAcceptAll_Click"></asp:LinkButton>
                            <a class="btn btn-sm btn-info" data-toggle="modal" href="#responsive">Add Extra Item</a>
                        </div>
                    </div>

                    <div class="portlet-body">

                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>

                                    <th>
                                        <asp:Label runat="server" Font-Size="Large" ID="Label2">Recipe<br /> item</asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" Font-Size="Large" ID="Label3">Recipe<br /> Input</asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label ID="Label8" Font-Size="Large" runat="server">Product<br /> OutPut</asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" Font-Size="Large" ID="Label4">Qty</asp:Label></th>
                                    <th>
                                        <asp:Label runat="server" Font-Size="Large" ID="Label6">UOM</asp:Label></th>
                                    <th>
                                        <asp:Label runat="server" Font-Size="Large" ID="Label10">Request<br /> QTY</asp:Label></th>
                                    <th>
                                        <asp:Label runat="server" Font-Size="Large" ID="Label11">Receive<br /> QTY</asp:Label></th>
                                    <%-- <th>
                                    <asp:Label ID="Label10" Font-Size="Large" runat="server"></asp:Label>Request<br /> QTY</th>
                                <th>
                                    <asp:Label ID="Label11" Font-Size="Large" runat="server"></asp:Label>Receive<br />QTY</th>--%>
                                    <th>
                                        <asp:Label ID="lblRequestStatus" Font-Size="Large" runat="server">Request<br /> Status</asp:Label>
                                    </th>

                                </tr>
                            </thead>
                            <tbody>
                                <asp:ListView ID="Listview2" runat="server" OnItemDataBound="Listview2_ItemDataBound" OnItemCommand="Listview2_ItemCommand">


                                    <LayoutTemplate>
                                        <tr id="ItemPlaceholder" runat="server">
                                        </tr>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>

                                            <td>
                                                <asp:Label ID="R1" runat="server" Text='<%# GetReceipy(Convert.ToInt32(Eval("recNo"))) %>'></asp:Label>
                                                <asp:DropDownList ID="DuplicateRec" Visible="false" runat="server" CssClass="form-control input-medium select2me"></asp:DropDownList>
                                                <asp:Label ID="lblrecNo" runat="server" Visible="false" Text='<%# Eval("recNo") %>'></asp:Label>
                                                <asp:Label ID="lblIOswitch" runat="server" Visible="false" Text='<%# Eval("IOSwitch") %>'></asp:Label>
                                                <asp:DropDownList ID="MoreReceipe" CssClass="form-control select2me" Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="MoreReceipe_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="R2" runat="server" Text='<%# GetProd(Convert.ToInt32(Eval("ItemCode"))) %>'></asp:Label>
                                                <asp:Label ID="ProdIID" runat="server" Visible="false" Text='<%# Eval("ItemCode") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text='<%# getOutPut(Convert.ToInt32(Eval("recNo"))) %>'></asp:Label>
                                                <asp:Label ID="output" runat="server" Visible="false" Text='<%# OutPut(Convert.ToInt32(Eval("recNo"))) %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="R3" Visible="false" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                <%--<asp:Label ID="FinalTot" runat="server" Text=""></asp:Label>--%>
                                                <asp:TextBox ID="FinalTot" runat="server" CssClass="form-control input-xsmall"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="R4" runat="server" Text='<%#GetUOM(Convert.ToInt32(Eval("UOM"))) %>'></asp:Label>
                                                <asp:Label ID="lbluom" runat="server" Visible="false" Text='<%#Eval("UOM")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="RequestQTY" runat="server" Text="0.000"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="ReceiveQTY" runat="server" Text="0.000"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                <asp:LinkButton ID="LinkAccept" runat="server" Visible="false" CommandName="LinkAccept" CommandArgument='<%# Eval("TenentID") +"-"+ Eval("recNo") +"-"+ Eval("IOSwitch") +"-"+ Eval("ItemCode") +"-"+ Eval("UOM") %>' Text="Accept" CssClass="btn btn-sm btn-success" Style="font-weight: bold; padding: 1px 5px 2px 5px"></asp:LinkButton>
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
        <asp:Panel ID="pnlissue" runat="server" Visible="false">
            <div class="row">
                <div class="col-md-12 col-sm-12">
                    <!-- BEGIN PORTLET-->
                    <div class="portlet box yellow tasks-widget">
                        <div class="portlet-title">
                            <div class="caption caption-md">
                                <i class="icon-bar-chart theme-font-color hide"></i>
                                <span class="caption-subject bold uppercase">Recipe Management Issue</span>
                                <span class="caption-helper" style="color: #345466;">(
                                    <asp:Label ID="ISUCOUNT" runat="server" Text="0"></asp:Label>
                                    Issue )</span>
                            </div>

                        </div>
                        <div class="portlet-body">
                            <div class="task-content">
                                <div class="scroller" style="height: 282px;" data-always-visible="1" data-rail-visible1="0" data-handle-color="#D7DCE2">
                                    <!-- START TASK LIST -->
                                    <ul class="task-list">
                                        <asp:ListView ID="ReceipyIssue" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <div class="task-title">

                                                        <span class="task-title-sp">This item Recipe Is Not Available <%# "<b><u>"+ GetProd(Convert.ToInt32(Eval("ItemCode"))) +"</u></b>" %> and UOM <%#  "<b><u>"+ GetUOM(Convert.ToInt32(Eval("UOM"))) +"</u></b>" %> </span>
                                                        <div style="float: right;">
                                                            <%--<span class="label label-sm label-success">Company</span>--%>
                                                            <span class="task-bell">
                                                                <i class="fa fa-bell-o"></i>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ul>
                                    <!-- END START TASK LIST -->
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- END PORTLET-->
                </div>

            </div>
        </asp:Panel>

    </div>
    <div id="responsive" class="modal" tabindex="-1" data-width="760" aria-hidden="true" style="margin-top: 0px; top: 310px;">
        <div class="portlet box yellow">
            <div class="modal-header" style="padding-top: 10px; border-bottom-width: 0px;">
                <button type="button" data-dismiss="modal" class="btn btn-danger" style="float: right;">Close</button>
                <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>--%>
                <h4 class="modal-title">Extra Item</h4>
            </div>
        </div>
        <div class="modal-body">
            <div class="row">
                <div class="col-md-12">
                    <table class="table table-striped table-bordered table-hover" id="sample_3">
                        <thead>
                            <tr>
                                <th style="width: 200px;">Item</th>
                                <th>UOM</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:ListView ID="ListItemExtra" runat="server" OnItemDataBound="ListExtra_ItemDataBound" OnItemCommand="ListItemExtra_ItemCommand">
                                <LayoutTemplate>
                                    <tr id="ItemPlaceholder" runat="server">
                                    </tr>
                                </LayoutTemplate>
                                <ItemTemplate>
                                     <tr>
                                <td>
                                    <asp:Label ID="lblExtProd" runat="server" Text='<%# Eval("ProdName1") %>'></asp:Label>
                                    <asp:Label ID="lblextProdID" Visible="false" runat="server" Text='<%# Eval("MYPRODID") %>'></asp:Label>
                                    <asp:Label ID="lblExtUOMID" Visible="false" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpExtUOM" CssClass="form-control select2me input-small" runat="server"></asp:DropDownList></td>
                                <td style="text-align:center;">
                                    <asp:LinkButton ID="LinkExtAction" runat="server" CssClass="btn btn-sm btn-info" Text="Select" CommandName="Extra" CommandArgument='<%# Eval("MYPRODID")+"-"+Eval("UOM") %>'></asp:LinkButton>
                                </td>
                            </tr>
                                </ItemTemplate>
                            </asp:ListView>
                           
                        </tbody>
                    </table>
                </div>

            </div>
        </div>
        <%--<div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-default">Close</button>
                    <button type="button" class="btn blue">Save changes</button>
                </div>--%>
    </div>
</asp:Content>
