using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "Arrow", menuName = "Statements/Equipment/Arrow", order = 1)]
    public sealed class ArrowStatement : ItemStatement
    {
        public GameObject arrowPrefab;
        public GameObject quiverPrefab;
        public float baseDamage;
        [Range(0f, 100f)] public float speed = 20f;
        [Range(0f, 10f)] public float gravity = 9.8f;
        public Vector3 launchOffset;

        public ArrowItem CreateItem()
        {
            ArrowItem item = new ArrowItem();
            item.statement = this;
            
            return item;
        }
    }
}

