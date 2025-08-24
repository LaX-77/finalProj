<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="Librarian.AdminPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bokamoso LMS - Admin Dashboard</title>
    <link rel="stylesheet" href="AdminPage.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <header>
                
                <nav>
                    <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="landingPage.aspx" Text="Home" CssClass="nav-link" />
                    <asp:HyperLink ID="lnkStudentPage" runat="server" NavigateUrl="studentPage.aspx" Text="Student Dashboard" CssClass="nav-link" />
                    <asp:HyperLink ID="lnkLibrarianPage" runat="server" NavigateUrl="librarianPage.aspx" Text="Librarian Dashboard" CssClass="nav-link" />
                </nav>
                
                <h1>&nbsp;</h1>
                <h1>Bokamoso Library Management System</h1>
                <p>&nbsp;</p>
            </header>
            <main>
                <section class="manage-section">
                    <h2>Admin Dashboard</h2>
                    <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false" />
                    
                    <div class="form-group">
                        <h3>Add New Librarian</h3>
                        <asp:TextBox ID="txtLibrarianId" runat="server" CssClass="input-field" placeholder="Enter Librarian ID" />
                        <asp:TextBox ID="txtLibrarianEmail" runat="server" CssClass="input-field" placeholder="Email" />
                        <asp:TextBox ID="txtLibrarianFirstName" runat="server" CssClass="input-field" placeholder="First Name" />
                        <asp:TextBox ID="txtLibrarianLastName" runat="server" CssClass="input-field" placeholder="Last Name" />
                        <asp:TextBox ID="txtLibrarianPassword" runat="server" TextMode="Password" CssClass="input-field" placeholder="Password" />
                        <asp:Button ID="btnAddLibrarian" runat="server" Text="Add Librarian" CssClass="btn-action" OnClick="btnAddLibrarian_Click" />
                    </div>
                    
                    <div class="form-group">
                        <h3>Remove Librarian or User</h3>
                        <asp:DropDownList ID="ddlRemoveType" runat="server" CssClass="type-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlRemoveType_SelectedIndexChanged">
                            <asp:ListItem Value="Librarian" Text="Librarian" />
                            <asp:ListItem Value="Student" Text="Student" />
                        </asp:DropDownList>
                        <asp:ListBox ID="lstUsers" runat="server" CssClass="user-list" SelectionMode="Single" />
                        <asp:Button ID="btnRemoveUser" runat="server" Text="Remove Selected" CssClass="btn-delete" OnClick="btnRemoveUser_Click" OnClientClick="return confirm('Are you sure you want to remove this user? This cannot be undone.');" />
                    </div>
                    
                    <div class="form-group">
                        <h3>Update Librarian or User</h3>
                        <asp:DropDownList ID="ddlUpdateType" runat="server" CssClass="type-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlUpdateType_SelectedIndexChanged">
                            <asp:ListItem Value="Librarian" Text="Librarian" />
                            <asp:ListItem Value="Student" Text="Student" />
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlUsers" runat="server" CssClass="user-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged" />
                        <asp:TextBox ID="txtUpdateEmail" runat="server" CssClass="input-field" placeholder="Email" />
                        <asp:TextBox ID="txtUpdateFirstName" runat="server" CssClass="input-field" placeholder="First Name" />
                        <asp:TextBox ID="txtUpdateLastName" runat="server" CssClass="input-field" placeholder="Last Name" />
                        <asp:Button ID="btnUpdateUser" runat="server" Text="Update Details" CssClass="btn-action" OnClick="btnUpdateUser_Click" />
                    </div>
                    
                    <div class="form-group">
                        <h3>View All Users</h3>
                        <asp:TextBox ID="txtSearchUsers" runat="server" CssClass="input-field" placeholder="Search by name or ID..." AutoPostBack="true" OnTextChanged="txtSearchUsers_TextChanged" />
                        <asp:DropDownList ID="ddlSortUsers" runat="server" CssClass="sort-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlSortUsers_SelectedIndexChanged">
                            <asp:ListItem Value="Name ASC" Text="Sort by Name (A-Z)" />
                            <asp:ListItem Value="Name DESC" Text="Sort by Name (Z-A)" />
                            <asp:ListItem Value="ID ASC" Text="Sort by ID (A-Z)" />
                            <asp:ListItem Value="ID DESC" Text="Sort by ID (Z-A)" />
                        </asp:DropDownList>
                        <asp:GridView ID="gvUsers" runat="server" CssClass="users-table" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" />
                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:BoundField DataField="Type" HeaderText="Type" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </section>
            </main>
        </div>
    </form>
</body>
</html>