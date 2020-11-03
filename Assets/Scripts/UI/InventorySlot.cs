using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ARPG.Inventory;
using ARPG.Items;

namespace ARPG.UI
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] GameObject selected;
        [SerializeField] Image icon;

        ItemSlot itemSlot;
        Button button;

        public ItemSlot ItemSlot
        {
            get => itemSlot;
            set
            {
                itemSlot = value;
                icon.sprite = itemSlot.item.property.icon;
                icon.gameObject.SetActive(true);
            }
        }

        void Awake()
        {
            button = GetComponent<Button>();

            SetEquipped(false);
        }

        void Start()
        {
            button.onClick.AddListener(() => UseItem());
        }

        void UseItem()
        {
            if (itemSlot != null)
                itemSlot.item.UseItem();
        }

        public void SetEquipped(bool value)
        {
            selected.SetActive(value);
        }
    }
}
