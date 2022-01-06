using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

public partial class login_register : System.Web.UI.Page
{
    bool messageStatus;// to use in the show message method.
    protected void Page_Load(object sender, EventArgs e)
    {


        checkUserLogin();
        showMessage();


        if (!IsPostBack)
        {

            mulvLoginRegister.ActiveViewIndex = 0;


        }
    }

    protected void lbRegister_Click(object sender, EventArgs e)
    {
        mulvLoginRegister.ActiveViewIndex = 1;

    }

    protected void lbToLogin_Click(object sender, EventArgs e)
    {
        mulvLoginRegister.ActiveViewIndex = 0;
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        registerNewUser();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        loginUser();
    }

    public void checkUserLogin()
    {
        if (Session["user_id"] != null) // if the user already loged in
        {
            // kick the user from the login and register page
            Response.Redirect("default.aspx");

        }
    }

    public void loginUser()
    {
        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;


        SqlConnection conn = new SqlConnection(constr);

        conn.Open();

        SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE username =@username and password=@password", conn);

        cmd.Parameters.AddWithValue("@username", txtLoginUsername.Text);
        cmd.Parameters.AddWithValue("@password", encryptSha1(txtLoginPassword.Text));


        SqlDataReader dr = cmd.ExecuteReader();
        bool recordfound = dr.Read();
        if (recordfound) //found data 
        {
            string status = dr["status"].ToString();

            if (status == "True")
            {
                string user_id = dr["user_id"].ToString();
                string username = dr["username"].ToString();
                string rold = dr["role"].ToString();
                // data is found and stored in strings
                Session["user_id"] = user_id;
                Session["username"] = username;
                Session["role"] = rold;
                Session.Timeout = 10; // the user will be logged in for 10 min only, 
                if (cbRememberMe.Checked) // if the user clicked on cbRememberMe checkbox
                {
                    rememberMeUsingCookie(txtLoginPassword.Text, txtLoginUsername.Text); // the method will encrypt the password and username then save it to the cookie
                }

                // save on the session the message with the type so the message will be appear if there is session detected called message and not null 
                Session["message"] = "Login successfully";
                Session["typeOfMessage"] = "success";
                // this way has been used becaue we want the user get the message while he/she is on another page , as after login , the user will be out from login_register.asp page 
                Response.Redirect(Request.RawUrl); // refrech the page, there is method has been created in the page_load , it will check if the user is loggedin, based on that the user will be out from login page
            }
            else
            {
                SetMessage("Sorry your account has been blocked", "danger"); // call the method to set message to the user 
            }
        }
        else
        {


            SetMessage("wrong username or password ,please try again", "danger"); // call the method to set message to the user 

        }
    }

    protected void txtLoginPassword_TextChanged(object sender, EventArgs e)
    {
        loginUser();
    }

    protected void txtLoginUsername_TextChanged(object sender, EventArgs e)
    {
        loginUser();
    }
    public void registerNewUser()
    {
        if (checkPhoneDuplication(txtPhone.Text) == false)
        {
            if (checkEmailDuplication(txtEmail.Text) == false)// if the email is not exist in the database
            {
                if (checkUsernameDuplication(txtUsername.Text) == false) // if the username is not exist in the database
                {
                    try
                    {
                        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
                        SqlConnection conn = new SqlConnection(constr);
                        conn.Open();
                        string insertQuery = "INSERT INTO [user] (fname, lname,email,username,password,phone_no,picture,role,status,gender) VALUES (@fname, @lname, @email, @username, @password, @phone_no, @picture, @role, @status, @gender)";
                        SqlCommand cmd = new SqlCommand(insertQuery, conn);
                        cmd.Parameters.AddWithValue("@fname", txtFname.Text);
                        cmd.Parameters.AddWithValue("@lname", txtLname.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        string encryptPassword = encryptSha1(txtPassword.Text);
                        cmd.Parameters.AddWithValue("@password", encryptPassword);
                        cmd.Parameters.AddWithValue("@phone_no", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@picture", "null.png");
                        cmd.Parameters.AddWithValue("@role", "0"); // default 
                        cmd.Parameters.AddWithValue("@status", "1");// default
                        cmd.Parameters.AddWithValue("@gender", rblGender.Text);
                        cmd.ExecuteNonQuery();
                        signUserAsMember(getUserId(txtUsername.Text));// call the method signUserAsMember to add the new user to the member table but it needs user_id so using the method get User Id by entering username it will return the user id to complete the process
                        SetMessage("register successfully", "success"); // call the method to set message to the user 
                        mulvLoginRegister.ActiveViewIndex = 0; // send the user to login after register successfully

                    }
                    catch (SqlException e)
                    {
                        System.Diagnostics.Debug.Write(e);
                    }
                }
                else // if the username is taken
                {
                    SetMessage("Username is taken, please type another username", "danger"); // call the method to set message to the user 
                }
            }
            else
            {
                SetMessage("email is exist! , if you forget your password ,click on forgot password to reset password", "danger"); // call the method to set message to the user
            }
        }
        else
        {
            SetMessage("Phone is exist! , if you forget your password ,click on forgot password to reset password", "danger"); // call the method to set message to the user

        }

    }
    public void signUserAsMember(string user_id)
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            string insertQuery = "INSERT INTO [member] (user_id) VALUES (@user_id)";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.ExecuteNonQuery();
        }
        catch (SqlException e)
        {
            System.Diagnostics.Debug.Write(e);
        }
    }

    public string getUserId(string username)
    {
        string user_id = null;
        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE username =@username", conn);
        cmd.Parameters.AddWithValue("@username", username);
        SqlDataReader dr = cmd.ExecuteReader();
        bool recordfound = dr.Read();
        if (recordfound) //found data 
        {
            user_id = dr["user_id"].ToString();
            return user_id;
        }
        else
        {
            return null;
        }
    }


    public bool checkUsernameDuplication(string username)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE username = @username", conn);

            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader dr = cmd.ExecuteReader();
            bool recordfound = dr.Read();
            //conn.Close();
            if (recordfound) //found data 
            {
                return true; // username is exist
            }
            else
            {
                return false;
            }
        }
        catch (SqlException e)
        {
            return false;
        }


    }
    public bool checkEmailDuplication(string email)
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE email = @email", conn);

            cmd.Parameters.AddWithValue("@email", email);
            SqlDataReader dr = cmd.ExecuteReader();
            bool recordfound = dr.Read();
            //conn.Close();
            if (recordfound) //found data 
            {
                return true; // email is exist
            }
            else
            {
                return false;
            }
        }
        catch (SqlException e)
        {
            return false;
        }
    }
    public bool checkPhoneDuplication(string phone_no)
    {

        try
        {
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE phone_no = @phone_no", conn);

            cmd.Parameters.AddWithValue("@phone_no", phone_no);
            SqlDataReader dr = cmd.ExecuteReader();
            bool recordfound = dr.Read();
            //conn.Close();
            if (recordfound) //found data 
            {
                return true; // phone is exist
            }
            else
            {
                return false;
            }
        }
        catch (SqlException e)
        {
            return false;
        }


    }

    public string encryptSha1(string data)
    {
        string sha1 = FormsAuthentication.HashPasswordForStoringInConfigFile(data, "sha1");
        return sha1;
    }
    protected void btnResetPassword_Click(object sender, EventArgs e)
    {

        resetPassword();
    }

    public void resetPassword()
    {

        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;

        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE email =@email and phone_no=@phone_no", conn);
        cmd.Parameters.AddWithValue("@email", txtResetPassword_Email.Text);
        cmd.Parameters.AddWithValue("@phone_no", txtResetPassword_PhoneNo.Text);
        SqlDataReader dr = cmd.ExecuteReader();
        bool recordfound = dr.Read();


        if (recordfound) //found data 
        {
            string user_id = dr["user_id"].ToString();
            conn.Close();

            string randomPassword = null;

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                string updateQuesry = "UPDATE [user] SET password=@password WHERE user_id=@user_id";
                SqlCommand com = new SqlCommand(updateQuesry, conn);
                {
                    randomPassword = generateRandomTempPass(); //get random pass
                    com.Parameters.AddWithValue("@password", encryptSha1(randomPassword)); //add it to the database after encrypt it
                    com.Parameters.AddWithValue("@user_id", user_id);

                    com.ExecuteNonQuery();
                }
                var newPasswordMessage = String.Format("password has been changed successfully to[ {0} ]please change it once you login", randomPassword);
                SetMessage(newPasswordMessage, "success"); // call the method to set message to the user 
                mulvLoginRegister.ActiveViewIndex = 0; //. send the user to login after reset the password
            }
        }
        else
        {
            // no user exist
            conn.Close();

            SetMessage("email or phone is not found,please try again!", "danger"); // call the method to set message to the user 

        }
    }

    public string generateRandomTempPass()
    {
        Random ran = new Random();

        String b = "abcdefghijklmnopqrstuvwxyz1234567890";

        int length = 7;

        String random = "";

        for (int i = 0; i < length; i++)
        {
            int a = ran.Next(26);
            random = random + b.ElementAt(a);
        }
        return random;
    }


    protected void lbFromRestPasswordToLogin_Click(object sender, EventArgs e)
    {
        clearFrom("login");
        mulvLoginRegister.ActiveViewIndex = 0;

    }

    protected void lbFromRestPasswordToRegister_Click(object sender, EventArgs e)
    {
        clearFrom("register");
        mulvLoginRegister.ActiveViewIndex = 1;

    }

    protected void lbFromRegisterToResetPass_Click(object sender, EventArgs e)
    {
        clearFrom("resetPassword");
        mulvLoginRegister.ActiveViewIndex = 2;

    }

    protected void lbFromLoginToResetPass_Click(object sender, EventArgs e)
    {
        clearFrom("resetPassword");
        mulvLoginRegister.ActiveViewIndex = 2;

    }

    protected void txtResetPassword_Email_TextChanged(object sender, EventArgs e)
    {
        resetPassword();
    }

    protected void txtResetPassword_Phone_TextChanged(object sender, EventArgs e)
    {
        resetPassword();
    }


    public void SetMessage(string message, string typeOfMessage)
    {
        string html = "<div style='width: fit-content;margin: auto;top: 130px;box-shadow: 10px 10px 10px 6px rgb(0 0 0 / 40%);  z-index: 10;' class='alert alert-type'><h6 style='text-align: center;'>message</h6></div>";
        string replaceHtml = html.Replace("type", typeOfMessage).Replace("message", message);
        messageStatus = true;
        lblMessage.Text = replaceHtml;

    }

    public void showMessage()
    {

        if (messageStatus == true) // there is new message detected
        {
            messageStatus = false;// close it so it will not appear again
        }
        else // there is no new message to appear
        {
            lblMessage.Text = null;
        }
    }

    public void rememberMeUsingCookie(string password, string username)
    {
        HttpCookie rememberMe = new HttpCookie("rememberMe");
        //encrypt Sha1 the password then save it to the cookies as well as the username with some unnecessary data so the user will not know what is it if he/she checks the cookie in the browser
        // when we will call the cookies to Relogin the user, by using split method we will remove the unnecessary data and use only username and password to Relogin the user
        rememberMe["rememberMe"] = encryptSha1(password) + "|" + username + "|" + "!@#$%^*^&*(^&%^%$@#$" + "|" + generateRandomTempPass();
        Response.Cookies.Add(rememberMe);
        rememberMe.Expires = DateTime.Now.AddDays(30); // for 30 days 
    }

    public void clearFrom(string fromName)
    {
        switch (fromName)
        {

            case "register":
                {
                    txtFname.Text = txtLname.Text = txtEmail.Text = txtUsername.Text = txtPassword.Text = txtPhone.Text = txtRepeatPassword.Text = "";
                    rblGender.SelectedIndex = -1;
                    break;
                }
            case "login":
                {

                    txtLoginUsername.Text = txtLoginPassword.Text = "";

                    break;
                }
            case "resetPassword":
                {
                    txtResetPassword_Email.Text = txtResetPassword_PhoneNo.Text = "";
                    break;
                }
        }

    }

}