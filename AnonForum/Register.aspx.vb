Imports AnonForum.BLL
Imports AnonForum.BLL.DTOs.User

Public Class WebForm1
    Inherits System.Web.UI.Page
    Dim userBLL As New UserBLL()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim newUser As New CreateUserDTO
        newUser.Username = txtUsername.Text
        newUser.Email = txtEmail.Text
        newUser.Password = txtPassword.Text
        newUser.Nickname = txtNick.Text
        newUser.UserImage = fileImage.FileBytes
        Dim createUser = userBLL.AddNewUser(newUser)
        Response.Redirect("/Login")
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Response.Redirect("/Login")
    End Sub
End Class