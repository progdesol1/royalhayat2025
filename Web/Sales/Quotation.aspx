<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="Quotation.aspx.cs" Inherits="Web.Sales.Quotation" %>

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
    <script type="text/javascript">

        function ace_itemCoutry(sender, e) {

            var HiddenField3 = $get('<%= HiddenField3.ClientID %>');

            HiddenField3.value = e.get_value();

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <script src="PrductJs/CommonPurchase.js"></script>
    <div  id="b" runat="server">
      
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">

                            <div class="portlet box green ">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>
                                      Sales Quotation
                                    </div>
                                    <div class="tools">
                                        <a id="shlinkProductDetails" runat="server" href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">

                                        <asp:Button ID="btnnewAdd" class="btn purple" runat="server" Visible="false" Text="Add New" OnClick="btnNew_Click" />
                                        <asp:Button ID="BTNsAVEcONFsA" class="btn blue" runat="server" Text="Draft Invoice" OnClientClick="return showWarningToast()" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnSaveData" class="btn yellow" runat="server" ValidationGroup="submit" Text="Confirm Invoice" OnClick="btnConfirmOrder_Click" />
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
                                                            <div class="col-md-2" style="padding-left: 25px; padding-top: 5px;">
                                                                <div class="radio-list">
                                                                    <asp:RadioButtonList ID="rbtPsle" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtPsle_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="1" Selected="True" Text="Credit" style="padding-right: 10px;"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="Cash" style="padding-right: 10px;"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                 <asp:Label runat="server" ID="Label18" Text="Salesman" class="col-md-3 control-label"></asp:Label>
                                                                 <div class="col-md-8">
                                                                     <asp:DropDownList ID="drpselsmen" CssClass="form-control select2me"  runat="server" ></asp:DropDownList>
                                                                 </div> 
                                                            </div>
                                                            
                                                            <div class="col-md-6" runat="server" id="Drpdown" visible="true">
                                                                <asp:Label runat="server" ID="lblSupplier1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSupplier1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                      <div class="input-group">
                                                                     <asp:TextBox ID="txtLocationSearch" runat="server" CssClass="form-control"  autocomplete="off" placeholder="Custmer Name" OnTextChanged="txtLocationSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtLocationSearch" ServiceMethod="GetCounrty" CompletionInterval="1000" EnableCaching="FALSE" CompletionSetCount="20"
                                                                        MinimumPrefixLength="1" OnClientItemSelected="ace_itemCoutry" DelimiterCharacters=";, :" FirstRowSelected="false"
                                                                        runat="server" />
                                                                     <asp:HiddenField ID="HiddenField3" runat="server" />
                                                                    <span class="input-group-btn"> <uc1:Custmer runat="server" ID="Custmer" /></span>
                                                                        </div> 
                                                                    <%--<asp:DropDownList ID="ddlSupplier" CssClass="form-control select2me" runat="server" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLocationSearch" ErrorMessage=" Select Supplier  Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
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
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblTransactionDate1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTransactionDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtOrderDate" CssClass="form-control"
                                                                        runat="server" placeholder="Transaction Date" onchange="checkdate(this)"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtOrderDate" Format="dd-MMM-yy" Enabled="True">
                                                                    </cc1:CalendarExtender>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblTransactionDate2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTransactionDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblNo1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNo1s" class="col-md-1 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtTraNoHD" ReadOnly="true" CssClass="form-control" runat="server" placeholder="Transaction Number"></asp:TextBox>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblNo2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNo2h" class="col-md-1 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblLocalForeign1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLocalForeign1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlLocalForeign" CssClass="form-control select2me" OnSelectedIndexChanged="ddlLocalForeign_SelectedIndexChanged" runat="server" onchange="selectcurrency()"></asp:DropDownList>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblLocalForeign2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLocalForeign2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblCurrency1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCurrency1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlCurrency" CssClass="form-control select2me" Enabled="false" runat="server"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCurrency" InitialValue="0" ErrorMessage=" Select Currency Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                    <%--<asp:LinkButton ID="lbtnCR"  runat="server" Text="Currency Rate"></asp:LinkButton>--%>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblCurrency2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCurrency2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblBatchNo1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBatchNo1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox AutoCompleteType="Disabled" ID="txtBatchNo" CssClass="form-control" runat="server" placeholder="Batch Number"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBatchNo" ErrorMessage="Batch Number Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblBatchNo2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBatchNo2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="lblProjectNo1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProjectNo1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlProjectNo" runat="server" CssClass="form-control select2me" ForeColor="#8e5fa2">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlProjectNo" InitialValue="0" ErrorMessage=" Select Project Name  Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                    <%--<asp:TextBox AutoCompleteType="Disabled" ID="txtProjectNo" CssClass="form-control" runat="server" placeholder="Project Number"></asp:TextBox>--%>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblProjectNo2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProjectNo2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtrefreshno" ErrorMessage="Reference Number Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblReference2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReference2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                <panel id="pnlresponsive" runat="server" cssclass="modalPopup" style="display: none; height: auto; overflow: auto">
       <div class="row">
                                                                            <div class="col-md-12">
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
                                                               <asp:TextBox Style="width: 300px;" ID="txtrefresh" runat="server" class="form-control" maxlength="50"></asp:TextBox>
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
                                                                <asp:Label runat="server" ID="lblTerms1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTerms1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpterms" CssClass="form-control select2me" runat="server"></asp:DropDownList>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblTerms2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTerms2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6" style="top: 20px;">
                                                                <asp:Label runat="server" ID="lblCRMActivity1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRMActivity1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlCrmAct" runat="server" CssClass="form-control select2me" ForeColor="#8e5fa2">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCrmAct" InitialValue="0" ErrorMessage=" Select CRM Activity  Required." CssClass="Validation" ValidationGroup="submit" ForeColor="White"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblCRMActivity2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCRMActivity2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6" style="top: -5px;">
                                                                <center> <div class="col-md-4 form-group" style="margin-right: 0px; margin-bottom: 0px; margin-left: 0px;" >
                                                                           <asp:Label runat="server" ID="lblOHCost1s" class="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOHCost1s" class="col-md-12 control-label" Visible="false"></asp:TextBox>
                                                                     <asp:Label runat="server" ID="lblOHCost2h" class="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOHCost2h" class="col-md-12 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:TextBox AutoCompleteType="Disabled" ID="txtOHCostHD" ReadOnly="true"  CssClass="form-control" runat="server" placeholder="Overhead Cost"></asp:TextBox>
                                                                   
                                                                        </div>
                                                                        <div class="col-md-4 form-group" style="margin-right: 0px; margin-bottom: 0px; margin-left: 0px;">
                                                                           <asp:Label runat="server" ID="lblPerUnitValue1s" style="width: 139px;" class="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPerUnitValue1s" class="col-md-12 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:Label runat="server" ID="lblPerUnitValue2h" style="width: 139px;" class="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPerUnitValue2h" class="col-md-12 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:TextBox AutoCompleteType="Disabled" ID="txtuintOhCost" ReadOnly="true"  CssClass="form-control" runat="server" placeholder="@ Per Unit Value"></asp:TextBox>
                                                                            
                                                                        </div>
                                                                        <div class="col-md-4 form-group" style="margin-right: 0px; margin-bottom: 0px; margin-left: 0px;">
                                                                            <asp:Label runat="server" ID="lblTotal1s" class="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotal1s" class="col-md-12 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:Label runat="server" ID="lblTotal2h" class="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotal2h" class="col-md-12 control-label" Visible="false"></asp:TextBox>
                                                                            <asp:TextBox AutoCompleteType="Disabled" ID="txttotxl" ReadOnly="true" CssClass="form-control" runat="server"  placeholder="Total Cost"></asp:TextBox>
                                                                            
                                                                        </div>
                                                                   </center>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-12">
                                                                <asp:Label runat="server" ID="lblNotes1s" class="col-md-1 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNotes1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                <div class="col-md-10" style="left: 32px;">
                                                                    <asp:TextBox ID="txtNoteHD" CssClass="form-control" MaxLength="550" runat="server" data-toggle="tooltip" ToolTip="Description" Style="resize: none" placeholder="Notes" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                                <asp:Label runat="server" ID="lblNotes2h" class="col-md-1 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNotes2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-6">
                                                                <asp:Label runat="server" ID="Label2" class="col-md-4 control-label" Text="Delivery Note"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:CheckBox ID="chbDeliNote" runat="server" />
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="tabbable">
                                                        <ul class="nav nav-tabs">
                                                            <li class="active">
                                                                <a id="A5" runat="server" alt="FormDisplay" class="lblDisPro2" style="color: #fff; background-color: #e08283" onclick="showhidepanelItems('ITEMS');">1. Items</a>
                                                            </li>
                                                            <li>
                                                                <a id="A4" runat="server" alt="FormDisplay" class="lblDisPro2" style="color: #fff; background-color: #f3c200" onclick="showhidepanelItems('OHCOST');">2. Overhead Cost</a></li>

                                                        </ul>
                                                        <div class="tab-content no-space">
                                                            <asp:Panel ID="step3" runat="server" Style="display: none">

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
                                                                            <asp:LinkButton ID="ButtonAdd" runat="server" CssClass="btn btn-icon-only green" OnClick="btnAdd_Click" Style="width: 60px; padding-top: 4px; padding-bottom: 4px; height: 30px;"><i class="fa fa-plus"> Add</i></asp:LinkButton></td>
                                                                                 <asp:LinkButton ID="buttonUpdate" runat="server" CssClass="btn btn-sm red" OnClick="buttonUpdate_Click" Style="width: 80px; height: 30px;"><i class="fa fa-edit"> Update</i></asp:LinkButton></td>
                                                                                 <asp:LinkButton ID="btndiscorohcost" runat="server" CssClass="btn default" OnClick="btndiscorohcost_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;">Exit</asp:LinkButton>
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
                                                                                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound" OnItemCommand="Repeater1_ItemCommand">
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
                                                                                        <%-- <th>Amount</th>--%>
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
                                                            </asp:Panel>
                                                            <asp:Panel ID="step4" runat="server" class="tab-pane active">
                                                                <asp:UpdatePanel ID="itemsupde" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="portlet box red-pink">
                                                                            <div class="portlet-title">
                                                                                <div class="caption">
                                                                                    <i class="fa fa-gift"></i>
                                                                                    <asp:Label ID="lblitems" Style="font-size: 14px;" runat="server" Text="Items"></asp:Label>                                                                                    
                                                                                    <asp:Label ID="lblitemsearch" runat="server" Visible="false" style="margin-left: 320px;"></asp:Label>
                                                                                </div>                                                                                
                                                                                <div class="actions btn-set">                                                                                    
                                                                                    <asp:LinkButton ID="btnAddItemsIn" runat="server" CssClass="btn default btn-xs purple" Visible="false" OnClick="btnAddnew_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;"><i class="fa fa-plus"> Add New</i></asp:LinkButton>
                                                                                    <asp:LinkButton ID="btnAddDT" runat="server" CssClass="btn btn-icon-only green" ValidationGroup="submititems" OnClick="btnAddDT_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;"><i class="fa fa-plus">Add Items</i></asp:LinkButton>
                                                                                    <asp:LinkButton ID="btndiscartitems" runat="server" CssClass="btn default" OnClick="btnExit1_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;">Exit</asp:LinkButton>

                                                                                </div>
                                                                            </div>
                                                                            <asp:Panel ID="pnlSuccessMsg123" runat="server" Visible="false">
                                                                                                <div class="alert alert-danger alert-dismissable">
                                                                                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                                                    <asp:Label ID="lblMsg123" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </asp:Panel>

                                                                            <asp:Panel ID="panelRed" class="portlet-body" runat="server">
                                                                                <div class="tabbable">
                                                                                    <div class="tab-content no-space">
                                                                                        <div class="tab-pane active" id="tab_general2">
                                                                                            
                                                                                            <div class="form-body">

                                                                                                <div class="form-group" style="margin-bottom: 0px;">
                                                                                                    <asp:Label class="col-md-2 control-label" ID="lblmultiserialize" runat="server" Visible="false" Style="top: -10px;">
                                                                                                        <asp:LinkButton ID="LinkButton2" class="btn default green-stripe" runat="server">Serialized</asp:LinkButton>
                                                                                                        <asp:Panel ID="pneSerial" runat="server" Style="height: 75%; overflow: auto; display: none">
                                                                                                            <%-- <asp:HyperLink ID="lnkClose" runat="server">--%>
                                                                                                            <%--</asp:HyperLink>--%>
                                                                                                            <div class="row">
                                                                                                                <div class="col-md-12">
                                                                                                                    <div class="portlet box grey-cascade">
                                                                                                                        <div class="portlet-title">
                                                                                                                            <div class="caption">
                                                                                                                                <i class="fa fa-globe"></i>
                                                                                                                                <asp:Label runat="server" ID="lblSerialization1s" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSerialization1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbltotalqty" runat="server"></asp:Label>
                                                                                                                                <asp:Label runat="server" ID="lblSerialization2h" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSerialization2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </div>

                                                                                                                        <div class="portlet-body">
                                                                                                                            <table class="table table-striped table-bordered table-hover" id="sample_2">

                                                                                                                                <thead class="repHeader">
                                                                                                                                    <tr>
                                                                                                                                        <th>
                                                                                                                                            <asp:Label ID="Label1" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>

                                                                                                                                        <th>
                                                                                                                                            <asp:Label runat="server" ID="lblSerializationNo1s" Style="width: 145px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSerializationNo1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            <asp:Label runat="server" ID="lblSerializationNo2h" Style="width: 145px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSerializationNo2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            <th>
                                                                                                                                                <asp:Label ID="Label14" runat="server" Text="Select"></asp:Label></th>
                                                                                                                                    </tr>
                                                                                                                                </thead>
                                                                                                                                <tbody>
                                                                                                                                    <asp:ListView ID="listSerial" runat="server">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <tr class="gradeA">
                                                                                                                                                <td>
                                                                                                                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lbltidser" Visible="false" runat="server" Text='<%# Eval("TenantID") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lblpidsril" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lblpdcodeser" Visible="false" runat="server" Text='<%# Eval("period_code") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lblmysysser" Visible="false" runat="server" Text='<%# Eval("MySysName") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lbluomser" Visible="false" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lbllocaser" Visible="false" runat="server" Text='<%# Eval("LocationID") %>'></asp:Label>
                                                                                                                                                </td>

                                                                                                                                                <td>
                                                                                                                                                    <asp:TextBox ID="txtlistSerial" class="form-control" runat="server" Enabled="false" Text='<%# Eval("Serial_Number") %>'></asp:TextBox>
                                                                                                                                                </td>
                                                                                                                                                <td>
                                                                                                                                                    <asp:CheckBox ID="cbslectsernumber" runat="server" OnCheckedChanged="cbslectsernumber_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:ListView>
                                                                                                                                </tbody>
                                                                                                                            </table>

                                                                                                                            <div class="form-group" style="width: 350px; padding-left: 20px;">
                                                                                                                                <asp:TextBox ID="tags_2" runat="server"  CssClass="form-control tags"></asp:TextBox>
                                                                                                                            </div>

                                                                                                                            <div class="form-actions noborder">
                                                                                                                                <asp:LinkButton ID="LinkButton3" class="btn green-haze modalBackgroundbtn-circle" OnClick="LinkButton3_Click" runat="server"> Save</asp:LinkButton>
                                                                                                                                <asp:Button ID="Button10" runat="server" class="btn btn-default" Text="Cancel" />
                                                                                                                            </div>
                                                                                                                        </div>

                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </asp:Panel>

                                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button10" Enabled="True" PopupControlID="pneSerial" TargetControlID="LinkButton2"></cc1:ModalPopupExtender>
                                                                                                    </asp:Label>
                                                                                                    <asp:Label class="col-md-2 control-label" ID="lblmultiuom" runat="server" Visible="false" Style="top: -10px;">
                                                                                                        <asp:LinkButton ID="LinkButton9" class="btn default red-stripe" runat="server">Multi UOM</asp:LinkButton>
                                                                                                        <asp:Panel ID="pnlMultiUom" runat="server" Style="display: none; height: auto; overflow: auto">

                                                                                                            <div class="row">
                                                                                                                <div class="col-md-12">
                                                                                                                    <div class="portlet box purple">
                                                                                                                        <div class="portlet-title">
                                                                                                                            <div class="caption">
                                                                                                                                <i class="fa fa-globe"></i>
                                                                                                                                <asp:Label runat="server" ID="lblMultiUOM1s" class="col-md-4 control-label" Style="width: 137px;" ForeColor="White"></asp:Label><asp:TextBox runat="server" ID="txtMultiUOM1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl1" runat="server" Style="padding-left: 100px;"></asp:Label>
                                                                                                                                <asp:Label runat="server" ID="lblMultiUOM2h" class="col-md-4 control-label" Style="width: 137px;" ForeColor="White"></asp:Label><asp:TextBox runat="server" ID="txtMultiUOM2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>

                                                                                                                        </div>
                                                                                                                        <div class="portlet-body">
                                                                                                                            <table class="table table-striped table-bordered table-hover">

                                                                                                                                <thead class="repHeader">
                                                                                                                                    <tr>
                                                                                                                                        <th>
                                                                                                                                            <asp:Label ID="Label22" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                                                                        <th>
                                                                                                                                            <asp:Label runat="server" ID="lblUOM1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOM1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            <asp:Label runat="server" ID="lblUOM2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOM2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        </th>
                                                                                                                                        <th style="background-color: #E08283; color: #fff">
                                                                                                                                            <asp:Label runat="server" ID="lblUomNewQty1s" Style="width: 141px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUomNewQty1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            <asp:Label runat="server" ID="lblUomNewQty2h" Style="width: 141px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUomNewQty2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        </th>

                                                                                                                                    </tr>
                                                                                                                                </thead>
                                                                                                                                <tbody>
                                                                                                                                    <asp:ListView ID="lidtUom" runat="server">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <tr class="gradeA">
                                                                                                                                                <td>
                                                                                                                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                                </td>
                                                                                                                                                <td>
                                                                                                                                                    <asp:Label ID="LblColoername" runat="server" Text='<%#getrecodtypename(Convert.ToInt32(  Eval("UOM"))) +" , "+Eval("OnHand") %>'></asp:Label>
                                                                                                                                                    <%-- <asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("CompniyAndContactID") %>'></asp:Label>--%>
                                                                                                                                                    <asp:Label ID="lbltidser" Visible="false" runat="server" Text='<%# Eval("TenantID") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lblpidsril" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lblpdcodeser" Visible="false" runat="server" Text='<%# Eval("period_code") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lblmysysser" Visible="false" runat="server" Text='<%# Eval("MySysName") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lbluomser" Visible="false" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lbllocaser" Visible="false" runat="server" Text='<%# Eval("LocationID") %>'></asp:Label>
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 70px;">
                                                                                                                                                    <asp:TextBox ID="txtuomQty" Style="width: 70px;" class="form-control" runat="server"></asp:TextBox>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:ListView>
                                                                                                                                </tbody>
                                                                                                                            </table>
                                                                                                                            <div class="form-actions noborder">
                                                                                                                                <asp:LinkButton ID="LinkButton1" class="btn green-haze modalBackgroundbtn-circle" OnClick="LinkButton1_Click" runat="server"> Save</asp:LinkButton>
                                                                                                                                <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                                <asp:Button ID="Button11" runat="server" class="btn btn-default" Text="Cancel" />

                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>

                                                                                                        </asp:Panel>
                                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button11" Enabled="True" PopupControlID="pnlMultiUom" TargetControlID="LinkButton9"></cc1:ModalPopupExtender>
                                                                                                    </asp:Label>
                                                                                                    <asp:Label class="col-md-2 control-label" ID="lblmultiperishable" runat="server" Visible="false" Style="top: -10px;">

                                                                                                        <asp:LinkButton ID="LinkButton5" class="btn default yellow-stripe" runat="server">Perishable</asp:LinkButton>
                                                                                                        <asp:Panel ID="panelPershibal" runat="server" Style="display: none; height: auto;">
                                                                                                            <div class="row">
                                                                                                                <div class="col-md-12">
                                                                                                                    <div class="portlet box purple">
                                                                                                                        <div class="portlet-title">
                                                                                                                            <div class="caption">
                                                                                                                                <i class="fa fa-globe"></i>
                                                                                                                                <asp:Label runat="server" ID="lblPerishable1s" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPerishable1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl2" runat="server" Style="padding-left: 50px;"></asp:Label>
                                                                                                                                <asp:Label runat="server" ID="lblPerishable2h" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPerishable2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>

                                                                                                                        </div>
                                                                                                                        <div class="portlet-body">
                                                                                                                            <asp:UpdatePanel ID="updatePerishebal" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>

                                                                                                                                    <table class="table table-striped table-bordered table-hover">

                                                                                                                                        <thead class="repHeader">
                                                                                                                                            <tr>
                                                                                                                                                <th>
                                                                                                                                                    <asp:Label ID="Label6" runat="server" Text="#"></asp:Label></th>
                                                                                                                                                <th style="width: 80px;">
                                                                                                                                                    <asp:Label ID="Label7" runat="server" Text="Batch No"></asp:Label></th>
                                                                                                                                                <th style="width: 70px;">
                                                                                                                                                    <asp:Label ID="Label8" runat="server" Text="Qty"></asp:Label></th>
                                                                                                                                                <th style="width: 113px;">
                                                                                                                                                    <asp:Label ID="Label9" runat="server" Text="Prodct Date"></asp:Label></th>
                                                                                                                                                <th style="width: 113px;">
                                                                                                                                                    <asp:Label ID="Label10" runat="server" Text="Expiry Date"></asp:Label></th>
                                                                                                                                                <th style="width: 109px;">
                                                                                                                                                    <asp:Label ID="Label11" runat="server" Text="Days to Destory"></asp:Label></th>
                                                                                                                                                 <th style="width: 109px;">
                                                                                                                                                    <asp:Label ID="Label15" runat="server" Text="Sell Qty"></asp:Label></th>
                                                                                                                                            </tr>
                                                                                                                                        </thead>
                                                                                                                                        <tbody>
                                                                                                                                            <asp:ListView ID="listperishibal" runat="server">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <tr class="gradeA">
                                                                                                                                                        <td>
                                                                                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                                        </td>
                                                                                                                                                        <td style="width: 80px;">
                                                                                                                                                            <asp:TextBox ID="txtBatchno" Style="width: 70px;" Enabled="false" Text='<%# Eval("BatchNo") %>' class="form-control" runat="server"></asp:TextBox>
                                                                                                                                                        </td>
                                                                                                                                                        <td style="width: 70px;">
                                                                                                                                                            <asp:TextBox ID="txtqty" Enabled="false" Style="width: 70px;" class="form-control" Text='<%# Eval("OnHand") %>' runat="server"></asp:TextBox>
                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtqty" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                                                                                        </td>
                                                                                                                                                        <td style="width: 113px;">
                                                                                                                                                            <asp:TextBox ID="txtproductdate" Enabled="false" Style="width: 113px;" class="form-control" Text='<%# Eval("ProdDate", "{0:dd-MMM-yyyy}") %>' runat="server"></asp:TextBox>
                                                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtproductdate" Format="dd-MMM-yy" Enabled="True">
                                                                                                                                                            </cc1:CalendarExtender>
                                                                                                                                                        </td>
                                                                                                                                                        <td style="width: 113px;">
                                                                                                                                                            <asp:TextBox ID="txtexpirydate" Enabled="false" Style="width: 113px;" class="form-control" Text='<%# Eval("ExpiryDate", "{0:dd-MMM-yyyy}") %>' runat="server"></asp:TextBox>
                                                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtexpirydate" Format="dd-MMM-yy" Enabled="True">
                                                                                                                                                            </cc1:CalendarExtender>
                                                                                                                                                        </td>
                                                                                                                                                        <td style="width: 109px;">
                                                                                                                                                            <asp:TextBox ID="txtlead2destroydate" Style="width: 109px;" Enabled="false" class="form-control" Text='<%# Eval("LeadDays2Destroy", "{0:dd-MMM-yyyy}") %>' runat="server"></asp:TextBox>
                                                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtlead2destroydate" Format="dd-MMM-yy" Enabled="True">
                                                                                                                                                            </cc1:CalendarExtender>
                                                                                                                                                        </td>
                                                                                                                                                         <td style="width: 109px;">
                                                                                                                                                            <asp:TextBox ID="txtnewqty" Style="width: 109px;"  class="form-control" runat="server"></asp:TextBox>
                                                                                                                                                            
                                                                                                                                                        </td>
                                                                                                                                                    </tr>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:ListView>
                                                                                                                                        </tbody>
                                                                                                                                    </table>
                                                                                                                                </ContentTemplate>

                                                                                                                            </asp:UpdatePanel>
                                                                                                                            <div class="form-actions noborder">
                                                                                                                                <asp:LinkButton ID="LinkButton10" class="btn green-haze modalBackgroundbtn-circle" runat="server" OnClick="LinkButton10_Click"> Save</asp:LinkButton>
                                                                                                                                <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                                <asp:Button ID="Button2" runat="server" OnClientClick="close(this)" class="btn btn-default" data-dismiss="modal" aria-hidden="true" Text="Cancel" />

                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="modal-footer">
                                                                                                            </div>

                                                                                                        </asp:Panel>
                                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button2" Enabled="True" PopupControlID="panelPershibal" TargetControlID="LinkButton5"></cc1:ModalPopupExtender>
                                                                                                    </asp:Label>
                                                                                                    <asp:Label class="col-md-2 control-label" ID="lblmulticolor" runat="server" Visible="false" Style="top: -10px;">
                                                                                                        <asp:LinkButton ID="LinkButton8" class="btn default blue-stripe" runat="server">Multi Color</asp:LinkButton>
                                                                                                        <asp:Panel ID="pnlMultiColoer" runat="server" Style="display: none; height: auto; overflow: auto">

                                                                                                            <div class="row">
                                                                                                                <div class="col-md-12">
                                                                                                                    <div class="portlet box yellow">
                                                                                                                        <div class="portlet-title">
                                                                                                                            <div class="caption">
                                                                                                                                <i class="fa fa-globe"></i>
                                                                                                                                <asp:Label runat="server" ID="lblMultiColoer1s" Style="width: 160px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiColoer1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl3" runat="server"></asp:Label>
                                                                                                                                <asp:Label runat="server" ID="lblMultiColoer2h" Style="width: 160px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiColoer2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                            <div class="actions btn-set">
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="portlet-body">
                                                                                                                            <table class="table table-striped table-bordered table-hover">

                                                                                                                                <thead class="repHeader">
                                                                                                                                    <tr>
                                                                                                                                        <th>
                                                                                                                                            <asp:Label ID="Label30" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                                                                        <th>
                                                                                                                                            <asp:Label runat="server" ID="lblMultiColoer11s" Style="width: 112px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiColoer11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            <asp:Label runat="server" ID="lblMultiColoer12h" Style="width: 112px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiColoer12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        </th>

                                                                                                                                        <th style="background-color: #E08283; color: #fff">
                                                                                                                                            <asp:Label runat="server" ID="lblColoerNewQty1s" Style="width: 146px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtColoerNewQty1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            <asp:Label runat="server" ID="lblColoerNewQty2h" Style="width: 146px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtColoerNewQty2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        </th>

                                                                                                                                    </tr>
                                                                                                                                </thead>
                                                                                                                                <tbody>
                                                                                                                                    <asp:ListView ID="listmulticoler" runat="server">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <tr class="gradeA">
                                                                                                                                                <td>
                                                                                                                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                                </td>
                                                                                                                                                <td>
                                                                                                                                                    <asp:Label ID="LblColoername" runat="server" Text='<%# getrecodtypename(Convert.ToInt32( Eval("COLORID")))+" , "+ Eval("OnHand")%>'></asp:Label>
                                                                                                                                                      <asp:Label ID="lblpidsril" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                                                                                      <asp:Label ID="lblcolerid" Visible="false" runat="server" Text='<%# Eval("COLORID") %>'></asp:Label>
                                                                                                                                                    <%-- <asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("CompniyAndContactID") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("RecTypeID") %>'></asp:Label>--%>
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 70px;">
                                                                                                                                                    <asp:TextBox ID="txtcoloerqty" Style="width: 70px;" class="form-control" runat="server"></asp:TextBox>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:ListView>
                                                                                                                                </tbody>
                                                                                                                            </table>
                                                                                                                            <div class="form-actions noborder">
                                                                                                                                <asp:LinkButton ID="linkMulticoler" class="btn green-haze modalBackgroundbtn-circle" runat="server" OnClick="linkMulticoler_Click"> Save</asp:LinkButton>
                                                                                                                                <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                                <asp:Button ID="Button5" runat="server" class="btn btn-default" Text="Cancel" />

                                                                                                                            </div>
                                                                                                                        </div>

                                                                                                                    </div>

                                                                                                                </div>
                                                                                                            </div>


                                                                                                        </asp:Panel>
                                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button5" Enabled="True" PopupControlID="pnlMultiColoer" TargetControlID="LinkButton8"></cc1:ModalPopupExtender>
                                                                                                    </asp:Label>
                                                                                                    <asp:Label class="col-md-2 control-label" ID="lblmultisize" runat="server" Visible="false" Style="top: -10px;">
                                                                                                        <asp:LinkButton ID="LinkButton6" class="btn default green-stripe" runat="server">Multi Size</asp:LinkButton>
                                                                                                        <asp:Panel ID="pnlMultiSize" runat="server" Style="display: none; height: auto; overflow: auto">

                                                                                                            <div class="row">
                                                                                                                <div class="col-md-12">
                                                                                                                    <div class="portlet box purple">
                                                                                                                        <div class="portlet-title">
                                                                                                                            <div class="caption">
                                                                                                                                <i class="fa fa-globe"></i>
                                                                                                                                <asp:Label runat="server" ID="lblMultiSize1s" Style="width: 133px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiSize1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl4" runat="server"></asp:Label>
                                                                                                                                <asp:Label runat="server" ID="lblMultiSize2h" Style="width: 133px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiSize2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                        <div class="portlet-body">
                                                                                                                            <table class="table table-striped table-bordered table-hover">

                                                                                                                                <thead class="repHeader">
                                                                                                                                    <tr>
                                                                                                                                        <th>
                                                                                                                                            <asp:Label ID="Label24" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                                                                        <th>
                                                                                                                                            <asp:Label runat="server" ID="lblSize1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSize1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            <asp:Label runat="server" ID="lblSize2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSize2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        </th>
                                                                                                                                        <th style="background-color: #E08283; color: #fff">
                                                                                                                                            <asp:Label runat="server" ID="lblSizeNewQty1s" Style="width: 142px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSizeNewQty1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            <asp:Label runat="server" ID="lblSizeNewQty2h" Style="width: 142px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSizeNewQty2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        </th>

                                                                                                                                    </tr>
                                                                                                                                </thead>
                                                                                                                                <tbody>
                                                                                                                                    <asp:ListView ID="listSize" runat="server">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <tr class="gradeA">
                                                                                                                                                <td>
                                                                                                                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                                </td>
                                                                                                                                                <td>
                                                                                                                                                    <asp:Label ID="LblColoername" runat="server" Text='<%#getrecodtypename(Convert.ToInt32( Eval("SIZECODE")))+" , "+ Eval("OnHand")%>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lblpidsril" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                                                                                      <asp:Label ID="lblcolerid" Visible="false" runat="server" Text='<%# Eval("SIZECODE") %>'></asp:Label>
                                                                                                                                                    <%--<asp:Label ID="LblMyid" Visible="false" runat="server" Text='<%# Eval("CompniyAndContactID") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("RecTypeID") %>'></asp:Label>--%>
                                                                                                                                                </td>
                                                                                                                                                <td style="width: 70px;">
                                                                                                                                                    <asp:TextBox ID="txtmultisze" Style="width: 70px;" class="form-control" runat="server"></asp:TextBox>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:ListView>
                                                                                                                                </tbody>
                                                                                                                            </table>
                                                                                                                            <div class="form-actions noborder">
                                                                                                                                <asp:LinkButton ID="LinkButton7" class="btn green-haze modalBackgroundbtn-circle" OnClick="LinkButton7_Click" runat="server"> Save</asp:LinkButton>
                                                                                                                                <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                                <asp:Button ID="Button9" runat="server" class="btn btn-default" Text="Cancel" />

                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>

                                                                                                        </asp:Panel>
                                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button9" Enabled="True" PopupControlID="pnlMultiSize" TargetControlID="LinkButton6"></cc1:ModalPopupExtender>
                                                                                                    </asp:Label>
                                                                                                    <label class="col-md-2 control-label" id="lblmultibinstore" runat="server" visible="false" style="top: -10px;">

                                                                                                        <asp:LinkButton ID="LinkButton4" class="btn default purple-stripe" runat="server">MultiBinStore</asp:LinkButton>
                                                                                                        <asp:Panel ID="panelMultibin" runat="server" Style="display: none; height: auto;">
                                                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                                <ContentTemplate>
                                                                                                                    <div class="row">
                                                                                                                        <div class="col-md-12">
                                                                                                                            <div class="portlet box yellow">
                                                                                                                                <div class="portlet-title">
                                                                                                                                    <div class="caption">
                                                                                                                                        <i class="fa fa-globe"></i>
                                                                                                                                        <asp:Label runat="server" ID="lblMultiBin1s" Style="width: 129px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiBin1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl5" runat="server"></asp:Label>
                                                                                                                                        <asp:Label runat="server" ID="lblMultiBin2h" Style="width: 129px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiBin2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        <asp:Label ID="tblProdid" Visible="false" runat="server"></asp:Label><asp:Label ID="lblQTY" Visible="false" runat="server"></asp:Label><asp:Label ID="Label36" Visible="false" runat="server"></asp:Label>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                                <div class="portlet-body">
                                                                                                                                    <%--<asp:LinkButton ID="linkBinAddNew" runat="server" OnClick="linkBinAddNew_Click">
                                                                                                                                        <asp:Image ID="Image4" runat="server" ImageUrl="images/plus.png" Style="float: right; padding-right: 25px; padding-bottom: 10px;" />
                                                                                                                                    </asp:LinkButton>--%>
                                                                                                                                    <table class="table table-striped table-bordered table-hover">

                                                                                                                                        <thead class="repHeader">
                                                                                                                                            <tr>
                                                                                                                                                <th>
                                                                                                                                                    <asp:Label ID="Label37" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>
                                                                                                                                                <th>
                                                                                                                                                    <asp:Label runat="server" ID="lblBin1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBin1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                    <asp:Label runat="server" ID="lblBin2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBin2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                </th>
                                                                                                                                                <th style="background-color: #E08283; color: #fff">
                                                                                                                                                    <asp:Label runat="server" ID="lblQty1s" Style="color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQty1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                    <asp:Label runat="server" ID="lblQty2h" Style="color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQty2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                            </tr>
                                                                                                                                        </thead>
                                                                                                                                        <tbody>
                                                                                                                                            <asp:ListView ID="listBin" runat="server">

                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <tr class="gradeA">
                                                                                                                                                        <td>
                                                                                                                                                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                                                        </td>
                                                                                                                                                        <td>
                                                                                                                                                            <%--<asp:DropDownList ID="dropBacthCode" runat="server" CssClass="table-group-action-input form-control input-medium">
                                                                                                                                                            </asp:DropDownList>--%>
                                                                                                                                                            <asp:Label ID="LblMyid" runat="server" Text='<%#getbinname(Convert.ToInt32( Eval("Bin_ID"))) + " , "+ Eval("OnHand") %>'></asp:Label>
                                                                                                                                                            <asp:Label ID="lblbinid" Visible="false" runat="server" Text='<%# Eval("Bin_ID") %>'></asp:Label>
                                                                                                                                                             <asp:Label ID="lblpidsril" Visible="false" runat="server" Text='<%# Eval("MyProdID") %>'></asp:Label>
                                                                                                                                                        </td>

                                                                                                                                                        <td style="width: 70px;">
                                                                                                                                                            <asp:TextBox ID="txtqty" Style="width: 70px;" class="form-control" runat="server"></asp:TextBox>
                                                                                                                                                        </td>

                                                                                                                                                    </tr>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:ListView>


                                                                                                                                        </tbody>
                                                                                                                                    </table>
                                                                                                                                    <div class="form-actions noborder">
                                                                                                                                        <asp:LinkButton ID="lbApproveIss" class="btn blue" runat="server" OnClick="lbApproveIss_Click">Save</asp:LinkButton>
                                                                                                                                        <asp:Button ID="Button3" runat="server" class="btn btn-default" data-dismiss="modal" aria-hidden="true" Text="Cancel" />
                                                                                                                                    </div>
                                                                                                                                </div>

                                                                                                                            </div>
                                                                                                                        </div>

                                                                                                                    </div>
                                                                                                                </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                        </asp:Panel>
                                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender6" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button3" Enabled="True" PopupControlID="panelMultibin" TargetControlID="LinkButton4"></cc1:ModalPopupExtender>
                                                                                                    </label>

                                                                                                </div>
                                                                                                <div class="form-group">
                                                                                                    <asp:Label runat="server" ID="lblProduct1s" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProduct1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                    <div class="col-md-4">

                                                                                                        <asp:Panel ID="Panel1" DefaultButton="btnserchAdvans" runat="server" class="input-group">
                                                                                                            <asp:TextBox ID="txtserchProduct" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Search" MaxLength="250">
                                                                                                            </asp:TextBox>
                                                                                                            <span class="input-group-btn"></span>
                                                                                                            <asp:LinkButton ID="btnserchAdvans" CssClass="btn btn-icon-only yellow" runat="server" Style="margin-top: -23px; padding-left: 0px; margin-left: 5px; margin-bottom: 7px;" OnClick="btnserchAdvans_Click">
                                                                                 <i class="fa fa-search" ></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </asp:Panel>

                                                                                                    </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <asp:DropDownList ID="ddlProduct" CssClass="form-control select2me" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlProduct" InitialValue="0" ErrorMessage=" Select Product  Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                                    </div>

                                                                                                    <asp:Label runat="server" ID="lblProduct2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProduct2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="form-group" style="margin-top: -18px;">
                                                                                                    <div class="col-md-12">
                                                                                                        <asp:TextBox ID="txtDescription" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TextMode="MultiLine" placeholder="Description" Rows="2" Columns="5" MaxLength="250"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="form-group">
                                                                                                    <div class="col-md-6">
                                                                                                        <asp:Label runat="server" ID="lblUnitofMeasure1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitofMeasure1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        <div class="col-md-8">
                                                                                                            <asp:DropDownList ID="ddlUOM" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                                                            <asp:HiddenField ID="hidUOMId" runat="server" Value="" />
                                                                                                            <asp:HiddenField ID="hidUOMText" runat="server" Value="" />
                                                                                                        </div>
                                                                                                        <asp:Label runat="server" ID="lblUnitofMeasure2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitofMeasure2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <asp:Label runat="server" ID="lblQuantity1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantity1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        <div class="col-md-8">
                                                                                                            <asp:TextBox ID="txtQuantity" runat="server" AutoCompleteType="Disabled" onblur="checkdate1(this)" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" MaxLength="7" placeholder="Quantity" CssClass="form-control quntity"></asp:TextBox>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtQuantity" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtQuantity" ErrorMessage=" Quantity  Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                                            <asp:HiddenField ID="hidMColor" runat="server" Value="" />
                                                                                                        </div>
                                                                                                        <asp:Label runat="server" ID="lblQuantity2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantity2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="form-group">
                                                                                                    <div class="col-md-6">
                                                                                                        <asp:Label runat="server" ID="lblUnitPriceForeign1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPriceForeign1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        <div class="col-md-8">
                                                                                                            <asp:TextBox ID="txtUPriceForeign" Enabled="false" onblur="checkdate1(this)" onchange="getLocalPrize()" AutoCompleteType="Disabled" runat="server" placeholder="Unit Price" CssClass="form-control uprice"></asp:TextBox>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtUPriceForeign" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                                        </div>
                                                                                                        <asp:Label runat="server" ID="lblUnitPriceForeign2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPriceForeign2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <asp:Label runat="server" ID="lblUnitPricelocal1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPricelocal1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        <div class="col-md-8">
                                                                                                            <asp:TextBox ID="txtUPriceLocal" runat="server" onblur="checkdate1(this)" AutoCompleteType="Disabled" placeholder="Unit Price" CssClass="form-control lprice"></asp:TextBox>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtUPriceLocal" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                                            <asp:HiddenField ID="hidUPriceLocal" runat="server" Value="" />
                                                                                                        </div>
                                                                                                        <asp:Label runat="server" ID="lblUnitPricelocal2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPricelocal2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="form-group">
                                                                                                     <div class="col-md-6">
                                                                                                        <asp:Label runat="server" ID="lblTax1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTax1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        <div class="col-md-8">
                                                                                                            <asp:TextBox ID="txtTax" runat="server" AutoCompleteType="Disabled" onblur="checkdate1(this)" CssClass="form-control tax" onchange="isNumericSourceOfIncomeallDecimal(this);isPercentage(this)" placeholder="Tax(%)"></asp:TextBox>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtTax" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                                        </div>
                                                                                                        <asp:Label runat="server" ID="lblTax2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTax2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <asp:Label runat="server" ID="lblDiscount1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDiscount1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        <div class="col-md-8">
                                                                                                            <asp:TextBox ID="txtDiscount" runat="server" AutoCompleteType="Disabled" Text="0" onblur="checkdate1(this)" onchange="isNumericWithPer(this)" placeholder="25 or 25%" CssClass="form-control dis"></asp:TextBox>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtDiscount" ValidChars="./%" FilterType="Custom, numbers" runat="server" />

                                                                                                        </div>
                                                                                                        <asp:Label runat="server" ID="lblDiscount2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDiscount2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                   
                                                                                                </div>
                                                                                                <div class="form-group">
                                                                                                    <div class="col-md-6">
                                                                                                        <asp:Label runat="server" ID="lblTotalCurrencyForeign1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalCurrencyForeign1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        <div class="col-md-8">
                                                                                                            <asp:TextBox ID="txtTotalCurrencyForeign" AutoCompleteType="Disabled" runat="server" ReadOnly="true" CssClass="form-control" placeholder="Total Currency (Foreign)"></asp:TextBox>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtTotalCurrencyForeign" ValidChars="." FilterType="Custom, numbers" runat="server" />

                                                                                                            <asp:HiddenField ID="hidTotalCurrencyForeign" runat="server" Value="" />
                                                                                                        </div>
                                                                                                        <asp:Label runat="server" ID="lblTotalCurrencyForeign2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalCurrencyForeign2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <asp:Label runat="server" ID="lblTotalCurrencyLocal1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalCurrencyLocal1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        <div class="col-md-8">
                                                                                                            <asp:TextBox ID="txtTotalCurrencyLocal" AutoCompleteType="Disabled" runat="server" ReadOnly="true" CssClass="form-control" placeholder="Total Currency (Local)"></asp:TextBox>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtTotalCurrencyLocal" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                                            <asp:HiddenField ID="hidTotalCurrencyLocal" runat="server" Value="" />
                                                                                                        </div>
                                                                                                        <asp:Label runat="server" ID="lblTotalCurrencyLocal2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalCurrencyLocal2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="form-group">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="col-md-8" style="top: 10px;">
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </asp:Panel>


                                                                            <asp:Panel ID="ListItems" class="portlet-body" runat="server">
                                                                                <div class="table-scrollable">
                                                                                <table class="table table-striped table-bordered table-hover">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th scope="col">
                                                                                                <asp:Label ID="Label13" runat="server" Text="#"></asp:Label></th>
                                                                                            <th scope="col">
                                                                                                <asp:Label runat="server" ID="lblProductCodeProductName1s"  class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductCodeProductName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                <asp:Label runat="server" ID="lblProductCodeProductName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductCodeProductName2h" Style="width: 189px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                            </th>
                                                                                            <th scope="col"><asp:Label ID="Label17"  runat="server" Text="Description"></asp:Label>
                                                                                            </th>
                                                                                            <th scope="col">
                                                                                                <asp:Label runat="server" ID="lblQuantity11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantity11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                <asp:Label runat="server" ID="lblQuantity12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantity12h" Style="width: 78px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                            </th>
                                                                                            <th scope="col">
                                                                                                <asp:Label runat="server" ID="lblUnitPrice1s"  class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPrice1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                <asp:Label runat="server" ID="lblUnitPrice2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPrice2h" Style="width: 77px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                            </th>
                                                                                            <th scope="col">
                                                                                                <asp:Label runat="server" ID="lblTax11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTax11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                <asp:Label runat="server" ID="lblTax12h"  class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTax12h" Style="width: 104px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                            </th>
                                                                                            <th scope="col">
                                                                                                <asp:Label runat="server" ID="lblTotal11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotal11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                <asp:Label runat="server" ID="lblTotal12h"  class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotal12h" Style="width: 85px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                            </th>
                                                                                            <th scope="col">
                                                                                                Action
                                                                                                </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="Repeater2_ItemCommand">
                                                                                            <ItemTemplate>
                                                                                                <tr >
                                                                                                    <td style="text-align: center">
                                                                                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td style="text-align: center">
                                                                                                        <asp:Label ID="Label27"  runat="server" Text='<%#getprodname(Convert .ToInt32 ( Eval("MyProdID"))) %>'></asp:Label>
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
                                                                                                        <asp:Label ID="lblTaxDis" runat="server" Text='<%#Eval("TAXPER") %>' />
                                                                                                    </td>
                                                                                                    <td style="text-align: center">
                                                                                                        <asp:Label ID="lblTotalCurrency" runat="server" Text='<%#Eval("AMOUNT") %>' />
                                                                                                    </td>
                                                                                                    <td style="text-align: center">
                                                                                                        <asp:LinkButton ID="lnkbtnItem" runat="server" CommandName="editDT" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") %>'>
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
                                                                                                    <asp:Label ID="lblTextotl" runat="server" Text="0.00"></asp:Label></th>
                                                                                                <th style="color: green">
                                                                                                    <asp:Label ID="lblTotatotl" runat="server" Text="0.00"></asp:Label></th>
                                                                                                <th colspan="2"></th>

                                                                                            </tr>
                                                                                        </tfoot>
                                                                                    </tbody>
                                                                                </table>
                                                                                    </div>
                                                                                <table class="table table-bordered" id="datatable1" style="margin-left: 390px; width: 494px; margin-top: -35px;">

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
                                                                                                        <asp:Label ID="lblOHType" Visible="false" Text='<%#Eval("PaymentTermsId") %>' runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txtrefresh" runat="server" Text='<%#Eval("ReferenceNo")+","+Eval("ApprovalID") %>' AutoCompleteType="Disabled" placeholder="ReferenceNo , ApprovalID" CssClass="form-control tags"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txtammunt" runat="server" Text='<%#Eval("Amount") %>' AutoCompleteType="Disabled" placeholder="Amount" Style="width: 104px;" CssClass="form-control"></asp:TextBox>
                                                                                                    </td>


                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </tbody>
                                                                                </table>

                                                                            </asp:Panel>


                                                                        </div>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="txtQuantity" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>

                                                </div>
                                            </asp:Panel>
                                            <div class="pull-left" style="margin-left: 32%">

                                                <asp:Button ID="btnSubmit" class="btn blue" runat="server" Text="Draft Invoice" OnClientClick="return showWarningToast()" OnClick="btnSubmit_Click" />
                                                <asp:Button ID="btnConfirmOrder" class="btn yellow" runat="server" Text="Confirm Invoice" ValidationGroup="submit" OnClick="btnConfirmOrder_Click" />
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
                                        Sales Quotation List
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
                                                                <th>
                                                                   </th>
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
                                                                            <asp:Label ID="lblspnoo" runat="server" Text='<%# Eval("Status").ToString() =="SO" ? "Final" : "Draft "%>'></asp:Label></td>
                                                                       
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
                                                                            <asp:LinkButton class="btn btn-lg blue" CommandName="btnprient" CommandArgument='<%# Eval("MYTRANSID") %>' ID="Print" runat="server" >Print <i class="fa fa-print"></i></asp:LinkButton>
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
