<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="ImportExportFunction.aspx.cs" Inherits="NewHRM.ImportExportFunction" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }
    </style>
    <script type="text/javascript">
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=pnlSuccessMsg.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div  id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
              <%--  <ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">Import Export Function </a>
                    </li>
                </ul>--%>
                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet light">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="icon-basket font-green-sharp"></i>
                                        <span class="caption-subject font-green-sharp bold uppercase">Improt</span>
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:Button ID="Button1" CssClass="btn blue-hoki" runat="server" Text="Improt" OnClick="Button1_Click" />

                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general1">
                                                <div class="form-body">

                                                    <div class="form-group" style="color: ">
                                                        <label class="col-md-2 control-label">Select File</label><div class="col-md-4">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
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
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet light">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="icon-basket font-green-sharp"></i>
                                        <span class="caption-subject font-green-sharp bold uppercase">Export</span>
                                    </div>

                                    <div class="actions btn-set">


                                        <asp:Button ID="btnExport" CssClass="btn blue-hoki" runat="server" Text="Export" OnClick="btnExport_Click" />

                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general1">
                                                <div class="form-body">
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            Choose Module <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList ID="drpModulName" class="form-control select2me" runat="server" AutoPostBack="true" ></asp:DropDownList>
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
    </div>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="footer" runat="server">
    <script>

        jQuery(document).ready(function () {
            // initiate layout and plugins
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Demo.init(); // init demo features
            TableEditable.init();
            ComponentsEditors.init();
            $("#draggable").draggable({
                handle: ".modal-header"
            });
            UITree.init();
            ComponentsFormTools.init();
            TableAdvanced.init();

        });
    </script>
</asp:Content>
