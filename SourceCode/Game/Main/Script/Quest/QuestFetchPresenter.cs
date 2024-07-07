using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loyufei.DomainEvents;

namespace FightingGame
{
    public class QuestFetchPresenter
    {
        public QuestFetchPresenter(QuestFetchModel model, DomainEventService service)
        {
            _Model = model;

            service.Register<FetchQuestsEvent>(FetchQuest, GroupId);
            service.Register<FetchQuestEvent> (FetchQuest, GroupId);
        }

        private QuestFetchModel _Model;

        public virtual object GroupId { get; }

        public void FetchQuest(FetchQuestsEvent fetch) 
        {
            fetch.Response?.Invoke(_Model.GetQuestInfo());
        }

        public void FetchQuest(FetchQuestEvent fetch)
        {
            fetch.Response?.Invoke(_Model.GetQuestInfo(fetch.Id));
        }
    }

    public class FetchQuestsEvent : DomainEventBase 
    {
        public FetchQuestsEvent(Action<IEnumerable<(int id, QuestInfo info, bool clear)>> response) 
        {
            Response = response;
        }

        public Action<IEnumerable<(int id, QuestInfo info, bool clear)>> Response { get; }
    }

    public class FetchQuestEvent : DomainEventBase
    {
        public FetchQuestEvent(int id, Action<QuestInfo> response)
        {
            Id       = id;
            Response = response;
        }

        public int               Id       { get; }
        public Action<QuestInfo> Response { get; }
    }
}