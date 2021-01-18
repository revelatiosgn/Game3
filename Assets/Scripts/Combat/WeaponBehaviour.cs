using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;
using ARPG.Controller;
using ARPG.Items;

namespace ARPG.Combat
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        protected Animator animator;
        protected Equipment equipment;
        protected PlayerController playerController;

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            equipment = GetComponent<Equipment>();
            playerController = GetComponent<PlayerController>();
            WeaponItem weaponItem = equipment.GetEquipmentSlot(EquipmentSlot.SlotType.Weapon).item as WeaponItem;
            animator.runtimeAnimatorController = weaponItem.animatorContoller;
            animator.SetLayerWeight(animator.GetLayerIndex(weaponItem.layerName), 1f);
        }

        protected virtual void Start()
        {
        }

        public abstract bool AttackBegin();
        public abstract void AttackEnd();

        public abstract bool DefenceBegin();
        public abstract void DefenceEnd();
    }
}
