using System;

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
            string[] greetingsArr = { "Good Morning, ", "Good Afternoon, " };


            Console.WriteLine("What is your name...");
            userName = Console.ReadLine();


            // Gets string retuned from getTimeOfDay method.
            string timeOfDay = getTimeOfDay();

            // Assign greeting based on timeOfDay value returned.
            string dayTypeCode = timeOfDay.Substring(timeOfDay.Length - 2);
            if(dayTypeCode == "AM")
            {
                Console.WriteLine("\n" + greetingsArr[0] + userName + '.');
            }else if(dayTypeCode == "PM")
            {
                Console.WriteLine("\n" + greetingsArr[1] + userName + '.');
            }

            // Password setup
            Console.WriteLine("Looks like this is your first time using this application." + "\n" + "It's time to setup your password: " + "\n");
            string userPassword = Console.ReadLine();

            // Account creation
            accountCreation();
        }

        static string getTimeOfDay()
        {
            string timeOfDay;
            DateTime currentDateTime = new DateTime();

            // extracts time (including am/pm from dateTime as a string 
            timeOfDay = currentDateTime.ToShortTimeString(); 

            return timeOfDay; 
        }

        static void accountCreation()
        {

        }
    }
}
