<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="UnderCuntruction.aspx.cs" Inherits="Web.Sales.UnderCuntruction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <%-- <ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">Under Cuntruction </a>
                    </li>
                </ul>--%>
                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box blue">

                                <p align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Page under Contruction" Font-Bold="true" Font-Size="XX-Large" Font-Italic="true" ForeColor="WindowText" CssClass="center-block"></asp:Label>
                                </p>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
