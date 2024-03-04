Imports AnonForum.BLL
Imports AnonForum.BLL.DTOs.Post
Imports AnonForum.BO
Imports AnonForum.DAL

Public Class _UserHomePage
    Inherits Page
    Dim postBLL As New PostBLL()
    Dim userBLL As New UserBLL()

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
        If e.CommandName = "deletePost" Then
            ' Determine the index of the item clicked
            Dim itemIndex As Integer = e.Item.ItemIndex
            Dim title As String = CType(postRepeater.Items(itemIndex).FindControl("Title"), Label).Text
            Dim username As String = CType(postRepeater.Items(itemIndex).FindControl("Username"), Label).Text
            postBLL.DeletePost(title, userBLL.GetUserbyUsername(username).UserID)
            Response.Redirect("/", True)
        ElseIf e.CommandName = "likePost" Then
            Dim itemIndex As Integer = e.Item.ItemIndex
            Dim title As String = CType(postRepeater.Items(itemIndex).FindControl("Title"), Label).Text
            Dim username As String = CType(postRepeater.Items(itemIndex).FindControl("Username"), Label).Text
            Dim likeBtn As Boolean = postBLL.LikePost(postBLL.GetPostbyTitleandUsername(title, username).UserID, postBLL.GetPostbyTitleandUsername(title, username).PostID)
            Response.Redirect("/", True)
            Dim likeButton As Button = DirectCast(e.Item.FindControl("btnLike"), Button)
            If likeBtn Then
                likeButton.BorderStyle = BorderStyle.Solid
            Else
                likeButton.BorderStyle = BorderStyle.NotSet
            End If
        End If
    End Sub
    Protected Sub postRepeater_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles postRepeater.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim deleteButton As Button = DirectCast(e.Item.FindControl("btnDelete"), Button)
            Dim usernameLabel As Label = DirectCast(e.Item.FindControl("Username"), Label)
            ' Check if the current user's username matches the username associated with the post
            If Context.User.Identity.Name.Trim() = usernameLabel.Text.Trim() Then
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
End Class