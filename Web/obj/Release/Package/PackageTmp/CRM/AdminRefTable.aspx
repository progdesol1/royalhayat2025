<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="AdminRefTable.aspx.cs" Inherits="Web.CRM.AdminRefTable" %>

<%@ Register Assembly="LangTextBox" Namespace="ServerControl1" TagPrefix="Lang" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <script type="text/javascript">
        function showWarningToast() {
            var temp = document.getElementById("<%=ddlSystem.ClientID %>").value;
            if (temp == "-- Select --") {
                //alert("Please select the System.");
                var message = 'Localize(system)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
            var temp1 = document.getElementById("<%=ddldescrip.ClientID %>").value;
            if (temp1 == "-- Select --") {
                // alert("Please select the RefType.");
                var message = 'Localize(activity)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
        }
    </script>--%>

    <script type="text/javascript">
        function showWarningToast1() {
            var temp = document.getElementById("<%=txtRefType.ClientID %>").value;
            if (temp == "") {
                //alert("Please select the System.");
                var message = 'Localize(mc)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
            var temp1 = document.getElementById("<%=txtRefSubType.ClientID %>").value;
            if (temp1 == "") {
                // alert("Please select the RefType.");
                var message = 'Localize(sc)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
            var temp2 = document.getElementById("<%=txtShortname.ClientID %>").value;
            if (temp2 == "") {
                // alert("Please select the RefType.");
                var message = 'Localize(sname)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
            var temp1 = document.getElementById("<%=txtRefName.ClientID %>").value;
            if (temp1 == "") {
                // alert("Please select the RefType.");
                var message = 'Localize(des1)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
            var temp1 = document.getElementById("<%=txtRefNameO.ClientID %>").value;
            if (temp1 == "") {
                // alert("Please select the RefType.");
                var message = 'Localize(des2)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
            var temp1 = document.getElementById("<%=txtRefNameCH.ClientID %>").value;
            if (temp1 == "") {
                // alert("Please select the RefType.");
                var message = 'Localize(des3)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
            var temp1 = document.getElementById("<%=txtSwitch1.ClientID %>").value;
            if (temp1 == "") {
                // alert("Please select the RefType.");
                var message = 'Localize(switch1)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
            var temp1 = document.getElementById("<%=txtSwitch2.ClientID %>").value;
            if (temp1 == "") {
                // alert("Please select the RefType.");
                var message = 'Localize(switch2)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
            var temp1 = document.getElementById("<%=txtSortNo.ClientID %>").value;
            if (temp1 == "") {
                // alert("Please select the RefType.");
                var message = 'Localize(sortno)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
            var temp1 = document.getElementById("<%=txtRemarks.ClientID %>").value;
            if (temp1 == "") {
                // alert("Please select the RefType.");
                var message = 'Localize(Remarks)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
            var temp1 = document.getElementById("<%=drpactive.ClientID %>").value;
            if (temp1 == 0) {
                // alert("Please select the RefType.");
                var message = 'Localize(status)';
                $().toastmessage('showWarningToast', message);
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function ClearAll() {
            document.getElementById('ContentPlaceHolder1_ddlSystem').selectedIndex = "0";
            document.getElementById('ContentPlaceHolder1_ddlReftype').selectedIndex = "0";
            document.getElementById('ContentPlaceHolder1_ddlRefSubType').selectedIndex = "0";
            var Param1 = document.getElementById("ContentPlaceHolder1_ddlSystem").value;
            $(document).ready(function () {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "AdminRefTable.aspx/ClearGrid",
                    dataType: "json",
                    success: OnSuccess(result),
                    error: function (result) {
                    }
                });
            });
            document.getElementById('tbody').innerHTML = '';
            //document.getElementById('RefGridview').outerHTML = "";

        }
    </script>
    <script type="text/javascript">
        function ShowForm() {
            var add = document.getElementById('ContentPlaceHolder1_FrmView');
            var view = document.getElementById('ContentPlaceHolder1_pnlCreateForm');
            var view1 = document.getElementById('ContentPlaceHolder1_LstView');
            add.style.display = "none";
            view.style.display = "block";
            view1.style.display = "none";

        }

        function ShowGrid() {
            var add = document.getElementById('ContentPlaceHolder1_pnlCreateForm');
            var view = document.getElementById('ContentPlaceHolder1_FrmView');
            var view1 = document.getElementById('ContentPlaceHolder1_LstView');
            var e = document.getElementById('ContentPlaceHolder1_ddlSystem');
            //var check = e.options[e.selectedIndex];
            if (e.disabled == true) {
                view1.style.display = "block";
            }
            else {
                view1.style.display = "none";
            }
            add.style.display = "none";
            view.style.display = "block";
        }
        function ClearAll() {
            var a = (confirm('Are you sure you want to clear all data?'))
            if (a == true) {
                document.getElementById('ContentPlaceHolder1_txtRefType').value = "";
                document.getElementById('ContentPlaceHolder1_txtRefSubType').value = "";
                document.getElementById('ContentPlaceHolder1_txtShortname').value = "";
                document.getElementById('ContentPlaceHolder1_txtRefName').value = "";
                document.getElementById('ContentPlaceHolder1_txtRefNameO').value = "";
                document.getElementById('ContentPlaceHolder1_txtRefNameCH').value = "";
                document.getElementById('ContentPlaceHolder1_txtSwitch1').value = "";
                document.getElementById('ContentPlaceHolder1_txtSwitch2').value = "";
                document.getElementById('ContentPlaceHolder1_txtSortNo').value = "";
                document.getElementById('ContentPlaceHolder1_txtRemarks').value = "";
                document.getElementById('ContentPlaceHolder1_rbtnActiveNo').checked = true;
                document.getElementById('ContentPlaceHolder1_btnSubmitAd').value = "Save";
            }
        }

        function goForPostback() {
        }
    </script>
    <div>
        <%--<ul class="page-breadcrumb breadcrumb">
            <li>
                <a href="index.aspx">CRM</a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <a href="#">Admin RefTable</a>
            </li>

        </ul>--%>
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal form-row-seperated">


                    <asp:Panel ID="PanllistOfAdmin" runat="server">
                        <div class="portlet light">
                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>
                                        <asp:Label ID="Label16" runat="server" Text="Admin RefTable list"></asp:Label>
                                    </div>
                                    <%--<div class="actions btn-set">
                                <asp:Button ID="btnSubmit" runat="server" class="btn green-haze btn-circle" Text="Add" ValidationGroup="submit" />
                                <asp:Button ID="btnAdd" Visible="false" runat="server" class="btn green-haze btn-circle" Text="Add New" />
                                <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" Text="Cancel" />
                            </div>--%>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                         <asp:LinkButton ID="btnSyncro" runat="server" class="btn btn-success" Text="Synchronize" OnClick="btnSyncro_Click" />
                                         <asp:LinkButton ID="linkAddAdminEntry" OnClick="linkAddAdminEntry_Click" runat="server" class="btn btn-success" Text="Add New" />
                                        <asp:LinkButton ID="adminList" OnClick="adminList_Click" runat="server" class="btn btn-success" Text="Submit" />
                                        <asp:LinkButton ID="LinkButton4" runat="server" class="btn btn-danger" Text="Exit" /><%--trash-o--%>
                                    </div>
                                </div>


                                <div class="portlet-body">


                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general3">
                                                <div class="form-body">

                                                  
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label18" runat="server" Text="Project Name"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="drpSystem" runat="server" OnSelectedIndexChanged="drpSystem_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label20" runat="server" Text="RefType"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="drpRefType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                       
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />




                                    <div class="tabbable">
                                        <table class="table table-striped table-bordered table-hover" id="sample_11">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="Label21" runat="server" Text="#"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label22" runat="server" Text="Action"></asp:Label></th>
                                                     <th>
                                                        <asp:Label ID="Label9" runat="server" Text="Detail"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label23" runat="server" Text="My Project"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label24" runat="server" Text="Ref Type"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label25" runat="server" Text="RefSubType"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label26" runat="server" Text="Descrip"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label27" runat="server" Text="Remarks"></asp:Label></th>

                                                    <th>
                                                        <asp:Label ID="Label31" runat="server" Text="Action"></asp:Label></th>
                                                   
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:ListView ID="LISTADMIN" runat="server" OnItemCommand="LISTADMIN_ItemCommand">
                                                    <LayoutTemplate>
                                                        <tr id="ItemPlaceholder" runat="server">
                                                        </tr>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr onclick="return goForPostback()">
                                                            <td>
                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <div class="btn-group">
                                                                    <a data-toggle="dropdown" href="#" class="btn btn-sm blue dropdown-toggle" style="width: 60px;">
                                                                        <asp:Label ID="Label19" runat="server" Text="Action"></asp:Label>
                                                                        <i class="fa fa-angle-down"></i>
                                                                    </a>
                                                                    <ul class="dropdown-menu">
                                                                        <li>
                                                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="btnEdit" CausesValidation="False" CommandArgument='<%# Eval("RefAdminId") %>'>
                                                                                <i class="fa fa-pencil"></i>
                                                                                <asp:Label ID="Label73" runat="server" Text="Edit"></asp:Label>
                                                                            </asp:LinkButton>

                                                                        </li>
                                                                        <li>
                                                                            <asp:LinkButton ID="btnDelete123" runat="server" CommandArgument='<%# Eval("RefAdminId") %>' CommandName="btnDelete123">
                                                                                <i class="fa fa-pencil"></i>
                                                                                <asp:Label ID="Label74" runat="server" Text="Delete"></asp:Label>
                                                                            </asp:LinkButton>

                                                                        </li>

                                                                    </ul>
                                                                </div>
                                                            </td>
                                                            <td Style="background-color :blue;color: #fff ">
                                                                <asp:LinkButton ID="btnadd"  runat="server" CommandName="btnadd" CausesValidation="False" CommandArgument='<%# Eval("RefAdminId") %>'>

                                                                    <asp:Label ID="lble" runat="server"  Text="Detail" meta:resourcekey="lbleResource2"></asp:Label>
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRefId" runat="server" Visible="false" Text='<%# Eval("RefAdminId") %>' meta:resourcekey="lblRefIdResource1"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton1" Style="color: #5b9bd1;font-weight :bold   "  runat="server" CommandName="btnadd" CausesValidation="False" CommandArgument='<%# Eval("RefAdminId") %>' Text='<%# Eval("MySysName") %>'></asp:LinkButton>
                                                               <%-- <asp:Label ID="lblRefType" runat="server" Text='<%# Eval("MySysName") %>' meta:resourcekey="lblRefTypeResource1"></asp:Label>--%>
                                                            </td>

                                                            <td>
                                                                 <asp:LinkButton ID="LinkButton2" Style="color: #5b9bd1;font-weight :bold "  runat="server" CommandName="btnadd" CausesValidation="False" CommandArgument='<%# Eval("RefAdminId") %>' Text='<%# Eval("RefType") %>'></asp:LinkButton>
                                                                <%--<asp:Label ID="lblRefSubType" runat="server" Text='<%# Eval("RefType") %>' meta:resourcekey="lblRefSubTypeResource1"></asp:Label>--%>
                                                            </td>
                                                            <td>
                                                                 <asp:LinkButton ID="LinkButton3" Style="color: #5b9bd1;font-weight :bold "  runat="server" CommandName="btnadd" CausesValidation="False" CommandArgument='<%# Eval("RefAdminId") %>' Text='<%# Eval("RefSubType") %>'></asp:LinkButton>
                                                               <%-- <asp:Label ID="lblShortName" runat="server" Text='<%# Eval("RefSubType") %>' meta:resourcekey="lblShortNameResource1"></asp:Label>--%>
                                                            </td>
                                                            <td>
                                                                 <asp:LinkButton ID="LinkButton5" Style="color: #5b9bd1;font-weight :bold "  runat="server" CommandName="btnadd" CausesValidation="False" CommandArgument='<%# Eval("RefAdminId") %>' Text='<%# Eval("Descrip") %>'></asp:LinkButton>
                                                               <%-- <asp:Label ID="lblRefName" runat="server" Text='<%# Eval("Descrip") %>' meta:resourcekey="lblRefNameResource1"></asp:Label>--%>
                                                            </td>
                                                            <td>
                                                                 <asp:LinkButton ID="LinkButton6" Style="color: #5b9bd1;font-weight :bold "  runat="server" CommandName="btnadd" CausesValidation="False" CommandArgument='<%# Eval("RefAdminId") %>' Text='<%# Eval("Remarks") %>'></asp:LinkButton>
                                                               <%-- <asp:Label ID="lblRefNameO" runat="server" Text='<%# Eval("Remarks") %>' meta:resourcekey="lblRefNameResource1"></asp:Label>--%>
                                                            </td>

                                                            <td>
                                                                <asp:LinkButton ID="lnkAction" Style="color: #5b9bd1;font-weight :bold" runat="server" CommandArgument='<%# Eval("RefAdminId") + "," +Eval("Active") %>' CommandName="linkAction" Text='<%# Eval("Active") %>'></asp:LinkButton>
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
                    </asp:Panel>
                    <asp:Panel ID="panlRefAdmin" runat="server">
                        <div class="portlet light">
                            <div class="portlet box blue-hoki">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>
                                        <asp:Label ID="Label1" runat="server" Text="Admin RefTable Details"></asp:Label>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                       
                                        <asp:LinkButton ID="linkAdminRef" OnClick="linkAdminRef_Click" ToolTip="Submit Data" runat="server" class="btn btn-success" Text="Submit" />
                                        <asp:LinkButton ID="linkClen" runat="server" class="btn btn-danger" Text="Exit" /><%--trash-o--%>
                                    </div>
                                </div>

                                <div class="portlet-body">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general2">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label2" Text="Ref Type:" runat="server"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtRefTyep" placeholder="Ref Type" CssClass="form-control" MaxLength="20" runat="server"></asp:TextBox>
                                                            </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label5" Text="Ref Sub Type:" runat="server"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtSubtype" placeholder="Ref Sub Type" CssClass="form-control" MaxLength="20" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label6" Text="My Project Name" runat="server"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="drpProjectList" runat="server" CssClass="form-control"></asp:DropDownList>

                                                            </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label7" Text="Description" runat="server"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtDescriptionAdmin" placeholder="Description " MaxLength="30" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label11" Text="Switch" runat="server"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtswitchadmin" placeholder="Switch" MaxLength="3" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label12" Text="Remarks" runat="server" meta:resourcekey="lblSwitch2Resource1"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtRemarksadmin" placeholder="Remarks" MaxLength="500" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                         <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label13" Text="Start Serial" runat="server"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtStartSerial" placeholder="Start Serial" MaxLength="8" CssClass="form-control" runat="server"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="mobilenumericvalid" TargetControlID="txtStartSerial" FilterType="Custom, numbers" runat="server" />
                                                            </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label14" Text="End Serial" runat="server" meta:resourcekey="lblSwitch2Resource1"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtEndSerial" placeholder="End Serial" MaxLength="8" CssClass="form-control" runat="server"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtEndSerial" FilterType="Custom, numbers" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label15" Text="Infrastructure" runat="server"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:CheckBox ID="ckbInfras" runat="server"  />
                                                               
                                                            </div>
                                                           <%-- <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label17" Text="Sync Date" runat="server" meta:resourcekey="lblSwitch2Resource1"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtSyncDate"  CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>--%>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="panlListRef" runat="server">
                        <div class="portlet light">
                            <div class="portlet box blue-hoki">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>
                                        <asp:Label ID="Label8" runat="server" Text=" RefTable list"></asp:Label>
                                    </div>
                                    <%--<div class="actions btn-set">
                                <asp:Button ID="btnSubmit" runat="server" class="btn green-haze btn-circle" Text="Add" ValidationGroup="submit" />
                                <asp:Button ID="btnAdd" Visible="false" runat="server" class="btn green-haze btn-circle" Text="Add New" />
                                <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" Text="Cancel" />
                            </div>--%>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:LinkButton ID="AddNew" OnClick="AddNew_Click" runat="server" class="btn btn-success" Text="Add New" />
                                        <asp:LinkButton ID="btnSubmit" data-placement="top" data-toggle="tooltip" OnClientClick="return showWarningToast(this);" OnClick="btnSubmit_Click" ToolTip="Submit Data" runat="server" class="btn btn-success" Text="Submit" />
                                        <asp:LinkButton ID="btnExit" OnClientClick="return ClearAll();" data-placement="top" data-toggle="tooltip" ToolTip="Exit Data" OnClick="btnExit_Click" runat="server" class="btn btn-danger" Text="Exit" /><%--trash-o--%>
                                    </div>
                                </div>


                                <div class="portlet-body">


                                    <div class="tabbable">
                                        <div class="tab-content no-space">
                                            <div class="tab-pane active" id="tab_general">
                                                <div class="form-body">
                                                    <%--<asp:Label ID="title" runat="server" Text="Ref Table"></asp:Label>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblSystem" runat="server" SkinID="label1" Text="System"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="ddlSystem" runat="server" OnSelectedIndexChanged="ddlSystem_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lbldescrip" runat="server" SkinID="label1" Text="Activity"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="ddldescrip" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="ddlSystem" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />




                                    <div class="tabbable">
                                        <table class="table table-striped table-bordered table-hover" id="sample_1">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="lblSN" runat="server" Text="#"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblEdit" runat="server" Text="Action"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="Label10" runat="server" Text="Detail"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblHRefType" runat="server" Text="RefType"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblHRefSubType" runat="server" Text="RefSubType"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblhShortName" runat="server" Text="Short Name"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblHRefName" runat="server" Text="Ref Name1"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblHRefNameO" runat="server" Text="Ref Name2"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblHrefNamech" runat="server" Text="Ref Name3"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblHActive" runat="server" Text="Active"></asp:Label></th>
                                                    <th>
                                                        <asp:Label ID="lblHAction" runat="server" Text="Action"></asp:Label></th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:ListView ID="RefGridview" runat="server" OnItemCommand ="RefGridview_ItemCommand" >
                                                    <LayoutTemplate>
                                                        <tr id="ItemPlaceholder" runat="server">
                                                        </tr>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr onclick="return goForPostback()">
                                                            <td>
                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>' meta:resourcekey="lblSRNOResource2"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <div class="btn-group">
                                                                    <a data-toggle="dropdown" href="#" class="btn btn-sm blue dropdown-toggle" style="width: 60px;">
                                                                        <asp:Label ID="Label19" runat="server" Text="Action" meta:resourcekey="Label19Resource1"></asp:Label>
                                                                        <i class="fa fa-angle-down"></i>
                                                                    </a>
                                                                    <ul class="dropdown-menu">
                                                                        <li>
                                                                            <asp:LinkButton ID="lnkbtn" runat="server" CausesValidation="False" CommandArgument='<%# Eval("REFID") + "," +Eval("ACTIVE") %>' OnClick="lnkbtn_Click">
                                                                                <i class="fa fa-pencil"></i>
                                                                                <asp:Label ID="Label73" runat="server" Text="Edit" meta:resourcekey="Label73Resource1"></asp:Label>
                                                                            </asp:LinkButton>

                                                                        </li>
                                                                        <li>
                                                                            <asp:LinkButton ID="lnkbtndelete" runat="server" CommandArgument='<%# Eval("REFID") %>' CommandName="Delete" OnClick="lnkbtndelete_Click" OnClientClick="return confirm('Do you want to delete this Reftype?')">
                                                                                <i class="fa fa-pencil"></i>
                                                                                <asp:Label ID="Label74" runat="server" Text="Delete" meta:resourcekey="Label74Resource1"></asp:Label>
                                                                            </asp:LinkButton>

                                                                        </li>

                                                                    </ul>
                                                                </div>
                                                            </td>
                                                             <td Style="background-color :blue;color: #fff ">
                                                                <asp:LinkButton ID="linkadd"  runat="server" CommandName="linkadd" CausesValidation="False" CommandArgument='<%# Eval("REFID") %>'>

                                                                    <asp:Label ID="lble" runat="server"  Text="Detail" meta:resourcekey="lbleResource2"></asp:Label>
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRefId" runat="server" Visible="false" Text='<%# Eval("REFID") %>' meta:resourcekey="lblRefIdResource1"></asp:Label>
                                                                <asp:Label ID="lblRefType" runat="server" Text='<%# Eval("REFTYPE") %>' meta:resourcekey="lblRefTypeResource1"></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lblRefSubType" runat="server" Text='<%# Eval("REFSUBTYPE") %>' meta:resourcekey="lblRefSubTypeResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblShortName" runat="server" Text='<%# Eval("SHORTNAME") %>' meta:resourcekey="lblShortNameResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRefName" runat="server" Text='<%# Eval("REFNAME1") %>' meta:resourcekey="lblRefNameResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRefNameO" runat="server" Text='<%# Eval("REFNAME2") %>' meta:resourcekey="lblRefNameResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblrefNamech" runat="server" Text='<%# Eval("REFNAME3") %>' meta:resourcekey="lblRefNameResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCurrentStatus" runat="server" Text='<%# Eval("ACTIVE") %>' meta:resourcekey="lblCurrentStatusResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkAction" Style="color: #5b9bd1" runat="server" CommandArgument='<%# Eval("REFID") + "," +Eval("ACTIVE") %>' CommandName="linkAction" Text='<%# Eval("ACTIVE") %>' OnClick="lnkAction_Click" meta:resourcekey="lnkActionResource1"></asp:LinkButton>
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
                    </asp:Panel>
                    <asp:Panel ID="panlRefEntery" runat="server">
                        <div class="portlet light">
                            <div class="portlet box blue-hoki">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>
                                        <asp:Label ID="Label28" runat="server" Text=" RefTable Details"></asp:Label>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <asp:LinkButton ID="btnSubmitAd" data-placement="top" data-toggle="tooltip" OnClientClick="return showWarningToast1();" OnClick="btnSubmitAd_Click" ToolTip="Submit Data" runat="server" class="btn btn-success" Text="Submit" />
                                        <asp:LinkButton ID="Button1" OnClientClick="return ClearAll();" data-placement="top" data-toggle="tooltip" ToolTip="Exit Data" runat="server" class="btn btn-danger" Text="Exit" /><%--trash-o--%>
                                    </div>
                                </div>

                                <div class="portlet-body">
                                    <asp:Panel ID="pnlCreateForm" runat="server">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general1">
                                                    <div class="form-body">
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label3" Text="Ref Type" runat="server" meta:resourcekey="lblRefTypeResource2"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtRefType" Enabled="false" MaxLength="20" placeholder="Main Classification" data-toggle="tooltip" ToolTip="Main Classification" CssClass="form-control" runat="server" meta:resourcekey="txtRefTypeResource1"></asp:TextBox>
                                                            </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="Label4" Text="Ref Sub Type" runat="server" meta:resourcekey="lblRefSubTypeResource2"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtRefSubType" Enabled="false" MaxLength="20" placeholder="Sub Classification" data-toggle="tooltip" ToolTip="Sub Classification" CssClass="form-control" runat="server" meta:resourcekey="txtRefSubTypeResource1"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblShortname" Text="Project Name" runat="server" meta:resourcekey="lblShortnameResource2"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtShortname" Enabled="false" MaxLength="50" placeholder="Shortname" data-toggle="tooltip" ToolTip="ShortName" CssClass="form-control" runat="server" meta:resourcekey="txtShortnameResource1"></asp:TextBox>
                                                            </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblRefName" Text="Description1" runat="server" meta:resourcekey="lblRefNameResource2"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtRefName" placeholder="Description in English" MaxLength="100" data-toggle="tooltip" ToolTip="Description in English" CssClass="form-control" runat="server" meta:resourcekey="txtRefNameResource1"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblRefNameO" Text="Description2" runat="server" meta:resourcekey="lblRefNameOResource1"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <Lang:LangTextBox ID="txtRefNameO" runat="server" AutoCompleteType="Disabled" MaxLength="100" data-toggle="tooltip" ToolTip="Description in Second Language" CssClass="arabic form-control" placeholder="Description in Second Language" TextLanguage="Arabic"></Lang:LangTextBox>
                                                            </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblRefNameCH" Text="Description3" runat="server" meta:resourcekey="lblRefNameCHResource1"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtRefNameCH" placeholder="Description in Third Language" MaxLength="100" data-toggle="tooltip" ToolTip="Description in Third Language" CssClass="form-control" runat="server" meta:resourcekey="txtRefNameCHResource1"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblSwitch1" Text="Switch1" runat="server" meta:resourcekey="lblSwitch1Resource1"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtSwitch1" placeholder="Switch1" MaxLength="10" CssClass="form-control" runat="server" data-toggle="tooltip" ToolTip="Switch1" meta:resourcekey="txtSwitch1Resource1"></asp:TextBox>
                                                            </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblSwitch2" Text="Switch2" runat="server" meta:resourcekey="lblSwitch2Resource1"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtSwitch2" placeholder="Switch2" MaxLength="30" CssClass="form-control" runat="server" data-toggle="tooltip" ToolTip="Switch2" meta:resourcekey="txtSwitch2Resource1"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblSortNo" Text="Switch3" runat="server" meta:resourcekey="lblSortNoResource1"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtSortNo" placeholder="Switch3" MaxLength="3" CssClass="form-control" runat="server" data-toggle="tooltip" ToolTip="Switch3" meta:resourcekey="txtSortNoResource1"></asp:TextBox>
                                                            </div>
                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblActive" Text="Status" runat="server" meta:resourcekey="lblActiveResource1"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList runat="server" ID="drpactive" data-toggle="tooltip" ToolTip="Select Status" CssClass="form-control">
                                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="In Active" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">

                                                            <label class="col-md-2 control-label">
                                                                <asp:Label ID="lblRemarks" Text="Remarks" runat="server" meta:resourcekey="lblRemarksResource1"></asp:Label>
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-10">
                                                                <asp:TextBox ID="txtRemarks" placeholder="Remarks" MaxLength="500" CssClass="form-control" data-toggle="tooltip" ToolTip="Remarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:HiddenField ID="hdnRef" runat="server" />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                </div>
            </div>
        </div>

    </div>

</asp:Content>
