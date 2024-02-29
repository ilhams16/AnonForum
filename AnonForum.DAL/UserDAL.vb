Imports AnonForum.BO
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

Public Class UserDAL
    Implements IUser

    Private ReadOnly strConn As String
    Private conn As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader


    Public Sub New()
        strConn = New SqlConnection(ConfigurationManager.AppSettings.Get("MyDbConnectionString")).ConnectionString
    End Sub

    Public Function GetAll() As List(Of UserAuth) Implements IUser.GetAllUser
        Dim UserAuths As New List(Of UserAuth)
        Try
            Dim strSql = "SELECT * FROM dbo.UserAuth"

            conn = New SqlConnection(strConn)
            cmd = New SqlCommand(strSql, conn)
            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    Dim user As New UserAuth With {
                        .UserID = CInt(dr("UserID")),
                        .Username = dr("Username").ToString(),
                        .Email = dr("Email").ToString(),
                        .Nickname = dr("Nickname").ToString()
                    }
                    UserAuths.Add(user)
                End While
            End If
            dr.Close()

            Return UserAuths
        Catch ex As Exception
            Throw ex
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function

    Public Function UserLogin(ByVal usernameOrEmail As String, ByVal password As String) As UserAuth Implements IUser.UserLogin
        Dim user As New UserAuth()
        Try
            Using conn As New SqlConnection(strConn)
                Dim strSql As String = "[dbo].[UserLogin]"
                Dim cmd As New SqlCommand(strSql, conn) With {
                    .CommandType = CommandType.StoredProcedure
                }
                cmd.Parameters.AddWithValue("@usernameoremail", usernameOrEmail)
                cmd.Parameters.AddWithValue("@password", password)
                conn.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()

                If dr.HasRows Then
                    dr.Read()
                    user.UserID = CInt(dr("UserID"))
                    user.Username = dr("Username").ToString()
                    user.Email = dr("Email").ToString()
                    user.Nickname = dr("Nickname").ToString()
                    user.Password = dr("Password").ToString()
                End If

                dr.Close()
                cmd.Dispose()
                conn.Close()
            End Using
        Catch ex As Exception
            Throw New ArgumentException(ex.Message)
        End Try


        Return user
    End Function

    Public Function CreateUser(user As UserAuth) Implements IUser.AddNewUser
        Dim status = ""
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "DECLARE	@return_value int
                EXEC	@return_value = [dbo].[NewUser]
		                @username,
		                @email,
		                @password,
		                @nickname
                SELECT	'Return Value' = @return_value"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@username", user.Username)
            cmd.Parameters.AddWithValue("@email", user.Email)
            cmd.Parameters.AddWithValue("@password", user.Password)
            cmd.Parameters.AddWithValue("@nickname", user.Nickname)
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

    Public Function EditNickname(ByVal username As String, ByVal nickname As String) Implements IUser.EditNickname
        Dim user As New UserAuth()
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "DECLARE	@return_value int
                                EXECUTE @return_value = [dbo].[EditNickname] 
                                       @Username
                                      ,@Nickname
                                       GO
                                SELECT	'Return Value' = @return_value"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@Username", username)
            cmd.Parameters.AddWithValue("@Nickname", nickname)
            conn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()

            If dr.HasRows Then
                dr.Read()
                user.UserID = CInt(dr("UserID"))
                user.Username = dr("Username").ToString()
                user.Email = dr("Email").ToString()
                user.Nickname = dr("Nickname").ToString()
                user.Password = dr("Password").ToString()
            End If

            dr.Close()
            cmd.Dispose()
            conn.Close()
        End Using

        Return user
    End Function

    Public Function DeleteUser(ByVal username As String) Implements IUser.DeleteUser
        Dim status = ""
        Using conn As New SqlConnection(strConn)
            Dim strSql As String = "DECLARE	@return_value int
                EXEC	@return_value = [dbo].[DeleteUser]
		                @username
                SELECT	'Return Value' = @return_value"
            Dim cmd As New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@username", username)
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
