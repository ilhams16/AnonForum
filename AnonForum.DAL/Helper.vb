Public Class Helper
    Public Shared Function GetConnectionString() As String
        'If System.Configuration.ConfigurationManager.ConnectionStrings("MyDbConnectionString") Is Nothing Then
        '    Dim MyConfig = New ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
        '    Return MyConfig.GetConnectionString("MyDbConnectionString")
        'End If
        Dim connString = System.Configuration.ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString
        Return connString
    End Function
End Class