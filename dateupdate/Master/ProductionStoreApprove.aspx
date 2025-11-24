<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="ProductionStoreApprove.aspx.cs" Inherits="Web.Master.ProductionStoreApprove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-12">
        <div class="row">

            <asp:Panel ID="PanelmultiUom" runat="server" Visible="false">
                <div class="col-md-12">
                    <div class="tabbable-custom tabbable-noborder">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-horizontal form-row-seperated">
                                    <div class="portlet box green-turquoise">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>Multi UOM
                                            </div>
                                            <div class="tools">
                                                <a id="A5" runat="server" href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>

                                        </div>
                                        <div class="portlet-body" id="Div5" runat="server" style="padding-left: 25px;">
                                            <table class="table table-striped table-bordered table-hover">

                                                <thead>
                                                    <tr>
                                                        <th>no #</th>
                                                        <th>UOM #</th>
                                                        <th>Qty #</th>
                                                    </tr>
                                                </thead>

                                                <tbody>
                                                    <asp:ListView ID="ListUOM" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblMultiUOMNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lbluom" runat="server" Text='<%# Eval("UOM") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lbluomQty" runat="server" Text='<%# Eval("OnHand") %>'></asp:Label></td>
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

            </asp:Panel>


            <asp:Panel ID="Panelserialize" runat="server" Visible="false">
                <div class="col-md-12">
                    <div class="tabbable-custom tabbable-noborder">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-horizontal form-row-seperated">
                                    <div class="portlet box green-turquoise">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>Serialize
                                            </div>
                                            <div class="tools">
                                                <a id="A1" runat="server" href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>

                                        </div>
                                        <div class="portlet-body" id="Div1" runat="server" style="padding-left: 25px;">
                                            <table class="table table-striped table-bordered table-hover">

                                                <thead>
                                                    <tr>
                                                        <th>no #</th>
                                                        <th>Serial #</th>
                                                    </tr>
                                                </thead>

                                                <tbody>
                                                    <asp:ListView ID="ListSerialize" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblSerialSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblSerialNO" runat="server" Text='<%# Eval("Serial_Number") %>'></asp:Label></td>
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
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-horizontal form-row-seperated">
                                    <div class="portlet box green-turquoise">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>Serialize Product Transaction log
                                            </div>
                                            <div class="tools">
                                                <a id="A6" runat="server" href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>

                                        </div>
                                        <div class="portlet-body" id="Div6" runat="server" style="padding-left: 25px;">
                                            <table class="table table-striped table-bordered table-hover">

                                                <thead>
                                                    <tr>
                                                        <th>Serial</th>
                                                        <th>Date</th>
                                                        <th>Transaction type</th>
                                                        <th>Trans No</th>
                                                        <th>Customer</th>
                                                        <th>Supplier</th>
                                                    </tr>
                                                </thead>

                                                <tbody>
                                                    <asp:ListView ID="ListView1" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <%--<td>
                                                            <asp:Label ID="lblSerialSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label></td>--%>
                                                                <td>
                                                                    <asp:Label ID="lblSerialNO" runat="server" Text='<%# Eval("Serial_Number") %>'></asp:Label></td>
                                                                <asp:Label ID="lblserialDate" runat="server" Text='<%# Eval("Serial_Number") %>'></asp:Label></td>
                                                        <asp:Label ID="LabelTransectionType" runat="server" Text='<%# Eval("Serial_Number") %>'></asp:Label></td>
                                                        <asp:Label ID="LabelTransNO" runat="server" Text='<%# Eval("Serial_Number") %>'></asp:Label></td>
                                                        <asp:Label ID="LabelCustomer" runat="server" Text='<%# Eval("Serial_Number") %>'></asp:Label></td>
                                                        <asp:Label ID="LabelSupplier" runat="server" Text='<%# Eval("Serial_Number") %>'></asp:Label></td>
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
                
            </asp:Panel>


            <asp:Panel ID="PanelSizeColor" runat="server" Visible="false">
                <div class="col-md-12">
                    <div class="tabbable-custom tabbable-noborder">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-horizontal form-row-seperated">
                                    <div class="portlet box green-turquoise">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>Size Color
                                            </div>
                                            <div class="tools">
                                                <a id="A2" runat="server" href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>

                                        </div>
                                        <div class="portlet-body" id="Div2" runat="server" style="padding-left: 25px;">
                                            <table class="table table-striped table-bordered table-hover">

                                                <thead>
                                                    <tr>
                                                        <th>no #</th>
                                                        <th>Color</th>
                                                        <th>size</th>
                                                        <th>QTY</th>
                                                    </tr>
                                                </thead>

                                                <tbody>
                                                    <asp:ListView ID="ListSizecolor" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblSizecolorSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Lblcolor" runat="server" Text='<%# Eval("COLORID").ToString()!=null && Eval("COLORID").ToString()!="999999998"? getcolorname(Convert.ToInt32(Eval("COLORID"))) : "" %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblsize" runat="server" Text='<%# Eval("SIZECODE").ToString()!=null && Eval("SIZECODE").ToString()!="999999998" ? getsizename(Convert.ToInt32(Eval("SIZECODE"))) : ""  %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Lablqty" runat="server" Text='<%# Eval("OnHand") %>'></asp:Label></td>
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

            </asp:Panel>


            <asp:Panel ID="PanelPerishable" runat="server" Visible="false">
                <div class="col-md-12">
                    <div class="tabbable-custom tabbable-noborder">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-horizontal form-row-seperated">
                                    <div class="portlet box green-turquoise">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>Perishable
                                            </div>
                                            <div class="tools">
                                                <a id="A3" runat="server" href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>

                                        </div>
                                        <div class="portlet-body" id="Div3" runat="server" style="padding-left: 25px;">
                                            <table class="table table-striped table-bordered table-hover">

                                                <thead>
                                                    <tr>
                                                        <th>no #</th>
                                                        <th>Batch #</th>
                                                        <th>Date</th>
                                                        <th>Qty</th>
                                                    </tr>
                                                </thead>

                                                <tbody>
                                                    <asp:ListView ID="ListPerishable" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblPerishableNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="LblBatch" runat="server" Text='<%# Eval("BatchNo") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("ProdDate") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="LblPerishableqty" runat="server" Text='<%# Eval("OnHand") %>'></asp:Label></td>
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

            </asp:Panel>


            <asp:Panel ID="PanelBinWise" runat="server" Visible="false">
                <div class="col-md-12">
                    <div class="tabbable-custom tabbable-noborder">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-horizontal form-row-seperated">
                                    <div class="portlet box green-turquoise">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>Bin Wise
                                            </div>
                                            <div class="tools">
                                                <a id="A4" runat="server" href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>

                                        </div>
                                        <div class="portlet-body" id="Div4" runat="server" style="padding-left: 25px;">
                                            <table class="table table-striped table-bordered table-hover">

                                                <thead>
                                                    <tr>
                                                        <th>no #</th>
                                                        <th>Bin ID #</th>
                                                        <th>Qty</th>
                                                    </tr>
                                                </thead>

                                                <tbody>
                                                    <asp:ListView ID="ListBinWise" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblBinSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="LblBatch" runat="server" Text='<%# Eval("Bin_ID") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="LblBinqty" runat="server" Text='<%# Eval("OnHand") %>'></asp:Label></td>
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

            </asp:Panel>



        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box green-turquoise">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Items in Store
                                    </div>
                                    <div class="tools">
                                        <a id="shlinkProductDetails" runat="server" href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>

                                </div>
                                <div class="portlet-body" id="shProductDetails" runat="server" style="padding-left: 25px;">
                                    <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false" class="alert alert-danger ">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                        <asp:Label ID="lblMsg" runat="server" Text="demm"></asp:Label>
                                    </asp:Panel>
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">

                                        <ContentTemplate>

                                            <table class="table table-striped table-bordered table-hover" id="sample_1">

                                                <div class="form-group">
                                                    <div class="col-md-6">
                                                        <div class="col-md-3">
                                                            <asp:Label ID="lblslocation" runat="server" Text="Select Location"></asp:Label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:DropDownList ID="drplocation" CssClass="form-control select2me" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drplocation_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-md-3">
                                                            <asp:Label ID="Label9" runat="server" Text="Select Date"></asp:Label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:DropDownList ID="Formatedate" runat="server" CssClass="form-control select2me" DataTextFormatString="{0:dd/MMM/yyyy}" AutoPostBack="true" OnSelectedIndexChanged="Formatedate_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <thead class="repHeader">
                                                    <tr>
                                                        <%--<th rowspan="2">
                                                        <asp:Label ID="lblACTION" runat="server" Text="ACTION"></asp:Label></th>--%>
                                                        <th rowspan="2">
                                                            <asp:Label ID="lblSN" runat="server" Text="#"></asp:Label></th>

                                                        <th rowspan="2">
                                                            <asp:Label ID="lblProdid" runat="server" Text="Product ID"></asp:Label></th>
                                                        <th rowspan="2">
                                                            <asp:Label ID="lblSName" runat="server" Text="Recipe Input"></asp:Label></th>

                                                        <th rowspan="2">
                                                            <asp:Label ID="Label1" runat="server" Text="Unit Cost"></asp:Label></th>
                                                        <th rowspan="2">
                                                            <asp:Label ID="Label16" runat="server" Text="UOM"></asp:Label></th>
                                                        <th colspan="3">Quantity</th>
                                                        <th rowspan="2">Request<br />
                                                            QTY</th>
                                                        <th>
                                                            <asp:Label ID="Label18" runat="server" Text="Approve Material"></asp:Label>
                                                        </th>
                                                        <th rowspan="2">
                                                            <asp:Label ID="Label3" runat="server" Text="Label">Request Form</asp:Label>
                                                        </th>
                                                    </tr>
                                                    <tr>

                                                        <th style="border-top-width: 1px;">
                                                            <asp:Label ID="lblProOname" runat="server" Text="On Hand"></asp:Label></th>
                                                        <th style="border-top-width: 1px;">
                                                            <asp:Label ID="lblProO2name" runat="server" Text=" Out"></asp:Label></th>
                                                        <th style="border-top-width: 1px;">
                                                            <asp:Label ID="lblEdit" runat="server" Text=" Received"></asp:Label></th>

                                                        <th>
                                                            <asp:LinkButton ID="LinkApproveAll" runat="server" CssClass="btn btn-sm yellow-lemon" Text="Approve All" Style="padding: 2px 2px 2px 2px; font-weight: bold;" OnClick="LinkApproveAll_Click"></asp:LinkButton></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="ListICITBR" runat="server" OnItemCommand="ListICITBR_ItemCommand" OnItemDataBound="ListICITBR_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr class="gradeA">
                                                                <%--<td>
                                                                <asp:LinkButton ID="lnkbtnItem" runat="server" CommandName="showMulti" CommandArgument='<%# Eval("MyProdID") %>'>More
                                                                    
                                                                </asp:LinkButton>
                                                                /
                                                                <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="viewPrint" CommandArgument='<%# Eval("MyProdID") %>'>Print
                                                                    
                                                                </asp:LinkButton>

                                                            </td>--%>
                                                                <td>
                                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%# (((RepeaterItem)Container).ItemIndex+1).ToString() %>' meta:resourcekey="lblSRNOResource1"></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label14" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                    <asp:Label ID="TRANID" Visible="false" runat="server" Text='<%# Eval("MYTRANSID") %>'></asp:Label>
                                                                    <asp:Label ID="MY" Visible="false" runat="server" Text='<%# Eval("MYID") %>'></asp:Label>

                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblGRCOHNAME2" runat="server" Text='<%#getprodctname(Convert.ToInt32( Eval("MyProdID"))) %>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# getunitcost(Convert.ToInt32(Eval("MyProdID")),Convert.ToInt32(Eval("UOM"))) %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label17" runat="server" Text='<%# getUomname(Convert.ToInt32( Eval("UOM"))) %>'></asp:Label>
                                                                    <asp:Label ID="Label20" runat="server" Visible="false" Text='<%# Eval("UOM") %>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# getOnHand(Convert.ToInt32(Eval("MyProdID")),Convert.ToInt32(Eval("UOM"))) %>'></asp:Label>
                                                                    <%--Eval("OnHand")--%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# getQTYOUT(Convert.ToInt32(Eval("MyProdID")),Convert.ToInt32(Eval("UOM"))) %>'></asp:Label>
                                                                    <%--Eval("QtyOut")--%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# getQTYReceive(Convert.ToInt32(Eval("MyProdID")),Convert.ToInt32(Eval("UOM"))) %>'></asp:Label>
                                                                    <%--Eval("QtyReceived")--%>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("OVERHEADAMOUNT") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                                    <asp:LinkButton ID="LinkApprove" runat="server" CommandName="LinkApprove" CommandArgument='<%# Eval("MYTRANSID")+"-"+ Eval("MYID")+"-"+ Eval("MyProdID")+"-"+Eval("UOM") %>' CssClass="btn btn-sm yellow-lemon" Visible="false" Text="Approve" Style="padding: 2px 2px 2px 2px; font-weight: bold;"></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td><asp:DropDownList ID="ListPoDrft" runat="server" CssClass="form-control select2me input-small"></asp:DropDownList></td>
                                                                            <td style="padding-left: 4px;padding-right: 4px;"><asp:TextBox ID="txtfinalFormTOT" runat="server" CssClass="form-control input-xsmall"></asp:TextBox></td>
                                                                            <td><asp:Button ID="Button1" runat="server" CssClass="btn btn-sm btn-info" Text="Add" OnClick="Button1_Click" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>


                                        </ContentTemplate>
                                        <Triggers>
                                            <%--<asp:AsyncPostBackTrigger ControlID="LinkApprove" EventName="Click" />--%>
                                            <%-- <asp:AsyncPostBackTrigger ControlID="lnkbtnItem" />
                                        <asp:AsyncPostBackTrigger ControlID="LinkApproveAll" />--%>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <asp:Panel ID="pnlStoreiss" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12">
                <!-- BEGIN PORTLET-->
                <div class="portlet box yellow tasks-widget">
                    <div class="portlet-title">
                        <div class="caption caption-md">
                            <i class="icon-bar-chart theme-font-color hide"></i>
                            <span class="caption-subject bold uppercase">Store Issue</span>
                            <span class="caption-helper" style="color: #345466;">(
                                <asp:Label ID="STRIQE" runat="server" Text="0"></asp:Label>
                                Issue )</span>
                        </div>

                    </div>
                    <div class="portlet-body">
                        <div class="task-content">
                            <div class="scroller" style="height: 282px;" data-always-visible="1" data-rail-visible1="0" data-handle-color="#D7DCE2">
                                <!-- START TASK LIST -->
                                <ul class="task-list">
                                    <asp:ListView ID="StoreIssue" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <div class="task-title">

                                                    <span class="task-title-sp">This Item Is not Available In Store </u></b> <%# "<b><u>"+ getprodctname(Convert.ToInt32(Eval("MyProdID"))) +"</u></b>" %> and UOM <%# "<b><u>"+ getUomname(Convert.ToInt32(Eval("UOM"))) +"</u></b>" %> </span>
                                                    <div style="float: right;">

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
</asp:Content>
