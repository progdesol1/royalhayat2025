<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="Attendance_tna.aspx.cs" Inherits="Web.CRM.Attendance_tna" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script src="popup/tie-scripts.htm"></script>
   
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
                        <a href="#">Attendance</a>
                    </li>
                </ul>--%>

                <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                    <div class="alert alert-success alert-dismissable">
                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </asp:Panel>
                <!--pageheader-->
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal form-row-seperated">
                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-gift"></i>Attendance
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse"></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                        <asp:LinkButton ID="btnPagereload" runat="server"><img src="/assets/admin/layout4/img/reload.png" style="margin-top: -7px;" /></asp:LinkButton>
                                        <a href="javascript:;" class="remove"></a>
                                    </div>
                                  
                                </div>

                                <div class="portlet-body">
                                    <div class="portlet-body form">
                                        <div class="tabbable">
                                            <div class="tab-content no-space">
                                                <div class="tab-pane active" id="tab_general1">
                                                    <div class="form-body">
                                                        <table width="100%">
                                                            <tr>
                                                                

                                                                <td style="width: 100px">
                                                                    <asp:DropDownList ID="drpUsers" CssClass="select2-container form-control select2me" Width="150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpUsers_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                
                                                                <td style="width: 70px">
                                                                     <asp:Button ValidationGroup="userSubmit" ID="btnThumRegister" CssClass="btn btn-sm red" runat="server" Text="Thum Register" />
                                                                     <asp:LinkButton ID="linkCheckIn" Visible="false"   runat="server" OnClick="linkCheckIn_Click"><span class="badge badge-primary">Check In </span></asp:LinkButton>
                        <asp:LinkButton ID="linkCheckOut"   runat="server" OnClick="linkCheckOut_Click"  Visible="false"><span class="badge badge-primary">Check Out </span></asp:LinkButton></li>
                                                                </td>
                                                                <td style="width: 100px">
                                                                    <asp:DropDownList Visible="false" ID="drpTimeStatus" CssClass="select2-container form-control select2me" Width="150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpTimeStatus_SelectedIndexChanged">
                                                                       
                                                                        
                                                                         <asp:ListItem  Value="3">OverTime In</asp:ListItem>
                                                                         <asp:ListItem  Value="4">OverTime Out</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                 <td style="width: 70px">
                                                                     <asp:Button ID="BtnTimeStatusProcess" ValidationGroup="processSubmit" Visible="false" CssClass="btn btn-sm red" runat="server" Text="Process" OnClick="BtnTimeStatusProcess_Click"/>
                                                                </td>
                                                                <td><asp:RequiredFieldValidator ForeColor="Red" ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="drpUsers"
                    ErrorMessage="Choose Employee."  ValidationGroup="userSubmit"></asp:RequiredFieldValidator> 
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="drpTimeStatus"
                    ErrorMessage="Choose Type." ForeColor="Red" ValidationGroup="processSubmit"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td align="center">
                                                                    <div class="contenttitle2" style="margin: 0; padding: 0">
                                                                        <h3>
                                                                            <asp:Label ID="lblTitlePage" runat="server" Text="Label"></asp:Label></h3>
                                                                    </div>
                                                                </td>
                                                               
                                                                <td width="100">
                                                                    <asp:Button ID="btnPrevious" runat="server" Text="<<" Font-Bold="true"
                                                                        CssClass="btn grey-cascade" OnClick="btnPrevious_Click" />
                                                                    <asp:Button ID="btnNext" runat="server" Text=">>" Font-Bold="true"
                                                                        CssClass="btn grey-cascade" OnClick="btnNext_Click" />

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                     <asp:Panel ID="pnlMultiColoer" CssClass="" runat="server" Style="display: none; height: auto; overflow: auto">

                                                                         <section class="cat-box recent-box">

                                                                        <div class="cat-box-content" style="width: 250px;">


                                                                            <div style="text-align: left; background: #e26a70; align-items: center;">
                                                                                <div style="padding: 25px">

                                                                                    <img src="images/1251722468_44.gif" style="width:100%" />
                                                                                    <br />
                                                                                    <div>
                                                                                        <asp:Button ID="btnsave" runat="server"  Text="Scan" OnClick="btnThumRegister_Click" />
                                                                                        <%--<asp:Button ID="btnCustomerLogin" Visible="false" runat="server" CommandName="Save" CommandArgument='<%# Eval("COMPID") + "," + Eval("MYPRODID")+","+Eval("UOM") %>' Text="Save"></asp:Button>--%>
                                                                                        <asp:Button ID="btnCancelClogin" runat="server" Text="Close" />
                                                                                    </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                             </section>
                                                           </asp:Panel>
                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" DynamicServicePath="" BackgroundCssClass="modalBackground" CancelControlID="btnCancelClogin" Enabled="True" PopupControlID="pnlMultiColoer" TargetControlID="btnThumRegister"></cc1:ModalPopupExtender>
                                                    <div>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <table class="table table-striped table-bordered table-hover" id="sample_11">
                                                                    <thead>
                                                                        <tr>

                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhTenet" Text="Date"></asp:Label></th>
                                                                            
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhMyItems" Text="Time"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhCompaigntype" Text="Present?"></asp:Label></th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblhTypeID" Text="Total Time"></asp:Label></th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:ListView ID="Listview1" runat="server" OnItemDataBound="Listview1_ItemDataBound">
                                                                            <LayoutTemplate>
                                                                                <tr id="ItemPlaceholder" runat="server">
                                                                                </tr>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <tr>

                                                                                 
                                                                                    <td>
                                                                                        <asp:Label ID="lbldatdseTo" runat="server" Text='<%# Convert.ToDateTime(Eval("date")).ToShortDateString()%>'></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbldat4223eTodsd" runat="server" Text='<%# getDateData(Convert.ToDateTime(Eval("date")))%>'></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbld23eTodsd" runat="server" Text='<%# GetAbsantData(Convert.ToDateTime(Eval("date")))%>'></asp:Label></td>
                                                                                    <td>
                                                                                        <asp:Label ID="lbldated2Todsd" runat="server" Text='<%# GetTotalTime(Convert.ToDateTime(Eval("date")))%>'></asp:Label>
                                                                                        <asp:HiddenField ID="HiddenField1" Value='<%# GetTotalMinute(Convert.ToDateTime(Eval("date")))%>' runat="server" />
                                                                                        <asp:Label runat="server" ID="lblTotalTime" Visible="false" Font-Size="13px" Font-Bold="true" Text='<%# GetTotalTime(Convert.ToDateTime(Eval("date")))%>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                        <tfoot>
                                                                            <tr>
                                                                                <td colspan="3"></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblTotalTimeFinal" runat="server" Text="0.00"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </tfoot>
                                                                    </tbody>
                                                                </table>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
