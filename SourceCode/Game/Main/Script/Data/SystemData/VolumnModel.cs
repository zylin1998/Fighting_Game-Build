using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Loyufei;

namespace FightingGame.System
{
    public enum EPlayMode 
    {
        OneShot = 0,
        Loop    = 1,
    }

    public class VolumnModel
    {
        public VolumnModel([Inject(Id = GameNounDeclarations.Volumn)] IRepository data, List<AudioSource> audioSources) 
        {
            _Data       = data;
            _Sources    = audioSources.ToDictionary(k => k.name);
            _Names      = new()
            {
                { 0, GameNounDeclarations.Master },
                { 1, GameNounDeclarations.BGM    },
                { 2, GameNounDeclarations.SE    },
                { 3, GameNounDeclarations.SFX    },
            };

            _Data
                .SearchAll()
                .OfType<IReposit<VolumnRate>>()
                .ForEach(r => SetVolumn(_Names[(int)r.Identity], r.Data._Mute, r.Data._Volume / 100f));
        }

        private IRepository _Data;

        private Dictionary<int, string>         _Names;
        private Dictionary<string, AudioSource> _Sources;

        public void SetVolumn(int id, int rate) 
        {
            var volumn = Mathf.Clamp(rate, 0, 100);
            var data   = _Data.SearchAt(id).Data.To<VolumnRate>();
            
            data._Volume = volumn;

            SetVolumn(_Names[id], data._Mute, volumn / 100f);
        }

        public void SetVolumn(int id, bool mute) 
        {
            var data = _Data.SearchAt(id).Data.To<VolumnRate>();
            
            data._Mute = mute;

            SetVolumn(_Names[id], data._Mute, data._Volume / 100f);
        }

        public void Play(int id, AudioClip clip, EPlayMode playMode) 
        {
            switch(playMode) 
            {
                case EPlayMode.OneShot:
                    PlayOneShot(id, clip); break;
                case EPlayMode.Loop:
                    PlayLoop   (id, clip); break;
            }
        }

        public void PlayOneShot(int id, AudioClip clip) 
        {
            if (Mathf.Clamp(id, 0, 3) != id) { return; }

            var source = _Sources[_Names[id]];
            
            source.PlayOneShot(clip);
        }

        public void PlayLoop(int id, AudioClip clip)
        {
            if (Mathf.Clamp(id, 0, 2) != id) { return; }

            var source = _Sources[_Names[id]];

            source.clip = clip;
            source.loop = true;
            source.Play();
        }

        public void SetVolumn(string name, bool mute, float volume) 
        {
            var source = _Sources[name];
            
            source.mute   = mute;
            source.volume = volume;
        }

        public IEnumerable<(int id, VolumnRate volumn)> FetchData() 
        {
            return _Data.SearchAll().Select(r => ((int)r.Identity, (VolumnRate)r.Data));
        }
    }
}