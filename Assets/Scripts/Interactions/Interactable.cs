using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ARPG.Events;

namespace ARPG.Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] InteractableEvent onInteractableEnter;
        [SerializeField] InteractableEvent onInteractableExit;
        [SerializeField] Canvas canvas;
        [SerializeField] Text hintText;

        public string HUDHint;

        void Start()
        {
            if (canvas != null)
                canvas.worldCamera = GameObject.FindGameObjectWithTag(Constants.Tags.InteractionCamera).GetComponent<Camera>();

            if (hintText != null)
                hintText.text = GetHintText();

            SetHintActive(false);
        }

        void OnDestroy()
        {
            onInteractableExit.RaiseEvent(this);
        }

        public void SetHintActive(bool isHintActive)
        {
            if (canvas != null)
                canvas.gameObject.SetActive(isHintActive);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == Constants.Tags.Player == other.isTrigger)
            {
                onInteractableEnter.RaiseEvent(this);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.tag == Constants.Tags.Player == other.isTrigger)
            {
                onInteractableExit.RaiseEvent(this);
            }

            SetHintActive(false);
        }

        public abstract void Interact(GameObject target);
        protected abstract string GetHintText();
    }
}


