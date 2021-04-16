using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace The_Please_Dont_Fail_Me_Simulator.Menus
{
    public class GameOverMenu : TextMenu
    {
        public GameOverMenu()
        { 
        }
        
        public override string ToString()
        {
            return Properties.Resources.GameOver;
        }
    }
}
