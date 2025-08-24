using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCrypt.Net;


namespace Librarian
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is logged in
           if (Session["UserID"] == null)
            {
               // ShowMessage("Please log in to reset your password.", false);
                Response.Redirect("LoginPage.aspx");
           }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";


            if ( string.IsNullOrEmpty(txtCurrent.Text) || string.IsNullOrEmpty(txtNew.Text) || string.IsNullOrEmpty(txtConfirm.Text))
            {
                lblMessage.Text = "All fields are required.";
                return;
            }
            else if (txtNew.Text != txtConfirm.Text)
            {
                lblMessage.Text = "Passwords do not match.";
                return;
            }
            else
            {
                lblMessage.Text = "Password reset successfully!";

            }

            string newPassword = txtNew.Text;

            // Validate password strength
            if (!IsPasswordValid(newPassword))
            {
                ShowMessage("Password must be at least 8 characters with uppercase, lowercase, number, and special character.", false);
                return;
            }

            // Get UserID from session
            if (Session["UserID"] == null)
            {
                ShowMessage(" DO NOT FORGET YOUR NEW PASSWORD.", false);
                return;
            }
            int userId = (int)Session["UserID"];


            string connectionString = " Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\KGAUGELO\\OneDrive\\Attachments\\LMS2.1\\Librarian\\App_Data\\bookDB.mdf;Integrated Security=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Verify current password
                    string query = "SELECT UserPasswd FROM tblUsers WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        string storedHash = (string)cmd.ExecuteScalar();

                        if (storedHash == null || !BCrypt.Net.BCrypt.Verify(txtCurrent.Text, storedHash))
                        {
                            ShowMessage("Invalid current password.", false);
                            return;
                        }
                    }

                    // Update password (hashed)
                    string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    string updateQuery = "UPDATE tblUsers SET UserPasswd = @NewPassword WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@NewPassword", newPasswordHash);
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            ShowMessage("Password reset successfully!", true);
                        }
                        else
                        {
                            ShowMessage("Failed to update password.", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("An error occurred: " + ex.Message, false);
            }
        }

        private bool IsPasswordValid(string password)
        {
            // At least 8 characters, with uppercase, lowercase, number, and special character
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
            return regex.IsMatch(password);
        }

        private void ShowMessage(string message, bool isSuccess)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = isSuccess ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            lblMessage.Visible = true;
        }
    }
}