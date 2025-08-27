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
        protected void btnAddToSelection_Click(object sender, EventArgs e)
         {
           // Clear existing items in ListBox to avoid duplicates (optional, based on requirements)
           lstSelectedBooks.Items.Clear();

           // Loop through GridView rows to find selected checkboxes
          foreach (GridViewRow row in gvBooks.Rows)
              {
                 CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect.Checked)
                      {
                         // Get ISBN from the first column (index 0 of data cells, since checkbox is a TemplateField)
                          string isbn = row.Cells[1].Text;
                          string bName = row.Cells[2].Text;
                          lstSelectedBooks.Items.Add(new ListItem(isbn + " - " + bName));
                      }
               }


        }
        protected void btnRemoveSelected_Click(object sender, EventArgs e)
        {
         // Remove selected items from ListBox (iterate backwards to avoid index issues)
         for (int i = lstSelectedBooks.Items.Count - 1; i >= 0; i--)
            {
               if (lstSelectedBooks.Items[i].Selected)
                 {
                   lstSelectedBooks.Items.RemoveAt(i);
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
