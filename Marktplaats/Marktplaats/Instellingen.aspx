<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Instellingen.aspx.cs" Inherits="Marktplaats.Instellingen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Instellingen</h3>
    <asp:Repeater ID="RepeaterInstellingen" runat="server">
        <ItemTemplate>
            <table>
                 <tr>
                    <td>
                        Naam:
                    </td>
                    <td>
                    <asp:TextBox ID="TextBoxNaam" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Naam") %>'></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
   
</asp:Content>
