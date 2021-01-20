using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Core;
using System;

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

        void OnEnable()
        {
            inputHandler.inventoryEvent += OnInventoryEvent;
        }

        void OnDisable()
        {
            inputHandler.inventoryEvent -= OnInventoryEvent;
        }

        private void OnInventoryEvent()
        {
           inventoryPanel.gameObject.SetActive(!inventoryPanel.gameObject.activeSelf);
           inputHandler.SetInUI(inventoryPanel.gameObject.activeSelf);
        }
    }
}

