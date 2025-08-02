<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="Sales_Opportunities.aspx.cs" Inherits="Web.Sales.Sales_Opportunities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
       
      .aspNetDisabled btn btn-icon-only green{
           cursor: not-allowed !important;

       }
         .aspNetDisabled{
             cursor: not-allowed !important;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div  id="b" runat="server">
        <div class="page-head">
            <ol class="breadcrumb">
                <li><a href="../ECOMM/AdminIndex.aspx">
                    <asp:Label ID="lblACM" runat="server" Text="Home"></asp:Label>
                </a></li>
                <li class="active">
                    <asp:Label ID="lblOpp" runat="server" Text="Opportunities"></asp:Label>
                </li>
            </ol>
        </div>
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box red">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Opportunities
                                    </div>
                                    <div class="tools">
                                        <a id="shlinkProductDetails" runat="server" href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                    </div>
                                </div>
                                <div id="shProductDetails" runat="server" class="portlet-body" style="padding-left: 25px;">
                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general">

                                                <div class="form-body">
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="lblSubject" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Subject"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtSubject" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="lblExpectRev" SkinID="label1" class="col-md-4 control-label" runat="server" Text="Expected Revenue"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtExpect" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label1" class="col-md-2 control-label" SkinID="label1" runat="server" Text="at %"></asp:Label>
                                                            <div class="col-md-10">
                                                                <asp:TextBox ID="TextBox1"   CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label3" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Customer"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="ddlCustomer" CssClass="form-control" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label4" SkinID="label1" class="col-md-4 control-label" runat="server" Text="Next Action"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtNextAction" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label5" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Email"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label6" SkinID="label1" class="col-md-4 control-label" runat="server" Text="CallProposal"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="TxtCallProposal" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label7" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Phone"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtPhone" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label8" SkinID="label1" class="col-md-4 control-label" runat="server" Text="Expected Closing"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtExpectclose" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label9" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Priority"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="ddlPriority" CssClass="form-control" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label10" SkinID="label1" class="col-md-4 control-label" runat="server" Text="SalesPerson"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="ddlSalesPerson" CssClass="form-control" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label11" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Categories"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="ddlCategories" CssClass="form-control" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="btnsetposition">
                                                        <asp:Button ID="btnAddEditUser" class="btn btn-primary" runat="server" Text="Submit" />
                                                        <input id="btnExit" type="button" class="btn btn-default" value="Exit" onclick="ClearAllText()" />
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="hidOpp" runat="server" />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            </div> </div> </div> </div> </div> </div> 
</asp:Content>
