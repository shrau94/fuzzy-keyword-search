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


public partial class users_Search : System.Web.UI.Page
{
    public static string constring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    public static SqlConnection con = new SqlConnection(constring);
    List<int> termsList = new List<int>();
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
            //checkMe();
        }

    }
    protected void ButtonClick(object sender, EventArgs e)
    {
        int[] x = new int[150];
        Boolean flag;
        String inp, variable;
        String[] fuzz = new String[1000];
        String[] colltrue = new String[1000];
        int i=0, j,k=0,n,p=0;
        inp = TextBox1.Text.ToString();
        con.Open();
        SqlCommand command = new SqlCommand("select fuzzykeyword from FuzzyFiles", con);
       // SqlCommand command = new SqlCommand("select keywords from fileIDTable", con);
        SqlDataReader myreader;
        myreader = command.ExecuteReader();
                        k = 0;
        Response.Write("<div style='position: absolute; top: 60%; left: 45%; margin:0 auto;'>");
        //con.Close();
        while (myreader.Read())
        {
            variable = myreader.GetString(0);
            fuzz = variable.Split(' ');
                j = inp.Length;
               
                    foreach (string words in fuzz)
                    {
                        //words.Trim();
                        flag = stringmatch(words, inp);
                        if (flag == true)
                        {
                            colltrue[k] = words;
                           // Response.Write(colltrue[k] + "&&");
                            k++;
                            
                        }
                    }
        }
        con.Close();
        //con.Open();
        p = 0;
        foreach (String collword in colltrue)
        {
            con.Open();
            String s1 = "select distinct (fileID) from FuzzyFiles where fuzzykeyword = ";
            s1 = string.Concat(s1, "'");
            s1 = string.Concat(s1, collword);
            s1 = string.Concat(s1, "'");
            SqlCommand cmd1 = new SqlCommand(s1, con);
            SqlDataReader myreader1;
            myreader1 = cmd1.ExecuteReader();
            while (myreader1.Read())
            {
                int z = myreader1.GetInt32(0);
                if (!x.Contains(z))
                {
                    x[p] = z;
                    p++;
                    printRes(myreader1.GetInt32(0));
                }
                //Response.Write(z);
                
                
            }
            con.Close();
        }
       /* Response.Write(p + " ");
       for (int u=0;u<p ;u++ )
            Response.Write("%%" + x[p] );
        */
        int[] terms = termsList.ToArray();
        Session["Item"] = termsList;
        Response.Redirect("Result.aspx");
       Response.Write("</div>");
       
        
    }
    public Boolean stringmatch(string words, string inp)
    {
        int i;
        Boolean flag = true;
        //char[] fuzzwords = new char[100];
        //fuzzwords = words.ToCharArray();
        //Response.Write(words.Length + " " + (inp.Length + 1) + " ||");
        if (words.Length <= (inp.Length + 1) && words != null && words != "NULL" && words != " ")
        {
            //Response.Write(words + " " + inp + " ||");
            if (words.Length == inp.Length)
            {
                for (i = 0; i < inp.Length; i++)
                {
                    if (words[i] == '*')
                        continue;
                    else
                        if (words[i] != inp[i])
                        {
                            flag = false;
                            break;
                        }
                }
                return flag;
            }
            int findex = 0, uindex = 0;
            
                for (i = 0; i < words.Length; i++)
                {
                    
                    if (words[findex] == '*')
                    {
                        findex++;
                        continue;
                    }
                    else
                        if (words[findex] != inp[uindex])
                        {
                            flag = false;
                            break;
                        }
                    findex++;
                    uindex++;
                }
                return flag;
            }
            return false;
        }
    public void printRes(int res)
    {
        //Response.Write("$$$"+ res); 
                //<a href="javascript:;" onclick="checkMe();" runat="server">PK</a>        
        //Response.Write("<script type='text/javascript'> function checkMe() { alert('hello'); PageMethods.MyCSharpMethod('cSharp', onComplete);    }function onComplete(result, response, content) { alert(result);}    </script>");
        //Response.Write("<span>&nbsp;&nbsp;" + res + " <button onClick='checkMe();' >Download</button><br /> <br /> </span>");
        termsList.Add(res);
        //Session["Item"] = res;
        //Response.Redirect("Result.aspx");
        //Response.Write("<asp:LinkButton ID='lnkDownload' runat='server' Text='Download OnClick='lnkDownload_Click'></asp:LinkButton>");
    }
    public void checkMe()
    {
      //  Response.Write("1223");
    }


}
    
        
       
      
  
