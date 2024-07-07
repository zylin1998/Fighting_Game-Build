using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace FightingGame
{
    public class CharacterControllerPresenter
    {
        public CharacterControllerPresenter(CharacterControllerModel model, DomainEventService service)
        {
            _Model = model;

            service.Register<CharacterControllerSpawnEvent>  (Spawn  , GroupId);
            service.Register<CharacterControllerDespawnEvent>(Despawn, GroupId);
        }

        private CharacterControllerModel _Model;

        public virtual object GroupId { get; }

        public void Spawn(CharacterControllerSpawnEvent spawn)
        {
            spawn.Response?.Invoke(_Model.Spawn(spawn.Mark));
        }

        public void Despawn(CharacterControllerDespawnEvent spawn)
        {
            _Model.Despawn(spawn.Mark, spawn.Controller);
        }
    }

    public class CharacterControllerSpawnEvent : DomainEventBase
    {
        public CharacterControllerSpawnEvent(Mark mark, Action<CharacterController> response)
        {
            Mark     = mark;
            Response = response;
        }

        public Mark                        Mark     { get; }
        public Action<CharacterController> Response { get; }
    }

    public class CharacterControllerDespawnEvent : DomainEventBase
    {
        public CharacterControllerDespawnEvent(Mark mark, CharacterController controller)
        {
            Mark       = mark;
            Controller = controller;
        }

        public Mark                Mark       { get; }
        public CharacterController Controller { get; }
    }
}
