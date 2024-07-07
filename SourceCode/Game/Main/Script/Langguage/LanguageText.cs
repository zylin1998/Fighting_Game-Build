using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FightingGame
{
    public interface ILanguageText 
    {
        public int Id { get; }

        public void SetText(TMP_FontAsset font, string text);
    }

    public class LanguageText : MonoBehaviour, ILanguageText
    {
        [SerializeField]
        private int             _Id;
        [SerializeField]
        private TextMeshProUGUI _Text;

        public int Id  => _Id;

        public void SetText(TMP_FontAsset font, string text) 
        {
            _Text.font = font;

            _Text.SetText(text);
        }

        [ContextMenu("GetText")]
        private void GetText() 
        {
            _Text = GetComponent<TextMeshProUGUI>();
        }
    }
}