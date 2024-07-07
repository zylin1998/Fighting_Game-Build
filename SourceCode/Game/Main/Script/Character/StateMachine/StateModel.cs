using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using Loyufei;
using UnityEngine;

namespace FightingGame
{
    public class StateModel
    {
        public StateModel(DiContainer container)
        {
            Container = container;
        }

        public DiContainer Container { get; }

        public IEnumerable<State> Spawn(Mark mark)
        {
            return Container.ResolveIdAll<State>(mark.CharacterId);
        }

        public void Despawn(IEnumerable<State> states)
        {
            states.ForEach(state =>
            {
                state.Dispose();

                ScriptableObject.Destroy(state);
            });
        }
    }
}
