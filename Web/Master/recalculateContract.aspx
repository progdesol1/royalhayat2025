<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AcmMaster.Master" AutoEventWireup="true" CodeBehind="recalculateContract.aspx.cs" Inherits="Web.Master.recalculateContract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:Button ID="Button1" runat="server" class="btn btn-sm green" Text="Recalculate all Transection" OnClientClick="showProgress()" OnClick="Button1_Click" />
            <asp:Button ID="btnStartagain" Visible="false" runat="server" class="btn btn-sm yellow" Text="Unsuccess Recalculate" OnClientClick="showProgress()" OnClick="btnStartagain_Click" />

            <asp:Panel ID="pnlSuccessMsg" runat="server" Visible="false">
                <div class="alert alert-success alert-dismissable">
                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button"></button>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </asp:Panel>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnStartagain" />
            <asp:PostBackTrigger ControlID="Button1" />

        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel16">
        <ProgressTemplate>
            <div class="overlay">
                <div style="z-index: 1000; margin-left: 350px; margin-top: 200px; opacity: 1; -moz-opacity: 1;">
                    <img src="../assets/admin/layout4/img/loading-spinner-blue.gif" />
                    &nbsp;please wait...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>
