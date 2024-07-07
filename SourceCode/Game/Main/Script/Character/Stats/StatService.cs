using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loyufei.DomainEvents;
using FightingGame.System;

namespace FightingGame
{
    public class StatService : AggregateRoot
    {
        public StatService(DomainEventService service) : base(service)
        {

        }

        public virtual void Increase(Mark from, Mark to, string statName, float delta) 
        {
            this.SettleEvents(to.CharacterType, new StatIncreaseEvent(from, to, statName, delta));
        }

        public virtual void Decrease(Mark from, Mark to, string statName, float delta)
        {
            this.SettleEvents(to.CharacterType, new StatDecreaseEvent(from, to, statName, delta));
        }
    }
}