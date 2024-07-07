using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FightingGame.Event;

namespace FightingGame
{
    public class ButtonListenerAdapter : ListenerAdapter<Button>
    {
        public override void AddListener(Action<object, object> action)
        {
            _Listener.onClick.AddListener(() => action.Invoke(Listener, Id));
        }
    }
}