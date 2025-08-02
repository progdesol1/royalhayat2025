<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="serviesHubIPS.aspx.cs" Inherits="Web.Sales.serviesHubIPS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }
    </style>
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />

    <link href="../assets/global/css/components-rounded.css" id="style_components" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout3/css/layout.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout3/css/themes/default.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/admin/layout3/css/custom.css" rel="stylesheet" type="text/css" />
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
                                        <i class="fa fa-gift"></i>Show Report For Daily Sales
                                        <asp:Label runat="server" ID="lblHeader"></asp:Label>
                                        <asp:TextBox Style="color: #333333" ID="txtHeader" runat="server" Visible="false"></asp:TextBox>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="btnPagereload" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <%-- <div class="btn-group btn-group-circle btn-group-solid">
                                            <asp:Button ID="btnFirst" class="btn red" runat="server" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" />
                                        </div>--%>
                                        <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" OnClick="btnAdd_Click" Text="Search" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" Text="Cancel" />
                                        <asp:Button ID="btnEditLable" runat="server" class="btn green-haze btn-circle" Text="Update Label" />
                                        &nbsp;
                                        <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server">E&nbsp;<img src="../assets/global/img/flags/us.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server">A&nbsp;<img src="../assets/global/img/flags/ae.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server">F&nbsp;<img src="../assets/global/img/flags/fr.png" /></asp:LinkButton>
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
                                                                <div class="form-group" >
                                                                    <asp:Label runat="server" ID="lblActive1s" CssClass="col-md-4 control-label" Text="Date"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" CssClass="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:Label runat="server" ID="Label12" CssClass="col-md-2 control-label" Text="From"></asp:Label>
                                                                        <asp:TextBox ID="txtdateFrom" Placeholder="DD/MM/YYYY" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="TextBoxtxtdateFrom_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateFrom" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtdateFrom" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblActive2h" CssClass="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" CssClass="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="lblUOMNAME1s" class="col-md-0 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:Label runat="server" ID="Label13" CssClass="col-md-2 control-label" Text="To"></asp:Label>
                                                                        <asp:TextBox ID="txtdateTO" Placeholder="DD/MM/YYYY" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtendertxtdateTO" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateTO" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtdateTO" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUOMNAME2h" class="col-md-0 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <%--<div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" >
                                                                    <asp:Label runat="server" ID="Label14" CssClass="col-md-4 control-label" Text="Customer"></asp:Label><asp:TextBox runat="server" ID="TextBox3" CssClass="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:Label runat="server" ID="Label15" CssClass="col-md-2 control-label" Text="From"></asp:Label>
                                                                        <asp:TextBox ID="txtCustomerFrom" Placeholder="Customer Name" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                        
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtdateFrom" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="Label16" CssClass="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox5" CssClass="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="Label17" class="col-md-0 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox6" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:Label runat="server" ID="Label18" CssClass="col-md-2 control-label" Text="To"></asp:Label>
                                                                        <asp:TextBox ID="txtCustomerTo" Placeholder="Customer Name" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                        
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtdateTO" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="Label19" class="col-md-0 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox8" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                        <%--<div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUOMNAMEO1s" class="col-md-4 control-label" Text="Consolidated"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAMEO1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:CheckBox ID="chkConsolidated" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUOMNAMEO2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAMEO2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="Label1" class="col-md-4 control-label" Text="Report Name"></asp:Label><asp:TextBox runat="server" ID="TextBox1" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpReportName" runat="server" CssClass="form-control select2me input-medium">
                                                                            <asp:ListItem Text="Report Product by customer" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="Report Product by Items" Value="2"></asp:ListItem>
                                                                            
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="Label2" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox2" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                            <asp:Panel ID="PanelProductbycustomer" Visible="false" runat="server">
                          <div class="row">
                                <div class="col-md-12">
                                    <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                    <div class="portlet box green">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-cogs font-grey"></i>
                                                <span class="caption-subject font-grey-sharp bold uppercase">Report Product by customer</span>
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <table class="table table-striped table-hover table-bordered" id="sample_editable_1">
                                                <thead>
                                                    <tr>
                                                        <th>Customer 
                                                        </th>
                                                        <th>Invoice #
                                                        </th>
                                                        <th>Date
                                                        </th>
                                                        <th>Item # Description 
                                                        </th>
                                                        <th>Qty
                                                        </th>
                                                        <th>Amount </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="ListProductbycustomer" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                   <%-- <asp:Label ID="Label10" Visible="false" runat="server" Text='<%# Eval("MYTRANSID") %>'></asp:Label>--%>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# CustVendID(Convert.ToInt32(Eval("Customer"))) %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("InvoiceNO")  %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("TRANSDATE") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                 <asp:Label ID="Label11" runat="server" Text='<%# Eval("MYPRODID")+"-"+ Eval("DESCRIPTION") %>'></asp:Label>
                                                                </td>
                                                                <td class="center">
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("QUANTITY") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                                    <%--<a class="edit" href="javascript:;">Show </a>--%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <!-- END EXAMPLE TABLE PORTLET-->
                                </div>
                            </div>
                                </asp:Panel>
                             <asp:Panel ID="PanelProductbyItems" Visible="false"  runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                    <div class="portlet box green">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-cogs font-grey"></i>
                                                <span class="caption-subject font-grey-sharp bold uppercase">Report Product by Items</span>
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <table class="table table-striped table-hover table-bordered" id="sample_editable_1">
                                                <thead>
                                                    <tr>
                                                        <th>Item # Description
                                                        </th>
                                                        
                                                        <th>Customer 
                                                        </th>
                                                        <th>Invoice # 
                                                        </th>
                                                        <th>Date    
                                                        </th>
                                                        <th>Qty </th>
                                                        <th>Amount  </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="ListProductbyItems" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%--<asp:Label ID="Label10" Visible="false" runat="server" Text='<%# Eval("MYTRANSID") %>'></asp:Label>--%>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%#  Eval("MYPRODID")+"-"+ Eval("DESCRIPTION") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# CustVendID(Convert.ToInt32(Eval("CUSTVENDID"))) %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("InvoiceNO") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                 <asp:Label ID="Label11" runat="server" Text='<%# Eval("TRANSDATE") %>'></asp:Label>
                                                                </td>
                                                                <td class="center">
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("QUANTITY") %>'></asp:Label>
                                                                </td>
                                                                 <td class="center">
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                                </td>
                                                                
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <!-- END EXAMPLE TABLE PORTLET-->
                                </div>
                            </div>
                                 </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>

    <script src="../assets/global/scripts/metronic.js" type="text/javascript"></script>
    <script src="../assets/admin/layout3/scripts/layout.js" type="text/javascript"></script>
    <script src="../assets/admin/layout3/scripts/demo.js" type="text/javascript"></script>
    <script src="../assets/admin/pages/scripts/table-editable.js"></script>
    <script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Demo.init(); // init demo features
            TableEditable.init();
        });
    </script>
</asp:Content>
