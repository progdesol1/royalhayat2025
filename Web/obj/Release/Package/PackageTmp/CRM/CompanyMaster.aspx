<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" Async="true" CodeBehind="CompanyMaster.aspx.cs" Inherits="Web.CRM.CompanyMaster" %>

<%@ Register Src="~/CRM/UserControl/RightPanelUC.ascx" TagPrefix="uc1" TagName="RightPanelUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Assembly="EditableDropDownList" Namespace="EditableControls" TagPrefix="editable" %>--%>
<%@ Register Assembly="LangTextBox" Namespace="ServerControl1" TagPrefix="Lang" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


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

    <%--<style>
        #tab_images .form-group{
            color: #fff;
        }
    </style>
     <style>
        #tab_meta .form-group{
            color: #fff;
        }
    </style>--%>


    <%--<script type="text/javascript">
        $(function () {
            // for bootstrap 3 use 'shown.bs.tab', for bootstrap 2 use 'shown' in the next line
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                // save the latest tab; use cookies if you like 'em better:
                localStorage.setItem('lastTab', $(this).attr('href'));
            });

            // go to the latest tab, if it exists:
            var lastTab = localStorage.getItem('lastTab');
            if (lastTab) {
                $('[href="' + lastTab + '"]').tab('show');
            }
        });
    </script>--%>
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
    <style type="text/css">
        .MyOwnMD {
            width: 9.66666667%;
        }
    </style>
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
    <script type="text/javascript">
        function changeClass() {
            document.getElementById('tab_Tools').className = 'tab-pane active';
            document.getElementById('tab_general1').className = 'tab-pane';
        }
    </script>
    <script type="text/javascript">
        function openModal(x) {
            $('#small').modal('show');
            document.getElementById("lblmsgpop").innerHTML = x;
        }
    </script>
    <script type="text/javascript">
        function openModalsmall2(x) {
            $('#small2').modal('show');
            document.getElementById("lblmsgpop2").innerHTML = x;
        }
    </script>


    <script type="text/javascript">
        function Temp(x) {
            $('#Template').modal('show');
            //document.getElementById("HTMLTEMP1").innerText = x;
            document.getElementById("img").src = x;
        }
    </script>

    <script type="text/javascript">
        function TempHTML(x) {
            $('#Template').modal('show');
            document.getElementById("HTMLTEMP1").innerHTML = x;
        }
    </script>

    <script type="text/javascript">
        function PRBarBlock() {
            document.getElementById("Bar").style.display = "block";
        }

        function PRBar() {
            document.getElementById("Bar").style.display = "none";
        }
    </script>
    <%-- dd --%>
    <style type="text/css">
        /* Dropdown Button */
        .dropbtn {
            background-color: #5b9bd1;
            color: white;
            padding: 16px;
            font-size: 16px;
            border: none;
            height: 35px;
        }

        /* The container <div> - needed to position the dropdown content */
        .dropdown {
            position: relative;
            display: inline-block;
        }

        /* Dropdown Content (Hidden by Default) */
        .dropdown-content {
            display: none;
            position: relative;
            background-color: #f1f1f1;
            min-width: 110px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            z-index: 1;

         
        }
         
            /* Links inside the dropdown */
            .dropdown-content a {
                color: black;
                padding: 12px 16px;
                text-decoration: none;
                display: block;
            }

                /* Change color of dropdown links on hover */
                .dropdown-content a:hover {
                    background-color: #ddd;
                }

        /* Show the dropdown menu on hover */
        .dropdown:hover .dropdown-content {
            display: block;
        }

        /* Change the background color of the dropdown button when the dropdown content is shown */
        .dropdown:hover .dropbtn {
            background-color: #3e8e41;
            height: 35px;
        }
    </style>
    <script type="text/javascript">

        function getFocus() {
            $('#ContentPlaceHolder1_chkIsCmny').focus();
        }
    </script>
    <script type="text/javascript">
        function Block() {
            $.blockUI({ message: '<h2><img src="../assets/image_1210200.gif" />  Processing...</h2>' });
        }
        function BlockStop() {
            $.unblockUI();
        }
    </script>
  
    <%-- dd --%>

    <%--<link href="../assets/global/plugins/bootstrap-modal/css/bootstrap-modal-bs3patch.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../assets/global/plugins/bootstrap-modal/css/bootstrap-modal.css" rel="stylesheet" type="text/css" />--%>
</asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div id="b" runat="server">
            <%--<ul class="page-breadcrumb breadcrumb">
            <li>
                <a href="index.aspx">
                    <asp:Label ID="lblCRm" runat="server" Text="CRM" meta:resourcekey="lblCRmResource1"></asp:Label></a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <a href="#">
                    <asp:Label ID="lblCompCRm" runat="server" Text="Company Master" meta:resourcekey="lblCompCRmResource1"></asp:Label>
                </a>
            </li>
        </ul>--%>

            <!-- BEGIN BODY -->

            <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                <div class="alert alert-success alert-dismissable">
                    <button aria-hidden="true" data-dismiss="alert" type="button" class="close"></button>
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
                                    <%--<asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Company Classification Is Required" ControlToValidate="tags_1" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorWebsite" runat="server" ErrorMessage="Website Is Required" ControlToValidate="txtWebsite" ValidationGroup="Submit" meta:resourcekey="RequiredFieldValidatorWebsiteResource1"></asp:RequiredFieldValidator>--%>
                                    <%--<%--<asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Remark Is Required" ControlToValidate="txtRemark" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="regvalidateStartDate" runat="server" ControlToValidate="txtBirthdate" ForeColor="Red" ErrorMessage="Invalid Date Birthday" SetFocusOnError="True" ValidationExpression="[0-9]{2}[-|\/]{1}[0-9]{2}[-|\/]{1}[0-9]{4}" ValidationGroup="Submit" Enabled="true">Invalid Date Birthday</asp:RegularExpressionValidator>
                                    <div class="form-wizard">
                                        <div class="tabbable">
                                            <asp:Panel ID="PanalFBImage" Visible="false" runat="server">
                                                <ul class="nav nav-pills nav-justified steps" style="margin-bottom: 0px; padding-bottom: 0px; padding-top: 0px;">
                                                    <li style="width: 135px; text-align: center">
                                                        <asp:ImageButton ID="ImgFront" runat="server" Style="width: 30%;" />
                                                        <%--<img src="Upload/FICON.png" style="width: 30%;" data-target="#long" data-toggle="modal" />--%>
                                                        <br />
                                                        <asp:Label ID="front" runat="server" Text="Front END"></asp:Label>
                                                    </li>
                                                    <li style="width: 135px; text-align: center">
                                                        <%--<img src="Upload/BICON.png" style="width: 30%;" data-target="#Back" data-toggle="modal" />--%>
                                                        <asp:ImageButton ID="ImgBack" runat="server" Style="width: 30%;" />
                                                        <br />
                                                        <asp:Label ID="back" runat="server" Text="Back END"></asp:Label>
                                                    </li>
                                                    <li style="width: 80px;" id="attechment" runat="server">
                                                        <asp:LinkButton ID="btnattmentmst" Visible="false" runat="server" CssClass="btn green-haze btn-circle" Style="padding-top: 7px; padding-bottom: 7px; font-size: 11px;" OnClick="btnOpportunity_Click">
                                                        Attachment
                                                    </asp:LinkButton>
                                                        <span class="badge badge-default" style="background-color: #f3565d; color: #fff; position: relative; float: right; top: -24px; right: -20px;">
                                                            <asp:Label ID="lblAttecmentcount" runat="server"></asp:Label>
                                                        </span>
                                                    </li>
                                                </ul>

                                            </asp:Panel>


                                            <ul class="nav nav-pills nav-justified steps" style="margin-bottom: 0px; padding-bottom: 0px; padding-top: 0px;">
                                                <li class="active" style="width: 150px" id="licomditel" runat="server">

                                                    <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_general1" class="step" data-toggle="tab">
                                                        <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">1 </span><span class="desc">
                                                            <asp:Label ID="lblBusinessCo" runat="server" Text="Business Contact" meta:resourcekey="lblBusinessCoResource1"></asp:Label>
                                                        </span></a>
                                                </li>
                                                <li class="" style="width: 150px" id="libisnde" runat="server">
                                                    <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_meta" class="step" data-toggle="tab">
                                                        <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">2 </span><span class="desc">
                                                            <asp:Label ID="lblBusinessDetai" runat="server" Text="Business Details" meta:resourcekey="lblBusinessDetaiResource1"></asp:Label></span></a></li>
                                                <li style="width: 150px">
                                                    <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_images" class="step" data-toggle="tab">
                                                        <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">3 </span><span class="desc">
                                                            <asp:Label ID="lblWebExistance" runat="server" Text="Web Existance" meta:resourcekey="lblWebExistanceResource1"></asp:Label></span></a>&nbsp;
                                                </li>
                                                <li style="width: 150px">
                                                    <a style="color: #5b9bd1; padding: 0px; width: 150px" href="#tab_reviews" class="step" data-toggle="tab">
                                                        <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">4 </span><span class="desc">
                                                            <asp:Label ID="lblWorkingEmploy" runat="server" Text="Working Employees" meta:resourcekey="lblWorkingEmployResource1"></asp:Label></span> </a>
                                                </li>
                                                <li style="width: 150px;" id="toolss" runat="server">
                                                    <a style="color: #5b9bd1; padding: 0px; width: 150px" href="#tab_Tools" class="step" data-toggle="tab">
                                                        <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">5 </span>
                                                        <br />
                                                        <span class="desc">
                                                            <asp:Label ID="lblTools" runat="server" Text="Tools Details" meta:resourcekey="lblWorkingEmployResource1"></asp:Label></span> </a>
                                                </li>
                                                <li style="width: 80px">
                                                    <asp:Button ID="Button1" class="btn btn-circle btn-primary" Style="padding-top: 7px; margin-left: 11px; padding-bottom: 7px; font-size: 10px;" runat="server" Text="Add New" OnClick="Button1_Click" meta:resourcekey="btnSubmitResource1" />
                                                    <asp:Button ID="btnSubmit" class="btn btn-circle btn-primary" Style="padding-top: 7px; margin-left: 11px; padding-bottom: 7px; font-size: 10px" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSave_Click" meta:resourcekey="btnSubmitResource1" Visible="false" />
                                                    <asp:Button ID="btnSaveandConti" Visible="false" runat="server" Text="Save & continues" Style="padding-top: 7px; margin-left: -5px; padding-bottom: 7px; font-size: 10px; margin-top: 2px;" class="btn btn-circle btn-success" OnClick="btnSaveandConti_Click" />
                                                </li>

                                                <li style="width: 80px">

                                                    <asp:Button ID="btnCancel" class="btn btn-circle btn-default" Style="padding-top: 7px; padding-bottom: 7px; font-size: 11px" runat="server" Text="Cancel" OnClick="btnCancel_Click" meta:resourcekey="btnCancelResource1" />
                                                    <asp:HiddenField ID="TabName" runat="server" />
                                                </li>

                                                <li style="width: 40px">
                                                    <asp:LinkButton ID="LanguageEnglish" Style="color: #5b9bd1; width: 40px; padding: 0px;" runat="server">E&nbsp;
<img src="/assets/global/img/flags/us.png" /></asp:LinkButton>
                                                    &nbsp;
                                                </li>
                                                <li style="width: 40px">
                                                    <asp:LinkButton ID="LanguageArabic" Style="color: #5b9bd1; width: 40px; padding: 0px;" runat="server">A&nbsp;
<img src="/assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                    &nbsp; </li>
                                                <li style="width: 40px">
                                                    <asp:LinkButton ID="LanguageFrance" Style="color: #5b9bd1; width: 40px; padding: 0px;" runat="server">F&nbsp;
<img src="/assets/global/img/flags/fr.png" /></asp:LinkButton>
                                                </li>

                                            </ul>

                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general1">
                                                    <div class="portlet box blue">
                                                        <div class="portlet-title">
                                                            <div class="caption">
                                                                <i class="fa fa-gift"></i>
                                                                <asp:Label ID="lblBusContactDe" runat="server" Text="Business Company Details" meta:resourcekey="lblBusContactDeResource1"></asp:Label>
                                                            </div>
                                                            <div class="tools">
                                                                <a href="javascript:;" class="collapse"></a>
                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                <a href="javascript:;" class="reload"></a>
                                                                <a href="javascript:;" class="remove"></a>
                                                            </div>
                                                            <div class="actions btn-set">
                                                                <asp:Button ID="ImageReader" runat="server" Text="Text Reader" CssClass="btn btn-sm yellow-crusta" OnClick="ImageReader_Click" style="display: none;" />
                                                                <div id="navigation" runat="server" class="btn-group btn-group-circle btn-group-solid">
                                                                    <asp:Button ID="btnFirst" class="btn red" runat="server" Text="First" OnClick="btnFirst_Click1" />
                                                                    <asp:Button ID="btnNext" class="btn green" runat="server" Text="Next" OnClick="btnNext_Click" />
                                                                    <asp:Button ID="btnPrev" class="btn purple" runat="server" Text="Prev" OnClick="btnPrev_Click" />
                                                                    <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />
                                                                </div>
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

                                                                            <div class="col-md-3">

                                                                                <label runat="server" id="Label92" class="col-md-5 control-label getshow">
                                                                                    <asp:Label ID="Label93" runat="server" Text="Customer Id" meta:resourcekey="lblTyptResource1"></asp:Label>

                                                                                </label>
                                                                                <div class="col-md-7">
                                                                                    <asp:TextBox ID="txtCustoID" AutoPostBack="true" OnTextChanged="txtCustoID_TextChanged" Enabled="false" runat="server" CssClass="form-control" meta:resourcekey="txtCustomerNameResource1"></asp:TextBox>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">

                                                                                <label runat="server" id="Label13" class="col-md-4 control-label getshow">
                                                                                    <asp:Label ID="Label101" runat="server" Text="Reference:" meta:resourcekey="lblTyptResource1"></asp:Label>

                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:DropDownList ID="drpdatasource" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-2">
                                                                                <%--<div class="col-md-12" style="left: 76px; right: 0px; width: 282px;">--%>
                                                                                <asp:Label ID="Label11" runat="server" Text="" meta:resourcekey="Label11Resource1"></asp:Label>
                                                                                &nbsp;(<asp:CheckBox ID="chkIsCmny" Text="Is a Freelancer?" runat="server" onClick="forRentClicked(this)" meta:resourcekey="chkIsCmnyResource1" />
                                                                                )
                                                                            <%--</div>--%>
                                                                            </div>

                                                                            <div class="col-md-4">

                                                                                <label runat="server" id="lbl13" class="col-md-4 control-label getshow">
                                                                                    <asp:Label ID="lblTypt" runat="server" Text="Type:" meta:resourcekey="lblTyptResource1"></asp:Label>
                                                                                    <span class="required">* </span>

                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:Label ID="lblType" runat="server" meta:resourcekey="lblTypeResource1"></asp:Label>
                                                                                    <asp:DropDownList ID="drpType" runat="server" CssClass="form-control select2me" meta:resourcekey="drpTypeResource1"></asp:DropDownList>
                                                                                </div>
                                                                                <label runat="server" id="lbl12" class="col-md-4 control-label gethide ">
                                                                                    <asp:Label ID="Label7" runat="server" Text="Type:" meta:resourcekey="lblTyptResource1"></asp:Label>
                                                                                    <span class="required">* </span>

                                                                                </label>
                                                                            </div>

                                                                        </div>

                                                                        <div class="form-group  form-md-checkboxes" style="margin-left: 0px;">
                                                                            <div class="md-checkbox-inline">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <label>
                                                                                                <asp:CheckBox ID="chbIsMinistry" runat="server" meta:resourcekey="chbIsMinistryResource1" />
                                                                                                &nbsp;
                                                                   <asp:Label ID="lblIsMistry" runat="server" Text="Ministry" meta:resourcekey="lblIsMistryResource1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <label>
                                                                                                <asp:CheckBox ID="chbIssMb" runat="server" meta:resourcekey="chbIssMbResource1" />
                                                                                                &nbsp;
                                                                    <asp:Label ID="lblissmb" runat="server" Text="SMB" meta:resourcekey="lblissmbResource1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <label>
                                                                                                <asp:CheckBox ID="chbIsCorporate" runat="server" meta:resourcekey="chbIsCorporateResource1" />
                                                                                                &nbsp;
                                                                    <asp:Label ID="lbliscorporate" runat="server" Text="Corporate" meta:resourcekey="lbliscorporateResource1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <label>
                                                                                                <asp:CheckBox ID="chbInHawally" runat="server" meta:resourcekey="chbInHawallyResource1" />
                                                                                                &nbsp;
                                                                    <asp:Label ID="lblInhawally" runat="server" Text="Local" meta:resourcekey="lblInhawallyResource1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <label>
                                                                                                <asp:CheckBox ID="chbSaler" runat="server" meta:resourcekey="chbSalerResource1" />
                                                                                                &nbsp;
                                                                    <asp:Label ID="lblsaler" runat="server" Text="Saler" meta:resourcekey="lblsalerResource1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <label>
                                                                                                <asp:CheckBox ID="chbBuyer" runat="server" meta:resourcekey="chbBuyerResource1" />
                                                                                                &nbsp;
                                                                    <asp:Label ID="lblbuyer" runat="server" Text="Buyer" meta:resourcekey="lblbuyerResource1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <label>
                                                                                                <asp:CheckBox ID="chbSaleDeProd" runat="server" meta:resourcekey="chbSaleDeProdResource1" />
                                                                                                &nbsp;
                                                                    <asp:Label ID="lblsaledeprod" runat="server" Text="Sale OEM Product" meta:resourcekey="lblsaledeprodResource1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <label>
                                                                                                <asp:CheckBox ID="chbEmailSub" runat="server" meta:resourcekey="chbEmailSubResource1" />
                                                                                                &nbsp;
                                                                    <asp:Label ID="lblemailsub" runat="server" Text="Subscribed to Email" meta:resourcekey="lblemailsubResource1"></asp:Label></label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group" style="margin-bottom: 0px;">
                                                                            <div class="col-md-12">

                                                                                <label runat="server" id="l11" class=" control-label col-md-2 getshow">
                                                                                    <asp:Label ID="Label9" runat="server" Text="Company Name:" meta:resourcekey="lblComanyn1Resource1"></asp:Label>
                                                                                    <span class="required">* </span>

                                                                                </label>
                                                                                <div class="col-md-10" style="padding-left: 7px;">
                                                                                    <div class="input-group">
                                                                                        <asp:TextBox ID="txtCustomerName" runat="server" name="name" MaxLength="100" placeholder="Company Name" CssClass="form-control" meta:resourcekey="txtCustomerNameResource1" AutoPostBack="true" OnTextChanged="txtCustomerName_TextChanged"></asp:TextBox>
                                                                                        <span class="input-group-btn">
                                                                                            <%--<asp:Button ID="btnCustomerN1" class="btn blue" runat="server" Text="Check" OnClick="btnCustomerN1_Click" meta:resourcekey="btnCustomerN1Resource1" Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                               
                                                                                        </span>
                                                                                        <asp:LinkButton ID="lkbCustomerN1" runat="server" OnClick="btnCustomerN1_Click">
                                                                                            <i class="icon-arrow-right" style="color: black; padding-left: 4px;"></i>
                                                                                        </asp:LinkButton>

                                                                                    </div>
                                                                                    <asp:Label ID="lblCustomerName" runat="server" ForeColor="Green" meta:resourcekey="lblCustomerNameResource1"></asp:Label>
                                                                                    <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorCustomerName" runat="server" ErrorMessage="Company Name Is Required" ControlToValidate="txtCustomerName" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <label runat="server" id="l1" class=" control-label col-md-2 gethide">
                                                                                    <asp:Label ID="lblComanyn1" runat="server" Text="Company Name:" meta:resourcekey="lblComanyn1Resource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group" style="margin-bottom: 0px;">
                                                                            <div class="col-md-12">

                                                                                <label runat="server" id="lbl1" class="control-label col-md-2 getshow">
                                                                                    <asp:Label ID="lblCompanyN2" runat="server" Text=" Company Lang 2:" meta:resourcekey="lblCompanyN2Resource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                                <div class="col-md-10" style="padding-left: 7px;">
                                                                                    <div class="input-group">
                                                                                        <%--<asp:TextBox ID="txtCustomer" placeholder="اسم الشخص" runat="server" AutoCompleteType="Disabled" class="arabic form-control" TextLanguage="Arabic"></asp:TextBox>--%>
                                                                                        <Lang:LangTextBox ID="txtCustomer" MaxLength="100" runat="server" AutoCompleteType="Disabled" CssClass="arabic form-control" placeholder="اسم الشخص" AutoPostBack="true" TextLanguage="Arabic" meta:resourcekey="txtCustomerResource1" OnTextChanged="txtCustomer_TextChanged"></Lang:LangTextBox>
                                                                                        <span class="input-group-btn">
                                                                                            <%--<asp:Button ID="btnCompanyN2" class="btn blue " runat="server" Text="Check" OnClick="btnCompanyN2_Click" meta:resourcekey="btnCompanyN2Resource1" Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                               
                                                                                        </span>

                                                                                        <asp:LinkButton ID="lkbCustomerN2" runat="server" OnClick="btnCompanyN2_Click">
                                                                                            <i class="icon-arrow-right" style="color: black; padding-left: 4px;"></i>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                    <asp:Label ID="lblCustomerL1" runat="server" ForeColor="Green" meta:resourcekey="lblCustomerL1Resource1"></asp:Label>
                                                                                    <%-- <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorCustomer" runat="server" ErrorMessage="Company Name Other Language 1 Is Required" ControlToValidate="txtCustomer" ValidationGroup=""></asp:RequiredFieldValidator>--%>
                                                                                </div>
                                                                                <label runat="server" id="lbl2" class="control-label col-md-2 gethide  ">
                                                                                    <asp:Label ID="Label20" runat="server" Text=" Company Name Lang  2:" meta:resourcekey="lblCompanyN2Resource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                            </div>

                                                                        </div>
                                                                        <div class="form-group" style="margin-bottom: 0px;">
                                                                            <div class="col-md-12">

                                                                                <label runat="server" id="lbl3" class="col-md-2 control-label getshow ">
                                                                                    <asp:Label ID="lblCompanyN3" runat="server" Text="Company Lang 3:" meta:resourcekey="lblCompanyN3Resource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                                <div class="col-md-10" style="padding-left: 7px;">
                                                                                    <div class="input-group">
                                                                                        <asp:TextBox ID="txtCustomer2" MaxLength="100" placeholder="Company Name  Language 3" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCustomer2_TextChanged" meta:resourcekey="txtCustomer2Resource1"></asp:TextBox>
                                                                                        <span class="input-group-btn">
                                                                                            <%-- <asp:Button ID="btncompnyN3" class="btn blue " runat="server" Text="Check" OnClick="btncompnyN3_Click" meta:resourcekey="btncompnyN3Resource1" Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                
                                                                                        </span>
                                                                                        <asp:LinkButton ID="lkbcompnyN3" runat="server" OnClick="btncompnyN3_Click">
                                                                                            <i class="icon-arrow-right" style="color: black; padding-left: 4px;"></i>
                                                                                        </asp:LinkButton>
                                                                                    </div>
                                                                                    <asp:Label ID="lblCustomerL2" runat="server" ForeColor="Green" meta:resourcekey="lblCustomerL2Resource1"></asp:Label>
                                                                                    <%--<asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Company Name Other Language 2 Is Required" ControlToValidate="txtCustomer2" ValidationGroup=""></asp:RequiredFieldValidator>--%>
                                                                                </div>

                                                                                <label runat="server" id="Label10" class="col-md-2 control-label gethide ">
                                                                                    <asp:Label ID="Label23" runat="server" Text="Company Name Lang 3:" meta:resourcekey="lblCompanyN3Resource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                        <asp:HiddenField ID="Regulervalue" runat="server" />
                                                                        <%-- <asp:LinkButton ID="btntest4" Visible="false" class="btn blue" runat="server"></asp:LinkButton>--%>
                                                                        <asp:Panel ID="ReceivedSign1" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto">

                                                                            <div class="row" style="position: fixed; left: 20%; top: 5%; width: auto">
                                                                                <div class="col-md-12">
                                                                                    <div class="portlet box purple">
                                                                                        <div class="portlet-title">
                                                                                            <div class="caption">
                                                                                                <i class="fa fa-globe"></i>
                                                                                                All Ready Exit
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="portlet-body">
                                                                                            <div class="tabbable">
                                                                                                <div class="tab-content no-space">
                                                                                                    <div class="form-body">

                                                                                                        <div class="tabbable">
                                                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                                                <thead>
                                                                                                                    <tr>
                                                                                                                        <th>Company Name</th>
                                                                                                                        <th>Mobile Number</th>
                                                                                                                        <th>Email</th>
                                                                                                                        <th>Fax Number</th>
                                                                                                                        <th>Busphone</th>
                                                                                                                        <th>User Name</th>

                                                                                                                    </tr>
                                                                                                                </thead>
                                                                                                                <tbody>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="labelCopop" runat="server"></asp:Label></td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblmopop" runat="server"></asp:Label></td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblEmailpop" runat="server"></asp:Label></td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblFaxpop" runat="server"></asp:Label></td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblBuspop" runat="server"></asp:Label></td>
                                                                                                                        <td>
                                                                                                                            <asp:Label ID="lblusernamepop" runat="server"></asp:Label></td>
                                                                                                                    </tr>
                                                                                                                </tbody>
                                                                                                            </table>
                                                                                                        </div>
                                                                                                        <div class="modal-footer">
                                                                                                            <asp:Button ID="btnYes" runat="server" CssClass="btn green-haze btn-circle" Text="Yes" OnClick="btnYes_Click" />
                                                                                                            <asp:Button ID="btnNo" runat="server" CssClass="btn red-haze btn-circle" Text="No" />
                                                                                                            <%--OnClick="btnNo_Click"--%>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </asp:Panel>

                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" DynamicServicePath=""
                                                                            BackgroundCssClass="modalBackground" CancelControlID="btnNo" Enabled="True"
                                                                            PopupControlID="ReceivedSign1" TargetControlID="Regulervalue">
                                                                        </cc1:ModalPopupExtender>
                                                                        <asp:UpdatePanel ID="updCompny" runat="server" UpdateMode="Conditional">
                                                                            <contenttemplate>
                                                                            <div class="form-group">
                                                                                <div class="col-md-6">
                                                                                    <label runat="server" id="lbl21" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="lblCountry" runat="server" Text="Country:" meta:resourcekey="lblCountryResource1"></asp:Label>
                                                                                        <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="Label6" runat="server" meta:resourcekey="Label6Resource1"></asp:Label>
                                                                                        <asp:DropDownList ID="drpCountry" runat="server" CssClass="form-control select2me" meta:resourcekey="drpCountryResource1" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged" AutoPostBack="true">
                                                                                        </asp:DropDownList>
                                                                                        <%-- <asp:TextBox ID="TextBox4" runat="server" class="form-control"></asp:TextBox>
                                                                                        --%>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" Display="Dynamic" runat="server" ForeColor="Red" ErrorMessage="Country Is Required" ControlToValidate="drpCountry" InitialValue="0" ValidationGroup="ValiCont"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <label runat="server" id="Label24" class="col-md-4 control-label gethide ">
                                                                                        <asp:Label ID="Label25" runat="server" Text="Country:" meta:resourcekey="lblCountryResource1"></asp:Label>
                                                                                        <%-- <span class="required">* </span>--%>
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <label runat="server" id="lbl6" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="Label12" runat="server" Text="Postal Code :" meta:resourcekey="Label12Resource1"></asp:Label>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="lblPostalCode" runat="server" meta:resourcekey="lblPostalCodeResource1"></asp:Label>
                                                                                        <asp:TextBox ID="txtPostalCode" MaxLength="40" placeholder="Postal Code" runat="server" CssClass="form-control" meta:resourcekey="txtPostalCodeResource1"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtPostalCode" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                                    </div>
                                                                                    <label runat="server" id="Label26" class="col-md-4 control-label gethide ">
                                                                                        <asp:Label ID="Label27" runat="server" Text="Postal Code :" meta:resourcekey="Label12Resource1"></asp:Label>
                                                                                    </label>
                                                                                </div>

                                                                            </div>
                                                                            <div class="form-group">
                                                                                <div class="col-md-6">
                                                                                    <label runat="server" id="lbl7" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="lblState" runat="server" Text=" State:" meta:resourcekey="lblStateResource1"></asp:Label>
                                                                                        <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="Label8" runat="server" meta:resourcekey="Label8Resource1"></asp:Label>
                                                                                        <asp:DropDownList ID="drpSates" AutoPostBack="true" OnSelectedIndexChanged="drpSates_SelectedIndexChanged" runat="server" CssClass="form-control select2me">
                                                                                        </asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorPostalCode" runat="server" ErrorMessage="State Is Required" InitialValue="0" ControlToValidate="drpSates" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                        <%--<asp:TextBox ID="txtstate" placeholder="State" runat="server" class="form-control" meta:resourcekey="drpStateResource1"></asp:TextBox>--%>
                                                                                    </div>
                                                                                    <label runat="server" id="Label28" class="col-md-4 control-label gethide ">
                                                                                        <asp:Label ID="Label29" runat="server" Text=" State:" meta:resourcekey="lblStateResource1"></asp:Label>
                                                                                        <%-- <span class="required">* </span>--%>
                                                                                    </label>
                                                                                </div>

                                                                                <div class="col-md-6" style="display: none">
                                                                                    <label runat="server" id="Label76" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="Label86" runat="server" Text=" My Country Location:"></asp:Label>
                                                                                        <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="Label87" runat="server"></asp:Label>
                                                                                        <asp:DropDownList ID="drplocation" runat="server" CssClass="form-control select2me">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <label runat="server" id="lbl9" class="col-md-4 control-label getshow">
                                                                                        <asp:Label ID="lblZipCode1" runat="server" Text="Zip Code:" meta:resourcekey="lblZipCode1Resource1"></asp:Label>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="lblZipCode" runat="server" meta:resourcekey="lblZipCodeResource1"></asp:Label>
                                                                                        <asp:TextBox ID="txtZipCode" MaxLength="10" placeholder="ZipCode" runat="server" CssClass="form-control" meta:resourcekey="txtZipCodeResource1"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtZipCode" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                                    </div>
                                                                                    <label runat="server" id="Label30" class="col-md-4 control-label getshow">
                                                                                        <asp:Label ID="Label31" runat="server" Text="Zip Code:" meta:resourcekey="lblZipCode1Resource1"></asp:Label>
                                                                                    </label>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group">
                                                                                <div class="col-md-6">
                                                                                    <label runat="server" id="lbl14" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="lblCity1" runat="server" Text="City:" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="lblCity" runat="server" meta:resourcekey="lblCityResource1"></asp:Label>
                                                                                        <%--<asp:TextBox ID="txtCity" placeholder="City" MaxLength="49" runat="server" class="form-control" meta:resourcekey="txtCityResource1"></asp:TextBox>--%>
                                                                                        <asp:DropDownList ID="drpcity" AutoPostBack="true" OnSelectedIndexChanged="drpcity_SelectedIndexChanged" runat="server" CssClass="form-control select2me">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                    <label runat="server" id="Label32" class="col-md-4 control-label gethide ">
                                                                                        <asp:Label ID="Label33" runat="server" Text="City:" meta:resourcekey="lblCity1Resource1"></asp:Label>

                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <asp:Label ID="lblPACI" runat="server" CssClass="col-md-4 control-label" Text="PACI Number"></asp:Label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:TextBox ID="txtPACI" MaxLength="25" CssClass="form-control" placeholder="PACI Number" runat="server"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtPACI" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </contenttemplate>
                                                                        </asp:UpdatePanel>
                                                                        <div class="form-group">
                                                                            <div class="col-md-6">
                                                                                <label runat="server" id="lbl87" class="col-md-4 control-label getshow ">
                                                                                    <asp:Label ID="lblAddres" runat="server" Text="Address1:" meta:resourcekey="lblAddresResource1"></asp:Label>
                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:Label ID="lblAddress" runat="server" meta:resourcekey="lblAddressResource1"></asp:Label>
                                                                                    <asp:TextBox ID="txtAddress" placeholder="Address1" MaxLength="500" runat="server" CssClass="form-control" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                                                                                </div>
                                                                                <label runat="server" id="Label34" class="col-md-4 control-label gethide ">
                                                                                    <asp:Label ID="Label35" runat="server" Text="Address1:" meta:resourcekey="lblAddresResource1"></asp:Label>
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-6">
                                                                                <label runat="server" id="Label102" class="col-md-4 control-label getshow ">
                                                                                    <asp:Label ID="Label103" runat="server" Text="Civil ID" meta:resourcekey="lblAddres2Resource1"></asp:Label>
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
                                                                            <div class="col-md-6">
                                                                                <label runat="server" id="lbl74" class="col-md-4 control-label getshow ">
                                                                                    <asp:Label ID="lblAddres2" runat="server" Text=" Address2:" meta:resourcekey="lblAddres2Resource1"></asp:Label>
                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:Label ID="lblAddress2" runat="server" meta:resourcekey="lblAddress2Resource1"></asp:Label>
                                                                                    <asp:TextBox ID="txtAddress2" placeholder="Address2" MaxLength="500" runat="server" CssClass="form-control" meta:resourcekey="txtAddress2Resource1"></asp:TextBox>
                                                                                </div>
                                                                                <label runat="server" id="Label36" class="col-md-4 control-label gethide ">
                                                                                    <asp:Label ID="Label37" runat="server" Text=" Address2:" meta:resourcekey="lblAddres2Resource1"></asp:Label>
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-6">
                                                                                <label runat="server" id="Label97" class="col-md-4 control-label getshow ">
                                                                                    <asp:Label ID="Label98" runat="server" Text="Business Start Date" meta:resourcekey="lblAddresResource1"></asp:Label>
                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtBirthdate" placeholder="MM/dd/yyyy" MaxLength="500" runat="server" CssClass="form-control" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBirthdate" Format="MM/dd/yyyy" Enabled="True">
                                                                                    </cc1:CalendarExtender>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="col-md-6">
                                                                                <label runat="server" id="lbl741" class="col-md-4 control-label getshow ">
                                                                                    <asp:Label ID="lblprimaryl" runat="server" Text="Primary Language:" meta:resourcekey="lblprimarylResource1"></asp:Label>
                                                                                    <%--<span class="required">* </span>--%>
                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:Label ID="lblPrimaryLanguage" runat="server" meta:resourcekey="lblPrimaryLanguageResource1"></asp:Label>
                                                                                    <asp:DropDownList ID="drpPrimaryLang" runat="server" CssClass="form-control" meta:resourcekey="drpPrimaryLangResource1"></asp:DropDownList>
                                                                                </div>
                                                                                <label runat="server" id="Label60" class="col-md-4 control-label gethide ">
                                                                                    <asp:Label ID="Label61" runat="server" Text="Primary Language:" meta:resourcekey="lblprimarylResource1"></asp:Label>
                                                                                    <%--<span class="required">* </span>--%>
                                                                                </label>
                                                                            </div>
                                                                            <div class="col-md-6">
                                                                                <div class="input-group" style="margin-left: 10px;">
                                                                                    <span class="input-group-addon">
                                                                                        <i class="icon-link"></i>
                                                                                    </span>
                                                                                    <asp:TextBox ID="txtWebsite" MaxLength="150" placeholder="Website" name="url" runat="server" CssClass="form-control" Style="width: 96%;" meta:resourcekey="txtWebsiteResource1"></asp:TextBox>
                                                                                </div>
                                                                                <%-- <asp:RegularExpressionValidator ID="regUrl" runat="server" ControlToValidate="txtWebsite" ValidationExpression="^((http|https)://)?([\w-]+\.)+[\w]+(/[\w- ./?]*)?$" Text="Enter a valid URL" />  --%>
                                                                                <%--<asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator16" runat="server" ErrorMessage="Website Is Required" ControlToValidate="txtWebsite" ValidationGroup="UrlVelidetion" meta:resourcekey="RequiredFieldValidatorWebsiteResource1"></asp:RequiredFieldValidator>--%>
                                                                                <asp:Label ID="lblWebsite" runat="server" ForeColor="Red" meta:resourcekey="lblWebsiteResource1"></asp:Label>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="col-md-12">

                                                                                <label runat="server" id="lbl22" class="col-md-2 control-label getshow ">
                                                                                    <asp:Label ID="lblEmail" runat="server" Text="EMAIL:" meta:resourcekey="lblEmailResource1"></asp:Label>
                                                                                    <%--<span class="required">* </span>--%>
                                                                                </label>
                                                                                <div class="col-md-10">
                                                                                    <div class="input-group">
                                                                                        <asp:TextBox ID="tags_2" runat="server" MaxLength="500" name="email" CssClass="form-control tags" AutoPostBack="true" OnTextChanged="tags_2_TextChanged"></asp:TextBox>
                                                                                        <span class="input-group-btn">
                                                                                            <asp:LinkButton ID="lbkEmail" runat="server" OnClick="btnEmail_Click">
                                                                                                <i class="icon-arrow-right" style="color: black; padding-left: 4px;"></i>
                                                                                            </asp:LinkButton>
                                                                                            <%--<asp:Button ID="btnEmail" class="btn blue" runat="server" Text="Check" OnClick="btnEmail_Click" meta:resourcekey="btnEmailResource1" Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                        </span>

                                                                                    </div>
                                                                                    <%--<asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator10" runat="server" ErrorMessage="Email Is Required" ControlToValidate="tags_2" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                                                    <asp:Label ID="lblEmail12" runat="server" ForeColor="Green" meta:resourcekey="lblEmail12Resource1"></asp:Label>
                                                                                </div>
                                                                                <label runat="server" id="Label38" class="col-md-2 control-label gethide ">
                                                                                    <asp:Label ID="Label39" runat="server" Text="EMAIL:" meta:resourcekey="lblEmailResource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="col-md-12">
                                                                                <label runat="server" id="lbl55" class="col-md-2 control-label getshow ">
                                                                                    <asp:Label ID="lblFax" runat="server" Text="Fax:" meta:resourcekey="lblFaxResource1"></asp:Label>
                                                                                </label>
                                                                                <div class="col-md-10">
                                                                                    <div class="input-group">
                                                                                        <asp:TextBox ID="tags_3" name="number" MaxLength="500" runat="server" CssClass="form-control tags" meta:resourcekey="tags_3Resource1"></asp:TextBox>
                                                                                        <span class="input-group-btn">
                                                                                            <asp:LinkButton ID="lkbFax" runat="server" ValidationGroup="ValiCont" OnClick="btnFax_Click">
                                                                                                <i class="icon-arrow-right" style="color: black; padding-left: 4px;"></i>
                                                                                            </asp:LinkButton>
                                                                                            <%--<asp:Button ID="btnFax" class="btn blue " runat="server" Text="Check" OnClick="btnFax_Click" meta:resourcekey="btnFaxResource1" Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                        </span>
                                                                                    </div>
                                                                                    <%--<asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator9" runat="server" ErrorMessage="Fax Is Required" ControlToValidate="tags_3" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                                                    <asp:Label ID="Label21" runat="server" ForeColor="Green" meta:resourcekey="Label21Resource1"></asp:Label>
                                                                                </div>
                                                                                <label runat="server" id="Label40" class="col-md-2 control-label gethide ">
                                                                                    <asp:Label ID="Label41" runat="server" Text="Fax:" meta:resourcekey="lblFaxResource1"></asp:Label>
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="col-md-12">
                                                                                <label runat="server" id="lbl65" class="col-md-2 control-label getshow ">
                                                                                    <asp:Label ID="lblBusPhone" runat="server" Text=" Bus Phone:" meta:resourcekey="lblBusPhoneResource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                                <div class="col-md-10">
                                                                                    <div class="input-group">
                                                                                        <asp:TextBox ID="tags_4" name="number" MaxLength="500" runat="server" CssClass="form-control tags" meta:resourcekey="tags_4Resource1"></asp:TextBox>
                                                                                        <span class="input-group-btn">
                                                                                            <asp:LinkButton ID="lbkBusPhone" ValidationGroup="ValiCont" runat="server" OnClick="btnBusPhone_Click">
                                                                                                <i class="icon-arrow-right" style="color: black; padding-left: 4px;"></i>
                                                                                            </asp:LinkButton>
                                                                                            <%--<asp:Button ID="btnBusPhone" class="btn blue" runat="server" Text="Check" OnClick="btnBusPhone_Click" meta:resourcekey="btnBusPhoneResource1" Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                        </span>
                                                                                    </div>
                                                                                    <%--<asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator8" runat="server" ErrorMessage="BusPhone Is Required" ControlToValidate="tags_4" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                                                    <asp:Label ID="Label22" runat="server" ForeColor="Red" meta:resourcekey="Label22Resource1"></asp:Label>
                                                                                </div>
                                                                                <label runat="server" id="Label42" class="col-md-2 control-label gethide">
                                                                                    <asp:Label ID="Label43" runat="server" Text=" Bus Phone:" meta:resourcekey="lblBusPhoneResource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="col-md-12">

                                                                                <label runat="server" id="lbl75" class="col-md-2 control-label getshow ">
                                                                                    <asp:Label ID="lblMobileNo1" runat="server" Text=" Mobile No:" meta:resourcekey="lblMobileNo1Resource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                                <div class="col-md-10">
                                                                                    <div class="input-group">
                                                                                        <asp:TextBox ID="txtMobileNo" placeholder="Mobile No" MaxLength="150" runat="server" CssClass="form-control tags" meta:resourcekey="txtMobileNoResource1" AutoPostBack="true" OnTextChanged="txtMobileNo_TextChanged2"></asp:TextBox>

                                                                                        <span class="input-group-btn"></span>
                                                                                        <asp:LinkButton ID="lkbcheck" ValidationGroup="ValiCont" runat="server" OnClick="btnMobile_Click">
                                                                                            <i class="icon-arrow-right" style="color: black; padding-left: 4px;"></i>
                                                                                        </asp:LinkButton>


                                                                                    </div>

                                                                                    <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Mobile No Is Required" ControlToValidate="txtMobileNo" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                    <asp:Label ID="lblMobileNo" runat="server" ForeColor="Green" meta:resourcekey="lblMobileNoResource1"></asp:Label>

                                                                                </div>
                                                                                <label runat="server" id="Label44" class="col-md-2 control-label gethide ">
                                                                                    <asp:Label ID="Label45" runat="server" Text=" Mobile No:" meta:resourcekey="lblMobileNo1Resource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="col-md-12">

                                                                                <label runat="server" id="lbl78" class="col-md-2 control-label getshow">
                                                                                    <asp:Label ID="lblComClass" runat="server" Text="Company Classification :" meta:resourcekey="lblComClassResource1"></asp:Label>
                                                                                    <%-- <span class="required">* </span>--%>
                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <div class="input-group">
                                                                                        <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1"></asp:Label>
                                                                                        <asp:DropDownList ID="drpCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged" CssClass="form-control select2me" meta:resourcekey="drpCategoryResource1">
                                                                                        </asp:DropDownList>
                                                                                        <span class="input-group-btn">
                                                                                            <%--<asp:Button ID="btnCompanyCl" class="btn blue " runat="server" Text="ADD" OnClick="btnCompanyCl_Click" meta:resourcekey="btnCompanyClResource1" Style="padding-top: 7px; padding-bottom: 7px; margin-left: 257px; border-left-width: 0px; padding-left: 25px;" />--%>
                                                                                        </span>
                                                                                        <asp:LinkButton ID="libtnNewClass" runat="server"><i class="icon-plus">New Main</i> </asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                                <label runat="server" id="Label46" class="col-md-2 control-label gethide">
                                                                                    <asp:Label ID="Label47" runat="server" Text="Company Classification :" meta:resourcekey="lblComClassResource1"></asp:Label>
                                                                                    <%--   <span class="required">* </span>--%>
                                                                                </label>
                                                                                <%--<div class="col-md-4" style="margin-left: 30px;">
                                                                        <asp:Button ID="btnCompanyCl" class="btn blue " runat="server" Text="ADD" OnClick="btnCompanyCl_Click" meta:resourcekey="btnCompanyClResource1" Style="padding-top: 7px; padding-bottom: 7px" />
                                                                    </div>--%>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="col-md-12">
                                                                                <label runat="server" id="lbl79" class="col-md-2 control-label getshow ">
                                                                                    <asp:Label ID="lblClassifica" runat="server" Text="Classification:" meta:resourcekey="lblClassificaResource1"></asp:Label>
                                                                                </label>
                                                                                <div class="col-md-10">
                                                                                    <asp:TextBox ID="tags_1" MaxLength="250" name="number" runat="server" CssClass="form-control tags" meta:resourcekey="tags_1Resource1"></asp:TextBox>
                                                                                    <%-- <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Company Classification Is Required" ControlToValidate="tags_1" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                                                </div>
                                                                                <label runat="server" id="Label48" class="col-md-2 control-label gethide ">
                                                                                    <asp:Label ID="Label49" runat="server" Text="Classification:" meta:resourcekey="lblClassificaResource1"></asp:Label>
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="col-md-12">
                                                                                <asp:Label ID="lblMarketing" runat="server" Text="Company Marketing" CssClass="col-md-2 control-label"></asp:Label>
                                                                                <div class="col-md-4">
                                                                                    <asp:DropDownList ID="drpmarketing" runat="server" CssClass="form-control select2me" AutoPostBack="true" OnSelectedIndexChanged="drpmarketing_SelectedIndexChanged"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="col-md-12">
                                                                                <asp:Label ID="lblremarket" runat="server" Text="Marketing" CssClass="col-md-2 control-label"></asp:Label>
                                                                                <div class="col-md-10">
                                                                                    <asp:TextBox ID="txtrefreshno" MaxLength="500" CssClass="form-control tag" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="col-md-12">
                                                                                <label runat="server" id="lblrem123" class="col-md-2 control-label getshow ">
                                                                                    <asp:Label ID="lblremark1" runat="server" Text="Remark:" meta:resourcekey="lblremark1Resource1"></asp:Label>
                                                                                    <%--<span class="required">* </span>--%>
                                                                                </label>
                                                                                <div class="col-md-10">
                                                                                    <asp:Label ID="lblRemark" runat="server" meta:resourcekey="lblRemarkResource1"></asp:Label>
                                                                                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" MaxLength="250" placeholder="Remark" runat="server" CssClass="form-control" meta:resourcekey="txtRemarkResource1"></asp:TextBox>
                                                                                </div>
                                                                                <label runat="server" id="Label64" class="col-md-2 control-label gethide ">
                                                                                    <asp:Label ID="Label65" runat="server" Text="Remark:" meta:resourcekey="lblremark1Resource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                        <asp:Panel ID="pnlMultiSize" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto">

                                                                            <div class="row" style="position: fixed; left: 20%; top: 5%; width: auto">
                                                                                <div class="col-md-12">
                                                                                    <div class="portlet box purple">
                                                                                        <div class="portlet-title">
                                                                                            <div class="caption">
                                                                                                <i class="fa fa-globe"></i>
                                                                                                Mian Classification
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="portlet-body">
                                                                                            <div class="tabbable">
                                                                                                <div class="tab-content no-space">
                                                                                                    <div class="form-body">

                                                                                                        <div class="form-group">
                                                                                                            <div class="col-md-12">
                                                                                                                <label runat="server" id="lblrecod" class="col-md-4 control-label  ">
                                                                                                                    <asp:Label ID="Label63" runat="server" Text="Main Classification Name:" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                    <span class="required">* </span>
                                                                                                                </label>
                                                                                                                <div class="col-md-8">
                                                                                                                    <asp:TextBox ID="txtclassficname" placeholder="Main Classification Name" runat="server" CssClass="form-control" meta:resourcekey="txtCityResource1"></asp:TextBox>
                                                                                                                    <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator18" runat="server" ErrorMessage="Main Classification Name Is Required" ControlToValidate="txtclassficname" ValidationGroup="classfiction"></asp:RequiredFieldValidator>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="modal-footer">
                                                                                                            <asp:LinkButton ID="btnMinClassfication" CssClass="btn green-haze modalBackgroundbtn-circle" ValidationGroup="classfiction" runat="server" OnClick="btnMinClassfication_Click"> Save</asp:LinkButton>
                                                                                                            <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                            <asp:Button ID="Button9" runat="server" CssClass="btn btn-default" Text="Cancel" />

                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </asp:Panel>
                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button9" Enabled="True" PopupControlID="pnlMultiSize" TargetControlID="libtnNewClass"></cc1:ModalPopupExtender>
                                                                        <div class="form-group">
                                                                            <div class="col-md-12">
                                                                                <label runat="server" id="Label58" class=" control-label col-md-2 getshow">
                                                                                    <asp:Label ID="Label59" runat="server" Text="BarCode:" meta:resourcekey="lblBarCodeResource1"></asp:Label>
                                                                                    <span class="required">* </span>
                                                                                </label>
                                                                                <div class="col-md-4">
                                                                                    <asp:TextBox ID="txtBarCode" runat="server" name="name" MaxLength="100" placeholder="BarCode" CssClass="form-control" meta:resourcekey="txtBarCodeResource1"></asp:TextBox>
                                                                                    <asp:Label ID="Label88" runat="server" ForeColor="Green" meta:resourcekey="lblBarCodeResource1"></asp:Label>
                                                                                    <%-- <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Company Name Is Required" ControlToValidate="txtCustomerName" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                                                    <label runat="server" id="Label89" class=" control-label col-md-2 gethide">
                                                                                        <asp:Label ID="Label90" runat="server" Text="BarCode:" meta:resourcekey="lblBarCodeResource1"></asp:Label>
                                                                                        <span class="required">* </span>
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <asp:PlaceHolder ID="plBarCode" runat="server" />
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                        <div class="form-group" style="margin-left: -10px; margin-right: -10px;">
                                                                            <div class="col-md-12">

                                                                                <label runat="server" id="lbl70" class="col-md-2 control-label getshow">
                                                                                    <asp:Label ID="Label5" runat="server" Text="Avtar:"></asp:Label>
                                                                                    <%-- <span class="required">* </span>--%>
                                                                                </label>
                                                                                <div class="col-md-2">
                                                                                    <asp:Image ID="Avatar" Style="width: 50px; height: 50px;" runat="server" ImageUrl="~/Gallery/defolt.png" CssClass="img-responsive" meta:resourcekey="AvatarResource1" />

                                                                                </div>
                                                                                <div class="col-md-4">
                                                                                    <asp:FileUpload ID="avatarUploadd" class="btn btn-circle green-haze btn-sm" runat="server" onchange="previewFile()" meta:resourcekey="avatarUploaddResource1" />
                                                                                </div>
                                                                                <label runat="server" id="Label50" class="col-md-2 control-label gethide">
                                                                                    <asp:Label ID="Label51" runat="server" Text="Avtar:"></asp:Label>
                                                                                    <%-- <span class="required">* </span>--%>
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
                                                                <asp:Label ID="lblBCDeta" runat="server" Text="Business Company Details " meta:resourcekey="lblBCDetaResource1"></asp:Label>
                                                            </div>
                                                            <div class="tools">
                                                                <a href="javascript:;" class="collapse"></a>
                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                <a href="javascript:;" class="reload"></a>
                                                                <a href="javascript:;" class="remove"></a>
                                                            </div>
                                                            <div class="actions btn-set">
                                                                <a href="#tab_general1" class="step" data-toggle="tab" aria-expanded="true">
                                                                    <asp:Label ID="Label94" runat="server" Text="previous" class="btn yellow btn-circle"></asp:Label>
                                                                </a>

                                                                <a href="#tab_images" class="step" data-toggle="tab" aria-expanded="true">
                                                                    <asp:Label ID="Label14" runat="server" Text="Next " class="btn red btn-circle"></asp:Label>
                                                                </a>
                                                            </div>
                                                        </div>
                                                        <div class="portlet-body">
                                                            <div class="tabbable">
                                                                <div class="tab-content no-space">
                                                                    <div class="form-body">
                                                                        <div class="form-group">
                                                                            <div class="col-md-12">

                                                                                <label runat="server" id="lbl121" class="col-md-2 control-label getshow ">
                                                                                    <asp:Label ID="Label1" runat="server" Text="Company Brand :"></asp:Label>
                                                                                    <%--<span class="required">* </span>--%>
                                                                                </label>
                                                                                <div class="col-md-4">
                                                                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                                    <asp:DropDownList ID="drpbrand" AutoPostBack="true" OnSelectedIndexChanged="drpbrand_SelectedIndexChanged" runat="server" CssClass="form-control select2me">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="drpbrand" ErrorMessage="Company Brand Required." InitialValue="0" ValidationGroup="Braed"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <label runat="server" id="Label52" class="col-md-2 control-label gethide ">
                                                                                    <asp:Label ID="Label53" runat="server" Text="Company Brand :"></asp:Label>
                                                                                    <%--<span class="required">* </span>--%>
                                                                                </label>
                                                                                <div class="col-md-4">
                                                                                    <asp:LinkButton ID="LinkButton6" Visible="false" runat="server" ValidationGroup="Braed" OnClick="btnBared_Click">
                                                                                        <i class="icon-plus " style="color: black; padding-left: 4px;"></i>
                                                                                    </asp:LinkButton>
                                                                                    <asp:LinkButton ID="LinkButton14" class="btn btn-warning" runat="server">
                                                                                        <i class="fa fa-plus">New Brand</i>
                                                                                    </asp:LinkButton>
                                                                                    <%--  <asp:Button ID="btnBared" class="btn blue " runat="server"  Text="ADD" Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                </div>

                                                                            </div>

                                                                        </div>


                                                                        <asp:Panel ID="PenalAddBrand" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto">

                                                                            <div class="row" style="position: fixed; left: 20%; top: 3%; width: auto">
                                                                                <div class="col-md-12">
                                                                                    <div class="portlet box purple">
                                                                                        <div class="portlet-title">
                                                                                            <div class="caption">
                                                                                                <i class="fa fa-globe"></i>
                                                                                                Add Brand
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="portlet-body">
                                                                                            <div class="tabbable">
                                                                                                <div class="tab-content no-space">
                                                                                                    <div class="form-body">

                                                                                                        <div class="form-group">
                                                                                                            <div class="col-md-12">
                                                                                                                <label runat="server" id="Label99" class="col-md-4 control-label  ">
                                                                                                                    <asp:Label ID="Label100" runat="server" Text="Brand Name:" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                    <span class="required">* </span>
                                                                                                                </label>
                                                                                                                <div class="col-md-8">
                                                                                                                    <asp:TextBox ID="txtBrand" placeholder="Brand Name" runat="server" CssClass="form-control" meta:resourcekey="txtCityResource1"></asp:TextBox>
                                                                                                                    <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Brand Name Is Required" ControlToValidate="txtBrand" ValidationGroup="Brand"></asp:RequiredFieldValidator>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="modal-footer">
                                                                                                            <asp:LinkButton ID="btnSaveBrand" CssClass="btn green-haze modalBackgroundbtn-circle" ValidationGroup="Brand" runat="server" OnClick="btnSaveBrand_Click"> Save</asp:LinkButton>
                                                                                                            <asp:Button ID="btncancelBrand" runat="server" class="btn btn-default" Text="Cancel" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="btncancelBrand" Enabled="True" PopupControlID="PenalAddBrand" TargetControlID="LinkButton14"></cc1:ModalPopupExtender>
                                                                        <div class="form-group">
                                                                            <div class="col-md-12">
                                                                                <label runat="server" id="lbl456" class="col-md-2 control-label getshow ">
                                                                                    <asp:Label ID="Label4" runat="server" Text="Brand:"></asp:Label>
                                                                                </label>
                                                                                <div class="col-md-10">
                                                                                    <asp:TextBox ID="tags_5" name="number" MaxLength="250" runat="server" CssClass="form-control tags" meta:resourcekey="tags_1Resource1"></asp:TextBox>
                                                                                </div>
                                                                                <label runat="server" id="Label54" class="col-md-2 control-label gethide ">
                                                                                    <asp:Label ID="Label55" runat="server" Text="Brand:"></asp:Label>
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="col-md-6">
                                                                                <label runat="server" id="lbl147" class="col-md-4 control-label getshow">
                                                                                    <asp:Label ID="lblMyproductId" runat="server" Text="My Product:" meta:resourcekey="lblMyproductIdResource1"></asp:Label>
                                                                                    <%--<span class="required">* </span>--%>
                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:Label ID="lblProductId" runat="server" meta:resourcekey="lblProductIdResource1"></asp:Label>
                                                                                    <asp:DropDownList ID="drpMyProductId" runat="server" CssClass="form-control input-medium select2me" meta:resourcekey="drpMyProductIdResource1"></asp:DropDownList>
                                                                                </div>
                                                                                <label runat="server" id="Label56" class="col-md-4 control-label gethide">
                                                                                    <asp:Label ID="Label57" runat="server" Text="My Product Id:" meta:resourcekey="lblMyproductIdResource1"></asp:Label>
                                                                                    <%-- <span class="required">* </span>--%>
                                                                                </label>
                                                                            </div>
                                                                            <%-- YOGESH<div class="col-md-6">

                                                                            <label runat="server" id="lbl564" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="lblMCLID" runat="server" Text=" My Country Location:" meta:resourcekey="lblMCLIDResource1"></asp:Label>
                                                                              
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="lblCountryloc" runat="server" meta:resourcekey="lblCountrylocResource1"></asp:Label>
                                                                                <asp:DropDownList ID="drpMyCounLocID" runat="server" class="form-control" meta:resourcekey="drpMyCounLocIDResource1">
                                                                                </asp:DropDownList>

                                                                            </div>
                                                                            <label runat="server" id="Label58" class="col-md-4 control-label gethide ">
                                                                                <asp:Label ID="Label59" runat="server" Text=" My Country Location:" meta:resourcekey="lblMCLIDResource1"></asp:Label>
                                                                               
                                                                            </label>
                                                                        </div>--%>
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
                                                                <asp:Label ID="lblwExistance" runat="server" Text="Web Existance" meta:resourcekey="lblwExistanceResource1"></asp:Label>
                                                            </div>
                                                            <div class="tools">
                                                                <a href="javascript:;" class="collapse"></a>
                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                <a href="javascript:;" class="reload"></a>
                                                                <a href="javascript:;" class="remove"></a>
                                                            </div>
                                                            <div class="actions btn-set">

                                                                <a href="#tab_meta" class="step" data-toggle="tab" aria-expanded="true">
                                                                    <asp:Label ID="Label95" runat="server" Text="previous" class="btn purple btn-circle"></asp:Label>
                                                                </a>

                                                                <a href="#tab_reviews" class="step" data-toggle="tab" aria-expanded="true">
                                                                    <asp:Label ID="Label62" runat="server" Text="Next " class="btn blue btn-circle"></asp:Label>
                                                                </a>
                                                            </div>

                                                        </div>
                                                        <div class="portlet-body">

                                                            <div class="tabbable">
                                                                <div class="tab-content no-space">
                                                                    <div class="form-body">
                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                            <contenttemplate>
                                                                            <asp:Panel ID="panelMsg" runat="server" Visible="false">
                                                                                <div class="alert alert-danger alert-dismissable">
                                                                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                                    <asp:Label ID="lblErreorMsg" runat="server"></asp:Label>
                                                                                </div>
                                                                            </asp:Panel>

                                                                            <div class="form-group">
                                                                                <div class="col-md-6">
                                                                                    <label runat="server" id="lbl72" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="lblcususername" runat="server" Text=" Customer UserName:" meta:resourcekey="lblcususernameResource1"></asp:Label>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <div class="input-group">
                                                                                            <asp:TextBox ID="txtcUserName" placeholder="Customer UserName" runat="server" MaxLength="25" CssClass="form-control" meta:resourcekey="txtcUserNameResource1" AutoPostBack="true" OnTextChanged="txtcUserName_TextChanged"></asp:TextBox>
                                                                                            <span class="input-group-btn">
                                                                                                <%-- <asp:Button ID="btnUserName" class="btn purple " runat="server" Text="Check" ValidationGroup="username"  meta:resourcekey="btnUserNameResource1" Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                            </span>
                                                                                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="btnUserName_Click">
                                                                                               <i class="icon-arrow-right" style="color:black;padding-left: 4px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <asp:RegularExpressionValidator Display="Dynamic" ID="rev" runat="server" ControlToValidate="txtcUserName"
                                                                                            ErrorMessage="Spaces are not allowed!" ValidationGroup="username" ForeColor="Red" ValidationExpression="[^\s]+" meta:resourcekey="revResource1" />
                                                                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtcUserName" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{6,15}$" runat="server" ValidationGroup="username" ForeColor="Red" ErrorMessage="Minimum 6 and Maximum 15 characters required." meta:resourcekey="RegularExpressionValidator3Resource1"></asp:RegularExpressionValidator>
                                                                                        <asp:Label ID="lblcUserName" runat="server" ForeColor="Red" meta:resourcekey="lblcUserNameResource1"></asp:Label>
                                                                                    </div>
                                                                                    <label runat="server" id="Label66" class="col-md-4 control-label gethide ">
                                                                                        <asp:Label ID="Label67" runat="server" Text=" Customer UserName:" meta:resourcekey="lblcususernameResource1"></asp:Label>
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <label runat="server" id="lbl521" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="lblCustopassword" runat="server" Text=" Customer Password:" meta:resourcekey="lblCustopasswordResource1"></asp:Label>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="lblcPassword" runat="server" meta:resourcekey="lblcPasswordResource1"></asp:Label>
                                                                                        <asp:TextBox ID="txtcPassword" placeholder="Customer Password" runat="server" MaxLength="25" CssClass="form-control" meta:resourcekey="txtcPasswordResource1"></asp:TextBox>
                                                                                        <%--<asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator4" runat="server" ForeColor="Red" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                                                                                            ErrorMessage="Minimum 8 characters atleast 1 Alphabet, 1 Number and 1 Special Character" ControlToValidate="txtcPassword" meta:resourcekey="RegularExpressionValidator4Resource1"></asp:RegularExpressionValidator>--%>
                                                                                    </div>
                                                                                    <label runat="server" id="Label68" class="col-md-4 control-label gethide ">
                                                                                        <asp:Label ID="Label69" runat="server" Text=" Customer Password:" meta:resourcekey="lblCustopasswordResource1"></asp:Label>
                                                                                    </label>
                                                                                </div>
                                                                            </div>
                                                                        </contenttemplate>
                                                                        </asp:UpdatePanel>

                                                                        <asp:UpdatePanel ID="Sosielmediya" runat="server">
                                                                            <contenttemplate>
                                                                            <div class="form-group">
                                                                                <div class="col-md-6">
                                                                                    <label runat="server" id="lbl7521" class="col-md-4 control-label getshow">
                                                                                        <asp:Label ID="Label17" runat="server" Text="Social Media:"></asp:Label>
                                                                                        <%-- <span class="required">* </span>--%>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="Label18" runat="server" meta:resourcekey="Label3Resource1"></asp:Label>
                                                                                        <asp:DropDownList ID="drpSomib" runat="server" CssClass="form-control">
                                                                                        </asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="drpSomib" ErrorMessage="Social Media Required." InitialValue="0" ValidationGroup="sosieyl"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <label runat="server" id="Label70" class="col-md-4 control-label gethide">
                                                                                        <asp:Label ID="Label71" runat="server" Text="Social Media:"></asp:Label>
                                                                                        <%-- <span class="required">* </span>--%>
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="Label19" runat="server"></asp:Label>
                                                                                        <div class="input-group">
                                                                                            <asp:TextBox ID="txtSocial" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                            <span class="input-group-btn">
                                                                                                <%--<asp:LinkButton ID="LinkButton7" runat="server" ValidationGroup="sosieyl" OnClick="btnSocial_Click">
                                                                                    <i class="icon-plus " style="color:black;padding-left: 4px;"></i>
                                                                                                </asp:LinkButton>--%>
                                                                                            </span>
                                                                                        </div>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="txtSocial" ErrorMessage="Social Media Id  Required." ValidationGroup="sosieyl"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Button ID="btnsocial" CssClass="btn yellow" runat="server" OnClick="btnSocial_Click" Text="Add" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="tabbable">
                                                                                <table class="table table-striped table-bordered table-hover">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Social Media</th>
                                                                                            <th>Social Media Id</th>
                                                                                            <th>Remark</th>
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
                                                                                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%# getshocial(Convert .ToInt32 ( Eval("Recource")))%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("RecValue")%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%# getremark(Convert .ToInt32 ( Eval("Recource")))%>'></asp:Label></td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>

                                                                                    </tbody>
                                                                                </table>

                                                                            </div>
                                                                        </contenttemplate>
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
                                                                <asp:Label ID="lblWEmp" runat="server" Text="Working Employees" meta:resourcekey="lblWEmpResource1"></asp:Label>
                                                            </div>
                                                            <div class="tools">
                                                                <a href="javascript:;" class="collapse"></a>
                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                <a href="javascript:;" class="reload"></a>
                                                                <a href="javascript:;" class="remove"></a>
                                                            </div>

                                                            <div class="actions btn-set">
                                                                <a href="#tab_images" class="step" data-toggle="tab" aria-expanded="true">
                                                                    <asp:Label ID="Label96" runat="server" Text="Previous" class="btn red btn-circle"></asp:Label>
                                                                </a>
                                                                <asp:Button ID="btnFinish" class="btn purple btn-circle" runat="server" Text="Finish" ValidationGroup="Submit" OnClick="btnSave_Click " />
                                                            </div>
                                                        </div>
                                                        <div class="portlet-body">
                                                            <div class="tabbable">
                                                                <div class="tab-content no-space">
                                                                    <div class="form-body">
                                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                            <contenttemplate>
                                                                            <div class="form-group">
                                                                                <div class="col-md-2" style="margin-left: 5px;">
                                                                                    <div class="input-group">
                                                                                        <asp:TextBox ID="txtcompneySerch" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Contact Search" MaxLength="250">
                                                                                        </asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtcompneySerch" ErrorMessage="Company Name Required." Display="Dynamic" ForeColor="Red" ValidationGroup="Compniserch"></asp:RequiredFieldValidator>
                                                                                        <span class="input-group-btn"></span>
                                                                                        <asp:LinkButton ID="LinkButton12" CssClass="btn btn-icon-only yellow" runat="server" Style="margin-top: -28px;" ValidationGroup="Compniserch" OnClick="LinkButton12_Click">
                                                                                 <i class="fa fa-search" ></i>
                                                                                        </asp:LinkButton>

                                                                                    </div>
                                                                                    <asp:Label ID="lblcountserch" runat="server" ForeColor="Green"></asp:Label>
                                                                                </div>
                                                                                <div class="col-md-5">

                                                                                    <label runat="server" id="lbl5214" class="col-md-3 control-label getshow ">
                                                                                        <asp:Label ID="Label15" runat="server" Text="Employee Name:"></asp:Label>
                                                                                        <%-- <span class="required">* </span>--%>
                                                                                    </label>
                                                                                    <div class="col-md-8">

                                                                                        <asp:Label ID="Label16" runat="server" meta:resourcekey="Label3Resource1"></asp:Label>
                                                                                        <div class="input-group">
                                                                                            <asp:DropDownList ID="drpCompnay" runat="server" CssClass="form-control select2me" OnSelectedIndexChanged="drpCompnay_SelectedIndexChanged" AutoPostBack="true" meta:resourcekey="drpCategoryResource1">
                                                                                            </asp:DropDownList>
                                                                                            <span class="input-group-btn">
                                                                                                <asp:LinkButton ID="LinkButton13" runat="server" OnClick="LinkButton13_Click" OnClientClick="return confirm('Do you want to Add Employ?')" Style="display: none;">
                                                                                    <i class="icon-plus ">&nbsp;&nbsp;&nbsp;Employee</i>
                                                                                                </asp:LinkButton>
                                                                                                <asp:LinkButton ID="LinkAddEmployee" runat="server" Style="display: none;">
                                                                                                     <i class="icon-plus ">&nbsp;&nbsp;&nbsp;Employee</i>
                                                                                                </asp:LinkButton>

                                                                                                <%-- <asp:Button ID="btnAddCobus" class="btn blue " runat="server" Text="Add"  Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                            </span>
                                                                                        </div>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="drpCompnay" ErrorMessage="Employ Name Required." InitialValue="0" ValidationGroup="WorEnploy"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <label runat="server" id="Label74" class="col-md-4 control-label gethide ">
                                                                                        <asp:Label ID="Label75" runat="server" Text="Employ Name:"></asp:Label>
                                                                                        <%-- <span class="required">* </span>--%>
                                                                                    </label>
                                                                                </div>

                                                                                <div class="col-md-4">

                                                                                    <label runat="server" id="lbl632" class="col-md-4 control-label getshow">
                                                                                        <asp:Label ID="lblItmanager1" runat="server" Text="Position:" meta:resourcekey="lblItmanager1Resource1"></asp:Label>
                                                                                        <%--<span class="required">* </span>--%>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="lblitManager" runat="server" meta:resourcekey="lblitManagerResource1"></asp:Label>
                                                                                        <div class="input-group">
                                                                                            <asp:DropDownList ID="drpItManager" runat="server" CssClass="form-control select2me" meta:resourcekey="drpItManagerResource1"></asp:DropDownList>
                                                                                            <span class="input-group-btn">
                                                                                                <a data-toggle="modal" href="#responsive">
                                                                                                    <i class="icon-plus " style="color: black; padding-left: 5px; margin-top: 10px; padding-right: 5px;"></i>
                                                                                                </a>
                                                                                                <asp:LinkButton ID="LinkButton8" runat="server" ValidationGroup="WorEnploy" OnClick="btnAddCobus_Click">
                                                                                    <%--<i class="icon-plus " style="color:black;padding-left: 4px;"></i>--%>
                                                                                                 <img src="../ECOMM/images/Add.png" style="padding-bottom: 12px;"/>
                                                                                                </asp:LinkButton>
                                                                                                <%-- <asp:Button ID="btnAddCobus" class="btn blue " runat="server" Text="Add"  Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                            </span>
                                                                                        </div>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="drpItManager" ErrorMessage="Position Required." InitialValue="0" ValidationGroup="WorEnploy"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <label runat="server" id="Label72" class="col-md-4 control-label gethide">
                                                                                        <asp:Label ID="Label73" runat="server" Text="Position:" meta:resourcekey="lblItmanager1Resource1"></asp:Label>
                                                                                        <%-- <span class="required">* </span>--%>
                                                                                    </label>
                                                                                </div>

                                                                            </div>

                                                                            <%-- t --%>
                                                                            <panel id="PanelAddEmployee" style="padding: 1px; background-color: #fff; border: 2px solid purple; display: none; overflow: auto;" runat="server" cssclass="modalPopup">
                                                                              <div class="modal-dialog" style="position:fixed;left:30%;top:20px">
                     <div class="modal-content" style="width:400px;">
                                                                                    <div class="portlet box purple">
                                                                                        <div class="modal-header">

                                                                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                                                                            <h4 class="modal-title" style="color: white;">Add New Employee</h4>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="modal-body">
                                                                                        <div class="scroller" style="height: 300px" data-always-visible="1" data-rail-visible1="1">
                                                                                            <div class="row">

                                                                                                <div class="col-md-12">
                                                                                                    <p>
                                                                                                        <strong style="font-size: larger; margin-left: 10px;">Employee Name<span class="required">* </span></strong>
                                                                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" name="txtPOSENG"></asp:TextBox>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="EMP" Display="Dynamic" ForeColor="#a94442" ControlToValidate="TextBox1" ErrorMessage="Employee Name required"></asp:RequiredFieldValidator>
                                                                                                    </p>
                                                                                                    <p>
                                                                                                        <strong style="font-size: larger; margin-left: 10px;">EMail_ID<span class="required">* </span></strong>
                                                                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="EMP" Display="Dynamic" ForeColor="#a94442" ControlToValidate="TextBox2" ErrorMessage="EMail_ID required"></asp:RequiredFieldValidator>
                                                                                                    </p>
                                                                                                    <p>
                                                                                                        <strong style="font-size: larger; margin-left: 10px;">Mobile NO<span class="required">* </span></strong>
                                                                                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="EMP" Display="Dynamic" ForeColor="#a94442" ControlToValidate="TextBox3" ErrorMessage="Mobile NO required"></asp:RequiredFieldValidator>
                                                                                                    </p>
                                                                                                    <p>
                                                                                                         <strong style="font-size: larger; margin-left: 10px;">Position<span class="required">* </span></strong>
                                                                                                        <asp:DropDownList ID="DrpPopPosition" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                                        
                                                                                                    </p>
                                                                                                    <p>
                                                                                                        <%--<img id="Bar" src="../assets/admin/layout4/img/image_1210200.gif" style="width:100%;height:18px;display:none;"/>--%>
                                                                                                        <img id="Bar" src="Upload/image_1210200.gif" style="width:100%;height:18px;display:none;"/>
                                                                                                    </p>
                                                                                                </div>
                                                                                            </div>
                                                                                            
                                                                                        </div>
                                                                                    </div>
                                                                                                                                                                               
                                                                                    <div class="modal-footer" style="background-color: lightgray;">
                                                                                         <asp:Button ID="AddEmployeeCancel" runat="server" Text="Cancel" CssClass="btn btn-sm red"></asp:Button>
                                                                                        <asp:Button ID="btnEMPSAVE" runat="server" class="btn btn-sm green" Text="Save" ValidationGroup="EMP" OnClick="btnEMPSAVE_Click"  OnClientClick="PRBarBlock();"/>
                                                                                    </div>

                                                                                </div>
                                                                            </div>

                                                                                

                                                                            </panel>
                                                                            <cc1:ModalPopupExtender ID="ModalPopupExtender6" runat="server" DynamicServicePath=""
                                                                                BackgroundCssClass="modalBackground" CancelControlID="AddEmployeeCancel" Enabled="True"
                                                                                PopupControlID="PanelAddEmployee" TargetControlID="LinkAddEmployee">
                                                                            </cc1:ModalPopupExtender>
                                                                            <%-- t --%>
                                                                            <div class="tabbable">
                                                                                <table class="table table-striped table-bordered table-hover">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Company Name</th>
                                                                                            <th>Employee</th>
                                                                                            <th>Position</th>
                                                                                            <th>E_mail</th>
                                                                                            <th>Mobile No</th>
                                                                                            <th>State</th>
                                                                                            <th>City</th>
                                                                                            <th colspan="2">Action</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:ListView ID="lstCompniy" runat="server" OnItemCommand="lstCompniy_ItemCommand" OnItemDataBound="lstCompniy_ItemDataBound">
                                                                                            <LayoutTemplate>
                                                                                                <tr id="ItemPlaceholder" runat="server">
                                                                                                </tr>
                                                                                            </LayoutTemplate>
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%#getcompniy(Convert .ToInt32 ( Eval("CompID")))%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblEMP" runat="server" Text='<%# GetEmployee(Convert.ToInt32(Eval("ContactMyID")),Convert.ToInt32(Eval("CompID"))) %>'></asp:Label>
                                                                                                        <asp:TextBox ID="txtEMP" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# GetPossition(Convert.ToInt32(Eval("JobTitle"))) %>'></asp:Label>
                                                                                                        <asp:Label ID="Label117" runat="server" Visible="false" Text='<%# Eval("JobTitle") %>'></asp:Label>
                                                                                                        <asp:DropDownList ID="DrpEDTPosition" runat="server" Visible="false" CssClass="form-control select2me"></asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%# Eval("email2")%>'></asp:Label>
                                                                                                        <asp:TextBox ID="txtEMAIL" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblMOBPHONE" runat="server" Text='<%# Eval("BusPhone1")%>'></asp:Label>
                                                                                                        <asp:TextBox ID="txtMOBPHONE" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblSTATE" runat="server" Text='<%# getstate(Convert.ToInt32(Eval("CompID")))%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblCITY" runat="server" Text='<%# getcity(Convert.ToInt32(Eval("CompID")))%>'></asp:Label></td>
                                                                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <td>
                                                                                                                <asp:LinkButton ID="LinkEMPEDIT" runat="server" CommandName="LinkEMPEDIT" CommandArgument='<%# Eval("ContactMyID")+"-"+Eval("CompID") %>'>
                                                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="../ECOMM/images/editRec.png" />
                                                                                                                </asp:LinkButton>
                                                                                                                <asp:LinkButton ID="LinkEditSave" Visible="false" runat="server" CommandName="LinkEditSave" CommandArgument='<%# Eval("ContactMyID")+"-"+Eval("CompID") %>' Style="width: 28px; height: 22px;" CssClass="btn btn-sm btn-success">
                                                                                                            <i class="fa fa-check"></i>
                                                                                                                </asp:LinkButton>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:LinkButton ID="LinkEMPDELETE" runat="server" CommandName="LinkEMPDELETE" CommandArgument='<%# Eval("ContactMyID")+"-"+Eval("CompID") %>'>
                                                                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="../ECOMM/images/deleteRec.png" />
                                                                                                                </asp:LinkButton>
                                                                                                            </td>
                                                                                                        </ContentTemplate>
                                                                                                        <Triggers>
                                                                                                            <asp:AsyncPostBackTrigger ControlID="LinkEMPEDIT" />
                                                                                                            <asp:AsyncPostBackTrigger ControlID="LinkEditSave" />
                                                                                                            <asp:AsyncPostBackTrigger ControlID="LinkEMPDELETE" />
                                                                                                        </Triggers>
                                                                                                    </asp:UpdatePanel>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>



                                                                        </contenttemplate>
                                                                        </asp:UpdatePanel>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane" id="tab_Tools">
                                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                        <contenttemplate>
                                                        <div class="form-group">
                                                            <div class="col-md-12 col-sm-6">
                                                                <!-- BEGIN PORTLET-->
                                                                <div class="portlet box yellow">
                                                                    <div class="portlet-title tabbable-line">
                                                                        <div class="caption caption-md">
                                                                            <i class="icon-globe theme-font-color hide"></i>
                                                                            <span class="caption-subject bold uppercase">CRM Activity</span>
                                                                        </div>
                                                                        <ul class="nav nav-tabs">
                                                                            <li class="active">
                                                                                <a href="#CRMACT_1_1" data-toggle="tab">Activities<span class="badge badge-danger"><asp:Label ID="CRMNoteCount" runat="server" Text="0"></asp:Label>
                                                                                </span></a>
                                                                            </li>
                                                                            <li>
                                                                                <a href="#CRMACT_1_2" data-toggle="tab">Activities ALL </a>
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                    <div class="portlet-body" style="padding: 12px 20px 15px 20px;">
                                                                        <!--BEGIN TABS-->
                                                                        <div class="tab-content">
                                                                            <div class="tab-pane active" id="CRMACT_1_1">
                                                                                <div class="scroller" style="height: 337px;" data-always-visible="1" data-rail-visible1="0" data-handle-color="#D7DCE2">
                                                                                    <ul class="feeds">
                                                                                        <asp:ListView ID="ListCRMActivitis" runat="server">
                                                                                            <ItemTemplate>
                                                                                                <li>
                                                                                                    <a href="javascript:;">
                                                                                                        <div class="col1">
                                                                                                            <div class="cont">
                                                                                                                <div class="cont-col1">
                                                                                                                    <div class="label label-sm label-success">
                                                                                                                        <i class="fa fa-bell-o"></i>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <div class="cont-col2">
                                                                                                                    <div class="desc">
                                                                                                                        <asp:Label ID="Label133" runat="server" Text='<%# GetACT(Convert.ToInt32(Eval("Parameter2"))) %>'></asp:Label>
                                                                                                                        <asp:Label ID="Label134" runat="server" Text='<%# Eval("ButtionName") %>' CssClass="label label-sm label-default"></asp:Label>

                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="col2">
                                                                                                            <div class="date">
                                                                                                                <asp:Label ID="Label135" Style="color: #2c87b4;font-weight: bold;" runat="server" Text='<%# Convert.ToDateTime(Eval("ReminderDate")).ToString("dd/MMM/yyyy") %>'></asp:Label>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </a>
                                                                                                </li>
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>
                                                                                        <%-- <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-success">
                                                                                                            <i class="fa fa-bell-o"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            You have 4 pending tasks. <span class="label label-sm label-info">Take action <i class="fa fa-share"></i>
                                                                                                            </span>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    Just now
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-danger">
                                                                                                            <i class="fa fa-bolt"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            Database server #12 overloaded. Please fix the issue.
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    24 mins
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-info">
                                                                                                            <i class="fa fa-bullhorn"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            New order received and pending for process.
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    30 mins
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-success">
                                                                                                            <i class="fa fa-bullhorn"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            New payment refund and pending approval.
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    40 mins
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-warning">
                                                                                                            <i class="fa fa-plus"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            New member registered. Pending approval.
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    1.5 hours
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-success">
                                                                                                            <i class="fa fa-bell-o"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            Web server hardware needs to be upgraded. <span class="label label-sm label-default ">Overdue </span>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    2 hours
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-default">
                                                                                                            <i class="fa fa-bullhorn"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            Prod01 database server is overloaded 90%.
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    3 hours
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-warning">
                                                                                                            <i class="fa fa-bullhorn"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            New group created. Pending manager review.
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    5 hours
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-info">
                                                                                                            <i class="fa fa-bullhorn"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            Order payment failed.
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    18 hours
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-default">
                                                                                                            <i class="fa fa-bullhorn"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            New application received.
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    21 hours
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-info">
                                                                                                            <i class="fa fa-bullhorn"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            Dev90 web server restarted. Pending overall system check.
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    22 hours
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-default">
                                                                                                            <i class="fa fa-bullhorn"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            New member registered. Pending approval
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    21 hours
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-info">
                                                                                                            <i class="fa fa-bullhorn"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            L45 Network failure. Schedule maintenance.
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    22 hours
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-default">
                                                                                                            <i class="fa fa-bullhorn"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            Order canceled with failed payment.
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    21 hours
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-info">
                                                                                                            <i class="fa fa-bullhorn"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            Web-A2 clound instance created. Schedule full scan.
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    22 hours
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-default">
                                                                                                            <i class="fa fa-bullhorn"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            Member canceled. Schedule account review.
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    21 hours
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-info">
                                                                                                            <i class="fa fa-bullhorn"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            New order received. Please take care of it.
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    22 hours
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>--%>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                            <div class="tab-pane" id="CRMACT_1_2">
                                                                                <div class="scroller" style="height: 337px;" data-always-visible="1" data-rail-visible1="0" data-handle-color="#D7DCE8">
                                                                                    <ul class="feeds">
                                                                                        <asp:ListView ID="ListCRMActivitisAll" runat="server">
                                                                                            <ItemTemplate>
                                                                                                <li>
                                                                                                    <a href="javascript:;">
                                                                                                        <div class="col1">
                                                                                                            <div class="cont">
                                                                                                                <div class="cont-col1">
                                                                                                                    <div class="label label-sm label-danger">
                                                                                                                        <i class="fa fa-bolt"></i>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <div class="cont-col2">
                                                                                                                    <div class="desc">
                                                                                                                        <asp:Label ID="Label136" runat="server" Text='<%# GetACT(Convert.ToInt32(Eval("Parameter2"))) %>'></asp:Label>
                                                                                                                        <asp:Label ID="Label137" runat="server" Text='<%# Eval("ButtionName") %>' CssClass="label label-sm label-default"></asp:Label>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="col2">
                                                                                                            <div class="date">
                                                                                                                <asp:Label ID="Label138" Style="color: #2c87b4;font-weight: bold;" runat="server" Text='<%# Convert.ToDateTime(Eval("ReminderDate")).ToString("dd/MMM/yyyy") %>'></asp:Label>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </a>
                                                                                                </li>
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>

                                                                                        <%-- <li>
                                                                                            <a href="javascript:;">
                                                                                                <div class="col1">
                                                                                                    <div class="cont">
                                                                                                        <div class="cont-col1">
                                                                                                            <div class="label label-sm label-success">
                                                                                                                <i class="fa fa-bell-o"></i>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="cont-col2">
                                                                                                            <div class="desc">
                                                                                                                New order received
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col2">
                                                                                                    <div class="date">
                                                                                                        10 mins
                                                                                                    </div>
                                                                                                </div>
                                                                                            </a>
                                                                                        </li>
                                                                                        <li>
                                                                                            <div class="col1">
                                                                                                <div class="cont">
                                                                                                    <div class="cont-col1">
                                                                                                        <div class="label label-sm label-danger">
                                                                                                            <i class="fa fa-bolt"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="cont-col2">
                                                                                                        <div class="desc">
                                                                                                            Order #24DOP4 has been rejected. <span class="label label-sm label-danger ">Take action <i class="fa fa-share"></i>
                                                                                                            </span>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col2">
                                                                                                <div class="date">
                                                                                                    24 mins
                                                                                                </div>
                                                                                            </div>
                                                                                        </li>
                                                                                        <li>
                                                                                            <a href="javascript:;">
                                                                                                <div class="col1">
                                                                                                    <div class="cont">
                                                                                                        <div class="cont-col1">
                                                                                                            <div class="label label-sm label-success">
                                                                                                                <i class="fa fa-bell-o"></i>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="cont-col2">
                                                                                                            <div class="desc">
                                                                                                                New user registered
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col2">
                                                                                                    <div class="date">
                                                                                                        Just now
                                                                                                    </div>
                                                                                                </div>
                                                                                            </a>
                                                                                        </li>
                                                                                        <li>
                                                                                            <a href="javascript:;">
                                                                                                <div class="col1">
                                                                                                    <div class="cont">
                                                                                                        <div class="cont-col1">
                                                                                                            <div class="label label-sm label-success">
                                                                                                                <i class="fa fa-bell-o"></i>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="cont-col2">
                                                                                                            <div class="desc">
                                                                                                                New user registered
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col2">
                                                                                                    <div class="date">
                                                                                                        Just now
                                                                                                    </div>
                                                                                                </div>
                                                                                            </a>
                                                                                        </li>
                                                                                        <li>
                                                                                            <a href="javascript:;">
                                                                                                <div class="col1">
                                                                                                    <div class="cont">
                                                                                                        <div class="cont-col1">
                                                                                                            <div class="label label-sm label-success">
                                                                                                                <i class="fa fa-bell-o"></i>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="cont-col2">
                                                                                                            <div class="desc">
                                                                                                                New user registered
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col2">
                                                                                                    <div class="date">
                                                                                                        Just now
                                                                                                    </div>
                                                                                                </div>
                                                                                            </a>
                                                                                        </li>
                                                                                        <li>
                                                                                            <a href="javascript:;">
                                                                                                <div class="col1">
                                                                                                    <div class="cont">
                                                                                                        <div class="cont-col1">
                                                                                                            <div class="label label-sm label-success">
                                                                                                                <i class="fa fa-bell-o"></i>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="cont-col2">
                                                                                                            <div class="desc">
                                                                                                                New user registered
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col2">
                                                                                                    <div class="date">
                                                                                                        Just now
                                                                                                    </div>
                                                                                                </div>
                                                                                            </a>
                                                                                        </li>
                                                                                        <li>
                                                                                            <a href="javascript:;">
                                                                                                <div class="col1">
                                                                                                    <div class="cont">
                                                                                                        <div class="cont-col1">
                                                                                                            <div class="label label-sm label-success">
                                                                                                                <i class="fa fa-bell-o"></i>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="cont-col2">
                                                                                                            <div class="desc">
                                                                                                                New user registered
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col2">
                                                                                                    <div class="date">
                                                                                                        Just now
                                                                                                    </div>
                                                                                                </div>
                                                                                            </a>
                                                                                        </li>
                                                                                        <li>
                                                                                            <a href="javascript:;">
                                                                                                <div class="col1">
                                                                                                    <div class="cont">
                                                                                                        <div class="cont-col1">
                                                                                                            <div class="label label-sm label-success">
                                                                                                                <i class="fa fa-bell-o"></i>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="cont-col2">
                                                                                                            <div class="desc">
                                                                                                                New user registered
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col2">
                                                                                                    <div class="date">
                                                                                                        Just now
                                                                                                    </div>
                                                                                                </div>
                                                                                            </a>
                                                                                        </li>
                                                                                        <li>
                                                                                            <a href="javascript:;">
                                                                                                <div class="col1">
                                                                                                    <div class="cont">
                                                                                                        <div class="cont-col1">
                                                                                                            <div class="label label-sm label-success">
                                                                                                                <i class="fa fa-bell-o"></i>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="cont-col2">
                                                                                                            <div class="desc">
                                                                                                                New user registered
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col2">
                                                                                                    <div class="date">
                                                                                                        Just now
                                                                                                    </div>
                                                                                                </div>
                                                                                            </a>
                                                                                        </li>--%>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <!--END TABS-->
                                                                    </div>
                                                                </div>
                                                                <!-- END PORTLET-->
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-12">
                                                                <div class="portlet box purple-soft">
                                                                    <div class="portlet-title" style="margin-top: 5px;">
                                                                        <div class="caption">
                                                                            <i class="fa fa-gift"></i>Appointment
                                                                        </div>
                                                                        <div class="tools">
                                                                            <a href="javascript:;" class="collapse"></a>
                                                                            <a href="javascript:;" class="reload"></a>
                                                                        </div>
                                                                        <div class="tools" style="padding-top: 7px; padding-bottom: 0px;">
                                                                            <asp:Button ID="btnshowAPP" CssClass="btn btn-sm btn-circle yellow-gold" runat="server" Text="Show Appointment" Style="display: none;" OnClick="btnshowAPP_Click" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="portlet-body form">
                                                                        <div class="form-body">
                                                                            <div class="form-group" style="margin-left: 5px; margin-right: 5px;">

                                                                                <table class="table table-striped table-bordered table-hover dataTables_paginate">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Title</th>
                                                                                            <th>From</th>
                                                                                            <th>To</th>
                                                                                            <th>Status</th>
                                                                                            <th>Appoint Start</th>
                                                                                            <th>Appoint End</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:ListView ID="ListAppint" runat="server" OnItemDataBound="ListAppint_ItemDataBound">
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label118" runat="server" Text='<%# Eval("Title") %>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label119" runat="server" Text='<%# Eval("FromAppoint") %>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label120" runat="server" Text='<%# Eval("ToAppoint") %>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label124" Visible="false" runat="server" Text='<%# Eval("Color") %>'></asp:Label>
                                                                                                        <asp:Label ID="Label121" runat="server" Text=""></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label122" runat="server" Text='<%# Convert.ToDateTime(Eval("StartDt")).ToString("dd-MMM-yyyy") %>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label123" runat="server" Text='<%# Convert.ToDateTime(Eval("EndDt")).ToString("dd-MMM-yyyy") %>'></asp:Label></td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>
                                                                                    </tbody>

                                                                                </table>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-12">
                                                                <div class="portlet box yellow-saffron">
                                                                    <div class="portlet-title" style="margin-top: 5px;">
                                                                        <div class="caption">
                                                                            <i class="fa fa-gift"></i>Note
                                                                        </div>
                                                                        <div class="tools">
                                                                            <a href="javascript:;" class="collapse"></a>
                                                                            <a href="javascript:;" class="reload"></a>
                                                                        </div>
                                                                        <div class="tools" style="padding-top: 7px; padding-bottom: 0px;">
                                                                            <asp:Button ID="btnshownote" CssClass="btn btn-sm btn-circle yellow-gold" runat="server" Text="Show All Note" OnClick="btnshownote_Click" />
                                                                            <asp:Button ID="btnTodayReminder" CssClass="btn btn-sm btn-circle yellow" runat="server" Text="Today Reminder" OnClick="btnTodayReminder_Click" />
                                                                            
                                                                        </div>
                                                                    </div>
                                                                    <div class="portlet-body form">
                                                                        <div class="form-body">
                                                                            <div class="form-group" style="margin-left: 5px; margin-right: 5px;">

                                                                                <table class="table table-striped table-bordered table-hover dataTables_paginate">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Title</th>
                                                                                            <th>Description</th>
                                                                                            <th>From</th>
                                                                                            <th>To</th>
                                                                                            <th>Reminder Date</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:ListView ID="ListMemo" runat="server">
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label125" runat="server" Text='<%# Eval("Title") %>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label126" runat="server" Text='<%# Eval("Description") %>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label127" runat="server" Text='<%# Eval("Switch2From") %>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label128" runat="server" Text='<%# Eval("Switch1To") %>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label132" runat="server" Text='<%# Convert.ToDateTime(Eval("TransactionDate")).ToString("dd/MMM/yyyy") %>'></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>
                                                                                    </tbody>

                                                                                </table>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-12">
                                                                <div class="portlet box green-seagreen">
                                                                    <div class="portlet-title" style="margin-top: 5px;">
                                                                        <div class="caption">
                                                                            <i class="fa fa-gift"></i>Email
                                                                        </div>
                                                                        <div class="tools">
                                                                            <a href="javascript:;" class="collapse"></a>
                                                                            <a href="javascript:;" class="reload"></a>
                                                                        </div>
                                                                        <div class="tools" style="padding-top: 7px; padding-bottom: 0px;">
                                                                            <asp:Button ID="btnShowEmail" CssClass="btn btn-sm btn-circle yellow-gold" runat="server" Style="display: none;" Text="Show Email" OnClick="btnShowEmail_Click" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="portlet-body form">
                                                                        <div class="form-body">
                                                                            <div class="form-group" style="margin-left: 5px; margin-right: 5px;">

                                                                                <table class="table table-striped table-bordered table-hover dataTables_paginate">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>From</th>
                                                                                            <th>To</th>
                                                                                            <th>Description</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:ListView ID="ListEmail" runat="server">
                                                                                            <ItemTemplate>
                                                                                                <tr>

                                                                                                    <td>
                                                                                                        <asp:Label ID="Label129" runat="server" Text='<%# Eval("Switch2From") %>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label130" runat="server" Text='<%# Eval("Switch1To") %>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label131" runat="server" Text='<%# Eval("Description") %>'></asp:Label></td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>
                                                                                    </tbody>

                                                                                </table>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </contenttemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <asp:Panel ID="Panel1" Visible="false" runat="server">
                                                    <div class="page-content-wrapper">
                                                        <div class="form-horizontal form-row-seperated">
                                                            <div class="portlet light">
                                                                <div class="portlet-body">
                                                                    <div class="tabbable">
                                                                        <table>
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>
                                                                                        <asp:Label ID="Label78" runat="server" Text="Company Name"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="Label79" runat="server" Text="Address"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="Label80" runat="server" Text="EMAIL"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="Label81" runat="server" Text="Mobile No"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="Label82" runat="server" Text="State"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="Label83" runat="server" Text="ZipCode"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="Label84" runat="server" Text="City"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="Label85" runat="server" Text="Remark"></asp:Label></th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <asp:ListView ID="listserch" runat="server">
                                                                                    <layouttemplate>
                                                                                    <tr id="ItemPlaceholder" runat="server">
                                                                                    </tr>
                                                                                </layouttemplate>
                                                                                    <itemtemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("COMPNAME") %>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDR1") %>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblEMAIL" runat="server" Text='<%# Eval("EMAIL") %>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblMOBPHONE" runat="server" Text='<%# Eval("MOBPHONE") %>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblSTATE" runat="server" Text='<%# Eval("STATE") %>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblZIPCODE" runat="server" Text='<%# Eval("ZIPCODE") %>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblCITY" runat="server" Text='<%# Eval("CITY") %>'></asp:Label></td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("remarks") %>'></asp:Label></td>
                                                                                    </tr>
                                                                                </itemtemplate>
                                                                                </asp:ListView>

                                                                            </tbody>
                                                                        </table>
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


                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <contenttemplate>
                                <div class="portlet box blue">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-gift"></i>
                                            Company   List
                                        </div>
                                        <div class="tools">
                                            <a href="javascript:;" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <asp:LinkButton ID="LinkButton9" OnClick="btnlistreload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>

                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                        <div class="actions btn-set">
                                            <asp:LinkButton ID="LinkTools" runat="server" CssClass="btn btn-circle yellow-lemon" OnClick="LinkTools_Click">Tools</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton11" class="btn btn-circle btn-warning" runat="server" OnClick="LinkButton11_Click">Advance Search</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="portlet-body ">
                                        <div class="form-body">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group">
                                                        <label runat="server" id="Label106" class="control-label col-md-2 getshow">
                                                            <asp:Label runat="server" ID="Label107">Saved Search</asp:Label>
                                                        </label>
                                                        <div class="col-md-6">
                                                            <asp:DropDownList ID="DrpTitle" runat="server" class="form-control select2me"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Title" ControlToValidate="DrpTitle" ValidationGroup="SaveSearch21" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:CheckBox ID="chDefaultSet" ToolTip="Default Set" runat="server" />
                                                            <asp:Button ID="btnSearch" class="btn btn-circle purple" runat="server" Text="Show" ValidationGroup="SaveSearch21" OnClick="btnSearch_Click" />
                                                            <asp:Button ID="btnAppend" class="btn btn-circle red" runat="server" Text="Append" ValidationGroup="SaveSearch21" OnClick="btnAppend_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label runat="server" id="Label104" class="control-label col-md-2 getshow">
                                                            <asp:Label runat="server" ID="Label105">Title</asp:Label>
                                                        </label>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTitle" ErrorMessage="Title Required." CssClass="Validation" ValidationGroup="SaveSearch"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Button ID="btnSearchSave" class="btn btn-circle blue" runat="server" Text="Save" ValidationGroup="SaveSearch" OnClick="btnSearchSave_Click" />

                                                        </div>

                                                    </div>
                                                </div>
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
                                                                                           <%--<asp:ListItem Value="5" Selected="True">5</asp:ListItem>
                                                                                           <asp:ListItem Value="15">15</asp:ListItem>
                                                                                           <asp:ListItem Value="50">50</asp:ListItem>--%>
                                                                                           <asp:ListItem Value="100" Selected="True">100</asp:ListItem>
                                                                                       </asp:DropDownList>
                                                                                        <%--<select name="sample_1_length" aria-controls="sample_1"  tabindex="-1" title="">
                                                                                            <option value="5">5</option>
                                                                                            <option value="15">15</option>
                                                                                            <option value="20">20</option>
                                                                                            <option value="-1">All</option>
                                                                                        </select>--%>
                                                                                                Entries&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList Style="width: 150px;" class="form-control input-inline " ID="drpSort" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpSort_SelectedIndexChanged">
                                                                                                </asp:DropDownList></label>&nbsp;&nbsp;&nbsp;&nbsp;

                                                                                    Show Active&nbsp;&nbsp;<asp:CheckBox ID="chkactive" runat="server" AutoPostBack="true" OnCheckedChanged="chkactive_CheckedChanged" />
                                                                                </div>
                                                                            </div>
                                                                            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="LinkButton10">

                                                                                <div class="col-md-6 col-sm-12">

                                                                                    <div id="sample_1_filter" class="dataTables_filter">

                                                                                        <label>

                                                                                            <asp:TextBox ID="txtSearch" Placeholder="Search" class="form-control input-small input-inline" runat="server" ToolTip="You Want To Search By Company Name 1,2,3, City,Address,Mobile,Bussiness Phone Or Email"></asp:TextBox>
                                                                                            <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton10_Click" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-search"></i></asp:LinkButton></label>

                                                                                    </div>
                                                                                    <div class="dataTables_filter">
                                                                                        <label>
                                                                                            <asp:DropDownList ID="drpSelectSearch" runat="server" CssClass="form-control select2me input-small">
                                                                                                <asp:ListItem Text="--Select All--" Value="0"></asp:ListItem>
                                                                                                <asp:ListItem Text="Company Name-1" Value="1"></asp:ListItem>
                                                                                                <asp:ListItem Text="Company Name-2" Value="2"></asp:ListItem>
                                                                                                <asp:ListItem Text="Company Name-3" Value="3"></asp:ListItem>
                                                                                                <asp:ListItem Text="Address-1" Value="4"></asp:ListItem>
                                                                                                <asp:ListItem Text="Address-2" Value="5"></asp:ListItem>
                                                                                                <asp:ListItem Text="Classification" Value="6"></asp:ListItem>
                                                                                                <asp:ListItem Text="City" Value="7"></asp:ListItem>
                                                                                                <asp:ListItem Text="State" Value="8"></asp:ListItem>
                                                                                                <asp:ListItem Text="Mobile" Value="9"></asp:ListItem>
                                                                                                <asp:ListItem Text="Bussiness Phone" Value="10"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </label>
                                                                                    </div>
                                                                                </div>
                                                                            </asp:Panel>
                                                                            <asp:Panel ID="pnlToolss" runat="server" Visible="false">
                                                                                <div class="col-md-6 col-sm-12">
                                                                                    <div class="panel panel-info" style="padding-top: 10px; padding-bottom: 10px; padding-left: 10px; padding-right: 10px;">
                                                                                        <a class="btn btn-success " data-toggle="modal" href="#AppointPOP" onclick="getFocus();">Appointment</a>
                                                                                        <a class="btn btn-info" data-toggle="modal" href="#draggable" onclick="getFocus();">Notes</a>

                                                                                        <asp:Button ID="TemplateEmail" class="btn btn-primary" runat="server" Text="Email" OnClick="TemplateEmail_Click" />
                                                                                        <asp:DropDownList ID="drptemplete" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drptemplete_SelectedIndexChanged" Visible="false" CssClass="input-small">
                                                                                        </asp:DropDownList>

                                                                                    </div>
                                                                                </div>
                                                                            </asp:Panel>
                                                                        </div>
                                                                        <div class="table-scrollable">
                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>
                                                                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                                                        <thead class="repHeader">
                                                                                            <tr>
                                                                                                <th style="width: 13%">
                                                                                                    <asp:Label ID="Label77" runat="server" Text="Action" meta:resourcekey="lblActionResource1"></asp:Label>
                                                                                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                                                                                </th>
                                                                                                <th>
                                                                                                    <asp:Label ID="lblCompid" runat="server" Text="ID" meta:resourcekey="lblActionResource1"></asp:Label></th>
                                                                                                <th>
                                                                                                    <asp:Label ID="lblcustemername" runat="server" Text="Customer Name" meta:resourcekey="lblCuNameResource1"></asp:Label></th>
                                                                                                <th>
                                                                                                    <asp:Label ID="lblClass" runat="server" Text="Classification" meta:resourcekey="lblremark3Resource1"></asp:Label></th>
                                                                                                <th>
                                                                                                    <asp:Label ID="lblemaillist" runat="server" Text="E_MAIL" meta:resourcekey="lblemail3Resource1"></asp:Label></th>
                                                                                                <th>
                                                                                                    <asp:Label ID="lblmobiellis" runat="server" Text="MobileNo" meta:resourcekey="lblmobileno3Resource1"></asp:Label>
                                                                                                </th>
                                                                                                <th>
                                                                                                    <asp:Label ID="lblstatelis" runat="server" Text="State" meta:resourcekey="lblState3Resource1"></asp:Label></th>
                                                                                                <%-- <th>
                                                                            <asp:Label ID="lbladdresh" runat="server" Text="Address" meta:resourcekey="lbladdress3Resource1"></asp:Label></th>
                                                                                                                                           
                                                                        </th>                                                                        
                                                                        <th>
                                                                            <asp:Label ID="lblzipcodelit" runat="server" Text="ZipCode" meta:resourcekey="lblzipcode3Resource1"></asp:Label></th>--%>
                                                                                                <th>
                                                                                                    <asp:Label ID="lblcitylit" runat="server" Text="City" meta:resourcekey="lblcity3Resource1"></asp:Label></th>
                                                                                                <%--<th style="width: 10%">
                                                                                                    <asp:Label ID="lblActive" runat="server" Text="Active" meta:resourcekey="lblcity3Resource1"></asp:Label></th>--%>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <asp:ListView ID="Listview1" runat="server" OnItemCommand="ListCustomerMaster_ItemCommand" OnItemDataBound="Listview1_ItemDataBound" DataKey="COMPID" DataKeyNames="COMPID">
                                                                                                <LayoutTemplate>
                                                                                                    <tr id="ItemPlaceholder" runat="server">
                                                                                                    </tr>
                                                                                                </LayoutTemplate>
                                                                                                <ItemTemplate>

                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <table>
                                                                                                                <tr>
                                                                                                                    <%-- <asp:Label ID="lblDropComp" Visible="false" runat="server" Text='<%# Eval("COMPID") %>'></asp:Label>
                                                                                                                    <asp:DropDownList ID="DRPACTION" runat="server" CssClass="form-horizontal select2me input-xsmall" AutoPostBack="true" OnSelectedIndexChanged="DRPACTION_SelectedIndexChanged">
                                                                                                                        <asp:ListItem Value="0" Text="Action"></asp:ListItem>
                                                                                                                        <asp:ListItem Value="1" Text="View"></asp:ListItem>
                                                                                                                        <asp:ListItem Value="2" Text="Edit"></asp:ListItem>
                                                                                                                        <asp:ListItem Value="3" Text="Delete"></asp:ListItem>
                                                                                                                    </asp:DropDownList>--%>

                                                                                                                    <div class="dropdown">

                                                                                                                        <button class="dropbtn" style="padding-bottom: 0px; padding-top: 0px; height: auto">Action</button>
                                                                                                                        <div class="dropdown-content">

                                                                                                                            <asp:LinkButton Style="padding-bottom: 0px; padding-top: 0px; height: auto" ID="linkbtnview" CommandName="btnview" CommandArgument='<%# Eval("COMPID") %>' PostBackUrl='<%# "CompanyMaster.aspx?COMPID="+ Eval("COMPID") %>' runat="server" Text="View">                                                                                                                                    
                                                                                                                            </asp:LinkButton>

                                                                                                                            <asp:LinkButton Style="padding-bottom: 0px; padding-top: 0px; height: auto" ID="LinkButton1" CommandName="btnEdit" CommandArgument='<%# Eval("COMPID") %>' PostBackUrl='<%# "CompanyMaster.aspx?COMPID="+ Eval("COMPID") %>' runat="server" Text="Edit">                                                                                                                                    
                                                                                                                            </asp:LinkButton>

                                                                                                                            <asp:Label ID="lblcompid" Visible="false" runat="server" Text='<%# Eval("COMPID") %>'></asp:Label>
                                                                                                                            <asp:LinkButton Style="padding-bottom: 0px; padding-top: 0px; height: auto" ID="lnkbtnActive" CommandName="btnActive" CommandArgument='<%# Eval("COMPID") %>' runat="server">                                                                                                                   
                                                                                                                            </asp:LinkButton>

                                                                                                                            <%--<asp:LinkButton Style="padding-bottom: 0px; padding-top: 0px; height: auto" ID="LinkSelectt" CommandName="btnSelect" CommandArgument='<%# Eval("COMPID") %>' runat="server" Text="Select">                                                                                                                   
                                                                                                                            </asp:LinkButton>--%>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                    &nbsp;<asp:CheckBox ID="CheckBox1" runat="server" CssClass="input-inline" />

                                                                                                                    <%--<td>
                                                                                                                        <asp:LinkButton ID="linkbtnview" CommandName="btnview" class="btn btn-sm red filter-cancel" CommandArgument='<%# Eval("COMPID") %>' PostBackUrl='<%# "CompanyMaster.aspx?COMPID="+ Eval("COMPID") %>' runat="server">
                                                                                                                    <i class="fa fa-eye"></i>
                                                                                                                        </asp:LinkButton>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:LinkButton ID="LinkButton1" class="btn btn-sm yellow filter-submit margin-bottom" CommandName="btnEdit" CommandArgument='<%# Eval("COMPID") %>' PostBackUrl='<%# "CompanyMaster.aspx?COMPID="+ Eval("COMPID") %>' runat="server">
                                                                                                                    <i class="fa fa-pencil"></i>
                                                                                                                        </asp:LinkButton>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:LinkButton ID="LinkButton2" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("COMPID") %>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i>                                                                   
                                                                                                                        </asp:LinkButton>
                                                                                                                    </td>--%>
                                                                                                                </tr>
                                                                                                            </table>

                                                                                                        </td>
                                                                                                        <td>

                                                                                                            <asp:LinkButton ID="LinkButton3" CommandName="btnview" CommandArgument='<%# Eval("COMPID") %>' PostBackUrl='<%# "CompanyMaster.aspx?COMPID="+ Eval("COMPID") %>' runat="server">
                                                                                                                <asp:Label ID="Label91" runat="server" Text='<%# Eval("COMPID") %>'></asp:Label>
                                                                                                            </asp:LinkButton>
                                                                                                        </td>

                                                                                                        <td>
                                                                                                            <asp:HiddenField ID="hidecontactid" runat="server" Value='<%# Eval("COMPID") %>' />
                                                                                                            <asp:LinkButton ID="LinkButton5" CommandName="btnview" CommandArgument='<%# Eval("COMPID") %>' PostBackUrl='<%# "CompanyMaster.aspx?COMPID="+ Eval("COMPID") %>' runat="server">
                                                                                                                <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("COMPNAME1") %>' meta:resourcekey="lblCustomerNameResource2"></asp:Label>
                                                                                                            </asp:LinkButton>
                                                                                                        </td>
                                                                                                        <%-- <td>
                                                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ADDR1") %>' meta:resourcekey="lblAddressResource2"></asp:Label></td>
                                                                                
                                                                                <td>
                                                                                    <asp:Label ID="lblZIPCODE" runat="server" Text='<%# Eval("ZIPCODE") %>' meta:resourcekey="lblZIPCODEResource2"></asp:Label></td>--%>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("Keyword") %>' meta:resourcekey="lblREMARKSResource1"></asp:Label></td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblEMAIL" runat="server" Text='<%# Eval("EMAIL1") %>' meta:resourcekey="lblEMAILResource2"></asp:Label></td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblMOBPHONE" runat="server" Text='<%# Eval("MOBPHONE") %>' meta:resourcekey="lblMOBPHONEResource1"></asp:Label></td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblSTATE" runat="server" Text='<%# GetListState(Convert.ToInt32(Eval("COUNTRYID")),Convert.ToInt32(Eval("STATE"))) %>' meta:resourcekey="lblSTATEResource2"></asp:Label></td>
                                                                                                        <td>
                                                                                                            <asp:LinkButton ID="LinkButton15" CommandName="btnview" CommandArgument='<%# Eval("COMPID") %>' PostBackUrl='<%# "CompanyMaster.aspx?COMPID="+ Eval("COMPID") %>' runat="server">
                                                                                                                <asp:Label ID="lblCITY" runat="server" Text='<%# getCityName(Convert.ToInt32(Eval("CITY")),Convert.ToInt32(Eval("STATE")),Convert.ToInt32(Eval("COUNTRYID"))) %>' meta:resourcekey="lblCITYResource2"></asp:Label>
                                                                                                            </asp:LinkButton>
                                                                                                        </td>
                                                                                                        <%-- <td>
                                                                                                            <asp:Label ID="lblcompid" Visible="false" runat="server" Text='<%# Eval("COMPID") %>'></asp:Label>
                                                                                                            <asp:LinkButton ID="lnkbtnActive" CssClass="btn btn-sm green filter-submit margin-bottom" CommandName="btnActive" CommandArgument='<%# Eval("COMPID") %>' runat="server">
                                                                                                                   <i class="fa fa-check"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </td>--%>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                            </asp:ListView>

                                                                                        </tbody>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>
                                                                        <div class="row">
                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                <ContentTemplate>
                                                                                    <div class="col-md-5 col-sm-12">
                                                                                        <div class="dataTables_info" id="sample_1_info" role="status" aria-live="polite">
                                                                                            <asp:Label ID="lblShowinfEntry" runat="server"></asp:Label>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-md-7 col-sm-12">
                                                                                        <div class="dataTables_paginate paging_simple_numbers" id="sample_1_paginate">

                                                                                            <ul class="pagination">
                                                                                                <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_fist">

                                                                                                    <asp:LinkButton ID="btnfirst1" OnClick="btnfirst1_Click" runat="server" OnClientClick="Block()"> First</asp:LinkButton>
                                                                                                </li>
                                                                                                <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_Next">

                                                                                                    <asp:LinkButton ID="btnNext1" Style="width: 53px;" OnClick="btnNext1_Click" runat="server" OnClientClick="Block()"> Next</asp:LinkButton>
                                                                                                </li>
                                                                                                <asp:ListView ID="ListView2" runat="server" OnItemCommand="ListView2_ItemCommand" OnItemDataBound="AnswerList_ItemDataBound">
                                                                                                    <ItemTemplate>
                                                                                                        <td>
                                                                                                            <li class="paginate_button " aria-controls="sample_1" tabindex="0">
                                                                                                                <asp:LinkButton ID="LinkPageavigation" runat="server" CommandName="LinkPageavigation" CommandArgument='<%# Eval("ID")%>' OnClientClick="Block()" onchange="Block()"> <%# Eval("ID")%></asp:LinkButton></li>

                                                                                                        </td>
                                                                                                    </ItemTemplate>
                                                                                                </asp:ListView>
                                                                                                <li class="paginate_button next" aria-controls="sample_1" tabindex="0" id="sample_1_Previos">
                                                                                                    <asp:LinkButton ID="btnPrevious1" OnClick="btnPrevious1_Click" Style="width: 58px;" runat="server" OnClientClick="Block()"> Prev</asp:LinkButton>
                                                                                                </li>
                                                                                                <li class="paginate_button next" aria-controls="sample_1" tabindex="0" id="sample_1_last">
                                                                                                    <asp:LinkButton ID="btnLast1" OnClick="btnLast1_Click" runat="server" OnClientClick="Block()"> Last</asp:LinkButton>
                                                                                                </li>
                                                                                            </ul>

                                                                                        </div>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
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
                            </contenttemplate>
                                <triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnfirst1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnNext1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnPrevious1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnLast1" EventName="Click" />
                            </triggers>

                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                <progresstemplate>
                                <div class="overlay">
                                    <div style="z-index: 1000; margin-left: 45%; margin-top: 40%; opacity: 1; -moz-opacity: 1;">
                                        <img src="../assets/admin/layout4/img/loading-spinner-blue.gif" />
                                        &nbsp;<asp:Label ID="Labels3" runat="server" Text="Loading..." Font-Bold="true" Font-Size="Medium"></asp:Label>
                                    </div>
                                </div>
                            </progresstemplate>
                            </asp:UpdateProgress>

                        </div>
                    </div>
                    <div class="portlet box green-turquoise" style="display: none;">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-gift"></i>Import Company Data
                           &nbsp;&nbsp;&nbsp;
                            </div>

                            <div class="caption">
                                <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-circle blue-soft btn-sm" Style="padding-top: 1px; padding-bottom: 2px;" />
                            </div>
                            <div class="tools">
                                <a href="javascript:;" class="collapse" runat="server" id="importMain"></a>
                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                <a href="javascript:;" class="reload"></a>
                                <a href="javascript:;" class="remove"></a>
                            </div>

                            <div class="actions btn-set">

                                <asp:Button ID="Button3" CssClass="btn blue-hoki" runat="server" Text="Upload" OnClick="Button3_Click" />
                                <asp:Button ID="Button2" CssClass="btn blue-hoki" runat="server" Text="Import" OnClick="Button2_Click" />
                                <asp:Button ID="BtnDownload" CssClass="btn blue-hoki" runat="server" Text="Download Excel Format" OnClick="BtnDownload_Click" />
                            </div>
                        </div>
                        <div id="I123" runat="server" class="portlet-body" style="padding-left: 25px; display: block;">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <contenttemplate>

                                <table class="table table-striped table-bordered table-hover" id="sample_1">
                                    <thead class="repHeader">

                                        <tr class="gradeA">
                                            <th></th>
                                            <th>
                                                <asp:CheckBox ID="ckbAll" runat="server" AutoPostBack="true" OnCheckedChanged="ckbAll_CheckedChanged" /></th>
                                            <th>COMPNAME1</th>
                                            <th>COMPNAME2</th>
                                            <th>COMPNAME3</th>
                                            <th>BirthDate</th>

                                            <th>EMAIL</th>
                                            <th>Address</th>
                                            <th>BUSPHONE1</th>
                                            <th>MOBPHONE</th>
                                            <th>City</th>
                                            <th>State</th>
                                            <th>Country</th>
                                            <th>Keyword</th>
                                            <th>Remark</th>
                                            <th>Webpage</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="Listview3" runat="server">
                                            <ItemTemplate>
                                                <tr class="gradeA">
                                                    <td>
                                                        <%--<asp:Label ID="lblSecureflag" runat="server" CssClass='<%# getflag(Convert.ToBoolean(Eval("TenentID")))%>' Text='<%# Eval("Secureflag")%>'></asp:Label></td>--%>
                                                        <td>
                                                            <asp:CheckBox ID="cheList" runat="server" /></td>
                                                        <td>
                                                            <asp:Label ID="Label108" runat="server" Text='<%# Eval("COMPNAME1")%>'></asp:Label></td>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="TenantID" runat="server" Text='<%# Eval("COMPNAME2")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="MYPRODID" runat="server" Text='<%# Eval("COMPNAME3")%>'></asp:Label></td>

                                                    <td>
                                                        <asp:Label ID="BarCode" runat="server" Text='<%# Eval("CivilID")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="AlternateCode1" runat="server" Text='<%# Eval("EMAIL")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="AlternateCode2" runat="server" Text='<%# Eval("ADDR1")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="UOM" runat="server" Text='<%# Eval("BUSPHONE1")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="COLORID" runat="server" Text='<%# Eval("MOBPHONE")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label111" runat="server" Text='<%# getCityName(Convert.ToInt32(Eval("CITY")),Convert.ToInt32(Eval("STATE")),Convert.ToInt32(Eval("COUNTRYID"))) %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label112" runat="server" Text='<%# getStste(Convert.ToInt32(Eval("STATE")),Convert.ToInt32(Eval("COUNTRYID"))) %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label113" runat="server" Text='<%# GetCountryss(Convert.ToInt32(Eval("COUNTRYID")))%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label114" runat="server" Text='<%# Eval("Keyword")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label115" runat="server" Text='<%# Eval("REMARKS")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label116" runat="server" Text='<%# Eval("WEBPAGE")%>'></asp:Label></td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </tbody>
                                </table>


                            </contenttemplate>
                                <triggers>
                                <asp:PostBackTrigger ControlID="Listview1" />
                                <asp:PostBackTrigger ControlID="ckbAll" />

                            </triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="scroll-to-top">
                        <i class="icon-arrow-up"></i>
                    </div>
                </div>
            </div>
        </div>

        <%-- t --%>
        <panel id="pnlBarcodPop" style="padding: 1px; background-color: #fff; border: 2px solid pink; display: none; overflow: auto;" runat="server" cssclass="modalPopup">
              <div class="modal-dialog" style="position:fixed;left:30%;top:20px">
                     <div class="modal-content" style="width:400px;">
                                                                                    <div class="modal-header" style="background-color:#44B6AE;">										
											                                            <h4 class="modal-title"><i class="fa fa-image"></i>&nbsp;Front image</h4>
										                                            </div>
                                                                
                                                                                    <div class="modal-body">
											                                        <div class="scroller" style="height:150px;" data-always-visible="1" data-rail-visible1="1">
                                                                                        <div class="row">
                                                                                             <div class="col-md-12">
                                                                                                 <asp:Image ID="ImageFront" runat="server" class="img-responsive" />
                                                                                            </div>
                                                               
                                                                                </div></div></div>
                                                                 
                                                                                    <div class="modal-footer">                                                                                         
                                                                                         <asp:LinkButton ID="btncanclebarcode" runat="server"  Text="Cancel" class="btn default"/>
                                                                                    </div>
                                                                  
                     </div>
                                                                                </div>
      </panel>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" DynamicServicePath=""
            BackgroundCssClass="modalBackground" CancelControlID="btncanclebarcode" Enabled="True"
            PopupControlID="pnlBarcodPop" TargetControlID="ImgFront">
        </cc1:ModalPopupExtender>
        <%-- t --%>
        <panel id="Panel2" style="padding: 1px; background-color: #fff; border: 2px solid pink; display: none; overflow: auto;" runat="server" cssclass="modalPopup">
              <div class="modal-dialog" style="position:fixed;left:30%;top:20px">
                     <div class="modal-content" style="width:400px;">
                                                                                    <div class="modal-header" style="background-color:#44B6AE;">										
											                                            <h4 class="modal-title"><i class="fa fa-image"></i>&nbsp;Front image</h4>
										                                            </div>
                                                                
                                                                                    <div class="modal-body">
											                                        <div class="scroller" style="height:150px;" data-always-visible="1" data-rail-visible1="1">
                                                                                        <div class="row">
                                                                                             <div class="col-md-12">
                                                                                                  <asp:Image ID="ImageBack" runat="server" class="img-responsive" />
                                                                                            </div>
                                                               
                                                                                </div></div></div>
                                                                 
                                                                                    <div class="modal-footer">                                                                                         
                                                                                         <asp:LinkButton ID="LinkButton7" runat="server"  Text="Cancel" class="btn default"/>
                                                                                    </div>
                                                                  
                     </div>
                                                                                </div>
      </panel>
        <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" DynamicServicePath=""
            BackgroundCssClass="modalBackground" CancelControlID="LinkButton7" Enabled="True"
            PopupControlID="Panel2" TargetControlID="ImgBack">
        </cc1:ModalPopupExtender>
        <%--<div id="long" class="modal fade modal-scroll" style="top: 0%;" tabindex="-1" data-replace="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
            <h4 class="modal-title">Front image</h4>
        </div>
        <div class="modal-body">
           
            <asp:Image ID="ImageFront" runat="server" class="img-responsive" />
        </div>
        <div class="modal-footer">
            <button type="button" data-dismiss="modal" class="btn btn-success">Close</button>
        </div>
    </div>--%>
        <%--<div id="Back" class="modal fade modal-scroll" style="top: 0%;" tabindex="-1" data-replace="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
            <h4 class="modal-title">Back image</h4>
        </div>
        <div class="modal-body">
            
            <asp:Image ID="ImageBack" runat="server" class="img-responsive" />
        </div>
        <div class="modal-footer">
            <button type="button" data-dismiss="modal" class="btn btn-success">Close</button>
        </div>
    </div>--%>
        <div class="modal fade bs-modal-sm" id="small" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog modal-sm" style="margin-bottom: 0px; margin-top: 0px;">
                <div class="modal-content">
                    <div class="portlet box red">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title" style="color: white;"><i class="fa fa-warning"></i>&nbsp;Warning</h4>
                        </div>
                    </div>
                    <div class="modal-body">
                        <p id="lblmsgpop" style="text-align: center; font-family: 'Courier New';"></p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn default" data-dismiss="modal">Close</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
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
        <div id="responsive" class="modal fade" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="portlet box purple">
                        <div class="modal-header">

                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title" style="color: white;">Add New Position</h4>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="scroller" style="height: 300px" data-always-visible="1" data-rail-visible1="1">
                            <div class="row">

                                <div class="col-md-12">
                                    <p>
                                        <strong style="font-size: larger; margin-left: 10px;">Position Name ENG<span class="required">* </span></strong>
                                        <asp:TextBox ID="txtPOSENG" runat="server" CssClass="form-control" name="txtPOSENG"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="PO" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtPOSENG" ErrorMessage="Position Name ENG required"></asp:RequiredFieldValidator>
                                    </p>
                                    <p>
                                        <strong style="font-size: larger; margin-left: 10px;">Position Name ARB<span class="required">* </span></strong>
                                        <asp:TextBox ID="txtPOSARB" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="PO" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtPOSARB" ErrorMessage="Position Name ARB required"></asp:RequiredFieldValidator>
                                    </p>
                                    <p>
                                        <strong style="font-size: larger; margin-left: 10px;">Position Name FRE<span class="required">* </span></strong>
                                        <asp:TextBox ID="txtPOSFRE" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="PO" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtPOSFRE" ErrorMessage="Position Name FRE required"></asp:RequiredFieldValidator>
                                    </p>
                                    <%--<div class="form-group">
                                    <div class="col-md-6">
                                        <h4 style="margin-left: 10px;">Some Input</h4>
                                        <asp:TextBox ID="TextBox7" runat="server" class="col-md-12 form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <h4 style="margin-left: 10px;">Some Input</h4>
                                        <asp:TextBox ID="TextBox8" runat="server" class="col-md-12 form-control"></asp:TextBox>
                                    </div>
                                </div>--%>
                                </div>
                            </div>


                        </div>
                    </div>
                    <div class="modal-footer" style="background-color: lightgray;">
                        <button type="button" data-dismiss="modal" class="btn btn-sm red">Close</button>
                        <asp:Button ID="btnSaveposition" runat="server" class="btn btn-sm green" Text="Save" ValidationGroup="PO" OnClick="btnSaveposition_Click" />
                    </div>
                </div>
            </div>
        </div>

        <div id="Template" class="modal fade modal-scroll" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="portlet box purple">
                        <div class="modal-header">

                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title" style="color: white;"><i class="fa fa-reply"></i>&nbsp;Template</h4>
                        </div>
                    </div>
                    <div class="modal-body">
                        <%-- <div class="scroller" style="height: 300px" data-always-visible="1" data-rail-visible1="1">
                        <div class="row">
                            <div class="col-md-12">--%>

                        <img id="img" style="height: 800px" src="" />
                        <%-- <asp:Label ID="HTMLTEMP1" runat="server" Text="Label"></asp:Label>--%>

                        <%-- </div>
                        </div>
                    </div>--%>
                    </div>
                    <div class="modal-footer" style="background-color: lightgray;">
                        <asp:Button ID="btnSendMail" runat="server" class="btn btn-sm blue-madison" Text="Verify & Continues" OnClick="btnSendMail_Click" />
                        <button type="button" data-dismiss="modal" class="btn btn-sm red">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <div id="AppointPOP" class="modal fade" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="portlet box purple">
                        <div class="modal-header">

                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title" style="color: white;">Add New Appointment</h4>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="scroller" style="height: 380px" data-always-visible="1" data-rail-visible1="1">
                            <div class="row">

                                <div class="col-md-12">
                                    <p>
                                        <strong style="font-size: larger; margin-left: 10px;">Title <span class="required">* </span></strong>
                                        <asp:TextBox ID="txtAPOtitle" runat="server" CssClass="form-control" name="txtPOSENG"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="APO" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtAPOtitle" ErrorMessage="Title required"></asp:RequiredFieldValidator>
                                    </p>
                                    <p>
                                        <strong style="font-size: larger; margin-left: 10px;">Status<span class="required">* </span></strong>
                                        <asp:DropDownList ID="drpAPOStatus" runat="server" CssClass="table-group-action-input form-control input-medium">
                                            <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Not Confirmed" Value="red"></asp:ListItem>
                                            <asp:ListItem Text="Confirmed" Value="green"></asp:ListItem>
                                            <asp:ListItem Text="No Answer" Value="blue"></asp:ListItem>
                                            <asp:ListItem Text="In Waiting" Value="yellow"></asp:ListItem>
                                            <asp:ListItem Text="Visited" Value="purple"></asp:ListItem>
                                            <asp:ListItem Text="Closed" Value="gray"></asp:ListItem>
                                            <asp:ListItem Text="Canceled" Value="indigo"></asp:ListItem>
                                            <asp:ListItem Text="No Status" Value="aqua"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ValidationGroup="APO" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpAPOStatus" ErrorMessage="Status required" InitialValue="0"></asp:RequiredFieldValidator>
                                    </p>
                                    <p>
                                        <strong style="font-size: larger; margin-left: 10px;">Start Date<span class="required">* </span></strong>
                                        <asp:TextBox ID="txtAPOSDT" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtAPOSDT" Format="MM/dd/yyyy" Enabled="True"></cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="APO" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtAPOSDT" ErrorMessage="Start Date required"></asp:RequiredFieldValidator>
                                    </p>
                                    <p>
                                        <strong style="font-size: larger; margin-left: 10px;">End Date<span class="required">* </span></strong>
                                        <asp:TextBox ID="txtAPOEDT" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtAPOEDT" Format="MM/dd/yyyy" Enabled="True"></cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="APO" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtAPOSDT" ErrorMessage="End Date required"></asp:RequiredFieldValidator>
                                    </p>
                                    <div class="modal-footer" style="background-color: lightgray;">
                                        <button type="button" data-dismiss="modal" class="btn btn-sm red">Close</button>
                                        <asp:Button ID="btnAPOSave" runat="server" class="btn btn-sm green" Text="Save" ValidationGroup="APO" OnClick="btnAPOSave_Click" />
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="modal fade draggable-modal" id="draggable" tabindex="-1" role="note" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="portlet box yellow">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title" style="color: white;">Add Note</h4>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">

                            <div class="col-md-12">
                                <p>
                                    <strong style="font-size: larger; margin-left: 10px;">Title<span class="required">* </span></strong>
                                    <asp:TextBox ID="txtMOMTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="MOM" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtMOMTitle" ErrorMessage="Title required"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <strong style="font-size: larger; margin-left: 10px;">Description<span class="required">* </span></strong>
                                    <asp:TextBox ID="txtMOMDESC" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ValidationGroup="MOM" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtMOMDESC" ErrorMessage="Description required"></asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <strong style="font-size: larger; margin-left: 10px;">Date</strong>
                                    <asp:TextBox ID="txtnotedate" runat="server" CssClass="form-control" placeholder="Reminder Date"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtnotedate" Format="MM/dd/yyyy" Enabled="True"></cc1:CalendarExtender>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-sm red" data-dismiss="modal">Close</button>
                        <asp:Button ID="btnaddMEM" class="btn btn-sm green" runat="server" Text="Save" OnClick="btnaddMEM_Click" ValidationGroup="MOM" />
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <script>
            jQuery(document).ready(function () {
                // initiate layout and plugins
                Metronic.init(); // init metronic core components
                Layout.init(); // init current layout
                Demo.init(); // init demo features
                $("#draggable").draggable({
                    handle: ".modal-header"
                });
            });
        </script>

    <%--<script src="../assets/global/plugins/bootstrap-modal/js/bootstrap-modalmanager.js" type="text/javascript"></script>--%>
    <%--<script src="../assets/global/plugins/bootstrap-modal/js/bootstrap-modal.js" type="text/javascript"></script>--%>
    <%--<script src="../assets/admin/pages/scripts/ui-extended-modals.js"></script>--%>
</asp:Content>
