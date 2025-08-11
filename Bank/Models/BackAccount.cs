using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models
{
    internal class BankAccount
    {
        public const string BankCode = "BNK001";
        private static int _accountCounter = 1000;

        public readonly DateTime CreateDate;
        private int _accountNumber;
        private string _fullName;
        private string _nationalID;
        private string _phoneNumber;
        private string _address;
        protected double _balance;


        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Full name cannot be empty.");
                _fullName = value;
            }
        }

        public string NationalID
        {
            get { return _nationalID; }
            set
            {
                if (value.Length != 14 || !long.TryParse(value, out _))
                    throw new ArgumentException("National ID must Be 14 numbers.");
                _nationalID = value;
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (value.Length != 11 || !value.StartsWith("01") || !long.TryParse(value, out _))
                    throw new ArgumentException("Phone number must be 11 digits and start with '01'.");
                _phoneNumber = value;
            }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public double Balance
        {
            get { return _balance; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Balance cannot be negative.");
                _balance = value;
            }
        }

        public BankAccount()
        {
            _accountNumber = _accountCounter++;
            CreateDate = DateTime.Now;
            FullName = "User Name";
            NationalID = "00000000000000";
            PhoneNumber = "01000000000";
            Address = "User Address";
            Balance = 0;
        }

        public BankAccount(string fullName, string nationalId, string phoneNumber, string address, double balance)
        {
            _accountNumber = _accountCounter++;
            CreateDate = DateTime.Now;
            FullName = fullName;
            NationalID = nationalId;
            PhoneNumber = phoneNumber;
            Address = address;
            Balance = balance;
        }

        public BankAccount(string fullName, string nationalId, string phoneNumber, string address)
            : this(fullName, nationalId, phoneNumber, address, 0)
        {
        }

        public virtual void ShowAccountDetails()
        {
            Console.WriteLine("==== Account Details ====");
            Console.WriteLine($"Bank Code      : {BankCode}");
            Console.WriteLine($"Account Number : {_accountNumber}");
            Console.WriteLine($"Full Name      : {FullName}");
            Console.WriteLine($"National ID    : {NationalID}");
            Console.WriteLine($"Phone Number   : {PhoneNumber}");
            Console.WriteLine($"Address        : {Address}");
            Console.WriteLine($"Balance        : {Balance:C}");
            Console.WriteLine($"Created Date   : {CreateDate}");
           
        }

        public bool IsValidNationalID()
        {
            return NationalID.Length == 14 && long.TryParse(NationalID, out _);
        }

        public bool IsValidPhoneNumber()
        {
            return PhoneNumber.Length == 11 && PhoneNumber.StartsWith("01") && long.TryParse(PhoneNumber, out _);
        }

        public virtual decimal CalculateInterest() => 0;
    }

}
