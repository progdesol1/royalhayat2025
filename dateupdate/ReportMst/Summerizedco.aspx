<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Summerizedco.aspx.cs" Inherits="Web.ReportMst.Summerizedco" %>

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
                                                                <asp:CheckBox ID="chkyear" runat="server" OnCheckedChanged="chkyear_CheckedChanged" AutoPostBack="true" />
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
                                                                <asp:CheckBox ID="chkdates" runat="server" OnCheckedChanged="chkdates_CheckedChanged" AutoPostBack="true" />
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
                                                                <asp:Label runat="server" ID="Label20" class="col-md-4 control-label" Text="From Complain Type"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpcomplaintypefrom" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>


                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group" style="color: ">
                                                                <asp:Label runat="server" ID="Label21" class="col-md-4 control-label" Text="To Complain Type"></asp:Label>

                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="drpcomplaintypeto" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>

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
                                                                <asp:Label runat="server" ID="Label29" class="col-md-4 control-label" Text="Text in complain"></asp:Label>

                                                                <div class="col-md-8">
                                                                    <asp:TextBox ID="txtcomplain" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="Dynamic" ForeColor="#a94442" ControlToValidate="txtcomplain" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-1"></div>
                                                        <div class="col-md-3">
                                                            <asp:Button ID="btnsubmit" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnsubmit_Click" ValidationGroup="Ticks" />
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-md-6">
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
                <div class="portlet-title">
                    <div class="caption font-dark">
                        <i class="icon-settings font-dark"></i>
                        <span class="caption-subject bold uppercase">Complaint Report</span>
                    </div>
                    <div class="tools"></div>
                </div>
                <div class="portlet-body">
                    <table class="table table-bordered table-striped" id="sample_1">
                        <thead>
                            <tr>
                                <th></th>
                                <th>ACTION</th>
                                <th style="text-align: center"><strong>Complain</strong></th>
                                <th style="text-align: center"><strong>Date</strong></th>
                                <th style="text-align: center"><strong>Department</strong></th>
                                <th style="text-align: center"><strong>Category</strong></th>
                                <%--  <th style="text-align: center"><strong>Sub Category</strong></th>--%>
                                <th style="text-align: center"><strong>Remark</strong></th>
                                <th style="text-align: center"><strong>Reported By</strong></th>


                            </tr>
                        </thead>
                        <tbody>

                            <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListView1_ItemCommand">
                                <ItemTemplate>
                                    <tr>

                                        <td></td>
                                        <td>
                                            <asp:Button ID="lnkpn" runat="server" Text="EDIT" CommandName="lnkpn" CommandArgument='<%# Eval("MasterCODE")%>' /></td>
                                        <td style="text-align: center;">
                                            <asp:LinkButton ID="lnkpn6" runat="server" CommandName="lnkpn6" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                <asp:Label ID="lblcomplain" runat="server" Text='<%#getcomplainname(Convert .ToInt32( Eval("complain")))%>'></asp:Label>
                                                <asp:Label ID="lblm1" Visible="false" runat="server" Text='<%# Eval("MasterCODE") %>'></asp:Label>
                                            </asp:LinkButton>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:LinkButton ID="lnkmrn" runat="server" CommandName="lnkmrn" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                <asp:Label ID="lbldate" runat="server" Text='<%# Eval("UploadDate")%>'></asp:Label>
                                                <asp:Label ID="lblm2" Visible="false" runat="server" Text='<%# Eval("MasterCODE") %>'></asp:Label>
                                            </asp:LinkButton>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:LinkButton ID="lnkdept" runat="server" CommandName="lnkdept" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                <asp:Label ID="lbltickdk" runat="server" Text='<%#getdepartmentname(Convert .ToInt32( Eval("Department")))%>'></asp:Label>
                                                <asp:Label ID="lblm3" Visible="false" runat="server" Text='<%# Eval("MasterCODE") %>'></asp:Label>
                                            </asp:LinkButton>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:LinkButton ID="lnkcat" runat="server" CommandName="lnkcat" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                <asp:Label ID="lbltickcat" runat="server" Text='<%#getcategoryname(Convert .ToInt32( Eval("Category")))%>'></asp:Label>
                                                <asp:Label ID="lblm4" Visible="false" runat="server" Text='<%# Eval("MasterCODE") %>'></asp:Label>
                                            </asp:LinkButton></td>
                                        <%--  <td style="text-align: center;">
                                                            <asp:Label ID="Label3" runat="server" Text='<%#getsubcategoryname(Convert .ToInt32( Eval("SubCategory")))%>'></asp:Label></td>--%>
                                        <td style="text-align: center;">
                                            <asp:LinkButton ID="lnkremind" runat="server" CommandName="lnkremind" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                <asp:Label ID="lblremidernote" runat="server" Text='<%# Eval("REMINDERNOTE") %>'></asp:Label>
                                                <asp:Label ID="lblm5" Visible="false" runat="server" Text='<%# Eval("MasterCODE") %>'></asp:Label>
                                            </asp:LinkButton></td>
                                        <td style="text-align: center;">
                                            <asp:LinkButton ID="lnkreport" runat="server" CommandName="lnkreport" CommandArgument='<%# Eval("MasterCODE")%>'>
                                                <asp:Label ID="lblreports" runat="server" Text='<%#getReportname(Convert .ToInt32( Eval("ReportedBy")))%>'></asp:Label>
                                                <asp:Label ID="lblm6" Visible="false" runat="server" Text='<%# Eval("MasterCODE") %>'></asp:Label>
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
