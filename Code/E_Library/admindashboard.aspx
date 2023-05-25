<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage1.master" AutoEventWireup="true" CodeBehind="admindashboard.aspx.cs" Inherits="E_Library.admindashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ChildHead" runat="server">
    <style>
        .table-card {
            overflow: scroll;
            height: 250px;
            width: max-content;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ChildContent1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-4">
                <br />
                <div class="card shadow">
                    <div class="card-body" style="background-color: #eeeeee">
                        <div class="row">
                            <div class="col-md-7">
                                <asp:Label ID="Label1" runat="server" Text="10" Style="color: #009688" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                                <h5>Borrowed Books</h5>
                            </div>
                            <div class="col-md-5">
                                <img src="imgs/borrow-book.png" width="80" height="80" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <br />
                <div class="card shadow">
                    <div class="card-body" style="background-color: #eeeeee">
                        <div class="row">
                            <div class="col-md-7">
                                <asp:Label ID="Label2" runat="server" Text="10" Style="color: #009688" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                                <h5>Available Books</h5>
                            </div>
                            <div class="col-md-5">
                                <img src="imgs/book-shelf.png" width="80" height="80" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <br />
                <div class="card shadow">
                    <div class="card-body" style="background-color: #eeeeee">
                        <div class="row">
                            <div class="col-md-7">
                                <asp:Label ID="Label3" runat="server" Text="10" Style="color: #009688" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
                                <h5>Members</h5>
                            </div>
                            <div class="col-md-e">
                                <img src="imgs/members3.png" width="80" height="80" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-7">
                <br />
                <div class="card shadow">
                    <div class="card-body" style="background-color: #eeeeee">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4 style="color: #009688">Today Dues</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col table-card">
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:E-LibraryDBConnectionString %>" SelectCommand="SELECT * FROM [book_issue_tbl] WHERE due_date = convert(varchar, getdate(), 23)"></asp:SqlDataSource>
                                <asp:GridView ID="GridView1" class="table table-bordered table-hover" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#009688" RowStyle-BackColor="White">
                                    <Columns>
                                        <asp:BoundField DataField="book_name" HeaderText="Book Name" SortExpression="book_name"></asp:BoundField>
                                        <asp:BoundField DataField="member_name" HeaderText="Borrower Name" SortExpression="member_name"></asp:BoundField>
                                        <asp:BoundField DataField="issue_date" HeaderText="Issue Date" SortExpression="issue_date"></asp:BoundField>
                                        <asp:BoundField DataField="due_date" HeaderText="Due Date" SortExpression="due_date"></asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div align="center">No Dues Today.</div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-5">
                <br />
                <div class="card shadow">
                    <div class="card-body" style="background-color: #eeeeee">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4 style="color: #009688">Members Status</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col table-card">
                                <center>
                                    <asp:Chart ID="Chart1" runat="server" BackColor="#EEEEEE" Height="220px" Width="320px">
                                    <Legends>
                                        <asp:Legend Alignment="Center" Docking="Right" BackColor="#EEEEEE" />
                                    </Legends>
                                    <Series>
                                        <asp:Series Name="Series1" />
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BackColor="#EEEEEE"/>
                                    </ChartAreas>
                                </asp:Chart>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-7">
                <br />
                <div class="card shadow">
                    <div class="card-body" style="background-color: #eeeeee">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4 style="color: #009688">Defaulter List</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col table-card">
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:E-LibraryDBConnectionString %>" SelectCommand="SELECT * FROM [book_issue_tbl] WHERE due_date < convert(varchar, getdate(), 23)"></asp:SqlDataSource>
                                <asp:GridView ID="GridView2" class="table table-bordered table-hover" runat="server" DataSourceID="SqlDataSource2" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#dc3545" RowStyle-BackColor="White">
                                    <Columns>
                                        <asp:BoundField DataField="member_id" HeaderText="Member ID" SortExpression="member_id"></asp:BoundField>
                                        <asp:BoundField DataField="member_name" HeaderText="Member Name" SortExpression="member_name"></asp:BoundField>
                                        <asp:BoundField DataField="issue_date" HeaderText="Issue Date" SortExpression="issue_date"></asp:BoundField>
                                        <asp:BoundField DataField="due_date" HeaderText="Due Date" SortExpression="due_date"></asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div align="center">No Defaulters.</div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-5">
                <br />
                <div class="card shadow">
                    <div class="card-body" style="background-color: #eeeeee">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4 style="color: #009688">Pending Members</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col table-card">
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:E-LibraryDBConnectionString %>" SelectCommand="SELECT * FROM [member_master_tbl] WHERE account_status='Pending'"></asp:SqlDataSource>
                                <asp:GridView ID="GridView3" class="table table-bordered table-hover" runat="server" DataSourceID="SqlDataSource3" AutoGenerateColumns="False" DataKeyNames="member_id" ShowHeaderWhenEmpty="True" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#ffc107" RowStyle-BackColor="White" AlternatingRowStyle-BackColor="WhiteSmoke">
                                    <Columns>
                                        <asp:BoundField DataField="member_id" HeaderText="Member ID" ReadOnly="True" SortExpression="member_id" />
                                        <asp:BoundField DataField="full_name" HeaderText="Name" SortExpression="full_name" />
                                        <asp:BoundField DataField="contact_no" HeaderText="Contact No" SortExpression="contact_no" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div align="center">No Pending Members.</div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
