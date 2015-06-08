<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Categorie.aspx.cs" Inherits="Marktplaats.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:Repeater ID="RepeaterSubCategorie" runat="server">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <a href="/Categorie/<%# Eval("GroepId") %>">
                                        <asp:Label runat="server" Text='<%# Eval("Groepnaam") %>'>
                                        </asp:Label>
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
&nbsp;
</asp:Content>
