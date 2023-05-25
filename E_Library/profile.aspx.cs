using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Library
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string strconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["member_id"].ToString() == "" || Session["member_id"] == null)
            {
                String title = "Session Expired!!!";
                String body = "Please login again.";
                ShowModal(title, body);
                Response.Redirect("userlogin.aspx");
            }
            else
            {
                loadIssuedBooks();

                if (!Page.IsPostBack)
                {
                    loadMember();
                }
            }
        }

        protected void ShowModal(string title, string data)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Modal", "ShowModal('" + title + "', '" + data + "');", true);
        }

        //Update Button
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["member_id"].ToString() == "" || Session["member_id"] == null)
            {
                String title = "Session Expired!!!";
                String body = "Please login again.";
                ShowModal(title, body);
                Response.Redirect("userlogin.aspx");
            }
            else
            {
                updateMember();
            }
        }

        protected bool checkBookExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM book_master_tbl WHERE book_id='" + TextBox1.Text.Trim() + "'", con);
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

        protected void loadMember()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id='" + Session["member_id"].ToString() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox1.Text = dt.Rows[0]["full_name"].ToString();
                    TextBox2.Text = dt.Rows[0]["dob"].ToString();
                    TextBox3.Text = dt.Rows[0]["contact_no"].ToString().Trim();
                    TextBox4.Text = dt.Rows[0]["member_id"].ToString();
                    TextBox5.Text = dt.Rows[0]["password"].ToString();
                    DropDownList1.SelectedValue = dt.Rows[0]["state"].ToString().Trim();
                    TextBox8.Text = dt.Rows[0]["city"].ToString();
                    TextBox9.Text = dt.Rows[0]["pincode"].ToString().Trim();
                    Label1.Text = dt.Rows[0]["account_status"].ToString().Trim();

                    if (dt.Rows[0]["account_status"].ToString().Trim() == "Active")
                    {
                        Label1.Attributes.Add("class", "badge badge-pill badge-success");
                    }
                    else if (dt.Rows[0]["account_status"].ToString().Trim() == "Pending")
                    {
                        Label1.Attributes.Add("class", "badge badge-pill badge-warning");
                    }
                    else if (dt.Rows[0]["account_status"].ToString().Trim() == "Deactive")
                    {
                        Label1.Attributes.Add("class", "badge badge-pill badge-danger");
                    }
                    else
                    {
                        Label1.Attributes.Add("class", "badge badge-pill badge-info");
                    }
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

        protected void loadIssuedBooks()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM book_issue_tbl WHERE member_id='" + Session["member_id"].ToString() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource= dt;  
                GridView1.DataBind();
                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void updateMember()
        {
            string password;
            if (TextBox6.Text.Trim() == "")
            {
                password = TextBox5.Text.Trim();
            }
            else
            {
                password = TextBox6.Text.Trim();
            }
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlCommand cmd = new SqlCommand("UPDATE member_master_tbl SET full_name=@full_name, dob=@dob, contact_no=@contact_no, state=@state, city=@city, " +
                    "pincode=@pincode, password=@password, account_status=@account_status WHERE member_id='" + Session["member_id"].ToString().Trim() + "'", con);

                cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@account_status", "Pending");

                cmd.ExecuteNonQuery();
                con.Close();

                String title = "Profile Updated!!!";
                String body = "Profile updated successfully.";
                ShowModal(title, body);

                Session["full_name"] = TextBox1.Text;
                loadMember();
                loadIssuedBooks();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime duedate = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if (today > duedate) e.Row.BackColor = System.Drawing.Color.IndianRed;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}