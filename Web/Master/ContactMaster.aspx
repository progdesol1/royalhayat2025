<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="ContactMaster.aspx.cs" Inherits="Web.Master.ContactMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="LangTextBox" Namespace="ServerControl1" TagPrefix="Lang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--<script type="text/javascript">
        function forRentClicked(sender) {
            var tb1 = document.getElementById('<%= drpContact.ClientID %>');
            tb1.style.display = sender.checked ? 'none' : 'inline';
            var tb2 = document.getElementById('<%= btnSearch.ClientID %>');
            tb2.style.display = sender.checked ? 'none' : 'inline';
        }
    </script>--%>
    <script type="text/javascript">
        function previewFile() {
            var preview = document.querySelector('#<%=Avatar.ClientID %>');
            var file = document.querySelector('#<%=avatarUploadd.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>
    <style>
        .btn {
            border-bottom-right-radius: 0px;
            border-top-right-radius: 0px;
        }

        .input-group {
            width: 100%;
            text-align: left;
        }

        .form-control {
            width: 100%;
        }

        .aspNetDisabled {
            width: 100%;
        }

        .tagsinput {
            width: 60%;
        }

        .gethide {
            display: none;
        }

        .getshow {
            display: block;
        }
    </style>

    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "personal";
            $('.tabbable a[href="#' + tabName + '"]').tab('show');
            $(".tabbable a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        });
    </script>--%>
    <style>
        .btn {
            border-bottom-right-radius: 0px;
            border-top-right-radius: 0px;
        }
    </style>
    <style type="text/css" media="screen">
        /* commented backslash hack for ie5mac \*/
        html, body {
            height: 100%;
        }
        /* end hack */
        .overlay {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: #EDEDF3;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .CenterPB {
            position: fixed;
            z-index: 999;
            left: 50%;
            top: 40%;
            margin-top: -30px; /* make this half your image/element height */
            margin-left: -30px; /* make this half your image/element width */
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
    </style>
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="b" runat="server">
        <!-- BEGIN BODY -->

        <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
            <div class="alert alert-success alert-dismissable">
                <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
        </asp:Panel>

        <asp:Panel ID="PanelError" runat="server" Visible="false">
            <div class="alert alert-danger alert-dismissable">
                <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                <asp:Label ID="lblerror" runat="server"></asp:Label>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlwarning" runat="server" Visible="false">
            <div class="alert alert-warning alert-dismissable">
                <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                <asp:Label ID="lblmsgw" Font-Size="Large" Font-Bold="true" ForeColor="Black" runat="server"></asp:Label>
            </div>
        </asp:Panel>

        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">

                        <div class="portlet-body form">
                            <div class="portlet-body">
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Remark Is Required" ControlToValidate="txtRemark" ValidationGroup="Submit" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>--%>
                                <div class="form-wizard">
                                    <div class="tabbable">
                                        <ul class="nav nav-pills nav-justified steps" style="margin-bottom: 0px; padding-bottom: 0px; padding-top: 0px;">
                                            <%--<li class="active" style="width: 210px">

                                                <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_general1" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">1 </span><span class="desc">
                                                        <asp:Label ID="lblBusinessCo" runat="server" Text="Personal Details" meta:resourcekey="lblBusinessCoResource1"></asp:Label>
                                                    </span></a>
                                            </li>--%>
                                            <%--<li style="width: 210px">
                                                <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_meta" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">2 </span><span class="desc">
                                                        <asp:Label ID="lblBusinessDetai" runat="server" Text="Business Details" meta:resourcekey="lblBusinessDetaiResource1"></asp:Label></span></a></li>
                                            <li style="width: 210px">
                                                <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_images" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">3 </span><span class="desc">
                                                        <asp:Label ID="lblWebExistance" runat="server" Text="Web Existance" meta:resourcekey="lblWebExistanceResource1"></asp:Label></span></a>&nbsp;
                                            </li>
                                            <li style="width: 210px">
                                                <a style="color: #5b9bd1; padding: 0px; width: 150px" href="#tab_reviews" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">4 </span><span class="desc">
                                                        <asp:Label ID="lblWorkingEmploy" runat="server" Text="Working Company" meta:resourcekey="lblWorkingEmployResource1"></asp:Label></span> </a>
                                            </li>--%>
                                        </ul>

                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general1">
                                                <div class="portlet box blue">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            <i class="fa fa-gift"></i>
                                                            <asp:Label ID="lblBusContactDe" runat="server" Text="GYM Customer Details" meta:resourcekey="Label28Resource1"></asp:Label>
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                            <a href="javascript:;" class="reload"></a>
                                                            <a href="javascript:;" class="remove"></a>
                                                        </div>
                                                        <div class="actions btn-set">
                                                            <asp:Button ID="btninvoice1" runat="server" Visible="false" Text="Invoice" CssClass="btn btn-circle yellow-crusta" Style="padding-top: 7px; padding-bottom: 7px; font-size: 11px" OnClick="btninvoice1_Click"/>
                                                            <asp:Button ID="Button1" class="btn btn-circle yellow" Style="padding-top: 7px; padding-bottom: 7px; font-size: 11px" runat="server" Text="Add New" OnClick="Button1_Click" meta:resourcekey="btnSubmitResource1" />
                                                            <asp:Button ID="btnSubmit" class="btn btn-circle yellow" Visible="false" runat="server" Text="Submit" Style="padding-top: 7px; padding-bottom: 7px; font-size: 11px" ValidationGroup="Submit" OnClick="btnSubmit_Click" meta:resourcekey="btnSubmitResource1" />

                                                            <asp:Button ID="btnCancel" class="btn btn-circle red" runat="server" Text="Cancel" Style="padding-top: 7px; padding-bottom: 7px; font-size: 11px" OnClick="btnCancel_Click" meta:resourcekey="btnCancelResource1" />
                                                            <asp:HiddenField ID="TabName" runat="server" />

                                                            <%--<asp:LinkButton ID="btnattmentmst" Visible="false" runat="server" class="btn green-haze btn-circle" Style="padding-top: 7px; padding-bottom: 7px; font-size: 11px" OnClick="btnOpportunity_Click">
                                                                Attachment&nbsp;<span class="badge badge-default" style="background-color: #f3565d; color: #fff;">
                                                                    <asp:Label ID="lblAttecmentcount" runat="server"></asp:Label>
                                                                </span>
                                                            </asp:LinkButton>--%>
                                                            <asp:LinkButton ID="LanguageEnglish" Style="color: #5b9bd1; width: 60px; padding: 0px;" runat="server">E&nbsp;<img src="/assets/global/img/flags/us.png" /></asp:LinkButton>
                                                            <asp:LinkButton ID="LanguageArabic" Style="color: #5b9bd1; width: 40px; padding: 0px;" runat="server">A&nbsp;<img src="/assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                            <asp:LinkButton ID="LanguageFrance" Style="color: #5b9bd1; width: 50px; padding: 0px;" runat="server">F&nbsp;<img src="/assets/global/img/flags/fr.png" /></asp:LinkButton>

                                                        </div>
                                                        <%--<div class="actions btn-set">
                                                            <a href="#tab_meta" class="step" data-toggle="tab" aria-expanded="true">
                                                                <asp:Label ID="lblnext" runat="server" Text="Next" class="btn red btn-circle"></asp:Label>
                                                            </a>

                                                        </div>--%>
                                                    </div>
                                                    <div class="portlet-body">
                                                        <div class="tabbable">
                                                            <div class="tab-content no-space">
                                                                <div class="form-body">
                                                                    <div class="form-group">
                                                                        <div class="col-md-6">
                                                                            <label runat="server" id="Label71" class="col-md-4 control-label getshow">
                                                                                <asp:Label ID="Label73" runat="server" Text="Contact Id" meta:resourcekey="lblTyptResource1"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtCustoID" AutoPostBack="true" OnTextChanged="txtCustoID_TextChanged" Enabled="false" runat="server" class="form-control" meta:resourcekey="txtCustomerNameResource1"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Contact Id Is Required" ControlToValidate="txtCustoID" ValidationGroup="Submit" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <div class="col-md-6">
                                                                            <label runat="server" id="lbl01" class="col-md-4 control-label getshow">
                                                                                <asp:Label ID="Label29" runat="server" Text="Contact Name:" meta:resourcekey="Label29Resource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <div class="input-group" style="text-align: left">
                                                                                    <asp:TextBox ID="txtContactName" runat="server" name="name" placeholder="Contact Name" AutoPostBack="True" OnTextChanged="txtContactName_TextChanged" data-toggle="tooltip" ToolTip="Contact Name" MaxLength="50" class="form-control" meta:resourcekey="txtContactNameResource1"></asp:TextBox>
                                                                                    <span class="input-group-btn">
                                                                                        <%-- <asp:Button ID="btnContact" class="btn blue " runat="server" Text="Check" OnClick="btnContact_Click" Style="padding-top: 7px; padding-bottom: 7px" meta:resourcekey="btnContactResource1" />--%>
                                                                                    </span>
                                                                                    <asp:LinkButton ID="lkbContactName" runat="server" OnClick="btnContact_Click">
                                                                                 <i class="icon-arrow-right" style="color:black"></i>
                                                                                    </asp:LinkButton>
                                                                                    <%--<asp:Button ID="btnCustomerN1" class="btn green-haze btn-circle" runat="server" Text="Check" OnClick="btnCustomerN1_Click" />--%>
                                                                                </div>
                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorCustomerName" runat="server" ErrorMessage="Contact Name Is Required" ControlToValidate="txtContactName" ValidationGroup="Submit" meta:resourcekey="RequiredFieldValidatorCustomerNameResource1"></asp:RequiredFieldValidator>
                                                                                <asp:Label ID="lblCustomer1" runat="server" ForeColor="Green" meta:resourcekey="lblCustomer1Resource1"></asp:Label>
                                                                            </div>
                                                                            <label runat="server" id="Label5" class="col-md-2 control-label gethide">
                                                                                <asp:Label ID="Label7" runat="server" Text="Contact Name:" meta:resourcekey="Label29Resource1"></asp:Label>
                                                                                <span class="required">* </span>

                                                                            </label>
                                                                        </div>
                                                                        <%-- </div>
                                                                    <div class="form-group" style="margin-left: 0px; margin-right: 0px;">--%>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label runat="server" id="lbl02" class="col-md-4 control-label getshow ">
                                                                                    <asp:Label ID="Label30" runat="server" Text="Name Lang 2:" meta:resourcekey="Label30Resource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <div class="input-group" style="text-align: left">
                                                                                        <%--<asp:TextBox ID="txtCustomer" placeholder="اسم الشخص" runat="server" AutoCompleteType="Disabled" class="arabic form-control" TextLanguage="Arabic"></asp:TextBox>--%>
                                                                                        <Lang:LangTextBox ID="txtContact2" runat="server" AutoCompleteType="Disabled" MaxLength="50" CssClass="arabic form-control" placeholder="اسم الشخص" TextLanguage="Arabic" meta:resourcekey="txtContact2Resource1"></Lang:LangTextBox>
                                                                                        <span class="input-group-btn">
                                                                                            <%-- <asp:Button ID="btncontactNL2" class="btn blue " runat="server" Text="Check" OnClick="btncontactNL2_Click" Style="padding-top: 7px; padding-bottom: 7px" meta:resourcekey="btncontactNL2Resource1" />--%>
                                                                                        </span>
                                                                                        <asp:LinkButton ID="lkbContact2" runat="server" OnClick="btncontactNL2_Click">
                                                                                 <i class="icon-arrow-right" style="color:black"></i>
                                                                                        </asp:LinkButton>
                                                                                        <%--<asp:Button ID="btnCompanyN2" class="btn green-haze btn-circle" runat="server" Text="Check" OnClick="btnCompanyN2_Click" />--%>
                                                                                    </div>
                                                                                    <asp:Label ID="lblCustomerL1" runat="server" ForeColor="Green" meta:resourcekey="lblCustomerL1Resource1"></asp:Label>
                                                                                    <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorCustomer" runat="server" ErrorMessage="Customer Name Other Language 1 Is Required" ControlToValidate="txtContact2" ValidationGroup="Submit" meta:resourcekey="RequiredFieldValidatorCustomerResource1"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <label runat="server" id="Label9" class="col-md-2 control-label gethide ">
                                                                                    <asp:Label ID="Label10" runat="server" Text="Contact Name  Language 2:" meta:resourcekey="Label30Resource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="lbl03" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="Label37" runat="server" Text="Name Lang 3 :" meta:resourcekey="Label37Resource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <div class="input-group" style="text-align: left">
                                                                                    <asp:TextBox ID="txtContact3" placeholder="Contact Name  Language 3" MaxLength="50" runat="server" class="form-control" AutoPostBack="True" OnTextChanged="txtContact3_TextChanged" meta:resourcekey="txtContact3Resource1"></asp:TextBox>
                                                                                    <span class="input-group-btn">
                                                                                        <%--<asp:Button ID="btnContactnl3" class="btn blue " runat="server" Text="Check" OnClick="btnContactnl3_Click" Style="padding-top: 7px; padding-bottom: 7px" meta:resourcekey="btnContactnl3Resource1" />--%>
                                                                                    </span>
                                                                                    <asp:LinkButton ID="lkbContactnl3" runat="server" OnClick="btnContactnl3_Click">
                                                                                 <i class="icon-arrow-right" style="color:black"></i>
                                                                                    </asp:LinkButton>
                                                                                    <%--<asp:Button ID="btncompnyN3" class="btn green-haze btn-circle" runat="server" Text="Check" OnClick="btncompnyN3_Click" />--%>
                                                                                </div>
                                                                                <asp:Label ID="lblCustomerL2" runat="server" ForeColor="Green" meta:resourcekey="lblCustomerL2Resource1"></asp:Label>
                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator10" runat="server" ErrorMessage="Customer Name Other Language 2 Is Required" ControlToValidate="txtContact3" ValidationGroup="Submit" meta:resourcekey="RequiredFieldValidator10Resource1"></asp:RequiredFieldValidator>

                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="lbl09" class="col-md-4 control-label getshow">
                                                                                <asp:Label ID="Label43" runat="server" Text="Address1:" meta:resourcekey="Label43Resource1"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="lblAddress" runat="server" meta:resourcekey="lblAddressResource1"></asp:Label>
                                                                                <asp:TextBox ID="txtAddress" placeholder="Address1" MaxLength="500" runat="server" class="form-control" meta:resourcekey="txtAddressResource1"></asp:TextBox>

                                                                            </div>
                                                                            <label runat="server" id="Label80" class="col-md-4 control-label gethide">
                                                                                <asp:Label ID="Label81" runat="server" Text="Address1:" meta:resourcekey="Label43Resource1"></asp:Label>

                                                                            </label>

                                                                        </div>

                                                                    </div>
                                                                    <asp:HiddenField ID="Regulervalue" runat="server" />
                                                                    <%--  <asp:LinkButton ID="btntest4" class="btn blue" Visible="false" runat="server" meta:resourcekey="btntest4Resource1"></asp:LinkButton>--%>
                                                                    <asp:Panel ID="ReceivedSign1" Style="padding: 1px; background-color: #fff; border: 1px solid #000; display: none" runat="server">
                                                                        <div class="modal-header">
                                                                            <asp:LinkButton ID="LinkButton1" class="close" runat="server" meta:resourcekey="LinkButton1Resource1">
                                                                                <asp:Label ID="Label31" runat="server" Text="Cancel" meta:resourcekey="Label31Resource1"></asp:Label>
                                                                            </asp:LinkButton>
                                                                            <%-- <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>--%>
                                                                            <h4 class="modal-title"><b>
                                                                                <asp:Label ID="Label13" runat="server" Text="All Ready Exit" meta:resourcekey="Label13Resource1"></asp:Label>
                                                                            </b></h4>
                                                                        </div>
                                                                        <div class="modal-body">
                                                                            <div class="row">
                                                                                <div class="portlet-body">
                                                                                    <div class="tabbable">
                                                                                        <table class="table table-striped table-bordered table-hover">
                                                                                            <thead>
                                                                                                <tr>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label32" runat="server" Text="Contact Name" meta:resourcekey="Label32Resource1"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label33" runat="server" Text="Mobile Number" meta:resourcekey="Label33Resource1"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label34" runat="server" Text="Email" meta:resourcekey="Label34Resource1"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label35" runat="server" Text="Fax Number" meta:resourcekey="Label35Resource1"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label36" runat="server" Text="Busphone" meta:resourcekey="Label36Resource1"></asp:Label></th>


                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>


                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="labelCopop" runat="server" meta:resourcekey="labelCopopResource1"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblmopop" runat="server" meta:resourcekey="lblmopopResource1"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblEmailpop" runat="server" meta:resourcekey="lblEmailpopResource1"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblFaxpop" runat="server" meta:resourcekey="lblFaxpopResource1"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblBuspop" runat="server" meta:resourcekey="lblBuspopResource1"></asp:Label></td>


                                                                                                </tr>



                                                                                            </tbody>
                                                                                        </table>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                        <div class="modal-footer">
                                                                            <%-- <asp:Button ID="btnsend1" class="btn blue" runat="server" Text="Send" OnClick ="btnsend_Click" />
            <asp:LinkButton ID="btnEngineerSign" class="btn blue" runat="server" >Submit</asp:LinkButton>--%>
                                                                            <asp:Button ID="btnYes" runat="server" class="btn green-haze btn-circle" Text="Yes" OnClick="btnYes_Click" meta:resourcekey="btnYesResource1" />
                                                                            <asp:Button ID="btnNo" runat="server" class="btn red-haze btn-circle" Text="No" OnClick="btnNo_Click" meta:resourcekey="btnNoResource1" />


                                                                        </div>

                                                                    </asp:Panel>
                                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" DynamicServicePath=""
                                                                        BackgroundCssClass="modalBackground" CancelControlID="LinkButton1" Enabled="True"
                                                                        PopupControlID="ReceivedSign1" TargetControlID="Regulervalue">
                                                                    </cc1:ModalPopupExtender>



                                                                    <div class="form-group">


                                                                        <div class="col-md-6">
                                                                            <label runat="server" id="lbl14" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="Label45" runat="server" Text="Mobile No:" meta:resourcekey="Label45Resource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <div class="input-group" style="text-align: left">
                                                                                    <asp:TextBox ID="txtMobileNo" placeholder="Mobile No" MaxLength="150" runat="server" AutoPostBack="True" OnTextChanged="txtMobileNo_TextChanged" class="form-control" meta:resourcekey="txtMobileNoResource1"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtMobileNo" ValidChars="0123456789," FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                                    <span class="input-group-btn">
                                                                                        <%-- <asp:Button ID="btnMobile" class="btn red " runat="server" Text="Check" OnClick="btnMobile_Click" Style="padding-top: 7px; padding-bottom: 7px" meta:resourcekey="btnMobileResource1" />--%>
                                                                                    </span>
                                                                                    <asp:LinkButton ID="lkbMobile" runat="server" ValidationGroup="ValidCountry" OnClick="btnMobile_Click">
                                                                                 <i class="icon-arrow-right" style="color:black"></i>
                                                                                    </asp:LinkButton>
                                                                                    <%--<asp:Button ID="btnMobile" class="btn green-haze btn-circle" runat="server" Text="Check" OnClick="btnMobile_Click" />--%>
                                                                                </div>
                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ErrorMessage="MobileNo Is Required" ControlToValidate="txtMobileNo" ValidationGroup="Submit" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                                                                <asp:Label ID="lblMobileNo" runat="server" ForeColor="Green" meta:resourcekey="lblMobileNoResource1"></asp:Label>
                                                                            </div>
                                                                            <label runat="server" id="Label90" class="col-md-2 control-label gethide ">
                                                                                <asp:Label ID="Label91" runat="server" Text="Mobile No:" meta:resourcekey="Label45Resource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                        </div>

                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="lbl11" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="lblEmail" runat="server" Text="EMAIL:" meta:resourcekey="lblEmailResource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <div class="input-group" style="text-align: left">
                                                                                    <asp:TextBox ID="tags_2" runat="server" name="email" MaxLength="500" CssClass="form-control tags" meta:resourcekey="tags_2Resource1"></asp:TextBox>
                                                                                    <span class="input-group-btn">
                                                                                        <%-- <asp:Button ID="btnEmail" class="btn blue" runat="server" Text="Check" OnClick="btnEmail_Click" Style="padding-top: 7px; padding-bottom: 7px" meta:resourcekey="btnEmailResource1" />--%>
                                                                                    </span>
                                                                                    <asp:LinkButton ID="lkbEmail" ValidationGroup="ValidCountry" runat="server" OnClick="btnEmail_Click">
                                                                                 <i class="icon-arrow-right" style="color:black"></i>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                                <asp:Label ID="lblEmail12" runat="server" ForeColor="Red" meta:resourcekey="lblEmail12Resource1"></asp:Label>
                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Email Is Required" ControlToValidate="tags_2" ValidationGroup="Submit" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <label runat="server" id="Label84" class="col-md-2 control-label gethide ">
                                                                                <asp:Label ID="Label85" runat="server" Text="EMAIL:" meta:resourcekey="lblEmailResource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                        </div>


                                                                    </div>

                                                                    <div class="form-group">
                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="Label26" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="Label28" runat="server" Text="Birth Date" meta:resourcekey="lblAddresResource1"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtBirthdate" OnTextChanged="txtBirthdate_TextChanged" AutoPostBack="true" placeholder="Birthday" MaxLength="500" runat="server" class="form-control" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBirthdate" Format="dd-MMM-yy" Enabled="True">
                                                                                </cc1:CalendarExtender>
                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="Label46" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="Label49" runat="server" Text="Civil ID" meta:resourcekey="lblAddres2Resource1"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-8">

                                                                                <asp:TextBox ID="txtCivilID" placeholder="Civil ID" MaxLength="500" runat="server" class="form-control" meta:resourcekey="txtAddress2Resource1"></asp:TextBox>

                                                                            </div>
                                                                            <label runat="server" id="Label109" class="col-md-4 control-label gethide ">
                                                                                <asp:Label ID="Label110" runat="server" Text="Civil ID" meta:resourcekey="lblAddres2Resource1"></asp:Label>

                                                                            </label>
                                                                        </div>

                                                                    </div>


                                                                    <div class="form-group">
                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="Label6" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="Label8" runat="server" Text="Nationality :" meta:resourcekey="lblAddresResource1"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="drpCountry" runat="server" class="form-control" meta:resourcekey="drpCountryResource1" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Country Is Required" ControlToValidate="drpCountry" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="Label11" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="Label12" runat="server" Text="Motive to Join GYM" meta:resourcekey="lblAddres2Resource1"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="drpMotiveTOJoin" runat="server" class="form-control" meta:resourcekey="drpCountryResource1" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Motive to join Is Required" ControlToValidate="drpMotiveTOJoin" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>

                                                                    </div>



                                                                    <div class="form-group" style="margin-bottom: 20px; margin-top: 5px">

                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl15" class="col-md-2 control-label getshow">
                                                                                <asp:Label ID="Label4" runat="server" Text="Avtar:"></asp:Label>
                                                                                <%-- <span class="required">* </span>--%>
                                                                            </label>
                                                                            <div class="col-md-4">
                                                                                <asp:Image ID="Avatar" runat="server" ImageUrl="~/Gallery/defolt.png" Style="width: 100px; height: 50px;" class="img-responsive" meta:resourcekey="AvatarResource1" />
                                                                                <asp:FileUpload ID="avatarUploadd" class="btn btn-circle green-haze btn-sm" runat="server" Style="margin-top: -50px; margin-left: 175px;" onchange="previewFile()" meta:resourcekey="avatarUploaddResource1" />
                                                                            </div>
                                                                            <label runat="server" id="Label92" class="col-md-2 control-label gethide">
                                                                                <asp:Label ID="Label93" runat="server" Text="Avtar:"></asp:Label>
                                                                                <%--<span class="required">* </span>--%>
                                                                            </label>
                                                                        </div>

                                                                    </div>




                                                                </div>


                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>



                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">

                                                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                                <div class="portlet box blue">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            <i class="fa fa-edit"></i>
                                                            <asp:Label runat="server" ID="Label41" Text="Package Details"></asp:Label>
                                                            List
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                            <a href="javascript:;" class="reload"></a>
                                                            <a href="javascript:;" class="remove"></a>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body">



                                                        <asp:Panel ID="PanelDtl" runat="server">

                                                            <table class="table table-striped table-hover table-bordered" id="sample_2">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="width: 1%;"></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label1" Text="Contact ID"></asp:Label></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label44" Text="Package"></asp:Label></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label2" Text="Start Date"></asp:Label></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label3" Text="End Date"></asp:Label></th>
                                                                        <th>
                                                                            <asp:Label runat="server" ID="Label47" Text="Status"></asp:Label></th>


                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:ListView ID="ListPackage" runat="server">
                                                                        <LayoutTemplate>
                                                                            <tr id="ItemPlaceholder" runat="server">
                                                                            </tr>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td></td>
                                                                                <td>
                                                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("CUSTVENDID") %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label50" runat="server" Text='<%# GetPack(Convert.ToInt32(Eval("RefTransID"))) %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label51" runat="server" Text='<%# Convert.ToDateTime(Eval("ENTRYDATE")).ToShortDateString() %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label52" runat="server" Text='<%# Convert.ToDateTime(Eval("ENTRYTIME")).ToShortDateString() %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label53" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                                </td>

                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>

                                                                </tbody>
                                                            </table>

                                                        </asp:Panel>

                                                    </div>



                                                </div>
                                                <!-- END EXAMPLE TABLE PORTLET-->
                                            </div>
                                        </div>
                                        <%-- Attendence --%>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-horizontal form-row-seperated">
                                                    <div class="portlet box blue">
                                                        <div class="portlet-title">
                                                            <div class="caption">
                                                                <i class="fa fa-gift"></i>Attendance
                                                            </div>
                                                            <div class="tools">
                                                                <a href="javascript:;" class="collapse"></a>
                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                <asp:LinkButton ID="btnPagereload" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                                <a href="javascript:;" class="remove"></a>
                                                            </div>
                                                            <div class="actions btn-set">
                                                            </div>
                                                        </div>
                                                        <div class="portlet-body">
                                                            <div class="portlet-body form">
                                                                <div class="tabbable">
                                                                    <div class="tab-content no-space">
                                                                        <div class="tab-pane active" id="tab_general1">
                                                                            <div class="form-body">
                                                                            </div>
                                                                            <div>

                                                                                <table class="table table-striped table-bordered table-hover" id="sample_11">
                                                                                    <thead>
                                                                                        <tr>

                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lblhTenet" Text="Date"></asp:Label></th>
                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lblhName" Text="InTime"></asp:Label></th>
                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lblhMyItems" Text="OutTime"></asp:Label></th>
                                                                                            <th>
                                                                                                <asp:Label runat="server" ID="lblhCompaigntype" Text="Hour?"></asp:Label></th>

                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>

                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lbldatdseTo" runat="server" Text="18-Nov-2017"></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbldat4223eTodsd" runat="server" Text="12.00"></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbld23eTodsd" runat="server" Text="01.00"></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lbldated2Todsd" runat="server" Text="1.00"></asp:Label>
                                                                                            </td>
                                                                                        </tr>

                                                                                        <tfoot>
                                                                                            <tr>
                                                                                                <td colspan="4"></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblTotalTimeFinal" runat="server" Text="0.00"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tfoot>
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
                                        <asp:Panel ID="pnlpack" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="col-md-12">

                                                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                                <div class="portlet box blue">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            <i class="fa fa-edit"></i>
                                                            Add
                                                        <asp:Label runat="server" ID="Label18" Text="Package Details"></asp:Label>
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                            <a href="javascript:;" class="reload"></a>
                                                            <a href="javascript:;" class="remove"></a>
                                                        </div>
                                                        <div class="actions btn-set">
                                                            <asp:Button ID="btnpacksave" CssClass="btn btn-sm yellow-crusta" runat="server" Text="Save" OnClick="btnpacksave_Click"/>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body">
                                                        <div class="row" style="margin-left: -0px; margin-right: -0px;">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label runat="server" id="Label14" class="col-md-4 control-label getshow ">
                                                                        <asp:Label ID="Label15" runat="server" Text="Select Package " meta:resourcekey="lblAddresResource1"></asp:Label>

                                                                    </label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpPackage" runat="server" class="form-control input-large" meta:resourcekey="drpCountryResource1" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Package Is Required" ControlToValidate="drpPackage" InitialValue="0" ValidationGroup="ValidDT"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label runat="server" id="Label16" class="col-md-4 control-label getshow ">
                                                                        <asp:Label ID="Label17" runat="server" Text="Amount Total " meta:resourcekey="lblAddres2Resource1"></asp:Label>
                                                                    </label>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtAmount" placeholder="Amount" MaxLength="12" runat="server" class="form-control" meta:resourcekey="txtAddress2Resource1"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Amount Is Required" ControlToValidate="txtAmount" InitialValue="0" ValidationGroup="ValidDT"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" TargetControlID="txtAmount" ValidChars="0123456789." FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <asp:Button ID="btnSpecialAmount" CssClass="btn btn-sm green" runat="server" Text="Special Amount" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin-left: -0px; margin-right: -0px;">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label19" runat="server" Text="Start Date" CssClass="col-md-4 control-label"></asp:Label>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtpackStartdate" placeholder="MM/dd/yyyy" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtendertxtdateTO1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtpackStartdate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" TargetControlID="txtpackStartdate" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="Label20" runat="server" Text="End Date" CssClass="col-md-4 control-label"></asp:Label>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtpackEnddate" placeholder="MM/dd/yyyy" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtpackEnddate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtpackEnddate" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>


                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="portlet box blue">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-gift"></i>
                                            Contact Master List
                                        </div>
                                        <div class="tools">
                                            <a href="javascript:;" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <asp:LinkButton ID="LinkButton9" OnClick="btnlistreload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>

                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                        <%-- <div class="actions btn-set">
                                            <asp:LinkButton ID="LinkButton11" class="btn btn-circle btn-warning" runat="server" OnClick="LinkButton11_Click">Advance Search</asp:LinkButton>
                                        </div>--%>
                                    </div>
                                    <div class="portlet-body form">
                                        <div class="form-body">
                                            <div class="row">



                                                <%--<asp:Button ID="btnSearch" class="btn green" runat="server" Text="Search" ValidationGroup="SaveSearch21" OnClick="" />--%>
                                                <%-- <asp:Button ID="btnSearchSave" class="btn green" runat="server" Text="Save" ValidationGroup="SaveSearch" OnClick="" />--%>
                                            </div>
                                        </div>
                                        <asp:Panel runat="server" ID="pnlGrid">
                                            <div class="tab-content">
                                                <div id="tab_1_1" class="tab-pane active">

                                                    <div class="tab-content no-space">
                                                        <div class="tab-pane active" id="tab_general2">
                                                            <div class="table-container" style="">




                                                                <div class="portlet-body" style="margin-left: 10px; margin-right: 10px; margin-top: 0px; padding-top: 10px; padding-bottom: 10px;">
                                                                    <div id="sample_1_wrapper" class="dataTables_wrapper no-footer">

                                                                        <div class="row">
                                                                            <div class="col-md-6 col-sm-12">
                                                                                <div class="dataTables_length" id="sample_1_length">
                                                                                    <label>
                                                                                        Show
                                                                                       <asp:DropDownList class="form-control input-xsmall input-inline " ID="drpShowGrid" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpShowGrid_SelectedIndexChanged">
                                                                                           <asp:ListItem Value="5">5</asp:ListItem>
                                                                                           <asp:ListItem Value="15">15</asp:ListItem>
                                                                                           <asp:ListItem Value="20" Selected="True">20</asp:ListItem>
                                                                                           <asp:ListItem Value="30">30</asp:ListItem>
                                                                                           <asp:ListItem Value="40">40</asp:ListItem>
                                                                                           <asp:ListItem Value="50">50</asp:ListItem>
                                                                                           <asp:ListItem Value="100">100</asp:ListItem>
                                                                                       </asp:DropDownList>
                                                                                        <%--<select name="sample_1_length" aria-controls="sample_1"  tabindex="-1" title="">
                                                                                            <option value="5">5</option>
                                                                                            <option value="15">15</option>
                                                                                            <option value="20">20</option>
                                                                                            <option value="-1">All</option>
                                                                                        </select>--%>
                         Entries&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList Style="width: 150px;" class="form-control input-inline " ID="drpSort" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpSort_SelectedIndexChanged">
                         </asp:DropDownList></label>
                                                                                    Show Active&nbsp;&nbsp;<asp:CheckBox ID="chkactive" runat="server" AutoPostBack="true" OnCheckedChanged="chkactive_CheckedChanged" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-6 col-sm-12">
                                                                                <div id="sample_1_filter" class="dataTables_filter">
                                                                                    <label>
                                                                                        <asp:TextBox ID="txtSearch" Placeholder="Search" class="form-control input-small input-inline" runat="server"></asp:TextBox>
                                                                                        <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton10_Click" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-search"></i></asp:LinkButton></label>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="table-scrollable">
                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>
                                                                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th style="width: 10%">
                                                                                                    <asp:Label ID="Label65" runat="server" Text="Action" meta:resourcekey="Label65Resource1"></asp:Label></th>
                                                                                                <th style="width: 10%">
                                                                                                    <asp:Label ID="Label69" runat="server" Text="Contact ID" meta:resourcekey="Label65Resource1"></asp:Label></th>
                                                                                                <th>
                                                                                                    <asp:Label ID="Label66" runat="server" Text="Contact Name" meta:resourcekey="Label66Resource1"></asp:Label></th>
                                                                                                <%--<th>
                                                                                            <asp:Label ID="Label67" runat="server" Text="Address" meta:resourcekey="Label67Resource1"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label ID="Label68" runat="server" Text="EMAIL" meta:resourcekey="Label68Resource1"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label ID="Label69" runat="server" Text="Mobile No" meta:resourcekey="Label69Resource1"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label ID="Label70" runat="server" Text="State" meta:resourcekey="Label70Resource1"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label ID="Label71" runat="server" Text="ZipCode" meta:resourcekey="Label71Resource1"></asp:Label></th>--%>
                                                                                                <th style="width: 20%">
                                                                                                    <asp:Label ID="Label72" runat="server" Text="City" meta:resourcekey="Label72Resource1"></asp:Label></th>
                                                                                                <%--<th>
                                                                                            <asp:Label ID="Label73" runat="server" Text="Remark" meta:resourcekey="Label73Resource1"></asp:Label></th>--%>
                                                                                               <%-- <th style="width: 10%">
                                                                                                    <asp:Label ID="lblActive" runat="server" Text="Active" meta:resourcekey="lblcity3Resource1"></asp:Label></th>--%>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <asp:ListView ID="Listview2" runat="server" OnItemCommand="ListCustomerMaster_ItemCommand" OnItemDataBound="Listview2_ItemDataBound" DataKey="PersName1,PersName2,PersName3,EMAIL1,FaxID,MOBPHONE,ITMANAGER,ADDR1,ADDR2,CITY,STATE,POSTALCODE,ZIPCODE,MYCONLOCID,COUNTRYID,BUSPHONE1,REMARKS" DataKeyNames="PersName1,PersName2,PersName3,EMAIL1,FaxID,MOBPHONE,ITMANAGER,ADDR1,ADDR2,CITY,STATE,POSTALCODE,ZIPCODE,MYCONLOCID,COUNTRYID,BUSPHONE1,REMARKS">
                                                                                                <LayoutTemplate>
                                                                                                    <tr id="ItemPlaceholder" runat="server">
                                                                                                    </tr>
                                                                                                </LayoutTemplate>
                                                                                                <ItemTemplate>

                                                                                                    <tr>

                                                                                                        <td>
                                                                                                            <table>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:LinkButton ID="linkbtnview" CommandName="btnview" class="btn btn-sm red filter-cancel" CommandArgument='<%# Eval("ContactMyID") %>' PostBackUrl='<%# "ContactMaster.aspx?ContactMyID="+ Eval("ContactMyID") %>' runat="server">
                                                                                                                    <i class="fa fa-eye"></i>
                                                                                                                        </asp:LinkButton>

                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%-- <asp:LinkButton ID="btnEdit" CommandName="btnEdit" PostBackUrl='<%# "Campaign_Mst.aspx?ID="+ Eval("ID")%>' CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>--%>

                                                                                                                        <asp:LinkButton ID="LinkButton1" CommandName="btnEdit" CommandArgument='<%# Eval("ContactMyID") %>' PostBackUrl='<%# "ContactMaster.aspx?ContactMyID="+ Eval("ContactMyID") %>' class="btn btn-sm yellow filter-submit margin-bottom" runat="server">
                                                                <i class="fa fa-pencil"></i>
                                                                                                                        </asp:LinkButton>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:LinkButton ID="LinkButton2" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("ContactMyID") %>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i>
                                                              
                                                                                                                        </asp:LinkButton>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>

                                                                                                        </td>

                                                                                                        <td>
                                                                                                            <asp:LinkButton ID="LinkButton7" CommandName="btnview" CommandArgument='<%# Eval("ContactMyID") %>' PostBackUrl='<%# "ContactMaster.aspx?ContactMyID="+ Eval("ContactMyID") %>' runat="server">
                                                                                                                <asp:Label ID="Label70" runat="server" Text='<%# Eval("ContactMyID") %>' meta:resourcekey="lblCustomerNameResource3"></asp:Label>
                                                                                                            </asp:LinkButton>
                                                                                                        </td>

                                                                                                        <td>
                                                                                                            <asp:HiddenField ID="hidecontactid" runat="server" Value='<%# Eval("ContactMyID") %>' />

                                                                                                            <asp:LinkButton ID="LinkButton4" CommandName="btnview" CommandArgument='<%# Eval("ContactMyID") %>' PostBackUrl='<%# "ContactMaster.aspx?ContactMyID="+ Eval("ContactMyID") %>' runat="server">
                                                                                                                <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("PersName1") %>' meta:resourcekey="lblCustomerNameResource3"></asp:Label>
                                                                                                            </asp:LinkButton>
                                                                                                        </td>
                                                                                                        <%--<td>
                                                                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDR1") %>' meta:resourcekey="lblAddressResource4"></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblEMAIL" runat="server" Text='<%# Eval("EMAIL1") %>' meta:resourcekey="lblEMAILResource4"></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblMOBPHONE" runat="server" Text='<%# Eval("MOBPHONE") %>' meta:resourcekey="lblMOBPHONEResource2"></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblSTATE" runat="server" Text='<%# Eval("STATE") %>' meta:resourcekey="lblSTATEResource2"></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblZIPCODE" runat="server" Text='<%# Eval("ZIPCODE") %>' meta:resourcekey="lblZIPCODEResource2"></asp:Label></td>
                                                                                                        --%>
                                                                                                        <td>
                                                                                                            <asp:LinkButton ID="LinkButton6" CommandName="btnview" CommandArgument='<%# Eval("ContactMyID") %>' PostBackUrl='<%# "ContactMaster.aspx?ContactMyID="+ Eval("ContactMyID") %>' runat="server">
                                                                                                                <asp:Label ID="lblCITY" runat="server" Text='<%# getCityName(Convert.ToInt32(Eval("CITY")),Convert.ToInt32(Eval("STATE"))) %>' meta:resourcekey="lblCITYResource3"></asp:Label>
                                                                                                            </asp:LinkButton>
                                                                                                        </td>

                                                                                                        <%--<td>
                                                                                                            <asp:Label ID="lblContactID" Visible="false" runat="server" Text='<%# Eval("ContactMyID") %>'></asp:Label>
                                                                                                            <asp:LinkButton ID="lnkbtnActive" CssClass="btn btn-sm green filter-submit margin-bottom" CommandName="btnActive" CommandArgument='<%# Eval("ContactMyID") %>' runat="server">
                                                                                                                   
                                                                                                            </asp:LinkButton>
                                                                                                        </td>--%>

                                                                                                        <%--<td>
                                                                                                    <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>' meta:resourcekey="lblREMARKSResource2"></asp:Label></td>--%>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                            </asp:ListView>

                                                                                        </tbody>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                        <div class="row">
                                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                <ContentTemplate>
                                                                                    <div class="col-md-5 col-sm-12">
                                                                                        <div class="dataTables_info" id="sample_1_info" role="status" aria-live="polite">
                                                                                            <asp:Label ID="lblShowinfEntry" runat="server"></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <div class="col-md-7 col-sm-12">
                                                                                <div class="dataTables_paginate paging_simple_numbers" id="sample_1_paginate">

                                                                                    <ul class="pagination">
                                                                                        <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_fist">

                                                                                            <asp:LinkButton ID="btnfirst1" OnClick="btnfirst1_Click" runat="server"> First</asp:LinkButton>
                                                                                        </li>
                                                                                        <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_Next">

                                                                                            <asp:LinkButton ID="btnNext1" Style="width: 53px;" OnClick="btnNext1_Click" runat="server"> Next</asp:LinkButton>
                                                                                        </li>
                                                                                        <asp:ListView ID="ListView3" runat="server" OnItemCommand="ListView2_ItemCommand" OnItemDataBound="AnswerList_ItemDataBound">
                                                                                            <ItemTemplate>
                                                                                                <td>
                                                                                                    <li class="paginate_button " aria-controls="sample_1" tabindex="0">
                                                                                                        <asp:LinkButton ID="LinkPageavigation" runat="server" CommandName="LinkPageavigation" CommandArgument='<%# Eval("ID")%>'> <%# Eval("ID")%></asp:LinkButton></li>

                                                                                                </td>
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>
                                                                                        <li class="paginate_button next" aria-controls="sample_1" tabindex="0" id="sample_1_Previos">
                                                                                            <asp:LinkButton ID="btnPrevious1" OnClick="btnPrevious1_Click" Style="width: 58px;" runat="server"> Prev</asp:LinkButton>
                                                                                        </li>
                                                                                        <li class="paginate_button next" aria-controls="sample_1" tabindex="0" id="sample_1_last">
                                                                                            <asp:LinkButton ID="btnLast1" OnClick="btnLast1_Click" runat="server"> Last</asp:LinkButton>
                                                                                        </li>
                                                                                    </ul>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>

                                                    </div>
                                                    <asp:HiddenField ID="hideID" runat="server" Value="" />
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnfirst1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnNext1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnPrevious1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnLast1" EventName="Click" />
                            </Triggers>

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

                    </div>
                </div>
            </div>
        </div>

    </div>

    <div class="scroll-to-top">
        <i class="icon-arrow-up"></i>
    </div>

</asp:Content>
