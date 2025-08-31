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
    public partial class librarianPage : System.Web.UI.Page
    {
        private string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bookDB.mdf;Integrated Security=True";
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataAdapter ad;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddBook_Click(object sender, EventArgs e)
        {

        }

        protected void btnRemoveBook_Click(object sender, EventArgs e)
        {

        }

        protected void btnConfirmCollection_Click(object sender, EventArgs e)
        {

        }

        protected void btnConfirmReturn_Click(object sender, EventArgs e)
        {

        }

        public DataTable SearchBooks(string query)
        {
            string sql;
            if (query == "*")
                sql = "SELECT * FROM tblBooks";
            else
                sql = "SELECT * FROM tblBooks WHERE ISBN LIKE @id OR BookTitle LIKE @title OR BookAuthor LIKE @author";

            conn = new SqlConnection(conStr);





            conn.Open();
            cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", "%" + query + "%");
            cmd.Parameters.AddWithValue("@title", "%" + query + "%");
            cmd.Parameters.AddWithValue("@author", "%" + query + "%");

            ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            conn.Close();
            return dt;


        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string query = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(query))
            {
                gvBooks.DataSource = null;
                gvBooks.DataBind();
            
                return;
            }
            

            DataTable results = SearchBooks(query); // Your method to fetch filtered data

           
            gvBooks.DataSource = results;
            gvBooks.DataBind();

            // Set column headings above the ListBox
            //lblHeadings.Text = "<b>ISBN            | Book Title                     | Author                | Edition   | Year</b>";

        }

        protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvBooks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}