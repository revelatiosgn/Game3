using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    public class ItemProperty : ScriptableObject
    {
        public Sprite icon;

        public virtual void Use(Item item)
        {
        }
    }
}


