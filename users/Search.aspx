
<%@ Page Title="" Language="C#" MasterPageFile="~/users/FuzzyUser.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="users_Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <h1 style="text-align:center; margin-top:100px; color:black">Search Files</h1>
        <asp:Label ID="Label1" runat="server" Text="Enter a Keyword"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/image/search.png" Width="20px" height="15px" OnClick="ButtonClick"/><br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter a keyword to search" ControlToValidate="TextBox1" ForeColor="Red"></asp:RequiredFieldValidator>
        
        <br /><br />
        </div>
</asp:Content>

