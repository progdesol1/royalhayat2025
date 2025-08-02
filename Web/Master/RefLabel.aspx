<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="RefLabel.aspx.cs" Inherits="Web.Master.RefLabel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<asp:LinkButton ID="lbllabel1" runat="server" CommandName="lbllabel1" PostBackUrl='<%#"REFTABLE.aspx?id="+ Eval("Id") %>' class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i>Link1</asp:LinkButton><br /><br />    
    <asp:LinkButton ID="lbllabel2" runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-pencil"></i>Link2</asp:LinkButton><br /><br />
    <asp:LinkButton ID="lbllabel3" runat="server" CssClass ="btn btn-sm blue filter-option"><i class="fa fa-pencil"></i>Link3</asp:LinkButton>--%>
    <asp:Button ID="btn1" runat="server" Text="Link1" class="btn btn-sm yellow filter-submit margin-bottom" OnClick="btn1_Click" />
    <asp:Button ID="btn2" runat="server" Text="Link2" class="btn btn-sm red filter-cancel" OnClick="btn2_Click"/>
    <asp:Button ID="btn3" runat="server" Text="Link3" CssClass ="btn btn-sm blue filter-option" OnClick="btn3_Click"/>
</asp:Content>
