using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Rent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            equipmentDiv.Visible = false;
            courtDiv.Visible = false;
            BindGridEquipment();
            BindGridCourts();
            checkUserLogin();
            BindGridFAQ();
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
    private void BindGridFAQ()//for FAQ GridView
    {
        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT  faq_question as Questions, faq_answer as Answers FROM [FAQ] WHERE faq_type=1"))
            {

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        gvEquipmentFAQ.DataSource = dt;
                        gvEquipmentFAQ.DataBind();
                    }
                }
            }
        }
    }
    private void BindGridEquipment()
    {
        //binding data
        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(constr))
        {
            //sql command to duplicate data into the grid view
            using (SqlCommand cmd = new SqlCommand("SELECT equipment_id AS 'Equipment ID', equipment_title AS 'Equipment Title', equipment_desc AS 'Equipment Description', cost_per_hour AS 'Cost Per Hour', delay_cost_per_hour AS 'Delay Cost Per Hour' FROM equipment"))
            {
                

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        gvEquipment.DataSource = dt;
                        gvEquipment.DataBind();
                    }
                }
            }
        }
    }
    private void BindGridCourts()
    {//binding data
        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
        using (SqlConnection conn2 = new SqlConnection(constr))
        {
            //sql command to duplicate data into the grid view
            using (SqlCommand cmd = new SqlCommand("SELECT court_id AS 'Court ID', location AS Location, cost_per_hour AS 'Cost Per Hour' FROM court"))
            {

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = conn2;
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        gvCourt.DataSource = dt;
                        gvCourt.DataBind();
                    }
                }
            }
        }
    }
    protected void btnEquipment_Click(object sender, EventArgs e)
    {
        equipmentDiv.Visible = true;
        courtDiv.Visible = false;
    }

    protected void btnCourt_Click(object sender, EventArgs e)
    {
        courtDiv.Visible = true;
        equipmentDiv.Visible = false;
    }

    protected void lbtCheckEquipment_Click(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex; //to read row index of grid view
        //duplicate data to full event information table
        lblEquipmentID.Text = gvEquipment.Rows[rowIndex].Cells[1].Text;
        lblEquipmentTitle.Text = gvEquipment.Rows[rowIndex].Cells[2].Text;
        lblEquipmentDesc.Text = gvEquipment.Rows[rowIndex].Cells[3].Text;
        lblEquipmentCost.Text = "RM" + gvEquipment.Rows[rowIndex].Cells[4].Text;
        lblEquipmentDelayCost.Text = "RM" + gvEquipment.Rows[rowIndex].Cells[5].Text;

        lblchooseDateEq.Visible = true;
        tbChooseDate.Visible = true;
        btnEquipShowAvailableTime.Visible = true;
    }

    protected void lbtCheckCourt_Click(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(sender as Control).NamingContainer).RowIndex; //to read row index of grid view
                                                                                    //duplicate data to full event information table
        lblCourtID.Text = gvCourt.Rows[rowIndex].Cells[1].Text;
        lblCourtLocation.Text = gvCourt.Rows[rowIndex].Cells[2].Text;
        lblCost.Text = "RM" + gvCourt.Rows[rowIndex].Cells[3].Text;

        
        lblcourtChooseDate.Visible = true;
        tbCourtChooseDate.Visible = true;
        btnShowAvailableCourtTime.Visible = true;

    }

    protected void btnEquipShowAvailableTime_Click(object sender, EventArgs e)
    {
        lblTimeSlotEQ.Visible = true;
        ddlEquipmentTime.Visible = true;
        btnEquipmentBook.Visible = true;

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString);
        //String userID = Session["user_id"].ToString();
        List<string> tempList = new List<string>();
        conn.Open();
        //get event id by comparing event title in table
        SqlCommand cmd = new SqlCommand("SELECT [time] FROM rent INNER JOIN equipment_rent ON rent.rent_id=equipment_rent.rent_id WHERE equipment_rent.equipment_id=@equipment_id AND rent.[date]=@date", conn);
        cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(tbChooseDate.Text));
        cmd.Parameters.AddWithValue("@equipment_id", lblEquipmentID.Text);
        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            if (!dr.IsDBNull(0))
            {
                tempList.Add(dr[0].ToString());
            }
        }
        dr.Close();
        string[] Time = tempList.ToArray();
        string[] arrTime = { "12:00:00", "14:00:00", "16:00:00", "18:00:00", "20:00:00" };
        conn.Close();
        var c = arrTime.Except(Time).ToArray();//Removing duplicates from the available slots array
        //Label2.Text = c[3];

        List<string> availableTimeList = new List<string>(c);
        ddlEquipmentTime.DataSource = availableTimeList;
        ddlEquipmentTime.DataBind();
    }

    protected void btnEquipmentBook_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString);
        try{
            conn.Open();
            string insertQuery = "INSERT INTO rent (user_id, date, time, period, type) VALUES (@user_id, @date, @time, 2, 0)";//To insert data into the rent table
            SqlCommand com = new SqlCommand(insertQuery, conn);

            com.Parameters.AddWithValue("@user_id", user_id);
            com.Parameters.AddWithValue("@date", tbChooseDate.Text);
            com.Parameters.AddWithValue("@time", ddlEquipmentTime.Text);
            com.ExecuteNonQuery();
            conn.Close();
            
        }
        catch (Exception ex)
        {
            throw new Exception("Error " + ex.Message);
        }

        try
        {
            conn.Open();
            string insertQuery2 = "INSERT INTO equipment_rent (equipment_id, rent_id) VALUES (@equipment_id, (SELECT MAX(rent.rent_id) FROM rent))";//To insert data into equipment_rent table
            SqlCommand com2 = new SqlCommand(insertQuery2, conn);

            com2.Parameters.AddWithValue("@equipment_id", lblEquipmentID.Text);

            com2.ExecuteNonQuery();

            conn.Close();
        }
        catch (Exception ex)
        {
            throw new Exception("Error " + ex.Message);
        }
        Session["message"] = "Rent Placed Succesfully!";
        Session["typeOfMessage"] = "success";
        Response.Redirect(Request.RawUrl);
    }


    protected void btnShowAvailableCourtTime_Click(object sender, EventArgs e) 
        {
        lblCourtTimes.Visible = true;
        ddlCourtTimes.Visible = true;
        btnBookCourt.Visible = true;

            SqlConnection conn3 = new SqlConnection(ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString);
            //String userID = Session["user_id"].ToString();
            List<string> tempList2 = new List<string>();
            conn3.Open();
            //get event id by comparing event title in table
            SqlCommand cmd = new SqlCommand("SELECT [time] FROM rent INNER JOIN court_rent ON rent.rent_id=court_rent.rent_id WHERE court_rent.court_id=@court_id AND rent.[date]=@date", conn3);
            cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(tbCourtChooseDate.Text));
            cmd.Parameters.AddWithValue("@court_id", lblCourtID.Text);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                {
                    tempList2.Add(dr[0].ToString());
                }
            }
            dr.Close();
            string[] Time2 = tempList2.ToArray();
            string[] arrTime2 = { "12:00:00", "14:00:00", "16:00:00", "18:00:00", "20:00:00" };
            conn3.Close();
            var c = arrTime2.Except(Time2).ToArray();//Removing duplicates from the available slots array
                                                     //Label2.Text = c[3];

            List<string> availableTimeList2 = new List<string>(c);
            ddlCourtTimes.DataSource = availableTimeList2;
            ddlCourtTimes.DataBind();
        }

protected void btnBookCourt_Click(object sender, EventArgs e)
    {
        string user_id = Session["user_id"].ToString();
        SqlConnection conn4 = new SqlConnection(ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString);
        try
        {
            conn4.Open();
            string insertQuery3 = "INSERT INTO rent (user_id, date, time, period, type) VALUES (@user_id, @date, @time, 2, 1)";//To insert data into the rent table
            SqlCommand com = new SqlCommand(insertQuery3, conn4);

            com.Parameters.AddWithValue("@user_id", user_id);
            com.Parameters.AddWithValue("@date", tbCourtChooseDate.Text);
            com.Parameters.AddWithValue("@time", ddlCourtTimes.Text);
            com.ExecuteNonQuery();
            conn4.Close();

        }
        catch (Exception ex)
        {
            throw new Exception("Error " + ex.Message);
        }

        try
        {
            conn4.Open();
            string insertQuery2 = "INSERT INTO court_rent (court_id, rent_id) VALUES (@court_id, (SELECT MAX(rent.rent_id) FROM rent))";//To insert data into equipment_rent table
            SqlCommand com2 = new SqlCommand(insertQuery2, conn4);

            com2.Parameters.AddWithValue("@court_id", lblCourtID.Text);

            com2.ExecuteNonQuery();

            conn4.Close();
        }
        catch (Exception ex)
        {
            throw new Exception("Error " + ex.Message);
        }
        Session["message"] = "Rent Placed Succesfully!";
        Session["typeOfMessage"] = "success";
        Response.Redirect(Request.RawUrl);
    }

    protected void gvEquipmentFAQ_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].BackColor = System.Drawing.Color.AliceBlue;
        }
    }

    protected void btnSearchEquipment_Click(object sender, EventArgs e)
    {
        //binding data
        string constr = ConfigurationManager.ConnectionStrings["BasketballConStr"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(constr))
        {
            //sql command to duplicate data into the grid view
            using (SqlCommand cmd = new SqlCommand("SELECT equipment_id AS 'Equipment ID', equipment_title AS 'Equipment Title', equipment_desc AS 'Equipment Description', cost_per_hour AS 'Cost Per Hour', delay_cost_per_hour AS 'Delay Cost Per Hour' FROM equipment WHERE equipment_title LIKE '%' + @equipment_title + '%' "))
            {
                cmd.Parameters.AddWithValue("@equipment_title", tbEquipmentSearch.Text);

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;

                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        gvEquipment.DataSource = dt;
                        gvEquipment.DataBind();
                    }
                }
            }
        }
    }
}