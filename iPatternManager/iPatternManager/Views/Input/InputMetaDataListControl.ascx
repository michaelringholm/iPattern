<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iPatternManager.Models.InputDetailsModel>" %>
<h2>
    Meta data værdier på input beskednr.
    <%=Model.InputMessage.ID%></h2>
<%if (Model.InputMetaDataList.Count == 0)
  { %>
<div style="font-style: italic;">
    Ingen metadata fundet.</div>
<%}
  else
  {%>
<div class="table" style="background-color: #c7c7c7; color: #000000; margin-top: 12px;
    padding-top: 4px; padding-bottom: 4px; font-weight: bold;">
    <div style="float: left; width: 120px; margin-left: 8px;">
        Meta data type</div>
    <div style="float: left; width: 300px;">
        Værdi</div>
</div>
<% bool oddLine = true; String lineColor = "#ffffff"; %>
<% foreach (DTL.InputMetaDataDTO metaData in Model.InputMetaDataList)
   { %>
<% if (!oddLine) lineColor = "#e7e7e7"; else lineColor = "#ffffff"; oddLine = !oddLine; %>
<div class="table" style="background-color: <%=lineColor%>; padding-top: 4px; padding-bottom: 4px;">
    <div style="float: left; width: 120px; margin-left: 8px;">
        <%=metaData.Title%>&nbsp;</div>
    <div style="float: left; width: 300px;">
        <%=metaData.MetaValue%></div>
</div>
<div style="clear: both;">
</div>
<% } %>
<% } %>