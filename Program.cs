using System;
using System.Threading;

namespace StonePaperScissor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create new game with specified size.
            Game game = new Game();
            game.Run(50,10);

        }
    }
}
