<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iPatternManager.Models.BreadCrumbModel>" %>
<% int i = 1;
   foreach (SelectListItem item in Model.Items)
   { %>
<div style="float: left;">
    <a href="<%=item.Value%>">
        <%=item.Text%></a></div>
<%if (i < Model.Items.Count)
  { %>
<div style="float: left;">
    <img style="margin-left: 4px; margin-right: 4px;" src="/Content/Images/arrow-icon16.png" alt="" /></div>
<%} i++;
   }%>
<div class="clear">
</div>
