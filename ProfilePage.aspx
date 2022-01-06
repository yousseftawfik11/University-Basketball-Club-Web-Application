<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProfilePage.aspx.cs" Inherits="ProfilePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssLinks" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageContent" runat="Server">

    <br />
    <br />
    <br />

    <div style="background-color: #f5f7f9">
        <br />
        <h1 style="text-align: center"><u>Profile Page</u></h1>
        <br />
    </div>



    <table class="table table-hover" style="margin-right: auto; margin-left: auto; width: auto" border="1">
        <tr>
            <td style="background-color: aliceblue">Profile Picture: </td>
            <td>
                <asp:Image ID="profileImage" runat="server" CssClass="rounded" /><%--Shows the profile picture of the user--%>
                <br />
                <asp:Button ID="btnProfilePicEdit" runat="server" Text="Edit" OnClick="btnProfilePicEdit_Click" CssClass="btn btn-outline-info text-center" />
                <br />
                <asp:FileUpload ID="fUploadProfilePic" runat="server" /><br />
                <%--Allows the user to upload a file (in this case a picture)--%>
                <asp:Button ID="btnProfilePicUpload" runat="server" Text="Upload" OnClick="btnProfilePicUpload_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                &nbsp;&nbsp;
                <asp:Label ID="lblimageStat" runat="server"></asp:Label>
                <%--<asp:Label ID="Label1" runat="server" ></asp:Label>--%>
            &nbsp;
                <asp:Button ID="btnProfilePicCancel" runat="server" Text="Cancel" OnClick="btnProfilePicCancel_Click" CssClass="btn btn-outline-info text-center" />
            </td>
        </tr>
        <tr>
            <td style="background-color: aliceblue">Username: </td>
            <td>
                <asp:TextBox ID="tbUserName" runat="server"></asp:TextBox>
                <asp:Button ID="btnUserEdit" runat="server" Text="Edit" OnClick="btnUserEdit_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnUserSave" runat="server" Text="Save" OnClick="btnUserSave_Click" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnUserCancel" runat="server" Text="Cancel" OnClick="btnUserCancel_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="UserName field can't be empty!" ControlToValidate="tbUserName" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator><%--ensures that the user doesn't leave this field empty--%>
                <asp:Label ID="lblUserCheck" runat="server" Text="User Name exists, please choose another name"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="background-color: aliceblue">Password:</td>
            <td>
                <asp:TextBox ID="tbPassword" runat="server"></asp:TextBox>
                <asp:Button ID="btnPasswordEdit" runat="server" Text="Edit" OnClick="btnPasswordEdit_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnPassowrdSave" runat="server" Text="Save" OnClick="btnPassowrdSave_Click" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnPasswordCancel" runat="server" Text="Cancel" OnClick="btnPasswordCancel_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Password field can't be empty!" ControlToValidate="tbPassword" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="background-color: aliceblue">First Name: </td>
            <td>
                <asp:TextBox ID="tbFName" runat="server"></asp:TextBox>
                <asp:Button ID="btnFNameEdit" runat="server" Text="Edit" OnClick="btnFNameEdit_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnFNameSave" runat="server" Text="Save" OnClick="btnFNameSave_Click" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnFNameCancel" runat="server" Text="Cancel" OnClick="btnFNameCancel_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <asp:RequiredFieldValidator ID="rfvFName" runat="server" ErrorMessage="First Name field can't be empty!" ControlToValidate="tbFName" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="background-color: aliceblue">Last Name: </td>
            <td>
                <asp:TextBox ID="tbLName" runat="server"></asp:TextBox>
                <asp:Button ID="btnLNameEdit" runat="server" Text="Edit" OnClick="btnLNameEdit_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnLNameSave" runat="server" Text="Save" OnClick="btnLNameSave_Click" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnLNameCancel" runat="server" Text="Cancel" OnClick="btnLNameCancel_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <asp:RequiredFieldValidator ID="rfvLName" runat="server" ErrorMessage="Last Name field can't be empty!" ControlToValidate="tbLName" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="background-color: aliceblue">Gender: </td>
            <td>
                <%--<asp:TextBox ID="tbGender" runat="server"></asp:TextBox>--%>
                <asp:RadioButtonList ID="rblGender" runat="server">
                    <asp:ListItem>Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                </asp:RadioButtonList>
                <asp:Button ID="btnGenderEdit" runat="server" Text="Edit" OnClick="btnGenderEdit_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnGenderSave" runat="server" Text="Save" OnClick="btnGenderSave_Click" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnGenderCancel" runat="server" Text="Cancel" OnClick="btnGenderCancel_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <%--<asp:RegularExpressionValidator ID="revGender" runat="server" ErrorMessage="Please type Male or Female" ControlToValidate="tbGender" ValidationExpression="^(M?m?ale|F?f?emale)$" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                <%--<asp:RequiredFieldValidator ID="rfvGender" runat="server" ErrorMessage="Gender field can't be empty!" ControlToValidate="tbGender" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td style="background-color: aliceblue">Email Address: </td>
            <td>
                <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
                <asp:Button ID="btnEmailEdit" runat="server" Text="Edit" OnClick="btnEmailEdit_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnEmailSave" runat="server" Text="Save" OnClick="btnEmailSave_Click" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnEmailCancel" runat="server" Text="Cancel" OnClick="btnEmailCancel_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Please enter a valid email" ControlToValidate="tbEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red"></asp:RegularExpressionValidator><%--ensures that the user enters a valid email format--%>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email Address field can't be empty!" ControlToValidate="tbEmail" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="background-color: aliceblue">Phone Number: </td>
            <td>
                <asp:TextBox ID="tbPhoneNumber" runat="server"></asp:TextBox>
                <asp:Button ID="btnPhoneEdit" runat="server" Text="Edit" OnClick="btnPhoneEdit_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnPhoneSave" runat="server" Text="Save" OnClick="btnPhoneSave_Click" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnPhoneCancel" runat="server" Text="Cancel" OnClick="btnPhoneCancel_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <asp:RegularExpressionValidator ID="revPhone" runat="server" ErrorMessage="Please enter a valid Malaysian Number" ControlToValidate="tbPhoneNumber" ValidationExpression="^(\+?6?01)[0-46-9]-*[0-9]{7,8}$" ForeColor="Red"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ErrorMessage="Phone Number field can't be empty!" ControlToValidate="tbPhoneNumber" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="background-color: aliceblue">Address: </td>
            <td>
                <asp:TextBox ID="tbAddress" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddressEdit" runat="server" Text="Edit" OnClick="btnAddressEdit_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnAddressSave" runat="server" Text="Save" OnClick="btnAddressSave_Click" CssClass="btn btn-outline-info text-center" />
                <asp:Button ID="btnAddressCancel" runat="server" Text="Cancel" OnClick="btnAddressCancel_Click" CausesValidation="False" CssClass="btn btn-outline-info text-center" />

            </td>
        </tr>
    </table>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="jsLinks" runat="Server">
</asp:Content>

