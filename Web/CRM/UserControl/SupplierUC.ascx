<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierUC.ascx.cs" Inherits="Web.CRM.UserControl.SupplierUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <script type="text/javascript">
        function PRBarBlock() {
            document.getElementById("Bar").style.display = "block";
        }

        function PRBar() {
            document.getElementById("Bar").style.display = "none";
        }
    </script>
<asp:LinkButton ID="LinkAccount1" runat="server" data-toggle="tooltip" ToolTip="Add">
<i class="icon-plus" style="color:black;padding-left: 4px;"></i></asp:LinkButton>
<panel id="pnlresponsive1" style="padding: 1px; background-color: #fff; border: 1px solid #000; display: none" runat="server">
<%--<panel id="responsive" class="modal fade" tabindex="-1" aria-hidden="true" runat="server">--%>
    <div class="modal-dialog" style="position:fixed;left:20%;top:10px">
									<div class="modal-content">
										<div class="modal-header">
										
											<h4 class="modal-title">ADD SUPPLIER</h4>
                                            <asp:Label ID="Label1" runat="server" Visible="false" ForeColor="#a94442" Text="Supplier is allready exist..."></asp:Label>
										</div>
										<div class="modal-body">
											<div class="scroller" style="height:250px" data-always-visible="1" data-rail-visible1="1">
                                                <div class="col-md-12">														
														    <p>
                                                                <%--<asp:TextBox runat="server" ID="txtserach" Visible="false" Enabled="false" ForeColor="Red" CssClass="col-md-6 form-control" BorderStyle="None" style="background-color:white;"></asp:TextBox>--%>
                                                                <img id="Bar" src="../assets/admin/layout4/img/image_1210200.gif" style="width:100%;height:18px;display:none;"/>
														    </p> 
                                                        </div>
												<div class="row">
                                                    <div class="col-md-6">
														<h4>Supplier Name <span class="required">* </span></h4>
														<p>
                                                            <asp:TextBox runat="server" ID="txtSupplierName"  class="col-md-12 form-control"></asp:TextBox>
                                                            <asp:TextBox ID="lblSupplierNames" runat="server" Visible="false" Enabled="false" CssClass="col-md-6 form-control" style="background-color:white;border:none;color:#a94442" Text="Supplier Required"></asp:TextBox>
														</p> 

                                                    </div> 
                                                    <div class="col-md-6">
                                                        <h4>Mobile NO <span class="required">* </span></h4>
														<p> <asp:TextBox runat="server" ID="txtMobileNO" class="form-control"></asp:TextBox>     
                                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtMobileNO" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />                                                       
                                                            <asp:TextBox ID="lblMobileNOs" runat="server" Visible="false" Enabled="false" CssClass="col-md-6 form-control" style="background-color:white;border:none;color:#a94442"  Text="Mobile Required"></asp:TextBox>
                                                            </p> </div> <div class="col-md-6">
                                                         <h4>Address <span class="required">* </span></h4>
														<p>  <asp:TextBox  ID="txtAddress1" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                                        </p>
                                                    </div>
                                                    <div class="col-md-6">
														<h4>Email <span class="required">* </span></h4>
														<p>
															<asp:TextBox ID="txtEMAIL" runat="server" class="col-md-12 form-control"></asp:TextBox>
                                                            <asp:TextBox ID="lblemails" ForeColor="#a94442" runat="server" Visible="false" Enabled="false" CssClass="col-md-6 form-control" style="background-color:white;border:none;color:#a94442" Text="E_Mail Required"></asp:TextBox>
														</p></div> <div class="col-md-6">
                                                        <h4> Bus Phone <span class="required">* </span></h4>
														<p> <asp:TextBox runat="server" ID="txtBusPhone1" class="form-control"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtBusPhone1" ValidChars="0123456789" FilterType="Custom, numbers" runat="server" />
                                                            <asp:TextBox ID="BusPhones" runat="server" Visible="false" Enabled="false" CssClass="col-md-6 form-control" style="background-color:white;border:none;color:#a94442" Text="BusPhone Required"></asp:TextBox>
                                                        </p>
												</div> </div> </div> </div> 
                                        <div class="modal-footer">
                                             <asp:Button ID="Button2" runat="server" data-dismiss="modal" class="btn default"  Text="Cancel" />
											
											 <asp:LinkButton ID="lbButton1" class="btn green" runat="server" OnClick="lbButton1_Click" OnClientClick="PRBarBlock()">Add New</asp:LinkButton>
										</div></div> </div> 
      
    </panel>
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" DynamicServicePath=""
    BackgroundCssClass="modalBackground" CancelControlID="Button2" Enabled="True"
    PopupControlID="pnlresponsive1" TargetControlID="LinkAccount1">
</cc1:ModalPopupExtender>

