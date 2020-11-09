using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Gear;
using ARPG.Combat;

namespace ARPG.Items
{
    [System.Serializable]
    public sealed class RangedWeaponItem : WeaponItem
    {
        public RangedWeaponStatement statement;

        public override WeaponStatement GetStatement()
        {
            return statement;
        }

        protected override void AddBehaviour(Transform target)
        {
            target.gameObject.AddComponent<RangedBehaviour>();
        }
    }
}