using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "RangedWeapon", menuName = "Statements/Equipment/RangedWeapon", order = 1)]
    public sealed class RangedWeaponStatement : WeaponStatement
    {
        public override WeaponItem CreateItem()
        {
            RangedWeaponItem item = new RangedWeaponItem();
            item.statement = this;
            
            return item;
        }
    }
}

