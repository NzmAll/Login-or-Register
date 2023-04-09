using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Reflection.Metadata;

namespace RegistrationAndLoginSystem
{
    class Program
    {
        static List<User> users = new List<User>();
        static User currentUser = null;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Registration and Login System!");
            Console.WriteLine("Commands:");
            Console.WriteLine("/register");
            Console.WriteLine("/login");
            Console.WriteLine("/exit");

            // Add Super Admin as default user
            users.Add(new User("Super", "Admin", "admin@gmail.com", "123321", true));

            while (true)
            {
                Console.Write("Enter a command: ");
                string command = Console.ReadLine()!;

                switch (command)
                {
                    case "/register":
                        Register();
                        break;
                    case "/login":
                        Login();
                        break;
                    case "/exit":
                        Console.WriteLine("Exited the program");
                        return;
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
            }
        }

        static void Register()
        {
            Console.WriteLine("Registration form:");
            Console.Write("First Name: ");
            string firstName = Console.ReadLine()!;

            // Validate first name
            if (firstName.Length < 3 || firstName.Length > 30)
            {
                Console.WriteLine("First name must be between 3 and 30 characters long!");
                return;
            }

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine()!;

            if (lastName.Length < 5 || lastName.Length > 20)
            {
                Console.WriteLine("Last name must be between 5 and 20 characters long!");
                return;
            }

            Console.Write("Email Address: ");
            string email = Console.ReadLine()!;

            if (!IsValidEmail(email))
            {
                Console.WriteLine("Invalid email address!");
                return;
            }

            Console.Write("Password: ");
            string password = Console.ReadLine()!;

            Console.Write("Confirm Password: ");
            string confirmPassword = Console.ReadLine()!;

            if (password != confirmPassword)
            {
                Console.WriteLine("Passwords do not match!");
                return;
            }

            if (UserExists(email))
            {
                Console.WriteLine("User with this email already exists!");
                return;
            }

            User user = new User(firstName, lastName, email, password, false);
            users.Add(user);

            Console.WriteLine("You successfully registered, now you can login with your new account!");
        }

        static void Login()
        {
            Console.Write("Email Address: ");
            string email = Console.ReadLine()!;

            Console.Write("Password: ");
            string password = Console.ReadLine()!;

            User user = FindUser(email, password);

            if (user == null)
            {
                Console.WriteLine("Invalid email or password!");
            }
            else
            {
                currentUser = user;
                Console.WriteLine("Welcome to your account!", currentUser.FirstName, currentUser.LastName);
                if (currentUser.IsAdmin)
                {
                    Console.WriteLine("Welcome to your account!", currentUser.FirstName, currentUser.LastName);
                }
            }
        }

        static bool IsValidEmail(string email)
        {
            if (email == null)
            {
                return false;
            }

            foreach (char c in email)
            {
                if (c == '\"' || c == '(' || c == ')' || c == ',' || c == ':' || c == ';' || c == '<' || c == '>' || c == '@' || c == '[' || c == '\\' || c == ']' || c == ' ')
                {
                    return false;
                }
            }
            if (email.IndexOf('@') != email.LastIndexOf('@'))
            {
                return false;
            }

            int dotIndex = email.LastIndexOf('.');
            if (dotIndex == -1 || dotIndex < email.IndexOf('@'))
            {
                return false;
            }

            return true;
        }

        static User FindUser(string email, string password)
        {
            foreach (User user in users)
            {
                if (user.Email == email && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }
    }

    class User
    {
        public string FirstName;
        public string LastName;
        public string Email;
        public string Password;
        public bool IsAdmin;

        public User(string firstName, string lastName, string email, string password, bool isAdmin)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            IsAdmin = isAdmin;
        }
    }

}
