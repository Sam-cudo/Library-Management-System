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
    public partial class adminauthormanagement : System.Web.UI.Page
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

        // Go Button
        protected void Button1_Click(object sender, EventArgs e)
        {
            selectAuthorByID();
        }

        // Add Button
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkAuthorExist())
            {
                String title = "Addition Failed!!!";
                String body = "Author with this ID already Exists. Please try with diffrent ID";
                ShowModal(title, body);
            }
            else addNewAuthor();
        }

        // Update Button
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkAuthorExist())
            {
                updateAuthor();
            }
            else
            {
                String title = "Updation Failed!!!";
                String body = "Author with this ID does not Exists. Please use valid ID";
                ShowModal(title, body);
            }
        }

        // Delete Button
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkAuthorExist())
            {
                deleteAuthor();
            }
            else
            {
                String title = "Deletion Failed!!!";
                String body = "Author with this ID does not Exists. Please use valid ID";
                ShowModal(title, body);
            }
        }

        protected bool checkAuthorExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM author_master_tbl WHERE author_id='" + TextBox1.Text.Trim() + "'", con);
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

        protected void selectAuthorByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM author_master_tbl WHERE author_id='" + TextBox1.Text.Trim() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1) { TextBox2.Text = dt.Rows[0][1].ToString(); }
                else
                {
                    String title = "Invalid ID!!!";
                    String body = "Author with this ID does not Exists. Please use valid ID";
                    ShowModal(title, body);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void addNewAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl (author_id, author_name) " + "VALUES(@author_id, @author_name)", con);

                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                String title = "Additon Successful!!!";
                String body = "Author has been successfully added in the system.";
                ShowModal(title, body);
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void updateAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE author_master_tbl SET author_name = @author_name WHERE author_id = @author_id", con);

                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                String title = "Updation Successful!!!";
                String body = "Author has been successfully Updated in the system.";
                ShowModal(title, body);
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void deleteAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE FROM author_master_tbl WHERE author_id = @author_id", con);

                cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                String title = "Deletion Successful!!!";
                String body = "Author has been successfully deleted from the system.";
                ShowModal(title, body);
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}