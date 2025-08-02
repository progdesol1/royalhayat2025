<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="SpendBasedCampaign.aspx.cs" Inherits="Web.CRM.SpendBasedCampaign" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <div class="portlet box green-jungle" style="background-color: snow;">
                    <div class="portlet-title">
                        <div class="caption">
                            Spend Based Campaign  <span class="step-title"></span>
                        </div>
                        <div class="tools">
                            <a href="javascript:;" id="A1" runat="server" class="collapse"></a>
                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                            <a href="javascript:;" class="reload"></a>
                            <a href="javascript:;" class="remove"></a>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 7px;">
                        <%--Row1--%>
                        <div class="col-md-12" style="padding-left: 15px;">
                            <div class="form-horizontal form-row-seperated">
                                <div class="portlet box blue" id="panel1" runat="server" visible="false" style="background-color: snow;">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-gift"></i>
                                            Campaign Setting <span class="step-title"></span>
                                        </div>
                                        <div class="tools hidden-xs">
                                            <a href="javascript:;" id="pack1" runat="server" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <a href="javascript:;" class="reload"></a>
                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                    </div>
                                    <div class="portlet-body form" id="body1" runat="server" style="display: block;">
                                        <div class="form-wizard">
                                            <div class="form-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-4">
                                                                Campaign Name <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtCampaignName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator1" runat="server" Style="color: #FF0000;" ErrorMessage="CAMPAIGN NAME Required." ControlToValidate="txtCampaignName" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                <%--<span class="help-block">CAMPAIGN NAME Required.</span>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-4">
                                                                Campaign End Date  <span class="required">* </span>
                                                            </label>

                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtEnddate" runat="server" CssClass="form-control" placeholder="Campaign End Date"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtEnddate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator5" Style="color: #FF0000;" runat="server" ErrorMessage="END DATE Required." ControlToValidate="txtEnddate" ValidationGroup="submeet"></asp:RequiredFieldValidator>
                                                                <%-- <span class="help-block">END DATE Required.</span>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-4">
                                                                Reedem Reward Until ? <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtexpirydate" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtexpirydate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                                                <asp:RequiredFieldValidator CssClass="Validation" Display="Dynamic" ID="RequiredFieldValidator12" runat="server" ErrorMessage="Expiration Date Required" ControlToValidate="txtexpirydate" ValidationGroup="Desire"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-md-offset-3 col-md-9">
                                                <a href="javascript:;" class="btn blue button-previous">
                                                    <i class="m-icon-swapleft"></i>Back </a>
                                                <asp:LinkButton ID="lnknext" runat="server" OnClick="lnknext_Click" CssClass="btn btn-info" ValidationGroup="submit">Next</asp:LinkButton>
                                            </div>
                                            
                                            <div class="table-scrollable">
                                                <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>--%>
                                                <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1_info">
                                                    <thead>
                                                        <tr>

                                                            <th>
                                                                <asp:Label runat="server" ID="lblhemp_birthday" Text="CampaignType"></asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblhnation_code" Text="CampaignName"></asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblhemp_mobile" Text="CEndDate"></asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblhemp_work_telephone" Text="SpendGoal"></asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblhemp_work_email" Text="CRewardCreditOffer"></asp:Label></th>
                                                            <th style="width: 60px;">ACTION</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand1">
                                                            <LayoutTemplate>
                                                                <tr id="ItemPlaceholder" runat="server">
                                                                </tr>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblemp_birthday" runat="server" Text='<%# Eval("CampaignType")%>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblnation_code" runat="server" Text='<%# Eval("CampaignName")%>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblemp_mobile" runat="server" Text='<%# Eval("CEndDate")%>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblemp_work_telephone" runat="server" Text='<%# Eval("SpendGoal")%>'></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblemp_work_email" runat="server" Text='<%# Eval("CRewardCreditOffer")%>'></asp:Label></td>

                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("CampaignID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="fa fa-pencil"></i></asp:LinkButton></td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("CampaignID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="fa fa-times"></i></asp:LinkButton></td>
                                                                                <%-- <td><asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("JobId")%>' CommandName="btnPrint" CommandArgument='<%# Eval("JobId")%>' runat="server" class="btn btn-sm green filter-submit margin-bottom"><i class="fa fa-print"></i></asp:LinkButton></td>--%>
                                                                            </tr>
                                                                        </table>

                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>

                                                    </tbody>
                                                </table>
                                                <%-- </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--Row2--%>
                    <div class="row">
                        <%--Row2--%>
                        <div class="col-md-12" style="padding-left: 15px;">
                            <div class="form-horizontal form-row-seperated">
                                <div class="portlet box red" id="panel2" runat="server">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-gift"></i>
                                            <asp:Label ID="lblmsg" runat="server" Text="Goal & Reward"></asp:Label>
                                            <span class="step-title"></span>
                                        </div>
                                        <div class="tools hidden-xs">
                                            <a href="javascript:;" id="pack2" runat="server" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <a href="javascript:;" class="reload"></a>
                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                    </div>
                                    <div class="portlet-body form" id="body2" runat="server" style="display: block;">
                                        <div class="form-wizard">
                                            <div class="form-body">
                                                <h3 class="block" style="font-family: 'Courier New'; font-weight: bold; margin-top: 0px; margin-bottom: 0px;">Amount Customer needs to Spend to get Reward</h3>
                                                <hr style="margin-top: 0px; margin-bottom: 10px;" />
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-4">Goal <span class="required">* </span></label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpSpendGoal" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpSpendGoal_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0.000" Text="Select Goal"></asp:ListItem>
                                                                    <asp:ListItem Value="1.000" Text="KD 25"></asp:ListItem>
                                                                    <asp:ListItem Value="2.000" Text="KD 50"></asp:ListItem>
                                                                    <asp:ListItem Value="3.000" Text="KD 75"></asp:ListItem>
                                                                    <asp:ListItem Value="4.000" Text="KD 100"></asp:ListItem>
                                                                    <asp:ListItem Value="5.000" Text="KD 125"></asp:ListItem>
                                                                    <asp:ListItem Value="6.000" Text="KD 150"></asp:ListItem>
                                                                    <asp:ListItem Value="7.000" Text="KD Custom Amount"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator2" runat="server" Style="color: #FF0000;" ErrorMessage="CAMPAIGN GOAL Required." ControlToValidate="drpSpendGoal" ValidationGroup="Submition"></asp:RequiredFieldValidator>
                                                                <%--<span class="help-block">CAMPAIGN GOAL Required.</span>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtcustom" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <h3 class="block" style="font-family: 'Courier New'; font-weight: bold; margin-top: 0px; margin-bottom: 0px;">Reward Type : Get store credit</h3>
                                                <hr style="margin-top: 0px; margin-bottom: 10px;" />
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-4">Store Credit offer <span class="required">* </span></label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpCRewardCreditOffer" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="1" Text="Select Amount"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="KD 25"></asp:ListItem>
                                                                    <asp:ListItem Value="3" Text="KD 50"></asp:ListItem>
                                                                    <asp:ListItem Value="4" Text="KD 75"></asp:ListItem>
                                                                    <asp:ListItem Value="5" Text="KD 100"></asp:ListItem>
                                                                    <asp:ListItem Value="6" Text="KD 125"></asp:ListItem>
                                                                    <asp:ListItem Value="7" Text="KD 150"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator3" runat="server" ErrorMessage="CREDIT OFFER Required." ControlToValidate="drpCRewardCreditOffer" Style="color: #FF0000;" ValidationGroup="Submitted" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-12">
                                                                Can a customer participate in this campaign more than once? 
                                                                        <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-2">
                                                                </div>
                                                            <div class="col-md-10">
                                                                <asp:RadioButtonList ID="rdo1" runat="server" CssClass="md-radio-inline md-radio-list">
                                                                    <asp:ListItem Value="Y" Text="Yes,Multiple Times"></asp:ListItem>
                                                                    <asp:ListItem Value="N" Text="No,Just once per customer"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator4" Style="color: #FF0000;" runat="server" ErrorMessage="select radio Required." ControlToValidate="rdo1" ValidationGroup="Submitted"></asp:RequiredFieldValidator>
                                                                <%--   <span class="help-block">select radio Required.</span>--%>
                                                                <asp:Label ID="lblreward" runat="server" Visible="false"></asp:Label>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-offset-3 col-md-9">
                                                        <a href="javascript:;" class="btn blue button-previous"><i class="m-icon-swapleft"></i>Back </a>
                                                        <asp:LinkButton ID="linkpnl" runat="server" CssClass="btn btn-info" ValidationGroup="Submition" OnClick="linkpnl_Click">Next</asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
<%--                    <div class="row">
                      
                        <div class="col-md-12" style="padding-left: 15px;">
                            <div class="form-horizontal form-row-seperated">
                                <div class="portlet box yellow" id="panel3" runat="server">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <asp:Label ID="lblrewa" runat="server"></asp:Label>
                                            Reward <span class="step-title"></span>
                                        </div>
                                        <div class="tools hidden-xs">
                                            <a href="javascript:;" id="pack3" runat="server" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <a href="javascript:;" class="reload"></a>
                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                    </div>
                                    <div class="portlet-body form" id="body3" runat="server" style="display: block;">
                                        <div class="form-wizard">
                                            <div class="form-body">
                                                <h3 class="block">Reward Type : Get store credit</h3>



                                                <div class="col-md-offset-3 col-md-9">
                                                    <a href="javascript:;" class="btn blue button-previous"><i class="m-icon-swapleft"></i>Back </a>
                                                    <asp:LinkButton ID="lnkconti" runat="server" CssClass="btn btn-info" OnClick="lnkconti_Click" ValidationGroup="Submitted">Continue</asp:LinkButton>
                                                </div>
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                            </div>


                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>--%>
                    <div class="row">
                        <%--Row2--%>
                        <div class="col-md-12" style="padding-left: 15px;">
                            <div class="form-horizontal form-row-seperated">
                                <div class="portlet box green" id="panel4" runat="server">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <asp:Label ID="lblend" runat="server"></asp:Label>
                                            Setting <span class="step-title"></span>
                                        </div>
                                        <div class="tools hidden-xs">
                                            <a href="javascript:;" id="pack4" runat="server" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <a href="javascript:;" class="reload"></a>
                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                    </div>
                                    <div class="portlet-body form" id="body4" runat="server" style="display: block;">



                                        <div class="form-wizard">
                                            <div class="form-body">

                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="control-label col-md-4">Campaign Status <span class="required">* </span></label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">

                                                                <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Pause"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator8" Style="color: #FF0000;" runat="server" ErrorMessage="Select Status." ControlToValidate="DropDownList1" ValidationGroup="submeet"></asp:RequiredFieldValidator>
                                                            <%-- <span class="help-block">Select Status.</span>--%>
                                                            <asp:Label ID="lblset" runat="server" Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-offset-3 col-md-9">
                                                    <a href="javascript:;" class="btn blue button-previous">
                                                        <i class="m-icon-swapleft"></i>Back </a>
                                                    <asp:LinkButton ID="lnkcreate1" runat="server" OnClick="lnkcreate1_Click" ValidationGroup="submeet" CssClass="btn btn-info">Save Campaign</asp:LinkButton>
                                                </div>
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
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

