using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;

namespace E_Library
{
    public partial class adminbookissue : System.Web.UI.Page
    {
        string strconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            if (!IsPostBack)
            {
                memberDropdown();
                bookDropdown();
            }
        }

        protected void ShowModal(string title, string data)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Modal", "ShowModal('" + title + "', '" + data + "');", true);
        }

        // Member selected
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectMemberByID(DropDownList1.SelectedItem.Value);
        }

        // Book selected

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectBookByID(DropDownList2.SelectedItem.Value);
        }

        // Issue Button
        protected void Button2_Click(object sender, EventArgs e)
        {
            if(checkBookAvailable())
            {
                if(checkSameBookIssued())
                {
                    String title = "Book Already Issued!!!";
                    String body = "Member already has this book issued in his/her account.";
                    ShowModal(title, body);
                }
                else issueBook();
            }
            else
            {
                String title = "Out of Stock!!!";
                String body = "This Boook is currently not available.";
                ShowModal(title, body);
            }
        }

        // Return Button
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkSameBookIssued()) returnBook();
            else
            {
                String title = "Invalid ID!!!";
                String body = "Book with this ID does not Exists in this member account. Please use valid ID.";
                ShowModal(title, body);
            }
        }
        protected void memberDropdown()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT member_id from member_master_tbl;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DropDownList1.DataSource = dt;
                DropDownList1.DataValueField = "member_id";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void bookDropdown()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT book_id from book_master_tbl;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DropDownList2.DataSource = dt;
                DropDownList2.DataValueField = "book_id";
                DropDownList2.DataBind();
                DropDownList2.Items.Insert(0, "Select");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void selectMemberByID(String member)
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id ='" + member + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox1.Text = dt.Rows[0][3].ToString();
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

        protected void selectBookByID(String book)
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM book_master_tbl WHERE book_id ='" + book + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0]["book_name"].ToString();

                }
                else
                {
                    String title = "Invalid ID!!!";
                    String body = "Book with this ID does not Exists. Please use valid ID.";
                    ShowModal(title, body);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected bool checkBookAvailable()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM book_master_tbl WHERE book_id ='" + DropDownList2.SelectedItem.Value + "' AND current_stock > 0", con);
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

        protected bool checkSameBookIssued()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM book_issue_tbl WHERE book_id ='" + DropDownList2.SelectedItem.Value + "' AND member_id ='" + DropDownList1.SelectedItem.Value + "'", con);
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

        protected void issueBook()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO book_issue_tbl (member_id, member_name, book_id, book_name, issue_date, due_date) VALUES" +
                    "(@member_id, @member_name, @book_id, @book_name, @issue_date, @due_date)", con);

                cmd.Parameters.AddWithValue("@member_id", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@member_name", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@book_id", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@issue_date", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@due_date", TextBox4.Text.Trim());

                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand("UPDATE book_master_tbl SET current_stock = current_stock-1 WHERE book_id = @book_id", con);
                cmd1.Parameters.AddWithValue("@book_id", DropDownList2.SelectedItem.Value);
                cmd1.ExecuteNonQuery();

                con.Close();
                String title = "Book Issued!!!";
                String body = "Book has been issued successfully.";
                ShowModal(title, body);
                GridView1.DataBind();
                clearForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void returnBook()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE FROM book_issue_tbl WHERE book_id = @book_id AND member_id = @member_id", con);

                cmd.Parameters.AddWithValue("@member_id", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@book_id", DropDownList2.SelectedItem.Value);

                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand("UPDATE book_master_tbl SET current_stock = current_stock+1 WHERE book_id = @book_id", con);
                cmd1.Parameters.AddWithValue("@book_id", DropDownList2.SelectedItem.Value);
                cmd1.ExecuteNonQuery();

                con.Close();
                String title = "Book Returned!!!";
                String body = "Book has been returned successfully.";
                ShowModal(title, body);
                GridView1.DataBind();
                clearForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void clearForm()
        {
            DropDownList1.ClearSelection();
            TextBox1.Text = "";
            DropDownList2.ClearSelection();
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
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
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}