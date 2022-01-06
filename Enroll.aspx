<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Enroll.aspx.cs" Inherits="Enroll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    Enroll
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssLinks" runat="Server">
    <style>
        .buttonClass {
            padding: 2px 20px;
            text-decoration: none;
            border: solid 1px grey;
            background-color: white;
        }

            .buttonClass:hover {
                border: solid 1px Black;
                background-color: #ffffff;
            }

        .auto-style1 {
            border: solid 2px black;
            min-width: 80%;
        }

        .auto-style2 {
            min-width: 80%;
            border: 2px solid black;
            margin-bottom: 0px;
        }
    </style>
    <link href="assets/css/gvDesign.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="Server">

    <br />
    <br />
    <br />
    <br />


    <div style="background-color: #f5f7f9">
        <h2 style="text-align: center"><u>Enroll</u></h2>
        <br />
        <p style="text-align: center">
            Enroll to club events and club teams now.
            <br />
            Increase your knowledge in basketball by enrolling to the club's informative events and weekly practices.
        </p>

        <div style="text-align: center">

            <asp:Button ID="btnEventShow" runat="server" Text="Enroll to Events" OnClick="btnEventShow_Click" CssClass="btn btn-outline-info text-center" />
            &nbsp;&nbsp;
        <asp:Button ID="btnTeamsShow" runat="server" Text="Join Teams" OnClick="btnTeamsShow_Click" CssClass="btn btn-outline-info text-center" />
            <br />
        </div>
    </div>

    <br />

    <div id="eventsDiv" runat="server">
        <%--Grid view showing the event data brief--%>
        <asp:GridView ID="gvEnroll" runat="server" HorizontalAlign="Center" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header"
            RowStyle-CssClass="rows" OnRowCreated="gvEnroll_RowCreated" Height="321px" Width="1000px">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnGetEvent" runat="server" OnClick="btnGetEvent_Click" CausesValidation="false" CssClass="btn btn-primary btn-sm" Style="background-color: #5D7B9D;">Check</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <HeaderStyle CssClass="header"></HeaderStyle>

            <PagerStyle CssClass="pager"></PagerStyle>

            <RowStyle CssClass="rows"></RowStyle>

        </asp:GridView>

        <asp:Table ID="EventTable" runat="server" Width="1000px" CssClass="table table-hover" Style="margin-right: auto; margin-left: auto;">
            <%--Table to show the full event information--%>
            <asp:TableRow>
                <asp:TableCell>
        Event Title:&nbsp; 
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblEventTitle" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
        Event Date:&nbsp;
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblEventDate" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
        Event Instructor:&nbsp;
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblInstructorName" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
        Event Instructor Institution:&nbsp;
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblInstitution" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
        Event Instructor Eamil:&nbsp;
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblInstructorEmail" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
        Event Instructor Information:&nbsp;
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblInstructorDesc" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
       
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnEnrollEvent" runat="server" Text="Enroll to Event" OnClick="btnEnrollEvent_Click" CssClass="btn btn-outline-info" Visible="false" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>

    <div id="teams" runat="server">
        <asp:GridView ID="gvPractice" runat="server" HorizontalAlign="Center" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header"
            RowStyle-CssClass="rows" Height="139px" Width="1000px">

            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnGetPractice" runat="server" OnClick="btnGetPractice_Click" CausesValidation="false" CssClass="btn btn-primary btn-sm" Style="background-color: #5D7B9D;">Check</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>


        <asp:Table ID="practiceTable" runat="server" Height="158px" Width="1000px" CssClass="table table-hover" Style="margin-right: auto; margin-left: auto;">
            <%--Table to show the full event information--%>
            <asp:TableRow>
                <asp:TableCell>
        Practice Level:&nbsp; 
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblTeam" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
        Practice Weekly Day:&nbsp;
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblPday" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
        Practice Time:&nbsp;
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblPTime" runat="server" Text=""></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
       
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="btnPracticeEnroll" runat="server" Text="Enroll to practice" OnClick="btnPracticeEnroll_Click" CssClass="btn btn-outline-info" Visible="false" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jsLinks" runat="Server">
</asp:Content>

