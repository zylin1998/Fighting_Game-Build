using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FightingGame.Event;

namespace FightingGame
{
    public class ToggleListenerAdapter : ListenerAdapter<Toggle>
    {
        public override void AddListener(Action<object, object> action)
        {
            _Listener.onValueChanged.AddListener((isOn) => action.Invoke(Listener, isOn));
        }
    }
}