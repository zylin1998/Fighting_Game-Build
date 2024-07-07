using FightingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    public abstract class Talent : Item, ITalent, IPurchase
    {
        [SerializeField]
        protected int    _Delta;
        [SerializeField]
        protected string _TargetProperty;
        [SerializeField]
        protected int    _Price;
        
        public int    Delta          => _Delta;
        public int    Price          => _Price;
        public string TargetProperty => _TargetProperty;
    }
}