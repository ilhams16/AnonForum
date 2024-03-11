Imports System.Data.SqlClient
Imports System.Text
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
                            on p.UserID = ua.UserID
							join PostCategory pc
							on p.PostCategoryID = pc.PostCategoryID
							order by p.TimeStamp desc"

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
                        .TimeStamp = DirectCast(dr("TimeStamp"), Date),
                        .PostCategoryID = CInt(dr("PostCategoryID")),
                        .Image = dr("Image").ToString(),
                        .TotalLikes = CInt(dr("TotalLikes")),
                        .TotalDislikes = CInt(dr("TotalDislikes")),
                        .Username = dr("Username").ToString(),
                        .CategoryName = dr("Name").ToString(),
                        .UserImage = dr("UserImage").ToString()
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

    Public Function GetPostbyID(ByVal postID As Integer) As Post Implements IPost.GetPostbyID
        Try
            Dim resPost As New Post
            Dim strSql = "select * from Posts p
                            join UserAuth ua
                            on p.UserID = ua.UserID
                            where p.PostID = @postID"
            conn = New SqlConnection(strConn)
            cmd = New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@postID", postID)
            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.HasRows Then
                dr.Read()
                resPost.UserID = CInt(dr("UserID"))
                resPost.PostID = CInt(dr("PostID"))
                resPost.Title = dr("Title").ToString()
                resPost.PostText = dr("PostText").ToString()
                resPost.TimeStamp = dr("TimeStamp").ToString()
                resPost.PostCategoryID = CInt(dr("PostCategoryID"))
                resPost.Image = dr("Image").ToString()
                resPost.TotalLikes = CInt(dr("TotalLikes"))
                resPost.TotalDislikes = CInt(dr("TotalDislikes"))
                resPost.Username = dr("Username").ToString()
                resPost.UserImage = dr("UserImage").ToString()
            End If
            dr.Close()
            Return resPost
        Catch ex As Exception
            Throw
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function

    Public Sub CreatePost(post As Post) Implements IPost.AddNewPost
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "DECLARE	@return_value int
                EXEC	@return_value = [dbo].[NewPost]
		                @userID
                        ,@title
                        ,@post
                        ,@postCategoryID
,@image
                SELECT	'Return Value' = @return_value"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@userID", post.UserID)
            cmd.Parameters.AddWithValue("@title", post.Title)
            cmd.Parameters.AddWithValue("@post", post.PostText)
            cmd.Parameters.AddWithValue("@postCategoryID", post.PostCategoryID)
            cmd.Parameters.AddWithValue("@image", post.Image)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()

            dr.Close()
            cmd.Dispose()
            conn.Close()
        End Using
    End Sub

    Public Sub EditPost(postID As Integer, newPost As Post) Implements IPost.EditPost
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "DECLARE	@return_value int
                EXECUTE @return_value = [dbo].[EditPost] 
                          @postID
                          ,@newPostCategory
                          ,@newTitle
                          ,@newPostText
                SELECT	'Return Value' = @return_value"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@postID", postID)
            cmd.Parameters.AddWithValue("@newTitle", newPost.Title)
            cmd.Parameters.AddWithValue("@newPostText", newPost.PostText)
            cmd.Parameters.AddWithValue("@newPostCategory", newPost.PostCategoryID)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            dr.Close()
            cmd.Dispose()
            conn.Close()
        End Using
    End Sub

    Public Sub DeletePost(postID As Integer) Implements IPost.DeletePost
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "DECLARE	@return_value int
EXEC	@return_value = [dbo].[DeletePost]
		@postID
SELECT	'Return Value' = @return_value
GO"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@postID", postID)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            dr.Close()
            cmd.Dispose()
            conn.Close()
        End Using
    End Sub
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
    Public Sub LikePost(ByVal postID As Integer, ByVal userID As Integer) Implements IPost.LikePost
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "DECLARE	@return_value int
                EXEC	@return_value = [dbo].[LikePostSP]
		                @userID
                        ,@postID
                SELECT	'Return Value' = @return_value"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@userID", userID)
            cmd.Parameters.AddWithValue("@postID", postID)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        End Using
    End Sub
    Public Sub UnlikePost(ByVal postID As Integer, ByVal userID As Integer) Implements IPost.UnlikePost
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "delete from LikePost where PostID = @postID and UserID = @userID"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@userID", userID)
            cmd.Parameters.AddWithValue("@postID", postID)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        End Using
    End Sub
    Public Function GetLike(ByVal postID As Integer, ByVal userID As Integer) As Boolean Implements IPost.GetLike
        Try
            Dim res As Boolean
            Dim strSql = "select * from LikePost where PostID = @postID and UserID = @userID"
            conn = New SqlConnection(strConn)
            cmd = New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@postID", postID)
            cmd.Parameters.AddWithValue("@userID", userID)
            conn.Open()
            dr = cmd.ExecuteReader()
            res = dr.HasRows
            dr.Close()
            Return res
        Catch ex As Exception
            Throw
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function
    Public Sub DislikePost(ByVal postID As Integer, ByVal userID As Integer) Implements IPost.DislikePost
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "DECLARE	@return_value int
                EXEC	@return_value = [dbo].[UnlikePostSP]
		                @userID
                        ,@postID
                SELECT	'Return Value' = @return_value"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@userID", userID)
            cmd.Parameters.AddWithValue("@postID", postID)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        End Using
    End Sub
    Public Sub UndislikePost(ByVal postID As Integer, ByVal userID As Integer) Implements IPost.UndislikePost
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "delete from UnlikePost where PostID = @postID and UserID = @userID"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@userID", userID)
            cmd.Parameters.AddWithValue("@postID", postID)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        End Using
    End Sub
    Public Function GetDislike(ByVal postID As Integer, ByVal userID As Integer) As Boolean Implements IPost.GetDislike
        Try
            Dim res As Boolean
            Dim strSql = "select * from UnlikePost where PostID = @postID and UserID = @userID"
            conn = New SqlConnection(strConn)
            cmd = New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@postID", postID)
            cmd.Parameters.AddWithValue("@userID", userID)
            conn.Open()
            dr = cmd.ExecuteReader()
            res = dr.HasRows
            dr.Close()
            Return res
        Catch ex As Exception
            Throw
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function
End Class
