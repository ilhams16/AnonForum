Imports AnonForum.BO

Public Interface IPost
    Function AddNewPost(post As Post)
    Function GetAllPost() As List(Of Post)
    Function GetPostbyTitleandUsername(title As String, username As String) As Post
    Function EditPost(post As Post, newPost As Post)
    Function DeletePost(title As String, userID As Integer)
    Function GetAllCategories() As List(Of Category)
    Sub LikePost(postID As Integer, userID As Integer)
    Sub UnlikePost(postID As Integer, userID As Integer)
    Function GetLike(postID As Integer, userID As Integer) As Boolean
    Sub DislikePost(postID As Integer, userID As Integer)
    Sub UndislikePost(postID As Integer, userID As Integer)
    Function GetDislike(postID As Integer, userID As Integer) As Boolean
End Interface
