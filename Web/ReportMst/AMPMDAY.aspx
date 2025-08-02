<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AMPMDAY.aspx.cs" Inherits="Web.ReportMst.AMPMDAY" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="Preview page of Metronic Admin Theme #4 for buttons extension demos" name="description" />
    <meta content="" name="author" />
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&amp;subset=all" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/layouts/layout4/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/layouts/layout4/css/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="assets/layouts/layout4/css/custom.min.css" rel="stylesheet" type="text/css" />


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
    <script type="text/javascript">
        function showProgress() {
            $.blockUI({ message: '<h1>please wait...</h1>' });
        }

        function stopProgress() {
            $.unblockUI();
        }

        $("#ContentPlaceHolder1_btnAdd").click(function () {
            $.blockUI({ message: '<h1>please wait...</h1>' });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="background-color: #ffffff">
            <table style="width: 100%; height: auto">
                <tr>
                    <td style="width: 10%;">
                        <%--<img src="assets/global/img/HealthBar.png" class="logo-default" style="margin-top: 10px; width: 250px;" />--%>
                        <asp:Image ID="HealtybarLogo" ImageUrl="assets/global/img/HealthBar.png" runat="server" />
                    </td>
                    <td style="width: 90%;">
                        <h2 style="text-align: center;">&nbsp;<asp:Label ID="lblHead" Style="margin-left: -100px;" Font-Bold="true" runat="server" Text="Kitchen Preparation Report"></asp:Label>
                        </h2>
                    </td>
                </tr>
            </table>
            <div class="page-container">
                <div class="page-content">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Panel ID="pnlWarningMsg" runat="server" Visible="false">
                                <div class="alert alert-warning alert-dismissable">
                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                    <asp:Label ID="lblWarningMsg" Font-Size="Large" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
                                </div>
                            </asp:Panel>
                            <div class="portlet-body" style="background-color: #ffffff">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group" style="color: ">
                                            <asp:Label runat="server" ID="lblActive1s" CssClass="col-md-4 control-label" Text="Delivery Date" Style="margin-top: 20px;"></asp:Label>
                                            <div class="col-md-8">
                                                <strong>
                                                    <asp:Label ID="lblFrom" runat="server" Text="From" Style="margin-left: 10px;"></asp:Label></strong>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtdateFrom" Placeholder="dd/MMM/YYYY" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                <cc1:CalendarExtender ID="TextBoxtxtdateFrom_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateFrom" Format="dd/MMM/yyyy"></cc1:CalendarExtender>
                                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtdateFrom" ValidChars="/" FilterType="Custom, numbers" runat="server" />--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group" style="color: ">

                                            <div class="col-md-12">
                                                <strong>
                                                    <asp:Label ID="lblTo" runat="server" Text="TO" Style="margin-left: 10px;"></asp:Label></strong>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtdateTO" Placeholder="dd/MMM/YYYY" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtendertxtdateTO" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateTO" Format="dd/MMM/yyyy"></cc1:CalendarExtender>
                                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtdateTO" ValidChars="/" FilterType="Custom, numbers" runat="server" />--%>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkcom" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="chkcom_CheckedChanged"/>

                                            </div>
                                            <asp:Label runat="server" ID="Label18" CssClass="control-label" Text="Combine One Report of the Day with Production"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group" style="color: ">
                                        <div class="col-md-4">

                                            <asp:Label runat="server" ID="Label9" CssClass="col-md-4 control-label" Text="Delivery Time" Style="margin-top: 10px;"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="DrpFromDelivery" CssClass="form-control select2me input-medium" Style="margin-top: 10px;" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <asp:Label ID="Label3" Visible="false" runat="server" Text=""></asp:Label>
                                        <div class="col-md-6">
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="DrpToDelivery" CssClass="form-control select2me input-medium" Style="margin-top: 10px;" runat="server"></asp:DropDownList>
                                            </div>
                                            <asp:Label ID="Label4" Visible="false" runat="server" Text=""></asp:Label>
                                            <div class="col-md-4">
                                                <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" Text="Search" Style="width: 222px; margin-top: 10px;" OnClick="btnAdd_Click"/>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group" style="color: ">

                                        <div class="col-md-8">
                                        </div>
                                    </div>
                                </div>
                                <%-- Seprate --%>
                                <asp:Panel ID="PNLseprate" runat="server">
                                    <div class="portlet-title">
                                        <div class="caption font-dark">
                                            <i class="icon-settings font-dark"></i>
                                            <span class="caption-subject bold uppercase">Kitchen Preparation Report Delivery Wise</span>
                                        </div>
                                        <div class="tools"></div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="table-scrollable">
                                            <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1" id="sample_1">
                                                <thead class="repHeader">
                                                    <tr>
                                                        <th></th>
                                                        <th>
                                                            <asp:CheckBox ID="ChkAll" runat="server" AutoPostBack="true" OnCheckedChanged="ChkAll_CheckedChanged"/>
                                                            <asp:LinkButton ID="lblAtion" CssClass="btn btn-sm green" runat="server" OnClick="lblAtion_Click">Print</asp:LinkButton>
                                                            <asp:Label runat="server" ID="lblSN"><br />SN#</asp:Label>
                                                        </th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="Label8" Text="Delivery Date"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="Label10" Text="Delivery Time"></asp:Label></th>
                                                        <%--<th>
                                                <asp:Label runat="server" Font-Size="Large" ID="Label19" Text="Meal"></asp:Label></th>--%>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="lblProduct" Text="Product"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="lblNormal" Text="Normal"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="lbl150grms" Text="100grms"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="lbl200grms" Text="150grms"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="lbl250grms" Text="200grms"></asp:Label></th>
                                                    </tr>

                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" OnItemDataBound="Listview1_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    <asp:CheckBox ID="CHKPrint" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpectedDelDate")).ToString("dd/MMM/yyyy") %>'></asp:Label>
                                                                    <asp:Label ID="lblMy" runat="server" Visible="false" Text='<%# Eval("MyID") %>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("Delivery") %>'></asp:Label></td>
                                                                <%--<td>
                                                        <asp:Label ID="Label18" runat="server" Text='<%# getMealType1(Convert.ToInt32(Eval("Meal"))) %>'></asp:Label>
                                                    </td>--%>
                                                                <td>
                                                                    <asp:LinkButton ID="lbkView" runat="server" CommandName="lbkView" CommandArgument='<%# Eval("MyID") %>'>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Product")+" - "+Eval("ProductName") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>

                                                                <td style="text-align: center;">
                                                                    <asp:LinkButton ID="lbkNormalQty" class="btn btn-danger btn-sm" Enabled="false" Style="width: 100%;" runat="server" CommandName="normal" CommandArgument='<%# Eval("MyID") %>'>
                                                                        <%--<span class="label label-danger label-sm">--%>
                                                                        <asp:Label ID="lblNormalQty" runat="server" Font-Size="Medium" Text='<%# Eval("NormalQty")%>'></asp:Label>                                                                        
                                                                        <asp:Label ID="lblProductionDate" runat="server" Visible="false" Text='<%# Eval("ProductionDate") %>'></asp:Label>
                                                                        <%--</span>--%>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:LinkButton ID="LinkbtnQty150" class="btn btn-danger btn-sm" Enabled="false" Style="width: 100%;" CommandName="btnQty150" runat="server" CommandArgument='<%# Eval("MyID") %>'>
                                                                        <asp:Label ID="lblQty150" runat="server" Font-Size="Medium" Text='<%# Eval("Qty150") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:LinkButton ID="LinkbtnQty200" class="btn btn-danger btn-sm" Enabled="false" Style="width: 100%;" CommandName="btnQty200" CommandArgument='<%# Eval("MyID") %>' runat="server">
                                                                        <asp:Label ID="lblQty200" runat="server" Font-Size="Medium" Text='<%# Eval("Qty200") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:LinkButton ID="LinkbtnQty250" class="btn btn-danger btn-sm" Enabled="false" Style="width: 100%;" CommandName="btnQty250" CommandArgument='<%# Eval("MyID") %>' runat="server">
                                                                        <asp:Label ID="lblQty250" runat="server" Font-Size="Medium" Text='<%# Eval("Qty250") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            <%-- t --%>
                                                            <panel id="pnlDriverCheckList" style="padding: 1px; background-color: #fff; border: 2px solid pink; display: none; overflow: auto;" runat="server" cssclass="modalPopup">
                                                                            <div class="modal-dialog" style="position:fixed;left:30%;top:20px">
                                                                                <div class="modal-content" style="width:400px;">
                                                                                    <div class="modal-header" style="background-color:#44B6AE;">										
											                                            <h4 class="modal-title"><i class="fa fa-file-text-o"></i> Kitchen Preparation Report</h4>
										                                            </div>
                                                                
                                                                                    <div class="modal-body">
											                                        <div class="scroller" style="height:300px;" data-always-visible="1" data-rail-visible1="1">
                                                                                           <asp:ListView ID="ListView3" runat="server">
                                                                                            <ItemTemplate>
                                                                                        <div class="row">
                                                                                             <div class="col-md-12">                                                                                                                                                                                                
                                                                                                      <div class="col-md-3">
                                                                                                            <asp:Label ID="Label5" Font-Bold="true" Font-Size="Medium" runat="server" Text="Driver"></asp:Label>
                                                                                                      </div>
                                                                                                  <div class="col-md-1">
                                                                                                            <asp:Label ID="Label14" Font-Bold="true" Font-Size="Medium" runat="server" Text=":-"></asp:Label>
                                                                                                      </div>
                                                                                                      <div class="col-md-6">
														                                                    <asp:Label ID="Driver" runat="server" Text='<%# DriverName(Convert.ToInt32(Eval("DriverID"))) %>'></asp:Label>
                                                                                                      </div> 
                                                                                               </div>            
														                                 </div>   
                                                                                        <div class="row">
                                                                                             <div class="col-md-12">                                                                                                                                                                                                
                                                                                                      <div class="col-md-3">
                                                                                                            <asp:Label ID="Label6" Font-Bold="true" Font-Size="Medium" runat="server" Text="Product"></asp:Label>
                                                                                                      </div>
                                                                                                        <div class="col-md-1">
                                                                                                            <asp:Label ID="Label7" Font-Bold="true" Font-Size="Medium" runat="server" Text=":-"></asp:Label>
                                                                                                      </div>
                                                                                                      <div class="col-md-6">
														                                                    <asp:Label ID="Product" runat="server" Text='<%# Eval("ProdName1") %>'></asp:Label>
                                                                                                      </div> 
                                                                                               </div>            
														                                 </div> 
                                                                                        <div class="row">
                                                                                             <div class="col-md-12">                                                                                                                                                                                                
                                                                                                      <div class="col-md-3">
                                                                                                            <asp:Label ID="Label13" Font-Bold="true" Font-Size="Medium" runat="server" Text="Customer"></asp:Label>
                                                                                                      </div>
                                                                                                  <div class="col-md-1">
                                                                                                            <asp:Label ID="Label16" Font-Bold="true" Font-Size="Medium" runat="server" Text=":-"></asp:Label>
                                                                                                      </div>
                                                                                                      <div class="col-md-6">
														                                                    <asp:Label ID="Customer" runat="server" Text='<%# CustomID(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label>
                                                                                                      </div> 
                                                                                               </div>            
														                                 </div> 
                                                                                        <div class="row">
                                                                                             <div class="col-md-12">                                                                                                                                                                                                
                                                                                                      <div class="col-md-3">
                                                                                                            <asp:Label ID="Label15" Font-Bold="true" Font-Size="Medium" runat="server" Text="QTY"></asp:Label>
                                                                                                      </div>
                                                                                                  <div class="col-md-1">
                                                                                                            <asp:Label ID="Label17" Font-Bold="true" Font-Size="Medium" runat="server" Text=":-"></asp:Label>
                                                                                                      </div>
                                                                                                      <div class="col-md-6">
														                                                    <asp:Label ID="QTY" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                                                                            <asp:Label ID="itemWeight" runat="server" Text='<%# "(Weight" +"-"+ Convert.ToInt32(Eval("ItemWeight")) +")" %>'></asp:Label>
                                                                                                            <asp:Label ID="Statusss" runat="server" Text='<%# GetSTD(Convert.ToDateTime(Eval("ProductionDate")).ToShortDateString()) %>'></asp:Label>
                                                                                                      </div> 
                                                                                               </div>            
														                                 </div>        
                                                                                                <hr />      
                                                                                                </ItemTemplate>                                                                                                                                                                       
                                                                                         </asp:ListView>    
                                                                                        
											                                        </div>
                                                                                    </div>
                                                                 
                                                                                    <div class="modal-footer">                                                                                         
                                                                                         <asp:LinkButton ID="btncancle" runat="server"  Text="Cancel" class="btn default"/>
                                                                                    </div>
                                                                  
                                                                           </div>
                                                                                </div>
                                                                        </panel>
                                                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
                                                                BackgroundCssClass="modalBackground" CancelControlID="btncancle" Enabled="True"
                                                                PopupControlID="pnlDriverCheckList" TargetControlID="lbkView">
                                                            </cc1:ModalPopupExtender>
                                                            <%-- t --%>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                            </table>

                                        </div>
                                    </div>
                                </asp:Panel>
                                <%-- Combine --%>
                                <asp:Panel ID="PNLcombine" Visible="false" runat="server">
                                    <div class="portlet-title">
                                        <div class="caption font-dark">
                                            <i class="icon-settings font-dark"></i>
                                            <span class="caption-subject bold uppercase">Kitchen Preparation Report Day Wise</span>
                                        </div>
                                        <div class="tools"></div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="table-scrollable">
                                            <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1" id="sample_2">
                                                <thead class="repHeader">
                                                    <tr>
                                                        <th></th>
                                                        <th>
                                                            <asp:CheckBox ID="Checkcombine" runat="server" AutoPostBack="true" OnCheckedChanged="Checkcombine_CheckedChanged"/>
                                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-sm green" runat="server" OnClick="lblAtion_Click">Print</asp:LinkButton>
                                                            <asp:Label runat="server" ID="Label19"><br />SN#</asp:Label>
                                                        </th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="Label20" Text="Delivery Date"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="Label22" Text="Product"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="Label23" Text="Normal"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="Label24" Text="100grms"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="Label25" Text="150grms"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" Font-Size="Large" ID="Label26" Text="200grms"></asp:Label></th>
                                                    </tr>

                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview2" runat="server" OnItemDataBound="Listview2_ItemDataBound" OnItemCommand="Listview2_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                    <asp:CheckBox ID="CHKPrint3" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label113" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpectedDelDate")).ToString("dd/MMM/yyyy") %>'></asp:Label>
                                                                    <asp:Label ID="lblMy3" runat="server" Visible="false" Text='<%# Eval("MyID") %>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lbkView3" runat="server" CommandName="lbkView3" CommandArgument='<%# Eval("MyID") %>'>
                                                                        <asp:Label ID="Label23" runat="server" Text='<%# Eval("Product")+" - "+Eval("ProductName") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>

                                                                <td style="text-align: center;">
                                                                    <asp:LinkButton ID="LinkNormalQty3" class="btn btn-danger btn-sm" Style="width: 100%;" runat="server" CommandName="normal" CommandArgument='<%# Eval("MyID") %>'>
                                                                        <asp:Label ID="lblNormalQty3" runat="server" Font-Size="Medium" Text='<%# Eval("NormalQty")%>'></asp:Label>
                                                                        <asp:Label ID="lblProductionDate3" runat="server" Visible="false" Text='<%# Eval("ProductionDate") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:LinkButton ID="LinkbtnQty1503" class="btn btn-danger btn-sm" Style="width: 100%;" CommandName="btnQty150" runat="server" CommandArgument='<%# Eval("MyID") %>'>
                                                                        <asp:Label ID="lblQty1503" runat="server" Font-Size="Medium" Text='<%# Eval("Qty150") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:LinkButton ID="LinkbtnQty2003" class="btn btn-danger btn-sm" Style="width: 100%;" CommandName="btnQty200" CommandArgument='<%# Eval("MyID") %>' runat="server">
                                                                        <asp:Label ID="lblQty2003" runat="server" Font-Size="Medium" Text='<%# Eval("Qty200") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:LinkButton ID="LinkbtnQty2503" class="btn btn-danger btn-sm" Style="width: 100%;" CommandName="btnQty250" CommandArgument='<%# Eval("MyID") %>' runat="server">
                                                                        <asp:Label ID="lblQty2503" runat="server" Font-Size="Medium" Text='<%# Eval("Qty250") %>'></asp:Label>
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                             <%-- t --%>
                                                            <panel id="pnlDriverCheckList123" style="padding: 1px; background-color: #fff; border: 2px solid pink; display: none; overflow: auto;" runat="server" cssclass="modalPopup">
                                                                            <div class="modal-dialog" style="position:fixed;left:30%;top:20px">
                                                                                <div class="modal-content" style="width:400px;">
                                                                                    <div class="modal-header" style="background-color:#44B6AE;">										
											                                            <h4 class="modal-title"><i class="fa fa-file-text-o"></i> Kitchen Preparation Report</h4>
										                                            </div>
                                                                
                                                                                    <div class="modal-body">
											                                        <div class="scroller" style="height:300px;" data-always-visible="1" data-rail-visible1="1">
                                                                                           <asp:ListView ID="Listcombine123" runat="server">
                                                                                            <ItemTemplate>
                                                                                        <div class="row">
                                                                                             <div class="col-md-12">                                                                                                                                                                                                
                                                                                                      <div class="col-md-3">
                                                                                                            <asp:Label ID="lblCD1" Font-Bold="true" Font-Size="Medium" runat="server" Text="Driver"></asp:Label>
                                                                                                      </div>
                                                                                                  <div class="col-md-1">
                                                                                                            <asp:Label ID="lblC1" Font-Bold="true" Font-Size="Medium" runat="server" Text=":-"></asp:Label>
                                                                                                      </div>
                                                                                                      <div class="col-md-6">
														                                                    <asp:Label ID="lblCDriver" runat="server" Text='<%# DriverName(Convert.ToInt32(Eval("DriverID"))) %>'></asp:Label>
                                                                                                      </div> 
                                                                                               </div>            
														                                 </div>   
                                                                                        <div class="row">
                                                                                             <div class="col-md-12">                                                                                                                                                                                                
                                                                                                      <div class="col-md-3">
                                                                                                            <asp:Label ID="lblCP1" Font-Bold="true" Font-Size="Medium" runat="server" Text="Product"></asp:Label>
                                                                                                      </div>
                                                                                                        <div class="col-md-1">
                                                                                                            <asp:Label ID="lblC2" Font-Bold="true" Font-Size="Medium" runat="server" Text=":-"></asp:Label>
                                                                                                      </div>
                                                                                                      <div class="col-md-6">
														                                                    <asp:Label ID="ProductC1" runat="server" Text='<%# Eval("ProdName1") %>'></asp:Label>
                                                                                                      </div> 
                                                                                               </div>            
														                                 </div> 
                                                                                        <div class="row">
                                                                                             <div class="col-md-12">                                                                                                                                                                                                
                                                                                                      <div class="col-md-3">
                                                                                                            <asp:Label ID="lblCCU1" Font-Bold="true" Font-Size="Medium" runat="server" Text="Customer"></asp:Label>
                                                                                                      </div>
                                                                                                  <div class="col-md-1">
                                                                                                            <asp:Label ID="lblC3" Font-Bold="true" Font-Size="Medium" runat="server" Text=":-"></asp:Label>
                                                                                                      </div>
                                                                                                      <div class="col-md-6">
														                                                    <asp:Label ID="CustomerC1" runat="server" Text='<%# CustomID(Convert.ToInt32(Eval("CustomerID"))) %>'></asp:Label>
                                                                                                      </div> 
                                                                                               </div>            
														                                 </div> 
                                                                                        <div class="row">
                                                                                             <div class="col-md-12">                                                                                                                                                                                                
                                                                                                      <div class="col-md-3">
                                                                                                            <asp:Label ID="lblCQ1" Font-Bold="true" Font-Size="Medium" runat="server" Text="QTY"></asp:Label>
                                                                                                      </div>
                                                                                                  <div class="col-md-1">
                                                                                                            <asp:Label ID="lblC4" Font-Bold="true" Font-Size="Medium" runat="server" Text=":-"></asp:Label>
                                                                                                      </div>
                                                                                                      <div class="col-md-6">
														                                                    <asp:Label ID="QTYC1" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                                                                            <asp:Label ID="itemWeightC" runat="server" Text='<%# "(Weight" +"-"+ Convert.ToInt32(Eval("ItemWeight")) +")" %>'></asp:Label>
                                                                                                            <asp:Label ID="StatusssC" runat="server" Text='<%# GetSTD(Convert.ToDateTime(Eval("ProductionDate")).ToShortDateString()) %>'></asp:Label>
                                                                                                      </div> 
                                                                                               </div>            
														                                 </div>        
                                                                                                <hr />      
                                                                                                </ItemTemplate>                                                                                                                                                                       
                                                                                         </asp:ListView>    
                                                                                        
											                                        </div>
                                                                                    </div>
                                                                 
                                                                                    <div class="modal-footer">                                                                                         
                                                                                         <asp:LinkButton ID="btncancle" runat="server"  Text="Cancel" class="btn default"/>
                                                                                    </div>
                                                                  
                                                                           </div>
                                                                                </div>
                                                                        </panel>
                                                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
                                                                BackgroundCssClass="modalBackground" CancelControlID="btncancle" Enabled="True"
                                                                PopupControlID="pnlDriverCheckList123" TargetControlID="lbkView3">
                                                            </cc1:ModalPopupExtender>
                                                            <%-- t --%>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </tbody>
                                            </table>

                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <div class="quick-nav-overlay"></div>
    <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery.blockui.min.js"></script>
   
    <script src="assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/table-datatables-buttons.min.js" type="text/javascript"></script>
    <script src="assets/layouts/layout4/scripts/layout.min.js" type="text/javascript"></script>
    <script src="assets/layouts/layout4/scripts/demo.min.js" type="text/javascript"></script>
    <script src="assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
    <script src="assets/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>
</body>
</html>
