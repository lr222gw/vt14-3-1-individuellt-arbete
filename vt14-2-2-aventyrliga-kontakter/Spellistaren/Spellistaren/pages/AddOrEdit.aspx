<%@ Page Title="" Language="C#" MasterPageFile="~/pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="AddOrEdit.aspx.cs" Inherits="Spellistaren.pages.AddOrEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PHTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PHContent" runat="server">
    <h1>Lägg till Spel</h1>
    <div id="allGameList">
        <asp:Repeater ID="GamelistRepeater" runat="server" ItemType="Spellistaren.model.Game" SelectMethod="GamelistRepeater_GetData">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:HyperLink runat="server" Text='<%#: Item.GameName %>' NavigateUrl='<%#: GetRouteUrl("AddOrEdit", null) + "?GameID=" + Server.UrlEncode(Item.GameID.ToString()) %>'></asp:HyperLink>
                    </li>
                </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <asp:Button ID="addnewgame" runat="server" Text="Nytt spel +" OnClick="addnewgame_Click" />
    <div id="GameDetails">
        <asp:Repeater ID="GameDetailRepeater" runat="server" ItemType="Spellistaren.model.Game" SelectMethod="GameDetailRepeater_GetData" Visible="false">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
                <ItemTemplate>
                    <label for="GameName">Spelets namn</label>
                    <input id="GameName" type="text" placeholder="<%#: Item.GameName %>" />
                           
                    <label for="CompanyName">Företag</label>
                    <input id="CompanyName" type="text" placeholder="<%#: Item.CompanyName %>" />      
                    
                    <label for="ReleaseDate">Utgivningsdatum</label>
                    <input id="ReleaseDate" type="text" placeholder="<%#: Item.ReleaseDate %>" />   
                    
                    <label for="PlayersOffline">Spelare Offline</label>
                    <input id="PlayersOffline" type="text" placeholder="<%#: Item.PlayersOffline %>" />   
                    
                    <label for="PlayersOnline">Spelare Online</label>
                    <input id="PlayersOnline" type="text" placeholder="<%#: Item.PlayersOnline %>" />   
                    
                    <label for="Story">Handling</label>
                    <input id="Story" type="text"  value="<%#: Item.Story %>" />   <%-- Sparar ner Storyn i text, den är oftast så lång att man inte vill skriva om hela bara för att ändra något i den.. --%>
                    
                    <label for="CustomNote">Egen notering</label>
                    <input id="CustomNote" type="text" placeholder="<%#: Item.CustomNote %>" />
                    
                </ItemTemplate>
            <FooterTemplate>
                <asp:Button ID="Sendbutton" runat="server" Text="Spara/Uppdatera" OnClick="Sendbutton_Click"/>
                </ul>
            </FooterTemplate>
        </asp:Repeater> 
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="OutsideContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PHScript" runat="server">
</asp:Content>
