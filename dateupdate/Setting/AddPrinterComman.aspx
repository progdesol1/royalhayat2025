<%@ Page Title="" Language="C#" MasterPageFile="~/Setting/SettingMaster.Master" AutoEventWireup="true" CodeBehind="AddPrinterComman.aspx.cs" Inherits="Web.Setting.AddPrinterComman" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
        <div class="row">
            <div class="col-md-12 ">
                <!-- BEGIN SAMPLE FORM PORTLET-->
                <div class="portlet box green ">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-gift"></i>Add Printer
                        </div>
                        <div class="tools">
                            <a href="" class="collapse"></a>
                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                            <a href="" class="reload"></a>
                            <a href="" class="remove"></a>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal" role="form">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Title </label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtsiteName" class="form-control" runat="server" placeholder="20"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Type</label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="drpaccount" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="1" Text="Network"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Linux"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Windows"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Profile  </label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="1" Text="Simple"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Star-branded"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Espon Tep"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="P822D"></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="Default"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Characters per line</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="TextBox14" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">IP Address</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="TextBox15" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Port</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="TextBox16" class="form-control" runat="server" placeholder="9100"></asp:TextBox>
                                                <h6>Most printers are open on port 9100</h6>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row ">
                                    <div class="col-md-1"></div>
                                    <!-- BEGIN SAMPLE FORM PORTLET-->
                                    <div class="col-md-11">
                                        <button type="submit" class="btn blue">Add Printer</button>

                                        <!-- END SAMPLE FORM PORTLET-->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
   
</asp:Content>
