<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TBLLOCATION.aspx.cs" Inherits="Web.TBLLOCATION" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnAdd" runat="server" Text="Add Record" OnClick="btnAdd_Click"/>
        <asp:Button ID="btnUpdate" runat="server" Text="Update Record" OnClick="btnUpdate_Click"/>
        <asp:Button ID="btnDelete" runat="server" Text="Delete Record" OnClick="btnDelete_Click"/>
        <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" />

         <table class="table table-striped table-bordered table-hover" id="sample_1">
                <thead>
                    <tr>
                       
                        <th>
                            <asp:Label runat="server" ID="Label1" Text="PHYSICALLOCID"></asp:Label></th>
                        <th>
                            <asp:Label runat="server" ID="Label2" Text="LOCNAME1"></asp:Label></th>
                        <th>
                            <asp:Label runat="server" ID="Label3" Text="LOCNAME2"></asp:Label></th>
                        <th>
                            <asp:Label runat="server" ID="Label8" Text="LOCNAME3"></asp:Label></th>
                         <th>
                            <asp:Label runat="server" ID="Label9" Text="Check"></asp:Label></th>
                    </tr>
                </thead>
                <tbody>
             <asp:ListView ID="Listview1" runat="server" >
                        <LayoutTemplate>
                            <tr id="ItemPlaceholder" runat="server">
                            </tr>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>

                                <td>
                                    <asp:Label ID="lblID2" runat="server" Visible="false" Text='<%# Eval("LOCATIONID")%>'></asp:Label>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("PHYSICALLOCID")%>'></asp:Label>
                                    
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("LOCNAME1")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("LOCNAME2")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("LOCNAME3")%>'></asp:Label></td>
                               <td>
                                   <asp:CheckBox ID="chkchecklock" runat="server" />
                               </td>
                            </tr>

                        </ItemTemplate>

                    </asp:ListView>
            
                </tbody>
              
            </table>

    </div>
         <div class="portlet-body">
                <div class="table-scrollable">
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th style="background-color: skyblue; font-size: large; font-weight: bold;">Checked Location
                                </th>
                            </tr>
                        </thead>
                        <asp:ListView ID="ListView2" runat="server">
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("LOCATIONID") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("LOCNAME1") %>'></asp:Label>
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
