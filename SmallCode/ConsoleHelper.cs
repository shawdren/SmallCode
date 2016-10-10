using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallCode
{
    public class ConsoleHelper
    {
        public void Write(string content)
        {
            Console.Write(content + Environment.NewLine);
        }
        public void WriteForegroundColorGreen(string content)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(content + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void WriteForegroundColorRed(string content)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(content + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
