using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loyufei;

namespace FightingGame.Player
{
    [Serializable]
    public class InventoryData : DataRepository<int> 
    {
        public InventoryData(int capacity) : base(capacity)
        {

        }

        protected override Reposit DefaultReposit(int id)
        {
            return new Reposit(id, 0);
        }
    }

    [Serializable]
    public class DataRepository<TData> : IRepository<TData>
    {
        [Serializable]
        public class Reposit : RepositBase<int, TData> 
        {
            public Reposit(int id, TData data) : base(id, data) 
            {

            }
        }

        public DataRepository()
        {
            _Reposits = new Reposit[0];
        }

        public DataRepository(int capacity)
        {
            var id = 0;
            _Reposits = new Reposit[capacity].Select(r => DefaultReposit(id++)).ToArray();
        }

        public DataRepository(IEnumerable<Reposit> reposits) 
        {
            _Reposits = reposits.ToArray();
        }

        [SerializeField]
        protected Reposit[] _Reposits;

        public int Capacity => _Reposits.Length;

        public IReposit<TData> SearchAt(object id)
        {
            return id is int i ? SearchAt(i) : DefaultReposit(int.MaxValue);
        }

        public virtual IReposit<TData> SearchAt(int id) 
        {
            if (id >= Capacity) 
            {
                return DefaultReposit(int.MaxValue); 
            }

            var reposit = _Reposits[id];

            return reposit;
        }

        public IReposit<TData> Search(Func<IReposit<TData>, bool> predicate) 
        {
            Debug.Log("Not using method");

            return DefaultReposit(int.MaxValue);
        }

        public IEnumerable<IReposit<TData>> SearchAll()
        {
            return _Reposits;
        }

        public IEnumerable<IReposit<TData>> SearchAll(Func<IReposit<TData>, bool> predicate) 
        {
            return _Reposits.Where(predicate);
        }

        public void Sort(Comparison<IReposit<TData>> comparison) 
        {
            Debug.Log("Not using method");
        }

        protected virtual Reposit DefaultReposit(int id) 
        {
            return new(id, default);
        }
    }
}