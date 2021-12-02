// Used to give user options after authentication //

using System;
using System.Linq;
using System.Collections.Generic;
using Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MenuSelector
{
    public class MenuSelectorClass
    {
        public static string servciveName { get; set; }
        public static string email { get; set; }
        public static string username { get; set; }
        public static string password { get; set; }

        public static void runMainMenu()
        {
            //Console.Clear();

            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));

                Console.WriteLine("Please make a selection: ");
                Console.WriteLine("1. Add new account information.");
                Console.WriteLine("2. Search by website/service name.");
                Console.WriteLine("3. Search by email.");
                Console.WriteLine("4. Search by password.");
                Console.WriteLine("5. EXIT.");

            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));

            // check users selection
            string userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    Console.WriteLine("\n" + "ADD NEW ACCOUNT INFORMATION");
                    AddNewAccount();
                    break;
                case "2":
                    Console.WriteLine("\n" + "SEARCH BY WEBSITE/SERVICE NAME");
                    SearchEngine("service");
                    break;
                case "3":
                    Console.WriteLine("\n" + "SEARCH BY EMAIL");
                    SearchEngine("email");
                    break;
                case "4":
                    Console.WriteLine("\n" + "SEARCH BY PASSWORD");
                    SearchEngine("password");
                    break;
                case "5":
                    return;
                default:
                    runMainMenu();
                    break;
            }
        }

        public static void AddNewAccount()
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));

                Console.WriteLine("Account/Service name:");
                string acc_name = Console.ReadLine();

                Console.WriteLine("\n" + "Account/Service email:");
                string acc_email = Console.ReadLine();

                Console.WriteLine("\n" + "Account/Service username:");
                string acc_username = Console.ReadLine();

                Console.WriteLine("\n" + "Account/Service password:");
                string acc_password = Console.ReadLine();

            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)) + "\n");


            //confirm details
            Console.WriteLine("Account/Service name " + $"[ {acc_name} ]" );
            Console.WriteLine("Account/Service username " + $"[ {acc_username} ]");
            Console.WriteLine("Account/Service email " + $"[ {acc_email} ]");
            Console.WriteLine("Account/Service password " + $"[ {acc_password} ]");

            Console.WriteLine("\n" + "Everything look good? (Y) or (N)");
            char answer = Console.ReadLine()[0];


            //save data as new record
            if (answer == 'y' || answer == 'Y')
            {
                //db call
                var dbCon = DBConnection.Instance();
                if (dbCon.IsConnect())
                {
                    Console.WriteLine("Saving data...");

                    string query = $"INSERT INTO ACCOUNTS(serviceName, email, username, password) VALUES ('{acc_name}', '{acc_email}', '{acc_username}', '{acc_password}')";
                    var cmd = new MySqlCommand(query, dbCon.Connection);

                    cmd.ExecuteNonQuery();

                    Console.Clear();
                    Console.WriteLine("Data saved...");
                    System.Threading.Thread.Sleep(800);

                    runMainMenu();
                }
                else
                {
                    Console.WriteLine("Connection failed...Data not saved");
                }
            }
            else
            {
                Console.Clear();
                AddNewAccount();
            }
        }

        public static void SearchEngine(string searchType)
        {
            string query = string.Empty;

            List<MenuSelectorClass> results = new List<MenuSelectorClass>();

            switch (searchType)
            {
                case "service":
                    Console.WriteLine("Service name: ");
                    string serviceName = Console.ReadLine();
                    query = $"SELECT serviceName, email, username, password FROM accounts WHERE serviceName LIKE '{serviceName}'";
                    break;
                case "email":
                    Console.WriteLine("Email address: ");
                    string email = Console.ReadLine();
                    query = $"SELECT serviceName, email, username, password FROM accounts WHERE email LIKE '{email}'";
                    break;
                case "password":
                    Console.WriteLine("Password: ");
                    string password = Console.ReadLine();
                    query = $"SELECT serviceName, email, username, password FROM accounts WHERE password LIKE '{password}'";
                    break;
            }

            //db hit
            var dbCon = DBConnection.Instance();
            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.ExecuteNonQuery();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    MenuSelectorClass.servciveName = rdr[0].ToString();
                    MenuSelectorClass.email = rdr[1].ToString();
                    MenuSelectorClass.username = rdr[2].ToString();
                    MenuSelectorClass.password = rdr[3].ToString();

                    Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)) + "\n");
                    Console.WriteLine(MenuSelectorClass.servciveName);
                    Console.WriteLine(MenuSelectorClass.email);
                    Console.WriteLine(MenuSelectorClass.username);
                    Console.WriteLine(MenuSelectorClass.password);
                }
                rdr.Close();
                runMainMenu();
            }
        }
    } 
}
