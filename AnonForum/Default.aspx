<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="AnonForum._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <div>
                <asp:Repeater ID="postRepeater" runat="server">
                    <ItemTemplate>
                        <div class="rounded-2 bg-light p-3 border-bottom border-2 mt-1">
                            <div class="d-flex align-items-center justify-content-start">
                                <img src="Assets/user.png" class="img rounded-circle" width="20" height="20" alt="User Profile Image" />
                                <asp:Label ID="Username" CssClass="ms-2" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                            </div>
                            <div class="my-3">
                                <h3 class="text-center">
                                    <asp:Label ID="Title" CssClass="text-center" runat="server" Text='<%# Eval("Title") %>'></asp:Label></h3>
                                <p class="text-start"><%# Eval("PostText") %></p>
                            </div>
                            <div class="d-block">
                                <div class="d-flex align-items-center justify-content-start m-3">
                                    <asp:Button ID="btnLike" runat="server" CssClass="btn-primary" Text="Like" CommandName="likePost" /><h4 class="text-center mx-2"><%# Eval("TotalLikes") %></h4>
                                    <asp:Button ID="btnDislike" runat="server" CssClass="btn-primary" text="Dislike" /><h4 class="text-center mx-2"><%# Eval("TotalDislikes") %></h4>
                                    <asp:Button ID="btnComment" runat="server" cssClass="btn-primary" text="Comment"/>
                                </div>
                            </div>
                            <div class="text-end">
                                <p class="text-muted">Published on: <%# Eval("TimeStamp", "{0:MM/dd/yyyy}") %></p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>

    </main>

</asp:Content>
