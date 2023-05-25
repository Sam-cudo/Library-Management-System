using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Library
{
    public partial class adminbookinventory : System.Web.UI.Page
    {
        string strconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        static string global_fpath;
        static int global_actual_stock, global_current_stock, global_issued;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            if(!IsPostBack) authorDropdown();
        }

        protected void ShowModal(string title, string data)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Modal", "ShowModal('" + title + "', '" + data + "');", true);
        }

        protected void Showimg(string fpath)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "image", "showimg('" + fpath + "');", true);
        }

        // Go Button
        protected void Button2_Click(object sender, EventArgs e)
        {
            selectBookByID();
        }

        // Add Button
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkBookExist())
            {
                String title = "Addition Failed!!!";
                String body = "Book with this ID already Exists. Please try with diffrent ID";
                ShowModal(title, body);
            }
            else addNewBook();
        }

        // Update Button
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkBookExist())
            {
                updateBook();
            }
            else
            {
                String title = "Invalid ID!!!";
                String body = "Book with this ID does not Exists. Please use valid ID";
                ShowModal(title, body);
            }
        }

        // Delete Button
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkBookExist())
            {
                deleteBook();
            }
            else
            {
                String title = "Invalid ID!!!";
                String body = "Book with this ID does not Exists. Please use valid ID";
                ShowModal(title, body);
            }
        }

        protected void authorDropdown()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if(con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT author_name from author_master_tbl;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DropDownList3.DataSource = dt;
                DropDownList3.DataValueField = "author_name";
                DropDownList3.DataBind();
                DropDownList3.Items.Insert(0, "Select");
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
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

        protected void selectBookByID()
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

                if (dt.Rows.Count >= 1) 
                {
                    TextBox2.Text = dt.Rows[0]["book_name"].ToString();
                    DropDownList1.SelectedValue = dt.Rows[0]["language"].ToString().Trim();
                    DropDownList2.SelectedValue = dt.Rows[0]["publisher_name"].ToString().Trim();
                    DropDownList3.SelectedValue = dt.Rows[0]["author_name"].ToString().Trim();

                    ListBox1.ClearSelection();
                    string[] genre = dt.Rows[0]["genre"].ToString().Trim().Split(',');
                    foreach (string item in genre)
                    {
                        for (int j = 0; j < ListBox1.Items.Count; j++)
                        {
                            if (ListBox1.Items[j].ToString() == item)
                            {
                                ListBox1.Items[j].Selected = true;
                            }
                        }
                    }

                    TextBox4.Text = dt.Rows[0]["publish_date"].ToString().Trim();
                    TextBox5.Text = dt.Rows[0]["edition"].ToString();
                    TextBox8.Text = dt.Rows[0]["book_cost"].ToString().Trim();
                    TextBox9.Text = dt.Rows[0]["no_of_pages"].ToString().Trim();
                    TextBox3.Text = dt.Rows[0]["actual_stock"].ToString().Trim();
                    TextBox6.Text = dt.Rows[0]["current_stock"].ToString().Trim();
                    TextBox7.Text =  "" + (Convert.ToInt32(dt.Rows[0]["actual_stock"]) - Convert.ToInt32(dt.Rows[0]["current_stock"]));
                    TextBox10.Text = dt.Rows[0]["book_description"].ToString();

                    global_fpath = dt.Rows[0]["book_img_link"].ToString();
                    global_actual_stock = Convert.ToInt32(dt.Rows[0]["actual_stock"]);
                    global_current_stock = Convert.ToInt32(dt.Rows[0]["current_stock"]);
                    global_issued = global_actual_stock - global_current_stock;

                    Showimg(global_fpath);

                }
                else
                {
                    String title = "Invalid ID!!!";
                    String body = "Book with this ID does not Exists. Please use valid ID";
                    ShowModal(title, body);
                    clearForm();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void addNewBook()
        {
            try
            {
                string fpath = "/book_images/book1.png";
                string fname = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("book_images/" + fname));
                fpath = "/book_images/" + fname;

                string genre = "";
                foreach (int i in ListBox1.GetSelectedIndices())
                {
                    genre+= ListBox1.Items[i].ToString() + ",";
                }
                genre = genre.Remove(genre.Length - 1);

                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO book_master_tbl (book_id, book_name, genre, author_name, publisher_name, publish_date, language, " +
                    "edition, book_cost, no_of_pages, book_description, actual_stock, current_stock, book_img_link) VALUES" +
                    "(@book_id, @book_name, @genre, @author_name, @publisher_name, @publish_date, @language, @edition, @book_cost, @no_of_pages, @book_description, " +
                    "@actual_stock, @current_stock, @book_img_link)", con);

                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publish_date", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@edition", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@book_cost", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@no_of_pages", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@actual_stock", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@current_stock", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@book_description", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@genre", genre);
                cmd.Parameters.AddWithValue("@book_img_link", fpath);

                cmd.ExecuteNonQuery();
                con.Close();
                String title = "Additon Successful!!!";
                String body = "Book has been successfully added in the system.";
                ShowModal(title, body);
                GridView1.DataBind();
                clearForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void updateBook()
        {
            try
            {
                string fpath = "/book_images/book1.png";
                string fname = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if (fname == "" || fname == null)
                {
                    fpath = global_fpath;
                }
                else
                {
                    FileUpload1.SaveAs(Server.MapPath("book_images/" + fname));
                    fpath = "/book_images/" + fname;
                }

                string genre = "";
                foreach (int i in ListBox1.GetSelectedIndices())
                {
                    genre += ListBox1.Items[i].ToString() + ",";
                }
                genre = genre.Remove(genre.Length - 1);

                int actual_stock = Convert.ToInt32(TextBox3.Text.Trim());
                int current_stock = Convert.ToInt32(TextBox6.Text.Trim());
                if (global_actual_stock == actual_stock)
                {

                }
                else
                {
                    if (actual_stock < global_issued)
                    {
                        Response.Write("<script>alert('Actual Stock value cannot be less than the issued books');</script>");
                        return;
                    }
                    else
                    {
                        current_stock = actual_stock - global_issued;
                        TextBox6.Text = "" + current_stock;
                    }
                }

                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE book_master_tbl SET book_name = @book_name, genre = @genre, author_name = @author_name, publisher_name = @publisher_name, " +
                    "publish_date = @publish_date, language = @language, edition = @edition, book_cost = @book_cost, no_of_pages = @no_of_pages, book_description = @book_description, " +
                    "actual_stock = @actual_stock, current_stock = @current_stock, book_img_link = @book_img_link WHERE book_id = @book_id", con);

                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publisher_name", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publish_date", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@edition", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@book_cost", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@no_of_pages", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@actual_stock", actual_stock.ToString());
                cmd.Parameters.AddWithValue("@current_stock", current_stock.ToString());
                cmd.Parameters.AddWithValue("@book_description", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@genre", genre);
                cmd.Parameters.AddWithValue("@book_img_link", fpath);

                cmd.ExecuteNonQuery();
                con.Close();
                String title = "Updation Successful!!!";
                String body = "Book has been successfully Updated in the system.";
                ShowModal(title, body);
                GridView1.DataBind();
                clearForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void deleteBook()
        {
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE FROM book_master_tbl WHERE book_id = @book_id", con);

                cmd.Parameters.AddWithValue("@book_id", TextBox1.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                String title = "Deletion Successful!!!";
                String body = "Book has been successfully deleted from the system.";
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
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox3.Text = "";
            TextBox6.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            DropDownList1.ClearSelection();
            DropDownList2.ClearSelection();
            DropDownList3.ClearSelection();
            ListBox1.ClearSelection();
        }
    }
}