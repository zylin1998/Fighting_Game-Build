using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace FightingGame.System
{
    public class LanguageModel
    {
        public LanguageModel([Inject(Id = GameNounDeclarations.Language)] LanguageData data) 
        {
            _Data = data;
        }

        private LanguageData _Data;

        public void SetLangguage(int langguage) 
        {
            _Data._Language = (SystemLanguage)langguage;
        }

        public void SetLangguage(SystemLanguage langguage) 
        {
            _Data._Language = langguage;
        }

        public SystemLanguage FetchData() 
        {
            return _Data._Language;
        }
    }
}