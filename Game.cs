using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace StonePaperScissor
{
    class Game
    {
        public Player player = new Player();
        public static int level = 1;
        public static bool finished = false;
        private static bool isNameset;
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
            //Build
            Console.Clear();
            string[] menulist = {"Nuevo Juego", "Creditos", "???", "Salir"};
            List lstMainMenu = new List(menulist);
            if (isNameset) lstMainMenu.elements[0] = "Continuar";


            //Menu Selection Loop
            while (true)
            {
                Console.Clear();
                Graphics.DrawMargin(width, height, "Piedra, Papel, Tijeras", ConsoleColor.Green);
                //fix add element
                lstMainMenu.DrawCenterX(2);
                Console.Write(lstMainMenu.selecteditem);
                switch (Input.Detect())
                {
                    case ConsoleKey.UpArrow:
                        if (lstMainMenu.selecteditem > 1) lstMainMenu.selecteditem--;
                        else Console.Beep(800, 80);
                        break;
                    case ConsoleKey.DownArrow:
                        //Console.Beep(1800, 100);
                        if (lstMainMenu.selecteditem < lstMainMenu.lenght) lstMainMenu.selecteditem++;
                        else Console.Beep(800, 80);
                        break;
                    case ConsoleKey.Enter:
                        switch (lstMainMenu.selecteditem)
                        {
                            case 1: //Comenzar
                                if (!isNameset) Choosename();
                                while (Comenzar() != 0) ;
                                break;
                            case 2:
                                Credits();
                                break;
                            case 3:
                                if(player.keys >= 1) Keyroom(); //WORK IN PROGRESS: ROOM ACCESSED BY USING A KEY
                                else Graphics.DrawError("[!] No tienes suficientes llaves!", 2, height - 2);
                                break;
                            case 4: //Salir
                                return 0;
                            default: //No en lista
                                break;
                        }
                        break;
                }
            }
        }
        #region Game
        private void Choosename()
        {
            int initialdelay = 2000;
            while (true)
            {
                Console.Clear();
                Graphics.DrawMargin(width, height, "Let me meet you", ConsoleColor.Yellow);
                string[] list = { "Welcome to my land...", "I hope we can be friends, how are you named?", "C'mon, dont be shy hehehe", "Whats your name? :)" };
                Graphics.DrawDelayedList(list, 5, 4, initialdelay); //Delayed Text
                Console.SetCursorPosition(2, 6);
                Thread.Sleep(initialdelay);

                Console.Write("Your answer: "); //Enter your name:
                player.name = Console.ReadLine();
                if (player.name.Length > 8)
                {
                    Graphics.DrawError("[!] Tu nombre debe tener menos de 8 caracteres.", 2, height - 2);
                    initialdelay = 0;
                }
                else if (player.name.Length == 0)
                {
                    Graphics.DrawError("[!] No puedes tener un nombre vacio.", 2, height - 2);
                    initialdelay = 0;
                }
                else break;
            }
            

            Console.Clear();
            Graphics.DrawMargin(width, height, "I met you", ConsoleColor.DarkYellow);
            Thread.Sleep(1000);
            Console.SetCursorPosition(2, 2);
            Console.Write("Hello then"); Anim.SusDots(3, 700);
            Console.Write(" {0}.",player.name);
            Thread.Sleep(2000);
            isNameset = true;
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
            Random random = new Random();
            int rnd = random.Next(1,4);
            Console.Write(rnd);
            string botguess = "N/A";
            switch (rnd)
            {
                case 1:
                    botguess = "piedra";
                    break;
                case 2:
                    botguess = "papel";
                    break;
                case 3:
                    botguess = "tijera";
                    break;
            }

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
        #endregion
        /*public int LvLTemplate()
        {
            //Level Initial Draw
            Console.Clear();
            Graphics.DrawMargin(width, height, "Creditos", ConsoleColor.Yellow);

            //LevelConditions
            Console.Read();
            return 0;

            //Input Detect
            switch (Input.Detect())
            {
                case ConsoleKey.UpArrow:
                    if (lstMainMenu.selecteditem != 1 || lstMainMenu.selecteditem != 0) lstMainMenu.selecteditem--;
                    break;
                case ConsoleKey.DownArrow:
                    if (lstMainMenu.selecteditem <= lstMainMenu.lenght) lstMainMenu.selecteditem++;
                    break;
            }

        }*/
    }
}
