<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Advertentie.aspx.cs" Inherits="Marktplaats.Advertentie1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnVerwijderen" runat="server" ForeColor="#FF3300" Text="Advertentie Verwijderen" OnClick="btnVerwijderen_Click" />
    <br/>
    <br/>
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
    <br />
    <asp:Label runat="server" Text="Plaats hieronder uw bod"></asp:Label>
    <table class ="lines">
        <tr>
            <th>
                Naam
            </th>
            <th>
                Bedrag
            </th>
            <th>
                Datum
            </th>
        </tr>
    <asp:Repeater ID="RepeaterBod" runat="server">
         <ItemTemplate>
                            <tr>
                                <td>
                                <a href="/Gebruiker/<%# Eval("EMAIL") %>">
                                    <asp:Label runat="server" Text='<%# Eval("NAAM") %>'>
                                    </asp:Label>
                                </a>
                                </td>
                                <td>
                                    <asp:Label runat="server" Text='<%# Eval("BEDRAG") %>'>
                                    </asp:Label> Euro
                                </td>
                                <td>
                                    <asp:Label runat="server" Text='<%# Eval("DATUM") %>'>
                                    </asp:Label>
                                </td>
                            </tr>
        </ItemTemplate>
    </asp:Repeater>
    </table>
    <br/>
    <table class="auto-style1">
        <tr>
            <td class="auto-style3" style="width: 100px">
    <asp:TextBox ID="tbBieden" runat="server" Width="70px" onkeypress="return isNumberKey(event)"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblMessageBod" runat="server" Text="Label" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style3" style="width: 100px">
     <asp:Button ID="btnBieden" runat="server" Text="Bieden" OnClick="btnBieden_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <br/>
     </asp:Content>
