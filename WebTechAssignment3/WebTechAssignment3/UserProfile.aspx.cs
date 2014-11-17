using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Web.Security;

namespace WebTechAssignment3
{
    public partial class UserProfile : System.Web.UI.Page
    {
        OleDbConnection conn = null;
        OleDbCommand cmd = null;
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|/Database.accdb;Persist Security Info=True";
    
        protected void Page_Load(object sender, EventArgs e)
        {
           string currentUser = getUserID(Session["loggedUser"].ToString());

           if (Session["loggedUser"] != null)
           {
               lbl_currentUser.Text = "Hi! " + getCurrentUserName(Session["loggedUser"].ToString());
           }

           // string currentUser = "1";
            DataSource_BorrowedBook.SelectCommand = "SELECT Book.BookImgUrl AS [Image], Book.BookName AS Title, Book.BookAuthor AS Author, Book.BookPublishedIn AS [Year], Book.ISBN ,b.BorrowDate , b.ReturnDate  FROM Book INNER JOIN BorrowedBook b ON Book.ID =b.BorrowedBookID where b.BorrowedUserID =" + currentUser ;
            setBooksReturned();
        }

        protected string getCurrentUserName(string userEmail)
        {
            conn = new OleDbConnection(connectionString);
            string userName = "";
            try
            {
                conn.Open();
                cmd = new OleDbCommand("Select UserFirstName FROM [User] where UserEmail='" + userEmail + "'", conn);
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

        protected string getUserID(string userEmail)
        {
            string id = null;
            conn = new OleDbConnection(connectionString);
            try
            {
                conn.Open();
                cmd = new OleDbCommand("Select ID FROM [User] where UserEmail='" + userEmail + "'", conn);
                int x = (int)cmd.ExecuteScalar();
                if (x == 1)
                {
                    id = cmd.ExecuteScalar().ToString();
                }
                conn.Close();
            }
            catch
            {
            }
            return id;

        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("LoginPage.aspx", true);
        }

        protected void userProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx", true);
        }

        protected void homeLink_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainPage.aspx", true);
        }

        protected void setBooksReturned() {

            foreach (GridViewRow row in GridView_BorrowedBooks.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string bookTitle = row.Cells[1].Text;
                    if (bookTitle != null && bookIsReturned(bookTitle))
                    {
                     //   lbl_error.Text = bookTitle;
                        LinkButton theButton = (LinkButton)row.FindControl("btn_returnBook");

                        if (theButton != null)
                        {
                            theButton.Enabled = false;
                            theButton.Text = "Returned";
                        }
                    }
                }
            }
            GridView_BorrowedBooks.DataBind();
        }

        protected bool bookIsReturned(string bookTitle)
        {
            conn = new OleDbConnection(connectionString);
            string bookID = "";
            bool isReturned = true;
            try
            {
                conn.Open();
                cmd = new OleDbCommand("Select ID FROM Book where BookName='" + bookTitle + "'", conn);
                int x = (int)cmd.ExecuteScalar();
                if (x != 0)
                {
                    bookID = cmd.ExecuteScalar().ToString();
                }
                OleDbCommand command = new OleDbCommand("Select State from BorrowedBook where BorrowedBookID =" + bookID + "and BorrowedUserID=" + Session["userID"].ToString(), conn);
                int state = (int)cmd.ExecuteScalar();
                if (state != 0)
                {
                    state = int.Parse(cmd.ExecuteScalar().ToString());
                }
                conn.Close();
            }
            catch(Exception e)
            {
                lbl_error.Text = e.Message;
            }
            return isReturned;
        }

        

        protected void GridView_BorrowedBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            conn = new OleDbConnection(connectionString);
            string bookID = null;
            try
            {
                if (e.CommandName == "ReturnBook")
                {
                    string bookTitle = e.CommandArgument.ToString();
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
                    GridView_BorrowedBooks.DataBind();
                }
            }
            catch  { }
        }
    }
}