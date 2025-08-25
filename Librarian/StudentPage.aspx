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
                <asp:TextBox ID="txtSearch" runat="server" CssClass="search-input"
                    AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"
                    placeholder="Search by title, author, or ISBN..." />
            </header>
            <main>
                
                    <section class="search-section">
                    
                        <asp:ScriptManager ID="ScriptManager1" runat="server" />

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                
                                <asp:HiddenField ID="hdnSearchTriggered" runat="server" />

                                <asp:Label ID="lblHeadings" runat="server"></asp:Label>
                                <br />
                                <asp:ListBox ID="lstShow" runat="server" Font-Bold="True" Font-Overline="False" Font-Size="Large" Width="775px"></asp:ListBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <script type="text/javascript">
                            let debounceTimer;

                            function triggerSearchDebounced() {
                                clearTimeout(debounceTimer);
                                debounceTimer = setTimeout(function () {
                                    // ✅ Mark that search triggered the postback
                                    document.getElementById('<%= hdnSearchTriggered.ClientID %>').value = 'true';
                                    __doPostBack('<%= txtSearch.UniqueID %>', '');
                                }, 400);
                             }

                            function setupSearchDebounce() {
                                const searchBox = document.getElementById('<%= txtSearch.ClientID %>');
                                if (!searchBox) return;

                                searchBox.addEventListener('keyup', function () {
                                    triggerSearchDebounced();
                                });
                            }

                            function restoreSearchFocusIfNeeded() {
                                const searchBox = document.getElementById('<%= txtSearch.ClientID %>');
                                const hiddenField = document.getElementById('<%= hdnSearchTriggered.ClientID %>');

                                if (hiddenField && hiddenField.value === 'true') {
                                    hiddenField.value = ''; // Reset flag
                                    if (searchBox) {
                                        searchBox.focus();
                                        const val = searchBox.value;
                                        searchBox.value = '';
                                        searchBox.value = val;
                                    }
                                }
                            }

                            if (typeof window.searchDebounceInitialized === 'undefined') {
                                window.searchDebounceInitialized = true;
                                setupSearchDebounce();
                            }

                            window.onload = function () {
                                restoreSearchFocusIfNeeded();
                            };
                        </script>


                        <br />
                        
                    </section>

                <asp:Button ID="btnAddToSelection" runat="server" Text="Add to Selection" CssClass="btn-add" OnClick="btnAddToSelection_Click" />
                
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
