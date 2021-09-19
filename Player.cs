using System;
using System.Collections.Generic;
using System.Text;

namespace StonePaperScissor
{
    public class Player
    {
        public string name = "undefined";
        public int health = 10;
        public int lives = 3;
        public int xplevel = 0;
        public int streak = 0;
        public bool currentlyOnStreak = false;

        public void GetStats(int x, int y)
        {
            Console.SetCursorPosition(x,y);
            Console.WriteLine("Nombre: {0}\nSalud: {1}\nVidas: {2}\nNivel de XP: {3}",name,health,lives,xplevel);
        }
        public void LevelSuccess()
        {
            streak++;
            if(streak>=1) currentlyOnStreak = true;
        }
        public void LevelFail()
        {
            if (lives == 0)
            {
                currentlyOnStreak = false;
                lives = 3;
                streak = 0;
            }
            else lives--;
            
        }
        public Player() { }
        public Player(string tname)
        {
            name = tname;
        }
    }
}
