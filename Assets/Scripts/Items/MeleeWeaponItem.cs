using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Combat;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "MeleeWeapon", menuName = "Items/Equipment/Weapon/Melee", order = 1)]
    public sealed class MeleeWeaponItem : WeaponItem
    {
        [Range(0f, 360f)] public float angle = 180f;
    }
}

