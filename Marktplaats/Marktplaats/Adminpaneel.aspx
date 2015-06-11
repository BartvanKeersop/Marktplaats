<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Adminpaneel.aspx.cs" Inherits="Marktplaats.Adminpaneel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="lines">
        <tr>
            <td style="width: 130px">Naam:</td>
            <td>
                <asp:TextBox ID="tbNaam" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 130px">Categorie:</td>
            <td>
                <asp:DropDownList ID="ddlCategorie" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 130px">Subcategorie:</td>
            <td>
                <asp:DropDownList ID="ddlSubcategorie" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 130px">
                <asp:Button ID="btnToevoegen" runat="server" Text="Categorie Toevoegen" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
