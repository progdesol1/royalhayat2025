<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="Web.ACM.CreateAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html lang="en">

<head>
    <meta charset="utf-8" />
    <title>DIGITAL ERP</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />

    <link href="../assets/admin/pages/css/login.css" rel="stylesheet" type="text/css" />

    <link href="../assets/global/css/components-rounded.css" id="style_components" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout/css/layout.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout/css/themes/default.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/admin/layout/css/custom.css" rel="stylesheet" type="text/css" />

    <link rel="icon" href="../assets/favicon.ico" type="image/x-icon" />


</head>

<body class="login">

    <div class="menu-toggler sidebar-toggler">
    </div>

    <div class="content" style="margin-top: 20px;">

        <form id="form1" runat="server">
            <asp:ScriptManager ID="toolscriptmanagerID" runat="server">
            </asp:ScriptManager>
            <div class="logo" style="margin-top: 0px;">
                <a href="http://digital53.com/">

                    <img src="../assets/delogo.png" alt="Logo" style="width: 15%;" />
                </a>
            </div>
            <asp:Panel class="alert alert-success " ID="pnlErrorMsg" Visible="false" runat="server">
                <button data-close="alert" class="close"></button>
                <asp:Label ID="lblerrmsg" runat="server" Text=""></asp:Label>

            </asp:Panel>
            <asp:Panel class="alert alert-danger " ID="pnlMSG" Visible="false" runat="server">
                <button data-close="alert" class="close"></button>
                <asp:Label ID="lblMSG" runat="server" Text=""></asp:Label>

            </asp:Panel>
            <p class="hint">
                Enter your Company details below:
            </p>
            <div class="form-group">
                <label class="control-label visible-ie8 visible-ie9">Company Name</label>
                <asp:TextBox ID="txtcompany" runat="server" CssClass="form-control placeholder-no-fix" placeholder="Company"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCompanyName" runat="server" ForeColor="Red" ControlToValidate="txtcompany" ErrorMessage="Company Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label class="control-label visible-ie8 visible-ie9">User Name</label>
                <asp:TextBox ID="txtusername" runat="server" CssClass="form-control placeholder-no-fix" placeholder="User Name"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server" ForeColor="Red" ControlToValidate="txtusername" ErrorMessage="User Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label class="control-label visible-ie8 visible-ie9">Packge</label>
                <asp:DropDownList ID="drpPackage" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorPackage" ForeColor="Red" runat="server" ErrorMessage="Package Required." ControlToValidate="drpPackage" ValidationGroup="s" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label class="control-label visible-ie8 visible-ie9">Number Of User</label>
                <asp:TextBox ID="txtnumuser" runat="server" CssClass="form-control placeholder-no-fix" placeholder="Number Of User"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ControlToValidate="txtnumuser" ErrorMessage="Number Of User Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderNumberofUser" ValidChars="1234567890" TargetControlID="txtnumuser" FilterType="Custom, numbers" runat="server" />
            </div>
            <div class="form-group">
                <label class="control-label visible-ie8 visible-ie9">Mobile</label>
                <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control placeholder-no-fix" placeholder="Mobile"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ControlToValidate="txtmobile" ErrorMessage="Mobile Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderMobileNo" ValidChars="1234567890" TargetControlID="txtmobile" FilterType="Custom, numbers" runat="server" />
            </div>
            <div class="form-group">
                <label class="control-label visible-ie8 visible-ie9">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control placeholder-no-fix" placeholder="Your Email Id"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator3" ForeColor="Red" runat="server" ErrorMessage="Email Required." ControlToValidate="txtEmail" ValidationGroup="s"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail" ForeColor="red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" ErrorMessage="Email Is Not Valid"></asp:RegularExpressionValidator>
            </div>


            <div class="form-group">
                <label class="control-label visible-ie8 visible-ie9">Country</label>
                <asp:DropDownList ID="DrpContry" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorCountry" ForeColor="Red" runat="server" ErrorMessage="Country Required." ControlToValidate="DrpContry" ValidationGroup="s" InitialValue="0"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group margin-top-20 margin-bottom-20">
                <label class="check">
                    <input type="checkbox" name="tnc" />
                    I agree to the <a href="javascript:;">Terms of Service </a>
                    & <a href="javascript:;">Privacy Policy </a>
                </label>
                <div id="register_tnc_error">
                </div>
            </div>
            <div class="form-actions">
                <a href="Login.aspx">
                    <button type="button" id="register-back-btn" class="btn btn-default">Back</button>
                </a>
                <asp:Button ID="btnSubmit" CssClass="btn btn-success uppercase pull-right" runat="server" Text="Submit" ValidationGroup="s" OnClick="btnSubmit_Click" />
            </div>
        </form>

    </div>
    <div class="copyright">
        2014 © Saas.
    </div>

    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>

    <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>

    <script src="../assets/global/scripts/metronic.js" type="text/javascript"></script>
    <script src="../assets/admin/layout/scripts/layout.js" type="text/javascript"></script>
    <script src="../assets/admin/layout/scripts/demo.js" type="text/javascript"></script>
    <script src="../assets/admin/pages/scripts/login.js" type="text/javascript"></script>

    <script>
        jQuery(document).ready(function () {
            Metronic.init();
            Layout.init();
            Login.init();
            Demo.init();
        });
    </script>

</body>

</html>

