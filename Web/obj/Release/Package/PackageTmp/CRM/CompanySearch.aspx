<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="CompanySearch.aspx.cs" Inherits="Web.CRM.CompanySearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="LangTextBox" Namespace="ServerControl1" TagPrefix="Lang" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
    --%>
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
    <script>
        $(document).ready(function () {
            $("#ckball").click(function () {
                $(".checkBoxClass").prop('checked', $(this).prop('checked'));
            });

            $(".checkBoxClass").change(function () {
                if (!$(this).prop("checked")) {
                    $("#ckball").prop("checked", false);
                }
            });
        });</script>

    <script type="text/javascript">
        function openModal() {
            $('#small').modal('show');
        }
 </script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="b" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet-body form">
                        <div class="portlet-body">
                            <div class="form-wizard">
                                <div class="portlet box blue">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-gift"></i>
                                            <asp:Label ID="lblComany" runat="server" Text="Company Master" meta:resourcekey="lblComanyResource1"></asp:Label>

                                        </div>
                                        <div class="tools">
                                            <a href="javascript:;" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <asp:LinkButton ID="btnlistreload" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>

                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                        <div class="actions btn-set">
                                            <asp:Button ID="btnfind" class="btn yellow  btn-circle " runat="server" Text="Search" OnClick="btnfind_Click" OnClientClick="showProgress()" />

                                            <asp:Button ID="Button2" class="btn green-haze btn-circle" runat="server" Text="Clear Search" OnClick="Button2_Click" />
                                            <asp:HiddenField ID="TabName" runat="server" />
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="portlet-body form">
                                            <div class="form-wizard">
                                                <div class="tabbable">
                                                    <div class="tab-content no-space">
                                                        <div class="form-body">
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblComanyn1" runat="server" Text="Company Name:" meta:resourcekey="lblComanyn1Resource1"></asp:Label>


                                                                </label>
                                                                <div class="col-md-4">

                                                                    <asp:TextBox ID="txtCustomerName" runat="server" name="name" placeholder="Company Name" class="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblCompanyN2" runat="server" Text=" Company Name Lan#2:" meta:resourcekey="lblCompanyN2Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">

                                                                    <%--<asp:TextBox ID="txtCustomer" placeholder="اسم الشخص" runat="server" AutoCompleteType="Disabled" class="arabic form-control" TextLanguage="Arabic"></asp:TextBox>--%>
                                                                    <Lang:LangTextBox ID="txtCustomer" runat="server" AutoCompleteType="Disabled" CssClass="arabic form-control" placeholder="اسم الشخص" TextLanguage="Arabic"></Lang:LangTextBox>
                                                                </div>

                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblCompanyN3" runat="server" Text="Company Name Lan#3:" meta:resourcekey="lblCompanyN3Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">

                                                                    <asp:TextBox ID="txtCustomer2" placeholder="Company Name  Language 2" runat="server" class="form-control"></asp:TextBox>
                                                                </div>

                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblCountry" runat="server" Text="Country:" meta:resourcekey="lblCountryResource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label6" runat="server" meta:resourcekey="Label6Resource1"></asp:Label>
                                                                    <asp:DropDownList ID="drpCountry12" runat="server" class="form-control select2me" meta:resourcekey="drpCountryResource1" OnSelectedIndexChanged="drpCountry12_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                    <%-- <asp:TextBox ID="TextBox4" runat="server" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Address Is Required" ControlToValidate="txtAddress" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">

                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="Label12" runat="server" Text="Postal Code :" meta:resourcekey="Label12Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblPostalCode" runat="server" meta:resourcekey="lblPostalCodeResource1"></asp:Label>
                                                                    <asp:TextBox ID="txtPostalCode" placeholder="Postal Code" runat="server" class="form-control" meta:resourcekey="txtPostalCodeResource1"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtPostalCode" FilterType="Custom, Numbers" runat="server" Enabled="True" />

                                                                </div>
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblState" runat="server" Text=" State:" meta:resourcekey="lblStateResource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="Label8" runat="server" meta:resourcekey="Label8Resource1"></asp:Label>
                                                                    <asp:DropDownList ID="drpSates" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpSates_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <%--<asp:TextBox ID="txtstaesh" placeholder="State" runat="server" class="form-control" meta:resourcekey="txtPostalCodeResource1"></asp:TextBox>--%>
                                                                </div>

                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblZipCode1" runat="server" Text="Zip Code:" meta:resourcekey="lblZipCode1Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblZipCode" runat="server" meta:resourcekey="lblZipCodeResource1"></asp:Label>
                                                                    <asp:TextBox ID="txtZipCode" placeholder="ZipCode" runat="server" class="form-control" meta:resourcekey="txtZipCodeResource1"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtZipCode" FilterType="Custom, Numbers" runat="server" Enabled="True" />

                                                                </div>
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblCity1" runat="server" Text="City:" meta:resourcekey="lblCity1Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblCity" runat="server" meta:resourcekey="lblCityResource1"></asp:Label>
                                                                    <asp:DropDownList ID="drpcity" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpcity_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <%--<asp:TextBox ID="txtCity" placeholder="City" runat="server" class="form-control" meta:resourcekey="txtCityResource1"></asp:TextBox>--%>
                                                                </div>

                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblAddres" runat="server" Text="Address1:" meta:resourcekey="lblAddresResource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblAddress" runat="server" meta:resourcekey="lblAddressResource1"></asp:Label>
                                                                    <asp:TextBox ID="txtAddress" placeholder="Address1" runat="server" class="form-control" meta:resourcekey="txtAddressResource1"></asp:TextBox>

                                                                </div>
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblAddres2" runat="server" Text=" Address2:" meta:resourcekey="lblAddres2Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblAddress2" runat="server" meta:resourcekey="lblAddress2Resource1"></asp:Label>
                                                                    <asp:TextBox ID="txtAddress2" placeholder="Address2" runat="server" class="form-control" meta:resourcekey="txtAddress2Resource1"></asp:TextBox>

                                                                </div>

                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblEmail" runat="server" Text="EMAIL:" meta:resourcekey="lblEmailResource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-10">

                                                                    <asp:TextBox ID="tags_2" runat="server" name="email" CssClass="form-control tags"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblFax" runat="server" Text="Fax:" meta:resourcekey="lblFaxResource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-10">

                                                                    <asp:TextBox ID="tags_3" name="number" runat="server" CssClass="form-control tags" meta:resourcekey="tags_3Resource1"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblBusPhone" runat="server" Text=" Bus Phone:" meta:resourcekey="lblBusPhoneResource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-10">

                                                                    <asp:TextBox ID="tags_4" name="number" runat="server" CssClass="form-control tags" meta:resourcekey="tags_4Resource1"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblMobileNo1" runat="server" Text=" Mobile No:" meta:resourcekey="lblMobileNo1Resource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">

                                                                    <asp:TextBox ID="txtMobileNo" placeholder="Mobile No" runat="server" class="form-control" meta:resourcekey="txtMobileNoResource1"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtMobileNo" FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                </div>
                                                                <label class="col-md-2 control-label">
                                                                    <asp:Label ID="lblMyproductId" runat="server" Text="My Product Id:" meta:resourcekey="lblMyproductIdResource1"></asp:Label>

                                                                </label>
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblProductId" runat="server" meta:resourcekey="lblProductIdResource1"></asp:Label>
                                                                    <asp:DropDownList ID="drpMyProductId" runat="server" class="form-control input-medium select2me" meta:resourcekey="drpMyProductIdResource1"></asp:DropDownList>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" style="margin-left: 10px; margin-right: 10px;">
                                                    <ContentTemplate>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblprimaryl" runat="server" Text="Primary Language:" meta:resourcekey="lblprimarylResource1"></asp:Label>

                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblPrimaryLanguage" runat="server" meta:resourcekey="lblPrimaryLanguageResource1"></asp:Label>
                                                                <asp:DropDownList ID="drpPrimaryLang" runat="server" class="form-control" meta:resourcekey="drpPrimaryLangResource1"></asp:DropDownList>

                                                            </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblwebsite1" runat="server" Text="Website:" meta:resourcekey="lblwebsite1Resource1"></asp:Label>

                                                            </label>
                                                            <div class="col-md-4">

                                                                <asp:TextBox ID="txtWebsite" placeholder="Website" name="url" runat="server" class="form-control" meta:resourcekey="txtWebsiteResource1"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label2" runat="server" Text="Classification:" meta:resourcekey="lblremark1Resource1">
                                                                </asp:Label>
                                                            </label>
                                                            <div class="col-md-10">
                                                                <asp:TextBox ID="txtclassification" runat="server" TextMode="MultiLine" placeholder="Classification" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblremark1" runat="server" Text="Remark:" meta:resourcekey="lblremark1Resource1"></asp:Label>

                                                            </label>
                                                            <div class="col-md-10">
                                                                <asp:Label ID="lblRemark" runat="server" meta:resourcekey="lblRemarkResource1"></asp:Label>
                                                                <asp:TextBox ID="txtRemark" TextMode="MultiLine" placeholder="Remark" runat="server" class="form-control" meta:resourcekey="txtRemarkResource1"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblcususername" runat="server" Text=" Customer UserName:" meta:resourcekey="lblcususernameResource1"></asp:Label>

                                                            </label>
                                                            <div class="col-md-4">

                                                                <asp:TextBox ID="txtcUserName" placeholder="Customer UserName" runat="server" class="form-control" meta:resourcekey="txtcUserNameResource1" AutoPostBack="true"></asp:TextBox>

                                                                <asp:RegularExpressionValidator Display="Dynamic" ID="rev" runat="server" ControlToValidate="txtcUserName"
                                                                    ErrorMessage="Spaces are not allowed!" ValidationGroup="username" ForeColor="Red" ValidationExpression="[^\s]+" meta:resourcekey="revResource1" />
                                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtcUserName" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{6,15}$" runat="server" ValidationGroup="username" ForeColor="Red" ErrorMessage="Minimum 6 and Maximum 15 characters required." meta:resourcekey="RegularExpressionValidator3Resource1"></asp:RegularExpressionValidator>

                                                                <asp:Label ID="lblcUserName" runat="server" ForeColor="Red" meta:resourcekey="lblcUserNameResource1"></asp:Label>
                                                            </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblCustopassword" runat="server" Text=" Customer Password:" meta:resourcekey="lblCustopasswordResource1"></asp:Label>

                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblcPassword" runat="server" meta:resourcekey="lblcPasswordResource1"></asp:Label>
                                                                <asp:TextBox ID="txtcPassword" placeholder="Customer Password" runat="server" class="form-control" meta:resourcekey="txtcPasswordResource1"></asp:TextBox>
                                                                <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator4" runat="server" ForeColor="Red" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                                                                    ErrorMessage="Minimum 8 characters atleast 1 Alphabet, 1 Number and 1 Special Character" ControlToValidate="txtcPassword" meta:resourcekey="RegularExpressionValidator4Resource1"></asp:RegularExpressionValidator>

                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="form-group  form-md-checkboxes" style="margin-left: 0px;">
                                                    <div class="md-checkbox-inline">
                                                        <label>
                                                            <asp:CheckBox ID="chbIsMinistry" runat="server" meta:resourcekey="chbIsMinistryResource1" />&nbsp;
                                                                   <asp:Label ID="lblIsMistry" runat="server" Text="Ministry" meta:resourcekey="lblIsMistryResource1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                        <label>
                                                            <asp:CheckBox ID="chbIssMb" runat="server" meta:resourcekey="chbIssMbResource1" />&nbsp;
                                                                    <asp:Label ID="lblissmb" runat="server" Text="SMB" meta:resourcekey="lblissmbResource1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                        <label>
                                                            <asp:CheckBox ID="chbIsCorporate" runat="server" meta:resourcekey="chbIsCorporateResource1" />&nbsp;
                                                                    <asp:Label ID="lbliscorporate" runat="server" Text="Corporate" meta:resourcekey="lbliscorporateResource1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                        <label>
                                                            <asp:CheckBox ID="chbInHawally" runat="server" meta:resourcekey="chbInHawallyResource1" />&nbsp;
                                                                    <asp:Label ID="lblInhawally" runat="server" Text="Hawally" meta:resourcekey="lblInhawallyResource1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                        <label>
                                                            <asp:CheckBox ID="chbSaler" runat="server" meta:resourcekey="chbSalerResource1" />&nbsp;
                                                                    <asp:Label ID="lblsaler" runat="server" Text="Saler" meta:resourcekey="lblsalerResource1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                        <label>
                                                            <asp:CheckBox ID="chbBuyer" runat="server" meta:resourcekey="chbBuyerResource1" />&nbsp;
                                                                    <asp:Label ID="lblbuyer" runat="server" Text="Buyer" meta:resourcekey="lblbuyerResource1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                        <label>
                                                            <asp:CheckBox ID="chbSaleDeProd" runat="server" meta:resourcekey="chbSaleDeProdResource1" />&nbsp;
                                                                    <asp:Label ID="lblsaledeprod" runat="server" Text="Sale OEM Product" meta:resourcekey="lblsaledeprodResource1"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                                        <label>
                                                            <asp:CheckBox ID="chbEmailSub" runat="server" meta:resourcekey="chbEmailSubResource1" />&nbsp;
                                                                    <asp:Label ID="lblemailsub" runat="server" Text="Subscribed to Email" meta:resourcekey="lblemailsubResource1"></asp:Label></label>

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
                <asp:Panel ID="Panel2" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="portlet box blue">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>
                                                Company   List
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <asp:LinkButton ID="LinkButton9" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>

                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                            <div class="actions btn-set">
                                                <asp:LinkButton ID="btnselectexit" class="btn yellow  btn-circle " runat="server" OnClick="btnselectexit_Click">Select & Exit </asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" class="btn green-haze btn-circle" runat="server" OnClick="Button2_Click">Clear Search </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="row">
                                                    <div class="col-md-8">
                                                        <div class="form-group">
                                                            <label runat="server" id="Label106" class="control-label col-md-2 getshow">
                                                                <asp:Label runat="server" ID="Label107">Saved Search</asp:Label>
                                                            </label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="DrpTitle" runat="server" class="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Title" ControlToValidate="DrpTitle" ValidationGroup="SaveSearch21" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-4">
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
                                                                                           <asp:ListItem Value="5" Selected="True">5</asp:ListItem>
                                                                                           <asp:ListItem Value="15">15</asp:ListItem>
                                                                                           <asp:ListItem Value="20">20</asp:ListItem>
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
                                                                                            <thead class="repHeader">
                                                                                                <tr>
                                                                                                    <th>#</th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label1" runat="server" Text="Company Name"></asp:Label></th>
                                                                                                    <%-- <th>
                                                                <asp:Label ID="Label14" runat="server" Text="Address"></asp:Label></th>--%>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label15" runat="server" Text="EMAIL"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label16" runat="server" Text="Mobile No"></asp:Label></th>
                                                                                                    <%--  <th>
                                                                <asp:Label ID="Label17" runat="server" Text="State"></asp:Label></th>--%>
                                                                                                    <%-- <th>
                                                                <asp:Label ID="Label18" runat="server" Text="ZipCode"></asp:Label></th>--%>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label19" runat="server" Text="City"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label20" runat="server" Text="Remark"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:CheckBox ID="ckball" OnCheckedChanged="ckball_CheckedChanged" AutoPostBack="true" runat="server" /><asp:Label ID="Label23" runat="server" Text="All"></asp:Label></th>

                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:ListView ID="Listview1" runat="server" OnItemDataBound="Listview1_ItemDataBound">

                                                                                                    <ItemTemplate>
                                                                                                        <tr class="gradeA">
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblcompniid" Visible="false" runat="server" Text='<%# Eval("COMPID") %>'></asp:Label>
                                                                                                                <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("COMPNAME1") %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblAddress" runat="server" Visible="false" Text='<%# Eval("ADDR1") %>'></asp:Label>
                                                                                                                <asp:Label ID="lblEMAIL" runat="server" Text='<%# Eval("EMAIL1") %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblMOBPHONE" runat="server" Text='<%# Eval("MOBPHONE") %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblSTATE" runat="server" Visible="false" Text='<%# Eval("STATE") %>'></asp:Label>
                                                                                                                <asp:Label ID="lblZIPCODE" runat="server" Visible="false" Text='<%# Eval("ZIPCODE") %>'></asp:Label>
                                                                                                                <asp:Label ID="lblCITY" runat="server" Text='<%# Eval("CITY") %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblREMARKS" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:CheckBox ID="cbSelect" runat="server" /></td>

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
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                                <div class="col-md-7 col-sm-12">
                                                                                    <div class="dataTables_paginate paging_simple_numbers" id="sample_1_paginate">

                                                                                        <ul class="pagination">
                                                                                            <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_fist">

                                                                                                <asp:LinkButton ID="btnfirst1" OnClick="btnfirst1_Click1" runat="server"> First</asp:LinkButton>
                                                                                            </li>
                                                                                            <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_Next">

                                                                                                <asp:LinkButton ID="btnNext1" Style="width: 53px;" OnClick="btnNext1_Click1" runat="server"> Next</asp:LinkButton>
                                                                                            </li>
                                                                                            <asp:ListView ID="ListView3" runat="server" OnItemCommand="ListView3_ItemCommand" OnItemDataBound="AnswerList_ItemDataBound">
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

                                    <asp:PostBackTrigger ControlID="btnfind" />
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
                <div class="scroll-to-top">
                    <i class="icon-arrow-up"></i>
                </div>

            </div>

        </div>
    </div>
    
    <div class="modal fade bs-modal-sm" id="small" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">Warning</h4>
                </div>
                <div class="modal-body">
                    Please Complete First Primary Search Above us After Use List Search...
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn default" data-dismiss="modal">Close</button>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
