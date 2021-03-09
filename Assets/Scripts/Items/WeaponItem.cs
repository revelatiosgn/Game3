using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;
using ARPG.Combat;

namespace ARPG.Items
{
    public abstract class WeaponItem : EquipmentItem
    {
        public enum WeaponType
        {
            LightMelee,
            HeavyMelee,
            Ranged
        }

        public WeaponBehaviour prefab;
        public string animationLayer;
        public float baseDamage = 0f;

        public override EquipmentSlot.Type GetSlotType()
        {
            return EquipmentSlot.Type.Weapon;
        }

        public abstract WeaponType GetWeaponType();
        public abstract string GetAnimatorLayer();
    }
}

