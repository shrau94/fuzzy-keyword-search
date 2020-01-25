using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;

public partial class users_Result : System.Web.UI.Page
{
    public static string constring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static SqlConnection con = new SqlConnection(constring);
    protected void Page_Load(object sender, EventArgs e)
    {
        List<int> x = (List<int>)Session["Item"];
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        String s1 = "Select * from fileIDTable where fileId=0";
        foreach (int i in x)
        {
            //Response.Write(i);
            
            
            
            s1 = string.Concat(s1, " or fileID=");
            s1 = string.Concat(s1, i);
        }
            cmd.CommandText = s1;
            con.Open();
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();
            con.Close();
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
    protected void lnkDownload_Click(object sender, EventArgs e)
    {

        LinkButton lnkbtn = sender as LinkButton;
        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
        //string filePath = GridView1.DataKeys[gvrow.RowIndex].Value.ToString();
        //string file = DecryptFile("/Cloud_Fuzzy1"+filePath);
        int id = Convert.ToInt32(GridView1.DataKeys[gvrow.RowIndex].Values[0]);
        con.Open();
        SqlDataReader myreader;
        SqlCommand cmd2 = new SqlCommand("Select privpub from fileIDTable where FileId=@id", con);
        cmd2.Parameters.AddWithValue("@id", id);
        myreader = cmd2.ExecuteReader();
        while (myreader.Read())
        {
            int x = myreader.GetInt32(0);
            if (x != 1)
            {
                pubdown(x, gvrow);

            }
            else
            {
                Session["fileid"] = id;
                Session ["filePath"] = GridView1.DataKeys[gvrow.RowIndex].Values[1].ToString();
                Response.Redirect("ConfirmPass.aspx?data=" + Server.UrlEncode(id.ToString()));
            }
        }
        con.Close();
        
    }
    protected void pubdown(int z, GridViewRow gvrow)
    {
        
            string filePath = GridView1.DataKeys[gvrow.RowIndex].Values[1].ToString();
            string file = DecryptFile(filePath);
            Response.ContentType = "application/msword";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + file + "\"");
            Response.TransmitFile(file);
            Response.Flush();
            Response.End();
        
    }
}