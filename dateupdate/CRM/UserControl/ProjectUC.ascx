<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectUC.ascx.cs" Inherits="Web.CRM.UserControl.ProjectUC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:LinkButton ID="LinkAccount123" runat="server"><i class="icon-arrow-right" style="color:black"></i></asp:LinkButton>
<panel id="pnlresponsive1" style="padding: 1px; background-color: #fff; border: 1px solid #000;" runat="server">
    
        <div class="modal-header">
           
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
            <h4 class="modal-title"> <b>  <asp:Label ID="Label6" Text="ADD Project" runat="server" ></asp:Label> </b></h4>
        </div>
        <div class="modal-body">
            <div class="row">
           
                  <div class="col-md-6">
                                                             <h4><b>ADD PROJECT</b></h4>
                                                            <p>
                                                                    <label>
                            Projetc Name</label><span class="field">
                                                                <asp:TextBox Style="width: 300px;" ID="txtproject" runat="server" class="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Add Reference Required" ControlToValidate="txtproject" ValidationGroup="S"></asp:RequiredFieldValidator>
                                </span>
                                                            </p>
                                                          

            </div>
        </div>
            </div>
        <div class="modal-footer">
           <%-- <asp:Button ID="btnsend1" class="btn blue" runat="server" Text="Send" OnClick ="btnsend_Click" />
            <asp:LinkButton ID="btnEngineerSign" class="btn blue" runat="server" >Submit</asp:LinkButton>--%>
            <asp:LinkButton ID="lbButton1" class="btn green-haze btn-circle" ValidationGroup="S"  runat="server" OnClick="lbButton1_Click" >Add New</asp:LinkButton>
                   <%-- <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle" validationgroup="S" Text="AddNew" OnClick="btnAddNew_Click" />--%>
                    <asp:Button ID="Button2" runat="server"  class="btn green-haze btn-circle"  Text="Cancel" />

        </div>

   </panel>
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
    BackgroundCssClass="modalBackground" CancelControlID="Button2" Enabled="True"
    PopupControlID="pnlresponsive1" TargetControlID="LinkAccount123">
</cc1:ModalPopupExtender>



