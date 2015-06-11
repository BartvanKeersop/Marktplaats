<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Advertenties.aspx.cs" Inherits="Marktplaats.Advertenties" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    
<asp:Repeater ID="RepeaterAdvertenties" runat="server">
<ItemTemplate>
    <table class ="lines">
            <td>Plaatje</td>
            <td>
                <table>
                    <tr>
                        <td>Titel:</td>
                        <td><a href="/Advertentie/<%# Eval("ID") %>"> <asp:Label runat="server" Text='<%# Eval("Titel") %>'></asp:Label></a></td>
                    </tr>
                    <tr>
                        <td>Naam:</td>
                        <td><a href="/Gebruiker/<%# Eval("PERSOONID") %>"><asp:Label runat="server" Text='<%# Eval("Naam") %>'></asp:Label></td>
                    </tr>
                </table>     
    </table>
</ItemTemplate>
</asp:Repeater>
    
</asp:Content>
