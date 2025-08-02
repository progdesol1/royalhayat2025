<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="AttachmentMst.aspx.cs" Inherits="Web.CRM.AttachmentMst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%--  <ul class="page-breadcrumb breadcrumb">
            <li>
                <a href="index.aspx">DMS</a>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <a href="#">AttachmentMst</a>
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
            <ul class="page-breadcrumb breadcrumb">
                <li>
                    <a href="index.aspx">CRM</a>
                    <i class="fa fa-circle"></i>
                </li>
                <li>
                    <a href="#">Attachment Master</a>
                </li>
            </ul>
            <asp:Panel ID="pnlform" runat="server">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="icon-basket font-green-sharp"></i>
                                <span class="caption-subject font-green-sharp bold uppercase">
                                    <asp:Label ID="lblTitle" runat="server" Text="Attachment Master"></asp:Label>
                                </span>
                            </div>
                            <div class="actions btn-set">
                                <asp:Button ID="btnSubmit" runat="server" class="btn green-haze btn-circle" OnClick="btnSave_Click" Text="Add" ValidationGroup="submit" />
                                <%-- <asp:Button ID="btnAdd"  runat="server" class="btn green-haze btn-circle" Text="Add New" OnClick="btnAdd_Click" />--%>
                                <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="tabbable">
                                <div class="tab-content no-space">
                                    <div class="tab-pane active" id="tab_general1">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Browse Document</label>
                                                <div class="col-md-4">
                                                    <asp:FileUpload ID="FUDoc" runat="server" CssClass="form-control" Style="padding-top: 0px;" />

                                                    <%--<asp:Image ID="Image1" Visible ="false" runat="server" />--%>
                                                </div>
                                               
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Attachments Detail</label><div class="col-md-10">
                                                    <asp:Label ID="lblPath" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txtAttachmentsDetail" runat="server" TextMode="MultiLine" class="form-control" MaxLength="1000"></asp:TextBox><asp:RequiredFieldValidator CssClass="Validation" ID="RequiredFieldValidatorAttachmentsDetail" runat="server" ErrorMessage="AttachmentsDetail Required" ControlToValidate="txtAttachmentsDetail" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Source of Infomation</label><div class="col-md-4">
                                                    <asp:DropDownList ID="drpAttachmentType" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <div class="form-horizontal form-row-seperated">
                <div class="portlet light">                    
                    <div class="portlet-body">
                        <div class="tabbable">
                            <div class="tab-content no-space">
                                <div class="tab-pane active" id="tab111_general1">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <asp:Label ID="lblText" runat="server" />
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
                            <h4 class="modal-title">AttachmentMst</h4>
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


            <asp:Panel ID="pnllist" runat="server">
                <div class="form-horizontal form-row-seperated">
                    <div class="portlet light">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="icon-basket font-green-sharp"></i>
                                <span class="caption-subject font-green-sharp bold uppercase">
                                    <asp:Label ID="lblTitle1" runat="server" Text="Attachment Master List"></asp:Label>
                                </span>
                            </div>
                            <div class="actions btn-set">

                                <asp:Button ID="Button1" runat="server" Visible="false" class="btn green-haze btn-circle" Text="Share" OnClick="Button1_Click1" />
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="tabbable">
                                <table class="table table-striped table-bordered table-hover" id="sample">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:Label ID="lblselect" runat="server" Text="Select" Visible="false"></asp:Label></th>
                                            <th style="width: 60px;">Action</th>
                                            <th>Serial</th>
                                            <th>Type</th>
                                            <th>Detail</th>
                                            <th>Path and Thumnail</th>

                                            <%--<th>Edit</th>
                                            <th>Delete</th>--%>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="Listview1" runat="server" OnItemCommand="ListAttachmentMst_ItemCommand">
                                            <LayoutTemplate>
                                                <tr id="ItemPlaceholder" runat="server">
                                                </tr>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="cbkselect" runat="server" Visible="false" />
                                                    </td>
                                                    <td>
                                                        <div class="btn-group">
                                                            <a data-toggle="dropdown" href="#" class="btn btn-sm blue dropdown-toggle" style="width: 60px;">Action <i class="fa fa-angle-down"></i>
                                                            </a>
                                                            <ul class="dropdown-menu">
                                                                <li>
                                                                    <asp:LinkButton ID="LinkButton1" CommandName="btnEdit" CommandArgument='<%# Eval("AttachID")%>' runat="server">  <i class="fa fa-pencil"></i>Edit</asp:LinkButton>

                                                                </li>
                                                                <li>
                                                                    <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("AttachID")%>' runat="server"> <i class="fa fa-pencil"></i>Delete</asp:LinkButton>

                                                                </li>
                                                                <li>
                                                                    <asp:Label ID="lblName" runat="server" Visible="false" Text='<%# "../Gallery/"+ Eval("AttachmentPath")  %>'></asp:Label>
                                                                    <asp:LinkButton ID="btnDownload" CommandName="btnDownload" CommandArgument='<%# Eval("AttachID") + "," +Eval("AttachmentById") + "," +Eval("Serialno")+ "," +Eval("ReferenceNo")%>' runat="server"> <i class="fa fa-pencil"></i>Download</asp:LinkButton>

                                                                </li>
                                                                <%--<li>
                                                                    <asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("ID")%>' CommandName="btnPrint" CommandArgument='<%# Eval("ID")%>' runat="server"><i class="fa fa-pencil"></i>Print</asp:LinkButton>
                                                                </li>--%>
                                                            </ul>
                                                        </div>
                                                    </td>
                                                    <%-- <td>
                                                        <asp:Label ID="lblAttachID" runat="server" Text='<%# Eval("AttachID")%>'></asp:Label></td>--%>
                                                    <td>
                                                        <asp:Label ID="lblSerialno" runat="server" Text='<%# Eval("Serialno")%>'></asp:Label>
                                                        <asp:Label ID="lblAttachID" Visible="false" runat="server" Text='<%# Eval("AttachID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAttachmentType" runat="server" Text='<%#  getAttachmentType(Convert.ToInt32(Eval("AttachmentType")))%>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblAttachmentsDetail" runat="server" Text='<%# Eval("AttachmentsDetail")%>'></asp:Label></td>
                                                    <td>
                                                      
                                                         <asp:LinkButton ID="LinkButton2" CommandName="btnDownload" CommandArgument='<%# Eval("AttachID") + "," +Eval("AttachmentById") + "," +Eval("Serialno")+ "," +Eval("ReferenceNo")%>' runat="server">
                                                             <asp:Label ID="lblAttachmentPath1" runat="server" Text='<%# Eval("AttachmentPath")%>'></asp:Label>
                                                         </asp:LinkButton></td>
                                                    <%-- <td>
                                                        <div class="actions btn-set">
                                                            <asp:LinkButton ID="btnEdit" class="btn green-haze btn-circle" CommandName="btnEdit" CommandArgument='<%# Eval("AttachID")%>' PostBackUrl='<%# "AttachmentMst.aspx?ID="+ Eval("AttachID")%>' runat="server">Edit</asp:LinkButton></div>
                                                    </td>
                                                    <td>
                                                        <div class="actions btn-set">
                                                            <asp:LinkButton ID="btnDelete" class="btn red-haze btn-circle" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("AttachID")%>' runat="server">Delete</asp:LinkButton></div>
                                                    </td>--%>
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
        </div>
    </div>
    <script>
        $(window).load(function () {
            $("#ToolTables_sample_1_0").remove();
        });
    </script>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
