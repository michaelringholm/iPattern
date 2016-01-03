<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iPatternManager.Models.UserIndexModel>" %>
<div style="float: left; width: 400px; font-weight: bold; display: table ">
    Bruger: 
    <%=Model.SelectedUser.Email%>
</div>
<div style="clear: both;">
</div>
<div style="background-color: #c7c7c7; color: #000000; margin-top: 12px; padding-top: 4px;
    padding-bottom: 4px; display: table; text-indent: 4px; width: 100%;">
    <div style="float: left; width: 200px; font-weight: bold;">
        Område</div>
    <div style="float: left; width: 200px; font-weight: bold;">
        Vises</div>
</div>
<div style="clear: both;">
</div>
<% bool oddLine = false;
   String lineColor = "#ffffff";
   int rowIndex = 0;
   int rowMarginTop = 0; %>
   

<%Html.BeginForm("SaveUserAreas", "User"); %>   
<%using (Ajax.BeginForm("Areas", "Areas", new AjaxOptions { UpdateTargetId = "userListDiv" }))
  { %>
  
<% foreach (DTL.AreaDTO area in Model.Areas)
   {  %>
<% if (!oddLine) lineColor = "#e7e7e7"; else lineColor = "#ffffff"; oddLine = !oddLine;
   if (area.ID.HasValue) rowMarginTop = 0; else rowMarginTop = 10; %>
    <div style="display: table; background-color: <%=lineColor%>; padding-top: 4px; padding-bottom: 4px;
        width: 100%; text-indent: 4px; margin-top: <%=rowMarginTop%>px;">
        <div style="float: left; width: 200px;">
        <%=Html.TextBox("areas[" + rowIndex + "].Title", area.Title)%></div>
        <%=Html.CheckBox("areas[" + rowIndex + "].Selected", area.Selected, new { style = "width: 190px;" })%>
        <a style=" visibility:hidden">
        <%=Html.TextBox("areas[" + rowIndex + "].ID", area.ID, new { style = "width: 190px;" })%></a>
    </div>
<% rowIndex++;
   } %>
   <div style="margin-top: 6px; margin-left: 4px;"><input type="submit" value="Gem" /></div>
    <div style="float: left; width: 200px; visibility :hidden ">
        <%=Html.TextBox("selectedUser.ID", Model.SelectedUser.ID, new { style = "width: 190px;" })%></div>
<%} %>