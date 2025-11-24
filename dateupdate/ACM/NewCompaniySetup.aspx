<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="NewCompaniySetup.aspx.cs" Inherits="Web.ACM.NewCompaniySetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EditableDropDownList" Namespace="EditableControls" TagPrefix="editable" %>
<%@ Register Assembly="LangTextBox" Namespace="ServerControl1" TagPrefix="Lang" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    $('#<%=lblqtytotl123.ClientID %>').html(total.toFixed(2));
                    $('#<%=lblSubtotal.ClientID %>').html(total.toFixed(2));
                });
            });
        });     </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //Iterate through each Textbox and add keyup event handler 
            $(".clsTxtToCalculate1").each(function () {
                $(this).keyup(function () {
                    //Initialize total to 0        
                    var total = 0;
                    $(".clsTxtToCalculate1").each(function () {

                        // Sum only if the text entered is number and greater than 0    
                        if (!isNaN(this.value) && this.value.length != 0) {
                            total += parseFloat(this.value);
                        }
                    });
                    //Assign the total to label     
                    //.toFixed() method will roundoff the final sum to 2 decimal places 
                    $('#<%=lblFuctio.ClientID %>').html(total.toFixed(2));
                    $('#<%=lblFuvtion.ClientID %>').html(total.toFixed(2));
                });
            });
        });     </script>

    <script type="text/javascript">
        function multiplication() {
            var weight = document.getElementsByName("lblSubtotal").value;
            var rate = document.getElementsByName("lblFuvtion").value;
            //= document.getElementById().value;
            //var rate = document.getElementById(lblFuvtion).value;

            document.getElementsByName(lblGalredTot).value = weight + rate;
        }
    </script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="b" runat="server">
        <%--<ul class="page-breadcrumb breadcrumb">
            <li>
                <a href="index.aspx">Home</a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <a href="#">Company Setup</a>
            </li>
        </ul>--%>
        <!-- END PAGE BREADCRUMB -->
        <!-- END PAGE HEADER-->
        <!-- BEGIN PAGE CONTENT-->

        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <div class="portlet box blue" id="form_wizard_1">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-gift"></i>Company Setup - <span class="step-title">Step 1 of 5 </span>
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

                                                    <asp:LinkButton href="#tab1" PostBackUrl="#tab1" data-toggle="tab" ID="COnformTab1" class="step" runat="server"> 
                                                        <span class="number">1 </span>
                                                        <span class="desc">Company Approval </span>
                                                    </asp:LinkButton>

                                                </li>
                                                <li id="tabCSL2" runat="server">
                                                    <%-- <% 
                                                        if (ViewState["Tabs"] == "2")
                                                        { %>--%>
                                                    <asp:LinkButton href="#tab2" data-toggle="tab" ID="COnformTab2" class="step" runat="server"> 
                                                  
                                                        <span class="number">2 </span>
                                                        <span class="desc">User/Role Setup</span>
                                                    </asp:LinkButton>
                                                    <%-- <%}else{ %>
                                                     <asp:LinkButton  class="step" runat="server"> 
                                                  
                                                        <span class="number">2 </span>
                                                        <span class="desc">Module Setup </span>
                                                    </asp:LinkButton>
                                                     <%} %>--%>
                                                </li>
                                                <li id="tabCSL3" runat="server">
                                                    <%-- <% 
                                                        if (ViewState["Tabs2"] == "3")
                                                        { %>--%>
                                                    <asp:LinkButton href="#tab3" data-toggle="tab" ID="COnformTab3" class="step" runat="server"> 
                                                   
                                                        <span class="number">3 </span>
                                                        <span class="desc">Module Setup</span>
                                                    </asp:LinkButton>
                                                    <%-- <%}else{ %>
                                                     <asp:LinkButton  class="step" runat="server"> 
                                                   
                                                        <span class="number">3 </span>
                                                        <span class="desc"> User/Role Setup </span>
                                                    </asp:LinkButton>
                                                      <%} %>--%>
                                                </li>
                                                <li id="tabCSL4" runat="server">
                                                    <%-- <% 
                                                        if (ViewState["Tabs2"] == "3")
                                                        { %>--%>
                                                    <asp:LinkButton href="#tab4" data-toggle="tab" ID="COnformTab4" class="step" runat="server"> 
                                                   
                                                        <span class="number">4 </span>
                                                        <span class="desc">Mapping Setup</span>
                                                    </asp:LinkButton>
                                                    <%-- <%}else{ %>
                                                     <asp:LinkButton  class="step" runat="server"> 
                                                   
                                                        <span class="number">3 </span>
                                                        <span class="desc"> User/Role Setup </span>
                                                    </asp:LinkButton>
                                                      <%} %>--%>
                                                </li>
                                                <li id="tabCSL5" runat="server">
                                                    <%-- <% 
                                                        if (ViewState["Tabs3"] == "4")
                                                        { %>--%>
                                                    <asp:LinkButton href="#tab5" data-toggle="tab" class="step" ID="COnform" runat="server"> 
                                                    
                                                        <span class="number">5 </span>
                                                        <span class="desc">Function Setup </span>
                                                    </asp:LinkButton>
                                                    <%-- <%}else{ %>
                                                      <asp:LinkButton class="step" ID="LinkButton3" runat="server"> 
                                                    
                                                        <span class="number">4 </span>
                                                        <span class="desc">Function Setup </span>
                                                    </asp:LinkButton>
                                                     <%} %>--%>
                                                </li>
                                            </ul>
                                            <%--<div id="bar" class="progress progress-striped" role="progressbar">
											<div class="progress-bar progress-bar-success">
											</div>
										</div>--%>
                                            <div class="tab-content no-space">

                                                <div class="tab-pane active" id="tab1" runat="server">

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
                                                                        <asp:Button ID="btnNewRegistration" runat="server" CssClass="btn yellow fc-button-prev" Text="New Registration" OnClick="btnNewRegistration_Click" />
                                                                        <asp:Button ID="btnNewComoany" runat="server" class="btn blue button-next" Text="New Company" OnClick="btnNewComoany_Click" />
                                                                        <asp:Button ID="btnExitingCompany" runat="server" class="btn red button-submit" Text="Existing Company" OnClick="btnExitingCompany_Click" />
                                                                        <asp:LinkButton ID="ButtonAdd" runat="server" ValidationGroup="submititems" CssClass="btn btn-sm yellow" Visible="false" Style="width: 60px; padding-top: 4px; padding-bottom: 4px; height: 30px;" OnClick="ButtonAdd_Click"><i class="fa fa-plus"> Save</i></asp:LinkButton>
                                                                        <asp:LinkButton ID="buttonUpdate" runat="server" ValidationGroup="submititems" CssClass="btn yellow-crusta btn-sm blue" Visible="false" Style="width: 125px; height: 30px;" OnClick="buttonUpdate_Click" Text="Update & Continue"> </asp:LinkButton>
                                                                        <asp:LinkButton ID="btndiscorohcost" runat="server" CssClass="btn default" Visible="false" Style="width: 80px; padding-top: 4px; padding-bottom: 4px; height: 30px;" OnClick="btndiscorohcost_Click">Cancel</asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="portlet-body form">
                                                                    <div class="portlet-body">
                                                                        <div class="form-wizard">
                                                                            <div class="tabbable">
                                                                                <%-- <asp:Panel ID="pnlCompnyExit" runat="server">

                                                                                    <div class="form-group">
                                                                                        <label class="control-label col-md-1">
                                                                                        </label>
                                                                                        <div class="col-md-3">
                                                                                            
                                                                                        </div>

                                                                                        <label class="control-label col-md-1">
                                                                                        </label>
                                                                                        <div class="col-md-3">
                                                                                           
                                                                                        </div>
                                                                                        <label class="control-label col-md-1"></label>
                                                                                        <div class="col-md-3">
                                                                                           
                                                                                        </div>
                                                                                    </div>
                                                                                </asp:Panel>--%>
                                                                                <%-- <asp:Panel ID="pnlAssignTenant" runat="server" Style="display: none">--%>
                                                                                <asp:Panel ID="panCompniDrp" runat="server" Style="display: none">
                                                                                    <div class="form-group">
                                                                                        <label class="control-label col-md-2">
                                                                                            Company Name <span class="required">* </span>
                                                                                        </label>
                                                                                        <div class="col-md-4">
                                                                                            <div class="input-group">
                                                                                                <asp:TextBox ID="txtserchProduct" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Search" MaxLength="250">
                                                                                                </asp:TextBox>
                                                                                                <span class="input-group-btn"></span>
                                                                                                <asp:LinkButton ID="lkbCustomerN1" CssClass="btn btn-icon-only yellow" runat="server" Style="margin-top: -15px; padding-left: 0px; margin-left: 5px; margin-bottom: 7px;" OnClick="lkbCustomerN1_Click">
                                                                                 <i class="fa fa-search" ></i>
                                                                                                </asp:LinkButton>


                                                                                            </div>
                                                                                        </div>
                                                                                        <label class="control-label col-md-2">
                                                                                            OR
                                                                                        </label>
                                                                                        <div class="col-md-4">
                                                                                            <div class="input-group">
                                                                                                <asp:TextBox ID="TextBoxSearchTID" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Tenant ID" MaxLength="250">
                                                                                                </asp:TextBox>
                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="TextBoxSearchTID" FilterType="Custom, numbers" runat="server" />
                                                                                                <span class="input-group-btn"></span>
                                                                                                <asp:LinkButton ID="LinkButtonSearch" CssClass="btn btn-icon-only yellow" runat="server" Style="margin-top: -15px; padding-left: 0px; margin-left: 5px; margin-bottom: 7px;" OnClick="LinkButtonSearch_Click">
                                                                                 <i class="fa fa-search" ></i>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>
                                                                                        <br />
                                                                                        <div class="col-md-11">
                                                                                            <asp:DropDownList ID="drpcompniy" class="form-control" OnSelectedIndexChanged="drpcompniy_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                                                                        </div>
                                                                                        <div class="col-md-1">
                                                                                            <asp:Button ID="btnsearchGO" CssClass="btn btn-sm yellow" runat="server" Text="GO" OnClick="btnsearchGO_Click" />
                                                                                        </div>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                                <asp:Panel ID="panFormUp" runat="server" Style="display: none">
                                                                                    <div class="form-group">
                                                                                        <label class="control-label col-md-2">
                                                                                            Assign TenentID <span class="required">* </span>
                                                                                        </label>
                                                                                        <div class="col-md-4">
                                                                                            <asp:TextBox ID="txtAssignTenant" class="form-control" runat="server"></asp:TextBox>
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
                                                                                            <asp:DropDownList ID="drpcuntry" class="form-control select2me" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpcuntry_SelectedIndexChanged"></asp:DropDownList>
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
                                                                                    <div class="form-group">
                                                                                        <label class="control-label col-md-2">
                                                                                            Company Logo<span class="required">* </span>
                                                                                        </label>
                                                                                        <div class="col-md-3">
                                                                                            <asp:FileUpload ID="avatarUploadd" class="btn btn-circle green-haze btn-sm" runat="server" onchange="previewFile()" />
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="Validation" runat="server" ErrorMessage="Only Image Files Are Allowed"
                                                                                                ControlToValidate="avatarUploadd" ValidationGroup="submititems" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.JPG|.gif|.GIF|.png|.PNG|.bmp|.BMP|.JPEG|.jpeg|.JFIF|.jfif|.TIFF|.tiff)$">
                                                                                            </asp:RegularExpressionValidator>
                                                                                            <asp:Label ID="MSG" runat="server" Text="upload LOGO fix Dimensions(155px - 45px)" ForeColor="Red"></asp:Label>
                                                                                        </div>
                                                                                        <div>
                                                                                            <asp:Image ID="Avatar" Style="width: 40px; height: 40px;" runat="server" ImageUrl="~/Gallery/defolt.png" class="img-responsive" />
                                                                                        </div>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                                <asp:Panel ID="pnlRegistration" runat="server" Style="display: none">
                                                                                    <%--<label class="control-label col-md-2">
                                                                                            Registed Company <span class="required">* </span>
                                                                                        </label>
                                                                                        <div class="col-md-4">
                                                                                            <asp:DropDownList ID="drpRegCompany" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                                        </div>
                                                                                        <label class="control-label col-md-2">
                                                                                            Search Existing <span class="required">* </span>
                                                                                        </label>
                                                                                        <div class="col-md-4">
                                                                                            <div class="input-group">
                                                                                                <asp:TextBox ID="txtRegCompany" CssClass="form-control" AutoCompleteType="Disabled" placeholder="Search" MaxLength="250" runat="server"></asp:TextBox>
                                                                                                <span class="input-group-btn"></span>
                                                                                                <asp:LinkButton ID="lblRegSearch" runat="server" CssClass="btn btn-icon-only yellow" Style="margin-top: -20px; padding-left: 0px; margin-left: 5px; margin-bottom: 7px;">
                                                                                                    <i class="fa fa-search" ></i>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                        </div>--%>
                                                                                    <div class="tabbable">
                                                                                        <table class="table table-striped table-bordered table-hover">
                                                                                            <thead>
                                                                                                <tr>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label12" runat="server" Text="#"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label1" runat="server" Text="Company Name"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label2" runat="server" Text="User Name"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label3" runat="server" Text="Package"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label4" runat="server" Text="No. of User"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label5" runat="server" Text="EmailID"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label6" runat="server" Text="Mobile"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label14" runat="server" Text="Reg. Date"></asp:Label></th>

                                                                                                    <th style="width: 60px;">Company</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:ListView ID="ListRegistration" runat="server" OnItemDataBound="ListRegistration_ItemDataBound" OnItemCommand="ListRegistration_ItemCommand">
                                                                                                    <ItemTemplate>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="Label12" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="listlblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
                                                                                                                <asp:Label ID="ListLabelTenentID" runat="server" Visible="false" Text='<%# Eval("TenentID") %>'></asp:Label>
                                                                                                                <asp:Label ID="ListLabelLocation" runat="server" Visible="false" Text='<%# Eval("LocationID") %>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("UserName") %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="Label9" runat="server" Text='<%# Pack(Convert.ToInt32(Eval("Package"))) %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("NumberofUser") %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="listlblEmailID" runat="server" Text='<%# Eval("EmailID") %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="listlblMobileNo" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="Label15" runat="server" Text='<%# Convert.ToDateTime(Eval("datetime")).ToString("dd/MMM/yyyy") %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <table>
                                                                                                                    <tr>
                                                                                                                        <td>


                                                                                                                            <asp:DropDownList ID="ListDrpCompany" Visible="false" runat="server" Style="width: 150px;"></asp:DropDownList>
                                                                                                                            <asp:LinkButton ID="btnNew" CommandName="btnNew" CommandArgument='<%# Eval("MyID") %>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"></asp:LinkButton>

                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </ItemTemplate>
                                                                                                </asp:ListView>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                                <%--  </asp:Panel>--%>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="tab-pane" id="tab3" runat="server">
                                                    <asp:Panel ID="pnlAllModul" runat="server">
                                                        <div class="form-horizontal form-row-seperated">
                                                            <div class="portlet light" style="padding-left: 5px; padding-right: 5px; padding-bottom: 0px;">

                                                                <div class="portlet box green" id="form_wizard_2">
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
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn yellow-crusta button-next" Style="width: 100px; padding-top: 4px; padding-bottom: 4px; height: 30px;" OnClick="LinkButton1_Click">Continue <i class="m-icon-swapright m-icon-white"></i></asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="portlet-body form">
                                                                        <div class="portlet-body">
                                                                            <div class="form-wizard">


                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-2">
                                                                                        Number Of Module <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:DropDownList ID="drpModulName" class="form-control select2me" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpModulName_SelectedIndexChanged"></asp:DropDownList>
                                                                                    </div>
                                                                                    <label class="control-label col-md-2">
                                                                                        Package <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:DropDownList ID="drpPackage" runat="server" CssClass="form-control select2me" AutoPostBack="true" OnSelectedIndexChanged="drpPackage_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                    </div>

                                                                                </div>
                                                                                <asp:Panel ID="PnlSelectFull" runat="server" Visible="false">
                                                                                    <div class="form-group">
                                                                                        <div class="col-md-12">
                                                                                            <asp:CheckBoxList ID="CHKSelectFull" runat="server" RepeatDirection="Horizontal">
                                                                                            </asp:CheckBoxList>
                                                                                        </div>
                                                                                        <div class="col-md-12">
                                                                                            <asp:Button ID="btnSelectFull" CssClass="btn btn-sm blue-madison" runat="server" Text="Proccess" OnClick="btnSelectFull_Click" />
                                                                                        </div>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                                <div class="form-group">
                                                                                    <label class="control-label col-md-3">
                                                                                        Cost <span class="required">* </span>
                                                                                    </label>
                                                                                    <div class="col-md-4">
                                                                                        <asp:TextBox ID="TextBox4" class="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="tabbable">
                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>
                                                                                            <asp:CheckBox ID="ckbAll" runat="server" OnCheckedChanged="ckbAll_CheckedChanged" AutoPostBack="true" /></th>
                                                                                        <th>
                                                                                            <asp:Label runat="server" ID="lblhMODULE_ID" Text="Module  name"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label runat="server" ID="lblhMENU_NAME1" Text="Menu name1"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label runat="server" ID="lblhLINK" Text="Link"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label runat="server" ID="lblhURLREWRITE" Text="Url Rewrite"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label runat="server" ID="lblhMENU_ORDER" Text="Menu order"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label runat="server" ID="lblhAMIGLOBALE" Text="Amiglobale"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label runat="server" ID="lblhACTIVETILLDATE" Text="Active Till Date"></asp:Label></th>



                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <asp:ListView ID="ListView1" runat="server">
                                                                                        <LayoutTemplate>
                                                                                            <tr id="ItemPlaceholder" runat="server">
                                                                                            </tr>
                                                                                        </LayoutTemplate>
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:CheckBox ID="cheList" runat="server" /></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblMODULE_ID" runat="server" Text='<%# Module_Name(Convert.ToInt32(Eval("MODULE_ID")))%>'></asp:Label>
                                                                                                    <asp:Label ID="lblMenu" Visible="false" runat="server" Text='<%# Eval("MENU_ID") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblMaster" Visible="false" runat="server" Text='<%# Eval("MASTER_ID") %>'></asp:Label>
                                                                                                    <asp:Label ID="lblMOD" Visible="false" runat="server" Text='<%# Eval("MODULE_ID") %>'></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblMENU_NAME1" runat="server" Text='<%# Eval("MENU_NAME1")%>'></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblLINK" runat="server" Text='<%# Eval("LINK")%>'></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblURLREWRITE" runat="server" Text='<%# Eval("URLREWRITE")%>'></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblMENU_ORDER" runat="server" Text='<%# Eval("MENU_ORDER")%>'></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblAMIGLOBALE" runat="server" Text='<%# Eval("AMIGLOBALE")%>'></asp:Label></td>
                                                                                                <td>

                                                                                                    <asp:Label ID="lblACTIVETILLDATE" runat="server" Text='<%# Convert.ToDateTime(Eval("ACTIVETILLDATE")).ToShortDateString()%>'></asp:Label>

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
                                                    </asp:Panel>
                                                </div>
                                                <div class="tab-pane" id="tab2" runat="server">
                                                    <div class="form-horizontal form-row-seperated">
                                                        <div class="portlet light" style="padding-left: 5px; padding-right: 5px;">

                                                            <div class="portlet box green" id="form_wizard_3">
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
                                                                    <div class="actions btn-set">
                                                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn yellow-crusta button-next" Style="width: 100px; padding-top: 4px; padding-bottom: 4px; height: 30px;" OnClick="LinkButton2_Click">Continue <i class="m-icon-swapright m-icon-white"></i></asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="portlet-body form">
                                                                    <div class="portlet-body">
                                                                        <div class="form-wizard">
                                                                            <div class="form-group">
                                                                                <label class="control-label col-md-2">
                                                                                    Number Of User <span class="required">* </span>
                                                                                </label>
                                                                                <div class="col-md-4">
                                                                                    <asp:TextBox ID="txtuserNumber" Enabled="false" class="form-control" runat="server"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtuserNumber" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                    <span class="help-block">Provide your Number Of User </span>
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <asp:RadioButton ID="RDBONEMonth" GroupName="anemonth" Checked="true" runat="server" Text="One Month Demo" CssClass="md-radio-list md-radio-inline" AutoPostBack="true" OnCheckedChanged="RDBONEMonth_CheckedChanged" />
                                                                                    <asp:RadioButton ID="RDBMore" GroupName="anemonth" runat="server" Text="More" CssClass="md-radio-list md-radio-inline" AutoPostBack="true" OnCheckedChanged="RDBMore_CheckedChanged" />
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <br />
                                                                                    <asp:Panel ID="PNL11" Visible="false" runat="server">
                                                                                        <label class="control-label col-md-2">
                                                                                            From Date <span class="required">* </span>
                                                                                        </label>
                                                                                        <div class="col-md-3">
                                                                                            <asp:TextBox ID="txtFromDate" class="form-control" placeholder="MM/dd/yyyy" runat="server"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="MM/dd/yyyy" Enabled="True"></cc1:CalendarExtender>
                                                                                        </div>
                                                                                        <label class="control-label col-md-2">
                                                                                            Till Date <span class="required">* </span>
                                                                                        </label>
                                                                                        <div class="col-md-3">
                                                                                            <asp:TextBox ID="txtTillDate" class="form-control" runat="server"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTillDate" Format="MM/dd/yyyy" Enabled="True"></cc1:CalendarExtender>
                                                                                        </div>
                                                                                    </asp:Panel>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <asp:Button ID="btnRoll" runat="server" Text="User" CssClass="btn red-flamingo" OnClick="btnRoll_Click" />
                                                                                </div>
                                                                            </div>
                                                                            <asp:Panel ID="Pnlroll" runat="server">
                                                                                <div class="form-group">
                                                                                    <table class="table table-striped table-hover">
                                                                                        <asp:ListView ID="ListView2" runat="server" OnItemDataBound="ListView2_ItemDataBound">
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <label class="control-label col-md-2">
                                                                                                            User - <%# Container.DataItemIndex + 1 %> <span class="required">* </span>
                                                                                                        </label>
                                                                                                        <div class="col-md-4">
                                                                                                            <asp:TextBox ID="txtuserNumber1" class="form-control" runat="server"></asp:TextBox>
                                                                                                            <%--<span class="help-block">Provide your Number Of User </span>--%>
                                                                                                        </div>
                                                                                                        <label class="control-label col-md-2">
                                                                                                            User Role <span class="required">*</span>
                                                                                                        </label>
                                                                                                        <div class="col-md-4">
                                                                                                            <asp:DropDownList ID="drpRoll" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                                            <%-- <span class="help-block">Select your Number Of Roll </span>--%>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <label class="control-label col-md-12" style="color: red;">
                                                                                                    Your Default Password id 12345 You Want To Change Password Go To Web User MST page 
                                                                                                </label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </asp:Panel>
                                                                            <div class="form-group">
                                                                                <label class="control-label col-md-2">
                                                                                    Cost <span class="required">* </span>
                                                                                </label>
                                                                                <div class="col-md-4">
                                                                                    <asp:TextBox ID="TextBox2" class="form-control" Enabled="false" runat="server"></asp:TextBox>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="tab-pane" id="tab4" runat="server">
                                                    <div class="form-horizontal form-row-seperated">
                                                        <div class="portlet light" style="padding-left: 5px; padding-right: 5px; padding-bottom: 0px;">

                                                            <div class="portlet box green" id="form_wizard_5">
                                                                <div class="portlet-title">
                                                                    <div class="caption">
                                                                        <i class="fa fa-gift"></i>Mapping
                                                                    </div>
                                                                    <div class="tools hidden-xs">
                                                                        <a href="javascript:;" class="collapse"></a>
                                                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                        <a href="javascript:;" class="reload"></a>
                                                                        <a href="javascript:;" class="remove"></a>
                                                                    </div>
                                                                    <div class="actions btn-set">
                                                                        <asp:LinkButton ID="LinkMapping" runat="server" CssClass="btn yellow-crusta button-next" Style="width: 100px; padding-top: 4px; padding-bottom: 4px; height: 30px;" OnClick="LinkMapping_Click">Continue <i class="m-icon-swapright m-icon-white"></i></asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="portlet-body form">
                                                                    <div class="portlet-body">
                                                                        <div class="form-wizard">
                                                                            <%-- Mapping for Module --%>
                                                                            <div class="form-group">
                                                                                    <div class="col-md-12">
                                                                                        <div class="portlet box blue-hoki">
                                                                                            <div class="portlet-title" style="margin-top: 5px;">
                                                                                                <div class="caption">
                                                                                                    <i class="fa fa-gift"></i>List Module Mapping
                                                                                                </div>
                                                                                                <div class="tools">
                                                                                                    <a href="javascript:;" class="collapse"></a>
                                                                                                    <a href="javascript:;" class="reload"></a>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="portlet-body form">
                                                                                                <div class="form-body">
                                                                                                    <div class="form-group">
                                                                                                        <div class="col-md-12">
                                                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                                                <thead>
                                                                                                                    <tr>
                                                                                                                        <th>Privilage</th>
                                                                                                                        <th>Module</th>
                                                                                                                        <th>User</th>
                                                                                                                        <th>Active</th>
                                                                                                                    </tr>
                                                                                                                </thead>
                                                                                                                <tbody>
                                                                                                                    <asp:ListView ID="ListModuleMapping" runat="server">
                                                                                                                        <ItemTemplate>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Privilage(Convert.ToInt32(Eval("PRIVILEGE_ID"))) %>'></asp:Label></td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Module(Convert.ToInt32(Eval("MODULE_ID"))) %>'></asp:Label></td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label3" runat="server" Text='<%# USER(Convert.ToInt32(Eval("UserID"))) %>'></asp:Label></td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("ACTIVE_FLAG") %>'></asp:Label></td>
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
                                                                                </div>
                                                                            <%-- Mapping for Role --%>
                                                                            <div class="form-group">
                                                                                    <div class="col-md-12">
                                                                                        <div class="portlet box blue-hoki">
                                                                                            <div class="portlet-title" style="margin-top: 5px;">
                                                                                                <div class="caption">
                                                                                                    <i class="fa fa-gift"></i>List Role Mapping
                                                                                                </div>
                                                                                                <div class="tools">
                                                                                                    <a href="javascript:;" class="collapse"></a>
                                                                                                    <a href="javascript:;" class="reload"></a>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="portlet-body form">
                                                                                                <div class="form-body">
                                                                                                    <div class="form-group">
                                                                                                        <div class="col-md-12">
                                                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                                                <thead>
                                                                                                                    <tr>
                                                                                                                        <th>Privilage</th>
                                                                                                                        <th>User</th>
                                                                                                                        <th>Role</th>
                                                                                                                        <th>Active</th>
                                                                                                                    </tr>
                                                                                                                </thead>
                                                                                                                <tbody>
                                                                                                                    <asp:ListView ID="ListRoleMapping" runat="server">
                                                                                                                        <ItemTemplate>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Privilage(Convert.ToInt32(Eval("PRIVILEGE_ID"))) %>'></asp:Label></td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# USER(Convert.ToInt32(Eval("USER_ID"))) %>'></asp:Label></td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label3" runat="server" Text='<%# ROLE(Convert.ToInt32(Eval("ROLE_ID"))) %>'></asp:Label></td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("ACTIVE_FLAG") %>'></asp:Label></td>
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
                                                                                </div>
                                                                            <%-- Mapping for user --%>
                                                                            <div class="form-group">
                                                                                    <div class="col-md-12">
                                                                                        <div class="portlet box blue-hoki">
                                                                                            <div class="portlet-title" style="margin-top: 5px;">
                                                                                                <div class="caption">
                                                                                                    <i class="fa fa-gift"></i>List Right Mapping
                                                                                                </div>
                                                                                                <div class="tools">
                                                                                                    <a href="javascript:;" class="collapse"></a>
                                                                                                    <a href="javascript:;" class="reload"></a>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="portlet-body form">
                                                                                                <div class="form-body">
                                                                                                    <div class="form-group">
                                                                                                        <div class="col-md-12">
                                                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                                                <thead>
                                                                                                                    <tr>
                                                                                                                        <th>Privilage</th>
                                                                                                                        <th>User</th>

                                                                                                                    </tr>
                                                                                                                </thead>
                                                                                                                <tbody>
                                                                                                                    <asp:ListView ID="ListUserMapping" runat="server">
                                                                                                                        <ItemTemplate>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Privilage(Convert.ToInt32(Eval("PRIVILEGE_ID"))) %>'></asp:Label></td>
                                                                                                                                <td>
                                                                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# USER(Convert.ToInt32(Eval("USER_ID"))) %>'></asp:Label></td>
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
                                                                                </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane" id="tab5" runat="server">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-horizontal form-row-seperated">
                                                                <div class="portlet light">
                                                                    <!-- END PAGE BREADCRUMB -->
                                                                    <!-- BEGIN PAGE BASE CONTENT -->
                                                                    <div class="invoice">
                                                                        <div class="row invoice-logo">
                                                                            <div class="col-xs-6 invoice-logo-space" style="width: 300px;">
                                                                                <img src="/Gallery/Logo.jpg" class="img-responsive" alt="" style="width: 300px;" />
                                                                            </div>
                                                                            <div class="col-xs-6">
                                                                                <p>
                                                                                    <asp:Label ID="LblDate" runat="server"></asp:Label>

                                                                                    <span class="muted">Consectetuer adipiscing elit </span>
                                                                                </p>
                                                                            </div>
                                                                        </div>
                                                                        <hr />
                                                                        <div class="row">
                                                                            <div class="col-xs-4">
                                                                                <h3>Client:</h3>
                                                                                <ul class="list-unstyled">
                                                                                    <li>
                                                                                        <asp:Label ID="labelUSerNAme" runat="server"></asp:Label></li>
                                                                                    <li>
                                                                                        <asp:Label ID="LabelUserAddresh" runat="server"></asp:Label>
                                                                                    </li>
                                                                                </ul>
                                                                            </div>
                                                                            <div class="col-xs-4">
                                                                                <h3>About:</h3>
                                                                                <ul class="list-unstyled">
                                                                                    <li>Drem psum dolor sit amet </li>
                                                                                    <li>Laoreet dolore magna </li>
                                                                                    <li>Consectetuer adipiscing elit </li>
                                                                                    <li>Magna aliquam tincidunt erat volutpat </li>
                                                                                    <li>Olor sit amet adipiscing eli </li>
                                                                                    <li>Laoreet dolore magna </li>
                                                                                </ul>
                                                                            </div>
                                                                            <div class="col-xs-4 invoice-payment">
                                                                                <h3>Payment Details:</h3>
                                                                                <ul class="list-unstyled">
                                                                                    <li>
                                                                                        <strong>V.A.T Reg #:</strong> 542554(DEMO)78 </li>
                                                                                    <li>
                                                                                        <strong>Account Name:</strong> FoodMaster Ltd </li>
                                                                                    <li>
                                                                                        <strong>SWIFT code:</strong> 45454DEMO545DEMO </li>
                                                                                    <li>
                                                                                        <strong>V.A.T Reg #:</strong> 542554(DEMO)78 </li>
                                                                                    <li>
                                                                                        <strong>Account Name:</strong> FoodMaster Ltd </li>
                                                                                    <li>
                                                                                        <strong>SWIFT code:</strong> 45454DEMO545DEMO </li>
                                                                                </ul>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <h3 class="block">User Quotation</h3>
                                                                            <div class="col-xs-12">
                                                                                <table class="table table-striped table-hover">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th style="width: 5%"># </th>
                                                                                            <th style="width: 15%">User Name </th>
                                                                                            <th class="hidden-xs" style="width: 50%">Description </th>

                                                                                            <th class="hidden-xs" style="width: 30%">Unit Cost </th>

                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:ListView ID="listProductst" runat="server">

                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td><%#Container.DataItemIndex+1%></td>
                                                                                                    <td>User <%# Container.DataItemIndex+1 %>:</td>
                                                                                                    <td class="hidden-xs">
                                                                                                        <asp:TextBox ID="TextBox1" class="form-control" runat="server"></asp:TextBox></td>

                                                                                                    <td class="hidden-xs">
                                                                                                        <asp:TextBox ID="txtuomQty" class="clsTxtToCalculate" Placeholder="K.D" runat="server"></asp:TextBox></td>

                                                                                                </tr>
                                                                                            </ItemTemplate>

                                                                                        </asp:ListView>
                                                                                    </tbody>
                                                                                    <tfoot>
                                                                                        <tr>
                                                                                            <th colspan="2"></th>
                                                                                            <th>User Total Amunt</th>
                                                                                            <%-- <th>Amount</th>--%>
                                                                                            <th style="color: green">
                                                                                                <asp:Label ID="lblqtytotl123" runat="server" Text="0"></asp:Label></th>
                                                                                        </tr>
                                                                                    </tfoot>
                                                                                </table>
                                                                            </div>

                                                                        </div>
                                                                        <div class="row">
                                                                            <h3 class="block">Function Quotation</h3>
                                                                            <div class="col-xs-12">
                                                                                <table class="table table-striped table-hover">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th style="width: 5%"># </th>

                                                                                            <th style="width: 25%">Item </th>
                                                                                            <th class="hidden-xs" style="width: 50%">Description </th>

                                                                                            <th class="hidden-xs" style="width: 20%">Unit Cost </th>

                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <asp:ListView ID="ListView3" runat="server">

                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td><%#Container.DataItemIndex+1%></td>
                                                                                                    <td><%# Eval("MENU_NAME1")%></td>
                                                                                                    <td class="hidden-xs">
                                                                                                        <asp:TextBox ID="TextBox1" class="form-control" runat="server"></asp:TextBox>
                                                                                                    </td>

                                                                                                    <td class="hidden-xs">
                                                                                                        <asp:TextBox ID="txtuomQty" class="clsTxtToCalculate1" Placeholder="K.D" runat="server"></asp:TextBox>
                                                                                                    </td>

                                                                                                </tr>
                                                                                            </ItemTemplate>

                                                                                        </asp:ListView>

                                                                                    </tbody>
                                                                                    <tfoot>
                                                                                        <tr>
                                                                                            <th colspan="2"></th>
                                                                                            <th>Function Total Amunt</th>
                                                                                            <%-- <th>Amount</th>--%>
                                                                                            <th style="color: green">
                                                                                                <asp:Label ID="lblFuctio" runat="server" Text="0"></asp:Label></th>
                                                                                        </tr>
                                                                                    </tfoot>
                                                                                </table>
                                                                            </div>

                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-xs-4">
                                                                                <div class="well">
                                                                                    <address>
                                                                                        <strong>Loop, Inc.</strong>
                                                                                        <br />
                                                                                        795 Park Ave, Suite 120
                                       
                                    <br />
                                                                                        San Francisco, CA 94107
                                       
                                    <br />
                                                                                        <abbr title="Phone">P:</abbr>
                                                                                        (234) 145-1810
                                                                                    </address>
                                                                                    <address>
                                                                                        <strong>Full Name</strong>
                                                                                        <br />
                                                                                        <a href="mailto:#">first.last@email.com </a>
                                                                                    </address>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-xs-8 invoice-block">
                                                                                <ul class="list-unstyled amounts">
                                                                                    <li>
                                                                                        <strong>User Total amount:</strong>K.D
                                                    <asp:Label ID="lblSubtotal" class="clsTxtToCalculate123" onfocusout="multiplication();" runat="server"></asp:Label>
                                                                                    </li>
                                                                                    <li>
                                                                                        <strong>Fuction Total amount:</strong>K.D
                                                    <asp:Label ID="lblFuvtion" class="clsTxtToCalculate123" onfocusout="multiplication();" runat="server"></asp:Label>
                                                                                    </li>
                                                                                    <li>
                                                                                        <strong>Discount:</strong><asp:Label ID="lblDiscount" runat="server" Text="5"></asp:Label>% </li>
                                                                                    <li>
                                                                                        <strong>VAT:</strong>
                                                                                        <asp:Label ID="lblVat" runat="server" Text="12.5"></asp:Label>%</li>
                                                                                    <li>
                                                                                        <strong>Grand Total:</strong> K.D
                                                    <asp:Label ID="lblGalredTot" runat="server"></asp:Label>
                                                                                    </li>
                                                                                </ul>
                                                                                <br />
                                                                                <a class="btn btn-lg blue hidden-print margin-bottom-5" onclick="javascript:window.print();">Print
                                   
                                <i class="fa fa-print"></i>
                                                                                </a>
                                                                                <a class="btn btn-lg green hidden-print margin-bottom-5">Submit Your Invoice
                                   
                                <i class="fa fa-check"></i>
                                                                                </a>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <!-- END PAGE BASE CONTENT -->
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- <div class="form-actions">
                                            <div class="row">
                                                <div class="col-md-offset-3 col-md-9">

                                                    <a href="javascript:;" class="btn blue button-next">Save & Continue 
                                                    </a>
                                                    <a href="javascript:;" class="btn green button-submit">Submit 
                                                    </a>
                                                </div>
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- END PAGE CONTENT-->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <%-- <script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core componets
            Layout.init(); // init layout
            Demo.init(); // init demo features 
            
        });
</script>--%>
</asp:Content>
