<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="ContactMaster.aspx.cs" Inherits="Web.CRM.ContactMaster" %>

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
                                            <li class="active" style="width: 210px">

                                                <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_general1" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">1 </span><span class="desc">
                                                        <asp:Label ID="lblBusinessCo" runat="server" Text="Personal Details" meta:resourcekey="lblBusinessCoResource1"></asp:Label>
                                                    </span></a>
                                            </li>
                                            <li style="width: 210px">
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
                                            </li>
                                            <li style="width: 60px">
                                                <asp:LinkButton ID="LanguageEnglish" Style="color: #5b9bd1; width: 60px; padding: 0px;" runat="server">E&nbsp;<img src="/assets/global/img/flags/us.png" /></asp:LinkButton>
                                                &nbsp;
                                            </li>
                                            <li style="width: 40px">
                                                <asp:LinkButton ID="LanguageArabic" Style="color: #5b9bd1; width: 40px; padding: 0px;" runat="server">A&nbsp;<img src="/assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                &nbsp; </li>
                                            <li style="width: 50px">
                                                <asp:LinkButton ID="LanguageFrance" Style="color: #5b9bd1; width: 50px; padding: 0px;" runat="server">F&nbsp;<img src="/assets/global/img/flags/fr.png" /></asp:LinkButton>
                                            </li>
                                            <li style="width: 80px">
                                                <asp:Button ID="Button1" class="btn btn-circle btn-primary" Style="padding-top: 7px; padding-bottom: 7px; font-size: 11px" runat="server" Text="Add New" OnClick="Button1_Click" meta:resourcekey="btnSubmitResource1" />
                                                <asp:Button ID="btnSubmit" class="btn btn-circle btn-primary" Visible="false" runat="server" Text="Submit" Style="padding-top: 7px; padding-bottom: 7px; font-size: 11px" ValidationGroup="Submit" OnClick="btnSubmit_Click" meta:resourcekey="btnSubmitResource1" />
                                            </li>

                                            <li style="width: 80px">
                                                <asp:Button ID="btnCancel" class="btn btn-circle btn-default" runat="server" Text="Cancel" Style="padding-top: 7px; padding-bottom: 7px; font-size: 11px" OnClick="btnCancel_Click" meta:resourcekey="btnCancelResource1" />
                                                <asp:HiddenField ID="TabName" runat="server" />
                                            </li>
                                            <li style="width: 80px">

                                                <asp:LinkButton ID="btnattmentmst" Visible="false" runat="server" class="btn green-haze btn-circle" Style="padding-top: 7px; padding-bottom: 7px; font-size: 11px" OnClick="btnOpportunity_Click">
                                                    &nbsp;<span class="badge badge-default" style="background-color: #f3565d; color: #fff;">
                                                        <asp:Label ID="lblAttecmentcount" runat="server"></asp:Label>
                                                    </span>
                                                </asp:LinkButton>
                                            </li>
                                        </ul>

                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general1">
                                                <div class="portlet box blue">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            <i class="fa fa-gift"></i>
                                                            <asp:Label ID="lblBusContactDe" runat="server" Text="Business Contact Details" meta:resourcekey="Label28Resource1"></asp:Label>
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                            <a href="javascript:;" class="reload"></a>
                                                            <a href="javascript:;" class="remove"></a>
                                                        </div>
                                                        <div class="actions btn-set">
                                                            <a href="#tab_meta" class="step" data-toggle="tab" aria-expanded="true">
                                                                <asp:Label ID="lblnext" runat="server" Text="Next" class="btn red btn-circle"></asp:Label>
                                                            </a>

                                                        </div>
                                                    </div>
                                                    <div class="portlet-body">
                                                        <div class="tabbable">
                                                            <div class="tab-content no-space">
                                                                <div class="form-body">

                                                                    <div class="form-group">

                                                                        <div class="col-md-4">

                                                                            <label runat="server" id="Label71" class="col-md-5 control-label getshow">
                                                                                <asp:Label ID="Label73" runat="server" Text="Contact Id" meta:resourcekey="lblTyptResource1"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-7">
                                                                                <asp:TextBox ID="txtCustoID" AutoPostBack="true" OnTextChanged="txtCustoID_TextChanged" Enabled="false" runat="server" CssClass="form-control" meta:resourcekey="txtCustomerNameResource1"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Contact Id Is Required" ControlToValidate="txtCustoID" ValidationGroup="Submit" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-4">

                                                                            <%--<label runat="server" id="Label74" class="col-md-4 control-label getshow">
                                                                                <asp:Label ID="Label108" runat="server" Text="Reffrance" meta:resourcekey="lblTyptResource1"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="drpdatasource" runat="server" class="form-control select2me"></asp:DropDownList>
                                                                            </div>--%>
                                                                        </div>

                                                                        <%--<div class="col-md-2">
                                                                            <%--<div class="col-md-12" style="left: 76px; right: 0px; width: 282px;">--%
                                                                                <asp:Label ID="Label111" runat="server" Text="" meta:resourcekey="Label11Resource1"></asp:Label>
                                                                                &nbsp;(<asp:CheckBox ID="chkIsCmny" Text="Is a Freelancer?" runat="server" onClick="forRentClicked(this)" meta:resourcekey="chkIsCmnyResource1" />
                                                                                )
                                                                            <%--</div>-
                                                                        </div--%>

                                                                        <div class="col-md-4">

                                                                            <label runat="server" id="Label112" class="col-md-4 control-label getshow">
                                                                                <asp:Label ID="lblTypt" runat="server" Text="Type:" meta:resourcekey="lblTyptResource1"></asp:Label>
                                                                                <span class="required">* </span>

                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="lblType" runat="server" meta:resourcekey="lblTypeResource1"></asp:Label>
                                                                                <asp:DropDownList ID="drpType" runat="server" CssClass="form-control select2me" meta:resourcekey="drpTypeResource1"></asp:DropDownList>
                                                                            </div>
                                                                            <label runat="server" id="Label113" class="col-md-4 control-label gethide ">
                                                                                <asp:Label ID="Label114" runat="server" Text="Type:" meta:resourcekey="lblTyptResource1"></asp:Label>
                                                                                <span class="required">* </span>

                                                                            </label>
                                                                        </div>

                                                                    </div>

                                                                    <div class="form-group" style="margin-bottom: 0px;">
                                                                        <div class="col-md-12">
                                                                            <label runat="server" id="lbl01" class="col-md-2 control-label getshow">
                                                                                <asp:Label ID="Label29" runat="server" Text="Contact Name:" meta:resourcekey="Label29Resource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-10" style="padding-left: 7px;">
                                                                                <div class="input-group" style="text-align: left">
                                                                                    <asp:TextBox ID="txtContactName" runat="server" name="name" placeholder="Contact Name" AutoPostBack="True" OnTextChanged="txtContactName_TextChanged" data-toggle="tooltip" ToolTip="Contact Name" MaxLength="50" CssClass="form-control" meta:resourcekey="txtContactNameResource1"></asp:TextBox>
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
                                                                    </div>
                                                                    <div class="form-group" style="margin-bottom: 0px;">
                                                                        <div class="col-md-12">
                                                                            <label runat="server" id="lbl02" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="Label30" runat="server" Text="Name Lang 2:" meta:resourcekey="Label30Resource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-10" style="padding-left: 7px;">
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
                                                                    <div class="form-group" style="margin-bottom: 0px;">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl03" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="Label37" runat="server" Text="Name Lang 3 :" meta:resourcekey="Label37Resource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-10" style="padding-left: 7px;">
                                                                                <div class="input-group" style="text-align: left">
                                                                                    <asp:TextBox ID="txtContact3" placeholder="Contact Name  Language 3" MaxLength="50" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtContact3_TextChanged" meta:resourcekey="txtContact3Resource1"></asp:TextBox>
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
                                                                            <label runat="server" id="Label11" class="col-md-2 control-label gethide ">
                                                                                <asp:Label ID="Label12" runat="server" Text="Name Lang 3 :" meta:resourcekey="Label37Resource1"></asp:Label>
                                                                                <span class="required">* </span>
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
                                                                            <asp:Button ID="btnYes" runat="server" CssClass="btn green-haze btn-circle" Text="Yes" OnClick="btnYes_Click" meta:resourcekey="btnYesResource1" />
                                                                            <asp:Button ID="btnNo" runat="server" CssClass="btn red-haze btn-circle" Text="No" OnClick="btnNo_Click" meta:resourcekey="btnNoResource1" />


                                                                        </div>

                                                                    </asp:Panel>
                                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" DynamicServicePath=""
                                                                        BackgroundCssClass="modalBackground" CancelControlID="LinkButton1" Enabled="True"
                                                                        PopupControlID="ReceivedSign1" TargetControlID="Regulervalue">
                                                                    </cc1:ModalPopupExtender>
                                                                    <asp:UpdatePanel ID="updCountry" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <div class="form-group">
                                                                                <div class="col-md-6">
                                                                                    <label runat="server" id="lbl04" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="Label38" runat="server" Text="Country:" meta:resourcekey="Label38Resource1"></asp:Label>
                                                                                        <%-- <span class="required">* </span>--%>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label6Resource1"></asp:Label>
                                                                                        <asp:DropDownList ID="drpCountry" runat="server" CssClass="form-control" meta:resourcekey="drpCountryResource1" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged" AutoPostBack="true">
                                                                                        </asp:DropDownList>
                                                                                        <%-- <asp:TextBox ID="TextBox4" runat="server" class="form-control"></asp:TextBox>
                                                                                        --%>
                                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Country Is Required" ControlToValidate="drpCountry" InitialValue="0" ValidationGroup="ValidCountry"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <label runat="server" id="Label14" class="col-md-4 control-label gethide ">
                                                                                        <asp:Label ID="Label15" runat="server" Text="Country:" meta:resourcekey="Label38Resource1"></asp:Label>
                                                                                        <%-- <span class="required">* </span>--%>
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <label runat="server" id="lbl05" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="Label39" runat="server" Text="Postal Code :" meta:resourcekey="Label39Resource1"></asp:Label>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="lblPostalCode" runat="server" meta:resourcekey="lblPostalCodeResource1"></asp:Label>
                                                                                        <asp:TextBox ID="txtPostalCode" placeholder="Postal Code" MaxLength="40" runat="server" CssClass="form-control" meta:resourcekey="txtPostalCodeResource1"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtPostalCode" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                                    </div>
                                                                                    <label runat="server" id="Label16" class="col-md-4 control-label gethide ">
                                                                                        <asp:Label ID="Label20" runat="server" Text="Postal Code :" meta:resourcekey="Label39Resource1"></asp:Label>
                                                                                    </label>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <div class="col-md-6">
                                                                                    <label runat="server" id="lbl06" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="Label40" runat="server" Text="State:" meta:resourcekey="Label40Resource1"></asp:Label>
                                                                                        <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="Label8" runat="server" meta:resourcekey="Label8Resource1"></asp:Label>
                                                                                        <asp:DropDownList ID="drpSates" AutoPostBack="true" OnSelectedIndexChanged="drpSates_SelectedIndexChanged" runat="server" CssClass="form-control">
                                                                                        </asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorPostalCode" runat="server" ErrorMessage="State Is Required" InitialValue="0" ControlToValidate="drpSates" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <label runat="server" id="Label27" class="col-md-4 control-label gethide ">
                                                                                        <asp:Label ID="Label75" runat="server" Text="State:" meta:resourcekey="Label40Resource1"></asp:Label>
                                                                                        <span class="required">* </span>
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <label runat="server" id="lbl07" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="Label41" runat="server" Text="Zip Code" meta:resourcekey="Label41Resource1"></asp:Label>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="lblZipCode" runat="server" meta:resourcekey="lblZipCodeResource1"></asp:Label>
                                                                                        <asp:TextBox ID="txtZipCode" placeholder="ZipCode" MaxLength="10" runat="server" CssClass="form-control" meta:resourcekey="txtZipCodeResource1"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtZipCode" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                                    </div>
                                                                                    <label runat="server" id="Label76" class="col-md-4 control-label gethide ">
                                                                                        <asp:Label ID="Label77" runat="server" Text="Zip Code" meta:resourcekey="Label41Resource1"></asp:Label>
                                                                                    </label>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <div class="col-md-6">
                                                                                    <label runat="server" id="lbl08" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="Label42" runat="server" Text="City:" meta:resourcekey="Label42Resource1"></asp:Label>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="lblCity" runat="server" meta:resourcekey="lblCityResource1"></asp:Label>
                                                                                        <%--<asp:TextBox ID="txtCity" placeholder="City" MaxLength="49" runat="server" class="form-control" meta:resourcekey="txtCityResource1"></asp:TextBox>--%>
                                                                                        <asp:DropDownList ID="drpcity" AutoPostBack="true" OnSelectedIndexChanged="drpcity_SelectedIndexChanged" runat="server" CssClass="form-control">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                    <label runat="server" id="Label78" class="col-md-4 control-label gethide ">
                                                                                        <asp:Label ID="Label79" runat="server" Text="City:" meta:resourcekey="Label42Resource1"></asp:Label>
                                                                                    </label>
                                                                                </div>

                                                                                <div class="col-md-6">
                                                                                    <asp:Label ID="lblPACI" runat="server" CssClass="col-md-4 control-label" Text="PACI Number"></asp:Label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:TextBox ID="txtPACI" MaxLength="25" CssClass="form-control" placeholder="PACI Number" runat="server"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtPACI" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    <div class="form-group">
                                                                        <div class="col-md-6">
                                                                            <label runat="server" id="lbl09" class="col-md-4 control-label getshow">
                                                                                <asp:Label ID="Label43" runat="server" Text="Address1:" meta:resourcekey="Label43Resource1"></asp:Label>
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="lblAddress" runat="server" meta:resourcekey="lblAddressResource1"></asp:Label>
                                                                                <asp:TextBox ID="txtAddress" placeholder="Address1" MaxLength="500" runat="server" CssClass="form-control" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                                                                            </div>
                                                                            <label runat="server" id="Label80" class="col-md-4 control-label gethide">
                                                                                <asp:Label ID="Label81" runat="server" Text="Address1:" meta:resourcekey="Label43Resource1"></asp:Label>
                                                                            </label>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <label runat="server" id="lbl10" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="Label44" runat="server" Text="Address2:" meta:resourcekey="Label44Resource1"></asp:Label>
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="lblAddress2" runat="server" meta:resourcekey="lblAddress2Resource1"></asp:Label>
                                                                                <asp:TextBox ID="txtAddress2" placeholder="Address2" MaxLength="500" runat="server" CssClass="form-control" meta:resourcekey="txtAddress2Resource1"></asp:TextBox>
                                                                            </div>
                                                                            <label runat="server" id="Label82" class="col-md-4 control-label gethide ">
                                                                                <asp:Label ID="Label83" runat="server" Text="Address2:" meta:resourcekey="Label44Resource1"></asp:Label>
                                                                            </label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-6">
                                                                            <label runat="server" id="Label26" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="Label28" runat="server" Text="Birth Date" meta:resourcekey="lblAddresResource1"></asp:Label>
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtBirthdate" OnTextChanged="txtBirthdate_TextChanged" AutoPostBack="true" placeholder="Birthday" MaxLength="500" runat="server" CssClass="form-control" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBirthdate" Format="dd-MMM-yy" Enabled="True">
                                                                                </cc1:CalendarExtender>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <label runat="server" id="Label46" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="Label49" runat="server" Text="Civil ID" meta:resourcekey="lblAddres2Resource1"></asp:Label>
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtCivilID" placeholder="Civil ID" MaxLength="500" runat="server" CssClass="form-control" meta:resourcekey="txtAddress2Resource1"></asp:TextBox>
                                                                            </div>
                                                                            <label runat="server" id="Label109" class="col-md-4 control-label gethide ">
                                                                                <asp:Label ID="Label110" runat="server" Text="Civil ID" meta:resourcekey="lblAddres2Resource1"></asp:Label>
                                                                            </label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl11" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="lblEmail" runat="server" Text="EMAIL:" meta:resourcekey="lblEmailResource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-10">
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
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl12" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="lblFax" runat="server" Text="Fax:" meta:resourcekey="lblFaxResource1"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-10">
                                                                                <div class="input-group" style="text-align: left">
                                                                                    <asp:TextBox ID="tags_3" name="number" runat="server" MaxLength="500" CssClass="form-control tags" meta:resourcekey="tags_3Resource1"></asp:TextBox>
                                                                                    <span class="input-group-btn">
                                                                                        <%--<asp:Button ID="btnFax" class="btn blue " runat="server" Text="Check" OnClick="btnFax_Click" Style="padding-top: 7px; padding-bottom: 7px" meta:resourcekey="btnFaxResource1" />--%>
                                                                                    </span>
                                                                                    <asp:LinkButton ID="lkbFax" runat="server" ValidationGroup="ValidCountry" OnClick="btnFax_Click">
                                                                                 <i class="icon-arrow-right" style="color:black"></i>
                                                                                    </asp:LinkButton>

                                                                                </div>
                                                                                <asp:Label ID="Label21" runat="server" ForeColor="Red" meta:resourcekey="Label21Resource1"></asp:Label>

                                                                            </div>
                                                                            <label runat="server" id="Label86" class="col-md-2 control-label gethide ">
                                                                                <asp:Label ID="Label87" runat="server" Text="Fax:" meta:resourcekey="lblFaxResource1"></asp:Label>

                                                                            </label>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl13" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="lblBusPhone" runat="server" Text="Bus Phone:" meta:resourcekey="lblBusPhoneResource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-10">
                                                                                <div class="input-group" style="text-align: left">
                                                                                    <asp:TextBox ID="tags_4" name="number" MaxLength="500" runat="server" CssClass="form-control tags" meta:resourcekey="tags_4Resource1"></asp:TextBox>
                                                                                    <span class="input-group-btn">
                                                                                        <%--<asp:Button ID="btnBusPhone" class="btn blue" runat="server" Text="Check" OnClick="btnBusPhone_Click" Style="padding-top: 7px; padding-bottom: 7px" meta:resourcekey="btnBusPhoneResource1" />--%>
                                                                                    </span>
                                                                                    <asp:LinkButton ID="lkbBusPhone" runat="server" ValidationGroup="ValidCountry" OnClick="btnBusPhone_Click  ">
                                                                                 <i class="icon-arrow-right" style="color:black"></i>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                                <asp:Label ID="Label22" runat="server" ForeColor="Red" meta:resourcekey="Label22Resource1"></asp:Label>
                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Bus Phone Is Required" ControlToValidate="tags_2" ValidationGroup="Submit" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <label runat="server" id="Label88" class="col-md-2 control-label gethide ">
                                                                                <asp:Label ID="Label89" runat="server" Text=" Bus Phone:" meta:resourcekey="lblBusPhoneResource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                        </div>

                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">


                                                                            <label runat="server" id="lbl14" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="Label45" runat="server" Text="Mobile No:" meta:resourcekey="Label45Resource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-10">
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
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="Label19" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="Label23" runat="server" Text="Barcode:" meta:resourcekey="Label48Resource1"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="Label24" runat="server" meta:resourcekey="lblBarcode5Resource1"></asp:Label>
                                                                                <asp:TextBox ID="txtBarcode" placeholder="Barcode" MaxLength="500" runat="server" CssClass="form-control" meta:resourcekey="txtBarcodeResource1"></asp:TextBox>

                                                                            </div>
                                                                            <label runat="server" id="Label64" class="col-md-4 control-label gethide ">
                                                                                <asp:Label ID="Label25" runat="server" Text="Barcode:" meta:resourcekey="Label48Resource1"></asp:Label>

                                                                            </label>

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

                                            <div class="tab-pane" id="tab_meta">
                                                <div class="portlet box purple">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            <i class="fa fa-gift"></i>
                                                            <asp:Label ID="lblBCDeta" runat="server" Text="Business Contact Details" meta:resourcekey="Label46Resource1"></asp:Label>
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                            <a href="javascript:;" class="reload"></a>
                                                            <a href="javascript:;" class="remove"></a>
                                                        </div>
                                                        <div class="actions btn-set">
                                                            <a href="#tab_general1" class="step" data-toggle="tab" aria-expanded="true">
                                                                <asp:Label ID="Label53" runat="server" Text="previous" class="btn yellow btn-circle"></asp:Label>
                                                            </a>

                                                            <a href="#tab_images" class="step" data-toggle="tab" aria-expanded="true">
                                                                <asp:Label ID="Label62" runat="server" Text="Next " class="btn red btn-circle"></asp:Label>
                                                            </a>
                                                        </div>

                                                    </div>
                                                    <div class="portlet-body">
                                                        <div class="tabbable">
                                                            <div class="tab-content no-space">
                                                                <div class="form-body">
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl16" class="col-md-2 control-label getshow">
                                                                                <asp:Label ID="Label47" runat="server" Text="My LocationID:" meta:resourcekey="Label47Resource1"></asp:Label>
                                                                                <%-- <span class="required">* </span>--%>
                                                                            </label>
                                                                            <div class="col-md-4">
                                                                                <asp:Label ID="lblCountryloc" runat="server" meta:resourcekey="lblCountrylocResource1"></asp:Label>
                                                                                <asp:DropDownList ID="drpMyCounLocID" runat="server" class="form-control" meta:resourcekey="drpMyCounLocIDResource1">
                                                                                </asp:DropDownList>

                                                                            </div>
                                                                            <label runat="server" id="Label94" class="col-md-2 control-label gethide">
                                                                                <asp:Label ID="Label95" runat="server" Text="My LocationID:" meta:resourcekey="Label47Resource1"></asp:Label>
                                                                                <%-- <span class="required">* </span>--%>
                                                                            </label>

                                                                        </div>
                                                                    </div>
                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                        <ContentTemplate>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl17" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="Label48" runat="server" Text="Remark:" meta:resourcekey="Label48Resource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-10">
                                                                                <asp:Label ID="lblRemark" runat="server" meta:resourcekey="lblRemarkResource1"></asp:Label>
                                                                                <asp:TextBox ID="txtRemark" TextMode="MultiLine" MaxLength="255" placeholder="Remark" runat="server" class="form-control"></asp:TextBox>

                                                                            </div>
                                                                            <label runat="server" id="Label96" class="col-md-2 control-label gethide ">
                                                                                <asp:Label ID="Label97" runat="server" Text="Remark:" meta:resourcekey="Label48Resource1"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane" id="tab_images">
                                                <div class="portlet box red-sunglo">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            <i class="fa fa-gift"></i>
                                                            <asp:Label ID="lblwExistance" runat="server" Text="Web Existance" meta:resourcekey="Label49Resource1"></asp:Label>
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                            <a href="javascript:;" class="reload"></a>
                                                            <a href="javascript:;" class="remove"></a>
                                                        </div>
                                                        <div class="actions btn-set">

                                                            <a href="#tab_meta" class="step" data-toggle="tab" aria-expanded="true">
                                                                <asp:Label ID="Label63" runat="server" Text="previous" class="btn purple btn-circle"></asp:Label>
                                                            </a>

                                                            <a href="#tab_reviews" class="step" data-toggle="tab" aria-expanded="true">
                                                                <asp:Label ID="Label67" runat="server" Text="Next " class="btn blue btn-circle"></asp:Label>
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body ">
                                                        <div class="tabbable">
                                                            <div class="tab-content no-space">
                                                                <div class="form-body">
                                                                    <asp:UpdatePanel ID="updateshochi" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Panel ID="panelMsg" runat="server" Visible="false">
                                                                                <div class="alert alert-danger alert-dismissable">
                                                                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                                    <asp:Label ID="lblErreorMsg" runat="server"></asp:Label>
                                                                                </div>
                                                                            </asp:Panel>
                                                                            <div class="form-group">
                                                                                <div class="col-md-12">

                                                                                    <label runat="server" id="lbl18" class="col-md-2 control-label getshow ">
                                                                                        <asp:Label ID="Label17" runat="server" Text="Social Media:" meta:resourcekey="Label17Resource1"></asp:Label>
                                                                                        <%-- <span class="required">* </span>--%>
                                                                                    </label>
                                                                                    <div class="col-md-4" style="width: 350px;">
                                                                                        <asp:Label ID="Label18" runat="server" meta:resourcekey="Label18Resource1"></asp:Label>
                                                                                        <asp:DropDownList ID="drpSomib" runat="server" CssClass="form-control" meta:resourcekey="drpSomibResource1">
                                                                                        </asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="drpSomib" ErrorMessage="Social Media Required." InitialValue="0" ValidationGroup="socielmediya"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <label runat="server" id="Label98" class="col-md-2 control-label gethide ">
                                                                                        <asp:Label ID="Label99" runat="server" Text="Social Media:" meta:resourcekey="Label17Resource1"></asp:Label>
                                                                                        <%-- <span class="required">* </span>--%>
                                                                                    </label>

                                                                                    <div class="col-md-4">
                                                                                        <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1"></asp:Label>
                                                                                        <div class="input-group" style="text-align: left">
                                                                                            <asp:TextBox ID="txtSocial" runat="server" CssClass="form-control" meta:resourcekey="txtSocialResource1"></asp:TextBox>
                                                                                            <span class="input-group-btn">
                                                                                                <%-- <asp:Button ID="btnSocial" class="btn red " runat="server"  Text="Add" Style="padding-top: 7px; padding-bottom: 7px" meta:resourcekey="btnSocialResource1" />--%>
                                                                                            </span>
                                                                                            <asp:LinkButton ID="LinkButton5" ValidationGroup="socielmediya" runat="server" OnClick="btnSocial_Click">
                                                                                    <i class="icon-plus " style="color:black;padding-left: 4px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="txtSocial" ErrorMessage="Social Id Required." ValidationGroup="socielmediya"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                            <div class="tabbable">
                                                                                <table class="table table-striped table-bordered table-hover">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>
                                                                                                <asp:Label ID="Label50" runat="server" Text="Social Media" meta:resourcekey="Label50Resource1"></asp:Label></th>
                                                                                            <th>
                                                                                                <asp:Label ID="Label51" runat="server" Text="Social Media Id" meta:resourcekey="Label51Resource1"></asp:Label></th>
                                                                                            <th>
                                                                                                <asp:Label ID="Label52" runat="server" Text="Remark" meta:resourcekey="Label52Resource1"></asp:Label></th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:ListView ID="listSocialMedia" runat="server">
                                                                                            <LayoutTemplate>
                                                                                                <tr id="ItemPlaceholder" runat="server">
                                                                                                </tr>
                                                                                            </LayoutTemplate>
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%# getshocial(Convert .ToInt32 ( Eval("Recource"))) %>' meta:resourcekey="lblCustomerNameResource1"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("RecValue") %>' meta:resourcekey="lblAddressResource2"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%# getremark(Convert .ToInt32 ( Eval("Recource"))) %>' meta:resourcekey="lblEMAILResource2"></asp:Label></td>


                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>

                                                                                    </tbody>
                                                                                </table>

                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="tab-pane" id="tab_reviews">
                                                <div class="portlet box yellow-crusta">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            <i class="fa fa-gift"></i>
                                                            <asp:Label ID="lblWEmp" runat="server" Text="Working Company" meta:resourcekey="Label53Resource1"></asp:Label>
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                            <a href="javascript:;" class="reload"></a>
                                                            <a href="javascript:;" class="remove"></a>
                                                        </div>
                                                        <div class="actions btn-set">
                                                            <a href="#tab_images" class="step" data-toggle="tab" aria-expanded="true">
                                                                <asp:Label ID="Label68" runat="server" Text="Previous" class="btn red btn-circle"></asp:Label>
                                                            </a>
                                                            <asp:Button ID="btnFinish" class="btn purple btn-circle" runat="server" Text="Finish" ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body ">
                                                        <div class="tabbable">
                                                            <div class="tab-content no-space">
                                                                <div class="form-body">
                                                                    <asp:UpdatePanel ID="updatenumber" runat="server">
                                                                        <ContentTemplate>
                                                                            <div class="row">
                                                                                <div class="col-md-12" style="padding-left: 20px;">
                                                                                    <div class="form-group">
                                                                                        <div class="col-md-3">
                                                                                            <div class="input-group">

                                                                                                <asp:TextBox ID="txtcompneySerch" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Company Search" MaxLength="250">
                                                                                                </asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtcompneySerch" ErrorMessage="Company Name Required." Display="Dynamic" ForeColor="Red" ValidationGroup="Compniserch"></asp:RequiredFieldValidator>
                                                                                                <span class="input-group-btn"></span>
                                                                                                <asp:LinkButton ID="lkbCustomerN1" CssClass="btn btn-icon-only yellow" runat="server" Style="margin-top: -28px;" ValidationGroup="Compniserch" OnClick="lkbCustomerN1_Click">
                                                                                 <i class="fa fa-search" ></i>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                            <asp:Label ID="lblcountserch" runat="server" ForeColor="Green"></asp:Label>
                                                                                        </div>
                                                                                        <div class="col-md-5">

                                                                                            <label runat="server" id="lbl20" class="col-md-4 control-label getshow">
                                                                                                <asp:Label ID="Label1" runat="server" Text="Company Name:" meta:resourcekey="Label1Resource1"></asp:Label>
                                                                                                <%-- <span class="required">* </span>--%>
                                                                                            </label>
                                                                                            <div class="col-md-8">
                                                                                                <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1"></asp:Label>
                                                                                                <asp:DropDownList ID="drpCompnay" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="drpCompnay_SelectedIndexChanged" meta:resourcekey="drpCompnayResource1">
                                                                                                </asp:DropDownList>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="drpCompnay" ErrorMessage="Select Compnay Required." InitialValue="0" Display="Dynamic" ForeColor="Red" ValidationGroup="selectcomniy"></asp:RequiredFieldValidator>
                                                                                            </div>
                                                                                            <label runat="server" id="Label100" class="col-md-4 control-label gethide">
                                                                                                <asp:Label ID="Label101" runat="server" Text="Company Name:" meta:resourcekey="Label1Resource1"></asp:Label>
                                                                                                <%--  <span class="required">* </span>--%>
                                                                                            </label>

                                                                                        </div>
                                                                                        <div class="col-md-4">

                                                                                            <label runat="server" id="lbl21" class="col-md-2 control-label getshow ">
                                                                                                <asp:Label ID="Label54" runat="server" Text="Position:" meta:resourcekey="Label54Resource1"></asp:Label>
                                                                                                <span class="required">* </span>
                                                                                            </label>
                                                                                            <div class="col-md-8">
                                                                                                <asp:Label ID="lblitManager" runat="server" meta:resourcekey="lblitManagerResource1"></asp:Label>
                                                                                                <div class="input-group" style="text-align: left">
                                                                                                    <asp:DropDownList ID="drpItManager" runat="server" CssClass="form-control" meta:resourcekey="drpItManagerResource1"></asp:DropDownList>
                                                                                                    
                                                                                                    <span class="input-group-btn">
                                                                                                       <asp:LinkButton ID="LinkButton3" runat="server" CssClass="list-group" ValidationGroup="selectcomniy" OnClick="btnCompniyadd_Click">
                                                                                                        <img src="../ECOMM/images/Add.png" />
                                                                                                    </asp:LinkButton>
                                                                                                    </span>
                                                                                                    
                                                                                                </div>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="drpItManager" InitialValue="0" ErrorMessage="Position Required." CssClass="Validation" ValidationGroup="selectcomniy"></asp:RequiredFieldValidator>
                                                                                            </div>
                                                                                            <label runat="server" id="Label102" class="col-md-4 control-label gethide ">
                                                                                                <asp:Label ID="Label103" runat="server" Text="Position:" meta:resourcekey="Label54Resource1"></asp:Label>
                                                                                                <span class="required">* </span>
                                                                                            </label>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="tabbable">
                                                                                <table class="table table-striped table-bordered table-hover">
                                                                                    <thead>
                                                                                        <tr>

                                                                                            <th>
                                                                                                <asp:Label ID="Label55" runat="server" Text="Contact Name" meta:resourcekey="Label55Resource1"></asp:Label></th>
                                                                                            <th>
                                                                                                <asp:Label ID="Label74" runat="server" Text="Company"></asp:Label>
                                                                                            </th>
                                                                                            <th>
                                                                                                <asp:Label ID="Label56" runat="server" Text="Job Title" meta:resourcekey="Label56Resource1"></asp:Label></th>
                                                                                            <th>
                                                                                                <asp:Label ID="Label57" runat="server" Text="EMAIL" meta:resourcekey="Label57Resource1"></asp:Label></th>
                                                                                            <th>
                                                                                                <asp:Label ID="Label58" runat="server" Text="Mobile No" meta:resourcekey="Label58Resource1"></asp:Label></th>


                                                                                            <th>
                                                                                                <asp:Label ID="Label60" runat="server" Text="City" meta:resourcekey="Label60Resource1"></asp:Label></th>
                                                                                            <th>
                                                                                                <asp:Label ID="Label59" runat="server" Text="share" meta:resourcekey="Label59Resource1"></asp:Label></th>
                                                                                            <th>
                                                                                                <asp:Label ID="Label61" runat="server" Text="Remark" meta:resourcekey="Label61Resource1"></asp:Label></th>


                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListView1_ItemCommand">
                                                                                            <LayoutTemplate>
                                                                                                <tr id="ItemPlaceholder" runat="server">
                                                                                                </tr>
                                                                                            </LayoutTemplate>
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%# getcomniy(Convert .ToInt32 ( Eval("ContactMyID"))) %>' meta:resourcekey="lblCustomerNameResource2"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label108" runat="server" Text='<%# workingcompany(Convert.ToInt32(Eval("CompID"))) %>'></asp:Label>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# GetPosition(Convert.ToInt32(Eval("JobTitle"))) %>' meta:resourcekey="lblAddressResource3"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%# Eval("email2") %>' meta:resourcekey="lblEMAILResource3"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblMOBPHONE" runat="server" Text='<%# Eval("BusPhone1") %>' meta:resourcekey="lblMOBPHONEResource1"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblCITY" runat="server" Text='<%# getStste(Convert .ToInt32( Eval("CompID"))) %>' meta:resourcekey="lblCITYResource2"></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:LinkButton ID="LinkButton1" CommandName="btnshare" CommandArgument='<%# Eval("ContactMyID") %>' runat="server">share</asp:LinkButton></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("remarks") %>' meta:resourcekey="lblREMARKSResource1"></asp:Label></td>


                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>

                                                                                    </tbody>
                                                                                </table>

                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
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
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
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
                            <div class="actions btn-set">
                                <asp:LinkButton ID="LinkButton11" class="btn btn-circle btn-warning" runat="server" OnClick="LinkButton11_Click">Advance Search</asp:LinkButton>
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label runat="server" id="Label106" class="control-label col-md-2 getshow">
                                                <asp:Label runat="server" ID="Label107">Saved Search</asp:Label>
                                            </label>
                                            <div class="col-md-5">
                                                <asp:DropDownList ID="DrpTitle" runat="server" class="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Title" ControlToValidate="DrpTitle" ValidationGroup="SaveSearch21" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:CheckBox ID="chDefaultSet" ToolTip="Default Set" runat="server" />
                                                <asp:Button ID="Button3" class="btn btn-circle purple" runat="server" Text="Show" ValidationGroup="SaveSearch21" OnClick="btnSearch_Click" OnClientClick="showProgress()" />
                                                <asp:Button ID="btnAppend" class="btn btn-circle red" runat="server" Text="Append" ValidationGroup="SaveSearch21" OnClick="btnAppend_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label runat="server" id="Label104" class="control-label col-md-2 getshow">
                                                <asp:Label runat="server" ID="Label105">Title</asp:Label>
                                            </label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTitle" ErrorMessage="Title Required." CssClass="Validation" ValidationGroup="SaveSearch"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="Button2" class="btn btn-circle blue" runat="server" Text="Save" ValidationGroup="SaveSearch" OnClick="btnSearchSave_Click" />
                                            </div>
                                        </div>
                                    </div>
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
                                                                                    <th style="width: 10%">
                                                                                        <asp:Label ID="lblActive" runat="server" Text="Active" meta:resourcekey="lblcity3Resource1"></asp:Label></th>
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

                                                                                            <td>
                                                                                                <asp:Label ID="lblContactID" Visible="false" runat="server" Text='<%# Eval("ContactMyID") %>'></asp:Label>
                                                                                                <asp:LinkButton ID="lnkbtnActive" CssClass="btn btn-sm green filter-submit margin-bottom" CommandName="btnActive" CommandArgument='<%# Eval("ContactMyID") %>' runat="server">
                                                                                                                   
                                                                                                </asp:LinkButton>
                                                                                            </td>

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
    </asp:Panel>
    </div>

    <div class="scroll-to-top">
        <i class="icon-arrow-up"></i>
    </div>
    </div>
        </div> 
</asp:Content>
