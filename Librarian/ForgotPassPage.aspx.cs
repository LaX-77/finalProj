using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Librarian
{
    public partial class ForgotPassPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Visible = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {


            if (Page.IsValid)
            {
                string email = txtEmail.Text.Trim();

                // Validate inputs
                if (string.IsNullOrEmpty(email))
                {
                    ShowMessage("Email field cannot be empty.", false);
                    return;
                }
                else if (!IsValidEmail(email))
                {
                    ShowMessage("Please provide a valid email address.", false);
                    return;
                }

                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\KGAUGELO\\OneDrive\\Attachments\\LMS2.1\\Librarian\\App_Data\\bookDB.mdf;Integrated Security=True";
                string newPassword = GenerateRandomPassword();

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT COUNT(*) FROM tblUsers WHERE UserMail = @UserMail";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserMail", email);
                            int count = (int)cmd.ExecuteScalar();
                            if (count == 0)
                            {
                                ShowMessage("No user found with the provided email.", false);
                                return;
                            }
                        }

                        // Update password (hashed)
                        string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                        string updateQuery = "UPDATE tblUsers SET UserPasswd = @UserPasswd WHERE UserMail = @UserMail";
                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserPasswd", newPasswordHash);
                            cmd.Parameters.AddWithValue("@UserMail", email);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Send email with temporary password
                    bool emailSent = SendPasswordEmail(email, newPassword);
                    if (emailSent)
                    {
                        ShowMessage("A new password has been sent to your email.", true);
                    }
                    else
                    {
                        ShowMessage("Failed to send email. Please try again later.", false);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("An error occurred: " + ex.Message, false);
                }
            }

        }

            private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private string GenerateRandomPassword(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private bool SendPasswordEmail(string toEmail, string newPassword)
        {
            try
            {
                SmtpClient client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(
                        System.Configuration.ConfigurationManager.AppSettings["SmtpUser"],
                        System.Configuration.ConfigurationManager.AppSettings["SmtpPass"])
                };

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["SmtpUser"], "Bokamoso Library System"),
                    Subject = "Your New Password",
                    Body = $"Your new password for Library Number  is: {newPassword}\n\nPlease log in and change your password immediately.\n\nBest regards,\nBokamoso Library System",
                    IsBodyHtml = false
                };
                mail.To.Add(toEmail);

                client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Email error: " + ex.Message);
                return false;
            }
        }


        private void ShowMessage(string message, bool isSuccess)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = isSuccess ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            lblMessage.Visible = true;
        }


    }
    }
