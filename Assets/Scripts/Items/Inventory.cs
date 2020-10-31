using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] List<Item> items;
        [SerializeField] Transform weaponSlot;

        Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public List<Item> GetItems()
        {
            return items;
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public void Equip(WeaponItem item)
        {
            Unequip();

            GameObject weapon = Instantiate(item.prefab, weaponSlot);
            animator.runtimeAnimatorController = item.animatorOverrideController;
        }

        public void Unequip()
        {
            foreach(Transform weapon in weaponSlot)
                Destroy(weapon.gameObject);
        }
    }
}
