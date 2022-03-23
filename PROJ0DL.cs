using System.IO;  //use this namespace to work with files, open, save, override 
using System;
using System.Globalization;
using System.Data.SqlClient; //Connect and manipulate the Database
using PROJ0DL;
using PROJ0Models;


namespace PROJ0DL
{
#region thinking of moving this codes
    // public class NewAccount
    //      {
    //      private string _connectionString; 
    //      public NewAccount(string connectionString) {
    //          _connectionString = connectionString;
    //      }
        
    //     SqlConnection con = new SqlConnection(@"server=DESKTOP-6T8MT4L\MYSQL_ART;database=APLCreditUnionBank;integrated security=true");        
    //     #region Connect and Create New Account
    //     public string AddNewAccounts(Accounts newAccount)
    //     {
    //         SqlCommand cmd_addAccount = new SqlCommand("insert into Accounts values(@accName,@accAddress,@accemail,@accBalance,@accType,@accStatus,@accpinNo)",con);
    //         cmd_addAccount.Parameters.AddWithValue("@accName",newAccount.accName);
    //         cmd_addAccount.Parameters.AddWithValue("@accAddress",newAccount.accAddress);
    //         cmd_addAccount.Parameters.AddWithValue("@accemail",newAccount.accemail);
    //         cmd_addAccount.Parameters.AddWithValue("@accBalance",newAccount.accBalance);
    //         cmd_addAccount.Parameters.AddWithValue("@accType",newAccount.accType);
    //         cmd_addAccount.Parameters.AddWithValue("@accStatus",newAccount.accIsActive);
    //         cmd_addAccount.Parameters.AddWithValue("@accpinNo",newAccount.accPinNo);
    //         try
    //         {
    //             con.Open();
    //             cmd_addAccount.ExecuteNonQuery();                    
    //         }
    //         catch(SqlException ex)
    //         {
    //             System.Console.WriteLine(ex.Message);
    //         }
    //         finally
    //         {
    //             con.Close();
    //         }
    //         return "Account Added Successfully";
    //         }    
 #endregion
    
        public class ProjectClassLib
        {
        #region - Project Header  
        public void prnttitle()
        {
            Console.Clear();
            Console.WriteLine(" ");
            Console.BackgroundColor = ConsoleColor.Blue;
            string border = @"                                                    ";
            Console.WriteLine(border);
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.WriteLine("                                                    ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Title = "ASCII Art";
            string title = @"░█▀█░█▀█░█░░░░░█░█░█▀█░▀█▀░█▀█░█▀█░░░█▀▄░█▀█░█▀█░█░█
░█▀█░█▀▀░█░░░░░█░█░█░█░░█░░█░█░█░█░░░█▀▄░█▀█░█░█░█▀▄
░▀░▀░▀░░░▀▀▀░░░▀▀▀░▀░▀░▀▀▀░▀▀▀░▀░▀░░░▀▀░░▀░▀░▀░▀░▀░▀ ";
            Console.WriteLine(title);
            
            //Console.Read();
        // Console.Clear();
        // Console.WriteLine(" ");
        // Console.BackgroundColor = ConsoleColor.Blue;
        // Console.ForegroundColor = ConsoleColor.White;
        // Console.WriteLine("                                                   ");
        // Console.WriteLine("                 APL CREDIT UNION                  ");
        // Console.WriteLine("                                                   ");
         Console.BackgroundColor = ConsoleColor.Blue;
        }
        #endregion
        
        #region Hide Pin
        public int hidePin(int hPin)
        {
            string pass = "";
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            int accPin = Convert.ToInt32(pass);

            return accPin;
        }
        #endregion

        #region SecurityCheck User and Password

        public bool SecurityCheck(bool pContinueBank)
        {
            
            Console.ForegroundColor = ConsoleColor.Black;
            //Console.WriteLine("                 Security Check                     ");
            string border = @"                 Security Check                     ";
            Console.WriteLine(border);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\n\n\n              ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("         User           \n\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("              User Name: ");
            string uName = Console.ReadLine();
            Console.Write("               Password: ");
            Console.BackgroundColor = ConsoleColor.Black;
            string accCusPinx="";
            string accCusPinEnt=hidePass(accCusPinx); //Password entered
            Security sec2 = new Security();
            bool loginResult = sec2.Login(uName,accCusPinEnt);
            // For testing the values
            // Console.WriteLine("\nLogin Result is "+loginResult);
            // Console.WriteLine("Login User "+uName);
            // Console.WriteLine("Login Pass "+accCusPinx);
            // Console.WriteLine("Hide Pass "+accCusPinEnt);
            //Console.ReadLine();
            if (!loginResult)
            {    
                //ContinueBanking2 main = new ContinueBanking2();
                //Security mainmenu = new Security();
                //mainmenu.
                
                Console.WriteLine("\n\n\n Invalid Username and Password.");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n                                                    ");
                Console.BackgroundColor = ConsoleColor.Black;  
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Press enter to try again.");
                Console.ReadLine();
                Console.Clear();
                pContinueBank = false;
                //MainMenu pcontbank = new MainMenu();
                //Security sec = new Security();
                //pContBanking pcontbank = new pContBanking();
                //Console.WriteLine("Security Check If" + pContinueBank);
                //Console.ReadLine();
                return pContinueBank;
            }
            else
            { pContinueBank = true;
            //Console.WriteLine("Security Check Else" + pContinueBank);
            return pContinueBank;}
        }
        #endregion

        #region Hide Password
        public string hidePass(string hPass)
        {
            string pass = "";
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            string accPin = Convert.ToString(pass);

            return accPin;
        }
        #endregion


        #region Login Account
        public int LoginAccount(int accNoLogin)
        {
            prnttitle();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("                 Security Check                     ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\n\n\n              ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("      Account Login       \n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("               Account No: ");
            accNoLogin = Convert.ToInt32(Console.ReadLine());
            Console.Write("           Enter your PIN: ");
            Console.BackgroundColor = ConsoleColor.Black;
            int accCusPinx=0;
            int accCusPinEnt=hidePin(accCusPinx); //Pin Code Entered
            Accounts accGetRec = new Accounts();
 
            Accounts accGR = accGetRec.GetExistingAccount(accNoLogin,accCusPinEnt);
            
            //  Console.WriteLine("The Pin in database is : " + accGR.accPinNo);
            //  Console.WriteLine("The Pin in entered is : " + accCusPinEnt);
            //  Console.ReadLine();
 
            //check if PIN is correct
            int cusPin = Convert.ToInt32(accCusPinEnt);
            if (accGR.accPinNo != cusPin)
            {
                //Console.WriteLine("accGR.accPinNo "+accGR.accPinNo);
                //Console.WriteLine("cusPin "+cusPin);
                accCusPinEnt=0;
                accNoLogin=0;
                Console.WriteLine("\n\n\n The account you entered not found.");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n                                                    ");
                Console.BackgroundColor = ConsoleColor.Black;  
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Press enter to return to Main Menu ");
                Console.ReadLine();
                Console.Clear();
            }
                return (accNoLogin);
        }
        #endregion
        }

}

