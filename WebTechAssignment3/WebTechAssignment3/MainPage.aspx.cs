using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

namespace WebTechAssignment3
{
    public partial class Main : System.Web.UI.Page
    {
        OleDbConnection conn = null;
        OleDbCommand cmd = null;
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|/Database.accdb;Persist Security Info=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedUser"] != null)
            {
                lbl_currentUser.Text = "Hi! " + getCurrentUserName(Session["loggedUser"].ToString());
            }
            foreach (GridViewRow row in GridView_Book.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string bookTitle = row.Cells[1].Text;
                    if (bookTitle != null && bookIsBorrowed(bookTitle) == true)
                    {

                        LinkButton theButton = (LinkButton)row.FindControl("btn_borrowBook");

                        if (theButton != null)
                        {
                            theButton.Enabled = false;
                            theButton.Text = "Borrowed";
                        }
                    }
                }
            } 
            
        }

        protected string getCurrentUserName(string userEmail)
        {
            conn = new OleDbConnection(connectionString);
            string userName="";
            try
            {
                conn.Open();
                cmd = new OleDbCommand("Select UserFirstName FROM [User] where UserEmail='" +userEmail + "'", conn);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    userName = reader.GetString(0);
                }
                conn.Close();
            }
            catch
            {
                //lbl_error.Text = e.Message;
            }
            return userName;
        }

        protected void BookViewTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BookViewTypes.SelectedValue == "AllBook")
            {
                GridView_Book.DataSourceID = "DataSource_AllBook";
                GridView_Book.DataBind();   
                //disable the borrowed books
                foreach (GridViewRow row in GridView_Book.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        string bookTitle = row.Cells[1].Text;                      
                        if (bookTitle != null && bookIsBorrowed(bookTitle) == true)
                        {

                            LinkButton theButton = (LinkButton)row.FindControl("btn_borrowBook");

                            if (theButton != null)
                            {
                                theButton.Enabled = false;
                                theButton.Text = "Borrowed";
                            }
                        }
                    }
                } 
            }
            if (BookViewTypes.SelectedValue == "AvailableBook")
            {
                GridView_Book.DataSourceID = "DataSource_AvailableBook";
                GridView_Book.DataBind();
                //for all available book make enable borrow book button
                foreach (GridViewRow row in GridView_Book.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        LinkButton theLinkButton = (LinkButton)row.FindControl("btn_borrowBook");

                        if (theLinkButton != null)
                        {
                            theLinkButton.Enabled = true;
                        }
                    }
                } 
            }
            if (BookViewTypes.SelectedValue == "BorrowedBook") 
            {
                GridView_Book.DataSourceID = "DataSource_BorrowedBook";
                GridView_Book.DataBind();
                //for all borrowed book show that its borrowed and disable the borrow book button
                foreach (GridViewRow row in GridView_Book.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        LinkButton theButton = (LinkButton)row.FindControl("btn_borrowBook");

                        if (theButton != null)
                        {
                            theButton.Enabled = false;
                            theButton.Text = "Borrowed";
                        }
                    }
                } 
            }          
            }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LoginPage.aspx", true);
        }

        protected void btn_addBook_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            try
            {
                connection.Open();
                OleDbCommand insert = new OleDbCommand();
                insert.Connection = connection;

                insert.CommandType = CommandType.Text;
                insert.CommandText = "insert into Book ([BookName],[BookAuthor],[BookPublishedIn],[ISBN],[BookEdition],[BookCategory]) values (?,?,?,?,?,?)";
                insert.Parameters.Add(new OleDbParameter("BookName", txt_bookTitle.Text));
                insert.Parameters.Add(new OleDbParameter("BookAuthor", txt_bookAuthor.Text));
                insert.Parameters.Add(new OleDbParameter("BookPublishedIn", int.Parse(txt_year.Text)));
                insert.Parameters.Add(new OleDbParameter("ISBN",int.Parse(txt_isbn.Text)));
                insert.Parameters.Add(new OleDbParameter("BookEdition", txt_volume.Text));
                insert.Parameters.Add(new OleDbParameter("BookCategory",int.Parse( droplist_bookCategory.SelectedValue)));
                insert.ExecuteNonQuery();
                lbl_errorAddBook.Text = "Successfully registered!";
                connection.Close();
                GridView_Book.DataBind();
            }
            catch
            {
            }
        }

        protected void userProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx", true);
        }

        protected bool bookIsBorrowed(string bookTitle)
        {
            conn = new OleDbConnection(connectionString);
            string bookID = null;
            try
            {
                conn.Open();
                cmd = new OleDbCommand("Select ID FROM Book where BookName='" + bookTitle + "'", conn);
                int x = (int)cmd.ExecuteScalar();
                if (x != 0 )
                {
                    bookID = cmd.ExecuteScalar().ToString();
                }
                OleDbCommand comd = new OleDbCommand("Select count(*) from BorrowedBook where BorrowedBookID=" + bookID + " and State=true", conn);
                int res = (int)comd.ExecuteScalar();
                if (res == 1)
                {
                    return true;
                }
                conn.Close();

            }
            catch
            {
            }
            return false;
        }

        //add books to user's borrowed book list

        protected void GridView_Book_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            conn = new OleDbConnection(connectionString);
            string bookID = null;
            try
            {
                if (e.CommandName == "BorrowBook")
                {
                    string bookTitle= e.CommandArgument.ToString();
                    conn.Open();
                    cmd = new OleDbCommand("Select ID FROM Book where BookName='" + bookTitle + "'", conn);
                    int x = (int)cmd.ExecuteScalar();
                    if (x != 0)
                    {
                        bookID = cmd.ExecuteScalar().ToString();
                    }
                    OleDbCommand insertIntoBorrowedBooks = new OleDbCommand("insert into BorrowedBook([BorrowedBookID],[BorrowedUserID],[State],[BorrowDate]) values (?,?,?,?)", conn);
                    insertIntoBorrowedBooks.Parameters.Add(new OleDbParameter("BorrowedBookID", int.Parse(bookID)));
                    insertIntoBorrowedBooks.Parameters.Add(new OleDbParameter("BorrowedUserID", int.Parse(Session["userID"].ToString())));
                    insertIntoBorrowedBooks.Parameters.Add(new OleDbParameter("State", OleDbType.Boolean)).Value = true;
                    insertIntoBorrowedBooks.Parameters.Add(new OleDbParameter("BorrowDate", OleDbType.Date)).Value = System.DateTime.Now;
                    insertIntoBorrowedBooks.ExecuteNonQuery();
                    conn.Close();
                    GridView_Book.DataBind();
                }
            }
            catch (Exception exp1)
            {

              Label2.Text = exp1.Message;
            }
        }
    }
}