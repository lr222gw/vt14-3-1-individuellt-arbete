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
                    <asp:TextBox ID="GameName" placeholder="<%#: Item.GameName %>" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Du måste fylla i ett namn!" Text="*" ControlToValidate="GameName" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        

                    <label for="CompanyName">Företag</label>
                    <asp:TextBox runat="server" id="CompanyName" placeholder="<%#: Item.CompanyName %>" ></asp:TextBox>      
                    
                    <label for="ReleaseDate">Utgivningsdatum</label>
                    <asp:TextBox runat="server" id="ReleaseDate" placeholder="<%#: Item.ReleaseDate %>" ></asp:TextBox>   
                    
                    <label for="PlayersOffline">Spelare Offline</label>
                    <asp:TextBox runat="server" id="PlayersOffline" placeholder="<%#: Item.PlayersOffline %>" ></asp:TextBox>   
                    
                    <label for="PlayersOnline">Spelare Online</label>
                    <asp:TextBox runat="server" id="PlayersOnline" placeholder="<%#: Item.PlayersOnline %>" ></asp:TextBox>   
                    
                    <label for="Story">Handling</label>
                    <asp:TextBox runat="server" id="Story" value="<%#: Item.Story %>" ></asp:TextBox>   <%-- Sparar ner Storyn i text, den är oftast så lång att man inte vill skriva om hela bara för att ändra något i den.. --%>
                    
                    <label for="CustomNote">Egen notering</label>
                    <asp:TextBox runat="server" id="CustomNote" placeholder="<%#: Item.CustomNote %>" ></asp:TextBox>
                    
                </ItemTemplate>
            <FooterTemplate>
                <asp:Button ID="Sendbutton" runat="server" Text="Spara/Uppdatera" OnClick="Sendbutton_Click"/>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </ul>
            </FooterTemplate>
        </asp:Repeater> 
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="OutsideContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PHScript" runat="server">
</asp:Content>
