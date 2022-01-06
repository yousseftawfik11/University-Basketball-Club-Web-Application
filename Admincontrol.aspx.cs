using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admincontrol : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
    bool messageStatus;// to use in the show message method.
    protected void Page_Load(object sender, EventArgs e)
    {


        checklogin();

        showMessage();
        if (!IsPostBack)
        {

            UserBindGrid();
            mlvAdminControlPanel.ActiveViewIndex = 0;


        }

    }

    private void UserBindGrid()
    {//binding data
        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM [user]"))
            {

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        gvUsers.DataSource = dt;
                        gvUsers.DataBind();
                    }
                }
            }
        }
    }



    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUsers.PageIndex = e.NewPageIndex;

    }




    protected void ddlShowUsers_SelectedIndexChanged(object sender, EventArgs e)
    {

        gvUsers.PageSize = Convert.ToInt32(ddlPageSizeUsers.SelectedValue);



        UserBindGrid();

    }

    protected void btnUserSelect_Click(object sender, EventArgs e)
    {
        int SelectedRow = ((GridViewRow)(sender as Control).NamingContainer).RowIndex; //to read row index of grid view
        getUserDataFromTable(SelectedRow);
        divUserEdit.Visible = true;// show div for editing and adding,remove
        btnUpdate.Visible = true;
        btnDelete.Visible = true;
        btnAddNewUser.Visible = false;
        btnSaveNewUser.Visible = false;

    }

    protected void rblSelectImg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblSelectImg.SelectedIndex == 0)
        {
            fuUserImg.Visible = true;
        }
        else
        {
            fuUserImg.Visible = false;
        }
    }


    public void getUserDataFromTable(int rowNo)
    {
        lblUser_id.Text = gvUsers.Rows[rowNo].Cells[0].Text;
        txtUsername.Text = gvUsers.Rows[rowNo].Cells[2].Text;
        txtFname.Text = gvUsers.Rows[rowNo].Cells[3].Text;
        txtLname.Text = gvUsers.Rows[rowNo].Cells[4].Text;
        txtEmail.Text = gvUsers.Rows[rowNo].Cells[5].Text;
        txtPhoneNo.Text = gvUsers.Rows[rowNo].Cells[6].Text;
        Label role = (Label)gvUsers.Rows[rowNo].Cells[7].FindControl("lblrole");
        rblRole.SelectedValue = role.Text;
        Label status = (Label)gvUsers.Rows[rowNo].Cells[8].FindControl("lblStatus");
        rblStatus.SelectedValue = status.Text;
        if (gvUsers.Rows[rowNo].Cells[9].Text=="Male" || gvUsers.Rows[rowNo].Cells[9].Text=="Female")
        {
            rblGender.SelectedValue = gvUsers.Rows[rowNo].Cells[9].Text;
        }       
        txtAddress.Text = gvUsers.Rows[rowNo].Cells[10].Text;
    }


    public void addNewUser(bool picture)
    {

        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        switch (picture)
        {
            case true:
                {

                    string insertQuery = "INSERT INTO [user] (fname, lname,email,username,phone_no,picture,role,status,gender,address,password) VALUES (@fname, @lname, @email, @username, @phone_no, @picture, @role, @status, @gender,@address,@password)";
                    SqlCommand com = new SqlCommand(insertQuery, conn);
                    {
                        com.Parameters.AddWithValue("@user_id", lblUser_id.Text);
                        com.Parameters.AddWithValue("@username", txtUsername.Text);
                        com.Parameters.AddWithValue("@fname", txtFname.Text);
                        com.Parameters.AddWithValue("@lname", txtLname.Text);
                        com.Parameters.AddWithValue("@email", txtEmail.Text);
                        com.Parameters.AddWithValue("@phone_no", txtPhoneNo.Text);
                        com.Parameters.AddWithValue("@address", txtAddress.Text);
                        if (rblRole.SelectedValue == "Member")
                        {
                            com.Parameters.AddWithValue("@role", "0");
                        }
                        else
                        {
                            com.Parameters.AddWithValue("@role", "1");
                        }
                        if (rblStatus.SelectedValue == "Active")
                        {
                            com.Parameters.AddWithValue("@status", "1");
                        }
                        else
                        {
                            com.Parameters.AddWithValue("@status", "0");
                        }
                        com.Parameters.AddWithValue("@gender", rblGender.SelectedValue.ToString());
                        com.Parameters.AddWithValue("@picture", uploadFile_getFileName());
                        com.Parameters.AddWithValue("@password", encryptSha1(txtUsername.Text)); //sign username as password
                        com.ExecuteNonQuery();
                        SetMessage("User added successfully", "success"); // call the method to set message to the user 
                        UserBindGrid();


                        break;
                    }
                }
            case false:
                {

                    string insertQuery = "INSERT INTO [user] (fname, lname,email,username,phone_no,picture,role,status,gender,address,password) VALUES (@fname, @lname, @email, @username, @phone_no, @picture, @role, @status, @gender,@address,@password)";
                    SqlCommand com = new SqlCommand(insertQuery, conn);
                    {
                        com.Parameters.AddWithValue("@user_id", lblUser_id.Text);
                        com.Parameters.AddWithValue("@username", txtUsername.Text);
                        com.Parameters.AddWithValue("@fname", txtFname.Text);
                        com.Parameters.AddWithValue("@lname", txtLname.Text);
                        com.Parameters.AddWithValue("@email", txtEmail.Text);
                        com.Parameters.AddWithValue("@phone_no", txtPhoneNo.Text);
                        com.Parameters.AddWithValue("@address", txtAddress.Text);
                        if (rblRole.SelectedValue == "Member")
                        {
                            com.Parameters.AddWithValue("@role", "0");
                        }
                        else
                        {
                            com.Parameters.AddWithValue("@role", "1");
                        }
                        if (rblStatus.SelectedValue == "Active")
                        {
                            com.Parameters.AddWithValue("@status", "1");
                        }
                        else
                        {
                            com.Parameters.AddWithValue("@status", "0");
                        }
                        com.Parameters.AddWithValue("@gender", rblGender.SelectedValue.ToString());
                        com.Parameters.AddWithValue("@picture", "null.png"); //default img
                        com.Parameters.AddWithValue("@password", encryptSha1(txtUsername.Text)); //sign username as password
                        com.ExecuteNonQuery();
                        SetMessage("User added successfully", "success"); // call the method to set message to the user 
                        UserBindGrid();
                    }
                    break;
                }
        }



    }

    public void updateUser(bool picture)
    {

        SqlConnection conn = new SqlConnection(constr);
        conn.Open();
        switch (picture)
        {
            case true:
                {
                    string updateQuesry = "UPDATE [user] SET username=@username,fname=@fname,lname=@lname,email=@email,phone_no=@phone_no,address=@address,role=@role,status=@status,gender=@gender,picture=@picture WHERE user_id=@user_id";
                    SqlCommand com = new SqlCommand(updateQuesry, conn);
                    {
                        com.Parameters.AddWithValue("@user_id", lblUser_id.Text);
                        com.Parameters.AddWithValue("@username", txtUsername.Text);
                        com.Parameters.AddWithValue("@fname", txtFname.Text);
                        com.Parameters.AddWithValue("@lname", txtLname.Text);
                        com.Parameters.AddWithValue("@email", txtEmail.Text);
                        com.Parameters.AddWithValue("@phone_no", txtPhoneNo.Text);
                        com.Parameters.AddWithValue("@address", txtAddress.Text);
                        if (rblRole.SelectedValue == "Member")
                        {
                            com.Parameters.AddWithValue("@role", "0");
                        }
                        else
                        {
                            com.Parameters.AddWithValue("@role", "1");
                        }
                        if (rblStatus.SelectedValue == "Active")
                        {
                            com.Parameters.AddWithValue("@status", "1");
                        }
                        else
                        {
                            com.Parameters.AddWithValue("@status", "0");
                        }
                        com.Parameters.AddWithValue("@gender", rblGender.SelectedValue.ToString());
                        com.Parameters.AddWithValue("@picture", uploadFile_getFileName());
                        com.ExecuteNonQuery();
                        SetMessage("Update user successfully", "success"); // call the method to set message to the user 
                        UserBindGrid();


                        break;
                    }
                }
            case false:
                {
                    string updateQuesry = "UPDATE [user] SET username=@username,fname=@fname,lname=@lname,email=@email,phone_no=@phone_no,address=@address,role=@role,status=@status,gender=@gender WHERE user_id=@user_id";
                    SqlCommand com = new SqlCommand(updateQuesry, conn);
                    {
                        com.Parameters.AddWithValue("@user_id", lblUser_id.Text);
                        com.Parameters.AddWithValue("@username", txtUsername.Text);
                        com.Parameters.AddWithValue("@fname", txtFname.Text);
                        com.Parameters.AddWithValue("@lname", txtLname.Text);
                        com.Parameters.AddWithValue("@email", txtEmail.Text);
                        com.Parameters.AddWithValue("@phone_no", txtPhoneNo.Text);
                        com.Parameters.AddWithValue("@address", txtAddress.Text);
                        if (rblRole.SelectedValue == "Member")
                        {
                            com.Parameters.AddWithValue("@role", "0");
                        }
                        else
                        {
                            com.Parameters.AddWithValue("@role", "1");
                        }
                        if (rblStatus.SelectedValue == "Active")
                        {
                            com.Parameters.AddWithValue("@status", "1");
                        }
                        else
                        {
                            com.Parameters.AddWithValue("@status", "0");
                        }
                        com.Parameters.AddWithValue("@gender", rblGender.SelectedValue.ToString());
                        com.ExecuteNonQuery();
                        SetMessage("Update user successfully", "success"); // call the method to set message to the user 
                        UserBindGrid();
                    }
                    break;
                }
        }



    }
    public void deleteUser()
    {
        try
        {
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            string DeleteQuesry = "DELETE FROM [user] WHERE user_id =@user_id ";
            SqlCommand com = new SqlCommand(DeleteQuesry, conn);
            {
                com.Parameters.AddWithValue("@user_id", lblUser_id.Text);
                com.ExecuteNonQuery();
                SetMessage("Delete user successfully", "success"); // call the method to set message to the user 
                UserBindGrid();
            }
        }
        catch(SqlException ex)
        {
            SetMessage("Cannot be deleted", "danger"); 
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
    public void SetMessage(string message, string typeOfMessage)
    {
        string html = "<div style='width: fit-content;margin: auto;top: 130px;box-shadow: 10px 10px 10px 6px rgb(0 0 0 / 40%);  z-index: 10;' class='alert alert-type'><h6 style='text-align: center;'>message</h6></div>";
        string replaceHtml = html.Replace("type", typeOfMessage).Replace("message", message);
        messageStatus = true;
        lblMessage.Text = replaceHtml;

    }


    public string uploadFile_getFileName()
    {

        if (fuUserImg.HasFile)
        {
            try
            {
                if (fuUserImg.PostedFile.ContentType == "image/jpeg" || fuUserImg.PostedFile.ContentType == "image/png")

                {
                    //Saving image in the folder
                    string filename = Path.GetFileName(fuUserImg.FileName);
                    fuUserImg.SaveAs(Server.MapPath("~/assets/ProfilePics/") + filename);



                    return fuUserImg.PostedFile.FileName.ToString();

                }
                else
                {
                    SetMessage("only jpg and jpeg file is allowed", "danger"); // call the method to set message to the user 
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }
        }



        return "null.png"; // if there is error ,it will return the default image for users

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        if (rblSelectImg.SelectedIndex == 0)
        {
            updateUser(true); //true means update with picture.....using switch
        }
        else
        {
            updateUser(false); //false means update without picture.....using switch
        }

        divUserEdit.Visible = false;
        btnAddNewUser.Visible = true;

    }




    protected void btnAddNewUser_Click(object sender, EventArgs e)
    {
        // click on add new user
        divUserEdit.Visible = true;// show div for editing and adding,remove
        btnAddNewUser.Visible = false;
        btnUpdate.Visible = false;
        btnDelete.Visible = false;
        btnSaveNewUser.Visible = true;
        cleanFrom();

    }


    protected void btnSaveNewUser_Click(object sender, EventArgs e)
    {
        // save new user
        divUserEdit.Visible = false;
        btnAddNewUser.Visible = true;


        if (checkUserInfoDuplication() == true)
        {
            if (rblSelectImg.SelectedIndex == 0)
            {
                addNewUser(true); //true means update with picture.....using switch
            }
            else
            {
                addNewUser(false); //false means update without picture.....using switch
            }
        }
        else
        {
            SetMessage("duplication detected,check the data", "danger"); // call the method to set message to the user 

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        // user click on delete
        divUserEdit.Visible = false;
        btnAddNewUser.Visible = true;
        deleteUser();// call the mehod to delet the user

    }


    public string encryptSha1(string data)
    {
        string sha1 = FormsAuthentication.HashPasswordForStoringInConfigFile(data, "sha1");
        return sha1;
    }

    public bool checkUserInfoDuplication()
    {
        bool isValid = true;
        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(constr);
        
        string[] columnName = { "email", "phone_no", "username" };
        TextBox[] UserTextboxs = new TextBox[] { txtEmail, txtPhoneNo, txtUsername };
        string sqlQuery = "SELECT * from [user] WHERE columnName = @columnName";
        for (int index = 0; index < columnName.Length; index++)
        {
            conn.Open();
            string replceQuery = sqlQuery.Replace("columnName", columnName[index].ToString());

            SqlCommand cmd = new SqlCommand(replceQuery, conn);

            cmd.Parameters.AddWithValue("@" + columnName[index], UserTextboxs[index].Text);
            SqlDataReader dr = cmd.ExecuteReader();
            bool recordfound = dr.Read();
            conn.Close();
            if (recordfound) //found data 
            {
                isValid = false;
                break;
            }


        }
        return isValid;
    }

    public void search()
    {
        // 
        string SearchQuery = "select* from [user] where column like '%'+@search +'%' ".Replace("column", ddlSearchBy.SelectedValue);

        


        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(constr))
        {         
            using (SqlCommand cmd = new SqlCommand(SearchQuery))
            {
                cmd.Parameters.AddWithValue("@search", txtSearchUsers.Text);

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        gvUsers.DataSource = dt;
                        gvUsers.DataBind();
                    }
                }
            }
        }

    }

    public void cleanFrom()
    {
        lblUser_id.Text = txtAddress.Text = txtEmail.Text = txtFname.Text = txtLname.Text = txtPhoneNo.Text = txtUsername.Text = "";
        rblGender.SelectedIndex = rblRole.SelectedIndex = rblStatus.SelectedIndex = -1;
    }



    protected void btnSearchUser_Click(object sender, EventArgs e)
    {
        search();
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        divUserEdit.Visible = false;
        btnAddNewUser.Visible = true;
        btnSaveNewUser.Visible = false;
        cleanFrom();
    }

    private void checklogin() // check if the user is logged in , and if yes, check if the user is admin
    {
        if (Session["role"] != null)
        {
            if (Session["role"].ToString() != "True") // if another user access the page and he/she is not admin
            {
                Session["message"] = "you're not allowed to visit admin contorl panel page";
                Session["typeOfMessage"] = "danger";
                Response.Redirect("default.aspx");


            }
        }
        else
        {
            Session["message"] = "Login first to access this page";
            Session["typeOfMessage"] = "warning";

            Response.Redirect("login_register.aspx");
        }
    }

    protected void lbUser_Click(object sender, EventArgs e)
    {
        mlvAdminControlPanel.ActiveViewIndex = 0;
    }

    protected void lbTeam_Click(object sender, EventArgs e)
    {
        mlvAdminControlPanel.ActiveViewIndex = 1;
    }

    protected void lbEvents_Click(object sender, EventArgs e)
    {
        mlvAdminControlPanel.ActiveViewIndex = 2;
    }

    protected void lbFAQ_Click(object sender, EventArgs e)
    {
        mlvAdminControlPanel.ActiveViewIndex = 3;
    }
}