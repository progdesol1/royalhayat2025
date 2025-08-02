<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="tbl_Employee.aspx.cs" Inherits="Web.Master.tbl_Employee" %>

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
                        <a href="#">Employee </a>
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
                                        <asp:Label runat="server" ID="lblHeader" Text="Employee"></asp:Label>
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
                                            <%-- <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />--%>
                                        </div>
                                        <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="Add New" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                        <asp:Button ID="btnEditLable" runat="server" class="btn green-haze btn-circle" OnClick="btnEditLable_Click" Style="display: none;" Text="Update Label" />
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


                                                        <%-- <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTenentID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenentID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtTenentID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTenentID" runat="server" ControlToValidate="txtTenentID" ErrorMessage="Tenent name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblTenentID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenentID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>
                                                        <%-- <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblLocationID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLocationID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drplocation" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true">
                                                                            <asp:ListItem Value="1" Text="Kuwait"></asp:ListItem>
                                                            <asp:ListItem Value="2 " Text="Lebanon"></asp:ListItem>--%>

                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorLocationID" runat="server" ControlToValidate="drplocation" ErrorMessage="Location name Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblLocationID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLocationID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>
                                                        <%--  <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemployeeID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemployeeID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemployeeID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremployeeID" runat="server" ControlToValidate="txtemployeeID" ErrorMessage="Employee name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemployeeID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemployeeID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>

                                                        <%-- </div>
                                                        </div>
                                                        --%>

                                                        <%-- <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblcontactID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtcontactID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtcontactID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorcontactID" runat="server" ControlToValidate="txtcontactID" ErrorMessage="Contact name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblcontactID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtcontactID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>

                                                        <div class="row">
                                                            <div class="col-md-12">

                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblfirstname1s" class="col-md-4 control-label" Text="First Name"></asp:Label><asp:TextBox runat="server" ID="txtfirstname1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtfirstname" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorfirstname" runat="server" ControlToValidate="txtfirstname" ErrorMessage="Firstname Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblfirstname2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtfirstname2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lbllastname1s" class="col-md-4 control-label" Text="Last Name"></asp:Label><asp:TextBox runat="server" ID="txtlastname1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtlastname" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorlastname" runat="server" ControlToValidate="txtlastname" ErrorMessage="Lastname Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lbllastname2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtlastname2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <%--<div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblActiveDirectoryID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActiveDirectoryID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtActiveDirectoryID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorActiveDirectoryID" runat="server" ControlToValidate="txtActiveDirectoryID" ErrorMessage="Activedirectory name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblActiveDirectoryID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActiveDirectoryID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <%-- <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblmiddle_name1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtmiddle_name1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtmiddle_name" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatormiddle_name" runat="server" ControlToValidate="txtmiddle_name" ErrorMessage="Middle name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblmiddle_name2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtmiddle_name2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>

                                                                <%-- <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_nick_name1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_nick_name1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_nick_name" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_nick_name" runat="server" ControlToValidate="txtemp_nick_name" ErrorMessage="Emp nick name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_nick_name2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_nick_name2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lbluserID1s" class="col-md-4 control-label" Text="Middle Name"></asp:Label><asp:TextBox runat="server" ID="txtuserID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtMiddlename" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatoruserID" runat="server" ControlToValidate="txtMiddlename" ErrorMessage="Middle Name Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lbluserID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtuserID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblemp_mobile1s" class="col-md-4 control-label" Text="Mobile"></asp:Label>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtemp_mobile" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_mobile" runat="server" ControlToValidate="txtemp_mobile" ErrorMessage="Emp mobile Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderMENU_ORDER" TargetControlID="txtemp_mobile"
                                                                            FilterType="Custom, numbers" runat="server" />
                                                                        <asp:Label runat="server" ID="lblemp_mobile2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_mobile2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblname21s" class="col-md-4 control-label" Text="ACM User"></asp:Label><asp:TextBox runat="server" ID="txtname21s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:DropDownList ID="drpvaliduser" runat="server" CssClass="form-control input-medium"></asp:DropDownList>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblname22h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtname22h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblMainHRRoleID1s" class="col-md-4 control-label" Text="IS Dept Super"></asp:Label><asp:TextBox runat="server" ID="txtMainHRRoleID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:CheckBox ID="ISSuper" runat="server" />
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblMainHRRoleID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMainHRRoleID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%-- <div class="row">
                                                            <div class="col-md-12">--%>

                                                        <%--<div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_smoker1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_smoker1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_smoker" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_smoker" runat="server" ControlToValidate="txtemp_smoker" ErrorMessage="Emp smoker Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_smoker2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_smoker2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>

                                                        <%--<div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblethnic_race_code1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtethnic_race_code1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtethnic_race_code" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorethnic_race_code" runat="server" ControlToValidate="txtethnic_race_code" ErrorMessage="Ethnic race code Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblethnic_race_code2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtethnic_race_code2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>
                                                        <%--<div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_birthday1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_birthday1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtemp_birthday" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxemp_birthday_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtemp_birthday" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_birthday" runat="server" ControlToValidate="txtemp_birthday" ErrorMessage="Emp birthday Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtemp_birthday" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblemp_birthday2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_birthday2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>

                                                        <%-- <div class="row">
                                                            <%--<div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblnation_code1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtnation_code1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtnation_code" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatornation_code" runat="server" ControlToValidate="txtnation_code" ErrorMessage="Nation code Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblnation_code2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtnation_code2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>
                                                        <%--<div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_gender1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_gender1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_gender" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_gender" runat="server" ControlToValidate="txtemp_gender" ErrorMessage="Emp gender Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_gender2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_gender2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>
                                                        <%--</div>--%>
                                                        <%-- <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_marital_status1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_marital_status1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_marital_status" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_marital_status" runat="server" ControlToValidate="txtemp_marital_status" ErrorMessage="Emp marital status Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_marital_status2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_marital_status2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_ssn_num1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_ssn_num1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_ssn_num" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_ssn_num" runat="server" ControlToValidate="txtemp_ssn_num" ErrorMessage="Emp ssn num Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_ssn_num2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_ssn_num2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                        <%--  <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_sin_num1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_sin_num1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_sin_num" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_sin_num" runat="server" ControlToValidate="txtemp_sin_num" ErrorMessage="Emp sin num Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_sin_num2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_sin_num2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_other_id1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_other_id1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_other_id" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_other_id" runat="server" ControlToValidate="txtemp_other_id" ErrorMessage="Emp other id Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_other_id2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_other_id2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                        <%--  <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_dri_lice_num1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_dri_lice_num1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_dri_lice_num" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_dri_lice_num" runat="server" ControlToValidate="txtemp_dri_lice_num" ErrorMessage="Emp dri lice num Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_dri_lice_num2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_dri_lice_num2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_dri_lice_exp_date1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_dri_lice_exp_date1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtemp_dri_lice_exp_date" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxemp_dri_lice_exp_date_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtemp_dri_lice_exp_date" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_dri_lice_exp_date" runat="server" ControlToValidate="txtemp_dri_lice_exp_date" ErrorMessage="Emp dri lice exp date Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtemp_dri_lice_exp_date" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblemp_dri_lice_exp_date2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_dri_lice_exp_date2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                        <%-- <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_military_service1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_military_service1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_military_service" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_military_service" runat="server" ControlToValidate="txtemp_military_service" ErrorMessage="Emp military service Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_military_service2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_military_service2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_status1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_status1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_status" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_status" runat="server" ControlToValidate="txtemp_status" ErrorMessage="Emp status Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_status2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_status2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                        <%--<div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblEmployeeType1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEmployeeType1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpEmployeeType" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorEmployeeType" runat="server" ErrorMessage="Employeetype Required." ControlToValidate="drpEmployeeType" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblEmployeeType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEmployeeType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lbljob_title_code1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtjob_title_code1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtjob_title_code" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorjob_title_code" runat="server" ControlToValidate="txtjob_title_code" ErrorMessage="Job title code Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lbljob_title_code2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtjob_title_code2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                        <%-- <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lbleeo_cat_code1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txteeo_cat_code1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txteeo_cat_code" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoreeo_cat_code" runat="server" ControlToValidate="txteeo_cat_code" ErrorMessage="Eeo cat code Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lbleeo_cat_code2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txteeo_cat_code2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblwork_station1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtwork_station1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtwork_station" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorwork_station" runat="server" ControlToValidate="txtwork_station" ErrorMessage="Work station Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblwork_station2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtwork_station2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                        <%-- <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_street11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_street11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_street1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_street1" runat="server" ControlToValidate="txtemp_street1" ErrorMessage="Emp street1 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_street12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_street12h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_street21s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_street21s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_street2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_street2" runat="server" ControlToValidate="txtemp_street2" ErrorMessage="Emp street2 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_street22h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_street22h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                        <%-- <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblcity_code1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtcity_code1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtcity_code" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorcity_code" runat="server" ControlToValidate="txtcity_code" ErrorMessage="City code Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblcity_code2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtcity_code2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblcoun_code1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtcoun_code1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtcoun_code" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorcoun_code" runat="server" ControlToValidate="txtcoun_code" ErrorMessage="Coun code Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblcoun_code2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtcoun_code2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                        <%--<div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblprovin_code1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtprovin_code1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtprovin_code" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorprovin_code" runat="server" ControlToValidate="txtprovin_code" ErrorMessage="Provin code Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblprovin_code2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtprovin_code2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_zipcode1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_zipcode1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_zipcode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_zipcode" runat="server" ControlToValidate="txtemp_zipcode" ErrorMessage="Emp zipcode Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_zipcode2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_zipcode2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>

                                                        <%--      <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_hm_telephone1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_hm_telephone1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_hm_telephone" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_hm_telephone" runat="server" ControlToValidate="txtemp_hm_telephone" ErrorMessage="Emp hm telephone Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_hm_telephone2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_hm_telephone2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>
                                                        <div class="row" style="display: none;">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblStuden_LoginID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStuden_LoginID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtStuden_LoginID" runat="server" CssClass="form-control"></asp:TextBox>

                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblStuden_LoginID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStuden_LoginID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblPASSWORD1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPASSWORD1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtPASSWORD" runat="server" CssClass="form-control"></asp:TextBox>

                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblPASSWORD2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPASSWORD2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <%--<div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDeviceID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeviceID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtDeviceID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDeviceID" runat="server" ControlToValidate="txtDeviceID" ErrorMessage="Device name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblDeviceID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeviceID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>
                                                            </div>
                                                        </div>


                                                        <%-- <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_work_telephone1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_work_telephone1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_work_telephone" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_work_telephone" runat="server" ControlToValidate="txtemp_work_telephone" ErrorMessage="Emp work telephone Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_work_telephone2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_work_telephone2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_work_email1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_work_email1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_work_email" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_work_email" runat="server" ControlToValidate="txtemp_work_email" ErrorMessage="Emp work email Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_work_email2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_work_email2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                        <%-- <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblsal_grd_code1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtsal_grd_code1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtsal_grd_code" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorsal_grd_code" runat="server" ControlToValidate="txtsal_grd_code" ErrorMessage="Sal grd code Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblsal_grd_code2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtsal_grd_code2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lbljoined_date1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtjoined_date1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtjoined_date" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxjoined_date_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtjoined_date" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorjoined_date" runat="server" ControlToValidate="txtjoined_date" ErrorMessage="Joined date Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtjoined_date" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lbljoined_date2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtjoined_date2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                        <%-- <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblemp_oth_email1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_oth_email1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtemp_oth_email" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatoremp_oth_email" runat="server" ControlToValidate="txtemp_oth_email" ErrorMessage="Emp oth email Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblemp_oth_email2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtemp_oth_email2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lbltermination_id1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttermination_id1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txttermination_id" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortermination_id" runat="server" ControlToValidate="txttermination_id" ErrorMessage="Termination id Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lbltermination_id2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txttermination_id2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                        </div>--%>
                                                        <%-- <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblActive1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="cbActive" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorActive" runat="server" ControlToValidate="cbActive" ErrorMessage="Active Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblActive2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDeleted1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeleted1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtDeleted" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDeleted" runat="server" ControlToValidate="txtDeleted" ErrorMessage="Deleted Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblDeleted2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeleted2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>
                                                        <%--<div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDateTime1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateTime1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtDateTime" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxDateTime_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDateTime" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDateTime" runat="server" ControlToValidate="txtDateTime" ErrorMessage="Datetime Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator></div>
                                                                    <asp:Label runat="server" ID="lblDateTime2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDateTime2h" class="col-md-4 control-label" Visible="false"></asp:TextBox></div>
                                                            </div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet box green-haze">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-globe"></i>
                                                <asp:Label runat="server" ID="Label5" Text="Employee"></asp:Label>
                                                List
                                            </div>
                                            <div class="tools">
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhemp_birthday" Text="First Name"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhnation_code" Text="Last Name"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhemp_mobile" Text="Middle Name"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label1" Text="Mobile"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhemp_work_telephone" Text="Complaint System User"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhemp_work_email" Text="Department supervisor"></asp:Label></th>

                                                        <th style="width: 60px;">ACTION</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="employeeID" DataKeyNames="employeeID">
                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblemp_birthday" runat="server" Text='<%# Eval("firstname")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblnation_code" runat="server" Text='<%# Eval("lastname")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblemp_mobile" runat="server" Text='<%# Eval("middle_name")%>'></asp:Label></td>
                                                                  <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("emp_mobile")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblemp_work_telephone" runat="server" Text='<%# GetSuper(Convert.ToInt32(Eval("userID"))) %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblemp_work_email" runat="server" Text='<%# getISSUP(Eval("MainHRRoleID").ToString()) %>'></asp:Label></td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("employeeID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("employeeID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>

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
                            </div>
                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <ContentTemplate>
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
                                                                                                entries</label>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-6 col-sm-12">
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
                                                                                                            <asp:Label runat="server" ID="lblhemp_birthday" Text="First Name"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhnation_code" Text="Last Name"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhemp_mobile" Text="Emp mobile"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhemp_work_telephone" Text="Studen_LoginID"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhemp_work_email" Text="PASSWORD"></asp:Label></th>

                                                                                                        <th style="width: 60px;">ACTION</th>
                                                                                                    </tr>
                                                                                                </thead>
                                                                                                <tbody>
                                                                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="employeeID" DataKeyNames="employeeID">
                                                                                                        <LayoutTemplate>
                                                                                                            <tr id="ItemPlaceholder" runat="server">
                                                                                                            </tr>
                                                                                                        </LayoutTemplate>
                                                                                                        <ItemTemplate>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblemp_birthday" runat="server" Text='<%# Eval("firstname")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblnation_code" runat="server" Text='<%# Eval("lastname")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblemp_mobile" runat="server" Text='<%# Eval("emp_mobile")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblemp_work_telephone" runat="server" Text='<%# Eval("Studen_LoginID")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblemp_work_email" runat="server" Text='<%# Eval("PASSWORD")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <table>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("employeeID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                            <td>
                                                                                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("employeeID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                                                         
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

                                                                                            <ul class="pagination">
                                                                                                <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_fist">
                                                                                                    
                                                                                                    <asp:LinkButton ID="btnfirst1" OnClick="btnfirst_Click" runat="server"> First</asp:LinkButton>
                                                                                                </li>
                                                                                                <li class="paginate_button first " aria-controls="sample_1" tabindex="0" id="sample_1_Next">
                                                                                                    
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

                                </asp:UpdatePanel>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
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
