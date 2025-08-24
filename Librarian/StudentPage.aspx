<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentPage.aspx.cs" Inherits="Librarian.StudentPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bokamoso LMS - Student Dashboard</title>
    <link rel="stylesheet" href="StudentPage.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <header>
               
                <nav>
                    <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="~/LandingPage.aspx" Text="Home" CssClass="nav-link" />
                    <asp:HyperLink ID="lnkSettings" runat="server" NavigateUrl="~/SettingsPage.aspx" Text="Settings" CssClass="nav-link" />
                     
                </nav>
                <h1>Bokamoso Library Management System</h1>
                <p>&nbsp;</p>
                    <h2>Search Books</h2>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="search-input" placeholder="Search by title, author, or ISBN..." AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" />
            </header>
            <main>
                <section class="search-section">
                    <asp:ListBox ID="lstBooks" runat="server" CssClass="book-list" SelectionMode="Multiple" />
                    <asp:Button ID="btnAddToSelection" runat="server" Text="Add to Selection" CssClass="btn-add" OnClick="btnAddToSelection_Click" />
                </section>
                <section class="selected-books">
                    <h2>Selected Books</h2>
                    <asp:ListBox ID="lstSelectedBooks" runat="server" CssClass="selected-list" />
                    <asp:Button ID="btnConfirmSelections" runat="server" Text="Confirm Selections" CssClass="btn-confirm" OnClick="btnConfirmSelections_Click" />
                </section>
            </main>
        </div>
    </form>
</body>
</html>
