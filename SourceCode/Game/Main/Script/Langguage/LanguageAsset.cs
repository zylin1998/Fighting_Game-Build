using Loyufei;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FightingGame
{
    [CreateAssetMenu(fileName = "LangguageAsset", menuName = "Fighting Game/Langguage/Asset", order = 1)]
    public class LanguageAsset : EntityFormAsset<string, Entity<int, string>>, IEntity<LanguageAsset>
    {
        [SerializeField]
        private SystemLanguage _Langguage;
        [SerializeField]
        private TMP_FontAsset  _Font;

        public object         Identity => _Langguage;
        public LanguageAsset Data     => this;
        public TMP_FontAsset  Font     => _Font;
    }
}