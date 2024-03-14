Imports AnonForum.BO

Public Interface ICommunity
    Function GetAll() As IEnumerable(Of Community)
    Function GetByID(id As Integer) As Community
End Interface
