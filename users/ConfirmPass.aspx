<%@ Page Title="" Language="C#" MasterPageFile="~/users/FuzzyUser.master" AutoEventWireup="true" CodeFile="ConfirmPass.aspx.cs" Inherits="users_ConfirmPass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <asp:TextBox ID="TextBox1" runat="server" TextMode="Password" ></asp:TextBox>
        <asp:RequiredFieldValidator  ID="rfvname" 
       runat="server" 
       ControlToValidate="TextBox1" 
       ErrorMessage=" * Enter the password" Display="Dynamic" Style="color: red">
        </asp:RequiredFieldValidator>
    <br /><br />
    <asp:Button ID="Button1" runat="server" Text="Download" OnClick="btnsubmit_Click" />
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="red" EnableViewState="false"></asp:Label>
</asp:Content>

