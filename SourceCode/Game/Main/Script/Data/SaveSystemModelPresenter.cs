using Loyufei.DomainEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    public class SaveSystemModelPresenter
    {
        public SaveSystemModelPresenter(SaveSystemModel model, DomainEventService service)
        {
            _Model = model;

            service.Register<SaveEvent>(Save, GroupId);
        }

        private SaveSystemModel _Model;

        public virtual object GroupId { get; }

        public void Save(SaveEvent save) 
        {
            _Model.Save(save.Id);
        }
    }

    public class SaveEvent : DomainEventBase
    {
        public SaveEvent(string id)
        {
            Id = id;
        }   

        public string Id { get; }
    }
}