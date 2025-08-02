<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventUC.ascx.cs" Inherits="Web.CRM.UserControl.EventUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .bigimg {
        background-image: url('../images/IMGPOP.jpg');
    }
</style>
<asp:LinkButton ID="LinkAccount1" runat="server" CssClass="btn default">Registration <i class="icon-plus" style="color:black;padding-left: 4px;"></i></asp:LinkButton>
<%--<asp:LinkButton ID="LinkAccount1" runat="server" data-toggle="tooltip" ToolTip="Add" CssClass="btn ">
<i class="icon-plus" style="color:black;padding-left: 4px;font-size: 25px;" ></i></asp:LinkButton>--%>

<panel id="pnlresponsive1" style="padding: 1px; background-color: #fff; border: 1px solid #000; display: none" runat="server">
<%--<panel id="responsive" class="modal fade" tabindex="-1" aria-hidden="true" runat="server">--%>
    
    <div class="modal-dialog">
        
									<div class="modal-content">
                                        <div style="background-image: url('CRM/images/IMGPOP.jpg')">
                                           
                                             
                                            
                                       
                                         <div class="modal-header">
										
											<h4 class="modal-title" style="color:#fff;"><strong>Add Customer</strong></h4>
                                            <asp:ImageButton ID="imgRefresh" runat="server" ImageUrl="~/CRM/images/Button-Refresh.ico" Height="25" Width="25" OnClick="imgRefresh_Click"></asp:ImageButton>                                 
                                             <asp:ImageButton ID="ImageButton1" data-dismiss="modal" ImageUrl="Close.jpg" Height="25" Width="25" runat="server" style="margin-left: 10px;"></asp:ImageButton>
                                            
										</div>
                                       
<asp:UpdatePanel ID="UpdatePanel1" runat="server">    
    <ContentTemplate>
        
										<div class="modal-body">
											<div class="scroller" style="height:6%" data-always-visible="1" data-rail-visible1="1">
												<div class="row">
                                                    <asp:Panel ID="PanelCustomer" runat="server" Visible="false">
                                                    <div class="col-md-12" >
                                                    <table class="table table-bordered" id="datatable1">
                                                        <thead>
                                                            <tr style="color:#ffffff;">
                                                            <th>Attendee</th>
                                                            <th>Mobile</th>
                                                            <th>Email ID</th>
                                                            <th>Bus Number </th>
                                                                </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:ListView ID="CustomerList" runat="server" OnItemCommand="CustomerList_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr style="font-size:14px;color:yellow;font-weight:bolder;">
                                                                        <td><%#Eval("PersName1") %></td>
                                                                        <td><%#Eval("MOBPHONE") %></td>
                                                                        <td><%#Eval("EMAIL1") %></td>
                                                                        <td><%#Eval("BUSPHONE1") %></td>
                                                                        <td><asp:LinkButton ID="btnEdit" runat="server" CssClass="btn green-haze " CommandName="Modify" CommandArgument='<%# Eval("ContactMyID") %>'>Edit</asp:LinkButton></td>                                                                  
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>                                                            
                                                        </tbody>
                                                    </table>

                                                    </div>
                                                        </asp:Panel>
                                                    <%-- <div class="col-md-12">
														
														<p>
                                                            <asp:TextBox runat="server" ID="txtserach" CssClass="col-md-6 form-control" placeholder="Search..."></asp:TextBox>                                
                                                            <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn blue"></asp:Button>
														</p> 

                                                     </div>--%>
                                                    <asp:Panel DefaultButton="lkbCustomerN1" ID="Panel1" runat="server">                                                        
                                                        <div class="col-md-12">
                                                        <div class="col-md-4" style="margin-left: -10px;">
                                                           <h4 style="margin-left: 10px; color:#fff;"><strong>Company Search </strong><span class="required">* </span> </h4>
                                                            <p>
                                                                <div class="col-md-12">
                                                                <asp:TextBox ID="txtcompneySerch" runat="server" AutoCompleteType="Disabled" CssClass="form-control" placeholder="Company Search" MaxLength="250">
                                                                </asp:TextBox>                                                                                                                
                                                                <asp:LinkButton ID="lkbCustomerN1" CssClass="btn btn-icon-only yellow" runat="server" OnClick="lkbCustomerN1_Click" ><i class="fa fa-search" ></i>
                                                                </asp:LinkButton>                                                               
                                                                </div>                                                                
                                                            </p>                                                                      
                                                        </div>
                                                        <div class="col-md-4" style="margin-left: 10px;">
                                                            <h4 style="color:#fff;"><strong>Company Name </strong><span class="required">* </span></h4>
                                                            <p>                                                        
                                                        
                                                                <asp:Label ID="Label2" runat="server" ></asp:Label>
                                                                <asp:DropDownList ID="drpCompnay" runat="server" CssClass="form-control select2me" AutoPostBack="true" OnSelectedIndexChanged="drpCompnay_SelectedIndexChanged">
                                                                </asp:DropDownList>                                                                                    
                                                       
                                                            </p>                                                     
                                                        </div>
                                                        <div class="col-md-4">
                                                            <h4 style="color:#fff;"><strong>Position </strong><span class="required">* </span></h4>
                                                                <p>                                                                                    
                                                                    <%-- <div class="input-group" style="text-align: left">--%>
                                                                    <asp:DropDownList ID="drpItManager" runat="server" CssClass="form-control select2me" ></asp:DropDownList>
                                                                                   
                                                                  <%--  <asp:LinkButton ID="LinkButton3" runat="server" >
                                                                    <i class="icon-plus " style="color:black;padding-left: 4px;"></i>
                                                                    </asp:LinkButton>--%>
                                                                    <%--</div>  --%>                                                                                
                                                                </p>                                                                            
                                                        </div>
                                                        </div>
                                                        <div class="col-md-12">														
														    <p>
                                                                <asp:TextBox runat="server" ID="txtserach" Visible="false" Enabled="false" ForeColor="Green" CssClass="col-md-6 form-control" BorderStyle="None" style="background-color:white;"></asp:TextBox>                                                                                           
														    </p> 
                                                        </div>
                                                    </asp:Panel>

                                                    <div class="col-md-12" style="margin-top: 10px;">
                                                    <div class="col-md-6">
														<h4 style="color:#fff;"><strong>Attendees </strong><span class="required" style="color:red;">* </span></h4>
														<p>
                                                            <asp:TextBox runat="server" ID="txtSupplierName1" class="col-md-12 form-control" AutoPostBack="true" OnTextChanged="txtSupplierName_TextChanged"></asp:TextBox>                                
                                                            <asp:Image ID="imgtxtSupplierName" runat="server" ImageUrl="images.png" Height="15" Width="15" Visible="false"></asp:Image>
                                                            <asp:Image ID="imgimgtxtSupplierNameNo" runat="server" ImageUrl="no.png" Height="15" Width="15" Visible="false"></asp:Image>
															
														</p> </div>
                                                     <div class="col-md-6">
                                                        <h4 style="color:#fff;"><strong>Mobile NO </strong><span class="required" style="color:red;">* </span></h4>                                                         
														<p> <asp:TextBox runat="server" ID="txtMobileNO"  class="form-control" AutoPostBack="true" OnTextChanged="txtMobileNO_TextChanged"></asp:TextBox>                                
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtMobileNO" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                            <asp:Image ID="imgtxtMobileNO" runat="server" Visible="false" ImageUrl="no.png" Height="15" Width="15"></asp:Image>
                                                            <asp:Image ID="imgtxtMobile" runat="server" Visible="false" ImageUrl="images.png" Height="15" Width="15"></asp:Image>
                                                            </p> 
                                                     </div>
                                                   
                                                     <div class="col-md-6">
                                                         <h4 style="color:#fff;"><strong> Address </strong><span class="required" style="color:red;">* </span></h4>
														<p>  <asp:TextBox  ID="txtAddress1" runat="server" class="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>                                
														</p> 
                                                    </div>
                                                    <div class="col-md-6">
														<h4 style="color:#fff;"><strong>Email </strong><span class="required" style="color:red;">* </span></h4>
														<p>
															<asp:TextBox ID="txtEMAIL" runat="server" class="col-md-12 form-control" AutoPostBack="true" OnTextChanged="txtEMAIL_TextChanged"></asp:TextBox>
                               <%-- <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" ForeColor="Red" runat="server" ErrorMessage="Email Required" ControlToValidate="txtEMAIL" ValidationGroup="S2"></asp:RequiredFieldValidator>--%>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEMAIL" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" ErrorMessage="Email Is Not Valid"></asp:RegularExpressionValidator>
                                                            <asp:Image ID="imgtxtEMAIL" runat="server" Visible="false" ImageUrl="images.png" Height="15" Width="15"></asp:Image>
                                                            <asp:Image ID="imgtxtEMAILno" runat="server" ImageUrl="no.png" Height="15" Width="15" Visible="false"></asp:Image>
														</p></div> 
                                                    <div class="col-md-6">
                                                        <h4 style="color:#fff;"><strong>Bus Phone </strong><span class="required" style="color:red;">* </span></h4>
														<p> <asp:TextBox runat="server" ID="txtBusPhone1"  class="form-control" AutoPostBack="true" OnTextChanged="txtBusPhone1_TextChanged"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtBusPhone1" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                <%--<asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator14" ForeColor="Red" runat="server" ErrorMessage="Bus Phone Required" ControlToValidate="txtBusPhone1" ValidationGroup="S2"></asp:RequiredFieldValidator>--%>
                                                        <asp:Image ID="imgtxtBusPhone1" runat="server" Visible="false" ImageUrl="images.png" Height="15" Width="15"></asp:Image>
                                                        <asp:Image ID="imgtxtBusPhone1no" runat="server" Visible="false" ImageUrl="no.png" Height="15" Width="15"></asp:Image>
                                                        </p> 
												</div> 
                                                    <div class="col-md-6">
                                                        <h4 style="color:#fff;"><strong>Attendees Image </strong></h4>
                                                        <asp:FileUpload ID="avatarUploadd" class="btn btn-circle green-haze btn-sm" runat="server"/>
                                                    </div>
                                                    </div>
												</div> 
                                                


											</div> 

										</div> 
                                          </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="txtSupplierName1" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtMobileNO" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtEMAIL" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtBusPhone1" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="imgRefresh" EventName="Click" />
    </Triggers>
    
</asp:UpdatePanel>          
         <div class="modal-footer">
        <asp:LinkButton ID="lbButton1" class="btn green" ValidationGroup="S2"  runat="server" Visible="true" OnClick="lbButton1_Click" Text="Add New"></asp:LinkButton>
             <%--<asp:LinkButton ID="lblBarCode" CssClass="btn yellow" runat="server" Visible="true" OnClick="lblBarCode_Click">Bar Code</asp:LinkButton>--%>
         </div>
   
										
                                        
                                             
											
											 
										
                                        </div>
									</div> </div> 
    
    </panel>
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
    BackgroundCssClass="modalBackground" CancelControlID="ImageButton1" Enabled="True"
    PopupControlID="pnlresponsive1" TargetControlID="LinkAccount1">
</cc1:ModalPopupExtender>




