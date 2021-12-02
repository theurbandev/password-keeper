using System;
using Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using MenuSelector;


namespace password_keeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Greetings();
        }

        static void Greetings()
        {
            string userName;
            bool password = accountExist();
            string[] greetingsArr = { "Good Morning, ", "Good Afternoon, ", "Good Evening, " };

            if (password)
            {
                userActiveGreeting(password);
            }
            else
            {
                Console.WriteLine("----- Welcome to password manager. One applicaiton to manange all your passwords! -----");
                Console.WriteLine("What is your name: ");
                userName = Console.ReadLine();

                // Gets string retuned from getTimeOfDay method.
                string timeOfDay = getTimeOfDay();

                // Assign greeting based on timeOfDay value returned.
                string dayTypeCode = timeOfDay.Substring(timeOfDay.Length - 2);
                string DigitTime = timeOfDay.Substring(0, 2);

                if (dayTypeCode == "AM")
                {
                    Console.WriteLine("\n" + greetingsArr[0] + userName + '.');
                }
                else if (dayTypeCode == "PM")
                {
                    Console.WriteLine("\n" + greetingsArr[2] + userName + '.');
                }

                userActiveGreeting(password);
            }
        }

        static void userActiveGreeting(bool password)
        {
            // Password setup
            if (password)
            {
                //query to check if there is an account under users table
                Console.WriteLine("Welcome back,");

                Console.WriteLine("What is your username:");
                string usernameSubmited = Console.ReadLine();

                Console.WriteLine("\n" + "What is your password?");
                string passwordSubmited = Console.ReadLine();

                bool pwd_user_valid = validateCreds(passwordSubmited, usernameSubmited);

                while (!pwd_user_valid)
                {
                    Console.WriteLine("__________");
                    Console.WriteLine("Wrong credentials. Try again.." + "\n");

                    Console.WriteLine("Username:");
                    usernameSubmited = Console.ReadLine();

                    Console.WriteLine("Password:");
                    passwordSubmited = Console.ReadLine();
                    pwd_user_valid = validateCreds(passwordSubmited, usernameSubmited);
                }

                //run main menu
                MenuSelector.MenuSelectorClass.runMainMenu();
            }
            else
            {
                string pwd = passwordCreation();
                string username = usernameCreation();

                Console.WriteLine("\n" + "Username is: " + username);
                Console.WriteLine("Password is: " + pwd);

                accountDB_save(username, pwd);
            }
        }

        static bool validateCreds(string passwordSubmitted, string usernameSubmited)
        {
            var dbCon = DBConnection.Instance();
            if (dbCon.IsConnect())
            {
                string query = $"SELECT user_name, user_password FROM USERS";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.ExecuteNonQuery();

                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    object db_username = rdr[0];
                    object db_password = rdr[1];

                    if (passwordSubmitted == db_password.ToString() && usernameSubmited == db_username.ToString())
                    {
                        rdr.Close();
                        return true;
                    }
                    else
                    {
                        rdr.Close();
                        return false;
                    }
                }
                rdr.Close();
            }
            return false;
        }

        static bool accountExist()
        {
            //check if an account exisit in users table
            var dbCon = DBConnection.Instance();
            if (dbCon.IsConnect())
            {
                string query = $"SELECT id FROM USERS";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.ExecuteNonQuery();

                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    object data = rdr[0];
                    if (data != null)
                    {
                        rdr.Close();
                        return true;
                    }
                    else
                    {
                        rdr.Close();
                        return false;
                    }
                }
                rdr.Close();
            }
            else
            {
                Console.WriteLine("Connection failed...Data not saved");
            }

            return false;
        }

        static string getTimeOfDay()
        {
            string timeOfDay;
            string currentDateTime = DateTime.Now.ToString("h:mm:ss tt");

            // extracts time (including am/pm from dateTime as a string 
            timeOfDay = currentDateTime; 

            return timeOfDay; 
        }

        static string passwordCreation()
        {
            Console.WriteLine("Looks like this is your first time using this application." + "\n" + "It's time to setup your password: ");
            string userPassword = Console.ReadLine();

            return userPassword;
        }

        static string usernameCreation()
        {
            Console.WriteLine("\n" + "What would you like your username to be: ");
            string userName = Console.ReadLine();

            return userName;
        }

        static void accountDB_save(string username, string password)
        {
            Console.WriteLine("\n" + "Connecting to database...");

            var dbCon = DBConnection.Instance();
            if (dbCon.IsConnect())
            {
                Console.WriteLine("Saving data...");

                string query = $"INSERT INTO USERS(user_name, user_password) VALUES ('{username}', '{password}')";
                var cmd = new MySqlCommand(query, dbCon.Connection);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Data saved...");

                //clear the screen
                Console.WriteLine("Clearing the screen...");
                Console.Clear();
                Greetings();
            }
            else
            {
                Console.WriteLine("Connection failed...Data not saved");
            }
        }
    }
}