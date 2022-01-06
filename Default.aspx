<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    INTI BasketBall Club

</asp:Content>
<asp:Content ID="cssLinks" ContentPlaceHolderID="cssLinks" runat="Server">
    <%--  // slider//--%>
    <link rel="stylesheet" href="assets/css/Simple-Slider.css">
    <link href="assets/css/Simple-Slider.css" rel="stylesheet" />
    <%--   // slider//--%>
    <link href="assets/css/gvDesign.css" rel="stylesheet" />



    <script src="Scripts/jquery-3.0.0.slim.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <style type="text/css">
        .my-container {
            border: 1px solid;
            /*border-color: green;*/
        }

        .my-row {
            border: 3px;
            /*border-color:red;*/
        }

        .my-col {
            border: 3px;
            /*border-color:cornflowerblue;*/
        }

        .myJustify {
            text-align: justify;
            text-justify: inter-word;
        }

        .auto-style1 {
            margin-right: 0px;
        }

        .auto-style2 {
            margin-right: 0px;
            margin-bottom: 0px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageContent" runat="Server">
    <asp:SqlDataSource ID="SqlDataSourceCategories" runat="server" ConnectionString="<%$ ConnectionStrings:BasketballConStr %>" SelectCommand="SELECT TOP 3 [event_title] AS Title, [event_date] AS Date FROM [event] WHERE [event_date]> GETDATE() ORDER BY [event_date]"></asp:SqlDataSource>

    <div class="simple-slider">
        <%--a slider at the top of the page that displays picture to the user. The user can use the arrows to change the pictures--%>
        <div class="swiper-container">
            <div class="swiper-wrapper">
                <asp:Image class="swiper-slide" ID="imgSlider1" runat="server" ImageUrl="~/assets/img/basketballpic.jpg" />
                <asp:Image class="swiper-slide" ID="imgSlider2" runat="server" ImageUrl="~/assets/img/girlscamp.png" />
            </div>
            <div class="swiper-pagination"></div>
            <div class="swiper-button-prev"></div>
            <div class="swiper-button-next"></div>
        </div>
    </div>

    <div class="container container-fluid">
        <%--Bootstrap container that has the club mission and the Upcoming events grid--%>
        <div class="row my-row">
            <div class="col-8 my-col myJustify">
                <asp:Label ID="lblUserName" runat="server" />
                <h2><u>Club Mission</u></h2>
                Here in INTI International University Nilai basketball club, we aim to provide the best experince to all basketball lovers from skill levels.
                We offer all of our members plenty of activites, weekly practices, tournaments, basketball related events like talks from coaches and experts, basketball bootcamps and much more.
                INTI International University Nilai basket ball club is your second home. We aim to build a family where we could all enjoy our love to the game.
                What are you waiting for join now!

                Our Club is lead by some of the best and experinced basketball players on campus Youssef Mohamed Tawfik as president and Boda Osa as Vice President
            </div>
            <div class="col-4 my-col">
                <h2><u>Upcoming Events!</u></h2>
                <asp:ListView ID="lvEvents" runat="server" DataSourceID="SqlDataSourceCategories">
                    <%--A list view that shows the most 3 recent upcoming events--%>
                    <AlternatingItemTemplate>
                        <li style="background-color: #FFFFFF; color: #284775;">Title:
                            <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
                            <br />
                            Date:
                            <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
                            <br />
                        </li>
                    </AlternatingItemTemplate>
                    <EditItemTemplate>
                        <li style="background-color: #999999;">Title:
                            <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
                            <br />
                            Date:
                            <asp:TextBox ID="DateTextBox" runat="server" Text='<%# Bind("Date") %>' />
                            <br />
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                        </li>
                    </EditItemTemplate>
                    <EmptyDataTemplate>
                        No data was returned.
                    </EmptyDataTemplate>
                    <InsertItemTemplate>
                        <li style="">Title:
                            <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
                            <br />
                            Date:
                            <asp:TextBox ID="DateTextBox" runat="server" Text='<%# Bind("Date") %>' />
                            <br />
                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                        </li>
                    </InsertItemTemplate>
                    <ItemSeparatorTemplate>
                        <br />
                    </ItemSeparatorTemplate>
                    <ItemTemplate>
                        <li style="background-color: #E0FFFF; color: #333333;">Title:
                            <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
                            <br />
                            Date:
                            <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
                            <br />
                        </li>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <ul id="itemPlaceholderContainer" runat="server" style="font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <li runat="server" id="itemPlaceholder" />
                        </ul>
                        <div style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF;">
                        </div>
                    </LayoutTemplate>
                    <SelectedItemTemplate>
                        <li style="background-color: #E2DED6; font-weight: bold; color: #333333;">Title:
                            <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
                            <br />
                            Date:
                            <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
                            <br />
                        </li>
                    </SelectedItemTemplate>
                </asp:ListView>
                <br />

            </div>
        </div>
    </div>
    <div id="ViewDashBoardDiv" runat="server" style="text-align: center">
        <%--Div that contains a button--%>
        <asp:Button ID="btnDashBoard" runat="server" Text="View Dashboard" OnClick="btnDashBoard_Click" CssClass="btn btn-outline-info text-center" />
        &nbsp;
        <asp:Button ID="btnHideDash" runat="server" Text="Hide DashBoard" OnClick="btnHideDash_Click" CssClass="btn btn-outline-info text-center" />
    </div>
    <br />
    <div id="DashBoardDiv" runat="server" class="container">
        <%--This is a div that has all the Dashboard gridviews--%>
        <div class="row">
            <div class="col col-4" style="text-align: center">
                <h6><u>My Team</u></h6>
                <asp:GridView ID="myTeamgv" runat="server" CellPadding="0" ForeColor="#333333" GridLines="None" CssClass="auto-style2" HorizontalAlign="Center" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header"
                    RowStyle-CssClass="rows2" Width="390px" AllowPaging="True" OnPageIndexChanging="myTeamgv_PageIndexChanging" PageSize="5">
                </asp:GridView>
                <hr />
                Practice Day:
                <asp:Label ID="lblPracticeDay" runat="server" /><br />
                Practice Time :
                <asp:Label ID="lblPracticeTime" runat="server" />
                <hr />
            </div>
            <div class="col col-4" style="text-align: center">
                <h6><u>My Rents</u></h6>
                <asp:GridView ID="myRentgv" runat="server" CellPadding="0" ForeColor="#333333" GridLines="None" CssClass="auto-style2" HorizontalAlign="Center" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header"
                    RowStyle-CssClass="rows2" Width="316px" AllowPaging="True" OnPageIndexChanging="myPracticegv_PageIndexChanging" PageSize="5">
                </asp:GridView>
            </div>
            <div class="col col-4" style="text-align: center">
                <h6><u>My Events</u></h6>
                <asp:GridView ID="myEventsgv" runat="server" CellPadding="0" ForeColor="#333333" GridLines="None" CssClass="auto-style2" HorizontalAlign="Center" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header"
                    RowStyle-CssClass="rows2" AllowPaging="True" OnPageIndexChanging="myEventsgv_PageIndexChanging" PageSize="5">
                </asp:GridView>
            </div>
        </div>
    </div>

    <br />
    <div class="container">
        <%--bootstrap container that has the FAQ grid--%>
        <div class="row my-row">
            <div class="col-12 my-col text-center">
                <h2>FAQ</h2>
                <asp:GridView ID="gvFAQ" runat="server" CellPadding="0" ForeColor="#333333" GridLines="None" CssClass="auto-style1" Width="1118px" HorizontalAlign="Center" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header"
                    RowStyle-CssClass="rows2" OnRowDataBound="gvFAQ_RowDataBound">
                </asp:GridView>
            </div>
        </div>
    </div>



</asp:Content>
<asp:Content ID="jsLinks" ContentPlaceHolderID="jsLinks" runat="Server">
</asp:Content>
