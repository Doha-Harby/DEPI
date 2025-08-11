using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models
{
    internal class CurrentAccount : BankAccount
    {
        private decimal OverdraftLimit;

        public CurrentAccount(string fullName, string nationalId, string phoneNumber, string address, double balance, decimal overdraftLimit)
            : base(fullName, nationalId, phoneNumber, address, balance)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override decimal CalculateInterest() => 0;

        public override void ShowAccountDetails()
        {
            base.ShowAccountDetails();
            Console.WriteLine($"Overdraft limit : {OverdraftLimit:C}");
        }

    }
}
