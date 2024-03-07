Imports AnonForum.BLL
Imports AnonForum.DAL

Public Class _Detail
    Inherits System.Web.UI.Page
    Dim postBLL As New PostBLL()
    Dim userBLL As New UserBLL()
    Dim commBLL As New CommentBLL()
    Dim comdal As New CommentDAL()
    Dim currentUserID As Integer = userBLL.GetUserbyUsername(Context.User.Identity.Name.Trim()).UserID
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Function GetPostsFromDatabase()
        Dim posts
        Return posts
    End Function

End Class