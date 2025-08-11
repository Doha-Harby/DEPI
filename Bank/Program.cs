using Bank.Models;

namespace Bank
{
    public class Program
    {
        public static void Main()
        {
            //objects of Back Account class
            //BankAccount account1 = new BankAccount();
            //account1.ShowAccountDetails();


            //BankAccount account2 = new BankAccount(
            //    fullName: "Doha Harby",
            //    nationalId: "29811234567890",
            //    phoneNumber: "01012345678",
            //    address: "Cairo, Egypt",
            //    balance: 5000
            //);
            //account2.ShowAccountDetails();


            //Console.WriteLine($"Is Account 2 National ID Valid? {account2.IsValidNationalID()}");
            //Console.WriteLine($"Is Account 2 Phone Number Valid? {account2.IsValidPhoneNumber()}");


            // objects of Saving Account
            SavingAccount saving = new SavingAccount(
                fullName: "Doha Harby",
                nationalId: "29811234567890",
                phoneNumber: "01012345678",
                address: "Cairo, Egypt",
                balance: 10000,
                interestRate: 5
            );

            // objects of  Current Account
            CurrentAccount current = new CurrentAccount(
                fullName: "Omar Hassan",
                nationalId: "29911234567891",
                phoneNumber: "01098765432",
                address: "Giza, Egypt",
                balance: 5000,
                overdraftLimit: 2000
            );

            List<BankAccount> accounts = new List<BankAccount>()
            {
                saving, current
            };

            foreach (var acc in accounts)
            {
                acc.ShowAccountDetails();
                Console.WriteLine($"Interest Amount: {acc.CalculateInterest():C}");
                Console.WriteLine("\n");
            }
        }
    }

    
}