using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using FightingGame.Event;

namespace FightingGame
{
    public class DropdownListenerAdapter : ListenerAdapter<TMP_Dropdown>
    {
        public override void AddListener(Action<object, object> action)
        {
            _Listener.onValueChanged.AddListener((param) => action.Invoke(Listener, param));
        }
    }
}