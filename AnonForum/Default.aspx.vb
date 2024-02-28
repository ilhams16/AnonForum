Imports AnonForum.BO
Imports AnonForum.DAL

Public Class _Default
    Inherits Page
    Dim dal As New PostDAL()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not (IsPostBack) Then
            Dim posts As List(Of Post) = GetPostsFromDatabase()
            postRepeater.DataSource = posts
            postRepeater.DataBind()
        End If
    End Sub
    Protected Function GetPostsFromDatabase() As List(Of Post)
        Dim posts = dal.GetAllPost()
        Return posts
    End Function
End Class