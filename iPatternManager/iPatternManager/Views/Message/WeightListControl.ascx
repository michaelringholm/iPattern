<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iPatternManager.Models.MessageDetailsModel>" %>
<% bool oddLine = true;
   String lineColor = "#ffffff";
   String rowTopMargin = "0px"; %>
<div class="table" style="background-color: #c7c7c7; color: #000000; margin-top: 12px;
    padding-top: 4px; padding-bottom: 4px; padding-left: 4px; font-weight: bold;">
    <div style="float: left; width: 200px;">
        Ord</div>
    <div style="float: left; width: 200px;">
        Vægt</div>
</div>
<div style="clear: both;">
</div>
<input type="hidden" name="id" value="<%=Model.InformationType.ID%>" />
<% foreach (DTL.RelevantWordDTO relevantWord in Model.CurrentWords)
   { %>
<% if (!oddLine) lineColor = "#e7e7e7"; else lineColor = "#ffffff"; oddLine = !oddLine; %>
<% if (Model.CurrentWords.Count == (Model.ItemCounter + 1) && Model.CurrentList == iPatternManager.Models.CurrentListEnum.Manual) rowTopMargin = "20px"; %>
<input type="hidden" name='<%= "RelevantWordList[" + Model.ItemCounter + "].ID" %>' value='<%=relevantWord.ID%>' />
<input type="hidden" name='<%= "RelevantWordList[" + Model.ItemCounter + "].InformationTypeID" %>'
    value='<%=relevantWord.InformationTypeID%>' />
<div class="table" style="background-color: <%=lineColor%>; padding-top: 4px; padding-bottom: 4px;
    padding-left: 4px; margin-top: <%=rowTopMargin%>">
    <div style="float: left; width: 200px;">
        <% if (relevantWord.CreationType == DTL.RelevantWordDTO.CreationTypeEnum.AUTOMATIC)
           { %>
        <input style="width: 160px" type="text" disabled="disabled" name='<%= "RelevantWordList[" + Model.ItemCounter + "].Word" %>'
            value="<%=relevantWord.Word%>" />
        <% }
           else
           { %>
        <input style="width: 160px" type="text" name='<%= "RelevantWordList[" + Model.ItemCounter + "].Word" %>'
            value="<%=relevantWord.Word%>" />
        <% } %>
    </div>
    <div style="float: left; width: 200px;">
        <% if (relevantWord.CreationType == DTL.RelevantWordDTO.CreationTypeEnum.AUTOMATIC)
           { %>
        <input style="width: 160px;" disabled="disabled" type="text" name='<%= "RelevantWordList[" + Model.ItemCounter + "].Weight" %>'
            value="<%=relevantWord.Weight%>" />
        <% }
           else
           { %>
        <input style="width: 160px;" type="text" name='<%= "RelevantWordList[" + Model.ItemCounter + "].Weight" %>'
            value="<%=relevantWord.Weight%>" />
        <% } %>
    </div>
    <input type="hidden" id="<%= "RelevantWordList[" + Model.ItemCounter + "].CreationType" %>" value="<%=relevantWord.CreationType.ToString()%>" />
    <%if (!relevantWord.ID.HasValue)
      {%>
    <div style="float: left; width: 80px; color: Green; font-weight: bold;">
        Ny!</div>
    <%}
      else
      { %>
    <% if (Model.CurrentList != iPatternManager.Models.CurrentListEnum.Blocked)
       { %>
    <div style="float: left; width: 80px;">
        <a href="/Message/BlockWord/<%=relevantWord.ID%>" title="Blokérer ordet ved at sætte vægtningen til 0">
            Blokér</a></div>
    <%} %>
    <div style="float: left; width: 80px;">
        <a href="/Message/IgnoreWord/<%=relevantWord.ID%>" title="Tilføjer ordet til listen over ignorerede ord">
            Ignorer</a></div>
    <div style="float: left; width: 80px;">
        <% if (relevantWord.CreationType == DTL.RelevantWordDTO.CreationTypeEnum.MANUAL)
           { %>
        <a href="/Message/DeleteRelevantWord/<%=relevantWord.ID%>" title="Sletter ordet">Slet</a>
        <% } %>&nbsp;
    </div>
    <%} %>
</div>
<div style="clear: both;">
</div>
<% Model.ItemCounter++; %>
<% } %>
