using System;
using System.Collections.Generic;
using System.Text;

namespace StonePaperScissor
{
    class Input
    {
        static ConsoleKeyInfo keyInfo;
        static ConsoleKey key;

        public static ConsoleKey Detect()
        {
            keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;
            return key;
        }
    }
}
