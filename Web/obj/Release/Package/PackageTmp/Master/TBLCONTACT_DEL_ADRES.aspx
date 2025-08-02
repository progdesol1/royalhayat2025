<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="TBLCONTACT_DEL_ADRES.aspx.cs" Inherits="Web.Master.TBLCONTACT_DEL_ADRES" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .input-controls {
            margin-top: 10px;
            border: 1px solid transparent;
            border-radius: 2px 0 0 2px;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            height: 32px;
            outline: none;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
        }

        #ContentPlaceHolder1_searchInput {
            background-color: #fff;
            font-family: Roboto;
            font-size: 15px;
            font-weight: 300;
            margin-left: 12px;
            padding: 0 11px 0 13px;
            text-overflow: ellipsis;
            width: 50%;
        }

            #ContentPlaceHolder1_searchInput:focus {
                border-color: #4d90fe;
            }
    </style>

    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }
    </style>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDd-0GRWdGVwGD-nc5Jojdnr6aQO46RV_4&sensor=false&libraries=places"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <%--  <ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">CONTACT_DEL_ADRES </a>
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
                                           <%-- <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />--%>
                                        </div>
                                        <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="Add New" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                        <asp:Button ID="btnEditLable" runat="server" class="btn green-haze btn-circle" OnClick="btnEditLable_Click" Text="Update Label" />
                                        <asp:Button ID="Button1" runat="server" class="btn yellow-gold dz-square" Text="BACK CONTACT" OnClick="Button1_Click" />
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
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>


                                                                <div class="row">
                                                                    <%-- <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblContactID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtContactID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpContactID" runat="server" CssClass="table-group-action-input form-control input-medium select2me"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblContactID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtContactID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>--%>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblContactMyID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtContactMyID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                <asp:DropDownList ID="drpContactMyID" runat="server" CssClass="form-control input-medium select2me" AutoPostBack="true" OnSelectedIndexChanged="drpContactMyID_SelectedIndexChanged"></asp:DropDownList>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblContactMyID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtContactMyID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblCOUNTRYID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOUNTRYID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                <asp:DropDownList ID="drpCOUNTRYID" runat="server" CssClass="form-control input-medium select2me" AutoPostBack="true" OnSelectedIndexChanged="drpCOUNTRYID_SelectedIndexChanged"></asp:DropDownList>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblCOUNTRYID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCOUNTRYID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <%-- <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDeliveryAdressID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeliveryAdressID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpDeliveryAdressID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDeliveryAdressID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDeliveryAdressID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblAdressName11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAdressName11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtAdressName1" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAdressName1" ErrorMessage="Address Name Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblAdressName12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAdressName12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <%-- </div>
                                                        <div class="row">--%>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblSTATE1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSTATE1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="drpSTATE" runat="server" CssClass="table-group-action-input form-control input-medium select2me" AutoPostBack="true" OnSelectedIndexChanged="drpSTATE_SelectedIndexChanged"></asp:DropDownList>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblSTATE2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSTATE2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <asp:Label runat="server" ID="lblBlock" ForeColor="Green" Style="font-size: 15px;" class="col-md-4 control-label" Text="Block"></asp:Label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtBlock" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <%--</div>
                                                        <div class="row">--%>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblCITY1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCITY1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="drpCity" runat="server" CssClass="table-group-action-input form-control input-medium select2me" AutoPostBack="true" OnSelectedIndexChanged="drpCity_SelectedIndexChanged"></asp:DropDownList>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblCITY2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCITY2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblADDR11s" ForeColor="Green" Style="font-size: 15px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtADDR11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtADDR1" Enabled="true" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtADDR1" ErrorMessage="Address 1 Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblADDR12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtADDR12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <%-- </div>
                                                        <div class="row">--%>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblADDR21s" ForeColor="Green" Style="font-size: 15px;" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtADDR21s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtADDR2" MaxLength="200" Enabled="false" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblADDR22h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtADDR22h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="lblBuilding" MaxLength="150" ForeColor="Green" Style="font-size: 15px;" class="col-md-4 control-label" Text="Building"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtBuilding" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="lblStreet" MaxLength="150" ForeColor="Green" Style="font-size: 15px;" class="col-md-4 control-label" Text="Street"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtstreet" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="lblforflat" MaxLength="50" ForeColor="Green" Style="font-size: 15px;" class="col-md-4 control-label" Text="Flat"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtFlat" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="lblLane" MaxLength="25" ForeColor="Green" Style="font-size: 15px;" class="col-md-4 control-label" Text="Floor/Lane"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtlane" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row">

                                                            <%-- </div>
                                                        <div class="row">--%>
                                                            <div class="col-md-12">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblREMARKS1s" CssClass="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtREMARKS1s" CssClass="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-10">
                                                                        <asp:TextBox ID="txtREMARKS" MaxLength="255" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblREMARKS2h" CssClass="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtREMARKS2h" CssClass="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%-- <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblAdressShortName11s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAdressShortName11s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtAdressShortName1" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblAdressShortName12h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtAdressShortName12h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>                                                           
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label ID="lblinfoWindow" runat="server" class="col-md-4 control-label" Text="infoWindow"></asp:Label>
                                                                    <asp:Label runat="server" ID="lblinfoWindow1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtinfoWindow1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtinfoWindow" runat="server" CssClass="form-control"></asp:TextBox>                                                                        
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblinfoWindow2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtinfoWindow2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" Enabled="true" ID="lblLongitute1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLongitute1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtLongitute" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorLongitute" runat="server" ControlToValidate="txtLongitute" ErrorMessage="Longitute Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblLongitute2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLongitute2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--</div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" Enabled="true" ID="lblLatitute1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLatitute1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtLatitute" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:Button ID="btnLocation" runat="server" CssClass="btn btn-default" Text="Set Location" OnClick="btnLocation_Click" />
                                                                        <asp:Button ID="btnSaveLocation" runat="server" CssClass="btn btn-default" Text="Save Location" OnClick="btnSaveLocation_Click" />
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorLatitute" runat="server" ControlToValidate="txtLatitute" ErrorMessage="Latitute Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblLatitute2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLatitute2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="display: none;">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label ID="lblinfoWindow" runat="server" class="col-md-4 control-label" Text="infoWindow"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtinfoWindow" runat="server" CssClass="form-control"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--<div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCUSERID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCUSERID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCUSERID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCUSERID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCUSERID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                       <%-- </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblENTRYDATE1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtENTRYDATE1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtENTRYDATE" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblENTRYDATE2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtENTRYDATE2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                       <%-- </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblENTRYTIME1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtENTRYTIME1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtENTRYTIME" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblENTRYTIME2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtENTRYTIME2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                       <%-- </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblUPDTTIME1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUPDTTIME1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtUPDTTIME" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblUPDTTIME2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtUPDTTIME2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                        <%--  <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblActive1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:CheckBox ID="cbActive" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblActive2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                      <%--  </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblDefualt1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDefualt1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:CheckBox ID="cbDefualt" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblDefualt2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDefualt2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                        <div class="row">
                                                            <%-- <div class="col-md-12">
                                                                <ul class="nav nav-pills nav-justified steps" style="margin-bottom: 0px; padding-bottom: 0px; padding-top: 0px;">
                                                                    <li class=" active" style="width: 33%" id="licomditel" runat="server">
                                                                        <a style="color: #FFFFFF; background-color: #3598DC; width: 90%;" href="#tab_general" class="step" data-toggle="tab">
                                                                            <span class="number" style="border-radius: 104%;">1 </span><span class="desc">
                                                                                <asp:Label ID="lblBusinessCo" runat="server" Text="Locate Your existing Address" meta:resourcekey="lblBusinessCoResource1"></asp:Label>
                                                                            </span>
                                                                        </a>
                                                                    </li>

                                                                    <li class="" style="width: 33%" id="libisnde" runat="server">
                                                                        <a style="color: #FFFFFF; background-color: #C49F47; width: 90%;" href="#tab_meta" class="step" data-toggle="tab">
                                                                            <span class="number" style="border-radius: 104%;">2 </span><span class="desc">
                                                                                <asp:Label ID="lblBusinessDetai" runat="server" Text="Search a Address by moving Red Truck" meta:resourcekey="lblBusinessDetaiResource1"></asp:Label>
                                                                            </span>
                                                                        </a>
                                                                    </li>

                                                                    <li style="width: 33%" class="">
                                                                        <a style="color: #FFFFFF; background-color: #44B6AE; width: 100%;" href="#tab_images" class="step" data-toggle="tab">
                                                                            <span class="number" style="border-radius: 104%;">3 </span><span class="desc">
                                                                                <asp:Label ID="lblWebExistance" runat="server" Text="Search Address by using Saved Geo Keywords" meta:resourcekey="lblWebExistanceResource1"></asp:Label>
                                                                            </span>
                                                                        </a>&nbsp;
                                                                    </li>
                                                                </ul>
                                                            </div>--%>

                                                            <asp:Panel ID="Panel1" DefaultButton="Button1" class="col-md-12" runat="server">
                                                                <div class="tab-content no-space">
                                                                    <div class="tab-pane active" id="tab_general">

                                                                        <input id="searchInput123" value="Digital Edge Solutions Computer, Hawally, Kuwait" class="input-controls" type="text" style="width: 75%" placeholder="Enter a location">
                                                                        <input id="Button1" type="button" value="" style="display:none" />
                                                                        <div id="map123" style="height: 300px">
                                                                        </div>

                                                                    </div>

                                                                    <%--<div class="tab-pane" id="tab_meta">
                                                                        <div style="height: 300px">
                                                                            <h1>Search a Address by moving Red Truck</h1>
                                                                        </div>
                                                                    </div>

                                                                    <div class="tab-pane" id="tab_images">
                                                                        <div style="height: 300px">
                                                                            <h1>Search Address by using Saved Geo Keywords</h1>
                                                                        </div>
                                                                    </div>--%>
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
                                                                                        <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1">
                                                                                            <thead>
                                                                                                <tr>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhContactMyID" Text="Contact My ID"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhContactID" Text="ContactName"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhAdressName1" Text="AdressName1"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhCOUNTRYID" Text="Countryname"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblSTATE" Text="STATE"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblCITY" Text="CITY"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhADDR1" Text="ADDR1"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhADDR2" Text="ADDR2"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblRemarks" Text="Remarks"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblAdressshortname1" Text="Adressshortname1"></asp:Label></th>                                                                                                  
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblLatitute" Text="Latitute"></asp:Label></th>
                                                                                                    <th>
                                                                                                        <asp:Label runat="server" ID="lblhLongitute" Text="Longitute"></asp:Label></th>
                                                                                                   
                                                                                                    <th style="width: 60px;">ACTION</th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="" DataKeyNames="">
                                                                                                    <LayoutTemplate>
                                                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                                                        </tr>
                                                                                                    </LayoutTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblContactMyID" runat="server" Text='<%# Eval("ContactMyID")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblhContactID" runat="server" Text='<%#Name1(Convert.ToInt32(Eval("ContactID")))%>'></asp:Label></td>

                                                                                                            <td>
                                                                                                                <asp:Label ID="lblhAdressName1" runat="server" Text='<%# Eval("AdressName1")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblhCOUNTRYID" runat="server" Text='<%#Name(Convert.ToInt32( Eval("COUNTRYID")))%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblSTATEs" runat="server" Text='<%# GetState(Convert.ToInt32(Eval("STATE")),Convert.ToInt32( Eval("COUNTRYID")) )%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblhCITY" runat="server" Text='<%# nameCITY(Convert.ToInt32(Eval("CITY")),Convert.ToInt32(Eval("STATE")))%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblhADDR1" runat="server" Text='<%# Eval("ADDR1")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblhADDR2" runat="server" Text='<%# Eval("ADDR2")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblAdressshortname1" runat="server" Text='<%# Eval("Adressshortname1")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblLatitute" runat="server" Text='<%# Eval("Latitute")%>'></asp:Label></td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblhLongitute" runat="server" Text='<%#Eval("Longitute")%>'></asp:Label></td>                                                                                                           
                                                                                                            <td>
                                                                                                                <table>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("ContactMyID")+","+ Eval("DeliveryAdressID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                                        <td>
                                                                                                                            <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("ContactMyID")+","+ Eval("DeliveryAdressID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>                                                                                                                       
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
                            <%-- List --%>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet box green">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-edit"></i>
                                                <asp:Label runat="server" ID="Label5"></asp:Label>
                                                List
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhContactMyID" Text="ID"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhContactID" Text="Contact"></asp:Label></th>

                                                        <th>
                                                            <asp:Label runat="server" ID="lblhCOUNTRYID" Text="COUNTRY"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblSTATE" Text="STATE"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblCITY" Text="CITY"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhADDR1" Text="ADDR1"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhADDR2" Text="ADDR2"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhAdressName1" Text="AdressName1"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblRemarks" Text="Remarks"></asp:Label></th>
                                                        <%-- <th>
                                                            <asp:Label runat="server" ID="lblAdressshortname1" Text="Short ADDR"></asp:Label></th>--%>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblLatitute" Text="Latitute"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhLongitute" Text="Longitute"></asp:Label></th>

                                                        <th style="width: 60px;">ACTION</th>
                                                    </tr>
                                                </thead>

                                                <tbody>
                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="TenentID,ContactMyID,DeliveryAdressID,GoogleName,Latitute,Longitute,ContactID,AdressShortName1,AdressName1,ADDR1,ADDR2,CITY,STATE,COUNTRYID,Block,Building,Street,Lane,ForFlat,REMARKS,CRUP_ID,CUSERID,ENTRYDATE,ENTRYTIME,UPDTTIME,Active,Defualt" DataKeyNames="TenentID,ContactMyID,DeliveryAdressID,GoogleName,Latitute,Longitute,ContactID,AdressShortName1,AdressName1,ADDR1,ADDR2,CITY,STATE,COUNTRYID,Block,Building,Street,Lane,ForFlat,REMARKS,CRUP_ID,CUSERID,ENTRYDATE,ENTRYTIME,UPDTTIME,Active,Defualt">

                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblContactMyID" runat="server" Text='<%# Eval("ContactMyID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblhContactID" runat="server" Text='<%#Name1(Convert.ToInt32(Eval("ContactMyID")))%>'></asp:Label></td>


                                                                <td>
                                                                    <asp:Label ID="lblhCOUNTRYID" runat="server" Text='<%#Name(Convert.ToInt32( Eval("COUNTRYID")))%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblSTATEs" runat="server" Text='<%# GetState(Convert.ToInt32(Eval("STATE")!=null?Eval("STATE"):0),Convert.ToInt32(Eval("COUNTRYID")!=null?Eval("COUNTRYID"):126))%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblhCITY" runat="server" Text='<%# nameCITY(Convert.ToInt32(Eval("CITY")!=null?Eval("CITY"):0),Convert.ToInt32(Eval("STATE")!=null?Eval("STATE"):0))%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblhADDR1" runat="server" Text='<%# Eval("ADDR1")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblhADDR2" runat="server" Text='<%# GetAdd2(Convert.ToInt32(Eval("ContactMyID")),Convert.ToInt32(Eval("DeliveryAdressID"))) %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblhAdressName1" runat="server" Text='<%# Eval("AdressName1")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# GetRemark(Convert.ToInt32(Eval("ContactMyID")),Convert.ToInt32(Eval("DeliveryAdressID"))) %>'></asp:Label></td>
                                                                <%--<td>
                                                                    <asp:Label ID="lblAdressshortname1" runat="server" Text='<%# Eval("Adressshortname1")%>'></asp:Label></td>--%>
                                                                <td>
                                                                    <asp:Label ID="lblLatitute" runat="server" Text='<%# Eval("Latitute")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblhLongitute" runat="server" Text='<%#Eval("Longitute")%>'></asp:Label></td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("ContactMyID")+","+ Eval("DeliveryAdressID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("ContactMyID")+","+ Eval("DeliveryAdressID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
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
                            <%-- End List --%>
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
