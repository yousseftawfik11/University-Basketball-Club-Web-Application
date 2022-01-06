<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="login_register.aspx.cs" Inherits="login_register" %>

<asp:Content ID="PageTitle" ContentPlaceHolderID="head" runat="Server">
    login/register
</asp:Content>

<asp:Content ID="cssLinks" ContentPlaceHolderID="cssLinks" runat="Server">




    <%--for the additional css links--%>
</asp:Content>



<asp:Content ID="PageContent" ContentPlaceHolderID="PageContent" runat="Server">

    <br />
    <br />
    <asp:Label ID="lblMessage" runat="server"></asp:Label>
    <asp:MultiView ID="mulvLoginRegister" runat="server">
        <asp:View ID="login" runat="server">
            <div class="bg-gradient-primary">
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-md-9 col-lg-12 col-xl-10">
                            <div class="card shadow-lg o-hidden border-0 my-5">
                                <div class="card-body p-0">
                                    <div class="row">
                                        <div class="col-lg-6 d-none d-lg-flex">
                                            <div class="flex-grow-1 bg-login-image" style="background-image: url(&quot;assets/img/121.jpg&quot;);">
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="p-5">
                                                <div class="text-center">
                                                    <h4 class="text-dark mb-4">Welcome Back!</h4>
                                                </div>
                                                <div class="form-group">
                                                    <asp:TextBox class="form-control form-control-user" placeholder="Username" Display="Dynamic" ID="txtLoginUsername" runat="server" OnTextChanged="txtLoginUsername_TextChanged"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvalidUsername" runat="server" Display="Dynamic" ControlToValidate="txtLoginUsername" ErrorMessage="Username is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <asp:TextBox class="form-control form-control-user" placeholder="Password" TextMode="Password" ID="txtLoginPassword" runat="server" OnTextChanged="txtLoginPassword_TextChanged"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvalidPassword" runat="server" ControlToValidate="txtLoginUsername" ErrorMessage="Password is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <div class="custom-control custom-checkbox small">
                                                        <div class="form-check">
                                                            <asp:CheckBox class="form-check-input" ID="cbRememberMe" runat="server" Text="&nbsp;Remeber me" Style="font-size: 120%" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:Button class="btn btn-primary btn-block text-white btn-user" Style="background-color: #2C3E50;" ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                                                <div class="text-center">
                                                    <asp:LinkButton ID="lbFromLoginToResetPass" runat="server" CausesValidation="false" OnClick="lbFromLoginToResetPass_Click">Forgot Password? </asp:LinkButton>
                                                </div>
                                                <div class="text-center">
                                                    <asp:LinkButton ID="lbRegister" runat="server" CausesValidation="false" OnClick="lbRegister_Click">Create an
                                                Account!</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>

        <asp:View ID="register" runat="server">
            <div class="bg-gradient-primary">
                <div class="container">
                    <div class="card shadow-lg o-hidden border-0 my-5">
                        <div class="card-body p-0">
                            <div class="row">
                                <div class="col-lg-5 d-none d-lg-flex">
                                    <div class="flex-grow-1 bg-register-image"
                                        style="background-image: url(&quot;assets/img/121.jpg&quot;);">
                                    </div>
                                </div>
                                <div class="col-lg-7">
                                    <div class="p-5">
                                        <div class="text-center">
                                            <h4 class="text-dark mb-4">Create an Account!</h4>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-6 mb-3 mb-sm-0">
                                                <asp:TextBox class="form-control form-control-user" ID="txtFname" runat="server" placeholder="First name"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFname" runat="server" Display="Dynamic" ControlToValidate="txtFname" ErrorMessage="First name is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:TextBox class="form-control form-control-user" ID="txtLname" runat="server" placeholder="Last name"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvLname" runat="server" Display="Dynamic" ControlToValidate="txtLname" ErrorMessage="Last name is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-6 mb-3 mb-sm-0">
                                                <asp:TextBox class="form-control form-control-user" ID="txtEmail" runat="server" TextMode="Email" placeholder="Email"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail" ErrorMessage="Email is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="Invalid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:TextBox class="form-control form-control-user" ID="txtUsername" runat="server" placeholder="Username"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" Display="Dynamic" ControlToValidate="txtUsername" ErrorMessage="username is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-6 mb-3 mb-sm-0">
                                                <asp:TextBox class="form-control form-control-user" ID="txtPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Password is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:TextBox class="form-control form-control-user" ID="txtPhone" runat="server" placeholder="Phone Number"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revPhoneNumber" runat="server" ControlToValidate="txtPhone" ErrorMessage="Malaysian mobile phones only" Display="Dynamic" ForeColor="Red" ValidationExpression="^(\+?6?01)[0-46-9]-*[0-9]{7,8}$"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" ControlToValidate="txtPhone" Display="Dynamic" ErrorMessage="Phone number is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-6">
                                                <asp:TextBox class="form-control form-control-user" ID="txtRepeatPassword" runat="server" placeholder="Repeat Password" TextMode="Password"></asp:TextBox>
                                                <asp:CompareValidator ID="cvRepeatPassword" ControlToValidate="txtRepeatPassword" Display="Dynamic" ForeColor="Red" ControlToCompare="txtPassword" runat="server" Operator="Equal" ErrorMessage="Password must match"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="rfvRepeatPassword" runat="server" ControlToValidate="txtRepeatPassword" Display="Dynamic" ErrorMessage="Repeat Password is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-sm-6">
                                                Gender
                                                <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="rblGender" ErrorMessage="Select gender"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <asp:Button class="btn btn-primary btn-block text-white btn-user" Style="background-color: #2C3E50;" ID="btnRegister" runat="server" Text="Register an account" OnClick="btnRegister_Click" />
                                        <div class="text-center">
                                            <asp:LinkButton ID="lbFromRegisterToResetPass" runat="server" CausesValidation="false" OnClick="lbFromRegisterToResetPass_Click">Forgot Password? </asp:LinkButton>
                                        </div>
                                        <div class="text-center">
                                            <asp:LinkButton ID="lbToLogin" runat="server" CausesValidation="false" OnClick="lbToLogin_Click">Already have an account?
                                    Login!</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </asp:View>

        <asp:View ID="ViewResetPassword" runat="server">


            <div class="bg-gradient-primary">

                <div class="container">

                    <div class="row justify-content-center">
                        <div class="col-md-9 col-lg-12 col-xl-10">
                            <div class="card shadow-lg o-hidden border-0 my-5">
                                <div class="card-body p-0">
                                    <div class="row">
                                        <div class="col-lg-6 d-none d-lg-flex">
                                            <div class="flex-grow-1 bg-login-image" style="background-image: url(&quot;assets/img/121.jpg&quot;);">
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="p-5">
                                                <div class="text-center">
                                                    <h4 class="text-dark mb-4">Reset password</h4>
                                                </div>

                                                <div class="form-group">
                                                    <asp:TextBox class="form-control form-control-user" placeholder="Email" Display="Dynamic" ID="txtResetPassword_Email" runat="server" OnTextChanged="txtResetPassword_Email_TextChanged"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvResetPassword_Email" runat="server" Display="Dynamic" ControlToValidate="txtResetPassword_Email" ErrorMessage="Email is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <asp:TextBox class="form-control form-control-user" placeholder="Phone No" TextMode="Password" ID="txtResetPassword_PhoneNo" runat="server" OnTextChanged="txtResetPassword_Phone_TextChanged"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvResetPassword_PhoneNo" runat="server" ControlToValidate="txtResetPassword_PhoneNo" ErrorMessage="Phone number is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <div class="custom-control custom-checkbox small">
                                                    </div>
                                                </div>

                                                <asp:Button class="btn btn-primary btn-block text-white btn-user" Style="background-color: #2C3E50;" ID="btnResetPassword" runat="server" Text="Reset password" />
                                                <div class="text-center">
                                                    <asp:LinkButton ID="lbFromRestPasswordToLogin" runat="server" CausesValidation="false" OnClick="lbFromRestPasswordToLogin_Click">Do you have an account,login!</asp:LinkButton>
                                                </div>
                                                <div class="text-center">
                                                    <asp:LinkButton ID="lbFromRestPasswordToRegister" runat="server" CausesValidation="false" OnClick="lbFromRestPasswordToRegister_Click">Create an
                                                Account!</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
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

<asp:Content ID="jsLinks" ContentPlaceHolderID="jsLinks" runat="Server">
</asp:Content>



