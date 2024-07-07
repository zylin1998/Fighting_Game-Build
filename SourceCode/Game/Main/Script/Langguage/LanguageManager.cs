using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;
using Loyufei;

namespace FightingGame
{
    public class LanguageManager
    {
        public LanguageManager([Inject(Id = GameNounDeclarations.Language)]IEntityForm assets) 
        {
            _Texts  = new();
            _Assets = assets;
        }

        private IEntityForm _Assets;

        private List<ILanguageText> _Texts;

        public void Register(ILanguageText text) 
        {
            _Texts.Add(text);
        }

        public void SetLangguage(SystemLanguage language) 
        {
            var asset = _Assets[language].To<LanguageAsset>();

            var font = asset.Font;

            _Texts.ForEach(t => t.SetText(font, asset[t.Id]?.Data));
        }

        public (TMP_FontAsset font, IEnumerable<(int id, string text)> texts) FetchTexts(SystemLanguage language, params int[] textId) 
        {
            var asset = _Assets[language].To<LanguageAsset>();

            return (asset.Font, textId.Select(id => (id, asset[id].Data)));
        }
    }
}