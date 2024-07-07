using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    public interface IItem
    {
        public string Name     { get; }
        public Sprite Icon     { get; }
        public string Describe { get; }
        public int    Limit    { get; }
    }
}