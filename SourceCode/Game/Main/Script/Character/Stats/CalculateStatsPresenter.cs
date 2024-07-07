using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace FightingGame
{
    public class CalculateStatsPresenter : AggregateRoot
    {
        public CalculateStatsPresenter(CalculateStatsModel model, DomainEventService service) 
            : base(service)
        {
            _Model = model;

            service.Register<StatsSpawnEvent>  (Spawn  , GroupId);
            service.Register<StatsDespawnEvent>(Despawn, GroupId);
        }

        private CalculateStatsModel _Model;

        public virtual object GroupId { get; }

        public void Spawn(StatsSpawnEvent spawn) 
        {
            var stats = _Model.Spawn(spawn.Mark, spawn.Others);

            spawn.Response?.Invoke(stats);

            this.SettleEvents(spawn.Mark.CharacterType, new OnStatsSpawn(spawn.Mark, stats));
        }

        public void Despawn(StatsDespawnEvent despawn)
        {
            _Model.Despawn(despawn.Stats);

            this.SettleEvents(despawn.Mark.CharacterType, new OnStatsDespawn(despawn.Mark));
        }
    }

    public class StatsSpawnEvent : DomainEventBase 
    {
        public StatsSpawnEvent(Mark mark, IEnumerable<Stat> others, Action<CalculateStats> response)
        {
            Mark     = mark;
            Others   = others;
            Response = response;
        }

        public Mark                   Mark     { get; }
        public IEnumerable<Stat>      Others   { get; }
        public Action<CalculateStats> Response { get; }
    }

    public class StatsDespawnEvent : DomainEventBase
    {
        public StatsDespawnEvent(Mark mark, CalculateStats stats)
        {
            Mark  = mark;
            Stats = stats;
        }

        public Mark           Mark  { get; }
        public CalculateStats Stats { get; }
    }
}
