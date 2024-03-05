Imports System.Drawing
Imports System.Runtime.InteropServices
Imports AnonForum.BLL
Imports AnonForum.BLL.DTOs
Imports AnonForum.BLL.DTOs.Comment
Imports AnonForum.BLL.DTOs.Post
Imports AnonForum.BO
Imports AnonForum.DAL
Imports Microsoft.Ajax.Utilities

Public Class _UserHomePage
    Inherits Page
    Dim postBLL As New PostBLL()
    Dim userBLL As New UserBLL()
    Dim commBLL As New CommentBLL()
    Dim comdal As New CommentDAL()
    Dim currentUserID As Integer = userBLL.GetUserbyUsername(Context.User.Identity.Name.Trim()).UserID

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not (IsPostBack) Then
            Dim posts = GetPostsFromDatabase()
            postRepeater.DataSource = posts
            postRepeater.DataBind()
            BindCategories()

        End If
    End Sub
    Protected Sub BindCategories()
        Dim categories = postBLL.GetAllCategories()
        ddlCategories.DataSource = categories
        ddlCategories.DataBind()
        ddlCategories.Items.Insert(0, New ListItem("Select Category", "-1"))
    End Sub
    Protected Function GetPostsFromDatabase()
        Dim posts = postBLL.GetAllPosts()
        Return posts
    End Function

    Protected Sub postRepeater_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles postRepeater.ItemCommand
        Dim userID As Integer = CInt(DirectCast(e.Item.FindControl("UserID"), Label).Text)
        Dim postID As Integer = CInt(DirectCast(e.Item.FindControl("PostID"), Label).Text)
        If e.CommandName = "likePost" Then
            Dim likeBtn = postBLL.GetLikePost(postID, currentUserID)
            If likeBtn Then
                postBLL.UnlikePost(postID, currentUserID)
            Else
                postBLL.LikePost(postID, currentUserID)
            End If
            Response.Redirect("/", True)
        ElseIf e.CommandName = "dislikePost" Then
            Dim dislikeBtn = postBLL.GetDislikePost(postID, currentUserID)
            If dislikeBtn Then
                postBLL.UndislikePost(postID, currentUserID)
            Else
                postBLL.DislikePost(postID, currentUserID)
            End If
            Response.Redirect("/", True)
        ElseIf e.CommandName = "deletePost" Then
            postBLL.DeletePost(title, currentUserID)
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
        Dim comments = commBLL.GetCommentbyID(postID)
        Dim likeBtn = postBLL.GetLikePost(postID, currentUserID)
        likeButton.CssClass = If(likeBtn, "btn-primary", "btn-secondary")
        Dim dislikeBtn = postBLL.GetDislikePost(postID, currentUserID)
        dislikeButton.CssClass = If(dislikeBtn, "btn-primary", "btn-secondary")
        likeButton.Text = likeBtn
        dislikeButton.Text = dislikeBtn
        comment.DataSource = comments
        comment.DataBind()
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim deleteButton As Button = DirectCast(e.Item.FindControl("btnDelete"), Button)
            If Context.User.Identity.Name.Trim() = username.Trim() Then
                deleteButton.Visible = True
            Else
                deleteButton.Visible = False
            End If
        End If
    End Sub
    Protected Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Dim post As New CreatePostDTO
        Dim user = userBLL.GetUserbyUsername(Context.User.Identity.Name)
        post.UserID = user.UserID
        post.PostText = txtPostText.Text
        post.Title = txtTitle.Text
        post.PostCategoryID = CInt(ddlCategories.Text)
        postBLL.AddNewPost(post)
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
        Dim likeBtn = comdal.GetLike(commentID, postID, currentUserID)
        likeButton.CssClass = If(likeBtn, "btn-primary", "btn-secondary")
        Dim dislikeBtn = comdal.GetDislike(commentID, postID, currentUserID)
        dislikeButton.CssClass = If(dislikeBtn, "btn-primary", "btn-secondary")
        likeButton.Text = likeBtn
        dislikeButton.Text = dislikeBtn
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
        If e.CommandName = "likeComment" Then
            Dim likeBtn = comdal.GetLike(commentID, postID, currentUserID)
            If likeBtn Then
                comdal.UnlikeComment(commentID, postID, currentUserID)
            Else
                comdal.LikeComment(commentID, postID, currentUserID)
            End If
            Response.Redirect("/", True)
        ElseIf e.CommandName = "dislikeComment" Then
            Dim dislikeBtn = comdal.GetDislike(commentID, postID, currentUserID)
            If dislikeBtn Then
                comdal.UndislikeComment(commentID, postID, currentUserID)
            Else
                comdal.DislikeComment(commentID, postID, currentUserID)
            End If
            Response.Redirect("/", True)
        ElseIf e.CommandName = "deleteComment" Then
            comdal.DeleteComment(commentID, postID, currentUserID)
            Response.Redirect("/", True)
        End If
    End Sub
End Class