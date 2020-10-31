using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    public class Item : ScriptableObject
    {
        public string itemName;

        public virtual void Use() {}
    }
}


