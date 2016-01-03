<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iPatternManager.Models.WordIndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    iPattern - Ordhåndtering
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Ordhåndtering</h2>
    <!-- Search -->
    <%using (Ajax.BeginForm("Search", new AjaxOptions { UpdateTargetId = "listDiv" }))
      { %>
    <div style="font-weight: bold; float: left; width: 50px;">
        Søg:</div>
    <div style="float: left;">
        <input id="searchText" name="searchText" type="text" onkeyup="btnSearch.click()" />
    </div>
    <div style="float: left;">
        <input id="showIgnoredWords" name="showIgnoredWords" type="checkbox" onclick="btnSearch.click()" value="true" />
        Vis kun ignorerede ord
    </div>
    <input id="btnSearch" class="hidden" type="submit" value="Søg" />
    <%}%>
    <div style="clear: both;">
    </div>
    <!-- End -->
    <div style="background-color: #c7c7c7; color: #000000; margin-top: 12px; padding-top: 4px;
        padding-bottom: 4px;">
        &nbsp;
        <div style="float: left; width: 200px; font-weight: bold; margin-left: 8px;">
            Ord</div>
        <div style="float: left; width: 100px; font-weight: bold;">
            Antal ord</div>
    </div>
    <div style="clear: both;">
    </div>
    <!-- Word List -->
    <div id="listDiv">
        <%Html.RenderPartial("WordListControl");%></div>
    <!-- End Word List -->
</asp:Content>
