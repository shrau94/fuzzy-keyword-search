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

public partial class users_ConfirmPass : System.Web.UI.Page
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
    public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
    {
        byte[] decryptedBytes = null;

        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                AES.KeySize = 256;
                AES.BlockSize = 128;

                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);

                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cs.Close();
                }
                decryptedBytes = ms.ToArray();
            }
        }

        return decryptedBytes;
    }
    public string DecryptFile(string s)
    {
        string fileEncrypted = Server.MapPath(s);
        string password = "abcd1234";

        byte[] bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

        string file = Server.MapPath("/Files/decp.txt");

        File.WriteAllBytes(file, bytesDecrypted);
        return file;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        int x = (int)Session["fileid"];
        String filepath = Session["filepath"].ToString();
        String pass = TextBox1.Text.ToString();
        String buff = CreateSalt(10);
        //Response.Write(buff + "      ");
        String passx = TextBox1.Text.ToString();
        //Response.Write(x + "    ");
        String a = GenerateSHA256Hash(passx, TextBox1.Text.ToString());
        
        con.Open();
        SqlDataReader myreader;
        SqlCommand cmd2 = new SqlCommand("Select privpass from IDPass where FileId=@id", con);
        cmd2.Parameters.AddWithValue("@id", x);
        myreader = cmd2.ExecuteReader();
        while (myreader.Read())
        {
            String matchpass = myreader.GetString(0);
            if (a == matchpass)
            {
                Response.Write("down");
                string file = DecryptFile(filepath);
                Response.ContentType = "application/msword";
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + file + "\"");
                Response.TransmitFile(file);
                Response.Flush();
                Response.End();
                
            }
        }
    }
}