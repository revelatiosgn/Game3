using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Combat;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "MeleeWeapon", menuName = "Items/Equipment/MeleeWeapon", order = 1)]
    public sealed class MeleeWeaponItem : WeaponItem
    {
        [Range(0f, 10f)] public float range = 3f;
        [Range(0f, 360f)] public float angle = 180f;
    }
}

