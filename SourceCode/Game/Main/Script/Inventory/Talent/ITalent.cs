using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    public interface ITalent 
    {
        public int    Delta          { get; }
        public string TargetProperty { get; }
    }
}