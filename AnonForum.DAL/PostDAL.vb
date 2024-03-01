Imports System.Data.SqlClient
Imports AnonForum.BO

Public Class PostDAL
    Implements IPost
    Private ReadOnly strConn As String
    Private conn As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader

    Public Sub New()
        strConn = "Server=.\BSISqlExpress;Database=AnonForum;Trusted_Connection=True;"
        conn = New SqlConnection(strConn)
    End Sub

    Public Function GetAllPost() As List(Of Post) Implements IPost.GetAllPost
        Dim Posts As New List(Of Post)
        Try
            Dim strSql = "select * from Posts p
                            join UserAuth ua
                            on p.UserID = ua.UserID"

            conn = New SqlConnection(strConn)
            cmd = New SqlCommand(strSql, conn)
            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    Dim post As New Post With {
                        .UserID = CInt(dr("UserID")),
                        .PostID = dr("PostID").ToString(),
                        .Title = dr("Title").ToString(),
                        .PostText = dr("PostText").ToString(),
                        .TimeStamp = dr("TimeStamp").ToString(),
                        .PostCategoryID = CInt(dr("PostCategoryID")),
                        .TotalLikes = CInt(dr("TotalLikes")),
                        .TotalDislikes = CInt(dr("TotalDislikes")),
                        .Username = dr("Username").ToString()
                    }
                    Posts.Add(post)
                End While
            End If
            dr.Close()

            Return Posts
        Catch ex As Exception
            Throw
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function

    Public Function GetPostbyTitle(ByVal title As String) As List(Of Post) Implements IPost.GetPostbyTitle
        Dim Posts As New List(Of Post)
        Try
            Dim strSql = "select * from Posts p
                            join UserAuth ua
                            on p.UserID = ua.UserID
                            where p.Title = @title"
            conn = New SqlConnection(strConn)
            cmd = New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@title", title)
            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    Dim post As New Post With {
                        .UserID = CInt(dr("UserID")),
                        .PostID = dr("PostID").ToString(),
                        .Title = dr("Title").ToString(),
                        .PostText = dr("PostText").ToString(),
                        .TimeStamp = dr("TimeStamp").ToString(),
                        .PostCategoryID = CInt(dr("PostCategoryID")),
                        .TotalLikes = CInt(dr("TotalLikes")),
                        .TotalDislikes = CInt(dr("TotalDislikes")),
                        .Username = dr("Username").ToString()
                    }
                    Posts.Add(post)
                End While
            End If
            dr.Close()

            Return Posts
        Catch ex As Exception
            Throw
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function

    Public Function CreatePost(post As Post) Implements IPost.AddNewPost
        Dim status = ""
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "DECLARE	@return_value int
                EXEC	@return_value = [dbo].[NewPost]
		                @userID
                        ,@title
                        ,@post
                        ,@postCategoryID
                SELECT	'Return Value' = @return_value"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@userID", post.UserID)
            cmd.Parameters.AddWithValue("@title", post.Title)
            cmd.Parameters.AddWithValue("@post", post.PostText)
            cmd.Parameters.AddWithValue("@postCategoryID", post.PostCategoryID)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()

            If dr.HasRows Then
                dr.Read()
                status = dr("Status")
            End If

            dr.Close()
            cmd.Dispose()
            conn.Close()
        End Using

        Return status
    End Function

    Public Function EditPost(post As Post, newPost As Post) Implements IPost.EditPost
        Dim status = ""
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "DECLARE	@return_value int
                EXECUTE @return_value = [dbo].[EditPost] 
                          @userID
                          ,@title
                          ,@postCategory
                          ,@newTitle
                          ,@newPostText
                          ,@newPostCategory
                SELECT	'Return Value' = @return_value"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@userID", post.UserID)
            cmd.Parameters.AddWithValue("@title", post.Title)
            cmd.Parameters.AddWithValue("@postCategory", post.PostCategoryID)
            cmd.Parameters.AddWithValue("@newTitle", newPost.Title)
            cmd.Parameters.AddWithValue("@newPostText", newPost.PostText)
            cmd.Parameters.AddWithValue("@newPostCategory", newPost.PostCategoryID)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()

            If dr.HasRows Then
                dr.Read()
                status = dr("Message")
            End If

            dr.Close()
            cmd.Dispose()
            conn.Close()
        End Using

        Return status
    End Function

    Public Function DeletePost(ByVal title As String, ByVal userID As Integer) Implements IPost.DeletePost
        Dim status = ""
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "DECLARE	@return_value int
                EXEC	@return_value = [dbo].[DeletePost]
		                @title
                        ,@userID
                SELECT	'Return Value' = @return_value"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@title", title)
            cmd.Parameters.AddWithValue("@userID", userID)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()

            If dr.HasRows Then
                dr.Read()
                status = dr("Status")
            End If

            dr.Close()
            cmd.Dispose()
            conn.Close()
        End Using

        Return status
    End Function
    Public Function GetAllCategories() As List(Of Category) Implements IPost.GetAllCategories
        Dim categories As New List(Of Category)
        Try
            Dim strSql = "select * from PostCategory"

            conn = New SqlConnection(strConn)
            cmd = New SqlCommand(strSql, conn)
            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    Dim category As New Category With {
                        .PostCategoryID = CInt(dr("PostCategoryID")),
                        .Name = dr("Name").ToString()
                    }
                    categories.Add(category)
                End While
            End If
            dr.Close()

            Return categories
        Catch ex As Exception
            Throw
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function
End Class
