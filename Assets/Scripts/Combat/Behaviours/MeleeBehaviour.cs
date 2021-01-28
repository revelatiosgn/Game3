using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

using ARPG.Core;
using ARPG.Movement;
using ARPG.Items;
using ARPG.Stats;
using ARPG.Controller;

namespace ARPG.Combat
{
    public class MeleeBehaviour : WeaponBehaviour
    {
        private bool isAttacking = false;

        public MeleeBehaviour(BaseCombat combat) : base(combat)
        {
        }
        
        public override bool DefenceBegin()
        {
            animator.SetBool("defence", true);

            return true;
        }

        public override bool DefenceEnd()
        {
            animator.SetBool("defence", false);

            return true;
        }

        public override bool AttackBegin()
        {
            if (controller.isInteracting)
                return false;

            if (isAttacking)
                return false;

            animator.SetTrigger("attack");
            isAttacking = true;
            animator.SetLayerWeight(maskLayerIndex, 0f);
            controller.isInteracting = true;

            return true;
        }

        public override bool AttackEnd()
        {
            return true;
        }

        public override void OnAttackComplete()
        {
            isAttacking = false;
            animator.SetLayerWeight(maskLayerIndex, 1f);
            controller.isInteracting = false;
        }

        public override void OnAnimationEvent(string animationEvent)
        {
            if (animationEvent == "Attack")
            {
                isAttacking = false;
                controller.isInteracting = false;

                MeleeWeaponItem weaponItem = item as MeleeWeaponItem;

                GameObject collisionObject = new GameObject();
                collisionObject.transform.position = combat.transform.position;
                SphereCollider collider = collisionObject.AddComponent<SphereCollider>();
                collider.isTrigger = true;
                collider.radius = weaponItem.range;
                collider.center = Vector3.up * collider.radius * 0.5f;

                MeleeCollider meleeCollider = collisionObject.AddComponent<MeleeCollider>();
                meleeCollider.weaponEvent.AddListener(OnWeaponEvent);
            }
            else if (animationEvent == "Death")
            {
                animator.SetLayerWeight(maskLayerIndex, 0f);
            }
        }

        public override void OnAnimatorIK(int layerIndex)
        {
        }

        void OnWeaponEvent(Transform target)
        {
            if (target.GetComponent<BaseController>().characterGroup == controller.characterGroup)
                return;

            MeleeWeaponItem weaponItem = item as MeleeWeaponItem;
            float angle = Vector3.Angle(target.transform.position - combat.transform.position, combat.transform.forward);
            if (angle * 2f < weaponItem.angle)
                target.GetComponent<CharacterStats>().TakeDamage(item.baseDamage);
        }
    }
}


