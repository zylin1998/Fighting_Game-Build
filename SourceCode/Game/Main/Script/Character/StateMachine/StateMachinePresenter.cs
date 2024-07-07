using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace FightingGame
{
    public class StateMachinePresenter
    {
        public StateMachinePresenter(CharacterStateMachinePool pool, DomainEventService service)
        {
            _Pool = pool;

            service.Register<StateMachineSpawnEvent>  (Spawn  , GroupId);
            service.Register<StateMachineDespawnEvent>(Despawn, GroupId);
        }

        private CharacterStateMachinePool _Pool;

        public virtual object GroupId { get; }

        public void Spawn(StateMachineSpawnEvent spawn)
        {
            spawn.Response?.Invoke(_Pool.Spawn());
        }

        public void Despawn(StateMachineDespawnEvent spawn)
        {
            _Pool.Despawn(spawn.Machine);
        }
    }

    public class StateMachineSpawnEvent : DomainEventBase
    {
        public StateMachineSpawnEvent(Action<CharacterStateMachine> response)
        {
            Response = response;
        }

        public Action<CharacterStateMachine> Response { get; }
    }

    public class StateMachineDespawnEvent : DomainEventBase
    {
        public StateMachineDespawnEvent(CharacterStateMachine machine)
        {
            Machine = machine;
        }

        public CharacterStateMachine Machine { get; }
    }
}
