<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" EnableViewState="true" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="AnonForum._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .gradient-custom {
            /* fallback for old browsers */
            background: #6a11cb;
            /* Chrome 10-25, Safari 5.1-6 */
            background: -webkit-linear-gradient(to right, rgba(106, 17, 203, 1), rgba(37, 117, 252, 1));
            /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
            background: linear-gradient(to right, rgba(106, 17, 203, 1), rgba(37, 117, 252, 1))
        }
    </style>
    <main class="p-5 vh-150 gradient-custom">
        <div visible="false" runat="server" id="isLogin" class="d-block justify-content-center align-content-center">
            <div class="d-flex justify-content-center">
                <h4 class="col-9">Welcome <%: Context.User.Identity.Name %></h4>
                <div class="col-3 justify-content-end align-content-end mx-auto">
                    <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="form-control btn btn-danger" />
                </div>
            </div>
            <div class="mx-auto my-3">
                <div class="row justify-content-center">
                    <!-- Form Post -->
                    <div class="col-auto">
                        <div class="my-3">
                            <asp:TextBox ID="txtTitle" runat="server" TextMode="SingleLine" placeholder="Title" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div>
                            <asp:TextBox ID="txtPostText" runat="server" TextMode="MultiLine" Rows="4" Columns="50" CssClass="form-control" placeholder="What do you think?"></asp:TextBox>
                        </div>
                        <div class="my-3">
                            <label for="ddlCategory">Select Category:</label>
                            <asp:DropDownList ID="ddlCategories" runat="server" DataTextField="Name" DataValueField="PostCategoryID" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="my-3">
                            <label for="fileImage">Image:</label>
                            <asp:FileUpload ID="fileImage" runat="server" CssClass="form-control" />
                        </div>
                        <div class="my-3">
                            <asp:Button ID="btnPost" runat="server" Text="Post" OnClick="btnPost_Click" CssClass="form-control btn btn-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <section class="row" aria-labelledby="aspnetTitle">
            <div>
                <div class="mx-5 mt-1">
                    <label for="ddlCategory" class="text-white">Select Category:</label>
                    <asp:DropDownList ID="ddlFilter" runat="server" AutoPostBack="true" DataTextField="Name" DataValueField="PostCategoryID" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <asp:Repeater ID="postRepeater" runat="server" OnItemDataBound="postRepeater_ItemDataBound" OnItemCommand="postRepeater_ItemCommand">
                    <ItemTemplate>
                        <div class="rounded-2 bg-light p-3 border-bottom border-2 mt-1 mx-5">
                            <div class="d-flex align-items-center justify-content-start">
                                <asp:Image runat="server" ImageUrl='<%# "~/UserImages/" & Eval("UserImage") %>' ID="UserImage" class="img rounded-circle" Width="20" Height="20" alt="User Profile Image" /><asp:Label ID="Username" CssClass="ms-2" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                <asp:Label ID="UserID" runat="server" Visible="false" Text='<%# Eval("UserID") %>'></asp:Label>
                            </div>
                            <div class="m-3">
                                <h3 class="text-center">
                                    <a href='<%# "Detail.aspx?postID=" + Server.UrlEncode(Eval("PostID").ToString()) %>' class="text-center" style="text-decoration: none;">
                                        <asp:Label ID="TitleLabel" CssClass="text-center" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                    </a>
                                </h3>
                                <asp:Label ID="PostID" runat="server" Visible="false" Text='<%# Eval("PostID") %>'></asp:Label>
                                <p class="text-start" style="font-size: larger;"><%# Eval("PostText") %></p>
                                <asp:Image runat="server"
                                    ID="PostImage"
                                    CssClass="img-fluid"
                                    Visible='<%# Not String.IsNullOrEmpty(Eval("Image").ToString()) %>'
                                    ImageUrl='<%# If(Not String.IsNullOrEmpty(Eval("Image").ToString()), "~/PostImages/" & Eval("Image"), String.Empty) %>'
                                    Style="display: block; margin: 0 auto;" />
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
                                    <button type="button" id="commBtn" class="btn btn-info" onclick='<%# "showCommentModal(" & Container.ItemIndex & ")" %>'>Comment</button>
                                </div>
                                <asp:Repeater ID="commentRepeater" runat="server" OnItemDataBound="commentRepeater_ItemDataBound" OnItemCommand="commentRepeater_ItemCommand">
                                    <ItemTemplate>
                                        <div class="rounded-2 bg-light p-3 border-bottom border-2 m-2 ms-4">
                                            <div class="my-1">
                                                <asp:Image runat="server" ImageUrl='<%# "~/UserImages/" & Eval("UserImage") %>' ID="UserImage" class="img rounded-circle" Width="20" Height="20" alt="User Profile Image" />
                                                <asp:Label ID="Username" CssClass="ms-2" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                                <asp:Label ID="UserID" Visible="false" CssClass="ms-2" runat="server" Text='<%# Eval("UserID") %>'></asp:Label>
                                                <asp:Label ID="PostID" Visible="false" CssClass="ms-2" runat="server" Text='<%# DataBinder.Eval(Container.Parent.Parent, "DataItem.PostID") %>'></asp:Label>
                                                <p class="text-start m-2"><%# Eval("Comment") %></p>
                                                <asp:Label ID="CommentID" runat="server" Visible="false" Text='<%# Eval("CommentID") %>'></asp:Label>
                                                <div class="d-flex align-items-center justify-content-start m-3">
                                                    <asp:Button ID="btnLikeComment" Width="100" runat="server" Text="Like" CommandName="likeComment" /><h5 class="text-center mx-2 btn"><%# Eval("TotalLikes") %></h5>
                                                    <asp:Button ID="btnDislikeComment" Width="100" runat="server" Text="Dislike" CommandName="dislikeComment" /><h5 class="text-center mx-2 btn"><%# Eval("TotalDislikes") %></h5>
                                                </div>
                                            </div>
                                            <p class="text-muted"><%# Eval("TimeStamp", "{0:dddd, dd MMMM yyyy hh:mm}") %></p>
                                            <asp:Button runat="server" ID="btnDelete" CommandName="deleteComment" Text="Delete" CssClass="btn btn-danger" />
                                        </div>
                                        <!-- Modal Comment-->
                                        <div class="modal fade" id="myCommentModal<%# Container.ItemIndex %>" tabindex="-1" aria-labelledby="commentModalLabel" aria-hidden="true">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h5 class="modal-title" id="commentModalLabel">Comment</h5>
                                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                                    </div>
                                                    <div class="modal-body">
                                                        <asp:Label ID="modalPostID" runat="server" Visible="true" Text='<%# Eval("PostID") %>'></asp:Label>
                                                        <asp:Label ID="test" runat="server" Visible="true"></asp:Label>
                                                        <div class="justify-content-start align-content-start my-3">
                                                            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="4" Columns="50" CssClass="form-control mx-auto" placeholder="Comment..."></asp:TextBox>
                                                            <div>
                                                                <asp:Button ID="btnPostComment" runat="server" Text="Send" CssClass="btn btn-primary" CommandName="postComment" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="d-flex justify-content-between align-items-center text-end my-2">
                                <!-- Published on -->
                                <p class="text-muted mb-0 mx-1">Published on: <%# Eval("TimeStamp", "{0:dddd, dd MMMM yyyy hh:mm}") %></p>
                                <p class="text-muted mb-0 mx-1">Category: <%# Eval("CategoryName") %></p>
                                <!-- Delete button -->
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger ms-auto me-1" CommandName="deletePost" />

                                <!-- Edit button -->
                                <%--<asp:Literal ID="showEdit" runat="server"></asp:Literal>--%>
                                <button type="button" runat="server" id="editBtn" class="btn btn-info" onclick='<%# "showModal(" & Container.ItemIndex & ")" %>'>Edit</button>
                            </div>
                        </div>
                        <!-- Modal Edit Post -->
                        <div class="modal fade" id="myModal<%# Container.ItemIndex %>" tabindex="-1" role="dialog">
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
                                            <asp:Image runat="server"
                                                ID="oldImage"
                                                Height="50"
                                                CssClass="img-fluid"
                                                Visible='<%# Not String.IsNullOrEmpty(Eval("Image").ToString()) %>'
                                                ImageUrl='<%# If(Not String.IsNullOrEmpty(Eval("Image").ToString()), "~/PostImages/" & Eval("Image"), String.Empty) %>' />
                                            <asp:FileUpload ID="newFileImage" runat="server" CssClass="form-control" value='<%# Eval("Image") %>' />
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
                    </ItemTemplate>
                </asp:Repeater>
                <div class="d-flex">
                    <asp:Button ID="btnPreviousPage" runat="server" Text="Previous Page" OnClick="btnPreviousPage_Click" CssClass="m-2" />
                    <asp:Button ID="btnNextPage" runat="server" Text="Next Page" OnClick="btnNextPage_Click" CssClass="m-2" />
                </div>
                <!-- JavaScript to control modal -->
                <script>
                    function showModal(index) {
                        $('#myModal' + index).modal('show');
                    }
                    function showCommentModal(index) {
                        $('#myCommentModal' + index).modal('show');
                    }
                </script>
            </div>
        </section>

    </main>


</asp:Content>
