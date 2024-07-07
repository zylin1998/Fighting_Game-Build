using Loyufei;
using Loyufei.DomainEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame
{
    public class DataUpdaterPresenter 
    {
        public DataUpdaterPresenter(IEnumerable<IUpdateGroup> groups, DomainEventService service) 
        {
            Updater = new();

            groups.ForEach(group =>
            {
                group.Contexts.ForEach(c => Updater.Register(c));
            });

            service.Register<UpdateData>(Update, GroupId);
        }

        public virtual object GroupId { get; }

        public DataUpdater Updater { get; }

        public void Update(UpdateData update) 
        {
            Updater.Update(update.Id, update.Value);
        }
    }

    public class UpdateData : DomainEventBase 
    {
        public UpdateData(object id, object value) 
        {
            Id    = id;
            Value = value;
        }

        public object Id    { get; }
        public object Value { get; }
    }
}