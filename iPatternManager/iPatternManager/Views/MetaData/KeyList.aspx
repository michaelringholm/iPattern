<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iPatternManager.Models.MetaDataKeyListModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Meta data nøgler
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Meta data nøgler for beskedmappen &quot;<%=Model.InformationType.Title%>&quot;</h2>
    <div class="table" style="background-color: #c7c7c7; color: #000000; margin-top: 12px;
        padding-top: 4px; padding-bottom: 4px; padding-left: 4px; font-weight: bold;">
        <div style="float: left; width: 200px;">
            Navn</div>
        <div style="float: left; width: 400px;">
            Regulært udtryk</div>
    </div>
    <div style="clear: both;">
    </div>
    <%using (Ajax.BeginForm("SaveKeyList", new AjaxOptions { UpdateTargetId = "keyListDiv" }))
      { %>
    <input type="hidden" name="informationTypeID" value="<%=Model.InformationType.ID%>" />
    <div id="keyListDiv">
        <%Html.RenderPartial("KeyListControl");%></div>
    <div style="margin-left: 3px; margin-top: 12px;">
        <input id="btnSave" type="submit" value="Gem" />
    </div>
    <%} %>
</asp:Content>
