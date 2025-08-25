<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Summercops.aspx.cs" Inherits="Web.ReportMst.Summercops" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <title>Summirized</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta content="Preview page of Metronic Admin Theme #4 for buttons extension demos" name="description" />
    <meta content="" name="author" />
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&amp;subset=all" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/layouts/layout4/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/layouts/layout4/css/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="assets/layouts/layout4/css/custom.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="row" style="margin-left: 0px; margin-right: 10px;">
            <br />
            <br />
           
                                                <div class="row">
                                                   
                                                    <div class="col-md-8">
                                                        <div class="col-md-1">
                                                        <asp:CheckBox ID="chkyear" runat="server" OnCheckedChanged="chkyear_CheckedChanged" AutoPostBack="true"/>
                                                   </div>
                                                        <div class="col-md-7">
                                                            <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label3" CssClass="col-md-3 control-label" Text="Year"></asp:Label>
                                                            <div class="col-md-9">
                                                                <asp:DropDownList ID="drplistyear" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>


                                                            </div>
                                                        </div>
                                                        </div>
                                                        
                                                    </div>
                                                </div>
                                                <div class="row">
                                                       
                                                    <div class="col-md-6">
                                                        <div class="col-md-1">
                                                            <asp:CheckBox ID="chkdates" runat="server" OnCheckedChanged="chkdates_CheckedChanged" AutoPostBack="true"/>
                                                       </div>
                                                        <div class="col-md-5">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="lblActive1s" class="col-md-7 control-label" Text="Start Date"></asp:Label>

                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="txtdateFrom" Placeholder="dd/MMM/yyyy" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="TextBoxtxtdateFrom_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateFrom" Format="dd/MMM/yyyy"></cc1:CalendarExtender>
                                                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtdateFrom" ValidChars="/" FilterType="Custom, numbers" runat="server" />--%>
                                                            </div>
                                                        </div>
                                                            </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label22" class="col-md-4 control-label" Text="To Date"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtdateTO" Placeholder="dd/MMM/yyyy" runat="server" CssClass="form-control input-medium"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtendertxtdateTO" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdateTO" Format="dd/MMM/yyyy"></cc1:CalendarExtender>
                                                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtdateTO" ValidChars="/" FilterType="Custom, numbers" runat="server" />--%>
                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label11" class="col-md-4 control-label" Text="From Category"></asp:Label>

                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpcategoryfrom" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpcategoryfrom" ErrorMessage="select Category "></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label14" class="col-md-4 control-label" Text="To Category"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpcategoryto" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpcategoryto" ErrorMessage="select Category"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label17" class="col-md-4 control-label" Text="From Sub Category"></asp:Label>

                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpscategoryfrom" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpscategoryfrom" ErrorMessage="select sub Category"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label16" class="col-md-4 control-label" Text="To Sub Category"></asp:Label>

                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpscategoryto" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpscategoryto" ErrorMessage="select sub Category"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label26" class="col-md-4 control-label" Text="From Location"></asp:Label>

                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drplocationfrom" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drplocationfrom" ErrorMessage="select Location"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label19" class="col-md-4 control-label" Text="To Location"></asp:Label>

                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drplocationto" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drplocationto" ErrorMessage="select Location"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label20" class="col-md-4 control-label" Text="From Feedback Type"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpFeedbackypefrom" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>


                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label21" class="col-md-4 control-label" Text="To Feedback Type"></asp:Label>

                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpFeedbackypeto" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label1" class="col-md-4 control-label" Text="From Department"></asp:Label>

                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpdepartmentfrom" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpdepartmentfrom" ErrorMessage="select Department"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">

                                                            <asp:Label runat="server" ID="Label2" class="col-md-4 control-label" Text="To Department"></asp:Label>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="drpdepartmentto" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpdepartmentto" ErrorMessage="select Department"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label23" class="col-md-4 control-label" Text="From Reported By"></asp:Label>

                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpreportfrom" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpreportfrom" ErrorMessage="Select reportedby"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label18" class="col-md-4 control-label" Text="To Reported By"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="drpreportto" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Ticks" Display="Dynamic" ForeColor="#a94442" ControlToValidate="drpreportto" ErrorMessage="Select reportedby"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group" style="color: ">
                                                            <asp:Label runat="server" ID="Label29" class="col-md-4 control-label" Text="Text in Feedback"></asp:Label>

                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txtFeedback" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtFeedback" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-1"></div>
                                                    <div class="col-md-3">
                                                        <asp:Button ID="btnsubmit" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnsubmit_Click" ValidationGroup="Ticks" />
                                                    </div>
                                                </div>                                                


                                               
                                                        <div class ="row">
                                                           
                                                             <div class ="col-md-6">
                                                                  <div class="form-group" style="color: ">
                                                                 <asp:Label runat="server" ID="Label4" class="col-md-4 control-label" Text="Email"></asp:Label>

                                                                 
                                                                 <div class="col-md-8">
                                                                      <asp:TextBox ID="txtpmail" runat="server" placeholder="Enter E-mail" ValidationGroup="tmail" CssClass="form-control"></asp:TextBox>
                                                                 </div>
                                                                    </div>                                                         
                                                              
                                                             </div>
                                                            
                                                       
                                                             <div class="col-md-1"></div>
                                                              <div class="col-md-3">
                                                                     <asp:Button ID="btnmail" runat="server" Text="Send by mail" CssClass="btn btn-info" OnClick="btnmail_Click" ValidationGroup="tmail" />
                                                                 </div> 
                                                        
                                                      </div> 
            <hr />
            <br />
            <div class="col-md-6">
                <div class="portlet box blue">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Department
                        </div>

                    </div>
                    <div class="portlet-body">

                        <table class="table table-bordered table-striped" id="sample_1">
                            <thead>
                                <tr style="background-color: #337ab7;">
                                    

                                    <th>Feedback
                                    </th>
                                    <th>Date
                                    </th>
                                    <th>Department
                                    </th>
                                    <th>Category
                                    </th>
                                </tr>

                            </thead>
                            <tbody>
                                <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListView1_ItemCommand1">
                                    <ItemTemplate>
                                        <tr>
                                          
                                            <td>
                                                 <asp:LinkButton ID="lnkpn" runat="server" CommandName="lnkpn" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                    <asp:Label ID="lblFeedback" runat="server" Text='<%#getFeedbackname(Convert .ToInt32( Eval("Feedback")))%>'></asp:Label>
                                                    <%--<asp:Label ID="lblm1" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>--%>
                                                </asp:LinkButton>
                                                <%--<asp:Label ID="lblDeptID" runat="server" Text='<%# GetDept(Eval("COMMANID").ToString(),Eval("typee").ToString()) %>'></asp:Label>--%>
                                               
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkmrn" runat="server" CommandName="lnkmrn" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                    <asp:Label ID="lbldate" runat="server" Text='<%# Eval("UploadDate")%>'></asp:Label>
                                                    <%--   <asp:Label ID="lblm2" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>--%>
                                                </asp:LinkButton>

                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkdept" runat="server" CommandName="lnkdept" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                    <asp:Label ID="lbltickdk" runat="server" Text='<%#getdepartmentname(Convert .ToInt32( Eval("Department")))%>'></asp:Label>
                                                    <%-- <asp:Label ID="lblm3" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>--%>
                                                </asp:LinkButton>

                                            </td>
                                            <td>
                                               <asp:LinkButton ID="lnkcat" runat="server" CommandName="lnkcat" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                    <asp:Label ID="lbltickcat" runat="server" Text='<%#getcategoryname(Convert .ToInt32( Eval("Category")))%>'></asp:Label>
                                                    <%-- <asp:Label ID="lblm4" runat="server" Text='<%# Eval("MasterCODE") %>' Visible="false"></asp:Label>--%>
                                                </asp:LinkButton>

                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                            </tbody>

                        </table>

                    
                    </div>
                </div>
            </div>

           
        </div>
        


    </form>
    <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/table-datatables-buttons.min.js" type="text/javascript"></script>
    <script src="assets/layouts/layout4/scripts/layout.min.js" type="text/javascript"></script>
    <script src="assets/layouts/layout4/scripts/demo.min.js" type="text/javascript"></script>
    <script src="assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
    <script src="assets/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>
</body>

</html>
