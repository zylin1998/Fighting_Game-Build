using System;
using System.Collections;
using System.Collections.Generic;
using Loyufei.DomainEvents;
using UnityEngine;

namespace FightingGame.System
{
    public class ScreenModelPresenter : AggregateRoot
    {
        public ScreenModelPresenter(ScreenModel model, DomainEventService service) : base(service)
        {
            _Model   = model;

            service.Register<ScreenModeEvent>     (SetScreen, GroupId);
            service.Register<ResolutionEvent>     (SetScreen, GroupId);
            service.Register<FrameRateEvent>      (SetScreen, GroupId);
            service.Register<VSyncEvent>          (SetScreen, GroupId);
            service.Register<FetchScreenDataEvent>(FetchData, GroupId);
        }

        private ScreenModel _Model;
        
        public virtual object GroupId { get; }

        public void SetScreen(ScreenModeEvent screenMode) 
        {
            _Model.SetScreenMode(screenMode.ScreenMode);

            this.SettleEvents(GroupId, new SaveEvent("System"));
        }

        public void SetScreen(ResolutionEvent resolution)
        {
            _Model.SetResolution(resolution.Resolution);

            this.SettleEvents(GroupId, new SaveEvent("System"));
        }

        public void SetScreen(FrameRateEvent frameRate)
        {
            _Model.SetFrameRate(frameRate.FrameRate);

            this.SettleEvents(GroupId, new SaveEvent("System"));
        }

        public void SetScreen(VSyncEvent vSync)
        {
            _Model.SetVSync(vSync.VSync);

            this.SettleEvents(GroupId, new SaveEvent("System"));
        }

        public void FetchData(FetchScreenDataEvent fetch) 
        {
            fetch.Response?.Invoke(_Model.FetchData());
        }
    }

    public class ScreenModeEvent : DomainEventBase
    {
        public ScreenModeEvent(int mode) 
        {
            ScreenMode = mode;
        }

        public int ScreenMode { get; }
    }

    public class ResolutionEvent : DomainEventBase
    {
        public ResolutionEvent(int mode)
        {
            Resolution = mode;
        }

        public int Resolution { get; }
    }

    public class FrameRateEvent : DomainEventBase 
    {
        public FrameRateEvent(int frameRate) 
        {
            FrameRate = frameRate;
        }

        public int FrameRate { get; }
    }

    public class VSyncEvent : DomainEventBase
    {
        public VSyncEvent(bool vSync)
        {
            VSync = vSync;
        }

        public bool VSync { get; }
    }

    public class FetchScreenDataEvent : DomainEventBase 
    {
        public FetchScreenDataEvent(Action<(int resolution, int screenMode, int frameRate, bool vSync)> response)
        {
            Response = response;
        }

        public Action<(int resolution, int screenMode, int frameRate, bool vSync)> Response { get; }
    }
}