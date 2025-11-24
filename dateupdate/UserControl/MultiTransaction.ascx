<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiTransaction.ascx.cs" Inherits="Web.UserControl.MultiTransaction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


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

        <cc1:modalpopupextender id="ModalPopupExtender2" runat="server" dynamicservicepath="" backgroundcssclass="" cancelcontrolid="Button10" enabled="True" popupcontrolid="pneSerial" targetcontrolid="LinkButton2"></cc1:modalpopupextender>
    </asp:Label>
    <asp:Label class="col-md-2 control-label" ID="lblmultiuom" runat="server" Visible="false" Style="top: -10px;">
        <asp:LinkButton ID="LinkButton9" class="btn default red-stripe" runat="server">Multi UOM</asp:LinkButton>
        <asp:Panel ID="pnlMultiUom" runat="server" Style="height: 75%; overflow: auto; display: none">

            <div class="row" style="position:fixed;top:30px;left:20%">
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
        <cc1:modalpopupextender id="ModalPopupExtender1" runat="server" dynamicservicepath="" backgroundcssclass="" cancelcontrolid="Button11" enabled="True" popupcontrolid="pnlMultiUom" targetcontrolid="LinkButton9"></cc1:modalpopupextender>
    </asp:Label>
    <asp:Label class="col-md-2 control-label" ID="lblmultiperishable" runat="server" Visible="false" Style="top: -10px;">

        <asp:LinkButton ID="LinkButton5" class="btn default yellow-stripe" runat="server">Perishable</asp:LinkButton>
        <asp:Panel ID="panelPershibal" runat="server" Style="height: 75%; overflow: auto; display: none">
            <div class="row" style="position:fixed;top:30px;left:20%">
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
                                                            <cc1:filteredtextboxextender id="FilteredTextBoxExtender2" targetcontrolid="txtqty" validchars="." filtertype="Custom, numbers" runat="server" />
                                                        </td>
                                                        <td style="width: 113px;">
                                                            <asp:TextBox ID="txtproductdate" Enabled="false" Style="width: 113px;" class="form-control" Text='<%# Eval("ProdDate", "{0:dd-MMM-yyyy}") %>' runat="server"></asp:TextBox>
                                                            <cc1:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txtproductdate" format="dd-MMM-yy" enabled="True">
                                                                                                                                                            </cc1:calendarextender>
                                                        </td>
                                                        <td style="width: 113px;">
                                                            <asp:TextBox ID="txtexpirydate" Enabled="false" Style="width: 113px;" class="form-control" Text='<%# Eval("ExpiryDate", "{0:dd-MMM-yyyy}") %>' runat="server"></asp:TextBox>
                                                            <cc1:calendarextender id="CalendarExtender5" runat="server" targetcontrolid="txtexpirydate" format="dd-MMM-yy" enabled="True">
                                                                                                                                                            </cc1:calendarextender>
                                                        </td>
                                                        <td style="width: 109px;">
                                                            <asp:TextBox ID="txtlead2destroydate" Style="width: 109px;" Enabled="false" class="form-control" Text='<%# Eval("LeadDays2Destroy", "{0:dd-MMM-yyyy}") %>' runat="server"></asp:TextBox>
                                                            <cc1:calendarextender id="CalendarExtender6" runat="server" targetcontrolid="txtlead2destroydate" format="dd-MMM-yy" enabled="True">
                                                                                                                                                            </cc1:calendarextender>
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
        <cc1:modalpopupextender id="ModalPopupExtender4" runat="server" dynamicservicepath="" backgroundcssclass="" cancelcontrolid="Button2" enabled="True" popupcontrolid="panelPershibal" targetcontrolid="LinkButton5"></cc1:modalpopupextender>
    </asp:Label>
    <asp:Label class="col-md-2 control-label" ID="lblmulticolor" runat="server" Visible="false" Style="top: -10px;">
        <asp:LinkButton ID="LinkButton8" class="btn default blue-stripe" runat="server">Multi Color</asp:LinkButton>
        <asp:Panel ID="pnlMultiColoer" runat="server" Style="height: 75%; overflow: auto; display: none">

            <div class="row" style="position:fixed;top:30px;left:20%">
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
        <cc1:modalpopupextender id="ModalPopupExtender5" runat="server" dynamicservicepath="" backgroundcssclass="" cancelcontrolid="Button5" enabled="True" popupcontrolid="pnlMultiColoer" targetcontrolid="LinkButton8"></cc1:modalpopupextender>
    </asp:Label>
    <asp:Label class="col-md-2 control-label" ID="lblmultisize" runat="server" Visible="false" Style="top: -10px;">
        <asp:LinkButton ID="LinkButton6" class="btn default green-stripe" runat="server">Multi Size</asp:LinkButton>
        <asp:Panel ID="pnlMultiSize" runat="server" Style="height: 75%; overflow: auto; display: none">

            <div class="row" style="position:fixed;top:30px;left:20%">
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
        <cc1:modalpopupextender id="ModalPopupExtender3" runat="server" dynamicservicepath="" backgroundcssclass="" cancelcontrolid="Button9" enabled="True" popupcontrolid="pnlMultiSize" targetcontrolid="LinkButton6"></cc1:modalpopupextender>
    </asp:Label>
    <label class="col-md-2 control-label" id="lblmultibinstore" runat="server" visible="false" style="top: -10px;">

        <asp:LinkButton ID="LinkButton4" class="btn default purple-stripe" runat="server">MultiBinStore</asp:LinkButton>
        <asp:Panel ID="panelMultibin" runat="server" Style="height: 75%; overflow: auto; display: none">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row" style="position:fixed;top:30px;left:20%">
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
        </asp:Panel>
        <cc1:modalpopupextender id="ModalPopupExtender6" runat="server" dynamicservicepath="" backgroundcssclass="" cancelcontrolid="Button3" enabled="True" popupcontrolid="panelMultibin" targetcontrolid="LinkButton4"></cc1:modalpopupextender>
    </label>

</div>
