using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace StonePaperScissor
{
    class Game
    {
        public Player player;
        public static int level = 1;
        public static bool finished = false;
        private static bool nameset;
        private int width;
        private int height;
        private static string failmsg = "La consola ha ganado, ¡intentalo de nuevo!";
        private static string winmsg = "Le has ganado al bot, ¡felicidades!";
        public int Run() //Reiniciar main menu en caso de opcion invalida.
        {
            do
            {
                switch (MainMenu())
                {
                    case 0:
                        return 0;
                    default:
                        break;
                }
            } while (true);

        }
        public int MainMenu() //Devolver 0 para reiniciar, 2 para salir.
        {
            Console.Clear();
            string neworcontinue = "1: Nuevo Juego";
            if (nameset) neworcontinue = "1: Continuar";
            string[] menulist = {neworcontinue, "2: Creditos", "3: ???", "0: Salir", ""};
            Graphics.DrawMargin(width,height,"Piedra, Papel, Tijeras", ConsoleColor.Green);
            Graphics.DrawListCenterX(menulist,2);

            switch (GetInput(width / 2 - 16, height - 2))
            {
                case 1: //Comenzar
                    if (!nameset) Choosename();
                    else Comenzar();
                    break;
                case 2:
                    Credits();
                    break;
                case 3:
                    Keyroom();
                    break;
                case 0: //Salir
                    return 0;
                default: //No en lista
                    break;
            }
            return 1;


            //ReturnConditions
            
        }
        #region Game
        private void Choosename()
        {
            Console.Clear();
            Graphics.DrawMargin(width, height, "Let me meet you", ConsoleColor.Yellow);
            string[] list = { "Welcome to my land...", "I hope we can be friends, how are you named?", "C'mon, dont be shy hehehe", "Whats your name? :)"};
            Graphics.DrawDelayedList(list, 5, 4,2500); //Delayed Text
            Console.SetCursorPosition(2,6);
            Thread.Sleep(3000);
            Console.Write("Your answer: "); //Enter your name:
            player = new Player(Console.ReadLine());

            Console.Clear();
            Graphics.DrawMargin(width, height, "I met you", ConsoleColor.DarkYellow);
            Thread.Sleep(1000);
            Console.SetCursorPosition(2, 2);
            Console.Write("Hello then"); Anim.SusDots(3, 1000);
            Console.Write(" {0}.",player.name);
            Thread.Sleep(3000);
            nameset = true;
            while(true)
            {
                if(Comenzar() == 0) break;
            }
        }
        public int Comenzar()
        {
            //Level Itself
            Console.Clear();
            Graphics.DrawMargin(width, height, "Coliseo Demano", ConsoleColor.Blue);
            Graphics.DrawStatsOnScreen(player);
            Console.SetCursorPosition(width-9, height-2);
            Graphics.ColorText("X: Salir", ConsoleColor.Red);
            //LevelConditions
            Console.SetCursorPosition(2, 2);
            Console.Write("Piedra, papel o tijera"); Anim.SusDots(3, 800);
            Console.Write(" Elige alguno: ");
            string playerguess = Console.ReadLine();
            switch (StartRound(playerguess))
            {
                case 1: //WIN
                    Console.SetCursorPosition(2, 5);
                    Graphics.ColorText(winmsg, ConsoleColor.Green);
                    player.LevelSuccess();
                    break;
                case 2: //FAIL
                    Console.SetCursorPosition(2, 5);
                    Graphics.ColorText(failmsg, ConsoleColor.Red);
                    player.LevelFail();
                    break;
                case 3: //DRAW
                    Console.SetCursorPosition(2, 5);
                    Console.Write("Han empatado!");
                    break;
                case 4:
                    Console.SetCursorPosition(2, 5);
                    Graphics.ColorText("Error. Intenta con piedra, papel o tijera.", ConsoleColor.Red);
                    break;
                case 0:
                    return 0;
                default:
                    break;
            }
            Console.ReadKey();
            return 1;
        }
        #endregion
        public int Credits()
        {   
            //Level Itself
            Console.Clear();
            Graphics.DrawMargin(width, height, "Creditos", ConsoleColor.Yellow);
            //LevelConditions
            string[] list = { "Language: C#", "Piedra, Papel, Tijeras 1.0", "Created by @andrimator", "Hope you like it :)", "Made in 5 hours lol" };
            Graphics.DrawList(list, 5, 4);
            Console.ReadKey();
            return 0;
        }
        public int Keyroom()
        {
            //Level Itself
            Console.Clear();
            Graphics.DrawMargin(width, height, "Keyroom", ConsoleColor.Magenta);
            //LevelConditions
            string[] list = { "Language: C#", "IDE: Visual Studio Community 2019", "Created by @andrimator", "Hope you like it :)", "Made in 5 hours lol" };
            Graphics.DrawList(list, 5, 4);
            Console.ReadKey();
            return 0;
        }

        void GetGameResults()
        {

        }
        public void SetResolution(int x, int y)
        {
            height = y;
            width = x;
            Graphics.SetGlobalResolution(x,y);
        }

        #region PiedraPapelTijeraLogics
        private static int StartRound(string guess)
        {
            // 1: PLAYER 2: BOT 3: DRAW
            //PLAYER
            guess = guess.ToLower();
            if (guess == "x") return 0;
            if (guess == "tijeras") guess = "tijera";
            //BOT GUESS
            string botguess = RandomBotGuess();

            Console.SetCursorPosition(2, 4);
            Console.Write("Tu: {0}| El bot: {1}", guess, botguess);

            return CompareMatchResults(guess,botguess);
        }
        private static string RandomBotGuess()
        {
            var random = new Random();
            var bytes = new byte[5];
            random.NextBytes(bytes);
            //ShowRandomBytes(bytes);
            string bytedetect = bytes[3].ToString();
            bool cond1 = System.DateTime.Now.Millisecond % 2 == 0;
            string botguess = "N/A";
            if (bytedetect.Contains('1') || bytedetect.Contains('3') || bytedetect.Contains('5'))
            {
                if (cond1)
                {
                    botguess = "piedra";
                }
                else
                {
                    botguess = "papel";
                }
            }
            else if (bytedetect.Contains('7') || bytedetect.Contains('9') || bytedetect.Contains('0'))
            {
                if (cond1)
                {
                    botguess = "papel";
                }
                else
                {
                    botguess = "tijera";
                }
            }
            else if (bytedetect.Contains('2') || bytedetect.Contains('6'))
            {
                if (cond1)
                {
                    botguess = "tijera";
                }
            }
            else botguess = "piedra";

            if (botguess != "piedra" || botguess != "tijera" || botguess != "papel") botguess = "tijera";

            return botguess;
        }
        public static int CompareMatchResults(string guess, string botguess)
        {
            //Final Comparation Between Answers
            if (guess == botguess) return 3; // DRAW - Nobody wins, try again

            else if (guess == "piedra")
            {
                switch (botguess)
                {
                    case "papel":
                        return 2; //Bot Wins
                    case "tijera":
                        return 1; //Player Wins
                }
            }
            else if (guess == "papel")
            {
                switch (botguess)
                {
                    case "tijera":
                        return 2; //Bot Wins
                    case "piedra":
                        return 1; //Player Wins
                }
            }
            else if (guess == "tijera")
            {
                switch (botguess)
                {
                    case "papel":
                        return 1; //Player Wins
                    case "piedra":
                        return 2; //Bot Wins
                }
            }
            return 4; //4 means no valid USER entry
        }
        private static void ShowRandomBytes(byte[] input)
        {
            foreach (byte byteValue in input)
                Console.Write("{0, 5}", byteValue);
            Console.WriteLine();
        }
        private static int GetInput(int x, int y) { return Graphics.DrawGetInputInt(x,y); }
        #endregion
        private static void DelayMil(int miliseconds) { Thread.Sleep(miliseconds); }
        /*public int Template()
        {
            //Level Itself
            Console.Clear();
            Graphics.DrawMargin(width, height, "Creditos", ConsoleColor.Yellow);
            //LevelConditions
            Console.Read();
            return 0;
        }*/
    }
}
