<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iPatternManager.Models.MetaDataValueListModel>" %>
<h2>
    Meta data værdier på beskednr.
    <%=Model.AnalysisResultID%></h2>
<%if (Model.MetaDataValues.Count == 0)
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
<% foreach (DTL.MetaDataValueDTO metaDataValue in Model.MetaDataValues)
   { %>
<% if (!oddLine) lineColor = "#e7e7e7"; else lineColor = "#ffffff"; oddLine = !oddLine; %>
<div class="table" style="background-color: <%=lineColor%>; padding-top: 4px; padding-bottom: 4px;">
    <div style="float: left; width: 120px; margin-left: 8px;">
        <%=metaDataValue.MetaDataKeyTitle%>&nbsp;</div>
    <div style="float: left; width: 300px;">
        <%=metaDataValue.MetaValue%></div>
</div>
<div style="clear: both;">
</div>
<% } %>
<% } %>
