using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ARPG.Items;

namespace ARPG.UI
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] Text itemName;
        [SerializeField] Button button;

        Item item;

        public void SetItem(Item item)
        {
            this.item = item;

            itemName.text = item.itemName;
            button.onClick.AddListener(() => UseItem());
        }

        void UseItem()
        {
            item.Use();
        }
    }
}
