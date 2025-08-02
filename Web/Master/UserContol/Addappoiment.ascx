<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Addappoiment.ascx.cs" Inherits="Web.Master.UserContol.Addappoiment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

 <asp:Panel ID="pnlappoint" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Add Apppinment</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal" role="form">
                        <div class="row">
                            <asp:Panel ID="Panel7" runat="server" Visible="false">
                                <div class="alert alert-danger">
                                    <strong>Error!</strong>
                                    Apppinment Allready Exist..
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblTitle" class="col-md-2 control-label">Title</asp:Label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtTitleAP" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTitleAP" ErrorMessage="Title is Required." CssClass="Validation" ValidationGroup="appoint"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblcolor" class="col-md-2 control-label">Color</asp:Label>
                                    <div class="col-md-10">
                                        <asp:DropDownList ID="drpColor" runat="server" CssClass="table-group-action-input form-control input-medium">
                                            <asp:ListItem Text="-- Select --" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Red" Value="red"></asp:ListItem>
                                            <asp:ListItem Text="Green" Value="green"></asp:ListItem>
                                            <asp:ListItem Text="Blue" Value="blue"></asp:ListItem>
                                            <asp:ListItem Text="Yellow" Value="yellow"></asp:ListItem>
                                            <asp:ListItem Text="Purple" Value="purple"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="drpColor" ErrorMessage="Color is Required." CssClass="Validation" ValidationGroup="appoint" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblurl" class="col-md-2 control-label">URL</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtURLAP" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtURLAP" ErrorMessage="URL is Required." CssClass="Validation" ValidationGroup="appoint"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblsdate" class="col-md-2 control-label">Start Date</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtSdateAP" runat="server" CssClass="form-control" placeholder="MM/dd/yyyy"></asp:TextBox>
                                        <cc1:CalendarExtender ID="TextBoxEndDt_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtSdateAP" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" TargetControlID="txtSdateAP" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtSdateAP" ErrorMessage="Start Date is Required." CssClass="Validation" ValidationGroup="appoint"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lbledate" class="col-md-2 control-label">End Date</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtEnddateAP" runat="server" CssClass="form-control" placeholder="MM/dd/yyyy"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtEnddateAP" Format="MM/dd/yyyy"></cc1:CalendarExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtEnddateAP" ValidChars="/" FilterType="Custom, numbers" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtEnddateAP" ErrorMessage="End Date Required." CssClass="Validation" ValidationGroup="appoint"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="linkclose" runat="server" class="btn default">Close</asp:LinkButton>

                    <asp:Button ID="btnsaveAppoint" runat="server" class="btn green" Text="Save" ValidationGroup="appoint" OnClick="btnsaveAppoint_Click" />

                </div>
            </div>
        </div>

    </asp:Panel>

    <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" DynamicServicePath=""
        BackgroundCssClass="modalBackground" CancelControlID="linkclose" Enabled="True"
        PopupControlID="pnlappoint" TargetControlID="btnappoint">
    </cc1:ModalPopupExtender>
