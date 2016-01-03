<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    iPattern - Analyse detaljer
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Analyse detaljer</h2>
    <% List<DTL.AnalysisResultItemDTO> analysisResultItems = (List<DTL.AnalysisResultItemDTO>)Model; %>
    <% bool oddLine = true;
       String lineColor = "#ffffff";
       Int32 totalWeight = 0;
       Int32 previousInformationTypeID = -1;
       Int32 count = 0; %>
    <% foreach (DTL.AnalysisResultItemDTO analysisResultItem in analysisResultItems)
       { %>
    <%totalWeight += analysisResultItem.SubTotalWeight; %>
    <%if (analysisResultItem.InformationTypeID != previousInformationTypeID)
      { %>
    <div style="font-size: 13px; font-weight: bold; margin-top: 28px;">
        Matchende vægtede ord tilhørende beskedmappen &quot;<%=Html.ActionLink(analysisResultItem.InformationTypeTitle, "Details", "Message", new { id = analysisResultItem.InformationTypeID}, null)%>&quot;</div>
    <div style="font-size: 13px; font-weight: bold;">Grænseværdi: <%=DAL.InformationTypeDAC.GetInformationType(analysisResultItem.InformationTypeID, iPatternManager.ContextManager.Current.SelectedAreaID.Value).CertainLimit %></div>
    <div class="table" style="color: #000000; margin-top: 8px; font-weight: bold; background-color: #C7C7C7;
        padding: 4px;">
        <div style="float: left; width: 200px;">
            Ord</div>
        <div style="float: left; width: 120px;">
            Vægt</div>
        <div style="float: left; width: 120px;">
            Antal ord</div>
        <div style="float: left; width: 120px;">
            Vægt (sub total)</div>
    </div>
    <div style="clear: both;">
    </div>
    <% } %>
    <% if (!oddLine) lineColor = "#e7e7e7"; else lineColor = "#ffffff"; oddLine = !oddLine; %>
    <div class="table" style="background-color: <%=lineColor%>; padding-top: 4px; padding-bottom: 4px;
        padding-left: 4px; padding-right: 4px;">
        <div style="float: left; width: 200px;">
            <%=analysisResultItem.Word%>&nbsp;</div>
        <div style="float: left; width: 120px;">
            <%=analysisResultItem.Weight%></div>
        <div style="float: left; width: 120px;">
            <%=analysisResultItem.WordCount%></div>
        <div style="float: left; width: 120px;">
            <%=analysisResultItem.SubTotalWeight%></div>
    </div>
    <div style="clear: both;">
    </div>
    <%if ((analysisResultItems.Count == count + 1) || (analysisResultItem.InformationTypeID != analysisResultItems[count+1].InformationTypeID))
      {
    %>
    <div class="table" style="margin-left: 4px; margin-top: 6px; margin-bottom: 16px;">
        <div style="float: left; font-weight: bold;">
            Total vægt:</div>
        <div style="float: left; font-weight: bold; margin-left: 6px; width: 120px;">
            <%=totalWeight.ToString()%>
        </div>
    </div>
    <%totalWeight = 0;%>
    <% } %>
    <%if (analysisResultItem.InformationTypeID != previousInformationTypeID) previousInformationTypeID = analysisResultItem.InformationTypeID; %>
    <% count++;
       } %>
</asp:Content>
