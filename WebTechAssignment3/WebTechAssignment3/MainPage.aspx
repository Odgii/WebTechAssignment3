<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" MasterPageFile="~/MainMaster.master"
    Inherits="WebTechAssignment3.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="Server">
    <form id="form1" runat="server">
    <div class="main-menu">
        <asp:LinkButton ID="lbl_home" runat="server" OnClick="homeLink_Click" Text="Home"></asp:LinkButton>
        |
        <asp:LinkButton ID="lbl_currentUser" runat="server" OnClick="userProfile_Click" Text="My Reading History"></asp:LinkButton>
        |
        <asp:LinkButton ID="btn_logout" runat="server" OnClick="btn_logout_Click" Text="Log Out"></asp:LinkButton>
    </div>
    <div id="AddBook" runat="server" align="center" class="add-book">
        <h2>
            Add a new Book</h2>
        <asp:Label ID="lbl_bookTitle" runat="server" Text="Book Title:" CssClass="label-add-book" />
        <asp:TextBox ID="txt_bookTitle" runat="server" Width="154px"></asp:TextBox>
        <br />
        <asp:Label ID="lbl_bookAuthor" runat="server" Text="Author:" CssClass="label-add-book"></asp:Label>
        <asp:TextBox ID="txt_bookAuthor" runat="server" Width="154px"></asp:TextBox>
        <br />
        <asp:Label ID="lbl_year" runat="server" Text="Year:" CssClass="label-add-book"></asp:Label>
        <asp:TextBox ID="txt_year" runat="server" Width="154px"></asp:TextBox>
        <asp:RegularExpressionValidator ID="YearValidator" runat="server" ErrorMessage="Only Number!"
            ControlToValidate="txt_year" ValidationExpression="^[0-9]*$*"> </asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="lbl_isbn" runat="server" Text="ISBN:" CssClass="label-add-book"></asp:Label>
        <asp:TextBox ID="txt_isbn" runat="server" Width="154px"></asp:TextBox>
        <asp:RegularExpressionValidator ID="ISBNValidator" runat="server" ErrorMessage="Only Number!"
            ControlToValidate="txt_isbn" ValidationExpression="^[0-9]*$*"> </asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="volume" runat="server" Text="Volume:" CssClass="label-add-book"></asp:Label>
        <asp:TextBox ID="txt_volume" runat="server" Width="154px"></asp:TextBox>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Category:" CssClass="label-add-book"></asp:Label>
        <asp:DropDownList ID="droplist_bookCategory" runat="server" Width="154px" DataSourceID="DataSource_BookCategory"
            DataTextField="Category" DataValueField="ID">
        </asp:DropDownList>
        <asp:SqlDataSource ID="DataSource_BookCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [Category]">
        </asp:SqlDataSource>
        <asp:Button ID="btn_addBook" runat="server" Text="Add Book" OnClick="btn_addBook_Click"
            CssClass="button" />
        <asp:Label ID="lbl_errorAddBook" runat="server" Text=" " />
        <br />
    </div>
    <div class="book-list">
        <h1 class="bold">
            Book list</h1>
        <div id="GridView" align="center">
            <asp:SqlDataSource ID="DataSource_AvailableBook" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT Book.BookImgUrl AS [Image], Book.BookName AS Name, Book.BookAuthor AS Author, Book.BookPublishedIn AS [Year], Book.ISBN, Book.BookEdition AS Volume, Category.Category FROM (Book INNER JOIN Category ON Book.BookCategory = Category.ID) WHERE (Book.ID NOT IN (SELECT BorrowedBookID FROM BorrowedBook WHERE (State = true)))">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="DataSource_AllBook" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT Book.BookImgUrl AS [Image], Book.BookName AS Name, Book.BookAuthor AS Author, Book.BookPublishedIn AS [Year], Book.ISBN, Book.BookEdition AS Volume, Category.Category FROM (Book INNER JOIN Category ON Book.BookCategory = Category.ID)">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="DataSource_BorrowedBook" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT Book.BookImgUrl AS [Image], Book.BookName AS Name, Book.BookAuthor AS Author, Book.BookPublishedIn AS [Year], Book.ISBN, Book.BookEdition AS Volume, Category.Category FROM (Book INNER JOIN Category ON Book.BookCategory = Category.ID) WHERE (Book.ID IN (SELECT BorrowedBookID FROM BorrowedBook WHERE (State = true)))">
            </asp:SqlDataSource>
            <asp:DropDownList ID="BookViewTypes" runat="server" AutoPostBack="true" Width="150px"
                OnSelectedIndexChanged="BookViewTypes_SelectedIndexChanged">
                <asp:ListItem Value="AllBook">All Books</asp:ListItem>
                <asp:ListItem Value="AvailableBook">Available Books</asp:ListItem>
                <asp:ListItem Value="BorrowedBook">Borrowed Books</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:GridView ID="GridView_Book" runat="server" Width="700px" DataSourceID="DataSource_AllBook"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowCommand="GridView_Book_RowCommand">
                <Columns>
                    <asp:ImageField HeaderText="Book Cover" ControlStyle-Height="100px" ControlStyle-Width="100px"
                        DataImageUrlField="Image">
                        <ControlStyle Height="100px" Width="100px"></ControlStyle>
                    </asp:ImageField>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author" />
                    <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
                    <asp:BoundField DataField="ISBN" HeaderText="ISBN" SortExpression="ISBN" />
                    <asp:BoundField DataField="Volume" HeaderText="Volume" SortExpression="Volume" />
                    <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                    <asp:TemplateField HeaderText="BorrowBook">
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_borrowBook" runat="server" CommandName="BorrowBook" CommandArgument='<%# Eval("Name") %>'
                                Text="Borrow Book"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
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
    </div>
    </form>
</asp:Content>
