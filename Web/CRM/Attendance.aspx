<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="Attendance.aspx.cs" Inherits="Web.CRM.Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <%--<ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">Attendance</a>
                    </li>
                </ul>--%>

                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
                <!--pageheader-->
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Attendance
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="btnPagereload" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:Button ID="btnThumRegister" CssClass="btn btn-sm red" runat="server" Text="Thum Register" OnClick="btnThumRegister_Click"/>
                                    </div>
                                </div>

                                <div class="portlet-body">
                                    <div class="portlet-body form">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general1">
                                                    <div class="form-body">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 98px;">
                                                                    <asp:CheckBox ID="chePresent" runat="server" Text=" check In" AutoPostBack="true" OnCheckedChanged="chePresent_CheckedChanged" CssClass="portlet-body" />
                                                                    <%--Present--%>
                                                                </td>
                                                                <td style="width: 98px;">
                                                                    <asp:CheckBox ID="checkabsent" runat="server" Text="Check Out" CssClass="portlet-body" OnCheckedChanged="checkabsent_CheckedChanged" />
                                                                    <%--Absent--%>
                                                                </td>

                                                                <td style="width: 100px">
                                                                    <asp:DropDownList ID="drpUsers" CssClass="select2-container form-control select2me" Width="150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpUsers_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>

                                                                <td align="center">
                                                                    <div class="contenttitle2" style="margin: 0; padding: 0">
                                                                        <h3>
                                                                            <asp:Label ID="lblTitlePage" runat="server" Text="Label"></asp:Label></h3>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="drpmonth" runat="server" CssClass="select2-container form-control select2me" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="drpmonth_SelectedIndexChanged">
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
                                                                </td>
                                                                <td width="100">
                                                                    <asp:Button ID="btnPrevious" runat="server" Text="<<" Font-Bold="true"
                                                                        CssClass="btn grey-cascade" OnClick="btnPrevious_Click" />
                                                                    <asp:Button ID="btnNext" runat="server" Text=">>" Font-Bold="true"
                                                                        CssClass="btn grey-cascade" OnClick="btnNext_Click" />

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
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
                                                                        <asp:ListView ID="Listview1" runat="server" OnItemDataBound="Listview1_ItemDataBound">
                                                                            <LayoutTemplate>
                                                                                <tr id="ItemPlaceholder" runat="server">
                                                                                </tr>
                                                                            </LayoutTemplate>
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
                                                                        <tfoot>
                                                                            <tr>
                                                                                <td colspan="4"></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblTotalTimeFinal" runat="server" Text="0.00"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </tfoot>
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
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
