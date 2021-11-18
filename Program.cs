using System;
using Data;
using MySql.Data;
using MySql.Data.MySqlClient;


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
            bool password = false;
            string[] greetingsArr = { "Good Morning, ", "Good Afternoon, ", "Good Evening, " };


            Console.WriteLine("What is your name...");
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
            /*else if (dayTypeCode == "PM" && int.Parse(DigitTime) < 5)
            {
                Console.WriteLine("\n" + greetingsArr[1] + userName + '.');
            } */
            else if (dayTypeCode == "PM")
            {
                Console.WriteLine("\n" + greetingsArr[2] + userName + '.');
            }
                        

            // Password setup
            if (password){
                //ask for password 
            }else{
                string pwd = passwordCreation();
                string username = usernameCreation();

                Console.WriteLine("\n" + "Username is: " + username);
                Console.WriteLine("Password is: " + pwd);

                accountDB_save();
            }
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
            Console.WriteLine("\n" + "What would you like your username to be?");
            string userName = Console.ReadLine();

            return userName;
        }

        static void accountDB_save()
        {
            Console.WriteLine("\n" + "Connecting to database...");

            var dbCon = DBConnection.Instance();
            if (dbCon.IsConnect())
            {
                Console.WriteLine("Saving data...");

                string query = "SELECT * FROM USERS";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string someStringFromColumnZero = reader.GetString(0);
                    Console.WriteLine("Users: " + reader);
                }
                dbCon.Close();

                Console.WriteLine("Data saved...");
            }
            else
            {
                Console.WriteLine("Connection failed...");
            }
        }
    }
}
