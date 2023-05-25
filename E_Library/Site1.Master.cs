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
    public partial class Site1 : System.Web.UI.MasterPage
    {
        string strconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["role"].Equals(""))
                {
                    LinkButton1.Visible = true;
                    LinkButton2.Visible = true;
                    LinkButton6.Visible = true;

                    LinkButton3.Visible = false;
                    LinkButton5.Visible = false;
                }
                else if (Session["role"].Equals("member"))
                {
                    LinkButton1.Visible = false;
                    LinkButton2.Visible = false;
                    LinkButton6.Visible = true;

                    LinkButton3.Visible = true;
                    LinkButton5.Visible = true;
                    LinkButton5.Text = "Hello " + Session["fullname"].ToString();
                }
                else if (Session["role"].Equals("admin"))
                {
                    LinkButton1.Visible = false;
                    LinkButton2.Visible = false;
                    LinkButton6.Visible = false;

                    LinkButton3.Visible = true;
                    LinkButton5.Visible = true;
                    LinkButton5.Text = "Hello Admin";

                }
            }
            catch { }
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminlogin.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("userlogin.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("register.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewbooks.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session.Clear();
            LinkButton1.Visible = true;
            LinkButton2.Visible = true;

            LinkButton3.Visible = false;
            LinkButton5.Visible = false;

            Response.Redirect("Home.aspx");
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            if(LinkButton5.Text == "Hello Admin")                
                Response.Redirect("admindashboard.aspx");            
            else
                Response.Redirect("profile.aspx");
        }
    }
}