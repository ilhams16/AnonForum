Imports System.Web.Configuration
Imports AnonForum.DAL
Public Class Login
    Inherits Page
    Dim dal As New UserDAL()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If IsValidUser(txtUsername.Text, txtPassword.Text) Then
            FormsAuthentication.SetAuthCookie(txtUsername.Text, False)
            Response.Redirect(FormsAuthentication.DefaultUrl)
        Else
            lblMessage.Text = "Invalid username or password."
        End If
    End Sub
    Protected Function IsValidUser(username As String, password As String) As Boolean
        Dim result = False
        Dim user = dal.UserLogin(username, password)
        result = True
        Session("CurrentUsername") = user.Username
        Session("CurrentUserID") = user.UserID
        Return result
    End Function
End Class