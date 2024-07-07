using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace FightingGame
{
    public interface ISelectableGroup
    {
        public IEnumerable<Selectable> Selectables { get; }

        public Selectable First { get; }
        public Selectable LastSelect { get; set; }
    }
}
