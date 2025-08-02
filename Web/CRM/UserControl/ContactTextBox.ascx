<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactTextBox.ascx.cs" Inherits="Web.CRM.UserControl.ContactTextBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="txtupdaet" runat="server">
    <ContentTemplate>
        <div class="input-group" style="text-align: left">
            <asp:TextBox ID="txtContactName" runat="server" name="name" placeholder="Contact Name" AutoPostBack="True" OnTextChanged="txtContactName_TextChanged" data-toggle="tooltip" ToolTip="Company Name" class="form-control"></asp:TextBox>
            <span class="input-group-btn">
              
            </span>
            <asp:LinkButton ID="lkbContactName" runat="server" OnClick="btnContact_Click">
                                                                                 <i class="icon-arrow-right" style="color:black"></i>
            </asp:LinkButton>
          
        </div>
        <asp:RequiredFieldValidator Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidatorCustomerName" runat="server" ErrorMessage="Contact Name Is Required" ControlToValidate="txtContactName" ValidationGroup="Submit"></asp:RequiredFieldValidator>
        <asp:Label ID="lblCustomer1" runat="server" ForeColor="Red"></asp:Label>
    </ContentTemplate>
</asp:UpdatePanel>
<%--<asp:LinkButton ID="btntest4ContactTextBox" class="btn blue" runat="server"></asp:LinkButton>--%>
<asp:HiddenField ID="h1" runat="server" />
<panel id="ReceivedSign1ContactTextBox" style="padding: 1px; background-color: #fff; border: 1px solid #000;display:block" runat="server">
        <div class="modal-header">
            <asp:LinkButton ID="LinkButton3ContactTextBox" class="close" runat="server"><asp:Label ID="Label31" runat="server" Text="Cancel" ></asp:Label>
</asp:LinkButton>
           <%-- <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>--%>
            <h4 class="modal-title"> <b>  <asp:Label ID="Label13" runat="server" Text ="All Ready Exit"></asp:Label> </b></h4>
        </div>
        <div class="modal-body">
            <div class="row">     
                <div class="portlet-body">
                        <div class="tabbable">
                            <table class="table table-striped table-bordered table-hover" >
                                <thead>
                                    <tr>                                      
                                        <th><asp:Label ID="Label32" runat="server" Text="Contact Name" ></asp:Label></th>
                                        <th><asp:Label ID="Label33" runat="server" Text="Mobile Number" ></asp:Label></th>
                                        <th><asp:Label ID="Label34" runat="server" Text="Email" ></asp:Label></th>                                       
                                        <th><asp:Label ID="Label35" runat="server" Text="Fax Number" ></asp:Label></th>
                                        <th><asp:Label ID="Label36" runat="server" Text="Busphone" ></asp:Label></th>
                                        

                                    </tr>
                     </thead>
                                <tbody>
                                    
                                
                 <tr>                                      
                                        <td><asp:Label ID="labelCopop" runat="server" ></asp:Label></td>
                                        <td><asp:Label ID="lblmopop" runat="server"></asp:Label></td>
                                        <td><asp:Label ID="lblEmailpop" runat="server" ></asp:Label></td>                                       
                                        <td><asp:Label ID="lblFaxpop" runat="server"></asp:Label></td>
                                        <td><asp:Label ID="lblBuspop" runat="server"></asp:Label></td>
                    

                                    </tr>

              
                                       
                                    </tbody>
                  </table>
                            </div> 
                    </div> 
                
                </div>
            </div>
        <div class="modal-footer">
           <%-- <asp:Button ID="btnsend1" class="btn blue" runat="server" Text="Send" OnClick ="btnsend_Click" />
            <asp:LinkButton ID="btnEngineerSign" class="btn blue" runat="server" >Submit</asp:LinkButton>--%>
           <asp:Button ID="btnYes" runat="server" class="btn green-haze btn-circle" Text="Yes" OnClick="btnYes_Click" />
           <asp:Button ID="btnNo" runat="server" class="btn red-haze btn-circle" Text="No" OnClick="btnNo_Click" />
          
        </div>

    </panel>
<cc1:ModalPopupExtender ID="ModalPopupExtender4ContactTextBox" runat="server" DynamicServicePath=""
    BackgroundCssClass="modalBackground" CancelControlID="LinkButton1ContactTextBox" Enabled="True"
    PopupControlID="ReceivedSign1ContactTextBox" TargetControlID="h1">
</cc1:ModalPopupExtender>

