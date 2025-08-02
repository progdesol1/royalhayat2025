<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionTimeOut.aspx.cs" Inherits="Web.ACM.SessionTimeOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">

                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-danger alert-dismissable">
                     
                        <asp:Label ID="lblMsg" runat="server" ></asp:Label>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
