using FightingGame.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame.System 
{
    [Serializable]
    public class VolumnRate
    {
        public VolumnRate()
        {
            _Volume = 100;
            _Mute   = false;
        }

        [Range(0, 100)]
        public int  _Volume;
        public bool _Mute;
    }

    [Serializable]
    public class VolumnData : DataRepository<VolumnRate>
    {
        public VolumnData(int capacity) : base(capacity)
        {

        }

        protected override Reposit DefaultReposit(int id)
        {
            return new Reposit(id, new VolumnRate());
        }
    }
}
