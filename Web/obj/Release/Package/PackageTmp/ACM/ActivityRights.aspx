<%@ Page Title="" Language="C#" MasterPageFile="~/ACM/ACMMaster.Master" AutoEventWireup="true" CodeBehind="ActivityRights.aspx.cs" Inherits="Web.ACM.ActivityRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnOneCheckboxSelected(chkB) {
            var IsChecked = chkB.checked;
            var Parent = document.getElementById('ContentPlaceHolder1_ListViewActivity');
            var cbxAll;
            var items = Parent.getElementsByTagName('input');
            var bAllChecked = true;
            for (i = 0; i < items.length; i++) {
                if (items[i].id.indexOf('ContentPlaceHolder1_cbxSelectAll') != -1) {
                    cbxAll = items[i];
                    continue;
                }
                if (items[i].type == "checkbox" && items[i].checked == false) {
                    bAllChecked = false;
                    break;
                }
            }
            cbxAll.checked = bAllChecked;
        }

        function SelectAllCheckboxes(spanChk) {
            var IsChecked = spanChk.checked;
            var cbxAll = spanChk;
            var Parent = document.getElementById('ContentPlaceHolder1_ListViewActivity');
            var items = Parent.getElementsByTagName('input');
            for (i = 0; i < items.length; i++) {
                if (items[i].id != cbxAll.id && items[i].type == "checkbox") {
                    items[i].checked = IsChecked;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div  id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <%--<ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">Activity Rights</a>
                    </li>
                </ul>--%>
                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
                <div class="col-md-12 col-sm-12">
                    <!-- BEGIN EXAMPLE TABLE PORTLET style="padding-top: 85px;"-->
                    <div class="portlet box blue">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-user"></i>Activity List
                            </div>
                            <div class="actions">
                                <a href="Campaign_Mst.aspx?View=Add" class="btn btn-success uppercase">
                                    <i class="fa fa-plus"></i>&nbsp;&nbsp;Add </a>
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="tabbable">
                                <div class="tab-content no-space">
                                    <div class="tab-pane active" id="tab_general21">
                                        <div class="form-body">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="Label2" class="col-md-4 control-label">Tenant ID</asp:Label><asp:TextBox runat="server" ID="TextBox2" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                            <asp:DropDownList ID="DrpTENANT_ID" runat="server" OnSelectedIndexChanged="DrpTENANT_ID_SelectedIndexChanged" AutoPostBack="true" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Tenant Required." ControlToValidate="DrpTENANT_ID" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <asp:Label runat="server" ID="Label3" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox3" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="Label4" class="col-md-4 control-label">Location ID</asp:Label><asp:TextBox runat="server" ID="TextBox4" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                            <asp:DropDownList ID="drplocation" runat="server" CssClass="table-group-action-input form-control input-medium" AutoPostBack="true" OnSelectedIndexChanged="drplocation_SelectedIndexChanged"></asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Tenant Required." ControlToValidate="DrpTENANT_ID" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <asp:Label runat="server" ID="Label12" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox11" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="lblMODULE_ID1s" class="col-md-4 control-label">Module Name</asp:Label><asp:TextBox runat="server" ID="txtMODULE_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                            <asp:DropDownList ID="drpMODULE_ID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                        </div>
                                                        <asp:Label runat="server" ID="lblMODULE_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtMODULE_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group" style="color: ">
                                                        <asp:Label runat="server" ID="Label10" class="col-md-4 control-label">User</asp:Label><asp:TextBox runat="server" ID="TextBox9" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                            <asp:DropDownList ID="DrpUser" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                        </div>
                                                        <asp:Label runat="server" ID="Label11" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox10" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <asp:Button ID="btnSaveACTIVITYRights" runat="server" class="btn blue" OnClick="btnSaveACTIVITYRights_Click" Text="Search" />


                            </div>
                        </div>

                        <div class="portlet-body">
                            <div class="table-scrollable">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <table class="table table-striped table-bordered table-hover" id="sample_2">
                                            <thead>
                                                <tr>

                                                    <th style="width: 0%">Activity Name
                                                    </th>
                                                    <th class="table-checkbox">
                                                        <input type="checkbox" class="group-checkable" data-set="#sample_2 .checkboxes" />
                                                    </th>
                                                    <th class="table-checkbox">UPDATE<br />
                                                        <asp:CheckBox ID="UpdateCheckBox" runat="server" />
                                                    </th>
                                                    <th class="table-checkbox">DELETE<br />
                                                        <asp:CheckBox ID="DeleteCheckBox" runat="server" />
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:ListView ID="ListViewActivity" runat="server">
                                                    <LayoutTemplate>
                                                        <tr id="ItemPlaceholder" runat="server">
                                                        </tr>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="odd gradeX">
                                                            <td>
                                                                <asp:Label ID="lblTenet" runat="server" Text='<%# Eval("ACTIVITYA")%>'></asp:Label>
                                                                <asp:HiddenField ID="hiddenActivityID" runat="server" Value='<%# Eval("ACTIVITYCODE")%>' />
                                                            </td>

                                                            <td>
                                                                <asp:CheckBox ID="cbxSelect" runat="server" CssClass="checkboxes"  />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="UpdateCheckBox1" runat="server" />
                                                            </td>

                                                            <td>
                                                                <asp:CheckBox ID="DeleteCheckBox1" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="form-actions">
                                <asp:Button ID="btnSaveRights" runat="server" class="btn blue" OnClick="btnSaveRights_Click" Text="Save" />
                                <asp:Button ID="btnCancelACTIVITYRights" runat="server" class="btn blue" OnClick="btnCancelACTIVITYRights_Click" Text="Cancel" />
                            </div>
                        </div>
                    </div>
                    <!-- END EXAMPLE TABLE PORTLET-->
                </div>



                <!-- END PAGE CONTENT-->
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
