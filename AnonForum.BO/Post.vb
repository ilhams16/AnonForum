﻿Public Class Post
    Public Property PostID As Integer
    Public Property UserID As Integer
    Public Property Title As String
    Public Property PostText As String
    Public Property TimeStamp As Date
    Public Property Image As String
    Public Property PostCategoryID As Integer
    Public Property TotalLikes As Integer
    Public Property TotalDislikes As Integer
    Public Property Category As Category
    Public Property UserPost As UserAuth
End Class
