using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        this.BindFAQGrid();
        this.BindTeamGrid();
        this.BindRentGrid();
        this.BindEventsGrid();

        btnHideDash.Visible = false;
        lblUserName.Visible = false;
        ViewDashBoardDiv.Visible = false;
        DashBoardDiv.Visible = false;
        myWelcomeUser();


    }
    protected void myWelcomeUser()//Welcomes the user
    {
        if (Session["user_id"] != null) // if the user is already logged in
        {
            lblUserName.Visible = true;//label that displays the welcome message if the user is logged in
            try
            {
                string user_id = Session["user_id"].ToString();//Grabs the user id that logged in to get his/her info
                if (Session["user_id"] != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
                    SqlConnection conn = new SqlConnection(constr);
                    conn.Open();//opening a connection
                    SqlCommand cmd = new SqlCommand("SELECT * from [user] WHERE user_id=@user_id", conn);//query to get the information of the logged user
                    cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                    SqlDataReader dReader = cmd.ExecuteReader();//executing the query
                    bool userfound = dReader.Read();
                    if (userfound)
                    {
                        string fName = dReader["fname"].ToString();//saving the results in a variable
                        string gender = dReader["gender"].ToString();
                        if (gender == "Male")//to display the welcome message based on the gender of the logged in user
                        {
                            lblUserName.Text = "Welcome Mr. " + fName + "!";
                        }
                        else
                        {
                            lblUserName.Text = "Welcome Ms. " + fName + "!";
                        }
                        ViewDashBoardDiv.Visible = true;//shows the div holding the buttons that control the dashboard
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }



        }
    }
    private void BindFAQGrid()//for FAQ GridView
    {
        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT  faq_question as Questions, faq_answer as Answers FROM [FAQ] WHERE faq_type=0"))
            {

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())//filling the grid
                    {
                        sda.Fill(dt);
                        gvFAQ.DataSource = dt;
                        gvFAQ.DataBind();
                    }
                }
            }
        }
    }
    private void BindTeamGrid()//Show the user's Team
    {
        if (Session["user_id"] != null)
        {
            string user_id = Session["user_id"].ToString();
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT CONCAT(fname, ' ', lname) as Name, phone_no AS 'Phone Number' FROM [user] inner join Team_participant ON [user].user_id = Team_participant.user_id WHERE Team_participant.team_id=(SELECT team_id from Team_participant where user_id=@user_id)"))//Query that will show the names and phone numbers of the members that are in the same team as the logged in user
                {
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            myTeamgv.DataSource = dt;
                            myTeamgv.DataBind();
                        }
                    }
                }
            }

            lblUserName.Visible = true;
            try
            {
                if (Session["user_id"] != null)
                {
                    SqlConnection conn = new SqlConnection(constr);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * from Team_participant WHERE user_id=@user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", user_id);/*Telling the query where the @user_id is coming from (telling the query that @user_id is coming from the session)*/
                    SqlDataReader dReader = cmd.ExecuteReader();
                    bool userfound = dReader.Read();
                    if (userfound)//displays the practice time and day based on the team that the user is enrolled in
                    {
                        string teamID = dReader["team_id"].ToString();
                        if (teamID == "1")
                        {
                            lblPracticeDay.Text = "Monday";
                            lblPracticeTime.Text = "20:00:00";
                        }
                        else if (teamID == "2")
                        {
                            lblPracticeDay.Text = "Tuesday";
                            lblPracticeTime.Text = "20:00:00";
                        }
                        else
                        {
                            lblPracticeDay.Text = "Friday";
                            lblPracticeTime.Text = "20:00:00";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }
        }


    }
    private void BindRentGrid()//Shows the users their rents
    {
        if (Session["user_id"] != null)
        {
            string user_id = Session["user_id"].ToString();
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Convert(varchar, [date], 105) AS 'Date', [time] AS 'Time', CASE WHEN [type]=1 THEN 'Court' ELSE 'Equipment' END AS 'Type' from rent WHERE user_id=@user_id AND [date] > GETDATE() ORDER BY [time] ASC"))//Displays to the user their rent history that hasn't expired and shows if the rents were for courts or equipments
                {
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            myRentgv.DataSource = dt;
                            myRentgv.DataBind();
                        }
                    }
                }
            }

        }
    }
    private void BindEventsGrid()//shows the user the events they enrolled in
    {
        if (Session["user_id"] != null)
        {
            string user_id = Session["user_id"].ToString();
            string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT event_title AS 'Event Title', event_date AS 'Date & Time' from [event] inner join event_record on [event].event_id=event_record.event_id WHERE user_id=@user_id AND event_date > GETDATE() ORDER BY event_date ASC"))//shows the user the events they enrolled in that hasnt yet expired
                {
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            myEventsgv.DataSource = dt;
                            myEventsgv.DataBind();
                        }
                    }
                }
            }

        }
    }


    protected void btnDashBoard_Click(object sender, EventArgs e)//button that shows the dash board
    {
        DashBoardDiv.Visible = true;
        btnHideDash.Visible = true;
    }

    protected void btnHideDash_Click(object sender, EventArgs e)//button that hides the dash board
    {
        DashBoardDiv.Visible = false;
        btnHideDash.Visible = false;
    }

    protected void myEventsgv_PageIndexChanging(object sender, GridViewPageEventArgs e)//Controls the grid when the user changes the page
    {
        DashBoardDiv.Visible = true;//fixes an error where the dashboard would close
        btnHideDash.Visible = true;
        myEventsgv.PageIndex = e.NewPageIndex;
        myEventsgv.DataBind();

    }

    protected void myTeamgv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DashBoardDiv.Visible = true;
        btnHideDash.Visible = true;
        myTeamgv.PageIndex = e.NewPageIndex;
        myTeamgv.DataBind();
    }


    protected void myPracticegv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DashBoardDiv.Visible = true;
        btnHideDash.Visible = true;
        myRentgv.PageIndex = e.NewPageIndex;
        myRentgv.DataBind();
    }

    protected void gvFAQ_RowDataBound(object sender, GridViewRowEventArgs e)//Styling for the FAQ grid
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].BackColor = System.Drawing.Color.AliceBlue;
        }
    }
}