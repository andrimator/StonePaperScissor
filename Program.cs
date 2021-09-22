using System;
using System.Threading;

namespace StonePaperScissor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            //Sets global resolution for the game.
            int x = 50; int y = 10;
            Console.SetWindowSize(x+1, y+1);
            Console.SetBufferSize(x+1, y+1);

            //Create a new game, sets the resolution, and then starts.
            Game game = new Game();
            game.SetResolution(x,y);
            game.Run();

            //Console.ReadLine(); //Final Read
        }
    }
}
