using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

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

           // string currentUser = "1";
            DataSource_BorrowedBook.SelectCommand = "SELECT Book.BookImgUrl AS [Image], Book.BookName AS Title, Book.BookAuthor AS Author, Book.BookPublishedIn AS [Year], Book.ISBN ,b.BorrowDate , b.ReturnDate  FROM Book INNER JOIN BorrowedBook b ON Book.ID =b.BorrowedBookID where b.BorrowedUserID =" + currentUser ;
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
    }
}