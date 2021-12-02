// Used to give user options after authentication //

using System;
using System.Linq;
using Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MenuSelector
{
    public partial class MenuSelectorClass
    {
        public static void runMainMenu()
        {
            Console.Clear();

            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));

                Console.WriteLine("Please make a selection: ");
                Console.WriteLine("1. Add new account information.");
                Console.WriteLine("2. Search by email.");
                Console.WriteLine("3. Search by password.");
                Console.WriteLine("4. EXIT.");

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
                    Console.WriteLine("\n" + "SEARCH BY EMAIL");
                    break;
                case "3":
                    Console.WriteLine("\n" + "SEARCH BY PASSWORD");
                    break;
                case "4":
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

                    Console.WriteLine("Data saved...");
                    System.Threading.Thread.Sleep(500);

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
    } 
}
