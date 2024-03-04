Imports AnonForum.BO

Public Interface IPost
    Function AddNewPost(post As Post)
    Function GetAllPost() As List(Of Post)
    Function GetPostbyTitleandUsername(title As String, username As String) As Post
    Function EditPost(post As Post, newPost As Post)
    Function DeletePost(title As String, userID As Integer)
    Function GetAllCategories() As List(Of Category)
    Function LikePost(userID As Integer, postID As Integer)
End Interface
