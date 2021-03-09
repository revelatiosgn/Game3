using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Combat;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "RangedWeapon", menuName = "Items/Equipment/RangedWeapon", order = 1)]
    public class RangedWeaponItem : WeaponItem
    {
        public override WeaponType GetWeaponType()
        {
            return WeaponType.Ranged;
        }

        public override string GetAnimatorLayer()
        {
            return "Ranged";
        }
    }
}

