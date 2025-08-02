<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="UserMaster.aspx.cs" Inherits="Web.CRM.UserMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

       <%-- <ul class="page-breadcrumb breadcrumb">
            <li>
                <a href="index.aspx">CRM</a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <a href="#">USER MASTER</a>
            </li>
        </ul>--%>
         <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>

                        <asp:Label ID="lblMsg" runat="server"></asp:Label>

                    </div>
                </asp:Panel>
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="icon-basket font-green-sharp"></i>
                                <span class="caption-subject font-green-sharp bold uppercase"> User Master</span>
                            </div>
                            <div class="actions btn-set">
                                <asp:Button ID="btnSubmit" runat="server" class="btn green-haze btn-circle" OnClick="btnSave_Click" Text="Add" ValidationGroup="submit" />
                                <asp:Button ID="btnAdd" Visible="false" runat="server" class="btn green-haze btn-circle" Text="Add New" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="tabbable">
                                <div class="tab-content no-space">
                                    <div class="tab-pane active" id="tab_general1">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">User Name</label><div class="col-md-4">
                                                    <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator7" runat="server" ErrorMessage="User Name is Required" ControlToValidate="txtUserId" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    
                                                </div>

                                                <label class="col-md-2 control-label">First Name</label><div class="col-md-4">
                                                    <asp:TextBox ID="txtFirsName" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator2" runat="server" ErrorMessage="First Name is Required" ControlToValidate="txtFirsName" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Last Name</label><div class="col-md-4">
                                                    <asp:TextBox ID="txtlastname" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Last Name is Required" ControlToValidate="txtlastname" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>

                                                <label class="col-md-2 control-label">First Name1</label><div class="col-md-4">
                                                    <asp:TextBox ID="txtFirsName1" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator4" runat="server" ErrorMessage="First Name1 is Required" ControlToValidate="txtFirsName1" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Last Name1</label><div class="col-md-4">
                                                   <asp:TextBox ID="txtlastname1" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Last Name1 is Required" ControlToValidate="txtlastname1" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>

                                                <label class="col-md-2 control-label">First Name2</label><div class="col-md-4">
                                                    <asp:TextBox ID="txtFirstName2" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator1" runat="server" ErrorMessage="First Name2 is Required" ControlToValidate="txtFirstName2" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Last Name2</label><div class="col-md-4">
                                                    <asp:TextBox ID="txtLastName2" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorLastName2" runat="server" ErrorMessage="Last Name2 is Required" ControlToValidate="txtLastName2" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>

                                                <label class="col-md-2 control-label">Login Id</label><div class="col-md-4">
                                                    <asp:TextBox ID="txtloginid" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorLoginId" runat="server" ErrorMessage="Login id is Required" ControlToValidate="txtloginid" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Password</label><div class="col-md-4">
                                                    <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="Password is Required" ControlToValidate="txtpassword" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>

                                                <label class="col-md-2 control-label">User Type</label><div class="col-md-4">
                                                    <asp:DropDownList ID="drpUsertype" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Remarks</label><div class="col-md-10">
                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorRemarks" runat="server" ErrorMessage="Remark is Required" ControlToValidate="txtRemark" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div> 

                                                <label class="col-md-2 control-label">Active_Flag</label><div class="col-md-4">
                                                    <asp:CheckBox ID="ckbActiveFlag" runat="server" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Upload Signature</label><div class="col-md-4">
                                                    <asp:FileUpload ID="FUDoc" runat="server" CssClass="form-control" style="padding-top: 0px;" />
                                                   
                                                    <%--<asp:Image ID="Image1" Visible ="false" runat="server" />--%>
                                                </div>
                                                <label class="col-md-2 control-label">Last Login Date</label><div class="col-md-4">
                                                    <asp:TextBox Placeholder="MM/DD/YYYY" ID="txtlastlogin" runat="server" CssClass="form-control"></asp:TextBox><cc1:calendarextender id="Calendarextender1" runat="server" enabled="True" popupbuttonid="calender" targetcontrolid="txtlastlogin"></cc1:calendarextender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtlastlogin" ErrorMessage="date is Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                                <%--<label class="col-md-2 control-label">User Detail Id</label><div class="col-md-4">
                                                    <asp:DropDownList ID="drpUserdetal" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                </div>--%>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Acc Lock</label><div class="col-md-4">
                                                    <asp:CheckBox ID="ckbAccLock" Enabled ="false" runat="server" />
                                                </div>
                                                <label class="col-md-2 control-label">First Time</label><div class="col-md-4">
                                                    <asp:CheckBox ID="ckbFirsttime" runat="server" />
                                                </div>                                                
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Password Change</label><div class="col-md-4">
                                                    <asp:TextBox ID="txtPasswordChng" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>

                                                <label class="col-md-2 control-label">Theme Name</label><div class="col-md-4">
                                                    <asp:TextBox ID="txtThemeName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Approvel Date</label><div class="col-md-4">
                                                    <asp:TextBox Placeholder="MM/DD/YYYY" ID="txtApprovalDate" runat="server" CssClass="form-control"></asp:TextBox><cc1:calendarextender id="Calendarextender2" runat="server"  enabled="True" popupbuttonid="calender" targetcontrolid="txtApprovalDate"></cc1:calendarextender>                                                    
                                                </div>

                                                <label class="col-md-2 control-label">Verification Code</label><div class="col-md-4">
                                                    <asp:TextBox ID="txtVerification" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Till Date</label><div class="col-md-4">
                                                    <asp:TextBox Placeholder="MM/DD/YYYY" ID="txtTilldate" runat="server" CssClass="form-control"></asp:TextBox><cc1:calendarextender id="Calendarextender4" runat="server" enabled="True" popupbuttonid="calender" targetcontrolid="txtTilldate"></cc1:calendarextender>                                                    
                                                    
                                                </div>

                                                <label class="col-md-2 control-label">Email Address</label><div class="col-md-4">
                                                    <asp:TextBox ID="txtEmailAddres" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>



                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
              
                    <div class="modal fade" id="portlet-config" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                    <h4 class="modal-title"></h4>
                                </div>
                                <div class="modal-body">
                                    Widget settings form goes here
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn blue">Save changes</button>
                                    <button type="button" class="btn default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                <ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">CRM</a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">User Master List</a>
                    </li>
                </ul>


                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="icon-basket font-green-sharp"></i>
                                <span class="caption-subject font-green-sharp bold uppercase">User Master List
                                </span>
                            </div>
                            <%--<div class="actions btn-set">
                                <asp:Button ID="addNew" runat="server" Text="Add New" OnClick="addNew_Click" />
                                <asp:Button ID="Button1" runat="server" class="btn green-haze btn-circle" Text="Cancel" />
                            </div>--%>
                        </div>
                        <div class="portlet-body">
                            <div class="tabbable">
                                <table class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>Action</th>
                                            <th>User Name</th>
                                            <th>Login Id</th>
                                            <th>Password</th>
                                            <th>Signature</th>


                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="listUserMaster" runat="server" OnItemCommand="ListUserMaster_ItemCommand">
                                            <LayoutTemplate>
                                                <tr id="ItemPlaceholder" runat="server">
                                                </tr>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                     <td>
                                                                    <div class="btn-group">
                                                                        <a data-toggle="dropdown" href="#" class="btn btn-sm blue dropdown-toggle" style="width: 60px;">Action <i class="fa fa-angle-down"></i>
                                                                        </a>
                                                                        <ul class="dropdown-menu">
                                                                            <li>
                                                                                <asp:LinkButton ID="LinkButton1" CommandName="btnEdit" CommandArgument='<%# Eval("USER_ID")%>' PostBackUrl='<%# "UserMaster.aspx?USER_ID="+ Eval("USER_ID")%>' runat="server"><i class="fa fa-pencil"></i>Edit</asp:LinkButton>

                                                                            </li>
                                                                            <li>
                                                                                <asp:LinkButton ID="LinkButton2" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("USER_ID")%>' runat="server"> <i class="fa fa-pencil"></i>Delete</asp:LinkButton>

                                                                            </li>
                                                                           
                                                                        </ul>
                                                                    </div>
                                                                </td>




                                                  
                                                    <td>
                                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("FIRST_NAME")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblLoginId" runat="server" Text='<%# Eval("LOGIN_ID")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("PASSWORD")%>'></asp:Label></td>
                                                     <td>
                                                         <asp:Image ID="lblAttachmentPath" Width="30px" Height="30px" ImageUrl='<%# "../Gallery/" + Eval("SignatureImage")  %>' runat="server"></asp:Image></td>
                                                       
                                                   
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
            >
        </div>
    </div>

</asp:Content>
