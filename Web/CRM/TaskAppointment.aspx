<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="TaskAppointment.aspx.cs" Inherits="Web.CRM.TaskAppointment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tabletools-dropdown-on-portlet {
            left: -90px;
            margin-top: -35px;
        }

        .form-group {
            margin-bottom: 5px;
        }

        hr {
            margin-bottom: 5px;
        }
    </style>
    <script id="template-upload" type="text/x-tmpl">
{% for (var i=0, file; file=o.files[i]; i++) { %}
    <tr class="template-upload fade">
        <td class="name" width="30%"><span>{%=file.name%}</span></td>
        <td class="size" width="40%"><span>{%=o.formatFileSize(file.size)%}</span></td>
        {% if (file.error) { %}
            <td class="error" width="20%" colspan="2"><span class="label label-danger">Error</span> {%=file.error%}</td>
        {% } else if (o.files.valid && !i) { %}
            <td>
                <p class="size">{%=o.formatFileSize(file.size)%}</p>
                <div class="progress progress-striped active" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0">
                   <div class="progress-bar progress-bar-success" style="width:0%;"></div>
                   </div>
            </td>
        {% } else { %}
            <td colspan="2"></td>
        {% } %}
        <td class="cancel" width="10%" align="right">{% if (!i) { %}
            <button class="btn btn-sm red cancel">
                       <i class="fa fa-ban"></i>
                       <span>Cancel</span>
                   </button>
        {% } %}</td>
    </tr>
{% } %}
    </script>
    <!-- The template to display files available for download -->
    <script id="template-download" type="text/x-tmpl">
{% for (var i=0, file; file=o.files[i]; i++) { %}
    <tr class="template-download fade">
        {% if (file.error) { %}
            <td class="name" width="30%"><span>{%=file.name%}</span></td>
            <td class="size" width="40%"><span>{%=o.formatFileSize(file.size)%}</span></td>
            <td class="error" width="30%" colspan="2"><span class="label label-danger">Error</span> {%=file.error%}</td>
        {% } else { %}
            <td class="name" width="30%">
                <a href="{%=file.url%}" title="{%=file.name%}" data-gallery="{%=file.thumbnail_url&&'gallery'%}" download="{%=file.name%}">{%=file.name%}</a>
            </td>
            <td class="size" width="40%"><span>{%=o.formatFileSize(file.size)%}</span></td>
            <td colspan="2"></td>
        {% } %}
        <td class="delete" width="10%" align="right">
            <button class="btn default btn-sm" data-type="{%=file.delete_type%}" data-url="{%=file.delete_url%}"{% if (file.delete_with_credentials) { %} data-xhr-fields='{"withCredentials":true}'{% } %}>
                <i class="fa fa-times"></i>
            </button>
        </td>
    </tr>
{% } %}
    </script>

    <script src="../assets/global/plugins/fancybox/source/jquery.fancybox.pack.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-wysihtml5/wysihtml5-0.3.0.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-wysihtml5/bootstrap-wysihtml5.js" type="text/javascript"></script>
    <!-- BEGIN:File Upload Plugin JS files-->
    <!-- The jQuery UI widget factory, can be omitted if jQuery UI is already included -->
    <script src="../assets/global/plugins/jquery-file-upload/js/vendor/jquery.ui.widget.js"></script>
    <!-- The Templates plugin is included to render the upload/download listings -->
    <script src="../assets/global/plugins/jquery-file-upload/js/vendor/tmpl.min.js"></script>
    <!-- The Load Image plugin is included for the preview images and image resizing functionality -->
    <script src="../assets/global/plugins/jquery-file-upload/js/vendor/load-image.min.js"></script>
    <!-- The Canvas to Blob plugin is included for image resizing functionality -->
    <script src="../assets/global/plugins/jquery-file-upload/js/vendor/canvas-to-blob.min.js"></script>
    <!-- blueimp Gallery script -->
    <script src="../assets/global/plugins/jquery-file-upload/blueimp-gallery/jquery.blueimp-gallery.min.js"></script>
    <!-- The Iframe Transport is required for browsers without support for XHR file uploads -->
    <script src="../assets/global/plugins/jquery-file-upload/js/jquery.iframe-transport.js"></script>
    <!-- The basic File Upload plugin -->
    <script src="../assets/global/plugins/jquery-file-upload/js/jquery.fileupload.js"></script>
    <!-- The File Upload processing plugin -->
    <script src="../assets/global/plugins/jquery-file-upload/js/jquery.fileupload-process.js"></script>
    <!-- The File Upload image preview & resize plugin -->
    <script src="../assets/global/plugins/jquery-file-upload/js/jquery.fileupload-image.js"></script>
    <!-- The File Upload audio preview plugin -->
    <script src="../assets/global/plugins/jquery-file-upload/js/jquery.fileupload-audio.js"></script>
    <!-- The File Upload video preview plugin -->
    <script src="../assets/global/plugins/jquery-file-upload/js/jquery.fileupload-video.js"></script>
    <!-- The File Upload validation plugin -->
    <script src="../assets/global/plugins/jquery-file-upload/js/jquery.fileupload-validate.js"></script>
    <!-- The File Upload user interface plugin -->
    <script src="../assets/global/plugins/jquery-file-upload/js/jquery.fileupload-ui.js"></script>

    <script src="../assets/admin/pages/scripts/inbox.js" type="text/javascript"></script>
    <script>
        jQuery(document).ready(function () {
            // initiate layout and plugins
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Demo.init(); // init demo features
            Inbox.init();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div id="b" runat="server">
            <div class="col-md-12">
                <div class="tabbable-custom tabbable-noborder">
                    <ul class="page-breadcrumb breadcrumb">

                        <li>
                            <asp:Button ID="btntaskpanal" runat="server" OnClick="btntaskpanal_Click" data-title="Inbox" Style="background: #F2DEDE none repeat scroll 0 0; width: 150px; border-left: medium none; color: #b64949; display: block; font-size: 15px; margin-bottom: 1px; padding: 8px 14px; text-align: left;" class="btn red" Text="Task" />

                        </li>
                        <li>
                            <asp:Button ID="btndetail" runat="server" OnClick="btndetail_Click" data-title="Inbox" Style="background: #f7f5d4 none repeat scroll 0 0; width: 150px; border-left: medium none; color: #d7cf28; display: block; font-size: 15px; margin-bottom: 1px; padding: 8px 14px; text-align: left;" class="btn" Text="Details" />

                        </li>
                        <li>
                            <asp:Button ID="btnassingtask" runat="server" OnClick="btnassingtask_Click" data-title="Inbox" Style="background: #edf7d4 none repeat scroll 0 0; width: 150px; border-left: medium none; color: #43c224; display: block; font-size: 15px; margin-bottom: 1px; padding: 8px 14px; text-align: left;" class="btn" Visible="false" Text="Assign Sub Task" />

                        </li>
                        <li>
                            <asp:Button ID="btnsendstatus" runat="server" OnClick="btnsendstatus_Click" data-title="Inbox" Style="background: #dbf7d4 none repeat scroll 0 0; width: 150px; border-left: medium none; color: #24c28b; display: block; font-size: 15px; margin-bottom: 1px; padding: 8px 14px; text-align: left;" class="btn" Text="Status Reports" />

                        </li>
                        <li>
                            <asp:Button ID="btnmarkcomplete" runat="server" OnClick="btnmarkcomplete_Click" data-title="Inbox" Style="background: #d4f7eb none repeat scroll 0 0; width: 150px; border-left: medium none; color: #10563e; display: block; font-size: 15px; margin-bottom: 1px; padding: 8px 14px; text-align: left;" class="btn" Text="Mark Complete" />

                        </li>
                        <li>
                            <asp:Button ID="btnRecurrence" runat="server" OnClick="btnRecurrence_Click" data-title="Inbox" Style="background: #d4e0f7 none repeat scroll 0 0; width: 150px; border-left: medium none; color: #2865d7; display: block; font-size: 15px; margin-bottom: 1px; padding: 8px 14px; text-align: left;" class="btn" Text="Recurrence" />

                        </li>
                        <li>
                            <asp:Button ID="btncancleassignment" runat="server" OnClick="btncancleassignment_Click" data-title="Inbox" Style="background: #f4f9fd none repeat scroll 0 0; width: 150px; border-left: medium none; color: #4d82a3; display: block; font-size: 15px; margin-bottom: 1px; padding: 8px 14px; text-align: left;" class="btn" Text="Cancle Task" />

                        </li>
                    </ul>
                    <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                        <div class="alert alert-success alert-dismissable">
                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </asp:Panel>
                    <%-- <asp:UpdatePanel ID="upnlnewtask" runat="server">
                    <ContentTemplate>--%>

                    <div class="row">

                        <div class="col-md-12" id="Taks1">
                            <div class="form-horizontal form-row-seperated">
                                <asp:Panel ID="pnltask" runat="server" Visible="true">
                                    <div class="portlet box blue">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>Add 
                                        <asp:Label runat="server" ID="lblHeader"></asp:Label>
                                                <asp:TextBox Style="color: #333333" ID="txtHeader" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <%--<a href="#portlet-config" data-toggle="modal" class="config"></a>--%>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                            <div class="actions btn-set">
                                                <div id="navigation" runat="server" visible="false" class="btn-group btn-group-circle btn-group-solid">
                                                    <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                                    <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                                    <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                                    <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />
                                                </div>


                                                <%--<asp:Button ID="Button4" class="btn green-haze btn-circle" runat="server" Text="Categorize"  />--%>
                                                <%-- <asp:Button ID="Button5" class="btn green-haze btn-circle" runat="server" Text="Follow Up"/>
                                         <asp:Button ID="Button6" class="btn green-haze btn-circle" runat="server" Text="Private"/>--%>

                                                <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="AddNew" OnClick="btnAdd_Click" />
                                                <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                                <asp:Button ID="btnEditLable" runat="server" class="btn green-haze btn-circle" OnClick="btnEditLable_Click" Text="Update Label" />
                                                &nbsp;
                                        <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="../assets/global/img/flags/us.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="../assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="../assets/global/img/flags/fr.png" /></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="portlet-body" style="padding: 0px;">
                                            <div class="portlet-body form">
                                                <div class="tabbable">
                                                    <div class="tab-content no-space">
                                                        <div class="tab-pane active" id="tab_general1">
                                                            <div class="form-body" style="padding: 5px;">
                                                                <%--<div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTanentId1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTanentId1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpTanentId" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTanentId2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTanentId2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                       
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblLocationID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLocationID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpLocationID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblLocationID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtLocationID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                                <%--<div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTaskID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTaskID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpTaskID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTaskID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTaskID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                       
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCerticateNo1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCerticateNo1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpCerticateNo" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCerticateNo2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCerticateNo2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                                <%--<div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblForEmp_ID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtForEmp_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpForEmp_ID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblForEmp_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtForEmp_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                       
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblPerformingEmp_ID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPerformingEmp_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpPerformingEmp_ID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblPerformingEmp_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPerformingEmp_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblactivity" Text="Activity" class="col-md-4 control-label"></asp:Label><div class="col-md-8">
                                                                                <asp:DropDownList ID="drpactivity" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblproject" Text="Project" class="col-md-2 control-label"></asp:Label><div class="col-md-10">
                                                                                <asp:DropDownList ID="drpproject" runat="server" CssClass="form-control select2me" Style="width: 100%"></asp:DropDownList>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lbltodate" Text="To Field" class="col-md-4 control-label"></asp:Label><div class="col-md-8">
                                                                                <asp:DropDownList ID="drptofield" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblowner" Text="Owner" class="col-md-2 control-label"></asp:Label><div class="col-md-10">
                                                                                <asp:TextBox ID="txtowner" runat="server" CssClass="form-control" Enabled="false" Style="width: 100%"> </asp:TextBox>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblSubject1s" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSubject1s" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-10">
                                                                                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" Style="width: 100%;"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorname" runat="server" ControlToValidate="txtSubject" ErrorMessage="Subject Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblSubject2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtSubject2h" class="col-md-2 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <hr style="margin-top: 0px; border-color: #3598dc" />
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblStartingDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStartingDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-4">
                                                                                <asp:TextBox Placeholder="MM/DD/YYYY" ID="txtStartingDate" runat="server" CssClass="form-control" Style="width: 100%"> </asp:TextBox>
                                                                                <cc1:CalendarExtender ID="TextBoxStartingDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtStartingDate" Format="MM/dd/yyyy hh:mm:ss tt"></cc1:CalendarExtender>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtStartingDate" ErrorMessage="StartingDate Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender" TargetControlID="txtStartingDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox placeholder="Num Of Day" ID="txtnoofday" OnTextChanged="txtnoofday_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" Style="width: 118px;"> </asp:TextBox>
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblStartingDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtStartingDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblTaskStatus1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTaskStatus1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="drpTaskStatus" runat="server" CssClass="form-control select2me" Style="width: 100%;">
                                                                                    <asp:ListItem Value="Not Started">Not Started</asp:ListItem>
                                                                                    <asp:ListItem Value="In Progress">In Progress</asp:ListItem>
                                                                                    <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                                                                    <asp:ListItem Value="Waiting on Somone else">Waiting on Somone else</asp:ListItem>
                                                                                    <asp:ListItem Value="Deferred">Deferred</asp:ListItem>
                                                                                </asp:DropDownList>

                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblTaskStatus2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTaskStatus2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lbltasktype" Text="Task Type" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox8" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="drptasktype" runat="server" CssClass="form-control select2me" Style="width: 100%;">
                                                                                    <asp:ListItem Value="Private">Private</asp:ListItem>
                                                                                    <asp:ListItem Value="High Importance">High Importance</asp:ListItem>
                                                                                    <asp:ListItem Value="Low Importance">Low Importance</asp:ListItem>

                                                                                </asp:DropDownList>

                                                                            </div>
                                                                            <asp:Label runat="server" ID="Label10" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox9" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <%-- <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblTaskType1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTaskType1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtTaskType" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTaskType" runat="server" ControlToValidate="txtTaskType" ErrorMessage="Task Type Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblTaskType2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtTaskType2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>--%>

                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblDueDate1s" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDueDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                                <asp:TextBox Placeholder="MM/DD/YYYY" OnTextChanged="txtDueDate_TextChanged" AutoPostBack="true" ID="txtDueDate" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxDueDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDueDate" Format="MM/dd/yyyy hh:mm:ss tt"></cc1:CalendarExtender>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDueDate" ErrorMessage="DueDate Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" TargetControlID="txtDueDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblDueDate2h" class="col-md-2 control-label"></asp:Label><asp:TextBox runat="server" ID="txtDueDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblPriority1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPriority1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="drpPriority" runat="server" CssClass="form-control select2me" Style="width: 100%;">
                                                                                    <asp:ListItem Value="Low">Low</asp:ListItem>
                                                                                    <asp:ListItem Value="Normal">Normal</asp:ListItem>
                                                                                    <asp:ListItem Value="High">High</asp:ListItem>
                                                                                </asp:DropDownList>

                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblPriority2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtPriority2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblCompletedPerctange1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCompletedPerctange1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="drpCompletedPerctange" runat="server" CssClass="form-control select2me" Style="width: 100%;">
                                                                                    <asp:ListItem Value="0">0%</asp:ListItem>
                                                                                    <asp:ListItem Value="25">25%</asp:ListItem>
                                                                                    <asp:ListItem Value="50">50%</asp:ListItem>
                                                                                    <asp:ListItem Value="75">75%</asp:ListItem>
                                                                                    <asp:ListItem Value="100">100%</asp:ListItem>
                                                                                </asp:DropDownList>

                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblCompletedPerctange2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCompletedPerctange2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <hr style="margin-top: 0px; border-color: #3598dc" />

                                                                <%--<div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblActualCompletionDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActualCompletionDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtActualCompletionDate" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxActualCompletionDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtActualCompletionDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorActualCompletionDate" runat="server" ControlToValidate="txtActualCompletionDate" ErrorMessage="Actual Completion Date Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" TargetControlID="txtActualCompletionDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblActualCompletionDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActualCompletionDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        
                                                           
                                                        </div>--%>
                                                                <asp:UpdatePanel ID="cheack" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="row">
                                                                            <div class="col-md-6">
                                                                                <div class="form-group" style="color: ">
                                                                                    <asp:Label runat="server" ID="lblReminderDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReminderDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                    <div class="col-md-1">
                                                                                        <asp:CheckBox ID="cbreminder" runat="server" OnCheckedChanged="cbreminder_CheckedChanged" AutoPostBack="true" />
                                                                                    </div>
                                                                                    <div class="col-md-6">
                                                                                        <asp:TextBox Style="width: 100%;" Placeholder="DD/MM/YYYY" ID="txtReminderDate" Enabled="false" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxReminderDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtReminderDate" Format="MM/dd/yyyy hh:mm:ss tt"></cc1:CalendarExtender>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtReminderDate" ErrorMessage="ReminderDate Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" TargetControlID="txtReminderDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                                                                    </div>
                                                                                    <asp:Label runat="server" ID="lblReminderDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtReminderDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                        <%--<div class="row">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group" style="color: ">
                                                                                <asp:Label runat="server" ID="lblActive1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8" style="margin-top: 8px;">
                                                                                    <asp:CheckBox ID="cbActive" runat="server" Checked="true" />
                                                                                </div>
                                                                                <asp:Label runat="server" ID="lblActive2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtActive2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>--%>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                                <%-- <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCategories1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCategories1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCategories" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCategories" runat="server" ControlToValidate="txtCategories" ErrorMessage="Categories Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCategories2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCategories2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                       
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblFollowUp1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtFollowUp1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtFollowUp" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorFollowUp" runat="server" ControlToValidate="txtFollowUp" ErrorMessage="Followup Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblFollowUp2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtFollowUp2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCustomFollowUpStartDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCustomFollowUpStartDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtCustomFollowUpStartDate" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxCustomFollowUpStartDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtCustomFollowUpStartDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCustomFollowUpStartDate" runat="server" ControlToValidate="txtCustomFollowUpStartDate" ErrorMessage="Custom Follow Up Start Date Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" TargetControlID="txtCustomFollowUpStartDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCustomFollowUpStartDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCustomFollowUpStartDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                       
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCustomFollowUpEndDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCustomFollowUpEndDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtCustomFollowUpEndDate" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxCustomFollowUpEndDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtCustomFollowUpEndDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCustomFollowUpEndDate" runat="server" ControlToValidate="txtCustomFollowUpEndDate" ErrorMessage="Custom Follow Up End Date Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" TargetControlID="txtCustomFollowUpEndDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCustomFollowUpEndDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCustomFollowUpEndDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCustomFollowUpReminderDate1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCustomFollowUpReminderDate1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtCustomFollowUpReminderDate" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="TextBoxCustomFollowUpReminderDate_CalendarExtender" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtCustomFollowUpReminderDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCustomFollowUpReminderDate" runat="server" ControlToValidate="txtCustomFollowUpReminderDate" ErrorMessage="Custom Follow Up Reminder Date Required." CssClass="Validation" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" TargetControlID="txtCustomFollowUpReminderDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCustomFollowUpReminderDate2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCustomFollowUpReminderDate2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblForwardToEmp_ID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtForwardToEmp_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpForwardToEmp_ID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblForwardToEmp_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtForwardToEmp_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblOccurance_ID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOccurance_ID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpOccurance_ID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblOccurance_ID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtOccurance_ID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        
                                                            <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblRemarks1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRemarks1s" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRemarks" runat="server" ControlToValidate="txtRemarks" ErrorMessage="Remarks Required." CssClass="Validation" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblRemarks2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtRemarks2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>--%>

                                                                <%-- <div class="col-md-6">
                                                                <div class="form-group" style="color: ">
                                                                    <asp:Label runat="server" ID="lblCruipID1s" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCruipID1s" class="col-md-4 control-label" Visible="false"></asp:TextBox><div class="col-md-8">
                                                                        <asp:DropDownList ID="drpCruipID" runat="server" CssClass="table-group-action-input form-control input-medium"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Label runat="server" ID="lblCruipID2h" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="txtCruipID2h" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                </div>
                                                            </div>--%>

                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-body">
                                                                            <div class="form-group">
                                                                                <label class="control-label col-md-2">Remark</label>
                                                                                <div class="col-md-10">
                                                                                    <%--<asp:TextBox ID="txtReminder" name="content" data-provide="markdown" class="form-control" Style="resize: none; height: 157px;" Text="" TextMode="MultiLine" runat="server"></asp:TextBox>--%>
                                                                                    
                                                                                    <CKEditor:CKEditorControl runat="server" ID ="txtReminder"></CKEditor:CKEditorControl>
                                                                                </div>
                                                                            </div>

                                                                        </div>
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
                                <%--  manish--%>
                                <asp:Panel ID="pnldetail" runat="server" Visible="false">
                                    <div class="portlet box blue" id="details">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>Details
                                        <asp:Label runat="server" ID="Label1"></asp:Label>
                                                <asp:TextBox Style="color: #333333" ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <%--<a href="#portlet-config" data-toggle="modal" class="config"></a>--%>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                            <div class="actions btn-set">
                                                <%--<div id="navigation" runat="server" visible="false" class="btn-group btn-group-circle btn-group-solid">
                                            <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />
                                        </div>--%>


                                                <%--<asp:Button ID="Button4" class="btn green-haze btn-circle" runat="server" Text="Categorize"  />--%>
                                                <%-- <asp:Button ID="Button5" class="btn green-haze btn-circle" runat="server" Text="Follow Up"/>
                                         <asp:Button ID="Button6" class="btn green-haze btn-circle" runat="server" Text="Private"/>--%>

                                                <asp:Button ID="Button11" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="AddNew" OnClick="btnAdd_Click" />
                                                <asp:Button ID="Button12" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                                <asp:Button ID="Button13" runat="server" class="btn green-haze btn-circle" OnClick="btnEditLable_Click" Text="Update Label" />
                                                &nbsp;
                                        <asp:LinkButton ID="LinkButton1" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="../assets/global/img/flags/us.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="../assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton3" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="../assets/global/img/flags/fr.png" /></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="portlet-body" style="padding: 0px;">
                                            <div class="portlet-body form">
                                                <div class="tabbable">
                                                    <div class="tab-content no-space">
                                                        <div class="tab-pane active" id="tab_general1">
                                                            <div class="form-body" style="padding: 5px;">
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lbldatecomplete" Text="Date Complete" class="col-md-4 control-label"></asp:Label><div class="col-md-8">
                                                                                <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtdatecomplete" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdatecomplete" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdatecomplete" ErrorMessage="datecomplete Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtStartingDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />--%>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lbltotalwork" Text="Total Work" class="col-md-4 control-label"></asp:Label><div class="col-md-8">
                                                                                <asp:TextBox ID="txttotalwork" runat="server" CssClass="form-control"> </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblMileage" class="col-md-2 control-label" Text="Mileage"></asp:Label>
                                                                            <div class="col-md-10">
                                                                                <asp:TextBox ID="txtmileage" runat="server" CssClass="form-control select2me" Style="width: 100%;">
                                                                               
                                                                                </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblActualwork" Text="Actual Work" class="col-md-4 control-label"></asp:Label><div class="col-md-8">
                                                                                <asp:TextBox ID="txtactualwork" runat="server" CssClass="form-control"> </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblbillinginfo" class="col-md-2 control-label" Text="Billing Information"></asp:Label>
                                                                            <div class="col-md-10">
                                                                                <asp:TextBox ID="txtbillinbinfo" runat="server" CssClass="form-control select2me" Style="width: 100%;">
                                                                               
                                                                                </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblcompany" Text="Company" class="col-md-2 control-label"></asp:Label>
                                                                            <div class="col-md-10">
                                                                                <asp:TextBox ID="txtcompany" runat="server" CssClass="form-control" Style="width: 100%;"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcompany" ErrorMessage="company Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <hr style="margin-top: 0px; border-color: #3598dc" />
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblupdatelist" Text="Update List" class="col-md-2 control-label"></asp:Label>
                                                                            <div class="col-md-10">
                                                                                <asp:TextBox ID="txtupdatelist" Enabled="false" runat="server" CssClass="form-control" Style="width: 100%;"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtupdatelist" ErrorMessage="updatelist Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="Label8" class="col-md-4 control-label"></asp:Label>
                                                                            <div class="col-md-8">
                                                                                <asp:Button ID="btncreate" Enabled="false" Text="Create Unassigned Copy" runat="server" CssClass="form-control"></asp:Button>

                                                                            </div>
                                                                        </div>
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
                                <asp:Panel ID="pnlassigntask" runat="server" Visible="false">
                                    <div class="portlet box blue" id="AssignTask">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>Assign Task
                                        <asp:Label runat="server" ID="Label2"></asp:Label>
                                                <asp:TextBox Style="color: #333333" ID="TextBox2" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                            <div class="actions btn-set">
                                                <%--<div id="navigation" runat="server" visible="false" class="btn-group btn-group-circle btn-group-solid">
                                            <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />
                                        </div>--%>

                                                <%--<asp:Button ID="Button20" class="btn grey-cascade btn-circle" runat="server" Text="Task" />
                                            <asp:Button ID="Button21" class="btn grey-cascade btn-circle" runat="server" Text="Details" />
                                            <asp:Button ID="Button22" class="btn grey-cascade btn-circle" runat="server" Text="Asssign Task" />
                                            <asp:Button ID="Button23" class="btn grey-cascade btn-circle" runat="server" Text="Send Status Reports" />
                                            <asp:Button ID="Button24" class="btn grey-cascade btn-circle" runat="server" Text="Mark Complete" />
                                            <asp:Button ID="Button25" class="btn grey-cascade btn-circle" runat="server" Text="Recurrence" />
                                            <asp:Button ID="Button26" class="btn grey-cascade btn-circle" runat="server" Text="Cancel Assignment" />--%>
                                                <%--<asp:Button ID="Button4" class="btn green-haze btn-circle" runat="server" Text="Categorize"  />--%>
                                                <%-- <asp:Button ID="Button5" class="btn green-haze btn-circle" runat="server" Text="Follow Up"/>
                                         <asp:Button ID="Button6" class="btn green-haze btn-circle" runat="server" Text="Private"/>--%>

                                                <asp:Button ID="btnassignNew" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="AddNew" OnClick="btnassignNew_Click" />
                                                <asp:Button ID="Button28" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                                <asp:Button ID="Button29" runat="server" class="btn green-haze btn-circle" OnClick="btnEditLable_Click" Text="Update Label" />
                                                &nbsp;
                                        <asp:LinkButton ID="LinkButton4" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="../assets/global/img/flags/us.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton5" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="../assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton6" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="../assets/global/img/flags/fr.png" /></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="portlet-body" style="padding: 0px;">
                                            <div class="portlet-body form">
                                                <div class="tabbable">
                                                    <div class="tab-content no-space">
                                                        <div class="tab-pane active" id="tab_general1">
                                                            <div class="form-body" style="padding: 5px;">
                                                                <%--<div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="form-group" style="color: ">
                                                                        <asp:Label runat="server" ID="Label11" Text="To..." class="col-md-2 control-label"></asp:Label><div class="col-md-10">
                                                                            <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control" Style="width: 100%"> </asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtStartingDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblassignactivity" Text="Activity" class="col-md-4 control-label"></asp:Label><div class="col-md-8">
                                                                                <asp:DropDownList ID="drpassignactivity" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblassignproject" Text="Project" class="col-md-2 control-label"></asp:Label><div class="col-md-10">
                                                                                <asp:DropDownList ID="drpassignproject" runat="server" CssClass="form-control select2me" Style="width: 100%"></asp:DropDownList>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblassigntofield" Text="To Field" class="col-md-4 control-label"></asp:Label><div class="col-md-8">
                                                                                <asp:DropDownList ID="drpassigntofield" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblassignowner" Text="Owner" class="col-md-2 control-label"></asp:Label><div class="col-md-10">
                                                                                <asp:TextBox ID="txtassignowner" runat="server" CssClass="form-control" Enabled="false" Style="width: 100%"> </asp:TextBox>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblassignsubject" Text="Subject" class="col-md-2 control-label"></asp:Label><div class="col-md-10">
                                                                                <asp:TextBox ID="txtassignsubject" runat="server" CssClass="form-control" Style="width: 100%"> </asp:TextBox>
                                                                                <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtStartingDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />--%>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <hr style="margin-top: 0px; border-color: #3598dc" />
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblassignstartdate" Text="Start Date" class="col-md-4 control-label"></asp:Label><div class="col-md-8">
                                                                                <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtassignstartdate" runat="server" CssClass="form-control"> </asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtdatecomplete" Format="dd/MM/yyyy hh:mm:ss tt"></cc1:CalendarExtender>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtassignstartdate" ErrorMessage="assignstartdate Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtStartingDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblassignstatus" Text="Status" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox10" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="drpassignstatus" runat="server" CssClass="form-control select2me" Style="width: 100%;">
                                                                                    <asp:ListItem Value="Not Started">Not Started</asp:ListItem>
                                                                                    <asp:ListItem Value="In Progress">In Progress</asp:ListItem>
                                                                                    <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                                                                    <asp:ListItem Value="Waiting on Somone else">Waiting on Somone else</asp:ListItem>
                                                                                    <asp:ListItem Value="Deferred">Deferred</asp:ListItem>
                                                                                </asp:DropDownList>

                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblassigntasktype" Text="Task Type" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox22" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="drpassigntasktype" runat="server" CssClass="form-control select2me" Style="width: 100%;">
                                                                                    <asp:ListItem Value="Private">Private</asp:ListItem>
                                                                                    <asp:ListItem Value="High Importance">High Importance</asp:ListItem>
                                                                                    <asp:ListItem Value="Low Importance">Low Importance</asp:ListItem>

                                                                                </asp:DropDownList>

                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblassignduedate" Text="Due Date" class="col-md-4 control-label"></asp:Label><div class="col-md-8">
                                                                                <asp:TextBox Placeholder="DD/MM/YYYY" ID="txtassignduedate" runat="server" CssClass="form-control"> </asp:TextBox>
                                                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" PopupButtonID="calender" TargetControlID="txtDueDate" Format="MM/dd/yyyy hh:mm:ss tt"></cc1:CalendarExtender>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtassignduedate" ErrorMessage="assignduedate Required." CssClass="Validation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtDueDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblassignpriority" Text="Priority" class="col-md-4 control-label"></asp:Label><asp:TextBox runat="server" ID="TextBox16" class="col-md-4 control-label" Visible="false"></asp:TextBox>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="drpassignpriority" runat="server" CssClass="form-control select2me" Style="width: 100%;">
                                                                                    <asp:ListItem Value="Low">Low</asp:ListItem>
                                                                                    <asp:ListItem Value="Normal">Normal</asp:ListItem>
                                                                                    <asp:ListItem Value="High">High</asp:ListItem>
                                                                                </asp:DropDownList>

                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="lblassigncomplete" Text="Complete" class="col-md-4 control-label"></asp:Label>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="drpassigncomplete" runat="server" CssClass="form-control select2me" Style="width: 100%;">
                                                                                    <asp:ListItem Value="0">0%</asp:ListItem>
                                                                                    <asp:ListItem Value="25">25%</asp:ListItem>
                                                                                    <asp:ListItem Value="50">50%</asp:ListItem>
                                                                                    <asp:ListItem Value="75">75%</asp:ListItem>
                                                                                    <asp:ListItem Value="100">100%</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <hr style="margin-top: 0px; border-color: #3598dc" />
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <div class="col-md-9" style="margin-top: 8px;">
                                                                                <asp:CheckBox ID="cbkeepanupdate" Text="Keep an updated copy of this task on my task list" runat="server" Checked="true" />
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group" style="color: ">
                                                                            <div class="col-md-9" style="margin-top: 8px;">
                                                                                <asp:CheckBox ID="cbsendmea" Text="Send me a status report when this task is complete" runat="server" Checked="true" />
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-body">
                                                                            <div class="form-group">
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="txtassignremark" name="content" data-provide="markdown" class="form-control" Style="resize: none; height: 157px;" Text="" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>

                                                                        </div>
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
                                <asp:Panel ID="pnlsendstatus" runat="server" Visible="false">
                                    <div class="portlet box blue" id="SendStatus">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>Send Status Report
                                        <asp:Label runat="server" ID="Label3"></asp:Label>
                                                <asp:TextBox Style="color: #333333" ID="TextBox3" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                            <div class="actions btn-set">
                                                <%--<div id="navigation" runat="server" visible="false" class="btn-group btn-group-circle btn-group-solid">
                                            <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />
                                        </div>--%>

                                                <%--<asp:Button ID="Button30" class="btn grey-cascade btn-circle" runat="server" Text="Task" />
                                            <asp:Button ID="Button31" class="btn grey-cascade btn-circle" runat="server" Text="Details" />
                                            <asp:Button ID="Button32" class="btn grey-cascade btn-circle" runat="server" Text="Asssign Task" />
                                            <asp:Button ID="Button33" class="btn grey-cascade btn-circle" runat="server" Text="Send Status Reports" />
                                            <asp:Button ID="Button34" class="btn grey-cascade btn-circle" runat="server" Text="Mark Complete" />
                                            <asp:Button ID="Button35" class="btn grey-cascade btn-circle" runat="server" Text="Recurrence" />
                                            <asp:Button ID="Button36" class="btn grey-cascade btn-circle" runat="server" Text="Cancel Assignment" />--%>
                                                <%--<asp:Button ID="Button4" class="btn green-haze btn-circle" runat="server" Text="Categorize"  />--%>
                                                <%-- <asp:Button ID="Button5" class="btn green-haze btn-circle" runat="server" Text="Follow Up"/>
                                         <asp:Button ID="Button6" class="btn green-haze btn-circle" runat="server" Text="Private"/>--%>

                                                <asp:Button ID="Button37" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="AddNew" OnClick="btnAdd_Click" />
                                                <asp:Button ID="Button38" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                                <asp:Button ID="Button39" runat="server" class="btn green-haze btn-circle" OnClick="btnEditLable_Click" Text="Update Label" />
                                                &nbsp;
                                        <asp:LinkButton ID="LinkButton7" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="../assets/global/img/flags/us.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton8" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="../assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton9" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="../assets/global/img/flags/fr.png" /></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="portlet-body" style="padding: 0px;">
                                            <div class="portlet-body form">
                                                <div class="tabbable">
                                                    <div class="tab-content no-space">
                                                        <div class="tab-pane active" id="tab_general1">
                                                            <div class="form-body" style="padding: 5px;">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="Label16" Text="To..." class="col-md-2 control-label"></asp:Label><div class="col-md-10">
                                                                                <asp:TextBox ID="TextBox15" runat="server" CssClass="form-control" Style="width: 100%"> </asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="txtStartingDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="Label20" Text="Cc..." class="col-md-2 control-label"></asp:Label><div class="col-md-10">
                                                                                <asp:TextBox ID="TextBox18" runat="server" CssClass="form-control" Style="width: 100%"> </asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="txtStartingDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group" style="color: ">
                                                                            <asp:Label runat="server" ID="Label21" Text="Subject" class="col-md-2 control-label"></asp:Label><div class="col-md-10">
                                                                                <asp:TextBox ID="TextBox19" runat="server" Text="Task Status Report: " CssClass="form-control" Style="width: 100%"> </asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" TargetControlID="txtStartingDate" ValidChars="/,-,:, " FilterType="Custom, numbers" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="form-body">
                                                                            <div class="form-group">

                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TextBox20" name="content" data-provide="markdown" class="form-control" Style="resize: none; height: 157px;" Text="" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-body">
                                                                            <div class="form-group">
                                                                                <div class="colo-md-4">
                                                                                    <asp:Button ID="Button15" runat="server" data-title="Inbox" Style="background: #3598dc none repeat scroll 0 0; width: 75px; border-left: medium none; color: #fff; display: block; font-size: 15px; margin-bottom: 1px; padding: 8px 14px; text-align: left;" class="btn" Text="Send.." />
                                                                                </div>
                                                                            </div>
                                                                        </div>
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
                                <asp:Panel ID="pnlmarkcomplaete" runat="server" Visible="false">
                                    <div class="portlet box blue" id="MarkComplete">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>Mark Complete
                                        <asp:Label runat="server" ID="Label4"></asp:Label>
                                                <asp:TextBox Style="color: #333333" ID="TextBox4" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                            <div class="actions btn-set">
                                                <%--  <div id="navigation" runat="server" visible="false" class="btn-group btn-group-circle btn-group-solid">
                                            <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />
                                        </div>--%>

                                                <%-- <asp:Button ID="Button40" class="btn grey-cascade btn-circle" runat="server" Text="Task" />
                                            <asp:Button ID="Button41" class="btn grey-cascade btn-circle" runat="server" Text="Details" />
                                            <asp:Button ID="Button42" class="btn grey-cascade btn-circle" runat="server" Text="Asssign Task" />
                                            <asp:Button ID="Button43" class="btn grey-cascade btn-circle" runat="server" Text="Send Status Reports" />
                                            <asp:Button ID="Button44" class="btn grey-cascade btn-circle" runat="server" Text="Mark Complete" />
                                            <asp:Button ID="Button45" class="btn grey-cascade btn-circle" runat="server" Text="Recurrence" />
                                            <asp:Button ID="Button46" class="btn grey-cascade btn-circle" runat="server" Text="Cancel Assignment" />--%>
                                                <%--<asp:Button ID="Button4" class="btn green-haze btn-circle" runat="server" Text="Categorize"  />--%>
                                                <%-- <asp:Button ID="Button5" class="btn green-haze btn-circle" runat="server" Text="Follow Up"/>
                                         <asp:Button ID="Button6" class="btn green-haze btn-circle" runat="server" Text="Private"/>--%>

                                                <asp:Button ID="Button47" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="AddNew" OnClick="btnAdd_Click" />
                                                <asp:Button ID="Button48" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                                <asp:Button ID="Button49" runat="server" class="btn green-haze btn-circle" OnClick="btnEditLable_Click" Text="Update Label" />
                                                &nbsp;
                                        <asp:LinkButton ID="LinkButton10" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="../assets/global/img/flags/us.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton11" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="../assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton12" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="../assets/global/img/flags/fr.png" /></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="portlet-body" style="padding: 0px;">
                                            <div class="portlet-body form">
                                                <div class="tabbable">
                                                    <div class="tab-content no-space">
                                                        <div class="tab-pane active" id="tab_general1">
                                                            <div class="form-body" style="padding: 5px;">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlrecurrnce" runat="server" Visible="false">
                                    <div class="portlet box blue" id="Recurrence">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-gift"></i>Recurrence
                                        <asp:Label runat="server" ID="Label6"></asp:Label>
                                                <asp:TextBox Style="color: #333333" ID="TextBox5" runat="server" Visible="false"></asp:TextBox>
                                            </div>
                                            <div class="tools">
                                                <a href="javascript:;" class="collapse"></a>
                                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                                <a href="javascript:;" class="reload"></a>
                                                <a href="javascript:;" class="remove"></a>
                                            </div>
                                            <div class="actions btn-set">
                                                <%--<div id="navigation" runat="server" visible="false" class="btn-group btn-group-circle btn-group-solid">
                                            <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />
                                        </div>--%>

                                                <%-- <asp:Button ID="Button50" class="btn grey-cascade btn-circle" runat="server" Text="Task" />
                                            <asp:Button ID="Button51" class="btn grey-cascade btn-circle" runat="server" Text="Details" />
                                            <asp:Button ID="Button52" class="btn grey-cascade btn-circle" runat="server" Text="Asssign Task" />
                                            <asp:Button ID="Button53" class="btn grey-cascade btn-circle" runat="server" Text="Send Status Reports" />
                                            <asp:Button ID="Button54" class="btn grey-cascade btn-circle" runat="server" Text="Mark Complete" />
                                            <asp:Button ID="Button55" class="btn grey-cascade btn-circle" runat="server" Text="Recurrence" />
                                            <asp:Button ID="Button56" class="btn grey-cascade btn-circle" runat="server" Text="Cancel Assignment" />--%>
                                                <%--<asp:Button ID="Button4" class="btn green-haze btn-circle" runat="server" Text="Categorize"  />--%>
                                                <%-- <asp:Button ID="Button5" class="btn green-haze btn-circle" runat="server" Text="Follow Up"/>
                                         <asp:Button ID="Button6" class="btn green-haze btn-circle" runat="server" Text="Private"/>--%>

                                                <asp:Button ID="Button57" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="AddNew" OnClick="btnAdd_Click" />
                                                <asp:Button ID="Button58" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                                <asp:Button ID="Button59" runat="server" class="btn green-haze btn-circle" OnClick="btnEditLable_Click" Text="Update Label" />
                                                &nbsp;
                                        <asp:LinkButton ID="LinkButton13" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="../assets/global/img/flags/us.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton14" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="../assets/global/img/flags/ae.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton15" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src="../assets/global/img/flags/fr.png" /></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="portlet-body" style="padding: 0px;">
                                            <div class="portlet-body form">
                                                <div class="tabbable">
                                                    <div class="tab-content no-space">
                                                        <div class="tab-pane active" id="tab_general1">
                                                            <div class="form-body" style="padding: 5px;">
                                                                <div class="row">
                                                                    <div class="col-md-2">
                                                                        <div class="form-body">
                                                                            <div class="form-group">
                                                                                <div class="colo-md-4">
                                                                                    <asp:RadioButton ID="rdodaily" runat="server" />Daily
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-2">
                                                                        <div class="form-body">
                                                                            <div class="form-group">
                                                                                <div class="colo-md-4">
                                                                                    <asp:RadioButton ID="RadioButton1" runat="server" />Weekly
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
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
                            <asp:Panel ID="pnlcancleassignment" runat="server" Visible="false">
                                <div class="portlet box blue" id="Cancleassignment">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-gift"></i>Cancle Assignment
                                        <asp:Label runat="server" ID="Label7"></asp:Label>
                                            <asp:TextBox Style="color: #333333" ID="TextBox6" runat="server" Visible="false"></asp:TextBox>
                                        </div>
                                        <div class="tools">
                                            <a href="javascript:;" class="collapse"></a>
                                            <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                            <a href="javascript:;" class="reload"></a>
                                            <a href="javascript:;" class="remove"></a>
                                        </div>
                                        <div class="actions btn-set">
                                            <%--<div id="navigation" runat="server" visible="false" class="btn-group btn-group-circle btn-group-solid">
                                            <asp:Button ID="btnFirst" class="btn red" runat="server" OnClick="btnFirst_Click" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" OnClick="btnNext_Click" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" OnClick="btnPrev_Click" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" OnClick="btnLast_Click" />
                                        </div>--%>

                                            <%--<asp:Button ID="Button60" class="btn grey-cascade btn-circle" runat="server" Text="Task" />
                                            <asp:Button ID="Button61" class="btn grey-cascade btn-circle" runat="server" Text="Details" />
                                            <asp:Button ID="Button62" class="btn grey-cascade btn-circle" runat="server" Text="Asssign Task" />
                                            <asp:Button ID="Button63" class="btn grey-cascade btn-circle" runat="server" Text="Send Status Reports" />
                                            <asp:Button ID="Button64" class="btn grey-cascade btn-circle" runat="server" Text="Mark Complete" />
                                            <asp:Button ID="Button65" class="btn grey-cascade btn-circle" runat="server" Text="Recurrence" />
                                            <asp:Button ID="Button66" class="btn grey-cascade btn-circle" runat="server" Text="Cancel Assignment" />--%>
                                            <%--<asp:Button ID="Button4" class="btn green-haze btn-circle" runat="server" Text="Categorize"  />--%>
                                            <%-- <asp:Button ID="Button5" class="btn green-haze btn-circle" runat="server" Text="Follow Up"/>
                                         <asp:Button ID="Button6" class="btn green-haze btn-circle" runat="server" Text="Private"/>--%>

                                            <asp:Button ID="Button67" runat="server" class="btn green-haze btn-circle" ValidationGroup="submit" Text="AddNew" OnClick="btnAdd_Click" />
                                            <asp:Button ID="Button68" runat="server" class="btn green-haze btn-circle" OnClick="btnCancel_Click" Text="Cancel" />
                                            <asp:Button ID="Button69" runat="server" class="btn green-haze btn-circle" OnClick="btnEditLable_Click" Text="Update Label" />
                                            &nbsp;
                                        <asp:LinkButton ID="LinkButton16" Style="color: #fff; width: 60px; padding: 0px;" runat="server" OnClick="LanguageEnglish_Click">E&nbsp;<img src="../assets/global/img/flags/us.png" /></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton17" Style="color: #fff; width: 40px; padding: 0px;" runat="server" OnClick="LanguageArabic_Click">A&nbsp;<img src="../assets/global/img/flags/ae.png" /></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton18" Style="color: #fff; width: 50px; padding: 0px;" runat="server" OnClick="LanguageFrance_Click">F&nbsp;<img src=".../assets/global/img/flags/fr.png" /></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="portlet-body" style="padding: 0px;">
                                        <div class="portlet-body form">
                                            <div class="tabbable">
                                                <div class="tab-content no-space">
                                                    <div class="tab-pane active" id="tab_general1">
                                                        <div class="form-body" style="padding: 5px;">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
                            <%--  manish--%>
                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>
                                        <asp:Label runat="server" ID="Label5"></asp:Label>
                                        List
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <a href="javascript:;" class="reload"></a>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <asp:Panel runat="server" ID="pnlGrid">
                                        <div class="tab-content">
                                            <div id="tab_1_1" class="tab-pane active">

                                                <div class="tab-content no-space">
                                                    <div class="tab-pane active" id="tab_general2">
                                                        <div class="table-container" style="">


                                                            <div class="portlet-body">
                                                                <table class="table table-striped table-bordered table-hover" id="sample_1">
                                                                    <thead>
                                                                        <tr>
                                                                            <th style="width: 60px;">ACTION</th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhSubject" Text="Subject"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhStartingDate" Text="Starting Date"></asp:Label></th>

                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhTaskStatus" Text="Task Status"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhDueDate" Text="Due Date"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhPriority" Text="Priority"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhCompletedPerctange" Text="Completed Perctange"></asp:Label></th>

                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhActive" Text="Active"></asp:Label></th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:ListView ID="Listview1" runat="server" OnItemCommand="Listview1_ItemCommand" DataKey="TenentId,LocationID,TaskID,CerticateNo,ForEmp_ID,PerformingEmp_ID,Subject,StartingDate,TaskType,TaskStatus,DueDate,Priority,ActualCompletionDate,CompletedPerctange,ReminderDate,ReminderTime,Categories,FollowUp,CustomFollowUpStartDate,CustomFollowUpEndDate,CustomFollowUpReminderDate,ForwardToEmp_ID,Occurance_ID,Remarks,Active" DataKeyNames="TenentId,LocationID,TaskID,CerticateNo,ForEmp_ID,PerformingEmp_ID,Subject,StartingDate,TaskType,TaskStatus,DueDate,Priority,ActualCompletionDate,CompletedPerctange,ReminderDate,ReminderTime,Categories,FollowUp,CustomFollowUpStartDate,CustomFollowUpEndDate,CustomFollowUpReminderDate,ForwardToEmp_ID,Occurance_ID,Remarks,Active">
                                                                            <LayoutTemplate>
                                                                                <tr id="ItemPlaceholder" runat="server">
                                                                                </tr>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:LinkButton ID="btnEdit" CommandName="btnEdit" CommandArgument='<%# Eval("TaskID")%>' runat="server" class="btn btn-sm yellow filter-submit margin-bottom"><i class="glyphicon glyphicon-edit " style="font-size :13px;"></i></asp:LinkButton></td>
                                                                                                <td>
                                                                                                    <asp:LinkButton ID="btnDelete" CommandName="btnDelete" OnClientClick="return ConfirmMsg();" CommandArgument='<%# Eval("TaskID")%>' runat="server" class="btn btn-sm red filter-cancel"><i class="glyphicon glyphicon-remove " style="font-size :13px;"></i></asp:LinkButton></td>
                                                                                                <%-- <td><asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "PrintMDSF.aspx?ID="+ Eval("JobId")%>' CommandName="btnPrint" CommandArgument='<%# Eval("JobId")%>' runat="server" class="btn btn-sm green filter-submit margin-bottom"><i class="fa fa-print"></i></asp:LinkButton></td>--%>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject")%>'></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblStartingDate" runat="server" Text='<%# Eval("StartingDate")%>'></asp:Label></td>

                                                                                    <td>
                                                                                        <asp:Label ID="lblTaskStatus" runat="server" Text='<%# Eval("TaskStatus")%>'></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblDueDate" runat="server" Text='<%# Eval("DueDate")%>'></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblPriority" runat="server" Text='<%# Eval("Priority")%>'></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lblCompletedPerctange" runat="server" Text='<%# Eval("CompletedPerctange")%>'></asp:Label></td>

                                                                                    <td>
                                                                                        <asp:Label ID="lblActive" runat="server" Text='<%# Eval("Active")%>'></asp:Label></td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="hideID" runat="server" Value="" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../assets/global/plugins/ckeditor/ckeditor.js"></script>
</asp:Content>

