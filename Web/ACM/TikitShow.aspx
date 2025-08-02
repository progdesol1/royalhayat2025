<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="TikitShow.aspx.cs" Inherits="Web.ACM.TikitShow" %>

<%@ Register Src="~/UserControl/NewTickit.ascx" TagPrefix="uc1" TagName="NewTickit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div  id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <uc1:NewTickit runat="server" ID="NewTickit" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
