<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DTL.InformationTypeDTO>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    iPattern - Ny beskedmappe
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%if (Model.ParentID == null)
      { %>
    <h2>
        Ny beskedmappe</h2>
    <%}
      else
      { %>
    <h2>
        Ny undermappe til &quot;<%=DAL.InformationTypeDAC.GetInformationType(Model.ParentID.Value, iPatternManager.ContextManager.Current.SelectedAreaID.Value).Title%>&quot;</h2>
    <%} %>
    <%Html.BeginForm("SaveNew", "Message"); %>
    <%if (Model != null)
      { %>
    <input type="hidden" id="ParentID" name="ParentID"
        value="<%=Model.ParentID%>" />
    <%} %>
    <div class="fieldLabel" style="float: left; width: 160px;">
        Mappenavn:</div>
    <div style="float: left; width: 190px;">
        <%=Html.TextBoxFor(m => m.Title, new { style = "width: 180px;" })%>
    </div>
    <div style="float: left; margin-left: 5px;">
        <%= Html.ValidationMessageFor(m => m.Title) %></div>
    <div style="clear: both; padding: 6px;">
    </div>
    <div class="fieldLabel" style="float: left; width: 160px;">
        Grænseværdi (mulig):</div>
    <div style="float: left;">
        <%=Html.TextBoxFor(m => m.PossibleLimit, new { style="text-align: right; width: 40px;", disabled="disabled" } ) %>
    </div>
    <div style="clear: both; padding: 6px;">
    </div>
    <div class="fieldLabel" style="float: left; width: 160px;">
        Grænseværdi (sikker):</div>
    <div style="float: left;">
        <%=Html.TextBoxFor(m => m.CertainLimit, new { style="text-align: right; width: 40px;"} ) %>
    </div>
    <div style="float: left; margin-left: 5px;">
        <%= Html.ValidationMessageFor(m => m.CertainLimit) %></div>
    <div style="clear: both;">
    </div>
    <div>
        <input style="margin-top: 10px;" type="submit" value="Gem" />
    </div>
    <%Html.EndForm(); %>
</asp:Content>
