<%@ Page Title="Login" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.vb" Inherits="AnonForum.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <div class="center align-content-center justify-content-center">
            <h2 class="text-center">Login</h2>
            <div class="m-auto center form-control text-center border-2">
                <asp:Label ID="lblMessage" CssClass="mt-3" runat="server" Text="" ForeColor="green"></asp:Label>
                <br />
                <asp:TextBox ID="txtUsername" CssClass="mt-3 rounded-1" runat="server" placeholder="Username"></asp:TextBox>
                <br />
                <asp:TextBox ID="txtPassword" CssClass="mt-3 rounded-1" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
                <br />
                <asp:Button ID="btnLogin" CssClass="my-3 bg-primary rounded-5" runat="server" Text="Login" OnClick="btnLogin_Click" />
            </div>
            <p>Not have an Account?</p>
            <asp:Button ID="btnRegister" CssClass="my-3 bg-secondary d-flex" runat="server" Text="Register" OnClick="btnRegister_Click" />
        </div>
    </main>
</asp:Content>
