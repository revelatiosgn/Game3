using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Combat
{
    public class Equipment : MonoBehaviour
    {
        [SerializeField] List<WeaponHolder> weaponHolders;

        public Weapon currentWeapon;

        Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Equip(Weapon weapon)
        {
            Unequip();

            WeaponHolder weaponHolder = weaponHolders.Find(holder => holder.holderType == weapon.holderType);
            weaponHolder.Equip(weapon);
            animator.runtimeAnimatorController = weapon.animatorOverrideController;

            currentWeapon = weapon;
        }

        public void Unequip()
        {
            foreach(WeaponHolder weaponHolder in weaponHolders)
                weaponHolder.Unequip();

            currentWeapon = null;
        }

        public void SetDamaging(int value)
        {
            currentWeapon.SetDamaging(value > 0);
        }
    }
}
