using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Librarian
{
    public partial class AdminPage : System.Web.UI.Page
    {
        string orderBy = "UserID ASC";
        string remove;
        string con_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bookDB.mdf;Integrated Security=True";
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adap;
        DataSet ds;
        SqlDataReader read;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Only load full table once
            {
                Showtable2();
            }


        }

        public void Showtable()
        {
            try
            {
                conn = new SqlConnection(con_string);

                conn.Open();

                adap = new SqlDataAdapter();

                ds = new DataSet();

                string sql = @"SELECT * FROM tblUsers";

                comm = new SqlCommand(sql, conn);

                adap.SelectCommand = comm;
                adap.Fill(ds);

                GridView2.DataSource = ds;
                GridView2.DataBind();

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Showtable2(string orderBy = "UserID ASC")
        {
            try
            {
                conn = new SqlConnection(con_string);
                conn.Open();

                adap = new SqlDataAdapter();
                ds = new DataSet();

                string sql = $"SELECT * FROM tblUsers ORDER BY {orderBy}";

                comm = new SqlCommand(sql, conn);

                adap.SelectCommand = comm;
                adap.Fill(ds);

                GridView2.DataSource = ds;
                GridView2.DataBind();

                conn.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }


        protected void btnAddLibrarian_Click(object sender, EventArgs e)
        {
            string role = "Librarian";
            decimal fees = 0.0m;
            try
            {
<<<<<<< HEAD
                conn = new SqlConnection(con_string);
=======
                decimal fees = 0;
                string role = "Librarian";
>>>>>>> 6fcb2baea475353c5dfd531a6d745e39fd562f0e

                conn.Open();
<<<<<<< HEAD
=======
                string sql = "INSERT INTO tblUsers (UserName, UserSurname, UserMail, UserPasswd, UserRole, OutstandingFees) " +
                             "VALUES (@UserName, @UserSurname, @UserMail, @UserPasswd, @UserRole, @OutstandingFees)";
                cmd = new SqlCommand(sql, conn);
>>>>>>> 6fcb2baea475353c5dfd531a6d745e39fd562f0e

                string sql = $"INSERT INTO tblUsers(UserName,UserSurname,UserMail,UserPasswd,UserRole,OutstandingFees)Values(@name, @sur,@mail, @pass, @role,@fees)";

<<<<<<< HEAD
                comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@name", txtLibrarianFirstName.Text.Trim());
                comm.Parameters.AddWithValue("@sur", txtLibrarianLastName.Text.Trim());
                comm.Parameters.AddWithValue("@mail", txtLibrarianEmail.Text.Trim());
                comm.Parameters.AddWithValue("@pass", txtLibrarianPassword.Text.Trim());
                comm.Parameters.AddWithValue("@role", role);
                comm.Parameters.AddWithValue("@fees", fees);

                comm.ExecuteNonQuery();

                lblMessage.Text = "Successfully added....";
=======
                int rowsAffected = cmd.ExecuteNonQuery();
                lblMessage.Text = "Librarian added successfully!";
                lblMessage.Visible = true;

>>>>>>> 6fcb2baea475353c5dfd531a6d745e39fd562f0e
                conn.Close();

            }
            catch (Exception ex)
            {
<<<<<<< HEAD
                Console.WriteLine(ex.Message);
            }
            Showtable();

            lblMessage.Text = "Successfully added....";
=======
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
            }
>>>>>>> 6fcb2baea475353c5dfd531a6d745e39fd562f0e
        }

        protected void btnRemoveUser_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD

            try
            {
                conn = new SqlConnection(con_string);

                conn.Open();

                string sql = $"DELETE FROM tblUsers WHERE UserID = @delID";

                comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@delID", txtUserIdDel.Text.Trim());

                adap = new SqlDataAdapter();
=======
            if (lstUsers.SelectedValue == "")
            {
                lblMessage.Text = "Please select a user to delete.";
                lblMessage.Visible = true;
                return;
            }

            try
            {
                conn = new SqlConnection(constr);
                conn.Open();
                string sql = "DELETE FROM tblUsers WHERE UserID = @UserID";
                SqlCommand comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@UserID", lstUsers.SelectedValue);

                int result = comm.ExecuteNonQuery();

                if (result > 0)
                {
                    lblMessage.Text = "User deleted successfully.";
                }
                else
                {
                    lblMessage.Text = "No user found.";
                }

                lblMessage.Visible = true;
                conn.Close();
                loadData();
            }
            catch (SqlException ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
            }
        }
>>>>>>> 6fcb2baea475353c5dfd531a6d745e39fd562f0e

                adap.DeleteCommand = comm;
                adap.DeleteCommand.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Showtable2();
            lblMessage.Text = "User Seccesfully Deleted!!";
        }

          

<<<<<<< HEAD
       
=======
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
            if (ddlUsers.SelectedValue == "")
            {
                lblMessage.Text = "Select a user to update.";
                lblMessage.Visible = true;
                return;
            }

            try
            {
                conn = new SqlConnection(constr);
                conn.Open();

                string sql = "UPDATE tblUsers SET UserName=@UserName, UserSurname=@UserSurname, UserMail=@UserMail " +
                             "WHERE UserID=@UserID";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@UserID", ddlUsers.SelectedValue);
                cmd.Parameters.AddWithValue("@UserName", txtUpdateFirstName.Text);
                cmd.Parameters.AddWithValue("@UserSurname", txtUpdateLastName.Text);
                cmd.Parameters.AddWithValue("@UserMail", txtUpdateEmail.Text);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                    lblMessage.Text = "User details updated successfully.";
                else
                    lblMessage.Text = "Update failed. Check User ID.";

                lblMessage.Visible = true;
                conn.Close();
                loadData();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.Visible = true;
            }
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
>>>>>>> 6fcb2baea475353c5dfd531a6d745e39fd562f0e

        protected void txtSearchUsers_TextChanged(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(con_string);
                conn.Open();

                adap = new SqlDataAdapter();
                ds = new DataSet();

                string sql = "SELECT * FROM tblUsers WHERE UserName LIKE @search OR CAST(UserID AS NVARCHAR) LIKE @search";

                comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@search", "%" + txtSearchUsers.Text.Trim() + "%");

                adap.SelectCommand = comm;
                adap.Fill(ds);

                GridView2.DataSource = ds;
                GridView2.DataBind();

                if (ds.Tables[0].Rows.Count == 0)
                {
                    lblMessage.Text = "No users found.";
                }
                else
                {
                    lblMessage.Text = "";
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }

        }

        protected void ddlSortUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sort = ddlSortUsers.SelectedValue;
            conn = new SqlConnection(constr);
            string sql = "SELECT * FROM tblUsers ORDER BY ";

<<<<<<< HEAD

            if (ddlSortUsers.SelectedValue == "ID ASC")
            {
                orderBy = "UserID ASC";
            }
            else if (ddlSortUsers.SelectedValue == "ID DESC")
            {
                orderBy = "UserID DESC";
            }
            else if (ddlSortUsers.SelectedValue == "Name ASC")
            {
                orderBy = "UserName ASC";
            }
            else if (ddlSortUsers.SelectedValue == "Name DESC")
            {
                orderBy = "UserName DESC";
            }

            Showtable2(orderBy);
=======
            if (sort.Contains("Name"))
                sql += "UserName " + (sort.Contains("DESC") ? "DESC" : "ASC");
            else
                sql += "UserID " + (sort.Contains("DESC") ? "DESC" : "ASC");

            SqlDataAdapter ad = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);

            gvUsers.DataSource = dt;
            gvUsers.DataBind();
>>>>>>> 6fcb2baea475353c5dfd531a6d745e39fd562f0e
        }

        protected void bntUpdateUser_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(con_string);
                conn.Open();
                

                string sql;

                if (radEmail.Checked)
                {
                    sql = "UPDATE tblUsers SET UserMail = @NewValue WHERE UserID = @UserID";
                    comm = new SqlCommand(sql, conn);



                    comm.Parameters.AddWithValue("@NewValue", txtUpdate.Text.Trim());

                }
                else if (radLast.Checked)
                {
                    sql = "UPDATE tblUsers SET UserSurname = @NewValue WHERE UserID = @UserID";
                }
                else if (radPass.Checked)
                {
                    sql = "UPDATE tblUsers SET UserPasswd = @NewValue WHERE UserID = @UserID";
                }
                else
                {
                    Label1.Text = "Please select what you want to update.";
                    return;
                }

                comm = new SqlCommand(sql, conn);



                comm.Parameters.AddWithValue("@UserID", txtUserID.Text.Trim());


                int row = comm.ExecuteNonQuery();
                conn.Close();
                if (row > 0)
                {
                    Console.WriteLine("Update successful.");
                }
                else
                {
                    Console.WriteLine("No record found to update.");
                }




                Showtable2(); // Refresh grid
            }
            catch (Exception ex)
            {
                Label1.Text = "Error: " + ex.Message;
            }
        
    }
    }
}