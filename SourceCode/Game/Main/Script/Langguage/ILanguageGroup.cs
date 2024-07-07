using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    public interface ILanguageGroup 
    {
        public IEnumerable<ILanguageText> LanguageTexts { get; }
    }
}