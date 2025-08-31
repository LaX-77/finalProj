using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Librarian
{
    public partial class librarianPage : System.Web.UI.Page
    {
        string con_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bookDB.mdf;Integrated Security=True";
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adap;
        DataSet ds;
        SqlDataReader read;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public DataTable SearchBooks(string query)
        {
            try
            {
                string sql = "SELECT ISBN, BookTitle, BookAuthor, BookEdition, Year FROM tblBooks WHERE ISBN LIKE @id OR BookTitle LIKE @title OR BookAuthor LIKE @author";
                conn = new SqlConnection(con_string);

                conn.Open();
                comm = new SqlCommand(sql, conn);

                comm.Parameters.AddWithValue("@id", "%" + query + "%");
                comm.Parameters.AddWithValue("@title", "%" + query + "%");
                comm.Parameters.AddWithValue("@author", "%" + query + "%");
                adap = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                conn.Close();
                return dt;
            }
            catch (SqlException)
            {
                return null;
            }


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

            // Bind to gridview
            gvBooks.DataSource = results;
            gvBooks.DataBind();
        }

        protected void btnAddBook_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(con_string);

                conn.Open();
                string sql = "INSERT INTO tblBooks (ISBN, BookTitle, BookAuthor, BookGenre, BookEdition, Year, Available) VALUES (@ISBN, @BookTitle, @BookAuthor, @BookGenre, @BookEdition, @Year, @Available)";
                string genre = dropdownGenre.SelectedItem.ToString();
                comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                comm.Parameters.AddWithValue("@BookTitle", txtTitle.Text);
                comm.Parameters.AddWithValue("@BookAuthor", txtAuthor.Text);
                comm.Parameters.AddWithValue("@BookGenre", genre);
                comm.Parameters.AddWithValue("@BookEdition", txtEdition.Text);
                comm.Parameters.AddWithValue("@Year", txtYear.Text);
                comm.Parameters.AddWithValue("@Available", txtAvailable.Text);
                comm.ExecuteNonQuery();


                lblMessage.Text = "Books  added successfully!";
                lblMessage.Visible = true;
                conn.Close();




            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }


        }


        protected void btnRemoveBook_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(con_string);
                conn.Open();

                string sql = "DELETE FROM tblBooks WHERE ISBN = @delISBN";
                comm = new SqlCommand(sql, conn);
                comm.Parameters.AddWithValue("@delISBN", txtEnterISBN.Text.Trim());

                int rows = comm.ExecuteNonQuery();

                if (rows > 0)
                {
                    lblMessage.Text = "Book successfully deleted!";
                }
                else
                {
                    lblMessage.Text = "No book found with that ISBN.";
                }

                lblMessage.Visible = true;

                conn.Close();


            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.Visible = true;
            }
        }

       
        protected void btnConfirmCollection_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(con_string);
                conn.Open();

                // Retrieve all books with the given QRCode and Status = 'Borrowed'
                string sqlCheckBorrowed = "SELECT ISBN FROM tblBorrowed WHERE QRCode = @QRCode AND Status = 'Pending'";
                SqlCommand comm = new SqlCommand(sqlCheckBorrowed, conn);
                comm.Parameters.AddWithValue("@QRCode", txtCollectionCode.Text.Trim());
                SqlDataReader reader = comm.ExecuteReader();
                List<int> isbnList = new List<int>();
                while (reader.Read())
                {
                    isbnList.Add(reader.GetInt32(0)); // Collect ISBNs
                }
                reader.Close();

                if (isbnList.Count == 0)
                {
                    lblMessage.Text = "No borrowed books found for this QRCode.";
                    lblMessage.Visible = true;
                    return;
                }

                int booksProcessed = 0;
                foreach (int isbn in isbnList)
                {
                    // Check availability for each ISBN
                    string sqlCheck = "SELECT Available FROM tblBooks WHERE ISBN = @ISBN";
                    comm = new SqlCommand(sqlCheck, conn);
                    comm.Parameters.AddWithValue("@ISBN", isbn);
                    object result = comm.ExecuteScalar();

                    if (result != null)
                    {
                        int available = Convert.ToInt32(result);
                        if (available > 0)
                        {
                            // Update available copies
                            string sqlUpdate = "UPDATE tblBooks SET Available = @Available WHERE ISBN = @ISBN";
                            comm = new SqlCommand(sqlUpdate, conn);
                            comm.Parameters.AddWithValue("@Available", available - 1);
                            comm.Parameters.AddWithValue("@ISBN", isbn);
                            comm.ExecuteNonQuery();

                            // Update borrow record status to 'Collected'
                            string sqlUpdateBorrow = "UPDATE tblBorrowed SET Status = 'Collected' WHERE ISBN = @ISBN AND QRCode = @QRCode AND Status = 'Pending'";
                            comm = new SqlCommand(sqlUpdateBorrow, conn);
                            comm.Parameters.AddWithValue("@ISBN", isbn);
                            comm.Parameters.AddWithValue("@QRCode", txtCollectionCode.Text.Trim());
                            comm.ExecuteNonQuery();

                            booksProcessed++;
                        }
                        else
                        {
                            lblMessage.Text = $"No copies available for ISBN {isbn}.";
                            lblMessage.Visible = true;
                            return;
                        }
                    }
                    else
                    {
                        lblMessage.Text = $"Book with ISBN {isbn} not found.";
                        lblMessage.Visible = true;
                        return;
                    }
                }

                lblMessage.Text = $"{booksProcessed} book(s) successfully collected.";
                lblMessage.Visible = true;
                conn.Close();
                //Showtable(); // Refresh GridView
            }
            catch (FormatException)
            {
                lblMessage.Text = "Please enter a valid QRCode.";
                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.Visible = true;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        protected void btnConfirmReturn_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(con_string);
                conn.Open();

                // Retrieve all books with the given QRCode and Status = 'Collected'
                string sqlCheckBorrowed = "SELECT ISBN, ReturnDate FROM tblBorrowed WHERE QRCode = @QRCode AND Status = 'Collected'";
                SqlCommand comm = new SqlCommand(sqlCheckBorrowed, conn);
                comm.Parameters.AddWithValue("@QRCode", txtCollectionCode.Text.Trim());
                SqlDataReader reader = comm.ExecuteReader();
                List<(int ISBN, DateTime ReturnDate)> borrowList = new List<(int, DateTime)>();
                while (reader.Read())
                {
                    borrowList.Add((reader.GetInt32(0), reader.GetDateTime(1))); // Collect ISBN and ReturnDate
                }
                reader.Close();

                if (borrowList.Count == 0)
                {
                    lblMessage.Text = "No collected books found for this QRCode.";
                    lblMessage.Visible = true;
                    return;
                }

                int booksProcessed = 0;
                foreach (var borrow in borrowList)
                {
                    int isbn = borrow.ISBN;
                    DateTime returnDate = borrow.ReturnDate;

                    // Check if book exists and get current availability
                    string sqlCheck = "SELECT Available FROM tblBooks WHERE ISBN = @ISBN";
                    comm = new SqlCommand(sqlCheck, conn);
                    comm.Parameters.AddWithValue("@ISBN", isbn);
                    object result = comm.ExecuteScalar();

                    if (result != null)
                    {
                        int available = Convert.ToInt32(result);

                        // Check if overdue
                        string newStatus = (returnDate < DateTime.Now.Date) ? "Overdue" : "Collected";

                        // Update available copies
                        string sqlUpdateBook = "UPDATE tblBooks SET Available = @Available WHERE ISBN = @ISBN";
                        comm = new SqlCommand(sqlUpdateBook, conn);
                        comm.Parameters.AddWithValue("@Available", available + 1);
                        comm.Parameters.AddWithValue("@ISBN", isbn);
                        comm.ExecuteNonQuery();

                        // Update borrow record status to 'Overdue' (if applicable) then 'Returned'
                        if (newStatus == "Overdue")
                        {
                            string sqlUpdateOverdue = "UPDATE tblBorrowed SET Status = 'Overdue' WHERE ISBN = @ISBN AND QRCode = @QRCode AND Status = 'Collected'";
                            comm = new SqlCommand(sqlUpdateOverdue, conn);
                            comm.Parameters.AddWithValue("@ISBN", isbn);
                            comm.Parameters.AddWithValue("@QRCode", txtCollectionCode.Text.Trim());
                            comm.ExecuteNonQuery();
                        }

                        string sqlUpdateBorrow = "UPDATE tblBorrowed SET Status = 'Returned', ReturnDate = @ReturnDate WHERE ISBN = @ISBN AND QRCode = @QRCode AND Status IN ('Collected', 'Overdue')";
                        comm = new SqlCommand(sqlUpdateBorrow, conn);
                        comm.Parameters.AddWithValue("@ReturnDate", DateTime.Now.Date);
                        comm.Parameters.AddWithValue("@ISBN", isbn);
                        comm.Parameters.AddWithValue("@QRCode", txtCollectionCode.Text.Trim());
                        comm.ExecuteNonQuery();

                        booksProcessed++;
                    }
                    else
                    {
                        lblMessage.Text = $"Book with ISBN {isbn} not found.";
                        lblMessage.Visible = true;
                        return;
                    }
                }

                lblMessage.Text = $"{booksProcessed} book(s) successfully returned.";
                lblMessage.Visible = true;
                conn.Close();
                //Showtable(); // Refresh GridView
            }
            catch (FormatException)
            {
                lblMessage.Text = "Please enter a valid QRCode.";
                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.Visible = true;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void Showtable(string orderBy)
        {
            try
            {
                conn = new SqlConnection(con_string);
                conn.Open();

                adap = new SqlDataAdapter();
                ds = new DataSet();

                string sql = $"SELECT * FROM tblBooks ORDER BY {orderBy}";

                comm = new SqlCommand(sql, conn);

                adap.SelectCommand = comm;
                adap.Fill(ds);

                gvBooks.DataSource = ds;
                gvBooks.DataBind();

                conn.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orderBy;
            if (ddlSort.SelectedIndex == -1)
            {
                orderBy = "BookTitle ASC";
            }
            else
            {
                orderBy = ddlSort.SelectedItem.Value.ToString();
            }

            Showtable(orderBy);
        }
    }
}