<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="capstone.Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <!-- CSS only -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-iYQeCzEYFbKjA/T2uDLTpkwGzCiq6soy8tYaI1GyVh/UjpbCx/TYkiZhlZB6+fzT" crossorigin="anonymous" />
    <!-- JavaScript Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-u1OknCvxWvY5kfmNBILK2hRnQC3Pr17a+RTT6rIHI7NnikvbZlHgTPOOmMi466C8" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="cart.css" />

    <script src="https://kit.fontawesome.com/a076d05399.js" crossorigin="anonymous"></script>
    <script type="text/javascript">
        function curday(sp, off) {
            today = new Date();
            var dd = today.getDate() + off;
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();

            if (dd < 10) dd = '0' + dd;
            if (mm < 10) mm = '0' + mm;
            return (mm + sp + dd + sp + yyyy);
        };

        function hide(cost, status) {
            if (status == 'False') {
                return "Price: &#8377;" + cost;
            }
            else
                return "";

        }
        function hide2(cost, status) {
            if (status == 'True') {
                return "Price: &#8377;" + cost.toString();
            }
            else
                return "";
        }

        function price(costString, percentString) {
            let cost = parseInt(costString);
            let percent = parseInt(percentString);

            return (cost - (cost * percent / 100));
        }

        document.onkeydown = function () {
            if (event.keyCode == 13 && event.srcElement.tagName.toLowerCase() == "input") {

                return false;
            }
        }

    </script>



</head>
<body class="bgcolor">
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-md navbar-dark bg-dark mb-3">
            <div class="container">
                <a class="navbar-brand" href="Home.aspx">
                    <img id="logo"
                        src="Images/electro1.jpg" alt="Logo"
                        draggable="false" height="50" /></a>





                <div class="d-flex align-items-center w-100 form-search">
                    <div class="input-group">

                        <asp:Button ID="searchCategory" runat="server" class="btn btn-light dropdown-toggle shadow-0" type="button" data-bs-toggle="dropdown"
                            aria-expanded="false" Style="padding-bottom: 0.4rem;" Text="All"></asp:Button>
                        <div class=" dropdown-menu dropdown-menu-dark fa-ul">

                            <asp:LinkButton class="dropdown-item" CommandArgument='All' OnClick="categorySelect" runat="server"><span class="fa-li pe-2"><i class="fas fa-search"></i></span>All</asp:LinkButton>


                            <asp:ListView ID="categories" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton class="dropdown-item" CommandArgument='<%#Eval("[name]")%>' OnClick="categorySelect" runat="server"><span class="fa-li pe-2"><i class="fas fa-search"></i></span><%#: Eval("name") %></asp:LinkButton>

                                </ItemTemplate>
                            </asp:ListView>

                        </div>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                            <Services>

                                <asp:ServiceReference Path="~/WebService1.asmx" />

                            </Services>
                        </asp:ScriptManager>
                        <asp:TextBox runat="server" TextMode="Search" ID="SearchBox" class="form-control" placeholder="Search" aria-label="Search" />



                    </div>
                    <asp:LinkButton runat="server" OnClick="Search" class="text-white"><i class="fas fa-search ps-3"></i></asp:LinkButton>
                </div>

                <ul class="navbar-nav ms-3 align-items-center">

                    <li class="nav-item">
                        <a class="nav-link d-flex align-items-center ms-3 me-3" href="Cart.aspx">
                            <i class="fas fa-cart-plus"></i>Cart</a>
                    </li>

                    <li class="nav-item align-items-center">
                        <a class="nav-link d-flex me-3" style="width: 100px;" href="loginPage.aspx">Sign In/Out</a>
                    </li>


                    <li class="nav-item ">
                        <div class="nav-link d-flex align-items-center ms-6 btn-group  me-3 ">
                            <img runat="server" data-bs-toggle="tooltip" data-bs-placement="left" title="View Profile" id="profilePic" src="Images/profileDefaultGreen.png" class="rounded-circle" alt="..." style="height: 50px;" />
                            <button type="button" class="btn btn-dark dropdown-toggle dropdown-toggle-split" data-bs-toggle="dropdown" aria-expanded="false">
                                <span class="visually-hidden">Toggle Dropdown</span>
                            </button>
                            <ul class="dropdown-menu">
                                <li><a class="dropdown-item" href="profile.aspx">Profile</a></li>
                                <li><a class="dropdown-item" href="orders.aspx">View Orders</a></li>

                            </ul>
                        </div>
                    </li>
                </ul>
            </div>
            <!-- Collapsible wrapper -->

            <!-- Container wrapper -->
        </nav>
        <!-- Navbar -->
        <section class="h-100 bgcolor">
            <div class="container py-5">
                <div class="row d-flex justify-content-center my-4">
                    <div class="col-md-8">
                        <div class="card mb-4">
                            <div class="card-header py-3">
                                <h5 class="mb-0">Cart</h5>
                            </div>

                            <asp:ListView ID="ListView1" runat="server" OnSelectedIndexChanging="ListView1_SelectedIndexChanging">

                                <EmptyDataTemplate>
                                    <div class="row justify-content-center">
                                        <div class="col p-3 m-3">
                                            <p class=" display-6 text-secondary">Empty</p>
                                        </div>
                                    </div>
                                </EmptyDataTemplate>

                                <ItemSeparatorTemplate>
                                    <hr class="my-4" />

                                </ItemSeparatorTemplate>
                                <ItemTemplate>
                                    <section>
                                        <div class="card-body">

                                            <div class="row">
                                                <div class="col-lg-3 col-md-12 mb-4 mb-lg-0">

                                                    <div class="bg-image hover-overlay hover-zoom ripple rounded" data-mdb-ripple-color="light">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# getImg(Eval("[pic]")) %>' Height="150" Width="150" class="w-100" alt="Image" />
                                                        <a href="#!">
                                                            <div class="mask" style="background-color: rgba(251, 251, 251, 0.2)"></div>
                                                        </a>
                                                    </div>

                                                </div>

                                                <div class="col-lg-5 col-md-6 mb-4 mb-lg-0">

                                                    <p><strong><%# Eval("[name]") %></strong></p>
                                                    <p>Description: <%# Eval("[description]") %></p>
                                                    <p>Offer: <%# Eval("[offerName]") %> </p>
                                                    <p>Valid Till: <%# Eval("[expiry]") %> </p>

                                                    <asp:LinkButton runat="server" class="btn btn-primary btn-sm mb-2" OnClick="deleteFromCart" CommandArgument='<%# Eval("[prodID]") %>' data-mdb-toggle="tooltip"
                                                        title="Remove item">
                                                            <i class="fas fa-trash-alt"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" class="btn btn-danger btn-sm mb-2" OnClick="later" CommandArgument='<%# Eval("[prodID]") %>' data-mdb-toggle="tooltip"
                                                        title="Move to the wish list">
                                                        <%--<button type="button" class="btn btn-danger btn-sm mb-2" data-mdb-toggle="tooltip"
                                                            title="Move to the wish list">--%>
                                                            <i class="fas fa-bookmark"></i>
                                                    </asp:LinkButton>

                                                </div>

                                                <div class="col-lg-4 col-md-6 mb-4 mb-lg-0">

                                                    <div class="d-flex mb-4" style="max-width: 300px">


                                                        <div class="form-outline">
                                                            <asp:TextBox runat="server" min="1" class="w-100 sel" ID="quantity" Text='<%# Eval("quantity") %>' TextMode="Number" />
                                                            <label class="form-label" for="form1">Quantity</label>
                                                        </div>



                                                        <asp:LinkButton runat="server" class="btn btn-primary h-50 px-3 ms-2"
                                                            OnClick="updateCart" CommandArgument='<%# Eval("[prodID]") %>'>
                                                                <i class="fas fa-check-circle"></i>
                                                        </asp:LinkButton>
                                                    </div>

                                                    <p class="text-start text-md-center">
                                                        <strong>
                                                            <script type="text/javascript">
                                                                document.write(hide('<%# Eval("[price]").ToString() %>','<%# Eval("[oStatus]") %>'));
                                                            </script>
                                                        </strong>

                                                    </p>

                                                    <p class="text-start text-md-center">
                                                        <strong><s>
                                                            <script type="text/javascript">
                                                                document.write(hide2('<%# Eval("[price]").ToString() %>','<%# Eval("[oStatus]") %>'));
                                                            </script>
                                                        </s></strong>
                                                        <br />
                                                        <strong>
                                                            <script type="text/javascript">
                                                                document.write(hide2(price('<%# Eval("[price]") %>', '<%# Eval("[disc]") %>'), '<%# Eval("[oStatus]") %>'));
                                                            </script>
                                                        </strong>

                                                    </p>

                                                </div>
                                            </div>
                                        </div>
                                    </section>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <div id="itemPlaceHolderContainer" runat="server">
                                        <section id="itemPlaceHolder" runat="server"></section>
                                    </div>
                                </LayoutTemplate>

                            </asp:ListView>
                        </div>
                        <div class="card mb-4">
                            <div class="card-body">
                                <p><strong>Expected shipping delivery</strong></p>
                                <p class="mb-0">
                                    <script type="text/javascript">
                                        document.write(curday('/', 2));
                                    </script>
                                    -
                                    <script type="text/javascript">
                                        document.write(curday('/', 7));
                                    </script>
                                </p>
                            </div>
                        </div>

                    </div>
                    <div class="col-md-4">
                        <div class="card mb-4">
                            <div class="card-header py-3">
                                <h5 class="mb-0">Summary</h5>
                            </div>
                            <div class="card-body text-center">
                                <ul class="list-group list-group-flush">
                                    <li
                                        class="list-group-item d-flex justify-content-between align-items-center border-0 px-0 pb-0">Products <span>&#8377;
                                            <asp:Label runat="server" ID="productCost" Text=""></asp:Label></span>
                                    </li>
                                    <li class="list-group-item d-flex justify-content-between align-items-center px-0">Shipping <span>Free Delivery</span>
                                    </li>
                                    <li
                                        class="list-group-item d-flex justify-content-between align-items-center border-0 px-0 mb-3">
                                        <div>
                                            <strong>Total amount</strong>

                                        </div>
                                        <span><strong><span>&#8377;
                                            <asp:Label runat="server" ID="total" Text=""></asp:Label></span></strong></span>
                                    </li>
                                </ul>

                                <asp:Button runat="server" OnClick="checkOut_Click" Text="Checkout" type="button" class="btn btn-primary btn-lg btn-block"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row d-flex my-4">
                    <div class="col-md-8">
                        <div class="card mb-4">
                            <div class="card-header py-3">
                                <h5 class="mb-0">Saved For Later</h5>
                            </div>

                            <asp:ListView ID="Later" runat="server">

                                <EmptyDataTemplate>
                                    <div class="row justify-content-center">
                                        <div class="col p-3 m-3">
                                            <p class=" display-6 text-secondary">Empty</p>
                                        </div>
                                    </div>
                                </EmptyDataTemplate>

                                <ItemSeparatorTemplate>
                                    <hr class="my-4" />

                                </ItemSeparatorTemplate>
                                <ItemTemplate>
                                    <section>
                                        <div class="card-body">

                                            <div class="row">
                                                <div class="col-lg-3 col-md-12 mb-4 mb-lg-0">

                                                    <div class="bg-image hover-overlay hover-zoom ripple rounded" data-mdb-ripple-color="light">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# getImg(Eval("[pic]")) %>' Height="150" Width="150" class="w-100" alt="Image" />
                                                        <a href="#!">
                                                            <div class="mask" style="background-color: rgba(251, 251, 251, 0.2)"></div>
                                                        </a>
                                                    </div>

                                                </div>

                                                <div class="col-lg-5 col-md-6 mb-4 mb-lg-0">

                                                    <p><strong><%# Eval("[name]") %></strong></p>
                                                    <p>Description: <%# Eval("[description]") %></p>
                                                    <p>Offer: <%# Eval("[offerName]") %> </p>
                                                    <p>Valid Till: <%# Eval("[expiry]") %> </p>
                                                    <asp:LinkButton runat="server" class="btn btn-primary btn-sm mb-2" OnClick="deleteFromLater" CommandArgument='<%# Eval("[prodID]") %>' data-mdb-toggle="tooltip"
                                                        title="Remove item">
                                                            <i class="fas fa-trash-alt"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" class="btn btn-danger btn-sm mb-2" OnClick="cart" CommandArgument='<%# Eval("[prodID]") %>' data-mdb-toggle="tooltip"
                                                        title="Move to Cart">
                                                        <%--<button type="button" class="btn btn-danger btn-sm mb-2" data-mdb-toggle="tooltip"
                                                            title="Move to the wish list">--%>
                                                            <i class="fas fa-cart-plus"></i>
                                                    </asp:LinkButton>

                                                </div>

                                                <div class="col-lg-4 col-md-6 mb-4 mb-lg-0">



                                                    <p class="text-start text-md-center">
                                                        <strong>
                                                            <script type="text/javascript">
                                                                document.write(hide('<%# Eval("[price]").ToString() %>','<%# Eval("[oStatus]") %>'));
                                                            </script>
                                                        </strong>

                                                    </p>

                                                    <p class="text-start text-md-center">
                                                        <strong><s>
                                                            <script type="text/javascript">
                                                                document.write(hide2('<%# Eval("[price]").ToString() %>','<%# Eval("[oStatus]") %>'));
                                                            </script>
                                                        </s></strong>
                                                        <br />
                                                        <strong>
                                                            <script type="text/javascript">
                                                                document.write(hide2(price('<%# Eval("[price]") %>', '<%# Eval("[disc]") %>'), '<%# Eval("[oStatus]") %>'));
                                                            </script>
                                                        </strong>

                                                    </p>

                                                </div>
                                            </div>
                                        </div>
                                    </section>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <div id="itemPlaceHolderContainer" runat="server">
                                        <section id="itemPlaceHolder" runat="server"></section>
                                    </div>
                                </LayoutTemplate>

                            </asp:ListView>
                        </div>


                    </div>
                </div>
            </div>
        </section>



        <div>
        </div>
        <p>
            &nbsp;
        </p>
        <asp:Label runat="server" ID="Label1" Text="Label"></asp:Label>
    </form>
</body>

</html>
