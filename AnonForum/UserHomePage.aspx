<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserHomePage.aspx.vb" Inherits="AnonForum._UserHomePage" EnableSessionState="True" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="card p-3">
        <div class="d-block justify-content-center align-content-center">
            <h4>Welcome <%: Context.User.Identity.Name %></h4>
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
            <div class="my-3">
                <div>
                    <asp:TextBox ID="txtTitle" runat="server" TextMode="SingleLine" placeholder="Title" cssclass="form-control"></asp:TextBox>
                </div>
                <div>
                    <asp:TextBox ID="txtPostText" runat="server" TextMode="MultiLine" Rows="4" Columns="50" cssclass="form-control" placeholder="What do you think?"></asp:TextBox>
                </div>
                <label for="ddlCategory">Select Category:</label>
                <asp:DropDownList ID="ddlCategories" runat="server" DataTextField="Name" DataValueField="PostCategoryID" cssclass="form-control">
                </asp:DropDownList>
                <div>
                    <label for="fileImage">Image:</label>
                    <asp:FileUpload ID="fileImage" runat="server" cssclass="form-control" />
                </div>
                <div class="my-3">
                    <asp:Button ID="btnPost" runat="server" Text="Post" OnClick="btnPost_Click" cssclass="form-control" />
                </div>
            </div>
        </div>
        <section class="row" aria-labelledby="aspnetTitle">
            <div class="container">
                <asp:Repeater ID="postRepeater" runat="server" OnItemDataBound="postRepeater_ItemDataBound" OnItemCommand="postRepeater_ItemCommand">
                    <ItemTemplate>
                        <div class="rounded-2 bg-light p-3 border-bottom border-2 mt-1">
                            <div class="d-flex align-items-center justify-content-start">
                                <img src="Assets/user.png" class="img rounded-circle" width="20" height="20" alt="User Profile Image" />
                                <asp:Label ID="Username" CssClass="ms-2" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                <asp:Label ID="UserID" runat="server" Visible="false" Text='<%# Eval("UserID") %>'></asp:Label>
                            </div>
                            <div class="my-3">
                                <h3 class="text-center">
                                    <asp:Label ID="Title" CssClass="text-center" runat="server" Text='<%# Eval("Title") %>'></asp:Label></h3>
                                <asp:Label ID="PostID" runat="server" Visible="false" Text='<%# Eval("PostID") %>'></asp:Label>
                                <p class="text-start"><%# Eval("PostText") %></p>

                            </div>
                            <div class="d-block">
                                <div class="d-flex align-items-center justify-content-start m-3">
                                    <asp:Button ID="btnLike" runat="server" Text="Like" CommandName="likePost" /><h4 class="text-center mx-2"><%# Eval("TotalLikes") %></h4>
                                    <asp:Button ID="btnDislike" runat="server" Text="Dislike" CommandName="dislikePost" /><h4 class="text-center mx-2"><%# Eval("TotalDislikes") %></h4>
                                    <asp:Button ID="btnComment" runat="server" CssClass="btn-primary" Text="Comment" />
                                </div>
                                <asp:Repeater ID="commentRepeater" runat="server" OnItemDataBound="commentRepeater_ItemDataBound" OnItemCommand="commentRepeater_ItemCommand">
                                    <ItemTemplate>
                                        <div class="rounded-2 bg-light p-1 border-bottom border-2 mt-0">
                                            <div class="my-1">
                                                <img src="Assets/user.png" class="img rounded-circle" width="20" height="20" alt="User Profile Image" />
                                                <asp:Label ID="Username" CssClass="ms-2" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                                <asp:Label ID="UserID" Visible="false" CssClass="ms-2" runat="server" Text='<%# Eval("UserID") %>'></asp:Label>
                                                <asp:Label ID="PostID" Visible="false" CssClass="ms-2" runat="server" Text='<%# DataBinder.Eval(Container.Parent.Parent, "DataItem.PostID") %>'></asp:Label>
                                                <p class="text-start m-2"><%# Eval("Comment") %></p>
                                                <asp:Label ID="CommentID" runat="server" Visible="false" Text='<%# Eval("CommentID") %>'></asp:Label>
                                                <div class="d-flex align-items-center justify-content-start m-3">
                                                    <asp:Button ID="btnLikeComment" runat="server" Text="Like" CommandName="likeComment" /><h5 class="text-center mx-2"><%# Eval("TotalLikes") %></h5>
                                                    <asp:Button ID="btnDislikeComment" runat="server" Text="Dislike" CommandName="dislikeComment" /><h5 class="text-center mx-2"><%# Eval("TotalDislikes") %></h5>
                                                </div>
                                            </div>
                                            <p class="text-muted"><%# Eval("TimeStamp", "{0:MM/dd/yyyy/hh:mm}") %></p>
                                            <asp:Button runat="server" ID="btnDelete" Text="Delete" />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="justify-content-start align-content-start my-3">
                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="4" Columns="50" CssClass="form-control mx-auto" placeholder="Comment..."></asp:TextBox>
                                    <div>
                                        <asp:Button ID="btnPost" runat="server" Text="Post" />
                                    </div>
                                </div>
                            </div>
                            <div class="text-end">
                                <p class="text-muted">Published on: <%# Eval("TimeStamp", "{0:MM/dd/yyyy/hh:mm}") %></p>
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>

    </main>

</asp:Content>
