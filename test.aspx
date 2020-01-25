<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox> <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
       runat="server" 
       ControlToValidate="TextBox1" 
       ErrorMessage=" * Enter your email-id for login" Display="Dynamic" Style="color: red">
        </asp:RequiredFieldValidator>
    <asp:Label ID="Label2" runat="server" Text="Email"></asp:Label>
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox> <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
       runat="server" 
       ControlToValidate="TextBox2" 
       ErrorMessage=" * Enter your email-id for login" Display="Dynamic" Style="color: red">
        </asp:RequiredFieldValidator>
         <asp:RegularExpressionValidator ID="remail" 
      runat="server" 
      ControlToValidate="TextBox2" ErrorMessage=" * Enter your email correctly" 
            Display="Dynamic" Style="color: red"
ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
</asp:RegularExpressionValidator>
    <asp:Label ID="Label3" runat="server" Text="Password" Style="float:left" />
       <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox> <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
       runat="server" 
       ControlToValidate="TextBox3" 
       ErrorMessage=" * Enter your email-id for login" Display="Dynamic" Style="color: red">
        </asp:RequiredFieldValidator>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
