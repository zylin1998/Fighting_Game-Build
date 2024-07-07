using Loyufei;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;
using FightingGame.System;

namespace FightingGame
{
    public class NearAttackView : AttackView
    {
        [SerializeField]
        private float     _AwakeTime;
        [SerializeField]
        private float     _SleepTime;
        
        protected override void Awake()
        {
            base.Awake();

            gameObject.SetActive(false);
        }

        public virtual bool ShouldAttack(float time)
        {
            var should = time >= _AwakeTime && time <= _SleepTime;
            
            gameObject.SetActive(should);

            return should;
        }
    }
}