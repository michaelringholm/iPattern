<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iPatternManager.Models.MessageIndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    iPattern - Beskedmapper
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Beskedmapper</h2>
    <div class="table" style="background-color: #c7c7c7; color: #000000; margin-top: 12px;
        padding-top: 4px; padding-bottom: 4px;">
        <div style="float: left; width: 200px; font-weight: bold; margin-left: 4px;">
            Mappenavn</div>
    </div>
    <div style="clear: both;">
    </div>
    <% bool oddLine = true;
       String lineColor = "#ffffff"; %>
    <div class="table" style="background-color: <%=lineColor%>; padding-top: 4px; padding-bottom: 4px; text-indent: 4px;">
        <div style="float: left; width: 16px;">
            <img src="/Content/Images/bullet-black-icon16.png" alt="" />
        </div>
        <div style="float: left; width: 200px;">
            <a href="/Input/Index/">Alle (Modtagede)</a></div>
        <div style="float: left; width: 50px;">
            <a href="/Input/Index/">
                <img src="/Content/Images/Folder-icon16.png" alt="Settings" /></a></div>
    </div>
    <div style="clear: both;">
    </div>
    <!-- Render sub elements, consider moving to user control -->
    <div id="informationTypeListDiv">
        <%Html.RenderAction("RenderListControl", "Message", new { parentID = -1, level = 1, oddLine = oddLine });%>
    </div>
    <!-- End render sub elements, consider moving to user control -->
    <div style="margin-top: 24px;">
        <a href="/Message/New">Opret ny beskedmappe</a></div>
</asp:Content>
