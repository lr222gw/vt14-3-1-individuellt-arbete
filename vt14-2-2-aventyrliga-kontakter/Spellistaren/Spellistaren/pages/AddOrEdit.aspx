﻿<%@ Page Title="" Language="C#" MasterPageFile="~/pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="AddOrEdit.aspx.cs" Inherits="Spellistaren.pages.AddOrEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PHTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PHContent" runat="server">
    <h2 id="allGameListHeader">Lägg till Spel</h2>
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
    <asp:Button ID="addnewgame" runat="server" Text="Nytt spel +" OnClick="addnewgame_Click"  CausesValidation="false" UseSubmitBehavior="false"/>
    <asp:Button ID="Close" runat="server" Text="close" OnClick="Close_Click"  CausesValidation="false" UseSubmitBehavior="false"/>
    <div id="GameDetails">
        <asp:FormView ID="GameDetailRepeater" ItemType="Spellistaren.model.Game" SelectMethod="GameDetailRepeater_GetData" runat="server" Visible="false" OnPageIndexChanging="GameDetailRepeater_PageIndexChanging">
            <ItemTemplate>
                    <label for="GameName">Spelets namn</label>
                    <asp:TextBox ID="GameName" value="<%#: (Item.GameName) %>" runat="server" CssClass="boxes"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Du måste fylla i ett namn!" Text="*" ControlToValidate="GameName" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CV_gamename" runat="server" ErrorMessage="Måste vara mellan 1-65 tecken." Display="Dynamic" ControlToValidate="GameName" Operator="GreaterThan" Text="*"  ValueToCompare="65"></asp:CompareValidator>

                    <label for="CompanyName">Företag</label>
                    <asp:TextBox runat="server" id="CompanyName" value='<%#: Item.CompanyName %>' CssClass="boxes"></asp:TextBox>
                        <asp:CompareValidator runat="server" ID="CV_companyname" ErrorMessage="Måste vara mellan 0-35 tecken." Display="Dynamic" ControlToValidate="CompanyName" Operator="GreaterThan" Text="*" Type="String" ValueToCompare="35" ></asp:CompareValidator>
                    
                    <label for="ReleaseDate">Utgivningsdatum</label>
                    <asp:TextBox runat="server" id="ReleaseDate" value="<%#: Item.ReleaseDate.Value.ToShortDateString()  %>" CssClass="boxes"></asp:TextBox>   
                        <asp:RegularExpressionValidator ID="RegexValidatorReleaseDate" runat="server" ErrorMessage="Datum måste anges DD/MM/ÅÅÅÅ ex: 4/1/2001" ControlToValidate="ReleaseDate" Display="Dynamic" Text="*" ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$"></asp:RegularExpressionValidator>
                    
                    <label for="PlayersOffline">Spelare Offline</label>
                    <asp:TextBox runat="server" id="PlayersOffline" value="<%#: Item.PlayersOffline %>" CssClass="boxes"></asp:TextBox> 
                        <asp:CompareValidator ID="CV_PlayersOffline" runat="server" ErrorMessage="Måste vara mellan 0-200" ControlToValidate="PlayersOffline" Type="Integer" Display="Dynamic" Text="*" ValueToCompare="200" Operator="LessThanEqual"></asp:CompareValidator>
                    
                    <label for="PlayersOnline">Spelare Online</label>
                    <asp:TextBox runat="server" id="PlayersOnline" value="<%#: Item.PlayersOnline %>" CssClass="boxes"></asp:TextBox>
                        <asp:CompareValidator ID="CV_Playeronline" runat="server" ErrorMessage="Måste vara en eller fler siffror..." ControlToValidate="PlayersOnline" Type="Integer" Display="Dynamic" Text="*" Operator="DataTypeCheck" ></asp:CompareValidator>   
                    
                    <label for="Story">Handling</label>
                    <asp:TextBox runat="server" id="Story" TextMode="MultiLine" Columns="45" Rows="5" Text="<%#: (Item.Story).ToString() %>" CssClass="boxes"></asp:TextBox>   
                        <asp:CompareValidator ID="CV_story" runat="server"  ErrorMessage="Måste vara mellan 0-1000 tecken" ControlToValidate="Story" Type="String" Display="Dynamic" Text="*" ValueToCompare="1000" Operator="GreaterThan"></asp:CompareValidator>
                    
                    <label for="CustomNote">Egen notering</label>
                    <asp:TextBox runat="server" id="CustomNote" TextMode="MultiLine" Text="<%#: Item.CustomNote %>" CssClass="boxes"></asp:TextBox>
                        <asp:CompareValidator ID="cv_CustomNote" runat="server"  ErrorMessage="Måste vara mellan 0-2000 tecken" ControlToValidate="Story" Type="String" Display="Dynamic" Text="*" ValueToCompare="2000" Operator="GreaterThan"></asp:CompareValidator>
                </ItemTemplate>
        </asp:FormView>        
        <asp:Button ID="Sendbutton" runat="server" Text="Spara/Uppdatera" OnClick="Sendbutton_Click" Visible="false"/>
        <asp:Button ID="EraseButton" runat="server" Text="Radera spelet" OnClientClick='return confirm("Spelet kommer raderas för alltid, säker?");' OnClick="EraseButton_Click" Visible="false"/>
    </div>
    
    <div id="blackborder">
        <asp:ValidationSummary ID="ValidationSummary" runat="server" />
    </div>

    <h2 id="ListsHeader">Väljs lista</h2>
    <div id="Lists">
        <asp:Repeater ID="ListRepeater" runat="server" ItemType="Spellistaren.model.List" SelectMethod="ListRepeater_GetData">
            <HeaderTemplate>                
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                
                    <li>
                        <asp:HyperLink runat="server" NavigateUrl='<%#: GetRouteUrl("AddOrEdit", null) + "?List=" + Server.UrlEncode((Item.ListID).ToString())%>'  Text='<%#: Item.ListName %>'></asp:HyperLink> <%-- --%>
                    </li>
                
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <h2 id="listContentHeader">granska/ta bort spel i listan</h2>
    <div id="listContent">
        <asp:Button ID="DeleteButton" runat="server" Text="Ta bort valt spel" OnClientClick='return confirm("Säker på att du vill radera spelet från listan?");' OnClick="DeleteButton_Click" Visible="false" />
        <asp:Repeater ID="ListContentRepeater" runat="server" ItemType="Spellistaren.model.Game" SelectMethod="ListContentRepeater_GetData">
            <HeaderTemplate>                
                <ul>
            </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:HyperLink runat="server" NavigateUrl='<%#: GetRouteUrl("AddOrEdit", null)+"?List="+ Request.QueryString["List"] + "&GameID=" + Server.UrlEncode((Item.GameID).ToString())  %>' Text='<%#: Item.GameName %>'></asp:HyperLink>
                        
                    </li>
                </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <h2 id="gameToAddListHeader">Lägg till spel i listan</h2>
    <div runat="server" id="gameToAddList"  Visible="false" >
        <asp:Button ID="addToListButton" runat="server" Text="Lägg till" OnClick="addToListButton_Click" visible="false" />
        <asp:Repeater ID="gameToAddListRepeater" runat="server" ItemType="Spellistaren.model.Game" SelectMethod="GamelistRepeater_GetData" Visible="false">
            <HeaderTemplate>
                
                <ul>
            </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:HyperLink ID="listid" runat="server" Text='<%#: Item.GameName %>' NavigateUrl='<%#: GetRouteUrl("AddOrEdit", null)+"?List="+ Request.QueryString["List"] + "&GameToAddID=" + Server.UrlEncode(Item.GameID.ToString()) %>' ></asp:HyperLink> <%--NavigateUrl='<%#: GetRouteUrl("AddOrEdit", null)+"?List="+ Request.QueryString["List"] + "&GameToAddID=" + Server.UrlEncode(Item.GameID.ToString()) %>' --%>
                        
                    </li>
                </ItemTemplate>
            <FooterTemplate>
                </ul>
                
            </FooterTemplate>
        </asp:Repeater>        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="OutsideContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PHScript" runat="server">
    <script type="text/javascript" src="../scripts/script.js" ></script>
</asp:Content>







<%--<asp:ListView ID="GameDetailRepeater" runat="server"  ItemType="Spellistaren.model.Game"  SelectMethod="GameDetailRepeater_GetData"  Visible="false"> 
            <LayoutTemplate>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" ></asp:PlaceHolder>
            </LayoutTemplate>
                <ItemTemplate>
                    <label for="GameName">Spelets namn</label>
                    <asp:TextBox ID="GameName" placeholder="<%#: Item.GameName %>" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Du måste fylla i ett namn!" Text="*" ControlToValidate="GameName" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator> --denna heter repeater.. men är en listview..  -->
                        

                    <label for="CompanyName">Företag</label>
                    <asp:TextBox runat="server" id="CompanyName" placeholder="<%#: Item.CompanyName %>" ></asp:TextBox>      
                    
                    <label for="ReleaseDate">Utgivningsdatum</label>
                    <asp:TextBox runat="server" id="ReleaseDate" placeholder="<%#: Item.ReleaseDate %>" ></asp:TextBox>   
                    
                    <label for="PlayersOffline">Spelare Offline</label>
                    <asp:TextBox runat="server" id="PlayersOffline" placeholder="<%#: Item.PlayersOffline %>" ></asp:TextBox>   
                    
                    <label for="PlayersOnline">Spelare Online</label>
                    <asp:TextBox runat="server" id="PlayersOnline" placeholder="<%#: Item.PlayersOnline %>" ></asp:TextBox>   
                    
                    <label for="Story">Handling</label>
                    <asp:TextBox runat="server" id="Story" value="<%#: Item.Story %>" ></asp:TextBox>   <-- Sparar ner Storyn i text, den är oftast så lång att man inte vill skriva om hela bara för att ändra något i den.. -->
                    
                    <label for="CustomNote">Egen notering</label>
                    <asp:TextBox runat="server" id="CustomNote" placeholder="<%#: Item.CustomNote %>" ></asp:TextBox>
                    
                </ItemTemplate>

        </asp:ListView> --%>









<%--    <asp:Repeater ID="Repeater1" runat="server" ItemType="Spellistaren.model.Game" SelectMethod="GameDetailRepeater_GetData" Visible="false">
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
                    <asp:TextBox runat="server" id="Story" value="<%#: Item.Story %>" ></asp:TextBox>   <!-- Sparar ner Storyn i text, den är oftast så lång att man inte vill skriva om hela bara för att ändra något i den.. -->
                    
                    <label for="CustomNote">Egen notering</label>
                    <asp:TextBox runat="server" id="CustomNote" placeholder="<%#: Item.CustomNote %>" ></asp:TextBox>
                    
                </ItemTemplate>
            <FooterTemplate>
                
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </ul>
            </FooterTemplate>
        </asp:Repeater> --%>