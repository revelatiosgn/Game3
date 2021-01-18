using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    public abstract class Item : ScriptableObject
    {
        public string title = "Unnamed Item";
        public Sprite icon;

        public abstract void OnUse(GameObject target);
    }
}

