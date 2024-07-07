using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    public class LanguageToggleAdapter : ToggleListenerAdapter
    {
        [SerializeField]
        private SystemLanguage _Langguage = SystemLanguage.ChineseTraditional;

        public SystemLanguage Langguage => _Langguage;
    }
}