Imports System.ComponentModel
Imports System.IO
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
            ViewState("CurrentPage") = 1 ' Set the initial page number in ViewState
            BindData(ViewState("CurrentPage"))
            'Dim posts = GetPostsFromDatabase()
            'postRepeater.DataSource = posts
            'postRepeater.DataBind()
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

    Private Sub BindData(ByVal pageNumber As Integer)
        Dim data = GetDataForPage(pageNumber)
        postRepeater.DataSource = data
        postRepeater.DataBind()
    End Sub

    Private Function GetDataForPage(ByVal pageNumber As Integer)
        Dim allData As IEnumerable(Of PostDTO) = GetPostsFromDatabase()
        Dim pageSize As Integer = 10 ' Set your desired page size
        Dim skip As Integer = (pageNumber - 1) * pageSize
        Dim pageData As IEnumerable(Of PostDTO) = allData.Skip(skip).Take(pageSize).ToList()

        Return pageData
    End Function
    Protected Sub btnNextPage_Click(sender As Object, e As EventArgs)
        Dim currentPage As Integer = ViewState("CurrentPage") ' Get the current page number from ViewState
        Dim maxPage As Integer = postRepeater.Items.Count
        If currentPage = maxPage Then
            BindData(currentPage) ' Bind data for the next page
            ViewState("CurrentPage") = currentPage ' Update the current page number in ViewState
        Else
            Dim nextPage As Integer = currentPage + 1 ' Increment the current page number to navigate to the next page
            BindData(nextPage) ' Bind data for the next page
            ViewState("CurrentPage") = nextPage ' Update the current page number in ViewState
        End If
    End Sub
    Protected Sub btnPreviousPage_Click(sender As Object, e As EventArgs)
        Dim currentPage As Integer = ViewState("CurrentPage") ' Get the current page number from ViewState
        If currentPage >= 1 Then
            Dim previousPage As Integer = currentPage - 1 ' Decrement the current page number to navigate to the previous page
            BindData(previousPage) ' Bind data for the previous page
            ViewState("CurrentPage") = previousPage ' Update the current page number in ViewState
        Else
            BindData(currentPage)
            ViewState("CurrentPage") = currentPage
        End If
    End Sub
    Protected Sub postRepeater_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles postRepeater.ItemCommand
        Dim userID As Integer = CInt(DirectCast(e.Item.FindControl("UserID"), Label).Text)
        Dim postID As Integer = CInt(DirectCast(e.Item.FindControl("PostID"), Label).Text)
        Dim editPostID As Integer = CInt(DirectCast(e.Item.FindControl("modalPostID"), Label).Text)
        Dim title As String = DirectCast(e.Item.FindControl("NewTitle"), TextBox).Text
        Dim post As String = DirectCast(e.Item.FindControl("NewPost"), TextBox).Text
        Dim cat As Integer = CInt(DirectCast(e.Item.FindControl("ddlEditCategories"), DropDownList).Text)
        If e.CommandName = "likePost" Then
            Dim likeBtn = _postBLL.GetLikePost(postID, currentUserID)
            If (Context.User.Identity.IsAuthenticated) Then
                If likeBtn Then
                    _postBLL.UnlikePost(postID, currentUserID)
                Else
                    _postBLL.LikePost(postID, currentUserID)
                End If
                Response.Redirect(Request.Url.AbsoluteUri)
            Else
                Response.Redirect("/Login")
            End If
        ElseIf e.CommandName = "dislikePost" Then
            Dim dislikeBtn = _postBLL.GetDislikePost(postID, currentUserID)
            If (Context.User.Identity.IsAuthenticated) Then
                If dislikeBtn Then
                    _postBLL.UndislikePost(postID, currentUserID)
                Else
                    _postBLL.DislikePost(postID, currentUserID)
                End If
                Response.Redirect(Request.Url.AbsoluteUri)
            Else
                Response.Redirect("/Login")
            End If
        ElseIf e.CommandName = "deletePost" Then
            _postBLL.DeletePost(postID)
            Response.Redirect("/", True)
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
            Dim editBtn As HtmlButton = DirectCast(e.Item.FindControl("editBtn"), HtmlButton)
            'Dim showEdit As Literal = DirectCast(e.Item.FindControl("showEdit"), Literal)
            If Context.User.Identity.Name.Trim() = username.Trim() Then
                deleteButton.Visible = True
                'showEdit.Text = "<button type=" & "button" & "id=" & "editBtn" & "class=" & "btn btn-info" & "onclick='<%# " & "showModal(" & "& Container.ItemIndex & " & ")" & "%>'>Edit</button>"
                editBtn.Visible = True
            Else
                deleteButton.Visible = False
                editBtn.Visible = False
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
        Dim fileName As String = Guid.NewGuid().ToString() + Path.GetExtension(fileImage.FileName)

        ' Specify the directory to save the uploaded file
        Dim uploadDirectory As String = Server.MapPath("~/PostImages/")

        ' Create the directory if it doesn't exist
        If Not Directory.Exists(uploadDirectory) Then
            Directory.CreateDirectory(uploadDirectory)
        End If

        ' Save the uploaded file to the server
        fileImage.SaveAs(Path.Combine(uploadDirectory, fileName))
        post.Image = fileName
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
        Dim test As Label = DirectCast(e.Item.FindControl("test"), Label)
        If e.CommandName = "likeComment" Then
            Dim likeBtn = _comBLL.GetLike(commentID, postID, currentUserID)
            If (Context.User.Identity.IsAuthenticated) Then
                If likeBtn Then
                    _comBLL.UnlikeComment(commentID, postID, currentUserID)
                Else
                    _comBLL.LikeComment(commentID, postID, currentUserID)
                End If
                Response.Redirect("/", True)
            Else
                Response.Redirect("/Login")
            End If
        ElseIf e.CommandName = "dislikeComment" Then
            Dim dislikeBtn = _comBLL.GetDislike(commentID, postID, currentUserID)
            If (Context.User.Identity.IsAuthenticated) Then
                If dislikeBtn Then
                    _comBLL.UndislikeComment(commentID, postID, currentUserID)
                Else
                    _comBLL.DislikeComment(commentID, postID, currentUserID)
                End If
                Response.Redirect("/", True)
            Else
                Response.Redirect("/Login")
            End If
        ElseIf e.CommandName = "deleteComment" Then
            _comBLL.DeleteComment(commentID)
            Response.Redirect("/", True)
        ElseIf e.CommandName = "postComment" Then
            If (Context.User.Identity.IsAuthenticated) Then
                Dim newComment As New CreateCommentDTO With {
                .PostID = postID,
                .UserID = currentUserID,
                .Comment = comment
            }
                _comBLL.AddNewComment(newComment)
                Response.Redirect("/", True)
            Else
                Response.Redirect("/Login")
            End If
        End If
    End Sub
End Class