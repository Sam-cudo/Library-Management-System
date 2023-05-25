using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Library
{
    public partial class userlogin : System.Web.UI.Page
    {
        string strconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ShowErrorModal()
        {
            string title = "Invalid Credentials!!!";
            string data = "Please try again.";
            ClientScript.RegisterStartupScript(this.GetType(), "Modal", "ShowModal('" + title + "', '" + data + "');", true);
        }
        protected void LoginBt_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("Select * FROM member_master_tbl WHERE member_id='" + TextBox1.Text.Trim() + "' AND password='" + TextBox2.Text.Trim() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while(dr.Read())
                    {
                        Response.Write("<script>alert('" + dr.GetValue(3).ToString() + "');</script>");
                        Session["member_id"] = dr.GetValue(0).ToString();
                        Session["fullname"] = dr.GetValue(3).ToString();
                        Session["role"] = "member";
                        Session["status"] = dr.GetValue(2).ToString();
                    }
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    ShowErrorModal();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}