<%@ Page Title="" Language="C#" MasterPageFile="~/users/FuzzyUser.master" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="users_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <h1 style="text-align:center;color:black">Upload Files</h1>
            <asp:FileUpload ID="file1" runat="server" />
            




            <br /><br />
            <asp:Label ID="Label6" runat="server" Text="Give atleast two Keywords to search your file"></asp:Label><br /><br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:Label ID="Label1" runat="server" Text="Keyword1"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Enter a keyword" ControlToValidate="TextBox1" ForeColor="Red"></asp:RequiredFieldValidator>
             <br /><br />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><asp:Label ID="Label2" runat="server" Text="Keyword2"></asp:Label>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Enter a keyword" ControlToValidate="TextBox2" ForeColor="Red"></asp:RequiredFieldValidator>
            <br /><br />
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><asp:Label ID="Label3" runat="server" Text="Keyword3"></asp:Label>
            <asp:Label ID="Label7" runat="server" Text="Optional Keyword"></asp:Label>
            <br /><br />
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox><asp:Label ID="Label4" runat="server" Text="Keyword4"></asp:Label>
            <asp:Label ID="Label8" runat="server" Text="Optional Keyword"></asp:Label>
            <br /><br />
            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox><asp:Label ID="Label5" runat="server" Text="Keyword5"></asp:Label>
            <asp:Label ID="Label9" runat="server" Text="Optional Keyword"></asp:Label>
            <br /><br />
    <asp:Label ID="Label10" runat="server" Text="Keep the file as:"></asp:Label>
            <br /><br />
       <div style="text-align:center;padding-left:45%;">
    <asp:RadioButtonList ID="access" runat="server">
            <asp:ListItem Text="Public" Selected="True" />
            <asp:ListItem Text="Private" />
            </asp:RadioButtonList>
        </div>
    <br /><br />
            <asp:Button ID="Button1" runat="server" Text="Upload" OnClick="btnUpload_Click" />


    </asp:Content>

