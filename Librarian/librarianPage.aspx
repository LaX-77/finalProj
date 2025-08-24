<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="librarianPage.aspx.cs" Inherits="Librarian.librarianPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bokamoso LMS - Librarian Dashboard</title>
    <link rel="stylesheet" href="librarianPage.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <header>
                <nav>
                    <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="landingPage.aspx" Text="Home" CssClass="nav-link" />
                    <asp:HyperLink ID="lnkStudentPage" runat="server" NavigateUrl="studentPage.aspx" Text="Student Dashboard" CssClass="nav-link" />
                </nav>

                  <div>

                <h1>&nbsp;</h1>
                      <h1>Bokamoso Library Management System</h1>
                        <h3>&nbsp;</h3>
                      <h3>Search Books</h3>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="input-field" placeholder="Search by title, author, or ISBN..." AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" />

                  </div>

            </header>
            <main>
                <section class="manage-section">
                    <h2>Librarian Dashboard</h2>
                        <asp:DropDownList ID="ddlSort" runat="server" CssClass="sort-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlSort_SelectedIndexChanged">
                            <asp:ListItem Value="Title ASC" Text="Sort by Title (A-Z)" />
                            <asp:ListItem Value="Title DESC" Text="Sort by Title (Z-A)" />
                            <asp:ListItem Value="Author ASC" Text="Sort by Author (A-Z)" />
                            <asp:ListItem Value="Author DESC" Text="Sort by Author (Z-A)" />
                        </asp:DropDownList>
                        <asp:GridView ID="gvBooks" runat="server" CssClass="books-table" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:BoundField DataField="Author" HeaderText="Author" />
                                <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                            </Columns>
                        </asp:GridView>
                    <p>&nbsp;</p>
                    <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false" />
                    
                    <div class="form-group">
                        <h3>Add New Book</h3>
                        <asp:TextBox ID="txtISBN" runat="server" CssClass="input-field" placeholder="ISBN" />
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="input-field" placeholder="Book Title" />
                        <asp:TextBox ID="txtAuthor" runat="server" CssClass="input-field" placeholder="Author" />
                        
                        <asp:TextBox ID="txtYear" runat="server" CssClass="input-field" placeholder="Year"/>
                        <asp:Textbox ID="txtEdition" runat="server" CssClass="input-field" placeholder="Edition"/>
                        <asp:Button ID="btnAddBook" runat="server" Text="Add Book" CssClass="btn-action" OnClick="btnAddBook_Click" />
                    </div>
                    
                    <div class="form-group">
                        <h3>Remove Book</h3>
                        <asp:ListBox ID="lstBooks" runat="server" CssClass="book-list" SelectionMode="Single" />
                        <asp:Button ID="btnRemoveBook" runat="server" Text="Remove Selected Book" CssClass="btn-delete" OnClick="btnRemoveBook_Click" OnClientClick="return confirm('Are you sure you want to remove this book?');" />
                        <br />
                    </div>
                    
                    <div class="form-group">
                        <h3>Confirm Book Collection</h3>
                        <asp:TextBox ID="txtCollectionCode" runat="server" CssClass="input-field" placeholder="Enter Collection Code" />
                        <asp:Button ID="btnConfirmCollection" runat="server" Text="Confirm Collection" CssClass="btn-action" OnClick="btnConfirmCollection_Click" />
                        <asp:GridView ID="gvCollectionBooks" runat="server" CssClass="books-table" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:BoundField DataField="Author" HeaderText="Author" />
                                <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    
                    <div class="form-group">
                        <h3>Confirm Book Return</h3>
                        <asp:TextBox ID="txtReturnCode" runat="server" CssClass="input-field" placeholder="Enter Collection Code" />
                        <asp:Button ID="btnConfirmReturn" runat="server" Text="Confirm Return" CssClass="btn-action" OnClick="btnConfirmReturn_Click" />
                        <asp:GridView ID="gvReturnBooks" runat="server" CssClass="books-table" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:BoundField DataField="Author" HeaderText="Author" />
                                <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    
                    <div class="form-group">
                    </div>
                </section>
            </main>
        </div>
    </form>
</body>
</html>