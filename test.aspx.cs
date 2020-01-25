using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public static string constring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static SqlConnection con = new SqlConnection(constring);
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            SqlCommand cmd = new SqlCommand("insert into test values('" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox1.Text + "');", con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (con.State == ConnectionState.Open)
            {
                cmd.ExecuteNonQuery();
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            //Response.Redirect("log-in.aspx?q=success");
        }
        catch (Exception ex)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Some Error Occured Try Again";
        }
    }
}