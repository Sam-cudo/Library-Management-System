using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Library
{
    public partial class adminmembermanagement : System.Web.UI.Page
    {
        string strconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void ShowModal(string title, string data)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Modal", "ShowModal('" + title + "', '" + data + "');", true);
        }

        // GO Button
        protected void Button2_Click(object sender, EventArgs e)
        {
            selectMemberByID();
        }

        // Active Status Button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (checkMemberExist())
            {
                setMemberStatus("Active");
                selectMemberByID();
            }
            else
            {
                String title = "Invalid ID!!!";
                String body = "Member with this ID does not Exists. Please use valid ID.";
                ShowModal(title, body);
            }
        }

        // Pending Status Button
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (checkMemberExist())
            {
                setMemberStatus("Pending");
                selectMemberByID();
            }
            else
            {
                String title = "Invalid ID!!!";
                String body = "Member with this ID does not Exists. Please use valid ID.";
                ShowModal(title, body);
            }
        }

        // Deactive Status Button
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (checkMemberExist())
            {
                setMemberStatus("Deactive");
                selectMemberByID();
            }
            else
            {
                String title = "Invalid ID!!!";
                String body = "Member with this ID does not Exists. Please use valid ID.";
                ShowModal(title, body);
            }
        }

        // Delete Button
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkMemberExist())
            {
                deleteMember();
            }
            else
            {
                String title = "Invalid ID!!!";
                String body = "Member with this ID does not Exists. Please use valid ID.";
                ShowModal(title, body);
            }
        }

        protected bool checkMemberExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id='" + TextBox1.Text.Trim() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1) { return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        protected void selectMemberByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id='" + TextBox1.Text.Trim() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1) { 
                    TextBox2.Text = dt.Rows[0][3].ToString();
                    TextBox10.Text = dt.Rows[0][2].ToString();
                    TextBox11.Text = dt.Rows[0][4].ToString();
                    TextBox3.Text = dt.Rows[0][5].ToString();
                    TextBox4.Text = dt.Rows[0][0].ToString();
                    TextBox5.Text = dt.Rows[0][6].ToString();
                    TextBox8.Text = dt.Rows[0][7].ToString();
                    TextBox9.Text = dt.Rows[0][8].ToString();
                }
                else
                {
                    String title = "Invalid ID!!!";
                    String body = "Member with this ID does not Exists. Please use valid ID.";
                    ShowModal(title, body);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void setMemberStatus(String status)
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE member_master_tbl SET account_status = @status WHERE member_id = @member_id", con);

                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                String title = "Status Changed!!!";
                String body = "Status of the member changed successfully.";
                ShowModal(title, body);
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void deleteMember()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE FROM member_master_tbl WHERE member_id = @member_id", con);

                cmd.Parameters.AddWithValue("@member_id", TextBox1.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                String title = "Member Deleted!!!";
                String body = "Member was Successfully deleted.";
                ShowModal(title, body);
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
        }
    }
}