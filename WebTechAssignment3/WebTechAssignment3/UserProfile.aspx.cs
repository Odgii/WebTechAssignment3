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

           if (Session["loggedUser"] != null)
           {
               lbl_currentUser.Text = "Hi! " + getCurrentUserName(Session["loggedUser"].ToString());
           }

           // string currentUser = "1";
            DataSource_BorrowedBook.SelectCommand = "SELECT Book.BookImgUrl AS [Image], Book.BookName AS Title, Book.BookAuthor AS Author, Book.BookPublishedIn AS [Year], Book.ISBN ,b.BorrowDate , b.ReturnDate  FROM Book INNER JOIN BorrowedBook b ON Book.ID =b.BorrowedBookID where b.BorrowedUserID =" + currentUser ;
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
    }
}