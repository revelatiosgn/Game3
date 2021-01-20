using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;
using ARPG.Controller;
using ARPG.Items;

namespace ARPG.Combat
{
    public abstract class WeaponBehaviour : EquipmentBehaviour
    {
        protected virtual void OnEnable()
        {
            WeaponItem weaponItem = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon).item as WeaponItem;
            animator.runtimeAnimatorController = weaponItem.animatorContoller;
            animator.SetLayerWeight(animator.GetLayerIndex(weaponItem.maskLayer), 1f);
        }

        protected virtual void OnDisable()
        {
            WeaponItem weaponItem = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon).item as WeaponItem;
            animator.runtimeAnimatorController = weaponItem.animatorContoller;
            animator.SetLayerWeight(animator.GetLayerIndex(weaponItem.maskLayer), 0f);
        }
        
        public abstract bool AttackBegin();
        public abstract void AttackEnd();

        public abstract bool DefenceBegin();
        public abstract void DefenceEnd();
    }
}
