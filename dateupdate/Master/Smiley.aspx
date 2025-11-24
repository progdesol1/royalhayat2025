<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Smiley.aspx.cs" Inherits="Web.Master.Smiley" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type ="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
        <script type="text/javascript">
            $(function () {               
                $('#lblmsg').show();
                setTimeout(function () { $("#lblmsg").fadeOut(1500); }, 1000);
            });
       </script>

    
        <script type="text/javascript">
            $(function () {
                $('#lblmsg2').show();
                setTimeout(function () { $("#lblmsg2").fadeOut(1500); }, 1000);
            });
       </script>
   
        <script type="text/javascript">
            $(function () {
                $('#lblmsg3').show();
                setTimeout(function () { $("#lblmsg3").fadeOut(1500); }, 1000);
            });
       </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--<asp:Button ID="Button1" runat="server" Text="Button" CssClass="Button1" />
         <style type="text/css">
        .Button1
        {
        	background-image:url('../Smilies/Smilies/72464_thank-you-smiley-face-gold-foil-stickers-and-labels.png');
        }
    </style>--%>
            <div class="row">
                <img src="../Smilies/Smilies/royal.png" style="width: 1200px; margin-top: -99px;" />
                <center>  <h1>
                        <asp:Label ID="lblmsg" runat="server" Visible="false" Style="color: green;"></asp:Label></h1></center>
                 

                  <center>   <h1>
                        <asp:Label ID="lblmsg2" runat="server" Visible="false" Style="color: yellow;"></asp:Label></h1></center>
                     
                  <center>   <h1>
                        <asp:Label ID="lblmsg3" runat="server" Visible="false" Style="color: red;"></asp:Label></h1></center>
                    
                   <center>  <h1>
                        <asp:Label ID="lblwait" runat="server" Visible="false" Style="color: green;"></asp:Label></h1></center>
                     
                   <center>  <h1>
                        <asp:Label ID="lblwait2" runat="server" Visible="false" Style="color: green;"></asp:Label></h1></center>
                    
                  <center>   <h1>
                        <asp:Label ID="lblwait3" runat="server" Visible="false" Style="color: green;"></asp:Label></h1></center>
                <div class="col-md-4">
                  
                    
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../Smilies/Smilies/0Happy.JPG" OnClick="ImageButton2_Click" Style="width: 350px;" />
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../Smilies/Smilies/1Neutral.JPG" OnClick="ImageButton1_Click1" Style="width: 380px;" />
                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="../Smilies/Smilies/2Sad.JPG" OnClick="ImageButton3_Click" Style="width: 370px; margin-bottom: 11px; margin-top: 0px;" />
                </div>
            </div>
        </div>

    </form>
</body>
</html>
