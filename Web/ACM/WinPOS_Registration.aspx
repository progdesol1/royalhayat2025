<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WinPOS_Registration.aspx.cs" Inherits="Web.ACM.WinPOS_Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POS_Registration</title>
    <link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css">
    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="../assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css">
    <link href="../assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/select2/select2.css" />
    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/bootstrap-datepicker/css/datepicker.css" />
    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" />
    <link href="../assets/global/plugins/fancybox/source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL STYLES -->
    <!-- BEGIN THEME STYLES -->
    <link href="../assets/global/css/components-rounded.css" id="style_components" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout4/css/layout.css" rel="stylesheet" type="text/css" />
    <link id="style_color" href="../assets/admin/layout4/css/themes/light.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout4/css/custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function openModalsmall2() {
            $('#small2').modal('show');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="content">

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-horizontal form-row-seperated">
                                <div class="portlet light">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-basket font-green-sharp"></i>
                                            <span class="caption-subject font-green-sharp bold uppercase">Registration </span>

                                        </div>
                                        <div class="actions btn-set">
                                            <a href="Login.aspx" class="btn btn-default btn-circle"><i class="fa fa-angle-double-left"></i>Back
                                            </a>
                                            <a href="WinPOS_Registration.aspx" class="btn btn-default btn-circle "><i class="fa fa-reply"></i>Reset</a>
                                        </div>

                                    </div>
                                    <div class="portlet-body">
                                        <div class="tabbable">
                                            <div class="progress progress-striped">
                                                <div id="Progress" runat="server" class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%">
                                                </div>
                                            </div>
                                            <ul class="nav nav-tabs">
                                                <li class="active" id="tabcrd" runat="server">
                                                    <a href="#tab_general" data-toggle="tab">Company Registration </a>
                                                </li>
                                                <li id="tabstd" runat="server">
                                                    <a href="#tab_meta" data-toggle="tab">Store Detail </a>
                                                </li>
                                                <li id="tabtrm" runat="server">
                                                    <a href="#tab_images" data-toggle="tab">Terminal </a>
                                                </li>
                                                <li id="tabtrge" runat="server">
                                                    <a href="#tab_reviews" data-toggle="tab">User Registration </a>
                                                </li>

                                            </ul>
                                            <div class="tab-content no-space">

                                                <div class="tab-pane active" id="tab_general" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="form-body">
                                                            <%--<span class="help-block" style="margin-bottom: 0px; padding-left: 10px;">Existing Company </span>
                                                            <div class="col-md-12 form-control height-auto" style="margin-top: 10px;">
                                                                <div class="form-group">
                                                                    <div class="col-md-4">
                                                                        <label class="col-md-3 control-label">
                                                                            TenentID: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-9">
                                                                            <input type="text" class="form-control" name="product[name]" placeholder="" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <label class="col-md-3 control-label">
                                                                            User Name: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-9">
                                                                            <input type="text" class="form-control" name="product[name]" placeholder="" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <label class="col-md-3 control-label">
                                                                            PassWord: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-9">
                                                                            <input type="text" class="form-control" name="product[name]" placeholder="" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <span class="help-block" style="margin-bottom: 0px; padding-left: 10px;">Company Registration </span>--%>
                                                            <div class="col-md-12 form-control height-auto" style="margin-top: 10px;">
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            TenentID: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txttenent" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Shop ID: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtshopid" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidatorPostalCode" runat="server" ErrorMessage="Shop ID Required" ControlToValidate="txtshopid" ValidationGroup="CRD"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Company Name in English: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtCNameENG" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCNameENG_TextChanged"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Company Name ENG Required" ControlToValidate="txtCNameENG" ValidationGroup="CRD"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Country: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:DropDownList ID="drpcountry" CssClass="form-control input-medium" runat="server"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Country Required" ControlToValidate="drpcountry" ValidationGroup="CRD" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Company Name in Arabic: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtCNameAra" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Company Name ARABIC Required" ControlToValidate="txtCNameAra" ValidationGroup="CRD"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Default Language: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:DropDownList ID="DrpDefaultLang" CssClass="form-control input-medium" runat="server">
                                                                                <asp:ListItem Text="English" Value="English"></asp:ListItem>
                                                                                <asp:ListItem Text="Arabic" Value="Arabic"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Company Name in Franch: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtCNameFra" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Company Name FRANCH Required" ControlToValidate="txtCNameFra" ValidationGroup="CRD"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Logo: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btn-circle green-haze btn-sm" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group last">
                                                                    <div class="col-md-12 col-sm-push-1">
                                                                        <asp:Button ID="btnCRD" runat="server" Text="Countinues" CssClass="btn btn-sm blue" OnClick="btnCRD_Click" ValidationGroup="CRD" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="tab-pane" id="tab_meta" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="form-body">
                                                            <div class="col-md-12 form-control height-auto">
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Company Name: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtstoreCMDName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Company Name Required" ControlToValidate="txtstoreCMDName" ValidationGroup="STORE"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            TAX Registration No: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtstoreTAXREG" runat="server" CssClass="form-control" Text="000"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator6" runat="server" ErrorMessage="TAX Registration No Required" ControlToValidate="txtstoreTAXREG" ValidationGroup="STORE"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Phone: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtStorePhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Phone Required" ControlToValidate="txtStorePhone" ValidationGroup="STORE"></asp:RequiredFieldValidator>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderMobileNo" ValidChars="1234567890" TargetControlID="txtStorePhone" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            TAX Percentage (%): <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTAXPER" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator8" runat="server" ErrorMessage="TAX Percentage Required" ControlToValidate="txtTAXPER" ValidationGroup="STORE"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Company Address: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtStoreADD" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator9" runat="server" ErrorMessage="Company Address Required" ControlToValidate="txtStoreADD" ValidationGroup="STORE"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Discount Percentage (%): <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtstoreDISPER" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator10" runat="server" ErrorMessage="Discount Percentage Required" ControlToValidate="txtstoreDISPER" ValidationGroup="STORE"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Footer Massage: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtstoreFooter" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator12" runat="server" ErrorMessage="Footer Massage Required" ControlToValidate="txtstoreFooter" ValidationGroup="STORE"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Web Site: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtstoreWEBsite" runat="server" CssClass="form-control" Text="http://www.erp53.com"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator11" runat="server" ErrorMessage="Web Site Required" ControlToValidate="txtstoreWEBsite" ValidationGroup="STORE"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            invoice Additional Line <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtstoreAddLine" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            FaceBook: 
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtstorefacebook" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Twitter
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtstoretwitter" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Insta:
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtstoreinsta" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group last">
                                                                    <div class="col-md-12 col-sm-push-1">
                                                                        <asp:Button ID="btnstore" runat="server" Text="Continues" CssClass="btn btn-sm green" ValidationGroup="STORE" OnClick="btnstore_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane" id="tab_images" runat="server">
                                                    <div class="col-md-12">

                                                        <div class="form-body">
                                                            <div class="col-md-12 form-control height-auto">
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Terminal Type: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:DropDownList ID="drpTERMtype" runat="server" CssClass="form-control input-medium">
                                                                                <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                                                                <asp:ListItem Text="Main Cashier" Value="Main Cashier"></asp:ListItem>
                                                                                <asp:ListItem Text="Order Taking Cashier" Value="Order Taking Cashier"></asp:ListItem>
                                                                                <asp:ListItem Text="Kitchen" Value="Kitchen"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            TAX Registration No: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTERMTaxReg" runat="server" CssClass="form-control" Text="000"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator13" runat="server" ErrorMessage="TAX Registration No Required" ControlToValidate="txtTERMTaxReg" ValidationGroup="TERM"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Terminal Name(AL_RE): <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTERMName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator14" runat="server" ErrorMessage="Terminal Name Required" ControlToValidate="txtTERMName" ValidationGroup="TERM"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            TAX Percentage(%): <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTERMTaxPer" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator16" runat="server" ErrorMessage="TAX Percentage Required" ControlToValidate="txtTERMTaxPer" ValidationGroup="TERM"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Phone: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTERMPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator15" runat="server" ErrorMessage="Phone Required" ControlToValidate="txtTERMPhone" ValidationGroup="TERM"></asp:RequiredFieldValidator>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" ValidChars="1234567890" TargetControlID="txtTERMPhone" FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Discount Percentage(%): <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTEMPDisPer" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator17" runat="server" ErrorMessage="Discount Percentage Required" ControlToValidate="txtTEMPDisPer" ValidationGroup="TERM"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Email: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTERMEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator18" runat="server" ErrorMessage="email Required" ControlToValidate="txtTERMEmail" ValidationGroup="TERM"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Web Site: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTERMWebsite" runat="server" CssClass="form-control" Text="http://www.erp53.com"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            FaceBook: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTermFacebook" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Twitter: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTERMtwitter" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Allow Minimum Sales: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Sync After: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTERMsyncafter" runat="server" CssClass="form-control" Text="60"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Terminal Address: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTERMadd" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator19" runat="server" ErrorMessage="Terminal Address Required" ControlToValidate="txtTERMadd" ValidationGroup="TERM"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Footer Massage: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTERMFooter" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator20" runat="server" ErrorMessage="Footer Massage Required" ControlToValidate="txtTERMFooter" ValidationGroup="TERM"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Invoice Additional Line: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTERMinvline" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Insta: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtTERMinsta" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group last">
                                                                    <div class="col-md-12 col-sm-push-1">
                                                                        <asp:Button ID="btnTERM" runat="server" Text="Countinues" CssClass="btn btn-sm green-jungle" OnClick="btnTERM_Click" ValidationGroup="TERM" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>


                                                    </div>


                                                </div>
                                                <div class="tab-pane" id="tab_reviews" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="form-body">
                                                            <asp:Panel class="alert alert-danger" ID="pnlErrorMsg" Visible="False" runat="server">
                                                                <button data-close="alert" class="close"></button>
                                                                <asp:Label ID="lblerrmsg" runat="server" Text="Label" meta:resourcekey="lblerrmsgResource1"></asp:Label>
                                                            </asp:Panel>
                                                            <div class="col-md-12 form-control height-auto">
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Name: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtUSERREGname" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator21" runat="server" ErrorMessage="Name Required" ControlToValidate="txtUSERREGname" ValidationGroup="Reg"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Last Name: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtUSERREGLname" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator22" runat="server" ErrorMessage="Last Name Required" ControlToValidate="txtUSERREGLname" ValidationGroup="Reg"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            User Name: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtUSERREGuername" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator23" runat="server" ErrorMessage="User Name Required" ControlToValidate="txtUSERREGuername" ValidationGroup="Reg"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            PassWord: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtUSERREGpassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator24" runat="server" ErrorMessage="Password Required" ControlToValidate="txtUSERREGpassword" ValidationGroup="Reg"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Contact No: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtUSERREGcontact" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator25" runat="server" ErrorMessage="Contact No Required" ControlToValidate="txtUSERREGcontact" ValidationGroup="Reg"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Email: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtUSERREGEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator26" runat="server" ErrorMessage="Email Required" ControlToValidate="txtUSERREGEmail" ValidationGroup="Reg"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Date of Birth: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtUSERREGdob" runat="server" CssClass="form-control" placeholder="MMM/dd/yyyy"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="#a94442" ID="RequiredFieldValidator27" runat="server" ErrorMessage="Date of Birth Required" ControlToValidate="txtUSERREGdob" ValidationGroup="Reg"></asp:RequiredFieldValidator>
                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtUSERREGdob" Format="MMM/dd/yyyy" Enabled="True"></cc1:CalendarExtender>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Assign Shop Location: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtREGASL" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            Address: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtaddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <label class="col-md-12 control-label">
                                                                            User Type: <span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:DropDownList ID="drpregusertype" runat="server" CssClass="form-control input-medium">
                                                                                <asp:ListItem Text="Admin" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="Manager" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Salesman" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="Cheff" Value="4"></asp:ListItem>
                                                                                <asp:ListItem Text="Driver" Value="5"></asp:ListItem>
                                                                                <asp:ListItem Text="Spa Employee" Value="6"></asp:ListItem>
                                                                                <asp:ListItem Text="Block" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group last">
                                                                    <div class="col-md-12 col-sm-push-1">
                                                                        <asp:Button ID="btnRegFinal" runat="server" Text="Finish" CssClass="btn btn-sm purple-soft" OnClick="btnRegFinal_Click" ValidationGroup="Reg" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="modal fade bs-modal-sm" id="small2" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog modal-sm" style="margin-bottom: 0px; margin-top: 0px;">
                                                        <div class="modal-content">
                                                            <div class="portlet box green">
                                                                <div class="modal-header" style="padding-top: 10px; padding-bottom: 10px;">
                                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                                                    <h4 class="modal-title" style="color: white;"><i class="fa fa-save"></i>&nbsp;Success</h4>
                                                                </div>
                                                            </div>
                                                            <div class="modal-body">
                                                                <p id="lblmsgpop2" style="text-align: center; font-family: 'Courier New';">hi</p>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button type="button" class="btn default" data-dismiss="modal">Close</button>
                                                            </div>
                                                        </div>
                                                        <!-- /.modal-content -->
                                                    </div>
                                                    <!-- /.modal-dialog -->
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- END PAGE CONTENT-->
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../assets/global/plugins/select2/select2.min.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/datatables/media/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-maxlength/bootstrap-maxlength.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-touchspin/bootstrap.touchspin.js" type="text/javascript"></script>
    <script type="text/javascript" src="../assets/global/plugins/fancybox/source/jquery.fancybox.pack.js"></script>
    <script src="../assets/global/plugins/plupload/js/plupload.full.min.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/metronic.js" type="text/javascript"></script>
    <script src="../assets/admin/layout4/scripts/layout.js" type="text/javascript"></script>
    <script src="../assets/admin/layout4/scripts/demo.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/datatable.js"></script>
    <script src="../assets/admin/pages/scripts/ecommerce-products-edit.js"></script>
    <script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Demo.init(); // init demo features
            EcommerceProductsEdit.init();
        });
    </script>

</body>
</html>
