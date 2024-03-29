﻿Imports AnonForum.BO

Public Interface IUser
    Function AddNewUser(user As UserAuth)
    Function GetAllUser() As IEnumerable(Of UserAuth)
    Function UserLogin(usernameOrEmail As String, password As String) As UserAuth
    Function EditNickname(username As String, Nickname As String)
    Function DeleteUser(username As String)
    Function GetbyUsername(username As String) As UserAuth
    Function GetbyID(id As Integer) As UserAuth

End Interface
