using System;
using System.Collections.Generic;
using System.Text;

namespace SkyhoshiLinkedInLibrary.Extensions.Console
{
    public static class ConsoleExtensions
    {
        public static bool IsConsoleAvailable()
        { 
            try
            {
                int window_height = System.Console.WindowHeight;
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
