using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace FightingGame
{
    public class CalculateStatsModel
    {
        public CalculateStatsModel(DiContainer container, [Inject(Id = GameNounDeclarations.Character)] CalculateStatsPool pool)
        {
            Container = container;

            _Pool = pool;
        }

        public DiContainer Container { get; }

        private CalculateStatsPool _Pool;

        public CalculateStats Spawn(Mark mark, IEnumerable<Stat> others) 
        {
            return _Pool?.Spawn(mark, others);
        }

        public void Despawn(CalculateStats stats) 
        {
            _Pool?.Despawn(stats);
        }
    }
}