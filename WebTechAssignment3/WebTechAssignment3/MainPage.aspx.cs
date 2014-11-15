using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace WebTechAssignment3
{
    public partial class Main : System.Web.UI.Page
    {
        string currentUserEmail = "user@gmail.com";
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
    }
}