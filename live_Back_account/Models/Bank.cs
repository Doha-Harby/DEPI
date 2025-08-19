using System;
using System.Collections.Generic;
using System.Text;

namespace live_Back_account.Models
{
    internal class Bank
    {
        private readonly int _branchCode;
        private readonly string _name;
        private List<Customer> Customers { get; } = new List<Customer>();

        public Bank(int branchCode, string name)
        {
            _branchCode = branchCode;
            _name = name;
        }

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                Console.WriteLine("Customer can not be null.");
                return;
            }
            Customers.Add(customer);
            Console.WriteLine($"Customer {customer.FullName} added,  with CustomerID: {customer.CustomerID}");
        }

        public void RemoveCustomer(Customer customer)
        {
            if (customer == null)
            {
                Console.WriteLine("Customer can not be null.");
                return;
            }

            foreach (var account in customer.CustomerAccounts)
            {
                if (account.Balance != 0)
                {
                    Console.WriteLine("Cannot remove customer with non-zero balance accounts.");
                    return;
                }
            }
            Customers.Remove(customer);
            Console.WriteLine($"Customer {customer.FullName} removed.");
        }

        public List<Customer> SearchCustomers(string searchText)
        {
            List<Customer> results = new List<Customer>();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                Console.WriteLine("Search term can not be empty.");
                return results;
            }

            foreach (var customer in Customers)
            {
                if (customer.FullName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    customer.NationalID.ToString().Contains(searchText))
                {
                    results.Add(customer);
                }
            }

            return results;
        }

        public Customer FindCustomerById(int customerId)
        {
            foreach (var customer in Customers)
            {
                if (customer.CustomerID == customerId)
                {
                    return customer;
                }
            }
            return null;
        }

        public double GetTotalBalance(Customer customer)
        {
            double total = 0;
            foreach (var account in customer.CustomerAccounts)
            {
                total += account.Balance;
            }
            return total;
        }

        public void ShowBankReport()
        {
            Console.WriteLine($"Bank Report for {_name} (Branch {_branchCode}):");
            foreach (var customer in Customers)
            {
                Console.WriteLine($"\nCustomer: {customer.FullName} (ID: {customer.CustomerID})");
                foreach (var account in customer.CustomerAccounts)
                {
                    account.ShowDetails();
                }
                Console.WriteLine($"Total Balance: {GetTotalBalance(customer)}");
            }
        }

        public void Transfer(Account fromAccount, Account toAccount, double amount)
        {
            if (fromAccount == null || toAccount == null)
            {
                Console.WriteLine("Accounts can not be null.");
                return;
            }
            if (fromAccount.Withdrawing(amount) != fromAccount.Balance)
            {
                return;
            }
            toAccount.Depositing(amount);
            Console.WriteLine($"Transferred {amount} from {fromAccount.AccNumber} to {toAccount.AccNumber}.");
        }

        public Account FindAccountByNumber(int accNumber)
        {
            foreach (var customer in Customers)
            {
                foreach (var account in customer.CustomerAccounts)
                {
                    if (account.AccNumber == accNumber)
                    {
                        return account;
                    }
                }
            }
            return null;
        }

        public Saving FindSavingsAccountByNumber(int accNumber)
        {
            foreach (var customer in Customers)
            {
                foreach (var account in customer.CustomerAccounts)
                {
                    if (account is Saving saving && account.AccNumber == accNumber)
                    {
                        return saving;
                    }
                }
            }
            return null;
        }
    }
}