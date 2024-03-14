Imports System.Data.SqlClient
Imports AnonForum.BO

Public Class CommunityDAL
    Implements ICommunity

    Private ReadOnly strConn As String
    Private conn As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader


    Public Sub New()
        strConn = Helper.GetConnectionString()
    End Sub
    Public Function GetAll() As IEnumerable(Of Community) Implements ICommunity.GetAll
        Dim communities As New List(Of Community)
        Try
            Dim strSql = "SELECT * FROM dbo.Community"

            conn = New SqlConnection(strConn)
            cmd = New SqlCommand(strSql, conn)
            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    Dim community As New Community With {
                        .CommunityID = CInt(dr("CommunityID")),
                        .ComunityName = dr("CommunityName").ToString()
                    }
                    communities.Add(community)
                End While
            End If
            dr.Close()

            Return communities
        Catch ex As Exception
            Throw ex
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function
    Public Function GetByID(id As Integer) As Community Implements ICommunity.GetByID
        Dim community As New Community()
        Try
            Using conn As New SqlConnection(strConn)
                Dim strSql = "SELECT * FROM dbo.Community where CommunityID=@id"
                Dim cmd As New SqlCommand(strSql, conn)
                cmd.Parameters.AddWithValue("@id", id)
                conn.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()

                If dr.HasRows Then
                    dr.Read()
                    community.CommunityID = CInt(dr("CommunityID"))
                    community.ComunityName = dr("CommunityName").ToString()
                End If

                dr.Close()
                cmd.Dispose()
                conn.Close()
            End Using
        Catch ex As Exception
            Throw New ArgumentException(ex.Message)
        End Try
        Return community
    End Function
End Class