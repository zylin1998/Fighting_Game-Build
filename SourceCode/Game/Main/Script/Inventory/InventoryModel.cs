using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Loyufei;
using Zenject;
using UnityEngine;

namespace FightingGame
{
    public class InventoryModel
    {
        public  InventoryModel(
            [Inject(Id = GameNounDeclarations.Inventory)] IRepository data,
            [Inject(Id = GameNounDeclarations.Inventory)] IEntityForm asset) 
        {
            _Data  = data;
            _Asset = asset;
        }

        private IRepository _Data;
        private IEntityForm _Asset;

        public (int count, IItem item) GetItem(int id) 
        {
            var item    = _Asset[id].Data.To<IItem>();
            var reposit = _Data.SearchAt(id).To<IReposit<int>>();
            var count   = reposit.Data;
            
            return (count, item);
        }

        public IEnumerable<(int id, int count, IItem item)> GetItem(params int[] idList)
        {
            return idList.Select(id =>
            {
                var item = _Asset[id].Data.To<IItem>();
                var count = _Data.SearchAt(id).Data.To<int>();
                
                return (id, count, item);
            });
        }

        public IEnumerable<(int id, int count, IItem item)> GetItem(Func<IItem, bool> predicate)
        {
            return _Asset
                .Where(e => predicate((IItem)e.Data))
                .Select(e =>
                {
                    var id    = (int)e.Identity;
                    var count = (int)_Data.SearchAt(id).Data;
                    var item  = (IItem)e.Data;
                    
                    return (id, count, item);
                })
                .ToArray();
        }

        public bool Increase(int id, int count) 
        {
            var reposit   = _Data.SearchAt(id).To<IReposit<int>>();
            var item      = _Asset[id].Data.To<IItem>();
            var preserve  = reposit.Data + count;
            var available = item.Limit <= 0 || item.Limit > preserve;

            if (available) 
            {
                reposit.Preserve(preserve);
            }

            return available;
        }

        public bool Purchase(int id, int count) 
        {
            var currency = _Data.SearchAt(0).To<IReposit<int>>();
            var reposit  = _Data.SearchAt(id).To<IReposit<int>>();
            var purchase = _Asset[id].Data.To<IPurchase>();

            if (purchase.IsDefault()) { return false; }

            var spend    = purchase.Price * count;
            var afford   = spend <= currency.Data;

            if (afford) 
            {
                currency.Preserve(currency.Data - spend);

                reposit.Preserve(reposit.Data + count);
            }

            return afford;
        }
    }
}