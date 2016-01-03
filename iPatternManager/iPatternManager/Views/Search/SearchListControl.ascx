<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iPatternManager.Models.SearchIndexModel>" %>
<%foreach (var result in Model.SearchResults)
  { %>
<div style="margin-top: 20px;">
   <div style="color: #2200c1; font-size: medium; font-weight: bold; text-decoration: underline;">
      <%=result.Title%></div>
   <div style="color: #0e774a; font-size: small;">
      <%=result.Location%></div>
   <div style="padding-top: 4px; padding-bottom: 4px;">
      <%=result.Summary%></div>
   <div style="font-size: smaller;">
      <ul style="margin: 0px; padding: 0px; list-style: none; font-style: italic;">
         <li>- Speditionsnr: 154200</li>
         <li>- Modtaget: 12.03.2011 10:23:12</li>
         <li>- Fra: salg@sadolin.dk</li>
      </ul>
   </div>
</div>
<%} %>