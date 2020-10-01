using System;
using System.Collections.Generic;
using System.Text;

namespace TextGameCuevaCristales.Models
{
    public static class ConsoleAdditions
    {
        public static bool AskYesOrNo(string message)
        {
            Console.WriteLine(message);
            while (true)
            {
                Console.Write("Введите 'yes' или 'no': ");
                string answer = Console.ReadLine();
                if (answer == "yes")
                {
                    return true;
                }
                else if (answer == "no")
                {
                    return false;
                }
            }
        }
    }
}
