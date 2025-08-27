using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Librarian
{
    public partial class SettingsPage : System.Web.UI.Page
    {
        //CONNECTION STRING using DataDirectory 
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bookDB.mdf;Integrated Security=True";

        //  PAGE LOAD: Load borrowed books for current user 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBorrowedBooks();
            }
        }

        //Load all borrowed books where UserID matches session
        private void LoadBorrowedBooks()
        {
           
            if (Session["UserID"] == null)
            {
                lblMessage.Text = "❌ You are not logged in.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string userID = Session["UserID"].ToString();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                     // Get all borrowed books for this user
                    string query = @"
                        SELECT 
                            ISBN, 
                            BookTitle, 
                            BookAuthor, 
                            IssueDate, 
                            ReturnDate, 
                            Status 
                        FROM tblBorrowed 
                        WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Use parameters to prevent SQL injection (best practice)
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                lblMessage.Text = "📘 You haven't borrowed any books yet.";
                            }
                            else
                            {
                                lblMessage.Text = $"✅ You have borrowed {dt.Rows.Count} book(s).";
                            }

                            // Bind data to GridView
                            gvBorrowed.DataSource = dt;
                            gvBorrowed.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //
                lblMessage.Text = "⚠️ An error occurred: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        //  Redirect to change password page 
        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResetPasswordPage.aspx");
        }

        //  Delete account with confirmation 
        protected void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                lblMessage.Text = "❌ Session expired. Please log in again.";
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string deleteQuery = "DELETE FROM tblUsers WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Session.Clear();
                            Session.Abandon();
                            Response.Redirect("LoginPage.aspx?msg=AccountDeleted");
                        }
                        else
                        {
                            lblMessage.Text = "❌ No account found with this ID.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "⚠️ Error deleting account: " + ex.Message;
            }
        }
    }
}
