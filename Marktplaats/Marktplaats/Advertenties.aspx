<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Advertenties.aspx.cs" Inherits="Marktplaats.Advertenties" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    
    <asp:Repeater ID="RepeaterAdvertenties" runat="server">
        <ItemTemplate>
            <table>
                <tr>
                    <td>
                        
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                Titel:
                                </td>
                                <td>
                                <a href="/Advertentie/<%# Eval("Id") %>">
                                    <asp:Label runat="server" Text='<%# Eval("Titel") %>'>
                                    </asp:Label>
                                </a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Naam:
                                </td>
                                <td>
                                <a href="/Gebruiker/<%# Eval("Email") %>">
                                    <asp:Label runat="server" Text='<%# Eval("Naam") %>'>
                                    </asp:Label>
                                </td>
                            </tr>
                     </tr>       
                    </table>
            </table>
        </ItemTemplate>
    </asp:Repeater>
    
</asp:Content>
