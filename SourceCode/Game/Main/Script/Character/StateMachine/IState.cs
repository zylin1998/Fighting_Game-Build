using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    public interface IState
    {
        public void OnEnter();
        public void OnExit();
        public void Tick();

        public bool CanEnter { get; }
    }
}