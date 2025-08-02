<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="ACM_NewUsewSetupScreen.aspx.cs" Inherits="Web.ACM.ACM_NewUsewSetupScreen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EditableDropDownList" Namespace="EditableControls" TagPrefix="editable" %>
<%@ Register Assembly="LangTextBox" Namespace="ServerControl1" TagPrefix="Lang" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function showADDRole() {
            var view = document.getElementById('ContentPlaceHolder1_pnlRoleAdd');
            if (view.style.display == "none") {
                view.style.display = "block";
                view1.value = "Hide";
            }
            else {
                view.style.display = "none";
                view1.value = "Show";
            }
        }
    </script>
    <script type="text/javascript">
        function showProgressUser() {
            var updateProgress = $get("<%= UpdateProgressUser.ClientID %>");
            updateProgress.style.display = "block";

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="b" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <div class="portlet box blue" id="form_wizard_1">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-user"></i>Admin Setup - <span class="step-title">Step 1 of 3 </span>
                                </div>
                                <div class="tools hidden-xs">
                                    <a href="javascript:;" class="collapse"></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                    <a href="javascript:;" class="reload"></a>
                                    <a href="javascript:;" class="remove"></a>
                                </div>

                            </div>

                            <div class="portlet-body form">
                                <div class="portlet-body">
                                    <div class="form-wizard">
                                        <div class="tabbable">
                                            <ul class="nav nav-pills nav-justified steps">
                                                <li class="active" id="tabCSL1" runat="server">
                                                    <%--<asp:LinkButton href="#tab1" PostBackUrl="#tab1" data-toggle="tab" ID="COnformTab1" class="step" runat="server"> 
                                                        <span class="number">1 </span>
                                                        <span class="desc">Company Approval </span>
                                                    </asp:LinkButton>--%>
                                                    <a style="color: #5b9bd1; padding: 0px; width: 150px" href="#tab1" class="step" data-toggle="tab">
                                                        <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">1 </span><span class="desc">
                                                            <asp:Label ID="lblApproval" runat="server" Text="Company Profile"></asp:Label>
                                                        </span>
                                                    </a>
                                                </li>
                                                <li id="tabCSL4" runat="server">
                                                    <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab4" class="step" data-toggle="tab">
                                                        <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">2 </span><span class="desc">
                                                            <asp:Label ID="Label10" runat="server">User Profile/Setup</asp:Label>
                                                        </span>
                                                    </a>
                                                </li>
                                                <li id="tabCSL2" runat="server">
                                                    <%-- <asp:LinkButton href="#tab2" data-toggle="tab" ID="COnformTab2" class="step" runat="server">                                                   
                                                        <span class="number">2 </span>
                                                        <span class="desc">User/Role Setup</span>
                                                    </asp:LinkButton>--%>
                                                    <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab2" class="step" data-toggle="tab">
                                                        <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">3 </span><span class="desc">
                                                            <asp:Label ID="lblUserRole" runat="server" Text="User/Role Setup"></asp:Label>
                                                        </span>
                                                    </a>
                                                </li>
                                                <li id="tabCSL3" runat="server">
                                                    <%--<asp:LinkButton href="#tab3" data-toggle="tab" ID="COnformTab3" class="step" runat="server"> 
                                                        <span class="number">3 </span>
                                                        <span class="desc">Module Setup</span>
                                                    </asp:LinkButton>--%>
                                                    <a style="color: #5b9bd1; padding: 0px; width: 130px" href="#tab3" class="step" data-toggle="tab">
                                                        <span class="number" style="padding: 3px 10px 13px; width: 30px; margin-right: 5px; height: 30px; border-radius: 104%;">3 </span><span class="desc">
                                                            <asp:Label ID="lblModule" runat="server" Text="Module Setup"></asp:Label>
                                                        </span>
                                                    </a>
                                                </li>
                                            </ul>
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab1">

                                                    <div class="form-horizontal form-row-seperated">
                                                        <div class="portlet light" style="padding-left: 5px; padding-right: 5px;">
                                                            <div class="portlet box green">
                                                                <div class="portlet-title">
                                                                    <div class="caption">
                                                                        <i class="fa fa-gift"></i>Company Approval
                                                                    </div>
                                                                    <div class="tools hidden-xs">
                                                                        <a href="javascript:;" class="collapse"></a>
                                                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                        <a href="javascript:;" class="reload"></a>
                                                                        <a href="javascript:;" class="remove"></a>
                                                                    </div>
                                                                    <div class="actions btn-set">

                                                                        <asp:LinkButton ID="buttonUpdate" runat="server" ValidationGroup="submititems" CssClass="btn yellow-crusta btn-sm blue" Style="width: 125px; height: 30px;" OnClick="buttonUpdate_Click"> Update & Continue</asp:LinkButton>
                                                                        <asp:LinkButton ID="btndiscorohcost" runat="server" CssClass="btn default" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;">Cancel</asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="portlet-body form">
                                                                    <div class="portlet-body">
                                                                        <div class="form-wizard">
                                                                            <div class="tabbable">
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">
                                                                                        Company Logo<span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:FileUpload ID="avatarUploadd" class="btn btn-circle green-haze btn-sm" runat="server" onchange="previewFile()" />
                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="Validation" runat="server" ErrorMessage="Only Image Files Are Allowed"
                                                                                            ControlToValidate="avatarUploadd" ValidationGroup="submititems" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.JPG|.gif|.GIF|.png|.PNG|.bmp|.BMP|.JPEG|.jpeg|.JFIF|.jfif|.TIFF|.tiff)$">
                                                                                        </asp:RegularExpressionValidator>
                                                                                        <asp:Label ID="MSG" runat="server" Text="upload LOGO fix Dimensions(155px - 45px)" ForeColor="Red"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <asp:Image ID="Avatar" runat="server" class="img-responsive pic-bordered" ImageUrl="~/Gallery/defolt.png" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">
                                                                                        Assign TenentID <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtAssignTenant" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                                                                        <asp:Table ID="lblUname" runat="server" Visible="false"></asp:Table>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAssignTenant" ErrorMessage=" Tenant Id  Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <label class="control-label col-md-2">
                                                                                        Location<span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:DropDownList ID="drplocation" class="form-control select2me" runat="server"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="drplocation" InitialValue="0" ErrorMessage=" Select Location  Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">
                                                                                        Company <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtcompni1" class="form-control" runat="server"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcompni1" ErrorMessage=" Company Name Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <label class="control-label col-md-2">
                                                                                        Company Arabic <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtcompni2" class="form-control" runat="server"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcompni2" ErrorMessage=" Company Name 2 Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">
                                                                                        Company French <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtcompni3" class="form-control" runat="server"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcompni3" ErrorMessage=" Company Name 3 Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <label class="control-label col-md-2">
                                                                                        Company Type<span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:DropDownList ID="drpcompanttype" class="form-control select2me" runat="server"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server" ControlToValidate="drpcompanttype" ErrorMessage=" Select  Company Type Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">
                                                                                        Country Name<span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:DropDownList ID="drpcuntry" class="form-control select2me" runat="server"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="0" runat="server" ControlToValidate="drpcuntry" ErrorMessage=" Select Country Name Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>

                                                                                    <label class="control-label col-md-2">
                                                                                        State<span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <%--<asp:TextBox ID="txtstate" class="form-control" runat="server"></asp:TextBox>--%>
                                                                                        <asp:DropDownList ID="drpstate" runat="server" class="form-control select2me"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" InitialValue="0" runat="server" ControlToValidate="drpstate" ErrorMessage=" State Name Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">
                                                                                        City <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtcity" class="form-control" runat="server"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtcity" ErrorMessage=" City Name Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>

                                                                                    <label class="control-label col-md-2">
                                                                                        Address 1<span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtAddre1" class="form-control" runat="server"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtAddre1" ErrorMessage="Address 1 Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">
                                                                                        Address 2 <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtaddre2" class="form-control" runat="server"></asp:TextBox>
                                                                                    </div>


                                                                                    <label class="control-label col-md-2">
                                                                                        Postalcode <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtpostcod" class="form-control" runat="server"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtpostcod" ValidChars="0123456789-" FilterType="Custom, numbers" runat="server" />
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtpostcod" ErrorMessage=" Postalcode Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">
                                                                                        Zip Code<span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtzipcod" class="form-control" runat="server"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtzipcod" ValidChars="0123456789-" FilterType="Custom, numbers" runat="server" />
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtzipcod" ErrorMessage="Zip Code Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>


                                                                                    <label class="control-label col-md-2">
                                                                                        Phon No <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtphon" class="form-control" runat="server"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtphon" ValidChars="0123456789-" FilterType="Custom, numbers" runat="server" />
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtphon" ErrorMessage=" Phon No Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">
                                                                                        Fax<span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtfax" class="form-control" runat="server"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtfax" ValidChars="0123456789-" FilterType="Custom, numbers" runat="server" />
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtfax" ErrorMessage=" Fax Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>


                                                                                    <label class="control-label col-md-2">
                                                                                        Arabic <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtarabic" MaxLength="1" class="form-control" runat="server" placeholder="Y/N" Text="Y"></asp:TextBox>
                                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtarabic"  ErrorMessage="Arabic Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>--%>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">
                                                                                        Decimal Currency<span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtdcurreny" class="form-control" runat="server"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtdcurreny" ValidChars="0123456789." FilterType="Custom, numbers" runat="server" />
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtdcurreny" ErrorMessage=" Decimal Currency Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>


                                                                                    <label class="control-label col-md-2">
                                                                                        Report Dfault <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtreportDf" MaxLength="1" class="form-control" runat="server" placeholder="Y/N" Text="Y"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtreportDf" ErrorMessage="  Report Dfault Required." CssClass="Validation" ValidationGroup="submititems" ForeColor="White"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">
                                                                                        Report Directory<span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtreportDire" Text="\\digitale\erp53\604\Report" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                                                                    </div>


                                                                                    <label class="control-label col-md-2">
                                                                                        Data Directory <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtdatadirec" Text="\\digitale\erp53\604\data" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">
                                                                                        Backup Directory<span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtbackdirect" Text="\\digitale\erp53\604\backup" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                                                                    </div>


                                                                                    <label class="control-label col-md-2">
                                                                                        Executable Directory <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtexecutdirecto" Text="\\digitale\erp53\604\executable" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">
                                                                                        Invdatabase Name<span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtinvdatabsn" Text="inv" class="form-control" runat="server"></asp:TextBox>
                                                                                    </div>


                                                                                    <label class="control-label col-md-2">
                                                                                        ActDataBase Name<span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="txtactdata" Text="NewSaas" class="form-control" runat="server"></asp:TextBox>
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
                                                <div class="tab-pane" id="tab4">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <div class="form-horizontal form-row-seperated">
                                                                <div class="portlet light" style="padding-left: 5px; padding-right: 5px;">
                                                                    <div class="portlet box green">
                                                                        <div class="portlet-title">
                                                                            <div class="caption">
                                                                                <i class="fa fa-gift"></i>User Setup
                                                                            </div>
                                                                            <div class="tools hidden-xs">
                                                                                <a href="javascript:;" class="collapse"></a>
                                                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                <a href="javascript:;" class="reload"></a>
                                                                                <a href="javascript:;" class="remove"></a>
                                                                            </div>
                                                                        </div>
                                                                        <div class="portlet-body form">
                                                                            <div class="portlet-body">
                                                                                <div class="form-wizard">
                                                                                    <div class="tabbable">
                                                                                        <div class="row">
                                                                                            <div class="col-md-12">
                                                                                                <div class="portlet box yellow">
                                                                                                    <div class="portlet-title" style="margin-top: 5px;">
                                                                                                        <div class="caption">
                                                                                                            <i class="fa fa-gift"></i>User 
                                                                                                        </div>
                                                                                                        <div class="tools">
                                                                                                            <a href="javascript:;" class="collapse"></a>
                                                                                                            <a href="javascript:;" class="reload"></a>
                                                                                                        </div>
                                                                                                        <div class="actions btn-set">
                                                                                                            <asp:Button ID="btnGenerateUserTemp" runat="server" Text="Generate" CssClass="btn btn-sm green-jungle" Visible="false" OnClick="btnGenerateUserTemp_Click" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="portlet-body form">
                                                                                                        <div class="form-body">
                                                                                                            <div class="form-group">
                                                                                                                <asp:Label ID="Label11" CssClass="control-label col-md-2" runat="server" Text="Active User"></asp:Label>
                                                                                                                <div class="col-md-3">
                                                                                                                    <asp:DropDownList ID="drppuseract" class="form-control select2me" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drppuseract_SelectedIndexChanged"></asp:DropDownList>
                                                                                                                </div>
                                                                                                                <div class="col-md-2">
                                                                                                                    <asp:Button ID="btnedituser" runat="server" Text="Edit user" CssClass="btn btn-sm green-jungle" OnClick="btnedituser_Click" />
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <asp:Panel ID="pnlADT_user" runat="server" Visible="false">
                                                                                                                <div class="form-group">
                                                                                                                    <asp:Label ID="Label14" CssClass="control-label col-md-2" runat="server" Text="From Date"></asp:Label>
                                                                                                                    <div class="col-md-3">
                                                                                                                        <asp:TextBox ID="txtActivefromdate" runat="server" CssClass="form-control" Placeholder="MM/dd/yyyy"></asp:TextBox>
                                                                                                                        <cc1:CalendarExtender ID="TextBoxFromDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtActivefromdate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                                                    </div>
                                                                                                                    <asp:Label ID="Label15" CssClass="control-label col-md-2" runat="server" Text="Till Date"></asp:Label>
                                                                                                                    <div class="col-md-3">
                                                                                                                        <asp:TextBox ID="txtActiveTilldate" runat="server" CssClass="form-control" Placeholder="MM/dd/yyyy"></asp:TextBox>
                                                                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtActiveTilldate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                                                    </div>

                                                                                                                </div>
                                                                                                                <div class="form-group">
                                                                                                                    <asp:Label ID="Label16" CssClass="control-label col-md-2" runat="server" Text="User Active/Deactive"></asp:Label>
                                                                                                                    <div class="col-md-1" style="padding-top: 7px;">
                                                                                                                        <asp:CheckBox ID="chkuserAD" runat="server" Enabled="false" />
                                                                                                                    </div>
                                                                                                                    <div class="col-md-2">
                                                                                                                        <asp:Button ID="btnsubmituser" runat="server" Text="Submit Menu" CssClass="btn purple-soft" OnClick="btnsubmituser_Click" />
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </asp:Panel>
                                                                                                        </div>
                                                                                                        <div class="portlet-body">
                                                                                                            <div class="tabbable">
                                                                                                                <div class="tab-content no-space">
                                                                                                                    <div class="form-body">
                                                                                                                        <div class="form-group">
                                                                                                                            <div class="col-md-12">
                                                                                                                                <div class="row">
                                                                                                                                </div>
                                                                                                                                <div class="portlet-body">
                                                                                                                                    <table width="100%" class="table table-striped table-hover table-bordered">
                                                                                                                                        <thead>
                                                                                                                                            <tr style="background-color: lightskyblue">
                                                                                                                                                <th width="12%">Menu and Sub Menu</th>
                                                                                                                                                <th width="8%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox25" runat="server"></asp:CheckBox>
                                                                                                                                                </th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox26" runat="server"></asp:CheckBox>
                                                                                                                                                </th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox27" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox28" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox29" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox30" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox31" runat="server"></asp:CheckBox>
                                                                                                                                                </th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox32" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox33" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox34" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox35" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="10%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox36" runat="server"></asp:CheckBox></th>
                                                                                                                                            </tr>
                                                                                                                                        </thead>
                                                                                                                                        <asp:ListView ID="UserListNew" runat="server" OnItemDataBound="UserListNew_ItemDataBound">
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <thead>
                                                                                                                                                    <tr style="background-color: lightgoldenrodyellow;">
                                                                                                                                                        <th width="12%"><%# GetMenuname(Convert.ToInt32(Eval("MENU_ID"))) %>
                                                                                                                                                            <asp:Label ID="lbluserSeparateMENU_IDN" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                                                                                                            <asp:Label ID="lblprivilagemenuidN" Visible="false" runat="server" Text='<%# Eval("PRIVILEGE_MENU_ID") %>'></asp:Label>
                                                                                                                                                            <asp:Label ID="lblprivilageidN" Visible="false" runat="server" Text='<%# Eval("PRIVILEGE_ID") %>'></asp:Label>
                                                                                                                                                        </th>
                                                                                                                                                        <th width="8%">Admin </th>
                                                                                                                                                        <th width="7%">Add </th>
                                                                                                                                                        <th width="7%">Edit</th>
                                                                                                                                                        <th width="7%">Delete</th>
                                                                                                                                                        <th width="7%">Print</th>
                                                                                                                                                        <th width="7%">Label</th>
                                                                                                                                                        <th width="7%">SP1</th>
                                                                                                                                                        <th width="7%">SP2</th>
                                                                                                                                                        <th width="7%">SP3</th>
                                                                                                                                                        <th width="7%">SP4</th>
                                                                                                                                                        <th width="7%">assign<br /> Menu</th>
                                                                                                                                                        <th width="10%">Active<br />
                                                                                                                                                            Menu</th>

                                                                                                                                                    </tr>
                                                                                                                                                </thead>
                                                                                                                                                <tbody>
                                                                                                                                                    <asp:ListView ID="userListLeftNew" runat="server">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <tr>

                                                                                                                                                                <td>
                                                                                                                                                                    <asp:Label ID="lbluserListLeftMENU_IDN" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                                                                                                                    <asp:Label ID="lbluserrleleftMENU_NAME1N" runat="server" Text='<%# GetMenuname(Convert.ToInt32(Eval("MENU_ID"))) %>'></asp:Label>
                                                                                                                                                                    <asp:Label ID="lblpriidN" Visible="false" runat="server" Text='<%# Eval("PRIVILEGE_ID") %>'></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chuserAdminN" Checked='<%# Eval("ALL_FLAG").ToString()=="Y"?true:false %>' Enabled="false" runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chuseraddN" Checked='<%# Eval("ADD_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chusereditN" Checked='<%# Eval("MODIFY_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chuserdeleteN" Checked='<%# Eval("DELETE_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chuserprintN" Checked='<%# Eval("VIEW_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chuserLabelN" Checked='<%# Eval("LABEL_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chusersp1N" Checked='<%# Eval("SP1").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chusersp2N" Checked='<%# Eval("SP2").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chusersp3N" Checked='<%# Eval("SP3").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chusersp4N" Checked='<%# Eval("SP4").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chusersp5N" Checked='<%# Eval("SP5").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="CHuserMenuN" Checked='<%# Eval("ActiveMenu").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                            </tr>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:ListView>
                                                                                                                                                </tbody>
                                                                                                                                            </ItemTemplate>
                                                                                                                                        </asp:ListView>

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
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdateProgress ID="UpdateProgressUser" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                                        <ProgressTemplate>
                                                            <div class="overlay">
                                                                <div style="z-index: 1000; margin-left: 45%; margin-top: 30%; opacity: 1; -moz-opacity: 1;">
                                                                    <%--<img src="../assets/admin/layout4/img/loading-spinner-blue.gif" />--%>
                                                                    <img src="../assets/admin/layout4/img/loading.gif" />
                                                                    &nbsp;<asp:Label ID="Labelu3" runat="server" Text="Loading..." Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </div>
                                                <div class="tab-pane" id="tab2">

                                                    <div class="form-horizontal form-row-seperated">
                                                        <div class="portlet light" style="padding-left: 5px; padding-right: 5px;">
                                                            <div class="portlet box green">
                                                                <div class="portlet-title">
                                                                    <div class="caption">
                                                                        <i class="fa fa-gift"></i>User/Role Setup
                                                                    </div>
                                                                    <div class="tools hidden-xs">
                                                                        <a href="javascript:;" class="collapse"></a>
                                                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                        <a href="javascript:;" class="reload"></a>
                                                                        <a href="javascript:;" class="remove"></a>
                                                                    </div>

                                                                </div>
                                                                <div class="portlet-body form">
                                                                    <div class="portlet-body">
                                                                        <div class="form-wizard">
                                                                            <div class="tabbable">
                                                                                <%-- user panel --%>
                                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <div class="row">
                                                                                            <div class="col-md-12">
                                                                                                <div class="portlet box yellow">
                                                                                                    <div class="portlet-title" style="margin-top: 5px;">
                                                                                                        <div class="caption">
                                                                                                            <i class="fa fa-gift"></i>Role 
                                                                                                        </div>
                                                                                                        <div class="tools">
                                                                                                            <a id="pnlRoleCE" runat="server" href="javascript:;" class="collapse"></a>
                                                                                                            <a href="javascript:;" class="reload"></a>
                                                                                                        </div>
                                                                                                        <div class="actions btn-set">
                                                                                                            <asp:Button ID="btnGenerateRoleTemp" runat="server" Text="Generate" CssClass="btn btn-sm green-jungle" Visible="false" OnClick="btnGenerateRoleTemp_Click" />
                                                                                                            <asp:Button ID="btnpnluserrole" runat="server" Text="User Map Setup" CssClass="btn btn-sm red-haze" OnClick="btnpnluserrole_Click" />
                                                                                                            <asp:Button ID="btnpnlrole" runat="server" Text="Role Setup" CssClass="btn btn-sm yellow-saffron" OnClick="btnpnlrole_Click" />
                                                                                                            <asp:LinkButton ID="LinkAddNewRole" runat="server" CssClass="btn btn-sm purple-medium" OnClick="LinkAddNewRole_Click">Add New Role</asp:LinkButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div id="pnlRole" runat="server" class="portlet-body form" style="display: block;">
                                                                                                        <div class="form-body">
                                                                                                            <%--<div class="form-group">
                                                                                                                <label class="control-label col-md-3" style="padding-top: 0px;">How Many User You have:</label>
                                                                                                                <div class="col-md-9">
                                                                                                                    <table>
                                                                                                                        <tr>
                                                                                                                            <asp:ListView ID="Listuser" runat="server">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <td>

                                                                                                                                        <asp:Label ID="lblUID" Visible="false" runat="server" Text='<%# Eval("USER_ID") %>'></asp:Label>
                                                                                                                                        <asp:CheckBox ID="CHKmodule" Checked="true" runat="server" Enabled="false" />
                                                                                                                                        <asp:Label ID="lblusername" runat="server" Text='<%# Eval("LOGIN_ID") %>'></asp:Label>
                                                                                                                                    </td>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:ListView>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">

                                                                                                                <label class="control-label col-md-3">
                                                                                                                    Module you have
                                                                                                                </label>
                                                                                                                <div class="col-md-3">
                                                                                                                    <asp:DropDownList ID="drpUsermodule" class="form-control select2me" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpUsermodule_SelectedIndexChanged"></asp:DropDownList>
                                                                                                                </div>
                                                                                                            </div>--%>
                                                                                                            <div class="form-group">
                                                                                                                <asp:Label ID="Label1" CssClass="control-label col-md-2" runat="server" Text="Role"></asp:Label>
                                                                                                                <div class="col-md-3">
                                                                                                                    <asp:DropDownList ID="drprole" class="form-control select2me" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drprole_SelectedIndexChanged"></asp:DropDownList>
                                                                                                                    <asp:RequiredFieldValidator ID="RequireddrpROLE" runat="server" ControlToValidate="drprole" ErrorMessage="Role Required." CssClass="Validation" ValidationGroup="proccess" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                                                </div>
                                                                                                                <asp:Label ID="Label2" CssClass="control-label col-md-2" runat="server" Text="Module"></asp:Label>
                                                                                                                <div class="col-md-5">
                                                                                                                    <asp:CheckBoxList ID="Checkmodule" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>

                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <asp:Label ID="Label7" CssClass="control-label col-md-2" runat="server" Text="Module"></asp:Label>
                                                                                                                <div class="col-md-3">
                                                                                                                    <asp:DropDownList ID="drpaddmodule" class="form-control select2me" runat="server"></asp:DropDownList>
                                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="drpaddmodule" ErrorMessage="Module Required." CssClass="Validation" ValidationGroup="add" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                                                </div>
                                                                                                                <div class="col-md-3">
                                                                                                                    <asp:Button ID="btnAddmodule" CssClass="btn blue" runat="server" Text="Add Module" OnClick="btnAddmodule_Click" ValidationGroup="add" />
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <asp:Label ID="Label8" CssClass="control-label col-md-2" runat="server" Text="Module"></asp:Label>
                                                                                                                <div class="col-md-3">
                                                                                                                    <asp:DropDownList ID="drpdeletemodule" class="form-control select2me" runat="server"></asp:DropDownList>
                                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="drpdeletemodule" ErrorMessage="Module Required." CssClass="Validation" ValidationGroup="Delete" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                                                </div>
                                                                                                                <div class="col-md-3">
                                                                                                                    <asp:Button ID="btndeletemodule" CssClass="btn blue" runat="server" Text="Delete Module" OnClick="btndeletemodule_Click" ValidationGroup="Delete" />
                                                                                                                </div>
                                                                                                            </div>

                                                                                                            <div class="col-md-12">
                                                                                                                <asp:Button ID="btnproccess" CssClass="btn blue" runat="server" Text="Process" ValidationGroup="proccess" OnClick="btnproccess_Click" Visible="false" />
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="portlet-body">
                                                                                                            <div class="tabbable">
                                                                                                                <div class="tab-content no-space">
                                                                                                                    <div class="form-body">
                                                                                                                        <div class="form-group">
                                                                                                                            <div class="col-md-12">
                                                                                                                                <div class="row">
                                                                                                                                </div>
                                                                                                                                <div class="portlet-body">
                                                                                                                                    <table width="100%" class="table table-striped table-hover table-bordered">
                                                                                                                                        <thead>
                                                                                                                                            <tr style="background-color: lightskyblue">
                                                                                                                                                <th width="12%">Menu and Sub Menu</th>
                                                                                                                                                <th width="8%">
                                                                                                                                                    <asp:CheckBox ID="chuserallAdmin" runat="server"></asp:CheckBox>
                                                                                                                                                </th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="chuserallAdd" runat="server"></asp:CheckBox>
                                                                                                                                                </th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="chuserallEdit" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="chuserallDelete" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="chuserallPrint" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="chuserallLabel" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox11" runat="server"></asp:CheckBox>
                                                                                                                                                </th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox12" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox13" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox14" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox15" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="10%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox16" runat="server"></asp:CheckBox></th>

                                                                                                                                            </tr>
                                                                                                                                        </thead>
                                                                                                                                        <asp:ListView ID="userListSeparate" runat="server" OnItemDataBound="userListSeparate_ItemDataBound">

                                                                                                                                            <ItemTemplate>


                                                                                                                                                <thead>
                                                                                                                                                    <tr style="background-color: lightgoldenrodyellow;">
                                                                                                                                                        <th width="12%"><%# GetMenuname(Convert.ToInt32(Eval("MENU_ID"))) %>
                                                                                                                                                            <asp:Label ID="lbluserSeparateMENU_ID" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                                                                                                            <asp:Label ID="lblprivilagemenuid" Visible="false" runat="server" Text='<%# Eval("PRIVILEGE_MENU_ID") %>'></asp:Label>
                                                                                                                                                            <asp:Label ID="lblprivilageid" Visible="false" runat="server" Text='<%# Eval("PRIVILEGE_ID") %>'></asp:Label>
                                                                                                                                                        </th>
                                                                                                                                                        <th width="8%">Admin </th>
                                                                                                                                                        <th width="7%">Add </th>
                                                                                                                                                        <th width="7%">Edit</th>
                                                                                                                                                        <th width="7%">Delete</th>
                                                                                                                                                        <th width="7%">Print</th>
                                                                                                                                                        <th width="7%">Label</th>
                                                                                                                                                        <th width="7%">SP1</th>
                                                                                                                                                        <th width="7%">SP2</th>
                                                                                                                                                        <th width="7%">SP3</th>
                                                                                                                                                        <th width="7%">SP4</th>
                                                                                                                                                        <th width="7%">SP5</th>
                                                                                                                                                        <th width="10%">Active<br />
                                                                                                                                                            Menu</th>

                                                                                                                                                    </tr>
                                                                                                                                                </thead>
                                                                                                                                                <tbody>
                                                                                                                                                    <asp:ListView ID="userListLeft" runat="server">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <tr>

                                                                                                                                                                <td>
                                                                                                                                                                    <asp:Label ID="lbluserListLeftMENU_ID" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                                                                                                                    <asp:Label ID="lbluserrleleftMENU_NAME1" runat="server" Text='<%# GetMenuname(Convert.ToInt32(Eval("MENU_ID"))) %>'></asp:Label>
                                                                                                                                                                    <asp:Label ID="lblpriid" Visible="false" runat="server" Text='<%# Eval("PRIVILEGE_ID") %>'></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chuserAdmin" Checked='<%# Eval("ALL_FLAG").ToString()=="Y"?true:false %>' Enabled="false" runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chuseradd" Checked='<%# Eval("ADD_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chuseredit" Checked='<%# Eval("MODIFY_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chuserdelete" Checked='<%# Eval("DELETE_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chuserprint" Checked='<%# Eval("VIEW_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chuserLabel" Checked='<%# Eval("LABEL_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chusersp1" Checked='<%# Eval("SP1").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chusersp2" Checked='<%# Eval("SP2").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chusersp3" Checked='<%# Eval("SP3").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chusersp4" Checked='<%# Eval("SP4").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chusersp5" Checked='<%# Eval("SP5").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="CHuserMenu" Checked='<%# Eval("ActiveMenu").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                            </tr>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:ListView>
                                                                                                                                                </tbody>
                                                                                                                                            </ItemTemplate>
                                                                                                                                        </asp:ListView>

                                                                                                                                    </table>
                                                                                                                                    <div class="col-md-12">
                                                                                                                                        <asp:Button ID="btnsave1" CssClass="btn green" runat="server" Text="Save Module" OnClick="btnsave1_Click" Visible="false" />
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
                                                                                        <div class="portlet box red-sunglo">
                                                                                            <div class="portlet-title">
                                                                                                <div class="caption">
                                                                                                    <i class="fa fa-gift"></i>
                                                                                                    <asp:Label ID="lblforUser" runat="server" Text="User Mapping"></asp:Label>
                                                                                                </div>
                                                                                                <div class="tools">
                                                                                                    <a id="pnluserRoleCE" runat="server" href="javascript:;" class="collapse"></a>
                                                                                                    <a href="javascript:;" class="reload"></a>
                                                                                                </div>
                                                                                                <div class="actions btn-set">
                                                                                                    <%-- <asp:Button ID="btnSaveuser" runat="server" Text="Submit Menu User wise" CssClass="btn btn-sm purple" OnClick="btnSaveuser_Click" Visible="false" />
                                                                                                                <asp:Button ID="btnGenerateUserTemp" runat="server" Text="Generate" CssClass="btn btn-sm green-jungle" Visible="false" OnClick="btnGenerateUserTemp_Click" />--%>
                                                                                                </div>

                                                                                            </div>
                                                                                            <div id="pnlUserRole" runat="server" class="portlet-body form" style="display: block;">
                                                                                                <div class="form-body">

                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="Label3" CssClass="control-label col-md-2" runat="server" Text="Role"></asp:Label>
                                                                                                        <div class="col-md-3">
                                                                                                            <asp:DropDownList ID="drprole1" class="form-control select2me" runat="server"></asp:DropDownList>
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="drprole1" ErrorMessage="Role Required." CssClass="Validation" ValidationGroup="Map" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                                        </div>
                                                                                                        <asp:Label ID="Label6" CssClass="control-label col-md-2" runat="server" Text="user"></asp:Label>
                                                                                                        <div class="col-md-3">
                                                                                                            <asp:DropDownList ID="drpuserr" class="form-control select2me" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpuserr_SelectedIndexChanged"></asp:DropDownList>
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="drpuserr" ErrorMessage="User Required." CssClass="Validation" ValidationGroup="Map" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="Label4" CssClass="control-label col-md-2" runat="server" Text="Module"></asp:Label>
                                                                                                        <div class="col-md-3">
                                                                                                            <asp:DropDownList ID="drpmodule" class="form-control select2me" runat="server"></asp:DropDownList>
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="drpmodule" ErrorMessage="Module Required." CssClass="Validation" ValidationGroup="mapadd" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                                        </div>
                                                                                                        <div class="col-md-3">
                                                                                                            <asp:Button ID="btnadduserrolemodele" runat="server" Text="Add Module" CssClass="btn blue" ValidationGroup="mapadd" OnClick="btnadduserrolemodele_Click" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="form-group">
                                                                                                        <asp:Label ID="Label9" CssClass="control-label col-md-2" runat="server" Text="Module"></asp:Label>
                                                                                                        <div class="col-md-3">
                                                                                                            <asp:DropDownList ID="drpdeleteModuleR" class="form-control select2me" runat="server"></asp:DropDownList>
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="drpdeleteModuleR" ErrorMessage="Module Required." CssClass="Validation" ValidationGroup="mapdelete" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                                        </div>
                                                                                                        <div class="col-md-3">
                                                                                                            <asp:Button ID="btnDeleteuserrolemodule" runat="server" Text="Delete Module" CssClass="btn blue" ValidationGroup="mapdelete" OnClick="btnDeleteuserrolemodule_Click" />
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <div class="col-md-12">
                                                                                                        <div class="form-group">
                                                                                                            <asp:Button ID="btnRefresh" runat="server" Text="REFRESH" CssClass="btn purple-soft" ValidationGroup="Map" OnClick="btnRefresh_Click" />
                                                                                                            <asp:Button ID="Btnsubmit" runat="server" Text="Submit Menu" CssClass="btn purple-soft" ValidationGroup="Map" OnClick="Btnsubmit_Click" />

                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="portlet-body">
                                                                                                    <div class="tabbable">
                                                                                                        <div class="tab-content no-space">
                                                                                                            <div class="form-body">
                                                                                                                <div class="form-group">
                                                                                                                    <div class="col-md-12">
                                                                                                                        <div class="row">
                                                                                                                        </div>
                                                                                                                        <div class="portlet-body">
                                                                                                                            <table width="100%" class="table table-striped table-hover table-bordered">
                                                                                                                                <thead>
                                                                                                                                    <tr style="background-color: lightskyblue;">
                                                                                                                                        <th width="12%">Menu and Sub Menu</th>
                                                                                                                                        <th width="8%">
                                                                                                                                            <asp:CheckBox ID="CheckBox6" runat="server"></asp:CheckBox>
                                                                                                                                        </th>
                                                                                                                                        <th width="7%">
                                                                                                                                            <asp:CheckBox ID="CheckBox7" runat="server"></asp:CheckBox>
                                                                                                                                        </th>
                                                                                                                                        <th width="7%">
                                                                                                                                            <asp:CheckBox ID="CheckBox8" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox8_CheckedChanged"></asp:CheckBox></th>
                                                                                                                                        <th width="7%">
                                                                                                                                            <asp:CheckBox ID="CheckBox9" runat="server"></asp:CheckBox></th>
                                                                                                                                        <th width="7%">
                                                                                                                                            <asp:CheckBox ID="CheckBox10" runat="server"></asp:CheckBox></th>
                                                                                                                                        <th width="7%">
                                                                                                                                            <asp:CheckBox ID="CheckBox17" runat="server"></asp:CheckBox></th>
                                                                                                                                        <th width="7%">
                                                                                                                                            <asp:CheckBox ID="CheckBox19" runat="server"></asp:CheckBox>
                                                                                                                                        </th>
                                                                                                                                        <th width="7%">
                                                                                                                                            <asp:CheckBox ID="CheckBox20" runat="server"></asp:CheckBox></th>
                                                                                                                                        <th width="7%">
                                                                                                                                            <asp:CheckBox ID="CheckBox21" runat="server"></asp:CheckBox></th>
                                                                                                                                        <th width="7%">
                                                                                                                                            <asp:CheckBox ID="CheckBox22" runat="server"></asp:CheckBox></th>
                                                                                                                                        <th width="7%">
                                                                                                                                            <asp:CheckBox ID="CheckBox23" runat="server"></asp:CheckBox></th>
                                                                                                                                        <th width="10%">
                                                                                                                                            <asp:CheckBox ID="CheckBox24" runat="server"></asp:CheckBox></th>

                                                                                                                                    </tr>
                                                                                                                                </thead>
                                                                                                                                <asp:ListView ID="userListSeparateavove" runat="server" OnItemDataBound="userListSeparateavove_ItemDataBound">

                                                                                                                                    <ItemTemplate>


                                                                                                                                        <thead>
                                                                                                                                            <tr style="background-color: lightgoldenrodyellow;">
                                                                                                                                                <th width="12%"><%# GetMenuname(Convert.ToInt32(Eval("MENU_ID"))) %>
                                                                                                                                                    <asp:Label ID="lbluserSeparateMENU_ID" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lblprivilagemenuid" Visible="false" runat="server" Text='<%# Eval("PRIVILEGE_MENU_ID") %>'></asp:Label>
                                                                                                                                                    <asp:Label ID="lblprivilageid" Visible="false" runat="server" Text='<%# Eval("PRIVILEGE_ID") %>'></asp:Label>
                                                                                                                                                </th>
                                                                                                                                                <th width="8%">Admin </th>
                                                                                                                                                <th width="7%">Add </th>
                                                                                                                                                <th width="7%">Edit</th>
                                                                                                                                                <th width="7%">Delete</th>
                                                                                                                                                <th width="7%">Print</th>
                                                                                                                                                <th width="7%">Label</th>
                                                                                                                                                <th width="7%">SP1</th>
                                                                                                                                                <th width="7%">SP2</th>
                                                                                                                                                <th width="7%">SP3</th>
                                                                                                                                                <th width="7%">SP4</th>
                                                                                                                                                <th width="7%">SP5</th>
                                                                                                                                                <th width="10%">Active<br />
                                                                                                                                                    Menu</th>

                                                                                                                                            </tr>
                                                                                                                                        </thead>
                                                                                                                                        <tbody>
                                                                                                                                            <asp:ListView ID="userListLeftabove" runat="server">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <tr>

                                                                                                                                                        <td>
                                                                                                                                                            <asp:Label ID="lbluserListLeftMENU_ID" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                                                                                                            <asp:Label ID="lbluserrleleftMENU_NAME1" runat="server" Text='<%# GetMenuname(Convert.ToInt32(Eval("MENU_ID"))) %>'></asp:Label>
                                                                                                                                                            <asp:Label ID="lblpriid" Visible="false" runat="server" Text='<%# Eval("PRIVILEGE_ID") %>'></asp:Label>
                                                                                                                                                        </td>
                                                                                                                                                        <td style="text-align: center">
                                                                                                                                                            <asp:CheckBox ID="chuserAdmin" Checked='<%# Eval("ALL_FLAG").ToString()=="Y"?true:false %>' Enabled="false" runat="server"></asp:CheckBox></td>
                                                                                                                                                        <td style="text-align: center">
                                                                                                                                                            <asp:CheckBox ID="chuseradd" Checked='<%# Eval("ADD_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                        <td style="text-align: center">
                                                                                                                                                            <asp:CheckBox ID="chuseredit" Checked='<%# Eval("MODIFY_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                        <td style="text-align: center">
                                                                                                                                                            <asp:CheckBox ID="chuserdelete" Checked='<%# Eval("DELETE_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                        <td style="text-align: center">
                                                                                                                                                            <asp:CheckBox ID="chuserprint" Checked='<%# Eval("VIEW_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                        <td style="text-align: center">
                                                                                                                                                            <asp:CheckBox ID="chuserLabel" Checked='<%# Eval("LABEL_FLAG").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                        <td style="text-align: center">
                                                                                                                                                            <asp:CheckBox ID="chusersp1" Checked='<%# Eval("SP1").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                        <td style="text-align: center">
                                                                                                                                                            <asp:CheckBox ID="chusersp2" Checked='<%# Eval("SP2").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                        <td style="text-align: center">
                                                                                                                                                            <asp:CheckBox ID="chusersp3" Checked='<%# Eval("SP3").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                        <td style="text-align: center">
                                                                                                                                                            <asp:CheckBox ID="chusersp4" Checked='<%# Eval("SP4").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                        <td style="text-align: center">
                                                                                                                                                            <asp:CheckBox ID="chusersp5" Checked='<%# Eval("SP5").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                        <td style="text-align: center">
                                                                                                                                                            <asp:CheckBox ID="CHuserMenu" Checked='<%# Eval("ActiveMenu").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                    </tr>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:ListView>
                                                                                                                                        </tbody>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:ListView>

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
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                                <%-- userpanel end --%>
                                                                                <%-- role panel --%>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="tab-pane" id="tab3">

                                                    <div class="form-horizontal form-row-seperated">
                                                        <div class="portlet light" style="padding-left: 5px; padding-right: 5px;">
                                                            <div class="portlet box green">
                                                                <div class="portlet-title">
                                                                    <div class="caption">
                                                                        <i class="fa fa-gift"></i>Module Setup
                                                                    </div>
                                                                    <div class="tools hidden-xs">
                                                                        <a href="javascript:;" class="collapse"></a>
                                                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                        <a href="javascript:;" class="reload"></a>
                                                                        <a href="javascript:;" class="remove"></a>
                                                                    </div>
                                                                    <div class="actions btn-set">
                                                                    </div>
                                                                </div>
                                                                <div class="portlet-body form">
                                                                    <div class="portlet-body">
                                                                        <div class="form-wizard">
                                                                            <div class="tabbable">
                                                                                <%-- Module panel --%>
                                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <div class="row">
                                                                                            <div class="col-md-12">
                                                                                                <div class="portlet box yellow">
                                                                                                    <div class="portlet-title" style="margin-top: 5px;">
                                                                                                        <div class="caption">
                                                                                                            <i class="fa fa-gift"></i>Module
                                                                                                        </div>
                                                                                                        <div class="tools">
                                                                                                            <a href="javascript:;" class="collapse"></a>
                                                                                                            <a href="javascript:;" class="reload"></a>
                                                                                                        </div>
                                                                                                        <div class="actions btn-set">
                                                                                                            <asp:Button ID="btnGenerateModuleTemp" runat="server" Text="Generate" CssClass="btn btn-sm green-jungle" Visible="false" OnClick="btnGenerateModuleTemp_Click" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="portlet-body form">
                                                                                                        <div class="form-body">

                                                                                                            <div class="form-group">
                                                                                                                <asp:Label ID="Label12" runat="server" Text="User" CssClass="control-label col-md-3"></asp:Label>
                                                                                                                <div class="col-md-3">
                                                                                                                    <asp:DropDownList ID="drpuserMod" class="form-control select2me" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpuserMod_SelectedIndexChanged"></asp:DropDownList>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <asp:Label ID="Label5" runat="server" Text="Active Module For This User" CssClass="control-label col-md-3"></asp:Label>
                                                                                                                <div class="col-md-3">
                                                                                                                    <asp:DropDownList ID="DrpModuleActive" class="form-control select2me" runat="server"></asp:DropDownList>
                                                                                                                </div>
                                                                                                                <div class="col-md-3">
                                                                                                                    <asp:Button ID="btnDeactiveM" CssClass="btn blue" runat="server" Text="Dactive Module" OnClick="btnDeactiveM_Click" />
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="form-group">
                                                                                                                <asp:Label ID="Label13" runat="server" Text="Deactive Module For This User" CssClass="control-label col-md-3"></asp:Label>
                                                                                                                <div class="col-md-3">
                                                                                                                    <asp:DropDownList ID="drpModuleDeactive" class="form-control select2me" runat="server"></asp:DropDownList>
                                                                                                                </div>
                                                                                                                <div class="col-md-3">
                                                                                                                    <asp:Button ID="btnActiveM" CssClass="btn blue" runat="server" Text="Active Module" OnClick="btnActiveM_Click" />
                                                                                                                </div>
                                                                                                            </div>

                                                                                                        </div>
                                                                                                        <div class="portlet-body">
                                                                                                            <div class="tabbable">
                                                                                                                <div class="tab-content no-space">
                                                                                                                    <div class="form-body">
                                                                                                                        <div class="form-group">
                                                                                                                            <div class="col-md-12">

                                                                                                                                <div class="portlet-body">
                                                                                                                                    <table width="100%" class="table table-striped table-hover table-bordered">
                                                                                                                                        <thead>
                                                                                                                                            <tr style="background-color: lightskyblue;">
                                                                                                                                                <th width="12%">Menu and Sub Menu</th>
                                                                                                                                                <th width="8%">
                                                                                                                                                    <asp:CheckBox ID="chsysallAdmin" runat="server"></asp:CheckBox>
                                                                                                                                                </th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="chsysallAdd" runat="server"></asp:CheckBox>
                                                                                                                                                </th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="chsysallEdit" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="chsysallDelete" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="chsysallPrint" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="chsysallLabel" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox>
                                                                                                                                                </th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox2" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox3" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox4" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="7%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox5" runat="server"></asp:CheckBox></th>
                                                                                                                                                <th width="10%">
                                                                                                                                                    <asp:CheckBox ID="CheckBox18" runat="server"></asp:CheckBox></th>
                                                                                                                                                <%--<th>Delete</th>--%>
                                                                                                                                            </tr>
                                                                                                                                        </thead>
                                                                                                                                        <asp:ListView ID="sysListSeparate" runat="server" OnItemDataBound="sysListSeparate_ItemDataBound">
                                                                                                                                            <ItemTemplate>


                                                                                                                                                <thead>
                                                                                                                                                    <tr style="background-color: lightgoldenrodyellow;">
                                                                                                                                                        <th width="12%"><%# GetMenuname(Convert.ToInt32(Eval("MENU_ID"))) %><asp:Label ID="lblsysSeparateMENU_ID" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                                                                                                            <asp:Label ID="lblprivilagemenuidM" Visible="false" runat="server" Text='<%# Eval("PRIVILEGE_MENU_ID") %>'></asp:Label>
                                                                                                                                                            <asp:Label ID="lblprivilageidM" Visible="false" runat="server" Text='<%# Eval("PRIVILEGE_ID") %>'></asp:Label>
                                                                                                                                                        </th>
                                                                                                                                                        <th width="8%">Admin </th>
                                                                                                                                                        <th width="7%">Add </th>
                                                                                                                                                        <th width="7%">Edit</th>
                                                                                                                                                        <th width="7%">Delete</th>
                                                                                                                                                        <th width="7%">Print</th>
                                                                                                                                                        <th width="7%">Label</th>
                                                                                                                                                        <th width="7%">SP1</th>
                                                                                                                                                        <th width="7%">SP2</th>
                                                                                                                                                        <th width="7%">SP3</th>
                                                                                                                                                        <th width="7%">SP4</th>
                                                                                                                                                        <th width="7%">SP5</th>
                                                                                                                                                        <th width="10%">Active<br />
                                                                                                                                                            Menu</th>
                                                                                                                                                        <%--<th>Delete</th>--%>
                                                                                                                                                    </tr>
                                                                                                                                                </thead>
                                                                                                                                                <tbody>
                                                                                                                                                    <asp:ListView ID="sysListLeft" runat="server">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <tr>
                                                                                                                                                                <td>
                                                                                                                                                                    <asp:Label ID="lblsysListLeftMENU_ID" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                                                                                                                    <asp:Label ID="lblsysrleleftMENU_NAME1" runat="server" Text='<%# GetMenuname(Convert.ToInt32(Eval("MENU_ID"))) %>'></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chsysAdmin" Checked='<%# Eval("ALL_FLAG").ToString()=="Y"  ?true:false %>' Enabled="false" runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chsysadd" Checked='<%# Eval("ADD_FLAG").ToString()=="Y" ?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chsysedit" Checked='<%# Eval("MODIFY_FLAG").ToString()=="Y"  ?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chsysdelete" Checked='<%# Eval("DELETE_FLAG").ToString()=="Y"  ?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chsysprint" Checked='<%# Eval("VIEW_FLAG").ToString()=="Y"   ?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chsysLabel" Checked='<%# Eval("LABEL_FLAG").ToString()=="Y"   ?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chsyssp1" Checked='<%# Eval("SP1").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chsyssp2" Checked='<%# Eval("SP2").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chsyssp3" Checked='<%# Eval("SP3").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chsyssp4" Checked='<%# Eval("SP4").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="chsyssp5" Checked='<%# Eval("SP5").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                                <td style="text-align: center">
                                                                                                                                                                    <asp:CheckBox ID="CHsysMenu" Checked='<%# Eval("ActiveMenu").ToString()=="Y"?true:false %>' runat="server"></asp:CheckBox></td>
                                                                                                                                                            </tr>
                                                                                                                                                        </ItemTemplate>
                                                                                                                                                    </asp:ListView>
                                                                                                                                                </tbody>
                                                                                                                                            </ItemTemplate>
                                                                                                                                        </asp:ListView>

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
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
