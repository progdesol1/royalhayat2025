<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="PaymentVouchar.aspx.cs" Inherits="Web.Sales.PaymentVouchar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ECOMM/UserControl/items.ascx" TagPrefix="uc1" TagName="items" %>
<%@ Register Src="~/CRM/UserControl/Custmer.ascx" TagPrefix="uc1" TagName="Custmer" %>


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
   <%-- <script type="text/javascript">

        function ace_itemCoutry(sender, e) {

            var HiddenField3 = $get('<%= HiddenField3.ClientID %>');

            HiddenField3.value = e.get_value();

        }
    </script>--%>
    <script src="//code.jquery.com/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //Iterate through each Textbox and add keyup event handler 
            $(".clsTxtToCalculate").each(function () {
                $(this).keyup(function () {
                    //Initialize total to 0        
                    var total = 0;
                    $(".clsTxtToCalculate").each(function () {

                        // Sum only if the text entered is number and greater than 0    
                        if (!isNaN(this.value) && this.value.length != 0) {
                            total += parseFloat(this.value);
                        }
                    });
                    //Assign the total to label     
                    //.toFixed() method will roundoff the final sum to 2 decimal places 
                    $('#<%=lblTotatotl.ClientID %>').html(total.toFixed(2));
                    $('#ContentPlaceHolder1_txtammunt').val(total.toFixed(2));
                    
                })
            });
        });     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <script src="PrductJs/CommonPurchase.js"></script>
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

                                        <asp:Button ID="btnnewAdd" class="btn purple" runat="server" Visible="false" Text="Add New" OnClick="btnNew_Click" />
                                        <asp:Button ID="BTNsAVEcONFsA" class="btn blue" runat="server" ValidationGroup="submit" Text="Draft Invoice"  OnClick="btnSubmit_Click" /><%--OnClientClick="return showWarningToast()"--%>
                                        <asp:Button ID="btnSaveData" class="btn yellow" runat="server"  Text="Confirm Invoice" OnClick="btnConfirmOrder_Click" />
                                        <asp:Button ID="btndiscatr" class="btn default" runat="server" Text="Exit" OnClick="btnExit_Click" OnClientClick="return confirm('Are you sure you want to Exit data?');" />
                                        <asp:Button ID="btnEditLable" runat="server" class="btn red" OnClick="btnEditLable_Click" Text="Update Label" />
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
                                                            <div class="col-md-3">
                                                                <%--<div class="radio-list">
                                                                    <asp:RadioButtonList ID="rbtPsle" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtPsle_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="3" Selected="True" Text="Supplier" style="padding-right: 10px;"></asp:ListItem>                                                                        
                                                                    </asp:RadioButtonList>
                                                                </div>--%>
                                                                <div>
                                                                    <asp:Label runat="server" ID="lblVoucherType" Text="Voucher" class="col-md-3 control-label"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpPsle" CssClass="form-control select2me input-small" runat="server" OnSelectedIndexChanged="drpPsle_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="Supplier" Value="3" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="Customer" Value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="General" Value="5"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <asp:Label runat="server" ID="Label18" Text="Salesman" class="col-md-3 control-label"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpselsmen" CssClass="form-control select2me" runat="server"></asp:DropDownList>
                                                                </div>
                                                            </div>

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
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtrefreshno" ErrorMessage="Reference Number Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblReference2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReference2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                <panel id="pnlresponsive" runat="server" cssclass="modalPopup" style=" display:none; height: auto; overflow: auto; top: 120px;">
                                                                         <div class="row">
                                                                            <div class="col-md-12" >
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


                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblTransactionDate1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTransactionDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtOrderDate" CssClass="form-control"
                                                                        runat="server" placeholder="Transaction Date" onchange="checkdate(this)"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtOrderDate" Format="dd-MMM-yy" Enabled="True">
                                                                    </cc1:CalendarExtender>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblTransactionDate2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTransactionDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                <div class="col-md-5">
                                                                    <asp:Label runat="server" ID="lblNo1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNo1s" class="col-md-1 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox AutoCompleteType="Disabled" ID="txtTraNoHD" ReadOnly="true" CssClass="form-control" runat="server" placeholder="Transaction Number"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblNo2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNo2h" class="col-md-1 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>


                                                            <div class="col-md-6" runat="server" id="Drpdown" visible="true">
                                                                <asp:Label runat="server" ID="lblSupplier1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplier1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                   <%-- <div class="input-group">
                                                                        <asp:TextBox ID="txtLocationSearch" runat="server" CssClass="form-control" autocomplete="off" placeholder="Custmer Name" OnTextChanged="txtLocationSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtLocationSearch" ServiceMethod="GetCounrty" CompletionInterval="1000" EnableCaching="FALSE" CompletionSetCount="20"
                                                                            MinimumPrefixLength="1" OnClientItemSelected="ace_itemCoutry" DelimiterCharacters=";, :" FirstRowSelected="false"
                                                                            runat="server" />
                                                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                                                        <span class="input-group-btn">
                                                                            <uc1:Custmer runat="server" ID="Custmer" />
                                                                        </span>
                                                                    </div>--%>
                                                                    <asp:DropDownList ID="ddlSupplier" CssClass="form-control select2me" runat="server" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSupplier" InitialValue="0" ErrorMessage=" Select Supplier  Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                    <asp:LinkButton ID="lnkbtnSupp" runat="server" Visible="false"></asp:LinkButton>

                                                                </div>
                                                                <asp:Label runat="server" ID="lblSupplier2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplier2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6" runat="server" id="doptext" visible="false">
                                                                <asp:Label runat="server" ID="Label12" class="col-md-3 control-label" Text="Customer"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox ID="txtcustomer" runat="server" AutoCompleteType="Disabled" CssClass="form-control" MaxLength="250"></asp:TextBox>
                                                                </div>

                                                            </div>



                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6" style="display: none">
                                                                <asp:Label runat="server" ID="lblLocalForeign1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLocalForeign1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlLocalForeign" CssClass="form-control select2me" runat="server" onchange="selectcurrency()"></asp:DropDownList>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblLocalForeign2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLocalForeign2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <asp:Panel ID="penelcorpcurency" runat="server">
                                                                <div class="col-md-6" style="display: none">
                                                                    <asp:Label runat="server" ID="lblCurrency1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCurrency1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="ddlCurrency" CssClass="form-control select2me" Enabled="false" runat="server"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCurrency" InitialValue="0" ErrorMessage=" Select Currency Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                        <%--<asp:LinkButton ID="lbtnCR"  runat="server" Text="Currency Rate"></asp:LinkButton>--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCurrency2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCurrency2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblBatchNo1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBatchNo1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtBatchNo" CssClass="form-control" runat="server" placeholder="Batch Number" Text="99999"></asp:TextBox>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBatchNo" ErrorMessage="Batch Number Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblBatchNo2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBatchNo2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblProjectNo1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProjectNo1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlProjectNo" runat="server" CssClass="form-control select2me" ForeColor="#8e5fa2">
                                                                    </asp:DropDownList>
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlProjectNo" InitialValue="0" ErrorMessage=" Select Project  Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlProjectNo" InitialValue="0" ErrorMessage=" Select Project Name  Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>--%>
                                                                    <%--<asp:TextBox AutoCompleteType="Disabled" ID="txtProjectNo" CssClass="form-control" runat="server" placeholder="Project Number"></asp:TextBox>--%>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblProjectNo2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProjectNo2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">


                                                            <div class="col-md-6" style="display: none;">
                                                                <asp:Label runat="server" ID="lblTerms1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTerms1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpterms" CssClass="form-control select2me" runat="server"></asp:DropDownList>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblTerms2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTerms2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-6" style="display: none;">
                                                                <asp:Label runat="server" ID="lblTotal1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotal1s" CssClass="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txttotxl" ReadOnly="true" CssClass="form-control" runat="server" placeholder="Total Cost"></asp:TextBox>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblTotal2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotal2h" CssClass="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                <div class="col-md-5" style="display: none;">
                                                                    <asp:Label runat="server" ID="Label2" class="col-md-2 control-label" Text="Delivery Note" Style="width: 110px;"></asp:Label>
                                                                    <div class="col-md-2">
                                                                        <asp:CheckBox ID="chbDeliNote" runat="server" CssClass="checkbox-inline " Style="margin-left: -10px;" />
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <%-- Payment new field --%>
                                                            <div class="col-md-4">
                                                                <asp:Label runat="server" ID="Label20" Text="Payment Method" CssClass="col-md-5 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox1" CssClass="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-6">
                                                                    <asp:DropDownList ID="drppmethod" CssClass="form-control select2me input-small" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drppmethod_SelectedIndexChanged">
                                                                       
                                                                        <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Bank" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drppmethod" ErrorMessage="Payment Method Required." CssClass="Validation" InitialValue="0" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                </div>
                                                                 
                                                                <asp:Label runat="server" ID="Label21" CssClass="col-md-5 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox2" CssClass="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                            
                                                            <div class="col-md-4">
                                                                <asp:Label runat="server" ID="Label26" Text="Bank Cheque #" CssClass="col-md-5 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox5" CssClass="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtbankcheck" CssClass="form-control" Enabled="false" runat="server" placeholder="Bank Check"></asp:TextBox>
                                                                </div>
                                                                <asp:Label runat="server" ID="Label28" CssClass="col-md-5 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox7" CssClass="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-4">
                                                                <asp:Label runat="server" ID="Label29" Text="Date" CssClass="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox8" CssClass="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtDate" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="TextBoxUpdatedDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" TargetControlID="txtDate" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                </div>
                                                                <asp:Label runat="server" ID="Label31" CssClass="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox10" CssClass="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <%-- Payment new field End--%>
                                                        </div>
                                                        <div class="form-group">

                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="Label23" Text="Cash/Bank ID" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox3" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtcashaccountID" CssClass="form-control" runat="server" placeholder="Cash Account ID"></asp:TextBox>
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtcashaccountID" ErrorMessage="Cash Account ID Required." CssClass="Validation"  ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <asp:Label runat="server" ID="Label25" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox4" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="Label32" Text="Amount" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox6" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtAmount" CssClass="form-control" Text="0" MaxLength="7"   runat="server"></asp:TextBox>
                                                                   <%-- <asp:RangeValidator runat="server" Type="Integer" MinimumValue="0" MaximumValue="1000" ControlToValidate="txtAmount" ForeColor="Red" ValidationGroup="submit" ErrorMessage="Amount must be between 0 and 1000" />--%>
                                                                </div>
                                                                <asp:Label runat="server" ID="Label33" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox11" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" style="display: none">
                                                            <div class="col-md-6" style="top: 20px;">
                                                                <asp:Label runat="server" ID="lblCRMActivity1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRMActivity1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlCrmAct" runat="server" CssClass="form-control select2me" ForeColor="#8e5fa2">
                                                                    </asp:DropDownList>
                                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCrmAct" InitialValue="0" ErrorMessage=" Select CRM Activity  Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblCRMActivity2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRMActivity2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6" style="top: -5px;">

                                                                <center>  <asp:Panel ID="PanelCorp" runat="server">
                                                                     <div class="col-md-4 form-group" style="margin-right: 0px; margin-bottom: 0px; margin-left: 0px;" >
                                                                           <asp:Label runat="server" ID="lblOHCost1s" class="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOHCost1s" class="col-md-12 control-label" Visible="false"></asp:TextBox>
                                                                     <asp:Label runat="server" ID="lblOHCost2h" class="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOHCost2h" class="col-md-12 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:TextBox AutoCompleteType="Disabled" ID="txtOHCostHD" ReadOnly="true"  CssClass="form-control" runat="server" placeholder="Overhead Cost"></asp:TextBox>
                                                                   
                                                                        </div>
                                                                        <div class="col-md-4 form-group" style="margin-right: 0px; margin-bottom: 0px; margin-left: 0px;">
                                                                           <asp:Label runat="server" ID="lblPerUnitValue1s" style="width: 139px;" class="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPerUnitValue1s" class="col-md-12 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:Label runat="server" ID="lblPerUnitValue2h" style="width: 139px;" class="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPerUnitValue2h" class="col-md-12 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:TextBox AutoCompleteType="Disabled" ID="txtuintOhCost" ReadOnly="true"  CssClass="form-control" runat="server" placeholder="@ Per Unit Value"></asp:TextBox>
                                                                            
                                                                        </div>
                                                                    </asp:Panel>
                                                                        
                                                                   </center>
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
                                                        <div class="tab-content no-space">
                                                           <%-- <asp:Panel ID="step3" runat="server" Style="display: none">

                                                                <div class="portlet box yellow-crusta">
                                                                    <div class="portlet-title">
                                                                        <div class="caption">
                                                                            <i class="fa fa-gift"></i>
                                                                            <asp:Label runat="server" ID="lblOverheadCost1s" Style="color: White; width: 179px; padding-top: 0px; font-size: 14px; padding-left: 0px; font-weight: lighter;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOverheadCost1s" Style="width: 181px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:Label runat="server" Style="width: 125px; font-size: 14px;" ForeColor="White" ID="lblOverheadCost2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOverheadCost2h" Style="width: 139px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                        <div class="tools">
                                                                            <a id="A3" runat="server" href="javascript:;" class="collapse"></a>
                                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                            <a href="javascript:;" class="reload"></a>
                                                                            <a href="javascript:;" class="remove"></a>
                                                                        </div>
                                                                        <div class="actions btn-set">
                                                                            <asp:LinkButton ID="ButtonAdd" runat="server" CssClass="btn btn-icon-only green"  Style="width: 60px; padding-top: 4px; padding-bottom: 4px; height: 30px;"><i class="fa fa-plus"> Add</i></asp:LinkButton></td>
                                                                                 <asp:LinkButton ID="buttonUpdate" runat="server" CssClass="btn btn-sm red" OnClick="buttonUpdate_Click" Style="width: 80px; height: 30px;"><i class="fa fa-edit"> Update</i></asp:LinkButton></td>
                                                                                 <asp:LinkButton ID="btndiscorohcost" runat="server" CssClass="btn default" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;">Exit</asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="portlet-body">
                                                                        <table class="table table-bordered" id="datatable1">
                                                                            <thead class="repHeader">
                                                                                <tr>
                                                                                    <th style="width: 5%;">
                                                                                        <asp:Label ID="Label5" runat="server" Text="#"></asp:Label></th>
                                                                                    <th style="width: 18%;">
                                                                                        <asp:Label runat="server" ID="lblOverheadCost11s" Style="width: 148px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" Style="width: 144px;" ID="txtOverheadCost11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <asp:Label runat="server" ID="lblOverheadCost12h" Style="width: 141px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOverheadCost12h" Style="width: 121px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </th>
                                                                                    <th style="width: 12%;">
                                                                                        <asp:Label runat="server" ID="lblOverheadAmount1s" Style="width: 158px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" Style="width: 160px;" ID="txtOverheadAmount1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <asp:Label runat="server" ID="lblOverheadAmount2h" Style="width: 150px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOverheadAmount2h" Style="width: 150px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </th>
                                                                                    <th style="width: 45%;">
                                                                                        <asp:Label runat="server" ID="lblNotes11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNotes11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <asp:Label runat="server" ID="lblNotes12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNotes12h" Style="width: 150px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </th>
                                                                                    <th style="width: 10%;">
                                                                                        <asp:Label runat="server" ID="lblAccountID1s" Style="width: 106px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" Style="width: 119px;" ID="txtAccountID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <asp:Label runat="server" ID="lblAccountID2h" Style="width: 120px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAccountID2h" Style="width: 122px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </th>
                                                                                    <th style="width: 5%;">
                                                                                        <asp:Label runat="server" ID="lblDel1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDel1s" Style="width: 61px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <asp:Label runat="server" ID="lblDel2h" Style="width: 48px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDel2h" Style="width: 86px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <asp:Repeater ID="Repeater1" runat="server" >
                                                                                    <ItemTemplate>
                                                                                        <tr class="gradeA">
                                                                                            <td>
                                                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddlOHType" runat="server" CssClass="form-control">
                                                                                                </asp:DropDownList>
                                                                                                <asp:Label ID="lblOHType" Visible="false" Text='<%#Eval("OVERHEADCOSTID") %>' runat="server"></asp:Label>
                                                                                                <asp:Label ID="lbltransid" Visible="false" Text='<%#Eval("MYTRANSID") %>' runat="server"></asp:Label>
                                                                                                <asp:Label ID="lblMyid" Visible="false" Text='<%#Eval("MYID") %>' runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtOHAmntLocal" runat="server" AutoCompleteType="Disabled" Text='<%#Eval("NEWCOST") %>' CssClass="form-control" placeholder="Overhead Amount"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtOHNote" runat="server" AutoCompleteType="Disabled" TextMode="MultiLine" Text='<%#Eval("Note") %>' CssClass="form-control" placeholder="Notes" MaxLength="300"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtOHAMT" runat="server" AutoCompleteType="Disabled" CssClass="form-control" Enabled="false" placeholder="Account ID"></asp:TextBox>
                                                                                            </td>

                                                                                            <td class="center">
                                                                                                <asp:LinkButton ID="lnkbtndelete1" runat="server" CommandName="DeleteOverHed" CommandArgument='<%#Eval("MYTRANSID") + "," +Eval("MYID") %>' OnClientClick="return confirm('Do you want to delete overhead cost?')">
                                                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="images/deleteRec.png" />
                                                                                                </asp:LinkButton></td>
                                                                                        </tr>
                                                                                    </ItemTemplate>

                                                                                </asp:Repeater>
                                                                                <tfoot>
                                                                                    <tr>

                                                                                        <th colspan="2">Total</th>
                                                                                      
                                                                                        <th style="color: green">
                                                                                            <asp:Label ID="lblcr" runat="server" Text="0.00"></asp:Label></th>
                                                                                        <th colspan="4"></th>
                                                                                    </tr>
                                                                                </tfoot>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                                <asp:HiddenField ID="hiddOHCostTab" runat="server" Value="" />
                                                            </asp:Panel>--%>
                                                            <asp:Panel ID="step4" runat="server" class="tab-pane active">
                                                                <asp:UpdatePanel ID="itemsupde" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="portlet box red-pink">
                                                                            <div class="portlet-title">
                                                                                <div class="caption">
                                                                                    <i class="fa fa-gift"></i>
                                                                                    <asp:Label ID="lblitems" Style="font-size: 14px;" runat="server" Text="Pending Payment Invoices"></asp:Label>
                                                                                </div>
                                                                                <div class="actions btn-set">
                                                                                    <asp:Label ID="lblitemsearch" runat="server" Visible="false"></asp:Label>
                                                                                    <asp:LinkButton ID="btnAddItemsIn" runat="server" CssClass="btn default btn-xs purple" Visible="false" OnClick="btnAddnew_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px; display: none;"><i class="fa fa-plus"> Add New</i></asp:LinkButton>
                                                                                    <asp:LinkButton ID="btnAddDT" runat="server" Visible="false" CssClass="btn btn-icon-only green" ValidationGroup="submititems" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px; display: none;">Save</asp:LinkButton>
                                                                                    <asp:LinkButton ID="btndiscartitems" runat="server" Visible="false" CssClass="btn default" OnClick="btnExit1_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;">Exit</asp:LinkButton>
                                                                                    <asp:CheckBox ID="chReFill" AutoPostBack="true" runat="server" Text="Auto Fill" OnCheckedChanged="chReFill_CheckedChanged1" />
                                                                                </div>
                                                                            </div>
                                                                            <asp:Panel ID="pnlSuccessMsg123" runat="server" Visible="false">
                                                                                <div class="alert alert-danger alert-dismissable">
                                                                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                                    <asp:Label ID="lblMsg123" runat="server"></asp:Label>
                                                                                </div>
                                                                            </asp:Panel>

                                                                           


                                                                            <asp:Panel ID="ListItems" class="portlet-body" runat="server">
                                                                                <%--<div class="table-scrollable">
                                                                                    <table class="table table-striped table-bordered table-hover">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th scope="col">
                                                                                                    <asp:Label ID="Label13" runat="server" Text="#"></asp:Label></th>
                                                                                                <th scope="col">
                                                                                                    <asp:Label runat="server" ID="lblProductCodeProductName1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductCodeProductName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label runat="server" ID="lblProductCodeProductName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductCodeProductName2h" Style="width: 189px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                </th>
                                                                                                <th scope="col">
                                                                                                    <asp:Label ID="Label17" runat="server" Text="Tran ID & Batch NO."></asp:Label>
                                                                                                </th>
                                                                                                <th scope="col">
                                                                                                    <asp:Label runat="server" ID="lblQuantity11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantity11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label runat="server" ID="lblQuantity12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantity12h" Style="width: 78px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                </th>
                                                                                                <th scope="col">
                                                                                                    <asp:Label runat="server" ID="lblUnitPrice1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPrice1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label runat="server" ID="lblUnitPrice2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPrice2h" Style="width: 77px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                </th>
                                                                                                <th scope="col">
                                                                                                    <asp:Label runat="server" ID="lblTax11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTax11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label runat="server" ID="lblTax12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTax12h" Style="width: 104px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                </th>
                                                                                                 <th scope="col">
                                                                                                    <asp:Label runat="server" ID="lblTotal11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotal11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                                                                            <asp:Label ID="Label27" runat="server" Visible="false" Text='<%#getprodname(Convert .ToInt32 ( Eval("MYTRANSID"))) %>'></asp:Label>
                                                                                                            <asp:Label ID="lblProductNameItem" runat="server" Text='<%#Eval("ENTRYDATE") %>'></asp:Label>
                                                                                                            <asp:Label ID="lblDiscription" Visible="false" runat="server" Text='<%#Eval("transid")+"-"+Eval("USERBATCHNO") %>'></asp:Label>
                                                                                                            
                                                                                                            <asp:Label ID="lblUNITPRICE" Visible="false" runat="server" Text='<%#Eval("TOTAMT") %>'></asp:Label>
                                                                                                            <asp:Label ID="lblDISAMT" Visible="false" runat="server" Text='<%#Eval("AmtPaid") %>'></asp:Label>
                                                                                                            <asp:Label ID="lblDISPER" Visible="false" runat="server" Text='<%#Eval("AmtPaid") %>'></asp:Label>
                                                                                                            
                                                                                                        </td>
                                                                                                        <td style="text-align: center">
                                                                                                            <asp:Label ID="lbltransisbatchno" runat="server" Text='<%#Eval("transid")+" & "+Eval("USERBATCHNO") %>'></asp:Label>
                                                                                                        </td>
                                                                                                        <td style="text-align: center">
                                                                                                            <asp:Label ID="lblDisQnty" runat="server" Text='<%#Eval("TOTAMT") %>' />
                                                                                                        </td>
                                                                                                        <td style="text-align: center">
                                                                                                            <asp:Label ID="lblOHAmnt" runat="server" Text='<%#Eval("AmtPaid") %>' />
                                                                                                        </td>

                                                                                                        <td style="text-align: center" class="list-inline">
                                                                                                            <asp:TextBox ID="TextBox9" Style="width: 100px;" class="form-control" runat="server"></asp:TextBox>                                                                                                           
                                                                                                        </td>
                                                                                                       
                                                                                                        <td style="text-align: center">
                                                                                                            <asp:LinkButton ID="lnkbtnItem" runat="server" CommandName="editDT" CommandArgument='<%# Eval("MYTRANSID") +","+ Eval("transid") +","+ Eval("transsubid") + "," + Eval("TenantID") %>'>
                                                                                                                <asp:Image ID="Image1" runat="server" ImageUrl="images/editRec.png" />
                                                                                                            </asp:LinkButton>
                                                                                                            <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDT" CommandArgument='<%# Eval("MYTRANSID") +","+ Eval("transid") + "," + Eval("transsubid") + "," + Eval("TenantID") %>' OnClientClick="return confirm('Do you want to delete product?')">
                                                                                                                <asp:Image ID="Image2" runat="server" ImageUrl="images/deleteRec.png" />
                                                                                                            </asp:LinkButton>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                            </asp:Repeater>
                                                                                            <tfoot>
                                                                                                <tr>

                                                                                                    <th colspan="3">Total</th>                                                                                                   
                                                                                                    <th style="color: green">
                                                                                                        <asp:Label ID="lblqtytotl" runat="server" Text="0"></asp:Label></th>
                                                                                                    <th style="color: green">
                                                                                                        <asp:Label ID="lblUNPtotl" runat="server" Text="0.00"></asp:Label></th>
                                                                                                    <th style="color: green">
                                                                                                        <asp:Label ID="lblTextotl" runat="server" Text="0.00"></asp:Label></th>
                                                                                                    <th style="color: green">
                                                                                                        <asp:Label ID="lblTotatotl" runat="server" Text="0.00"></asp:Label></th>
                                                                                                    <th colspan="2"></th>

                                                                                                </tr>
                                                                                            </tfoot>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>--%>
                                                                                 <div class="table-scrollable">
                                                                                    <table class="table table-striped table-bordered table-hover">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th scope="col">
                                                                                                    <asp:Label ID="Label13" runat="server" Text="#"></asp:Label></th>
                                                                                                <th scope="col">
                                                                                                    <asp:Label runat="server" ID="lblProductCodeProductName1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductCodeProductName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label runat="server" ID="lblProductCodeProductName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductCodeProductName2h" Style="width: 189px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                </th>
                                                                                                <th scope="col">
                                                                                                    <asp:Label ID="Label17" runat="server" Text="TranID & Batch NO."></asp:Label>
                                                                                                </th>
                                                                                                <th scope="col">
                                                                                                    <asp:Label runat="server" ID="lblQuantity11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantity11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label runat="server" ID="lblQuantity12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantity12h" Style="width: 78px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                </th>
                                                                                                <th scope="col">
                                                                                                    <asp:Label runat="server" ID="lblUnitPrice1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPrice1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label runat="server" ID="lblUnitPrice2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPrice2h" Style="width: 77px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                </th>
                                                                                                <th scope="col">
                                                                                                    <asp:Label runat="server" ID="lblTax11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTax11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label runat="server" ID="lblTax12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTax12h" Style="width: 104px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                </th>
                                                                                                <th scope="col">Action
                                                                                                    <asp:Label runat="server" ID="lblTotal11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotal11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    <asp:Label runat="server" ID="lblTotal12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotal12h" Style="width: 85px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                </th>
                                                                                               
                                                                                                 
                                                                                                <%-- <th scope="col">Action
                                                                                                </th>--%>
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
                                                                                                            <asp:Label ID="lblMyProdID" runat="server" Visible="false" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                                            <asp:Label ID="Label27" runat="server" Visible="false" Text='<%#getprodname(Convert .ToInt32 ( Eval("MyProdID"))) %>'></asp:Label>
                                                                                                            <asp:Label ID="lblProductNameItem" runat="server" Text='<%# Entrydate(Convert .ToInt32(Eval("MYTRANSID"))) %>'></asp:Label>
                                                                                                             <asp:Label ID="lblMYTRANSID" Visible="false" runat="server" Text='<%# Eval("MYTRANSID") %>'></asp:Label>
                                                                                                            <asp:Label ID="lblDiscription" Visible="false" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label>
                                                                                                            <%--<asp:Label ID="lblUOM" Visible="false" runat="server" Text='<%#Eval("UOM") %>'></asp:Label>--%>
                                                                                                            <asp:Label ID="lblUNITPRICE" Visible="false" runat="server" Text='<%#Eval("UNITPRICE") %>'></asp:Label>
                                                                                                            <asp:Label ID="lblDISAMT" Visible="false" runat="server" Text='<%#Eval("DISAMT") %>'></asp:Label>
                                                                                                            <asp:Label ID="lblDISPER" Visible="false" runat="server" Text='<%#Eval("DISPER") %>'></asp:Label>
                                                                                                            <asp:Label ID="lblTAXAMT" Visible="false" runat="server" Text='<%#Eval("TAXAMT") %>'></asp:Label>
                                                                                                        </td>
                                                                                                        <td style="text-align: center">
                                                                                                            <asp:Label ID="lbltransisbatchno" runat="server" Text='<%#gettransidandBatch(Convert.ToInt32(Eval("MYTRANSID"))) %>'></asp:Label>
                                                                                                        </td>
                                                                                                         <td style="text-align: center">
                                                                                                            <asp:Label ID="lblTotalCurrency" runat="server" Text='<%#Eval("AMOUNT") %>' />
                                                                                                        </td>
                                                                                                        <td style="text-align: center">
                                                                                                            <asp:Label ID="lblDisQnty" runat="server" Text='<%# Amountpaids(Convert.ToInt32( Eval("MYTRANSID"))) %>' />
                                                                                                            <asp:Label ID="lblTaxDis" runat="server" Visible="false" Text='<%#Eval("TAXPER") %>' />
                                                                                                        </td>
                                                                                                       <%-- <td style="text-align: center">
                                                                                                            <asp:Label ID="lblOHAmnt" runat="server" Text='<%#Eval("UNITPRICE") %>' />
                                                                                                        </td>

                                                                                                        <td style="text-align: center">
                                                                                                            <asp:Label ID="lblTaxDis" runat="server" Text='<%#Eval("TAXPER") %>' />
                                                                                                        </td>--%>
                                                                                                        <td style="text-align: center" class="list-inline">
                                                                                                            <asp:TextBox class="form-control clsTxtToCalculate" ID="txtpaidamounr" Style="width: 100px;"  runat="server" Text="0"></asp:TextBox>     <%-- AutoPostBack="true" OnTextChanged="txtpaidamounr_TextChanged" --%>                                                                                                     
                                                                                                              <asp:RangeValidator class="list-inline" runat="server" Type="Double" MinimumValue="0" MaximumValue='<%# Amountpaids(Convert.ToInt32( Eval("MYTRANSID"))) %>' ControlToValidate="txtpaidamounr" ForeColor="red" ValidationGroup="submit" ErrorMessage="Invalid!"  />
                                                                                                            
                                                                                                        </td>
                                                                                                      
                                                                                                        <td style="text-align: center">
                                                                                                            
                                                                                                            <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDT" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") %>' OnClientClick="return confirm('Do you want to delete product?')">
                                                                                                                <asp:Image ID="Image2" runat="server" ImageUrl="images/deleteRec.png" />
                                                                                                            </asp:LinkButton>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                            </asp:Repeater>
                                                                                            <tfoot>
                                                                                                <tr>

                                                                                                    <th colspan="2">Total</th>
                                                                                                    
                                                                                                    <%-- <th>Amount</th>--%>
                                                                                                    <th style="color: green">
                                                                                                        <asp:Label ID="lblqtytotl" runat="server" Visible="false" Text="0"></asp:Label></th>
                                                                                                    <th style="color: green">
                                                                                                        <asp:Label ID="lblUNPtotl" runat="server" Text="0.00"></asp:Label></th>
                                                                                                    <th style="color: green">
                                                                                                        <asp:Label ID="lblTextotl" runat="server" Text="0.00"></asp:Label></th>
                                                                                                    <th style="color: green">
                                                                                                        
                                                                                                        <asp:Label ID="lblTotatotl" runat="server" Text="0.00"></asp:Label></th>
                                                                                                    <th colspan="2"></th>

                                                                                                </tr>
                                                                                            </tfoot>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                               <%--  <table class="table table-bordered" id="datatable1" style="margin-left: 390px; width: 494px; margin-top: -35px;">

                                                                                    <tbody>
                                                                                        <asp:LinkButton ID="btnaddamunt" runat="server" OnClick="btnaddamunt_Click" Style="margin-left: 350px; margin-top: 0px; margin-bottom: 9px;">
                                                                                            <asp:Image ID="Image2" runat="server" ImageUrl="images/plus.png" />
                                                                                        </asp:LinkButton>
                                                                                       <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                                                                                            <ItemTemplate>
                                                                                                <tr class="gradeA">
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="drppaymentMeted" runat="server" Style="width: 124px;" CssClass="form-control">
                                                                                                        </asp:DropDownList>
                                                                                                       
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <b><font size="1px"> <asp:Label runat="server" slyle="font-size: 9px;" ID="Label19" Text="Card/Reference# , Bank Approval #" ></asp:Label></font></b>
                                                                                                       <asp:TextBox ID="txtCardReferenceBank" runat="server"  AutoCompleteType="Disabled" placeholder="Card/Reference# , Bank Approval #" CssClass="form-control tags"></asp:TextBox>


                                                                                                    </td>
                                                                                                    <td>
                                                                                                         
                                                                                                        <asp:TextBox ID="txtammunt" runat="server" Style="width: 104px;"></asp:TextBox>
                                                                                                         
                                                                                                       
                                                                                                    </td>


                                                                                                </tr>
                                                                                          <%--  </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </tbody>
                                                                                </table>--%>

                                                                            </asp:Panel>


                                                                        </div>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <%--<asp:PostBackTrigger ControlID="txtQuantity" />--%>
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="pull-left" style="margin-left: 32%">

                                                <asp:Button ID="btnSubmit" class="btn blue" runat="server" Text="Draft Invoice" ValidationGroup="submit1"  OnClick="btnSubmit_Click" /><%--OnClientClick="return showWarningToast()"--%>
                                                <asp:Button ID="btnConfirmOrder" class="btn yellow" runat="server" Text="Confirm Invoice"  OnClick="btnConfirmOrder_Click" />
                                                <asp:Button ID="btnExit" class="btn default" runat="server" Visible="false" Text="Exit" OnClick="btnExit_Click" OnClientClick="return confirm('Are you sure you want to Exit data?');" />
                                                <asp:Button ID="btnPrint" class="btn red" OnClientClick="return PrintPanel();" runat="server" Text="Print" />
                                                <asp:HyperLink data-toggle="modal" role="button" ID="hlPeri" onclick="getEmail(this.id)" Text="Send Mail" NavigateUrl="#form_Mail" runat="server">
                                                </asp:HyperLink>
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </div>


                                            <div id="form_Mail" class="modal fade" tabindex="-1" role="dialog" style="display: block; margin-top: -106.5px; width: 700px;">
                                                <div class="modal-dialog">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="close(this);disButton(this);">&times;</button><h4 class="modal-title"><i class="icon-paragraph-justify2"></i>
                                                                <asp:Label runat="server" ID="lblSendMail1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSendMail1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <asp:Label runat="server" ID="lblSendMail2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSendMail2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <asp:Button ID="btnSendMail" CssClass="btn btn-primary" runat="server" OnClick="btnSendMail_Click1" Text="Send Mail" />
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
                                                                    <asp:Label runat="server" ID="lblStatus1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStatus1s" Style="width: 89px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                                            <asp:Label ID="lblspnoo" runat="server" Text='<%# Eval("Status").ToString() =="RDPOCT" ? "Final" : "Draft "%>'></asp:Label></td>

                                                                        <td class="center">
                                                                            <asp:LinkButton ID="lnkbtnPurchase_Order" runat="server" CommandName="Edit" CommandArgument='<%# Eval("MYTRANSID") %>'>
                                                                                <asp:Image ID="Image1" runat="server" ImageUrl="images/editRec.png" />
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Do you want to delete purchase quotation?')" CommandArgument='<%# Eval("MYTRANSID") %>'>
                                                                                <asp:Image ID="Image2" runat="server" ImageUrl="images/deleteRec.png" />
                                                                            </asp:LinkButton>
                                                                            <%-- <asp:LinkButton ID="LinkButton14" runat="server">
                                                                               <i class="fa fa-print"></i>
                                                                            </asp:LinkButton>--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton class="btn btn-lg blue" CommandName="btnprient" CommandArgument='<%# Eval("MYTRANSID") %>' ID="Print" style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px;font-size: 12px;" runat="server">Print <i class="fa fa-print"></i></asp:LinkButton>
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
