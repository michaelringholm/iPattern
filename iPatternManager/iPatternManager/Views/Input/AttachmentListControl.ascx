<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iPatternManager.Models.InputDetailsModel>" %>
<h2>
    Vedhæftninger</h2>
<%if (Model.Attachments.Count == 0)
  { %>
<div style="font-style: italic;">
    Ingen vedhæftninger fundet.</div>
<%}
  else
  {%>
<div class="table" style="background-color: #c7c7c7; color: #000000; margin-top: 12px;
    padding-top: 4px; padding-bottom: 4px; font-weight: bold; text-indent: 8px;">
    <div style="float: left; width: 35px;">
        &nbsp;</div>
    <div style="float: left; width: 120px;">
        Navn</div>
</div>
<% bool oddLine = true; String lineColor = "#ffffff"; %>
<% foreach (DTL.AttachmentDTO attachment in Model.Attachments)
   { %>
<% if (!oddLine) lineColor = "#e7e7e7"; else lineColor = "#ffffff"; oddLine = !oddLine; %>
<div class="table" style="background-color: <%=lineColor%>; padding-top: 4px; padding-bottom: 4px; text-indent: 8px;">
    <div style="float: left; width: 35px;">
    <a href="/Input/ShowAttachment/<%=attachment.ID.Value%>"><img src="/Content/Images/attachment-icon16.png" alt="<%=attachment.Title%>" title="<%=attachment.Title%>" /></a>
    </div>
    <div style="float: left; width: 120px;">
        <%=attachment.Title%>&nbsp;</div>
</div>
<div style="clear: both;">
</div>
<% } %>
<% } %>
