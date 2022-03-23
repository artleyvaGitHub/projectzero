using System;
using System.Globalization;
using PROJ0DL;
using PROJ0Models;
using System.IO;  //use this namespace to work with files, open, save, override
using System.Data; 


namespace PROJ0UI;


public class MainMenu
{
 
    public MainMenu() {}

        string accMessage = "";
        char accSave = 'Y';
        public static int accNoLogin;
        public bool continueBanking = false;      
        ProjectClassLib prjclslib = new ProjectClassLib();
        Security sec = new Security();

    public void Start()
    {
        Accounts accAccObj = new Accounts();      
        Savings accSavObj = new Savings();
        Security sec = new Security();
        bool pcontinueBanking = true;
        
       //Check the Login Accounts for Credentials
       //ConsoleKey key;
        do
        {
            //var keyInfo = Console.ReadKey(intercept: true);
            //key = keyInfo.Key;
            //if (key == ConsoleKey.Escape)
            //pcontinueBanking = true;
            //else{
            prjclslib.prnttitle();
            bool seccheck = prjclslib.SecurityCheck(pcontinueBanking);
            if (seccheck == true)
                {continueBanking = seccheck;
                pcontinueBanking = true;}
            else{pcontinueBanking = seccheck;}
            //}
        } while (!pcontinueBanking);

    #region Main Menu

    while(continueBanking)
        {
           
        #region Case Options
        
        prjclslib.prnttitle();
        Console.ForegroundColor = ConsoleColor.Black;
        //Console.WriteLine("                      Welcome                       \n");
        string welcome = @"                      Welcome                       ";
        Console.WriteLine(welcome+"\n");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("                     Main Menu\n");
        Console.ForegroundColor = ConsoleColor.Black;
        // OPTIONS 1 & 4
        Console.BackgroundColor = ConsoleColor.DarkMagenta;
        Console.Write(" 1 >>");
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Write(" ");
        Console.BackgroundColor = ConsoleColor.White;
        Console.Write(" New Account       ");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Write("  ");
        Console.BackgroundColor = ConsoleColor.White;
        Console.Write("           Deposit ");
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Write(" ");
        Console.BackgroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("<< 4 ");
        
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine(" ");
        Console.BackgroundColor = ConsoleColor.Black;

        // OPTIONS 2 & 5
        Console.BackgroundColor = ConsoleColor.DarkMagenta;
        Console.Write(" 2 >>");
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Write(" ");
        Console.BackgroundColor = ConsoleColor.White;
        Console.Write(" Check Balance     ");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Write("  ");
        Console.BackgroundColor = ConsoleColor.White;
        Console.Write("   Account Details ");
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Write(" ");
        Console.BackgroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("<< 5 ");

        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine(" ");

        // OPTIONS 3 & 6
        Console.BackgroundColor = ConsoleColor.DarkMagenta;
        Console.Write(" 3 >>");
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Write(" ");
        Console.BackgroundColor = ConsoleColor.White;
        Console.Write(" Withdrawal        ");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Write("  ");
        Console.BackgroundColor = ConsoleColor.White;
        Console.Write("              Exit ");
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Write(" ");
        Console.BackgroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("<< 6 \n\n");

        
        
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        /*
        Console.WriteLine("\t1. Create New Account\t\t");
        Console.WriteLine("\t2. Check Balance\t\t");
        Console.WriteLine("\t3. Withdraw\t\t\t");
        Console.WriteLine("\t4. Deposit\t\t\t");
        Console.WriteLine("\t5. Account Information\t\t\t");
        Console.WriteLine("\t6. Exit\t\t\t\t");
        Console.WriteLine("                                                 ");
        */    

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(accMessage);
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.WriteLine("                                                    ");
        Console.BackgroundColor = ConsoleColor.Black;  
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Enter option [1 - 6]: ");        
    #endregion

        
        int choicex=0;
        //int choice = Convert.ToInt32(Console.ReadLine());
        if (int.TryParse(Console.ReadLine(), out int number))
        {   
            choicex = number;
        }
        else
        {
            choicex = 0;     
        }
        
          int choice = choicex;

        if((choice !> 0 )  && (choice !< 7))
            {   
                switch(choice)
                {
                    // Create New Account
                    case 1:
                        CreateNewAccount();
                    break;
                    // Check Balance
                    case 2:
                        CheckBalanceAccount();
                    break;
                    // Withdrawal
                    case 3:
                        WithdrawToAccount();
                    break;
                    // Deposit
                    case 4:
                        DepositToAccount();
                    break;
                    // View Transactions
                    case 5:
                        ViewTransactions();
                        
                        break;
                #endregion

                #region 6. Exit
                    case 6:
                        Console.Clear();
                        continueBanking = false;
                        break;
                #endregion              

                #region Default Other Options and exit
                    default:
                        accMessage = "Message: Enter 1 to 6";
                        break;
                #endregion
                }
            }
            else
            {
                accMessage = "Message: Enter 1 to 6";
            }
        }

    }
    #region 1. Create New Account  CreateNewAccount()
    public void CreateNewAccount()
    {
        accMessage = "";
        prjclslib.prnttitle();
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("              Create a New Account                 \n");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Accounts accNewAccounts = new Accounts();
        Console.WriteLine("     Account No: *** Bank Generated ***");
        Console.Write("      Full Name: ");
        accNewAccounts.accName = Console.ReadLine();
        Console.Write("   Home Address: ");
        accNewAccounts.accAddress = Console.ReadLine();
        Console.Write("   Account Type: [1] Checking [2] Savings: ");
        int accType_v = Convert.ToInt32(Console.ReadLine());
        if(accType_v == 1)
            {accNewAccounts.accType = "Checking";}
        else{accNewAccounts.accType = "Savings";}
        Console.Write("  Email address: ");
        accNewAccounts.accemail = Console.ReadLine();
        Console.Write("      Enter PIN: ");
        int accNewPin = prjclslib.hidePin(accNoLogin);
        accNewAccounts.accPinNo = Convert.ToInt32(accNewPin);
        Console.Write("\nEnter Initial Deposit Amount: ");
        accNewAccounts.accBalance = Convert.ToInt32(Console.ReadLine());
        Console.Write("\n               Save? (Y/N): ");
        accSave = Convert.ToChar(Console.ReadLine());
        if(char.ToUpper(accSave) == 'Y'){
            accNewAccounts.accIsActive = true;
            //Get the Account Number Generated (that would be the last record in the table)
            Accounts accLastRec = new Accounts();
            int genAcct = Convert.ToInt32(accLastRec.GetLastRecord(accNoLogin));            accNewAccounts.accStatus = "Active";
            accNewAccounts.AddNewAccounts(accNewAccounts);
            //accGetlast.SaveObject();
            //accGetlast.SaveLastAccObj();
            Console.Write("\nAccount: " +genAcct+" "+ accNewAccounts.accName+ " was created.");}
        else{
            Console.ForegroundColor = ConsoleColor.Red;  
            Console.Write("\nTransaction for Account: " +accNewAccounts.accNo+" "+ accNewAccounts.accName+ " was cancelled.");
            Console.ForegroundColor = ConsoleColor.White;  
        }
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n\n\n                                                   ");
        Console.BackgroundColor = ConsoleColor.Black;  

        Console.Write("Press enter to return to Main Menu ");
            Console.ReadLine();
            Console.Clear();    
    }
    #endregion

    #region 2. Check Balance  CheckBalanceAccount()
    public void CheckBalanceAccount()
    {
        accMessage = "";
        int cusPinC=0;
        int accNum = prjclslib.LoginAccount(accNoLogin);
        if (accNum != 0)
        {
        Accounts accGetObj = new Accounts();
        Accounts accE = accGetObj.GetExistingAccount(accNum,cusPinC);
        prjclslib.prnttitle();
        Console.ForegroundColor = ConsoleColor.Black;     
        Console.WriteLine("                  Balance Account                   \n");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("      Account No: "+accE.accNo);
        Console.WriteLine("            Type: "+accE.accType);
        Console.WriteLine("            Name: "+accE.accName);
        Console.WriteLine("         Address: "+accE.accAddress);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n Available Balance: "+(accE.accBalance).ToString("C",CultureInfo.CurrentCulture));

        Console.BackgroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n\n\n                                                   ");
        Console.BackgroundColor = ConsoleColor.Black;  

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Press enter to return to Main Menu ");
        Console.ReadLine();
        Console.Clear();
        }
    }
    #endregion
    
    #region 3. Withdraw To Account WithdrawToAccount()
    public void WithdrawToAccount()
    {
        accMessage = "";
        string accDes = "Withdrawal";
        //int cusChoice = 3;
        int cusPinW=0;
        
        int accNumW = prjclslib.LoginAccount(accNoLogin);
        if (accNumW != 0)
        {
        string accActiveStatus ="";
        prjclslib.prnttitle();
        Console.ForegroundColor = ConsoleColor.Black;     
        Console.WriteLine("                    Withdrawal                      \n");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;                        
        Accounts accGetObjW = new Accounts();
        Accounts accW = accGetObjW.GetExistingAccount(accNumW,cusPinW);
        //Console.Write("Enter Account No: ");
        //int accNumW = Convert.ToInt32(Console.ReadLine());
        //accGetObjW = accGetObjW.GetObject(accNumW);
        Console.WriteLine("      Account No: "+accW.accNo);
        Console.WriteLine("            Type: "+accW.accType);
        Console.WriteLine("            Name: "+accW.accName);
        int accActStatus = Convert.ToInt32(accW.accStatus);
        //string accActiveStatus = "";
        if(accActStatus == 1)
            {accActiveStatus = "Active";}
        else{accActiveStatus = "Inactive";}
        Console.WriteLine("  Account Status: "+accActiveStatus);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n Current Balance: "+(accW.accBalance).ToString("C",CultureInfo.CurrentCulture));
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Write("Enter the amount to withdraw: ");
        int amountToWithdraw =Convert.ToInt32(Console.ReadLine());
        accW.accBalance -= amountToWithdraw;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("\n             Continue? (Y/N): ");
        accSave = Convert.ToChar(Console.ReadLine());
        if(char.ToUpper(accSave) == 'Y')
        {
            accGetObjW.UpdateAccount(accW.accNo,accW.accBalance,amountToWithdraw,accDes);
            Console.Write("\nAccount: " +accW.accNo+" "+ accW.accName);
            Console.Write("\nNew Balance: " +(accW.accBalance).ToString("C",CultureInfo.CurrentCulture));

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n                                                   ");
            Console.BackgroundColor = ConsoleColor.Black;  
            
            Console.Write("Press enter to return to Main Menu ");
            Console.ReadLine();
            Console.Clear(); 
        }
        else
        {accMessage = "Transaction was cancelled";
            Console.Clear();                
        }
        }    
    }
    #endregion

    #region 4. Deposit To Account DepositToAcccount()
    public void DepositToAccount()
    {
        accMessage = "";
        string accDes = "Deposit";
        int cusPinD=0;
        int accNumD = prjclslib.LoginAccount(accNoLogin);
        if (accNumD != 0)
        {        
        string accActiveStatus ="";
        prjclslib.prnttitle();
        Console.ForegroundColor = ConsoleColor.Black;     
        Console.WriteLine("                    Deposit                         \n");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Accounts accGetObjD = new Accounts();
        Accounts accD = accGetObjD.GetExistingAccount(accNumD,cusPinD);
        Console.WriteLine("      Account No: "+accD.accNo);
        Console.WriteLine("            Type: "+accD.accType);
        Console.WriteLine("            Name: "+accD.accName);
        int accActStatus = Convert.ToInt32(accD.accStatus);
        if(accActStatus == 1)
            {accActiveStatus = "Active";}
        else{accActiveStatus = "Inactive";}
        Console.WriteLine("  Account Status: "+accActiveStatus);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n Current Balance: "+(accD.accBalance).ToString("C",CultureInfo.CurrentCulture));
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("Enter amount to deposit: ");
        int amountToDeposit = Convert.ToInt32(Console.ReadLine());
        accD.accBalance += amountToDeposit;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("\n             Continue? (Y/N): ");
        accSave = Convert.ToChar(Console.ReadLine());
        if(char.ToUpper(accSave) == 'Y')
        {
            accGetObjD.UpdateAccount(accD.accNo,accD.accBalance,amountToDeposit,accDes);
            Console.Write("\nAccount: " +accD.accNo+" "+ accD.accName);
            Console.Write("\nNew Balance: " +(accD.accBalance).ToString("C",CultureInfo.CurrentCulture));

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n                                                    ");
            Console.BackgroundColor = ConsoleColor.Black;  

            Console.Write("Press enter to return to Main Menu ");
            Console.ReadLine();
            Console.Clear();                
        }
        else
        {accMessage = "Transaction was cancelled";
            Console.Clear();                
        }
        }
    }

    #endregion

    #region 5. View Transactions ViewTransactions()
    public void ViewTransactions()
    {
        accMessage = "";
        //int cusChoice = 5;
        int cusPinV=0;
        int accNumV = prjclslib.LoginAccount(accNoLogin);
        if (accNumV != 0)
        {          
        //string accActiveStatus ="";
        prjclslib.prnttitle();
        Console.ForegroundColor = ConsoleColor.Black;     
        Console.WriteLine("                Account Information                 \n");
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        Accounts accInfoObj = new Accounts();
        AccountTransaction accTrans = new AccountTransaction();
        Accounts accV = accInfoObj.GetExistingAccount(accNumV,cusPinV);
        accTrans.accNo = accV.accNo;
        //Name and Account
        string strname = " Name: "+accV.accName;
        string stracc = "Account No: "+accV.accNo;
        //get the pad space
        int blnkspace = 52-(strname.Length+14);
        Console.Write(strname);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(stracc.PadLeft(blnkspace+14));
        Console.ForegroundColor = ConsoleColor.White;
        
        //Address and Type
        string strAddress = " Address: "+accV.accAddress;
        string strType = "Type: "+accV.accType;
        //get the pad space
        blnkspace = 52-(strAddress.Length+14);
        Console.Write(strAddress);
        Console.WriteLine(strType.PadLeft(blnkspace+14));

         //Email and Account Status
        string strEmail = " Email: "+accV.accemail;
        string strStatus ="";
        int accActStatus = Convert.ToInt32(accV.accStatus);
        if(accActStatus==1)
            {strStatus = "Active";}
        else{strStatus = "Inactive";}
        strStatus = "Status: "+strStatus;
        //get the pad space
        blnkspace = 52-(strEmail.Length+14);
        Console.Write(strEmail);
        Console.WriteLine(strStatus.PadLeft(blnkspace+14));
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" Account Balance: "+(accV.accBalance).ToString("C",CultureInfo.CurrentCulture));
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.Yellow;
        string viewInfo = "Statement of Account";
        blnkspace = (52-viewInfo.Length)/2;
        string strspace=" ";
        Console.WriteLine("\n"+strspace.PadLeft(blnkspace)+viewInfo.PadLeft(blnkspace)+strspace.PadLeft(blnkspace));
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.DarkGray;
        strspace=" ";
        string strDatex = "   Date   ";
        string strDesc = "       Description         ";
        string strAmount = "    Amount     "; 
        Console.WriteLine(strDatex+strDesc+strAmount);
        //View the Account Transaction for the selected Account No.
        AccountTransaction pracc = new AccountTransaction();
        List<AccountTransaction> listact = new List<AccountTransaction>();
        listact.Add(new AccountTransaction{accNo=accNumV});
        List<AccountTransaction> lstacc = pracc.GetAccountTransaction(listact);
        foreach (var item in lstacc)
        {
            string date_desc = (item.accDate.Substring(0,10)+item.accDescription);  
            string accamt = (item.accAmount).ToString("C",CultureInfo.CurrentCulture); 
            blnkspace = 52-date_desc.Length-2;
                            
            Console.Write(" "+date_desc+accamt.PadLeft(blnkspace)+" \n");
        }
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n                                                    ");
        Console.BackgroundColor = ConsoleColor.Black;  
        Console.ForegroundColor = ConsoleColor.White;

        Console.Write("Press enter to return to Main Menu ");
        Console.ReadLine();
        Console.Clear();
        }
    }
}
    #endregion
