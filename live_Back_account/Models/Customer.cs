using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace live_Back_account.Models
{
    internal class Customer
    {
        private string _fullName;
        private int _nationalID; 
        private DateTime _dateOfBirth;
        private static int _custCounter = 0;
        public int CustomerID { get; private set; }
        public string FullName { get; set; }
        public int NationalID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Account> CustomerAccounts { get; } = new List<Account>();

        public Customer(string fullName, int nationalID, DateTime dateOfBirth)
        {
            CustomerID = ++_custCounter;
            FullName = fullName;
            NationalID = nationalID;
            DateOfBirth = dateOfBirth;
        }

        public void AddAccount(Account account)
        {
            CustomerAccounts.Add(account);
            Console.WriteLine($"Account {account.AccNumber} added to customer {FullName}.");
        }

        public void Update()
        {
            Console.WriteLine("What do you want to update? \n1: Name\n2: National ID\n3: Date of Birth");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter new name:");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        string oldName = _fullName; 
                        FullName = newName; 
                        Console.WriteLine($"Name updated. Old value: {oldName}, New value: {newName}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Name cannot be empty.");
                    }
                    break;

                case "2":
                    Console.WriteLine("Enter new National ID:");
                    if (int.TryParse(Console.ReadLine(), out int newNationalID) && newNationalID > 0)
                    {
                        int oldNationalID = _nationalID; 
                        NationalID = newNationalID; 
                        Console.WriteLine($"National ID updated. Old National ID: {oldNationalID}, New National ID: {newNationalID}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. National ID must be a positive number.");
                    }
                    break;

                case "3":
                    Console.WriteLine("Enter new Date of Birth (yyyy-MM-dd):");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime newDOB) && newDOB <= DateTime.Now)
                    {
                        DateTime oldDOB = _dateOfBirth;
                        DateOfBirth = newDOB; 
                        Console.WriteLine($"Date of Birth updated. Old date of birth: {oldDOB.ToString("yyyy-MM-dd")}, New date of birth: {newDOB.ToString("yyyy-MM-dd")}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid date. Date of birth must be in yyyy-MM-dd format and not in the future.");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        public void ShowDetails()
        {
            Console.WriteLine($"Customer ID: {CustomerID}");
            Console.WriteLine($"Full Name: {FullName}");
            Console.WriteLine($"National ID: {NationalID}");
            Console.WriteLine($"Date of Birth: {DateOfBirth.ToShortDateString()}");
            Console.WriteLine("Accounts:");
            foreach (var account in CustomerAccounts)
            {
                account.ShowDetails();
            }
        }
    }
}