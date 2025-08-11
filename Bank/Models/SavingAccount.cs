using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models
{
    internal class SavingAccount : BankAccount
    {
        public decimal InterestRate { get; set; }

        public SavingAccount(string fullName, string nationalId, string phoneNumber, string address, double balance, decimal interestRate)
                    : base(fullName, nationalId, phoneNumber, address, balance)
                {
                    InterestRate = interestRate;
                }

        public override decimal CalculateInterest() => (decimal)_balance * InterestRate / 100;
       
        public override void ShowAccountDetails()
        {
            base.ShowAccountDetails();
            Console.WriteLine($"Interest rate : {InterestRate}");
        }
    }
}

