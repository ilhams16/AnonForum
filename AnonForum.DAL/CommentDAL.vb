Imports AnonForum.BO
Imports System.Data.SqlClient

Public Class CommentDAL
    Implements IComment
    Private ReadOnly strConn As String
    Private conn As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader

    Public Sub New()
        strConn = "Server=.\BSISqlExpress;Database=AnonForum;Trusted_Connection=True;"
        conn = New SqlConnection(strConn)
    End Sub
    Public Function GetAllCommentbyPostID(postID As Integer) As IEnumerable(Of Comment) Implements IComment.GetAllCommentbyPostID
        Dim comments As New List(Of Comment)
        Try
            Dim strSql = "select * from Comments c
                          join UserAuth ua
                          on c.UserID = ua.UserID
                          where PostID=@postID"
            conn = New SqlConnection(strConn)
            cmd = New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@postID", postID)
            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    Dim comment As New Comment With {
                        .CommentID = CInt(dr("CommentID")),
                        .UserID = CInt(dr("UserID")),
                        .PostID = dr("PostID").ToString(),
                        .Comment = dr("Comment").ToString(),
                        .TimeStamp = DirectCast(dr("TimeStamp"), Date),
                        .TotalLikes = CInt(dr("TotalLikes")),
                        .TotalDislikes = CInt(dr("TotalDislikes")),
                        .Username = dr("Username").ToString()
                    }
                    comments.Add(comment)
                End While
            End If
            dr.Close()

            Return comments
        Catch ex As Exception
            Throw
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function
    Public Function GetCommentbyUserIDandPostID(ByVal userID As Integer, ByVal postID As Integer) As Comment Implements IComment.GetPostbyTitleandUsername
        Try
            Dim resPost As New Comment
            Dim strSql = "select * from Comments c
                            join UserAuth ua
                            on c.UserID = ua.UserID
                            where c.UserID = @userID and c.PostID = @postID"
            conn = New SqlConnection(strConn)
            cmd = New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@userID", userID)
            cmd.Parameters.AddWithValue("@postID", postID)
            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.HasRows Then
                dr.Read()
                resPost.CommentID = CInt(dr("CommentID"))
                resPost.UserID = CInt(dr("UserID"))
                resPost.PostID = CInt(dr("PostID"))
                resPost.Comment = dr("Comment").ToString()
                resPost.TimeStamp = dr("TimeStamp").ToString()
                resPost.TotalLikes = CInt(dr("TotalLikes"))
                resPost.TotalDislikes = CInt(dr("TotalDislikes"))
                resPost.Username = dr("Username").ToString()
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
    Public Sub LikeComment(ByVal commentID As Integer, ByVal postID As Integer, ByVal userID As Integer) Implements IComment.LikeComment
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "DECLARE	@return_value int
                EXEC	@return_value = [dbo].[LikeCommentSP]
                         @commentID
		                ,@userID
                        ,@postID
                SELECT	'Return Value' = @return_value"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@commentID", commentID)
            cmd.Parameters.AddWithValue("@userID", userID)
            cmd.Parameters.AddWithValue("@postID", postID)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        End Using
    End Sub
    Public Sub UnlikeComment(ByVal commentID As Integer, ByVal postID As Integer, ByVal userID As Integer) Implements IComment.UnlikeComment
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "delete from UnlikeComment where CommentID = @commentID and PostID = @postID and UserID = @userID"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@commentID", commentID)
            cmd.Parameters.AddWithValue("@userID", userID)
            cmd.Parameters.AddWithValue("@postID", postID)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        End Using
    End Sub
    Public Function GetLike(ByVal commentID As Integer, ByVal postID As Integer, ByVal userID As Integer) As Boolean Implements IComment.GetLike
        Try
            Dim res As Boolean
            Dim strSql = "select * from LikeComment where CommentID = @commentID and PostID = @postID and UserID = @userID"
            conn = New SqlConnection(strConn)
            cmd = New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@commentID", commentID)
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
    Public Sub DislikeComment(ByVal commentID As Integer, ByVal postID As Integer, ByVal userID As Integer) Implements IComment.DislikeComment
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "DECLARE	@return_value int
                EXEC	@return_value = [dbo].[UnlikeCommentSP]
                         @commentID
		                ,@userID
                        ,@postID
                SELECT	'Return Value' = @return_value"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@commentID", commentID)
            cmd.Parameters.AddWithValue("@userID", userID)
            cmd.Parameters.AddWithValue("@postID", postID)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        End Using
    End Sub
    Public Sub UndislikeComment(ByVal commentID As Integer, ByVal postID As Integer, ByVal userID As Integer) Implements IComment.UndislikeComment
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "delete from UnlikeComment where CommentID = @commentID and PostID = @postID and UserID = @userID"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@commentID", commentID)
            cmd.Parameters.AddWithValue("@userID", userID)
            cmd.Parameters.AddWithValue("@postID", postID)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        End Using
    End Sub
    Public Function GetDislike(ByVal commentID As Integer, ByVal postID As Integer, ByVal userID As Integer) As Boolean Implements IComment.GetDislike
        Try
            Dim res As Boolean
            Dim strSql = "select * from UnlikeComment where CommentID = @commentID and PostID = @postID and UserID = @userID"
            conn = New SqlConnection(strConn)
            cmd = New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@commentID", commentID)
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
    Public Function DeleteComment(ByVal commentID As Integer, ByVal postID As Integer, ByVal userID As Integer) Implements IComment.DeleteComment
        Dim status = ""
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "DECLARE	@return_value int
                EXEC	@return_value = [dbo].[DeleteComment]
                        @commentID
		                ,@postID
                        ,@userID
                SELECT	'Return Value' = @return_value"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@commentID", commentID)
            cmd.Parameters.AddWithValue("@postID", postID)
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
End Class
