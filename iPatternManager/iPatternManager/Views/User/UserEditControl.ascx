<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iPatternManager.Models.RegisterModel>" %>


<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iPatternManager.Models.UserEditModel>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    EditUser
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit an account</h2>

    <% using (Html.BeginForm(SaveUser)) { %>
        <%= Html.ValidationSummary(true, "Account change was unsuccessful. Please correct the errors and try again.") %>
        <div>
            <fieldset>
                <legend>Kontooplysninger</legend>
               
                <div class="display-label">
                    <%= Html.LabelFor(m => m.UserID) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.Email) %>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.Email) %>
                    <%= Html.ValidationMessageFor(m => m.Email) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.Password) %>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.Password) %>
                    <%= Html.ValidationMessageFor(m => m.Password) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.ConfirmPassword) %>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.ConfirmPassword) %>
                    <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </div>
                

                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.IsAdministrator) %>
                </div>
                <div class="editor-field">
                    <%= Html.CheckBoxFor(m => m.IsAdministrator) %>
                </div>
                <p>
                    <input type="submit" value="EditUser" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
