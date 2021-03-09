using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Combat;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "MeleeWeapon", menuName = "Items/Equipment/MeleeWeapon", order = 1)]
    public class MeleeWeaponItem : WeaponItem
    {
        public enum MeleeType
        {
            Light,
            Heavy
        }

        public MeleeType meleeType;
        public float attackRange = 1f;
        public float attackAngle = 1f;
        
        public override WeaponType GetWeaponType()
        {
            return meleeType == MeleeType.Light ? WeaponType.LightMelee : WeaponType.HeavyMelee;
        }

        public override string GetAnimatorLayer()
        {
            return "Melee";
        }
    }
}

