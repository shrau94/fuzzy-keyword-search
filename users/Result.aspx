<%@ Page Title="" Language="C#" MasterPageFile="~/users/FuzzyUser.master" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="users_Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div> <asp:GridView ID="GridView1" runat="server" Font-Size="20px" HorizontalAlign="Center" DataKeyNames="FileId,FilePath">
       <Columns>
                <asp:BoundField DataField="FileId" HeaderText="FileId" ReadOnly="True" SortExpression="FileId" />
                <asp:BoundField DataField="FileName" HeaderText="FileName" SortExpression="FileName" />
                <asp:BoundField DataField="FilePath" HeaderText="FilePath" SortExpression="FilePath" />
                <asp:TemplateField HeaderText="Download" HeaderStyle-HorizontalAlign="Center">
<ItemTemplate>
<asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="lnkDownload_Click" ></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        
         </asp:GridView>
       </div>
</asp:Content>

