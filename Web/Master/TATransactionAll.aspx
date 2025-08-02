<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="TATransactionAll.aspx.cs" Inherits="Web.Master.TATransactionAll" %>

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
                                        <i class="fa fa-gift"></i>Employee Attendence
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="btnPagereload" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>

                                </div>

                                <div class="portlet-body">
                                    <div class="portlet-body form">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general1">
                                                    <div class="form-body">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td align="center">
                                                                    <div class="contenttitle2" style="margin: 0; padding: 0">
                                                                        <asp:Label ID="Label1" runat="server" Text="Employee" Font-Bold="true"></asp:Label>
                                                                         <asp:DropDownList ID="drpEMP" runat="server" CssClass="select2-container form-control select2me" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="drpEMP_SelectedIndexChanged"></asp:DropDownList>
                                                                        <asp:Label ID="Label2" runat="server" Text="" Font-Bold="true"></asp:Label>


                                                                        <asp:DropDownList ID="drpmonth" runat="server" CssClass="select2-container form-control select2me input-medium" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="drpmonth_SelectedIndexChanged">
                                                                            <asp:ListItem Text="-- Month --" Enabled="true" Selected="True" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                                            <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                                            <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                                            <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                                            <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                                            <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                                            <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                                            <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>

                                                    <table class="table table-striped table-bordered table-hover" id="sample_11">
                                                        <thead>
                                                            <tr>

                                                                <th>
                                                                    <asp:Label runat="server" ID="lblhTenet" Text="Date"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label runat="server" ID="lblhName" Text="Day"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label runat="server" ID="lblhMyItems" Text="Time"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label runat="server" ID="lblhCompaigntype" Text="Present?"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label runat="server" ID="lblhTypeID" Text="Total Time"></asp:Label></th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:ListView ID="ListView111" runat="server" OnItemDataBound="ListView111_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblTenet" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbldatdseTo" runat="server" Text='<%# Eval("DayName")%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbldat4223eTodsd" runat="server" Text='<%# getDateData(Convert.ToDateTime(Eval("date")))%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbld23eTodsd" runat="server" Text='<%# GetAbsantData(Convert.ToDateTime(Eval("date")))%>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lbldated2Todsd" runat="server" Text='<%# GetTotalTime(Convert.ToDateTime(Eval("date")))%>'></asp:Label>
                                                                            <asp:HiddenField ID="HiddenField1" Value='<%# GetTotalMinute(Convert.ToDateTime(Eval("date")))%>' runat="server" />
                                                                            <asp:Label runat="server" ID="lblTotalTime" Visible="false" Font-Size="13px" Font-Bold="true" Text='<%# GetTotalTime(Convert.ToDateTime(Eval("date")))%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </tbody>
                                                        <tfoot>
                                                            <tr>
                                                                <td colspan="4"></td>
                                                                <td>
                                                                    <asp:Label ID="lblTotalTimeFinal" runat="server" Text="0.00"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </tfoot>
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
