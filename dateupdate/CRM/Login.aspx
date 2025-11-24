<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.CRM.Login" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html lang="en">
<!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
<meta charset="utf-8"/>
<title>DIGITAL ERP</title>
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<meta http-equiv="Content-type" content="text/html; charset=utf-8">
<meta content="" name="description"/>
<meta content="" name="author"/>
<!-- BEGIN GLOBAL MANDATORY STYLES -->
<%--<link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css"/>--%>
<link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css"/>
<!-- END GLOBAL MANDATORY STYLES -->
<!-- BEGIN PAGE LEVEL STYLES -->
<link href="../assets/admin/pages/css/login.css" rel="stylesheet" type="text/css"/>
<!-- END PAGE LEVEL SCRIPTS -->
<!-- BEGIN THEME STYLES -->
<link href="../assets/global/css/components-rounded.css" id="style_components" rel="stylesheet" type="text/css"/>
<link href="../assets/global/css/plugins.css" rel="stylesheet" type="text/css"/>
<link href="../assets/admin/layout/css/layout.css" rel="stylesheet" type="text/css"/>
<link href="../assets/admin/layout/css/themes/default.css" rel="stylesheet" type="text/css" id="style_color"/>
<link href="../assets/admin/layout/css/custom.css" rel="stylesheet" type="text/css"/>
<!-- END THEME STYLES -->
<link rel="shortcut icon" href="favicon.ico"/>
     <script type="text/javascript">
         function ClearAllText() {
             document.getElementById('txtUser').value = "";
             document.getElementById('txtPass').value = "";
         }
    </script>

</head>
<!-- END HEAD -->
<!-- BEGIN BODY -->
<body class="login">
<!-- BEGIN SIDEBAR TOGGLER BUTTON -->
<div class="menu-toggler sidebar-toggler">
</div>
    
<!-- END SIDEBAR TOGGLER BUTTON -->
<!-- BEGIN LOGO -->

<!-- END LOGO -->
<!-- BEGIN LOGIN -->
<div class="content" style="margin-top: 20px;">

	<!-- BEGIN REGISTRATION FORM -->
    <form id="form1" runat="server">
         <asp:ScriptManager ID="toollogingpage" runat="server"></asp:ScriptManager>
       <div class="logo" style="margin-top: 0px;">
	<a href="index.aspx">
	<img src="../assets/admin/layout4/img/logo360.jpg" style="height: 51px;" alt=""/>
	</a>
</div>
        <asp:Panel class="alert alert-danger " ID="pnlErrorMsg" Visible="False" runat="server" meta:resourcekey="pnlErrorMsgResource1">
            <button data-close="alert" class="close"></button>
            <asp:Label ID="lblerrmsg" runat="server" Text="Label" meta:resourcekey="lblerrmsgResource1"></asp:Label>

        </asp:Panel>
        
        <p class="hint">Login Id:</p>
        <div class="form-group">
             <asp:Label class="control-label visible-ie8 visible-ie9" ID="lblLoginID" runat="server" Text="Login Id" meta:resourcekey="lblLoginIDResource1"></asp:Label>
       
            <asp:TextBox ID="txtUser" runat="server" Text="Haresh" onchange="securityCheck(this)"
                         autocomplete="off" class="form-control placeholder-no-fix" placeholder="Login Id" meta:resourcekey="txtUserResource1"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidatoropportunity_type" ForeColor="Red" runat="server" ControlToValidate="txtUser" ErrorMessage="Login Id Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
            <!--<input class="form-control placeholder-no-fix" type="text" placeholder="Full Name" name="fullname" />-->
        </div>
        <p class="hint">Password :</p>
        <div class="form-group">
         <asp:Label class="control-label visible-ie8 visible-ie9" ID="lblPassword" runat="server" Text="Password" meta:resourcekey="lblPasswordResource1"></asp:Label>
            
            <asp:TextBox autocomplete="off" ID="txtPass" runat="server"  Text="Haresh@1" class="form-control placeholder-no-fix" placeholder="Password" meta:resourcekey="txtPassResource1"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ControlToValidate="txtPass" ErrorMessage="Password Required." CssClass="Validation"  ValidationGroup="s"></asp:RequiredFieldValidator>
            <!--<input class="form-control placeholder-no-fix" type="text" placeholder="Email" name="email" />-->
        </div>
        <p class="hint">Tenant Id :</p>
        <div class="form-group">
           <asp:Label class="control-label visible-ie8 visible-ie9" ID="lblTenantId" runat="server" Text="Tenant Id" meta:resourcekey="lblTenantIdResource1"></asp:Label>
            <asp:TextBox autocomplete="off" ID="txtTenantId" runat="server" Text="362" class="form-control placeholder-no-fix" meta:resourcekey="txtTenantIdResource1" ></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ControlToValidate="txtTenantId" ErrorMessage="Tenant Id Required." CssClass="Validation"  ValidationGroup="s"></asp:RequiredFieldValidator>
            <cc1:FilteredTextBoxExtender ValidChars="." ID="FilteredTextBoxExtenderAmount" TargetControlID="txtTenantId" FilterType="Custom, numbers" runat="server" />
           
        </div>
       
        
       
       
        <div class="form-actions">
             <asp:Button ID="btnLogin" runat="server" class="btn btn-success uppercase" Text="Login"
                                    OnClientClick="return showWarningToast();" OnClick="btnLogin_Click" ValidationGroup="s" meta:resourcekey="btnLoginResource1" />
                                
            
            <button type="button" id="Reset1" class="btn btn-default pull-right" onclick="ClearAllText()">Exit</button>
            
        </div>
    </form>
	<!-- END REGISTRATION FORM -->
</div>
<div class="copyright">
    <asp:Label ID="lblcopyright" runat="server" Text="2014 © Saas." meta:resourcekey="lblcopyrightResource1"></asp:Label> 
</div>

<script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
<script src="../assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
<script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
<script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
<script src="../assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
<script src="../assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script>
<!-- END CORE PLUGINS -->
<!-- BEGIN PAGE LEVEL PLUGINS -->
<script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
<!-- END PAGE LEVEL PLUGINS -->
<!-- BEGIN PAGE LEVEL SCRIPTS -->
<script src="../assets/global/scripts/metronic.js" type="text/javascript"></script>
<script src="../assets/admin/layout/scripts/layout.js" type="text/javascript"></script>
<script src="../assets/admin/layout/scripts/demo.js" type="text/javascript"></script>
<script src="../assets/admin/pages/scripts/login.js" type="text/javascript"></script>
<!-- END PAGE LEVEL SCRIPTS -->
<script>
    jQuery(document).ready(function () {
        Metronic.init(); // init metronic core components
        Layout.init(); // init current layout
        Login.init();
        Demo.init();
    });
</script>
<!-- END JAVASCRIPTS -->
</body>
<!-- END BODY -->
</html>
