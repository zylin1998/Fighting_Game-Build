using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame.Event
{
    public abstract class ListenerAdapter<TListener> : ListenerAdapter, IListenerAdapter<TListener> 
    {
        [SerializeField]
        protected TListener _Listener;

        public TListener Listener => _Listener;
    }

    public abstract class ListenerAdapter : MonoBehaviour, IListenerAdapter
    {
        [SerializeField]
        protected int _Id;

        public int Id => _Id;

        public abstract void AddListener(Action<object, object> action);
    }

    public interface IListenerAdapter<TListener> : IListenerAdapter
    {
        public TListener Listener { get; }
    }

    public interface IListenerAdapter
    {
        public int Id { get; }

        public void AddListener(Action<object, object> action);
    }
}