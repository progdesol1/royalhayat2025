<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="DriverDelRouting.aspx.cs" Inherits="Web.Master.DriverDelRouting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
            //$.blockUI();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-md-12">
        <div class="tabbable-custom tabbable-noborder">

            <div class="portlet box blue">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-edit"></i>
                        <asp:Label runat="server" ID="Label41" Text="DAY CLOSING PROCESS"></asp:Label>

                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a>
                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                        <a href="javascript:;" class="reload"></a>
                        <a href="javascript:;" class="remove"></a>
                    </div>
                </div>
                <div class="portlet-body">

                    <br />
                    <br />
                    <asp:Panel ID="pnlErrorMsg" runat="server" Visible="false">
                        <div class="alert alert-danger alert-dismissable">
                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                            <asp:Label ID="lblerrorMsg" runat="server"></asp:Label>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                        <div class="alert alert-success alert-dismissable">
                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </asp:Panel>


                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4" style="color: black; font-weight: normal">
                                <label runat="server" id="Label78" class="col-md-4 control-label  ">
                                    <asp:Label ID="Label79" runat="server" Text="Select Date" meta:resourcekey="lblCity1Resource1"></asp:Label>
                                    <span class="required">* </span>
                                </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtExpDate" runat="server" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtExpDate_TextChanged" placeholder="MM/dd/yyyy"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtExpDate" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator26" runat="server" ErrorMessage=" Requaird Date" ControlToValidate="txtExpDate" ValidationGroup="Delivery"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <asp:Button ID="Button1" runat="server" class="btn btn-sm green" Text="Day Close" ValidationGroup="Delivery" OnClientClick="showProgress()" OnClick="Button1_Click" />

                                       
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="Button1" />

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
                                </asp:UpdateProgress>
                            </div>

                        </div>


                    </div>

                    <br />
                    <div class="alert alert-danger">
                        <asp:Label ID="Label1" runat="server" Font-Size="Large" Font-Bold="true" Text="This Proces required only if you have not used Driver Screen to deliver all the Subscription. "></asp:Label><br />
                        <asp:Label ID="Label2" runat="server" Font-Size="Large" Font-Bold="true" Text="This process will do the followings :-"></asp:Label><br />
                        <asp:Label ID="Label3" runat="server" Font-Size="Large" Font-Bold="true" Text="1. Will consider from given date and before all the subscription as Delivered"></asp:Label><br />
                        <asp:Label ID="Label4" runat="server" Font-Size="Large" Font-Bold="true" Text="2. Will consider from given date and before all the Products as Production done"></asp:Label><br />
                    </div>
                    <br />
                    <br />
                    <br />
                    <br />


                </div>

            </div>




        </div>
    </div>

</asp:Content>
