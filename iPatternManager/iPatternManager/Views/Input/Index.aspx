<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iPatternManager.Models.InputIndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    iPattern - Beskeder
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BreadCrumb" runat="server">
    <%    List<SelectListItem> items = new List<SelectListItem>();
          items.Add(new SelectListItem { Text = "Beskedmapper", Value = "/Message/Index" });

          if (Model.InformationType == null)
              items.Add(new SelectListItem { Text = "Alle", Value = "/Input/Index/" });
          else
          {
              int id = Model.InformationType.ID.Value;
              DTL.InformationTypeDTO parentInformationType = DAL.InformationTypeDAC.GetParentInformationType(id, iPatternManager.ContextManager.Current.SelectedAreaID.Value);
              while (parentInformationType != null)
              {
                  items.Insert(1, new SelectListItem { Text = parentInformationType.Title, Value = "/Input/Index/" + parentInformationType.ID.Value });
                  id = parentInformationType.ID.Value;
                  parentInformationType = DAL.InformationTypeDAC.GetParentInformationType(id, iPatternManager.ContextManager.Current.SelectedAreaID.Value);

              }
             
              items.Add(new SelectListItem { Text = Model.InformationType.Title, Value = "/Input/Index/" + Model.InformationType.ID.Value });
          }

          Html.RenderPartial("BreadCrumbControl", new iPatternManager.Models.BreadCrumbModel { Items = items });  %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Ajax.BeginForm("Search", "Input", new AjaxOptions { UpdateTargetId = "mybody" }, new { id = "myform" }))
      { %>
    <%if (false)
      { %>
    <div style="height: 24px; width: 300px; padding: 4px; border: solid 1px #c3c3c3;">
        <div>
            <a onclick="return MoveMessage(this)">
                <img src="/Content/Images/page-swap-icon24.png" alt="Flyt besked" title="Flyt besked" /></a></div>
    </div>
    <div style="clear: both;">
    </div>
    <%} %>
    <%if (Model.InformationType != null)
      { %>
    <h2>
        Beskeder i mappen &quot;<%=Model.InformationType.Title%>&quot;</h2>
    <%}
      else
      {%>
    <h2>
        Beskeder i alle mapper</h2>
    <%} %>
    <!-- Search -->
    <div style="font-weight: bold; float: left; width: 50px;">
        Søg:</div>
    <div style="float: left; margin-left: 8px;">
        <input id="searchText" name="searchText" style="float: left;" type="text" onkeyup="btnSearch.click()" />
    </div>
    <div style="float: right; margin-left: 8px;">
        <input id="msgAmount" style="width: 30px;" value="50" name="msgAmount" type="text" onkeyup="btnSearch.click()" /></div>
    <div style="font-weight: bold; float: right;">
        Antal beskeder:</div>
    <%if (Model.InformationType != null)
      { %>
    <input type="hidden" name="informationTypeID" value="<%=Model.InformationType.ID%>" />
    <%}%>
    <input id="btnSearch" class="hidden" type="submit" value="Søg" />
    <div style="clear: both;">
    </div>
    <%} %>
    <!-- End Search -->
    <div style="margin-top: 10px;">
        <%if (Model.InformationType != null)
          { %>
        <%=Html.ActionLink("Reananalyser alle", "ReanalyzeAllItems", new { informationTypeID = Model.InformationType.ID })%>
        <%}
          else
          { %>
        <%=Html.ActionLink("Reananalyser alle", "ReanalyzeAllItems", new { informationTypeID = "" })%>
        <%} %>
    </div>
    <div class="table" style="background-color: #c7c7c7; color: #000000; margin-top: 12px;
        padding-top: 4px; padding-bottom: 4px; text-indent: 10px;">
        <%if (Model.InformationType != null)
          { %>
        <div style="float: left; width: 120px; font-weight: bold;">
            <label>
                Besked ID</label></div>
        <div style="float: left; width: 50px; font-weight: bold;">
            Læst</div>
        <%} %>
        <div style="float: left; width: 380px; font-weight: bold;">
            Sammendrag</div>
        <div style="float: left; width: 100px; font-weight: bold;">
            <label style="margin-left: 10px;">
                Input ID</label></div>
        <div style="float: left; width: 200px; font-weight: bold;">
            Sidst ændret</div>
        <div style="float: left; width: 160px; font-weight: bold;">
            Analyse Status</div>
    </div>
    <div style="clear: both;">
    </div>
    <!-- Input List -->
    <div id="mybody">
        <form id="inputListForm" action="/Input/Move">
        <%if (Model.InformationType != null)
          {%>
        <input type="hidden" name="informationTypeID" value="<%=Model.InformationType.ID%>" />
        <%} %>
        <%Html.RenderPartial("InputListControl");%>
        </form>
    </div>
    <!-- End Input List -->
</asp:Content>
