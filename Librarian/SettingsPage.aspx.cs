using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Librarian
{
    public partial class SettingsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //display data
            
            string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionStringName"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT IssueDate, ReturnDate, Amount, Status FROM tblBorrow WHERE UserID = @UserID";
        
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Replace with the actual user ID from session or authentication
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]); 
        
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvFees.DataSource = dt;
                        gvFees.DataBind();
                    }
                }
            }
        }
        


        protected void btnUpdateEmail_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResetPasswordPage.aspx");
        }

        protected void btnDeleteAccount_Click(object sender, EventArgs e)
        {

        }

        
    }
}
