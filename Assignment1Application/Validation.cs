using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace _12656132
{
    public class Validation
    {
        public string input(string value)
        {
            //check valid of data input
            Console.WriteLine("{0}", value);
            string getValue = Console.ReadLine();
            if (getValue.Length > 0)
            {
                return getValue;
            }
            Console.WriteLine("{0} should not be blank!", value);
            return input(value);
        }

        public User checkLogin()
        {
            string usernameInput = input("User Name:");
            Console.WriteLine("Password:");

            string passwordInput = null;
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                passwordInput += key.KeyChar;
            }
            //create new user with data from input
            User userInput = new User
            {
                Username = usernameInput,
                Password = passwordInput
            };

            return userInput;
        }

        public string isValidEmail()
        {
            //check valid of email input
            Console.WriteLine("Enter your email:");
            string email = Console.ReadLine();
            if (email.Length > 0)
            {
                try
                {
                    MailAddress ma = new MailAddress(email);
                    return email;
                }
                catch (Exception)
                {
                    Console.WriteLine("Error, This is not format of email. Please try again");
                    return isValidEmail();
                }
            }
            Console.WriteLine("Email should not be blank!");
            return isValidEmail();
        }

        public int isValidPhoneNumber(string value)
        {
            //check valid of phone number input
            Console.WriteLine("Enter your phone number.");
            string telNo = Console.ReadLine();
            try
            {
                if (!Regex.IsMatch(telNo, @"\d{8,10}"))
                    throw new FormatException();

                return Int32.Parse(telNo);
            }
            catch (FormatException)
            {
                Console.WriteLine("{0} is not valid, please try again", value);
                return this.isValidPhoneNumber(value);
            }
            catch (OverflowException)
            {
                Console.WriteLine("This {0} is invalid, please try again", value);
                return this.isValidPhoneNumber(value);
            }
        }

        public string confirmationCheck()
        {
            //return yes no option
            string value = Console.ReadLine();
            switch (value)
            {
                case "y":
                    return value;
                case "n":
                    return value;
                default:
                    Console.WriteLine("Please only choose y or n");
                    return confirmationCheck();
            }
        }

        
        public int isValidAccountNumber(string value)
        {
            //check valid of account number
            Console.WriteLine("Enter your account number.");
            string accNo = Console.ReadLine();
            try
            {
                if (!Regex.IsMatch(accNo, @"\d{6,8}"))
                    throw new FormatException();
                return Int32.Parse(accNo);
            }
            catch (FormatException)
            {
                Console.WriteLine("{0} is not valid, it should be integer and from 6 to 8 digits, please try again", value);
                return this.isValidAccountNumber(value);
            }
            catch (OverflowException)
            {
                Console.WriteLine("{0} should be from 6 to 8 digits.", value);
                return this.isValidAccountNumber(value);
            }
        }

        public decimal isValidDecimal()
        {
            //check valid of balance input
            Console.WriteLine("Please enter an amount: $");
            string amount = Console.ReadLine();
            try
            {
                decimal x = Decimal.Parse(amount);
                if (x < 0)
                    throw new FormatException();

                return x;
            }
            catch (FormatException)
            {
                Console.WriteLine("Your amount is invalid, please try again" );
                return this.isValidDecimal();
            }
            catch (OverflowException)
            {
                Console.WriteLine("Your amount is too large, please try again");
                return this.isValidDecimal();
            }
        }
    }
}
