<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="tblcontact_addon1.aspx.cs" Inherits="Web.Master.tblcontact_addon1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }
    </style>
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }

        function showProgress1() {
            $.blockUI({ message: '<h1>please wait...</h1>' });
        }

        function stopProgress() {
            $.unblockUI();
        }
    </script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDd-0GRWdGVwGD-nc5Jojdnr6aQO46RV_4&sensor=false&libraries=places"></script>
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
                        <a href="#">contact_addon1 </a>
                    </li>
                </ul>--%>
                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <asp:Panel ID="PanelError" runat="server" Visible="false">
                    <div class="alert alert-danger alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblerror" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>
                                        <asp:Label runat="server" ID="lblADDLable" Text="Add Coustomer "></asp:Label>
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
                                            <%--<asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />--%>
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
                                                        <div class="row">

                                                            <div class="col-md-6">
                                                                <asp:Panel ID="PenalSearchComp" runat="server" DefaultButton="LnkSearcustomer">
                                                                    <div class="form-group">
                                                                        <asp:Label runat="server" ID="Label23" class="col-md-4 control-label" Text="Search Customer"></asp:Label>
                                                                        <div class="col-md-4">
                                                                            <asp:TextBox ID="txtCustomerSearch" runat="server" placeholder="Number Only" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtCustomerSearch" FilterMode="ValidChars" runat="server" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCustomerSearch" ErrorMessage="Customerid Required." CssClass="Validation" ValidationGroup="submitsearch"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:LinkButton ID="LnkSearcustomer" CssClass="btn btn-icon-only yellow" runat="server" ValidationGroup="submitsearch" OnClick="LnkSearcustomer_Click">  <%--Style="margin-top: -23px; padding-left: 0px; margin-left: 5px; margin-bottom: 7px;"--%>
                                                                                                             <i class="fa fa-search" ></i>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="lblCustomerId1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCustomerId1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpCustomerId" OnSelectedIndexChanged="drpCustomerId_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="table-group-action-input form-control input-medium select2me"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCustomerId" runat="server" ControlToValidate="drpCustomerId" InitialValue="0" ErrorMessage="Customerid Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCustomerId2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCustomerId2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row">

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="lblPromotionOn1s" class="col-md-4 control-label"></asp:Label>
                                                                    <asp:TextBox runat="server" ID="txtPromotionOn1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpPromotionOn" runat="server" CssClass="table-group-action-input form-control input-medium select2me">
                                                                            <asp:ListItem Value="0">--Select Promotionon--</asp:ListItem>
                                                                            <asp:ListItem Value="1">On Promotion </asp:ListItem>
                                                                            <asp:ListItem Value="2">Normal</asp:ListItem>
                                                                            <asp:ListItem Value="3" Selected="True">Not Apply</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPromotionOn2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPromotionOn2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="lblAdvCollectionStatus1s" class="col-md-4 control-label"></asp:Label>
                                                                    <asp:TextBox runat="server" ID="txtAdvCollectionStatus1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpAdvCollectionStatus" runat="server" CssClass="table-group-action-input form-control input-medium select2me">
                                                                            <asp:ListItem Value="0">--Select Adv collection status</asp:ListItem>
                                                                            <asp:ListItem Value="1">Collected </asp:ListItem>
                                                                            <asp:ListItem Value="2">Released</asp:ListItem>
                                                                            <asp:ListItem Value="3" Selected="True">Not Apply</asp:ListItem>

                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblAdvCollectionStatus2h" class="col-md-4 control-label"></asp:Label>
                                                                    <asp:TextBox runat="server" ID="txtAdvCollectionStatus2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="Label4" class="col-md-4 control-label" Text="Height"></asp:Label>

                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txtHeight" class="form-control input-medium" placeholder="Feet,CM" ToolTip="Feet,CM"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtHeight" FilterMode="ValidChars" runat="server" ValidChars="0123456789,"></cc1:FilteredTextBoxExtender>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCOUNTRYID1s" class="col-md-4 control-label" Text="Nationality"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpCOUNTRYID" runat="server" CssClass="form-control input-medium select2me"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label runat="server" ID="Label25" class="col-md-4 control-label" Text="Agreed Cabs"></asp:Label>

                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txtAgreedCabs" class="form-control input-medium" placeholder="1500" ToolTip="1500"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtHeight" FilterMode="ValidChars" runat="server" ValidChars="0123456789.,"></cc1:FilteredTextBoxExtender>
                                                                    </div>


                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <div class="col-md-8">
                                                                        <asp:Button ID="BtnSaveandContinue" runat="server" class="btn green-haze dz-square" ValidationGroup="submit" Text="save and Continue" OnClick="BtnSaveandContinue_Click" />
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>


                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lbladdress1" runat="server" Text="Delivery Address to be use" class="col-md-4 control-label"></asp:Label>
                                                                    <div class="col-md-6">
                                                                        <asp:DropDownList ID="drpAddress1" AutoPostBack="true" OnSelectedIndexChanged="drpAddress1_SelectedIndexChanged" runat="server" CssClass="table-group-action-input form-control input-medium select2me"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:Button ID="btneditAddress" CssClass="btn yellow" runat="server" Text="Edit" OnClick="btneditAddress_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%--   </div>
                                                        <div class="row">--%>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <div class="col-md-8">
                                                                        <asp:Button ID="Button1" runat="server" CssClass="btn yellow-gold dz-square" Text="Set Primary Address" OnClick="Button1_Click" />
                                                                        <asp:Button ID="Button2" runat="server" CssClass="btn yellow-saffron dz-square" Text="Add New Address" OnClick="Button2_Click" />
                                                                        <asp:Label ID="lblMSSGE" runat="server" Visible="false"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <br />
                                                        <div class="row">
                                                            <asp:Panel ID="penalmap" Visible="true" runat="server">
                                                                <div class="col-md-12">


                                                                    <div id="map123" style="height: 300px">
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

                        <div class="portlet box blue">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-gift"></i>
                                    Customer Delivery type & meal
                                </div>
                                <div class="caption" style="margin-left: 45px;">
                                </div>
                                <div class="tools">
                                    <a href="javascript:;" class="collapse"></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                    <asp:LinkButton ID="LinkButton12" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                    <a href="javascript:;" class="remove"></a>
                                </div>
                                <div class="actions btn-set">
                                </div>

                            </div>
                            <div class="portlet-body">
                                <asp:UpdatePanel ID="UpdatePanelDelivery" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="tabbable">

                                            <div class="tab-content no-space">
                                                <div class="row" style="margin-top: 10px;">

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="Label22" class="col-md-2 control-label" Text="Plan"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpPlan" AutoPostBack="true" OnSelectedIndexChanged="drpPlan_SelectedIndexChanged" class="select2me table-group-action-input form-control input-small" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpPlan" InitialValue="0" ErrorMessage="select Delivery Time." CssClass="Validation" ValidationGroup="Delivery"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:LinkButton ID="lnkaddplan" runat="server" OnClick="lnkaddplan_Click">
                                                                                    <i class="icon-plus " style="color:black;padding-left: 4px;"></i>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="Label17" class="col-md-4 control-label" Text="Delivery Time"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpDelivieryTime1" AutoPostBack="true" OnSelectedIndexChanged="drpDelivieryTime1_SelectedIndexChanged" class="select2me table-group-action-input form-control input-small" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpDelivieryTime1" InitialValue="0" ErrorMessage="select Delivery Time." CssClass="Validation" ValidationGroup="Delivery"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <%-- </div>
                                                        <div class="row">--%>

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="Label18" class="col-md-2 control-label" Text="Meal Deliver"></asp:Label>

                                                            <div class="col-md-5">
                                                                <asp:DropDownList ID="drpMealDeliver" class="select2me table-group-action-input form-control input-small" runat="server"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpMealDeliver" InitialValue="0" ErrorMessage="select Meal." CssClass="Validation" ValidationGroup="Delivery"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <asp:Button ID="btnDelivery" CssClass="btn green" runat="server" Text="Add Delivery" Enabled="false" OnClick="btnDelivery_Click" ValidationGroup="Delivery" OnClientClick="showProgress()" />
                                                                <asp:Button ID="btnallmeal" Visible="false" runat="server" CssClass="btn yellow" Text="All Meal" OnClick="btnallmeal_Click" OnClientClick="showProgress()" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>


                                        <div class="table-scrollable">
                                            <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                <thead>
                                                    <tr>
                                                        <td style="width: 5%;">
                                                            <asp:Label ID="Label19" runat="server" Text="#"></asp:Label></td>
                                                        <th style="width: 20%;">Plan</th>
                                                        <th style="width: 25%;">
                                                            <asp:Label runat="server" ID="Label20" Text="Delivery Time"></asp:Label></th>
                                                        <th style="width: 30%;">
                                                            <asp:Label runat="server" ID="Label21" Text="Meal Delivered"></asp:Label></th>
                                                        <th style="width: 20%;">ACTION</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <asp:ListView ID="DeliveryMealtype" runat="server" OnItemCommand="DeliveryMealtype_ItemCommand" DataKey="" DataKeyNames="">

                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="align-content: center">
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="Label20" Text='<%# GetPlane(Convert.ToInt32(Eval("planid"))) %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# getDeliveryTime(Convert.ToInt32(Eval("DeliveryTime"))) %>'></asp:Label></td>

                                                                    <td>
                                                                        <asp:Label ID="lblRemak" runat="server" Text='<%# getMeal(Convert.ToInt32(Eval("ItemCode")))%>'></asp:Label></td>

                                                                    <td>
                                                                        <%-- <asp:LinkButton ID="btnEditDelivery" CommandName="btnEdit" CommandArgument='<%# Eval("CustomerID")+","+ Eval("myID") %>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>--%>

                                                                        &nbsp;

                                                             <asp:LinkButton ID="btnDeleteDelivery" runat="server" class="btn btn-sm red filter-cancel" CommandArgument='<%#  Eval("CustomerID")+","+ Eval("myID") %>' CommandName="btnDelete" OnClientClick="return ConfirmMsg();"><i class="fa fa-times"></i></asp:LinkButton>

                                                                        &nbsp;&nbsp;

                                                                        <asp:LinkButton ID="lnkbtnchangeTime" runat="server" OnClientClick="showProgress1()" class="btn btn-sm yellow filter-cancel" CommandArgument='<%#  Eval("CustomerID")+","+ Eval("myID") %>' CommandName="btnChange">Change</i></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>

                                                    </asp:Panel>
                                                </tbody>
                                            </table>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanelDelivery">
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



                        <%--<asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>--%>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-horizontal form-row-seperated">
                                    <div class="portlet box blue">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>

                                                Weight Measured
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <asp:LinkButton ID="LinkButton1" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                        </div>
                                        <div class="portlet-body">

                                            <div class="table-scrollable">
                                                <asp:Panel ID="Panel2" runat="server" DefaultButton="btnweight">
                                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">

                                                        <tr>
                                                            <td style="width: 5%;">
                                                                <asp:Label ID="Label3" runat="server" Text="#"></asp:Label>
                                                            </td>
                                                            <td style="width: 25%;">
                                                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control input-medium" placeholder="Insert Date"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="DateValidde" runat="server" ControlToValidate="txtdate" ErrorMessage="Date Required" CssClass="Validation" ValidationGroup="Weight"></asp:RequiredFieldValidator>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdate" Format="dd-MMM-yy" Enabled="True">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td style="width: 25%;">
                                                                <asp:TextBox ID="txtwight" runat="server" CssClass="form-control input-medium" placeholder="Insert Weight"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="weightvalide" runat="server" ControlToValidate="txtwight" ErrorMessage="Weight Required" CssClass="Validation" ValidationGroup="Weight"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="width: 25%;">
                                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control input-medium" placeholder="Insert Remark"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RemarkValide" runat="server" ControlToValidate="txtRemarks" ErrorMessage="Remarks Required" CssClass="Validation" ValidationGroup="Weight"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                            <td style="width: 20%;">
                                                                <div class="actions btn-set">
                                                                    <asp:Button ID="btnweight" CssClass="btn red" runat="server" Text="Add Weight" ValidationGroup="Weight" OnClick="btnweight_Click" />
                                                                </div>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </asp:Panel>
                                                <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                    <thead>
                                                        <tr>

                                                            <td style="width: 5%;">
                                                                <asp:Label ID="lblh" runat="server" Text="#"></asp:Label></td>
                                                            <td style="width: 25%;">
                                                                <asp:Label ID="lblhDateMeasure" runat="server" Text="DateMeasure"></asp:Label></td>
                                                            <td style="width: 25%;">
                                                                <asp:Label ID="lblhWeightMeasuredinKG" runat="server" Text="Weight Measured in KG"></asp:Label></td>
                                                            <td style="width: 25%;">
                                                                <asp:Label ID="lblaRemarks" runat="server" Text="Remarks"></asp:Label></td>
                                                            <td style="width: 20%;">ACTION</td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:ListView ID="WeightMeasured" runat="server" OnItemCommand="WeightMeasured_ItemCommand" DataKey="" DataKeyNames="">

                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <%#Container.DataItemIndex+1 %>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblhDateMeasure" runat="server" Text='<%# Eval("DateAdded")%>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblhWeightMeasuredinKG" runat="server" Text='<%# Eval("Weight")%>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblaRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:LinkButton ID="btnEditWeight" CommandName="btnEdit" CommandArgument='<%# Eval("customerID")+","+Eval("myID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>

                                                                        &nbsp;

                                                                        <asp:LinkButton ID="btnDeleteWeight" runat="server" class="btn btn-sm red filter-cancel" CommandArgument='<%#  Eval("customerID")+","+Eval("myID")%>' CommandName="btnDelete" OnClientClick="return ConfirmMsg();"><i class="fa fa-times"></i></asp:LinkButton>


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
                        </div>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>                        
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>--%>

                        <div class="portlet box blue">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-gift"></i>
                                    Like
                                </div>
                                <div class="caption" style="margin-left: 45px;">
                                </div>
                                <div class="tools">
                                    <a href="javascript:;" class="collapse"></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                    <asp:LinkButton ID="LinkButton2" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                    <a href="javascript:;" class="remove"></a>
                                </div>

                            </div>
                            <div class="portlet-body">

                                <div class="tabbable">
                                    <div class="tab-content no-space">
                                        <asp:Panel ID="Panel3" runat="server" DefaultButton="btnlike">
                                            <div class="row" style="margin-top: 10px;">
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <asp:Label runat="server" ID="Label6" class="col-md-4 control-label" Text="Product"></asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="drpItem" class="select2me table-group-action-input form-control input-medium" ForeColor="Blue" runat="server"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="likeProductvalide" runat="server" ControlToValidate="drpItem" ErrorMessage="Item of Product Item Required." CssClass="Validation" InitialValue="0" ValidationGroup="Like"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%-- </div>
                                                        <div class="row">--%>

                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Label runat="server" ID="Label8" class="col-md-3 control-label" Text="Remarks "></asp:Label>

                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtremark" CssClass="form-control" ForeColor="Blue" runat="server"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtremark" ErrorMessage="Remark Required." CssClass="Validation" ValidationGroup="Like"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:Button ID="btnlike" CssClass="btn yellow" runat="server" Text="Add Like" Enabled="false" ValidationGroup="Like" OnClick="btnlike_Click" />
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>


                                <div class="table-scrollable">
                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                        <thead>
                                            <tr>
                                                <td style="width: 5%;">
                                                    <asp:Label ID="lbl" runat="server" Text="#"></asp:Label></td>
                                                <th style="width: 35%;">
                                                    <asp:Label runat="server" ID="lblhItemCode" Text="ItemCode"></asp:Label></th>
                                                <th style="width: 40%;">
                                                    <asp:Label runat="server" ID="lblhRemarks" Text="Remarks"></asp:Label></th>
                                                <th style="width: 20%;">ACTION</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="CustomerLike" runat="server" OnItemCommand="CustomerLike_ItemCommand" DataKey="" DataKeyNames="">

                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="align-content: center">
                                                            <%#Container.DataItemIndex+1 %>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%#nameItem(Convert.ToInt32(Eval("ItemCode")))%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblRemak" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label></td>

                                                        <td>
                                                            <asp:LinkButton ID="btnEditLike" CommandName="btnEdit" CommandArgument='<%# Eval("customerID")+","+Eval("myID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>

                                                            &nbsp;

                                                             <asp:LinkButton ID="btnDeleteLike" runat="server" class="btn btn-sm red filter-cancel" CommandArgument='<%#  Eval("customerID")+","+Eval("myID")%>' CommandName="btnDelete" OnClientClick="return ConfirmMsg();"><i class="fa fa-times"></i></asp:LinkButton>

                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>


                        <%-- </ContentTemplate>
                        </asp:UpdatePanel>                                                                                    
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>--%>
                        <div class="portlet box blue">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-gift"></i>
                                    Alergie
                                </div>
                                <%--<div class="caption" style="margin-left: 30px;">
                                    <asp:DropDownList ID="drpAllergy" class="select2me input-medium" ForeColor="Blue" runat="server"></asp:DropDownList>
                                    Remarks
                                                <asp:TextBox ID="txtremak" ForeColor="Blue" Style="width: 350px;" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnAllergy" CssClass="btn bg-red-pink" runat="server" Text="Add Allergy" Enabled="false" OnClick="btnAllergy_Click" />
                                </div>--%>
                                <div class="tools">
                                    <a href="javascript:;" class="collapse"></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                    <asp:LinkButton ID="LinkButton5" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                    <a href="javascript:;" class="remove"></a>
                                </div>

                            </div>
                            <div class="portlet-body">
                                <asp:Panel ID="Panel4" runat="server" DefaultButton="btnAllergy">
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="Label7" class="col-md-4 control-label" Text="Product"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="drpAllergy" class="select2me table-group-action-input form-control input-medium" ForeColor="Blue" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="drpAllergy" ErrorMessage="Item of Product Required." CssClass="Validation" ValidationGroup="Allergy" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- </div>
                                                        <div class="row">--%>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="Label9" class="col-md-3 control-label" Text="Remarks "></asp:Label>

                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtremak" CssClass="form-control" ForeColor="Blue" runat="server"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtremak" ErrorMessage="Remark Required." CssClass="Validation" ValidationGroup="Allergy"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnAllergy" CssClass="btn bg-red-pink" runat="server" Text="Add Allergy" ValidationGroup="Allergy" Enabled="false" OnClick="btnAllergy_Click" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </asp:Panel>

                                <div class="table-scrollable">

                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                        <thead>
                                            <tr>
                                                <td style="width: 5%;">
                                                    <asp:Label ID="lbla" runat="server" Text="#"></asp:Label></td>
                                                <th style="width: 35%;">
                                                    <asp:Label runat="server" ID="lblIhtemCode" Text="ItemCode"></asp:Label></th>
                                                <th style="width: 40%;">
                                                    <asp:Label runat="server" ID="lbliRemark" Text="Remarks"></asp:Label></th>

                                                <th style="width: 20%;">ACTION</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="Allergy" runat="server" OnItemCommand="Allergy_ItemCommand" DataKey="" DataKeyNames="">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblIhtemCode" runat="server" Text='<%#nameAllergy(Convert.ToInt32(Eval("ItemCode")))%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbliRemark" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label></td>

                                                        <td>
                                                            <asp:LinkButton ID="btnEditAlergie" CommandName="btnEdit" CommandArgument='<%# Eval("customerID")+","+Eval("myID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>

                                                            &nbsp;

                                                             <asp:LinkButton ID="btnDeleteAlergie" runat="server" class="btn btn-sm red filter-cancel" CommandArgument='<%#  Eval("customerID")+","+Eval("myID")%>' CommandName="btnDelete" OnClientClick="return ConfirmMsg();"><i class="fa fa-times"></i></asp:LinkButton>

                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </tbody>
                                    </table>


                                </div>
                            </div>
                        </div>
                        <%-- </ContentTemplate>
                        </asp:UpdatePanel>                                                 
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>--%>
                        <div class="portlet box blue">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-gift"></i>
                                    DisLike
                                </div>
                                <%--<div class="caption" style="margin-left: 30px;">
                                    <asp:DropDownList ID="drpitems" class="select2me input-medium" ForeColor="Blue" runat="server"></asp:DropDownList>
                                    Remarks
                                                <asp:TextBox ID="txtrem" ForeColor="Blue" Style="width: 350px;" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnDislike" CssClass="btn green" runat="server" Text="Add Dislike" Enabled="false" OnClick="btnDislike_Click" />
                                </div>--%>
                                <div class="tools">
                                    <a href="javascript:;" class="collapse"></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                    <asp:LinkButton ID="LinkButton3" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                    <a href="javascript:;" class="remove"></a>
                                </div>

                            </div>
                            <div class="portlet-body">
                                <asp:Panel ID="Panel5" runat="server" DefaultButton="btnDislike">
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="Label10" class="col-md-4 control-label" Text="Product"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="drpitems" class="select2me table-group-action-input form-control input-medium" ForeColor="Blue" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="drpitems" ErrorMessage="Item of Product Required." CssClass="Validation" ValidationGroup="DisLike" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- </div>
                                                        <div class="row">--%>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="Label11" class="col-md-3 control-label" Text="Remarks "></asp:Label>

                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtrem" CssClass="form-control" ForeColor="Blue" runat="server"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtrem" ErrorMessage="Remark Required." CssClass="Validation" ValidationGroup="DisLike"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnDislike" CssClass="btn green" runat="server" Text="Add DisLike" Enabled="false" ValidationGroup="DisLike" OnClick="btnDislike_Click" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </asp:Panel>

                                <div class="table-scrollable">

                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                        <thead>
                                            <tr>
                                                <td style="width: 5%;">
                                                    <asp:Label ID="Label1" runat="server" Text="#"></asp:Label></td>
                                                <th style="width: 35%;">
                                                    <asp:Label runat="server" ID="lblItemCode" Text="ItemCode"></asp:Label></th>
                                                <th style="width: 40%;">
                                                    <asp:Label runat="server" ID="lblhRemark" Text="Remarks"></asp:Label></th>

                                                <th style="width: 20%;">ACTION</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="CustomerDisLike" runat="server" OnItemCommand="CustomerDisLike_ItemCommand" DataKey="" DataKeyNames="">

                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# nameItems(Convert.ToInt32(Eval("ItemCode")))%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblRemak" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label></td>
                                                        <td>
                                                            <asp:LinkButton ID="btnEditDisLike" CommandName="btnEdit" CommandArgument='<%# Eval("customerID")+","+Eval("myID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>

                                                            &nbsp;

                                                             <asp:LinkButton ID="btnDeleteDisLike" runat="server" class="btn btn-sm red filter-cancel" CommandArgument='<%#  Eval("customerID")+","+Eval("myID")%>' CommandName="btnDelete" OnClientClick="return ConfirmMsg();"><i class="fa fa-times"></i></asp:LinkButton>

                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </tbody>
                                    </table>


                                </div>
                            </div>
                        </div>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>--%>
                        <div class="portlet box blue">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-gift"></i>
                                    Complain
                                </div>
                                <%-- <div class="caption" style="margin-left: 20px;">

                                    <asp:DropDownList ID="drpcomplinid" class="select2me input-medium" ForeColor="Blue" runat="server"></asp:DropDownList>
                                    Remarks
                                                <asp:TextBox ID="txtrema" ForeColor="Blue" Style="width: 350px;" runat="server"></asp:TextBox>
                                    <asp:Button ID="btncomplain" CssClass="btn blue-madison" runat="server" Text="Add Complain" Enabled="false" OnClick="btncomplain_Click" />

                                </div>--%>
                                <div class="tools">
                                    <a href="javascript:;" class="collapse"></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                    <asp:LinkButton ID="LinkButton4" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                    <a href="javascript:;" class="remove"></a>
                                </div>

                            </div>
                            <div class="portlet-body">

                                <%-- <div class="table-scrollable">

                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">

                                        <tr>
                                            <td style="width: 5%;">
                                                <asp:Label ID="Label16" runat="server" Text="#"></asp:Label>
                                            </td>
                                            <td style="width: 25%;">
                                                <asp:DropDownList ID="drpcomplinid" class="select2me table-group-action-input form-control input-medium" ForeColor="Blue" runat="server"></asp:DropDownList>
                                            </td>
                                            <td style="width: 25%;">
                                                <asp:TextBox ID="txtComplainDate" runat="server" CssClass="form-control input-medium" placeholder="Complain Date"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtComplainDate" Format="dd-MMM-yy" Enabled="True">
                                                </cc1:CalendarExtender>

                                            </td>
                                            <td style="width: 25%;">
                                                
                                                <asp:TextBox ID="txtrema" CssClass="form-control input-medium" placeholder="Remark" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="width: 20%;">
                                                <div class="actions btn-set">
                                                    <asp:Button ID="btncomplain" CssClass="btn blue-madison" runat="server" Text="Add Complain" Enabled="false" OnClick="btncomplain_Click" />
                                                </div>
                                            </td>
                                        </tr>

                                    </table>

                                </div>--%>

                                <asp:Panel ID="Panel6" runat="server" DefaultButton="btncomplain">

                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="Label12" class="col-md-4 control-label" Text="Complain"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="drpcomplinid" class="select2me table-group-action-input form-control input-medium" ForeColor="Blue" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="drpcomplinid" ErrorMessage="Item of Product Required." CssClass="Validation" ValidationGroup="Complain" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="Label16" class="col-md-4 control-label" Text="Complain Date"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtComplainDate" runat="server" CssClass="form-control" placeholder="Complain Date"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtComplainDate" ErrorMessage="Date Required." CssClass="Validation" ValidationGroup="Complain"></asp:RequiredFieldValidator>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtComplainDate" Format="MM/dd/yyyy" Enabled="True">
                                                    </cc1:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="Label13" class="col-md-4 control-label" Text="Remarks "></asp:Label>

                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtrema" CssClass="form-control" placeholder="Remark" ForeColor="Blue" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtrema" ErrorMessage="Remark Required." CssClass="Validation" ValidationGroup="Complain"></asp:RequiredFieldValidator>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <asp:Button ID="btncomplain" CssClass="btn blue-madison" runat="server" Text="Add Complain" Enabled="false" ValidationGroup="Complain" OnClick="btncomplain_Click" />
                                        </div>


                                    </div>

                                </asp:Panel>

                                <div class="table-scrollable">

                                    <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                        <thead>
                                            <tr>
                                                <td style="width: 5%;">
                                                    <asp:Label ID="Label2" runat="server" Text="#"></asp:Label></td>
                                                <th style="width: 25%;">
                                                    <asp:Label runat="server" ID="lblcomplinid" Text="complinid"></asp:Label></th>
                                                <th style="width: 20%;">
                                                    <asp:Label runat="server" ID="Label14" Text="Complain Date"></asp:Label>
                                                </th>
                                                <th style="width: 30%;">
                                                    <asp:Label runat="server" ID="lblRemarks" Text="Remarks"></asp:Label></th>
                                                <th style="width: 20%;">ACTION</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:ListView ID="CustomerComplainID" runat="server" OnItemCommand="CustomerComplainID_ItemCommand" DataKey="" DataKeyNames="">

                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblcomplinid" runat="server" Text='<%# nameComplian(Convert.ToInt32(Eval("complinid")))%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="Label15" runat="server" Text='<%# Eval("DateAdded")%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblRemak" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label></td>
                                                        <td>
                                                            <asp:LinkButton ID="btnEditComplain" CommandName="btnEdit" CommandArgument='<%# Eval("customerID")+","+Eval("myID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton>

                                                            &nbsp;

                                                             <asp:LinkButton ID="btnDeleteComplain" runat="server" class="btn btn-sm red filter-cancel" CommandArgument='<%#  Eval("customerID")+","+Eval("myID")%>' CommandName="btnDelete" OnClientClick="return ConfirmMsg();"><i class="fa fa-times"></i></asp:LinkButton>

                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </tbody>
                                    </table>

                                </div>
                            </div>
                        </div>
                        <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>

                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
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
                                                            <%-- <div class="row">
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
                                                                                        </select>--%
                                                                                                entries</label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6 col-sm-12">
                                                                    <div id="sample_1_filter" class="dataTables_filter">
                                                                        <label>Search:<input type="search" class="form-control input-small input-inline" placeholder="" aria-controls="sample_1"></label>
                                                                    </div>
                                                                </div>
                                                            </div>--%>

                                                            <asp:Panel ID="PanelListError" runat="server" Visible="false">
                                                                <div class="alert alert-danger alert-dismissable">
                                                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                    <asp:Label ID="lbllistError" runat="server"></asp:Label>
                                                                </div>
                                                            </asp:Panel>

                                                            <div class="table-scrollable">
                                                                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>--%>
                                                                <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1" id="sample_1">
                                                                    <thead class="repHeader">
                                                                        <tr>
                                                                            <th style="width: 0px"></th>
                                                                            <th style="width: 100px;">ACTION</th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblCustomerNo" Text="Customer No"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhCustomerName" Text="Customer Name"></asp:Label></th>
                                                                            <%--<th>
                                                                                <asp:Label runat="server" ID="lblhDeliveryCountry" Text="Deliverycountry"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhArea" Text="Area"></asp:Label></th>--%>
                                                                            <%--<th>
                                                                                <asp:Label runat="server" ID="lblhDeliveryTime" Text="Deliverytime"></asp:Label></th>--%>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhAdvCollectionStatus" Text="Advcollectionstatus"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhPromotionOn" Text="Promotionon"></asp:Label></th>
                                                                            <th>ACTIVE
                                                                            </th>
                                                                            <%--<th>
                                                                                                            <asp:Label runat="server" ID="lblhlatituet" Text="Latituet"></asp:Label></th>
                                                                                                        <th>
                                                                                                            <asp:Label runat="server" ID="lblhlongituet" Text="Longituet"></asp:Label></th>--%>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" OnItemDataBound="Listview1_ItemDataBound" DataKey="CustomerId,DeliveryCountry,State,Area,DeliveryTime,AdvCollectionStatus,PromotionOn,Height,agreedcabs" DataKeyNames="CustomerId,DeliveryCountry,State,Area,DeliveryTime,AdvCollectionStatus,PromotionOn,Height,agreedcabs">

                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td></td>
                                                                                    <td>

                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("CustomerId")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                                <td>&nbsp;&nbsp;&nbsp;</td>
                                                                                                <td>
                                                                                                    <asp:LinkButton ID="btnDelete" runat="server" class="btn btn-sm red filter-cancel" CommandArgument='<%#  Eval("CustomerId")%>' CommandName="btnDelete" OnClientClick="return ConfirmMsg();"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                                <%-- <td><asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("JobId")%>'
                                                                                                                            CommandName="btnPrint" CommandArgument='<%# Eval("JobId")%>' runat="server" class="btn btn-sm green filter-submit margin-bottom"><i class="fa fa-print"></i></asp:LinkButton></td>
                                                                                                --%>
                                                                                            </tr>
                                                                                        </table>

                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="LinkButton9" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerId")%>'>
                                                                                            <asp:Label ID="Label24" runat="server" Text='<%# Eval("CustomerId")%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="LinkButton6" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerId")%>'>
                                                                                            <asp:Label ID="lblCustomerId" runat="server" Text='<%# name(Convert.ToInt32(Eval("CustomerId")))%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                    </td>

                                                                                    <%-- <td>
                                                                                        <asp:LinkButton ID="LinkButton7" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerId")%>'>
                                                                                            <asp:Label ID="lblDeliveryCountry" runat="server" Text='<%# nameCOU(Convert.ToInt32(Eval("DeliveryCountry")))%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="LinkButton8" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerId")%>'>
                                                                                            <asp:Label ID="lblArea" runat="server" Text='<%# nameCITY(Convert.ToInt32(Eval("Area")),Convert.ToInt32(Eval("State")))%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                    </td>--%>
                                                                                    <%-- <td>
                                                                                        <asp:LinkButton ID="LinkButton9" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerId")%>'>
                                                                                            <asp:Label ID="lblDeliveryTime" runat="server" Text='<%# Eval("DeliveryTime")%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                        <%--<asp:Label ID="lblDeliveryTime" runat="server" Text='<%# getsuppername11(Convert.ToInt32(Eval("DeliveryTime")))%>'></asp:Label>--%
                                                                                    </td>--%>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="LinkButton10" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerId")%>'>
                                                                                            <asp:Label ID="lblAdvCollectionStatus" runat="server" Text='<%# getsuppername(Convert.ToInt32(Eval("AdvCollectionStatus")))%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="LinkButton11" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerId")%>'>
                                                                                            <asp:Label ID="lblPromotionOn" runat="server" Text='<%#getsuppername1(Convert.ToInt32(Eval("PromotionOn")))%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblCID" Visible="false" runat="server" Text='<%# Eval("CustomerId") %>'></asp:Label>
                                                                                        <asp:LinkButton ID="lnkbtnActive" CssClass="btn btn-sm green filter-submit margin-bottom" CommandName="btnActive" CommandArgument='<%# Eval("CustomerId") %>' runat="server">                                                                                                                   
                                                                                        </asp:LinkButton>
                                                                                    </td>


                                                                                    <%--<td>
                                                                                                                    <asp:Label ID="lbllatituet" runat="server" Text='<%# Eval("latituet")%>'></asp:Label></td>
                                                                                                                <td>
                                                                                                                    <asp:Label ID="lbllongituet" runat="server" Text='<%# Eval("longituet")%>'></asp:Label></td>--%>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>

                                                                    </tbody>
                                                                </table>
                                                                <%--</ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
                                                            </div>

                                                        </div>

                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                        <asp:HiddenField ID="hideID" runat="server" Value="" />
                                    </div>

                                </asp:Panel>
                            </div>
                        </div>
                        <%--</ContentTemplate>
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
    </div>
</asp:Content>
