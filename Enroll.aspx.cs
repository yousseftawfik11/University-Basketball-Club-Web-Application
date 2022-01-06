using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Enroll : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            BindGridEvents();
            BindGridPractice();
            checkUserLogin();
            teams.Visible = false;
            eventsDiv.Visible = false;
        }
        
    }
    public void checkUserLogin()
    {
        if (Session["user_id"] == null) // if the user already loged in
        {
            Session["message"] = "Please log in first to use this function!";
            Session["typeOfMessage"] = "warning";
            // kick the user from the login and register page
            Response.Redirect("login_register.aspx");

        }
    }
    public void clearEvent()
    {
        lblEventTitle.Text = lblEventDate.Text = lblInstitution.Text = lblInstructorDesc.Text = lblInstructorName.Text = lblInstructorEmail.Text= "";

    }
    public void clearTeam()
    {
        lblPday.Text = lblPTime.Text = lblTeam.Text = "";
    }
    private void BindGridEvents()
    {//binding data
        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(constr))
        {
            //sql command to duplicate data into the grid view
            using (SqlCommand cmd = new SqlCommand("SELECT  event.event_date AS Date, event.event_title AS Title, instructor.instructor_name AS 'Instructor Name', " +
                "instructor.instructor_institution, instructor.instructor_description,instructor.instructor_email " +
                "FROM event INNER JOIN instructor ON event.instructor_id=instructor.instructor_id " +
                "WHERE event_date > GETDATE()" +
                " ORDER BY event.event_date"))
            {

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        gvEnroll.DataSource = dt;
                        gvEnroll.DataBind();
                    }
                }
            }
        }
    }

    private void BindGridPractice()//bind for practice grid view
    {//binding data
        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(constr))
        {
            //sql command to duplicate data into the grid view
            using (SqlCommand cmd = new SqlCommand("SELECT practice_title AS 'Group', practice_day AS Day, practice_time AS Time FROM practice"))
            {

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        gvPractice.DataSource = dt;
                        gvPractice.DataBind();
                    }
                }
            }
        }
    }
    protected void gvEnroll_RowCreated(object sender, GridViewRowEventArgs e)
    {

        //  hide a cell in the column to hide column 1
        e.Row.Cells[4].Visible = false;
        e.Row.Cells[5].Visible = false;
        e.Row.Cells[6].Visible = false;
    }



    protected void btnGetEvent_Click(object sender, EventArgs e)
    {

        int rowind = ((GridViewRow)(sender as Control).NamingContainer).RowIndex; //to read row index of grid view
        //duplicate data to full event information table
        lblEventDate.Text = gvEnroll.Rows[rowind].Cells[1].Text;
        lblEventTitle.Text = gvEnroll.Rows[rowind].Cells[2].Text;
        lblInstructorName.Text = gvEnroll.Rows[rowind].Cells[3].Text;
        lblInstitution.Text = gvEnroll.Rows[rowind].Cells[4].Text;
        lblInstructorEmail.Text = gvEnroll.Rows[rowind].Cells[6].Text;
        lblInstructorDesc.Text = gvEnroll.Rows[rowind].Cells[5].Text;
        btnEnrollEvent.Visible = true;
    }
    protected void btnGetPractice_Click(object sender, EventArgs e)
    {
        int rowind = ((GridViewRow)(sender as Control).NamingContainer).RowIndex; //to read row index of grid view
        //duplicate data to full event information table
        lblTeam.Text = gvPractice.Rows[rowind].Cells[1].Text;
        lblPday.Text = gvPractice.Rows[rowind].Cells[2].Text;
        lblPTime.Text = gvPractice.Rows[rowind].Cells[3].Text;
        btnPracticeEnroll.Visible = true;
    }







    protected void btnEnrollEvent_Click(object sender, EventArgs e)
    {



        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString);
        String userID = Session["user_id"].ToString();
        string event_id = null;
        conn.Open();
        //get event id by comparing event title in table
        SqlCommand cmd = new SqlCommand(" SELECT event_id FROM event WHERE event_title = @event_title", conn);
        cmd.Parameters.AddWithValue("@event_title", lblEventTitle.Text);
        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            event_id = dr["event_id"].ToString();
        }
        conn.Close();

        conn.Open();
        SqlCommand cmmd = new SqlCommand("SELECT event_id, user_id FROM [event_record] WHERE user_id = @user_id AND event_id=@event_id ", conn);
        cmmd.Parameters.AddWithValue("@user_id", userID);
        cmmd.Parameters.AddWithValue("@event_id", event_id);
        SqlDataReader drr = cmmd.ExecuteReader();
        bool recordFound = drr.Read();
        conn.Close();
        if (recordFound)
        {
            Session["message"] = "You are already enrolled to this event!";
            Session["typeOfMessage"] = "danger";
            Response.Redirect(Request.RawUrl);
        }
        else
        {
            try
            {
                if (Session["user_id"] != null)
                {

                    conn.Open();
                    string insertQuery = "INSERT INTO event_record(event_id, user_id) VALUES (@event_id, @user_id)";
                    SqlCommand comm = new SqlCommand(insertQuery, conn);

                    comm.Parameters.AddWithValue("@event_id", event_id);
                    comm.Parameters.AddWithValue("@user_id", userID);
                    comm.ExecuteNonQuery();

                    Session["message"] = "Enrolled Successfully!";
                    Session["typeOfMessage"] = "success";
                    Response.Redirect(Request.RawUrl);
                    conn.Close();
                    clearEvent();


                }
            }
            catch (SqlException ex)
            {
                Session["message"] = "Error";
                Session["typeOfMessage"] = "danger";
                Response.Redirect(Request.RawUrl);
                //conn.Close();
            }
        }
        //conn.Close();

    }

    protected void btnPracticeEnroll_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString);
        String userID = Session["user_id"].ToString();
        string team_id = null;

        if (lblTeam.Text == "Beginners101")
        {
            team_id = "1";
        }
        else if (lblTeam.Text == "Advanced202")
        {
            team_id = "2";
        }
        else if (lblTeam.Text == "Pro303")
        {
            team_id = "3";
        }


        try
        {
            if (Session["user_id"] != null)
            {
                if(checkTeamEnrollDuplication(Session["user_id"].ToString()) == false){
                    conn.Open();
                    string insertQuery = "INSERT INTO Team_participant (team_id, user_id) VALUES (@team_id, @user_id)";
                    SqlCommand com = new SqlCommand(insertQuery, conn);

                    com.Parameters.AddWithValue("@team_id", team_id);
                    com.Parameters.AddWithValue("@user_id", userID);
                    com.ExecuteNonQuery();
                    conn.Close();

                    

                    Session["message"] = "Enrolled Successfully!";
                    Session["typeOfMessage"] = "success";
                    Response.Redirect(Request.RawUrl);

                    clearTeam();
                }
                else
                {
                    Session["message"] = "You are already enrolled to a team!";
                    Session["typeOfMessage"] = "danger";
                    Response.Redirect(Request.RawUrl);
                }

                


            }
        }
        catch (SqlException ex)
        {

            Session["message"] = "There is an Error"+ex.ToString();
            Session["typeOfMessage"] = "danger";
            Response.Redirect(Request.RawUrl);
        }



    }


    public bool checkTeamEnrollDuplication(string user_ID)
    {
        
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from [Team_participant] WHERE user_id = @user_id", conn);

            cmd.Parameters.AddWithValue("@user_id", user_ID);
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

        }
        return false;

    }

    protected void btnEventShow_Click(object sender, EventArgs e)
    {
        eventsDiv.Visible = true;
        teams.Visible = false;
    }

    protected void btnTeamsShow_Click(object sender, EventArgs e)
    {
        eventsDiv.Visible = false;
        teams.Visible = true;
    }
}

