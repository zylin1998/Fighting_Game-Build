using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace FightingGame.System
{
    public class ScreenModel
    {
        public ScreenModel([Inject(Id = GameNounDeclarations.Screen)] ScreenData data) 
        {
            _Data             = data;
            _Resolutions      = Screen.resolutions
                .Reverse()
                .Where((r) => r.width / 16 == r.height / 9 || r.width / 21 == r.height / 9)
                .ToList();
            _TargetFrameRates = new() { -1, 60, 30 };

            SetScreen(_Data._Resolution, _Data._FullScreenMode);
            SetFrameRate(_Data._FrameRate);
            SetVSync(_Data._VSync);
        }

        private ScreenData       _Data;

        private List<Resolution> _Resolutions;
        private List<int>        _TargetFrameRates;

        public void SetScreenMode(int mode) 
        {
            SetScreenMode((FullScreenMode)Mathf.Clamp(mode, 0, 3));
        }

        public void SetScreenMode(FullScreenMode mode)
        {
            _Data._FullScreenMode = mode;

            SetScreen(_Resolutions[_Data._Resolution], _Data._FullScreenMode);
        }

        public void SetResolution(int rate)
        {
            _Data._Resolution = Mathf.Clamp(rate, 0, _Resolutions.Count - 1);

            SetScreen(_Resolutions[_Data._Resolution], _Data._FullScreenMode);
        }

        public void SetScreen(int resolution, FullScreenMode screenMode) 
        {
            SetScreen(_Resolutions[resolution], screenMode);
        }

        public void SetScreen(Resolution resolution, FullScreenMode mode) 
        {
            var width  = resolution.width;
            var height = resolution.height;
            
            Screen.SetResolution(width, height, mode);
        }

        public void SetFrameRate(int frameRate) 
        {
            _Data._FrameRate = frameRate;

            Application.targetFrameRate = _TargetFrameRates[frameRate];
        }

        public void SetVSync(bool vsync) 
        {
            _Data._VSync = vsync;

            QualitySettings.vSyncCount = vsync ? 1 : 0;
        }

        public (int resolution, int screenMode, int frameRate, bool vSync) FetchData()
        {
            return (_Data._Resolution, (int)_Data._FullScreenMode, _Data._FrameRate, _Data._VSync);
        }
    }
}