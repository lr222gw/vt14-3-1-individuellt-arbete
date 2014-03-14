<%@ Page Title="Listor" Language="C#" MasterPageFile="~/pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Spellistaren.pages.Default" %>

<asp:Content ID="Content" ContentPlaceHolderID="PHContent" runat="server">
    <div id="Lists">
        <asp:Repeater ID="ListRepeater" runat="server" ItemType="Spellistaren.model.List" SelectMethod="ListRepeater_GetData">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                
                    <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#: "Default.aspx?List=" + Server.UrlEncode((Item.ListID).ToString())  %>' Text='<%#: Item.ListName %>'></asp:HyperLink>
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
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#: "Default.aspx?List="+ Request.QueryString["List"] + Server.UrlEncode((Item.GameID).ToString())  %>' Text='<%#: Item.GameName %>'></asp:HyperLink>
                    </li>
                </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>

</asp:Content>

