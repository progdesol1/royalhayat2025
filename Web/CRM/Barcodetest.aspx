<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Barcodetest.aspx.cs" Inherits="Web.CRM.Barcodetest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="Button1" runat="server" Text="BArcode Generate 1" OnClick="Button1_Click"/>
            <asp:Button ID="Button2" runat="server" Text="BArcode Generate 2" OnClick="Button2_Click"/>
            <asp:Button ID="btnSubmit" runat="server"
                Text="Backup" ValidationGroup="submit" OnClick="btnSubmit_Click" />

            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="txtpass" runat="server" Text="Decrypt" OnClick="txtpass_Click" />
            <asp:Button ID="txtPassEnc" runat="server" Text="Encryp" OnClick="txtPassEnc_Click" />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>
        <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />

        <script src="../assets/toast/jquery.js"></script>
        <script src="../assets/toast/script.js"></script>
        <script src="../assets/toast/toastr.min.js"></script>
        <link href="../assets/toast/toastr.min.css" rel="stylesheet" />

    </form>
</body>
</html>
