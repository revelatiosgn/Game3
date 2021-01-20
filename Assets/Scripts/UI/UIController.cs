using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Core;
using System;
using ARPG.Events;

namespace ARPG.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] VoidEvent onPlayerInventory;
        [SerializeField] BoolEvent onLockPlayerActions;
        [SerializeField] InventoryPanel inventoryPanel;

        void Start()
        {
            inventoryPanel.gameObject.SetActive(false);
        }

        void OnEnable()
        {
            onPlayerInventory.onEventRaised += OnPlayerInventory;
        }

        void OnDisable()
        {
            onPlayerInventory.onEventRaised -= OnPlayerInventory;
        }

        private void OnPlayerInventory()
        {
           inventoryPanel.gameObject.SetActive(!inventoryPanel.gameObject.activeSelf);
           onLockPlayerActions.RaiseEvent(inventoryPanel.gameObject.activeSelf);
        }
    }
}

