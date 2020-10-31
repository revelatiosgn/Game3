using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Combat
{
    public class WeaponHolder : MonoBehaviour
    {
        public enum HolderType
        {
            LeftHand,
            RightHand
        }

        public HolderType holderType;

        Weapon currentWeapon;

        public void Equip(Weapon weapon)
        {
            Unequip();
            
            weapon.transform.parent = transform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            currentWeapon = weapon;
        }

        public void Unequip()
        {
            if (currentWeapon)
                Destroy(currentWeapon.gameObject);
        }
    }
}


