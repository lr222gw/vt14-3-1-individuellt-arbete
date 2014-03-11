<%@ Page Title="" Language="C#" MasterPageFile="~/pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Spellistaren.pages.Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PHContent" runat="server">
    <asp:TextBox ID="b1" runat="server"></asp:TextBox>
    <asp:TextBox ID="b2" runat="server"></asp:TextBox>
    <asp:Button ID="Button" runat="server" Text="send"  OnClick="Button_Click" />
</asp:Content>

