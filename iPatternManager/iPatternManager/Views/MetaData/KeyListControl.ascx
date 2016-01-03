<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iPatternManager.Models.MetaDataKeyListModel>" %>
<% bool oddLine = true;
   String lineColor = "#ffffff";
   int i = 0;
   String rowTopMargin = "0px";%>
<% foreach (DTL.MetaDataKeyDTO metaDataKey in Model.MetaDataKeys)
   { %>
<% if (!oddLine) lineColor = "#e7e7e7"; else lineColor = "#ffffff"; oddLine = !oddLine; %>
<% if (Model.MetaDataKeys.Count == (i + 1)) rowTopMargin = "20px"; %>
<input type="hidden" name='<%= "MetaDataKeyList[" + i + "].ID" %>' value='<%=metaDataKey.ID%>' />
<input type="hidden" name='<%= "MetaDataKeyList[" + i + "].InformationTypeID" %>'
    value='<%=metaDataKey.InformationTypeID%>' />
<div class="table" style="background-color: <%=lineColor%>; padding-top: 4px; padding-bottom: 4px;
    padding-left: 4px; margin-top: <%=rowTopMargin%>">
    <div style="float: left; width: 200px;">
        <input style="width: 160px" type="text" name='<%= "MetaDataKeyList[" + i + "].Title" %>'
            value="<%=metaDataKey.Title%>" />
    </div>
    <div style="float: left; width: 400px;">
        <input style="width: 360px;" type="text" name='<%= "MetaDataKeyList[" + i + "].RegEx" %>'
            value="<%=metaDataKey.RegEx%>" />
    </div>
    <%if (metaDataKey.ID.HasValue)
      { %>
    <div style="float: left; width: 80px;">
        <a href="/MetaData/DeleteKey/<%=metaDataKey.ID%>">Slet</a></div>
    <%}
      else
      { %>
    <div style="float: left; width: 80px; color: Green;">
        Ny!</div>
    <%} %>
</div>
<div style="clear: both;">
</div>
<% i++; %>
<% } %>