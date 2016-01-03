<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iPatternManager.Models.MessageIndexModel>" %>
<% bool oddLine = false;

   if (TempData["OddLine"] != null)
       oddLine = Convert.ToBoolean(TempData["OddLine"]);
    
   String lineColor = "#ffffff";
   Int32 marginLeft = ((Model.Level - 1) * 20) + 4;  %>
<% foreach (DTL.InformationTypeDTO informationType in Model.InformationTypes)
   { %>
<% if (!oddLine) lineColor = "#e7e7e7"; else lineColor = "#ffffff"; oddLine = !oddLine; %>
<div class="table" style="background-color: <%=lineColor%>; padding-top: 4px; padding-bottom: 4px;
    text-indent: <%=marginLeft.ToString()%>px;">
    <div style="float: left; width: 16px;">
        <%if (DAL.InformationTypeDAC.HasChildren(informationType.ID.Value, iPatternManager.ContextManager.Current.SelectedAreaID.Value))
          {
              if (Model.CollapsedElements.Contains(informationType.ID.Value))
              {%>
                <img style="cursor: hand;" src="/Content/Images/bullet-toggle-plus-icon16.png" alt="" onclick="return CollapseExpand(<%=informationType.ID%>)" />
        <% }
              else
              { %>
              <img style="cursor: hand;" src="/Content/Images/bullet-toggle-minus-icon16.png" alt="" onclick="return CollapseExpand(<%=informationType.ID%>)" />
        <% }
          }
          else
          { %>
        <img src="/Content/Images/bullet-black-icon16.png" alt="" />
        <% } %>
    </div>
    <div style="float: left; width: 200px;">
        <a href="/Input/Index/<%=informationType.ID%>">
            <%=informationType.Title%></a>
    </div>
    <div style="float: left; width: 30px;">
        <a href="/Input/Index/<%=informationType.ID%>">
            <img src="/Content/Images/Folder-icon16.png" title="Beskeder" alt="Beskeder" /></a>
    </div>
    <div style="float: left; width: 40px;">
        <%if (Model.UnreadResults.ContainsKey(informationType.ID.Value))
          { %>
            <%if (informationType.Title == "Unknown")
              { %>
                <%if (Model.UserFilteredUnreadResults.ContainsKey(informationType.ID.Value))
                  { %>
                    (<label style="color: Green; font-weight: bold;"><%=Model.UserFilteredUnreadResults[informationType.ID.Value]%></label>)
                <%}
                  else
                  {%>
                    (<label style="color: Green; font-weight: bold;">0</label>)
                <%}
              }
              else
              {%>
                (<label style="color: Green; font-weight: bold;"><%=Model.UnreadResults[informationType.ID.Value]%></label>)
            <%}
          }
          else
          {%>
        (0)
        <%}%></div>
    <%if (informationType.Title.ToLower() != "unknown")
      { %>
    <div style="float: left; width: 30px;">
        <a href="/Message/Details/<%=informationType.ID%>">
            <img src="/Content/Images/Settings-icon16.png" title="Vægtning" alt="Vægtning" /></a></div>
    <div style="float: left; width: 30px;">
        <a href="/MetaData/KeyList/<%=informationType.ID%>">
            <img src="/Content/Images/Properties-icon16.png" title="Meta data" alt="Meta data" /></a></div>
    <div style="float: left; width: 30px;">
        <a href="/Message/New/<%=informationType.ID%>">
            <img src="/Content/Images/folder-add-icon16.png" title="Opret undermappe" alt="Opret undermappe" /></a></div>
    <div style="float: left; width: 30px;">
        <a href="/Message/Delete/<%=informationType.ID%>">
            <img src="/Content/Images/folder-remove-icon16.png" title="Slet mappe" alt="Slet undermappe" /></a></div>
    <%} %>
</div>
<div style="clear: both;">
</div>
<%if ((!Model.CollapsedElements.Contains(informationType.ID.Value)) && DAL.InformationTypeDAC.HasChildren(informationType.ID.Value, iPatternManager.ContextManager.Current.SelectedAreaID.Value)) { Html.RenderAction("RenderListControl", "Message", new { parentID = informationType.ID.Value, level = Model.Level + 1, oddLine = oddLine }); }%>
<% } %>