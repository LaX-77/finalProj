<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="Librarian.AdminPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bokamoso LMS - Admin Dashboard</title>
    <link rel="stylesheet" href="AdminPage.css" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 185px;
        }
    </style>
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
                    <asp:Label ID="lblMessage" runat="server" CssClass="message" />
                    
                    <div class="form-group">
                        <h2>Add New Librarian</h2>
                        <p>&nbsp;</p>
                        <table class="auto-style1">
                            <tr>
                                <td class="auto-style2">
                        <asp:TextBox ID="txtLibrarianFirstName" runat="server" CssClass="input-field" placeholder="First Name" Width="806px" />
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLibrarianFirstName" ErrorMessage=" First Name Required!!!" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                        <asp:TextBox ID="txtLibrarianLastName" runat="server" CssClass="input-field" placeholder="Last Name" Width="804px" />
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLibrarianLastName" ErrorMessage="Last Name Required!!" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                        <asp:TextBox ID="txtLibrarianEmail" runat="server" CssClass="input-field" placeholder="Email" Width="803px" />
                                    <br />
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtLibrarianEmail" ErrorMessage="Email Required!!" Font-Bold="True" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2">
                        <asp:TextBox ID="txtLibrarianPassword" runat="server" TextMode="Password" CssClass="input-field" placeholder="Password" Width="802px" />
                                </td>
                                <td>
                                    &nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLibrarianPassword" ErrorMessage="PassWord Required!!!" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btnAddLibrarian" runat="server" Text="Add Librarian" CssClass="btn-action" OnClick="btnAddLibrarian_Click" />
                    </div>
                    
                    <div class="form-group">
                        <h2>Remove Librarian or User</h2>
                        <asp:TextBox ID="txtUserIdDel" runat="server" Width="1174px"></asp:TextBox>
                        <br />
                        <br />
                        <asp:GridView ID="GridView3" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="1112px">
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#000065" />
                        </asp:GridView>
                        <br />
                        <br />
                        <asp:Button ID="btnRemoveUser" runat="server" Text="Remove Selected" CssClass="btn-delete" OnClick="btnRemoveUser_Click" OnClientClick="return confirm('Are you sure you want to remove this user? This cannot be undone.');" />
                    </div>
                    
                    <div class="form-group">
                        <h2>&nbsp;</h2>
                        <h2>Update Librarian or User</h2>
                        <p>&nbsp;</p>
                        <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="1112px">
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#000065" />
                        </asp:GridView>
                        <br />
                        <br />
                        <asp:RadioButton ID="radEmail" runat="server" GroupName="Chosen" Text="Email" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="radLast" runat="server" GroupName="Chosen" Text="Last Name"  />
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="radPass" runat="server" GroupName="Chosen" Text="Password" />
                        <br />
                        <br />
                        <asp:TextBox ID="txtUserID" runat="server" CssClass="input-field" placeholder="Enter ID to change"/>
                        <br />
                        <br />
                        <asp:TextBox ID="txtUpdate" runat="server" CssClass="input-field" placeholder="Enter new Email/Name/Password" />
                        &nbsp;
                        <asp:Button ID="bntUpdateUser" runat="server" BackColor="#3366FF" Height="54px" OnClick="bntUpdateUser_Click" Text="Update Info" Width="241px" />
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" />
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