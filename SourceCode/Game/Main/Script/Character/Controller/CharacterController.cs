using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using UniRx;

namespace FightingGame
{
    public class CharacterController : ITickable, IFixedTickable
    {
        public bool IsDead { get; protected set; }

        public CalculateStats Stats { get; set; }

        public virtual void Tick() 
        {

        }

        public virtual void FixedTick() 
        {

        }

        public virtual void Death()
        {
            IsDead = true;
        }

        public virtual void Init() 
        {
            IsDead = false;
        }
    }

    public class CharacterControllerPool : MemoryPool<CharacterController>
    {
        public CharacterControllerPool(TickableManager tickable)
        {
            Tickable = tickable;
        }

        public TickableManager    Tickable  { get; }

        protected override void Reinitialize(CharacterController controller)
        {
            controller.Init();

            Observable
                .NextFrame(FrameCountType.Update)
                .Subscribe((f) => Tickable.Add(controller));
            Observable
                .NextFrame(FrameCountType.FixedUpdate)
                .Subscribe((f) => Tickable.AddFixed(controller));
        }

        protected override void OnDespawned(CharacterController controller)
        {
            Tickable.Remove(controller);
            Tickable.RemoveFixed(controller);
        }
    }
}
