using System;
using live_Back_account.Models;

namespace live_Back_account
{
    class Program
    {
        static void Main(string[] args)
        {    
            Console.WriteLine("Enter bank name:");
            string bankName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(bankName))
            {
                Console.WriteLine("Bank name cannot be empty. Using default name 'MyBank'.");
                bankName = "MyBank";
            }

            Console.WriteLine("Enter branch code:");
            int branchCode;
            while (!int.TryParse(Console.ReadLine(), out branchCode))
            {
                Console.WriteLine("Invalid branch code. Please enter a valid number:");
            }

          
            Bank bank = new Bank(branchCode, bankName);

            Customer customer = new Customer("Amer El Kholy", 123456789, new DateTime(1990, 1, 1));
            customer.AddAccount(new Saving());
            Customer customer2 = new Customer("khaled Hathoot", 987654321, new DateTime(1995, 5, 5));
            customer2.AddAccount(new Current());
            Customer customer3 = new Customer("Ali El Kholy", 111222333, new DateTime(2000, 10, 10));
            bank.AddCustomer(customer);
            bank.AddCustomer(customer2);
            bank.AddCustomer(customer3);



            while (true)
            {
                Console.WriteLine("\nBank System Menu:");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Update Customer");
                Console.WriteLine("3. Remove Customer");
                Console.WriteLine("4. Search Customers");
                Console.WriteLine("5. Add Account to Customer");
                Console.WriteLine("6. Deposit");
                Console.WriteLine("7. Withdraw");
                Console.WriteLine("8. Transfer");
                Console.WriteLine("9. Show Customer Total Balance");
                Console.WriteLine("10. Calculate Monthly Interest (Savings)");
                Console.WriteLine("11. Show Bank Report");
                Console.WriteLine("12. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": // إضافة عميل
                            Console.WriteLine("Enter full name:");
                            string name = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(name))
                            {
                                Console.WriteLine("Name cannot be empty.");
                                break;
                            }

                            Console.WriteLine("Enter national ID:");
                            int nid;
                            while (!int.TryParse(Console.ReadLine(), out nid))
                            {
                                Console.WriteLine("Invalid National ID. Please enter a valid number:");
                            }

                            Console.WriteLine("Enter date of birth (yyyy-MM-dd):");
                            DateTime dob;
                            while (!DateTime.TryParse(Console.ReadLine(), out dob))
                            {
                                Console.WriteLine("Invalid date format. Please use yyyy-MM-dd:");
                            }

                            Customer newCustomer = new Customer(name, nid, dob);
                            bank.AddCustomer(newCustomer);
                            break;

                        case "2": // تحديث عميل
                            Console.WriteLine("Enter customer ID to update:");
                            int custIdUpdate;
                            while (!int.TryParse(Console.ReadLine(), out custIdUpdate))
                            {
                                Console.WriteLine("Invalid Customer ID. Please enter a valid number:");
                            }

                            Customer custUpdate = bank.FindCustomerById(custIdUpdate);
                            if (custUpdate != null)
                                custUpdate.Update();
                            else
                                Console.WriteLine("Customer not found.");
                            break;

                        case "3": // حذف عميل
                            Console.WriteLine("Enter customer ID to remove:");
                            int custIdRemove;
                            while (!int.TryParse(Console.ReadLine(), out custIdRemove))
                            {
                                Console.WriteLine("Invalid Customer ID. Please enter a valid number:");
                            }

                            Customer custRemove = bank.FindCustomerById(custIdRemove);
                            if (custRemove != null)
                                bank.RemoveCustomer(custRemove);
                            else
                                Console.WriteLine("Customer not found.");
                            break;

                        case "4": // البحث عن عملاء
                            Console.WriteLine("Enter search term (name or national ID):");
                            string search = Console.ReadLine();
                            var results = bank.SearchCustomers(search);
                            if (results.Count == 0)
                                Console.WriteLine("No customers found.");
                            else
                            {
                                foreach (var res in results)
                                {
                                    res.ShowDetails();
                                    Console.WriteLine();
                                }
                            }
                            break;

                        case "5": // إضافة حساب لعميل
                            Console.WriteLine("Enter customer ID:");
                            int custIdAcc;
                            while (!int.TryParse(Console.ReadLine(), out custIdAcc))
                            {
                                Console.WriteLine("Invalid Customer ID. Please enter a valid number:");
                            }

                            Customer custAcc = bank.FindCustomerById(custIdAcc);
                            if (custAcc == null)
                            {
                                Console.WriteLine("Customer not found.");
                                break;
                            }

                            Console.WriteLine("Account type: 1. Savings 2. Current");
                            string accType = Console.ReadLine();
                            Account newAcc;
                            if (accType == "1")
                                newAcc = new Saving();
                            else if (accType == "2")
                                newAcc = new Current();
                            else
                            {
                                Console.WriteLine("Invalid account type.");
                                break;
                            }
                            custAcc.AddAccount(newAcc);
                            break;

                        case "6": // إيداع
                            Console.WriteLine("Enter account number:");
                            int accNumDep;
                            while (!int.TryParse(Console.ReadLine(), out accNumDep))
                            {
                                Console.WriteLine("Invalid account number. Please enter a valid number:");
                            }

                            Account accDep = bank.FindAccountByNumber(accNumDep);
                            if (accDep == null)
                            {
                                Console.WriteLine("Account not found.");
                                break;
                            }

                            Console.WriteLine("Enter deposit amount:");
                            double depAmt;
                            while (!double.TryParse(Console.ReadLine(), out depAmt))
                            {
                                Console.WriteLine("Invalid amount. Please enter a valid number:");
                            }
                            accDep.Depositing(depAmt);
                            break;

                        case "7": // سحب
                            Console.WriteLine("Enter account number:");
                            int accNumWith;
                            while (!int.TryParse(Console.ReadLine(), out accNumWith))
                            {
                                Console.WriteLine("Invalid account number. Please enter a valid number:");
                            }

                            Account accWith = bank.FindAccountByNumber(accNumWith);
                            if (accWith == null)
                            {
                                Console.WriteLine("Account not found.");
                                break;
                            }

                            Console.WriteLine("Enter withdrawal amount:");
                            double withAmt;
                            while (!double.TryParse(Console.ReadLine(), out withAmt))
                            {
                                Console.WriteLine("Invalid amount. Please enter a valid number:");
                            }
                            accWith.Withdrawing(withAmt);
                            break;

                        case "8": // تحويل
                            Console.WriteLine("Enter from account number:");
                            int fromAccNum;
                            while (!int.TryParse(Console.ReadLine(), out fromAccNum))
                            {
                                Console.WriteLine("Invalid account number. Please enter a valid number:");
                            }

                            Account fromAcc = bank.FindAccountByNumber(fromAccNum);
                            Console.WriteLine("Enter to account number:");
                            int toAccNum;
                            while (!int.TryParse(Console.ReadLine(), out toAccNum))
                            {
                                Console.WriteLine("Invalid account number. Please enter a valid number:");
                            }

                            Account toAcc = bank.FindAccountByNumber(toAccNum);
                            if (fromAcc == null || toAcc == null)
                            {
                                Console.WriteLine("One or both accounts not found.");
                                break;
                            }

                            Console.WriteLine("Enter transfer amount:");
                            double transAmt;
                            while (!double.TryParse(Console.ReadLine(), out transAmt))
                            {
                                Console.WriteLine("Invalid amount. Please enter a valid number:");
                            }
                            bank.Transfer(fromAcc, toAcc, transAmt);
                            break;

                        case "9": // عرض إجمالي رصيد العميل
                            Console.WriteLine("Enter customer ID:");
                            int custIdBal;
                            while (!int.TryParse(Console.ReadLine(), out custIdBal))
                            {
                                Console.WriteLine("Invalid Customer ID. Please enter a valid number:");
                            }

                            Customer custBal = bank.FindCustomerById(custIdBal);
                            if (custBal != null)
                                Console.WriteLine($"Total Balance: {bank.GetTotalBalance(custBal)}");
                            else
                                Console.WriteLine("Customer not found.");
                            break;

                        case "10": // حساب الفائدة الشهرية (حساب توفير)
                            Console.WriteLine("Enter savings account number:");
                            int savAccNum;
                            while (!int.TryParse(Console.ReadLine(), out savAccNum))
                            {
                                Console.WriteLine("Invalid account number. Please enter a valid number:");
                            }

                            Saving savAcc = bank.FindSavingsAccountByNumber(savAccNum);
                            if (savAcc != null)
                                savAcc.CalculateMonthlyInterest();
                            else
                                Console.WriteLine("Savings account not found.");
                            break;

                        case "11": // عرض تقرير البنك
                            bank.ShowBankReport();
                            break;

                        case "12": // الخروج
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please select a number from 1 to 12.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}