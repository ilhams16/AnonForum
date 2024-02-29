<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="AnonForum._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <div class="container">
                <asp:Repeater ID="postRepeater" runat="server">
                    <ItemTemplate>
                        <div class="card my-2 mx-auto p-2 rounded-2 bg-light">
                            <div class="d-flex align-items-center justify-content-start">
                                <img src="Assets/user.png" class="img rounded-circle" width="20" height="20" alt="User Profile Image" />
                                <h4 class="ms-2"><%# Eval("Username") %></h4>
                            </div>
                            <div class="my-3">
                                <h3 class="text-center"><%# Eval("Title") %></h3>
                                <p class="text-start"><%# Eval("PostText") %></p>
                            </div>
                            <div class="d-block">
                                <div class="d-flex align-items-center justify-content-start">
                                    <asp:ImageButton ID="btnLike" runat="server" src="Assets/like.png" Width="50" class="ms-2 btn" />
                                    <asp:ImageButton ID="btnDislike" runat="server" src="Assets/dislike.png" Width="50" class="ms-2 btn" />
                                    <asp:ImageButton ID="btnComment" runat="server" src="Assets/comment.png" Width="50" class="ms-2 btn" />
                                </div>
                                <div class="d-flex align-items-center justify-content-start">
                                    <h4 class="ms-2 mx-6"><%# Eval("TotalLikes") %></h4>
                                    <h4 class="ms-2 mx-6"><%# Eval("TotalDislikes") %></h4>
                                </div>
                            </div>
                            <div class="text-end">
                                <p class="text-muted">Published on: <%# Eval("TimeStamp", "{0:MMMM dd, yyyy}") %></p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>

    </main>

</asp:Content>
