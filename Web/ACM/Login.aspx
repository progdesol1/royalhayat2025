<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.ACM.Login" %>
<!DOCTYPE html>

<html lang="en">
<!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
    <meta charset="utf-8" />
    <title>DIGITAL ERP</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="Content-type" content="text/html; charset=utf-8">
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <%--<link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css"/>--%>
    <link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link href="../assets/admin/pages/css/login.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- BEGIN THEME STYLES -->
    <link href="../assets/global/css/components-rounded.css" id="style_components" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout/css/layout.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout/css/themes/default.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/admin/layout/css/custom.css" rel="stylesheet" type="text/css" />
    <!-- END THEME STYLES -->

   <%-- <link rel="icon" href="../assets/delogo.png" type="image/x-icon"/>--%>
     <link rel="icon" type="image/png" href="/favicon.ico" />
    <script type="text/javascript">
        function ClearAllText() {
            document.getElementById('txtUser').value = "";
            document.getElementById('txtPass').value = "";
        }

    </script>
  <script type="text/javascript">
	  // disable back *******************************************
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", -1);
        window.onunload = function () { null };
        //*********************************************************
		
		
        //JS right click and ctrl+(n,a,j) disable
        function disableCtrlKeyCombination(e) {
            //list all CTRL + key combinations you want to disable
            var forbiddenKeys = new Array('a', 'n', 'j');

            var key;
            var isCtrl;

            if (window.event) {
                key = window.event.keyCode;     //IE
                if (window.event.ctrlKey)
                    isCtrl = true;
                else
                    isCtrl = false;
            }
            else {
                key = e.which;     //firefox

                if (e.ctrlKey)
                    isCtrl = true;
                else
                    isCtrl = false;
            }
            //Disabling F5 key
            if (key == 116) {
                alert('Key  F5 has been disabled.');
                return false;
            }
            //if ctrl is pressed check if other key is in forbidenKeys array
            if (isCtrl) {

                for (i = 0; i < forbiddenKeys.length; i++) {
                    //  alert(String.fromCharCode(key));
                    //case-insensitive comparation
                    if (forbiddenKeys[i].toLowerCase() == String.fromCharCode(key).toLowerCase()) {
                        alert('Key combination CTRL + '
                            + String.fromCharCode(key)
                            + ' has been disabled.');
                        return false;
                    }
                    if (key == 116) {
                        alert('Key combination CTRL + F5 has been disabled.');
                        return false;
                    }
                }
            }
            return true;
        }

        //Disable right mouse click Script

        var message = "Right click Disabled!";

        ///////////////////////////////////
        function clickIE4() {
            if (event.button == 2) {
                alert(message);
                return false;
            }
        }

        function clickNS4(e) {
            if (document.layers || document.getElementById && !document.all) {
                if (e.which == 2 || e.which == 3) {
                    alert(message);
                    return false;
                }
            }
        }

        if (document.layers) {
            document.captureEvents(Event.MOUSEDOWN);
            document.onmousedown = clickNS4;
        }

        else if (document.all && !document.getElementById) {
            document.onmousedown = clickIE4;
        }

        document.oncontextmenu = new Function("alert(message);return false");
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
            <asp:ScriptManager ID="toolscriptmanagerID" runat="server">
            </asp:ScriptManager>
            <div class="logo" style="margin-top: 0px;">
                <a href="#">
                    <img src="../assets/Untitled.png" style="HEIGHT: 167PX;margin-left: -45px;width: 400px;margin-top: -24px;"/>
                </a>
            </div>
            
                    <asp:Panel class="alert alert-danger " ID="pnlErrorMsg" Visible="false" runat="server">
                        <button data-close="alert" class="close"></button>
                        <asp:Label ID="lblerrmsg" runat="server" Text="Label"></asp:Label>

                    </asp:Panel>

                    <p class="hint">Login Id:</p>
                    <div class="form-group">
                        <label class="control-label visible-ie8 visible-ie9">Login Id</label>
                        <asp:TextBox ID="txtUser" runat="server" onchange="securityCheck(this)"
                            autocomplete="off" class="form-control placeholder-no-fix" placeholder="Login Id"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUser" ErrorMessage="Login Id  Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="Red"></asp:RequiredFieldValidator>
                        <!--<input class="form-control placeholder-no-fix" type="text" placeholder="Full Name" name="fullname" />-->
                    </div>
                    <p class="hint">Password :</p>
                    <div class="form-group">

                        <label class="control-label visible-ie8 visible-ie9">Password</label>
                        <asp:TextBox autocomplete="off" ID="txtPass" runat="server" TextMode="Password" placeholder="Password" class="form-control placeholder-no-fix"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPass" ErrorMessage="Password Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="Red"></asp:RequiredFieldValidator><!--<input class="form-control placeholder-no-fix" type="text" placeholder="Email" name="email" />-->
                    </div>
                  
                    <asp:Panel ID="pnltentid" runat="server" Visible="false">
                        <p class="hint">Select Tenet :</p>
                        <div class="form-group">
                            <label class="control-label visible-ie8 visible-ie9">Select Tenet</label>
                            <asp:DropDownList ID="ddltenet" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </asp:Panel>
                       <%-- <asp:DropDownList ID="DDLLanguage" runat="server" CssClass="form-control" Visible="false">
                        </asp:DropDownList>--%>
                    
                    <div class="form-actions">
                        <asp:Button ID="btnLogin" runat="server" class="btn btn-success uppercase" Text="Login" ValidationGroup="submititems" OnClick="btnLogin_Click" />
                        <button type="button" id="Reset1" class="btn btn-default pull-right" onclick="ClearAllText()">Exit</button>
                        
                    </div>
           
                      <p class="hint">Terminal Id :</p>
                    <div class="form-group">
                        <label class="control-label visible-ie8 visible-ie9">TERMINAL ID</label>
                        <asp:DropDownList ID="drpterminal" runat="server"   CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpterminal_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                       
                    <div class="form-actions">
                        <asp:Button ID="Button1" runat="server" class="btn btn-success uppercase" Text="Feedback" OnClick="Button1_Click" Visible ="false" />
                           
                    </div>
            
                  <%-- <div style="text-align:center;" >
                       <a href="javascript:;" id="forget-password" >Forgot Password?</a>
                   </div>
                         
                    
                     <div class="create-account">
                        <p>
                            <a href="CreateAccount.aspx" id="Account" class="uppercase">Create an account</a>
                        </p>
                         <p>
                             <a href="WinPOS_Registration.aspx" class="uppercase">POS Registration</a>
                        </p>
                    </div>--%>
               
        </form>
        <!-- END REGISTRATION FORM -->
    </div>
    <div class="copyright">
        <%--2014 © Saas.--%>
        2019 © Royal Hayat Hospital
    </div>

    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <%--<script src="../assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script> yogesh 19042017--%>
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
