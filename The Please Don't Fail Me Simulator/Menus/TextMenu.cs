using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace The_Please_Dont_Fail_Me_Simulator.Menus
{
    public abstract class TextMenu
    {
        protected static int Width = 23;

        //Override ToString() for each child class. This is to make it of the same type.

        protected static string AlignCenter(string text, int displace)
        {
            int width = Width - displace;
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
