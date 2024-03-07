Imports AnonForum.BLL
Imports AnonForum.BLL.DTOs.User
Imports AnonForum.BLL.[Interface]

Public Class _Register
    Inherits System.Web.UI.Page
    Private ReadOnly _userBLL As IUserBLL
    Public Sub New()
        _userBLL = New UserBLL
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim newUser As New CreateUserDTO
        If CPassword.Text IsNot Password.Text Then
            lblMessage.Text = "Password is not match"
        End If
        newUser.Username = Username.Text
        newUser.Email = Email.Text
        newUser.Password = Password.Text
        newUser.Nickname = Nickname.Text
        newUser.UserImage = Image.FileBytes
        Dim createUser = _userBLL.AddNewUser(newUser)
        Response.Redirect("/Login")
    End Sub
End Class