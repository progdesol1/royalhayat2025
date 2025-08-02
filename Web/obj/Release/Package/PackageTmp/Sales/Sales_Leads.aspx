<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="Sales_Leads.aspx.cs" Inherits="Web.Sales.Sales_Leads" %>

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
    <script type="text/javascript">
        function ClearAllText() {
            document.getElementById('ContentPlaceHolder1_ddlCustomer').value = "0";
            //document.getElementById('ContentPlaceHolder1_ddlCountry').value = "0";
            //document.getElementById('ContentPlaceHolder1_ddlState').value = "0";
            document.getElementById('ContentPlaceHolder1_ddlSalePers').value = "0";
            document.getElementById('ContentPlaceHolder1_ddlCategories').value = "0";
            document.getElementById('ContentPlaceHolder1_ddlPriority').value = "0";
            document.getElementById('ContentPlaceHolder1_ddlTitle').value = "0";
            document.getElementById('ContentPlaceHolder1_txtStreet').value = "";
            document.getElementById('ContentPlaceHolder1_txtAddress').value = "";
            document.getElementById('ContentPlaceHolder1_txtCity').value = "";
            document.getElementById('ContentPlaceHolder1_txtZip').value = "";
            document.getElementById('ContentPlaceHolder1_txtPhone').value = "";
            document.getElementById('ContentPlaceHolder1_txtZip').value = "";
            document.getElementById('ContentPlaceHolder1_txtMobile').value = "";
            document.getElementById('ContentPlaceHolder1_txtFax').value = "";
            document.getElementById('ContentPlaceHolder1_txtEmail').value = "";
            document.getElementById('ContentPlaceHolder1_btnSubmit').value = "Submit";
        }
    </script>
    <script type="text/javascript">

        function showSuccessToast() {
            $().toastmessage('showSuccessToast', "Order created successfully.");
        }
    </script>
    <div  id="b" runat="server">
        <div class="page-head">
            <ol class="breadcrumb">
                <li><a href="../ECOMM/AdminIndex.aspx">
                    <asp:Label ID="lblSales" runat="server" Text="Home"></asp:Label>
                </a></li>
                <li class="active">
                    <asp:Label ID="lblRMaster" runat="server" Text="Sales Leads"></asp:Label>
                </li>
            </ol>
        </div>
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box green ">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Sales Leads
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
                                                            <asp:Label ID="lblCompName" class="col-md-4 control-label" runat="server" SkinID="label1" Text="Company Name"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtCompany" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="lblContName" class="col-md-4 control-label" runat="server" SkinID="label1" Text="Contact Name"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtContName" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="lblCustomer" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Customer"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="ddlCustomer" CssClass="form-control select2me" runat="server"></asp:DropDownList>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label1" class="col-md-4 control-label" SkinID="label1" runat="server" Text="TiTle"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="ddlTitle" CssClass="form-control select2me" runat="server">
                                                                    <asp:ListItem>--Title--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label2" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Country"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="ddlCountry" placeholder="Country" CssClass="form-control select2me" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged1">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label3" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Email"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtEmail" CssClass="form-control" placeholder="Email" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label4" class="col-md-4 control-label" SkinID="label1" runat="server" Text="State"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="ddlState"  CssClass="form-control select2me" placeholder="State" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label5" class="col-md-4 control-label" SkinID="label1" runat="server" Text="City"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtCity"  runat="server" CssClass="form-control" placeholder="City"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label6" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Function"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtFunction" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label7" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Street"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtStreet" placeholder="Street" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label8" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Phone"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="TextBox1" placeholder="Phone" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label9" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Address"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label10" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Mobile"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtMobile" placeholder="Mobile" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label11" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Zip"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtZip" CssClass="form-control" placeholder="Zip" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label12" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Fax"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtFax" placeholder="Fax" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label13" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Salesperson"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="ddlSalePers" CssClass="form-control select2me" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label14" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Priority"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="ddlPriority" CssClass="form-control select2me" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="Label15" class="col-md-4 control-label" SkinID="label1" runat="server" Text="Categories"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="DropDownList1" CssClass="form-control select2me" runat="server">
                                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                                    <asp:ListItem>Product</asp:ListItem>
                                                                    <asp:ListItem>Supply</asp:ListItem>
                                                                    <asp:ListItem>ABC</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="btnsetposition">
                                                        <asp:Button ID="btnAddEditUser" class="btn btn-primary" runat="server" Text="Submit" />
                                                        <input id="btnExit" type="button" class="btn btn-default" value="Exit" onclick="ClearAllText();" />
                                                        <%--<asp:Button ID="btnExit" class="btn btn-default" runat="server" Text="Exit" OnClientClick="return ClearAllText();" meta:resourcekey="btnExitResource1" />--%>
                                                    </div>
                                                    <asp:HiddenField ID="hidContactId" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>






                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
