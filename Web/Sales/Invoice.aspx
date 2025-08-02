<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="Web.Sales.InvoiceNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ECOMM/UserControl/items.ascx" TagPrefix="uc1" TagName="items" %>
<%@ Register Src="~/CRM/UserControl/Custmer.ascx" TagPrefix="uc1" TagName="Custmer" %>
<%@ Register Src="~/UserControl/InvoiceItem.ascx" TagPrefix="uc1" TagName="InvoiceItem" %>
<%@ Register Src="~/UserControl/InvoiceItemList.ascx" TagPrefix="uc1" TagName="InvoiceItemList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function abc(obj) {

            debugger
            var q = $(obj).closest('tr').find('#txtq').val();
            var p = $(obj).closest('tr').find('#txtp').val();
            var t = $(obj).closest('tr').find('#txtt').val();
            var s = parseInt(q) - parseInt(p);
            $(obj).closest('tr').find('#txtt').val(s);
        }
    </script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=PrindPo.ClientID %>");
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
    </script>
    <style type="text/css">
        .form-control .select2me {
            Color: #8e5fa2;
        }

        .select2-chosen {
            Color: #8e5fa2;
        }

        .control-label {
            Color: #428bca;
            font-weight: bold;
        }

        .form-control {
            Color: #8e5fa2;
        }

        .form-horizontal .control-label {
            margin-bottom: 0;
            padding-top: 7px;
            text-align: start;
        }

        .repHeader {
            Color: #428bca;
        }

        th {
            text-align: center;
        }
    </style>
    <style>
        .aspNetDisabled btn btn-icon-only green {
            cursor: not-allowed !important;
        }

        .aspNetDisabled {
            cursor: not-allowed !important;
        }
    </style>
    <script type="text/javascript">

        function ace_itemCoutry(sender, e) {

            var HiddenField3 = $get('<%= HiddenField3.ClientID %>');

            HiddenField3.value = e.get_value();

        }


    </script>
   <%-- <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>--%>
    <script src="PrductJs/CommonSales.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div id="b" runat="server">

        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">

                            <div class="portlet box green ">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>
                                        <asp:Label runat="server" ID="lbllistname"></asp:Label>

                                    </div>
                                    <div class="tools">
                                        <a id="shlinkProductDetails" runat="server" href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:LinkButton ID="BtnCheckin" class="btn purple" runat="server">Cash Delivery</asp:LinkButton>
                                        <asp:Button ID="btnnewAdd" class="btn purple" runat="server" Visible="false" Text="Add New" OnClick="btnNew_Click" />
                                        <asp:Button ID="BTNsAVEcONFsA" Visible="false" class="btn blue" runat="server" Text="Draft Invoice" OnClientClick="return showWarningToast()" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnSaveData" class="btn yellow" runat="server" ValidationGroup="submit" Text="Save" OnClick="btnConfirmOrder_Click" />
                                        <asp:Button ID="btndiscatr" class="btn default" runat="server" Text="Exit" OnClick="btnExit_Click" OnClientClick="return confirm('Are you sure you want to Exit data?');" />
                                        <asp:Button ID="btnEditLable" runat="server" class="btn red" OnClick="btnEditLable_Click" Text="Update Label" />
                                        <%--<asp:Button ID="Button3" runat="server" Text="Toast" OnClick="Button3_Click" />--%>
                                        &nbsp;
                                        <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="/assets/global/img/flags/us.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="/assets/global/img/flags/ae.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="/assets/global/img/flags/fr.png" /></asp:LinkButton>
                                    </div>
                                </div>
                                <div id="shProductDetails" runat="server" class="portlet-body" style="padding: 10px;">
                                    <asp:Panel ID="panelMsg" runat="server" Visible="false">
                                        <div class="alert alert-danger alert-dismissable">
                                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                            <asp:Label ID="lblErreorMsg" runat="server"></asp:Label>
                                        </div>
                                    </asp:Panel>
                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <asp:Panel ID="PrindPo" runat="server">
                                                <div class="tab-pane active" id="tab_general1">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <div class="col-md-2" style="padding-left: 25px; padding-top: 5px;">
                                                                <div class="radio-list">
                                                                    <asp:RadioButtonList ID="rbtPsle" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtPsle_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="1" Text="Credit" style="padding-right: 10px;"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Selected="True" Text="Cash" style="padding-right: 10px;"></asp:ListItem>
                                                                        <%-- By Dipak --%>
                                                                        <%--<asp:ListItem Value="3" Text="Corp" style="padding-right: 10px;"></asp:ListItem>--%>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:Label runat="server" ID="Label18" Text="Salesman" class="col-md-3 control-label"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpselsmen" CssClass="form-control select2me" runat="server"></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6" runat="server" id="Drpdown" visible="true">
                                                                <asp:Label runat="server" ID="lblSupplier1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplier1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="BTnEdit">
                                                                    <div class="input-group">
                                                                        <asp:TextBox ID="txtLocationSearch" runat="server" CssClass="form-control" autocomplete="off" placeholder="Custmer Name" OnTextChanged="txtLocationSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtLocationSearch" ServiceMethod="GetCounrty" CompletionInterval="1000" EnableCaching="FALSE" CompletionSetCount="20"
                                                                            MinimumPrefixLength="1" OnClientItemSelected="ace_itemCoutry" DelimiterCharacters=";, :" FirstRowSelected="false"
                                                                            runat="server" />
                                                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                                                        <span class="input-group-btn">
                                                                            <uc1:Custmer runat="server" ID="Custmer" />

                                                                            <span class="input-group-btn" style="border-left-width: 0px; margin-left: 0px; left: 32px; top: -18px;">
                                                                                <asp:Button ID="BTnEdit" runat="server" CssClass="btn yellow" ValidationGroup="submit1" Text="Edit" OnClick="BTnEdit_Click" />
                                                                            </span>
                                                                    </div>
                                                                    <%--<asp:DropDownList ID="ddlSupplier" CssClass="form-control select2me" runat="server" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLocationSearch" ErrorMessage=" Select Supplier  Required." CssClass="Validation" ValidationGroup="submit1" ForeColor="White"></asp:RequiredFieldValidator>
                                                                    <asp:LinkButton ID="lnkbtnSupp" runat="server" Visible="false"></asp:LinkButton>
                                                                    </asp:Panel>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblSupplier2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplier2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <%-- <div class="col-md-6" runat="server" id="doptext" visible="false">
                                                                <asp:Label runat="server" ID="Label12" class="col-md-3 control-label" Text="Customer"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox ID="txtcustomer" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="250"></asp:TextBox>
                                                                </div>

                                                            </div>--%>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                
                                                                <div class="col-md-4">
                                                                    <asp:Label runat="server" ID="lblTransactionDate1s" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTransactionDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-9">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtOrderDate" CssClass="form-control"
                                                                        runat="server" placeholder="Transaction Date" onchange="checkdate(this)"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtOrderDate" Format="dd-MMM-yy" Enabled="True">
                                                                    </cc1:CalendarExtender>
                                                                        </div>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblTransactionDate2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTransactionDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                <div class="col-md-8">
                                                                    <asp:Label runat="server" ID="Label1" class="col-md-3 control-label" Text="Transaction #"></asp:Label>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox AutoCompleteType="Disabled" ID="SubSerialNo" ReadOnly="true" CssClass="form-control" runat="server" placeholder="Transaction Number"></asp:TextBox>
                                                                    </div>
                                                                   
                                                               <%-- </div>

                                                                <div class="col-md-4">--%>
                                                                    <%--<asp:Label runat="server" ID="lblNo1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNo1s" class="col-md-1 control-label" Visible="false"></asp:TextBox>--%>
                                                                    <div class="col-md-4">
                                                                        
                                                                        <asp:TextBox AutoCompleteType="Disabled" ID="txtTraNoHD" ReadOnly="true" CssClass="form-control" runat="server" placeholder="Transaction Number"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblNo2h" class="col-md-1 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNo2h" class="col-md-1 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>


                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblTerms1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTerms1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpterms" CssClass="form-control select2me" runat="server"></asp:DropDownList>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblTerms2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTerms2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>





                                                        </div>

                                                        <div class="form-group" style="display: none">
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblBatchNo1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBatchNo1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtBatchNo" CssClass="form-control" runat="server" placeholder="Batch Number" Text="99999"></asp:TextBox>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBatchNo" ErrorMessage="Batch Number Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblBatchNo2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBatchNo2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblReference1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReference1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>


                                                                <div class="col-md-8">
                                                                    <div class="input-group">
                                                                        <asp:TextBox AutoCompleteType="Disabled" ID="txtrefreshno" CssClass="form-control tags" runat="server" placeholder="Reference Number"></asp:TextBox>
                                                                        <span class="input-group-btn">
                                                                            <asp:LinkButton ID="btnrefesh" data-toggle="tooltip" ToolTip="Add New Refersh" runat="server">
                                                                                 <i class="icon-plus" style="color:black;padding-left: 4px;"></i>
                                                                            </asp:LinkButton>
                                                                        </span>
                                                                    </div>
                                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtrefreshno" ErrorMessage="Reference Number Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblReference2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReference2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                <panel id="pnlresponsive" runat="server" cssclass="modalPopup" style="display: none; height: auto; overflow: auto; top: 0px;">
       <div class="row">
                                                                            <div class="col-md-12" style="position:fixed;left:20%;top:0%; width:auto">
                                                                                <div class="portlet box purple">
                                                                                    <div class="portlet-title">
                                                                                        <div class="caption"> 
                                                                                            <i class="fa fa-globe"></i><asp:Label runat="server" style="color: White; width: 166px;" ID="lblAddReference1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAddReference1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                            <asp:Label runat="server" ID="lblAddReference2h" style="width: 129px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAddReference2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="portlet-body"> 
        <div class="modal-body">
            <div class="row">
           
                  <div class="col-md-6">
                                                             <h4><b><asp:Label runat="server" ID="lblReferenceType1s" style="width: 174px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReferenceType1s" class="col-md-4 control-label" Visible="false"></asp:TextBox></b></h4>
                                                            <p>
                                                                 <asp:DropDownList ID="drpRefnstype" Style="width: 300px;" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator9" runat="server" ErrorMessage="Add Reference Required" ControlToValidate="drpRefnstype" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </p>
                      <h4><b><asp:Label runat="server" ID="lblReferenceType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReferenceType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></b></h4>
                                                            <h4><b><asp:Label runat="server" ID="lblReferenceNo1s" style="width: 154px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReferenceNo1s" class="col-md-4 control-label" Visible="false"></asp:TextBox></b></h4>
                                                            <p>
                                                                 
                                                               <asp:TextBox Style="width: 300px;"  ID="txtrefresh" runat="server" class="form-control" maxlength="70"></asp:TextBox>
                                                                 <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator10" runat="server" ErrorMessage="Add Reference Required" ControlToValidate="txtrefresh" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                             
                                                            </p>
                      <h4><b><asp:Label runat="server" ID="lblReferenceNo2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReferenceNo2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></b></h4>
                      
                                                           

            </div>
        </div>
            </div>
        <div class="modal-footer">
           <asp:LinkButton ID="btnaddrefresh" class="btn green-haze modalBackgroundbtn-circle" OnClick ="btnaddrefresh_Click" ValidationGroup="S" runat="server"> Save</asp:LinkButton>
 <asp:Button ID="Button4" runat="server" class="btn green-haze btn-circle" Text="Cancel" />

        </div>
                                                                                    </div></div> </div> </div> 
                                                                                    </panel>
                                                                <cc1:ModalPopupExtender ID="ModalPopupExtender7" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button4" Enabled="True" PopupControlID="pnlresponsive" TargetControlID="btnrefesh">
                                                                </cc1:ModalPopupExtender>


                                                            </div>


                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblTotal1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotal1s" CssClass="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txttotxl" ReadOnly="true" CssClass="form-control" runat="server" placeholder="Total Cost"></asp:TextBox>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblTotal2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotal2h" CssClass="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                <div class="col-md-5" style="margin-bottom: 0px; margin-top: -7px;">
                                                                    <asp:Label runat="server" ID="Label2" class="col-md-2 control-label" Text="Delivery Note" Style="width: 110px;"></asp:Label>
                                                                    <div class="col-md-2">
                                                                        <asp:CheckBox ID="chbDeliNote" runat="server" CssClass="checkbox-inline " Style="margin-left: -10px;" />
                                                                    </div>

                                                                </div>

                                                            </div>



                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-md-12">
                                                                <asp:Label runat="server" ID="lblNotes1s" Style="width: 132px;" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNotes1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-10">
                                                                    <asp:TextBox ID="txtNoteHD" CssClass="form-control" MaxLength="550" runat="server" data-toggle="tooltip" ToolTip="Description" Style="resize: none" placeholder="Notes" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblNotes2h" class="col-md-1 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNotes2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="form-group">
                                                        </div>
                                                    </div>

                                                    <div class="tabbable">
                                                        <%-- <ul class="nav nav-tabs">
                                                            <li class="active">
                                                                <a id="A5" runat="server" alt="FormDisplay" class="lblDisPro2" style="color: #fff; background-color: #e08283" onclick="showhidepanelItems('ITEMS');">1. Items</a>
                                                            </li>
                                                            <%--<asp:Panel ID="penelCorpOverheadCost" runat="server">--%>
                                                        <%-- By Dipak --%
                                                            <li>
                                                                <a id="A4" runat="server" alt="FormDisplay" class="lblDisPro2" style="color: #fff; background-color: #f3c200" onclick="showhidepanelItems('OHCOST');">2. Overhead Cost</a>
                                                            </li>
                                                            <%--</asp:Panel>--%
                                                        </ul>--%>
                                                        <asp:Panel ID="panelRed" class="portlet-body" runat="server">
                                                            <uc1:InvoiceItem runat="server" ID="InvoiceItem" />

                                                        </asp:Panel>
                                                        <asp:Panel ID="ListItems" class="portlet-body" runat="server">
                                                            <div class="portlet box red-pink">
                                                                <div class="portlet-title">
                                                                    <div class="caption">
                                                                        <i class="fa fa-gift"></i>
                                                                        <asp:Label ID="lblitems" Style="font-size: 14px;" runat="server" Text="Items"></asp:Label>
                                                                        (
                                                                                    <asp:Label runat="server" ID="lblProductCount"></asp:Label>)
                                                                    </div>
                                                                    <div class="actions btn-set">
                                                                        <asp:Label ID="lblitemsearch" runat="server" Visible="false"></asp:Label>
                                                                        <asp:LinkButton ID="btnAddItemsIn" runat="server" CssClass="btn default btn-xs purple" Visible="true" OnClick="btnAddnew_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;"><i class="fa fa-plus"> Add New</i></asp:LinkButton>

                                                                        <asp:LinkButton ID="btndiscartitems" runat="server" CssClass="btn default" OnClick="btndiscartitems_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;">Exit</asp:LinkButton>

                                                                    </div>
                                                                </div>
                                                                <asp:Panel ID="pnlSuccessMsg123" runat="server" Visible="false">
                                                                    <div class="alert alert-danger alert-dismissable">
                                                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                        <asp:Label ID="lblMsg123" runat="server"></asp:Label>
                                                                    </div>
                                                                </asp:Panel>

                                                                <div class="portlet-body">
                                                                    <div class="table-scrollable">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th scope="col">
                                                                                        <asp:Label ID="Label13" runat="server" Text="#"></asp:Label></th>
                                                                                    <th scope="col">
                                                                                        <asp:Label runat="server" ID="lblProductCodeProductName1s" class="col-md-4 control-label" Text="Product Code"></asp:Label><asp:TextBox runat="server" ID="txtProductCodeProductName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <asp:Label runat="server" ID="lblProductCodeProductName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductCodeProductName2h" Style="width: 189px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </th>
                                                                                    <th scope="col">
                                                                                        <asp:Label ID="Label17" runat="server" Text="Description"></asp:Label>
                                                                                    </th>
                                                                                    <th scope="col">
                                                                                        <asp:Label runat="server" ID="lblQuantity11s" class="col-md-4 control-label" Text="Quantity"></asp:Label><asp:TextBox runat="server" ID="txtQuantity11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <asp:Label runat="server" ID="lblQuantity12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantity12h" Style="width: 78px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </th>
                                                                                    <th scope="col">
                                                                                        <asp:Label runat="server" ID="lblUnitPrice1s" class="col-md-4 control-label" Text="Unit Price"></asp:Label><asp:TextBox runat="server" ID="txtUnitPrice1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <asp:Label runat="server" ID="lblUnitPrice2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPrice2h" Style="width: 77px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </th>

                                                                                    <th scope="col">
                                                                                        <asp:Label runat="server" ID="lblTotal11s" class="col-md-4 control-label" Text="Total"></asp:Label><asp:TextBox runat="server" ID="txtTotal11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <asp:Label runat="server" ID="lblTotal12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotal12h" Style="width: 85px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </th>
                                                                                    <th scope="col">Action
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="Repeater2_ItemCommand">
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="Label27" runat="server" Text='<%# getprodnameui(Convert .ToInt32 ( Eval("MyProdID"))) %>'></asp:Label>
                                                                                                <asp:Label ID="lblProductNameItem" runat="server" Visible="false" Text='<%#Eval("MyProdID") %>'></asp:Label>
                                                                                                <asp:Label ID="lblDiscription" Visible="false" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label>
                                                                                                <asp:Label ID="lblUOM" Visible="false" runat="server" Text='<%#Eval("UOM") %>'></asp:Label>
                                                                                                <asp:Label ID="lblUNITPRICE" Visible="false" runat="server" Text='<%#Eval("UNITPRICE") %>'></asp:Label>
                                                                                                <asp:Label ID="lblDISAMT" Visible="false" runat="server" Text='<%#Eval("DISAMT") %>'></asp:Label>
                                                                                                <asp:Label ID="lblDISPER" Visible="false" runat="server" Text='<%#Eval("DISPER") %>'></asp:Label>
                                                                                                <asp:Label ID="lblTAXAMT" Visible="false" runat="server" Text='<%#Eval("TAXAMT") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label16" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblDisQnty" runat="server" Text='<%#Eval("QUANTITY") %>' />
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblOHAmnt" runat="server" Text='<%#Eval("UNITPRICE") %>' />
                                                                                            </td>


                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblTotalCurrency" runat="server" Text='<%#Eval("AMOUNT") %>' />
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:LinkButton ID="lnkbtnItem" runat="server" CommandName="editDT" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") %>'>
                                                                                                    <%--+","+Eval("MyProdID")--%>
                                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="images/editRec.png" />
                                                                                                </asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDT" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") %>' OnClientClick="return confirm('Do you want to delete product?')">
                                                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="images/deleteRec.png" />
                                                                                                </asp:LinkButton>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                                <tfoot>
                                                                                    <tr>

                                                                                        <th colspan="3">Total</th>
                                                                                        <%-- <th>Amount</th>--%>
                                                                                        <th style="color: green">
                                                                                            <asp:Label ID="lblqtytotl" runat="server" Text="0"></asp:Label></th>
                                                                                        <th style="color: green">
                                                                                            <asp:Label ID="lblUNPtotl" runat="server" Text="0.00"></asp:Label></th>

                                                                                        <th style="color: green">
                                                                                            <asp:Label ID="lblTotatotl1" runat="server" Text="0.00"></asp:Label>
                                                                                            
                                                                                        </th>
                                                                                        <th colspan="2"></th>

                                                                                    </tr>
                                                                                </tfoot>
                                                                            </tbody>
                                                                        </table>

                                                                    </div>

                                                                    <table class="table table-bordered" id="datatable1" style="margin-left: 390px; width: 494px; margin-top: -35px;">

                                                                        <tbody>
                                                                            <asp:LinkButton ID="btnaddamunt" runat="server" OnClick="btnaddamunt_Click" Style="margin-left: 350px; margin-top: 0px; margin-bottom: 9px;">
                                                                                <asp:Image ID="Image3" runat="server" ImageUrl="images/plus.png" />
                                                                            </asp:LinkButton>
                                                                            <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <tr class="gradeA">
                                                                                        <td>
                                                                                            <asp:DropDownList ID="drppaymentMeted" runat="server" Style="width: 124px;" CssClass="form-control">
                                                                                            </asp:DropDownList>
                                                                                            <asp:Label ID="lblOHType" Visible="false" Text='<%#Eval("PaymentTermsId") %>' runat="server"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <b><font size="1px"> <asp:Label runat="server" slyle="font-size: 9px;" ID="Label19" Text="Card/Reference# , Bank Approval #" ></asp:Label></font></b>
                                                                                            <asp:TextBox ID="txtrefresh" runat="server" Text='<%#Eval("ReferenceNo")+","+Eval("ApprovalID") %>' AutoCompleteType="Disabled" placeholder="Card/Reference# , Bank Approval #" CssClass="form-control tags"></asp:TextBox>


                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtammunt" runat="server" Text='<%#Eval("Amount") %>' AutoCompleteType="Disabled" placeholder="Amount" Style="width: 104px;" CssClass="form-control"></asp:TextBox>
                                                                                        </td>


                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>


                                                        </asp:Panel>
                                                        <%--<uc1:InvoiceItemList runat="server" id="InvoiceItemList" />--%>
                                                    </div>

                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="btnPenel" runat="server" Visible="true">
                                                <div class="pull-right" style="margin-left: 32%">

                                                    <asp:Button ID="btnSubmit" Visible="false" class="btn blue" runat="server" Text="Draft Invoice" OnClientClick="return showWarningToast()" OnClick="btnSubmit_Click" />
                                                    <asp:Button ID="btnConfirmOrder" Visible="false" class="btn yellow" runat="server" Text="Save" ValidationGroup="submit" OnClick="btnConfirmOrder_Click" />
                                                    <asp:Button ID="btnExit" class="btn default" runat="server" Visible="false" Text="Exit" OnClick="btnExit_Click" OnClientClick="return confirm('Are you sure you want to Exit data?');" />
                                                    <asp:Button ID="btnPrint" class="btn red" OnClick="btnPrint_Click" runat="server" Text="Save and Print" />
                                                    <%--OnClientClick="return PrintPanel();" yogesh 1704--%>

                                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                                </div>
                                            </asp:Panel>

                                            <div id="form_Mail" class="modal fade" tabindex="-1" role="dialog" style="display: block; margin-top: -106.5px; width: 700px;">
                                                <div class="modal-dialog">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="close(this);disButton(this);">&times;</button><h4 class="modal-title"><i class="icon-paragraph-justify2"></i>
                                                                <asp:Label runat="server" ID="lblSendMail1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSendMail1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <asp:Label runat="server" ID="lblSendMail2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSendMail2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                <asp:Button ID="Button1" class="btn btn-default" data-dismiss="modal" aria-hidden="true" OnClientClick="close(this)" runat="server" Text="Close" />
                                                                <a id="a1" data-dismiss="modal" aria-hidden="true" onclick="close(this)"></a>
                                                            </h4>
                                                        </div>
                                                        <br />
                                                        <div class="block-flat">
                                                            <div class="row">
                                                                <div class="col-md-2 form-group">
                                                                </div>
                                                                <div class="col-md-4 form-group">
                                                                    <asp:Label runat="server" ID="lblToRecipients1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtToRecipients1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtRecipient" runat="server" CssClass="form-control" placeholder="Recipients email"></asp:TextBox>
                                                                    <asp:Label runat="server" ID="lblToRecipients2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtToRecipients2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4 form-group">
                                                                    <asp:Label runat="server" ID="lblCC1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCC1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtRecCC" runat="server" CssClass="form-control" placeholder="Recipients email"></asp:TextBox>
                                                                    <asp:Label runat="server" ID="lblCC2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCC2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hidICHD" runat="server" />


                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="penelCheckin" runat="server" Style="display: none">
                                <div class="col-md-6" style="top: -400px;">
                                    <div class="portlet box purple">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-globe"></i>
                                                <label style="text-align: left; margin-top: 2px;">
                                                    Cashier 
                                                    <asp:Label runat="server" ID="Label19" Style="width: 174px;" Text="Abu Ali Manager"></asp:Label>
                                                    Cash Delivery</label>

                                                <%--<asp:Label runat="server" Style="color: White; width: 166px;" ID="Label19" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox1" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                <asp:Label runat="server" ID="Label20" Style="width: 129px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox2" class="col-md-4 control-label" Visible="false"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="modal-body">
                                                <div class="row">

                                                    <div class="col-md-6">
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="2">



                                                                    <asp:Label runat="server" ID="Label21" Style="width: 174px;" Text="Ref No.# :"></asp:Label><asp:TextBox runat="server" ID="txtRefNo" Width="125px"></asp:TextBox>


                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:Label runat="server" ID="Label23" Style="width: 174px;" Text="VA Ref No.# :"></asp:Label><asp:TextBox runat="server" ID="txtVARefNo" Width="125px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <p style="width: 85px;">


                                                                        <br />
                                                                        <span>Amount :</span>
                                                                        <br />
                                                                        <span>Count :</span>
                                                                        <br />
                                                                        <span>Verified.Date :</span>
                                                                        <br />
                                                                        <span>Verified :</span>

                                                                    </p>
                                                                </td>
                                                                <td>
                                                                    <p>

                                                                        <asp:Label runat="server" ID="Label28" Style="width: 174px;" Text="Cash in hand"></asp:Label><asp:TextBox Width="100px" runat="server" ID="txtcashinhand"></asp:TextBox><asp:TextBox Width="100px" runat="server" ID="txtcashinhandc"></asp:TextBox><asp:TextBox Width="100px" runat="server" ID="txtcashinhandd"></asp:TextBox><asp:CheckBox Checked="true" ID="chcashinhand" runat="server" />

                                                                    </p>
                                                                </td>
                                                                <td>
                                                                    <p>

                                                                        <asp:Label runat="server" ID="Label29" Text="Cheque in hand"></asp:Label><asp:TextBox runat="server" Width="100px" ID="txtChequeinhand"></asp:TextBox><asp:TextBox Width="100px" runat="server" ID="txtChequeinhandc"></asp:TextBox><asp:TextBox Width="100px" runat="server" ID="TextBox7"></asp:TextBox><asp:CheckBox ID="CheckBox3" runat="server" />



                                                                    </p>
                                                                </td>
                                                                <td>
                                                                    <p>

                                                                        <asp:Label runat="server" ID="Label31" Text="Bank Pay Slip"></asp:Label><asp:TextBox runat="server" Width="100px" ID="txtBankPaySlip"></asp:TextBox><asp:TextBox Width="100px" runat="server" ID="txtBankPaySlipc"></asp:TextBox><asp:TextBox Width="100px" runat="server" ID="TextBox9"></asp:TextBox><asp:CheckBox ID="CheckBox4" runat="server" />



                                                                    </p>
                                                                </td>
                                                                <td>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label25" Text="Gift/Voucher"></asp:Label><asp:TextBox runat="server" Width="100px" ID="txtGiftVoucher"></asp:TextBox><asp:TextBox Width="100px" runat="server" ID="txtGiftVoucherc"></asp:TextBox><asp:TextBox Width="100px" runat="server" ID="TextBox11"></asp:TextBox><asp:CheckBox ID="CheckBox5" runat="server" />
                                                                    </p>
                                                                </td>

                                                            </tr>
                                                        </table>





                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:DropDownList ID="drpChkinEmp" Style="width: 150px;" runat="server" CssClass="form-control inline"></asp:DropDownList>

                                                <asp:LinkButton ID="LinkButton14" class="btn green-haze modalBackgroundbtn-circle" OnClick="btnCheckIn_Click" ValidationGroup="S" runat="server"> Save</asp:LinkButton>
                                                <asp:Button ID="Button6" runat="server" class="btn default btn-circle" Text="Cancel" />
                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator11" runat="server" ErrorMessage="Add Reference Required" ControlToValidate="drpRefnstype" ValidationGroup="S"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender10" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button6" Enabled="True" PopupControlID="penelCheckin" TargetControlID="BtnCheckin"></cc1:ModalPopupExtender>

                            <div class="portlet box yellow">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>
                                        <asp:Label runat="server" ID="lbllabellist"></asp:Label>
                                    </div>
                                    
                                    <div class="tools">
                                        <a id="A2" runat="server" href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:Button ID="btnNew" class="btn purple" runat="server" Visible="false" Text="Add New" OnClick="btnNew_Click" />
                                    </div>
                                    <div class="tools">
                                        <asp:CheckBox ID="CheckgrdPO" runat="server" Text="Show Deleted" AutoPostBack="true" Checked="false" OnCheckedChanged="CheckgrdPO_CheckedChanged" />&nbsp &nbsp
                                    </div>
                                </div>
                                <div id="Div1" runat="server" class="portlet-body">
                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general">

                                                <div class="portlet-body">
                                                    <div class="form-group">

                                                        <div class="col-md-6">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="col-md-8">
                                                                <asp:TextBox AutoCompleteType="Disabled" ID="txtallserchforview" CssClass="form-control"
                                                                    runat="server" placeholder="Advance search"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="col-md-8">
                                                                    <asp:CheckBox ID="cbkseriz" runat="server" Text="Serial" />
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:LinkButton ID="btnallserch" CssClass="btn btn-icon-only red" runat="server" OnClick="btnallserch_Click">
                                                                                 <i class="fa fa-search" ></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                        <thead class="repHeader">
                                                            <tr>
                                                                <th></th>
                                                                <th>
                                                                    <asp:DropDownList ID="ddlcustmoerlist" CssClass="form-control select2me" runat="server"></asp:DropDownList></th>
                                                                <th>
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtsleshdate" CssClass="form-control"
                                                                        runat="server" placeholder="Sale Date"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtsleshdate" Format="dd-MMM-yy" Enabled="True">
                                                                    </cc1:CalendarExtender>
                                                                </th>
                                                                <th colspan="2">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtremarcklist" CssClass="form-control"
                                                                        runat="server" placeholder="Remark"></asp:TextBox>
                                                                </th>

                                                                <th>
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtreferencrlist" CssClass="form-control"
                                                                        runat="server" placeholder="Reference No"></asp:TextBox>
                                                                </th>
                                                                <th>
                                                                    <asp:LinkButton ID="btnlistserch" CssClass="btn btn-icon-only purple" runat="server" OnClick="btnlistserch_Click">
                                                                                 <i class="fa fa-search" ></i>
                                                                    </asp:LinkButton>
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    <asp:Label ID="lblSN" runat="server" Text="#"></asp:Label></th>
                                                               
                                                                <th>
                                                                    <asp:Label runat="server" ID="lblSupplierName1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplierName1s" Style="width: 158px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <asp:Label runat="server" ID="lblSupplierName2h" Style="width: 136px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplierName2h" Style="width: 141px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </th>
                                                                <th>
                                                                    <asp:Label runat="server" ID="lblOrderDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOrderDate1s" Style="width: 128px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <asp:Label runat="server" ID="lblOrderDate2h" Style="width: 107px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOrderDate2h" Style="width: 107px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </th>
                                                                <th>
                                                                    <asp:Label runat="server" ID="lblTotalAmount1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalAmount1s" Style="width: 131px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <asp:Label runat="server" ID="lblTotalAmount2h" Style="width: 123px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" Style="width: 117px;" ID="txtTotalAmount2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </th>
                                                                 <th>
                                                                    <asp:Label runat="server" ID="LblTransDocNO" Style="width: 89px;" Text="Trans Doc#"></asp:Label>/
                                                                    <asp:Label runat="server" ID="LblMYTRANSID" Style="width: 89px;" Text="Trans #"></asp:Label>
                                                                </th>
                                                                <th style="width:68px;">
                                                                    <asp:Label runat="server" ID="lblStatus1s"  class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStatus1s" Style="width: 89px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <asp:Label runat="server" ID="lblStatus2h" Style="width: 68px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStatus2h" Style="width: 102px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </th>

                                                                <th>
                                                                    <asp:Label runat="server" ID="lblEdit11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEdit11s" Style="width: 73px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <asp:Label runat="server" ID="lblEdit12h" Style="width: 73px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" Style="width: 91px;" ID="txtEdit12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </th>

                                                                <th>
                                                                    <asp:Label runat="server" ID="Label3" Text="Print"></asp:Label>
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="grdPO" runat="server" OnItemCommand="grdPO_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr class="gradeA">
                                                                        <td>
                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        
                                                                        <td>
                                                                            <asp:Label ID="lblMYTRANSID" runat="server" Visible="false" Text='<%# Eval("MYTRANSID") %>'></asp:Label>
                                                                            <asp:LinkButton ID="LinkButton11" runat="server" CommandName="Btnview" CommandArgument='<%# Eval("MYTRANSID") %>'>
                                                                                <asp:Label ID="lblPSN" runat="server" Text='<%#getsuppername(Convert .ToInt32 ( Eval("CUSTVENDID"))) %>'></asp:Label>
                                                                            </asp:LinkButton>
                                                                            <asp:Label ID="lbluserid" Visible="false" runat="server" Text='<%#Eval("USERID") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkButton12" runat="server" CommandName="Btnview" CommandArgument='<%# Eval("MYTRANSID") %>'>
                                                                                <asp:Label ID="lblODate" runat="server" Text='<%# Eval("TRANSDATE", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkButton13" runat="server" CommandName="Btnview" CommandArgument='<%# Eval("MYTRANSID") %>'>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("TOTAMT") %>'></asp:Label>
                                                                            </asp:LinkButton>
                                                                            <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("Status") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblMYTRANSIDshow" runat="server" Visible="true" Text='<%# Eval("TransDocNo")+" / "+ Eval("MYTRANSID") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%--<asp:Label ID="lblspnoo" runat="server" Text='<%# Eval("Status").ToString() =="SO" ? "Final" : "Draft "%>'></asp:Label></td>--%>
                                                                            <asp:Label ID="lblspnoo" runat="server" Text='<%# Status(Convert.ToInt32(Eval("MYTRANSID")))%>'></asp:Label></td>
                                                                        <td class="center">
                                                                            <asp:LinkButton ID="lnkbtnPurchase_Order" Visible="false" runat="server" CommandName="Edit" CommandArgument='<%# Eval("MYTRANSID") %>'>
                                                                                <asp:Image ID="Imagedelete" runat="server" ImageUrl="images/editRec.png" />
                                                                            </asp:LinkButton>
                                                                           
                                                                            <asp:LinkButton ID="lnkbtndelete" Visible="false" runat="server" CommandName="Delete" OnClientClick="return confirm('Do you want to delete purchase quotation?')" CommandArgument='<%# Eval("MYTRANSID") %>'>
                                                                                <asp:Image ID="Image4" runat="server" ImageUrl="images/deleteRec.png" />
                                                                            </asp:LinkButton>
                                                                            
                                                                            <%-- <asp:LinkButton ID="LinkButton14" runat="server">
                                                                               <i class="fa fa-print"></i>
                                                                            </asp:LinkButton>--%>
                                                                        </td>
                                                                        <td>
                                                                           
                                                                            <asp:LinkButton class="btn btn-lg blue" Visible="false" CommandName="btnprient" CommandArgument='<%# Eval("MYTRANSID") %>' ID="Print" Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;" runat="server">Print <i class="fa fa-print"></i></asp:LinkButton>
                                                                           
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
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
    <asp:UpdatePanel ID="test" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
