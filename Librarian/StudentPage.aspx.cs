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
    public partial class StudentPage : System.Web.UI.Page
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bookDB.mdf;Integrated Security=True";
        private SqlCommand cmd;
        private SqlDataAdapter ad;
        private SqlConnection conn;
        

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirmSelections_Click(object sender, EventArgs e)
        {
          lblError.Text = "";
        if (lstSelectedBooks.Items.Count == 0)
        {
            lblError.Text = "No books selected. Please add books to your selection before confirming.";
            return;
        }
        Session["SelectedBooks"] = lstSelectedBooks.Items.Cast<ListItem>().Select(item => item.Text).ToList();

        if (calCollectDate.SelectedDate == DateTime.MinValue || calCollectDate.SelectedDate < DateTime.Today)
        {
            lblError.Text = "Please select a valid collection date (today or later).";
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Visible = true;
            return;
        }

        DateTime collectDT = calCollectDate.SelectedDate;
        DateTime returnDT = collectDT.AddDays(8);
        string[] arrBookIDs = new string[4];
        int i = 0;
        foreach (ListItem item in lstSelectedBooks.Items)
        {
            // Assuming the format is "123 - Book Title"
            string[] parts = item.Text.Split('-');
            if (parts.Length > 0)
            {
                arrBookIDs[i] = parts[0].Trim(); // Get the ID part and trim whitespace
                i++;
            }
        }

        string transactionCode = "KR"+collectDT.ToShortDateString()+userId;

        conn = new SqlConnection(connectionString);

        for (int j = 0; j < i; j++)
        {
            try
            {
                conn.Open();
                string sql = "INSERT INTO tblBorrowed (UserID, ISBN, BorrowDate, ReturnDate, Status, QRCode) VALUES(@id, @is, @b, @r, @st, @qr)";
                string status = "Pending";
                cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", Convert.ToInt64(userId));
                cmd.Parameters.AddWithValue("@is", Convert.ToInt64(arrBookIDs[j]));
                cmd.Parameters.AddWithValue("@b", collectDT);
                cmd.Parameters.AddWithValue("@r", returnDT);
                cmd.Parameters.AddWithValue("@st", status);
                cmd.Parameters.AddWithValue("@qr", transactionCode);
                
                cmd.ExecuteNonQuery();
                conn.Close();
                
                
            }
            catch(SqlException ex)
            {
                lblError.Text = " Couldnt place booking for ISBN: " + arrBookIDs[j] + "\n"+ex.Message;
                return;
            }
            
        }
        lblError.Text = "Collection QR CODE: " + transactionCode;
        //Response.Redirect("ConfirmSelectionPage.aspx");


        }

        protected void btnAddToSelection_Click(object sender, EventArgs e)
        {
           // Loop through the items in the search results (lstShow)
           foreach (ListItem item in lstShow.Items)
            {
              // If the item is selected and not already in lstSelectedBooks, add it
             if (item.Selected && lstSelectedBooks.Items.FindByValue(item.Value) == null)
                 {
                    lstSelectedBooks.Items.Add(new ListItem(item.Text, item.Value));
                }
            }
        }

        public DataTable SearchBooks(string query)
        {
            string sql = "SELECT ISBN, BookTitle, BookAuthor, BookEdition, Year FROM tblBooks WHERE ISBN LIKE @id OR BookTitle LIKE @title OR BookAuthor LIKE @author";
            conn = new SqlConnection(connectionString);




            
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
                //dgViewBooks.DataSource = null;
                //dgViewBooks.DataBind();
                lstShow.DataSource = null;
                lstShow.DataBind();
                lblHeadings.Text = ""; // Clear headings
                return;
            }

            DataTable results = SearchBooks(query); // Your method to fetch filtered data

            // Add a new column to the DataTable for formatted display
            results.Columns.Add("FormattedText", typeof(string));
            foreach (DataRow row in results.Rows)
            {
                // Format the text to align columns (adjust spacing as needed)
                row["FormattedText"] = $"{row["ISBN"],-10}    | {row["BookTitle"],-30} by {row["BookAuthor"],-20} {row["Year"],-4}, EDITION: {row["BookEdition"],-11}";
            }

            // Bind to ListBox
            lstShow.DataSource = results;
            lstShow.DataTextField = "FormattedText"; // Use the formatted column
            lstShow.DataValueField = "ISBN";         // Value field for selection
            lstShow.DataBind();

            // Set column headings above the ListBox
            lblHeadings.Text = "<b>ISBN            | Book Title                     | Author                | Edition   | Year</b>";


        }

        protected void dgViewBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////GridViewRow row = dgViewBooks.SelectedRow;
            //string isbn = row.Cells[1].Text; // Adjust index based on your column order
        //    string title = row.Cells[2].Text;
            

        }//... WHERE ISBN LIKE '%' + @id + '%' OR BookTitle LIKE '%' + @title + '%' ...
    }
}
