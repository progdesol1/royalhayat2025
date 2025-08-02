<%@ Page Title="" Language="C#" MasterPageFile="~/Sales/Sales_Master.Master" AutoEventWireup="true" CodeBehind="invoicepos.aspx.cs" Inherits="Web.Sales.invoicepos" %>

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

    <script type="text/javascript">
        function openModalsmall2(x) {
            $('#small2').modal('show');
            document.getElementById("lblmsgpop2").innerHTML = x;
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
                                        <asp:Label ID="lblmodev" runat="server" Text="" style="font-size:18px;color:#a94442;"></asp:Label>
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



                                        <asp:Button ID="btndiscatr" class="btn default" runat="server" Text="Exit" OnClick="btnExit_Click" OnClientClick="return confirm('Are you sure you want to Exit data?');" />
                                        <asp:Button ID="btnEditLable" runat="server" class="btn red" OnClick="btnEditLable_Click" Text="Update Label" />
                                        <%--<asp:Button ID="Button3" runat="server" Text="Toast" OnClick="Button3_Click" />--%>
                                        &nbsp;
                                        <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="/assets/global/img/flags/us.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="/assets/global/img/flags/ae.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="/assets/global/img/flags/fr.png" /></asp:LinkButton>

                                    </div>
                                    &nbsp;
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" class="actions btn-set" style="margin: 0px 10px 0px 0px;">
                                        <ContentTemplate>
                                            <asp:Button ID="BTNsAVEcONFsA" Visible="false" class="btn blue" runat="server" Text="Draft Invoice" OnClientClick="return showWarningToast()" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnSaveData" class="btn red" runat="server" ValidationGroup="submit" Text="Save and Print" OnClick="btnPrint_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                                                                    <asp:Panel ID="Panel1" runat="server">
                                                                        <div class="input-group">
                                                                            <asp:DropDownList ID="DRPLocationSearch" CssClass="form-control select2me" runat="server"></asp:DropDownList>
                                                                            <%--<asp:TextBox ID="txtLocationSearch" runat="server" CssClass="form-control" autocomplete="off" placeholder="Custmer Name" OnTextChanged="txtLocationSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtLocationSearch" ServiceMethod="GetCounrty" CompletionInterval="1000" EnableCaching="FALSE" CompletionSetCount="20"
                                                                                MinimumPrefixLength="1" OnClientItemSelected="ace_itemCoutry" DelimiterCharacters=";, :" FirstRowSelected="false"
                                                                                runat="server" />--%>
                                                                            <asp:HiddenField ID="HiddenField3" runat="server" />
                                                                            <span class="input-group-btn">
                                                                                <%--<uc1:Custmer runat="server" ID="Custmer" />--%>
                                                                                <asp:LinkButton ID="LinkAccount1" runat="server" data-toggle="tooltip" ToolTip="Add"><i class="icon-plus" style="color:black;padding-left: 4px;font-size: 25px;" ></i></asp:LinkButton>
                                                                                <%--<span class="input-group-btn" style="border-left-width: 0px; margin-left: 0px; left: 32px; top: -18px;">
                                                                                    <asp:Button ID="BTnEdit" runat="server" CssClass="btn yellow" ValidationGroup="submit1" Text="Edit" OnClick="BTnEdit_Click" />
                                                                                </span>--%>
                                                                            </span>
                                                                        </div>
                                                                        <%--<asp:DropDownList ID="ddlSupplier" CssClass="form-control select2me" runat="server" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>--%>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DRPLocationSearch" ErrorMessage=" Select Supplier  Required." CssClass="Validation" ValidationGroup="submit1" ForeColor="red" InitialValue="0"></asp:RequiredFieldValidator>
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
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator9" runat="server" ErrorMessage="Add Reference Required" ForeColor="red" ControlToValidate="drpRefnstype" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </p>
                      <h4><b><asp:Label runat="server" ID="lblReferenceType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReferenceType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></b></h4>
                                                            <h4><b><asp:Label runat="server" ID="lblReferenceNo1s" style="width: 154px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReferenceNo1s" class="col-md-4 control-label" Visible="false"></asp:TextBox></b></h4>
                                                            <p>
                                                                 
                                                               <asp:TextBox Style="width: 300px;"  ID="txtrefresh" runat="server" class="form-control" maxlength="70"></asp:TextBox>
                                                                 <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator10" runat="server" ErrorMessage="Add Reference Required" ForeColor="red" ControlToValidate="txtrefresh" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                             
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
                                                        <div class="tab-content no-space">

                                                            <asp:Panel ID="step4" runat="server" class="tab-pane active">
                                                                <asp:UpdatePanel ID="itemsupde" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Panel ID="panelRed" runat="server" DefaultButton="btnAddDT">
                                                                            <div class="portlet box red-pink">
                                                                                <div class="portlet-title">
                                                                                    <div class="caption">
                                                                                        <i class="fa fa-gift"></i>
                                                                                        <asp:Label ID="lblitems" Style="font-size: 14px;" runat="server" Text="Items"></asp:Label>
                                                                                        (
                                <asp:Label runat="server" ID="lblProductCount"></asp:Label>


                                                                                        <%--<asp:Label ID="lblMode" runat="server" Text="User Cotrol"></asp:Label>--%>
                            )
                            <asp:Label ID="lblSaveCount" runat="server" Text="0 Item Save"></asp:Label>
                                                                                        <asp:Label ID="lblmodeitem" runat="server" Text="" style="font-size:18px;color:#a94442;"></asp:Label>
                                                                                    </div>
                                                                                    <div class="actions btn-set">
                                                                                        <asp:Label ID="lblitemsearch" runat="server" Visible="false"></asp:Label>
                                                                                        <%--<asp:LinkButton ID="btnAddItemsIn" runat="server" CssClass="btn default btn-xs purple" Visible="false" OnClick="btnAddnew_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;"><i class="fa fa-plus"> Add New</i></asp:LinkButton>--%>
                                                                                        <asp:LinkButton ID="btnAddDT" runat="server" CssClass="btn btn-icon-only green" ValidationGroup="submititems" Visible="false" OnClick="btnAddDT_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;">Item Save</asp:LinkButton>

                                                                                        <asp:LinkButton ID="btndiscartitems" runat="server" CssClass="btn default" OnClick="btndiscartitems_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;">Exit</asp:LinkButton>

                                                                                    </div>
                                                                                </div>


                                                                                <div class="portlet-body" style="padding-top: 10px;">
                                                                                    <asp:Panel ID="pnlSuccessMsg123" runat="server" Visible="false">
                                                                                        <div class="alert alert-danger alert-dismissable">
                                                                                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                                            <asp:Label ID="lblMsg123" runat="server"></asp:Label>
                                                                                        </div>
                                                                                    </asp:Panel>
                                                                                    <div class="tabbable">
                                                                                        <div class="tab-content no-space">
                                                                                            <div class="tab-pane active" id="tab_general2">
                                                                                                <%--<uc1:multitransaction runat="server" id="MultiTransaction" />--%>
                                                                                                <asp:Panel ID="penalAddDTITem" runat="server">
                                                                                                    <div class="form-group" style="margin-bottom: 0px;">
                                                                                                        <div class="col-md-12">
                                                                                                            <asp:Label class="col-md-2 control-label" ID="lblmultiserialize" runat="server" Visible="false" Style="top: -10px;">
                                                                                                                <asp:LinkButton ID="LinkButton2" class="btn default green-stripe" runat="server">Serialized</asp:LinkButton>
                                                                                                                <asp:Panel ID="pneSerial" runat="server" Style="height: 75%; overflow: auto; display: none">
                                                                                                                    <%-- <asp:HyperLink ID="lnkClose" runat="server">--%>
                                                                                                                    <%--</asp:HyperLink>--%>
                                                                                                                    <div class="row" style="position: fixed; left: 25%; width: 40%; top: 1%;">
                                                                                                                        <div class="col-md-12">
                                                                                                                            <div class="portlet box grey-cascade">
                                                                                                                                <div class="portlet-title">
                                                                                                                                    <div class="caption">
                                                                                                                                        <i class="fa fa-globe"></i>
                                                                                                                                        <asp:Label runat="server" ID="lblSerialization1s" ForeColor="White" Text="Serialization" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSerialization1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                        <asp:Label runat="server" ID="lblSerialization2h" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSerialization2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                                                                                    </div>
                                                                                                                                    <div class="tools">
                                                                                                                                        <asp:Label ID="lbltotalqty" runat="server"></asp:Label>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                                <div id="Div2" runat="server" class="portlet-body">
                                                                                                                                    <div class="tabbable">
                                                                                                                                        <div class="tab-content no-space">
                                                                                                                                            <div class="tab-pane active">
                                                                                                                                                <div class="portlet-body">
                                                                                                                                                    <table class="table table-striped table-bordered table-hover" id="sample_2">

                                                                                                                                                        <thead class="repHeader">
                                                                                                                                                            <tr>
                                                                                                                                                                <th>
                                                                                                                                                                    <asp:Label ID="Label5" runat="server" Text="#" meta:resourcekey="lblSNResource1"></asp:Label></th>

                                                                                                                                                                <th>
                                                                                                                                                                    <asp:Label runat="server" ID="lblSerializationNo1s" Style="width: 145px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSerializationNo1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                                    <asp:Label runat="server" ID="lblSerializationNo2h" Style="width: 145px;" Text="Serialization No" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSerializationNo2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                                                                                                                                            <asp:Label ID="lbltidser" Visible="false" runat="server" Text='<%# Eval("TenentID") %>'></asp:Label>
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
                                                                                                                                                                            <asp:CheckBox ID="cbslectsernumber" runat="server"></asp:CheckBox>
                                                                                                                                                                            <%--OnCheckedChanged="cbslectsernumber_CheckedChanged" AutoPostBack="true"--%>
                                                                                                                                                                        </td>
                                                                                                                                                                    </tr>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:ListView>
                                                                                                                                                        </tbody>
                                                                                                                                                    </table>

                                                                                                                                                    <div class="form-group" style="width: 350px; padding-left: 20px;">
                                                                                                                                                        <asp:TextBox ID="tags_2" runat="server" CssClass="form-control tags"></asp:TextBox>
                                                                                                                                                    </div>

                                                                                                                                                    <div class="form-actions noborder">
                                                                                                                                                        <asp:LinkButton ID="LinkButton3" class="btn green-haze modalBackgroundbtn-circle" OnClick="LinkButton3_Click" runat="server"> Save</asp:LinkButton>
                                                                                                                                                        <asp:Button ID="Button10" runat="server" class="btn btn-default" Text="Cancel" />
                                                                                                                                                    </div>
                                                                                                                                                </div>
                                                                                                                                            </div>
                                                                                                                                        </div>
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
                                                                                                                <asp:Panel ID="pnlMultiUom" runat="server" Style="height: 75%; overflow: auto; display: none">

                                                                                                                    <div class="row" style="position: fixed; top: 30px; left: 20%">
                                                                                                                        <div class="col-md-12">
                                                                                                                            <div class="portlet box purple">
                                                                                                                                <div class="portlet-title">
                                                                                                                                    <div class="caption">
                                                                                                                                        <i class="fa fa-globe"></i>
                                                                                                                                        <asp:Label runat="server" ID="lblMultiUOM1s" class="col-md-4 control-label" Text="Multi UOM" Style="width: 137px;" ForeColor="White"></asp:Label><asp:TextBox runat="server" ID="txtMultiUOM1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl1" runat="server" Style="padding-left: 100px;"></asp:Label>
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
                                                                                                                                                    <asp:Label runat="server" ID="lblUOM1s" Text="UOM" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOM1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                    <asp:Label runat="server" ID="lblUOM2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUOM2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                </th>
                                                                                                                                                <th style="background-color: #E08283; color: #fff">
                                                                                                                                                    <asp:Label runat="server" ID="lblUomNewQty1s" Text="UOM New Qty" Style="width: 141px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUomNewQty1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                                                                                                                            <asp:Label ID="LblColoername" runat="server" Text='<%#getrecodtypename(Convert.ToInt32(Eval("UOM")) , Convert.ToInt32(Eval("MyProdID"))) %>'></asp:Label>
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
                                                                                                                <asp:Panel ID="panelPershibal" runat="server" Style="height: 75%; overflow: auto; display: none">
                                                                                                                    <div class="row" style="position: fixed; top: 30px; left: 20%">
                                                                                                                        <div class="col-md-12">
                                                                                                                            <div class="portlet box purple">
                                                                                                                                <div class="portlet-title">
                                                                                                                                    <div class="caption">
                                                                                                                                        <i class="fa fa-globe"></i>
                                                                                                                                        <asp:Label runat="server" ID="lblPerishable1s" Text="Perishable" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPerishable1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl2" runat="server" Style="padding-left: 50px;"></asp:Label>
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
                                                                                                                                                                    <asp:TextBox ID="txtnewqty" Style="width: 109px;" class="form-control" runat="server"></asp:TextBox>

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
                                                                                                                <asp:Panel ID="pnlMultiColoer" runat="server" Style="height: 75%; overflow: auto; display: none">

                                                                                                                    <div class="row" style="position: fixed; top: 30px; left: 20%">
                                                                                                                        <div class="col-md-12">
                                                                                                                            <div class="portlet box yellow">
                                                                                                                                <div class="portlet-title">
                                                                                                                                    <div class="caption">
                                                                                                                                        <i class="fa fa-globe"></i>
                                                                                                                                        <asp:Label runat="server" ID="lblMultiColoer1s" Text="Multi Colors" Style="width: 160px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiColoer1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl3" runat="server"></asp:Label>
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
                                                                                                                                                    <asp:Label runat="server" ID="lblMultiColoer11s" Text="Multi Colors" Style="width: 112px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiColoer11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                    <asp:Label runat="server" ID="lblMultiColoer12h" Style="width: 112px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiColoer12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                </th>

                                                                                                                                                <th style="background-color: #E08283; color: #fff">
                                                                                                                                                    <asp:Label runat="server" ID="lblColoerNewQty1s" Text="Colors New Qty" Style="width: 146px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtColoerNewQty1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                                                                                                                            <asp:Label ID="LblColoername" runat="server" Text='<%# getrecodtypename(Convert.ToInt32( Eval("COLORID")),Convert.ToInt32( Eval("MyProdID")))%>'></asp:Label>
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
                                                                                                                <asp:Panel ID="pnlMultiSize" runat="server" Style="height: 75%; overflow: auto; display: none">

                                                                                                                    <div class="row" style="position: fixed; top: 30px; left: 20%">
                                                                                                                        <div class="col-md-12">
                                                                                                                            <div class="portlet box purple">
                                                                                                                                <div class="portlet-title">
                                                                                                                                    <div class="caption">
                                                                                                                                        <i class="fa fa-globe"></i>
                                                                                                                                        <asp:Label runat="server" ID="lblMultiSize1s" Text="Multi Size" Style="width: 133px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiSize1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl4" runat="server"></asp:Label>
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
                                                                                                                                                    <asp:Label runat="server" ID="lblSize1s" Text="Size" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSize1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                    <asp:Label runat="server" ID="lblSize2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSize2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                </th>
                                                                                                                                                <th style="background-color: #E08283; color: #fff">
                                                                                                                                                    <asp:Label runat="server" ID="lblSizeNewQty1s" Text="Size New Qty" Style="width: 142px; color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSizeNewQty1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                                                                                                                            <asp:Label ID="LblColoername" runat="server" Text='<%# getrecodtypename(Convert.ToInt32( Eval("SIZECODE")), Convert.ToInt32( Eval("MyProdID")))%>'></asp:Label>
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
                                                                                                                <asp:Panel ID="panelMultibin" runat="server" Style="height: 75%; overflow: auto; display: none">
                                                                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                                        <ContentTemplate>
                                                                                                                            <div class="row" style="position: fixed; top: 30px; left: 20%">
                                                                                                                                <div class="col-md-12">
                                                                                                                                    <div class="portlet box yellow">
                                                                                                                                        <div class="portlet-title">
                                                                                                                                            <div class="caption">
                                                                                                                                                <i class="fa fa-globe"></i>
                                                                                                                                                <asp:Label runat="server" ID="lblMultiBin1s" Text="Multi Bin" Style="width: 129px;" ForeColor="White" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMultiBin1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><asp:Label ID="lbl5" runat="server"></asp:Label>
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
                                                                                                                                                            <asp:Label runat="server" ID="lblBin1s" Text="Bin" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBin1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                            <asp:Label runat="server" ID="lblBin2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBin2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                                                        </th>
                                                                                                                                                        <th style="background-color: #E08283; color: #fff">
                                                                                                                                                            <asp:Label runat="server" ID="lblQty1s" Text="Qty" Style="color: #fff" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQty1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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

                                                                                                                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                                                                                        <ProgressTemplate>
                                                                                                                            <div class="overlay">
                                                                                                                                <div style="z-index: 1000; margin-left: 350px; margin-top: 200px; opacity: 1; -moz-opacity: 1;">
                                                                                                                                    <img src="../assets/admin/layout4/img/loading-spinner-blue.gif" />
                                                                                                                                    &nbsp;please wait...
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </ProgressTemplate>
                                                                                                                    </asp:UpdateProgress>
                                                                                                                </asp:Panel>
                                                                                                                <cc1:ModalPopupExtender ID="ModalPopupExtender6" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button3" Enabled="True" PopupControlID="panelMultibin" TargetControlID="LinkButton4"></cc1:ModalPopupExtender>
                                                                                                            </label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </asp:Panel>
                                                                                                <div class="form-body">


                                                                                                    <div class="form-group">
                                                                                                        <div class="col-md-12">
                                                                                                            <asp:Label runat="server" ID="lblProduct1s" class="col-md-2 control-label" Text="Product"></asp:Label><asp:TextBox runat="server" ID="txtProduct1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                            <div class="col-md-4">

                                                                                                                <asp:Panel ID="Panel2" DefaultButton="btnserchAdvans" runat="server" class="input-group">

                                                                                                                    <asp:TextBox ID="txtserchProduct" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Search" MaxLength="250">
                                                                                                                    </asp:TextBox>
                                                                                                                    <span class="input-group-btn"></span>
                                                                                                                    <asp:LinkButton ID="btnserchAdvans" CssClass="btn btn-icon-only yellow" runat="server" Style="margin-top: -23px; padding-left: 0px; margin-left: 5px; margin-bottom: 7px;" OnClick="btnserchAdvans_Click1"> <%--OnClientClick="showProgress()"--%>
                                                                                                             <i class="fa fa-search" ></i>
                                                                                                                    </asp:LinkButton>
                                                                                                                </asp:Panel>

                                                                                                            </div>
                                                                                                            <div class="col-md-6">
                                                                                                                <asp:DropDownList ID="ddlProduct" CssClass="form-control select2me" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlProduct" InitialValue="0" ErrorMessage=" Select Product  Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="red"></asp:RequiredFieldValidator>
                                                                                                            </div>

                                                                                                            <asp:Label runat="server" ID="lblProduct2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProduct2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group">
                                                                                                        <div class="col-md-12">
                                                                                                            <div class="col-md-12">
                                                                                                                <asp:TextBox ID="txtDescription" runat="server" AutoCompleteType="Disabled" CssClass="form-control" TextMode="MultiLine" placeholder="Description" Rows="2" Columns="5" MaxLength="250"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </div>

                                                                                                    </div>
                                                                                                    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">

                                            <ContentTemplate>--%>
                                                                                                    <div class="form-group">
                                                                                                        <div class="col-md-6">
                                                                                                            <asp:Label runat="server" ID="lblUnitofMeasure1s" class="col-md-3 control-label" Text="Unit Of Measure"></asp:Label><asp:TextBox runat="server" ID="txtUnitofMeasure1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                            <div class="col-md-6">
                                                                                                                <asp:DropDownList ID="ddlUOM" CssClass="form-control" OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged" AutoPostBack="true" Enabled="false" runat="server"></asp:DropDownList>
                                                                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator12" ForeColor="red" runat="server" InitialValue="0" ErrorMessage="Add to Uom" ControlToValidate="ddlUOM" ValidationGroup="submititems"></asp:RequiredFieldValidator>
                                                                                                                <%-- <asp:HiddenField ID="hidUOMId" runat="server" Value="" />
                                                            <asp:HiddenField ID="hidUOMText" runat="server" Value="" />--%>
                                                                                                            </div>
                                                                                                            <asp:Label runat="server" ID="lblUnitofMeasure2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitofMeasure2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        </div>
                                                                                                        <div class="col-md-6">
                                                                                                            <asp:Label runat="server" ID="lblQuantity1s" class="col-md-3 control-label" Text="Quantity"></asp:Label><asp:TextBox runat="server" ID="txtQuantity1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                            <div class="col-md-4">
                                                                                                                <asp:TextBox ID="txtQuantity" runat="server" AutoCompleteType="Disabled" OnTextChanged="txtQuantity_TextChanged1" AutoPostBack="true" MaxLength="7" placeholder="Quantity" CssClass="form-control quntity"></asp:TextBox>
                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtQuantity" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                                                <%--onblur="checkdate1(this)"--%>
                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtQuantity" ErrorMessage=" Quantity  Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="red"></asp:RequiredFieldValidator>
                                                                                                                <asp:HiddenField ID="hidMColor" runat="server" Value="" />
                                                                                                            </div>

                                                                                                            <asp:Label runat="server" ID="lblQuantity2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtQuantity2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                            <div class="col-md-4">
                                                                                                                <asp:Label runat="server" ID="lblAvailableQty"></asp:Label>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group">

                                                                                                        <div class="col-md-6">
                                                                                                            <asp:Label runat="server" ID="lblUnitPricelocal1s" class="col-md-3 control-label" Text="Unit Price (Local)"></asp:Label><asp:TextBox runat="server" ID="txtUnitPricelocal1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                            <div class="col-md-3">
                                                                                                                <%--<asp:TextBox ID="txtUPriceLocal" runat="server" onblur="checkdate1(this)" AutoCompleteType="Disabled" placeholder="Unit Price" CssClass="form-control lprice"></asp:TextBox>--%>
                                                                                                                <asp:TextBox ID="txtUPriceLocal" AutoPostBack="true" Text="0" runat="server" OnTextChanged="txtUPriceLocal_TextChanged1" AutoCompleteType="Disabled" placeholder="Unit Price" CssClass="form-control lprice"></asp:TextBox>
                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtUPriceLocal" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                                                <asp:HiddenField ID="hidUPriceLocal" runat="server" Value="" />
                                                                                                            </div>
                                                                                                            <asp:Label runat="server" ID="lblUnitPricelocal2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPricelocal2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                                                            <div class="col-md-4">
                                                                                                                <asp:Label runat="server" ID="lblDiscount1s" class="col-md-4 control-label" Text="Discount"></asp:Label><asp:TextBox runat="server" ID="txtDiscount1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                                <div class="col-md-5">
                                                                                                                    <asp:TextBox ID="txtDiscount" runat="server" AutoCompleteType="Disabled" Text="0" AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged1" placeholder="25 or 25%" CssClass="form-control dis"></asp:TextBox>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtDiscount" ValidChars="./%" FilterType="Custom, numbers" runat="server" />
                                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDiscount" ErrorMessage=" txtDiscount  Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="red"></asp:RequiredFieldValidator>
                                                                                                                </div>
                                                                                                                <asp:Label runat="server" ID="lblDiscount2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDiscount2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <%--<div class="col-md-2">
                                                                                                                <div class="inline-block popovers">
                                                                                                                   
                                                                                                                </div>
                                                                                                            </div>--%>
                                                                                                        </div>

                                                                                                        <div class="col-md-6">
                                                                                                            <asp:Label runat="server" ID="lblTotalCurrencyLocal1s" class="col-md-3 control-label" Text="Total Currency (Local)"></asp:Label><asp:TextBox runat="server" ID="txtTotalCurrencyLocal1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                            <div class="col-md-8">
                                                                                                                <asp:TextBox ID="txtTotalCurrencyLocal" AutoCompleteType="Disabled" runat="server" ReadOnly="true" CssClass="form-control" placeholder="Total Currency (Local)"></asp:TextBox>
                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtTotalCurrencyLocal" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                                                <asp:HiddenField ID="hidTotalCurrencyLocal" runat="server" Value="" />
                                                                                                            </div>
                                                                                                            <asp:Label runat="server" ID="lblTotalCurrencyLocal2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalCurrencyLocal2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        </div>

                                                                                                        <div class="col-md-6">
                                                                                                            <asp:Label ID="Label32" runat="server" Text="Loyality" CssClass="control-label col-md-3"></asp:Label>
                                                                                                            <div class="col-md-3">
                                                                                                                <asp:TextBox ID="txtLoyalityDis" runat="server" CssClass="form-control" Enabled="false" Text="0"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="col-md-6" style="padding-left: 20px; padding-top: 6px;">
                                                                                                            <asp:Label ID="loymsg" runat="server" Text="" Style="font-family: 'Courier New'; color: #ff6666; font-size: 14px;"></asp:Label>
                                                                                                        </div>
                                                                                                        <%-- <asp:Panel ID="pnlforentotal" runat="server" >
                                                                                                    <div class="col-md-6">
                                                                                                        <asp:Label runat="server" ID="lblTotalCurrencyForeign1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalCurrencyForeign1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        <div class="col-md-8">
                                                                                                            <asp:TextBox ID="txtTotalCurrencyForeign" AutoCompleteType="Disabled" runat="server" ReadOnly="true" CssClass="form-control" placeholder="Total Currency (Foreign)"></asp:TextBox>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtTotalCurrencyForeign" ValidChars="." FilterType="Custom, numbers" runat="server" />

                                                                                                            <asp:HiddenField ID="hidTotalCurrencyForeign" runat="server" Value="" />
                                                                                                        </div>
                                                                                                        <asp:Label runat="server" ID="lblTotalCurrencyForeign2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalCurrencyForeign2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    </div></asp:Panel>--%>
                                                                                                        <%--<asp:Panel ID="pnlforeign" runat="server" >
                                                                                                    <div class="col-md-6">
                                                                                                        <asp:Label runat="server" ID="lblUnitPriceForeign1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPriceForeign1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        <div class="col-md-8">
                                                                                                            <asp:TextBox ID="txtUPriceForeign" Enabled="false" onblur="checkdate1(this)" onchange="getLocalPrize()" AutoCompleteType="Disabled" runat="server" placeholder="Unit Price" CssClass="form-control uprice"></asp:TextBox>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtUPriceForeign" ValidChars="." FilterType="Custom, numbers" runat="server" />
                                                                                                        </div>
                                                                                                        <asp:Label runat="server" ID="lblUnitPriceForeign2h" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUnitPriceForeign2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    </div></asp:Panel>   --%>
                                                                                                    </div>
                                                                                                    <%--  </ContentTemplate>

                                        </asp:UpdatePanel>--%>


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
                                                                                </div>

                                                                            </div>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="ListITtemDT" class="portlet-body" runat="server">
                                                                            <div class="portlet box yellow-lemon">
                                                                                <div class="portlet-title">
                                                                                    <div class="caption">
                                                                                        <i class="fa fa-list"></i>
                                                                                        Sale Item List
                                                                                        <asp:Label ID="lblmode" runat="server" Text="" style="font-size:18px;color:#a94442;"></asp:Label>
                                                                                    </div>
                                                                                    <div class="actions btn-set">
                                                                                    </div>
                                                                                </div>
                                                                                <div class="portlet-body">
                                                                                    <div class="table-scrollable">
                                                                                        <table class="table table-striped table-bordered table-hover" border="1">
                                                                                            <thead>
                                                                                                <tr>
                                                                                                    <th scope="col">
                                                                                                        <asp:Label ID="Label13" runat="server" Text="#"></asp:Label></th>
                                                                                                    <th scope="col">
                                                                                                        <asp:Label runat="server" ID="lblProductCodeProductName1s" class="col-md-4 control-label" Text="Product Code"></asp:Label><asp:TextBox runat="server" ID="txtProductCodeProductName1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        <asp:Label runat="server" ID="lblProductCodeProductName2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtProductCodeProductName2h" Style="width: 189px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                    </th>
                                                                                                    <th scope="col">
                                                                                                        <asp:Label ID="Label17" runat="server" Text="Description" style="color:#428bca;"></asp:Label>
                                                                                                    </th>
                                                                                                    <th scope="col">
                                                                                                        <asp:Label ID="Label12" runat="server" Text="UOM" style="color:#428bca;"></asp:Label>
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
                                                                                                    <% if(lblmode.Text !="(View Only)"){ %>                                                                                   
                                                                                                    <th scope="col" colspan="2">
                                                                                                        <asp:Label runat="server" ID="Label26" class="col-md-4 control-label" Text="Action"></asp:Label></th>
                                                                                                    <%} %>
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
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblMYID" runat="server" Visible="false" Text='<%#Eval("MYID") %>'></asp:Label>
                                                                                                                <asp:Label ID="Label20" runat="server" Text='<%# getUom(Convert.ToInt32(Eval("UOM"))) %>'></asp:Label>
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
                                                                                                            <% if(lblmode.Text !="(View Only)"){ %>         
                                                                                                            <td style="text-align: center">
                                                                                                                <asp:LinkButton ID="lnkbtnItem" runat="server" CommandName="editDT" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") %>' Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;" class="btn btn-lg green">
                                                                                                                    <%--<asp:Image ID="Image1" runat="server" ImageUrl="../Sales/images/editRec.png" />--%> Edit <i class="fa fa-edit"></i>
                                                                                                                </asp:LinkButton>
                                                                                                            </td>
                                                                                                            <td style="text-align: center">
                                                                                                                <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDT" CommandArgument='<%# Eval("MYTRANSID") + "," + Eval("MYID") %>' OnClientClick="return confirm('Do you want to delete product?')" Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;" class="btn btn-lg red">
                                                                                                                    <%--<asp:Image ID="Image2" runat="server" ImageUrl="../Sales/images/deleteRec.png" />--%>Delete <i class="fa fa-remove"></i>
                                                                                                                </asp:LinkButton>
                                                                                                            </td>
                                                                                                            <%} %>
                                                                                                        </tr>
                                                                                                    </ItemTemplate>
                                                                                                </asp:Repeater>
                                                                                            </tbody>
                                                                                            <tfoot>
                                                                                                <tr>

                                                                                                    <th colspan="4">Total</th>
                                                                                                    <%-- <th>Amount</th>--%>
                                                                                                    <th style="color: green">
                                                                                                        <asp:Label ID="lblqtytotl" runat="server" Text="0"></asp:Label></th>
                                                                                                    <th style="color: green">
                                                                                                        <asp:Label ID="lblUNPtotl" runat="server" Text="0.00"></asp:Label></th>

                                                                                                    <th style="color: green">
                                                                                                        <asp:Label ID="lblTotatotl" runat="server" Text="0.00"></asp:Label></th>
                                                                                                    <th colspan="3"></th>

                                                                                                </tr>
                                                                                            </tfoot>

                                                                                        </table>
                                                                                    </div>
                                                                                    <table class="table table-bordered" id="datatable11" style="margin-left: 390px; width: 494px; margin-top: -35px;">

                                                                                        <tbody>
                                                                                            <asp:LinkButton ID="btnaddamunt" runat="server" OnClick="btnaddamunt_Click" Style="margin-left: 350px; margin-top: 0px; margin-bottom: 9px;">
                                                                                                <asp:Image ID="Image3" runat="server" ImageUrl="../Sales/images/plus.png" />
                                                                                            </asp:LinkButton>
                                                                                            <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                                                                                                <ItemTemplate>
                                                                                                    <tr class="gradeA" style="top: 0px;">
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="drppaymentMeted" runat="server" Style="width: 124px;" CssClass="form-control">
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:Label ID="lblOHType" Visible="false" Text='<%#Eval("PaymentTermsId") %>' runat="server"></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <b><font size="1px"> <asp:Label runat="server" slyle="font-size: 9px;" ID="Label191" Text="Card/Reference# , Bank Approval #" ></asp:Label></font></b>
                                                                                                            <asp:TextBox ID="txtrefresh" runat="server" Text='<%#Eval("ReferenceNo")+","+Eval("ApprovalID") %>' AutoCompleteType="Disabled" placeholder="Card/Reference# , Bank Approval #" CssClass="form-control tags"></asp:TextBox>
                                                                                                            <%--OnTextChanged="txtrefresh1_TextChanged"--%>

                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:TextBox ID="txtammunt" runat="server" Text='<%#Eval("Amount") %>' AutoCompleteType="Disabled" placeholder="Amount" Style="width: 104px;" CssClass="form-control"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <%--OnTextChanged="txtammunt1_TextChanged"--%>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                            </asp:Repeater>
                                                                                            <%--<div class="pull-right" style="margin-left: 40%">--%>
                                                                                            <%--<asp:LinkButton ID="LnkBtnSavePayTerm" runat="server" Visible="true" CssClass="btn btn-icon-only green pull-right" ValidationGroup="submititems" OnClick="LnkBtnSavePayTerm_Click" Style="width: 100px; padding-top: 4px; padding-bottom: 4px; height: 30px;">Save Payment</asp:LinkButton>--%>
                                                                                            <%--</div>--%>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>

                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="txtQuantity" EventName="TextChanged" />
                                                                        <asp:AsyncPostBackTrigger ControlID="ddlUOM" EventName="SelectedIndexChanged" />
                                                                        <asp:AsyncPostBackTrigger ControlID="lnkBTNSAVE" EventName="Click" />
                                                                        <asp:AsyncPostBackTrigger ControlID="btnAddDT" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="btnPenel" runat="server" Visible="true">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="pull-right" style="margin-left: 32%">
                                                            <asp:LinkButton ID="lnkBTNSAVE" runat="server" Enabled="false" CssClass="btn btn-icon-only green" ValidationGroup="submititems" OnClick="btnAddDT_Click" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;">Item Save</asp:LinkButton>
                                                            <asp:Button ID="btnSubmit" Visible="false" class="btn blue" runat="server" Text="Draft Invoice" OnClientClick="return showWarningToast()" OnClick="btnSubmit_Click" />
                                                            <asp:Button ID="btnConfirmOrder" Visible="false" class="btn yellow" runat="server" Text="Save" ValidationGroup="submit" OnClick="btnConfirmOrder_Click" />
                                                            <asp:Button ID="btnExit" class="btn default" runat="server" Visible="false" Text="Exit" OnClick="btnExit_Click" OnClientClick="return confirm('Are you sure you want to Exit data?');" />
                                                            <asp:Button ID="btnPrint" class="btn red" OnClick="btnPrint_Click" runat="server" Text="Save and Print" />
                                                            <%--OnClientClick="return PrintPanel();" yogesh 1704--%>

                                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

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
                                <div class="col-md-6" style="position: fixed; top: 30px; left: 20%">
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
                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator11" ForeColor="red" runat="server" ErrorMessage="Add Reference Required" ControlToValidate="drpRefnstype" ValidationGroup="S"></asp:RequiredFieldValidator>

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
                                        <asp:LinkButton ID="LinkShoall" runat="server" Text="Show All" CssClass="btn btn-sm default" OnClick="LinkButton11_Click"></asp:LinkButton>
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

                                                        <div class="col-md-8">
                                                        </div>
                                                        <div class="col-md-4" style="float: right; padding-right: 35px;">
                                                            <div class="col-md-11">
                                                                <asp:TextBox AutoCompleteType="Disabled" ID="txtallserchforview" CssClass="form-control"
                                                                    runat="server" placeholder="Advance search"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1" style="float: right;">
                                                                <asp:LinkButton ID="btnallserch" CssClass="btn btn-icon-only red" runat="server" OnClick="btnallserch_Click">
                                                                                 <i class="fa fa-search" ></i>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                        <thead class="repHeader">
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
                                                                <th style="width: 68px;">
                                                                    <asp:Label runat="server" ID="lblStatus1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStatus1s" Style="width: 89px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <asp:Label runat="server" ID="lblStatus2h" Style="width: 68px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStatus2h" Style="width: 102px;" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </th>

                                                                <th colspan="2">
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
                                                                            <asp:LinkButton ID="LinkButton111" runat="server" CommandName="Btnview" CommandArgument='<%# Eval("MYTRANSID") %>'>
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
                                                                        <td style="width: 50px;">
                                                                            <asp:LinkButton ID="lnkbtnPurchase_Order" Visible="false" runat="server" CommandName="Edit" CommandArgument='<%# Eval("MYTRANSID") %>' Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;" class="btn btn-lg green">
                                                                                <%--<asp:Image ID="Imagedelete" runat="server" ImageUrl="images/editRec.png" />--%>Edit <i class="fa fa-edit"></i>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width: 50px;">
                                                                            <asp:LinkButton ID="lnkbtndelete" Visible="false" runat="server" CommandName="Delete" OnClientClick="return confirm('Do you want to delete purchase quotation?')" CommandArgument='<%# Eval("MYTRANSID") %>' Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;" class="btn btn-lg red" >
                                                                                <%--<asp:Image ID="Image4" runat="server" ImageUrl="images/deleteRec.png" />--%>Delete <i class="fa fa-remove"></i>
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
                    <p id="lblmsgpop2" style="text-align: center; font-family: 'Courier New';"></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn default" data-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <panel id="pnlresponsive1" style="padding: 1px; background-color: #fff; border: 1px solid #000; display: none" runat="server">
<%--<panel id="responsive" class="modal fade" tabindex="-1" aria-hidden="true" runat="server">--%>

    <div class="modal-dialog" style="position:fixed;left:20%;top:10px">
        
									<div class="modal-content">
                                        <div style="left:93%; position:relative; top: 15px;">
                                             <%--<asp:ImageButton ID="ImageButton1" data-dismiss="modal" ImageUrl="../CRM/UserControl/Close.jpg" Height="25" Width="25" runat="server"></asp:ImageButton>--%>
                                            <%--<asp:Button ID="Button2" runat="server" data-dismiss="modal" class="btn default" />--%></div>
                                         <div class="modal-header">
											<h4 class="modal-title">Add Customer</h4>
										</div>
<asp:UpdatePanel ID="UpdatePanel4" runat="server">    
    <ContentTemplate>
										<div class="modal-body">
											<div class="slimScrollBar" style="height:250px" data-always-visible="1" data-rail-visible1="1">
												<div class="row">
                                                    <asp:Panel ID="PanelCustomer" runat="server" Visible="false">
                                                    <div class="col-md-12" >
                                                    <table class="table table-bordered" id="datatable1">
                                                        <thead>
                                                            <th>Customer</th>
                                                            <th>Mobile</th>
                                                            <th>Email ID</th>
                                                            <th>Bus Number </th>
                                                        </thead>
                                                        <tbody>
                                                            <asp:ListView ID="CustomerList" runat="server" OnItemCommand="CustomerList_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr style="font-size:12px;color:blue;">
                                                                        <td><%#Eval("COMPNAME1") %></td>
                                                                        <td><%#Eval("MOBPHONE") %></td>
                                                                        <td><%#Eval("EMAIL") %></td>
                                                                        <td><%#Eval("BUSPHONE1") %></td>
                                                                        <td><asp:LinkButton ID="btnEdit" runat="server" CssClass="btn green-haze " CommandName="Modify" CommandArgument='<%# Eval("COMPID") %>'>Edit</asp:LinkButton></td>
                                                                    <%--<td><asp:Label ID="LBLCName" runat="server" Text='<%#Eval("COMPNAME1") %>'></asp:Label></td>
                                                                    <td><asp:Label ID="LBLCmobile" runat="server" Text='<%#Eval("MOBPHONE") %>'></asp:Label></td>
                                                                    <td><asp:Label ID="LBLCEmail" runat="server" Text='<%#Eval("EMAIL") %>'></asp:Label></td>
                                                                    <td><asp:Label ID="LBLCBUSno" runat="server" Text='<%#Eval("BUSPHONE1") %>'></asp:Label></td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>                                                            
                                                        </tbody>
                                                    </table>

                                                    </div>
                                                        </asp:Panel>
                                                    <div class="col-md-12">														
														    <p>
                                                                <asp:TextBox runat="server" ID="txtserach" Visible="false" Enabled="false" ForeColor="Red" CssClass="col-md-6 form-control" BorderStyle="None" style="background-color:white;"></asp:TextBox>                                                                                           
														    </p> 
                                                        </div>
                                                    <div class="col-md-6">
														<h4>Customer Name <span class="required">* </span></h4>
														<p>
                                                            <asp:TextBox runat="server" ID="txtSupplierName" class="col-md-12 form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ErrorMessage="Supplier Name Required" ControlToValidate="txtSupplierName" ValidationGroup="S2"></asp:RequiredFieldValidator>--%>
                                                            
															
														</p> </div>
                                                     <div class="col-md-6">
                                                        <h4>Mobile NO <span class="required">* </span></h4>
														<p> <asp:TextBox runat="server" ID="txtMobileNO"  class="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtMobileNO" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator15" ForeColor="Red" runat="server" ErrorMessage="Supplier Name Required" ControlToValidate="txtMobileNO" ValidationGroup="S2"></asp:RequiredFieldValidator>--%>
                                                           
                                                            </p> </div>
                                                   
                                                     <div class="col-md-6">
                                                         <h4>Address <span class="required">* </span></h4>
														<p>  <asp:TextBox  ID="txtAddress1" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator8" ForeColor="Red" runat="server" ErrorMessage="Supplier Name Required" ControlToValidate="txtAddress1" ValidationGroup="S2"></asp:RequiredFieldValidator>--%></p> 
                                                    </div>
                                                    <div class="col-md-6">
														<h4>Email <%--<span class="required">* </span>--%></h4>
														<p>
															<asp:TextBox ID="txtEMAIL" runat="server"  class="col-md-12 form-control"></asp:TextBox>
                               <%-- <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" ForeColor="Red" runat="server" ErrorMessage="Supplier Name Required" ControlToValidate="txtEMAIL" ValidationGroup="S2"></asp:RequiredFieldValidator>--%>
                                                           
														</p></div> 
                                                    <div class="col-md-6">
                                                        <h4> Bus Phone <%--<span class="required">* </span>--%></h4>
														<p> <asp:TextBox runat="server" ID="txtBusPhone1"  class="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtBusPhone1" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator14" ForeColor="Red" runat="server" ErrorMessage="Supplier Name Required" ControlToValidate="txtBusPhone1" ValidationGroup="S2"></asp:RequiredFieldValidator>--%></p> 
                                                        
												</div> 

												</div> 
                                                


											</div> 

										</div>
                                          </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="txtSupplierName" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtMobileNO" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtEMAIL" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtBusPhone1" EventName="TextChanged" />
    </Triggers>
    
</asp:UpdatePanel>          
        
    <div class="modal-footer">
        <asp:LinkButton ID="lbButton1" class="btn green" runat="server" Visible="true" OnClick="lbButton1_Click" >Add New</asp:LinkButton>
        <asp:Button ID="Button7" runat="server" data-dismiss="modal" class="btn default" Text="Cancel" />
         </div>
									</div> </div> 
      
    </panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender8" runat="server" DynamicServicePath=""
        BackgroundCssClass="modalBackground" CancelControlID="Button7" Enabled="True"
        PopupControlID="pnlresponsive1" TargetControlID="LinkAccount1">
    </cc1:ModalPopupExtender>

</asp:Content>
