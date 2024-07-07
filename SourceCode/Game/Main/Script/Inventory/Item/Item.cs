using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    public abstract class Item : ScriptableObject, IItem
    {
        [SerializeField]
        protected string _Name;
        [SerializeField]
        protected Sprite _Icon;
        [SerializeField]
        protected string _DecribeFormat;

        public string Name => _Name;
        public Sprite Icon => _Icon;

        public abstract int    Limit    { get; }
        public abstract string Describe { get; }
    }
}