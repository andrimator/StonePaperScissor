using System;
using System.Collections.Generic;
using System.Text;

namespace StonePaperScissor
{
    public class List
    {
        public string[] elements;
        public int selecteditem = 1;
        public int lenght = 0;

        public List()
        {

        }
        public List(string[] items)
        {
            lenght = items.Length;
            elements = items;
        }
        public void AddElement(string item)
        {
            string[] tmpArray = elements;
            elements = new string[lenght];
            elements = tmpArray;
            elements[lenght-1] = item;
            lenght++;
        }
        public int GetLenght()
        {
            return elements.Length;
        }

        public void DrawCenterX(int posY)
        {
            int x, y;
            string[] list = elements;
            y = posY;
            x = Console.WindowWidth / 2 - list.Length;
            try
            {
                for(int i = 0; i <= list.Length-1; i++)
                {
                    if (i == selecteditem-1)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(x, y + i); Console.Write(list[i]);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.SetCursorPosition(x, y + i); Console.Write(list[i]);
                    }
                }
            }
            catch (SystemException)
            {

            }

        }
    }
}
