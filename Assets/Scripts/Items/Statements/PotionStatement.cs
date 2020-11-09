using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "Potion", menuName = "Statements/Potion", order = 1)]
    public sealed class PotionStatement : ItemStatement
    {
        public float hp;
    }
}

