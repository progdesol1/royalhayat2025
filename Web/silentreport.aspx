<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="silentreport.aspx.cs" Inherits="Web.silentreport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="portlet-body">
                <table class="table table-striped table-bordered table-hover" id="sample_2">
                    <thead>
                        <tr>
                            <th style="text-align: center"><strong>Complain No</strong></th>
                            <th style="text-align: center"><strong>MRN No</strong></th>
                            <th style="text-align: center"><strong>Date</strong></th>
                            <th style="text-align: center"><strong>Action</strong></th>
                            <th style="text-align: center"><strong>Remark</strong></th>
                            <th style="text-align: center"><strong>Status</strong></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:ListView ID="ListView2" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:Label ID="lblcomplain" runat="server" Text='<%# Eval("complain")%>'></asp:Label>
                                        <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("MasterCODE") %>'></asp:Label>
                                        <asp:Label ID="lbldepartID" Visible="false" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                        <asp:Label ID="lblnoticeemail" Visible="false" runat="server" Text='<%# Eval("NoticeEmail") %>'></asp:Label>
                                        <asp:Label ID="lblDeptName" Visible="false" runat="server" Text='<%# Eval("DeptName") %>'></asp:Label>
                                    </td>

                                    <td style="text-align: center;">
                                        <asp:Label ID="lblmrn" runat="server" Text='<%# Eval("MRN")%>'></asp:Label></td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="lbltickdk" runat="server" Text='<%#Eval("UploadDate")%>'></asp:Label></td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="lbltickcat" runat="server" Text='<%#getactionname(Convert .ToInt32(Eval("aspcomment")))%>'></asp:Label></td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="lblremark" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label></td>
                                    <td style="text-align: center;">
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("MyStatus")%>'></asp:Label></td>

                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
