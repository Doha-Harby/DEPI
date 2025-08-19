using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace live_Back_account.Models
{
    internal abstract class Account 
    {
        protected static int _accCounter = 1;
        protected double _balance;
        protected DateTime _dateCreated;
        public int AccNumber { get; private set; }
        public double Balance { get { return _balance; } protected set { _balance = value; } }
        public DateTime DateCreated { get { return _dateCreated; } }

        public Account()
        {
            _dateCreated = DateTime.Now;
            _balance = 0;
            AccNumber = _accCounter++; //
        }

        public virtual double Depositing(double value)
        {
            if (value <= 0)
            {
                Console.WriteLine("Deposit amount must be positive.");
                return Balance;
            }
            Balance += value;
            Console.WriteLine($"Deposited {value}. New balance: {Balance}");
            return Balance;
        }

        public virtual double Withdrawing(double value)
        {
            if (value <= 0)
            {
                Console.WriteLine("Withdrawal amount must be positive.");
                return Balance;
            }
            if (value > Balance)
            {
                Console.WriteLine("Insufficient balance.");
                return Balance;
            }
            Balance -= value;
            Console.WriteLine($"Withdrawn {value}. New balance: {Balance}");
            return Balance;
        }

        public virtual void ShowDetails()
        {
            Console.WriteLine($"Account Number: {AccNumber}");
            Console.WriteLine($"Balance: {Balance}");
            Console.WriteLine($"Date Created: {_dateCreated}");
        }
    }
}