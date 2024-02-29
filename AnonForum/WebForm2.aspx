<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="WebForm2.aspx.vb" Inherits="AnonForum.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="border-2 m-2">
        <p>Test</p>
        <asp:GridView ID="gvUsers"
            CssClass="table table-hover" DataKeyNames="UserID" AutoGenerateColumns="False"
            runat="server">
            <Columns>
                <asp:BoundField DataField="UserID" HeaderText="ID" />
                <asp:BoundField DataField="Username" HeaderText="Name" />
                
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" CssClass="btn btn-outline-danger btn-sm" CausesValidation="False" CommandName="Delete" Text="Delete" />
                        &nbsp;<asp:Button ID="Button2" runat="server" CssClass="btn btn-outline-primary btn-sm" CausesValidation="False" CommandName="Select" Text="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
