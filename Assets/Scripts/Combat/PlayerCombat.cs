using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Core;
using ARPG.Controller;
using ARPG.Items;

namespace ARPG.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] WeaponProperty defaultWeapon;

        Animator animator;
        PlayerController playerController;
        Equipment equipment;
        WeaponBehaviour weaponBehaviour;

        void Awake()
        {
            animator = GetComponent<Animator>();
            playerController = GetComponent<PlayerController>();
            equipment = GetComponent<Equipment>();

            equipment.onEquip += OnEquip;
            equipment.onUnequip += OnUnequip;
        }

        void Update()
        {
            Attack();
        }

        void Attack()
        {
            if (!weaponBehaviour)
                return;

            if (InputHandler.attackBeginInput && !playerController.isInteracting)
            {
                playerController.isInteracting = true;
                weaponBehaviour.AttackBegin();
            }

            if (InputHandler.attackEndInput)
            {
                weaponBehaviour.AttackEnd();
            }
        }

        void OnEquip(Item item)
        {
            WeaponProperty property = item.property as WeaponProperty;
            weaponBehaviour = (WeaponBehaviour) gameObject.AddComponent(property.behaviour.GetClass());
            animator.runtimeAnimatorController = property.animatorController;
        }

        void OnUnequip(Item item)
        {
            if (weaponBehaviour)
            {
                Destroy(weaponBehaviour);
                weaponBehaviour = null;
            }
        }
    }
}


