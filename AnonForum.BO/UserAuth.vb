Public Class UserAuth
    Public Sub New()
        Me.Users = New List(Of UserAuth)()
    End Sub
    Public Property UserID As Integer
    Public Property Username As String
    Public Property Email As String
    Public Property Password As String
    Public Property Nickname As String
    Public Property UserImage As String
    Public Property Users As List(Of UserAuth)
End Class