<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/CRMMaster.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="Web.CRM.DashBoard" %>

<%@ Register Src="~/CRM/UserControl/RightPanelUC.ascx" TagPrefix="uc1" TagName="RightPanelUC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="b" runat="server">
        <div class="col-md-12">
            <div class="tabbable-custom tabbable-noborder">
              
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
                                <i class="fa fa-user"></i> Campaign List
                            </div>
                            <div class="actions">
                                <a href="Campaign_Mst.aspx?View=Add" class="btn btn-success uppercase">
                                    <i class="fa fa-plus"></i>&nbsp;&nbsp;Add </a>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="table-scrollable">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <table class="table table-striped table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Name
                                                    </th>
                                                    <th>Actual Cost
                                                    </th>
                                                    <th>Start Date
                                                    </th>

                                                    <th>End Date
                                                    </th>
                                                    <th style="width: 250px;">Action
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:ListView ID="ListViewCampaign" runat="server" OnItemCommand="ListViewCampaign_ItemCommand">
                                                    <LayoutTemplate>
                                                        <tr id="ItemPlaceholder" runat="server">
                                                        </tr>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTenet" runat="server" Text='<%# Eval("Name")%>'></asp:Label></td>

                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ActualCost")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Convert.ToDateTime(Eval("StartDate")).ToShortDateString()%>'></asp:Label></td>

                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Convert.ToDateTime(Eval("EndDate")).ToShortDateString()%>'></asp:Label></td>

                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "Campaign_Mst.aspx?ID="+ Eval("ID")%>' class="btn btn-sm blue filter-submit margin-bottom" runat="server"><i class="fa fa-pencil"></i>Edit</asp:LinkButton>
                                                                            <asp:LinkButton ID="btnOpportunity" CommandName="btnOpportunity" CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm blue filter-submit margin-bottom">Show Opportunity&nbsp;<span class="badge badge-default" Style="background-color: #f3565d;color: #fff;" ><%# GetOppCount(Convert.ToInt32(Eval("ID").ToString()))%> </span></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <!-- END EXAMPLE TABLE PORTLET-->
                </div>

                <asp:Panel ID="pnloppertunity" runat="server" Visible="false">

                    <div class="col-md-7 col-sm-6">
                        <!-- BEGIN EXAMPLE TABLE PORTLET-->
                        <div class="portlet box purple">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-user"></i>Opportunity List base on Campaign 
                                    <asp:Label ID="lblcampinname" runat="server"></asp:Label>
                                </div>
                                <div class="actions">
                                    <a href="Opportunity_Mst.aspx?View=Add" class="btn btn-success uppercase">
                                        <i class="fa fa-plus"></i>&nbsp;&nbsp;Add </a>

                                </div>
                            </div>
                            <div class="portlet-body">

                                <div class="table-scrollable">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" id="sample_1_wrapper">
                                                <thead>
                                                    <tr>

                                                        <th>Opp Name
                                                        </th>
                                                        <th>Cam Name
                                                        </th>
                                                        <th>Customer
                                                        </th>
                                                        <%--<th>Status
                                                        </th>--%>

                                                        <th style="width: 93px;">ACTION</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="lstOpportunity" runat="server" OnItemCommand="lstOpportunity_ItemCommand" OnItemDataBound="lstOpportunity_ItemDataBound">
                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>

                                                                <td>
                                                                    <asp:Label ID="lblTenet" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                                      <asp:Label ID="lblcustomername" Visible="false" runat="server" Text='<%# Eval("Customer_Name")%>'></asp:Label>
                                                                    <asp:Label ID="lbloppp" Visible="false" runat="server" Text='<%# Eval("OpportID")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# getCampid(Convert.ToInt32(Eval("Campaign_ID")))%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Customer_Name")%>'></asp:Label></td>
                                                                <%--<td>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Status")%>'></asp:Label></td>--%>


                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <%--  <asp:LinkButton ID="LinkButton1" PostBackUrl='<%# "Campaign_Mst.aspx?ID="+ Eval("ID")%>' class="btn btn-sm blue filter-submit margin-bottom" runat="server"><i class="fa fa-pencil"></i>Edit</asp:LinkButton>
                                                                                <asp:LinkButton ID="btnOpportunity" CommandName="btnOpportunity" CommandArgument='<%# Eval("ID")%>' runat="server" class="btn btn-sm blue filter-submit margin-bottom">Show Opportunity</asp:LinkButton>
                                                                                <span class="badge badge-default" style="margin-left: 0px; margin-top: 2px; background-color: #f3565d"><%# GetOppCount(Convert.ToInt32(Eval("ID").ToString()))%> </span></td>--%>


                                                                                <asp:LinkButton ID="LinkButton2" style="margin-bottom: 10px;" PostBackUrl='<%# "Opportunity_Mst.aspx?ID="+ Eval("OpportID")%>' class="btn btn-sm blue filter-submit margin-bottom" runat="server"><i class="fa fa-pencil"></i>&nbsp;Edit</asp:LinkButton>

                                                                                <asp:LinkButton ID="btnLead" CommandName="btnLead" CommandArgument='<%# Eval("OpportID")%>' runat="server" class="btn btn-sm blue filter-submit margin-bottom">Show Lead &nbsp;<span class="badge badge-default" style=" background-color: #f3565d;color: #fff;"> <asp:Label ID="lblcountlead" runat="server"></asp:Label>  </span></asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <!-- END EXAMPLE TABLE PORTLET-->
                        <asp:Panel ID="pnlLead" runat="server" Visible="false">
                        <div class="portlet box yellow">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-user"></i>Lead List base on Opportunity
                                    <asp:Label ID="lblOpportname" runat="server"></asp:Label>
                                </div>
                                <div class="actions">
                                    <a href="CRM_tbl_Lead_Mst.aspx?View=Add" class="btn btn-success uppercase">
                                        <i class="fa fa-plus"></i>&nbsp;&nbsp;Add </a>

                                </div>
                            </div>
                            <div class="portlet-body">

                                <div class="table-scrollable">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="sample_1">
                                                <thead>
                                                    <tr>


                                                        <th>Lead Name
                                                        </th>
                                                        
                                                        <th>Customer
                                                        </th>
                                                        <th style="width: 133px;">Status
                                                        </th>
                                                        <th>ACTION</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:ListView ID="lstLead" runat="server">
                                                        <LayoutTemplate>
                                                            <tr id="ItemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>

                                                                <td>
                                                                    <asp:Label ID="lblTenet" runat="server" Text='<%# Eval("LeadName1")%>'></asp:Label>
                                                                  
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Customer_Name")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# GetStatusName(Convert.ToInt32(Eval("Status")))%>'></asp:Label></td>

                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton2" PostBackUrl='<%# "CRM_tbl_Lead_Mst.aspx?ID="+ Eval("ID")%>' class="btn btn-sm blue filter-submit margin-bottom" runat="server"><i class="fa fa-pencil"></i>Edit</asp:LinkButton>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                             </asp:Panel>
                    </div>

                </asp:Panel>
                
                   
                        <!-- BEGIN EXAMPLE TABLE PORTLET-->
                        
                        <!-- END EXAMPLE TABLE PORTLET-->
                   
               
                <!-- END PAGE CONTENT-->
               <%-- <asp:Panel ID="Panel1" runat="server">
                    <div class="col-md-5 col-sm-12">
                        <div id="divtools" runat="server"></div>--%>
                       <%-- <uc1:RightPanelUC runat="server" ID="RightPanelUC" />--%>
                        <%--<asp:Panel ID="Panel123" runat="server">--%>
                    <%-- <div class="row">--%>
                    
                        <%--<a name="Support"></a>
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption caption-md">
                                    <i class="icon-bar-chart theme-font-color hide"></i>
                                    <span class="caption-subject theme-font-color bold uppercase">Customer Support</span>
                                    <a id="ContentPlaceHolder1_pnlresponsive" class="btn blue" href="javascript:__doPostBack('ctl00$ContentPlaceHolder1$pnlresponsive','')">Reply</a>
                                    <a id="ContentPlaceHolder1_pnlresponsive1" class="btn blue" href="javascript:__doPostBack('ctl00$ContentPlaceHolder1$pnlresponsive1','')">Open New Ticket</a>
                                    <a href="TikitView.aspx">
                                        <span class="badge badge-success">
                                            <span id="ContentPlaceHolder1_lblAttachment">5</span>
                                        </span>
                                    </a>
                                </div>
                                <div class="inputs">
                                    <div class="portlet-input input-inline input-small ">
                                        <div class="input-icon right">
                                            <i class="icon-magnifier"></i>
                                            <input class="form-control form-control-solid" type="text" placeholder="search...">
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <%--<div class="portlet-body">
                                <div class="slimScrollDiv" style="position: relative; overflow: hidden; width: auto; height: 305px;">
                                    <div class="scroller" data-handle-color="#D7DCE2" data-rail-visible1="0" data-always-visible="1" style="height: 305px; overflow: hidden; width: auto;" data-initialized="1">
                                        <div class="general-item-list">
                                            <div class="item">
                                                <div class="item-head">
                                                    <div class="item-details">
                                                        <a class="item-name primary-link" href="">Ayo</a>
                                                        <span class="item-label">27/09/2015 11:41:58</span>
                                                    </div>
                                                    <span class="item-status">
                                                        <span class="badge badge-empty badge-success"></span>
                                                        Pending
                                                    </span>
                                                </div>
                                                <div class="item-body">ok </div>
                                            </div>
                                            <div class="item">
                                                <div class="item-head">
                                                    <div class="item-details">

                                                        <a class="item-name primary-link" href="">maria</a>
                                                        <span class="item-label">19/09/2015 09:58:36</span>
                                                    </div>
                                                    <span class="item-status"><span class="badge badge-empty badge-success"></span>Pending</span>
                                                </div>
                                                <div class="item-body">--%>
                                                  <%--  kindly pls delete ref no. 210
                                               
                                                </div>
                                            </div>
                                            <div class="item">
                                                <div class="item-head">
                                                    <div class="item-details">

                                                        <a class="item-name primary-link" href="">maria</a>
                                                        <span class="item-label">19/09/2015 09:58:30</span>
                                                    </div>
                                                    <span class="item-status"><span class="badge badge-empty badge-success"></span>Pending</span>
                                                </div>
                                                <div class="item-body">
                                                    kindly pls delete ref no. 210
                                               
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                   <%-- <div class="slimScrollBar" style="background: rgb(215, 220, 226) none repeat scroll 0% 0%; width: 7px; position: absolute; top: 0px; opacity: 0.4; display: none; border-radius: 7px; z-index: 99; right: 1px; height: 305px;"></div>
                                    <div class="slimScrollRail" style="width: 7px; height: 100%; position: absolute; top: 0px; display: none; border-radius: 7px; background: rgb(234, 234, 234) none repeat scroll 0% 0%; opacity: 0.2; z-index: 90; right: 1px;"></div>
                                </div>
                            </div>
                        </div>
                    
                </asp:Panel>
                    </div>
                    <a name="MemberActivity"></a>
                </asp:Panel>--%>
                <%--<asp:Panel ID="Panel2" runat="server">
                    <div class="col-md-6 col-sm-12">
                <iframe src="appointment.aspx" runat="server" id="ifrm" name="ifrm" frameborder="0" clientidmode="Static" width="600px" height="500px" style="overflow: hidden"></iframe>
                       </div>
                    </asp:Panel>--%>
              

            </div>
        </div>
    </div>	
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
