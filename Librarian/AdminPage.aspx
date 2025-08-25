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
                    <p>&nbsp;</p>
                    <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false" />
                    
                    <div class="form-group">
                        <h2>Add New Librarian</h2>
                        <asp:TextBox ID="txtLibrarianFirstName" runat="server" CssClass="input-field" placeholder="First Name" />
                        <asp:TextBox ID="txtLibrarianLastName" runat="server" CssClass="input-field" placeholder="Last Name" />
                        <asp:TextBox ID="txtLibrarianEmail" runat="server" CssClass="input-field" placeholder="Email" />
                        <asp:TextBox ID="txtLibrarianPassword" runat="server" TextMode="Password" CssClass="input-field" placeholder="Password" />
                        <asp:Button ID="btnAddLibrarian" runat="server" Text="Add Librarian" CssClass="btn-action" OnClick="btnAddLibrarian_Click" />
                    </div>
                    
                    <div class="form-group">
                        <h2>Remove Librarian or User</h2>
                        <asp:TextBox ID="txtUserIdDel" runat="server" Width="1174px"></asp:TextBox>
                        <br />
                        <br />
                        <asp:ListBox ID="lstUsers" runat="server" CssClass="user-list" SelectionMode="Single" />
                        <br />
                        <br />
                        <asp:Button ID="btnRemoveUser" runat="server" Text="Remove Selected" CssClass="btn-delete" OnClick="btnRemoveUser_Click" OnClientClick="return confirm('Are you sure you want to remove this user? This cannot be undone.');" />
                    </div>
                    
                    <div class="form-group">
                        <h2>&nbsp;</h2>
                        <h2>Update Librarian or User</h2>
                        <asp:TextBox ID="txtUserID" runat="server" Width="1179px"></asp:TextBox>
                        <br />
                        <br />
                        <asp:RadioButton ID="radEmail" runat="server" GroupName="TargetField" Text="Email" />
&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="radLast" runat="server" GroupName="TargetField" Text="Last Name" />
&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="radFirst" runat="server" GroupName="TargetField" Text="First Name" />
&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="radPass" runat="server" GroupName="TargetField" Text="Password" />
                        <br />
                        <br />
                        <asp:TextBox ID="txtUpdate" runat="server" CssClass="input-field" placeholder="Enter new Email/Name/Password" />
                        <asp:Button ID="btnUpdateUser" runat="server" Text="Update Details" CssClass="btn-action" OnClick="btnUpdateUser_Click" Width="267px" />
                        <br />
                    </div>
                    
                    <div class="form-group">
                        <h2>View All Users</h2>
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