<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdvertentieAanmaken.aspx.cs" Inherits="Marktplaats.AdvertentieAanmaken" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    Vul hieronder de Advertentiegegevens in, de velden met een * zijn verplicht.
    <br/>
    <asp:Label ID="lblWaarschuwing" runat="server" CssClass="highlight"></asp:Label>
    <br/>
    <table class="lines">
        <tr>
            <td style="width: 165px">Kies een categorie:*</td>
            <td>
                <asp:DropDownList ID="ddlCategorie" runat="server" OnSelectedIndexChanged="ddlCategorie_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 165px">Kies een sub categorie:*</td>
            <td>
                <asp:DropDownList ID="ddlSubCategorie" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 165px">Titel van de advertentie:*</td>
            <td>
                <asp:TextBox ID="tbTitel" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 165px">Conditie:*</td>
            <td>
                <asp:DropDownList ID="ddlConditie" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 165px">Merk:</td>
            <td>
                <asp:TextBox ID="tbMerk" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 165px">Foto:</td>
            <td>
                <asp:FileUpload ID="fuFoto" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 165px">Prijs:*</td>
            <td>
                <asp:TextBox ID="tbPrijs" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 165px; height: 120px;">Beschrijving*:</td>
            <td style="height: 120px">
                <asp:TextBox ID="tbBeschrijving" runat="server" Height="105px" TextMode="MultiLine" Width="186px"></asp:TextBox>
            </td>
        </tr>
        </table>
    
    <asp:Label ID="lblMeldingen" runat="server" Text="Meldingen" CssClass="highlight" Visible="False"></asp:Label>
    
    <br/>
    <br/>
    <asp:Button ID="btnPlaatsAdvertentie" runat="server" Text="Plaats uw advertentie!" OnClick="btnPlaatsAdvertentie_Click" />
</asp:Content>
