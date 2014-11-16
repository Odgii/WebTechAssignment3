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
            }
            if (BookViewTypes.SelectedValue == "AvailableBook")
            {
                GridView_Book.DataSourceID = "DataSource_AvailableBook";
                GridView_Book.DataBind();
            }
            if (BookViewTypes.SelectedValue == "BorrowedBook") 
            {
                GridView_Book.DataSourceID = "DataSource_BorrowedBook";
                GridView_Book.DataBind();
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

       
    }
}