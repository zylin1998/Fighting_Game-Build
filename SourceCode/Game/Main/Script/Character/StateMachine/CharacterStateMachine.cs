using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using UniRx;

namespace FightingGame
{
    public class CharacterStateMachine : StateMachine, ITickable, IFixedTickable
    {
        public static bool Pause { get; set; }

        public virtual void Tick()
        {
            if (Pause) return; 

            Transfer();
        }

        public virtual void FixedTick()
        {
            if (Pause) return;

            Update();
        }
    }

    public class CharacterStateMachinePool : MemoryPool<CharacterStateMachine> 
    {
        public CharacterStateMachinePool(TickableManager tickable) 
        {
            Tickable = tickable;
        }

        public TickableManager Tickable { get; }

        protected override void Reinitialize(CharacterStateMachine machine)
        {
            machine.States.Clear();

            Observable
                .NextFrame(FrameCountType.Update)
                .Subscribe((f) => Tickable.Add(machine));
            Observable
                .NextFrame(FrameCountType.FixedUpdate)
                .Subscribe((f) => Tickable.AddFixed(machine));
        }

        protected override void OnDespawned(CharacterStateMachine machine)
        {
            machine.States.Clear();

            Tickable.Remove(machine);
            Tickable.RemoveFixed(machine);
        }
    }
}
