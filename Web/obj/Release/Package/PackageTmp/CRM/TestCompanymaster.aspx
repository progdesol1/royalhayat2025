<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="TestCompanymaster.aspx.cs" Inherits="Web.CRM.TestCompanymaster" %>

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
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>

   


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="b" runat="server">
        <%--<ul class="page-breadcrumb breadcrumb">
            <li>
                <a href="index.aspx">
                    <asp:Label ID="lblCRm" runat="server" Text="CRM" ></asp:Label></a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <a href="#">
                    <asp:Label ID="lblCompCRm" runat="server" Text="Company Master" ></asp:Label>
                </a>
            </li>
        </ul>--%>

        <!-- BEGIN BODY -->
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <div class="portlet-body form">
                            <div class="portlet-body">
                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Company Classification Is Required" ControlToValidate="tags_1" ValidationGroup="Submit"></asp:RequiredFieldValidator><br />
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorWebsite" runat="server" ErrorMessage="Website Is Required" ControlToValidate="txtWebsite" ValidationGroup="Submit" ></asp:RequiredFieldValidator>--%><br />
                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Remark Is Required" ControlToValidate="txtRemark" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                <div class="form-wizard">
                                    <div class="tabbable">
                                        <ul class="nav nav-pills nav-justified steps" style="margin-bottom: 0px; padding-bottom: 0px; padding-top: 0px;">
                                            <li class="active" style="width: 210px" id="licomditel" runat="server">

                                                <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_general1" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">1 </span><span class="desc">
                                                        <asp:Label ID="lblBusinessCo" runat="server" Text="Business Contact"></asp:Label>
                                                    </span></a>
                                            </li>
                                            <li class="" style="width: 210px" id="libisnde" runat="server">
                                                <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_meta" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">2 </span><span class="desc">
                                                        <asp:Label ID="lblBusinessDetai" runat="server" Text="Business Details"></asp:Label></span></a></li>
                                            <li style="width: 210px">
                                                <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab_images" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">3 </span><span class="desc">
                                                        <asp:Label ID="lblWebExistance" runat="server" Text="Web Existance"></asp:Label></span></a>&nbsp;
                                            </li>
                                            <li style="width: 210px">
                                                <a style="color: #5b9bd1; padding: 0px; width: 150px" href="#tab_reviews" class="step" data-toggle="tab">
                                                    <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">4 </span><span class="desc">
                                                        <asp:Label ID="lblWorkingEmploy" runat="server" Text="Working Employees"></asp:Label></span> </a>
                                            </li>
                                            <li style="width: 80px">
                                                <asp:Button ID="Button1" class="btn btn-circle btn-primary" Style="padding-top: 7px; margin-left: 11px; padding-bottom: 7px; font-size: 10px" runat="server" Text="Add New" OnClick="Button1_Click" />
                                                <asp:Button ID="btnSubmit" class="btn btn-circle btn-primary" Style="padding-top: 7px; margin-left: 11px; padding-bottom: 7px; font-size: 10px" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSave_Click" />
                                            </li>

                                            <li style="width: 80px">

                                                <asp:Button ID="btnCancel" class="btn btn-circle btn-default" Style="padding-top: 7px; padding-bottom: 7px; font-size: 11px" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                                <asp:HiddenField ID="TabName" runat="server" />
                                            </li>
                                            <li style="width: 80px">
                                                <asp:LinkButton ID="btnattmentmst" Visible="false" runat="server" class="btn green-haze btn-circle" Style="padding-top: 7px; padding-bottom: 7px; font-size: 11px" OnClick="btnOpportunity_Click">
                                                    Attachment&nbsp;<span class="badge badge-default" style="background-color: #f3565d; color: #fff;">
                                                        <asp:Label ID="lblAttecmentcount" runat="server"></asp:Label>
                                                    </span>
                                                </asp:LinkButton>
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

                                        </ul>

                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general1">
                                                <div class="portlet box blue">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            <i class="fa fa-gift"></i>
                                                            <asp:Label ID="lblBusContactDe" runat="server" Text="Business Company Details"></asp:Label>
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                            <a href="javascript:;" class="reload"></a>
                                                            <a href="javascript:;" class="remove"></a>
                                                        </div>
                                                        <div class="actions btn-set">
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
                                                                        <div class="col-md-6">
                                                                            <div class="col-md-12" style="left: 76px; right: 0px; width: 282px;">
                                                                                <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                                                                                &nbsp;(<asp:CheckBox ID="chkIsCmny" Text="Is a Free Lancer?" runat="server" onClick="forRentClicked(this)" />
                                                                                )
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="lbl13" class="col-md-4 control-label getshow">
                                                                                <asp:Label ID="lblTypt" runat="server" Text="Type:"></asp:Label>
                                                                                <span class="required">* </span>

                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="lblType" runat="server"></asp:Label>
                                                                                <asp:DropDownList ID="drpType" runat="server" class="form-control"></asp:DropDownList>
                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="l11" class=" control-label col-md-2 getshow">
                                                                                <asp:Label ID="Label9" runat="server" Text="Company Name:"></asp:Label>
                                                                                <span class="required">* </span>

                                                                            </label>
                                                                            <div class="col-md-10">
                                                                                <div class="input-group">
                                                                                    <asp:TextBox ID="txtCustomerName" runat="server" name="name" MaxLength="100" placeholder="Company Name" class="form-control" AutoPostBack="true" OnTextChanged="txtCustomerName_TextChanged"></asp:TextBox>
                                                                                    <span class="input-group-btn"></span>
                                                                                    <asp:LinkButton ID="lkbCustomerN1" runat="server" OnClick="btnCustomerN1_Click">
                                                                                 <i class="icon-arrow-right" style="color:black;padding-left: 4px;"></i>
                                                                                    </asp:LinkButton>

                                                                                </div>
                                                                                <asp:Label ID="lblCustomerName" runat="server" ForeColor="Green"></asp:Label>
                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorCustomerName" runat="server" ErrorMessage="Company Name Is Required" ControlToValidate="txtCustomerName" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl1" class="control-label col-md-2 getshow">
                                                                                <asp:Label ID="lblCompanyN2" runat="server" Text=" Company Lang 2:"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-10">
                                                                                <div class="input-group">
                                                                                    <%--<asp:TextBox ID="txtCustomer" placeholder="اسم الشخص" runat="server" AutoCompleteType="Disabled" class="arabic form-control" TextLanguage="Arabic"></asp:TextBox>--%>
                                                                                    <Lang:LangTextBox ID="txtCustomer" MaxLength="100" runat="server" AutoCompleteType="Disabled" CssClass="arabic form-control" placeholder="اسم الشخص" AutoPostBack="true" TextLanguage="Arabic" OnTextChanged="txtCustomer_TextChanged"></Lang:LangTextBox>
                                                                                    <span class="input-group-btn">
                                                                                        <%--<asp:Button ID="btnCompanyN2" class="btn blue " runat="server" Text="Check" OnClick="btnCompanyN2_Click"  Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                               
                                                                                    </span>

                                                                                    <asp:LinkButton ID="lkbCustomerN2" runat="server" OnClick="btnCompanyN2_Click">
                                                                                 <i class="icon-arrow-right" style="color:black;padding-left: 4px;"></i>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                                <asp:Label ID="lblCustomerL1" runat="server" ForeColor="Green"></asp:Label>
                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorCustomer" runat="server" ErrorMessage="Company Name Other Language 1 Is Required" ControlToValidate="txtCustomer" ValidationGroup=""></asp:RequiredFieldValidator>
                                                                            </div>

                                                                        </div>

                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl3" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="lblCompanyN3" runat="server" Text="Company Lang 3:"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-10">
                                                                                <div class="input-group">
                                                                                    <asp:TextBox ID="txtCustomer2" MaxLength="100" placeholder="Company Name  Language 3" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtCustomer2_TextChanged"></asp:TextBox>
                                                                                    <span class="input-group-btn">
                                                                                        <%-- <asp:Button ID="btncompnyN3" class="btn blue " runat="server" Text="Check" OnClick="btncompnyN3_Click"  Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                
                                                                                    </span>
                                                                                    <asp:LinkButton ID="lkbcompnyN3" runat="server" OnClick="btncompnyN3_Click">
                                                                                 <i class="icon-arrow-right" style="color:black;padding-left: 4px;"></i>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                                <asp:Label ID="lblCustomerL2" runat="server" ForeColor="Green"></asp:Label>
                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Company Name Other Language 2 Is Required" ControlToValidate="txtCustomer2" ValidationGroup=""></asp:RequiredFieldValidator>
                                                                            </div>


                                                                        </div>
                                                                    </div>
                                                                    <asp:HiddenField ID="Regulervalue" runat="server" />
                                                                    <%-- <asp:LinkButton ID="btntest4" Visible="false" class="btn blue" runat="server"></asp:LinkButton>--%>
                                                                    <panel id="ReceivedSign1" style="padding: 1px; background-color: #fff; border: 1px solid #000; display: none" runat="server">
        <div class="modal-header">
            <asp:LinkButton ID="LinkButton1" class="close" runat="server" >Cancel</asp:LinkButton>
           <%-- <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>--%>
            <h4 class="modal-title"> <b>  <asp:Label ID="Label13" runat="server" Text ="All Ready Exit"  ></asp:Label> </b></h4>
        </div>
        <div class="modal-body">
            <div class="row">     
                <div class="portlet-body">
                        <div class="tabbable">
                            <table class="table table-striped table-bordered table-hover" >
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
                                        <td><asp:Label ID="labelCopop" runat="server"></asp:Label></td>
                                        <td><asp:Label ID="lblmopop" runat="server"></asp:Label></td>
                                        <td><asp:Label ID="lblEmailpop" runat="server"></asp:Label></td>                                       
                                        <td><asp:Label ID="lblFaxpop" runat="server"></asp:Label></td>
                                        <td><asp:Label ID="lblBuspop" runat="server"></asp:Label></td>
                     <td><asp:Label ID="lblusernamepop" runat="server"></asp:Label></td>

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
           <asp:Button ID="btnYes" runat="server" class="btn green-haze btn-circle" Text="Yes" OnClick="btnYes_Click"/>
           <asp:Button ID="btnNo" runat="server" class="btn red-haze btn-circle" Text="No" OnClick="btnNo_Click"/>
            
          
        </div>

    </panel>
                                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" DynamicServicePath=""
                                                                        BackgroundCssClass="modalBackground" CancelControlID="LinkButton1" Enabled="True"
                                                                        PopupControlID="ReceivedSign1" TargetControlID="Regulervalue">
                                                                    </cc1:ModalPopupExtender>
                                                                    <asp:UpdatePanel ID="updCompny" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <div class="form-group">


                                                                                <div class="col-md-6">

                                                                                    <label runat="server" id="lbl21" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="lblCountry" runat="server" Text="Country:"></asp:Label>
                                                                                        <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="Label6" runat="server"></asp:Label>
                                                                                        <asp:DropDownList ID="drpCountry" runat="server" class="form-control" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged" AutoPostBack="true">
                                                                                        </asp:DropDownList>
                                                                                        <%-- <asp:TextBox ID="TextBox4" runat="server" class="form-control"></asp:TextBox>
                                                                                        --%>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ForeColor="Red" ErrorMessage="Country Is Required" ControlToValidate="drpCountry" InitialValue="0" ValidationGroup="ValiCont"></asp:RequiredFieldValidator>
                                                                                    </div>


                                                                                </div>

                                                                            </div>
                                                                            <div class="form-group">

                                                                                <div class="col-md-6">

                                                                                    <label runat="server" id="lbl6" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="Label12" runat="server" Text="Postal Code :"></asp:Label>

                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="lblPostalCode" runat="server"></asp:Label>
                                                                                        <asp:TextBox ID="txtPostalCode" MaxLength="40" placeholder="Postal Code" runat="server" class="form-control"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtPostalCode" FilterType="Custom, Numbers" runat="server" Enabled="True" />

                                                                                    </div>

                                                                                </div>
                                                                                <div class="col-md-6">

                                                                                    <label runat="server" id="lbl7" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="lblState" runat="server" Text=" State:"></asp:Label>
                                                                                        <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="Label8" runat="server"></asp:Label>
                                                                                        <asp:DropDownList ID="drpSates" runat="server" class="form-control">
                                                                                        </asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorPostalCode" runat="server" ErrorMessage="State Is Required" InitialValue="0" ControlToValidate="drpSates" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                        <%--<asp:TextBox ID="txtstate" placeholder="State" runat="server" class="form-control" ></asp:TextBox>--%>
                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    <div class="form-group">
                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="lbl9" class="col-md-4 control-label getshow">
                                                                                <asp:Label ID="lblZipCode1" runat="server" Text="Zip Code:"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="lblZipCode" runat="server"></asp:Label>
                                                                                <asp:TextBox ID="txtZipCode" MaxLength="10" placeholder="ZipCode" runat="server" class="form-control"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtZipCode" FilterType="Custom, Numbers" runat="server" Enabled="True" />

                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="lbl14" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="lblCity1" runat="server" Text="City:"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="lblCity" runat="server"></asp:Label>
                                                                                <asp:TextBox ID="txtCity" placeholder="City" MaxLength="20" runat="server" class="form-control"></asp:TextBox>

                                                                            </div>

                                                                        </div>


                                                                    </div>

                                                                    <div class="form-group">
                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="lbl87" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="lblAddres" runat="server" Text="Address1:"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                                                                <asp:TextBox ID="txtAddress" placeholder="Address1" MaxLength="500" runat="server" class="form-control"></asp:TextBox>

                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="lbl74" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="lblAddres2" runat="server" Text=" Address2:"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="lblAddress2" runat="server"></asp:Label>
                                                                                <asp:TextBox ID="txtAddress2" placeholder="Address2" MaxLength="500" runat="server" class="form-control"></asp:TextBox>

                                                                            </div>

                                                                        </div>

                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl22" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="lblEmail" runat="server" Text="EMAIL:"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-10">
                                                                                <div class="input-group">
                                                                                    <asp:TextBox ID="tags_2" runat="server" MaxLength="500" name="email" CssClass="form-control tags"></asp:TextBox>
                                                                                    <span class="input-group-btn">
                                                                                        <asp:LinkButton ID="lbkEmail" runat="server" OnClick="btnEmail_Click">
                                                                                 <i class="icon-arrow-right" style="color:black;padding-left: 4px;"></i>
                                                                                        </asp:LinkButton>
                                                                                        <%--<asp:Button ID="btnEmail" class="btn blue" runat="server" Text="Check" OnClick="btnEmail_Click" Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                    </span>

                                                                                </div>
                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator10" runat="server" ErrorMessage="Email Is Required" ControlToValidate="tags_2" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                <asp:Label ID="lblEmail12" runat="server" ForeColor="Green"></asp:Label>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl55" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="lblFax" runat="server" Text="Fax:"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-10">
                                                                                <div class="input-group">
                                                                                    <asp:TextBox ID="tags_3" name="number" MaxLength="500" runat="server" CssClass="form-control tags"></asp:TextBox>
                                                                                    <span class="input-group-btn">
                                                                                        <asp:LinkButton ID="lkbFax" runat="server" ValidationGroup="ValiCont" OnClick="btnFax_Click">
                                                                                 <i class="icon-arrow-right" style="color:black;padding-left: 4px;"></i>
                                                                                        </asp:LinkButton>
                                                                                        <%--<asp:Button ID="btnFax" class="btn blue " runat="server" Text="Check" OnClick="btnFax_Click"  Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                    </span>


                                                                                </div>
                                                                                <%--<asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator9" runat="server" ErrorMessage="Fax Is Required" ControlToValidate="tags_3" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                                                <asp:Label ID="Label21" runat="server" ForeColor="Green"></asp:Label>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl65" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="lblBusPhone" runat="server" Text=" Bus Phone:"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-10">
                                                                                <div class="input-group">
                                                                                    <asp:TextBox ID="tags_4" name="number" MaxLength="500" runat="server" CssClass="form-control tags"></asp:TextBox>
                                                                                    <span class="input-group-btn">
                                                                                        <asp:LinkButton ID="lbkBusPhone" ValidationGroup="ValiCont" runat="server" OnClick="btnBusPhone_Click">
                                                                                 <i class="icon-arrow-right" style="color:black;padding-left: 4px;"></i>
                                                                                        </asp:LinkButton>
                                                                                        <%--<asp:Button ID="btnBusPhone" class="btn blue" runat="server" Text="Check" OnClick="btnBusPhone_Click"  Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                    </span>

                                                                                </div>
                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator8" runat="server" ErrorMessage="BusPhone Is Required" ControlToValidate="tags_4" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                <asp:Label ID="Label22" runat="server" ForeColor="Red"></asp:Label>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl75" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="lblMobileNo1" runat="server" Text=" Mobile No:"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-10">
                                                                                <div class="input-group">
                                                                                    <asp:TextBox ID="txtMobileNo" placeholder="Mobile No" MaxLength="150" runat="server" CssClass="form-control tags"></asp:TextBox>

                                                                                    <span class="input-group-btn"></span>
                                                                                    <asp:LinkButton ID="lkbcheck" ValidationGroup="ValiCont" runat="server" OnClick="btnMobile_Click">
                                                                                    <i class="icon-arrow-right" style="color:black;padding-left: 4px;"></i>
                                                                                    </asp:LinkButton>


                                                                                </div>

                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Mobile No Is Required" ControlToValidate="txtMobileNo" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                                <asp:Label ID="lblMobileNo" runat="server" ForeColor="Green"></asp:Label>

                                                                            </div>

                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl78" class="col-md-2 control-label getshow">
                                                                                <asp:Label ID="lblComClass" runat="server" Text="Company Classification :"></asp:Label>
                                                                                <%-- <span class="required">* </span>--%>
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <div class="input-group">
                                                                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                                                                    <asp:DropDownList ID="drpCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged" class="form-control select2me">
                                                                                    </asp:DropDownList>

                                                                                    <span class="input-group-btn">
                                                                                        <%--<asp:Button ID="btnCompanyCl" class="btn blue " runat="server" Text="ADD" OnClick="btnCompanyCl_Click"  Style="padding-top: 7px; padding-bottom: 7px; margin-left: 257px; border-left-width: 0px; padding-left: 25px;" />--%>
                                                                                    </span>

                                                                                    <asp:LinkButton ID="libtnNewClass" runat="server">   <i class="icon-plus"> New Main</i> </asp:LinkButton>

                                                                                </div>

                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl79" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="lblClassifica" runat="server" Text="Classification:"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-10">

                                                                                <asp:TextBox ID="tags_1" MaxLength="250" name="number" runat="server" CssClass="form-control tags"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Company Classification Is Required" ControlToValidate="tags_1" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <asp:Panel ID="pnlMultiSize" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto">

                                                                        <div class="row">
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
                                                                                                                <asp:Label ID="Label63" runat="server" Text="Main Classification Name:"></asp:Label>
                                                                                                                <span class="required">* </span>
                                                                                                            </label>
                                                                                                            <div class="col-md-8">
                                                                                                                <asp:TextBox ID="txtclassficname" placeholder="Main Classification Name" runat="server" class="form-control"></asp:TextBox>
                                                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator18" runat="server" ErrorMessage="Main Classification Name Is Required" ControlToValidate="txtclassficname" ValidationGroup="classfiction"></asp:RequiredFieldValidator>
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
                                                                        <div class="modal-footer">
                                                                            <asp:LinkButton ID="btnMinClassfication" class="btn green-haze modalBackgroundbtn-circle" ValidationGroup="classfiction" runat="server" OnClick="btnMinClassfication_Click"> Save</asp:LinkButton>
                                                                            <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                            <asp:Button ID="Button9" runat="server" class="btn btn-default" Text="Cancel" />

                                                                        </div>
                                                                    </asp:Panel>
                                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="Button9" Enabled="True" PopupControlID="pnlMultiSize" TargetControlID="libtnNewClass"></cc1:ModalPopupExtender>
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lblrem123" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="lblremark1" runat="server" Text="Remark:"></asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-10">
                                                                                <asp:Label ID="lblRemark" runat="server"></asp:Label>
                                                                                <asp:TextBox ID="txtRemark" TextMode="MultiLine" MaxLength="250" placeholder="Remark" runat="server" class="form-control"></asp:TextBox>

                                                                            </div>

                                                                        </div>

                                                                    </div>

                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl70" class="col-md-2 control-label getshow">
                                                                                <asp:Label ID="Label5" runat="server" Text="Avtar:"></asp:Label>
                                                                                <%-- <span class="required">* </span>--%>
                                                                            </label>
                                                                            <div class="col-md-2">
                                                                                <asp:Image ID="Avatar" Style="width: 50px; height: 50px;" runat="server" ImageUrl="~/Gallery/defolt.png" class="img-responsive" />

                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                <asp:FileUpload ID="avatarUploadd" class="btn btn-circle green-haze btn-sm" runat="server" onchange="previewFile()" />
                                                                            </div>

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
                                                            <asp:Label ID="lblBCDeta" runat="server" Text="Business Company Details "></asp:Label>
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                            <a href="javascript:;" class="reload"></a>
                                                            <a href="javascript:;" class="remove"></a>
                                                        </div>
                                                        <div class="actions btn-set">
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
                                                                                <asp:DropDownList ID="drpbrand" runat="server" class="form-control select2me">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="drpbrand" ErrorMessage="Company Brand Required." InitialValue="0" ValidationGroup="Braed"></asp:RequiredFieldValidator>
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <asp:LinkButton ID="LinkButton6" runat="server" ValidationGroup="Braed" OnClick="btnBared_Click">
                                                                                    <i class="icon-plus " style="color:black;padding-left: 4px;"></i>
                                                                                </asp:LinkButton>
                                                                                <asp:LinkButton ID="LinkButton14" class="btn btn-warning" runat="server">
                                                                                   <i class="fa fa-plus"> New Brand</i>
                                                                                </asp:LinkButton>
                                                                                <%--  <asp:Button ID="btnBared" class="btn blue " runat="server"  Text="ADD" Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                            </div>

                                                                        </div>

                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-12">

                                                                            <label runat="server" id="lbl456" class="col-md-2 control-label getshow ">
                                                                                <asp:Label ID="Label4" runat="server" Text="Brand:"></asp:Label>

                                                                            </label>
                                                                            <div class="col-md-10">

                                                                                <asp:TextBox ID="tags_5" name="number" MaxLength="250" runat="server" CssClass="form-control tags"></asp:TextBox>

                                                                            </div>

                                                                        </div>

                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="lbl147" class="col-md-4 control-label getshow">
                                                                                <asp:Label ID="lblMyproductId" runat="server" Text="My Product Id:"></asp:Label>
                                                                                <%--<span class="required">* </span>--%>
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="lblProductId" runat="server"></asp:Label>
                                                                                <asp:DropDownList ID="drpMyProductId" runat="server" class="form-control input-medium select2me"></asp:DropDownList>

                                                                            </div>

                                                                        </div>
                                                                        <div class="col-md-6">

                                                                            <label runat="server" id="lbl564" class="col-md-4 control-label getshow ">
                                                                                <asp:Label ID="lblMCLID" runat="server" Text=" My Country Location:"></asp:Label>
                                                                                <%--<span class="required">* </span>--%>
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:Label ID="lblCountryloc" runat="server"></asp:Label>
                                                                                <asp:DropDownList ID="drpMyCounLocID" runat="server" class="form-control">
                                                                                </asp:DropDownList>

                                                                            </div>

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
                                                            <asp:Label ID="lblwExistance" runat="server" Text="Web Existance"></asp:Label>
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                            <a href="javascript:;" class="reload"></a>
                                                            <a href="javascript:;" class="remove"></a>
                                                        </div>
                                                        <div class="actions btn-set">
                                                            <a href="#tab_reviews" class="step" data-toggle="tab" aria-expanded="true">
                                                                <asp:Label ID="Label62" runat="server" Text="Next " class="btn red btn-circle"></asp:Label>
                                                            </a>
                                                        </div>

                                                    </div>
                                                    <div class="portlet-body">

                                                        <div class="tabbable">
                                                            <div class="tab-content no-space">
                                                                <div class="form-body">
                                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Panel ID="panelMsg" runat="server" Visible="false">
                                                                                <div class="alert alert-danger alert-dismissable">
                                                                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                                    <asp:Label ID="lblErreorMsg" runat="server"></asp:Label>
                                                                                </div>
                                                                            </asp:Panel>
                                                                            <div class="form-group">
                                                                                <div class="col-md-6">

                                                                                    <label runat="server" id="lbl741" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="lblprimaryl" runat="server" Text="Primary Language:"></asp:Label>
                                                                                        <%--<span class="required">* </span>--%>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="lblPrimaryLanguage" runat="server"></asp:Label>
                                                                                        <asp:DropDownList ID="drpPrimaryLang" runat="server" class="form-control"></asp:DropDownList>

                                                                                    </div>

                                                                                </div>

                                                                                <div class="col-md-6">


                                                                                    <div class="input-group">
                                                                                        <span class="input-group-addon">
                                                                                            <i class="icon-link"></i>
                                                                                        </span>
                                                                                        <asp:TextBox ID="txtWebsite" MaxLength="150" placeholder="Website" name="url" runat="server" class="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                    <%-- <asp:RegularExpressionValidator ID="regUrl" runat="server" ControlToValidate="txtWebsite" ValidationExpression="^((http|https)://)?([\w-]+\.)+[\w]+(/[\w- ./?]*)?$" Text="Enter a valid URL" />  --%>
                                                                                    <%--<asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator16" runat="server" ErrorMessage="Website Is Required" ControlToValidate="txtWebsite" ValidationGroup="UrlVelidetion"></asp:RequiredFieldValidator>--%>

                                                                                    <asp:Label ID="lblWebsite" runat="server" ForeColor="Red"></asp:Label>
                                                                                </div>



                                                                            </div>

                                                                            <div class="form-group">
                                                                                <div class="col-md-6">

                                                                                    <label runat="server" id="lbl72" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="lblcususername" runat="server" Text=" Customer UserName:"></asp:Label>

                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <div class="input-group">
                                                                                            <asp:TextBox ID="txtcUserName" placeholder="Customer UserName" runat="server" MaxLength="10" class="form-control" AutoPostBack="true" OnTextChanged="txtcUserName_TextChanged"></asp:TextBox>
                                                                                            <span class="input-group-btn">
                                                                                                <%-- <asp:Button ID="btnUserName" class="btn purple " runat="server" Text="Check" ValidationGroup="username"   Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                            </span>
                                                                                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="btnUserName_Click">
                                                                                 <i class="icon-arrow-right" style="color:black;padding-left: 4px;"></i>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                        <asp:RegularExpressionValidator Display="Dynamic" ID="rev" runat="server" ControlToValidate="txtcUserName"
                                                                                            ErrorMessage="Spaces are not allowed!" ValidationGroup="username" ForeColor="Red" ValidationExpression="[^\s]+" />
                                                                                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtcUserName" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{6,15}$" runat="server" ValidationGroup="username" ForeColor="Red" ErrorMessage="Minimum 6 and Maximum 15 characters required."></asp:RegularExpressionValidator>

                                                                                        <asp:Label ID="lblcUserName" runat="server" ForeColor="Red"></asp:Label>
                                                                                    </div>

                                                                                </div>

                                                                                <div class="col-md-6">

                                                                                    <label runat="server" id="lbl521" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="lblCustopassword" runat="server" Text=" Customer Password:"></asp:Label>

                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="lblcPassword" runat="server"></asp:Label>
                                                                                        <asp:TextBox ID="txtcPassword" placeholder="Customer Password" runat="server" MaxLength="10" class="form-control"></asp:TextBox>
                                                                                        <%--<asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator4" runat="server" ForeColor="Red" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                                                                                            ErrorMessage="Minimum 8 characters atleast 1 Alphabet, 1 Number and 1 Special Character" ControlToValidate="txtcPassword" ></asp:RegularExpressionValidator>--%>
                                                                                    </div>

                                                                                </div>

                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    <div class="form-group  form-md-checkboxes" style="margin-left: 0px;">
                                                                        <div class="md-checkbox-inline">
                                                                            <label>
                                                                                <asp:CheckBox ID="chbIsMinistry" runat="server" />&nbsp;
                                                                   <asp:Label ID="lblIsMistry" runat="server" Text="Ministry"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                                            <label>
                                                                                <asp:CheckBox ID="chbIssMb" runat="server" />&nbsp;
                                                                    <asp:Label ID="lblissmb" runat="server" Text="SMB"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                                            <label>
                                                                                <asp:CheckBox ID="chbIsCorporate" runat="server" />&nbsp;
                                                                    <asp:Label ID="lbliscorporate" runat="server" Text="Corporate"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                                            <label>
                                                                                <asp:CheckBox ID="chbInHawally" runat="server" />&nbsp;
                                                                    <asp:Label ID="lblInhawally" runat="server" Text="Hawally"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                                            <label>
                                                                                <asp:CheckBox ID="chbSaler" runat="server" />&nbsp;
                                                                    <asp:Label ID="lblsaler" runat="server" Text="Saler"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                                            <label>
                                                                                <asp:CheckBox ID="chbBuyer" runat="server" />&nbsp;
                                                                    <asp:Label ID="lblbuyer" runat="server" Text="Buyer"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                                            <label>
                                                                                <asp:CheckBox ID="chbSaleDeProd" runat="server" />&nbsp;
                                                                    <asp:Label ID="lblsaledeprod" runat="server" Text="Sale OEM Product"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                                            <label>
                                                                                <asp:CheckBox ID="chbEmailSub" runat="server" />&nbsp;
                                                                    <asp:Label ID="lblemailsub" runat="server" Text="Subscribed to Email"></asp:Label></label>

                                                                        </div>
                                                                    </div>
                                                                    <asp:UpdatePanel ID="Sosielmediya" runat="server">
                                                                        <ContentTemplate>
                                                                            <div class="form-group">
                                                                                <div class="col-md-6">

                                                                                    <label runat="server" id="lbl7521" class="col-md-4 control-label getshow">
                                                                                        <asp:Label ID="Label17" runat="server" Text="Social Media:"></asp:Label>
                                                                                        <%-- <span class="required">* </span>--%>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="Label18" runat="server"></asp:Label>
                                                                                        <asp:DropDownList ID="drpSomib" runat="server" class="form-control">
                                                                                        </asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="drpSomib" ErrorMessage="Social Media Required." InitialValue="0" ValidationGroup="sosieyl"></asp:RequiredFieldValidator>
                                                                                    </div>

                                                                                </div>

                                                                                <div class="col-md-6">

                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="Label19" runat="server"></asp:Label>
                                                                                        <div class="input-group">
                                                                                            <asp:TextBox ID="txtSocial" runat="server" class="form-control"></asp:TextBox>

                                                                                            <span class="input-group-btn">
                                                                                                <asp:LinkButton ID="LinkButton7" runat="server" ValidationGroup="sosieyl" OnClick="btnSocial_Click">
                                                                                    <i class="icon-plus " style="color:black;padding-left: 4px;"></i>
                                                                                                </asp:LinkButton>

                                                                                            </span>
                                                                                        </div>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="txtSocial" ErrorMessage="Social Media Id  Required." ValidationGroup="sosieyl"></asp:RequiredFieldValidator>
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
                                                            <asp:Label ID="lblWEmp" runat="server" Text="Working Employees"></asp:Label>
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                            <a href="javascript:;" class="reload"></a>
                                                            <a href="javascript:;" class="remove"></a>
                                                        </div>
                                                        <div class="actions btn-set">
                                                            <asp:Button ID="btnFinish" class="btn purple btn-circle" runat="server" Text="Finish" ValidationGroup="Submit" OnClick="btnSave_Click " />
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body">
                                                        <div class="tabbable">
                                                            <div class="tab-content no-space">
                                                                <div class="form-body">
                                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                        <ContentTemplate>
                                                                            <div class="form-group">
                                                                                <div class="col-md-3">
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

                                                                                    <label runat="server" id="lbl5214" class="col-md-4 control-label getshow ">
                                                                                        <asp:Label ID="Label15" runat="server" Text="Employ Name:"></asp:Label>
                                                                                        <%-- <span class="required">* </span>--%>
                                                                                    </label>
                                                                                    <div class="col-md-8">

                                                                                        <asp:Label ID="Label16" runat="server"></asp:Label>
                                                                                        <div class="input-group">
                                                                                            <asp:DropDownList ID="drpCompnay" runat="server" class="form-control" OnSelectedIndexChanged="drpCompnay_SelectedIndexChanged" AutoPostBack="true">
                                                                                            </asp:DropDownList>
                                                                                            <span class="input-group-btn">
                                                                                                <asp:LinkButton ID="LinkButton13" runat="server" OnClick="LinkButton13_Click" OnClientClick="return confirm('Do you want to Add Employ?')">
                                                                                    <i class="icon-plus ">&nbsp;&nbsp;&nbsp;Employ</i>
                                                                                                </asp:LinkButton>
                                                                                                <%-- <asp:Button ID="btnAddCobus" class="btn blue " runat="server" Text="Add"  Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                            </span>
                                                                                        </div>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="drpCompnay" ErrorMessage="Employ Name Required." InitialValue="0" ValidationGroup="WorEnploy"></asp:RequiredFieldValidator>
                                                                                    </div>

                                                                                </div>

                                                                                <div class="col-md-4">

                                                                                    <label runat="server" id="lbl632" class="col-md-4 control-label getshow">
                                                                                        <asp:Label ID="lblItmanager1" runat="server" Text="Position:"></asp:Label>
                                                                                        <%--<span class="required">* </span>--%>
                                                                                    </label>
                                                                                    <div class="col-md-8">
                                                                                        <asp:Label ID="lblitManager" runat="server"></asp:Label>
                                                                                        <div class="input-group">
                                                                                            <asp:DropDownList ID="drpItManager" runat="server" class="form-control"></asp:DropDownList>
                                                                                            <span class="input-group-btn">
                                                                                                <asp:LinkButton ID="LinkButton8" runat="server" ValidationGroup="WorEnploy" OnClick="btnAddCobus_Click">
                                                                                    <i class="icon-plus " style="color:black;padding-left: 4px;"></i>
                                                                                                </asp:LinkButton>
                                                                                                <%-- <asp:Button ID="btnAddCobus" class="btn blue " runat="server" Text="Add"  Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                                            </span>
                                                                                        </div>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" Display="Dynamic" ForeColor="Red" runat="server" ControlToValidate="drpItManager" ErrorMessage="Position Required." InitialValue="0" ValidationGroup="WorEnploy"></asp:RequiredFieldValidator>
                                                                                    </div>

                                                                                </div>

                                                                            </div>
                                                                            <div class="tabbable">
                                                                                <table class="table table-striped table-bordered table-hover">
                                                                                    <thead>
                                                                                        <tr>

                                                                                            <th>Company Name</th>
                                                                                            <th>Itmanager</th>
                                                                                            <th>EMAIL</th>
                                                                                            <th>Mobile No</th>
                                                                                            <th>State</th>
                                                                                            <th>City</th>
                                                                                            <th>Remark</th>


                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:ListView ID="lstCompniy" runat="server">
                                                                                            <LayoutTemplate>
                                                                                                <tr id="ItemPlaceholder" runat="server">
                                                                                                </tr>
                                                                                            </LayoutTemplate>
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%#getcompniy(Convert .ToInt32 ( Eval("CompID")))%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("JobTitle")%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%# Eval("email2")%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblMOBPHONE" runat="server" Text='<%# Eval("BusPhone1")%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblSTATE" runat="server" Text='<%# getstate(Convert .ToInt32 (  Eval("CompID")))%>'></asp:Label></td>

                                                                                                    <td>
                                                                                                        <asp:Label ID="lblCITY" runat="server" Text='<%# getcity(Convert .ToInt32 (  Eval("CompID")))%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("remarks")%>'></asp:Label></td>


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
                                                                                <LayoutTemplate>
                                                                                    <tr id="ItemPlaceholder" runat="server">
                                                                                    </tr>
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
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
                                                                                </ItemTemplate>
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
            </div>
        </div>


        <div class="row">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <div class="portlet box green">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-cogs"></i>Responsive Flip Scroll Tables
                                </div>
                                <div class="tools">
                                    <a href="javascript:;" class="collapse"></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                    <a href="javascript:;" class="reload"></a>
                                    <a href="javascript:;" class="remove"></a>
                                </div>
                            </div>
                            <div class="actions btn-set">
                                <asp:LinkButton ID="LinkButton11" class="btn btn-circle btn-warning" runat="server" OnClick="LinkButton11_Click">Advance Search</asp:LinkButton>
                            </div>
                        </div>
                        <div class="portlet-body ">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <label runat="server" id="Label106" class="control-label col-md-2 getshow">
                                                <asp:Label runat="server" ID="Label107">Title</asp:Label>
                                            </label>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="DrpTitle" runat="server" class="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Title" ControlToValidate="DrpTitle" ValidationGroup="SaveSearch21" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Button ID="btnSearch" class="btn btn-circle purple" runat="server" Text="Show" ValidationGroup="SaveSearch21" OnClick="btnSearch_Click" OnClientClick="showProgress()" />
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

                                <div class="portlet-body flip-scroll">
                                    <table class="table table-bordered table-striped table-condensed flip-content">
                                        <thead class="flip-content">
                                            <tr>
                                                <th style="width: 10%">
                                                    <asp:Label ID="Label7" runat="server" Text="Action"></asp:Label></th>
                                                <th>
                                                    <asp:Label ID="Label10" runat="server" Text="Customer Name"></asp:Label></th>

                                                <th style="width: 10%">
                                                    <asp:Label ID="Label20" runat="server" Text="City"></asp:Label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="Listview1" runat="server" OnItemCommand="ListCustomerMaster_ItemCommand" DataKey="COMPID" DataKeyNames="COMPID">
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
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </td>

                                                        <td>
                                                            <asp:HiddenField ID="hidecontactid" runat="server" Value='<%# Eval("COMPID") %>' />
                                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("COMPNAME1") %>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblCITY" runat="server" Text='<%# Eval("CITY") %>'></asp:Label></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </tbody>
                                    </table>
                            </asp:Panel>
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
            </div>
        </div>
        <div class="scroll-to-top">
            <i class="icon-arrow-up"></i>
        </div>
    </div>
</asp:Content>
