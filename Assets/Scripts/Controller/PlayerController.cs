using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ARPG.Core;
using ARPG.Movement;
using ARPG.Combat;
using ARPG.Events;

namespace ARPG.Controller
{
    public class PlayerController : BaseController
    {
        [SerializeField] Vector2Event onPlayerMove;
        [SerializeField] VoidEvent onPlayerAttackBegin;
        [SerializeField] VoidEvent onPlayerAttackEnd;
        [SerializeField] VoidEvent onPlayerDefenceBegin;
        [SerializeField] VoidEvent onPlayerDefenceEnd;
        [SerializeField] VoidEvent onPlayerJump;
        [SerializeField] BoolEvent onPlayerSprint;

        PlayerMovement playerMovement;
        PlayerCombat playerCombat;

        Vector2 lastMovement;
        
        protected override void Awake()
        {
            base.Awake();

            playerMovement = GetComponent<PlayerMovement>();
            playerCombat = GetComponent<PlayerCombat>();
        }

        void OnEnable()
        {
            onPlayerMove.onEventRaised += OnPlayerMove;
            onPlayerAttackBegin.onEventRaised += OnPlayerAttackBegin;
            onPlayerAttackEnd.onEventRaised += OnPlayerAttackEnd;
            onPlayerDefenceBegin.onEventRaised += OnPlayerDefenceBegin;
            onPlayerDefenceEnd.onEventRaised += OnPlayerDefenceEnd;
            onPlayerJump.onEventRaised += OnPlayerJump;
            onPlayerSprint.onEventRaised += OnPlayerSprint;
        }

        void OnDisable()
        {
            onPlayerMove.onEventRaised -= OnPlayerMove;
            onPlayerAttackBegin.onEventRaised += OnPlayerAttackBegin;
            onPlayerAttackEnd.onEventRaised += OnPlayerAttackEnd;
            onPlayerDefenceBegin.onEventRaised += OnPlayerDefenceBegin;
            onPlayerDefenceEnd.onEventRaised += OnPlayerDefenceEnd;
            onPlayerJump.onEventRaised -= OnPlayerJump;
            onPlayerSprint.onEventRaised -= OnPlayerSprint;
        }

        void Update()
        {
            // if (!isInteracting)
                playerMovement.Move(lastMovement);
        }

        void OnPlayerMove(Vector2 value)
        {
            lastMovement = value;
        }

        void OnPlayerAttackBegin()
        {
            playerCombat.AttackBegin();
        }

        void OnPlayerAttackEnd()
        {
            playerCombat.AttackEnd();
        }

        void OnPlayerDefenceBegin()
        {
            playerCombat.DefenceBegin();
        }

        void OnPlayerDefenceEnd()
        {
            playerCombat.DefenceEnd();
        }

        void OnPlayerJump()
        {
            playerMovement.Jump();
        }

        void OnPlayerSprint(bool isSprinting)
        {
            playerMovement.Sprint(isSprinting);
        }
    }
}