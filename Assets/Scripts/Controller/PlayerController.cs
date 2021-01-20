using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Core;
using ARPG.Movement;
using ARPG.Combat;

namespace ARPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] InputHandler inputHandler;

        PlayerMovement playerMovement;
        PlayerCombat playerCombat;

        Vector2 lastMovement;
        
        void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
            playerCombat = GetComponent<PlayerCombat>();
        }

        void OnEnable()
        {
            inputHandler.movementEvent += OnMovementEvent;
            inputHandler.attackBeginEvent += OnAttackBeginEvent;
            inputHandler.attackEndEvent += OnAttackEndEvent;
            inputHandler.defenceBeginEvent += OnDefenceBeginEvent;
            inputHandler.defenceEndEvent += OnDefenceEndEvent;
        }

        void OnDisable()
        {
            inputHandler.movementEvent -= OnMovementEvent;
            inputHandler.attackBeginEvent -= OnAttackBeginEvent;
            inputHandler.attackEndEvent -= OnAttackEndEvent;
            inputHandler.defenceBeginEvent -= OnDefenceBeginEvent;
            inputHandler.defenceEndEvent -= OnDefenceEndEvent;
        }

        void Update()
        {
            playerMovement.Move(lastMovement);
        }

        void OnMovementEvent(Vector2 value)
        {
            lastMovement = value;
        }

        void OnAttackBeginEvent()
        {
            playerCombat.AttackBegin();
        }

        void OnAttackEndEvent()
        {
            playerCombat.AttackEnd();
        }

        void OnDefenceBeginEvent()
        {
            playerCombat.DefenceBegin();
        }

        void OnDefenceEndEvent()
        {
            playerCombat.DefenceEnd();
        }
    }
}