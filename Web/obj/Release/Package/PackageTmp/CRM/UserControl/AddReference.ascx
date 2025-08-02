<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddReference.ascx.cs" Inherits="Web.CRM.UserControl.AddReference" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:LinkButton ID="LinkAccount123" runat="server">ADD REFERENCE</asp:LinkButton>
<panel id="pnlresponsive" style="padding: 1px; background-color: #fff; border: 1px solid #000;" runat="server">
        <div class="modal-header" >
           
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
            <h4 class="modal-title"> <b>  <asp:Label ID="Label18" Text="ADD REFERENCE" runat="server" ></asp:Label> </b></h4>
        </div>
        <div class="modal-body">
            <div class="row">
           
                  <div class="col-md-6">
                                                             <h4><b>Add Reference Name</b></h4>
                                                            <p>
                                                                <asp:TextBox Style="width: 300px;" ID="txtAddReference3" runat="server" class="form-control" maxlength="50"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Add Reference Required" ControlToValidate="txtAddReference3" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </p>
                                                            <h4><b>Short Name</b></h4>
                                                            <p>
                                                               <asp:TextBox Style="width: 300px;" ID="txtShortName1" runat="server" class="form-control" maxlength="50"></asp:TextBox>

                                                            </p>
                                                            <h4><b>Remark</b></h4>
                                                            <p>
                                                                <asp:TextBox Style="width: 300px;" ID="txtREMARK2" runat="server" class="form-control" maxlength="500" TextMode="MultiLine"></asp:TextBox>

                                                            </p>

            </div>
        </div>
            </div>
        <div class="modal-footer">
         
            <asp:LinkButton ID="lbButton1" class="btn green-haze btn-circle" ValidationGroup="S"  runat="server" OnClick="btnAddNew_Click">AddNew</asp:LinkButton>
                 
                    <asp:Button ID="Button2" runat="server" data-dismiss="modal" class="btn green-haze btn-circle" OnClick="btnCancel2_Click" Text="Cancel" />

        </div>

    </panel>
<cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" DynamicServicePath=""
    BackgroundCssClass="modalBackground" CancelControlID="Button2" Enabled="True"
    PopupControlID="pnlresponsive" TargetControlID="LinkAccount123">
</cc1:ModalPopupExtender>
