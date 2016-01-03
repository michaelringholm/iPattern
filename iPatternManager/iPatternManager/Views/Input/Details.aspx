<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iPatternManager.Models.InputDetailsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    iPattern - Besked detaljer
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Besked detaljer</h2>
    <div style="float: left; width: 525px;">
        <table class="topAlignedNoBorder">
            <tr>
                <td>
                    <div class="fieldLabel" style="width: 100px;">
                        Input ID:</div>
                </td>
                <td>
                    <div style="width: 160px;">
                        <%= Model.InputMessage.ID %></div>
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
        </table>
        <div style="width: 500px; background-color: #FFD4AA; margin-top: 8px;">
            <div style="padding: 20px;">
                <%= Model.InputMessage.TextInputAsHTML %></div>
        </div>
    </div>
    <div id="metaValueListDiv" style="width: 500px; margin-left: 25px; float: left;">
        <%Html.RenderAction("InputMetaDataListControl", "Input", Model);%>
        <%Html.RenderAction("AttachmentListControl", "Input", Model);%>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
