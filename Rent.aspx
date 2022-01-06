<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Rent.aspx.cs" Inherits="Rent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    Rent
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssLinks" Runat="Server">
    <link href="assets/css/gvDesign.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style2 {
            width: 892px;
            height: 188px;
        }
        .auto-style4 {
            width: 973px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" Runat="Server">
    <div style="background-color:#f5f7f9">
        <br /><br /><br /><br />
                <h1 style="text-align:center"><u>Rent</u></h1>
        <br />
        <p style="text-align:center">Enjoy your exclusive club members only features. It all starts with a button click. <br />Satisify your needs by renting basketball equipments and courts on campus</p>
            <div style="text-align:center">
     
        <asp:Button ID="btnEquipment" runat="server" Text="Show Equipment" OnClick="btnEquipment_Click" CssClass="btn btn-outline-info text-center"/>
        &nbsp;&nbsp;
        <asp:Button ID="btnCourt" runat="server" Text="Show Courts" OnClick="btnCourt_Click" CssClass="btn btn-outline-info text-center" />
                <br />
    </div>
        <br />
    </div>


    <div id="equipmentDiv" runat="server">
        <h2 style="text-align:center"><u>Equipment</u></h2>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="tbEquipmentSearch" runat="server"></asp:TextBox>
        &nbsp;<asp:Button ID="btnSearchEquipment" runat="server" Text="Search" CssClass="btn btn-outline-info text-center" OnClick="btnSearchEquipment_Click" />
        <br /> <br />
        <asp:GridView ID="gvEquipment" runat="server"  HorizontalAlign="Center" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" 
                RowStyle-CssClass="rows" Height="139px" Width="973px">
            <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtCheckEquipment" runat="server" OnClick="lbtCheckEquipment_Click" CausesValidation="false" CssClass="btn btn-primary btn-sm" Style="background-color:#5D7B9D;">View Equipment Availability</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
        <table class="auto-style4 table table-hover" Style="margin-right:auto; margin-left:auto;" >
            <tr>
                <td>Equipment ID: </td>
                <td>
                    <asp:Label ID="lblEquipmentID" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Equipment Title: </td>
                <td>
                    <asp:Label ID="lblEquipmentTitle" runat="server" Text=""></asp:Label>

                </td>
            </tr>
            <tr>
                <td>Equipment Desc: </td>
                <td>
                    <asp:Label ID="lblEquipmentDesc" runat="server" Text=""></asp:Label>

                </td>
            </tr>
            <tr>
                <td>Cost Per Hour: </td>
                <td>
                    <asp:Label ID="lblEquipmentCost" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Delay Cost: </td>
                <td>
                    <asp:Label ID="lblEquipmentDelayCost" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblchooseDateEq" runat="server" Text="Choose a date:" Visible="false"></asp:Label> </td>
                <td>
                    <asp:TextBox ID="tbChooseDate" runat="server" TextMode="Date" Visible="false"></asp:TextBox>&nbsp;<asp:Button ID="btnEquipShowAvailableTime" runat="server" Text="Show Available Times" OnClick="btnEquipShowAvailableTime_Click" Visible="false" CssClass="btn btn-outline-info" />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTimeSlotEQ" runat="server" Text="Choose a Time Slot (only one booking at a time):" Visible="false"></asp:Label> </td>
                <td>
                    <asp:DropDownList ID="ddlEquipmentTime" runat="server" Visible="false"></asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnEquipmentBook" runat="server" Text="Book" OnClick="btnEquipmentBook_Click" Visible="false" CssClass="btn btn-outline-info"/>
                </td>
            </tr>
        </table>

            <div class="container">
        <div class="row my-row">
            <div class="col-12 my-col text-center">
                <h2>FAQ</h2>
                <asp:GridView ID="gvEquipmentFAQ" runat="server" CellPadding="0" ForeColor="#333333" GridLines="None" CssClass="auto-style1" Width="1118px" HorizontalAlign="Center" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header"
                    RowStyle-CssClass="rows2" OnRowDataBound="gvEquipmentFAQ_RowDataBound">
                </asp:GridView>
            </div>
        </div>
    </div>
    </div>


    <div id="courtDiv" runat="server">
        <h2 style="text-align:center"><u>Courts</u></h2>

        <asp:GridView ID="gvCourt" runat="server" HorizontalAlign="Center"
                CssClass="auto-style2" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" 
                RowStyle-CssClass="rows" Height="139px" Width="892px">
          <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtCheckCourt" runat="server" OnClick="lbtCheckCourt_Click" CssClass="btn btn-primary btn-sm" Style="background-color:#5D7B9D;">View Court Availability</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
           </Columns>
        </asp:GridView>    
<%--        <asp:SqlDataSource ID="showCourt" runat="server" ConnectionString="<%$ ConnectionStrings:BasketballConStr %>" SelectCommand="SELECT [court_id] AS 'Court ID', [location] AS Location, [court_image] AS 'Court Image', [cost_per_hour] AS 'Cost Per Hour' FROM [court]"></asp:SqlDataSource>--%>


        <table class="auto-style2 table table-hover" Style="margin-right:auto; margin-left:auto;">
            <tr>
                <td>Court ID: </td>
                <td>
                    <asp:Label ID="lblCourtID" runat="server" Text=""></asp:Label>                    

                </td>
            </tr>
            <tr>
                <td>Location: </td>
                <td>
                    <asp:Label ID="lblCourtLocation" runat="server"></asp:Label>

                </td>
            </tr>
            <tr>
                <td>Cost Per Hour: </td>
                <td>
                    <asp:Label ID="lblCost" runat="server"></asp:Label>
                </td>
            </tr>
             <tr>
                <td> <asp:Label ID="lblcourtChooseDate" runat="server" Text="Choose a date:" Visible="false"></asp:Label> </td>
                <td>
                    <asp:TextBox ID="tbCourtChooseDate" runat="server" TextMode="Date" Visible="false">

                    </asp:TextBox>&nbsp;<asp:Button ID="btnShowAvailableCourtTime" runat="server" Text="Show Available Times" OnClick="btnShowAvailableCourtTime_Click" CssClass="btn btn-outline-info" Visible="false" />
                    <br />
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblCourtTimes" runat="server" Text="Choose a Time Slot (only one booking at a time):" Visible="false"></asp:Label> </td>
                <td>
                    <asp:DropDownList ID="ddlCourtTimes" runat="server" Visible="false"></asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnBookCourt" runat="server" Text="Book" OnClick="btnBookCourt_Click" CssClass="btn btn-outline-info" Visible="false" />
                </td>
            </tr>
        </table>


    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jsLinks" Runat="Server">
</asp:Content>

