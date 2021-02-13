﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using ARPG.Stats;
using ARPG.Events;
using ARPG.Interactions;
using ARPG.Core;

namespace ARPG.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] Text health;
        [SerializeField] Text interaction;

        [SerializeField] InteractableEvent onInteractableEnter;
        [SerializeField] InteractableEvent onInteractableExit;
        [SerializeField] VoidEvent onPlayerInteract;
        [SerializeField] InputActionsEvent onInputActionsUpdate;

        [SerializeField] InputHandler inputHandler;

        GameObject player;
        CharacterStats playerStats;
        SphereCollider interactionCollider;
        LayerMask environmentMask;

        public List<Interactable> interactables = new List<Interactable>();


        void Start()
        {
            player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
            playerStats = player.GetComponent<CharacterStats>();
            interactionCollider = player.GetComponent<SphereCollider>();

            environmentMask = LayerMask.GetMask("Environment");
        }

        void OnEnable()
        {
            onInteractableEnter.onEventRaised += OnInteractableEnter;
            onInteractableExit.onEventRaised += OnInteractableExit;
            onPlayerInteract.onEventRaised += OnPlayerInteract;
            onInputActionsUpdate.onEventRaised += OnInputActionsUpdate;
            interactables.Clear();
        }

        void OnDisable()
        {
            onInteractableEnter.onEventRaised -= OnInteractableEnter;
            onInteractableExit.onEventRaised -= OnInteractableExit;
            onPlayerInteract.onEventRaised -= OnPlayerInteract;
            onInputActionsUpdate.onEventRaised -= OnInputActionsUpdate;
            interactables.Clear();
        }

        void Update()
        {
            UpdateHealth();
            UpdateInteractions();
        }

        void UpdateHealth()
        {
            health.text = playerStats.health.ToString();
        }

        void UpdateInteractions()
        {
            Interactable interactable = GetInteractable();
            
            if (interactable != null)
            {
                interaction.gameObject.SetActive(true);
                interaction.text = inputHandler.inputActions.PlayerActions.Interact.GetBindingDisplayString();
                interactable.SetHintActive(true);
            }
            else
            {
                interaction.gameObject.SetActive(false);
            }
        }

        Interactable GetInteractable()
        {
            Interactable target = null;
            float minDot = float.MinValue;

            foreach (Interactable interactable in interactables)
            {
                interactable.SetHintActive(false);

                RaycastHit hit;
                Vector3 src = interactionCollider.transform.position + interactionCollider.center;
                Vector3 dir = (interactable.transform.position - src).normalized;
                if (!Physics.Raycast(src, dir, out hit, dir.magnitude, environmentMask))
                {
                    float dot = Vector3.Dot(dir, interactionCollider.transform.forward);
                    if (dot > minDot)
                    {
                        minDot = dot;
                        target = interactable;
                    }
                }
            }

            return target;
        }

        void OnInteractableEnter(Interactable interactable)
        {
            interactables.Add(interactable);
        }

        void OnInteractableExit(Interactable interactable)
        {
            interactables.Remove(interactable);
        }

        void OnPlayerInteract()
        {
            Interactable interactable = GetInteractable();
            if (interactable != null)
                interactable.Interact(player);
        }

        void OnInputActionsUpdate(InputActions inputActions)
        {
        }
    }
}


