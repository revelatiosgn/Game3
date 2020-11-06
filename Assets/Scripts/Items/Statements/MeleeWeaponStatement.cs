using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    [CreateAssetMenu(fileName = "MeleeWeapon", menuName = "MeleeWeapon", order = 1)]
    public class MeleeWeaponStatement : ItemStatement
    {
        public GameObject prefab;
        public float damage;
        public AnimatorOverrideController animatorContoller;
    }
}

