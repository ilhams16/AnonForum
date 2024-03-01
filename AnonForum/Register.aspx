<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.vb" Inherits="AnonForum.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3">
        <h2 class="text-center">Register</h2>
        <div class="d-flex justify-content-center">
            <asp:TextBox ID="txtUsername" runat="server" placeholder="Username" CssClass="rounded-2 m-1"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-center">
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="rounded-2 m-1"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-center">
            <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" CssClass="rounded-2 m-1"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-center">
            <asp:TextBox ID="txtNick" runat="server" placeholder="Nickname" CssClass="rounded-2 m-1"></asp:TextBox>
        </div>
        <div class="d-flex justify-content-center">
            <asp:FileUpload ID="fileImage" runat="server" placeholder="Image" CssClass="m-1" />
        </div>
        <div>
            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="m-1" />
        </div>
        <div>
            <p>Have an Account?</p>
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="m-1" />
        </div>
    </div>
</asp:Content>
