using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Items;
using ARPG.Combat;

namespace ARPG.Gear
{
    [System.Serializable]
    public sealed class EquipmentWeaponSlot : EquipmentSlot
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

            AddBehaviour(target);
        }

        public override void Unequip(GameObject target)
        {
            int childCount = leftHolder.childCount;
            for (int i = childCount - 1; i >= 0; i--)
                Destroy(leftHolder.GetChild(i).gameObject);

            childCount = rightHolder.childCount;
            for (int i = childCount - 1; i >= 0; i--)
                Destroy(rightHolder.GetChild(i).gameObject);

            target.GetComponent<BaseCombat>().WeaponBehaviour = null;

            base.Unequip(target);
        }
        
        public override void AddBehaviour(GameObject target)
        {
            if (item == null)
                return;

            WeaponItem weaponItem = item as WeaponItem;
            BaseCombat combat = target.GetComponent<BaseCombat>();
            if (weaponItem as MeleeWeaponItem)
                combat.WeaponBehaviour = new MeleeBehaviour(combat);
            else
                combat.WeaponBehaviour = new RangedBehaviour(combat);
        }
        
        public Transform GetWeaponTransform()
        {
            return leftHolder.GetChild(0) != null ? leftHolder.GetChild(0) : rightHolder.GetChild(0);
        }
    }
}


