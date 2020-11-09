using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    [System.Serializable]
    public sealed class PotionItem : Item
    {
        public PotionStatement statement;

        public override Sprite GetIcon()
        {
            return statement.icon;
        }

        public override void OnUse()
        {
            Debug.Log("heal " + statement.hp);
        }

        public override bool IsEquals(Item other)
        {
            PotionItem potionItem = other as PotionItem;
            if (potionItem != null && potionItem.statement == statement)
                return true;

            return false;
        }
    }
}

