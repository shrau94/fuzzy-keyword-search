using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

public partial class users_PriPub : System.Web.UI.Page
{
   
    public static string constring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static SqlConnection con = new SqlConnection(constring);
   
    protected void Page_Load(object sender, EventArgs e)
    {
       
       
    }
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

        int x = (int)Session["fileid"]; 
        String pass = TextBox3.Text.ToString();
        String buff = CreateSalt(10);
        //Response.Write(buff + "      ");
        String passx = TextBox3.Text.ToString();
        //Response.Write(x + "    ");
        String a = GenerateSHA256Hash(passx, TextBox3.Text.ToString());
        //Response.Write(a);
        con.Open();
        SqlCommand cmd1 = new SqlCommand("insert into IDPass(FileId,privpass) values(@id,@pass)", con);
        cmd1.Parameters.AddWithValue("@id", x);
        cmd1.Parameters.AddWithValue("@pass", a);
        cmd1.ExecuteNonQuery();
        con.Close();
        Response.Redirect("upsuccess.aspx");
    }
}