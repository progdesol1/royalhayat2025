<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="QuestionList.aspx.cs" Inherits="Web.CRM.QuestionList" %>

<%@ Register Src="~/CRM/UserControl/RightPanelUC.ascx" TagPrefix="uc1" TagName="RightPanelUC" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .abc {
            margin-top: -34px !important;
        }

        .chkChoice input label {
            margin-left: -20px;
        }

        .chkChoice td {
            padding-left: 20px;
        }
    </style>
    <script type = "text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
                <%--<ul class="page-breadcrumb breadcrumb">
                    <li>
                        <a href="index.aspx">HOME </a>
                        <i class="fa fa-circle"></i>
                    </li>
                    <li>
                        <a href="#">Question List</a>
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
                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Question List 
                                        
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="btnPagereload" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                    <div class="actions btn-set">
                                        <div id="navigation" runat="server" class="btn-group btn-group-circle btn-group-solid">
                                            <asp:Button ID="btnFirst" class="btn red" runat="server" Text="First" />
                                            <asp:Button ID="btnNext" class="btn green" runat="server" Text="Next" />
                                            <asp:Button ID="btnPrev" class="btn purple" runat="server" Text="Prev" />
                                            <asp:Button ID="btnLast" class="btn grey-cascade" runat="server" Text="Last" />
                                        </div>
                                        <asp:Button ID="btnAdd" runat="server" class="btn green-haze btn-circle" Text="AddNew" />
                                        <asp:Button ID="btnCancel" runat="server" class="btn green-haze btn-circle" Text="Cancel" />
                                        <asp:Button ID="btnEditLable" runat="server" class="btn green-haze btn-circle" Text="Update Label" />
                                        &nbsp;
                                        <asp:LinkButton ID="LanguageEnglish" Style="color: #fff; width: 60px; padding: 0px;" runat="server">E&nbsp;<img src="/assets/global/img/flags/us.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageArabic" Style="color: #fff; width: 40px; padding: 0px;" runat="server">A&nbsp;<img src="/assets/global/img/flags/ae.png" /></asp:LinkButton>
                                        <asp:LinkButton ID="LanguageFrance" Style="color: #fff; width: 50px; padding: 0px;" runat="server">F&nbsp;<img src="/assets/global/img/flags/fr.png" /></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="portlet-body form">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general1">
                                                    <div class="col-md-12" id="Divmainsub" runat="server">
                                                        <div class="form-body">
                                                            <div class="portlet-body form">
                                                                <div class="tab-content">
                                                                    <div id="tab_1_1" class="tab-pane active">

                                                                        <div class="table-container" style="">
                                                                            <div class="portlet-body">
                                                                                <div class="portlet-title">
                                                                                     <asp:Label ID="lblInfo" runat="server"   ></asp:Label> <br />
                                                                                    <asp:Button ID="btnCompleteit" class="btn default" runat="server" Text="Complete It" Style="height: 35px; margin-top: 10px;" OnClick="btnCompleteit_Click" />
                                                                                    
                                                                                </div>
                                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <div class="row">
                                                                                            <div class="col-md-12">

                                                                                                <asp:Repeater ID="repAccordian" runat="server" OnItemCommand="repAccordian_ItemCommand" OnItemDataBound="repAccordian_ItemDataBound">
                                                                                                    <ItemTemplate>
                                                                                                       <div>
                                                                                                            <table>

                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>.
                                                                                                                                <asp:Label ID="Label3" runat="server" Text=' <%#Eval("QuestionLang1") %>'></asp:Label>
                                                                                                                        <asp:Label ID="lblMultipleNote" runat="server"></asp:Label><asp:Label ID="lblNoteSingle" runat="server"></asp:Label>
                                                                                                                        <asp:Label ID="lbl_QUESTION_id" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                                                                                        <asp:Label ID="lblChoiceType" runat="server" Text='<%# Eval("ChoiseType") %>' Visible="false"></asp:Label>
                                                                                                                        <asp:HiddenField ID="hiddenqueby" Value='<%# Eval("CreateBy") %>' runat="server" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                     <hr>
                                                                                                                    <asp:Repeater ID="repAccordianSubQuion" runat="server" OnItemDataBound="repAccordianSubQuion_ItemDataBound">
                                                                                                                        <ItemTemplate>
                                                                                                                            <div>
                                                                                                                                <table style="margin-left: 30px;">

                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>.
                                                                                                                                <asp:Label ID="Label3" runat="server" Text=' <%#Eval("QuestionLang1") %>'></asp:Label>
                                                                                                                                            <asp:Label ID="lblQutionID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                                                                                                            <asp:Label ID="lblsubChoyesh" runat="server" Text='<%# Eval("ChoiseType") %>' Visible="false"></asp:Label>
                                                                                                                                             <asp:Repeater ID="Repeater3" runat="server">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <label style="padding-left: 10px;">
                                                                                                                                                        <asp:RadioButton ID="radioswapYes" runat="server" GroupName="yreno" Text="Yes" />
                                                                                                                                                        <asp:RadioButton ID="radioswapNo" runat="server" GroupName="yreno" Text="No" /></label>
                                                                                                                                                 
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:Repeater>
                                                                                                                                             <asp:Repeater ID="Repeater5" runat="server">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <asp:TextBox ID="range_4"  runat="server" ></asp:TextBox> 
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:Repeater>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td class="md-radiobtn" style="padding-top: 10px; padding-left: 15px;">
                                                                                                                                            <div class="radio-list">

                                                                                                                                                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">

                                                                                                                                                    <ItemTemplate>

                                                                                                                                                        <label>
                                                                                                                                                            <asp:RadioButtonList ID="QuestionsList" RepeatDirection="Horizontal" runat="server"></asp:RadioButtonList></label>

                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:Repeater>
                                                                                                                                            </div>

                                                                                                                                            <div class="checkbox-list">
                                                                                                                                                <asp:Repeater ID="listCheckBoxList" runat="server" OnItemDataBound="listCheckBoxList_ItemDataBound">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <label>
                                                                                                                                                            <asp:CheckBoxList ID="chmultipletpreview" runat="server" RepeatDirection="Horizontal">
                                                                                                                                                            </asp:CheckBoxList></label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:Repeater>
                                                                                                                                            </div>
                                                                                                                                           
                                                                                                                                           
                                                                                                                                            <asp:Repeater ID="Repeater4" runat="server" OnItemDataBound="Repeater4_ItemDataBound">
                                                                                                                                                <ItemTemplate>

                                                                                                                                                    <label class="control-label col-md-1">Option</label>

                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:ListBox ID="ListAllOption11" runat="server" SelectionMode="Multiple" Width="200px" Height="200px"></asp:ListBox>
                                                                                                                                                    </div>
                                                                                                                                                    <div class="col-md-1" style="padding-left: 0px; margin-top: 31px;">
                                                                                                                                                        <asp:Button ID="btnbefore" runat="server" class="btn default btn-lg" Text=">>" />
                                                                                                                                                        <asp:Button ID="btnAfter" runat="server" class="btn default btn-lg" Text="<<" Style="padding-top: 10px; margin-top: 9px; margin-left: 1px;" />
                                                                                                                                                    </div>
                                                                                                                                                    <label class="control-label col-md-1">Answer</label>
                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:ListBox ID="ListSelectOption11" runat="server" SelectionMode="Multiple" Width="200px" Height="200px"></asp:ListBox>
                                                                                                                                                    </div>

                                                                                                                                                    <br />
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:Repeater>
                                                                                                                                        </td>
                                                                                                                                    </tr>

                                                                                                                                </table>
                                                                                                                            </div>
                                                                                                                             <br />
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:Repeater>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="md-radiobtn" style="width:100%">
                                                                                                                        <div class="radio-list">

                                                                                                                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1ItemEventHandler">

                                                                                                                                <ItemTemplate>

                                                                                                                                    <label>
                                                                                                                                        <asp:RadioButtonList ID="QuestionsList" RepeatDirection="Horizontal" runat="server"></asp:RadioButtonList></label>

                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:Repeater>
                                                                                                                        </div>

                                                                                                                        <div class="checkbox-list">
                                                                                                                            <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <label>
                                                                                                                                        <asp:CheckBoxList ID="chmultipletpreview" runat="server" RepeatDirection="Horizontal">
                                                                                                                                        </asp:CheckBoxList></label>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:Repeater>
                                                                                                                        </div>
                                                                                                                        <asp:Repeater ID="Repeater3" runat="server">
                                                                                                                            <ItemTemplate>
                                                                                                                                <label>
                                                                                                                                    <asp:RadioButton ID="radioswapYes" GroupName="singbuton" runat="server" />Yes
                                                                                                                                        <asp:RadioButton ID="radioswapNo" GroupName="singbuton" runat="server" />No</label>
                                                                                                                                <br />
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:Repeater>
                                                                                                                        <asp:Repeater ID="Repeater5" runat="server">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:TextBox ID="range_4" runat="server"></asp:TextBox>
                                                                                                                                <br />

                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:Repeater>
                                                                                                                        <asp:Repeater ID="Repeater4" runat="server" OnItemDataBound="Repeater4_ItemDataBound">
                                                                                                                            <ItemTemplate>

                                                                                                                                <label class="control-label col-md-1">Option</label>

                                                                                                                                <div class="col-md-4">
                                                                                                                                    <asp:ListBox ID="ListAllOption11" runat="server" SelectionMode="Multiple" Width="200px" Height="200px"></asp:ListBox>
                                                                                                                                </div>
                                                                                                                                <div class="col-md-1" style="padding-left: 0px; margin-top: 31px;">
                                                                                                                                    <asp:Button ID="btnbefore" runat="server" class="btn default btn-lg" Text=">>" />
                                                                                                                                    <asp:Button ID="btnAfter" runat="server" class="btn default btn-lg" Text="<<" Style="padding-top: 10px; margin-top: 9px; margin-left: 1px;" />
                                                                                                                                </div>
                                                                                                                                <label class="control-label col-md-1">Answer</label>
                                                                                                                                <div class="col-md-4">
                                                                                                                                    <asp:ListBox ID="ListSelectOption11" runat="server" SelectionMode="Multiple" Width="200px" Height="200px"></asp:ListBox>
                                                                                                                                </div>

                                                                                                                                <br />
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:Repeater>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                            <td>
                                                                                                                                <label runat="server" id="Label104" class="control-label col-md-2">
                                                                                                                                    <asp:Label runat="server" ID="Label105" >Remarks</asp:Label>
                                                                                                                                </label>
                                                                                                                           
                                                                                                                                <div class="col-md-4">
                                                                                                                                    <asp:TextBox ID="txtRemarks" runat="server"  TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                                                                                </div>
                                                                                                                           
                                                                                                                            <asp:Label runat="server" ID="Label2"  class="col-md-2 control-label">Answer Date</asp:Label>
                                                                                                                            <div class="col-md-2">
                                                                                                                                <asp:TextBox ID="txtAnsDatetime" runat="server" CssClass="form-control" ReadOnly="true" ValidationGroup="repe"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                   
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <div class="col-md-10"  style="padding-top: 10px; padding-bottom: 10px;" >

                                                                                                                            <asp:Panel ID="pnlContactDrop" runat="server" Visible="false">
                                                                                                                                <asp:Label runat="server"  ID="lblcontact" class="col-md-4 control-label">Contact Who is Answered</asp:Label>
                                                                                                                                <div class="col-md-5">
                                                                                                                                    <asp:DropDownList ID="DrpContact" runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                                                                                </div>
                                                                                                                                <div class="col-md-2" >
                                                                                                                                   
                                                                                                                                    <asp:LinkButton ID="LinkButtonContact" CommandName="LinkContact"  runat="server" ForeColor="#3598dc" > <i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                                                                                                </div>
                                                                                                                            </asp:Panel>
                                                                                                                           
                                                                                                                            <asp:Panel ID="pnlCompany" runat="server" Visible="false">
                                                                                                                                <asp:Label runat="server" ID="Label1" class="col-md-4 control-label">Contact Who is Answered</asp:Label>
                                                                                                                                <div class="col-md-5">
                                                                                                                                    <asp:DropDownList ID="DrpCompany"  runat="server" CssClass="form-control select2me"></asp:DropDownList>
                                                                                                                                </div>
                                                                                                                                <div class="col-md-2" >
                                                                                                                                    <asp:LinkButton ID="linkcompany" runat="server" CommandName="LinkCompany" ForeColor="#3598dc"> <i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                                                                                                </div>
                                                                                                                            </asp:Panel>


                                                                                                                        </div>
                                                                                                                   
                                                                                                                        <div class="col-md-2" style="padding-top: 10px; padding-bottom: 10px;">
                                                                                                                            <asp:LinkButton ID="BtnSaveOpin" CommandName="btnQueSave" Style="padding: 7px 10px;" class="btn btn-xs green" CommandArgument='<%# Eval("ID")%>' runat="server" Text="Save" ValidationGroup="repe"></asp:LinkButton>
                                                                                                                        </div>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                              
                                                                                                            </table>
                                                                                                        </div>
                                                                                                    </ItemTemplate>
                                                                                                </asp:Repeater>
                                                                                            </div>
                                                                                        </div>
                                                                                        <br />

                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>


                                                                        </div>

                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                     <%-- <asp:UpdatePanel ID="updaterightpanel" runat="server">
                                                        <ContentTemplate>
                                                    <div class="col-md-4" id="Div1" runat="server">
                                                        <uc1:RightPanelUC runat="server" ID="RightPanelUC1" />
                                                    </div>

                                                    <%--   </ContentTemplate>
                                                       
                                                    </asp:UpdatePanel>--%>
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
    <script src="assets/admin/pages/scripts/components-ion-sliders.js"></script>
    <script src="assets/apps/scripts/todo-2.min.js"></script>
    <script>
        jQuery(document).ready(function () {

            ComponentsNoUiSliders.init();
            TableManaged.init();
        });
    </script>
</asp:Content>
