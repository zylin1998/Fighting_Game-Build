using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FightingGame.System 
{
    [Serializable]
    public class ScreenData
    {
        public ScreenData()
        {
            _FullScreenMode = FullScreenMode.ExclusiveFullScreen;
            _Resolution     = 0;
            _FrameRate      = 0;
            _VSync          = false;
        }

        [Header("畫面設定")]
        public FullScreenMode _FullScreenMode;
        [Min(0)]
        public int _Resolution;
        [Min(0)]
        public int _FrameRate;
        public bool _VSync;
    }
}