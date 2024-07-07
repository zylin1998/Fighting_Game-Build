using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loyufei;

namespace FightingGame
{
    public class StateMachine
    {
        public StateMachine() 
        {
            States = new();
        }

        public State CurrentState { get; protected set; }

        public List<State> States { get; protected set; }

        public virtual void Update() 
        {
            CurrentState?.Tick();
        }

        public virtual void Transfer() 
        {
            var next = States.FirstOrDefault(s => s.CanEnter && s != CurrentState);

            if (!next.IsDefault() && CurrentState.CanExit) 
            {
                CurrentState.OnExit();

                SetState(next);
            }
        }

        public virtual void SetState(State state) 
        {
            if (States.Contains(state)) 
            {
                CurrentState = state;

                CurrentState.OnEnter();
            }
        }
    }
}