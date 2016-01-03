<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    iPattern - Velkommen
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Velkommen til iPattern, som gør det enkelt og overskueligt at gennemgå dagens e-mails</h2>
    <div style="width: 600px; padding-bottom: 100px;">
        <p>
            iPattern er en add-on komponent, der automatisk identificerer, sorterer og arkiverer jeres e-mails, så det bliver både nemmere og mere overskueligt at håndtere og anvende informationerne.
Baseret på intelligent mønstergenkendelse identificerer iPattern automatisk typen af e-mail, trækker definerede metadata ud og arkiverer mailen i de ønskede systemer. Metadata udtræk kan være alt fra e-mail adresser, navne og telefonnumre til ordrenumre og kundenumre. Det er kun fantasien og forretningen, der sætter grænsen. Med iPattern vil medarbejdere fremadrettet kunne træffe mere informerede beslutninger og samtidig være i stand til at håndtere langt flere kundehenvendelser eller behandle flere ordrer. 

        </p>
        <p>
            <a href="/Home/About/" title="Læs mere">Læs mere.</a>
        </p>
    </div>
</asp:Content>
