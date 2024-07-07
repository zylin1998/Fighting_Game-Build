using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Loyufei.DomainEvents;

namespace FightingGame
{
    public class InventoryModelPresenter : AggregateRoot
    {
        public InventoryModelPresenter(InventoryModel model, DomainEventService service)
            : base(service)
        {
            _Model = model;

            service.Register<FetchItemEvent>   (FetchItem   , GroupId);
            service.Register<FetchItemsEvent>  (FetchItem   , GroupId);
            service.Register<ItemIncreaseEvent>(ItemIncrease, GroupId);
            service.Register<ItemPurchaseEvent>(ItemPurchase, GroupId);
            service.Register<SearchItemsEvent> (SearchItem  , GroupId);
        }

        private InventoryModel _Model;

        public virtual object GroupId { get; }

        public void FetchItem(FetchItemEvent fetch) 
        {
            var item =_Model.GetItem(fetch.Id);

            fetch.Response?.Invoke(item);
        }

        public void FetchItem(FetchItemsEvent fetch)
        {
            var items = _Model.GetItem(fetch.IdList.ToArray());

            fetch.Response?.Invoke(items);
        }

        public void SearchItem(SearchItemsEvent search) 
        {
            search.Response?.Invoke(_Model.GetItem(search.Predicate));
        }

        public void ItemIncrease(ItemIncreaseEvent increase) 
        {
            var done = _Model.Increase(increase.Id, increase.Count);

            if (done)
                this.SettleEvents(GroupId, new SaveEvent(GameNounDeclarations.Player));
        }

        public void ItemPurchase(ItemPurchaseEvent increase)
        {
            var done = _Model.Purchase(increase.Id, increase.Count);

            if (done)
                this.SettleEvents(GroupId, new SaveEvent(GameNounDeclarations.Player));
        }
    }

    public class FetchItemEvent : DomainEventBase 
    {
        public FetchItemEvent(int id, Action<(int count, IItem item)> response)
        {
            Id       = id;
            Response = response;
        }

        public int                             Id       { get; }
        public Action<(int count, IItem item)> Response { get; }
    }

    public class FetchItemsEvent : DomainEventBase
    {
        public FetchItemsEvent(IEnumerable<int> idList, Action<IEnumerable<(int id, int count, IItem item)>> response)
        {
            IdList   = idList;
            Response = response;
        }

        public IEnumerable<int>                                     IdList   { get; }
        public Action<IEnumerable<(int id, int count, IItem item)>> Response { get; }
    }

    public class SearchItemsEvent : DomainEventBase
    {
        public SearchItemsEvent(Func<IItem, bool> predicate, Action<IEnumerable<(int id, int count, IItem item)>> response)
        {
            Predicate = predicate;
            Response  = response;
        }

        public Func<IItem, bool>                                    Predicate { get; }
        public Action<IEnumerable<(int id, int count, IItem item)>> Response  { get; }
    }

    public class ItemIncreaseEvent : DomainEventBase 
    {
        public ItemIncreaseEvent(int id, int count)
        {
            Id = id;
            Count = count;
        }

        public int Id    { get; }
        public int Count { get; }
    }

    public class ItemPurchaseEvent : DomainEventBase
    {
        public ItemPurchaseEvent(int id, int count)
        {
            Id    = id;
            Count = count;
        }

        public int Id    { get; }
        public int Count { get; }
    }
}