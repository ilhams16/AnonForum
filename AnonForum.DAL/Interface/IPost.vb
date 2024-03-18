Imports AnonForum.BO

Public Interface IPost
    Sub AddNewPost(post As Post)
    Function GetAllPost() As List(Of Post)
    Function GetAllCategories() As List(Of Category)
    Sub LikePost(postID As Integer, userID As Integer)
    Sub UnlikePost(postID As Integer, userID As Integer)
    Function GetLike(postID As Integer, userID As Integer) As Boolean
    Sub DislikePost(postID As Integer, userID As Integer)
    Sub UndislikePost(postID As Integer, userID As Integer)
    Function GetDislike(postID As Integer, userID As Integer) As Boolean
    Sub DeletePost(postID As Integer)
    Sub EditPost(postID As Integer, newPost As Post)
    Function GetPostbyID(postID As Integer) As Post
    Function GetAllPostbyCategories(catID As Integer) As List(Of Post)
    Function GetAllPostbySearch(query As String) As List(Of Post)
    Function GetUserLike(postID As Integer) As IEnumerable(Of Integer)
    Function GetUserDislike(postID As Integer) As IEnumerable(Of Integer)
End Interface
