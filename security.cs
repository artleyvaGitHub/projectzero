using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PROJ0Models
{
    public class Security 
    {
        public int uName { get; set; }
        public int uPass { get; set; }

           public bool Login(string uName,string pwd)
          {
             SqlConnection con = new SqlConnection(@"server=DESKTOP-6T8MT4L\MYSQL_ART;database=APLCreditUnionBank;integrated security=true");
             SqlCommand cmd_login = new SqlCommand("select count(*) from login where username=@uName and password=@pwd",con);

             cmd_login.Parameters.AddWithValue("@uName",uName);
             cmd_login.Parameters.AddWithValue("@pwd",pwd);

             try
             {
                 con.Open();
                 int cridential_count =(int) cmd_login.ExecuteScalar();
                 if (cridential_count > 0)
                 {
                     return true;
                 }
                 else
                 {
                     return false;
                 }
             }
             catch (System.Exception es)
             {
                 
                 throw new Exception(es.Message);
             }
             finally
             {
                 con.Close();
             }
        }
    }
}
