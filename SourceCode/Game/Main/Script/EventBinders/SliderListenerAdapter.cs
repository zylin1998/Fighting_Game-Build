using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FightingGame.Event;

namespace FightingGame
{
    public class SliderListenerAdapter : ListenerAdapter<Slider>
    {
        public override void AddListener(Action<object, object> action)
        {
            _Listener.onValueChanged.AddListener((param) => action.Invoke(Listener, param));
        }
    }
}