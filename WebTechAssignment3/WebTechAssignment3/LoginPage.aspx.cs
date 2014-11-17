using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Web.Security;

namespace WebTechAssignment3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        OleDbConnection conn = null;
        OleDbCommand cmd = null;
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|/Database.accdb;Persist Security Info=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_error.Visible = false;
            lbl_errorSignup.Visible = false;
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            if (isRegistered())
            {
                Session.Add("isAuth", "yes");
                FormsAuthentication.RedirectFromLoginPage(Session["loggedUser"].ToString(), false);
                // Response.Redirect("MainPage.aspx", true);
            }
            else
            {
                lbl_error.Visible = true;
                lbl_error.Text = "Wrong email or password";
            }
        }

        protected bool isRegistered()
        {
            conn = new OleDbConnection(connectionString);
            try
            {
                conn.Open();
                cmd = new OleDbCommand("Select ID FROM [User] where UserEmail='" + txt_emailSignin.Text + "'" + "and UserPassword='" + txt_passwordSignin.Text + "'", conn);
                string userID = cmd.ExecuteScalar().ToString();
                if (userID != "0")
                {
                    Session["loggedUser"] = txt_emailSignin.Text;
                    Session["userID"] = userID;
                    return true;
                }
                conn.Close();

            }
            catch 
            {
                //lbl_error.Text = e.Message;
            }
            return false;
        }

        protected bool accountExists()
        {
            conn = new OleDbConnection(connectionString);
            bool ret = false;
            try
            {
                conn.Open();
                cmd = new OleDbCommand("Select ID FROM [User] where UserEmail='" + txt_emailSignup.Text + "'", conn);
                string userID = cmd.ExecuteScalar().ToString();
                if (userID != "0")
                {
                    ret = true;
                }

            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }
            return ret;
        }

        protected void btn_signup_Click(object sender, EventArgs e)
        {
            if (accountExists() == true)
            {
                lbl_errorSignup.Visible = true;
                lbl_errorSignup.Text = "E-Mail already registered!";
            }
            else
            {
                OleDbConnection connection = new OleDbConnection(connectionString);
                try
                {
                    connection.Open();
                    OleDbCommand insert = new OleDbCommand();
                    insert.Connection = connection;

                    insert.CommandType = CommandType.Text;
                    insert.CommandText = "insert into [User] ([UserFirstName],[UserLastName],[UserEmail],[UserPassword],[UserBirthDate]) values (?,?,?,?,?)";
                    insert.Parameters.Add(new OleDbParameter("UserFirstName", txt_firstname.Text));
                    insert.Parameters.Add(new OleDbParameter("UserLastName", txt_lastname.Text));
                    insert.Parameters.Add(new OleDbParameter("UserEmail", txt_emailSignup.Text));
                    insert.Parameters.Add(new OleDbParameter("UserPassword", txt_passwordSignup.Text));
                    insert.Parameters.Add(new OleDbParameter("UserBirthDate", txt_birthdate.Text));
                    insert.ExecuteNonQuery();
                    lbl_errorSignup.Visible = true;
                    lbl_errorSignup.Text = "Successfully registered! You can sign in now.";
                }
                catch
                {
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        
    }
}