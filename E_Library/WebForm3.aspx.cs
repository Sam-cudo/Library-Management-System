using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace E_Library
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        string strconn = ConfigurationManager.ConnectionStrings["E-LibraryDBConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected string GetData()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM [book_issue_tbl]", con);
                SqlDataReader sqlRdr = cmd.ExecuteReader();
                string data = "";

                DataTable dt = new DataTable();
                

                if (sqlRdr.HasRows)
                {
                    while (sqlRdr.Read())
                    {
                        string member_id = sqlRdr.GetString(0);
                        string member_name = sqlRdr.GetString(1);
                        string book_id = sqlRdr.GetString(2);
                        string book_name = sqlRdr.GetString(3);
                        string issue_date = sqlRdr.GetString(4);
                        string due_date = sqlRdr.GetString(5);
                        data += "<tr><td>" + member_id + "</td><td>" + member_name + "</td><td>" + book_id + "</td><td>" + book_name + "</td><td>" + issue_date + "</td><td>" + due_date + "</td></tr>";
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return "0";
            }
        }
    }
}