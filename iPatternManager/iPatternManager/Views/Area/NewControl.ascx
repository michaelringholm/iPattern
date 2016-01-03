<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DTL.AreaDTO>" %>
    <div style="float: left; font-weight: bold; width: 80px;">
        Firma:
    </div>
    <div style="float: left;">
        iHedge
    </div>
    <div class="clear">
    </div>
    <%Html.BeginForm("SaveNew", "Area"); %>
    <div style="margin-top: 12px;">
        <div class="fieldLabel" style="float: left; width: 80px;">
            Navn:</div>
        <div style="float: left; width: 200px;">
            <%=Html.TextBoxFor(m => m.Title, new { style="width: 180px;" }) %></div>           
        <div style="float: left; width: 200px;">
            <%=Html.ValidationMessageFor(m => m.Title) %></div>
        <div style="clear: both; padding: 6px;">
        </div>
        <div>
            <input style="margin-top: 10px;" type="submit" value="Gem" />
        </div>
    </div>
    <%Html.EndForm(); %>