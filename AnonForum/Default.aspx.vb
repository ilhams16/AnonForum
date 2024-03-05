Imports AnonForum.BLL

Public Class _Default
    Inherits Page
    Dim postBLL As New PostBLL()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not (IsPostBack) Then
            If (Context.User.Identity.IsAuthenticated) Then
                Response.Redirect("/UserHomePage")
            Else
                Dim posts = GetPostsFromDatabase()
                postRepeater.DataSource = posts
                postRepeater.DataBind()
            End If
        End If

    End Sub
    Protected Function GetPostsFromDatabase()
        Dim posts = postBLL.GetAllPosts()
        Return posts
    End Function

    Protected Sub postRepeater_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles postRepeater.ItemCommand
    End Sub

End Class