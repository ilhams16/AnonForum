Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports AnonForum.BLL
Imports AnonForum.BLL.DTOs.Comment
Imports AnonForum.BLL.DTOs.Post
Imports AnonForum.BLL.[Interface]
Imports AnonForum.DAL
Imports AnonForum.Login

Public Class _Default
    Inherits Page
    Private ReadOnly _postBLL As IPostBLL
    Private ReadOnly _userBLL As IUserBLL
    Private ReadOnly _comBLL As ICommentBLL

    Dim currentUserID As Integer
    Public Sub New()
        _postBLL = New PostBLL()
        _userBLL = New UserBLL()
        _comBLL = New CommentBLL()
        If (Context.User.Identity.IsAuthenticated) Then
            currentUserID = _userBLL.GetUserbyUsername(Context.User.Identity.Name.Trim()).UserID
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not (IsPostBack) Then
            Dim posts = GetPostsFromDatabase()
            postRepeater.DataSource = posts
            postRepeater.DataBind()
            BindCategories()
            If (Context.User.Identity.IsAuthenticated) Then
                isLogin.Visible = True
            End If
        End If
    End Sub
    Protected Sub BindCategories()
        Dim categories = _postBLL.GetAllCategories()
        ddlCategories.DataSource = categories
        ddlCategories.DataBind()
        ddlCategories.Items.Insert(0, New ListItem("Select Category", "-1"))
    End Sub
    Protected Function GetPostsFromDatabase()
        Dim posts = _postBLL.GetAllPosts()
        Return posts
    End Function

    Protected Sub postRepeater_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles postRepeater.ItemCommand
        Dim userID As Integer = CInt(DirectCast(e.Item.FindControl("UserID"), Label).Text)
        Dim postID As Integer = CInt(DirectCast(e.Item.FindControl("PostID"), Label).Text)
        Dim editPostID As Integer = CInt(DirectCast(e.Item.FindControl("modalPostID"), Label).Text)
        Dim title As String = DirectCast(e.Item.FindControl("NewTitle"), TextBox).Text
        Dim post As String = DirectCast(e.Item.FindControl("NewPost"), TextBox).Text
        Dim cat As Integer = CInt(DirectCast(e.Item.FindControl("ddlEditCategories"), DropDownList).Text)
        If e.CommandName = "likePost" Then
            Dim likeBtn = _postBLL.GetLikePost(postID, currentUserID)
            If likeBtn Then
                _postBLL.UnlikePost(postID, currentUserID)
            Else
                _postBLL.LikePost(postID, currentUserID)
            End If
            Response.Redirect("/", True)
        ElseIf e.CommandName = "dislikePost" Then
            Dim dislikeBtn = _postBLL.GetDislikePost(postID, currentUserID)
            If dislikeBtn Then
                _postBLL.UndislikePost(postID, currentUserID)
            Else
                _postBLL.DislikePost(postID, currentUserID)
            End If
            Response.Redirect("/", True)
        ElseIf e.CommandName = "deletePost" Then
            _postBLL.DeletePost(postID)
            Response.Redirect("/", True)
        ElseIf e.CommandName = "btnComment" Then
            If Visible Then
                Visible = False
            Else
                Visible = True
            End If
        ElseIf e.CommandName = "editPost" Then
            Dim edit As New EditPostDTO With {
                .Title = title,
                .PostText = post,
                .PostCategoryID = cat
            }
            _postBLL.EditPost(edit, editPostID)
            Response.Redirect("/", True)
        End If
    End Sub
    Protected Sub postRepeater_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles postRepeater.ItemDataBound
        Dim comment As Repeater = DirectCast(e.Item.FindControl("commentRepeater"), Repeater)
        Dim userID As Integer = CInt(DirectCast(e.Item.FindControl("UserID"), Label).Text)
        Dim postID As Integer = CInt(DirectCast(e.Item.FindControl("PostID"), Label).Text)
        Dim username As String = DirectCast(e.Item.FindControl("Username"), Label).Text
        Dim likeButton As Button = DirectCast(e.Item.FindControl("btnLike"), Button)
        Dim dislikeButton As Button = DirectCast(e.Item.FindControl("btnDislike"), Button)
        Dim ddlEditCategories As DropDownList = DirectCast(e.Item.FindControl("ddlEditCategories"), DropDownList)

        Dim categories = _postBLL.GetAllCategories()
        ddlEditCategories.DataSource = categories
        ddlEditCategories.DataBind()
        ddlEditCategories.Items.Insert(0, New ListItem("Select Category", "-1"))
        Dim comments = _comBLL.GetAllCommentbyPostID(postID)
        Dim likeBtn = _postBLL.GetLikePost(postID, currentUserID)
        likeButton.CssClass = If(likeBtn, "btn btn-primary form-control", "btn btn-secondary form-control")
        Dim dislikeBtn = _postBLL.GetDislikePost(postID, currentUserID)
        dislikeButton.CssClass = If(dislikeBtn, "btn btn-danger form-control", "btn btn-secondary form-control")
        likeButton.Text = If(likeBtn, "Unlike", "Like")
        dislikeButton.Text = If(dislikeBtn, "Undislike", "Dislike")
        comment.DataSource = comments
        comment.DataBind()
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim deleteButton As Button = DirectCast(e.Item.FindControl("btnDelete"), Button)
            Dim showEdit As Literal = DirectCast(e.Item.FindControl("showEdit"), Literal)
            If Context.User.Identity.Name.Trim() = username.Trim() Then
                deleteButton.Visible = True
                showEdit.Text = "<button type='button' class='btn btn-info' onclick='showModal(" & "&" & "Container.ItemIndex" & "&" & ")'>Edit</button>"
            Else
                deleteButton.Visible = False
            End If
        End If
    End Sub
    Protected Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Dim post As New CreatePostDTO
        Dim user = _userBLL.GetUserbyUsername(Context.User.Identity.Name)
        post.UserID = user.UserID
        post.PostText = txtPostText.Text
        post.Title = txtTitle.Text
        post.PostCategoryID = CInt(ddlCategories.Text)
        _postBLL.AddNewPost(post)
        Response.Redirect("/")
    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        FormsAuthentication.SignOut()
        Response.Redirect("/")
    End Sub

    Protected Sub commentRepeater_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim userID As Integer = CInt(DirectCast(e.Item.FindControl("UserID"), Label).Text)
        Dim commentID As Integer = CInt(DirectCast(e.Item.FindControl("CommentID"), Label).Text)
        Dim postID As Integer = CInt(DirectCast(e.Item.FindControl("PostID"), Label).Text)
        Dim username As String = DirectCast(e.Item.FindControl("Username"), Label).Text
        Dim likeButton As Button = DirectCast(e.Item.FindControl("btnLikeComment"), Button)
        Dim dislikeButton As Button = DirectCast(e.Item.FindControl("btnDislikeComment"), Button)

        Dim likeBtn = _comBLL.GetLike(commentID, postID, currentUserID)
        likeButton.CssClass = If(likeBtn, "btn-primary btn form-control", "btn-secondary btn form-control")
        Dim dislikeBtn = _comBLL.GetDislike(commentID, postID, currentUserID)
        dislikeButton.CssClass = If(dislikeBtn, "btn-danger btn form-control", "btn-secondary btn form-control")
        likeButton.Text = If(likeBtn, "Unlike", "Like")
        dislikeButton.Text = If(dislikeBtn, "Undislike", "Dislike")


        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim deleteButton As Button = DirectCast(e.Item.FindControl("btnDelete"), Button)
            If Context.User.Identity.Name.Trim() = username.Trim() Then
                deleteButton.Visible = True
            Else
                deleteButton.Visible = False
            End If
        End If
    End Sub

    Protected Sub commentRepeater_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Dim commentID As Integer = CInt(DirectCast(e.Item.FindControl("CommentID"), Label).Text)
        Dim postID As Integer = CInt(DirectCast(e.Item.FindControl("PostID"), Label).Text)
        Dim comment As String = DirectCast(e.Item.FindControl("txtComment"), TextBox).Text
        If e.CommandName = "likeComment" Then
            Dim likeBtn = _comBLL.GetLike(commentID, postID, currentUserID)
            If likeBtn Then
                _comBLL.UnlikeComment(commentID, postID, currentUserID)
            Else
                _comBLL.LikeComment(commentID, postID, currentUserID)
            End If
            Response.Redirect("/", True)
        ElseIf e.CommandName = "dislikeComment" Then
            Dim dislikeBtn = _comBLL.GetDislike(commentID, postID, currentUserID)
            If dislikeBtn Then
                _comBLL.UndislikeComment(commentID, postID, currentUserID)
            Else
                _comBLL.DislikeComment(commentID, postID, currentUserID)
            End If
            Response.Redirect("/", True)
        ElseIf e.CommandName = "deleteComment" Then
            _comBLL.DeleteComment(commentID, postID, currentUserID)
            Response.Redirect("/", True)
        ElseIf e.CommandName = "postComment" Then
            Dim newComment As New CreateCommentDTO
            newComment.PostID = postID
            newComment.UserID = currentUserID
            newComment.Comment = comment
            _comBLL.AddNewComment(newComment)
            Response.Redirect("/", True)
        End If
    End Sub
End Class