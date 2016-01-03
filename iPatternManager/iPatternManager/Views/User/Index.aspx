<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iPatternManager.Models.UserIndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Brugeradministration
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BreadCrumb" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Brugeradministration</h2>
    <div id="userListDiv">
        <%Html.RenderPartial("UserListControl");%>
    </div>   
    <%= Html.ActionLink("Registrér", "Register", "Account") %> ny bruger.
</asp:Content>
