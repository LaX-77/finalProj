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
                conn = new SqlConnection(con_string);

                conn.Open();

                string sql = $"INSERT INTO tblUsers(UserName,UserSurname,UserMail,UserPasswd,UserRole,OutstandingFees)Values(@name, @sur,@mail, @pass, @role,@fees)";

                comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@name", txtLibrarianFirstName.Text.Trim());
                comm.Parameters.AddWithValue("@sur", txtLibrarianLastName.Text.Trim());
                comm.Parameters.AddWithValue("@mail", txtLibrarianEmail.Text.Trim());
                comm.Parameters.AddWithValue("@pass", txtLibrarianPassword.Text.Trim());
                comm.Parameters.AddWithValue("@role", role);
                comm.Parameters.AddWithValue("@fees", fees);

                comm.ExecuteNonQuery();

                lblMessage.Text = "Successfully added....";
                conn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Showtable();

            lblMessage.Text = "Successfully added....";
        }

        protected void btnRemoveUser_Click(object sender, EventArgs e)
        {

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
            Showtable2();
            lblMessage.Text = "User Seccesfully Deleted!!";
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