using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace FightingGame
{
    public class StatePresenter
    {
        public StatePresenter(StateModel model, DomainEventService service) 
        {
            _Model = model;

            service.Register<FetchStateEvent>  (FetchState  , GroupId);
            service.Register<ReleaseStateEvent>(ReleaseState, GroupId);
        }

        private StateModel _Model;

        public virtual object GroupId { get; }

        public void FetchState(FetchStateEvent fetch) 
        {
            fetch.Response?.Invoke(_Model.Spawn(fetch.Mark).OrderBy((s) => s.Id));
        }

        public void ReleaseState(ReleaseStateEvent release)
        {
            _Model.Despawn(release.States);
        }
    }

    public class FetchStateEvent : DomainEventBase 
    {
        public FetchStateEvent(Mark mark, Action<IEnumerable<State>> response) 
        {
            Mark     = mark;
            Response = response;
        }

        public Mark                       Mark     { get; }
        public Action<IEnumerable<State>> Response { get;}
    }

    public class ReleaseStateEvent : DomainEventBase
    {
        public ReleaseStateEvent(IEnumerable<State> states)
        {
            States = states;
        }

        public IEnumerable<State> States { get; }
    }
}
