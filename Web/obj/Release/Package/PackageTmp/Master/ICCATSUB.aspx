<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="ICCATSUB.aspx.cs" Inherits="Web.Master.ICCATSUB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">

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
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Add 
                                        <asp:Label runat="server" ID="lblHeader"></asp:Label>

                                    </div>
                                    <div class="actions btn-set">
                                        <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="Add New"  OnClick="btnAdd_Click"/>
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="portlet-body form">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general1">
                                                    <div class="form-body">
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblcat" CssClass="col-md-3 control-label" Text="Category"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpcat" runat="server" CssClass="form-control select2me" AutoPostBack="true"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpcat" ErrorMessage="Category Required." CssClass="Validation" ValidationGroup="submit" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblsubcat" CssClass="col-md-3 control-label" Text="Sub Category"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpsubcat" runat="server" CssClass="form-control select2me" AutoPostBack="true"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUOMNAME1" runat="server" ControlToValidate="drpsubcat" ErrorMessage="Sub Category Required." CssClass="Validation" ValidationGroup="submit" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                               <%-- <asp:Button ID="btnsub" runat="server" Text="Submit" OnClick="btnsub_Click1" CssClass="btn btn-success" />--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="tabbable">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>Category Name</th>
                                                                <th>Sub Category Name</th>
                                                                <th>Remark</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:ListView ID="listcategory" runat="server" OnItemCommand="listcategory_ItemCommand">
                                                                <LayoutTemplate>
                                                                    <tr id="ItemPlaceholder" runat="server">
                                                                    </tr>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblcat" runat="server" Text='<%#getCatname(Convert .ToInt32( Eval("CATID")))%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblsub" runat="server" Text='<%#getSubCatname(Convert .ToInt32( Eval("SUBCATID")))%>'></asp:Label>
                                                                            <asp:TextBox ID="Remark" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="llname1" runat="server" Text='<%# Eval("REMARKS")%>'></asp:Label>
                                                                            <asp:TextBox ID="txtname1" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </td>


                                                                        <td>
                                                                            <asp:LinkButton ID="LinkEMPEDIT" runat="server" CommandName="LinkEMPEDIT" CommandArgument='<%# Eval("MYCATSUBID")%>' class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i>
                                                                                        
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkEMPDELETE" runat="server" CommandName="LinkEMPDELETE" CommandArgument='<%# Eval("MYCATSUBID")%>' class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i>
                                                                            </asp:LinkButton>
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </tbody>
                                                    </table>

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
