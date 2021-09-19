using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace StonePaperScissor
{
    public class Graphics
    {
        public static bool usingMargin;
        private static int width;
        private static int height;
        public static void SetGlobalResolution(int x, int y)
        {
            width = x;
            height = y;
        }
        public static void ColorText(string text, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public static void SetPoint(int x, int y) { Console.SetCursorPosition(x, y); }
        public static void SetPointPadding(int padding) { Console.SetCursorPosition(padding, padding); }
        public static void Draw(string text) { Console.Write(text); }
        public static void DrawMargin(int x, int y, string name, ConsoleColor titleColor)
        {
            int tx, ty;
            tx = x; ty = y;
            //Este metodo dibujara el menu en X
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i <= x; i++)
            {
                Console.Write("═");
            }
            Console.SetCursorPosition(0, y);
            for (int i = 0; i <= x; i++)
            {
                Console.Write("═");
            }
            for (int i = 0; i <= y; i++)
            {
                Console.SetCursorPosition(x, ty);
                Console.Write("║");
                ty--;
            }
            ty = y;
            for (int i = 0; i <= y; i++)
            {
                Console.SetCursorPosition(0, ty);
                Console.Write("║");
                ty--;
            }
            //Window 
            Console.ForegroundColor = titleColor;
            Console.SetCursorPosition((x - name.Length) / 2, 0);
            Console.Write("[" + name + "]");
            //Footer
            Console.ForegroundColor = ConsoleColor.Gray;
            name = "[created by andrimator]";
            Console.SetCursorPosition((x - name.Length) / 2, y);
            Console.Write(name);
            usingMargin = true;
            //Corners
            Console.SetCursorPosition(x,y);
            Console.Write('╝');
            Console.SetCursorPosition(0, 0);
            Console.Write('╔');
            Console.SetCursorPosition(x, 0);
            Console.Write('╗');
            Console.SetCursorPosition(0, y);
            Console.Write('╚');

        }
        public static void DrawDelayedList(string[] list, int posX, int posY, int milisec)
        {
            int[] startPos = { 0, 0 };
            int x, y;
            x = posX;
            y = posY;
            startPos[0] = x;
            startPos[1] = y;
            x = 2; y = 2;
            try
            {
                SetPoint(x, y); Draw(list[0]);
                Thread.Sleep(milisec);
                SetPoint(x, y + 1); Draw(list[1]);
                Thread.Sleep(milisec);
                SetPoint(x, y + 2); Draw(list[2]);
                Thread.Sleep(milisec);
                SetPoint(x, y + 3); Draw(list[3]);
                SetPoint(x, y + 4); Draw(list[4]);
                SetPoint(x, y + 5); Draw(list[5]);
                SetPoint(x, y + 6); Draw(list[6]);
                SetPoint(x, y + 7); Draw(list[7]);
                SetPoint(x, y + 8); Draw(list[8]);
                SetPoint(x, y + 9); Draw(list[9]);
            }
            catch (SystemException)
            {

            }
        }
        public static void DrawList(string[] list, int posX, int posY)
        {
            int[] startPos = { 0, 0 };
            int x, y;
            x = posX;
            y = posY;
            startPos[0] = x;
            startPos[1] = y;
            x = 2; y = 2;
            try
            {
                SetPoint(x, y); Draw(list[0]);
                SetPoint(x, y + 1); Draw(list[1]);
                SetPoint(x, y + 2); Draw(list[2]);
                SetPoint(x, y + 3); Draw(list[3]);
                SetPoint(x, y + 4); Draw(list[4]);
                SetPoint(x, y + 5); Draw(list[5]);
                SetPoint(x, y + 6); Draw(list[6]);
                SetPoint(x, y + 7); Draw(list[7]);
                SetPoint(x, y + 8); Draw(list[8]);
                SetPoint(x, y + 9); Draw(list[9]);
            }
            catch (SystemException)
            {

            }

        }
        public static void DrawListCenterX(string[] list, int posY)
        {
            int[] startPos = { 0, 0 };
            int x, y;
            y = posY;
            startPos[1] = y;
            x = Console.WindowWidth/2 - list.Length;
            y = 2;
            try
            {
                SetPoint(x, y); Draw(list[0]);
                SetPoint(x, y + 1); Draw(list[1]);
                SetPoint(x, y + 2); Draw(list[2]);
                SetPoint(x, y + 3); Draw(list[3]);
                SetPoint(x, y + 4); Draw(list[4]);
                SetPoint(x, y + 5); Draw(list[5]);
                SetPoint(x, y + 6); Draw(list[6]);
                SetPoint(x, y + 7); Draw(list[7]);
                SetPoint(x, y + 8); Draw(list[8]);
                SetPoint(x, y + 9); Draw(list[9]);
            }
            catch (SystemException)
            {

            }

        }
        public static void DrawStatsOnScreen(Player player)
        {
            SetPoint(2, height-2);
            Console.Write("Nombre: {0} | ♥: {1} | Racha: {2}", player.name,player.lives,player.streak);
        }
        public static int DrawGetInputInt(int posx, int posy)
        {
            int input = 0;
            bool validData = false;
            while (validData == false)
            {
                SetPoint(posx, posy);
                try
                {
                    Console.Write("Elige una opción de la lista: ");
                    input = Convert.ToInt32(Console.ReadLine());
                    return input;
                }
                catch (SystemException)
                {
                    Graphics.SetPoint(posy+2, posx);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("¡Debes escribir solo numeros!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            return input;
        }


    }
}
