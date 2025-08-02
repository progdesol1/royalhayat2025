<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="HBInvoice.aspx.cs" Inherits="Web.Master.HBInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }
    </style>
    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }

        .ListItem {
            width: 33.33%;
        }

        .txtcenter {
            text-align: center;
        }
    </style>
    <%--<script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>--%>

    <script type="text/javascript">

        function showProgress()
        {
            $.blockUI({ message: '<h1>please wait...</h1>' });
        }

        function stopProgress()
        {
            $.unblockUI();
        }


    </script>

    <%--<script type="text/javascript">
        $(function () {
            $('[id*=drpweekofday]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>--%>
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
                        <a href="#">Student_demo </a>
                    </li>
                </ul>--%>

                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlErrorMsg" runat="server" Visible="false">
                    <div class="alert alert-danger alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblerrorMsg" runat="server"></asp:Label>
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
                                        <asp:Label runat="server" ID="lblpagemode"></asp:Label>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="btnPagereload" OnClick="btnPagereload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <div class="btn-group btn-group-circle btn-group-solid">

                                            <asp:Button ID="btnCustomer" class="btn red" runat="server" OnClick="btnCustomer_Click" Text="Add Customer" />
                                            <asp:Button ID="btnAddDriver" class="btn purple" runat="server" Text="Add Driver" />
                                            <asp:Button ID="btnAllergies" class="btn green" runat="server" OnClick="btnAllergies_Click" Text="Allergies" />
                                            <asp:Button ID="btnAddresses" class="btn purple" runat="server" OnClick="btnAddresses_Click" Text="Addresses" />

                                            <asp:Panel ID="PanelAddDriver" runat="server" CssClass="modalPopup" Style="padding: 1px; background-color: #fff; border: 2px solid pink; display: none; overflow: auto;">

                                                <div class="row" style="position: fixed; left: 10%; top: 4%; width: 60%">
                                                    <div class="col-md-12">
                                                        <div class="portlet box purple">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="fa fa-globe"></i>
                                                                    Add Driver
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="tabbable">
                                                                    <div class="tab-content no-space">
                                                                        <div class="form-body">
                                                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                                                <ContentTemplate>
                                                                                    <div class="col-md-12">
                                                                                        <div class="row">
                                                                                            <div class="form-group">
                                                                                                <div class="col-md-12" style="color: black; font-weight: normal">
                                                                                                    <label runat="server" id="Label78" class="col-md-4 control-label  ">
                                                                                                        <asp:Label ID="Label79" runat="server" Text="Driver name English" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                        <span class="required">* </span>
                                                                                                    </label>
                                                                                                    <div class="col-md-8">
                                                                                                        <asp:TextBox ID="txtDriverEng" runat="server" AutoCompleteType="Disabled" OnTextChanged="txtDriverEng_TextChanged" CssClass="form-control" AutoPostBack="true" placeholder="Driver Name English"></asp:TextBox>
                                                                                                        <asp:Label ID="lblDriverEnglish" runat="server" Visible="false" ForeColor="Red" Text=""></asp:Label>
                                                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator26" runat="server" ErrorMessage=" Requaird Driver Name English" ControlToValidate="txtDriverEng" ValidationGroup="DriverAdd"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <div class="row">
                                                                                            <div class="form-group">
                                                                                                <div class="col-md-12" style="color: black; font-weight: normal">
                                                                                                    <label runat="server" id="Label76" class="col-md-4 control-label  ">
                                                                                                        <asp:Label ID="Label77" runat="server" Text="Driver name Arabic" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                        <span class="required">* </span>
                                                                                                    </label>
                                                                                                    <div class="col-md-8">
                                                                                                        <asp:TextBox ID="txtDriverArabic" runat="server" CssClass="form-control" AutoPostBack="true" placeholder="Driver Name Arabic"></asp:TextBox>
                                                                                                        <asp:Label ID="lblDriverArabic" runat="server" Visible="false" ForeColor="Red" Text=""></asp:Label>
                                                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator25" runat="server" ErrorMessage=" Requaird Driver Name Arabic" ControlToValidate="txtDriverArabic" ValidationGroup="DriverAdd"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <div class="row">
                                                                                            <div class="form-group">
                                                                                                <div class="col-md-12" style="color: black; font-weight: normal">
                                                                                                    <label runat="server" id="Label80" class="col-md-4 control-label  ">
                                                                                                        <asp:Label ID="Label81" runat="server" Text="Driver name Franch" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                        <span class="required">* </span>
                                                                                                    </label>
                                                                                                    <div class="col-md-8">
                                                                                                        <asp:TextBox ID="txtDriverFranch" runat="server" CssClass="form-control" AutoPostBack="true" placeholder="Driver Name Franch"></asp:TextBox>
                                                                                                        <asp:Label ID="lblDriverFranch" runat="server" Visible="false" ForeColor="Red" Text=""></asp:Label>
                                                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator27" runat="server" ErrorMessage=" Requaird Driver Name Franch" ControlToValidate="txtDriverFranch" ValidationGroup="DriverAdd"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <div class="row">
                                                                                            <div class="form-group">
                                                                                                <div class="col-md-12" style="color: black; font-weight: normal">
                                                                                                    <label runat="server" id="Label82" class="col-md-4 control-label  ">
                                                                                                        <asp:Label ID="Label83" runat="server" Text="Mobile No" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                        <span class="required">* </span>
                                                                                                    </label>
                                                                                                    <div class="col-md-8">
                                                                                                        <asp:TextBox ID="txtDriverMobile" runat="server" CssClass="form-control" AutoPostBack="true" AutoCompleteType="Disabled" placeholder="Mobile No"></asp:TextBox>
                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" TargetControlID="txtDriverMobile" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator28" runat="server" ErrorMessage=" Requaird Mobile No" ControlToValidate="txtDriverMobile" ValidationGroup="DriverAdd"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-md-12">
                                                                                        <div class="row">
                                                                                            <div class="form-group">
                                                                                                <div class="col-md-12" style="color: black; font-weight: normal">
                                                                                                    <label runat="server" id="Label100" class="col-md-4 control-label  ">
                                                                                                        <asp:Label ID="Label101" runat="server" Text="DeviceID" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                        <span class="required">* </span>
                                                                                                    </label>
                                                                                                    <div class="col-md-8">
                                                                                                        <asp:TextBox ID="txtDeviceID" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" placeholder="DeviceID"></asp:TextBox>
                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" TargetControlID="txtDeviceID" ValidChars="0123456789," FilterType="Custom, Numbers" runat="server" Enabled="True" />
                                                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator32" runat="server" ErrorMessage=" Requaird DeviceID" ControlToValidate="txtDeviceID" ValidationGroup="DriverAdd"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-md-12">
                                                                                        <div class="row">
                                                                                            <div class="form-group">
                                                                                                <div class="col-md-12" style="color: black; font-weight: normal">
                                                                                                    <label runat="server" id="Label84" class="col-md-4 control-label  ">
                                                                                                        <asp:Label ID="Label85" runat="server" Text="User ID" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                        <span class="required">* </span>
                                                                                                    </label>
                                                                                                    <div class="col-md-8">
                                                                                                        <asp:TextBox ID="txtdriverUserID" runat="server" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" placeholder="User ID"></asp:TextBox>
                                                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator29" runat="server" ErrorMessage=" Requaird User ID" ControlToValidate="txtdriverUserID" ValidationGroup="DriverAdd"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="col-md-12">
                                                                                        <div class="row">
                                                                                            <div class="form-group">
                                                                                                <div class="col-md-12" style="color: black; font-weight: normal">
                                                                                                    <label runat="server" id="Label86" class="col-md-4 control-label  ">
                                                                                                        <asp:Label ID="Label87" runat="server" Text="Password" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                        <span class="required">* </span>
                                                                                                    </label>
                                                                                                    <div class="col-md-8">
                                                                                                        <asp:TextBox ID="txtdriverpass" runat="server" TextMode="Password" CssClass="form-control" AutoCompleteType="Disabled" AutoPostBack="true" placeholder="Password"></asp:TextBox>
                                                                                                        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator30" runat="server" ErrorMessage=" Requaird Password" ControlToValidate="txtdriverpass" ValidationGroup="DriverAdd"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>


                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="txtDriverEng" EventName="TextChanged" />
                                                                                    <%--<asp:AsyncPostBackTrigger ControlID="SetDay2" EventName="TextChanged" />--%>
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>

                                                                            <div class="modal-footer">
                                                                                <asp:LinkButton ID="lnkbtndriveradd" class="btn green-haze modalBackgroundbtn-circle" ValidationGroup="DriverAdd" runat="server" OnClick="lnkbtndriveradd_Click"> Save</asp:LinkButton>
                                                                                <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                <asp:Button ID="btncanadddriver" runat="server" class="btn btn-default" Text="Cancel" />
                                                                            </div>



                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                            <cc1:ModalPopupExtender ID="ModalPopupExtender10" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="btncanadddriver" Enabled="True" PopupControlID="PanelAddDriver" TargetControlID="btnAddDriver"></cc1:ModalPopupExtender>





                                        </div>

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


                                                    <%-- <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                        <ContentTemplate>--%>


                                                    <div class="form-body">
                                                        <div class="row">

                                                            <div class="col-md-4">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCustomer1s" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCustomer1s" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                    <asp:Label ID="lblCID" runat="server" CssClass="col-md-1 control-label" ForeColor="Green" Font-Bold="true" Font-Size="Medium" Text="0"></asp:Label>
                                                                    <div class="col-md-8">
                                                                        <asp:DropDownList ID="drpCustomer" runat="server" CssClass="form-control select2me input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpCustomer_SelectedIndexChanged"></asp:DropDownList>
                                                                        <asp:LinkButton ID="lnkbtn1" runat="server"></asp:LinkButton>
                                                                        <asp:Panel ID="PanelContract" runat="server" CssClass="modalPopup" Style="padding: 1px; background-color: #fff; border: 2px solid pink; display: none; overflow: auto;">

                                                                            <div class="row" style="position: fixed; left: 10%; top: 4%; width: 60%">
                                                                                <div class="col-md-12">
                                                                                    <div class="portlet box purple">
                                                                                        <div class="portlet-title">
                                                                                            <div class="caption">
                                                                                                <i class="fa fa-globe"></i>
                                                                                                Contract
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="portlet-body">
                                                                                            <div class="tabbable">
                                                                                                <div class="tab-content no-space">
                                                                                                    <div class="form-body">


                                                                                                        <table class="table table-striped table-hover table-bordered">
                                                                                                            <thead>
                                                                                                                <tr>

                                                                                                                    <th style="width: 60px;">ACTION</th>
                                                                                                                    <th style="width: 7%;">
                                                                                                                        <asp:Label runat="server" ID="Label89" Text="ID#"></asp:Label></th>
                                                                                                                    <th style="width: 8%;">
                                                                                                                        <asp:Label runat="server" ID="Label90" Text="Ctrct ID#"></asp:Label></th>
                                                                                                                    <th>
                                                                                                                        <asp:Label runat="server" ID="Label91" Text="Customer"></asp:Label></th>
                                                                                                                    <th>
                                                                                                                        <asp:Label runat="server" ID="Label92" Text="Plan"></asp:Label></th>
                                                                                                                    <th>
                                                                                                                        <asp:Label runat="server" ID="Label93" Visible="false" Text="Day"></asp:Label>
                                                                                                                        <asp:Label runat="server" ID="Label94" Text="Meal"></asp:Label></th>
                                                                                                                    <th>
                                                                                                                        <asp:Label runat="server" ID="Label95" Text="Start Date"></asp:Label></th>
                                                                                                                    <th>
                                                                                                                        <asp:Label runat="server" ID="Label96" Text="End Date"></asp:Label></th>
                                                                                                                    <th>
                                                                                                                        <asp:Label runat="server" ID="Label97" Text="Days"></asp:Label></th>
                                                                                                                    <th>
                                                                                                                        <asp:Label runat="server" ID="Label98" Text="Delivered"></asp:Label></th>
                                                                                                                    <th>
                                                                                                                        <asp:Label runat="server" ID="Label99" Text="Pending"></asp:Label></th>
                                                                                                                </tr>
                                                                                                            </thead>
                                                                                                            <tbody>
                                                                                                                <asp:ListView ID="ListViewContract" runat="server" OnItemCommand="ListViewContract_ItemCommand" OnItemDataBound="ListViewContract_ItemDataBound">
                                                                                                                    <LayoutTemplate>
                                                                                                                        <tr id="ItemPlaceholder" runat="server">
                                                                                                                        </tr>
                                                                                                                    </LayoutTemplate>
                                                                                                                    <ItemTemplate>
                                                                                                                        <tr>

                                                                                                                            <td>
                                                                                                                                <table>
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <asp:LinkButton ID="btnEditContract" CommandName="btnEditContract" CommandArgument='<%# Eval("CustomerID")+","+Eval("planid")+","+Eval("DeliveryMeal")+","+Eval("MYTRANSID")+","+Eval("DeliveryID") %>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom" OnClientClick="showProgress()"><i class="fa fa-pencil"></i></asp:LinkButton>

                                                                                                                                        </td>
                                                                                                                                        <td>
                                                                                                                                            <asp:LinkButton ID="btnCopyContract" CommandName="btnCopyContract" CommandArgument='<%# Eval("CustomerID")+","+Eval("planid")+","+Eval("DeliveryMeal")+","+Eval("MYTRANSID")+","+Eval("DeliveryID") %>' runat="server" class="btn btn-sm red filter-cancel" OnClientClick="showProgress()"><i class="fa fa-copy"></i></asp:LinkButton>

                                                                                                                                        </td>

                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("CustomerID") %>'></asp:Label>
                                                                                                                            </td>

                                                                                                                            <td>
                                                                                                                                <asp:Label ID="Label14" runat="server" Text='<%# Eval("ContractID") %>'></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="lblCustomer" runat="server" Text='<%# GetCustomer(Convert.ToInt32( Eval("CustomerID")))%>'></asp:Label>
                                                                                                                                <asp:Label ID="lblMYTRANSID" runat="server" Visible="false" Text='<%# Eval("MYTRANSID")%>'></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="lblPlan" runat="server" Text='<%# GetPlan(Convert.ToInt32( Eval("planid")))%>'></asp:Label></td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="lblMeal" runat="server" Text='<%# GetMeal1(Convert.ToInt32( Eval("DeliveryMeal")))%>'></asp:Label>
                                                                                                                                <%--<asp:Label ID="lblMeal" runat="server" Text='<%# GetMeal(Convert.ToInt32( Eval("planid")),Convert.ToInt32(Eval("MYTRANSID")))%>'></asp:Label>--%>

                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="Label69" runat="server" Text='<%# getStartDate(Convert.ToDateTime(Eval("StartDate"))) %>'></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="Label72" runat="server" Text='<%# getEndDate(Convert.ToDateTime(Eval("EndDate"))) %>'></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="Label73" runat="server" Text='<%# getTotaldel(Convert.ToInt32(Eval("MYTRANSID"))) %>'></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="Label74" runat="server" Text='<%# getTotaldeldone(Convert.ToInt32(Eval("MYTRANSID"))) %>'></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:Label ID="Label75" runat="server" Text='<%# getdeliverd(Convert.ToInt32(Eval("MYTRANSID"))) %>'></asp:Label>
                                                                                                                            </td>

                                                                                                                        </tr>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:ListView>

                                                                                                            </tbody>
                                                                                                        </table>


                                                                                                        <div class="modal-footer">

                                                                                                            <asp:Button ID="ContractCancel" runat="server" class="btn btn-default" Text="Cancel" />
                                                                                                        </div>



                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </asp:Panel>
                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtenderContract" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="ContractCancel" Enabled="True" PopupControlID="PanelContract" TargetControlID="lnkbtn1"></cc1:ModalPopupExtender>




                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCustomer2h" CssClass="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCustomer2h" CssClass="col-md-3 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblPlan1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPlan1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpPlan" runat="server" CssClass="form-control select2me input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpPlan_SelectedIndexChanged"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorPlan" runat="server" ErrorMessage="Plan Required." ControlToValidate="drpPlan" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPlan2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPlan2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-4">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblInvoiceNumber1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtInvoiceNumber1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-6">
                                                                        <asp:TextBox ID="txtInvoiceNumber" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceNumber" runat="server" ControlToValidate="txtInvoiceNumber" ErrorMessage="Invoice Number Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <%--<div class="col-md-2">
                                                                        <asp:Label runat="server" ID="lblInvoice" class="col-md-4 control-label" Text="12345"></asp:Label>
                                                                    </div>--%>

                                                                    <asp:Label runat="server" ID="lblInvoiceNumber2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtInvoiceNumber2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label ID="Label8" runat="server" class="col-md-4 control-label" Text="Delivery Time"></asp:Label>
                                                                    <div class="col-md-7">
                                                                        <asp:Label ID="lblDeliveryTIME" runat="server" CssClass="col-md-1 control-label" ForeColor="Green" Font-Bold="true" Font-Size="Medium" Text="0"></asp:Label>
                                                                        <asp:Label ID="Label88" runat="server" CssClass="col-md-10 control-label" Font-Size="Medium" Text="times in a day"></asp:Label>
                                                                        <asp:Label ID="lblDeliveryTIMEID" runat="server" CssClass="col-md-1 control-label" Visible="false" Font-Size="Medium" Text="times in a day"></asp:Label>
                                                                        <%--<asp:DropDownList ID="drpDeliveryTime" CssClass="form-control select2me input-medium" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpDeliveryTime_SelectedIndexChanged"></asp:DropDownList>--%>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label ID="Label10" runat="server" class="col-md-4 control-label" Text="Contract Date"></asp:Label>
                                                                    <div class="col-md-7">
                                                                        <asp:TextBox ID="txtExpDate" runat="server" AutoPostBack="true" OnTextChanged="txtExpDate_TextChanged" CssClass="form-control" placeholder="MM/dd/yyyy"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtExpDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-4" style="display: none">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblMeal1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMeal1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpMeal" runat="server" CssClass="form-control select2me input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpMeal_SelectedIndexChanged"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblMeal2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMeal2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="Label6" class="col-md-4 control-label" Text=" Expected Driver"></asp:Label>
                                                                    <div class="col-md-5">
                                                                        <asp:DropDownList ID="drpExpecteddriver" runat="server" CssClass="form-control select2me input-small"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator13" runat="server" ErrorMessage="Driver Required." ControlToValidate="drpExpecteddriver" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:Button ID="btnChengeDriver" runat="server" class="btn btn-sm red" Text="Change" />

                                                                        <asp:Panel ID="PanelDriver" runat="server" CssClass="modalPopup" Style="padding: 1px; background-color: #fff; border: 2px solid pink; display: none; overflow: auto;">

                                                                            <div class="row" style="position: fixed; left: 20%; top: 4%; width: auto">
                                                                                <div class="col-md-12">
                                                                                    <div class="portlet box purple">
                                                                                        <div class="portlet-title">
                                                                                            <div class="caption">
                                                                                                <i class="fa fa-globe"></i>
                                                                                                Change Driver
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="portlet-body">
                                                                                            <div class="tabbable">
                                                                                                <div class="tab-content no-space">
                                                                                                    <div class="form-body">
                                                                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                                                                            <ContentTemplate>
                                                                                                                <div class="form-group">

                                                                                                                    <div class="col-md-6">
                                                                                                                        <label runat="server" id="Label66" class="col-md-4 control-label  ">
                                                                                                                            <asp:Label ID="Label67" runat="server" Text="Driver" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                            <span class="required">* </span>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-8">
                                                                                                                            <asp:DropDownList ID="drpChangeDriver" runat="server" CssClass="form-control input-medium"></asp:DropDownList>
                                                                                                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator23" runat="server" ErrorMessage="Driver Required." ControlToValidate="drpChangeDriver" InitialValue="0" ValidationGroup="DriverChange"></asp:RequiredFieldValidator>

                                                                                                                        </div>
                                                                                                                    </div>

                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                        <label runat="server" id="Label60" class="col-md-3 control-label  ">
                                                                                                                            <asp:Label ID="Label64" runat="server" Text="Select Date" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                            <span class="required">* </span>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-6">
                                                                                                                            <asp:TextBox ID="txtDriverChangeDate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDriverChangeDate_TextChanged" placeholder="MM/dd/yyyy"></asp:TextBox>
                                                                                                                            <asp:Label ID="lblChangeDridate" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender10" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDriverChangeDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator22" runat="server" ErrorMessage="Select Date" ControlToValidate="txtDriverChangeDate" ValidationGroup="DriverChange"></asp:RequiredFieldValidator>
                                                                                                                        </div>

                                                                                                                        <div class="col-md-3">
                                                                                                                            <asp:CheckBox ID="chkAll" runat="server" Checked="false" />Rest All

                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>

                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                            <Triggers>
                                                                                                                <asp:AsyncPostBackTrigger ControlID="txtDriverChangeDate" EventName="TextChanged" />
                                                                                                                <%--<asp:AsyncPostBackTrigger ControlID="SetDay2" EventName="TextChanged" />--%>
                                                                                                            </Triggers>
                                                                                                        </asp:UpdatePanel>

                                                                                                        <div class="modal-footer">
                                                                                                            <asp:LinkButton ID="lnkbtnChangeDriver" class="btn green-haze modalBackgroundbtn-circle" ValidationGroup="DriverChange" runat="server" OnClick="lnkbtnChangeDriver_Click"> Save</asp:LinkButton>
                                                                                                            <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                            <asp:Button ID="btncandriver" runat="server" class="btn btn-default" Text="Cancel" />
                                                                                                        </div>



                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </asp:Panel>
                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender8" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="btncandriver" Enabled="True" PopupControlID="PanelDriver" TargetControlID="btnChengeDriver"></cc1:ModalPopupExtender>


                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <%--<div class="col-md-6" style="display: none">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblBlank1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBlank1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtBlank" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblBlank2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBlank2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>--%>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-8">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label7" class="col-md-2 control-label" Text="Week of Day"></asp:Label>
                                                                        <div class="col-md-2">
                                                                            <asp:DropDownList ID="drpweekofday" runat="server" CssClass="table-group-action-input form-control input-xsmall" AutoPostBack="true" OnSelectedIndexChanged="drpweekofday_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0" Text="-- Select --"></asp:ListItem>
                                                                                <asp:ListItem Value="1" Text="Sat"></asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="Sun"></asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="Mon"></asp:ListItem>
                                                                                <asp:ListItem Value="4" Text="Tue"></asp:ListItem>
                                                                                <asp:ListItem Value="5" Text="Wed"></asp:ListItem>
                                                                                <asp:ListItem Value="6" Text="Thu"></asp:ListItem>
                                                                            </asp:DropDownList>

                                                                            <%--<asp:ListBox ID="drpweekofday1" runat="server" CssClass="dropdown-checkboxes form-control" SelectionMode="Multiple">
                                                                                <asp:ListItem Value="1" Text="Sat"></asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="Sun"></asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="Mon"></asp:ListItem>
                                                                                <asp:ListItem Value="4" Text="Tue"></asp:ListItem>
                                                                                <asp:ListItem Value="5" Text="Wed"></asp:ListItem>
                                                                                <asp:ListItem Value="6" Text="Thu"></asp:ListItem>
                                                                            </asp:ListBox>--%>
                                                                        </div>
                                                                        <%--<div class="col-md-2">
                                                                            <asp:Button ID="btnWeekofDay" CssClass="btn red" runat="server" OnClick="btnWeekofDay_Click" Text="Set" />
                                                                        </div>--%>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="tags_1" MaxLength="100" name="number" runat="server" CssClass="form-control tags" meta:resourcekey="tags_1Resource1"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblTotalPlanPrice1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalPlanPrice1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="txtTotalPlanPrice" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblTotalPlanPrice2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalPlanPrice2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>


                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">

                                                                <div class="col-md-4">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblBeingDate1s" class="col-md-3 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBeingDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-7">
                                                                            <asp:TextBox ID="txtBeingDate" runat="server" CssClass="form-control" placeholder="MM/dd/yyyy" AutoPostBack="true" OnTextChanged="txtBeingDate_TextChanged"></asp:TextBox>
                                                                            <asp:Label ID="lblMSGDate" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                            <cc1:CalendarExtender ID="TextBoxFromDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtBeingDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorBeingDate" runat="server" ControlToValidate="txtBeingDate" ErrorMessage="Being Date Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblBeingDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtBeingDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnDateSet">
                                                                        <div class="col-md-12">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="Label42" class="col-md-4 control-label" Text="Enter Day"></asp:Label>
                                                                                <div class="col-md-6">
                                                                                    <asp:TextBox ID="txtDelTotalDay" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" TargetControlID="txtDelTotalDay" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                    <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator20" runat="server" ErrorMessage="Enter Total Delivery Day" ControlToValidate="txtDelTotalDay" ValidationGroup="DelTotalDay"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <asp:Button ID="btnDateSet" CssClass="btn red" runat="server" OnClick="btnDateSet_Click" Text="Set" ValidationGroup="txtDelTotalDay" />
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>


                                                                <div class="col-md-4">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblEndDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEndDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-4">
                                                                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" placeholder="MM/dd/yyyy" AutoPostBack="true" OnTextChanged="txtEndDate_TextChanged"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtEndDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                            <asp:Label ID="lblMSGEdate" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:TextBox ID="txtTotalWeek" Enabled="false" runat="server" CssClass="form-control" placeholder="Total Week"></asp:TextBox>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblEndDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtEndDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-4" style="display: none">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="lblDay1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDay1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        <div class="col-md-8">
                                                                            <asp:DropDownList ID="drpDay" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drpDay_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0" Text="-- Select --"></asp:ListItem>
                                                                                <asp:ListItem Value="1" Text="1.Saturday"></asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="2.Sunday"></asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="3.Monday"></asp:ListItem>
                                                                                <asp:ListItem Value="4" Text="4.Tusday"></asp:ListItem>
                                                                                <asp:ListItem Value="5" Text="5.Wednesday"></asp:ListItem>
                                                                                <asp:ListItem Value="6" Text="6.Thursday"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <asp:Label runat="server" ID="lblDay2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDay2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-8" style="margin-left: -10px;">
                                                                    <div class="form-group" style="color: ">
                                                                        <div class="col-md-3">
                                                                            <asp:Label runat="server" ID="lblTotalDeliveryDay1s" class="col-md-6 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalDeliveryDay1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-6">
                                                                                <asp:TextBox ID="txtTotalDeliveryDay" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>

                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblTotalDeliveryDay2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTotalDeliveryDay2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <asp:Label ID="Label65" class="col-md-6 control-label" runat="server" Text="Delivered"></asp:Label>
                                                                            <div class="col-md-6">
                                                                                <asp:TextBox ID="txtDelivered" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <asp:Label ID="Label56" class="col-md-6 control-label" runat="server" Text="Remaining"></asp:Label>
                                                                            <div class="col-md-6">
                                                                                <asp:TextBox ID="txtRemainingDay" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>


                                                                    </div>
                                                                </div>



                                                                <div class="col-md-4">
                                                                    <div class="actions btn-set">
                                                                        <asp:Button ID="btnCopyFullPlan" runat="server" class="btn btn-sm green" Text="Copy Full Plan" OnClientClick="showProgress()" OnClick="btnCopyFullPlan_Click" />
                                                                        <asp:Button ID="Button3" Enabled="false" runat="server" class="btn btn-sm blue" Text="Refund" />
                                                                        <asp:Button ID="btnRecalculate" runat="server" class="btn btn-sm purple" OnClientClick="showProgress()" OnClick="btnRecalculate_Click" Text="Recl Days" />
                                                                        <asp:Button ID="btnOnHold" runat="server" class="btn btn-sm red" Text="Hold" />

                                                                        <asp:Panel ID="PenelONHOLD" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto">

                                                                            <div class="row" style="position: fixed; left: 20%; top: 4%; width: auto">
                                                                                <div class="col-md-12">
                                                                                    <div class="portlet box purple">
                                                                                        <div class="portlet-title">
                                                                                            <div class="caption">
                                                                                                <i class="fa fa-globe"></i>
                                                                                                Hold Plan
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="portlet-body">
                                                                                            <div class="tabbable">
                                                                                                <div class="tab-content no-space">
                                                                                                    <div class="form-body">
                                                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                                                            <ContentTemplate>
                                                                                                                <div class="form-group">
                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                        <label runat="server" id="Label58" class="col-md-4 control-label  ">
                                                                                                                            <asp:Label ID="Label59" runat="server" Text="Hold Date" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                            <span class="required">* </span>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-8">
                                                                                                                            <asp:TextBox ID="txtHoldDate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtHoldDate_TextChanged" placeholder="MM/dd/yyyy"></asp:TextBox>
                                                                                                                            <asp:Label ID="lblHoadDate" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender9" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtHoldDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator21" runat="server" ErrorMessage="Select Date" ControlToValidate="txtHoldDate" ValidationGroup="HoldDate"></asp:RequiredFieldValidator>
                                                                                                                        </div>
                                                                                                                    </div>

                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                        <label runat="server" id="Label61" class="col-md-4 control-label  ">
                                                                                                                            <asp:Label ID="Label62" runat="server" Text="Remark" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                            <span class="required">* </span>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-8">
                                                                                                                            <asp:TextBox ID="txtHoldRemark" runat="server" CssClass="form-control" placeholder="Remark"></asp:TextBox>

                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                            <Triggers>
                                                                                                                <asp:AsyncPostBackTrigger ControlID="txtHoldDate" EventName="TextChanged" />
                                                                                                                <%--<asp:AsyncPostBackTrigger ControlID="SetDay2" EventName="TextChanged" />--%>
                                                                                                            </Triggers>
                                                                                                        </asp:UpdatePanel>

                                                                                                        <div class="modal-footer">
                                                                                                            <asp:LinkButton ID="lnkbtnhold" class="btn green-haze modalBackgroundbtn-circle" ValidationGroup="HoldDate" runat="server" OnClick="lnkbtnhold_Click"> Hold</asp:LinkButton>
                                                                                                            <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                            <asp:Button ID="btnHoldCancel" runat="server" class="btn btn-default" Text="Cancel" />

                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </asp:Panel>
                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender7" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="btnHoldCancel" Enabled="True" PopupControlID="PenelONHOLD" TargetControlID="btnOnHold"></cc1:ModalPopupExtender>

                                                                        <asp:Button ID="btnunhold" Visible="false" runat="server" class="btn btn-sm green" Text="UnHold" />

                                                                        <asp:Panel ID="PenelUnHOLD" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto">

                                                                            <div class="row" style="position: fixed; left: 20%; top: 4%; width: auto">
                                                                                <div class="col-md-12">
                                                                                    <div class="portlet box purple">
                                                                                        <div class="portlet-title">
                                                                                            <div class="caption">
                                                                                                <i class="fa fa-globe"></i>
                                                                                                UnHold Plan
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="portlet-body">
                                                                                            <div class="tabbable">
                                                                                                <div class="tab-content no-space">
                                                                                                    <div class="form-body">
                                                                                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                                                                            <ContentTemplate>
                                                                                                                <div class="form-group">
                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                        <label runat="server" id="Label57" class="col-md-4 control-label  ">
                                                                                                                            <asp:Label ID="Label68" runat="server" Text="UnHold Date" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                            <span class="required">* </span>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-8">
                                                                                                                            <asp:TextBox ID="txtUnHoldDate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtUnHoldDate_TextChanged" placeholder="MM/dd/yyyy"></asp:TextBox>
                                                                                                                            <asp:Label ID="lblUnholdMsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender11" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtUnHoldDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator24" runat="server" ErrorMessage="Select Date" ControlToValidate="txtUnHoldDate" ValidationGroup="UnHoldDate"></asp:RequiredFieldValidator>
                                                                                                                        </div>
                                                                                                                    </div>

                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                        <label runat="server" id="Label70" class="col-md-4 control-label  ">
                                                                                                                            <asp:Label ID="Label71" runat="server" Text="Remark" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                            <span class="required">* </span>
                                                                                                                        </label>
                                                                                                                        <div class="col-md-8">
                                                                                                                            <asp:TextBox ID="txtUnHoldRemark" runat="server" CssClass="form-control" placeholder="Remark"></asp:TextBox>

                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                            <Triggers>
                                                                                                                <asp:AsyncPostBackTrigger ControlID="txtHoldDate" EventName="TextChanged" />
                                                                                                                <%--<asp:AsyncPostBackTrigger ControlID="SetDay2" EventName="TextChanged" />--%>
                                                                                                            </Triggers>
                                                                                                        </asp:UpdatePanel>

                                                                                                        <div class="modal-footer">
                                                                                                            <asp:LinkButton ID="lnkbtnUnHold" class="btn green-haze modalBackgroundbtn-circle" ValidationGroup="UnHoldDate" runat="server" OnClick="lnkbtnUnHold_Click"> UnHold</asp:LinkButton>
                                                                                                            <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                            <asp:Button ID="btnUnHoldCancel" runat="server" class="btn btn-default" Text="Cancel" />

                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </asp:Panel>
                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender9" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="btnUnHoldCancel" Enabled="True" PopupControlID="PenelUnHOLD" TargetControlID="btnunhold"></cc1:ModalPopupExtender>


                                                                        <%--is button to calculate today days selected--%>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-4" style="display: none;">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label4" class="col-md-6 control-label" Text="How many days in Week"></asp:Label>
                                                                        <div class="col-md-4">
                                                                            <%--<asp:TextBox ID="txtHowManyDay" runat="server" Enabled="false" CssClass="form-control" Text="2"></asp:TextBox>--%>
                                                                            <asp:DropDownList ID="drpHowManyDay" runat="server" CssClass="table-group-action-input form-control input-xsmall">
                                                                                <%--<asp:ListItem Value="0" Text="0"></asp:ListItem>--%>
                                                                                <%-- <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                                            <asp:ListItem Value="6" Text="6"></asp:ListItem>--%>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                                                <div class="portlet box green">
                                                                    <div class="portlet-title">
                                                                        <div class="caption">
                                                                            <i class="fa fa-edit"></i>Product List
                                                                        </div>
                                                                        <div class="tools">
                                                                            <a href="javascript:;" class="collapse"></a>
                                                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                                            <a href="javascript:;" class="reload"></a>
                                                                            <a href="javascript:;" class="remove"></a>
                                                                        </div>
                                                                        <div>
                                                                            <div class="col-md-7">
                                                                                <div class="form-group" style="margin-top: 1px; margin-top: 4px; margin-left: 10px;">
                                                                                    <asp:Label ID="Label9" runat="server" class="col-md-2 control-label" Text="Delivery Meal"></asp:Label>
                                                                                    <div class="col-md-5">
                                                                                        <asp:DropDownList ID="drpDeliveryMeal" CssClass="form-control select2me input-medium" AutoPostBack="true" onchange="showProgress()" OnSelectedIndexChanged="drpDeliveryMeal_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-md-5">
                                                                                        <asp:Button ID="btnAddmeal" Visible="false" runat="server" class="btn blue-steel" Text="Add Meal" OnClick="btnAddmeal_Click" />
                                                                                        <asp:Label ID="lbldeltime" runat="server" Text=""></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="actions btn-set">
                                                                            <asp:Button ID="btnAdd" runat="server" class="btn blue-steel btn-circle" ValidationGroup="submit" Text="Add New" OnClick="btnAdd_Click" OnClientClick="showProgress()" />
                                                                            <%-- OnClientClick="showProgress()"--%>
                                                                            <asp:Button ID="btnCancel" runat="server" class="btn red-flamingo btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                                                        </div>
                                                                    </div>


                                                                    <div class="portlet-body">



                                                                        <asp:Panel ID="pnlErrorMsg1" runat="server" Visible="false">
                                                                            <div class="alert alert-danger alert-dismissable">
                                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                                                                                <asp:Label ID="lblerrorMsg1" runat="server"></asp:Label>
                                                                            </div>
                                                                        </asp:Panel>


                                                                        <table class="table table-striped table-hover table-bordered">
                                                                            <thead>

                                                                                <tr style="border: 1px solid gray;">
                                                                                    <th style="text-align: right;">Week</th>
                                                                                    <th colspan="6">
                                                                                        <asp:DropDownList ID="DrpWeek" Style="" AutoPostBack="true" class="form-control select2me input-medium" runat="server" onchange="showProgress()" OnSelectedIndexChanged="DrpWeek_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DrpWeek" InitialValue="0" ErrorMessage="Week Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                    </th>
                                                                                    <th></th>
                                                                                    <th></th>

                                                                                </tr>
                                                                                <tr style="border: 1px solid gray; background: #01C501 none repeat scroll 0 0; color: #ffffff; font-weight: bold">

                                                                                    <th style="text-align: right; width: 30%">Date</th>
                                                                                    <th style="width: 5%">
                                                                                        <asp:Label ID="lblDate1" runat="server" Text=""></asp:Label>
                                                                                        <asp:Label ID="lblFDate1" Visible="false" runat="server" Text=""></asp:Label>
                                                                                        <asp:Button ID="btnDateset1" CssClass="btn red" Visible="false" runat="server" Text="Set" />

                                                                                        <asp:Panel ID="pnlsetdate1" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto">

                                                                                            <div class="row" style="position: fixed; left: 20%; top: 4%; width: auto">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="portlet box purple">
                                                                                                        <div class="portlet-title">
                                                                                                            <div class="caption">
                                                                                                                <i class="fa fa-globe"></i>
                                                                                                                Set Date
                                                                                                            </div>
                                                                                                        </div>

                                                                                                        <div class="portlet-body">
                                                                                                            <div class="tabbable">
                                                                                                                <div class="tab-content no-space">
                                                                                                                    <div class="form-body">
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="form-group">
                                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                                        <label runat="server" id="lblrecod" class="col-md-4 control-label  ">
                                                                                                                                            <asp:Label ID="Label63" runat="server" Text="Set Date" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                                            <span class="required">* </span>
                                                                                                                                        </label>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <asp:TextBox ID="txtdate1" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtdate1_TextChanged" placeholder="MM/dd/yyyy"></asp:TextBox>
                                                                                                                                            <asp:Label ID="lblmdate1" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdate1" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator18" runat="server" ErrorMessage="Select Date" ControlToValidate="txtdate1" ValidationGroup="txtdate11"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </div>

                                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                                        <label runat="server" id="Label29" class="col-md-4 control-label  ">
                                                                                                                                            <asp:Label ID="Label30" runat="server" Text="Set Delivery Day" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                                            <span class="required">* </span>
                                                                                                                                        </label>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <asp:TextBox ID="SetDay1" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="SetDay1_TextChanged" placeholder="Day Number"></asp:TextBox>
                                                                                                                                            <asp:Label ID="lblSetDay1" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="SetDay1" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter Day Number" ControlToValidate="SetDay1" ValidationGroup="txtdate11"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </div>

                                                                                                                                </div>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <asp:AsyncPostBackTrigger ControlID="txtdate1" EventName="TextChanged" />
                                                                                                                                <%--<asp:AsyncPostBackTrigger ControlID="SetDay1" EventName="TextChanged" />--%>
                                                                                                                            </Triggers>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                        <div class="modal-footer">
                                                                                                                            <asp:LinkButton ID="setdate1" class="btn green-haze modalBackgroundbtn-circle" ValidationGroup="txtdate11" runat="server" OnClick="setdate1_Click"> Set</asp:LinkButton>
                                                                                                                            <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                            <asp:Button ID="btncancle1" runat="server" class="btn btn-default" Text="Cancel" />

                                                                                                                        </div>

                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>

                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                        </asp:Panel>
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="btncancle1" Enabled="True" PopupControlID="pnlsetdate1" TargetControlID="btnDateset1"></cc1:ModalPopupExtender>

                                                                                    </th>
                                                                                    <th style="width: 5%">
                                                                                        <asp:Label ID="lblDate2" runat="server" Text=""></asp:Label>
                                                                                        <asp:Label ID="lblFDate2" Visible="false" runat="server" Text=""></asp:Label>
                                                                                        <asp:Button ID="btnDateset2" CssClass="btn red" Visible="false" runat="server" Text="Set" />

                                                                                        <asp:Panel ID="pnlsetdate2" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto">

                                                                                            <div class="row" style="position: fixed; left: 20%; top: 4%; width: auto">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="portlet box purple">
                                                                                                        <div class="portlet-title">
                                                                                                            <div class="caption">
                                                                                                                <i class="fa fa-globe"></i>
                                                                                                                Set Date
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="portlet-body">
                                                                                                            <div class="tabbable">
                                                                                                                <div class="tab-content no-space">
                                                                                                                    <div class="form-body">
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="form-group">
                                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                                        <label runat="server" id="Label15" class="col-md-4 control-label  ">
                                                                                                                                            <asp:Label ID="Label16" runat="server" Text="Set Date" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                                            <span class="required">* </span>
                                                                                                                                        </label>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <asp:TextBox ID="txtdate2" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtdate2_TextChanged" placeholder="MM/dd/yyyy"></asp:TextBox>
                                                                                                                                            <asp:Label ID="lblmdate2" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdate2" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Date" ControlToValidate="txtdate2" ValidationGroup="txtdate21"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </div>

                                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                                        <label runat="server" id="Label31" class="col-md-4 control-label  ">
                                                                                                                                            <asp:Label ID="Label32" runat="server" Text="Set Delivery Day" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                                            <span class="required">* </span>
                                                                                                                                        </label>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <asp:TextBox ID="SetDay2" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="SetDay2_TextChanged" placeholder="Day Number"></asp:TextBox>
                                                                                                                                            <asp:Label ID="lblSetDay2" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="SetDay2" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator8" runat="server" ErrorMessage="Enter Day Number" ControlToValidate="SetDay2" ValidationGroup="txtdate21"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <asp:AsyncPostBackTrigger ControlID="txtdate2" EventName="TextChanged" />
                                                                                                                                <%--<asp:AsyncPostBackTrigger ControlID="SetDay2" EventName="TextChanged" />--%>
                                                                                                                            </Triggers>
                                                                                                                        </asp:UpdatePanel>

                                                                                                                        <div class="modal-footer">
                                                                                                                            <asp:LinkButton ID="setdate2" class="btn green-haze modalBackgroundbtn-circle" ValidationGroup="txtdate21" runat="server" OnClick="setdate2_Click"> Set</asp:LinkButton>
                                                                                                                            <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                            <asp:Button ID="btncancle2" runat="server" class="btn btn-default" Text="Cancel" />

                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                        </asp:Panel>
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="btncancle2" Enabled="True" PopupControlID="pnlsetdate2" TargetControlID="btnDateset2"></cc1:ModalPopupExtender>

                                                                                    </th>
                                                                                    <th style="width: 5%">
                                                                                        <asp:Label ID="lblDate3" runat="server" Text=""></asp:Label>
                                                                                        <asp:Label ID="lblFDate3" Visible="false" runat="server" Text=""></asp:Label>
                                                                                        <asp:Button ID="btnDateset3" CssClass="btn red" Visible="false" runat="server" Text="Set" />

                                                                                        <asp:Panel ID="pnlsetdate3" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto">

                                                                                            <div class="row" style="position: fixed; left: 20%; top: 4%; width: auto">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="portlet box purple">
                                                                                                        <div class="portlet-title">
                                                                                                            <div class="caption">
                                                                                                                <i class="fa fa-globe"></i>
                                                                                                                Set Date
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="portlet-body">
                                                                                                            <div class="tabbable">
                                                                                                                <div class="tab-content no-space">
                                                                                                                    <div class="form-body">
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="form-group">
                                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                                        <label runat="server" id="Label21" class="col-md-4 control-label  ">
                                                                                                                                            <asp:Label ID="Label22" runat="server" Text="Set Date" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                                            <span class="required">* </span>
                                                                                                                                        </label>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <asp:TextBox ID="txtdate3" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtdate3_TextChanged" placeholder="MM/dd/yyyy"></asp:TextBox>
                                                                                                                                            <asp:Label ID="lblmdate3" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdate3" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select Date" ControlToValidate="txtdate3" ValidationGroup="txtdate31"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </div>

                                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                                        <label runat="server" id="Label33" class="col-md-4 control-label  ">
                                                                                                                                            <asp:Label ID="Label34" runat="server" Text="Set Delivery Day" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                                            <span class="required">* </span>
                                                                                                                                        </label>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <asp:TextBox ID="SetDay3" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="SetDay3_TextChanged" placeholder="Day Number"></asp:TextBox>
                                                                                                                                            <asp:Label ID="lblSetDay3" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="SetDay3" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter Day Number" ControlToValidate="SetDay3" ValidationGroup="txtdate31"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </div>

                                                                                                                                </div>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <asp:AsyncPostBackTrigger ControlID="txtdate3" EventName="TextChanged" />
                                                                                                                                <%--<asp:AsyncPostBackTrigger ControlID="SetDay3" EventName="TextChanged" />--%>
                                                                                                                            </Triggers>
                                                                                                                        </asp:UpdatePanel>

                                                                                                                        <div class="modal-footer">
                                                                                                                            <asp:LinkButton ID="setdate3" class="btn green-haze modalBackgroundbtn-circle" ValidationGroup="txtdate31" runat="server" OnClick="setdate3_Click"> Set</asp:LinkButton>
                                                                                                                            <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                            <asp:Button ID="btncancle3" runat="server" class="btn btn-default" Text="Cancel" />

                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                        </asp:Panel>
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="btncancle3" Enabled="True" PopupControlID="pnlsetdate3" TargetControlID="btnDateset3"></cc1:ModalPopupExtender>

                                                                                    </th>
                                                                                    <th style="width: 5%">
                                                                                        <asp:Label ID="lblDate4" runat="server" Text=""></asp:Label>
                                                                                        <asp:Label ID="lblFDate4" Visible="false" runat="server" Text=""></asp:Label>
                                                                                        <asp:Button ID="btnDateset4" CssClass="btn red" Visible="false" runat="server" Text="Set" />

                                                                                        <asp:Panel ID="pnlsetdate4" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto">

                                                                                            <div class="row" style="position: fixed; left: 20%; top: 4%; width: auto">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="portlet box purple">
                                                                                                        <div class="portlet-title">
                                                                                                            <div class="caption">
                                                                                                                <i class="fa fa-globe"></i>
                                                                                                                Set Date
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="portlet-body">
                                                                                                            <div class="tabbable">
                                                                                                                <div class="tab-content no-space">
                                                                                                                    <div class="form-body">

                                                                                                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="form-group">
                                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                                        <label runat="server" id="Label23" class="col-md-4 control-label  ">
                                                                                                                                            <asp:Label ID="Label24" runat="server" Text="Set Date" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                                            <span class="required">* </span>
                                                                                                                                        </label>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <asp:TextBox ID="txtdate4" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtdate4_TextChanged" placeholder="MM/dd/yyyy"></asp:TextBox>
                                                                                                                                            <asp:Label ID="lblmdate4" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdate4" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select Date" ControlToValidate="txtdate4" ValidationGroup="txtdate41"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </div>

                                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                                        <label runat="server" id="Label35" class="col-md-4 control-label  ">
                                                                                                                                            <asp:Label ID="Label36" runat="server" Text="Set Delivery Day" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                                            <span class="required">* </span>
                                                                                                                                        </label>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <asp:TextBox ID="SetDay4" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="SetDay4_TextChanged" placeholder="Day Number"></asp:TextBox>
                                                                                                                                            <asp:Label ID="lblSetDay4" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="SetDay4" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator10" runat="server" ErrorMessage="Enter Day Number" ControlToValidate="SetDay4" ValidationGroup="txtdate41"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </div>

                                                                                                                                </div>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <asp:AsyncPostBackTrigger ControlID="txtdate4" EventName="TextChanged" />
                                                                                                                                <%--<asp:AsyncPostBackTrigger ControlID="SetDay4" EventName="TextChanged" />--%>
                                                                                                                            </Triggers>
                                                                                                                        </asp:UpdatePanel>


                                                                                                                        <div class="modal-footer">
                                                                                                                            <asp:LinkButton ID="setdate4" class="btn green-haze modalBackgroundbtn-circle" ValidationGroup="txtdate41" runat="server" OnClick="setdate4_Click"> Set</asp:LinkButton>
                                                                                                                            <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                            <asp:Button ID="btncancle4" runat="server" class="btn btn-default" Text="Cancel" />

                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                        </asp:Panel>
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="btncancle4" Enabled="True" PopupControlID="pnlsetdate4" TargetControlID="btnDateset4"></cc1:ModalPopupExtender>

                                                                                    </th>
                                                                                    <th style="width: 5%">
                                                                                        <asp:Label ID="lblDate5" runat="server" Text=""></asp:Label>
                                                                                        <asp:Label ID="lblFDate5" Visible="false" runat="server" Text=""></asp:Label>
                                                                                        <asp:Button ID="btnDateset5" CssClass="btn red" Visible="false" runat="server" Text="Set" />

                                                                                        <asp:Panel ID="pnlsetdate5" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto">

                                                                                            <div class="row" style="position: fixed; left: 20%; top: 4%; width: auto">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="portlet box purple">
                                                                                                        <div class="portlet-title">
                                                                                                            <div class="caption">
                                                                                                                <i class="fa fa-globe"></i>
                                                                                                                Set Date
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="portlet-body">
                                                                                                            <div class="tabbable">
                                                                                                                <div class="tab-content no-space">
                                                                                                                    <div class="form-body">

                                                                                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="form-group">
                                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                                        <label runat="server" id="Label25" class="col-md-4 control-label  ">
                                                                                                                                            <asp:Label ID="Label26" runat="server" Text="Set Date" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                                            <span class="required">* </span>
                                                                                                                                        </label>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <asp:TextBox ID="txtdate5" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtdate5_TextChanged" placeholder="MM/dd/yyyy"></asp:TextBox>
                                                                                                                                            <asp:Label ID="lblmdate5" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdate5" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select Date" ControlToValidate="txtdate5" ValidationGroup="txtdate51"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </div>

                                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                                        <label runat="server" id="Label37" class="col-md-4 control-label  ">
                                                                                                                                            <asp:Label ID="Label38" runat="server" Text="Set Delivery Day" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                                            <span class="required">* </span>
                                                                                                                                        </label>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <asp:TextBox ID="SetDay5" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="SetDay5_TextChanged" placeholder="Day Number"></asp:TextBox>
                                                                                                                                            <asp:Label ID="lblSetDay5" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="SetDay5" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator11" runat="server" ErrorMessage="Enter Day Number" ControlToValidate="SetDay5" ValidationGroup="txtdate51"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <asp:AsyncPostBackTrigger ControlID="txtdate5" EventName="TextChanged" />
                                                                                                                                <%-- <asp:AsyncPostBackTrigger ControlID="SetDay5" EventName="TextChanged" />--%>
                                                                                                                            </Triggers>
                                                                                                                        </asp:UpdatePanel>

                                                                                                                        <div class="modal-footer">
                                                                                                                            <asp:LinkButton ID="setdate5" class="btn green-haze modalBackgroundbtn-circle" ValidationGroup="txtdate51" runat="server" OnClick="setdate5_Click"> Set</asp:LinkButton>
                                                                                                                            <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                            <asp:Button ID="btncancle5" runat="server" class="btn btn-default" Text="Cancel" />

                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                        </asp:Panel>
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="btncancle5" Enabled="True" PopupControlID="pnlsetdate5" TargetControlID="btnDateset5"></cc1:ModalPopupExtender>

                                                                                    </th>
                                                                                    <th style="width: 5%">
                                                                                        <asp:Label ID="lblDate6" runat="server" Text=""></asp:Label>
                                                                                        <asp:Label ID="lblFDate6" Visible="false" runat="server" Text=""></asp:Label>
                                                                                        <asp:Button ID="btnDateset6" CssClass="btn red" Visible="false" runat="server" Text="Set" />

                                                                                        <asp:Panel ID="pnlsetdate6" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: auto; overflow: auto">

                                                                                            <div class="row" style="position: fixed; left: 20%; top: 4%; width: auto">
                                                                                                <div class="col-md-12">
                                                                                                    <div class="portlet box purple">
                                                                                                        <div class="portlet-title">
                                                                                                            <div class="caption">
                                                                                                                <i class="fa fa-globe"></i>
                                                                                                                Set Date
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="portlet-body">
                                                                                                            <div class="tabbable">
                                                                                                                <div class="tab-content no-space">
                                                                                                                    <div class="form-body">

                                                                                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="form-group">
                                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                                        <label runat="server" id="Label27" class="col-md-4 control-label  ">
                                                                                                                                            <asp:Label ID="Label28" runat="server" Text="Set Date" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                                            <span class="required">* </span>
                                                                                                                                        </label>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <asp:TextBox ID="txtdate6" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtdate6_TextChanged" placeholder="MM/dd/yyyy"></asp:TextBox>
                                                                                                                                            <asp:Label ID="lblmdate6" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdate6" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Date" ControlToValidate="txtdate6" ValidationGroup="txtdate61"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </div>

                                                                                                                                    <div class="col-md-6" style="color: black; font-weight: normal">
                                                                                                                                        <label runat="server" id="Label39" class="col-md-4 control-label  ">
                                                                                                                                            <asp:Label ID="Label40" runat="server" Text="Set Delivery Day" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                                                                                                                            <span class="required">* </span>
                                                                                                                                        </label>
                                                                                                                                        <div class="col-md-8">
                                                                                                                                            <asp:TextBox ID="SetDay6" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="SetDay6_TextChanged" placeholder="Day Number"></asp:TextBox>
                                                                                                                                            <asp:Label ID="lblSetDay6" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="SetDay6" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                                                                            <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator12" runat="server" ErrorMessage="Enter Day Number" ControlToValidate="SetDay6" ValidationGroup="txtdate61"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </div>

                                                                                                                                </div>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <asp:AsyncPostBackTrigger ControlID="txtdate6" EventName="TextChanged" />
                                                                                                                                <%--<asp:AsyncPostBackTrigger ControlID="SetDay6" EventName="TextChanged" />--%>
                                                                                                                            </Triggers>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                        <div class="modal-footer">
                                                                                                                            <asp:LinkButton ID="setdate6" class="btn green-haze modalBackgroundbtn-circle" ValidationGroup="txtdate61" runat="server" OnClick="setdate6_Click"> Set</asp:LinkButton>
                                                                                                                            <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle"  validationgroup="S" Text="Update" OnClick="btnUpdate_Click" />--%>
                                                                                                                            <asp:Button ID="btncancle6" runat="server" class="btn btn-default" Text="Cancel" />

                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>

                                                                                        </asp:Panel>
                                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender6" runat="server" DynamicServicePath="" BackgroundCssClass="" CancelControlID="btncancle6" Enabled="True" PopupControlID="pnlsetdate6" TargetControlID="btnDateset6"></cc1:ModalPopupExtender>

                                                                                    </th>
                                                                                    <th></th>
                                                                                    <th></th>
                                                                                    <th></th>
                                                                                    <th></th>
                                                                                    <th></th>
                                                                                    <th></th>
                                                                                    <th></th>

                                                                                </tr>
                                                                                <tr style="border: 1px solid gray; background: #33ff77 none repeat scroll 0 0;">
                                                                                    <th style="text-align: right;">Delivery Day</th>
                                                                                    <th>
                                                                                        <asp:Label ID="lbldelday1" runat="server" Text="1"></asp:Label>
                                                                                        <asp:Label ID="lbldelday11" Visible="false" runat="server" Text="1"></asp:Label>
                                                                                        <asp:TextBox ID="txtdelday1" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtdelday1" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />

                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lbldelday2" runat="server" Text="2"></asp:Label>
                                                                                        <asp:Label ID="lbldelday21" Visible="false" runat="server" Text="2"></asp:Label>
                                                                                        <asp:TextBox ID="txtdelday2" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" TargetControlID="txtdelday2" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />

                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lbldelday3" runat="server" Text="3"></asp:Label>
                                                                                        <asp:Label ID="lbldelday31" Visible="false" runat="server" Text="3"></asp:Label>
                                                                                        <asp:TextBox ID="txtdelday3" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" TargetControlID="txtdelday3" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />

                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lbldelday4" runat="server" Text="4"></asp:Label>
                                                                                        <asp:Label ID="lbldelday41" Visible="false" runat="server" Text="4"></asp:Label>
                                                                                        <asp:TextBox ID="txtdelday4" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" TargetControlID="txtdelday4" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />

                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lbldelday5" runat="server" Text="5"></asp:Label>
                                                                                        <asp:Label ID="lbldelday51" Visible="false" runat="server" Text="5"></asp:Label>
                                                                                        <asp:TextBox ID="txtdelday5" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" TargetControlID="txtdelday5" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />

                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lbldelday6" runat="server" Text="6"></asp:Label>
                                                                                        <asp:Label ID="lbldelday61" Visible="false" runat="server" Text="6"></asp:Label>
                                                                                        <asp:TextBox ID="txtdelday6" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" TargetControlID="txtdelday6" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />

                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Button ID="btnsetday" CssClass="btn red" Visible="false" OnClick="btnsetday_Click" runat="server" ValidationGroup="ss" Text="Set" />
                                                                                    </th>
                                                                                    <th></th>
                                                                                    <th></th>
                                                                                    <th></th>
                                                                                    <th></th>
                                                                                    <th></th>
                                                                                    <th></th>

                                                                                </tr>
                                                                                <tr style="border: 1px solid gray; border-bottom: 1px solid gray; background: #e6ffee none repeat scroll 0 0;">
                                                                                    <th>
                                                                                        <div style="width: -moz-max-content; position: relative; float: left;">
                                                                                            MEAL PRODUCT
                                                                                        </div>
                                                                                        <div style="width: -moz-max-content; position: relative; float: right;">
                                                                                            Operation Days
                                                                                        </div>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDayName1" runat="server" Text="Sat"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDayName2" runat="server" Text="Sun"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDayName3" runat="server" Text="Mon"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDayName4" runat="server" Text="Tue"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDayName5" runat="server" Text="Wed"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDayName6" runat="server" Text="Thu"></asp:Label></th>
                                                                                    <th style="width: 5.5%;">Cal</th>
                                                                                    <th style="width: 5.5%;">Prot</th>
                                                                                    <th style="width: 5.5%;">Cbs</th>
                                                                                    <th style="width: 5.5%;">Fat</th>
                                                                                    <th style="width: 5.5%;">Wght</th>
                                                                                    <th style="width: 7%;">Sale Price</th>
                                                                                    <th>Qty</th>

                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" >
                                                                                    <ContentTemplate>--%>
                                                                                <asp:ListView ID="ListView2" runat="server" OnItemCommand="ListView2_ItemCommand" OnItemDataBound="ListView2_ItemDataBound">
                                                                                    <ItemTemplate>

                                                                                        <tr>
                                                                                            <td>
                                                                                                <%--<asp:Label ID="Label11" runat="server" Text='<%# Eval("ProdName1") %>'></asp:Label>--%>
                                                                                                <asp:TextBox ID="txtproduct" runat="server" Width="100%" Text='<%# getProdname(Convert.ToInt64(Eval("MYPRODID"))) %>' OnBlue="showProgress()"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator31" runat="server" ErrorMessage="Enter Product Name" ControlToValidate="txtproduct" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                                <%--OnTextChanged="txtproduct_TextChanged" AutoPostBack="true"--%>
                                                                                                <asp:Label ID="lblProd" runat="server" Visible="false" Text='<%# Eval("MYPRODID") %>'></asp:Label>
                                                                                                <%--<asp:DropDownList ID="drpProtein" Style="" class="form-control select2me input-xlarge" ForeColor="Blue" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpProtein_SelectedIndexChanged">
                                                                                                </asp:DropDownList>--%>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:CheckBox ID="RDODay1" OnCheckedChanged="RDODay1_CheckedChanged" onchange="showProgress()" AutoPostBack="true" runat="server" Value="1" Enabled="false" CssClass="item-list radio-inline" GroupName="A" Style="margin-top: 0px; margin-left: -5px;" />
                                                                                                <asp:TextBox ID="txtqty1" Visible="false" Text="1" Width="100%" CssClass="txtcenter" runat="server"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator12" runat="server" ErrorMessage="Enter Qty" ControlToValidate="txtqty1" ValidationGroup="txtqty"></asp:RequiredFieldValidator>
                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" TargetControlID="txtqty1" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />

                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:CheckBox ID="RDODay2" OnCheckedChanged="RDODay2_CheckedChanged" onchange="showProgress()" AutoPostBack="true" runat="server" Value="2" Enabled="false" CssClass="item-list radio-inline" GroupName="A" Style="margin-top: 0px; margin-left: -5px;" />
                                                                                                <asp:TextBox ID="txtqty2" Visible="false" Text="1" Width="100%" CssClass="txtcenter" runat="server"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator14" runat="server" ErrorMessage="Enter Qty" ControlToValidate="txtqty2" ValidationGroup="txtqty"></asp:RequiredFieldValidator>
                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" TargetControlID="txtqty2" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:CheckBox ID="RDODay3" OnCheckedChanged="RDODay3_CheckedChanged" onchange="showProgress()" AutoPostBack="true" runat="server" Value="3" Enabled="false" CssClass="item-list radio-inline" GroupName="A" Style="margin-top: 0px; margin-left: -5px;" />
                                                                                                <asp:TextBox ID="txtqty3" Visible="false" Text="1" Width="100%" CssClass="txtcenter" runat="server"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator15" runat="server" ErrorMessage="Enter Qty" ControlToValidate="txtqty3" ValidationGroup="txtqty"></asp:RequiredFieldValidator>
                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" TargetControlID="txtqty3" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:CheckBox ID="RDODay4" OnCheckedChanged="RDODay4_CheckedChanged" onchange="showProgress()" AutoPostBack="true" runat="server" Value="4" Enabled="false" CssClass="item-list radio-inline" GroupName="A" Style="margin-top: 0px; margin-left: -5px;" />
                                                                                                <asp:TextBox ID="txtqty4" Visible="false" Text="1" Width="100%" CssClass="txtcenter" runat="server"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator16" runat="server" ErrorMessage="Enter Qty" ControlToValidate="txtqty4" ValidationGroup="txtqty"></asp:RequiredFieldValidator>
                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" TargetControlID="txtqty4" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:CheckBox ID="RDODay5" OnCheckedChanged="RDODay5_CheckedChanged" onchange="showProgress()" AutoPostBack="true" runat="server" Value="5" Enabled="false" CssClass="item-list radio-inline" GroupName="A" Style="margin-top: 0px; margin-left: -5px;" />
                                                                                                <asp:TextBox ID="txtqty5" Visible="false" Text="1" Width="100%" CssClass="txtcenter" runat="server"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator17" runat="server" ErrorMessage="Enter Qty" ControlToValidate="txtqty5" ValidationGroup="txtqty"></asp:RequiredFieldValidator>
                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" TargetControlID="txtqty5" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:CheckBox ID="RDODay6" OnCheckedChanged="RDODay6_CheckedChanged" onchange="showProgress()" AutoPostBack="true" runat="server" Value="6" Enabled="false" CssClass="item-list radio-inline" GroupName="A" Style="margin-top: 0px; margin-left: -5px;" />
                                                                                                <asp:TextBox ID="txtqty6" Visible="false" Text="1" Width="100%" CssClass="txtcenter" runat="server"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator19" runat="server" ErrorMessage="Enter Qty" ControlToValidate="txtqty6" ValidationGroup="txtqty"></asp:RequiredFieldValidator>
                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" TargetControlID="txtqty6" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtCalories" OnTextChanged="txtCalories_TextChanged" AutoPostBack="true" Width="100%" runat="server" OnBlue="showProgress()"></asp:TextBox>

                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtProtain" OnTextChanged="txtProtain_TextChanged" AutoPostBack="true" Width="100%" runat="server" OnBlue="showProgress()"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtCarbs" OnTextChanged="txtCarbs_TextChanged" AutoPostBack="true" Width="100%" runat="server" OnBlue="showProgress()"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtFat" OnTextChanged="txtFat_TextChanged" AutoPostBack="true" Width="100%" runat="server" OnBlue="showProgress()"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtWeight" OnTextChanged="txtWeight_TextChanged" AutoPostBack="true" Width="100%" runat="server" OnBlue="showProgress()"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtsaleprice" OnTextChanged="txtsaleprice_TextChanged" AutoPostBack="true" Width="100%" runat="server"></asp:TextBox>
                                                                                            </td>

                                                                                            <td>
                                                                                                <asp:LinkButton ID="lnkbtnQty" CssClass="btn red" runat="server" CommandName="AddQty" CommandArgument='<%# Eval("PlanID")+","+ Eval("MealType")+","+ Eval("MYPRODID")%>' Text="Qty">Qty</asp:LinkButton>
                                                                                                <%--<asp:LinkButton ID="lnkhideQty" CssClass="btn red" runat="server" CommandName="AddQty" CommandArgument='<%# Eval("PlanID")+","+ Eval("MealType")+","+ Eval("MYPRODID")%>' Text="Qty">Save</asp:LinkButton>--%>
                                                                                            </td>
                                                                                        </tr>


                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                                <%--</ContentTemplate>
                                                                                    <Triggers>

                                                                                        <asp:AsyncPostBackTrigger ControlID="txtproduct" EventName="TextChanged" />

                                                                                        <asp:AsyncPostBackTrigger ControlID="RDODay1" EventName="CheckedChanged" />
                                                                                        <asp:AsyncPostBackTrigger ControlID="RDODay2" EventName="CheckedChanged" />
                                                                                        <asp:AsyncPostBackTrigger ControlID="RDODay3" EventName="CheckedChanged" />
                                                                                        <asp:AsyncPostBackTrigger ControlID="RDODay4" EventName="CheckedChanged" />
                                                                                        <asp:AsyncPostBackTrigger ControlID="RDODay5" EventName="CheckedChanged" />
                                                                                        <asp:AsyncPostBackTrigger ControlID="RDODay6" EventName="CheckedChanged" />

                                                                                        <asp:AsyncPostBackTrigger ControlID="txtCalories" EventName="TextChanged" />
                                                                                        <asp:AsyncPostBackTrigger ControlID="txtProtain" EventName="TextChanged" />
                                                                                        <asp:AsyncPostBackTrigger ControlID="txtCarbs" EventName="TextChanged" />
                                                                                        <asp:AsyncPostBackTrigger ControlID="txtFat" EventName="TextChanged" />
                                                                                        <asp:AsyncPostBackTrigger ControlID="txtWeight" EventName="TextChanged" />
                                                                                        <asp:AsyncPostBackTrigger ControlID="txtsaleprice" EventName="TextChanged" />

                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>--%>
                                                                            </tbody>
                                                                        </table>
                                                                        <table class="table table-striped table-bordered table-hover" id="sample_2">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>
                                                                                        <asp:Label runat="server" ID="Label1" Text="Plan name"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label runat="server" ID="Label2" Text="Meal name"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblProd1" runat="server" Text="Product Name"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label runat="server" ID="Label3" Text="Protein/Carbs"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label runat="server" ID="Label17" Text="Delivery #"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label runat="server" ID="Label18" Text="Expected Del Date"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label runat="server" ID="Label12" Text="Week"></asp:Label></th>

                                                                                    <%--<th style="width: 60px;">ACTION</th>--%>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <asp:ListView ID="Listview3" runat="server">
                                                                                    <ItemTemplate>
                                                                                        <tr>

                                                                                            <td>
                                                                                                <asp:Label ID="lblPlan" runat="server" Text='<%# GetPlan(Convert.ToInt32( Eval("planid")))%>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblMeal" runat="server" Text='<%# GetMeal1(Convert.ToInt32( Eval("MealType")))%>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblProd2" runat="server" Text='<%# getProdname(Convert.ToInt64(Eval("MYPRODID"))) %>'></asp:Label>
                                                                                                <asp:Label ID="lblprodID" Visible="false" runat="server" Text='<%# Eval("MYPRODID") %>'></asp:Label>
                                                                                                <asp:Label ID="lblSalePrice" Visible="false" runat="server" Text='<%# Eval("Item_cost") %>'></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblproteinCarb1" runat="server" Text='<%# Eval("ItemWeight") %>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label19" runat="server" Text='<%# Eval("DayNumber") %>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label20" runat="server" Text='<%# Convert.ToDateTime(Eval("ExpectedDelDate")).ToShortDateString() %>'></asp:Label></td>
                                                                                            <td>
                                                                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("NoOfWeek") %>'></asp:Label>
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

                                                    <%-- </ContentTemplate>

                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnAdd" />
                                                        </Triggers>


                                                    </asp:UpdatePanel>
                                                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel16">
                                                        <ProgressTemplate>
                                                            <div class="overlay">
                                                                <div style="z-index: 1000; margin-left: 350px; margin-top: 200px; opacity: 1; -moz-opacity: 1;">
                                                                    <img src="../assets/admin/layout4/img/loading-spinner-blue.gif" />
                                                                    &nbsp;please wait...
                                                                </div>
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-12">
                                    <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                    <div class="portlet box blue">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-edit"></i>
                                                <asp:Label runat="server" ID="Label5"></asp:Label>
                                                List
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <%--<a href="javascript:;" class="reload"></a>--%>
                                                <asp:LinkButton ID="lnkbtnreload" OnClick="lnkbtnreload_Click" runat="server"><img src="../assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                        </div>
                                        <div class="portlet-body">

                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>

                                                        <th style="width: 60px;">ACTION</th>
                                                        <th style="width: 7%;">
                                                            <asp:Label runat="server" ID="lblCID1" Text="ID#"></asp:Label></th>
                                                        <th style="width: 8%;">
                                                            <asp:Label runat="server" ID="lblContractID" Text="Ctrct ID#"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhCustomer" Text="Customer"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhPlan" Text="Plan"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhDay" Visible="false" Text="Day"></asp:Label>
                                                            <asp:Label runat="server" ID="lblhMeal" Text="Meal"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhStartDate" Text="Start Date"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhEndDate" Text="End Date"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhDays" Text="Days"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhDelivered" Text="Delivered"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="lblhPending" Text="Pending"></asp:Label></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand">
                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>

                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("CustomerID")+","+Eval("planid")+","+Eval("DeliveryMeal")+","+Eval("MYTRANSID")+","+Eval("DeliveryID") %>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom" OnClientClick="showProgress()"><i class="fa fa-pencil"></i></asp:LinkButton>

                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("CustomerID")+","+Eval("planid")+","+Eval("DeliveryMeal")+","+Eval("MYTRANSID")+","+Eval("DeliveryID") %>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton>

                                                                            </td>
                                                                            <%--<td>
                                                                                <asp:LinkButton class="btn btn-lg blue" CommandName="btnprient" CommandArgument='<%# Eval("CustomerID")+","+Eval("planid")+","+Eval("DeliveryMeal") %>' ID="btnprient" Style="padding: 0px 5px 0px 0px; border-left-width: 5px; border-top-width: 2px; border-bottom-width: 2px; font-size: 12px;" runat="server">Report</asp:LinkButton>
                                                                            </td>--%>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("CustomerID") %>'></asp:Label>
                                                                </td>
                                                                <%--<td>
                                                                    <asp:LinkButton ID="LinkButton10" runat="server" CommandName="btnview" CommandArgument='<%# Eval("CustomerID")+","+Eval("planid")+","+Eval("DeliveryMeal")+","+Eval("MYTRANSID")+","+Eval("DeliveryID") %>'>
                                                                    <asp:Label ID="Label14" runat="server" Text='<%# Eval("ContractID") %>'></asp:Label>
                                                                        </asp:LinkButton>
                                                                </td>--%>
                                                                <td>
                                                                    <asp:Label ID="Label14" runat="server" Text='<%# Eval("ContractID") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCustomer" runat="server" Text='<%# GetCustomer(Convert.ToInt32( Eval("CustomerID")))%>'></asp:Label>
                                                                    <asp:Label ID="lblMYTRANSID" runat="server" Visible="false" Text='<%# Eval("MYTRANSID")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPlan" runat="server" Text='<%# GetPlan(Convert.ToInt32( Eval("planid")))%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblMeal" runat="server" Text='<%# GetMeal1(Convert.ToInt32( Eval("DeliveryMeal")))%>'></asp:Label>
                                                                    <%--<asp:Label ID="lblMeal" runat="server" Text='<%# GetMeal(Convert.ToInt32( Eval("planid")),Convert.ToInt32(Eval("MYTRANSID")))%>'></asp:Label>--%>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label69" runat="server" Text='<%# getStartDate(Convert.ToDateTime(Eval("StartDate"))) %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label72" runat="server" Text='<%# getEndDate(Convert.ToDateTime(Eval("EndDate"))) %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label73" runat="server" Text='<%# getTotaldel(Convert.ToInt32(Eval("MYTRANSID"))) %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label74" runat="server" Text='<%# getTotaldeldone(Convert.ToInt32(Eval("MYTRANSID"))) %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label75" runat="server" Text='<%# getdeliverd(Convert.ToInt32(Eval("MYTRANSID"))) %>'></asp:Label>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <!-- END EXAMPLE TABLE PORTLET-->
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                    <div class="portlet box blue">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-edit"></i>
                                                <asp:Label runat="server" ID="Label41" Text="Activity Log"></asp:Label>
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

                                            <table class="table table-striped table-hover table-bordered" id="sample_3">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 1%;"></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label43" Text="CRUP ID#"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label44" Text="PHYSICAL LOCATION ID"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label45" Text="Activity Note"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label46" Text="CREATED BY"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label47" Text="CREATED DATE"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label48" Text="UPDATED BY"></asp:Label></th>
                                                        <th>
                                                            <asp:Label runat="server" ID="Label49" Text="UPDATED DATE"></asp:Label></th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="Listview4" runat="server" OnItemCommand="Listview1_ItemCommand">
                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td></td>

                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("CRUP_ID") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label50" runat="server" Text='<%# Eval("PHYSICALLOCID") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label51" runat="server" Text='<%# Eval("ActivityNote") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label52" runat="server" Text='<%# Eval("CREATED_BY") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label53" runat="server" Text='<%# Eval("CREATED_DT") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label54" runat="server" Text='<%# Eval("UPDATED_BY") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label55" runat="server" Text='<%# Eval("UPDATED_DT") %>'></asp:Label>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <!-- END EXAMPLE TABLE PORTLET-->
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
