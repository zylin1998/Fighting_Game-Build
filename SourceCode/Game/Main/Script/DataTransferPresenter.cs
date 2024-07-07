using System.Collections;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace FightingGame
{
    public class DataTransferPresenter
    {
        public DataTransferPresenter(TargetQuest targetQuest, DomainEventService service) 
        {
            _TargetQuest = targetQuest;
            
            service.Register<SetQuest>    (SetQuest, GroupId);
            service.Register<SetNextQuest>(SetQuest, GroupId);
        }

        public virtual object GroupId { get; }

        private TargetQuest _TargetQuest;

        public void SetQuest(SetQuest targetQuest) 
        {
            _TargetQuest.Id = targetQuest.Id;
        }

        public void SetQuest(SetNextQuest target) 
        {
            _TargetQuest.Id++;
        }
    }

    public class SetQuest : DomainEventBase
    {
        public SetQuest(int id) 
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class SetNextQuest : DomainEventBase 
    {

    }
}