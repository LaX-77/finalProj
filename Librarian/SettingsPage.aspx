<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SettingsPage.aspx.cs" Inherits="Librarian.SettingsPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bokamoso LMS - Manage User Details</title>
    <link rel="stylesheet" href="SettingsPage.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <header>
                
                <nav>
                    <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="landingPage.aspx" Text="Home" CssClass="nav-link" />
                    <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="studentPage.aspx" Text="Back to Selection" CssClass="nav-link" />
                </nav>
                <br />
                
                <h1>Bokamoso Library Management System</h1>
            </header>
            <main>
                <section class="manage-section">
                    <h2>Manage User Details</h2>
                    <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false" />
                    
                    <div class="form-group">
                    </div>
                    
                    <div class="form-group">
                        <asp:Button ID="btnUpdatePassword" runat="server" Text="Update Password" CssClass="btn-action" OnClick="btnUpdatePassword_Click" />
                    </div>
                    
                    <div class="form-group">
                        <h3>Delete Account</h3>
                        <asp:Button ID="btnDeleteAccount" runat="server" Text="Delete My Account" CssClass="btn-delete" OnClick="btnDeleteAccount_Click" OnClientClick="return confirm('Are you sure you want to delete your account? This cannot be undone.');" />
                    </div>
                    
                    <div class="form-group">
                        <h3>Outstanding Fees</h3>
                        <asp:GridView ID="gvFees" runat="server" CssClass="fees-table" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="IssueDate" HeaderText="Issue Date" DataFormatString="{0:yyyy-MM-dd}" />
                                <asp:BoundField DataField="ReturnDate" HeaderText="Return Date" DataFormatString="{0:yyyy-MM-dd}"/>
                                <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:C}" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </section>
            </main>
        </div>
    </form>
    <footer>
    <p>&copy; 2025 Library Management System. All Rights Reserved.</p>
    </footer>
</body>

    <!-- Footer -->

</html>
