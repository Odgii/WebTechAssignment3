<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" MasterPageFile="~/MainMaster.master"
    Inherits="WebTechAssignment3.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
    <form id="form1" runat="server">

    <div class="main-menu">
        <asp:LinkButton ID="lbl_home" runat="server" OnClick="homeLink_Click" Text="Home"></asp:LinkButton>
        |
        <asp:LinkButton ID="lbl_currentUser" runat="server" OnClick="userProfile_Click" Text="My Reading History"></asp:LinkButton>
        |
        <asp:LinkButton ID="btn_logout" runat="server" OnClick="btn_logout_Click" Text="Log Out"></asp:LinkButton>
    </div>

    <div align="center">
        <h1 class="bold">User History</h1>
        <asp:Label ID="lbl_error" runat="server" Text="Label"></asp:Label>
        <asp:SqlDataSource ID="DataSource_BorrowedBook" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"></asp:SqlDataSource>
        <br />
        <asp:GridView ID="GridView_BorrowedBooks" runat="server" BackColor="White" BorderColor="White"
            BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None"
            AutoGenerateColumns="False" DataSourceID="DataSource_BorrowedBook" OnRowCommand="GridView_BorrowedBooks_RowCommand"   >
            <Columns>
                <asp:ImageField HeaderText="Image" ControlStyle-Height="100px" ControlStyle-Width="100px"
                    DataImageUrlField="Image">
                </asp:ImageField>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author" />
                <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                <asp:BoundField DataField="ISBN" HeaderText="ISBN" SortExpression="ISBN" />
                <asp:BoundField DataField="BorrowDate" HeaderText="BorrowDate" SortExpression="BorrowDate" />
                <asp:BoundField DataField="ReturnDate" HeaderText="ReturnDate" SortExpression="ReturnDate" />
                 <asp:TemplateField HeaderText="ReturnBook">
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_returnBook" runat="server" CommandName="ReturnBook" CommandArgument='<%# Eval("Title") %>'
                                Text="Return Book"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#594B9C" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#33276A" />
        </asp:GridView>
    </div>
    </form>
</asp:Content>
