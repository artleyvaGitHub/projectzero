using System;
using System.Collections.Generic;
using System.Xml.Serialization; // use this namespace to serialize and deserialize in XML Format
using System.IO;  //use this namespace to work with files, open, save, override
using System.Data; 
using System.Data.SqlClient; //Connect and manipulate the Database
using PROJ0Models;


public class AccountTransaction
{
    #region my Variables (Account Transaction)
    public int accNoLogin { get; set; }
    public int accNo { get; set; }
    public string accType { get; set; }
    public string accDescription { get; set; }
    public double accAmount { get; set; }
    public string accDate { get; set; }

    #endregion
   #region Check and Get the existing Account
       public List<AccountTransaction> GetAccountTransaction(List<AccountTransaction> list)
        {
            foreach(var s in list)
            {
                accNo = s.accNo;
            }

           SqlConnection con = new SqlConnection(@"server=DESKTOP-6T8MT4L\MYSQL_ART;database=APLCreditUnionBank;integrated security=true");
           SqlCommand cmd_accGAT = new SqlCommand("select * from AcctTransaction where accNo = @accNo",con);
           cmd_accGAT.Parameters.AddWithValue("@accNo",accNo);
           SqlDataReader readAccountTrans = null;
           List<AccountTransaction> lst_AccountTransDB = new List<AccountTransaction>();

            try
            {
                con.Open();
                readAccountTrans = cmd_accGAT.ExecuteReader();

                while (readAccountTrans.Read())
                {
                    lst_AccountTransDB.Add(new AccountTransaction()
                    {
                    accNo = Convert.ToInt32(readAccountTrans[0]),
                    accDescription = (String.Format("{0:dd-MM-yyyy}",readAccountTrans[1])),
                    accAmount = Convert.ToInt32(readAccountTrans[2]),
                    accDate = readAccountTrans[3].ToString()
                    });
                }
 
            }
            catch (SqlException es)
            {
            throw new Exception(es.Message);
            }
            finally
            {
                readAccountTrans.Close();
                con.Close();
            }
        return lst_AccountTransDB;
    }
    #endregion
}