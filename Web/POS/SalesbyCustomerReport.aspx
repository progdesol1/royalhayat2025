<%@ Page Title="" Language="C#" MasterPageFile="~/POS/POSMaster.Master" AutoEventWireup="true" CodeBehind="SalesbyCustomerReport.aspx.cs" Inherits="Web.POS.SalesbyCustomerReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="shortcut icon" href="favicon.ico" />
    <script type="text/javascript">
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <div class="page-content-wrapper">

            <div class="tabbable-custom tabbable-noborder">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet light" style="border: 1px solid #abb2c9;">
                                <div class="portlet-title">
                                    <div class="caption">
                                       
                                        <span class="caption-subject font-green-sharp bold uppercase">Sales Report</span>
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:Button ID="btnPrint" runat="server" class="btn green-haze btn-circle" OnClientClick="return PrintPanel();" Text="Print" ValidationGroup="submit" />
                                        <%--   <asp:LinkButton ID="btnCancel1" runat="server" CssClass="btn green-haze btn-circle" Text="Cancel" PostBackUrl="~/Admin/Index.aspx"></asp:LinkButton>--%>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlWarningMsg" runat="server" Visible="false">
                                    <div class="alert alert-warning alert-dismissable">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                        <asp:Label ID="lblWarningMsg" Font-Size="Large" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-md-5">
                                        <div class="form-group" style="color: ">
                                            <asp:Label runat="server" ID="Label8" CssClass="col-md-4 control-label" Text="Start Date"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtstartdate" CssClass="form-control" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtstartdate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group" style="color: ">
                                            <asp:Label runat="server" ID="Label2" CssClass="col-md-4 control-label" Text="End Date"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtEnddate" CssClass="form-control" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtEnddate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-sm blue" OnClick="btnsearch_Click1"/>
                                    </div>
                                </div>
                                  <div class="row">
                                     
                                    <div class="col-md-5">
                                            <div class="form-group" style="color: ">
                                            <asp:Label runat="server" ID="Label11" CssClass="col-md-4 control-label" Text="Sales by Customer"></asp:Label>
                                            <div class="col-md-8">
                                        <asp:DropDownList ID="drpcustomer" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                </div>
                                             </div>
                                      </div>

                                <asp:Panel ID="pnlContents" runat="server">
                                    <br />
                                    <table style="width: 100%" border="1" cellpadding="1" cellspacing="1">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;"> No.</th>
                                                &nbsp;&nbsp;&nbsp;
            <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Date</th>
                                                &nbsp;&nbsp;&nbsp;
            <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Name</th>
                                                &nbsp;&nbsp;&nbsp;
            <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">UOM</th>
                                                &nbsp;&nbsp;&nbsp;

            <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Price</th>
                                                &nbsp;&nbsp;&nbsp;
            <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">QTY</th>
                                                &nbsp;&nbsp;&nbsp;
             <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Total</th>
                                                &nbsp;&nbsp;&nbsp;
             <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Profit</th>
                                                &nbsp;&nbsp;&nbsp;
             <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Dis Rate</th>
                                                &nbsp;&nbsp;&nbsp;
            <th style="text-align: center; background-color: #FFB6C1; color: #ffffff;">Customer</th>
                                                                          </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="ListView1" runat="server">
                                                <ItemTemplate>

                                                    <tr>
                                                         <td style="text-align: center;">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("RptNo") %>' ></asp:Label></td>
                                                         <td style="text-align: center;">
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Date") %>' ></asp:Label></td>
                                                         <td style="text-align: center;">
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Name") %>' ></asp:Label></td>
                                                         <td style="text-align: center;">
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("UOM") %>' ></asp:Label></td>
                                                         <td style="text-align: center;">
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("Price") %>' ></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="lblcustomer" runat="server" Text='<%# Eval("Qty") %>'></asp:Label></td>
                                                        <td style="text-align: center;">
                                                            <asp:Label ID="lblTamount" runat="server" Text='<%# Eval("Total") %>' ></asp:Label></td>
                                                          <td style="text-align: center;">
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("Profit") %>' ></asp:Label></td>
                                                          <td style="text-align: center;">
                                                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("Dis Rate") %>' ></asp:Label></td>
                                                          <td style="text-align: center;">
                                                            <asp:Label ID="Label10" runat="server" Text='<%# Eval("Customer") %>' ></asp:Label></td>


                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </tbody>

                                    </table>
                                    <br />
                                    <br />
                                    <br />
                                </asp:Panel>
                            </div>
                        </div>


                    </div>

                </div>
            </div>

    </div>
</asp:Content>
