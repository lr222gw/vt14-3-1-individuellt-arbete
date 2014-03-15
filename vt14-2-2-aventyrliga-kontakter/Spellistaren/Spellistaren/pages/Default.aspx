<%@ Page Title="Listor" Language="C#" MasterPageFile="~/pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Spellistaren.pages.Default" %>

<asp:Content ID="Content" ContentPlaceHolderID="PHContent" runat="server">
    <div id="Lists">
        <asp:Repeater ID="ListRepeater" runat="server" ItemType="Spellistaren.model.List" SelectMethod="ListRepeater_GetData">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                
                    <li>
                        <asp:HyperLink runat="server" NavigateUrl='<%#: "Default.aspx?List=" + Server.UrlEncode((Item.ListID).ToString())  %>' Text='<%#: Item.ListName %>'></asp:HyperLink>
                    </li>
                
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    
    <div id="listContent">
        <asp:Repeater ID="ListContentRepeater" runat="server" ItemType="Spellistaren.model.Game" SelectMethod="ListContentRepeater_GetData">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:HyperLink runat="server" NavigateUrl='<%#: "Default.aspx?List="+ Request.QueryString["List"] + "&GameID=" + Server.UrlEncode((Item.GameID).ToString())  %>' Text='<%#: Item.GameName %>'></asp:HyperLink>
                    </li>
                </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <div id="gameDetails">
        <asp:Repeater ID="GameDetailsRepeater" runat="server" ItemType="Spellistaren.model.Game" SelectMethod="GameDetailsRepeater_GetData">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
                <ItemTemplate>
                    <li id="gamename">
                        <h1>
                            <%#: Item.GameName  %>
                        </h1>
                    </li>
                    <li>
                        <h4>Företag</h4>
                        <p>
                            <%#: Item.CompanyName  %>
                        </p>
                    </li>
                    <li>
                        <h4>Utgivningsdatum</h4>
                        <p>
                            <%#: Item.ReleaseDate  %>
                        </p>
                    </li>
                    <li>
                        <h4>Spelare Offline</h4>
                        <p>
                            <%#: Item.PlayersOffline  %>
                        </p>
                    </li>
                    <li>
                        <h4>Spelare Online</h4>
                        <p>
                            <%#: Item.PlayersOnline  %>
                        </p>
                    </li>
                    <li>
                        <h4>Handling</h4>
                        <p>
                            <%#: Item.Story  %>
                        </p>
                    </li>
                    <li>
                        <h4>Egen notering</h4>
                        <p>
                            <%#: Item.CustomNote  %>
                        </p>
                    </li>
                </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>

        </asp:Repeater>
    </div>

</asp:Content>

