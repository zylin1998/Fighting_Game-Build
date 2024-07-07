using Loyufei;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame.System 
{
    [Serializable]
    public class SystemSave : SaveSystem
    {
        public SystemData FetchData()
        {
            var save      = Saveable.To<SystemData>() ?? this.Load<SystemData>();
            var isDefault = save.IsDefault();

            Saveable      = isDefault ? new() : save;

            if (isDefault) { this.Save(); }

            return Saveable.To<SystemData>();
        }
    }
}