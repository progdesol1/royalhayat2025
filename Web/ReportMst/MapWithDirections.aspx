<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MapWithDirections.aspx.cs" Inherits="Web.ReportMst.MapWithDirections" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>


<%@ Register Src="~/ReportMst/GoogleMapForASPNet.ascx" TagPrefix="uc1" TagName="GoogleMapForASPNet" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Map with Driving Directions</title>
      <script type="text/javascript">
	  // disable back *******************************************
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", -1);
        window.onunload = function () { null };
        //*********************************************************
		
		
        //JS right click and ctrl+(n,a,j) disable
        function disableCtrlKeyCombination(e) {
            //list all CTRL + key combinations you want to disable
            var forbiddenKeys = new Array('a', 'n', 'j');

            var key;
            var isCtrl;

            if (window.event) {
                key = window.event.keyCode;     //IE
                if (window.event.ctrlKey)
                    isCtrl = true;
                else
                    isCtrl = false;
            }
            else {
                key = e.which;     //firefox

                if (e.ctrlKey)
                    isCtrl = true;
                else
                    isCtrl = false;
            }
            //Disabling F5 key
            if (key == 116) {
                alert('Key  F5 has been disabled.');
                return false;
            }
            //if ctrl is pressed check if other key is in forbidenKeys array
            if (isCtrl) {

                for (i = 0; i < forbiddenKeys.length; i++) {
                    //  alert(String.fromCharCode(key));
                    //case-insensitive comparation
                    if (forbiddenKeys[i].toLowerCase() == String.fromCharCode(key).toLowerCase()) {
                        alert('Key combination CTRL + '
                            + String.fromCharCode(key)
                            + ' has been disabled.');
                        return false;
                    }
                    if (key == 116) {
                        alert('Key combination CTRL + F5 has been disabled.');
                        return false;
                    }
                }
            }
            return true;
        }

        //Disable right mouse click Script

        var message = "Right click Disabled!";

        ///////////////////////////////////
        function clickIE4() {
            if (event.button == 2) {
                alert(message);
                return false;
            }
        }

        function clickNS4(e) {
            if (document.layers || document.getElementById && !document.all) {
                if (e.which == 2 || e.which == 3) {
                    alert(message);
                    return false;
                }
            }
        }

        if (document.layers) {
            document.captureEvents(Event.MOUSEDOWN);
            document.onmousedown = clickNS4;
        }

        else if (document.all && !document.getElementById) {
            document.onmousedown = clickIE4;
        }

        document.oncontextmenu = new Function("alert(message);return false");
		 </script>
</head>
<body>
    <h3><a href="Default.aspx">Back</a></h3>
    <form id="form1" runat="server">
    <h3>Map with Driving Directions</h3>
    <div>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:TextBox ID="txtcount" runat="server" Text="5" Width="395px"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="set" OnClick="Button1_Click" />
            <table>
            
                <asp:ListView ID="ListView1" runat="server">
                    <ItemTemplate>
                        <tr>
                             <td align="right">First Address :</td><td><asp:TextBox ID="txtFirstAddress" runat="server" Text="11 Wall Street, NY" Width="395px"></asp:TextBox></td>
            </tr>
                    </ItemTemplate>
                </asp:ListView>
           
           
            <tr>
            <td align="right">Show direction instructions?  :</td><td><asp:CheckBox ID="chkShowDirections" runat="server" Checked=true ></asp:CheckBox></td>
            </tr>
            <tr>
            <td align="right">Hide Markers? :</td><td><asp:CheckBox ID="chkHideMarkers" runat="server"  ></asp:CheckBox></td>
            </tr>
            <tr>
            <td align="right">Color of direction line :</td><td><asp:TextBox ID="txtDirColor" runat="server" Text="#FF2200" Width="120px"></asp:TextBox>(Hex value)</td>
            </tr>
            <tr>
            <td align="right">Direction line width :</td><td><asp:TextBox ID="txtDirWidth" runat="server" Text="3" Width="120px"></asp:TextBox>(1 to 6)</td>
            </tr>
            <tr>
            <td align="right">Direction line opacity :</td><td><asp:TextBox ID="txtDirOpacity" runat="server" Text="0.6" Width="120px"></asp:TextBox>(0.1 to 1.0)</td>
            </tr>
            <tr>
            <td></td><td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnDrawDirections" runat="server" Text="Draw Directions" OnClick="btnDrawDirections_Click" />
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            </tr>
            </table>
        <br /><br />
        <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" />
    </div>
    </form>
</body>
</html>

