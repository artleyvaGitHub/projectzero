using System;
using System.Collections.Generic;
using System.Xml.Serialization; // use this namespace to serialize and deserialize in XML Format
using System.IO;  //use this namespace to work with files, open, save, override
using System.Data; 
using System.Data.SqlClient; //Connect and manipulate the Database


[Serializable] //this is an attribute, neeed for an object to come out of memory 

public class Accounts
{
    #region my Variables (class Accounts)
    public int accNo { get; set; }
    public string accName { get; set; }
    public string accAddress { get; set; }
    public string accType { get; set; }
    public double accBalance { get; set; }
    public bool accIsActive { get; set; }
    public string accemail { get; set; }
    public string accStatus { get; set; }
    public int accPinNo { get; set; }
    #endregion

        private string _connectionString; 
        // public Accounts(string connectionString) {
        //     _connectionString = connectionString;
        // }


    // Using Database APLCreditUnionBank, Tables: Accounts, AcctTransaction
    SqlConnection con = new SqlConnection(@"server=DESKTOP-6T8MT4L\MYSQL_ART;database=APLCreditUnionBank;integrated security=true");        
    
    #region Connect and Create New Account
    public string AddNewAccounts(Accounts newAccount)
        {
            SqlCommand cmd_addAccount = new SqlCommand("insert into Accounts values(@accName,@accAddress,@accemail,@accBalance,@accType,@accStatus,@accpinNo)",con);
            cmd_addAccount.Parameters.AddWithValue("@accName",newAccount.accName);
            cmd_addAccount.Parameters.AddWithValue("@accAddress",newAccount.accAddress);
            cmd_addAccount.Parameters.AddWithValue("@accemail",newAccount.accemail);
            cmd_addAccount.Parameters.AddWithValue("@accBalance",newAccount.accBalance);
            cmd_addAccount.Parameters.AddWithValue("@accType",newAccount.accType);
            cmd_addAccount.Parameters.AddWithValue("@accStatus",newAccount.accIsActive);
            cmd_addAccount.Parameters.AddWithValue("@accpinNo",newAccount.accPinNo);
            
            try
            {
                con.Open();
                cmd_addAccount.ExecuteNonQuery();                    
            }
            catch(SqlException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return "Account Added Successfully";
        }    
    #endregion

    #region Check and Get the existing Account
    public Accounts GetExistingAccount(int id, int cusPin)
        {
            SqlConnection con = new SqlConnection(@"server=DESKTOP-6T8MT4L\MYSQL_ART;database=APLCreditUnionBank;integrated security=true");
            Accounts accEx = new Accounts();
            SqlCommand cmd_search = new SqlCommand("select * from Accounts where accNo = @accNo",con);
            cmd_search.Parameters.AddWithValue("@accNo",id);
            SqlDataReader _read;
            
            try
            {
                con.Open();
                _read = cmd_search.ExecuteReader();
                _read.Read();

                //if(_read.Read())
                {
                    accEx.accNo = id;
                    accEx.accName =Convert.ToString(_read[1]);
                    accEx.accAddress =Convert.ToString(_read[2]);
                    accEx.accemail =Convert.ToString(_read[3]);
                    accEx.accBalance =Convert.ToInt32(_read[4]);
                    accEx.accType = Convert.ToString(_read[5]);
                    accEx.accStatus = Convert.ToString(_read[6]);
                    accEx.accPinNo = Convert.ToInt32(_read[7]);

                    cusPin = accEx.accPinNo = Convert.ToInt32(_read[7]);
                    return accEx;
                }
            // System.Console.WriteLine("Else if");
            // Console.ReadLine();
            }
            
            catch (System.Exception es)
        {
            // System.Console.WriteLine("Error Catch");
            //cusPin = 0;
            //accEx.accPinNo = 0;
            //Console.WriteLine("\n\n\n Account "+id+" not found");
            return accEx;
            //System.Console.WriteLine(es.Message);
        }
        finally
        {
            // System.Console.WriteLine("Finally");
            // Console.ReadLine();
            con.Close();
        }
        return accEx;
    }
    #endregion
    public bool Login(string userid, string password)
        {
            SqlConnection con = new SqlConnection(@"server=DESKTOP-6T8MT4L\MYSQL_ART;database=APLCreditUnionBank;integrated security=true");
             SqlCommand cmd_checkrec = new SqlCommand("select count(*) from login where username=@username and password=@password",con);

             cmd_checkrec.Parameters.AddWithValue("@username",userid);
             cmd_checkrec.Parameters.AddWithValue("@password",password);

             try
             {
                 con.Open();
                 int record_count =(int) cmd_checkrec.ExecuteScalar();
                 if (record_count > 0)
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

 
    public bool GetAcctTransaction(string userid, string password)
        {
            SqlConnection con = new SqlConnection(@"server=DESKTOP-6T8MT4L\MYSQL_ART;database=APLCreditUnionBank;integrated security=true");
             SqlCommand cmd_checkrec = new SqlCommand("select count(*) from AcctTransaction where accNo=@accNo",con);

             cmd_checkrec.Parameters.AddWithValue("@accNo",userid);
             

             try
             {
                 con.Open();
                 int record_count =(int) cmd_checkrec.ExecuteScalar();
                 if (record_count > 0)
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

    #region Check and Get the existing Account
        
    public int GetLastRecord(int lastaccNo)
        {
            SqlConnection con = new SqlConnection(@"server=DESKTOP-6T8MT4L\MYSQL_ART;database=APLCreditUnionBank;integrated security=true");
            Accounts accGenAcct = new Accounts();
            SqlCommand cmd_search = new SqlCommand("select Max(accNo)+1 as lastaccNo from Accounts",con);
            SqlDataReader _read;
            try
            {
                con.Open();
                _read = cmd_search.ExecuteReader();
                _read.Read();
            //Generated Account - basically increment of 1 to primary key
                accGenAcct.accNo =Convert.ToInt32(_read[0]);
            }
            catch (System.Exception es)
        {
            // System.Console.WriteLine("Error Catch");
            System.Console.WriteLine(es.Message);
        }
        finally
        {
            con.Close();
        }
        return accGenAcct.accNo;
    }
    #endregion

    #region Update Existing Account either by Deposit or Withdrawal
    public Accounts UpdateAccount(int id,double nBal,double tranAmt,string accDes)
        {
            SqlConnection con = new SqlConnection(@"server=DESKTOP-6T8MT4L\MYSQL_ART;database=APLCreditUnionBank;integrated security=true");
            Accounts accEx = new Accounts();
            SqlCommand cmd_search = new SqlCommand("update Accounts SET accBalance = @accBalance where accNo = @accNo",con);
            
            cmd_search.Parameters.AddWithValue("@accNo",id);
            cmd_search.Parameters.AddWithValue("@accBalance",nBal);
            
            SqlDataReader _read;
            try
            {
                con.Open();
                // update Accounts
                _read = cmd_search.ExecuteReader();
                _read.Read();
                string accTypeI = accEx.accType;
                con.Close();
                
                con.Open();
                // insert Account Transaction Table
                SqlCommand cmd_insert = new SqlCommand("insert into AcctTransaction VALUES(@accNo, @accDesc, @accAmount,CURRENT_TIMESTAMP)",con);
                cmd_insert.Parameters.AddWithValue("@accNo",id);
                cmd_insert.Parameters.AddWithValue("@accDesc",accDes);
                cmd_insert.Parameters.AddWithValue("@accAmount",tranAmt);
                cmd_insert.ExecuteNonQuery(); 

            }
            catch (System.Exception es)
        {
            System.Console.WriteLine(es.Message);
        }
        finally
        {
            con.Close();
        }
        return accEx;
        }
    #endregion

    #region Widraw
    public virtual double Widraw(int w_amount)
        {
            if(w_amount < 0)
            {
                //throw the exception
                throw new Exception("Please Enter Postitive Value");
            }
            else if(w_amount > accBalance)
            {
                throw new Exception("Insufficient Balance");
            }
            else if(w_amount == 0)
            {
                throw new Exception("Enter Amount more than Zero");
            }
            accBalance = accBalance - w_amount;
            return accBalance;
        }
    #endregion
    #region - Save Object to .xml
        public void SaveObject()
        {
                FileStream myfile = new FileStream(accNo + ".xml",FileMode.Create,FileAccess.Write);
                XmlSerializer xs = new XmlSerializer(typeof(Accounts));
                xs.Serialize(myfile,this);
                myfile.Close();
        }
    #endregion

    #region - Save Last Account to .xml
        public void SaveLastAccObj()
        {
                FileStream myfileLast = new FileStream("0db.xml",FileMode.Create,FileAccess.Write);
                XmlSerializer xs = new XmlSerializer(typeof(Accounts));
                xs.Serialize(myfileLast,this);
                myfileLast.Close();
        }
    #endregion

    #region - Get Object from .xml
        public Accounts GetObject(int accountNumber)
        {       
                //Get the account information in the database
                if (accountNumber !=0)
                {
                    FileStream myfile = new FileStream(accountNumber + ".xml",FileMode.Open,FileAccess.Read);
                    XmlSerializer xs = new XmlSerializer(typeof(Accounts));
                    Accounts details =(Accounts) xs.Deserialize(myfile);
                    myfile.Close();
                    return details;
                }
                else
                { //Get the last account number saved
                    FileStream myfile = new FileStream(accountNumber + "db.xml",FileMode.Open,FileAccess.Read);
                    XmlSerializer xs = new XmlSerializer(typeof(Accounts));
                    Accounts details =(Accounts) xs.Deserialize(myfile);
                    myfile.Close();
                    return details;
                }
        }
    #endregion    
}


