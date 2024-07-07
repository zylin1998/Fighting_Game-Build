using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loyufei.DomainEvents;

namespace FightingGame.System
{
    public class VolumnModelPresenter : AggregateRoot
    {
        public VolumnModelPresenter(VolumnModel model, DomainEventService service) : base(service)
        {
            _Model   = model;
            
            service.Register<VolumnEvent>         (SetVolumn, GroupId);
            service.Register<MuteEvent>           (SetVolumn, GroupId);
            service.Register<PlayAudioClipEvent>  (Play     , GroupId);
            service.Register<FetchVolumnDataEvent>(FetchData, GroupId);
        }

        private VolumnModel _Model;

        public virtual object GroupId { get; }

        public void SetVolumn(VolumnEvent volumn) 
        {
            _Model.SetVolumn(volumn.Id, (int)volumn.Rate);

            this.SettleEvents(GroupId, new SaveEvent("System"));
        }

        public void SetVolumn(MuteEvent mute)
        {
            _Model.SetVolumn(mute.Id, mute.Mute);

            this.SettleEvents(GroupId, new SaveEvent("System"));
        }

        public void Play(PlayAudioClipEvent play) 
        {
            _Model.Play(play.Id, play.Clip, play.PlayMode);
        }

        public void FetchData(FetchVolumnDataEvent fetch) 
        {
            fetch.Response?.Invoke(_Model.FetchData());
        }
    }

    public class VolumnEvent : DomainEventBase
    {
        public VolumnEvent(int id, float rate) : base() 
        {
            Id   = id;
            Rate = rate;
        }

        public int   Id   { get; }
        public float Rate { get; }
    }

    public class MuteEvent : DomainEventBase
    {
        public MuteEvent(int id, bool mute) : base()
        {
            Id   = id;
            Mute = mute;
        }

        public int Id    { get; }
        public bool Mute { get; }
    }

    public class PlayAudioClipEvent : DomainEventBase 
    {
        public PlayAudioClipEvent(int id, AudioClip clip, EPlayMode playMode)
        {
            Id       = id;
            Clip     = clip;
            PlayMode = playMode;
        }

        public int       Id       { get; }
        public AudioClip Clip     { get; }
        public EPlayMode PlayMode { get; }
    }

    public class FetchVolumnDataEvent : DomainEventBase
    {
        public FetchVolumnDataEvent(Action<IEnumerable<(int id, VolumnRate volumn)>> response)
        {
            Response = response;
        }

        public Action<IEnumerable<(int id, VolumnRate volumn)>> Response { get; }
    }
}