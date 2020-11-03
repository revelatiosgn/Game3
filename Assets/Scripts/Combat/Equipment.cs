using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using ARPG.Items;

namespace ARPG.Combat
{
    public class Equipment : MonoBehaviour
    {
        public List<WeaponHolder> weaponHolders;
        public WeaponItemProperty defaultWeapon;
        public WeaponBehaviour weaponBehaviour;

        Animator animator;

        public UnityAction<Item> onEquip;
        public UnityAction<Item> onUnequip;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            EquipDefaultWeapon();
        }

        void EquipDefaultWeapon()
        {   
            if (GetWeaponHolder(WeaponHolder.HolderType.LeftHand).item == null && GetWeaponHolder(WeaponHolder.HolderType.RightHand).item == null)
            {
                Item item = new Item();
                item.property = defaultWeapon;
                EquipWeapon(item);
            }
        }

        public void EquipWeapon(Item item)
        {
            WeaponItemProperty property = item.GetItemProperty<WeaponItemProperty>();

            Unequip(WeaponHolder.HolderType.LeftHand);
            Unequip(WeaponHolder.HolderType.RightHand);

            animator.runtimeAnimatorController = property.animatorOverrideController;
            weaponBehaviour = (WeaponBehaviour) gameObject.AddComponent(property.behaviour.GetClass());

            WeaponHolder weaponHolder = GetWeaponHolder(property.holderType);
            weaponHolder.Equip(item);

            onEquip(item);
        }

        public void Unequip(WeaponHolder.HolderType holderType)
        {
            Item item = GetWeaponHolder(holderType).item;
            GetWeaponHolder(holderType).Unequip();
            if (weaponBehaviour)
                Destroy(weaponBehaviour);
                
            if (item != null)
                onUnequip(item);
        }

        public void EquipArrows(Item item)
        {
            MissileItemProperty property = item.GetItemProperty<MissileItemProperty>();

            Unequip(WeaponHolder.HolderType.Back);

            WeaponHolder weaponHolder = GetWeaponHolder(WeaponHolder.HolderType.Back);
            weaponHolder.Equip(item);

            onEquip(item);
        }

        public WeaponHolder GetWeaponHolder(WeaponHolder.HolderType holderType)
        {
            return weaponHolders.Find(holder => holder.holderType == holderType);
        }
        
        public WeaponHolder GetWeaponHolder(Item item)
        {
            return weaponHolders.Find(holder => holder.item == item);
        }
    }
}
