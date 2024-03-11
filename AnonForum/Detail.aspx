<%@ Page Title="Detail" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Detail.aspx.vb" Inherits="AnonForum._Detail" %>


<asp:Content ID="DetailContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="rounded-2 bg-light p-3 border-bottom border-2 mt-1">
        <asp:Label ID="lblcurrentUserID" runat="server" Visible="false" ></asp:Label>
        <div class="d-flex align-items-center justify-content-start">
            <asp:Image runat="server" ID="UserImage" class="img rounded-circle" Width="20" Height="20" alt="User Profile Image" /><asp:Label ID="Username" CssClass="ms-2" runat="server"></asp:Label>
            <asp:Label ID="UserID" runat="server" Visible="false" ></asp:Label>
        </div>
        <h3 class="text-center">
            <asp:Label ID="lblTitle" CssClass="text-center" runat="server"></asp:Label>
        </h3>
        <asp:Label ID="lblPostID" runat="server" Visible="false"></asp:Label>
        <p id="txtPost" class="text-start" style="font-size: larger;">
            <asp:Label ID="lblPost" runat="server"></asp:Label>
            <asp:Image runat="server"
                ID="PostImage"
                CssClass="img-fluid"
                Style="display: block; margin: 0 auto;" />
        </p>
    </div>
    <div class="d-block">
        <div class="d-flex align-items-center justify-content-start m-3">
            <!-- Like button -->
            <asp:Button Width="100" ID="btnLikePost" runat="server" Text="Like" CssClass="btn btn-primary me-2" />
            <h4 class="text-center mx-3">
                <asp:Label ID="lblTotalLikes" runat="server"></asp:Label></h4>

            <!-- Dislike button -->
            <asp:Button Width="100" ID="btnDislikePost" runat="server" Text="Dislike" CssClass="btn btn-danger me-2" />
            <h4 class="text-center mx-3">
                <asp:Label ID="lblDislikePost" runat="server"></asp:Label></h4>

            <!-- Comment button -->
            <button type="button" id="commBtn" class="btn btn-info" onclick='<%# "showCommentModal()" %>'>Comment</button>
        </div>
    </div>
    <asp:Repeater ID="commentRepeater" runat="server" OnItemDataBound="commentRepeater_ItemDataBound" OnItemCommand="commentRepeater_ItemCommand">
        <ItemTemplate>
            <div class="rounded-2 bg-light p-3 border-bottom border-2 m-2">
                <div class="my-1">
                    <asp:Image runat="server" ImageUrl='<%# "~/UserImages/" & Eval("UserImage") %>' ID="UserImage" class="img rounded-circle" Width="20" Height="20" alt="User Profile Image" />
                    <asp:Label ID="commUsername" CssClass="ms-2" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                    <asp:Label ID="commUserID" Visible="false" CssClass="ms-2" runat="server" Text='<%# Eval("UserID") %>'></asp:Label>
                    <p class="text-start m-2"><%# Eval("Comment") %></p>
                    <asp:Label ID="commCommentID" runat="server" Visible="false" Text='<%# Eval("CommentID") %>'></asp:Label>
                    <div class="d-flex align-items-center justify-content-start m-3">
                        <asp:Button ID="btnLikeComment" Width="100" runat="server" Text="Like" CommandName="likeComment" /><h5 class="text-center mx-2 btn"><%# Eval("TotalLikes") %></h5>
                        <asp:Button ID="btnDislikeComment" Width="100" runat="server" Text="Dislike" CommandName="dislikeComment" /><h5 class="text-center mx-2 btn"><%# Eval("TotalDislikes") %></h5>
                    </div>
                </div>
            </div>
            <p class="text-muted">
                <asp:Label runat="server"><%# Eval("TimeStamp", "{0:MM/dd/yyyy/hh:mm}") %></asp:Label>
            </p>
            <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger" />
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
        </ItemTemplate>
    </asp:Repeater>
    <div class="d-flex justify-content-between align-items-center text-end my-2">
        <!-- Published on -->
        <p class="text-muted mb-0">
            Published on:
            <asp:Label ID="lblTimeStamp" runat="server"></asp:Label>
        </p>

        <!-- Delete button -->
        <asp:Button runat="server" ID="btnDeletePost" Text="Delete" CssClass="btn btn-danger ms-auto me-1" />
        <asp:Literal ID="showEdit" runat="server"></asp:Literal>
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
    </script>
</asp:Content>
