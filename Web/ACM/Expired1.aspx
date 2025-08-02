<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="Expired1.aspx.cs" Inherits="Web.ACM.Expired1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box red">

                                <p align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Menu Is Expired Please Contact Your Administrator" Font-Bold="true" Font-Size="XX-Large" Font-Italic="true" ForeColor="White" CssClass="center-block"></asp:Label>
                                </p>

                            </div>
                        </div>
                    </div>

                </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">

</asp:Content>
