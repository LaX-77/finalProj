<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmSelectionPage.aspx.cs" Inherits="Librarian.ConfirmSelectionPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bokamoso LMS - Confirm Selections</title>
    <link rel="stylesheet" href="ConfirmSelectionPage.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <header>
                <h1>Bokamoso Library Management System</h1>
                <nav>
                    <asp:HyperLink ID="lnkHome" runat="server" NavigateUrl="landingPage.aspx" Text="Home" CssClass="nav-link" />
                    <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="studentPage.aspx" Text="Back to Selection" CssClass="nav-link" />
                </nav>
            </header>
            <main>
                <section class="confirm-section">
                    <h2>Confirm Your Book Selections</h2>
                    <asp:Label ID="lblSelectedBooks" runat="server" Text="Selected Books:" AssociatedControlID="lstSelectedBooks"></asp:Label>
                    <br />
                    <asp:ListBox ID="lstSelectedBooks" runat="server" CssClass="selected-list" SelectionMode="Multiple"></asp:ListBox>
                    <br />
                    <asp:Label ID="lblCollectionDate" runat="server" Text="Choose Date of Collection:" AssociatedControlID="Calendar1"></asp:Label>
                    <br />
                    <asp:Calendar ID="Calendar1" runat="server" CssClass="calendar"></asp:Calendar>
                    <br />
                    <asp:Button ID="btnFinalConfirm" runat="server" Text="Final Confirm" CssClass="btn-confirm" OnClick="btnFinalConfirm_Click" />
                    <br />
                    <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false" />
                </section>
            </main>
        </div>
    </form>
</body>
</html>