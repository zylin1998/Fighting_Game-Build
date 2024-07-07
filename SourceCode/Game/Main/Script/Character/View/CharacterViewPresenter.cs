using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace FightingGame
{
    public class CharacterViewPresenter
    {
        public CharacterViewPresenter(CharacterViewModel model, DomainEventService service) 
        {
            _Model = model;

            service.Register<CharacterViewSpawnEvent>  (Spawn  , GroupId);
            service.Register<CharacterViewDespawnEvent>(Despawn, GroupId);
        }

        private CharacterViewModel _Model;

        public virtual object GroupId { get; }

        public void Spawn(CharacterViewSpawnEvent spawn)
        {
            spawn.Response?.Invoke(_Model.Spawn(spawn.Mark, spawn.PosiInfo));
        }

        public void Despawn(CharacterViewDespawnEvent spawn)
        {
            _Model.Despawn(spawn.Mark, spawn.View);
        }
    }

    public class CharacterViewSpawnEvent : DomainEventBase
    {
        public CharacterViewSpawnEvent(Mark mark, PosiInfo posi, Action<CharacterView> response)
        {
            Mark     = mark;
            PosiInfo = posi;
            Response = response;
        }

        public Mark                  Mark     { get; }
        public PosiInfo              PosiInfo { get; }
        public Action<CharacterView> Response { get; }
    }

    public class CharacterViewDespawnEvent : DomainEventBase
    {
        public CharacterViewDespawnEvent(Mark mark, CharacterView view)
        {
            Mark = mark;
            View = view;
        }

        public Mark          Mark { get; }
        public CharacterView View { get; }
    }
}
