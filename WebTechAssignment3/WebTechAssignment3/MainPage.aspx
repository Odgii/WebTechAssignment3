<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="WebTechAssignment3.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id = "LoginInformation" align = "right">
            <asp:Label ID = "lbl_currentUser" runat ="server" Text ="CurrentUser" /> 
            <asp:LinkButton ID="btn_logout" runat="server" onclick="btn_logout_Click" >Log out</asp:LinkButton>

    </div>
    <div id = "GridView" align = "center">
        <asp:SqlDataSource ID="DataSource_AvailableBook" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            
            
            SelectCommand="SELECT Book.BookName AS Name, Book.BookAuthor AS Author, Book.BookPublishedIn AS [Year], Book.ISBN, Book.BookEdition AS Volume, Category.Category FROM (Book INNER JOIN Category ON Book.BookCategory = Category.ID) WHERE (Book.ID NOT IN (SELECT BorrowedBookID FROM BorrowedBook WHERE (State = false)))">
        </asp:SqlDataSource>
                <asp:SqlDataSource ID="DataSource_AllBook" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="SELECT Book.BookName AS Name, Book.BookAuthor AS Author, Book.BookPublishedIn AS [Year], Book.ISBN, Book.BookEdition AS Volume, Category.Category FROM (Book INNER JOIN Category ON Book.BookCategory = Category.ID)">
        </asp:SqlDataSource>
                <asp:SqlDataSource ID="DataSource_BorrowedBook" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
            SelectCommand="SELECT Book.BookName AS Name, Book.BookAuthor AS Author, Book.BookPublishedIn AS [Year], Book.ISBN, Book.BookEdition AS Volume, Category.Category FROM (Book INNER JOIN Category ON Book.BookCategory = Category.ID) WHERE (Book.ID NOT IN (SELECT BorrowedBookID FROM BorrowedBook WHERE (State = true)))">
        </asp:SqlDataSource>
        <br/>
        <br/>
        <asp:DropDownList ID="BookViewTypes" runat="server" AutoPostBack="true" Width = "150px"
            onselectedindexchanged="BookViewTypes_SelectedIndexChanged">
                    <asp:ListItem Value="AllBook">All Book</asp:ListItem>
                    <asp:ListItem Value="AvailableBook">Available Book</asp:ListItem>
                    <asp:ListItem Value="BorrowedBook">Borrowed Book</asp:ListItem>
        </asp:DropDownList>
        <br />
                <asp:GridView ID="GridView_Book" runat="server" 
            DataSourceID="DataSource_AllBook" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Author" HeaderText="Author" 
                            SortExpression="Author" />
                        <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                        <asp:BoundField DataField="ISBN" HeaderText="ISBN" SortExpression="ISBN" />
                        <asp:BoundField DataField="Volume" HeaderText="Volume" 
                            SortExpression="Volume" />
                        <asp:BoundField DataField="Category" HeaderText="Category" 
                            SortExpression="Category" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
    </div>
    </form>
</body>
</html>
