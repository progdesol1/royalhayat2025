<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="tbl_Event_Detail.aspx.cs" Inherits="Web.Master.tbl_Event_Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <%--<ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">tbl_Event_Detail </a>
                    </li>
                </ul>--%>
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
                                                        <div class="row">
                                                            <%--<div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblEventID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEventID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpEventID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorEventID" runat="server" ErrorMessage="Event name Required." ControlToValidate="drpEventID" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblEventID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEventID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>--%>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <%--<div class="col-md-4">
                                                                <div class="form-group">
                                                                    
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtevename" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                            <div class="col-md-12">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label ID="lblevename" CssClass="col-md-1 control-label" runat="server" Text="Event Name"></asp:Label>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtevename" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                                    </div>

                                                                    <asp:Label runat="server" ID="lblMyID1s" CssClass="col-sm-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMyID1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>                                                                    
                                                                    <div class="col-md-2">
                                                                        <%--<asp:Label ID="lblSubEventID" runat="server" CssClass="col-md-1 control-label" Enabled="false" Text=""></asp:Label>--%>
                                                                        <asp:TextBox ID="txtSubEventID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                       <%-- <asp:TextBox ID="txtMyID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMyID" runat="server" ControlToValidate="txtMyID" ErrorMessage="My name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblMyID2h" CssClass="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMyID2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4 form-group style="margin-right: 0px; margin-left: 0px;"">
                                                                <%--<div class="form-group" style="color: ">--%>
                                                                    <asp:Label runat="server" ID="lblDescription11s" CssClass="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescription11s" CssClass="col-md-12 control-label" Visible="false"></asp:TextBox>                                                                    
                                                                    <div class="col-md-12">
                                                                        <asp:TextBox ID="txtDescription1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDescription1" runat="server" ControlToValidate="txtDescription1" ErrorMessage="Event Description 1 Required." CssClass="Validation" ValidationGroup="submit123"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDescription12h" CssClass="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescription12h" CssClass="col-md-12 control-label" Visible="false"></asp:TextBox>
                                                                <%--</div>--%>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-4 form-group style="margin-right: 0px; margin-left: 0px;"">
                                                                <%--<div class="form-group" style="color: ">--%>
                                                                    <asp:Label runat="server" ID="lblDescription21s" CssClass="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescription21s" CssClass="col-md-12 control-label" Visible="false"></asp:TextBox>                                                                    
                                                                    <div class="col-md-12">
                                                                        <asp:TextBox ID="txtDescription2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDescription2" runat="server" ControlToValidate="txtDescription2" ErrorMessage="Description 2 Required." CssClass="Validation" ValidationGroup="submit123"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDescription22h" CssClass="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescription22h" CssClass="col-md-12 control-label" Visible="false"></asp:TextBox>
                                                               <%-- </div>--%>
                                                            </div>

                                                             <div class="col-md-4 form-group style="margin-right: 0px; margin-left: 0px;"">
                                                                <%--<div class="form-group" style="color: ">--%>
                                                                    <asp:Label runat="server" ID="lblDescription31s" CssClass="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescription31s" CssClass="col-md-12 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-12">
                                                                        <asp:TextBox ID="txtDescription3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDescription3" runat="server" ControlToValidate="txtDescription3" ErrorMessage="Description 3 Required." CssClass="Validation" ValidationGroup="submit123"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDescription32h" CssClass="col-md-12 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDescription32h" CssClass="col-md-12 control-label" Visible="false"></asp:TextBox>
                                                                <%--</div>--%>
                                                            </div>
                                                        </div>                                                    
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <%--<asp:Label runat="server" ID="lblForcastedVisitor1s" Text="Forcast Visiter" class="col-md-4 control-label">
                                                                    </asp:Label><asp:TextBox runat="server" ID="txtForcastedVisitor1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>--%>
                                                                    <asp:Label ID="lblForCastVisiter" runat="server" CssClass="col-md-4 control-label" Text="Forcaste Visitor"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <%-- <asp:DropDownList ID="drpForcastedVisitor" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                        <asp:TextBox ID="txtForcastedVisitor" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                     
                                                                                    
                                                                                   <%-- <div class="col-md-9" style="width: 335px">--%>
                                                                                        <div id="spinner1">
                                                                                            <div class="input-group input-small">
                                                                                                <asp:TextBox ID="txtForcastedVisitor" runat="server" class="spinner-input form-control" MaxLength="3"></asp:TextBox>                                                                                                
                                                                                                <div class="spinner-buttons input-group-btn btn-group-vertical">
                                                                                                    <button class="btn spinner-up btn-xs blue" type="button">
                                                                                                        <i class="fa fa-angle-up"></i>
                                                                                                    </button>
                                                                                                    <button class="btn spinner-down btn-xs blue" type="button">
                                                                                                        <i class="fa fa-angle-down"></i>
                                                                                                    </button>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                   <%-- </div>--%>
                                                                              
                                                                    </div>
                                                                    <%--<asp:Label runat="server" ID="lblForcastedVisitor2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtForcastedVisitor2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>--%>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblEventType1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEventType1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpEventType" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorEventType" runat="server" ErrorMessage="Event Type Required." ControlToValidate="drpEventType" ValidationGroup="submit123" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblEventType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEventType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblEventTopic1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEventTopic1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpEventTopic" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorEventTopic" runat="server" ErrorMessage="Event Topic Required." ControlToValidate="drpEventTopic" ValidationGroup="submit123" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblEventTopic2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEventTopic2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCategoryID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCategoryID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                       <%-- <asp:DropDownList ID="drpCategoryID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorCategoryID" runat="server" ErrorMessage="Category name Required." ControlToValidate="drpCategoryID" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                                         <asp:TextBox ID="txtCategoryID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCategoryID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCategoryID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblFromDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtFromDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtFromDate" runat="server" CssClass="form-control"> </asp:TextBox>
                                                                        <cc1:CalendarExtender ID="TextBoxFromDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtFromDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorFromDate" runat="server" ControlToValidate="txtFromDate" ErrorMessage="Detail From Date Required." CssClass="Validation" ValidationGroup="submit123"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblFromDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtFromDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblToDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtToDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtToDate" runat="server" CssClass="form-control"> </asp:TextBox>
                                                                        <cc1:CalendarExtender ID="TextBoxToDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtToDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorToDate" runat="server" ControlToValidate="txtToDate" ErrorMessage="Detail To Date Required." CssClass="Validation" ValidationGroup="submit123"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblToDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtToDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblRegURL1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRegURL1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtRegURL" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRegURL" runat="server" ControlToValidate="txtRegURL" ErrorMessage="Registeration URL Required." CssClass="Validation" ValidationGroup="submit123"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblRegURL2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRegURL2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblRegisterationIDBegin1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRegisterationIDBegin1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-6">
                                                                        <asp:TextBox ID="txtRegisterationIDBegin" runat="server" MaxLength="10" placeholder="Alphabet" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtRegisterationIDBegin_TextChanged"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRegisterationIDBegin" runat="server" ControlToValidate="txtRegisterationIDBegin" ErrorMessage="Registeration ID Required." CssClass="Validation" ValidationGroup="submit123"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtRegNO" Text="1" runat="server" placeholder="Number" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtRegNO" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRegNO" ErrorMessage="Registeration NO Required." CssClass="Validation" ValidationGroup="submit123"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblRegisterationIDBegin2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRegisterationIDBegin2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>                                                                    
                                                                </div>
                                                            </div>
                                                        </div>
                                                       
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblKeyWord1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtKeyWord1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">                                                                       
                                                                        <asp:TextBox ID="txtKeyWord" runat="server" CssClass="form-control tags"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorKeyWord" runat="server" ControlToValidate="txtKeyWord" ErrorMessage="KeyWord Required." CssClass="Validation" ValidationGroup="submit123"></asp:RequiredFieldValidator>                                                                    
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblKeyWord2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtKeyWord2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                         
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblRate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtRate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRate" runat="server" ControlToValidate="txtRate" ErrorMessage="Rate Required." CssClass="Validation" ValidationGroup="submit123"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblRate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%-- SocialMedia --%>
                                                        <asp:Panel ID="pnlsocialmedia" Enabled="false" runat="server">
                                                            <txtFromDatediv class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblSocMediaID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSocMediaID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpSocMediaID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorSocMediaID" runat="server" ErrorMessage="Socmedia Name Required." ControlToValidate="drpSocMediaID" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                            <asp:Label ID="lblerrorSocialMSG" CssClass="control-label" ForeColor="Red" Visible="false" runat="server" Text="Label"></asp:Label>
                                                                           <%--  <asp:TextBox ID="txtSocMediaID" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblSocMediaID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSocMediaID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                     <div class="col-md-7">
                                                                         <asp:TextBox ID="txtSocial" runat="server" class="form-control"></asp:TextBox>  
                                                                                                                                      
                                                                     </div>
                                                                        <div class="col-md-1">                                                                       
                                                                              <asp:LinkButton ID="linkAddSocial" runat="server" OnClick="linkAddSocial_Click">
                                                                                    <i class="icon-plus " style="color:black;padding-left: 4px;"></i>
                                                                               </asp:LinkButton>                                                                    
                                                                        </div>
                                                                </div>
                                                                                                                       
                                                            </txtFromDatediv>
                                                            <div class="row">
                                                                <div class="col-md-6">
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
                                                                </div>
                                                            </div>

                                                            <%-- FloorPlan --%>
                                                             <div class="row">
                                                                 <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblFloorPlanID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtFloorPlanID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpFloorPlanID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorFloorPlanID" runat="server" ErrorMessage="Floorplan Name Required." ControlToValidate="drpFloorPlanID" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                            <asp:Label ID="lblErrorFloorMSG" CssClass="control-label" ForeColor="Red" Visible="false" runat="server" Text="Label"></asp:Label>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblFloorPlanID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtFloorPlanID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                  <div class="col-md-6">
                                                                      <div class="col-md-7">
                                                                          <asp:TextBox ID="txtFloorPlan" class="form-control" runat="server"></asp:TextBox>
                                                                      </div>
                                                                       <div class="col-md-1">
                                                                           <asp:LinkButton ID="linkFloorPlan" runat="server" OnClick="linkFloorPlan_Click">
                                                                                    <i class="icon-plus " style="color:black;padding-left: 4px;"></i>
                                                                               </asp:LinkButton>  
                                                                      </div>
                                                                </div>
                                                             </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                <div class="tabbable">
                                                                  <table class="table table-striped table-bordered table-hover">
                                                                        <thead>
                                                                              <tr>
                                                                                   <th>Floor Plan</th>
                                                                                   <th>Floor Plan Id</th>
                                                                                   <th>Remark</th>
                                                                                </tr>
                                                                         </thead>
                                                                         <tbody>
                                                                                 <asp:ListView ID="ListFloorPlane" runat="server">
                                                                                      <LayoutTemplate>
                                                                                           <tr id="ItemPlaceholder" runat="server">
                                                                                           </tr>
                                                                                       </LayoutTemplate>
                                                                                       <ItemTemplate>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# getsFloor(Convert .ToInt32 ( Eval("Recource")))%>'></asp:Label></td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("RecValue")%>'></asp:Label></td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblEMAIL" runat="server" Text='<%# getFloorremark(Convert .ToInt32 ( Eval("Recource")))%>'></asp:Label></td>


                                                                                                    </tr>
                                                                                        </ItemTemplate>
                                                                                   </asp:ListView>

                                                                          </tbody>
                                                                   </table>

                                                             </div>
                                                                </div>
                                                            </div>

                                                            <%-- EmailTemplete --%>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lbleMailTemplateID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txteMailTemplateID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpeMailTemplateID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatoreMailTemplateID" runat="server" ErrorMessage="Email Template Required." ControlToValidate="drpeMailTemplateID" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                            <asp:Label ID="lblemailErrorMSG" CssClass="control-label" ForeColor="Red" Visible="false" runat="server" Text="Label"></asp:Label>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lbleMailTemplateID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txteMailTemplateID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                      <div class="col-md-7">
                                                                          <asp:TextBox ID="txtemailtemplate" class="form-control" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <asp:LinkButton ID="LinkemailTemplate" runat="server" OnClick="LinkemailTemplate_Click"><i class="icon-plus " style="color:black;padding-left: 4px;"></i></asp:LinkButton>
                                                                        </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                <div class="tabbable">
                                                                  <table class="table table-striped table-bordered table-hover">
                                                                        <thead>
                                                                              <tr>
                                                                                   <th>Email Template</th>
                                                                                   <th>Email Template Id</th>
                                                                                   <th>Remark</th>
                                                                                </tr>
                                                                         </thead>
                                                                         <tbody>
                                                                                 <asp:ListView ID="ListEmailTempate" runat="server">
                                                                                      <LayoutTemplate>
                                                                                           <tr id="ItemPlaceholder" runat="server">
                                                                                           </tr>
                                                                                       </LayoutTemplate>
                                                                                       <ItemTemplate>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# getsTemplate(Convert .ToInt32 ( Eval("Recource")))%>'></asp:Label></td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("RecValue")%>'></asp:Label></td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblEMAIL" runat="server" Text='<%# getTemplateremark(Convert .ToInt32 ( Eval("Recource")))%>'></asp:Label></td>


                                                                                                    </tr>
                                                                                        </ItemTemplate>
                                                                                   </asp:ListView>

                                                                          </tbody>
                                                                   </table>

                                                             </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                            <div class="row">                                                           
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblContractID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtContractID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                            <asp:DropDownList ID="drpContractID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorContractID" runat="server" ErrorMessage="Contract Name Required." ControlToValidate="drpContractID" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblContractID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtContractID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>                                                            
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblTitle2Print1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTitle2Print1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtTitle2Print" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTitle2Print" runat="server" ControlToValidate="txtTitle2Print" ErrorMessage="Title 2 print Required." CssClass="Validation" ValidationGroup="submit123"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblTitle2Print2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTitle2Print2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <%--</div>
                                                            <div class="row">--%>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblDMSid1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDMSid1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                           <%-- <asp:DropDownList ID="drpDMSid" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorDMSid" runat="server" ErrorMessage="Dms ID Required." ControlToValidate="drpDMSid" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                                             <asp:TextBox ID="txtDMSid" runat="server" Text="1" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblDMSid2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDMSid2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit123" Text="Add New" OnClick="btnAdd_Click" />
                                                                <asp:Button ID="btnActivate" Enabled="false" runat="server" class="btn green-haze btn-default" Text="Activate" OnClick="btnActivate_Click"/>
                                                                <asp:Label runat="server" ForeColor="Green" ID="lblActivatedStatusDate" Text=" "></asp:Label> 
                                                                 <asp:Button ID="btnRegister" Enabled="false" runat="server" class="btn green-haze btn-default" Text="Register" OnClick="btnRegister_Click"/>
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

                           <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>--%>
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
                                                                                            <%--<select name="sample_1_length" aria-controls="sample_1"  tabindex="-1" title="">
                                                                                            <option value="5">5</option>
                                                                                            <option value="15">15</option>
                                                                                            <option value="20">20</option>
                                                                                            <option value="-1">All</option>
                                                                                        </select>--%>
                                                                                                entries</label>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-5">
                                                                                     <div class="form-group" style="color: ">
                                                                                         <asp:Label runat="server" ID="lblEventID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEventID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                         <div class="col-md-8">
                                                                                             <asp:DropDownList ID="drpEventID" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpEventID_SelectedIndexChanged"></asp:DropDownList>
                                                                                             <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorEventID" runat="server" ErrorMessage="Event name Required." ControlToValidate="drpEventID" ValidationGroup="submit123" InitialValue="0"></asp:RequiredFieldValidator>                                                                                              
                                                                                        </div>
                                                                                         <asp:Label runat="server" ID="lblEventID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEventID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                     </div>
                                                                                </div>
                                                                                
                                                                                <div class="col-md-5 col-sm-12">
                                                                                    <div id="sample_1_filter" class="dataTables_filter">
                                                                                        <label>Search:<input type="search" class="form-control input-small input-inline" placeholder="" aria-controls="sample_1"></label>
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
                                                                                                        <asp:Label runat="server" ID="lblhEventID" Text="Event name"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhEventType" Text="Event Type"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhEventTopic" Text="Event Topic"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhTitle2Print" Text="Title 2 print"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhFromDate" Text="From Date"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhToDate" Text="To Date"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhDescription1" Text="Description 1"></asp:Label></th>

                                                                                                    <th >ACTION</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" OnItemDataBound="Listview1_ItemDataBound">
                                                                                                    <LayoutTemplate>
                                                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                                                        </tr>
                                                                                                    </LayoutTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>

                                                                                                            <td>
                                                                                                                <asp:Label ID="lblEventID" runat="server" Text='<%# Eval("EventID")%>'></asp:Label></td>
                                                                                                   <td>
                                                                                                                <asp:Label ID="lblEventType" runat="server" Text='<%# getEventType(Convert.ToInt32(Eval("EventType")))%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblEventTopic" runat="server" Text='<%# getEventTopic(Convert.ToInt32(Eval("EventTopic")))%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblTitle2Print" runat="server" Text='<%# Eval("Title2Print")%>'></asp:Label></td>
                                                                                                             <td>
                                                                                                                <asp:Label ID="lblFromDate" runat="server" Text='<%# Convert.ToDateTime(Eval("FromDate")).ToShortDateString()%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblToDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ToDate")).ToShortDateString()%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblDescription1" runat="server" Text='<%# Eval("Description1")%>'></asp:Label></td>

                                                                                                            <td>
                                                                                                                <table>
                                                                                                                    <tr>
                                                                                                                         <td><asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("EventID")+","+ Eval("MyID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                                                                       <asp:LinkButton ID="btnRegister" Visible="false"  PostBackUrl='<%# "Tbl_Event_Register.aspx?SEID="+ Eval("MyID") +"&MEID=" +Eval("EventID")%>' runat="server" class="btn btn-sm green filter-submit margin-bottom">Register</asp:LinkButton>
                                                                                                                       
                                                                                                                            <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("EventID")+","+ Eval("MyID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton>
                                                                                                                         <asp:Label ID="lblhideMyID" runat="server" Text='<%# Eval("MyID")%>' Visible="false"></asp:Label><asp:Label Visible="false" ID="lblhideEventID" runat="server" Text='<%# Eval("EventID")%>'></asp:Label></td>
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
                               <%-- </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnfirst1" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnNext1" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnPrevious1" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnLast1" EventName="Click" />
                                </Triggers>

                            </asp:UpdatePanel>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>

   <%-- <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text=" Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="assets/admin/layout4/img/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>


</asp:Content>
