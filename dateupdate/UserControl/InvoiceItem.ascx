<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoiceItem.ascx.cs" Inherits="Web.UserControl.InvoiceItem" %>
<%@ Register Src="~/UserControl/MultiTransaction.ascx" TagPrefix="uc1" TagName="MultiTransaction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="tab-content no-space">

    <asp:Panel ID="step4" runat="server" class="tab-pane active">
        <asp:UpdatePanel ID="itemsupde" runat="server">
            <ContentTemplate>
                <div class="portlet box red-pink">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-gift"></i>
                            <asp:Label ID="lblitems" Style="font-size: 14px;" runat="server" Text="Items"></asp:Label>
                            (
                                <asp:Label runat="server" ID="lblProductCount"></asp:Label>

                                                                                    
                            <asp:Label ID="lblMode" runat="server" Text="User Cotrol"></asp:Label>
                            )
                            <asp:Label ID="lblSaveCount" runat="server" Text="0 Item Save"></asp:Label>
                        </div>
                        <div class="actions btn-set">
                            <asp:Label ID="lblitemsearch" runat="server" Visible="false"></asp:Label>
                            <asp:LinkButton ID="btnAddItemsIn" runat="server" CssClass="btn default btn-xs purple" Visible="false" OnClick="btnAddnew_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;"><i class="fa fa-plus"> Add New</i></asp:LinkButton>
                            <asp:LinkButton ID="btnAddDT" runat="server" CssClass="btn btn-icon-only green" ValidationGroup="submititems" Visible="false" OnClick="btnAddDT_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;">Save</asp:LinkButton>
                             
                            <asp:LinkButton ID="btndiscartitems" runat="server" CssClass="btn default" OnClick="btndiscartitems_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;">Exit</asp:LinkButton>

                        </div>
                    </div>
                    <asp:Panel ID="pnlSuccessMsg123" runat="server" Visible="false">
                        <div class="alert alert-danger alert-dismissable">
                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                            <asp:Label ID="lblMsg123" runat="server"></asp:Label>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="panelRed" class="portlet-body" runat="server">
                        <div class="tabbable">
                            <div class="tab-content no-space">
                                <div class="tab-pane active" id="tab_general2">
                                    <%--<uc1:multitransaction runat="server" id="MultiTransaction" />--%>
                                    <div class="form-group" style="margin-bottom: 0px;">
                                        <asp:Label class="col-md-2 control-label" ID="lblmultiserialize" runat="server" Visible="false" Style="top: -10px;">
                                            <asp:LinkButton ID="LinkButton2" class="btn default green-stripe" runat="server">Serialized</asp:LinkButton>
                                            <asp:Panel ID="pneSerial" runat="server" Style="height: 75%; overflow: auto; display: none">
                                                <%-- <asp:HyperLink ID="lnkClose" runat="server">--%>
                                                <%--</asp:HyperLink>--%>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="portlet box grey-cascade">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="fa fa-globe"></i>
                                                                    <asp:Label runat="server" ID="lblSerialization1s" ForeColor="White" Text="Serialization" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSerialization1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbltotalqty" runat="server"></asp:Label>
                                                                    <asp:Label runat="server" ID="lblSerialization2h" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSerialization2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="portlet-body">
                                                                <table class="table table-striped table-bordered table-hover" id="sample_2">

                                                                    <thead class="repHeader">
                                                                        <tr>
                                                                            <th>
                                                                                <asp:Label ID="Label1" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>

                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblSerializationNo1s" Style="width: 145px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSerializationNo1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <asp:Label runat="server" ID="lblSerializationNo2h" Style="width: 145px;" Text="Serialization No" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSerializationNo2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <th>
                                                                                <asp:Label ID="Label14" runat="server" Text="Select"></asp:Label></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:ListView ID="listSerial" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr class="gradeA">
                                                                                    <td>
                                                                                        <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                        <asp:Label ID="lbltidser" Visible="false" runat="server" Text='<%# Eval("TenantID") %>'></asp:Label>
                                                                                        <asp:Label ID="lblpidsril" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                        <asp:Label ID="lblpdcodeser" Visible="false" runat="server" Text='<%# Eval("period_code") %>'></asp:Label>
                                                                                        <asp:Label ID="lblmysysser" Visible="false" runat="server" Text='<%# Eval("MySysName") %>'></asp:Label>
                                                                                        <asp:Label ID="lbluomser" Visible="false" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                                                                        <asp:Label ID="lbllocaser" Visible="false" runat="server" Text='<%# Eval("LocationID") %>'></asp:Label>
                                                                                    </td>

                                                                                    <td>
                                                                                        <asp:TextBox ID="txtlistSerial" class="form-control" runat="server" Enabled="false" Text='<%# Eval("Serial_Number") %>'></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:CheckBox ID="cbslectsernumber" runat="server" OnCheckedChanged="cbslectsernumber_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </tbody>
                                                                </table>

                                                                <div class="form-group" style="width: 350px; padding-left: 20px;">
                                                                    <asp:TextBox ID="tags_2" runat="server" CssClass="form-control tags"></asp:TextBox>
                                                                </div>

                                                                <div class="form-actions noborder">
                                                                    <asp:LinkButton ID="LinkButton3" class="btn green-haze modalBackgroundbtn-circle" OnClick="LinkButton3_Click" runat="server"> Save</asp:LinkButton>
                                                                    <asp:Button ID="Button10" runat="server" class="btn btn-default" Text="Cancel" />
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button10" Enabled="True" PopupControlID="pneSerial" TargetControlID="LinkButton2"></cc1:ModalPopupExtender>
                                        </asp:Label>
                                        <asp:Label class="col-md-2 control-label" ID="lblmultiuom" runat="server" Visible="false" Style="top: -10px;">
                                            <asp:LinkButton ID="LinkButton9" class="btn default red-stripe" runat="server">Multi UOM</asp:LinkButton>
                                            <asp:Panel ID="pnlMultiUom" runat="server" Style="height: 75%; overflow: auto; display: none">

                                                <div class="row" style="position: fixed; top: 30px; left: 20%">
                                                    <div class="col-md-12">
                                                        <div class="portlet box purple">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="fa fa-globe"></i>
                                                                    <asp:Label runat="server" ID="lblMultiUOM1s" class="col-md-4 control-label" Text="Multi UOM" Style="width: 137px;" ForeColor="White"></asp:Label><asp:TextBox runat="server" ID="txtMultiUOM1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl1" runat="server" Style="padding-left: 100px;"></asp:Label>
                                                                    <asp:Label runat="server" ID="lblMultiUOM2h" class="col-md-4 control-label" Style="width: 137px;" ForeColor="White"></asp:Label><asp:TextBox runat="server" ID="txtMultiUOM2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                            <div class="portlet-body">
                                                                <table class="table table-striped table-bordered table-hover">

                                                                    <thead class="repHeader">
                                                                        <tr>
                                                                            <th>
                                                                                <asp:Label ID="Label22" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblUOM1s" Text="UOM" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOM1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <asp:Label runat="server" ID="lblUOM2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOM2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </th>
                                                                            <th style="background-color: #E08283; color: #fff">
                                                                                <asp:Label runat="server" ID="lblUomNewQty1s" Text="UOM New Qty" Style="width: 141px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUomNewQty1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <asp:Label runat="server" ID="lblUomNewQty2h" Style="width: 141px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUomNewQty2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </th>

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
                                                                                        <asp:Label ID="LblColoername" runat="server" Text='<%#getrecodtypename(Convert.ToInt32(  Eval("UOM"))) +" , "+Eval("OnHand") %>'></asp:Label>
                                                                                        <%-- <asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("CompniyAndContactID") %>'></asp:Label>--%>
                                                                                        <asp:Label ID="lbltidser" Visible="false" runat="server" Text='<%# Eval("TenantID") %>'></asp:Label>
                                                                                        <asp:Label ID="lblpidsril" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                        <asp:Label ID="lblpdcodeser" Visible="false" runat="server" Text='<%# Eval("period_code") %>'></asp:Label>
                                                                                        <asp:Label ID="lblmysysser" Visible="false" runat="server" Text='<%# Eval("MySysName") %>'></asp:Label>
                                                                                        <asp:Label ID="lbluomser" Visible="false" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                                                                        <asp:Label ID="lbllocaser" Visible="false" runat="server" Text='<%# Eval("LocationID") %>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 70px;">
                                                                                        <asp:TextBox ID="txtuomQty" Style="width: 70px;" class="form-control" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </tbody>
                                                                </table>
                                                                <div class="form-actions noborder">
                                                                    <asp:LinkButton ID="LinkButton1" class="btn green-haze modalBackgroundbtn-circle" OnClick="LinkButton1_Click" runat="server"> Save</asp:LinkButton>
                                                                    <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                    <asp:Button ID="Button11" runat="server" class="btn btn-default" Text="Cancel" />

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button11" Enabled="True" PopupControlID="pnlMultiUom" TargetControlID="LinkButton9"></cc1:ModalPopupExtender>
                                        </asp:Label>
                                        <asp:Label class="col-md-2 control-label" ID="lblmultiperishable" runat="server" Visible="false" Style="top: -10px;">

                                            <asp:LinkButton ID="LinkButton5" class="btn default yellow-stripe" runat="server">Perishable</asp:LinkButton>
                                            <asp:Panel ID="panelPershibal" runat="server" Style="height: 75%; overflow: auto; display: none">
                                                <div class="row" style="position: fixed; top: 30px; left: 20%">
                                                    <div class="col-md-12">
                                                        <div class="portlet box purple">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="fa fa-globe"></i>
                                                                    <asp:Label runat="server" ID="lblPerishable1s" Text="Perishable" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPerishable1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl2" runat="server" Style="padding-left: 50px;"></asp:Label>
                                                                    <asp:Label runat="server" ID="lblPerishable2h" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPerishable2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                            <div class="portlet-body">
                                                                <asp:UpdatePanel ID="updatePerishebal" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>

                                                                        <table class="table table-striped table-bordered table-hover">

                                                                            <thead class="repHeader">
                                                                                <tr>
                                                                                    <th>
                                                                                        <asp:Label ID="Label6" runat="server" Text="#"></asp:Label></th>
                                                                                    <th style="width: 80px;">
                                                                                        <asp:Label ID="Label7" runat="server" Text="Batch No"></asp:Label></th>
                                                                                    <th style="width: 70px;">
                                                                                        <asp:Label ID="Label8" runat="server" Text="Qty"></asp:Label></th>
                                                                                    <th style="width: 113px;">
                                                                                        <asp:Label ID="Label9" runat="server" Text="Prodct Date"></asp:Label></th>
                                                                                    <th style="width: 113px;">
                                                                                        <asp:Label ID="Label10" runat="server" Text="Expiry Date"></asp:Label></th>
                                                                                    <th style="width: 109px;">
                                                                                        <asp:Label ID="Label11" runat="server" Text="Days to Destory"></asp:Label></th>
                                                                                    <th style="width: 109px;">
                                                                                        <asp:Label ID="Label15" runat="server" Text="Sell Qty"></asp:Label></th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <asp:ListView ID="listperishibal" runat="server">
                                                                                    <ItemTemplate>
                                                                                        <tr class="gradeA">
                                                                                            <td>
                                                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 80px;">
                                                                                                <asp:TextBox ID="txtBatchno" Style="width: 70px;" Enabled="false" Text='<%# Eval("BatchNo") %>' class="form-control" runat="server"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 70px;">
                                                                                                <asp:TextBox ID="txtqty" Enabled="false" Style="width: 70px;" class="form-control" Text='<%# Eval("OnHand") %>' runat="server"></asp:TextBox>
                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtqty" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                            </td>
                                                                                            <td style="width: 113px;">
                                                                                                <asp:TextBox ID="txtproductdate" Enabled="false" Style="width: 113px;" class="form-control" Text='<%# Eval("ProdDate", "{0:dd-MMM-yyyy}") %>' runat="server"></asp:TextBox>
                                                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtproductdate" Format="dd-MMM-yy" Enabled="True">
                                                                                                </cc1:CalendarExtender>
                                                                                            </td>
                                                                                            <td style="width: 113px;">
                                                                                                <asp:TextBox ID="txtexpirydate" Enabled="false" Style="width: 113px;" class="form-control" Text='<%# Eval("ExpiryDate", "{0:dd-MMM-yyyy}") %>' runat="server"></asp:TextBox>
                                                                                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtexpirydate" Format="dd-MMM-yy" Enabled="True">
                                                                                                </cc1:CalendarExtender>
                                                                                            </td>
                                                                                            <td style="width: 109px;">
                                                                                                <asp:TextBox ID="txtlead2destroydate" Style="width: 109px;" Enabled="false" class="form-control" Text='<%# Eval("LeadDays2Destroy", "{0:dd-MMM-yyyy}") %>' runat="server"></asp:TextBox>
                                                                                                <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtlead2destroydate" Format="dd-MMM-yy" Enabled="True">
                                                                                                </cc1:CalendarExtender>
                                                                                            </td>
                                                                                            <td style="width: 109px;">
                                                                                                <asp:TextBox ID="txtnewqty" Style="width: 109px;" class="form-control" runat="server"></asp:TextBox>

                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </tbody>
                                                                        </table>
                                                                    </ContentTemplate>

                                                                </asp:UpdatePanel>
                                                                <div class="form-actions noborder">
                                                                    <asp:LinkButton ID="LinkButton10" class="btn green-haze modalBackgroundbtn-circle" runat="server" OnClick="LinkButton10_Click"> Save</asp:LinkButton>
                                                                    <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                    <asp:Button ID="Button2" runat="server" OnClientClick="close(this)" class="btn btn-default" data-dismiss="modal" aria-hidden="true" Text="Cancel" />

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                </div>

                                            </asp:Panel>
                                            <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button2" Enabled="True" PopupControlID="panelPershibal" TargetControlID="LinkButton5"></cc1:ModalPopupExtender>
                                        </asp:Label>
                                        <asp:Label class="col-md-2 control-label" ID="lblmulticolor" runat="server" Visible="false" Style="top: -10px;">
                                            <asp:LinkButton ID="LinkButton8" class="btn default blue-stripe" runat="server">Multi Color</asp:LinkButton>
                                            <asp:Panel ID="pnlMultiColoer" runat="server" Style="height: 75%; overflow: auto; display: none">

                                                <div class="row" style="position: fixed; top: 30px; left: 20%">
                                                    <div class="col-md-12">
                                                        <div class="portlet box yellow">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="fa fa-globe"></i>
                                                                    <asp:Label runat="server" ID="lblMultiColoer1s" Text="Multi Colors" Style="width: 160px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiColoer1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl3" runat="server"></asp:Label>
                                                                    <asp:Label runat="server" ID="lblMultiColoer2h" Style="width: 160px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiColoer2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                                <div class="actions btn-set">
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <table class="table table-striped table-bordered table-hover">

                                                                    <thead class="repHeader">
                                                                        <tr>
                                                                            <th>
                                                                                <asp:Label ID="Label30" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblMultiColoer11s" Text="Multi Colors" Style="width: 112px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiColoer11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <asp:Label runat="server" ID="lblMultiColoer12h" Style="width: 112px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiColoer12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </th>

                                                                            <th style="background-color: #E08283; color: #fff">
                                                                                <asp:Label runat="server" ID="lblColoerNewQty1s" Text="Colors New Qty" Style="width: 146px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtColoerNewQty1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <asp:Label runat="server" ID="lblColoerNewQty2h" Style="width: 146px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtColoerNewQty2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </th>

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
                                                                                        <asp:Label ID="LblColoername" runat="server" Text='<%# getrecodtypename(Convert.ToInt32( Eval("COLORID")))+" , "+ Eval("OnHand")%>'></asp:Label>
                                                                                        <asp:Label ID="lblpidsril" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                        <asp:Label ID="lblcolerid" Visible="false" runat="server" Text='<%# Eval("COLORID") %>'></asp:Label>
                                                                                        <%-- <asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("CompniyAndContactID") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("RecTypeID") %>'></asp:Label>--%>
                                                                                    </td>
                                                                                    <td style="width: 70px;">
                                                                                        <asp:TextBox ID="txtcoloerqty" Style="width: 70px;" class="form-control" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </tbody>
                                                                </table>
                                                                <div class="form-actions noborder">
                                                                    <asp:LinkButton ID="linkMulticoler" class="btn green-haze modalBackgroundbtn-circle" runat="server" OnClick="linkMulticoler_Click"> Save</asp:LinkButton>
                                                                    <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                    <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Cancel" />

                                                                </div>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>


                                            </asp:Panel>
                                            <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button5" Enabled="True" PopupControlID="pnlMultiColoer" TargetControlID="LinkButton8"></cc1:ModalPopupExtender>
                                        </asp:Label>
                                        <asp:Label class="col-md-2 control-label" ID="lblmultisize" runat="server" Visible="false" Style="top: -10px;">
                                            <asp:LinkButton ID="LinkButton6" class="btn default green-stripe" runat="server">Multi Size</asp:LinkButton>
                                            <asp:Panel ID="pnlMultiSize" runat="server" Style="height: 75%; overflow: auto; display: none">

                                                <div class="row" style="position: fixed; top: 30px; left: 20%">
                                                    <div class="col-md-12">
                                                        <div class="portlet box purple">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="fa fa-globe"></i>
                                                                    <asp:Label runat="server" ID="lblMultiSize1s" Text="Multi Size" Style="width: 133px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiSize1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl4" runat="server"></asp:Label>
                                                                    <asp:Label runat="server" ID="lblMultiSize2h" Style="width: 133px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiSize2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <table class="table table-striped table-bordered table-hover">

                                                                    <thead class="repHeader">
                                                                        <tr>
                                                                            <th>
                                                                                <asp:Label ID="Label24" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblSize1s" Text="Size" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSize1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <asp:Label runat="server" ID="lblSize2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSize2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </th>
                                                                            <th style="background-color: #E08283; color: #fff">
                                                                                <asp:Label runat="server" ID="lblSizeNewQty1s" Text="Size New Qty" Style="width: 142px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSizeNewQty1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                <asp:Label runat="server" ID="lblSizeNewQty2h" Style="width: 142px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSizeNewQty2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </th>

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
                                                                                        <asp:Label ID="LblColoername" runat="server" Text='<%# getrecodtypename(Convert.ToInt32( Eval("SIZECODE")))+" , "+ Eval("OnHand")%>'></asp:Label>
                                                                                        <asp:Label ID="lblpidsril" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                        <asp:Label ID="lblcolerid" Visible="false" runat="server" Text='<%# Eval("SIZECODE") %>'></asp:Label>
                                                                                        <%--<asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("CompniyAndContactID") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("RecTypeID") %>'></asp:Label>--%>
                                                                                    </td>
                                                                                    <td style="width: 70px;">
                                                                                        <asp:TextBox ID="txtmultisze" Style="width: 70px;" class="form-control" runat="server"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </tbody>
                                                                </table>
                                                                <div class="form-actions noborder">
                                                                    <asp:LinkButton ID="LinkButton7" class="btn green-haze modalBackgroundbtn-circle" OnClick="LinkButton7_Click" runat="server"> Save</asp:LinkButton>
                                                                    <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                    <asp:Button ID="Button9" runat="server" class="btn btn-default" Text="Cancel" />

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                            <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button9" Enabled="True" PopupControlID="pnlMultiSize" TargetControlID="LinkButton6"></cc1:ModalPopupExtender>
                                        </asp:Label>
                                        <label class="col-md-2 control-label" id="lblmultibinstore" runat="server" visible="false" style="top: -10px;">

                                            <asp:LinkButton ID="LinkButton4" class="btn default purple-stripe" runat="server">MultiBinStore</asp:LinkButton>
                                            <asp:Panel ID="panelMultibin" runat="server" Style="height: 75%; overflow: auto; display: none">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row" style="position: fixed; top: 30px; left: 20%">
                                                            <div class="col-md-12">
                                                                <div class="portlet box yellow">
                                                                    <div class="portlet-title">
                                                                        <div class="caption">
                                                                            <i class="fa fa-globe"></i>
                                                                            <asp:Label runat="server" ID="lblMultiBin1s" Text="Multi Bin" Style="width: 129px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiBin1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl5" runat="server"></asp:Label>
                                                                            <asp:Label runat="server" ID="lblMultiBin2h" Style="width: 129px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiBin2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:Label ID="tblProdid" Visible="false" runat="server"></asp:Label><asp:Label ID="lblQTY" Visible="false" runat="server"></asp:Label><asp:Label ID="Label36" Visible="false" runat="server"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="portlet-body">
                                                                        <%--<asp:LinkButton ID="linkBinAddNew" runat="server" OnClick="linkBinAddNew_Click">
                                                                                                                                        <asp:Image ID="Image4" runat="server" ImageUrl="images/plus.png" Style="float: right; padding-right: 25px; padding-bottom: 10px;" />
                                                                                                                                    </asp:LinkButton>--%>
                                                                        <table class="table table-striped table-bordered table-hover">

                                                                            <thead class="repHeader">
                                                                                <tr>
                                                                                    <th>
                                                                                        <asp:Label ID="Label37" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label runat="server" ID="lblBin1s" Text="Bin" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBin1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <asp:Label runat="server" ID="lblBin2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBin2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </th>
                                                                                    <th style="background-color: #E08283; color: #fff">
                                                                                        <asp:Label runat="server" ID="lblQty1s" Text="Qty" Style="color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQty1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <asp:Label runat="server" ID="lblQty2h" Style="color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQty2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                                                                <%--<asp:DropDownList ID="dropBacthCode" runat="server" CssClass="table-group-action-input form-control input-medium">
                                                                                                                                                            </asp:DropDownList>--%>
                                                                                                <asp:Label ID="LblMyid" runat="server" Text='<%#getbinname(Convert.ToInt32( Eval("Bin_ID"))) + " , "+ Eval("OnHand") %>'></asp:Label>
                                                                                                <asp:Label ID="lblbinid" Visible="false" runat="server" Text='<%# Eval("Bin_ID") %>'></asp:Label>
                                                                                                <asp:Label ID="lblpidsril" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                            </td>

                                                                                            <td style="width: 70px;">
                                                                                                <asp:TextBox ID="txtqty" Style="width: 70px;" class="form-control" runat="server"></asp:TextBox>
                                                                                            </td>

                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>


                                                                            </tbody>
                                                                        </table>
                                                                        <div class="form-actions noborder">
                                                                            <asp:LinkButton ID="lbApproveIss" class="btn blue" runat="server" OnClick="lbApproveIss_Click">Save</asp:LinkButton>
                                                                            <asp:Button ID="Button3" runat="server" class="btn btn-default" data-dismiss="modal" aria-hidden="true" Text="Cancel" />
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                                <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="overlay">
                        <div style="z-index: 1000; margin-left: 350px; margin-top: 200px; opacity: 1; -moz-opacity: 1;">
                            <img src="../assets/admin/layout4/img/loading-spinner-blue.gif" />
                            &nbsp;please wait...
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
                                            </asp:Panel>
                                            <cc1:ModalPopupExtender ID="ModalPopupExtender6" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button3" Enabled="True" PopupControlID="panelMultibin" TargetControlID="LinkButton4"></cc1:ModalPopupExtender>
                                        </label>

                                    </div>

                                    <div class="form-body">


                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <asp:Label runat="server" ID="lblProduct1s" class="col-md-2 control-label" Text="Product"></asp:Label><asp:TextBox runat="server" ID="txtProduct1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                <div class="col-md-4">

                                                    <asp:Panel ID="Panel1" DefaultButton="btnserchAdvans" runat="server" class="input-group">

                                                        <asp:TextBox ID="txtserchProduct" runat="server" AutoCompleteType="Disabled" CssClass="form-control"  placeholder="Search" MaxLength="250">
                                                        </asp:TextBox>
                                                        <span class="input-group-btn"></span>
                                                        <asp:LinkButton ID="btnserchAdvans" CssClass="btn btn-icon-only yellow" runat="server" Style="margin-top: -23px; padding-left: 0px; margin-left: 5px; margin-bottom: 7px;" OnClick="btnserchAdvans_Click1" OnClientClick="showProgress()">
                                                                                                             <i class="fa fa-search" ></i>
                                                        </asp:LinkButton>
                                                    </asp:Panel>

                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="ddlProduct" CssClass="form-control select2me" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlProduct" InitialValue="0" ErrorMessage=" Select Product  Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                </div>

                                                <asp:Label runat="server" ID="lblProduct2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProduct2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <asp:TextBox ID="txtDescription" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TextMode="MultiLine" placeholder="Description" Rows="2" Columns="5" MaxLength="250"></asp:TextBox>
                                            </div>

                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">

                                            <ContentTemplate>

                                                <div class="form-group">
                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblUnitofMeasure1s" class="col-md-3 control-label" Text="Unit Of Measure"></asp:Label><asp:TextBox runat="server" ID="txtUnitofMeasure1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddlUOM" CssClass="form-control" runat="server"></asp:DropDownList>
                                                           <%-- <asp:HiddenField ID="hidUOMId" runat="server" Value="" />
                                                            <asp:HiddenField ID="hidUOMText" runat="server" Value="" />--%>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblUnitofMeasure2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitofMeasure2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>





                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblQuantity1s" class="col-md-3 control-label" Text="Quantity"></asp:Label><asp:TextBox runat="server" ID="txtQuantity1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtQuantity" runat="server" AutoCompleteType="Disabled" OnTextChanged="txtQuantity_TextChanged1" AutoPostBack="true" MaxLength="7" placeholder="Quantity" CssClass="form-control quntity"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtQuantity" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                            <%--onblur="checkdate1(this)"--%>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtQuantity" ErrorMessage=" Quantity  Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                            <asp:HiddenField ID="hidMColor" runat="server" Value="" />
                                                        </div>

                                                        <asp:Label runat="server" ID="lblQuantity2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantity2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-4">
                                                            <asp:Label runat="server" ID="lblAvailableQty"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">

                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblUnitPricelocal1s" class="col-md-3 control-label" Text="Unit Price (Local)"></asp:Label><asp:TextBox runat="server" ID="txtUnitPricelocal1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-3">
                                                            <%--<asp:TextBox ID="txtUPriceLocal" runat="server" onblur="checkdate1(this)" AutoCompleteType="Disabled" placeholder="Unit Price" CssClass="form-control lprice"></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtUPriceLocal" AutoPostBack="true" Text="0" runat="server" OnTextChanged="txtUPriceLocal_TextChanged1" AutoCompleteType="Disabled" placeholder="Unit Price" CssClass="form-control lprice"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtUPriceLocal" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                            <asp:HiddenField ID="hidUPriceLocal" runat="server" Value="" />
                                                        </div>
                                                        <asp:Label runat="server" ID="lblUnitPricelocal2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPricelocal2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                        <div class="col-md-4">
                                                            <asp:Label runat="server" ID="lblDiscount1s" class="col-md-3 control-label" Text="Discount"></asp:Label><asp:TextBox runat="server" ID="txtDiscount1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="txtDiscount" runat="server" AutoCompleteType="Disabled" Text="0" AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged1" placeholder="25 or 25%" CssClass="form-control dis"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtDiscount" ValidChars="./%" FilterType="Custom, numbers" runat="server" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDiscount" ErrorMessage=" txtDiscount  Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <asp:Label runat="server" ID="lblDiscount2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDiscount2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="inline-block popovers" data-container="body" data-trigger="hover" data-placement="bottom" data-content="Plase Search the product before Save" data-original-title="Popover in bottom">
                                                            <%--<button class="btn btn-default disabled" type="button">Disabled</button>--%>
                                                            <asp:LinkButton ID="LinkButton11" runat="server" Enabled="false" CssClass="btn btn-icon-only green" ValidationGroup="submititems" OnClick="btnAddDT_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;">Save</asp:LinkButton>
                                                                </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <asp:Label runat="server" ID="lblTotalCurrencyLocal1s" class="col-md-3 control-label" Text="Total Currency (Local)"></asp:Label><asp:TextBox runat="server" ID="txtTotalCurrencyLocal1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtTotalCurrencyLocal" AutoCompleteType="Disabled" runat="server" ReadOnly="true" CssClass="form-control" placeholder="Total Currency (Local)"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtTotalCurrencyLocal" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                            <asp:HiddenField ID="hidTotalCurrencyLocal" runat="server" Value="" />
                                                        </div>
                                                        <asp:Label runat="server" ID="lblTotalCurrencyLocal2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalCurrencyLocal2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>

                                                    <%-- <asp:Panel ID="pnlforentotal" runat="server" >
                                                                                                    <div class="col-md-6">
                                                                                                        <asp:Label runat="server" ID="lblTotalCurrencyForeign1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalCurrencyForeign1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        <div class="col-md-8">
                                                                                                            <asp:TextBox ID="txtTotalCurrencyForeign" AutoCompleteType="Disabled" runat="server" ReadOnly="true" CssClass="form-control" placeholder="Total Currency (Foreign)"></asp:TextBox>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtTotalCurrencyForeign" ValidChars="." FilterType="Custom, numbers" runat="server" />

                                                                                                            <asp:HiddenField ID="hidTotalCurrencyForeign" runat="server" Value="" />
                                                                                                        </div>
                                                                                                        <asp:Label runat="server" ID="lblTotalCurrencyForeign2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalCurrencyForeign2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    </div></asp:Panel>--%>
                                                    <%--<asp:Panel ID="pnlforeign" runat="server" >
                                                                                                    <div class="col-md-6">
                                                                                                        <asp:Label runat="server" ID="lblUnitPriceForeign1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPriceForeign1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        <div class="col-md-8">
                                                                                                            <asp:TextBox ID="txtUPriceForeign" Enabled="false" onblur="checkdate1(this)" onchange="getLocalPrize()" AutoCompleteType="Disabled" runat="server" placeholder="Unit Price" CssClass="form-control uprice"></asp:TextBox>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtUPriceForeign" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                                        </div>
                                                                                                        <asp:Label runat="server" ID="lblUnitPriceForeign2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPriceForeign2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    </div></asp:Panel>   --%>
                                                </div>
                                            </ContentTemplate>

                                        </asp:UpdatePanel>
                                        

                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-8" style="top: 10px;">
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>


                    <asp:Panel ID="ListItems" class="portlet-body" runat="server">

                        <div class="table-scrollable">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th scope="col">
                                            <asp:Label ID="Label13" runat="server" Text="#"></asp:Label></th>
                                        <th scope="col">
                                            <asp:Label runat="server" ID="lblProductCodeProductName1s" class="col-md-4 control-label" Text="Product Code"></asp:Label><asp:TextBox runat="server" ID="txtProductCodeProductName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                            <asp:Label runat="server" ID="lblProductCodeProductName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductCodeProductName2h" Style="width: 189px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                        </th>
                                        <th scope="col">
                                            <asp:Label ID="Label17" runat="server" Text="Description"></asp:Label>
                                        </th>
                                        <th scope="col">
                                            <asp:Label runat="server" ID="lblQuantity11s" class="col-md-4 control-label" Text="Quantity"></asp:Label><asp:TextBox runat="server" ID="txtQuantity11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                            <asp:Label runat="server" ID="lblQuantity12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantity12h" Style="width: 78px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                        </th>
                                        <th scope="col">
                                            <asp:Label runat="server" ID="lblUnitPrice1s" class="col-md-4 control-label" Text="Unit Price"></asp:Label><asp:TextBox runat="server" ID="txtUnitPrice1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                            <asp:Label runat="server" ID="lblUnitPrice2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPrice2h" Style="width: 77px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                        </th>

                                        <th scope="col">
                                            <asp:Label runat="server" ID="lblTotal11s" class="col-md-4 control-label" Text="Total"></asp:Label><asp:TextBox runat="server" ID="txtTotal11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                            <asp:Label runat="server" ID="lblTotal12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotal12h" Style="width: 85px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                        </th>
                                        <th scope="col">Action
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="Repeater2_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label27" runat="server" Text='<%# getprodnameui(Convert .ToInt32 ( Eval("MyProdID"))) %>'></asp:Label>
                                                    <asp:Label ID="lblProductNameItem" runat="server" Visible="false" Text='<%#Eval("MyProdID") %>'></asp:Label>
                                                    <asp:Label ID="lblDiscription" Visible="false" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label>
                                                    <asp:Label ID="lblUOM" Visible="false" runat="server" Text='<%#Eval("UOM") %>'></asp:Label>
                                                    <asp:Label ID="lblUNITPRICE" Visible="false" runat="server" Text='<%#Eval("UNITPRICE") %>'></asp:Label>
                                                    <asp:Label ID="lblDISAMT" Visible="false" runat="server" Text='<%#Eval("DISAMT") %>'></asp:Label>
                                                    <asp:Label ID="lblDISPER" Visible="false" runat="server" Text='<%#Eval("DISPER") %>'></asp:Label>
                                                    <asp:Label ID="lblTAXAMT" Visible="false" runat="server" Text='<%#Eval("TAXAMT") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblDisQnty" runat="server" Text='<%#Eval("QUANTITY") %>' />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblOHAmnt" runat="server" Text='<%#Eval("UNITPRICE") %>' />
                                                </td>


                                                <td style="text-align: center">
                                                    <asp:Label ID="lblTotalCurrency" runat="server" Text='<%#Eval("AMOUNT") %>' />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:LinkButton ID="lnkbtnItem" runat="server" CommandName="editDT" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") %>'>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="../Sales/images/editRec.png" />
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDT" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") %>' OnClientClick="return confirm('Do you want to delete product?')">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="../Sales/images/deleteRec.png" />
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tfoot>
                                        <tr>

                                            <th colspan="3">Total</th>
                                            <%-- <th>Amount</th>--%>
                                            <th style="color: green">
                                                <asp:Label ID="lblqtytotl" runat="server" Text="0"></asp:Label></th>
                                            <th style="color: green">
                                                <asp:Label ID="lblUNPtotl" runat="server" Text="0.00"></asp:Label></th>

                                            <th style="color: green">
                                                <asp:Label ID="lblTotatotl" runat="server" Text="0.00"></asp:Label></th>
                                            <th colspan="2"></th>

                                        </tr>
                                    </tfoot>
                                </tbody>
                            </table>
                        </div>
                        <table class="table table-bordered" id="datatable11" style="margin-left: 390px; width: 494px; margin-top: -35px;">

                            <tbody>
                                <asp:LinkButton ID="btnaddamunt1" runat="server" OnClick="btnaddamunt1_Click" Style="margin-left: 350px; margin-top: 0px; margin-bottom: 9px;">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="../Sales/images/plus.png" />
                                </asp:LinkButton>
                                <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                                    <ItemTemplate>
                                        <tr class="gradeA" style="top:0px;">
                                            <td>
                                                <asp:DropDownList ID="drppaymentMeted1" runat="server" Style="width: 124px;" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblOHType1" Visible="false" Text='<%#Eval("PaymentTermsId") %>' runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <b><font size="1px"> <asp:Label runat="server" slyle="font-size: 9px;" ID="Label191" Text="Card/Reference# , Bank Approval #" ></asp:Label></font></b>
                                                <asp:TextBox ID="txtrefresh1" runat="server" Text='<%#Eval("ReferenceNo")+","+Eval("ApprovalID") %>' AutoCompleteType="Disabled"  placeholder="Card/Reference# , Bank Approval #" CssClass="form-control tags"></asp:TextBox>
                                                <%--OnTextChanged="txtrefresh1_TextChanged"--%>

                                            </td>
                                            <td> 
                                                <asp:TextBox ID="txtammunt1" runat="server" Text='<%#Eval("Amount") %>'   AutoCompleteType="Disabled" placeholder="Amount" Style="width: 104px;" CssClass="form-control"></asp:TextBox>
                                            </td><%--OnTextChanged="txtammunt1_TextChanged"--%>


                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <%--<div class="pull-right" style="margin-left: 40%">--%>
                                <asp:LinkButton ID="LnkBtnSavePayTerm" runat="server" Visible="true" CssClass="btn btn-icon-only green pull-right" ValidationGroup="submititems" OnClick="LnkBtnSavePayTerm_Click" Style="width: 100px; padding-top: 4px; padding-bottom: 4px; height: 30px;">Save Payment</asp:LinkButton>
                                <%--</div>--%>
                            </tbody>
                        </table>

                        
                    </asp:Panel>


                </div>
            </ContentTemplate>
            <%-- <Triggers>
                                                                        <asp:PostBackTrigger ControlID="txtQuantity" />
                                                                    </Triggers>--%>
        </asp:UpdatePanel>
    </asp:Panel>
</div>
