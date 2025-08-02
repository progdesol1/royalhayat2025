<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="AllReportForHealthBar.aspx.cs" Inherits="Web.Master.AllReportForHealthBar" %>
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
                                        <i class="fa fa-gift"></i>Show All Report For Subscriber
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
                                      
                                        <%--<asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" OnClick="btnAdd_Click" Text="Search" />--%>
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
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblActive1s" CssClass="col-md-4 control-label" Text="Date" style="margin-top:15px;"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" CssClass="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <strong>
                                                                            <asp:Label ID="lblFrom" runat="server" Text="From" Style="margin-left: 10px;"></asp:Label></strong>
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtdateFrom" Placeholder="dd/MMM/yyyy" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="TextBoxtxtdateFrom_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateFrom" Format="dd/MMM/yyyy"></cc1:CalendarExtender>
                                                                        <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtdateFrom" ValidChars="/" FilterType="Custom, numbers" runat="server" />--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblActive2h" CssClass="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" CssClass="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUOMNAME1s" class="col-md-0 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <strong>
                                                                            <asp:Label ID="lblTo" runat="server" Text="TO" Style="margin-left: 10px;"></asp:Label></strong>
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtdateTO" Placeholder="dd/MMM/yyyy" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtendertxtdateTO" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateTO" Format="dd/MMM/yyyy"></cc1:CalendarExtender>
                                                                        <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtdateTO" ValidChars="/" FilterType="Custom, numbers" runat="server" />--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUOMNAME2h" class="col-md-0 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOMNAME2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>                                                       
                                                        <br />
                                                    </div>
                                                    <div class="actions btn-set">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-3" style="margin-bottom: 10px;">
                                                                    <asp:Button ID="btnRepCustomer" runat="server" class="btn green-haze btn-circle" Text="Customer Report" Style="width: 222px;" OnClick="btnRepCustomer_Click" />
                                                                </div>
                                                                <div class="col-md-3" style="margin-bottom: 10px;">
                                                                    <asp:Button ID="btnRepPlan" runat="server" class="btn green-haze btn-circle" Text="Plan Report" Style="width: 222px;" OnClick="btnRepPlan_Click" />
                                                                </div>
                                                               <%-- <div class="col-md-3" style="margin-bottom: 10px;">
                                                                    <asp:Button ID="btnRepDriverCheckList" runat="server" class="btn green-haze btn-circle" Text="Driver Check List Report" Style="width: 222px;" OnClick="btnRepDriverCheckList_Click"/>
                                                                </div>--%>
                                                                 <div class="col-md-3" style="margin-bottom: 10px;">
                                                                    <asp:Button ID="btnRepDeliveryCard" runat="server" class="btn green-haze btn-circle" Text="Delivery Card Report" Style="width: 222px;" OnClick="btnRepDeliveryCard_Click" />
                                                                </div>
                                                                <div class="col-md-3" style="margin-bottom: 10px;">
                                                                    <asp:Button ID="btnRepKitchen" runat="server" class="btn green-haze btn-circle" Text="Kitchen Report" Style="width: 222px;" OnClick="btnRepKitchen_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="col-md-3" style="margin-bottom: 10px;">
                                                                    <asp:Button ID="btnRepOnHold" runat="server" class="btn green-haze btn-circle" Text="Customer OnHold List" OnClick="btnRepOnHold_Click" Style="width: 222px;"/>
                                                                </div>
                                                            </div>
                                                           <%-- <div class="col-md-12">
                                                                <div class="col-md-3" style="margin-bottom: 10px;">
                                                                    <asp:Button ID="btnsaleToHDC" runat="server" class="btn green-haze btn-circle" Text="Sale Report" Style="width: 222px;" />
                                                                </div>
                                                                <div class="col-md-3" style="margin-bottom: 10px;">
                                                                    <asp:Button ID="btnsReturnHDC" runat="server" class="btn green-haze btn-circle" Text="Sale Return" Style="width: 222px;"/>
                                                                </div>
                                                                <div class="col-md-3" style="margin-bottom: 10px;">
                                                                    <asp:Button ID="btnPURHDC" runat="server" class="btn green-haze btn-circle" Text="Purchase Report" Style="width: 222px;"/>
                                                                </div>
                                                                <div class="col-md-3" style="margin-bottom: 10px;">
                                                                    <asp:Button ID="btnPurReportHDC" runat="server" class="btn green-haze btn-circle" Text="Purchase Return" Style="width: 222px;"/>
                                                                </div>
                                                            </div>--%>
                                                           <%-- <div class="col-md-12">
                                                                <div class="col-md-3" style="margin-bottom: 10px;">
                                                                    <asp:Button ID="btnsaleToDTC" runat="server" class="btn green-haze btn-circle" Text="Sale Report Detailed" Style="width: 222px;" />
                                                                </div>
                                                                <div class="col-md-3" style="margin-bottom: 10px;">
                                                                    <asp:Button ID="btnsReturnDTC" runat="server" class="btn green-haze btn-circle" Text="Sale Return Detailed" Style="width: 222px;"/>
                                                                </div>
                                                                <div class="col-md-3" style="margin-bottom: 10px;">
                                                                    <asp:Button ID="btnPURDTC" runat="server" class="btn green-haze btn-circle" Text="Purchase Report Detailed" Style="width: 222px;"/>
                                                                </div>
                                                                <div class="col-md-3" style="margin-bottom: 10px;">
                                                                    <asp:Button ID="btnPurReportDTC" runat="server" class="btn green-haze btn-circle" Text="Purchase Return Detailed" Style="width: 222px;"/>
                                                                </div>
                                                            </div>--%>
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
