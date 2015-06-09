<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Advertentie.aspx.cs" Inherits="Marktplaats.Advertentie1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="RepeaterAdvertentie" runat="server">
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
                                   <%# Eval("Titel") %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Aangeboden door:
                                </td>
                                <td>
                                    <%# Eval("ContactNaam") %>
                                </td>
                            </tr>
                     </tr>
                     <tr>
                         <td>
                             Beschrijving:
                         </td>
                         <td>
                             <%# Eval("Beschrijving") %>
                         </td>
                     </tr>       
                    </table>
            </table>
        </ItemTemplate>
    </asp:Repeater>
    <table style="width: 100%">
        <tr>
            <td style="width: 174px">Leveren/Ophalen/Beide: </td>
            <td>
                <asp:Label ID="lblType" runat="server" Text="Type"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 174px">Vaste prijs/Bieden/Overig:</td>
            <td>
                <asp:Label ID="lblPrijs" runat="server" Text="Prijs"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
