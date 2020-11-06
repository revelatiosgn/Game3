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
        [SerializeField] Image icon;
        [SerializeField] Text count;
        [SerializeField] Image equipped;

        ItemSlot itemSlot;
        Button button;

        public ItemSlot ItemSlot
        {
            get => itemSlot;
            set
            {
                itemSlot = value;

                if (itemSlot == null)
                {
                    Reset();
                    return;
                }

                icon.sprite = itemSlot.item.GetIcon();
                icon.gameObject.SetActive(true);
                
                if (itemSlot.count > 1)
                {
                    count.gameObject.SetActive(true);
                    count.text = itemSlot.count.ToString();
                }
                else
                {
                    count.gameObject.SetActive(false);
                }
            }
        }

        void Awake()
        {
            button = GetComponent<Button>();

            Reset();
        }

        void Start()
        {
            button.onClick.AddListener(() => UseItem());
        }

        public void SetEquipped(bool value)
        {
            equipped.gameObject.SetActive(value);
        }

        void UseItem()
        {
            if (itemSlot != null)
                itemSlot.item.OnUse(null);
        }

        void Reset()
        {
            icon.gameObject.SetActive(false);
            count.gameObject.SetActive(false);
            SetEquipped(false);
        }
    }
}
