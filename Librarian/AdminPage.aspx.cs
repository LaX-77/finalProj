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

        protected void btnAddLibrarian_Click(object sender, EventArgs e)
        {

        }

        protected void btnRemoveUser_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {

        }

        protected void ddlRemoveType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlUpdateType_SelectedIndexChanged(object sender, EventArgs e)
        {

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