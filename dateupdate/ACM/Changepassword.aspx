<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Changepassword.aspx.cs" Inherits="Web.ACM.Changepassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Metronic | Dashboard
    </title>
    <meta name="description" content="Latest updates and statistic charts">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <!--begin::Web font -->
    <script src="https://ajax.googleapis.com/ajax/libs/webfont/1.6.16/webfont.js"></script>
    <script>
        WebFont.load({
            google: { "families": ["Poppins:300,400,500,600,700", "Roboto:300,400,500,600,700"] },
            active: function () {
                sessionStorage.fonts = true;
            }
        });
    </script>
    <!--end::Web font -->
    <!--begin::Base Styles -->
    <link href="../assetsP/vendors/base/vendors.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../assetsP/demo/demo2/base/style.bundle.css" rel="stylesheet" type="text/css" />
    <!--end::Base Styles -->
    <link rel="shortcut icon" href="../assetsP/demo/demo2/media/img/logo/favicon.ico" />

    <script type="text/javascript">
        // <![CDATA[
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // this is the function I'm trying to replace:
        function loadIframe(iframeName, url) {
            if (window.frames[iframeName]) {
                window.frames[iframeName].location = url;
                return false;
            }
            return true;
        }


        // ]]>
    </script>
    <script type="text/javascript">
        function setIframeHeight(iframe) {
            if (iframe) {
                var iframeWin = iframe.contentWindow || iframe.contentDocument.parentWindow;
                if (iframeWin.document.body) {
                    iframe.height = 0 + iframeWin.document.documentElement.scrollHeight || iframeWin.document.body.scrollHeight;

                }
            }
        };
    </script>
    <style>
        .demo-preview__frame {
            border: none;
            -webkit-box-flex: 1;
            -ms-flex: 1;
            flex: 1;
            position: relative;
            z-index: 1;
            width: 100%;
            height: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <a class="btn default" data-toggle="modal" href="#responsive" style="margin-left: 639px;"> Change Password</a>
     
    <div id="responsive" class="modal fade" tabindex="-1" aria-hidden="true">
								<div class="modal-dialog">
									<div class="modal-content">
										<div class="modal-header">
                                              
											<button type="button" class="close" style="margin-left: 639px;" data-dismiss="modal" aria-hidden="true"></button>
											
										</div>
										<div class="modal-body">
											<div class="scroller" style="height:300px" data-always-visible="1" data-rail-visible1="1">
												<div class="row">
                                                     <h4 align ="center" style="margin-left: 140px;">You're Almost Done</h4>
                                                  
													<div class="col-md-3"></div>
                                                    <div class="col-md-6" style="margin-left: 122px;">
													  <br />
                                                    <br />
														<p align ="center">
															<input type="text" class="col-md-12 form-control" placeholder="Set new Password" required>
														</p>
														<p align ="center">
															<input type="text" class="col-md-12 form-control" placeholder="Confirm Password" required >
														</p>
                                                        <asp:Button ID="btnchange" runat="server" Text="Done"  style="margin-left: 44px;" CssClass="btn btn-danger"/>
                                                   <br />
                                                        <br />
                                                       
                                                         </div>
                                                    <div class="col-md-3"></div>
                                                   
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
    </div>
    </form>
      <!--begin::Base Scripts -->
    <script src="../assetsP/vendors/base/vendors.bundle.js" type="text/javascript"></script>
    <script src="../assetsP/demo/demo2/base/scripts.bundle.js" type="text/javascript"></script>
    <!--end::Base Scripts -->
    <!--begin::Page Snippets -->
    <script src="../assetsP/app/js/dashboard.js" type="text/javascript"></script>
    <!--end::Page Snippets -->
</body>
</html>
