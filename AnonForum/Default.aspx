<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="AnonForum._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <div class="container">
                <asp:Repeater ID="postRepeater" runat="server">
                    <ItemTemplate>
                        <div class="card my-2 mx-auto p-2 rounded-2 bg-light">
                            <h2><%# Eval("Title") %></h2>
                            <p><%# Eval("PostText") %></p>
                            <p>Published on: <%# Eval("TimeStamp", "{0:MMMM dd, yyyy}") %></p>
                            <asp:Button ID="commentButton" runat="server" Text="Comment" OnClick="CommentButton_Click" />
                            <asp:Button ID="likeButton" runat="server" Text="Like" OnClick="LikeButton_Click" />
                            <asp:Button ID="unlikeButton" runat="server" Text="Unlike" OnClick="UnlikeButton_Click" Visible="false" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>
    </main>

</asp:Content>
