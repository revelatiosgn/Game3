using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "MeleeWeapon", menuName = "Statements/Equipment/MeleeWeapon", order = 1)]
    public sealed class MeleeWeaponStatement : WeaponStatement
    {
        public override WeaponItem CreateItem()
        {
            MeleeWeaponItem item = new MeleeWeaponItem();
            item.statement = this;
            
            return item;
        }
    }
}

