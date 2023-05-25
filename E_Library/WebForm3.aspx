<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="E_Library.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Data Control (GridView) --%>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:E-LibraryDBConnectionString %>" SelectCommand="SELECT * FROM [book_issue_tbl]"></asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1"></asp:GridView>

    <%--HTML Table--%>
    <div>
        <table id="TB1">
            <tr>
                <td>Member ID</td>
                <td>Member Name</td>
                <td>Book ID</td>
                <td>Book Name</td>
                <td>Issue Date</td>
                <td>Due Date</td>
            </tr>
        <%=GetData() %>
        </table>
    </div>
</asp:Content>
