using System;
using System.Collections.Generic;
using System.Text;

namespace SkyhoshiLinkedInLibrary.Logging
{
    public class SkyhoshiLogger
    {
        public static void Log(string message = "")
        {
            if (Extensions.Console.ConsoleExtensions.IsConsoleAvailable())
            {
                System.Console.WriteLine(message);
            }

            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Trace.WriteLine(message);
        }
    }
}
