using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame.System
{
    [Serializable]
    public class LanguageData
    {
        public LanguageData() 
        {
            _Language = Application.systemLanguage;
        }

        public SystemLanguage _Language;
    }
}