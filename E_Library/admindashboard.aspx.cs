using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace E_Library
{
    public partial class admindashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            count_issued_books();
            count_available_books();
            count_members();
        }

        protected void count_issued_books()
        {
            var count = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["E-LibraryDBConnectionString"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [book_issue_tbl]", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                count++;
            }
            con.Close();
            Label1.Text = count.ToString();
        }

        protected void count_available_books()
        {
            var count = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["E-LibraryDBConnectionString"].ConnectionString);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [book_master_tbl]", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    count += Convert.ToInt16(dr["current_stock"]);
                }
            }
            con.Close();
            Label2.Text = count.ToString();
        }

        protected void count_members()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["E-LibraryDBConnectionString"].ConnectionString);
            con.Open();

            //Total Members
            var count = 0;
            SqlCommand cmd = new SqlCommand("SELECT * FROM [member_master_tbl]", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                count++;
            }
            dr.Close();
            Label3.Text = count.ToString();

            //Active Members
            var active_count = 0;
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM [member_master_tbl] WHERE account_status = 'Active'", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                active_count++;
            }
            dr1.Close();

            //Pending Members
            var pending_count = 0;
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM [member_master_tbl] WHERE account_status = 'Pending'", con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                pending_count++;
            }
            dr2.Close();

            //Deactive Members
            var deactive_count = 0;
            SqlCommand cmd3 = new SqlCommand("SELECT * FROM [member_master_tbl] WHERE account_status = 'Deactive'", con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                deactive_count++;
            }
            dr3.Close();

            con.Close();
            var x = new string[3];
            x[0] = "Active"; x[1] = "Pending"; x[2] = "Deactive";
            var y = new int[3];
            y[0] = active_count; y[1] = pending_count; y[2] = deactive_count;
            Color[] PieColors = { Color.LightGreen, Color.LightYellow, Color.PaleVioletRed };
            Chart1.Series[0].Label = "#VAL";
            Chart1.Series[0].LegendText = "#AXISLABEL";
            Chart1.Series[0].ChartType = SeriesChartType.Pie;
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Series[0].Points[0].Color = PieColors[0];
            Chart1.Series[0].Points[1].Color = PieColors[1];
            Chart1.Series[0].Points[2].Color = PieColors[2];
        }
        
    }
}
