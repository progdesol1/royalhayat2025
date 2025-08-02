<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.Master" AutoEventWireup="true" CodeBehind="Brochure.aspx.cs" Inherits="Web.Brochure" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <iframe src="http://pos53.com/bcompleteERPSolution.aspx" class="demo-preview__frame" runat="server" style="width: 100%;height: 1000px;" id="ifrm" name="ifrm"></iframe>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
     <script type="text/javascript">
         jQuery('#navigation ul li').css('display', 'inline-block');
         jQuery(document).ready(function () {
             Layout.init();
             Layout.initOWL();
             Layout.initTwitter();
             LayersliderInit.initLayerSlider();
             Layout.initImageZoom();

         });
    </script>
</asp:Content>
