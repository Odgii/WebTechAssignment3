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
    public partial class WebForm1 : System.Web.UI.Page
    {
        OleDbConnection conn = null;
        OleDbCommand cmd = null;
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|/Database.accdb;Persist Security Info=True";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            if (isRegistered())
            {
                Session.Add("isAuth", "yes");
                Response.Redirect("MainPage.aspx", true);
            }
            else
            {
                lbl_error.Text = "Wrong email or password";
            }
        }

        protected bool isRegistered()
        {
            conn = new OleDbConnection(connectionString);
            try
            {
                conn.Open();
                cmd = new OleDbCommand("Select count(*) FROM [User] where UserEmail='" + txt_emailSignin.Text + "'" + "and UserPassword='" + txt_passwordSignin.Text + "'", conn);
                int x = (int)cmd.ExecuteScalar();
                lbl_errorSignup.Text = x.ToString();
                if (x == 1)
                {
                    Session["loggedUser"] = txt_emailSignin.Text;
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

        protected void btn_signup_Click(object sender, EventArgs e)
        {
            if (isRegistered() == true)
            {
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
                    insert.Parameters.Add(new OleDbParameter("UserFirstName",txt_firstname.Text));
                    insert.Parameters.Add(new OleDbParameter("UserLastName",txt_lastname.Text));
                    insert.Parameters.Add(new OleDbParameter("UserEmail",txt_emailSignup.Text));
                    insert.Parameters.Add(new OleDbParameter("UserPassword",txt_passwordSignup.Text));
                    insert.Parameters.Add(new OleDbParameter("UserBirthDate",txt_birthdate.Text));
                    insert.ExecuteNonQuery();
                    lbl_errorSignup.Text = "Successfully registered!";
                    connection.Close();
                    }      
            catch {
            }
            }
            
            

        }
        
    }
}