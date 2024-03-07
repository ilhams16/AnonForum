<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Detail.aspx.vb" Inherits="AnonForum._Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3>
            <asp:Label ID="Title" CssClass="text-center" runat="server" Text='<%# Eval("Title") %>'></asp:Label></h3>
        <asp:Label ID="PostID" runat="server" Visible="false" Text='<%# Eval("PostID") %>'></asp:Label>
        <p class="text-start" style="font-size: larger;"><%# Eval("PostText") %></p>
    </div>
    <div class="d-block">
        <div class="d-flex align-items-center justify-content-start m-3">
            <!-- Like button -->
            <asp:Button Width="100" ID="btnLike" runat="server" CommandName="likePost" CssClass="btn btn-primary me-2" />
            <h4 class="text-center mx-3"><%# Eval("TotalLikes") %></h4>

            <!-- Dislike button -->
            <asp:Button Width="100" ID="btnDislike" runat="server" Text="Dislike" CommandName="dislikePost" CssClass="btn btn-danger me-2" />
            <h4 class="text-center mx-3"><%# Eval("TotalDislikes") %></h4>

            <!-- Comment button -->
            <button type="button" id="commBtn" class="btn btn-info" onclick='<%# "showCommentModal()" %>'>Comment</button>
        </div>
        <div class="rounded-2 bg-light p-3 border-bottom border-2 m-2">
            <div class="my-1">
                <img src="Assets/user.png" class="img rounded-circle" width="20" height="20" alt="User Profile Image" />
                <asp:Label ID="Username" CssClass="ms-2" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                <asp:Label ID="UserID" Visible="false" CssClass="ms-2" runat="server" Text='<%# Eval("UserID") %>'></asp:Label>
                <asp:Label ID="Label1" Visible="false" CssClass="ms-2" runat="server" Text='<%# DataBinder.Eval(Container.Parent.Parent, "DataItem.PostID") %>'></asp:Label>
                <p class="text-start m-2"><%# Eval("Comment") %></p>
                <asp:Label ID="CommentID" runat="server" Visible="false" Text='<%# Eval("CommentID") %>'></asp:Label>
                <div class="d-flex align-items-center justify-content-start m-3">
                    <asp:Button ID="btnLikeComment" Width="100" runat="server" Text="Like" CommandName="likeComment" /><h5 class="text-center mx-2 btn"><%# Eval("TotalLikes") %></h5>
                    <asp:Button ID="btnDislikeComment" Width="100" runat="server" Text="Dislike" CommandName="dislikeComment" /><h5 class="text-center mx-2 btn"><%# Eval("TotalDislikes") %></h5>
                </div>
            </div>
            <p class="text-muted"><%# Eval("TimeStamp", "{0:MM/dd/yyyy/hh:mm}") %></p>
            <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger" />
        </div>
        <!-- Modal Comment-->
        <div class="modal fade" id="myCommentModal" tabindex="-1" aria-labelledby="commentModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="commentModalLabel">Comment</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="justify-content-start align-content-start my-3">
                            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="4" Columns="50" CssClass="form-control mx-auto" placeholder="Comment..."></asp:TextBox>
                            <div>
                                <asp:Button ID="btnPostComment" runat="server" Text="Send" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="d-flex justify-content-between align-items-center text-end my-2">
        <!-- Published on -->
        <p class="text-muted mb-0">Published on: <%# Eval("TimeStamp", "{0:MM/dd/yyyy/hh:mm}") %></p>

        <!-- Delete button -->
        <asp:Button runat="server" ID="Button1" Text="Delete" CssClass="btn btn-danger ms-auto me-1" CommandName="deletePost" />

        <!-- Edit button -->
        <asp:Literal ID="showEdit" runat="server"></asp:Literal>
        <%--<button type="button" hidden="hidden" id="editBtn" class="btn btn-info" onclick='<%# "showModal(" & Container.ItemIndex & ")" %>'>Edit</button>--%>
    </div>
    <!-- Modal Edit Post -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Edit Post</h5>
                    <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="modalUsername" CssClass="ms-2" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                    <asp:Label ID="modalUserID" runat="server" Visible="false" Text='<%# Eval("UserID") %>'></asp:Label>
                </div>
                <div class="my-3">
                    <h3 class="text-center">
                        <asp:Label ID="modalPostID" runat="server" Visible="false" Text='<%# Eval("PostID") %>'></asp:Label>
                        <asp:TextBox ID="newTitle" runat="server" TextMode="SingleLine" CssClass="form-control mx-auto mt-1" Text='<%# Eval("Title") %>'></asp:TextBox>
                        <asp:TextBox ID="newPost" runat="server" TextMode="MultiLine" Rows="4" Columns="50" CssClass="form-control mx-auto mt-1" Text='<%# Eval("PostText") %>'></asp:TextBox>
                        <asp:DropDownList ID="ddlEditCategories" runat="server" DataTextField="Name" DataValueField="PostCategoryID" CssClass="form-control mx-auto mt-1">
                        </asp:DropDownList>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <asp:Button ID="saveEdit" runat="server" CssClass="btn btn-primary" Text="Save Changes" CommandName="editPost" />
                </div>
            </div>
        </div>
    </div>
    <script>
        function showModal(index) {
            $('#myModal').modal('show');
        }
        function showCommentModal(index) {
            $('#myCommentModal').modal('show');
        }
    </script>
</asp:Content>
m