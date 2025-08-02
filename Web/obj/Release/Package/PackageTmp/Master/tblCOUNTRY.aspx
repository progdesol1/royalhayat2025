<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="tblCOUNTRY.aspx.cs" Inherits="Web.Master.tblCOUNTRY" %>

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
                                        <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="s" Text="Add New" OnClick="btnAdd_Click" />
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
                                                        <asp:Panel runat="server" Visible="false">
                                                            <div class="row">
                                                                <div class="col-md-5">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblTenantID1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenantID1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtTenantID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblTenantID2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTenantID2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <%--</div>
                                                        <div class="row">--%>
                                                                <div class="col-md-5">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblCOUNTRYID1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOUNTRYID1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtCOUNTRYID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblCOUNTRYID2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOUNTRYID2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblREGION11s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtREGION11s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtREGION1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblREGION12h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtREGION12h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCOUNAME11s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOUNAME11s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCOUNAME1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCOUNAME1" runat="server" ControlToValidate="txtCOUNAME1" ErrorMessage="Couname 1 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCOUNAME12h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOUNAME12h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCOUNAME21s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOUNAME21s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCOUNAME2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCOUNAME2" runat="server" ControlToValidate="txtCOUNAME2" ErrorMessage="Couname 2 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCOUNAME22h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOUNAME22h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCOUNAME31s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOUNAME31s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCOUNAME3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCOUNAME3" runat="server" ControlToValidate="txtCOUNAME3" ErrorMessage="Couname 3 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCOUNAME32h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOUNAME32h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCAPITAL1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCAPITAL1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCAPITAL" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCAPITAL2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCAPITAL2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%-- </div>
                                                        <div class="row">--%>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblNATIONALITY11s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNATIONALITY11s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtNATIONALITY1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNATIONALITY1" runat="server" ControlToValidate="txtNATIONALITY1" ErrorMessage="Nationality 1 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblNATIONALITY12h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNATIONALITY12h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblNATIONALITY21s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNATIONALITY21s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtNATIONALITY2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNATIONALITY2" runat="server" ControlToValidate="txtNATIONALITY2" ErrorMessage="Nationality 2 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblNATIONALITY22h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNATIONALITY22h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblNATIONALITY31s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNATIONALITY31s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtNATIONALITY3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNATIONALITY3" runat="server" ControlToValidate="txtNATIONALITY3" ErrorMessage="Nationality 3 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblNATIONALITY32h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtNATIONALITY32h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCURRENCYNAME11s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCURRENCYNAME11s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCURRENCYNAME1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCURRENCYNAME12h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCURRENCYNAME12h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCURRENCYNAME21s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCURRENCYNAME21s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCURRENCYNAME2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCURRENCYNAME22h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCURRENCYNAME22h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCURRENCYNAME31s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCURRENCYNAME31s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCURRENCYNAME3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCURRENCYNAME32h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCURRENCYNAME32h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCURRENTCONVRATE1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCURRENTCONVRATE1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCURRENTCONVRATE" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtCURRENTCONVRATE" FilterMode="ValidChars" runat="server" ValidChars="0123456789."></cc1:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCURRENTCONVRATE2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCURRENTCONVRATE2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCURRENCYSHORTNAME11s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCURRENCYSHORTNAME11s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCURRENCYSHORTNAME1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCURRENCYSHORTNAME12h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCURRENCYSHORTNAME12h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCURRENCYSHORTNAME21s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCURRENCYSHORTNAME21s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCURRENCYSHORTNAME2" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCURRENCYSHORTNAME22h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCURRENCYSHORTNAME22h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCURRENCYSHORTNAME31s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCURRENCYSHORTNAME31s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCURRENCYSHORTNAME3" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCURRENCYSHORTNAME32h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCURRENCYSHORTNAME32h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCountryType1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCountryType1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <%--<asp:TextBox ID="txtCountryType" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                        <asp:DropDownList ID="drpCountryType" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCountryType2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCountryType2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCountryTSubType1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCountryTSubType1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCountryTSubType" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCountryTSubType2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCountryTSubType2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblSovereignty1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSovereignty1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtSovereignty" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblSovereignty2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSovereignty2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblISO4217CurCode1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtISO4217CurCode1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtISO4217CurCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblISO4217CurCode2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtISO4217CurCode2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblISO4217CurName1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtISO4217CurName1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtISO4217CurName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblISO4217CurName2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtISO4217CurName2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblITUTTelephoneCode1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtITUTTelephoneCode1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtITUTTelephoneCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblITUTTelephoneCode2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtITUTTelephoneCode2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblFaxLength1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtFaxLength1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtFaxLength" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtFaxLength" FilterMode="ValidChars" runat="server" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblFaxLength2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtFaxLength2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTelLength1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTelLength1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtTelLength" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtTelLength" FilterMode="ValidChars" runat="server" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTelLength2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTelLength2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%-- </div>
                                                        <div class="row">--%>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblISO3166_1_2LetterCode1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtISO3166_1_2LetterCode1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtISO3166_1_2LetterCode" runat="server" MaxLength="2" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblISO3166_1_2LetterCode2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtISO3166_1_2LetterCode2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblISO3166_1_3LetterCode1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtISO3166_1_3LetterCode1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtISO3166_1_3LetterCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblISO3166_1_3LetterCode2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtISO3166_1_3LetterCode2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblISO3166_1Number1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtISO3166_1Number1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtISO3166_1Number" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblISO3166_1Number2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtISO3166_1Number2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblIANACountryCodeTLD1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtIANACountryCodeTLD1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtIANACountryCodeTLD" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblIANACountryCodeTLD2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtIANACountryCodeTLD2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblActive1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:CheckBox ID="cbActive" runat="server" />
                                                                        <%--<asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorActive" runat="server" ErrorMessage="Active Required." ControlToValidate="drpActive" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator></div>--%>
                                                                        <asp:Label runat="server" ID="lblActive2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
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


                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
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
                                                                                                <%--<select name="sample_1_length" aria-controls="sample_1"  tabindex="-1" title="">
                                                                                            <option value="5">5</option>
                                                                                            <option value="15">15</option>
                                                                                            <option value="20">20</option>
                                                                                            <option value="-1">All</option>
                                                                                        </select>--%>
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
                                                                                                            <asp:Label runat="server" ID="lblhCOUNAME1" Text="Couname 1"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhCOUNAME2" Text="Couname 2"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhCOUNAME3" Text="Couname 3"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhNATIONALITY1" Text="Nationality 1"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhNATIONALITY2" Text="Nationality 2"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhNATIONALITY3" Text="Nationality 3"></asp:Label></th>

                                                                                                        <th style="width: 60px;">ACTION</th>
                                                                                                    </tr>
                                                                                                </thead>
                                                                                                <tbody>
                                                                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="TenentID,COUNTRYID,REGION1,COUNAME1,COUNAME2,COUNAME3,CAPITAL,NATIONALITY1,NATIONALITY2,NATIONALITY3,CURRENCYNAME1,CURRENCYNAME2,CURRENCYNAME3,CURRENTCONVRATE,CURRENCYSHORTNAME1,CURRENCYSHORTNAME2,CURRENCYSHORTNAME3,CountryType,CountryTSubType,Sovereignty,ISO4217CurCode,ISO4217CurName,ITUTTelephoneCode,FaxLength,TelLength,ISO3166_1_2LetterCode,ISO3166_1_3LetterCode,ISO3166_1Number,IANACountryCodeTLD,Active,CRUP_ID" DataKeyNames="TenentID,COUNTRYID,REGION1,COUNAME1,COUNAME2,COUNAME3,CAPITAL,NATIONALITY1,NATIONALITY2,NATIONALITY3,CURRENCYNAME1,CURRENCYNAME2,CURRENCYNAME3,CURRENTCONVRATE,CURRENCYSHORTNAME1,CURRENCYSHORTNAME2,CURRENCYSHORTNAME3,CountryType,CountryTSubType,Sovereignty,ISO4217CurCode,ISO4217CurName,ITUTTelephoneCode,FaxLength,TelLength,ISO3166_1_2LetterCode,ISO3166_1_3LetterCode,ISO3166_1Number,IANACountryCodeTLD,Active,CRUP_ID">
                                                                                                        <LayoutTemplate>
                                                                                                            <tr id="ItemPlaceholder" runat="server">
                                                                                                            </tr>
                                                                                                        </LayoutTemplate>
                                                                                                        <ItemTemplate>
                                                                                                            <tr>

                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblCOUNAME1" runat="server" Text='<%# Eval("COUNAME1")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblCOUNAME2" runat="server" Text='<%# Eval("COUNAME2")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblCOUNAME3" runat="server" Text='<%# Eval("COUNAME3")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblNATIONALITY1" runat="server" Text='<%# Eval("NATIONALITY1")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblNATIONALITY2" runat="server" Text='<%# Eval("NATIONALITY2")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lblNATIONALITY3" runat="server" Text='<%# Eval("NATIONALITY3")%>'></asp:Label></td>

                                                                                                                <td>
                                                                                                                    <table>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("COUNTRYID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                            <td>
                                                                                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("COUNTRYID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
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
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnfirst1" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnNext1" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnPrevious1" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnLast1" EventName="Click" />
                                    </Triggers>

                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="10"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divWaiting">
                <asp:Label ID="lblWait" runat="server"
                    Text=" Please wait... " />
                <asp:Image ID="imgWait" runat="server"
                    ImageAlign="Middle" ImageUrl="assets/admin/layout4/img/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>


</asp:Content>
