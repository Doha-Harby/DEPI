using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace live_Back_account.Models
{
    internal class Saving : Account
    {
        public double InterestRate { get; set; } = 0.05; 

        public double CalculateMonthlyInterest()
        {
            double monthlyInterest = Balance * InterestRate / 12;
            Console.WriteLine($"Monthly Interest: {monthlyInterest}");
            return monthlyInterest;
        }

        public override void ShowDetails()
        {
            base.ShowDetails();
            Console.WriteLine($"Interest Rate: {InterestRate * 100}%");
        }
    }
}