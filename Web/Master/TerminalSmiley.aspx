<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TerminalSmiley.aspx.cs" Inherits="Web.Master.TerminalSmiley" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     


</head>
<body>
    <form id="form1" runat="server">

    <div>

        <h1>Select Terminal Name to Give us Rate</h1>
        <asp:DropDownList ID="drpterminal" runat="server" style="width: 300px;width: 300px;height: 30px;" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="drpterminal_SelectedIndexChanged"></asp:DropDownList>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpterminal" ErrorMessage="Please select Terminal Name" CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
              <br />       
        <br />                                               
          <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-default" ValidationGroup="submit"  OnClick="btnsubmit_Click" OnClientClick="SetTarget();" style="background-color: green;color: white;width: 100px;height: 30px;"/>
        
          </div>
    </form>
</body>
</html>
