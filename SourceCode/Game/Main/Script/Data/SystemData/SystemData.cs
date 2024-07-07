using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Loyufei;

namespace FightingGame.System
{
    public class SystemData : ISaveable
    {
        public SystemData()
        {
            _Screen    = new();
            _Volumn    = new(4);
            _Langguage = new();
        }

        public SystemData(ScreenData screen, VolumnData volumn, LanguageData langguage)
        {
            _Screen    = screen;
            _Volumn    = volumn;
            _Langguage = langguage;
        }

        public ScreenData    _Screen;
        public VolumnData    _Volumn;
        public LanguageData _Langguage;
    }
}