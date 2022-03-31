using System;
using System.Collections.Generic;



    public class Savings : Accounts
    {
        public override double Widraw(int w_amount)
        {
            if (w_amount > 5000)
            {
               throw new Exception("You cannot widraw more than 5000");
            }
            else
            {
              
              return  base.Widraw(w_amount);
            }
        }
    }
