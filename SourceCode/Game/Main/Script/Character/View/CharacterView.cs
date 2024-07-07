using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using Zenject;
using FightingGame.ViewManagement;

namespace FightingGame
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField]
        private Transform   _CameraFocus;
        [SerializeField]
        private Animator    _Animator;
        [SerializeField]
        private Rigidbody2D _Rigidbody;
        [SerializeField]
        private Mark        _Mark;
        [SerializeField]
        private LayerMask   _GroundMask;

        private float _ScaleX;

        public Transform   CameraFocus => _CameraFocus;
        public Animator    Animator    => _Animator;
        public Rigidbody2D Rigidbody   => _Rigidbody;
        public bool        IsGround    { get; protected set; }
        public bool        IsActive    { get; protected set; }
        
        public Mark Mark 
        { 
            get => _Mark;

            set 
            { 
                _Mark = value; 

                IsActive = true;
            }
        }

        private void Awake()
        {
            _ScaleX = Mathf.Abs(transform.localScale.x);
        }

        public virtual void PlayAnimation(string stateName) 
        {
            Animator?.Play(stateName);
        }

        public virtual void Move(float direction, float speed) 
        {
            if (direction == 0) 
            {
                _Rigidbody.velocity = new Vector2(0, _Rigidbody.velocity.y);

                return; 
            }
            
            _Rigidbody.velocity  = new Vector2(speed * direction, _Rigidbody.velocity.y);
        }

        public virtual void Flip(float direction, bool invert = false)
        {
            if (direction == 0) { return; }

            var localScale = transform.localScale;
            var side       = (direction >= 0 ? 1 : -1) * (invert ? -1 : 1);
            var scaleX     = side * _ScaleX;

            transform.localScale = new Vector3(scaleX, localScale.y, localScale.z);
        }

        public virtual void Jump(float jumpForce) 
        {
            _Rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            GroundCheck();
        }

        public virtual bool GroundCheck() 
        {
            var collider = Physics2D.OverlapCircle(transform.position, 0.01f, _GroundMask);
            var velocity = _Rigidbody.velocity;

            IsGround = collider && Mathf.Abs(velocity.y) <= 0.1;

            return IsGround;
        }

        public virtual float AnimNormalizedTime(string stateName, int layer = 0) 
        {
            var state = _Animator.GetCurrentAnimatorStateInfo(layer);

            return state.IsName(stateName) ? state.normalizedTime : 0f;
        }

        public Vector2 DistanceTo(ICharacter character) 
        {
            var from = transform;
            var to   = character.View.transform;

            return to.position - from.position;
        }

        public void Close() 
        {
            IsActive = false;
        }
    }

    public class CharacterViewPool : MemoryPool<Mark, PosiInfo, CharacterView> 
    {
        protected override void Reinitialize(Mark mark, PosiInfo posi, CharacterView view)
        {
            view.Mark = mark;
            view.gameObject.SetActive(true);
            view.transform.SetPositionAndRotation(posi.Position, posi.Rotation);
        }

        protected override void OnDespawned(CharacterView view)
        {
            view.Mark = null;

            view.gameObject.SetActive(false);
        }
    }

    public struct PosiInfo 
    {
        public PosiInfo(Vector3 posi, Quaternion rota) 
        {
            Position = posi;
            Rotation = rota;
        }

        public Vector3    Position { get; }
        public Quaternion Rotation { get; }
    }
}