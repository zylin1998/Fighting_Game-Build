using Loyufei;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace FightingGame
{
    public abstract class CharacterState : State, ISetup<ICharacter>
    {
        protected CharacterView _View;

        public abstract string AnimateStateName { get; }

        public override bool CanExit => true;

        public virtual void Setup(ICharacter character)
        {
            _View = character.View;

            character.StateMachine.States.Add(this);
        }

        public override void OnEnter()
        {
            _View.PlayAnimation(AnimateStateName);
        }
    }

    public abstract class State : ScriptableObject, IState, IDisposable
    {
        [SerializeField]
        private int _Id;

        [Inject]
        protected DiContainer _Container;

        public int Id => _Id;

        public abstract bool CanEnter { get; }
        public abstract bool CanExit  { get; }

        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void Tick();

        public virtual void Dispose() 
        {

        }

        public static State Empty => ScriptableObject.CreateInstance<EmptyState>();

        private class EmptyState : State
        {
            public override bool CanEnter => true;
            public override bool CanExit  => true;

            public override void OnEnter() { }
            public override void OnExit()  { }
            public override void Tick()    { }
        }
    }

    public interface IAttack
    {
        public AttackView AttackView { get; }
    }
}