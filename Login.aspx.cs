using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["StudentUser"] = null;
    }
    public static string constring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static SqlConnection con = new SqlConnection(constring);
    private Boolean CheckUser(string Email)
    {
        SqlParameter p1, p2;
        SqlCommand cmd = new SqlCommand("select email from test where email=@email", con);
        p1 = new SqlParameter();
        p1.ParameterName = "@email";
        p1.Value = Email;
        p1.SqlDbType = SqlDbType.VarChar;
        p1.Size = 50;
        p1.Direction = ParameterDirection.InputOutput;
        cmd.Parameters.Add(p1);
        string str = "";
        try
        {
            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }
            str = cmd.ExecuteScalar().ToString();

        }
        catch (Exception ex)
        {
           // throw new Exception("Invalid Command !" + ex.Message);
            return false;
        }
        finally
        {
            if (cmd.Connection.State != ConnectionState.Closed)
            {
                cmd.Connection.Close();
            }
        }

        if (str != "")
        {
            return true;
        }
        else
        {
            return false;
        }

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
   
    public string CheckAuthentication(string Email, string Password)
    {
        if (CheckUser(Email))
        {
            SqlParameter p1, p2;
            SqlCommand cmd = new SqlCommand("select email, password from test where email=@email AND password=@password", con);

            p1 = new SqlParameter();
            p1.ParameterName = "@email";
            p1.Value = Email;
            p1.SqlDbType = SqlDbType.VarChar;
            p1.Size = 50;
            p1.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(p1);

            p2 = new SqlParameter();
            p2.ParameterName = "@password";
            p2.Value = Password;
            p2.SqlDbType = SqlDbType.NVarChar;
            p2.Size = 1000;
            p2.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(p2);

            DataTable dt = new DataTable();
            try
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid Command !" + ex.Message);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                {
                    cmd.Connection.Close();
                }
            }



            if (dt.Rows.Count > 0)
            {
                string sUser = dt.Rows[0]["email"].ToString();
                return sUser;
            }
            else
            {
                return "Fail";
            }

        }
        else
        {
            return "Fail";
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        String buff = CreateSalt(10);
        
        String x = TextBox2.Text.ToString();
        //Response.Write(x+"   ");
        String a = GenerateSHA256Hash(x, TextBox1.Text.ToString());
        //Response.Write(a);
        String strUserName = TextBox1.Text;
        String strPassword = a;
        String sResult = CheckAuthentication(strUserName, strPassword);
        if (sResult == "Fail")
        {
            lblError.Text = "<li>Either UserName or Password is incorrect!</li>";
        }
        else
        {
            Session["StudentUser"] = sResult;
            Response.Redirect("/users/Dashboard.aspx");
        }
    }
}