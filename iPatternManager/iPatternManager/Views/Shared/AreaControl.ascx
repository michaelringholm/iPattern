<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<SelectListItem>>" %>
<label style="font-weight: bold; color: #ffffff; margin-right: 6px;">
    Valgte område:</label><%=Html.DropDownList("areas", Model, new { onchange = "ChangeArea(this)" })%>