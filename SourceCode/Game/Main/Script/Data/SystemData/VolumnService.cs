using FightingGame.System;
using Loyufei.DomainEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    public class VolumnService : AggregateRoot
    {
        public VolumnService(DomainEventService service) : base(service)
        {

        }

        public virtual object GroupId { get; }

        public void Play(int id, AudioClip clip, EPlayMode playMode) 
        {
            this.SettleEvents(GroupId, new PlayAudioClipEvent(id, clip, playMode));
        }
    }
}