using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace live_Back_account.Models
{
    internal class Current : Account
    {
        public double OverdraftLimit { get; set; } = 1000; 

        public override double Withdrawing(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be positive.");
                return Balance;
            }
            if (amount > Balance + OverdraftLimit)
            {
                Console.WriteLine("Exceeds overdraft limit.");
                return Balance;
            }
            Balance -= amount;
            Console.WriteLine($"Withdrawn {amount}. New balance: {Balance}");
            return Balance;
        }

        public override void ShowDetails()
        {
            base.ShowDetails();
            Console.WriteLine($"Overdraft Limit: {OverdraftLimit}");
        }
    }
}