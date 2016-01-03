<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iPatternManager.Models.UserIndexModel>" %>
<div style="background-color: #c7c7c7; color: #000000; margin-top: 12px; padding-top: 4px;
    padding-bottom: 4px; display: table; text-indent: 4px; width: 100%;">
    <div style="float: left; width: 100px; font-weight: bold;">
        Ret</div>
    <div style="float: left; width: 200px; font-weight: bold;">
        Email</div>
    <div style="float: left; width: 200px; font-weight: bold;">
        Tilknyttet Område</div>
    <div style="float: left; width: 200px; font-weight: bold;">
        Sidst opdateret</div>
    <div style="float: left; width: 100px; font-weight: bold;">
        Områder</div>
</div>
<div style="clear: both;">
</div>
<% bool oddLine = false;
   String lineColor = "#ffffff";
   int rowIndex = 0;
   int rowMarginTop = 0; %>
<%using (Ajax.BeginForm("SaveUsers", "User", new AjaxOptions { UpdateTargetId = "userListDiv" }))
  { %>
<% foreach (DTL.UserDTO user in Model.Users)
   {  %>
<% if (!oddLine) lineColor = "#e7e7e7"; else lineColor = "#ffffff"; oddLine = !oddLine;
   if (!String.IsNullOrEmpty(user.ID)) rowMarginTop = 0; else rowMarginTop = 10; %>
<div style="display: table; background-color: <%=lineColor%>; padding-top: 4px; padding-bottom: 4px;
    width: 100%; text-indent: 4px; margin-top: <%=rowMarginTop%>px;">
    <input type="hidden" name="users[<%=rowIndex%>].ID" value="<%=user.ID%>" />
    <div style="float: left; width: 100px;">
        <a href="/User/EditUser/<%=user.ID%>">Ret</a></div>
    <div style="float: left; width: 200px;">
        <%=Html.TextBox("users[" + rowIndex + "].Email", user.Email, new { style = "width: 190px;" })%></div>
    <div style="float: left; width: 200px;">
        <%=Html.DropDownList("users[" + rowIndex + "].DefaultAreaID", Model.AreaNameList[user.ID])%></div>
    <div style="float: left; width: 200px;">
        <%=Html.TextBox("users[" + rowIndex + "].EventTime", user.EventTime, new { style = "width: 190px;" })%></div>
    <div style="float: left; width: 200px;">
        <a href="/User/Areas/<%=user.ID%>">Områder</a>
    <div style="float: left; width: 20px; visibility :hidden ">
        <%=Html.TextBox("users[" + rowIndex + "].Company_ID", user.Company_ID, new { style = "width: 190px;" } )%></div>   
    </div>
</div>
<% rowIndex++;
   } %>
   <div style="margin-top: 6px; margin-left: 4px;"><input type="submit" value="Gem" /></div>
<%} %>