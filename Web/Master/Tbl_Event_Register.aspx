<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="Tbl_Event_Register.aspx.cs" Inherits="Web.Master.Tbl_Event_Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }
    </style>
    <script type="text/javascript">
        function previewFile() {
            var preview = document.querySelector('#<%=Avatar.ClientID %>');
            var file = document.querySelector('#<%=FileUpload1.ClientID %>').files[0];
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="PrductJs/Productjs.js"></script>
    <div id="b" runat="server">
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="tabbable-custom tabbable-noborder">
                        <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                            <div class="alert alert-success alert-dismissable">
                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </div>
                        </asp:Panel>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-horizontal form-row-seperated">
                                    <div class="portlet box blue">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>Add 
                                        <asp:Label runat="server" ID="lblHeader"></asp:Label>
                                                <asp:TextBox Style="color: #333333" ID="txtHeader" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <asp:LinkButton ID="btnPagereload" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                            <div class="actions btn-set">
                                                <div class="btn-group btn-group-circle btn-group-solid">
                                                    <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                                    <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                                    <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                                    <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />
                                                </div>
                                                <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="Add New" OnClick="btnAdd_Click" />
                                                <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                                <asp:Button ID="btnEditLable" runat="server" class="btn green-haze btn-circle" OnClick="btnEditLable_Click" Text="Update Label" />
                                                &nbsp;
                                        <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="../assets/global/img/flags/us.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="../assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="../assets/global/img/flags/fr.png" /></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="portlet-body form">
                                                <div class="tabbable">
                                                    <div class="tab-content no-space">
                                                        <div class="tab-pane active" id="tab_general1">
                                                            <div class="form-body">
                                                                <%-- <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>--%>
                                                                <asp:Panel ID="pnldanger" runat="server" Visible="false">
                                                                    <div class="alert alert-danger alert-dismissable">
                                                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                        <asp:Label ID="lbldmsg" runat="server"></asp:Label>
                                                                    </div>
                                                                </asp:Panel>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <asp:Label ID="Label12" CssClass="col-md-4 control-label" runat="server" Text="Event & details"></asp:Label>
                                                                            <div class="col-md-6">
                                                                                <asp:DropDownList ID="drpMD" runat="server" CssClass="table-group-action-input form-control" AutoPostBack="true" OnSelectedIndexChanged="drpMD_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="drpMD" ErrorMessage="Event/Detail Required." CssClass="Validation" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row" style="display: none;">
                                                                    <div class="col-md-4">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label ID="lblmainEvent" CssClass="col-md-2 control-label" runat="server" Text="Event"></asp:Label>
                                                                            <div class="col-md-10">
                                                                                <asp:TextBox ID="TextBox1" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorStartDt" runat="server" ControlToValidate="TextBox1" ErrorMessage="Main Event Required." CssClass="Validation" ValidationGroup="sss"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label ID="lblSubevent" CssClass="col-md-1 control-label" runat="server" Text="In"></asp:Label>
                                                                            <div class="col-md-10">
                                                                                <asp:TextBox ID="TextBox2" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox2" ErrorMessage="Event Detail Required." CssClass="Validation" ValidationGroup="sss"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <%--<asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>--%>
                                                                <asp:Panel ID="pnlComSearch" runat="server" DefaultButton="lkbCustomerN1">
                                                                    <div class="row">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label ID="lblCompanySearch" runat="server" class="col-md-4 control-label" Text="Company Search"></asp:Label>
                                                                                <div class="col-md-6">
                                                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search For Company By Name, Mobile, Bus Phone, Address, Email" MaxLength="250" ToolTip="Search For Company By Name, Mobile, Bus Phone, Address, Email"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSearch" ErrorMessage="Company Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                    <asp:Label ID="lblCompanyNames" Visible="false" ForeColor="Green" runat="server" class="control-label"></asp:Label>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:CheckBox ID="CHKListCompany" CssClass="checkbox-list input-inline" runat="server" AutoPostBack="true" OnCheckedChanged="CHKListCompany_CheckedChanged" />
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:LinkButton ID="lkbCustomerN1" CssClass="btn btn-icon-only yellow" runat="server" OnClick="lkbCustomerN1_Click"><i class="fa fa-search" ></i>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label ID="lblCompanyName" runat="server" class="col-md-4 control-label" Text="Company Name"></asp:Label>
                                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="drpCompanyName" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpCompanyName_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:Image ID="ImageCompany1" runat="server" ImageUrl="~/ECOMM/images/Add.png" OnClick="javascript:showADDCompany();" Style="float: right" AlternateText="contact" />
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <asp:Panel ID="pnlCompany" runat="server" Style="display: none">
                                                                        <fieldset style="margin-bottom: 10px; margin-left: 10px;">
                                                                            <legend>
                                                                                <asp:Label ID="Label4" runat="server" ForeColor="#4A8BC2" Text="ADD Company"></asp:Label></legend>
                                                                            <div class="row" style="margin-right: 0px;">
                                                                                <div class="form-group">
                                                                                    <div class="col-md-6">
                                                                                        <asp:Label runat="server" ID="Label6" class="col-md-4 control-label" ForeColor="#4A8BC2" Text="Company Name"></asp:Label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtaddCompany" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                                                            <asp:Label ID="lblErrorMSGCompany" ForeColor="Red" runat="server" Visible="false" Text=""></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-6">
                                                                                        <asp:Button ID="btnCompany" runat="server" CssClass="btn btn-primary" OnClick="btnCompany_Click" Text="Save" Height="34px" />
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                        </fieldset>
                                                                    </asp:Panel>
                                                                </asp:Panel>
                                                                <%--</ContentTemplate>
                                                                </asp:UpdatePanel>--%>
                                                                <%-- <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>--%>
                                                                <asp:Panel ID="pnlContSearch" runat="server" DefaultButton="lkbContact">
                                                                    <div class="row">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label ID="lblContact" runat="server" class="col-md-4 control-label" Text="Attendee search"></asp:Label>
                                                                                <div class="col-md-6">
                                                                                    <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" placeholder="Serach For Attendee by Name, Mobile, Bussiness Phone, Email" ToolTip="Serach For Attendee by Name, Mobile, Bussiness Phone, Email"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtContact" ErrorMessage="Attendee Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                    <asp:Label ID="lblContactsa" ForeColor="Green" runat="server" class="control-label"></asp:Label>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:CheckBox ID="CHKListContact" CssClass="checkbox-list input-inline" runat="server" AutoPostBack="true" OnCheckedChanged="CHKListContact_CheckedChanged" />
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:LinkButton ID="lkbContact" CssClass="btn btn-icon-only yellow" runat="server" OnClick="lkbContact_Click"><i class="fa fa-search" ></i>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label ID="lblContacts" runat="server" class="col-md-4 control-label" Text="Attendee"></asp:Label>
                                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="drpContacts" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpContacts_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:Image ID="ImageContact1" runat="server" ImageUrl="~/ECOMM/images/Add.png" OnClick="javascript:showADDContact();" Style="float: right" AlternateText="contact" />
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <asp:Panel ID="pnlContact" runat="server" Style="display: none">
                                                                        <fieldset style="margin-bottom: 10px; margin-left: 10px;">
                                                                            <legend>
                                                                                <asp:Label ID="Label2" runat="server" ForeColor="#4A8BC2" Text="ADD Contact"></asp:Label></legend>
                                                                            <div class="row" style="margin-right: 0px;">
                                                                                <div class="form-group">
                                                                                    <div class="col-md-6">
                                                                                        <asp:Label runat="server" ID="Label3" class="col-md-4 control-label" ForeColor="#4A8BC2" Text="Contact Name"></asp:Label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtAddContact" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-6">
                                                                                        <asp:Label runat="server" ID="Label17" class="col-md-4 control-label" ForeColor="#4A8BC2" Text="EmailID"></asp:Label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtcEmail" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-6">
                                                                                        <asp:Label runat="server" ID="Label8" class="col-md-4 control-label" ForeColor="#4A8BC2" Text="Mobile NO."></asp:Label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtcMobile" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-6">
                                                                                        <asp:Label runat="server" ID="Label9" class="col-md-4 control-label" ForeColor="#4A8BC2" Text="Bus Contact"></asp:Label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtcBuscontact" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-6">
                                                                                        <asp:Label runat="server" ID="Label16" class="col-md-4 control-label" ForeColor="#4A8BC2" Text="Address"></asp:Label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtAdd" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                                                            <asp:Label ID="lblErrorMSGContact" Visible="false" ForeColor="Red" runat="server" Text=""></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-6">
                                                                                        <asp:Button ID="btnContact" runat="server" CssClass="btn btn-primary" OnClick="btnContact_Click" Text="Save" Height="34px" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </fieldset>
                                                                    </asp:Panel>
                                                                </asp:Panel>
                                                                <%-- </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="lkbContact" EventName="Click" />
                                                                        <asp:AsyncPostBackTrigger ControlID="btnContact" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>--%>
                                                                <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>--%>
                                                                <asp:Panel ID="pnlPosiSearch" runat="server" DefaultButton="lkbPositions">
                                                                    <div class="row">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label ID="lblPosition" runat="server" class="col-md-4 control-label" Text="Position Search"></asp:Label>
                                                                                <div class="col-md-6">
                                                                                    <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control" placeholder="You Want To Serach by Your Position in your Company" MaxLength="250" ToolTip="You Want To Serach by Your Position in your Company"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPosition" ErrorMessage="Position Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                    <asp:Label ID="lblPositionSa" runat="server" ForeColor="Green" class="control-label"></asp:Label>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:CheckBox ID="CHKListPosition" CssClass="checkbox-list input-inline" runat="server" AutoPostBack="true" OnCheckedChanged="CHKListPosition_CheckedChanged" />
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:LinkButton ID="lkbPositions" CssClass="btn btn-icon-only yellow" runat="server" OnClick="lkbPositions_Click"><i class="fa fa-search" ></i>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label ID="lblPositions" runat="server" class="col-md-4 control-label" Text="Positions Name"></asp:Label>
                                                                                <div class="col-md-6">
                                                                                    <asp:DropDownList ID="drpPositions" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpPositions_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <asp:Image ID="ImageP1" runat="server" ImageUrl="~/ECOMM/images/Add.png" OnClick="javascript:showADDPpsition();" Style="float: right" AlternateText="brand" />
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <asp:Panel ID="pnlPosition" runat="server" Style="display: none">
                                                                        <fieldset style="margin-bottom: 10px; margin-left: 10px;">
                                                                            <legend>
                                                                                <asp:Label ID="Label32" runat="server" ForeColor="#4A8BC2" Text="ADD Position"></asp:Label></legend>
                                                                            <div class="row" style="margin-right: 0px;">
                                                                                <div class="form-group">
                                                                                    <div class="col-md-6">
                                                                                        <asp:Label runat="server" ID="lblcolorN" class="col-md-4 control-label" ForeColor="#4A8BC2" Text="Position Name"></asp:Label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtAddPosition" runat="server" AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                                                                            <asp:Label ID="lblerrorMSGPosition" runat="server" Visible="false" Text=""></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-6">
                                                                                        <asp:Button ID="btnPosition" runat="server" CssClass="btn btn-primary" OnClick="btnPosition_Click" Text="Save" Height="34px" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </fieldset>
                                                                    </asp:Panel>
                                                                </asp:Panel>
                                                                <%-- </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="lkbPositions" EventName="Click" />
                                                                        <asp:AsyncPostBackTrigger ControlID="btnPosition" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>--%>

                                                                <div class="row">
                                                                    <%--<asp:UpdatePanel ID="updateAttendee" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>--%>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label ID="lblAttendes" runat="server" class="col-md-4 control-label" Text="Attendes"></asp:Label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtAttendes" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <%-- </ContentTemplate>
                                                                    </asp:UpdatePanel>--%>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label ID="lblFileUpload1" runat="server" class="col-md-4 control-label" Text="Image Upload"></asp:Label>
                                                                            <div class="col-md-6">
                                                                                <asp:FileUpload ID="FileUpload1" class="btn btn-circle green-haze btn-sm" runat="server" onchange="previewFile()" />
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="Validation" runat="server" ErrorMessage="Only Image Files Are Allowed"
                                                                                    ControlToValidate="FileUpload1" ValidationGroup="submit" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.JPG|.gif|.GIF|.png|.PNG|.bmp|.BMP|.JPEG|.jpeg|.JFIF|.jfif|.TIFF|.tiff)$">
                                                                                </asp:RegularExpressionValidator>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:Image ID="Avatar" Style="width: 40px; height: 40px;" runat="server" ImageUrl="~/Gallery/defolt.png" class="img-responsive" />
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label ID="lblMobile" runat="server" class="col-md-4 control-label" Text="Mobile"></asp:Label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtMobile" ValidChars="0123456789," FilterType="Custom, numbers" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <%-- </div>
                                                        <div class="row">--%>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label ID="lblBussinessPhone" runat="server" class="col-md-4 control-label" Text="BussinessPhone"></asp:Label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtBussinessPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtBussinessPhone" ValidChars="0123456789," FilterType="Custom, numbers" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label ID="lblEmailId" runat="server" class="col-md-4 control-label" Text="EmailId"></asp:Label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label ID="lblAddress" runat="server" class="col-md-4 control-label" Text="Address"></asp:Label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblRegisteredAs1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRegisteredAs1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                <asp:DropDownList ID="drpRegisteredAs" runat="server" CssClass="table-group-action-input form-control input-medium">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="drpRegisteredAs" ErrorMessage="Registered As Required." CssClass="Validation" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblRegisteredAs2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRegisteredAs2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <%--<asp:UpdatePanel ID="UpdateRegistration" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>--%>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblRegistrationID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRegistrationID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtRegistrationID" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblRegistrationID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRegistrationID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <%-- </ContentTemplate>
                                                                    </asp:UpdatePanel>--%>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label ID="lblbarcodeimg" runat="server" CssClass="col-md-2 control-label" Text="BarCode"></asp:Label>
                                                                            <div class="col-md-4">
                                                                                <asp:Image ID="Image1" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                <asp:PlaceHolder ID="plBarCode" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group" style="margin-right: 0px; margin-left: 0px;">
                                                                        <div class="col-md-12">
                                                                            <asp:Button ID="btnsaveContinue" runat="server" class="btn btn-primary" Text="Save & Continue" OnClick="btnsaveContinue_Click" ValidationGroup="sss" />
                                                                             <asp:Button ID="btnsaveprint" Visible="false" runat="server" class="btn btn-primary" Text="Save & Print" OnClick="btnsaveprint_Click" ValidationGroup="submit" />
                                                                        </div>
                                                                    </div>
                                                                </div>                                                               

                                                                <div class="alert alert-info alert-dismissable">
                                                                    <asp:Label ID="Label1" Text="View Only" Font-Bold="true" runat="server"></asp:Label>
                                                                </div>
                                                                <asp:Panel ID="pnlviewonly" runat="server" Visible="false">
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label ID="lblwalkondate" ForeColor="Green" CssClass="col-md-12 control-label" runat="server" Text=""></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label ID="lblBatchPrintedon" ForeColor="Green" CssClass="col-md-12 control-label" runat="server" Text=""></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label ID="lblCertificatePrintedon" ForeColor="Green" CssClass="col-md-12 control-label" runat="server" Text=""></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="portlet box green-haze">
                                                                            <div class="portlet-title">
                                                                                <div class="caption">
                                                                                    <i class="fa fa-paypal"></i>
                                                                                    <asp:Label runat="server" ID="Label11" Text="Payment"></asp:Label>
                                                                                </div>
                                                                                <div class="tools">
                                                                                    <a id="Paytype" runat="server" href="javascript:;" class="collapse"></a>
                                                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                                    <a href="javascript:;" class="reload"></a>
                                                                                    <a href="javascript:;" class="remove"></a>
                                                                                </div>
                                                                            </div>
                                                                            <div id="PayMethod" runat="server" class="portlet-body" style="display: block;">
                                                                                <div class="portlet-body form">
                                                                                    <div class="tabbable">
                                                                                        <div class="tab-content no-space">
                                                                                            <div class="form-body">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="form-group" style="color: ">
                                                                                                            <asp:Label runat="server" ID="lblPaidBy1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPaidBy1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                            <div class="col-md-8">
                                                                                                                <asp:DropDownList ID="drpPaydby" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Pay By Required." ControlToValidate="drpPaydby" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                                            </div>
                                                                                                            <asp:Label runat="server" ID="lblPaidBy2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPaidBy2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>

                                                                                                    <div class="col-md-6">
                                                                                                        <div class="form-group" style="color: ">
                                                                                                            <asp:Label runat="server" ID="lblAmountPaid1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAmountPaid1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                            <div class="col-md-8">
                                                                                                                <asp:TextBox ID="txtAmountPaid" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAmountPaid_TextChanged"></asp:TextBox>
                                                                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredAmountPaid" runat="server" ErrorMessage="Amount Required." ControlToValidate="txtAmountPaid" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                                                                <asp:RangeValidator runat="server" ID="rngAmount" ControlToValidate="txtAmountPaid" Type="Double" MinimumValue="0" MaximumValue="0" ValidationGroup="submit" ForeColor="Red" ErrorMessage="Please enter Minimum Amount" />
                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtAmountPaid" ValidChars="0123456789." FilterType="Custom, numbers" runat="server" />
                                                                                                            </div>
                                                                                                            <asp:Label runat="server" ID="lblAmountPaid2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAmountPaid2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="form-group" style="color: ">
                                                                                                            <asp:Label runat="server" ID="lblPaymentReference1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPaymentReference1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                            <div class="col-md-8">
                                                                                                                <asp:TextBox ID="txtPaymentReference" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <asp:Label runat="server" ID="lblPaymentReference2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPaymentReference2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="col-md-6">
                                                                                                        <div class="form-group" style="color: ">
                                                                                                            <asp:Label runat="server" ID="lblNotes1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNotes1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                                            <div class="col-md-8">
                                                                                                                <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <asp:Label runat="server" ID="lblNotes2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNotes2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
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
                                                                <%-- </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="lkbContact" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="btnContact" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="lkbPositions" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="btnPosition" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="lkbCustomerN1" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="btnCompany" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>--%>
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
                                <div class="form-horizontal form-row-seperated">
                                    <div class="portlet box blue">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>
                                                <asp:Label runat="server" ID="Label5"></asp:Label>
                                                List
                                            </div>

                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <asp:LinkButton ID="btnlistreload" OnClick="btnlistreload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>

                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                        </div>
                                        <div class="portlet-body form">
                                            <asp:Panel runat="server" ID="pnlGrid">
                                                <div class="tab-content">
                                                    <div id="tab_1_1" class="tab-pane active">
                                                        <div class="tab-content no-space">
                                                            <div class="tab-pane active" id="tab_general2">
                                                                <div class="table-container" style="">
                                                                    <div class="portlet-body">
                                                                        <div id="sample_1_wrapper" class="dataTables_wrapper no-footer">
                                                                            <div class="row">
                                                                                <div class="col-md-2 col-sm-12">
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
                                                                                            entries</label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-4 col-sm-12">
                                                                                    <div class="form-group" style="color: ">
                                                                                        <asp:Label runat="server" ID="lblEventID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEventID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="drpEventID" AppendDataBoundItems="true" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpEventID_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator InitialValue="0" CssClass="Validation" ID="DRPEventERRO" Display="Dynamic" runat="server" ControlToValidate="drpEventID" ErrorMessage="Required Main Event" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                        <asp:Label runat="server" ID="lblEventID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEventID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12">
                                                                                    <div class="form-group" style="color: ">
                                                                                        <asp:Label runat="server" ID="lblMyID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMyID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="drpMyID" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpMyID_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator InitialValue="0" CssClass="Validation" ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ControlToValidate="drpMyID" ErrorMessage="Required Details" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                        <asp:Label runat="server" ID="lblMyID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMyID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3 col-sm-12">
                                                                                    <div id="sample_1_filter" class="dataTables_filter">
                                                                                        <label>
                                                                                             <asp:TextBox ID="txtsearch123" Placeholder="Search" class="form-control input-small input-inline" runat="server"></asp:TextBox>
                                                                                            <asp:LinkButton ID="LinkButton10" runat="server" class="btn btn-sm yellow filter-submit margin-bottom" OnClick="LinkButton10_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                                                                        </label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="table-scrollable">
                                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                                                            <thead>
                                                                                                <tr>
                                                                                                    <th>
                                                                                                        <asp:Label ID="Label7" runat="server" Text="ID"></asp:Label></th>
                                                                                                    <th colspan="2">
                                                                                                        <asp:Label ID="lblEventID" runat="server">Event/Detail<br />(ID + Name)</asp:Label></th>
                                                                                                    <%--<th>
                                                                                                <asp:Label ID="lblMyID" runat="server" Text="Detail ID"></asp:Label></th>--%>
                                                                                                    <th>
                                                                                                        <asp:Label ID="lblAttendee" runat="server" Text="Attendee"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="lblCompany" runat="server" Text="Company"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="lblMobile1" runat="server" Text="Mobile"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label ID="lblemail" runat="server" Text="Email"></asp:Label></th>
                                                                                                    <%-- <th>
                                                                                                <asp:Label ID="lblPaidBy" runat="server" Text="PaidBy"></asp:Label></th>--%>
                                                                                                    <th>
                                                                                                        <asp:Label ID="lblRegas" runat="server" Text="Registered As"></asp:Label></th>
                                                                                                    <th style="text-align: center;">
                                                                                                        <asp:Button ID="btnBarcodePrint" CssClass="btn btn-sm blue" runat="server" Text="Print" OnClick="btnBarcodePrint_Click" />
                                                                                                        <asp:CheckBox ID="CHKBarcodeAll" runat="server" AutoPostBack="true" OnCheckedChanged="CHKBarcodeAll_CheckedChanged" />
                                                                                                    </th>
                                                                                                    <th style="width: 60px;">ACTION</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand">
                                                                                                    <LayoutTemplate>
                                                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                                                        </tr>
                                                                                                    </LayoutTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <tr class="gradeA">
                                                                                                            <td>
                                                                                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("RegistrationID") %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblEventID" runat="server" Text='<%# getEvent(Convert.ToInt32(Eval("EventID"))) %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblMyID" runat="server" Text='<%# getSubEventID(Convert.ToInt32(Eval("EventID")),Convert.ToInt32(Eval("MyID"))) %>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblAttendee1" runat="server" Text='<%# Eval("Attendee")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblcompany1" runat="server" Text='<%# Eval("CompanyName")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblMobile11" runat="server" Text='<%# Eval("MobileNo")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblemail1" runat="server" Text='<%# Eval("Email")%>'></asp:Label></td>
                                                                                                            <%-- <td>
                                                                                                        <asp:Label ID="lblPaidBy" runat="server" Text='<%# getPaidByID(Convert.ToInt32(Eval("PaidBy"))) %>'></asp:Label></td>--%>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblRegas1" runat="server" Text='<%# RegistrationAS(Convert.ToInt32(Eval("RegisteredAs"))) %>'></asp:Label></td>
                                                                                                            <td style="text-align: center;">
                                                                                                                <asp:CheckBox ID="CHKBarcodePrint" runat="server" />
                                                                                                                <asp:Label ID="LBLBARCODEE" Visible="false" runat="server" Text='<%# Eval("BARCODE") %>'></asp:Label>
                                                                                                            </td>

                                                                                                            <td>
                                                                                                                <table>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("EventID")+","+ Eval("MyID")+","+Eval("ContactMyID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("EventID")+","+ Eval("MyID")+","+Eval("ContactMyID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                                                        <%-- <td><asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("JobId")%>' CommandName="btnPrint" CommandArgument='<%# Eval("JobId")%>' runat="server" class="btn btn-sm green filter-submit margin-bottom"><i class="fa fa-print"></i></asp:LinkButton></td>--%>
                                                                                                                    </tr>
                                                                                                                </table>

                                                                                                            </td>
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
                                                                                        <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                                <ContentTemplate>--%>
                                                                                        <ul class="pagination">
                                                                                            <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_fist">
                                                                                                <%--  <asp:LinkButton ID="Button1" runat="server"  BorderStyle="None" />First</asp:LinkButton> --%>
                                                                                                <asp:LinkButton ID="btnfirst1" OnClick="btnfirst_Click" runat="server"> First</asp:LinkButton>
                                                                                            </li>
                                                                                            <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_Next">
                                                                                                <%--  <asp:LinkButton ID="Button1" runat="server"  BorderStyle="None" />First</asp:LinkButton> --%>
                                                                                                <asp:LinkButton ID="btnNext1" OnClick="btnNext1_Click" runat="server"> Next</asp:LinkButton>
                                                                                            </li>
                                                                                            <asp:ListView ID="ListView2" runat="server" OnItemCommand="ListView2_ItemCommand" OnItemDataBound="AnswerList_ItemDataBound">
                                                                                                <ItemTemplate>
                                                                                                    <td>
                                                                                                        <li class="paginate_button " aria-controls="sample_1" tabindex="0">
                                                                                                            <asp:LinkButton ID="LinkPageavigation" runat="server" CommandName="LinkPageavigation" CommandArgument='<%# Eval("ID")%>'> <%# Eval("ID")%></asp:LinkButton></li>

                                                                                                    </td>
                                                                                                </ItemTemplate>
                                                                                            </asp:ListView>
                                                                                            <li class="paginate_button next" aria-controls="sample_1" tabindex="0" id="sample_1_Previos">
                                                                                                <asp:LinkButton ID="btnPrevious1" OnClick="btnPrevious1_Click" runat="server"> Prev</asp:LinkButton>
                                                                                            </li>
                                                                                            <li class="paginate_button next" aria-controls="sample_1" tabindex="0" id="sample_1_last">
                                                                                                <asp:LinkButton ID="btnLast1" OnClick="btnLast1_Click" runat="server"> Last</asp:LinkButton>
                                                                                            </li>
                                                                                        </ul>
                                                                                        <%--  </ContentTemplate>
                                                                                            </asp:UpdatePanel>--%>
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
                                </div>
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lkbContact" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnContact" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lkbPositions" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnPosition" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lkbCustomerN1" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnCompany" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnfirst1" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnNext1" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnPrevious1" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnLast1" EventName="Click" />
        </Triggers>

    </asp:UpdatePanel>

    <<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text=" Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="../assets/admin/layout4/img/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
