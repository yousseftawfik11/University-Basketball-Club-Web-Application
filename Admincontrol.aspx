<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Admincontrol.aspx.cs" Inherits="Admincontrol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Admin control panel
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssLinks" runat="Server">





    <link rel="stylesheet" href="assets/adminControl/bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i">
    <link rel="stylesheet" href="assets/adminControl/fonts/fontawesome-all.min.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="Server">
    <asp:Label ID="lblMessage" runat="server"></asp:Label>


    <br />
    <br />
    <br />


    <div id="wrapper">

        <%-----------------------------navbar----------------%>
        <nav class="navbar navbar-dark align-items-start sidebar sidebar-dark accordion" style="background-color: #2C3E50 !important;">
            <div class="container-fluid d-flex flex-column p-0">
                <a class="navbar-brand d-flex justify-content-center align-items-center sidebar-brand m-0" href="#">
                    <div class="sidebar-brand-icon rotate-n-15"></div>
                    <div class="sidebar-brand-text mx-3"><span>Control Panel</span></div>
                </a>
                <hr class="sidebar-divider my-0">
                <ul class="nav navbar-nav text-light" id="accordionSidebar">
                    <li class="nav-item" role="presentation">
                        <asp:LinkButton class="nav-link" ID="lbUser" CausesValidation="false" runat="server" OnClick="lbUser_Click"><i class="fas fa-user"></i><span>Users</span></asp:LinkButton>
                    </li>
                    <li class="nav-item" role="presentation">
                        <asp:LinkButton class="nav-link" ID="lbTeam" CausesValidation="false" runat="server" OnClick="lbTeam_Click"><i class="fas fa-group"></i><span>Teams</span></asp:LinkButton>
                    </li>
                    <li class="nav-item" role="presentation">
                        <asp:LinkButton class="nav-link" ID="lbEvents" CausesValidation="false" runat="server" OnClick="lbEvents_Click"><i class="fas fa-table"></i><span>Events</span></asp:LinkButton>
                    </li>
                    <li class="nav-item" role="presentation">
                        <asp:LinkButton class="nav-link" ID="lbFAQ" CausesValidation="false" runat="server" OnClick="lbFAQ_Click"><i class="fas fa-question-circle"></i><span>FAQ</span></asp:LinkButton>
                    </li>
                </ul>
                <div class="text-center d-none d-md-inline">
                    <button class="btn rounded-circle border-0" id="sidebarToggle" type="button"></button>
                </div>
            </div>
        </nav>

        <%-----------------------------navbar----------------%>

        <asp:MultiView ID="mlvAdminControlPanel" runat="server">
            <asp:View runat="server" ID="Users">
                <div class="d-flex flex-column" id="content-wrapper">
                    <div id="content">
                        <div class="container-fluid">
                            <h3 class="text-dark mb-4">Manage user</h3>
                            <div class="card shadow">
                                <div class="card-header py-3">
                                    <p class="text-primary m-0 font-weight-bold">Members</p>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-6 text-nowrap">
                                            <div id="dataTable_length" class="dataTables_length" aria-controls="dataTable">
                                                <label>
                                                    <asp:DropDownList class="form-control form-control-sm custom-select custom-select-sm" ID="ddlPageSizeUsers" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShowUsers_SelectedIndexChanged">
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                    </asp:DropDownList>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="text-md-right dataTables_filter" id="dataTable_filter">
                                                <label>
                                                    <asp:Button class="btn btn-primary btn-block text-white btn-user" Style="background-color: #2C3E50;" ID="btnAddNewUser" runat="server" Text="Add new user" OnClick="btnAddNewUser_Click" />
                                                </label>
                                                <label>
                                                    <asp:Button class="btn btn-primary btn-block text-white btn-user" Style="background-color: #2C3E50;" ID="btnSearchUser" runat="server" Text="Search" OnClick="btnSearchUser_Click" />
                                                </label>
                                                <label>
                                                    <asp:TextBox class="form-control" ID="txtSearchUsers" runat="server" placeholder="Search"></asp:TextBox>
                                                </label>
                                                <label>
                                                    <asp:DropDownList class="btn btn-secondary dropdown-toggle" ID="ddlSearchBy" runat="server" OnSelectedIndexChanged="ddlShowUsers_SelectedIndexChanged">
                                                        <asp:ListItem Value="user_id">user id</asp:ListItem>
                                                        <asp:ListItem Value="username">username</asp:ListItem>
                                                        <asp:ListItem Value="fname">First name</asp:ListItem>
                                                        <asp:ListItem Value="lname">Last name</asp:ListItem>
                                                        <asp:ListItem Value="email">Email</asp:ListItem>
                                                        <asp:ListItem Value="address">Address</asp:ListItem>
                                                        <asp:ListItem Value="phone_no">Phone number</asp:ListItem>
                                                    </asp:DropDownList>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="divUserEdit" runat="server" visible="false">
                                        <div class="col-md-2">
                                            <div class="dataTables_length" aria-controls="dataTable">
                                                <label>
                                                    ID &nbsp;
                                                    <asp:Label ID="lblUser_id" runat="server"></asp:Label>
                                                    Username
                                                    <asp:TextBox class="form-control" ID="txtUsername" runat="server"></asp:TextBox>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="dataTables_length" aria-controls="dataTable">
                                                <label>
                                                    First name
                                                    <asp:TextBox class="form-control" ID="txtFname" runat="server"></asp:TextBox>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="dataTables_length" aria-controls="dataTable">
                                                <label>
                                                    Last name
                                                    <asp:TextBox class="form-control" ID="txtLname" runat="server"></asp:TextBox>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="dataTables_length" aria-controls="dataTable">
                                                <label>
                                                    Email
                                                    <asp:TextBox class="form-control" ID="txtEmail" runat="server"></asp:TextBox>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="dataTables_length" aria-controls="dataTable">
                                                <label>
                                                    Phone number
                                                    <asp:TextBox class="form-control" ID="txtPhoneNo" runat="server"></asp:TextBox>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="dataTables_length" aria-controls="dataTable">
                                                <label>
                                                    Address
                                                    <asp:TextBox class="form-control" ID="txtAddress" runat="server"></asp:TextBox>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="dataTables_length" aria-controls="dataTable">
                                                <label>
                                                    Update image 
                                                    <asp:RadioButtonList ID="rblSelectImg" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblSelectImg_SelectedIndexChanged">
                                                        <asp:ListItem Value="yes">&nbsp;yes</asp:ListItem>
                                                        <asp:ListItem Value="no" Selected="True">&nbsp;no</asp:ListItem>
                                                    </asp:RadioButtonList>

                                                    <asp:FileUpload class="custom-file" ID="fuUserImg" runat="server" Visible="False" />
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="dataTables_length" aria-controls="dataTable">
                                                <label>
                                                    Role
                                                <asp:RadioButtonList ID="rblRole" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Admin">&nbsp;Admin</asp:ListItem>
                                                    <asp:ListItem Value="Member">&nbsp;Member</asp:ListItem>
                                                </asp:RadioButtonList>

                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="dataTables_length" aria-controls="dataTable">
                                                <label>
                                                    Status
                                                <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Active">&nbsp;Active</asp:ListItem>
                                                    <asp:ListItem Value="Blocked">&nbsp;Blocked</asp:ListItem>
                                                </asp:RadioButtonList>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="dataTables_length" aria-controls="dataTable">
                                                <label>
                                                    Gender
                                                <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Male">&nbsp;Male</asp:ListItem>
                                                    <asp:ListItem Value="Female">&nbsp;Female</asp:ListItem>
                                                </asp:RadioButtonList>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="dataTables_length" aria-controls="dataTable">
                                            </div>
                                            <div class="dataTables_length" aria-controls="dataTable">
                                                <asp:Button class="btn btn-primary btn-block text-white btn-user" Style="background-color: #2C3E50;" ID="btnSaveNewUser" runat="server" Text="Save new user" Visible="false" OnClick="btnSaveNewUser_Click" />
                                                <asp:Button class="btn btn-primary btn-block text-white btn-user" Style="background-color: #2C3E50;" Visible="false" ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                                                <asp:Button class="btn btn-primary btn-block text-white btn-user" Style="background-color: #2C3E50;" ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="dataTables_length" aria-controls="dataTable">
                                            </div>
                                            <div class="dataTables_length" aria-controls="dataTable">
                                                <asp:Button class="btn btn-primary btn-block text-white btn-user" Style="background-color: #b7280c;" Visible="false" ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <asp:GridView CssClass="table-responsive table mt-2" Style="width: 100%; word-wrap: break-word;" ID="gvUsers" PageSize="5" AllowPaging="True" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="gvUsers_PageIndexChanging" AutoGenerateColumns="False">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:BoundField DataField="user_id" HeaderText="ID" />
                                            <asp:TemplateField HeaderText="Picture">
                                                <ItemTemplate>
                                                    <asp:Image Width="50" Height="50" class="rounded-circle mr-2" ID="imgUserPicture" runat="server" ImageUrl='<%# Eval("picture","~/assets/ProfilePics/{0}") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="username" HeaderText="Username">
                                                <HeaderStyle Width="250px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Fname" HeaderText="First name">
                                                <HeaderStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Lname" HeaderText="Last name">
                                                <HeaderStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="email" HeaderText="Email">
                                                <HeaderStyle Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="phone_no" HeaderText="Phone number">
                                                <HeaderStyle Width="250px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Role">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRole" runat="server" Text='<%# Eval("role").ToString() == "False" ? "Member" : "Admin" %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status").ToString() == "False" ? "Blocked" : "Active" %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="gender" HeaderText="Gender">
                                                <HeaderStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Address" HeaderText="Address">
                                                <HeaderStyle Width="250px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:Button class="btn btn-primary btn-block text-white btn-user" Style="background-color: #2C3E50;" ID="btnUserSelect" runat="server" Text="Select" OnClick="btnUserSelect_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <a class="border rounded d-inline scroll-to-top" href="#page-top"><i class="fas fa-angle-up"></i></a>
            </asp:View>
            <asp:View runat="server" ID="viewTeams">
                <div class="d-flex flex-column" id="content-wrapper">
                    <div id="content">
                        <div class="container-fluid">
                            <h3 class="text-dark mb-4">Manage Teams</h3>
                            <div class="card shadow">
                                <div class="card-header py-3">
                                    <p class="text-primary m-0 font-weight-bold">Teams</p>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-8 text-nowrap">
                                            <h1>This page is for managing teams, </h1>
                                            <h1>but the same functions will be repeated here, so I left it empty</h1>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
 <asp:View runat="server" ID="viewEvents">
                <div class="d-flex flex-column" id="content-wrapper">
                    <div id="content">
                        <div class="container-fluid">
                            <h3 class="text-dark mb-4">Manage Events</h3>
                            <div class="card shadow">
                                <div class="card-header py-3">
                                    <p class="text-primary m-0 font-weight-bold">Events</p>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-8 text-nowrap">
                                            <h1>This page is for managing events, </h1>
                                            <h1>but the same functions will be repeated here, so I left it empty</h1>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
             <asp:View runat="server" ID="viewFAQ">
                <div class="d-flex flex-column" id="content-wrapper">
                    <div id="content">
                        <div class="container-fluid">
                            <h3 class="text-dark mb-4">Manage FAQ</h3>
                            <div class="card shadow">
                                <div class="card-header py-3">
                                    <p class="text-primary m-0 font-weight-bold">FAQ</p>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-8 text-nowrap">
                                            <h1>This page is for managing events, </h1>
                                            <h1>but the same functions will be repeated here, so I left it empty</h1>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jsLinks" runat="Server">
    <script src="assets/adminControl/js/jquery.min.js"></script>
    <script src="assets/adminControl/bootstrap/js/bootstrap.min.js"></script>
    <script src="assets/adminControl/js/chart.min.js"></script>
    <script src="assets/adminControl/js/bs-init.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.4.1/jquery.easing.js"></script>
    <script src="assets/adminControl/js/theme.js"></script>
</asp:Content>


