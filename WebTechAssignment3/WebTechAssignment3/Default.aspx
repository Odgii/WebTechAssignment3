<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebTechAssignment3._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource">
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource" runat="server"></asp:SqlDataSource>
            <asp:AccessDataSource ID="AccessDataSource" runat="server">
            </asp:AccessDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
