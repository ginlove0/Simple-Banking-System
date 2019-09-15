using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace _12656132
{
    public class Controller
    {
        private string userName;
        private string userPassword;
        private string[] readFile;
        View view = new View();
        Validation validation = new Validation();
        private Account account;
        public ArrayList accounts;

        public Controller()
        {
            //create a array list to save account
            account = new Account();
            accounts = new ArrayList();
        }

        public void Login()
        {
            Console.WriteLine("Welcome to simple Banking System");
            Console.WriteLine("Login Start");
            Console.WriteLine("User name: ");
            userName = Console.ReadLine();
            Console.WriteLine("Password: ");
            userPassword = Console.ReadLine();


            //open login.txt file and split data and check valid username and password
            string[] files = File.ReadAllLines("Login.txt");
            List<User> users = new List<User>();
            foreach (string file in files)
            {
                readFile = file.Split('|');
                User checkValid = new User { Username = readFile[0], Password = readFile[1] };
                users.Add(checkValid);
            }


            //fetch data input with data in file txt, if its not match, print error, if match, go to main menu
            User? userData = null;
            foreach (User user in users)
            {
                if (user.Username.Equals(userName) && user.Password.Equals(userPassword))
                {
                    userData = user;
                }
            }

            if (userData == null)
            {
                Console.WriteLine("Username or password are incorrect, please try again");
                Login();
            }
            else
            {
                //go to to main menu
                mainMenu();
            }
        }

        public void mainMenu()
        {
            string choose = view.mainMenuView();
            switch (choose)
            {
                case "1":
                    createAccount();
                    break;
                case "2":
                    searchAccount();
                    break;
                case "3":
                    deposit();
                    break;
                case "4":
                    withdraw();
                    break;
                case "5":
                    accountStatement();
                    break;
                case "6":
                    deleteAccount();
                    break;
                case "7":
                    Console.WriteLine("Thank you and see you again!");
                    break;
                default:
                    Console.WriteLine("Error, your option must be interge from 1 to 7. Please try again");
                    mainMenu();
                    break;
            }
        }

        private void createAccount()
        {
            //check validation for input data
            account.firstName = validation.input("First Name");
            account.lastName = validation.input("Last Name");
            account.address = validation.input("Address");
            account.phoneNumber = validation.isValidPhoneNumber("Phone Number");
            account.email = validation.isValidEmail();

            view.displayInformation(account);

            Console.WriteLine("Are you sure with this information?(y/n) ");

            //push option to check, only display yes or no
            string option = validation.confirmationCheck();

            switch (option)
            {
                case "y":
                    //push account number in random object from 8 to 10 numbers
                    Random random = new Random();
                    account.accountNumber = random.Next(100000, 99999999);

                    //create file name equals account number
                    string textFileName = Convert.ToString(account.accountNumber) + ".txt";
                    string[] createText = { account.firstName + "|" + account.lastName + "|" + account.address + "|" + account.phoneNumber + "|" + account.email +"|" + account.balance  };
                    File.WriteAllLines(textFileName, createText);

                    //add account to array list
                    accounts.Add(account);
                    Console.WriteLine("New account was created! Your cccount number is: {0}", account.accountNumber);
                    mainMenu();
                    break;
                case "n":
                    Console.WriteLine("Create account was cancled, go back to main menu");
                    mainMenu();
                    break;
            }
        }

        private Account searchAccount()
        {
            //check account number input
            int checkAccountNumber = validation.isValidAccountNumber("Your account number");
            Account foundAccount = searchFunction(checkAccountNumber);
            if(foundAccount != null)
            {
                //if found, display details account
                view.displayInformation(foundAccount);
                Console.WriteLine("Do you want to search another account?(y/n)");
                string choose = validation.confirmationCheck();
                switch (choose)
                {
                    case "y":
                        return searchAccount();

                    case "n":
                        this.mainMenu();
                        break;
                }

            }
            else
            {
                //if not ask to next step
                Console.WriteLine("Couldn't find this account!");
                Console.WriteLine("Do you want to try again?(y/n)");
                string choose = validation.confirmationCheck();
                switch (choose)
                {
                    case "y":
                        return searchAccount();
                        
                    case "n":
                        this.mainMenu();
                        break;
                }
            }
            return null;
        }

        private void deposit()
        {
            //check account number input
            int checkAccountNumber = validation.isValidAccountNumber("Your account number");
            Account getAccount = searchFunction(checkAccountNumber);
            if(getAccount != null)
            {
                //if found, get balance of this account plus with number input
                getAccount.balance += validation.isValidDecimal();
                Console.WriteLine("Deposit successful!");

                //save data to file with file name equal account number with new balance
                string file_name = Convert.ToString(getAccount.accountNumber) + ".txt";
                string[] createText = { getAccount.firstName + "|" + getAccount.lastName + "|" +
                        getAccount.address + "|" + getAccount.phoneNumber + "|" + getAccount.email + "|" + getAccount.balance };
                File.WriteAllLines(file_name, createText);
                accounts.Add(getAccount);


                Console.WriteLine("Do you want to deposit more?");
                string choose = validation.confirmationCheck();
                switch (choose)
                {
                    case "y":
                        deposit();
                        break;
                    case "n":
                        this.mainMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Couldn't find this account!");
                Console.WriteLine("Do you want to try again?(y/n)");
                string choose = validation.confirmationCheck();
                switch (choose)
                {
                    case "y":
                        deposit();
                        break;

                    case "n":
                        this.mainMenu();
                        break;
                }
            }
        }

        private void withdraw()
        {
            //check account number input
            int checkAccountNumber = validation.isValidAccountNumber("Your account number");
            Account getAccount = searchFunction(checkAccountNumber);
            if (getAccount != null)
            {
                //check valid of number input
                decimal checkWithdraw = validation.isValidDecimal();
                if (getAccount.balance < checkWithdraw)
                {
                    //if number withdraw larges then balance, run error
                    Console.WriteLine("Error, Your money is not enough. It should be less than {0}", getAccount.balance);
                    Console.WriteLine("Do you want to try again?");
                    string choose = validation.confirmationCheck();
                    switch (choose)
                    {
                        case "y":
                            withdraw();
                            break;
                        case "n":
                            mainMenu();
                            break;
                    }
                }
                else
                {
                    //otherwise, balance minus number withdraw and save new balance to file with file name equals account number
                    getAccount.balance -= checkWithdraw;
                    Console.WriteLine("Withdraw successful!");
                    string file_name = Convert.ToString(getAccount.accountNumber) + ".txt";
                    string[] createText = { getAccount.firstName + "|" + getAccount.lastName + "|" +
                        getAccount.address + "|" + getAccount.phoneNumber + "|" + getAccount.email + "|" + getAccount.balance };
                    File.WriteAllLines(file_name, createText);
                    accounts.Add(getAccount);


                    Console.WriteLine("Do you want to withdraw more?(y/n)");
                    string choose = validation.confirmationCheck();
                    switch (choose)
                    {
                        case "y":
                            withdraw();
                            break;
                        case "n":
                            mainMenu();
                            break;
                    }
                }
            }
            else
            {
                //if not found account, running error
                Console.WriteLine("Couldn't find this account!");
                Console.WriteLine("Do you want to try again?(y/n)");
                string choose = validation.confirmationCheck();
                switch (choose)
                {
                    case "y":
                        withdraw();
                        break;

                    case "n":
                        mainMenu();
                        break;
                }
            }
        }

        private void accountStatement()
        {
            int checkAccountNumber = validation.isValidAccountNumber("Your account number");
            Account getAccount = searchFunction(checkAccountNumber);
            if (getAccount != null)
            {
                view.displayStatement(getAccount);
                Console.WriteLine("Email Statement (y/n)?");
                string choose = validation.confirmationCheck();
                switch (choose)
                {
                    case "y":
                        Console.WriteLine("Email sent");
                        mainMenu();
                        break;
                    case "n":
                        this.mainMenu();
                        break;
                }
            }
            else
            {
                //if not found account, running error
                Console.WriteLine("Couldn't find this account!");
                Console.WriteLine("Do you want to try again?(y/n)");
                string choose = validation.confirmationCheck();
                switch (choose)
                {
                    case "y":
                        accountStatement();
                        break;
                    case "n":
                        this.mainMenu();
                        break;
                }
            }
        }

        private void deleteAccount()
        {
            //check account number input
            int checkAccountNumber = validation.isValidAccountNumber("Your account number");
            Account getAccount = searchFunction(checkAccountNumber);
            if(getAccount != null)
            {
                //if found account, display details of account
                Console.WriteLine("Account found! Details display below...");
                view.displayInformation(getAccount);
                Console.WriteLine("Delete? (y/n)");
                string choose = validation.confirmationCheck();
                switch (choose)
                {
                    case "y":
                        //delete account
                        string file_name = getAccount.accountNumber + ".txt";
                        File.Delete(file_name);
                        Console.WriteLine("Deleted account!");
                        mainMenu();
                        break;
                    case "n":
                        Console.WriteLine("Canceled!");
                        mainMenu();
                        break;
                }

            }
            else
            {
                //if not found account, running error
                Console.WriteLine("Couldn't find this account!");
                Console.WriteLine("Do you want to try again?(y/n)");
                string choose = validation.confirmationCheck();
                switch (choose)
                {
                    case "y":
                        deleteAccount();
                        break;
                    case "n":
                        this.mainMenu();
                        break;
                }
            }
        }


        public Account searchFunction(int accNumber)
        {
            try
            {
                //search file with input file name
                string searchFile = accNumber + ".txt";
                string[] files = File.ReadAllLines(searchFile);
                foreach (string file in files)
                {
                    readFile = file.Split('|');
                }
                Account validAcc = new Account
                {
                    //read all data in this file
                    accountNumber = accNumber,
                    firstName = readFile[0],
                    lastName = readFile[1],
                    address = readFile[2],
                    phoneNumber = Int32.Parse(readFile[3]),
                    email = readFile[4],
                    balance = Decimal.Parse(readFile[5])
                };
                return validAcc;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            
        }

    }
}
