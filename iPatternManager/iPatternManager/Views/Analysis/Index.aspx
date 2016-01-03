<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    iPattern - Analyser
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Analyser</h2>
    <div class="table" style="background-color: #c7c7c7; color: #000000; margin-top: 12px;
        padding-top: 4px; padding-bottom: 4px; padding-left: 4px;">
        <div style="float: left; width: 100px; font-weight: bold; text-indent: 16px;">
            Besked ID</div>
        <div style="float: left; width: 200px; font-weight: bold;">
            Beskedmappe</div>
        <div style="float: left; width: 80px; font-weight: bold;">
            Input ID</div>
        <div style="float: left; width: 200px; font-weight: bold;">
            Sidst analyseret</div>
    </div>
    <div style="clear: both;">
    </div>
    <% List<DTL.AnalysisResultDTO> analysisResults = (List<DTL.AnalysisResultDTO>)Model; %>
    <% bool oddLine = true;
       String lineColor = "#ffffff"; %>
    <% foreach (DTL.AnalysisResultDTO analysisResult in analysisResults)
       { %>
    <% if (!oddLine) lineColor = "#e7e7e7"; else lineColor = "#ffffff"; oddLine = !oddLine; %>
    <input type="hidden" value="<%=analysisResult.InformationTypeID%>" />
    <div class="table" style="background-color: <%=lineColor%>; padding-top: 4px; padding-bottom: 4px;
        padding-left: 4px;">
        <div style="float: left; width: 100px; text-align: right;">
            <a href="/Analysis/Details/<%=analysisResult.ID%>" style="margin-right: 25px;">
                <%=analysisResult.ID%></a></div>
        <div style="float: left; width: 200px;">
            <a href="/Input/Index/<%=analysisResult.InformationTypeID%>">
                <%=analysisResult.InformationTypeTitle%></a>
        </div>
        <div style="float: left; width: 80px; text-align: right;">
            <a href="/Input/Details/<%=analysisResult.AnalysisInputID%>" style="margin-right: 25px;">
                <%=analysisResult.AnalysisInputID%></a></div>
        <div style="float: left; width: 200px;">
            <%=analysisResult.EventTime%></div>
    </div>
    <div style="clear: both;">
    </div>
    <% } %>
</asp:Content>
