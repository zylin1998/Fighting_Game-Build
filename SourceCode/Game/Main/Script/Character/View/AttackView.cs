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
    public class AttackView : MonoBehaviour
    {
        [SerializeField]
        protected string    _TargetStat = "Health";
        [SerializeField]
        protected int       _AudioSourceId = 3;
        [SerializeField]
        protected AudioClip _Clip;
        [SerializeField]
        protected EPlayMode _PlayMode = EPlayMode.OneShot;

        public Mark          From   { get; protected set; }
        public CalculateStat Damage { get; protected set; }
        
        public string TargetStat => _TargetStat;

        public (int id, AudioClip clip, EPlayMode playMode) SoundInfo
            => (_AudioSourceId, _Clip, _PlayMode);

        [Inject]
        protected StatService   Stat;
        [Inject]
        protected VolumnService Volumn;

        protected virtual void Awake()
        {
            this.OnTriggerEnter2DAsObservable()
                .Subscribe(c =>  AttackEvent(this, c));
        }

        [Inject]
        protected virtual void Construct(Mark from, CalculateStat damage) 
        {
            From   = from;
            Damage = damage;
        }

        public (Mark from, Mark target, string statName, float damage, Vector2 force) AttackInfo(Collider2D collider) 
        {
            var character = collider.GetComponent<CharacterView>();

            if (!character) 
            {
                return (default, default, default, default, default);
            }

            if (!character.IsActive)
            {
                return (default, default, default, default, default);
            }

            var from     = From;
            var target   = character.Mark;
            var statName = TargetStat;
            var damage   = Damage.Calculate;
            var force    = transform.parent.position - collider.transform.position;

            if(Equals(from.CharacterType, target.CharacterType)) 
            {
                return (default, default, default, default, default);
            }

            return (from, target, statName, damage, force);
        }

        protected virtual void AttackEvent(AttackView attack, Collider2D collider)
        {
            var info  = attack.AttackInfo(collider);
            var sound = attack.SoundInfo;

            if (!info.target.IsDefault())
            {
                var from     = info.from;
                var target   = info.target;
                var statName = info.statName;
                var damage   = info.damage;
                var force    = info.force.x < 0 ? 1 : -1;

                collider.attachedRigidbody.AddForce(new Vector2(force * 1.7f, 0), ForceMode2D.Impulse);

                Stat.Decrease(from, target, statName, damage);
                Volumn.Play(sound.id, sound.clip, sound.playMode);
            }
        }
    }
}