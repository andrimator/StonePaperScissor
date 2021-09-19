using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace StonePaperScissor
{
    class Anim : Graphics
    {
        public static void Spell(string text, int delaymilsec)
        {
            //TODO method to make spelling in cmd
        }
        public static void SusDots(int amount, int delaymilsec)
        {
            RepeatChar('.', amount, delaymilsec);
        }
        public static void RepeatChar(char tchar, int amount, int delaymilsec)
        {
            for (int i = 1; i <= amount; i++)
            {
                Console.Write(tchar);
                Thread.Sleep(delaymilsec);
            }
        }
        #region Modifiers
        private static void mod1() { }
        #endregion
    }

}
