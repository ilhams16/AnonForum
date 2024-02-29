Imports AnonForum.BLL

Public Class WebForm2
    Inherits System.Web.UI.Page
    Dim userBLL As New UserBLL()
    Dim postBLL As New PostBLL()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'gvUsers.DataSource = userBLL.GetUsers()
        gvUsers.DataSource = postBLL.GetAllPosts()
        gvUsers.DataBind()
    End Sub

End Class