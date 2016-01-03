<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iPatternManager.Models.AreaIndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    iPattern - Områder
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Områder</h2>
    <div style="float: left; font-weight: bold; width: 80px;">
        Firma:
    </div>
    <div style="float: left;">
          <%= Html.Encode(Model.CompanyTitle)%>
    </div>
    <div class="clear">
    </div>
    <div id="areaListDiv">
        <%Html.RenderPartial("IndexControl");%>
    </div>    
</asp:Content>
