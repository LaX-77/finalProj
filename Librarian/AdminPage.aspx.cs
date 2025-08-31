using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BCrypt.Net;

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
                Showtable("UserID ASC", gvUsers);
            }


        }

        public void Showtable(string id)
        {
            try
            {
                conn = new SqlConnection(con_string);

                conn.Open();

                adap = new SqlDataAdapter();

                ds = new DataSet();

                string sql = @"SELECT * FROM tblUsers WHERE UserID=@id";

                comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@id", Convert.ToInt64(id));

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

        public void Showtable(string orderBy, GridView gv)
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

                gv.DataSource = ds;
                gv.DataBind();

                conn.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }


        protected void btnAddLibAdm_Click(object sender, EventArgs e)
        {
            string role;
            if (radAdmin.Checked || radLib.Checked)
            {
                role = radLib.Checked ? radLib.Text : radAdmin.Text;


                decimal fees = 0.0m;
                try
                {
                    conn = new SqlConnection(con_string);

                    conn.Open();

                    //string sql = $"INSERT INTO tblUsers(UserName,UserSurname,UserMail,UserPasswd,UserRole,OutstandingFees)Values('{txtLibrarianFirstName.Text}','{txtLibrarianLastName.Text}','{txtLibrarianEmail.Text}','{txtLibrarianPassword.Text}','{role}','{fees}')";
                    string sql = "INSERT INTO tblUsers (UserName, UserSurname, UserMail, UserPasswd, UserRole, OutstandingFees) VALUES (@UserName, @UserSurname, @UserMail, @UserPasswd, @UserRole, @OutstandingFees)";
                    comm = new SqlCommand(sql, conn);

                    comm.Parameters.AddWithValue("@UserName", txtLibrarianFirstName.Text);
                    comm.Parameters.AddWithValue("@UserSurname", txtLibrarianLastName.Text);
                    comm.Parameters.AddWithValue("@UserMail", txtLibrarianEmail.Text);
                    comm.Parameters.AddWithValue("@UserPasswd", BCrypt.Net.BCrypt.HashPassword(txtLibrarianPassword.Text.Trim()));
                    comm.Parameters.AddWithValue("@UserRole", role);
                    comm.Parameters.AddWithValue("@OutstandingFees", fees);


                    int rowsAffected = comm.ExecuteNonQuery();
                    lblMessage.Text = "User added successfully!";
                    lblMessage.Visible = true;
                    conn.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Showtable(txtUserID.Text.Trim());

                lblMessage.Text = "Successfully added....";


                lblMessage.Visible = true;
            }
        }

        protected void btnRemoveUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserIdDel.Text))
            {
                return;
            }
            try
            {
                conn = new SqlConnection(con_string);

                conn.Open();

                string sql = $"DELETE FROM tblUsers WHERE UserID = @delID";

                comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@delID", txtUserIdDel.Text.Trim());

                adap = new SqlDataAdapter();

                adap.DeleteCommand = comm;
                adap.DeleteCommand.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Showtable(txtUserIdDel.Text.Trim());
            lblMessage.Text = "User Seccesfully Deleted!!";
        }

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserID.Text) || string.IsNullOrEmpty(txtUpdate.Text))
            {
                return;
            }
            try
            {
                conn = new SqlConnection(con_string);
                conn.Open();

                string sql = "";

                // Check which radio button is selected
                if (radEmail.Checked)
                {
                    
                    sql = "UPDATE tblUsers SET UserMail = @NewValue WHERE UserID = @UserID";
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
                    lblMessage.Text = "Please select what you want to update.";
                    conn.Close();
                    return;
                }

                // SQL query using string interpolation for simplicity (not best practice but same as your style)
                

                comm = new SqlCommand(sql, conn);

                // Add values from textboxes
                comm.Parameters.AddWithValue("@UserID", txtUserID.Text.Trim());
                comm.Parameters.AddWithValue("@NewValue", radPass.Checked ? BCrypt.Net.BCrypt.HashPassword(txtUpdate.Text.Trim()) : txtUpdate.Text.Trim());

                int rows = comm.ExecuteNonQuery();

                if (rows > 0)
                {
                    lblMessage.Text = "Successfully updated.";
                    txtUserID.Text = string.Empty;
                    txtUpdate.Text = string.Empty;
                }
                else
                {
                    lblMessage.Text = "No user found with that ID.";
                    txtUserID.Text = string.Empty;
                    txtUpdate.Text = string.Empty;
                }

                conn.Close();
                Showtable(txtUserID.Text.Trim()); // Refresh grid
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }


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

                gvUsers.DataSource = ds;
                gvUsers.DataBind();

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

            Showtable(orderBy, gvUsers);
        }

    }
}