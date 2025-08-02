<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Custmer.ascx.cs" Inherits="Web.CRM.UserControl.Custmer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:LinkButton ID="LinkAccount1" runat="server" data-toggle="tooltip" ToolTip="Add">
<i class="icon-plus" style="color:black;padding-left: 4px;font-size: 25px;" ></i></asp:LinkButton>

<panel id="pnlresponsive1" style="padding: 1px; background-color: #fff; border: 1px solid #000; display: none" runat="server">
<%--<panel id="responsive" class="modal fade" tabindex="-1" aria-hidden="true" runat="server">--%>

    <div class="modal-dialog" style="position:fixed;left:20%;top:10px">
        
									<div class="modal-content">
                                        <div style="left:93%; position:relative; top: 15px;">
                                             <asp:ImageButton ID="ImageButton1" data-dismiss="modal" ImageUrl="Close.jpg" Height="25" Width="25" runat="server"></asp:ImageButton>
                                            <%--<asp:Button ID="Button2" runat="server" data-dismiss="modal" class="btn default" />--%></div>
                                         <div class="modal-header">
										
											<h4 class="modal-title">Add Customer</h4>
                                            
                                            
										</div>
                                       
<asp:UpdatePanel ID="UpdatePanel1" runat="server">    
    <ContentTemplate>
        
										<div class="modal-body">
											<div class="scroller" style="height:250px" data-always-visible="1" data-rail-visible1="1">
												<div class="row">
                                                    <asp:Panel ID="PanelCustomer" runat="server" Visible="false">
                                                    <div class="col-md-12" >
                                                    <table class="table table-bordered" id="datatable1">
                                                        <thead>
                                                            <th>Customer</th>
                                                            <th>Mobile</th>
                                                            <th>Email ID</th>
                                                            <th>Bus Number </th>
                                                        </thead>
                                                        <tbody>
                                                            <asp:ListView ID="CustomerList" runat="server" OnItemCommand="CustomerList_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr style="font-size:12px;color:blue;">
                                                                        <td><%#Eval("COMPNAME1") %></td>
                                                                        <td><%#Eval("MOBPHONE") %></td>
                                                                        <td><%#Eval("EMAIL") %></td>
                                                                        <td><%#Eval("BUSPHONE1") %></td>
                                                                        <td><asp:LinkButton ID="btnEdit" runat="server" CssClass="btn green-haze " CommandName="Modify" CommandArgument='<%# Eval("COMPID") %>'>Edit</asp:LinkButton></td>
                                                                    <%--<td><asp:Label ID="LBLCName" runat="server" Text='<%#Eval("COMPNAME1") %>'></asp:Label></td>
                                                                    <td><asp:Label ID="LBLCmobile" runat="server" Text='<%#Eval("MOBPHONE") %>'></asp:Label></td>
                                                                    <td><asp:Label ID="LBLCEmail" runat="server" Text='<%#Eval("EMAIL") %>'></asp:Label></td>
                                                                    <td><asp:Label ID="LBLCBUSno" runat="server" Text='<%#Eval("BUSPHONE1") %>'></asp:Label></td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>                                                            
                                                        </tbody>
                                                    </table>

                                                    </div>
                                                        </asp:Panel>
                                                    <div class="col-md-12">														
														    <p>
                                                                <asp:TextBox runat="server" ID="txtserach" Visible="false" Enabled="false" ForeColor="Red" CssClass="col-md-6 form-control" BorderStyle="None" style="background-color:white;"></asp:TextBox>                                                                                           
														    </p> 
                                                        </div>
                                                    <div class="col-md-6">
														<h4>Customer Name <span class="required">* </span></h4>
														<p>
                                                            <asp:TextBox runat="server" ID="txtSupplierName" class="col-md-12 form-control" AutoPostBack="true" OnTextChanged="txtSupplierName_TextChanged"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ErrorMessage="Supplier Name Required" ControlToValidate="txtSupplierName" ValidationGroup="S2"></asp:RequiredFieldValidator>--%>
<asp:Image ID="imgtxtSupplierName" runat="server" ImageUrl="images.png" Height="15" Width="15" Visible="false"></asp:Image>
                                                            <asp:Image ID="imgimgtxtSupplierNameNo" runat="server" ImageUrl="no.png" Height="15" Width="15" Visible="false"></asp:Image>
															
														</p> </div>
                                                     <div class="col-md-6">
                                                        <h4>Mobile NO <span class="required">* </span></h4>
														<p> <asp:TextBox runat="server" ID="txtMobileNO"  class="form-control" AutoPostBack="true" OnTextChanged="txtMobileNO_TextChanged"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtMobileNO" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator15" ForeColor="Red" runat="server" ErrorMessage="Supplier Name Required" ControlToValidate="txtMobileNO" ValidationGroup="S2"></asp:RequiredFieldValidator>--%>
                                                            <asp:Image ID="imgtxtMobileNO" runat="server" Visible="false" ImageUrl="no.png" Height="15" Width="15"></asp:Image>
                                                            <asp:Image ID="imgtxtMobile" runat="server" Visible="false" ImageUrl="images.png" Height="15" Width="15"></asp:Image>
                                                            </p> </div>
                                                   
                                                     <div class="col-md-6">
                                                         <h4>Address <span class="required">* </span></h4>
														<p>  <asp:TextBox  ID="txtAddress1" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator8" ForeColor="Red" runat="server" ErrorMessage="Supplier Name Required" ControlToValidate="txtAddress1" ValidationGroup="S2"></asp:RequiredFieldValidator>--%></p> 
                                                    </div>
                                                    <div class="col-md-6">
														<h4>Email <%--<span class="required">* </span>--%></h4>
														<p>
															<asp:TextBox ID="txtEMAIL" runat="server"  class="col-md-12 form-control" AutoPostBack="true" OnTextChanged="txtEMAIL_TextChanged"></asp:TextBox>
                               <%-- <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" ForeColor="Red" runat="server" ErrorMessage="Supplier Name Required" ControlToValidate="txtEMAIL" ValidationGroup="S2"></asp:RequiredFieldValidator>--%>
                                                            <asp:Image ID="imgtxtEMAIL" runat="server" Visible="false" ImageUrl="images.png" Height="15" Width="15"></asp:Image>
                                                            <asp:Image ID="imgtxtEMAILno" runat="server" ImageUrl="no.png" Height="15" Width="15" Visible="false"></asp:Image>
														</p></div> 
                                                    <div class="col-md-6">
                                                        <h4> Bus Phone <%--<span class="required">* </span>--%></h4>
														<p> <asp:TextBox runat="server" ID="txtBusPhone1"  class="form-control" AutoPostBack="true" OnTextChanged="txtBusPhone1_TextChanged"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtBusPhone1" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator14" ForeColor="Red" runat="server" ErrorMessage="Supplier Name Required" ControlToValidate="txtBusPhone1" ValidationGroup="S2"></asp:RequiredFieldValidator>--%></p> 
                                                        <asp:Image ID="imgtxtBusPhone1" runat="server" Visible="false" ImageUrl="images.png" Height="15" Width="15"></asp:Image>
                                                        <asp:Image ID="imgtxtBusPhone1no" runat="server" Visible="false" ImageUrl="no.png" Height="15" Width="15"></asp:Image>
												</div> 

												</div> 
                                                


											</div> 

										</div> 
        
                                          </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="txtSupplierName" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtMobileNO" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtEMAIL" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtBusPhone1" EventName="TextChanged" />
    </Triggers>
    
</asp:UpdatePanel>          
        
    <div class="modal-footer">
        <asp:LinkButton ID="lbButton1" class="btn green" runat="server" Visible="true" OnClick="lbButton1_Click" >Add New</asp:LinkButton>

         </div>
										
                                        
                                             
											
											 
										

									</div> </div> 
      
    </panel>
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
    BackgroundCssClass="modalBackground" CancelControlID="ImageButton1" Enabled="True"
    PopupControlID="pnlresponsive1" TargetControlID="LinkAccount1">
</cc1:ModalPopupExtender>

