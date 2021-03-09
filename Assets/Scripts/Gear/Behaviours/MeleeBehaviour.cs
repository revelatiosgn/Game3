using System.Collections;
using System.Collections.Generic;
using ARPG.Gear;
using UnityEngine;

using ARPG.Core;
using ARPG.Movement;
using ARPG.Items;
using ARPG.Stats;
using ARPG.Controller;

namespace ARPG.Gear
{
    public class MeleeBehaviour : WeaponBehaviour
    {
        private bool isAttacking = false;

        private int defenceLayerIndex;

        public override void Dispose()
        {
            base.Dispose();

            // animator.SetLayerWeight(defenceLayerIndex, 0f);
        }

        protected override void OnAttackBegin()
        {
            Debug.Log("MELEE ATTACK BEGIN");
        }

        protected override void OnAttackEnd()
        {
            Debug.Log("MELEE ATTACK END");
        }

        protected override void OnAttackComplete()
        {
            Debug.Log("MELEE ATTACK COMPLETE");
        }

        // public override void AttackBegin()
        // {
        //     if (isAttacking)
        //         return;

        //     animator.SetTrigger("attack");
        //     isAttacking = true;

        //     return;
        // }
        
        // public override void DefenceBegin()
        // {
        //     animator.SetBool("defence", true);
        // }

        // public override void DefenceEnd()
        // {
        //     animator.SetBool("defence", false);
        // }

        // public override void OnAttackComplete()
        // {
        //     isAttacking = false;
        // }

        // public override void OnAnimationEvent(string animationEvent)
        // {
        //     base.OnAnimationEvent(animationEvent);

        //     if (animationEvent == "Attack")
        //     {
        //         MeleeWeaponItem weaponItem = item as MeleeWeaponItem;

        //         GameObject collisionObject = new GameObject();
        //         collisionObject.transform.position = baseCombat.transform.position;
        //         SphereCollider collider = collisionObject.AddComponent<SphereCollider>();
        //         collider.isTrigger = true;
        //         collider.radius = weaponItem.attackRange;
        //         collider.center = Vector3.up * collider.radius * 0.5f;

        //         MeleeCollider meleeCollider = collisionObject.AddComponent<MeleeCollider>();
        //         meleeCollider.weaponEvent.AddListener(OnWeaponEvent);
        //     }
        //     else if (animationEvent == "AttackComplete")
        //     {
        //         OnAttackComplete();
        //     }
        // }

        // void OnWeaponEvent(Transform target)
        // {
        //     BaseController targetController = target.GetComponent<BaseController>();

        //     if (targetController.characterGroup == baseController.characterGroup)
        //         return;

        //     MeleeWeaponItem weaponItem = item as MeleeWeaponItem;
        //     float angle = Vector3.Angle(target.transform.position - baseCombat.transform.position, baseCombat.transform.forward);
        //     if (angle * 2f < weaponItem.attackAngle)
        //         targetController.OnTakeDamage(baseController, item.baseDamage);
        // }
    }
}


