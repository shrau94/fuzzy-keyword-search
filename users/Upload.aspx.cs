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



public partial class users_Upload : System.Web.UI.Page
{
    public static string constring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static SqlConnection con = new SqlConnection(constring);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["StudentUser"] == null)
                Response.Redirect("~/Login.aspx");
            else
            {
                Response.ClearHeaders();
                Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "no-cache");
            }

        }

    }
    /*static string[] SplitWords(string s)
    {
        //
        // Split on all non-word characters.
        // ... Returns an array of all the words.
        //
        return Regex.Split(s, @"\W+");
        // @      special verbatim string syntax
        // \W+    one or more non-word characters together
    }*/
    public void wild(String p, int f)
    {
        String a = p;
        int i, j, k = 0, l, m, v;
        char[] b = a.ToCharArray();
        l = a.Length;
        v = (2 * l + 2) * l + (2 * l + 1);
        char[] str = new char[l*(l+1)+l+10];
        char[,] s = new char[l+1,l];

        for (i = 0; i < l+1; i++)
        {
            k = 0;
            for (j = 0; j < l; j++)
            {
                s[i, j] = b[k++];
                //Console.Write(s[i,j]);

            }

        }

        for (i = 0; i < l; i++)
        {
            for (j = 0; j < l ; j++)
            {

                if (i == j)
                {
                    s[i, j] = '*';
                }
                //Console.Write(s[i,j]);
                //Console.WriteLine();
            }
        }
        k = 0;
        
            for (i = 0; i < l+1; i++)
            {
                for (j = 0; j < l ; j++)
                {
                    
                        str[k] = s[i, j];
                        //Response.Write(str[k]);
                    k++;
                }
                str[k] = ' ';
                k++;
            }
            //str[k] = '\0';
            String stri = new String(str);
            //stri.TrimEnd('\0');
            stri.Split('\0');
            con.Open();
            SqlCommand cmd1 = new SqlCommand("insert into fuzzyGen(keyword,fuzzyKeyword) values(@Name,@Path)", con);
            cmd1.Parameters.AddWithValue("@Name", p);
            cmd1.Parameters.AddWithValue("@Path", stri);
            cmd1.ExecuteNonQuery();
            con.Close();

            stri.Trim();
            string[] w = stri.Split(' ');
            Response.Write(w.Length + "%%"); 
            //foreach (string s1 in w)
            for(int z=0;z<w.Length-1;z++)
            {
                
                String s1 = w[z];
                Response.Write(s1 + "&&");
                con.Open();
                SqlCommand cmd2 = new SqlCommand("insert into FuzzyFiles(fileID,fuzzykeyword) values(@id,@fk)", con);
                cmd2.Parameters.AddWithValue("@id", f);
                cmd2.Parameters.AddWithValue("@fk", s1);
                cmd2.ExecuteNonQuery();
                con.Close();
            }
        }
    public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
    {
        byte[] encryptedBytes = null;

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

                using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    cs.Close();
                }
                encryptedBytes = ms.ToArray();
            }
        }

        return encryptedBytes;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string file = Path.GetFileName(file1.PostedFile.FileName);
        String s="x";
        int x;
        file1.SaveAs(Server.MapPath("/Files/" + file));
        con.Open();
        SqlCommand cmd1 = new SqlCommand("select MAX(FileID) from fileIDTable", con);
        x =  (int) cmd1.ExecuteScalar();
        con.Close();
        if (s == "NULL")
        {
            x = 101;
        }
        else
        {
           
            x++;
        }
        String s1, s2, s3, s4, s5;
        int val = 0;
        s1 = TextBox1.Text.ToString();
        s2 = TextBox2.Text.ToString();
        s3 = TextBox3.Text.ToString();
        s4 = TextBox4.Text.ToString();
        s5 = TextBox5.Text.ToString();
        if (s1 == "")
            s1=" ";

        if (s2 == "")
            s2 = " ";

        if (s3 == "")
            s3 = " ";

        if (s4 == "")
            s4 = " ";

        if (s5 == "")
            s5 = " ";
        if (access.SelectedValue == "Private")
            val = 1;
        else
            val = 0;
        con.Open();
        SqlCommand cmd = new SqlCommand("insert into fileIDTable(FileID,FileName,FilePath,Keywords,privpub) values(@ID,@Name,@Path,@keyword,@pri)", con);
        cmd.Parameters.AddWithValue("@ID", x);
        cmd.Parameters.AddWithValue("@Name", file);
        cmd.Parameters.AddWithValue("@Path", "/Files/" + file);
        cmd.Parameters.AddWithValue("@keyword", s1 + " " + s2 + " " + s3 + " " + s4 + " " + s5);
        cmd.Parameters.AddWithValue("@pri", val);
        cmd.ExecuteNonQuery();
        con.Close();
        string filename = Server.MapPath("/Files/" + file);
        string password = "abcd1234";

        byte[] bytesToBeEncrypted = File.ReadAllBytes(filename);
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        // Hash the password with SHA256
        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

        string fileEncrypted = Server.MapPath("/Files/" + file);

        File.WriteAllBytes(fileEncrypted, bytesEncrypted);

        s1 = TextBox1.Text.ToString();
        s2 = TextBox2.Text.ToString();
        s3 = TextBox3.Text.ToString();
        s4 = TextBox4.Text.ToString();
        s5 = TextBox5.Text.ToString();
        char[] z = s5.ToCharArray();
        int j = s5.Length;
        
        //for (int i = 0; i < j; i++)
            //Response.Write(z[i]+"&nbsp;");
        if(s1!= "")
            wild(s1,x);
       
        if (s2 != "")
            wild(s2,x);

        if (s3 != "")
            wild(s3,x);

        if (s4 != "")
            wild(s4,x);

        if (s5 != "")
            wild(s5,x);

        if (access.SelectedValue == "Private")
        {
            Session["fileid"] = x;
            Response.Redirect("PriPub.aspx?data=" + Server.UrlEncode(x.ToString()));
        }
        Response.Redirect("/users/upsuccess.aspx");
    }
}
