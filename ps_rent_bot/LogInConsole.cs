using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_rent_bot
{
    public static class LogInConsole
    {
        public static void PrintException(string text)
        {

           
                Console.ForegroundColor = ConsoleColor.Red;
            
           
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
