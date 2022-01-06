using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProfilePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Prevents users from editing those fileds
        tbUserName.Enabled = false;
        tbPassword.Enabled = false;
        tbFName.Enabled = false;
        tbLName.Enabled = false;
        rblGender.Enabled = false;
        tbEmail.Enabled = false;
        tbPhoneNumber.Enabled = false;
        tbAddress.Enabled = false;
        //Hides buttons from users
        btnProfilePicUpload.Visible = false;
        btnProfilePicCancel.Visible = false;
        fUploadProfilePic.Visible = false;
        btnUserSave.Visible = false;
        btnUserCancel.Visible = false;
        btnPassowrdSave.Visible = false;
        btnPasswordCancel.Visible = false;
        btnFNameSave.Visible = false;
        btnFNameCancel.Visible = false;
        btnLNameSave.Visible = false;
        btnLNameCancel.Visible = false;
        btnGenderSave.Visible = false;
        btnGenderCancel.Visible = false;
        btnEmailSave.Visible = false;
        btnEmailCancel.Visible = false;
        btnPhoneSave.Visible = false;
        btnPhoneCancel.Visible = false;
        btnAddressSave.Visible = false;
        btnAddressCancel.Visible = false;
        checkUserLogin();
        if (!IsPostBack)//resets the data that appear in the page based on the database
        {
            displayInfo();
        }
        lblUserCheck.Visible = false;



    }
    public void checkUserLogin()//ensures that the user is logged in before using this function, otherwise they are redirected to the homepage
    {
        if (Session["user_id"] == null) // if the user already loged in
        {
            Session["message"] = "Please log in first to use this function!";
            Session["typeOfMessage"] = "warning";
            // kick the user from the login and register page
            Response.Redirect("login_register.aspx");

        }
    }
    protected void displayInfo()//function to display the data in the database
    {
        try
        {
            string user_id = Session["user_id"].ToString();//Grabs the user id that logged in to get his/her info
            if (Session["user_id"] != null)
            {
                //SqlConnection conn = new SqlConnection();
                string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE user_id=@user_id", conn);//a query to grab all data about the logged in user
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                SqlDataReader dReader = cmd.ExecuteReader();
                bool userfound = dReader.Read();
                if (userfound)
                {
                    string profilePic = dReader["picture"].ToString();
                    string UserName = dReader["username"].ToString();
                    string password = dReader["password"].ToString();
                    string fName = dReader["fname"].ToString();
                    string lName = dReader["lname"].ToString();
                    string gender = dReader["gender"].ToString();
                    string email = dReader["email"].ToString();
                    string phoneNo = dReader["phone_no"].ToString();
                    string address = dReader["address"].ToString();

                    profileImage.ImageUrl = "~/assets/ProfilePics/" + profilePic;//The image is saved in the database as a string, so to view it, we add the path of the image to the picture name
                    tbUserName.Text = UserName;
                    tbPassword.Text = "Password doesn't show for privacy";//Hiding the password for privacy
                    tbFName.Text = fName;
                    tbLName.Text = lName;
                    if (gender == "Male")//Displaying the gender of the user into the radio button list based on the stored data
                    {
                        rblGender.SelectedIndex = 0;
                    }
                    else
                    {
                        rblGender.SelectedIndex = 1;
                    }
                    tbEmail.Text = email;
                    tbPhoneNumber.Text = phoneNo;
                    tbAddress.Text = address;
                }
                //conn.Close();

            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error " + ex.Message);
        }
    }

    protected void btnUserCancel_Click(object sender, EventArgs e)//When the user presses the cancel box, the saved value will be re-displayed
    {
        string user_id = Session["user_id"].ToString();//Grabs the user id that logged in to get his/her info
        try
        {
            if (Session["user_id"] != null)
            {
                string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE user_id=@user_id", conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                SqlDataReader dReader = cmd.ExecuteReader();
                bool userfound = dReader.Read();
                if (userfound)
                {
                    string UserName = dReader["username"].ToString();//gets the saved value
                    tbUserName.Text = UserName;//pastes the saved value back into the textbox
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error " + ex.Message);
        }
    }

    protected void btnUserEdit_Click(object sender, EventArgs e)//Enables the user to edit the info
    {
        btnUserSave.Visible = true;
        btnUserCancel.Visible = true;
        tbUserName.Enabled = true;
    }

    protected void btnPasswordEdit_Click(object sender, EventArgs e)
    {
        btnPassowrdSave.Visible = true;
        btnPasswordCancel.Visible = true;
        tbPassword.Enabled = true;
        tbPassword.Text = null;
    }

    protected void btnPasswordCancel_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();//Grabs the user id that logged in to get his/her info
        try
        {
            if (Session["user_id"] != null)
            {
                string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE user_id=@user_id", conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                SqlDataReader dReader = cmd.ExecuteReader();
                bool userfound = dReader.Read();
                if (userfound)
                {
                    //string password = dReader["password"].ToString();//gets the saved value
                    tbPassword.Text = "Password doesn't show for privacy";//pastes the saved value back into the textbox
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error " + ex.Message);
        }
    }

    protected void btnFNameEdit_Click(object sender, EventArgs e)
    {
        btnFNameSave.Visible = true;
        btnFNameCancel.Visible = true;
        tbFName.Enabled = true;
    }

    protected void btnFNameCancel_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();//Grabs the user id that logged in to get his/her info
        try
        {
            if (Session["user_id"] != null)
            {
                string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE user_id=@user_id", conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                SqlDataReader dReader = cmd.ExecuteReader();
                bool userfound = dReader.Read();
                if (userfound)
                {
                    string fName = dReader["fname"].ToString();//gets the saved value
                    tbFName.Text = fName;//pastes the saved value back into the textbox
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error " + ex.Message);
        }
    }

    protected void btnLNameEdit_Click(object sender, EventArgs e)
    {
        btnLNameSave.Visible = true;
        btnLNameCancel.Visible = true;
        tbLName.Enabled = true;
    }

    protected void btnLNameCancel_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();//Grabs the user id that logged in to get his/her info
        try
        {
            if (Session["user_id"] != null)
            {
                string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE user_id=@user_id", conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                SqlDataReader dReader = cmd.ExecuteReader();
                bool userfound = dReader.Read();
                if (userfound)
                {
                    string lName = dReader["lname"].ToString();//gets the saved value
                    tbLName.Text = lName;//pastes the saved value back into the textbox
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error " + ex.Message);
        }
    }

    protected void btnGenderEdit_Click(object sender, EventArgs e)
    {
        btnGenderSave.Visible = true;
        btnGenderCancel.Visible = true;
        //tbGender.Enabled = true;
        rblGender.Enabled = true;
    }

    protected void btnGenderCancel_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();//Grabs the user id that logged in to get his/her info
        try
        {
            if (Session["user_id"] != null)
            {
                string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE user_id=@user_id", conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                SqlDataReader dReader = cmd.ExecuteReader();
                bool userfound = dReader.Read();
                if (userfound)
                {
                    string gender = dReader["gender"].ToString();//gets the saved value
                    //tbGender.Text = gender;//pastes the saved value back into the textbox
                    if (gender == "Male")
                    {
                        rblGender.SelectedIndex = 0;
                    }
                    else
                    {
                        rblGender.SelectedIndex = 1;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error " + ex.Message);
        }
    }

    protected void btnEmailEdit_Click(object sender, EventArgs e)
    {
        btnEmailSave.Visible = true;
        btnEmailCancel.Visible = true;
        tbEmail.Enabled = true;
    }

    protected void btnEmailCancel_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();//Grabs the user id that logged in to get his/her info
        try
        {
            if (Session["user_id"] != null)
            {
                string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE user_id=@user_id", conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                SqlDataReader dReader = cmd.ExecuteReader();
                bool userfound = dReader.Read();
                if (userfound)
                {
                    string email = dReader["email"].ToString();//gets the saved value
                    tbEmail.Text = email;//pastes the saved value back into the textbox
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error " + ex.Message);
        }
    }

    protected void btnPhoneEdit_Click(object sender, EventArgs e)
    {
        btnPhoneSave.Visible = true;
        btnPhoneCancel.Visible = true;
        tbPhoneNumber.Enabled = true;
    }

    protected void btnPhoneCancel_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();//Grabs the user id that logged in to get his/her info
        try
        {
            if (Session["user_id"] != null)
            {
                string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE user_id=@user_id", conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                SqlDataReader dReader = cmd.ExecuteReader();
                bool userfound = dReader.Read();
                if (userfound)
                {
                    string phone = dReader["phone_no"].ToString();//gets the saved value
                    tbPhoneNumber.Text = phone;//pastes the saved value back into the textbox
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error " + ex.Message);
        }
    }

    protected void btnAddressEdit_Click(object sender, EventArgs e)
    {
        btnAddressSave.Visible = true;
        btnAddressCancel.Visible = true;
        tbAddress.Enabled = true;
    }

    protected void btnAddressCancel_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();//Grabs the user id that logged in to get his/her info
        try
        {
            if (Session["user_id"] != null)
            {
                string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE user_id=@user_id", conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                SqlDataReader dReader = cmd.ExecuteReader();
                bool userfound = dReader.Read();
                if (userfound)
                {
                    string address = dReader["address"].ToString();//gets the saved value
                    tbAddress.Text = address;//pastes the saved value back into the textbox
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error " + ex.Message);
        }
    }

    protected void btnUserSave_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();
        if (tbUserName != null)
        {
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(constr))
            {

                //UPDATE user(username) VALUES(@username) WHERE user_id = @user_id
                //UPDATE user SET username = @username WHERE user_id = @user_id
                string query = "UPDATE [user] SET username = @username WHERE user_id = @user_id";//A query that populates the updated info into the database
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                cmd.Parameters.AddWithValue("@username", tbUserName.Text);/*Telling the query where to get the new username from*/
                cmd.Connection.Open();

                int flag = 0;//Flag used in the while loop to control when the user can save
                while (flag == 0)
                {
                    bool check;
                    check = detectUsernamesimilarities(tbUserName.Text);
                    if (check == false)
                    {
                        flag = 1;

                    }
                    else
                    {
                        Session["message"] = "Username already exists, please choose another username!";//A notification that will inform the user that the data has been updated
                        Session["typeOfMessage"] = "danger";
                        Response.Redirect(Request.RawUrl);
                    }

                }


                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error " + ex.Message);
                }
            }


            Session["message"] = "Username Succesfully Changed!";//A notification that will inform the user that the data has been updated
            Session["typeOfMessage"] = "success";
            Response.Redirect(Request.RawUrl);//redirects the user to the same page that they were on


        }
    }

    protected void btnPassowrdSave_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();
        if (tbPassword != null)
        {
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                string query = "UPDATE [user] SET password = @password WHERE user_id = @user_id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                cmd.Parameters.AddWithValue("@password", encryptSha1(tbPassword.Text));/*Telling the query where to get the new password from*/
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error " + ex.Message);
                }
            }


            Session["message"] = "Password Succesfully Changed!";
            Session["typeOfMessage"] = "success";
            Response.Redirect(Request.RawUrl);


        }
    }

    protected void btnFNameSave_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();
        if (tbFName != null)
        {
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                string query = "UPDATE [user] SET fname = @fname WHERE user_id = @user_id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                cmd.Parameters.AddWithValue("@fname", tbFName.Text);/*Telling the query where to get the new first name from*/
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error " + ex.Message);
                }
            }


            Session["message"] = "First Name Succesfully Changed!";
            Session["typeOfMessage"] = "success";
            Response.Redirect(Request.RawUrl);


        }
    }

    protected void btnLNameSave_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();
        if (tbLName != null)
        {
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                string query = "UPDATE [user] SET lname = @lname WHERE user_id = @user_id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                cmd.Parameters.AddWithValue("@lname", tbLName.Text);/*Telling the query where to get the new last name from*/
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error " + ex.Message);
                }
            }


            Session["message"] = "Last Name Succesfully Changed!";
            Session["typeOfMessage"] = "success";
            Response.Redirect(Request.RawUrl);


        }
    }

    protected void btnGenderSave_Click(object sender, EventArgs e)
    {
        String gend;
        if (rblGender.SelectedIndex == 0)//Saving the gender in a variable based on the selection of the radio button list
        {
            gend = "Male";
        }
        else
        {
            gend = "Female";
        }


        string user_id = Session["user_id"].ToString();

        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(constr))
        {
            string query = "UPDATE [user] SET gender = @gender WHERE user_id = @user_id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
            cmd.Parameters.AddWithValue("@gender", gend);/*Telling the query where to get the new gender from*/
            cmd.Connection.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }
        }


        Session["message"] = "Gender Succesfully Changed!";
        Session["typeOfMessage"] = "success";
        Response.Redirect(Request.RawUrl);


        //}
    }

    protected void btnEmailSave_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();

        //if(!string.IsNullOrWhiteSpace(tbGender.Text))
        if (tbEmail != null)
        {
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                string query = "UPDATE [user] SET email = @email WHERE user_id = @user_id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                cmd.Parameters.AddWithValue("@email", tbEmail.Text);/*Telling the query where to get the new email from*/
                cmd.Connection.Open();

                int flag = 0;//Flag used in the while loop to control when the user can save
                while (flag == 0)
                {
                    bool check;
                    check = detectEmailSimilarities(tbEmail.Text);
                    if (check == false)
                    {
                        flag = 1;

                    }
                    else
                    {
                        Session["message"] = "Email already exists, please choose your correct Email!";//A notification that will inform the user that the data has been updated
                        Session["typeOfMessage"] = "danger";
                        Response.Redirect(Request.RawUrl);
                    }

                }

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error " + ex.Message);
                }
            }


            Session["message"] = "Email Address Succesfully Changed!";
            Session["typeOfMessage"] = "success";
            Response.Redirect(Request.RawUrl);


        }
    }

    protected void btnPhoneSave_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();

        //if(!string.IsNullOrWhiteSpace(tbGender.Text))
        if (tbPhoneNumber != null)
        {
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                string query = "UPDATE [user] SET phone_no = @phone WHERE user_id = @user_id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                cmd.Parameters.AddWithValue("@phone", tbPhoneNumber.Text);/*Telling the query where to get the new phone number from*/
                cmd.Connection.Open();

                int flag = 0;//Flag used in the while loop to control when the user can save
                while (flag == 0)
                {
                    bool check;
                    check = detectPhoneSimilarities(tbPhoneNumber.Text);
                    if (check == false)
                    {
                        flag = 1;

                    }
                    else
                    {
                        Session["message"] = "Phone number already exists, please choose your correct Phone number!";//A notification that will inform the user that the data has been updated
                        Session["typeOfMessage"] = "danger";
                        Response.Redirect(Request.RawUrl);
                    }

                }

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error " + ex.Message);
                }
            }


            Session["message"] = "Phone Number Succesfully Changed!";
            Session["typeOfMessage"] = "success";
            Response.Redirect(Request.RawUrl);


        }
    }

    protected void btnAddressSave_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();

        //if(!string.IsNullOrWhiteSpace(tbGender.Text))
        if (tbAddress != null)
        {
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(constr))
            {
                string query = "UPDATE [user] SET address = @address WHERE user_id = @user_id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                cmd.Parameters.AddWithValue("@address", tbAddress.Text);/*Telling the query where to get the new address from*/
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error " + ex.Message);
                }
            }


            Session["message"] = "Address Succesfully Changed!";
            Session["typeOfMessage"] = "success";
            Response.Redirect(Request.RawUrl);


        }
    }
    public string encryptSha1(string data)
    {
        string sha1 = FormsAuthentication.HashPasswordForStoringInConfigFile(data, "sha1");
        return sha1;
    }

    protected void btnProfilePicUpload_Click(object sender, EventArgs e)
    {
        if (fUploadProfilePic.HasFile)
        {
            try
            {
                if (fUploadProfilePic.PostedFile.ContentType == "image/jpeg" || fUploadProfilePic.PostedFile.ContentType == "image/png")//Ensures that the file that the user choosed is an imapge of type JPEG or png
                {
                    //Saving image in the folder
                    string filename = Path.GetFileName(fUploadProfilePic.FileName);
                    fUploadProfilePic.SaveAs(Server.MapPath("~/assets/ProfilePics/") + filename);


                    lblimageStat.Text = "Upload Status: File uploaded succesfully!";
                    lblimageStat.ForeColor = System.Drawing.Color.Green;

                    //adding image to database
                    string user_id = Session["user_id"].ToString();
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString);
                    try
                    {
                        conn.Open();
                        string insertQuery = "UPDATE [user] SET picture=@picture WHERE user_id=@user_id";//To insert data into the logged in user table
                        SqlCommand com = new SqlCommand(insertQuery, conn);
                        com.Parameters.AddWithValue("@picture", fUploadProfilePic.PostedFile.FileName);//uploads the file name of the picture to the database
                        com.Parameters.AddWithValue("@user_id", user_id);
                        com.ExecuteNonQuery();
                        conn.Close();

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error " + ex.Message);
                    }

                }
                else
                {
                    lblimageStat.Text = "Only JPEG/png files accepted!";
                    lblimageStat.ForeColor = System.Drawing.Color.Red;
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }
        }
        Session["message"] = "Profile Picture Succesfully Changed!";
        Session["typeOfMessage"] = "success";
        Response.Redirect(Request.RawUrl);
    }

    protected void btnProfilePicEdit_Click(object sender, EventArgs e)
    {
        btnProfilePicUpload.Visible = true;
        btnProfilePicCancel.Visible = true;
        fUploadProfilePic.Visible = true;
    }

    protected void btnProfilePicCancel_Click(object sender, EventArgs e)
    {
        btnProfilePicUpload.Visible = false;
        btnProfilePicCancel.Visible = false;
        fUploadProfilePic.Visible = false;
    }
    public bool detectUsernamesimilarities(string username)
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
            if (recordfound) //checks if the data is found 
            {
                return true; //returns true if the username was found
            }
            else
            {
                return false; //returns false if the username was found
            }
        }
        catch (SqlException ex)
        {
            return false;
        }


    }

    public bool detectEmailSimilarities(string email)
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
            if (recordfound) //checks if the data is found 
            {
                return true; //returns true if the email was found
            }
            else
            {
                return false; //returns false if the email was found
            }
        }
        catch (SqlException ex)
        {
            return false;
        }


    }
    public bool detectPhoneSimilarities(string phone)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE phone_no = @phone_no", conn);

            cmd.Parameters.AddWithValue("@phone_no", phone);
            SqlDataReader dr = cmd.ExecuteReader();
            bool recordfound = dr.Read();
            if (recordfound) //checks if the data is found 
            {
                return true; //returns true if the Phone was found
            }
            else
            {
                return false; //returns false if the Phone was found
            }
        }
        catch (SqlException ex)
        {
            return false;
        }

    }
}