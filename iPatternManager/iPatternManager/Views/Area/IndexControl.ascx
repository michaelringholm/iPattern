<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iPatternManager.Models.AreaIndexModel>" %>
<div style="background-color: #c7c7c7; color: #000000; margin-top: 12px; padding-top: 4px;
    padding-bottom: 4px; display: table; text-indent: 4px; width: 100%;">
    <div style="float: left; width: 200px; font-weight: bold;">
        Navn</div>
    <div style="float: left; width: 200px; font-weight: bold;">
        Sidst opdateret</div>
    <div style="float: left; width: 200px; font-weight: bold;">
        Vis kun brugers egne mails i unknown mappe</div>
</div>
<div style="clear: both;">
</div>
<% bool oddLine = false;
   String lineColor = "#ffffff";
   int rowIndex = 0;
   int rowMarginTop = 0; %>
<%using (Ajax.BeginForm("SaveAreas", new AjaxOptions { UpdateTargetId = "areaListDiv" }))
  { %>
<% foreach (DTL.AreaDTO area in Model.Areas)
   {  %>
<% if (!oddLine) lineColor = "#e7e7e7"; else lineColor = "#ffffff"; oddLine = !oddLine;
   if (area.ID.HasValue) rowMarginTop = 0; else rowMarginTop = 10; %>
<div style="display: table; background-color: <%=lineColor%>; padding-top: 4px; padding-bottom: 4px;
    width: 100%; text-indent: 4px; margin-top: <%=rowMarginTop%>px;">
   <input type="hidden"  name="areas[<%=rowIndex%>].ID" value="<%=area.ID%>" />
       <div style="float: left; width: 200px;">
        <input type="text" name="areas[<%=rowIndex%>].Title" value="<%=area.Title%>" style = "width: 190px;" /></div>
    <div style="float: left; width: 200px;">
        <%=area.EventTime%></div>
    <div style="float: left; width: 200px;">
        <%if (area.FilterOnUnknown)
          { %>
        <input type="checkbox" name="areas[<%=rowIndex%>].FilterOnUnknown" checked="checked" onclick=" this.value = this.checked;" value="<%=area.FilterOnUnknown%>" style = "width: 190px;" />
        <% }
          else
          { %>
        <input type="checkbox" name="areas[<%=rowIndex%>].FilterOnUnknown" onclick="this.value = this.checked" value="<%=area.FilterOnUnknown%>" style="width: 190px;" />
        <% } %>
        </div>
    <div style="float: left; width: 200px;">
        <%if (area.ID.HasValue)
          { %>
        <a href="/Area/Remove/<%=area.ID%>">Fjern</a>
        <%}
          else
          { %>
        <label style="color: Green;">
            Ny!</label>
        <%} %>
    </div>
</div>
<% rowIndex++;
   } %>
   
   
   <div style="margin-top: 6px; margin-left: 4px;"><input type="submit" value="Gem" /></div>
<%} %>