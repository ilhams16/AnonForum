Imports System.Runtime.CompilerServices
Imports AnonForum.BLL
Imports AnonForum.BLL.DTOs.Comment
Imports AnonForum.BLL.DTOs.Post
Imports AnonForum.BLL.Interface

Public Class _Detail
    Inherits System.Web.UI.Page
    Private ReadOnly _postBLL As IPostBLL
    Private ReadOnly _userBLL As IUserBLL
    Private ReadOnly _commBLL As ICommentBLL

    Dim currentUserID As Integer
    Public Sub New()
        _postBLL = New PostBLL()
        _userBLL = New UserBLL()
        _commBLL = New CommentBLL()
        If (Context.User.Identity.IsAuthenticated) Then
            currentUserID = _userBLL.GetUserbyUsername(Context.User.Identity.Name.Trim()).UserID
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (IsPostBack) Then
            If Request.QueryString("postID") IsNot Nothing Then
                Dim postID As Integer = CInt(Request.QueryString("postID").ToString())
                Dim item As PostDTO = GetPostsFromDatabase(postID)
                Dim comments = _commBLL.GetAllCommentbyPostID(postID)
                'Dim username = ""
                'If currentUserID <> 0 Then
                '    username = _userBLL.GetUserbyID(currentUserID).Username
                'End If
                commentRepeater.DataSource = comments
                commentRepeater.DataBind()
                lblcurrentUserID.Text = currentUserID
                UserID.Text = item.UserID
                Username.Text = item.Username
                lblTitle.Text = item.Title
                lblPostID.Text = item.PostID
                lblPost.Text = item.PostText
                lblTotalLikes.Text = item.TotalLikes
                lblDislikePost.Text = item.TotalDislikes
                lblTimeStamp.Text = item.TimeStamp
                UserImage.ImageUrl = "~/UserImages/" & item.UserImage
                If item.Image Is Nothing Then
                    PostImage.Visible = False
                Else
                    PostImage.ImageUrl = "~/PostImages/" & item.Image
                End If
                Dim likeBtn = _postBLL.GetLikePost(postID, currentUserID)
                btnLikePost.CssClass = If(likeBtn, "btn btn-primary form-control", "btn btn-secondary form-control")
                Dim dislikeBtn = _postBLL.GetDislikePost(postID, currentUserID)
                btnDislikePost.CssClass = If(dislikeBtn, "btn btn-danger form-control", "btn btn-secondary form-control")
                If Context.User.Identity.Name.Trim() = Username.Text.Trim() Then
                    btnDeletePost.Visible = True
                    showEdit.Text = "<button type='button' class='btn btn-info' onclick='showModal'>Edit</button>"
                Else
                    btnDeletePost.Visible = False
                End If
            End If
        End If
    End Sub
    Protected Function GetPostsFromDatabase(postID As Integer)
        Dim post = _postBLL.GetPostbyID(postID)
        Return post
    End Function

    Protected Sub commentRepeater_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Dim userID As Integer = CInt(DirectCast(e.Item.FindControl("commUserID"), Label).Text)
        Dim commentID As Integer = CInt(DirectCast(e.Item.FindControl("commCommentID"), Label).Text)
        Dim postID As Integer = CInt(Request.QueryString("postID").ToString())
        Dim username As String = DirectCast(e.Item.FindControl("commUsername"), Label).Text
        Dim likeButton As Button = DirectCast(e.Item.FindControl("btnLikeComment"), Button)
        Dim dislikeButton As Button = DirectCast(e.Item.FindControl("btnDislikeComment"), Button)
        Dim likeBtn = _commBLL.GetLike(commentID, postID, currentUserID)
        likeButton.CssClass = If(likeBtn, "btn-primary btn form-control", "btn-secondary btn form-control")
        Dim dislikeBtn = _commBLL.GetDislike(commentID, postID, currentUserID)
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
        Dim commentID As Integer = CInt(DirectCast(e.Item.FindControl("commCommentID"), Label).Text)
        Dim postID As Integer = CInt(Request.QueryString("postID").ToString())
        Dim comment As String = DirectCast(e.Item.FindControl("txtComment"), TextBox).Text
        If e.CommandName = "likeComment" Then
            Dim likeBtn = _commBLL.GetLike(commentID, postID, currentUserID)
            If likeBtn Then
                _commBLL.UnlikeComment(commentID, postID, currentUserID)
            Else
                _commBLL.LikeComment(commentID, postID, currentUserID)
            End If
            Response.Redirect("/", True)
        ElseIf e.CommandName = "dislikeComment" Then
            Dim dislikeBtn = _commBLL.GetDislike(commentID, postID, currentUserID)
            If dislikeBtn Then
                _commBLL.UndislikeComment(commentID, postID, currentUserID)
            Else
                _commBLL.DislikeComment(commentID, postID, currentUserID)
            End If
            Response.Redirect("/", True)
        ElseIf e.CommandName = "deleteComment" Then
            _commBLL.DeleteComment(commentID)
            Response.Redirect("/", True)
        ElseIf e.CommandName = "postComment" Then
            Dim newComment As New CreateCommentDTO
            newComment.PostID = postID
            newComment.UserID = currentUserID
            newComment.Comment = comment
            _commBLL.AddNewComment(newComment)
            Response.Redirect("/", True)
        End If
    End Sub
End Class