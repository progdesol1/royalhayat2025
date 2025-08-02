<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="GYMReport1.aspx.cs" Inherits="Web.Master.GYMReport1" %>
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
                                        <%--<asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" OnClick="btnAdd_Click" Text="Search" />--%>
                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" Text="Cancel" />
                                        <%--<asp:Button ID="btnEditLable" runat="server" class="btn green-haze btn-circle" Text="Update Label" />--%>
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
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUOMNAMESHORT1s" class="col-md-4 control-label" Text="System"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAMESHORT1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpSystem" runat="server" CssClass="form-control select2me input-medium" OnSelectedIndexChanged="drpSystem_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUOMNAMESHORT2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAMESHORT2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">

                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpToSystem" runat="server" CssClass="form-control select2me input-medium" OnSelectedIndexChanged="drpSystem_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                            </div>                                           
                                                        </div>
                                                        <div class="row" style="display:none;">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUOMNAME21s" CssClass="col-md-4 control-label" Text="Main Trans Type"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME21s" CssClass="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpMainTranTypefrom" runat="server" CssClass="form-control select2me input-medium" OnSelectedIndexChanged="drpMainTranTypefrom_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                        <asp:Label runat="server" ID="lblUOMNAME22h" CssClass="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME22h" CssClass="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUOMNAME31s" class="col-md-0 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME31s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpMainTranTypeTo" runat="server" CssClass="form-control select2me input-medium" OnSelectedIndexChanged="drpMainTranTypeTo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUOMNAME32h" class="col-md-0 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME32h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="display:none;">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblREMARKS1s" CssClass="col-md-4 control-label" Text="Sub Trans Type"></asp:Label><asp:TextBox runat="server" ID="txtREMARKS1s" CssClass="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpsupTranTypeFrom" runat="server" CssClass="form-control select2me input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblREMARKS2h" CssClass="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtREMARKS2h" CssClass="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCRUP_ID1s" class="col-md-0 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRUP_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpSubTranTypeTo" runat="server" CssClass="form-control select2me input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCRUP_ID2h" class="col-md-0 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRUP_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblActive1s" CssClass="col-md-4 control-label" Text="Date"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" CssClass="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtdateFrom" Placeholder="MM/DD/YYYY" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="TextBoxtxtdateFrom_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateFrom" Format="M/d/yyyy"></cc1:CalendarExtender>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtdateFrom" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblActive2h" CssClass="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" CssClass="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUOMNAME1s" class="col-md-0 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtdateTO" Placeholder="MM/DD/YYYY" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtendertxtdateTO" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateTO" Format="M/d/yyyy"></cc1:CalendarExtender>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtdateTO" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUOMNAME2h" class="col-md-0 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="Label1" class="col-md-4 control-label" Text="Report Name"></asp:Label><asp:TextBox runat="server" ID="TextBox1" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpReportName" runat="server" CssClass="form-control select2me input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpReportName_SelectedIndexChanged">
                                                                            <asp:ListItem Text="-- Select Report --" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="Daily Sales Report Consolildated (Today)" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="Daily Sales Report Detailed (Today)" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="Daily Sales Report Consolildated" Value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="Sales Report Detailed" Value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="Daily Sales Return Report Consolildated (Today)" Value="5"></asp:ListItem>
                                                                            <asp:ListItem Text="Daily Sales Return Report Detailed (Today)" Value="6"></asp:ListItem>
                                                                            <asp:ListItem Text="Daily Sales Return Report Consolildated" Value="7"></asp:ListItem>
                                                                            <asp:ListItem Text="Sales Return Report Detailed" Value="8"></asp:ListItem>
                                                                            <asp:ListItem Text="Daily Purchase Report Consolildated (Today)" Value="9"></asp:ListItem>
                                                                            <asp:ListItem Text="Daily Purchase Report Detailed (Today)" Value="10"></asp:ListItem>
                                                                            <asp:ListItem Text="Daily Purchase Report Consolildated" Value="11"></asp:ListItem>
                                                                            <asp:ListItem Text="Purchase Report Detailed" Value="12"></asp:ListItem>
                                                                            <asp:ListItem Text="Daily Purchase Return Report Consolildated (Today)" Value="13"></asp:ListItem>
                                                                            <asp:ListItem Text="Daily Purchase Return Report Detailed (Today)" Value="14"></asp:ListItem>
                                                                            <asp:ListItem Text="Daily Purchase Return Report Consolildated" Value="15"></asp:ListItem>
                                                                            <asp:ListItem Text="Purchase Return Report Detailed" Value="16"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="Label2" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox2" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="actions btn-set">
                                                                <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" OnClick="btnAdd_Click" Text="Search" />
                                                                <asp:Button ID="btnPrints" runat="server" Text="Prints" class="btn yellow btn-circle" Visible="false" OnClick="btnPrints_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUOMNAMEO1s" class="col-md-4 control-label" Text="Consolidated"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAMEO1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:CheckBox ID="chkConsolidated" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUOMNAMEO2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAMEO2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlDailySale" runat="server" Visible="false">
                                
                                 <div class="row">
                                    <div class="col-md-12">
                                        
                                        <div class="portlet box green">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <i class="fa fa-cogs font-grey"></i>
                                                    <span class="caption-subject font-grey bold uppercase">Report For Daily Sales <asp:Label ID="lblSaleRet" Visible="false" runat="server" Text="Return"></asp:Label></span>
                                                </div>

                                                <div class="tools">
                                                    <a href="javascript:;" class="collapse"></a>
                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                    <a href="javascript:;" class="reload"></a>
                                                    <a href="javascript:;" class="remove"></a>
                                                </div>
                                                <div class="actions btn-set">
                                                    <asp:Button ID="lblExport" runat="server" Text="Export to Excel" CssClass="btn red-soft btn-circle" OnClick="lblExport_Click" />
                                                </div>
                                            </div>
                                            <div class="portlet-body">
                                               
                                                <table class="table table-striped table-hover table-bordered" id="sample_editable_1">
                                                    <thead>
                                                        <tr>
                                                            <th>Date 
                                                            </th>
                                                            <th>Transaction<br />
                                                                (Comp#/User#)
                                                            </th>
                                                            <th>TransDocNO
                                                            </th>
                                                            <th>Amount
                                                            </th>
                                                            <th>Paid BY
                                                            </th>
                                                            <th>Reference<br />
                                                                Project/Batch/GL
                                                            </th>
                                                            <th>Customer/<br />
                                                                Vender
                                                            </th>
                                                            <th>Action
                                                            </th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListView1_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblmytranid" Visible="false" runat="server" Text='<%# Eval("MYTRANSID") %>'></asp:Label>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("ENTRYDATE", "{0:M/d/yyyy}") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# CompAndUser(Convert.ToInt32(Eval("MYTRANSID"))) %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("TransDocNo") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("TOTAMT") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label7" runat="server" Text='<%# PaidBy(Convert.ToInt32(Eval("MYTRANSID"))) %>'></asp:Label>
                                                                    </td>
                                                                    <td class="center">
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("REFERENCE") %>'></asp:Label>
                                                                    </td>
                                                                    <td class="center">
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# CustVendID(Convert.ToInt32( Eval("CUSTVENDID"))) %>'></asp:Label>
                                                                    </td>
                                                                    <td>                                                                       
                                                                         <asp:DropDownList ID="DrpsaleHDList" CssClass="form-horizontal select2me input-xsmall" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrpsaleHDList_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                                            <asp:ListItem Value="1" Text="Detail"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Print"></asp:ListItem>
                                                                            <asp:ListItem Value="3" Text="Trans"></asp:ListItem>
                                                                        </asp:DropDownList>                                                                
                                                                        <%--<asp:LinkButton class="btn btn-lg blue" Visible="false" CommandName="Prints" CommandArgument='<%# Eval("MYTRANSID") %>' ID="btnprient" Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;" runat="server">Print <i class="fa fa-print"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lblview" Visible="false" CommandName="view" CommandArgument='<%# Eval("MYTRANSID")%>' runat="server" class="btn btn-sm red" Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;">Details&nbsp;<i class="fa fa-users"></i></asp:LinkButton>--%>
                                                                        <asp:LinkButton CssClass="btn btn-lg blue" Visible="false" CommandName="Prints" ID="btnprient" CommandArgument='<%# Eval("MYTRANSID") %>' runat="server" Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;">GO <i class="fa fa-print"></i></asp:LinkButton>
                                                                        <asp:LinkButton CssClass="btn btn-lg red" Visible="false" CommandName="view" ID="lblview" CommandArgument='<%# Eval("MYTRANSID")%>' runat="server" Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;">GO&nbsp;<i class="fa fa-users"></i></asp:LinkButton>
                                                                        <asp:LinkButton CssClass="btn btn-lg green" Visible="false" CommandName="Trans" ID="btnTrans" CommandArgument='<%# Eval("MYTRANSID")%>' runat="server" Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;">GO&nbsp;<i class="fa fa-search-plus"></i></asp:LinkButton>

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
                            </asp:Panel>
                           
                            <asp:Panel ID="pnlDailySaleDTL" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-12">
                                        <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                        <div class="portlet box green">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <i class="fa fa-cogs font-grey"></i>
                                                    <span class="caption-subject font-grey bold uppercase">Report For Daily Sales <asp:Label ID="lblsaleRetDet" Visible="false" runat="server" Text="Return"></asp:Label> Details</span>
                                                    <asp:Label ID="lblDetailDT" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="tools">
                                                    <a href="javascript:;" class="collapse"></a>
                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                    <a href="javascript:;" class="reload"></a>
                                                    <a href="javascript:;" class="remove"></a>
                                                </div>

                                                <div class="actions btn-set">
                                                    <asp:Button ID="lblexportDT" runat="server" Text="Export to Excel" CssClass="btn red-soft btn-circle" OnClick="lblexportDT_Click" />
                                                </div>
                                                <div class="tools">
                                                    <%-- <asp:Label ID="Label13" runat="server" Text="All Sale Report"></asp:Label>  --%>
                                                    <asp:CheckBox ID="ChkAllHDDT" runat="server" Text="All Sale Report" />&nbsp &nbsp
                                                </div>
                                            </div>

                                            <div class="portlet-body">

                                                <table class="table table-striped table-hover table-bordered" id="">
                                                    <thead>
                                                        <tr>
                                                            <th>Item Code
                                                            </th>
                                                            <th>TransDocNO
                                                            </th>
                                                            <th>Description<br />
                                                                (Serial/Colour/Size)
                                                            </th>
                                                            <th>QTY
                                                            </th>
                                                            <th>Local Amount<br />
                                                                Per Piece
                                                            </th>
                                                            <%-- <th>Foreign Amount<br />
                                                                Per Piece
                                                            </th>--%>
                                                            <th>Total Local
                                                            </th>
                                                            <%-- <th>Total Foreign
                                                            </th>--%>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:ListView ID="ListViewDT" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label12" runat="server" Text='<%# InvoiceNODT(Convert.ToInt32(Eval("MYTRANSID"))) %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("DESCRIPTION") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("BIN_ID") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# (LocalPiece(Convert.ToInt32(Eval("MYTRANSID")))).ToString() == "POS Invoice"? Eval("UNITPRICE"): "" %>'></asp:Label>
                                                                    </td>
                                                                    <%--  <td>
                                                                        <asp:Label ID="Label7" runat="server" Text='<%# (ForeignPiece(Convert.ToInt32(Eval("MYTRANSID")))).ToString() == "Foreign Invoice"? Eval("UNITPRICE"): "" %>'></asp:Label>
                                                                    </td>--%>
                                                                    <td class="center">
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# (Local(Convert.ToInt32(Eval("MYTRANSID")))).ToString() == "POS Invoice"? Eval("AMOUNT"):"" %>'></asp:Label>
                                                                    </td>
                                                                    <%-- <td class="center">
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# (Foreign(Convert.ToInt32(Eval("MYTRANSID")))).ToString() == "Foreign Invoice"? Eval("AMOUNT"):"" %>'></asp:Label>
                                                                    </td>--%>
                                                                    <td style="text-align: center;">
                                                                        <%--<a class="edit" href="javascript:;">Show </a>--%>
                                                                        <asp:LinkButton class="btn btn-lg blue" CommandName="Prints" CommandArgument='<%# Eval("MYTRANSID") %>' ID="btnprient" Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;" runat="server">Print <i class="fa fa-print"></i></asp:LinkButton>
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
                            <asp:Panel ID="pnlPurchase" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-12">
                                        <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                        <div class="portlet box green">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <i class="fa fa-cogs font-grey"></i>
                                                    <span class="caption-subject font-grey-sharp bold uppercase">Report For Purchase <asp:Label ID="lblpurRet" Visible="false" runat="server" Text="Return"></asp:Label></span>
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
                                                            <th>Date 
                                                            </th>
                                                            <th>Transaction<br />
                                                                (Comp#/User#)
                                                            </th>
                                                            <th>Invoice#
                                                            </th>
                                                            <th>Amount
                                                            </th>
                                                            <th>Paid BY
                                                            </th>
                                                            <th>Reference<br />
                                                                Project/Batch/GL
                                                            </th>
                                                            <th>Customer/<br />
                                                                Vender
                                                            </th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:ListView ID="ListViewPur" runat="server" OnItemCommand="ListViewPur_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label10" Visible="false" runat="server" Text='<%# Eval("MYTRANSID") %>'></asp:Label>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("ENTRYDATE", "{0:M/d/yyyy}") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# CompAndUser(Convert.ToInt32(Eval("MYTRANSID"))) %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("InvoiceNO") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("TOTAMT") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label7" runat="server" Text='<%# PaidBy(Convert.ToInt32(Eval("MYTRANSID"))) %>'></asp:Label>
                                                                    </td>
                                                                    <td class="center">
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("REFERENCE") %>'></asp:Label>
                                                                    </td>
                                                                    <td class="center">
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# CustVendID(Convert.ToInt32( Eval("CUSTVENDID"))) %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <%--<asp:LinkButton class="btn btn-lg blue" CommandName="btnprient" CommandArgument='<%# Eval("MYTRANSID") %>' ID="Print" runat="server">Print <i class="fa fa-print"></i></asp:LinkButton>--%>
                                                                        <asp:DropDownList ID="DropDownListPur" CssClass="form-horizontal select2me input-xsmall" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0" Text="Action" ></asp:ListItem>
                                                                            <asp:ListItem Value="1" Text="Detail"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Print"></asp:ListItem>
                                                                            <asp:ListItem Value="3" Text="Trans"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:LinkButton CssClass="btn btn-lg red" Visible="false" ID="lblPurview" CommandName="view" CommandArgument='<%# Eval("MYTRANSID")%>' runat="server" >GO <i class="fa fa-users"></i></asp:LinkButton>
                                                                        <asp:LinkButton CssClass="btn btn-lg blue" Visible="false" ID="lblPurPrints" CommandName="Prints" CommandArgument='<%# Eval("MYTRANSID")%>' runat="server" >GO <i class="fa fa-users"></i></asp:LinkButton>
                                                                        <asp:LinkButton CssClass="btn btn-lg green" Visible="false" ID="lblPurTrans" CommandName="Trans" CommandArgument='<%# Eval("MYTRANSID")%>' runat="server" >GO <i class="fa fa-users"></i></asp:LinkButton>
                                                                        <%--<asp:LinkButton CssClass="btn btn-lg blue" Visible="false" CommandName="Prints" ID="btnprient" CommandArgument='<%# Eval("MYTRANSID") %>' runat="server" Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;">GO <i class="fa fa-print"></i></asp:LinkButton>
                                                                        <asp:LinkButton CssClass="btn btn-lg red" Visible="false" CommandName="view" ID="lblview" CommandArgument='<%# Eval("MYTRANSID")%>' runat="server" Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;">GO&nbsp;<i class="fa fa-users"></i></asp:LinkButton>
                                                                        <asp:LinkButton CssClass="btn btn-lg green" Visible="false" CommandName="Trans" ID="btnTrans" CommandArgument='<%# Eval("MYTRANSID")%>' runat="server" Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;">GO&nbsp;<i class="fa fa-search-plus"></i></asp:LinkButton>--%>
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
                            <asp:Panel ID="pnlPurchaseDTL" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-12">
                                        <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                        <div class="portlet box green">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <i class="fa fa-cogs font-grey"></i>
                                                    <span class="caption-subject font-grey bold uppercase">Report For Purchase <asp:Label ID="lblPurretDet" Visible="false" runat="server" Text="Return"></asp:Label> Details</span>
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
                                                            <th>Item Code
                                                            </th>
                                                            <th>Description<br />
                                                                (Serial/Colour/Size)
                                                            </th>
                                                            <th>QTY
                                                            </th>
                                                            <th>Local Amount<br />
                                                                Per Piece
                                                            </th>
                                                            <th>Foreign Amount<br />
                                                                Per Piece
                                                            </th>
                                                            <th>Total Local
                                                            </th>
                                                            <th>Total Foreign
                                                            </th>
                                                            <th>Action
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:ListView ID="ListViewPurDT" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label10" runat="server" Text='<%# Eval("MYTRANSID") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("DESCRIPTION") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("QUANTITY") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# (LocalPiece(Convert.ToInt32(Eval("MYTRANSID")))).ToString() == "Cashier Board"? Eval("UNITPRICE"): "" %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label7" runat="server" Text='<%# (ForeignPiece(Convert.ToInt32(Eval("MYTRANSID")))).ToString() == "Foreign Invoice"? Eval("UNITPRICE"): "" %>'></asp:Label>
                                                                    </td>
                                                                    <td class="center">
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# (Local(Convert.ToInt32(Eval("MYTRANSID")))).ToString() == "Cashier Board"? Eval("AMOUNT"):"" %>'></asp:Label>
                                                                    </td>
                                                                    <td class="center">
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# (Foreign(Convert.ToInt32(Eval("MYTRANSID")))).ToString() == "Foreign Invoice"? Eval("AMOUNT"):"" %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <%--<a class="edit" href="javascript:;">Show </a>--%>
                                                                        <asp:DropDownList ID="DropDownList4" CssClass="form-horizontal select2me input-xsmall" runat="server">
                                                                            <asp:ListItem Value="1" Text="Print"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Trans"></asp:ListItem>
                                                                        </asp:DropDownList>
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
                            <asp:Panel ID="pnlCollectionRPT" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-md-12">
                                        <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                        <div class="portlet box green">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <i class="fa fa-cogs font-grey"></i>
                                                    <span class="caption-subject font-grey-sharp bold uppercase">Cash Collection Report</span>
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
                                                            <th>Date
                                                            </th>
                                                            <th>Paid BY
                                                            </th>
                                                            <th>Collected Amount
                                                            </th>
                                                            <th>Account Posted
                                                            </th>
                                                            <th>JV Number
                                                            </th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:ListView ID="ListViewICTRPayTermHD" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label10" Visible="false" runat="server" Text='<%# Eval("MyTransID") %>'></asp:Label>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("CheckOutDate") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# paidbypayterm(Convert.ToInt32(Eval("PaymentTermsId")))  %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("ReferenceNo") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label11" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                    <td class="center">
                                                                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("JVRefNo") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList5" CssClass="form-horizontal select2me input-xsmall" runat="server">
                                                                            <asp:ListItem Value="1" Text="Detail"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Print"></asp:ListItem>
                                                                            <asp:ListItem Value="3" Text="Trans"></asp:ListItem>
                                                                        </asp:DropDownList>
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
