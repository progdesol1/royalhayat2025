<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="AdvanshSearch.aspx.cs" Inherits="Web.CRM.AdvanshSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div >
        <div id="b" runat="server" class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <div class="page-head">
                    <ol class="breadcrumb">
                        <li><a href="#"></a></li>
                        <li><a href="#">
                            <asp:Label ID="lblproductbyctegory" runat="server" Text="Advance Search"></asp:Label></a></li>
                    </ol>

                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div id="form1" class="form-horizontal form-row-seperated">
                            <%--<div class="portlet box yellow-crusta">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Advance Search
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:LinkButton ID="lbApproveIss" class="btn blue" runat="server" OnClick="lbApproveIss_Click">Search</asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton2" class="btn blue" runat="server" OnClick="LinkButton2_Click">Cancel</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general1">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <asp:Label runat="server" Style="color: #333333" CssClass="control-label col-md-2" ID="lblsignbyperson" Text="Product Name"></asp:Label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtproduct" placeholder="Name" data-toggle="tooltip" ToolTip="Category Name" CssClass="form-control" runat="server"></asp:TextBox>

                                                            </div>
                                                            <asp:Label runat="server" Style="color: #333333" CssClass="control-label col-md-2" ID="Label1" Text="Barcode No"></asp:Label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtbarcode" placeholder="Barcode No" data-toggle="tooltip" ToolTip="Category Name" CssClass="form-control" runat="server"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" Style="color: #333333" CssClass="control-label col-md-2" ID="Label2" Text="Alternate Code1"></asp:Label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtaltcoed1" placeholder="Alternate Code1" data-toggle="tooltip" ToolTip="Category Name" CssClass="form-control" runat="server"></asp:TextBox>

                                                            </div>
                                                            <asp:Label runat="server" Style="color: #333333" CssClass="control-label col-md-2" ID="Label4" Text="Alternate Code2"></asp:Label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtaltcoed2" placeholder="Alternate Code2" data-toggle="tooltip" ToolTip="Category Name" CssClass="form-control" runat="server"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" Style="color: #333333" CssClass="control-label col-md-2" ID="Label5" Text="Product Name (O)"></asp:Label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtproduc2" placeholder="Product Name (O)" data-toggle="tooltip" ToolTip="Category Name" CssClass="form-control" runat="server"></asp:TextBox>

                                                            </div>
                                                            <asp:Label runat="server" Style="color: #333333" CssClass="control-label col-md-2" ID="Label6" Text="Product Name (O2)"></asp:Label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtprodu3" placeholder="Product Name (O2)" data-toggle="tooltip" ToolTip="Category Name" CssClass="form-control" runat="server"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" Style="color: #333333" CssClass="control-label col-md-2" ID="Label7" Text="Short Name"></asp:Label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtshortname" placeholder="Short Name" data-toggle="tooltip" ToolTip="Category Name" CssClass="form-control" runat="server"></asp:TextBox>

                                                            </div>
                                                            <asp:Label runat="server" Style="color: #333333" CssClass="control-label col-md-2" ID="Label8" Text="Brand"></asp:Label>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="drpBeand" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                         <div class="form-group">
                                                            <asp:Label runat="server" Style="color: #333333" CssClass="control-label col-md-2" ID="Label3" Text="Main Category"></asp:Label>
                                                            <div class="col-md-4">
                                                                 <asp:DropDownList ID="drpMinCategr" CssClass="form-control select2" OnSelectedIndexChanged ="drpMinCategr_SelectedIndexChanged" AutoPostBack ="true"  runat="server"></asp:DropDownList>

                                                            </div>
                                                            <asp:Label runat="server" Style="color: #333333" CssClass="control-label col-md-2" ID="Label13" Text="Sub Category"></asp:Label>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="drpsubctegory" CssClass="form-control select2" Enabled ="false"  runat="server"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" Style="color: #333333" CssClass="control-label col-md-2" ID="Label9" Text="MSRP"></asp:Label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtmsrp" placeholder="MSRP" Text="0" data-toggle="tooltip" ToolTip="Category Name" CssClass="form-control" runat="server"></asp:TextBox>

                                                            </div>
                                                            <asp:Label runat="server" Style="color: #333333" CssClass="control-label col-md-2" ID="Label10" Text="Price"></asp:Label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtprice" placeholder="Price" Text="0" data-toggle="tooltip" ToolTip="Category Name" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" Style="color: #333333" CssClass="control-label col-md-2" ID="Label11" Text="Remarks"></asp:Label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtremarck" placeholder="Remarks" data-toggle="tooltip" ToolTip="Category Name" CssClass="form-control" runat="server"></asp:TextBox>

                                                            </div>
                                                            <asp:Label runat="server" Style="color: #333333" CssClass="control-label col-md-2" ID="Label12" Text="Keywords"></asp:Label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtkeywored" placeholder="Keywords" data-toggle="tooltip" ToolTip="Category Name" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-horizontal form-row-seperated">
                                        <div class="portlet box green-turquoise">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <i class="fa fa-gift"></i>List For  <asp:Label ID="lbl" Style="color: #333333" runat="server" ></asp:Label> 
                                                </div>
                                                <div class="tools">
                                                    <a href="javascript:;" class="collapse"></a>
                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                    <a href="javascript:;" class="reload"></a>
                                                    <a href="javascript:;" class="remove"></a>
                                                </div>
                                                <div class="actions btn-set">
                                                     <asp:LinkButton ID="btnsubmit" class="btn blue"   runat="server" OnClick ="btnsubmit_Click"  >Submit</asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="portlet-body" style="padding-left: 25px;">
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>

                                                        <table class="table table-striped table-bordered table-hover" id="sample_2">
                                                            <thead class="repHeader">
                                                                <tr>
                                                                    <th style="width: 8%">
                                                                        <asp:Label ID="lblSN" Style="color: #333333" runat="server" Text="#"></asp:Label></th>
                                                                    <th style="width: 10%">
                                                                        <asp:Label ID="lblGrCI" Style="color: #333333" runat="server" Text="Name"></asp:Label></th>
                                                                    <th style="width: 10%">
                                                                        <asp:Label ID="lblGrCN" Style="color: #333333" runat="server" Text="Email"></asp:Label></th>
                                                                    <th style="width: 10%">
                                                                        <asp:Label ID="lblGrCD" Style="color: #333333" runat="server" Text="Address"></asp:Label></th>
                                                                    <th style="width: 35%">
                                                                        <asp:Label ID="lblGrPC" Style="color: #333333" runat="server" Text="City"></asp:Label></th>
                                                                    <th style="width: 12%">
                                                                        <asp:Label ID="lblGrCT" Style="color: #333333" runat="server" Text="PostalCode"></asp:Label></th>
                                                                    <th style="width: 15%">
                                                                        <asp:Label ID="lblGrAC" Style="color: #333333" runat="server" Text="Select"></asp:Label></th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="pMasterGridview" runat="server" OnItemCommand ="pMasterGridview_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr class="gradeA">
                                                                            <td>
                                                                                <asp:Label ID="lblSRNO" Style="color: #333333" runat="server" Text='<%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblmuprodid" Style="color: #333333" runat="server" Text='<%# Eval("COMPNAME1") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblGrICN" Style="color: #333333" runat="server" Text='<%# Eval("EMAIL1") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblGrICD" Style="color: #333333" runat="server" Text='<%# Eval("ADDR1") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblGrIPC" Style="color: #333333" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblGrICT" Style="color: #333333" runat="server" Text='<%#Eval("POSTALCODE") %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                 <%--<asp:LinkButton ID="btnselect" class="btn blue" CommandName="btnselect" CommandArgument='<%# Eval("MYPRODID")%>' PostBackUrl='<%# Session["ADMInPrevious"].ToString()+"?MYPRODID="+ Eval("MYPRODID")%>' runat="server" >Seclect</asp:LinkButton>--%>
                                                                                <asp:CheckBox ID="cbSelect"  runat="server" />
                                                                            </td>
                                                                            
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>


                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
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
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
