<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iPatternManager.Models.MessageDetailsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    iPattern - Detaljer for beskedmappen
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Detaljer for beskedmappen &quot;<%=Model.InformationType.Title %>&quot;</h2>
    <%Html.BeginForm("SaveDetails", "Message"); %>
    <input type="hidden" id="ParentID" name="ParentID" value="<%=Model.InformationType.ParentID%>" />
    <!-- Put in control -->
    <div class="fieldLabel" style="float: left; width: 160px;">
        Mappenavn:</div>
    <div style="float: left; width: 200px;">
        <input type="text" id="Title" name="Title" value="<%=Model.InformationType.Title%>" /></div>
    <div style="clear: both; padding: 6px;">
    </div>
    <div class="fieldLabel" style="float: left; width: 160px;">
        Grænseværdi (mulig):</div>
    <div style="float: left;">
        <input disabled="disabled" type="text" id="PossibleLimit" name="PossibleLimit" value="<%=Model.InformationType.PossibleLimit%>"
            style="text-align: right; width: 40px;" /></div>
    <div style="clear: both; padding: 6px;">
    </div>
    <div class="fieldLabel" style="float: left; width: 160px;">
        Grænseværdi (sikker):</div>
    <div style="float: left;">
        <input type="text" id="CertainLimit" name="CertainLimit" value="<%=Model.InformationType.CertainLimit%>"
            style="text-align: right; width: 40px;" /></div>
    <div style="clear: both;">
    </div>
    <!-- End control -->
    <div class="subHeading">Manuelt oprettede ord</div>
    <div id="manualWeightedWords">
        <%Model.CurrentList = iPatternManager.Models.CurrentListEnum.Manual;%>
        <%Html.RenderPartial("WeightListControl");%>
    </div>        
    <div class="subHeading">Automatisk oprettede ord</div>
    <div id="automaticallyWeightedWords">
        <%Model.CurrentList = iPatternManager.Models.CurrentListEnum.Automatic;%>
        <%Html.RenderPartial("WeightListControl");%>
    </div>        
    <div class="subHeading">Blokerede ord</div>
    <div id="Div2">
        <%Model.CurrentList = iPatternManager.Models.CurrentListEnum.Blocked;%>
        <%Html.RenderPartial("WeightListControl");%>
    </div>        
    <input style="margin-top: 10px;" type="submit" value="Gem" />
    <%Html.EndForm(); %>
</asp:Content>
