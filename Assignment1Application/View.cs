using System;


namespace _12656132
{
    public class View
    {

        Validation validation = new Validation();

        public string mainMenuView()
        {
            Console.WriteLine("");
            Console.WriteLine("1. Create a new account");
            Console.WriteLine("2. Search for an account");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Withdraw");
            Console.WriteLine("5. A/C Statement");
            Console.WriteLine("6. Delete account");
            Console.WriteLine("7. Exit");
            Console.WriteLine("Please choose your option (1-7)");
            Console.WriteLine("");
            String option = Console.ReadLine();
            return option;
        }

        public void displayInformation(Account account)
        {
            Console.WriteLine("");
            Console.WriteLine("Account Details");
            Console.WriteLine("Your account number: ");
            Console.WriteLine("First Name: " + account.firstName);
            Console.WriteLine("Last Name: " + account.lastName);
            Console.WriteLine("Address: " + account.address);
            Console.WriteLine("Email: " + account.email);
            Console.WriteLine("Phone Number: " + account.phoneNumber);
            Console.WriteLine("");
        }

        public void displayStatement(Account account)
        {
            Console.WriteLine("");
            Console.WriteLine("Account Details");
            Console.WriteLine("Your account number: ");
            Console.WriteLine("Your balance: $ ");
            Console.WriteLine("First Name: " + account.firstName);
            Console.WriteLine("Last Name: " + account.lastName);
            Console.WriteLine("Address: " + account.address);
            Console.WriteLine("Email: " + account.email);
            Console.WriteLine("Phone Number: " + account.phoneNumber);
            Console.WriteLine("");
        }

        




    }
}
