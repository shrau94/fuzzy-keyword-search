<%@ Page Title="" Language="C#" MasterPageFile="~/fuzzy.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <h1 style="text-align:center; margin-top:100px; color:black">Register Here!!</h1>
    <p style="text-align:center; color:black">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label1" runat="server" Text="Name" ></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvname" 
       runat="server" 
       ControlToValidate="TextBox1" 
       ErrorMessage=" * Enter your name" Display="Dynamic" Style="color: red">
        </asp:RequiredFieldValidator>
     
    <br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label2" runat="server" Text="Email-id"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="TextBox2" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvmail" 
       runat="server" 
       ControlToValidate="TextBox2" 
       ErrorMessage=" * Enter your email id" Display="Dynamic" Style="color: red">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="remail" 
      runat="server" 
      ControlToValidate="TextBox2" ErrorMessage=" * Enter your email correctly" 
            Display="Dynamic" Style="color: red"
ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
</asp:RegularExpressionValidator>
       
    <br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Text="Password"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
      
        </p>
    
        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="btnsubmit_Click" />
        <br />
     </div>
</asp:Content>

