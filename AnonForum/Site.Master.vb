Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            UpdateSignInOutButton() ' Update button text initially
        End If
    End Sub

    Protected Sub btnSignInOut_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSignInOut.Click
        If Context.User.Identity.IsAuthenticated Then
            ' If logged in, log out
            FormsAuthentication.SignOut()
            UpdateSignInOutButton()
            Response.Redirect("~/") ' Redirect to login page after logout
        Else
            ' If not logged in, redirect to login page
            Response.Redirect("~/Login")
        End If
    End Sub

    Private Sub UpdateSignInOutButton()
        If Context.User.Identity.IsAuthenticated Then
            ' If logged in, change button text to "Log out"
            btnSignInOut.Text = "Log out"
        Else
            ' If not logged in, change button text to "Sign in"
            btnSignInOut.Text = "Sign in"
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Dim searchQuery As String = txtSearch.Text.Trim()
        If Not String.IsNullOrEmpty(searchQuery) Then
            Response.Redirect("~/?query=" & HttpUtility.UrlEncode(searchQuery))
        End If
    End Sub

End Class