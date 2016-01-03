<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iPatternManager.Models.InputDetailsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    iPattern - Besked detaljer
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BreadCrumb" runat="server">
    <%    List<SelectListItem> items = new List<SelectListItem>();
          items.Add(new SelectListItem { Text = "Beskedmapper", Value = "/Message/Index" });
        
          int id = Model.AnalysisResult.InformationTypeID;
        DTL.InformationTypeDTO parentInformationType = DAL.InformationTypeDAC.GetParentInformationType(id, iPatternManager.ContextManager.Current.SelectedAreaID.Value);
        while (parentInformationType != null)
          {
              items.Insert(1, new SelectListItem { Text = parentInformationType.Title, Value = "/Input/Index/" + parentInformationType.ID.Value });
              id = parentInformationType.ID.Value;
              parentInformationType = DAL.InformationTypeDAC.GetParentInformationType(id, iPatternManager.ContextManager.Current.SelectedAreaID.Value);
              
          }

          items.Add(new SelectListItem { Text = Model.AnalysisResult.InformationTypeTitle, Value = "/Input/Index/" + Model.AnalysisResult.InformationTypeID });
          items.Add(new SelectListItem { Text = Model.AnalysisResult.ID.ToString(), Value = "/Result/Details/" + Model.AnalysisResult.ID.Value });
          Html.RenderPartial("BreadCrumbControl", new iPatternManager.Models.BreadCrumbModel { Items = items });  %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Besked detaljer</h2>
    <div style="float: left; width: 525px;">
        <table class="topAlignedNoBorder">
            <tr>
                <td>
                    <div class="fieldLabel" style="width: 100px;">
                        Besked ID:</div>
                </td>
                <td>
                    <div style="width: 160px;">
                        <%= Model.AnalysisResult.ID %></div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="fieldLabel" style="width: 100px;">
                        Input ID:</div>
                </td>
                <td>
                    <div style="width: 160px;">
                        <%= Model.AnalysisResult.AnalysisInputID %></div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="fieldLabel" style="width: 100px;">
                        Modtaget:</div>
                </td>
                <td>
                    <div style="width: 160px;">
                        <%= Model.InputMessage.EventTime%></div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="fieldLabel" style="width: 130px;">
                        <a href="/Analysis/Details/<%=Model.AnalysisResult.ID.Value %>">Se analysedetaljer</a></div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="fieldLabel" style="width: 130px;">
                        <a href="/Input/Rerun/<%=Model.AnalysisResult.ID.Value %>">Genkør analyse</a></div>
                </td>
            </tr>            
        </table>
        <div style="width: 500px; background-color: #FFD4AA; margin-top: 8px;">
            <div style="padding: 20px;">
                <%= Model.InputMessage.TextInputAsHTML %></div>
        </div>
    </div>
    <div id="metaValueListDiv" style="width: 500px; margin-left: 25px; float: left;">
        <%Html.RenderAction("InputMetaDataListControl", "Input", Model);%>
        <%Html.RenderAction("ValueListControl", "MetaData", Model.AnalysisResult.ID.Value);%>
        <%Html.RenderAction("AttachmentListControl", "Input", Model);%>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
