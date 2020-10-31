using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Core;

namespace ARPG.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] InputHandler inputHandler;
        [SerializeField] InventoryPanel inventoryPanel;

        void Start()
        {
            inventoryPanel.gameObject.SetActive(false);
        }

        void Update()
        {
            if (InputHandler.inventoryInput)
            {
                inventoryPanel.gameObject.SetActive(!inventoryPanel.gameObject.activeSelf);
            }

            inputHandler.SetInUI(inventoryPanel.gameObject.activeSelf);
        }
    }
}

