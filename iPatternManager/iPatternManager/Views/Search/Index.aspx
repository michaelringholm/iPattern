<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iPatternManager.Models.SearchIndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   Søgning
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <h2>
      Søgning</h2>
   <div style="font-family: Arial, Sans-Serif; font-size: 11pt; color: #000000;">
      <div style="height: 24px; vertical-align: middle; line-height: 24px;">
         <div style="float: left;">
            <%=Html.TextBox("tbGlobalSearch", null, new { style = "width: 500px; height: 24px;" } )%>
         </div>
         <div style="float: left;">
            <div style="height: 24px; background-color: #F1F1F1; padding-left: 3px; padding-right: 3px; border: solid 2px #E2E2E2; color: #000000;">
               Søg</div>
         </div>
      </div>
      <div style="clear: both;">
      </div>
      <div id="searchResultArea">
         <%Html.RenderPartial("SearchListControl", Model);%>
      </div>
   </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BreadCrumb" runat="server">
</asp:Content>
