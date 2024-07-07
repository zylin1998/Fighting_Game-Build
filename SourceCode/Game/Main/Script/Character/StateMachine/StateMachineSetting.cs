using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loyufei;

namespace FightingGame
{
    [CreateAssetMenu(fileName = "StateMachine Setting", menuName = "Fighting Game/Character/State/StateMachine Setting", order = 1)]
    public class StateMachineSetting : ScriptableObject, IEntity<StateMachineSetting>
    {
        [SerializeField]
        private string _Id;
        [SerializeField]
        private GameObject _Prefab;
        [SerializeField]
        private List<State> _States;

        public object Identity => _Id;
        public StateMachineSetting Data => this;

        public List<State> States => _States;
        public GameObject  Prefab => _Prefab;
    }
}