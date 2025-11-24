<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="QuestionMaster.aspx.cs" Inherits="Web.CRM.QuestionMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="assets/global/plugins/ion.rangeslider/css/ion.rangeSlider.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/ion.rangeslider/css/ion.rangeSlider.Metronic.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="b" runat="server">
        <div class="page-head">
           <%-- <ul class="breadcrumb">
                <li>
                    <a href="DashBoard.aspx">HOME </a>
                    <i class="fa fa-circle"></i>
                </li>
                <li>
                    <a href="#">Account</a>
                </li>
            </ul>--%>
            <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                <div class="alert alert-success alert-dismissable">
                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </asp:Panel>
        </div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet-body form">
                                <div class="portlet-body">
                                    <div class="form-wizard">
                                        <div class="portlet box red">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <i class="fa fa-gift"></i>Add Question
                                       
                                      <%--  <asp:Label runat="server" ID="lblHeader"></asp:Label>
                                        <asp:TextBox Style="color: #333333" ID="txtHeader" runat="server" Visible="false"></asp:TextBox>--%>
                                                </div>
                                                <div class="tools">
                                                    <a href="javascript:;" class="collapse"></a>
                                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                    <asp:LinkButton ID="btnPagereload" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                    <a href="javascript:;" class="remove"></a>
                                                </div>
                                                <div class="actions btn-set">
                                                    <div id="navigation" runat="server" class="btn-group btn-group-circle btn-group-solid">
                                                    </div>
                                                    <asp:Button ID="btnAdd" runat="server" class="btn btn-circle btn-primary" Text="AddNew" OnClick="btnAdd_Click1" />
                                                    <asp:Button ID="btnCancel" runat="server" class="btn btn-circle btn-default" Text="Cancel" OnClick="btnCancel_Click" />
                                                    <asp:Button ID="btnEditLable" runat="server" class="btn green-haze btn-circle" Text="Update Label" />
                                                    &nbsp;
                                        <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server">E&nbsp;<img src="/assets/global/img/flags/us.png" /></asp:LinkButton>
                                                    <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server">A&nbsp;<img src="/assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                    <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server">F&nbsp;<img src="/assets/global/img/flags/fr.png" /></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="portlet-body" style="padding-top: 0px; padding-bottom: 0px;">
                                                <div class="tabbable">
                                                    <div class="tab-content no-space">
                                                        <div class="form-body">


                                                            <div class="form-group">
                                                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                                    <div class="alert alert-success">
                                                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                        <strong></strong>
                                                                        Question Save Succesfully..
                                                                    </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                                    <div class="alert alert-danger">
                                                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                        <strong></strong>Question Allready Exist...
                                                                    </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="Panel3" runat="server" Visible="false">
                                                                    <div class="alert alert-danger">
                                                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                        <strong></strong>After And Before Question Must Be Different...
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-2">
                                                                    <asp:Label runat="server" class="control-label col-md-4 getshow" ID="Label37"> No</asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtqutionnumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                </div>
                                                            <div class="form-group">
                                                                <div class="col-md-6">
                                                                    <label runat="server" id="Label2" class="control-label col-md-4 getshow">

                                                                        <asp:Label runat="server" ID="Label7">Question Lan#1</asp:Label><span class="required">* </span>
                                                                    </label>

                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtquelang1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtquelang1" ErrorMessage="Question Lan 1 Required." CssClass="Validation" ValidationGroup="suboption"></asp:RequiredFieldValidator>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtquelang1" ErrorMessage="Question Lan 1 Required." CssClass="Validation" ValidationGroup="subqution"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label runat="server" id="Label28" class="control-label col-md-4 getshow">
                                                                        <asp:Label runat="server" ID="Label29">Question Lan#2</asp:Label>
                                                                        <span class="required">* </span>
                                                                    </label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtquelang2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtquelang2" ErrorMessage="Question Lan 1 Required." CssClass="Validation" ValidationGroup="suboption"></asp:RequiredFieldValidator>
                                                                    </div>


                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-6">
                                                                    <label runat="server" id="lbl1" class="control-label col-md-4 getshow">
                                                                        <asp:Label runat="server" ID="lblName1s">Question in Lan#3</asp:Label>
                                                                        <span class="required">* </span>
                                                                    </label>

                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtquelang3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="txtquelang3" ErrorMessage="Question Lan 1 Required." CssClass="Validation" ValidationGroup="suboption"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label runat="server" id="Label32" class="control-label col-md-4 getshow">

                                                                        <asp:Label runat="server" ID="Label33">Total Weightage</asp:Label>
                                                                        <span class="required">* </span>
                                                                    </label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtWeitage" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtWeitage" ErrorMessage="Weitage Required." CssClass="Validation" ValidationGroup="suboption"></asp:RequiredFieldValidator>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtWeitage" ErrorMessage="Weitage Required." CssClass="Validation" ValidationGroup="subqution"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>


                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-6">

                                                                    <label runat="server" id="Label1" class="control-label col-md-4 getshow">

                                                                        <asp:Label runat="server" ID="Label13">Category</asp:Label><span class="required">* </span>
                                                                    </label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpCategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="drpCategory" InitialValue="0" ErrorMessage="Select Category Required." CssClass="Validation" ValidationGroup="suboption"></asp:RequiredFieldValidator>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="drpCategory" InitialValue="0" ErrorMessage="Select Category Required." CssClass="Validation" ValidationGroup="subqution"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>
                                                                <div class="col-md-2">
                                                                </div>
                                                                <div class="col-md-6">

                                                                    <label runat="server" id="Label5" class="control-label col-md-4 ">

                                                                        <asp:Label runat="server" ID="Label6">Number Questions</asp:Label><span class="required">* </span>
                                                                    </label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtChoice" placeholder="Number of Questions" runat="server" OnTextChanged="txtChoice_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtChoice" ErrorMessage="Name Required." CssClass="Validation" ValidationGroup="FinalSubmit"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderBudget" TargetControlID="txtChoice" FilterType="Custom, numbers" runat="server" />
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-6">

                                                                    <label runat="server" id="Label8" class="control-label col-md-4 getshow">

                                                                        <asp:Label runat="server" ID="Label9">Before Question</asp:Label><span class="required">* </span>
                                                                    </label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="DrpBeforeThisQuestion" runat="server" CssClass="form-control" OnSelectedIndexChanged="DrpBeforeThisQuestion_SelectedIndexChanged"></asp:DropDownList>
                                                                    </div>

                                                                </div>

                                                                <div class="col-md-6">

                                                                    <label runat="server" id="Label4" class="control-label col-md-4 getshow">

                                                                        <asp:Label runat="server" ID="Label10">After Question</asp:Label>
                                                                        <span class="required">* </span>
                                                                    </label>
                                                                    <asp:TextBox runat="server" ID="TextBox2" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="DrpAfterThisQuestion" CssClass="form-control" runat="server" OnSelectedIndexChanged="DrpAfterThisQuestion_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-6">

                                                                    <label runat="server" id="Label3" class="control-label col-md-4 getshow">

                                                                        <asp:Label runat="server" ID="Label12">Group</asp:Label>
                                                                        <span class="required">* </span>
                                                                    </label>
                                                                    <div class="col-md-8">
                                                                        <div class="input-group">
                                                                            <asp:DropDownList ID="DrpGroup" Style="width: 259px;" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                            <span class="input-group-btn">
                                                                                <%--<asp:Button ID="btnCustomerN1" class="btn blue" runat="server" Text="Check" OnClick="btnCustomerN1_Click" meta:resourcekey="btnCustomerN1Resource1" Style="padding-top: 7px; padding-bottom: 7px" />--%>
                                                                               
                                                                            </span>
                                                                            <asp:LinkButton ID="lkbCustomerN1" runat="server">
                                                                                 <i class="icon-arrow-right" style="color:black;padding-left: 4px;"></i>
                                                                            </asp:LinkButton>

                                                                        </div>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="DrpGroup" InitialValue="0" ErrorMessage="Select Group Required." CssClass="Validation" ValidationGroup="suboption"></asp:RequiredFieldValidator>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="DrpGroup" InitialValue="0" ErrorMessage="Select Group Required." CssClass="Validation" ValidationGroup="subqution"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <asp:Panel ID="PnelChoice" runat="server" Visible="false">
                                                                    <div class="col-md-6">

                                                                        <label runat="server" id="Label38" class="control-label col-md-4 getshow">
                                                                            <span class="required">* </span>
                                                                            <asp:Label runat="server" ID="Label39">Choice Type</asp:Label>
                                                                        </label>
                                                                        <div class="col-md-8">
                                                                            <asp:DropDownList ID="drpchoiceList" CssClass="form-control" runat="server"></asp:DropDownList>

                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="drpchoiceList" InitialValue="0" ErrorMessage="Select Choice Type  Required." CssClass="Validation" ValidationGroup="suboption"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                                <%-- <div class="col-md-6">

                                                                    <label runat="server" id="Label5" class="control-label col-md-4 getshow">

                                                                        <asp:Label runat="server" ID="Label6">Choice</asp:Label>
                                                                        <span class="required">* </span>
                                                                    </label>
                                                                   
                                                                </div>--%>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-md-12">

                                                                    <label runat="server" id="Label30" class="control-label col-md-2 getshow">

                                                                        <asp:Label runat="server" ID="Label31">Remark</asp:Label>
                                                                        <span class="required">* </span>
                                                                    </label>

                                                                    <div class="col-md-10">
                                                                        <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>

                                                                    </div>

                                                                </div>



                                                            </div>
                                                            <asp:Panel ID="Panel5" runat="server" Style="display: none">
                                                                <%--<div id="form_modal11" class="modal fade" role="dialog" aria-labelledby="myModalLabel10" aria-hidden="true">--%>
                                                                <div class="modal-dialog">
                                                                    <div class="modal-content">
                                                                        <div class="modal-header">
                                                                            <%--<asp:Button ID="Button1" class="close" runat="server" Text="Button" />--%>
                                                                            <h4 class="modal-title">Add Group </h4>
                                                                        </div>
                                                                        <div class="modal-body">
                                                                            <div class="form-horizontal" role="form">
                                                                                <div class="row">
                                                                                    <h4 style="padding-left: 25px;">
                                                                                        <asp:Label ID="Label41" runat="server" Text="Group Name"></asp:Label>

                                                                                        <asp:TextBox ID="txtgrupname" runat="server"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtgrupname" ErrorMessage="Enter The Group Name Required." CssClass="Validation" ValidationGroup="Btngrop"></asp:RequiredFieldValidator>
                                                                                    </h4>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="modal-footer">
                                                                            <asp:LinkButton ID="btnGrupSave" runat="server" class="btn default" ValidationGroup="Btngrop" OnClick="btnGrupSave_Click">Save</asp:LinkButton>
                                                                            <asp:LinkButton ID="LinkButton3" runat="server" class="btn default">Close</asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <%-- </div>--%>
                                                            </asp:Panel>
                                                            <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" DynamicServicePath=""
                                                                BackgroundCssClass="modalBackground" CancelControlID="LinkButton3" Enabled="True"
                                                                PopupControlID="Panel5" TargetControlID="lkbCustomerN1">
                                                            </cc1:ModalPopupExtender>
                                                            <asp:Panel ID="pnlNumberQ" runat="server" Visible="false">
                                                                <div class="row">
                                                                    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                        <ContentTemplate>--%>
                                                                    <div class="portlet box blue">
                                                                        <div class="portlet-title">
                                                                            <div class="caption">
                                                                                <i class="fa fa-globe"></i>Sub Question List
                                                                            </div>
                                                                            <div class="tools">
                                                                            </div>
                                                                            <div class="actions btn-set">
                                                                                <asp:Button ID="btnPreview" class="btn red-haze btn-circle" Visible="false" runat="server" Text="Preview" OnClick="btnPreview_Click" />
                                                                                <asp:Button ID="btnSubmit" class="btn green-haze btn-circle" runat="server" Text="Save" ValidationGroup="subqution" OnClick="btnSubmit_Click1" />
                                                                                <asp:Button ID="Button3" class="btn red-haze btn-circle" runat="server" Text="Edit" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="portlet-body">
                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th style="width: 54px;">Sr
                                                                                        </th>
                                                                                        <th>Label
                                                                                        </th>
                                                                                        <th style="width: 191px;">Question Type
                                                                                        </th>
                                                                                        <th style="width: 69px;">Total Ans
                                                                                        </th>
                                                                                        <th style="width: 67px;">Ans Req
                                                                                        </th>
                                                                                        <th style="width: 183px;">Answer
                                                                                            <br />
                                                                                            <asp:Label ID="lblOptionName" Style="font-size: 12px" runat="server" Text="(Comma Seperated)"></asp:Label>
                                                                                        </th>
                                                                                        <th style="width: 61px;">POS+</th>
                                                                                        <th style="width: 70px;">Neg-
                                                                                        </th>
                                                                                        <th></th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <%-- list 1 --%>
                                                                                    <asp:ListView ID="listsubqution" runat="server" OnItemDataBound="listsubqution_ItemDataBound">
                                                                                        <LayoutTemplate>
                                                                                            <tr id="ItemPlaceholder" runat="server">
                                                                                            </tr>
                                                                                        </LayoutTemplate>
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtser" CssClass="form-control" Text='<%# Container.DataItemIndex+1 %>' runat="server"></asp:TextBox>
                                                                                                    <%-- <asp:Label ID="lblTenet" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>--%></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtqutionname" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="drpQutintype" CssClass="form-control" runat="server"></asp:DropDownList></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtTotalAns" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtAnsReq" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtAnswer" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtPos" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtNeg" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                                                <td>
                                                                                                    <img src="images/deleteRec.png" /></td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:ListView>

                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlCommaAnswer" Visible="false" runat="server">
                                                                <div class="row">

                                                                    <div class="form-body">
                                                                        <div class="form-group">
                                                                            <label class="control-label col-md-3">Option</label>
                                                                            <div class="col-md-9">
                                                                                <div class="col-md-4">
                                                                                    <asp:ListBox ID="ListAllOption" runat="server" SelectionMode="Multiple" Width="200px" Height="200px"></asp:ListBox>
                                                                                </div>
                                                                                <div class="col-md-1" style="padding-left: 0px; margin-top: 31px;">
                                                                                    <asp:Button ID="btnbefore" runat="server" class="btn default btn-lg" Text=">>" OnClick="btnbefore_Click" />
                                                                                    <asp:Button ID="btnAfter" runat="server" class="btn default btn-lg" Text="<<" Style="padding-top: 10px; margin-top: 9px; margin-left: 1px;" OnClick="btnAfter_Click" />
                                                                                </div>
                                                                                Answer
                                                                                                <div class="col-md-4">

                                                                                                    <asp:ListBox ID="ListSelectOption" runat="server" SelectionMode="Multiple" Width="200px" Height="200px"></asp:ListBox>
                                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <br />
                                                            <asp:Panel ID="pnlMutipleyeshowmany" Visible="false" runat="server">
                                                                <div class="form-group form-md-line-input">
                                                                    <label class="col-md-2 control-label" for="form_control_1">Option</label>
                                                                    <div class="col-md-10">
                                                                        <div>
                                                                            <div>
                                                                                <label>
                                                                                    <asp:CheckBoxList ID="CheckBoxListMultiple" RepeatDirection="Horizontal" runat="server">
                                                                                    </asp:CheckBoxList>
                                                                                </label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>

                                                            <asp:Panel ID="pnlMutipleknowOne" Visible="false" runat="server">
                                                                <div class="form-group">
                                                                    <div class="col-md-12">

                                                                        <label runat="server" id="Label22" class="control-label col-md-2 getshow">
                                                                            <span class="required">* </span>
                                                                            <asp:Label runat="server" ID="Label23">Option</asp:Label>
                                                                        </label>
                                                                        <div class="col-md-10">
                                                                            <asp:RadioButtonList ID="RadioButtonListSingle" runat="server" RepeatDirection="Horizontal">
                                                                            </asp:RadioButtonList>

                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlSwappersingledobl" Visible="false" runat="server">
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label runat="server" id="Label24" class="control-label col-md-4 getshow">
                                                                                <span class="required">* </span>
                                                                                <asp:Label runat="server" ID="Label25"></asp:Label>
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:RadioButton ID="RadioYes" runat="server" GroupName="rad" />Yes
                                                                                        <asp:RadioButton ID="RadioNO" runat="server" GroupName="rad" />No
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <asp:Panel ID="pnlRankingMultiple" runat="server" Visible="false">
                                                                    <div class="form-group">
                                                                        <label class="col-md-3 control-label">Basic Slider</label>
                                                                        <div class="col-md-9">
                                                                            <asp:TextBox ID="range_4" runat="server"></asp:TextBox>

                                                                        </div>
                                                                    </div>

                                                                </asp:Panel>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlOption" Visible="false" runat="server">
                                                                <div class="form-group">
                                                                    <div class="col-md-12">

                                                                        <label runat="server" id="Label26" class="control-label col-md-2 getshow">

                                                                            <asp:Label runat="server" ID="Label27">Option</asp:Label><span class="required">* </span>
                                                                        </label>
                                                                        <div class="col-md-6">
                                                                            <asp:TextBox ID="txtOption" runat="server" CssClass="form-control"></asp:TextBox><asp:Label runat="server" ID="lbltext"></asp:Label>
                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorCustomerName" runat="server" ErrorMessage="Option Is Required" ControlToValidate="txtOption" ValidationGroup="MainOption"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:Button ID="btnAddMainoption" class="btn default" runat="server" Text="Save" ValidationGroup="MainOption" OnClick="btnAddMainoption_Click" />
                                                                            <asp:Button ID="btnCancelMainoption" class="btn blue" runat="server" Text="Cancel" OnClick="btnCancelMainoption_Click" />

                                                                        </div>
                                                                    </div>

                                                                </div>


                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlValueofdropdowncommaseperator" Visible="false" runat="server">
                                                                <div class="row">
                                                                    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                        <ContentTemplate>--%>
                                                                    <div class="portlet box blue">
                                                                        <div class="portlet-title">
                                                                            <div class="caption">
                                                                                <i class="fa fa-globe"></i>Option List
                                                                            </div>
                                                                            <div class="tools">
                                                                            </div>
                                                                            <div class="actions btn-set">
                                                                                <asp:Button ID="Button4" class="btn red-haze btn-circle" Visible="false" runat="server" Text="Preview" OnClick="Button4_Click" />
                                                                                <asp:Button ID="btnSubmitForSing" class="btn green-haze btn-circle" ValidationGroup="suboption" runat="server" Text="Save" OnClick="btnSubmitForSing_Click" />

                                                                            </div>
                                                                        </div>
                                                                        <div class="portlet-body">
                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>Sr No
                                                                                        </th>
                                                                                        <th>Option
                                                                                        </th>

                                                                                        <th class="hidden-xs">ACTION
                                                                                        </th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <%-- list 1 --%>
                                                                                    <asp:ListView ID="ListviewMainOption111" runat="server" OnItemCommand="ListviewMainOption_ItemCommand">
                                                                                        <LayoutTemplate>
                                                                                            <tr id="ItemPlaceholder" runat="server">
                                                                                            </tr>
                                                                                        </LayoutTemplate>
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblTenet" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label></td>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblOptionName" runat="server" Text='<%# Eval("OptionName")%>'></asp:Label>
                                                                                                    <asp:HiddenField ID="hidenopid" runat="server" Value='<%# Eval("ID")%>' />
                                                                                                </td>

                                                                                                <td style="align-items: center">
                                                                                                    <asp:LinkButton ID="lnkMainedit" CommandName="btnMainEdit" CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                                                                    <asp:LinkButton ID="btnMainDelete" CommandName="btnMainDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton>
                                                                                                </td>

                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:ListView>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnloverall" runat="server" Visible="false">
                                                                <asp:Panel ID="pnladdsuboption" Visible="false" runat="server">
                                                                    <div class="form-group">
                                                                        <div class="col-md-9">

                                                                            <label runat="server" id="Label34" class="control-label col-md-5 getshow">

                                                                                <asp:Label runat="server" ID="Label35">Sub Option(Add with Comma Seprator)</asp:Label>
                                                                                <span class="required">* </span>
                                                                            </label>
                                                                            <div class="col-md-6" style="left: -9px;">
                                                                                <asp:TextBox ID="txtSubOption1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtSubOption1" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" ValidChars=",;- "></cc1:FilteredTextBoxExtender>
                                                                            </div>

                                                                        </div>

                                                                    </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlanswer" runat="server" Visible="false">
                                                                    <div class="row">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label runat="server" id="Label14" class="control-label col-md-4 getshow">
                                                                                    <span class="required">* </span>
                                                                                    <asp:Label runat="server" ID="Label15">Choice Type</asp:Label>
                                                                                </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:DropDownList ID="DrpChoiceType" CssClass="form-control" runat="server" OnSelectedIndexChanged="DrpChoiceType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </asp:Panel>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <asp:Button ID="btnAnswer" runat="server" class="btn default btn-lg" Text="Answer" OnClick="btnAnswer_Click" />
                                                                    </div>
                                                                </div>

                                                                <asp:Panel ID="pnlMultiple" runat="server" Visible="false">
                                                                    <div class="form-group">
                                                                        <div class="col-md-5">

                                                                            <label runat="server" id="Label11" class="control-label col-md-4 getshow">
                                                                                <span class="required">* </span>
                                                                                <asp:Label runat="server" ID="Label16">Main Option</asp:Label>
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="DrpMainOption" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Option Is Required" InitialValue="0" ControlToValidate="DrpMainOption" ValidationGroup="SubOption"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-5">

                                                                            <label runat="server" id="Label17" class="control-label col-md-4 getshow">
                                                                                <span class="required">* </span>
                                                                                <asp:Label runat="server" ID="Label18">Sub Option</asp:Label>
                                                                            </label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtSubOption" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Sub Option Is Required" ControlToValidate="txtSubOption" ValidationGroup="SubOption"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col=md-2">
                                                                            <asp:Button ID="btnAddsuboption" class="btn default" runat="server" Text="Add" OnClick="btnAddsuboption_Click" ValidationGroup="SubOption" />
                                                                            <asp:Button ID="btnCancelsubOption" class="btn blue" runat="server" Text="Cancel" OnClick="btnCancelsubOption_Click" />
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <ContentTemplate>--%>
                                                                        <div class="portlet box blue">
                                                                            <div class="portlet-title">
                                                                                <div class="caption">
                                                                                    <i class="fa fa-globe"></i>Sub Option List
                                                                                </div>
                                                                                <div class="tools">
                                                                                </div>
                                                                            </div>
                                                                            <div class="portlet-body">
                                                                                <table class="table table-striped table-bordered table-hover">
                                                                                    <thead>
                                                                                        <tr>
                                                                                            <th>Sr No
                                                                                            </th>
                                                                                            <th>Main Option
                                                                                            </th>
                                                                                            <th>Sub Option
                                                                                            </th>
                                                                                            <th class="hidden-xs">ACTION
                                                                                            </th>

                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <%-- list 2 --%>
                                                                                        <asp:ListView ID="ListviewSubOption" runat="server" OnItemCommand="ListviewSubOption_ItemCommand">
                                                                                            <LayoutTemplate>
                                                                                                <tr id="ItemPlaceholder" runat="server">
                                                                                                </tr>
                                                                                            </LayoutTemplate>
                                                                                            <ItemTemplate>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblTenet" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblsuboptionName" runat="server" Text='<%# GetMainOptionName(Convert.ToInt32(Eval("OptionMasterID").ToString()))%>'></asp:Label></td>
                                                                                                    <asp:HiddenField ID="mainoptionid" runat="server" Value='<%# Eval("OptionMasterID")%>' />
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblsuboptionname1" runat="server" Text='<%# Eval("RowName")%>'></asp:Label></td>
                                                                                                    <td>
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:LinkButton ID="lnkSubedit" CommandName="btnSubEdit" CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-pencil"></i></asp:LinkButton>

                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:LinkButton ID="btnSubDelete" CommandName="btnSubDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton>

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
                                                                        </div>


                                                                        <%--</ContentTemplate>
                                                                    </asp:UpdatePanel>--%>
                                                                    </div>








                                                                </asp:Panel>



                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <asp:Button ID="btnFinalSubmit" runat="server" class="btn default btn-lg disabled" Text="Submit" ValidationGroup="FinalSubmit" OnClick="btnFinalSubmit_Click" />
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div style="float: right">
                                                                            <asp:Button ID="btnAnswerPreview" class="btn default btn-lg disabled" runat="server" Text="Question Preview" OnClick="btnAnswerPreview_Click" />
                                                                        </div>
                                                                    </div>
                                                                </div>




                                                            </asp:Panel>
                                                            <asp:HiddenField ID="hiddpreview" runat="server" />
                                                            <asp:Panel ID="pnlticket" runat="server" Style="display: none">
                                                                <%--<div id="form_modal11" class="modal fade" role="dialog" aria-labelledby="myModalLabel10" aria-hidden="true">--%>
                                                                <div class="modal-dialog">
                                                                    <div class="modal-content">
                                                                        <div class="modal-header">
                                                                            <%--<asp:Button ID="Button1" class="close" runat="server" Text="Button" />--%>
                                                                            <h4 class="modal-title">Question Preview</h4>
                                                                        </div>
                                                                        <div class="modal-body">
                                                                            <div class="form-horizontal" role="form">
                                                                                <div class="row">
                                                                                </div>

                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="search-classic">
                                                                                            <h4>
                                                                                                <asp:Label ID="lblshowQuestion" runat="server"></asp:Label>
                                                                                                <br />
                                                                                                <br />

                                                                                                <asp:Panel ID="pnlquePreviewMultiple" runat="server" Visible="false">
                                                                                                    <asp:CheckBoxList ID="chmultipletpreview" runat="server" RepeatDirection="Vertical" Style="margin-right: 10px;">
                                                                                                    </asp:CheckBoxList>
                                                                                                </asp:Panel>
                                                                                                <asp:Panel ID="pnlsingleanspreview" runat="server" Visible="false" Style="margin-left: 100px;">
                                                                                                    <label style="padding-bottom: 10px; padding-top: 10px;">
                                                                                                        <asp:RadioButtonList ID="RadiosinPreview" runat="server" RepeatDirection="Vertical">
                                                                                                        </asp:RadioButtonList></label>
                                                                                                </asp:Panel>
                                                                                                <asp:Panel ID="pnlcommasepanspreview" runat="server" Visible="false">
                                                                                                    <div class="form-group">
                                                                                                        <label class="control-label col-md-3">Option</label>
                                                                                                        <div class="col-md-9">
                                                                                                            <div class="col-md-4">
                                                                                                                <asp:ListBox ID="ListBoxcommasepAnswer" runat="server" SelectionMode="Multiple" Width="200px" Height="200px"></asp:ListBox>
                                                                                                            </div>
                                                                                                            <div class="col-md-1" style="padding-left: 0px; margin-top: 31px;">
                                                                                                                <asp:Button ID="Button1" runat="server" class="btn default btn-lg" Text=">>" OnClick="btnbefore_Click" />
                                                                                                                <asp:Button ID="Button2" runat="server" class="btn default btn-lg" Text="<<" Style="padding-top: 10px; margin-top: 9px; margin-left: 1px;" OnClick="btnAfter_Click" />
                                                                                                            </div>
                                                                                                            Answer
                                                                                                <div class="col-md-4">
                                                                                                    <asp:ListBox ID="ListBoxSelecropt" runat="server" SelectionMode="Multiple" Width="200px" Height="200px"></asp:ListBox>
                                                                                                </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </asp:Panel>
                                                                                                <asp:Panel ID="pnlanspreviewswaper" runat="server" Visible="false">
                                                                                                    <label>
                                                                                                        <asp:RadioButton ID="RADIOYES1" runat="server" GroupName="rad1" Checked="true" />Yes
                                                                                                                  <asp:RadioButton ID="RADIONO1" runat="server" GroupName="rad1" />No</label>
                                                                                                </asp:Panel>
                                                                                                <asp:Panel ID="pneltxtbox" runat="server" Visible="false">
                                                                                                    <label>
                                                                                                        <asp:TextBox ID="txttag4" runat="server"></asp:TextBox></label>
                                                                                                </asp:Panel>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                            </h4>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                        <div class="modal-footer">
                                                                            <asp:LinkButton ID="LinkButton2" runat="server" class="btn default">Close</asp:LinkButton>


                                                                            <%--<asp:Button ID="btnTicketSave" runat="server" class="btn green" Text="Save" OnClick="btnTicketSave_Click" ValidationGroup="btnsubmitTicket" />--%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <%-- </div>--%>
                                                            </asp:Panel>
                                                            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" DynamicServicePath=""
                                                                BackgroundCssClass="modalBackground" CancelControlID="LinkButton2" Enabled="True"
                                                                PopupControlID="pnlticket" TargetControlID="hiddpreview">
                                                            </cc1:ModalPopupExtender>
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                            <asp:Panel ID="Panel4" runat="server" Style="display: none">
                                                                <%--<div id="form_modal11" class="modal fade" role="dialog" aria-labelledby="myModalLabel10" aria-hidden="true">--%>
                                                                <div class="modal-dialog">
                                                                    <div class="modal-content">
                                                                        <div class="modal-header">
                                                                            <%--<asp:Button ID="Button1" class="close" runat="server" Text="Button" />--%>
                                                                            <h4 class="modal-title">Question Preview</h4>
                                                                        </div>
                                                                        <div class="modal-body">
                                                                            <div class="form-horizontal" role="form">
                                                                                <div class="row">
                                                                                </div>

                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="search-classic">
                                                                                            <h4>
                                                                                                <asp:Label ID="Label40" runat="server"></asp:Label>
                                                                                                <br />
                                                                                                <br />
                                                                                                <asp:Repeater ID="repAccordian" runat="server">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ser" Style="padding-left: 15px;" runat="server" Text='<%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>'></asp:Label>
                                                                                                        <asp:Label ID="Label3" Style="padding-left: 15px;" runat="server" Text=' <%#Eval("QuestionLang1") %>'></asp:Label>
                                                                                                        <asp:Label ID="lblMultipleNote" runat="server"></asp:Label><asp:Label ID="lblNoteSingle" runat="server"></asp:Label>
                                                                                                        <asp:Label ID="lbl_QUESTION_id" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                                                                        <asp:Label ID="lblChoiceType" runat="server" Text='<%# Eval("ChoiseType") %>' Visible="false"></asp:Label>
                                                                                                        <asp:HiddenField ID="hiddenqueby" Value='<%# Eval("CreateBy") %>' runat="server" />
                                                                                                        <br />
                                                                                                        <br />
                                                                                                        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                                                                                                            <ItemTemplate>
                                                                                                                <label style="margin-left: 100px; padding-bottom: 10px; margin-bottom: 20px;">
                                                                                                                    <asp:CheckBoxList ID="chmultipletpreview" runat="server" RepeatDirection="Vertical">
                                                                                                                    </asp:CheckBoxList></label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:Repeater>
                                                                                                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1ItemEventHandler">

                                                                                                            <ItemTemplate>

                                                                                                                <label style="margin-left: 100px; padding-bottom: 10px; margin-bottom: 20px;">
                                                                                                                    <asp:RadioButtonList ID="QuestionsList" RepeatDirection="Vertical" runat="server"></asp:RadioButtonList></label>

                                                                                                            </ItemTemplate>
                                                                                                        </asp:Repeater>
                                                                                                        <asp:Repeater ID="Repeater4" runat="server" OnItemDataBound="Repeater4_ItemDataBound">
                                                                                                            <ItemTemplate>

                                                                                                                <label class="control-label col-md-1">Option</label>

                                                                                                                <div class="col-md-4">
                                                                                                                    <asp:ListBox ID="ListAllOption11" runat="server" SelectionMode="Multiple" Width="200px" Height="200px"></asp:ListBox>
                                                                                                                </div>
                                                                                                                <div class="col-md-1" style="padding-left: 0px; margin-top: 31px;">
                                                                                                                    <asp:Button ID="btnbefore" runat="server" class="btn default btn-lg" Text=">>" />
                                                                                                                    <asp:Button ID="btnAfter" runat="server" class="btn default btn-lg" Text="<<" Style="padding-top: 10px; margin-top: 9px; margin-left: 1px;" />
                                                                                                                </div>
                                                                                                                <label class="control-label col-md-1">Answer</label>
                                                                                                                <div class="col-md-4">
                                                                                                                    <asp:ListBox ID="ListSelectOption11" runat="server" SelectionMode="Multiple" Width="200px" Height="200px"></asp:ListBox>
                                                                                                                </div>

                                                                                                                <br />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:Repeater>
                                                                                                        <asp:Repeater ID="Repeater3" runat="server">
                                                                                                            <ItemTemplate>
                                                                                                                <label style="margin-left: 100px; padding-bottom: 10px;">
                                                                                                                    <asp:RadioButton ID="radioswapYes" runat="server" />Yes
                                                                                                                                        <asp:RadioButton ID="radioswapNo" runat="server" />No</label>
                                                                                                                <br />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:Repeater>
                                                                                                        <asp:Repeater ID="Repeater5" runat="server">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:TextBox ID="range_4" runat="server" Style="margin-left: 100px; padding-bottom: 10px; margin-bottom: 10px;"></asp:TextBox>
                                                                                                                <br />

                                                                                                            </ItemTemplate>
                                                                                                        </asp:Repeater>
                                                                                                    </ItemTemplate>
                                                                                                </asp:Repeater>
                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                                <h4></h4>

                                                                                            </h4>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                        <div class="modal-footer">
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" class="btn default">Close</asp:LinkButton>


                                                                            <%--<asp:Button ID="btnTicketSave" runat="server" class="btn green" Text="Save" OnClick="btnTicketSave_Click" ValidationGroup="btnsubmitTicket" />--%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <%-- </div>--%>
                                                            </asp:Panel>
                                                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
                                                                BackgroundCssClass="modalBackground" CancelControlID="LinkButton1" Enabled="True"
                                                                PopupControlID="Panel4" TargetControlID="HiddenField1">
                                                            </cc1:ModalPopupExtender>



                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="tab-pane fade" id="tab_2_2" runat="server" visible="false">
                                                    <p>
                                                        2
                                                                        Food truck fixie locavore, accusamus mcsweeney's marfa nulla single-origin coffee squid. Exercitation +1 labore velit, blog sartorial PBR leggings next level wes anderson artisan four loko farm-to-table craft beer twee. Qui photo booth letterpress, commodo enim craft beer mlkshk aliquip jean shorts ullamco ad vinyl cillum PBR. Homo nostrud organic, assumenda labore aesthetic magna delectus mollit. Keytar helvetica VHS salvia yr, vero magna velit sapiente labore stumptown. Vegan fanny pack odio cillum wes anderson 8-bit, sustainable jean shorts beard ut DIY ethical culpa terry richardson biodiesel. Art party scenester stumptown, tumblr butcher vero sint qui sapiente accusamus tattooed echo park.
                                                    </p>
                                                </div>
                                                <div class="tab-pane fade" id="tab_2_3" runat="server" visible="false">
                                                    <p>
                                                        3
                                                                        Etsy mixtape wayfarers, ethical wes anderson tofu before they sold out mcsweeney's organic lomo retro fanny pack lo-fi farm-to-table readymade. Messenger bag gentrify pitchfork tattooed craft beer, iphone skateboard locavore carles etsy salvia banksy hoodie helvetica. DIY synth PBR banksy irony. Leggings gentrify squid 8-bit cred pitchfork. Williamsburg banh mi whatever gluten-free, carles pitchfork biodiesel fixie etsy retro mlkshk vice blog. Scenester cred you probably haven't heard of them, vinyl craft beer blog stumptown. Pitchfork sustainable tofu synth chambray yr.
                                                    </p>
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

                    <div class="portlet box blue">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-globe"></i>Question Answer List...
                            </div>
                            <div class="tools">
                            </div>
                        </div>
                        <div class="portlet-body">
                            <table class="table table-striped table-bordered table-hover" id="sample_1">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <%--<th>Q_No
                                </th>--%>
                                        <th>Question
                                        </th>
                                        <th>Weitage 
                                        </th>
                                        <th>Category 
                                        </th>
                                        <th>Choice Type 
                                        </th>
                                        <th>Group 
                                        </th>
                                        <th class="hidden-xs">ACTION
                                        </th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <%-- list 3 --%>
                                    <asp:ListView ID="listqution" runat="server" OnItemCommand="Listview2QuestionAnswer_ItemCommand" DataKey="ID">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label19" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblquestion" runat="server" Text='<%# Eval("QuestionLang1")%>'></asp:Label>
                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label20" runat="server" Text='<%# Eval("Weitage")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label21" runat="server" Text='<%#getcetegoryname(Convert.ToInt32( Eval("CategoryID")))%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label36" runat="server" Text='<%#getchoisetype(Convert.ToInt32( Eval("ChoiseType")))%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblAnswer" runat="server" Text='<%#getgrupname(Convert.ToInt32( Eval("GroupID")))%>'></asp:Label></td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="lnksstview" CommandName="btnqstView" class="btn btn-sm red filter-cancel" CommandArgument='<%# Eval("ID") %>' runat="server"><i class="fa fa-eye"></i></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="btnEdit" CommandName="btnQutionEdit" class="btn btn-sm red filter-cancel" CommandArgument='<%# Eval("ID") %>' runat="server"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="btnDelet" CommandName="btnQutionDelet" class="btn btn-sm red filter-cancel" CommandArgument='<%# Eval("ID") %>' runat="server"><i class="fa fa-times"></i></asp:LinkButton>
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
                    </div>
                </div>
            </ContentTemplate>
            <Triggers >
                <asp:PostBackTrigger ControlID="btnAdd" />
                 <asp:PostBackTrigger ControlID="btnCancel" />
                 <asp:PostBackTrigger ControlID="btnSubmit" />
                 <asp:PostBackTrigger ControlID="btnSubmitForSing" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <script src="assets/admin/pages/scripts/components-ion-sliders.js"></script>
    <script>
        jQuery(document).ready(function () {

            ComponentsNoUiSliders.init();
        });
    </script>
</asp:Content>
