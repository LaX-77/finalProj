using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Librarian
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:|DataDirectoty|\bookDB.mdf;Integrated Security=True";
        SqlConnection conn;
        SqlDataAdapter adapter;
        DataSet ds;
        SqlCommand cmd;
        private void loadData()
        {
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                adapter = new SqlDataAdapter();
                ds = new DataSet();
                string sql = "SELECT * FROM tblUsers";
                cmd = new SqlCommand(sql, conn);
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "tblUsers");
                lstUsers.DataSource = ds;
                //lstUsers.DataBind;
                conn.Close();
            }
            catch (SqlException ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void btnAddLibrarian_Click(object sender, EventArgs e)
        {
            try
            {
                decimal fees = 0;
                String role = "Librarian";

                conn = new SqlConnection(constr);
                conn.Open();
                string sql = "INSERT INTO tblUsers (UserName, UserSurname, UserMail, UserPasswd, UserRole, OutstandingFees) VALUES (@UserName, @UserSurname, @UserMail, @UserPasswd, @UserRole, @OutstandingFees)";
                cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@UserName", txtLibrarianFirstName.Text);
                cmd.Parameters.AddWithValue("@UserSurname", txtLibrarianLastName.Text);
                cmd.Parameters.AddWithValue("@UserMail", txtLibrarianEmail.Text);
                cmd.Parameters.AddWithValue("@UserPasswd", txtLibrarianPassword.Text);
                cmd.Parameters.AddWithValue("@UserRole", role);
                cmd.Parameters.AddWithValue("@OutstandingFees", fees);



                int rowsAffected = cmd.ExecuteNonQuery();
                lblMessage.Text = "Added at row: " + rowsAffected;
                

                conn.Close();
                loadData();
            }
            catch (SqlException ex)
            {
                lblMessage.Text = ex.Message;
            }
           
        }
    

        protected void btnRemoveUser_Click(object sender, EventArgs e)
        {
           
            conn = new SqlConnection(constr);
            
            try
            {

                conn.Open();
                string sql = "DELETE FROM Vehicles WHERE UserID = @UserID";
                SqlCommand comm = new SqlCommand(sql, conn);
                //comm.Parameters.AddWithValue("@UserID", txtLibrarianId.Text);
                int result = comm.ExecuteNonQuery();

                if (result > 0)
                {
                    lblMessage.Text ="User deleted successfully.";
                    conn.Close();
                }
                else
                {
                    lblMessage.Text = "Select user to Delete";
                }

            }
            catch (SqlException ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        

        string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:|DataDirectoty|\bookDB.mdf;Integrated Security=True";
        SqlConnection conn;
        SqlDataAdapter adapter;
        DataSet ds;
        SqlCommand cmd;
        private void loadData()
        {
            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                adapter = new SqlDataAdapter();
                ds = new DataSet();
                string sql = "SELECT * FROM tblUsers";
                cmd = new SqlCommand(sql, conn);
                adapter.SelectCommand = cmd;
                adapter.Fill(ds, "tblUsers");
                lstUsers.DataSource = ds;
                //lstUsers.DataBind;
                conn.Close();
            }
            catch (SqlException ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void btnAddLibrarian_Click(object sender, EventArgs e)
        {
            try
            {
                decimal fees = 0;
                String role = "Librarian";

                conn = new SqlConnection(constr);
                conn.Open();
                string sql = "INSERT INTO tblUsers (UserName, UserSurname, UserMail, UserPasswd, UserRole, OutstandingFees) VALUES (@UserName, @UserSurname, @UserMail, @UserPasswd, @UserRole, @OutstandingFees)";
                cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@UserName", txtLibrarianFirstName.Text);
                cmd.Parameters.AddWithValue("@UserSurname", txtLibrarianLastName.Text);
                cmd.Parameters.AddWithValue("@UserMail", txtLibrarianEmail.Text);
                cmd.Parameters.AddWithValue("@UserPasswd", txtLibrarianPassword.Text);
                cmd.Parameters.AddWithValue("@UserRole", role);
                cmd.Parameters.AddWithValue("@OutstandingFees", fees);



                int rowsAffected = cmd.ExecuteNonQuery();
                lblMessage.Text = "Added at row: " + rowsAffected;
                

                conn.Close();
                loadData();
            }
            catch (SqlException ex)
            {
                lblMessage.Text = ex.Message;
            }
           
        }
    

        protected void btnRemoveUser_Click(object sender, EventArgs e)
        {
           
            conn = new SqlConnection(constr);
            
            try
            {

                conn.Open();
                string sql = "DELETE FROM Vehicles WHERE UserID = @UserID";
                SqlCommand comm = new SqlCommand(sql, conn);
                //comm.Parameters.AddWithValue("@UserID", txtLibrarianId.Text);
                int result = comm.ExecuteNonQuery();

                if (result > 0)
                {
                    lblMessage.Text ="User deleted successfully.";
                    conn.Close();
                }
                else
                {
                    lblMessage.Text = "Select user to Delete";
                }

            }
            catch (SqlException ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {

        }

        protected void ddlRemoveType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            string userId = txtUserID.Text.Trim();
            string newValue = txtUpdate.Text.Trim();
        
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(newValue))
            {
                lblMessage.Text = "User ID and new value cannot be empty.";
                lblMessage.Visible = true;
                return;
            }
        
            string columnToUpdate = "";
        
            if (radFirst.Checked)
                columnToUpdate = "UserName";
            else if (radLast.Checked)
                columnToUpdate = "UserSurname";
            else if (radEmail.Checked)
                columnToUpdate = "UserMail";
            else if (radPass.Checked)
                columnToUpdate = "UserPasswd";
            else
            {
                lblMessage.Text = "Please select a field to update.";
                lblMessage.Visible = true;
                return;
            }
        
            string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
        
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = $"UPDATE Users SET {columnToUpdate} = @newValue WHERE Id = @userId";
        
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@newValue", newValue);
                    cmd.Parameters.AddWithValue("@userId", userId);
        
                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
        
                        if (rowsAffected > 0)
                        {
                            lblMessage.Text = "User details updated successfully!";
                            lblMessage.CssClass = "message success";
                        }
                        else
                        {
                            lblMessage.Text = "No user found with the specified ID.";
                            lblMessage.CssClass = "message error";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                        lblMessage.CssClass = "message error";
                    }
                    finally
                    {
                        lblMessage.Visible = true;
                    }
                }
            }
        }

        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public DataTable SearchBooks(string query)
        {
            String conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bookDB.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(conStr);
            string sql = "SELECT * FROM tblUsers WHERE UserID LIKE @id OR UserName LIKE @name OR UserSurname LIKE @sur OR UserMail LIKE @mail";
            SqlCommand cmd = new SqlCommand(sql, conn);





            conn.Open();
            cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", "%" + query + "%");
            cmd.Parameters.AddWithValue("@name", "%" + query + "%");
            cmd.Parameters.AddWithValue("@sur", "%" + query + "%");
            cmd.Parameters.AddWithValue("@mail", "%" + query + "%");

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            conn.Close();
            return dt;


        }

        protected void txtSearchUsers_TextChanged(object sender, EventArgs e)
        {
            string query = txtSearchUsers.Text.Trim();

            if (string.IsNullOrEmpty(query))
            {
                //dgViewBooks.DataSource = null;
                //dgViewBooks.DataBind();
                gvUsers.DataSource = null;
                gvUsers.DataBind();
               // lblHeadings.Text = ""; // Clear headings
                return;
            }

            DataTable results = SearchBooks(query); // Your method to fetch filtered data

            // Add a new column to the DataTable for formatted display
            //results.Columns.Add("FormattedText", typeof(string));
            //foreach (DataRow row in results.Rows)
            //{
            //    // Format the text to align columns (adjust spacing as needed)
            //    row["FormattedText"] = $"{row["ISBN"],-10}    | {row["BookTitle"],-30} by {row["BookAuthor"],-20} {row["Year"],-4}, EDITION: {row["BookEdition"],-11}";
            //}

            // Bind to ListBox
            gvUsers.DataSource = results;
            gvUsers.DataBind();

            // Set column headings above the ListBox
           // lblHeadings.Text = "<b>ISBN            | Book Title                     | Author                | Edition   | Year</b>";


        }

        protected void ddlSortUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
