<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iPatternManager.Models.WordIndexModel>" %>
<% bool oddLine = true;
   String lineColor = "#ffffff"; %>
<% foreach (DTL.WordHeaderDTO wordHeader in Model.WordHeaders)
   { %>
<% if (!oddLine) lineColor = "#e7e7e7"; else lineColor = "#ffffff"; oddLine = !oddLine; %>
<div class="table" style="background-color: <%=lineColor%>; padding-top: 4px; padding-bottom: 4px;">
    <%String word; if (String.IsNullOrEmpty(wordHeader.Word.Trim())) word = "&nbsp;"; else word = System.Web.HttpUtility.HtmlEncode(wordHeader.Word); %>
    <div style="float: left; width: 200px; margin-left: 8px;">
        <%=word%>&nbsp;</div>
    <div style="float: left; width: 100px;">
        <%=wordHeader.WordCount %></div>
    <%if (Model.ShowIgnoredWords)
      { %>
    <div style="float: left; width: 200px;">
        <a href="/Word/UnignoreWord/<%=System.Web.HttpUtility.UrlEncode(wordHeader.Word)%>">Afignorer ord</a></div>
    <%}
      else
      {%>
    <div style="float: left; width: 200px;">
        <a href="/Word/IgnoreWord/<%=System.Web.HttpUtility.UrlEncode(wordHeader.Word)%>">Ignorer ord</a></div>
    <%} %>
</div>
<div style="clear: both;">
</div>
<% } %>