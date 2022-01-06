using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        checkUserLoginStatus();
        messageFromSession(); // show message if there is message saved in the session from another page 
        remainUserLogin(); // if there is cookie saved 

    }


    public void messageFromSession()
    {
        if (Session["message"] == null || Session["typeOfMessage"] == null)
        {

            divMessage.Visible = false;

        }
        else
        {
            lblMessage.Visible = true;
            // get the message and it's type from the session
            string message = Session["message"].ToString();
            string typeOfMessage = Session["typeOfMessage"].ToString();

            string html = "<div style='width: fit-content;margin: auto;top: 130px;box-shadow: 10px 10px 10px 6px rgb(0 0 0 / 40%);  z-index: 10;' class='alert alert-type'><h6 style='text-align: center;'>message</h6></div>";

            string replaceHtml = html.Replace("type", typeOfMessage).Replace("message", message);


            // the html will be print in the label lblMessage which is exist in the master page 
            lblMessage.Text = replaceHtml;

            // clean the season so the message will not appear again after refresh
            //Session.Remove("message");
            // Session.Remove("typeOfMessage");
            Session["message"] = null;
            Session["typeOfMessage"] = null;


        }

    }

    public void checkUserLoginStatus() //if the user loggedin , it will show in the navbar logout link button instead of login_register
    {

        if (Session["user_id"] != null && Session["username"] != null && Session["role"] != null)
        {
            LbuserStatus.Text = "Logout";
        }
        else
        {
            LbuserStatus.Text = "Login/Register";
        }

    }

    protected void LbuserStatus_Click(object sender, EventArgs e)
    {  // if the user click on the link button 


        if (Session["user_id"] != null || Session["username"] != null || Session["role"] != null) // if the user is loggedin
        {

            Session.Remove("user_id");
            Session.Remove("username");
            Session.Remove("role");

            Session["message"] = "Log Out successfully";
            Session["typeOfMessage"] = "success";
            HttpCookie rememberMe = new HttpCookie("rememberMe");
            // clear the cookie
            if (Request.Cookies["rememberMe"] != null)
            {
                Response.Cookies["rememberMe"].Expires = DateTime.Now.AddDays(-1);
            }

            Response.Redirect(Request.RawUrl);

            // refresh the page after clean the session
            Response.Redirect(Request.RawUrl);
        }
        else // if the user is not logged in
        {
            Server.Transfer("login_register.aspx");
        }




    }
    public void remainUserLogin() // this method will check if there is a cookie saved for the user, it will relogin the user automatically, this work based on if the user click on remember me on the login page
    {

        if (Session["user_id"] == null)
        {
            HttpCookie rememberMe = new HttpCookie("rememberMe");
            rememberMe = Request.Cookies["rememberMe"];   
                if (rememberMe != null) // if the cookie is  exist 
                {                   
                    if ( rememberMe["rememberMe"] != null && rememberMe["rememberMe"].ToString().Contains("|") == true) // if the cookie has data
                        
                    {                 
                        rememberMe = Request.Cookies["rememberMe"];
                        // as there is unnecessary data in the cookies to mislead the hackers , using split , we will get the only information we want 
                       

                        string[] splitRememberMeCookie = rememberMe["rememberMe"].ToString().Split('|');
                        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
                        SqlConnection conn = new SqlConnection(constr);
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE password =@password and username=@username", conn);
                        cmd.Parameters.AddWithValue("@password", splitRememberMeCookie[0]); // ffrom the cookie
                        cmd.Parameters.AddWithValue("@username", splitRememberMeCookie[1]); // ffrom the cookie
                        System.Diagnostics.Debug.Write("testing the code only ++++++++++ "+ splitRememberMeCookie[0]);
                        System.Diagnostics.Debug.Write("testing the code only ++++++++++ " + splitRememberMeCookie[1]);
                        SqlDataReader dr = cmd.ExecuteReader();
                        bool recordfound = dr.Read();
                        if (recordfound) //found data 
                        {

                            string user_id = dr["user_id"].ToString();
                            string username = dr["username"].ToString();
                            string rold = dr["role"].ToString();
                            // data is found and stored in strings
                            Session["user_id"] = user_id;
                            Session["username"] = username;
                            Session["role"] = rold;
                            Session.Timeout = 10; // the user will be logged in for 10 min only, 
                            Session["message"] = "Relogin successfully by remember me feature";
                            Session["typeOfMessage"] = "success";
                            Response.Redirect(Request.RawUrl);
                        }
                    }
                    else
                    {
                        Session["message"] = "Modifying cookie detected , please do not modify the cookie to avoid getting error";
                        Session["typeOfMessage"] = "danger";


                        if (Request.Cookies["rememberMe"] != null) // Remove the cookie which the user changed 
                        {
                            Response.Cookies["rememberMe"].Expires = DateTime.Now.AddDays(-1);
                        }
                        Response.Redirect(Request.RawUrl);
                    }
                }
        }

    }

}
