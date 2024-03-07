<%@ Page Title="Register" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Register.aspx.vb" Inherits="AnonForum._Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .gradient-custom {
            /* fallback for old browsers */
            background: #6a11cb;
            /* Chrome 10-25, Safari 5.1-6 */
            background: -webkit-linear-gradient(to right, rgba(106, 17, 203, 1), rgba(37, 117, 252, 1));
            /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
            background: linear-gradient(to right, rgba(106, 17, 203, 1), rgba(37, 117, 252, 1))
        }
    </style>
    <section class="vh-150 gradient-custom">
        <div class="container py-5 h-100">
            <div class="row d-flex justify-content-center align-items-center h-100">
                <div class="col-12 col-md-8 col-lg-6 col-xl-5">
                    <div class="card bg-dark text-white" style="border-radius: 1rem;">
                        <div class="card-body p-5 text-center">

                            <div class="mb-md-5 mt-md-4 pb-5">

                                <h2 class="fw-bold mb-2 text-uppercase">Register</h2>
                                <p class="text-white-50 mb-5">Please fill the form register!</p>
                                <asp:Label ID="lblMessage" CssClass="mt-3" runat="server" ForeColor="red"></asp:Label>

                                <div class="form-outline form-white mb-4">
                                    <asp:TextBox runat="server" ID="Username" class="form-control form-control-lg text-bg-dark mx-auto" />
                                    <label class="form-label" for="typeUsernameX">Username</label>
                                </div>
                                <div class="form-outline form-white mb-4">
                                    <asp:TextBox runat="server" TextMode="Email" ID="Email" class="form-control form-control-lg text-bg-dark mx-auto" />
                                    <label class="form-label" for="typeEmailX">Email</label>
                                </div>
                                <div class="form-outline form-white mb-4">
                                    <asp:TextBox runat="server" ID="Nickname" class="form-control form-control-lg text-bg-dark mx-auto" />
                                    <label class="form-label" for="typeUsernameX">Username</label>
                                </div>
                                <div class="form-outline form-white mb-4">
                                    <asp:TextBox runat="server" TextMode="Password" ID="Password" class="form-control form-control-lg text-bg-dark mx-auto" />
                                    <label class="form-label" for="typePasswordX">Password</label>
                                </div>

                                <div class="form-outline form-white mb-4">
                                    <asp:TextBox runat="server" TextMode="Password" ID="CPassword" class="form-control form-control-lg text-bg-dark mx-auto" />
                                    <label class="form-label" for="typePasswordX">Confirm Password</label>
                                </div>
                                <div class="form-outline form-white mb-4">
                                    <asp:FileUpload runat="server" ID="Image" placeholder="Image" class="form-control form-control-lg text-bg-dark mx-auto" />
                                    <label class="form-label" for="typePasswordX">Image</label>
                                </div>

                                <asp:Button ID="btnRegister" class="btn btn-outline-light btn-lg px-5" runat="server" Text="Login" OnClick="btnRegister_Click" />

                                <div class="d-flex justify-content-center text-center mt-4 pt-1">
                                    <a href="#!" class="text-white"><i class="fab fa-facebook-f fa-lg"></i></a>
                                    <a href="#!" class="text-white"><i class="fab fa-twitter fa-lg mx-4 px-2"></i></a>
                                    <a href="#!" class="text-white"><i class="fab fa-google fa-lg"></i></a>
                                </div>

                            </div>

                            <div>
                                <p class="mb-0">
                                    Have an account? <a href="/Login" class="text-white-50 fw-bold">Login</a>
                                </p>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
