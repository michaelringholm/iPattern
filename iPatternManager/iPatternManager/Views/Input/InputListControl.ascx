<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iPatternManager.Models.InputIndexModel>" %>
<% bool oddLine = true;
   String lineColor = "#ffffff";
   int itemCounter = 0; %>
<% foreach (DTL.InputDTO inputMessage in Model.InputMessages)
   { %>
<% if (!oddLine) lineColor = "#e7e7e7"; else lineColor = "#ffffff"; oddLine = !oddLine; %>
<div style="background-color: <%=lineColor%>; padding-top: 4px; padding-bottom: 4px;
    text-indent: 10px;">
    &nbsp;
    <%if (inputMessage.AnalysisResultID.HasValue)
      { %>
    <div style="float: left; width: 30px; text-align: right;">
        <input type="hidden" name='<%= "CheckedList[" + itemCounter + "].Text" %>' value='<%=inputMessage.AnalysisResultID%>' />
        <input type="checkbox" value='<%=inputMessage.AnalysisResultID%>' name='<%= "CheckedList[" + itemCounter + "].Value" %>' />
    </div>
    <div style="float: left; width: 90px; text-align: right;">
        <a style="margin-right: 40px;" href="/Result/Details/<%=inputMessage.AnalysisResultID%>">
            <%=inputMessage.AnalysisResultID%></a></div>
    <div style="float: left; width: 50px;">
        <%if (inputMessage.IsRead.HasValue && inputMessage.IsRead.Value)
          {%>
        Ja
        <%}
          else
          {%>Nej<%} %></div>
    <%} %>
    <div style="float: left; width: 380px;">
        <%=inputMessage.TextInputSummary%>&nbsp;</div>
    <div style="float: left; width: 100px; text-align: right;">
        <a style="margin-right: 25px;" href="/Input/Details/<%=inputMessage.ID.Value%>">
            <%=inputMessage.ID%></a></div>
    <div style="float: left; width: 200px;">
        <%=inputMessage.EventTime%></div>
    <%if (inputMessage.AnalysisResultID.HasValue)
      { %>
    <div style="float: left; width: 25px;">
        <a href="/Input/Rerun/<%=inputMessage.AnalysisResultID%>" title="Genkør analyse">
            <img style="border: none;" src="/Content/Images/gear-icon16.png" alt="Rerun Analysis" /></a></div>
    <% }
      else
      { %>
    <div style="float: left; width: 25px;">
        <a href="/Input/RerunByAnalysisInputID/<%=inputMessage.ID%>" title="Genkør analyse">
            <img style="border: none;" src="/Content/Images/gear-icon16.png" alt="Rerun Analysis" /></a></div>
    <%} %>
    <div style="float: left; width: 100px;">
        <%if (inputMessage.Status == "ANALYZED")
          {%>Analyseret<%}
          else
          { %>Ikke analyseret<%} %></div>
</div>
<div style="clear: both;">
</div>
<% itemCounter++;
   } %>
<% if (Model.InputMessages.Count == 0)
   { %>
<div style="margin-top: 20px; font-style: italic;">
    Ingen beskeder modtaget.</div>
<% } %>
<div style="padding-bottom: 20px;">
    &nbsp;</div>
