using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public static string constring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static SqlConnection con = new SqlConnection(constring);
     public String CreateSalt(int size)
    {
        var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        var buff = new byte[size];
        rng.GetBytes(buff);
        return Convert.ToBase64String(buff);
    }

     public static string ByteArrayToHexString(byte[] ba)
     {
         string hex = BitConverter.ToString(ba);
         return hex.Replace("-", "");
     }

    public String GenerateSHA256Hash(String input, String salt)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
        System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
        byte[] hash = sha256hashstring.ComputeHash(bytes);
        String pass = ByteArrayToHexString(hash);
        return pass;
    }
   
   

    

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        
     //   try
      //  {
           
            String buff = CreateSalt(10);
            Response.Write(buff + "      ");
            String x = TextBox3.Text.ToString();
            Response.Write(x+"    ");
            String a = GenerateSHA256Hash(x, TextBox2.Text.ToString());
            Response.Write(a);
        SqlCommand cmd = new SqlCommand("insert into test(email,password,name) values(@email,@Pass,@name)", con);
            cmd.Parameters.AddWithValue("@email", TextBox2.Text);
            cmd.Parameters.AddWithValue("@Pass", a);
            cmd.Parameters.AddWithValue("@name", TextBox1.Text);
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

           Response.Redirect("Login.aspx?q=success");
       // }
      /*  catch (Exception ex)
        {
            Label1.ForeColor = System.Drawing.Color.Red;
            Label1.Text = "Some Error Occured Try Again";
        }*/
    }
}