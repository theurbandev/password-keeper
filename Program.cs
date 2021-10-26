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
            string[] greetingsArr = { "Good Morning, ", "Good Afternoon, ", "Good Evening, " };


            Console.WriteLine("What is your name...");
            userName = Console.ReadLine();


            // Serve custom greeting based on time of day.
            string timeOfDay = getTimeOfDay();

            // Assign greeting based on timeOfDay value returned.
            

            Console.WriteLine("\n" + greetingsArr[0] + userName + '.');
            Console.WriteLine(timeOfDay);
        }

        static string getTimeOfDay()
        {
            string timeOfDay;
            DateTime currentDateTime = new DateTime();

            // extracts time (including am/pm from dateTime as a string 
            timeOfDay = currentDateTime.ToShortTimeString(); 
;
            return timeOfDay; 
        }
    }
}
