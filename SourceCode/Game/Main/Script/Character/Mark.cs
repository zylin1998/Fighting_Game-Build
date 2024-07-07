using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame 
{
    [Serializable]
    public class Mark
    {
        public Mark(int id, string characterId, string characterType)
        {
            _Id            = id;
            _CharacterId   = characterId;
            _CharacterType = characterType;
        }

        [SerializeField]
        private int    _Id;
        [SerializeField]
        private string _CharacterId;
        [SerializeField]
        private string _CharacterType;

        public int    Id            => _Id;
        public string CharacterId   => _CharacterId;
        public string CharacterType => _CharacterType;
    }
}
