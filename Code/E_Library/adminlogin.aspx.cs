using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace E_Library
{
    public partial class adminlogin : System.Web.UI.Page
    {
        string strconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ShowErrorModal(object sender, EventArgs e)
        {
            string title = "Invalid Credentials!!!";
            string data = "Please try again.";
            ClientScript.RegisterStartupScript(this.GetType(), "Modal", "ShowModal('" + title + "', '" + data + "');", true);
        }

        protected void adminlogBt_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("Select * FROM admin_login_tbl WHERE username='" + TextBox1.Text.Trim() + "' AND password='" + TextBox2.Text.Trim() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Response.Write("<script>alert('" + dr.GetValue(2).ToString() + "');</script>");
                        Session["member_id"] = dr.GetValue(0).ToString();
                        Session["fullname"] = dr.GetValue(2).ToString();
                        Session["role"] = "admin";
                    }
                    Response.Redirect("admindashboard.aspx");
                }
                else
                {
                    ShowErrorModal(sender, e);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}