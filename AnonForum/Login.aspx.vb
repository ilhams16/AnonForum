Imports AnonForum.DAL
Public Class Login
    Inherits Page
    Dim dal As New UserDAL()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim user = dal.UserLogin(txtUsername.Text, txtPassword.Text)
        lblMessage.Text = user.Username
        Response.RedirectPermanent("/")
    End Sub
End Class