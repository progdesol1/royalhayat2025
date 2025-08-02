<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="CityStateCountry.aspx.cs" Inherits="Web.Master.CityStateCountry" %>

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
                                        <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="Add New" />
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
                                                                    <asp:Label runat="server" ID="lblCountry" CssClass="col-md-3 control-label" Text="Country"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpCountry" runat="server" CssClass="form-control select2me" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpCountry" ErrorMessage="Short Name UOM Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblstate" CssClass="col-md-3 control-label" Text="State"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="DrpState" runat="server" CssClass="form-control select2me" OnSelectedIndexChanged="DrpState_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUOMNAME1" runat="server" ControlToValidate="DrpState" ErrorMessage="Uomname1 Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:Button ID="btnsub" runat="server" Text="Submit" OnClick="btnsub_Click" CssClass="btn btn-success"/>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="tabbable">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>City ID</th>
                                                                <th>City Name</th>
                                                                <th>City Arabic</th>
                                                                <th>City Other</th>
                                                                <th>Action</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:ListView ID="listSocialMedia" runat="server" OnItemCommand="listSocialMedia_ItemCommand">
                                                                <LayoutTemplate>
                                                                    <tr id="ItemPlaceholder" runat="server">
                                                                    </tr>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblcityID" runat="server" Text='<%# Eval("CityID")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="llname" runat="server" Text='<%# Eval("CityEnglish")%>'></asp:Label>
                                                                            <asp:TextBox ID="txtname" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </td>
                                                                         <td>
                                                                            <asp:Label ID="llname1" runat="server" Text='<%# Eval("CityArabic")%>'></asp:Label>
                                                                            <asp:TextBox ID="txtname1" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </td>
                                                                         <td>
                                                                            <asp:Label ID="llname2" runat="server" Text='<%# Eval("CityOther")%>'></asp:Label>
                                                                            <asp:TextBox ID="txtname2" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </td>
                                                                      
                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkEMPEDIT" runat="server" CommandName="LinkEMPEDIT" CommandArgument='<%# Eval("CityID")%>'>
                                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="../ECOMM/images/editRec.png" />
                                                                                    </asp:LinkButton>
                                                                                    <asp:LinkButton ID="LinkEditSave" Visible="false" runat="server" CommandName="LinkEditSave" CommandArgument='<%# Eval("CityID") %>' Style="width: 28px; height: 22px;" CssClass="btn btn-sm btn-success">
                                                                                                            <i class="fa fa-check"></i>
                                                                                    </asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="LinkEMPDELETE" runat="server" CommandName="LinkEMPDELETE" CommandArgument='<%# Eval("CityID")%>'>
                                                                                        <asp:Image ID="Image2" runat="server" ImageUrl="../ECOMM/images/deleteRec.png" />
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
