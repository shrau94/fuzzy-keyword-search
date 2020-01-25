<%@ Page Title="" Language="C#" MasterPageFile="~/fuzzy.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <h1 style="text-align:center; margin-top:100px; color:black">Admin Log-in</h1>
    <p style="text-align:center; color:black"><asp:Label ID="Label1" runat="server" Text="Email-id"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
       runat="server" 
       ControlToValidate="TextBox1" 
       ErrorMessage=" * Enter your email-id for login" Display="Dynamic" Style="color: red">
        </asp:RequiredFieldValidator>
    <br /><br />
    <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
    <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" ></asp:TextBox>
        <asp:RequiredFieldValidator  ID="rfvname" 
       runat="server" 
       ControlToValidate="TextBox2" 
       ErrorMessage=" * Enter the password" Display="Dynamic" Style="color: red">
        </asp:RequiredFieldValidator>
    <br /><br />
    <asp:Button ID="Button1" runat="server" Text="Login" OnClick="btnLogin_Click"  />
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="red" EnableViewState="false"></asp:Label>
    </div>
</asp:Content>

