<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iPatternManager.Models.UserEditModel>" %>

<asp:Content ID="editUserTitle" ContentPlaceHolderID="TitleContent" runat="server">
    EditUser
</asp:Content>

<asp:Content ID="editUserContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Ret en konto</h2>

    <% using (Html.BeginForm("SaveUser","User"))
       { %>
        <%= Html.ValidationSummary(true, "User change was unsuccessful. Please correct the errors and try again.") %>
        <div>
            <fieldset>
                <legend>Kontooplysninger
               
                <a style=" visibility:hidden">
                    <%= Html.LabelFor(m => m.UserID) %></a>
                </div>
                <a style=" visibility:hidden">
                    <%= Html.TextBoxFor(m => m.UserID) %></a>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.Email) %>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.Email) %>
                    <%= Html.ValidationMessageFor(m => m.Email) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.IsAdministrator) %>
                </div>
                <div class="editor-field">
                    <%= Html.CheckBoxFor(m => m.IsAdministrator) %>
                </div>
                </legend>
                <p>
                    <input type="submit" value="Save" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
