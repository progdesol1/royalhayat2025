<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoiceItemList.ascx.cs" Inherits="Web.UserControl.InvoiceItemList" %>

<asp:Panel ID="ListItems" class="portlet-body" runat="server">
   
    <table class="table table-bordered" id="datatable1" style="margin-left: 390px; width: 494px; margin-top: -35px;">

        <tbody>
            <asp:LinkButton ID="btnaddamunt" runat="server" OnClick="btnaddamunt_Click" Style="margin-left: 350px; margin-top: 0px; margin-bottom: 9px;">
                <asp:Image ID="Image2" runat="server" ImageUrl="images/plus.png" />
            </asp:LinkButton>
            <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                <ItemTemplate>
                    <tr class="gradeA">
                        <td>
                            <asp:DropDownList ID="drppaymentMeted" runat="server" Style="width: 124px;" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:Label ID="lblOHType" Visible="false" Text='<%#Eval("PaymentTermsId") %>' runat="server"></asp:Label>
                        </td>
                        <td>
                            <b><font size="1px"> <asp:Label runat="server" slyle="font-size: 9px;" ID="Label19" Text="Card/Reference# , Bank Approval #" ></asp:Label></font></b>
                            <asp:TextBox ID="txtrefresh" runat="server" Text='<%#Eval("ReferenceNo")+","+Eval("ApprovalID") %>' AutoCompleteType="Disabled" placeholder="Card/Reference# , Bank Approval #" CssClass="form-control tags"></asp:TextBox>


                        </td>
                        <td>
                            <asp:TextBox ID="txtammunt" runat="server" AutoCompleteType="Disabled" placeholder="Amount" Style="width: 104px;" CssClass="form-control"></asp:TextBox>
                        </td>


                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

</asp:Panel>
