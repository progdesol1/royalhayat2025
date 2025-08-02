<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Event_Management_MST.aspx.cs" Inherits="Web.CRM.Event_Management_MST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/CRM/UserControl/Custmer.ascx" TagPrefix="uc1" TagName="Custmer" %>
<%@ Register Src="~/CRM/UserControl/EventUC.ascx" TagPrefix="uc1" TagName="EventUC" %>



<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../assets/global/plugins/bootstrap-datepicker/css/datepicker.css" />
    <link href="../assets/admin/pages/css/search.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-modal/css/bootstrap-modal-bs3patch.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-modal/css/bootstrap-modal.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/components-rounded.css" id="style_components" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout4/css/layout.css" rel="stylesheet" type="text/css" />
    <link id="style_color" href="../assets/admin/layout4/css/themes/light.css" rel="stylesheet" type="text/css" />
    <link href="../assets/admin/layout4/css/custom.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="favicon.ico" />
    <link href="../assets/global/plugins/select2/select2.css" rel="stylesheet" />
    <script src="../assets/toast/jquery.js"></script>
    <script src="../assets/toast/script.js"></script>
    <script src="../assets/toast/toastr.min.js"></script>
    <link href="../assets/toast/toastr.min.css" rel="stylesheet" />
    <style>
        .todo-tasklist-item {
            background: #f6fbfc none repeat scroll 0 0;
            margin-bottom: 15px;
            overflow: hidden;
            padding: 10px;
        }

        .todo-tasklist-item-border-green {
            border-left: 2px solid #3faba4;
        }

        .todo-userpic {
            border: 1px solid #cedae1;
            border-radius: 50%;
        }

        .pull-left {
            float: left;
        }

        .todo-tasklist-item-title {
            color: #2b4a5c;
            font-size: 15px;
            font-weight: 600;
            padding-bottom: 13px;
            padding-top: 3px;
        }

        .todo-tasklist-item-text {
            color: #577688;
            font-size: 13px;
            padding-bottom: 5px;
        }

        .todo-tasklist-controls {
            margin-top: 5px;
        }

        .todo-tasklist-date {
            color: #637b89;
            margin-right: 12px;
        }

            .todo-tasklist-date i {
                color: #abbfca;
                margin-right: 5px;
            }

        }

        .badge.badge-roundless {
            border-radius: 0;
        }

        .todo-tasklist-badge {
            background-color: #b3bfcb;
        }

        .badge {
            border-radius: 12px;
            font-size: 11px;
            font-weight: 300;
            height: 18px;
            padding: 3px 6px;
            text-align: center;
            text-shadow: none;
            vertical-align: middle;
        }

        .My-control {
            background-color: red;
            border: 15px solid #00E21B;
            border-radius: 4px;
            box-shadow: none;
            color: #333;
            font-size: 14px;
            font-weight: normal;
            transition: border-color 0.15s ease-in-out 0s, box-shadow 0.15s ease-in-out 0s;
        }
    </style>

    <style type="text/css">
        .bgimg {
            background-image: url('images/beautifully-certificate.jpg');
        }
    </style>
    <style type="text/css">
        .MainImg {
            background-image: url('images/mainbg.png');
        }
    </style>
    <script type="text/javascript">
        function ace_itemCoutry(sender, e) {
            var HiddenField3 = $get('<%= HiddenField3.ClientID %>');
            HiddenField3.value = e.get_value();
        }
    </script>
    <script type="text/javascript">
        function printdiv(printpage) {
            var headstr = "<html><head></head><body>";
            var footstr = "</body>";
            var newstr = document.all.item(printpage).innerHTML;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = headstr + newstr + footstr;
            window.print();
            document.body.innerHTML = oldstr;
            return false;
        }
    </script>

</head>

<body class="" runat="server">
    <%--     <script type="text/javascript">
         function printEventPNL() {
             var panel = document.getElementById("<%= %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>--%>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="toolscriptmanagerID" runat="server" AsyncPostBackTimeout="360000"></asp:ScriptManager>
        <div class="page-container" style="margin-top: 0px;">

            <div class="page-content-wrapper MainImg">

                <div class="page-content" style="margin-left: 0px; background-image: url('CRM/images/IMGEVENT.jpg');">

                    <div class="modal fade" id="portlet-config" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                    <h4 class="modal-title">Modal title</h4>
                                </div>
                                <div class="modal-body">
                                    Widget settings form goes here
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn blue">Save changes</button>
                                    <button type="button" class="btn default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-head">
                        <div class="page-title">
                            <h1 style="color: #71fe34;">
                                <asp:Label ID="lblhead" runat="server" Text=""></asp:Label>
                                <small style="color: #6FE6E1;"></small></h1>
                        </div>
                    </div>
                    <%-- <ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.html">Home</a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">Extra</a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">Search Results</a>
                    </li>
                </ul>       --%>
                    <div class="row">
                        <div class="col-md-12">


                            <div id="tab_1_5" class="tab-pane">
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSerrch">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <%--alert-danger --%>
                                            <div id="back123" class="My-control" style="background-color: #00E21B;">
                                                <div class="input-group">
                                                    <div class="input-cont">
                                                        <%--<input type="text" placeholder="Search..." class="form-control" />--%>
                                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="You Want To Serach by Name, Mobile, Bussiness Phone, Email & Address"></asp:TextBox>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtSearch" ServiceMethod="GetCounrty" CompletionInterval="1000" EnableCaching="FALSE" CompletionSetCount="20"
                                                            MinimumPrefixLength="1" OnClientItemSelected="ace_itemCoutry" DelimiterCharacters=";, :" FirstRowSelected="false"
                                                            runat="server" />
                                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                                    </div>
                                                    <div class="input-group-btn">
                                                        <%--<button type="button" class="btn green-haze">
                                                        Search &nbsp; <i class="m-icon-swapright m-icon-white"></i>
                                                    </button>--%>
                                                        <asp:Button ID="btnSerrch" runat="server" Text="Search" CssClass="btn default" OnClick="btnSerrch_Click" />
                                                    </div>
                                                    &nbsp;
                                                    <div class="input-group-btn" id="uc1" runat="server">
                                                        <%--<button type="button" class="btn green-haze" data-toggle="modal" href="#responsive">
                                                            New Joining &nbsp; <i class="m-icon-swapright m-icon-white"></i>
                                                        </button>--%>
                                                        <%--<uc1:Custmer runat="server" ID="Custmer" />--%>
                                                      
                                                        <uc1:EventUC runat="server" ID="EventUC" />
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="portlet light" style="background-color: #e6ffe6;">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <i class="icon-basket font-green-sharp"></i>
                                                    <span class="caption-subject font-green-sharp bold uppercase">Registration Listing</span>
                                                    <span class="caption-helper">Registration orders...</span>
                                                </div>
                                            </div>
                                            <div class="portlet-body">
                                                <div class="table-container">
                                                    <table class="table table-striped table-bordered table-hover" id="datatable_orders">
                                                        <thead>
                                                            <tr style="background-color: #00cc00; color: #fff;">
                                                                <th style="font-size: x-large"># </th>
                                                                <th style="font-size: x-large">PHOTO
                                                                </th>
                                                                <th style="font-size: x-large">NAME
                                                                </th>
                                                                <th style="font-size: x-large">COMPANY
                                                                </th>
                                                                <th style="font-size: x-large">DESIGNATION
                                                                </th>

                                                                <th style="width: 220px; font-size: x-large;">ACTION</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td style="height: 60px;">
                                                                            <asp:Label ID="Label1" Font-Size="XX-Large" runat="server" Text='<%# Container.DisplayIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td style="height: 60px;">
                                                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# "images/"+ Image(Convert.ToInt32(Eval("EventID")),Convert.ToInt32(Eval("MyID")),Convert.ToInt16(Eval("ContactMyID")))  %>' Width="40px" Height="40px" />
                                                                            <asp:Image ID="Imgbar" runat="server" Visible="false" ImageUrl='<%# "../Master/Img/"+ (Eval("BARCODE")) %>' />
                                                                        </td>
                                                                        <td style="height: 60px;">
                                                                            <%--<asp:Label ID="lblCusomer" Font-Size="XX-Large" runat="server" Text='<%# Eval("COMPNAME1") %>'></asp:Label>--%>
                                                                            <asp:Label ID="lblCusomer" Font-Size="XX-Large" runat="server" Text='<%# Eval("Attendee") %>'></asp:Label>
                                                                        </td>
                                                                        <td style="height: 60px;">
                                                                            <asp:Label ID="lblComm" Font-Size="XX-Large" runat="server" Text='<%#CompanyID(Convert.ToInt32(Eval("CompanyId"))) %>'></asp:Label>
                                                                        </td>
                                                                        <td style="height: 60px;"></td>

                                                                        <td style="height: 60px;">
                                                                            <asp:ImageButton ID="btnCard" runat="server" ImageUrl="~/CRM/images/card.jpg" Height="50px" Width="50px" />
                                                                            <asp:ImageButton ID="btnCerti" runat="server" ImageUrl="~/CRM/images/Certificate.png" Height="50px" Width="50px" />

                                                                            <%--<asp:LinkButton ID="btnCard" runat="server" CssClass="btn default btn-xs red-stripe">Card</asp:LinkButton>
                                                            <asp:LinkButton ID="btnCerti" runat="server" CssClass="btn default btn-xs blue-stripe">Certificate</asp:LinkButton>--%>

                                                                            <panel id="pnlresponsive1" style="padding: 1px; background-color: #fff; border: 1px solid #000; display: none" runat="server">

                                                             <div class="modal-dialog" style="position:fixed;left:30%;top:10%; width: 30%;">        
								                            	<div class="modal-content">                                                                                                                                                 
										                            <div class="modal-body" id=<%#"pnlCard" + Container.DataItemIndex + 1 %> >
										                            	<div class="scroller" style="height:250px" data-always-visible="1" data-rail-visible1="1">
											                            	<div class="row">
                                                                                <div class="todo-tasklist">
												                                    <div class="todo-tasklist-item todo-tasklist-item-border-green" style="width: 300px;">
													                                    <%--img class="todo-userpic pull-left" src="../assets/admin/layout/img/avatar4.jpg"" width="27px" height="27px">--%>
                                                                                        <asp:Image ID="IMGCusImg" runat="server" width="27px" height="27px" class="todo-userpic pull-left">   </asp:Image>
													                                    <div class="todo-tasklist-item-title">
                                                                                            <asp:Label ID="lblCusName" runat="server" Text="" style="margin-left: 10px;"></asp:Label>														                                    
													                                    </div>
													                                    <div class="todo-tasklist-item-text">
														                                     Wel Come To Kuwait Security Event 2018 .
													                                    </div>
													                                    <div class="todo-tasklist-item-text">
														                                     <%--<img src="../ECOMM/images/12.png" alt="Avatar" style="width:100%" />--%>
                                                                                            <asp:Image ID="IMGBarCode" runat="server" style="width:100%"></asp:Image>
													                                    </div>
                                                                                        <div class="todo-tasklist-item-text"><i class="fa fa-barcode"></i>
                                                                                            <asp:Label ID="lblBar" runat="server" Text='<%# Eval("BARCODE") %>'></asp:Label>
                                                                                        </div>
													                                    <div class="todo-tasklist-controls pull-left">
														                                    <span class="todo-tasklist-date"><i class="fa fa-calendar"></i> <asp:Label ID="lblbardate" runat="server" Text=""></asp:Label> </span>
														                                    <%--<span class="todo-tasklist-badge badge badge-roundless">Urgent</span>--%>
													                                    </div>
												                                    </div>
												
											                                    </div>
												                            </div>                                                 
											                            </div> 
										                            </div> 
                                                                    <div class="modal-footer">	
                                                                        <asp:LinkButton ID="btncancel" CssClass="btn red-haze" runat="server">Cancel</asp:LinkButton>
                                                                        <input name="b_print" type="button" class="btn blue" onClick="<%# "printdiv('pnlCard" + Container.DataItemIndex + 1 +"');" %>" value=" Print ">
                                                                        
										                            </div>  
								                                </div> 
                                                             </div>
      
                                                            </panel>
                                                                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
                                                                                BackgroundCssClass="modalBackground" CancelControlID="btncancel" Enabled="True"
                                                                                PopupControlID="pnlresponsive1" TargetControlID="btnCard">
                                                                            </cc1:ModalPopupExtender>



                                                                            <panel id="pnlCerty" style="padding: 1px; background-color: #fff; border: 1px solid #000; display: none" runat="server">
                                                             <div class="modal-dialog" style="position:fixed; left:30%; top:10%; width: 38%;">        
								                            	<div class="modal-content">                                                                                                                                                 
										                            <div class="modal-body" id=<%#"divPrintCertificate"+ Container.DataItemIndex + 1 %>>
										                            	<div class="scroller" style="height:30%" data-always-visible="1" data-rail-visible1="1">
											                            	<div class="row">
                                                                                <div class="bgimg" style="width: 423px; height: 320px;">
                                                                                     <%--<asp:Image ID="ImgCerty" ImageUrl="~/CRM/images/beautifully-certificate-template.jpg" runat="server" ></asp:Image>--%>                                                                                                                                                                                                                                                   
                                                                                     <center style="padding-top: 110px;"><b><asp:Label CssClass="center" ID="lblFamilyName" runat="server" Text="Family" ></asp:Label></b></center>
                                                                                </div>                                                                                   
												                            </div>                                                 
											                            </div> 
										                            </div>
                                                                    <div class="modal-footer">
                                                                        <asp:LinkButton ID="btnCancle123" CssClass="btn red-haze" runat="server">Cancel</asp:LinkButton>
                                                                        <input name="b_print" type="button" class="btn blue"   onClick="<%# "printdiv('divPrintCertificate" + Container.DataItemIndex + 1 +"');" %>" value=" Print ">
                                                                    </div> 
                                                                </div>
                                                             </div>
                                                             </panel>
                                                                            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" DynamicServicePath=""
                                                                                BackgroundCssClass="modalBackground" CancelControlID="btnCancle123" Enabled="True"
                                                                                PopupControlID="pnlCerty" TargetControlID="btnCerti">
                                                                            </cc1:ModalPopupExtender>
                                                                        </td>
                                                                    </tr>

                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                            <%--<tr>
                                                <td>
                                                    <img src="../assets/admin/pages/media/profile/avatar1.jpg" alt="" />
                                                </td>
                                                <td>Johar Mandav
                                                </td>
                                                <td>Digital
                                                </td>
                                                <td>IT Consultant
                                                </td>
                                               
                                                <td>
                                                   Print&nbsp; <a class="btn default btn-xs red-stripe" href="javascript:;">Card
                                                    </a>&nbsp;  <a class="btn default btn-xs blue-stripe" href="javascript:;">Certificate
                                                    </a>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    <img src="../assets/admin/pages/media/profile/avatar1.jpg" alt="" />
                                                </td>
                                                <td>Najam Mandav
                                                </td>
                                                <td>Digital
                                                </td>
                                                <td>IT Consultant
                                                </td>
                                               
                                                <td>
                                                   Print&nbsp; <a class="btn default btn-xs red-stripe" href="javascript:;">Card
                                                    </a>&nbsp;  <a class="btn default btn-xs blue-stripe" href="javascript:;">Certificate
                                                    </a>
                                                </td>
                                            </tr>--%>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                            </div>

                            <%--</div>--%>
                            <%-- </div>--%>
                        </div>

                    </div>
                    <div class="page-head" id="uc2" runat="server">
                        <div class="page-title">
                            <h1 style="color: #F5CD2D">Note :  <small style="color: #F5CD2D">Your name not found above please </small>
                                <uc1:EventUC runat="server" ID="EventUC1" />
                            </h1>
                        </div>
                    </div>

                </div>

            </div>
        </div>

        <%-- <div id="responsive" class="modal fade" tabindex="-1" data-width="760">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                <h4 class="modal-title"><strong>Add Customer</strong></h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <h4>Customer Name * </h4>
                        <p>                           
                            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control"></asp:TextBox>
                        </p>
                        <h4>Address * </h4>
                        <p>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                        </p>

                    </div>
                    <div class="col-md-6">
                        <h4>Mobile NO * </h4>
                        <p>
                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                        </p>
                        <h4>Email </h4>
                        <p>
                            <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
                        </p>
                        <h4>Bus Phone </h4>
                        <p>
                            <asp:TextBox ID="txtbusphone" runat="server" CssClass="form-control"></asp:TextBox>
                        </p>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" data-dismiss="modal" class="btn btn-default">Close</button>
                <button type="button" class="btn blue">Save changes</button>
            </div>
        </div>--%>
    </form>
    <div class="page-footer">
        <div class="page-footer-inner">
            2019 &copy; Royal Hayat Hospital.
        </div>
        <div class="scroll-to-top">
            <i class="icon-arrow-up"></i>
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
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-modal/js/bootstrap-modalmanager.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-modal/js/bootstrap-modal.js" type="text/javascript"></script>
    <script type="text/javascript" src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../assets/global/plugins/fancybox/source/jquery.fancybox.pack.js"></script>
    <script src="../assets/global/scripts/metronic.js" type="text/javascript"></script>
    <script src="../assets/admin/layout4/scripts/layout.js" type="text/javascript"></script>
    <script src="../assets/admin/layout4/scripts/demo.js" type="text/javascript"></script>
    <script src="../assets/admin/pages/scripts/search.js"></script>
    <script src="../assets/admin/pages/scripts/ui-extended-modals.js"></script>
    <script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Demo.init(); // init demo features
            Search.init();
            UIExtendedModals.init();
        });
    </script>

</body>
</html>
