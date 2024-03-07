Imports AnonForum.BO

Public Interface IComment
    Sub LikeComment(commentID As Integer, postID As Integer, userID As Integer)
    Sub UnlikeComment(commentID As Integer, postID As Integer, userID As Integer)
    Sub DislikeComment(commentID As Integer, postID As Integer, userID As Integer)
    Sub UndislikeComment(commentID As Integer, postID As Integer, userID As Integer)
    Sub AddNewComment(comment As Comment)
    Function GetAllCommentbyPostID(postID As Integer) As IEnumerable(Of Comment)
    Function GetLike(commentID As Integer, postID As Integer, userID As Integer) As Boolean
    Function GetDislike(commentID As Integer, postID As Integer, userID As Integer) As Boolean
    Function DeleteComment(commentID As Integer, postID As Integer, userID As Integer)
    Function GetCommentbyUserIDandPostID(userID As Integer, postID As Integer) As Comment
End Interface
