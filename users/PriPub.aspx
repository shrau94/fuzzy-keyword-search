<%@ Page Title="" Language="C#" MasterPageFile="~/users/FuzzyUser.master" AutoEventWireup="true" CodeFile="PriPub.aspx.cs" Inherits="users_PriPub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div style="text-align:center;">
        <p style="text-align:center;"><br />Enter password for the private file:</p>
    <asp:Label ID="Label3" runat="server" Text="Password"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="TextBox3" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvpwd" 
       runat="server" 
       ControlToValidate="TextBox3" 
       ErrorMessage=" * Enter Password" Display="Dynamic" Style="color: red">
        </asp:RequiredFieldValidator>
       
    <br /><br />
    <asp:Label ID="Label4" runat="server" Text="Retype password"></asp:Label>
    &nbsp;&nbsp;
    <asp:TextBox ID="TextBox4" runat="server" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvpwd2" 
       runat="server" 
       ControlToValidate="TextBox4" 
       ErrorMessage=" * Renter the Password" Display="Dynamic" Style="color: red">
        </asp:RequiredFieldValidator>
       <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage=" * Does not match with the given Password" 
           Display="Dynamic" Style="color: red" controlToCompare="TextBox4" ControlToValidate="TextBox3">
       </asp:CompareValidator> 
       
       <br /><br />
    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="btnsubmit_Click" />
        </div>
</asp:Content>

