using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Library
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string strconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterBt.Enabled = false;
            RegisterBt.CssClass = "btn btn-success btn-block";
            string pass = TextBox5.Text;
            TextBox5.Attributes.Add("value", pass);
            string cnfpass = TextBox6.Text;
            TextBox6.Attributes.Add("value", cnfpass);
        }

        protected void ShowModal(string title, string data)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Modal", "ShowModal('" + title + "', '" + data + "');", true);
        }

        protected void RegisterBt_Click(object sender, EventArgs e)
        {
            if (checkMemberExist())
            {
                String title = "Invalid Email!!!";
                String body = "Member already Exist with this Email. Try other Email.";
                ShowModal(title, body);
                CheckBox1.Checked = false;
            }
            else registerNewMember();
        }

        protected void CancelBt_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in register.Controls)

            {
                TextBox txt;
                if (ctrl is TextBox)
                {
                    txt = ctrl as TextBox;
                    if (txt != null)
                    {
                        txt.Text = String.Empty;

                    }
                }
            }
            DropDownList1.SelectedValue= null;
            CheckBox1.Checked= false;
            TextBox5.Attributes.Remove("value");
            TextBox6.Attributes.Remove("value");
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked) RegisterBt.Enabled = true;
            else RegisterBt.Enabled = false;
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

                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id='"+ TextBox4.Text.Trim() +"'", con);
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

        protected void registerNewMember()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO member_master_tbl (member_id, password, account_status, full_name, dob, contact_no, state, city, pincode) " +
                    "VALUES(@member_id, @password, @account_status, @full_name, @dob, @contact_no, @state, @city, @pincode)", con);

                cmd.Parameters.AddWithValue("@member_id", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@password", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@account_status", "Pending");
                cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", TextBox9.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                String title = "Sign Up Successfull!!!";
                String body = "You will be redirected to Login Page";
                ShowModal(title, body);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS",  "setTimeout(function() { window.location.replace('userlogin.aspx') }, 5000);", true);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}