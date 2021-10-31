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
            bool password = false;
            string[] greetingsArr = { "Good Morning, ", "Good Afternoon, ", "Good Evening, " };


            Console.WriteLine("What is your name...");
            userName = Console.ReadLine();


            // Gets string retuned from getTimeOfDay method.
            string timeOfDay = getTimeOfDay();

            // Assign greeting based on timeOfDay value returned.
            string dayTypeCode = timeOfDay.Substring(timeOfDay.Length - 2);
            string firstDigitTime = timeOfDay.Substring(0, 1);

            if (dayTypeCode == "AM")
            {
                Console.WriteLine("\n" + greetingsArr[0] + userName + '.');
            }
            else if (dayTypeCode == "PM" && int.Parse(firstDigitTime) < 5)
            {
                Console.WriteLine("\n" + greetingsArr[1] + userName + '.');
            }
            else if (dayTypeCode == "PM" && int.Parse(firstDigitTime) > 5)
            {
                Console.WriteLine("\n" + greetingsArr[2] + userName + '.');
            }

            // Password setup
            if (password){
                
            }else{
                accountCreation();
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

        static void accountCreation()
        {
            Console.WriteLine("Looks like this is your first time using this application." + "\n" + "It's time to setup your password: " + "\n");
            string userPassword = Console.ReadLine();
        }
    }
}
