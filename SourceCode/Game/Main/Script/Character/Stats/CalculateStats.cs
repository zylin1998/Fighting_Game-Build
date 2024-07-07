using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Loyufei;
using Zenject;

namespace FightingGame
{
    [Serializable]
    public class CalculateStats
    {
        public CalculateStats() 
        {

        }

        public CalculateStats(StatsAsset stats, IEnumerable<Stat> others)
        {
            Reset(stats, others);
        }

        public CalculateStats(StatsAsset stats)
        {
            Reset(stats.OfType<Stat>());
        }

        public Dictionary<string, CalculateStat> Stats { get; protected set; }

        public CalculateStat.Variable Increase(string statName, float amount) 
        {
            return Stats.TryGetValue(statName, out var stat) ? stat.Increase(amount) : new();
        }

        public CalculateStat.Variable Decrease(string statName, float amount)
        {
            return Stats.TryGetValue(statName, out var stat) ? stat.Decrease(amount) : new();
        }

        public CalculateStat FetchStat(string statName) 
        {
            return Stats.GetorReturn(statName, () => default);
        }

        public void Reset(StatsAsset asset, IEnumerable<Stat> others)
        {
            var stats = asset.OfType<Stat>().Select(stat =>
            {
                var id   = (string)stat.Identity;
                var data = stat.Data + others
                    .Where(s => Equals(s.Identity, id))
                    .Sum(s => s.Data);

                return new Stat(id, data);
            });

            Stats = stats.ToDictionary(k => (string)k.Identity, v => new CalculateStat(v));
        }

        public void Reset(IEnumerable<Stat> stats) 
        {
            Stats = stats.ToDictionary(k => (string)k.Identity, v => new CalculateStat(v));
        }

        public void Clear() 
        {
            Stats.Clear();
        }
    }

    public class CalculateStatsPool : MemoryPool<Mark, IEnumerable<Stat>, CalculateStats>
    {
        [Inject(Id = GameNounDeclarations.Character)]
        protected IEntityForm _Assets;

        protected override void Reinitialize(Mark mark, IEnumerable<Stat> others, CalculateStats stats)
        {
            stats.Reset((StatsAsset)_Assets[mark.CharacterId].Data, others);
        }

        protected override void OnDespawned(CalculateStats stats)
        {
            stats.Clear();
        }
    }
}