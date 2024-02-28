Imports AnonForum.BO

Public Interface IPost
    Function AddNewPost(post As Post)
    Function GetAllPost() As List(Of Post)
    Function GetPostbyTitle(title As String) As List(Of Post)
    Function EditPost(post As Post, newPost As Post)
    Function DeletePost(title As String, userID As Integer)
End Interface
