Imports AnonForum.BLL
Imports AnonForum.BO
Imports AnonForum.DAL

Public Class _UserHomePage
    Inherits Page
    Dim postBLL As New PostBLL()

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

    End Sub

    Protected Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click

    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        FormsAuthentication.SignOut()
        Response.Redirect("/")
    End Sub
End Class