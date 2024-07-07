using System;
using System.Collections;
using System.Collections.Generic;
using Loyufei;

namespace FightingGame.Player
{
    [Serializable]
    public class PlayerSave : SaveSystem
    {
        public PlayerData FetchData() 
        {
            var save      = Saveable.To<PlayerData>() ?? this.Load<PlayerData>();
            var isDefault = save.IsDefault();

            Saveable      = isDefault ? new(new(3), new(10)) : save;

            if (isDefault) { this.Save(); }

            return Saveable.To<PlayerData>();
        }
    }
}