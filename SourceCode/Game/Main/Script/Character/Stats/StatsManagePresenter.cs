using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Loyufei;
using Loyufei.DomainEvents;

namespace FightingGame
{
    public class StatsManagePresenter : AggregateRoot
    {
        public StatsManagePresenter(CharacterCollection collection, DomainEventService service)
            : base(service)
        {
            Model = new(collection.GetGroup((string)CharacterType));

            StatIncreaseAction = new();
            StatDecreaseAction = new();

            service.Register<StatIncreaseEvent>(StatIncrease  , CharacterType);
            service.Register<StatDecreaseEvent>(StatDecrease  , CharacterType);
        }

        public StatsManageModel Model { get; }

        public virtual object GroupId       { get; }
        public virtual object CharacterType { get; }

        public Dictionary<object, Action<CalculateStat.Variable, ICharacter>> StatIncreaseAction { get; }
        public Dictionary<object, Action<CalculateStat.Variable, ICharacter>> StatDecreaseAction { get; }

        protected void StatIncrease(StatIncreaseEvent increase)
        {
            var mark     = increase.Target;
            var statName = increase.StatName;
            var delta    = increase.Delta;
            var action   = StatIncreaseAction.GetorReturn(statName, () => (v, c) => { } );
            var response = Model.Increase(mark, statName, delta);

            action.Invoke(response.variable, response.character);
        }

        protected void StatDecrease(StatDecreaseEvent decrease)
        {
            var mark     = decrease.Target;
            var statName = decrease.StatName;
            var delta    = decrease.Delta;
            var action   = StatDecreaseAction.GetorReturn(statName, () => (v, c) => { });
            var response = Model.Decrease(mark, statName, delta);
            
            action.Invoke(response.variable, response.character);
        }
    }

    public class OnStatsSpawn : DomainEventBase 
    {
        public OnStatsSpawn(Mark mark, CalculateStats stats)
        {
            Mark  = mark;
            Stats = stats;
        }

        public Mark           Mark  { get; }
        public CalculateStats Stats { get; }
    }

    public class OnStatsDespawn : DomainEventBase
    {
        public OnStatsDespawn(Mark mark)
        {
            Mark = mark;
        }

        public Mark Mark { get; }
    }

    public class StatIncreaseEvent : DomainEventBase
    {
        public StatIncreaseEvent(Mark from, Mark target, string statName, float delta)
        {
            From     = from;
            Target   = target;
            StatName = statName;
            Delta    = delta;
        }

        public Mark   From     { get; }
        public Mark   Target   { get; }
        public string StatName { get; }
        public float  Delta    { get; }
    }

    public class StatDecreaseEvent : DomainEventBase
    {
        public StatDecreaseEvent(Mark from, Mark target, string statName, float delta)
        {
            From     = from;
            Target   = target;
            StatName = statName;
            Delta    = delta;
        }

        public Mark   From     { get; }
        public Mark   Target   { get; }
        public string StatName { get; }
        public float  Delta    { get; }
    }
}
