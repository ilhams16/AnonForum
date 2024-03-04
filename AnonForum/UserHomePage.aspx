<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserHomePage.aspx.vb" Inherits="AnonForum._UserHomePage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="m-auto d-block justify-content-center align-content-center">
            <h4>Welcome <%: Context.User.Identity.Name %></h4>
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
            <div>
                <asp:TextBox ID="txtTitle" runat="server" TextMode="SingleLine" placeholder="Title"></asp:TextBox>
            </div>
            <div>
                <asp:TextBox ID="txtPostText" runat="server" TextMode="MultiLine" Rows="4" Columns="50" placeholder="What do you think?"></asp:TextBox>
            </div>
            <label for="ddlCategory">Select Category:</label>
            <asp:DropDownList ID="ddlCategories" runat="server" DataTextField="Name" DataValueField="PostCategoryID">
            </asp:DropDownList>
            <div>
                <label for="fileImage">Image:</label>
                <asp:FileUpload ID="fileImage" runat="server" />
            </div>
            <div>
                <asp:Button ID="btnPost" runat="server" Text="Post" OnClick="btnPost_Click" />
            </div>
        </div>
        <section class="row" aria-labelledby="aspnetTitle">
            <div class="container">
                <asp:Repeater ID="postRepeater" runat="server" OnItemDataBound="postRepeater_ItemDataBound">
                    <ItemTemplate>
                        <div class="card my-2 mx-auto p-2 rounded-2 bg-light">
                            <div class="d-flex align-items-center justify-content-start">
                                <img src="Assets/user.png" class="img rounded-circle" width="20" height="20" alt="User Profile Image" />
                                <asp:Label ID="Username" CssClass="ms-2" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                            </div>
                            <div class="my-3">
                                <h3 class="text-center">
                                    <asp:Label ID="Title" CssClass="text-center" runat="server" Text='<%# Eval("Title") %>'></asp:Label></h3>
                                <p class="text-start"><%# Eval("PostText") %></p>
                            </div>
                            <div class="d-block">
                                <div class="d-flex align-items-center justify-content-start">
                                    <asp:ImageButton ID="btnLike" runat="server" src="Assets/like.png" Width="50" class="ms-2 btn" commandName="likePost"/>
                                    <asp:ImageButton ID="btnDislike" runat="server" src="Assets/dislike.png" Width="50" class="ms-2 btn" />
                                    <asp:ImageButton ID="btnComment" runat="server" src="Assets/comment.png" Width="50" class="ms-2 btn" />
                                </div>
                                <div class="d-flex align-items-center justify-content-start">
                                    <h4 class="ms-2 mx-6"><%# Eval("TotalLikes") %></h4>
                                    <h4 class="ms-2 mx-6"><%# Eval("TotalDislikes") %></h4>
                                </div>
                            </div>
                            <div class="text-end">
                                <p class="text-muted">Published on: <%# Eval("TimeStamp", "{0:MM/dd/yyyy}") %></p>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="deletePost" />
                            </div>
                        </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>

    </main>

</asp:Content>
