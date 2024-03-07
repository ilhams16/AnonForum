Imports System.Web.Configuration
Imports AnonForum.BLL
Imports AnonForum.BLL.DTOs.User
Imports AnonForum.BLL.[Interface]
Public Class Login
    Inherits Page
    Private ReadOnly _userBLL As IUserBLL
    Dim currentUserID As Integer
    Public Sub New()
        _userBLL = New UserBLL()
    End Sub

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
        Dim user As New UserLoginDTO
        user.UsernameOrEmail = username
        user.Password = password
        Dim userLogin = _userBLL.UserLogin(user)
        currentUserID = userLogin.UserID
        result = True
        Session("CurrentUsername") = userLogin.Username
        Session("CurrentUserID") = userLogin.UserID
        Return result
    End Function
End Class