using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Combat;

namespace ARPG.Gear
{
    [System.Serializable]
    public class EquipmentWeaponSlot : EquipmentSlot
    {
        public enum Hand
        {
            Left,
            Right
        }

        [SerializeField] Transform leftHolder;
        [SerializeField] Transform rightHolder;
        [SerializeField] WeaponItem defaultItem;

        public override SlotType GetSlotType()
        {
            return SlotType.Weapon;
        }

        public override void Equip(EquipmentItem item, GameObject target)
        {
            base.Equip(item, target);

            WeaponItem weaponItem = item as WeaponItem;

            Transform parent = weaponItem.hand == Hand.Left ? leftHolder : rightHolder;
            
            if (parent != null)
                GameObject.Instantiate(weaponItem.prefab, parent);

            WeaponBehaviour weaponBehaviour = target.GetComponent<WeaponBehaviour>();
            if (weaponBehaviour != null)
                GameObject.DestroyImmediate(weaponBehaviour);

            if (weaponItem as MeleeWeaponItem)
                target.AddComponent<MeleeBehaviour>();
            else
                target.AddComponent<RangedBehaviour>();
        }

        public override void Unequip(GameObject target)
        {
            base.Unequip(target);

            while(leftHolder.childCount != 0)
                GameObject.DestroyImmediate(leftHolder.GetChild(0).gameObject);

            while(rightHolder.childCount != 0)
                GameObject.DestroyImmediate(rightHolder.GetChild(0).gameObject);

            WeaponBehaviour weaponBehaviour = target.GetComponent<WeaponBehaviour>();
            if (weaponBehaviour != null)
                GameObject.DestroyImmediate(weaponBehaviour);

            Equip(defaultItem, target);
        }
        
        public Transform GetWeaponTransform()
        {
            return leftHolder.GetChild(0) != null ? leftHolder.GetChild(0) : rightHolder.GetChild(0);
        }
    }
}


