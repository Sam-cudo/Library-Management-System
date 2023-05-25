<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="E_Library.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <section>
        <img src="imgs/home-bg-1.png" class="img-fluid" />
    </section>

    <section>
        <div class="container">  
            <div class="row">
                <div class="col-12">
                    <center>
                        <h2>Our Features</h2>
                    </center>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <center>
                        <img src="imgs/digital-inventory.png" width="150"/>
                        <h4>Digital Book Inventory</h4>
                        <p class="text-justify">Book inventory is the cost of inventory on hand, as stated in an organization's accounting records. This amount is compared to the actual inventory on hand to see if there are any discrepancies in the accounting records, which can indicate procedural problems that should be corrected</p>
                    </center>
                </div>
                <div class="col-md-3">
                    <center>
                        <img src="imgs/search-online.png" width="150"/>
                        <h4>Search Books</h4>
                        <p class="text-justify">Book inventory is the cost of inventory on hand, as stated in an organization's accounting records. This amount is compared to the actual inventory on hand to see if there are any discrepancies in the accounting records, which can indicate procedural problems that should be corrected</p>
                    </center>
                </div>
                <div class="col-md-3">
                    <center>
                        <img src="imgs/Dashboard.png" width="150"/>
                        <h4>Admin Dashboard</h4>
                        <p class="text-justify">Book inventory is the cost of inventory on hand, as stated in an organization's accounting records. This amount is compared to the actual inventory on hand to see if there are any discrepancies in the accounting records, which can indicate procedural problems that should be corrected</p>
                    </center>
                </div>
                <div class="col-md-3">
                    <center>
                        <img src="imgs/defaulters-list.png" width="150"/>
                        <h4>Defaulter List</h4>
                        <p class="text-justify">Book inventory is the cost of inventory on hand, as stated in an organization's accounting records. This amount is compared to the actual inventory on hand to see if there are any discrepancies in the accounting records, which can indicate procedural problems that should be corrected</p>
                    </center>
                </div>
            </div>
        </div>
    </section>
    <hr />
    <section>
        <div class="container">
            <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
                <ol class="carousel-indicators">
                    <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active" style="background-color:grey"></li>
                    <li data-target="#carouselExampleIndicators" data-slide-to="1" style="background-color:grey"></li>
                    <li data-target="#carouselExampleIndicators" data-slide-to="2" style="background-color:grey"></li>
                </ol>
                <div class="carousel-inner">
                    <div class="carousel-item active">
                        <img src="/imgs/ci-1.png" class="d-block w-100" alt="...">
                    </div>
                    <div class="carousel-item">
                        <img src="/imgs/ci-2.png" class="d-block w-100" alt="...">
                    </div>
                    <div class="carousel-item">
                        <img src="/imgs/ci-3.png" class="d-block w-100" alt="...">
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
