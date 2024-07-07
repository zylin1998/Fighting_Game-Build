using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;
using Loyufei;

namespace FightingGame
{
    public class DistanceAttackView : AttackView
    {
        [SerializeField]
        protected float _InstantiateTime;
        [SerializeField]
        protected float _Speed;

        protected float _Direction;

        protected override void Awake()
        {
            base.Awake();

            this.FixedUpdateAsObservable()
                .Subscribe(Unity => transform
                    .Translate(new Vector2(_Direction * _Speed, 0) * Time.fixedDeltaTime, Space.World));
        }

        [Inject]
        protected virtual void Construct(int direction) 
        {
            _Direction = direction;
            
            var scale = transform.localScale;

            transform.localScale = new Vector3(scale.x * _Direction, scale.y, scale.z);
        }

        public float InstantiateTime => _InstantiateTime;

        protected override void AttackEvent(AttackView attack, Collider2D collider)
        {
            var info = attack.AttackInfo(collider);
            var (id, clip, playMode) = attack.SoundInfo;

            if (!info.target.IsDefault())
            {
                var from     = info.from;
                var target   = info.target;
                var statName = info.statName;
                var damage   = info.damage;
                var force    = info.force.x < 0 ? 1 : -1;

                collider.attachedRigidbody.AddForce(new Vector2(force * 1.7f, 0), ForceMode2D.Impulse);

                Stat  .Decrease(from, target, statName, damage);
                Volumn.Play    (id, clip, playMode);

                Destroy(gameObject);
            }
        }
    }
}