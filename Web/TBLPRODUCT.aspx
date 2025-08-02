<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TBLPRODUCT.aspx.cs" Inherits="Web.TBLPRODUCT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>


            <table class="table table-striped table-bordered table-hover" id="sample_1">
                <thead>
                    <tr>

                        <th>
                            <asp:Label runat="server" ID="Label1" Text="UserProdID"></asp:Label></th>
                        <th>
                            <asp:Label runat="server" ID="Label2" Text="BarCode"></asp:Label></th>
                        <th>
                            <asp:Label runat="server" ID="Label3" Text="AlternateCode1"></asp:Label></th>
                        <th>
                            <asp:Label runat="server" ID="Label8" Text="ProdName1"></asp:Label></th>
                        <th>
                            <asp:Label runat="server" ID="Label9" Text="check"></asp:Label></th>



                    </tr>
                </thead>
                <tbody>
                    <asp:ListView ID="Listview1" runat="server">
                        <LayoutTemplate>
                            <tr id="ItemPlaceholder" runat="server">
                            </tr>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>

                                <td>
                                    <asp:Label ID="lblID2" runat="server" Visible="false" Text='<%# Eval("MYPRODID")%>'></asp:Label>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("UserProdID")%>'></asp:Label>

                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("BarCode")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("AlternateCode1")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("ProdName1")%>'></asp:Label></td>
                                <td>
                                    <asp:CheckBox ID="chkProdcheck" runat="server" />
                                </td>
                            </tr>

                        </ItemTemplate>

                    </asp:ListView>

                </tbody>

            </table>
           
            <asp:Button ID="btnAdd" runat="server" Text="Add Record" OnClick="btnAdd_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update Record" OnClick="btnUpdate_Click" />
            <asp:Button ID="btndelte" runat="server" Text="Delete Record" OnClick="btndelte_Click" />
            <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" />
        </div>
         <div class="portlet-body">
                <div class="table-scrollable">
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th style="background-color: skyblue; font-size: large; font-weight: bold;">Checked Product
                                </th>
                            </tr>
                        </thead>
                        <asp:ListView ID="ListView2" runat="server">
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("MYPRODID") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("ProdName1") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </ItemTemplate>
                        </asp:ListView>

                    </table>
                </div>
            </div>
    </form>
</body>
</html>
